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
using VMS.HIS.EMR;
using VMS.HIS.Bus.Emr;
using System.Globalization;

namespace VMS.HIS.UI.EMR
{
    public partial class frm_BenhAn_SanKhoa : Form
    {
        public delegate void OnCreated(long id,string ma_ba, action m_enAct);
        public event OnCreated _OnCreated;
        string lstLoaiBA = "";
        DataTable dt_ThongtinNguoibenh = new DataTable();
        public frm_BenhAn_SanKhoa(string lstLoaiBA)
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
            chkttrvngoai24gioVaoVien.CheckedChanged += chkttrvSau24Gio_CheckedChanged;
            ucThongtinnguoibenh_emr_basic1._OnEnterMe += ucThongtinnguoibenh_emr_basic1__OnEnterMe;
            txtIDBenhAn.KeyDown += txtIDBenhAn_KeyDown;
            txtMaBenhAn.KeyDown += txtMaBenhAn_KeyDown;
            ucThongtinnguoibenh_emr_basic1.trangthai_noitru = 5;
            Utility.setEnterEvent(this);
          
            txtB_CTScanner.TextChanged += soluongto_TextChanged;
            txtB_Khac.TextChanged += soluongto_TextChanged;
            txtB_SieuAm.TextChanged += soluongto_TextChanged;
            txtB_XetNghiem.TextChanged += soluongto_TextChanged;
            txtB_Xquang.TextChanged += soluongto_TextChanged;
            txt_chandoan_truocphauthuat._OnEnterMe += txt_chandoan_truocphauthuat_OnEnterMe;
            txt_chandoan_sauphauthuat._OnEnterMe += txt_chandoan_sauphauthuat_OnEnterMe;
            PhanquyenTinhnang();
            txtChieuCao.Leave += txtChieucao_Leave;
            txtCanNang.Leave += txtCannang_Leave;
            grdTiensuSankhoa.MouseDoubleClick += GrdTiensuSankhoa_MouseDoubleClick;
            grdTiensuSankhoa.ColumnButtonClick += GrdTiensuSankhoa_ColumnButtonClick;
        }

        private void GrdTiensuSankhoa_ColumnButtonClick(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "XOA")
                {
                    int id = Utility.Int32Dbnull(grdTiensuSankhoa.GetValue("id"));
                    int num = new Delete().From(EmrTiensuSankhoa.Schema).Where(EmrTiensuSankhoa.Columns.Id).IsEqualTo(id).Execute();
                    if (num > 0)
                    {
                        DataRow[] rows = dt_tssk.Select("id = " + id);
                        foreach (DataRow row in rows)
                        {
                            row.Delete();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            
        }

        private void GrdTiensuSankhoa_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Utility.isValidGrid(grdTiensuSankhoa))
            {
                EmrTiensuSankhoa tssk = EmrTiensuSankhoa.FetchByID(Utility.Int64Dbnull(grdTiensuSankhoa.GetValue("Id")));
                frm_ThemtiensuSankhoa f = new frm_ThemtiensuSankhoa(objLuotkham, tssk);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    FillThongtinTienSuSanKhoa();
                }
            }

        }

        private void txtChieucao_Leave(object sender, EventArgs e)
        {
            Utility.CalculateIBM(Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtChieuCao.Text), 0), Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtCanNang.Text), 0), txtBMI);
        }

        private void txtCannang_Leave(object sender, EventArgs e)
        {
            Utility.CalculateIBM(Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtChieuCao.Text), 0), Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtCanNang.Text), 0), txtBMI);
        }
        private void txt_chandoan_truocphauthuat_OnEnterMe()
        {
            lbl_ma_chandoan_truocphauthuat.Text = txt_chandoan_truocphauthuat.MyCode;

            txt_chandoan_sauphauthuat.Focus();
            txt_chandoan_sauphauthuat.SelectAll();
        }

       private void txt_chandoan_sauphauthuat_OnEnterMe()
        {
            lbl_ma_chandoan_sauphauthuat.Text = txt_chandoan_sauphauthuat.MyCode;
        }
        void PhanquyenTinhnang()
        {
            //cmdKCB.Visible = cmdKCB.Enabled = Utility.Coquyen("EMR_THEM_PHIEUKCB");
            //chkEditPKB.Visible = chkEditPKB.Enabled = Utility.Coquyen("EMR_SUA_PHIEUKCB");
            //chkEditTKBA.Visible = chkEditTKBA.Enabled = Utility.Coquyen("EMR_SUA_TKBA");
            //txtBenhAnToanThan.ReadOnly = txtBenhAnTuanHoan.ReadOnly = txtBenhAnHoHap.ReadOnly 
            //    = txtBenhAnTieuHoa.ReadOnly = txtBenhAnThanTietNieuSinhDuc.ReadOnly = txtBenhAnThanKinh.ReadOnly 
            //    = txtBenhAnCoXuongKhop.ReadOnly = txtBenhAnTaiMuiHong.ReadOnly = txtBenhAnMat.ReadOnly 
            //    = txtBenhAnNoiTiet.ReadOnly = Utility.Coquyen("EMR_SUA_PHIEUKCB");
        }    
        void soluongto_TextChanged(object sender, EventArgs e)
        {
            txtB_Tongso.Text =( Utility.Int32Dbnull(txtB_CTScanner.Text, 0) + Utility.Int32Dbnull(txtB_Khac.Text, 0) + Utility.Int32Dbnull(txtB_SieuAm.Text, 0) + Utility.Int32Dbnull(txtB_XetNghiem.Text, 0) + Utility.Int32Dbnull(txtB_Xquang.Text, 0)).ToString();
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
                        ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text = objEmrBa.MaLuotkham;
                        ucThongtinnguoibenh_emr_basic1.Refresh(true);
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
                        ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text = objEmrBa.MaLuotkham;
                        ucThongtinnguoibenh_emr_basic1.Refresh(true);
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

        void ucThongtinnguoibenh_emr_basic1__OnEnterMe()
        {
            if (ucThongtinnguoibenh_emr_basic1.objLuotkham != null )
            {
                if (ucThongtinnguoibenh_emr_basic1.objLuotkham.TrangthaiNoitru <= 0)
                {
                    Utility.ShowMsg(string.Format("Người bệnh {0} với mã lần khám {1} đang ở trạng thái ngoại trú nên bạn không thể thực hiện tạo BA được. Vui lòng kiểm tra lại", ucThongtinnguoibenh_emr_basic1.txtTenBN.Text, ucThongtinnguoibenh_emr_basic1.objLuotkham.MaLuotkham));
                    objLuotkham = null;
                    objBenhnhan = null;
                    objEmrBa = null;
                    ClearControl();
                    ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
                    ucThongtinnguoibenh_emr_basic1.txtMaluotkham.SelectAll();
                    return;
                }
                objEmrBa = null;
                objPhieukhamSankhoa = null;
                objQttk = null;
                objChandoanSankhoa = null;
                objTspk = null;
                objPhieutheodoitaibuongde = null;
                objLuotkham = ucThongtinnguoibenh_emr_basic1.objLuotkham;
                dt_ThongtinNguoibenh = ucThongtinnguoibenh_emr_basic1.dt_ThongtinNguoibenh;
                objBenhnhan = Utility.getKcbDanhsachBenhnhan(objLuotkham);
                if (objBenhnhan.IdGioitinh != 1)//0=Nam;1=Nữ
                {
                    Utility.ShowMsg("Giới tính của người bệnh phải là Nữ mới được phép tạo bệnh án Sản khoa. Vui lòng kiểm tra lại");
                    objLuotkham = null;
                    objBenhnhan = null;
                    return;
                }
                ClearControl();
                if (!KiemTraBenhAn())
                {
                    ModifyCommand();
                    return;
                }
                FillData4Update();
                dtQLNBVaoVien.Focus();
                ModifyCommand();
            }
        }
        bool KiemTraBenhAn()
        {
            objEmrBa = new Select().From<EmrBa>()
                    .Where(EmrBa.Columns.MaLuotkham)
                    .IsEqualTo(objLuotkham.MaLuotkham)
                    .And(EmrBa.Columns.IdBenhnhan)
                    .IsEqualTo(Utility.Int32Dbnull(objLuotkham.IdBenhnhan))
                    .ExecuteSingle<EmrBa>();
            if (objEmrBa == null || (objEmrBa != null && this.lstLoaiBA.Contains(objEmrBa.LoaiBa)))
            {
                return true;
            }
            else if (objEmrBa != null && !this.lstLoaiBA.Contains(objEmrBa.LoaiBa))
            {
                Utility.ShowMsg(string.Format("Người bệnh {0} đã có {1} nên không thể tạo Bệnh án Sản khoa. Vui lòng kiểm tra lại", ucThongtinnguoibenh_emr_basic1.txtTenBN.Text, Utility.GetTenLoaiBenhAn(objEmrBa.LoaiBa)));
                objLuotkham = null;
                objBenhnhan = null; 
                return false;
            }
            return false;
        }

        #region checkbox
        private void chkttrvTrong24GioVaoVien_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvTrong24GioVaoVien.Checked == true)
            {
                chkttrvDoBenh.Checked = false;

                chkttrvDoTaiBien.Checked = false;
                chkttrvngoai24gioVaoVien.Checked = false;
            }
        }

        private void chkttrvDoTaiBien_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvDoTaiBien.Checked == true)
            {
                chkttrvDoBenh.Checked = false;
                chkttrvTrong24GioVaoVien.Checked = false;

                chkttrvngoai24gioVaoVien.Checked = false;
            }
        }

        private void chkttrvSau24Gio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvngoai24gioVaoVien.Checked == true)
            {
                chkttrvDoBenh.Checked = false;
                chkttrvTrong24GioVaoVien.Checked = false;
                chkttrvDoTaiBien.Checked = false;

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
                chkttrvngoai24gioVaoVien.Checked = false;
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
     

       
        NoitruPhieuravien objPhieuRavien;
        private void FillThongtinRavien()
        {

            objPhieuRavien=  new Select().From(NoitruPhieuravien.Schema)
                .Where(NoitruPhieuravien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
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
               Utility.GetChanDoanChinhPhu(Utility.sDbnull(objPhieuRavien.MabenhChinh, ""),
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
                chkttrvTrong24GioVaoVien.Checked = Utility.Bool2Bool(objPhieuRavien.TuvongTrong24gio);
                chkttrvngoai24gioVaoVien.Checked = Utility.Bool2Bool(objPhieuRavien.TuvongSau24h);

                txtTTRVNguyenNhanChinhTuVong.Text = Utility.sDbnull(objPhieuRavien.TuvongNguyennhanchinh);
                chkTTRVChandoanGiaiphauTuthi.Checked = Utility.Bool2Bool(objPhieuRavien.TuvongChandoangiaiphaututhi);
                txtTTRVChandoanGiaiphauTuthi.Text = Utility.sDbnull(objPhieuRavien.TuvongChandoangiaiphaututhiMota);
                chkCDTaiBien.Checked = Utility.Bool2Bool(objPhieuRavien.Taibien);
                chkCDBienChung.Checked = Utility.Bool2Bool(objPhieuRavien.Bienchung);
            }
            //txtCDRavienTenBenhChinh.Text = chandoan;
            //txtCDRavienMaBenhChinh.Text = Utility.sDbnull(mabenh);
            //txtCDRavienTenBenhKemTheo.Text = chandoanphu;
            //txtCDRavienMaBenhKemTheo.Text = mabenhphu;

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


           // txtCDKhiVaoDieuTri.Clear();
            txtCDMaKhiVaoDieuTri.Clear();
            //txtCDRavienTenBenhChinh.Clear();
            //txtCDRavienMaBenhChinh.Clear();
            //txtCDRavienTenBenhKemTheo.Clear();
            //txtCDRavienMaBenhKemTheo.Clear();
           
            chkCDTaiBien.Checked = false;
            chkCDBienChung.Checked = false;
            chk_cd_dogayme.Checked = false;
            chk_cd_donhiemkhuan.Checked = false;
            chk_cd_dokhac.Checked = false;
            chk_cd_dokhac.Checked = false;

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
            chkttrvngoai24gioVaoVien.Checked = false;
            txtTTRVNguyenNhanChinhTuVong.Clear();
            chkTTRVChandoanGiaiphauTuthi.Checked = false;
            txtTTRVChandoanGiaiphauTuthi.Clear();
            txtBenhAnLyDoNhapVien.SetDefaultItem();
            txtBenhAnVaoNgayThu.Clear();
            txtBenhAnQuaTrinhBenhLy.Clear();
            txtBenhAnTiensuBanthan.Clear();
           //Xóa thông tin khám phụ khoa
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
       
        private bool IsValidData(int trangthai)
        {
            if (objLuotkham != null)
                objLuotkham = Utility.getKcbLuotkham(objLuotkham);
            Utility.SetMsg(lblMsg, "", false);
            if (objLuotkham == null)
            {
                Utility.SetMsg(lblMsg, "Cần chọn người bệnh trước khi làm Bệnh án. Vui lòng kiểm tra lại", true);
                ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
                ucThongtinnguoibenh_emr_basic1.txtMaluotkham.SelectAll();
                return false;
            }
            if (Utility.sDbnull(cboLoaiBA.SelectedValue, "-1") == "-1")
            {
                uiTabBA.SelectedTab = tabpageTo1;
                Utility.SetMsg(lblMsg, "Cần chọn loại bệnh án", true);
                cboLoaiBA.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(txtBSlamBA.MyID, -1) <= 0)
            {
                uiTabBA.SelectedTab = tabpageTo1;
                Utility.SetMsg(lblMsg, "Bạn cần chọn Bác sĩ làm bệnh án từ danh mục Bác sĩ trong hệ thống", true);
                txtBSlamBA.Focus();
                return false;
            }
            if (trangthai == 2)
            {
                if (Utility.Int32Dbnull(txtBacsiKham.MyID, -1) <= 0)
                {
                    uiTabBA.SelectedTab = tabpageTo2;
                    Utility.SetMsg(lblMsg, "Bạn cần chọn Bác sĩ khám từ danh mục Bác sĩ trong hệ thống", true);
                    txtBacsiKham.Focus();
                    return false;
                }
                if (dtpKinhcuoidenngay.Value <= dtpKinhcuoitungay.Value)
                {
                    uiTabBA.SelectedTab = tabpageTo2;
                    Utility.SetMsg(lblMsg, "Kinh cuối đến ngày phải >= kinh cuối từ ngày", true);
                    dtpKinhcuoidenngay.Focus();
                    return false;
                }
                if (dtpDeluc.Value <= dtpVaobuongdeluc.Value)
                {
                    uiTabBA.SelectedTab = tabpageTo4;
                    Utility.SetMsg(lblMsg, "Thời gian đẻ phải >= thời gian vào đẻ", true);
                    dtpDeluc.Focus();
                    return false;
                }
                if (dtpRausoluc.Value <= dtpVaobuongdeluc.Value)
                {
                    uiTabBA.SelectedTab = tabpageTo4;
                    Utility.SetMsg(lblMsg, "Thời gian sổ rau >= thời gian đẻ", true);
                    dtpRausoluc.Focus();
                    return false;
                }
                if (Utility.Int32Dbnull(txtNguoiGiaoHoSo.MyID, -1) <= 0)
                {
                    uiTabBA.SelectedTab = tabpageTo4;
                    Utility.SetMsg(lblMsg, "Bạn cần chọn Người giao hồ sơ trong danh mục hệ thống", true);
                    txtNguoiGiaoHoSo.Focus();
                    return false;
                }
                //if (Utility.Int32Dbnull(txtNguoiNhanHoSo.MyID, -1) <= 0)
                //{
                //    uiTabBA.SelectedTab = tabpageTo4;
                //    Utility.SetMsg(lblMsg, "Bạn cần chọn Người nhận hồ sơ trong danh mục hệ thống", true);
                //    txtNguoiNhanHoSo.Focus();
                //    return false;
                //}
                if (Utility.Int32Dbnull(txtBSDieuTri.MyID, -1) <= 0)
                {
                    uiTabBA.SelectedTab = tabpageTo4;
                    Utility.SetMsg(lblMsg, "Bạn cần chọn Bác sĩ điều trị từ danh mục Bác sĩ trong hệ thống", true);
                    txtBSDieuTri.Focus();
                    return false;
                }
                if (Utility.Int32Dbnull(txtTruongkhoa.MyID, -1) <= 0)
                {
                    uiTabBA.SelectedTab = tabpageTo4;
                    Utility.SetMsg(lblMsg, "Bạn cần chọn Trưởng khoa điều trị từ danh mục Bác sĩ trong hệ thống", true);
                    txtTruongkhoa.Focus();
                    return false;
                }
                if (Utility.Int32Dbnull(txtGDBV.MyID, -1) <= 0)
                {
                    uiTabBA.SelectedTab = tabpageTo4;
                    Utility.SetMsg(lblMsg, "Bạn cần chọn Giám đốc bệnh viện", true);
                    txtGDBV.Focus();
                    return false;
                }
            }
            return true;
        }
     
        EmrDocuments emrdoc = new EmrDocuments();
        bool isSuccess = false;
        private void cmdSave_Click(object sender, EventArgs e)
        {
            LuuBA(2);
        }
        void LuuBA(int trangthai)
        {
            try
            {
                isSuccess = false;
                if (!IsValidData(trangthai)) return;
                //Phiếu khám sản khoa
                TaoPhieuKhamSanKhoa();
                //Chẩn đoán sản khoa
                TaoChandoanSanKhoa();
                //Quá trinh thai kỳ
                TaoQuatrinhThaiKy();
                //Tiền sử sản phụ khoa
                TaoTienSuPhuKhoa();
                //Phiếu theo dõi tại buồng đẻ
                TaoPhieuTheodoiTaiBuongde();
                objEmrBa = TaoEmrBa();
                if (objEmrBa.IdBa > 0)
                {
                    if (!Utility.isValidSignStatus4UpdateDelete(objLuotkham, objEmrBa.IdBa, Loaiphieu_HIS.BA_SANKHOA, "Bệnh án Sản khoa"))
                        return;
                }
                objEmrBa.TrangThai = Utility.ByteDbnull(trangthai);
                //if (objEmrBa.IdBa > 0 && objEmrBa.MaBa != maBA)
                //{
                //    if(Utility.AcceptQuestion("Mã bệnh án cũ :{0} đang khác với mã bệnh án nhập tay: {1}. Bạn có chắc chắn muốn cập nhật lại thành mã bệnh án mới","",))
                //    {
                //    }
                //}
                EmrHosoluutru hsba = null;
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
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        objEmrBa.Save();
                        new Update(KcbLuotkham.Schema)
                            .Set(KcbLuotkham.Columns.IdBsDieutrinoitruChinh).EqualTo(objEmrBa.IdBacsiDieutri)
                            .Set(KcbLuotkham.Columns.IdBa).EqualTo(objEmrBa.IdBa)
                             .Set(KcbLuotkham.Columns.LoaiBenhAn).EqualTo(objEmrBa.LoaiBa)
                                 .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                                 .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                 .Execute();
                        if (hsba != null)
                        {
                            hsba.IdBa = objEmrBa.IdBa;
                            hsba.Save();
                        }
                        if (trangthai <= 0)
                        {
                            //Thực hiện hàm refresh EMR
                            int num = 0;
                            StoredProcedure sp = SPs.EmrLaydanhsachDocumentsFromTables(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, "", 1, num);
                            sp.Execute();
                        }
                        else
                        {
                            //if (Utility.Coquyen("EMR_SUA_PHIEUKCB") && objEmrBa.IdBa > 0)
                            //{
                            TaoPhieuKCB();
                            objPKB.Save();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin phiếu khám toàn thân tại chức năng EMR cho người bệnh: {0}-{1} thành công", objEmrBa.MaLuotkham, objEmrBa.TenBenhnhan), objPKB.IsNew ? newaction.Insert : newaction.Update, "EMR");
                            //}
                            //if (Utility.Coquyen("EMR_SUA_TKBA") && objEmrBa.IdBa > 0)
                            //{
                            //    TaoPhieuTKBA();
                            //    objTKBA.Save();
                            //    if (objTKBA.IsNew)
                            //    {

                            //        emrdoc.InitDocument(objTKBA.IdBenhnhan, objTKBA.MaLuotkham, Utility.Int64Dbnull(objTKBA.Id), objTKBA.NgayTtba.Value, Loaiphieu_HIS.PHIEU_TKBA, "BA_TKBA", objTKBA.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true);
                            //        emrdoc.Save();
                            //    }
                            //    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin Tổng kết BA tại chức năng EMR cho người bệnh: {0}-{1} thành công", objEmrBa.MaLuotkham, objEmrBa.TenBenhnhan), objTKBA.IsNew ? newaction.Insert : newaction.Update, "EMR");
                            //}
                            //if (Utility.Coquyen("EMR_SUA_PHIEUKHAMSANKHOA") && objEmrBa.IdBa > 0)
                            //{

                            objPhieukhamSankhoa.Save();
                            objTspk.Save();
                            objQttk.Save();
                            objChandoanSankhoa.Save();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin phiếu khám sản khoa tại chức năng EMR cho người bệnh: {0}-{1} thành công", objEmrBa.MaLuotkham, objEmrBa.TenBenhnhan), objChandoanSankhoa.IsNew ? newaction.Insert : newaction.Update, "EMR");
                            //}
                            //if (Utility.Coquyen("EMR_SUA_PHIEUTHEODOITAIBUONGDE") && objEmrBa.IdBa > 0)
                            //{

                            objPhieutheodoitaibuongde.Save();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin phiếu theo dõi tại buồng đẻ tại chức năng EMR cho người bệnh: {0}-{1} thành công", objEmrBa.MaLuotkham, objEmrBa.TenBenhnhan), objPhieutheodoitaibuongde.IsNew ? newaction.Insert : newaction.Update, "EMR");
                            //}
                        }
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_BIA, "BA05_BASANKHOA_BIA", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", objEmrBa.LoaiBa);
                        emrdoc.Save();
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO1, "BA05_BASANKHOA_TO1", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", objEmrBa.LoaiBa);
                        emrdoc.Save();
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO2, "BA05_BASANKHOA_TO2", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", objEmrBa.LoaiBa);
                        emrdoc.Save();
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO3, "BA05_BASANKHOA_TO3", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", objEmrBa.LoaiBa);
                        emrdoc.Save();
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO4, "BA05_BASANKHOA_TO4", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", objEmrBa.LoaiBa);
                        emrdoc.Save();
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BA_SANKHOA, "BA05_BASANKHOA", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", objEmrBa.LoaiBa);
                        emrdoc.Save();

                    }
                    scope.Complete();
                    isSuccess = true;
                }
                txtIDBenhAn.Text = objEmrBa.IdBa.ToString();
                if (isSuccess)
                {
                    if (m_enAct == action.Insert)
                    {
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới BA cho bệnh nhân: {0}-{1} thành công", objEmrBa.IdBa, objEmrBa.TenBenhnhan), objEmrBa.IsNew ? newaction.Insert : newaction.Update, "UI");
                        MessageBox.Show(trangthai == 0 ? "Đã khởi tạo Bệnh án thành công" : "Đã thêm mới Bệnh án thành công");
                        cmdXoaBenhAn.Enabled = cmdPrint.Enabled = true;
                        if (_OnCreated != null) _OnCreated(objEmrBa.IdBa, objEmrBa.MaBa, action.Insert);
                        m_enAct = action.Update;
                    }
                    else if (m_enAct == action.Update)
                    {
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật Bệnh án cho bệnh nhân: {0}-{1} thành công", objEmrBa.IdBa, objEmrBa.TenBenhnhan), objEmrBa.IsNew ? newaction.Insert : newaction.Update, "UI");
                        if (_OnCreated != null) _OnCreated(objEmrBa.IdBa, objEmrBa.MaBa, action.Update);
                        MessageBox.Show("Đã cập nhật Bệnh án thành công");
                        m_enAct = action.Update;
                    }
                }
                EnableBA();
                //Utility.ShowMsg("Lưu thông tin thành công", "Thông báo");
                dtDataBA = SPs.EmrBaLaythongtinIn(-1, "", LoaiBA.BA_SANKHOA, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                _isSuccess = true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                //if (objEmrBa != null && _isSuccess)
                //{
                //    new Update(KcbLuotkham.Schema)
                //        .Set(KcbLuotkham.Columns.IdBa).EqualTo(objEmrBa.IdBa)
                //        .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                //        .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                //    // EmrThemBenhAn();
                //}

            }
        }
        void TaoPhieuTheodoiTaiBuongde()
        {
            objPhieutheodoitaibuongde = new Select().From(EmrPhieutheodoiTaibuongde.Schema)
                  .Where(EmrPhieutheodoiTaibuongde.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                  .And(EmrPhieutheodoiTaibuongde.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                  .ExecuteSingle<EmrPhieutheodoiTaibuongde>();
            if (objPhieutheodoitaibuongde == null || objPhieutheodoitaibuongde.Id <= 0)
            {
                objPhieutheodoitaibuongde = new EmrPhieutheodoiTaibuongde();
                objPhieutheodoitaibuongde.IsNew = true;
                objPhieutheodoitaibuongde.NgayTao = DateTime.Now;
                objPhieutheodoitaibuongde.NguoiTao = globalVariables.UserName;
            }
            else
            {
                objPhieutheodoitaibuongde.IsNew = false;
                objPhieutheodoitaibuongde.MarkOld();
                objPhieutheodoitaibuongde.NgaySua = DateTime.Now;
                objPhieutheodoitaibuongde.NguoiSua = globalVariables.UserName;
            }
            objPhieutheodoitaibuongde.IdBenhnhan = objLuotkham.IdBenhnhan;
            objPhieutheodoitaibuongde.MaLuotkham = objLuotkham.MaLuotkham;
            objPhieutheodoitaibuongde.Vaobuongdeluc = dtpVaobuongdeluc.Value;
            objPhieutheodoitaibuongde.Nguoitheodoi = Utility.sDbnull(txtTennguoitheodoi.Text);
            objPhieutheodoitaibuongde.Chucdanh = Utility.sDbnull(txtChucdanhnguoitheodoi.Text);

            objPhieutheodoitaibuongde.Deluc = dtpDeluc.Value;
            objPhieutheodoitaibuongde.Apgar1phut = Utility.sDbnull(txt1phut.Text);
            objPhieutheodoitaibuongde.Apgar5phut = Utility.sDbnull(txt5phut.Text);
            objPhieutheodoitaibuongde.Apgar10phut = Utility.sDbnull(txt10phut.Text);
            objPhieutheodoitaibuongde.TresosinhCannang = Utility.Int16Dbnull(nmrCannangtresosinh.Value);
            objPhieutheodoitaibuongde.TresosinhCao = Utility.Int16Dbnull(nmrcao.Value);
            objPhieutheodoitaibuongde.TresosinhVongdau = Utility.Int16Dbnull(nmrvongdau.Value);

            objPhieutheodoitaibuongde.TresosinhDonthaiTrai = optDonthaiTrai.Checked;
            objPhieutheodoitaibuongde.TresosinhDonthaiGai = optDonthaiGai.Checked;
            objPhieutheodoitaibuongde.TresosinhDathaiTrai = optDathaiTrai.Checked;
            objPhieutheodoitaibuongde.TresosinhDathaiGai = optDathaiGai.Checked;
            objPhieutheodoitaibuongde.TresosinhTatbamsinh = chkTatbamsinh.Checked;
            objPhieutheodoitaibuongde.TresosinhCohaumon = chkCohaumon.Checked;
            objPhieutheodoitaibuongde.TresosinhCuthetatbamsinh = Utility.sDbnull(txtCuthetatbamsinh.Text);
            objPhieutheodoitaibuongde.TresosinhTinhtrangsaude = Utility.sDbnull(txtTinhtrangtresosinhsaukhide.Text);
            objPhieutheodoitaibuongde.TresosinhXulyvaketqua = Utility.sDbnull(txtXulyvaketquaTresosinh.Text);

            objPhieutheodoitaibuongde.SorauBoc = optRauboc.Checked;
            objPhieutheodoitaibuongde.SorauSo = optRauso.Checked;

            objPhieutheodoitaibuongde.SorauLuc = dtpRausoluc.Value;
            objPhieutheodoitaibuongde.SorauCachsorau = Utility.sDbnull(txtCachsorau.Text);
            objPhieutheodoitaibuongde.SorauMatmang = Utility.sDbnull(txtMatmang.Text);
            objPhieutheodoitaibuongde.SorauMatmui = Utility.sDbnull(txtMatmui.Text);
            objPhieutheodoitaibuongde.SorauBanhrau = Utility.sDbnull(txtBanhrau.Text);
            objPhieutheodoitaibuongde.SorauCannang = Utility.Int16Dbnull(nmrCannangRau.Value);
            objPhieutheodoitaibuongde.SorauRaucuonco = chkRaucuonco.Checked;
            objPhieutheodoitaibuongde.CuongrauDai = Utility.Int16Dbnull(nmrCuongrau.Value);
            objPhieutheodoitaibuongde.SorauChaymausauso = chkCochaymausauso.Checked;
            objPhieutheodoitaibuongde.SorauLuongmaumat = Utility.Int16Dbnull(nmrLuongmaumat.Value);
            objPhieutheodoitaibuongde.SorauKiemsoattucung = chkKiemsoattucung.Checked;
            objPhieutheodoitaibuongde.SorauXulyvaketqua = Utility.sDbnull(txtXulyvaketquaRau.Text);

            objPhieutheodoitaibuongde.SanphuDaniemmac = Utility.sDbnull(txtSanphuDaniemmac.Text);
            objPhieutheodoitaibuongde.SanphuPhuongphapdeThuong = optDethuong.Checked;
            objPhieutheodoitaibuongde.SanphuPhuongphapdeForceps = optForceps.Checked;
            objPhieutheodoitaibuongde.SanphuPhuongphapdeGiachut = optGiachut.Checked;
            objPhieutheodoitaibuongde.SanphuPhuongphapdePt = optPhauthuat.Checked;
            objPhieutheodoitaibuongde.SanphuPhuongphapdeDechihuy = optDechihuy.Checked;
            objPhieutheodoitaibuongde.SanphuPhuongphapdeKhac = optKhac.Checked;
            objPhieutheodoitaibuongde.SanphuLydocanthiep = Utility.sDbnull(txtLydocanthiep.Text);
            objPhieutheodoitaibuongde.SanphuTangsinhmonKhongrach = optTangsinhmonKhongrach.Checked;
            objPhieutheodoitaibuongde.SanphuTangsinhmonRach = optTangsinhmonRach.Checked;
            objPhieutheodoitaibuongde.SanphuTangsinhmonCat = optTangsinhmonCat.Checked;
            objPhieutheodoitaibuongde.SanphuPhuongphapkhauvaloaichi = chkPhuongphapkhauvaloaichi.Checked;
            if (chkPhuongphapkhauvaloaichi.Checked)
            {
                
                objPhieutheodoitaibuongde.SanphuPhuongphapkhauvaloaichiMota = Utility.sDbnull(txtPhuongphapkhauvaloaichi.Text);
                objPhieutheodoitaibuongde.SanphuSomuikhau = Utility.Int16Dbnull(nmrSomuikhau.Value);
            }
            else
            {
                objPhieutheodoitaibuongde.SanphuPhuongphapkhauvaloaichiMota = "";
                objPhieutheodoitaibuongde.SanphuSomuikhau = 0;
            }    

            objPhieutheodoitaibuongde.SanphuCotucungRach = optCotucungRach.Checked;
            objPhieutheodoitaibuongde.SanphuCotucungKhongrach = optCotucungKhongrach.Checked;
        }
        void TaoPhieuKhamSanKhoa()
        {
            objPhieukhamSankhoa = new Select().From(EmrPhieukhamSankhoa.Schema)
                .Where(EmrPhieukhamSankhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrPhieukhamSankhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteSingle<EmrPhieukhamSankhoa>();
            if (objPhieukhamSankhoa != null && objPhieukhamSankhoa.Id > 0)
            {
                objPhieukhamSankhoa.MarkOld();
                objPhieukhamSankhoa.NguoiSua = globalVariables.UserName;
                objPhieukhamSankhoa.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
            }
            else
            {
                objPhieukhamSankhoa = new EmrPhieukhamSankhoa();
                objPhieukhamSankhoa.IsNew = true;
                objPhieukhamSankhoa.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                objPhieukhamSankhoa.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                objPhieukhamSankhoa.NgayKham = dtpNgayKham.Value.Date;
                objPhieukhamSankhoa.NguoiTao = globalVariables.UserName;
                objPhieukhamSankhoa.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
            }
            objPhieukhamSankhoa.IdBacsi = Utility.Int16Dbnull(txtBacsiKham.MyID, -1);

            //khám ngoài
            objPhieukhamSankhoa.KhamngoaiBungcoseophauthuatcu = chkBungcoseophauthuatcu.Checked;
            objPhieukhamSankhoa.KhamngoaiHinhdangtucung = Utility.sDbnull(txtHinhdangTucung.Text);
            objPhieukhamSankhoa.KhamngoaiTuthe = Utility.sDbnull(txtTutheTucung.Text);
            objPhieukhamSankhoa.KhamngoaiChieucaotucung = Utility.ByteDbnull(txtChieucaoTC.Text, 0);
            objPhieukhamSankhoa.KhamngoaiVongbung = Utility.ByteDbnull(txtVongbung.Text, 0);
            objPhieukhamSankhoa.KhamngoaiConcotucung = Utility.sDbnull(txtConcoTC.Text);
            objPhieukhamSankhoa.KhamngoaiTimthai = Utility.ByteDbnull(txtTimthai.Text, 0);
            objPhieukhamSankhoa.KhamngoaiVu = Utility.sDbnull(txtVu.Text);

            objPhieukhamSankhoa.KbTinhtrangoiPhong = optOiphong.Checked;
            objPhieukhamSankhoa.KbTinhtrangoiDet = optOidet.Checked;
            objPhieukhamSankhoa.KbTinhtrangoiQuale = optOiquale.Checked;

            objPhieukhamSankhoa.KbTinhtrangoivoTunhien = optOivoTunhien.Checked;
            objPhieukhamSankhoa.KbTinhtrangoivoBamoi = optOivoBamoi.Checked;

            objPhieukhamSankhoa.KbDolotCao = optDolotCao.Checked;
            objPhieukhamSankhoa.KbDolotChuc = optDolotChuc.Checked;
            objPhieukhamSankhoa.KbDolotChat = optDolotChat.Checked;
            objPhieukhamSankhoa.KbDolotLot = optDolotLot.Checked;

            objPhieukhamSankhoa.KbChisoBishop = Utility.sDbnull(txtChisoBishop.Text);
            objPhieukhamSankhoa.KbAmho = Utility.sDbnull(txtAmho.Text);
            objPhieukhamSankhoa.KbAmdao = Utility.sDbnull(txtAmdao.Text);
            objPhieukhamSankhoa.KbTangsinhmon = Utility.sDbnull(txtTangsinhmon.Text);
            objPhieukhamSankhoa.KbCotucung = Utility.sDbnull(txtCoTC.Text);
            objPhieukhamSankhoa.KbPhanphu = Utility.sDbnull(txtPhanphu.Text);
            objPhieukhamSankhoa.KbMausacnuocoi = Utility.sDbnull(txtMausacNuocoi.Text);
            objPhieukhamSankhoa.KbNuocoinhieuit = Utility.sDbnull(txtNuocoiNhieuhayIt.Text);
            objPhieukhamSankhoa.KbNgoi = Utility.sDbnull(txtKbNgoi.Text);
            objPhieukhamSankhoa.KbThe = Utility.sDbnull(txtThe.Text);
            objPhieukhamSankhoa.KbKieuthe = Utility.sDbnull(txtKieuthe.Text);
            objPhieukhamSankhoa.KbDuongkinhnhohave = Utility.sDbnull(txtDuongkinhnhoHave.Text);
            //Chức năng sống
            //objPhieukhamSankhoa.NhomMau = txtNhommau.myCode;
            objPhieukhamSankhoa.HuyetAp = txtha.Text;
            objPhieukhamSankhoa.NhietDo = txtNhietDo.Text;
            objPhieukhamSankhoa.Mach = Utility.sDbnull(txtMach.Text);
            objPhieukhamSankhoa.NhịpTho = Utility.sDbnull(txtNhipTho.Text);
            objPhieukhamSankhoa.ChieuCao = Utility.sDbnull(txtChieuCao.Text);
            objPhieukhamSankhoa.CanNang = Utility.sDbnull(txtCanNang.Text);
            objPhieukhamSankhoa.Bmi = Utility.sDbnull(txtBMI.Text);
            objPhieukhamSankhoa.MotaThem = "";
        }
        void TaoTienSuPhuKhoa()
        {
            objTspk = new Select().From(EmrTiensusanphukhoa.Schema)
                .Where(EmrTiensusanphukhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrTiensusanphukhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteSingle<EmrTiensusanphukhoa>();
            if (objTspk != null && objTspk.IdTsspk > 0)
            {
                objTspk.MarkOld();
                objTspk.NguoiSua = globalVariables.UserName;
                objTspk.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
            }
            else
            {
                objTspk = new EmrTiensusanphukhoa();
                objTspk.IsNew = true;
                objTspk.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                objTspk.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                //objTspk.NgayKham = dtNgayKham.Value.Date;
                objTspk.NguoiTao = globalVariables.UserName;
                objTspk.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
            }
            objTspk.BaTsspkBatdauthaykinhNam = Utility.Int16Dbnull(dtpBatdauthaykinhnam.Text);
            objTspk.BaTsspkBatdauthaykinhTuoi = Utility.Int16Dbnull(nmrBatdauthaykinhtuoi.Value);
            objTspk.BaTsspkTinhchatkinhnguyet = Utility.sDbnull(txt_tinhchatkinhnguyet.Text);
            objTspk.BaTsspkChukykinhnguyet = Utility.Int16Dbnull(txt_chuky.Text);
            objTspk.BaTsspkSongaythaykinh = Utility.Int16Dbnull(txt_songaythaykinh.Text);
            objTspk.BaTsspkLuongkinh = Utility.sDbnull(txt_luongkinh.Text);
            objTspk.BaTsspkKinhlancuoingay = dtpKinhlancuoingay.Value;
            objTspk.BaTsspkCodaubung = chkCodaubung.Checked;
            objTspk.BaTsspkThoigianTruoc = chk_thoigiantruoc.Checked;
            objTspk.BaTsspkThoigianTrong = chk_thoigiantrong.Checked;
            objTspk.BaTsspkThoigianSau = chk_thoigiansau.Checked;
            objTspk.BaTsspkLaychongNam = Utility.Int16Dbnull(dtpLaychongNam.Text);
            objTspk.BaTsspkLaychongTuoi = Utility.Int16Dbnull(nmrLaychongTuoi.Value);
            objTspk.BaTsspkHetkinhnam = Utility.Int16Dbnull(dtpHetKinhNam.Text);
            objTspk.BaTsspkHetkinhtuoi = Utility.Int16Dbnull(nmrHetkinhTuoi.Value);
            objTspk.BaTsspkBenhphukhoadadieutri = Utility.sDbnull(txt_benhphukhoadadieutri.Text);
        }
        void TaoQuatrinhThaiKy()
        {
            objQttk = new Select().From(EmrQuatrinhThaiky.Schema)
                .Where(EmrQuatrinhThaiky.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrQuatrinhThaiky.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteSingle<EmrQuatrinhThaiky>();
            //Quá trình thai kì
            if (objQttk != null && objQttk.Id > 0)
            {
                objQttk.MarkOld();
                objQttk.NguoiSua = globalVariables.UserName;
                objQttk.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
            }
            else
            {
                objQttk = new EmrQuatrinhThaiky();
                objQttk.IsNew = true;
                objQttk.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                objQttk.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                objQttk.NguoiTao = globalVariables.UserName;
                objQttk.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
            }
            objQttk.Kinhcuoitungay = dtpKinhcuoitungay.Value;
            objQttk.Kinhcuoidenngay = dtpKinhcuoidenngay.Value;
            objQttk.Tuoithai = Utility.ByteDbnull(txtTuoithai.Text);

            objQttk.Khamthaitai = Utility.sDbnull(txtKhamthaitai.Text);
            objQttk.TiemphongUonvan = chkTiemphonguonvan.Checked;
            objQttk.TiemphongUonvanSolan = Utility.ByteDbnull(txtDuoctiemphonguonvanSolan.Text);
            objQttk.Batdauchuyenda = dtpBatdauchuyendatu.Value;
            objQttk.Dauhieulucdau = Utility.sDbnull(txtDauhieuLucdau.Text);
            objQttk.Bienchuyen = Utility.sDbnull(txtBienchuyen.Text);
        }
        void TaoChandoanSanKhoa()
        {
            objChandoanSankhoa = new Select().From(EmrChandoanSankhoa.Schema)
                            .Where(EmrChandoanSankhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                            .And(EmrChandoanSankhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                            .ExecuteSingle<EmrChandoanSankhoa>();
            if (objChandoanSankhoa != null && objChandoanSankhoa.Id > 0)
            {
                objChandoanSankhoa.MarkOld();
                objChandoanSankhoa.NguoiSua = globalVariables.UserName;
                objChandoanSankhoa.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
            }
            else
            {
                objChandoanSankhoa = new EmrChandoanSankhoa();
                objChandoanSankhoa.IsNew = true;
                objChandoanSankhoa.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                objChandoanSankhoa.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                objChandoanSankhoa.NguoiTao = globalVariables.UserName;
                objChandoanSankhoa.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
            }
            objChandoanSankhoa.CdLucvaode = Utility.sDbnull(txtLucvaode.Text);
            objChandoanSankhoa.CdNgaymode = dtpNgaymode.Value;
            objChandoanSankhoa.CdNgoithai = Utility.sDbnull(txtNgoithai.Text);
            objChandoanSankhoa.CdCachthucde = Utility.sDbnull(txtCachthucde.Text);
            objChandoanSankhoa.CdDitatThainhi = Utility.sDbnull(txtDitat.Text);
            objChandoanSankhoa.CdKiemsoattucung = Utility.sDbnull(txtKiemsoattucung.Text);
            objChandoanSankhoa.CdDonthai = optDonthai.Checked;
            objChandoanSankhoa.CdDathai = optDathai.Checked;
            objChandoanSankhoa.CdTrai = optTrai.Checked;
            objChandoanSankhoa.CdGai = optGai.Checked;
            objChandoanSankhoa.CdSong = optSong.Checked;
            objChandoanSankhoa.CdChet = optChet.Checked;
            objChandoanSankhoa.CdCannangThainhi = (int)nmrCannang.Value;

        }
        void TaoPhieuTKBA()
        {
             objTKBA = new Select().From(EmrTomtatBa.Schema).Where(EmrTomtatBa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(EmrTomtatBa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<EmrTomtatBa>();
            if (objTKBA == null) objTKBA = new EmrTomtatBa();
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
            objTKBA.IdNguoigiaoHoso = Utility.Int16Dbnull(txtNguoiGiaoHoSo.MyID);
            objTKBA.MaNguoigiaohoso = txtNguoiGiaoHoSo.MyCode;
            //objTKBA.IdNguoinhanHoso = Utility.Int16Dbnull(txtNguoiNhanHoSo.MyID);
            //objTKBA.MaNguoinhanhoso = txtNguoiNhanHoSo.MyCode;
            objTKBA.IdGiamdoc = Utility.Int16Dbnull(txtGDBV.MyID);
            objTKBA.MaGiamdoc = txtGDBV.MyCode;
            objTKBA.IdBacsiDieutri = Utility.Int16Dbnull(txtBSDieuTri.MyID);
            objTKBA.MaBacsiDieutri = txtBSDieuTri.MyCode;
            objTKBA.IdTruongkhoadieutri = Utility.Int16Dbnull(txtTruongkhoa.MyID);
            objTKBA.MaTruongkhoadieutri = txtTruongkhoa.MyCode;
        }
        void TaoPhieuKCB()
        {
            //Refresh lại thông tin KCB
            objPKB = new Select().From(EmrPhieukhambenh.Schema)
                .Where(EmrPhieukhambenh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrPhieukhambenh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .And(EmrPhieukhambenh.Columns.Noitru).IsEqualTo(1)
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
                objPKB.Noitru = 1;
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
            if (objEmrBa != null && objEmrBa.LoaiBa != Utility.sDbnull(cboLoaiBA.SelectedValue))
            {
                ThongbaoSaiBenhAn(objEmrBa);
                cmdPrint.Enabled = cmdKetthucBA.Enabled = cmdXoaBenhAn.Enabled = false;
            }
        }
        void ThongbaoSaiBenhAn(EmrBa objEmrBa)
        {
            string Msg = string.Format("Người bệnh {0} đang có hồ sơ Bệnh án {1} không khớp với loại Bệnh án bạn đang chọn. Vui lòng chọn lại đúng loại Bệnh án cần làm", ucThongtinnguoibenh_emr_basic1.txtTenBN.Text, objEmrBa.LoaiBa);
            Utility.ShowMsg(Msg);
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

                }
                //objEmrBa.BenhNgoaiKhoa = Utility.sDbnull(txtBenhNgoai_Khoa.Text);
                objEmrBa.MaCoso = objLuotkham.MaCoso;
                objEmrBa.IdBenhnhan = objLuotkham.IdBenhnhan;
                objEmrBa.TenBenhnhan = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                objEmrBa.MaLuotkham = objLuotkham.MaLuotkham;
                objEmrBa.MaYte = objLuotkham.MaYte;
                objEmrBa.NgaySinh = DateTime.ParseExact(Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ngay_sinh"], DateTime.Now.ToString("yyyyMMdd")), "yyyyMMdd", CultureInfo.InvariantCulture);
                objEmrBa.MaGioitinh = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["id_gioitinh"], "0") == "0" ? "M" : "F";
                objEmrBa.GioiTinh = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["gioi_tinh"], "");
                objEmrBa.Tuoi = Utility.ByteDbnull(dt_ThongtinNguoibenh.Rows[0]["Tuoi"], "0");
                objEmrBa.LoaiTuoi = (byte)objLuotkham.LoaiTuoi;


                objEmrBa.MaNghenghiep = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["nghe_nghiep"], "");
                objEmrBa.TenNghenghiep = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_nghenghiep"], "");
                objEmrBa.MaDantoc = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["dan_toc"], "");
                objEmrBa.TenDantoc = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_dantoc"], "");
                objEmrBa.MaTongiao = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ton_giao"], "");
                objEmrBa.TenTongiao = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_tongiao"], "");
                objEmrBa.MaQuocgia = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ma_quocgia"], "VN");
                objEmrBa.TenQuocgia = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_quocgia"], "");
                objEmrBa.NgoaiKieu = (Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ma_quocgia"], "VN") == "VN" ? 0 : 1) == 1;

                objEmrBa.DiachiLienhe = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["diachi_lienhe"], "");
                objEmrBa.DienthoaiLienhe = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["dienthoai_lienhe"], "");
                objEmrBa.NguoiLienhe = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["nguoi_lienhe"], "");
                objEmrBa.CmtNguoilienhe = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["CMT_nguoilienhe"], "");
                objEmrBa.DiaChi = objLuotkham.DiaChi;
                objEmrBa.MaTinhtp = objLuotkham.MaTinhtp;
                objEmrBa.TenTinhtp = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_tinhtp"], "");
                objEmrBa.MaQuanhuyen = objLuotkham.MaQuanhuyen;
                objEmrBa.TenQuanhuyen = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_quanhuyen"], "");
                objEmrBa.MaXaphuong = objLuotkham.MaXaphuong;
                objEmrBa.TenXaphuong = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_xaphuong"], "");
                objEmrBa.MaCoquan = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["co_quan"], "");
                objEmrBa.TenCoquan = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_coquan"], "");
                objEmrBa.MatheBhyt = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["mathe_bhyt"], "");
                objEmrBa.MaDoituong = Utility.ByteDbnull(objLuotkham.IdDoituongKcb);
                objEmrBa.TenDoituong = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_doituong_kcb"], "");

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
                objEmrBa.DienThoai = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["dien_thoai"], "");
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
                if (objPhieuchuyenvien != null)
                {
                    objEmrBa.ChuyenvienTuyentren = chkQLNBTuyenTren.Checked;
                    objEmrBa.ChuyenvienTuyenduoi = chkQLNBTuyenDuoi.Checked;
                    objEmrBa.ChuyenvienKhac = chkQLNBChuyenVienCK.Checked;
                    objEmrBa.ChuyenvienNoichuyenden = Utility.sDbnull(txtQLNBChuyenVienNoiChuyenDen.Text);
                }
                //if (objPhieuRavien != null)
                //{
                    objEmrBa.RavienRavien = chkQLNBRaVienRavien.Checked;
                    objEmrBa.RavienXinve = chkQLNBRavienXinVe.Checked;
                    objEmrBa.RavienBove = chkQLNBRavienBoVe.Checked;
                    objEmrBa.RavienDuave = chkQLNBRavienDuaVe.Checked;
                    objEmrBa.ChuyenvienNoichuyenden = Utility.sDbnull(txtQLNBChuyenVienNoiChuyenDen.Text);
                    objEmrBa.ChuyenvienNoichuyenden = Utility.sDbnull(txtQLNBChuyenVienNoiChuyenDen.Text);
                    objEmrBa.RavienMaBenhchinh = Utility.sDbnull(txtCDRavienMaBenhChinh.Text);
                    objEmrBa.RavienMaBenhphu = Utility.sDbnull(txtCDRavienMaBenhKemTheo.Text);
                    objEmrBa.RavienTenBenhchinh = Utility.sDbnull(txtCDRavienTenBenhKemTheo.Text);
                    objEmrBa.RavienTenBenhphu = Utility.sDbnull(txtCDRavienMaBenhKemTheo.Text);
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
                objEmrBa.TinhtrangravienThoigianTuvong = dtpNgaytuvong.Value;
                    objEmrBa.TinhtrangravienLydotuvongDobenh = chkttrvDoBenh.Checked;
                    objEmrBa.TinhtrangravienLydotuvongDotaibiendieutri = chkttrvDoTaiBien.Checked;
                    objEmrBa.TinhtrangravienLydotuvongKhac = chkttrvDoKhac.Checked;
                    objEmrBa.TinhtrangravienThoigiantuvongTrong24h = chkttrvTrong24GioVaoVien.Checked;
                    objEmrBa.TinhtrangravienThoigiantuvongSau24h = chkttrvngoai24gioVaoVien.Checked;
                    objEmrBa.TinhtrangravienNguyennhantuvong = Utility.sDbnull(txtTTRVNguyenNhanChinhTuVong.Text);
                    //objEmrBa.TinhtrangravienMaNguyennhantuvong = Utility.sDbnull(txtTTRVNguyenNhanChinhTuVong.Text);
                    objEmrBa.TinhtrangravienKhamnghiemtuthi = chkTTRVKhamNgiemTuThi.Checked;
                    objEmrBa.TinhtrangravienChandoangiauphaututhi = Utility.sDbnull(txtTTRVChandoanGiaiphauTuthi.Text);
                    //objEmrBa.TinhtrangravienChandoangiauphaututhi
               // }


                //Chẩn đoán
                objEmrBa.RavienTongsongayDieutri = Utility.Int16Dbnull(txtQLNBTongSoNgayDieuTri.Text);
                objEmrBa.CdNoichuyenden = txtCDNoiChuyenDen.Text;
                objEmrBa.CdNoichuyendenMa = txtCDMaNoiChuyenDen.Text;
                objEmrBa.CdKkbCapcuu = txtCDKKBCapCuu.Text;
                objEmrBa.CdKkbCapcuuMa = txtCDMaKKBCapCuu.Text;
                objEmrBa.CdKhoadieutri = txtCDKhiVaoDieuTri.Text;
                objEmrBa.CdKhoadieutriMa = txtCDMaKhiVaoDieuTri.Text;
                objEmrBa.CdNoichuyenden = txtCDNoiChuyenDen.Text;

                objEmrBa.CdDophauthuat = chk_cd_dophauthuat.Checked;
                objEmrBa.CdDonhiemkhuan = chk_cd_donhiemkhuan.Checked;
                objEmrBa.CdDogayme = chk_cd_dogayme.Checked;
                objEmrBa.CdTaibienBienchungKhac = chk_cd_dokhac.Checked;
                objEmrBa.CdTongsongaydieutriSauphauthuat = Utility.ByteDbnull(nmr_cd_tongsongaydieutri_sauphauthuat.Value);
                objEmrBa.CdTongsolanphauthuat = Utility.ByteDbnull(nmr_cd_tongsolanphauthuat.Value);
                objEmrBa.ChandoanTruocphauthuat = Utility.sDbnull(txt_chandoan_truocphauthuat.Text);
                objEmrBa.MaChandoanTruocphauthuat = Utility.sDbnull(txt_chandoan_truocphauthuat.MyCode);

                objEmrBa.ChandoanSauphauthuat = Utility.sDbnull(txt_chandoan_sauphauthuat.Text);
                objEmrBa.MaChandoanSauphauthuat = Utility.sDbnull(txt_chandoan_sauphauthuat.MyCode);

                objEmrBa.CdTaibien = chkCDTaiBien.Checked;
                objEmrBa.CdBienchung = chkCDBienChung.Checked;


                objEmrBa.VaovienLydovaovien = txtBenhAnLyDoNhapVien.Text;
                objEmrBa.VaovienVaongaythucuabenh = Utility.ByteDbnull(txtBenhAnVaoNgayThu.Text);
                objEmrBa.HoibenhQuatrinhbenhly = Utility.sDbnull(txtBenhAnQuaTrinhBenhLy.Text);
                objEmrBa.HoibenhTiensubanthan = Utility.sDbnull(txtBenhAnTiensuBanthan.Text);
                //Phiếu khám phụ khoa
                //if (objPhieukhamSankhoa != null)
                //{
                //    //khám ngoài
                //    objEmrBa.KhamngoaiBungcoseophauthuatcu = objPhieukhamSankhoa.KhamngoaiBungcoseophauthuatcu;
                //    objEmrBa.KhamngoaiHinhdangtucung = objPhieukhamSankhoa.KhamngoaiHinhdangtucung;
                //    objEmrBa.KhamngoaiTuthe = objPhieukhamSankhoa.KhamngoaiTuthe;
                //    objEmrBa.KhamngoaiChieucaotucung = objPhieukhamSankhoa.KhamngoaiChieucaotucung;
                //    objEmrBa.KhamngoaiVongbung = objPhieukhamSankhoa.KhamngoaiVongbung;
                //    objEmrBa.KhamngoaiConcotucung = objPhieukhamSankhoa.KhamngoaiConcotucung;
                //    objEmrBa.KhamngoaiTimthai = objPhieukhamSankhoa.KhamngoaiTimthai;
                //    objEmrBa.KhamngoaiVu = objPhieukhamSankhoa.KhamngoaiVu;

                //    objEmrBa.KhamtrongTinhtrangoiPhong = objPhieukhamSankhoa.KbTinhtrangoiPhong;
                //    objEmrBa.KhamtrongTinhtrangoiDet = objPhieukhamSankhoa.KbTinhtrangoiDet;
                //    objEmrBa.KhamtrongTinhtrangoiQuale = objPhieukhamSankhoa.KbTinhtrangoiQuale;

                //    objEmrBa.KhamtrongTinhtrangoivoTunhien = objPhieukhamSankhoa.KbTinhtrangoivoTunhien;
                //    objEmrBa.KhamtrongTinhtrangoivoBamoi = objPhieukhamSankhoa.KbTinhtrangoivoBamoi;

                //    objEmrBa.KhamtrongDolotCao = objPhieukhamSankhoa.KbDolotCao;
                //    objEmrBa.KhamtrongDolotChuc = objPhieukhamSankhoa.KbDolotChuc;
                //    objEmrBa.KhamtrongDolotChat = objPhieukhamSankhoa.KbDolotChat;
                //    objEmrBa.KhamtrongDolotLot = objPhieukhamSankhoa.KbDolotLot;

                //    objEmrBa.KhamtrongChisoBishop = objPhieukhamSankhoa.KbChisoBishop;
                //    objEmrBa.KhamtrongAmho = objPhieukhamSankhoa.KbAmho;
                //    objEmrBa.KhamtrongAmdao = objPhieukhamSankhoa.KbAmdao;
                //    objEmrBa.KhamtrongTangsinhmon = objPhieukhamSankhoa.KbTangsinhmon;
                //    objEmrBa.KhamtrongCotucung = objPhieukhamSankhoa.KhamngoaiConcotucung;
                //    objEmrBa.KhamtrongPhanphu = objPhieukhamSankhoa.KbPhanphu;
                //    objEmrBa.KhamtrongMausacnuocoi = objPhieukhamSankhoa.KbMausacnuocoi;
                //    objEmrBa.KhamtrongNuocoinhieuit = objPhieukhamSankhoa.KbNuocoinhieuit;
                //    objEmrBa.KhamtrongNgoi = objPhieukhamSankhoa.KbNgoi;
                //    objEmrBa.KhamtrongThe = objPhieukhamSankhoa.KbThe;
                //    objEmrBa.KhamtrongKieuthe = objPhieukhamSankhoa.KbKieuthe;
                //    objEmrBa.KhamtrongDuongkinhnhohave = objPhieukhamSankhoa.KbDuongkinhnhohave;

                //}
                //else
                //{
                //    //khám ngoài
                //    objEmrBa.KhamngoaiBungcoseophauthuatcu = chkBungcoseophauthuatcu.Checked;
                //    objEmrBa.KhamngoaiHinhdangtucung = Utility.sDbnull(txtHinhdangTucung.Text);
                //    objEmrBa.KhamngoaiTuthe = Utility.sDbnull(txtTutheTucung.Text);
                //    objEmrBa.KhamngoaiChieucaotucung = Utility.ByteDbnull(txtChieucaoTC.Text, 0);
                //    objEmrBa.KhamngoaiVongbung = Utility.ByteDbnull(txtVongbung.Text, 0);
                //    objEmrBa.KhamngoaiConcotucung = Utility.sDbnull(txtConcoTC.Text);
                //    objEmrBa.KhamngoaiTimthai = Utility.ByteDbnull(txtTimthai.Text, 0);
                //    objEmrBa.KhamngoaiVu = Utility.sDbnull(txtVu.Text);

                //    objEmrBa.KhamtrongTinhtrangoiPhong = optOiphong.Checked;
                //    objEmrBa.KhamtrongTinhtrangoiDet = optOidet.Checked;
                //    objEmrBa.KhamtrongTinhtrangoiQuale = optOiquale.Checked;

                //    objEmrBa.KhamtrongTinhtrangoivoTunhien = optOivoTunhien.Checked;
                //    objEmrBa.KhamtrongTinhtrangoivoBamoi = optOivoBamoi.Checked;

                //    objEmrBa.KhamtrongDolotCao = optDolotCao.Checked;
                //    objEmrBa.KhamtrongDolotChuc = optDolotChuc.Checked;
                //    objEmrBa.KhamtrongDolotChat = optDolotChat.Checked;
                //    objEmrBa.KhamtrongDolotLot = optDolotLot.Checked;

                //    objEmrBa.KhamtrongChisoBishop = Utility.sDbnull(txtChisoBishop.Text);
                //    objEmrBa.KhamtrongAmho = Utility.sDbnull(txtAmho.Text);
                //    objEmrBa.KhamtrongAmdao = Utility.sDbnull(txtAmdao.Text);
                //    objEmrBa.KhamtrongTangsinhmon = Utility.sDbnull(txtTangsinhmon.Text);
                //    objEmrBa.KhamtrongCotucung = Utility.sDbnull(txtCoTC.Text);
                //    objEmrBa.KhamtrongPhanphu = Utility.sDbnull(txtPhanphu.Text);
                //    objEmrBa.KhamtrongMausacnuocoi = Utility.sDbnull(txtMausacNuocoi.Text);
                //    objEmrBa.KhamtrongNuocoinhieuit = Utility.sDbnull(txtNuocoiNhieuhayIt.Text);
                //    objEmrBa.KhamtrongNgoi = Utility.sDbnull(txtKbNgoi.Text);
                //    objEmrBa.KhamtrongThe = Utility.sDbnull(txtThe.Text);
                //    objEmrBa.KhamtrongKieuthe = Utility.sDbnull(txtKieuthe.Text);
                //    objEmrBa.KhamtrongDuongkinhnhohave = Utility.sDbnull(txtDuongkinhnhoHave.Text);
                //}
                ////Phiếu theo dõi buồng đẻ
                //if(objPhieutheodoitaibuongde!=null)
                //{
                //    objEmrBa.Vaobuongdeluc = objPhieutheodoitaibuongde.Vaobuongdeluc;
                //    objEmrBa.Nguoitheodoi = objPhieutheodoitaibuongde.Nguoitheodoi;
                //    objEmrBa.Chucdanh = objPhieutheodoitaibuongde.Chucdanh;

                //    objEmrBa.Deluc = objPhieutheodoitaibuongde.Deluc;
                //    objEmrBa.Apgar1phut = objPhieutheodoitaibuongde.Apgar1phut;
                //    objEmrBa.Apgar5phut = objPhieutheodoitaibuongde.Apgar5phut;
                //    objEmrBa.Apgar10phut = objPhieutheodoitaibuongde.Apgar10phut;
                //    objEmrBa.TresosinhCannang = objPhieutheodoitaibuongde.TresosinhCannang;
                //    objEmrBa.TresosinhCao = objPhieutheodoitaibuongde.TresosinhCao;
                //    objEmrBa.TresosinhVongdau = objPhieutheodoitaibuongde.TresosinhVongdau;

                //    objEmrBa.TresosinhDonthaiTrai = objPhieutheodoitaibuongde.TresosinhDonthaiTrai;
                //    objEmrBa.TresosinhDonthaiGai = objPhieutheodoitaibuongde.TresosinhDonthaiGai;
                //    objEmrBa.TresosinhDathaiTrai = objPhieutheodoitaibuongde.TresosinhDathaiTrai;
                //    objEmrBa.TresosinhDathaiGai = objPhieutheodoitaibuongde.TresosinhDathaiGai;
                //    objEmrBa.TresosinhTatbamsinh = objPhieutheodoitaibuongde.TresosinhTatbamsinh;
                //    objEmrBa.TresosinhCohaumon = objPhieutheodoitaibuongde.TresosinhCohaumon;
                //    objEmrBa.TresosinhCuthetatbamsinh = objPhieutheodoitaibuongde.TresosinhCuthetatbamsinh;
                //    objEmrBa.TresosinhTinhtrangsaude = objPhieutheodoitaibuongde.TresosinhTinhtrangsaude;
                //    objEmrBa.TresosinhXulyvaketqua = objPhieutheodoitaibuongde.TresosinhXulyvaketqua;

                //    objEmrBa.SorauBoc = objPhieutheodoitaibuongde.SorauBoc;
                //    objEmrBa.SorauSo = objPhieutheodoitaibuongde.SorauSo;

                //    objEmrBa.SorauLuc = objPhieutheodoitaibuongde.SorauLuc;
                //    objEmrBa.SorauCachsorau = objPhieutheodoitaibuongde.SorauCachsorau;
                //    objEmrBa.SorauMatmang = objPhieutheodoitaibuongde.SorauMatmang;
                //    objEmrBa.SorauMatmui = objPhieutheodoitaibuongde.SorauMatmui;
                //    objEmrBa.SorauBanhrau = objPhieutheodoitaibuongde.SorauBanhrau;
                //    objEmrBa.CuongrauDai = objPhieutheodoitaibuongde.CuongrauDai;
                //    objEmrBa.SorauCannang = objPhieutheodoitaibuongde.SorauCannang;
                //    objEmrBa.SorauRaucuonco = objPhieutheodoitaibuongde.SorauRaucuonco;
                //    objEmrBa.SorauChaymausauso = objPhieutheodoitaibuongde.SorauChaymausauso;
                //    objEmrBa.SorauLuongmaumat = objPhieutheodoitaibuongde.SorauLuongmaumat;
                //    objEmrBa.SorauKiemsoattucung = objPhieutheodoitaibuongde.SorauKiemsoattucung;
                //    objEmrBa.SorauXulyvaketqua = objPhieutheodoitaibuongde.SorauXulyvaketqua;

                //    objEmrBa.SanphuDaniemmac = objPhieutheodoitaibuongde.SanphuDaniemmac;
                //    objEmrBa.SanphuPhuongphapdeThuong = objPhieutheodoitaibuongde.SanphuPhuongphapdeThuong;
                //    objEmrBa.SanphuPhuongphapdeForceps = objPhieutheodoitaibuongde.SanphuPhuongphapdeForceps;
                //    objEmrBa.SanphuPhuongphapdeGiachut = objPhieutheodoitaibuongde.SanphuPhuongphapdeGiachut;
                //    objEmrBa.SanphuPhuongphapdePt = objPhieutheodoitaibuongde.SanphuPhuongphapdePt;
                //    objEmrBa.SanphuPhuongphapdeDechihuy = objPhieutheodoitaibuongde.SanphuPhuongphapdeDechihuy;
                //    objEmrBa.SanphuPhuongphapdeKhac = objPhieutheodoitaibuongde.SanphuPhuongphapdeKhac;
                //    objEmrBa.SanphuLydocanthiep = objPhieutheodoitaibuongde.SanphuLydocanthiep;
                //    objEmrBa.SanphuTangsinhmonKhongrach = objPhieutheodoitaibuongde.SanphuTangsinhmonKhongrach;
                //    objEmrBa.SanphuTangsinhmonRach = objPhieutheodoitaibuongde.SanphuTangsinhmonRach;
                //    objEmrBa.SanphuTangsinhmonCat = objPhieutheodoitaibuongde.SanphuTangsinhmonCat;

                //    objEmrBa.SanphuPhuongphapkhauvaloaichi = objPhieutheodoitaibuongde.SanphuPhuongphapkhauvaloaichi;
                //    objEmrBa.SanphuPhuongphapkhauvaloaichiMota = objPhieutheodoitaibuongde.SanphuPhuongphapkhauvaloaichiMota;
                //    objEmrBa.SanphuSomuikhau = objPhieutheodoitaibuongde.SanphuSomuikhau;

                //    objEmrBa.SanphuCotucungRach = objPhieutheodoitaibuongde.SanphuCotucungRach;
                //    objEmrBa.SanphuCotucungKhongrach = objPhieutheodoitaibuongde.SanphuCotucungKhongrach;
                //}
                //else
                //{
                //    objEmrBa.Vaobuongdeluc = dtpVaobuongdeluc.Value;
                //    objEmrBa.Nguoitheodoi = Utility.sDbnull(txtTennguoitheodoi.Text);
                //    objEmrBa.Chucdanh = Utility.sDbnull(txtChucdanhnguoitheodoi.Text);

                //    objEmrBa.Deluc = dtpDeluc.Value;
                //    objEmrBa.Apgar1phut = Utility.sDbnull(txt1phut.Text);
                //    objEmrBa.Apgar5phut = Utility.sDbnull(txt5phut.Text);
                //    objEmrBa.Apgar10phut = Utility.sDbnull(txt10phut.Text);
                //    objEmrBa.TresosinhCannang = Utility.Int16Dbnull(nmrCannangtresosinh.Value);
                //    objEmrBa.TresosinhCao = Utility.Int16Dbnull(nmrcao.Value);
                //    objEmrBa.TresosinhVongdau = Utility.Int16Dbnull(nmrvongdau.Value);

                //    objEmrBa.TresosinhDonthaiTrai = optDonthaiTrai.Checked;
                //    objEmrBa.TresosinhDonthaiGai = optDonthaiGai.Checked;
                //    objEmrBa.TresosinhDathaiTrai = optDathaiTrai.Checked;
                //    objEmrBa.TresosinhDathaiGai = optDathaiGai.Checked;
                //    objEmrBa.TresosinhTatbamsinh = chkTatbamsinh.Checked;
                //    objEmrBa.TresosinhCohaumon = chkCohaumon.Checked;
                //    objEmrBa.TresosinhCuthetatbamsinh = Utility.sDbnull(txtCuthetatbamsinh.Text);
                //    objEmrBa.TresosinhTinhtrangsaude = Utility.sDbnull(txtTinhtrangtresosinhsaukhide.Text);
                //    objEmrBa.TresosinhXulyvaketqua = Utility.sDbnull(txtXulyvaketquaTresosinh.Text);

                //    objEmrBa.SorauBoc = optRauboc.Checked;
                //    objEmrBa.SorauSo = optRauso.Checked;

                //    objEmrBa.SorauLuc = dtpRausoluc.Value;
                //    objEmrBa.SorauCachsorau = Utility.sDbnull(txtCachsorau.Text);
                //    objEmrBa.SorauMatmang = Utility.sDbnull(txtMatmang.Text);
                //    objEmrBa.SorauMatmui = Utility.sDbnull(txtMatmui.Text);
                //    objEmrBa.SorauBanhrau = Utility.sDbnull(txtBanhrau.Text);
                //    objEmrBa.SorauCannang = Utility.Int16Dbnull(nmrCannangRau.Value);
                //    objEmrBa.SorauRaucuonco = chkRaucuonco.Checked;
                //    objEmrBa.CuongrauDai = Utility.Int16Dbnull(nmrCuongrau.Value);
                //    objEmrBa.SorauChaymausauso = chkCochaymausauso.Checked;
                //    objEmrBa.SorauLuongmaumat = Utility.Int16Dbnull(nmrLuongmaumat.Value);
                //    objEmrBa.SorauKiemsoattucung = chkKiemsoattucung.Checked;
                //    objEmrBa.SorauXulyvaketqua = Utility.sDbnull(txtXulyvaketquaRau.Text);

                //    objEmrBa.SanphuDaniemmac = Utility.sDbnull(txtSanphuDaniemmac.Text);
                //    objEmrBa.SanphuPhuongphapdeThuong = optDethuong.Checked;
                //    objEmrBa.SanphuPhuongphapdeForceps = optForceps.Checked;
                //    objEmrBa.SanphuPhuongphapdeGiachut = optGiachut.Checked;
                //    objEmrBa.SanphuPhuongphapdePt = optPhauthuat.Checked;
                //    objEmrBa.SanphuPhuongphapdeDechihuy = optDechihuy.Checked;
                //    objEmrBa.SanphuPhuongphapdeKhac = optKhac.Checked;
                //    objEmrBa.SanphuLydocanthiep = Utility.sDbnull(txtLydocanthiep.Text);
                //    objEmrBa.SanphuTangsinhmonKhongrach = optTangsinhmonKhongrach.Checked;
                //    objEmrBa.SanphuTangsinhmonRach = optTangsinhmonRach.Checked;
                //    objEmrBa.SanphuTangsinhmonCat = optTangsinhmonCat.Checked;

                //    objEmrBa.SanphuPhuongphapkhauvaloaichi = chkPhuongphapkhauvaloaichi.Checked;
                //    if (chkPhuongphapkhauvaloaichi.Checked)
                //    {
                //        objEmrBa.SanphuPhuongphapkhauvaloaichiMota = Utility.sDbnull(txtPhuongphapkhauvaloaichi.Text);
                //        objEmrBa.SanphuSomuikhau = Utility.Int16Dbnull(nmrSomuikhau.Value);
                //    }
                //    else
                //    {
                //        objEmrBa.SanphuPhuongphapkhauvaloaichiMota = "";
                //        objEmrBa.SanphuSomuikhau = 0;
                //    }
                //    objEmrBa.SanphuCotucungRach = optCotucungRach.Checked;
                //    objEmrBa.SanphuCotucungKhongrach = optCotucungKhongrach.Checked;
                //}  
                ////Tiền sử sản phụ khoa
                //if(objTspk!=null)
                //{
                //    objEmrBa.BatdauthaykinhNam = objTspk.BaTsspkBatdauthaykinhNam;
                //    objEmrBa.BatdauthaykinhTuoi = objTspk.BaTsspkBatdauthaykinhTuoi;
                //    objEmrBa.Tinhchatkinhnguyet = objTspk.BaTsspkTinhchatkinhnguyet;
                //    objEmrBa.Chuky = objTspk.BaTsspkChukykinhnguyet;
                //    objEmrBa.Songaythaykinh = objTspk.BaTsspkSongaythaykinh;
                //    objEmrBa.Luongkinh = objTspk.BaTsspkLuongkinh;
                //    objEmrBa.Kinhlancuoingay = objTspk.BaTsspkKinhlancuoingay;
                //    objEmrBa.Codaubung = objTspk.BaTsspkCodaubung;
                //    objEmrBa.ThoigianTruoc = objTspk.BaTsspkThoigianTruoc;
                //    objEmrBa.ThoigianTrong = objTspk.BaTsspkThoigianTrong;
                //    objEmrBa.ThoigianSau = objTspk.BaTsspkThoigianSau;
                //    objEmrBa.LaychongNam = objTspk.BaTsspkLaychongNam;
                //    objEmrBa.LaychongTuoi =Utility.ByteDbnull( objTspk.BaTsspkLaychongTuoi);
                //    objEmrBa.Hetkinhnam = objTspk.BaTsspkHetkinhnam;
                //    objEmrBa.Hetkinhtuoi = Utility.ByteDbnull(objTspk.BaTsspkHetkinhtuoi);
                //    objEmrBa.Nhungbenhphukhoadadieutri = objTspk.BaTsspkBenhphukhoadadieutri;
                //}   
                //else
                //{
                //    objEmrBa.BatdauthaykinhNam = Utility.Int16Dbnull(dtpBatdauthaykinhnam.Text);
                //    objEmrBa.BatdauthaykinhTuoi = Utility.Int16Dbnull(nmrBatdauthaykinhtuoi.Value);
                //    objEmrBa.Tinhchatkinhnguyet = Utility.sDbnull(txt_tinhchatkinhnguyet.Text);
                //    objEmrBa.Chuky = Utility.Int16Dbnull(txt_chuky.Text);
                //    objEmrBa.Songaythaykinh = Utility.Int16Dbnull(txt_songaythaykinh.Text);
                //    objEmrBa.Luongkinh = Utility.sDbnull(txt_luongkinh.Text);
                //    objEmrBa.Kinhlancuoingay = dtpKinhlancuoingay.Value;
                //    objEmrBa.Codaubung = chkCodaubung.Checked;
                //    objEmrBa.ThoigianTruoc = chk_thoigiantruoc.Checked;
                //    objEmrBa.ThoigianTrong = chk_thoigiantrong.Checked;
                //    objEmrBa.ThoigianSau = chk_thoigiansau.Checked;
                //    objEmrBa.LaychongNam = Utility.Int16Dbnull(dtpLaychongNam.Text);
                //    objEmrBa.LaychongTuoi = Utility.ByteDbnull(nmrLaychongTuoi.Value);
                //    objEmrBa.Hetkinhnam = Utility.Int16Dbnull(dtpHetKinhNam.Text);
                //    objEmrBa.Hetkinhtuoi = Utility.ByteDbnull(nmrHetkinhTuoi.Value);
                //    objEmrBa.Nhungbenhphukhoadadieutri = Utility.sDbnull(txt_benhphukhoadadieutri.Text);
                //}    
                ////Chẩn đoán sản khoa
                //if(objChandoanSankhoa!=null)
                //{
                //    objEmrBa.CdLucvaode = objChandoanSankhoa.CdLucvaode;
                //    objEmrBa.CdNgaymode = objChandoanSankhoa.CdNgaymode;
                //    objEmrBa.CdNgoithai = objChandoanSankhoa.CdNgoithai;
                //    objEmrBa.CdCachthucde = objChandoanSankhoa.CdCachthucde;
                //    objEmrBa.CdDitatThainhi = objChandoanSankhoa.CdDitatThainhi;
                //    objEmrBa.CdKiemsoattucung = objChandoanSankhoa.CdKiemsoattucung;
                //    objEmrBa.CdDonthai = objChandoanSankhoa.CdDonthai;
                //    objEmrBa.CdDathai = objChandoanSankhoa.CdDathai;
                //    objEmrBa.CdTrai = objChandoanSankhoa.CdTrai;
                //    objEmrBa.CdGai = objChandoanSankhoa.CdGai;
                //    objEmrBa.CdSong = objChandoanSankhoa.CdSong;
                //    objEmrBa.CdChet = objChandoanSankhoa.CdChet;
                //    objEmrBa.CdCannangThainhi = objChandoanSankhoa.CdCannangThainhi;
                //    objEmrBa.CdPhuongphapphauthuat = Utility.sDbnull(txt_cd_phuongphapphuatthuat.Text);
                //}   
                //else
                //{
                //    objEmrBa.CdLucvaode = Utility.sDbnull(txtLucvaode.Text);
                //    objEmrBa.CdNgaymode = dtpNgaymode.Value;
                //    objEmrBa.CdNgoithai = Utility.sDbnull(txtNgoithai.Text);
                //    objEmrBa.CdCachthucde = Utility.sDbnull(txtCachthucde.Text);
                //    objEmrBa.CdDitatThainhi = Utility.sDbnull(txtDitat.Text);
                //    objEmrBa.CdKiemsoattucung = Utility.sDbnull(txtKiemsoattucung.Text);
                //    objEmrBa.CdDonthai = optDonthai.Checked;
                //    objEmrBa.CdDathai = optDathai.Checked;
                //    objEmrBa.CdTrai = optTrai.Checked;
                //    objEmrBa.CdGai = optGai.Checked;
                //    objEmrBa.CdSong = optSong.Checked;
                //    objEmrBa.CdChet = optChet.Checked;
                //    objEmrBa.CdCannangThainhi = (int)nmrCannang.Value;
                //    objEmrBa.CdPhuongphapphauthuat = Utility.sDbnull(txt_cd_phuongphapphuatthuat.Text);
                //}  
                ////Quá trình thai kỳ
                //if(objQttk!=null)
                //{
                //    objEmrBa.HoibenhKinhcuoitungay = objQttk.Kinhcuoitungay;
                //    objEmrBa.HoibenhKinhcuoiden = objQttk.Kinhcuoidenngay;
                //    objEmrBa.HoibenhTuoithai = objQttk.Tuoithai;

                //    objEmrBa.HoibenhKhamthaitai = objQttk.Khamthaitai;
                //    objEmrBa.HoibenhTiemphongUonvan = objQttk.TiemphongUonvan;
                //    objEmrBa.HoibenhTiemphongUonvanSolan = objQttk.TiemphongUonvanSolan;
                //    objEmrBa.HoibenhBatdauchuyenda = objQttk.Batdauchuyenda;
                //    objEmrBa.HoibenhDauhieulucdau = objQttk.Dauhieulucdau;
                //    objEmrBa.HoibenhBienchuyen = objQttk.Bienchuyen;
                //}   
                //else
                //{
                //    objEmrBa.HoibenhKinhcuoitungay = dtpKinhcuoitungay.Value;
                //    objEmrBa.HoibenhKinhcuoiden = dtpKinhcuoidenngay.Value;
                //    objEmrBa.HoibenhTuoithai = Utility.ByteDbnull(txtTuoithai.Text);

                //    objEmrBa.HoibenhKhamthaitai = Utility.sDbnull(txtKhamthaitai.Text);
                //    objEmrBa.HoibenhTiemphongUonvan = chkTiemphonguonvan.Checked;
                //    objEmrBa.HoibenhTiemphongUonvanSolan = Utility.ByteDbnull(txtDuoctiemphonguonvanSolan.Text);
                //    objEmrBa.HoibenhBatdauchuyenda = dtpBatdauchuyendatu.Value;
                //    objEmrBa.HoibenhDauhieulucdau = Utility.sDbnull(txtDauhieuLucdau.Text);
                //    objEmrBa.HoibenhBienchuyen = Utility.sDbnull(txtBienchuyen.Text);
                //}    
                objEmrBa.TinhinhphauthuatChandoansauphauthuat = Utility.sDbnull(txtTinhhinhphauthuat_cd_sauphauthuat.Text);
                objEmrBa.TinhinhphauthuatChandoantruocphauthuat = Utility.sDbnull(txtTinhhinhphauthuat_cd_truocphauthuat.Text);
                objEmrBa.TinhinhphauthuatTaibien = chk_tinhinhphauthuat_taibien.Checked;
                objEmrBa.TinhinhphauthuatBienchung = chk_tinhinhphauthuat_bienchung.Checked;
                objEmrBa.TinhinhphauthuatDophauthuat = chk_tinhinhphauthuat_dophauthuat.Checked;
                objEmrBa.TinhinhphauthuatDogayme = chk_tinhinhphauthuat_dogayme.Checked;
                objEmrBa.TinhinhphauthuatDonhiemkhuan = chk_tinhinhphauthuat_donhiemkhuan.Checked;
                objEmrBa.TinhinhphauthuatDokhac = chk_tinhinhphauthuat_dokhac.Checked;

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

                objEmrBa.IdNguoigiaoHoso = Utility.Int16Dbnull(txtNguoiGiaoHoSo.MyID);
                objEmrBa.TongketbaMaNguoigiaohoso = txtNguoiGiaoHoSo.Text;
                //objEmrBa.IdNguoinhanHoso = Utility.Int16Dbnull(txtNguoiNhanHoSo.MyID);
                //objEmrBa.TongketbaMaNguoiNhanhoso = txtNguoiNhanHoSo.Text;
                objEmrBa.MabacsiLamBA = txtBSlamBA.MyCode;
                objEmrBa.IdBacsiLamBA = Utility.Int16Dbnull(txtBSlamBA.MyID);
                objEmrBa.TenbacsiLamBA = txtBSlamBA.Text;
                objEmrBa.TenbacsiDieutri = txtBSDieuTri.Text;
                objEmrBa.IdGiamdoc = Utility.Int16Dbnull(txtGDBV.MyID);
                objEmrBa.MaGiamdoc = txtGDBV.MyCode;
                objEmrBa.IdBacsiDieutri = Utility.Int16Dbnull(txtBSDieuTri.MyID);
                objEmrBa.MabacsiDieutri = txtBSDieuTri.MyCode;

                objEmrBa.IdBacsiKham = Utility.Int16Dbnull(txtBacsiKham.MyID);
                objEmrBa.MabacsiKham = txtBacsiKham.MyCode;

                objEmrBa.IdTruongkhoadieutri = Utility.Int16Dbnull(txtTruongkhoa.MyID);
                objEmrBa.MaTruongkhoadieutri = txtTruongkhoa.MyCode;


                objEmrBa.TongketbaSotoCt = Utility.Int16Dbnull(txtB_CTScanner.Text);
                objEmrBa.TongketbaSotoXquang = Utility.Int16Dbnull(txtB_Xquang.Text);
                objEmrBa.TongketbaSotoSieuam = Utility.Int16Dbnull(txtB_SieuAm.Text);
                objEmrBa.TongketbaSotoXetnghiem = Utility.Int16Dbnull(txtB_XetNghiem.Text);
                objEmrBa.TongketbaSotoKhac = Utility.Int16Dbnull(txtB_Khac.Text);
                objEmrBa.TongketbaNgay = dtpNgayTKBA.Value;
                return objEmrBa;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
                return objEmrBa;

            }
        }

        private void frm_BenhAn_SanKhoa_KeyDown(object sender, KeyEventArgs e)
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
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (cmdKhoitaoBA.Enabled)
                    cmdKhoitaoBA.PerformClick();
                else
                    cmdSave.PerformClick();
            }
            //if (e.KeyCode == Keys.F4) cmdInBenhAn.PerformClick();
            if (e.KeyCode == Keys.Escape) Close();
            if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTabBA.SelectedIndex = 0;
                ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
            }
            else if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTabBA.SelectedIndex = 1;
            }
            else if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTabBA.SelectedIndex = 2;
            }
            else if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTabBA.SelectedIndex = 3;
            }
            else if(e.KeyCode==Keys.F5)
            {
                PhanquyenTinhnang();
            }    
        }
        public action m_enAct = action.Insert;
        private void frm_BenhAn_SanKhoa_Load(object sender, EventArgs e)
        {
            try
            {
                ucThongtinnguoibenh_emr_basic1.noitrungoaitru = 1;
                ucThongtinnguoibenh_emr_basic1.AutoLoad = true;
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
                txtNguoiGiaoHoSo.Init(txtBSDieuTri.AutoCompleteSource, txtBSDieuTri.defaultItem);
                txtNguoiNhanHoSo.Init(txtBSDieuTri.AutoCompleteSource, txtBSDieuTri.defaultItem);
                txtGDBV.Init(txtBSDieuTri.AutoCompleteSource, txtBSDieuTri.defaultItem);
                VMS.HIS.Danhmuc.Util.SetNguoiDaiDienDonVi(txtGDBV);
                txtTruongkhoa.Init(txtBSDieuTri.AutoCompleteSource, txtBSDieuTri.defaultItem);
                txt_chandoan_sauphauthuat.Init(globalVariables.gv_dtDmucBenh, new List<string> { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });
                txt_chandoan_truocphauthuat.Init(globalVariables.gv_dtDmucBenh, new List<string> { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });
                txtTinhhinhphauthuat_cd_truocphauthuat.Init(globalVariables.gv_dtDmucBenh, new List<string> { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });
                txtTinhhinhphauthuat_cd_sauphauthuat.Init(globalVariables.gv_dtDmucBenh, new List<string> { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });

                DataTable dtData =
                    new Select().From(DmucChung.Schema)
                        .Where(DmucChung.Columns.Loai).IsEqualTo("EMR_LOAIBA")
                        .And(DmucChung.Columns.TrangThai).IsEqualTo(1)
                        .And(DmucChung.Columns.Ma).In(lstLoaiBA.Split(',').ToList<string>())
                        .OrderAsc(DmucChung.Columns.SttHthi)
                        .ExecuteDataSet().Tables[0];
                if (dtData.Rows.Count > 1)
                {
                    DataRow dr = dtData.NewRow();
                    dr[DmucChung.Columns.Ten] = "---Chọn loại BA---";
                    dr[DmucChung.Columns.Ma] = "-1";

                    dtData.Rows.InsertAt(dr, 0);
                }
                DataBinding.BindDataCombobox(cboLoaiBA, dtData, "MA", "TEN");
                txtBenhAnLyDoNhapVien.Init();
                if (m_enAct != action.Insert) ucThongtinnguoibenh_emr_basic1.Refresh();
                //if (m_enAct == action.Insert)
                //{

                //}
                //else
                //{
                //    objLuotkham = Utility.getKcbLuotkham(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham);
                //    objBenhnhan = Utility.getKcbDanhsachBenhnhan(objLuotkham);
                //    dt_ThongtinNguoibenh = SPs.EmrLaythongtinnguoibenhMaluotkhamIdbenhnhan(objLuotkham.IdBenhnhan,objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                //    FillData4Update();

                //}

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
                .Where(KcbPhieuchuyenvien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
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
       
        EmrPhieutheodoiTaibuongde objPhieutheodoitaibuongde;
         EmrPhieukhambenh objPKB;
        string maBA = "";
        private bool _isSuccess = false;
        void FillData4Update()
        {
            try
            {
                maBA = "";

                isAllowChangedNgayTuoi = false;

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

                dtCacKhoa = new KCB_THAMKHAM().NoitruTimkiemlichsuBuonggiuong(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, "-1", -1);
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
                if (objLuotkham.NgayNhapvien.HasValue)
                    dtQLNBVaoVien.Value = objLuotkham.NgayNhapvien.Value;
                else
                    dtQLNBVaoVien.ResetText();
                if (objLuotkham.NgayRavien.HasValue)
                    dtpRavien_ngay.Value = objLuotkham.NgayRavien.Value;//.Value.ToString("dd/MM/yyyy HH:mm:ss");
                else
                    dtpRavien_ngay.ResetText();
                txtQLNBTongSoNgayDieuTri.Text = Utility.sDbnull(objLuotkham.SongayDieutri);
                Utility.GetChanDoanNoitru(objLuotkham, ref ICD_Khoa_NoITru, ref Name_Khoa_NoITru);
                FillThongtinRavien();
                FillThongtinChuyenVien();
                FillTongketBenhAn();
                FillThongtinPTTT();
                FillThongtinTienSuSanKhoa();
                //Trang 2
                FillThongtinNhapvien();
                FillPhieuKhamSanKhoa();
              
                FillPhieutheodoitaiBuongde();
                //Trang 3
                FillPhieuKCB();
                txtCDKhiVaoDieuTri.Text = Name_Khoa_NoITru;
                txtCDMaKhiVaoDieuTri.Text = ICD_Khoa_NoITru;
                
                if (objEmrBa != null)
                {
                    m_enAct = action.Update;
                    cboLoaiBA.SelectedIndex = Utility.GetSelectedIndex(cboLoaiBA, objEmrBa.LoaiBa);
                    maBA = objEmrBa.MaBa;
                    dtDataBA = SPs.EmrBaLaythongtin(-1, "",  objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
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
                       

                       
                       
                        lblqlbnKhoa.Text = objEmrBa.VaovienTenkhoa;
                        lblMakhoavao.Text = objEmrBa.VaovienMakhoa;
                        chkQLNBTuyenTren.Checked = Utility.Bool2Bool(objEmrBa.ChuyenvienTuyentren);
                        chkQLNBTuyenDuoi.Checked = Utility.Bool2Bool(objEmrBa.ChuyenvienTuyenduoi);
                        chkQLNBChuyenVienCK.Checked = Utility.Bool2Bool(objEmrBa.ChuyenvienKhac);
                        txtQLNBChuyenVienNoiChuyenDen.Text = Utility.sDbnull(objEmrBa.ChuyenvienNoichuyenden);
                        if (objEmrBa.TrangThai >= 1)
                        {
                            if (objEmrBa.RavienNgay.HasValue)
                                dtpRavien_ngay.Value = objEmrBa.RavienNgay.Value;
                            else
                                dtpRavien_ngay.ResetText();
                            chkQLNBRaVienRavien.Checked = Utility.Bool2Bool(objEmrBa.RavienRavien);
                            chkQLNBRavienXinVe.Checked = Utility.Bool2Bool(objEmrBa.RavienXinve);
                            chkQLNBRavienBoVe.Checked = Utility.Bool2Bool(objEmrBa.RavienBove);
                            chkQLNBRavienDuaVe.Checked = Utility.Bool2Bool(objEmrBa.RavienDuave);
                        }
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

                        chk_cd_dogayme.Checked = Utility.Bool2Bool(objEmrBa.CdDogayme);
                        chk_cd_dophauthuat.Checked = Utility.Bool2Bool(objEmrBa.CdPhauthuat);
                        chk_cd_donhiemkhuan.Checked = Utility.Bool2Bool(objEmrBa.CdDonhiemkhuan);
                        chk_cd_dokhac.Checked = Utility.Bool2Bool(objEmrBa.CdTaibienBienchungKhac);
                        chkCDTaiBien.Checked = Utility.Bool2Bool(objEmrBa.CdTaibien);
                        chkCDBienChung.Checked = Utility.Bool2Bool(objEmrBa.CdBienchung);
                        nmr_cd_tongsolanphauthuat.Value = Utility.Int32Dbnull(objEmrBa.CdTongsolanphauthuat);
                        nmr_cd_tongsongaydieutri_sauphauthuat.Value = Utility.Int32Dbnull(objEmrBa.CdTongsongaydieutriSauphauthuat);
                     
                        txt_chandoan_truocphauthuat.SetCode(objEmrBa.MaChandoanTruocphauthuat);
                        lbl_ma_chandoan_truocphauthuat.Text = Utility.sDbnull(objEmrBa.MaChandoanTruocphauthuat);
                        txt_chandoan_sauphauthuat.SetCode(Utility.sDbnull(objEmrBa.MaChandoanSauphauthuat));
                        lbl_ma_chandoan_sauphauthuat.Text = Utility.sDbnull(objEmrBa.MaChandoanSauphauthuat);
                        //Tình trạng ra viện
                        if (objEmrBa.TrangThai >= 1)
                        {
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
                            chkttrvDoKhac.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienLydotuvongKhac);
                            chkttrvTrong24GioVaoVien.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienThoigiantuvongTrong24h);
                            chkttrvngoai24gioVaoVien.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienThoigiantuvongTrong48h);

                            txtTTRVNguyenNhanChinhTuVong.Text = Utility.sDbnull(objEmrBa.TinhtrangravienNguyennhantuvong);
                            chkTTRVChandoanGiaiphauTuthi.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienKhamnghiemtuthi);
                            txtTTRVChandoanGiaiphauTuthi.Text = Utility.sDbnull(objEmrBa.TinhtrangravienChandoangiauphaututhi);
                        }
                        //Tờ 2
                        txtBenhAnLyDoNhapVien._Text = Utility.sDbnull(objEmrBa.VaovienLydovaovien);// Utility.sDbnull(dr["BaLdvv"].ToString());
                        txtBenhAnVaoNgayThu.Text = Utility.sDbnull(objEmrBa.VaovienVaongaythucuabenh);
                        txtBenhAnQuaTrinhBenhLy.Text = Utility.sDbnull(objEmrBa.HoibenhQuatrinhbenhly);// Utility.sDbnull(dr["BaQtbl"].ToString());
                        txtBenhAnTiensuBanthan.Text = Utility.sDbnull(objEmrBa.HoibenhTiensubanthan);

                        //Thông tin khám phụ khoa
                       
                      
//                        #region Tiền sử sản phụ khoa
//                        dtpBatdauthaykinhnam.Text = Utility.sDbnull(objEmrBa.BatdauthaykinhNam);
//                        nmrBatdauthaykinhtuoi.Text = Utility.sDbnull(objEmrBa.BatdauthaykinhTuoi);
//                        txt_tinhchatkinhnguyet.Text = Utility.sDbnull(objEmrBa.Tinhchatkinhnguyet);
//                        txt_chuky.Text = Utility.sDbnull(objEmrBa.Chuky);
//                        txt_songaythaykinh.Text = Utility.sDbnull(objEmrBa.Songaythaykinh);
//                        txt_luongkinh.Text = Utility.sDbnull(objEmrBa.Luongkinh);
//                        if (objEmrBa.HoibenhKinhcuoitungay.HasValue)
//                            dtpKinhlancuoingay.Value = objEmrBa.HoibenhKinhcuoitungay.Value;
//                        else
//                            dtpKinhlancuoingay.ResetText();
//                        chkCodaubung.Checked = Utility.Bool2Bool(objEmrBa.Codaubung);
//                        chk_thoigiantruoc.Checked = Utility.Bool2Bool(objEmrBa.ThoigianTruoc);
//                        chk_thoigiantrong.Checked = Utility.Bool2Bool(objEmrBa.ThoigianTrong);
//                        chk_thoigiansau.Checked = Utility.Bool2Bool(objEmrBa.ThoigianSau);
//                        dtpLaychongNam.Text = Utility.sDbnull(objEmrBa.HoibenhLaychongNam);
//                        nmrLaychongTuoi.Text = Utility.sDbnull(objEmrBa.HoibenhLaychongTuoi);
//                        dtpHetKinhNam.Text = Utility.sDbnull(objEmrBa.Hetkinhnam);
//                        nmrHetkinhTuoi.Text = Utility.sDbnull(objEmrBa.Hetkinhtuoi);
//                        txt_benhphukhoadadieutri.Text = Utility.sDbnull(objEmrBa.HoibenhNhungbenhphukhoadadieutri);
//                        //txt_para.Text = Utility.sDbnull(objEmrBa.pa);
//                        #endregion
//                        #region "Chẩn đoán sản khoa"
//                        txtLucvaode.Text = Utility.sDbnull(objEmrBa.CdLucvaode);
//                        txtNgoithai.Text = Utility.sDbnull(objEmrBa.CdNgoithai);
//                        txtCachthucde.Text = Utility.sDbnull(objEmrBa.CdCachthucde);
//                        txtKiemsoattucung.Text = Utility.sDbnull(objEmrBa.CdKiemsoattucung);
//                        txtDitat.Text = Utility.sDbnull(objEmrBa.CdDitatThainhi);
//                        nmrCannang.Text = Utility.sDbnull(objEmrBa.CdCannangThainhi);
//                        if (objEmrBa.CdNgaymode.HasValue)
//                            dtpNgaymode.Value = objEmrBa.CdNgaymode.Value;
//                        else
//                            dtpNgaymode.ResetText();
//                        txt_cd_phuongphapphuatthuat.Text = Utility.sDbnull(objEmrBa.CdPhuongphapphauthuat);
//                        optDonthai.Checked = Utility.Bool2Bool(objEmrBa.CdDonthai);
//                        optDathai.Checked = Utility.Bool2Bool(objEmrBa.CdDathai);
//                        optTrai.Checked = Utility.Bool2Bool(objEmrBa.CdTrai);
//                        optGai.Checked = Utility.Bool2Bool(objEmrBa.CdGai);
//                        optSong.Checked = Utility.Bool2Bool(objEmrBa.CdSong);
//                        optChet.Checked = Utility.Bool2Bool(objEmrBa.CdChet);
//                        #endregion
//                        #region Quá trình thai kỳ
//                        if (objEmrBa.HoibenhKinhcuoitungay.HasValue)
//                            dtpKinhcuoitungay.Value = objEmrBa.HoibenhKinhcuoitungay.Value;
//                        else
//                            dtpKinhcuoitungay.ResetText();
//                        if (objEmrBa.HoibenhKinhcuoiden.HasValue)
//                            dtpKinhcuoidenngay.Value = objEmrBa.HoibenhKinhcuoiden.Value;
//                        else
//                            dtpKinhcuoidenngay.ResetText();
//                        txtKhamthaitai.Text = Utility.sDbnull(objEmrBa.HoibenhKhamthaitai);
//                        chkTiemphonguonvan.Checked = Utility.Bool2Bool(objEmrBa.HoibenhTiemphongUonvan);
//                        txtDuoctiemphonguonvanSolan.Text = Utility.sDbnull(objEmrBa.HoibenhTiemphongUonvanSolan);
//                        if (objEmrBa.HoibenhBatdauchuyenda.HasValue)
//                            dtpBatdauchuyendatu.Value = objEmrBa.HoibenhBatdauchuyenda.Value;
//                        txtDauhieuLucdau.Text = Utility.sDbnull(objEmrBa.HoibenhDauhieulucdau);
//                        txtBienchuyen.Text = Utility.sDbnull(objEmrBa.HoibenhBienchuyen);
//                        #endregion
//                        #region Khám sản khoa

//                        //khám ngoài
//                        chkBungcoseophauthuatcu.Checked = Utility.Bool2Bool(objEmrBa.KhamngoaiBungcoseophauthuatcu);
//                        txtHinhdangTucung.Text = Utility.sDbnull(objEmrBa.KhamngoaiHinhdangtucung);
//                        txtTutheTucung.Text = Utility.sDbnull(objEmrBa.KhamngoaiTuthe);
//                        txtChieucaoTC.Text = Utility.sDbnull(objEmrBa.KhamngoaiChieucaotucung);
//                        txtVongbung.Text = Utility.sDbnull(objEmrBa.KhamngoaiVongbung);
//                        txtConcoTC.Text = Utility.sDbnull(objEmrBa.KhamngoaiConcotucung);
//                        txtTimthai.Text = Utility.sDbnull(objEmrBa.KhamngoaiTimthai);
//                        txtVu.Text = Utility.sDbnull(objEmrBa.KhamngoaiVu);

//                        //Khám trong
//                        txtChisoBishop.Text = Utility.sDbnull(objEmrBa.KhamtrongChisoBishop);
//                        txtAmho.Text = Utility.sDbnull(objEmrBa.KhamtrongAmho);
//                        txtAmdao.Text = Utility.sDbnull(objEmrBa.KhamtrongAmdao);
//                        txtTangsinhmon.Text = Utility.sDbnull(objEmrBa.KhamtrongTangsinhmon);
//                        txtCoTC.Text = Utility.sDbnull(objEmrBa.KhamtrongCotucung);
//                        txtPhanphu.Text = Utility.sDbnull(objEmrBa.KhamtrongPhanphu);

//                        optOiphong.Checked = Utility.Bool2Bool(objEmrBa.KhamtrongTinhtrangoiPhong);
//                        optOidet.Checked = Utility.Bool2Bool(objEmrBa.KhamtrongTinhtrangoiDet);
//                        optOiquale.Checked = Utility.Bool2Bool(objEmrBa.KhamtrongTinhtrangoiQuale);

//                        optOivoTunhien.Checked = Utility.Bool2Bool(objEmrBa.KhamtrongTinhtrangoivoTunhien);
//                        optOivoBamoi.Checked = Utility.Bool2Bool(objEmrBa.KhamtrongTinhtrangoivoBamoi);

//                        optDolotCao.Checked = Utility.Bool2Bool(objEmrBa.KhamtrongDolotCao);
//                        optDolotChuc.Checked = Utility.Bool2Bool(objEmrBa.KhamtrongDolotChuc);
//                        optDolotChat.Checked = Utility.Bool2Bool(objEmrBa.KhamtrongDolotChat);
//                        optDolotLot.Checked = Utility.Bool2Bool(objEmrBa.KhamtrongDolotLot);

//                        txtMausacNuocoi.Text = Utility.sDbnull(objEmrBa.KhamtrongMausacnuocoi);
//                        txtNuocoiNhieuhayIt.Text = Utility.sDbnull(objEmrBa.KhamtrongNuocoinhieuit);
//                        txtKbNgoi.Text = Utility.sDbnull(objEmrBa.KhamtrongNgoi);
//                        txtThe.Text = Utility.sDbnull(objEmrBa.KhamtrongThe);
//                        txtKieuthe.Text = Utility.sDbnull(objEmrBa.KhamtrongKieuthe);
//                        txtDuongkinhnhoHave.Text = Utility.sDbnull(objEmrBa.KhamtrongDuongkinhnhohave);
//                        txtBacsiKham.SetId(objEmrBa.IdBacsiKham);
//                        //dtNgayKham.Value = Convert.ToDateTime(string.IsNullOrEmpty(objEmrBa.NgayKham) ? dtNgayKham.Value : objEmrBa.NgayKham);
//                        dtpNgayKham.Value = string.IsNullOrEmpty(objEmrBa.NgayKham.ToString()) ? dtpNgayKham.Value : Convert.ToDateTime(objEmrBa.NgayKham);
//#endregion
//                        #region Phiếu theo dõi tại buồng đẻ
//                        if (objEmrBa.Vaobuongdeluc.HasValue)
//                            dtpVaobuongdeluc.Value = objEmrBa.Vaobuongdeluc.Value;
//                        txtTennguoitheodoi.Text = objEmrBa.Nguoitheodoi;
//                        txtChucdanhnguoitheodoi.Text = objEmrBa.Chucdanh;
//                        if (objEmrBa.Deluc.HasValue)
//                            dtpDeluc.Value = objEmrBa.Deluc.Value;

//                        txt1phut.Text = objEmrBa.Apgar1phut;
//                        txt5phut.Text = objEmrBa.Apgar5phut;
//                        txt10phut.Text = objEmrBa.Apgar10phut;

//                        nmrCannangRau.Value = Utility.Int32Dbnull(objEmrBa.TresosinhCannang);
//                        nmrcao.Value = Utility.Int32Dbnull(objEmrBa.TresosinhCao);
//                        nmrvongdau.Value = Utility.Int32Dbnull(objEmrBa.TresosinhVongdau);
//                        optDonthaiTrai.Checked = Utility.Bool2Bool(objEmrBa.TresosinhDonthaiTrai);
//                        optDonthaiGai.Checked = Utility.Bool2Bool(objEmrBa.TresosinhDonthaiGai);
//                        optDathaiTrai.Checked = Utility.Bool2Bool(objEmrBa.TresosinhDathaiTrai);
//                        optDathaiGai.Checked = Utility.Bool2Bool(objEmrBa.TresosinhDathaiGai);
//                        chkTatbamsinh.Checked = Utility.Bool2Bool(objEmrBa.TresosinhTatbamsinh);
//                        chkRaucuonco.Checked = Utility.Bool2Bool(objEmrBa.SorauRaucuonco);

//                        txtCuthetatbamsinh.Text = objEmrBa.TresosinhCuthetatbamsinh;
//                        txtTinhtrangtresosinhsaukhide.Text = objEmrBa.TresosinhTinhtrangsaude;
//                        txtXulyvaketquaTresosinh.Text = objEmrBa.TresosinhXulyvaketqua;

//                        optRauboc.Checked = Utility.Bool2Bool(objEmrBa.SorauBoc);
//                        optRauso.Checked = Utility.Bool2Bool(objEmrBa.SorauSo);
//                        if (objEmrBa.SorauLuc.HasValue)
//                            dtpRausoluc.Value = objEmrBa.SorauLuc.Value;

//                        txtCachsorau.Text = objEmrBa.SorauCachsorau;
//                        txtMatmang.Text = objEmrBa.SorauMatmang;
//                        txtMatmui.Text = objEmrBa.SorauMatmui;
//                        txtBanhrau.Text = objEmrBa.SorauBanhrau;
//                        nmrCannangRau.Value = Utility.Int32Dbnull(objEmrBa.SorauCannang);
//                        chkRaucuonco.Checked = Utility.Bool2Bool(objEmrBa.SorauRaucuonco);
//                        nmrCuongrau.Value = Utility.Int32Dbnull(objEmrBa.CuongrauDai);
//                        chkCochaymausauso.Checked = Utility.Bool2Bool(objEmrBa.SorauChaymausauso);
//                        nmrLuongmaumat.Value = Utility.Int32Dbnull(objEmrBa.SorauLuongmaumat);
//                        chkKiemsoattucung.Checked = Utility.Bool2Bool(objEmrBa.SorauKiemsoattucung);
//                        txtXulyvaketquaRau.Text = Utility.sDbnull(objEmrBa.SorauXulyvaketqua);

//                        txtSanphuDaniemmac.Text = Utility.sDbnull(objEmrBa.SanphuDaniemmac);
//                        optDethuong.Checked = Utility.Bool2Bool(objEmrBa.SanphuPhuongphapdeThuong);
//                        optForceps.Checked = Utility.Bool2Bool(objEmrBa.SanphuPhuongphapdeForceps);
//                        optGiachut.Checked = Utility.Bool2Bool(objEmrBa.SanphuPhuongphapdeGiachut);
//                        optPhauthuat.Checked = Utility.Bool2Bool(objEmrBa.SanphuPhuongphapdePt);
//                        optDechihuy.Checked = Utility.Bool2Bool(objEmrBa.SanphuPhuongphapdeDechihuy);
//                        optKhac.Checked = Utility.Bool2Bool(objEmrBa.SanphuPhuongphapdeKhac);
//                        txtLydocanthiep.Text = Utility.sDbnull(objEmrBa.SanphuLydocanthiep);
//                        optTangsinhmonRach.Checked = Utility.Bool2Bool(objEmrBa.SanphuTangsinhmonRach);
//                        optTangsinhmonKhongrach.Checked = Utility.Bool2Bool(objEmrBa.SanphuTangsinhmonKhongrach);
//                        optTangsinhmonCat.Checked = Utility.Bool2Bool(objEmrBa.SanphuTangsinhmonCat);
//                        chkPhuongphapkhauvaloaichi.Checked = Utility.Bool2Bool(objEmrBa.SanphuPhuongphapkhauvaloaichi);
//                        txtPhuongphapkhauvaloaichi.Text = Utility.sDbnull(objEmrBa.SanphuPhuongphapkhauvaloaichiMota);
//                        nmrSomuikhau.Value = Utility.Int32Dbnull(objEmrBa.SanphuSomuikhau);
//                        optCotucungKhongrach.Checked = Utility.Bool2Bool(objEmrBa.SanphuCotucungKhongrach);
//                        optCotucungRach.Checked = Utility.Bool2Bool(objEmrBa.SanphuCotucungRach);
//#endregion
                        txtTinhhinhphauthuat_cd_truocphauthuat._Text= Utility.sDbnull(objEmrBa.TinhinhphauthuatChandoantruocphauthuat);
                        txtTinhhinhphauthuat_cd_sauphauthuat._Text = Utility.sDbnull(objEmrBa.TinhinhphauthuatChandoansauphauthuat);
                        chk_tinhinhphauthuat_taibien.Checked = Utility.Byte2Bool(objEmrBa.TinhinhphauthuatTaibien);
                        chk_tinhinhphauthuat_bienchung.Checked = Utility.Byte2Bool(objEmrBa.TinhinhphauthuatBienchung);
                        chk_tinhinhphauthuat_dophauthuat.Checked = Utility.Byte2Bool(objEmrBa.TinhinhphauthuatDophauthuat);
                        chk_tinhinhphauthuat_dogayme.Checked = Utility.Byte2Bool(objEmrBa.TinhinhphauthuatDogayme);
                        chk_tinhinhphauthuat_donhiemkhuan.Checked = Utility.Byte2Bool(objEmrBa.TinhinhphauthuatDonhiemkhuan);
                        chk_tinhinhphauthuat_dokhac.Checked = Utility.Byte2Bool(objEmrBa.TinhinhphauthuatDokhac);

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

                        txtNguoiGiaoHoSo.SetId(objEmrBa.IdNguoigiaoHoso);
                        txtNguoiNhanHoSo.SetId(objEmrBa.IdNguoinhanHoso);
                        txtBSDieuTri.SetId(objEmrBa.IdBacsiDieutri);
                        txtGDBV.SetId(objEmrBa.IdGiamdoc);
                        txtTruongkhoa.SetId(objEmrBa.IdTruongkhoadieutri);

                        txtBacsiKham.SetId(objEmrBa.IdBacsiKham);
                        txtBSlamBA.SetId(objEmrBa.IdBacsiLamBA);

                        txtB_CTScanner.Text = Utility.sDbnull(objEmrBa.TongketbaSotoCt);
                        txtB_Xquang.Text = Utility.sDbnull(objEmrBa.TongketbaSotoXquang);
                        txtB_SieuAm.Text = Utility.sDbnull(objEmrBa.TongketbaSotoSieuam);
                        txtB_XetNghiem.Text = Utility.sDbnull(objEmrBa.TongketbaSotoXetnghiem);
                        txtB_Khac.Text = Utility.sDbnull(objEmrBa.TongketbaSotoKhac);
                        if (objEmrBa.TongketbaNgay.HasValue)
                            dtpNgayTKBA.Value = objEmrBa.TongketbaNgay.Value;
                        else
                            dtpNgayTKBA.Value = DateTime.Now;
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
                   
                   
                    //Trang 4
                    //GetChanDoanKKB();
                    txtCDKKBCapCuu.Text = Utility.Get_ChanDoan_KKB_CapCuu(objLuotkham);
                    txtCDMaKKBCapCuu.Text = Utility.sDbnull(objLuotkham.MabenhChinh, string.Empty);
                    KcbThongtinchung tef = new Select().From(KcbThongtinchung.Schema)
                        .Where(KcbThongtinchung.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
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
                isAllowChangedNgayTuoi = true;
                EnableBA();
            }
        }
        DataTable dt_tssk = new DataTable();
        void FillThongtinTienSuSanKhoa()
        {
            dt_tssk = new Select().From(EmrTiensuSankhoa.Schema)
                    .Where(EmrQuatrinhThaiky.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrQuatrinhThaiky.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx_Basic(grdTiensuSankhoa, dt_tssk, true, true, "",
                                                   EmrTiensuSankhoa.Columns.Nam); //"locked=0", "");
        }
        DataTable dtPhieuPttt = new DataTable();
        void FillThongtinPTTT()
        {
            dtPhieuPttt = SPs.EmrLaythongtinPhieuPtttTo4(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
            grdPTTT.DataSource = dtPhieuPttt;
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
                txtVu.Text = Utility.sDbnull(objPKB.Vu);
                txtHach.Text = Utility.sDbnull(objPKB.Hach);
                txtBenhAnTuanHoan.Text = Utility.sDbnull(objPKB.Tuanhoan);
                txtBenhAnHoHap.Text = Utility.sDbnull(objPKB.Hohap);
                txtBenhAnTieuHoa.Text = Utility.sDbnull(objPKB.Tieuhoa);
                txtBenhAnThanTietNieuSinhDuc.Text = Utility.sDbnull(objPKB.Thantietnieusinhduc);
                txtBenhAnThanKinh.Text = Utility.sDbnull(objPKB.Thankinh);
                txtBenhAnCoXuongKhop.Text = Utility.sDbnull(objPKB.Coxuongkhop);
                txtBenhAnTaiMuiHong.Text = Utility.sDbnull(objPKB.Taimuihong);
                txtBenhAnRangHamMat.Text = Utility.sDbnull(objPKB.Ranghammat);
                txtBenhAnMat.Text = Utility.sDbnull(objPKB.Mat);
                txtDaniemmac.Text = objPKB.MausacDa;
                txtHach.Text = objPKB.Hach;
                txtVu.Text = objPKB.Vu;
            }
        }
        public void FillPhieutheodoitaiBuongde()
        {
            try
            {
               
                    objPhieutheodoitaibuongde = new Select().From(EmrPhieutheodoiTaibuongde.Schema)
                        .Where(EmrPhieutheodoiTaibuongde.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(EmrPhieutheodoiTaibuongde.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<EmrPhieutheodoiTaibuongde>();
                if (objPhieutheodoitaibuongde != null)
                {
                    if (objPhieutheodoitaibuongde.Vaobuongdeluc.HasValue)
                        dtpVaobuongdeluc.Value = objPhieutheodoitaibuongde.Vaobuongdeluc.Value;
                    txtTennguoitheodoi.Text = objPhieutheodoitaibuongde.Nguoitheodoi;
                    txtChucdanhnguoitheodoi.Text = objPhieutheodoitaibuongde.Chucdanh;
                    if (objPhieutheodoitaibuongde.Deluc.HasValue)
                        dtpDeluc.Value = objPhieutheodoitaibuongde.Deluc.Value;

                    txt1phut.Text = objPhieutheodoitaibuongde.Apgar1phut;
                    txt5phut.Text = objPhieutheodoitaibuongde.Apgar5phut;
                    txt10phut.Text = objPhieutheodoitaibuongde.Apgar10phut;

                    nmrCannangRau.Value = Utility.Int32Dbnull(objPhieutheodoitaibuongde.TresosinhCannang);
                    nmrcao.Value = Utility.Int32Dbnull(objPhieutheodoitaibuongde.TresosinhCao);
                    nmrvongdau.Value = Utility.Int32Dbnull(objPhieutheodoitaibuongde.TresosinhVongdau);
                    optDonthaiTrai.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.TresosinhDonthaiTrai);
                    optDonthaiGai.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.TresosinhDonthaiGai);
                    optDathaiTrai.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.TresosinhDathaiTrai);
                    optDathaiGai.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.TresosinhDathaiGai);
                    chkTatbamsinh.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.TresosinhTatbamsinh);
                    chkRaucuonco.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SorauRaucuonco);

                    txtCuthetatbamsinh.Text = objPhieutheodoitaibuongde.TresosinhCuthetatbamsinh;
                    txtTinhtrangtresosinhsaukhide.Text = objPhieutheodoitaibuongde.TresosinhTinhtrangsaude;
                    txtXulyvaketquaTresosinh.Text = objPhieutheodoitaibuongde.TresosinhXulyvaketqua;

                    optRauboc.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SorauBoc);
                    optRauso.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SorauSo);
                    if (objPhieutheodoitaibuongde.SorauLuc.HasValue)
                        dtpRausoluc.Value = objPhieutheodoitaibuongde.SorauLuc.Value;

                    txtCachsorau.Text = objPhieutheodoitaibuongde.SorauCachsorau;
                    txtMatmang.Text = objPhieutheodoitaibuongde.SorauMatmang;
                    txtMatmui.Text = objPhieutheodoitaibuongde.SorauMatmui;
                    txtBanhrau.Text = objPhieutheodoitaibuongde.SorauBanhrau;
                    nmrCannangRau.Value = Utility.Int32Dbnull(objPhieutheodoitaibuongde.SorauCannang);
                    chkRaucuonco.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SorauRaucuonco);
                    nmrCuongrau.Value = Utility.Int32Dbnull(objPhieutheodoitaibuongde.CuongrauDai);
                    chkCochaymausauso.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SorauChaymausauso);
                    nmrLuongmaumat.Value = Utility.Int32Dbnull(objPhieutheodoitaibuongde.SorauLuongmaumat);
                    chkKiemsoattucung.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SorauKiemsoattucung);
                    txtXulyvaketquaRau.Text = Utility.sDbnull(objPhieutheodoitaibuongde.SorauXulyvaketqua);

                    txtSanphuDaniemmac.Text = Utility.sDbnull(objPhieutheodoitaibuongde.SanphuDaniemmac);
                    optDethuong.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuPhuongphapdeThuong);
                    optForceps.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuPhuongphapdeForceps);
                    optGiachut.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuPhuongphapdeGiachut);
                    optPhauthuat.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuPhuongphapdePt);
                    optDechihuy.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuPhuongphapdeDechihuy);
                    optKhac.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuPhuongphapdeKhac);
                    txtLydocanthiep.Text = Utility.sDbnull(objPhieutheodoitaibuongde.SanphuLydocanthiep);
                    optTangsinhmonRach.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuTangsinhmonRach);
                    optTangsinhmonKhongrach.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuTangsinhmonKhongrach);
                    optTangsinhmonCat.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuTangsinhmonCat);
                    chkPhuongphapkhauvaloaichi.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuPhuongphapkhauvaloaichi);
                    txtPhuongphapkhauvaloaichi.Text = Utility.sDbnull(objPhieutheodoitaibuongde.SanphuPhuongphapkhauvaloaichiMota);
                    nmrSomuikhau.Value = Utility.Int32Dbnull(objPhieutheodoitaibuongde.SanphuSomuikhau);
                    optCotucungKhongrach.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuCotucungKhongrach);
                    optCotucungRach.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuCotucungRach);

                }
                else
                    Utility.ClearAllInputControls(pnlPhieutheodoitaibuongde);
            }
            catch (System.Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        EmrChandoanSankhoa objChandoanSankhoa;
        EmrTiensusanphukhoa objTspk = null;
        EmrPhieukhamSankhoa objPhieukhamSankhoa = null;
        EmrQuatrinhThaiky objQttk;

        private void FillPhieuKhamSanKhoa()
        {
            try
            {
                isAllowChangedNgayTuoi = false;
                objPhieukhamSankhoa = new Select().From(EmrPhieukhamSankhoa.Schema)
          .Where(EmrPhieukhamSankhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
          .And(EmrPhieukhamSankhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
          .ExecuteSingle<EmrPhieukhamSankhoa>();
                objTspk = new Select().From(EmrTiensusanphukhoa.Schema)
          .Where(EmrTiensusanphukhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
          .And(EmrTiensusanphukhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
          .ExecuteSingle<EmrTiensusanphukhoa>();
                objQttk = new Select().From(EmrQuatrinhThaiky.Schema)
          .Where(EmrQuatrinhThaiky.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
          .And(EmrQuatrinhThaiky.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
          .ExecuteSingle<EmrQuatrinhThaiky>();
                objChandoanSankhoa = new Select().From(EmrChandoanSankhoa.Schema)
       .Where(EmrChandoanSankhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
       .And(EmrChandoanSankhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
       .ExecuteSingle<EmrChandoanSankhoa>();
                if (objPhieukhamSankhoa != null)
                {

                    //txtID.Text = objPhieukhamSankhoa.Id.ToString();
                    txtNhietDo.Text = objPhieukhamSankhoa.NhietDo;
                    txtha.Text = objPhieukhamSankhoa.NhomMau;
                    txtMach.Text = objPhieukhamSankhoa.Mach;
                    txtNhipTho.Text = objPhieukhamSankhoa.NhịpTho;
                    txtChieuCao.Text = objPhieukhamSankhoa.ChieuCao;
                    txtCanNang.Text = objPhieukhamSankhoa.CanNang;
                    txtBMI.Text = objPhieukhamSankhoa.Bmi;
                    // txtNhommau.SetCode(objPhieukhamSankhoa.NhomMau);
                    //khám ngoài
                    chkBungcoseophauthuatcu.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KhamngoaiBungcoseophauthuatcu);
                    txtHinhdangTucung.Text = Utility.sDbnull(objPhieukhamSankhoa.KhamngoaiHinhdangtucung);
                    txtTutheTucung.Text = Utility.sDbnull(objPhieukhamSankhoa.KhamngoaiTuthe);
                    txtChieucaoTC.Text = Utility.sDbnull(objPhieukhamSankhoa.KhamngoaiChieucaotucung);
                    txtVongbung.Text = Utility.sDbnull(objPhieukhamSankhoa.KhamngoaiVongbung);
                    txtConcoTC.Text = Utility.sDbnull(objPhieukhamSankhoa.KhamngoaiConcotucung);
                    txtTimthai.Text = Utility.sDbnull(objPhieukhamSankhoa.KhamngoaiTimthai);
                    txtVu.Text = Utility.sDbnull(objPhieukhamSankhoa.KhamngoaiVu);

                    //Khám trong
                    txtChisoBishop.Text = Utility.sDbnull(objPhieukhamSankhoa.KbChisoBishop);
                    txtAmho.Text = Utility.sDbnull(objPhieukhamSankhoa.KbAmho);
                    txtAmdao.Text = Utility.sDbnull(objPhieukhamSankhoa.KbAmdao);
                    txtTangsinhmon.Text = Utility.sDbnull(objPhieukhamSankhoa.KbTangsinhmon);
                    txtCoTC.Text = Utility.sDbnull(objPhieukhamSankhoa.KbCotucung);
                    txtPhanphu.Text = Utility.sDbnull(objPhieukhamSankhoa.KbPhanphu);

                    optOiphong.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbTinhtrangoiPhong);
                    optOidet.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbTinhtrangoiDet);
                    optOiquale.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbTinhtrangoiQuale);

                    optOivoTunhien.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbTinhtrangoivoTunhien);
                    optOivoBamoi.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbTinhtrangoivoBamoi);

                    optDolotCao.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbDolotCao);
                    optDolotChuc.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbDolotChuc);
                    optDolotChat.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbDolotChat);
                    optDolotLot.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbDolotLot);

                    txtMausacNuocoi.Text = Utility.sDbnull(objPhieukhamSankhoa.KbMausacnuocoi);
                    txtNuocoiNhieuhayIt.Text = Utility.sDbnull(objPhieukhamSankhoa.KbNuocoinhieuit);
                    txtKbNgoi.Text = Utility.sDbnull(objPhieukhamSankhoa.KbNgoi);
                    txtThe.Text = Utility.sDbnull(objPhieukhamSankhoa.KbThe);
                    txtKieuthe.Text = Utility.sDbnull(objPhieukhamSankhoa.KbKieuthe);
                    txtDuongkinhnhoHave.Text = Utility.sDbnull(objPhieukhamSankhoa.KbDuongkinhnhohave);


                    txtBacsiKham.SetId(Utility.Int32Dbnull(objPhieukhamSankhoa.IdBacsi));
                    //dtNgayKham.Value = Convert.ToDateTime(string.IsNullOrEmpty(objPhieukhamSankhoa.NgayKham) ? dtNgayKham.Value : objPhieukhamSankhoa.NgayKham);
                    dtpNgayKham.Value = string.IsNullOrEmpty(objPhieukhamSankhoa.NgayKham.ToString()) ? dtpNgayKham.Value : Convert.ToDateTime(objPhieukhamSankhoa.NgayKham);


                }
                else
                {
                    KcbThongtinchung tef = new Select().From(KcbThongtinchung.Schema)
                        .Where(KcbThongtinchung.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
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
                //Chẩn đoán sản khoa
                if (objChandoanSankhoa != null)
                {
                    //lnkChandoanSankhoa_UserName.Visible = objChandoanSankhoa.NguoiTao != globalVariables.UserName;
                    //pnlChanDoanSankhoa.Enabled = objChandoanSankhoa.NguoiTao == globalVariables.UserName;
                    //if (lnkChandoanSankhoa_UserName.Visible)
                    //{
                    //    lnkChandoanSankhoa_UserName.Text = string.Format("Dữ liệu này được tạo bởi người dùng: {0}", objChandoanSankhoa.NguoiTao);
                    //}
                    txtLucvaode.Text = Utility.sDbnull(objChandoanSankhoa.CdLucvaode);
                    txtNgoithai.Text = Utility.sDbnull(objChandoanSankhoa.CdNgoithai);
                    txtCachthucde.Text = Utility.sDbnull(objChandoanSankhoa.CdCachthucde);
                    txtKiemsoattucung.Text = Utility.sDbnull(objChandoanSankhoa.CdKiemsoattucung);
                    txtDitat.Text = Utility.sDbnull(objChandoanSankhoa.CdDitatThainhi);
                    nmrCannang.Text = Utility.sDbnull(objChandoanSankhoa.CdCannangThainhi);
                    if (objChandoanSankhoa.CdNgaymode.HasValue)
                        dtpNgaymode.Value = objChandoanSankhoa.CdNgaymode.Value;
                    else
                        dtpNgaymode.ResetText();
                    optDonthai.Checked = Utility.Bool2Bool(objChandoanSankhoa.CdDonthai);
                    optDathai.Checked = Utility.Bool2Bool(objChandoanSankhoa.CdDathai);
                    optTrai.Checked = Utility.Bool2Bool(objChandoanSankhoa.CdTrai);
                    optGai.Checked = Utility.Bool2Bool(objChandoanSankhoa.CdGai);
                    optSong.Checked = Utility.Bool2Bool(objChandoanSankhoa.CdSong);
                    optChet.Checked = Utility.Bool2Bool(objChandoanSankhoa.CdChet);
                }
                //else
                //    lnkChandoanSankhoa_UserName.Visible = false;
                //Quá trình thai kỳ
                if (objQttk != null)
                {
                    //lnkQuatrinhthaiky_UserName.Visible = objQttk.NguoiTao != globalVariables.UserName;
                    //pnlChanDoanSankhoa.Enabled = objQttk.NguoiTao == globalVariables.UserName;
                    //if (lnkChandoanSankhoa_UserName.Visible)
                    //{
                    //    lnkQuatrinhthaiky_UserName.Text = string.Format("Dữ liệu này được tạo bởi người dùng: {0}", objQttk.NguoiTao);
                    //}

                    if (objQttk.Kinhcuoitungay.HasValue)
                        dtpKinhcuoitungay.Value = objQttk.Kinhcuoitungay.Value;
                    else
                        dtpKinhcuoitungay.ResetText();
                    if (objQttk.Kinhcuoidenngay.HasValue)
                        dtpKinhcuoidenngay.Value = objQttk.Kinhcuoidenngay.Value;
                    else
                        dtpKinhcuoidenngay.ResetText();
                    txtKhamthaitai.Text = Utility.sDbnull(objQttk.Khamthaitai);
                    chkTiemphonguonvan.Checked = Utility.Bool2Bool(objQttk.TiemphongUonvan);
                    txtDuoctiemphonguonvanSolan.Text = Utility.sDbnull(objQttk.TiemphongUonvanSolan);
                    if (objQttk.Batdauchuyenda.HasValue)
                        dtpBatdauchuyendatu.Value = objQttk.Batdauchuyenda.Value;
                    txtDauhieuLucdau.Text = Utility.sDbnull(objQttk.Dauhieulucdau);
                    txtBienchuyen.Text = Utility.sDbnull(objQttk.Bienchuyen);
                }
                //else
                //    lnkQuatrinhthaiky_UserName.Visible = false;
                //Tiền sử sản phụ khoa
                if (objTspk != null)
                {
                    //lnkTiensuphukhoa.Visible = objTspk.NguoiTao != globalVariables.UserName;
                    //pnlChanDoanSankhoa.Enabled = objTspk.NguoiTao == globalVariables.UserName;
                    //if (lnkChandoanSankhoa_UserName.Visible)
                    //{
                    //    lnkTiensuphukhoa.Text = string.Format("Dữ liệu này được tạo bởi người dùng: {0}", objTspk.NguoiTao);
                    //}
                    dtpBatdauthaykinhnam.Text = Utility.sDbnull(objTspk.BaTsspkBatdauthaykinhNam);
                    nmrBatdauthaykinhtuoi.Text = Utility.sDbnull(objTspk.BaTsspkBatdauthaykinhTuoi);
                    txt_tinhchatkinhnguyet.Text = Utility.sDbnull(objTspk.BaTsspkTinhchatkinhnguyet);
                    txt_chuky.Text = Utility.sDbnull(objTspk.BaTsspkChukykinhnguyet);
                    txt_songaythaykinh.Text = Utility.sDbnull(objTspk.BaTsspkSongaythaykinh);
                    txt_luongkinh.Text = Utility.sDbnull(objTspk.BaTsspkLuongkinh);
                    if (objTspk.BaTsspkKinhlancuoingay.HasValue)
                        dtpKinhlancuoingay.Value = objTspk.BaTsspkKinhlancuoingay.Value;
                    else
                        dtpKinhlancuoingay.ResetText();
                    chkCodaubung.Checked = Utility.Bool2Bool(objTspk.BaTsspkCodaubung);
                    chk_thoigiantruoc.Checked = Utility.Bool2Bool(objTspk.BaTsspkThoigianTruoc);
                    chk_thoigiantrong.Checked = Utility.Bool2Bool(objTspk.BaTsspkThoigianTrong);
                    chk_thoigiansau.Checked = Utility.Bool2Bool(objTspk.BaTsspkThoigianSau);
                    dtpLaychongNam.Text = Utility.sDbnull(objTspk.BaTsspkLaychongNam);
                    nmrLaychongTuoi.Text = Utility.sDbnull(objTspk.BaTsspkLaychongTuoi);
                    dtpHetKinhNam.Text = Utility.sDbnull(objTspk.BaTsspkHetkinhnam);
                    nmrHetkinhTuoi.Text = Utility.sDbnull(objTspk.BaTsspkHetkinhtuoi);
                    txt_benhphukhoadadieutri.Text = Utility.sDbnull(objTspk.BaTsspkBenhphukhoadadieutri);
                    //txt_para.Text = Utility.sDbnull(objTspk.BaTsspkPara);
                    //}
                    //else
                    //    lnkTiensuphukhoa.Visible = false;
                }
            }
            catch (Exception)
            {


            }
            finally
            {
                isAllowChangedNgayTuoi = true;
                // ModifyCommmands();
            }


        }

        private void FillPhieuKhamSanKhoa_bak()
        {
            try
            {
                objPhieukhamSankhoa = new Select().From(EmrPhieukhamSankhoa.Schema)
           .Where(EmrPhieukhamSankhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
           .And(EmrPhieukhamSankhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
           .ExecuteSingle<EmrPhieukhamSankhoa>();
                objTspk = new Select().From(EmrTiensusanphukhoa.Schema)
          .Where(EmrTiensusanphukhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
          .And(EmrTiensusanphukhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
          .ExecuteSingle<EmrTiensusanphukhoa>();
                objQttk = new Select().From(EmrQuatrinhThaiky.Schema)
          .Where(EmrQuatrinhThaiky.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
          .And(EmrQuatrinhThaiky.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
          .ExecuteSingle<EmrQuatrinhThaiky>();
                objChandoanSankhoa = new Select().From(EmrChandoanSankhoa.Schema)
       .Where(EmrChandoanSankhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
       .And(EmrChandoanSankhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
       .ExecuteSingle<EmrChandoanSankhoa>();

                if (objPhieukhamSankhoa != null)
                {

                   // txtID.Text = objPhieukhamSankhoa.Id.ToString();
                    txtNhietDo.Text = objPhieukhamSankhoa.NhietDo;
                    txtha.Text = objPhieukhamSankhoa.NhomMau;
                    txtMach.Text = objPhieukhamSankhoa.Mach;
                    txtNhipTho.Text = objPhieukhamSankhoa.NhịpTho;
                    txtChieuCao.Text = objPhieukhamSankhoa.ChieuCao;
                    txtCanNang.Text = objPhieukhamSankhoa.CanNang;
                    txtBMI.Text = objPhieukhamSankhoa.Bmi;
                   // txtNhommau.SetCode(objPhieukhamSankhoa.NhomMau);
                    //khám ngoài
                    chkBungcoseophauthuatcu.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KhamngoaiBungcoseophauthuatcu);
                    txtHinhdangTucung.Text = Utility.sDbnull(objPhieukhamSankhoa.KhamngoaiHinhdangtucung);
                    txtTutheTucung.Text = Utility.sDbnull(objPhieukhamSankhoa.KhamngoaiTuthe);
                    txtChieucaoTC.Text = Utility.sDbnull(objPhieukhamSankhoa.KhamngoaiChieucaotucung);
                    txtVongbung.Text = Utility.sDbnull(objPhieukhamSankhoa.KhamngoaiVongbung);
                    txtConcoTC.Text = Utility.sDbnull(objPhieukhamSankhoa.KhamngoaiConcotucung);
                    txtTimthai.Text = Utility.sDbnull(objPhieukhamSankhoa.KhamngoaiTimthai);
                    txtVu.Text = Utility.sDbnull(objPhieukhamSankhoa.KhamngoaiVu);

                    //Khám trong
                    txtChisoBishop.Text = Utility.sDbnull(objPhieukhamSankhoa.KbChisoBishop);
                    txtAmho.Text = Utility.sDbnull(objPhieukhamSankhoa.KbAmho);
                    txtAmdao.Text = Utility.sDbnull(objPhieukhamSankhoa.KbAmdao);
                    txtTangsinhmon.Text = Utility.sDbnull(objPhieukhamSankhoa.KbTangsinhmon);
                    txtCoTC.Text = Utility.sDbnull(objPhieukhamSankhoa.KbCotucung);
                    txtPhanphu.Text = Utility.sDbnull(objPhieukhamSankhoa.KbPhanphu);

                    optOiphong.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbTinhtrangoiPhong);
                    optOidet.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbTinhtrangoiDet);
                    optOiquale.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbTinhtrangoiQuale);

                    optOivoTunhien.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbTinhtrangoivoTunhien);
                    optOivoBamoi.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbTinhtrangoivoBamoi);

                    optDolotCao.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbDolotCao);
                    optDolotChuc.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbDolotChuc);
                    optDolotChat.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbDolotChat);
                    optDolotLot.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbDolotLot);

                    txtMausacNuocoi.Text = Utility.sDbnull(objPhieukhamSankhoa.KbMausacnuocoi);
                    txtNuocoiNhieuhayIt.Text = Utility.sDbnull(objPhieukhamSankhoa.KbNuocoinhieuit);
                    txtKbNgoi.Text = Utility.sDbnull(objPhieukhamSankhoa.KbNgoi);
                    txtThe.Text = Utility.sDbnull(objPhieukhamSankhoa.KbThe);
                    txtKieuthe.Text = Utility.sDbnull(objPhieukhamSankhoa.KbKieuthe);
                    txtDuongkinhnhoHave.Text = Utility.sDbnull(objPhieukhamSankhoa.KbDuongkinhnhohave);


                    txtBacsiKham.SetId(objPhieukhamSankhoa.IdBacsi);
                    //dtNgayKham.Value = Convert.ToDateTime(string.IsNullOrEmpty(objPhieukhamSankhoa.NgayKham) ? dtNgayKham.Value : objPhieukhamSankhoa.NgayKham);
                    dtpNgayKham.Value = string.IsNullOrEmpty(objPhieukhamSankhoa.NgayKham.ToString()) ? dtpNgayKham.Value : Convert.ToDateTime(objPhieukhamSankhoa.NgayKham);


                }
                else
                {
                    KcbThongtinchung tef = new Select().From(KcbThongtinchung.Schema)
                        .Where(KcbThongtinchung.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
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
                
                //Chẩn đoán sản khoa
                if (objChandoanSankhoa != null)
                {
                    //lnkChandoanSankhoa_UserName.Visible = objChandoanSankhoa.NguoiTao != globalVariables.UserName;
                    //pnlChanDoanSankhoa.Enabled = objChandoanSankhoa.NguoiTao == globalVariables.UserName;
                    //if (lnkChandoanSankhoa_UserName.Visible)
                    //{
                    //    lnkChandoanSankhoa_UserName.Text = string.Format("Dữ liệu này được tạo bởi người dùng: {0}", objChandoanSankhoa.NguoiTao);
                    //}
                    txtLucvaode.Text = Utility.sDbnull(objChandoanSankhoa.CdLucvaode);
                    txtNgoithai.Text = Utility.sDbnull(objChandoanSankhoa.CdNgoithai);
                    txtCachthucde.Text = Utility.sDbnull(objChandoanSankhoa.CdCachthucde);
                    txtKiemsoattucung.Text = Utility.sDbnull(objChandoanSankhoa.CdKiemsoattucung);
                    txtDitat.Text = Utility.sDbnull(objChandoanSankhoa.CdDitatThainhi);
                    nmrCannang.Text = Utility.sDbnull(objChandoanSankhoa.CdCannangThainhi);
                    if (objChandoanSankhoa.CdNgaymode.HasValue)
                        dtpNgaymode.Value = objChandoanSankhoa.CdNgaymode.Value;
                    else
                        dtpNgaymode.ResetText();
                    optDonthai.Checked = Utility.Bool2Bool(objChandoanSankhoa.CdDonthai);
                    optDathai.Checked = Utility.Bool2Bool(objChandoanSankhoa.CdDathai);
                    optTrai.Checked = Utility.Bool2Bool(objChandoanSankhoa.CdTrai);
                    optGai.Checked = Utility.Bool2Bool(objChandoanSankhoa.CdGai);
                    optSong.Checked = Utility.Bool2Bool(objChandoanSankhoa.CdSong);
                    optChet.Checked = Utility.Bool2Bool(objChandoanSankhoa.CdChet);
                }
                //else
                //    lnkChandoanSankhoa_UserName.Visible = false;
                //Quá trình thai kỳ
                if (objQttk != null)
                {
                    //lnkQuatrinhthaiky_UserName.Visible = objQttk.NguoiTao != globalVariables.UserName;
                    //pnlChanDoanSankhoa.Enabled = objQttk.NguoiTao == globalVariables.UserName;
                    //if (lnkChandoanSankhoa_UserName.Visible)
                    //{
                    //    lnkQuatrinhthaiky_UserName.Text = string.Format("Dữ liệu này được tạo bởi người dùng: {0}", objQttk.NguoiTao);
                    //}

                    if (objQttk.Kinhcuoitungay.HasValue)
                        dtpKinhcuoitungay.Value = objQttk.Kinhcuoitungay.Value;
                    else
                        dtpKinhcuoitungay.ResetText();
                    if (objQttk.Kinhcuoidenngay.HasValue)
                        dtpKinhcuoidenngay.Value = objQttk.Kinhcuoidenngay.Value;
                    else
                        dtpKinhcuoidenngay.ResetText();
                    txtKhamthaitai.Text = Utility.sDbnull(objQttk.Khamthaitai);
                    chkTiemphonguonvan.Checked = Utility.Bool2Bool(objQttk.TiemphongUonvan);
                    txtDuoctiemphonguonvanSolan.Text = Utility.sDbnull(objQttk.TiemphongUonvanSolan);
                    if (objQttk.Batdauchuyenda.HasValue)
                        dtpBatdauchuyendatu.Value = objQttk.Batdauchuyenda.Value;
                    txtDauhieuLucdau.Text = Utility.sDbnull(objQttk.Dauhieulucdau);
                    txtBienchuyen.Text = Utility.sDbnull(objQttk.Bienchuyen);
                }
                //else
                //    lnkQuatrinhthaiky_UserName.Visible = false;
                //Tiền sử sản phụ khoa
                if (objTspk != null)
                {
                    //lnkTiensuphukhoa.Visible = objTspk.NguoiTao != globalVariables.UserName;
                    //pnlChanDoanSankhoa.Enabled = objTspk.NguoiTao == globalVariables.UserName;
                    //if (lnkChandoanSankhoa_UserName.Visible)
                    //{
                    //    lnkTiensuphukhoa.Text = string.Format("Dữ liệu này được tạo bởi người dùng: {0}", objTspk.NguoiTao);
                    //}
                    dtpBatdauthaykinhnam.Text = Utility.sDbnull(objTspk.BaTsspkBatdauthaykinhNam);
                    nmrBatdauthaykinhtuoi.Text = Utility.sDbnull(objTspk.BaTsspkBatdauthaykinhTuoi);
                    txt_tinhchatkinhnguyet.Text = Utility.sDbnull(objTspk.BaTsspkTinhchatkinhnguyet);
                    txt_chuky.Text = Utility.sDbnull(objTspk.BaTsspkChukykinhnguyet);
                    txt_songaythaykinh.Text = Utility.sDbnull(objTspk.BaTsspkSongaythaykinh);
                    txt_luongkinh.Text = Utility.sDbnull(objTspk.BaTsspkLuongkinh);
                    if (objTspk.BaTsspkKinhlancuoingay.HasValue)
                        dtpKinhlancuoingay.Value = objTspk.BaTsspkKinhlancuoingay.Value;
                    else
                        dtpKinhlancuoingay.ResetText();
                    chkCodaubung.Checked = Utility.Bool2Bool(objTspk.BaTsspkCodaubung);
                    chk_thoigiantruoc.Checked = Utility.Bool2Bool(objTspk.BaTsspkThoigianTruoc);
                    chk_thoigiantrong.Checked = Utility.Bool2Bool(objTspk.BaTsspkThoigianTrong);
                    chk_thoigiansau.Checked = Utility.Bool2Bool(objTspk.BaTsspkThoigianSau);
                    dtpLaychongNam.Text = Utility.sDbnull(objTspk.BaTsspkLaychongNam);
                    nmrLaychongTuoi.Text = Utility.sDbnull(objTspk.BaTsspkLaychongTuoi);
                    dtpHetKinhNam.Text = Utility.sDbnull(objTspk.BaTsspkHetkinhnam);
                    nmrHetkinhTuoi.Text = Utility.sDbnull(objTspk.BaTsspkHetkinhtuoi);
                    txt_benhphukhoadadieutri.Text = Utility.sDbnull(objTspk.BaTsspkBenhphukhoadadieutri);
                    //txt_para.Text = Utility.sDbnull(objTspk.BaTsspkPara);
                }
                //}
                //else
                //    lnkTiensuphukhoa.Visible = false;
            }
            catch (Exception)
            {


            }
            finally
            {
               // ModifyCommmands();
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
       // VKcbLuotkham objBenhnhan = null;
        KcbLuotkham objLuotkham = null;
        KcbDanhsachBenhnhan objBenhnhan = null;
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
            cmdKhoitaoBA.Enabled = objEmrBa == null;
            cmdSave.Enabled = objEmrBa != null && objEmrBa.TrangThai <= 1;
            //cmdSave.Tag = objEmrBa!=null &&  objEmrBa.TrangThai == 1 ? "HUY" : "LUU";
            //cmdSave.Text = objEmrBa != null && objEmrBa.TrangThai == 1 ? "2. Hủy Lưu" : "2. Lưu BA (Ctrl+S)";
            cmdKetthucBA.Enabled = objEmrBa != null && objEmrBa.TrangThai >= 1;
            cmdKetthucBA.Tag = objEmrBa != null && objEmrBa.TrangThai == 2 ? "HUY" : "HOANTAT";
            cmdKetthucBA.Text = objEmrBa != null && objEmrBa.TrangThai == 2 ? "3. Làm lại BA" : "3. Hoàn tất BA";
        }

        private void txtMaLanKham_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    objLuotkham.MaLuotkham = THU_VIEN_CHUNG.SinhMaHoSoKhiTimKiem(objLuotkham.MaLuotkham);
            //    if (!IsValidData()) return;
            //    FillBenhAnByPatientCode();
            //}
        }
       
        private DataTable getChitietCLS()
        {
            int status = 0;
            DataTable temdt = SPs.ClsKetQuaXetNghiem(-1,"",objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, 1, status).GetDataSet().Tables[0];

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
                    ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
                    ucThongtinnguoibenh_emr_basic1.txtMaluotkham.SelectAll();
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
                                emrdoc.DeleteDocument_WithoutTransaction(objEmrBa.IdBa, new List<string>() { Utility.LayMaBA(objEmrBa.LoaiBa), "BENHAN_BIA", "BENHAN_TO1", "BENHAN_TO2", "BENHAN_TO3", "BENHAN_TO4" }, "");
                                Utility.Log("frm_BenhAn_SanKhoa", globalVariables.UserName, string.Format("Xóa bệnh án id={0}, loại BA={1}, mã BA={2} của người bệnh id ={3}, mã lần khám {4} thành công",objEmrBa.IdBa,objEmrBa.LoaiBa,objEmrBa.MaBa,objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham), newaction.Delete, "UI");
                            }
                            Scope.Complete();
                        }
                       
                            Utility.ShowMsg("Bạn xóa bệnh án thành công", "Thông báo");
                            ucThongtinnguoibenh_emr_basic1.Refresh();
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

            DataTable sub_dtData = new KCB_THAMKHAM().NoitruTimkiemlichsuBuonggiuong(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, "-1",-1);
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
           // objBenhnhan = null;
            objLuotkham = null;
            m_enAct = action.Insert;
            ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
            ucThongtinnguoibenh_emr_basic1.txtMaluotkham.SelectAll();
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
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, dt_tssk, dtPhieuPttt, 0, false);
        }

        private void mnuInTomtatBA_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Chưa có thông tin người bệnh để thực hiện thao tác in tóm tắt bệnh án");
                return;
            }
            EmrTomtatBa objTKBA =new Select().From(EmrTomtatBa.Schema)
                .Where(EmrTomtatBa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrTomtatBa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteSingle<EmrTomtatBa>();
            if (objTKBA == null || objTKBA.Id <= 0)
            {
                Utility.ShowMsg("Bạn cần tạo Tóm tắt hồ sơ bệnh án trước khi thực hiện in");
                return;
            }
            clsInBA.InTomTatBA(objTKBA);
        }

        private void mnuInTo1_Click(object sender, EventArgs e)
        {
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, dt_tssk, dtPhieuPttt, 1, false);
        }

        private void mnuInTo2_Click(object sender, EventArgs e)
        {
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, dt_tssk, dtPhieuPttt, 2, false);
        }

        private void mnuInTo3_Click(object sender, EventArgs e)
        {
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, dt_tssk, dtPhieuPttt, 3, false);
        }

        private void mnuInTo4_Click(object sender, EventArgs e)
        {
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, dt_tssk, dtPhieuPttt, 4, false);
        }
        private void mnuInBA_Click(object sender, EventArgs e)
        {
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, dt_tssk, dtPhieuPttt, 100, false);
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
        EmrTomtatBa objTKBA;
        void FillTongketBenhAn()
        {
            try
            {
                objTKBA=  new Select().From(EmrTomtatBa.Schema)
                    .Where(EmrTomtatBa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(EmrTomtatBa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .ExecuteSingle<EmrTomtatBa>();
                if (objTKBA != null)
                {
                    dtpNgayTKBA.Value = objTKBA.NgayTtba.Value;
                    txtBSDieuTri.SetId(objTKBA.IdBacsiDieutri);
                    txtTKBAQuaTrinhBenhLy.Text = objTKBA.QuatrinhbenhlyDienbienlamsang;
                    txtTKBATTomTatKetQua.Text = objTKBA.TomtatKqcls;
                    txtTKBAPhuongPhapDieuTri.Text = objTKBA.PhuongphapDieutri;
                    txtTKBATinhTrangRaVien.Text = objTKBA.TinhtrangRavienMota;
                    txtTKBAHuongDieuTri.Text = objTKBA.HuongDieutri;

                    txtNguoiGiaoHoSo.SetId(objTKBA.IdNguoigiaoHoso);
                    txtNguoiNhanHoSo.SetId(objTKBA.IdNguoinhanHoso);

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
            LuuBA(0);
        }

        private void cmdLaythongtinKCB_Click(object sender, EventArgs e)
        {

        }

        private void cmdDiungkhac_Click(object sender, EventArgs e)
        {
            
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

        private void chkEditPKB_CheckedChanged(object sender, EventArgs e)
        {
            //txtBenhAnToanThan.ReadOnly = txtBenhAnTuanHoan.ReadOnly = txtBenhAnHoHap.ReadOnly
            //   = txtBenhAnTieuHoa.ReadOnly = txtBenhAnThanTietNieuSinhDuc.ReadOnly = txtBenhAnThanKinh.ReadOnly
            //   = txtBenhAnCoXuongKhop.ReadOnly = txtBenhAnTaiMuiHong.ReadOnly = txtBenhAnMat.ReadOnly
            //   = txtBenhAnNoiTiet.ReadOnly = !chkEditPKB.Checked && !chkEditPKB.Visible;
        }

        private void chkEditTKBA_CheckedChanged(object sender, EventArgs e)
        {
            //txtTKBAQuaTrinhBenhLy.ReadOnly = txtTKBATTomTatKetQua.ReadOnly
            //  = txtTKBAPhuongPhapDieuTri.ReadOnly = txtTKBATinhTrangRaVien.ReadOnly
            //  = txtTKBAHuongDieuTri.ReadOnly = txtB_Xquang.ReadOnly = txtB_CTScanner.ReadOnly = txtB_SieuAm.ReadOnly
            //  = txtB_XetNghiem.ReadOnly = txtB_Khac.ReadOnly = txtNguoiGiaoHoSo.ReadOnly = txtNguoiNhanHoSo.ReadOnly = txtBSDieuTri.ReadOnly
            //  = !chkEditTKBA.Checked && !chkEditTKBA.Visible;
        }

        private void mnuSent2EMR_Click(object sender, EventArgs e)
        {
            try
            {
                if(objEmrBa!=null)
                {
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_BIA, "BA05_BASANKHOA_BIA", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_SANKHOA);
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO1, "BA05_BASANKHOA_TO1", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_SANKHOA);
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO2, "BA05_BASANKHOA_TO2", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_SANKHOA);
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO3, "BA05_BASANKHOA_TO3", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_SANKHOA);
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO4, "BA05_BASANKHOA_TO4", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_SANKHOA);
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BA_SANKHOA, "BA05_BASANKHOA", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_SANKHOA);
                    emrdoc.Save();
                    Utility.ShowMsg("Đẩy dữ liệu vào EMR thành công");
                }    
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

       

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void uiGroupBox5_Click(object sender, EventArgs e)
        {

        }

        private void cmdPhieutheodoitaibuongde_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Cần nhập thông tin người bệnh trước khi thực hiện thêm thông tin phiếu khám sản khoa");
                return;
            }
            frm_theodoitaibuongde _theodoitaibuongde = new frm_theodoitaibuongde(objLuotkham, null);
            _theodoitaibuongde.ShowDialog();
            FillPhieutheodoitaiBuongde();
        }

        private void cmdKhamsankhoa_2_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Cần nhập thông tin người bệnh trước khi thực hiện thêm thông tin phiếu khám sản khoa");
                return;
            }
            frm_khamSanKhoa _khamSanKhoa = new frm_khamSanKhoa(objLuotkham, objBenhnhan);
            _khamSanKhoa.ShowDialog();
            FillPhieuKhamSanKhoa();
        }

        private void cmdKhamsankhoa_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Cần nhập thông tin người bệnh trước khi thực hiện thêm thông tin phiếu khám sản khoa");
                return;
            }
            frm_khamSanKhoa _khamSanKhoa = new frm_khamSanKhoa(objLuotkham, objBenhnhan);
            _khamSanKhoa.ShowDialog();
            FillPhieuKhamSanKhoa();
        }

        private void chkPhuongphapkhauvaloaichi_CheckedChanged(object sender, EventArgs e)
        {
            txtPhuongphapkhauvaloaichi.Enabled = nmrSomuikhau.Enabled = chkPhuongphapkhauvaloaichi.Checked;
        }

        bool isAllowChangedNgayTuoi = false;
        private void dtpBatdauthaykinhnam_ValueChanged(object sender, EventArgs e)
        {
            if (!isAllowChangedNgayTuoi || objBenhnhan == null) return;
            isAllowChangedNgayTuoi = false;
            nmrBatdauthaykinhtuoi.Text = Utility.sDbnull(dtpBatdauthaykinhnam.Value.Year - Utility.Int32Dbnull(objBenhnhan.NamSinh, 0));
            isAllowChangedNgayTuoi = true;
        }

        private void nmrBatdauthaykinhtuoi_ValueChanged(object sender, EventArgs e)
        {
            if (!isAllowChangedNgayTuoi || objBenhnhan == null) return;
            isAllowChangedNgayTuoi = false;
            dtpBatdauthaykinhnam.Text = Utility.sDbnull(Utility.Int32Dbnull(objBenhnhan.NamSinh, 0) + Utility.Int32Dbnull(nmrBatdauthaykinhtuoi.Value));
            isAllowChangedNgayTuoi = true;
        }

        private void dtpKinhlancuoingay_ValueChanged(object sender, EventArgs e)
        {
            if (!isAllowChangedNgayTuoi || objBenhnhan == null) return;
            isAllowChangedNgayTuoi = false;
            dtpBatdauthaykinhnam.Text = Utility.sDbnull(Utility.Int32Dbnull(objBenhnhan.NamSinh, 0) + Utility.Int32Dbnull(nmrBatdauthaykinhtuoi.Value));
            isAllowChangedNgayTuoi = true;
        }

        private void dtpLaychongNam_ValueChanged(object sender, EventArgs e)
        {
            if (!isAllowChangedNgayTuoi || objBenhnhan == null) return;
            isAllowChangedNgayTuoi = false;
            nmrLaychongTuoi.Text = Utility.sDbnull(dtpLaychongNam.Value.Year - Utility.Int32Dbnull(objBenhnhan.NamSinh, 0));
            isAllowChangedNgayTuoi = true;
        }

        private void nmrLaychongTuoi_ValueChanged(object sender, EventArgs e)
        {
            if (!isAllowChangedNgayTuoi || objBenhnhan == null) return;
            isAllowChangedNgayTuoi = false;
            dtpLaychongNam.Text = Utility.sDbnull(Utility.Int32Dbnull(objBenhnhan.NamSinh, 0) + Utility.Int32Dbnull(nmrLaychongTuoi.Value));
            isAllowChangedNgayTuoi = true;
        }

        private void dtpHetKinhNam_ValueChanged(object sender, EventArgs e)
        {
            if (!isAllowChangedNgayTuoi || objBenhnhan == null) return;
            isAllowChangedNgayTuoi = false;
            nmrHetkinhTuoi.Text = Utility.sDbnull(dtpHetKinhNam.Value.Year - Utility.Int32Dbnull(objBenhnhan.NamSinh, 0));
            isAllowChangedNgayTuoi = true;
        }

        private void nmrHetkinhTuoi_ValueChanged(object sender, EventArgs e)
        {
            if (!isAllowChangedNgayTuoi || objBenhnhan == null) return;
            isAllowChangedNgayTuoi = false;
            dtpHetKinhNam.Text = Utility.sDbnull(Utility.Int32Dbnull(objBenhnhan.NamSinh, 0) + Utility.Int32Dbnull(nmrHetkinhTuoi.Value));
            isAllowChangedNgayTuoi = true;
        }

        private void cmdSave_Click_1(object sender, EventArgs e)
        {
            LuuBA(1);
        }

        private void label22_Click(object sender, EventArgs e)
        {
            txtCDKKBCapCuu.Text = Utility.Get_ChanDoan_KKB_CapCuu(objLuotkham);
            txtCDMaKKBCapCuu.Text = Utility.sDbnull(objLuotkham.MabenhChinh, string.Empty);
        }

        private void label23_Click(object sender, EventArgs e)
        {
            Utility.GetChanDoanNoitru(objLuotkham, ref ICD_Khoa_NoITru, ref Name_Khoa_NoITru);
            txtCDKhiVaoDieuTri.Text = Name_Khoa_NoITru;
            txtCDMaKhiVaoDieuTri.Text = ICD_Khoa_NoITru;
        }

        private void cmd_them_tiensusanphukhoa_Click(object sender, EventArgs e)
        {
            frm_ThemtiensuSankhoa f = new frm_ThemtiensuSankhoa(objLuotkham, null);
            f.dt_tssk = dt_tssk;
            if (f.ShowDialog() == DialogResult.OK)
            {
                DataRow newdr = dt_tssk.NewRow();
                Utility.FromObjectToDatarow(f.tssk, ref newdr);
                dt_tssk.Rows.Add(newdr);
            }
        }
    }
}
