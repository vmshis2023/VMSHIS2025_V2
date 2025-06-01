using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.Libs;
using SubSonic;
using System.Diagnostics;
using System.IO;
using Aspose.Words;
using VNS.HIS.BusRule.Classes;

namespace VMS.HIS.Danhmuc.UserControls
{
    public partial class uc_khamthiluc : UserControl
    {
        KcbLuotkham objLuotkham;
        KcbPhieukhamThiluc objKhamthiluc;
        KcbDanhsachBenhnhan objBenhnhan;
        KcbDangkyKcb objCongkham;
        public uc_khamthiluc()
        {
            InitializeComponent();
            this.Load += uc_khamthiluc_Load;
            this.KeyDown += Uc_khamthiluc_KeyDown;
        }

        private void Uc_khamthiluc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.ActiveControl != null && !this.ActiveControl.GetType().Equals(cbo_donkinh_thiluc_cokinh_phai.GetType()))
            {
                SendKeys.Send("{TAB}");
            }
        }
        void NapPhongkhamThiluc()
        {
            DataTable dtPKTL = SPs.BvmKcbLayphongdothiluc().GetDataSet().Tables[0];
            DataBinding.BindDataCombobox(cboPhongHtai, dtPKTL, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong);
            DataBinding.BindDataCombobox(cboPhongkhamThiluc, dtPKTL.Copy(), DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong);
            cboPhongHtai.SelectedValue = objCongkham.IdPhongkham;
        }
        public void ClearData(bool isReset)
        {
            if (isReset)
            {
                objLuotkham = null;
                objBenhnhan = null;
                objCongkham = null;
            }
            foreach(Control ctrl in this.Controls)
            {
                if (ctrl.GetType().Equals(cbo_khucxahientai_thiluc_cokinh_phai.GetType()))
                {
                    ((VNS.HIS.UCs.EasyCompletionComboBox)ctrl).SelectedValue = -1;
                }
                else if (ctrl.GetType().Equals(txt_biendo_dieutiet_phai.GetType()))
                {
                    ((Janus.Windows.GridEX.EditControls.EditBox)ctrl).Clear();
                }
            }

            modifyCommands();
        }
        public void SetData(KcbLuotkham objLuotkham, KcbDanhsachBenhnhan objBenhnhan,KcbDangkyKcb objCongkham)
        {
            this.objBenhnhan = objBenhnhan;
            this.objLuotkham = objLuotkham;
            this.objCongkham = objCongkham;
            try
            {
                NapPhongkhamThiluc();
                dtpNgaykham.Value = DateTime.Now;
                dtpThoigian_batdau.Value = DateTime.Now.AddSeconds(60);
                dtpThoigianKetthuc.Value = DateTime.Now.AddSeconds(60);
                DataTable dtDmucThiluc = THU_VIEN_CHUNG.LayDulieuDanhmucChung("DANHSACHTHILUC", true);
                DataRow dr = dtDmucThiluc.NewRow();
                dr[DmucChung.Columns.Ma] = "";
                dr[DmucChung.Columns.Ten] = "";
                dtDmucThiluc.Rows.InsertAt(dr, 0);
                DataBinding.BindDataCombobox(cbo_donkinh_thiluc_cokinh_phai, dtDmucThiluc.Copy(), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                DataBinding.BindDataCombobox(cbo_donkinh_thiluc_cokinh_trai, dtDmucThiluc.Copy(), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                DataBinding.BindDataCombobox(cbo_khucxahientai_thiluc_cokinh_phai, dtDmucThiluc.Copy(), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                DataBinding.BindDataCombobox(cbo_khucxahientai_thiluc_cokinh_trai, dtDmucThiluc.Copy(), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                DataBinding.BindDataCombobox(cbo_khucxahientai_thiluc_khongkinh_phai, dtDmucThiluc.Copy(), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                DataBinding.BindDataCombobox(cbo_khucxahientai_thiluc_khongkinh_trai, dtDmucThiluc.Copy(), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                DataBinding.BindDataCombobox(cbo_khucxahientai_thiluc_kinhlo_phai, dtDmucThiluc.Copy(), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                DataBinding.BindDataCombobox(cbo_khucxahientai_thiluc_kinhlo_trai, dtDmucThiluc.Copy(), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                DataBinding.BindDataCombobox(cbo_khucxasaudieutiet_thiluc_cokinh_phai, dtDmucThiluc.Copy(), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                DataBinding.BindDataCombobox(cbo_khucxasaudieutiet_thiluc_cokinh_trai, dtDmucThiluc.Copy(), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                DataBinding.BindDataCombobox(cbo_khucxasaudieutiet_thiluc_khongkinh_phai, dtDmucThiluc.Copy(), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                DataBinding.BindDataCombobox(cbo_khucxasaudieutiet_thiluc_khongkinh_trai, dtDmucThiluc.Copy(), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                DataBinding.BindDataCombobox(cbo_kinhcu_thiluc_cokinh_phai, dtDmucThiluc.Copy(), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                DataBinding.BindDataCombobox(cbo_kinhcu_thiluc_cokinh_trai, dtDmucThiluc.Copy(), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                txtBacsi.Enabled = false;
                txtBacsi.Init(globalVariables.gv_dtDmucNhanvien,
                             new List<string>
                                 {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                 });
                modifyCommands();
            }
            catch (Exception)
            {


            }
        }
        void uc_khamthiluc_Load(object sender, EventArgs e)
        {
           
        }
        public void SetPermision(bool LaKham_Thiluc)
        {
            cmdBatdaukham.Visible = cmdHuybatdaukham.Visible = cmdSave.Visible = cmdFinish.Visible = cmdXoa.Visible = cmdChange.Enabled = cboPhongkhamThiluc.Enabled = LaKham_Thiluc;

        }
        public void SetBacsiKham(object myId)
        {
            txtBacsi.SetId(myId);
        }
        public void InitData()
        {
            try
            {
                LoadData();
                modifyCommands();
            }
            catch (Exception)
            {

             
            }
        }    
        public void LoadData()
        {
            try
            {
                objKhamthiluc = new Select().From(KcbPhieukhamThiluc.Schema)
                    .Where(KcbPhieukhamThiluc.Columns.IdBenhnhan).IsEqualTo(objCongkham.IdBenhnhan)
                    .And(KcbPhieukhamThiluc.Columns.MaLuotkham).IsEqualTo(objCongkham.MaLuotkham)
                    .ExecuteSingle<KcbPhieukhamThiluc>();
                if (objKhamthiluc != null)
                {
                    //Khúc xạ máy
                    txt_khucxamay_cau_phai.Text = objKhamthiluc.KhucxamayCauPhai;
                    txt_khucxamay_tru_phai.Text = objKhamthiluc.KhucxamayTruPhai;
                    txt_khucxamay_truc_phai.Text = objKhamthiluc.KhucxamayTrucPhai;
                    txt_khucxamay_cau_trai.Text = objKhamthiluc.KhucxamayCauTrai;
                    txt_khucxamay_tru_trai.Text = objKhamthiluc.KhucxamayTruTrai;
                    txt_khucxamay_truc_trai.Text = objKhamthiluc.KhucxamayTrucTrai;
                    //Kính cũ
                    txt_kinhcu_cau_phai.Text = objKhamthiluc.KinhcuCauPhai;
                    txt_kinhcu_tru_phai.Text = objKhamthiluc.KinhcuTruPhai;
                    txt_kinhcu_truc_phai.Text = objKhamthiluc.KinhcuTrucPhai;
                    cbo_kinhcu_thiluc_cokinh_phai.Text = objKhamthiluc.KinhcuThilucCokinhPhai;
                    txt_kinhcu_add_phai.Text = objKhamthiluc.KinhcuAddPhai;
                    txt_kinhcu_cau_trai.Text = objKhamthiluc.KinhcuCauTrai;
                    txt_kinhcu_tru_trai.Text = objKhamthiluc.KinhcuTruTrai;
                    txt_kinhcu_truc_trai.Text = objKhamthiluc.KinhcuTrucTrai;
                    cbo_kinhcu_thiluc_cokinh_trai.Text = objKhamthiluc.KinhcuThilucCokinhTrai;
                    txt_kinhcu_add_trai.Text = objKhamthiluc.KinhcuAddTrai;
                    txt_kinhcu_kcdt.Text = objKhamthiluc.KinhcuKcdt;
                    //Khúc xạ hiện tại
                    txt_khucxahientai_cau_phai.Text = objKhamthiluc.KhucxahientaiCauPhai;
                    txt_khucxahientai_tru_phai.Text = objKhamthiluc.KhucxahientaiTruPhai;
                    txt_khucxahientai_truc_phai.Text = objKhamthiluc.KhucxahientaiTrucPhai;
                    cbo_khucxahientai_thiluc_cokinh_phai.Text = objKhamthiluc.KhucxahientaiThilucCokinhPhai;
                    cbo_khucxahientai_thiluc_kinhlo_phai.Text = objKhamthiluc.KhucxahientaiThilucKinhloPhai;
                    cbo_khucxahientai_thiluc_khongkinh_phai.Text = objKhamthiluc.KhucxahientaiThilucKhongkinhPhai;
                    txt_khucxahientai_add_phai.Text = objKhamthiluc.KhucxahientaiAddPhai;

                    txt_khucxahientai_cau_trai.Text = objKhamthiluc.KhucxahientaiCauTrai;
                    txt_khucxahientai_tru_trai.Text = objKhamthiluc.KhucxahientaiTruTrai;
                    txt_khucxahientai_truc_trai.Text = objKhamthiluc.KhucxahientaiTrucTrai;
                    cbo_khucxahientai_thiluc_cokinh_trai.Text = objKhamthiluc.KhucxahientaiThilucCokinhTrai;
                    cbo_khucxahientai_thiluc_kinhlo_trai.Text = objKhamthiluc.KhucxahientaiThilucKinhloTrai;
                    cbo_khucxahientai_thiluc_khongkinh_trai.Text = objKhamthiluc.KhucxahientaiThilucKhongkinhTrai;
                    txt_khucxahientai_add_trai.Text = objKhamthiluc.KhucxahientaiAddTrai;
                    txt_khucxahientai_kcdt.Text = objKhamthiluc.KhucxahientaiKcdt;
                    //Khúc xạ sau liệt điều tiết
                    txt_khucxasaudieutiet_cau_phai.Text = objKhamthiluc.KhucxasaudieutietCauPhai;
                    txt_khucxasaudieutiet_tru_phai.Text = objKhamthiluc.KhucxasaudieutietTruPhai;
                    txt_khucxasaudieutiet_truc_phai.Text = objKhamthiluc.KhucxasaudieutietTrucPhai;
                    cbo_khucxasaudieutiet_thiluc_cokinh_phai.Text = objKhamthiluc.KhucxasaudieutietThilucCokinhPhai;
                    cbo_khucxasaudieutiet_thiluc_khongkinh_phai.Text = objKhamthiluc.KhucxasaudieutietThilucKhongkinhPhai;
                    txt_khucxasaudieutiet_add_phai.Text = objKhamthiluc.KhucxasaudieutietAddPhai;

                    txt_khucxasaudieutiet_cau_trai.Text = objKhamthiluc.KhucxasaudieutietCauTrai;
                    txt_khucxasaudieutiet_tru_trai.Text = objKhamthiluc.KhucxasaudieutietTruTrai;
                    txt_khucxasaudieutiet_truc_trai.Text = objKhamthiluc.KhucxasaudieutietTrucTrai;
                    cbo_khucxasaudieutiet_thiluc_cokinh_trai.Text = objKhamthiluc.KhucxasaudieutietThilucCokinhTrai;
                    cbo_khucxasaudieutiet_thiluc_khongkinh_trai.Text = objKhamthiluc.KhucxasaudieutietThilucKhongkinhTrai;
                    txt_khucxasaudieutiet_add_trai.Text = objKhamthiluc.KhucxasaudieutietAddTrai;
                    txt_khucxasaudieutiet_kcdt.Text = objKhamthiluc.KhucxasaudieutietKcdt;

                    //Đơn kính
                    txt_donkinh_cau_phai.Text = objKhamthiluc.DonkinhCauPhai;
                    txt_donkinh_tru_phai.Text = objKhamthiluc.DonkinhTruPhai;
                    txt_donkinh_truc_phai.Text = objKhamthiluc.DonkinhTrucPhai;
                    cbo_donkinh_thiluc_cokinh_phai.Text = objKhamthiluc.DonkinhThilucCokinhPhai;
                    txt_donkinh_add_phai.Text = objKhamthiluc.DonkinhAddPhai;

                    txt_donkinh_cau_trai.Text = objKhamthiluc.DonkinhCauTrai;
                    txt_donkinh_tru_trai.Text = objKhamthiluc.DonkinhTruTrai;
                    txt_donkinh_truc_trai.Text = objKhamthiluc.DonkinhTrucTrai;
                    cbo_donkinh_thiluc_cokinh_trai.Text = objKhamthiluc.DonkinhThilucCokinhTrai;
                    txt_donkinh_add_trai.Text = objKhamthiluc.DonkinhAddTrai;
                    txt_donkinh_kcdt.Text = objKhamthiluc.DonkinhKcdt;
                    //Các thông số khác
                    txt_Skiascopy_phai.Text = objKhamthiluc.SkiascopyPhai;
                    txt_nhanap_phai.Text = objKhamthiluc.NhanapPhai;
                    txt_dodaygiacmac_phai.Text = objKhamthiluc.DodaygiacmacPhai;
                    txt_duongkinh_dongtu_phai.Text = objKhamthiluc.DuongkinhDongtuPhai;
                    txt_chieudaitrucnhancau_phai.Text = objKhamthiluc.ChieudaitrucnhancauPhai;
                    txt_biendo_dieutiet_phai.Text = objKhamthiluc.BiendoDieutietPhai;
                    txt_k1_phai.Text = objKhamthiluc.K1Phai;
                    txt_k2_phai.Text = objKhamthiluc.K2Phai;
                    txt_langkinh_phai.Text = objKhamthiluc.LangkinhPhai;
                    txt_chandoan_phai.Text = objKhamthiluc.ChandoanPhai;

                    txt_Skiascopy_trai.Text = objKhamthiluc.SkiascopyTrai;
                    txt_nhanap_trai.Text = objKhamthiluc.NhanapTrai;
                    txt_dodaygiacmac_trai.Text = objKhamthiluc.DodaygiacmacTrai;
                    txt_duongkinh_dongtu_trai.Text = objKhamthiluc.DuongkinhDongtuTrai;
                    txt_chieudaitrucnhancau_trai.Text = objKhamthiluc.ChieudaitrucnhancauTrai;
                    txt_biendo_dieutiet_trai.Text = objKhamthiluc.BiendoDieutietTrai;
                    txt_k1_trai.Text = objKhamthiluc.K1Trai;
                    txt_k2_trai.Text = objKhamthiluc.K2Trai;
                    txt_langkinh_trai.Text = objKhamthiluc.LangkinhTrai;
                    txt_chandoan_trai.Text = objKhamthiluc.ChandoanTrai;
                    //Ghi chú
                    txt_khoangcachdongtu_xa.Text = objKhamthiluc.KhoangcachdongtuXa;
                    txt_khoangcachdongtu_gan.Text = objKhamthiluc.KhoangcachdongtuGan;

                    chk_kinhdatrong.Checked = Utility.Bool2Bool(objKhamthiluc.Kinhdatrong);
                    chk_kinhnhingan.Checked = Utility.Bool2Bool(objKhamthiluc.Kinhnhingan);
                    chk_kinh2trong.Checked = Utility.Bool2Bool(objKhamthiluc.Kinh2trong);
                    chk_kinhdoimau.Checked = Utility.Bool2Bool(objKhamthiluc.Kinhdoimau);
                    chk_kinhpoly.Checked = Utility.Bool2Bool(objKhamthiluc.Kinhpoly);
                    chk_kinhaptrong.Checked = Utility.Bool2Bool(objKhamthiluc.Kinhaptrong);

                    chk_pt_duc_thuytinhthe.Checked = Utility.Bool2Bool(objKhamthiluc.PtDucThuytinhthe);
                    chk_pt_khucxa.Checked = Utility.Bool2Bool(objKhamthiluc.PtKhucxa);
                    chk_benhnhan_sau_pt_duc_ttt.Checked = Utility.Bool2Bool(objKhamthiluc.BnSauPtDucThuytinhthe);
                    chk_benhnhan_sau_pt_khucxa.Checked = Utility.Bool2Bool(objKhamthiluc.BnSauPtKhucxa);
                    txtGhichu.Text = objKhamthiluc.GhiChu;
                    txtBacsi.SetId(objKhamthiluc.IdBacsikham);
                    dtpNgaykham.Value = objKhamthiluc.NgayKham.Value;
                    if (objKhamthiluc.ThoigianBatdau.HasValue)
                        dtpThoigian_batdau.Value = objKhamthiluc.ThoigianBatdau.Value;
                    else
                        dtpThoigian_batdau.Value = DateTime.Now;
                    if (objKhamthiluc.ThoigianKetthuc.HasValue)
                        dtpThoigianKetthuc.Value = objKhamthiluc.ThoigianKetthuc.Value;
                    else
                        dtpThoigianKetthuc.Value = DateTime.Now.AddSeconds(30);
                }
                else
                    objKhamthiluc = new KcbPhieukhamThiluc();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        public void PrepareData4Save()
        {
            try
            {
                if (objKhamthiluc == null) objKhamthiluc = new KcbPhieukhamThiluc();//99% ko xảy ra
                if (objKhamthiluc.IdKhamThiluc > 0)
                {
                    objKhamthiluc.MarkOld();
                    objKhamthiluc.IsNew = false;
                    objKhamthiluc.NgaySua = DateTime.Now;
                    objKhamthiluc.NguoiSua = globalVariables.UserName;
                }
                objKhamthiluc.IdBenhnhan = objLuotkham.IdBenhnhan;
                objKhamthiluc.MaLuotkham = objLuotkham.MaLuotkham;
                objKhamthiluc.IdCongkham = objCongkham.IdKham;
                objKhamthiluc.KhucxamayCauPhai=Utility.sDbnull( txt_khucxamay_cau_phai.Text);
                 objKhamthiluc.KhucxamayTruPhai=Utility.sDbnull( txt_khucxamay_tru_phai.Text);
                 objKhamthiluc.KhucxamayTrucPhai=Utility.sDbnull( txt_khucxamay_truc_phai.Text);
                 objKhamthiluc.KhucxamayCauTrai=Utility.sDbnull( txt_khucxamay_cau_trai.Text);
                 objKhamthiluc.KhucxamayTruTrai=Utility.sDbnull( txt_khucxamay_tru_trai.Text);
                 objKhamthiluc.KhucxamayTrucTrai=Utility.sDbnull( txt_khucxamay_truc_trai.Text);
                //Kính cũ

                 objKhamthiluc.KinhcuCauPhai = Utility.sDbnull(txt_kinhcu_cau_phai.Text);
                 objKhamthiluc.KinhcuTruPhai = Utility.sDbnull(txt_kinhcu_tru_phai.Text);
                 objKhamthiluc.KinhcuTrucPhai = Utility.sDbnull(txt_kinhcu_truc_phai.Text);
                 objKhamthiluc.KinhcuThilucCokinhPhai = Utility.sDbnull(cbo_kinhcu_thiluc_cokinh_phai.Text);
                 objKhamthiluc.KinhcuAddPhai = Utility.sDbnull(txt_kinhcu_add_phai.Text);
                
                objKhamthiluc.KinhcuCauTrai = Utility.sDbnull(txt_kinhcu_cau_trai.Text);
                 objKhamthiluc.KinhcuTruTrai = Utility.sDbnull(txt_kinhcu_tru_trai.Text);
                 objKhamthiluc.KinhcuTrucTrai = Utility.sDbnull(txt_kinhcu_truc_trai.Text);
                 objKhamthiluc.KinhcuThilucCokinhTrai = Utility.sDbnull(cbo_kinhcu_thiluc_cokinh_trai.Text);
                 objKhamthiluc.KinhcuAddTrai = Utility.sDbnull(txt_kinhcu_add_trai.Text);
                
                objKhamthiluc.KinhcuKcdt = Utility.sDbnull(txt_kinhcu_kcdt.Text);
                 //Khúc xạ hiện tại
                objKhamthiluc.KhucxahientaiCauPhai = Utility.sDbnull(txt_khucxahientai_cau_phai.Text);
                objKhamthiluc.KhucxahientaiTruPhai = Utility.sDbnull(txt_khucxahientai_tru_phai.Text);
                objKhamthiluc.KhucxahientaiTrucPhai = Utility.sDbnull(txt_khucxahientai_truc_phai.Text);
                objKhamthiluc.KhucxahientaiThilucCokinhPhai = Utility.sDbnull(cbo_khucxahientai_thiluc_cokinh_phai.Text);
                objKhamthiluc.KhucxahientaiThilucKinhloPhai = Utility.sDbnull(cbo_khucxahientai_thiluc_kinhlo_phai.Text);
                objKhamthiluc.KhucxahientaiThilucKhongkinhPhai = Utility.sDbnull(cbo_khucxahientai_thiluc_khongkinh_phai.Text);
                objKhamthiluc.KhucxahientaiAddPhai = Utility.sDbnull(txt_khucxahientai_add_phai.Text);

                objKhamthiluc.KhucxahientaiCauTrai = Utility.sDbnull(txt_khucxahientai_cau_trai.Text);
                objKhamthiluc.KhucxahientaiTruTrai = Utility.sDbnull(txt_khucxahientai_tru_trai.Text);
                objKhamthiluc.KhucxahientaiTrucTrai = Utility.sDbnull(txt_khucxahientai_truc_trai.Text);
                objKhamthiluc.KhucxahientaiThilucCokinhTrai = Utility.sDbnull(cbo_khucxahientai_thiluc_cokinh_trai.Text);
                objKhamthiluc.KhucxahientaiThilucKinhloTrai = Utility.sDbnull(cbo_khucxahientai_thiluc_kinhlo_trai.Text);
                objKhamthiluc.KhucxahientaiThilucKhongkinhTrai = Utility.sDbnull(cbo_khucxahientai_thiluc_khongkinh_trai.Text);
                objKhamthiluc.KhucxahientaiAddTrai = Utility.sDbnull(txt_khucxahientai_add_trai.Text);

                objKhamthiluc.KhucxahientaiKcdt = Utility.sDbnull(txt_khucxahientai_kcdt.Text);

                //Khúc xạ sau liệt điều tiết

                objKhamthiluc.KhucxasaudieutietCauPhai = Utility.sDbnull(txt_khucxasaudieutiet_cau_phai.Text);
                objKhamthiluc.KhucxasaudieutietTruPhai = Utility.sDbnull(txt_khucxasaudieutiet_tru_phai.Text);
                objKhamthiluc.KhucxasaudieutietTrucPhai = Utility.sDbnull(txt_khucxasaudieutiet_truc_phai.Text);
                objKhamthiluc.KhucxasaudieutietThilucCokinhPhai = Utility.sDbnull(cbo_khucxasaudieutiet_thiluc_cokinh_phai.Text);
                objKhamthiluc.KhucxasaudieutietThilucKhongkinhPhai = Utility.sDbnull(cbo_khucxasaudieutiet_thiluc_khongkinh_phai.Text);
                objKhamthiluc.KhucxasaudieutietAddPhai = Utility.sDbnull(txt_khucxasaudieutiet_add_phai.Text);

                objKhamthiluc.KhucxasaudieutietCauTrai = Utility.sDbnull(txt_khucxasaudieutiet_cau_trai.Text);
                objKhamthiluc.KhucxasaudieutietTruTrai = Utility.sDbnull(txt_khucxasaudieutiet_tru_trai.Text);
                objKhamthiluc.KhucxasaudieutietTrucTrai = Utility.sDbnull(txt_khucxasaudieutiet_truc_trai.Text);
                objKhamthiluc.KhucxasaudieutietThilucCokinhTrai = Utility.sDbnull(cbo_khucxasaudieutiet_thiluc_cokinh_trai.Text);
                objKhamthiluc.KhucxasaudieutietThilucKhongkinhTrai = Utility.sDbnull(cbo_khucxasaudieutiet_thiluc_khongkinh_trai.Text);
                objKhamthiluc.KhucxasaudieutietAddTrai = Utility.sDbnull(txt_khucxasaudieutiet_add_trai.Text);

                objKhamthiluc.KhucxasaudieutietKcdt = Utility.sDbnull(txt_khucxasaudieutiet_kcdt.Text);

               
                //Đơn kính
                objKhamthiluc.DonkinhCauPhai = Utility.sDbnull(txt_donkinh_cau_phai.Text);
                objKhamthiluc.DonkinhTruPhai = Utility.sDbnull(txt_donkinh_tru_phai.Text);
                objKhamthiluc.DonkinhTrucPhai = Utility.sDbnull(txt_donkinh_truc_phai.Text);
                objKhamthiluc.DonkinhThilucCokinhPhai = Utility.sDbnull(cbo_donkinh_thiluc_cokinh_phai.Text);
                objKhamthiluc.DonkinhAddPhai = Utility.sDbnull(txt_donkinh_add_phai.Text);

                objKhamthiluc.DonkinhCauTrai = Utility.sDbnull(txt_donkinh_cau_trai.Text);
                objKhamthiluc.DonkinhTruTrai = Utility.sDbnull(txt_donkinh_tru_trai.Text);
                objKhamthiluc.DonkinhTrucTrai = Utility.sDbnull(txt_donkinh_truc_trai.Text);
                objKhamthiluc.DonkinhThilucCokinhTrai = Utility.sDbnull(cbo_donkinh_thiluc_cokinh_trai.Text);
                objKhamthiluc.DonkinhAddTrai = Utility.sDbnull(txt_donkinh_add_trai.Text);

                objKhamthiluc.DonkinhKcdt = Utility.sDbnull(txt_donkinh_kcdt.Text);


               
                //Các thông số khác
                objKhamthiluc.SkiascopyPhai = Utility.sDbnull(txt_Skiascopy_phai.Text);
                objKhamthiluc.NhanapPhai = Utility.sDbnull(txt_nhanap_phai.Text);
                objKhamthiluc.DodaygiacmacPhai = Utility.sDbnull(txt_dodaygiacmac_phai.Text);
                objKhamthiluc.DuongkinhDongtuPhai = Utility.sDbnull(txt_duongkinh_dongtu_phai.Text);
                objKhamthiluc.ChieudaitrucnhancauPhai = Utility.sDbnull(txt_chieudaitrucnhancau_phai.Text);

                objKhamthiluc.BiendoDieutietPhai = Utility.sDbnull(txt_biendo_dieutiet_phai.Text);
                objKhamthiluc.K1Phai = Utility.sDbnull(txt_k1_phai.Text);
                objKhamthiluc.K2Phai = Utility.sDbnull(txt_k2_phai.Text);
                objKhamthiluc.LangkinhPhai = Utility.sDbnull(txt_langkinh_phai.Text);
                objKhamthiluc.ChandoanPhai = Utility.sDbnull(txt_chandoan_phai.Text);
                //Trái
                objKhamthiluc.SkiascopyTrai = Utility.sDbnull(txt_Skiascopy_trai.Text);
                objKhamthiluc.NhanapTrai = Utility.sDbnull(txt_nhanap_trai.Text);
                objKhamthiluc.DodaygiacmacTrai = Utility.sDbnull(txt_dodaygiacmac_trai.Text);
                objKhamthiluc.DuongkinhDongtuTrai= Utility.sDbnull(txt_duongkinh_dongtu_trai.Text);
                objKhamthiluc.ChieudaitrucnhancauTrai = Utility.sDbnull(txt_chieudaitrucnhancau_trai.Text);

                objKhamthiluc.BiendoDieutietTrai = Utility.sDbnull(txt_biendo_dieutiet_trai.Text);
                objKhamthiluc.K1Trai = Utility.sDbnull(txt_k1_trai.Text);
                objKhamthiluc.K2Trai = Utility.sDbnull(txt_k2_trai.Text);
                objKhamthiluc.LangkinhTrai = Utility.sDbnull(txt_langkinh_trai.Text);
                objKhamthiluc.ChandoanTrai = Utility.sDbnull(txt_chandoan_trai.Text);


                //Ghi chú
                objKhamthiluc.KhoangcachdongtuXa = Utility.sDbnull(txt_khoangcachdongtu_xa.Text);
                objKhamthiluc.KhoangcachdongtuGan = Utility.sDbnull(txt_khoangcachdongtu_gan.Text);
                objKhamthiluc.GhiChu = Utility.sDbnull(txtGhichu.Text);

                objKhamthiluc.Kinhdatrong = chk_kinhdatrong.Checked;
                objKhamthiluc.Kinhnhingan = chk_kinhnhingan.Checked;
                objKhamthiluc.Kinh2trong = chk_kinh2trong.Checked;
                objKhamthiluc.Kinhdoimau = chk_kinhdoimau.Checked;
                objKhamthiluc.Kinhpoly = chk_kinhpoly.Checked;
                 objKhamthiluc.Kinhaptrong = chk_kinhaptrong.Checked;

                objKhamthiluc.PtDucThuytinhthe = chk_pt_duc_thuytinhthe.Checked;
                objKhamthiluc.PtKhucxa = chk_pt_khucxa.Checked;
                objKhamthiluc.BnSauPtDucThuytinhthe = chk_benhnhan_sau_pt_duc_ttt.Checked;
                objKhamthiluc.BnSauPtKhucxa = chk_benhnhan_sau_pt_khucxa.Checked;
             
                objKhamthiluc.IdBacsikham = Utility.Int32Dbnull(txtBacsi.MyID, -1);
                objKhamthiluc.NgayKham = dtpNgaykham.Value;
                objKhamthiluc.ThoigianBatdau = dtpThoigian_batdau.Value;
                objKhamthiluc.ThoigianKetthuc = dtpThoigianKetthuc.Value;
                cmdFinish.Visible = cmdXoa.Visible  = cmdIn.Visible = true;
               
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
       
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null || objCongkham == null || objBenhnhan == null)
            {
                Utility.ShowMsg("Mời bạn chọn người bệnh trước khi thực hiện nhập thông tin khám Đo thị lực");
                return;
            }
            if (txtBacsi.MyID == "-1")
            {
                Utility.ShowMsg("Bạn cần nhập bác sĩ khám");
                txtBacsi.Focus();
                return;
            }
             
            PrepareData4Save();
            
            objKhamthiluc.TrangThai = 1;
            objKhamthiluc.Save();
            Utility.Log(Name, globalVariables.UserName, string.Format("Lưu thông tin khám thị lực cho Bệnh nhân {0} có mã lần khám {1} và ID {2}", objBenhnhan.TenBenhnhan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan), newaction.Save, this.GetType().Assembly.ManifestModule.Name);
            modifyCommands();
        }
        void modifyCommands()
        {
            if (objKhamthiluc == null || objKhamthiluc.IdKhamThiluc<=0 || objKhamthiluc.IdBenhnhan<=0)
            {
                cmdBatdaukham.Enabled = true;
                cmdChange.Enabled = true;
                cmdHuybatdaukham.Enabled = cmdSave.Enabled = cmdFinish.Enabled = cmdKhamlai.Enabled = cmdIn.Enabled = cmdXoa.Enabled =cmdInDonkinh.Enabled= false;
            }
            else
            {
                cmdBatdaukham.Enabled = false;
                cmdHuybatdaukham.Enabled = objKhamthiluc.TrangThai ==0;
                cmdSave.Enabled = objKhamthiluc.TrangThai <2;
                cmdXoa.Enabled = objKhamthiluc.TrangThai == 1;
                cmdFinish.Enabled = objKhamthiluc.TrangThai >=1;
                cmdKhamlai.Enabled = objKhamthiluc.TrangThai == 2;
                cmdInDonkinh.Enabled = cmdIn.Enabled = objKhamthiluc.TrangThai >= 1;
                cmdChange.Enabled = objKhamthiluc.TrangThai <= 0;//Chưa lưu

            }
        }
        private void cmdBatdaukham_Click(object sender, EventArgs e)
        {
            if(objLuotkham==null || objCongkham==null || objBenhnhan==null)
            {
                Utility.ShowMsg("Mời bạn chọn người bệnh trước khi thực hiện nhập thông tin khám Đo thị lực");
                return;
            }    
            if (objKhamthiluc == null)
            {
                objKhamthiluc = new KcbPhieukhamThiluc();
                objKhamthiluc.NgayTao = DateTime.Now;
                objKhamthiluc.NguoiTao = globalVariables.UserName;
            }
            objKhamthiluc.ThoigianBatdau = DateTime.Now;
            objKhamthiluc.TrangThai = 0;
            dtpThoigian_batdau.Value = objKhamthiluc.ThoigianBatdau.Value;
            objKhamthiluc.IdBenhnhan = objLuotkham.IdBenhnhan;
            objKhamthiluc.MaLuotkham = objLuotkham.MaLuotkham;
            objKhamthiluc.IdCongkham = objCongkham.IdKham;
            objKhamthiluc.Save();
            Utility.Log(Name, globalVariables.UserName, string.Format("Bắt đầu khám thị lực cho Bệnh nhân {0} có mã lần khám {1} và ID {2}", objBenhnhan.TenBenhnhan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan), newaction.Begin, this.GetType().Assembly.ManifestModule.Name);
            modifyCommands();
        }

        private void cmdHuybatdaukham_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null || objCongkham == null || objBenhnhan == null)
            {
                Utility.ShowMsg("Mời bạn chọn người bệnh trước khi thực hiện nhập thông tin khám Đo thị lực");
                return;
            }
            new Delete().From(KcbPhieukhamThiluc.Schema)
                     .Where(KcbPhieukhamThiluc.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                     .And(KcbPhieukhamThiluc.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                     .Execute();
            //new Update(KcbPhieukhamThiluc.Schema)
            //.Set(KcbPhieukhamThiluc.Columns.ThoigianBatdau).EqualTo(null)
            //         .Where(KcbPhieukhamThiluc.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
            //         .And(KcbPhieukhamThiluc.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
            //         .Execute();
            Utility.Log(Name, globalVariables.UserName, string.Format("Hủy bắt đầu khám thị lực cho Bệnh nhân {0} có mã lần khám {1} và ID {2}", objBenhnhan.TenBenhnhan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan), newaction.CancelData, this.GetType().Assembly.ManifestModule.Name);
            dtpThoigian_batdau.Value = DateTime.Now;
            dtpThoigianKetthuc.Value = DateTime.Now.AddMinutes(1);
            objKhamthiluc = null;
            modifyCommands();

        }

        private void cmdFinish_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null || objCongkham == null || objBenhnhan == null)
            {
                Utility.ShowMsg("Mời bạn chọn người bệnh trước khi thực hiện nhập thông tin khám Đo thị lực");
                return;
            }
            if(!objKhamthiluc.ThoigianKetthuc.HasValue)
            {
                dtpThoigianKetthuc.Value = DateTime.Now;
            }    
            if (dtpThoigianKetthuc.Value <= dtpThoigian_batdau.Value)
            {
                Utility.ShowMsg("Thời gian kết thúc phải > Thời gian bắt đầu");
                dtpThoigianKetthuc.Focus();
                return;
            }
            PrepareData4Save();
            objKhamthiluc.TrangThai = 2;
            objKhamthiluc.Save();
            new Update(KcbDangkyKcb.Schema).Set(KcbDangkyKcb.Columns.TrangThai).EqualTo(1).Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objCongkham.IdKham).Execute();
            //Sinh số QMS cho công khám chính sau khi kết thúc khám thị lực
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_COKHAM_DOTHILUC", "0", false) == "1" && THU_VIEN_CHUNG.Laygiatrithamsohethong("QMSPK_ENABLE", "0", false) == "1")
            {
                KcbDangkyKcb objCK = new Select().From(KcbDangkyKcb.Schema)
                    .Where(KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .And(KcbDangkyKcb.Columns.KhamThiluc).IsEqualTo(0)
                    .And(KcbDangkyKcb.Columns.TrangThai).IsEqualTo(0)
                    .ExecuteSingle<KcbDangkyKcb>();
                if (objCK != null && objCK.TrangThai != 1)//Chưa khám thị lực mới sinh số QMS cho phòng đo thị lực
                {
                    KCB_QMS _KCB_QMS = new KCB_QMS();
                    Int16 STT_QMS = THU_VIEN_CHUNG.LaySothutuKCB(Utility.Int32Dbnull(objCK.IdPhongkham, -1));
                    objCK.SttKham = STT_QMS;
                    new Update(KcbDangkyKcb.Schema).Set(KcbDangkyKcb.Columns.SttKham).EqualTo(STT_QMS).Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objCK.IdKham).Execute();
                    _KCB_QMS.QmsPhongkhamInsert((int)objCK.SttKham.Value, objCK.MaPhongStt, DateTime.Now, objCK.NgayTiepdon.Value, objCK.MaLuotkham, objBenhnhan.IdBenhnhan, objBenhnhan.TenBenhnhan, (int)objBenhnhan.NamSinh, Utility.Int32Dbnull(objBenhnhan.NamSinh, 0), objBenhnhan.GioiTinh, objCK.MaKhoaThuchien, (int)objCK.IdPhongkham, objCK.IdKham, (int)objCK.IdDichvuKcb, objCK.TenDichvuKcb);
                    Utility.Log(Name, globalVariables.UserName, string.Format("Tự động sinh số QMS ={0} cho công khám chính {1}", STT_QMS, objCK.TenDichvuKcb), newaction.FirstOrFinished, this.GetType().Assembly.ManifestModule.Name);
                }
            }
            Utility.Log(Name, globalVariables.UserName, string.Format("Kết thúc khám thị lực cho Bệnh nhân {0} có mã lần khám {1} và ID {2}", objBenhnhan.TenBenhnhan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan), newaction.FirstOrFinished, this.GetType().Assembly.ManifestModule.Name);
            modifyCommands();
        }

        private void cmdKhamlai_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null || objCongkham == null || objBenhnhan == null)
            {
                Utility.ShowMsg("Mời bạn chọn người bệnh trước khi thực hiện nhập thông tin khám Đo thị lực");
                return;
            }
            new Update(KcbDangkyKcb.Schema).Set(KcbDangkyKcb.Columns.TrangThai).EqualTo(0).Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objCongkham.IdKham).Execute();
            dtpThoigianKetthuc.Value = DateTime.Now.AddMinutes(1);
            objKhamthiluc.ThoigianKetthuc = null;
            objKhamthiluc.TrangThai = 1;
            objKhamthiluc.Save();
            Utility.Log(Name, globalVariables.UserName, string.Format("Hủy kết thúc khám thị lực (Khám lại) cho Bệnh nhân {0} có mã lần khám {1} và ID {2}", objBenhnhan.TenBenhnhan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan), newaction.Restore, this.GetType().Assembly.ManifestModule.Name);
            modifyCommands();
        }
        /// <summary>
        /// Xóa quay trở lại khâu mới bắt đầu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoa_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null || objCongkham == null || objBenhnhan == null)
            {
                Utility.ShowMsg("Mời bạn chọn người bệnh trước khi thực hiện nhập thông tin khám Đo thị lực");
                return;
            }
            if (objKhamthiluc != null && objKhamthiluc.NguoiTao != globalVariables.UserName)
            {
                if (!globalVariables.isSuperAdmin)
                {
                    Utility.ShowMsg(string.Format("Bạn không có quyền xóa thông tin phiếu khám khúc xạ được tạo bởi người dùng {0}. Vui lòng liên hệ người khám khúc xạ hoặc dùng quyền super admin để xóa", objKhamthiluc.NguoiTao));
                    return;
                }
            }
            if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa thông tin Khám khúc xạ đang làm?", "Xác nhận xóa", true)) return;
            new Delete().From(KcbPhieukhamThiluc.Schema)
                      .Where(KcbPhieukhamThiluc.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                      .And(KcbPhieukhamThiluc.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                      .Execute();
            Utility.Log(Name, globalVariables.UserName, string.Format("Xóa thông tin khám thị lực cho Bệnh nhân {0} có mã lần khám {1} và ID {2}", objBenhnhan.TenBenhnhan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
            dtpThoigian_batdau.Value = DateTime.Now;
            dtpThoigianKetthuc.Value = DateTime.Now.AddMinutes(1);
            objKhamthiluc = null;
            modifyCommands();
        }

        private void cmdIn_Click(object sender, EventArgs e)
        {
            InPhieuDothiluc();
        }
        void InPhieuDothiluc()
        {
            try
            {

                if (objKhamthiluc == null || objKhamthiluc.IdKhamThiluc <= 0)
                {
                    Utility.ShowMsg("Bạn cần lưu thông tin Đo thị lực trước khi thực hiện in");
                    return;
                }

                DataTable dtData = SPs.KcbPhieukhamkhucxaIn(objKhamthiluc.IdKhamThiluc, objKhamthiluc.IdBenhnhan, objKhamthiluc.MaLuotkham).GetDataSet().Tables[0];
                dtData.TableName = "PhieuDoThiluc";

                DataTable dtMergeField = dtData.Clone();


                THU_VIEN_CHUNG.CreateXML(dtData, "PhieuDoThiluc.xml");
                if (dtData == null || dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                dtData.TableName = "PhieuDoThiluc";
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["donvi_captren"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diachi_benhvien"] = globalVariables.Branch_Address;
                drData["dienthoai_benhvien"] = globalVariables.Branch_Phone;
                drData["hotline_benhvien"] = globalVariables.Branch_Hotline;
                drData["fax_benhvien"] = globalVariables.Branch_Fax;
                drData["website_benhvien"] = globalVariables.Branch_Website;
                drData["email_benhvien"] = globalVariables.Branch_Email;
                drData["ngay_thuchien"] = Utility.FormatDateTimeWithLocation(Utility.sDbnull(drData["sNgay_kham"], DateTime.Now.ToString("dd/MM/yyyy")), globalVariables.gv_strDiadiem);

                List<string> fieldNames = new List<string>();

                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\PhieuDoThiluc.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtMergeField);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport("PhieuDoThiluc", ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu Tóm tắt hồ sơ bệnh án tại thư mục sau :" + PathDoc);
                    return;
                }


                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return;
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), "PhieuDoThiluc", objKhamthiluc.MaLuotkham, Utility.sDbnull(objKhamthiluc.IdKhamThiluc), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


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
                    byte[] QRCode = Utility.GetQRCode(objKhamthiluc.MaLuotkham);
                    sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("qrsize").ExecuteSingle<SysSystemParameter>();
                    if (builder.MoveToMergeField("qrcode") && QRCode != null && QRCode.Length > 100)
                        if (sysLogosize != null)
                        {
                            int w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            int h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(QRCode, w, h);
                            else
                                builder.InsertImage(QRCode);
                        }
                        else
                            builder.InsertImage(QRCode);
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
                    MessageBox.Show("Không tìm thấy biểu mẫu", "Thông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void cmdInDonkinh_Click(object sender, EventArgs e)
        {
            InDonKinh();
        }
        void InDonKinh()
        {
            try
            {

                if (objKhamthiluc == null || objKhamthiluc.IdKhamThiluc <= 0)
                {
                    Utility.ShowMsg("Bạn cần lưu thông tin Đo thị lực trước khi thực hiện in");
                    return;
                }

                DataTable dtData = SPs.BvmKcbDonkinhKhucxaIn(objKhamthiluc.IdKhamThiluc, objKhamthiluc.IdBenhnhan, objKhamthiluc.MaLuotkham).GetDataSet().Tables[0];
                dtData.TableName = "DonKinh";
                List<string> lstAddedFields = new List<string>() {"gioitinh_nam","gioitinh_nu","noikhoa_khong", "noikhoa_co", "pttt_khong", "pttt_co",
                "tinhtrangravien_khoi", "tinhtrangravien_do", "tinhtrangravien_khongthaydoi",
                "tinhtrangravien_nanghon", "tinhtrangravien_tuvong", "tinhtrangravien_xinve","tinhtrangravien_khongxacdinh","chkkinh2trong","chkKinhdatrong","chkKinhnhingan","chkKinhdoimau","chkKinhpoly","chkKinhaptrong",};
                DataTable dtMergeField = dtData.Clone();
                Utility.AddColums2DataTable(ref dtMergeField, lstAddedFields, typeof(string));


                THU_VIEN_CHUNG.CreateXML(dtData, "DonKinh.xml");
                if (dtData == null || dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                dtData.TableName = "DonKinh";
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["donvi_captren"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diachi_benhvien"] = globalVariables.Branch_Address;
                drData["dienthoai_benhvien"] = globalVariables.Branch_Phone;
                drData["hotline_benhvien"] = globalVariables.Branch_Hotline;
                drData["fax_benhvien"] = globalVariables.Branch_Fax;
                drData["website_benhvien"] = globalVariables.Branch_Website;
                drData["email_benhvien"] = globalVariables.Branch_Email;
                drData["ngay_thuchien"] = Utility.FormatDateTimeWithLocation(Utility.sDbnull(drData["sNgaykedon"], DateTime.Now.ToString("dd/MM/yyyy")), globalVariables.gv_strDiadiem);

                Dictionary<string, string> dicMF = new Dictionary<string, string>();
                dicMF.Add("chkkinh2trong", Utility.Bool2byte(objKhamthiluc.Kinh2trong).ToString() == "1" ? "1" : "0");
                dicMF.Add("chkKinhdatrong", Utility.Bool2byte(objKhamthiluc.Kinhdatrong).ToString() == "1" ? "1" : "0");
                dicMF.Add("chkKinhnhingan", Utility.Bool2byte(objKhamthiluc.Kinhnhingan).ToString() == "1" ? "1" : "0");
                dicMF.Add("chkKinhdoimau", Utility.Bool2byte(objKhamthiluc.Kinhdoimau).ToString() == "1" ? "1" : "0");
                dicMF.Add("chkKinhpoly", Utility.Bool2byte(objKhamthiluc.Kinhpoly).ToString() == "1" ? "1" : "0");
                dicMF.Add("chkKinhaptrong", Utility.Bool2byte(objKhamthiluc.Kinhaptrong).ToString() == "1" ? "1" : "0");
                List<string> fieldNames = new List<string>();

                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\DonKinh.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtMergeField);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport("DonKinh", ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu Tóm tắt hồ sơ bệnh án tại thư mục sau :" + PathDoc);
                    return;
                }


                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return;
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), "DonKinh", objKhamthiluc.MaLuotkham, Utility.sDbnull(objKhamthiluc.IdKhamThiluc), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


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
                    byte[] QRCode = Utility.GetQRCode(objKhamthiluc.MaLuotkham);
                    sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("qrsize").ExecuteSingle<SysSystemParameter>();
                    if (builder.MoveToMergeField("qrcode") && QRCode != null && QRCode.Length > 100)
                        if (sysLogosize != null)
                        {
                            int w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            int h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(QRCode, w, h);
                            else
                                builder.InsertImage(QRCode);
                        }
                        else
                            builder.InsertImage(QRCode);
                    Utility.MergeFieldsCheckBox2Doc(builder, dicMF, null, drData);
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
                    MessageBox.Show("Không tìm thấy biểu mẫu", "Thông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        int num = 0;
        private void cmdChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.Int16Dbnull(cboPhongkhamThiluc.SelectedValue) == Utility.Int16Dbnull(objCongkham.IdPhongkham))
                {
                    Utility.ShowMsg(string.Format("Bạn cần chọn phòng đo thị lực khác phòng đo hiện tại {0} trước khi thực hiện đổi phòng", cboPhongHtai.Text));
                    return;
                }
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn đổi phòng Đo thị lực từ {0} sang {1}?\nChú ý: Số QMS ứng với phòng Đo thị lực mới sẽ được sinh ngay sau khi bạn chấp nhận", cboPhongHtai.Text, cboPhongkhamThiluc.Text), "Xác nhận đổi phòng đo Thị lực", true))
                {
                    objCongkham.IdPhongkham = Utility.Int16Dbnull(cboPhongkhamThiluc.SelectedValue);
                    KCB_QMS _KCB_QMS = new KCB_QMS();
                    Int16 STT_QMS = THU_VIEN_CHUNG.LaySothutuKCB(Utility.Int32Dbnull(objCongkham.IdPhongkham, -1));
                    objCongkham.SttKham = STT_QMS;
                    //Lấy dịch vụ KCB ứng với phòng khám mới
                    DmucDichvukcb objDvuKcb =
                        new Select().From(DmucDichvukcb.Schema)
                        .Where(DmucDichvukcb.Columns.IdPhongkham).IsEqualTo(objCongkham.IdPhongkham)
                        .And(DmucDichvukcb.Columns.IdKieukham).IsEqualTo(objCongkham.IdKieukham)
                        .ExecuteSingle<DmucDichvukcb>();
                    string ten_dichvukcb = objDvuKcb != null ? objDvuKcb.TenDichvukcb : objCongkham.TenDichvuKcb;
                    new Update(KcbDangkyKcb.Schema)
                        .Set(KcbDangkyKcb.Columns.SttKham).EqualTo(STT_QMS)
                         .Set(KcbDangkyKcb.Columns.IdPhongkham).EqualTo(objCongkham.IdPhongkham)
                         .Set(KcbDangkyKcb.Columns.IdDichvuKcb).EqualTo(objDvuKcb.IdDichvukcb)
                          .Set(KcbDangkyKcb.Columns.TenDichvuKcb).EqualTo(ten_dichvukcb)
                        .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objCongkham.IdKham)
                        .Execute();
                    _KCB_QMS.QmsPhongkhamInsert((int)objCongkham.SttKham.Value, objCongkham.MaPhongStt, DateTime.Now, objCongkham.NgayTiepdon.Value, objCongkham.MaLuotkham, objBenhnhan.IdBenhnhan, objBenhnhan.TenBenhnhan, (int)objBenhnhan.NamSinh, Utility.Int32Dbnull(objBenhnhan.NamSinh, 0), objBenhnhan.GioiTinh, objCongkham.MaKhoaThuchien, (int)objCongkham.IdPhongkham, objCongkham.IdKham, (int)objCongkham.IdDichvuKcb, objCongkham.TenDichvuKcb);
                    Utility.Log(Name, globalVariables.UserName, string.Format("Đổi phòng đo Thị lực từ {0} sang {1} với số QMS mới {2} cho người bệnh mã khám ={3}, tên ={4}", cboPhongHtai.Text, cboPhongkhamThiluc.Text, STT_QMS, objBenhnhan.TenBenhnhan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }

        }
    }
}
