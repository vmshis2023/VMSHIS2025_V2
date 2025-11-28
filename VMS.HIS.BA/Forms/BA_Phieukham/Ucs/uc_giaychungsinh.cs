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
    public partial class uc_giaychungsinh : UserControl
    {
        public delegate void OnMsg(string msg, bool IsSucess = false);
        public event OnMsg _OnMsg;
        public delegate void OnStatus(bool isNew);
        public event OnStatus _OnStatus;
        public EmrGiayChungsinh _phieu;
        KcbLuotkham objLuotkham;
        public action m_enAct = action.FirstOrFinished;
        public int id_bacsikham = -1;
        DmucNhanvien objNguoiDoDe = null;
        DmucNhanvien objNguoiDaidien = null;
        public bool Force2Saved = false;
        public bool isAllowSelectionChanged = true;
        bool DaKhoiTaoDanhMuc = false;
        public uc_giaychungsinh()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            txt_nguoi_dode._OnEnterMe += TxtBSDieuTri__OnEnterMe;
            txt_daidien_donvi._OnEnterMe += TxtDaidienDonvi__OnEnterMe;
            grdList.SelectionChanged += GrdList_SelectionChanged;
            grdList.MouseDoubleClick += GrdList_MouseDoubleClick;
            grdList.ColumnButtonClick += GrdList_ColumnButtonClick;
            txt_tinhtrang_be._OnShowDataV1 += _OnShowDataV1;
            txt_quocgia._OnShowDataV1 += _OnShowDataV1;
            txt_quocgia._OnEnterMe += Txt_quocgia__OnEnterMe;
        }

        private void GrdList_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            BeginUpdate();
            isAllowSelectionChanged = false;
        }

        private void Txt_quocgia__OnEnterMe()
        {
            if (Utility.sDbnull(txt_quocgia.Text) != "")
                chk_ngoaikieu.Checked = txt_quocgia.myCode != "VN";
        }

        private void _OnShowDataV1(AutoCompleteTextbox_Danhmucchung obj)
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(obj.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = obj.myCode;
                obj.Init();
                obj.SetCode(oldCode);
                obj.Focus();
            }
        }

        private void GrdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BeginUpdate();
            isAllowSelectionChanged = false;
        }
       void BeginUpdate()
        {
            try
            {
                if (!isAllowSelectionChanged || !Utility.isValidGrid(grdList))
                {
                    _phieu = null;
                    grb_thongtin_me.Enabled = grb_thongtin_bo.Enabled = grb_thongtin_con.Enabled = false;
                    ClearControl(this);
                    return;
                }
                long id = Utility.Int64Dbnull(grdList.GetValue("id"));
                _phieu = new Select().From(EmrGiayChungsinh.Schema)
                            .Where(EmrGiayChungsinh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                            .And(EmrGiayChungsinh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                            .And(EmrGiayChungsinh.Columns.Id).IsEqualTo(id)
                            .ExecuteSingle<EmrGiayChungsinh>();
                m_enAct = action.Update;
                cmdthemmoi.Enabled = cmdxoa.Enabled = grdList.Enabled = false;
                grb_thongtin_me.Enabled = grb_thongtin_bo.Enabled = grb_thongtin_con.Enabled = true;
                cmdGhi.Enabled = cmdHuy.Enabled = cmdIn.Enabled = true;
                FillData4Update();
            }
            catch (Exception ex)
            {
               
            }
            
        }
        private void GrdList_SelectionChanged(object sender, EventArgs e)
        {
         
        }
        DataTable m_dtData = new DataTable();
        public void Init(KcbLuotkham objLuotkham, EmrGiayChungsinh _phieu)
        {
            isAllowSelectionChanged = false;
            dtp_ngaycap_giaychungsinh.Value = globalVariables.SysDate;
            this.objLuotkham = objLuotkham;
            this._phieu = _phieu;
            InitCommonData();
            
            DateTime dtNgay = new DateTime(1900, 1, 1);
             m_dtData = SPs.EmrGiayChungsinhLaydanhsach(-1, dtNgay, dtNgay, "", 100, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, "", "", "", 100).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, m_dtData, true, true, "1=1", "id,maso_giaychungsinh");
            isAllowSelectionChanged = true;
            ModifyCommandButtons();
            if(_phieu!=null)//Nhảy đến và đưa về trạng thái Update
            {

            }    

        }
        /// <summary>
        /// Khởi tạo các danh mục 
        /// </summary>
       void InitCommonData()
        {
            if (DaKhoiTaoDanhMuc) return;
            txt_tinhtrang_be.Init();
            txt_quocgia.Init();
            txt_nguoi_dode.Init(globalVariables.gv_dtDmucNhanvien,
                                            new List<string>
                                 {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                 });
            txt_daidien_donvi.Init(txt_nguoi_dode.AutoCompleteSource, txt_nguoi_dode.defaultItem);
            VMS.HIS.Danhmuc.Util.SetNguoiDaiDienDonVi(txt_daidien_donvi);
            DaKhoiTaoDanhMuc = true;
        }
        private void TxtDaidienDonvi__OnEnterMe()
        {
            objNguoiDaidien = DmucNhanvien.FetchByID(Utility.Int32Dbnull(txt_daidien_donvi.MyID));
        }

        private void TxtBSDieuTri__OnEnterMe()
        {
            objNguoiDoDe = DmucNhanvien.FetchByID(Utility.Int32Dbnull( txt_nguoi_dode.MyID));
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
            dtp_ngaycap_giaychungsinh.Value = globalVariables.SysDate;
            txt_nguoi_dode.Init(globalVariables.gv_dtDmucNhanvien,
                                           new List<string>
                                {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                });
            txt_daidien_donvi.Init(txt_nguoi_dode.AutoCompleteSource, txt_nguoi_dode.defaultItem);
            VMS.HIS.Danhmuc.Util.SetNguoiDaiDienDonVi(txt_daidien_donvi);
            
          
        }
        void ModifyCommandButtons()
        {
            bool isValid = objLuotkham != null;
            bool isValid2 = Utility.isValidGrid(grdList);
            cmdSua.Enabled =_phieu!=null && isValid && isValid2 && m_enAct==action.FirstOrFinished;
            cmdxoa.Enabled = _phieu != null && isValid && isValid2 && m_enAct == action.FirstOrFinished;
            cmdIn.Enabled =  isValid && isValid2;
            cmdthemmoi.Enabled = grdList.Enabled = isValid && m_enAct == action.FirstOrFinished;
            cmdGhi.Enabled = m_enAct != action.FirstOrFinished;
            cmdHuy.Enabled = cmdGhi.Enabled;
            cmd_duplicate.Enabled= isValid && m_enAct == action.FirstOrFinished;
        }
        public void FillData4Update()
        {
            try
            {

                if (_phieu == null)
                {
                    ClearControl(this);
                   
                    return;
                }
                
                txtId.Text = "";
                if (_phieu != null)
                {
                    txtId.Text = _phieu.Id.ToString();
                    opt_cap_landau.Checked = Utility.Bool2Bool(_phieu.CapLandau);
                    opt_cap_lai.Checked = Utility.Bool2Bool(_phieu.CapLai);
                    txt_ma_sobhxh_me.Text = Utility.sDbnull(_phieu.MaBhxhMe);
                    txt_noicap_bhxh_me.Text = Utility.sDbnull(_phieu.NoicapBhxhMe);
                    if (_phieu.NgaycapBhxhMe.HasValue)
                        dtp_ngaycap_bhxh_me.Value = _phieu.NgaycapBhxhMe.Value;
                    else
                        dtp_ngaysinh_bo.ResetText();
                    txt_solan_sinh.Text = Utility.sDbnull(_phieu.SolanSinh);
                    txt_so_con_sinhlannay.Text = Utility.sDbnull(_phieu.SoConSinhlannay);
                    txt_so_con_consong.Text = Utility.sDbnull(_phieu.SoConConsong);
                    chk_sinhcon_phauthuat.Checked = Utility.Bool2Bool(_phieu.SinhconDuoi32tuan);
                    chk_sinhcon_phauthuat.Checked = Utility.Bool2Bool(_phieu.SinhconPhauthuat);
                   
                    txt_hoten_bo.Text = Utility.sDbnull(_phieu.HotenBo);
                    if (_phieu.NgaysinhBo.HasValue)
                        dtp_ngaysinh_bo.Value = _phieu.NgaysinhBo.Value;
                    else
                        dtp_ngaysinh_bo.ResetText();
                    txt_nghenghiep_bo._Text = Utility.sDbnull(_phieu.NghenghiepBo);
                    txt_quocgia._Text= Utility.sDbnull(_phieu.TenQuocgia);
                    chk_ngoaikieu.Checked = Utility.Bool2Bool(_phieu.NgoaiKieu);

                    txt_hoten_be.Text = Utility.sDbnull(_phieu.HotenBe);
                    if (_phieu.NgaycapGiaychungsinh.HasValue)
                        dtp_ngaycap_giaychungsinh.Value = _phieu.NgaycapGiaychungsinh.Value;
                    else
                        dtp_ngaycap_giaychungsinh.Value = globalVariables.SysDate;
                    if (_phieu.NgaysinhBe.HasValue)
                        dtp_ngaysinh_be.Value = _phieu.NgaysinhBe.Value;
                    else
                        dtp_ngaysinh_be.Value = globalVariables.SysDate;

                    cbo_gioitinh.SelectedIndex = Utility.Int32Dbnull(_phieu.IdGioitinh);
                    txt_ma_thetam.Text = Utility.sDbnull(_phieu.MaThetam);
                    txt_ma_dinhdanh_be.Text = Utility.sDbnull(_phieu.MaDinhdanhBe);
                    txt_maso_giaychungsinh.Text = Utility.sDbnull(_phieu.MasoGiaychungsinh);
                    nmr_cannang.Value = Utility.DecimaltoDbnull(_phieu.CanNang);
                    nmr_chieudai.Value = Utility.DecimaltoDbnull(_phieu.ChieuDai);
                    nmr_vongdau.Value = Utility.DecimaltoDbnull(_phieu.VongDau);
                    txt_ghichu.Text = Utility.sDbnull(_phieu.GhiChu);
                    txt_noisinh.Text = Utility.sDbnull(_phieu.NoisinhBe);
                    txt_nguoi_dode.SetId(_phieu.IdNguoiDode);
                    txt_nguoi_dode.RaiseEnterEvents();
                    txt_daidien_donvi.SetId(_phieu.IdNguoiDaidien);
                    txt_daidien_donvi.RaiseEnterEvents();
                    txt_ma_sobhxh_me.Focus();
                }
                else
                {
                    ClearControl(this);
                }
               
                if (_OnStatus != null) _OnStatus(_phieu == null || _phieu.Id <= 0);
            }
            catch (System.Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
       void ClearControl(Control parentCtrl)
        {
            foreach (Control ctr in parentCtrl.Controls)
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
                else if (ctr is DateTimePicker)
                {
                    ((DateTimePicker)(ctr)).Value = globalVariables.SysDate;
                }
                else if (ctr is Janus.Windows.CalendarCombo.CalendarCombo)
                {
                    if (((Janus.Windows.CalendarCombo.CalendarCombo)(ctr)).IsNullDate)
                        ((Janus.Windows.CalendarCombo.CalendarCombo)(ctr)).ResetText();
                }
                if (ctr.Controls.Count > 0) 
                    ClearControl(ctr);
            }
            opt_cap_landau.Checked = true;
            opt_cap_lai.Checked = false;
            //dtp_ngaycap_bhxh_me.ResetText();
            //dtp_ngaycap_giaychungsinh.ResetText();
            //dtp_ngaysinh_be.ResetText();
            //dtp_ngaysinh_bo.ResetText();
        }
        string Msg = "";
        bool isValidData()
        {
            Msg = "";
            if(Utility.sDbnull( txt_solan_sinh.Text)=="" || Utility.DecimaltoDbnull(txt_solan_sinh.Text) <=0)
            {
                Msg = "Số lần sinh phải >=1";
                if (_OnMsg != null) _OnMsg(Msg, false);
                txt_solan_sinh.Focus();
                return false;
            }
            if (Utility.sDbnull(txt_so_con_sinhlannay.Text) == "" || Utility.DecimaltoDbnull(txt_so_con_sinhlannay.Text) <= 0)
            {
                Msg = "Số con sinh lần này phải >=1";
                if (_OnMsg != null) _OnMsg(Msg, false);
                txt_so_con_sinhlannay.Focus();
                return false;
            }
            if (Utility.sDbnull(txt_hoten_bo.Text) == "" )
            {
                Msg = "Phải nhập họ tên bố";
                if (_OnMsg != null) _OnMsg(Msg, false);
                txt_hoten_bo.Focus();
                return false;
            }
            if (dtp_ngaysinh_bo.Text =="")
            {
                Msg = "Phải nhập ngày sinh bố";
                if (_OnMsg != null) _OnMsg(Msg, false);
                dtp_ngaysinh_bo.Focus();
                return false;
            }
            if (dtp_ngaycap_giaychungsinh.Text == "")
            {
                Msg = "Phải nhập ngày cấp Giấy chứng sinh";
                if (_OnMsg != null) _OnMsg(Msg, false);
                dtp_ngaycap_giaychungsinh.Focus();
                return false;
            }
            if (dtp_ngaysinh_bo.Value.Date> dtp_ngaycap_giaychungsinh.Value.Date)
            {
                Msg = "Ngày sinh của bố phải trước ngày cấp giấy chứng sinh của bé. Vui lòng kiểm tra lại";
                if (_OnMsg != null) _OnMsg(Msg,false);
                dtp_ngaycap_giaychungsinh.Focus();
                return false;
            }
            if (Utility.sDbnull(txt_maso_giaychungsinh.Text) == "")
            {
                Msg = "Bạn phải nhập mã số Giấy chứng sinh";
                if (_OnMsg != null) _OnMsg(Msg, false);
                txt_maso_giaychungsinh.Focus();
                return false;
            }
            if (Utility.sDbnull(txt_hoten_be.Text) == "")
            {
                Msg = "Phải nhập họ tên bé";
                if (_OnMsg != null) _OnMsg(Msg, false);
                txt_hoten_be.Focus();
                return false;
            }
            if (cbo_gioitinh.SelectedIndex==-1)
            {
                Msg = "Phải nhập giới tính bé";
                if (_OnMsg != null) _OnMsg(Msg, false);
                cbo_gioitinh.Focus();
                return false;
            }
            if (dtp_ngaysinh_be.Text == "")
            {
                Msg = "Phải nhập ngày sinh của bé";
                if (_OnMsg != null) _OnMsg(Msg, false);
                dtp_ngaysinh_be.Focus();
                return false;
            }
            if (dtp_ngaysinh_be.Value.Date > dtp_ngaycap_giaychungsinh.Value.Date)
            {
                Msg = "Ngày sinh của bé phải <= ngày cấp giấy chứng sinh";
                if (_OnMsg != null) _OnMsg(Msg, false);
                dtp_ngaysinh_be.Focus();
                return false;
            }
            if (dtp_ngaysinh_be.Value.Date < dtp_ngaysinh_bo.Value.Date)
            {
                Msg = "Ngày sinh của bé phải sau ngày sinh của bố";
                if (_OnMsg != null) _OnMsg(Msg, false);
                dtp_ngaysinh_be.Focus();
                return false;
            }
            if (Utility.sDbnull( txt_tinhtrang_be.Text)=="")
            {
                Msg = "Bạn phải nhập tình trạng của bé theo danh mục";
                if (_OnMsg != null) _OnMsg(Msg, false);
                txt_tinhtrang_be.Focus();
                return false;
            }
            if (txt_tinhtrang_be.MyCode=="-1")
            {
                Msg = "Phải nhập tình trạng của bé theo danh mục thay vì nhập tự do";
                if (_OnMsg != null) _OnMsg(Msg, false);
                txt_tinhtrang_be.Focus();
                txt_tinhtrang_be.SelectAll();
                return false;
            }    
            if(nmr_cannang.Value<=0)
            {
                Msg = "Cân nặng của bé phải >0 gram";
                if (_OnMsg != null) _OnMsg(Msg, false);
                nmr_cannang.Focus();
                return false;
            }
            if (Utility.sDbnull(txt_noisinh.Text) == "")
            {
                Msg = "Phải nhập nơi sinh(thông tin địa điểm sinh) của bé";
                if (_OnMsg != null) _OnMsg(Msg, false);
                txt_noisinh.Focus();
                return false;
            }
            if(txt_nguoi_dode.MyID=="-1")
            {
                Msg = "Phải nhập người đỡ đẻ cho bé";
                if (_OnMsg != null) _OnMsg(Msg, false);
                txt_nguoi_dode.Focus();
                return false;
            }
            if (txt_daidien_donvi.MyID == "-1")
            {
                Msg = "Phải nhập đại diện đơn vị nơi sinh bé";
                if (_OnMsg != null) _OnMsg(Msg, false);
                txt_daidien_donvi.Focus();
                return false;
            }
            DataTable dtData = new Select().From(EmrGiayChungsinh.Schema)
                .Where(EmrGiayChungsinh.Columns.MasoGiaychungsinh).IsEqualTo(Utility.DoTrim(txt_maso_giaychungsinh.Text))
                .And(EmrGiayChungsinh.Columns.Id).IsNotEqualTo(Utility.Int64Dbnull(txtId.Text, -1))
                .ExecuteDataSet().Tables[0];
            if (dtData.Rows.Count > 0)
            {
                Msg = "Mã số Giấy chứng sinh đã được sử dụng cho bé khác. Vui lòng nhấn Refresh lại dữ liệu để lấy số GCS mới";
                if (_OnMsg != null) _OnMsg(Msg, false);
                txt_maso_giaychungsinh.Focus();
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
                DateTime? dtp = null;
                if (!isValidData()) return false;
                Msg = "";
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        if (_phieu == null || _phieu.Id <= 0)
                        {
                            isNew = true;
                            _phieu = new EmrGiayChungsinh();
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
                        _phieu.CapLandau = opt_cap_landau.Checked;
                        _phieu.CapLai = opt_cap_lai.Checked;
                        _phieu.MaLuotkham = objLuotkham.MaLuotkham;
                       
                        _phieu.MaBhxhMe = Utility.sDbnull(txt_ma_sobhxh_me.Text);
                        _phieu.NoicapBhxhMe = Utility.sDbnull(txt_noicap_bhxh_me.Text);
                        _phieu.NgaycapBhxhMe = dtp_ngaycap_bhxh_me.Value == null ? dtp : dtp_ngaycap_bhxh_me.Value;

                        _phieu.SolanSinh = Utility.ByteDbnull(txt_solan_sinh.Text);
                        _phieu.SoConSinhlannay = Utility.ByteDbnull(txt_so_con_sinhlannay.Text);
                        _phieu.SoConConsong = Utility.ByteDbnull(txt_so_con_consong.Text);
                        _phieu.SinhconPhauthuat = chk_sinhcon_phauthuat.Checked;
                        _phieu.SinhconDuoi32tuan = chk_sinhcon_duoi32tuan.Checked;

                        _phieu.HotenBo = Utility.sDbnull(txt_hoten_bo.Text);
                        _phieu.NgaysinhBo = dtp_ngaysinh_bo.Value;
                        _phieu.NghenghiepBo = Utility.sDbnull(txt_nghenghiep_bo.Text);
                        _phieu.MaQuocgia = Utility.sDbnull(txt_quocgia.myCode);
                        _phieu.TenQuocgia = Utility.sDbnull(txt_quocgia.Text);
                        _phieu.NgoaiKieu = chk_ngoaikieu.Checked;

                        _phieu.NgaycapGiaychungsinh = dtp_ngaycap_giaychungsinh.Value;
                        _phieu.NgaysinhBe = dtp_ngaysinh_be.Value;
                        _phieu.MasoGiaychungsinh = Utility.sDbnull(txt_maso_giaychungsinh.Text);
                        _phieu.MaThetam = Utility.sDbnull(txt_ma_thetam.Text);
                        _phieu.MaDinhdanhBe = Utility.sDbnull(txt_ma_dinhdanh_be.Text);
                        _phieu.HotenBe = Utility.sDbnull(txt_hoten_be.Text);
                        _phieu.IdGioitinh = Utility.ByteDbnull(cbo_gioitinh.SelectedIndex);
                        _phieu.GioiTinh =cbo_gioitinh.Text;
                        _phieu.MaTinhtrangBe = Utility.sDbnull(txt_tinhtrang_be.MyCode);
                        _phieu.TenTinhtrangBe = Utility.sDbnull(txt_tinhtrang_be.Text);
                        _phieu.CanNang = Utility.Int16Dbnull(nmr_cannang.Value);
                        _phieu.ChieuDai = Utility.Int16Dbnull(nmr_chieudai.Value);
                        _phieu.VongDau = Utility.Int16Dbnull(nmr_vongdau.Value);
                        _phieu.NoisinhBe = Utility.sDbnull(txt_noisinh.Text, "");
                        _phieu.GhiChu = Utility.sDbnull(txt_ghichu.Text, "");
                       
                        if (objNguoiDoDe != null)
                        {
                            _phieu.IdNguoiDode = objNguoiDoDe.IdNhanvien;
                            _phieu.MaNguoiDode = objNguoiDoDe.MaNhanvien;
                        }
                        if (objNguoiDaidien != null)
                        {
                            _phieu.IdNguoiDaidien = objNguoiDaidien.IdNhanvien;
                            _phieu.MaNguoiDaidien = objNguoiDaidien.MaNhanvien;
                        }
                        _phieu.Save();
                        emrdoc.Force2Saved = Force2Saved;
                        emrdoc.InitDocument(_phieu.IdBenhnhan.Value, objLuotkham.MaLuotkham, Utility.Int64Dbnull(_phieu.Id), _phieu.NgaycapGiaychungsinh.Value, Loaiphieu_HIS.GIAY_CHUNGSINH, "GIAY_CHUNGSINH", _phieu.NguoiTao,Utility.Int16Dbnull( objNguoiDoDe.IdKhoa), Utility.Int16Dbnull(-1), Utility.Byte2Bool(0),"");
                        emrdoc.Save();
                    }
                    scope.Complete();
                }
                txtId.Text = _phieu.Id.ToString();
                if (_OnStatus != null) _OnStatus(isNew);
                OnChangedData(_phieu.Id, m_enAct);
                Msg = "Lưu thông tin thành công";
                if (_OnMsg != null) _OnMsg(Msg,true);
                isAllowSelectionChanged = true;
                cmdHuy.PerformClick();
                return true;
            }
            catch (System.Exception ex)
            {
                if (_OnMsg != null) _OnMsg(ex.Message,false);
                Utility.CatchException(ex);
                return false;
            }
            finally
            {
                ModifyCommandButtons();

            }
        }

        void OnChangedData(long id, action m_enAct)
        {
            try
            {
                DataTable dt_temp = SPs.EmrGiayChungsinhLaydanhsach(id, new DateTime(1900, 1, 1), new DateTime(1900, 1, 1), "", 100, -1, "", "", "", "", 100).GetDataSet().Tables[0];
                if (m_enAct == action.Delete)
                {
                    if (DeleteMe())
                    {
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", EmrGiayChungsinh.Columns.Id, grdList.GetValue(EmrGiayChungsinh.Columns.Id)));
                        if (arrDr.Length > 0)
                            m_dtData.Rows.Remove(arrDr[0]);
                        m_dtData.AcceptChanges();
                    }
                }
                if (m_enAct == action.Insert && m_dtData != null && m_dtData.Columns.Count > 0 && dt_temp.Rows.Count > 0)
                {
                    m_dtData.ImportRow(dt_temp.Rows[0]);
                    return;
                }
                if (m_enAct == action.Update && m_dtData != null && m_dtData.Columns.Count > 0 && dt_temp.Rows.Count > 0)
                {
                    DataRow[] arrDr = m_dtData.Select("id=" + id);
                    if (arrDr.Length > 0)
                    {
                        foreach (DataColumn col in m_dtData.Columns)
                        {
                            arrDr[0][col.ColumnName] = dt_temp.Rows[0][col.ColumnName];
                        }

                    }
                    else
                        m_dtData.ImportRow(dt_temp.Rows[0]);

                }
                m_dtData.AcceptChanges();
                Utility.GotoNewRowJanus(grdList, EmrGiayChungsinh.Columns.Id, id.ToString());
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommandButtons();
            }
        }
        bool DeleteMe()
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        long IdPhieu = Utility.Int32Dbnull(grdList.GetValue(EmrGiayChungsinh.Columns.Id), -1);
                        new Delete().From(EmrGiayChungsinh.Schema).Where(EmrGiayChungsinh.Columns.Id).IsEqualTo(IdPhieu).Execute();
                        emrdoc.DeleteDocument(IdPhieu, Loaiphieu_HIS.GIAY_CHUNGSINH, Loaiphieu_HIS.GIAY_CHUNGSINH);
                        _phieu = null;
                        ClearControl(this);
                    }
                    scope.Complete();


                    return true;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                ModifyCommandButtons();
            }
        }

        public void Print()
        {
            try
            {
                long IdPhieu = Utility.Int32Dbnull(grdList.GetValue(EmrGiayChungsinh.Columns.Id), -1);
                _phieu = new Select().From(EmrGiayChungsinh.Schema)
                       .Where(EmrGiayChungsinh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                       .And(EmrGiayChungsinh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                       .And(EmrGiayChungsinh.Columns.Id).IsEqualTo(IdPhieu)
                       .ExecuteSingle<EmrGiayChungsinh>();
                if (_phieu.Id <= 0)
                {
                    Utility.ShowMsg("Bạn cần lưu thông tin Giấy chứng nhận tai nạn thương tích trước khi thực hiện in phiếu");
                    //cmdGhi.Focus();
                    return;
                }
                DataTable dtData = SPs.EmrGiayChungsinhLaythongtinIn(_phieu.Id).GetDataSet().Tables[0];
                dtData.TableName = "GIAY_CHUNGSINH";
                dtData.Rows[0]["sngaysinh_be"] = _phieu != null ? Utility.FormatDateTime_giophut_ngay_thang_nam(_phieu.NgaysinhBe, "") : ":........giờ........phút, ngày........tháng........năm...............";
                dtData.Rows[0]["sngaycap_giaychungsinh"] = _phieu != null ? Utility.FormatDateTime_giophut_ngay_thang_nam(_phieu.NgaycapGiaychungsinh, "") : "ngày........tháng........năm...............";
                WordPrinter.InPhieu(dtData, "GIAY_CHUNGSINH.doc","",false, @"\MAUBA\GIAY_CHUNGSINH_CHECKED_FIELDS.txt");
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdTuSinh_Click(object sender, EventArgs e)
        {
            SinhMaSoGCSMoi();
        }

        private void uc_giaychungsinh_Load(object sender, EventArgs e)
        {

        }

        private void cmdthemmoi_Click(object sender, EventArgs e)
        {
            ClearControl(this);
            m_enAct = action.Insert;
            _phieu = null;
            grb_thongtin_me.Enabled = grb_thongtin_bo.Enabled = grb_thongtin_con.Enabled = true;
            ModifyCommandButtons();
            SinhMaSoGCSMoi();
             isAllowSelectionChanged = false;
            txt_ma_sobhxh_me.Focus();
        }
        void SinhMaSoGCSMoi()
        {
            DataTable dtMaGCS = SPs.GiaychungsinhSinhmaso(globalVariables.Ma_Coso, dtp_ngaycap_giaychungsinh.Value.Year).GetDataSet().Tables[0];
            string ma_moi = dtMaGCS != null && dtMaGCS.Rows.Count > 0 ? dtMaGCS.Rows[0][0].ToString() : "";
            txt_maso_giaychungsinh.Text = _phieu == null || string.IsNullOrEmpty(Utility.sDbnull(_phieu.MasoGiaychungsinh, "")) ? ma_moi : Utility.sDbnull(_phieu.MasoGiaychungsinh, "");
        }
        private void cmdHuy_Click(object sender, EventArgs e)
        {
            m_enAct = action.FirstOrFinished;
            isAllowSelectionChanged = true;
            grb_thongtin_me.Enabled = grb_thongtin_bo.Enabled = grb_thongtin_con.Enabled = false;
            ModifyCommandButtons();
            
        }

        private void cmdSua_Click(object sender, EventArgs e)
        {
            
            BeginUpdate();
            isAllowSelectionChanged = false;
        }

        private void cmdxoa_Click(object sender, EventArgs e)
        {
            try
            {
                EmrGiayChungsinh _phieu = EmrGiayChungsinh.FetchByID(Utility.Int32Dbnull(grdList.GetValue(EmrGiayChungsinh.Columns.Id), -1));
                if (_phieu == null)
                {
                    Utility.ShowMsg(string.Format("Giấy chứng sinh của bé {0} con của sản phụ {1} có thể đã bị người khác xóa ở chức năng khác. Vui lòng bấm OK để hệ thống refresh lại dữ liệu", grdList.GetValue("hoten_be").ToString(), grdList.GetValue("ten_benhnhan").ToString()));
                    Init(objLuotkham, null);
                    return;
                }

                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa GCS có mã: {0} của bé {1} con của sản phụ {2} hay không?", grdList.GetValue(EmrGiayChungsinh.Columns.MasoGiaychungsinh).ToString(), grdList.GetValue("hoten_be").ToString(), grdList.GetValue("ten_benhnhan").ToString()), "Xác nhận xóa", true))
                {
                    if (DeleteMe())
                    {
                        Utility.ShowMsg(string.Format("Xóa Giấy chứng sinh cho bé {0} thành công", grdList.GetValue("hoten_be").ToString()));
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", EmrGiayChungsinh.Columns.Id, grdList.GetValue(EmrGiayChungsinh.Columns.Id)));
                        if (arrDr.Length > 0)
                            m_dtData.Rows.Remove(arrDr[0]);
                        m_dtData.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

       

        private void cmdIn_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void cmdGhi_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void dtp_ngaycap_giaychungsinh_ValueChanged(object sender, EventArgs e)
        {
            //if(m_enAct!=action.FirstOrFinished)
            //    SinhMaSoGCSMoi();
        }

        private void cmd_duplicate_Click(object sender, EventArgs e)
        {
            
            if(Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn sao chép giấy chứng sinh có mã số {0} của bé {1} thành 1 phiếu khác.\nTính năng sao chép chỉ nên được dùng khi sản phụ sinh số con nhiều hơn 1 trong lần sinh này",Utility.sDbnull(grdList.GetValue("maso_giaychungsinh")), Utility.sDbnull(grdList.GetValue("hoten_be"))),"Xác nhận",true))
            {
                EmrGiayChungsinh _phieu = EmrGiayChungsinh.FetchByID(Utility.Int32Dbnull(grdList.GetValue(EmrGiayChungsinh.Columns.Id), -1));
                SinhMaSoGCSMoi();
                _phieu.MasoGiaychungsinh = Utility.sDbnull(txt_maso_giaychungsinh.Text);
                _phieu.IsNew = true;
                _phieu.Save();
                Utility.ShowMsg("Đã sao chép phiếu thành công. Nhấn OK để kết thúc");
                OnChangedData(_phieu.Id, action.Insert);
            }    
        }
    }
}
