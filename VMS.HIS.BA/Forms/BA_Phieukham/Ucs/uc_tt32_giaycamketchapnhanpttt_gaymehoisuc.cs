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
    public partial class uc_tt32_giaycamketchapnhanpttt_gaymehoisuc : UserControl
    {
        public delegate void OnMsg(string msg, bool IsSucess = false);
        public event OnMsg _OnMsg;
        public delegate void OnStatus(bool isNew);
        public event OnStatus _OnStatus;
        public EmrPhieucamketchapnhanPttt phieucamket;
        KcbLuotkham objLuotkham;
        public int id_bacsikham = -1;
        DmucNhanvien objBacsiPttt = null;
        DmucNhanvien objNguoiDaidien = null;
        public bool Force2Saved = false;
        public uc_tt32_giaycamketchapnhanpttt_gaymehoisuc()
        {
            InitializeComponent();
            txt_bacsi_pttt._OnEnterMe += txt_bacsi_pttt_OnEnterMe;
            txt_chucdanh_bacsi_pttt._OnShowDataV1 += _OnShowDataV1;
            txt_chucdanh_bacsi_gaymehoisuc._OnShowDataV1 += _OnShowDataV1;
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

        private void txt_bacsi_pttt_OnEnterMe()
        {
            objBacsiPttt = DmucNhanvien.FetchByID(Utility.Int32Dbnull(txt_bacsi_pttt.MyID));
            if (objBacsiPttt != null)
                txt_khoa.SetId(Utility.Int16Dbnull( objBacsiPttt.IdKhoa));
        }

        public void Init(KcbLuotkham objLuotkham, EmrPhieucamketchapnhanPttt phieucamket)
        {
            dtp_ngaycamket.Value = globalVariables.SysDate;
            this.objLuotkham = objLuotkham;
            this.phieucamket = phieucamket;
            txt_bacsi_pttt.Init(globalVariables.gv_dtDmucNhanvien,
                                            new List<string>
                                 {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                 });
            txt_bacsi_gaymehoisuc.Init(txt_bacsi_pttt.AutoCompleteSource, txt_bacsi_pttt.defaultItem);
            txt_chucdanh_bacsi_gaymehoisuc.Init();
            DataTable dtData = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { txt_chucdanh_bacsi_gaymehoisuc.LOAI_DANHMUC, txt_quanhevoinguoibenh.LOAI_DANHMUC }, true);
            txt_chucdanh_bacsi_pttt.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_chucdanh_bacsi_gaymehoisuc.LOAI_DANHMUC));
            txt_chucdanh_bacsi_gaymehoisuc.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_chucdanh_bacsi_gaymehoisuc.LOAI_DANHMUC));


        }


        public void Init(KcbLuotkham objLuotkham)
        {
            dtp_ngaycamket.Value = globalVariables.SysDate;
            this.objLuotkham = objLuotkham;
            phieucamket = new Select().From(EmrPhieucamketchapnhanPttt.Schema)
                        .Where(EmrPhieucamketchapnhanPttt.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(EmrPhieucamketchapnhanPttt.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<EmrPhieucamketchapnhanPttt>();
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
            dtp_ngaycamket.Value = globalVariables.SysDate;
            txt_bacsi_pttt.Init(globalVariables.gv_dtDmucNhanvien,
                                             new List<string>
                                  {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                  });
            txt_bacsi_gaymehoisuc.Init(txt_bacsi_pttt.AutoCompleteSource, txt_bacsi_pttt.defaultItem);
            txt_chucdanh_bacsi_gaymehoisuc.Init();
            DataTable dtData = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { txt_chucdanh_bacsi_gaymehoisuc.LOAI_DANHMUC, txt_quanhevoinguoibenh.LOAI_DANHMUC }, true);
            txt_chucdanh_bacsi_pttt.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_chucdanh_bacsi_gaymehoisuc.LOAI_DANHMUC));
            txt_chucdanh_bacsi_gaymehoisuc.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_chucdanh_bacsi_gaymehoisuc.LOAI_DANHMUC));
            DataTable dtKhoaPhong = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL", 0);
            txt_khoa.Init(dtKhoaPhong, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
        }
        public void DisplayData()
        {
            try
            {
               
                if (phieucamket == null)
                    phieucamket = new Select().From(EmrPhieucamketchapnhanPttt.Schema)
                        .Where(EmrPhieucamketchapnhanPttt.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(EmrPhieucamketchapnhanPttt.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<EmrPhieucamketchapnhanPttt>();
               
                txtId.Text = "";
                if (phieucamket != null)
                {
                    txtId.Text = phieucamket.IdPhieu.ToString();
                    txtSoHoso.Text = phieucamket.MaPhieu;
                    dtp_ngaycamket.Value = phieucamket.NgayCamket;
                    opt_capcuu.Checked = Utility.Bool2Bool(phieucamket.Capcuu);
                    opt_bancap.Checked = Utility.Bool2Bool(phieucamket.Bancap);
                    opt_chuongtrinh_phien.Checked = Utility.Bool2Bool(phieucamket.ChuongtrinhPhien);
                    txt_bacsi_pttt.SetId(phieucamket.IdBacsiPttt);
                    txt_chucdanh_bacsi_pttt._Text = phieucamket.ChucdanhBacsiPttt;
                    txt_khoa.SetId(phieucamket.IdKhoa);
                    txt_bacsi_gaymehoisuc.SetId(phieucamket.IdBacsiGaymehoisuc);
                    txt_chucdanh_bacsi_gaymehoisuc._Text = phieucamket.ChucdanhBacsiGaymehoisuc;
                    txt_chandoan.Text = phieucamket.ChandoanMota;
                    chk_chandoan.Checked = Utility.Bool2Bool(phieucamket.Chandoan);
                    chk_lydo_pttt.Checked = Utility.Bool2Bool(phieucamket.LydoPttt);
                    chk_ruiro_nguyco_neukhongthuchien_pttt.Checked = Utility.Bool2Bool(phieucamket.RuiroNguycoNeukhongthuchienPttt);
                    chk_ketquasaupttt.Checked = Utility.Bool2Bool(phieucamket.Ketquasaupttt);
                    txt_ketquasaupttt_mota.Text = phieucamket.KetquasauptttMota;
                    chk_phauthuatnoisoi.Checked = Utility.Bool2Bool(phieucamket.Phauthuatnoisoi);
                    chk_thuthuat.Checked = Utility.Bool2Bool(phieucamket.Thuthuat);
                    //Phương pháp gây mê hồi sức dự kiến
                    chk_gaymenoikhiquan.Checked = Utility.Bool2Bool(phieucamket.Gaymenoikhiquan);
                    chk_gayme_mask_thanhquan.Checked = Utility.Bool2Bool(phieucamket.GaymeMaskThanhquan);
                    chk_gaymetinhmach.Checked = Utility.Bool2Bool(phieucamket.Gaymetinhmach);
                    chk_gaytetuysong.Checked = Utility.Bool2Bool(phieucamket.Gaytetuysong);
                    chk_gayte_ngoaimangcung.Checked = Utility.Bool2Bool(phieucamket.GayteNgoaimangcung);
                    chk_gayte_damroi_thanhkinh.Checked = Utility.Bool2Bool(phieucamket.GayteDamroiThanhkinh);
                    chk_gaytetaicho.Checked = Utility.Bool2Bool(phieucamket.Gaytetaicho);
                    chk_phuongphapgayme_khac.Checked = Utility.Bool2Bool(phieucamket.GayteKhac);
                    txt_gaytekhac_mota.Text = phieucamket.GayteKhacMota;
                    //Các phương pháp điều trị khác ngoài pttt
                    chk_cacphuongphapdieutrikhacpttt_khong.Checked = Utility.Bool2Bool(phieucamket.CacphuongphapdieutrikhacptttKhong);
                   chk_cacphuongphapdieutrikhacpttt_co.Checked = Utility.Bool2Bool(phieucamket.CacphuongphapdieutrikhacptttCo);
                    txt_cacphuongphapdieutrikhacpttt_mota.Text = phieucamket.CacphuongphapdieutrikhacptttMota;
                    //Nguy cơ tai biến
                    chk_phanungthuoc.Checked = Utility.Bool2Bool(phieucamket.Phanungthuoc);
                    chk_suyhohap_tuanhoan.Checked = Utility.Bool2Bool(phieucamket.SuyhohapTuanhoan);
                    chk_chaymau.Checked = Utility.Bool2Bool(phieucamket.Chaymau);
                    chk_nhiemtrung.Checked = Utility.Bool2Bool(phieucamket.Nhiemtrung);
                    chk_tuvong.Checked = Utility.Bool2Bool(phieucamket.Tuvong);
                    chk_nguycokhac.Checked = Utility.Bool2Bool(phieucamket.Nguycokhac);
                    txt_nguycokhac_mota.Text = phieucamket.NguycokhacMota;
                    //Người liên hệ
                    txt_hoten_nguoilienhe.Text = phieucamket.HotenNguoilienhe;
                    dtp_namsinh_nguoilienhe.Text = Utility.sDbnull(phieucamket.NamsinhNguoilienhe);
                    txt_quanhevoinguoibenh._Text = phieucamket.Quanhevoinguoibenh;

                }
                else
                {
                    ClearControl();
                    
                }
                txtSoHoso.Text = phieucamket == null || string.IsNullOrEmpty(Utility.sDbnull(phieucamket.MaPhieu, "")) ? THU_VIEN_CHUNG.TT25LaySohoso(6) : Utility.sDbnull(phieucamket.MaPhieu, "");
                if (_OnStatus != null) _OnStatus(phieucamket == null || phieucamket.IdPhieu <= 0);
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
            DataTable dtData = new Select().From(EmrPhieucamketchapnhanPttt.Schema)
              .Where(EmrPhieucamketchapnhanPttt.Columns.MaPhieu).IsEqualTo(Utility.DoTrim(txtSoHoso.Text))
              .And(EmrPhieucamketchapnhanPttt.Columns.IdPhieu).IsNotEqualTo(Utility.Int64Dbnull(txtId.Text, -1))
              .ExecuteDataSet().Tables[0];
            if (dtData.Rows.Count > 0)
            {
                Msg = "Mã phiếu đã được sử dụng. Vui lòng nhập mã phiếu khác";
                txtSoHoso.Focus();
                return false;
            }
            if (txt_bacsi_pttt.MyID=="-1")
            {
                Msg = "Bạn phải chọn Bác sĩ phẫu thuật từ danh mục bác sĩ";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_bacsi_pttt.SelectAll();
                txt_bacsi_pttt.Focus();
                return false;
            }
            if(Utility.sDbnull( txt_chucdanh_bacsi_pttt.Text)=="")
            {
                Msg = "Bạn phải nhập chức danh Bác sĩ phẫu thuật";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_chucdanh_bacsi_pttt.SelectAll();
                txt_chucdanh_bacsi_pttt.Focus();
                return false;
            }
            if (txt_khoa.MyID == "-1")
            {
                Msg = "Bạn phải chọn Khoa từ danh mục Khoa phòng";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_khoa.SelectAll();
                txt_khoa.Focus();
                return false;
            }
            if (txt_bacsi_gaymehoisuc.MyID == "-1")
            {
                Msg = "Bạn phải chọn Bác sĩ gây mê từ danh mục bác sĩ";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_bacsi_gaymehoisuc.SelectAll();
                txt_bacsi_gaymehoisuc.Focus();
                return false;
            }
            if (Utility.sDbnull(txt_chucdanh_bacsi_gaymehoisuc.Text) == "")
            {
                Msg = "Bạn phải nhập chức danh Bác sĩ gây mê";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_chucdanh_bacsi_gaymehoisuc.SelectAll();
                txt_chucdanh_bacsi_gaymehoisuc.Focus();
                return false;
            }
            if (Utility.sDbnull(txt_chandoan.Text) == "")
            {
                Msg = "Bạn phải nhập thông tin chẩn đoán";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_chandoan.SelectAll();
                txt_chandoan.Focus();
                return false;
            }
            if (Utility.sDbnull(txt_hoten_nguoilienhe.Text) == "")
            {
                Msg = "Bạn phải nhập thông tin người liên hệ";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_hoten_nguoilienhe.SelectAll();
                txt_hoten_nguoilienhe.Focus();
                return false;
            }
            if (Utility.sDbnull(txt_quanhevoinguoibenh.Text) == "")
            {
                Msg = "Bạn phải nhập thông tin quan hệ thân nhân với người bệnh";
                if (_OnMsg != null) _OnMsg(Msg);
                txt_quanhevoinguoibenh.SelectAll();
                txt_quanhevoinguoibenh.Focus();
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
                        phieucamket = new Select().From(EmrPhieucamketchapnhanPttt.Schema)
                   .Where(EmrPhieucamketchapnhanPttt.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(EmrPhieucamketchapnhanPttt.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<EmrPhieucamketchapnhanPttt>();
                       
                        if (phieucamket == null || phieucamket.IdPhieu <= 0)
                        {
                            isNew = true;
                            phieucamket = new EmrPhieucamketchapnhanPttt();
                            phieucamket.IsNew = true;
                            phieucamket.NgayTao = DateTime.Now;
                            phieucamket.NguoiTao = globalVariables.UserName;
                        }
                        else
                        {
                            isNew = false;
                            phieucamket.IsNew = false;
                            phieucamket.MarkOld();
                            phieucamket.NgaySua = DateTime.Now;
                            phieucamket.NguoiSua = globalVariables.UserName;
                        }
                        phieucamket.IdBenhnhan = objLuotkham.IdBenhnhan;
                        phieucamket.MaLuotkham = objLuotkham.MaLuotkham;
                        phieucamket.Capcuu = opt_capcuu.Checked;
                        phieucamket.Bancap = opt_bancap.Checked;
                        phieucamket.ChuongtrinhPhien = opt_chuongtrinh_phien.Checked;
                        phieucamket.NgayCamket = dtp_ngaycamket.Value;
                        phieucamket.MaPhieu = txtSoHoso.Text;
                        phieucamket.IdBacsiPttt =Utility.Int16Dbnull( txt_bacsi_pttt.MyID);
                        phieucamket.ChucdanhBacsiPttt = txt_chucdanh_bacsi_pttt.Text;
                        phieucamket.IdKhoa = Utility.Int16Dbnull(txt_khoa.MyID);
                        phieucamket.TenKhoa= Utility.sDbnull(txt_khoa.Text);
                        phieucamket.IdBacsiGaymehoisuc = Utility.Int16Dbnull(txt_bacsi_gaymehoisuc.MyID);
                        phieucamket.ChucdanhBacsiGaymehoisuc = txt_chucdanh_bacsi_gaymehoisuc.Text;
                        phieucamket.ChandoanMota = Utility.sDbnull(txt_chandoan.Text);
                        phieucamket.Chandoan = chk_chandoan.Checked;
                        phieucamket.LydoPttt = chk_lydo_pttt.Checked;
                        phieucamket.RuiroNguycoNeukhongthuchienPttt = chk_ruiro_nguyco_neukhongthuchien_pttt.Checked;
                        phieucamket.Ketquasaupttt = chk_ketquasaupttt.Checked;
                        phieucamket.KetquasauptttMota = chk_ketquasaupttt.Checked? Utility.sDbnull(txt_ketquasaupttt_mota.Text):"";
                        phieucamket.Phauthuatnoisoi = chk_phauthuatnoisoi.Checked;
                        phieucamket.Thuthuat = chk_thuthuat.Checked;

                        // Phương pháp gây mê hồi sức dự kiến
                        phieucamket.Gaymenoikhiquan = chk_gaymenoikhiquan.Checked;
                        phieucamket.GaymeMaskThanhquan = chk_gayme_mask_thanhquan.Checked;
                        phieucamket.Gaymetinhmach = chk_gaymetinhmach.Checked;
                        phieucamket.Gaytetuysong = chk_gaytetuysong.Checked;
                        phieucamket.GayteNgoaimangcung = chk_gayte_ngoaimangcung.Checked;
                        phieucamket.GayteDamroiThanhkinh = chk_gayte_damroi_thanhkinh.Checked;
                        phieucamket.Gaytetaicho = chk_gaytetaicho.Checked;
                        phieucamket.GayteKhac = chk_phuongphapgayme_khac.Checked;
                        phieucamket.GayteKhacMota = chk_phuongphapgayme_khac.Checked? Utility.sDbnull(txt_gaytekhac_mota.Text):"";

                        // Các phương pháp điều trị khác ngoài PTTT
                        phieucamket.CacphuongphapdieutrikhacptttKhong = chk_cacphuongphapdieutrikhacpttt_khong.Checked;
                        phieucamket.CacphuongphapdieutrikhacptttCo = chk_cacphuongphapdieutrikhacpttt_co.Checked;
                        phieucamket.CacphuongphapdieutrikhacptttMota = chk_cacphuongphapdieutrikhacpttt_co.Checked?Utility.sDbnull(txt_cacphuongphapdieutrikhacpttt_mota.Text):"";

                        // Nguy cơ tai biến
                        phieucamket.Phanungthuoc = chk_phanungthuoc.Checked;
                        phieucamket.SuyhohapTuanhoan = chk_suyhohap_tuanhoan.Checked;
                        phieucamket.Chaymau = chk_chaymau.Checked;
                        phieucamket.Nhiemtrung = chk_nhiemtrung.Checked;
                        phieucamket.Tuvong = chk_tuvong.Checked;
                        phieucamket.Nguycokhac = chk_nguycokhac.Checked;
                        phieucamket.NguycokhacMota = chk_nguycokhac.Checked? Utility.sDbnull(txt_nguycokhac_mota.Text):"";

                        // Người liên hệ
                        phieucamket.HotenNguoilienhe = Utility.sDbnull(txt_hoten_nguoilienhe.Text);
                        phieucamket.NamsinhNguoilienhe = Utility.Int32Dbnull(dtp_namsinh_nguoilienhe.Text);
                        phieucamket.Quanhevoinguoibenh = txt_quanhevoinguoibenh.Text;

                        phieucamket.Save();
                        if (objBacsiPttt == null)
                            objBacsiPttt = DmucNhanvien.FetchByID(Utility.Int32Dbnull(txt_bacsi_pttt.MyID));
                        emrdoc.Force2Saved = Force2Saved;
                        emrdoc.InitDocument(phieucamket.IdBenhnhan, phieucamket.MaLuotkham, Utility.Int64Dbnull(phieucamket.IdPhieu), phieucamket.NgayCamket, Loaiphieu_HIS.PHIEU_CAMKET_PTTT, "PHIEU_CAMKET_PTTT", phieucamket.NguoiTao,Utility.Int16Dbnull( objBacsiPttt.IdKhoa), Utility.Int16Dbnull(objBacsiPttt.IdPhong), Utility.Byte2Bool(0),"");
                        emrdoc.Save();

                    }
                    scope.Complete();
                }
                txtId.Text = phieucamket.IdPhieu.ToString();
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
                phieucamket = new Select().From(EmrPhieucamketchapnhanPttt.Schema)
                       .Where(EmrPhieucamketchapnhanPttt.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                       .And(EmrPhieucamketchapnhanPttt.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                       .ExecuteSingle<EmrPhieucamketchapnhanPttt>();
                if (phieucamket.IdPhieu <= 0)
                {
                    Utility.ShowMsg("Bạn cần lưu thông tin phiếu chấp thuận PTTT và Gây mê hồi sức trước khi thực hiện in phiếu");
                    return;
                }
                DataTable dtData = SPs.EmrPhieucamketchapnhanPtttLaythongtinIn(phieucamket.IdPhieu).GetDataSet().Tables[0];
                dtData.TableName = "phieucamketchapnhan_pttt";
                dtData.Rows[0]["sngay_camket"] = phieucamket != null ? Utility.FormatDateTime_gio_ngay_thang_nam(phieucamket.NgayCamket, "") : "Ngày ......./......./..........";
                WordPrinter.InPhieu(dtData, "phieucamketchapnhan_pttt.doc", "");


            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdTuSinh_Click(object sender, EventArgs e)
        {
            txtSoHoso.Text = THU_VIEN_CHUNG.TT25LaySohoso(6);
        }

        private void chk_ketquasaupttt_CheckedChanged(object sender, EventArgs e)
        {
            txt_ketquasaupttt_mota.Enabled = chk_ketquasaupttt.Checked;
            txt_ketquasaupttt_mota.Focus();
        }

        private void chk_phuongphapgayme_khac_CheckedChanged(object sender, EventArgs e)
        {
            txt_gaytekhac_mota.Enabled = chk_phuongphapgayme_khac.Checked;
            txt_gaytekhac_mota.Focus();
        }

        private void chk_cacphuongphapdieutrikhacpttt_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_cacphuongphapdieutrikhacpttt_mota.Enabled = chk_cacphuongphapdieutrikhacpttt_co.Checked;
        }

        private void chk_nguycokhac_CheckedChanged(object sender, EventArgs e)
        {
            txt_nguycokhac_mota.Enabled = chk_nguycokhac.Checked;
            txt_nguycokhac_mota.Focus();
        }
    }
}
