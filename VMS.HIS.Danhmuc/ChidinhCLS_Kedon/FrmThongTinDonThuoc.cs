using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.DAL;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.Libs;
using NLog;

namespace VNS.HIS.UI.Forms.NGOAITRU
{
    public partial class FrmThongTinDonThuoc : Form
    {
        private string KIEU_THUOC_VT;
        public int idkhosaochep = -1;
        public int id_kham = -1;
        private long IdDonthuoc = -1;
        public DateTime ngaykedon = new DateTime();
        private int tu_tuc = 0;
        public DataTable DtIcd = new DataTable();
        public DataTable dt_ICD_PHU = new DataTable();
        private decimal _bhytPtramTraituyennoitru;
        public CallAction em_CallAction = CallAction.FromMenu;
        private string madoituong_gia = "DV";
        private DataTable m_dtqheCamchidinhChungphieu = new DataTable();
        public Janus.Windows.GridEX.GridEX grd_ICD;
        public DataTable m_dtDanhmucthuoc = new DataTable();
        public KcbChandoanKetluan KcbChandoanKetluan = null;
        public DataTable m_dtDonthuocChitiet_View = new DataTable();
        private TDmucKho objDKho = null;
        private KcbLuotkham objLuotkham = null;
        public FrmThongTinDonThuoc(string kt_vatu)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            KIEU_THUOC_VT = kt_vatu;
        }
        private readonly KCB_KEDONTHUOC _kedonthuoc = new KCB_KEDONTHUOC();
        public DataTable m_dtDonthuocChitiet = null;

        private void LoadThongTinChanDoan(string maBenh)
        {
            var objMaBenh = new Select().From(DmucBenh.Schema).Where(DmucBenh.Columns.MaBenh).IsEqualTo(maBenh).ExecuteSingle<DmucBenh>();
            if (objMaBenh != null)
                txtChanDoan.Text = Utility.sDbnull(objMaBenh.TenBenh, "");
        }
        private void FrmThongTinDonThuoc_Load(object sender, EventArgs e)
        {
            // 
             objDKho =
                new Select().From(TDmucKho.Schema)
                    .Where(TDmucKho.Columns.IdKho)
                    .IsEqualTo(idkhosaochep)
                    .ExecuteSingle<TDmucKho>();
             objLuotkham =
                new Select().From(KcbLuotkham.Schema)
                    .Where(KcbLuotkham.Columns.MaLuotkham)
                    .IsEqualTo(txtPatientCode.Text.Trim())
                    .ExecuteSingle<KcbLuotkham>();
            m_dtqheCamchidinhChungphieu = globalVariables.gv_dtDmucQheCamCLSChungPhieu;
            _bhytPtramTraituyennoitru =
                   Utility.DecimaltoDbnull(
                       THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
            if(!string.IsNullOrEmpty(txtMaBenhChinh.Text.Trim()))  LoadThongTinChanDoan(txtMaBenhChinh.Text);
            m_dtDonthuocChitiet = _kedonthuoc.LaythongtinchitietdonthuocDeSaoChep(Utility.Int64Dbnull(txtPres_ID.Text));
            if (m_dtDonthuocChitiet.Rows.Count > 0)
            {
                Utility.SetDataSourceForDataGridEx(grdPresDetail, m_dtDonthuocChitiet, false, true, "", "");
                grdPresDetail.CheckAllRecords();
                m_dtDonthuocChitiet_View = m_dtDonthuocChitiet.Clone();
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private ActionResult _temp = ActionResult.Success;
        private readonly Dictionary<long, string> lstChangeData = new Dictionary<long, string>();
        private bool hasChanged;
        public bool isSaved;
        private readonly Logger log;
        public action em_Action = action.Insert;
        private bool isChanged(string value)
        {
            string[] strArray = value.Split(new[] { '-' });
            if (strArray.Length != 2)
            {
                return false;
            }
            return (Utility.Int32Dbnull(strArray[0], 0) != Utility.Int32Dbnull(strArray[1], 0));
        }

        private bool IsvalidData()
        {
            if (Utility.Int16Dbnull(idkhosaochep) <= 0)
            {
                Utility.ShowMsg("Bạn chưa chọn kho thuốc để sao chép!", "Cảnh báo", MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void cmdSavePres_Click(object sender, EventArgs e)
        {

            //if(IsvalidData()) return;
            //cmdSavePres.Enabled = false;
            isSaved = true;
            this.Close();
        }
        private void AddPreDetail()
        {
            try
            {
                string errMsg = string.Empty;
                string errMsg_temp = string.Empty;
                log.Trace("Get thong tin cac doi tuong can de luu don thuoc"); 
            
                if (objDKho == null) return;
                if (objLuotkham == null) return;
                    log.Trace(
                        "Bat dau them chi tiet don thuoc.......................................................................................");
                foreach (GridEXRow _row in grdPresDetail.GetCheckedRows())
                {
                    if (Utility.Int32Dbnull(objDKho.KtraTon) == 1)
                    {
                        decimal num = CommonLoadDuoc.SoLuongTonTrongKho(-1L,
                            Utility.Int32Dbnull(idkhosaochep), Utility.Int32Dbnull(_row.Cells["id_thuoc"].Value, -1), Utility.Int64Dbnull(_row.Cells["id_thuockho"].Value, -1),
                            Utility.Int32Dbnull(
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("KIEMTRATHUOC_CHOXACNHAN", "1", false), 1),
                            Utility.ByteDbnull(objLuotkham.Noitru, 0));
                        log.Trace("1. Lay xong so luong ton kho ke don");
                        if (Utility.DecimaltoDbnull(_row.Cells["so_luong"].Value, 0) > num)
                        {
                            Utility.ShowMsg(
                                string.Format(
                                    "Số lượng " + THU_VIEN_CHUNG.laytenthuoc_vattu(KIEU_THUOC_VT) +
                                    " cấp phát {0} vượt quá số lượng " +
                                    THU_VIEN_CHUNG.laytenthuoc_vattu(KIEU_THUOC_VT) +
                                    " trong kho {1}.\nCó thể trong lúc bạn chọn " +
                                    THU_VIEN_CHUNG.laytenthuoc_vattu(KIEU_THUOC_VT) +
                                    " chưa kịp đưa vào đơn, các Bác sĩ khác hoặc Dược sĩ đã kê hoặc cấp phát mất một lượng " +
                                    THU_VIEN_CHUNG.laytenthuoc_vattu(KIEU_THUOC_VT) +
                                    " so với thời điểm bạn chọn.\nMời bạn liên hệ phòng Dược kiểm tra lại",
                                   Utility.sDbnull(_row.Cells["so_luong"].Value), num), "Cảnh báo", MessageBoxIcon.Hand);
                            bool selected = _row.Selected;
                            return;
                        }
                    }
                    DataTable listdata =
                        new XuatThuoc().GetObjThuocKhoCollection(
                            Utility.Int32Dbnull(idkhosaochep, 0),
                            Utility.Int32Dbnull(_row.Cells["id_thuoc"].Value, -1),
                            Utility.Int32Dbnull(_row.Cells["id_thuockho"].Value, -1),
                            (decimal)Utility.DecimaltoDbnull(_row.Cells["so_luong"].Value, 0),
                            Utility.ByteDbnull(objLuotkham.IdLoaidoituongKcb, 0),
                            Utility.ByteDbnull(objLuotkham.DungTuyen, 0), (byte)objLuotkham.Noitru);
                    var list2 = new List<KcbDonthuocChitiet>();
                    foreach (DataRow thuockho in listdata.Rows)
                    {
                        decimal _soluong = Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.SoLuong], 0);
                        if (_soluong > 0)
                        {
                            DataRow[] rowArray =
                                m_dtDonthuocChitiet.Select(TThuockho.Columns.IdThuockho + "=" +
                                                           Utility.sDbnull(
                                                               thuockho[TThuockho.Columns.IdThuockho]) +
                                                           " AND tu_tuc=" + (0));
                            if (rowArray.Length > 0)
                            {
                                rowArray[0][KcbDonthuocChitiet.Columns.SoLuong] =
                                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) +
                                    _soluong;
                                rowArray[0]["TT_KHONG_PHUTHU"] =
                                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]);
                                rowArray[0]["TT"] =
                                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                    (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]) +
                                     Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu]));
                                rowArray[0]["TT_BHYT"] =
                                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                                rowArray[0]["TT_BN"] =
                                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                    (Utility.DecimaltoDbnull(
                                        rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                                     Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                                rowArray[0]["TT_PHUTHU"] =
                                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                                rowArray[0]["TT_BN_KHONG_PHUTHU"] =
                                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                    Utility.DecimaltoDbnull(
                                        rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                                AddtoView(rowArray[0], _soluong);
                                list2.Add(getNewItem(rowArray[0]));
                            }
                            else
                            {
                               DataRow  row = m_dtDonthuocChitiet.NewRow();
                                row[DmucThuoc.Columns.TenThuoc] = Utility.sDbnull(_row.Cells["ten_thuoc"].Value, "");
                                row[KcbDonthuocChitiet.Columns.SoLuong] = _soluong;

                                row[KcbDonthuocChitiet.Columns.PhuThu] =
                                    Utility.DecimaltoDbnull(thuockho["phu_thu"], 0);
                                ;
                                // !this.Giathuoc_quanhe ? 0M : Utility.DecimaltoDbnull(this.txtSurcharge.Text, 0);
                                row[KcbDonthuocChitiet.Columns.PhuthuDungtuyen] =
                                    Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.PhuthuDungtuyen], 0);
                                row[KcbDonthuocChitiet.Columns.PhuthuTraituyen] =
                                    Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.PhuthuTraituyen], 0);

                                row[KcbDonthuocChitiet.Columns.IdThuoc] =
                                    Utility.Int32Dbnull(_row.Cells["id_thuoc"].Value, -1);
                                row[KcbDonthuocChitiet.Columns.IdDonthuoc] = IdDonthuoc;
                                row["IsNew"] = 1;
                                row[KcbDonthuocChitiet.Columns.MadoituongGia] = Utility.sDbnull(_row.Cells["madoituong_gia"].Value, "");
                                row[KcbDonthuocChitiet.Columns.IdThuockho] =
                                    Utility.Int64Dbnull(thuockho[TThuockho.Columns.IdThuockho], -1);
                                row[KcbDonthuocChitiet.Columns.GiaNhap] =
                                    Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaNhap], 0);
                                row[KcbDonthuocChitiet.Columns.GiaBan] =
                                    Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaBan], 0);
                                row[KcbDonthuocChitiet.Columns.GiaBhyt] =
                                    Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaBhyt], 0);
                                row[KcbDonthuocChitiet.Columns.Vat] =
                                    Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.Vat], 0);
                                row[KcbDonthuocChitiet.Columns.SoLo] =
                                    Utility.sDbnull(thuockho[TThuockho.Columns.SoLo], "");
                                row[KcbDonthuocChitiet.Columns.SoDky] =
                                    Utility.sDbnull(thuockho[TThuockho.Columns.SoDky], "");
                                row[KcbDonthuocChitiet.Columns.SoQdinhthau] =
                                    Utility.sDbnull(thuockho[TThuockho.Columns.SoQdinhthau], "");
                                row[KcbDonthuocChitiet.Columns.MaNhacungcap] =
                                    Utility.sDbnull(thuockho[TThuockho.Columns.MaNhacungcap], "");
                                row["ten_donvitinh"] = Utility.sDbnull(_row.Cells["ten_donvitinh"].Value, "");
                                row["sNgay_hethan"] = Utility.sDbnull(thuockho["sNgay_hethan"], "");
                                row["sNgay_nhap"] = Utility.sDbnull(thuockho["sNgay_nhap"], "");
                                row[KcbDonthuocChitiet.Columns.NgayHethan] =
                                    thuockho[TThuockho.Columns.NgayHethan];
                                row[KcbDonthuocChitiet.Columns.NgayNhap] = thuockho[TThuockho.Columns.NgayNhap];
                                row[KcbDonthuocChitiet.Columns.IdKho] =
                                    Utility.Int32Dbnull(idkhosaochep, -1);
                                row[TDmucKho.Columns.TenKho] = Utility.sDbnull(objDKho.TenKho, -1);
                                row[KcbDonthuocChitiet.Columns.DonviTinh] = Utility.sDbnull(_row.Cells["ten_donvitinh"].Value, "");
                                row[DmucThuoc.Columns.MaHoatchat] = Utility.sDbnull(_row.Cells["hoat_chat"].Value, "");
                                row[KcbDonthuocChitiet.Columns.ChidanThem] = Utility.sDbnull(_row.Cells["chidan_them"].Value, "");
                                row[KcbDonthuocChitiet.Columns.MotaThem] = Utility.sDbnull(_row.Cells["mota_them_chitiet"].Value, "");
                                row["mota_them_chitiet"] = Utility.sDbnull(_row.Cells["mota_them_chitiet"].Value, "");
                                row[KcbDonthuocChitiet.Columns.CachDung] = Utility.sDbnull(_row.Cells["cach_dung"].Value, "");
                                row[KcbDonthuocChitiet.Columns.SoluongDung] = Utility.sDbnull(_row.Cells["soluong_dung"].Value, "");
                                row[KcbDonthuocChitiet.Columns.SolanDung] = Utility.sDbnull(_row.Cells["solan_dung"].Value, "");
                                row["ma_loaithuoc"] = Utility.sDbnull(_row.Cells["ma_loaithuoc"].Value, "");
                                row["ten_loaithuoc"] = Utility.sDbnull(_row.Cells["ten_loaithuoc"].Value, "");
                                row[KcbDonthuocChitiet.Columns.TrangthaiThanhtoan] = 0;
                                row[KcbDonthuocChitiet.Columns.SttIn] = GetMaxSTT(m_dtDonthuocChitiet);
                                row[KcbDonthuocChitiet.Columns.TuTuc] = 0;
                                row[KcbDonthuocChitiet.Columns.DonGia] =
                                    Utility.DecimaltoDbnull(thuockho["GIA_BAN"], 0);
                                // (this.APDUNG_GIATHUOC_DOITUONG || this.Giathuoc_quanhe) ?
                                // (Utility.DecimaltoDbnull(this.txtPrice.Text, 0)) : 
                                // (this.objLuotkham.IdLoaidoituongKcb== 1 ? 
                                //Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaBan],0) : Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaBhyt],0));
                                row[KcbDonthuocChitiet.Columns.PtramBhyt] = objLuotkham.PtramBhyt;
                                row[KcbDonthuocChitiet.Columns.PtramBhytGoc] = objLuotkham.PtramBhytGoc;
                                row[KcbDonthuocChitiet.Columns.MaDoituongKcb] = objLuotkham.MaDoituongKcb;
                                row[KcbDonthuocChitiet.Columns.KieuBiendong] = thuockho["kieubiendong"];
                                if (em_CallAction == CallAction.FromMenu)
                                {
                                    if (tu_tuc == 0)
                                    {
                                        decimal BHCT = 0m;
                                        if (objLuotkham.DungTuyen == 1)
                                        {
                                            BHCT =
                                                Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia],
                                                    0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                                        }
                                        else
                                        {
                                            if (objLuotkham.TrangthaiNoitru <= 0)
                                                BHCT =
                                                    Utility.DecimaltoDbnull(
                                                        row[KcbDonthuocChitiet.Columns.DonGia], 0) *
                                                    (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                                            else //Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                                                BHCT =
                                                    Utility.DecimaltoDbnull(
                                                        row[KcbDonthuocChitiet.Columns.DonGia], 0) *
                                                    (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) *
                                                    (_bhytPtramTraituyennoitru / 100);
                                        }
                                        // decimal num2 = (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * Utility.DecimaltoDbnull(this.objLuotkham.PtramBhyt, 0)) / 100M;
                                        decimal num3 =
                                            Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) -
                                            BHCT;
                                        row[KcbDonthuocChitiet.Columns.BhytChitra] = BHCT;
                                        row[KcbDonthuocChitiet.Columns.BnhanChitra] = num3;
                                    }
                                    else
                                    {
                                        row[KcbDonthuocChitiet.Columns.PtramBhyt] = 0;
                                        row[KcbDonthuocChitiet.Columns.BhytChitra] = 0;
                                        row[KcbDonthuocChitiet.Columns.BnhanChitra] =
                                            Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0);
                                    }
                                }
                                row["TT_KHONG_PHUTHU"] =
                                    Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                                    Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]);
                                row["TT"] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                                            (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]) +
                                             Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu]));
                                row["TT_BHYT"] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                                                 Utility.DecimaltoDbnull(
                                                     row[KcbDonthuocChitiet.Columns.BhytChitra]);
                                row["TT_BN"] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                                               (Utility.DecimaltoDbnull(
                                                   row[KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                                                Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu],
                                                    0));
                                row["TT_PHUTHU"] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                                                   Utility.DecimaltoDbnull(
                                                       row[KcbDonthuocChitiet.Columns.PhuThu], 0);
                                row["TT_BN_KHONG_PHUTHU"] =
                                    Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                                    Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0);

                                errMsg_temp =
                                    KiemtraCamchidinhchungphieu(
                                        Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.IdThuoc], 0),
                                        Utility.sDbnull(row[DmucThuoc.Columns.TenThuoc], ""));
                                log.Trace("3. Đã kiểm tra xong cấm chỉ định chung đơn thuốc");
                                if (errMsg_temp != string.Empty)
                                {
                                    errMsg += errMsg_temp;
                                }
                                else
                                {
                                    m_dtDonthuocChitiet.Rows.Add(row);
                                    AddtoView(row, _soluong);
                                    list2.Add(getNewItem(row));
                                }
                            }
                        }
                    }
                    if (errMsg != string.Empty)
                    {
                        if (errMsg.Contains("Single-Service:"))
                        {
                            Utility.ShowMsg(
                                "Thuốc sau được đánh dấu không được phép kê chung đơn bất kỳ Thuốc nào. Đề nghị bạn kiểm tra lại:\n" +
                                Utility.DoTrim(errMsg.Replace("Single-Service:", "")));
                        }
                        else
                            Utility.ShowMsg(
                                "Các cặp Thuốc sau đã được thiết lập chống kê chung đơn. Đề nghị bạn kiểm tra lại:\n" +
                                errMsg);
                    }
                    else
                    {
                        //PerformAction(list2.ToArray());
                       // Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuocChitiet.Columns.IdThuoc, Utility.sDbnull(_row.Cells["id_thuoc"].Value, -1));
                    }
                }
                   
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                log.Trace(
                    "KẾT THÚC THÊM CHI TIẾT THUỐC.......................................................................................");
            }
        }
        private void Create_ChandoanKetluan()
        {
            if (((Utility.DoTrim(txtTenBenhChinh.Text) != "") || (grd_ICD.GetDataRows().Length > 0)) ||
                (Utility.DoTrim(txtchandoan_new.Text) != ""))
            {
                SqlQuery sqlkt =
                    new Select().From(KcbChandoanKetluan.Schema)
                        .Where(KcbChandoanKetluan.Columns.IdKham)
                        .IsEqualTo(Utility.Int64Dbnull(id_kham));
                if (KcbChandoanKetluan == null || sqlkt.GetRecordCount() <= 0)
                {
                    KcbChandoanKetluan = new KcbChandoanKetluan();
                }
                KcbChandoanKetluan.IdKham = id_kham;
                KcbChandoanKetluan.MaLuotkham = objLuotkham.MaLuotkham;
                KcbChandoanKetluan.IdBenhnhan = objLuotkham.IdBenhnhan;
                KcbChandoanKetluan.MotaBenhchinh = Utility.sDbnull(txtTenBenhChinh.Text);
                KcbChandoanKetluan.MabenhChinh = Utility.sDbnull(txtMaBenhChinh.Text, "");
                if (Utility.Int16Dbnull(globalVariables.gv_intIDNhanvien, -1) > 0)
                {
                    KcbChandoanKetluan.IdBacsikham = Utility.Int16Dbnull(globalVariables.gv_intIDNhanvien, -1);
                }
                else
                {
                    KcbChandoanKetluan.IdBacsikham = globalVariables.gv_intIDNhanvien;
                }
                KcbChandoanKetluan.MabenhPhu = Utility.sDbnull(GetDanhsachBenhphu(), "");
                if (KcbChandoanKetluan.IsNew)
                {
                    KcbChandoanKetluan.NgayTao = ngaykedon;
                    KcbChandoanKetluan.NguoiTao = globalVariables.UserName;

                    KcbChandoanKetluan.IpMaytao = globalVariables.gv_strIPAddress;
                    KcbChandoanKetluan.TenMaytao = globalVariables.gv_strComputerName;
                }
                else
                {
                    KcbChandoanKetluan.NgaySua = ngaykedon;
                    KcbChandoanKetluan.NguoiSua = globalVariables.UserName;

                    KcbChandoanKetluan.IpMaysua = globalVariables.gv_strIPAddress;
                    KcbChandoanKetluan.TenMaysua = globalVariables.gv_strComputerName;
                }
                KcbChandoanKetluan.NgayChandoan = ngaykedon;
                KcbChandoanKetluan.IdBacsikham = globalVariables.gv_intIDNhanvien;
                KcbChandoanKetluan.Chandoan = Utility.ReplaceString(txtChanDoan.Text);
                if (objLuotkham.Noitru != null) KcbChandoanKetluan.Noitru = (byte)objLuotkham.Noitru;
            }
        }
        private string GetDanhsachBenhphu()
        {
            var builder = new StringBuilder("");
            try
            {
                int num = 0;
                if (DtIcd.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_ICD_PHU.Rows)
                    {
                        if (num > 0)
                        {
                            builder.Append(",");
                        }
                        builder.Append(Utility.sDbnull(row[DmucBenh.Columns.MaBenh], ""));
                        num++;
                    }
                }
                return builder.ToString();
            }
            catch
            {
                return "";
            }
        }

        //private void PerformAction(KcbDonthuocChitiet[] arrPresDetail)
        //{
        //    isSaved = true;

        //    Create_ChandoanKetluan();

        //    switch (em_Action)
        //    {
        //        case action.Insert:
        //            ThemDonthuoc(arrPresDetail);
        //            break;

        //        case action.Update:
        //            CapnhatDonthuoc(arrPresDetail);
        //            break;
        //    }
        //}
        private KcbDonthuocChitiet getNewItem(DataRow drv)
        {
            KcbDonthuocChitiet chitiet;
            return new KcbDonthuocChitiet
            {
                IdDonthuoc = IdDonthuoc,
                IdChitietdonthuoc = Utility.Int32Dbnull(drv[KcbDonthuocChitiet.Columns.IdChitietdonthuoc], -1),
                IdKham = id_kham,
                IdKho = Utility.Int32Dbnull(drv[KcbDonthuocChitiet.Columns.IdKho], -1),
                IdThuoc = Utility.Int16Dbnull(drv[KcbDonthuocChitiet.Columns.IdThuoc], -1),
                TrangthaiThanhtoan = Utility.ByteDbnull(drv[KcbDonthuocChitiet.Columns.TrangthaiThanhtoan], 0),
                SttIn = Utility.Int16Dbnull(drv[KcbDonthuocChitiet.Columns.SttIn], 1),
                TrangthaiHuy = 0,
                IdThuockho = Utility.Int32Dbnull(drv[KcbDonthuocChitiet.Columns.IdThuockho], -1),
                GiaNhap = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.GiaNhap], -1),
                GiaBan = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.GiaBan], -1),
                GiaBhyt = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.GiaBhyt], -1),
                Vat = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.Vat], -1),
                SoLo = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.SoLo], ""),
                SoDky = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.SoDky], ""),
                SoQdinhthau = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.SoQdinhthau], ""),
                MaNhacungcap = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.MaNhacungcap], -1),
                NgayHethan = Utility.ConvertDate(drv["sngay_hethan"].ToString()).Date,
                NgayNhap = Utility.ConvertDate(drv["sngay_nhap"].ToString()).Date,
                SoluongHuy = 0,
                SluongLinh = 0,
                SluongSua = 0,
                IdThanhtoan = -1,
                TrangthaiTonghop = 0,
                MadoituongGia =
                    Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.MadoituongGia], objLuotkham.MadoituongGia),
                TrangthaiChuyen = 0,
                IdGoi = -1,
                TrongGoi = 0,
                NguonThanhtoan = (byte)(objLuotkham.Noitru == 0 ? 0 : 1),
                TuTuc = Utility.ByteDbnull(drv[KcbDonthuocChitiet.Columns.TuTuc], 0),
                SoLuong = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.SoLuong], 0),
                DonGia = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.DonGia], 0),
                PhuThu = Utility.Int16Dbnull(drv[KcbDonthuocChitiet.Columns.PhuThu], 0),
                PhuthuDungtuyen = Utility.Int16Dbnull(drv[KcbDonthuocChitiet.Columns.PhuthuDungtuyen], 0),
                PhuthuTraituyen = Utility.Int16Dbnull(drv[KcbDonthuocChitiet.Columns.PhuthuTraituyen], 0),
                MotaThem = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.MotaThem], ""),
                TrangthaiBhyt = Utility.ByteDbnull(drv[KcbDonthuocChitiet.Columns.TuTuc], 0),
                TrangThai = 0,
                ChidanThem = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.ChidanThem], ""),
                CachDung = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.CachDung], ""),
                DonviTinh = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.DonviTinh], ""),
                SolanDung = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.SolanDung], null),
                SoluongDung = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.SoluongDung], null),
                NgayTao = globalVariables.SysDate,
                NguoiTao = globalVariables.UserName,
                BhytChitra = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.BhytChitra], 0),
                BnhanChitra = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.BnhanChitra], 0),
                PtramBhyt = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.PtramBhyt], 0),
                PtramBhytGoc = objLuotkham.PtramBhytGoc,
                MaDoituongKcb = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.MaDoituongKcb], "DV"),
                KieuBiendong = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.KieuBiendong], "EXP"),
                DaDung = 0,
                IpMaytao = globalVariables.gv_strIPAddress,
                TenMaytao = globalVariables.gv_strComputerName,
                IpMaysua = globalVariables.gv_strIPAddress,
                TenMaysua = globalVariables.gv_strComputerName
            };
        }
        private void AddtoView(DataRow newDr, decimal newQuantity)
        {
            DataRow[] rowArray =
                m_dtDonthuocChitiet_View.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" +
                                                Utility.sDbnull(newDr[KcbDonthuocChitiet.Columns.IdThuoc], "-1") +
                                                "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" +
                                                Utility.sDbnull(newDr[KcbDonthuocChitiet.Columns.DonGia], "-1")
                                                + " AND PHU_THU=" +
                                                Utility.sDbnull(newDr[KcbDonthuocChitiet.Columns.PhuThu], "-1")
                                                + " AND tu_tuc=" +
                                                Utility.sDbnull(newDr[KcbDonthuocChitiet.Columns.TuTuc], "-1")
                    );
            if (rowArray.Length <= 0)
            {
                m_dtDonthuocChitiet_View.ImportRow(newDr);
            }
            else
            {
                rowArray[0][KcbDonthuocChitiet.Columns.SoLuong] =
                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong], 0) + newQuantity;
                rowArray[0]["TT_KHONG_PHUTHU"] = Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                                 Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]);
                rowArray[0]["TT"] = Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                    (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]) +
                                     Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu]));
                rowArray[0]["TT_BHYT"] = Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                         Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                rowArray[0]["TT_BN"] = Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                       (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                                        Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                rowArray[0]["TT_PHUTHU"] = Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                           Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                rowArray[0]["TT_BN_KHONG_PHUTHU"] =
                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                rowArray[0][KcbDonthuocChitiet.Columns.SttIn] =
                    Math.Min(Utility.Int32Dbnull(newDr[KcbDonthuocChitiet.Columns.SttIn], 0),
                        Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                m_dtDonthuocChitiet_View.AcceptChanges();
            }
        }
        private string KiemtraCamchidinhchungphieu(int id_thuoc, string ten_chitiet)
        {
            string _reval = "";
            string _tempt = "";
            var lstKey = new List<string>();
            string _key = "";
            //Kiểm tra dịch vụ đang thêm có phải là dạng Single-Service hay không?
            DataRow[] _arrSingle =
                m_dtDanhmucthuoc.Select(DmucThuoc.Columns.SingleService + "=1 AND " + DmucThuoc.Columns.IdThuoc + "=" +
                                        id_thuoc);
            if (_arrSingle.Length > 0 &&
                m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdThuoc + "<>" + id_thuoc).Length > 0)
            {
                return string.Format("Single-Service: {0}", ten_chitiet);
            }
            //Kiểm tra các dịch vụ đã thêm có cái nào là Single-Service hay không?
            List<int> lstID =
                m_dtDonthuocChitiet.AsEnumerable()
                    .Select(c => Utility.Int32Dbnull(c[KcbDonthuocChitiet.Columns.IdThuoc], 0))
                    .Distinct()
                    .ToList();
            EnumerableRowCollection<DataRow> q = from p in m_dtDanhmucthuoc.AsEnumerable()
                                                 where Utility.ByteDbnull(p[DmucThuoc.Columns.SingleService], 0) == 1
                                                       && lstID.Contains(Utility.Int32Dbnull(p[DmucThuoc.Columns.IdThuoc], 0))
                                                 select p;
            if (q.Any())
            {
                return string.Format("Single-Service: {0}",
                    Utility.sDbnull(q.FirstOrDefault()[DmucThuoc.Columns.TenThuoc], ""));
            }
            //Lấy các cặp cấm chỉ định chung cùng nhau
            DataRow[] arrDr =
                m_dtqheCamchidinhChungphieu.Select(QheCamchidinhChungphieu.Columns.IdDichvu + "=" + id_thuoc);
            DataRow[] arrDr1 =
                m_dtqheCamchidinhChungphieu.Select(QheCamchidinhChungphieu.Columns.IdDichvuCamchidinhchung + "=" +
                                                   id_thuoc);
            foreach (DataRow dr in arrDr)
            {
                DataRow[] arrtemp =
                    m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" +
                                               Utility.sDbnull(
                                                   dr[QheCamchidinhChungphieu.Columns.IdDichvuCamchidinhchung]));
                if (arrtemp.Length > 0)
                {
                    foreach (DataRow dr1 in arrtemp)
                    {
                        _tempt = string.Empty;
                        _key = id_thuoc + "-" + Utility.sDbnull(dr1[KcbDonthuocChitiet.Columns.IdThuoc], "");
                        if (!lstKey.Contains(_key))
                        {
                            lstKey.Add(_key);
                            _tempt = string.Format("{0} - {1}", ten_chitiet,
                                Utility.sDbnull(dr1[DmucThuoc.Columns.IdThuoc], ""));
                        }
                        if (_tempt != string.Empty)
                            _reval += _tempt + "\n";
                    }
                }
            }
            foreach (DataRow dr in arrDr1)
            {
                DataRow[] arrtemp =
                    m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" +
                                               Utility.sDbnull(dr[QheCamchidinhChungphieu.Columns.IdDichvu]));
                if (arrtemp.Length > 0)
                {
                    foreach (DataRow dr1 in arrtemp)
                    {
                        _tempt = string.Empty;
                        _key = id_thuoc + "-" + Utility.sDbnull(dr1[KcbDonthuocChitiet.Columns.IdThuoc], "");
                        if (!lstKey.Contains(_key))
                        {
                            lstKey.Add(_key);
                            _tempt = string.Format("{0} - {1}", ten_chitiet,
                                Utility.sDbnull(dr1[DmucThuoc.Columns.TenThuoc], ""));
                        }
                        if (_tempt != string.Empty)
                            _reval += _tempt + "\n";
                    }
                }
            }
            return _reval;
        }
        private int GetMaxSTT(DataTable dataTable)
        {
            try
            {
                return
                    (Utility.Int32Dbnull(
                        dataTable.AsEnumerable().Max(c => c.Field<short>(KcbDonthuocChitiet.Columns.SttIn)), 0) + 1);
            }
            catch (Exception)
            {
                return 1;
            }
        }

        private void chkTutuc_CheckedChanged(object sender, EventArgs e)
        {
            tu_tuc = chkTutuc.Checked ? 1 : 0;
        }
    }
}
