using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VietBaIT.CommonLibrary;
using VietBaIT.HISLink.DataAccessLayer;
using VietBaIT.HISLink.Business.PhongMo;
using VietBaIT.HISLink.UI.ControlUtility.LichSuCLS;
using NLog;

namespace VietBaIT.HISLink.UI.PhongMo.NghiepVu
{
    public partial class frmPhieuSoKetTruocMo : Form
    {
        #region Variables
        int _nBoSung = 0;
        public int _nIDPhongMo = -1;
        public int _nAssignDetail_ID = -1;
        public int _nPatient_ID = -1;
        public string _sPatient_Code = string.Empty;
        string _sPatient_Name = string.Empty;

        PmQuanLyPhongMo _oQLPM = null;
        PmSoKetTruocMo _oSKTM = null;
        TPatientInfo _oPatientInfo = null;
        TPatientExam _oPatientExam = null;

        decimal _nMoney = 0; //Biến này dùng để tính chi phí ca mổ, dựa vào phương pháp mổ.

        private NLog.Logger log;

        #endregion

        #region Form Events
        public frmPhieuSoKetTruocMo(int nBoSung = 0)
        {
            InitializeComponent();
            _nBoSung = nBoSung;
            btnSoKetBoSung.Enabled = _nBoSung == 0;

            txtDuKien_PPVC._OnSaveAs += txtDuKien_PPVC_OnSaveAs;
            txtDuKien_PPVC._OnShowData += txtDuKien_PPVC_OnShowData;

            txtDuKien_KhoKhan._OnSaveAs += txtDuKien_KhoKhan_OnSaveAs;
            txtDuKien_KhoKhan._OnShowData += txtDuKien_KhoKhan_OnShowData;

            txtkipmo._OnSaveAs += txtkipmo_OnSaveAs;
            txtkipmo._OnShowData += txtkipmo_OnShowData;

            txtkipgayme._OnSaveAs += txtkipgayme_OnSaveAs;
            txtkipgayme._OnShowData += txtkipgayme_OnShowData;
            log = LogManager.GetCurrentClassLogger();
        }

        private void frmPhieuSoKetTruocMo_Load(object sender, EventArgs e)
        {
            if(globalVariables.gvAccountName =="PKKQ" )
            {
                txtkipgayme.Visible = true;
                txtkipmo.Visible = true;
                cbokip_gayme.Visible = false;
                cbokip_pttt.Visible = false;
            }
            txtChuTri.Init(
                globalVariables.g_dtStaffs.Select("stafftype_id = 1 or stafftype_id = 3")
                    .Any()
                    ? globalVariables.g_dtStaffs.Select("stafftype_id = 1 or stafftype_id = 3").CopyToDataTable()
                    : null,
                new List<string> { LStaff.Columns.StaffId, LStaff.Columns.StaffCode, LStaff.Columns.StaffName });

            btnSoKetBoSung.Enabled = _nBoSung == 0;
            this.Text = "Sơ kết trước PT-TT" + (_nBoSung == 1 ? " bổ sung" : "");
            _oQLPM = PmQuanLyPhongMo.FetchByID(_nIDPhongMo);
            if(_oQLPM != null)
            {
                _nPatient_ID = Utility.Int32Dbnull(_oQLPM.PatientId, -1);
                _sPatient_Code = Utility.sDbnull(_oQLPM.PatientCode, string.Empty);
            }
            else
            {

            }

            DataTable _dtStaff = globalVariables.g_dtStaffs.Select("stafftype_id = 1 or stafftype_id = 3").CopyToDataTable();

            foreach (DataRow item in _dtStaff.Rows)
            {
                item.SetField<string>("Staff_Name", item.Field<string>("Staff_Name").Replace(",", ""));
            }

            cbokip_gayme.DropDownDataSource = _dtStaff.Copy();
            cbokip_gayme.DropDownDataMember = LStaff.Columns.StaffId; //LStaff.Columns.StaffCode;
            cbokip_gayme.DropDownValueMember = LStaff.Columns.StaffId; //LStaff.Columns.StaffCode;
            cbokip_gayme.DropDownDisplayMember = LStaff.Columns.StaffName;

            cbokip_pttt.DropDownDataSource = _dtStaff.Copy();
            cbokip_pttt.DropDownDataMember = LStaff.Columns.StaffId;
            cbokip_pttt.DropDownValueMember = LStaff.Columns.StaffId;
            cbokip_pttt.DropDownDisplayMember = LStaff.Columns.StaffName;

            cbophudungcu.DropDownDataSource = _dtStaff.Copy();
            cbophudungcu.DropDownDataMember = LStaff.Columns.StaffId;
            cbophudungcu.DropDownValueMember = LStaff.Columns.StaffId;
            cbophudungcu.DropDownDisplayMember = LStaff.Columns.StaffName;


            GetPatientInfo();
            bindPhieuMau();
            bindPhuongPhapVoCam();
            bindDanhsachBSPT();
            bindDanhsachBSGayMe();
            bindDuKienKhoKhan();
            GetInfo();
        }
        #endregion

        #region Methods
        void GetPatientInfo()
        {
            try
            {
                _oPatientInfo = TPatientInfo.FetchByID(Utility.Int32Dbnull(_nPatient_ID, -1));
                _oPatientExam = new Select().From(TPatientExam.Schema)
                                            .Where(TPatientExam.Columns.PatientId).IsEqualTo(_nPatient_ID)
                                            .And(TPatientExam.Columns.PatientCode).IsEqualTo(_sPatient_Code)
                                            .ExecuteSingle<TPatientExam>();
                TAssignDetail  _oAssign = new Select().From(TAssignDetail.Schema)
                                          .Where(TAssignDetail.Columns.AssignDetailId).IsEqualTo(_nAssignDetail_ID)
                                          
                                          .ExecuteSingle<TAssignDetail>();

                _sPatient_Name = Utility.sDbnull(_oPatientInfo.PatientName, string.Empty);
                txtPatientID.Text = Utility.Int32Dbnull(_oPatientInfo.PatientId, -1).ToString();
                txtPatientCode.Text = _sPatient_Code;
                txtPatientName.Text = _sPatient_Name;
                txtSex.Text = Utility.Int32Dbnull(_oPatientInfo.PatientSex, 2) == 0 ? "Nam" : Utility.Int32Dbnull(_oPatientInfo.PatientSex, 2) == 1 ? "Nữ" : "Khác";
                txtYearBirth.Text = Utility.Int32Dbnull(_oPatientInfo.YearOfBirth).ToString();
                if(_oQLPM!= null)
                {
                    txtDepartment_ID.Text = Utility.sDbnull(_oQLPM.DepartmentId, string.Empty);
                    txtKhoaNoiTru.Text = Utility.sDbnull(LDepartment.FetchByID(_oQLPM.DepartmentId).DepartmentName, string.Empty);

                    txtChanDoan.Text = Utility.sDbnull(_oQLPM.ChanDoan, string.Empty);
                    txtServiceName.Text = Utility.sDbnull(LService.FetchByID(Utility.Int32Dbnull(_oQLPM.ServiceId)).ServiceName);
                    txtServiceDetailName.Text = Utility.sDbnull(LServiceDetail.FetchByID(Utility.Int32Dbnull(_oQLPM.ServiceDetailId)).ServiceDetailName);

                    if (Utility.Int32Dbnull(_oQLPM.RoomId) > 0)
                    {
                        txtPhong.Text = Utility.sDbnull(LRoom.FetchByID(Utility.Int32Dbnull(_oQLPM.RoomId)).RoomName);
                    }
                    if (Utility.Int32Dbnull(_oQLPM.BedId) > 0)
                    {
                        txtGiuong.Text = Utility.sDbnull(LBed.FetchByID(Utility.Int32Dbnull(_oQLPM.BedId)).BedName);
                    }

                    txtObjectType_ID.Text = Utility.sDbnull(_oPatientExam.ObjectTypeId, "1");
                    txtAssignDetail_ID.Text = Utility.sDbnull(_oQLPM.AssignDetailId, "-1");
                }
                else
                {
                    if(_oPatientExam != null)
                    {
                        txtDepartment_ID.Text = Utility.sDbnull(_oPatientExam.DepartmentId, string.Empty);
                        txtKhoaNoiTru.Text = Utility.sDbnull(LDepartment.FetchByID(_oPatientExam.DepartmentId).DepartmentName, string.Empty);                       
                        txtServiceName.Text = Utility.sDbnull(LService.FetchByID(Utility.Int32Dbnull(_oAssign.ServiceId)).ServiceName);
                        txtServiceDetailName.Text = Utility.sDbnull(LServiceDetail.FetchByID(Utility.Int32Dbnull(_oAssign.ServiceDetailId)).ServiceDetailName);
                        if (Utility.Int32Dbnull(_oPatientExam.RoomId) > 0)
                        {
                            txtPhong.Text = Utility.sDbnull(LRoom.FetchByID(Utility.Int32Dbnull(_oPatientExam.RoomId)).RoomName);
                        }
                        if (Utility.Int32Dbnull(_oPatientExam.BedId) > 0)
                        {
                            txtGiuong.Text = Utility.sDbnull(LBed.FetchByID(Utility.Int32Dbnull(_oPatientExam.BedId)).BedName);
                        }
                        txtObjectType_ID.Text = Utility.sDbnull(_oPatientExam.ObjectTypeId, "1");
                        txtAssignDetail_ID.Text = Utility.sDbnull(_nAssignDetail_ID, "-1");
                    }
                    
                }
                
                //txtPatientDept_ID.Text = Utility.sDbnull(_oQLPM.pa)
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                log.Error("Loi trong qua trinh lay thong tin benh nhan" + ex.ToString());
                //throw;
            }
        }

        void GetInfo()
        {
           ClearControl();
            txtDuKien_PPPT.Text = txtServiceDetailName.Text;
            if (_nIDPhongMo > 0)
            {
                if (_nBoSung == 0)
                {
                    _oSKTM = new Select().From(PmSoKetTruocMo.Schema)
                                        .Where(PmSoKetTruocMo.Columns.IdPhongMo).IsEqualTo(_nIDPhongMo)
                                        .AndExpression(PmSoKetTruocMo.Columns.SoKetBoSung).IsNull()
                                        .Or(PmSoKetTruocMo.Columns.SoKetBoSung).IsEqualTo(0)
                                        .CloseExpression().ExecuteSingle<PmSoKetTruocMo>();
                }
                else
                {
                    _oSKTM = new Select().From(PmSoKetTruocMo.Schema)
                                        .Where(PmSoKetTruocMo.Columns.IdPhongMo).IsEqualTo(_nIDPhongMo)
                                        .And(PmSoKetTruocMo.Columns.SoKetBoSung).IsEqualTo(1)
                                        .ExecuteSingle<PmSoKetTruocMo>();
                }
            }

            if (_nAssignDetail_ID > 0 && _nIDPhongMo <= 0)
            {
                if (_nBoSung == 0)
                {
                    _oSKTM = new Select().From(PmSoKetTruocMo.Schema)
                                        .Where(PmSoKetTruocMo.Columns.AssignDetailId).IsEqualTo(_nAssignDetail_ID)
                                        .AndExpression(PmSoKetTruocMo.Columns.SoKetBoSung).IsNull()
                                        .Or(PmSoKetTruocMo.Columns.SoKetBoSung).IsEqualTo(0)
                                        .CloseExpression().ExecuteSingle<PmSoKetTruocMo>();
                }
                else
                {
                    _oSKTM = new Select().From(PmSoKetTruocMo.Schema)
                                        .Where(PmSoKetTruocMo.Columns.AssignDetailId).IsEqualTo(_nAssignDetail_ID)
                                        .And(PmSoKetTruocMo.Columns.SoKetBoSung).IsEqualTo(1)
                                        .ExecuteSingle<PmSoKetTruocMo>();
                }
            }

            if (_oSKTM != null && Utility.Int32Dbnull(_oSKTM.IdPhieu, -1) > 0)
            {
                txtID_Phieu.Text = Utility.sDbnull(_oSKTM.IdPhieu, "-1");
                txtChanDoanTruocMo.Text = Utility.sDbnull(_oSKTM.ChanDoanTruocMo);
                txtBenhSu.Text = Utility.sDbnull(_oSKTM.BenhSuVaDienBien);
                txtTrieuChung.Text = Utility.sDbnull(_oSKTM.TrieuChung);
                txtKetQuaXN_Mau.Text = Utility.sDbnull(_oSKTM.KetQuaXNHh);
                txtKetQuaXN_MauSH.Text = Utility.sDbnull(_oSKTM.KetQuaXNSh);
                txtKetQuaXN_MauDM.Text = Utility.sDbnull(_oSKTM.KetQuaXNDm);
                txtKetQuaXN_NT.Text = Utility.sDbnull(_oSKTM.KetQuaXNNt);
                txtKetQuaXN_Khac.Text = Utility.sDbnull(_oSKTM.KetQuaXNKhac);
                txtKetQuaCDHA_XQuangTP.Text = Utility.sDbnull(_oSKTM.KetQuaCDHAXqtp);
                txtKetQuaCDHA_XQuang.Text = Utility.sDbnull(_oSKTM.KetQuaCDHAXq);
                txtKetQuaCDHA_SieuAm.Text = Utility.sDbnull(_oSKTM.KetQuaCDHASa);
                txtKetQuaCDHA_NoiSoi.Text = Utility.sDbnull(_oSKTM.KetQuaCDHANs);
                txtKetQuaCDHA_DienTim.Text = Utility.sDbnull(_oSKTM.KetQuaCDHADt);
                txtKetQuaCDHA_Khac.Text = Utility.sDbnull(_oSKTM.KetQuaCDHAKhac);
                txtPhanUngThuoc.Text = Utility.sDbnull(_oSKTM.PhanUngThuoc);
                txtKhamGayMe.Text = Utility.sDbnull(_oSKTM.KhamGayMe);
                txtDuKien_PPPT.Text = Utility.sDbnull(_oSKTM.DuKienPPMo);
                dtpNgayDuKienMo.Value = _oSKTM.NgayDuKienMo != null ? (DateTime)_oSKTM.NgayDuKienMo : globalVariables.SysDate;

                _nMoney = Utility.Int32Dbnull(_oSKTM.TongChiPhi, 0);
                txtTongChiPhiDuKien.Text = Utility.sDbnull(_nMoney, "0");
                txtListMaPPPT.Text = Utility.sDbnull(_oSKTM.ListMaPPPT, string.Empty);

                txtDuKien_PPVC.Text = Utility.sDbnull(_oSKTM.DuKienPPVoCam);
                txtDuTruMau.Text = Utility.sDbnull(_oSKTM.DuTruMau);
                txtDuKien_KhoKhan.Text = Utility.sDbnull(_oSKTM.DuKienKhoKhan);
                txtChuTri.SetId(Utility.Int32Dbnull(_oSKTM.ChuTri, -1));
                txtBacSyTrongKhoa.Text = Utility.sDbnull(_oSKTM.CacBSTrongKhoa);
                txtTomTatYKien.Text = Utility.sDbnull(_oSKTM.TomTatYKien);
                txtKetLuan.Text = Utility.sDbnull(_oSKTM.KetLuan);
                radThuong.Checked = (bool)_oSKTM.ThuongOrCapCuu;
                radCapCuu.Checked = !radThuong.Checked;
                dtpNgayLapPhieu.Value = _oSKTM.NgayLapPhieu != null ? (DateTime)_oSKTM.NgayLapPhieu : globalVariables.SysDate;
                txtkipgayme._Text = Utility.sDbnull(_oSKTM.MotaKipGayme);
                txtkipmo._Text = Utility.sDbnull(_oSKTM.MotaKipPttt);
                //cbokip_gayme.CheckedValues = Utility.sDbnull(_oSKTM.KipGayme).Split(',').ToArray();
                //cbokip_pttt.CheckedValues = Utility.sDbnull(_oSKTM.KipPttt).Split(',').ToArray();
                //cbophudungcu.CheckedValues = Utility.sDbnull(_oSKTM.Phudungcu).Split(',').ToArray();

                Int16[] _x;
                if (!string.IsNullOrEmpty(Utility.sDbnull(_oSKTM.KipGayme)))
                {
                    int _id = Utility.sDbnull(_oSKTM.KipGayme).Split(',').Length;
                    _x = new Int16[_id];
                    _id = 0;
                    foreach (var item in Utility.sDbnull(_oSKTM.KipGayme).Split(','))
                    {
                        _x[_id] = Utility.Int16Dbnull(item);
                        _id++;
                    }
                    var newArray = Array.ConvertAll(_x, item => (object)item);
                    cbokip_gayme.CheckedValues = newArray;
                }

                if (!string.IsNullOrEmpty(Utility.sDbnull(_oSKTM.KipPttt)))
                {
                    int _id = Utility.sDbnull(_oSKTM.KipPttt).Split(',').Length;
                    _x = new Int16[_id];
                    _id = 0;
                    foreach (var item in Utility.sDbnull(_oSKTM.KipPttt).Split(','))
                    {
                        _x[_id] = Utility.Int16Dbnull(item);
                        _id++;
                    }
                    var newArray = Array.ConvertAll(_x, item => (object)item);
                    cbokip_pttt.CheckedValues = newArray;
                }

                if (!string.IsNullOrEmpty(Utility.sDbnull(_oSKTM.Phudungcu)))
                {
                    int _id = Utility.sDbnull(_oSKTM.Phudungcu).Split(',').Length;
                    _x = new Int16[_id];
                    _id = 0;
                    foreach (var item in Utility.sDbnull(_oSKTM.Phudungcu).Split(','))
                    {
                        _x[_id] = Utility.Int16Dbnull(item);
                        _id++;
                    }
                    var newArray = Array.ConvertAll(_x, item => (object)item);
                    cbophudungcu.CheckedValues = newArray;
                }
            }
            
        }

        void ClearControl()
        {
            try
            {
                txtID_Phieu.Text = "-1";
                txtChanDoanTruocMo.Text = QuanLyPhongMo.getChanDoan(_nPatient_ID, _sPatient_Code, Utility.Int32Dbnull(txtDepartment_ID.Text));
                txtBenhSu.Text = QuanLyPhongMo.getBenhSu(_nPatient_ID, _sPatient_Code, Utility.Int32Dbnull(_nAssignDetail_ID));
                txtTrieuChung.Text = QuanLyPhongMo.getTrieuChung(_nPatient_ID, _sPatient_Code, Utility.Int32Dbnull(_nAssignDetail_ID));

                //txtKetQuaXN_Mau.Text = QuanLyPhongMo.getKetQuaCLS(_nPatient_ID, _sPatient_Code);
                txtKetQuaXN_Mau.Text = string.Empty;
                txtKetQuaXN_MauDM.Text = string.Empty;
                txtKetQuaXN_MauSH.Text = string.Empty;
                txtKetQuaXN_Khac.Text = string.Empty;
                txtKetQuaXN_NT.Text = string.Empty;
                txtDuKien_PPPT.Text = string.Empty;
                txtDuKien_PPVC.Text = string.Empty;
                txtDuTruMau.Text = string.Empty;
                txtDuKien_KhoKhan.Text = string.Empty;
                txtChuTri.SetId(-1);
                txtBacSyTrongKhoa.Text = string.Empty;
                txtTomTatYKien.Text = string.Empty;
                txtKetLuan.Text = string.Empty;
                radThuong.Checked = true;
                radCapCuu.Checked = !radThuong.Checked;
                dtpNgayLapPhieu.Value = globalVariables.SysDate;

                cbokip_gayme.CheckedValues = null;
                cbokip_pttt.CheckedValues = null;
                cbophudungcu.CheckedValues = null;
             
                txtkipmo.setDefaultValue();
                txtkipgayme.setDefaultValue();
                PmPhieuKhamTruocMo _phieuKhamTruocMo = new Select().From(PmPhieuKhamTruocMo.Schema).Where(PmPhieuKhamTruocMo.Columns.IdPhongMo).IsEqualTo(_nIDPhongMo).ExecuteSingle<PmPhieuKhamTruocMo>();
                if (_phieuKhamTruocMo != null && Utility.Int32Dbnull(_phieuKhamTruocMo.IdPhieu, -1) > 0)
                {
                    txtChanDoanTruocMo.Text = Utility.sDbnull(_phieuKhamTruocMo.ChanDoanTruocMo, txtChanDoanTruocMo.Text);
                    //txtKetQuaXN_Mau.Text = Utility.sDbnull(_phieuKhamTruocMo.LuuY, txtKetQuaXN_Mau.Text);
                    txtDuKien_PPPT.Text = Utility.sDbnull(_phieuKhamTruocMo.DuKienPPMo, string.Empty);
                    txtDuKien_PPVC.Text = Utility.sDbnull(_phieuKhamTruocMo.DuKienTienMe, string.Empty);
                    txtDuKien_KhoKhan.Text = Utility.sDbnull(_phieuKhamTruocMo.DuKienKhoKhan, string.Empty);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                log.Error("Loi trong qua trinh lay thong tin phieu so ket truoc mo" + ex.ToString());
            }
        }
        void bindPhieuMau()
        {
            DataTable _colPhieuKhamMau = new Select().From(PmSoKetTruocMoMau.Schema).ExecuteDataSet().Tables[0];
            //cboPhieuMau.DataSource = _colPhieuKhamMau;
            //cboPhieuMau.ValueMember = PmSoKetTruocMoMau.IDKeysColumn.ColumnName;
            //cboPhieuMau.DisplayMember = PmSoKetTruocMoMau.TenSoKetTruocMoMauColumn.ColumnName;
            DataBinding.BindDataCombox(cboPhieuMau, _colPhieuKhamMau,
                PmSoKetTruocMoMau.Columns.IDKeys, PmSoKetTruocMoMau.Columns.TenSoKetTruocMoMau);
        }

        #region Phương pháp vô cảm
        void bindPhuongPhapVoCam()
        {
            DataTable _dtDmChung = new Select().From(DDmucChung.Schema)
                                            .Where(DDmucChung.Columns.Loai).IsEqualTo("PHUONGPHAP_VOCAM")
                                            .And(DDmucChung.Columns.Ten).IsNotEqualTo("")
                                            .ExecuteDataSet().Tables[0];

            AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
            try
            {
                foreach (DataRow dr in _dtDmChung.Rows)
                {
                    namesCollection.Add(dr[DDmucChung.Columns.Ten].ToString());
                }
                txtDuKien_PPVC.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtDuKien_PPVC.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtDuKien_PPVC.AutoCompleteCustomSource = namesCollection;
            }
            catch (Exception)
            {
                //throw;
            }
            txtDuKien_PPVC.Init();
            txtDuKien_PPVC.SetCode("-1");
        }
        void bindDanhsachBSPT()
        {
            DataTable _dtDmChung = new Select().From(DDmucChung.Schema)
                                            .Where(DDmucChung.Columns.Loai).IsEqualTo("SOKETMO_BS_PTTT")
                                            .And(DDmucChung.Columns.Ten).IsNotEqualTo("")
                                            .ExecuteDataSet().Tables[0];

            AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
            try
            {
                foreach (DataRow dr in _dtDmChung.Rows)
                {
                    namesCollection.Add(dr[DDmucChung.Columns.Ten].ToString());
                }
                txtkipmo.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtkipmo.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtkipmo.AutoCompleteCustomSource = namesCollection;
            }
            catch (Exception)
            {
                //throw;
            }
            txtkipmo.Init();
            txtkipmo.SetCode("-1");
        }
        void bindDanhsachBSGayMe()
        {
            DataTable _dtDmChung = new Select().From(DDmucChung.Schema)
                                            .Where(DDmucChung.Columns.Loai).IsEqualTo("SOKETMO_BS_GAYME")
                                            .And(DDmucChung.Columns.Ten).IsNotEqualTo("")
                                            .ExecuteDataSet().Tables[0];

            AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
            try
            {
                foreach (DataRow dr in _dtDmChung.Rows)
                {
                    namesCollection.Add(dr[DDmucChung.Columns.Ten].ToString());
                }
                txtkipgayme.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtkipgayme.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtkipgayme.AutoCompleteCustomSource = namesCollection;
            }
            catch (Exception)
            {
                //throw;
            }
            txtkipgayme.Init();
            txtkipgayme.SetCode("-1");
        }
        private void txtDuKien_PPVC_OnSaveAs()
        {
            if (Utility.DoTrim(txtDuKien_PPVC.Text) == "") return;
            var dmucDchung = new VietBaIT.HISLink.UI.ControlUtility.FORM_CHUNG.DMUC_DCHUNG(txtDuKien_PPVC.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txtDuKien_PPVC.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtDuKien_PPVC.myCode;
                txtDuKien_PPVC.Init();
                txtDuKien_PPVC.SetCode(oldCode);
                txtDuKien_PPVC.Focus();
            }
        }
        private void txtDuKien_PPVC_OnShowData()
        {
            var dmucDchung = new ControlUtility.FORM_CHUNG.DMUC_DCHUNG(txtDuKien_PPVC.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtDuKien_PPVC.myCode;
                txtDuKien_PPVC.Init();
                txtDuKien_PPVC.SetCode(oldCode);
                txtDuKien_PPVC.Focus();
            }
        }
        private void txtkipmo_OnSaveAs()
        {
            if (Utility.DoTrim(txtkipmo.Text) == "") return;
            var dmucDchung = new VietBaIT.HISLink.UI.ControlUtility.FORM_CHUNG.DMUC_DCHUNG(txtkipmo.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txtkipmo.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtkipmo.myCode;
                txtkipmo.Init();
                txtkipmo.SetCode(oldCode);
                txtkipmo.Focus();
            }
        }
        private void txtkipmo_OnShowData()
        {
            var dmucDchung = new ControlUtility.FORM_CHUNG.DMUC_DCHUNG(txtkipmo.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtkipmo.myCode;
                txtkipmo.Init();
                txtkipmo.SetCode(oldCode);
                txtkipmo.Focus();
            }
        }
        private void txtkipgayme_OnSaveAs()
        {
            if (Utility.DoTrim(txtkipgayme.Text) == "") return;
            var dmucDchung = new VietBaIT.HISLink.UI.ControlUtility.FORM_CHUNG.DMUC_DCHUNG(txtkipgayme.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txtkipgayme.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtkipgayme.myCode;
                txtkipgayme.Init();
                txtkipgayme.SetCode(oldCode);
                txtkipgayme.Focus();
            }
        }
        private void txtkipgayme_OnShowData()
        {
            var dmucDchung = new ControlUtility.FORM_CHUNG.DMUC_DCHUNG(txtkipgayme.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtkipgayme.myCode;
                txtkipgayme.Init();
                txtkipgayme.SetCode(oldCode);
                txtkipgayme.Focus();
            }
        }
        #endregion

        #region Dự kiến khó khăn
        void bindDuKienKhoKhan()
        {
            DataTable _dtDmChung = new Select().From(DDmucChung.Schema)
                                                .Where(DDmucChung.Columns.Loai).IsEqualTo("KHOKHAN_PTTT")
                                                .And(DDmucChung.Columns.Ten).IsNotEqualTo("")
                                                .ExecuteDataSet().Tables[0];

            AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();

            try
            {
                foreach (DataRow dr in _dtDmChung.Rows)
                {
                    namesCollection.Add(dr[DDmucChung.Columns.Ten].ToString());
                }

                txtDuKien_KhoKhan.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtDuKien_KhoKhan.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtDuKien_KhoKhan.AutoCompleteCustomSource = namesCollection;
            }
            catch (Exception)
            {
            }
            txtDuKien_KhoKhan.Init();
            txtDuKien_KhoKhan.SetCode("-1");
        }
        private void txtDuKien_KhoKhan_OnSaveAs()
        {
            if (Utility.DoTrim(txtDuKien_KhoKhan.Text) == "") return;
            var dmucDchung = new VietBaIT.HISLink.UI.ControlUtility.FORM_CHUNG.DMUC_DCHUNG(txtDuKien_KhoKhan.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txtDuKien_KhoKhan.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtDuKien_KhoKhan.myCode;
                txtDuKien_KhoKhan.Init();
                txtDuKien_KhoKhan.SetCode(oldCode);
                txtDuKien_KhoKhan.Focus();
            }
        }
        private void txtDuKien_KhoKhan_OnShowData()
        {
            var dmucDchung = new ControlUtility.FORM_CHUNG.DMUC_DCHUNG(txtDuKien_KhoKhan.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtDuKien_KhoKhan.myCode;
                txtDuKien_KhoKhan.Init();
                txtDuKien_KhoKhan.SetCode(oldCode);
                txtDuKien_KhoKhan.Focus();
            }
        }
        #endregion
        #endregion

        #region Buttons Click
        private void btnSoKetBoSung_Click(object sender, EventArgs e)
        {
            frmPhieuSoKetTruocMo frm = new frmPhieuSoKetTruocMo(1);
            frm._nIDPhongMo = _nIDPhongMo;
            frm.ShowDialog();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXemIn_Click(object sender, EventArgs e)
        {
            if (Utility.Int32Dbnull(txtID_Phieu.Text, -1) <= 0) return;
            PrintReport.SoKetTruocMo(Utility.Int32Dbnull(txtID_Phieu.Text, -1), "SƠ KẾT TRƯỚC MỔ");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.AcceptQuestion("Bạn chắc chắn muốn xóa phiếu sơ kết này?", "Xóa phiếu sơ kết trước mổ", true))
                {
                    SqlQuery _sql = new Select().From(PmSoKetTruocMo.Schema)
                                                .Where(PmSoKetTruocMo.Columns.IdPhieu).IsNotEqualTo(Utility.Int32Dbnull(txtID_Phieu.Text, -1))
                                                .And(PmSoKetTruocMo.Columns.SoKetBoSung).IsEqualTo(1);
                    if (Utility.Int32Dbnull(_oSKTM.SoKetBoSung, 0) == 0 && _sql.GetRecordCount() > 0)
                    {
                        if (Utility.AcceptQuestion("Bạn đang xóa phiếu sơ kết chính. /nBệnh nhân còn phiếu sơ kết bổ sung. /nChuyển phiếu bổ sung thành phiếu chính?", "Xóa phiếu sơ kết trước mổ", true))
                        {
                            PmSoKetTruocMo _oSKBS = _sql.ExecuteSingle<PmSoKetTruocMo>();
                            _oSKBS.SoKetBoSung = 0;
                            _oSKBS.IsLoaded = true;
                            _oSKBS.MarkOld();
                            _oSKBS.Save();
                            HISLinkLog.Log(this.Name, globalVariables.UserName, string.Format("Chuyển phiếu sơ kết bổ sung thành phiếu chính: Bệnh nhân: {0} - {1}; ID Phiếu: {2}; Ngày lập: {3}", _sPatient_Code, txtPatientName.Text, _oSKBS.IdPhieu, _oSKBS.NgayLapPhieu), action.Update, "UI");
                        }
                        new Delete().From(PmSoKetTruocMo.Schema).Where(PmSoKetTruocMo.Columns.IdPhieu).IsEqualTo(Utility.Int32Dbnull(txtID_Phieu.Text, -1)).Execute();
                        HISLinkLog.Log(this.Name, globalVariables.UserName, string.Format("Xóa phiếu sơ kết trước mổ: Bệnh nhân: {0} - {1}; ID Phiếu: {2}; Ngày lập: {3}", _sPatient_Code, txtPatientName.Text, txtID_Phieu.Text, dtpNgayLapPhieu.Value), action.Update, "UI");
                    }
                    else
                    {
                        new Delete().From(PmSoKetTruocMo.Schema).Where(PmSoKetTruocMo.Columns.IdPhieu).IsEqualTo(Utility.Int32Dbnull(txtID_Phieu.Text, -1)).Execute();
                        HISLinkLog.Log(this.Name, globalVariables.UserName, string.Format("Xóa phiếu sơ kết trước mổ: Bệnh nhân: {0} - {1}; ID Phiếu: {2}; Ngày lập: {3}", _sPatient_Code, txtPatientName.Text, txtID_Phieu.Text, dtpNgayLapPhieu.Value), action.Update, "UI");
                    }
                    GetInfo();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                log.Error("Loi trong qua trinh xoa thong tin phieu so ket truoc mo" + ex.ToString());
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                _oSKTM = PmSoKetTruocMo.FetchByID(Utility.Int32Dbnull(txtID_Phieu.Text, -1));

                if (_oSKTM == null) _oSKTM = new PmSoKetTruocMo();

                _oSKTM.IdPhongMo = _nIDPhongMo;
                _oSKTM.PatientId = _nPatient_ID;
                _oSKTM.PatientCode = _sPatient_Code;

                _oSKTM.ChanDoanTruocMo = Utility.sDbnull(txtChanDoanTruocMo.Text, string.Empty);
                _oSKTM.BenhSuVaDienBien = Utility.sDbnull(txtBenhSu.Text, string.Empty);
                _oSKTM.TrieuChung = Utility.sDbnull(txtTrieuChung.Text, string.Empty);
                _oSKTM.KetQuaXNHh = Utility.sDbnull(txtKetQuaXN_Mau.Text, string.Empty);
                _oSKTM.KetQuaXNSh = Utility.sDbnull(txtKetQuaXN_MauSH.Text, string.Empty);
                _oSKTM.KetQuaXNDm = Utility.sDbnull(txtKetQuaXN_MauDM.Text, string.Empty);
                _oSKTM.KetQuaXNNt = Utility.sDbnull(txtKetQuaXN_NT.Text, string.Empty);
                _oSKTM.KetQuaXNKhac = Utility.sDbnull(txtKetQuaXN_Khac.Text, string.Empty);
                _oSKTM.KetQuaCDHAXqtp = Utility.sDbnull(txtKetQuaCDHA_XQuangTP.Text, string.Empty);
                _oSKTM.KetQuaCDHAXq = Utility.sDbnull(txtKetQuaCDHA_XQuang.Text, string.Empty);
                _oSKTM.KetQuaCDHASa = Utility.sDbnull(txtKetQuaCDHA_SieuAm.Text, string.Empty);
                _oSKTM.KetQuaCDHANs = Utility.sDbnull(txtKetQuaCDHA_NoiSoi.Text, string.Empty);
                _oSKTM.KetQuaCDHADt = Utility.sDbnull(txtKetQuaCDHA_DienTim.Text, string.Empty);
                _oSKTM.KetQuaCDHAKhac = Utility.sDbnull(txtKetQuaCDHA_Khac.Text, string.Empty);
                _oSKTM.PhanUngThuoc = Utility.sDbnull(txtPhanUngThuoc.Text, string.Empty);
                _oSKTM.KhamGayMe = Utility.sDbnull(txtKhamGayMe.Text, string.Empty);

                _oSKTM.DuKienPPMo = Utility.sDbnull(txtDuKien_PPPT.Text, string.Empty);
                _oSKTM.TongChiPhi = Utility.DecimaltoDbnull(txtTongChiPhiDuKien.Text, 0);
                _oSKTM.ListMaPPPT = Utility.sDbnull(txtListMaPPPT.Text, string.Empty);

                _oSKTM.DuKienPPVoCam = Utility.sDbnull(txtDuKien_PPVC.Text, string.Empty);
                _oSKTM.DuTruMau = Utility.sDbnull(txtDuTruMau.Text, string.Empty);
                _oSKTM.DuKienKhoKhan = Utility.sDbnull(txtDuKien_KhoKhan.Text, string.Empty);
                _oSKTM.ChuTri = Utility.sDbnull(txtChuTri.MyID, string.Empty);
                _oSKTM.CacBSTrongKhoa = Utility.sDbnull(txtBacSyTrongKhoa.Text, string.Empty);
                _oSKTM.TomTatYKien = Utility.sDbnull(txtTomTatYKien.Text, string.Empty);
                _oSKTM.KetLuan = Utility.sDbnull(txtKetLuan.Text, string.Empty);
                _oSKTM.ThuongOrCapCuu = radThuong.Checked;
                _oSKTM.NgayLapPhieu = dtpNgayLapPhieu.Value;
                _oSKTM.NgayDuKienMo = dtpNgayDuKienMo.Value;
                _oSKTM.MotaKipGayme = txtkipgayme.Text;
                _oSKTM.MotaKipPttt = txtkipmo.Text;

                string selectValues = string.Empty;
                if (!string.IsNullOrEmpty(cbokip_gayme.Text))
                {
                    var query = (from chk in cbokip_gayme.CheckedValues.AsEnumerable() let x = Utility.sDbnull(chk) select x).ToArray();
                    if (query.Count() > 0) selectValues = string.Join(",", query);
                }
                _oSKTM.KipGayme = selectValues;

                selectValues = string.Empty;
                if (!string.IsNullOrEmpty(cbokip_pttt.Text))
                {
                    var query = (from chk in cbokip_pttt.CheckedValues.AsEnumerable() let x = Utility.sDbnull(chk) select x).ToArray();
                    if (query.Count() > 0) selectValues = string.Join(",", query);
                }
                _oSKTM.KipPttt = selectValues;

                selectValues = string.Empty;
                if (!string.IsNullOrEmpty(cbophudungcu.Text))
                {
                    var query = (from chk in cbophudungcu.CheckedValues.AsEnumerable() let x = Utility.sDbnull(chk) select x).ToArray();
                    if (query.Count() > 0) selectValues = string.Join(",", query);
                }
                _oSKTM.Phudungcu = selectValues;

                _oSKTM.AssignDetailId = _nAssignDetail_ID;

                if (Utility.Int32Dbnull(txtID_Phieu.Text, -1) <= 0)
                {
                    _oSKTM.NgayTao = globalVariables.SysDate;
                    _oSKTM.NguoiTao = globalVariables.UserName;
                    _oSKTM.IpMayTao = globalVariables.IpAddress;
                    _oSKTM.IpMacTao = globalVariables.IpMacAddress;
                    _oSKTM.IsLoaded = false;
                    _oSKTM.IsNew = true;
                    _oSKTM.Save();
                }
                else
                {
                    _oSKTM.NgaySua = globalVariables.SysDate;
                    _oSKTM.NguoiSua = globalVariables.UserName;
                    _oSKTM.IpMaySua = globalVariables.IpAddress;
                    _oSKTM.IpMacSua = globalVariables.IpMacAddress;
                    _oSKTM.IsNew = false;
                    _oSKTM.IsLoaded = true;
                    _oSKTM.MarkOld();
                    _oSKTM.Save();
                }
                HISLinkLog.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật phiếu sơ kết trước mổ: Bệnh nhân: {0} - {1}; ID Phiếu: {2}; Ngày lập: {3}", _sPatient_Code, txtPatientName.Text, txtID_Phieu.Text, dtpNgayLapPhieu.Value), action.Update, "UI");
                GetInfo();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                log.Error("Loi trong qua trinh luu thong tin phieu so ket truoc mo" + ex.ToString());
            }
        }


        private void btnLuuPhieuMau_Click(object sender, EventArgs e)
        {
            try
            {
                PmSoKetTruocMoMau _oSKTM_Mau = PmSoKetTruocMoMau.FetchByID(Utility.Int32Dbnull(cboPhieuMau.SelectedValue, -1));

                if (_oSKTM_Mau == null) _oSKTM_Mau = new PmSoKetTruocMoMau();
                _oSKTM_Mau.TenSoKetTruocMoMau = cboPhieuMau.Text;
                _oSKTM_Mau.ChanDoanTruocMo = Utility.sDbnull(txtChanDoanTruocMo.Text, string.Empty);
                _oSKTM_Mau.BenhSuVaDienBien = Utility.sDbnull(txtBenhSu.Text, string.Empty);
                _oSKTM_Mau.TrieuChung = Utility.sDbnull(txtTrieuChung.Text, string.Empty);
                _oSKTM_Mau.KetQuaXNHh = Utility.sDbnull(txtKetQuaXN_Mau.Text, string.Empty);
                _oSKTM_Mau.KetQuaXNNt = Utility.sDbnull(txtKetQuaXN_NT.Text, string.Empty);
                _oSKTM_Mau.KetQuaXNKhac = Utility.sDbnull(txtKetQuaXN_Khac.Text, string.Empty);
                _oSKTM_Mau.KetQuaCDHAXqtp = Utility.sDbnull(txtKetQuaCDHA_XQuangTP.Text, string.Empty);
                _oSKTM_Mau.KetQuaCDHAXq = Utility.sDbnull(txtKetQuaCDHA_XQuang.Text, string.Empty);
                _oSKTM_Mau.KetQuaCDHASa = Utility.sDbnull(txtKetQuaCDHA_SieuAm.Text, string.Empty);
                _oSKTM_Mau.KetQuaCDHANs = Utility.sDbnull(txtKetQuaCDHA_NoiSoi.Text, string.Empty);
                _oSKTM_Mau.KetQuaCDHADt = Utility.sDbnull(txtKetQuaCDHA_DienTim.Text, string.Empty);
                _oSKTM_Mau.KetQuaCDHAKhac = Utility.sDbnull(txtKetQuaCDHA_Khac.Text, string.Empty);
                _oSKTM_Mau.PhanUngThuoc = Utility.sDbnull(txtPhanUngThuoc.Text, string.Empty);
                _oSKTM_Mau.KhamGayMe = Utility.sDbnull(txtKhamGayMe.Text, string.Empty);

                _oSKTM_Mau.DuKienPPMo = Utility.sDbnull(txtDuKien_PPPT.Text, string.Empty);
                _oSKTM_Mau.TongChiPhi = Utility.DecimaltoDbnull(txtTongChiPhiDuKien.Text, 0);
                _oSKTM_Mau.ListMaPPPT = Utility.sDbnull(txtListMaPPPT.Text, string.Empty);

                _oSKTM_Mau.DuKienPPVoCam = Utility.sDbnull(txtDuKien_PPVC.Text, string.Empty);
                _oSKTM_Mau.DuTruMau = Utility.sDbnull(txtDuTruMau.Text, string.Empty);
                _oSKTM_Mau.DuKienKhoKhan = Utility.sDbnull(txtDuKien_KhoKhan.Text, string.Empty);
                _oSKTM_Mau.ChuTri = Utility.sDbnull(txtChuTri.MyID, string.Empty);
                _oSKTM_Mau.CacBSTrongKhoa = Utility.sDbnull(txtBacSyTrongKhoa.Text, string.Empty);
                _oSKTM_Mau.TomTatYKien = Utility.sDbnull(txtTomTatYKien.Text, string.Empty);
                _oSKTM_Mau.KetLuan = Utility.sDbnull(txtKetLuan.Text, string.Empty);
                //_oSKTM.ThuongOrCapCuu = radThuong.Checked;
                //_oSKTM.NgayLapPhieu = dtpNgayLapPhieu.Value;
                string selectValues = string.Empty;
                if (!string.IsNullOrEmpty(cbokip_gayme.Text))
                {
                    var query = (from chk in cbokip_gayme.CheckedValues.AsEnumerable() let x = Utility.sDbnull(chk) select x).ToArray();
                    if (query.Count() > 0) selectValues = string.Join(",", query);
                }
                _oSKTM_Mau.KipGayme = selectValues;

                selectValues = string.Empty;
                if (!string.IsNullOrEmpty(cbokip_pttt.Text))
                {
                    var query = (from chk in cbokip_pttt.CheckedValues.AsEnumerable() let x = Utility.sDbnull(chk) select x).ToArray();
                    if (query.Count() > 0) selectValues = string.Join(",", query);
                }
                _oSKTM_Mau.KipPttt = selectValues;

                selectValues = string.Empty;
                if (!string.IsNullOrEmpty(cbophudungcu.Text))
                {
                    var query = (from chk in cbophudungcu.CheckedValues.AsEnumerable() let x = Utility.sDbnull(chk) select x).ToArray();
                    if (query.Count() > 0) selectValues = string.Join(",", query);
                }
                _oSKTM_Mau.Phudungcu = selectValues;

                if (Utility.Int32Dbnull(_oSKTM_Mau.IDKeys,0) <=0)
                {
                    _oSKTM_Mau.NgayTao = globalVariables.SysDate;
                    _oSKTM_Mau.NguoiTao = globalVariables.UserName;
                    _oSKTM_Mau.IpMayTao = globalVariables.IpAddress;
                    _oSKTM_Mau.IpMacTao = globalVariables.IpMacAddress;
                    _oSKTM_Mau.IsLoaded = false;
                    _oSKTM_Mau.IsNew = true;
                    _oSKTM_Mau.Save();
                }
                else
                {
                    _oSKTM_Mau.NgaySua = globalVariables.SysDate;
                    _oSKTM_Mau.NguoiSua = globalVariables.UserName;
                    _oSKTM_Mau.IpMaySua = globalVariables.IpAddress;
                    _oSKTM_Mau.IpMacSua = globalVariables.IpMacAddress;
                    _oSKTM_Mau.IsNew = false;
                    _oSKTM_Mau.IsLoaded = true;
                    _oSKTM_Mau.MarkOld();
                    _oSKTM_Mau.Save();
                }

                Utility.ShowMsg("Bạn lưu phiếu mẫu thành công");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void btnXoaPhieuMau_Click(object sender, EventArgs e)
        {
            if (Utility.Int32Dbnull(cboPhieuMau.SelectedValue) > 0)
            {
                if (Utility.AcceptQuestion("Bạn chắc chắn muốn xóa phiếu mẫu đang chọn?", "Xóa phiếu mẫu", true))
                {
                    PmSoKetTruocMoMau _oPhieuMau = new Select().From(PmSoKetTruocMoMau.Schema)
                                                                    .Where(PmSoKetTruocMoMau.Columns.IDKeys).IsEqualTo(Utility.Int32Dbnull(cboPhieuMau.SelectedValue))
                                                                    .ExecuteSingle<PmSoKetTruocMoMau>();

                    if (Utility.sDbnull(_oPhieuMau.NguoiTao, string.Empty) == globalVariables.UserName)
                    {
                        new Delete().From(PmSoKetTruocMoMau.Schema).Where(PmSoKetTruocMoMau.Columns.IDKeys).IsEqualTo(Utility.Int32Dbnull(cboPhieuMau.SelectedValue)).Execute();
                        HISLinkLog.Log(this.Name, globalVariables.UserName, string.Format("Xóa mẫu phiếu khám trước mổ: {0}", Utility.sDbnull(_oPhieuMau.TenSoKetTruocMoMau, string.Empty)), action.Delete, "UI");
                    }
                    else
                    {
                        Utility.ShowMsg(string.Format("Phiếu mẫu này được tạo bởi tài khoản: {0}. \nBạn không thể xóa.", Utility.sDbnull(_oPhieuMau.NguoiTao, string.Empty)));
                    }
                }
            }
        }

        private void btnLayPPMau_Click(object sender, EventArgs e)
        {
            DanhMuc.frmPhuongPhapPTTT frmPPPT = new DanhMuc.frmPhuongPhapPTTT();
            frmPPPT._nObjectTypeID = Utility.Int32Dbnull(txtObjectType_ID.Text, 1);
            frmPPPT._nServiceID = Utility.Int32Dbnull(_oQLPM.ServiceId, -1);
            frmPPPT._nServiceDetailID = Utility.Int32Dbnull(_oQLPM.ServiceDetailId, -1);

            frmPPPT.ShowDialog();

            txtDuKien_PPPT.Text = frmPPPT._sResultText;
            txtListMaPPPT.Text = frmPPPT._sResultCode;
            _nMoney = frmPPPT._nMoney;
            txtTongChiPhiDuKien.Text = Utility.sDbnull(_nMoney, "0");
        }
        #endregion

        #region ComboBoxs Selected Index Changed
        private void cboPhieuMau_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PmSoKetTruocMoMau _oSKTM_Mau = new Select().From(PmSoKetTruocMoMau.Schema)
                                        .Where(PmSoKetTruocMoMau.Columns.IDKeys).IsEqualTo(Utility.Int32Dbnull(cboPhieuMau.SelectedValue)).ExecuteSingle<PmSoKetTruocMoMau>();

                if (_oSKTM_Mau != null && Utility.Int32Dbnull(_oSKTM_Mau.IDKeys, -1) > 0)
                {
                    //txtChanDoanTruocMo.Text = Utility.sDbnull(_oSKTM_Mau.ChanDoanTruocMo);
                    //txtBenhSu.Text = Utility.sDbnull(_oSKTM_Mau.BenhSuVaDienBien);
                    ////txtTrieuChung.Text = Utility.sDbnull(_oSKTM_Mau.TrieuChung);
                    txtKetQuaXN_Mau.Text = Utility.sDbnull(_oSKTM_Mau.KetQuaXNHh);
                    txtKetQuaXN_NT.Text = Utility.sDbnull(_oSKTM_Mau.KetQuaXNNt);
                    txtKetQuaXN_Khac.Text = Utility.sDbnull(_oSKTM_Mau.KetQuaXNKhac);
                    txtKetQuaCDHA_XQuangTP.Text = Utility.sDbnull(_oSKTM_Mau.KetQuaCDHAXqtp);
                    txtKetQuaCDHA_XQuang.Text = Utility.sDbnull(_oSKTM_Mau.KetQuaCDHAXq);
                    txtKetQuaCDHA_SieuAm.Text = Utility.sDbnull(_oSKTM_Mau.KetQuaCDHASa);
                    txtKetQuaCDHA_NoiSoi.Text = Utility.sDbnull(_oSKTM_Mau.KetQuaCDHANs);
                    txtKetQuaCDHA_DienTim.Text = Utility.sDbnull(_oSKTM_Mau.KetQuaCDHADt);
                    txtKetQuaCDHA_Khac.Text = Utility.sDbnull(_oSKTM_Mau.KetQuaCDHAKhac);
                    txtPhanUngThuoc.Text = Utility.sDbnull(_oSKTM_Mau.PhanUngThuoc);
                    txtKhamGayMe.Text = Utility.sDbnull(_oSKTM_Mau.KhamGayMe);
                    txtDuKien_PPPT.Text = Utility.sDbnull(_oSKTM_Mau.DuKienPPMo);
                    txtDuKien_PPVC.Text = Utility.sDbnull(_oSKTM_Mau.DuKienPPVoCam);
                    txtDuTruMau.Text = Utility.sDbnull(_oSKTM_Mau.DuTruMau);
                    txtDuKien_KhoKhan.Text = Utility.sDbnull(_oSKTM_Mau.DuKienKhoKhan);
                    txtChuTri.SetId(Utility.Int32Dbnull(_oSKTM_Mau.ChuTri, -1));
                    txtBacSyTrongKhoa.Text = Utility.sDbnull(_oSKTM_Mau.CacBSTrongKhoa);
                    txtTomTatYKien.Text = Utility.sDbnull(_oSKTM_Mau.TomTatYKien);
                    txtKetLuan.Text = Utility.sDbnull(_oSKTM_Mau.KetLuan);

                    _nMoney = Utility.Int32Dbnull(_oSKTM_Mau.TongChiPhi, 0);
                    txtTongChiPhiDuKien.Text = Utility.sDbnull(_nMoney, "0");
                    txtListMaPPPT.Text = Utility.sDbnull(_oSKTM_Mau.ListMaPPPT, string.Empty);

                    //cbokip_gayme.CheckedValues = Utility.sDbnull(_oSKTM.KipGayme).Split(',').ToArray();
                    //cbokip_pttt.CheckedValues = Utility.sDbnull(_oSKTM.KipPttt).Split(',').ToArray();
                    //cbophudungcu.CheckedValues = Utility.sDbnull(_oSKTM.Phudungcu).Split(',').ToArray();

                    Int16[] _x;
                    if (!string.IsNullOrEmpty(Utility.sDbnull(_oSKTM.KipGayme)))
                    {
                        int _id = Utility.sDbnull(_oSKTM.KipGayme).Split(',').Length;
                        _x = new Int16[_id];
                        _id = 0;
                        foreach (var item in Utility.sDbnull(_oSKTM.KipGayme).Split(','))
                        {
                            _x[_id] = Utility.Int16Dbnull(item);
                            _id++;
                        }
                        var newArray = Array.ConvertAll(_x, item => (object)item);
                        cbokip_gayme.CheckedValues = newArray;
                    }

                    if (!string.IsNullOrEmpty(Utility.sDbnull(_oSKTM.KipPttt)))
                    {
                        int _id = Utility.sDbnull(_oSKTM.KipPttt).Split(',').Length;
                        _x = new Int16[_id];
                        _id = 0;
                        foreach (var item in Utility.sDbnull(_oSKTM.KipPttt).Split(','))
                        {
                            _x[_id] = Utility.Int16Dbnull(item);
                            _id++;
                        }
                        var newArray = Array.ConvertAll(_x, item => (object)item);
                        cbokip_pttt.CheckedValues = newArray;
                    }

                    if (!string.IsNullOrEmpty(Utility.sDbnull(_oSKTM.Phudungcu)))
                    {
                        int _id = Utility.sDbnull(_oSKTM.Phudungcu).Split(',').Length;
                        _x = new Int16[_id];
                        _id = 0;
                        foreach (var item in Utility.sDbnull(_oSKTM.Phudungcu).Split(','))
                        {
                            _x[_id] = Utility.Int16Dbnull(item);
                            _id++;
                        }
                        var newArray = Array.ConvertAll(_x, item => (object)item);
                        cbophudungcu.CheckedValues = newArray;
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        #endregion

        private void btnLayKQXN_Click(object sender, EventArgs e)
        {
            frm_LichSuCLS_SingleExam _frm = new frm_LichSuCLS_SingleExam();
            _frm._nPatientID = _nPatient_ID;
            _frm._sPatientCode = _sPatient_Code;
            _frm._nServiceTypeID = 1;
            _frm._sServiceCode = "-1";
            _frm.ShowDialog();

            if(_frm.DialogResult == DialogResult.OK) txtKetQuaXN_Mau.Text = _frm._sReturn;
        }
        private void btnLayKQXN_NT_Click(object sender, EventArgs e)
        {
            frm_LichSuCLS_SingleExam _frm = new frm_LichSuCLS_SingleExam();
            _frm._nPatientID = _nPatient_ID;
            _frm._sPatientCode = _sPatient_Code;
            _frm._nServiceTypeID = 1;
            _frm._sServiceCode = "NT";
            _frm.ShowDialog();

            if (_frm.DialogResult == DialogResult.OK) txtKetQuaXN_NT.Text = _frm._sReturn;
        }

        private void btnLayKQXN_Khac_Click(object sender, EventArgs e)
        {
            frm_LichSuCLS_SingleExam _frm = new frm_LichSuCLS_SingleExam();
            _frm._nPatientID = _nPatient_ID;
            _frm._sPatientCode = _sPatient_Code;
            _frm._nServiceTypeID = 1;
            _frm._sServiceCode = "-1";
            _frm.ShowDialog();

            if (_frm.DialogResult == DialogResult.OK) txtKetQuaXN_Khac.Text = _frm._sReturn;
        }

        private void btnLayKQCDHA_XQuangTP_Click(object sender, EventArgs e)
        {
            frm_LichSuCLS_SingleExam _frm = new frm_LichSuCLS_SingleExam();
            _frm._nPatientID = _nPatient_ID;
            _frm._sPatientCode = _sPatient_Code;
            _frm._nServiceTypeID = 2;
            _frm._sServiceCode = "XQ";
            _frm.ShowDialog();

            if (_frm.DialogResult == DialogResult.OK) txtKetQuaCDHA_XQuangTP.Text = _frm._sReturn;
        }

        private void btnLayKQCDHA_XQuang_Click(object sender, EventArgs e)
        {
            frm_LichSuCLS_SingleExam _frm = new frm_LichSuCLS_SingleExam();
            _frm._nPatientID = _nPatient_ID;
            _frm._sPatientCode = _sPatient_Code;
            _frm._nServiceTypeID = 2;
            _frm._sServiceCode = "XQ";
            _frm.ShowDialog();

            if (_frm.DialogResult == DialogResult.OK) txtKetQuaCDHA_XQuang.Text = _frm._sReturn;
        }

        private void btnLayKQCDHA_SA_Click(object sender, EventArgs e)
        {
            frm_LichSuCLS_SingleExam _frm = new frm_LichSuCLS_SingleExam();
            _frm._nPatientID = _nPatient_ID;
            _frm._sPatientCode = _sPatient_Code;
            _frm._nServiceTypeID = 2;
            _frm._sServiceCode = "SA";
            _frm.ShowDialog();

            if (_frm.DialogResult == DialogResult.OK) txtKetQuaCDHA_SieuAm.Text = _frm._sReturn;
        }

        private void btnLayKQCDHA_NS_Click(object sender, EventArgs e)
        {
            frm_LichSuCLS_SingleExam _frm = new frm_LichSuCLS_SingleExam();
            _frm._nPatientID = _nPatient_ID;
            _frm._sPatientCode = _sPatient_Code;
            _frm._nServiceTypeID = 2;
            _frm._sServiceCode = "NS";
            _frm.ShowDialog();

            if (_frm.DialogResult == DialogResult.OK) txtKetQuaCDHA_NoiSoi.Text = _frm._sReturn;
        }

        private void btnLayKQCDHA_DT_Click(object sender, EventArgs e)
        {
            frm_LichSuCLS_SingleExam _frm = new frm_LichSuCLS_SingleExam();
            _frm._nPatientID = _nPatient_ID;
            _frm._sPatientCode = _sPatient_Code;
            _frm._nServiceTypeID = 2;
            _frm._sServiceCode = "DT";
            _frm.ShowDialog();

            if (_frm.DialogResult == DialogResult.OK) txtKetQuaCDHA_DienTim.Text = _frm._sReturn;
        }

        private void btnLayKQCDHA_Khac_Click(object sender, EventArgs e)
        {
            frm_LichSuCLS_SingleExam _frm = new frm_LichSuCLS_SingleExam();
            _frm._nPatientID = _nPatient_ID;
            _frm._sPatientCode = _sPatient_Code;
            _frm._nServiceTypeID = 2;
            _frm._sServiceCode = "-1";
            _frm.ShowDialog();

            if (_frm.DialogResult == DialogResult.OK) txtKetQuaCDHA_Khac.Text = _frm._sReturn;
        }

        private void btnLayKQXN_Mau_SH_Click(object sender, EventArgs e)
        {
            frm_LichSuCLS_SingleExam _frm = new frm_LichSuCLS_SingleExam();
            _frm._nPatientID = _nPatient_ID;
            _frm._sPatientCode = _sPatient_Code;
            _frm._nServiceTypeID = 1;
            _frm._sServiceCode = "-1";
            _frm.ShowDialog();

            if (_frm.DialogResult == DialogResult.OK) txtKetQuaXN_MauSH.Text = _frm._sReturn;
        }

        private void btnLayKQXN_Mau_DongMau_Click(object sender, EventArgs e)
        {
            frm_LichSuCLS_SingleExam _frm = new frm_LichSuCLS_SingleExam();
            _frm._nPatientID = _nPatient_ID;
            _frm._sPatientCode = _sPatient_Code;
            _frm._nServiceTypeID = 1;
            _frm._sServiceCode = "-1";
            _frm.ShowDialog();

            if (_frm.DialogResult == DialogResult.OK) txtKetQuaXN_MauDM.Text = _frm._sReturn;
        }
    }
}
