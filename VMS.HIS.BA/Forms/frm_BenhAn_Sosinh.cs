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
using VNS.HIS.UI.DANHMUC;

namespace VMS.HIS.UI.EMR
{
    public partial class frm_BenhAn_Sosinh : Form
    {
        public delegate void OnCreated(long id,string ma_ba, action m_enAct);
        public event OnCreated _OnCreated;
        string lstLoaiBA = "";
        DataTable dt_ThongtinNguoibenh = new DataTable();
        public frm_BenhAn_Sosinh(string lstLoaiBA)
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
            chkttrvTrong48GioVaoVien.CheckedChanged += chkttrvSau24Gio_CheckedChanged;
            chkttrvTrong72hVaovien.CheckedChanged += chkttrvKhac_CheckedChanged;
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

            PhanquyenTinhnang();
            txtChieuCao.Leave += txtChieucao_Leave;
            txtCanNang.Leave += txtCannang_Leave;
            InitEvents();
            SetLeaveEvents(txtBenhAnQuaTrinhBenhLy);
        }
        void SetLeaveEvents(params VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung[] autoTextboxes)
        {
            foreach (var autoTxt in autoTextboxes)
            {
                autoTxt.Leave += AutoTxt_Leave;
            }
        }
        public static void RemoveTrailingEmptyLine( TextBox tb)
        {
            var lines = tb.Lines;
            int last = lines.Length - 1;

            if (last >= 0 && string.IsNullOrWhiteSpace(lines[last]))
            {
                tb.Lines = lines.Take(last).ToArray();
            }
        }
        private void AutoTxt_Leave(object sender, EventArgs e)
        {
            RemoveTrailingEmptyLine(sender as VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung);
        }

        void InitEvents()
        {
            txtBenhAnQuaTrinhBenhLy._OnShowDataV1 += _OnShowDataV1;
            txt_maluotkham_me.KeyDown += Txt_maluotkham_me_KeyDown;
        }

        private void Txt_maluotkham_me_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string ma_lankham_me = Utility.AutoFullPatientCode(txt_maluotkham_me.Text);
                KcbLuotkham objMe = Utility.getKcbLuotkham(ma_lankham_me);
                if (objMe != null)
                {
                    KcbDanhsachBenhnhan objMeBN = Utility.getKcbDanhsachBenhnhan(objMe);
                    if(objMeBN != null)
                    {
                        txt_hoten_me.Text = objMeBN.TenBenhnhan;
                        txt_nghenghiep_me.SetCode(objMeBN.NgheNghiep);
                        dtp_ngaysinh_me.Value = objMeBN.NgaySinh.Value;
                        txtNhommau.SetCode(objMeBN.NhomMau);
                    }    
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void _OnShowDataV1(VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung obj)
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(obj.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = obj.myCode;
                obj.Init();
                obj.SetCode(oldCode);
                obj.Focus();
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

        private void Txt_chandoan_sauphauthuat__OnEnterMe()
        {
            throw new NotImplementedException();
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
                objBenhnhan = Utility.getKcbDanhsachBenhnhan(ucThongtinnguoibenh_emr_basic1.objLuotkham);
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
                //if(objLuotkham!=null && objLuotkham.Tuoi>=30 && objLuotkham.LoaiTuoi==2)//0=năm;1=tháng;2=tuần
                //{

                //}    
                dt_ThongtinNguoibenh = ucThongtinnguoibenh_emr_basic1.dt_ThongtinNguoibenh;
                objEmrBa = null;
                objPhieuKhamSoSinh = null;
                objLuotkham = ucThongtinnguoibenh_emr_basic1.objLuotkham;
               
                objBenhnhan = Utility.getKcbDanhsachBenhnhan(objLuotkham); 
                ClearControl();
                if(!KiemTraBenhAn())
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
            if(objEmrBa==null ||(objEmrBa!=null && this.lstLoaiBA.Contains(objEmrBa.LoaiBa)))
            {
                return true;
            }
            else if (objEmrBa != null && !this.lstLoaiBA.Contains( objEmrBa.LoaiBa ))
            {
                Utility.ShowMsg(string.Format("Người bệnh {0} đã có {1} nên không thể tạo Bệnh án Sơ sinh. Vui lòng kiểm tra lại",ucThongtinnguoibenh_emr_basic1.txtTenBN.Text, Utility.GetTenLoaiBenhAn(objEmrBa.LoaiBa)));
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
                chkttrvTrong48GioVaoVien.Checked = false;
                chkttrvTrong72hVaovien.Checked = false;
            }
        }

        private void chkttrvDoTaiBien_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvDoTaiBien.Checked == true)
            {
                chkttrvDoBenh.Checked = false;
                chkttrvTrong24GioVaoVien.Checked = false;

                chkttrvTrong48GioVaoVien.Checked = false;
                chkttrvTrong72hVaovien.Checked = false;
            }
        }

        private void chkttrvSau24Gio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvTrong48GioVaoVien.Checked == true)
            {
                chkttrvDoBenh.Checked = false;
                chkttrvTrong24GioVaoVien.Checked = false;
                chkttrvDoTaiBien.Checked = false;

                chkttrvTrong72hVaovien.Checked = false;
            }
        }

        private void chkttrvKhac_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvTrong72hVaovien.Checked == true)
            {
                chkttrvDoBenh.Checked = false;
                chkttrvTrong24GioVaoVien.Checked = false;
                chkttrvDoTaiBien.Checked = false;
                chkttrvTrong48GioVaoVien.Checked = false;

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
                chkttrvTrong48GioVaoVien.Checked = false;
                chkttrvTrong72hVaovien.Checked = false;
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
                chkttrvTrong72hVaovien.Checked = Utility.Bool2Bool(objPhieuRavien.TuvongDokhac);
                chkttrvTrong24GioVaoVien.Checked = Utility.Bool2Bool(objPhieuRavien.TuvongTrong24gio);
                chkttrvTrong48GioVaoVien.Checked = Utility.Bool2Bool(objPhieuRavien.TuvongSau24h);

                txtTTRVNguyenNhanChinhTuVong.Text = Utility.sDbnull(objPhieuRavien.TuvongNguyennhanchinh);
                chkTTRVChandoanGiaiphauTuthi.Checked = Utility.Bool2Bool(objPhieuRavien.TuvongChandoangiaiphaututhi);
                txtTTRVChandoanGiaiphauTuthi.Text = Utility.sDbnull(objPhieuRavien.TuvongChandoangiaiphaututhiMota);
                chk_taibien.Checked = Utility.Bool2Bool(objPhieuRavien.Taibien);
                chk_bienchung.Checked = Utility.Bool2Bool(objPhieuRavien.Bienchung);
            }
            txtCDRavienTenBenhChinh.Text = chandoan;
            txtCDRavienMaBenhChinh.Text = Utility.sDbnull(mabenh);
            txtCDRavienTenBenhKemTheo.Text = chandoanphu;
            txtCDRavienMaBenhKemTheo.Text = mabenhphu;

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
           
            chk_taibien.Checked = false;
            chk_bienchung.Checked = false;
            chk_phauthuat_sausinh.Checked = false;
           

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
            chkttrvTrong48GioVaoVien.Checked = false;
            chkttrvTrong72hVaovien.Checked = false;
            txtTTRVNguyenNhanChinhTuVong.Clear();
            chkTTRVChandoanGiaiphauTuthi.Checked = false;
            txtTTRVChandoanGiaiphauTuthi.Clear();
            txtBenhAnLyDoNhapVien.SetDefaultItem();
            txtBenhAnVaoNgayThu.Clear();
            txtBenhAnQuaTrinhBenhLy.Clear();
        
            txt_tinhtrang_toanthan.Clear();
            txtMach.Clear();
            txtNhietDo.Clear();
            txtha.Clear();
            txtNhipTho.Clear();
            txtCanNang.Clear();
            txtChieuCao.Clear();
            txtBMI.Clear();
          
            txt_coquan_sinhduc_ngoai.Clear();
            txt_thankinh_phanxa.Clear();
            txt_xuongkhop.Clear();
            txt_thankinh_truonglucco.Clear();
          
            txtBenhAnCacXetNghiem.Clear();
            txtBenhAnTomTatBenhAn.Clear();
          
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
                Utility.SetMsg(lblMsg, "Cần chọn người mẹ trước khi làm Bệnh án sơ sinh. Vui lòng kiểm tra lại", true);
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
            if (trangthai >= 1)
            {
                if (Utility.sDbnull(txt_hoten_me.Text) == "")
                {
                    uiTabBA.SelectedTab = tabpageTo1;
                    Utility.SetMsg(lblMsg, "Bạn cần nhập họ tên Mẹ", true);
                    txt_hoten_me.Focus();
                    return false;
                }
                if (dtp_ngaysinh_me.Text == "")
                {
                    Utility.SetMsg(lblMsg, "Bạn phải nhập ngày sinh của mẹ", true);
                    dtp_ngaysinh_me.Focus();
                    return false;
                }
                if (dtp_ngaysinh_me.Value >= objBenhnhan.NgaySinh.Value)
                {
                    Utility.SetMsg(lblMsg, "Ngày sinh mẹ phải trước ngày sinh bé", true);
                    dtp_ngaysinh_me.Focus();
                    return false;
                }
                if (Utility.sDbnull(txt_hoten_bo.Text) == "")
                {
                    uiTabBA.SelectedTab = tabpageTo1;
                    Utility.SetMsg(lblMsg, "Bạn cần nhập họ tên Bố", true);
                    txt_hoten_bo.Focus();
                    return false;
                }

                if (dtp_ngaysinh_bo.Text == "")
                {
                    Utility.SetMsg(lblMsg, "Bạn phải nhập ngày sinh của bố", true);
                    dtp_ngaysinh_bo.Focus();
                    return false;
                }
                if (dtp_ngaysinh_bo.Value >= objBenhnhan.NgaySinh.Value)
                {
                    Utility.SetMsg(lblMsg, "Ngày sinh bố phải trước ngày sinh bé", true);
                    dtp_ngaysinh_bo.Focus();
                    return false;
                }
                
            }

            if (trangthai == 2)
            {
                if (dtp_oivo_luc.Value <= dtp_deluc.Value)
                {
                    Utility.SetMsg(lblMsg, "Thời gian đẻ phải > thời gian vỡ ối", true);
                    dtp_deluc.Focus();
                    return false;
                }

                //if (dtp_oivo_luc.Value <= dtp_deluc.Value)
                //{
                //    Utility.SetMsg(lblMsg, "Thời gian đẻ phải > thời gian vỡ ối", true);
                //    dtp_deluc.Focus();
                //    return false;
                //}
                if (Utility.Int32Dbnull(txtBacsiKham.MyID, -1) <= 0)
                {
                    uiTabBA.SelectedTab = tabpageTo2;
                    Utility.SetMsg(lblMsg, "Bạn cần chọn Bác sĩ khám từ danh mục Bác sĩ trong hệ thống", true);
                    txtBacsiKham.Focus();
                    return false;
                }
                if (Utility.Int32Dbnull(txtNguoiGiaoHoSo.MyID, -1) <= 0)
                {
                    uiTabBA.SelectedTab = tabpageTo4;
                    Utility.SetMsg(lblMsg, "Bạn cần chọn Người giao hồ sơ trong danh mục hệ thống", true);
                    txtNguoiGiaoHoSo.Focus();
                    return false;
                }
                if (Utility.Int32Dbnull(txtNguoiNhanHoSo.MyID, -1) <= 0)
                {
                    uiTabBA.SelectedTab = tabpageTo4;
                    Utility.SetMsg(lblMsg, "Bạn cần chọn Người nhận hồ sơ trong danh mục hệ thống", true);
                    txtNguoiNhanHoSo.Focus();
                    return false;
                }
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
        int num = 0;
        private void cmdKetthucBA_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmdKetthucBA.Tag.ToString() == "HUY" || objEmrBa.TrangThai == 2)
                {
                    num = new Update(EmrBa.Schema)
                        .Set(EmrBa.Columns.TrangThai).EqualTo(1)
                        .Where(EmrBa.Columns.IdBa).IsEqualTo(objEmrBa.IdBa)
                        .And(EmrBa.Columns.LoaiBa).IsEqualTo(objEmrBa.LoaiBa)
                        .Execute();
                    if (num > 0) objEmrBa.TrangThai = 1;
                    ModifyCommand();
                }
                else
                    LuuBA(2);
            }
            catch (Exception ex)
            {

            }
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trangthai">0= khởi tạo;1= Lưu;2= Kết thúc</param>
        void LuuBA(int trangthai)
        {
            try
            {
                isSuccess = false;
                if (!IsValidData(trangthai)) return;
                TaoPhieuKhamSoSinh();
                objEmrBa = TaoEmrBa();
                if (objEmrBa.IdBa > 0)
                {
                    if (!Utility.isValidSignStatus4UpdateDelete(objLuotkham, objEmrBa.IdBa, Loaiphieu_HIS.BA_PHUKHOA, "Bệnh án Sơ sinh"))
                        return;
                }
                objEmrBa.TrangThai =Utility.ByteDbnull( trangthai);
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
                            
                            objPhieuKhamSoSinh.Save();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin phiếu khám Sơ sinh tại chức năng EMR cho người bệnh: {0}-{1} thành công", objEmrBa.MaLuotkham, objEmrBa.TenBenhnhan), objPhieuKhamSoSinh.IsNew ? newaction.Insert : newaction.Update, "EMR");
                          
                        }
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_BIA, "BA06_BASOSINH_BIA", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_SOSINH);
                        emrdoc.Save();
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO1, "BA06_BASOSINH_TO1", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_SOSINH);
                        emrdoc.Save();
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO2, "BA06_BASOSINH_TO2", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_SOSINH);
                        emrdoc.Save();
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO3, "BA06_BASOSINH_TO3", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_SOSINH);
                        emrdoc.Save();
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO4, "BA06_BASOSINH_TO4", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_SOSINH);
                        emrdoc.Save();
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BA_PHUKHOA, "BA06_BASOSINH", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_SOSINH);
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
                dtDataBA = SPs.EmrBaLaythongtinIn(-1, "", LoaiBA.BA_PHUKHOA, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                _isSuccess = true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                ModifyCommand();
               
            }
        }
        void TaoPhieuKhamSoSinh()
        {
            objPhieuKhamSoSinh = new Select().From(EmrPhieukhamSosinh.Schema)
             .Where(EmrPhieukhamSosinh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
             .And(EmrPhieukhamSosinh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
             .ExecuteSingle<EmrPhieukhamSosinh>();
            if (objPhieuKhamSoSinh != null && objPhieuKhamSoSinh.Id > 0)
            {
                objPhieuKhamSoSinh.MarkOld();
                objPhieuKhamSoSinh.NguoiSua = globalVariables.UserName;
                objPhieuKhamSoSinh.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
            }
            else
            {
                objPhieuKhamSoSinh = new EmrPhieukhamSosinh();
                objPhieuKhamSoSinh.IsNew = true;
                objPhieuKhamSoSinh.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                objPhieuKhamSoSinh.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                objPhieuKhamSoSinh.NgayKham = dtpNgayKham.Value.Date;
                objPhieuKhamSoSinh.NguoiTao = globalVariables.UserName;
                objPhieuKhamSoSinh.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
            }
            objPhieuKhamSoSinh.HotenBe = Utility.sDbnull(objBenhnhan.TenBenhnhan);
            objPhieuKhamSoSinh.GioitinhNam = objBenhnhan.IdGioitinh==0;
            objPhieuKhamSoSinh.GioitinhNu = objBenhnhan.IdGioitinh == 1;
           
            objPhieuKhamSoSinh.HotenBo = Utility.sDbnull(txt_hoten_bo.Text);
            objPhieuKhamSoSinh.NghenghiepBo = txt_nghenghiep.Text;
            objPhieuKhamSoSinh.NgaysinhBo = dtp_ngaysinh_bo.Value;
            objPhieuKhamSoSinh.MaDantoc = txtDantoc.myCode;
            objPhieuKhamSoSinh.TenDantoc = txtDantoc.Text;
            objPhieuKhamSoSinh.NgoaiKieu = Utility.Bool2byte(chk_ngoaikieu.Checked);

            objPhieuKhamSoSinh.HotenMe = Utility.sDbnull(txt_hoten_me.Text);
            objPhieuKhamSoSinh.NgaysinhMe = dtp_ngaysinh_me.Value;
            objPhieuKhamSoSinh.NghenghiepMe = Utility.sDbnull(txt_nghenghiep_me.Text);
            objPhieuKhamSoSinh.NhommauMe = txtNhommau.Text;
            objPhieuKhamSoSinh.TienthaiPara = txt_para.Text;
            objPhieuKhamSoSinh.SolanDe = Utility.ByteDbnull(nmr_solan_de.Value);
            //Hỏi bệnh
            objPhieuKhamSoSinh.IdBacsi = Utility.Int16Dbnull(txtBacsiKham.MyID, -1);
            objPhieuKhamSoSinh.OivoLuc = dtp_oivo_luc.Value;

            objPhieuKhamSoSinh.NuocOiTrong = opt_nuoc_oi_trong.Checked;
            objPhieuKhamSoSinh.NuocOiXanhban = opt_nuoc_oi_xanhban.Checked;
            objPhieuKhamSoSinh.NuocOiLanmau = opt_nuoc_oi_lanmau.Checked;
            objPhieuKhamSoSinh.NuocOiMota = Utility.sDbnull(txt_nuoc_oi_mota.Text);

            objPhieuKhamSoSinh.Dethuong = opt_dethuong.Checked;
            objPhieuKhamSoSinh.Canthiep = opt_canthiep.Checked;
            objPhieuKhamSoSinh.Deluc = dtp_deluc.Value;

            //Tiền sử sản Sơ sinh
            objPhieuKhamSoSinh.Lydocanthiep = opt_canthiep.Checked? Utility.sDbnull(txt_lydocanthiep.Text):"";
            //Tình trạng trẻ sơ sinh
            objPhieuKhamSoSinh.Khocnga = opt_khocnga.Checked;
            objPhieuKhamSoSinh.Nga = opt_nga.Checked;
            objPhieuKhamSoSinh.Kha = opt_kha.Checked;
            objPhieuKhamSoSinh.HotenNguoiDode = Utility.sDbnull(txt_hoten_nguoi_dode.Text);
            objPhieuKhamSoSinh.ChucdanhNguoiDode = Utility.sDbnull(txt_chucdanh_nguoi_dode.Text);

            objPhieuKhamSoSinh.Apgar1phut= Utility.sDbnull(txt1phut.Text);
            objPhieuKhamSoSinh.Apgar5phut = Utility.sDbnull(txt5phut.Text);
            objPhieuKhamSoSinh.Apgar10phut = Utility.sDbnull(txt10phut.Text);

            objPhieuKhamSoSinh.TresosinhCannang = Utility.Int16Dbnull(txtCanNang.Text);
            objPhieuKhamSoSinh.VongDau = Utility.sDbnull(txt_vongdau.Text);
            objPhieuKhamSoSinh.ChieuDai = Utility.sDbnull(txtChieuCao.Text);
            objPhieuKhamSoSinh.TinhtrangDinhduongSausinh = Utility.sDbnull(txt_tinhtrang_dinhduong_sausinh.Text);
            //Phương pháp hồi sinh sau đẻ
            objPhieuKhamSoSinh.HutDich = chk_hut_dich.Checked;
            objPhieuKhamSoSinh.XoabopTim = chk_xoabop_tim.Checked;
            objPhieuKhamSoSinh.ThoOxy = chk_tho_oxy.Checked;
            objPhieuKhamSoSinh.DatNoikhiquan = chk_dat_noikhiquan.Checked;
            objPhieuKhamSoSinh.BopBong = chk_bop_bong.Checked;
            objPhieuKhamSoSinh.PhuongphapKhac = chk_phuongphap_khac.Checked;
            //Khám bệnh
            objPhieuKhamSoSinh.TresosinhTatbamsinh = chk_tresosinh_tatbamsinh.Checked;
            objPhieuKhamSoSinh.TresosinhCohaumon = chk_tresosinh_cohaumon.Checked;
            objPhieuKhamSoSinh.TresosinhTatbamsinhMota = Utility.sDbnull(txt_tresosinh_tatbamsinh_mota.Text);
            objPhieuKhamSoSinh.TinhhinhTresosinhKhivaokhoa = Utility.sDbnull(txt_tinhhinh_tresosinh_khivaokhoa.Text);
            objPhieuKhamSoSinh.TinhtrangToanthan = Utility.sDbnull(txt_tinhtrang_toanthan.Text);
            objPhieuKhamSoSinh.MausacdaHonghao = chk_mausacda_honghao.Checked;
            objPhieuKhamSoSinh.MausacdaXanhtai = chk_mausacda_xanhtai.Checked;
            objPhieuKhamSoSinh.MausacdaVang = chk_mausacda_vang.Checked;
            objPhieuKhamSoSinh.MausacdaTim = chk_mausacda_tim.Checked;
            objPhieuKhamSoSinh.MausacdaKhac = chk_mausacda_khac.Checked;
            //Các cơ quan khác
            objPhieuKhamSoSinh.NhipTho = Utility.sDbnull(txtNhipTho.Text);
            objPhieuKhamSoSinh.NghePhoi = Utility.sDbnull(txt_nghe_phoi.Text);
            objPhieuKhamSoSinh.ChisoSilverman = Utility.sDbnull(nmr_chiso_silverman.Text);

            objPhieuKhamSoSinh.Dieuhoa = opt_dieuhoa.Checked;
            objPhieuKhamSoSinh.XedichNhiptho = opt_xedich_nhiptho.Checked;
            objPhieuKhamSoSinh.KhongdidongNgucbung = opt_khongdidong_ngucbung.Checked;
            
            objPhieuKhamSoSinh.CokeoColiensuonKhong = opt_cokeo_coliensuon_khong.Checked;
            objPhieuKhamSoSinh.CokeoColiensuonCoit = opt_cokeo_coliensuon_coit.Checked;
            objPhieuKhamSoSinh.CokeoColiensuonThayro = opt_cokeo_coliensuon_thayro.Checked;
            
            objPhieuKhamSoSinh.CokeoMuiucKhong = opt_cokeo_muiuc_khong.Checked;
            objPhieuKhamSoSinh.CokeoMuiucCoit = opt_cokeo_muiuc_coit.Checked;
            objPhieuKhamSoSinh.CokeoMuiucThayro = opt_cokeo_muiuc_thayro.Checked;
            
            objPhieuKhamSoSinh.DapcanhMuiKhong = opt_dapcanh_mui_khong.Checked;
            objPhieuKhamSoSinh.DapcanhMuiNhe = opt_dapcanh_mui_nhe.Checked;
            objPhieuKhamSoSinh.DapcanhMuiRo = opt_dapcanh_mui_ro.Checked;
            
            objPhieuKhamSoSinh.RenriKhong = opt_renri_khong.Checked;
            objPhieuKhamSoSinh.RenriNghebangOngnghe = opt_renri_nghebang_ongnghe.Checked;
            objPhieuKhamSoSinh.RenriTaithuongNghero = opt_renri_taithuong_nghero.Checked;
            objPhieuKhamSoSinh.NhipTim = Utility.sDbnull(txtMach.Text);

            objPhieuKhamSoSinh.Bung = Utility.sDbnull(txt_bung.Text);
            objPhieuKhamSoSinh.CoquanSinhducNgoai = Utility.sDbnull(txt_coquan_sinhduc_ngoai.Text);
            objPhieuKhamSoSinh.Xuongkhop = Utility.sDbnull(txt_xuongkhop.Text);
            objPhieuKhamSoSinh.ThankinhPhanxa = Utility.sDbnull(txt_thankinh_phanxa.Text);
            objPhieuKhamSoSinh.ThankinhTruonglucco = Utility.sDbnull(txt_thankinh_truonglucco.Text);

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
            objTKBA.IdNguoinhanHoso = Utility.Int16Dbnull(txtNguoiNhanHoSo.MyID);
            objTKBA.MaNguoinhanhoso = txtNguoiNhanHoSo.MyCode;
            objTKBA.IdGiamdoc = Utility.Int16Dbnull(txtGDBV.MyID);
            objTKBA.MaGiamdoc = txtGDBV.MyCode;
            objTKBA.IdBacsiDieutri = Utility.Int16Dbnull(txtBSDieuTri.MyID);
            objTKBA.MaBacsiDieutri = txtBSDieuTri.MyCode;
            objTKBA.IdTruongkhoadieutri = Utility.Int16Dbnull(txtTruongkhoa.MyID);
            objTKBA.MaTruongkhoadieutri = txtTruongkhoa.MyCode;
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
                if (objPhieuKhamSoSinh != null)
                {
                    objEmrBa.HotenBo = objPhieuKhamSoSinh.HotenBo;
                    objEmrBa.TrinhdoVanhoaBo = "";
                    objEmrBa.NghenghiepBo = objPhieuKhamSoSinh.NghenghiepBo;
                    objEmrBa.HotenMe = objPhieuKhamSoSinh.HotenMe;
                    objEmrBa.TrinhdoVanhoaMe = "";
                    objEmrBa.NghenghiepMe = objPhieuKhamSoSinh.NghenghiepMe;
                }
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
                    objEmrBa.TinhtrangravienLydotuvongKhac = chkttrvTrong72hVaovien.Checked;
                    objEmrBa.TinhtrangravienThoigiantuvongTrong24h = chkttrvTrong24GioVaoVien.Checked;
                    objEmrBa.TinhtrangravienThoigiantuvongSau24h = chkttrvTrong48GioVaoVien.Checked;
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
                objEmrBa.CdDophauthuat = chk_thuthuat_sausinh.Checked;
                objEmrBa.CdDogayme = chk_phauthuat_sausinh.Checked;
                objEmrBa.CdTaibien = chk_taibien.Checked;
                objEmrBa.CdBienchung = chk_bienchung.Checked;

                objEmrBa.VaovienLydovaovien = txtBenhAnLyDoNhapVien.Text;
                objEmrBa.VaovienVaongaythucuabenh = Utility.ByteDbnull(txtBenhAnVaoNgayThu.Text);
                objEmrBa.HoibenhQuatrinhbenhly = Utility.sDbnull(txtBenhAnQuaTrinhBenhLy.Text);
              
                objEmrBa.KbMach = txtMach.Text;
                objEmrBa.KbNhietdo = txtNhietDo.Text;
                objEmrBa.KbHuyetap = txtha.Text;
                objEmrBa.KbNhiptho = txtNhipTho.Text;
                objEmrBa.KbCannang = txtCanNang.Text;
                objEmrBa.KbChieucao = txtChieuCao.Text;
                tinhBMI();
                //Thông tin khám bệnh
                objEmrBa.KbBmi = Utility.sDbnull(txtBMI.Text, 0);
                objEmrBa.KhambenhToanthan = Utility.sDbnull(txt_tinhtrang_toanthan.Text);
               
                objEmrBa.KhambenhThantietnieusinhduc = Utility.sDbnull(txt_coquan_sinhduc_ngoai.Text);
                objEmrBa.KhambenhThankinh = Utility.sDbnull(txt_thankinh_phanxa.Text);
                objEmrBa.KhambenhCoxuongkhop = Utility.sDbnull(txt_xuongkhop.Text);
             
              

                //
                objEmrBa.KhambenhXetnghiemClsCanlam = Utility.sDbnull(txtBenhAnCacXetNghiem.Text);
                objEmrBa.KhambenhTomtatbenhan = Utility.sDbnull(txtBenhAnTomTatBenhAn.Text);
             
                objEmrBa.TongketbaQuatrinhbenhlyDienbienlamsang = Utility.sDbnull(txtTKBAQuaTrinhBenhLy.Text);
                objEmrBa.TongketbaTomtatKqcls = Utility.sDbnull(txtTKBATTomTatKetQua.Text);
                objEmrBa.TongketbaPhuongphapdieutri = Utility.sDbnull(txtTKBAPhuongPhapDieuTri.Text);
                objEmrBa.TongketbaTinhtrangNguoiravien = Utility.sDbnull(txtTKBATinhTrangRaVien.Text);
                objEmrBa.TongketbaHuongdieutritieptheo = Utility.sDbnull(txtTKBAHuongDieuTri.Text);

                objEmrBa.IdNguoigiaoHoso = Utility.Int16Dbnull(txtNguoiGiaoHoSo.MyID);
                objEmrBa.TongketbaMaNguoigiaohoso = txtNguoiGiaoHoSo.Text;
                objEmrBa.IdNguoinhanHoso = Utility.Int16Dbnull(txtNguoiNhanHoSo.MyID);
                objEmrBa.TongketbaMaNguoiNhanhoso = txtNguoiNhanHoSo.Text;
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

        private void frm_BenhAn_Sosinh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                #region "Xử lý multiline"
                if (e.KeyCode == Keys.Enter)
                {
                    Control activeCtrl = Utility.getActiveControl(this);
                    if ((activeCtrl != null && activeCtrl.GetType().Equals(txtBenhAnQuaTrinhBenhLy.GetType())))
                        return;
                    else
                    {
                        if (activeCtrl.GetType().Equals(typeof(EditBox)))
                        {
                            EditBox box = activeCtrl as EditBox;
                            if (box.Multiline)
                            {
                                return;
                            }
                            else
                                SendKeys.Send("{TAB}");
                        }
                        else if (activeCtrl.GetType().Equals(typeof(TextBox)))
                        {
                            TextBox box = activeCtrl as TextBox;
                            if (box.Multiline)
                            {
                                return;
                            }
                            else
                                SendKeys.Send("{TAB}");
                        }
                        //else if (activeCtrl.Name == txtNhommau.Name)
                        //{
                        //    //uiTabInfor.SelectedIndex = 1;
                        //    //txtCT.Focus();
                        //}
                        else
                            SendKeys.Send("{TAB}");
                    }
                    

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
        void InitDanhmucChung()
        {
            txtBenhAnQuaTrinhBenhLy.Init();
            txtNhommau.Init();
            txt_nghenghiep.Init();
            txt_nghenghiep_me.Init();
            txtDantoc.Init();
        }
        public action m_enAct = action.Insert;
        private void frm_BenhAn_Sosinh_Load(object sender, EventArgs e)
        {
            try
            {
                InitDanhmucChung();
                EnableTextBox();//Cho nhanh 
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
        EmrPhieukhamSosinh objPhieuKhamSoSinh;
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
              
                FillThongtinRavien();
                FillThongtinChuyenVien();
                FillTongketBenhAn();
               
                //Trang 2
                FillThongtinNhapvien();
                FillPhieuKhamSoSinh();
                //Trang 3
               
                Utility.GetChanDoanNoitru(objLuotkham, ref ICD_Khoa_NoITru, ref Name_Khoa_NoITru);
                txtCDKhiVaoDieuTri.Text = Name_Khoa_NoITru;
                txtCDMaKhiVaoDieuTri.Text = ICD_Khoa_NoITru;
                
                if (objEmrBa != null)
                {
                    m_enAct = action.Update;
                    cboLoaiBA.SelectedIndex = Utility.GetSelectedIndex(cboLoaiBA, objEmrBa.LoaiBa);
                    maBA = objEmrBa.MaBa;
                    dtDataBA = SPs.EmrBaLaythongtin(-1, "", objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
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

                       
                        txtCDNoiChuyenDen.Text = Utility.sDbnull(objEmrBa.CdNoichuyenden);
                        txtCDMaNoiChuyenDen.Text = Utility.sDbnull(objEmrBa.CdNoichuyendenMa);
                        txtCDKKBCapCuu.Text = Utility.sDbnull(objEmrBa.CdKkbCapcuu);
                        txtCDMaKKBCapCuu.Text = Utility.sDbnull(objEmrBa.CdKkbCapcuuMa);
                        txtCDKhiVaoDieuTri.Text = Utility.sDbnull(objEmrBa.CdKhoadieutri);
                        txtCDMaKhiVaoDieuTri.Text = Utility.sDbnull(objEmrBa.CdKhoadieutriMa);
                      
                        lblqlbnKhoa.Text = objEmrBa.VaovienTenkhoa;
                        lblMakhoavao.Text = objEmrBa.VaovienMakhoa;
                        chkQLNBTuyenTren.Checked = Utility.Bool2Bool(objEmrBa.ChuyenvienTuyentren);
                        chkQLNBTuyenDuoi.Checked = Utility.Bool2Bool(objEmrBa.ChuyenvienTuyenduoi);
                        chkQLNBChuyenVienCK.Checked = Utility.Bool2Bool(objEmrBa.ChuyenvienKhac);
                        txtQLNBChuyenVienNoiChuyenDen.Text = Utility.sDbnull(objEmrBa.ChuyenvienNoichuyenden);
                        if (objEmrBa.TrangThai >=1 )
                        {
                            if (objEmrBa.RavienNgay.HasValue)
                                dtpRavien_ngay.Value = objEmrBa.RavienNgay.Value;
                            else
                                dtpRavien_ngay.ResetText();
                            chkQLNBRaVienRavien.Checked = Utility.Bool2Bool(objEmrBa.RavienRavien);
                            chkQLNBRavienXinVe.Checked = Utility.Bool2Bool(objEmrBa.RavienXinve);
                            chkQLNBRavienBoVe.Checked = Utility.Bool2Bool(objEmrBa.RavienBove);
                            chkQLNBRavienDuaVe.Checked = Utility.Bool2Bool(objEmrBa.RavienDuave);
                            txtQLNBTongSoNgayDieuTri.Text = Utility.sDbnull(objEmrBa.RavienTongsongayDieutri);
                            txtCDRavienTenBenhChinh.Text = Utility.sDbnull(objEmrBa.RavienTenBenhchinh);
                            txtCDRavienMaBenhChinh.Text = Utility.sDbnull(objEmrBa.RavienMaBenhchinh);
                            txtCDRavienTenBenhKemTheo.Text = Utility.sDbnull(objEmrBa.RavienTenBenhphu);
                            txtCDRavienMaBenhKemTheo.Text = Utility.sDbnull(objEmrBa.RavienMaBenhphu);
                        }
                        chk_phauthuat_sausinh.Checked = Utility.Bool2Bool(objEmrBa.CdDogayme);
                        chk_thuthuat_sausinh.Checked = Utility.Bool2Bool(objEmrBa.CdPhauthuat);
                       
                        chk_taibien.Checked = Utility.Bool2Bool(objEmrBa.CdTaibien);
                        chk_bienchung.Checked = Utility.Bool2Bool(objEmrBa.CdBienchung);
                      
                        if (objEmrBa.TrangThai >= 1)
                        {
                            txt_hoten_bo.Text = Utility.sDbnull(objEmrBa.HotenBo);
                            txt_nghenghiep._Text = Utility.sDbnull(objEmrBa.NghenghiepBo);
                            txtNhommau._Text = Utility.sDbnull(objEmrBa.KbNhommau);
                            nmr_solan_de.Value = Utility.DecimaltoDbnull(objEmrBa.SolanDe);

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
                            chkttrvDoKhac.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienLydotuvongKhac);
                            chkttrvTrong24GioVaoVien.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienThoigiantuvongTrong24h);
                            chkttrvTrong48GioVaoVien.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienThoigiantuvongTrong48h);
                            chkttrvTrong72hVaovien.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienThoigiantuvongTrong72h);

                            txtTTRVNguyenNhanChinhTuVong.Text = Utility.sDbnull(objEmrBa.TinhtrangravienNguyennhantuvong);
                            chkTTRVChandoanGiaiphauTuthi.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienKhamnghiemtuthi);
                            txtTTRVChandoanGiaiphauTuthi.Text = Utility.sDbnull(objEmrBa.TinhtrangravienChandoangiauphaututhi);
                            //Tờ 2
                            txtBenhAnLyDoNhapVien._Text = Utility.sDbnull(objEmrBa.VaovienLydovaovien);// Utility.sDbnull(dr["BaLdvv"].ToString());
                            txtBenhAnVaoNgayThu.Text = Utility.sDbnull(objEmrBa.VaovienVaongaythucuabenh);
                            txtBenhAnQuaTrinhBenhLy._Text = Utility.sDbnull(objEmrBa.HoibenhQuatrinhbenhly);// Utility.sDbnull(dr["BaQtbl"].ToString());
                            
                        }
                       
                        txtBacsiKham.SetId(objEmrBa.IdBacsiKham);
                        //dtNgayKham.Value = Convert.ToDateTime(string.IsNullOrEmpty(objEmrBa.NgayKham) ? dtNgayKham.Value : objEmrBa.NgayKham);
                        dtpNgayKham.Value = string.IsNullOrEmpty(objEmrBa.NgayKham.ToString()) ? dtpNgayKham.Value : Convert.ToDateTime(objEmrBa.NgayKham);

                      
                        
                        txtMach.Text = Utility.sDbnull(objEmrBa.KbMach);
                        txtNhietDo.Text = Utility.sDbnull(objEmrBa.KbNhietdo);
                        txtha.Text = Utility.sDbnull(objEmrBa.KbHuyetap);
                        txtNhipTho.Text = Utility.sDbnull(objEmrBa.KbNhiptho);
                        txtCanNang.Text = Utility.sDbnull(objEmrBa.KbCannang);
                        txtChieuCao.Text = Utility.sDbnull(objEmrBa.KbChieucao);
                        tinhBMI();
                        txt_tinhtrang_toanthan.Text = Utility.sDbnull(objEmrBa.KhambenhToanthan);// Utility.sDbnull(dr["KbToanThan"].ToString());
                     
                        txt_coquan_sinhduc_ngoai.Text = Utility.sDbnull(objEmrBa.KhambenhThantietnieusinhduc);
                        txt_thankinh_phanxa.Text = Utility.sDbnull(objEmrBa.KhambenhThankinh);
                        txt_xuongkhop.Text = Utility.sDbnull(objEmrBa.KhambenhCoxuongkhop);
                        txt_thankinh_truonglucco.Text = Utility.sDbnull(objEmrBa.KhambenhTaimuihong);
                      
                        txtBenhAnCacXetNghiem.Text = Utility.sDbnull(objEmrBa.KhambenhXetnghiemClsCanlam);
                        txtBenhAnTomTatBenhAn.Text = Utility.sDbnull(objEmrBa.KhambenhTomtatbenhan);
                       
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
                    txtCDKKBCapCuu.Text =Utility.Get_ChanDoan_KKB_CapCuu(objLuotkham);
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
                EnableBA();
            }
        }
       
        void FillPhieuKhamSoSinh()
        {
            objPhieuKhamSoSinh = new Select().From(EmrPhieukhamSosinh.Schema)
             .Where(EmrPhieukhamSosinh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
             .And(EmrPhieukhamSosinh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
             .ExecuteSingle<EmrPhieukhamSosinh>();
            if (objPhieuKhamSoSinh != null)
            {

                txtBacsiKham.SetId(objPhieuKhamSoSinh.IdBacsi);
                if (objPhieuKhamSoSinh.OivoLuc.HasValue)
                    dtp_oivo_luc.Value = objPhieuKhamSoSinh.OivoLuc.Value;

                txt_hoten_me.Text = Utility.sDbnull(objPhieuKhamSoSinh.HotenBe);
                if (objPhieuKhamSoSinh.NgaysinhMe.HasValue)
                    dtp_ngaysinh_me.Value = objPhieuKhamSoSinh.NgaysinhMe.Value;
                txtNhommau.SetCode(Utility.sDbnull(objPhieuKhamSoSinh.NhommauMe));
                nmr_solan_de.Value = Utility.DecimaltoDbnull(objPhieuKhamSoSinh.SolanDe);
                txt_nghenghiep_me.SetCode( Utility.sDbnull(objPhieuKhamSoSinh.NghenghiepMe));

                txtDantoc.SetCode(Utility.sDbnull(objPhieuKhamSoSinh.MaDantoc));
                chk_ngoaikieu.Checked = Utility.Byte2Bool(objPhieuKhamSoSinh.NgoaiKieu);
                txt_hoten_bo.Text = Utility.sDbnull(objPhieuKhamSoSinh.HotenBo);
                txt_nghenghiep.SetCode( Utility.sDbnull(objPhieuKhamSoSinh.NghenghiepBo));
                if (objPhieuKhamSoSinh.NgaysinhBo.HasValue)
                    dtp_ngaysinh_bo.Value = objPhieuKhamSoSinh.NgaysinhBo.Value;
                


                opt_nuoc_oi_trong.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.NuocOiTrong);
                opt_nuoc_oi_xanhban.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.NuocOiXanhban);
                opt_nuoc_oi_lanmau.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.NuocOiLanmau);
                txt_nuoc_oi_mota.Text = Utility.sDbnull(objPhieuKhamSoSinh.NuocOiMota);

                opt_dethuong.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.Dethuong);
                opt_canthiep.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.Canthiep);
                if (objPhieuKhamSoSinh.Deluc.HasValue)
                    dtp_deluc.Value = objPhieuKhamSoSinh.Deluc.Value;

                //Tiền sử sản Sơ sinh
                txt_lydocanthiep.Text = Utility.sDbnull(objPhieuKhamSoSinh.Lydocanthiep);
                //Tình trạng trẻ sơ sinh
                opt_khocnga.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.Khocnga);
                opt_nga.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.Nga);
                opt_kha.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.Kha);
                txt_hoten_nguoi_dode.Text = Utility.sDbnull(objPhieuKhamSoSinh.HotenNguoiDode);
                txt_chucdanh_nguoi_dode.Text = Utility.sDbnull(objPhieuKhamSoSinh.ChucdanhNguoiDode);

                txt1phut.Text = Utility.sDbnull(objPhieuKhamSoSinh.Apgar1phut);
                txt5phut.Text = Utility.sDbnull(objPhieuKhamSoSinh.Apgar5phut);
                txt10phut.Text = Utility.sDbnull(objPhieuKhamSoSinh.Apgar10phut);

                txtCanNang.Text = Utility.sDbnull(objPhieuKhamSoSinh.TresosinhCannang);
                txt_vongdau.Text = Utility.sDbnull(objPhieuKhamSoSinh.VongDau);
                txtChieuCao.Text = Utility.sDbnull(objPhieuKhamSoSinh.ChieuDai);
                txt_tinhtrang_dinhduong_sausinh.Text = Utility.sDbnull(objPhieuKhamSoSinh.TinhtrangDinhduongSausinh);
                //Phương pháp hồi sinh sau đẻ
                chk_hut_dich.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.HutDich);
                chk_xoabop_tim.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.XoabopTim);
                chk_tho_oxy.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.ThoOxy);
                chk_dat_noikhiquan.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.DatNoikhiquan);
                chk_bop_bong.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.BopBong);
                chk_phuongphap_khac.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.PhuongphapKhac);
                //Khám bệnh
                chk_tresosinh_tatbamsinh.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.TresosinhTatbamsinh);
                chk_tresosinh_cohaumon.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.TresosinhCohaumon);
                txt_tresosinh_tatbamsinh_mota.Text = Utility.sDbnull(objPhieuKhamSoSinh.TresosinhTatbamsinhMota);
                txt_tinhhinh_tresosinh_khivaokhoa.Text = Utility.sDbnull(objPhieuKhamSoSinh.TinhhinhTresosinhKhivaokhoa);
                txt_tinhtrang_toanthan.Text = Utility.sDbnull(objPhieuKhamSoSinh.TinhtrangToanthan);
                chk_mausacda_honghao.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.MausacdaHonghao);
                chk_mausacda_xanhtai.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.MausacdaXanhtai);
                chk_mausacda_vang.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.MausacdaVang);
                chk_mausacda_tim.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.MausacdaTim);
                chk_mausacda_khac.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.MausacdaKhac);
                //Các cơ quan khác
                txtNhipTho.Text = Utility.sDbnull(objPhieuKhamSoSinh.NhipTho);
                txt_nghe_phoi.Text = Utility.sDbnull(objPhieuKhamSoSinh.NghePhoi);
                nmr_chiso_silverman.Text = Utility.sDbnull(objPhieuKhamSoSinh.ChisoSilverman);

                opt_dieuhoa.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.Dieuhoa);
                opt_xedich_nhiptho.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.XedichNhiptho);
                opt_khongdidong_ngucbung.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.KhongdidongNgucbung);

                opt_cokeo_coliensuon_khong.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.CokeoColiensuonKhong);
                opt_cokeo_coliensuon_coit.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.CokeoColiensuonCoit);
                opt_cokeo_coliensuon_thayro.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.CokeoColiensuonThayro);

                opt_cokeo_muiuc_khong.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.CokeoMuiucKhong);
                opt_cokeo_muiuc_coit.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.CokeoMuiucCoit);
                opt_cokeo_muiuc_thayro.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.CokeoMuiucThayro);

                opt_dapcanh_mui_khong.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.DapcanhMuiKhong);
                opt_dapcanh_mui_nhe.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.DapcanhMuiNhe);
                opt_dapcanh_mui_ro.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.DapcanhMuiRo);

                opt_renri_khong.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.RenriKhong);
                opt_renri_nghebang_ongnghe.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.RenriNghebangOngnghe);
                opt_renri_taithuong_nghero.Checked = Utility.Bool2Bool(objPhieuKhamSoSinh.RenriTaithuongNghero);
                txtMach.Text = Utility.sDbnull(objPhieuKhamSoSinh.NhipTim);

                txt_bung.Text = Utility.sDbnull(objPhieuKhamSoSinh.Bung);
                txt_coquan_sinhduc_ngoai.Text = Utility.sDbnull(objPhieuKhamSoSinh.CoquanSinhducNgoai);
                txt_xuongkhop.Text = Utility.sDbnull(objPhieuKhamSoSinh.Xuongkhop);
                txt_thankinh_phanxa.Text = Utility.sDbnull(objPhieuKhamSoSinh.ThankinhPhanxa);
                txt_thankinh_truonglucco.Text = Utility.sDbnull(objPhieuKhamSoSinh.ThankinhTruonglucco);


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
                txtBenhAnQuaTrinhBenhLy._Text = Utility.sDbnull(objNhapvien.QuatrinhBenhly);
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
        /// <summary>
        /// Trạng thái 0= khởi tạo;1= Lưu;2= kết thúc
        /// </summary>
        void ModifyCommand()
        {
            tabpageTo2.Enabled = tabpageTo3.Enabled = tabpageTo4.Enabled = objLuotkham != null;
            btnInto2.Enabled = btnInto3.Enabled = Into1.Enabled = btnInto4.Enabled = button1.Enabled = btnInVoBA.Enabled = objLuotkham != null && objEmrBa != null;
            cmdXoaBenhAn.Enabled = objLuotkham != null && objEmrBa != null;
            cmdKhoitaoBA.Enabled = objEmrBa == null;
            cmdSave.Enabled = objEmrBa != null && objEmrBa.TrangThai<=1;
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
                                Utility.Log("frm_BenhAn_Sosinh", globalVariables.UserName, string.Format("Xóa bệnh án id={0}, loại BA={1}, mã BA={2} của người bệnh id ={3}, mã lần khám {4} thành công",objEmrBa.IdBa,objEmrBa.LoaiBa,objEmrBa.MaBa,objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham), newaction.Delete, "UI");
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
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, null, null, 0, false);
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
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, null, null, 1, false);
        }

        private void mnuInTo2_Click(object sender, EventArgs e)
        {
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, null, null, 2, false);
        }

        private void mnuInTo3_Click(object sender, EventArgs e)
        {
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, null, null, 3, false);
        }

        private void mnuInTo4_Click(object sender, EventArgs e)
        {
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, null, null, 4, false);
        }
       
        private void mnuInBA_Click(object sender, EventArgs e)
        {
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, null, null, 100, false);
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


        private void chkEditPKB_CheckedChanged(object sender, EventArgs e)
        {
            //txtBenhAnToanThan.ReadOnly = txtBenhAnTuanHoan.ReadOnly = txtBenhAnHoHap.ReadOnly
            //   = txtBenhAnTieuHoa.ReadOnly = txtBenhAnThanTietNieuSinhDuc.ReadOnly = txtBenhAnThanKinh.ReadOnly
            //   = txtBenhAnCoXuongKhop.ReadOnly = txtBenhAnTaiMuiHong.ReadOnly = txtBenhAnMat.ReadOnly
            //   = txtBenhAnNoiTiet.ReadOnly = true;// !chkEditPKB.Checked && !chkEditPKB.Visible;
        }
        void EnableTextBox()
        {
            //txtBenhAnToanThan.ReadOnly = txtBenhAnTuanHoan.ReadOnly = txtBenhAnHoHap.ReadOnly
            // = txtBenhAnTieuHoa.ReadOnly = txtBenhAnThanTietNieuSinhDuc.ReadOnly = txtBenhAnThanKinh.ReadOnly
            // = txtBenhAnCoXuongKhop.ReadOnly = txtBenhAnTaiMuiHong.ReadOnly = txtBenhAnMat.ReadOnly
            // = txtBenhAnNoiTiet.ReadOnly = true;
            //txtTKBAQuaTrinhBenhLy.ReadOnly = txtTKBATTomTatKetQua.ReadOnly
            // = txtTKBAPhuongPhapDieuTri.ReadOnly = txtTKBATinhTrangRaVien.ReadOnly
            // = txtTKBAHuongDieuTri.ReadOnly = txtB_Xquang.ReadOnly = txtB_CTScanner.ReadOnly = txtB_SieuAm.ReadOnly
            // = txtB_XetNghiem.ReadOnly = txtB_Khac.ReadOnly = txtNguoiGiaoHoSo.ReadOnly = txtNguoiNhanHoSo.ReadOnly = txtBSDieuTri.ReadOnly
            // = false;
        }
        private void chkEditTKBA_CheckedChanged(object sender, EventArgs e)
        {
            //txtTKBAQuaTrinhBenhLy.ReadOnly = txtTKBATTomTatKetQua.ReadOnly
            //  = txtTKBAPhuongPhapDieuTri.ReadOnly = txtTKBATinhTrangRaVien.ReadOnly
            //  = txtTKBAHuongDieuTri.ReadOnly = txtB_Xquang.ReadOnly = txtB_CTScanner.ReadOnly = txtB_SieuAm.ReadOnly
            //  = txtB_XetNghiem.ReadOnly = txtB_Khac.ReadOnly = txtNguoiGiaoHoSo.ReadOnly = txtNguoiNhanHoSo.ReadOnly = txtBSDieuTri.ReadOnly
            //  = false;// !chkEditTKBA.Checked && !chkEditTKBA.Visible;
        }

        private void mnuSent2EMR_Click(object sender, EventArgs e)
        {
            try
            {
                if(objEmrBa!=null)
                {
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_BIA, "BA06_BASOSINH_BIA", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_SOSINH);
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO1, "BA06_BASOSINH_TO1", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_SOSINH);
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO2, "BA06_BASOSINH_TO2", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_SOSINH);
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO3, "BA06_BASOSINH_TO3", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_SOSINH);
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO4, "BA06_BASOSINH_TO4", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_SOSINH);
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BA_PHUKHOA, "BA06_BASOSINH", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_SOSINH);
                    emrdoc.Save();
                    Utility.ShowMsg("Đẩy dữ liệu vào EMR thành công");
                }    
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdKhamphukhoa_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Cần nhập thông tin người bệnh trước khi thực hiện thêm thông tin phiếu khám Sơ sinh");
                return;
            }
            frm_khamPhukhoa _khamPhukhoa = new frm_khamPhukhoa(objLuotkham, objBenhnhan);
            _khamPhukhoa.ShowDialog();
            FillPhieuKhamSoSinh();
        }

        private void cmdKhamPhuKhoa2_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Cần nhập thông tin người bệnh trước khi thực hiện thêm thông tin phiếu khám Sơ sinh");
                return;
            }
            frm_khamPhukhoa _khamPhukhoa = new frm_khamPhukhoa(objLuotkham, objBenhnhan);
            _khamPhukhoa.ShowDialog();
            FillPhieuKhamSoSinh();
        }

        private void cmdSave_Click_1(object sender, EventArgs e)
        {
            LuuBA(1);
        }

        private void lnk21_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtCDKKBCapCuu.Text = Utility.Get_ChanDoan_KKB_CapCuu(objLuotkham);
            txtCDMaKKBCapCuu.Text = Utility.sDbnull(objLuotkham.MabenhChinh, string.Empty);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Utility.GetChanDoanNoitru(objLuotkham, ref ICD_Khoa_NoITru, ref Name_Khoa_NoITru);
            txtCDKhiVaoDieuTri.Text = Name_Khoa_NoITru;
            txtCDMaKhiVaoDieuTri.Text = ICD_Khoa_NoITru;
        }

        private void opt_canthiep_CheckedChanged(object sender, EventArgs e)
        {
            txt_lydocanthiep.Enabled = opt_canthiep.Checked;
            if(opt_canthiep.Checked)
            {
                txt_lydocanthiep.Focus();
            }    
        }
    }
}
