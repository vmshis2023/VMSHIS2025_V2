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
            if(objBenhnhan==null)
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
                if (globalVariables.IsAdmin || objPhieuKhamIvfChong.NguoiTao == globalVariables.UserName.ToString())
                {
                    objPhieuKhamIvfChong = EmrPhieukhamIvfChong.FetchByID(objPhieuKhamIvfChong.Id);
                    if (objPhieuKhamIvfChong != null)
                    {
                        if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa thông tin khám chữa bệnh ngày {0} của bác sĩ {1} thực hiện", "Cảnh báo", true))
                        {
                            EmrPhieukhamIvfChong.Delete(objPhieuKhamIvfChong.Id);
                        }
                    }
                    else
                    {
                        Utility.ShowMsg(string.Format("Không thể xóa phiếu khám IVF chồng.\nVui lòng kiểm tra lại vì có thể trong lúc bạn mở thao tác người khác đã xóa thông tin", objPhieuKhamIvfChong.NguoiTao));
                    }    
                }
                else
                {
                    Utility.ShowMsg(string.Format("Bạn không thể xóa thông tin khám được tạo bởi bác sĩ {0}.\nVui lòng kiểm tra lại", objPhieuKhamIvfChong.NguoiTao));
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
            cmdxoa.Enabled = cmdIn.Enabled = objPhieuKhamIvfChong != null;
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
                objPhieuKhamIvfChong= new Select().From(EmrPhieukhamIvfChong.Schema)
                   .Where(EmrPhieukhamIvfChong.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(EmrPhieukhamIvfChong.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<EmrPhieukhamIvfChong>();
              
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
            ucThongtinnguoibenh1.Refresh();
            dtNgayKham.Value = DateTime.Now.Date;
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
        EmrPhieukhamIvfChong objPhieuKhamIvfChong = null;
      
        private void FillData()
        {
            try
            {
                FillDacdiemLienquan();
                if (objPhieuKhamIvfChong != null)
                {
                    txtID.Text = objPhieuKhamIvfChong.Id.ToString();
                    txtNhietDo.Text = objPhieuKhamIvfChong.NhietDo;
                    txtha.Text = objPhieuKhamIvfChong.NhomMau;
                    txtMach.Text = objPhieuKhamIvfChong.Mach;
                    txtNhipTho.Text = objPhieuKhamIvfChong.NhịpTho;
                    txtChieuCao.Text = objPhieuKhamIvfChong.ChieuCao;
                    txtCanNang.Text = objPhieuKhamIvfChong.CanNang;
                    txtBMI.Text = objPhieuKhamIvfChong.Bmi;
                    txtNhommau.SetCode(objPhieuKhamIvfChong.NhomMau);
                    //Tiền sử nội khoa
                    txt_hoten_vo.Text = Utility.sDbnull(objPhieuKhamIvfChong.HotenChong);
                    dtp_ngaysinh_vo.Value = objPhieuKhamIvfChong.NgaythangnamsinhChong.Value;
                    txt_sodienthoai_vo.Text=Utility.sDbnull(objPhieuKhamIvfChong.SodienthoaiChong);
                    cbo_bacsitheodoi.SelectedValue = objPhieuKhamIvfChong.IdBacsitheodoiChong;

                    opt_vosinh_nguyenphat.Checked= Utility.Bool2Bool(objPhieuKhamIvfChong.VosinhNguyenphat);
                    opt_vosinh_thuphat.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.VosinhThuphat);
                    nmr_thoigian_vosinh.Value = Utility.Int32Dbnull(objPhieuKhamIvfChong.ThoigianVosinh);

                    opt_tiensubenhanhhuongdensinhsan_co.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.TiensubenhanhhuongdensinhsanCo);
                    opt_tiensubenhanhhuongdensinhsan_khong.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.TiensubenhanhhuongdensinhsanKhong);
                    //Các bệnh toàn thân
                    chk_benhtieuduong.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.Benhtieuduong);
                    chk_benhlaophoi.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.Benhlaophoi);
                    chk_benhtuyengiap.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.Benhtuyengiap);
                    chk_benhthankinh.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.Benhthankinh);
                    chk_benhkhac.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.Benhkhac);
                    txt_noikhoabenhkhac_mota.Text = Utility.sDbnull(objPhieuKhamIvfChong.BenhkhacMota);
                    //Tiền sử điều trị nội khoa
                    opt_codaukhisinhhoat_co.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.TiensudieutrinoikhoaCo); 
                    opt_codaukhisinhhoat_khong.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.TiensudieutrinoikhoaKhong); 
                    txt_tiensudieutrinoikhoa_mota.Text = Utility.sDbnull(objPhieuKhamIvfChong.TiensudieutrinoikhoaMota);
                    //Tiền sử PTTT
                    chk_tiensupttt_khong.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.TiensuptttKhong);
                    chk_hepnieudao.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.Hepnieudao);
                    chk_lotietnieuthap.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.Lotietnieuthap);
                    chk_thoatviben.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.Thoatviben);
                    chk_catbotinhhoan.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.Catbotinhhoan);
                    chk_thatongdantinh.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.Thatongdantinh);
                    chk_tiensupttt_khac.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.TiensuptttKhac);
                    txt_tiensupttt_mota.Text = Utility.sDbnull(objPhieuKhamIvfChong.TiensuptttMota);
                    //Tiền sử nhiễm trùng đường tiết niệu
                    opt_tiensunhiemtrungduongtietnieu_co.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.TiensunhiemtrungduongtietnieuCo); 
                    opt_tiensunhiemtrungduongtietnieu_khong.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.TiensunhiemtrungduongtietnieuKhong); 
                    //Tiền sử bệnh lây lan qua đường tình dục
                    chk_tiensubenhlayquaduongtinhduc_co.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.TiensubenhlayquaduongtinhducCo);
                    chk_tiensubenhlayquaduongtinhduc_lau.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.TiensubenhlayquaduongtinhducLau); 
                    chk_tiensubenhlayquaduongtinhduc_giangmai.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.TiensubenhlayquaduongtinhducGiangmai); 
                    chk_tiensubenhlayquaduongtinhduc_hiv.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.TiensubenhlayquaduongtinhducHiv); 
                    chk_tiensubenhlayquaduongtinhduc_chlamydia.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.TiensubenhlayquaduongtinhducChlamydia); 
                    chk_tiensubenhlayquaduongtinhduc_khac.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.TiensubenhlayquaduongtinhducKhac); 
                    txt_tiensubenhlayquaduongtinhduc_mota.Text = Utility.sDbnull(objPhieuKhamIvfChong.TiensubenhlayquaduongtinhducMota);
                    
                    //KHÁM THỰC THỂ
                    opt_khamtoanthan_binhthuong.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.KhamtoanthanBinhthuong); 
                    opt_khamtoanthan_batthuong.Checked = Utility.Bool2Bool(objPhieuKhamIvfChong.KhamtoanthanBatthuong);

                    txt_khamtoanthan_mota.Text = Utility.sDbnull(objPhieuKhamIvfChong.KhamtoanthanMota);
                    //Khám đường tiết niệu sinh dục
                    txt_duongvat.Text = Utility.sDbnull(objPhieuKhamIvfChong.Duongvat);
                    txt_khamtructrang.Text = Utility.sDbnull(objPhieuKhamIvfChong.Khamtructrang);
                    txt_tuitinh.Text = Utility.sDbnull(objPhieuKhamIvfChong.Tuitinh);
                    txt_sungbiu.Text = Utility.sDbnull(objPhieuKhamIvfChong.Sungbiu);
                    txt_tuyentienliet.Text = Utility.sDbnull(objPhieuKhamIvfChong.Tuyentienliet);
                    txt_GEU.Text = Utility.sDbnull(objPhieuKhamIvfChong.TinhoanPhai);
                    txt_tinhoan_trai.Text = Utility.sDbnull(objPhieuKhamIvfChong.TinhoanTrai);
                    txt_catvoitrung.Text = Utility.sDbnull(objPhieuKhamIvfChong.ThetichPhai);
                    txt_thetich_trai.Text = Utility.sDbnull(objPhieuKhamIvfChong.ThetichTrai);
                    txt_mothongvoi.Text = Utility.sDbnull(objPhieuKhamIvfChong.MaotinhoanPhai);
                    txt_maotinhoan_trai.Text = Utility.sDbnull(objPhieuKhamIvfChong.MaotinhoanTrai);
                    txt_boctach_unang_buongtrung.Text = Utility.sDbnull(objPhieuKhamIvfChong.OngdantinhPhai);
                    txt_ongdantinh_trai.Text = Utility.sDbnull(objPhieuKhamIvfChong.OngdantinhTrai);
                    txt_cat_unang_buongtrung.Text = Utility.sDbnull(objPhieuKhamIvfChong.GiantinhmachthungtinhPhai);
                    txt_giantinhmachthungtinh_trai.Text = Utility.sDbnull(objPhieuKhamIvfChong.GiantinhmachthungtinhTrai);
                    txt_boctachnhanxotucung.Text = Utility.sDbnull(objPhieuKhamIvfChong.BenPhai);
                    txt_ben_trai.Text = Utility.sDbnull(objPhieuKhamIvfChong.BenTrai);
                   

                    cboBacsi.SelectedValue = Utility.sDbnull(objPhieuKhamIvfChong.IdBacsi, "-1");
                    //dtNgayKham.Value = Convert.ToDateTime(string.IsNullOrEmpty(objPhieuKhamIvfChong.NgayKham) ? dtNgayKham.Value : objPhieuKhamIvfChong.NgayKham);
                    dtNgayKham.Value = string.IsNullOrEmpty(objPhieuKhamIvfChong.NgayKham.ToString()) ? dtNgayKham.Value : Convert.ToDateTime(objPhieuKhamIvfChong.NgayKham);
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
                        TaoPhieuKhamIVFChong();
                        ActID = objPhieuKhamIvfChong.Id;
                        //Hỏi bệnh
                        
                        //Lưu thông tin vào CSDL
                        objPhieuKhamIvfChong.Save();
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
       
        void TaoPhieuKhamIVFChong()
        {
            objPhieuKhamIvfChong = new Select().From(EmrPhieukhamIvfChong.Schema)
                .Where(EmrPhieukhamIvfChong.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrPhieukhamIvfChong.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteSingle<EmrPhieukhamIvfChong>();
            if (objPhieuKhamIvfChong != null && objPhieuKhamIvfChong.Id > 0)
            {
                objPhieuKhamIvfChong.MarkOld();
                objPhieuKhamIvfChong.NguoiSua = globalVariables.UserName;
                objPhieuKhamIvfChong.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
            }
            else
            {
                objPhieuKhamIvfChong = new EmrPhieukhamIvfChong();
                objPhieuKhamIvfChong.IsNew = true;
                objPhieuKhamIvfChong.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                objPhieuKhamIvfChong.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                objPhieuKhamIvfChong.NgayKham = dtNgayKham.Value.Date;
                objPhieuKhamIvfChong.NguoiTao = globalVariables.UserName;
                objPhieuKhamIvfChong.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
            }
            objPhieuKhamIvfChong.IdBacsi = Utility.Int16Dbnull(cboBacsi.SelectedValue, -1);
           
            //Nội khoa
            objPhieuKhamIvfChong.HotenChong = Utility.sDbnull(txt_hoten_vo.Text);
            objPhieuKhamIvfChong.NgaythangnamsinhChong = dtp_ngaysinh_vo.Value;
            objPhieuKhamIvfChong.SodienthoaiChong = Utility.sDbnull(txt_sodienthoai_vo.Text);
            objPhieuKhamIvfChong.DiachiChong= Utility.sDbnull(txt_diachi_vo.Text);
            objPhieuKhamIvfChong.IdBacsitheodoiChong = Utility.Int16Dbnull(cbo_bacsitheodoi.SelectedValue, -1);


            objPhieuKhamIvfChong.VosinhNguyenphat = opt_vosinh_nguyenphat.Checked;
            objPhieuKhamIvfChong.VosinhThuphat = opt_vosinh_thuphat.Checked;
            objPhieuKhamIvfChong.ThoigianVosinh = Utility.ByteDbnull(nmr_thoigian_vosinh.Value);
            objPhieuKhamIvfChong.TiensubenhanhhuongdensinhsanCo = opt_tiensubenhanhhuongdensinhsan_co.Checked;
            objPhieuKhamIvfChong.TiensubenhanhhuongdensinhsanKhong = opt_tiensubenhanhhuongdensinhsan_khong.Checked;
           //Bệnh toàn thân
            objPhieuKhamIvfChong.Benhtieuduong = chk_benhtieuduong.Checked;
            objPhieuKhamIvfChong.Benhlaophoi = chk_benhlaophoi.Checked;
            objPhieuKhamIvfChong.Benhtuyengiap = chk_benhtuyengiap.Checked;
            objPhieuKhamIvfChong.Benhthankinh = chk_benhthankinh.Checked;
            objPhieuKhamIvfChong.Benhkhac = chk_benhkhac.Checked;
            objPhieuKhamIvfChong.BenhkhacMota = chk_benhkhac.Checked? Utility.sDbnull(txt_noikhoabenhkhac_mota.Text):"";
            //tiền sử nội khoa
            objPhieuKhamIvfChong.TiensudieutrinoikhoaCo = opt_codaukhisinhhoat_co.Checked;
            objPhieuKhamIvfChong.TiensudieutrinoikhoaKhong = opt_codaukhisinhhoat_khong.Checked;
            objPhieuKhamIvfChong.TiensudieutrinoikhoaMota = opt_codaukhisinhhoat_co.Checked? Utility.sDbnull(txt_tiensudieutrinoikhoa_mota.Text):"";
            //Tiền sử pttt

            objPhieuKhamIvfChong.TiensuptttCo = !chk_tiensupttt_khong.Checked;
            objPhieuKhamIvfChong.TiensuptttKhong = chk_tiensupttt_khong.Checked;

            objPhieuKhamIvfChong.Hepnieudao = chk_hepnieudao.Checked;
            objPhieuKhamIvfChong.Lotietnieuthap = chk_lotietnieuthap.Checked;

            objPhieuKhamIvfChong.Thoatviben = chk_thoatviben.Checked;
            objPhieuKhamIvfChong.Catbotinhhoan = chk_catbotinhhoan.Checked;
            objPhieuKhamIvfChong.Thatongdantinh = chk_thatongdantinh.Checked;

            objPhieuKhamIvfChong.TiensuptttKhac = chk_tiensupttt_khac.Checked;
            objPhieuKhamIvfChong.TiensuptttMota = chk_tiensupttt_khac.Checked? Utility.sDbnull(txt_tiensupttt_mota.Text):"";
            //tiền sử bệnh lây qua đường tiết niệu
            objPhieuKhamIvfChong.TiensunhiemtrungduongtietnieuCo = opt_tiensunhiemtrungduongtietnieu_co.Checked;
            objPhieuKhamIvfChong.TiensunhiemtrungduongtietnieuKhong = opt_tiensunhiemtrungduongtietnieu_khong.Checked;
            //tiền sử bệnh lây qua đường tình dục
            objPhieuKhamIvfChong.TiensubenhlayquaduongtinhducCo = chk_tiensubenhlayquaduongtinhduc_co.Checked;
            objPhieuKhamIvfChong.TiensubenhlayquaduongtinhducLau = chk_tiensubenhlayquaduongtinhduc_lau.Checked;
            objPhieuKhamIvfChong.TiensubenhlayquaduongtinhducGiangmai = chk_tiensubenhlayquaduongtinhduc_giangmai.Checked;
            objPhieuKhamIvfChong.TiensubenhlayquaduongtinhducHiv = chk_tiensubenhlayquaduongtinhduc_hiv.Checked;
            objPhieuKhamIvfChong.TiensubenhlayquaduongtinhducChlamydia = chk_tiensubenhlayquaduongtinhduc_chlamydia.Checked;
            objPhieuKhamIvfChong.TiensubenhlayquaduongtinhducKhac = chk_tiensubenhlayquaduongtinhduc_khac.Checked;
            objPhieuKhamIvfChong.TiensubenhlayquaduongtinhducMota = chk_tiensubenhlayquaduongtinhduc_khac.Checked? Utility.sDbnull(txt_tiensubenhlayquaduongtinhduc_mota.Text):"";
           
            //Khám thực thể


            objPhieuKhamIvfChong.KhamtoanthanBinhthuong = opt_khamtoanthan_binhthuong.Checked;
            objPhieuKhamIvfChong.KhamtoanthanBatthuong = opt_khamtoanthan_batthuong.Checked;
            objPhieuKhamIvfChong.KhamtoanthanMota = opt_khamtoanthan_batthuong.Checked? Utility.sDbnull(txt_khamtoanthan_mota.Text):"";
            //Khám đường niệu sinh dục
            objPhieuKhamIvfChong.Duongvat = Utility.sDbnull(txt_duongvat.Text);
            objPhieuKhamIvfChong.Khamtructrang = Utility.sDbnull(txt_khamtructrang.Text);
            objPhieuKhamIvfChong.Tuitinh = Utility.sDbnull(txt_tuitinh.Text);
            objPhieuKhamIvfChong.Sungbiu = Utility.sDbnull(txt_sungbiu.Text);
            objPhieuKhamIvfChong.Tuyentienliet = Utility.sDbnull(txt_tuyentienliet.Text);
            //Khám các bộ phận sinh dục trái, phải
            objPhieuKhamIvfChong.TinhoanPhai = Utility.sDbnull(txt_GEU.Text);
            objPhieuKhamIvfChong.TinhoanTrai = Utility.sDbnull(txt_tinhoan_trai.Text);
            objPhieuKhamIvfChong.ThetichPhai = Utility.sDbnull(txt_catvoitrung.Text);
            objPhieuKhamIvfChong.ThetichTrai = Utility.sDbnull(txt_thetich_trai.Text);
            objPhieuKhamIvfChong.MaotinhoanPhai = Utility.sDbnull(txt_mothongvoi.Text);
            objPhieuKhamIvfChong.MaotinhoanTrai = Utility.sDbnull(txt_maotinhoan_trai.Text);
            objPhieuKhamIvfChong.OngdantinhPhai = Utility.sDbnull(txt_boctach_unang_buongtrung.Text);
            objPhieuKhamIvfChong.OngdantinhTrai = Utility.sDbnull(txt_ongdantinh_trai.Text);
            objPhieuKhamIvfChong.GiantinhmachthungtinhPhai = Utility.sDbnull(txt_cat_unang_buongtrung.Text);
            objPhieuKhamIvfChong.GiantinhmachthungtinhTrai = Utility.sDbnull(txt_giantinhmachthungtinh_trai.Text);
            objPhieuKhamIvfChong.BenPhai = Utility.sDbnull(txt_boctachnhanxotucung.Text);
            objPhieuKhamIvfChong.BenTrai = Utility.sDbnull(txt_ben_trai.Text);
            
            //Chức năng sống
            objPhieuKhamIvfChong.NhomMau = txtNhommau.myCode;
            objPhieuKhamIvfChong.HuyetAp = txtha.Text;
            objPhieuKhamIvfChong.NhietDo = txtNhietDo.Text;
            objPhieuKhamIvfChong.Mach = Utility.sDbnull(txtMach.Text);
            objPhieuKhamIvfChong.NhịpTho = Utility.sDbnull(txtNhipTho.Text);
            objPhieuKhamIvfChong.ChieuCao = Utility.sDbnull(txtChieuCao.Text);
            objPhieuKhamIvfChong.CanNang = Utility.sDbnull(txtCanNang.Text);
            objPhieuKhamIvfChong.Bmi = Utility.sDbnull(txtBMI.Text);
            
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

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            txt_tiensupttt_mota.Enabled = chk_tiensupttt_khac.Checked;
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
    }
}
