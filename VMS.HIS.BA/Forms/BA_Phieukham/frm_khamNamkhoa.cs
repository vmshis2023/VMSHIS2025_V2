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
    public partial class frm_khamNamkhoa : Form
    {
        public KcbLuotkham objLuotkham;
        KcbDanhsachBenhnhan objBenhnhan;
        DataTable dt_tssk;
        bool AllowedChanged = false;
        action m_enAct = action.FirstOrFinished;
        public frm_khamNamkhoa(KcbLuotkham objLuotkham, KcbDanhsachBenhnhan objBenhnhan)
        {
            InitializeComponent();
            this.KeyDown += frm_khamNamkhoa_KeyDown;
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
                if (globalVariables.IsAdmin || objPhieukhamNamkhoa.NguoiTao == globalVariables.UserName.ToString())
                {
                    objPhieukhamNamkhoa = EmrPhieukhamNamkhoa.FetchByID(objPhieukhamNamkhoa.Id);
                    if (objPhieukhamNamkhoa != null)
                    {
                        if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa thông tin khám chữa bệnh ngày {0} của bác sĩ {1} thực hiện", "Cảnh báo", true))
                        {
                            EmrPhieukhamNamkhoa.Delete(objPhieukhamNamkhoa.Id);
                        }
                    }
                    else
                    {
                        Utility.ShowMsg(string.Format("Không thể xóa phiếu khám Nam khoa.\nVui lòng kiểm tra lại vì có thể trong lúc bạn mở thao tác người khác đã xóa thông tin", objPhieukhamNamkhoa.NguoiTao));
                    }    
                }
                else
                {
                    Utility.ShowMsg(string.Format("Bạn không thể xóa thông tin khám được tạo bởi bác sĩ {0}.\nVui lòng kiểm tra lại", objPhieukhamNamkhoa.NguoiTao));
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
            txt_toanthan.Enabled = txt_hach.Enabled = txt_tuanhoan.Enabled = txt_tieuhoa.Enabled = txt_hohap.Enabled = txt_thankinh.Enabled = txt_coxuongkhop.Enabled = txt_thantietnieu_sinhduc.Enabled = txt_khac.Enabled = txt_ranghammat.Enabled = objPKB == null || (objPKB != null && objPKB.NguoiTao == globalVariables.UserName);
            cmdxoa.Enabled = cmdIn.Enabled = objPhieukhamNamkhoa != null;
        }
        void frm_khamNamkhoa_KeyDown(object sender, KeyEventArgs e)
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
        EmrPhieukhambenh objPKB;
        void ucThongtinnguoibenh1__OnEnterMe()
        {
            if (ucThongtinnguoibenh1.objLuotkham != null)
            {
                objLuotkham = ucThongtinnguoibenh1.objLuotkham;
                this.Text = string.Format("Phiếu khám sản khoa cho người bệnh {0} - {1} - {2} -{3}", ucThongtinnguoibenh1.txtTenBN.Text, ucThongtinnguoibenh1.txtgioitinh.Text, ucThongtinnguoibenh1.txttuoi.Text, ucThongtinnguoibenh1.txtDiachi.Text);
                objPKB = new Select().From(EmrPhieukhambenh.Schema)
                   .Where(EmrPhieukhambenh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(EmrPhieukhambenh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<EmrPhieukhambenh>();
                objPhieukhamNamkhoa= new Select().From(EmrPhieukhamNamkhoa.Schema)
                   .Where(EmrPhieukhamNamkhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(EmrPhieukhamNamkhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<EmrPhieukhamNamkhoa>();
              
                FillData();
            }
            else
            {
                ClearControls();
                this.Text = "Phiếu khám Nam khoa";
            }    
        }
       
       
        private void frm_khamNamkhoa_Load(object sender, EventArgs e)
        {
            InitDanhmucchung();
            DataBinding.BindDataCombobox(cboBacsi, globalVariables.gv_dtDmucNhanvien.Copy(),
                                     DmucNhanvien.Columns.UserName, DmucNhanvien.Columns.TenNhanvien, "----Chọn bác sĩ khám----", true);
            ucThongtinnguoibenh1.Refresh();
            dtNgayKham.Value = DateTime.Now.Date;
            dtNgayKham.Focus();
            ModifyCommmands();
           
        }
      
        EmrPhieukhamNamkhoa objPhieukhamNamkhoa = null;
      
        private void FillData()
        {
            try
            {
                if (objPhieukhamNamkhoa != null)
                {
                    txtID.Text = objPhieukhamNamkhoa.Id.ToString();
                    txtNhietDo.Text = objPhieukhamNamkhoa.NhietDo;
                    txtha.Text = objPhieukhamNamkhoa.NhomMau;
                    txtMach.Text = objPhieukhamNamkhoa.Mach;
                    txtNhipTho.Text = objPhieukhamNamkhoa.NhịpTho;
                    txtChieuCao.Text = objPhieukhamNamkhoa.ChieuCao;
                    txtCanNang.Text = objPhieukhamNamkhoa.CanNang;
                    txtBMI.Text = objPhieukhamNamkhoa.Bmi;
                    txtNhommau.SetCode(objPhieukhamNamkhoa.NhomMau);
                    //Tiền sử nội khoa
                    txt_benhly_toanthan.Text = Utility.sDbnull(objPhieukhamNamkhoa.BenhlyToanthan);

                    opt_quaibi_co.Checked= Utility.Bool2Bool(objPhieukhamNamkhoa.QuaibiCo);
                    opt_quaibi_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.QuaibiKhong);
                    opt_bienchungtinhhoan_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.BienchungtinhhoanCo);
                    opt_bienchungtinhhoan_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.BienchungtinhhoanKhong);
                    opt_bienchungtinhhoan_1ben.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.Bienchungtinhhoan1ben);
                    opt_bienchungtinhhoan_2ben.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.Bienchungtinhhoan2ben);
                    txt_bienchungtinhhoan_mota.Text = Utility.sDbnull(objPhieukhamNamkhoa.BienchungtinhoanMota);

                    opt_benhxahoi_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.BenhxahoiCo);
                    opt_benhxahoi_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.BenhxahoiKhong);
                    txt_benhxahoi_mota.Text = Utility.sDbnull(objPhieukhamNamkhoa.BenhxahoiMota);

                    opt_ungthu_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.UngthuCo);
                    opt_ungthu_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.UngthuKhong);
                    txt_ungthu_mota.Text = Utility.sDbnull(objPhieukhamNamkhoa.UngthuMota);

                    opt_tiencanlao_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.TiencanlaoCo);
                    opt_tiencanlao_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.TiencanlaoKhong);

                    opt_sudungtestosteron_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.SudungTestosteronCo);
                    opt_sudungtestosteron_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.SudungTestosteronKhong);
                    txt_testosteron_mota.Text = Utility.sDbnull(objPhieukhamNamkhoa.SudungTestosteronMota);

                    txt_noikhoa_khac.Text = Utility.sDbnull(objPhieukhamNamkhoa.NoikhoaKhac);
                    txt_thuocdangdieutri.Text = Utility.sDbnull(objPhieukhamNamkhoa.Thuocdangdieutri);
                    //Ngoại khoa
                    opt_viphauthuatthatTMT_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.ViphaucothattmtCo);
                    opt_viphauthuatthatTMT_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.ViphaucothattmtKhong);

                    opt_hatinhoanan_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.HatinhoananCo);
                    opt_hatinhoanan_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.HatinhoananKhong);
                    txt_hatinhhoan_mota.Text = Utility.sDbnull(objPhieukhamNamkhoa.HatinhoananMota);

                    opt_thatongdantinh_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.ThatongdantinhCo);
                    opt_thatongdantinh_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.ThatongdantinhKhong);
                    txt_thatongdantinh_mota.Text = Utility.sDbnull(objPhieukhamNamkhoa.ThatongdantinhThoigian);
                    txt_ngoaikhoa_khac.Text = Utility.sDbnull(objPhieukhamNamkhoa.NgoaikhoaKhac);
                    //Quan hệ tình dục
                    txtTansuatquanhetinhduc.Text = Utility.sDbnull(objPhieukhamNamkhoa.QuanhetinhducTansuat);
                    opt_roiloancuongduong_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.RoiloancuongCo);
                    opt_roiloancuongduong_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.RoiloancuongKhong);
                    txt_roiloancuongduong_mota.Text = Utility.sDbnull(objPhieukhamNamkhoa.RoiloancuongMota);

                    chk_xuattinh_som.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.XuattinhsomTruockhixamnhap);
                    chk_xuattinh_sau.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.XuattinhsomSaukhixamnhap);
                    chk_xuattinh_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.XuattinhsomKhong);

                    opt_cuckhoai_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.CuckhoaiCo);
                    opt_cuckhoai_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.CuckhoaiKhong);
                    opt_sudungchatboitron_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.CosudungchatboitronCo);
                    opt_sudungchatboitron_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.CosudungchatboitronKhong);

                    txt_chatboitron_mota.Text = Utility.sDbnull(objPhieukhamNamkhoa.CosudungchatboitronMota);
                    //Khám chuyên khoa
                    //Tinh hoàn
                    txt_tinhoan_thetich_phai.Text = Utility.sDbnull(objPhieukhamNamkhoa.ThetichtinhhoanPhai);
                    txt_tinhoan_thetich_trai.Text = Utility.sDbnull(objPhieukhamNamkhoa.ThetichtinhhoanTrai);
                    txt_matdotinhoan_phai.Text = Utility.sDbnull(objPhieukhamNamkhoa.MatdotinhhoanPhai);
                    txt_matdotinhoan_trai.Text = Utility.sDbnull(objPhieukhamNamkhoa.MatdotinhhoanTrai);
                    opt_matdotinhhoanphai_chac.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MatdotinhhoanPhaiChac);
                    opt_matdotinhhoanphai_mem.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MatdotinhhoanPhaiMem);
                    opt_matdotinhhoantrai_chac.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MatdotinhhoanTraiChac);
                    opt_matdotinhhoantrai_mem.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MatdotinhhoanTraiMem);
                    //Mào tinh
                    opt_matdomaotinhphai_mem.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MatdotinhhoanPhaiMem);
                    opt_matdotinhhoantrai_chac.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MatdotinhhoanTraiChac);
                    opt_matdomaotinhtrai_mem.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MatdotinhhoanTraiMem);

                    txt_bemattinhhoan_phai.Text = Utility.sDbnull(objPhieukhamNamkhoa.BemattinhoanPhai);
                    txt_bemattinhhoan_trai.Text = Utility.sDbnull(objPhieukhamNamkhoa.BemattinhoanTrai);
                    //Mào tinh
                    txt_matdomaotinh_phai.Text = Utility.sDbnull(objPhieukhamNamkhoa.MatdomaotinhPhai);
                    txt_matdomaotinh_trai.Text = Utility.sDbnull(objPhieukhamNamkhoa.MatdomaotinhTrai);
                    opt_matdomaotinhphai_chac.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MatdomaotinhPhaiChac);
                    opt_matdomaotinhtrai_chac.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MatdomaotinhTraiChac);
                    opt_matdomaotinhphai_mem.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MatdomaotinhPhaiMem);
                    opt_matdomaotinhtrai_mem.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MatdomaotinhTraiMem);
                    //Nang
                    opt_maotinh_nangphai_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MaotinhNangphaiCo);
                    opt_maotinh_nangphai_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MaotinhNangphaiKhong);
                    opt_maotinh_nangphai_khongxacdinh.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MaotinhNangphaiKhongxacdinh);
                    
                    opt_maotinh_nangtrai_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MaotinhNangtraiCo);
                    opt_maotinh_nangtrai_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MaotinhNangtraiKhong);
                    opt_maotinh_nangtrai_khongxacdinh.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MaotinhNangtraiKhongxacdinh);

                    //Ống dẫn tinh đoạn trong bầu
                    opt_ongdantinhdoantrongbauphai_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.OngdantinhPhaiCo);
                    opt_ongdantinhdoantrongbauphai_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.OngdantinhPhaiKhong);
                    opt_ongdantinhdoantrongbauphai_khongro.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.OngdantinhPhaiKhongro);

                    opt_ongdantinhdoantrongbautrai_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.OngdantinhTraiCo);
                    opt_ongdantinhdoantrongbautrai_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.OngdantinhTraiKhong);
                    opt_ongdantinhdoantrongbautrai_khongro.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.OngdantinhTraiKhongro);

                    //Tĩnh mạch thừng tinh
                    opt_tinhmachthungtinh_gianphai_1.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.TinhmachthungtingGianphai1);
                    opt_tinhmachthungtinh_gianphai_2.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.TinhmachthungtingGianphai2);
                    opt_tinhmachthungtinh_gianphai_3.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.TinhmachthungtingGianphai3);
                    chk_tinhmachthungtinhphai_binhthuong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.TinhmachthungtingTraiBinhthuong);
                   

                    opt_tinhmachthungtinh_giantrai_1.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.TinhmachthungtingGiantrai1);
                    opt_tinhmachthungtinh_giantrai_2.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.TinhmachthungtingGiantrai2);
                    opt_tinhmachthungtinh_giantrai_3.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.TinhmachthungtingGiantrai3);
                    chk_tinhmachthungtinhtrai_binhthuong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.TinhmachthungtingTraiBinhthuong);
                    //Đặc điểm sinh dục thứ phát
                    opt_phanboco_binhthuong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.PhanbocoBinhthuong);
                    opt_phanboco_batthuong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.PhanbocoBatthuong);
                    txt_phanbomo.Text = Utility.sDbnull(objPhieukhamNamkhoa.PhanboMo);
                    txt_longmu.Text = Utility.sDbnull(objPhieukhamNamkhoa.PhanboLongmu);
                    txt_chi.Text = Utility.sDbnull(objPhieukhamNamkhoa.PhanboChi);

                    cboBacsi.SelectedValue = Utility.sDbnull(objPhieukhamNamkhoa.IdBacsi, "-1");
                    //dtNgayKham.Value = Convert.ToDateTime(string.IsNullOrEmpty(objPhieukhamNamkhoa.NgayKham) ? dtNgayKham.Value : objPhieukhamNamkhoa.NgayKham);
                    dtNgayKham.Value = string.IsNullOrEmpty(objPhieukhamNamkhoa.NgayKham.ToString()) ? dtNgayKham.Value : Convert.ToDateTime(objPhieukhamNamkhoa.NgayKham);
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
                //Khám bộ phận
                if(objPKB!=null)
                {
                    txt_toanthan.Text = objPKB.ToanThan;
                    txt_hach.Text = objPKB.Hach;
                    txt_tuanhoan.Text = objPKB.Tuanhoan;
                    txt_tieuhoa.Text = objPKB.Tieuhoa;
                    txt_hohap.Text = objPKB.Hohap;
                    txt_thankinh.Text = objPKB.Thankinh;

                    txt_coxuongkhop.Text = objPKB.Coxuongkhop;
                    txt_thantietnieu_sinhduc.Text = objPKB.Thantietnieusinhduc;
                    txt_taimuihong.Text = objPKB.Taimuihong;
                    txt_ranghammat.Text = objPKB.Ranghammat;
                    txt_khac.Text = objPKB.Noitietdinhduongbenhlykhac;
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
                        TaoPhieuKCB();
                        
                        //Phiếu khám sản khoa
                        TaoPhieuKhamNamkhoa();
                        ActID = objPhieukhamNamkhoa.Id;
                        //Hỏi bệnh
                        
                        //Lưu thông tin vào CSDL
                        objPhieukhamNamkhoa.Save();
                        if (txt_toanthan.Enabled)
                            objPKB.Save();
                        Utility.Log(Name, globalVariables.UserName, string.Format(
                                              "Lưu thông tin phiếu khám Nam khoa cho người bệnh có mã lần khám {0} và ID bệnh nhân {1} ",
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
        void TaoPhieuKCB()
        {
            objPKB = new Select().From(EmrPhieukhambenh.Schema)
                 .Where(EmrPhieukhambenh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                 .And(EmrPhieukhambenh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                 .ExecuteSingle<EmrPhieukhambenh>();
            //Phiếu khám bệnh toàn thân
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
                objPKB.NgayKham = dtNgayKham.Value.Date;
                objPKB.NguoiTao = globalVariables.UserName;
                objPKB.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
            }
            objPKB.Hach = Utility.sDbnull(txt_hach.Text);
            objPKB.ToanThan = Utility.sDbnull(txt_toanthan.Text);
            objPKB.Tuanhoan = Utility.sDbnull(txt_tuanhoan.Text);
            objPKB.Hohap = Utility.sDbnull(txt_hohap.Text);
            objPKB.Tieuhoa = Utility.sDbnull(txt_tieuhoa.Text);
            objPKB.Thankinh = Utility.sDbnull(txt_thankinh.Text);
            objPKB.Coxuongkhop = Utility.sDbnull(txt_coxuongkhop.Text);
            objPKB.Thantietnieusinhduc = Utility.sDbnull(txt_thantietnieu_sinhduc.Text);
            objPKB.Taimuihong = Utility.sDbnull(txt_taimuihong.Text);
            objPKB.Ranghammat = Utility.sDbnull(txt_ranghammat.Text);
            objPKB.Noitietdinhduongbenhlykhac = Utility.sDbnull(txt_khac.Text);
            

        }
        void TaoPhieuKhamNamkhoa()
        {
            objPhieukhamNamkhoa = new Select().From(EmrPhieukhamNamkhoa.Schema)
                .Where(EmrPhieukhamNamkhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrPhieukhamNamkhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteSingle<EmrPhieukhamNamkhoa>();
            if (objPhieukhamNamkhoa != null && objPhieukhamNamkhoa.Id > 0)
            {
                objPhieukhamNamkhoa.MarkOld();
                objPhieukhamNamkhoa.NguoiSua = globalVariables.UserName;
                objPhieukhamNamkhoa.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
            }
            else
            {
                objPhieukhamNamkhoa = new EmrPhieukhamNamkhoa();
                objPhieukhamNamkhoa.IsNew = true;
                objPhieukhamNamkhoa.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                objPhieukhamNamkhoa.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                objPhieukhamNamkhoa.NgayKham = dtNgayKham.Value.Date;
                objPhieukhamNamkhoa.NguoiTao = globalVariables.UserName;
                objPhieukhamNamkhoa.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
            }
            objPhieukhamNamkhoa.IdBacsi = Utility.Int16Dbnull(cboBacsi.SelectedValue, -1);

            //Nội khoa
            objPhieukhamNamkhoa.BenhlyToanthan = Utility.sDbnull(txt_benhly_toanthan.Text);
            objPhieukhamNamkhoa.QuaibiCo = opt_quaibi_co.Checked;
            objPhieukhamNamkhoa.QuaibiKhong = opt_quaibi_khong.Checked;
            objPhieukhamNamkhoa.BienchungtinhhoanCo = opt_bienchungtinhhoan_co.Checked;
            objPhieukhamNamkhoa.BienchungtinhhoanKhong = opt_bienchungtinhhoan_khong.Checked;
            objPhieukhamNamkhoa.Bienchungtinhhoan1ben = opt_bienchungtinhhoan_1ben.Checked;
            objPhieukhamNamkhoa.Bienchungtinhhoan2ben = opt_bienchungtinhhoan_2ben.Checked;
            objPhieukhamNamkhoa.BienchungtinhoanMota= opt_bienchungtinhhoan_co.Checked ? Utility.sDbnull(txt_bienchungtinhhoan_mota.Text):"";

            objPhieukhamNamkhoa.BenhxahoiCo = opt_benhxahoi_co.Checked;
            objPhieukhamNamkhoa.BenhxahoiKhong = opt_benhxahoi_khong.Checked;
            objPhieukhamNamkhoa.BenhxahoiMota = opt_benhxahoi_co.Checked? Utility.sDbnull(txt_benhxahoi_mota.Text):"";

            objPhieukhamNamkhoa.UngthuCo = opt_ungthu_co.Checked;
            objPhieukhamNamkhoa.UngthuKhong = opt_ungthu_khong.Checked;
            objPhieukhamNamkhoa.UngthuMota = opt_ungthu_co.Checked? Utility.sDbnull(txt_ungthu_mota.Text):"";

            objPhieukhamNamkhoa.TiencanlaoCo = opt_tiencanlao_co.Checked;
            objPhieukhamNamkhoa.TiencanlaoKhong = opt_tiencanlao_khong.Checked;

            objPhieukhamNamkhoa.SudungTestosteronCo = opt_sudungtestosteron_co.Checked;
            objPhieukhamNamkhoa.SudungTestosteronKhong = opt_sudungtestosteron_khong.Checked;
            objPhieukhamNamkhoa.SudungTestosteronMota = opt_sudungtestosteron_co.Checked? Utility.sDbnull(txt_testosteron_mota.Text):"";

            objPhieukhamNamkhoa.NoikhoaKhac = Utility.sDbnull(txt_noikhoa_khac.Text);
            objPhieukhamNamkhoa.Thuocdangdieutri = Utility.sDbnull(txt_thuocdangdieutri.Text);
            //Ngoại khoa
            objPhieukhamNamkhoa.ViphaucothattmtCo = opt_viphauthuatthatTMT_co.Checked;
            objPhieukhamNamkhoa.ViphaucothattmtKhong = opt_viphauthuatthatTMT_khong.Checked;

            objPhieukhamNamkhoa.HatinhoananCo = opt_hatinhoanan_co.Checked;
            objPhieukhamNamkhoa.HatinhoananKhong = opt_hatinhoanan_khong.Checked;
            objPhieukhamNamkhoa.HatinhoananMota = opt_hatinhoanan_co.Checked? Utility.sDbnull(txt_hatinhhoan_mota.Text):"";

            objPhieukhamNamkhoa.ThatongdantinhCo = opt_thatongdantinh_co.Checked;
            objPhieukhamNamkhoa.ThatongdantinhKhong = opt_thatongdantinh_khong.Checked;
            objPhieukhamNamkhoa.ThatongdantinhThoigian = Utility.sDbnull(txt_thatongdantinh_mota.Text);
            objPhieukhamNamkhoa.NgoaikhoaKhac = Utility.sDbnull(txt_ngoaikhoa_khac.Text);
            //Quan hệ tình dục
            objPhieukhamNamkhoa.QuanhetinhducTansuat = Utility.sDbnull(txtTansuatquanhetinhduc.Text);
            objPhieukhamNamkhoa.RoiloancuongCo = opt_roiloancuongduong_co.Checked;
            objPhieukhamNamkhoa.RoiloancuongKhong = opt_roiloancuongduong_khong.Checked;
            objPhieukhamNamkhoa.RoiloancuongMota = opt_roiloancuongduong_co.Checked?Utility.sDbnull(txt_roiloancuongduong_mota.Text):"";
           
            objPhieukhamNamkhoa.XuattinhsomTruockhixamnhap = chk_xuattinh_som.Checked;
            objPhieukhamNamkhoa.XuattinhsomSaukhixamnhap = chk_xuattinh_sau.Checked;
            objPhieukhamNamkhoa.XuattinhsomKhong = chk_xuattinh_khong.Checked;

            objPhieukhamNamkhoa.CuckhoaiCo = opt_cuckhoai_co.Checked;
            objPhieukhamNamkhoa.CuckhoaiKhong = opt_cuckhoai_khong.Checked;

            objPhieukhamNamkhoa.CosudungchatboitronCo = opt_sudungchatboitron_co.Checked;
            objPhieukhamNamkhoa.CosudungchatboitronKhong = opt_sudungchatboitron_khong.Checked;
            objPhieukhamNamkhoa.CosudungchatboitronMota = opt_sudungchatboitron_co.Checked? Utility.sDbnull(txt_chatboitron_mota.Text):"";
            //Khám chuyên khoa
            objPhieukhamNamkhoa.ThetichtinhhoanPhai = Utility.sDbnull(txt_tinhoan_thetich_phai.Text);
            objPhieukhamNamkhoa.MatdotinhhoanPhai = Utility.sDbnull(txt_matdotinhoan_phai.Text);
            objPhieukhamNamkhoa.MatdotinhhoanPhaiChac = opt_matdotinhhoanphai_chac.Checked;
            objPhieukhamNamkhoa.MatdotinhhoanPhaiMem = opt_matdotinhhoanphai_mem.Checked;
            objPhieukhamNamkhoa.BemattinhoanPhai = Utility.sDbnull(txt_bemattinhhoan_phai.Text);

            objPhieukhamNamkhoa.ThetichtinhhoanTrai = Utility.sDbnull(txt_tinhoan_thetich_trai.Text);
            objPhieukhamNamkhoa.MatdotinhhoanTrai = Utility.sDbnull(txt_matdotinhoan_trai.Text);
            objPhieukhamNamkhoa.MatdotinhhoanTraiChac = opt_matdotinhhoantrai_chac.Checked;
            objPhieukhamNamkhoa.MatdotinhhoanTraiMem = opt_matdotinhhoantrai_mem.Checked;
            objPhieukhamNamkhoa.BemattinhoanTrai = Utility.sDbnull(txt_bemattinhhoan_trai.Text);
            //Mào tinh
            objPhieukhamNamkhoa.MatdomaotinhPhai = Utility.sDbnull(txt_matdomaotinh_phai.Text);
            objPhieukhamNamkhoa.MatdomaotinhPhaiChac = opt_matdomaotinhphai_chac.Checked;
            objPhieukhamNamkhoa.MatdomaotinhPhaiMem = opt_matdomaotinhphai_mem.Checked;

            objPhieukhamNamkhoa.MatdomaotinhTrai = Utility.sDbnull(txt_matdomaotinh_trai.Text);
            objPhieukhamNamkhoa.MatdomaotinhTraiChac = opt_matdomaotinhtrai_chac.Checked;
            objPhieukhamNamkhoa.MatdomaotinhTraiMem = opt_matdomaotinhtrai_mem.Checked;
            //Mào tinh Nang
            objPhieukhamNamkhoa.MaotinhNangphaiCo = opt_maotinh_nangphai_co.Checked;
            objPhieukhamNamkhoa.MaotinhNangphaiKhong = opt_maotinh_nangphai_khong.Checked;
            objPhieukhamNamkhoa.MaotinhNangphaiKhongxacdinh = opt_maotinh_nangphai_khongxacdinh.Checked;

            objPhieukhamNamkhoa.MaotinhNangtraiCo = opt_maotinh_nangtrai_co.Checked;
            objPhieukhamNamkhoa.MaotinhNangtraiKhong = opt_maotinh_nangtrai_khong.Checked;
            objPhieukhamNamkhoa.MaotinhNangtraiKhongxacdinh = opt_maotinh_nangtrai_khongxacdinh.Checked;
            //Ống dẫn tinh đoạn trong bầu
            objPhieukhamNamkhoa.OngdantinhPhaiCo = opt_ongdantinhdoantrongbauphai_co.Checked;
            objPhieukhamNamkhoa.OngdantinhPhaiKhong = opt_ongdantinhdoantrongbauphai_khong.Checked;
            objPhieukhamNamkhoa.OngdantinhPhaiKhongro = opt_ongdantinhdoantrongbauphai_khongro.Checked;

            objPhieukhamNamkhoa.OngdantinhTraiCo = opt_ongdantinhdoantrongbautrai_co.Checked;
            objPhieukhamNamkhoa.OngdantinhTraiKhong = opt_ongdantinhdoantrongbautrai_khong.Checked;
            objPhieukhamNamkhoa.OngdantinhTraiKhongro = opt_ongdantinhdoantrongbautrai_khongro.Checked;
            //Tĩnh mạch thừng tinh
            objPhieukhamNamkhoa.TinhmachthungtingPhaiBinhthuong = chk_tinhmachthungtinhphai_binhthuong.Checked;
            objPhieukhamNamkhoa.TinhmachthungtingGianphai1 = opt_tinhmachthungtinh_gianphai_1.Checked;
            objPhieukhamNamkhoa.TinhmachthungtingGianphai2 = opt_tinhmachthungtinh_gianphai_2.Checked;
            objPhieukhamNamkhoa.TinhmachthungtingGianphai3 = opt_tinhmachthungtinh_gianphai_3.Checked;
            objPhieukhamNamkhoa.TinhmachthungtingTraiBinhthuong = chk_tinhmachthungtinhtrai_binhthuong.Checked;
            objPhieukhamNamkhoa.TinhmachthungtingGiantrai1 = opt_tinhmachthungtinh_giantrai_1.Checked;
            objPhieukhamNamkhoa.TinhmachthungtingGiantrai2 = opt_tinhmachthungtinh_giantrai_2.Checked;
            objPhieukhamNamkhoa.TinhmachthungtingGiantrai3 = opt_tinhmachthungtinh_giantrai_3.Checked;
            //Đặc điểm sinh dục thứ phát

            objPhieukhamNamkhoa.PhanbocoBinhthuong = opt_phanboco_binhthuong.Checked;
            objPhieukhamNamkhoa.PhanbocoBatthuong = opt_phanboco_batthuong.Checked;

            objPhieukhamNamkhoa.PhanboMo = Utility.sDbnull(txt_phanbomo.Text);
            objPhieukhamNamkhoa.PhanboLongmu = Utility.sDbnull(txt_longmu.Text);
            objPhieukhamNamkhoa.PhanboChi = Utility.sDbnull(txt_chi.Text);
            //Chức năng sống
            objPhieukhamNamkhoa.NhomMau = txtNhommau.myCode;
            objPhieukhamNamkhoa.HuyetAp = txtha.Text;
            objPhieukhamNamkhoa.NhietDo = txtNhietDo.Text;
            objPhieukhamNamkhoa.Mach = Utility.sDbnull(txtMach.Text);
            objPhieukhamNamkhoa.NhịpTho = Utility.sDbnull(txtNhipTho.Text);
            objPhieukhamNamkhoa.ChieuCao = Utility.sDbnull(txtChieuCao.Text);
            objPhieukhamNamkhoa.CanNang = Utility.sDbnull(txtCanNang.Text);
            objPhieukhamNamkhoa.Bmi = Utility.sDbnull(txtBMI.Text);
            
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

        private void opt_hatinhoanan_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_hatinhhoan_mota.Enabled = opt_hatinhoanan_co.Checked;
            txt_hatinhhoan_mota.Focus();
        }

        private void opt_thatongdantinh_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_thatongdantinh_mota.Enabled = opt_thatongdantinh_co.Checked;
            txt_thatongdantinh_mota.Focus();
        }

        private void opt_roiloancuongduong_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_roiloancuongduong_mota.Enabled = opt_roiloancuongduong_co.Checked;
            txt_roiloancuongduong_mota.Focus();
        }

        private void opt_sudungchatboitron_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_chatboitron_mota.Enabled = opt_sudungchatboitron_co.Checked;
            txt_chatboitron_mota.Focus();
        }

        private void opt_bienchungtinhhoan_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_bienchungtinhhoan_mota.Enabled = opt_bienchungtinhhoan_co.Checked;
            txt_bienchungtinhhoan_mota.Focus();
        }

        private void opt_benhxahoi_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_benhxahoi_mota.Enabled = opt_benhxahoi_co.Checked;
            txt_benhxahoi_mota.Focus();
        }

        private void opt_ungthu_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_ungthu_mota.Enabled = opt_ungthu_co.Checked;
            txt_ungthu_mota.Focus();
        }

        private void opt_sudungtestosteron_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_testosteron_mota.Enabled = opt_sudungtestosteron_co.Checked;
            txt_testosteron_mota.Focus();
        }
    }
}
