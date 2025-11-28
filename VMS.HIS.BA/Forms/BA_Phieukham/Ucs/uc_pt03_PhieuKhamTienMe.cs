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
using System;
using System.Transactions;
using VMS.HIS.Bus;
using VMS.HIS.Bus.Emr;

namespace VMS.HIS.UI.EMR.Ucs
{
    public partial class uc_pt03_PhieuKhamTienMe : UserControl
    {
        public delegate void OnMsg(string msg, bool IsSucess = false);
        public event OnMsg _OnMsg;
        public delegate void OnStatus(bool isNew);
        public event OnStatus _OnStatus;
        public EmrPt03PhieukhamTienme _phieu;
        KcbLuotkham objLuotkham;
        public int id_bacsikham = -1;
        DmucNhanvien objBacsiKham = null;
        DmucNhanvien objNguoiDaidien = null;
        public bool Force2Saved = false;
        bool isInit = false;
        public uc_pt03_PhieuKhamTienMe()
        {
            InitializeComponent();
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
        public void Init()
        {
            dtp_ngay_kham.Value = globalVariables.SysDate;
            txt_nhommau.Init();
            DataBinding.BindDataCombobox(cbo_bacsy_kham, globalVariables.gv_dtDmucNhanvien, DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "----Chọn----", true);
            isInit = true;
        }
        public void Init(KcbLuotkham objLuotkham, EmrPt03PhieukhamTienme _phieu)
        {
           
            this.objLuotkham = objLuotkham;
            this._phieu = _phieu;
            if (!isInit)
                Init();
            DisplayData();
        }


        public void Init(KcbLuotkham objLuotkham)
        {
            if (!isInit)
                Init();
            this.objLuotkham = objLuotkham;
            _phieu = new Select().From(EmrPt03PhieukhamTienme.Schema)
                        .Where(EmrPt03PhieukhamTienme.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(EmrPt03PhieukhamTienme.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<EmrPt03PhieukhamTienme>();
            DisplayData();
        }
        public void HandleKeyEnter()
        {
            Control activeCtrl = Utility.getActiveControl(this);
            if (activeCtrl == null) return;
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
            else
                SendKeys.Send("{TAB}");
        }
        
        public void DisplayData()
        {
            try
            {

                if (_phieu == null)
                    _phieu = new Select().From(EmrPt03PhieukhamTienme.Schema)
                        .Where(EmrPt03PhieukhamTienme.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(EmrPt03PhieukhamTienme.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<EmrPt03PhieukhamTienme>();

                txtId.Text = "";
                if (_phieu != null)
                {
                    txtId.Text = _phieu.Id.ToString();
                    txtSoHoso.Text = _phieu.SoPhieu;
                    dtp_ngay_kham.Value = _phieu.NgayKham;
                    opt_capcuu.Checked = Utility.Bool2Bool(_phieu.Capcuu);
                    opt_bancap.Checked = Utility.Bool2Bool(_phieu.Bancap);
                    opt_chuongtrinh_phien.Checked = Utility.Bool2Bool(_phieu.ChuongtrinhPhien);
                   
                    cbo_bacsy_kham.SelectedValue = _phieu.IdBacsyKham;
                    chk_mang_thai.Checked = Utility.Bool2Bool(_phieu.MangThai);
                    txt_tuoi_thai_mota.Text = _phieu.TuoiThaiMota;

                    chk_thoiquen_hut_thuoc.Checked = Utility.Bool2Bool(_phieu.ThoiquenHutThuoc);
                    chk_thoiquen_uong_ruou.Checked = Utility.Bool2Bool(_phieu.ThoiquenUongRuou);
                    txt_thoiquen_khac_mota.Text = _phieu.ThoiquenKhacMota;

                    txtMach.Text=Utility.sDbnull(_phieu.Mach);
                    txtNhietDo.Text = Utility.sDbnull(_phieu.NhietDo);
                    txtha.Text = Utility.sDbnull(_phieu.HuyetAp);
                    txtNhipTho.Text = Utility.sDbnull(_phieu.NhipTho);
                    txtCanNang.Text = Utility.sDbnull(_phieu.CanNang);
                    txtChieuCao.Text = Utility.sDbnull(_phieu.ChieuCao);
                    txtMach.Text = Utility.sDbnull(_phieu.Mach);
                    txt_nhommau.SetCode(Utility.sDbnull(_phieu.NhomMau));
                    tinhBMI();


                    txt_chan_doan.Text = _phieu.ChanDoan;
                    txt_huong_xu_tri.Text = _phieu.HuongXuTri;
                   
                   
                    opt_tiensu_noikhoa_khong.Checked = !Utility.Bool2Bool(_phieu.TiensuNoikhoa);
                    opt_tiensu_noikhoa_co.Checked = Utility.Bool2Bool(_phieu.TiensuNoikhoa);

                    opt_benh_tim_mach_khong.Checked = !Utility.Bool2Bool(_phieu.BenhTimMach);
                    opt_opt_benh_tim_mach_co.Checked = Utility.Bool2Bool(_phieu.BenhTimMach);

                    opt_benh_hohap_khong.Checked = !Utility.Bool2Bool(_phieu.BenhHohap);
                    opt_opt_benh_hohap_co.Checked = Utility.Bool2Bool(_phieu.BenhHohap);

                    chk_tang_huyet_ap.Checked = Utility.Bool2Bool(_phieu.TangHuyetAp);
                    chk_loan_nhip_tim.Checked = Utility.Bool2Bool(_phieu.LoanNhipTim);
                    chk_benh_van_tim.Checked = Utility.Bool2Bool(_phieu.BenhVanTim);
                    chk_benh_mach_vanh.Checked = Utility.Bool2Bool(_phieu.BenhMachVanh);
                    chk_suy_tim_man.Checked = Utility.Bool2Bool(_phieu.SuyTimMan);


                    chk_roi_loan_chuyen_mo.Checked = Utility.Bool2Bool(_phieu.RoiLoanChuyenMo);
                    txt_roi_loan_chuyen_mo_khac.Text = _phieu.RoiLoanChuyenMoKhac;
                    chk_suy_than_man.Checked = Utility.Bool2Bool(_phieu.SuyThanMan);
                    chk_copd.Checked = Utility.Bool2Bool(_phieu.Copd);
                    chk_roi_loan_nhan_thuc.Checked = Utility.Bool2Bool(_phieu.RoiLoanNhanThuc);
                    chk_suyen_hen_phe_quan.Checked = Utility.Bool2Bool(_phieu.SuyenHenPheQuan);
                    txt_suyen_hen_phe_quan_khac.Text = _phieu.SuyenHenPheQuanKhac;

                    opt_dai_thao_duong_khong.Checked = !Utility.Bool2Bool(_phieu.DaiThaoDuong);
                    opt_dai_thao_duong_co.Checked = Utility.Bool2Bool(_phieu.DaiThaoDuong);
                    chk_dtd_phu_thuoc_insulin.Checked = !Utility.Bool2Bool(_phieu.DtdPhuThuocInsulin);
                    chk_dtd_khong_phu_thuoc_insulin.Checked = Utility.Bool2Bool(_phieu.DtdKhongPhuThuocInsulin);

                    opt_roi_loan_dong_mau_khong.Checked = !Utility.Bool2Bool(_phieu.RoiLoanDongMau);
                    opt_opt_roi_loan_dong_mau_co.Checked = Utility.Bool2Bool(_phieu.RoiLoanDongMau);
                    chk_de_tu_mau.Checked = !Utility.Bool2Bool(_phieu.DeTuMau);
                    chk_xuat_huyet_ngoai_khoa.Checked = Utility.Bool2Bool(_phieu.XuatHuyetNgoaiKhoa);

                    txt_tien_su_noi_khoa_khac.Text = _phieu.TienSuNoiKhoaKhac;
                    txt_tien_su_ngoai_khoa.Text = _phieu.TienSuNgoaiKhoa;
                    txt_tien_su_gay_me.Text = _phieu.TienSuGayMe;
                    txt_thuoc_dang_dieu_tri.Text = _phieu.ThuocDangDieuTri;
                    txt_kham_tim_mach.Text = _phieu.KhamTimMach;
                    txt_kham_ho_hap.Text = _phieu.KhamHoHap;

                    

                    opt_cot_song_batthuong.Checked = Utility.Bool2Bool(_phieu.CotSong);
                    opt_cot_song_binhthuong.Checked = !Utility.Bool2Bool(_phieu.CotSong);
                    txt_cot_song_ghi_ro.Text = _phieu.CotSongGhiRo;
                    txt_cac_dau_hieu_co_lien_quan.Text = _phieu.CacDauHieuCoLienQuan;

                    opt_duong_truyen_tinh_mach_kho_batthuong.Checked = Utility.Bool2Bool(_phieu.DuongTruyenTinhMachKho);
                    opt_duong_truyen_tinh_mach_kho_binhthuong.Checked = !Utility.Bool2Bool(_phieu.DuongTruyenTinhMachKho);
                    txt_duong_truyen_tinh_mach_kho_ghi_ro.Text = _phieu.DuongTruyenTinhMachKhoGhiRo;
                    txt_cu_dong_co.Text = _phieu.CuDongCo;

                    cbo_rang_gia.SelectedIndex =Utility.Int32Dbnull( _phieu.RangGia,-1);
                    cbo_mallampati.SelectedIndex = Utility.Int32Dbnull(_phieu.Mallampati,-1);
                    cbo_phando_ASA.SelectedIndex = Utility.Int32Dbnull(_phieu.PhandoAsa, -1);

                    nmr_ha_mieng_cm.Value = Utility.DecimaltoDbnull(_phieu.HaMiengCm);
                    chk_ha_mieng_tren_3cm.Checked = Utility.Bool2Bool(_phieu.HaMiengTren3cm);

                    nmr_khoang_cach_cam_sun_giap_cm.Value = Utility.DecimaltoDbnull(_phieu.KhoangCachCamSunGiapCm);
                    chk_khoang_cach_cam_sun_giap_tren_6_5_cm.Checked = Utility.Bool2Bool(_phieu.KhoangCachCamSunGiapTren65Cm);

                    txt_xet_nghiem.Text = _phieu.XetNghiem;
                    txt_du_kien_thuoc.Text = _phieu.DuKienThuoc;
                    txt_du_kien_giam_dau_sau_pt.Text = _phieu.DuKienGiamDauSauPt;
                    txt_de_nghi_khac.Text = _phieu.DeNghiKhac;


                }
                else
                {
                    ClearControl();

                }
                txtSoHoso.Text = _phieu == null || string.IsNullOrEmpty(Utility.sDbnull(_phieu.SoPhieu, "")) ? THU_VIEN_CHUNG.TT25LaySohoso(12) : Utility.sDbnull(_phieu.SoPhieu, "");
                if (_OnStatus != null) _OnStatus(_phieu == null || _phieu.Id <= 0);
            }
            catch (System.Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        public void ClearControl()
        {
            try
            {
                foreach (Control ctr in this.Controls)
                {
                    if (ctr.GetType().Equals(autoTxt.GetType()))
                        ((AutoCompleteTextbox_Danhmucchung)ctr).SetDefaultItem();
                    else if (ctr is EditBox)
                    {
                        ((EditBox)(ctr)).Clear();
                    }
                    else if (ctr is CheckBox)
                    {
                        ((CheckBox)(ctr)).Checked = false;
                    }
                    else if (ctr is RadioButton)
                    {
                        ((RadioButton)(ctr)).Checked = false;
                    }
                    else if (ctr is DateTimePicker)
                    {
                        ((DateTimePicker)(ctr)).Value = globalVariables.SysDate;
                    }
                    else if (ctr is Janus.Windows.CalendarCombo.Calendar)
                    {
                        Janus.Windows.CalendarCombo.CalendarCombo dtp = ctr as Janus.Windows.CalendarCombo.CalendarCombo;
                        if (dtp.IsNullDate)
                            dtp.ResetText();
                        else
                            dtp.Value = globalVariables.SysDate;
                    }
                }
            }
            catch (Exception)
            {
            }

        }
        string Msg = "";
        bool isValidData()
        {
            Msg = "";
            if (!opt_capcuu.Checked && !opt_bancap.Checked && !opt_chuongtrinh_phien.Checked)
            {
                Msg = "Bạn phải chọn Phân loại Biên bản";
                if (_OnMsg != null) _OnMsg(Msg);
                return false;
            }
            if (Utility.sDbnull(txtSoHoso.Text)=="")
            {
                Msg = "Bạn phải nhập mã phiếu";
                if (_OnMsg != null) _OnMsg(Msg);
                txtSoHoso.Focus();
                return false;
            }
            DataTable dtData = new Select().From(EmrPt03PhieukhamTienme.Schema)
              .Where(EmrPt03PhieukhamTienme.Columns.SoPhieu).IsEqualTo(Utility.DoTrim(txtSoHoso.Text))
              .And(EmrPt03PhieukhamTienme.Columns.Id).IsNotEqualTo(Utility.Int64Dbnull(txtId.Text, -1))
              .ExecuteDataSet().Tables[0];
            if (dtData.Rows.Count > 0)
            {
                Msg = "Mã phiếu đã được sử dụng. Vui lòng nhập mã phiếu khác";
                txtSoHoso.Focus();
                return false;
            }
            if (dtp_ngay_kham.Text == "")
            {
                Msg = "Phải nhập ngày khám tiền mê";
                if (_OnMsg != null) _OnMsg(Msg, false);
                dtp_ngay_kham.Focus();
                return false;
            }
            if (Utility.Int32Dbnull( cbo_bacsy_kham.SelectedValue)<=0)
            {
                Msg = "Bạn phải chọn Bác sĩ khám tiền mê từ danh mục bác sĩ";
                if (_OnMsg != null) _OnMsg(Msg);
                cbo_bacsy_kham.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(cbo_rang_gia.SelectedIndex) < 0)
            {
                Msg = "Bạn phải chọn thông tin Răng giả";
                if (_OnMsg != null) _OnMsg(Msg);
                cbo_rang_gia.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(cbo_mallampati.SelectedIndex) < 0)
            {
                Msg = "Bạn phải chọn thông tin Mallampati";
                if (_OnMsg != null) _OnMsg(Msg);
                cbo_mallampati.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(cbo_phando_ASA.SelectedIndex) < 0)
            {
                Msg = "Bạn phải chọn thông tin Phân loại Asa";
                if (_OnMsg != null) _OnMsg(Msg);
                cbo_phando_ASA.Focus();
                return false;
            }
            return true;
        }
        EmrDocuments emrdoc = new EmrDocuments();
        public bool Save()
        {
            try
            {
                bool isNew = true;
                if (!isValidData()) return false;
                DateTime? dtp=null;
                Msg = "";
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        _phieu = new Select().From(EmrPt03PhieukhamTienme.Schema)
                   .Where(EmrPt03PhieukhamTienme.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(EmrPt03PhieukhamTienme.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<EmrPt03PhieukhamTienme>();
                       
                        if (_phieu == null || _phieu.Id <= 0)
                        {
                            isNew = true;
                            _phieu = new EmrPt03PhieukhamTienme();
                            _phieu.IsNew = true;
                            _phieu.NgayTao = DateTime.Now;
                            _phieu.NguoiTao = globalVariables.UserName;
                        }
                        else
                        {
                            isNew = false;
                            _phieu.IsNew = false;
                            _phieu.MarkOld();
                            _phieu.NgaySua = DateTime.Now;
                            _phieu.NguoiSua = globalVariables.UserName;
                        }
                        _phieu.IdBenhnhan = objLuotkham.IdBenhnhan;
                        _phieu.MaLuotkham = objLuotkham.MaLuotkham;
                        _phieu.SoPhieu = Utility.sDbnull(txtSoHoso.Text);
                        _phieu.NgayKham = dtp_ngay_kham.Value;

                        _phieu.Capcuu = opt_capcuu.Checked;
                        _phieu.Bancap = opt_bancap.Checked;
                        _phieu.ChuongtrinhPhien = opt_chuongtrinh_phien.Checked;

                        _phieu.IdBacsyKham = Utility.Int16Dbnull(cbo_bacsy_kham.SelectedValue);
                        _phieu.MangThai = chk_mang_thai.Checked;
                        _phieu.TuoiThaiMota = chk_mang_thai.Checked?Utility.sDbnull(txt_tuoi_thai_mota.Text):"";

                        _phieu.Mach = txtMach.Text;
                        _phieu.NhietDo = txtNhietDo.Text;
                        _phieu.HuyetAp = txtha.Text;
                        _phieu.NhipTho = txtNhipTho.Text;
                        _phieu.CanNang = txtCanNang.Text;
                        _phieu.ChieuCao = txtChieuCao.Text;
                        tinhBMI();

                        _phieu.ThoiquenHutThuoc = chk_thoiquen_hut_thuoc.Checked;
                        _phieu.ThoiquenUongRuou = chk_thoiquen_uong_ruou.Checked;
                        _phieu.ThoiquenKhac = chk_thoiquen_khac.Checked;
                        _phieu.ThoiquenKhacMota = chk_thoiquen_khac.Checked?Utility.sDbnull(txt_thoiquen_khac_mota.Text):"";

                       

                        _phieu.ChanDoan = Utility.sDbnull(txt_chan_doan.Text);
                        _phieu.HuongXuTri = Utility.sDbnull(txt_huong_xu_tri.Text);

                        _phieu.TiensuNoikhoa = opt_tiensu_noikhoa_co.Checked;
                        _phieu.BenhTimMach = opt_opt_benh_tim_mach_co.Checked;
                        _phieu.BenhHohap = opt_opt_benh_hohap_co.Checked;

                        _phieu.TangHuyetAp = chk_tang_huyet_ap.Checked;
                        _phieu.LoanNhipTim = chk_loan_nhip_tim.Checked;
                        _phieu.BenhVanTim = chk_benh_van_tim.Checked;
                        _phieu.BenhMachVanh = chk_benh_mach_vanh.Checked;
                        _phieu.SuyTimMan = chk_suy_tim_man.Checked;

                        _phieu.RoiLoanChuyenMo = chk_roi_loan_chuyen_mo.Checked;
                        _phieu.RoiLoanChuyenMoKhac = chk_roi_loan_chuyen_mo.Checked? Utility.sDbnull(txt_roi_loan_chuyen_mo_khac.Text):"";
                        _phieu.SuyThanMan = chk_suy_than_man.Checked;
                        _phieu.Copd = chk_copd.Checked;
                        _phieu.RoiLoanNhanThuc = chk_roi_loan_nhan_thuc.Checked;
                        _phieu.SuyenHenPheQuan = chk_suyen_hen_phe_quan.Checked;
                        _phieu.SuyenHenPheQuanKhac = chk_suyen_hen_phe_quan.Checked? Utility.sDbnull(txt_suyen_hen_phe_quan_khac.Text):"";

                        _phieu.DaiThaoDuong = opt_dai_thao_duong_co.Checked;
                        _phieu.DtdPhuThuocInsulin = chk_dtd_phu_thuoc_insulin.Checked;
                        _phieu.DtdKhongPhuThuocInsulin = chk_dtd_khong_phu_thuoc_insulin.Checked;

                        _phieu.RoiLoanDongMau = opt_opt_roi_loan_dong_mau_co.Checked;
                        _phieu.DeTuMau = chk_de_tu_mau.Checked;
                        _phieu.XuatHuyetNgoaiKhoa = chk_xuat_huyet_ngoai_khoa.Checked;

                        _phieu.TienSuNoiKhoaKhac = Utility.sDbnull(txt_tien_su_noi_khoa_khac.Text);
                        _phieu.TienSuNgoaiKhoa = Utility.sDbnull(txt_tien_su_ngoai_khoa.Text);
                        _phieu.TienSuGayMe = Utility.sDbnull(txt_tien_su_gay_me.Text);
                        _phieu.ThuocDangDieuTri = Utility.sDbnull(txt_thuoc_dang_dieu_tri.Text);
                        _phieu.KhamTimMach = Utility.sDbnull(txt_kham_tim_mach.Text);
                        _phieu.KhamHoHap = Utility.sDbnull(txt_kham_ho_hap.Text);

                        _phieu.CotSong = opt_cot_song_batthuong.Checked;
                        _phieu.CotSongGhiRo = opt_cot_song_batthuong.Checked? Utility.sDbnull(txt_cot_song_ghi_ro.Text):"";
                        _phieu.CacDauHieuCoLienQuan = Utility.sDbnull(txt_cac_dau_hieu_co_lien_quan.Text);

                        _phieu.DuongTruyenTinhMachKho = opt_duong_truyen_tinh_mach_kho_batthuong.Checked;
                        _phieu.DuongTruyenTinhMachKhoGhiRo = opt_duong_truyen_tinh_mach_kho_batthuong.Checked? Utility.sDbnull(txt_duong_truyen_tinh_mach_kho_ghi_ro.Text):"";
                        _phieu.CuDongCo = Utility.sDbnull(txt_cu_dong_co.Text);

                        _phieu.RangGia = Utility.ByteDbnull(cbo_rang_gia.SelectedIndex);
                        _phieu.Mallampati = Utility.ByteDbnull(cbo_mallampati.SelectedIndex);
                        _phieu.PhandoAsa = Utility.ByteDbnull(cbo_phando_ASA.SelectedIndex);

                        _phieu.HaMiengCm = Utility.Int16Dbnull(nmr_ha_mieng_cm.Value);
                        _phieu.HaMiengTren3cm = chk_ha_mieng_tren_3cm.Checked;

                        _phieu.KhoangCachCamSunGiapCm = Utility.Int16Dbnull(nmr_khoang_cach_cam_sun_giap_cm.Value);
                        _phieu.KhoangCachCamSunGiapTren65Cm = chk_khoang_cach_cam_sun_giap_tren_6_5_cm.Checked;

                        _phieu.XetNghiem = Utility.sDbnull(txt_xet_nghiem.Text);
                        _phieu.DuKienThuoc = Utility.sDbnull(txt_du_kien_thuoc.Text);
                        _phieu.DuKienGiamDauSauPt = Utility.sDbnull(txt_du_kien_giam_dau_sau_pt.Text);
                        _phieu.DeNghiKhac = Utility.sDbnull(txt_de_nghi_khac.Text);



                        _phieu.Save();
                        if (objBacsiKham == null)
                            objBacsiKham = DmucNhanvien.FetchByID(Utility.Int32Dbnull(cbo_bacsy_kham.SelectedValue));
                        emrdoc.Force2Saved = Force2Saved;
                        emrdoc.InitDocument(Utility.Int64Dbnull(_phieu.IdBenhnhan), _phieu.MaLuotkham, Utility.Int64Dbnull(_phieu.Id), _phieu.NgayKham, Loaiphieu_HIS.PHIEUKHAM_TIENME, "PHIEUKHAMTIENME", _phieu.NguoiTao,Utility.Int16Dbnull( objBacsiKham.IdKhoa), Utility.Int16Dbnull(objBacsiKham.IdPhong), Utility.Byte2Bool(0),"");
                        emrdoc.Save();

                    }
                    scope.Complete();
                }
                txtId.Text = _phieu.Id.ToString();
                if (_OnStatus != null) _OnStatus(isNew);
                Msg = "Lưu thông tin thành công";
                if (_OnMsg != null) _OnMsg(Msg,true);
                return true;
            }
            catch (System.Exception ex)
            {
                if (_OnMsg != null) _OnMsg(ex.Message);
                Utility.CatchException(ex);
                return false;
            }
        }

        private void cmdGhi_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void cmdInphieu_Click(object sender, EventArgs e)
        {
            Print();
        }
        public  void Print()
        {
            try
            {
                _phieu = new Select().From(EmrPt03PhieukhamTienme.Schema)
                       .Where(EmrPt03PhieukhamTienme.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                       .And(EmrPt03PhieukhamTienme.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                       .ExecuteSingle<EmrPt03PhieukhamTienme>();
                if (_phieu.Id <= 0)
                {
                    Utility.ShowMsg("Bạn cần lưu thông tin Phiếu khám tiền mê trước khi thực hiện in phiếu");
                    return;
                }
                DataTable dtData = SPs.EmrPt03PhieukhamTienmeLaythongtinIn(_phieu.Id).GetDataSet().Tables[0];
                dtData.TableName = "PHIEU_KHAM_TIEN_ME";
                dtData.Rows[0]["sngay_kham"] = _phieu != null ? Utility.FormatDateTime_gio_ngay_thang_nam(_phieu.NgayKham, "") : "Ngày..........tháng........năm..........";
                WordPrinter.InPhieu(dtData, "PHIEU_KHAM_TIEN_ME.doc", "",false, @"\MergeFields\PHIEU_KHAM_TIEN_ME_CHECKED_FIELDS.txt");


            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdTuSinh_Click(object sender, EventArgs e)
        {
            txtSoHoso.Text = THU_VIEN_CHUNG.TT25LaySohoso(12);
        }

        private void cbo_bacsy_phauthuat_SelectedIndexChanged(object sender, EventArgs e)
        {
            objBacsiKham = DmucNhanvien.FetchByID(Utility.Int32Dbnull(cbo_bacsy_kham.SelectedValue));
        }

        private void chk_mang_thai_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_tuoi_thai_mota, sender as CheckBox);
        }

        private void chk_thoiquen_khac_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_thoiquen_khac_mota, sender as CheckBox);
        }

        private void opt_cot_song_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_cot_song_ghi_ro, sender as RadioButton);
        }

        private void opt_duong_truyen_tinh_mach_kho_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_duong_truyen_tinh_mach_kho_ghi_ro, sender as RadioButton);
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
            txt_nhommau.SetCode(nhommau);
        }
    }
}
