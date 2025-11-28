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
    public partial class uc_pt02_BangKiemChuanBiVaBanGiaoNguoiBenhTruocPhauThuat : UserControl
    {
        public delegate void OnMsg(string msg, bool IsSucess = false);
        public event OnMsg _OnMsg;
        public delegate void OnStatus(bool isNew);
        public event OnStatus _OnStatus;
        public EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat _phieu;
        KcbLuotkham objLuotkham;
        public int id_bacsikham = -1;
        DmucNhanvien objBacsiPttt = null;
        DmucNhanvien objNguoiDaidien = null;
        public bool Force2Saved = false;
        bool isInit = false;
        public uc_pt02_BangKiemChuanBiVaBanGiaoNguoiBenhTruocPhauThuat()
        {
            InitializeComponent();
        }

        public void Init()
        {
           
            DataBinding.BindDataCombobox(cbo_nguoi_giao, globalVariables.gv_dtDmucNhanvien, DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "----Chọn----", true);
            DataBinding.BindDataCombobox(cbo_nguoi_nhan, globalVariables.gv_dtDmucNhanvien, DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "----Chọn----", true);
            DataTable dtKhoaPhong = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL", 0); 
            DataBinding.BindDataCombobox(cbo_khoa_nhan, dtKhoaPhong, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "----Chọn----", true);
            DataBinding.BindDataCombobox(cbo_khoa_giao, dtKhoaPhong, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "----Chọn----", true);
          
            isInit = true;
        }
        public void Init(KcbLuotkham objLuotkham, EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat _phieu)
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
            _phieu = new Select().From(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Schema)
                        .Where(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat>();
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
                    _phieu = new Select().From(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Schema)
                        .Where(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat>();

                txtId.Text = "";
                if (_phieu != null)
                {
                    txtId.Text = _phieu.IdPhieu.ToString();
                    txtSoHoso.Text = _phieu.MaPhieu;
                    dtp_ngayphauthuat.Value = _phieu.Ngayphauthuat.Value;
                   
                    dtp_ngaygiobangiao.Value = _phieu.NgayGiao.Value;
                    dtp_ngay_nhan.Value = _phieu.NgayNhan.Value;
                    cbo_nguoi_nhan.SelectedValue = _phieu.IdNguoiNhan;
                    cbo_nguoi_giao.SelectedValue = _phieu.IdNguoiGiao;
                    cbo_khoa_nhan.SelectedValue = _phieu.IdKhoaNhan;
                    cbo_khoa_giao.SelectedValue = _phieu.IdKhoaGiao;

                    // txt_khoa.SetId(_phieu.idkho);

                    txt_chandoan.Text = _phieu.Chandoan;
                    
                    
                    opt_tiensudiung_co.Checked = Utility.Bool2Bool(_phieu.TiensudiungCo);
                    opt_tiensudiung_khong.Checked = Utility.Bool2Bool(_phieu.TiensudiungKhong);
                    txt_chandoan.Text =Utility.sDbnull( _phieu.TiensudiungCoGhiro);
                    opt_cobenhtruyennhiem_co.Checked = Utility.Bool2Bool(_phieu.CobenhtruyennhiemCo);
                    opt_cobenhtruyennhiem_khong.Checked = Utility.Bool2Bool(_phieu.CobenhtruyennhiemKhong);
                    txt_cobenhtruyennhiem_co_ghiro.Text = Utility.sDbnull(_phieu.CobenhtruyennhiemCoGhiro);

                    opt_nguoibenhdatamruatruockhimo_co.Checked = Utility.Bool2Bool(_phieu.NguoibenhdatamruatruockhimoCo);
                    opt_nguoibenhdatamruatruockhimo_khong.Checked = Utility.Bool2Bool(_phieu.NguoibenhdatamruatruockhimoKhong);

                    chk_daxacnhandacdiemnhandangnguoibenh_lan1.Checked = Utility.Bool2Bool(_phieu.DaxacnhandacdiemnhandangnguoibenhLan1);
                    chk_daxacnhandacdiemnhandangnguoibenh_lan2.Checked = Utility.Bool2Bool(_phieu.DaxacnhandacdiemnhandangnguoibenhLan2);

                    chk_hosobenhan_lan1.Checked = Utility.Bool2Bool(_phieu.HosobenhanLan1);
                    chk_hosobenhan_lan2.Checked = Utility.Bool2Bool(_phieu.HosobenhanLan2);

                    chk_tailieuphauthuat_lan1.Checked = Utility.Bool2Bool(_phieu.TailieuphauthuatLan1);
                    chk_tailieuphauthuat_lan2.Checked = Utility.Bool2Bool(_phieu.TailieuphauthuatLan2);

                    chk_phimchupxq_lan1.Checked = Utility.Bool2Bool(_phieu.PhimchupxqLan1);
                    chk_phimchupxq_lan2.Checked = Utility.Bool2Bool(_phieu.PhimchupxqLan2);
                    nmr_phimchupxq_soluong.Value =Utility.DecimaltoDbnull( _phieu.PhimchupxqSoluong);
                    chk_phimchupxq_khongapdung.Checked = Utility.Bool2Bool(_phieu.PhimchupxqKhongapdung);

                    chk_phimchupmsct_lan1.Checked = Utility.Bool2Bool(_phieu.PhimchupmsctLan1);
                    chk_phimchupmsct_lan2.Checked = Utility.Bool2Bool(_phieu.PhimchupmsctLan2);
                    nmr_phimchupmsct_soluong.Value = Utility.DecimaltoDbnull(_phieu.PhimchupmsctSoluong);
                    chk_phimchupmsct_khongapdung.Checked = Utility.Bool2Bool(_phieu.PhimchupmsctKhongapdung);

                    chk_phimchup_mri_lan1.Checked = Utility.Bool2Bool(_phieu.PhimchupMriLan1);
                    chk_phimchup_mri_lan2.Checked = Utility.Bool2Bool(_phieu.PhimchupMriLan2);
                    nmr_phimchup_mri_soluong.Value = Utility.DecimaltoDbnull(_phieu.PhimchupMriSoluong);
                    chk_phimchup_mri_khongapdung.Checked = Utility.Bool2Bool(_phieu.PhimchupMriKhongapdung);


                    chk_khangsinhduphong_lan1.Checked = Utility.Bool2Bool(_phieu.KhangsinhduphongLan1);
                    chk_khangsinhduphong_lan2.Checked = Utility.Bool2Bool(_phieu.KhangsinhduphongLan2);
                    chk_khangsinhduphong_khongapdung.Checked = Utility.Bool2Bool(_phieu.KhangsinhduphongKhongapdung);
                    dtp_khangsinhduphong_giophut.Text =Utility.sDbnull( _phieu.KhangsinhduphongGiophut);

                    chk_nhinantugio_lan1.Checked = Utility.Bool2Bool(_phieu.NhinantugioLan1);
                    chk_nhinantugio_lan2.Checked = Utility.Bool2Bool(_phieu.NhinantugioLan2);
                    chk_nhinantugio_khongapdung.Checked = Utility.Bool2Bool(_phieu.NhinantugioKhongapdung);
                    dtp_nhinantugio__giophut.Text = Utility.sDbnull(_phieu.NhinantugioGiophut);

                    chk_chuanbivesinhvungdatruocmo_lan1.Checked = Utility.Bool2Bool(_phieu.ChuanbivesinhvungdatruocmoLan1);
                    chk_chuanbivesinhvungdatruocmo_lan2.Checked = Utility.Bool2Bool(_phieu.ChuanbivesinhvungdatruocmoLan2);
                    chk_chuanbivesinhvungdatruocmo_khongapdung.Checked = Utility.Bool2Bool(_phieu.ChuanbivesinhvungdatruocmoKhongapdung);
                    dtp_chuanbivesinhvungdatruocmo_giophut.Text = Utility.sDbnull(_phieu.ChuanbivesinhvungdatruocmoGiophut);

                    chk_daduocdanhdauvitriphauthuat_lan1.Checked = Utility.Bool2Bool(_phieu.DaduocdanhdauvitriphauthuatLan1);
                    chk_daduocdanhdauvitriphauthuat_lan2.Checked = Utility.Bool2Bool(_phieu.DaduocdanhdauvitriphauthuatLan2);
                    chk_daduocdanhdauvitriphauthuat_khongapdung.Checked = Utility.Bool2Bool(_phieu.DaduocdanhdauvitriphauthuatKhongapdung);

                    chk_dungthuoctruocmochongnon_lan1.Checked = Utility.Bool2Bool(_phieu.DungthuoctruocmochongnonLan1);
                    chk_dungthuoctruocmochongnon_lan2.Checked = Utility.Bool2Bool(_phieu.DungthuoctruocmochongnonLan2);
                    chk_dungthuoctruocmochongnon_khongapdung.Checked = Utility.Bool2Bool(_phieu.DungthuoctruocmochongnonKhongapdung);
                    dtp_dungthuoctruocmochongnon_giophut.Text = Utility.sDbnull(_phieu.DungthuoctruocmochongnonGiophut);

                    chk_dungthuoctruocmothuocdieutrikhac_lan1.Checked = Utility.Bool2Bool(_phieu.DungthuoctruocmothuocdieutrikhacLan1);
                    chk_dungthuoctruocmothuocdieutrikhac_lan2.Checked = Utility.Bool2Bool(_phieu.DungthuoctruocmothuocdieutrikhacLan2);
                    chk_dungthuoctruocmothuocdieutrikhac_khongapdung.Checked = Utility.Bool2Bool(_phieu.DungthuoctruocmothuocdieutrikhacKhongapdung);
                    dtp_dungthuoctruocmothuocdieutrikhac_giophut.Text = Utility.sDbnull(_phieu.DungthuoctruocmothuocdieutrikhacGiophut);

                    chk_dathaoranggia_lan1.Checked = Utility.Bool2Bool(_phieu.DathaoranggiaLan1);
                    chk_dathaoranggia_lan2.Checked = Utility.Bool2Bool(_phieu.DathaoranggiaLan2);
                    chk_dathaoranggia_khongapdung.Checked = Utility.Bool2Bool(_phieu.DathaoranggiaKhongapdung);

                    chk_dathaocathietbiphutro_lan1.Checked = Utility.Bool2Bool(_phieu.DathaocacthietbiphutroLan1);
                    chk_dathaocathietbiphutro_lan2.Checked = Utility.Bool2Bool(_phieu.DathaocacthietbiphutroLan2);
                    chk_dathaocathietbiphutro_khongapdung.Checked = Utility.Bool2Bool(_phieu.DathaocacthietbiphutroKhongapdung);

                    chk_dathaonutrangdokeptoc_lan1.Checked = Utility.Bool2Bool(_phieu.DathaonutrangdokeptocLan1);
                    chk_dathaonutrangdokeptoc_lan2.Checked = Utility.Bool2Bool(_phieu.DathaonutrangdokeptocLan2);
                    chk_dathaonutrangdokeptoc_khongapdung.Checked = Utility.Bool2Bool(_phieu.DathaonutrangdokeptocKhongapdung);

                    chk_damacaochoangmo_lan1.Checked = Utility.Bool2Bool(_phieu.DamacaochoangmoLan1);
                    chk_damacaochoangmo_lan2.Checked = Utility.Bool2Bool(_phieu.DamacaochoangmoLan2);
                    chk_damacaochoangmo_khongapdung.Checked = Utility.Bool2Bool(_phieu.DamacaochoangmoKhongapdung);

                    chk_dachuanbidaitrang_lan1.Checked = Utility.Bool2Bool(_phieu.DachuanbidaitrangLan1);
                    chk_dachuanbidaitrang_lan2.Checked = Utility.Bool2Bool(_phieu.DachuanbidaitrangLan2);
                    chk_dachuanbidaitrang_khongapdung.Checked = Utility.Bool2Bool(_phieu.DachuanbidaitrangKhongapdung);

                    txt_khac.Text = _phieu.Khac;

                }
                else
                {
                    ClearControl();

                }
                txtSoHoso.Text = _phieu == null || string.IsNullOrEmpty(Utility.sDbnull(_phieu.MaPhieu, "")) ? THU_VIEN_CHUNG.TT25LaySohoso(10) : Utility.sDbnull(_phieu.MaPhieu, "");
                if (_OnStatus != null) _OnStatus(_phieu == null || _phieu.IdPhieu <= 0);
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
            
            if (Utility.sDbnull(txtSoHoso.Text)=="")
            {
                Msg = "Bạn phải nhập mã phiếu";
                if (_OnMsg != null) _OnMsg(Msg);
                txtSoHoso.Focus();
                return false;
            }
            DataTable dtData = new Select().From(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Schema)
              .Where(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Columns.MaPhieu).IsEqualTo(Utility.DoTrim(txtSoHoso.Text))
              .And(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Columns.IdPhieu).IsNotEqualTo(Utility.Int64Dbnull(txtId.Text, -1))
              .ExecuteDataSet().Tables[0];
            if (dtData.Rows.Count > 0)
            {
                Msg = "Mã phiếu đã được sử dụng. Vui lòng nhập mã phiếu khác";
                txtSoHoso.Focus();
                return false;
            }
            if (dtp_ngayphauthuat.Text == "")
            {
                Msg = "Phải nhập ngày giờ dự kiến phẫu thuật";
                if (_OnMsg != null) _OnMsg(Msg, false);
                dtp_ngayphauthuat.Focus();
                return false;
            }
            if (dtp_ngaygiobangiao.Text == "")
            {
                Msg = "Phải nhập ngày giờ giao";
                if (_OnMsg != null) _OnMsg(Msg, false);
                dtp_ngaygiobangiao.Focus();
                return false;
            }
            if (dtp_ngayphauthuat.Value < dtp_ngaygiobangiao.Value)
            {
                Msg = "Ngày giờ giao phải trước ngày giờ dự kiến phẫu thuật";
                if (_OnMsg != null) _OnMsg(Msg, false);
                dtp_ngaygiobangiao.Focus();
                return false;
            }
            if (dtp_ngay_nhan.Text == "")
            {
                Msg = "Phải nhập ngày giờ nhận";
                if (_OnMsg != null) _OnMsg(Msg, false);
                dtp_ngay_nhan.Focus();
                return false;
            }
            if (dtp_ngay_nhan.Value < dtp_ngaygiobangiao.Value)
            {
                Msg = "Ngày giờ nhận phải sau ngày giờ giao";
                if (_OnMsg != null) _OnMsg(Msg, false);
                dtp_ngay_nhan.Focus();
                return false;
            }
            if (dtp_ngayphauthuat.Value < dtp_ngay_nhan.Value)
            {
                Msg = "Ngày giờ nhận phải trước ngày giờ dự kiến phẫu thuật";
                if (_OnMsg != null) _OnMsg(Msg, false);
                dtp_ngay_nhan.Focus();
                return false;
            }
            if (Utility.Int32Dbnull( cbo_nguoi_giao.SelectedValue)<=0)
            {
                Msg = "Bạn phải chọn người giao từ danh mục bác sĩ";
                if (_OnMsg != null) _OnMsg(Msg);
                cbo_nguoi_giao.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(cbo_nguoi_nhan.SelectedValue) <= 0)
            {
                Msg = "Bạn phải chọn người nhận từ danh mục bác sĩ";
                if (_OnMsg != null) _OnMsg(Msg);
                cbo_nguoi_nhan.Focus();
                return false;
            }
            if(Utility.Int32Dbnull(cbo_nguoi_nhan.SelectedValue)>0 && Utility.Int32Dbnull(cbo_nguoi_nhan.SelectedValue)== Utility.Int32Dbnull(cbo_nguoi_giao.SelectedValue))
            {
                Msg = "Người nhận phải khác người giao";
                if (_OnMsg != null) _OnMsg(Msg);
                cbo_nguoi_nhan.Focus();
                return false;
            }    
            if (Utility.Int32Dbnull(cbo_khoa_giao.SelectedValue) <= 0)
            {
                Msg = "Bạn phải chọn khoa giao từ danh mục khoa phòng";
                if (_OnMsg != null) _OnMsg(Msg);
                cbo_khoa_giao.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(cbo_khoa_nhan.SelectedValue) <= 0)
            {
                Msg = "Bạn phải chọn khoa nhận từ danh mục khoa phòng";
                if (_OnMsg != null) _OnMsg(Msg);
                cbo_khoa_nhan.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(cbo_khoa_nhan.SelectedValue) > 0 && Utility.Int32Dbnull(cbo_khoa_nhan.SelectedValue) == Utility.Int32Dbnull(cbo_khoa_giao.SelectedValue))
            {
                Msg = "Khoa nhận phải khác Khoa giao";
                if (_OnMsg != null) _OnMsg(Msg);
                cbo_khoa_nhan.Focus();
                return false;
            }
            if (opt_tiensudiung_co.Checked && Utility.sDbnull( txt_tiensudiung_co_ghiro.Text)=="")
            {
                Msg = "Bạn phải ghi rõ tiền sử dị ứng";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_tiensudiung_co_ghiro.Focus();
                return false;
            }
            if (opt_cobenhtruyennhiem_co.Checked && Utility.sDbnull(txt_cobenhtruyennhiem_co_ghiro.Text) == "")
            {
                Msg = "Bạn phải ghi rõ Bệnh truyền nhiễm nếu có";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_cobenhtruyennhiem_co_ghiro.Focus();
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
                        _phieu = new Select().From(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Schema)
                   .Where(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat>();
                       
                        if (_phieu == null || _phieu.IdPhieu <= 0)
                        {
                            isNew = true;
                            _phieu = new EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat();
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
                        _phieu.MaPhieu = txtSoHoso.Text;
                        _phieu.Ngayphauthuat = dtp_ngayphauthuat.Value;

                        _phieu.NgayGiao = dtp_ngaygiobangiao.Value;
                        _phieu.NgayNhan = dtp_ngay_nhan.Value;
                        _phieu.IdNguoiNhan = Utility.Int32Dbnull(cbo_nguoi_nhan.SelectedValue);
                        _phieu.IdNguoiGiao = Utility.Int32Dbnull(cbo_nguoi_giao.SelectedValue);
                        _phieu.IdKhoaNhan = Utility.Int32Dbnull(cbo_khoa_nhan.SelectedValue);
                        _phieu.IdKhoaGiao = Utility.Int32Dbnull(cbo_khoa_giao.SelectedValue);

                        // _phieu.idkho = txt_khoa.GetId();

                        _phieu.Chandoan = txt_chandoan.Text;

                        _phieu.TiensudiungCo = opt_tiensudiung_co.Checked;
                        _phieu.TiensudiungKhong = opt_tiensudiung_khong.Checked;
                        _phieu.TiensudiungCoGhiro = Utility.sDbnull(txt_chandoan.Text);
                        _phieu.CobenhtruyennhiemCo = opt_cobenhtruyennhiem_co.Checked;
                        _phieu.CobenhtruyennhiemKhong = opt_cobenhtruyennhiem_khong.Checked;
                        _phieu.CobenhtruyennhiemCoGhiro = Utility.sDbnull(txt_cobenhtruyennhiem_co_ghiro.Text);

                        _phieu.NguoibenhdatamruatruockhimoCo = opt_nguoibenhdatamruatruockhimo_co.Checked;
                        _phieu.NguoibenhdatamruatruockhimoKhong = opt_nguoibenhdatamruatruockhimo_khong.Checked;

                        _phieu.DaxacnhandacdiemnhandangnguoibenhLan1 = chk_daxacnhandacdiemnhandangnguoibenh_lan1.Checked;
                        _phieu.DaxacnhandacdiemnhandangnguoibenhLan2 = chk_daxacnhandacdiemnhandangnguoibenh_lan2.Checked;

                        _phieu.HosobenhanLan1 = chk_hosobenhan_lan1.Checked;
                        _phieu.HosobenhanLan2 = chk_hosobenhan_lan2.Checked;

                        _phieu.TailieuphauthuatLan1 = chk_tailieuphauthuat_lan1.Checked;
                        _phieu.TailieuphauthuatLan2 = chk_tailieuphauthuat_lan2.Checked;

                        _phieu.PhimchupxqLan1 = chk_phimchupxq_lan1.Checked;
                        _phieu.PhimchupxqLan2 = chk_phimchupxq_lan2.Checked;
                        _phieu.PhimchupxqSoluong = Utility.ByteDbnull(nmr_phimchupxq_soluong.Value);
                        _phieu.PhimchupxqKhongapdung = chk_phimchupxq_khongapdung.Checked;

                        _phieu.PhimchupmsctLan1 = chk_phimchupmsct_lan1.Checked;
                        _phieu.PhimchupmsctLan2 = chk_phimchupmsct_lan2.Checked;
                        _phieu.PhimchupmsctSoluong = Utility.ByteDbnull(nmr_phimchupmsct_soluong.Value);
                        _phieu.PhimchupmsctKhongapdung = chk_phimchupmsct_khongapdung.Checked;

                        _phieu.PhimchupMriLan1 = chk_phimchup_mri_lan1.Checked;
                        _phieu.PhimchupMriLan2 = chk_phimchup_mri_lan2.Checked;
                        _phieu.PhimchupMriSoluong = Utility.ByteDbnull(nmr_phimchup_mri_soluong.Value);
                        _phieu.PhimchupMriKhongapdung = chk_phimchup_mri_khongapdung.Checked;

                        _phieu.KhangsinhduphongLan1 = chk_khangsinhduphong_lan1.Checked;
                        _phieu.KhangsinhduphongLan2 = chk_khangsinhduphong_lan2.Checked;
                        _phieu.KhangsinhduphongKhongapdung = chk_khangsinhduphong_khongapdung.Checked;
                        _phieu.KhangsinhduphongGiophut = Utility.sDbnull(dtp_khangsinhduphong_giophut.Text);

                        _phieu.NhinantugioLan1 = chk_nhinantugio_lan1.Checked;
                        _phieu.NhinantugioLan2 = chk_nhinantugio_lan2.Checked;
                        _phieu.NhinantugioKhongapdung = chk_nhinantugio_khongapdung.Checked;
                        _phieu.NhinantugioGiophut = Utility.sDbnull(dtp_nhinantugio__giophut.Text);

                        _phieu.ChuanbivesinhvungdatruocmoLan1 = chk_chuanbivesinhvungdatruocmo_lan1.Checked;
                        _phieu.ChuanbivesinhvungdatruocmoLan2 = chk_chuanbivesinhvungdatruocmo_lan2.Checked;
                        _phieu.ChuanbivesinhvungdatruocmoKhongapdung = chk_chuanbivesinhvungdatruocmo_khongapdung.Checked;
                        _phieu.ChuanbivesinhvungdatruocmoGiophut = Utility.sDbnull(dtp_chuanbivesinhvungdatruocmo_giophut.Text);

                        _phieu.DaduocdanhdauvitriphauthuatLan1 = chk_daduocdanhdauvitriphauthuat_lan1.Checked;
                        _phieu.DaduocdanhdauvitriphauthuatLan2 = chk_daduocdanhdauvitriphauthuat_lan2.Checked;
                        _phieu.DaduocdanhdauvitriphauthuatKhongapdung = chk_daduocdanhdauvitriphauthuat_khongapdung.Checked;

                        _phieu.DungthuoctruocmochongnonLan1 = chk_dungthuoctruocmochongnon_lan1.Checked;
                        _phieu.DungthuoctruocmochongnonLan2 = chk_dungthuoctruocmochongnon_lan2.Checked;
                        _phieu.DungthuoctruocmochongnonKhongapdung = chk_dungthuoctruocmochongnon_khongapdung.Checked;
                        _phieu.DungthuoctruocmochongnonGiophut = Utility.sDbnull(dtp_dungthuoctruocmochongnon_giophut.Text);

                        _phieu.DungthuoctruocmothuocdieutrikhacLan1 = chk_dungthuoctruocmothuocdieutrikhac_lan1.Checked;
                        _phieu.DungthuoctruocmothuocdieutrikhacLan2 = chk_dungthuoctruocmothuocdieutrikhac_lan2.Checked;
                        _phieu.DungthuoctruocmothuocdieutrikhacKhongapdung = chk_dungthuoctruocmothuocdieutrikhac_khongapdung.Checked;
                        _phieu.DungthuoctruocmothuocdieutrikhacGiophut = Utility.sDbnull(dtp_dungthuoctruocmothuocdieutrikhac_giophut.Text);

                        _phieu.DathaoranggiaLan1 = chk_dathaoranggia_lan1.Checked;
                        _phieu.DathaoranggiaLan2 = chk_dathaoranggia_lan2.Checked;
                        _phieu.DathaoranggiaKhongapdung = chk_dathaoranggia_khongapdung.Checked;

                        _phieu.DathaocacthietbiphutroLan1 = chk_dathaocathietbiphutro_lan1.Checked;
                        _phieu.DathaocacthietbiphutroLan2 = chk_dathaocathietbiphutro_lan2.Checked;
                        _phieu.DathaocacthietbiphutroKhongapdung = chk_dathaocathietbiphutro_khongapdung.Checked;

                        _phieu.DathaonutrangdokeptocLan1 = chk_dathaonutrangdokeptoc_lan1.Checked;
                        _phieu.DathaonutrangdokeptocLan2 = chk_dathaonutrangdokeptoc_lan2.Checked;
                        _phieu.DathaonutrangdokeptocKhongapdung = chk_dathaonutrangdokeptoc_khongapdung.Checked;

                        _phieu.DamacaochoangmoLan1 = chk_damacaochoangmo_lan1.Checked;
                        _phieu.DamacaochoangmoLan2 = chk_damacaochoangmo_lan2.Checked;
                        _phieu.DamacaochoangmoKhongapdung = chk_damacaochoangmo_khongapdung.Checked;

                        _phieu.DachuanbidaitrangLan1 = chk_dachuanbidaitrang_lan1.Checked;
                        _phieu.DachuanbidaitrangLan2 = chk_dachuanbidaitrang_lan2.Checked;
                        _phieu.DachuanbidaitrangKhongapdung = chk_dachuanbidaitrang_khongapdung.Checked;

                        _phieu.Khac = txt_khac.Text;



                        _phieu.Save();
                        if (objBacsiPttt == null)
                            objBacsiPttt = DmucNhanvien.FetchByID(Utility.Int32Dbnull(cbo_nguoi_giao.SelectedValue));
                        emrdoc.Force2Saved = Force2Saved;
                        emrdoc.InitDocument(Utility.Int64Dbnull(_phieu.IdBenhnhan), _phieu.MaLuotkham, Utility.Int64Dbnull(_phieu.IdPhieu), _phieu.NgayGiao.Value, Loaiphieu_HIS.BANGKIEM_CHUANBI_VA_BANGIAO_NGUOIBENH_TRUOCPHAUTHUAT, "BANGKIEM_CHUANBI_VA_BANGIAO_NGUOIBENH_TRUOCPHAUTHUAT", _phieu.NguoiTao,Utility.Int16Dbnull( objBacsiPttt.IdKhoa), Utility.Int16Dbnull(objBacsiPttt.IdPhong), Utility.Byte2Bool(0),"");
                        emrdoc.Save();

                    }
                    scope.Complete();
                }
                txtId.Text = _phieu.IdPhieu.ToString();
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
                _phieu = new Select().From(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Schema)
                       .Where(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                       .And(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                       .ExecuteSingle<EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat>();
                if (_phieu.IdPhieu <= 0)
                {
                    Utility.ShowMsg("Bạn cần lưu thông tin Biên bản hội chẩn thông qua mổ trước khi thực hiện in phiếu");
                    return;
                }
                DataTable dtData = SPs.EmrPt02BangkiemchuanbivabangiaonguoibenhtruocphauthuatLaythongtinIn(_phieu.IdPhieu).GetDataSet().Tables[0];
                dtData.TableName = "BIENBANHOICHAN_THONGQUAMO";
                dtData.Rows[0]["sngayphauthuat"] = _phieu != null ? Utility.FormatDateTime_gio_ngay_thang_nam(_phieu.Ngayphauthuat, "") : "....... giờ.......ngày................./............../20..............";
                dtData.Rows[0]["sngay_giao"] = _phieu != null ? Utility.FormatDateTime_gio_ngay_thang_nam(_phieu.NgayGiao, "") : "........giờ...........phút, ngày........./........./20.........";
                dtData.Rows[0]["sngay_nhan"] = _phieu != null ? Utility.FormatDateTime_gio_ngay_thang_nam(_phieu.NgayNhan, "") : "....... giờ.......ngày................./............../20..............";
                dtData.Rows[0]["khangsinhduphong_giophut"] = _phieu != null ? Utility.GioPhut(Utility.sDbnull(_phieu.KhangsinhduphongGiophut)): "..........giờ..........phút";
                dtData.Rows[0]["chuanbivesinhvungdatruocmo_giophut"] = _phieu != null ? Utility.GioPhut(Utility.sDbnull(_phieu.ChuanbivesinhvungdatruocmoGiophut)) : "..........giờ..........phút";
                dtData.Rows[0]["dungthuoctruocmochongnon_giophut"] = _phieu != null ? Utility.GioPhut(Utility.sDbnull(_phieu.DungthuoctruocmochongnonGiophut)) : "..........giờ..........phút";
                dtData.Rows[0]["dungthuoctruocmothuocdieutrikhac_giophut"] = _phieu != null ? Utility.GioPhut(Utility.sDbnull(_phieu.DungthuoctruocmothuocdieutrikhacGiophut)) : "..........giờ..........phút";
                dtData.Rows[0]["nhinantugio__giophut"] = _phieu != null ? Utility.GioPhut(Utility.sDbnull(_phieu.NhinantugioGiophut)) : "..........giờ..........phút";
                WordPrinter.InPhieu(dtData, "BANGKIEM_CHUANBI_VA_BANGIAO_NGUOIBENH_TRUOCPHAUTHUAT.doc", "",false, @"\MergeFields\BANGKIEM_CHUANBI_VA_BANGIAO_NGUOIBENH_TRUOCPHAUTHUAT_CHECKED_FIELDS.txt");


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
            objBacsiPttt = DmucNhanvien.FetchByID(Utility.Int32Dbnull(cbo_nguoi_giao.SelectedValue));
        }
    }
}
