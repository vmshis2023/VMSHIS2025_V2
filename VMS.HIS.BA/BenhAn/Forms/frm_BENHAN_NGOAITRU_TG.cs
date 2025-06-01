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
using VMS.HIS.DAL;
using System.Windows.Forms;
using SubSonic;

namespace VNS.HIS.UI.BENH_AN
{
    public partial class frm_BENHAN_NGOAITRU_TG : Form
    {
        public action em_Action = action.Insert;
        private bool SuaBenhAn = false;
        private string MaBenhAnText = "";
        private string MaBenhNhanText = "";
        private string strBenhAn = Application.StartupPath + @"\CAUHINH\BAn_NTru_DongSauLuu.txt";
        private bool AllowTextChanged = false;
        public string loaibenhan = "";

        public frm_BENHAN_NGOAITRU_TG()
        {

            InitializeComponent();
            Utility.SetVisualStyle(this);
            txtMaBN.KeyDown += txtMaBN_KeyDown;
            txtMaLanKham.KeyDown += txtMaLanKham_KeyDown;
            chkla.CheckedChanged += chkla_CheckedChanged;
            chklb.CheckedChanged += chklb_CheckedChanged;
            chkll.CheckedChanged+= chkll_CheckedChanged;
            chklll.CheckedChanged+= chklll_CheckedChanged;
            chkMatDoMem.CheckedChanged+= chkMatDoMem_CheckedChanged;
            chkMatDoChac.CheckedChanged += chkMatDoChac_CheckedChanged;
            chkMatDoChac.CheckedChanged += chkMatDoChac_CheckedChanged;
            chkhbGaySutCan.CheckedChanged += chkhbGaySutCan_CheckedChanged;
            chkRoiLoanTieuHoa.CheckedChanged += chkRoiLoanTieuHoa_CheckedChanged;
            //frm_BENHAN_NGOAITRU_TG.KeyDown+= frm_BENHAN_NGOAITRU_TG_KeyDown;
            chkkbDaNongAmCo.CheckedChanged+= chkkbDaNongAmCo_CheckedChanged;
            chkkbDaNongAmKhong.CheckedChanged+= chkkbDaNongAmKhong_CheckedChanged;
            chkkbCungMacMatVangCo.CheckedChanged+= chkkbCungMacMatVangCo_CheckedChanged;
            chkkbCungMacMatVangKhong.CheckedChanged+= chkkbCungMacMatVangKhong_CheckedChanged;
            chkkbRunTayCo.CheckedChanged+= chkkbRunTayCo_CheckedChanged;
            chkkbRunTayKhong.CheckedChanged+= chkkbRunTayKhong_CheckedChanged;
            chkkbNhipTimDeu.CheckedChanged+= chkkbNhipTimDeu_CheckedChanged;
            chkkbNhipTimKhong.CheckedChanged+= chkkbNhipTimKhong_CheckedChanged;
            chkBenhLyDaDayCo.CheckedChanged+= chkBenhLyDaDayCo_CheckedChanged;
            chkBenhLyDaDayKhong.CheckedChanged+= chkBenhLyDaDayKhong_CheckedChanged;
            chkKinhNguyetDeu.CheckedChanged+= chkKinhNguyetDeu_CheckedChanged;
            chkKinhNguyetKhong.CheckedChanged+= chkKinhNguyetKhong_CheckedChanged;
            txtMaLanKham.KeyPress+= txtMaLanKham_KeyPress;
            txtMaBN.KeyPress+= txtMaBN_KeyPress;
            txtNgaySinh.KeyPress+= txtNgaySinh_KeyPress;
            txtThangSinh.KeyPress+= txtThangSinh_KeyPress;
            txtNamSinh.KeyPress += txtNamSinh_KeyPress;
            TG_txtNamChanDoanBasedow.KeyPress+=  TG_txtNamChanDoanBasedow_KeyPress;
            txtNgaySinh.Leave+= txtNgaySinh_Leave;
            txtThangSinh.Leave  += txtThangSinh_Leave;
            txtNamSinh.Leave += txtNamSinh_Leave;
            //TG_txtNamChanDoanBasedow.Leave += TG_txtNamChanDoanBasedow_Leave;
           // cmdSave.Click += cmdSave_Click_1;
            cmdInBenhAn.Click += cmdInBenhAn_Click_1;
            cmdDelete.Click += cmdDelete_Click;
            txtMaBenhAn.KeyPress += txtMaBenhAn_KeyPress;
            chkInNgay.CheckedChanged += chkInSauKhiLuu_CheckedChanged;
            txtMaBenhAn.KeyDown += txtMaBenhAn_KeyDown;



        }

        private void txtMaBN_KeyDown(object sender, KeyEventArgs e)
        {
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
                    txtDanToc.Text = Utility.sDbnull(objThongTinBenhNhan.DanToc);
                dtThoiDiemDkKham.Value = objThongTinBenhNhan.NgayTiepdon;
                QueryCommand cmd = KcbLuotkham.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandSql = "Select max(ma_luotkham) as ma_luotkham from Kcb_Luotkham where id_benhnhan =" +
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
        private void frm_BENHAN_NGOAITRU_TG_Load(object sender, EventArgs e)
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
                txtMaBenhAn.Text = THU_VIEN_CHUNG.SinhMaBenhAn(loaibenhan);
            }
           

        }
        // LAY DU LIEU BENH AN
        private void LayDuLieu()
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
                txtDoiTuong.Text = Utility.sDbnull(objBenhAnNgoaiTru.DoiTuong);
                txtSoBaoHiemYte.Text = Utility.sDbnull(objBenhAnNgoaiTru.SoBhyt);
                txtDiaChi.Text = Utility.sDbnull(objBenhAnNgoaiTru.DiaChi);
                chkYTe.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.YTe) == 1;
                chkTuDen.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.YTe) == 2;
                dtThoiDiemDkKham.Value = Convert.ToDateTime(objBenhAnNgoaiTru.NgayKham);
                label94.Text = "Bệnh nhân đã có bệnh án";
                label95.Text = Utility.sDbnull(objBenhAnNgoaiTru.NguoiTao);
                //{);
                //if (Utility.sDbnull(objBenhAnNgoaiTru.NguoiTao,"")!= "")
                //{

                //    LStaff objNguoiTao =
                //                    new Select("*").From(LStaff.Schema).Where(LStaff.Columns.Uid).IsEqualTo(
                //                        Utility.sDbnull(objBenhAnNgoaiTru.NguoiTao)).ExecuteSingle<LStaff>();
                //    label95.Text = Utility.sDbnull(objNguoiTao.StaffName);
                //}
                TG_chkldvvMetMoi.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgLlvvMm) == 1;
                TG_chkldvvGay.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgLdvvGsc) == 1;
                TG_chkldvvRCT.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgLdvvRct) == 1;
                TG_chkldvvCoTo.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgLdvvCt) == 1;
                TG_ldvvMatLoi.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgLdvvMl) == 1;
                TG_chkldvvKhac.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgLdvvKhac) == 1;
                TG_txtNamChanDoanBasedow.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgHbQtblNam);
                txtNoiChanDoanBasedow.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgHbQtblNoiChanDoan);
                chkhbDeu.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgHbQtblDieutriDeu) == 1;
                chkhbKhongDeu.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgHbQtblDieutriDeu) == 2;
                txtThuocDieuTri1.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgHbQtblThuocDIEUTRI1);
                txtThuocDieuTri2.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgHbQtblThuocDIEUTRI2);
                txtThuocDieuTri3.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgHbQtblThuocDIEUTRI3);
                txtThuocDieuTri4.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgHbQtblThuocDIEUTRI4);
                chkhbMetMoi.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgHbHtMm) == 1;
                chkhbGaySutCan.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgHbHtGsc) == 1;
                chkhbRunchantay.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgHbHtRct) == 1;
                chkhbVungTGto.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgHbHtVtgt) == 1;
                chkhbMatLoi.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgHbHtMl) == 1;
                chkNguoiNongBung.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgHbHtNnb) == 1;
                chkRoiLoanTieuHoa.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgHbHtRlth) == 1;
                chktsbHenPheQuanCo.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgHbTsbHpq) == 1;
                chktsbHenPheQuanKhong.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgHbTsbHpq) == 2;
                txthbKgDau.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgHbHtKgdau);
                txthbKgSau.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgHbHtKgcuoi);
                txtTinhChatPhan.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgHbHtTcp);
                txtSoLanDiNgoai.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgHbHtSldn);
                txthbKhac.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgHbHtKhac);
                chkBenhLyDaDayCo.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgHbTsbBldd) == 1;
                chkBenhLyDaDayKhong.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgHbTsbBldd) == 2;
                chkKinhNguyetDeu.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgHbTsbKn) == 1;
                chkKinhNguyetKhong.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgHbTsbKn) == 2;
                chktsgdBasedow.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgHbTsbBasedow) == 1;
                chktsgdBuouCo.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgHbTsbBc) == 1;
                txttsbKhac.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgHbTsbKhac);
                txttsgdKhac.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgHbTsbKHAC1);
                chkkbDaNongAmCo.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgKbKbDna) == 1;
                chkkbDaNongAmKhong.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgKbKbDna) == 2;
                chkkbCungMacMatVangCo.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgKbKbCmmv) == 1;
                chkkbCungMacMatVangKhong.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgKbKbCmmv) == 2;
                chkkbRunTayCo.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgKbKbTr) == 1;
                chkkbRunTayKhong.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgKbKbTr) == 2;
                chkkbNhipTimDeu.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgKbKbNtDeu) == 1;
                chkkbNhipTimKhong.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgKbKbNtDeu) == 2;
                txtkbNhipTim.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgKbKbNt);
                txtkbTiengTim.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgKbKbTt);
                chkla.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgKbKbLa) == 1;
                chklb.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgKbKbLb) == 1;
                chkll.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgKbKbLl) == 1;
                chklll.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgKbKbLll) == 1;
                chkMatDoMem.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgKbKbMdm) == 1;
                chkMatDoChac.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgKbKbMdc) == 1;
                chkkbBuouLanToa.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgKbKbBlt) == 1;
                chkbhBuouNhanThuyP.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgKbKbBntp) == 1;
                chkhbBuouNhanThuyT.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgKbKbBntt) == 1;
                chkhbBuouNhan2thuy.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgKbKbBN2T) == 1;
                chkhbThuyP.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgKbKbTtttp) == 1;
                chkThuyT.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgKbKbTtttt) == 1;
                chkhbLTThuyP.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgKbKbTtltp) == 1;
                chkbhLTThuyT.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.TgKbKbTtltt) == 1;
                txtkbMatPhai.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgKbKbMp);
                txtkbMatTrai.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgKbKbMt);
                txtkbHoHap.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgKbKbHh);
                txtkbBung.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgKbKbB);
                txtkbThanKinh.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgKbKbTk);
                txtMach.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgKbKbMach);
                txtNhietDo.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgKbKbNhietdo);
                txtHuyetApTu.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgKbKbHuyetap);
                txtNhipTho.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgKbKbNhiptho);
                txtCanNang.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgKbKbCannang);
                txtChieuCao.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgKbKbChieucao);
                txtBMI.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgKbKbBmi);
                txtkbTomTatKQCLSC.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgKbKbTtkqcks);
                TXTKBChanDoanBD.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgKbKbCdbd);
                TXTKBDaXuLy.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgKbKbDxl);
                txtkbChanDoanRaVien.Text = Utility.sDbnull(objBenhAnNgoaiTru.TgKbKbCdrv);

          

                dtDieuTriNgoaiTruTu.Value = Convert.ToDateTime(objBenhAnNgoaiTru.KcbDtriTungay);
                dtDieuTriNgoaiTruDen.Value = Convert.ToDateTime(objBenhAnNgoaiTru.KcbDtriDenngay);

               


            }
            else
            {
                MessageBox.Show("Có lỗi trong quá trình lấy thông tin bệnh án!");
            }
        }
        private void chkla_CheckedChanged(object sender, EventArgs e)
        {
            if (chkla.Checked == true)
            {
                chklb.Checked = false;
                chkll.Checked = false;
                chklll.Checked = false;
            }
        }
        private void chklb_CheckedChanged(object sender, EventArgs e)
        {
            if (chklb.Checked == true)
            {
                chkla.Checked = false;
                chkll.Checked = false;
                chklll.Checked = false;
            }
        }
        private void chkll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkll.Checked == true)
            {
                chkla.Checked = false;
                chklb.Checked = false;
                chklll.Checked = false;
            }
        }
        private void chklll_CheckedChanged(object sender, EventArgs e)
        {
            if (chklll.Checked == true)
            {
                chkla.Checked = false;
                chklb.Checked = false;
                chkll.Checked = false;
            }
        }


        private void chkMatDoMem_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMatDoMem.Checked == true)
                chkMatDoChac.Checked = false;
        }

        private void chkMatDoChac_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMatDoChac.Checked == true)
                chkMatDoMem.Checked = false;
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


        private void chkRoiLoanTieuHoa_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRoiLoanTieuHoa.Checked == true)
            {
                txtTinhChatPhan.Visible = true;
                txtSoLanDiNgoai.Visible = true;
                txtTinhChatPhan.Focus();
            }
            else
            {
                txtTinhChatPhan.Visible = false;
                txtSoLanDiNgoai.Visible = false;
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
       

        private void chkkbDaNongAmCo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkkbDaNongAmCo.Checked == true)
                chkkbDaNongAmKhong.Checked = false;
        }

        private void chkkbDaNongAmKhong_CheckedChanged(object sender, EventArgs e)
        {
            if (chkkbDaNongAmKhong.Checked == true)
                chkkbDaNongAmCo.Checked = false;
        }

        private void chkkbCungMacMatVangCo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkkbCungMacMatVangCo.Checked == true)
                chkkbCungMacMatVangKhong.Checked = false;
        }

        private void chkkbCungMacMatVangKhong_CheckedChanged(object sender, EventArgs e)
        {
            if (chkkbCungMacMatVangKhong.Checked == true)
                chkkbCungMacMatVangCo.Checked = false;
        }

        private void chkkbRunTayCo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkkbRunTayCo.Checked == true)
                chkkbRunTayKhong.Checked = false;
        }

        private void chkkbRunTayKhong_CheckedChanged(object sender, EventArgs e)
        {
            if (chkkbRunTayKhong.Checked == true)
                chkkbRunTayCo.Checked = false;
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
        private void chkBenhLyDaDayCo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBenhLyDaDayCo.Checked == true)
                chkBenhLyDaDayKhong.Checked = false;
        }
        private void chkBenhLyDaDayKhong_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBenhLyDaDayKhong.Checked == true)
                chkBenhLyDaDayCo.Checked = false;
        }
        private void chkKinhNguyetDeu_CheckedChanged(object sender, EventArgs e)
        {
            if (chkKinhNguyetDeu.Checked == true)
                chkKinhNguyetKhong.Checked = false;
        }
        private void chkKinhNguyetKhong_CheckedChanged(object sender, EventArgs e)
        {
            if (chkKinhNguyetKhong.Checked == true)
                chkKinhNguyetDeu.Checked = false;
        }
        private void txtMaLanKham_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
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

        private void TG_txtNamChanDoanBasedow_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            //    e.Handled = true;
        }


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
                    //    cmdInBenhAn.Enabled = true;
                    //    em_Action = action.Update;
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
                objBenhAnNgoaiTru.TgLlvvMm = Utility.Int16Dbnull(TG_chkldvvMetMoi.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgLdvvGsc = Utility.Int16Dbnull(TG_chkldvvGay.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgLdvvRct = Utility.Int16Dbnull(TG_chkldvvRCT.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgLdvvCt = Utility.Int16Dbnull(TG_chkldvvCoTo.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgLdvvMl = Utility.Int16Dbnull(TG_ldvvMatLoi.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgLdvvKhac = Utility.Int16Dbnull(TG_chkldvvKhac.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgHbQtblNam = Utility.sDbnull(TG_txtNamChanDoanBasedow.Text);
                objBenhAnNgoaiTru.TgHbQtblNoiChanDoan = Utility.sDbnull(txtNoiChanDoanBasedow.Text);
                if (chkhbDeu.Checked) objBenhAnNgoaiTru.TgHbQtblDieutriDeu = 1;
                else if (chkhbKhongDeu.Checked)
                {
                    objBenhAnNgoaiTru.TgHbQtblDieutriDeu = 2;
                }
                else
                {
                    objBenhAnNgoaiTru.TgHbQtblDieutriDeu = 3;
                }
                objBenhAnNgoaiTru.TgHbQtblThuocDIEUTRI1 = Utility.sDbnull(txtThuocDieuTri1.Text);
                objBenhAnNgoaiTru.TgHbQtblThuocDIEUTRI2 = Utility.sDbnull(txtThuocDieuTri2.Text);
                objBenhAnNgoaiTru.TgHbQtblThuocDIEUTRI3 = Utility.sDbnull(txtThuocDieuTri3.Text);
                objBenhAnNgoaiTru.TgHbQtblThuocDIEUTRI4 = Utility.sDbnull(txtThuocDieuTri4.Text);
                objBenhAnNgoaiTru.TgHbQtblThuocDIEUTRI5 = Utility.sDbnull(txtThuocDieuTri5.Text);
                objBenhAnNgoaiTru.TgHbHtMm = Utility.Int16Dbnull(chkhbMetMoi.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgHbHtGsc = Utility.Int16Dbnull(chkhbGaySutCan.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgHbHtKgdau = Utility.sDbnull(txthbKgDau.Text);
                objBenhAnNgoaiTru.TgHbHtKgcuoi = Utility.sDbnull(txthbKgSau.Text);
                objBenhAnNgoaiTru.TgHbHtRct = Utility.Int16Dbnull(chkhbRunchantay.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgHbHtVtgt = Utility.Int16Dbnull(chkhbVungTGto.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgHbHtMl = Utility.Int16Dbnull(chkhbMatLoi.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgHbHtNnb = Utility.Int16Dbnull(chkNguoiNongBung.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgHbHtRlth = Utility.Int16Dbnull(chkRoiLoanTieuHoa.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgHbHtTcp = Utility.sDbnull(txtTinhChatPhan.Text);
                objBenhAnNgoaiTru.TgHbHtSldn = Utility.sDbnull(txtSoLanDiNgoai.Text);
                objBenhAnNgoaiTru.TgHbHtKhac = Utility.sDbnull(txthbKhac.Text);

                if (chktsbHenPheQuanCo.Checked) objBenhAnNgoaiTru.TgHbTsbHpq = 1;
                else if (chktsbHenPheQuanKhong.Checked)
                {
                    objBenhAnNgoaiTru.TgHbTsbHpq = 2;
                }
                else
                {
                    objBenhAnNgoaiTru.TgHbTsbHpq = 3;
                }

                if (chkBenhLyDaDayCo.Checked) objBenhAnNgoaiTru.TgHbTsbBldd = 1;
                else if (chkBenhLyDaDayKhong.Checked)
                {
                    objBenhAnNgoaiTru.TgHbTsbBldd = 2;
                }
                else
                {
                    objBenhAnNgoaiTru.TgHbTsbBldd = 3;
                }


                if (chkKinhNguyetDeu.Checked) objBenhAnNgoaiTru.TgHbTsbKn = 1;
                else if (chkKinhNguyetKhong.Checked)
                {
                    objBenhAnNgoaiTru.TgHbTsbKn = 2;
                }
                else
                {
                    objBenhAnNgoaiTru.TgHbTsbKn = 3;
                }

                objBenhAnNgoaiTru.TgHbTsbKhac = Utility.sDbnull(txttsbKhac.Text);
                objBenhAnNgoaiTru.TgHbTsbBasedow = Utility.Int16Dbnull(chktsgdBasedow.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgHbTsbBc = Utility.Int16Dbnull(chktsgdBuouCo.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgHbTsbKHAC1 = Utility.sDbnull(txttsgdKhac.Text);



                if (chkkbDaNongAmCo.Checked) objBenhAnNgoaiTru.TgKbKbDna = 1;
                else if (chkkbDaNongAmKhong.Checked)
                {
                    objBenhAnNgoaiTru.TgKbKbDna = 2;
                }
                else
                {
                    objBenhAnNgoaiTru.TgKbKbDna = 3;
                }

                if (chkkbCungMacMatVangCo.Checked) objBenhAnNgoaiTru.TgKbKbCmmv = 1;
                else if (chkkbCungMacMatVangKhong.Checked)
                {
                    objBenhAnNgoaiTru.TgKbKbCmmv = 2;
                }
                else
                {
                    objBenhAnNgoaiTru.TgKbKbCmmv = 3;
                }


                if (chkkbRunTayCo.Checked) objBenhAnNgoaiTru.TgKbKbTr = 1;
                else if (chkkbRunTayKhong.Checked)
                {
                    objBenhAnNgoaiTru.TgKbKbTr = 2;
                }
                else
                {
                    objBenhAnNgoaiTru.TgKbKbTr = 3;
                }


                objBenhAnNgoaiTru.TgKbKbNt = Utility.sDbnull(txtkbNhipTim.Text);
                if (chkkbNhipTimDeu.Checked) objBenhAnNgoaiTru.TgKbKbNtDeu = 1;
                else if (chkkbNhipTimKhong.Checked)
                {
                    objBenhAnNgoaiTru.TgKbKbNtDeu = 2;
                }
                else
                {
                    objBenhAnNgoaiTru.TgKbKbNtDeu = 3;
                }
                objBenhAnNgoaiTru.TgKbKbTt = Utility.sDbnull(txtkbTiengTim.Text);
                objBenhAnNgoaiTru.TgKbKbLa = Utility.Int16Dbnull(chkla.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgKbKbLb = Utility.Int16Dbnull(chklb.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgKbKbLl = Utility.Int16Dbnull(chkll.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgKbKbLll = Utility.Int16Dbnull(chklll.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgKbKbMdm = Utility.Int16Dbnull(chkMatDoMem.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgKbKbMdc = Utility.Int16Dbnull(chkMatDoChac.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgKbKbBlt = Utility.Int16Dbnull(chkkbBuouLanToa.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgKbKbBntp = Utility.Int16Dbnull(chkbhBuouNhanThuyP.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgKbKbBntt = Utility.Int16Dbnull(chkhbBuouNhanThuyT.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgKbKbBN2T = Utility.Int16Dbnull(chkhbBuouNhan2thuy.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgKbKbTtttp = Utility.Int16Dbnull(chkhbThuyP.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgKbKbTtttt = Utility.Int16Dbnull(chkThuyT.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgKbKbTtltp = Utility.Int16Dbnull(chkhbLTThuyP.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgKbKbTtltt = Utility.Int16Dbnull(chkbhLTThuyT.Checked ? 1 : 0);
                objBenhAnNgoaiTru.TgKbKbMp = Utility.sDbnull(txtkbMatPhai.Text);
                objBenhAnNgoaiTru.TgKbKbMt = Utility.sDbnull(txtkbMatTrai.Text);
                objBenhAnNgoaiTru.TgKbKbHh = Utility.sDbnull(txtkbHoHap.Text);
                objBenhAnNgoaiTru.TgKbKbB = Utility.sDbnull(txtkbBung.Text);
                objBenhAnNgoaiTru.TgKbKbTk = Utility.sDbnull(txtkbThanKinh.Text);

                objBenhAnNgoaiTru.TgKbKbMach = Utility.sDbnull(txtMach.Text);
                objBenhAnNgoaiTru.TgKbKbNhietdo = Utility.sDbnull(txtNhietDo.Text);
                objBenhAnNgoaiTru.TgKbKbHuyetap = Utility.sDbnull(txtHuyetApTu.Text);
                //objBenhAnNgoaiTru.KcbHuyetap2 = Utility.sDbnull(txtHuyetApDen.Text);
                objBenhAnNgoaiTru.TgKbKbNhiptho = Utility.sDbnull(txtNhipTho.Text);
                objBenhAnNgoaiTru.TgKbKbCannang = Utility.sDbnull(txtCanNang.Text);
                objBenhAnNgoaiTru.TgKbKbChieucao = Utility.sDbnull(txtChieuCao.Text);
                objBenhAnNgoaiTru.TgKbKbBmi = Utility.sDbnull(txtBMI.Text);
                objBenhAnNgoaiTru.TrangThai = Utility.Int16Dbnull(0);


                objBenhAnNgoaiTru.TgKbKbTtkqcks = Utility.sDbnull(txtkbTomTatKQCLSC.Text);
                objBenhAnNgoaiTru.TgKbKbCdbd = Utility.sDbnull(TXTKBChanDoanBD.Text);
                objBenhAnNgoaiTru.TgKbKbDxl = Utility.sDbnull(TXTKBDaXuLy.Text);
                objBenhAnNgoaiTru.TgKbKbCdrv = Utility.sDbnull(txtkbChanDoanRaVien.Text);

                objBenhAnNgoaiTru.KcbDtriTungay = Convert.ToDateTime(dtDieuTriNgoaiTruTu.Value);
                objBenhAnNgoaiTru.KcbDtriDenngay = Convert.ToDateTime(dtDieuTriNgoaiTruDen.Value);


                objBenhAnNgoaiTru.DoiTuong = Utility.sDbnull(txtDoiTuong.Text);
                objBenhAnNgoaiTru.SoBhyt = Utility.sDbnull(txtSoBaoHiemYte.Text);
                objBenhAnNgoaiTru.DiaChi = Utility.sDbnull(txtDiaChi.Text);
                objBenhAnNgoaiTru.NamSinh = Utility.Int16Dbnull(txtNamSinh.Text);
                objBenhAnNgoaiTru.InPhieuLog = Utility.Int16Dbnull(0);
                objBenhAnNgoaiTru.LoaiBenhAn = Utility.Int16Dbnull(2);
                

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

            DataTable dsTable = SPs.KcbThamkhamLaythongtinBenhanNgoaitru(txtMaBN.Text).GetDataSet().Tables[0];
            CrystalDecisions.CrystalReports.Engine.ReportDocument crpt;
             string tieude="", reportname = "";
            crpt = Utility.GetReport("thamkham_BENH_AN_NGOAITRU_tuyengiap",ref tieude,ref reportname);
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
                if (objMaBn != null)
                {
                    txtMaBN.Text = Utility.sDbnull(objMaBn.IdBenhnhan, "");
                    MaBenhNhanText = txtMaBN.Text;
                    FindPatientID(txtMaBN.Text.Trim());
                    SuaBenhAn = false;

                }
                else
                {
                    MessageBox.Show("Cần kết thúc Bệnh nhân trước khi tạo Bệnh án ");
                    txtMaLanKham.Focus();
                }

            }

        }




        //private void TG_txtNamChanDoanBasedow_Leave(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(TG_txtNamChanDoanBasedow.Text))
        //    {
        //        if ((TG_txtNamChanDoanBasedow.Text.Length > 4) || (TG_txtNamChanDoanBasedow.Text.Length < 4))
        //        {
        //            Utility.ShowMsg("Năm sinh phải là 4 số", "Thông báo", MessageBoxIcon.Warning);
        //            TG_txtNamChanDoanBasedow.Focus();
        //            TG_txtNamChanDoanBasedow.SelectAll();
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

        private void frm_BENHAN_NGOAITRU_TG_KeyDown_1(object sender, KeyEventArgs e)
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
        private void ClearControl()
        {

            txtNgaySinh.Clear();
            txtThangSinh.Clear();
            txtDienThoai.Clear();
            txtDanToc.Clear();
            txtMaLanKham.Clear();
            txtThongTinLienHe.Clear();
            txtChanDoanBanDau.Clear();
            txtNoiLamViec.Clear();

            foreach (Control control in KHAMBENH.Controls)
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
                    txtControl = ((RichTextBox)control);
                    txtControl.Clear();
                }




            }

            foreach (Control control in LDVV.Controls)
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
                    txtControl = ((RichTextBox)control);
                    txtControl.Clear();
                }




            }
            foreach (Control control in HOIBENH.Controls)
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
                    txtControl = ((RichTextBox)control);
                    txtControl.Clear();
                }



            }
        }

        private void txtDienThoai_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkhbMetMoi_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmdInBenhAn_Click(object sender, EventArgs e)
        {

        }



















    }
}
  
  