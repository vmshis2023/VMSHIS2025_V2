using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using Janus.Windows.CalendarCombo;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX.EditControls;

using VNS.Libs;
using VNS.HIS.NGHIEPVU;
using VNS.HIS.DAL;

using System.Windows.Forms;
using SubSonic;

namespace VNS.HIS.UI.BENH_AN
{
    public partial class frm_BENHAN_NGOAITRU : Form
    {
        public action em_Action = action.Insert;
        private bool SuaBenhAn = false;
        private string MaBenhAnText = "";
        private string MaBenhNhanText = "";
        private string strBenhAn = Application.StartupPath + @"\CAUHINH\BAn_NTru_DongSauLuu.txt";
        private bool AllowTextChanged = false;

        public frm_BENHAN_NGOAITRU()
        {

            InitializeComponent();
            txtMaBN.KeyDown += txtMaBN_KeyDown;
            txtMaLanKham.KeyDown += txtMaLanKham_KeyDown;
            chkkbChanPhaiPXGXGiam.CheckedChanged += chkkbChanPhaiPXGXGiam_CheckedChanged;
            chkkbChanPhaiBinhThuong.CheckedChanged += chkkbChanPhaiBinhThuong_CheckedChanged;
            chkkbChanPhaiTang.CheckedChanged += chkkbChanPhaiTang_CheckedChanged;
            chkkbChanTraiPXGXGiam.CheckedChanged += chkkbChanTraiPXGXGiam_CheckedChanged;
            chkkbChanTraiBinhThuong.CheckedChanged += chkkbChanTraiBinhThuong_CheckedChanged;
            chkkbChanTraiTang.CheckedChanged += chkkbChanTraiTang_CheckedChanged;
            chkhbDeu.CheckedChanged += chkhbDeu_CheckedChanged;
            chkhbKhongDeu.CheckedChanged += chkhbKhongDeu_CheckedChanged;
            chkhbTiemInsulin.CheckedChanged += chkhbTiemInsulin_CheckedChanged;
            chkhbGaySutCan.CheckedChanged += chkhbGaySutCan_CheckedChanged;
            chkhbKhat.CheckedChanged += chkhbKhat_CheckedChanged;
            chkhbDai.CheckedChanged += chkhbDai_CheckedChanged;
            chktsbNhoiMauCoTim.CheckedChanged += chktsbNhoiMauCoTim_CheckedChanged;
            chktsbTBMN.CheckedChanged += chktsbTBMN_CheckedChanged;
            chkkbMatNuocCo.CheckedChanged += chkkbMatNuocCo_CheckedChanged;
            chkkbMatNuocKhong.CheckedChanged += chkkbMatNuocKhong_CheckedChanged;
            chkkbXuatHuetDuoiDaCo.CheckedChanged += chkkbXuatHuetDuoiDaCo_CheckedChanged;
            chkkbXuatHuyetDuoiDaKhong.CheckedChanged += chkkbXuatHuyetDuoiDaKhong_CheckedChanged;
            chkkbPhuCo.CheckedChanged += chkkbPhuCo_CheckedChanged;
            chkkbPhuKhong.CheckedChanged += chkkbPhuKhong_CheckedChanged;
            chkkbNhipTimDeu.CheckedChanged += chkkbNhipTimDeu_CheckedChanged;
            chkkbNhipTimKhong.CheckedChanged += chkkbNhipTimKhong_CheckedChanged;
            //  txtMaBenhAn.KeyDown += txtMaBenhAn_KeyDown;
            // cmdDelete.Click += cmdDelete_Click;

        }

        private void txtMaBN_KeyDown(object sender, KeyEventArgs e)
        {
            ClearControl();
            if (e.KeyCode == Keys.Enter && txtMaBN.Text.Trim() != "")
            {
                FindPatientID(txtMaBN.Text.Trim());
                SuaBenhAn = false;
            }
        }

        // LẤY THÔNG TIN BỆNH ÁN CỦA BỆNH NHÂN
        private void LayThongTinBenhNhan(string id_benhnhan)
        {
            KcbDanhsachBenhnhan objThongTinBenhNhan = new Select("*")
                .From(KcbDanhsachBenhnhan.Schema)
                .Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(id_benhnhan)).ExecuteSingle
                <KcbDanhsachBenhnhan>();
            try
            {

                txtDiaChi.Text = Utility.sDbnull(objThongTinBenhNhan.DiaChi, "");
                txtHoTen.Text = Utility.sDbnull(objThongTinBenhNhan.TenBenhnhan, "");
                txtNamSinh.Text = Utility.sDbnull(objThongTinBenhNhan.NamSinh, "");
                cboGioiTinh.SelectedValue = Utility.sDbnull(objThongTinBenhNhan.GioiTinh, "");
                txtNgheNghiep.Text = Utility.sDbnull(objThongTinBenhNhan.NgheNghiep, "");
                dtThoiDiemDkKham.Value = objThongTinBenhNhan.NgayTiepdon;

                txtDanToc.Text = Utility.sDbnull(objThongTinBenhNhan.DanToc);

                QueryCommand cmd = KcbLuotkham.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandSql = "Select max(ma_luotkham) as ma_luotkham from Kcb_Luotkham where id_benhnhan = " +
                                 id_benhnhan;
                DataTable temdt = DataService.GetDataSet(cmd).Tables[0];
                if (temdt.Rows.Count > 0)
                {
                    MaBenhNhanText = id_benhnhan;
                    string malankham = temdt.Rows[0]["ma_luotkham"].ToString();


                    QueryCommand cmd1 = KcbLuotkham.CreateQuery().BuildCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandSql = "Select *  from Kcb_Luotkham where ma_luotkham ='" +
                                      malankham + "'";
                    DataTable temdt1 = DataService.GetDataSet(cmd1).Tables[0];

                    txtDoiTuong.Text = Utility.sDbnull(temdt1.Rows[0]["ma_doituong_kcb"].ToString());
                    dtInsToDate.Text = temdt1.Rows[0]["ngayketthuc_bhyt"].ToString();
                    txtSoBaoHiemYte.Text = Utility.sDbnull(temdt1.Rows[0]["mathe_bhyt"].ToString());

                }
                else
                {
                    MessageBox.Show("Không tồn tại mã bệnh nhân vừa nhập");
                    txtMaBN.Focus();

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi khi tìm kiếm thông tin BN:\n" + ex.Message);
                txtMaBN.Focus();
            }



        }

        //TÌM BỆNH NHAN THEO MÃ BỆNH NHÂN
        private void FindPatientID(string id_benhnhan)
        {

            LayThongTinBenhNhan(txtMaBN.Text);
            dtDieuTriNgoaiTruTu.Value = dtDieuTriNgoaiTruDen.Value = DateTime.Now;
            try
            {
                SqlQuery SoKham =
                    new Select().From(KcbBenhAn.Schema).
                        Where(KcbBenhAn.Columns.IdBnhan).IsEqualTo(Utility.sDbnull(txtMaBN.Text));

                //Đã Tồn tại thông tin bệnh án ngoại trú. Sửa
                if (SoKham.GetRecordCount() > 0)
                {
                    em_Action = action.Update;
                    LayDuLieu();
                    txtNgaySinh.Focus();
                    MaBenhAnText = txtMaBenhAn.Text;
                    cmdInBenhAn.Enabled = true;



                }
                //chưa tồn tại thông tin ngoại tru. Them
                else
                {
                    em_Action = action.Insert;
                    txtMaBenhAn.Clear();
                    txtNgaySinh.Focus();
                    // MaBenhAnText = txtMaBenhAn.Text;
                    cmdInBenhAn.Enabled = false;
                    label94.Text = "- Bệnh nhân chưa có bệnh án: Ấn Lưu, hoặc nhập số bệnh án cũ và Lưu";
                    label95.Text = "- Người Tạo:";
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
            }
        }

        private void frm_BENHAN_NGOAITRU_Load(object sender, EventArgs e)
        {

            if (txtMaBN.Text.Trim() == "")
            {
                txtMaBN.SelectAll();
                txtMaBN.Focus();


            }
            else
            {

                MaBenhNhanText = txtMaBN.Text;
                LayThongTinBenhNhan(txtMaBN.Text);
                dtDieuTriNgoaiTruTu.Value = dtDieuTriNgoaiTruDen.Value = DateTime.Now;
                if (em_Action == action.Insert)
                {
                    //SinhMaBenhAn();
                    cmdInBenhAn.Enabled = false;
                    //MaBenhAnText = txtMaBenhAn.Text;
                    txtNgaySinh.Focus();

                }


                if (em_Action == action.Update)
                {
                    LayDuLieu();
                    cmdInBenhAn.Enabled = true;
                    MaBenhAnText = txtMaBenhAn.Text;
                    txtNgaySinh.Focus();
                }
                chkDongSauKhiLuu.Checked = true;
                AutoDongFrom();
            }
        }

        //SINH MÃ BỆNH ÁN
        private void SinhMaBenhAn()
        {

            if (em_Action == action.Insert)
            {
                txtMaBenhAn.Text = THU_VIEN_CHUNG.SinhMaBenhAn();
            }


        }

        // LAY DU LIEU BENH AN

        private void LayDuLieu()
        {
            try
            {



                KcbBenhAn objBenhAnNgoaiTru =
                    new Select("*").From(KcbBenhAn.Schema).Where(KcbBenhAn.Columns.IdBnhan).IsEqualTo(
                        Utility.Int32Dbnull(txtMaBN.Text.Trim())).ExecuteSingle<KcbBenhAn>();
                //  KcbBenhAn objBenhAnNgoaiTru = KcbBenhAn.FetchByID(Utility.Int32Dbnull(txtSoKham.Text));
                if (objBenhAnNgoaiTru != null)
                {
                    txtID_BA.Text = Utility.sDbnull(objBenhAnNgoaiTru.Id, "");
                    txtMaBenhAn.Text = Utility.sDbnull(objBenhAnNgoaiTru.SoBenhAn, -1);
                    txtMaBN.Text = Utility.sDbnull(objBenhAnNgoaiTru.IdBnhan, "");
                    txtNgaySinh.Text = Utility.sDbnull(objBenhAnNgoaiTru.NgaySinh, "");
                    txtThangSinh.Text = Utility.sDbnull(objBenhAnNgoaiTru.ThangSinh, "");
                    txtDanToc.Text = Utility.sDbnull(objBenhAnNgoaiTru.DanToc, "");
                    txtNoiLamViec.Text = Utility.sDbnull(objBenhAnNgoaiTru.NoiLamviec, "");
                    txtThongTinLienHe.Text = Utility.sDbnull(objBenhAnNgoaiTru.ThongtinLhe, "");
                    txtDienThoai.Text = Utility.sDbnull(objBenhAnNgoaiTru.DthoaiLhe, "");
                    txtChanDoanBanDau.Text = Utility.sDbnull(objBenhAnNgoaiTru.CdoanGioithieu, "");
                    chkYTe.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.YTe) == 1;
                    chkTuDen.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.YTe) == 2;
                    dtThoiDiemDkKham.Value = Convert.ToDateTime(objBenhAnNgoaiTru.NgayKham);
                    chkldvvMetMoi.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.LdoVaovienMm) == 1;
                    chkldvvGay.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.LdoVaovienGsc) == 1;
                    chkldvvKhat.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.LdoVaovienKndn) == 1;
                    chkldvvGiamThiLuc.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.LdoVaovienGtl) == 1;
                    chkldvvKhac.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.LdoVaovienKhac) == 1;
                    txtNamChanDoanDTD.Text = Utility.sDbnull(objBenhAnNgoaiTru.HbNam, "");
                    txtNoiChanDoanDTD.Text = Utility.sDbnull(objBenhAnNgoaiTru.HbNoiCdoan, "");
                    chkhbDeu.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.HbDieuTri) == 1;
                    chkhbKhongDeu.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.HbDieuTri) == 2;
                    chkhbTiemInsulin.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.HbTiemInsulin) == 1;
                    txtLuongInsulin.Text = Utility.sDbnull(objBenhAnNgoaiTru.HbInsulin1, "");
                    txthbCachDungInsulin.Text = Utility.sDbnull(objBenhAnNgoaiTru.HbInsulin2, "");
                    txthbThuocHaDuongHuyet.Text = Utility.sDbnull(objBenhAnNgoaiTru.HbThuochaDuonghuyet, "");
                    txthbThuocHaHA.Text = Utility.sDbnull(objBenhAnNgoaiTru.HbHa, "");
                    txthbThuocRLLP.Text = Utility.sDbnull(objBenhAnNgoaiTru.HbRllp, "");
                    txthbThuocChongDong.Text = Utility.sDbnull(objBenhAnNgoaiTru.HbThuocChongdong, "");
                    chkhbMetMoi.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.HtaiMm) == 1;
                    chkhbGaySutCan.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.HtaiGaysc) == 1;
                    txthbKgDau.Text = Utility.sDbnull(objBenhAnNgoaiTru.HtaiKgDau, "");
                    txthbKgSau.Text = Utility.sDbnull(objBenhAnNgoaiTru.HtaiKgSau, "");
                    chkhbKhat.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.HtaiKhatnhieu) == 1;
                    txthbSoLanUongNuoc.Text = Utility.sDbnull(objBenhAnNgoaiTru.HtaiUong, "");
                    chkhbDai.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.HtaiDainhieu) == 1;
                    txthbSoLanDai.Text = Utility.sDbnull(objBenhAnNgoaiTru.HtaiDai, "");
                    chkhbGiamThiLuc.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.HtaiGiamtl) == 1;
                    chkhbKhac.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.HtaiKhac) == 1;
                    chktsbChuaPhatHienBenh.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TsbBtChuaphathien) == 1;
                    chktsbNhoiMauCoTim.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TsbBtNhoimaucotim) == 1;
                    txttsbNamNhoiMauCoTim.Text = Utility.sDbnull(objBenhAnNgoaiTru.TsbBtNamNmct, "");
                    chktsbTBMN.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TsbBtTbmn) == 1;
                    txttsbNamTBMN.Text = Utility.sDbnull(objBenhAnNgoaiTru.TsbBtNamTbmn, "");
                    txttsbTangHuyetAp.Text = Utility.sDbnull(objBenhAnNgoaiTru.TsbHaNam, "");
                    txttsbHAmax.Text = Utility.sDbnull(objBenhAnNgoaiTru.TsbHaMax, "");
                    chktsbDeConLonHon4000.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TsbBtCON4000) == 1;
                    txttsbKhac.Text = Utility.sDbnull(objBenhAnNgoaiTru.TsbBtKhac, "");
                    chktsbDTD.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TsgdDtd) == 1;
                    chktsbTangHuyetAp.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TsgdTanghuyetap) == 1;
                    chktsgdNhoiMauCoTim.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TsbBtNhoimaucotim) == 1;
                    txttsgdKhac.Text = Utility.sDbnull(objBenhAnNgoaiTru.TsgdKhac, "");
                    chkkbMatNuocCo.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbMatnuoc) == 1;
                    chkkbMatNuocKhong.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbMatnuoc) == 2;
                    chkkbXuatHuetDuoiDaCo.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbXuathuyet) == 1;
                    chkkbXuatHuyetDuoiDaKhong.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbXuathuyet) == 2;
                    chkkbPhuCo.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbPhu) == 1;
                    chkkbPhuKhong.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbPhu) == 2;
                    txtkbKhac.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbToanthanKhac);
                    txtkbNhipTim.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbNhiptim);
                    chkkbNhipTimDeu.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbTthaiNhiptim) == 1;
                    chkkbNhipTimKhong.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbTthaiNhiptim) == 2;
                    txtkbTiengTim.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbTiengtim);
                    txtkbHoHap.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbHohap);
                    txtkbBung.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbBung);
                    chkkbChanPhaiPXGXGiam.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbChanphai) == 1;
                    chkkbChanPhaiBinhThuong.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbChanphai) == 2;
                    chkkbChanPhaiTang.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbChanphai) == 3;
                    txtkbChanPhaiKhac.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbChanphaiKhac);
                    chkkbChanTraiPXGXGiam.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbChantrai) == 1;
                    chkkbChanTraiBinhThuong.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbChantrai) == 2;
                    chkkbChanTraiTang.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbChantrai) == 3;
                    txtkbChanTraiKhac.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbChantraiKhac);
                    txtkbMatPhai.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbMatphai);
                    txtkbMatTrai.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbMattrai);
                    txtkbRangHamMat.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbRanghammat);
                    txtkbKhac1.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbTkinhKhac);
                    txtMach.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbMach, "");
                    txtNhietDo.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbNhietdo);
                    txtHuyetApTu.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbHuyetap1);
                    txtHuyetApDen.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbHuyetap2);
                    txtNhipTho.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbNhiptho);
                    txtCanNang.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbCannang);
                    txtChieuCao.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbChieucao);
                    txtBMI.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbBmi);


                    txtkbTomTatKQCLSC.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbTomtatClsChinh);
                    TXTKBChanDoanBD.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbCdoanBdau);
                    TXTKBDaXuLy.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbDaXly);
                    txtkbChanDoanRaVien.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbCdoanRavien);
                    dtDieuTriNgoaiTruTu.Value = Convert.ToDateTime(objBenhAnNgoaiTru.KcbDtriTungay);
                    dtDieuTriNgoaiTruDen.Value = Convert.ToDateTime(objBenhAnNgoaiTru.KcbDtriDenngay);
                    txtQuaTrinhBenhLy.Text = Utility.sDbnull(objBenhAnNgoaiTru.TkbaQtrinhBenhlyDbienCls);
                    txtTomTatCLS.Text = Utility.sDbnull(objBenhAnNgoaiTru.TkbaPphapDtri);
                    txtBenhChinh.Text = Utility.sDbnull(objBenhAnNgoaiTru.TenBenhChinh);
                    txtBenhPhu.Text = Utility.sDbnull(objBenhAnNgoaiTru.TenBenhPhu);
                    txtPPDT.Text = Utility.sDbnull(objBenhAnNgoaiTru.TkbaPphapDtri);
                    txtTrangThaiRaVien.Text = Utility.sDbnull(objBenhAnNgoaiTru.TkbaTtRavien);
                    txtHuongTiepTheo.Text = Utility.sDbnull(objBenhAnNgoaiTru.TkbaHuongTtTieptheo);
                    txtDoiTuong.Text = Utility.sDbnull(objBenhAnNgoaiTru.DoiTuong);
                    txtSoBaoHiemYte.Text = Utility.sDbnull(objBenhAnNgoaiTru.SoBhyt);
                    txtDiaChi.Text = Utility.sDbnull(objBenhAnNgoaiTru.DiaChi);
                    label94.Text = "Bệnh nhân đã có bệnh án";
                    label95.Text = Utility.sDbnull(objBenhAnNgoaiTru.NguoiTao);

                    if (Utility.sDbnull(objBenhAnNgoaiTru.NguoiTao, "") != "")
                    {

                        //LStaff objNguoiTao =
                        //    new Select("*").From(LStaff.Schema).Where(LStaff.Columns.Uid).IsEqualTo(
                        //        Utility.sDbnull(objBenhAnNgoaiTru.NguoiTao)).ExecuteSingle<LStaff>();
                        //label95.Text = Utility.sDbnull(objNguoiTao.StaffName);
                    }





                }

                else
                {
                    MessageBox.Show("Có lỗi trong quá trình lấy thông tin bệnh án!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi khi tìm kiếm thông tin BN:\n" + ex.Message);
            }
        }




        private void chkkbChanPhaiPXGXGiam_CheckedChanged(object sender, EventArgs e)
        {
            if (chkkbChanPhaiPXGXGiam.Checked == true)
            {
                chkkbChanPhaiBinhThuong.Checked = false;
                chkkbChanPhaiTang.Checked = false;
            }
        }

        private void chkkbChanPhaiBinhThuong_CheckedChanged(object sender, EventArgs e)
        {
            if (chkkbChanPhaiBinhThuong.Checked == true)
            {
                chkkbChanPhaiPXGXGiam.Checked = false;
                chkkbChanPhaiTang.Checked = false;
            }
        }

        private void chkkbChanPhaiTang_CheckedChanged(object sender, EventArgs e)
        {
            if (chkkbChanPhaiTang.Checked == true)
            {
                chkkbChanPhaiPXGXGiam.Checked = false;
                chkkbChanPhaiBinhThuong.Checked = false;
            }
        }

        private void chkkbChanTraiPXGXGiam_CheckedChanged(object sender, EventArgs e)
        {
            if (chkkbChanTraiPXGXGiam.Checked == true)
            {

                chkkbChanTraiBinhThuong.Checked = false;
                chkkbChanTraiTang.Checked = false;
            }

        }

        private void chkkbChanTraiBinhThuong_CheckedChanged(object sender, EventArgs e)
        {
            if (chkkbChanTraiBinhThuong.Checked == true)
            {
                chkkbChanTraiPXGXGiam.Checked = false;
                chkkbChanTraiTang.Checked = false;
            }
        }

        private void chkkbChanTraiTang_CheckedChanged(object sender, EventArgs e)
        {
            if (chkkbChanTraiTang.Checked == true)
            {
                chkkbChanTraiPXGXGiam.Checked = false;
                chkkbChanTraiBinhThuong.Checked = false;
            }
        }

        private void chkhbDeu_CheckedChanged(object sender, EventArgs e)
        {
            if (chkhbDeu.Checked == true)
                chkhbKhongDeu.Checked = false;
        }

        private void chkhbKhongDeu_CheckedChanged(object sender, EventArgs e)
        {
            if (chkhbKhongDeu.Checked == true)
                chkhbDeu.Checked = false;
        }

        private void chkhbTiemInsulin_CheckedChanged(object sender, EventArgs e)
        {
            if (chkhbTiemInsulin.Checked == true)
            {
                txtLuongInsulin.Visible = true;
                txthbCachDungInsulin.Visible = true;
                txtLuongInsulin.Focus();
            }
            else
            {
                txtLuongInsulin.Visible = false;
                txthbCachDungInsulin.Visible = false;
            }
        }

        private void chkhbGaySutCan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkhbGaySutCan.Checked == true)
            {
                txthbKgDau.Visible = true;
                txthbKgSau.Visible = true;
                txthbKgDau.Focus();

            }
            else
            {
                txthbKgDau.Visible = false;
                txthbKgSau.Visible = false;
            }
        }

        private void chkhbKhat_CheckedChanged(object sender, EventArgs e)
        {
            if (chkhbKhat.Checked == true)
            {
                txthbSoLanUongNuoc.Visible = true;
                txthbSoLanUongNuoc.Focus();
            }
            else
            {
                txthbSoLanUongNuoc.Visible = false;
            }
        }

        private void chkhbDai_CheckedChanged(object sender, EventArgs e)
        {
            if (chkhbDai.Checked == true)
            {
                txthbSoLanDai.Visible = true;
                txthbSoLanDai.Focus();
            }
            else
            {
                txthbSoLanDai.Visible = false;
            }
        }

        private void frm_BENHAN_NGOAITRU_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
            if (e.KeyCode == Keys.F4) cmdInBenhAn.PerformClick();
            if (e.KeyCode == Keys.Escape) Close();
            if (e.KeyCode == Keys.F1)
            {
                txtMaBN.SelectAll();
                txtMaBN.Focus();
            }

        }

        private void chktsbNhoiMauCoTim_CheckedChanged(object sender, EventArgs e)
        {
            if (chktsbNhoiMauCoTim.Checked == true)
            {
                txttsbNamNhoiMauCoTim.Visible = true;
                txttsbNamNhoiMauCoTim.Focus();
            }
            else
            {
                txttsbNamNhoiMauCoTim.Visible = false;
            }
        }

        private void chktsbTBMN_CheckedChanged(object sender, EventArgs e)
        {
            if (chktsbTBMN.Checked == true)
            {
                txttsbNamTBMN.Visible = true;
                txttsbNamTBMN.Focus();
            }
            else
            {
                txttsbNamTBMN.Visible = false;
            }
        }

        private void chkkbMatNuocCo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkkbMatNuocCo.Checked == true)
                chkkbMatNuocKhong.Checked = false;
        }

        private void chkkbMatNuocKhong_CheckedChanged(object sender, EventArgs e)
        {
            if (chkkbMatNuocKhong.Checked == true)
                chkkbMatNuocCo.Checked = false;
        }

        private void chkkbXuatHuetDuoiDaCo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkkbXuatHuetDuoiDaCo.Checked == true)
                chkkbXuatHuyetDuoiDaKhong.Checked = false;
        }

        private void chkkbXuatHuyetDuoiDaKhong_CheckedChanged(object sender, EventArgs e)
        {
            if (chkkbXuatHuyetDuoiDaKhong.Checked == true)
                chkkbXuatHuetDuoiDaCo.Checked = false;
        }

        private void chkkbPhuCo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkkbPhuCo.Checked == true)
                chkkbPhuKhong.Checked = false;
        }

        private void chkkbPhuKhong_CheckedChanged(object sender, EventArgs e)
        {
            if (chkkbPhuKhong.Checked == true)
                chkkbPhuCo.Checked = false;
        }

        private void chkkbNhipTimDeu_CheckedChanged(object sender, EventArgs e)
        {
            if (chkkbNhipTimDeu.Checked == true)
                chkkbNhipTimKhong.Checked = false;
        }

        private void chkkbNhipTimKhong_CheckedChanged(object sender, EventArgs e)
        {
            if (chkkbNhipTimKhong.Checked == true)
                chkkbNhipTimDeu.Checked = false;
        }



        private void PerformAction()
        {

            switch (em_Action)
            {
                case action.Update:
                    UpdatePatient();
                    break;
                case action.Insert:
                    InsertPatient();
                    break;
                case action.Select:
                    // AddNewPatient();
                    break;
                case action.Delete:
                    DeletePatient();
                    break;

            }
        }

        private void DeletePatient()
        {
            if (Utility.AcceptQuestion("Bạn có chắc chắn xóa BỆNH ÁN", "Thông Báo", true))
            {
                //SqlQuery KT_SO_BENH_AN =
                //   new Delete().From(KcbBenhAn.Schema).
                //       Where(KcbBenhAn.Columns.SoBenhAn).IsEqualTo(Utility.sDbnull(txtMaBenhAn.Text));
                //KT_SO_BENH_AN.ExecuteSingle<KcbBenhAn>();
                try
                {
                    new Delete().From(KcbBenhAn.Schema).Where(KcbBenhAn.Columns.SoBenhAn).IsEqualTo(
                        Utility.sDbnull(txtMaBenhAn.Text)).Execute();
                    ClearControl();
                    FindPatientID(MaBenhNhanText);

                }
                catch (Exception)
                {

                    throw;
                }


            }
            else
            {
                FindPatientID(MaBenhNhanText);
            }
        }

        private void UpdatePatient()
        {

            SqlQuery KT_IN_BENH_AN =
                new Select().From(KcbBenhAn.Schema).
                    Where(KcbBenhAn.Columns.InPhieuLog).IsEqualTo(Utility.Int16Dbnull(1)).And(
                        KcbBenhAn.Columns.IdBnhan).IsEqualTo(Utility.Int32Dbnull(MaBenhNhanText, -1));
            if (KT_IN_BENH_AN.GetRecordCount() > 0)
            {
                MessageBox.Show("Bệnh án đã được in, vui lòng mở khóa bệnh án để chỉnh sửa");
            }
            else
            {
                try
                {
                    SqlQuery KT_SO_BENH_AN =
                        new Select().From(KcbBenhAn.Schema).
                            Where(KcbBenhAn.Columns.SoBenhAn).IsEqualTo(Utility.sDbnull(txtMaBenhAn.Text))
                            .And(KcbBenhAn.Columns.IdBnhan).IsNotEqualTo(Utility.sDbnull(txtMaBN.Text, -1));
                    if (KT_SO_BENH_AN.GetRecordCount() > 0)
                    {
                        MessageBox.Show("Mã bệnh đã được sử dụng, Kiểm tra lại?");
                    }
                    else
                    {
                        KcbBenhAn objBenhAnNgoaiTru = CreateBenhAnNgoaiTru();
                        objBenhAnNgoaiTru.Save();
                        MessageBox.Show("Sửa thành công");

                        if (chkDongSauKhiLuu.Checked) this.Close();
                        else
                        {

                        }

                        //this.Close();
                    }
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg(ex.ToString());
                }
            }
        }

        private void InsertPatient()
        {

            try
            {
                if (SuaBenhAn)
                {
                    if (string.IsNullOrEmpty(txtMaBenhAn.Text))
                    {
                        SinhMaBenhAn();
                    }
                }
                else
                {
                    SinhMaBenhAn();
                }

                MaBenhAnText = txtMaBenhAn.Text;
                SqlQuery KT_SO_BENH_AN =
                    new Select().From(KcbBenhAn.Schema).
                        Where(KcbBenhAn.Columns.SoBenhAn).IsEqualTo(Utility.sDbnull(txtMaBenhAn.Text));
                if (KT_SO_BENH_AN.GetRecordCount() > 0)
                {
                    MessageBox.Show("Mã bệnh đã được sử dụng, Kiểm tra lại?");
                }
                else
                {
                    KcbBenhAn objBenhAnNgoaiTru = CreateBenhAnNgoaiTru();
                    objBenhAnNgoaiTru.IsNew = true;
                    objBenhAnNgoaiTru.Save();
                    MessageBox.Show("Lưu Thành Công Thông Tin Bệnh Án");

                    if (chkDongSauKhiLuu.Checked) this.Close();
                    else
                    {
                        FindPatientID(txtMaBN.Text);
                        //cmdInBenhAn.Enabled = true;
                        //em_Action = action.Update;


                    }

                    // this.Close();
                }


            }
            catch (Exception)
            {

                throw;
            }
            //KcbBenhAn objBenhAnNgoaiTru = new CreateBenhAnNgoaiTru();

        }

        private KcbBenhAn CreateBenhAnNgoaiTru()
        {
            KcbBenhAn objBenhAnNgoaiTru = new KcbBenhAn();
            try
            {
                if (em_Action == action.Update)
                {

                    objBenhAnNgoaiTru.IsLoaded = true;
                    objBenhAnNgoaiTru.MarkOld();
                    objBenhAnNgoaiTru.Id = Utility.Int32Dbnull(txtID_BA.Text, -1);
                    objBenhAnNgoaiTru.NgaySua = globalVariables.SysDate;
                    objBenhAnNgoaiTru.NguoiSua = globalVariables.UserName;



                }
                if (em_Action == action.Insert)
                {
                    objBenhAnNgoaiTru.NguoiTao = globalVariables.UserName;
                    objBenhAnNgoaiTru.NgayTao = globalVariables.SysDate;
                }

                objBenhAnNgoaiTru.SoBenhAn = Utility.sDbnull(txtMaBenhAn.Text, -1);
                objBenhAnNgoaiTru.IdBnhan = Utility.Int32Dbnull(txtMaBN.Text);
                objBenhAnNgoaiTru.NgaySinh = Utility.sDbnull(txtNgaySinh.Text);
                objBenhAnNgoaiTru.ThangSinh = Utility.sDbnull(txtThangSinh.Text);
                objBenhAnNgoaiTru.DanToc = Utility.sDbnull(txtDanToc.Text);
                objBenhAnNgoaiTru.NoiLamviec = Utility.sDbnull(txtNoiLamViec.Text);
                objBenhAnNgoaiTru.ThongtinLhe = Utility.sDbnull(txtThongTinLienHe.Text);
                objBenhAnNgoaiTru.DthoaiLhe = Utility.sDbnull(txtDienThoai.Text);
                objBenhAnNgoaiTru.CdoanGioithieu = Utility.sDbnull(txtChanDoanBanDau.Text);
                objBenhAnNgoaiTru.NgayKham = Convert.ToDateTime(dtThoiDiemDkKham.Value);


                if (chkYTe.Checked) objBenhAnNgoaiTru.YTe = 1;
                else if (chkTuDen.Checked)
                {
                    objBenhAnNgoaiTru.YTe = 2;
                }
                else
                {
                    objBenhAnNgoaiTru.YTe = 3;
                }
                objBenhAnNgoaiTru.LdoVaovienMm = Utility.Int16Dbnull(chkldvvMetMoi.Checked ? 1 : 0);
                objBenhAnNgoaiTru.LdoVaovienGsc = Utility.Int16Dbnull(chkldvvGay.Checked ? 1 : 0);
                objBenhAnNgoaiTru.LdoVaovienKndn = Utility.Int16Dbnull(chkldvvKhat.Checked ? 1 : 0);
                objBenhAnNgoaiTru.LdoVaovienGtl = Utility.Int16Dbnull(chkldvvGiamThiLuc.Checked ? 1 : 0);
                objBenhAnNgoaiTru.LdoVaovienKhac = Utility.Int16Dbnull(chkldvvKhac.Checked ? 1 : 0);

                objBenhAnNgoaiTru.HbNam = Utility.sDbnull(txtNamChanDoanDTD.Text);
                objBenhAnNgoaiTru.HbNoiCdoan = Utility.sDbnull(txtNoiChanDoanDTD.Text);
                if (chkhbDeu.Checked) objBenhAnNgoaiTru.HbDieuTri = 1;
                else if (chkhbKhongDeu.Checked)
                {
                    objBenhAnNgoaiTru.HbDieuTri = 2;
                }
                else
                {
                    objBenhAnNgoaiTru.HbDieuTri = 3;
                }
                objBenhAnNgoaiTru.HbTiemInsulin = Utility.Int16Dbnull(chkhbTiemInsulin.Checked ? 1 : 0);
                objBenhAnNgoaiTru.HbInsulin1 = Utility.sDbnull(txtLuongInsulin.Text);
                objBenhAnNgoaiTru.HbInsulin2 = Utility.sDbnull(txthbCachDungInsulin.Text);
                objBenhAnNgoaiTru.HbThuochaDuonghuyet = Utility.sDbnull(txthbThuocHaDuongHuyet.Text);
                objBenhAnNgoaiTru.HbHa = Utility.sDbnull(txthbThuocHaHA.Text);
                objBenhAnNgoaiTru.HbRllp = Utility.sDbnull(txthbThuocRLLP.Text);
                objBenhAnNgoaiTru.HbThuocChongdong = Utility.sDbnull(txthbThuocChongDong.Text);
                objBenhAnNgoaiTru.HtaiMm = Utility.Int16Dbnull(chkhbMetMoi.Checked ? 1 : 0);
                objBenhAnNgoaiTru.HtaiGaysc = Utility.Int16Dbnull(chkhbGaySutCan.Checked ? 1 : 0);
                objBenhAnNgoaiTru.HtaiKgDau = Utility.sDbnull(txthbKgDau.Text);
                objBenhAnNgoaiTru.HtaiKgSau = Utility.sDbnull(txthbKgSau.Text);
                objBenhAnNgoaiTru.HtaiKhatnhieu = Utility.Int16Dbnull(chkhbKhat.Checked ? 1 : 0);
                objBenhAnNgoaiTru.HtaiUong = Utility.sDbnull(txthbSoLanUongNuoc.Text);
                objBenhAnNgoaiTru.HtaiDainhieu = Utility.Int16Dbnull(chkhbDai.Checked ? 1 : 0);
                objBenhAnNgoaiTru.HtaiDai = Utility.sDbnull(txthbSoLanDai.Text);
                objBenhAnNgoaiTru.HtaiGiamtl = Utility.Int16Dbnull(chkhbGiamThiLuc.Checked ? 1 : 0);
                objBenhAnNgoaiTru.HtaiKhac = Utility.sDbnull(chkhbKhac.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TsbBtChuaphathien = Utility.Int16Dbnull(chktsbChuaPhatHienBenh.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TsbBtNhoimaucotim = Utility.Int16Dbnull(chktsbNhoiMauCoTim.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TsbBtNamNmct = Utility.sDbnull(txttsbNamNhoiMauCoTim.Text);
                objBenhAnNgoaiTru.TsbBtTbmn = Utility.Int16Dbnull(chktsbTBMN.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TsbBtNamTbmn = Utility.sDbnull(txttsbNamTBMN.Text);
                objBenhAnNgoaiTru.TsbHaNam = Utility.sDbnull(txttsbTangHuyetAp.Text);
                objBenhAnNgoaiTru.TsbHaMax = Utility.sDbnull(txttsbHAmax.Text);
                objBenhAnNgoaiTru.TsbBtCON4000 = Utility.Int16Dbnull(chktsbDeConLonHon4000.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TsbBtKhac = Utility.sDbnull(txttsbKhac.Text);

                objBenhAnNgoaiTru.TsgdDtd = Utility.Int16Dbnull(chktsbDTD.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TsgdTanghuyetap = Utility.Int16Dbnull(chktsbTangHuyetAp.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TsbBtNhoimaucotim = Utility.Int16Dbnull(chktsgdNhoiMauCoTim.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TsgdKhac = Utility.sDbnull(txttsgdKhac.Text);

                if (chkkbMatNuocCo.Checked) objBenhAnNgoaiTru.KcbMatnuoc = 1;
                else if (chkkbMatNuocKhong.Checked)
                {
                    objBenhAnNgoaiTru.KcbMatnuoc = 2;
                }
                else
                {
                    objBenhAnNgoaiTru.KcbMatnuoc = 3;
                }
                if (chkkbXuatHuetDuoiDaCo.Checked) objBenhAnNgoaiTru.KcbXuathuyet = 1;
                else if (chkkbXuatHuyetDuoiDaKhong.Checked)
                {
                    objBenhAnNgoaiTru.KcbXuathuyet = 2;
                }
                else
                {
                    objBenhAnNgoaiTru.KcbXuathuyet = 3;
                }
                if (chkkbPhuCo.Checked) objBenhAnNgoaiTru.KcbPhu = 1;
                else if (chkkbPhuKhong.Checked)
                {
                    objBenhAnNgoaiTru.KcbPhu = 2;
                }
                else
                {
                    objBenhAnNgoaiTru.KcbPhu = 3;
                }

                objBenhAnNgoaiTru.KcbToanthanKhac = Utility.sDbnull(txtkbKhac.Text);
                objBenhAnNgoaiTru.KcbNhiptim = Utility.sDbnull(txtkbNhipTim.Text);
                if (chkkbNhipTimDeu.Checked) objBenhAnNgoaiTru.KcbTthaiNhiptim = 1;
                else if (chkkbNhipTimKhong.Checked)
                {
                    objBenhAnNgoaiTru.KcbTthaiNhiptim = 2;
                }
                else
                {
                    objBenhAnNgoaiTru.KcbTthaiNhiptim = 3;
                }
                objBenhAnNgoaiTru.KcbTiengtim = Utility.sDbnull(txtkbTiengTim.Text);
                objBenhAnNgoaiTru.KcbHohap = Utility.sDbnull(txtkbHoHap.Text);
                objBenhAnNgoaiTru.KcbBung = Utility.sDbnull(txtkbBung.Text);

                if (chkkbChanPhaiPXGXGiam.Checked) objBenhAnNgoaiTru.KcbChanphai = 1;
                else if (chkkbChanPhaiBinhThuong.Checked)
                {
                    objBenhAnNgoaiTru.KcbChanphai = 2;
                }
                else if (chkkbChanPhaiTang.Checked)
                {
                    objBenhAnNgoaiTru.KcbChanphai = 3;
                }
                else
                {
                    objBenhAnNgoaiTru.KcbChanphai = 0;
                }
                objBenhAnNgoaiTru.KcbChanphaiKhac = Utility.sDbnull(txtkbChanPhaiKhac.Text);
                if (chkkbChanTraiPXGXGiam.Checked) objBenhAnNgoaiTru.KcbChantrai = 1;
                else if (chkkbChanTraiBinhThuong.Checked)
                {
                    objBenhAnNgoaiTru.KcbChantrai = 2;
                }
                else if (chkkbChanTraiTang.Checked)
                {
                    objBenhAnNgoaiTru.KcbChantrai = 3;
                }
                else
                {
                    objBenhAnNgoaiTru.KcbChantrai = 0;
                }
                objBenhAnNgoaiTru.KcbChantraiKhac = Utility.sDbnull(txtkbChanTraiKhac.Text);
                objBenhAnNgoaiTru.KcbMatphai = Utility.sDbnull(txtkbMatPhai.Text);
                objBenhAnNgoaiTru.KcbMattrai = Utility.sDbnull(txtkbMatTrai.Text);
                objBenhAnNgoaiTru.KcbRanghammat = Utility.sDbnull(txtkbRangHamMat.Text);
                objBenhAnNgoaiTru.KcbTkinhKhac = Utility.sDbnull(txtkbKhac1.Text);
                objBenhAnNgoaiTru.KcbMach = Utility.sDbnull(txtMach.Text);
                objBenhAnNgoaiTru.KcbNhietdo = Utility.sDbnull(txtNhietDo.Text);
                objBenhAnNgoaiTru.KcbHuyetap1 = Utility.sDbnull(txtHuyetApTu.Text);
                objBenhAnNgoaiTru.KcbHuyetap2 = Utility.sDbnull(txtHuyetApDen.Text);
                objBenhAnNgoaiTru.KcbNhiptho = Utility.sDbnull(txtNhipTho.Text);
                objBenhAnNgoaiTru.KcbCannang = Utility.sDbnull(txtCanNang.Text);
                objBenhAnNgoaiTru.KcbChieucao = Utility.sDbnull(txtChieuCao.Text);
                objBenhAnNgoaiTru.KcbBmi = Utility.sDbnull(txtBMI.Text);
                objBenhAnNgoaiTru.TrangThai = 0;


                objBenhAnNgoaiTru.KcbTomtatClsChinh = Utility.sDbnull(txtkbTomTatKQCLSC.Text);
                objBenhAnNgoaiTru.KcbCdoanBdau = Utility.sDbnull(TXTKBChanDoanBD.Text);
                objBenhAnNgoaiTru.KcbDaXly = Utility.sDbnull(TXTKBDaXuLy.Text);
                objBenhAnNgoaiTru.KcbCdoanRavien = Utility.sDbnull(txtkbChanDoanRaVien.Text);
                objBenhAnNgoaiTru.KcbDtriTungay = Convert.ToDateTime(dtDieuTriNgoaiTruTu.Value);
                objBenhAnNgoaiTru.KcbDtriDenngay = Convert.ToDateTime(dtDieuTriNgoaiTruDen.Value);
                objBenhAnNgoaiTru.TkbaQtrinhBenhlyDbienCls = Utility.sDbnull(txtQuaTrinhBenhLy.Text);
                objBenhAnNgoaiTru.TkbaPphapDtri = Utility.sDbnull(txtTomTatCLS.Text);
                objBenhAnNgoaiTru.TenBenhChinh = Utility.sDbnull(txtBenhChinh.Text);
                objBenhAnNgoaiTru.TenBenhPhu = Utility.sDbnull(txtBenhPhu.Text);
                objBenhAnNgoaiTru.TkbaPphapDtri = Utility.sDbnull(txtPPDT.Text);
                objBenhAnNgoaiTru.TkbaTtRavien = Utility.sDbnull(txtTrangThaiRaVien.Text);
                objBenhAnNgoaiTru.TkbaHuongTtTieptheo = Utility.sDbnull(txtHuongTiepTheo.Text);
                objBenhAnNgoaiTru.DoiTuong = Utility.sDbnull(txtDoiTuong.Text);
                objBenhAnNgoaiTru.SoBhyt = Utility.sDbnull(txtSoBaoHiemYte.Text);
                objBenhAnNgoaiTru.DiaChi = Utility.sDbnull(txtDiaChi.Text);
                objBenhAnNgoaiTru.NamSinh = Utility.Int16Dbnull(txtNamSinh.Text);
                objBenhAnNgoaiTru.InPhieuLog = Utility.Int16Dbnull(0);
                objBenhAnNgoaiTru.LoaiBenhAn = Utility.Int16Dbnull(1);


                return objBenhAnNgoaiTru;
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
                return objBenhAnNgoaiTru;
                //throw;
            }


        }



        private void cmdSave_Click_1(object sender, EventArgs e)
        {
            if (!KiemTraDuLieu()) return;
            PerformAction();

        }

        private bool KiemTraDuLieu()
        {
            if (string.IsNullOrEmpty(txtMaBN.Text))
            {
                Utility.ShowMsg("Chưa chọn bệnh nhân ", "Thông báo", MessageBoxIcon.Warning);
                txtMaBN.Focus();

                return false;
            }


            if (txtMaBN.Text != MaBenhNhanText)
            {
                Utility.ShowMsg("Mã bệnh nhân đã bị thay đổi:Chọn lại Bệnh Nhân", "Thông báo", MessageBoxIcon.Warning);
                txtMaBN.Focus();

                return false;
            }
            if (SuaBenhAn)
            {


                if (txtMaBenhAn.Text != "")
                {
                    if (globalVariables.SO_BENH_AN == -1)
                    {
                        return true;
                    }
                    else
                    {


                        if (Utility.Int32Dbnull(txtMaBenhAn.Text) > globalVariables.SO_BENH_AN)
                        {
                            Utility.ShowMsg("Số Bệnh án phải nhỏ hơn " + globalVariables.SO_BENH_AN.ToString(),
                                            "Thông báo",
                                            MessageBoxIcon.Warning);

                            txtMaBenhAn.Focus();
                            txtMaBenhAn.SelectAll();
                            return false;
                        }
                    }
                }



            }

            return true;
        }

        private void cmdInBenhAn_Click_1(object sender, EventArgs e)
        {
            // kiểm tra chưa khóa : in

            DataTable dsTable = SPs.LayThongtinBenhanNgoaitru(txtMaBN.Text).GetDataSet().Tables[0];
            CrystalDecisions.CrystalReports.Engine.ReportDocument crpt;
            string tieude = "", reportname = "";
            crpt = Utility.GetReport("thamkham_BENH_AN_NGOAITRU", ref tieude, ref reportname);
            if (crpt == null) return;
            frmPrintPreview objForm = new frmPrintPreview("bệnh án", crpt, true, true);
            crpt.SetDataSource(dsTable);
            objForm.crptViewer.ReportSource = crpt;
            objForm.ShowDialog();
            KcbBenhAn objBenhAnNgoaiTru = new KcbBenhAn();
            objBenhAnNgoaiTru.IsLoaded = true;
            objBenhAnNgoaiTru.MarkOld();
            objBenhAnNgoaiTru.Id = Utility.Int32Dbnull(txtID_BA.Text, -1);
            // objBenhAnNgoaiTru.InPhieuLog = Utility.Int16Dbnull(1);
            objBenhAnNgoaiTru.NgayIn = globalVariables.SysDate;
            objBenhAnNgoaiTru.NguoiIn = globalVariables.UserName;
            objBenhAnNgoaiTru.Save();





        }

        private void txtMaBenhAn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;

            }
            else
            {
                SuaBenhAn = true;
            }



        }


        private void txtMaLanKham_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter && txtMaLanKham.Text.Trim() != "")
            {

                KcbChandoanKetluan objMaBn =
                    new Select("*").From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(
                        Utility.Int32Dbnull(txtMaLanKham.Text.Trim())).ExecuteSingle<KcbChandoanKetluan>();
                //  KcbBenhAn objBenhAnNgoaiTru = KcbBenhAn.FetchByID(Utility.Int32Dbnull(txtSoKham.Text));
                if (objMaBn != null)
                {
                    ClearControl();
                    txtMaBN.Text = Utility.sDbnull(objMaBn.IdBenhnhan, "");
                    MaBenhNhanText = txtMaBN.Text;
                    FindPatientID(txtMaBN.Text.Trim());
                    SuaBenhAn = false;

                }
                else
                {
                    MessageBox.Show("Mã lần khám không tồn tại,Kiểm tra lại ");
                    txtMaLanKham.Focus();
                }

            }

        }

        private void txtMaLanKham_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            //    e.Handled = true;
        }

        private void txtMaBN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtNgaySinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtThangSinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtNamSinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        //private void txtNamChanDoanDTD_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
        //        e.Handled = true;
        //}

        //private void txttsbNamNhoiMauCoTim_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
        //        e.Handled = true;
        //}

        //private void txttsbNamTBMN_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
        //        e.Handled = true;
        //}

        private void txtNgaySinh_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNgaySinh.Text))
            {
                if (txtNgaySinh.Text.Length > 2)
                {
                    Utility.ShowMsg("Ngày sinh phải nhỏ hơn 2 số", "Thông báo", MessageBoxIcon.Warning);
                    txtNgaySinh.Focus();
                    txtNgaySinh.SelectAll();
                }
            }
        }

        private void txtThangSinh_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtThangSinh.Text))
            {
                if (txtThangSinh.Text.Length > 2)
                {
                    Utility.ShowMsg("Tháng phải nhỏ hơn 2 số", "Thông báo", MessageBoxIcon.Warning);
                    txtThangSinh.Focus();
                    txtThangSinh.SelectAll();
                }
            }
        }

        private void txtNamSinh_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNamSinh.Text))
            {
                if ((txtNamSinh.Text.Length > 4) || (txtNamSinh.Text.Length < 4))
                {
                    Utility.ShowMsg("Năm sinh phải là 4 số", "Thông báo", MessageBoxIcon.Warning);
                    txtNamSinh.Focus();
                    txtNamSinh.SelectAll();
                }
            }
        }

        //private void txtNamChanDoanDTD_Leave(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txtNamChanDoanDTD.Text))
        //    {
        //        if ((txtNamChanDoanDTD.Text.Length > 4) || (txtNamChanDoanDTD.Text.Length < 4))
        //        {
        //            Utility.ShowMsg("Năm sinh phải là 4 số", "Thông báo", MessageBoxIcon.Warning);
        //            txtNamChanDoanDTD.Focus();
        //            txtNamChanDoanDTD.SelectAll();
        //        }
        //    }
        //}

        //private void txttsbNamNhoiMauCoTim_Leave(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txttsbNamNhoiMauCoTim.Text))
        //    {
        //        if ((txttsbNamNhoiMauCoTim.Text.Length > 4) || (txttsbNamNhoiMauCoTim.Text.Length < 4))
        //        {
        //            Utility.ShowMsg("Năm sinh phải là 4 số", "Thông báo", MessageBoxIcon.Warning);
        //            txttsbNamNhoiMauCoTim.Focus();
        //            txttsbNamNhoiMauCoTim.SelectAll();
        //        }
        //    }
        //}

        //private void txttsbNamTBMN_Leave(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txttsbNamTBMN.Text))
        //    {
        //        if ((txttsbNamTBMN.Text.Length > 4) || (txttsbNamTBMN.Text.Length < 4))
        //        {
        //            Utility.ShowMsg("Năm sinh phải là 4 số", "Thông báo", MessageBoxIcon.Warning);
        //            txttsbNamTBMN.Focus();
        //            txttsbNamTBMN.SelectAll();
        //        }
        //    }
        //}

        private void AutoDongFrom()
        {
            try
            {
                Try2CreateFolder();
                AllowTextChanged = false;
                using (StreamReader _reader = new StreamReader(strBenhAn))
                {
                    chkDongSauKhiLuu.Checked = _reader.ReadLine().Trim() == "1";
                    _reader.BaseStream.Flush();
                    _reader.Close();
                }
            }
            catch
            {
            }
            finally
            {
                AllowTextChanged = true;
            }
        }

        private void Try2CreateFolder()
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(strBenhAn)))
                    Directory.CreateDirectory(Path.GetDirectoryName(strBenhAn));
            }
            catch
            {
            }
        }

        private void chkInSauKhiLuu_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                if (!AllowTextChanged) return;
                using (StreamWriter _writer = new StreamWriter(strBenhAn))
                {
                    _writer.WriteLine(chkDongSauKhiLuu.Checked ? "1" : "0");
                    _writer.Flush();
                    _writer.Close();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lưu trạng thái-->" + ex.Message);
            }
        }

        private void txtMaBenhAn_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaBenhAn_KeyDown(object sender, KeyEventArgs e)
        {
            //ClearControl();
            if (e.KeyCode == Keys.Enter && txtMaBenhAn.Text.Trim() != "")
            {
                KcbBenhAn objMa_Benh_An =
                    new Select("*").From(KcbBenhAn.Schema).Where(KcbBenhAn.Columns.SoBenhAn).IsEqualTo(
                        Utility.Int32Dbnull(txtMaBenhAn.Text.Trim())).ExecuteSingle<KcbBenhAn>();
                //  KcbBenhAn objBenhAnNgoaiTru = KcbBenhAn.FetchByID(Utility.Int32Dbnull(txtSoKham.Text));
                if (objMa_Benh_An != null)
                {
                    txtMaBN.Text = Utility.sDbnull(objMa_Benh_An.IdBnhan, "");
                    MaBenhNhanText = txtMaBN.Text;
                    FindPatientID(txtMaBN.Text.Trim());
                    SuaBenhAn = false;

                }
                else
                {
                    MessageBox.Show("Không Tồn Số Bệnh Án Vừa Nhập ");
                    txtMaBenhAn.Focus();

                }

            }

        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            em_Action = action.Delete;
            PerformAction();

        }

        private void ClearControl()
        {

            txtNgaySinh.Clear();
            txtThangSinh.Clear();
            txtDienThoai.Clear();
            txtDanToc.Clear();
            txtThongTinLienHe.Clear();
            txtChanDoanBanDau.Clear();
            txtNoiLamViec.Clear();

            foreach (Control control in grpThongTinBenhNhan.Controls)
            {
                if (control is EditBox)
                {
                    var txtControl = new EditBox();
                    txtControl = ((EditBox)control);
                    txtControl.Clear();
                }
                if (control is CalendarCombo)
                {
                    var txtControl = new CalendarCombo();
                    txtControl = ((CalendarCombo)control);
                    txtControl.NullValue = true;
                }

                if (control is UICheckBox)
                {
                    var txtControl = new UICheckBox();
                    txtControl = ((UICheckBox)control);
                    txtControl.Checked = false;
                }
                if (control is MaskedEditBox)
                {
                    var txtControl = new MaskedEditBox();
                    txtControl = ((MaskedEditBox)control);
                    txtControl.Clear();
                }
                if (control is RichTextBox)
                {
                    var txtControl = new RichTextBox();
                    txtControl = ((RichTextBox)control);
                    txtControl.Clear();
                }

            }
        }

    }
}
  
  