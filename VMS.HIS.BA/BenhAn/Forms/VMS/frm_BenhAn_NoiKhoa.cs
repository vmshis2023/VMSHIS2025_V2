using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Aspose.Words;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using Janus.Windows.GridEX.EditControls;
using VNS.HIS.UI.Classess;
using VMS.HIS.Bus;
using VMS.HIS.Danhmuc.Dungchung;
using System.Transactions;

namespace VNS.HIS.UI.BA
{
    public partial class frm_BenhAn_NoiKhoa : Form
    {
        public delegate void OnCreated(long id,string ma_ba, action m_enAct);
        public event OnCreated _OnCreated;
        string lstLoaiBA = "";
        public frm_BenhAn_NoiKhoa(string lstLoaiBA)
        {
            InitializeComponent();
            this.lstLoaiBA = lstLoaiBA;
            Utility.SetVisualStyle(this);
            chkQLNBCapCuu.CheckedChanged += chkQLNBCapCuu_CheckedChanged;
            chkQLNBKKB.CheckedChanged += chkQLNBKKB_CheckedChanged;
            chkQLNBKhoaDieuTri.CheckedChanged += chkQLNBKhoaDieuTri_CheckedChanged;
            chkQLNBTuDen.CheckedChanged += chkQLNBTuDen_CheckedChanged;
            chkQLNBCoQuanYTe.CheckedChanged += chkQLNBCoQuanYTe_CheckedChanged;
            chkQLNBKhac.CheckedChanged += chkQLNBKhac_CheckedChanged;
            chkQLNBTuyenDuoi.CheckedChanged += chkQLNBTuyenDuoi_CheckedChanged;
            chkQLNBTuyenTren.CheckedChanged += chkQLNBTuyenTren_CheckedChanged;
            chkQLNBRaVien.CheckedChanged += chkQLNBRaVien_CheckedChanged;
            chkQLNBCK.CheckedChanged += chkQLNBCK_CheckedChanged;
            chkQLNBXinVe.CheckedChanged += chkQLNBXinVe_CheckedChanged;
            chkQLNBBoVe.CheckedChanged += chkQLNBBoVe_CheckedChanged;
            chkQLNBDuaVe.CheckedChanged += chkQLNBDuaVe_CheckedChanged;
            chkTTRVKhoi.CheckedChanged += chkTTRVKhoi_CheckedChanged;
            chkTTRVDoGiam.CheckedChanged += chkTTRVDoGiam_CheckedChanged;
            chkTTRVKhongThayDoi.CheckedChanged += chkTTRVKhongThayDoi_CheckedChanged;
            chkTTRVNangHon.CheckedChanged += chkTTRVNangHon_CheckedChanged;
            chkTTRVTuVong.CheckedChanged += chkTTRVTuVong_CheckedChanged;
            chkTTRVLanhTinh.CheckedChanged += chkTTRVLanhTinh_CheckedChanged;
            chkTTRVNghiNgo.CheckedChanged += chkTTRVNghiNgo_CheckedChanged;
            chkTTRVAcTinh.CheckedChanged += chkTTRVAcTinh_CheckedChanged;
            chkttrvDoBenh.CheckedChanged += chkttrvDoBenh_CheckedChanged;
            chkttrvTrong24GioVaoVien.CheckedChanged += chkttrvTrong24GioVaoVien_CheckedChanged;
            chkttrvDoTaiBien.CheckedChanged += chkttrvDoTaiBien_CheckedChanged;
            chkttrvSau24Gio.CheckedChanged += chkttrvSau24Gio_CheckedChanged;
            chkttrvKhac.CheckedChanged += chkttrvKhac_CheckedChanged;
            ucThongtinnguoibenh_v31._OnEnterMe += ucThongtinnguoibenh_v31__OnEnterMe;
            txtIDBenhAn.KeyDown += txtIDBenhAn_KeyDown;
            txtMaBenhAn.KeyDown += txtMaBenhAn_KeyDown;
            ucThongtinnguoibenh_v31.trangthai_noitru = 5;
            Utility.setEnterEvent(this);
            chkDiUng.CheckedChanged += chkDiUng_CheckedChanged;
            chkMaTuy.CheckedChanged += chkMaTuy_CheckedChanged;
            chkRuouBia.CheckedChanged += chkRuouBia_CheckedChanged;
            chkThuocLa.CheckedChanged += chkThuocLa_CheckedChanged;
            chkThuocLao.CheckedChanged += chkThuocLao_CheckedChanged;
            chkKhac.CheckedChanged += chkKhac_CheckedChanged;
            txtB_CTScanner.TextChanged += soluongto_TextChanged;
            txtB_Khac.TextChanged += soluongto_TextChanged;
            txtB_SieuAm.TextChanged += soluongto_TextChanged;
            txtB_XetNghiem.TextChanged += soluongto_TextChanged;
            txtB_Xquang.TextChanged += soluongto_TextChanged;
        }

        void soluongto_TextChanged(object sender, EventArgs e)
        {
            txtB_Tongso.Text =( Utility.Int32Dbnull(txtB_CTScanner.Text, 0) + Utility.Int32Dbnull(txtB_Khac.Text, 0) + Utility.Int32Dbnull(txtB_SieuAm.Text, 0) + Utility.Int32Dbnull(txtB_XetNghiem.Text, 0) + Utility.Int32Dbnull(txtB_Xquang.Text, 0)).ToString();
        }

        void chkKhac_CheckedChanged(object sender, EventArgs e)
        {
            txtKhac.Enabled = chkKhac.Checked;
            txtKhac.Focus();
        }

        void chkThuocLao_CheckedChanged(object sender, EventArgs e)
        {
            txtThuocLao.Enabled = chkThuocLao.Checked;
            txtThuocLao.Focus();
        }

        void chkThuocLa_CheckedChanged(object sender, EventArgs e)
        {
            txtThuocLa.Enabled = chkThuocLa.Checked;
            txtThuocLa.Focus();
        }

        void chkRuouBia_CheckedChanged(object sender, EventArgs e)
        {
            txtRuouBia.Enabled = chkRuouBia.Checked;
            txtRuouBia.Focus();
        }

        void chkMaTuy_CheckedChanged(object sender, EventArgs e)
        {
            txtMaTuy.Enabled = chkMaTuy.Checked;
            txtMaTuy.Focus();
        }

        void chkDiUng_CheckedChanged(object sender, EventArgs e)
        {
            txtDiUng.Enabled = chkDiUng.Checked;
            txtDiUng.Focus();  
        }
        void txtMaBenhAn_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string maBA = Utility.AutoFillMaBA(txtMaBenhAn.Text);
                    txtMaBenhAn.Text = maBA;
                    if (objBANoitru != null && maBA != objBANoitru.MaBa)
                    {
                        if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn tìm Bệnh án theo mã: {0}.\nHệ thống sẽ nạp lại dữ liệu của Bệnh án tìm được và các thông tin bạn đang làm việc chưa kịp lưu sẽ bị hủy.\nNhấn Ok để tiếp tục. Nhấn No để quay lại trạng thái làm việc trước đó", Utility.DoTrim(txtMaBenhAn.Text)), "", true))
                        {
                            return;
                        }
                    }
                    objBANoitru = new Select().From(BaNoitru.Schema).Where(BaNoitru.Columns.MaBa).IsEqualTo(Utility.DoTrim(txtMaBenhAn.Text)).ExecuteSingle<BaNoitru>();
                    if (objBANoitru == null)
                        ClearControl();
                    else
                    {
                        ucThongtinnguoibenh_v31.txtMaluotkham.Text = objBANoitru.MaLuotkham;
                        ucThongtinnguoibenh_v31.Refresh(true);
                        // FillData4Update();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommand();
            }
        }

        void txtIDBenhAn_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (objBANoitru != null)
                        if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn tìm Bệnh án theo ID: {0}.\nHệ thống sẽ nạp lại dữ liệu của Bệnh án tìm được và các thông tin bạn đang làm việc chưa kịp lưu sẽ bị hủy.\nNhấn Ok để tiếp tục. Nhấn No để quay lại trạng thái làm việc trước đó", Utility.DoTrim(txtIDBenhAn.Text)), "", true))
                        {
                            return;
                        }
                    objBANoitru = BaNoitru.FetchByID(Utility.Int64Dbnull(txtIDBenhAn.Text));
                    if (objBANoitru == null)
                        ClearControl();
                    else
                    {
                        ucThongtinnguoibenh_v31.txtMaluotkham.Text = objBANoitru.MaLuotkham;
                        ucThongtinnguoibenh_v31.Refresh(true);
                        // FillData4Update();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommand();
            }
        }

        void ucThongtinnguoibenh_v31__OnEnterMe()
        {
            if (ucThongtinnguoibenh_v31.objLuotkham != null && ucThongtinnguoibenh_v31.objBenhnhan!=null)
            {
                if (ucThongtinnguoibenh_v31.objLuotkham.TrangthaiNoitru <= 0)
                {
                    Utility.ShowMsg(string.Format("Người bệnh {0} với mã lần khám {1} đang ở trạng thái ngoại trú nên bạn không thể thực hiện tạo BA được. Vui lòng kiểm tra lại", ucThongtinnguoibenh_v31.objBenhnhan.TenBenhnhan, ucThongtinnguoibenh_v31.objLuotkham.MaLuotkham));
                    objLuotkham = null;
                    objBenhnhan = null;
                    objBANoitru = null;
                    ClearControl();
                    ucThongtinnguoibenh_v31.txtMaluotkham.Focus();
                    ucThongtinnguoibenh_v31.txtMaluotkham.SelectAll();
                    return;
                }
                objBANoitru = null;
                objLuotkham = ucThongtinnguoibenh_v31.objLuotkham;
                objBenhnhan = ucThongtinnguoibenh_v31.objBenhnhan;
                if (!KiemTraThongTin()) return;
                ClearControl();
                FillData4Update();
                dtQLNBVaoVien.Focus();
                ModifyCommand();
            }
        }

        #region checkbox
        private void chkttrvTrong24GioVaoVien_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvTrong24GioVaoVien.Checked == true)
            {
                chkttrvDoBenh.Checked = false;

                chkttrvDoTaiBien.Checked = false;
                chkttrvSau24Gio.Checked = false;
                chkttrvKhac.Checked = false;
            }
        }

        private void chkttrvDoTaiBien_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvDoTaiBien.Checked == true)
            {
                chkttrvDoBenh.Checked = false;
                chkttrvTrong24GioVaoVien.Checked = false;

                chkttrvSau24Gio.Checked = false;
                chkttrvKhac.Checked = false;
            }
        }

        private void chkttrvSau24Gio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvSau24Gio.Checked == true)
            {
                chkttrvDoBenh.Checked = false;
                chkttrvTrong24GioVaoVien.Checked = false;
                chkttrvDoTaiBien.Checked = false;

                chkttrvKhac.Checked = false;
            }
        }

        private void chkttrvKhac_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvKhac.Checked == true)
            {
                chkttrvDoBenh.Checked = false;
                chkttrvTrong24GioVaoVien.Checked = false;
                chkttrvDoTaiBien.Checked = false;
                chkttrvSau24Gio.Checked = false;

            }
        }

        private void chkQLNBBoVe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBBoVe.Checked == true)
            {
                chkQLNBRaVien.Checked = false;
                chkQLNBXinVe.Checked = false;

                chkQLNBDuaVe.Checked = false;

            }
        }

        private void chkQLNBDuaVe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBDuaVe.Checked == true)
            {
                chkQLNBRaVien.Checked = false;
                chkQLNBXinVe.Checked = false;
                chkQLNBBoVe.Checked = false;


            }
        }

        private void chkTTRVKhoi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVKhoi.Checked == true)
            {

                chkTTRVDoGiam.Checked = false;
                chkTTRVKhongThayDoi.Checked = false;
                chkTTRVNangHon.Checked = false;
                chkTTRVTuVong.Checked = false;


            }
        }

        private void chkTTRVDoGiam_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVDoGiam.Checked == true)
            {
                chkTTRVKhoi.Checked = false;

                chkTTRVKhongThayDoi.Checked = false;
                chkTTRVNangHon.Checked = false;
                chkTTRVTuVong.Checked = false;


            }
        }

        private void chkTTRVKhongThayDoi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVKhongThayDoi.Checked == true)
            {
                chkTTRVKhoi.Checked = false;
                chkTTRVDoGiam.Checked = false;

                chkTTRVNangHon.Checked = false;
                chkTTRVTuVong.Checked = false;


            }
        }

        private void chkTTRVNangHon_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVNangHon.Checked == true)
            {
                chkTTRVKhoi.Checked = false;
                chkTTRVDoGiam.Checked = false;
                chkTTRVKhongThayDoi.Checked = false;

                chkTTRVTuVong.Checked = false;


            }
        }

        private void chkTTRVTuVong_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVTuVong.Checked == true)
            {
                chkTTRVKhoi.Checked = false;
                chkTTRVDoGiam.Checked = false;
                chkTTRVKhongThayDoi.Checked = false;
                chkTTRVNangHon.Checked = false;



            }
        }

        private void chkTTRVLanhTinh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVLanhTinh.Checked == true)
            {

                chkTTRVNghiNgo.Checked = false;
                chkTTRVAcTinh.Checked = false;

            }
        }

        private void chkTTRVNghiNgo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVNghiNgo.Checked == true)
            {
                chkTTRVLanhTinh.Checked = false;

                chkTTRVAcTinh.Checked = false;

            }
        }

        private void chkTTRVAcTinh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVAcTinh.Checked == true)
            {
                chkTTRVLanhTinh.Checked = false;
                chkTTRVNghiNgo.Checked = false;


            }
        }

        private void chkttrvDoBenh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvDoBenh.Checked == true)
            {

                chkttrvTrong24GioVaoVien.Checked = false;
                chkttrvDoTaiBien.Checked = false;
                chkttrvSau24Gio.Checked = false;
                chkttrvKhac.Checked = false;
            }
        }


        private void chkQLNBCapCuu_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBCapCuu.Checked == true)
            {
                chkQLNBKKB.Checked = false;
                chkQLNBKhoaDieuTri.Checked = false;


            }
        }

        private void chkQLNBKKB_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBKKB.Checked == true)
            {

                chkQLNBKhoaDieuTri.Checked = false;
                chkQLNBCapCuu.Checked = false;

            }
        }

        private void chkQLNBKhoaDieuTri_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBKhoaDieuTri.Checked == true)
            {
                chkQLNBKKB.Checked = false;

                chkQLNBCapCuu.Checked = false;

            }
        }

        private void chkQLNBCoQuanYTe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBCoQuanYTe.Checked == true)
            {
                chkQLNBTuDen.Checked = false;

                chkQLNBKhac.Checked = false;


            }
        }

        private void chkQLNBTuDen_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBTuDen.Checked == true)
            {


                chkQLNBKhac.Checked = false;
                chkQLNBCoQuanYTe.Checked = false;

            }
        }

        private void chkQLNBKhac_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBKhac.Checked == true)
            {
                chkQLNBTuDen.Checked = false;
                chkQLNBCoQuanYTe.Checked = false;

            }
        }

        private void chkQLNBTuyenTren_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBTuyenTren.Checked == true)
            {

                chkQLNBTuyenDuoi.Checked = false;
                chkQLNBCK.Checked = false;

            }
        }

        private void chkQLNBTuyenDuoi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBTuyenDuoi.Checked == true)
            {
                chkQLNBTuyenTren.Checked = false;

                chkQLNBCK.Checked = false;

            }
        }

        private void chkQLNBCK_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBCK.Checked == true)
            {
                chkQLNBTuyenTren.Checked = false;
                chkQLNBTuyenDuoi.Checked = false;


            }
        }

        private void chkQLNBRaVien_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBRaVien.Checked == true)
            {

                chkQLNBXinVe.Checked = false;
                chkQLNBBoVe.Checked = false;
                chkQLNBDuaVe.Checked = false;

            }
        }

        private void chkQLNBXinVe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBXinVe.Checked == true)
            {
                chkQLNBRaVien.Checked = false;

                chkQLNBBoVe.Checked = false;
                chkQLNBDuaVe.Checked = false;

            }
        }
        #endregion
        private void GetChanDoanChinhPhu(string ICD_chinh, string IDC_Phu, ref string ICD_chinh_Name,
            ref string ICD_chinh_Code, ref string ICD_Phu_Name, ref string ICD_Phu_Code)
        {
            try
            {
                List<string> lstICD = ICD_chinh.Split(',').ToList();
                DmucBenhCollection _list = new Select().From(DmucBenh.Schema).Where(DmucBenh.Columns.MaBenh).In(lstICD).ExecuteAsCollection<DmucBenhCollection>();
                    //new DmucBenh().FetchByQuery(               DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _objBANoitru in _list)
                {
                    ICD_chinh_Name += _objBANoitru.TenBenh + ";";
                    ICD_chinh_Code += _objBANoitru.MaBenh + ";";
                }
                lstICD = IDC_Phu.Split(',').ToList();
                _list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _objBANoitru in _list)
                {
                    ICD_Phu_Name += _objBANoitru.TenBenh + ";";
                    ICD_Phu_Code += _objBANoitru.MaBenh + ";";
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
        }

        private void GetChanDoanNoitru()
        {
            var dtPatient = new DataTable();
            dtPatient =
                new Select("*")
                    .From(KcbChandoanKetluan.Schema)
                    .Where(KcbChandoanKetluan.Columns.MaLuotkham)                    .IsEqualTo(objLuotkham.MaLuotkham)
                     .And(KcbChandoanKetluan.Columns.KieuChandoan).IsEqualTo(2)//Chẩn đoán trong quá trình điều trị nội trú.
                    .And(KcbChandoanKetluan.Columns.Noitru)                    .IsEqualTo(1)
                    .ExecuteDataSet()
                    .Tables[0];
            foreach (DataRow row in dtPatient.Rows)
            {
                ICD_Khoa_NoITru += row["mabenh_chinh"] + ";";
                Name_Khoa_NoITru += row["chandoan"] + ";";
            }
        }

        private void GetChanDoanRaVien()
        {
           
            NoitruPhieuravien _phieuravien=new Select().From(NoitruPhieuravien.Schema)
                .Where(NoitruPhieuravien.Columns.IdBenhnhan).IsEqualTo(objBenhnhan.IdBenhnhan)
                .And(NoitruPhieuravien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieuravien>();
            string chandoan = "";
            string mabenh = "";
            string chandoanphu = "";
            string mabenhphu = "";
           if(_phieuravien!=null)
            {
                string ICD_Name = "";
                string ICD_Code = "";
                string ICD_Phu_Name = "";
                string ICD_Phu_Code = "";
                GetChanDoanChinhPhu(Utility.sDbnull(_phieuravien.MabenhChinh, ""),
                           Utility.sDbnull(_phieuravien.MabenhPhu, ""), ref ICD_Name, ref ICD_Code, ref ICD_Phu_Name, ref ICD_Phu_Code);
                chandoan += string.IsNullOrEmpty(_phieuravien.ChanDoan)
                    ? ICD_Name
                    : Utility.sDbnull(_phieuravien.ChanDoan);
                mabenh += ICD_Code;
                chandoanphu += ICD_Phu_Name;
                mabenhphu += ICD_Phu_Code;
               //Điền 1 số thông tin ra viện
                dtQLNBRaVien.Text = _phieuravien.NgayRavien.ToString("dd/MM/yyyy");
                foreach (CheckBox cb in pnlKetquadieutriravien.Controls)
                    if (Utility.sDbnull(cb.Tag, "-1") == _phieuravien.MaKquaDieutri)
                        cb.Checked = true;
                    else
                        cb.Checked = false;
                foreach (CheckBox cb in pnlTinhtrangravien.Controls)
                    if (Utility.sDbnull(cb.Tag, "-1") == _phieuravien.MaTinhtrangravien)
                        cb.Checked = true;
                    else
                        cb.Checked = false;
            }
            txtCDBenhChinh.Text = chandoan;
            txtCDMaBenhChinh.Text = Utility.sDbnull(mabenh);
            txtCDBenhKemTheo.Text = chandoanphu;
            txtCDMaBenhKemTheo.Text = mabenhphu;

        }
        string Get_ChanDoan_KKB_CapCuu()
        {
            string _result = string.Empty;
            try
            {
                SqlQuery sqlQuery = new Select(KcbChandoanKetluan.Columns.Chandoan, KcbChandoanKetluan.Columns.ChandoanKemtheo, KcbChandoanKetluan.Columns.MabenhChinh, KcbChandoanKetluan.Columns.MabenhPhu)
                                            .From(KcbChandoanKetluan.Schema)
                                              .Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                                      //.And(KcbChandoanKetluan.Columns.KeyCode).IsEqualTo("NGOAITRU")
                                                      .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objBenhnhan.IdBenhnhan).OrderAsc(KcbChandoanKetluan.Columns.NgayChandoan);
                var objInfoCollection = sqlQuery.ExecuteAsCollection<KcbChandoanKetluanCollection>();
                string chandoan = "";
                string mabenh = "";
                string tenbenhphu = "";
                string tenbenhchinh = "";
                string mabenhphu = "";
                foreach (KcbChandoanKetluan objDiagInfo in objInfoCollection)
                {
                    string ICD_Name = "";
                    string ICD_Code = "";
                    string ICD_Phu_Name = "";
                    string ICD_Phu_Code = "";
                    GetChanDoanChinhPhu(Utility.sDbnull(objDiagInfo.MabenhChinh, ""), Utility.sDbnull(objDiagInfo.MabenhPhu, ""), ref ICD_Name, ref ICD_Code, ref ICD_Phu_Name, ref ICD_Phu_Code);
                    chandoan += string.IsNullOrEmpty(objDiagInfo.Chandoan) ? "" : Utility.sDbnull(objDiagInfo.Chandoan);
                    tenbenhchinh += ICD_Name;
                    mabenh += ICD_Code;
                    tenbenhphu += ICD_Phu_Name;
                    mabenhphu += ICD_Phu_Code;
                }
                _result =THU_VIEN_CHUNG.Laygiatrithamsohethong("BA_SUDUNG_ICD_LAM_CHANDOANSOBO","0",true)=="1"  ? tenbenhchinh + tenbenhphu + chandoan : chandoan; //nếu dùng icd làm cdsb thì trên cdsb đã có tên bệnh rồi, ko cần cộng vào nữa
            }
            catch (Exception)
            {
                _result = string.Empty;
            }
            return _result;
        }

        //private void GetChanDoanKKB()
        //{
        //    SqlQuery sqlQuery = new Select(KcbChandoanKetluan.Columns.DiagInfo, KcbChandoanKetluan.Columns.DifferInfo,
        //                                    KcbChandoanKetluan.Columns.MainDiseaseId, KcbChandoanKetluan.Columns.AuxiDiseaseId).From(
        //                                        KcbChandoanKetluan.Schema)
        //                  .Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
        //                  .And(KcbChandoanKetluan.Columns.KeyCode)
        //                    .IsEqualTo("NGOAITRU")
        //                  .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(txtMaBN.Text).OrderAsc(
        //                      KcbChandoanKetluan.Columns.DiagDate);
        //    var objInfoCollection = sqlQuery.ExecuteAsCollection<KcbChandoanKetluanCollection>();
        //    string chandoan = "";
        //    string mabenh = "";
        //    string tenbenhphu = "";
        //    string tenbenhchinh = "";
        //    string mabenhphu = "";
        //    foreach (KcbChandoanKetluan objDiagInfo in objInfoCollection)
        //    {
        //        string ICD_Name = "";
        //        string ICD_Code = "";
        //        string ICD_Phu_Name = "";
        //        string ICD_Phu_Code = "";
        //        GetChanDoanChinhPhu(Utility.sDbnull(objDiagInfo.MainDiseaseId, ""),
        //                   Utility.sDbnull(objDiagInfo.AuxiDiseaseId, ""), ref ICD_Name, ref ICD_Code, ref ICD_Phu_Name, ref ICD_Phu_Code);
        //        chandoan += string.IsNullOrEmpty(objDiagInfo.DiagInfo) ? "" : Utility.sDbnull(objDiagInfo.DiagInfo);
        //        tenbenhchinh += ICD_Name;
        //        mabenh += ICD_Code;
        //        tenbenhphu += ICD_Phu_Name;
        //        mabenhphu += ICD_Phu_Code;
        //    }
        //    txtCDKKBCapCuu.Text = tenbenhchinh + tenbenhphu + chandoan;
        //    txtCDMaKKBCapCuu.Text = Utility.sDbnull(mabenh + "" + mabenhphu);

        //}
        private void ClearControl()
        {
            //txtMaBN.Clear();
            //txtMaLanKham.Clear();
            txtMaBenhAn.Clear();
            chkQLNBBoVe.Checked = false;
            chkQLNBCK.Checked = false;
            chkQLNBTuyenDuoi.Checked = false;
            chkQLNBTuyenTren.Checked = false;
            txtQLNBChuyenDen.Clear();
            dtQLNBRaVien.Clear();
            chkQLNBRaVien.Checked = false;


            chkQLNBXinVe.Checked = false;
            chkQLNBBoVe.Checked = false;
            chkQLNBDuaVe.Checked = false;
            txtQLNBTongSoNgayDieuTri.Clear();
            txtCDNoiChuyenDen.Clear();
            txtCDMaNoiChuyenDen.Clear();
            txtCDKKBCapCuu.Clear();
            txtCDMaKKBCapCuu.Clear();


            txtCDKhiVaoDieuTri.Clear();
            txtCDMaKhiVaoDieuTri.Clear();
            txtCDBenhChinh.Clear();
            txtCDMaBenhChinh.Clear();
            txtCDBenhKemTheo.Clear();
            txtCDMaBenhKemTheo.Clear();
            chkCDThuThuat.Checked = false;
            chkCDPhauThuat.Checked = false;
            chkCDTaiBien.Checked = false;
            chkCDBienChung.Checked = false;
            chkTTRVKhoi.Checked = false;
            chkTTRVDoGiam.Checked = false;
            chkTTRVKhongThayDoi.Checked = false;
            chkTTRVNangHon.Checked = false;
            chkTTRVTuVong.Checked = false;

            chkTTRVLanhTinh.Checked = false;
            chkTTRVNghiNgo.Checked = false;
            chkTTRVAcTinh.Checked = false;

            txtTTRVNgayTuVong.Clear();
            chkttrvDoBenh.Checked = false;
            chkttrvTrong24GioVaoVien.Checked = false;
            chkttrvDoTaiBien.Checked = false;
            chkttrvSau24Gio.Checked = false;
            chkttrvKhac.Checked = false;
            txtTTRVNguyenNhanChinhTuVong.Clear();
            chkTTRVKhamNgiemTuThi.Checked = false;
            txtTTRVChuanDoanGiaiPhau.Clear();
            txtBenhAnLyDoNhapVien.SetDefaultItem();
            txtBenhAnVaoNgayThu.Clear();
            txtBenhAnQuaTrinhBenhLy.Clear();
            txtBenhAnTienSuBenh.Clear();
            chkDiUng.Checked = false;
            chkMaTuy.Checked = false;
            chkRuouBia.Checked = false;
            chkThuocLa.Checked = false;
            chkThuocLao.Checked = false;
            chkKhac.Checked = false;
            txtDiUng.Clear();
            txtMaTuy.Clear();
            txtRuouBia.Clear();
            txtThuocLa.Clear();
            txtThuocLao.Clear();
            txtKhac.Clear();
            txtBenhAnGiaDinh.Clear();
            txtBenhAnToanThan.Clear();
            txtMach.Clear();
            txtNhietDo.Clear();
            txtha.Clear();
            txtNhipTho.Clear();
            txtCanNang.Clear();
            txtChieuCao.Clear();
            txtBMI.Clear();
            txtBenhAnTuanHoan.Clear();
            txtBenhAnHoHap.Clear();
            txtBenhAnTieuHoa.Clear();
            txtBenhAnThanTietNieuSinhDuc.Clear();
            txtBenhAnThanKinh.Clear();
            txtBenhAnCoXuongKhop.Clear();
            txtBenhAnTaiMuiHong.Clear();
            txtBenhAnRangHamMat.Clear();
            txtBenhAnMat.Clear();
            txtBenhAnNoiTiet.Clear();
            txtBenhAnCacXetNghiem.Clear();
            txtBenhAnTomTatBenhAn.Clear();
            txtBenhAnBenhChinh.Clear();
            txtBenhAnBenhKemTheo.Clear();
            txtBenhAnPhanBiet.Clear();
            txtBenhAnTienLuong.Clear();
            txtBenhAnHuongDieuTri.Clear();
            txtTKBAQuaTrinhBenhLy.Clear();
            txtTKBATTomTatKetQua.Clear();
            txtTKBAPhuongPhapDieuTri.Clear();
            txtTKBATinhTrangRaVien.Clear();
            txtTKBAHuongDieuTri.Clear();
            txtNguoiGiaoHoSo.Clear();
            txtNguoiNhanHoSo.Clear();
            txtBSDieuTri.Clear();
            txtB_CTScanner.Clear();
            txtB_Xquang.Clear();
            txtB_SieuAm.Clear();
            txtB_XetNghiem.Clear();
            txtB_Khac.Clear();


        }
       
        private bool KiemTraThongTin()
        {
            objLuotkham = Utility.getKcbLuotkham(objLuotkham);
            if (objLuotkham==null)
            {
                Utility.ShowMsg("Bệnh nhân không tồn tại trong CSDL. Vui lòng kiểm tra lại", "Thông báo", MessageBoxIcon.Warning);
                ucThongtinnguoibenh_v31.txtMaluotkham.Focus();
                return false;
            }
            if (Utility.sDbnull(cboLoaiBA.SelectedValue, "-1") == "-1")
            {
                Utility.ShowMsg("Cần chọn loại bệnh án");
                cboLoaiBA.Focus();
                return false;
            }
            if (Utility.sDbnull( txtBSlamBA.Text).Length>0 && txtBSlamBA.MyCode=="-1")
            {
                Utility.ShowMsg("Bác sĩ làm Bệnh án cần thuộc danh mục bác sĩ. Mời bạn chọn từ danh mục có sẵn");
                txtBSlamBA.Focus();
                return false;
            }
            if (Utility.sDbnull(txtBSDieuTri.Text).Length > 0 && txtBSDieuTri.MyCode == "-1")
            {
                Utility.ShowMsg("Bác sĩ điều trị cần thuộc danh mục bác sĩ. Mời bạn chọn từ danh mục có sẵn");
                txtBSDieuTri.Focus();
                return false;
            }
            return true;
        }
        DataTable dtDataBA = new DataTable(); 
        public BaNoitru objBANoitru;
        string maBA = "";
        private bool _isSuccess = false;
        
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!KiemTraThongTin()) return;
                objBANoitru = TaoBANoitru();
                //if (objBANoitru.IdBa > 0 && objBANoitru.MaBa != maBA)
                //{
                //    if(Utility.AcceptQuestion("Mã bệnh án cũ :{0} đang khác với mã bệnh án nhập tay: {1}. Bạn có chắc chắn muốn cập nhật lại thành mã bệnh án mới","",))
                //    {
                //    }
                //}
                 BaHosoluutru hsba =null;
                if (objBANoitru.IdBa <= 0)
                {
                    hsba = new BaHosoluutru();
                    hsba.IdBa = objBANoitru.IdBa;
                    hsba.LoaiBa = objBANoitru.LoaiBa;
                    hsba.MaBa = objBANoitru.MaBa;
                    hsba.IdBenhnhan = objBANoitru.IdBenhnhan;
                    hsba.MaLuotkham = objBANoitru.MaLuotkham;
                    hsba.MaCoso = objBANoitru.MaCoso;
                    hsba.NgayTao = objBANoitru.NgaylamBa;
                    hsba.NguoiTao = objBANoitru.NguoiTao;
                    hsba.Nam = objBANoitru.NgayTao.Value.Year;
                    hsba.TrangThai = 0;
                }
                KcbTomtatBA ttba = new Select().From(KcbTomtatBA.Schema).Where(KcbTomtatBA.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(KcbTomtatBA.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<KcbTomtatBA>();
                if (ttba == null) ttba = new KcbTomtatBA();
                if (ttba.Id > 0)
                {
                    ttba.IsNew = false;
                    ttba.MarkOld();
                    ttba.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                    ttba.NguoiSua = globalVariables.UserName;
                }
                else
                {
                    ttba.IsNew = true;
                    ttba.NguoiTao = globalVariables.UserName;
                    ttba.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                }
                ttba.MaLuotkham = objLuotkham.MaLuotkham;
                ttba.IdBenhnhan = (int)objLuotkham.IdBenhnhan;
                ttba.IdKhoadieutri = Utility.Int32Dbnull(objBANoitru.IdKhoadieutri, -1);
                ttba.TiensuBenh = "";
                ttba.TomtatKqcls = "";
                ttba.QuatrinhbenhlyDienbienlamsang = objBANoitru.TkbaQtbl;
                ttba.TomtatKqcls = objBANoitru.TkbaTtkqxn;
                ttba.TinhtrangRavienMota = objBANoitru.TkbaTtrv;
                ttba.PhuongphapDieutri = objBANoitru.TkbaPpdt;
                ttba.HuongDieutri = objBANoitru.TkbaHdt;
                ttba.NgayTtba = objBANoitru.NgayTongKetBA;
                ttba.Noikhoa = 0;
                ttba.NoikhoaMota = "";
                ttba.Pttt = 0;
                ttba.PtttMota = "";
               
                ttba.Save();
                objBANoitru.Save();
                hsba.IdBa = objBANoitru.IdBa;
                if (hsba != null) hsba.Save();
                txtIDBenhAn.Text = objBANoitru.IdBa.ToString();
                if (m_enAct == action.Insert)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới BA nội trú cho bệnh nhân: {0}-{1} thành công", objBANoitru.IdBa, objBANoitru.TenBenhnhan), objBANoitru.IsNew ? newaction.Insert : newaction.Update, "UI");
                    MessageBox.Show("Đã thêm mới Bệnh án thành công. Nhấn Ok để kết thúc");
                    cmdXoaBenhAn.Enabled = cmdPrint.Enabled = true;
                    if (_OnCreated != null) _OnCreated(objBANoitru.IdBa, objBANoitru.MaBa, action.Insert);
                    m_enAct = action.Update;
                }
                else if (m_enAct == action.Update)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật Bệnh án nội trú cho bệnh nhân: {0}-{1} thành công", objBANoitru.IdBa, objBANoitru.TenBenhnhan), objBANoitru.IsNew ? newaction.Insert : newaction.Update, "UI");
                    if (_OnCreated != null) _OnCreated(objBANoitru.IdBa, objBANoitru.MaBa, action.Update);
                    MessageBox.Show("Đã cập nhật Bệnh án thành công. Nhấn Ok để kết thúc");
                    m_enAct = action.Update;
                }
                EnableBA();
                //Utility.ShowMsg("Lưu thông tin thành công", "Thông báo");
                dtDataBA = SPs.BaNoitruLaythongtinIn(-1, "", objBenhnhan.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                _isSuccess = true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                if (objBANoitru != null && _isSuccess)
                {
                    new Update(KcbLuotkham.Schema)
                        .Set(KcbLuotkham.Columns.SoBenhAn).EqualTo(objBANoitru.MaBa)
                        .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                   // EmrThemBenhAn();
                }
              
            }
        }
        void EnableBA()
        {
            cboLoaiBA.Enabled = txtIDBenhAn.Enabled=cmdKhoitaoBA.Enabled= m_enAct == action.Insert;

        }
        //private void EmrThemBenhAn()
        //{
        //    if (_isSuccess)
        //    {
        //        var objDmucBenhan =
        //            new Select().From(LDmucBenhan.Schema)
        //                .Where(LDmucBenhan.Columns.MaBenhan)
        //                .IsEqualTo(BenhAn_DanhMuc.BenhAn_NoiKhoa)
        //                .ExecuteSingle<LDmucBenhan>();
        //        if (objDmucBenhan != null&& objLuotkham != null)
        //        {
        //            var objPatientHi = new EmrPatientHi();
        //            objPatientHi.IdBenhanHis = Utility.Int32Dbnull(objBANoitru.Id);
        //            objPatientHi.MaPhieuEmr = objDmucBenhan.MaPhieuEmr;
        //            objPatientHi.MaDmucBa = objDmucBenhan.MaBenhan;
        //            objPatientHi.MaLuotkham = objLuotkham.MaLuotkham;
        //            objPatientHi.IdBenhnhan = objLuotkham.IdBenhnhan;
        //            objPatientHi.EmrNo = objBANoitru.MaBaNoitru;
        //            ActionResult actionResult = EmrDocumentServices.ThemBenhAn(objPatientHi, action.Insert);
        //            switch (actionResult)
        //            {
        //                case ActionResult.Success: 
        //                    break;
        //            }
        //        }
        //    }
        //} 
        private BaNoitru TaoBANoitru()
        {
            if (objBANoitru == null) objBANoitru = new BaNoitru();
            try
            {
                int id = Utility.Int32Dbnull(txtIDBenhAn.Text, -1);
                if (objBANoitru.IdBa > 0)
                {
                    objBANoitru.IsLoaded = true;
                    objBANoitru.MarkOld();
                    objBANoitru.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                    objBANoitru.NguoiSua = globalVariables.UserName;
                }
                else
                {
                    SinhMaBenhAn();
                    objBANoitru.MaBa = Utility.sDbnull(txtMaBenhAn.Text);
                    objBANoitru.NguoiTao = globalVariables.UserName;
                    objBANoitru.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                }
                objBANoitru.NgaylamBa = dtpNgayBA.Value;
                objBANoitru.NgayTongKetBA = dtpNgayTKBA.Value;
                objBANoitru.LoaiBa = cboLoaiBA.SelectedValue.ToString();
                if (dtkhoanhapvienCoGiuong.Rows.Count > 0 && THU_VIEN_CHUNG.Laygiatrithamsohethong("BA_LAYKHOANOITRU_COGIUONG", "0", false) == "1")
                {
                    objBANoitru.Khoa = Utility.sDbnull(dtkhoanhapvienCoGiuong.Rows[0]["ten_khoanoitru"], "");
                    objBANoitru.Giuong = Utility.sDbnull(dtkhoanhapvienCoGiuong.Rows[0]["ten_giuong"], "");
                    objBANoitru.Phong = Utility.sDbnull(dtkhoanhapvienCoGiuong.Rows[0]["ten_buong"], "");
                }
                else if (dtkhoanhapvien.Rows.Count > 0)
                {
                    objBANoitru.Khoa = Utility.sDbnull(dtkhoanhapvien.Rows[0]["ten_khoanoitru"], "");
                    objBANoitru.Giuong = Utility.sDbnull(dtkhoanhapvien.Rows[0]["ten_giuong"], "");
                    objBANoitru.Phong = Utility.sDbnull(dtkhoanhapvien.Rows[0]["ten_buong"], "");
                }
                else
                {
                    //REM lại vì đây là khoa nhập viện hoặc khoa nhập viện có nằm giường
                    objBANoitru.Phong = ucThongtinnguoibenh_v31.txtBuong.Text;
                    objBANoitru.Khoa = ucThongtinnguoibenh_v31.txtKhoanoitru.Text;
                    objBANoitru.Giuong = ucThongtinnguoibenh_v31.txtGiuong.Text;
                }
                //objBANoitru.BenhNgoaiKhoa = Utility.sDbnull(txtBenhNgoai_Khoa.Text);
                objBANoitru.MaCoso = objLuotkham.MaCoso;
                objBANoitru.IdBenhnhan = objLuotkham.IdBenhnhan;
                objBANoitru.TenBenhnhan = objBenhnhan.TenBenhnhan;
                objBANoitru.MaLuotkham = objLuotkham.MaLuotkham;
                objBANoitru.NgaySinh = objBenhnhan.NgaySinh.Value.ToString("dd/MM/yyyy");
                objBANoitru.GioiTinh = objBenhnhan.IdGioitinh;
                objBANoitru.Tuoi =(byte) objBenhnhan.Tuoi;
                objBANoitru.MaDantoc = objBenhnhan.DanToc;
                objBANoitru.TenDantoc = objBenhnhan.TenDantoc;
                objBANoitru.MaCoquan = objBenhnhan.CoQuan;
                objBANoitru.TenCoquan = objBenhnhan.CoQuan;
                objBANoitru.ThongtinLhe = objBenhnhan.DiachiLienhe;
                objBANoitru.DthoaiLhe = objBenhnhan.DienthoaiLienhe;
                //objBANoitru.MaKhoaThien = txtMaKhoaThucHien.Text;
                objBANoitru.MaQuocgia = objBenhnhan.MaQuocgia;// Utility.Int16Dbnull(objBenhnhan.MaQuocgia != "" && objBenhnhan.MaQuocgia != "VN" ? 1 : 0);
                objBANoitru.TenQuocgia = objBenhnhan.TenQuocgia;
                objBANoitru.MaNghenghiep = objBenhnhan.NgheNghiep;
                objBANoitru.TenNghenghiep = objBenhnhan.TenNghenghiep;
                objBANoitru.MaKhoaravien = "";
                objBANoitru.TenKhoaravien = objBenhnhan.TenKhoanoitru;
                objBANoitru.IdKhoadieutri = objBenhnhan.IdKhoanoitru;
                objBANoitru.MaTinhtp = objBenhnhan.MaTinhtp;
                objBANoitru.MaQuanhuyen = objBenhnhan.MaQuanhuyen;
                objBANoitru.MaXaphuong = objBenhnhan.MaXaphuong;
                objBANoitru.MatheBhyt = objBenhnhan.MatheBhyt;
                objBANoitru.NgayHhlucThe = objBenhnhan.NgayketthucBhyt;
                objBANoitru.NgayVaoVien = dtQLNBVaoVien.Value != null? dtQLNBVaoVien.Value : (DateTime?)null;

                if (chkQLNBCapCuu.Checked) objBANoitru.QlnbTtiepVao = 1;
                else if (chkQLNBKKB.Checked)
                {
                    objBANoitru.QlnbTtiepVao = 2;
                }
                else if (chkQLNBKhoaDieuTri.Checked)
                {
                    objBANoitru.QlnbTtiepVao = 3;
                }
                else objBANoitru.QlnbTtiepVao = 0;
                if (chkQLNBCoQuanYTe.Checked) objBANoitru.QlnbNoigioithieu = 1;
                else if (chkQLNBTuDen.Checked)
                {
                    objBANoitru.QlnbNoigioithieu = 2;
                }
                else if (chkQLNBKhac.Checked)
                {
                    objBANoitru.QlnbNoigioithieu = 3;
                }
                else objBANoitru.QlnbNoigioithieu = 0;
                objBANoitru.QlnbVaovienLanthu = Utility.sDbnull(txtQLNBLanVaoVien.Text);

                if (chkQLNBTuyenTren.Checked) objBANoitru.QlnbChuyenvien = 1;
                else if (chkQLNBTuyenDuoi.Checked)
                {
                    objBANoitru.QlnbChuyenvien = 2;
                }
                else if (chkQLNBCK.Checked)
                {
                    objBANoitru.QlnbChuyenvien = 3;
                }
                else objBANoitru.QlnbChuyenvien = 0;

                objBANoitru.QlnbChuyenden = Utility.sDbnull(txtQLNBChuyenDen.Text);
                objBANoitru.QlnbRavien = Utility.sDbnull(dtQLNBRaVien.Text);
                if (chkQLNBRaVien.Checked) objBANoitru.QlnbLydoravien = 1;
                else if (chkQLNBXinVe.Checked)
                {
                    objBANoitru.QlnbLydoravien = 3;
                }
                else if (chkQLNBBoVe.Checked)
                {
                    objBANoitru.QlnbLydoravien = 4;
                }

                else if (chkQLNBDuaVe.Checked)
                {
                    objBANoitru.QlnbLydoravien = 5;
                }
                else objBANoitru.QlnbLydoravien = 0;
                objBANoitru.QlnbTenkhoaVao = lblqlbnKhoa.Text;
                objBANoitru.QlnbMakhoaVao = lblMakhoavao.Text;
                objBANoitru.QlnbTongsongayDieutri = Utility.Int16Dbnull(txtQLNBTongSoNgayDieuTri.Text);
                objBANoitru.CdNoiChuyenDen = Utility.sDbnull(txtQLNBChuyenDen.Text);
                objBANoitru.CdKkbCcuu = txtCDKKBCapCuu.Text;
                objBANoitru.CdMaKkbCcuu = txtCDMaKKBCapCuu.Text;
                objBANoitru.CdKhoaDtri = txtCDKhiVaoDieuTri.Text;
                objBANoitru.CdMaKhoaDtri = txtCDMaKhiVaoDieuTri.Text;
                objBANoitru.CdNoiChuyenDen = txtCDNoiChuyenDen.Text;
                objBANoitru.CdMaNoiChuyenDen = txtCDMaNoiChuyenDen.Text;
                objBANoitru.CdThuThuat = Utility.Int16Dbnull(chkCDThuThuat.Checked ? 1 : 0);
                objBANoitru.CdPhauThuat = Utility.Int16Dbnull(chkCDPhauThuat.Checked ? 1 : 0);
                objBANoitru.CdRvienBchinh = txtCDBenhChinh.Text;
                objBANoitru.CdMaRvienBchinh = txtCDMaBenhChinh.Text;
                objBANoitru.CdRvienBphu = txtCDBenhKemTheo.Text;
                objBANoitru.CdMaRvienBphu = txtCDMaBenhKemTheo.Text;
                objBANoitru.CdTaiBien = Utility.Int16Dbnull(chkCDTaiBien.Checked ? 1 : 0);
                objBANoitru.CdBienChung = Utility.Int16Dbnull(chkCDBienChung.Checked ? 1 : 0);
                if (chkTTRVKhoi.Checked) objBANoitru.TtrvKquaDtri = 1;
                else if (chkTTRVDoGiam.Checked)
                {
                    objBANoitru.TtrvKquaDtri = 2;
                }
                else if (chkTTRVKhongThayDoi.Checked)
                {
                    objBANoitru.TtrvKquaDtri = 3;
                }

                else if (chkTTRVNangHon.Checked)
                {
                    objBANoitru.TtrvKquaDtri = 4;
                }
                else if (chkTTRVTuVong.Checked)
                {
                    objBANoitru.TtrvKquaDtri = 5;
                }
                else objBANoitru.TtrvKquaDtri = 0;
                if (chkTTRVLanhTinh.Checked) objBANoitru.TtrvGphauBenh = 1;
                else if (chkTTRVNghiNgo.Checked)
                {
                    objBANoitru.TtrvGphauBenh = 2;
                }
                else if (chkTTRVAcTinh.Checked)
                {
                    objBANoitru.TtrvGphauBenh = 3;
                }
                else objBANoitru.TtrvGphauBenh = 0;
                objBANoitru.TtrvTVong = txtTTRVNgayTuVong.Text;
                if (chkttrvDoBenh.Checked) objBANoitru.TtrvLdoTvong = 1;
                else if (chkttrvTrong24GioVaoVien.Checked)
                {
                    objBANoitru.TtrvLdoTvong = 2;
                }
                else if (chkttrvDoTaiBien.Checked)
                {
                    objBANoitru.TtrvLdoTvong = 3;
                }
                else if (chkttrvSau24Gio.Checked)
                {
                    objBANoitru.TtrvLdoTvong = 4;
                }
                else if (chkttrvKhac.Checked)
                {
                    objBANoitru.TtrvLdoTvong = 5;
                }
                else objBANoitru.TtrvLdoTvong = 0;
                objBANoitru.TtrvNnhanTvong = txtTTRVNguyenNhanChinhTuVong.Text;
                objBANoitru.TtrvKhamNghiem = Utility.Int16Dbnull(chkTTRVKhamNgiemTuThi.Checked ? 1 : 0);
                objBANoitru.TtrvCdoanGphau = txtTTRVChuanDoanGiaiPhau.Text;
                objBANoitru.BaLdvv = txtBenhAnLyDoNhapVien.Text;
                objBANoitru.BaNgayThu = txtBenhAnVaoNgayThu.Text;
                objBANoitru.BaQtbl = txtBenhAnQuaTrinhBenhLy.Text;
                objBANoitru.BaTsb = txtBenhAnTienSuBenh.Text;
                objBANoitru.BaDiUng = Utility.Int16Dbnull(chkDiUng.Checked ? 1 : 0);
                objBANoitru.BaMaTuy = Utility.Int16Dbnull(chkMaTuy.Checked ? 1 : 0);
                objBANoitru.BaRuouBia = Utility.Int16Dbnull(chkRuouBia.Checked ? 1 : 0);
                objBANoitru.BaThuocLa = Utility.Int16Dbnull(chkThuocLa.Checked ? 1 : 0);
                objBANoitru.BaThuocLao = Utility.Int16Dbnull(chkThuocLao.Checked ? 1 : 0);
                objBANoitru.BaKhac = Utility.Int16Dbnull(chkKhac.Checked ? 1 : 0);
                objBANoitru.BaTgDiUng = txtDiUng.Text;
                objBANoitru.BaTgMaTuy = txtMaTuy.Text;
                objBANoitru.BaTgRuouBia = txtRuouBia.Text;
                objBANoitru.BaTgThuocLa = txtThuocLa.Text;
                objBANoitru.BaTgThuocLao = txtThuocLao.Text;
                objBANoitru.BaTgKhac = txtKhac.Text;
                objBANoitru.BaGiaDinh = txtBenhAnGiaDinh.Text;
                objBANoitru.KbToanThan = txtBenhAnToanThan.Text;
                objBANoitru.KbMach = txtMach.Text;
                objBANoitru.KbNhietDo = txtNhietDo.Text;
                objBANoitru.KbHuyetAp = txtha.Text;
                objBANoitru.KbNhipTho = txtNhipTho.Text;
                objBANoitru.KbCanNang = txtCanNang.Text;
                objBANoitru.KbChieuCao = txtChieuCao.Text;
                tinhBMI();
                objBANoitru.KbBMI = Utility.DecimaltoDbnull(txtBMI.Text, 0);
                objBANoitru.KbTuanHoan = txtBenhAnTuanHoan.Text;
                objBANoitru.KbHoHap = txtBenhAnHoHap.Text;
                objBANoitru.KbTieuHoa = txtBenhAnTieuHoa.Text;
                objBANoitru.KbThan = txtBenhAnThanTietNieuSinhDuc.Text;
                objBANoitru.KbThanKinh = txtBenhAnThanKinh.Text;
                objBANoitru.KbCo = txtBenhAnCoXuongKhop.Text;
                objBANoitru.KbTai = txtBenhAnTaiMuiHong.Text;
                objBANoitru.KbRang = txtBenhAnRangHamMat.Text;
                objBANoitru.KbMat = txtBenhAnMat.Text;
                objBANoitru.KbNoiTiet = txtBenhAnNoiTiet.Text;
                objBANoitru.KbXnCls = txtBenhAnCacXetNghiem.Text;
                objBANoitru.KbTtba = txtBenhAnTomTatBenhAn.Text;
                objBANoitru.KbBenhChinh = txtBenhAnBenhChinh.Text;
                objBANoitru.KbBenhPhu = txtBenhAnBenhKemTheo.Text;
                objBANoitru.KbPhanBiet = txtBenhAnPhanBiet.Text;
                objBANoitru.KbTienLuong = txtBenhAnTienLuong.Text;
                objBANoitru.KbHuongDtri = txtBenhAnHuongDieuTri.Text;
                objBANoitru.TkbaQtbl = txtTKBAQuaTrinhBenhLy.Text;
                objBANoitru.TkbaTtkqxn = txtTKBATTomTatKetQua.Text;
                objBANoitru.TkbaPpdt = txtTKBAPhuongPhapDieuTri.Text;
                objBANoitru.TkbaTtrv = txtTKBATinhTrangRaVien.Text;
                objBANoitru.TkbaHdt = txtTKBAHuongDieuTri.Text;

                objBANoitru.NguoiGiaoHoSo = txtNguoiGiaoHoSo.Text;
                objBANoitru.NguoiNhanHoSo = txtNguoiNhanHoSo.Text;
                objBANoitru.MaBacsilamBa = txtBSlamBA.MyCode;
                objBANoitru.TenBacsilamBa = txtBSlamBA.Text;
                objBANoitru.TenBSDieuTri = txtBSDieuTri.Text;
                objBANoitru.MaBSDieuTri = txtBSDieuTri.MyCode;
                objBANoitru.StCTScanner = Utility.Int16Dbnull(txtB_CTScanner.Text);
                objBANoitru.StXQuang = Utility.Int16Dbnull(txtB_Xquang.Text);
                objBANoitru.StSieuAm = Utility.Int16Dbnull(txtB_SieuAm.Text);
                objBANoitru.StXetNghiem = Utility.Int16Dbnull(txtB_XetNghiem.Text);
                objBANoitru.StKhac = Utility.Int16Dbnull(txtB_Khac.Text);
                objBANoitru.NgayTongKetBA = dtpB_NgayTongKet.Text != null
                    ? Convert.ToDateTime(dtpB_NgayTongKet.Text)
                    : (DateTime?)null;
                return objBANoitru;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
                return objBANoitru;

            }
        }

        private void frm_BenhAn_NoiKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                #region "Xử lý multiline"
                if (tabpageTo1.ActiveControl != null)
                {
                    Control ctr = tabpageTo1.ActiveControl;
                    if (ctr.GetType().Equals(typeof(EditBox)))
                    {
                        EditBox box = ctr as EditBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(TextBox)))
                    {
                        TextBox box = ctr as TextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(RichTextBox)))
                    {
                        RichTextBox box = ctr as RichTextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else
                        SendKeys.Send("{TAB}");
                }
                if (tabpageTo2.ActiveControl != null)
                {
                    Control ctr = tabpageTo2.ActiveControl;
                    if (ctr.GetType().Equals(typeof(EditBox)))
                    {
                        EditBox box = ctr as EditBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(TextBox)))
                    {
                        TextBox box = ctr as TextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(RichTextBox)))
                    {
                        RichTextBox box = ctr as RichTextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else
                        SendKeys.Send("{TAB}");
                }
                if (tabpageTo3.ActiveControl != null)
                {
                    Control ctr = tabpageTo3.ActiveControl;
                    if (ctr.GetType().Equals(typeof(EditBox)))
                    {
                        EditBox box = ctr as EditBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(TextBox)))
                    {
                        TextBox box = ctr as TextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(RichTextBox)))
                    {
                        RichTextBox box = ctr as RichTextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else
                        SendKeys.Send("{TAB}");
                }
                if (tabpageTo4.ActiveControl != null)
                {
                    Control ctr = tabpageTo4.ActiveControl;
                    if (ctr.GetType().Equals(typeof(EditBox)))
                    {
                        EditBox box = ctr as EditBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(TextBox)))
                    {
                        TextBox box = ctr as TextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(RichTextBox)))
                    {
                        RichTextBox box = ctr as RichTextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else
                        SendKeys.Send("{TAB}");
                }
                #endregion
                
            }
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
            //if (e.KeyCode == Keys.F4) cmdInBenhAn.PerformClick();
            if (e.KeyCode == Keys.Escape) Close();
            if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTab1.SelectedIndex = 0;
                ucThongtinnguoibenh_v31.txtMaluotkham.Focus();
            }
            else if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTab1.SelectedIndex = 1;
            }
            else if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTab1.SelectedIndex = 2;
            }
            else if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTab1.SelectedIndex = 3;
            }
        }
        public action m_enAct = action.Insert;
        private void frm_BenhAn_NoiKhoa_Load(object sender, EventArgs e)
        {
            try
            {
                dtpNgayBA.Value = dtpNgayTKBA.Value = DateTime.Now;
                txtBSDieuTri.Init(globalVariables.gv_dtDmucNhanvien,
                                             new List<string>
                                  {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                  });
                txtBSlamBA.Init(txtBSDieuTri.AutoCompleteSource, txtBSDieuTri.defaultItem);
                DataTable dtData =
                    new Select().From(DmucChung.Schema)
                        .Where(DmucChung.Columns.Loai).IsEqualTo("EMR_LOAIBA")
                        .And(DmucChung.Columns.TrangThai).IsEqualTo(1)
                        .And(DmucChung.Columns.Ma).In(lstLoaiBA.Split(',').ToList<string>())
                        .OrderAsc(DmucChung.Columns.SttHthi)
                        .ExecuteDataSet().Tables[0];
                DataBinding.BindDataCombobox(cboLoaiBA, dtData, "MA", "TEN", "---Chọn loại BA---", true);
                txtBenhAnLyDoNhapVien.Init();
                if (m_enAct == action.Insert)
                {
                    ucThongtinnguoibenh_v31.Refresh();
                }
                else
                {
                    objLuotkham = Utility.getKcbLuotkham(objBANoitru.IdBenhnhan, objBANoitru.MaLuotkham);
                    objBenhnhan = Utility.getKcbBenhnhan(objBANoitru.IdBenhnhan, objBANoitru.MaLuotkham);
                    FillData4Update();

                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommand();
            }
            
        }
        private void GetThongtinChuyenVien()
        {

            KcbPhieuchuyenvien pcv = new Select().From(KcbPhieuchuyenvien.Schema)
                .Where(KcbPhieuchuyenvien.Columns.IdBenhnhan).IsEqualTo(objBenhnhan.IdBenhnhan)
                .And(KcbPhieuchuyenvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<KcbPhieuchuyenvien>();
            if (pcv != null)
            {
                DmucBenhvien objBV = DmucBenhvien.FetchByID(pcv.IdBenhvienChuyenden);
                if (objBV != null)
                {
                    txtCDNoiChuyenDen.Text = objBV.TenBenhvien;
                   
                }
                chkQLNBCK.Checked = Utility.ByteDbnull(pcv.TuyenChuyen, 1) == 3;
                chkQLNBTuyenDuoi.Checked = Utility.ByteDbnull(pcv.TuyenChuyen, 1) == 2;
                chkQLNBTuyenTren.Checked = Utility.ByteDbnull(pcv.TuyenChuyen, 1) == 1;
            }
        }

        string ICD_Khoa_NoITru = "";
        string Name_Khoa_NoITru = "";
        DataTable dtkhoachuyen = new DataTable();
        DataTable dtkhoanhapvien = new DataTable();
        DataTable dtkhoanhapvienCoGiuong = new DataTable();
        DataTable dtCacKhoa = new DataTable();
        void FillData4Update()
        {
            try
            {
                maBA = "";
                SqlQuery sqlQuery = new Select().From<BaNoitru>()
                    .Where(BaNoitru.Columns.MaLuotkham)
                    .IsEqualTo(objLuotkham.MaLuotkham)
                    .And(BaNoitru.Columns.IdBenhnhan)
                    .IsEqualTo(Utility.Int32Dbnull(objLuotkham.IdBenhnhan));
                if (objBANoitru == null || (objBANoitru.IdBenhnhan != objLuotkham.IdBenhnhan && objBANoitru.MaLuotkham != objLuotkham.MaLuotkham))
                    objBANoitru = sqlQuery.ExecuteSingle<BaNoitru>();
                //Autofill Data

                dtCacKhoa = new KCB_THAMKHAM().NoitruTimkiemlichsuBuonggiuong(objLuotkham.MaLuotkham, objBenhnhan.IdBenhnhan, "-1", -1);
                dtkhoachuyen = dtCacKhoa.Clone();
                DataRow[] arrKhoachuyen = dtCacKhoa.Select("id_chuyen>0");
                if (arrKhoachuyen.Length > 0) dtkhoachuyen = arrKhoachuyen.CopyToDataTable();
                grdQLNBKhoa.DataSource = dtkhoachuyen;
                DataRow[] arrKhoanhapvien = dtCacKhoa.Select("id_chuyen<=0");
                NoitruPhieunhapvien objNhapvien = new Select().From(NoitruPhieunhapvien.Schema)
                    .Where(NoitruPhieunhapvien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(NoitruPhieunhapvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieunhapvien>();

                if (arrKhoanhapvien.Length > 0)
                {
                    dtkhoanhapvien = arrKhoanhapvien.CopyToDataTable();
                    lblMakhoavao.Text = Utility.sDbnull(arrKhoanhapvien[0]["ma_khoanoitru"], "");
                    lblqlbnKhoa.Text = Utility.sDbnull(arrKhoanhapvien[0]["ten_khoanoitru"], "");
                }
                var q = from p in dtCacKhoa.AsEnumerable()
                        where Utility.Int32Dbnull(p["id_giuong"], 0) > 0
                        orderby p["ngay_vaokhoa"] ascending
                        select p;
                if (q.Any())
                    dtkhoanhapvienCoGiuong = q.CopyToDataTable();
                if (objBenhnhan.NgayNhapvien.HasValue)
                    dtQLNBVaoVien.Value = objBenhnhan.NgayNhapvien.Value;
                if (objBenhnhan.NgayRavien.HasValue)
                    dtQLNBRaVien.Text = objBenhnhan.NgayRavien.Value.ToString("dd/MM/yyyy HH:mm:ss");

                txtQLNBTongSoNgayDieuTri.Text = Utility.sDbnull(objLuotkham.SongayDieutri);
                GetChanDoanNoitru();
                GetChanDoanRaVien();
                GetThongtinChuyenVien();
                DongbothongtinTKBA();
                txtCDKhiVaoDieuTri.Text = Name_Khoa_NoITru;
                txtCDMaKhiVaoDieuTri.Text = ICD_Khoa_NoITru;
                
                if (objBANoitru != null)
                {
                    m_enAct = action.Update;
                    cboLoaiBA.SelectedIndex = Utility.GetSelectedIndex(cboLoaiBA, objBANoitru.LoaiBa);
                    maBA = objBANoitru.MaBa;
                    dtDataBA = SPs.BaNoitruLaythongtin(-1, "", objBenhnhan.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                    DataRow dr = dtDataBA.Rows[0];
                    try
                    {
                        txtIDBenhAn.Text = Utility.sDbnull(objBANoitru.IdBa);
                        txtMaBenhAn.Text = Utility.sDbnull(objBANoitru.MaBa);
                        //txtBenhNgoai_Khoa.Text = Utility.sDbnull(objBANoitru.BenhNgoaiKhoa);
                        dtQLNBRaVien.Text = Utility.sDbnull(objBANoitru.QlnbRavien);
                        string ICD_chinh_Name = "";
                        string ICD_chinh_Code = "";
                        string ICD_Phu_Name = "";
                        string ICD_Phu_Code = "";

                        GetChanDoanChinhPhu(objLuotkham.MabenhChinh,
                                            objLuotkham.MabenhPhu,
                                            ref ICD_chinh_Name,
                                            ref ICD_chinh_Code, ref ICD_Phu_Name,
                                            ref ICD_Phu_Code);

                        txtCDKKBCapCuu.Text = ICD_chinh_Name + ICD_Phu_Name;
                        txtCDMaKKBCapCuu.Text = ICD_chinh_Code + ICD_Phu_Code;

                        chkQLNBTuDen.Checked = Utility.Int16Dbnull(objBANoitru.QlnbTtiepVao) == 1;
                        chkQLNBKKB.Checked = Utility.Int16Dbnull(objBANoitru.QlnbTtiepVao) == 2;
                        chkQLNBKhoaDieuTri.Checked = Utility.Int16Dbnull(objBANoitru.QlnbTtiepVao) == 3;
                        chkQLNBCoQuanYTe.Checked = Utility.Int16Dbnull(objBANoitru.QlnbNoigioithieu) == 1;
                        chkQLNBTuDen.Checked = Utility.Int16Dbnull(objBANoitru.QlnbNoigioithieu) == 2;
                        chkQLNBKhac.Checked = Utility.Int16Dbnull(objBANoitru.QlnbNoigioithieu) == 3;
                        txtQLNBLanVaoVien.Text = Utility.sDbnull(objBANoitru.QlnbVaovienLanthu);
                        lblqlbnKhoa.Text = objBANoitru.QlnbTenkhoaVao;
                        chkQLNBTuyenTren.Checked = Utility.Int16Dbnull(objBANoitru.QlnbChuyenvien) == 1;
                        chkQLNBTuyenDuoi.Checked = Utility.Int16Dbnull(objBANoitru.QlnbChuyenvien) == 2;
                        chkQLNBCK.Checked = Utility.Int16Dbnull(objBANoitru.QlnbChuyenvien) == 3;
                        txtQLNBChuyenDen.Text = Utility.sDbnull(objBANoitru.QlnbChuyenden);
                        dtQLNBRaVien.Text = Utility.sDbnull(objBANoitru.QlnbRavien);// dr["QlnbRaVien"].ToString();
                        chkQLNBRaVien.Checked = Utility.Int16Dbnull(objBANoitru.QlnbLydoravien) == 1;
                        chkQLNBXinVe.Checked = Utility.Int16Dbnull(objBANoitru.QlnbLydoravien) == 3;
                        chkQLNBBoVe.Checked = Utility.Int16Dbnull(objBANoitru.QlnbLydoravien) == 4;
                        chkQLNBDuaVe.Checked = Utility.Int16Dbnull(objBANoitru.QlnbLydoravien) == 5;
                        txtQLNBTongSoNgayDieuTri.Text = Utility.sDbnull(objBANoitru.QlnbTongsongayDieutri);
                        txtCDNoiChuyenDen.Text = Utility.sDbnull(objBANoitru.CdNoiChuyenDen);
                        txtCDMaNoiChuyenDen.Text = Utility.sDbnull(objBANoitru.CdMaNoiChuyenDen);
                        txtCDKKBCapCuu.Text = Utility.sDbnull(objBANoitru.CdKkbCcuu);
                        txtCDMaKKBCapCuu.Text = Utility.sDbnull(objBANoitru.CdMaKkbCcuu);
                        txtCDKhiVaoDieuTri.Text = Utility.sDbnull(objBANoitru.CdKhoaDtri);
                        txtCDMaKhiVaoDieuTri.Text = Utility.sDbnull(objBANoitru.CdMaKhoaDtri);
                        txtCDBenhChinh.Text = Utility.sDbnull(objBANoitru.CdRvienBchinh);
                        txtCDMaBenhChinh.Text = Utility.sDbnull(objBANoitru.CdMaRvienBchinh);
                        txtCDBenhKemTheo.Text = Utility.sDbnull(objBANoitru.CdRvienBphu);
                        txtCDMaBenhKemTheo.Text = Utility.sDbnull(objBANoitru.CdMaRvienBphu);
                        chkCDThuThuat.Checked = Utility.Int16Dbnull(objBANoitru.CdThuThuat) == 1;
                        chkCDPhauThuat.Checked = Utility.Int16Dbnull(objBANoitru.CdPhauThuat) == 1;
                        chkCDTaiBien.Checked = Utility.Int16Dbnull(objBANoitru.CdTaiBien) == 1;
                        chkCDBienChung.Checked = Utility.Int16Dbnull(objBANoitru.CdBienChung) == 1;
                        chkTTRVKhoi.Checked = Utility.Int16Dbnull(objBANoitru.TtrvKquaDtri) == 1;
                        chkTTRVDoGiam.Checked = Utility.Int16Dbnull(objBANoitru.TtrvKquaDtri) == 2;
                        chkTTRVKhongThayDoi.Checked = Utility.Int16Dbnull(objBANoitru.TtrvKquaDtri) == 3;
                        chkTTRVNangHon.Checked = Utility.Int16Dbnull(objBANoitru.TtrvKquaDtri) == 4;
                        chkTTRVTuVong.Checked = Utility.Int16Dbnull(objBANoitru.TtrvKquaDtri) == 5;
                        chkTTRVLanhTinh.Checked = Utility.Int16Dbnull(objBANoitru.TtrvGphauBenh) == 1;
                        chkTTRVNghiNgo.Checked = Utility.Int16Dbnull(objBANoitru.TtrvGphauBenh) == 2;
                        chkTTRVAcTinh.Checked = Utility.Int16Dbnull(objBANoitru.TtrvGphauBenh) == 3;
                        txtTTRVNgayTuVong.Text = Utility.sDbnull(objBANoitru.TtrvTVong); //Utility.sDbnull(dr["TtrvTVong"].ToString());
                        chkttrvDoBenh.Checked = Utility.Int16Dbnull(objBANoitru.TtrvLdoTvong) == 1;
                        chkttrvTrong24GioVaoVien.Checked = Utility.Int16Dbnull(objBANoitru.TtrvLdoTvong) == 2;
                        chkttrvDoTaiBien.Checked = Utility.Int16Dbnull(objBANoitru.TtrvLdoTvong) == 3;
                        chkttrvSau24Gio.Checked = Utility.Int16Dbnull(objBANoitru.TtrvLdoTvong) == 4;
                        chkttrvKhac.Checked = Utility.Int16Dbnull(objBANoitru.TtrvLdoTvong) == 5;
                        txtTTRVNguyenNhanChinhTuVong.Text = Utility.sDbnull(objBANoitru.TtrvNnhanTvong);
                        chkTTRVKhamNgiemTuThi.Checked = Utility.Int16Dbnull(objBANoitru.TtrvKhamNghiem) == 1;
                        txtTTRVChuanDoanGiaiPhau.Text = Utility.sDbnull(objBANoitru.TtrvCdoanGphau);
                        txtBenhAnLyDoNhapVien._Text = Utility.sDbnull(objBANoitru.BaLdvv);// Utility.sDbnull(dr["BaLdvv"].ToString());
                        txtBenhAnVaoNgayThu.Text = Utility.sDbnull(objBANoitru.BaNgayThu);
                        txtBenhAnQuaTrinhBenhLy.Text = Utility.sDbnull(objBANoitru.BaQtbl);// Utility.sDbnull(dr["BaQtbl"].ToString());
                        txtBenhAnTienSuBenh.Text = Utility.sDbnull(objBANoitru.BaTsb);
                        chkDiUng.Checked = Utility.Int16Dbnull(objBANoitru.BaDiUng) == 1;
                        chkMaTuy.Checked = Utility.Int16Dbnull(objBANoitru.BaMaTuy) == 1;
                        chkRuouBia.Checked = Utility.Int16Dbnull(objBANoitru.BaRuouBia) == 1;
                        chkThuocLa.Checked = Utility.Int16Dbnull(objBANoitru.BaThuocLa) == 1;
                        chkThuocLao.Checked = Utility.Int16Dbnull(objBANoitru.BaThuocLao) == 1;
                        chkKhac.Checked = Utility.Int16Dbnull(objBANoitru.BaKhac) == 1;
                        txtDiUng.Text = Utility.sDbnull(objBANoitru.BaTgDiUng);
                        txtMaTuy.Text = Utility.sDbnull(objBANoitru.BaTgMaTuy);
                        txtRuouBia.Text = Utility.sDbnull(objBANoitru.BaTgRuouBia);
                        txtThuocLa.Text = Utility.sDbnull(objBANoitru.BaTgThuocLa);
                        txtThuocLao.Text = Utility.sDbnull(objBANoitru.BaTgThuocLao);
                        txtKhac.Text = Utility.sDbnull(objBANoitru.BaTgKhac);
                        txtBenhAnGiaDinh.Text = Utility.sDbnull(objBANoitru.BaGiaDinh);// Utility.sDbnull(dr["BaGiaDinh"].ToString());
                        txtBenhAnToanThan.Text = Utility.sDbnull(objBANoitru.KbToanThan);// Utility.sDbnull(dr["KbToanThan"].ToString());
                        txtMach.Text = Utility.sDbnull(objBANoitru.KbMach);
                        txtNhietDo.Text = Utility.sDbnull(objBANoitru.KbNhietDo);
                        txtha.Text = Utility.sDbnull(objBANoitru.KbHuyetAp);
                        txtNhipTho.Text = Utility.sDbnull(objBANoitru.KbNhipTho);
                        txtCanNang.Text = Utility.sDbnull(objBANoitru.KbCanNang);
                        txtChieuCao.Text = Utility.sDbnull(objBANoitru.KbChieuCao);
                        tinhBMI();
                        txtBenhAnTuanHoan.Text = Utility.sDbnull(objBANoitru.KbTuanHoan);
                        txtBenhAnHoHap.Text = Utility.sDbnull(objBANoitru.KbHoHap);
                        txtBenhAnTieuHoa.Text = Utility.sDbnull(objBANoitru.KbTieuHoa);
                        txtBenhAnThanTietNieuSinhDuc.Text = Utility.sDbnull(objBANoitru.KbThan);
                        txtBenhAnThanKinh.Text = Utility.sDbnull(objBANoitru.KbThanKinh);
                        txtBenhAnCoXuongKhop.Text = Utility.sDbnull(objBANoitru.KbCo);
                        txtBenhAnTaiMuiHong.Text = Utility.sDbnull(objBANoitru.KbTai);
                        txtBenhAnRangHamMat.Text = Utility.sDbnull(objBANoitru.KbRang);
                        txtBenhAnMat.Text = Utility.sDbnull(objBANoitru.KbMat);
                        txtBenhAnNoiTiet.Text = Utility.sDbnull(objBANoitru.KbNoiTiet);
                        txtBenhAnCacXetNghiem.Text = Utility.sDbnull(objBANoitru.KbXnCls);
                        txtBenhAnTomTatBenhAn.Text = Utility.sDbnull(objBANoitru.KbTtba);
                        txtBenhAnBenhChinh.Text = Utility.sDbnull(objBANoitru.KbBenhChinh);
                        txtBenhAnBenhKemTheo.Text = Utility.sDbnull(objBANoitru.KbBenhPhu);
                        txtBenhAnPhanBiet.Text = Utility.sDbnull(objBANoitru.KbPhanBiet);
                        txtBenhAnTienLuong.Text = Utility.sDbnull(objBANoitru.KbTienLuong);
                        txtBenhAnHuongDieuTri.Text = Utility.sDbnull(objBANoitru.KbHuongDtri);
                        txtTKBAQuaTrinhBenhLy.Text = Utility.sDbnull(objBANoitru.TkbaQtbl);
                        txtTKBATTomTatKetQua.Text = Utility.sDbnull(objBANoitru.TkbaTtkqxn);
                        txtTKBAPhuongPhapDieuTri.Text = Utility.sDbnull(objBANoitru.TkbaPpdt);
                        txtTKBATinhTrangRaVien.Text = Utility.sDbnull(objBANoitru.TkbaTtrv);// Utility.sDbnull(dr["TkbaTtrv"].ToString());
                        txtTKBAHuongDieuTri.Text = Utility.sDbnull(objBANoitru.TkbaHdt);// Utility.sDbnull(dr["TkbaHdt"].ToString());

                        txtNguoiGiaoHoSo.Text = Utility.sDbnull(objBANoitru.NguoiGiaoHoSo);
                        txtNguoiNhanHoSo.Text = Utility.sDbnull(objBANoitru.NguoiNhanHoSo);
                        txtBSDieuTri.SetCode( Utility.sDbnull(objBANoitru.MaBSDieuTri));
                        txtBSlamBA.SetCode(Utility.sDbnull(objBANoitru.MaBacsilamBa));
                        txtB_CTScanner.Text = Utility.sDbnull(objBANoitru.StCTScanner);
                        txtB_Xquang.Text = Utility.sDbnull(objBANoitru.StXQuang);
                        txtB_SieuAm.Text = Utility.sDbnull(objBANoitru.StSieuAm);
                        txtB_XetNghiem.Text = Utility.sDbnull(objBANoitru.StXetNghiem);
                        txtB_Khac.Text = Utility.sDbnull(objBANoitru.StKhac);
                        if (objBANoitru.NgayTongKetBA.HasValue)
                            dtpB_NgayTongKet.Value = objBANoitru.NgayTongKetBA.Value;
                        else
                            dtpB_NgayTongKet.Value = DateTime.Now;
                    }
                    catch (Exception ex)
                    {
                        Utility.ShowMsg(ex.ToString());
                    }
                }
                else//Auto fill
                {
                    //Điền các thông tin mặc định người bệnh
                    //Trang 1
                    m_enAct = action.Insert;

                    //Trang 2
                    if (objNhapvien != null)
                    {
                        txtBenhAnLyDoNhapVien.SetCode(Utility.sDbnull(objNhapvien.LydoNhapvien));
                        txtBenhAnTienSuBenh.Text = Utility.sDbnull(objNhapvien.TsuBanthan);
                        txtBenhAnGiaDinh.Text = Utility.sDbnull(objNhapvien.TsuGiadinh);
                        txtBenhAnQuaTrinhBenhLy.Text = Utility.sDbnull(objNhapvien.QuatrinhBenhly);
                        txtBenhAnToanThan.Text = Utility.sDbnull(objNhapvien.KhamToanthan);
                    }
                    //Trang 3
                    //Trang 4
                    //GetChanDoanKKB();
                    txtCDKKBCapCuu.Text = Get_ChanDoan_KKB_CapCuu();
                    txtCDMaKKBCapCuu.Text = Utility.sDbnull(objLuotkham.MabenhChinh, string.Empty);
                    KcbThongtinchung tef = new Select().From(KcbThongtinchung.Schema)
                        .Where(KcbThongtinchung.Columns.IdBenhnhan).IsEqualTo(objBenhnhan.IdBenhnhan)
                        .And(KcbThongtinchung.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<KcbThongtinchung>();
                    if (tef != null)
                    {
                        txtMach.Text = Utility.sDbnull(tef.Mach);
                        txtNhietDo.Text = Utility.sDbnull(tef.Nhietdo);
                        txtha.Text = Utility.sDbnull(tef.Huyetap);
                        txtNhipTho.Text = Utility.sDbnull(tef.Nhiptho);
                        txtCanNang.Text = Utility.sDbnull(tef.Cannang);
                        txtChieuCao.Text = Utility.sDbnull(tef.Chieucao);
                        tinhBMI();
                    }

                    var dmucChung = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("BENHAN_KHAMBENH").ExecuteAsCollection<DmucChungCollection>();
                    if (dmucChung != null && dmucChung.Count > 0)
                    {
                        if (Utility.DoTrim(txtBenhAnToanThan.Text).Length <= 0)
                            txtBenhAnToanThan.Text = Utility.sDbnull(dmucChung.Where(x => x.Ma == "TOANTHAN").FirstOrDefault().Ten, string.Empty);
                        txtBenhAnTuanHoan.Text = Utility.sDbnull(dmucChung.Where(x => x.Ma == "TUANHOAN").FirstOrDefault().Ten, string.Empty);
                        txtBenhAnHoHap.Text = Utility.sDbnull(dmucChung.Where(x => x.Ma == "HOHAP").FirstOrDefault().Ten, string.Empty);
                        txtBenhAnTieuHoa.Text = Utility.sDbnull(dmucChung.Where(x => x.Ma == "TIEUHOA").FirstOrDefault().Ten, string.Empty);
                        txtBenhAnThanTietNieuSinhDuc.Text = Utility.sDbnull(dmucChung.Where(x => x.Ma == "THAN_TIETNIEU").FirstOrDefault().Ten, string.Empty);
                        txtBenhAnThanKinh.Text = Utility.sDbnull(dmucChung.Where(x => x.Ma == "THANKINH").FirstOrDefault().Ten, string.Empty);
                        txtBenhAnCoXuongKhop.Text = Utility.sDbnull(dmucChung.Where(x => x.Ma == "COXUONGKHOP").FirstOrDefault().Ten, string.Empty);
                        txtBenhAnTaiMuiHong.Text = Utility.sDbnull(dmucChung.Where(x => x.Ma == "TAIMUIHONG").FirstOrDefault().Ten, string.Empty);
                        txtBenhAnRangHamMat.Text = Utility.sDbnull(dmucChung.Where(x => x.Ma == "RANGHAMMAT").FirstOrDefault().Ten, string.Empty);
                        txtBenhAnMat.Text = Utility.sDbnull(dmucChung.Where(x => x.Ma == "MAT").FirstOrDefault().Ten, string.Empty);
                        txtBenhAnNoiTiet.Text = Utility.sDbnull(dmucChung.Where(x => x.Ma == "NOITIET").FirstOrDefault().Ten, string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                EnableBA();
            }
          

        }
        VKcbLuotkham objBenhnhan = null;
        KcbLuotkham objLuotkham = null;
        private void SinhMaBenhAn()
        {
            //txtMaBenhAn.Text = THU_VIEN_CHUNG.SinhMaBenhAn_NoiTru();
            string MaxMaBenhAN = "";
            StoredProcedure sp = SPs.BaSinhMaBA(cboLoaiBA.SelectedValue.ToString(), MaxMaBenhAN);
            sp.Execute();
            sp.OutputValues.ForEach(delegate(object objOutput) { MaxMaBenhAN = (String)objOutput; });

            txtMaBenhAn.Text = MaxMaBenhAN;

        }
        void ModifyCommand()
        {
            tabpageTo2.Enabled = tabpageTo3.Enabled = tabpageTo4.Enabled = objLuotkham != null;
            btnInto2.Enabled = btnInto3.Enabled = Into1.Enabled = btnInto4.Enabled = button1.Enabled = btnInVoBA.Enabled = objLuotkham != null && objBANoitru!=null;
            cmdXoaBenhAn.Enabled = objLuotkham != null && objBANoitru != null;
        }

        private void txtMaLanKham_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    objLuotkham.MaLuotkham = THU_VIEN_CHUNG.SinhMaHoSoKhiTimKiem(objLuotkham.MaLuotkham);
            //    if (!KiemTraThongTin()) return;
            //    FillBenhAnByPatientCode();
            //}
        }
       
        private DataTable getChitietCLS()
        {
            int status = 0;
            DataTable temdt = SPs.ClsKetQuaXetNghiem(-1,"",objLuotkham.MaLuotkham, objBenhnhan.IdBenhnhan, 1, status).GetDataSet().Tables[0];

            return temdt;
        }

        private void cmdXoaBenhAn_Click(object sender, EventArgs e)
        {
            try
            {
               
                objBANoitru = BaNoitru.FetchByID(Utility.Int64Dbnull( txtIDBenhAn.Text));
                if (objBANoitru == null)
                {
                    Utility.ShowMsg("Bạn chưa chọn bệnh án nào để xóa hoặc bệnh án muốn xóa không tồn tại trong hệ thống. Vui lòng gõ lại mã lượt khám để kiểm tra");
                    ucThongtinnguoibenh_v31.txtMaluotkham.Focus();
                    ucThongtinnguoibenh_v31.txtMaluotkham.SelectAll();
                    return;
                }
                BaHosoluutru hosoba = new Select().From(BaHosoluutru.Schema)
                    .Where(BaHosoluutru.Columns.IdBa).IsEqualTo(objBANoitru.IdBa)
                    .And(BaHosoluutru.Columns.MaBa).IsEqualTo(objBANoitru.MaBa)
                    .And(BaHosoluutru.Columns.LoaiBa).IsEqualTo(objBANoitru.LoaiBa)
                    .And(BaHosoluutru.Columns.IdBenhnhan).IsEqualTo(objBANoitru.IdBenhnhan)
                     .And(BaHosoluutru.Columns.MaLuotkham).IsEqualTo(objBANoitru.MaLuotkham)
                    .ExecuteSingle<BaHosoluutru>();
                if (Utility.Int32Dbnull( hosoba.TrangThai,0) == 1)
                {
                    Utility.ShowMsg("Bệnh án đang ở trạng thái đóng nên không thể xóa. Muốn xóa cần quay về trạng thái mở");
                    return;
                }
                if (Utility.Int32Dbnull(hosoba.TrangThai, 0) == 2)
                {
                    Utility.ShowMsg("Bệnh án đang ở trạng thái đóng và đã gửi KHTH phê duyệt nên không thể xóa");
                    return;
                }
                if (Utility.Int32Dbnull(hosoba.TrangThai, 0) == 3)
                {
                    Utility.ShowMsg("Bệnh án đang ở trạng thái đã được duyệt bởi KHTH và đưa vào lưu trữ nên không thể xóa");
                    return;
                }
                if (objBANoitru != null && (Utility.Coquyen("kcb_banoitru_xoa") || globalVariables.UserName == objBANoitru.NguoiTao))
                {
                    if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin bệnh án ngoại khoa đi không ?", "Thông báo", true))
                    {
                        using (var Scope = new TransactionScope())
                        {
                            using (var dbScope = new SharedDbConnectionScope())
                            {
                               new Delete().From(BaNoitru.Schema)
                                     .Where(BaNoitru.Columns.IdBa).IsEqualTo(objBANoitru.IdBa)
                                     .And(BaNoitru.Columns.LoaiBa).IsEqualTo(objBANoitru.LoaiBa)
                                     .And(BaNoitru.Columns.MaCoso).IsEqualTo(objBANoitru.MaCoso)
                                     .Execute();
                              new Delete().From(BaHosoluutru.Schema)
                                    .Where(BaHosoluutru.Columns.IdBa).IsEqualTo(objBANoitru.IdBa)
                                    .And(BaHosoluutru.Columns.LoaiBa).IsEqualTo(objBANoitru.LoaiBa)
                                    .And(BaNoitru.Columns.MaCoso).IsEqualTo(objBANoitru.MaCoso)
                                    .Execute();
                              Utility.Log("frm_BenhAn_NoiKhoa", globalVariables.UserName, string.Format("Xóa bệnh án id={0}, loại BA={1}, mã BA={2} của người bệnh id ={3}, mã lần khám {4} thành công",objBANoitru.IdBa,objBANoitru.LoaiBa,objBANoitru.MaBa,objBANoitru.IdBenhnhan, objBANoitru.MaLuotkham), newaction.Delete, "UI");
                            }
                            Scope.Complete();
                        }
                       
                            Utility.ShowMsg("Bạn xóa bệnh án thành công", "Thông báo");
                            ucThongtinnguoibenh_v31.Refresh();
                            ModifyCommand();
                            
                       

                    }
                }
                else
                {
                    Utility.ShowMsg("Bạn không có quyền xóa BA(kcb_banoitru_xoa) hoặc không phải là người tạo Bệnh án");
                    return;

                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
         
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            //var frm = new frm_TimKiem_BN();
            //frm.ShowDialog();
            //if (frm.b_Cancel)
            //{
            //    objLuotkham.MaLuotkham = Utility.sDbnull(frm.SoHSBA);
            //    FillBenhAnByPatientCode();
            //}
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Into1_Click(object sender, EventArgs e)
        {

            DataTable sub_dtData = new KCB_THAMKHAM().NoitruTimkiemlichsuBuonggiuong(objLuotkham.MaLuotkham, objBenhnhan.IdBenhnhan, "-1",-1);
            string reportCode = "BA_NOITRU_TO1";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            THU_VIEN_CHUNG.CreateXML(sub_dtData,  "BA_noitru_khoachuyen.XML");
            noitru_inphieu.BA_noitru_Into1(dtDataBA, sub_dtData, true, reportCode, "");
        }

        private void btnInto2_Click(object sender, EventArgs e)
        {
            string reportCode = "BA_NOITRU_TO2";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            noitru_inphieu.BA_noitru_Into234_voba_tkba(dtDataBA, true, reportCode, "");
        }

        private void btnInto3_Click(object sender, EventArgs e)
        {
            string reportCode = "BA_NOITRU_TO3";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            noitru_inphieu.BA_noitru_Into234_voba_tkba(dtDataBA, true, reportCode, "");
        }

        private void btnInto4_Click(object sender, EventArgs e)
        {
            string reportCode = "BA_NOITRU_TO4";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            noitru_inphieu.BA_noitru_Into234_voba_tkba(dtDataBA, true, reportCode, "");
        }

        private void btnInVoBA_Click(object sender, EventArgs e)
        {
            string reportCode = "BA_NOITRU_VOBA";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            noitru_inphieu.BA_noitru_Into234_voba_tkba(dtDataBA, true, reportCode, "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string reportCode = "BA_noitru_tongketBA";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            noitru_inphieu.BA_noitru_Into234_voba_tkba(dtDataBA, true, reportCode, "");
        }

        private void cmdUpdateBNToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //if (objLuotkham != null)
            //{
            //    var frm = new frm_Update_BN();
            //    frm.objLuotkham = objLuotkham;
            //    frm.ShowDialog();
            //    SqlQuery sql = new Select().From<KcbLuotkham>().Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham);
            //    objLuotkham = sql.ExecuteSingle<KcbLuotkham>();
            //    FillBNById(Utility.Int32Dbnull(objLuotkham.IdBenhnhan));
            //    FillLanKhamById(objLuotkham);
            //}
        }

        private void cmdLamMoi_Click(object sender, EventArgs e)
        {
            ClearControl();
            objBANoitru = null;
            objBenhnhan = null;
            objLuotkham = null;
            m_enAct = action.Insert;
            ucThongtinnguoibenh_v31.txtMaluotkham.Focus();
            ucThongtinnguoibenh_v31.txtMaluotkham.SelectAll();
            ModifyCommand();
        }

        bool _isCounterpart = false; //mục đích dùng để ktra xem quá tình bệnh lý ở tổng kết bệnh án đã chỉnh sửa chưa, nếu chỉnh sửa rồi thì ko cập nhật lại
        private void txtBenhAnQuaTrinhBenhLy_Enter(object sender, EventArgs e)
        {
            //_isCounterpart = txtBenhAnQuaTrinhBenhLy.Text.Trim() == txtTKBAQuaTrinhBenhLy.Text.Trim();
        }

        private void txtBenhAnQuaTrinhBenhLy_TextChanged(object sender, EventArgs e)
        {
            //if (_isCounterpart)
            //{
            //    txtTKBAQuaTrinhBenhLy.Text = txtBenhAnQuaTrinhBenhLy.Text;
            //}
        }

        private void txtBenhAnCacXetNghiem_Enter(object sender, EventArgs e)
        {
            _isCounterpart = txtBenhAnCacXetNghiem.Text.Trim() == txtTKBATTomTatKetQua.Text.Trim();
        }

        private void txtBenhAnCacXetNghiem_TextChanged(object sender, EventArgs e)
        {
            if (_isCounterpart)
            {
                txtTKBATTomTatKetQua.Text = txtBenhAnCacXetNghiem.Text;
            }
        }

        private void txtCDKhiVaoDieuTri_TextChanged(object sender, EventArgs e)
        {
            txtBenhAnBenhChinh.Text = txtCDKhiVaoDieuTri.Text;
        }

        private void txtBenhAnHuongDieuTri_Enter(object sender, EventArgs e)
        {
            //_isCounterpart = txtBenhAnHuongDieuTri.Text.Trim() == txtTKBAHuongDieuTri.Text.Trim();
        }

        private void txtBenhAnHuongDieuTri_TextChanged(object sender, EventArgs e)
        {
            //if (_isCounterpart)
            //{
            //    txtTKBAHuongDieuTri.Text = txtBenhAnHuongDieuTri.Text;
            //}
        }

        private void txtBenhAnTomTatBenhAn_Enter(object sender, EventArgs e)
        {
            _isCounterpart = txtBenhAnTomTatBenhAn.Text.Trim() == txtTKBAQuaTrinhBenhLy.Text.Trim();
        }

        private void txtBenhAnTomTatBenhAn_TextChanged(object sender, EventArgs e)
        {
            if (_isCounterpart)
            {
                txtTKBAQuaTrinhBenhLy.Text = txtBenhAnTomTatBenhAn.Text;
            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            ctxIn.Show(cmdPrint, new Point(0, cmdPrint.Height));

            //if (pnlPrint.Visible == false)
            //{
            //    pnlPrint.Visible = true;
            //}
            //else
            //{
            //    pnlPrint.Visible = false;
            //}
        }

        private void cmdPrint_MouseHover(object sender, EventArgs e)
        {
            //if (pnlPrint.Visible == false)
            //{
            //    pnlPrint.Visible = true;
            //}
            //else
            //{
            //    pnlPrint.Visible = false;
            //}
        }

        private void cmdPrint_MouseLeave(object sender, EventArgs e)
        {
           
            //if (pnlPrint.Visible == false)
            //{
            //    pnlPrint.Visible = true;
            //}
            //else
            //{
            //    Thread.Sleep(5000);
            //    pnlPrint.Visible = false;
            //}
        }

        private void txtMaBenhAn_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCanNang_TextChanged(object sender, EventArgs e)
        {
            tinhBMI();
        }

        private void txtChieuCao_TextChanged(object sender, EventArgs e)
        {
            tinhBMI();
        }
        void tinhBMI()
        {
            if (txtCanNang.Text.Trim() != string.Empty && txtChieuCao.Text.Trim() != string.Empty) //2 ô có giá trị thì mới tính
            {
                if (txtCanNang.Text.Trim().All(char.IsDigit) && txtChieuCao.Text.Trim().All(char.IsDigit)) //2 ô phải là kiểu số
                {
                    if (Utility.DecimaltoDbnull(txtCanNang.Text, 0) > 0 && Utility.DecimaltoDbnull(txtChieuCao.Text, 0) > 0) //2 giá trị > 0
                    {
                        decimal bmi = Utility.DecimaltoDbnull(txtCanNang.Text, 0) / (Utility.DecimaltoDbnull(txtChieuCao.Text, 0) / 100 * Utility.DecimaltoDbnull(txtChieuCao.Text, 0) / 100);
                        txtBMI.Text = Utility.sDbnull(Math.Round(bmi, 2));
                    }
                }
            }
        }

        private void mnuInVoBA_Click(object sender, EventArgs e)
        {
            InBA(0);
        }

        private void mnuInTomtatBA_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Chưa có thông tin người bệnh để thực hiện thao tác in tóm tắt bệnh án");
                return;
            }
            KcbTomtatBA ttba =new Select().From(KcbTomtatBA.Schema)
                .Where(KcbTomtatBA.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(KcbTomtatBA.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteSingle<KcbTomtatBA>();
            if (ttba == null || ttba.Id <= 0)
            {
                Utility.ShowMsg("Bạn cần tạo Tóm tắt hồ sơ bệnh án trước khi thực hiện in");
                return;
            }
            clsInBA.InTomTatBA(ttba);
        }

        private void mnuInTo1_Click(object sender, EventArgs e)
        {
            InBA(1);
        }

        private void mnuInTo2_Click(object sender, EventArgs e)
        {
            InBA(2);
        }

        private void mnuInTo3_Click(object sender, EventArgs e)
        {
            InBA(3);
        }

        private void mnuInTo4_Click(object sender, EventArgs e)
        {
            InBA(4);
        }
        private void InBA(int toBA)
        {
            try
            {
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Chưa có thông tin người bệnh để thực hiện thao tác in tóm tắt bệnh án");
                    return;
                }
               
                if (objBANoitru == null || objBANoitru.IdBa <= 0)
                {
                    Utility.ShowMsg("Bạn cần tạo Bệnh án nội trú trước khi thực hiện in");
                    return;
                }
                DataTable dtData = SPs.BaNoitruLaythongtinIn(objBANoitru.IdBa, objBANoitru.MaBa, objBANoitru.IdBenhnhan, objBANoitru.MaLuotkham).GetDataSet().Tables[0];
                DataRow drData = dtData.Rows[0];
                List<string> lstcheckboxfields = new List<string>();
                Dictionary<string, string> dicMF = new Dictionary<string, string>();
                foreach (string chkField in lstcheckboxfields)
                {
                    dicMF.Add(chkField, Utility.Byte2Bool(drData[chkField]) ? "0" : "1");
                }
                string checkboxFieldsFile = AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\BA01_BANOITRU.txt";
                lstcheckboxfields = Utility.GetFirstValueFromFile(checkboxFieldsFile).Split(',').ToList<string>();
                NoitruPhieuravien _phieuravien = new Select().From(NoitruPhieuravien.Schema)
               .Where(NoitruPhieuravien.Columns.IdBenhnhan).IsEqualTo(objBenhnhan.IdBenhnhan)
               .And(NoitruPhieuravien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieuravien>();
                NoitruPhieunhapvien _phieunv = new Select().From(NoitruPhieunhapvien.Schema)
               .Where(NoitruPhieunhapvien.Columns.IdBenhnhan).IsEqualTo(objBenhnhan.IdBenhnhan)
               .And(NoitruPhieunhapvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieunhapvien>();
                dtData.TableName = "BA_NOITRU";
                Document doc;
                drData["ten_dvicaptren"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["p102"] = globalVariables.Branch_Name;
                drData["p101"] = globalVariables.ParentBranch_Name;
                drData["p132"] = _phieunv!=null?Utility.FormatDateTime_giophut_ngay_thang_nam(_phieunv.NgayNhapvien, ""):".......... giờ ....... ngày ........./........./.............";//Vào viện

                if (dtkhoanhapvien.Rows.Count > 0)
                {
                    drData["p141"] = Utility.FormatDateTime_giophut_ngay_thang_nam(Convert.ToDateTime(dtkhoanhapvien.Rows[0]["ngay_vaokhoa"]), "");//vào khoa
                    drData["p141_1"] = Utility.sDbnull(dtkhoanhapvien.Rows[0]["so_luong"], "0");
                }
                //REM lại do đã xử lý ở bước fillData trước khi ghi
                //drData["p103"] = drData["p140"];
                //if (dtkhoanhapvienCoGiuong.Rows.Count > 0 && THU_VIEN_CHUNG.Laygiatrithamsohethong("BA_LAYKHOANOITRU_COGIUONG", "0", false) == "1")
                //{
                //    drData["p103"] = Utility.sDbnull(dtkhoanhapvienCoGiuong.Rows[0]["ten_khoanoitru"], "");
                //    drData["p104"] = Utility.sDbnull(dtkhoanhapvienCoGiuong.Rows[0]["ten_giuong"], "");
                //}
                drData["p128"] = Utility.FormatDateTime(Utility.sDbnull(drData["p128"], ""), "ngày......tháng......năm.........");//BHYT giá trị đến
                drData["p145_1"] =_phieuravien!=null? Utility.FormatDateTime_giophut_ngay_thang_nam(_phieuravien.NgayRavien, ""):".......... giờ ....... ngày ........./........./.............";//ra viện
                //drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                //drData["SDT_bv"] = globalVariables.Branch_Phone;
                //drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                //drData["Fax_bv"] = globalVariables.Branch_Fax;
                //drData["website_bv"] = globalVariables.Branch_Website;
                //drData["email_bv"] = globalVariables.Branch_Email;
                //drData["ten_phieu"] = "Phiếu khám thai";
                //drData["sngay_kham_full"] = Utility.FormatDateTime_giophut_ngay_thang_nam(_pkt.NgayKham, "");
                //drData["sngay_kham"] = Utility.FormatDateTime(_pkt.NgayKham.Value);
                //drData["sNgaykykinh_cuoi"] = _pkt.NgayDaukykinhcuoi.HasValue ? _pkt.NgayDaukykinhcuoi.Value.ToString("dd/MM/yyyy") : "";
                //drData["sngaydukien_sinh"] = _pkt.NgayDukiensinh.HasValue ? _pkt.NgayDukiensinh.Value.ToString("dd/MM/yyyy") : "";
                //drData["sngay_kham"] = Utility.FormatDateTime(_pkt.NgayKham.Value);
                //drData["ngay_in"] = Utility.FormatDateTime(DateTime.Now);
                //drData["sngay_nhapvien"] = Utility.FormatDateTime_giophut_ngay_thang_nam(objLuotkham.NgayNhapvien, "");

                List<string> fieldNames = new List<string>();
                string tenToBA = "";
                if (toBA == 1) tenToBA = "BA01_BANOITRU_TO1.doc";
                else if (toBA == 0) tenToBA = "BA01_BANOITRU_BIA.doc";
                else if (toBA == 2) tenToBA = "BA01_BANOITRU_TO2.doc";
                else if (toBA == 3) tenToBA = "BA01_BANOITRU_TO3.doc";
                else if (toBA == 4) tenToBA = "BA01_BANOITRU_TO4.doc";
                else tenToBA = "BA01_BANOITRU.doc";
                string PathDoc = string.Format(AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\{0}", tenToBA);
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return;
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), "BANOITRU", objLuotkham.MaLuotkham, Utility.sDbnull(objBANoitru.IdBa), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return;
                    }
                    if (builder.MoveToMergeField("logo") && globalVariables.SysLogo != null)
                        if (sysLogosize != null)
                        {
                            int w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            int h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(globalVariables.SysLogo, w, h);
                            else
                                builder.InsertImage(globalVariables.SysLogo);
                        }
                        else
                            if (globalVariables.SysLogo != null)
                                builder.InsertImage(globalVariables.SysLogo);
                    Utility.MergeFieldsCheckBox2Doc(builder, null, lstcheckboxfields, drData);
                    //Nạp tiền sử sản khoa. Nhảy đến bảng số 3 trong file doc mẫu
                    try
                    {
                        if (toBA == 2 || toBA == 3 || toBA == 4)
                        {
                        }
                        else
                        {
                            int table_idx = 0;
                            string tbl_idx = AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\BA01_BANOITRU_TBLIDX.txt";
                            table_idx = Utility.Int32Dbnull(Utility.GetFirstValueFromFile(tbl_idx), 1);
                            Aspose.Words.Tables.Table tab = doc.FirstSection.Body.Tables[table_idx];

                            tab = tab.Rows[14].Cells[1].FirstChild as Aspose.Words.Tables.Table;//(Aspose.Words.Tables.Table)doc.GetChild(NodeType.Table, 0, true);//
                            int idx = 1;
                            foreach (DataRow dr in dtkhoachuyen.Rows)
                            {
                                Aspose.Words.Tables.Row newRow = idx == 1 ? (Aspose.Words.Tables.Row)tab.LastRow : (Aspose.Words.Tables.Row)tab.LastRow.Clone(true);//.Clone(true);
                                newRow.RowFormat.Borders.Shadow = false;
                                newRow.Cells[0].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                newRow.Cells[1].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                newRow.Cells[2].CellFormat.Shading.BackgroundPatternColor = Color.White;


                                newRow.Cells[0].FirstParagraph.Runs.Clear();
                                newRow.Cells[1].FirstParagraph.Runs.Clear();
                                newRow.Cells[2].FirstParagraph.Runs.Clear();

                                Run r = new Run(doc);
                                r.Font.Name = "Times New Roman";
                                r.Font.Size = 10d;
                                r.Font.Bold = false;
                                //r.Font.Color = Color.FromArgb(102, 0, 102);
                                r.Text = Utility.sDbnull(dr["ten_khoanoitru"], "");
                                newRow.Cells[0].FirstParagraph.AppendChild(r);
                                newRow.Cells[0].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                                r = new Run(doc);
                                r.Font.Name = "Times New Roman";
                                r.Font.Bold = false;
                                r.Font.Size = 10d;
                                //r.Font.Color = Color.FromArgb(102, 0, 102);
                                r.Text = Utility.sDbnull(dr["ngay_vaokhoa"], "");
                                newRow.Cells[1].FirstParagraph.AppendChild(r);
                                newRow.Cells[1].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                                r = new Run(doc);
                                r.Font.Name = "Times New Roman";
                                r.Font.Bold = false;
                                r.Font.Size = 10d;
                                //r.Font.Color = Color.FromArgb(102, 0, 102);
                                r.Text = Utility.sDbnull(dr["so_luong"], "");
                                newRow.Cells[2].FirstParagraph.AppendChild(r);
                                newRow.Cells[2].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                                if (idx > 1)
                                    tab.AppendChild(newRow);
                                idx += 1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }


                    //Các hàm MoveToMergeField cần thực hiện trước dòng MailMerge.Execute bên dưới
                    doc.MailMerge.Execute(drData);

                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    string path = fileKetqua;

                    if (File.Exists(path))
                    {
                        Process process = new Process();
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.Start();
                            process.WaitForInputIdle();
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy biểu mẫu", "TThông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void mnuInBA_Click(object sender, EventArgs e)
        {
            InBA(100);
        }

        private void cmdRefreshChucnangsong_Click(object sender, EventArgs e)
        {

            try
            {
                frm_XemthongtinChucnangsong _XemthongtinChucnangsong = new frm_XemthongtinChucnangsong(objLuotkham, true, 100);
                _XemthongtinChucnangsong._OnSelectMe += _XemthongtinChucnangsong__OnSelectMe;
                _XemthongtinChucnangsong.ShowDialog();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        void _XemthongtinChucnangsong__OnSelectMe(string mach, string nhietdo, string nhiptho, string huyetap, string chieucao, string cannang, string bmi, string nhommau, string SPO2)
        {
            txtMach.Text = mach;
            txtNhietDo.Text = nhietdo;
            txtNhipTho.Text = nhiptho;
            txtha.Text = huyetap;
            txtChieuCao.Text = chieucao;
            txtCanNang.Text = cannang;
            txtBMI.Text = bmi;
        }
        void DongbothongtinTKBA()
        {
            try
            {
                KcbTomtatBA ttba = new Select().From(KcbTomtatBA.Schema).Where(KcbTomtatBA.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(KcbTomtatBA.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<KcbTomtatBA>();
                if (ttba != null)
                {
                    dtpNgayTKBA.Value = ttba.NgayTtba.Value;
                    txtTKBAQuaTrinhBenhLy.Text = ttba.QuatrinhbenhlyDienbienlamsang;
                    txtTKBATTomTatKetQua.Text = ttba.TomtatKqcls;
                    txtTKBAPhuongPhapDieuTri.Text = ttba.PhuongphapDieutri;
                    txtTKBATinhTrangRaVien.Text = ttba.TinhtrangRavienMota;
                    txtTKBAHuongDieuTri.Text = ttba.HuongDieutri;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void cmdSyncTTBA_Click(object sender, EventArgs e)
        {
            DongbothongtinTKBA();
        }

        private void cmdKhoitaoBA_Click(object sender, EventArgs e)
        {
            cmdSave.PerformClick();
        }
    }
}
