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
using VMS.EMR.PHIEUKHAM;
using VMS.Emr;

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
            chkQLNBRaVienRavien.CheckedChanged += chkQLNBRaVien_CheckedChanged;
            chkQLNBChuyenVienCK.CheckedChanged += chkQLNBCK_CheckedChanged;
            chkQLNBRavienXinVe.CheckedChanged += chkQLNBXinVe_CheckedChanged;
            chkQLNBRavienBoVe.CheckedChanged += chkQLNBBoVe_CheckedChanged;
            chkQLNBRavienDuaVe.CheckedChanged += chkQLNBDuaVe_CheckedChanged;
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
        void PhanquyenTinhnang()
        {
            cmdKCB.Visible = cmdPhieuKCB2.Visible = Utility.Coquyen("EMR_THEM_PHIEUKCB");
            txtBenhAnToanThan.ReadOnly = txtBenhAnTuanHoan.ReadOnly = txtBenhAnHoHap.ReadOnly = txtBenhAnTieuHoa.ReadOnly = txtBenhAnThanTietNieuSinhDuc.ReadOnly = txtBenhAnThanKinh.ReadOnly = txtBenhAnCoXuongKhop.ReadOnly = txtBenhAnTaiMuiHong.ReadOnly = txtBenhAnMat.ReadOnly = txtBenhAnNoiTiet.ReadOnly = Utility.Coquyen("EMR_SUATRUCTIEP_THONGTINKHAMBENH");
        }    
        void soluongto_TextChanged(object sender, EventArgs e)
        {
            txtB_Tongso.Text =( Utility.Int32Dbnull(txtB_CTScanner.Text, 0) + Utility.Int32Dbnull(txtB_Khac.Text, 0) + Utility.Int32Dbnull(txtB_SieuAm.Text, 0) + Utility.Int32Dbnull(txtB_XetNghiem.Text, 0) + Utility.Int32Dbnull(txtB_Xquang.Text, 0)).ToString();
        }

        void chkKhac_CheckedChanged(object sender, EventArgs e)
        {
            txtThoigianKhac.Enabled = chkKhac.Checked;
            txtThoigianKhac.Focus();
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
                    if (objEmrBa != null && maBA != objEmrBa.MaBa)
                    {
                        if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn tìm Bệnh án theo mã: {0}.\nHệ thống sẽ nạp lại dữ liệu của Bệnh án tìm được và các thông tin bạn đang làm việc chưa kịp lưu sẽ bị hủy.\nNhấn Ok để tiếp tục. Nhấn No để quay lại trạng thái làm việc trước đó", Utility.DoTrim(txtMaBenhAn.Text)), "", true))
                        {
                            return;
                        }
                    }
                    objEmrBa = new Select().From(EmrBa.Schema).Where(EmrBa.Columns.MaBa).IsEqualTo(Utility.DoTrim(txtMaBenhAn.Text)).ExecuteSingle<EmrBa>();
                    if (objEmrBa == null)
                        ClearControl();
                    else
                    {
                        ucThongtinnguoibenh_v31.txtMaluotkham.Text = objEmrBa.MaLuotkham;
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
                    if (objEmrBa != null)
                        if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn tìm Bệnh án theo ID: {0}.\nHệ thống sẽ nạp lại dữ liệu của Bệnh án tìm được và các thông tin bạn đang làm việc chưa kịp lưu sẽ bị hủy.\nNhấn Ok để tiếp tục. Nhấn No để quay lại trạng thái làm việc trước đó", Utility.DoTrim(txtIDBenhAn.Text)), "", true))
                        {
                            return;
                        }
                    objEmrBa = EmrBa.FetchByID(Utility.Int64Dbnull(txtIDBenhAn.Text));
                    if (objEmrBa == null)
                        ClearControl();
                    else
                    {
                        ucThongtinnguoibenh_v31.txtMaluotkham.Text = objEmrBa.MaLuotkham;
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
                    objEmrBa = null;
                    ClearControl();
                    ucThongtinnguoibenh_v31.txtMaluotkham.Focus();
                    ucThongtinnguoibenh_v31.txtMaluotkham.SelectAll();
                    return;
                }
                objEmrBa = null;
                objTsbDacdiemlienquan = null;
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
            if (chkQLNBRavienBoVe.Checked == true)
            {
                chkQLNBRaVienRavien.Checked = false;
                chkQLNBRavienXinVe.Checked = false;

                chkQLNBRavienDuaVe.Checked = false;

            }
        }

        private void chkQLNBDuaVe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBRavienDuaVe.Checked == true)
            {
                chkQLNBRaVienRavien.Checked = false;
                chkQLNBRavienXinVe.Checked = false;
                chkQLNBRavienBoVe.Checked = false;


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
                chkQLNBChuyenVienCK.Checked = false;

            }
        }

        private void chkQLNBTuyenDuoi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBTuyenDuoi.Checked == true)
            {
                chkQLNBTuyenTren.Checked = false;

                chkQLNBChuyenVienCK.Checked = false;

            }
        }

        private void chkQLNBCK_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBChuyenVienCK.Checked == true)
            {
                chkQLNBTuyenTren.Checked = false;
                chkQLNBTuyenDuoi.Checked = false;


            }
        }

        private void chkQLNBRaVien_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBRaVienRavien.Checked == true)
            {

                chkQLNBRavienXinVe.Checked = false;
                chkQLNBRavienBoVe.Checked = false;
                chkQLNBRavienDuaVe.Checked = false;

            }
        }

        private void chkQLNBXinVe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBRavienXinVe.Checked == true)
            {
                chkQLNBRaVienRavien.Checked = false;

                chkQLNBRavienBoVe.Checked = false;
                chkQLNBRavienDuaVe.Checked = false;

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
                foreach (DmucBenh _objEmrBa in _list)
                {
                    ICD_chinh_Name += _objEmrBa.TenBenh + ";";
                    ICD_chinh_Code += _objEmrBa.MaBenh + ";";
                }
                lstICD = IDC_Phu.Split(',').ToList();
                _list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _objEmrBa in _list)
                {
                    ICD_Phu_Name += _objEmrBa.TenBenh + ";";
                    ICD_Phu_Code += _objEmrBa.MaBenh + ";";
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
        NoitruPhieuravien objPhieuRavien;
        private void FillThongtinRavien()
        {

            objPhieuRavien=  new Select().From(NoitruPhieuravien.Schema)
                .Where(NoitruPhieuravien.Columns.IdBenhnhan).IsEqualTo(objBenhnhan.IdBenhnhan)
                .And(NoitruPhieuravien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieuravien>();
            string chandoan = "";
            string mabenh = "";
            string chandoanphu = "";
            string mabenhphu = "";
           if(objPhieuRavien!=null)
            {
                string ICD_Name = "";
                string ICD_Code = "";
                string ICD_Phu_Name = "";
                string ICD_Phu_Code = "";
                GetChanDoanChinhPhu(Utility.sDbnull(objPhieuRavien.MabenhChinh, ""),
                           Utility.sDbnull(objPhieuRavien.MabenhPhu, ""), ref ICD_Name, ref ICD_Code, ref ICD_Phu_Name, ref ICD_Phu_Code);
                chandoan += string.IsNullOrEmpty(objPhieuRavien.ChanDoan)
                    ? ICD_Name
                    : Utility.sDbnull(objPhieuRavien.ChanDoan);
                mabenh += ICD_Code;
                chandoanphu += ICD_Phu_Name;
                mabenhphu += ICD_Phu_Code;
                //Điền 1 số thông tin ra viện
                dtpRavien_ngay.Value = objPhieuRavien.NgayRavien;//.ToString("dd/MM/yyyy");
                foreach (CheckBox cb in pnlKetquadieutriravien.Controls)
                    if (Utility.sDbnull(cb.Tag, "-1") == objPhieuRavien.MaKquaDieutri)
                        cb.Checked = true;
                    else
                        cb.Checked = false;
                foreach (CheckBox cb in pnlTinhtrangravien.Controls)
                    if (Utility.sDbnull(cb.Tag, "-1") == objPhieuRavien.MaTinhtrangravien)
                        cb.Checked = true;
                    else
                        cb.Checked = false;
                //Tình trạng ra viện
               

                chkTTRVLanhTinh.Checked = Utility.Bool2Bool(objPhieuRavien.GpbLanhtinh);
                chkTTRVNghiNgo.Checked = Utility.Bool2Bool(objPhieuRavien.GpbNghingo);
                chkTTRVAcTinh.Checked = Utility.Bool2Bool(objPhieuRavien.GpbActinh);
                if (objPhieuRavien.TuvongNgay.HasValue)
                    dtpNgaytuvong.Value = objPhieuRavien.TuvongNgay.Value;
                else
                    dtpNgaytuvong.ResetText();
                chkttrvDoBenh.Checked = Utility.Bool2Bool(objPhieuRavien.TuvongDobenh);
                chkttrvDoTaiBien.Checked = Utility.Bool2Bool(objPhieuRavien.TuvongDotaibien);
                chkttrvKhac.Checked = Utility.Bool2Bool(objPhieuRavien.TuvongDokhac);
                chkttrvTrong24GioVaoVien.Checked = Utility.Bool2Bool(objPhieuRavien.TuvongTrong24gio);
                chkttrvSau24Gio.Checked = Utility.Bool2Bool(objPhieuRavien.TuvongSau24h);

                txtTTRVNguyenNhanChinhTuVong.Text = Utility.sDbnull(objPhieuRavien.TuvongNguyennhanchinh);
                chkTTRVChandoanGiaiphauTuthi.Checked = Utility.Bool2Bool(objPhieuRavien.TuvongChandoangiaiphaututhi);
                txtTTRVChandoanGiaiphauTuthi.Text = Utility.sDbnull(objPhieuRavien.TuvongChandoangiaiphaututhiMota);
                chkCDTaiBien.Checked = Utility.Bool2Bool(objPhieuRavien.Taibien);
                chkCDBienChung.Checked = Utility.Bool2Bool(objPhieuRavien.Bienchung);
            }
            txtCDRavienTenBenhChinh.Text = chandoan;
            txtCDRavienMaBenhChinh.Text = Utility.sDbnull(mabenh);
            txtCDRavienTenBenhKemTheo.Text = chandoanphu;
            txtCDRavienMaBenhKemTheo.Text = mabenhphu;

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
            chkQLNBRavienBoVe.Checked = false;
            chkQLNBChuyenVienCK.Checked = false;
            chkQLNBTuyenDuoi.Checked = false;
            chkQLNBTuyenTren.Checked = false;
            txtQLNBChuyenVienNoiChuyenDen.Clear();
            dtpRavien_ngay.ResetText();
            chkQLNBRaVienRavien.Checked = false;


            chkQLNBRavienXinVe.Checked = false;
            chkQLNBRavienBoVe.Checked = false;
            chkQLNBRavienDuaVe.Checked = false;
            txtQLNBTongSoNgayDieuTri.Clear();
            txtCDNoiChuyenDen.Clear();
            txtCDMaNoiChuyenDen.Clear();
            txtCDKKBCapCuu.Clear();
            txtCDMaKKBCapCuu.Clear();


            txtCDKhiVaoDieuTri.Clear();
            txtCDMaKhiVaoDieuTri.Clear();
            txtCDRavienTenBenhChinh.Clear();
            txtCDRavienMaBenhChinh.Clear();
            txtCDRavienTenBenhKemTheo.Clear();
            txtCDRavienMaBenhKemTheo.Clear();
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

            dtpNgaytuvong.ResetText();
            chkttrvDoBenh.Checked = false;
            chkttrvTrong24GioVaoVien.Checked = false;
            chkttrvDoTaiBien.Checked = false;
            chkttrvSau24Gio.Checked = false;
            chkttrvKhac.Checked = false;
            txtTTRVNguyenNhanChinhTuVong.Clear();
            chkTTRVChandoanGiaiphauTuthi.Checked = false;
            txtTTRVChandoanGiaiphauTuthi.Clear();
            txtBenhAnLyDoNhapVien.SetDefaultItem();
            txtBenhAnVaoNgayThu.Clear();
            txtBenhAnQuaTrinhBenhLy.Clear();
            txtBenhAnTiensuBanthan.Clear();
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
            txtThoigianKhac.Clear();
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
        EmrDocuments emrdoc = new EmrDocuments();

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!KiemTraThongTin()) return;
                objEmrBa = TaoEmrBa();
                //if (objEmrBa.IdBa > 0 && objEmrBa.MaBa != maBA)
                //{
                //    if(Utility.AcceptQuestion("Mã bệnh án cũ :{0} đang khác với mã bệnh án nhập tay: {1}. Bạn có chắc chắn muốn cập nhật lại thành mã bệnh án mới","",))
                //    {
                //    }
                //}
                 EmrHosoluutru hsba =null;
                if (objEmrBa.IdBa <= 0)
                {
                    hsba = new EmrHosoluutru();
                    hsba.IdBa = objEmrBa.IdBa;
                    hsba.LoaiBa = objEmrBa.LoaiBa;
                    hsba.MaBa = objEmrBa.MaBa;
                    hsba.IdBenhnhan = objEmrBa.IdBenhnhan;
                    hsba.MaLuotkham = objEmrBa.MaLuotkham;
                    hsba.MaCoso = objEmrBa.MaCoso;
                    hsba.NgayTao = objEmrBa.NgaylamBa.Value;
                    hsba.NguoiTao = objEmrBa.NguoiTao;
                    hsba.Nam = objEmrBa.NgayTao.Value.Year;
                    hsba.TrangThai = 0;
                }
                
                if (Utility.Coquyen("EMR_SUA_PHIEUKCB") && objEmrBa.IdBa > 0)
                {
                    TaoPhieuKCB();
                    objPKB.Save();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin phiếu khám toàn thân tại chức năng EMR cho người bệnh: {0}-{1} thành công", objEmrBa.MaLuotkham, objEmrBa.TenBenhnhan), objPKB.IsNew ? newaction.Insert : newaction.Update, "EMR");
                }
                if (Utility.Coquyen("EMR_SUA_TKBA") && objEmrBa.IdBa >0)
                {
                    TaoPhieuTKBA();
                    objTKBA.Save();
                    if(objTKBA.IsNew)
                    {
                       
                        emrdoc.InitDocument(objTKBA.IdBenhnhan, objTKBA.MaLuotkham, Utility.Int64Dbnull(objTKBA.Id), objTKBA.NgayTtba.Value, Loaiphieu_HIS.BENHAN, "BA_TKBA", objTKBA.NguoiTao, -1, -1, Utility.Byte2Bool(0), "");
                        emrdoc.Save();
                    }    
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin Tổng kết BA tại chức năng EMR cho người bệnh: {0}-{1} thành công", objEmrBa.MaLuotkham, objEmrBa.TenBenhnhan), objTKBA.IsNew ? newaction.Insert : newaction.Update, "EMR");
                }
                if (Utility.Coquyen("EMR_SUA_DACDIEMLIENQUANBENH") && objEmrBa.IdBa > 0)
                {
                    TaoPhieuDacdiemLienquanBenh();
                    objTsbDacdiemlienquan.Save();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin đặc điểm liên quan bệnh tại chức năng EMR cho người bệnh: {0}-{1} thành công", objEmrBa.MaLuotkham, objEmrBa.TenBenhnhan), objTKBA.IsNew ? newaction.Insert : newaction.Update, "EMR");
                }
                objEmrBa.Save();
                if (hsba != null )
                {
                    hsba.IdBa = objEmrBa.IdBa;
                    hsba.Save();
                }
                txtIDBenhAn.Text = objEmrBa.IdBa.ToString();
                if (m_enAct == action.Insert)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới BA nội trú cho bệnh nhân: {0}-{1} thành công", objEmrBa.IdBa, objEmrBa.TenBenhnhan), objEmrBa.IsNew ? newaction.Insert : newaction.Update, "UI");
                    MessageBox.Show("Đã thêm mới Bệnh án thành công. Nhấn Ok để kết thúc");
                    cmdXoaBenhAn.Enabled = cmdPrint.Enabled = true;
                    if (_OnCreated != null) _OnCreated(objEmrBa.IdBa, objEmrBa.MaBa, action.Insert);
                    m_enAct = action.Update;
                   
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN, "BA_TO_1", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "");
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN, "BA_TO_2", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "");
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN, "BA_TO_3", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "");
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN, "BA_TO_4", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "");
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN, "BA_FULL", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "");
                    emrdoc.Save();
                }
                else if (m_enAct == action.Update)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật Bệnh án nội trú cho bệnh nhân: {0}-{1} thành công", objEmrBa.IdBa, objEmrBa.TenBenhnhan), objEmrBa.IsNew ? newaction.Insert : newaction.Update, "UI");
                    if (_OnCreated != null) _OnCreated(objEmrBa.IdBa, objEmrBa.MaBa, action.Update);
                    MessageBox.Show("Đã cập nhật Bệnh án thành công. Nhấn Ok để kết thúc");
                    m_enAct = action.Update;
                }
                EnableBA();
                //Utility.ShowMsg("Lưu thông tin thành công", "Thông báo");
                dtDataBA = SPs.EmrBaLaythongtinIn(-1, "", objBenhnhan.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                _isSuccess = true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                if (objEmrBa != null && _isSuccess)
                {
                    new Update(KcbLuotkham.Schema)
                        .Set(KcbLuotkham.Columns.SoBenhAn).EqualTo(objEmrBa.MaBa)
                        .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                   // EmrThemBenhAn();
                }
              
            }
        }
        void TaoPhieuDacdiemLienquanBenh()
        {
            objTsbDacdiemlienquan = new Select().From(EmrTiensubenhDacdiemlienquan.Schema)
             .Where(EmrTiensubenhDacdiemlienquan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
             .And(EmrTiensubenhDacdiemlienquan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
             .ExecuteSingle<EmrTiensubenhDacdiemlienquan>();
            if (objTsbDacdiemlienquan != null && objTsbDacdiemlienquan.IdTsb > 0)
            {
                objTsbDacdiemlienquan.MarkOld();
                objTsbDacdiemlienquan.NguoiSua = globalVariables.UserName;
                objTsbDacdiemlienquan.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
            }
            else
            {
                objTsbDacdiemlienquan = new EmrTiensubenhDacdiemlienquan();
                objTsbDacdiemlienquan.IsNew = true;
                objTsbDacdiemlienquan.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                objTsbDacdiemlienquan.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                objTsbDacdiemlienquan.NguoiTao = globalVariables.UserName;
                objTsbDacdiemlienquan.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
            }
            objTsbDacdiemlienquan.TsbDiung = chkDiUng.Checked;
            objTsbDacdiemlienquan.TsbMatuy = chkMaTuy.Checked;
            objTsbDacdiemlienquan.TsbRuoubia = chkRuouBia.Checked;
            objTsbDacdiemlienquan.TsbThuocla = chkThuocLa.Checked;
            objTsbDacdiemlienquan.TsbThuoclao = chkThuocLao.Checked;
            objTsbDacdiemlienquan.TsbKhac = chkKhac.Checked;
            if (chkDiUng.Checked) objTsbDacdiemlienquan.TsbThoigianDiung = txtDiUng.Text;
            else objTsbDacdiemlienquan.TsbThoigianDiung = "";
            if (chkMaTuy.Checked) objTsbDacdiemlienquan.TsbThoigianMatuy = txtMaTuy.Text;
            else objTsbDacdiemlienquan.TsbThoigianMatuy = "";
            if (chkRuouBia.Checked) objTsbDacdiemlienquan.TsbThoigianRuoubia = txtRuouBia.Text;
            else objTsbDacdiemlienquan.TsbThoigianRuoubia = "";
            if (chkThuocLa.Checked) objTsbDacdiemlienquan.TsbThoigianThuocla = txtThuocLa.Text;
            else objTsbDacdiemlienquan.TsbThoigianThuocla = "";
            if (chkThuocLao.Checked) objTsbDacdiemlienquan.TsbThoigianThuoclao = txtThuocLao.Text;
            else objTsbDacdiemlienquan.TsbThoigianThuoclao = "";
            if (chkKhac.Checked) objTsbDacdiemlienquan.TsbThoigianKhac = txtThoigianKhac.Text;
            else objTsbDacdiemlienquan.TsbThoigianKhac = "";
        }
        void TaoPhieuTKBA()
        {
             objTKBA = new Select().From(EmrTongketBenhan.Schema).Where(EmrTongketBenhan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(EmrTongketBenhan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<EmrTongketBenhan>();
            if (objTKBA == null) objTKBA = new EmrTongketBenhan();
            if (objTKBA.Id > 0)
            {
                objTKBA.IsNew = false;
                objTKBA.MarkOld();
                objTKBA.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                objTKBA.NguoiSua = globalVariables.UserName;
            }
            else
            {
                objTKBA.IsNew = true;
                objTKBA.NguoiTao = globalVariables.UserName;
                objTKBA.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
            }
            objTKBA.MaLuotkham = objLuotkham.MaLuotkham;
            objTKBA.IdBenhnhan = (int)objLuotkham.IdBenhnhan;
            objTKBA.IdKhoadieutri = Utility.Int32Dbnull(objEmrBa.IdKhoaravien, -1);
            objTKBA.TiensuBenh = "";
            objTKBA.TomtatKqcls = "";
            objTKBA.QuatrinhbenhlyDienbienlamsang = objEmrBa.TongketbaQuatrinhbenhlyDienbienlamsang;
            objTKBA.TomtatKqcls = objEmrBa.TongketbaTomtatKqcls;
            objTKBA.TinhtrangRavienMota = objEmrBa.TongketbaTinhtrangNguoiravien;
            objTKBA.PhuongphapDieutri = objEmrBa.TongketbaPhuongphapdieutri;
            objTKBA.HuongDieutri = objEmrBa.TongketbaHuongdieutritieptheo;
            objTKBA.NgayTtba = objEmrBa.TongketbaNgay;
            objTKBA.Noikhoa = 0;
            objTKBA.NoikhoaMota = "";
            objTKBA.Pttt = 0;
            objTKBA.PtttMota = "";
        }
        void TaoPhieuKCB()
        {
            //Refresh lại thông tin KCB
            objPKB = new Select().From(EmrPhieukhambenh.Schema)
                .Where(EmrPhieukhambenh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrPhieukhambenh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteSingle<EmrPhieukhambenh>();
            if (objPKB != null && objPKB.Id > 0)
            {
                objPKB.MarkOld();
                objPKB.NguoiSua = globalVariables.UserName;
                objPKB.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
            }
            else
            {
                objPKB = new EmrPhieukhambenh();
                objPKB.IsNew = true;
                objPKB.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                objPKB.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                objPKB.NgayKham = dtpNgayKham.Value.Date;
                objPKB.NguoiTao = globalVariables.UserName;
                objPKB.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
            }
            objPKB.IdBacsi = Utility.Int16Dbnull(txtBacsiKham.MyID, -1);
            objPKB.HuyetAp = txtha.Text;
            objPKB.NhietDo = txtNhietDo.Text;
            objPKB.Mach = Utility.sDbnull(txtMach.Text);
            objPKB.NhipTho = Utility.sDbnull(txtNhipTho.Text);
            objPKB.ChieuCao = Utility.sDbnull(txtChieuCao.Text);
            objPKB.CanNang = Utility.sDbnull(txtCanNang.Text);
            objPKB.Bmi = Utility.sDbnull(txtBMI.Text);
            objPKB.MotaThem = "";
            objPKB.ToanThan = Utility.sDbnull(txtBenhAnToanThan.Text);
            objPKB.Tuanhoan = Utility.sDbnull(txtBenhAnTuanHoan.Text);
            objPKB.Hohap = Utility.sDbnull(txtBenhAnHoHap.Text);
            objPKB.Tieuhoa = Utility.sDbnull(txtBenhAnTieuHoa.Text);
            objPKB.Thantietnieusinhduc = Utility.sDbnull(txtBenhAnThanTietNieuSinhDuc.Text);
            objPKB.Thankinh = Utility.sDbnull(txtBenhAnThanKinh.Text);
            objPKB.Coxuongkhop = Utility.sDbnull(txtBenhAnCoXuongKhop.Text);
            objPKB.Taimuihong = Utility.sDbnull(txtBenhAnTaiMuiHong.Text);
            objPKB.Ranghammat = Utility.sDbnull(txtBenhAnRangHamMat.Text);
            objPKB.Mat = Utility.sDbnull(txtBenhAnMat.Text);
            objPKB.Noitietdinhduongbenhlykhac = Utility.sDbnull(txtBenhAnNoiTiet.Text);

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
        //            objPatientHi.IdBenhanHis = Utility.Int32Dbnull(objEmrBa.Id);
        //            objPatientHi.MaPhieuEmr = objDmucBenhan.MaPhieuEmr;
        //            objPatientHi.MaDmucBa = objDmucBenhan.MaBenhan;
        //            objPatientHi.MaLuotkham = objLuotkham.MaLuotkham;
        //            objPatientHi.IdBenhnhan = objLuotkham.IdBenhnhan;
        //            objPatientHi.EmrNo = objEmrBa.MaEmrBa;
        //            ActionResult actionResult = EmrDocumentServices.ThemBenhAn(objPatientHi, action.Insert);
        //            switch (actionResult)
        //            {
        //                case ActionResult.Success: 
        //                    break;
        //            }
        //        }
        //    }
        //} 
        private EmrBa TaoEmrBa()
        {
            if (objEmrBa == null) objEmrBa = new EmrBa();
            try
            {
                int id = Utility.Int32Dbnull(txtIDBenhAn.Text, -1);
                if (objEmrBa.IdBa > 0)
                {
                    objEmrBa.IsLoaded = true;
                    objEmrBa.MarkOld();
                    objEmrBa.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                    objEmrBa.NguoiSua = globalVariables.UserName;
                }
                else
                {
                    SinhMaBenhAn();
                    objEmrBa.MaBa = Utility.sDbnull(txtMaBenhAn.Text);
                    objEmrBa.NguoiTao = globalVariables.UserName;
                    objEmrBa.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                }
                objEmrBa.NgaylamBa = dtpNgayBA.Value;
                objEmrBa.TongketbaNgay = dtpNgayTKBA.Value;
                objEmrBa.LoaiBa = cboLoaiBA.SelectedValue.ToString();
                if (dtkhoanhapvienCoGiuong.Rows.Count > 0 && THU_VIEN_CHUNG.Laygiatrithamsohethong("BA_LAYKHOANOITRU_COGIUONG", "0", false) == "1")
                {
                    objEmrBa.Khoa = Utility.sDbnull(dtkhoanhapvienCoGiuong.Rows[0]["ten_khoanoitru"], "");
                    objEmrBa.Giuong = Utility.sDbnull(dtkhoanhapvienCoGiuong.Rows[0]["ten_giuong"], "");
                    objEmrBa.Buong = Utility.sDbnull(dtkhoanhapvienCoGiuong.Rows[0]["ten_buong"], "");
                }
                else if (dtkhoanhapvien.Rows.Count > 0)
                {
                    objEmrBa.Khoa = Utility.sDbnull(dtkhoanhapvien.Rows[0]["ten_khoanoitru"], "");
                    objEmrBa.Giuong = Utility.sDbnull(dtkhoanhapvien.Rows[0]["ten_giuong"], "");
                    objEmrBa.Buong = Utility.sDbnull(dtkhoanhapvien.Rows[0]["ten_buong"], "");
                }
                else
                {
                    //REM lại vì đây là khoa nhập viện hoặc khoa nhập viện có nằm giường
                    objEmrBa.Buong = ucThongtinnguoibenh_v31.txtBuong.Text;
                    objEmrBa.Khoa = ucThongtinnguoibenh_v31.txtKhoanoitru.Text;
                    objEmrBa.Giuong = ucThongtinnguoibenh_v31.txtGiuong.Text;
                }
                //objEmrBa.BenhNgoaiKhoa = Utility.sDbnull(txtBenhNgoai_Khoa.Text);
                objEmrBa.MaCoso = objLuotkham.MaCoso;
                objEmrBa.IdBenhnhan = objLuotkham.IdBenhnhan;
                objEmrBa.TenBenhnhan = objBenhnhan.TenBenhnhan;
                objEmrBa.MaLuotkham = objLuotkham.MaLuotkham;
                objEmrBa.MaYte = objLuotkham.MaYte;
                objEmrBa.NgaySinh = objBenhnhan.NgaySinh.Value;
                objEmrBa.MaGioitinh =Utility.ByteDbnull( objBenhnhan.IdGioitinh)==0?"M":"F";
                objEmrBa.GioiTinh = objBenhnhan.GioiTinh;
                objEmrBa.Tuoi =(byte) objBenhnhan.Tuoi;
                objEmrBa.LoaiTuoi = (byte)objLuotkham.LoaiTuoi;
                
                
                objEmrBa.MaNghenghiep = objBenhnhan.NgheNghiep;
                objEmrBa.TenNghenghiep = objBenhnhan.TenNghenghiep;
                objEmrBa.MaDantoc = objBenhnhan.DanToc;
                objEmrBa.TenDantoc = objBenhnhan.TenDantoc; 
                objEmrBa.MaTongiao = objBenhnhan.TonGiao;
                objEmrBa.TenTongiao = objBenhnhan.TonGiao; 
                objEmrBa.MaQuocgia = objBenhnhan.MaQuocgia;// Utility.Int16Dbnull(objBenhnhan.MaQuocgia != "" && objBenhnhan.MaQuocgia != "VN" ? 1 : 0);
                objEmrBa.TenQuocgia = objBenhnhan.TenQuocgia;
                objEmrBa.NgoaiKieu = (Utility.sDbnull(objBenhnhan.MaQuocgia) == "VN" ? 0 : 1) == 1;
               
                objEmrBa.DiachiLienhe = objBenhnhan.DiachiLienhe;
                objEmrBa.DienthoaiLienhe = objBenhnhan.DienthoaiLienhe;
                objEmrBa.NguoiLienhe = objBenhnhan.NguoiLienhe;
                objEmrBa.CmtNguoilienhe = objBenhnhan.CmtNguoilienhe;
                objEmrBa.DiaChi = objLuotkham.DiaChi;
                objEmrBa.MaTinhtp = objLuotkham.MaTinhtp;
                objEmrBa.TenTinhtp = objBenhnhan.TenTinhtp;
                objEmrBa.MaQuanhuyen = objLuotkham.MaQuanhuyen;
                objEmrBa.TenQuanhuyen = objBenhnhan.TenQuanhuyen;
                objEmrBa.MaXaphuong = objLuotkham.MaXaphuong;
                objEmrBa.TenXaphuong = objBenhnhan.TenXaphuong; 
                objEmrBa.MaCoquan = objBenhnhan.CoQuan;
                objEmrBa.TenCoquan = objBenhnhan.CoQuan;
                objEmrBa.MatheBhyt = objBenhnhan.MatheBhyt;
                objEmrBa.MaDoituong =Utility.ByteDbnull( objLuotkham.IdDoituongKcb);
                objEmrBa.TenDoituong = objBenhnhan.TenDoituongKcb;

                objEmrBa.MatheBhyt = objLuotkham.MatheBhyt;
                objEmrBa.BhytTungay = objLuotkham.NgaybatdauBhyt;
                objEmrBa.BhytDenngay = objLuotkham.NgayketthucBhyt;
                    objEmrBa.HotenBo = "";
                objEmrBa.TrinhdoVanhoaBo = "";
                objEmrBa.NghenghiepBo = "";
                objEmrBa.HotenMe = "";
                objEmrBa.TrinhdoVanhoaMe = "";
                objEmrBa.NghenghiepMe = "";
                
                objEmrBa.CmtCccd = objLuotkham.Cmt;
                objEmrBa.SoHochieu = objLuotkham.Cmt;
                objEmrBa.DienThoai = objBenhnhan.DienThoai;
                objEmrBa.Email = objLuotkham.Email;

                //objEmrBa.MaKhoaravien = "";
                //objEmrBa.TenKhoaravien = objBenhnhan.TenKhoanoitru;
                //objEmrBa.IdKhoadieutri = objBenhnhan.IdKhoanoitru;
                if (objNhapvien != null)
                {
                    objEmrBa.VaovienNgay = objNhapvien.NgayNhapvien;
                    objEmrBa.VaovienCapcuu = chkQLNBCapCuu.Checked;
                    objEmrBa.VaovienKkb = chkQLNBKKB.Checked;
                    objEmrBa.VaovienKhoadieutri = chkQLNBKhoaDieuTri.Checked;
                    
                    objEmrBa.NoigioithieuCoquanyte = chkQLNBCoQuanYTe.Checked;
                    objEmrBa.NoigioithieuTuden = chkQLNBTuDen.Checked;
                    objEmrBa.NoigioithieuKhac = chkQLNBKhac.Checked;
                   
                  
                    objEmrBa.VaovienLanthu = Utility.ByteDbnull(txtQLNBLanVaoVien.Text);
                }
                //Check lại
                objEmrBa.VaovienMakhoa = lblMakhoavao.Text;
                objEmrBa.VaovienTenkhoa = lblqlbnKhoa.Text;
                objEmrBa.VaovienNgayvaokhoa = null;
                if(objPhieuchuyenvien!=null)
                {
                    objEmrBa.ChuyenvienTuyentren = chkQLNBTuyenTren.Checked;
                    objEmrBa.ChuyenvienTuyenduoi = chkQLNBTuyenDuoi.Checked;
                    objEmrBa.ChuyenvienKhac = chkQLNBChuyenVienCK.Checked;
                    objEmrBa.ChuyenvienNoichuyenden= Utility.sDbnull(txtQLNBChuyenVienNoiChuyenDen.Text);
                }
                if (objPhieuRavien != null)
                {
                    objEmrBa.RavienRavien = chkQLNBRaVienRavien.Checked;
                    objEmrBa.RavienXinve = chkQLNBRavienXinVe.Checked;
                    objEmrBa.RavienBove = chkQLNBRavienBoVe.Checked;
                    objEmrBa.RavienDuave = chkQLNBRavienDuaVe.Checked;
                    objEmrBa.ChuyenvienNoichuyenden = Utility.sDbnull(txtQLNBChuyenVienNoiChuyenDen.Text);
                    objEmrBa.RavienMaBenhchinh = txtCDRavienMaBenhChinh.Text;
                    objEmrBa.RavienMaBenhphu = txtCDRavienMaBenhKemTheo.Text;
                    objEmrBa.RavienTenBenhchinh = txtCDRavienTenBenhKemTheo.Text;
                    objEmrBa.RavienTenBenhphu = txtCDRavienMaBenhKemTheo.Text;
                    //Tình trạng ra viện
                    //Kết quả điều trị
                    objEmrBa.TinhtrangravienKetquadieutriKhoi = chkTTRVKhoi.Checked;
                    objEmrBa.TinhtrangravienKetquadieutriDogiam = chkTTRVDoGiam.Checked;
                    objEmrBa.TinhtrangravienKetquadieutriKhongthaydoi = chkTTRVKhongThayDoi.Checked;
                    objEmrBa.TinhtrangravienKetquadieutriNanghon = chkTTRVNangHon.Checked;
                    objEmrBa.TinhtrangravienKetquadieutriTuvong = chkTTRVTuVong.Checked;
                    //Giải phẫu bệnh
                    objEmrBa.TinhtrangravienGpbLanhtinh = chkTTRVLanhTinh.Checked;
                    objEmrBa.TinhtrangravienGpbNghingo = chkTTRVNghiNgo.Checked;
                    objEmrBa.TinhtrangravienGpbActinh = chkTTRVAcTinh.Checked;
                    //Tình hình tử vong
                    objEmrBa.TinhtrangravienThoigianTuvong = objPhieuRavien.TuvongNgay;
                    objEmrBa.TinhtrangravienLydotuvongDobenh = chkttrvDoBenh.Checked;
                    objEmrBa.TinhtrangravienLydotuvongDotaibiendieutri = chkttrvDoTaiBien.Checked;
                    objEmrBa.TinhtrangravienLydotuvongKhac = chkttrvKhac.Checked;
                    objEmrBa.TinhtrangravienThoigiantuvongTrong24h = chkttrvTrong24GioVaoVien.Checked;
                    objEmrBa.TinhtrangravienThoigiantuvongSau24h = chkttrvSau24Gio.Checked;
                    objEmrBa.TinhtrangravienNguyennhantuvong = Utility.sDbnull(txtTTRVNguyenNhanChinhTuVong.Text);
                    //objEmrBa.TinhtrangravienMaNguyennhantuvong = Utility.sDbnull(txtTTRVNguyenNhanChinhTuVong.Text);
                    objEmrBa.TinhtrangravienKhamnghiemtuthi = chkTTRVKhamNgiemTuThi.Checked;
                    objEmrBa.TinhtrangravienChandoangiauphaututhi = Utility.sDbnull(txtTTRVChandoanGiaiphauTuthi.Text);
                    //objEmrBa.TinhtrangravienChandoangiauphaututhi
                }
                
                
              //Chẩn đoán
                objEmrBa.RavienTongsongayDieutri = Utility.Int16Dbnull(txtQLNBTongSoNgayDieuTri.Text);
                objEmrBa.CdNoichuyenden = txtCDNoiChuyenDen.Text;
                objEmrBa.CdNoichuyendenMa = txtCDMaNoiChuyenDen.Text;
                objEmrBa.CdKkbCapcuu = txtCDKKBCapCuu.Text;
                objEmrBa.CdKkbCapcuuMa = txtCDMaKKBCapCuu.Text;
                objEmrBa.CdKhoadieutri = txtCDKhiVaoDieuTri.Text;
                objEmrBa.CdKhoadieutriMa = txtCDMaKhiVaoDieuTri.Text;
                objEmrBa.CdNoichuyenden = txtCDNoiChuyenDen.Text;
                objEmrBa.CdThuthuat = chkCDThuThuat.Checked;
                objEmrBa.CdPhauthuat = chkCDPhauThuat.Checked;
                objEmrBa.CdTaibien = chkCDTaiBien.Checked;
                objEmrBa.CdTaibienBienchungKhac = chkCDBienChung.Checked;

               
                objEmrBa.VaovienLydovaovien = txtBenhAnLyDoNhapVien.Text;
                objEmrBa.VaovienVaongaythucuabenh =Utility.ByteDbnull( txtBenhAnVaoNgayThu.Text);
                objEmrBa.HoibenhQuatrinhbenhly = Utility.sDbnull(txtBenhAnQuaTrinhBenhLy.Text);
                objEmrBa.HoibenhTiensubanthan = Utility.sDbnull(txtBenhAnTiensuBanthan.Text);
               if(objTsbDacdiemlienquan!=null)
                {
                    objEmrBa.TsbDiung = chkDiUng.Checked;
                    objEmrBa.TsbMatuy = chkMaTuy.Checked;
                    objEmrBa.TsbRuoubia = chkRuouBia.Checked;
                    objEmrBa.TsbThuocla = chkThuocLa.Checked;
                    objEmrBa.TsbThuoclao = chkThuocLao.Checked;
                    objEmrBa.TsbKhac = chkKhac.Checked;
                    objEmrBa.TsbThoigianDiung = txtDiUng.Text;
                    objEmrBa.TsbThoigianMatuy = txtMaTuy.Text;
                    objEmrBa.TsbThoigianRuoubia = txtRuouBia.Text;
                    objEmrBa.TsbThoigianThuocla = txtThuocLa.Text;
                    objEmrBa.TsbThoigianThuoclao = txtThuocLao.Text;
                    objEmrBa.TsbThoigianKhac = txtThoigianKhac.Text;
                }    
                
                objEmrBa.HoibenhTiensugiadinh = txtBenhAnGiaDinh.Text;
                
                objEmrBa.KbMach = txtMach.Text;
                objEmrBa.KbNhietdo = txtNhietDo.Text;
                objEmrBa.KbHuyetap = txtha.Text;
                objEmrBa.KbNhiptho = txtNhipTho.Text;
                objEmrBa.KbCannang = txtCanNang.Text;
                objEmrBa.KbChieucao = txtChieuCao.Text;
                tinhBMI();
               //Thông tin khám bệnh
                objEmrBa.KbBmi = Utility.sDbnull(txtBMI.Text, 0); 
                objEmrBa.KhambenhToanthan = Utility.sDbnull(txtBenhAnToanThan.Text);
                objEmrBa.KhambenhTuanhoan = Utility.sDbnull(txtBenhAnTuanHoan.Text);
                objEmrBa.KhambenhHohap = Utility.sDbnull(txtBenhAnHoHap.Text);
                objEmrBa.KhambenhTieuhoa = Utility.sDbnull(txtBenhAnTieuHoa.Text);
                objEmrBa.KhambenhThantietnieusinhduc = Utility.sDbnull(txtBenhAnThanTietNieuSinhDuc.Text);
                objEmrBa.KhambenhThankinh = Utility.sDbnull(txtBenhAnThanKinh.Text);
                objEmrBa.KhambenhCoxuongkhop = Utility.sDbnull(txtBenhAnCoXuongKhop.Text);
                objEmrBa.KhambenhTaimuihong = Utility.sDbnull(txtBenhAnTaiMuiHong.Text);
                objEmrBa.KhambenhRanghammat = Utility.sDbnull(txtBenhAnRangHamMat.Text);
                objEmrBa.KhambenhMat = Utility.sDbnull(txtBenhAnMat.Text);
                objEmrBa.KhambenhNoitietDinhduongBenhlykhac = Utility.sDbnull(txtBenhAnNoiTiet.Text);

               //
                objEmrBa.KhambenhXetnghiemClsCanlam = Utility.sDbnull(txtBenhAnCacXetNghiem.Text);
                objEmrBa.KhambenhTomtatbenhan = Utility.sDbnull(txtBenhAnTomTatBenhAn.Text);
                objEmrBa.CdKhivaokhoadieutriBenhchinh = Utility.sDbnull(txtBenhAnBenhChinh.Text);
                objEmrBa.CdKhivaokhoadieutriBenhphu = Utility.sDbnull(txtBenhAnBenhKemTheo.Text);
                objEmrBa.CdKhivaokhoadieutriPhanbiet = Utility.sDbnull(txtBenhAnPhanBiet.Text);
               
                objEmrBa.KhambenhTienluong = Utility.sDbnull(txtBenhAnTienLuong.Text);
                objEmrBa.KhambenhHuongdieutri = Utility.sDbnull(txtBenhAnHuongDieuTri.Text);
                
                objEmrBa.TongketbaQuatrinhbenhlyDienbienlamsang = Utility.sDbnull(txtTKBAQuaTrinhBenhLy.Text);
                objEmrBa.TongketbaTomtatKqcls = Utility.sDbnull(txtTKBATTomTatKetQua.Text);
                objEmrBa.TongketbaPhuongphapdieutri = Utility.sDbnull(txtTKBAPhuongPhapDieuTri.Text);
                objEmrBa.TongketbaTinhtrangNguoiravien = Utility.sDbnull(txtTKBATinhTrangRaVien.Text);
                objEmrBa.TongketbaHuongdieutritieptheo = Utility.sDbnull(txtTKBAHuongDieuTri.Text);

                objEmrBa.TongketbaMaNguoigiaohoso = txtNguoiGiaoHoSo.Text;
                objEmrBa.TongketbaMaNguoiNhanhoso = txtNguoiNhanHoSo.Text;
                objEmrBa.MabacsiLamBA = txtBSlamBA.MyCode;
                objEmrBa.IdBacsiLamBA =Utility.Int16Dbnull( txtBSlamBA.MyID);
                objEmrBa.TenbacsiLamBA = txtBSlamBA.Text;
                objEmrBa.TenbacsiDieutri = txtBSDieuTri.Text;
                objEmrBa.IdBacsiDieutri = Utility.Int16Dbnull(txtBSDieuTri.MyID);
                objEmrBa.MabacsiDieutri = txtBSDieuTri.MyCode;
                objEmrBa.TongketbaSotoCt = Utility.Int16Dbnull(txtB_CTScanner.Text);
                objEmrBa.TongketbaSotoXquang = Utility.Int16Dbnull(txtB_Xquang.Text);
                objEmrBa.TongketbaSotoSieuam = Utility.Int16Dbnull(txtB_SieuAm.Text);
                objEmrBa.TongketbaSotoXetnghiem = Utility.Int16Dbnull(txtB_XetNghiem.Text);
                objEmrBa.TongketbaSotoKhac = Utility.Int16Dbnull(txtB_Khac.Text);
                objEmrBa.TongketbaNgay = dtpB_NgayTongKet.Value;
                return objEmrBa;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
                return objEmrBa;

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
            else if(e.KeyCode==Keys.F5)
            {
                PhanquyenTinhnang();
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
                txtBacsiKham.Init(txtBSDieuTri.AutoCompleteSource, txtBSDieuTri.defaultItem);
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
                    objLuotkham = Utility.getKcbLuotkham(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham);
                    objBenhnhan = Utility.getKcbBenhnhan(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham);
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
        private void FillThongtinChuyenVien()
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
                chkQLNBChuyenVienCK.Checked = Utility.ByteDbnull(pcv.TuyenChuyen, 1) == 3;
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
        NoitruPhieunhapvien objNhapvien;
        KcbPhieuchuyenvien objPhieuchuyenvien;
        DataTable dtDataBA = new DataTable();
        public EmrBa objEmrBa;
        EmrTiensubenhDacdiemlienquan objTsbDacdiemlienquan;
        EmrPhieukhambenh objPKB;
        string maBA = "";
        private bool _isSuccess = false;
        void FillData4Update()
        {
            try
            {
                maBA = "";
              
          

                objPhieuchuyenvien = new Select().From(KcbPhieuchuyenvien.Schema)
                   .Where(KcbPhieuchuyenvien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(KcbPhieuchuyenvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<KcbPhieuchuyenvien>();

                SqlQuery sqlQuery = new Select().From<EmrBa>()
                    .Where(EmrBa.Columns.MaLuotkham)
                    .IsEqualTo(objLuotkham.MaLuotkham)
                    .And(EmrBa.Columns.IdBenhnhan)
                    .IsEqualTo(Utility.Int32Dbnull(objLuotkham.IdBenhnhan));
                if (objEmrBa == null || (objEmrBa.IdBenhnhan != objLuotkham.IdBenhnhan && objEmrBa.MaLuotkham != objLuotkham.MaLuotkham))
                    objEmrBa = sqlQuery.ExecuteSingle<EmrBa>();
                //Autofill Data

                dtCacKhoa = new KCB_THAMKHAM().NoitruTimkiemlichsuBuonggiuong(objLuotkham.MaLuotkham, objBenhnhan.IdBenhnhan, "-1", -1);
                dtkhoachuyen = dtCacKhoa.Clone();
                DataRow[] arrKhoachuyen = dtCacKhoa.Select("id_chuyen>0");
                if (arrKhoachuyen.Length > 0) dtkhoachuyen = arrKhoachuyen.CopyToDataTable();
                grdQLNBKhoa.DataSource = dtkhoachuyen;
                DataRow[] arrKhoanhapvien = dtCacKhoa.Select("id_chuyen<=0");
               

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
                else
                    dtQLNBVaoVien.ResetText();
                if (objBenhnhan.NgayRavien.HasValue)
                    dtpRavien_ngay.Value = objBenhnhan.NgayRavien.Value;//.Value.ToString("dd/MM/yyyy HH:mm:ss");
                else
                    dtpRavien_ngay.ResetText();
                txtQLNBTongSoNgayDieuTri.Text = Utility.sDbnull(objLuotkham.SongayDieutri);
                GetChanDoanNoitru();
                FillThongtinRavien();
                FillThongtinChuyenVien();
                FillTongketBenhAn();
                txtCDKhiVaoDieuTri.Text = Name_Khoa_NoITru;
                txtCDMaKhiVaoDieuTri.Text = ICD_Khoa_NoITru;
                
                if (objEmrBa != null)
                {
                    m_enAct = action.Update;
                    cboLoaiBA.SelectedIndex = Utility.GetSelectedIndex(cboLoaiBA, objEmrBa.LoaiBa);
                    maBA = objEmrBa.MaBa;
                    dtDataBA = SPs.EmrBaLaythongtin(-1, "", objBenhnhan.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                    DataRow dr = dtDataBA.Rows[0];
                    try
                    {
                        txtIDBenhAn.Text = Utility.sDbnull(objEmrBa.IdBa);
                        txtMaBenhAn.Text = Utility.sDbnull(objEmrBa.MaBa);
                        //txtBenhNgoai_Khoa.Text = Utility.sDbnull(objEmrBa.BenhNgoaiKhoa);
                        if (objEmrBa.VaovienNgay.HasValue)
                            dtQLNBVaoVien.Value = objEmrBa.VaovienNgay.Value;
                        else
                            dtQLNBVaoVien.ResetText();
                        chkQLNBCapCuu.Checked= Utility.Bool2Bool(objEmrBa.VaovienCapcuu);
                        chkQLNBKKB.Checked = Utility.Bool2Bool(objEmrBa.VaovienKkb);
                        chkQLNBKhoaDieuTri.Checked = Utility.Bool2Bool(objEmrBa.VaovienKhoadieutri);
                        chkQLNBCoQuanYTe.Checked = Utility.Bool2Bool(objEmrBa.NoigioithieuCoquanyte);
                        chkQLNBCoQuanYTe.Checked = Utility.Bool2Bool(objEmrBa.NoigioithieuTuden);
                        chkQLNBCoQuanYTe.Checked = Utility.Bool2Bool(objEmrBa.NoigioithieuKhac);
                        txtQLNBLanVaoVien.Text = Utility.sDbnull(objEmrBa.VaovienLanthu);
                       
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

                       
                       
                        lblqlbnKhoa.Text = objEmrBa.VaovienTenkhoa;
                        lblMakhoavao.Text = objEmrBa.VaovienMakhoa;
                        chkQLNBTuyenTren.Checked = Utility.Bool2Bool(objEmrBa.ChuyenvienTuyentren);
                        chkQLNBTuyenDuoi.Checked = Utility.Bool2Bool(objEmrBa.ChuyenvienTuyenduoi);
                        chkQLNBChuyenVienCK.Checked = Utility.Bool2Bool(objEmrBa.ChuyenvienKhac);
                        txtQLNBChuyenVienNoiChuyenDen.Text = Utility.sDbnull(objEmrBa.ChuyenvienNoichuyenden);
                        if (objEmrBa.RavienNgay.HasValue)
                            dtpRavien_ngay.Value = objEmrBa.RavienNgay.Value;
                        else
                            dtpRavien_ngay.ResetText();
                        chkQLNBRaVienRavien.Checked = Utility.Bool2Bool(objEmrBa.RavienRavien);
                        chkQLNBRavienXinVe.Checked = Utility.Bool2Bool(objEmrBa.RavienXinve);
                        chkQLNBRavienBoVe.Checked = Utility.Bool2Bool(objEmrBa.RavienBove);
                        chkQLNBRavienDuaVe.Checked = Utility.Bool2Bool(objEmrBa.RavienDuave);
                        txtQLNBTongSoNgayDieuTri.Text = Utility.sDbnull(objEmrBa.RavienTongsongayDieutri);
                        txtCDNoiChuyenDen.Text = Utility.sDbnull(objEmrBa.CdNoichuyenden);
                        txtCDMaNoiChuyenDen.Text = Utility.sDbnull(objEmrBa.CdNoichuyendenMa);
                        txtCDKKBCapCuu.Text = Utility.sDbnull(objEmrBa.CdKkbCapcuu);
                        txtCDMaKKBCapCuu.Text = Utility.sDbnull(objEmrBa.CdKkbCapcuuMa);
                        txtCDKhiVaoDieuTri.Text = Utility.sDbnull(objEmrBa.CdKhoadieutri);
                        txtCDMaKhiVaoDieuTri.Text = Utility.sDbnull(objEmrBa.CdKhoadieutriMa);
                        txtCDRavienTenBenhChinh.Text = Utility.sDbnull(objEmrBa.RavienTenBenhchinh);
                        txtCDRavienMaBenhChinh.Text = Utility.sDbnull(objEmrBa.RavienMaBenhchinh);
                        txtCDRavienTenBenhKemTheo.Text = Utility.sDbnull(objEmrBa.RavienTenBenhphu);
                        txtCDRavienMaBenhKemTheo.Text = Utility.sDbnull(objEmrBa.RavienMaBenhphu);
                        chkCDThuThuat.Checked = Utility.Bool2Bool(objEmrBa.CdThuthuat);
                        chkCDPhauThuat.Checked = Utility.Bool2Bool(objEmrBa.CdPhauthuat);
                        chkCDTaiBien.Checked = Utility.Bool2Bool(objEmrBa.CdTaibien);
                        chkCDBienChung.Checked = Utility.Bool2Bool(objEmrBa.CdTaibienBienchungKhac);
                        //Tình trạng ra viện
                        chkTTRVKhoi.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienKetquadieutriKhoi);
                        chkTTRVDoGiam.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienKetquadieutriDogiam);
                        chkTTRVKhongThayDoi.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienKetquadieutriKhongthaydoi);
                        chkTTRVNangHon.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienKetquadieutriNanghon);
                        chkTTRVTuVong.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienKetquadieutriTuvong);

                        chkTTRVLanhTinh.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienGpbLanhtinh);
                        chkTTRVNghiNgo.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienGpbNghingo);
                        chkTTRVAcTinh.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienGpbActinh);
                        if (objEmrBa.TinhtrangravienThoigianTuvong.HasValue)
                            dtpNgaytuvong.Value = objEmrBa.TinhtrangravienThoigianTuvong.Value;
                        else
                            dtpNgaytuvong.ResetText();
                        chkttrvDoBenh.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienLydotuvongDobenh);
                        chkttrvDoTaiBien.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienLydotuvongDotaibiendieutri);
                        chkttrvKhac.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienLydotuvongKhac);
                        chkttrvTrong24GioVaoVien.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienThoigiantuvongTrong24h);
                        chkttrvSau24Gio.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienThoigiantuvongSau24h);
                       
                        txtTTRVNguyenNhanChinhTuVong.Text = Utility.sDbnull(objEmrBa.TinhtrangravienNguyennhantuvong);
                        chkTTRVChandoanGiaiphauTuthi.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienKhamnghiemtuthi);
                        txtTTRVChandoanGiaiphauTuthi.Text = Utility.sDbnull(objEmrBa.TinhtrangravienChandoangiauphaututhi);
                        //Tờ 2
                        txtBenhAnLyDoNhapVien._Text = Utility.sDbnull(objEmrBa.VaovienLydovaovien);// Utility.sDbnull(dr["BaLdvv"].ToString());
                        txtBenhAnVaoNgayThu.Text = Utility.sDbnull(objEmrBa.VaovienVaongaythucuabenh);
                        txtBenhAnQuaTrinhBenhLy.Text = Utility.sDbnull(objEmrBa.TongketbaQuatrinhbenhlyDienbienlamsang);// Utility.sDbnull(dr["BaQtbl"].ToString());
                        txtBenhAnTiensuBanthan.Text = Utility.sDbnull(objEmrBa.HoibenhTiensubanthan);
                        
                        chkDiUng.Checked = Utility.Bool2Bool(objEmrBa.TsbDiung);
                        chkMaTuy.Checked = Utility.Bool2Bool(objEmrBa.TsbMatuy);
                        chkRuouBia.Checked = Utility.Bool2Bool(objEmrBa.TsbRuoubia);
                        chkThuocLa.Checked = Utility.Bool2Bool(objEmrBa.TsbThuocla);
                        chkThuocLao.Checked = Utility.Bool2Bool(objEmrBa.TsbThuoclao);
                        chkKhac.Checked = Utility.Bool2Bool(objEmrBa.TsbKhac);
                        txtDiUng.Text = Utility.sDbnull(objEmrBa.TsbThoigianDiung);
                        txtMaTuy.Text = Utility.sDbnull(objEmrBa.TsbThoigianMatuy);
                        txtRuouBia.Text = Utility.sDbnull(objEmrBa.TsbThoigianRuoubia);
                        txtThuocLa.Text = Utility.sDbnull(objEmrBa.TsbThoigianThuocla);
                        txtThuocLao.Text = Utility.sDbnull(objEmrBa.TsbThoigianThuoclao);
                        txtThoigianKhac.Text = Utility.sDbnull(objEmrBa.TsbThoigianKhac);
                        txtBenhAnGiaDinh.Text = Utility.sDbnull(objEmrBa.HoibenhTiensugiadinh);// Utility.sDbnull(dr["BaGiaDinh"].ToString());
                        
                        txtMach.Text = Utility.sDbnull(objEmrBa.KbMach);
                        txtNhietDo.Text = Utility.sDbnull(objEmrBa.KbNhietdo);
                        txtha.Text = Utility.sDbnull(objEmrBa.KbHuyetap);
                        txtNhipTho.Text = Utility.sDbnull(objEmrBa.KbNhiptho);
                        txtCanNang.Text = Utility.sDbnull(objEmrBa.KbCannang);
                        txtChieuCao.Text = Utility.sDbnull(objEmrBa.KbChieucao);
                        tinhBMI();
                        txtBenhAnToanThan.Text = Utility.sDbnull(objEmrBa.KhambenhToanthan);// Utility.sDbnull(dr["KbToanThan"].ToString());
                        txtBenhAnTuanHoan.Text = Utility.sDbnull(objEmrBa.KhambenhTuanhoan);
                        txtBenhAnHoHap.Text = Utility.sDbnull(objEmrBa.KhambenhHohap);
                        txtBenhAnTieuHoa.Text = Utility.sDbnull(objEmrBa.KhambenhTieuhoa);
                        txtBenhAnThanTietNieuSinhDuc.Text = Utility.sDbnull(objEmrBa.KhambenhThantietnieusinhduc);
                        txtBenhAnThanKinh.Text = Utility.sDbnull(objEmrBa.KhambenhThankinh);
                        txtBenhAnCoXuongKhop.Text = Utility.sDbnull(objEmrBa.KhambenhCoxuongkhop);
                        txtBenhAnTaiMuiHong.Text = Utility.sDbnull(objEmrBa.KhambenhTaimuihong);
                        txtBenhAnRangHamMat.Text = Utility.sDbnull(objEmrBa.KhambenhRanghammat);
                        txtBenhAnMat.Text = Utility.sDbnull(objEmrBa.KhambenhMat);
                        txtBenhAnNoiTiet.Text = Utility.sDbnull(objEmrBa.KhambenhNoitietDinhduongBenhlykhac);
                        txtBenhAnCacXetNghiem.Text = Utility.sDbnull(objEmrBa.KhambenhXetnghiemClsCanlam);
                        txtBenhAnTomTatBenhAn.Text = Utility.sDbnull(objEmrBa.KhambenhTomtatbenhan);
                        txtBenhAnBenhChinh.Text = Utility.sDbnull(objEmrBa.CdKhivaokhoadieutriBenhchinh);
                        txtBenhAnBenhKemTheo.Text = Utility.sDbnull(objEmrBa.CdKhivaokhoadieutriBenhphu);
                        txtBenhAnPhanBiet.Text = Utility.sDbnull(objEmrBa.CdKhivaokhoadieutriPhanbiet);
                        txtBenhAnTienLuong.Text = Utility.sDbnull(objEmrBa.KhambenhTienluong);
                        txtBenhAnHuongDieuTri.Text = Utility.sDbnull(objEmrBa.KhambenhHuongdieutri);
                        txtTKBAQuaTrinhBenhLy.Text = Utility.sDbnull(objEmrBa.TongketbaQuatrinhbenhlyDienbienlamsang);
                        txtTKBATTomTatKetQua.Text = Utility.sDbnull(objEmrBa.TongketbaTomtatKqcls);
                        txtTKBAPhuongPhapDieuTri.Text = Utility.sDbnull(objEmrBa.TongketbaPhuongphapdieutri);
                        txtTKBATinhTrangRaVien.Text = Utility.sDbnull(objEmrBa.TongketbaTinhtrangNguoiravien);// Utility.sDbnull(dr["TkbaTtrv"].ToString());
                        txtTKBAHuongDieuTri.Text = Utility.sDbnull(objEmrBa.TongketbaHuongdieutritieptheo);// Utility.sDbnull(dr["TkbaHdt"].ToString());

                        txtNguoiGiaoHoSo.Text = Utility.sDbnull(objEmrBa.TongketbaNguoigiaoHoso);
                        txtNguoiNhanHoSo.Text = Utility.sDbnull(objEmrBa.TongketbaNguoiNhanhoso);
                        txtBSDieuTri.SetCode( Utility.sDbnull(objEmrBa.MabacsiDieutri));
                       // txtBSlamBA.SetCode(Utility.sDbnull(objEmrBa.b));
                        txtB_CTScanner.Text = Utility.sDbnull(objEmrBa.TongketbaSotoCt);
                        txtB_Xquang.Text = Utility.sDbnull(objEmrBa.TongketbaSotoXquang);
                        txtB_SieuAm.Text = Utility.sDbnull(objEmrBa.TongketbaSotoSieuam);
                        txtB_XetNghiem.Text = Utility.sDbnull(objEmrBa.TongketbaSotoXetnghiem);
                        txtB_Khac.Text = Utility.sDbnull(objEmrBa.TongketbaSotoKhac);
                        if (objEmrBa.TongketbaNgay.HasValue)
                            dtpB_NgayTongKet.Value = objEmrBa.TongketbaNgay.Value;
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
                    chkQLNBCapCuu.Checked = false;
                    chkQLNBKKB.Checked = true;
                    chkQLNBKhoaDieuTri.Checked = false;
                    //Trang 2
                    FillThongtinNhapvien();
                    FillDacdiemLienquan();
                    //Trang 3
                    FillPhieuKCB();
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
        void FillPhieuKCB()
        {
            objPKB = new Select().From(EmrPhieukhambenh.Schema)
                 .Where(EmrPhieukhambenh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                 .And(EmrPhieukhambenh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                 .ExecuteSingle<EmrPhieukhambenh>();
            if (objPKB != null)
            {
                dtpNgayKham.Value = objPKB.NgayKham;
                txtBacsiKham.SetId(objPKB.IdBacsi);
                txtBenhAnToanThan.Text = Utility.sDbnull(objPKB.ToanThan);// Utility.sDbnull(dr["KbToanThan"].ToString());
                txtBenhAnTuanHoan.Text = Utility.sDbnull(objPKB.Tuanhoan);
                txtBenhAnHoHap.Text = Utility.sDbnull(objPKB.Hohap);
                txtBenhAnTieuHoa.Text = Utility.sDbnull(objPKB.Tieuhoa);
                txtBenhAnThanTietNieuSinhDuc.Text = Utility.sDbnull(objPKB.Thantietnieusinhduc);
                txtBenhAnThanKinh.Text = Utility.sDbnull(objPKB.Thankinh);
                txtBenhAnCoXuongKhop.Text = Utility.sDbnull(objPKB.Coxuongkhop);
                txtBenhAnTaiMuiHong.Text = Utility.sDbnull(objPKB.Taimuihong);
                txtBenhAnRangHamMat.Text = Utility.sDbnull(objPKB.Ranghammat);
                txtBenhAnMat.Text = Utility.sDbnull(objPKB.Mat);
            }
        }
        void FillDacdiemLienquan()
        {
            objTsbDacdiemlienquan = new Select().From(EmrTiensubenhDacdiemlienquan.Schema)
             .Where(EmrTiensubenhDacdiemlienquan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
             .And(EmrTiensubenhDacdiemlienquan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
             .ExecuteSingle<EmrTiensubenhDacdiemlienquan>();
            if (objTsbDacdiemlienquan != null)
            {
                chkDiUng.Checked = Utility.Bool2Bool(objTsbDacdiemlienquan.TsbDiung);
                chkMaTuy.Checked = Utility.Bool2Bool(objTsbDacdiemlienquan.TsbMatuy);
                chkRuouBia.Checked = Utility.Bool2Bool(objTsbDacdiemlienquan.TsbRuoubia);
                chkThuocLa.Checked = Utility.Bool2Bool(objTsbDacdiemlienquan.TsbThuocla);
                chkThuocLao.Checked = Utility.Bool2Bool(objTsbDacdiemlienquan.TsbThuoclao);
                chkKhac.Checked = Utility.Bool2Bool(objTsbDacdiemlienquan.TsbKhac);
                txtDiUng.Text = Utility.sDbnull(objTsbDacdiemlienquan.TsbThoigianDiung);
                txtMaTuy.Text = Utility.sDbnull(objTsbDacdiemlienquan.TsbThoigianMatuy);
                txtRuouBia.Text = Utility.sDbnull(objTsbDacdiemlienquan.TsbThoigianRuoubia);
                txtThuocLa.Text = Utility.sDbnull(objTsbDacdiemlienquan.TsbThoigianThuocla);
                txtThuocLao.Text = Utility.sDbnull(objTsbDacdiemlienquan.TsbThoigianThuoclao);
                txtThoigianKhac.Text = Utility.sDbnull(objTsbDacdiemlienquan.TsbThoigianKhac);
            }
        }
        void FillThongtinNhapvien()
        {
            objNhapvien = new Select().From(NoitruPhieunhapvien.Schema)
                   .Where(NoitruPhieunhapvien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(NoitruPhieunhapvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieunhapvien>();
            if (objNhapvien != null)
            {
                txtBenhAnLyDoNhapVien._Text = Utility.sDbnull(objNhapvien.LydoNhapvien);
                txtBenhAnTiensuBanthan.Text = Utility.sDbnull(objNhapvien.TsuBanthan);
                txtBenhAnGiaDinh.Text = Utility.sDbnull(objNhapvien.TsuGiadinh);
                txtBenhAnQuaTrinhBenhLy.Text = Utility.sDbnull(objNhapvien.QuatrinhBenhly);
                txtBenhAnToanThan.Text = Utility.sDbnull(objNhapvien.KhamToanthan);
            }
        }
        VKcbLuotkham objBenhnhan = null;
        KcbLuotkham objLuotkham = null;
        private void SinhMaBenhAn()
        {
            //txtMaBenhAn.Text = THU_VIEN_CHUNG.SinhMaBenhAn_NoiTru();
            string MaxMaBenhAN = "";
            StoredProcedure sp = SPs.EmrBaSinhMaBA(cboLoaiBA.SelectedValue.ToString(), MaxMaBenhAN);
            sp.Execute();
            sp.OutputValues.ForEach(delegate(object objOutput) { MaxMaBenhAN = (String)objOutput; });

            txtMaBenhAn.Text = MaxMaBenhAN;

        }
        void ModifyCommand()
        {
            tabpageTo2.Enabled = tabpageTo3.Enabled = tabpageTo4.Enabled = objLuotkham != null;
            btnInto2.Enabled = btnInto3.Enabled = Into1.Enabled = btnInto4.Enabled = button1.Enabled = btnInVoBA.Enabled = objLuotkham != null && objEmrBa!=null;
            cmdXoaBenhAn.Enabled = objLuotkham != null && objEmrBa != null;
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
               
                objEmrBa = EmrBa.FetchByID(Utility.Int64Dbnull( txtIDBenhAn.Text));
                if (objEmrBa == null)
                {
                    Utility.ShowMsg("Bạn chưa chọn bệnh án nào để xóa hoặc bệnh án muốn xóa không tồn tại trong hệ thống. Vui lòng gõ lại mã lượt khám để kiểm tra");
                    ucThongtinnguoibenh_v31.txtMaluotkham.Focus();
                    ucThongtinnguoibenh_v31.txtMaluotkham.SelectAll();
                    return;
                }
                EmrHosoluutru hosoba = new Select().From(EmrHosoluutru.Schema)
                    .Where(EmrHosoluutru.Columns.IdBa).IsEqualTo(objEmrBa.IdBa)
                    .And(EmrHosoluutru.Columns.MaBa).IsEqualTo(objEmrBa.MaBa)
                    .And(EmrHosoluutru.Columns.LoaiBa).IsEqualTo(objEmrBa.LoaiBa)
                    .And(EmrHosoluutru.Columns.IdBenhnhan).IsEqualTo(objEmrBa.IdBenhnhan)
                     .And(EmrHosoluutru.Columns.MaLuotkham).IsEqualTo(objEmrBa.MaLuotkham)
                    .ExecuteSingle<EmrHosoluutru>();
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
                if (objEmrBa != null && (Utility.Coquyen("EMR_XOA_BA") || globalVariables.UserName == objEmrBa.NguoiTao))
                {
                    if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin bệnh án đang chọn không ?", "Thông báo", true))
                    {
                        using (var Scope = new TransactionScope())
                        {
                            using (var dbScope = new SharedDbConnectionScope())
                            {
                               new Delete().From(EmrBa.Schema)
                                     .Where(EmrBa.Columns.IdBa).IsEqualTo(objEmrBa.IdBa)
                                     .And(EmrBa.Columns.LoaiBa).IsEqualTo(objEmrBa.LoaiBa)
                                     .And(EmrBa.Columns.MaCoso).IsEqualTo(objEmrBa.MaCoso)
                                     .Execute();
                              new Delete().From(EmrHosoluutru.Schema)
                                    .Where(EmrHosoluutru.Columns.IdBa).IsEqualTo(objEmrBa.IdBa)
                                    .And(EmrHosoluutru.Columns.LoaiBa).IsEqualTo(objEmrBa.LoaiBa)
                                    .And(EmrBa.Columns.MaCoso).IsEqualTo(objEmrBa.MaCoso)
                                    .Execute();
                              Utility.Log("frm_BenhAn_NoiKhoa", globalVariables.UserName, string.Format("Xóa bệnh án id={0}, loại BA={1}, mã BA={2} của người bệnh id ={3}, mã lần khám {4} thành công",objEmrBa.IdBa,objEmrBa.LoaiBa,objEmrBa.MaBa,objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham), newaction.Delete, "UI");
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
                    Utility.ShowMsg("Bạn không có quyền xóa BA(EMR_XOA_BA) hoặc không phải là người tạo Bệnh án");
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
            objEmrBa = null;
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
            EmrTongketBenhan objTKBA =new Select().From(EmrTongketBenhan.Schema)
                .Where(EmrTongketBenhan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrTongketBenhan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteSingle<EmrTongketBenhan>();
            if (objTKBA == null || objTKBA.Id <= 0)
            {
                Utility.ShowMsg("Bạn cần tạo Tóm tắt hồ sơ bệnh án trước khi thực hiện in");
                return;
            }
            clsInBA.InTomTatBA(objTKBA);
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
               
                if (objEmrBa == null || objEmrBa.IdBa <= 0)
                {
                    Utility.ShowMsg("Bạn cần tạo Bệnh án nội trú trước khi thực hiện in");
                    return;
                }
                DataTable dtData = SPs.EmrBaLaythongtinIn(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham).GetDataSet().Tables[0];
                DataRow drData = dtData.Rows[0];
                List<string> lstcheckboxfields = new List<string>();
                Dictionary<string, string> dicMF = new Dictionary<string, string>();
                foreach (string chkField in lstcheckboxfields)
                {
                    dicMF.Add(chkField, Utility.Byte2Bool(drData[chkField]) ? "0" : "1");
                }
                string checkboxFieldsFile = AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\BA_CHECKED_FIELDS.txt";
                lstcheckboxfields = Utility.GetFirstValueFromFile(checkboxFieldsFile).Split(',').ToList<string>();
                NoitruPhieuravien objPhieuRavien = new Select().From(NoitruPhieuravien.Schema)
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
                drData["p145_1"] =objPhieuRavien!=null? Utility.FormatDateTime_giophut_ngay_thang_nam(objPhieuRavien.NgayRavien, ""):".......... giờ ....... ngày ........./........./.............";//ra viện
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
                if (toBA == 1) tenToBA = "BA01_BANOIKHOA_TO1.doc";
                else if (toBA == 0) tenToBA = "BA01_BANOIKHOA_BIA.doc";
                else if (toBA == 2) tenToBA = "BA01_BANOIKHOA_TO2.doc";
                else if (toBA == 3) tenToBA = "BA01_BANOIKHOA_TO3.doc";
                else if (toBA == 4) tenToBA = "BA01_BANOIKHOA_TO4.doc";
                else tenToBA = "BA01_BANOIKHOA.doc";
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
                               Path.GetFileNameWithoutExtension(PathDoc), "EmrBa", objLuotkham.MaLuotkham, Utility.sDbnull(objEmrBa.IdBa), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


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
                            string tbl_idx = AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\BA01_BANOIKHOA_TBLIDX.txt";
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
        EmrTongketBenhan objTKBA;
        void FillTongketBenhAn()
        {
            try
            {
                objTKBA=  new Select().From(EmrTongketBenhan.Schema)
                    .Where(EmrTongketBenhan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(EmrTongketBenhan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .ExecuteSingle<EmrTongketBenhan>();
                if (objTKBA != null)
                {
                    dtpNgayTKBA.Value = objTKBA.NgayTtba.Value;
                    txtTKBAQuaTrinhBenhLy.Text = objTKBA.QuatrinhbenhlyDienbienlamsang;
                    txtTKBATTomTatKetQua.Text = objTKBA.TomtatKqcls;
                    txtTKBAPhuongPhapDieuTri.Text = objTKBA.PhuongphapDieutri;
                    txtTKBATinhTrangRaVien.Text = objTKBA.TinhtrangRavienMota;
                    txtTKBAHuongDieuTri.Text = objTKBA.HuongDieutri;

                    txtNguoiGiaoHoSo.Text = Utility.sDbnull(objTKBA.NguoigiaoHoso);
                    txtNguoiNhanHoSo.Text = Utility.sDbnull(objTKBA.NguoiNhanhoso);

                    txtB_CTScanner.Text = Utility.sDbnull(objTKBA.SotoCt);
                    txtB_Xquang.Text = Utility.sDbnull(objTKBA.SotoXquang);
                    txtB_SieuAm.Text = Utility.sDbnull(objTKBA.SotoSieuam);
                    txtB_XetNghiem.Text = Utility.sDbnull(objTKBA.SotoXetnghiem);
                    txtB_Khac.Text = Utility.sDbnull(objTKBA.SotoKhac);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void cmdSyncTKBA_Click(object sender, EventArgs e)
        {
            FillTongketBenhAn();
        }

        private void cmdKhoitaoBA_Click(object sender, EventArgs e)
        {
            cmdSave.PerformClick();
        }

        private void cmdLaythongtinKCB_Click(object sender, EventArgs e)
        {

        }

        private void cmdDiungkhac_Click(object sender, EventArgs e)
        {
            if ( objLuotkham == null)
            {
                Utility.ShowMsg("Bạn cần chọn ít nhất một người bệnh trên danh sách người bệnh để bắt đầu nhập thông tin khám các đặc điểm liên quan");
                return;
            }
            frm_Tiensubenh_Cacdacdiemlienquan _Tiensubenh_Cacdacdiemlienquan = new frm_Tiensubenh_Cacdacdiemlienquan(objLuotkham);
            _Tiensubenh_Cacdacdiemlienquan.ShowDialog();
            FillDacdiemLienquan();
        }

        private void cmdKCB_Click(object sender, EventArgs e)
        {
            try
            {
                if ( objLuotkham == null)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất một người bệnh trên danh sách người bệnh để bắt đầu công việc khám cơ bản");
                    return;
                }
                frm_KCBCoban _KCBCoban = new frm_KCBCoban(objLuotkham, null);
                _KCBCoban.ShowDialog();
                FillPhieuKCB();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdPhieuKCB2_Click(object sender, EventArgs e)
        {
            try
            {
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất một người bệnh trên danh sách người bệnh để bắt đầu công việc khám cơ bản");
                    return;
                }
                frm_KCBCoban _KCBCoban = new frm_KCBCoban(objLuotkham, null);
                _KCBCoban.ShowDialog();
                FillPhieuKCB();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
    }
}
