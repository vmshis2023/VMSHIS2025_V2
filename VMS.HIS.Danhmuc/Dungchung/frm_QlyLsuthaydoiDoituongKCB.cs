using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Transactions;
using System.Windows.Forms;
using Janus.Windows.CalendarCombo;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Janus.Windows.UI.StatusBar;
using SubSonic;
using SubSonic.Sugar;
using VNS.Libs;
using VNS.HIS.NGHIEPVU;
using VNS.HIS.DAL;
using TextAlignment = Janus.Windows.GridEX.TextAlignment;
using TriState = Janus.Windows.GridEX.TriState;
using VNS.Properties;
using CrystalDecisions.CrystalReports.Engine;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.Classes;
using VNS.HIS.UI.THANHTOAN;
using VNS.HIS.UI.DANHMUC;
using VNS.Libs.AppUI;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.HIS.UI.NGOAITRU;

namespace VNS.HIS.UI.Forms.Dungchung
{
    public partial class frm_QlyLsuthaydoiDoituongKCB : Form
    {
        public delegate void OnActionSuccess();
        public event OnActionSuccess _OnActionSuccess;
        NoitruPhanbuonggiuong objBuonggiuong = null;
        NoitruPhanbuonggiuongCollection LstNoitruPhanbuonggiuong = new NoitruPhanbuonggiuongCollection();
        KCB_DANGKY _KCB_DANGKY = new KCB_DANGKY();
        KCB_QMS _KCB_QMS = new KCB_QMS();
        DMUC_CHUNG _DMUC_CHUNG = new DMUC_CHUNG();
        private readonly AutoCompleteStringCollection namesCollectionThanhPho = new AutoCompleteStringCollection();
        private readonly string strSaveandprintPath = Application.StartupPath + @"\CAUHINH\SaveAndPrintConfig.txt";

        private readonly string strSaveandprintPath1 = Application.StartupPath +
                                                       @"\CAUHINH\DefaultPrinter_PhieuHoaSinh.txt";

        private string MA_DTUONG = "DV";
        private string SoBHYT = "";
        private string TrongGio = "";
        public bool m_blnCancel;
        private bool b_HasLoaded;
        private bool AllowTextChanged=true;
        private bool AllowGridSelecttionChanged = true;
        private string _rowFilter = "1=1";
        private bool b_NhapNamSinh;
        private bool hasjustpressBACKKey;
        private bool isAutoFinding;
        bool m_blnHasJustInsert = false;
        public action m_enAct = action.FirstOrFinished;
        //private frm_ScreenSoKham _QMSScreen;
        public DataTable m_dtData = new DataTable();
        string m_strMaluotkham = "";//Lưu giá trị patientcode khi cập nhật để người dùng ko được phép gõ Patient_code lung tung
        KcbDanhsachBenhnhan objBenhnhan = null;
        DataRow newItem = null;
        public frm_QlyLsuthaydoiDoituongKCB()
        {
            InitializeComponent();
            InitEvents();
            txtTEN_BN.CharacterCasing = globalVariables.CHARACTERCASING == 0
                                            ? CharacterCasing.Normal
                                            : CharacterCasing.Upper;
            dtFromDate.Value = dtToDate.Value = globalVariables.SysDate;
            ucBHYT1.Init();
            CauHinhKCB();
        }

        void InitEvents()
        {
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_QlyLsuthaydoiDoituongKCB_FormClosing);
            this.Load += new System.EventHandler(this.frm_QlyLsuthaydoiDoituongKCB_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_QlyLsuthaydoiDoituongKCB_KeyDown);
            txtMaBN.KeyDown += new KeyEventHandler(txtMaBN_KeyDown);
            txtMaLankham.KeyDown += new KeyEventHandler(txtMaLankham_KeyDown);
            dtpBOD.TextChanged += dtpBOD_TextChanged;
            txtTEN_BN.LostFocus += txtTEN_BN_LostFocus;
            cboPatientSex.SelectedIndex = 0;
            cmdExit.Click +=cmdExit_Click;
            cmdSave.Click += new System.EventHandler(cmdSave_Click);
            chkChuyenVien.CheckedChanged += new EventHandler(chkChuyenVien_CheckedChanged);
            cboDoituongKCB.SelectedIndexChanged += new EventHandler(cboDoituongKCB_SelectedIndexChanged);
            cmdGetBV.Click += new EventHandler(cmdGetBV_Click);
            ucBHYT1._OnFindPatientByMatheBHYT += ucBHYT1__OnFindPatientByMatheBHYT;

            cmdthemmoi.Click += new EventHandler(cmdthemmoi_Click);
            cmdSua.Click += new EventHandler(cmdSua_Click);
            cmdxoa.Click += new EventHandler(cmdxoa_Click);
            cmdHuy.Click += new EventHandler(cmdHuy_Click);
            cmdGhi.Click += new EventHandler(cmdGhi_Click);

            grdList.SelectionChanged += grdList_SelectionChanged;
            grdList.MouseDoubleClick += grdList_MouseDoubleClick;
        }

        void grdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            grdList_SelectionChanged(sender, new EventArgs());
        }
        void cmdGhi_Click(object sender, EventArgs e)
        {
            try
            {
                cmdGhi.Enabled = false;
                PerformAction();
                
            }
            catch
            {
            }
            finally
            {
                cmdGhi.Enabled = true;
            }
        }
       
        void grdList_SelectionChanged(object sender, EventArgs e)
        {
            if (!AllowGridSelecttionChanged || !Utility.isValidGrid(grdList)) return;
            try
            {
                newItem = Utility.getCurrentDataRow(grdList);
                _MaDoituongKcb = Utility.sDbnull(newItem[KcbLichsuDoituongKcb.Columns.MaDoituongKcb]);
                AllowTextChanged = false;
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _MaDoituongKcb);
                AllowTextChanged = true;
                ucBHYT1.SetValues(newItem);
                dtFromDate.Text = Utility.sDbnull(newItem["sngay_hieuluc"]);
                dtToDate.Text = Utility.sDbnull(newItem["sngay_hethieuluc"]);
            }
            catch (Exception ex)
            {
                
            }
        }
        void cmdHuy_Click(object sender, EventArgs e)
        {
            m_enAct = action.FirstOrFinished;
            SetControlStatus();
        }
        List<long> lstIDDelete = new List<long>();
        void cmdxoa_Click(object sender, EventArgs e)
        {
            XoaLsu();
            m_enAct = action.FirstOrFinished;
            SetControlStatus();
        }
        void XoaLsu()
        {
            if (!Utility.isValidGrid(grdList) || newItem==null)
            {
                Utility.ShowMsg("Bạn cần chọn ít nhất một dòng dữ liệu để thực hiện xóa");
                return;
            }
            if (m_dtData.Rows.Count == 1)
            {
                Utility.ShowMsg("Đây là dòng dữ liệu cuối cùng nên bạn chỉ có thể thay đổi và không được phép xóa");
                return;
            }
            string _ask = string.Format("Bạn có chắc chắn muốn xóa dòng đối tượng {0} có hiệu lực từ ngày {1} đến ngày {2} hay không?", Utility.sDbnull(newItem["ten_doituong_kcb"]), Utility.sDbnull(newItem["sngay_hieuluc"]), Utility.sDbnull(newItem["sngay_hethieuluc"]));
            if (!Utility.AcceptQuestion(_ask,"Xác nhận xóa",true))
            {
                return;
            }
            DataRow[] arrDr = m_dtData.Select(KcbLichsuDoituongKcb.Columns.IdLichsuDoituongKcb + "=" + Utility.sDbnull(newItem[KcbLichsuDoituongKcb.Columns.IdLichsuDoituongKcb]));
            if (arrDr.Length > 0)
            {
                lstIDDelete.Add(Utility.Int64Dbnull(newItem[KcbLichsuDoituongKcb.Columns.IdLichsuDoituongKcb]));
                m_dtData.Rows.Remove(arrDr[0]);
                m_dtData.AcceptChanges();
            }
        }
        void cmdSua_Click(object sender, EventArgs e)
        {
            m_enAct = action.Update;
            SetControlStatus();
        }
        public void Themmoi()
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn cần chọn bệnh nhân cần nộp tiền tạm ứng");
                return;
            }
            if (Utility.Byte2Bool(objLuotkham.TthaiThopNoitru) && objLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã hoàn ứng và ra viện nên bạn không thể nộp thêm tiền tạm ứng");
                return;
            }
            m_enAct = action.Insert;
            SetControlStatus();
        }
        private void SetControlStatus()
        {
            try
            {
                grdList.Enabled = false;
                AllowGridSelecttionChanged = false;
                switch (m_enAct)
                {
                    case action.Insert:
                        //--------------------------------------------------------------
                        //Thiết lập trạng thái các nút Insert, Update, Delete...
                        //Không cho phép nhấn Insert, Update,Delete
                        cmdthemmoi.Enabled = false;
                        cmdSua.Enabled = false;
                        cmdxoa.Enabled = false;
                        cmdGhi.Enabled = true;
                        cmdHuy.Enabled = true;
                        cmdGhi.BringToFront();
                        cmdHuy.BringToFront();
                        cboDoituongKCB.Enabled = true;
                        dtFromDate.Enabled = true;
                        dtToDate.Enabled = true;
                        ChangeObjectRegion();
                        ClearControl();
                        cboDoituongKCB.Focus();
                        //--------------------------------------------------------------
                        //Không cho phép chọn trên lưới dữ liệu được fill vào các Control
                        AllowGridSelecttionChanged = false;
                        //Tự động Focus đến mục ID để người dùng nhập liệu

                        break;
                    case action.Update:

                        //--------------------------------------------------------------
                        //Thiết lập trạng thái các nút Insert, Update, Delete...
                        //Không cho phép nhấn Insert, Update,Delete
                        cmdthemmoi.Enabled = false;
                        cmdSua.Enabled = false;
                        cmdxoa.Enabled = false;
                        cmdGhi.Enabled = true;
                        cmdHuy.Enabled = true;
                        cmdGhi.BringToFront();
                        cmdHuy.BringToFront();
                        cmdHuy.Text = "Hủy";

                        cboDoituongKCB.Enabled = true;
                        dtFromDate.Enabled = true;
                        dtToDate.Enabled = true;
                        ChangeObjectRegion();
                        cboDoituongKCB.Focus();
                        //--------------------------------------------------------------
                        //Không cho phép chọn trên lưới dữ liệu được fill vào các Control
                        AllowGridSelecttionChanged = false;
                        //Tự động Focus đến mục Position để người dùng nhập liệu

                        break;
                    case action.FirstOrFinished://Hủy hoặc trạng thái ban đầu khi mới hiển thị Form
                        grdList.Enabled = true;
                        AllowGridSelecttionChanged = true;

                        cboDoituongKCB.Enabled = false;
                        dtFromDate.Enabled = false;
                        dtToDate.Enabled = false;
                        ucBHYT1.IsBHYT = false;
                        //--------------------------------------------------------------
                        //Thiết lập trạng thái các nút Insert, Update, Delete...
                        //Sau khi nhấn Ghi thành công hoặc Hủy thao tác thì quay về trạng thái ban đầu
                        //Cho phép thêm mới
                        cmdthemmoi.Enabled = true;
                        cmdSua.Enabled = Utility.isValidGrid(grdList);
                        cmdxoa.Enabled = Utility.isValidGrid(grdList);

                        cmdGhi.Enabled = false;
                        cmdHuy.Enabled = false;
                        cmdGhi.SendToBack();
                        cmdHuy.SendToBack();
                        //--------------------------------------------------------------
                        //Cho phép chọn trên lưới dữ liệu được fill vào các Control
                        AllowGridSelecttionChanged = true;
                        //Tự động chọn dòng hiện tại trên lưới để hiển thị lại trên Control
                        grdList_SelectionChanged(grdList, new EventArgs());
                        //Tự động Focus đến nút thêm mới? 
                        cmdthemmoi.Focus();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                grdList.Enabled = true;
            }
        }
        void cmdthemmoi_Click(object sender, EventArgs e)
        {
            Themmoi();
        }
        void ucBHYT1__OnFindPatientByMatheBHYT(string matheBHYT)
        {
           
        }
        void dtpBOD_TextChanged(object sender, EventArgs e)
        {
            if (PropertyLib._KCBProperties.Nhapngaythangnamsinh)
            {
                txtTuoi.Text = Utility.sDbnull(globalVariables.SysDate.Year - dtpBOD.Value.Year);
            }
        }
        void cmdGetBV_Click(object sender, EventArgs e)
        {
            frm_danhsachbenhvien _danhsachbenhvien = new frm_danhsachbenhvien();
            if (_danhsachbenhvien.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtNoichuyenden.SetId(_danhsachbenhvien.idBenhvien);
            }
        }
        void cboDoituongKCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowTextChanged) return;
                _MaDoituongKcb = Utility.sDbnull(cboDoituongKCB.SelectedValue);
                objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(_MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
                ChangeObjectRegion();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void chkChuyenVien_CheckedChanged(object sender, EventArgs e)
        {
            txtNoichuyenden.Enabled = chkChuyenVien.Checked;
            cmdGetBV.Enabled = chkChuyenVien.Checked;
        }
        bool AutoLoad = false;
        void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._KCBProperties);
            frm.ShowDialog();
            CauHinhKCB();
        }

        private string GetSoBHYT
        {
            get { return SoBHYT; }
            set { SoBHYT = value; }
        }

        private void txtTEN_BN_LostFocus(object sender, EventArgs e)
        {
            txtTEN_BN.Text = Utility.CapitalizeWords(txtTEN_BN.Text.Trim());
        }
        private void txtMaLankham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtMaLankham.Text.Trim() != "")
            {
                ucBHYT1.ClearNoiDKKCBBD();
                isAutoFinding = true;
                string patient_ID = Utility.GetYY(globalVariables.SysDate) +
                                    Utility.FormatNumberToString(Utility.Int32Dbnull(txtMaLankham.Text, 0), "000000");
                txtMaLankham.Text = patient_ID;
                FindPatientIDbyMaLanKham(txtMaLankham.Text.Trim());
                isAutoFinding = false;
            }
        }

       

        private void txtMaBN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtMaBN.Text.Trim() != "")
            {
                ucBHYT1.ClearNoiDKKCBBD();
                isAutoFinding = true;
                FindPatient(txtMaBN.Text.Trim());
                isAutoFinding = false;
            }
        }

        private void FindPatient(string patient_ID)
        {
            try
            {
                QueryCommand cmd = KcbDanhsachBenhnhan.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandSql =
                    "Select id_benhnhan,ten_benhnhan,gioi_tinh from kcb_danhsach_benhnhan where id_benhnhan like '%" +
                    patient_ID + "%'";

                DataTable temdt = DataService.GetDataSet(cmd).Tables[0];
                if (temdt.Rows.Count == 1)
                {
                    AutoFindLastExamandFetchIntoControls(temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty);
                }
                else //Show dialog for select
                {
                    DataRow[] arrDr = temdt.Select("id_benhnhan=" + patient_ID);
                    if (arrDr.Length == 1)
                        AutoFindLastExamandFetchIntoControls(arrDr[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty);
                    else
                    {
                        var _ChonBN = new frm_CHON_BENHNHAN();
                        _ChonBN.temdt = temdt;
                        _ChonBN.ShowDialog();
                        if (!_ChonBN.mv_bCancel)
                        {
                            AutoFindLastExamandFetchIntoControls(_ChonBN.Patient_ID, string.Empty);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
            }
        }

        private void FindPatientIDbyMaLanKham(string malankham)
        {
            try
            {
                QueryCommand cmd = KcbDanhsachBenhnhan.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandSql =
                    "Select id_benhnhan,ten_benhnhan,gioi_tinh from kcb_danhsach_benhnhan p where exists(select 1 from kcb_luotkham where id_benhnhan=P.id_benhnhan and ma_luotkham like '%" +
                    malankham + "%')";
                DataTable temdt = DataService.GetDataSet(cmd).Tables[0];
                if (temdt.Rows.Count <= 0) return;
                if (temdt.Rows.Count == 1)
                {
                    AutoFindLastExamandFetchIntoControls(temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty);
                }
                else //Show dialog for select
                {
                    var _ChonBN = new frm_CHON_BENHNHAN();
                    _ChonBN.temdt = temdt;
                    _ChonBN.ShowDialog();
                    if (!_ChonBN.mv_bCancel)
                    {
                        AutoFindLastExamandFetchIntoControls(_ChonBN.Patient_ID, string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
            }

        }

        private void AutoFindLastExamandFetchIntoControls(string patientID, string sobhyt)
        {
            try
            {
                if (!Utility.CheckLockObject(m_strMaluotkham, "Tiếp đón", "TD"))
                    return;
                //Trả lại mã lượt khám nếu chưa được dùng đến
                new Update(KcbDmucLuotkham.Schema)
                       .Set(KcbDmucLuotkham.Columns.TrangThai).EqualTo(0)
                       .Set(KcbDmucLuotkham.Columns.UsedBy).EqualTo(DBNull.Value)
                       .Set(KcbDmucLuotkham.Columns.StartTime).EqualTo(DBNull.Value)
                       .Set(KcbDmucLuotkham.Columns.EndTime).EqualTo(null)
                       .Where(KcbDmucLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.Int32Dbnull(m_strMaluotkham, "-1"))
                       .And(KcbDmucLuotkham.Columns.TrangThai).IsEqualTo(1)
                       .And(KcbDmucLuotkham.Columns.UsedBy).IsEqualTo(globalVariables.UserName)
                       .And(KcbDmucLuotkham.Columns.Nam).IsEqualTo(globalVariables.SysDate.Year).Execute();
                ;

                SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema)
                    .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(patientID);
                if (!string.IsNullOrEmpty(sobhyt))
                {
                    sqlQuery.And(KcbLuotkham.Columns.MatheBhyt).IsEqualTo(sobhyt);
                }
                sqlQuery.OrderDesc(KcbLuotkham.Columns.NgayTiepdon);

                var objLuotkham = sqlQuery.ExecuteSingle<KcbLuotkham>();
                if (objLuotkham != null)
                {
                    txtMaBN.Text = patientID;
                    txtMaLankham.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                    m_strMaluotkham = objLuotkham.MaLuotkham;
                    m_enAct = action.FirstOrFinished;
                    AllowTextChanged = false;
                    LoadThongtinBenhnhan();
                    ModifyCommand();
                }
                else
                {
                    Utility.ShowMsg("Không tìm thấy thông tin bệnh nhân. Đề nghị liên hệ với VMS để được giải đáp");
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("AutoFindLastExam().Exception-->" + ex.Message);
            }
            finally
            {
                AllowTextChanged = true;
            }
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin của phần dữ liệu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_QlyLsuthaydoiDoituongKCB_Load(object sender, EventArgs e)
        {
            try
            {
                AllowTextChanged = false;
                b_HasLoaded = false;
                XoathongtinBHYT(true);
                AddAutoCompleteDiaChi();
                AutocompleteBenhvien();

                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(), DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "", false);
                objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(_MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _MaDoituongKcb);
                ChangeObjectRegion();
                SetControlStatus();
                
            }
            catch
            {
            }
            finally
            {
                AllowTextChanged = true;
                b_HasLoaded = true;
                ModifyCommand();
            }
        }
        byte _IdLoaidoituongKcb = 1;
        Int16 _IdDoituongKcb = 1;
        string _MaDoituongKcb = "DV";
        string _TenDoituongKcb = "Dịch vụ";
        decimal PtramBhytCu = 0m;
        decimal PtramBhytGocCu = 0m;
        KcbLuotkham objLuotkham = null;
        private void LoadThongtinBenhnhan()
        {
            PtramBhytCu = 0m;
            PtramBhytGocCu = 0m;
            AllowTextChanged = false;
            LstNoitruPhanbuonggiuong = new NoitruPhanbuonggiuongCollection();
            objBuonggiuong = null;
            objBenhnhan = KcbDanhsachBenhnhan.FetchByID(txtMaBN.Text);
            if (objBenhnhan != null)
            {
                txtTEN_BN.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan);
                txtNamSinh.Text = Utility.sDbnull(objBenhnhan.NamSinh);

                txtDiachi.Text = Utility.sDbnull(objBenhnhan.DiaChi);
                if (objBenhnhan.NgaySinh != null) dtpBOD.Value = objBenhnhan.NgaySinh.Value;
                else dtpBOD.Value = new DateTime((int)objBenhnhan.NamSinh, 1, 1);
                txtNamSinh.Text = Utility.sDbnull(objBenhnhan.NamSinh);
                txtTuoi.Text = Utility.sDbnull(globalVariables.SysDate.Year - Utility.Int32Dbnull(objBenhnhan.NamSinh));
                cboPatientSex.SelectedIndex = Utility.GetSelectedIndex(cboPatientSex, Utility.sDbnull(objBenhnhan.IdGioitinh));
                txtCMT.Text = Utility.sDbnull(objBenhnhan.Cmt);


                objLuotkham = new Select().From(KcbLuotkham.Schema)
                   .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtMaLankham.Text)
                   .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtMaBN.Text, -1)).ExecuteSingle
                   <KcbLuotkham>();
                if (objLuotkham != null)
                {
                    lstIDDelete = new List<long>();
                    m_strMaluotkham = objLuotkham.MaLuotkham;

                    txtSolankham.Text = Utility.sDbnull(objLuotkham.SolanKham);
                    _IdDoituongKcb = objLuotkham.IdDoituongKcb;
                    dtpInputDate.Value = objLuotkham.NgayTiepdon;
                    dtCreateDate.Value = objLuotkham.NgayTiepdon;

                    _MaDoituongKcb = Utility.sDbnull(objLuotkham.MaDoituongKcb);
                    objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(_MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();

                    ChangeObjectRegion();
                    PtramBhytCu = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                    PtramBhytGocCu = Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0);
                    _IdDoituongKcb = objDoituongKCB.IdDoituongKcb;
                    _TenDoituongKcb = objDoituongKCB.TenDoituongKcb;
                    cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _MaDoituongKcb);
                    chkChuyenVien.Checked = Utility.Int32Dbnull(objLuotkham.TthaiChuyenden, 0) == 1;
                    txtNoichuyenden.SetId(Utility.Int32Dbnull(objLuotkham.IdBenhvienDen, -1));
                    if (!string.IsNullOrEmpty(objLuotkham.MatheBhyt))//Thông tin BHYT
                    {
                        ucBHYT1.SetValues(chkChuyenVien.Checked, objDoituongKCB, objLuotkham);
                    }
                    else
                    {
                        XoathongtinBHYT(true);
                    }
                    m_dtData = SPs.KcbLaythongtinLichsuDoituongKcb(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                    ProcessData();
                    Utility.SetDataSourceForDataGridEx(grdList, m_dtData, true, true, "1=1", " fromdate desc,enddate desc");
                    AllowGridSelecttionChanged = true;
                    Utility.GotoNewRowJanus(grdList, KcbLichsuDoituongKcb.Columns.IdLichsuDoituongKcb, objLuotkham.IdLichsuDoituongKcb.ToString());
                }
                else
                {
                }
            }
            chkChuyenVien_CheckedChanged(chkChuyenVien, new EventArgs());
        }

        void XoathongtinBHYT(bool forcetodel)
        {
            if (forcetodel)
            {
                _IdDoituongKcb = 1;
                _MaDoituongKcb = "DV";
                _TenDoituongKcb = "Dịch vụ";
                ucBHYT1.ClearMe();
            }
        }

        private void AddAutoCompleteDiaChi()
        {
            ucBHYT1.DoAutoComplete();
        }

        private void AutocompleteBenhvien()
        {

            try
            {
                DataTable m_dtBenhvien = new Select().From(DmucBenhvien.Schema).ExecuteDataSet().Tables[0];
                if (m_dtBenhvien == null) return;
                txtNoichuyenden.Init(m_dtBenhvien, new List<string>() { DmucBenhvien.Columns.IdBenhvien, DmucBenhvien.Columns.MaBenhvien, DmucBenhvien.Columns.TenBenhvien });

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {


            }
        }

       


        /// <summary>
        /// hàm thực hiện việc làm sách thông tin của bệnh nhân
        /// </summary>
        private void ClearControl()
        {
            Utility.SetMsg(lblMsg, "", false);
            m_blnHasJustInsert = false;
            ModifyCommand();
            AllowTextChanged = false;
            XoathongtinBHYT(true);

            _MaDoituongKcb = Utility.sDbnull(cboDoituongKCB.SelectedValue);
            objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(_MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
            if (objDoituongKCB == null) return;
            _IdDoituongKcb = objDoituongKCB.IdDoituongKcb;
            _IdLoaidoituongKcb = objDoituongKCB.IdLoaidoituongKcb;
            _TenDoituongKcb = objDoituongKCB.TenDoituongKcb;
            PtramBhytCu = objDoituongKCB.PhantramTraituyen.Value;
            PtramBhytGocCu = PtramBhytCu;
            chkChuyenVien.Checked = false;
            AllowTextChanged = true;
            //Chuyển về trạng thái thêm mới
            m_enAct = action.Insert;
            if (PropertyLib._KCBProperties.SexInput) cboPatientSex.SelectedIndex = -1;
            ucBHYT1.ResetMe(objDoituongKCB);
            if (ucBHYT1.IsBHYT)
            {
            }
            else
            {
                PtramBhytCu = objDoituongKCB.PhantramTraituyen.Value;
                PtramBhytGocCu = PtramBhytCu;
                cboDoituongKCB.Focus();
            }
            if (m_enAct == action.Insert)
            {
                dtFromDate.Text = m_dtData.AsEnumerable().Max(x => x.Field<string>("sngay_hethieuluc"));
                dtFromDate.Value = dtFromDate.Value.AddDays(1);
                dtToDate.Text = "";
            }
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện việc lưu thông tin của đối tượng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                cmdSave.Enabled = false;
                if (m_dtData.Rows.Count <= 0)
                {
                    Utility.SetMsg(lblMsg, "Không có dữ liệu để ghi. Đề nghị bạn kiểm tra lại", true);
                    return;
                }
                if (!CheckDayRangeConflict()) return;
                List<KcbLichsuDoituongKcb> lstLichsu = GetHistory();
                if (lstLichsu.Count <= 0)
                {
                    Utility.ShowMsg("Cần ít nhất 1 dòng lịch sử đối tượng KCB trước khi thực hiện nhấn nút chấp nhận");
                    return;
                }
               ActionResult act= ChuyenDoituongKCB.CapnhatLichsuDoituongKCB(lstLichsu, lstIDDelete);
               if (act == ActionResult.Success)
               {
                   Utility.SetMsg(lblMsg, "Đã cập nhật lịch sử thay đổi đối tượng KCB của Bệnh nhân thành công", false);
                   lstIDDelete.Clear();
               }
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                cmdSave.Enabled = true;
            }
        }
       
        private bool IsValidData()
        {
            Utility.SetMsg(lblMsg, "", false);
            if (objLuotkham == null)
            {
                txtMaLankham.Focus();
                Utility.SetMsg(lblMsg, "Bạn phải nhập mã lượt khám", true);
                return false;
            }

            if (THU_VIEN_CHUNG.IsBaoHiem(_IdLoaidoituongKcb))
            {
                if (!ucBHYT1.IsValidBHYT()) return false;
                if (!ucBHYT1.IsValidTheBHYT()) return false;
                //So sánh ngày hiệu lực BHYT với ngày hiệu lực áp dụng đối tượng.
                if (ucBHYT1.dtInsFromDate.Value >dtFromDate.Value)
                {
                    Utility.ShowMsg(string.Format("Ngày bắt đầu hiệu lực của thẻ BHYT: {0} phải lớn hơn hoặc bằng ngày bắt đầu hiệu lực của đối tượng KCB: {1}", ucBHYT1.dtInsToDate.Text, dtFromDate.Text));
                    ucBHYT1.dtInsToDate.Focus();
                    return false;
                }
                if (ucBHYT1.dtInsToDate.Value < dtToDate.Value)
                {
                    Utility.ShowMsg(string.Format("Ngày kết thúc hiệu lực của thẻ BHYT: {0} phải lớn hơn hoặc bằng ngày kết thúc hiệu lực của đối tượng KCB: {1}", ucBHYT1.dtInsToDate.Text, dtToDate.Text));
                    ucBHYT1.dtInsToDate.Focus();
                    return false;
                }
            }
            if (THU_VIEN_CHUNG.IsBaoHiem(objDoituongKCB.IdLoaidoituongKcb))
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATNHAP_DIACHI_BHYT", "0", false) == "1")
                {
                    if (Utility.DoTrim(ucBHYT1.txtDiachi_bhyt.Text) == "")
                    {
                        Utility.SetMsg(lblMsg, "Bạn phải nhập địa chỉ thẻ BHYT", true);
                        ucBHYT1.txtDiachi_bhyt.Focus();
                        return false;
                    }
                }
                if (Utility.DoTrim(ucBHYT1.txtMaDTsinhsong.Text) != "" && ucBHYT1.txtMaDTsinhsong.myCode == "-1")
                {
                    Utility.SetMsg(lblMsg, "Mã đối tượng sinh sống chưa đúng. Mời bạn nhập lại", true);
                    ucBHYT1.txtMaDTsinhsong.SelectAll();
                    ucBHYT1.txtMaDTsinhsong.Focus();
                    return false;
                }
            }
            if (chkChuyenVien.Checked)
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("BATNHAPNOICHUYENDEN", "0", false) == "1")
                {
                    if (txtNoichuyenden.MyCode == "-1")
                    {
                        Utility.SetMsg(lblMsg, "Bạn phải nhập bệnh viện chuyển đến", true);
                        txtNoichuyenden.SelectAll();
                        txtNoichuyenden.Focus();
                        return false;
                    }
                }
            }
            if (string.IsNullOrEmpty(txtTEN_BN.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập tên Bệnh nhân", true);
                txtTEN_BN.Focus();
                return false;
            }
            if (!PropertyLib._KCBProperties.Nhapngaythangnamsinh && string.IsNullOrEmpty(txtNamSinh.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập ngày tháng năm sinh, hoặc năm sinh cho bệnh nhân ", true);
                txtNamSinh.Focus();
                return false;
            }
            if (cboPatientSex.SelectedIndex < 0)
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn giới tính của Bệnh nhân", true);
                cboPatientSex.Focus();
                return false;
            }

            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATNHAP_DIACHI_BENHNHAN", "0", false) == "1")
            {
                if (Utility.DoTrim(txtDiachi.Text) == "")
                {
                    Utility.SetMsg(lblMsg, "Bạn phải nhập địa chỉ Bệnh nhân", true);
                    txtDiachi.Focus();
                    return false;
                }
            }
            if (dtToDate.IsNullDate)
            {

                Utility.SetMsg(lblMsg, "Bạn phải nhập ngày hết hiệu lực của đối tượng", true);
                dtToDate.Focus();
                return false;

            }
            if (Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtFromDate.Value)) > Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtToDate.Value)))
            {
                Utility.SetMsg(lblMsg, "Ngày hiệu lực Đến phải lớn hơn hoặc bằng ngày hiệu lực Từ", true);
                dtToDate.Focus();
                return false;
            }
            return true;
        }
        private void ModifyCommand()
        {
            bool isValid = objLuotkham != null;
            bool isValid2 = Utility.isValidGrid(grdList);
            cmdSua.Enabled = isValid && isValid2 && m_enAct == action.FirstOrFinished;
            cmdxoa.Enabled = isValid && isValid2 && m_enAct == action.FirstOrFinished;
            cmdthemmoi.Enabled = isValid;
            cmdGhi.Enabled = m_enAct != action.FirstOrFinished;
            cmdHuy.Enabled = cmdGhi.Enabled ;
        }
        private void PerformAction()
        {
            if (!IsValidData()) return;
            TaoLichsu();
            m_enAct = action.FirstOrFinished;
            SetControlStatus();
            ModifyCommand();
        }
        void ProcessData()
        {
            foreach (DataRow dr in m_dtData.Rows)
            {
                dr["fromdate"] = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dr["sngay_hieuluc"].ToString()));
                dr["enddate"] = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dr["sngay_hethieuluc"].ToString())); 
            }
            m_dtData.AcceptChanges();
        }
        bool CheckDayRangeConflict()
        {
            bool first_DV = false;
            int firstId_DV=-1;
            bool first_BHYT = false;
            int firstId_BHYT = -1;
            string firstMatheBHYT="";
            decimal ptramNgoaitru=0m;
            decimal ptramDauthe=0m;
            //Kiểm tra nhiều dòng đối tượng Dịch vụ liền kề nhau
            foreach (DataRow dr in m_dtData.Select("1=1", "fromdate desc,enddate desc"))
            {
                if (!first_DV && Utility.sDbnull(dr[KcbLichsuDoituongKcb.Columns.MaDoituongKcb]) == "DV")
                {
                    first_DV = true;
                    firstId_DV = Utility.Int32Dbnull(dr[KcbLichsuDoituongKcb.Columns.IdLichsuDoituongKcb]);
                }
                else
                {
                    if (Utility.Int32Dbnull(dr[KcbLichsuDoituongKcb.Columns.IdLichsuDoituongKcb]) != firstId_DV)
                        if (Utility.sDbnull(dr[KcbLichsuDoituongKcb.Columns.MaDoituongKcb]) == "DV")
                        {
                            Utility.ShowMsg("Các hồ sơ có đối tượng khác BHYT không được phép liền kề nhau(Vì không có ý nghĩa). Đề nghị bạn kiểm tra lại");
                            return false;
                        }
                        else
                        {
                            first_DV = false;
                            firstId_DV = -1;
                        }
                }
                //Kiểm tra 2 mã thẻ BHYT giống nhau kề liền nhau, sửa dụng luôn các biến DV
                if (!first_BHYT && Utility.sDbnull(dr[KcbLichsuDoituongKcb.Columns.MaDoituongKcb]) == "BHYT")
                {
                    first_BHYT = true;
                    firstId_BHYT = Utility.Int32Dbnull(dr[KcbLichsuDoituongKcb.Columns.IdLichsuDoituongKcb]);
                    firstMatheBHYT = Utility.sDbnull(dr[KcbLichsuDoituongKcb.Columns.MatheBhyt], "");
                    ptramNgoaitru = Utility.DecimaltoDbnull(dr[KcbLichsuDoituongKcb.Columns.PtramBhyt], 0);
                    ptramDauthe = Utility.DecimaltoDbnull(dr[KcbLichsuDoituongKcb.Columns.PtramBhytGoc], 0);
                }
                else
                {
                    if (Utility.Int32Dbnull(dr[KcbLichsuDoituongKcb.Columns.IdLichsuDoituongKcb]) != firstId_BHYT)
                        if (Utility.sDbnull(dr[KcbLichsuDoituongKcb.Columns.MaDoituongKcb]) == "BHYT"
                            && firstMatheBHYT == Utility.sDbnull(dr[KcbLichsuDoituongKcb.Columns.MatheBhyt], "")
                            && ptramNgoaitru == Utility.DecimaltoDbnull(dr[KcbLichsuDoituongKcb.Columns.PtramBhyt], 0)
                            && ptramDauthe == Utility.DecimaltoDbnull(dr[KcbLichsuDoituongKcb.Columns.PtramBhytGoc], 0)
                            )
                        {
                            Utility.ShowMsg("Các hồ sơ có đối tượng BHYT giống nhau về mã thẻ, % BHYT ngoại trú, % đầu thẻ không được phép liền kề nhau(Vì không có ý nghĩa). Đề nghị bạn kiểm tra lại");
                            return false;
                        }
                        else
                        {
                            first_BHYT = false;
                            firstId_BHYT = -1;
                        }
                }
                int id = Utility.Int32Dbnull(dr[KcbLichsuDoituongKcb.Columns.IdLichsuDoituongKcb]);
                int fromdate = Utility.Int32Dbnull(dr["fromdate"]);
                int enddate = Utility.Int32Dbnull(dr["enddate"]);
                //Kiểm tra dữ liệu liền kề nhau bị cách ngày-->không cho phép
                var q = from p in m_dtData.AsEnumerable()
                        where
                        Utility.Int32Dbnull(p["fromdate"]) > enddate
                        orderby p["fromdate"] ascending
                        select p;
                if (q.Any())
                {
                    if (Utility.Int32Dbnull(q.ToList()[0]["fromdate"]) > enddate + 1)
                    {
                        Utility.ShowMsg("Các hồ sơ thay đổi đối tượng KCB của Bệnh nhân phải tuân thủ nguyên tắc : Ngày liền kề liên tiếp nhau.\nĐề nghị bạn kiểm tra lại");
                        return false;
                    }
                }
                //Kiểm tra dữ liệu khoảng thời gian bị chồng chéo nhau
               
               var  q1 = from p in m_dtData.AsEnumerable()
                        where Utility.Int32Dbnull(p[KcbLichsuDoituongKcb.Columns.IdLichsuDoituongKcb], -1) != id
                        && (
                        (fromdate >= Utility.Int32Dbnull(p["fromdate"]) && fromdate <= Utility.Int32Dbnull(p["enddate"]))
                        || (enddate >= Utility.Int32Dbnull(p["fromdate"]) && fromdate <= Utility.Int32Dbnull(p["enddate"]))
                        )
                        select p;
                if (q1.Any())
                {
                    Utility.ShowMsg("Dữ liệu thời gian bị chồng chéo nhau. Đề nghị bạn kiểm tra lại toàn bộ các khoảng thời gian hiệu lực từ ngày....đến ngày của tất cả các lần thay đổi");
                    return false;
                }
            }
           
            return true;
        }
        bool CheckDayRangeConflictImmediately(int id)
        {
            int fromdate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtFromDate.Value));
            int enddate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtToDate.Value));
            foreach (DataRow dr in m_dtData.Rows)
            {
                var q = from p in m_dtData.AsEnumerable()
                        where Utility.Int32Dbnull(p[KcbLichsuDoituongKcb.Columns.IdLichsuDoituongKcb], -1) != id
                        && (
                        (fromdate >= Utility.Int32Dbnull(p["fromdate"]) && fromdate <= Utility.Int32Dbnull(p["enddate"]))
                        || (enddate >= Utility.Int32Dbnull(p["fromdate"]) && fromdate <= Utility.Int32Dbnull(p["enddate"]))
                        )
                        select p;
                if (q.Any())
                {
                    Utility.ShowMsg(string.Format("Khoảng thời gian hiệu lực từ ngày {0} đến ngày {1} đang nằm trong một khoảng thời gian khác. Đề nghị bạn kiểm tra lại", dtFromDate.Value.ToString("dd/MM/yyyy"), dtToDate.Value.ToString("dd/MM/yyyy")));
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ham thwucj hiện việc chọn thông tin tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ChangeObjectRegion()
        {
            if (objDoituongKCB == null) return;
            _IdDoituongKcb = objDoituongKCB.IdDoituongKcb;
            _IdLoaidoituongKcb = objDoituongKCB.IdLoaidoituongKcb;
            _TenDoituongKcb = objDoituongKCB.TenDoituongKcb;
            PtramBhytCu = objDoituongKCB.PhantramTraituyen.Value;
            PtramBhytGocCu = PtramBhytCu;
            ucBHYT1.ChangeObjectRegion(objDoituongKCB);
            if (objDoituongKCB.IdLoaidoituongKcb == 0)//ĐỐi tượng BHYT
            {
                ucBHYT1.IsBHYT = ucBHYT1.IsBHYT && m_enAct != action.FirstOrFinished;
            }
            else//Đối tượng khác BHYT
            {
                XoathongtinBHYT(PropertyLib._KCBProperties.XoaBHYT);
                cboDoituongKCB.Focus();
            }
        }
        DmucDoituongkcb objDoituongKCB = null;
        /// <summary>
        /// hàm thực hienej phím tắt của form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_QlyLsuthaydoiDoituongKCB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                if (this.ActiveControl != null && this.ActiveControl.Name == txtTEN_BN.Name && Utility.DoTrim(txtTEN_BN.Text) != "")
                {
                    frm_DSACH_BN_TKIEM Timkiem_Benhnhan = new frm_DSACH_BN_TKIEM("ALL",100);
                    Timkiem_Benhnhan.AutoSearch = true;
                    Timkiem_Benhnhan.FillAndSearchData(false, "", "", Utility.DoTrim(txtTEN_BN.Text), "", "", "-1");
                    if (Timkiem_Benhnhan.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        ucBHYT1.ClearNoiDKKCBBD();
                        isAutoFinding = true;
                        FindPatient(Timkiem_Benhnhan.IdBenhnhan.ToString());
                        isAutoFinding = false;
                    }
                }
                else if (this.ActiveControl != null && this.ActiveControl.Name == txtCMT.Name && Utility.DoTrim(txtCMT.Text) != "")
                {
                    frm_DSACH_BN_TKIEM Timkiem_Benhnhan = new frm_DSACH_BN_TKIEM("ALL",100);
                    Timkiem_Benhnhan.AutoSearch = true;
                    Timkiem_Benhnhan.FillAndSearchData(false, "", "", "", Utility.DoTrim(txtCMT.Text), "", "-1");
                    if (Timkiem_Benhnhan.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        ucBHYT1.ClearNoiDKKCBBD();
                        isAutoFinding = true;
                        FindPatient(Timkiem_Benhnhan.IdBenhnhan.ToString());
                        isAutoFinding = false;
                    }
                }

                return;
            }

            if (e.Control && e.KeyCode == Keys.D)
            {

                _MaDoituongKcb = "DV";
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _MaDoituongKcb);
                return;
            }
            if (e.Control && e.KeyCode == Keys.B)
            {
                _MaDoituongKcb = "BHYT";
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _MaDoituongKcb);
                return;
            }
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.P))
            {
                AllowTextChanged = false;
                txtDiachi.Text = ucBHYT1.txtDiachi_bhyt.Text;
                AllowTextChanged = true;
                return;
            }

            string ngay_kham = globalVariables.SysDate.ToString("dd/MM/yyyy");

            if (e.Control && e.KeyCode == Keys.F)
            {
                txtMaBN.SelectAll();
                txtMaBN.Focus();
            }

            //if (e.KeyCode == Keys.F1)
            //{
            //    tabControl1.SelectedTab = tabControl1.TabPages[0];
            //    return;
            //}
            //if (e.KeyCode == Keys.F2)
            //{
            //    tabControl1.SelectedTab = tabControl1.TabPages[1];
            //    return;
            //}
            if (e.KeyCode == Keys.F11) Utility.ShowMsg(this.ActiveControl.Name);
            if (e.KeyCode == Keys.Escape && this.ActiveControl != null
                && (
                this.ActiveControl.GetType() != txtDiachi.GetType()
                || this.ActiveControl.GetType() != txtNoichuyenden.GetType()
                ))
            {

                Close();
            }
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.N && e.Control) cmdExit.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }



        private void frm_QlyLsuthaydoiDoituongKCB_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Utility.FreeLockObject(m_strMaluotkham);
            }
            catch (Exception exception)
            {
            }
        }

        private void CauHinhKCB()
        {
            dtpBOD.Value = globalVariables.SysDate;
            dtpBOD.Visible = PropertyLib._KCBProperties.Nhapngaythangnamsinh;
            txtNamSinh.Visible = !PropertyLib._KCBProperties.Nhapngaythangnamsinh;
            if (dtpBOD.Visible)
                txtTuoi.Text = Utility.sDbnull(globalVariables.SysDate.Year - dtpBOD.Value.Year);

        }



        #region "Sự kiện bắt cho phần khám bệnh"

        #endregion

        #region "khởi tạo sự kiện để lưu lại thông tin của bệnh nhân"

        private string mavuasinh = "";


        List<KcbLichsuDoituongKcb> GetHistory()
        {
            List<KcbLichsuDoituongKcb> lstValues = new List<KcbLichsuDoituongKcb>();
            foreach (DataRow dr in m_dtData.Rows)
            {
                KcbLichsuDoituongKcb _newItem = null;
                if (Utility.Int32Dbnull(dr[KcbLichsuDoituongKcb.Columns.IdLichsuDoituongKcb], -1) > 0)
                {
                    _newItem = KcbLichsuDoituongKcb.FetchByID(Utility.Int32Dbnull(dr[KcbLichsuDoituongKcb.Columns.IdLichsuDoituongKcb], -1));
                    _newItem.IsNew = false;
                    _newItem.MarkOld();
                    _newItem.NguoiSua = globalVariables.UserName;
                    _newItem.NgaySua = globalVariables.SysDate;
                }
                else
                {
                    _newItem = new KcbLichsuDoituongKcb();
                    _newItem.IsNew = true;
                    _newItem.NguoiTao = globalVariables.UserName;
                    _newItem.NgayTao = globalVariables.SysDate;
                }
                _newItem.IdBenhnhan = objLuotkham.IdBenhnhan;
                _newItem.MaLuotkham = objLuotkham.MaLuotkham;
                _newItem.NgayHieuluc = Convert.ToDateTime(dr[KcbLichsuDoituongKcb.Columns.NgayHieuluc]);
                if (dr[KcbLichsuDoituongKcb.Columns.NgayHethieuluc].Equals(DBNull.Value))
                    _newItem.NgayHethieuluc = null;
                else
                    _newItem.NgayHethieuluc = Convert.ToDateTime(dr[KcbLichsuDoituongKcb.Columns.NgayHethieuluc]);
                _newItem.IdDoituongKcb = Utility.Int16Dbnull(dr[KcbLichsuDoituongKcb.Columns.IdDoituongKcb]);
                _newItem.MaDoituongKcb = Utility.sDbnull(dr[KcbLichsuDoituongKcb.Columns.MaDoituongKcb]);
                _newItem.IdLoaidoituongKcb = Utility.ByteDbnull(dr[KcbLichsuDoituongKcb.Columns.IdLoaidoituongKcb]);
                _newItem.MatheBhyt = Utility.sDbnull(dr[KcbLichsuDoituongKcb.Columns.MatheBhyt]);
                _newItem.PtramBhyt = Utility.DecimaltoDbnull(dr[KcbLichsuDoituongKcb.Columns.PtramBhyt]);
                _newItem.PtramBhytGoc = Utility.DecimaltoDbnull(dr[KcbLichsuDoituongKcb.Columns.PtramBhytGoc]);
                if (dr[KcbLichsuDoituongKcb.Columns.NgaybatdauBhyt].Equals(DBNull.Value))
                    _newItem.NgaybatdauBhyt = null;
                else
                    _newItem.NgaybatdauBhyt = Convert.ToDateTime(dr[KcbLichsuDoituongKcb.Columns.NgaybatdauBhyt]);
                if (dr[KcbLichsuDoituongKcb.Columns.NgayketthucBhyt].Equals(DBNull.Value))
                    _newItem.NgayketthucBhyt = null;
                else
                    _newItem.NgayketthucBhyt = Convert.ToDateTime(dr[KcbLichsuDoituongKcb.Columns.NgayketthucBhyt]);
                _newItem.NoicapBhyt = Utility.sDbnull(dr[KcbLichsuDoituongKcb.Columns.NoicapBhyt]);
                _newItem.MaNoicapBhyt = Utility.sDbnull(dr[KcbLichsuDoituongKcb.Columns.MaNoicapBhyt]);
                _newItem.MaDoituongBhyt = Utility.sDbnull(dr[KcbLichsuDoituongKcb.Columns.MaDoituongBhyt]);
                _newItem.MaQuyenloi = Utility.Int32Dbnull(dr[KcbLichsuDoituongKcb.Columns.MaQuyenloi]);
                _newItem.NoiDongtrusoKcbbd = Utility.sDbnull(dr[KcbLichsuDoituongKcb.Columns.NoiDongtrusoKcbbd]);
                _newItem.MaKcbbd = Utility.sDbnull(dr[KcbLichsuDoituongKcb.Columns.MaKcbbd]);
                _newItem.TrangthaiNoitru = Utility.ByteDbnull(dr[KcbLichsuDoituongKcb.Columns.TrangthaiNoitru]);

                _newItem.DungTuyen = Utility.ByteDbnull(dr[KcbLichsuDoituongKcb.Columns.DungTuyen]);
                _newItem.Cmt = Utility.sDbnull(dr[KcbLichsuDoituongKcb.Columns.Cmt]);
                _newItem.IdRavien = Utility.Int64Dbnull(dr[KcbLichsuDoituongKcb.Columns.IdRavien]);
                _newItem.IdBuong = Utility.Int16Dbnull(dr[KcbLichsuDoituongKcb.Columns.IdBuong]);
                _newItem.IdGiuong = Utility.Int16Dbnull(dr[KcbLichsuDoituongKcb.Columns.IdGiuong]);
                _newItem.IdKhoanoitru = Utility.Int16Dbnull(dr[KcbLichsuDoituongKcb.Columns.IdKhoanoitru]);
                _newItem.GiayBhyt = Utility.ByteDbnull(dr[KcbLichsuDoituongKcb.Columns.GiayBhyt]);
                _newItem.MadtuongSinhsong = Utility.sDbnull(dr[KcbLichsuDoituongKcb.Columns.MadtuongSinhsong]);
                _newItem.DiachiBhyt = Utility.sDbnull(dr[KcbLichsuDoituongKcb.Columns.DiachiBhyt]);
                _newItem.TrangthaiCapcuu = Utility.ByteDbnull(dr[KcbLichsuDoituongKcb.Columns.TrangthaiCapcuu]);
                lstValues.Add(_newItem);
            }
            return lstValues;
        }

        /// <summary>
        /// hàm thực hiện việc khwoir tạo thoog tin PatietnExam
        /// </summary>
        /// <returns></returns>
        private void TaoLichsu()
        {
            if (m_enAct == action.Insert )
            {
                newItem = m_dtData.NewRow();
            }
            else
            {
               //Da co
            }
            if (newItem == null)
            {
                Utility.ShowMsg("Bạn phải chọn ít nhất 1 dòng dữ liệu cần sửa");
                return;
            }
            newItem[KcbLichsuDoituongKcb.Columns.IdBenhnhan] =objLuotkham.IdBenhnhan;
            newItem[KcbLichsuDoituongKcb.Columns.MaLuotkham] = objLuotkham.MaLuotkham;
            newItem[KcbLichsuDoituongKcb.Columns.NgayHieuluc] =dtFromDate.Value;
            newItem[KcbLichsuDoituongKcb.Columns.NgayHethieuluc] = dtToDate.Value;
            newItem["sngay_hieuluc"] = dtFromDate.Value.ToString("dd/MM/yyyy");
            if (dtToDate.IsNullDate)
                newItem["sngay_hethieuluc"] = new DateTime(2099,1,1).ToString("dd/MM/yyyy");
            else
                newItem["sngay_hethieuluc"] = dtToDate.Value.ToString("dd/MM/yyyy");

            ucBHYT1.GetData(ref newItem);
            newItem["fromdate"] = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtFromDate.Value));
            newItem["enddate"] = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtToDate.Value));
            DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(_IdDoituongKcb);
            if (objectType != null)
            {
                 newItem[KcbLichsuDoituongKcb.Columns.MaDoituongKcb]  = Utility.sDbnull(objectType.MaDoituongKcb, "");
                 newItem[KcbLichsuDoituongKcb.Columns.IdLoaidoituongKcb] = Utility.sDbnull(objectType.IdLoaidoituongKcb, "");
                 newItem[KcbLichsuDoituongKcb.Columns.IdDoituongKcb] = Utility.sDbnull(objectType.IdDoituongKcb, "");
                 newItem["ten_doituong_kcb"] = objectType.TenDoituongKcb;
            }
            newItem[KcbLichsuDoituongKcb.Columns.Cmt] = objLuotkham.Cmt;
            if (objLuotkham.NgayNhapvien.Equals(null))
            {
                newItem[KcbLichsuDoituongKcb.Columns.TrangthaiNoitru] = 0;
                newItem[KcbLichsuDoituongKcb.Columns.IdKhoanoitru] = -1;
                newItem[KcbLichsuDoituongKcb.Columns.IdRavien] = -1;
                newItem[KcbLichsuDoituongKcb.Columns.IdBuong] = -1;
                newItem[KcbLichsuDoituongKcb.Columns.IdGiuong] = -1;
            }
            else
            {
                if (objLuotkham.NgayNhapvien.Value >= dtFromDate.Value)
                {
                    newItem[KcbLichsuDoituongKcb.Columns.TrangthaiNoitru] = 1;
                    newItem[KcbLichsuDoituongKcb.Columns.IdKhoanoitru] = -1;
                    newItem[KcbLichsuDoituongKcb.Columns.IdRavien] = -1;
                    newItem[KcbLichsuDoituongKcb.Columns.IdBuong] = -1;
                    newItem[KcbLichsuDoituongKcb.Columns.IdGiuong] = -1;
                }
                else
                {
                    newItem[KcbLichsuDoituongKcb.Columns.TrangthaiNoitru] = 0;
                    newItem[KcbLichsuDoituongKcb.Columns.IdKhoanoitru] = -1;
                    newItem[KcbLichsuDoituongKcb.Columns.IdRavien] = -1;
                    newItem[KcbLichsuDoituongKcb.Columns.IdBuong] = -1;
                    newItem[KcbLichsuDoituongKcb.Columns.IdGiuong] = -1;
                }
            }
            int id=-1;
            if (Utility.Int32Dbnull(newItem[KcbLichsuDoituongKcb.Columns.IdLichsuDoituongKcb], -1) !=-1)
            {
                id=Utility.Int32Dbnull(newItem[KcbLichsuDoituongKcb.Columns.IdLichsuDoituongKcb], -1);
                m_dtData.AcceptChanges();
                Utility.SetMsg(lblMsg, "Sửa thông tin hồ sơ đối tượng KCB cho Bệnh nhân thành công", false);
            }
            else
            {
                id= Utility.Int32Dbnull(Utility.GetHHMMSS(DateTime.Now), 1) * -1;
                newItem[KcbLichsuDoituongKcb.Columns.IdLichsuDoituongKcb] =id;
                m_dtData.Rows.Add(newItem);
                Utility.SetMsg(lblMsg, "Thêm mới thông tin hồ sơ đối tượng KCB cho Bệnh nhân thành công", false);
            }
            Utility.GotoNewRowJanus(grdList, KcbLichsuDoituongKcb.Columns.IdLichsuDoituongKcb, id.ToString());
        }

        #endregion




    }
}
