using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.CalendarCombo;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX.EditControls;
using SubSonic;
using VMS.HIS.DAL;
using VNS.HIS.UI.Forms.BenhAn.UCs;
using VNS.Libs;
using VNS.Properties;

namespace VNS.HIS.UI.BENH_AN
{
    public partial class frm_BENHAN_NGOAITRU : Form
    {
        private readonly string strBenhAn = Application.StartupPath + @"\CAUHINH\BAn_NTru_DongSauLuu.txt";
        private bool AllowTextChanged = false;
        private string MaBenhAnText = "";
        public string loaibenhan = "";
        public action m_enAct = action.Insert;
        private string strIdBenhnhan = "";
        private string strMaluotkham = "";
        public uc_bant_1 uc_bant_11;
        public uc_bant_2 uc_bant_21;
        public uc_bant_3 uc_bant_31;
        public uc_bant_4 uc_bant_41;

        public frm_BENHAN_NGOAITRU()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            uc_bant_11 = new uc_bant_1();
            uc_bant_21 = new uc_bant_2();
            uc_bant_31 = new uc_bant_3();
            uc_bant_41 = new uc_bant_4();

            //pnlPanel.Controls.Add(uc_bant_11);
            //pnlPanel.Controls.Add(uc_bant_21);
            //pnlPanel.Controls.Add(uc_bant_31);
            //pnlPanel.Controls.Add(uc_bant_41);

            tabPage1.Controls.Add(uc_bant_11);
            tabPage2.Controls.Add(uc_bant_21);
            tabPage3.Controls.Add(uc_bant_31);
            tabPage4.Controls.Add(uc_bant_41);

            uc_bant_11.txtMaBN.KeyDown += txtMaBN_KeyDown;
            uc_bant_11.txtMaLanKham.KeyDown += txtMaLanKham_KeyDown;
            uc_bant_41.chkkbChanPhaiPXGXGiam.CheckedChanged += chkkbChanPhaiPXGXGiam_CheckedChanged;
            uc_bant_41.chkkbChanPhaiBinhThuong.CheckedChanged += chkkbChanPhaiBinhThuong_CheckedChanged;
            uc_bant_41.chkkbChanPhaiTang.CheckedChanged += chkkbChanPhaiTang_CheckedChanged;
            uc_bant_41.chkkbChanTraiPXGXGiam.CheckedChanged += chkkbChanTraiPXGXGiam_CheckedChanged;
            uc_bant_41.chkkbChanTraiBinhThuong.CheckedChanged += chkkbChanTraiBinhThuong_CheckedChanged;
            uc_bant_41.chkkbChanTraiTang.CheckedChanged += chkkbChanTraiTang_CheckedChanged;
            uc_bant_31.chkhbDeu.CheckedChanged += chkhbDeu_CheckedChanged;
            uc_bant_31.chkhbKhongDeu.CheckedChanged += chkhbKhongDeu_CheckedChanged;
            uc_bant_31.chkhbTiemInsulin.CheckedChanged += chkhbTiemInsulin_CheckedChanged;
            uc_bant_31.chkhbGaySutCan.CheckedChanged += chkhbGaySutCan_CheckedChanged;
            uc_bant_31.chkhbKhat.CheckedChanged += chkhbKhat_CheckedChanged;
            uc_bant_31.chkhbDai.CheckedChanged += chkhbDai_CheckedChanged;
            uc_bant_31.chktsbNhoiMauCoTim.CheckedChanged += chktsbNhoiMauCoTim_CheckedChanged;
            uc_bant_31.chktsbTBMN.CheckedChanged += chktsbTBMN_CheckedChanged;
            uc_bant_41.chkkbMatNuocCo.CheckedChanged += chkkbMatNuocCo_CheckedChanged;
            uc_bant_41.chkkbMatNuocKhong.CheckedChanged += chkkbMatNuocKhong_CheckedChanged;
            uc_bant_41.chkkbXuatHuetDuoiDaCo.CheckedChanged += chkkbXuatHuetDuoiDaCo_CheckedChanged;
            uc_bant_41.chkkbXuatHuyetDuoiDaKhong.CheckedChanged += chkkbXuatHuyetDuoiDaKhong_CheckedChanged;
            uc_bant_41.chkkbPhuCo.CheckedChanged += chkkbPhuCo_CheckedChanged;
            uc_bant_41.chkkbPhuKhong.CheckedChanged += chkkbPhuKhong_CheckedChanged;
            uc_bant_41.chkkbNhipTimDeu.CheckedChanged += chkkbNhipTimDeu_CheckedChanged;
            uc_bant_41.chkkbNhipTimKhong.CheckedChanged += chkkbNhipTimKhong_CheckedChanged;
            cmdConfig.Click += cmdConfig_Click;
            uc_bant_11.txtMaBenhAn.KeyDown += txtMaBenhAn_KeyDown;
            uc_bant_11.cmdSearch.Click += cmdSearch_Click;
            // cmdDelete.Click += cmdDelete_Click;
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            txtMaBenhAn_KeyDown(uc_bant_11.txtMaBenhAn, new KeyEventArgs(Keys.Enter));
        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            var _Properties = new frm_Properties(PropertyLib._BenhAnProperties);
            _Properties.ShowDialog();
        }

        private void txtMaBN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "", false);
                if (uc_bant_11.txtMaBN.Text.Trim() != "")
                {
                    string _temp = uc_bant_11.txtMaBN.Text.Trim();
                    ClearControl();
                    uc_bant_11.txtMaBN.Text = _temp;
                    FindPatientID(uc_bant_11.txtMaBN.Text.Trim());
                }
                else
                {
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"],
                                   "Bạn phải nhập mã bệnh nhân trước khi nhấn Enter để tìm kiếm", true);
                }
            }
        }

        // LẤY THÔNG TIN BỆNH ÁN CỦA BỆNH NHÂN
        private void LayThongTinBenhNhan(string id_benhnhan)
        {
            try
            {
                DataTable temdt = SPs.KcbLaythongtinBenhnhan(Utility.Int32Dbnull(id_benhnhan, 0)).GetDataSet().Tables[0];
                if (temdt != null && temdt.Rows.Count > 0)
                {
                    uc_bant_11.txtDiaChi.Text = Utility.sDbnull(temdt.Rows[0][VKcbLuotkham.Columns.DiaChi], "");
                    uc_bant_11.txtHoTen.Text = Utility.sDbnull(temdt.Rows[0][VKcbLuotkham.Columns.TenBenhnhan], "");
                    uc_bant_11.txtNamSinh.Text = Utility.sDbnull(temdt.Rows[0][VKcbLuotkham.Columns.NamSinh], "");
                    uc_bant_11.cboGioiTinh.SelectedValue = Utility.sDbnull(
                        temdt.Rows[0][VKcbLuotkham.Columns.GioiTinh], "");
                    uc_bant_11.txtNgheNghiep.Text = Utility.sDbnull(temdt.Rows[0][VKcbLuotkham.Columns.NgheNghiep], "");
                    uc_bant_11.dtThoiDiemDkKham.Text = Utility.sDbnull(temdt.Rows[0][VKcbLuotkham.Columns.NgayTiepdon]);
                    uc_bant_11.txtDanToc.Text = Utility.sDbnull(VKcbLuotkham.Columns.DanToc);
                    strIdBenhnhan = id_benhnhan;
                    uc_bant_11.txtDoiTuong.Text =
                        Utility.sDbnull(temdt.Rows[0][VKcbLuotkham.Columns.MaDoituongKcb].ToString());
                    uc_bant_11.dtInsToDate.Text = temdt.Rows[0][VKcbLuotkham.Columns.NgayketthucBhyt].ToString();
                    uc_bant_11.txtSoBaoHiemYte.Text =
                        Utility.sDbnull(temdt.Rows[0][VKcbLuotkham.Columns.MatheBhyt].ToString());
                }
                else
                {
                    MessageBox.Show("Không tồn tại mã bệnh nhân vừa nhập");
                    uc_bant_11.txtMaBN.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm thông tin BN:\n" + ex.Message);
                uc_bant_11.txtMaBN.Focus();
            }
        }

        //TÌM BỆNH NHAN THEO MÃ BỆNH NHÂN
        private void FindPatientID(string id_benhnhan)
        {
            LayThongTinBenhNhan(uc_bant_11.txtMaBN.Text);
            uc_bant_41.dtDieuTriNgoaiTruTu.Value = uc_bant_41.dtDieuTriNgoaiTruDen.Value = DateTime.Now;
            try
            {
                var _KcbBenhAn =
                    new Select().From(KcbBenhAn.Schema).
                        Where(KcbBenhAn.Columns.IdBnhan).IsEqualTo(Utility.sDbnull(uc_bant_11.txtMaBN.Text)).
                        ExecuteSingle<KcbBenhAn>();

                //Đã Tồn tại thông tin bệnh án ngoại trú. Sửa
                if (_KcbBenhAn != null)
                {
                    m_enAct = action.Update;
                    LayDulieubenh_an(_KcbBenhAn);
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bệnh nhân đã có Bệnh án-->Sửa", true);
                    Text =
                        string.Format(
                            "Bạn đang thực hiện sửa Bệnh án Đái tháo đường cho Bệnh nhân Id={0} -  Mã lượt khám={1} - Số BA={2} - Id BA={3}",
                            uc_bant_11.txtMaBN.Text, uc_bant_11.txtMaLanKham.Text, uc_bant_11.txtMaBenhAn.Text,
                            uc_bant_11.txtID_BA.Text);
                    uc_bant_11.txtNgaySinh.Focus();
                    MaBenhAnText = uc_bant_11.txtMaBenhAn.Text;
                    cmdInBenhAn.Enabled = true;
                }
                    //chưa tồn tại thông tin ngoại tru. Them
                else
                {
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bệnh nhân chưa đã có Bệnh án-->Thêm mới", true);
                    Text =
                        string.Format(
                            "Bạn đang thực hiện tạo Bệnh án Đái tháo đường cho Bệnh nhân Id={0} -  Mã lượt khám={1} ",
                            uc_bant_11.txtMaBN.Text, uc_bant_11.txtMaLanKham.Text);
                    m_enAct = action.Insert;
                    uc_bant_11.txtMaBenhAn.Clear();
                    uc_bant_11.txtNgaySinh.Focus();
                    // MaBenhAnText = uc_bant_11.txtMaBenhAn.Text;
                    cmdInBenhAn.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
            }
        }

        private void frm_BENHAN_NGOAITRU_Load(object sender, EventArgs e)
        {
            if (uc_bant_11.txtMaBN.Text.Trim() == "")
            {
                uc_bant_11.txtMaBN.SelectAll();
                uc_bant_11.txtMaBN.Focus();
            }
            else
            {
                SinhMaBenhAn();
                strIdBenhnhan = uc_bant_11.txtMaBN.Text;
                LayThongTinBenhNhan(uc_bant_11.txtMaBN.Text);
                uc_bant_41.dtDieuTriNgoaiTruTu.Value = uc_bant_41.dtDieuTriNgoaiTruDen.Value = DateTime.Now;
                if (m_enAct == action.Insert)
                {
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bệnh nhân chưa đã có Bệnh án-->Thêm mới", true);
                    Text =
                        string.Format(
                            "Bạn đang thực hiện tạo Bệnh án Đái tháo đường cho Bệnh nhân Id={0} -  Mã lượt khám={1}",
                            uc_bant_11.txtMaBN.Text, uc_bant_11.txtMaLanKham.Text);
                    cmdInBenhAn.Enabled = false;
                    uc_bant_11.txtMaBenhAn.Focus();
                }
                if (m_enAct == action.Update)
                {
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bệnh nhân đã có Bệnh án-->Sửa", true);
                    Text =
                        string.Format(
                            "Bạn đang thực hiện tạo Bệnh án Đái tháo đường cho Bệnh nhân Id={0} -  Mã lượt khám={1}",
                            uc_bant_11.txtMaBN.Text, uc_bant_11.txtMaLanKham.Text);
                    LayDulieubenh_an(null);
                    cmdInBenhAn.Enabled = true;
                    MaBenhAnText = uc_bant_11.txtMaBenhAn.Text;
                    uc_bant_11.txtMaBenhAn.Focus();
                }
            }
        }

        //SINH MÃ BỆNH ÁN
        private void SinhMaBenhAn()
        {
            if (m_enAct == action.Insert)
            {
                uc_bant_11.txtMaBenhAn.Text = THU_VIEN_CHUNG.SinhMaBenhAn(loaibenhan);
            }
        }

        // LAY DU LIEU BENH AN

        private void LayDulieubenh_an(KcbBenhAn objBenhAnNgoaiTru)
        {
            try
            {
                if (objBenhAnNgoaiTru == null)
                    objBenhAnNgoaiTru =
                        new Select().From(KcbBenhAn.Schema).Where(KcbBenhAn.Columns.IdBnhan).IsEqualTo(
                            Utility.Int32Dbnull(uc_bant_11.txtMaBN.Text.Trim())).ExecuteSingle<KcbBenhAn>();
                if (objBenhAnNgoaiTru != null)
                {
                    m_enAct = action.Update;
                    uc_bant_11.txtID_BA.Text = Utility.sDbnull(objBenhAnNgoaiTru.Id, "");
                    uc_bant_11.txtMaBenhAn.Text = Utility.sDbnull(objBenhAnNgoaiTru.SoBenhAn, -1);
                    uc_bant_11.txtMaBN.Text = Utility.sDbnull(objBenhAnNgoaiTru.IdBnhan, "");

                    uc_bant_11.txtNgaySinh.Text = Utility.sDbnull(objBenhAnNgoaiTru.NgaySinh, "");
                    uc_bant_11.txtThangSinh.Text = Utility.sDbnull(objBenhAnNgoaiTru.ThangSinh, "");
                    uc_bant_11.txtDanToc.Text = Utility.sDbnull(objBenhAnNgoaiTru.DanToc, "");
                    uc_bant_11.txtNoiLamViec.Text = Utility.sDbnull(objBenhAnNgoaiTru.NoiLamviec, "");
                    uc_bant_11.txtThongTinLienHe.Text = Utility.sDbnull(objBenhAnNgoaiTru.ThongtinLhe, "");
                    uc_bant_11.txtDienThoai.Text = Utility.sDbnull(objBenhAnNgoaiTru.DthoaiLhe, "");
                    uc_bant_11.txtChanDoanBanDau.Text = Utility.sDbnull(objBenhAnNgoaiTru.CdoanGioithieu, "");
                    uc_bant_11.chkYTe.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.YTe) == 1;
                    uc_bant_11.chkTuDen.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.YTe) == 2;
                    uc_bant_11.dtThoiDiemDkKham.Value = Convert.ToDateTime(objBenhAnNgoaiTru.NgayKham);

                    uc_bant_21.chkldvvMetMoi.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.LdoVaovienMm) == 1;
                    uc_bant_21.chkldvvGay.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.LdoVaovienGsc) == 1;
                    uc_bant_21.chkldvvKhat.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.LdoVaovienKndn) == 1;
                    uc_bant_21.chkldvvGiamThiLuc.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.LdoVaovienGtl) == 1;
                    uc_bant_21.chkldvvKhac.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.LdoVaovienKhac) == 1;

                    uc_bant_31.txtNamChanDoanDTD.Text = Utility.sDbnull(objBenhAnNgoaiTru.HbNam, "");
                    uc_bant_31.txtNoiChanDoanDTD.Text = Utility.sDbnull(objBenhAnNgoaiTru.HbNoiCdoan, "");
                    uc_bant_31.chkhbDeu.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.HbDieuTri) == 1;
                    uc_bant_31.chkhbKhongDeu.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.HbDieuTri) == 2;
                    uc_bant_31.chkhbTiemInsulin.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.HbTiemInsulin) == 1;
                    uc_bant_31.txtLuongInsulin.Text = Utility.sDbnull(objBenhAnNgoaiTru.HbInsulin1, "");
                    uc_bant_31.txthbCachDungInsulin.Text = Utility.sDbnull(objBenhAnNgoaiTru.HbInsulin2, "");
                    uc_bant_31.txthbThuocHaDuongHuyet.Text = Utility.sDbnull(objBenhAnNgoaiTru.HbThuochaDuonghuyet, "");
                    uc_bant_31.txthbThuocHaHA.Text = Utility.sDbnull(objBenhAnNgoaiTru.HbHa, "");
                    uc_bant_31.txthbThuocRLLP.Text = Utility.sDbnull(objBenhAnNgoaiTru.HbRllp, "");
                    uc_bant_31.txthbThuocChongDong.Text = Utility.sDbnull(objBenhAnNgoaiTru.HbThuocChongdong, "");
                    uc_bant_31.chkhbMetMoi.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.HtaiMm) == 1;
                    uc_bant_31.chkhbGaySutCan.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.HtaiGaysc) == 1;
                    uc_bant_31.txthbKgDau.Text = Utility.sDbnull(objBenhAnNgoaiTru.HtaiKgDau, "");
                    uc_bant_31.txthbKgSau.Text = Utility.sDbnull(objBenhAnNgoaiTru.HtaiKgSau, "");
                    uc_bant_31.chkhbKhat.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.HtaiKhatnhieu) == 1;
                    uc_bant_31.txthbSoLanUongNuoc.Text = Utility.sDbnull(objBenhAnNgoaiTru.HtaiUong, "");
                    uc_bant_31.chkhbDai.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.HtaiDainhieu) == 1;
                    uc_bant_31.txthbSoLanDai.Text = Utility.sDbnull(objBenhAnNgoaiTru.HtaiDai, "");
                    uc_bant_31.chkhbGiamThiLuc.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.HtaiGiamtl) == 1;
                    uc_bant_31.chkhbKhac.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.HtaiKhac) == 1;
                    uc_bant_31.chktsbChuaPhatHienBenh.Checked =
                        Utility.Int16Dbnull(objBenhAnNgoaiTru.TsbBtChuaphathien) == 1;
                    uc_bant_31.chktsbNhoiMauCoTim.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TsbBtNhoimaucotim) ==
                                                            1;
                    uc_bant_31.txttsbNamNhoiMauCoTim.Text = Utility.sDbnull(objBenhAnNgoaiTru.TsbBtNamNmct, "");
                    uc_bant_31.chktsbTBMN.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TsbBtTbmn) == 1;
                    uc_bant_31.txttsbNamTBMN.Text = Utility.sDbnull(objBenhAnNgoaiTru.TsbBtNamTbmn, "");
                    uc_bant_31.txttsbTangHuyetAp.Text = Utility.sDbnull(objBenhAnNgoaiTru.TsbHaNam, "");
                    uc_bant_31.txttsbHAmax.Text = Utility.sDbnull(objBenhAnNgoaiTru.TsbHaMax, "");
                    uc_bant_31.chktsbDeConLonHon4000.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TsbBtCON4000) == 1;
                    uc_bant_31.txttsbKhac.Text = Utility.sDbnull(objBenhAnNgoaiTru.TsbBtKhac, "");
                    uc_bant_31.chktsbDTD.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TsgdDtd) == 1;
                    uc_bant_31.chktsbTangHuyetAp.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TsgdTanghuyetap) == 1;
                    uc_bant_31.chktsgdNhoiMauCoTim.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TsgdNhoimaucotim) ==
                                                             1;
                    uc_bant_31.txttsgdKhac.Text = Utility.sDbnull(objBenhAnNgoaiTru.TsgdKhac, "");

                    uc_bant_41.chkkbMatNuocCo.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbMatnuoc) == 1;
                    uc_bant_41.chkkbMatNuocKhong.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbMatnuoc) == 2;
                    uc_bant_41.chkkbXuatHuetDuoiDaCo.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbXuathuyet) == 1;
                    uc_bant_41.chkkbXuatHuyetDuoiDaKhong.Checked =
                        Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbXuathuyet) == 2;
                    uc_bant_41.chkkbPhuCo.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbPhu) == 1;
                    uc_bant_41.chkkbPhuKhong.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbPhu) == 2;
                    uc_bant_41.txtkbKhac.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbToanthanKhac);
                    uc_bant_41.txtkbNhipTim.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbNhiptim);
                    uc_bant_41.chkkbNhipTimDeu.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbTthaiNhiptim) == 1;
                    uc_bant_41.chkkbNhipTimKhong.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbTthaiNhiptim) == 2;
                    uc_bant_41.txtkbTiengTim.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbTiengtim);
                    uc_bant_41.txtkbHoHap.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbHohap);
                    uc_bant_41.txtkbBung.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbBung);
                    uc_bant_41.chkkbChanPhaiPXGXGiam.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbChanphai) == 1;
                    uc_bant_41.chkkbChanPhaiBinhThuong.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbChanphai) == 2;
                    uc_bant_41.chkkbChanPhaiTang.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbChanphai) == 3;
                    uc_bant_41.txtkbChanPhaiKhac.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbChanphaiKhac);
                    uc_bant_41.chkkbChanTraiPXGXGiam.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbChantrai) == 1;
                    uc_bant_41.chkkbChanTraiBinhThuong.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbChantrai) == 2;
                    uc_bant_41.chkkbChanTraiTang.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.KcbChantrai) == 3;
                    uc_bant_41.txtkbChanTraiKhac.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbChantraiKhac);
                    uc_bant_41.txtkbMatPhai.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbMatphai);
                    uc_bant_41.txtkbMatTrai.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbMattrai);
                    uc_bant_41.txtkbRangHamMat.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbRanghammat);
                    uc_bant_41.txtkbKhac1.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbTkinhKhac);
                    uc_bant_41.txtMach.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbMach, "");
                    uc_bant_41.txtNhietDo.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbNhietdo);
                    uc_bant_41.txtHuyetApTu.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbHuyetap1);
                    uc_bant_41.txtHuyetApDen.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbHuyetap2);
                    uc_bant_41.txtNhipTho.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbNhiptho);
                    uc_bant_41.txtCanNang.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbCannang);
                    uc_bant_41.txtChieuCao.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbChieucao);
                    uc_bant_41.txtBMI.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbBmi);


                    uc_bant_41.txtkbTomTatKQCLSC.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbTomtatClsChinh);
                    uc_bant_41.TXTKBChanDoanBD.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbCdoanBdau);
                    uc_bant_41.TXTKBDaXuLy.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbDaXly);
                    uc_bant_41.txtkbChanDoanRaVien.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbCdoanRavien);
                    uc_bant_41.dtDieuTriNgoaiTruTu.Value = Convert.ToDateTime(objBenhAnNgoaiTru.KcbDtriTungay);
                    uc_bant_41.dtDieuTriNgoaiTruDen.Value = Convert.ToDateTime(objBenhAnNgoaiTru.KcbDtriDenngay);
                    uc_bant_41.txtQuaTrinhBenhLy.Text = Utility.sDbnull(objBenhAnNgoaiTru.TkbaQtrinhBenhlyDbienCls);
                    uc_bant_41.txtTomTatCLS.Text = Utility.sDbnull(objBenhAnNgoaiTru.TkbaPphapDtri);
                    uc_bant_41.txtBenhChinh.Text = Utility.sDbnull(objBenhAnNgoaiTru.TenBenhChinh);
                    uc_bant_41.txtBenhPhu.Text = Utility.sDbnull(objBenhAnNgoaiTru.TenBenhPhu);
                    uc_bant_41.txtPPDT.Text = Utility.sDbnull(objBenhAnNgoaiTru.TkbaPphapDtri);
                    uc_bant_41.txtTrangThaiRaVien.Text = Utility.sDbnull(objBenhAnNgoaiTru.TkbaTtRavien);
                    uc_bant_41.txtHuongTiepTheo.Text = Utility.sDbnull(objBenhAnNgoaiTru.TkbaHuongTtTieptheo);
                    uc_bant_11.txtDoiTuong.Text = Utility.sDbnull(objBenhAnNgoaiTru.DoiTuong);
                    uc_bant_11.txtSoBaoHiemYte.Text = Utility.sDbnull(objBenhAnNgoaiTru.SoBhyt);
                    uc_bant_11.txtDiaChi.Text = Utility.sDbnull(objBenhAnNgoaiTru.DiaChi);

                    if (Utility.sDbnull(objBenhAnNgoaiTru.NguoiTao, "") != "")
                    {
                        //LStaff objNguoiTao =
                        //    new Select("*").From(LStaff.Schema).Where(LStaff.Columns.Uid).IsEqualTo(
                        //        Utility.sDbnull(objBenhAnNgoaiTru.NguoiTao)).ExecuteSingle<LStaff>();
                        //label95.Text = Utility.sDbnull(objNguoiTao.StaffName);
                    }
                    Text =
                        string.Format(
                            "Bạn đang thực hiện sửa Bệnh án Đái tháo đường cho Bệnh nhân Id={0} -  Mã lượt khám={1} - Số BA={2} - Id BA={3}",
                            uc_bant_11.txtMaBN.Text, uc_bant_11.txtMaLanKham.Text, uc_bant_11.txtMaBenhAn.Text,
                            uc_bant_11.txtID_BA.Text);
                }
                else
                {
                    m_enAct = action.Insert;
                    Text =
                        string.Format(
                            "Bạn đang thực hiện sửa Bệnh án Đái tháo đường cho Bệnh nhân Id={0} -  Mã lượt khám={1} - Số BA={2} - Id BA={3}",
                            uc_bant_11.txtMaBN.Text, uc_bant_11.txtMaLanKham.Text, uc_bant_11.txtMaBenhAn.Text,
                            uc_bant_11.txtID_BA.Text);
                    MessageBox.Show("Không lấy được thông tin bệnh án");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm thông tin BN:\n" + ex.Message);
            }
        }


        private void chkkbChanPhaiPXGXGiam_CheckedChanged(object sender, EventArgs e)
        {
            if (uc_bant_41.chkkbChanPhaiPXGXGiam.Checked)
            {
                uc_bant_41.chkkbChanPhaiBinhThuong.Checked = false;
                uc_bant_41.chkkbChanPhaiTang.Checked = false;
            }
        }

        private void chkkbChanPhaiBinhThuong_CheckedChanged(object sender, EventArgs e)
        {
            if (uc_bant_41.chkkbChanPhaiBinhThuong.Checked)
            {
                uc_bant_41.chkkbChanPhaiPXGXGiam.Checked = false;
                uc_bant_41.chkkbChanPhaiTang.Checked = false;
            }
        }

        private void chkkbChanPhaiTang_CheckedChanged(object sender, EventArgs e)
        {
            if (uc_bant_41.chkkbChanPhaiTang.Checked)
            {
                uc_bant_41.chkkbChanPhaiPXGXGiam.Checked = false;
                uc_bant_41.chkkbChanPhaiBinhThuong.Checked = false;
            }
        }

        private void chkkbChanTraiPXGXGiam_CheckedChanged(object sender, EventArgs e)
        {
            if (uc_bant_41.chkkbChanTraiPXGXGiam.Checked)
            {
                uc_bant_41.chkkbChanTraiBinhThuong.Checked = false;
                uc_bant_41.chkkbChanTraiTang.Checked = false;
            }
        }

        private void chkkbChanTraiBinhThuong_CheckedChanged(object sender, EventArgs e)
        {
            if (uc_bant_41.chkkbChanTraiBinhThuong.Checked)
            {
                uc_bant_41.chkkbChanTraiPXGXGiam.Checked = false;
                uc_bant_41.chkkbChanTraiTang.Checked = false;
            }
        }

        private void chkkbChanTraiTang_CheckedChanged(object sender, EventArgs e)
        {
            if (uc_bant_41.chkkbChanTraiTang.Checked)
            {
                uc_bant_41.chkkbChanTraiPXGXGiam.Checked = false;
                uc_bant_41.chkkbChanTraiBinhThuong.Checked = false;
            }
        }

        private void chkhbDeu_CheckedChanged(object sender, EventArgs e)
        {
            if (uc_bant_31.chkhbDeu.Checked)
                uc_bant_31.chkhbKhongDeu.Checked = false;
        }

        private void chkhbKhongDeu_CheckedChanged(object sender, EventArgs e)
        {
            if (uc_bant_31.chkhbKhongDeu.Checked)
                uc_bant_31.chkhbDeu.Checked = false;
        }

        private void chkhbTiemInsulin_CheckedChanged(object sender, EventArgs e)
        {
            if (uc_bant_31.chkhbTiemInsulin.Checked)
            {
                uc_bant_31.txtLuongInsulin.Visible = true;
                uc_bant_31.txthbCachDungInsulin.Visible = true;
                uc_bant_31.txtLuongInsulin.Focus();
            }
            else
            {
                uc_bant_31.txtLuongInsulin.Visible = false;
                uc_bant_31.txthbCachDungInsulin.Visible = false;
            }
        }

        private void chkhbGaySutCan_CheckedChanged(object sender, EventArgs e)
        {
            if (uc_bant_31.chkhbGaySutCan.Checked)
            {
                uc_bant_31.txthbKgDau.Visible = true;
                uc_bant_31.txthbKgSau.Visible = true;
                uc_bant_31.txthbKgDau.Focus();
            }
            else
            {
                uc_bant_31.txthbKgDau.Visible = false;
                uc_bant_31.txthbKgSau.Visible = false;
            }
        }

        private void chkhbKhat_CheckedChanged(object sender, EventArgs e)
        {
            if (uc_bant_31.chkhbKhat.Checked)
            {
                uc_bant_31.txthbSoLanUongNuoc.Visible = true;
                uc_bant_31.txthbSoLanUongNuoc.Focus();
            }
            else
            {
                uc_bant_31.txthbSoLanUongNuoc.Visible = false;
            }
        }

        private void chkhbDai_CheckedChanged(object sender, EventArgs e)
        {
            if (uc_bant_31.chkhbDai.Checked)
            {
                uc_bant_31.txthbSoLanDai.Visible = true;
                uc_bant_31.txthbSoLanDai.Focus();
            }
            else
            {
                uc_bant_31.txthbSoLanDai.Visible = false;
            }
        }

        private void frm_BENHAN_NGOAITRU_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
            if (e.Control && e.KeyCode == Keys.P) cmdInBenhAn.PerformClick();
            if (e.KeyCode == Keys.Escape) Close();
            if (e.KeyCode == Keys.F1)
            {
                tabControl1.SelectedTab = tabPage1;
                uc_bant_11.txtMaBN.SelectAll();
                uc_bant_11.txtMaBN.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                tabControl1.SelectedTab = tabPage2;
                uc_bant_21.chkldvvMetMoi.Focus();
            }
            if (e.KeyCode == Keys.F3)
            {
                tabControl1.SelectedTab = tabPage3;
                uc_bant_31.txtNamChanDoanDTD.SelectAll();
                uc_bant_31.txtNamChanDoanDTD.Focus();
            }
            if (e.KeyCode == Keys.F4)
            {
                tabControl1.SelectedTab = tabPage4;
                uc_bant_41.txtkbNhipTim.SelectAll();
                uc_bant_41.txtkbNhipTim.Focus();
            }
        }

        private void chktsbNhoiMauCoTim_CheckedChanged(object sender, EventArgs e)
        {
            if (uc_bant_31.chktsbNhoiMauCoTim.Checked)
            {
                uc_bant_31.txttsbNamNhoiMauCoTim.Visible = true;
                uc_bant_31.txttsbNamNhoiMauCoTim.Focus();
            }
            else
            {
                uc_bant_31.txttsbNamNhoiMauCoTim.Visible = false;
            }
        }

        private void chktsbTBMN_CheckedChanged(object sender, EventArgs e)
        {
            if (uc_bant_31.chktsbTBMN.Checked)
            {
                uc_bant_31.txttsbNamTBMN.Visible = true;
                uc_bant_31.txttsbNamTBMN.Focus();
            }
            else
            {
                uc_bant_31.txttsbNamTBMN.Visible = false;
            }
        }

        private void chkkbMatNuocCo_CheckedChanged(object sender, EventArgs e)
        {
            if (uc_bant_41.chkkbMatNuocCo.Checked)
                uc_bant_41.chkkbMatNuocKhong.Checked = false;
        }

        private void chkkbMatNuocKhong_CheckedChanged(object sender, EventArgs e)
        {
            if (uc_bant_41.chkkbMatNuocKhong.Checked)
                uc_bant_41.chkkbMatNuocCo.Checked = false;
        }

        private void chkkbXuatHuetDuoiDaCo_CheckedChanged(object sender, EventArgs e)
        {
            if (uc_bant_41.chkkbXuatHuetDuoiDaCo.Checked)
                uc_bant_41.chkkbXuatHuyetDuoiDaKhong.Checked = false;
        }

        private void chkkbXuatHuyetDuoiDaKhong_CheckedChanged(object sender, EventArgs e)
        {
            if (uc_bant_41.chkkbXuatHuyetDuoiDaKhong.Checked)
                uc_bant_41.chkkbXuatHuetDuoiDaCo.Checked = false;
        }

        private void chkkbPhuCo_CheckedChanged(object sender, EventArgs e)
        {
            if (uc_bant_41.chkkbPhuCo.Checked)
                uc_bant_41.chkkbPhuKhong.Checked = false;
        }

        private void chkkbPhuKhong_CheckedChanged(object sender, EventArgs e)
        {
            if (uc_bant_41.chkkbPhuKhong.Checked)
                uc_bant_41.chkkbPhuCo.Checked = false;
        }

        private void chkkbNhipTimDeu_CheckedChanged(object sender, EventArgs e)
        {
            if (uc_bant_41.chkkbNhipTimDeu.Checked)
                uc_bant_41.chkkbNhipTimKhong.Checked = false;
        }

        private void chkkbNhipTimKhong_CheckedChanged(object sender, EventArgs e)
        {
            if (uc_bant_41.chkkbNhipTimKhong.Checked)
                uc_bant_41.chkkbNhipTimDeu.Checked = false;
        }


        private void PerformAction()
        {
            switch (m_enAct)
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
                try
                {
                    new Delete().From(KcbBenhAn.Schema).Where(KcbBenhAn.Columns.SoBenhAn).IsEqualTo(
                        Utility.sDbnull(uc_bant_11.txtMaBenhAn.Text)).Execute();
                    m_enAct = action.Insert;
                    uc_bant_11.txtID_BA.Clear();
                    uc_bant_11.txtMaBenhAn.Clear();
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn đã xóa bệnh án cho bệnh nhân thành công", false);
                }
                catch (Exception)
                {
                }
            }
        }

        private void UpdatePatient()
        {
            SqlQuery KT_IN_BENH_AN =
                new Select().From(KcbBenhAn.Schema).
                    Where(KcbBenhAn.Columns.InPhieuLog).IsEqualTo(Utility.Int16Dbnull(1)).And(
                        KcbBenhAn.Columns.IdBnhan).IsEqualTo(Utility.Int32Dbnull(strIdBenhnhan, -1));
            if (KT_IN_BENH_AN.GetRecordCount() > 0)
            {
                MessageBox.Show("Bệnh nhân này đã được in bệnh án ngoại trú, vui lòng mở khóa bệnh án để chỉnh sửa");
            }
            else
            {
                try
                {
                    SqlQuery KT_SO_BENH_AN =
                        new Select().From(KcbBenhAn.Schema).
                            Where(KcbBenhAn.Columns.SoBenhAn).IsEqualTo(Utility.sDbnull(uc_bant_11.txtMaBenhAn.Text))
                            .And(KcbBenhAn.Columns.IdBnhan).IsNotEqualTo(Utility.sDbnull(uc_bant_11.txtMaBN.Text, -1));
                    if (KT_SO_BENH_AN.GetRecordCount() > 0)
                    {
                        MessageBox.Show(
                            string.Format("Mã bệnh án {0} đã được sử dụng cho bệnh nhân khác. Mời bạn kiểm tra lại",
                                          uc_bant_11.txtMaBenhAn.Text));
                    }
                    else
                    {
                        KcbBenhAn objBenhAnNgoaiTru = CreateBenhAnNgoaiTru();
                        objBenhAnNgoaiTru.Save();
                        MessageBox.Show("Sửa bệnh án thành công");

                        if (PropertyLib._BenhAnProperties.Tudongthoatformngaysaukhiluu) Close();
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
                if (string.IsNullOrEmpty(uc_bant_11.txtMaBenhAn.Text))
                {
                    SinhMaBenhAn();
                }
                MaBenhAnText = uc_bant_11.txtMaBenhAn.Text;
                SqlQuery KT_SO_BENH_AN =
                    new Select().From(KcbBenhAn.Schema).
                        Where(KcbBenhAn.Columns.SoBenhAn).IsEqualTo(Utility.sDbnull(uc_bant_11.txtMaBenhAn.Text));
                if (KT_SO_BENH_AN.GetRecordCount() > 0)
                {
                    MessageBox.Show("Mã bệnh án đã được sử dụng, Kiểm tra lại?");
                }
                else
                {
                    KcbBenhAn objBenhAnNgoaiTru = CreateBenhAnNgoaiTru();
                    objBenhAnNgoaiTru.IsNew = true;
                    objBenhAnNgoaiTru.Save();
                    MessageBox.Show("Lưu bệnh án thành công");

                    if (PropertyLib._BenhAnProperties.Tudongthoatformngaysaukhiluu) Close();
                    else
                    {
                        FindPatientID(uc_bant_11.txtMaBN.Text);
                        //cmdInBenhAn.Enabled = true;
                        //m_enAct = action.Update;
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
            var objBenhAnNgoaiTru = new KcbBenhAn();
            try
            {
                if (m_enAct == action.Update)
                {
                    objBenhAnNgoaiTru = KcbBenhAn.FetchByID(uc_bant_11.txtID_BA.Text);
                    objBenhAnNgoaiTru.IsNew = false;
                    objBenhAnNgoaiTru.MarkOld();
                    objBenhAnNgoaiTru.NgaySua = globalVariables.SysDate;
                    objBenhAnNgoaiTru.NguoiSua = globalVariables.UserName;
                }
                if (m_enAct == action.Insert)
                {
                    objBenhAnNgoaiTru.NguoiTao = globalVariables.UserName;
                    objBenhAnNgoaiTru.NgayTao = globalVariables.SysDate;
                }

                objBenhAnNgoaiTru.SoBenhAn = Utility.sDbnull(uc_bant_11.txtMaBenhAn.Text, -1);
                objBenhAnNgoaiTru.LoaiBa = Utility.sDbnull(loaibenhan);
                objBenhAnNgoaiTru.MaLuotkham = Utility.sDbnull(uc_bant_11.txtMaLanKham.Text);
                objBenhAnNgoaiTru.IdBnhan = Utility.Int32Dbnull(uc_bant_11.txtMaBN.Text);
                objBenhAnNgoaiTru.NgaySinh = Utility.sDbnull(uc_bant_11.txtNgaySinh.Text);
                objBenhAnNgoaiTru.ThangSinh = Utility.sDbnull(uc_bant_11.txtThangSinh.Text);
                objBenhAnNgoaiTru.DanToc = Utility.sDbnull(uc_bant_11.txtDanToc.Text);
                objBenhAnNgoaiTru.NoiLamviec = Utility.sDbnull(uc_bant_11.txtNoiLamViec.Text);
                objBenhAnNgoaiTru.ThongtinLhe = Utility.sDbnull(uc_bant_11.txtThongTinLienHe.Text);
                objBenhAnNgoaiTru.DthoaiLhe = Utility.sDbnull(uc_bant_11.txtDienThoai.Text);
                objBenhAnNgoaiTru.CdoanGioithieu = Utility.sDbnull(uc_bant_11.txtChanDoanBanDau.Text);
                objBenhAnNgoaiTru.NgayKham = Convert.ToDateTime(uc_bant_11.dtThoiDiemDkKham.Value);


                if (uc_bant_11.chkYTe.Checked) objBenhAnNgoaiTru.YTe = 1;
                else if (uc_bant_11.chkTuDen.Checked)
                {
                    objBenhAnNgoaiTru.YTe = 2;
                }
                else
                {
                    objBenhAnNgoaiTru.YTe = 3;
                }
                objBenhAnNgoaiTru.LdoVaovienMm = Utility.Int16Dbnull(uc_bant_21.chkldvvMetMoi.Checked ? 1 : 0);
                objBenhAnNgoaiTru.LdoVaovienGsc = Utility.Int16Dbnull(uc_bant_21.chkldvvGay.Checked ? 1 : 0);
                objBenhAnNgoaiTru.LdoVaovienKndn = Utility.Int16Dbnull(uc_bant_21.chkldvvKhat.Checked ? 1 : 0);
                objBenhAnNgoaiTru.LdoVaovienGtl = Utility.Int16Dbnull(uc_bant_21.chkldvvGiamThiLuc.Checked ? 1 : 0);
                objBenhAnNgoaiTru.LdoVaovienKhac = Utility.Int16Dbnull(uc_bant_21.chkldvvKhac.Checked ? 1 : 0);

                objBenhAnNgoaiTru.HbNam = Utility.sDbnull(uc_bant_31.txtNamChanDoanDTD.Text);
                objBenhAnNgoaiTru.HbNoiCdoan = Utility.sDbnull(uc_bant_31.txtNoiChanDoanDTD.Text);
                if (uc_bant_31.chkhbDeu.Checked) objBenhAnNgoaiTru.HbDieuTri = 1;
                else if (uc_bant_31.chkhbKhongDeu.Checked)
                {
                    objBenhAnNgoaiTru.HbDieuTri = 2;
                }
                else
                {
                    objBenhAnNgoaiTru.HbDieuTri = 3;
                }
                objBenhAnNgoaiTru.HbTiemInsulin = Utility.Int16Dbnull(uc_bant_31.chkhbTiemInsulin.Checked ? 1 : 0);
                objBenhAnNgoaiTru.HbInsulin1 = Utility.sDbnull(uc_bant_31.txtLuongInsulin.Text);
                objBenhAnNgoaiTru.HbInsulin2 = Utility.sDbnull(uc_bant_31.txthbCachDungInsulin.Text);
                objBenhAnNgoaiTru.HbThuochaDuonghuyet = Utility.sDbnull(uc_bant_31.txthbThuocHaDuongHuyet.Text);
                objBenhAnNgoaiTru.HbHa = Utility.sDbnull(uc_bant_31.txthbThuocHaHA.Text);
                objBenhAnNgoaiTru.HbRllp = Utility.sDbnull(uc_bant_31.txthbThuocRLLP.Text);
                objBenhAnNgoaiTru.HbThuocChongdong = Utility.sDbnull(uc_bant_31.txthbThuocChongDong.Text);
                objBenhAnNgoaiTru.HtaiMm = Utility.Int16Dbnull(uc_bant_31.chkhbMetMoi.Checked ? 1 : 0);
                objBenhAnNgoaiTru.HtaiGaysc = Utility.Int16Dbnull(uc_bant_31.chkhbGaySutCan.Checked ? 1 : 0);
                objBenhAnNgoaiTru.HtaiKgDau = Utility.sDbnull(uc_bant_31.txthbKgDau.Text);
                objBenhAnNgoaiTru.HtaiKgSau = Utility.sDbnull(uc_bant_31.txthbKgSau.Text);
                objBenhAnNgoaiTru.HtaiKhatnhieu = Utility.Int16Dbnull(uc_bant_31.chkhbKhat.Checked ? 1 : 0);
                objBenhAnNgoaiTru.HtaiUong = Utility.sDbnull(uc_bant_31.txthbSoLanUongNuoc.Text);
                objBenhAnNgoaiTru.HtaiDainhieu = Utility.Int16Dbnull(uc_bant_31.chkhbDai.Checked ? 1 : 0);
                objBenhAnNgoaiTru.HtaiDai = Utility.sDbnull(uc_bant_31.txthbSoLanDai.Text);
                objBenhAnNgoaiTru.HtaiGiamtl = Utility.Int16Dbnull(uc_bant_31.chkhbGiamThiLuc.Checked ? 1 : 0);
                objBenhAnNgoaiTru.HtaiKhac = Utility.sDbnull(uc_bant_31.chkhbKhac.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TsbBtChuaphathien =
                    Utility.Int16Dbnull(uc_bant_31.chktsbChuaPhatHienBenh.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TsbBtNhoimaucotim = Utility.Int16Dbnull(uc_bant_31.chktsbNhoiMauCoTim.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TsbBtNamNmct = Utility.sDbnull(uc_bant_31.txttsbNamNhoiMauCoTim.Text);
                objBenhAnNgoaiTru.TsbBtTbmn = Utility.Int16Dbnull(uc_bant_31.chktsbTBMN.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TsbBtNamTbmn = Utility.sDbnull(uc_bant_31.txttsbNamTBMN.Text);
                objBenhAnNgoaiTru.TsbHaNam = Utility.sDbnull(uc_bant_31.txttsbTangHuyetAp.Text);
                objBenhAnNgoaiTru.TsbHaMax = Utility.sDbnull(uc_bant_31.txttsbHAmax.Text);
                objBenhAnNgoaiTru.TsbBtCON4000 = Utility.Int16Dbnull(uc_bant_31.chktsbDeConLonHon4000.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TsbBtKhac = Utility.sDbnull(uc_bant_31.txttsbKhac.Text);

                objBenhAnNgoaiTru.TsgdDtd = Utility.Int16Dbnull(uc_bant_31.chktsbDTD.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TsgdTanghuyetap = Utility.Int16Dbnull(uc_bant_31.chktsbTangHuyetAp.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TsgdNhoimaucotim = Utility.Int16Dbnull(uc_bant_31.chktsgdNhoiMauCoTim.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TsgdKhac = Utility.sDbnull(uc_bant_31.txttsgdKhac.Text);

                if (uc_bant_41.chkkbMatNuocCo.Checked) objBenhAnNgoaiTru.KcbMatnuoc = 1;
                else if (uc_bant_41.chkkbMatNuocKhong.Checked)
                {
                    objBenhAnNgoaiTru.KcbMatnuoc = 2;
                }
                else
                {
                    objBenhAnNgoaiTru.KcbMatnuoc = 3;
                }
                if (uc_bant_41.chkkbXuatHuetDuoiDaCo.Checked) objBenhAnNgoaiTru.KcbXuathuyet = 1;
                else if (uc_bant_41.chkkbXuatHuyetDuoiDaKhong.Checked)
                {
                    objBenhAnNgoaiTru.KcbXuathuyet = 2;
                }
                else
                {
                    objBenhAnNgoaiTru.KcbXuathuyet = 3;
                }
                if (uc_bant_41.chkkbPhuCo.Checked) objBenhAnNgoaiTru.KcbPhu = 1;
                else if (uc_bant_41.chkkbPhuKhong.Checked)
                {
                    objBenhAnNgoaiTru.KcbPhu = 2;
                }
                else
                {
                    objBenhAnNgoaiTru.KcbPhu = 3;
                }

                objBenhAnNgoaiTru.KcbToanthanKhac = Utility.sDbnull(uc_bant_41.txtkbKhac.Text);
                objBenhAnNgoaiTru.KcbNhiptim = Utility.sDbnull(uc_bant_41.txtkbNhipTim.Text);
                if (uc_bant_41.chkkbNhipTimDeu.Checked) objBenhAnNgoaiTru.KcbTthaiNhiptim = 1;
                else if (uc_bant_41.chkkbNhipTimKhong.Checked)
                {
                    objBenhAnNgoaiTru.KcbTthaiNhiptim = 2;
                }
                else
                {
                    objBenhAnNgoaiTru.KcbTthaiNhiptim = 3;
                }
                objBenhAnNgoaiTru.KcbTiengtim = Utility.sDbnull(uc_bant_41.txtkbTiengTim.Text);
                objBenhAnNgoaiTru.KcbHohap = Utility.sDbnull(uc_bant_41.txtkbHoHap.Text);
                objBenhAnNgoaiTru.KcbBung = Utility.sDbnull(uc_bant_41.txtkbBung.Text);

                if (uc_bant_41.chkkbChanPhaiPXGXGiam.Checked) objBenhAnNgoaiTru.KcbChanphai = 1;
                else if (uc_bant_41.chkkbChanPhaiBinhThuong.Checked)
                {
                    objBenhAnNgoaiTru.KcbChanphai = 2;
                }
                else if (uc_bant_41.chkkbChanPhaiTang.Checked)
                {
                    objBenhAnNgoaiTru.KcbChanphai = 3;
                }
                else
                {
                    objBenhAnNgoaiTru.KcbChanphai = 0;
                }
                objBenhAnNgoaiTru.KcbChanphaiKhac = Utility.sDbnull(uc_bant_41.txtkbChanPhaiKhac.Text);
                if (uc_bant_41.chkkbChanTraiPXGXGiam.Checked) objBenhAnNgoaiTru.KcbChantrai = 1;
                else if (uc_bant_41.chkkbChanTraiBinhThuong.Checked)
                {
                    objBenhAnNgoaiTru.KcbChantrai = 2;
                }
                else if (uc_bant_41.chkkbChanTraiTang.Checked)
                {
                    objBenhAnNgoaiTru.KcbChantrai = 3;
                }
                else
                {
                    objBenhAnNgoaiTru.KcbChantrai = 0;
                }
                objBenhAnNgoaiTru.KcbChantraiKhac = Utility.sDbnull(uc_bant_41.txtkbChanTraiKhac.Text);
                objBenhAnNgoaiTru.KcbMatphai = Utility.sDbnull(uc_bant_41.txtkbMatPhai.Text);
                objBenhAnNgoaiTru.KcbMattrai = Utility.sDbnull(uc_bant_41.txtkbMatTrai.Text);
                objBenhAnNgoaiTru.KcbRanghammat = Utility.sDbnull(uc_bant_41.txtkbRangHamMat.Text);
                objBenhAnNgoaiTru.KcbTkinhKhac = Utility.sDbnull(uc_bant_41.txtkbKhac1.Text);
                objBenhAnNgoaiTru.KcbMach = Utility.sDbnull(uc_bant_41.txtMach.Text);
                objBenhAnNgoaiTru.KcbNhietdo = Utility.sDbnull(uc_bant_41.txtNhietDo.Text);
                objBenhAnNgoaiTru.KcbHuyetap1 = Utility.sDbnull(uc_bant_41.txtHuyetApTu.Text);
                objBenhAnNgoaiTru.KcbHuyetap2 = Utility.sDbnull(uc_bant_41.txtHuyetApDen.Text);
                objBenhAnNgoaiTru.KcbNhiptho = Utility.sDbnull(uc_bant_41.txtNhipTho.Text);
                objBenhAnNgoaiTru.KcbCannang = Utility.sDbnull(uc_bant_41.txtCanNang.Text);
                objBenhAnNgoaiTru.KcbChieucao = Utility.sDbnull(uc_bant_41.txtChieuCao.Text);
                objBenhAnNgoaiTru.KcbBmi = Utility.sDbnull(uc_bant_41.txtBMI.Text);
                objBenhAnNgoaiTru.TrangThai = 0;


                objBenhAnNgoaiTru.KcbTomtatClsChinh = Utility.sDbnull(uc_bant_41.txtkbTomTatKQCLSC.Text);
                objBenhAnNgoaiTru.KcbCdoanBdau = Utility.sDbnull(uc_bant_41.TXTKBChanDoanBD.Text);
                objBenhAnNgoaiTru.KcbDaXly = Utility.sDbnull(uc_bant_41.TXTKBDaXuLy.Text);
                objBenhAnNgoaiTru.KcbCdoanRavien = Utility.sDbnull(uc_bant_41.txtkbChanDoanRaVien.Text);
                objBenhAnNgoaiTru.KcbDtriTungay = Convert.ToDateTime(uc_bant_41.dtDieuTriNgoaiTruTu.Value);
                objBenhAnNgoaiTru.KcbDtriDenngay = Convert.ToDateTime(uc_bant_41.dtDieuTriNgoaiTruDen.Value);
                objBenhAnNgoaiTru.TkbaQtrinhBenhlyDbienCls = Utility.sDbnull(uc_bant_41.txtQuaTrinhBenhLy.Text);
                objBenhAnNgoaiTru.TkbaPphapDtri = Utility.sDbnull(uc_bant_41.txtTomTatCLS.Text);
                objBenhAnNgoaiTru.TenBenhChinh = Utility.sDbnull(uc_bant_41.txtBenhChinh.Text);
                objBenhAnNgoaiTru.TenBenhPhu = Utility.sDbnull(uc_bant_41.txtBenhPhu.Text);
                objBenhAnNgoaiTru.TkbaPphapDtri = Utility.sDbnull(uc_bant_41.txtPPDT.Text);
                objBenhAnNgoaiTru.TkbaTtRavien = Utility.sDbnull(uc_bant_41.txtTrangThaiRaVien.Text);
                objBenhAnNgoaiTru.TkbaHuongTtTieptheo = Utility.sDbnull(uc_bant_41.txtHuongTiepTheo.Text);
                objBenhAnNgoaiTru.DoiTuong = Utility.sDbnull(uc_bant_11.txtDoiTuong.Text);
                objBenhAnNgoaiTru.SoBhyt = Utility.sDbnull(uc_bant_11.txtSoBaoHiemYte.Text);
                objBenhAnNgoaiTru.DiaChi = Utility.sDbnull(uc_bant_11.txtDiaChi.Text);
                objBenhAnNgoaiTru.NamSinh = Utility.Int16Dbnull(uc_bant_11.txtNamSinh.Text);
                objBenhAnNgoaiTru.InPhieuLog = Utility.Int16Dbnull(0);
                objBenhAnNgoaiTru.LoaiBenhAn = Utility.Int16Dbnull(1);


                return objBenhAnNgoaiTru;
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi tạo dữ liệu bệnh án ngoại trú", ex);
                return null;
            }
        }


        private void cmdSave_Click_1(object sender, EventArgs e)
        {
            if (!KiemTraDuLieu()) return;
            PerformAction();
        }

        private bool KiemTraDuLieu()
        {
            Utility.SetMsg(uiStatusBar1.Panels["MSG"], "", false);
            if (string.IsNullOrEmpty(uc_bant_11.txtMaBN.Text))
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Thông tin mã bệnh nhân không được để trống", true);
                uc_bant_11.txtMaBN.Focus();

                return false;
            }
            if (uc_bant_11.txtMaBN.Text != strIdBenhnhan)
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"],
                               "Mã bệnh nhân đã bị thay đổi-->Mời bạn nhập lại mã bệnh nhân và nhấn phím Enter để nạp lại thông tin",
                               true);
                uc_bant_11.txtMaBN.Focus();
                return false;
            }
            if (m_enAct == action.Update)
            {
                if (uc_bant_11.txtMaBenhAn.Text == "")
                {
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"],
                                   "Bạn đang ở chế độ sửa thông tin nên Mã bệnh án không được phép để trống", true);
                    uc_bant_11.txtMaBenhAn.Focus();
                    return false;
                }
            }

            if (Utility.Int32Dbnull(uc_bant_11.txtMaBenhAn.Text) <=
                Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_SO_BENH_AN", false), -1))
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"],
                               "Số Bệnh án phải lớn hơn số khởi tạo: " +
                               THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_SO_BENH_AN", false), true);

                uc_bant_11.txtMaBenhAn.Focus();
                uc_bant_11.txtMaBenhAn.SelectAll();
                return false;
            }

            return true;
        }

        private void cmdInBenhAn_Click_1(object sender, EventArgs e)
        {
            DataTable dsTable = SPs.KcbThamkhamLaythongtinBenhanNgoaitru(uc_bant_11.txtMaBN.Text).GetDataSet().Tables[0];
            THU_VIEN_CHUNG.CreateXML(dsTable, "thamkham_BENH_AN_NGOAITRU");
            ReportDocument crpt;
            string tieude = "", reportname = "";
            crpt = Utility.GetReport("thamkham_BENH_AN_NGOAITRU", ref tieude, ref reportname);
            if (crpt == null) return;
            var objForm = new frmPrintPreview("bệnh án", crpt, true, true);
            crpt.SetDataSource(dsTable);
            objForm.mv_sReportFileName = Path.GetFileName(reportname);
            objForm.mv_sReportCode = "thamkham_BENH_AN_NGOAITRU";
            Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
            Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
            Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
            Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
            Utility.SetParameterValue(crpt, "sTitleReport", tieude);
            Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
            objForm.crptViewer.ReportSource = crpt;
            objForm.ShowDialog();
            var objBenhAnNgoaiTru = new KcbBenhAn();
            objBenhAnNgoaiTru.IsLoaded = true;
            objBenhAnNgoaiTru.MarkOld();
            objBenhAnNgoaiTru.Id = Utility.Int32Dbnull(uc_bant_11.txtID_BA.Text, -1);
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
        }


        private void txtMaLanKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "", false);
                if (uc_bant_11.txtMaLanKham.Text.Trim() != "")
                {
                    string _temp = uc_bant_11.txtMaLanKham.Text.Trim();
                    ClearControl();
                    uc_bant_11.txtMaLanKham.Text = _temp;
                    var objMaBn =
                        new Select().From(KcbLuotkham.Schema).Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(
                            Utility.Int32Dbnull(uc_bant_11.txtMaLanKham.Text.Trim())).ExecuteSingle<KcbLuotkham>();
                    if (objMaBn != null)
                    {
                        uc_bant_11.txtMaBN.Text = Utility.sDbnull(objMaBn.IdBenhnhan, "");
                        strIdBenhnhan = uc_bant_11.txtMaBN.Text;
                        FindPatientID(uc_bant_11.txtMaBN.Text.Trim());
                    }
                    else
                    {
                        Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Mã lần khám không tồn tại-->Mời bạn kiểm tra lại",
                                       true);
                        uc_bant_11.txtMaLanKham.Focus();
                    }
                }
                else
                {
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"],
                                   "Bạn phải nhập mã lượt khám trước khi nhấn Enter để tìm kiếm", true);
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


        private void txtMaBenhAn_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtMaBenhAn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "", false);
                if (uc_bant_11.txtMaBenhAn.Text.Trim() != "")
                {
                    string _temp = uc_bant_11.txtMaBenhAn.Text.Trim();
                    ClearControl();
                    uc_bant_11.txtMaBenhAn.Text = _temp;
                    var objMa_Benh_An =
                        new Select().From(KcbBenhAn.Schema).Where(KcbBenhAn.Columns.SoBenhAn).IsEqualTo(
                            Utility.Int32Dbnull(uc_bant_11.txtMaBenhAn.Text.Trim())).ExecuteSingle<KcbBenhAn>();
                    if (objMa_Benh_An != null)
                    {
                        uc_bant_11.txtMaBN.Text = Utility.sDbnull(objMa_Benh_An.IdBnhan, "");
                        strIdBenhnhan = uc_bant_11.txtMaBN.Text;
                        FindPatientID(uc_bant_11.txtMaBN.Text.Trim());
                    }
                    else
                    {
                        Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Không tìm được Bệnh án theo số bạn nhập ", true);
                        uc_bant_11.txtMaBenhAn.Focus();
                    }
                }
                else
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải nhập số bệnh án nếu muốn tìm kiếm", true);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            m_enAct = action.Delete;
            PerformAction();
        }

        private void ClearControlOfUc(Control parent)
        {
            try
            {
                foreach (Control control in parent.Controls)
                {
                    if (control is EditBox)
                    {
                        var txtControl = new EditBox();
                        txtControl = ((EditBox) control);
                        txtControl.Clear();
                    }
                    if (control is CalendarCombo)
                    {
                        var txtControl = new CalendarCombo();
                        txtControl = ((CalendarCombo) control);
                        txtControl.NullValue = true;
                    }

                    if (control is UICheckBox)
                    {
                        var txtControl = new UICheckBox();
                        txtControl = ((UICheckBox) control);
                        txtControl.Checked = false;
                    }
                    if (control is MaskedEditBox)
                    {
                        var txtControl = new MaskedEditBox();
                        txtControl = ((MaskedEditBox) control);
                        txtControl.Clear();
                    }
                    if (control is RichTextBox)
                    {
                        var txtControl = new RichTextBox();
                        txtControl = ((RichTextBox) control);
                        txtControl.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void ClearControl()
        {
            uc_bant_11.txtNgaySinh.Clear();
            uc_bant_11.txtThangSinh.Clear();
            uc_bant_11.txtDienThoai.Clear();
            uc_bant_11.txtDanToc.Clear();
            uc_bant_11.txtThongTinLienHe.Clear();
            uc_bant_11.txtChanDoanBanDau.Clear();
            uc_bant_11.txtNoiLamViec.Clear();
            ClearControlOfUc(uc_bant_11);
            ClearControlOfUc(uc_bant_21);
            ClearControlOfUc(uc_bant_31);
            ClearControlOfUc(uc_bant_41);
        }
    }
}