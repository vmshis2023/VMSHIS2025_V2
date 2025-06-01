
    using CrystalDecisions.CrystalReports.Engine;
    using Janus.Windows.CalendarCombo;
    using Janus.Windows.EditControls;
    using Janus.Windows.GridEX;
    using Janus.Windows.GridEX.EditControls;
    using Janus.Windows.UI;
    using Janus.Windows.UI.StatusBar;
    using Janus.Windows.UI.Tab;
    
    using SubSonic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;
    using VNS.HIS.BusRule.Classes;
    using VMS.HIS.DAL;
    using VNS.HIS.NGHIEPVU.THUOC;
    using VNS.HIS.UCs;
    using VNS.Libs;
    using VNS.Properties;
    using VNS.UCs;
using VNS.HIS.UI.DANHMUC;
namespace VNS.HIS.UI.NGOAITRU
{
     public partial class frm_KCB_KeVacxin_Tiemchung : Form
    {
         decimal BHYT_PTRAM_TRAITUYENNOITRU = 0;
        private ActionResult _actionResult = ActionResult.Error;
        private bool _Found = false;
        public KcbChandoanKetluan _KcbChandoanKetluan;
        private KCB_KEDONTHUOC _KEDONTHUOC = new KCB_KEDONTHUOC();
        private VNS.Libs.MoneyByLetter _moneyByLetter = new VNS.Libs.MoneyByLetter();
        private string _rowFilter = "1=1";
        private ActionResult _temp = ActionResult.Success;
        private bool AllowTextChanged = false;
        private bool APDUNG_GIATHUOC_DOITUONG = (Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("APDUNG_GIATHUOC_DOITUONG", "0", true), 0) == 1);
        public bool m_blnCancel=true;
        private bool blnHasLoaded = false;
        public CallActionKieuKeDon CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;

        private long currentIdthuockho = 0L;
        public DataTable dt_ICD = new DataTable();
        public DataTable dt_ICD_PHU = new DataTable();

        private DataTable dtStockList = null;
        public action em_Action = action.Insert;
        public CallAction em_CallAction = CallAction.FromMenu;
        private bool FilterAgain;
        private bool Giathuoc_quanhe = false;

        private bool hasChanged = false;
        private bool hasMorethanOne = true;
        public int id_kham = -1;
        private long IdDonthuoc = -1;
        private bool isLike = true;
        public bool isLoaded = false;
        private bool isSaved = false;


        private string LOAIKHOTHUOC = "KHO";
        private Dictionary<long, string> lstChangeData = new Dictionary<long, string>();
        private bool m_blnGetDrugCodeFromList = false;
        private decimal m_decPrice = 0M;
        private DataTable m_dtCD_DVD = new DataTable();
        public DataTable m_dtDonthuocChitiet = new DataTable();
        public DataTable m_dtDonthuocChitiet_View = new DataTable();
        public DataTable m_dtDanhmucthuoc = new DataTable();
        private decimal m_Surcharge = 0M;
        private bool Manual = false;


        private QheDoituongThuoc objectPolicy = null;
        private QheDoituongThuoc objectPolicyTutuc = null;
        public short ObjectType_Id = -1;

        private string rowFilter = "1=2";
        private bool Selected;

        private string strSaveandprintPath = (Application.StartupPath + @"\CAUHINH\SaveAndPrintConfigKedonthuoc.txt");

        private string TEN_BENHPHU = "";
        private int tu_tuc = 0;

        public int v_Patient_ID = -1;
        public string v_PatientCode = "";

        public int noitru = 0;
        public int departmentID = -1;
        public string KIEU_THUOC_VT = "THUOC";
        public int id_goidv = -1;
        public byte trong_goi = 0;
        public bool forced2Add = false;
        public frm_KCB_KeVacxin_Tiemchung(string KIEU_THUOC_VT)
        {
            this.InitializeComponent();
           
            this.KIEU_THUOC_VT = KIEU_THUOC_VT;
            
            base.KeyPreview = true;
            this.dtpCreatedDate.Value = this.dtNgayIn.Value = this.dtNgayKhamLai.Value = globalVariables.SysDate;
            this.InitEvents();
            this.CauHinh();
        }

        private void AddBenhphu()
        {
            Func<DataRow, bool> predicate = null;
            try
            {
                try
                {
                    if ((this.txtMaBenhphu.Text.TrimStart(new char[0]).TrimEnd(new char[0]) != "") && !(this.txtTenBenhPhu.Text.TrimStart(new char[0]).TrimEnd(new char[0]) == ""))
                    {
                        if (predicate == null)
                        {
                            predicate = benh => Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == this.txtMaBenhphu.Text;
                        }
                        if (!this.dt_ICD_PHU.AsEnumerable().Where<DataRow>(predicate).Any<DataRow>())
                        {
                            this.AddMaBenh(this.txtMaBenhphu.Text, this.TEN_BENHPHU);
                            this.txtMaBenhphu.ResetText();
                            this.txtTenBenhPhu.ResetText();
                            this.txtMaBenhphu.Focus();
                            this.txtMaBenhphu.SelectAll();
                            this.Selected = false;
                        }
                        else
                        {
                            this.txtMaBenhphu.ResetText();
                            this.txtTenBenhPhu.ResetText();
                            this.txtMaBenhphu.Focus();
                            this.txtMaBenhphu.SelectAll();
                        }
                    }
                }
                catch (Exception)
                {
                    Utility.ShowMsg("Có lỗi trong quá trình thêm thông tin vào lưới");
                }
            }
            finally
            {
            }
        }

        private void AddMaBenh(string MaBenh, string TenBenh)
        {
            Func<DataRow, bool> predicate = null;
            if (!this.dt_ICD_PHU.AsEnumerable().Where<DataRow>(benh => (Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == MaBenh)).Any<DataRow>())
            {
                DataRow row = this.dt_ICD_PHU.NewRow();
                row[DmucBenh.Columns.MaBenh] = MaBenh;
                if (predicate == null)
                {
                    predicate = benh => Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == MaBenh;
                }
                EnumerableRowCollection<string> source = globalVariables.gv_dtDmucBenh.AsEnumerable().Where<DataRow>(predicate).Select<DataRow, string>(benh => Utility.sDbnull(benh[DmucBenh.Columns.TenBenh]));
                if (source.Any<string>())
                {
                    row[DmucBenh.Columns.TenBenh] = Utility.sDbnull(source.FirstOrDefault<string>());
                }
                this.dt_ICD_PHU.Rows.Add(row);
                this.dt_ICD_PHU.AcceptChanges();
                this.grd_ICD.AutoSizeColumns();
            }
        }
        string KiemtraCamchidinhchungphieu(int id_thuoc, string ten_chitiet)
        {
            string _reval = "";
            string _tempt = "";
            List<string> lstKey = new List<string>();
            string _key = "";
            //Kiểm tra dịch vụ đang thêm có phải là dạng Single-Service hay không?
            DataRow[] _arrSingle = m_dtDanhmucthuoc.Select(DmucThuoc.Columns.SingleService + "=1 AND " + DmucThuoc.Columns.IdThuoc + "=" + id_thuoc);
            if (_arrSingle.Length > 0 && m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdThuoc + "<>" + id_thuoc.ToString()).Length > 0)
            {
                return string.Format("Single-Service: {0}", ten_chitiet);
            }
            //Kiểm tra các dịch vụ đã thêm có cái nào là Single-Service hay không?
            List<int> lstID = m_dtDonthuocChitiet.AsEnumerable().Select(c => Utility.Int32Dbnull(c[KcbDonthuocChitiet.Columns.IdThuoc], 0)).Distinct().ToList<int>();
            var q = from p in m_dtDanhmucthuoc.AsEnumerable()
                    where Utility.ByteDbnull(p[DmucThuoc.Columns.SingleService], 0) == 1
                    && lstID.Contains(Utility.Int32Dbnull(p[DmucThuoc.Columns.IdThuoc], 0))
                    select p;
            if (q.Any())
            {
                return string.Format("Single-Service: {0}", Utility.sDbnull(q.FirstOrDefault()[DmucThuoc.Columns.TenThuoc], ""));
            }
            //Lấy các cặp cấm chỉ định chung cùng nhau
            DataRow[] arrDr = m_dtqheCamchidinhChungphieu.Select(QheCamchidinhChungphieu.Columns.IdDichvu + "=" + id_thuoc);
            DataRow[] arrDr1 = m_dtqheCamchidinhChungphieu.Select(QheCamchidinhChungphieu.Columns.IdDichvuCamchidinhchung + "=" + id_thuoc);
            foreach (DataRow dr in arrDr)
            {

                DataRow[] arrtemp = m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + Utility.sDbnull(dr[QheCamchidinhChungphieu.Columns.IdDichvuCamchidinhchung]));
                if (arrtemp.Length > 0)
                {

                    foreach (DataRow dr1 in arrtemp)
                    {
                        _tempt = string.Empty;
                        _key = id_thuoc.ToString() + "-" + Utility.sDbnull(dr1[KcbDonthuocChitiet.Columns.IdThuoc], "");
                        if (!lstKey.Contains(_key))
                        {
                            lstKey.Add(_key);
                            _tempt = string.Format("{0} - {1}", ten_chitiet, Utility.sDbnull(dr1[DmucThuoc.Columns.IdThuoc], ""));
                        }
                        if (_tempt != string.Empty)
                            _reval += _tempt + "\n";
                    }

                }
            }
            foreach (DataRow dr in arrDr1)
            {

                DataRow[] arrtemp = m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + Utility.sDbnull(dr[QheCamchidinhChungphieu.Columns.IdDichvu]));
                if (arrtemp.Length > 0)
                {

                    foreach (DataRow dr1 in arrtemp)
                    {
                        _tempt = string.Empty;
                        _key = id_thuoc.ToString() + "-" + Utility.sDbnull(dr1[KcbDonthuocChitiet.Columns.IdThuoc], "");
                        if (!lstKey.Contains(_key))
                        {
                            lstKey.Add(_key);
                            _tempt = string.Format("{0} - {1}", ten_chitiet, Utility.sDbnull(dr1[DmucThuoc.Columns.TenThuoc], ""));
                        }
                        if (_tempt != string.Empty)
                            _reval += _tempt + "\n";
                    }
                }
            }
            return _reval;
        }
        private void AddPreDetail()
        {
            try
            {
                 string errMsg = string.Empty;
                string errMsg_temp = string.Empty;
                this.setMsg(this.lblMsg, "", false);
                this.tu_tuc = this.chkTutuc.Checked ? 1 : 0;
                if (objDKho == null)
                {
                    this.setMsg(this.lblMsg, "Bạn cần chọn kho thuốc trước khi chọn thuốc kê đơn", true);
                    cboStock.Focus();
                    return;
                }
                else if (Utility.Int32Dbnull(this.txtDrugID.Text) < 0)
                {
                    this.txtdrug.Focus();
                    this.txtdrug.SelectAll();
                    return;
                }
                else if ((noitru==0 && Utility.DecimaltoDbnull(this.txtSoluong.Text,0) <= 0)||(noitru==1 && Utility.DecimaltoDbnull(this.txtSoluong.Text,0) <= 0 && Utility.Int32Dbnull(txtDonvichiaBut.Text,0)<=0))
                {
                    this.setMsg(this.lblMsg, "Số lượng "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư") +" phải lớn hơn 0", true);
                    this.txtSoluong.Focus();
                    return;
                }
                else if (Utility.Int32Dbnull(this.txtGioihanke.Text, -1) > 0 && Utility.DecimaltoDbnull(this.txtSoluong.Text, 0) > Utility.Int32Dbnull(this.txtGioihanke.Text, -1))
                {
                    this.setMsg(this.lblMsg, "Thuốc đã đặt giới hạn kê tối đa 1 lần nhỏ hơn hoặc bằng " + Utility.Int32Dbnull(this.txtGioihanke.Text, 0).ToString()+" "+txtDonViDung.Text, true);
                    this.txtSoluong.Focus();
                    return;
                }
                else
                {
                    if (Utility.Int32Dbnull(objDKho.KtraTon) == 1)
                    {
                        decimal num = CommonLoadDuoc.SoLuongTonTrongKho(-1L,
                            Utility.Int32Dbnull(this.cboStock.SelectedValue),
                            Utility.Int32Dbnull(this.txtDrugID.Text, -1),
                            txtdrug.GridView ? this.id_thuockho : (long) this.txtdrug.id_thuockho,
                            new int?(
                                Utility.Int32Dbnull(
                                    THU_VIEN_CHUNG.Laygiatrithamsohethong("KIEMTRATHUOC_CHOXACNHAN", "1", false), 1)),
                            Utility.ByteDbnull(objLuotkham.Noitru, 0));
                        if (Utility.DecimaltoDbnull(this.txtSoluong.Text,0) > num)
                        {
                            Utility.ShowMsg(string.Format("Số lượng "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư") +" cấp phát {0} vượt quá số lượng " +(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư")+" trong kho {1}.\nCó thể trong lúc bạn chọn "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư") +" chưa kịp đưa vào đơn, các Bác sĩ khác hoặc Dược sĩ đã kê hoặc cấp phát mất một lượng "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư")+" so với thời điểm bạn chọn.\nMời bạn liên hệ phòng Dược kiểm tra lại", this.txtSoluong.Text, num.ToString()), "Cảnh báo", MessageBoxIcon.Hand);
                            this.txtSoluong.Focus();
                            return;
                        }
                    }
                    if (!m_dtDonthuocChitiet.Columns.Contains("sngayhen_muiketiep")) m_dtDonthuocChitiet.Columns.Add(new DataColumn("sngayhen_muiketiep", typeof(string)));
                    DataTable listdata = new XuatThuoc().GetObjThuocKhoCollection(Utility.Int32Dbnull(this.cboStock.SelectedValue, 0), Utility.Int32Dbnull(this.txtDrugID.Text, -1), txtdrug.GridView ? this.id_thuockho : this.txtdrug.id_thuockho, (int)Utility.DecimaltoDbnull(this.txtSoluong.Text, 0), Utility.ByteDbnull(this.objLuotkham.IdLoaidoituongKcb.Value, 0), Utility.ByteDbnull(this.objLuotkham.DungTuyen.Value, 0), (byte)noitru);
                    List<KcbDonthuocChitiet> list2 = new List<KcbDonthuocChitiet>();
                    foreach (DataRow thuockho in listdata.Rows)
                    {
                        int _soluong=Utility.Int32Dbnull(thuockho[TThuockho.Columns.SoLuong],0);
                        if (_soluong > 0)
                        {
                            DataRow[] rowArray = this.m_dtDonthuocChitiet.Select(TThuockho.Columns.IdThuockho + "=" +Utility.sDbnull( thuockho[TThuockho.Columns.IdThuockho]));
                            if (rowArray.Length > 0)
                            {
                                rowArray[0][KcbDonthuocChitiet.Columns.SoLuong] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) + _soluong;
                                rowArray[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]);
                                rowArray[0]["TT"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu]));
                                rowArray[0]["TT_BHYT"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                                rowArray[0]["TT_BN"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                                rowArray[0]["TT_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                                rowArray[0]["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                                this.AddtoView(rowArray[0],_soluong);
                                list2.Add(this.getNewItem(rowArray[0]));
                            }
                            else
                            {
                                byte? nullable;
                                DataRow row = this.m_dtDonthuocChitiet.NewRow();
                                row[DmucThuoc.Columns.TenThuoc] = Utility.sDbnull(this.txtDrug_Name.Text, "");
                                row[KcbDonthuocChitiet.Columns.SoLuong] = _soluong;
                                
                                row[KcbDonthuocChitiet.Columns.PhuThu] = Utility.DecimaltoDbnull(thuockho["phu_thu"], 0); ;// !this.Giathuoc_quanhe ? 0M : Utility.DecimaltoDbnull(this.txtSurcharge.Text, 0);
                                row[KcbDonthuocChitiet.Columns.PhuthuDungtuyen] = Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.PhuthuDungtuyen], 0);
                                row[KcbDonthuocChitiet.Columns.PhuthuTraituyen] = Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.PhuthuTraituyen],0);

                                row[KcbDonthuocChitiet.Columns.IdThuoc] = Utility.Int32Dbnull(this.txtDrugID.Text, -1);
                                row[KcbDonthuocChitiet.Columns.IdDonthuoc] = this.IdDonthuoc;
                                row["IsNew"] = 1;
                                row[KcbDonthuocChitiet.Columns.MadoituongGia] = madoituong_gia;
                                row[KcbDonthuocChitiet.Columns.IdThuockho] = Utility.Int64Dbnull(thuockho[TThuockho.Columns.IdThuockho],-1);
                                row[KcbDonthuocChitiet.Columns.GiaNhap] = Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaNhap],0);
                                row[KcbDonthuocChitiet.Columns.GiaBan] = Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaBan],0);
                                row[KcbDonthuocChitiet.Columns.GiaBhyt] = Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaBhyt], 0);
                                row[KcbDonthuocChitiet.Columns.Vat] = Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.Vat],0);
                                row[KcbDonthuocChitiet.Columns.SoLo] = Utility.sDbnull(thuockho[TThuockho.Columns.SoLo],"");
                                row[KcbDonthuocChitiet.Columns.SoDky] = Utility.sDbnull(thuockho[TThuockho.Columns.SoDky], "");
                                row[KcbDonthuocChitiet.Columns.SoQdinhthau] = Utility.sDbnull(thuockho[TThuockho.Columns.SoQdinhthau], "");
                                row[KcbDonthuocChitiet.Columns.MaNhacungcap] = Utility.sDbnull(thuockho[TThuockho.Columns.MaNhacungcap],"");
                                row["ten_donvitinh"] = this.txtDonViDung.Text;
                                row["sNgay_hethan"] = Utility.sDbnull(thuockho["sNgay_hethan"],"");
                                row["sNgay_nhap"] = Utility.sDbnull(thuockho["sNgay_nhap"], "");
                                row[KcbDonthuocChitiet.Columns.NgayHethan] = thuockho[TThuockho.Columns.NgayHethan];
                                row[KcbDonthuocChitiet.Columns.NgayNhap] = thuockho[TThuockho.Columns.NgayNhap];
                                row[KcbDonthuocChitiet.Columns.IdKho] = Utility.Int32Dbnull(this.cboStock.SelectedValue, -1);
                                row[TDmucKho.Columns.TenKho] = Utility.sDbnull(this.cboStock.Text, -1);
                                row[KcbDonthuocChitiet.Columns.DonviTinh] = this.txtDonViDung.Text;
                                row[DmucThuoc.Columns.MaHoatchat] = this.txtBietduoc.Text;
                                row[KcbDonthuocChitiet.Columns.ChidanThem] = this.txtChiDanThem.Text;
                                row[KcbDonthuocChitiet.Columns.MotaThem] = Utility.sDbnull(this.txtChiDanDungThuoc.Text);
                                row["mota_them_chitiet"] = Utility.sDbnull(this.txtChiDanDungThuoc.Text);
                                row[KcbDonthuocChitiet.Columns.CachDung] = Utility.sDbnull(this.txtCachDung.Text);
                                row[KcbDonthuocChitiet.Columns.SoluongDung] = Utility.sDbnull(this.txtSoLuongDung.Text);
                                row[KcbDonthuocChitiet.Columns.SolanDung] = Utility.sDbnull(this.txtSolan.Text);
                                row["ma_loaithuoc"] = this.txtdrugtypeCode.Text;
                                row[KcbDonthuocChitiet.Columns.TrangthaiThanhtoan] = 0;
                                row[KcbDonthuocChitiet.Columns.SttIn] = this.GetMaxSTT(this.m_dtDonthuocChitiet);
                                row[KcbDonthuocChitiet.Columns.TuTuc] = this.chkTutuc.Checked ? 1 : this.tu_tuc;
                                row[KcbDonthuocChitiet.Columns.DonGia] = Utility.DecimaltoDbnull(thuockho["GIA_BAN"], 0);
                                   // (this.APDUNG_GIATHUOC_DOITUONG || this.Giathuoc_quanhe) ?
                                   // (Utility.DecimaltoDbnull(this.txtPrice.Text, 0)) : 
                                   // (this.objLuotkham.IdLoaidoituongKcb== 1 ? 
                                   //Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaBan],0) : Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaBhyt],0));
                                row[KcbDonthuocChitiet.Columns.PtramBhyt] = this.objLuotkham.PtramBhyt;
                                row[KcbDonthuocChitiet.Columns.PtramBhytGoc] = this.objLuotkham.PtramBhytGoc;
                                row[KcbDonthuocChitiet.Columns.MaDoituongKcb] = this.MaDoiTuong;
                                row[KcbDonthuocChitiet.Columns.KieuBiendong] = thuockho["kieubiendong"];
                               
                                row[KcbDonthuocChitiet.Columns.NguoiTiem] = Utility.Int16Dbnull(txtNguoitiem.MyID,-1);
                                row[KcbDonthuocChitiet.Columns.LydoTiemchung] = txtLydotiem.myCode;
                                row[KcbDonthuocChitiet.Columns.MuiThu] = Utility.Int16Dbnull(txtMuithu.Text, 1);
                                row[KcbDonthuocChitiet.Columns.VitriTiem] = txtVitritiem.myCode;
                                if (chkHennhaclai.Checked)
                                {
                                    row["hen_nhaclai"] = dtpHennhaclai.Text;
                                    row[KcbDonthuocChitiet.Columns.NgayhenMuiketiep] = dtpHennhaclai.Value;
                                }
                                else
                                {
                                    row["hen_nhaclai"] = "Không hẹn";
                                    row[KcbDonthuocChitiet.Columns.NgayhenMuiketiep] = DBNull.Value;
                                }
                                row[KcbDonthuocChitiet.Columns.LydoTiemchung] = txtLydotiem.Text;
                                if (this.em_CallAction == CallAction.FromMenu)
                                {
                                    if (this.tu_tuc == 0)
                                    {
                                        decimal BHCT = 0m;
                                        if (objLuotkham.DungTuyen == 1)
                                        {
                                            BHCT = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                                        }
                                        else
                                        {
                                            if (objLuotkham.TrangthaiNoitru <= 0)
                                                BHCT = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                                            else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                                                BHCT = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                                        }
                                       // decimal num2 = (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * Utility.DecimaltoDbnull(this.objLuotkham.PtramBhyt, 0)) / 100M;
                                        decimal num3 = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) - BHCT;
                                        row[KcbDonthuocChitiet.Columns.BhytChitra] = BHCT;
                                        row[KcbDonthuocChitiet.Columns.BnhanChitra] = num3;
                                    }
                                    else
                                    {
                                        row[KcbDonthuocChitiet.Columns.PtramBhyt] = 0;
                                        row[KcbDonthuocChitiet.Columns.BhytChitra] = 0;
                                        row[KcbDonthuocChitiet.Columns.BnhanChitra] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0);
                                    }
                                }
                                row["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]);
                                row["TT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu]));
                                row["TT_BHYT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BhytChitra]);
                                row["TT_BN"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0));
                                row["TT_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0);
                                row["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                                errMsg_temp = KiemtraCamchidinhchungphieu(Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.IdThuoc], 0), Utility.sDbnull(row[DmucThuoc.Columns.TenThuoc], ""));
                                if (errMsg_temp != string.Empty)
                                {
                                    errMsg += errMsg_temp;
                                }
                                else
                                {
                                    this.m_dtDonthuocChitiet.Rows.Add(row);
                                    this.AddtoView(row, _soluong);
                                    list2.Add(this.getNewItem(row));
                                }
                            }
                        }
                    }
                    if (errMsg != string.Empty)
                    {
                        if (errMsg.Contains("Single-Service:"))
                        {
                            Utility.ShowMsg("Thuốc sau được đánh dấu không được phép kê chung đơn bất kỳ Thuốc nào. Đề nghị bạn kiểm tra lại:\n" + Utility.DoTrim(errMsg.Replace("Single-Service:", "")));
                        }
                        else
                            Utility.ShowMsg("Các cặp Thuốc sau đã được thiết lập chống kê chung đơn. Đề nghị bạn kiểm tra lại:\n" + errMsg);
                    }
                    else
                    {
                        this.PerformAction(list2.ToArray());
                        Utility.GotoNewRowJanus(this.grdPresDetail, KcbDonthuocChitiet.Columns.IdThuoc, this.txtDrugID.Text);
                        this.UpdateDataWhenChanged();
                       
                    }
                    this.ClearControl();
                    this.txtdrug.Focus();
                    this.txtdrug.SelectAll();
                    this.m_dtDanhmucthuoc.DefaultView.RowFilter = "1=2";
                    this.m_dtDanhmucthuoc.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void AddQuantity(int id_thuoc, int id_thuockho, int newQuantity)
        {
            try
            {
                this.tu_tuc = this.chkTutuc.Checked ? 1 : 0;
                this.setMsg(this.lblMsg, "", false);
                if (Utility.Int32Dbnull(objDKho.KtraTon) == 1)
                {
                    decimal num = CommonLoadDuoc.SoLuongTonTrongKho(-1L, Utility.Int32Dbnull(this.cboStock.SelectedValue),
                        id_thuoc, (long) id_thuockho,
                        new int?(
                            Utility.Int32Dbnull(
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("KIEMTRATHUOC_CHOXACNHAN", "1", false), 1)),
                        Utility.ByteDbnull(objLuotkham.Noitru, 0));
                    if (newQuantity > num)
                    {
                        Utility.ShowMsg("Số lượng "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư") +" cấp phát vượt quá số lượng "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư") +" trong kho. Mời bạn kiểm tra lại", "Cảnh báo", MessageBoxIcon.Hand);
                        this.txtSoluong.Focus();
                        return;
                    }
                }
                DataTable listdata = new XuatThuoc().GetObjThuocKhoCollection(Utility.Int32Dbnull(this.cboStock.SelectedValue, 0), id_thuoc, id_thuockho, newQuantity,Utility.ByteDbnull(this.objLuotkham.IdLoaidoituongKcb.Value, 0), Utility.ByteDbnull(this.objLuotkham.DungTuyen.Value, 0), (byte)noitru);
                List<KcbDonthuocChitiet> list2 = new List<KcbDonthuocChitiet>();
                foreach (DataRow thuockho in listdata.Rows)
                {
                    int _soluong = Utility.Int32Dbnull(thuockho[TThuockho.Columns.SoLuong], 0);
                    if (_soluong > 0)
                    {
                        DataRow[] rowArray = this.m_dtDonthuocChitiet.Select(TThuockho.Columns.IdThuockho + "=" + Utility.sDbnull( thuockho[TThuockho.Columns.IdThuockho]));
                        if (rowArray.Length > 0)
                        {
                            rowArray[0][KcbDonthuocChitiet.Columns.SoLuong] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) + _soluong;
                            newQuantity -= _soluong;
                            rowArray[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]);
                            rowArray[0]["TT"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu]));
                            rowArray[0]["TT_BHYT"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                            rowArray[0]["TT_BN"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                            rowArray[0]["TT_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                            rowArray[0]["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                            this.AddtoView(rowArray[0], _soluong);
                            list2.Add(this.getNewItem(rowArray[0]));
                        }
                        else
                        {
                            byte? nullable;
                            DataRow row = this.m_dtDonthuocChitiet.NewRow();
                            string donviTinh = "";
                            string chidanThem = "";
                            string motaThem = "";
                            string cachDung = "";
                            string soluongDung = "";
                            string solanDung = "";
                            string tenthuoc = "";
                            string str8 = "";
                            string hoatchat = "";
                            this.getInfor(id_thuoc, ref tenthuoc, ref str8, ref hoatchat, ref donviTinh, ref chidanThem, ref motaThem, ref cachDung, ref soluongDung, ref solanDung);
                            this.txtDrugID.Text = id_thuoc.ToString();
                            this.txtDrugID_TextChanged(this.txtDrugID, new EventArgs());
                            row[DmucThuoc.Columns.TenThuoc] = Utility.sDbnull(this.txtDrug_Name.Text, "");
                            row[KcbDonthuocChitiet.Columns.SoLuong] = _soluong;
                            row[KcbDonthuocChitiet.Columns.PhuThu] = Utility.DecimaltoDbnull(thuockho["phu_thu"], 0);// !this.Giathuoc_quanhe ? 0M : Utility.DecimaltoDbnull(this.txtSurcharge.Text, 0);
                            row[KcbDonthuocChitiet.Columns.PhuthuDungtuyen] = Utility.DecimaltoDbnull( thuockho[TThuockho.Columns.PhuthuDungtuyen],0);
                            row[KcbDonthuocChitiet.Columns.PhuthuTraituyen] = Utility.DecimaltoDbnull( thuockho[TThuockho.Columns.PhuthuTraituyen],0);
                            row[KcbDonthuocChitiet.Columns.IdThuoc] = id_thuoc;
                            row[KcbDonthuocChitiet.Columns.IdDonthuoc] = this.IdDonthuoc;
                            row["IsNew"] = 1;
                            row[KcbDonthuocChitiet.Columns.MadoituongGia] = madoituong_gia;
                            row[KcbDonthuocChitiet.Columns.IdThuockho] = Utility.Int64Dbnull( thuockho[TThuockho.Columns.IdThuockho],0);
                            row[KcbDonthuocChitiet.Columns.GiaNhap] =Utility.DecimaltoDbnull( thuockho[TThuockho.Columns.GiaNhap],0);
                            row[KcbDonthuocChitiet.Columns.GiaBan] = Utility.DecimaltoDbnull( thuockho[TThuockho.Columns.GiaBan],0);
                            row[KcbDonthuocChitiet.Columns.GiaBhyt] = Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaBhyt], 0);
                            row[KcbDonthuocChitiet.Columns.Vat] =Utility.DecimaltoDbnull( thuockho[TThuockho.Columns.Vat],0);
                            row[KcbDonthuocChitiet.Columns.SoLo] = Utility.sDbnull( thuockho[TThuockho.Columns.SoLo],"");
                            row[KcbDonthuocChitiet.Columns.SoDky] = Utility.sDbnull(thuockho[TThuockho.Columns.SoDky], "");
                            row[KcbDonthuocChitiet.Columns.SoQdinhthau] = Utility.sDbnull(thuockho[TThuockho.Columns.SoQdinhthau], "");
                            row[KcbDonthuocChitiet.Columns.MaNhacungcap] =Utility.sDbnull( thuockho[TThuockho.Columns.MaNhacungcap],"");
                            row["ten_donvitinh"] = this.txtDonViDung.Text;
                            row["sNgay_hethan"] = Utility.sDbnull(thuockho["sNgay_hethan"]);
                            row["sNgay_nhap"] = Utility.sDbnull(thuockho["sNgay_nhap"], "");
                            row[KcbDonthuocChitiet.Columns.NgayHethan] =thuockho[TThuockho.Columns.NgayHethan];
                            row[KcbDonthuocChitiet.Columns.NgayNhap] = thuockho[TThuockho.Columns.NgayNhap];
                            row[KcbDonthuocChitiet.Columns.IdKho] = Utility.Int32Dbnull(this.cboStock.SelectedValue, -1);
                            row[TDmucKho.Columns.TenKho] = Utility.sDbnull(this.cboStock.Text, -1);
                            row[KcbDonthuocChitiet.Columns.DonviTinh] = donviTinh;
                            row[DmucThuoc.Columns.MaHoatchat] = hoatchat;
                            row[KcbDonthuocChitiet.Columns.ChidanThem] = chidanThem;
                            row["mota_them_chitiet"] = chidanThem;
                            row[KcbDonthuocChitiet.Columns.MotaThem] = motaThem;
                            row[KcbDonthuocChitiet.Columns.CachDung] = cachDung;
                            row[KcbDonthuocChitiet.Columns.SoluongDung] = soluongDung;
                            row[KcbDonthuocChitiet.Columns.SolanDung] = solanDung;
                            row["ma_loaithuoc"] = this.txtdrugtypeCode.Text;
                            row[KcbDonthuocChitiet.Columns.TrangthaiThanhtoan] = 0;
                            row[KcbDonthuocChitiet.Columns.SttIn] = this.GetMaxSTT(this.m_dtDonthuocChitiet);
                            row[KcbDonthuocChitiet.Columns.TuTuc] = this.chkTutuc.Checked ? 1 : this.tu_tuc;
                            row[KcbDonthuocChitiet.Columns.DonGia] = Utility.DecimaltoDbnull(thuockho["GIA_BAN"], 0); ;// (this.APDUNG_GIATHUOC_DOITUONG || this.Giathuoc_quanhe) ? new decimal?(Utility.DecimaltoDbnull(this.txtPrice.Text, 0)) : ((((nullable = this.objLuotkham.IdLoaidoituongKcb).GetValueOrDefault() == 1) && nullable.HasValue) ? new decimal?(Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaBan], 0)) : Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaBhyt], 0));
                            row[KcbDonthuocChitiet.Columns.PtramBhyt] = this.objLuotkham.PtramBhyt;
                            row[KcbDonthuocChitiet.Columns.PtramBhytGoc] = this.objLuotkham.PtramBhytGoc;
                            row[KcbDonthuocChitiet.Columns.MaDoituongKcb] = this.MaDoiTuong;
                            row[KcbDonthuocChitiet.Columns.KieuBiendong] = thuockho["kieubiendong"];
                            if (this.em_CallAction == CallAction.FromMenu)
                            {
                                if (this.tu_tuc == 0)
                                {
                                    decimal BHCT = 0m;
                                    if (objLuotkham.DungTuyen == 1)
                                    {
                                        BHCT = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                                    }
                                    else
                                    {
                                        if (objLuotkham.TrangthaiNoitru <= 0)
                                            BHCT = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                                        else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                                            BHCT = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                                    }
                                    //decimal num2 = (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * Utility.DecimaltoDbnull(this.objLuotkham.PtramBhyt, 0)) / 100M;
                                    decimal num3 = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) - BHCT;
                                    row[KcbDonthuocChitiet.Columns.BhytChitra] = BHCT;
                                    row[KcbDonthuocChitiet.Columns.BnhanChitra] = num3;
                                    
                                    
                                }
                                else
                                {
                                    row[KcbDonthuocChitiet.Columns.PtramBhyt] = 0;
                                   
                                    row[KcbDonthuocChitiet.Columns.BhytChitra] = 0;
                                    row[KcbDonthuocChitiet.Columns.BnhanChitra] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0);
                                }
                            }
                            row["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]);
                            row["TT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu]));
                            row["TT_BHYT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BhytChitra]);
                            row["TT_BN"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0));
                            row["TT_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0);
                            row["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                            this.m_dtDonthuocChitiet.Rows.Add(row);
                            int num4 = newQuantity - _soluong;
                            this.AddtoView(row, (num4 > 0) ? _soluong : newQuantity);
                            list2.Add(this.getNewItem(row));
                        }
                    }
                }
                this.PerformAction(list2.ToArray());
                Utility.GotoNewRowJanus(this.grdPresDetail, KcbDonthuocChitiet.Columns.IdThuoc, this.txtDrugID.Text);
                this.UpdateDataWhenChanged();
                this.ClearControl();
                this.txtdrug.Focus();
                this.txtdrug.SelectAll();
                this.m_dtDanhmucthuoc.DefaultView.RowFilter = "1=2";
                this.m_dtDanhmucthuoc.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void AddtoView(DataRow newDr, int newQuantity)
        {
            DataRow[] rowArray = this.m_dtDonthuocChitiet_View.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + Utility.sDbnull(newDr[KcbDonthuocChitiet.Columns.IdThuoc], "-1") +
                "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + Utility.sDbnull(newDr[KcbDonthuocChitiet.Columns.DonGia], "-1")
                + " AND PHU_THU="+ Utility.sDbnull(newDr[KcbDonthuocChitiet.Columns.PhuThu], "-1"));
            if (rowArray.Length <= 0)
            {
                this.m_dtDonthuocChitiet_View.ImportRow(newDr);
            }
            else
            {
                rowArray[0][KcbDonthuocChitiet.Columns.SoLuong] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong], 0) + newQuantity;
                rowArray[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]);
                rowArray[0]["TT"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu]));
                rowArray[0]["TT_BHYT"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                rowArray[0]["TT_BN"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                rowArray[0]["TT_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                rowArray[0]["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                rowArray[0][KcbDonthuocChitiet.Columns.SttIn] = Math.Min(Utility.Int32Dbnull(newDr[KcbDonthuocChitiet.Columns.SttIn], 0), Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                this.m_dtDonthuocChitiet_View.AcceptChanges();
            }
        }

        private void AutoCompleteDmucChung()
        {
            try
            {
                try
                {
                    List<string> lstLoai = new List<string> { "CDDT" };
                    DataTable source = THU_VIEN_CHUNG.LayDulieuDanhmucChung(lstLoai, true);
                    if (source != null)
                    {
                        if (!source.Columns.Contains("ShortCut"))
                        {
                            source.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                        }
                        foreach (DataRow row in source.Rows)
                        {
                            string str = "";
                            string str2 = row["TEN"].ToString().Trim() + " " + Utility.Bodau(row["TEN"].ToString().Trim());
                            str = row["MA"].ToString().Trim();
                            string[] strArray = str2.ToLower().Split(new char[] { ' ' });
                            string str3 = "";
                            foreach (string str5 in strArray)
                            {
                                if (str5.Trim() != "")
                                {
                                    str3 = str3 + str5 + " ";
                                }
                            }
                            str = str + str3;
                            foreach (string str5 in strArray)
                            {
                                if (str5.Trim() != "")
                                {
                                    str = str + str5.Substring(0, 1);
                                }
                            }
                            row["ShortCut"] = str;
                        }
                        List<string> list = new List<string>();
                        list = source.AsEnumerable().Where<DataRow>(p => (p.Field<string>("LOAI").ToString() == "CDDT")).Select<DataRow, string>(p => ("-1#" + p.Field<string>("MA").ToString() + "@" + p.Field<string>("TEN").ToString() + "@" + p.Field<string>("shortcut").ToString())).ToList<string>();
                        this.txtCachDung.AutoCompleteList = list;
                        this.txtCachDung.TextAlign = HorizontalAlignment.Center;
                        this.txtCachDung.CaseSensitive = false;
                        this.txtCachDung.MinTypedCharacters = 1;
                    }
                }
                catch
                {
                }
            }
            finally
            {
            }
        }

        private void AutoloadSaveAndPrintConfig()
        {
            try
            {
                this.AllowTextChanged = false;
                PropertyLib._MayInProperties.InDonthuocsaukhiluu = this.chkSaveAndPrint.Checked;
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
            catch
            {
            }
            finally
            {
                this.AllowTextChanged = true;
            }
        }

        private void LayDanhsachBSKham()
        {
            try
            {
                DataTable data = THU_VIEN_CHUNG.LaydanhsachBacsi(departmentID,noitru);
                txtBacsi.Init(data, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
                
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    txtBacsi.SetId(-1);
                }
                else
                {
                    txtBacsi.SetId( globalVariables.gv_intIDNhanvien);
                }
                DataTable Nguoitiem = THU_VIEN_CHUNG.Laydanhsachnhanvien("NGUOITIEM");
                txtNguoitiem.Init(Nguoitiem, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
                txtNguoitiem.SetCode(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEMCHUNG_MANHANVIEN_MACDINH", false));
                if (txtNguoitiem.MyCode == "-1" && globalVariables.gv_intIDNhanvien > 0)
                {
                    txtNguoitiem.SetId(globalVariables.gv_intIDNhanvien);
                }
                
            }
            catch (Exception)
            {
            }
        }
        void AutoWarning()
        {
            try
            {
                string Canhbaotamung = THU_VIEN_CHUNG.Canhbaotamung(objLuotkham);
                Utility.SetMsg(lblMsg, Canhbaotamung, true);

            }
            catch (Exception ex)
            {

            }
        }
        private void CauHinh()
        {
            if (PropertyLib._ThamKhamProperties != null)
            {
                this.cboA4.Text = (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4) ? "A4" : "A5";
            }
            this.cboPrintPreview.SelectedIndex = PropertyLib._MayInProperties.PreviewInDonthuoc ? 0 : 1;
            this.cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
            this.pnlPrint.Visible = PropertyLib._ThamKhamProperties.ChophepIndonthuoc;
            this.chkSaveAndPrint.Checked = PropertyLib._MayInProperties.InDonthuocsaukhiluu;
            this.cmdPrintPres.Visible = PropertyLib._ThamKhamProperties.ChophepIndonthuoc;
            this.chkHienthithuoctheonhom.Checked = PropertyLib._ThamKhamProperties.Hienthinhomthuoc;
            globalVariables.KHOKEDON = PropertyLib._ThamKhamProperties.IDKho;
            this.ModifyButton();
        }

        private void cboA4_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.CoGiayInDonthuoc = (this.cboA4.SelectedIndex == 0) ? Papersize.A4 : Papersize.A5;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        private void cboLaserPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveDefaultPrinter();
        }

        private void cboPrintPreview_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewInDonthuoc = this.cboPrintPreview.SelectedIndex == 0;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }
        TDmucKho objDKho = null;
        private void cboStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((this.blnHasLoaded && (this.cboStock.Items.Count > 0)) && ((this.cboStock.SelectedValue == null) || (this.cboStock.SelectedValue.ToString() != "-1")))
                {
                    globalVariables.KHOKEDON = Utility.Int32Dbnull(this.cboStock.SelectedValue, -1);
                    if (KIEU_THUOC_VT == "THUOC")
                        PropertyLib._ThamKhamProperties.IDKho = globalVariables.KHOKEDON;
                    else
                        PropertyLib._ThamKhamProperties.IDKhoVT = globalVariables.KHOKEDON;
                    PropertyLib.SaveProperty(PropertyLib._ThamKhamProperties);
                    int num = Utility.Int32Dbnull(this.cboStock.SelectedValue, -1);
                    if ((num > 0) && (this.blnHasLoaded && (this.cboStock.Items.Count > 0)))
                    {
                        this.m_dtDanhmucthuoc = this._KEDONTHUOC.LayThuoctrongkhokedon(num, KIEU_THUOC_VT, Utility.sDbnull(this.objLuotkham.MaDoituongKcb, "DV"), Utility.Int32Dbnull(this.objLuotkham.DungTuyen.Value, 0), noitru, globalVariables.MA_KHOA_THIEN);
                        this.ProcessData();
                        objDKho = ReadOnlyRecord<TDmucKho>.FetchByID(num);
                        this.rowFilter = "1=1";
                        this.txtdrug.AllowedSelectPrice = Utility.Byte2Bool(objDKho.ChophepChongia);
                        this.txtdrug.dtData = this.m_dtDanhmucthuoc;
                        this.txtdrug.ChangeDataSource();
                        this.txtdrug.Focus();
                        this.txtdrug.SelectAll();
                    }
                    else
                    {
                        objDKho = null;
                    }
                }
            }
            catch (Exception exception)
            {
                Utility.CatchException(exception);
            }
        }

        private void ChiDanThuoc()
        {
            string containGuide = this.GetContainGuide();
            this.txtChiDanDungThuoc.Text = containGuide;
        }

        private void chkAskbeforeDeletedrug_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._ThamKhamProperties.Hoitruockhixoathuoc = this.chkAskbeforeDeletedrug.Checked;
            PropertyLib.SaveProperty(PropertyLib._ThamKhamProperties);
        }

        private void chkHienthithuoctheonhom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                PropertyLib._ThamKhamProperties.Hienthinhomthuoc = this.chkHienthithuoctheonhom.Checked;
                PropertyLib.SaveProperty(PropertyLib._ThamKhamProperties);
                this.grdPresDetail.RootTable.Groups.Clear();
                if (this.chkHienthithuoctheonhom.Checked)
                {
                    GridEXColumn column = this.grdPresDetail.RootTable.Columns["ma_loaithuoc"];
                    GridEXGroup group = new GridEXGroup(column)
                    {
                        GroupPrefix = "Loại "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư")+" :"
                    };
                    this.grdPresDetail.RootTable.Groups.Add(group);
                }
            }
            catch
            {
            }
        }

        private void chkNgayTaiKham_CheckedChanged(object sender, EventArgs e)
        {
            this.dtNgayKhamLai.Enabled = this.chkNgayTaiKham.Checked;
        }

        private void chkSaveAndPrint_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                PropertyLib._MayInProperties.InDonthuocsaukhiluu = this.chkSaveAndPrint.Checked;
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi khi lưu trạng thái-->" + exception.Message);
            }
        }

        private void chkSaveAndPrint_CheckedChanged_1(object sender, EventArgs e)
        {
        }

        private void ClearControl()
        {
            txtdrug.Clear();
            txtDonViDung.Clear();
            txtVitritiem.Clear();
            txtLoaivacxin.Clear();
            txtLieuluong.Clear();
            txtMuithu.Clear();
            chkHennhaclai.Checked = false;
            txtMota.Clear();
            this.txtSoluong.Text = "1";
            this.txtChiDanDungThuoc.Clear();

            this.ModifyButton();
        }

        private void cmdAddDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.Int32Dbnull(txtBacsi.MyID, -1) <= 0)
                {
                    Utility.SetMsg(lblMsg, "Bạn cần chọn bác sĩ chỉ định trước khi thực hiện kê đơn thuốc", true);
                    txtBacsi.Focus();
                    return;
                }
                if (objPhieudieutriNoitru != null)
                {
                    if (dtpCreatedDate.Value.Date > objPhieudieutriNoitru.NgayDieutri.Value.Date)
                    {
                        Utility.ShowMsg("Ngày kê đơn phải <= " + objPhieudieutriNoitru.NgayDieutri.Value.ToString("dd/MM/yyyy"));
                        return;
                    }
                }
                this.AddPreDetail();
                this.Manual = true;
            }
            catch(Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
            
        }

        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            new frm_Properties(PropertyLib._ThamKhamProperties).ShowDialog();
            this.CauHinh();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.setMsg(this.lblMsg, "", false);
                if (this.grdPresDetail.GetCheckedRows().Length <= 0)
                {
                    this.setMsg(this.lblMsg, "Bạn phải chọn " +(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư") +" để xóa", true);
                    this.grdPresDetail.Focus();
                }
                else
                {
                    int num;
                    string s = "";
                    List<int> vals = this.GetIdChitiet(Utility.Int32Dbnull(this.grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1), Utility.DecimaltoDbnull(this.grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, -1), ref s);
                  
                    foreach (GridEXRow row in this.grdPresDetail.GetCheckedRows())
                    {
                        num = Utility.Int32Dbnull(row.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                        if ((num > 0) && (new SubSonic.Select().From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(num).And(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1).GetRecordCount() > 0))
                        {
                            setMsg(this.lblMsg, "Bản ghi đã thanh toán, bạn không thể xóa", true);
                            grdPresDetail.Focus();
                            return;
                        }
                    }
                    if (!PropertyLib._ThamKhamProperties.Hoitruockhixoathuoc || Utility.AcceptQuestion("Bạn Có muốn xóa các "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư") +" đang chọn hay không?", "thông báo xóa", true))
                    {
                        foreach (GridEXRow row in this.grdPresDetail.GetCheckedRows())
                        {
                            num = Utility.Int32Dbnull(row.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                            if (num > 0)
                            {
                                _KEDONTHUOC.XoaChitietDonthuoc(num);
                            }
                            row.Delete();
                            grdPresDetail.UpdateData();
                            DeletefromDatatable(vals);
                            m_dtDonthuocChitiet.AcceptChanges();
                        }
                        m_dtDonthuocChitiet.AcceptChanges();
                        m_blnCancel = false;
                        UpdateDataWhenChanged();
                    }
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
            finally
            {
                if (this.grdPresDetail.RowCount <= 0)
                {
                    this.em_Action = action.Insert;
                }
            }
        }

        private void cmdDonThuocDaKe_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

   

        private void cmdPrintPres_Click(object sender, EventArgs e)
        {
            this.PrintPres(Utility.Int32Dbnull(this.txtPres_ID.Text));
        }

        private void cmdSavePres_Click(object sender, EventArgs e)
        {
            try
            {
                this.cmdSavePres.Enabled = false;
                this.isSaved = true;
                if (Utility.Int32Dbnull(txtBacsi.MyID, -1) <= 0)
                {
                    Utility.SetMsg(lblMsg, "Bạn cần chọn bác sĩ chỉ định trước khi thực hiện kê đơn thuốc", true);
                    txtBacsi.Focus();
                    return;
                }
                if (objPhieudieutriNoitru != null)
                {
                    if (dtpCreatedDate.Value.Date > objPhieudieutriNoitru.NgayDieutri.Value.Date)
                    {
                        Utility.ShowMsg("Ngày kê đơn phải <= " + objPhieudieutriNoitru.NgayDieutri.Value.ToString("dd/MM/yyyy"));
                        dtpCreatedDate.Focus();
                        return;
                    }
                }
               
                if (this.grdPresDetail.RowCount <= 0)
                {
                    this.setMsg(this.lblMsg, "Hiện chưa Có bản ghi nào để thực hiện  lưu lại", true);
                    this.txtdrug.Focus();
                    this.txtdrug.SelectAll();
                }
                else
                {
                    List<KcbDonthuocChitiet> changedData = this.GetChangedData();
                    this.PerformAction((changedData == null) ? new List<KcbDonthuocChitiet>().ToArray() : changedData.ToArray());
                }
            }
            catch
            {
            }
            finally
            {
                this.cmdSavePres.Enabled = true;
                this.Manual = false;
                this.hasChanged = false;
                base.Close();
            }
        }

        private void cmdUpdateChiDan_Click(object sender, EventArgs e)
        {
            this.UpdateChiDanThem();
        }

        private void Create_ChandoanKetluan()
        {
            if (((Utility.DoTrim(this.txtTenBenhChinh.Text) != "") || (this.grd_ICD.GetDataRows().Length > 0)) || (Utility.DoTrim(this.txtChanDoan.Text) != ""))
            {
                if (this._KcbChandoanKetluan == null) this._KcbChandoanKetluan = new KcbChandoanKetluan();
                this._KcbChandoanKetluan.IdKham = this.id_kham;
                this._KcbChandoanKetluan.MaLuotkham = this.objLuotkham.MaLuotkham;
                this._KcbChandoanKetluan.IdBenhnhan = this.objLuotkham.IdBenhnhan;
                this._KcbChandoanKetluan.MabenhChinh = Utility.sDbnull(this.txtMaBenhChinh.Text, "");
                if (Utility.Int16Dbnull(txtBacsi.MyID, -1) > 0)
                {
                    this._KcbChandoanKetluan.IdBacsikham = Utility.Int16Dbnull(txtBacsi.MyID, -1);
                }
                else
                {
                    this._KcbChandoanKetluan.IdBacsikham = globalVariables.gv_intIDNhanvien;
                }
                this._KcbChandoanKetluan.MabenhPhu = Utility.sDbnull(this.GetDanhsachBenhphu().ToString(), "");
                if (this._KcbChandoanKetluan.IsNew)
                {
                    this._KcbChandoanKetluan.NgayTao = this.dtpCreatedDate.Value;
                    this._KcbChandoanKetluan.NguoiTao = globalVariables.UserName;

                    this._KcbChandoanKetluan.IpMaytao = globalVariables.gv_strIPAddress;
                    this._KcbChandoanKetluan.TenMaytao = globalVariables.gv_strComputerName;

                }
                else
                {
                    this._KcbChandoanKetluan.NgaySua = new DateTime?(this.dtpCreatedDate.Value);
                    this._KcbChandoanKetluan.NguoiSua = globalVariables.UserName;

                    this._KcbChandoanKetluan.IpMaysua = globalVariables.gv_strIPAddress;
                    this._KcbChandoanKetluan.TenMaysua = globalVariables.gv_strComputerName;
                }
                this._KcbChandoanKetluan.NgayChandoan = this.dtpCreatedDate.Value;
                this._KcbChandoanKetluan.IdBacsikham = globalVariables.gv_intIDNhanvien;
                this._KcbChandoanKetluan.Chandoan = Utility.ReplaceString(this.txtChanDoan.Text);
                this._KcbChandoanKetluan.Noitru =(byte)noitru;
            }
            else
            {
                this._KcbChandoanKetluan = null;
            }
        }

        private KcbDonthuocChitiet[] CreateArrayPresDetail()
        {
            this._temp = ActionResult.Success;
            int index = 0;
            KcbDonthuocChitiet[] chitietArray = new KcbDonthuocChitiet[this.m_dtDonthuocChitiet.DefaultView.Count];
            try
            {
                foreach (DataRowView view in this.m_dtDonthuocChitiet.DefaultView)
                {
                    long num2 = Utility.Int64Dbnull(view[KcbDonthuocChitiet.Columns.IdThuockho], -1);
                    decimal num3 = CommonLoadDuoc.SoLuongTonTrongKho(-1L, Utility.Int32Dbnull(view[KcbDonthuocChitiet.Columns.IdKho], -1), Utility.Int16Dbnull(view[KcbDonthuocChitiet.Columns.IdThuoc], -1), Utility.Int64Dbnull(view[KcbDonthuocChitiet.Columns.IdThuockho], -1), new int?(Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KIEMTRATHUOC_CHOXACNHAN", "1", false), 1)), Utility.ByteDbnull(objLuotkham.Noitru, 0));
                    if (this.em_Action == action.Update)
                    {
                        decimal soLuong = 0;
                        KcbDonthuocChitiet chitiet = new SubSonic.Select(new TableSchema.TableColumn[] { KcbDonthuocChitiet.SoLuongColumn }).From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.IdChitietdonthuocColumn).IsEqualTo(Utility.Int32Dbnull(view[KcbDonthuocChitiet.Columns.IdChitietdonthuoc], -1)).ExecuteSingle<KcbDonthuocChitiet>();
                        if (chitiet != null)
                        {
                            soLuong = chitiet.SoLuong;
                        }
                        num3 += soLuong;
                    }
                    if (Utility.Int32Dbnull(view[KcbDonthuocChitiet.Columns.SoLuong], 0) > num3)
                    {
                        Utility.ShowMsg(string.Format("Số lượng "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư")+" {0}({1}) vượt quá số lượng tồn trong kho{2}({3}) \nBạn cần chỉnh lại số lượng "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư")+"!", new object[] { Utility.sDbnull(view[DmucThuoc.Columns.TenThuoc], "").ToString(), Utility.Int32Dbnull(view[KcbDonthuocChitiet.Columns.SoLuong], 0).ToString(), Utility.sDbnull(view[TDmucKho.Columns.TenKho], "").ToString(), num3.ToString() }));
                        this._temp = ActionResult.NotEnoughDrugInStock;
                        return null;
                    }
                    chitietArray[index] = new KcbDonthuocChitiet();
                    chitietArray[index].IdDonthuoc = this.IdDonthuoc;
                    chitietArray[index].IdChitietdonthuoc = Utility.Int32Dbnull(view[KcbDonthuocChitiet.Columns.IdChitietdonthuoc], -1);
                    chitietArray[index].IdBenhnhan = new long?(this.objLuotkham.IdBenhnhan);
                    chitietArray[index].MaLuotkham = this.objLuotkham.MaLuotkham;
                    chitietArray[index].IdKho = new int?(Utility.Int32Dbnull(view[KcbDonthuocChitiet.Columns.IdKho], -1));
                    chitietArray[index].IdThuoc = Utility.Int16Dbnull(view[KcbDonthuocChitiet.Columns.IdThuoc], -1);
                    chitietArray[index].TrangthaiThanhtoan = Utility.ByteDbnull(view[KcbDonthuocChitiet.Columns.TrangthaiThanhtoan], 0);
                    chitietArray[index].SttIn = new short?(Utility.Int16Dbnull(view[KcbDonthuocChitiet.Columns.SttIn], 1));
                    chitietArray[index].TrangthaiHuy = 0;
                    chitietArray[index].IdThuockho = new long?(num2);
                    chitietArray[index].GiaNhap = new decimal?(Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.GiaNhap], -1));
                    chitietArray[index].GiaBan = new decimal?(Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.GiaBan], -1));
                    chitietArray[index].Vat = new decimal?(Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.Vat], -1));
                    chitietArray[index].SoLo = Utility.sDbnull(view[KcbDonthuocChitiet.Columns.SoLo], -1);
                    chitietArray[index].MaNhacungcap = Utility.sDbnull(view[KcbDonthuocChitiet.Columns.MaNhacungcap], -1);
                    chitietArray[index].NgayHethan = Utility.ConvertDate(view["sNgayhethan"].ToString()).Date;
                    chitietArray[index].SoluongHuy = 0;
                    chitietArray[index].TuTuc = new byte?(Utility.ByteDbnull(view[KcbDonthuocChitiet.Columns.TuTuc], 0));
                    chitietArray[index].SoLuong = Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.SoLuong], 0);
                    chitietArray[index].DonGia = Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.DonGia], 0);
                    chitietArray[index].PhuThu = new decimal?(Utility.Int16Dbnull(view[KcbDonthuocChitiet.Columns.PhuThu], 0));
                    chitietArray[index].MotaThem = Utility.sDbnull(view[KcbDonthuocChitiet.Columns.MotaThem], "");
                    chitietArray[index].ChidanThem = Utility.sDbnull(view[KcbDonthuocChitiet.Columns.ChidanThem], "");
                    chitietArray[index].CachDung = Utility.sDbnull(view[KcbDonthuocChitiet.Columns.CachDung], "");
                    chitietArray[index].DonviTinh = Utility.sDbnull(view[KcbDonthuocChitiet.Columns.DonviTinh], "");
                    chitietArray[index].SolanDung = Utility.sDbnull(view[KcbDonthuocChitiet.Columns.SolanDung], null);
                    chitietArray[index].SoluongDung = Utility.sDbnull(view[KcbDonthuocChitiet.Columns.SoluongDung], null);
                    chitietArray[index].SluongSua = 0;
                    chitietArray[index].SluongLinh = 0;
                    chitietArray[index].TrangThai = 0;
                    chitietArray[index].TrangthaiBhyt = 1;
                    chitietArray[index].IdThanhtoan = -1;
                    chitietArray[index].IdGoi = id_goidv;
                    chitietArray[index].TrongGoi = trong_goi;
                    chitietArray[index].NgayTao = new DateTime?(globalVariables.SysDate);
                    chitietArray[index].NguoiTao = globalVariables.UserName;
                    chitietArray[index].MaDoituongKcb = Utility.sDbnull(this.MaDoiTuong);
                    chitietArray[index].PtramBhyt = objLuotkham.TrangthaiNoitru <= 0 ? this.objLuotkham.PtramBhyt : objLuotkham.PtramBhytGoc;
                    chitietArray[index].PtramBhytGoc = this.objLuotkham.PtramBhytGoc;
                    if (Utility.Int32Dbnull(view[KcbDonthuocChitiet.Columns.TuTuc], 0) == 0)
                    {
                        decimal BHCT = 0m;
                        if (objLuotkham.DungTuyen == 1)
                        {
                            BHCT = Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.DonGia], 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                        }
                        else
                        {
                            if (objLuotkham.TrangthaiNoitru <= 0)
                                BHCT = Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.DonGia], 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                            else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                                BHCT = Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.DonGia], 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                        }
                        //decimal num5 = (Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.DonGia], 0) * Utility.DecimaltoDbnull(this.objLuotkham.PtramBhyt, 0)) / 100M;
                        decimal num6 = Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.DonGia], 0) - BHCT;
                        chitietArray[index].BhytChitra = new decimal?(BHCT);
                        chitietArray[index].BnhanChitra = new decimal?(num6);
                        
                    }
                    else//BHYT Tự túc
                    {
                        chitietArray[index].BhytChitra = 0;
                        chitietArray[index].BnhanChitra = new decimal?(Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.DonGia], 0));
                        chitietArray[index].PtramBhyt = 0;
                    }
                    if (this.objLuotkham.MaDoituongKcb == "BHYT")
                    {
                    }
                    index++;
                }
            }
            catch (Exception)
            {
            }
            return chitietArray;
        }

        private KcbDonthuoc CreateNewPres()
        {
            KcbDonthuoc donthuoc = new KcbDonthuoc
            {
                MaLuotkham = Utility.sDbnull(this.objLuotkham.MaLuotkham, ""),
                IdBenhnhan = new long?((long)Utility.Int32Dbnull(this.objLuotkham.IdBenhnhan, -1)),
                MaKhoaThuchien = globalVariables.MA_KHOA_THIEN,
                LoidanBacsi = Utility.sDbnull(this.txtLoiDanBS.Text),
                TaiKham = Utility.sDbnull(this.txtKhamLai.Text)
            };
            if (this.chkNgayTaiKham.Checked)
            {
                donthuoc.NgayTaikham = new DateTime?(this.dtNgayKhamLai.Value);
            }
            else
            {
                donthuoc.NgayTaikham = null;
            }
            donthuoc.NgayKedon = this.dtpCreatedDate.Value;
            donthuoc.MaDoituongKcb = this.MaDoiTuong;
            donthuoc.IdLichsuDoituongKcb = objLuotkham.IdLichsuDoituongKcb;
            donthuoc.MatheBhyt = objLuotkham.MatheBhyt;

            donthuoc.TenDonthuoc = THU_VIEN_CHUNG.TaoTenDonthuoc(this.objLuotkham.MaLuotkham, Utility.Int32Dbnull(this.objLuotkham.IdBenhnhan, -1));
            this.objRegExam = new SubSonic.Select().From(KcbDangkyKcb.Schema).Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(this.id_kham).ExecuteSingle<KcbDangkyKcb>();
            if (this.objRegExam != null)
            {
                donthuoc.IdKhoadieutri = Utility.Int16Dbnull(this.objRegExam.IdKhoakcb);
                donthuoc.IdPhongkham = new short?(Utility.Int16Dbnull(this.objRegExam.IdPhongkham));
                donthuoc.IdGiuongNoitru = -1;
            }
            else
            {
                donthuoc.IdKhoadieutri = new short?(globalVariables.idKhoatheoMay);
                donthuoc.IdPhongkham = new short?(globalVariables.idKhoatheoMay);
                donthuoc.IdGiuongNoitru = -1;
            }
           
            donthuoc.IdGoi = id_goidv;
            donthuoc.TrongGoi = trong_goi;
            if (objPhieudieutriNoitru != null)
            {
                donthuoc.IdPhieudieutri = objPhieudieutriNoitru.IdPhieudieutri;
                donthuoc.IdKhoadieutri = objPhieudieutriNoitru.IdKhoanoitru;
                donthuoc.IdPhongkham = objPhieudieutriNoitru.IdKhoanoitru;
                donthuoc.IdBuongGiuong = objPhieudieutriNoitru.IdBuongGiuong;
                donthuoc.IdBuongNoitru = objLuotkham.IdBuong;
                donthuoc.IdGiuongNoitru = objLuotkham.IdGiuong;

            }
            donthuoc.TrangthaiThanhtoan = 0;
            donthuoc.IdBacsiChidinh =Utility.Int16Dbnull(txtBacsi.MyID,globalVariables.gv_intIDNhanvien);
            donthuoc.TrangThai = 0;
            donthuoc.NguoiTao = globalVariables.UserName;
            donthuoc.NgayTao = globalVariables.SysDate;
            donthuoc.IdDonthuocthaythe = -1;
            donthuoc.IdKham = new long?((long)this.id_kham);
            donthuoc.NgayTao = globalVariables.SysDate;
            donthuoc.NguoiTao = globalVariables.UserName;
            donthuoc.Noitru = (byte)noitru;

            donthuoc.KieuDonthuoc = 3;
            donthuoc.KieuThuocvattu = KIEU_THUOC_VT;// (this.m_intKieudonthuoc == 1) ? "VT" : "THUOC";
            if (this.em_Action == action.Update)
            {
                donthuoc.IdDonthuoc = Utility.Int32Dbnull(this.txtPres_ID.Text, -1);
                donthuoc.NguoiSua = globalVariables.UserName;
                donthuoc.NgaySua = new DateTime?(globalVariables.SysDate);

                donthuoc.IpMaysua = globalVariables.gv_strIPAddress;
                donthuoc.TenMaysua = globalVariables.gv_strComputerName;
                donthuoc.IsNew = false;
                donthuoc.MarkOld();
                
            }
            else
            {
                donthuoc.IpMaytao = globalVariables.gv_strIPAddress;
                donthuoc.TenMaytao = globalVariables.gv_strComputerName;
            }
           
            return donthuoc;
        }

        private void CreateViewTable()
        {
            try
            {
                this.m_dtDonthuocChitiet_View = this.m_dtDonthuocChitiet.Clone();
                foreach (DataRow row in this.m_dtDonthuocChitiet.Rows)
                {
                    row["CHON"] = 0;
                    DataRow[] rowArray = this.m_dtDonthuocChitiet_View.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + Utility.sDbnull(row[KcbDonthuocChitiet.Columns.IdThuoc], "-1") + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + Utility.sDbnull(row[KcbDonthuocChitiet.Columns.DonGia], "-1"));
                    if (rowArray.Length <= 0)
                    {
                        row["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]);
                        row["TT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu]));
                        row["TT_BHYT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BhytChitra]);
                        row["TT_BN"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0));
                        row["TT_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0);
                        row["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                        this.m_dtDonthuocChitiet_View.ImportRow(row);
                    }
                    else
                    {
                        rowArray[0][KcbDonthuocChitiet.Columns.SoLuong] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong], 0) + Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong], 0);
                        rowArray[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]);
                        rowArray[0]["TT"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu]));
                        rowArray[0]["TT_BHYT"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                        rowArray[0]["TT_BN"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                        rowArray[0]["TT_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                        rowArray[0]["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                        rowArray[0][KcbDonthuocChitiet.Columns.SttIn] = Math.Min(Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SttIn], 0), Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                        this.m_dtDonthuocChitiet_View.AcceptChanges();
                    }
                }
                this.m_dtDonthuocChitiet_View.AcceptChanges();
                Utility.SetDataSourceForDataGridEx_Basic(this.grdPresDetail, this.m_dtDonthuocChitiet_View, false, true, "1=1", KcbDonthuocChitiet.Columns.SttIn);
            }
            catch
            {
            }
        }

        private void DeletefromDatatable(List<int> lstIdChitietDonthuoc)
        {
            try
            {
                Func<DataRow, bool> predicate = q => lstIdChitietDonthuoc.Contains(Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]));
                DataRow[] rowArray = this.m_dtDonthuocChitiet.Select("1=1").AsEnumerable<DataRow>().Where<DataRow>(predicate).ToArray<DataRow>();
                for (int i = 0; i <= (rowArray.Length - 1); i++)
                {
                    this.m_dtDonthuocChitiet.Rows.Remove(rowArray[i]);
                }
                this.m_dtDonthuocChitiet.AcceptChanges();
            }
            catch(Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }

        private void DeletefromDatatable(List<int> lstDeleteId, int lastdetailid, int soluong)
        {
            Func<DataRow, bool> predicate = null;
            Func<DataRow, bool> func2 = null;
            try
            {
                int num;
                if (predicate == null)
                {
                    predicate = q => Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]) == lastdetailid;
                }
                DataRow[] rowArray = this.m_dtDonthuocChitiet.Select("1=1").AsEnumerable<DataRow>().Where<DataRow>(predicate).ToArray<DataRow>();
                for (num = 0; num <= (rowArray.Length - 1); num++)
                {
                    if (soluong <= 0)
                    {
                        this.m_dtDonthuocChitiet.Rows.Remove(rowArray[num]);
                    }
                    else
                    {
                        rowArray[num][KcbDonthuocChitiet.Columns.SoLuong] = soluong;
                    }
                }
                if (func2 == null)
                {
                    func2 = q => lstDeleteId.Contains(Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc], 0));
                }
                rowArray = this.m_dtDonthuocChitiet.Select("1=1").AsEnumerable<DataRow>().Where<DataRow>(func2).ToArray<DataRow>();
                for (num = 0; num <= (rowArray.Length - 1); num++)
                {
                    this.m_dtDonthuocChitiet.Rows.Remove(rowArray[num]);
                }
                this.m_dtDonthuocChitiet.AcceptChanges();
            }
            catch
            {
            }
        }

      

        private void DSACH_ICD(EditBox tEditBox, string LOAITIMKIEM, int CP)
        {
            try
            {
                this.Selected = false;
                string filterExpression = "";
                if (LOAITIMKIEM.ToUpper() == DmucChung.Columns.Ten)
                {
                    filterExpression = " Disease_Name like '%" + tEditBox.Text + "%' OR FirstChar LIKE '%" + tEditBox.Text + "%'";
                }
                else if (LOAITIMKIEM == DmucChung.Columns.Ma)
                {
                    filterExpression = DmucBenh.Columns.MaBenh + " LIKE '%" + tEditBox.Text + "%'";
                }
                DataRow[] source = this.dt_ICD.Select(filterExpression);
                if (source.Length == 1)
                {
                    if (CP == 0)
                    {
                        this.txtMaBenhChinh.Text = "";
                        this.txtMaBenhChinh.Text = Utility.sDbnull(source[0][DmucBenh.Columns.MaBenh], "");
                        this.hasMorethanOne = false;
                        this.txtMaBenhChinh_TextChanged(this.txtMaBenhChinh, new EventArgs());
                        this.txtMaBenhChinh.Focus();
                    }
                    else if (CP == 1)
                    {
                        this.txtMaBenhphu.Text = Utility.sDbnull(source[0][DmucBenh.Columns.MaBenh], "");
                        this.hasMorethanOne = false;
                        this.txtMaBenhphu_TextChanged(this.txtMaBenhphu, new EventArgs());
                        this.txtMaBenhphu_KeyDown(this.txtMaBenhphu, new KeyEventArgs(Keys.Enter));
                        this.Selected = false;
                    }
                }
                else if (source.Length > 1)
                {
                    frm_DanhSach_ICD h_icd = new frm_DanhSach_ICD(CP)
                    {
                        dt_ICD = source.CopyToDataTable<DataRow>()
                    };
                    h_icd.ShowDialog();
                    if (!h_icd.has_Cancel)
                    {
                        List<GridEXRow> lstSelectedRows = h_icd.lstSelectedRows;
                        if (CP == 0)
                        {
                            this.isLike = false;
                            this.txtMaBenhChinh.Text = "";
                            this.txtMaBenhChinh.Text = Utility.sDbnull(lstSelectedRows[0].Cells[DmucBenh.Columns.MaBenh].Value, "");
                            this.hasMorethanOne = false;
                            this.txtMaBenhChinh_TextChanged(this.txtMaBenhChinh, new EventArgs());
                            this.txtMaBenhChinh_KeyDown(this.txtMaBenhChinh, new KeyEventArgs(Keys.Enter));
                            this.Selected = false;
                        }
                        else if (CP == 1)
                        {
                            if (lstSelectedRows.Count == 1)
                            {
                                this.isLike = false;
                                this.txtMaBenhphu.Text = "";
                                this.txtMaBenhphu.Text = Utility.sDbnull(lstSelectedRows[0].Cells[DmucBenh.Columns.MaBenh].Value, "");
                                this.hasMorethanOne = false;
                                this.txtMaBenhphu_TextChanged(this.txtMaBenhphu, new EventArgs());
                                this.txtMaBenhphu_KeyDown(this.txtMaBenhphu, new KeyEventArgs(Keys.Enter));
                                this.Selected = false;
                            }
                            else
                            {
                                foreach (GridEXRow row in lstSelectedRows)
                                {
                                    this.isLike = false;
                                    this.txtMaBenhphu.Text = "";
                                    this.txtMaBenhphu.Text = Utility.sDbnull(row.Cells[DmucBenh.Columns.MaBenh].Value, "");
                                    this.hasMorethanOne = false;
                                    this.txtMaBenhphu_TextChanged(this.txtMaBenhphu, new EventArgs());
                                    this.txtMaBenhphu_KeyDown(this.txtMaBenhphu, new KeyEventArgs(Keys.Enter));
                                    this.Selected = false;
                                }
                                this.hasMorethanOne = true;
                            }
                        }
                        tEditBox.Focus();
                    }
                    else
                    {
                        this.hasMorethanOne = true;
                        tEditBox.Focus();
                    }
                }
                else
                {
                    this.hasMorethanOne = true;
                    tEditBox.SelectAll();
                }
            }
            catch
            {
            }
            finally
            {
                this.isLike = true;
            }
        }

        private void frm_KCB_KeVacxin_Tiemchung_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.isSaved)
            {
                List<KcbDonthuocChitiet> changedData = this.GetChangedData();
                if (((changedData != null) && (changedData.Count > 0)) && Utility.AcceptQuestion("Bạn đã thay đổi đơn "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư") +" nhưng chưa lưu lại. Bạn Có muốn lưu đơn "+ (KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư") +" trước khi thoát hay không? Nhấn Yes để lưu đơn "+ (KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư")+". Nhấn No để không lưu đơn "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư"), "Cảnh báo", true))
                {
                    this.cmdSavePres_Click(this.cmdSavePres, new EventArgs());
                }
            }
        }

        private void frm_KCB_KeVacxin_Tiemchung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F11)
            {
                Utility.ShowMsg(base.ActiveControl.Name);
            }
            if (e.KeyCode == Keys.F4 || (e.Control && e.KeyCode==Keys.P))
            {
                this.cmdPrintPres.PerformClick();
            }
            if ((e.KeyCode == Keys.A) && e.Control)
            {
                this.cmdAddDetail.PerformClick();
            }
            if (e.KeyCode == Keys.S && e.Control)
            {
                this.cmdSavePres.PerformClick();
            }
            if (e.KeyCode == Keys.F3)
            {
                this.txtdrug.Focus();
                this.txtdrug.SelectAll();
            }
            if ((e.Shift || e.Alt) && (e.KeyCode == Keys.S))
            {
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (this.uiTabPage1.ActiveControl != null && this.uiTabPage1.ActiveControl.Name == txtMaBenhphu.Name)
                        return;
                    else
                        SendKeys.Send("{TAB}");
                }
                if (e.KeyCode == Keys.F5)
                {
                    this.cboStock_SelectedIndexChanged(this.cboStock, new EventArgs());
                }
                if (e.KeyCode == Keys.Escape)
                {
                    this.cmdExit_Click(this.cmdExit, new EventArgs());
                }
            }
        }
        DataTable m_dtqheCamchidinhChungphieu = new DataTable();
        DataTable m_dtDulieuTiemchungBN = new DataTable();
        private void frm_KCB_KeVacxin_Tiemchung_Load(object sender, EventArgs e)
        {
            try
            {
                chkAdditional.Checked = forced2Add;
                chkAdditional.Visible = !forced2Add && objLuotkham.TrangthaiNoitru>0;


                txtptramdauthe.Visible = objLuotkham.IdLoaidoituongKcb == 0;
                lblphantramdauthe.Visible = objLuotkham.IdLoaidoituongKcb == 0;
                pnlChandoanNgoaitru.Visible = objLuotkham.TrangthaiNoitru <= 0;
                m_dtDulieuTiemchungBN = KCB_KEDONTHUOC.KcbThamkhamDulieuTiemchungTheoBenhnhan(objLuotkham.IdBenhnhan);
                m_dtqheCamchidinhChungphieu = new Select().From(QheCamchidinhChungphieu.Schema).Where(QheCamchidinhChungphieu.Columns.Loai).IsEqualTo(1).ExecuteDataSet().Tables[0];
                BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
                m_intKieudonthuoc = KIEU_THUOC_VT == "THUOC" ? 2 : 1;
                txtCachDung.LOAI_DANHMUC = KIEU_THUOC_VT == "THUOC" ? "CDDT" :"CHIDAN_KEVATTU";
                this.AutoloadSaveAndPrintConfig();
                this.LayDanhsachBSKham();
                this.LayDanhsachKhoVacxin();
                this.LoadLaserPrinters();
                this.txtCachDung.Init();
                txtVitritiem.Init();
                txtLydotiem.Init();
                LayCongtiem();
                this.GetData();
                this.GetDataPresDetail();
                this.txtChanDoan.Init();
                this.LoadBenh();
                mnuThuoctutuc.Visible = THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb);
                chkTutuc.Visible = THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb);
                if (!chkTutuc.Visible) chkTutuc.Checked = false;
                this.MaDoiTuong = objLuotkham.MaDoituongKcb;
                if (this._KcbChandoanKetluan == null)
                {
                    this._KcbChandoanKetluan = new KcbChandoanKetluan();
                    this._KcbChandoanKetluan.IsNew = true;
                }
                else
                {
                    this._KcbChandoanKetluan.IsNew = false;
                    this._KcbChandoanKetluan.MarkOld();
                }
                bool gridView = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KEDONTHUOC_SUDUNGLUOI", "0", true), 0) == 1;
                if (!gridView)
                {
                    gridView = PropertyLib._AppProperties.GridView;
                }
                this.txtdrug.GridView = gridView;
                this.isLoaded = true;
                this.AllowTextChanged = true;
                this.blnHasLoaded = true;
                if (KIEU_THUOC_VT == "THUOC")
                    globalVariables.KHOKEDON = PropertyLib._ThamKhamProperties.IDKho;
                else
                    globalVariables.KHOKEDON = PropertyLib._ThamKhamProperties.IDKhoVT;

                if (this.dtStockList.Select(TDmucKho.Columns.IdKho + "= " + globalVariables.KHOKEDON).Length > 0)
                {
                    this.cboStock.SelectedIndex = Utility.GetSelectedIndex(this.cboStock, globalVariables.KHOKEDON.ToString());
                    this.cboStock_SelectedIndexChanged(this.cboStock, new EventArgs());
                }
                else
                {
                    this.cboStock.SelectedIndex = -1;
                }
                if (this.cboStock.Items.Count == 0)
                {
                    Utility.ShowMsg(string.Format("Bệnh nhân {0} thuộc đối tượng {1} chưa Có kho " + THU_VIEN_CHUNG.laytenthuoc_vattu(KIEU_THUOC_VT) + " để kê đơn", this.txtPatientName.Text.Trim(), this.txtObjectName.Text.Trim()));
                    base.Close();
                }
                this.grdPresDetail.RootTable.Groups.Clear();
                if (this.chkHienthithuoctheonhom.Checked)
                {
                    GridEXColumn column = this.grdPresDetail.RootTable.Columns["ma_loaithuoc"];
                    GridEXGroup group = new GridEXGroup(column)
                    {
                        GroupPrefix = "Loại " + KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư" + " : "
                    };
                    this.grdPresDetail.RootTable.Groups.Add(group);
                }
                this.txtdrug.Focus();
                this.txtdrug.Select();
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
            }
        }

        private void GetChanDoan(string ICD_chinh, string IDC_Phu, ref string ICD_Name, ref string ICD_Code)
        {
            try
            {
                List<string> paramValue = ICD_chinh.Split(new char[] { ',' }).ToList<string>();
                DmucBenhCollection benhs = new DmucBenhController().FetchByQuery(DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, paramValue));
                foreach (DmucBenh benh in benhs)
                {
                    ICD_Name = ICD_Name + benh.TenBenh + ";";
                    ICD_Code = ICD_Code + benh.MaBenh + ";";
                }
                paramValue = IDC_Phu.Split(new char[] { ',' }).ToList<string>();
                benhs = new DmucBenhController().FetchByQuery(DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, paramValue));
                foreach (DmucBenh benh in benhs)
                {
                    ICD_Name = ICD_Name + benh.TenBenh + ";";
                    ICD_Code = ICD_Code + benh.MaBenh + ";";
                }
                if (ICD_Name.Trim() != "")
                {
                    ICD_Name = ICD_Name.Substring(0, ICD_Name.Length - 1);
                }
                if (ICD_Code.Trim() != "")
                {
                    ICD_Code = ICD_Code.Substring(0, ICD_Code.Length - 1);
                }
            }
            catch
            {
            }
        }

        private List<KcbDonthuocChitiet> GetChangedData()
        {
            List<KcbDonthuocChitiet> list = new List<KcbDonthuocChitiet>();
            this._temp = ActionResult.Success;
            KcbDonthuocChitiet[] chitietArray = new KcbDonthuocChitiet[this.m_dtDonthuocChitiet.DefaultView.Count];
            try
            {
                foreach (DataRow row in this.m_dtDonthuocChitiet.Rows)
                {
                    long key = Utility.Int64Dbnull(row[KcbDonthuocChitiet.Columns.IdThuockho], -1);
                    if (this.lstChangeData.ContainsKey(key))
                    {
                        string str = this.lstChangeData[key].ToString();
                        if (this.isChanged(str))
                        {
                            decimal num3 = CommonLoadDuoc.SoLuongTonTrongKho(-1L, Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.IdKho], -1), Utility.Int16Dbnull(row[KcbDonthuocChitiet.Columns.IdThuoc], -1), key, new int?(Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KIEMTRATHUOC_CHOXACNHAN", "1", false), 1)), Utility.ByteDbnull(objLuotkham.Noitru, 0));
                            if (this.em_Action == action.Update)
                            {
                                decimal soLuong = 0;
                                KcbDonthuocChitiet chitiet = new SubSonic.Select(new TableSchema.TableColumn[] { KcbDonthuocChitiet.SoLuongColumn }).From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.IdChitietdonthuocColumn).IsEqualTo(Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.IdChitietdonthuoc], -1)).ExecuteSingle<KcbDonthuocChitiet>();
                                if (chitiet != null)
                                {
                                    soLuong = chitiet.SoLuong;
                                }
                                num3 += soLuong;
                            }
                            if (Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong], 0) > num3)
                            {
                                Utility.ShowMsg(string.Format("Số lượng "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư")+" {0}({1}) vượt quá số lượng tồn trong kho{2}({3}) \nBạn cần chỉnh lại số lượng "+ (KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư")+"!", new object[] { Utility.sDbnull(row[DmucThuoc.Columns.TenThuoc], "").ToString(), Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong], 0).ToString(), Utility.sDbnull(row[TDmucKho.Columns.TenKho], "").ToString(), num3.ToString() }));
                                this._temp = ActionResult.NotEnoughDrugInStock;
                                return null;
                            }
                            this.hasChanged = true;
                            list.Add(this.getNewItem(row));
                        }
                    }
                }
                return list;
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi khi lấy dữ liệu cập nhật đơn "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư")+":\n" + exception.Message);
                return null;
            }
        }

        private string GetContainGuide()
        {
            try
            {
                string yourString = "";
                yourString = yourString + this.txtCachDung.Text + " ";
                if (!string.IsNullOrEmpty(this.txtSoLuongDung.Text))
                {
                    string str3 = yourString;
                    yourString = str3 + "Mỗi ngày dùng " + this.txtSoLuongDung.Text.Trim() + " " + this.txtDonViDung.Text;
                }
                if (!string.IsNullOrEmpty(this.txtSolan.Text))
                {
                    yourString = yourString + " chia làm  " + this.txtSolan.Text + " lần";
                }
                if (!string.IsNullOrEmpty(this.txtChiDanThem.Text))
                {
                    yourString = yourString + ". " + this.txtChiDanThem.Text;
                }
                return Utility.ReplaceString(yourString);
            }
            catch (Exception exception)
            {
                return exception.ToString();
            }
        }

        private string GetDanhsachBenhphu()
        {
            StringBuilder builder = new StringBuilder("");
            try
            {
                int num = 0;
                if (this.dt_ICD.Rows.Count > 0)
                {
                    foreach (DataRow row in this.dt_ICD_PHU.Rows)
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

        private void GetData()
        {
            if (this.objLuotkham != null)
            {
                this.txtSoBHYT.Text = Utility.sDbnull(this.objLuotkham.MatheBhyt);
                this.txtPtramBHYT.Text = (objLuotkham.TrangthaiNoitru <= 0 ? Utility.sDbnull(this.objLuotkham.PtramBhyt, "0") : Utility.sDbnull(this.objLuotkham.PtramBhytGoc, "0")) + " %";
                this.txtptramdauthe.Text =  Utility.sDbnull(this.objLuotkham.PtramBhytGoc, "0")+ " %";
                this.txtAddress.Text = Utility.sDbnull(this.objLuotkham.DiaChi);
                this.txtdiachiBhyt.Text = Utility.sDbnull(this.objLuotkham.DiachiBhyt);
                DmucDoituongkcb doituongkcb = ReadOnlyRecord<DmucDoituongkcb>.FetchByID((int)this.objLuotkham.IdDoituongKcb);
                if (doituongkcb != null)
                {
                    this.Giathuoc_quanhe = Utility.ByteDbnull(doituongkcb.GiathuocQuanhe, 0) == 1;
                    this.txtObjectName.Text = Utility.sDbnull(doituongkcb.TenDoituongKcb);
                    this.chkTutuc.Visible = THU_VIEN_CHUNG.IsBaoHiem(doituongkcb.IdLoaidoituongKcb);
                    this.chkTutuc.Checked = false;
                    this.mnuThuoctutuc.Visible = THU_VIEN_CHUNG.IsBaoHiem(doituongkcb.IdLoaidoituongKcb);
                }
                KcbDanhsachBenhnhan benhnhan = ReadOnlyRecord<KcbDanhsachBenhnhan>.FetchByID(this.objLuotkham.IdBenhnhan);
                if (benhnhan != null)
                {
                    this.txtSoDT.Text = Utility.sDbnull(benhnhan.DienThoai);
                    this.txtPatientName.Text = Utility.sDbnull(benhnhan.TenBenhnhan);
                    this.txtYearBirth.Text = Utility.sDbnull(benhnhan.NamSinh);
                    this.txtSex.Text = Utility.sDbnull(benhnhan.GioiTinh);
                }
            }
        }

        private void GetDataPresDetail()
        {
            KcbDonthuoc donthuoc = ReadOnlyRecord<KcbDonthuoc>.FetchByID(Utility.Int32Dbnull(this.txtPres_ID.Text));
            if (donthuoc != null)
            {
                this.IdDonthuoc = Utility.Int32Dbnull(donthuoc.IdDonthuoc);
                
                this.txtLoiDanBS.Text = Utility.sDbnull(donthuoc.LoidanBacsi);
                this.txtKhamLai.Text = Utility.sDbnull(donthuoc.TaiKham);
                txtBacsi.SetId(Utility.sDbnull(donthuoc.IdBacsiChidinh, ""));
                dtpCreatedDate.Value = donthuoc.NgayKedon;
                if (donthuoc.NgayTaikham != null)
                {
                    this.chkNgayTaiKham.Checked = true;
                    this.dtNgayKhamLai.Value = donthuoc.NgayTaikham.Value;
                }
            }
            else
            {
                if (objPhieudieutriNoitru != null)
                    dtpCreatedDate.Value = objPhieudieutriNoitru.NgayDieutri.Value;
                else
                    dtpCreatedDate.Value = globalVariables.SysDate;
            }
            this.m_dtDonthuocChitiet = this._KEDONTHUOC.Laythongtinchitietdonthuoc(this.IdDonthuoc);
            this.CreateViewTable();
            if (!this.m_dtDonthuocChitiet.Columns.Contains("CHON"))
            {
                this.m_dtDonthuocChitiet.Columns.Add("CHON", typeof(int));
            }
            this.UpdateDataWhenChanged();
        }

        private List<int> GetIdChitiet(int id_thuoc, decimal don_gia, ref string s)
        {
            DataRow[] source = this.m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc.ToString() + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + don_gia.ToString());
            if (source.Length > 0)
            {
                IEnumerable<string> enumerable = (from q in source.AsEnumerable<DataRow>() select Utility.sDbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct<string>();
                s = string.Join(",", enumerable.ToArray<string>());
                return (from q in source.AsEnumerable<DataRow>() select Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct<int>().ToList<int>();
            }
            return new List<int>();
        }

        private void getInfor(int id_thuoc, ref string tenthuoc, ref string ten_donvitinh, ref string hoatchat, ref string DonviTinh, ref string ChidanThem, ref string MotaThem, ref string CachDung, ref string SoluongDung, ref string SolanDung)
        {
            try
            {
                DataRow[] rowArray = this.m_dtDonthuocChitiet.Select("id_thuoc=" + id_thuoc.ToString());
                if (rowArray.Length > 0)
                {
                    tenthuoc = Utility.sDbnull(rowArray[0][DmucThuoc.Columns.TenThuoc], "");
                    ten_donvitinh = Utility.sDbnull(rowArray[0]["ten_donvitinh"], "");
                    DonviTinh = Utility.sDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonviTinh], "");
                    ChidanThem = Utility.sDbnull(rowArray[0][KcbDonthuocChitiet.Columns.ChidanThem], "");
                    MotaThem = Utility.sDbnull(rowArray[0]["mota_them_chitiet"], "");
                    hoatchat = Utility.sDbnull(rowArray[0][DmucThuoc.Columns.MaHoatchat], "");
                    CachDung = Utility.sDbnull(rowArray[0][KcbDonthuocChitiet.Columns.CachDung], "");
                    SoluongDung = Utility.sDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoluongDung], "");
                    SolanDung = Utility.sDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SolanDung], "");
                }
            }
            catch
            {
            }
        }

        private int GetMaxSTT(DataTable dataTable)
        {
            try
            {
                return (Utility.Int32Dbnull(dataTable.AsEnumerable().Max<DataRow, short>(c => c.Field<short>(KcbDonthuocChitiet.Columns.SttIn)), 0) + 1);
            }
            catch (Exception)
            {
                return 1;
            }
        }

        private KcbDonthuocChitiet getNewItem(DataRow drv)
        {
            KcbDonthuocChitiet chitiet
            = new KcbDonthuocChitiet
            {
                IdDonthuoc = this.IdDonthuoc,
                IdChitietdonthuoc = Utility.Int32Dbnull(drv[KcbDonthuocChitiet.Columns.IdChitietdonthuoc], -1),
                IdKham = new long?((long)this.id_kham),
                IdKho = new int?(Utility.Int32Dbnull(drv[KcbDonthuocChitiet.Columns.IdKho], -1)),
                IdThuoc = Utility.Int16Dbnull(drv[KcbDonthuocChitiet.Columns.IdThuoc], -1),
                TrangthaiThanhtoan = Utility.ByteDbnull(drv[KcbDonthuocChitiet.Columns.TrangthaiThanhtoan], 0),
                SttIn = new short?(Utility.Int16Dbnull(drv[KcbDonthuocChitiet.Columns.SttIn], 1)),
                TrangthaiHuy = 0,
                IdThuockho = new long?((long)Utility.Int32Dbnull(drv[KcbDonthuocChitiet.Columns.IdThuockho], -1)),
                GiaNhap = new decimal?(Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.GiaNhap], -1)),
                GiaBan = new decimal?(Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.GiaBan], -1)),
                GiaBhyt = new decimal?(Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.GiaBhyt], -1)),
                Vat = new decimal?(Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.Vat], -1)),
                SoLo = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.SoLo], -1),
                SoDky = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.SoDky], ""),
                SoQdinhthau = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.SoQdinhthau], ""),
                MaNhacungcap = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.MaNhacungcap], -1),
                NgayHethan = Utility.ConvertDate(drv["sngay_hethan"].ToString()).Date,
                NgayNhap = Utility.ConvertDate(drv["sngay_nhap"].ToString()).Date,
                SoluongHuy = 0,
                SluongLinh = 0,
                SluongSua = 0,
                IdThanhtoan = -1,
                TrangthaiTonghop=0,
                MadoituongGia = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.MadoituongGia], objLuotkham.MadoituongGia),
                TrangthaiChuyen=0,
                IdGoi=-1,
                TrongGoi=0,
                NguonThanhtoan=(byte)(noitru==0?0:1),
                TuTuc = new byte?(Utility.ByteDbnull(drv[KcbDonthuocChitiet.Columns.TuTuc], 0)),
                SoLuong = Utility.Int32Dbnull(drv[KcbDonthuocChitiet.Columns.SoLuong], 0),
                DonGia = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.DonGia], 0),
                PhuThu = new decimal?(Utility.Int16Dbnull(drv[KcbDonthuocChitiet.Columns.PhuThu], 0)),
                PhuthuDungtuyen = new decimal?(Utility.Int16Dbnull(drv[KcbDonthuocChitiet.Columns.PhuthuDungtuyen], 0)),
                PhuthuTraituyen = new decimal?(Utility.Int16Dbnull(drv[KcbDonthuocChitiet.Columns.PhuthuTraituyen], 0)),
                MotaThem = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.MotaThem], ""),
                TrangthaiBhyt = Utility.ByteDbnull(drv[KcbDonthuocChitiet.Columns.TuTuc], 0),
                TrangThai = 0,
                ChidanThem = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.ChidanThem], ""),
                CachDung = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.CachDung], ""),
                DonviTinh = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.DonviTinh], ""),
                SolanDung = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.SolanDung], null),
                SoluongDung = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.SoluongDung], null),
                NgayTao = new DateTime?(globalVariables.SysDate),
                NguoiTao = globalVariables.UserName,
                BhytChitra = new decimal?(Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.BhytChitra], 0)),
                BnhanChitra = new decimal?(Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.BnhanChitra], 0)),
                PtramBhyt = new decimal?(Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.PtramBhyt], 0)),
                PtramBhytGoc = objLuotkham.PtramBhytGoc,
                MaDoituongKcb = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.MaDoituongKcb], "DV"),
                KieuBiendong = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.KieuBiendong], "EXP"),
                DaDung=0,
                VitriTiem = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.VitriTiem], ""),
                NguoiTiem = Utility.Int16Dbnull(drv[KcbDonthuocChitiet.Columns.NguoiTiem], -1),
                LydoTiemchung = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.LydoTiemchung], ""),
                MuiThu = Utility.ByteDbnull(drv[KcbDonthuocChitiet.Columns.MuiThu], (byte)1),
                 IpMaytao = globalVariables.gv_strIPAddress,
                TenMaytao = globalVariables.gv_strComputerName,
                IpMaysua = globalVariables.gv_strIPAddress,
                TenMaysua = globalVariables.gv_strComputerName
            };
            if (drv[KcbDonthuocChitiet.Columns.NgayhenMuiketiep] != null && drv[KcbDonthuocChitiet.Columns.NgayhenMuiketiep] != DBNull.Value)
            {
                chitiet.NgayhenMuiketiep = Convert.ToDateTime(drv[KcbDonthuocChitiet.Columns.NgayhenMuiketiep]);
            }
            else
            {
                chitiet.NgayhenMuiketiep = null;
            }
            return chitiet;
        }

        private void LayDanhsachKhoVacxin()
        {
            try
            {
                this.dtStockList = new DataTable();
                if (noitru == 0)
                {
                    if (KIEU_THUOC_VT == "THUOC")
                    {
                        this.dtStockList = CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE_NGOAI_TRU_KEDON(this.objLuotkham.MaDoituongKcb);
                    }
                    else
                    {
                        List<string> lstLoaiBn= new List<string> { "TATCA" };
                        if (noitru == 1)
                            lstLoaiBn.Add("NOITRU");
                        else
                            lstLoaiBn.Add("NGOAITRU");
                        this.dtStockList = CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_LE(lstLoaiBn);
                    }
                }
                else//Nội trú
                {
                    if (KIEU_THUOC_VT == "THUOC")
                    {
                        this.dtStockList = CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_TUTHUOC_NOITRU_THEOKHOA((int)this.objLuotkham.IdKhoanoitru);
                    }
                    else
                    {
                        this.dtStockList = CommonLoadDuoc.LAYTHONGTIN_VATTU_KHOA((int)this.objLuotkham.IdKhoanoitru);
                    }
                }
                VNS.Libs.DataBinding.BindDataCombobox(this.cboStock, this.dtStockList, TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho);
                this.cboStock.SelectedIndex = Utility.GetSelectedIndex(this.cboStock, PropertyLib._ThamKhamProperties.IDKho.ToString());
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin combobox");
            }
        }

        private string GetTenBenh(string MaBenh)
        {
            string str = "";
            DataRow[] rowArray = globalVariables.gv_dtDmucBenh.Select(string.Format(DmucBenh.Columns.MaBenh + "='{0}'", MaBenh));
            if (rowArray.GetLength(0) > 0)
            {
                str = Utility.sDbnull(rowArray[0][DmucBenh.Columns.TenBenh], "");
            }
            return str;
        }

        private string GetUnitName(string ma)
        {
            try
            {
                DmucChung chung = THU_VIEN_CHUNG.LaydoituongDmucChung("DONVITINH", ma);
                if (chung != null)
                {
                    return chung.Ten;
                }
                return "";
            }
            catch (Exception)
            {
                return "Lượt";
            }
        }

        private void grdPresDetail_CellEdited(object sender, ColumnActionEventArgs e)
        {
            this.CreateViewTable();
        }

        private void grdPresDetail_CellUpdated(object sender, ColumnActionEventArgs e)
        {
        }

        private void grdPresDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                this.mnuDelele_Click(this.mnuDelele, new EventArgs());
            }
        }

        private void grdPresDetail_SelectionChanged(object sender, EventArgs e)
        {
            this.ModifyButton();
        }

        private void grdPresDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                GridEXRow currentRow = this.grdPresDetail.CurrentRow;
                if (e.Column.Key == "stt_in")
                {
                    long IdChitietdonthuoc = Utility.Int64Dbnull(currentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, 0);
                    if (IdChitietdonthuoc > -1)
                        _KEDONTHUOC.Capnhatchidanchitiet(IdChitietdonthuoc,KcbDonthuocChitiet.Columns.SttIn, e.Value.ToString());
                    this.grdPresDetail.UpdateData();
                    DataRow[] arrSourceTable=m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdChitietdonthuoc+"="+IdChitietdonthuoc.ToString());
                    foreach (DataRow dr in arrSourceTable)
                        dr[KcbDonthuocChitiet.Columns.SttIn] = e.Value;
                    arrSourceTable = m_dtDonthuocChitiet_View.Select(KcbDonthuocChitiet.Columns.IdChitietdonthuoc + "=" + IdChitietdonthuoc.ToString());
                    foreach (DataRow dr in arrSourceTable)
                        dr[KcbDonthuocChitiet.Columns.SttIn] = e.Value;
                    m_dtDonthuocChitiet.AcceptChanges();
                    m_dtDonthuocChitiet_View.AcceptChanges();
                }
                if (e.Column.Key == "mota_them_chitiet")
                {
                    long IdChitietdonthuoc = Utility.Int64Dbnull(currentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, 0);
                    if(IdChitietdonthuoc>-1)
                        _KEDONTHUOC.Capnhatchidanchitiet(IdChitietdonthuoc, KcbDonthuocChitiet.Columns.MotaThem, e.Value.ToString());
                    this.grdPresDetail.UpdateData();
                    DataRow[] arrSourceTable = m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdChitietdonthuoc + "=" + IdChitietdonthuoc.ToString());
                    foreach (DataRow dr in arrSourceTable)
                        dr["mota_them_chitiet"] = e.Value;
                    arrSourceTable = m_dtDonthuocChitiet_View.Select(KcbDonthuocChitiet.Columns.IdChitietdonthuoc + "=" + IdChitietdonthuoc.ToString());
                    foreach (DataRow dr in arrSourceTable)
                        dr["mota_them_chitiet"] = e.Value;
                    m_dtDonthuocChitiet.AcceptChanges();
                    m_dtDonthuocChitiet_View.AcceptChanges();
                }
                else if ((e.Column.Key != KcbDonthuocChitiet.Columns.TuTuc) && (e.Column.Key == KcbDonthuocChitiet.Columns.SoLuong))
                {
                    Func<DataRow, bool> predicate = null;
                    int id_thuoc = Utility.Int32Dbnull(currentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, 0);
                    int num = Utility.Int32Dbnull(currentRow.Cells[KcbDonthuocChitiet.Columns.IdThuockho].Value, 0);
                    decimal don_gia = Utility.DecimaltoDbnull(currentRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, 0M);
                    this.hasChanged = true;
                    int num2 = Utility.Int32Dbnull(e.InitialValue, 0);
                    int num3 = Utility.Int32Dbnull(e.Value, 0);
                    int num4 = num3 - num2;
                    if (num3 != num2)
                    {
                        if (num3 > num2)
                        {
                            this.AddQuantity(id_thuoc, num, num3 - num2);
                        }
                        else
                        {
                            if (predicate == null)
                            {
                                predicate = q => (Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdThuoc], 0) == id_thuoc) && (Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.DonGia], 0) == don_gia);
                            }
                            DataRow[] rowArray = (from q in this.m_dtDonthuocChitiet.Select("1=1").AsEnumerable<DataRow>().Where<DataRow>(predicate)
                                                  orderby q[KcbDonthuocChitiet.Columns.SttIn] descending
                                                  select q).ToArray<DataRow>();
                            int num5 = num2 - num3;
                            Dictionary<int, int> dictionary = new Dictionary<int, int>();
                            List<int> lstDeleteId = new List<int>();
                            int iddetail = -1;
                            string lstIdChitietDonthuoc = "";
                            for (int i = 0; i <= (rowArray.Length - 1); i++)
                            {
                                if (num5 > 0)
                                {
                                    int num8 = Utility.Int32Dbnull(rowArray[i][KcbDonthuocChitiet.Columns.SoLuong], 0);
                                    if (num8 >= num5)
                                    {
                                        rowArray[i][KcbDonthuocChitiet.Columns.SoLuong] = num8 - num5;
                                        num5 = num8 - num5;
                                        dictionary.Add(Utility.Int32Dbnull(rowArray[i][KcbDonthuocChitiet.Columns.IdChitietdonthuoc]), num5);
                                        iddetail = Utility.Int32Dbnull(rowArray[i][KcbDonthuocChitiet.Columns.IdChitietdonthuoc]);
                                        if (num5 <= 0)
                                        {
                                            lstIdChitietDonthuoc = lstIdChitietDonthuoc + Utility.sDbnull(rowArray[i][KcbDonthuocChitiet.Columns.IdChitietdonthuoc], "-1") + ",";
                                        }
                                        break;
                                    }
                                    rowArray[i][KcbDonthuocChitiet.Columns.SoLuong] = 0;
                                    lstIdChitietDonthuoc = lstIdChitietDonthuoc + Utility.sDbnull(rowArray[i][KcbDonthuocChitiet.Columns.IdChitietdonthuoc], "-1") + ",";
                                    lstDeleteId.Add(Utility.Int32Dbnull(rowArray[i][KcbDonthuocChitiet.Columns.IdChitietdonthuoc]));
                                    num5 -= num8;
                                }
                            }
                            this._KEDONTHUOC.XoaChitietDonthuoc(lstIdChitietDonthuoc, iddetail, num5);
                            this.grdPresDetail.UpdateData();
                            this.DeletefromDatatable(lstDeleteId, iddetail, num5);
                        }
                        int num9 = Utility.Int32Dbnull(this.m_dtDonthuocChitiet_View.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc.ToString() + " AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + don_gia.ToString())[0][KcbDonthuocChitiet.Columns.SoLuong], 0);
                        if (num4 > 0)
                        {
                            e.Value = num9;
                        }
                        else
                        {
                            num9 = Utility.Int32Dbnull(e.Value, 0);
                            e.Value = e.Value;
                        }
                        DataRow[] rowArray2 = this.m_dtDonthuocChitiet_View.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc.ToString());
                        foreach (DataRow row2 in rowArray2)
                        {
                            if ((row2[KcbDonthuocChitiet.Columns.IdThuoc].ToString() == id_thuoc.ToString()) && (Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.DonGia], 0M) == don_gia))
                            {
                                row2[KcbDonthuocChitiet.Columns.SoLuong] = num9;
                            }
                            int num10 = Utility.Int32Dbnull(row2[KcbDonthuocChitiet.Columns.SoLuong], 0);
                            if (num10 > 0)
                            {
                                row2["TT_KHONG_PHUTHU"] = num10 * Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.DonGia]);
                                row2["TT"] = num10 * (Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.PhuThu]));
                                row2["TT_BHYT"] = num10 * Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.BhytChitra]);
                                row2["TT_BN"] = num10 * (Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.PhuThu], 0));
                                row2["TT_PHUTHU"] = num10 * Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.PhuThu], 0);
                                row2["TT_BN_KHONG_PHUTHU"] = num10 * Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                            }
                            else
                            {
                                this.m_dtDonthuocChitiet_View.Rows.Remove(row2);
                            }
                        }
                        this.m_dtDonthuocChitiet_View.AcceptChanges();
                    }
                }
            }
            catch
            {
            }
        }

        private void grdPresDetail_UpdatingCell_old(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == KcbDonthuocChitiet.Columns.SoLuong)
                {
                    this.hasChanged = true;
                    string str = "";
                    long key = Utility.Int64Dbnull(this.grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuockho].Value, -1);
                    if (this.lstChangeData.ContainsKey(key))
                    {
                        str = this.lstChangeData[key];
                        str = str.Split(new char[] { '-' })[0] + "-" + e.Value.ToString();
                        this.lstChangeData[key] = str;
                    }
                    else
                    {
                        str = e.InitialValue + "-" + e.Value.ToString();
                        this.lstChangeData.Add(key, str);
                    }
                    DataRow[] rowArray = this.m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdThuockho + "=" + key.ToString());
                    int num2 = Utility.Int32Dbnull(e.Value, Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]));
                    if (rowArray.Length > 0)
                    {
                        rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra] = num2 * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                        rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra] = num2 * (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                        rowArray[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(num2) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]);
                        rowArray[0]["TT"] = Utility.Int32Dbnull(num2) * (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu]));
                        rowArray[0]["TT_BHYT"] = Utility.Int32Dbnull(num2) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                        rowArray[0]["TT_BN"] = Utility.Int32Dbnull(num2) * (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                        rowArray[0]["TT_PHUTHU"] = Utility.Int32Dbnull(num2) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                        rowArray[0]["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(num2) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                    }
                    this.m_dtDonthuocChitiet.AcceptChanges();
                }
            }
            catch
            {
            }
        }

        private void InitEvents()
        {
            base.Load += new EventHandler(this.frm_KCB_KeVacxin_Tiemchung_Load);
            base.KeyDown += new KeyEventHandler(this.frm_KCB_KeVacxin_Tiemchung_KeyDown);
            base.FormClosing += new FormClosingEventHandler(this.frm_KCB_KeVacxin_Tiemchung_FormClosing);
            this.grdPresDetail.KeyDown += new KeyEventHandler(this.grdPresDetail_KeyDown);
            this.grdPresDetail.UpdatingCell += new UpdatingCellEventHandler(this.grdPresDetail_UpdatingCell);
            this.grdPresDetail.CellEdited += new ColumnActionEventHandler(this.grdPresDetail_CellEdited);
            this.grdPresDetail.CellUpdated += new ColumnActionEventHandler(this.grdPresDetail_CellUpdated);
            this.grdPresDetail.SelectionChanged += new EventHandler(this.grdPresDetail_SelectionChanged);
            this.txtPres_ID.TextChanged += new EventHandler(this.txtPres_ID_TextChanged);
            this.txtSoluong.TextChanged += new EventHandler(this.txtSoluong_TextChanged);
            this.txtDrugID.TextChanged += new EventHandler(this.txtDrugID_TextChanged);
            this.txtSolan.TextChanged += new EventHandler(this.txtSolan_TextChanged);
            this.txtSoLuongDung.TextChanged += new EventHandler(this.txtSoLuongDung_TextChanged);
            this.txtCachDung._OnSelectionChanged += new AutoCompleteTextbox_Danhmucchung.OnSelectionChanged(this.txtCachDung__OnSelectionChanged);
            this.txtCachDung.TextChanged += new EventHandler(this.txtCachDung_TextChanged);
            this.chkSaveAndPrint.CheckedChanged += new EventHandler(this.chkSaveAndPrint_CheckedChanged);
            this.chkNgayTaiKham.CheckedChanged += new EventHandler(this.chkNgayTaiKham_CheckedChanged);
            this.mnuDelele.Click += new EventHandler(this.mnuDelele_Click);
            this.cmdSavePres.Click += new EventHandler(this.cmdSavePres_Click);
            this.cmdExit.Click += new EventHandler(this.cmdExit_Click);
            this.cmdDelete.Click += new EventHandler(this.cmdDelete_Click);
            this.cmdDonThuocDaKe.Click += new EventHandler(this.cmdDonThuocDaKe_Click);
            this.cmdPrintPres.Click += new EventHandler(this.cmdPrintPres_Click);
            this.cmdAddDetail.Click += new EventHandler(this.cmdAddDetail_Click);
            this.cmdCauHinh.Click += new EventHandler(this.cmdCauHinh_Click);
            this.cboStock.SelectedIndexChanged += new EventHandler(this.cboStock_SelectedIndexChanged);
            this.txtdrug._OnGridSelectionChanged += new AutoCompleteTextbox_Thuoc.OnGridSelectionChanged(this.txtdrug__OnGridSelectionChanged);
            this.cboPrintPreview.SelectedIndexChanged += new EventHandler(this.cboPrintPreview_SelectedIndexChanged);
            this.cboA4.SelectedIndexChanged += new EventHandler(this.cboA4_SelectedIndexChanged);
            this.cboLaserPrinters.SelectedIndexChanged += new EventHandler(this.cboLaserPrinters_SelectedIndexChanged);
            this.chkHienthithuoctheonhom.CheckedChanged += new EventHandler(this.chkHienthithuoctheonhom_CheckedChanged);
            this.chkAskbeforeDeletedrug.CheckedChanged += new EventHandler(this.chkAskbeforeDeletedrug_CheckedChanged);
            this.txtMaBenhChinh.KeyDown += new KeyEventHandler(this.txtMaBenhChinh_KeyDown);
            this.txtMaBenhChinh.TextChanged += new EventHandler(this.txtMaBenhChinh_TextChanged);
            this.txtMaBenhphu.GotFocus += new EventHandler(this.txtMaBenhphu_GotFocus);
            this.txtMaBenhphu.KeyDown += new KeyEventHandler(this.txtMaBenhphu_KeyDown);
            this.txtMaBenhphu.TextChanged += new EventHandler(this.txtMaBenhphu_TextChanged);
            this.mnuThuoctutuc.Click += new EventHandler(this.mnuThuoctutuc_Click);
            this.txtCachDung._OnShowData += new AutoCompleteTextbox_Danhmucchung.OnShowData(this.txtCachDung__OnShowData);
            this.txtCachDung._OnSaveAs += new AutoCompleteTextbox_Danhmucchung.OnSaveAs(this.txtCachDung__OnSaveAs);
            this.txtdrug._OnChangedView += new AutoCompleteTextbox_Thuoc.OnChangedView(this.txtdrug__OnChangedView);
            txtdrug._OnEnterMe += new AutoCompleteTextbox_Thuoc.OnEnterMe(txtdrug__OnEnterMe);
            grd_ICD.ColumnButtonClick += new ColumnActionEventHandler(grd_ICD_ColumnButtonClick);
            cmdSearchBenhChinh.Click += new EventHandler(cmdSearchBenhChinh_Click);
            cmdSearchBenhPhu.Click += new EventHandler(cmdSearchBenhPhu_Click);
            chkHennhaclai.CheckedChanged += chkHennhaclai_CheckedChanged;
            txtLydotiem._OnShowData += txtLydotiem__OnShowData;
            txtVitritiem._OnShowData += txtVitritiem__OnShowData;
            chkMuithu.CheckedChanged += chkMuithu_CheckedChanged;
            cmdKecongtiem.Click+=cmdKecongtiem_Click;
        }
        DataTable m_dtCongtiem = new DataTable();
        void LayCongtiem()
        {
             m_dtCongtiem = new Select().From(KcbChidinhcl.Schema)
                .Where(KcbChidinhcl.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(KcbChidinhcl.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .And(KcbChidinhcl.Columns.KieuChidinh).IsEqualTo(4)
                 .ExecuteDataSet().Tables[0];
            if (m_dtCongtiem != null && m_dtCongtiem.Rows.Count > 0)
            {
                errorProvider1.Clear();
            }
            else
                errorProvider1.SetError(cmdKecongtiem, "Chú ý: Chưa kê khai công tiêm chủng");
        }
        void cmdKecongtiem_Click(object sender, EventArgs e)
        {
            if (m_dtDonthuocChitiet != null && m_dtDonthuocChitiet.Rows.Count <= 0)
            {
                Utility.ShowMsg("Chú ý: Bạn nên kê vắc xin tiêm trước khi kê công tiêm chủng");
            }
            DataRow[] arrDr = m_dtCongtiem.Select("1=1");
            if (arrDr.Length <= 0)
                ThemCongtiem();
            else
                CapnhatCongtiem(Utility.Int64Dbnull(arrDr[0]["id_chidinh"], 0));
        }
        private void ThemCongtiem()
        {
            try
            {
                frm_KCB_CHIDINH_CLS frm = new frm_KCB_CHIDINH_CLS("CONGTIEM", 4);
                frm.txtAssign_ID.Text = "-100";
                frm.AutoAddAfterCheck = true;
                frm.Exam_ID =id_kham;
                frm.objLuotkham = objLuotkham;
                frm.objBenhnhan = objBenhnhan;
                frm.objPhieudieutriNoitru = null;
                frm.m_eAction = action.Insert;
                frm.txtAssign_ID.Text = "-1";
                frm.noitru = 0;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    LayCongtiem();
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                
            }
        }
        private void CapnhatCongtiem(long idChidinh)
        {
            try
            {
                frm_KCB_CHIDINH_CLS frm = new frm_KCB_CHIDINH_CLS("CONGTIEM", 4);
                frm.txtAssign_ID.Text = "-100";
                frm.AutoAddAfterCheck = true;
                frm.Exam_ID = id_kham;
                frm.objLuotkham = objLuotkham;
                frm.objBenhnhan = objBenhnhan;
                frm.objPhieudieutriNoitru = null;
                frm.m_eAction = action.Update;
                frm.txtAssign_ID.Text = idChidinh.ToString();
                frm.noitru = 0;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    LayCongtiem();
                }
            }
            catch (Exception)
            {
                
            }
            finally
            {
               
            }
        }
        void chkMuithu_CheckedChanged(object sender, EventArgs e)
        {
            txtMuithu.Enabled = !chkMuithu.Checked;
        }

        void txtLydotiem__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtLydotiem.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtLydotiem.myCode;
                txtLydotiem.Init();
                txtLydotiem.SetCode(oldCode);
                txtLydotiem.Focus();
            } 
        }
        void txtVitritiem__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtVitritiem.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtVitritiem.myCode;
                txtVitritiem.Init();
                txtVitritiem.SetCode(oldCode);
                txtVitritiem.Focus();
            }
        }

        void chkHennhaclai_CheckedChanged(object sender, EventArgs e)
        {
            dtpHennhaclai.Enabled = chkHennhaclai.Checked;
        }
        private void cmdSearchBenhChinh_Click(object sender, EventArgs e)
        {
            ShowDiseaseList(txtMaBenhphu);
        }
        private void cmdSearchBenhPhu_Click(object sender, EventArgs e)
        {
           ShowDiseaseList(txtMaBenhphu);
        }
        private void ShowDiseaseList(EditBox txt)
        {
            try
            {
                var frm = new frm_QuickSearchDiseases();
                frm.DiseaseType_ID = -1;
                frm.ShowDialog();
                if (frm.m_blnCancel)
                {
                    txt.Text = frm.v_DiseasesCode;
                    txt.Focus();
                    txt.SelectAll();
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
        }
        bool AllowDrugChanged = false;
        void txtdrug__OnEnterMe()
        {
            AllowDrugChanged = true;
            txtDrugID_TextChanged(txtDrugID, new EventArgs());
            txtSoluong.Focus();
            txtSoluong.SelectAll();
            
        }

        void grd_ICD_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "XOA")
                {
                    grd_ICD.CurrentRow.Delete();
                    dt_ICD_PHU.AcceptChanges();
                    grd_ICD.Refetch();
                    grd_ICD.AutoSizeColumns();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình xóa thông tin Mã ICD");
                throw;
            }
            finally
            {
            }
        }


        private void InsertPres()
        {
            try
            {
                Dictionary<long, long> lstChitietDonthuoc = new Dictionary<long, long>();
                KcbDonthuocChitiet[] arrDonthuocChitiet = this.CreateArrayPresDetail();
                if (arrDonthuocChitiet != null)
                {
                    this._actionResult = this._KEDONTHUOC.ThemDonThuoc(null,this.objLuotkham, this.CreateNewPres(), arrDonthuocChitiet, this._KcbChandoanKetluan, ref this.IdDonthuoc, ref lstChitietDonthuoc);
                    switch (this._actionResult)
                    {
                        case ActionResult.Error:
                            this.setMsg(this.lblMsg, "Lỗi trong quá trình lưu đơn "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư"), true);
                            break;

                        case ActionResult.Success:
                            this.UpdateChiDanThem();
                            this.txtPres_ID.Text = this.IdDonthuoc.ToString();
                            this.em_Action = action.Update;
                            this.setMsg(this.lblMsg, "Bạn thực hiện lưu đơn "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư") +" thành công", false);
                            this.UpdateDetailID(lstChitietDonthuoc);
                            this.m_blnCancel = false;
                            break;
                    }
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
            finally
            {
                if (this.Manual)
                {
                    this.em_Action = action.Update;
                }
            }
        }

        private void InsertPres(KcbDonthuocChitiet[] arrPresDetail)
        {
            try
            {
                Dictionary<long, long> lstChitietDonthuoc = new Dictionary<long, long>();
                if (arrPresDetail != null)
                {
                    this._actionResult = this._KEDONTHUOC.ThemDonThuoc(null,this.objLuotkham, this.CreateNewPres(), arrPresDetail, this._KcbChandoanKetluan, ref this.IdDonthuoc, ref lstChitietDonthuoc);
                    switch (this._actionResult)
                    {
                        case ActionResult.Error:
                            this.setMsg(this.lblMsg, "Lỗi trong quá trình lưu đơn "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư"), true);
                            break;

                        case ActionResult.Success:
                            this.UpdateChiDanThem();
                            this.txtPres_ID.Text = this.IdDonthuoc.ToString();
                            this.em_Action = action.Update;
                            this.setMsg(this.lblMsg, "Bạn thực hiện lưu đơn "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư") +" thành công", false);
                            this.UpdateDetailID(lstChitietDonthuoc);
                            this.m_blnCancel = false;
                            break;
                    }
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
            finally
            {
                if (this.Manual)
                {
                    this.em_Action = action.Update;
                }
            }
        }

        private bool InValiMaBenh(string mabenh)
        {
            return globalVariables.gv_dtDmucBenh.AsEnumerable().Where<DataRow>(benh => (Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == Utility.sDbnull(mabenh))).Any<DataRow>();
        }

        private bool isChanged(string value)
        {
            string[] strArray = value.Split(new char[] { '-' });
            if (strArray.Length != 2)
            {
                return false;
            }
            return (Utility.Int32Dbnull(strArray[0], 0) != Utility.Int32Dbnull(strArray[1], 0));
        }

        private void Load_DSach_ICD()
        {
            try
            {
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy danh sách ICD");
            }
        }

        private void LoadBenh()
        {
            try
            {
                this.AllowTextChanged = true;
                this.isLike = false;
                this.txtChanDoan._Text = Utility.sDbnull(this._Chandoan);
                this.txtMaBenhChinh.Text = Utility.sDbnull(this._MabenhChinh);
                this.isLike = true;
                this.AllowTextChanged = false;
                this.grd_ICD.DataSource = this.dt_ICD_PHU;
            }
            catch
            {
            }
        }

        private void LoadLaserPrinters()
        {
            if (!string.IsNullOrEmpty(PropertyLib._MayInProperties.TenMayInBienlai))
            {
                PropertyLib._MayInProperties.TenMayInBienlai = Utility.GetDefaultPrinter();
            }
            if (PropertyLib._ThamKhamProperties != null)
            {
                try
                {
                    this.cboLaserPrinters.Items.Clear();
                    for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                    {
                        string item = PrinterSettings.InstalledPrinters[i];
                        this.cboLaserPrinters.Items.Add(item);
                    }
                }
                catch
                {
                }
                finally
                {
                    this.cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                }
            }
        }

        private void mnuDelele_Click(object sender, EventArgs e)
        {
            try
            {
                this.setMsg(this.lblMsg, "", false);
                if ((this.grdPresDetail.RowCount <= 0) || (this.grdPresDetail.CurrentRow.RowType != RowType.Record))
                {
                    this.setMsg(this.lblMsg, "Bạn phải chọn "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư") +" để xóa", true);
                    this.grdPresDetail.Focus();
                }
                else
                {
                    int num = Utility.Int32Dbnull(this.grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                    string s = "";
                    List<int> vals = this.GetIdChitiet(Utility.Int32Dbnull(this.grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1), Utility.DecimaltoDbnull(this.grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, -1), ref s);
                    if (new SubSonic.Select().From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).In(vals).And(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1).GetRecordCount() > 0)
                    {
                        this.setMsg(this.lblMsg, "Bản ghi đã thanh toán, bạn không thể xóa", true);
                        this.grdPresDetail.Focus();
                    }
                    else if (!PropertyLib._ThamKhamProperties.Hoitruockhixoathuoc || Utility.AcceptQuestion("Bạn Có muốn xóa các "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư") +" đang chọn hay không?", "thông báo xóa", true))
                    {
                        this._KEDONTHUOC.XoaChitietDonthuoc(s);
                        this.grdPresDetail.CurrentRow.Delete();
                        this.grdPresDetail.UpdateData();
                        this.DeletefromDatatable(vals);
                        this.m_blnCancel = false;
                        this.UpdateDataWhenChanged();
                    }
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
            finally
            {
                if (this.grdPresDetail.RowCount <= 0)
                {
                    this.em_Action = action.Insert;
                }
            }
        }

        private void mnuThuoctutuc_Click(object sender, EventArgs e)
        {
            this.Updatethuoctutuc();
        }

        private void ModifyButton()
        {
            try
            {
                this.cmdSavePres.Enabled = this.grdPresDetail.RowCount > 0;
                this.cmdPrintPres.Enabled = (Utility.isValidGrid(this.grdPresDetail) && PropertyLib._ThamKhamProperties.ChophepIndonthuoc) && (this.grdPresDetail.RowCount > 0);
                this.cmdDelete.Enabled = Utility.isValidGrid(this.grdPresDetail);
                this.cmdAddDetail.Enabled = Utility.Int32Dbnull(this.txtDrugID.Text) > 0;
                this.mnuThuoctutuc.Enabled = Utility.isValidGrid(this.grdPresDetail);
            }
            catch (Exception)
            {
            }
        }

        private void PerformAction()
        {
            switch (this.em_Action)
            {
                case action.Insert:
                    this.InsertPres();
                    break;

                case action.Update:
                    this.UpdatePres();
                    break;
            }
        }

        private void PerformAction(KcbDonthuocChitiet[] arrPresDetail)
        {
            this.isSaved = true;
            
            this.Create_ChandoanKetluan();

            switch (this.em_Action)
            {
                case action.Insert:
                    this.InsertPres(arrPresDetail);
                    break;

                case action.Update:
                    this.UpdatePres(arrPresDetail);
                    break;
            }
        }

        private void PrintPres(int PresID)
        {
            DataTable dataTable = this._KEDONTHUOC.LaythongtinDonthuoc_In(PresID);
            if (dataTable.Rows.Count <= 0)
            {
                Utility.ShowMsg("không tìm  thấy "+ (KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư")+", Có thể bạn chưa lưu được "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư")+", \nMời bạn kiểm tra lại", "thông báo", MessageBoxIcon.Exclamation);
                return;
            }
            Utility.AddColumToDataTable(ref dataTable, "BarCode", typeof(byte[]));
            THU_VIEN_CHUNG.CreateXML(dataTable, "thamkham_InDonthuocA4.xml");
            
            byte[] buffer = null;
            Utility.CreateBarcodeData(ref dataTable, Utility.sDbnull(this.txtPres_ID.Text), ref buffer);
            string str = "";
            string str2 = "";
            if ((dataTable != null) && (dataTable.Rows.Count > 0))
            {
                this.GetChanDoan(Utility.sDbnull(dataTable.Rows[0]["mabenh_chinh"], ""), Utility.sDbnull(dataTable.Rows[0]["mabenh_phu"], ""), ref str, ref str2);
            }
            foreach (DataRow row in dataTable.Rows)
            {
                row["BarCode"] = buffer;
                row["chan_doan"] = (Utility.sDbnull(row["chan_doan"]).Trim() == "") ? str : (Utility.sDbnull(row["chan_doan"]) + ";" + str);
                row["ma_icd"] = str2;
            }
            dataTable.AcceptChanges();
            Utility.UpdateLogotoDatatable(ref dataTable);
            string str3 = "A5";
            if (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4)
            {
                str3 = "A4";
            }
            ReportDocument document = new ReportDocument();
            string tieude = "";
            string fileName = "";
            string str6 = str3;
            if (str6 != null)
            {
                if (!(str6 == "A5"))
                {
                    if (str6 == "A4")
                    {
                        document = Utility.GetReport("thamkham_InDonthuocA4", ref tieude, ref fileName);
                        goto Label_0252;
                    }
                }
                else
                {
                    document = Utility.GetReport("thamkham_InDonthuocA5", ref tieude, ref fileName);
                    goto Label_0252;
                }
            }
            document = Utility.GetReport("thamkham_InDonthuocA5", ref tieude, ref fileName);
        Label_0252:
            if (document != null)
            {
                Utility.WaitNow(this);
                ReportDocument rptDoc = document;
                frmPrintPreview preview = new frmPrintPreview("IN ĐƠN "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư")+" BỆNH NHÂN", rptDoc, true, true);
                try
                {
                    rptDoc.SetDataSource(dataTable);
                    Utility.SetParameterValue(rptDoc, "ParentBranchName", globalVariables.ParentBranch_Name);
                    Utility.SetParameterValue(rptDoc, "BranchName", globalVariables.Branch_Name);
                    Utility.SetParameterValue(rptDoc, "Address", globalVariables.Branch_Address);
                    Utility.SetParameterValue(rptDoc, "Phone", globalVariables.Branch_Phone);
                    Utility.SetParameterValue(rptDoc, "ReportTitle", "ĐƠN " + THU_VIEN_CHUNG.laytenthuoc_vattu(KIEU_THUOC_VT));
                    Utility.SetParameterValue(rptDoc, "CurrentDate", Utility.FormatDateTime(this.dtNgayIn.Value));
                    Utility.SetParameterValue(rptDoc, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                    preview.crptViewer.ReportSource = rptDoc;
                    if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInDonthuoc))
                    {
                        preview.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                        preview.ShowDialog();
                        this.cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                    }
                    else
                    {
                        preview.addTrinhKy_OnFormLoad();
                        rptDoc.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                        rptDoc.PrintToPrinter(1, false, 0, 0);
                    }
                    Utility.DefaultNow(this);
                }
                catch (Exception)
                {
                    Utility.DefaultNow(this);
                }
            }
        }

        private void ProcessData()
        {
        }

        private void SaveDefaultPrinter()
        {
            try
            {
                PropertyLib._MayInProperties.TenMayInBienlai = Utility.sDbnull(this.cboLaserPrinters.Text);
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi khi lưu trạng thái-->" + exception.Message);
            }
        }

        private void SaveMe()
        {
            try
            {
                this.cmdSavePres.Enabled = false;
                if (this.grdPresDetail.RowCount <= 0)
                {
                    this.setMsg(this.lblMsg, "Hiện chưa Có bản ghi nào để thực hiện  lưu lại", true);
                    this.txtdrug.Focus();
                    this.txtdrug.SelectAll();
                }
                else
                {
                    this.isSaved = true;
                    switch (this.em_CallAction)
                    {
                        case CallAction.FromMenu:
                            if (this.hasChanged)
                            {
                                this.PerformAction();
                            }
                            if (!((this._temp == ActionResult.NotEnoughDrugInStock) || this.Manual))
                            {
                                base.Close();
                            }
                            return;

                        case CallAction.FromParentFormList:
                            this.m_blnCancel = false;
                            if (!this.Manual)
                            {
                                base.Close();
                            }
                            return;

                        case CallAction.FromAnotherForm:
                            this.m_blnCancel = false;
                            if (this.hasChanged)
                            {
                                this.PerformAction();
                            }
                            if (!((this._temp == ActionResult.NotEnoughDrugInStock) || this.Manual))
                            {
                                base.Close();
                            }
                            return;
                    }
                }
            }
            catch
            {
            }
            finally
            {
                this.cmdSavePres.Enabled = true;
                this.Manual = false;
                this.hasChanged = false;
            }
        }

        private void setMsg(Label item, string msg, bool isError)
        {
            try
            {
                item.Text = msg;
                if (isError)
                {
                    item.ForeColor = Color.Red;
                }
                else
                {
                    item.ForeColor = Color.DarkBlue;
                }
                Application.DoEvents();
            }
            catch
            {
            }
        }

       

        private void txtCachDung__OnSaveAs()
        {
        }

        private void txtCachDung__OnSelectionChanged()
        {
            this.ChiDanThuoc();
        }

        private void txtCachDung__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtCachDung.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtCachDung.myCode;
                txtCachDung.Init();
                txtCachDung.SetCode(oldCode);
                txtCachDung.Focus();
            } 
        }

        private void txtCachDung_TextChanged(object sender, EventArgs e)
        {
            this.ChiDanThuoc();
        }

        private void txtdrug__OnChangedView(bool gridview)
        {
            PropertyLib._AppProperties.GridView = gridview;
            PropertyLib.SaveProperty(PropertyLib._AppProperties);
        }
        int id_thuockho = -1;
        string madoituong_gia = "DV";
        private void txtdrug__OnGridSelectionChanged(string ID, int id_thuockho, string _name, string Dongia, string phuthu, int tutuc)
        {
            AllowDrugChanged = false;
            this.id_thuockho = id_thuockho;
            this.txtDrugID.Text = ID;
            this.txtPrice.Text = Dongia;
            this.txtSurcharge.Text = phuthu;
            
        }

        private void txtDrugID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowDrugChanged) return;
                this.m_decPrice = 0M;
                this.m_Surcharge = 0M;
                this.AllowTextChanged = false;
                Utility.SetMsg(lblMsg, "", false);
                if ((Utility.DoTrim(this.txtDrugID.Text) == "") || (Utility.Int32Dbnull(this.txtDrugID.Text, -1) < 0))
                {
                    this.m_decPrice = 0M;
                    this.tu_tuc = 0;
                    this.txtDrugID.Text = "";
                    this.txtDrug_Name.Text = "";
                    this.txtBietduoc.Clear();
                    this.txtDonViDung.Clear();
                    txtLieuluong.Clear();
                    txtLoaivacxin.Clear();
                    txtMota.Clear();
                    txtMuithu.Text = "0";
                    chkHennhaclai.Checked = false;
                    txtVitritiem.Clear();
                    txtTinhchat.Text = "0";
                    txtGioihanke.Text = "";
                    txtDonvichiaBut.Text = "";
                    txtMotathem.Clear();
                    this.txtSurcharge.Text = "0";
                    this.txtPrice.Text = "0";
                    this.txtdrugtypeCode.Clear();
                    this.cmdAddDetail.Enabled = false;
                    return;
                }
                DataRow[] rowArray = this.m_dtDanhmucthuoc.Select(DmucThuoc.Columns.IdThuoc + "=" + this.txtDrugID.Text);
                if (rowArray.Length > 0)
                {
                    madoituong_gia = rowArray[0]["madoituong_gia"].ToString();
                    this.txtTonKho.Text = CommonLoadDuoc.SoLuongTonTrongKho(-1L, Utility.Int32Dbnull(this.cboStock.SelectedValue), Utility.Int32Dbnull(this.txtDrugID.Text, -1), txtdrug.GridView ? this.id_thuockho : (long)this.txtdrug.id_thuockho, new int?(Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KIEMTRATHUOC_CHOXACNHAN", "1", false), 1)), Utility.ByteDbnull(objLuotkham.Noitru, 0)).ToString();
                    this.txtDonViDung.Text = rowArray[0]["ten_donvitinh"].ToString();
                    this.txtDrug_Name.Text = rowArray[0][DmucThuoc.Columns.TenThuoc].ToString();
                    this.txtBietduoc.Text = rowArray[0][DmucThuoc.Columns.MaHoatchat].ToString();
                    this.txtTinhchat.Text = rowArray[0][DmucThuoc.Columns.MaTinhchat].ToString();
                    this.txtGioihanke.Text = Utility.sDbnull(rowArray[0][DmucThuoc.Columns.GioihanKedon], "");
                    this.txtMotathem.Text = rowArray[0][DmucThuoc.Columns.MotaThem].ToString();
                    txtDonvichiaBut.Text =Utility.sDbnull( rowArray[0][DmucThuoc.Columns.DonviBut],"");
                    this.txtPrice.Text = rowArray[0]["GIA_BAN"].ToString();
                    this.tu_tuc = Utility.Int32Dbnull(rowArray[0]["tu_tuc"], 0);
                    this.txtSurcharge.Text = rowArray[0]["PHU_THU"].ToString();
                    this.txtdrugtypeCode.Text = rowArray[0][DmucLoaithuoc.Columns.MaLoaithuoc].ToString();

                    txtLieuluong.Text = Utility.sDbnull(rowArray[0][DmucThuoc.Columns.HamLuong]);
                    txtVitritiem.MyCode = Utility.sDbnull(rowArray[0]["cach_sudung"]);
                    txtVitritiem.Text = Utility.sDbnull(rowArray[0]["ten_cach_sudung"]);
                    txtVitritiem.Tag = Utility.sDbnull(rowArray[0]["cach_sudung"]);
                    txtLoaivacxin.Text = Utility.sDbnull(rowArray[0]["ten_loaithuoc"]);
                    txtMota.Text = Utility.sDbnull(rowArray[0][DmucThuoc.Columns.MotaThem]);

                    DataRow[] arrLichsutiem = m_dtDulieuTiemchungBN.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + txtDrugID.Text);
                    if (arrLichsutiem.Length > 0)
                    {
                        txtMuithu.Text = (Utility.Int32Dbnull(arrLichsutiem.AsEnumerable().Max(c => c[KcbDonthuocChitiet.Columns.MuiThu]), 0) + 1).ToString(); 
                    }
                    else
                        txtMuithu.Text = "1";
                    chkHennhaclai.Checked = false;
                    if (this.txtTinhchat.Text == "1")
                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NOIDUNGCANHBAO_THUOCDOCHAI", "0", false) == "1")
                            Utility.SetMsg(lblMsg, THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NOIDUNGCANHBAO_THUOCDOCHAI", Utility.DoTrim(txtMotathem.Text) == "" ? "Chú ý: THUỐC CÓ TÍNH CHẤT ĐỘC HẠI" : Utility.DoTrim(txtMotathem.Text), false), true);
                        else
                            Utility.SetMsg(lblMsg, "", false);
                    this.cmdAddDetail.Enabled = true;
                }
                else
                {
                    madoituong_gia = "DV";
                    this.m_decPrice = 0M;
                    this.tu_tuc = 0;
                    this.txtDrugID.Text = "";
                    this.txtDrug_Name.Text = "";
                    this.txtDonViDung.Clear();
                    txtTinhchat.Text = "0";
                    txtGioihanke.Text = "";
                    txtDonvichiaBut.Text = "";
                    txtLieuluong.Clear();
                    txtLoaivacxin.Clear();
                    txtMota.Clear();
                    txtMuithu.Text = "0";
                    chkHennhaclai.Checked = false;
                    txtVitritiem.Clear();
                    txtMotathem.Clear();
                    this.txtSurcharge.Text = "0";
                    this.txtPrice.Text = "0";
                    this.cmdAddDetail.Enabled = false;
                }
                this.m_blnGetDrugCodeFromList = false;
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
            finally
            {
                this.chkTutuc.Checked = this.tu_tuc == 1;
                this.AllowTextChanged = true;
            }
            this.ModifyButton();
        }

        private void txtMaBenhChinh_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && this.hasMorethanOne)
            {
                this.DSACH_ICD(this.txtMaBenhChinh, DmucChung.Columns.Ma, 0);
                this.hasMorethanOne = false;
            }
        }

        private void txtMaBenhChinh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    DataRow[] rowArray;
                    this.hasMorethanOne = true;
                    if (this.isLike)
                    {
                        rowArray = globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " like '" + Utility.sDbnull(this.txtMaBenhChinh.Text, "") + "%'");
                    }
                    else
                    {
                        rowArray = globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " = '" + Utility.sDbnull(this.txtMaBenhChinh.Text, "") + "'");
                    }
                    if (!string.IsNullOrEmpty(this.txtMaBenhChinh.Text))
                    {
                        if (rowArray.GetLength(0) == 1)
                        {
                            this.hasMorethanOne = false;
                            this.txtMaBenhChinh.Text = rowArray[0][DmucBenh.Columns.MaBenh].ToString();
                            this.txtTenBenhChinh.Text = Utility.sDbnull(rowArray[0][DmucBenh.Columns.TenBenh], "");
                        }
                        else
                        {
                            this.txtTenBenhChinh.Text = "";
                        }
                    }
                    else
                    {
                        this.txtTenBenhChinh.Text = "";
                    }
                }
                catch
                {
                }
            }
            finally
            {
            }
        }

        private void txtMaBenhphu_GotFocus(object sender, EventArgs e)
        {
            this.txtMaBenhphu_TextChanged(this.txtMaBenhphu, e);
        }

        private void txtMaBenhphu_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (this.hasMorethanOne)
                    {
                        this.DSACH_ICD(this.txtMaBenhphu, DmucChung.Columns.Ma, 1);
                        this.txtMaBenhphu.SelectAll();
                    }
                    else
                    {
                        this.AddBenhphu();
                        this.txtMaBenhphu.SelectAll();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtMaBenhphu_TextChanged(object sender, EventArgs e)
        {
            DataRow[] rowArray;
            this.hasMorethanOne = true;
            if (this.isLike)
            {
                rowArray = globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " like '" + Utility.sDbnull(this.txtMaBenhphu.Text, "") + "%'");
            }
            else
            {
                rowArray = globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " = '" + Utility.sDbnull(this.txtMaBenhphu.Text, "") + "'");
            }
            if (!string.IsNullOrEmpty(this.txtMaBenhphu.Text))
            {
                if (rowArray.GetLength(0) == 1)
                {
                    this.hasMorethanOne = false;
                    this.txtMaBenhphu.Text = rowArray[0][DmucBenh.Columns.MaBenh].ToString();
                    this.txtTenBenhPhu.Text = Utility.sDbnull(rowArray[0][DmucBenh.Columns.TenBenh], "");
                    this.TEN_BENHPHU = this.txtTenBenhPhu.Text;
                }
                else
                {
                    this.txtTenBenhPhu.Text = "";
                    this.TEN_BENHPHU = "";
                }
            }
            else
            {
                this.txtMaBenhphu.Text = "";
                this.TEN_BENHPHU = "";
            }
        }

        private void txtPres_ID_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            Utility.FormatCurrencyHIS(this.txtPrice);
        }

        private void txtSoluong_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Utility.DecimaltoDbnull(this.txtSoluong.Text,0) > 0) && (e.KeyCode == Keys.Enter))
            {
                if (!this._Found)
                {
                    if (globalVariables.gv_intChophepChinhgiathuocKhiKedon == 0)
                    {
                        this.txtSoLuongDung.Focus();
                        this.txtSoLuongDung.SelectAll();
                    }
                    else
                    {
                        this.txtPrice.Focus();
                        this.txtPrice.SelectAll();
                    }
                }
                else
                {
                    this.cmdAddDetail_Click(this.cmdAddDetail, new EventArgs());
                }
            }
        }

        private void txtSoluong_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Utility.ResetMessageError(this.errorProvider1);
                if (Utility.DoTrim(txtTonKho.Text) != "")
                {
                    if (Utility.Int32Dbnull(objDKho.KtraTon) == 1 && (Utility.DecimaltoDbnull(this.txtSoluong.Text, 0) > Utility.Int32Dbnull(this.txtTonKho.Text, 0)))
                    {
                        Utility.SetMsgError(this.errorProvider1, this.txtSoluong, "Số lượng " + THU_VIEN_CHUNG.laytenthuoc_vattu(KIEU_THUOC_VT) + " cấp phát vượt quá số lượng " + THU_VIEN_CHUNG.laytenthuoc_vattu(KIEU_THUOC_VT) + " trong kho. Mời bạn kiểm tra lại");
                        this.txtSoluong.Focus();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtSolan_TextChanged(object sender, EventArgs e)
        {
            this.ChiDanThuoc();
        }

        private void txtSoLuongDung_TextChanged(object sender, EventArgs e)
        {
            this.ChiDanThuoc();
        }

        private void txtSurcharge_TextChanged(object sender, EventArgs e)
        {
            Utility.FormatCurrencyHIS(this.txtSurcharge);
        }

        private void txtTenBenhChinh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!this.InValiMaBenh(this.txtMaBenhChinh.Text))
                {
                    this.DSACH_ICD(this.txtTenBenhChinh, DmucChung.Columns.Ten, 0);
                    this.txtMaBenhphu.Focus();
                }
                else
                {
                    this.txtMaBenhphu.Focus();
                }
            }
        }

        private void txtTenBenhChinh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtMaBenhChinh.TextLength <= 0)
                {
                    this.txtMaBenhChinh.ResetText();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình xử lý sự kiện");
            }
        }

        private void txtTenBenhPhu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.hasMorethanOne)
                {
                    this.DSACH_ICD(this.txtTenBenhPhu, DmucChung.Columns.Ten, 1);
                    this.txtTenBenhPhu.Focus();
                }
                else
                {
                    this.txtTenBenhPhu.Focus();
                }
            }
        }

        private void txtTenBenhPhu_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtTenBenhPhu.TextLength <= 0)
                {
                    this.Selected = false;
                    this.txtMaBenhphu.ResetText();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình xử lý sự kiện");
            }
        }

        private void UpdateChiDanThem()
        {
            if (!string.IsNullOrEmpty(txtChiDanThem.Text))
            {
                new Delete().From(DmucChung.Schema)
                    .Where(DmucChung.Columns.Loai).IsEqualTo("CDDT")
                    .And(DmucChung.Columns.NguoiTao).IsEqualTo(globalVariables.UserName)
                    .And(DmucChung.Columns.Ma).IsEqualTo(Utility.UnSignedCharacter(txtChiDanThem.Text.ToUpper())).Execute();
                DmucChung objDmucChung = new DmucChung();
                objDmucChung.NguoiTao = globalVariables.UserName;
                objDmucChung.NgayTao = globalVariables.SysDate;
                objDmucChung.Ten = Utility.sDbnull(Utility.chuanhoachuoi(txtChiDanThem.Text.Trim()));
                objDmucChung.TrangThai = 1;
                objDmucChung.MotaThem = Utility.sDbnull(Utility.chuanhoachuoi(txtChiDanThem.Text.Trim()));
                objDmucChung.Ma = Utility.UnSignedCharacter(objDmucChung.Ten.ToUpper());
                objDmucChung.Loai = "CDDT";
                objDmucChung.IsNew = true;
                objDmucChung.Save();

            }
        }

        private void UpdateDataWhenChanged()
        {
            try
            {
                decimal tongtien = Utility.DecimaltoDbnull(m_dtDonthuocChitiet.Compute("SUM(TT_BN)", "1=1"), "0");
                Utility.SetMsg(StatusBar.Panels["Tongtien"],"Tổng tiền BN: "+ String.Format(Utility.FormatDecimal(), tongtien));
            }
            catch (Exception ex)
            {
                
                
            }
        }

        private void UpdateDetailID(Dictionary<long, long> lstPresDetail)
        {
            if (lstPresDetail.Count > 0)
            {
                foreach (DataRow row in this.m_dtDonthuocChitiet.Rows)
                {
                    if (lstPresDetail.ContainsKey(Utility.Int64Dbnull(row[KcbDonthuocChitiet.Columns.IdThuockho])))
                    {
                        row[KcbDonthuocChitiet.Columns.IdChitietdonthuoc] = lstPresDetail[Utility.Int64Dbnull(row[KcbDonthuocChitiet.Columns.IdThuockho])];
                    }
                }
                this.m_dtDonthuocChitiet.AcceptChanges();

                CreateViewTable();

            }
        }

        private void UpdatePres()
        {
            Dictionary<long, long> lstChitietDonthuoc = new Dictionary<long, long>();
            KcbDonthuocChitiet[] arrDonthuocChitiet = this.CreateArrayPresDetail();
            if (arrDonthuocChitiet != null)
            {
                this._actionResult = this._KEDONTHUOC.CapnhatDonthuoc(null,this.objLuotkham, this.CreateNewPres(), arrDonthuocChitiet, this._KcbChandoanKetluan, ref this.IdDonthuoc, ref lstChitietDonthuoc);
                switch (this._actionResult)
                {
                    case ActionResult.Error:
                        this.setMsg(this.lblMsg, "Lỗi trong quá trình lưu đơn "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư"), true);
                        break;

                    case ActionResult.Success:
                        this.UpdateChiDanThem();
                        this.setMsg(this.lblMsg, "Bạn thực hiện lưu đơn "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư") +" thành công", false);
                        this.UpdateDetailID(lstChitietDonthuoc);
                        this.m_blnCancel = false;
                        break;
                }
            }
        }

        private void UpdatePres(KcbDonthuocChitiet[] arrPresDetail)
        {
            Dictionary<long, long> lstChitietDonthuoc = new Dictionary<long, long>();
            if (arrPresDetail != null)
            {
                this._actionResult = this._KEDONTHUOC.CapnhatDonthuoc(null,this.objLuotkham, this.CreateNewPres(), arrPresDetail, this._KcbChandoanKetluan, ref this.IdDonthuoc, ref lstChitietDonthuoc);
                switch (this._actionResult)
                {
                    case ActionResult.Error:
                        this.setMsg(this.lblMsg, "Lỗi trong quá trình lưu đơn "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư"), true);
                        break;

                    case ActionResult.Success:
                        this.UpdateChiDanThem();
                        this.setMsg(this.lblMsg, "Bạn thực hiện lưu đơn "+(KIEU_THUOC_VT == "THUOC" ?"thuốc":"vật tư") +" thành công", false);
                        this.UpdateDetailID(lstChitietDonthuoc);
                        this.m_blnCancel = false;
                        break;
                }
            }
        }

        private void Updatethuoctutuc()
        {
            try
            {
                if (Utility.isValidGrid(this.grdPresDetail))
                {
                    int num = Utility.Int32Dbnull(this.grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdChitietdonthuoc));
                    decimal num2 = Utility.Int32Dbnull(this.grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.DonGia));
                    int num3 = Utility.Int32Dbnull(this.grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdThuoc));
                    foreach (DataRow row in this.m_dtDonthuocChitiet.Rows)
                    {
                        if ((Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.DonGia], -1) == num2) && (Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.IdThuoc], -1) == num3))
                        {
                            row[KcbDonthuocChitiet.Columns.TuTuc] = 1;
                            if (this.tu_tuc == 0)
                            {
                                decimal BHCT = 0m;
                                if (objLuotkham.DungTuyen == 1)
                                {
                                    BHCT = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                                }
                                else
                                {
                                    if (objLuotkham.TrangthaiNoitru <= 0)
                                        BHCT = Utility.DecimaltoDbnull(row[KcbChidinhclsChitiet.Columns.DonGia], 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                                    else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                                        BHCT = Utility.DecimaltoDbnull(row[KcbChidinhclsChitiet.Columns.DonGia], 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                                }
                                //decimal num4 = (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * Utility.DecimaltoDbnull(this.objLuotkham.PtramBhyt, 0)) / 100M;
                                decimal num5 = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) - BHCT;
                                row[KcbDonthuocChitiet.Columns.BhytChitra] = BHCT;
                                row[KcbDonthuocChitiet.Columns.BnhanChitra] = num5;
                            }
                            else
                            {
                                row[KcbDonthuocChitiet.Columns.PtramBhyt] = 0;
                                row[KcbDonthuocChitiet.Columns.BhytChitra] = 0;
                                row[KcbDonthuocChitiet.Columns.BnhanChitra] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0);
                            }

                        }
                    }
                    CreateViewTable();
                }
            }
            catch
            {
            }
        }

        public string _Chandoan
        {
            get
            {
                return this.txtChanDoan.Text;
            }
            set
            {
                this.txtChanDoan._Text = value;
            }
        }

        public string _MabenhChinh
        {
            get
            {
                return this.txtMaBenhChinh.Text;
            }
            set
            {
                this.txtMaBenhChinh.Text = value;
            }
        }

        private int ID_Goi_Dvu { get; set; }

        public string MaDoiTuong { get; set; }

       

        public KcbLuotkham objLuotkham { get; set; }
        public KcbDanhsachBenhnhan objBenhnhan { get; set; }

        public KcbDangkyKcb objRegExam { get; set; }
        public NoitruPhieudieutri objPhieudieutriNoitru { get; set; }
        public int m_intKieudonthuoc { get; set; }

        public int TrongGoi { get; set; }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
     }
}

