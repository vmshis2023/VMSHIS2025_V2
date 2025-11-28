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
    public partial class uc_pt01_BienBanHoiChanThongQuaMo : UserControl
    {
        public delegate void OnMsg(string msg, bool IsSucess = false);
        public event OnMsg _OnMsg;
        public delegate void OnStatus(bool isNew);
        public event OnStatus _OnStatus;
        public EmrPt01Bienbanhoichanthongquamo _phieu;
        KcbLuotkham objLuotkham;
        public int id_bacsikham = -1;
        DmucNhanvien objBacsiPttt = null;
        DmucNhanvien objNguoiDaidien = null;
        public bool Force2Saved = false;
        bool isInit = false;
        public uc_pt01_BienBanHoiChanThongQuaMo()
        {
            InitializeComponent();


        }

        public void Init()
        {
            dtp_NgayBienBan.Value = globalVariables.SysDate;
            DataBinding.BindDataCombobox(cbo_bacsy_phauthuat, globalVariables.gv_dtDmucNhanvien, DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "----Chọn----", true);
            DataBinding.BindDataCombobox(cbo_bacsy_gayme, globalVariables.gv_dtDmucNhanvien, DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "----Chọn----", true);
            DataBinding.BindDataCombobox(cbo_lanhdao_duyetmo, globalVariables.gv_dtDmucNhanvien, DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "----Chọn----", true);
            DataBinding.BindDataCombobox(cbo_lanhdao_khoa_cls, globalVariables.gv_dtDmucNhanvien, DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "----Chọn----", true);
            DataTable dtKhoaPhong = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL", 0);
            txt_khoa.Init(dtKhoaPhong, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            DataTable dtDmucChung = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { txtNhommau.LOAI_DANHMUC, "CACHTHUC_PTTT", "PHUONGPHAPVOCAM" }, true);
            txtNhommau.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtDmucChung, txtNhommau.LOAI_DANHMUC));
            DataBinding.BindDataCombobox(cbo_phuongphapphauthuat, THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtDmucChung, "CACHTHUC_PTTT"), DmucChung.Columns.Ma, DmucChung.Columns.Ten, "----Chọn----", true);
            DataBinding.BindDataCombobox(cbo_phuongphapvocam_dukien, THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtDmucChung, "PHUONGPHAPVOCAM"), DmucChung.Columns.Ma, DmucChung.Columns.Ten, "----Chọn----", true);
            isInit = true;
        }
        public void Init(KcbLuotkham objLuotkham, EmrPt01Bienbanhoichanthongquamo _phieu)
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
            _phieu = new Select().From(EmrPt01Bienbanhoichanthongquamo.Schema)
                        .Where(EmrPt01Bienbanhoichanthongquamo.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(EmrPt01Bienbanhoichanthongquamo.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<EmrPt01Bienbanhoichanthongquamo>();
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
                    _phieu = new Select().From(EmrPt01Bienbanhoichanthongquamo.Schema)
                        .Where(EmrPt01Bienbanhoichanthongquamo.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(EmrPt01Bienbanhoichanthongquamo.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<EmrPt01Bienbanhoichanthongquamo>();

                txtId.Text = "";
                if (_phieu != null)
                {
                    txtId.Text = _phieu.Id.ToString();
                    txtSoHoso.Text = _phieu.MaPhieu;
                    dtp_NgayBienBan.Value = _phieu.NgayBienban;
                    opt_capcuu.Checked = Utility.Bool2Bool(_phieu.Capcuu);
                    opt_bancap.Checked = Utility.Bool2Bool(_phieu.Bancap);
                    opt_chuongtrinh_phien.Checked = Utility.Bool2Bool(_phieu.ChuongtrinhPhien);
                    dtp_NgayHoiChan.Value = _phieu.NgayHoichan.Value;
                    cbo_bacsy_gayme.SelectedValue = _phieu.IdBacsyGayme;
                    cbo_bacsy_phauthuat.SelectedValue = _phieu.IdBacsyPhauthuat;
                    cbo_lanhdao_duyetmo.SelectedValue = _phieu.IdLanhdaoDuyetmo;
                    cbo_lanhdao_khoa_cls.SelectedValue = _phieu.IdLanhdaokhoaLamsang;

                    // txt_khoa.SetId(_phieu.idkho);

                    txt_tomtat_tinhtrangbenh.Text = _phieu.TomtatTinhtrangbenh;
                    txt_cacxetnghiemcdha.Text = _phieu.Cacxetnghiemcdha;
                    txtNhommau.SetCode(_phieu.Nhommau);
                    nmr_dutrumau.Value = Utility.Int32Dbnull(_phieu.Dutrumau);
                    cbo_phuongphapphauthuat.Text = _phieu.Phuongphapphauthuat;
                    cbo_phuongphapvocam_dukien.Text = _phieu.PhuongphapvocamDukien;
                    opt_Mallampati_loai1.Checked = Utility.Bool2Bool(_phieu.MallampatiLoai1);
                    opt_Mallampati_loai2.Checked = Utility.Bool2Bool(_phieu.MallampatiLoai2);
                    opt_Mallampati_loai3.Checked = Utility.Bool2Bool(_phieu.MallampatiLoai3);
                    opt_Mallampati_loai4.Checked = Utility.Bool2Bool(_phieu.MallampatiLoai4);

                    opt_loaiphauthuat_dacbiet.Checked = Utility.Bool2Bool(_phieu.LoaiphauthuatDacbiet);
                    opt_loaiphauthuat_loai1.Checked = Utility.Bool2Bool(_phieu.LoaiphauthuatLoai1);
                    opt_loaiphauthuat_loai2.Checked = Utility.Bool2Bool(_phieu.LoaiphauthuatLoai2);
                    opt_loaiphauthuat_loai3.Checked = Utility.Bool2Bool(_phieu.LoaiphauthuatLoai3);

                    opt_phanloaiAsa_loai1.Checked = Utility.Bool2Bool(_phieu.PhanloaiAsaLoai1);
                    opt_phanloaiAsa_loai2.Checked = Utility.Bool2Bool(_phieu.PhanloaiAsaLoai2);
                    opt_phanloaiAsa_loai3.Checked = Utility.Bool2Bool(_phieu.PhanloaiAsaLoai3);
                    opt_phanloaiAsa_loai4.Checked = Utility.Bool2Bool(_phieu.PhanloaiAsaLoai4);
                    opt_phanloaiAsa_loai5.Checked = Utility.Bool2Bool(_phieu.PhanloaiAsaLoai5);

                    opt_phanloainguyco_sach.Checked = Utility.Bool2Bool(_phieu.PhanloainguycoSach);
                    opt_phanloainguyco_sachnhiem.Checked = Utility.Bool2Bool(_phieu.PhanloainguycoSachnhiem);
                    opt_phanloainguyco_nhiem.Checked = Utility.Bool2Bool(_phieu.PhanloainguycoNhiem);
                    opt_phanloainguyco_ban.Checked = Utility.Bool2Bool(_phieu.PhanloainguycoBan);

                    txt_khangsinh_duphong.Text = _phieu.KhangsinhDuphong;
                    dtp_ngay_dukien.Value = _phieu.NgayDukien.Value;
                    txt_cacbienchung_nguyco_khokhan_luuy.Text = _phieu.CacbienchungNguycoKhokhanLuuy;
                    txt_cacbienphapthaythe_yc_chuanbidacbiet.Text = _phieu.CacbienphapthaytheYcChuanbidacbiet;

                }
                else
                {
                    ClearControl();

                }
                txtSoHoso.Text = _phieu == null || string.IsNullOrEmpty(Utility.sDbnull(_phieu.MaPhieu, "")) ? THU_VIEN_CHUNG.TT25LaySohoso(10) : Utility.sDbnull(_phieu.MaPhieu, "");
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
            if (!opt_capcuu.Checked && !opt_bancap.Checked &&
               !opt_chuongtrinh_phien.Checked )
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
            DataTable dtData = new Select().From(EmrPt01Bienbanhoichanthongquamo.Schema)
              .Where(EmrPt01Bienbanhoichanthongquamo.Columns.MaPhieu).IsEqualTo(Utility.DoTrim(txtSoHoso.Text))
              .And(EmrPt01Bienbanhoichanthongquamo.Columns.Id).IsNotEqualTo(Utility.Int64Dbnull(txtId.Text, -1))
              .ExecuteDataSet().Tables[0];
            if (dtData.Rows.Count > 0)
            {
                Msg = "Mã phiếu đã được sử dụng. Vui lòng nhập mã phiếu khác";
                txtSoHoso.Focus();
                return false;
            }
            if (dtp_NgayHoiChan.Text == "")
            {
                Msg = "Phải nhập thời gian hội chẩn";
                if (_OnMsg != null) _OnMsg(Msg, false);
                dtp_NgayHoiChan.Focus();
                return false;
            }
            if (Utility.Int32Dbnull( cbo_bacsy_phauthuat.SelectedValue)<=0)
            {
                Msg = "Bạn phải chọn Bác sĩ phẫu thuật từ danh mục bác sĩ";
                if (_OnMsg != null) _OnMsg(Msg);
                cbo_bacsy_phauthuat.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(cbo_bacsy_gayme.SelectedValue) <= 0)
            {
                Msg = "Bạn phải chọn Bác sĩ Gây mê từ danh mục bác sĩ";
                if (_OnMsg != null) _OnMsg(Msg);
                cbo_bacsy_gayme.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(cbo_lanhdao_khoa_cls.SelectedValue) <= 0)
            {
                Msg = "Bạn phải chọn Lãnh đạo khoa lâm sàng từ danh mục bác sĩ";
                if (_OnMsg != null) _OnMsg(Msg);
                cbo_lanhdao_khoa_cls.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(cbo_lanhdao_duyetmo.SelectedValue) <= 0)
            {
                Msg = "Bạn phải chọn Lãnh đạo duyệt mổ từ danh mục bác sĩ";
                if (_OnMsg != null) _OnMsg(Msg);
                cbo_lanhdao_duyetmo.Focus();
                return false;
            }

            if (Utility.sDbnull( txt_tomtat_tinhtrangbenh.Text)=="")
            {
                Msg = "Bạn phải nhập Tóm tắt tình trạng bệnh";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_tomtat_tinhtrangbenh.Focus();
                return false;
            }
            if (Utility.sDbnull(txt_cacxetnghiemcdha.Text) == "")
            {
                Msg = "Bạn phải nhập Các xét nghiệm, CĐHA";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_cacxetnghiemcdha.Focus();
                return false;
            }
            if (txtNhommau.myCode == "-1")
            {
                Msg = "Bạn phải nhập nhóm máu trong danh mục";
                if (_OnMsg != null) _OnMsg(Msg);
                txtNhommau.SelectAll();
                txtNhommau.Focus();
                return false;
            }
            if (cbo_phuongphapphauthuat.Text=="")
            {
                Msg = "Bạn phải nhập Phương pháp phẫu thuật";
                if (_OnMsg != null) _OnMsg(Msg);
                cbo_phuongphapphauthuat.Focus();
                return false;
            }
            if (Utility.sDbnull(cbo_phuongphapvocam_dukien) == "")
            {
                Msg = "Bạn phải nhập Phương pháp vô cảm";
                if (_OnMsg != null) _OnMsg(Msg);
                cbo_phuongphapvocam_dukien.Focus();
                return false;
            }
            if (!opt_Mallampati_loai1.Checked && !opt_Mallampati_loai2.Checked &&
                !opt_Mallampati_loai3.Checked &&
                !opt_Mallampati_loai4.Checked  )
            {
                Msg = "Bạn phải chọn Mallampati";
                if (_OnMsg != null) _OnMsg(Msg);
                opt_Mallampati_loai1.Focus();
                return false;
            }
            if (!opt_loaiphauthuat_dacbiet.Checked && !opt_loaiphauthuat_loai1.Checked &&
                !opt_loaiphauthuat_loai2.Checked &&
                !opt_loaiphauthuat_loai3.Checked)
            {
                Msg = "Bạn phải chọn Loại phẫu thuật";
                if (_OnMsg != null) _OnMsg(Msg);
                opt_loaiphauthuat_dacbiet.Focus();
                return false;
            }
            if (!opt_phanloaiAsa_loai1.Checked && !opt_phanloaiAsa_loai2.Checked &&
                !opt_phanloaiAsa_loai3.Checked &&
                !opt_phanloaiAsa_loai4.Checked && !opt_phanloaiAsa_loai5.Checked)
            {
                Msg = "Bạn phải chọn Mallampati";
                if (_OnMsg != null) _OnMsg(Msg);
                opt_phanloaiAsa_loai1.Focus();
                return false;
            }
            if (!opt_phanloainguyco_sach.Checked && !opt_phanloainguyco_sachnhiem.Checked &&
                !opt_phanloainguyco_nhiem.Checked && !opt_phanloainguyco_ban.Checked)
            {
                Msg = "Bạn phải chọn Phân loại nguy cơ nhiễm khuẩn vết mổ";
                if (_OnMsg != null) _OnMsg(Msg);
                opt_phanloainguyco_sach.Focus();
                return false;
            }
            //
            if (Utility.sDbnull(txt_khangsinh_duphong.Text) == "")
            {
                Msg = "Bạn phải nhập thông tin người liên hệ";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_khangsinh_duphong.SelectAll();
                txt_khangsinh_duphong.Focus();
                return false;
            }
            if (dtp_ngay_dukien.Text == "")
            {
                Msg = "Phải nhập Ngày, giờ phẫu thuật dự kiến ";
                if (_OnMsg != null) _OnMsg(Msg, false);
                dtp_ngay_dukien.Focus();
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
                        _phieu = new Select().From(EmrPt01Bienbanhoichanthongquamo.Schema)
                   .Where(EmrPt01Bienbanhoichanthongquamo.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(EmrPt01Bienbanhoichanthongquamo.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<EmrPt01Bienbanhoichanthongquamo>();
                       
                        if (_phieu == null || _phieu.Id <= 0)
                        {
                            isNew = true;
                            _phieu = new EmrPt01Bienbanhoichanthongquamo();
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
                        _phieu.MaPhieu =Utility.sDbnull( txtSoHoso.Text);
                        _phieu.NgayBienban = dtp_NgayBienBan.Value;
                        _phieu.Capcuu = opt_capcuu.Checked;
                        _phieu.Bancap = opt_bancap.Checked;
                        _phieu.ChuongtrinhPhien = opt_chuongtrinh_phien.Checked;
                        _phieu.NgayHoichan = dtp_NgayHoiChan.Value;
                        _phieu.IdBacsyGayme = Utility.Int32Dbnull(cbo_bacsy_gayme.SelectedValue);
                        _phieu.IdBacsyPhauthuat = Utility.Int32Dbnull(cbo_bacsy_phauthuat.SelectedValue);
                        _phieu.IdLanhdaoDuyetmo = Utility.Int32Dbnull(cbo_lanhdao_duyetmo.SelectedValue);
                        _phieu.IdLanhdaokhoaLamsang = Utility.Int32Dbnull(cbo_lanhdao_khoa_cls.SelectedValue);

                        // _phieu.idkho = txt_khoa.GetId();

                        _phieu.TomtatTinhtrangbenh = Utility.sDbnull(txt_tomtat_tinhtrangbenh.Text);
                        _phieu.Cacxetnghiemcdha = Utility.sDbnull(txt_cacxetnghiemcdha.Text);
                        _phieu.Nhommau = txtNhommau.myCode;
                        _phieu.Dutrumau = Utility.Int32Dbnull(nmr_dutrumau.Value);
                        _phieu.Phuongphapphauthuat = Utility.sDbnull(cbo_phuongphapphauthuat.Text);
                        _phieu.PhuongphapvocamDukien = Utility.sDbnull(cbo_phuongphapvocam_dukien.Text);

                        _phieu.MallampatiLoai1 = opt_Mallampati_loai1.Checked;
                        _phieu.MallampatiLoai2 = opt_Mallampati_loai2.Checked;
                        _phieu.MallampatiLoai3 = opt_Mallampati_loai3.Checked;
                        _phieu.MallampatiLoai4 = opt_Mallampati_loai4.Checked;

                        _phieu.LoaiphauthuatDacbiet = opt_loaiphauthuat_dacbiet.Checked;
                        _phieu.LoaiphauthuatLoai1 = opt_loaiphauthuat_loai1.Checked;
                        _phieu.LoaiphauthuatLoai2 = opt_loaiphauthuat_loai2.Checked;
                        _phieu.LoaiphauthuatLoai3 = opt_loaiphauthuat_loai3.Checked;

                        _phieu.PhanloaiAsaLoai1 = opt_phanloaiAsa_loai1.Checked;
                        _phieu.PhanloaiAsaLoai2 = opt_phanloaiAsa_loai2.Checked;
                        _phieu.PhanloaiAsaLoai3 = opt_phanloaiAsa_loai3.Checked;
                        _phieu.PhanloaiAsaLoai4 = opt_phanloaiAsa_loai4.Checked;
                        _phieu.PhanloaiAsaLoai5 = opt_phanloaiAsa_loai5.Checked;

                        _phieu.PhanloainguycoSach = opt_phanloainguyco_sach.Checked;
                        _phieu.PhanloainguycoSachnhiem = opt_phanloainguyco_sachnhiem.Checked;
                        _phieu.PhanloainguycoNhiem = opt_phanloainguyco_nhiem.Checked;
                        _phieu.PhanloainguycoBan = opt_phanloainguyco_ban.Checked;

                        _phieu.KhangsinhDuphong = txt_khangsinh_duphong.Text;
                        _phieu.NgayDukien = dtp_ngay_dukien.Value;
                        _phieu.CacbienchungNguycoKhokhanLuuy = Utility.sDbnull(txt_cacbienchung_nguyco_khokhan_luuy.Text);
                        _phieu.CacbienphapthaytheYcChuanbidacbiet = Utility.sDbnull(txt_cacbienphapthaythe_yc_chuanbidacbiet.Text);


                        _phieu.Save();
                        if (objBacsiPttt == null)
                            objBacsiPttt = DmucNhanvien.FetchByID(Utility.Int32Dbnull(cbo_bacsy_phauthuat.SelectedValue));
                        emrdoc.Force2Saved = Force2Saved;
                        emrdoc.InitDocument(Utility.Int64Dbnull(_phieu.IdBenhnhan), _phieu.MaLuotkham, Utility.Int64Dbnull(_phieu.Id), _phieu.NgayBienban, Loaiphieu_HIS.BIENBANHOICHAN_THONGQUAMO, "BIENBANHOICHAN_THONGQUAMO", _phieu.NguoiTao,Utility.Int16Dbnull( objBacsiPttt.IdKhoa), Utility.Int16Dbnull(objBacsiPttt.IdPhong), Utility.Byte2Bool(0),"");
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
                _phieu = new Select().From(EmrPt01Bienbanhoichanthongquamo.Schema)
                       .Where(EmrPt01Bienbanhoichanthongquamo.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                       .And(EmrPt01Bienbanhoichanthongquamo.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                       .ExecuteSingle<EmrPt01Bienbanhoichanthongquamo>();
                if (_phieu.Id <= 0)
                {
                    Utility.ShowMsg("Bạn cần lưu thông tin Biên bản hội chẩn thông qua mổ trước khi thực hiện in phiếu");
                    return;
                }
                DataTable dtData = SPs.EmrPt01BienbanhoichanthongquamoLaythongtinIn(_phieu.Id).GetDataSet().Tables[0];
                dtData.TableName = "BIENBANHOICHAN_THONGQUAMO";
                dtData.Rows[0]["sngay_bienban"] = _phieu != null ? Utility.FormatDateTime_gio_ngay_thang_nam(_phieu.NgayBienban, "") : "....... giờ.......ngày................./............../20..............";
                dtData.Rows[0]["sngay_dukien"] = _phieu != null ? Utility.FormatDateTime_gio_ngay_thang_nam(_phieu.NgayDukien, "") : "........giờ...........phút, ngày........./........./20.........";
                dtData.Rows[0]["sngay_hoichan"] = _phieu != null ? Utility.FormatDateTime_gio_ngay_thang_nam(_phieu.NgayHoichan, "") : "....... giờ.......ngày................./............../20..............";
                dtData.Rows[0]["sngay_nhapvien"] = _phieu != null ? Utility.FormatDateTime_gio_ngay_thang_nam(Convert.ToDateTime(dtData.Rows[0]["ngay_nhapvien"]), "") : "....... giờ.......ngày................./............../20..............";
                WordPrinter.InPhieu(dtData, "BIENBANHOICHAN_THONGQUAMO.doc", "",false, @"\MergeFields\BIENBANHOICHAN_THONGQUAMO_CHECKED_FIELDS.txt");


            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdTuSinh_Click(object sender, EventArgs e)
        {
            txtSoHoso.Text = THU_VIEN_CHUNG.TT25LaySohoso(10);
        }

        private void cbo_bacsy_phauthuat_SelectedIndexChanged(object sender, EventArgs e)
        {
            objBacsiPttt = DmucNhanvien.FetchByID(Utility.Int32Dbnull(cbo_bacsy_phauthuat.SelectedValue));
        }
    }
}
