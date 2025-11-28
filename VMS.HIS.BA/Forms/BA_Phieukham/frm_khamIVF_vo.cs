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
using VMS.HIS.DAL;
using VNS.Libs;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.UCs;
using Janus.Windows.GridEX.EditControls;
using VMS.HIS.Danhmuc.Dungchung;
using System.Transactions;

namespace VMS.HIS.UI.EMR
{
    public partial class frm_khamIVF_vo : Form
    {
        public KcbLuotkham objLuotkham;
        KcbDanhsachBenhnhan objBenhnhan;
        DataTable dt_tssk;
        bool AllowedChanged = false;
        action m_enAct = action.FirstOrFinished;
        public frm_khamIVF_vo(KcbLuotkham objLuotkham, KcbDanhsachBenhnhan objBenhnhan)
        {
            InitializeComponent();
            this.KeyDown += frm_khamIVF_vo_KeyDown;
            this.objLuotkham = objLuotkham;
            this.objBenhnhan = objBenhnhan;
            if (objBenhnhan == null)
                objBenhnhan = new Select().From(KcbDanhsachBenhnhan.Schema).Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).ExecuteSingle<KcbDanhsachBenhnhan>();
            Utility.SetVisualStyle(this);
            ucThongtinnguoibenh1._OnEnterMe += ucThongtinnguoibenh1__OnEnterMe;
            ucThongtinnguoibenh1.txtMaluotkham.Text = objLuotkham.MaLuotkham;
            cmdxoa.Click += cmdxoa_Click;
            cmdIn.Click += cmdIn_Click;
            cmdGhi.Click += cmdGhi_Click;
            txtNhommau._OnShowDataV1 += __OnShowDataV1;
            txtCanNang.TextChanged += txtCanNang_TextChanged;
            txtChieuCao.TextChanged += txtChieuCao_TextChanged;
            SetProperties(this);
            
        }

        void SetProperties(Control parent)
        {
            foreach (Control ctr in parent.Controls)
                if (ctr.GetType().Equals(nmr_chukykinhnguyet_songay.GetType()))
                {
                    NumericUpDown nmr = ctr as NumericUpDown;
                    nmr.Tag = 0;
                    nmr.MouseUp += nmr_MouseUp;
                    nmr.GotFocus += nmr_GotFocus;
                    nmr.Leave += nmr_Leave;

                }
                else
                    SetProperties(ctr);
        }

        private void nmr_GotFocus(object sender, EventArgs e)
        {

            NumericUpDown nmr = sender as NumericUpDown;
            // focus từ bàn phím (TAB), chưa có mouse event, chọn toàn bộ
            nmr.Select(0, nmr.Text.Length);
            nmr.Tag = 1;
        }

        private void nmr_MouseUp(object sender, MouseEventArgs e)
        {
            NumericUpDown nmr = sender as NumericUpDown;
            // chọn toàn bộ sau khi click, tránh lặp lại nếu đã chọn
            if (nmr.Tag.ToString() != "1")
            {
                nmr.BeginInvoke((MethodInvoker)(() =>
                {
                    nmr.Select(0, nmr.Text.Length);
                }));
                nmr.Tag = 1;
            }
        }

        private void nmr_Leave(object sender, EventArgs e)
        {
            NumericUpDown nmr = sender as NumericUpDown;
            nmr.Tag = 0; // reset khi rời khỏi control
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
       
        void __OnShowDataV1(AutoCompleteTextbox_Danhmucchung obj)
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

        void cmdGhi_Click(object sender, EventArgs e)
        {
            if (!isValidData()) return;
            SaveData();
        }

        bool isValidData()
        {
            if (Utility.sDbnull(cboBacsi.SelectedValue, "-1") == "1")
            {
                Utility.ShowMsg("Bạn phải chọn bác sĩ khám");
                cboBacsi.Focus();
            }
            return true;
        }
        void cmdIn_Click(object sender, EventArgs e)
        {
            
        }


        void cmdxoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (objLuotkham.TrangthaiNgoaitru == 1 || objLuotkham.NgayKetthuc != null || (Utility.Byte2Bool(objLuotkham.TthaiThopNoitru) && objLuotkham.TrangthaiNoitru == 6))
                {
                    Utility.ShowMsg("Bệnh nhân đã kết thúc khám nên bạn không thể thực hiện chức năng này");
                    ucThongtinnguoibenh1.txtMaluotkham.Focus();
                    ucThongtinnguoibenh1.txtMaluotkham.SelectAll();
                    return;
                }
                if (globalVariables.IsAdmin || objPhieuKhamIvfVo.NguoiTao == globalVariables.UserName.ToString())
                {
                    objPhieuKhamIvfVo = EmrPhieukhamIvfVo.FetchByID(objPhieuKhamIvfVo.Id);
                    if (objPhieuKhamIvfVo != null)
                    {
                        if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa thông tin khám chữa bệnh ngày {0} của bác sĩ {1} thực hiện", "Cảnh báo", true))
                        {
                            EmrPhieukhamIvfVo.Delete(objPhieuKhamIvfVo.Id);
                        }
                    }
                    else
                    {
                        Utility.ShowMsg(string.Format("Không thể xóa phiếu khám IVF chồng.\nVui lòng kiểm tra lại vì có thể trong lúc bạn mở thao tác người khác đã xóa thông tin", objPhieuKhamIvfVo.NguoiTao));
                    }    
                }
                else
                {
                    Utility.ShowMsg(string.Format("Bạn không thể xóa thông tin khám được tạo bởi bác sĩ {0}.\nVui lòng kiểm tra lại", objPhieuKhamIvfVo.NguoiTao));
                }
            }
            catch (Exception)
            {


            }
            finally
            {
                ModifyCommmands();
            }
            
        }


        void ClearControls()
        {


            foreach (Control ctr in pnlInfor.Controls)
                if (ctr.GetType().Equals(autoTxt.GetType()))
                    ((AutoCompleteTextbox_Danhmucchung)ctr).SetDefaultItem();
                else if (ctr is EditBox)
                {
                    ((EditBox)(ctr)).Clear();
                }
            foreach (Control ctr in grpChucNangSong.Controls)
                if (ctr.GetType().Equals(autoTxt.GetType()))
                    ((AutoCompleteTextbox_Danhmucchung)ctr).SetDefaultItem();
                else if (ctr is EditBox)
                {
                    ((EditBox)(ctr)).Clear();
                }

        }
     
        void ModifyCommmands()
        {
            cmdxoa.Enabled = cmdIn.Enabled = objPhieuKhamIvfVo != null;
        }
        void frm_khamIVF_vo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Control activeCtrl = Utility.getActiveControl(this);
                if ((activeCtrl != null && (activeCtrl.Name == autoTxt.Name || activeCtrl.Name == autoTxt.Name )))
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
                    else if (activeCtrl.Name == txtNhommau.Name)
                    {
                        //uiTabInfor.SelectedIndex = 1;
                        //txtCT.Focus();
                    }
                    else
                        SendKeys.Send("{TAB}");
                }


            }
            else if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            else if (e.Control && e.KeyCode == Keys.D) cmdxoa.PerformClick();
            else if (e.Control && e.KeyCode == Keys.P) cmdIn.PerformClick();
        }
      
        void ucThongtinnguoibenh1__OnEnterMe()
        {
            if (ucThongtinnguoibenh1.objLuotkham != null)
            {
                objLuotkham = ucThongtinnguoibenh1.objLuotkham;
                this.Text = string.Format("Phiếu khám sản khoa cho người bệnh {0} - {1} - {2} -{3}", ucThongtinnguoibenh1.txtTenBN.Text, ucThongtinnguoibenh1.txtgioitinh.Text, ucThongtinnguoibenh1.txttuoi.Text, ucThongtinnguoibenh1.txtDiachi.Text);
                objTsbDacdiemlienquan = new Select().From(EmrTiensubenhDacdiemlienquan.Schema)
                   .Where(EmrTiensubenhDacdiemlienquan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(EmrTiensubenhDacdiemlienquan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<EmrTiensubenhDacdiemlienquan>();
                objPhieuKhamIvfVo= new Select().From(EmrPhieukhamIvfVo.Schema)
                   .Where(EmrPhieukhamIvfVo.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(EmrPhieukhamIvfVo.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<EmrPhieukhamIvfVo>();
              
                FillData();
            }
            else
            {
                ClearControls();
                this.Text = "Phiếu khám IVF chồng";
            }    
        }
       
       
        private void frm_khamIVF_vo_Load(object sender, EventArgs e)
        {
            InitDanhmucchung();
            DataBinding.BindDataCombobox(cboBacsi, globalVariables.gv_dtDmucNhanvien.Copy(),
                                     DmucNhanvien.Columns.UserName, DmucNhanvien.Columns.TenNhanvien, "----Chọn bác sĩ khám----", true);
            DataBinding.BindDataCombobox(cbo_bacsitheodoi, globalVariables.gv_dtDmucNhanvien.Copy(),
                                     DmucNhanvien.Columns.UserName, DmucNhanvien.Columns.TenNhanvien, "----Chọn bác sĩ khám----", true);
            ucThongtinnguoibenh1.Refresh();
            dtNgayKham.Value = DateTime.Now.Date;
            dtp_ngaysinh_vo.Value =  objBenhnhan.NgaySinh.Value;
            dtp_ngaylapgiadinh.Value = DateTime.Now.Date;
            dtNgayKham.Focus();
            ModifyCommmands();
           
        }
        void FillDacdiemLienquan()
        {
            objTsbDacdiemlienquan = new Select().From(EmrTiensubenhDacdiemlienquan.Schema)
             .Where(EmrTiensubenhDacdiemlienquan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
             .And(EmrTiensubenhDacdiemlienquan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
             .ExecuteSingle<EmrTiensubenhDacdiemlienquan>();
            if (objTsbDacdiemlienquan != null)
            {
                chkMaTuy.Checked = Utility.Bool2Bool(objTsbDacdiemlienquan.TsbMatuy);
                chkRuouBia.Checked = Utility.Bool2Bool(objTsbDacdiemlienquan.TsbRuoubia);
                chkThuocLa.Checked = Utility.Bool2Bool(objTsbDacdiemlienquan.TsbThuocla);
                chkKhac.Checked = Utility.Bool2Bool(objTsbDacdiemlienquan.TsbKhac);
                txtMaTuy.Text = Utility.sDbnull(objTsbDacdiemlienquan.TsbThoigianMatuy);
                txtRuouBia.Text = Utility.sDbnull(objTsbDacdiemlienquan.TsbThoigianRuoubia);
                txtThuocLa.Text = Utility.sDbnull(objTsbDacdiemlienquan.TsbThoigianThuocla);
                txt_dacdiemlienquankhac.Text = Utility.sDbnull(objTsbDacdiemlienquan.TsbThoigianKhac);
            }
        }
        EmrPhieukhamIvfVo objPhieuKhamIvfVo = null;
      
        private void FillData()
        {
            try
            {
                FillDacdiemLienquan();
                if (objPhieuKhamIvfVo != null)
                {
                    txtID.Text = objPhieuKhamIvfVo.Id.ToString();
                    txtNhietDo.Text = objPhieuKhamIvfVo.NhietDo;
                    txtha.Text = objPhieuKhamIvfVo.NhomMau;
                    txtMach.Text = objPhieuKhamIvfVo.Mach;
                    txtNhipTho.Text = objPhieuKhamIvfVo.NhịpTho;
                    txtChieuCao.Text = objPhieuKhamIvfVo.ChieuCao;
                    txtCanNang.Text = objPhieuKhamIvfVo.CanNang;
                    txtBMI.Text = objPhieuKhamIvfVo.Bmi;
                    txtNhommau.SetCode(objPhieuKhamIvfVo.NhomMau);
                    //Tiền sử nội khoa
                    txt_hoten_vo.Text = objPhieuKhamIvfVo.HotenVo;
                    dtp_ngaysinh_vo.Value = objPhieuKhamIvfVo.NgaysinhVo.Value;
                    txt_sodienthoai_vo.Text = objPhieuKhamIvfVo.SodienthoaiVo;
                    txt_diachi_vo.Text = objPhieuKhamIvfVo.DiachiVo;
                    cbo_bacsitheodoi.SelectedValue = Utility.Int16Dbnull(objPhieuKhamIvfVo.IdBacsitheodoiVo, -1);
                    //Tiền sử kinh nguyệt
                    nmr_tuoicokinhlandau.Value=  Utility.ByteDbnull(objPhieuKhamIvfVo.Tuoicokinhlandau);
                    opt_chukykinhnguyet_deu.Checked =Utility.Bool2Bool( objPhieuKhamIvfVo.ChukykinhnguyetDeu);
                    opt_chukykinhnguyet_khongdeu.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.ChukykinhnguyetKhongdeu);
                    txt_ghichu.Text = objPhieuKhamIvfVo.Ghichu;
                    nmr_chukykinhnguyet_songay.Value = Utility.ByteDbnull(objPhieuKhamIvfVo.ChukykinhnguyetSongay);
                    nmr_songay_cokinh.Value  = Utility.ByteDbnull(objPhieuKhamIvfVo.SongayCokinh);

                    opt_soluongkinhnguyet_it.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.SoluongkinhnguyetIt);
                    opt_soluongkinhnguyet_trungbinh.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.SoluongkinhnguyetTrungbinh);
                    opt_soluongkinhnguyet_nhieu.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.SoluongkinhnguyetNhieu);
                    opt_vokinh_nguyenphat.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.VokinhNguyenphat);
                    opt_vokinh_thuphat.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.VokinhThuphat);
                    opt_vokinh_khong.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.VokinhKhong);

                    opt_vosinh_nguyenphat.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.VosinhNguyenphat);
                    opt_vosinh_thuphat.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.VosinhThuphat);
                    opt_vosinh_khong.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.VosinhKhong);
                    //Mức độ sinh hoạt tình dục
                    nmr_mucdosinhhoattinhduc_theotuan.Value  = Utility.ByteDbnull(objPhieuKhamIvfVo.MucdosinhhoattinhducTheotuan);
                    opt_codaukhisinhhoat_co.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.CodaukhisinhhoatCo);
                    opt_codaukhisinhhoat_khong.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.CodaukhisinhhoatKhong);
                    //Tiền sử sinh sản
                    nmr_SolancothaiVoichonghiennay.Value = Utility.ByteDbnull(objPhieuKhamIvfVo.SolancothaiVoichonghiennay);
                    nmr_SolancothaiVoilankethontruoc.Value  = Utility.ByteDbnull(objPhieuKhamIvfVo.SolancothaiVoilankethontruoc);
                    nmr_SoconsongVoichonghiennay.Value= Utility.ByteDbnull(objPhieuKhamIvfVo.SoconsongVoichonghiennay);
                    nmr_SoconsongVoilankethontruoc.Value= Utility.ByteDbnull(objPhieuKhamIvfVo.SoconsongVoilankethontruoc);
                    nmr_DenonVoichonghiennay.Value = Utility.ByteDbnull(objPhieuKhamIvfVo.DenonVoichonghiennay);
                    nmr_DenonVoilankethontruoc.Value = Utility.ByteDbnull(objPhieuKhamIvfVo.DenonVoilankethontruoc);
                    nmr_SaythaiVoichonghiennay.Value= Utility.ByteDbnull(objPhieuKhamIvfVo.SaythaiVoichonghiennay);
                    nmr_SaythaiVoilankethontruoc.Value  = Utility.ByteDbnull(objPhieuKhamIvfVo.SaythaiVoilankethontruoc);
                    nmr_NaohutVoichonghiennay.Value= Utility.ByteDbnull(objPhieuKhamIvfVo.NaohutVoichonghiennay);
                    nmr_NaohutVoilankethontruoc.Value = Utility.ByteDbnull(objPhieuKhamIvfVo.NaohutVoilankethontruoc);
                    nmr_ThailuuVoichonghiennay.Value = Utility.ByteDbnull(objPhieuKhamIvfVo.ThailuuVoichonghiennay);
                    nmr_ThailuuVoilankethontruoc.Value = Utility.ByteDbnull(objPhieuKhamIvfVo.ThailuuVoilankethontruoc);


                    opt_GEU_co.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.GeuCo);
                    opt_GEU_khong.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.GeuKhong);
                    opt_chuatrung_co.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.ChuatrungCo);
                    opt_chuatrung_khong.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.ChuatrungKhong);

                    nmr_thoigian_vosinh.Value = Utility.ByteDbnull(objPhieuKhamIvfVo.Thoigianvosinh);
                    chk_phuongphapdieutrivosinh_khong.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.PhuongphapdieutrivosinhKhong);

                    chk_phuongphapdieutrivosinh_IUI.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.PhuongphapdieutrivosinhIui);
                    txt_phuongphapdieutrivosinh_IUI_mota.Text = objPhieuKhamIvfVo.PhuongphapdieutrivosinhIuiMota;

                    chk_phuongphapdieutrivosinh_IVF.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.PhuongphapdieutrivosinhIvf);
                    txt_phuongphapdieutrivosinh_IVF_mota.Text = objPhieuKhamIvfVo.PhuongphapdieutrivosinhIvfMota;

                    chk_phuongphapdieutrivosinh_khac.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.PhuongphapdieutrivosinhKhac);
                    txt_phuongphapdieutrivosinh_khac_mota.Text = objPhieuKhamIvfVo.PhuongphapdieutrivosinhKhacMota;
                    // Các thăm dò trước đó
                    //Chụp phim vòi trứng
                    chk_voitrungphai_thong.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.VoitrungphaiThong);
                    chk_voitrungphai_thonghanche.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.VoitrungphaiThonghanche);
                    chk_voitrungphai_tacgan.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.VoitrungphaiTacgan);
                    chk_voitrungphai_tacxa.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.VoitrungphaiTacxa);
                    chk_voitrungphai_unuoc.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.VoitrungphaiUnuoc);
                    chk_voitrungphai_khac.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.VoitrungphaiKhac);

                    chk_voitrungtrai_thong.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.VoitrungtraiThong);
                    chk_voitrungtrai_thonghanche.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.VoitrungtraiThonghanche);
                    chk_voitrungtrai_tacgan.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.VoitrungtraiTacgan);
                    chk_voitrungtrai_tacxa.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.VoitrungtraiTacxa);
                    chk_voitrungtrai_unuoc.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.VoitrungtraiUnuoc);
                    chk_voitrungtrai_khac.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.VoitrungtraiKhac);
                    //Nội soi vòi trứng
                    chk_noisoi_voitrungphai_thong.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.NoisoiVoitrungphaiThong);
                    chk_noisoi_voitrungphai_thonghanche.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.NoisoiVoitrungphaiThonghanche);
                    chk_noisoi_voitrungphai_tacgan.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.NoisoiVoitrungphaiTacgan);
                    chk_noisoi_voitrungphai_tacxa.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.NoisoiVoitrungphaiTacxa);
                    chk_noisoi_voitrungphai_unuoc.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.NoisoiVoitrungphaiUnuoc);
                    chk_noisoi_voitrungphai_khac.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.NoisoiVoitrungphaiKhac);

                    chk_noisoi_voitrungtrai_thong.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.NoisoiVoitrungtraiThong);
                    chk_noisoi_voitrungtrai_thonghanche.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.NoisoiVoitrungtraiThonghanche);
                    chk_noisoi_voitrungtrai_tacgan.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.NoisoiVoitrungtraiTacgan);
                    chk_noisoi_voitrungtrai_tacxa.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.NoisoiVoitrungtraiTacxa);
                    chk_noisoi_voitrungtrai_unuoc.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.NoisoiVoitrungtraiUnuoc);
                    chk_noisoi_voitrungtrai_khac.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.NoisoiVoitrungtraiKhac);

                    txt_chupphim_tucungvoitrung_ghichu.Text = Utility.sDbnull(objPhieuKhamIvfVo.ChupphimTucungvoitrungGhichu);
                    txt_noisoi_ghichu.Text= Utility.sDbnull(objPhieuKhamIvfVo.NoisoiGhichu);
                    //Siêu âm
                    opt_sis_co.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.SisCo );
                    opt_sis_khong.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.SisKhong);
                    dtp_ngay_sieuam_sis.Value = objPhieuKhamIvfVo.NgaySieuamSis.Value;
                    txt_ketqua_sieuam_sis.Text= Utility.sDbnull(objPhieuKhamIvfVo.KetquaSieuamSis);
                    //Các yếu tố ảnh hưởng khác
                    chkMaTuy.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.Matuy);
                    txtMaTuy.Text = objPhieuKhamIvfVo.MatuyMota;
                    chkRuouBia.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.Ruoubia);
                    txtRuouBia.Text = objPhieuKhamIvfVo.RuoubiaMota;
                    chkThuocLa.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.Thuocla);
                    txtThuocLa.Text = objPhieuKhamIvfVo.ThuoclaMota;

                    //Tiền sử nội khoa
                    opt_tiensubenh_anhhuongdensinhsan_co.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.TiensubenhAnhhuongdensinhsanCo);
                    opt_tiensubenh_anhhuongdensinhsan_khong.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.TiensubenhAnhhuongdensinhsanKhong);
                    chk_benhlaophoi.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.Benhlaophoi);
                    chk_benhtieuduong.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.Benhtieuduong);
                    chk_benhtuyengiap.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.Benhtuyengiap);
                    chk_benhthankinh.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.Benhthankinh);
                    chk_benhkhac.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.TiensunoikhoaBenhkhac);
                    txt_noikhoabenhkhac_mota.Text= Utility.sDbnull(objPhieuKhamIvfVo.TiensungoaikhoaBenhkhacMota);
                    //Tiền sử ngoại khoa
                    opt_tiensungoaikhoa_co.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.TiensungoaikhoaCo);
                    opt_tiensungoaikhoa_khong.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.TiensungoaikhoaKhong);
                    txt_GEU.Text = Utility.sDbnull(objPhieuKhamIvfVo.Geu);
                    txt_catvoitrung.Text = Utility.sDbnull(objPhieuKhamIvfVo.Catvoitrung);
                    txt_mothongvoi.Text = Utility.sDbnull(objPhieuKhamIvfVo.Mothongvoi);
                    txt_boctach_unang_buongtrung.Text = Utility.sDbnull(objPhieuKhamIvfVo.BoctachUnangBuongtrung);
                    txt_cat_unang_buongtrung.Text = Utility.sDbnull(objPhieuKhamIvfVo.CatUnangBuongtrung);
                    txt_boctachnhanxotucung.Text = Utility.sDbnull(objPhieuKhamIvfVo.Boctachnhanxotucung);
                    txt_VRT.Text = Utility.sDbnull(objPhieuKhamIvfVo.Vrt);
                    txt_sinhmo.Text  = Utility.sDbnull(objPhieuKhamIvfVo.Sinhmo);
                    txt_tiensungoaikhoa_benhkhac_mota.Text = Utility.sDbnull(objPhieuKhamIvfVo.TiensungoaikhoaBenhkhacMota);
                    // Tiền sử khám bệnh chậu, hông, núm vúi,dị ứng thuốc
                    opt_tiensucacbenhviemchau_co.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.TiensucacbenhviemchauCo);
                    opt_tiensucacbenhviemchau_khong.Checked= Utility.Bool2Bool(objPhieuKhamIvfVo.TiensucacbenhviemchauKhong);
                    txt_tiensucacbenhviemchau_mota.Text = Utility.sDbnull(objPhieuKhamIvfVo.TiensucacbenhviemchauMota);

                    opt_tiensubenhlayquaduongtinhduc_co.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.TiensubenhlayquaduongtinhducCo);
                    opt_tiensubenhlayquaduongtinhduc_khong.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.TiensubenhlayquaduongtinhducKhong);
                    chk_tiensubenhlayquaduongtinhduc_lau.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.TiensubenhlayquaduongtinhducLau);
                    chk_tiensubenhlayquaduongtinhduc_giangmai.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.TiensubenhlayquaduongtinhducGiangmai);
                    chk_tiensubenhlayquaduongtinhduc_hiv.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.TiensubenhlayquaduongtinhducHiv);
                    chk_tiensubenhlayquaduongtinhduc_chlamydia.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.TiensubenhlayquaduongtinhducChlamydia);
                    chk_tiensubenhlayquaduongtinhduc_viemphanphu.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.TiensubenhlayquaduongtinhducViemphanphu);
                    chk_tiensubenhlayquaduongtinhduc_lao.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.TiensubenhlayquaduongtinhducLao);
                    chk_tiensubenhlayquaduongtinhduc_khac.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.TiensubenhlayquaduongtinhducKhac);

                    txt_tiensubenhlayquaduongtinhduc_mota.Text = Utility.sDbnull(objPhieuKhamIvfVo.TiensubenhlayquaduongtinhducMota);
                    //Khám thực thể
                    // Khám toàn thân
                    opt_khamtoanthan_binhthuong.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.KhamToanthanBinhthuong);
                    opt_khamtoanthan_batthuong.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.KhamToanthanBatthuong);
                    txt_kham_toanthan_mota.Text = Utility.sDbnull(objPhieuKhamIvfVo.KhamToanthanMota);

                    // Hệ thống lông
                    opt_hethong_long_binhthuong.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.HethongLongBinhthuong);
                    opt_hethong_long_batthuong.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.HethongLongBatthuong);
                    txt_hethong_long_mota.Text = Utility.sDbnull(objPhieuKhamIvfVo.HethongLongMota);

                    // Phát triển vú
                    opt_phattrien_vu_batthuong.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.PhattrienVuBatthuong);
                    opt_phattrien_vu_binhthuong.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.PhattrienVuBinhthuong);
                    txt_phattrien_vu_mota.Text = Utility.sDbnull(objPhieuKhamIvfVo.PhattrienVuMota);

                    // Tiết sữa
                    opt_tietsua_co.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.TietsuaCo);
                    opt_tietsua_khong.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.TietsuaKhong);
                    txt_TietsuaMota.Text = Utility.sDbnull(objPhieuKhamIvfVo.TietsuaMota);

                    // Chiều cao hông
                    opt_chieucaohong_batthuong.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.ChieucaohongBatthuong);
                    opt_chieucaohong_binhthuong.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.ChieucaohongBinhthuong);
                    txt_chieucaohong_mota.Text = Utility.sDbnull(objPhieuKhamIvfVo.ChieucaohongMota);

                    // Màng trinh
                    opt_mangtrinh_connguyen.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.MangtrinhConnguyen);
                    opt_mangtrinh_rach.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.MangtrinhRach);
                    txt_mangtrinh_mota.Text = Utility.sDbnull(objPhieuKhamIvfVo.MangtrinhMota);

                    // Âm đạo
                    opt_amdao_binhthuong.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.AmdaoBinhthuong);
                    opt_amdao_batthuong.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.AmdaoBatthuong);
                    txt_amdao_mota.Text = Utility.sDbnull(objPhieuKhamIvfVo.AmdaoMota);

                    // Cổ tử cung – nhóm checkbox
                    chk_cotucung_binhthuong.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.CotucungBinhthuong);
                    chk_cotucung_haitucung.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.CotucungHaitucung);
                    chk_cotucung_lotuyen.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.CotucungLotuyen);
                    chk_cotucung_polyp.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.CotucungPolyp);
                    chk_cotucung_sui.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.CotucungSui);
                    chk_cotucung_viem.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.CotucungViem);

                    // Huyết trắng (dịch tiết)
                    txt_thetich.Text = Utility.sDbnull(objPhieuKhamIvfVo.Thetich);
                    txt_matdo.Text = Utility.sDbnull(objPhieuKhamIvfVo.Matdo);
                    txt_didong.Text = Utility.sDbnull(objPhieuKhamIvfVo.Didong);
                    txt_tuthetucung.Text = Utility.sDbnull(objPhieuKhamIvfVo.Tuthetucung);

                    // Hai phần phụ và Catheter
                    opt_haiphanphu_batthuong.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.HaiphanphuBatthuong);
                    opt_haiphanphu_binhthuong.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.HaiphanphuBinhthuong);
                    opt_catheter_de.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.CatheterDe);
                    opt_catheter_kho.Checked = Utility.Bool2Bool(objPhieuKhamIvfVo.CatheterKho);

                    // TÓM TẮT BỆNH ÁN
                    txt_tomtat_benhan.Text = Utility.sDbnull(objPhieuKhamIvfVo.TomtatBenhan);
                    txt_chandoanphanbiet.Text = Utility.sDbnull(objPhieuKhamIvfVo.Chandoanphanbiet);
                    txt_ma_chandoanphanbiet.Text = Utility.sDbnull(objPhieuKhamIvfVo.MaChandoanphanbiet);
                    txt_chandoanvaovien.Text = Utility.sDbnull(objPhieuKhamIvfVo.Chandoanvaovien);
                    txt_ma_chandoanvaovien.Text = Utility.sDbnull(objPhieuKhamIvfVo.MaChandoanvaovien);
                    txt_chandoanxacdinh.Text = Utility.sDbnull(objPhieuKhamIvfVo.Chandoanxacdinh);
                    txt_ma_chandoanxacdinh.Text = Utility.sDbnull(objPhieuKhamIvfVo.MaChandoanxacdinh);

                    txt_tenbenhchinh.Text = Utility.sDbnull(objPhieuKhamIvfVo.Tenbenhchinh);
                    txt_mabenhchinh.Text = Utility.sDbnull(objPhieuKhamIvfVo.Mabenhchinh);
                    txt_tenbenhphu.Text = Utility.sDbnull(objPhieuKhamIvfVo.Tenbenhphu);
                    txt_mabenhphu.Text = Utility.sDbnull(objPhieuKhamIvfVo.Mabenhphu);
                    txt_tenbienchung.Text = Utility.sDbnull(objPhieuKhamIvfVo.Tenbienchung);
                    txt_mabienchung.Text = Utility.sDbnull(objPhieuKhamIvfVo.Mabienchung);

                    txt_tienluong_gan.Text = Utility.sDbnull(objPhieuKhamIvfVo.TienluongGan);
                    txt_tienluong_xa.Text = Utility.sDbnull(objPhieuKhamIvfVo.TienluongXa);
                    txt_huongdieutri.Text = Utility.sDbnull(objPhieuKhamIvfVo.Huongdieutri);


                    cboBacsi.SelectedValue = Utility.sDbnull(objPhieuKhamIvfVo.IdBacsidieutri, "-1");
                    //dtNgayKham.Value = Convert.ToDateTime(string.IsNullOrEmpty(objPhieuKhamIvfVo.NgayKham) ? dtNgayKham.Value : objPhieuKhamIvfVo.NgayKham);
                    dtNgayKham.Value = string.IsNullOrEmpty(objPhieuKhamIvfVo.NgayKham.ToString()) ? dtNgayKham.Value : Convert.ToDateTime(objPhieuKhamIvfVo.NgayKham);
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
                
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommmands();
            }
           
           
        }
        private void InitDanhmucchung()
        {
           DataTable dtData= THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { txtNhommau.LOAI_DANHMUC},true);
            txtNhommau.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtNhommau.LOAI_DANHMUC));
        }
        bool IsValidChucnangsong()
        {
            try
            {
                if (objLuotkham == null )
                {
                    Utility.ShowMsg("Bạn cần chọn một người bệnh trên danh sách phía bên trái màn hình để bắt đầu thực hiện khám");
                    return false;
                }
                if (Utility.Laygiatrithamsohethong("CANHBAO_CHUCNANGSONG", "0", true) == "1")
                {
                    decimal value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtMach.Text), -1);
                    List<string> lstRange = Utility.Laygiatrithamsohethong("MACH", "5-70", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtMach.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Mạch có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép từ {0}-{1}. Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtMach.Focus();
                    }
                    value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtNhietDo.Text), -1);
                    lstRange = Utility.Laygiatrithamsohethong("NHIETDO", "34-43", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtNhietDo.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Nhiệt độ có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép từ {0}-{1}. Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtNhietDo.Focus();
                    }
                    value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtha.Text), -1);
                    lstRange = Utility.Laygiatrithamsohethong("HUYETAP", "40-250", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtha.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Huyết áp có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép từ {0}-{1}. Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtha.Focus();
                    }
                    value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtNhipTho.Text), -1);
                    lstRange = Utility.Laygiatrithamsohethong("NHIPTHO", "40-250", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtNhipTho.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Nhịp thở có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép từ {0}-{1}. Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtNhipTho.Focus();
                    }
                    value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtChieuCao.Text), -1);
                    lstRange = Utility.Laygiatrithamsohethong("CHIEUCAO", "10-250", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtChieuCao.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Chiều cao có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép chiều cao từ {0}(cm)-{1}(cm). Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtChieuCao.Focus();
                    }
                    value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtCanNang.Text), -1);
                    lstRange = Utility.Laygiatrithamsohethong("CANNANG", "1-150", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtCanNang.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Cân nặng có thể chưa chuẩn xác. Hệ thống đang xác lập mức cân nặng từ {0}(kg)-{1}(kg). Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtCanNang.Focus();
                    }
                    //value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtMach.Text), -1);
                    //lstRange = Utility.Laygiatrithamsohethong("NHIPTIM", "40-130", true).Split('-').ToList<string>();
                    //if (Utility.DoTrim(txtMach.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    //{
                    //    Utility.ShowMsg(string.Format("Thông tin Nhịp tim có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép từ {0}(kg)-{1}(kg). Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                    //    txtMach.Focus();
                    //}
                    if (Utility.DoTrim(txtNhommau.Text).Length > 0 && txtNhommau.MyCode == "-1")
                    {
                        Utility.ShowMsg(string.Format("Sai thông tin nhóm máu. Yêu cầu nhập lại hoặc xóa trắng nếu không muốn nhập"), "Cảnh báo");
                        txtNhommau.Focus();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
        private void txtCanNang_Leave(object sender, EventArgs e)
        {
            if (Utility.DecimaltoDbnull(txtChieuCao.Text, 0) > 0 && Utility.DecimaltoDbnull(txtChieuCao.Text, 0) > 0)
            {
                if (!string.IsNullOrEmpty(txtCanNang.Text) && !string.IsNullOrEmpty(txtChieuCao.Text))
                {
                    decimal cannang = Utility.DecimaltoDbnull(txtCanNang.Text);
                    decimal chieucao = Utility.DecimaltoDbnull(txtChieuCao.Text);
                    decimal bmi = Utility.DecimaltoDbnull(cannang / ((chieucao / 100) * (chieucao / 100)));
                    txtBMI.Text = bmi.ToString("0.00").Replace(".00", String.Empty);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        void SaveData()
        {
            try
            {
                long ActID = -1;
                //Lấy lại dữ liệu lần nữa đề phòng có người khác dùng chính tài khoản tạo ra các thông tin này và thực hiện xóa trên máy khác
               
              
              
               
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        TaoPhieuDacdiemLienquanBenh();
                        
                        //Phiếu khám sản khoa
                        TaoPhieuKhamIVFVo();
                        ActID = objPhieuKhamIvfVo.Id;
                        //Hỏi bệnh
                        
                        //Lưu thông tin vào CSDL
                        objPhieuKhamIvfVo.Save();
                        objTsbDacdiemlienquan.Save();
                        Utility.Log(Name, globalVariables.UserName, string.Format(
                                              "Lưu thông tin phiếu khám IVF chồng cho người bệnh có mã lần khám {0} và ID bệnh nhân {1} ",
                                              objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan),
                                         ActID > 0? newaction.Update: newaction.Insert, this.GetType().Assembly.ManifestModule.Name);
                    }
                    scope.Complete();
                }
                Utility.ShowMsg("Bạn đã lưu thông tin khám thành công. Nhấn nút OK để kết thúc");
                if (chkCloseAfterSave.Checked)
                    this.Close();
                else
                    Utility.SetMsg(lblMsg, "Lưu thông tin thành công", false);

            }
            catch (Exception exception)
            {
                Utility.CatchException(string.Format("Lỗi trong quá trình Lưu thông tin khám"), exception);
                //throw;
            }
        }
        EmrTiensubenhDacdiemlienquan objTsbDacdiemlienquan;
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
            objTsbDacdiemlienquan.TsbMatuy = chkMaTuy.Checked;
            objTsbDacdiemlienquan.TsbRuoubia = chkRuouBia.Checked;
            objTsbDacdiemlienquan.TsbThuocla = chkThuocLa.Checked;
            objTsbDacdiemlienquan.TsbKhac = chkKhac.Checked;
            if (chkMaTuy.Checked) objTsbDacdiemlienquan.TsbThoigianMatuy = txtMaTuy.Text;
            else objTsbDacdiemlienquan.TsbThoigianMatuy = "";
            if (chkRuouBia.Checked) objTsbDacdiemlienquan.TsbThoigianRuoubia = txtRuouBia.Text;
            else objTsbDacdiemlienquan.TsbThoigianRuoubia = "";
            if (chkThuocLa.Checked) objTsbDacdiemlienquan.TsbThoigianThuocla = txtThuocLa.Text;
            else objTsbDacdiemlienquan.TsbThoigianThuocla = "";
            if (chkKhac.Checked) objTsbDacdiemlienquan.TsbThoigianKhac = txt_dacdiemlienquankhac.Text;
            else objTsbDacdiemlienquan.TsbThoigianKhac = "";
        }
       
        void TaoPhieuKhamIVFVo()
        {
            objPhieuKhamIvfVo = new Select().From(EmrPhieukhamIvfVo.Schema)
                .Where(EmrPhieukhamIvfVo.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrPhieukhamIvfVo.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteSingle<EmrPhieukhamIvfVo>();
            if (objPhieuKhamIvfVo != null && objPhieuKhamIvfVo.Id > 0)
            {
                objPhieuKhamIvfVo.MarkOld();
                objPhieuKhamIvfVo.NguoiSua = globalVariables.UserName;
                objPhieuKhamIvfVo.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
            }
            else
            {
                objPhieuKhamIvfVo = new EmrPhieukhamIvfVo();
                objPhieuKhamIvfVo.IsNew = true;
                objPhieuKhamIvfVo.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                objPhieuKhamIvfVo.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                objPhieuKhamIvfVo.NgayKham = dtNgayKham.Value.Date;
                objPhieuKhamIvfVo.NguoiTao = globalVariables.UserName;
                objPhieuKhamIvfVo.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
            }
            objPhieuKhamIvfVo.IdBacsidieutri = Utility.Int16Dbnull(cboBacsi.SelectedValue, -1);
           
            //Nội khoa
            objPhieuKhamIvfVo.HotenVo = Utility.sDbnull(txt_hoten_vo.Text);
            objPhieuKhamIvfVo.NgaysinhVo = dtp_ngaysinh_vo.Value;
            objPhieuKhamIvfVo.SodienthoaiVo = Utility.sDbnull(txt_sodienthoai_vo.Text);
            objPhieuKhamIvfVo.DiachiVo= Utility.sDbnull(txt_diachi_vo.Text);
            objPhieuKhamIvfVo.IdBacsitheodoiVo = Utility.Int16Dbnull(cbo_bacsitheodoi.SelectedValue, -1);
            //Tiền sử kinh nguyệt
            objPhieuKhamIvfVo.Tuoicokinhlandau = Utility.ByteDbnull(nmr_tuoicokinhlandau.Value);
            objPhieuKhamIvfVo.ChukykinhnguyetDeu = opt_chukykinhnguyet_deu.Checked;
            objPhieuKhamIvfVo.ChukykinhnguyetKhongdeu = opt_chukykinhnguyet_khongdeu.Checked;
            objPhieuKhamIvfVo.Ghichu = opt_chukykinhnguyet_khongdeu.Checked? Utility.sDbnull(txt_ghichu.Text):"";
            objPhieuKhamIvfVo.ChukykinhnguyetSongay= Utility.ByteDbnull(nmr_chukykinhnguyet_songay.Value);
            objPhieuKhamIvfVo.SongayCokinh = Utility.ByteDbnull(nmr_songay_cokinh.Value);
           
            objPhieuKhamIvfVo.SoluongkinhnguyetIt = opt_soluongkinhnguyet_it.Checked;
            objPhieuKhamIvfVo.SoluongkinhnguyetTrungbinh = opt_soluongkinhnguyet_trungbinh.Checked;
            objPhieuKhamIvfVo.SoluongkinhnguyetNhieu = opt_soluongkinhnguyet_nhieu.Checked;
            objPhieuKhamIvfVo.VokinhNguyenphat = opt_vokinh_nguyenphat.Checked;
            objPhieuKhamIvfVo.VokinhThuphat = opt_vokinh_thuphat.Checked;
            objPhieuKhamIvfVo.VokinhKhong = opt_vokinh_khong.Checked;

            objPhieuKhamIvfVo.VosinhNguyenphat = opt_vosinh_nguyenphat.Checked;
            objPhieuKhamIvfVo.VosinhThuphat = opt_vosinh_thuphat.Checked;
            objPhieuKhamIvfVo.VosinhKhong = opt_vosinh_khong.Checked;
            //Mức độ sinh hoạt tình dục
            objPhieuKhamIvfVo.MucdosinhhoattinhducTheotuan = Utility.ByteDbnull(nmr_mucdosinhhoattinhduc_theotuan.Value);
            objPhieuKhamIvfVo.CodaukhisinhhoatCo = opt_codaukhisinhhoat_co.Checked;
            objPhieuKhamIvfVo.CodaukhisinhhoatKhong = opt_codaukhisinhhoat_khong.Checked;
            //Tiền sử sinh sản
            objPhieuKhamIvfVo.SolancothaiVoichonghiennay = Utility.ByteDbnull(nmr_SolancothaiVoichonghiennay.Value);
            objPhieuKhamIvfVo.SolancothaiVoilankethontruoc = Utility.ByteDbnull(nmr_SolancothaiVoilankethontruoc.Value);
            objPhieuKhamIvfVo.SoconsongVoichonghiennay = Utility.ByteDbnull(nmr_SoconsongVoichonghiennay.Value);
            objPhieuKhamIvfVo.SoconsongVoilankethontruoc = Utility.ByteDbnull(nmr_SoconsongVoilankethontruoc.Value);
            objPhieuKhamIvfVo.DenonVoichonghiennay = Utility.ByteDbnull(nmr_DenonVoichonghiennay.Value);
            objPhieuKhamIvfVo.DenonVoilankethontruoc = Utility.ByteDbnull(nmr_DenonVoilankethontruoc.Value);
            objPhieuKhamIvfVo.SaythaiVoichonghiennay = Utility.ByteDbnull(nmr_SaythaiVoichonghiennay.Value);
            objPhieuKhamIvfVo.SaythaiVoilankethontruoc = Utility.ByteDbnull(nmr_SaythaiVoilankethontruoc.Value);
            objPhieuKhamIvfVo.NaohutVoichonghiennay = Utility.ByteDbnull(nmr_NaohutVoichonghiennay.Value);
            objPhieuKhamIvfVo.NaohutVoilankethontruoc = Utility.ByteDbnull(nmr_NaohutVoilankethontruoc.Value);
            objPhieuKhamIvfVo.ThailuuVoichonghiennay = Utility.ByteDbnull(nmr_ThailuuVoichonghiennay.Value);
            objPhieuKhamIvfVo.ThailuuVoilankethontruoc = Utility.ByteDbnull(nmr_ThailuuVoilankethontruoc.Value);


            objPhieuKhamIvfVo.GeuCo = opt_GEU_co.Checked;
            objPhieuKhamIvfVo.GeuKhong = opt_GEU_khong.Checked;
            objPhieuKhamIvfVo.ChuatrungCo = opt_chuatrung_co.Checked;
            objPhieuKhamIvfVo.ChuatrungKhong = opt_chuatrung_khong.Checked;

            objPhieuKhamIvfVo.Thoigianvosinh = Utility.ByteDbnull(nmr_thoigian_vosinh.Value);
            objPhieuKhamIvfVo.PhuongphapdieutrivosinhKhong = chk_phuongphapdieutrivosinh_khong.Checked;
            
            objPhieuKhamIvfVo.PhuongphapdieutrivosinhIui = chk_phuongphapdieutrivosinh_IUI.Checked;
            objPhieuKhamIvfVo.PhuongphapdieutrivosinhIuiMota = chk_phuongphapdieutrivosinh_IUI.Checked? Utility.sDbnull(txt_phuongphapdieutrivosinh_IUI_mota.Text):"";

            objPhieuKhamIvfVo.PhuongphapdieutrivosinhIvf = chk_phuongphapdieutrivosinh_IVF.Checked;
            objPhieuKhamIvfVo.PhuongphapdieutrivosinhIvfMota = chk_phuongphapdieutrivosinh_IVF.Checked? Utility.sDbnull(txt_phuongphapdieutrivosinh_IVF_mota.Text):"";

            objPhieuKhamIvfVo.PhuongphapdieutrivosinhKhac = chk_phuongphapdieutrivosinh_khac.Checked;
            objPhieuKhamIvfVo.PhuongphapdieutrivosinhKhacMota = chk_phuongphapdieutrivosinh_khac.Checked? Utility.sDbnull(txt_phuongphapdieutrivosinh_khac_mota.Text):"";
            // Các thăm dò trước đó
            //Chụp phim vòi trứng
            objPhieuKhamIvfVo.VoitrungphaiThong = chk_voitrungphai_thong.Checked;
            objPhieuKhamIvfVo.VoitrungphaiThonghanche = chk_voitrungphai_thonghanche.Checked;
            objPhieuKhamIvfVo.VoitrungphaiTacgan = chk_voitrungphai_tacgan.Checked;
            objPhieuKhamIvfVo.VoitrungphaiTacxa = chk_voitrungphai_tacxa.Checked;
            objPhieuKhamIvfVo.VoitrungphaiUnuoc = chk_voitrungphai_unuoc.Checked;
            objPhieuKhamIvfVo.VoitrungphaiKhac = chk_voitrungphai_khac.Checked;

            objPhieuKhamIvfVo.VoitrungtraiThong = chk_voitrungtrai_thong.Checked;
            objPhieuKhamIvfVo.VoitrungtraiThonghanche = chk_voitrungtrai_thonghanche.Checked;
            objPhieuKhamIvfVo.VoitrungtraiTacgan = chk_voitrungtrai_tacgan.Checked;
            objPhieuKhamIvfVo.VoitrungtraiTacxa = chk_voitrungtrai_tacxa.Checked;
            objPhieuKhamIvfVo.VoitrungtraiUnuoc = chk_voitrungtrai_unuoc.Checked;
            objPhieuKhamIvfVo.VoitrungtraiKhac = chk_voitrungtrai_khac.Checked;
            //Nội soi vòi trứng
            objPhieuKhamIvfVo.NoisoiVoitrungphaiThong = chk_noisoi_voitrungphai_thong.Checked;
            objPhieuKhamIvfVo.NoisoiVoitrungphaiThonghanche = chk_noisoi_voitrungphai_thonghanche.Checked;
            objPhieuKhamIvfVo.NoisoiVoitrungphaiTacgan = chk_noisoi_voitrungphai_tacgan.Checked;
            objPhieuKhamIvfVo.NoisoiVoitrungphaiTacxa = chk_noisoi_voitrungphai_tacxa.Checked;
            objPhieuKhamIvfVo.NoisoiVoitrungphaiUnuoc = chk_noisoi_voitrungphai_unuoc.Checked;
            objPhieuKhamIvfVo.NoisoiVoitrungphaiKhac = chk_noisoi_voitrungphai_khac.Checked;

            objPhieuKhamIvfVo.NoisoiVoitrungtraiThong = chk_noisoi_voitrungtrai_thong.Checked;
            objPhieuKhamIvfVo.NoisoiVoitrungtraiThonghanche = chk_noisoi_voitrungtrai_thonghanche.Checked;
            objPhieuKhamIvfVo.NoisoiVoitrungtraiTacgan = chk_noisoi_voitrungtrai_tacgan.Checked;
            objPhieuKhamIvfVo.NoisoiVoitrungtraiTacxa = chk_noisoi_voitrungtrai_tacxa.Checked;
            objPhieuKhamIvfVo.NoisoiVoitrungtraiUnuoc = chk_noisoi_voitrungtrai_unuoc.Checked;
            objPhieuKhamIvfVo.NoisoiVoitrungtraiKhac = chk_noisoi_voitrungtrai_khac.Checked;
           
            objPhieuKhamIvfVo.ChupphimTucungvoitrungGhichu= Utility.sDbnull(txt_chupphim_tucungvoitrung_ghichu.Text);
            objPhieuKhamIvfVo.NoisoiGhichu = Utility.sDbnull(txt_noisoi_ghichu.Text);
            //Siêu âm
            objPhieuKhamIvfVo.SisCo = opt_sis_co.Checked;
            objPhieuKhamIvfVo.SisKhong = opt_sis_khong.Checked;
            objPhieuKhamIvfVo.NgaySieuamSis = dtp_ngay_sieuam_sis.Value;
            objPhieuKhamIvfVo.KetquaSieuamSis = Utility.sDbnull(txt_ketqua_sieuam_sis.Text);
            //Các yếu tố khác có thể ảnh hưởng đến sinh sản
            
            objPhieuKhamIvfVo.Matuy = chkMaTuy.Checked;
            objPhieuKhamIvfVo.MatuyMota = chkMaTuy.Checked ? Utility.sDbnull(txtMaTuy.Text) : "";
            objPhieuKhamIvfVo.Ruoubia = chkRuouBia.Checked;
            objPhieuKhamIvfVo.RuoubiaMota = chkRuouBia.Checked ? Utility.sDbnull(txtRuouBia.Text) : "";
            objPhieuKhamIvfVo.Thuocla = chkThuocLa.Checked;
            objPhieuKhamIvfVo.ThuoclaMota = chkThuocLa.Checked ? Utility.sDbnull(txtThuocLa.Text) : "";
            //objPhieuKhamIvfVo.cacd = chkThuocLa.Checked;
            //objPhieuKhamIvfVo.ThuoclaMota = chkThuocLa.Checked ? Utility.sDbnull(txtThuocLa.Text) : "";
            //objPhieuKhamIvfVo.Thuocla = chkThuocLa.Checked;
            //objPhieuKhamIvfVo.ThuoclaMota = chkThuocLa.Checked ? Utility.sDbnull(txtThuocLa.Text) : "";
            //Tiền sử nội khoa
            objPhieuKhamIvfVo.TiensubenhAnhhuongdensinhsanCo = opt_tiensubenh_anhhuongdensinhsan_co.Checked;
            objPhieuKhamIvfVo.TiensubenhAnhhuongdensinhsanKhong = opt_tiensubenh_anhhuongdensinhsan_khong.Checked;
            objPhieuKhamIvfVo.Benhlaophoi = chk_benhlaophoi.Checked;
            objPhieuKhamIvfVo.Benhtieuduong = chk_benhtieuduong.Checked;
            objPhieuKhamIvfVo.Benhtuyengiap = chk_benhtuyengiap.Checked;
            objPhieuKhamIvfVo.Benhthankinh = chk_benhthankinh.Checked;
            objPhieuKhamIvfVo.TiensunoikhoaBenhkhac = chk_benhkhac.Checked;
            objPhieuKhamIvfVo.TiensungoaikhoaBenhkhacMota = chk_benhkhac.Checked? Utility.sDbnull(txt_noikhoabenhkhac_mota.Text):"";
            //Tiền sử ngoại khoa
            objPhieuKhamIvfVo.TiensungoaikhoaCo = opt_tiensungoaikhoa_co.Checked;
            objPhieuKhamIvfVo.TiensungoaikhoaKhong = opt_tiensungoaikhoa_khong.Checked;
            objPhieuKhamIvfVo.Geu = Utility.sDbnull(txt_GEU.Text);
            objPhieuKhamIvfVo.Catvoitrung = Utility.sDbnull(txt_catvoitrung.Text);
            objPhieuKhamIvfVo.Mothongvoi = Utility.sDbnull(txt_mothongvoi.Text);
            objPhieuKhamIvfVo.BoctachUnangBuongtrung = Utility.sDbnull(txt_boctach_unang_buongtrung.Text);
            objPhieuKhamIvfVo.CatUnangBuongtrung = Utility.sDbnull(txt_cat_unang_buongtrung.Text);
            objPhieuKhamIvfVo.Boctachnhanxotucung = Utility.sDbnull(txt_boctachnhanxotucung.Text);
            objPhieuKhamIvfVo.Vrt = Utility.sDbnull(txt_VRT.Text);
            objPhieuKhamIvfVo.Sinhmo = Utility.sDbnull(txt_sinhmo.Text);
            objPhieuKhamIvfVo.TiensungoaikhoaBenhkhacMota = Utility.sDbnull(txt_tiensungoaikhoa_benhkhac_mota.Text);
            // Tiền sử khám bệnh chậu, hông, núm vúi,dị ứng thuốc
            objPhieuKhamIvfVo.TiensucacbenhviemchauCo = opt_tiensucacbenhviemchau_co.Checked;
            objPhieuKhamIvfVo.TiensucacbenhviemchauKhong = opt_tiensucacbenhviemchau_khong.Checked;
            objPhieuKhamIvfVo.TiensucacbenhviemchauMota = opt_tiensucacbenhviemchau_co.Checked? Utility.sDbnull(txt_tiensucacbenhviemchau_mota.Text):"";

            objPhieuKhamIvfVo.TiensubenhlayquaduongtinhducCo = opt_tiensubenhlayquaduongtinhduc_co.Checked;
            objPhieuKhamIvfVo.TiensubenhlayquaduongtinhducKhong = opt_tiensubenhlayquaduongtinhduc_khong.Checked;
            objPhieuKhamIvfVo.TiensubenhlayquaduongtinhducLau = chk_tiensubenhlayquaduongtinhduc_lau.Checked;
            objPhieuKhamIvfVo.TiensubenhlayquaduongtinhducGiangmai = chk_tiensubenhlayquaduongtinhduc_giangmai.Checked;
            objPhieuKhamIvfVo.TiensubenhlayquaduongtinhducHiv = chk_tiensubenhlayquaduongtinhduc_hiv.Checked;
            objPhieuKhamIvfVo.TiensubenhlayquaduongtinhducChlamydia = chk_tiensubenhlayquaduongtinhduc_chlamydia.Checked;
            objPhieuKhamIvfVo.TiensubenhlayquaduongtinhducViemphanphu = chk_tiensubenhlayquaduongtinhduc_viemphanphu.Checked;
            objPhieuKhamIvfVo.TiensubenhlayquaduongtinhducLao = chk_tiensubenhlayquaduongtinhduc_lao.Checked;
            objPhieuKhamIvfVo.TiensubenhlayquaduongtinhducKhac = chk_tiensubenhlayquaduongtinhduc_khac.Checked;

            objPhieuKhamIvfVo.TiensutietDichnumvuCo = opt_tiensutiet_dichnumvu_co.Checked;
            objPhieuKhamIvfVo.TiensutietDichnumvuKhong = opt_tiensutiet_dichnumvu_khong.Checked;
            objPhieuKhamIvfVo.TiensutietDichnumvuMota = opt_tiensutiet_dichnumvu_co.Checked?Utility.sDbnull(txt_tiensutiet_dichnumvu_mota.Text):"";

            objPhieuKhamIvfVo.DiungthuocCo = opt_diungthuoc_co.Checked;
            objPhieuKhamIvfVo.DiungthuocKhong = opt_diungthuoc_khong.Checked;
            objPhieuKhamIvfVo.DiungthuocMota = opt_diungthuoc_co.Checked ? Utility.sDbnull(txt_diungthuoc_mota.Text) : "";


            objPhieuKhamIvfVo.TiensubenhlayquaduongtinhducMota = chk_tiensubenhlayquaduongtinhduc_khac.Checked? Utility.sDbnull(txt_tiensubenhlayquaduongtinhduc_mota.Text):"";
            //Khám thực thể
            objPhieuKhamIvfVo.KhamToanthanBinhthuong = opt_khamtoanthan_binhthuong.Checked;
            objPhieuKhamIvfVo.KhamToanthanBatthuong = opt_khamtoanthan_batthuong.Checked;
            objPhieuKhamIvfVo.KhamToanthanMota = opt_khamtoanthan_batthuong.Checked? Utility.sDbnull(txt_kham_toanthan_mota.Text):"";

            objPhieuKhamIvfVo.HethongLongBinhthuong = opt_hethong_long_binhthuong.Checked;
            objPhieuKhamIvfVo.HethongLongBatthuong = opt_hethong_long_batthuong.Checked;
            objPhieuKhamIvfVo.HethongLongMota = opt_hethong_long_batthuong.Checked? Utility.sDbnull(txt_hethong_long_mota.Text):"";

            objPhieuKhamIvfVo.PhattrienVuBatthuong = opt_phattrien_vu_batthuong.Checked;
            objPhieuKhamIvfVo.PhattrienVuBinhthuong = opt_phattrien_vu_binhthuong.Checked;
            objPhieuKhamIvfVo.PhattrienVuMota = opt_phattrien_vu_batthuong.Checked? Utility.sDbnull(txt_phattrien_vu_mota.Text):"";

            objPhieuKhamIvfVo.TietsuaCo = opt_tietsua_co.Checked;
            objPhieuKhamIvfVo.TietsuaKhong = opt_tietsua_khong.Checked;
            objPhieuKhamIvfVo.TietsuaMota = opt_tietsua_khong.Checked? Utility.sDbnull(txt_TietsuaMota.Text):"";

            objPhieuKhamIvfVo.ChieucaohongBatthuong = opt_chieucaohong_batthuong.Checked;
            objPhieuKhamIvfVo.ChieucaohongBinhthuong = opt_chieucaohong_binhthuong.Checked;
            objPhieuKhamIvfVo.ChieucaohongMota = opt_chieucaohong_batthuong.Checked? Utility.sDbnull(txt_chieucaohong_mota.Text):"";

            objPhieuKhamIvfVo.MangtrinhConnguyen = opt_mangtrinh_connguyen.Checked;
            objPhieuKhamIvfVo.MangtrinhRach = opt_mangtrinh_rach.Checked;
            objPhieuKhamIvfVo.MangtrinhMota = opt_mangtrinh_rach.Checked? Utility.sDbnull(txt_mangtrinh_mota.Text):"";

            objPhieuKhamIvfVo.AmdaoBinhthuong = opt_amdao_binhthuong.Checked;
            objPhieuKhamIvfVo.AmdaoBatthuong = opt_amdao_batthuong.Checked;
            objPhieuKhamIvfVo.AmdaoMota = opt_amdao_batthuong.Checked? Utility.sDbnull(txt_amdao_mota.Text):"";


            objPhieuKhamIvfVo.CotucungBinhthuong = chk_cotucung_binhthuong.Checked;
            objPhieuKhamIvfVo.CotucungHaitucung = chk_cotucung_haitucung.Checked;
            objPhieuKhamIvfVo.CotucungLotuyen = chk_cotucung_lotuyen.Checked;
            objPhieuKhamIvfVo.CotucungPolyp = chk_cotucung_polyp.Checked;

            objPhieuKhamIvfVo.CotucungSui = chk_cotucung_sui.Checked;
            objPhieuKhamIvfVo.CotucungViem = chk_cotucung_viem.Checked;

           
            objPhieuKhamIvfVo.Thetich = Utility.sDbnull(txt_thetich.Text);
            objPhieuKhamIvfVo.Matdo = Utility.sDbnull(txt_matdo.Text);
            objPhieuKhamIvfVo.Didong = Utility.sDbnull(txt_didong.Text);
            objPhieuKhamIvfVo.Tuthetucung = Utility.sDbnull(txt_tuthetucung.Text);

            objPhieuKhamIvfVo.HaiphanphuBatthuong = opt_haiphanphu_batthuong.Checked;
            objPhieuKhamIvfVo.HaiphanphuBinhthuong = opt_haiphanphu_binhthuong.Checked;
            objPhieuKhamIvfVo.CatheterDe = opt_catheter_de.Checked;
            objPhieuKhamIvfVo.CatheterKho = opt_catheter_kho.Checked;
            //Tóm tắt BA
            objPhieuKhamIvfVo.TomtatBenhan = Utility.sDbnull(txt_tomtat_benhan.Text);
            objPhieuKhamIvfVo.Chandoanphanbiet = Utility.sDbnull(txt_chandoanphanbiet.Text);
            objPhieuKhamIvfVo.MaChandoanphanbiet = Utility.sDbnull(txt_ma_chandoanphanbiet.Text);
            objPhieuKhamIvfVo.Chandoanvaovien = Utility.sDbnull(txt_chandoanvaovien.Text);
            objPhieuKhamIvfVo.MaChandoanvaovien = Utility.sDbnull(txt_ma_chandoanvaovien.Text);
            objPhieuKhamIvfVo.Chandoanxacdinh = Utility.sDbnull(txt_chandoanxacdinh.Text);
            objPhieuKhamIvfVo.MaChandoanxacdinh = Utility.sDbnull(txt_ma_chandoanxacdinh.Text);

            objPhieuKhamIvfVo.Tenbenhchinh = Utility.sDbnull(txt_tenbenhchinh.Text);
            objPhieuKhamIvfVo.Mabenhchinh = Utility.sDbnull(txt_mabenhchinh.Text);
            objPhieuKhamIvfVo.Tenbenhphu = Utility.sDbnull(txt_tenbenhphu.Text);
            objPhieuKhamIvfVo.Mabenhphu = Utility.sDbnull(txt_mabenhphu.Text);
            objPhieuKhamIvfVo.Tenbienchung = Utility.sDbnull(txt_tenbienchung.Text);
            objPhieuKhamIvfVo.Mabienchung = Utility.sDbnull(txt_mabienchung.Text);
           
            objPhieuKhamIvfVo.TienluongGan = Utility.sDbnull(txt_tienluong_gan.Text);
            objPhieuKhamIvfVo.TienluongXa = Utility.sDbnull(txt_tienluong_xa.Text);
            objPhieuKhamIvfVo.Huongdieutri = Utility.sDbnull(txt_huongdieutri.Text);
           


            //Chức năng sống
            objPhieuKhamIvfVo.NhomMau = txtNhommau.myCode;
            objPhieuKhamIvfVo.HuyetAp = txtha.Text;
            objPhieuKhamIvfVo.NhietDo = txtNhietDo.Text;
            objPhieuKhamIvfVo.Mach = Utility.sDbnull(txtMach.Text);
            objPhieuKhamIvfVo.NhịpTho = Utility.sDbnull(txtNhipTho.Text);
            objPhieuKhamIvfVo.ChieuCao = Utility.sDbnull(txtChieuCao.Text);
            objPhieuKhamIvfVo.CanNang = Utility.sDbnull(txtCanNang.Text);
            objPhieuKhamIvfVo.Bmi = Utility.sDbnull(txtBMI.Text);
            
        }    
       
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void cmdDiungkhac_Click(object sender, EventArgs e)
        {
            frm_Tiensubenh_Cacdacdiemlienquan _Tiensubenh_Cacdacdiemlienquan = new frm_Tiensubenh_Cacdacdiemlienquan(objLuotkham);
            _Tiensubenh_Cacdacdiemlienquan.ShowDialog();
        }

       

        private void chkMaTuy_CheckedChanged(object sender, EventArgs e)
        {
            txtMaTuy.Enabled = chkMaTuy.Checked;
        }

        private void chkThuocLa_CheckedChanged(object sender, EventArgs e)
        {
            txtThuocLa.Enabled = chkThuocLa.Checked;
        }

        private void chkRuouBia_CheckedChanged(object sender, EventArgs e)
        {
            txtRuouBia.Enabled = chkRuouBia.Checked;
        }

        private void chkKhac_CheckedChanged(object sender, EventArgs e)
        {
            txt_dacdiemlienquankhac.Enabled = chkKhac.Checked;
        }

        private void chk_benhkhac_CheckedChanged(object sender, EventArgs e)
        {
            txt_noikhoabenhkhac_mota.Enabled = chk_benhkhac.Checked;
        }

        private void txt_tiensupttt_mota_TextChanged(object sender, EventArgs e)
        {

        }

        private void chk_tiensubenhlayquaduongtinhduc_khac_CheckedChanged(object sender, EventArgs e)
        {
            txt_tiensubenhlayquaduongtinhduc_mota.Enabled = chk_tiensubenhlayquaduongtinhduc_khac.Checked;
        }

        private void chkDiUng_CheckedChanged(object sender, EventArgs e)
        {
            txt_diungthuoc_mota.Enabled = chkDiUng.Checked;
            txt_diungthuoc_mota.Focus();
        }

        private void chkMaTuy_CheckedChanged_1(object sender, EventArgs e)
        {
            txtMaTuy.Enabled = chkMaTuy.Checked;
            txtMaTuy.Focus();
        }

        private void chkRuouBia_CheckedChanged_1(object sender, EventArgs e)
        {
            txtRuouBia.Enabled = chkRuouBia.Checked;
            txtRuouBia.Focus();
        }

        private void chkThuocLa_CheckedChanged_1(object sender, EventArgs e)
        {
            txtThuocLa.Enabled = chkThuocLa.Checked;
            txtThuocLa.Focus();
        }

        private void chkThuocLao_CheckedChanged(object sender, EventArgs e)
        {
            txtThuocLao.Enabled = chkThuocLao.Checked;
            txtThuocLao.Focus();

        }

        private void chkKhac_CheckedChanged_1(object sender, EventArgs e)
        {
            txt_dacdiemlienquankhac.Enabled = chkKhac.Checked;
            txt_dacdiemlienquankhac.Focus();
        }

        private void opt_chukykinhnguyet_khongdeu_CheckedChanged(object sender, EventArgs e)
        {
            txt_ghichu.Enabled = opt_chukykinhnguyet_khongdeu.Checked;
            txt_ghichu.Focus();
            txt_ghichu.Focus();
        }

        private void chk_phuongphapdieutrivosinh_IVF_CheckedChanged(object sender, EventArgs e)
        {
            txt_phuongphapdieutrivosinh_IVF_mota.Enabled = chk_phuongphapdieutrivosinh_IVF.Checked;
            txt_phuongphapdieutrivosinh_IVF_mota.Focus();
        }

        private void chk_phuongphapdieutrivosinh_IUI_CheckedChanged(object sender, EventArgs e)
        {
            txt_phuongphapdieutrivosinh_IUI_mota.Enabled = chk_phuongphapdieutrivosinh_IUI.Checked;
            txt_phuongphapdieutrivosinh_IUI_mota.Focus();
        }

        private void chk_phuongphapdieutrivosinh_khac_CheckedChanged(object sender, EventArgs e)
        {
            txt_phuongphapdieutrivosinh_khac_mota.Enabled = chk_phuongphapdieutrivosinh_khac.Checked;
            txt_phuongphapdieutrivosinh_khac_mota.Focus();
        }

        private void chk_benhkhac_CheckedChanged_1(object sender, EventArgs e)
        {
            txt_noikhoabenhkhac_mota.Enabled = chk_benhkhac.Checked;
            txt_noikhoabenhkhac_mota.Focus();
        }

        private void opt_tiensucacbenhviemchau_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_tiensucacbenhviemchau_mota.Enabled = opt_tiensucacbenhviemchau_co.Checked;
            txt_tiensucacbenhviemchau_mota.Focus();
        }

        private void opt_tiensutiet_dichnumvu_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_tiensutiet_dichnumvu_mota.Enabled = opt_tiensutiet_dichnumvu_co.Checked;
            txt_tiensutiet_dichnumvu_mota.Focus();
        }

        private void opt_diungthuoc_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_diungthuoc_mota.Enabled = opt_diungthuoc_co.Checked;
            txt_diungthuoc_mota.Focus();
        }

        private void chk_tiensubenhlayquaduongtinhduc_khac_CheckedChanged_1(object sender, EventArgs e)
        {
            txt_tiensubenhlayquaduongtinhduc_mota.Enabled = chk_tiensubenhlayquaduongtinhduc_khac.Checked;
            txt_tiensubenhlayquaduongtinhduc_mota.Focus();
        }

        private void opt_khamtoanthan_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            txt_kham_toanthan_mota.Enabled = opt_khamtoanthan_batthuong.Checked;
            txt_kham_toanthan_mota.Focus();
        }

        private void opt_hethong_long_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            txt_hethong_long_mota.Enabled = opt_hethong_long_batthuong.Checked;
            txt_hethong_long_mota.Focus();
        }

        private void opt_phattrien_vu_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            txt_phattrien_vu_mota.Enabled = opt_phattrien_vu_batthuong.Checked;
            txt_phattrien_vu_mota.Focus();
        }

        private void opt_tietsua_khong_CheckedChanged(object sender, EventArgs e)
        {
            txt_TietsuaMota.Enabled = opt_tietsua_khong.Checked;
            txt_TietsuaMota.Focus();
        }

        private void opt_chieucaohong_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            txt_chieucaohong_mota.Enabled = opt_chieucaohong_batthuong.Checked;
            txt_chieucaohong_mota.Focus();
        }

        private void opt_mangtrinh_rach_CheckedChanged(object sender, EventArgs e)
        {
            txt_mangtrinh_mota.Enabled = opt_mangtrinh_rach.Checked;
            txt_mangtrinh_mota.Focus();
        }

        private void opt_amdao_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            txt_amdao_mota.Enabled = opt_amdao_batthuong.Checked;
            txt_amdao_mota.Focus();
        }
    }
}
