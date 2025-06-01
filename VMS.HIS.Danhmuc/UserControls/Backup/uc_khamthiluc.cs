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
        }
        public void SetData(KcbLuotkham objLuotkham, KcbDanhsachBenhnhan objBenhnhan,KcbDangkyKcb objCongkham)
        {
            this.objBenhnhan = objBenhnhan;
            this.objLuotkham = objLuotkham;
            this.objCongkham = objCongkham;
        }
        void uc_khamthiluc_Load(object sender, EventArgs e)
        {
           
        }
        public void SetPermision(bool isFullPermision)
        {
            cmdBatdaukham.Visible = cmdHuybatdaukham.Visible = cmdSave.Visible = cmdFinish.Visible = cmdKhamlai.Visible = cmdXoa.Visible = isFullPermision;
        }
        public void SetBacsiKham(object myId)
        {
            txtBacsi.SetId(myId);
        }
        public void InitData()
        {
            try
            {
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
                    cbo_kinhcu_thiluc_cokinh_phai.SelectedValue = objKhamthiluc.KinhcuThilucCokinhPhai;
                    txt_kinhcu_add_phai.Text = objKhamthiluc.KinhcuAddPhai;
                    txt_kinhcu_cau_trai.Text = objKhamthiluc.KinhcuCauTrai;
                    txt_kinhcu_tru_trai.Text = objKhamthiluc.KinhcuTruTrai;
                    txt_kinhcu_truc_trai.Text = objKhamthiluc.KinhcuTrucTrai;
                    cbo_kinhcu_thiluc_cokinh_trai.SelectedValue = objKhamthiluc.KinhcuThilucCokinhTrai;
                    txt_kinhcu_add_trai.Text = objKhamthiluc.KinhcuAddTrai;
                    txt_kinhcu_kcdt.Text = objKhamthiluc.KinhcuKcdt;
                    //Khúc xạ hiện tại
                    txt_khucxahientai_cau_phai.Text = objKhamthiluc.KhucxahientaiCauPhai;
                    txt_khucxahientai_tru_phai.Text = objKhamthiluc.KhucxahientaiTruPhai;
                    txt_khucxahientai_truc_phai.Text = objKhamthiluc.KhucxahientaiTrucPhai;
                    cbo_khucxahientai_thiluc_cokinh_phai.SelectedValue = objKhamthiluc.KhucxahientaiThilucCokinhPhai;
                    cbo_khucxahientai_thiluc_kinhlo_phai.SelectedValue = objKhamthiluc.KhucxahientaiThilucKinhloPhai;
                    cbo_khucxahientai_thiluc_khongkinh_phai.SelectedValue = objKhamthiluc.KhucxahientaiThilucKhongkinhPhai;
                    txt_khucxahientai_add_phai.Text = objKhamthiluc.KhucxahientaiAddPhai;

                    txt_khucxahientai_cau_trai.Text = objKhamthiluc.KhucxahientaiCauTrai;
                    txt_khucxahientai_tru_trai.Text = objKhamthiluc.KhucxahientaiTruTrai;
                    txt_khucxahientai_truc_trai.Text = objKhamthiluc.KhucxahientaiTrucTrai;
                    cbo_khucxahientai_thiluc_cokinh_trai.SelectedValue = objKhamthiluc.KhucxahientaiThilucCokinhTrai;
                    cbo_khucxahientai_thiluc_kinhlo_trai.SelectedValue = objKhamthiluc.KhucxahientaiThilucKinhloTrai;
                    cbo_khucxahientai_thiluc_khongkinh_trai.SelectedValue = objKhamthiluc.KhucxahientaiThilucKhongkinhTrai;
                    txt_khucxahientai_add_trai.Text = objKhamthiluc.KhucxahientaiAddTrai;
                    txt_khucxahientai_kcdt.Text = objKhamthiluc.KhucxahientaiKcdt;
                    //Khúc xạ sau liệt điều tiết
                    txt_khucxasaudieutiet_cau_phai.Text = objKhamthiluc.KhucxasaudieutietCauPhai;
                    txt_khucxasaudieutiet_tru_phai.Text = objKhamthiluc.KhucxasaudieutietTruPhai;
                    txt_khucxasaudieutiet_truc_phai.Text = objKhamthiluc.KhucxasaudieutietTrucPhai;
                    cbo_khucxasaudieutiet_thiluc_cokinh_phai.SelectedValue = objKhamthiluc.KhucxasaudieutietThilucCokinhPhai;
                    cbo_khucxasaudieutiet_thiluc_khongkinh_phai.SelectedValue = objKhamthiluc.KhucxasaudieutietThilucKhongkinhPhai;
                    txt_khucxasaudieutiet_add_phai.Text = objKhamthiluc.KhucxasaudieutietAddPhai;

                    txt_khucxasaudieutiet_cau_trai.Text = objKhamthiluc.KhucxasaudieutietCauTrai;
                    txt_khucxasaudieutiet_tru_trai.Text = objKhamthiluc.KhucxasaudieutietTruTrai;
                    txt_khucxasaudieutiet_truc_trai.Text = objKhamthiluc.KhucxasaudieutietTrucTrai;
                    cbo_khucxasaudieutiet_thiluc_cokinh_trai.SelectedValue = objKhamthiluc.KhucxasaudieutietThilucCokinhTrai;
                    cbo_khucxasaudieutiet_thiluc_khongkinh_trai.SelectedValue = objKhamthiluc.KhucxasaudieutietThilucKhongkinhTrai;
                    txt_khucxasaudieutiet_add_trai.Text = objKhamthiluc.KhucxasaudieutietAddTrai;
                    txt_khucxasaudieutiet_kcdt.Text = objKhamthiluc.KhucxasaudieutietKcdt;

                    //Đơn kính
                    txt_donkinh_cau_phai.Text = objKhamthiluc.DonkinhCauPhai;
                    txt_donkinh_tru_phai.Text = objKhamthiluc.DonkinhTruPhai;
                    txt_donkinh_truc_phai.Text = objKhamthiluc.DonkinhTrucPhai;
                    cbo_donkinh_thiluc_cokinh_phai.SelectedValue = objKhamthiluc.DonkinhThilucCokinhPhai;
                    txt_donkinh_add_phai.Text = objKhamthiluc.DonkinhAddPhai;

                    txt_donkinh_cau_trai.Text = objKhamthiluc.DonkinhCauTrai;
                    txt_donkinh_tru_trai.Text = objKhamthiluc.DonkinhTruTrai;
                    txt_donkinh_truc_trai.Text = objKhamthiluc.DonkinhTrucTrai;
                    cbo_donkinh_thiluc_cokinh_trai.SelectedValue = objKhamthiluc.DonkinhThilucCokinhTrai;
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
                 objKhamthiluc.KinhcuThilucCokinhPhai = Utility.sDbnull(cbo_kinhcu_thiluc_cokinh_phai.SelectedValue);
                 objKhamthiluc.KinhcuAddPhai = Utility.sDbnull(txt_kinhcu_add_phai.Text);
                
                objKhamthiluc.KinhcuCauTrai = Utility.sDbnull(txt_kinhcu_cau_trai.Text);
                 objKhamthiluc.KinhcuTruTrai = Utility.sDbnull(txt_kinhcu_tru_trai.Text);
                 objKhamthiluc.KinhcuTrucTrai = Utility.sDbnull(txt_kinhcu_truc_trai.Text);
                 objKhamthiluc.KinhcuThilucCokinhTrai = Utility.sDbnull(cbo_kinhcu_thiluc_cokinh_trai.SelectedValue);
                 objKhamthiluc.KinhcuAddTrai = Utility.sDbnull(txt_kinhcu_add_trai.Text);
                
                objKhamthiluc.KinhcuKcdt = Utility.sDbnull(txt_kinhcu_kcdt.Text);
                 //Khúc xạ hiện tại
                objKhamthiluc.KhucxahientaiCauPhai = Utility.sDbnull(txt_khucxahientai_cau_phai.Text);
                objKhamthiluc.KhucxahientaiTruPhai = Utility.sDbnull(txt_khucxahientai_tru_phai.Text);
                objKhamthiluc.KhucxahientaiTrucPhai = Utility.sDbnull(txt_khucxahientai_truc_phai.Text);
                objKhamthiluc.KhucxahientaiThilucCokinhPhai = Utility.sDbnull(cbo_khucxahientai_thiluc_cokinh_phai.SelectedValue);
                objKhamthiluc.KhucxahientaiThilucKinhloPhai = Utility.sDbnull(cbo_khucxahientai_thiluc_kinhlo_phai.SelectedValue);
                objKhamthiluc.KhucxahientaiThilucKhongkinhPhai = Utility.sDbnull(cbo_khucxahientai_thiluc_khongkinh_phai.SelectedValue);
                objKhamthiluc.KhucxahientaiAddPhai = Utility.sDbnull(txt_khucxahientai_add_phai.Text);

                objKhamthiluc.KhucxahientaiCauTrai = Utility.sDbnull(txt_khucxahientai_cau_trai.Text);
                objKhamthiluc.KhucxahientaiTruTrai = Utility.sDbnull(txt_khucxahientai_tru_trai.Text);
                objKhamthiluc.KhucxahientaiTruTrai = Utility.sDbnull(txt_khucxahientai_truc_trai.Text);
                objKhamthiluc.KhucxahientaiThilucCokinhTrai = Utility.sDbnull(cbo_khucxahientai_thiluc_cokinh_trai.SelectedValue);
                objKhamthiluc.KhucxahientaiThilucKinhloTrai = Utility.sDbnull(cbo_khucxahientai_thiluc_kinhlo_trai.SelectedValue);
                objKhamthiluc.KhucxahientaiThilucKhongkinhTrai = Utility.sDbnull(cbo_khucxahientai_thiluc_khongkinh_trai.SelectedValue);
                objKhamthiluc.KhucxahientaiAddTrai = Utility.sDbnull(txt_khucxahientai_add_trai.Text);

                objKhamthiluc.KhucxahientaiKcdt = Utility.sDbnull(txt_khucxahientai_kcdt.Text);

                //Khúc xạ sau liệt điều tiết

                objKhamthiluc.KhucxasaudieutietCauPhai = Utility.sDbnull(txt_khucxasaudieutiet_cau_phai.Text);
                objKhamthiluc.KhucxasaudieutietTruPhai = Utility.sDbnull(txt_khucxasaudieutiet_tru_phai.Text);
                objKhamthiluc.KhucxasaudieutietTrucPhai = Utility.sDbnull(txt_khucxasaudieutiet_truc_phai.Text);
                objKhamthiluc.KhucxasaudieutietThilucCokinhPhai = Utility.sDbnull(cbo_khucxasaudieutiet_thiluc_cokinh_phai.SelectedValue);
                objKhamthiluc.KhucxasaudieutietThilucKhongkinhPhai = Utility.sDbnull(cbo_khucxasaudieutiet_thiluc_khongkinh_phai.SelectedValue);
                objKhamthiluc.KhucxasaudieutietAddPhai = Utility.sDbnull(txt_khucxasaudieutiet_add_phai.Text);

                objKhamthiluc.KhucxasaudieutietCauTrai = Utility.sDbnull(txt_khucxasaudieutiet_cau_trai.Text);
                objKhamthiluc.KhucxasaudieutietTruTrai = Utility.sDbnull(txt_khucxasaudieutiet_tru_trai.Text);
                objKhamthiluc.KhucxasaudieutietTrucTrai = Utility.sDbnull(txt_khucxasaudieutiet_truc_trai.Text);
                objKhamthiluc.KhucxasaudieutietThilucCokinhTrai = Utility.sDbnull(cbo_khucxasaudieutiet_thiluc_cokinh_trai.SelectedValue);
                objKhamthiluc.KhucxasaudieutietThilucKhongkinhTrai = Utility.sDbnull(cbo_khucxasaudieutiet_thiluc_khongkinh_trai.SelectedValue);
                objKhamthiluc.KhucxasaudieutietAddTrai = Utility.sDbnull(txt_khucxasaudieutiet_add_trai.Text);

                objKhamthiluc.KhucxasaudieutietKcdt = Utility.sDbnull(txt_khucxasaudieutiet_kcdt.Text);

               
                //Đơn kính
                objKhamthiluc.DonkinhCauPhai = Utility.sDbnull(txt_donkinh_cau_phai.Text);
                objKhamthiluc.DonkinhTruPhai = Utility.sDbnull(txt_donkinh_tru_phai.Text);
                objKhamthiluc.DonkinhTruTrai = Utility.sDbnull(txt_donkinh_truc_phai.Text);
                objKhamthiluc.DonkinhThilucCokinhPhai = Utility.sDbnull(cbo_donkinh_thiluc_cokinh_phai.SelectedValue);
                objKhamthiluc.DonkinhAddPhai = Utility.sDbnull(txt_donkinh_add_phai.Text);

                objKhamthiluc.DonkinhCauTrai = Utility.sDbnull(txt_donkinh_cau_trai.Text);
                objKhamthiluc.DonkinhTruTrai = Utility.sDbnull(txt_donkinh_tru_trai.Text);
                objKhamthiluc.DonkinhTruTrai = Utility.sDbnull(txt_donkinh_truc_trai.Text);
                objKhamthiluc.DonkinhThilucCokinhTrai = Utility.sDbnull(cbo_donkinh_thiluc_cokinh_trai.SelectedValue);
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
                cmdBatdaukham.Visible = true;
                cmdHuybatdaukham.Visible = cmdSave.Visible = cmdFinish.Visible = cmdKhamlai.Visible = cmdIn.Visible =cmdXoa.Visible= false;
            }
            else
            {
                cmdBatdaukham.Visible = false;
                cmdHuybatdaukham.Visible = objKhamthiluc.TrangThai ==0;
                cmdSave.Visible = objKhamthiluc.TrangThai <2;
                cmdXoa.Visible = objKhamthiluc.TrangThai == 1;
                cmdFinish.Visible = objKhamthiluc.TrangThai >=1;
                cmdKhamlai.Visible = cmdIn.Visible = objKhamthiluc.TrangThai == 2;
               
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
            PrepareData4Save();
            objKhamthiluc.TrangThai = 2;
            objKhamthiluc.Save();
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
            new Delete().From(KcbPhieukhamThiluc.Schema)
                      .Where(KcbPhieukhamThiluc.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                      .And(KcbPhieukhamThiluc.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                      .Execute();
            Utility.Log(Name, globalVariables.UserName, string.Format("Xóa thông tin khám thị lực cho Bệnh nhân {0} có mã lần khám {1} và ID {2}", objBenhnhan.TenBenhnhan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
            objKhamthiluc = null;
            modifyCommands();
        }
    }
}
