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
    public partial class uc_phieubangiaonguoibenhchuyenkhoa : UserControl
    {
        public delegate void OnMsg(string msg, bool IsSucess = false);
        public event OnMsg _OnMsg;
        public delegate void OnStatus(bool isNew);
        public event OnStatus _OnStatus;
        public EmrPhieubangiaonguoibenhchuyenkhoa phieubangiao;
        KcbLuotkham objLuotkham;
        public int id_bacsikham = -1;
        DmucNhanvien objBacsiPttt = null;
        DmucNhanvien objNguoiDaidien = null;
        public uc_phieubangiaonguoibenhchuyenkhoa()
        {
            InitializeComponent();
            txt_bacsi_chidinhchuyen._OnEnterMe += txt_bacsi_pttt_OnEnterMe;
        }

        private void txt_bacsi_pttt_OnEnterMe()
        {
            objBacsiPttt = DmucNhanvien.FetchByID(Utility.Int32Dbnull(txt_bacsi_chidinhchuyen.MyID));
            if (objBacsiPttt != null)
                txt_khoachuyen.SetId(Utility.Int16Dbnull( objBacsiPttt.IdKhoa));
        }

        public void Init(KcbLuotkham objLuotkham, EmrPhieubangiaonguoibenhchuyenkhoa phieubangiao)
        {
            dtp_ngaybangiao.Value = globalVariables.SysDate;
            this.objLuotkham = objLuotkham;
            this.phieubangiao = phieubangiao;
            txt_bacsi_chidinhchuyen.Init(globalVariables.gv_dtDmucNhanvien,
                                            new List<string>
                                 {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                 });
            txt_bacsi_nhan.Init(txt_bacsi_chidinhchuyen.AutoCompleteSource, txt_bacsi_chidinhchuyen.defaultItem);
            txt_bacsi_chuyen.Init(txt_bacsi_chidinhchuyen.AutoCompleteSource, txt_bacsi_chidinhchuyen.defaultItem);
            txt_dieuduongnhan.Init(txt_bacsi_chidinhchuyen.AutoCompleteSource, txt_bacsi_chidinhchuyen.defaultItem);
            txt_dieuduong_chuyen.Init(txt_bacsi_chidinhchuyen.AutoCompleteSource, txt_bacsi_chidinhchuyen.defaultItem);

            DataTable dtKhoaPhong = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL", 0);
            txt_khoachuyen.Init(dtKhoaPhong, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            txtKhoa.Init(txt_khoachuyen.AutoCompleteSource, txt_khoachuyen.defaultItem);
            txt_khoanhan.Init(txt_khoachuyen.AutoCompleteSource, txt_khoachuyen.defaultItem);

        }


        public void Init(KcbLuotkham objLuotkham)
        {
            dtp_ngaybangiao.Value = globalVariables.SysDate;
            this.objLuotkham = objLuotkham;
            phieubangiao = new Select().From(EmrPhieubangiaonguoibenhchuyenkhoa.Schema)
                        .Where(EmrPhieubangiaonguoibenhchuyenkhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(EmrPhieubangiaonguoibenhchuyenkhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<EmrPhieubangiaonguoibenhchuyenkhoa>();
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
        public void Init()
        {
            dtp_ngaybangiao.Value = globalVariables.SysDate;
            txt_bacsi_chidinhchuyen.Init(globalVariables.gv_dtDmucNhanvien,
                                             new List<string>
                                  {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                  });
            txt_bacsi_nhan.Init(txt_bacsi_chidinhchuyen.AutoCompleteSource, txt_bacsi_chidinhchuyen.defaultItem);
            txt_bacsi_chuyen.Init(txt_bacsi_chidinhchuyen.AutoCompleteSource, txt_bacsi_chidinhchuyen.defaultItem);
            txt_dieuduongnhan.Init(txt_bacsi_chidinhchuyen.AutoCompleteSource, txt_bacsi_chidinhchuyen.defaultItem);
            txt_dieuduong_chuyen.Init(txt_bacsi_chidinhchuyen.AutoCompleteSource, txt_bacsi_chidinhchuyen.defaultItem);
            DataTable dtKhoaPhong = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL", 0);
            txt_khoachuyen.Init(dtKhoaPhong, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            txtKhoa.Init(txt_khoachuyen.AutoCompleteSource, txt_khoachuyen.defaultItem);
            txt_khoanhan.Init(txt_khoachuyen.AutoCompleteSource, txt_khoachuyen.defaultItem);
        }
        public void DisplayData()
        {
            try
            {
               
                if (phieubangiao == null)
                    phieubangiao = new Select().From(EmrPhieubangiaonguoibenhchuyenkhoa.Schema)
                        .Where(EmrPhieubangiaonguoibenhchuyenkhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(EmrPhieubangiaonguoibenhchuyenkhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<EmrPhieubangiaonguoibenhchuyenkhoa>();
               
                txtId.Text = "";
                if (phieubangiao != null)
                {
                    txtId.Text = phieubangiao.IdPhieu.ToString();
                    txtSoHoso.Text = phieubangiao.MaPhieu;
                    dtp_ngaybangiao.Value = phieubangiao.NgayBangiao;
                    txtKhoa._Text = phieubangiao.Khoa;
                    txtBuong.Text = phieubangiao.Buong;
                    txtGiuong.Text = phieubangiao.Giuong;

                    txt_bacsi_chidinhchuyen.SetId(Utility.Int16Dbnull(phieubangiao.IdBacsiChidinhchuyen));
                    txt_bacsi_chuyen.SetId(Utility.Int16Dbnull(phieubangiao.IdBacsiBangiao));
                    txt_bacsi_nhan.SetId(Utility.Int16Dbnull(phieubangiao.IdBacsiNhan));
                    txt_dieuduongnhan.SetId(Utility.Int16Dbnull(phieubangiao.IdDieuduongKhoacnhan));
                    txt_dieuduong_chuyen.SetId(Utility.Int16Dbnull(phieubangiao.IdDieuduongKhoachuyen));

                    txt_khoachuyen.SetId(Utility.Int16Dbnull(phieubangiao.IdKhoabangiao));
                    txt_khoanhan.SetId(Utility.Int16Dbnull(phieubangiao.IdKhoanhan));

                    //Thông tin phiếu dành cho bác sĩ
                    txt_lydochuyen.Text = Utility.sDbnull(phieubangiao.Lydochuyen);
                    txt_lydonhapvien.Text = Utility.sDbnull(phieubangiao.Lydonhapvien);
                    txt_dienbienbenh.Text = Utility.sDbnull(phieubangiao.Dienbienbenh);
                    txt_chandoan.Text = Utility.sDbnull(phieubangiao.Chandoan);
                    txt_dacanthiep.Text = Utility.sDbnull(phieubangiao.Dacanthiep);
                    txt_tinhtranghientai.Text = Utility.sDbnull(phieubangiao.Tinhtranghientai);
                    txt_kehoachdieutritieptheo.Text = Utility.sDbnull(phieubangiao.Kehoachdieutritieptheo);
                    txt_lydochuyen.Text = Utility.sDbnull(phieubangiao.Lydochuyen);
                    //Thông tin phiếu dành cho điều dưỡng

                    chk_chuyentheoyeucaucuanguoibenh.Checked = Utility.Bool2Bool(phieubangiao.Chuyentheoyeucaucuanguoibenh);
                    opt_tinhtrangnguoibenh_tot.Checked = Utility.Bool2Bool(phieubangiao.TinhtrangnguoibenhTot);
                    opt_tinhtrangnguoibenh_nhe.Checked = Utility.Bool2Bool(phieubangiao.TinhtrangnguoibenhNhe);
                    opt_tinhtrangnguoibenh_nang.Checked = Utility.Bool2Bool(phieubangiao.TinhtrangnguoibenhNang);
                    //Mức độ tỉnh táo
                    chk_tinhtao.Checked = Utility.Bool2Bool(phieubangiao.Tinhtao);
                    chk_buongu_nguga.Checked = Utility.Bool2Bool(phieubangiao.BuonguNguga);
                    chk_kichdong.Checked = Utility.Bool2Bool(phieubangiao.Kichdong);
                    chk_lulan.Checked = Utility.Bool2Bool(phieubangiao.Lulan);
                    chk_honme.Checked = Utility.Bool2Bool(phieubangiao.Honme);
                    //Đau
                    opt_dau_co.Checked = Utility.Bool2Bool(phieubangiao.DauCo);
                    opt_dau_khong.Checked = Utility.Bool2Bool(phieubangiao.DauKhong);
                    txt_thangdiemdau.Text = Utility.sDbnull(phieubangiao.Thangdiemdau);
                    //Nguy cơ té ngã
                    opt_nguyco_te_nga_co.Checked = Utility.Bool2Bool(phieubangiao.NguycoTeNgaCo);
                    opt_nguyco_te_nga_khong.Checked = Utility.Bool2Bool(phieubangiao.NguycoTeNgaKhong);
                    txt_thangdiem_nguyco_te_nga.Text = Utility.sDbnull(phieubangiao.ThangdiemNguycoTeNga);
                    //Dị ứng
                    opt_diung_co.Checked = Utility.Bool2Bool(phieubangiao.DiungCo);
                    opt_diung_khong.Checked = Utility.Bool2Bool(phieubangiao.DiungKhong);
                    txt_diung_mota.Text =  Utility.sDbnull(phieubangiao.DiungMota);

                    chk_duongtruyentinhmach_ngoaibien.Checked = Utility.Bool2Bool(phieubangiao.DuongtruyentinhmachNgoaibien);
                    txt_duongtruyentinhmach_ngoaibien_noidat.Text = Utility.sDbnull(phieubangiao.DuongtruyentinhmachNgoaibienNoidat);
                    dtp_duongtruyendongmach_ngaydat.Value = phieubangiao.DuongtruyentinhmachNgoaibienNgaydat.HasValue ? phieubangiao.DuongtruyentinhmachNgoaibienNgaydat.Value : DateTime.Now;

                    chk_duongtruyentinhmach_trungtam.Checked = Utility.Bool2Bool(phieubangiao.DuongtruyentinhmachTrungtam);
                    txt_duongtruyentinhmach_trungtam_noidat.Text = Utility.sDbnull(phieubangiao.DuongtruyentinhmachTrungtamNoidat);
                    dtp_duongtruyentinhmach_trungtam_ngaydat.Value = phieubangiao.DuongtruyentinhmachTrungtamNgaydat.HasValue ? phieubangiao.DuongtruyentinhmachTrungtamNgaydat.Value : DateTime.Now;

                    chk_duongtruyendongmach.Checked = Utility.Bool2Bool(phieubangiao.Duongtruyendongmach);
                    txt_duongtruyendongmach_noidat.Text = Utility.sDbnull(phieubangiao.DuongtruyendongmachNoidat);
                    dtp_duongtruyendongmach_ngaydat.Value = phieubangiao.DuongtruyendongmachNgaydat.HasValue ? phieubangiao.DuongtruyendongmachNgaydat.Value : DateTime.Now;

                    chk_ongthongtieu.Checked = Utility.Bool2Bool(phieubangiao.Ongthongtieu);
                    txt_ongthongtieu_noidat.Text = Utility.sDbnull(phieubangiao.OngthongtieuNoidat);
                    dtp_ongthongtieu_ngaydat.Value = phieubangiao.OngthongtieuNgaydat.HasValue ? phieubangiao.OngthongtieuNgaydat.Value : DateTime.Now;

                    chk_danluu.Checked = Utility.Bool2Bool(phieubangiao.Danluu);
                    txt_danluu_noidat.Text = Utility.sDbnull(phieubangiao.DanluuNoidat);
                    dtp_danluu_ngaydat.Value = phieubangiao.DanluuNgaydat.HasValue ? phieubangiao.DanluuNgaydat.Value : DateTime.Now;
                    //Khác
                    txt_khac.Text = Utility.sDbnull(phieubangiao.Khac);

                   
                    chk_lieutho_oxy.Checked = Utility.Bool2Bool(phieubangiao.LieuthoOxy);
                    nmr_lieutho_oxy_mota.Text = Utility.sDbnull(phieubangiao.LieuthoOxyMota);
                    //Da
                    chk_da_vetloetdotide.Checked = Utility.Bool2Bool(phieubangiao.DaVetloetdotide);
                    txt_da_vetloetdotide_mota.Text = Utility.sDbnull(phieubangiao.DaVetloetdotideMota);

                    txt_da_bangvetthuong_mota.Text = Utility.sDbnull(phieubangiao.DaBangvetthuongMota);
                    chk_da_bangvetthuong.Checked = Utility.Bool2Bool(phieubangiao.DaBangvetthuong);

                    dtp_ngaycatchi.Value = phieubangiao.Ngaycatchi.HasValue ? phieubangiao.Ngaycatchi.Value : DateTime.Now;
                    //Dinh dưỡng
                    chk_dinhduong_nhin_anuong.Checked = Utility.Bool2Bool(phieubangiao.DinhduongNhinAnuong);
                    chk_dinhduong_qua_ongthong.Checked = Utility.Bool2Bool(phieubangiao.DinhduongQuaOngthong);
                    chk_dinhduong_chedo_an.Checked = Utility.Bool2Bool(phieubangiao.DinhduongChedoAn);
                    txt_dinhduong_chedo_an_mota.Text = Utility.sDbnull(phieubangiao.DinhduongChedoAnMota);
                    //Vận động
                    chk_vandong_khongphuthuoc.Checked = Utility.Bool2Bool(phieubangiao.VandongKhongphuthuoc);
                    chk_vandong_xelan.Checked = Utility.Bool2Bool(phieubangiao.VandongXelan);
                    chk_vandong_ngoighe.Checked = Utility.Bool2Bool(phieubangiao.VandongNgoighe);
                    chk_vandong_namtuyetdoi_taigiuong.Checked = Utility.Bool2Bool(phieubangiao.VandongNamtuyetdoiTaigiuong);
                    //Bài tiết
                    chk_baitiet_tieucotuchu.Checked = Utility.Bool2Bool(phieubangiao.BaitietTieucotuchu);
                    chk_baitiet_tieukhongtuchu.Checked = Utility.Bool2Bool(phieubangiao.BaitietTieukhongtuchu);
                    chk_baitiet_qualobaitiet.Checked = Utility.Bool2Bool(phieubangiao.BaitietQualobaitiet);

                    //
                    opt_thuocdadieutri_co.Checked = Utility.Bool2Bool(phieubangiao.ThuocdadieutriCo);
                    opt_thuocdadieutri_khong.Checked = Utility.Bool2Bool(phieubangiao.ThuocdadieutriKhong);
                    dtp_thuocdadieutri_luc.Value = phieubangiao.ThuocdadieutriLuc.HasValue ? phieubangiao.ThuocdadieutriLuc.Value : DateTime.Now;

                    opt_thuoccansudungtiep_co.Checked = Utility.Bool2Bool(phieubangiao.ThuoccansudungtiepCo);
                    opt_thuoccansudungtiep_khong.Checked = Utility.Bool2Bool(phieubangiao.ThuoccansudungtiepKhong);
                    dtp_thuoccansudungtiep_luc.Value = phieubangiao.ThuoccansudungtiepLuc.HasValue ? phieubangiao.ThuoccansudungtiepLuc.Value : DateTime.Now;


                    chk_tailieubangiao_hosobenhan.Checked = Utility.Bool2Bool(phieubangiao.TailieubangiaoHosobenhan);
                    chk_tailieubangiao_vatdungcanhan.Checked = Utility.Bool2Bool(phieubangiao.TailieubangiaoVatdungcanhan);
                    chk_tailieubangiao_khac.Checked = Utility.Bool2Bool(phieubangiao.TailieubangiaoKhac);
                    txt_tailieubangiao_mota.Text = phieubangiao.TailieubangiaoMota;
                    

                }
                else
                {
                    ClearControl();
                    
                }
                txtSoHoso.Text = phieubangiao == null || string.IsNullOrEmpty(Utility.sDbnull(phieubangiao.MaPhieu, "")) ? THU_VIEN_CHUNG.TT25LaySohoso(6) : Utility.sDbnull(phieubangiao.MaPhieu, "");
                if (_OnStatus != null) _OnStatus(phieubangiao == null || phieubangiao.IdPhieu <= 0);
            }
            catch (System.Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
       void ClearControl()
        {
            foreach (Control ctr in this.Controls)
                if (ctr.GetType().Equals(autoTxt.GetType()))
                    ((AutoCompleteTextbox_Danhmucchung)ctr).SetDefaultItem();
                else if (ctr is EditBox)
                {
                    ((EditBox)(ctr)).Clear();
                }
                else if (ctr is CheckBox)
                {
                    ((CheckBox)(ctr)).Checked=false;
                }
                else if (ctr is DateTimePicker)
                {
                    ((DateTimePicker)(ctr)).Value = globalVariables.SysDate;
                }
        }
        string Msg = "";
        bool isValidData()
        {
            Msg = "";
            
            if (Utility.sDbnull(txtSoHoso.Text)=="")
            {
                Msg = "Bạn phải nhập mã phiếu";
                if (_OnMsg != null) _OnMsg(Msg);
                txtSoHoso.Focus();
                return false;
            }
            DataTable dtData = new Select().From(EmrPhieubangiaonguoibenhchuyenkhoa.Schema)
              .Where(EmrPhieubangiaonguoibenhchuyenkhoa.Columns.MaPhieu).IsEqualTo(Utility.DoTrim(txtSoHoso.Text))
              .And(EmrPhieubangiaonguoibenhchuyenkhoa.Columns.IdPhieu).IsNotEqualTo(Utility.Int64Dbnull(txtId.Text, -1))
              .ExecuteDataSet().Tables[0];
            if (dtData.Rows.Count > 0)
            {
                Msg = "Mã phiếu đã được sử dụng. Vui lòng nhập mã phiếu khác. Hoặc nhấn nút refresh bên cạnh để sinh mã mới";
                txtSoHoso.Focus();
                return false;
            }
            if (Utility.sDbnull(txtKhoa.Text) == "")
            {
                Msg = "Bạn phải nhập thông tin khoa thực hiện";
                if (_OnMsg != null) _OnMsg(Msg);
                txtKhoa.SelectAll();
                txtKhoa.Focus();
                return false;
            }
           

            if (txt_bacsi_chidinhchuyen.MyID=="-1")
            {
                Msg = "Bạn phải chọn Bác sĩ chỉ định chuyển người bệnh";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_bacsi_chidinhchuyen.SelectAll();
                txt_bacsi_chidinhchuyen.Focus();
                return false;
            }
            if (txt_khoachuyen.MyID == "-1")
            {
                Msg = "Bạn phải chọn Khoa bàn giao";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_khoachuyen.SelectAll();
                txt_khoachuyen.Focus();
                return false;
            }
            if (txt_khoanhan.MyID == "-1")
            {
                Msg = "Bạn phải chọn khoa nhận";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_khoanhan.SelectAll();
                txt_khoanhan.Focus();
                return false;
            }
            if (txt_bacsi_chuyen.MyID == "-1")
            {
                Msg = "Bạn phải chọn Bác sĩ bàn giao";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_bacsi_chuyen.SelectAll();
                txt_bacsi_chuyen.Focus();
                return false;
            }
            if (txt_bacsi_nhan.MyID == "-1")
            {
                Msg = "Bạn phải chọn Bác sĩ nhận";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_bacsi_nhan.SelectAll();
                txt_bacsi_nhan.Focus();
                return false;
            }
            if (txt_dieuduong_chuyen.MyID == "-1")
            {
                Msg = "Bạn phải chọn điều dưỡng bàn giao";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_dieuduong_chuyen.SelectAll();
                txt_dieuduong_chuyen.Focus();
                return false;
            }

            if (txt_dieuduongnhan.MyID == "-1")
            {
                Msg = "Bạn phải chọn Điều dưỡng nhận";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_dieuduongnhan.SelectAll();
                txt_dieuduongnhan.Focus();
                return false;
            }

            if (Utility.sDbnull(txt_lydochuyen.Text) == "")
            {
                Msg = "Bạn phải nhập thông tin Lý do chuyển";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_lydochuyen.SelectAll();
                txt_lydochuyen.Focus();
                return false;
            }
            if (Utility.sDbnull(txt_lydonhapvien.Text) == "")
            {
                Msg = "Bạn phải nhập thông tin Lý do nhập viện";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_lydonhapvien.SelectAll();
                txt_lydonhapvien.Focus();
                return false;
            }
            if (Utility.sDbnull(txt_dienbienbenh.Text) == "")
            {
                Msg = "Bạn phải nhập thông tin Diễn biến bệnh";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_dienbienbenh.SelectAll();
                txt_dienbienbenh.Focus();
                return false;
            }
            if (Utility.sDbnull(txt_chandoan.Text) == "")
            {
                Msg = "Bạn phải nhập thông tin Chẩn đoán";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_chandoan.SelectAll();
                txt_chandoan.Focus();
                return false;
            }
            if (Utility.sDbnull(txt_dacanthiep.Text) == "")
            {
                Msg = "Bạn phải nhập thông tin Đã can thiệp";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_dacanthiep.SelectAll();
                txt_dacanthiep.Focus();
                return false;
            }
            if (Utility.sDbnull(txt_tinhtranghientai.Text) == "")
            {
                Msg = "Bạn phải nhập thông tin Tình trạng hiện tại của người bệnh";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_tinhtranghientai.SelectAll();
                txt_tinhtranghientai.Focus();
                return false;
            }
            if (Utility.sDbnull(txt_kehoachdieutritieptheo.Text) == "")
            {
                Msg = "Bạn phải nhập thông tin Kế hoạch điều trị tiếp theo";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_kehoachdieutritieptheo.SelectAll();
                txt_kehoachdieutritieptheo.Focus();
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
                        phieubangiao = new Select().From(EmrPhieubangiaonguoibenhchuyenkhoa.Schema)
                   .Where(EmrPhieubangiaonguoibenhchuyenkhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(EmrPhieubangiaonguoibenhchuyenkhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<EmrPhieubangiaonguoibenhchuyenkhoa>();
                       
                        if (phieubangiao == null || phieubangiao.IdPhieu <= 0)
                        {
                            isNew = true;
                            phieubangiao = new EmrPhieubangiaonguoibenhchuyenkhoa();
                            phieubangiao.IsNew = true;
                            phieubangiao.NgayTao = DateTime.Now;
                            phieubangiao.NguoiTao = globalVariables.UserName;
                        }
                        else
                        {
                            isNew = false;
                            phieubangiao.IsNew = false;
                            phieubangiao.MarkOld();
                            phieubangiao.NgaySua = DateTime.Now;
                            phieubangiao.NguoiSua = globalVariables.UserName;
                        }
                        phieubangiao.IdBenhnhan = objLuotkham.IdBenhnhan;
                        phieubangiao.MaLuotkham = objLuotkham.MaLuotkham;
                        phieubangiao.MaPhieu = Utility.sDbnull(txtSoHoso.Text);
                        phieubangiao.NgayBangiao = dtp_ngaybangiao.Value;
                        phieubangiao.Khoa = Utility.sDbnull(txtKhoa.Text);
                        phieubangiao.Buong = Utility.sDbnull(txtBuong.Text);
                        phieubangiao.Giuong = Utility.sDbnull(txtGiuong.Text);

                        phieubangiao.IdBacsiChidinhchuyen = Utility.Int16Dbnull(txt_bacsi_chidinhchuyen.MyID);
                        phieubangiao.IdBacsiBangiao = Utility.Int16Dbnull(txt_bacsi_chuyen.MyID);
                        phieubangiao.IdBacsiNhan = Utility.Int16Dbnull(txt_bacsi_nhan.MyID);
                        phieubangiao.IdDieuduongKhoacnhan = Utility.Int16Dbnull(txt_dieuduongnhan.MyID);
                        phieubangiao.IdDieuduongKhoachuyen = Utility.Int16Dbnull(txt_dieuduong_chuyen.MyID);

                        phieubangiao.IdKhoabangiao = Utility.Int16Dbnull(txt_khoachuyen.MyID);
                        phieubangiao.IdKhoanhan = Utility.Int16Dbnull(txt_khoanhan.MyID);

                        // Thông tin phiếu dành cho bác sĩ
                        phieubangiao.Lydochuyen = Utility.sDbnull(txt_lydochuyen.Text);
                        phieubangiao.Lydonhapvien = Utility.sDbnull(txt_lydonhapvien.Text);
                        phieubangiao.Dienbienbenh = Utility.sDbnull(txt_dienbienbenh.Text);
                        phieubangiao.Chandoan = Utility.sDbnull(txt_chandoan.Text);
                        phieubangiao.Dacanthiep = Utility.sDbnull(txt_dacanthiep.Text);
                        phieubangiao.Tinhtranghientai = Utility.sDbnull(txt_tinhtranghientai.Text);
                        phieubangiao.Kehoachdieutritieptheo = Utility.sDbnull(txt_kehoachdieutritieptheo.Text);
                        phieubangiao.Lydochuyen = Utility.sDbnull(txt_lydochuyen.Text);

                        phieubangiao.Chuyentheoyeucaucuanguoibenh = chk_chuyentheoyeucaucuanguoibenh.Checked;

                        phieubangiao.TinhtrangnguoibenhTot = opt_tinhtrangnguoibenh_tot.Checked;
                        phieubangiao.TinhtrangnguoibenhNhe = opt_tinhtrangnguoibenh_nhe.Checked;
                        phieubangiao.TinhtrangnguoibenhNang = opt_tinhtrangnguoibenh_nang.Checked;

                        // Mức độ tỉnh táo
                        phieubangiao.Tinhtao = chk_tinhtao.Checked;
                        phieubangiao.BuonguNguga = chk_buongu_nguga.Checked;
                        phieubangiao.Kichdong = chk_kichdong.Checked;
                        phieubangiao.Lulan = chk_lulan.Checked;
                        phieubangiao.Honme = chk_honme.Checked;

                        // Đau
                        phieubangiao.DauCo = opt_dau_co.Checked;
                        phieubangiao.DauKhong = opt_dau_khong.Checked;
                        phieubangiao.Thangdiemdau = opt_dau_co.Checked? Utility.sDbnull(txt_thangdiemdau.Text):"";

                        // Nguy cơ té ngã
                        phieubangiao.NguycoTeNgaCo = opt_nguyco_te_nga_co.Checked;
                        phieubangiao.NguycoTeNgaKhong = opt_nguyco_te_nga_khong.Checked;
                        phieubangiao.ThangdiemNguycoTeNga = opt_nguyco_te_nga_co.Checked? Utility.sDbnull(txt_thangdiem_nguyco_te_nga.Text):"";
                        DateTime? nulldate=null;
                        // Dị ứng
                        phieubangiao.DiungCo = opt_diung_co.Checked;
                        phieubangiao.DiungKhong = opt_diung_khong.Checked;
                        phieubangiao.DiungMota = opt_diung_co.Checked? Utility.sDbnull(txt_diung_mota.Text):"";

                        phieubangiao.DuongtruyentinhmachNgoaibien = chk_duongtruyentinhmach_ngoaibien.Checked;
                        phieubangiao.DuongtruyentinhmachNgoaibienNoidat = chk_duongtruyentinhmach_ngoaibien.Checked? Utility.sDbnull(txt_duongtruyentinhmach_ngoaibien_noidat.Text):"";
                        phieubangiao.DuongtruyentinhmachNgoaibienNgaydat = chk_duongtruyentinhmach_ngoaibien.Checked ? dtp_duongtruyendongmach_ngaydat.Value: nulldate;

                        phieubangiao.DuongtruyentinhmachTrungtam = chk_duongtruyentinhmach_trungtam.Checked;
                        phieubangiao.DuongtruyentinhmachTrungtamNoidat = chk_duongtruyentinhmach_trungtam.Checked? Utility.sDbnull(txt_duongtruyentinhmach_trungtam_noidat.Text):"";
                        phieubangiao.DuongtruyentinhmachTrungtamNgaydat = chk_duongtruyentinhmach_trungtam.Checked? dtp_duongtruyentinhmach_trungtam_ngaydat.Value:nulldate;

                        phieubangiao.Duongtruyendongmach = chk_duongtruyendongmach.Checked;
                        phieubangiao.DuongtruyendongmachNoidat = chk_duongtruyendongmach.Checked? Utility.sDbnull(txt_duongtruyendongmach_noidat.Text):"";
                        phieubangiao.DuongtruyendongmachNgaydat = chk_duongtruyendongmach.Checked? dtp_duongtruyendongmach_ngaydat.Value:nulldate;

                        phieubangiao.Ongthongtieu = chk_ongthongtieu.Checked;
                        phieubangiao.OngthongtieuNoidat = chk_ongthongtieu.Checked? Utility.sDbnull(txt_ongthongtieu_noidat.Text):"";
                        phieubangiao.OngthongtieuNgaydat = chk_ongthongtieu.Checked? dtp_ongthongtieu_ngaydat.Value:nulldate;

                        phieubangiao.Danluu = chk_danluu.Checked;
                        phieubangiao.DanluuNoidat = chk_danluu.Checked? Utility.sDbnull(txt_danluu_noidat.Text):"";
                        phieubangiao.DanluuNgaydat = chk_danluu.Checked? dtp_danluu_ngaydat.Value:nulldate;

                        // Khác
                        phieubangiao.Khac = Utility.sDbnull(txt_khac.Text);

                        phieubangiao.LieuthoOxy = chk_lieutho_oxy.Checked;
                        phieubangiao.LieuthoOxyMota = chk_lieutho_oxy.Checked? Utility.sDbnull(nmr_lieutho_oxy_mota.Text):"";

                        // Da
                        phieubangiao.DaVetloetdotide = chk_da_vetloetdotide.Checked;
                        phieubangiao.DaVetloetdotideMota = chk_da_vetloetdotide.Checked?Utility.sDbnull(txt_da_vetloetdotide_mota.Text):"";
                        phieubangiao.DaBangvetthuongMota = chk_da_bangvetthuong.Checked? Utility.sDbnull(txt_da_bangvetthuong_mota.Text):"";
                        phieubangiao.DaBangvetthuong = chk_da_bangvetthuong.Checked;

                        phieubangiao.Ngaycatchi = dtp_ngaycatchi.Value;

                        // Dinh dưỡng
                        phieubangiao.DinhduongNhinAnuong = chk_dinhduong_nhin_anuong.Checked;
                        phieubangiao.DinhduongQuaOngthong = chk_dinhduong_qua_ongthong.Checked;
                        phieubangiao.DinhduongChedoAn = chk_dinhduong_chedo_an.Checked;
                        phieubangiao.DinhduongChedoAnMota = chk_dinhduong_chedo_an.Checked? Utility.sDbnull(txt_dinhduong_chedo_an_mota.Text):"";

                        // Vận động
                        phieubangiao.VandongKhongphuthuoc = chk_vandong_khongphuthuoc.Checked;
                        phieubangiao.VandongXelan = chk_vandong_xelan.Checked;
                        phieubangiao.VandongNgoighe = chk_vandong_ngoighe.Checked;
                        phieubangiao.VandongNamtuyetdoiTaigiuong = chk_vandong_namtuyetdoi_taigiuong.Checked;

                        // Bài tiết
                        phieubangiao.BaitietTieucotuchu = chk_baitiet_tieucotuchu.Checked;
                        phieubangiao.BaitietTieukhongtuchu = chk_baitiet_tieukhongtuchu.Checked;
                        phieubangiao.BaitietQualobaitiet = chk_baitiet_qualobaitiet.Checked;

                        // Thuốc điều trị
                        phieubangiao.ThuocdadieutriCo = opt_thuocdadieutri_co.Checked;
                        phieubangiao.ThuocdadieutriKhong = opt_thuocdadieutri_khong.Checked;
                        phieubangiao.ThuocdadieutriLuc = dtp_thuocdadieutri_luc.Value;

                        phieubangiao.ThuoccansudungtiepCo = opt_thuoccansudungtiep_co.Checked;
                        phieubangiao.ThuoccansudungtiepKhong = opt_thuoccansudungtiep_khong.Checked;
                        phieubangiao.ThuoccansudungtiepLuc = dtp_thuoccansudungtiep_luc.Value;

                        // Tài liệu bàn giao
                        phieubangiao.TailieubangiaoHosobenhan = chk_tailieubangiao_hosobenhan.Checked;
                        phieubangiao.TailieubangiaoVatdungcanhan = chk_tailieubangiao_vatdungcanhan.Checked;
                        phieubangiao.TailieubangiaoKhac = chk_tailieubangiao_khac.Checked;
                        phieubangiao.TailieubangiaoMota = txt_tailieubangiao_mota.Text;

                        if(objBacsiPttt==null)
                            objBacsiPttt = DmucNhanvien.FetchByID(Utility.Int32Dbnull(txt_bacsi_chidinhchuyen.MyID));
                        phieubangiao.Save();
                        emrdoc.InitDocument(phieubangiao.IdBenhnhan, phieubangiao.MaLuotkham, Utility.Int64Dbnull(phieubangiao.IdPhieu), phieubangiao.NgayBangiao, Loaiphieu_HIS.PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA_BACSI, "PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA_BACSI", phieubangiao.NguoiTao,Utility.Int16Dbnull( txtKhoa.MyID), Utility.Int16Dbnull(objBacsiPttt.IdPhong), Utility.Byte2Bool(0),"",false,false,"",Loaiphieu_HIS.PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA);
                        emrdoc.Save();
                        emrdoc.InitDocument(phieubangiao.IdBenhnhan, phieubangiao.MaLuotkham, Utility.Int64Dbnull(phieubangiao.IdPhieu), phieubangiao.NgayBangiao, Loaiphieu_HIS.PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA_DIEUDUONG, "PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA_DIEUDUONG", phieubangiao.NguoiTao, Utility.Int16Dbnull(txtKhoa.MyID), Utility.Int16Dbnull(objBacsiPttt.IdPhong), Utility.Byte2Bool(0), "", false, false, "", Loaiphieu_HIS.PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA);
                        emrdoc.Save();

                    }
                    scope.Complete();
                }
                txtId.Text = phieubangiao.IdPhieu.ToString();
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
        public  void Print(bool isBacsi=false)
        {
            try
            {
                phieubangiao = new Select().From(EmrPhieubangiaonguoibenhchuyenkhoa.Schema)
                       .Where(EmrPhieubangiaonguoibenhchuyenkhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                       .And(EmrPhieubangiaonguoibenhchuyenkhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                       .ExecuteSingle<EmrPhieubangiaonguoibenhchuyenkhoa>();
                if (phieubangiao.IdPhieu <= 0)
                {
                    Utility.ShowMsg("Bạn cần lưu thông tin Phiếu bàn giao người bệnh chuyển khoa trước khi thực hiện in phiếu");
                    return;
                }
                DataTable dtData = SPs.EmrPhieubangiaonguoibenhchuyenkhoaLaythongtinIn(phieubangiao.IdPhieu).GetDataSet().Tables[0];
                dtData.TableName = "PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA";
                dtData.Rows[0]["sngay_bangiao"] = phieubangiao != null ? Utility.FormatDateTime_gio_ngay_thang_nam(phieubangiao.NgayBangiao, "") : "Ngày ......./......./..........";
                if (isBacsi)
                    WordPrinter.InPhieu(dtData, "PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA_BACSI.doc", "",false, @"doc\PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA_CHECKED_FIELDS.txt");
                else
                    WordPrinter.InPhieu(dtData, "PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA_DIEUDUONG.doc", "",false, @"doc\PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA_CHECKED_FIELDS.txt");


            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdTuSinh_Click(object sender, EventArgs e)
        {
            txtSoHoso.Text = THU_VIEN_CHUNG.TT25LaySohoso(7);
        }

       
        private void opt_dau_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_thangdiemdau.Enabled = opt_dau_co.Checked;
            txt_thangdiemdau.Focus();
        }

        private void opt_nguyco_te_nga_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_thangdiem_nguyco_te_nga.Enabled = opt_nguyco_te_nga_co.Checked;
            txt_thangdiem_nguyco_te_nga.Focus();
        }

        private void opt_diung_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_diung_mota.Enabled = opt_diung_co.Checked;
            txt_diung_mota.Focus();
        }

      

        private void chk_duongtruyentinhmach_ngoaibien_CheckedChanged(object sender, EventArgs e)
        {
            txt_duongtruyentinhmach_ngoaibien_noidat.Enabled = dtp_duongtruyentinhmach_ngoaibien_ngaydat.Enabled = chk_duongtruyentinhmach_ngoaibien.Checked;
            txt_duongtruyentinhmach_ngoaibien_noidat.Focus();
        }

        private void chk_duongtruyentinhmach_trungtam_CheckedChanged(object sender, EventArgs e)
        {
            txt_duongtruyentinhmach_trungtam_noidat.Enabled = dtp_duongtruyentinhmach_trungtam_ngaydat.Enabled = chk_duongtruyentinhmach_trungtam.Checked;
            txt_duongtruyentinhmach_trungtam_noidat.Focus();
        }

        private void chk_duongtruyendongmach_CheckedChanged(object sender, EventArgs e)
        {
            txt_duongtruyendongmach_noidat.Enabled = dtp_duongtruyendongmach_ngaydat.Enabled = chk_duongtruyendongmach.Checked;
            txt_duongtruyendongmach_noidat.Focus();
        }

        private void chk_ongthongtieu_CheckedChanged(object sender, EventArgs e)
        {
            txt_ongthongtieu_noidat.Enabled = dtp_ongthongtieu_ngaydat.Enabled = chk_ongthongtieu.Checked;
            txt_ongthongtieu_noidat.Focus();
        }

        private void chk_danluu_CheckedChanged(object sender, EventArgs e)
        {
            txt_danluu_noidat.Enabled = dtp_danluu_ngaydat.Enabled = chk_danluu.Checked;
            txt_danluu_noidat.Focus();
        }

        private void chk_lieutho_oxy_CheckedChanged(object sender, EventArgs e)
        {
            nmr_lieutho_oxy_mota.Enabled = chk_lieutho_oxy.Checked;
            nmr_lieutho_oxy_mota.Focus();
        }

        private void chk_da_vetloetdotide_CheckedChanged(object sender, EventArgs e)
        {
            txt_da_vetloetdotide_mota.Enabled = chk_da_vetloetdotide.Checked;
            txt_da_vetloetdotide_mota.Focus();
        }

        private void chk_ngaycatchi_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chk_dinhduong_chedo_an_CheckedChanged(object sender, EventArgs e)
        {
            txt_dinhduong_chedo_an_mota.Enabled = chk_dinhduong_chedo_an.Checked;
            txt_dinhduong_chedo_an_mota.Focus();
        }

        private void chk_tailieubangiao_khac_CheckedChanged(object sender, EventArgs e)
        {
            txt_tailieubangiao_mota.Enabled = chk_tailieubangiao_khac.Checked;
            txt_tailieubangiao_mota.Focus();
        }

        private void opt_thuocdadieutri_co_CheckedChanged(object sender, EventArgs e)
        {
            dtp_thuocdadieutri_luc.Enabled = opt_thuocdadieutri_co.Checked;
            dtp_thuocdadieutri_luc.Focus();
        }

        private void opt_thuoccansudungtiep_co_CheckedChanged(object sender, EventArgs e)
        {
            dtp_thuoccansudungtiep_luc.Enabled = opt_thuoccansudungtiep_co.Checked;
            dtp_thuoccansudungtiep_luc.Focus();
        }

        private void chk_da_bangvetthuong_CheckedChanged(object sender, EventArgs e)
        {
            txt_da_bangvetthuong_mota.Enabled = chk_da_bangvetthuong.Checked;
            txt_da_bangvetthuong_mota.Focus();
        }
    }
}
