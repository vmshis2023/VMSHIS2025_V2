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
        VKcbLuotkham objBenhnhan;
        public uc_khamthiluc()
        {
            InitializeComponent();
            this.Load += uc_khamthiluc_Load;
        }

        void uc_khamthiluc_Load(object sender, EventArgs e)
        {
            InitData();
            LoadData();
        }
        void InitData()
        {
        }
        public void LoadData()
        {
            try
            {
                objKhamthiluc = new Select().From(KcbPhieukhamThiluc.Schema)
                    .Where(KcbPhieukhamThiluc.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(KcbPhieukhamThiluc.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .ExecuteSingle<KcbPhieukhamThiluc>();
                if (objKhamthiluc != null)
                {
                    cbothiluckhongkinh_phai.Text = objKhamthiluc.ThilucKhongkinhPhai;
                    cbothiluckhongkinh_trai.Text = objKhamthiluc.ThilucKhongkinhTrai;
                    cbothiluckinhlo_phai.Text = objKhamthiluc.ThilucKinhloPhai;
                    cbothiluckinhlo_trai.Text = objKhamthiluc.ThilucKinhloTrai;
                    cbothongsokinhcu_phai.Text = objKhamthiluc.ThongsokinhcuPhai;
                    cbothongsokinhcu_trai.Text = objKhamthiluc.ThongsokinhcuTrai;
                    cbo_cyl_khongkinh_phai.Text = objKhamthiluc.CylThiluckhongkinhPhai;
                    cbo_cyl_khongkinh_trai.Text = objKhamthiluc.CylThiluckhongkinhTrai;
                    cbo_ax_khongkinh_phai.Text = objKhamthiluc.AxThiluckhongkinhPhai;
                    cbo_ax_khongkinh_trai.Text = objKhamthiluc.AxThiluckhongkinhTrai;

                    cbothiluckinhcu_phai.Text = objKhamthiluc.ThilucKinhcuPhai;
                    cbothiluckinhcu_trai.Text = objKhamthiluc.ThilucKinhcuTrai;
                    cbothongsokinhmoithu_phai.Text = objKhamthiluc.ThongsokinhmoithuPhai;
                    cbothongsokinhmoithu_trai.Text = objKhamthiluc.ThongsokinhmoithuTrai;
                    cbo_cyl_kinhcu_phai.Text = objKhamthiluc.CylKinhcuPhai;
                    cbo_cyl_kinhcu_trai.Text = objKhamthiluc.CylKinhcuTrai;
                    cbo_ax_kinhcu_phai.Text = objKhamthiluc.AxKinhcuPhai;
                    cbo_ax_kinhcu_trai.Text = objKhamthiluc.AxKinhcuTrai;

                    cbothiluckinhmoithu_phai.Text = objKhamthiluc.ThilucKinhmoithuPhai;
                    cbothiluckinhmoithu_trai.Text = objKhamthiluc.ThilucKinhmoithuTrai;
                    cbothongsokinhdocsach_phai.Text = objKhamthiluc.ThongsokinhdocsachPhai;
                    cbothongsokinhdocsach_trai.Text = objKhamthiluc.ThongsokinhdocsachTrai;
                    cbo_cyl_kinhmoi_phai.Text = objKhamthiluc.CylKinhmoithuPhai;
                    cbo_cyl_kinhmoi_trai.Text = objKhamthiluc.CylKinhmoithuTrai;
                    cbo_ax_kinhmoi_phai.Text = objKhamthiluc.AxKinhmoithuPhai;
                    cbo_ax_kinhmoi_trai.Text = objKhamthiluc.AxKinhmoithuTrai;

                    cbothiluckinhdocsach_phai.Text = objKhamthiluc.ThilucKinhdocsachPhai;
                    cbothiluckinhdocsach_trai.Text = objKhamthiluc.ThilucKinhdocsachTrai;
                    cbo_nhanap_phai.Text = objKhamthiluc.NhanapPhai;
                    cbo_nhanap_trai.Text = objKhamthiluc.NhanapTrai;
                    cbo_nhanaphoi_phai.Text = objKhamthiluc.NhanaphoiPhai;
                    cbo_nhanaphoi_trai.Text = objKhamthiluc.NhanaphoiTrai;
                    cbo_nhanap_mak_phai.Text = objKhamthiluc.NhanapMakPhai;
                    cbo_nhanap_mak_trai.Text = objKhamthiluc.NhanapMakTrai;
                    cbo_khucxa_khachquan_phai.Text = objKhamthiluc.KhucxakhachquanPhai;
                    cbo_khucxa_khachquan_trai.Text = objKhamthiluc.KhucxakhachquanTrai;
                    cbo_cyl_khucxa_phai.Text = objKhamthiluc.CylKhucxaPhai;
                    cbo_cyl_khucxa_trai.Text = objKhamthiluc.CylKhucxaTrai;
                    cbo_ax_khucxa_phai.Text = objKhamthiluc.AxKhucxaPhai;
                    cbo_ax_khucxa_trai.Text = objKhamthiluc.AxKhucxaTrai;

                    cbo_khucxaliet_truocdieutiet_phai.Text = objKhamthiluc.KhucxatruocdieutietPhai;
                    cbo_khucxaliet_truocdieutiet_trai.Text = objKhamthiluc.KhucxatruocdieutietTrai;
                    cbo_khucxaliet_saudieutiet_phai.Text = objKhamthiluc.KhucxasaudieutietPhai;
                    cbo_khucxaliet_saudieutiet_trai.Text = objKhamthiluc.KhucxasaudieutietTrai;
                    cbo_khucxahientai_phai.Text = objKhamthiluc.KhucxahientaiPhai;
                    cbo_khucxahientai_trai.Text = objKhamthiluc.KhucxahientaiTrai;
                    cbo_sokinhcu_phai.Text = objKhamthiluc.SokinhcuPhai;
                    cbo_sokinhcu_trai.Text = objKhamthiluc.SokinhcuTrai;

                    cbo_dodaygiacmac_phai.Text = objKhamthiluc.DodaygiacmacPhai;
                    cbo_dodaygiacmac_trai.Text = objKhamthiluc.DodaygiacmacTrai;
                    cbo_chieudautrucnhancau_phai.Text = objKhamthiluc.ChieudaitrucnhancauPhai;
                    cbo_chieudautrucnhancau_trai.Text = objKhamthiluc.ChieudaitrucnhancauTrai;

                    cbo_donkinhnhingan_phai.Text = objKhamthiluc.DonkinhNhinganPhai;
                    cbo_donkinhnhingan_trai.Text = objKhamthiluc.DonkinhNhinganTrai;
                    cbo_donkinhnhinxa_phai.Text = objKhamthiluc.DonkinhNhinxaPhai;
                    cbo_donkinhnhinxa_trai.Text = objKhamthiluc.DonkinhNhinxaTrai;
                    cbo_donkinh_cahai_phai.Text = objKhamthiluc.DonkinhCahaiPhai;
                    cbo_donkinh_cahai_trai.Text = objKhamthiluc.DonkinhCahaiTrai;
                    txtChanDoanMP._Text = objKhamthiluc.ChandoanPhai;
                    txtChandoanMT._Text = objKhamthiluc.ChandoanTrai;
                    txtBacsi.SetId(objKhamthiluc.IdBacsikham);
                    dtpNgaykham.Value = objKhamthiluc.NgayKham;
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
        public void SetData()
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
                objKhamthiluc.ThilucKhongkinhPhai = cbothiluckhongkinh_phai.Text;
                objKhamthiluc.ThilucKhongkinhTrai = cbothiluckhongkinh_trai.Text;
                objKhamthiluc.ThilucKinhloPhai = cbothiluckinhlo_phai.Text;
                objKhamthiluc.ThilucKinhloTrai = cbothiluckinhlo_trai.Text;
                objKhamthiluc.ThongsokinhcuPhai = cbothongsokinhcu_phai.Text;
                objKhamthiluc.ThongsokinhcuTrai = cbothongsokinhcu_trai.Text;
                objKhamthiluc.CylThiluckhongkinhPhai = cbo_cyl_khongkinh_phai.Text;
                objKhamthiluc.CylThiluckhongkinhTrai = cbo_cyl_khongkinh_trai.Text;
                objKhamthiluc.AxThiluckhongkinhPhai = cbo_ax_khongkinh_phai.Text;
                objKhamthiluc.AxThiluckhongkinhTrai = cbo_ax_khongkinh_trai.Text;

                objKhamthiluc.ThilucKinhcuPhai = cbothiluckinhcu_phai.Text;
                objKhamthiluc.ThilucKinhcuTrai = cbothiluckinhcu_trai.Text;
                objKhamthiluc.ThongsokinhmoithuPhai = cbothongsokinhmoithu_phai.Text;
                objKhamthiluc.ThongsokinhmoithuTrai = cbothongsokinhmoithu_trai.Text;
                objKhamthiluc.CylKinhcuPhai = cbo_cyl_kinhcu_phai.Text;
                objKhamthiluc.CylKinhcuTrai = cbo_cyl_kinhcu_trai.Text;
                objKhamthiluc.AxKinhcuPhai = cbo_ax_kinhcu_phai.Text;
                objKhamthiluc.AxKinhcuTrai = cbo_ax_kinhcu_trai.Text;

                objKhamthiluc.ThilucKinhmoithuPhai = cbothiluckinhmoithu_phai.Text;
                objKhamthiluc.ThilucKinhmoithuTrai = cbothiluckinhmoithu_trai.Text;
                objKhamthiluc.ThongsokinhdocsachPhai = cbothongsokinhdocsach_phai.Text;
                objKhamthiluc.ThongsokinhdocsachTrai = cbothongsokinhdocsach_trai.Text;
                objKhamthiluc.CylKinhmoithuPhai = cbo_cyl_kinhmoi_phai.Text;
                objKhamthiluc.CylKinhmoithuTrai = cbo_cyl_kinhmoi_trai.Text;
                objKhamthiluc.AxKinhmoithuPhai = cbo_ax_kinhmoi_phai.Text;
                objKhamthiluc.AxKinhmoithuTrai = cbo_ax_kinhmoi_trai.Text;

                objKhamthiluc.ThilucKinhdocsachPhai = cbothiluckinhdocsach_phai.Text;
                objKhamthiluc.ThilucKinhdocsachTrai = cbothiluckinhdocsach_trai.Text;
                objKhamthiluc.NhanapPhai = cbo_nhanap_phai.Text;
                objKhamthiluc.NhanapTrai = cbo_nhanap_trai.Text;
                objKhamthiluc.NhanaphoiPhai = cbo_nhanaphoi_phai.Text;
                objKhamthiluc.NhanaphoiTrai = cbo_nhanaphoi_trai.Text;
                objKhamthiluc.NhanapMakPhai = cbo_nhanap_mak_phai.Text;
                objKhamthiluc.NhanapMakTrai = cbo_nhanap_mak_trai.Text;
                objKhamthiluc.KhucxakhachquanPhai = cbo_khucxa_khachquan_phai.Text;
                objKhamthiluc.KhucxakhachquanTrai = cbo_khucxa_khachquan_trai.Text;
                objKhamthiluc.CylKhucxaPhai = cbo_cyl_khucxa_phai.Text;
                objKhamthiluc.CylKhucxaTrai = cbo_cyl_khucxa_trai.Text;
                objKhamthiluc.AxKhucxaPhai = cbo_ax_khucxa_phai.Text;
                objKhamthiluc.AxKhucxaTrai = cbo_ax_khucxa_trai.Text;

                objKhamthiluc.KhucxatruocdieutietPhai = cbo_khucxaliet_truocdieutiet_phai.Text;
                objKhamthiluc.KhucxatruocdieutietTrai = cbo_khucxaliet_truocdieutiet_trai.Text;
                objKhamthiluc.KhucxasaudieutietPhai = cbo_khucxaliet_saudieutiet_phai.Text;
                objKhamthiluc.KhucxasaudieutietTrai = cbo_khucxaliet_saudieutiet_trai.Text;
                objKhamthiluc.KhucxahientaiPhai = cbo_khucxahientai_phai.Text;
                objKhamthiluc.KhucxahientaiTrai = cbo_khucxahientai_trai.Text;
                objKhamthiluc.SokinhcuPhai = cbo_sokinhcu_phai.Text;
                objKhamthiluc.SokinhcuTrai = cbo_sokinhcu_trai.Text;

                objKhamthiluc.DodaygiacmacPhai = cbo_dodaygiacmac_phai.Text;
                objKhamthiluc.DodaygiacmacTrai = cbo_dodaygiacmac_trai.Text;
                objKhamthiluc.ChieudaitrucnhancauPhai = cbo_chieudautrucnhancau_phai.Text;
                objKhamthiluc.ChieudaitrucnhancauTrai = cbo_chieudautrucnhancau_trai.Text;

                objKhamthiluc.DonkinhNhinganPhai = cbo_donkinhnhingan_phai.Text;
                objKhamthiluc.DonkinhNhinganTrai = cbo_donkinhnhingan_trai.Text;
                objKhamthiluc.DonkinhNhinxaPhai = cbo_donkinhnhinxa_phai.Text;
                objKhamthiluc.DonkinhNhinxaTrai = cbo_donkinhnhinxa_trai.Text;
                objKhamthiluc.DonkinhCahaiPhai = cbo_donkinh_cahai_phai.Text;
                objKhamthiluc.DonkinhCahaiTrai = cbo_donkinh_cahai_trai.Text;
                objKhamthiluc.ChandoanPhai = txtChanDoanMP.Text;
                objKhamthiluc.ChandoanTrai = txtChandoanMT.Text;
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
            if (txtBacsi.MyID == "-1")
            {
                Utility.ShowMsg("Bạn cần nhập bác sĩ khám");
                txtBacsi.Focus();
                return;
            }
            SetData();
            objKhamthiluc.TrangThai = 1;
            objKhamthiluc.Save();
            Utility.Log(Name, globalVariables.UserName, string.Format("Lưu thông tin khám thị lực cho Bệnh nhân {0} có mã lần khám {1} và ID {2}", objBenhnhan.TenBenhnhan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan), newaction.Save, this.GetType().Assembly.ManifestModule.Name);
            modifyCommands();
        }
        void modifyCommands()
        {
            if (objKhamthiluc == null)
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
            objKhamthiluc.Save();
            Utility.Log(Name, globalVariables.UserName, string.Format("Bắt đầu khám thị lực cho Bệnh nhân {0} có mã lần khám {1} và ID {2}", objBenhnhan.TenBenhnhan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan), newaction.Begin, this.GetType().Assembly.ManifestModule.Name);
            modifyCommands();
        }

        private void cmdHuybatdaukham_Click(object sender, EventArgs e)
        {
            new Update(KcbPhieukhamThiluc.Schema)
            .Set(KcbPhieukhamThiluc.Columns.ThoigianBatdau).EqualTo(null)
                     .Where(KcbPhieukhamThiluc.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                     .And(KcbPhieukhamThiluc.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                     .Execute();
            Utility.Log(Name, globalVariables.UserName, string.Format("Hủy bắt đầu khám thị lực cho Bệnh nhân {0} có mã lần khám {1} và ID {2}", objBenhnhan.TenBenhnhan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan), newaction.CancelData, this.GetType().Assembly.ManifestModule.Name);
            objKhamthiluc = null;
            modifyCommands();

        }

        private void cmdFinish_Click(object sender, EventArgs e)
        {
            SetData();
            objKhamthiluc.TrangThai = 2;
            objKhamthiluc.Save();
            Utility.Log(Name, globalVariables.UserName, string.Format("Kết thúc khám thị lực cho Bệnh nhân {0} có mã lần khám {1} và ID {2}", objBenhnhan.TenBenhnhan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan), newaction.FirstOrFinished, this.GetType().Assembly.ManifestModule.Name);
            modifyCommands();
        }

        private void cmdKhamlai_Click(object sender, EventArgs e)
        {
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
