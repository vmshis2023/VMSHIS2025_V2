using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX;
using Janus.Windows.UI.StatusBar;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.Classes;
using VMS.HIS.DAL;
using VNS.HIS.UI.Classess;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.HIS.UI.THANHTOAN;
using VNS.Libs;
using VNS.Libs.AppUI;
using VNS.Properties;
using VMS.QMS;

namespace VNS.HIS.UI.Forms.NGOAITRU
{
    public partial class Frm_KCB_DANGKY_DICHVU : Form
    {
        public delegate void OnActionSuccess();
        public event OnActionSuccess _OnActionSuccess;
        public string Maluotkham = "";
        public int _mabenhnhan = -1;
        readonly KCB_DANGKY _kcbDangky = new KCB_DANGKY();
        readonly KCB_QMS _KCB_QMS = new KCB_QMS();
        private readonly string strSaveandprintPath = Application.StartupPath + @"\CAUHINH\SaveAndPrintConfig.txt";

        private string MA_DTUONG = "DV";
        private string SoBHYT = "";
        private string TrongGio = "";

        public bool m_blnCancel;
        private bool b_HasLoaded;
        private bool b_HasSecondScreen;
        private bool b_NhapNamSinh;
        public SysTrace myTrace;
        public GridEX grdList;
        private bool hasjustpressBACKKey;
        private bool isAutoFinding;
        private bool m_blnHasJustInsert = false;
        private DataTable m_DC;
        bool currentQMS = false;
        private DataTable m_dtDanhsachDichvuKCB = new DataTable();
        //private DataTable m_dtDanhmucChung;

        private DataTable m_PhongKham = new DataTable();
        private DataTable m_kieuKham;
        private DataTable m_dtChoKham = new DataTable();
        public action MEnAction = action.Insert;

        private DataTable m_dtDangkyPhongkham = new DataTable();
        private DataTable mdt_DataQuyenhuyen;
        private frm_ScreenSoKham _QMSScreen;
        public DataTable m_dtPatient = new DataTable();
        string m_strMaluotkham = "";//Lưu giá trị patientcode khi cập nhật để người dùng ko được phép gõ Patient_code lung tung
        string Args = "ALL";
        public Frm_KCB_DANGKY_DICHVU(string Agrs)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            try
            {
                this.Args = Args;
                //lblTuoi.Visible = txtTuoi.Visible = this.Args.Split('-')[0] != "KTC";
                txtTEN_BN.CharacterCasing = globalVariables.CHARACTERCASING == 0
                                                ? CharacterCasing.Normal
                                                : CharacterCasing.Upper;

                dtCreateDate.Value = globalVariables.SysDate;

                InitEvents();
                CauHinhQMS();
                CauHinhKCB();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
        }

        private void InitEvents()
        {
            txtMaLankham.LostFocus += txtMaLankham_LostFocus;
            txtExamtypeCode._OnSelectionChanged += new UCs.AutoCompleteTextbox.OnSelectionChanged(txtExamtypeCode__OnSelectionChanged);
            txtPhongkham._OnEnterMe += new VNS.HIS.UCs.AutoCompleteTextbox.OnEnterMe(txtPhongkham__OnEnterMe);
            txtKieuKham._OnEnterMe += new VNS.HIS.UCs.AutoCompleteTextbox.OnEnterMe(txtKieuKham__OnEnterMe);
            txtTrieuChungBD._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtTrieuChungBD__OnShowData);
            txtDantoc._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtDantoc__OnShowData);
            txtNgheNghiep._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtNgheNghiep__OnShowData);

            txtLoaiBN._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtLoaiBN__OnShowData);
            txtLoaiBN._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtLoaiBN__OnSaveAs);
            txtKieuKham._OnSelectionChanged += new VNS.HIS.UCs.AutoCompleteTextbox.OnSelectionChanged(txtKieuKham__OnSelectionChanged);
            cboPatientSex.SelectedIndex = 0;
            txtPhongkham._OnSelectionChanged += new VNS.HIS.UCs.AutoCompleteTextbox.OnSelectionChanged(txtPhongkham__OnSelectionChanged);

        }
        private void AutoLoadKieuKham()
        {
            try
            {
                if (Utility.Int32Dbnull(txtIDKieuKham.Text, -1) == -1 || Utility.Int32Dbnull(txtIDPkham.Text, -1) == -1)
                {
                    cboKieuKham.Text = @"CHỌN PHÒNG KHÁM";
                    cboKieuKham.SelectedIndex = -1;
                    return;
                }
                DataRow[] arrDr =
                    m_dtDanhsachDichvuKCB.Select("(ma_doituong_kcb='ALL' OR ma_doituong_kcb='" + MA_DTUONG + "') AND id_kieukham=" +
                                                  txtIDKieuKham.Text.Trim() + " AND  id_phongkham=" + txtIDPkham.Text.Trim());
                //nếu ko có đích danh phòng thì lấy dịch vụ bất kỳ theo kiểu khám và đối tượng
                if (arrDr.Length <= 0)
                    arrDr = m_dtDanhsachDichvuKCB.Select("(ma_doituong_kcb='ALL' OR ma_doituong_kcb='" + MA_DTUONG + "') AND id_kieukham=" +
                                                  txtIDKieuKham.Text.Trim() + " AND id_phongkham=-1 ");
                if (arrDr.Length <= 0)
                {
                    cboKieuKham.Text =@"CHỌN PHÒNG KHÁM";
                    cboKieuKham.SelectedIndex = -1;
                    return;
                }
                else
                {
                    cboKieuKham.Text = arrDr[0][DmucDichvukcb.Columns.TenDichvukcb].ToString();
                    txtIDPkham.Text = arrDr[0][DmucDichvukcb.Columns.IdPhongkham].ToString();
                    return;
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
            finally
            {
                AutoLoad = false;
            }
        }

       
        bool AutoLoad = false;
        void txtPhongkham__OnEnterMe()
        {
            AutoLoad = true;
            AutoLoadKieuKham();
        }

        void txtKieuKham__OnEnterMe()
        {
            AutoLoad = true;
            AutoLoadKieuKham();
        }

        void txtPhongkham__OnSelectionChanged()
        {
            AutoLoadKieuKham();
        }

        void txtKieuKham__OnSelectionChanged()
        {
            AutoLoadKieuKham();
        }

        void txtNgheNghiep__OnShowData()
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(txtNgheNghiep.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtNgheNghiep.myCode;
                txtNgheNghiep.Init();
                txtNgheNghiep.SetCode(oldCode);
                txtNgheNghiep.Focus();
            }
        }

        void txtDantoc__OnShowData()
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(txtDantoc.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtDantoc.myCode;
                txtDantoc.Init();
                txtDantoc.SetCode(oldCode);
                txtDantoc.Focus();
            }
        }
        void txtLoaiBN__OnSaveAs()
        {
            if (Utility.DoTrim(txtLoaiBN.Text) == "") return;
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(txtLoaiBN.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txtLoaiBN.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtLoaiBN.myCode;
                txtLoaiBN.Init();
                txtLoaiBN.SetCode(oldCode);
                txtLoaiBN.Focus();
            }
        }

        void txtLoaiBN__OnShowData()
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(txtLoaiBN.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtLoaiBN.myCode;
                txtLoaiBN.Init();
                txtLoaiBN.SetCode(oldCode);
                txtLoaiBN.Focus();
            }
        }

        void txtTrieuChungBD__OnShowData()
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(txtTrieuChungBD.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtTrieuChungBD.myCode;
                txtTrieuChungBD.Init();
                txtTrieuChungBD.SetCode(oldCode);
                txtTrieuChungBD.Focus();
            }
        }



        void txtExamtypeCode__OnSelectionChanged()
        {
            cboKieuKham.Text = txtMyNameEdit.Text;
            cboKieuKham.Value = txtExamtypeCode.MyID;
            txtKieuKham.Text = cboKieuKham.Text;
            txtIDKieuKham.Text = Utility.sDbnull(txtExamtypeCode.MyID);
        }



        private void CauHinhKCB()
        {

            dtpBOD.Value = globalVariables.SysDate;
            dtpBOD.CustomFormat = PropertyLib._KCBProperties.Nhapngaythangnamsinh ? "dd/MM/yyyy HH:mm" : "yyyy";
            txtTuoi.Enabled = dtpBOD.CustomFormat == "yyyy";
            lblLoaituoi.Visible = dtpBOD.CustomFormat != "yyyy";
            mnuBOD.Checked = dtpBOD.CustomFormat != "yyyy";
            if (PropertyLib._KCBProperties != null)
            {
                chkTudongthemmoi.Checked = PropertyLib._KCBProperties.Tudongthemmoi;
                //cmdThanhToanKham.Enabled = PropertyLib._KCBProperties.Chophepthanhtoan;
                //cmdThanhToanKham.Visible = cmdThanhToanKham.Enabled;

                grdRegExam.RootTable.Columns["colThanhtoan"].Visible = PropertyLib._KCBProperties.Chophepthanhtoan && (PropertyLib._KCBProperties.Kieuhienthi == Kieuhienthi.Trenluoi || PropertyLib._KCBProperties.Kieuhienthi == Kieuhienthi.Cahai);
                grdRegExam.RootTable.Columns["colDelete"].Visible = PropertyLib._KCBProperties.Kieuhienthi == Kieuhienthi.Trenluoi || PropertyLib._KCBProperties.Kieuhienthi == Kieuhienthi.Cahai;
                grdRegExam.RootTable.Columns["colIn"].Visible = PropertyLib._KCBProperties.Kieuhienthi == Kieuhienthi.Trenluoi || PropertyLib._KCBProperties.Kieuhienthi == Kieuhienthi.Cahai;
                pnlnutchucnang.Visible = PropertyLib._KCBProperties.Kieuhienthi != Kieuhienthi.Trenluoi;
                pnlnutchucnang.Height = PropertyLib._KCBProperties.Kieuhienthi == Kieuhienthi.Trenluoi ? 0 : 33;

                pnlChonKieukham.Visible = !PropertyLib._KCBProperties.GoMaDvu;
                pnlChonphongkham.Visible = PropertyLib._KCBProperties.GoMaDvu;

            }
        }
        private void CauHinhQMS()
        {

            if (PropertyLib._HISQMSProperties != null)
            {
                if (PropertyLib._HISQMSProperties.IsQMS)
                {
                    pnlTieuDe.SendToBack();
                    // lnkRestoreIgnoreQMS.Enabled = true;
                }
                else
                {
                    //lnkRestoreIgnoreQMS.Enabled = false;
                    pnlTieuDe.BringToFront();
                }

                pThongTinQMS.Enabled = PropertyLib._HISQMSProperties.IsQMS && Nhieuhon2Manhinh();
                if (!b_HasLoaded) return;
                if (!PropertyLib._HISQMSProperties.IsQMS)//Nếu chạy QMS và tạm dừng
                {
                    try2StopQMS();
                }
                else
                {
                    try2StopQMS();
                    ShowScreen();
                }
                currentQMS = PropertyLib._HISQMSProperties.IsQMS;
            }
        }
        private bool isQMSActive(string name)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Text == name)
                {
                    return true;
                }
            }
            return false;
        }
        private void ShowQMSOnScreen2()
        {
            try
            {
                Screen[] sc;
                sc = Screen.AllScreens;
                IEnumerable<Screen> query = from mh in Screen.AllScreens
                                            select mh;
                //get all the screen width and heights

                if (PropertyLib._HISQMSProperties.TestMode || query.Count() >= 2)
                {
                    _QMSScreen = new frm_ScreenSoKham();
                    if (!isQMSActive(_QMSScreen.Name))
                    {
                        if (PropertyLib._HISQMSProperties.TestMode)
                        {
                            _QMSScreen.FormBorderStyle = FormBorderStyle.Sizable;
                            _QMSScreen.Size = new Size(200, 200);
                        }
                        else
                            _QMSScreen.FormBorderStyle = FormBorderStyle.None;
                        if (query.Count() >= 2)
                        {
                            _QMSScreen.Left = sc[1].Bounds.Width;
                            _QMSScreen.Top = sc[1].Bounds.Height;
                            _QMSScreen.StartPosition = FormStartPosition.CenterScreen;
                            _QMSScreen.Location = sc[1].Bounds.Location;
                            var p = new Point(sc[1].Bounds.Location.X, sc[1].Bounds.Location.Y);
                            _QMSScreen.Location = p;
                        }
                        else
                        {
                            _QMSScreen.Left = 0;
                            _QMSScreen.Top = 0;
                            _QMSScreen.StartPosition = FormStartPosition.Manual;
                        }
                        if (!PropertyLib._HISQMSProperties.TestMode)
                            _QMSScreen.WindowState = FormWindowState.Maximized;
                        else
                            _QMSScreen.WindowState = FormWindowState.Normal;
                        _QMSScreen.Show();
                        //b_HasScreenmonitor = true;
                        txtSoQMS_TextChanged(txtSoQMS, new EventArgs());
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        private void txtSoQMS_TextChanged(object sender, EventArgs e)
        {
            _QMSScreen.SetQmsValue(IdQMS, txtSoQMS.Text, chkUuTien.Checked ? 1 : 0);
        }
        private void ShowScreen()
        {
            try
            {
                if (PropertyLib._HISQMSProperties == null)
                    PropertyLib._HISQMSProperties = PropertyLib.GetHISQMSProperties();
                if (PropertyLib._HISQMSProperties != null)
                {
                    if (PropertyLib._HISQMSProperties.IsQMS)
                    {
                        if (!globalVariables.b_QMS_Stop)
                        {
                            ShowQMSOnScreen2();
                            LaySokham(2);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
        }
        int QMS_IdDichvuKcb = 0;
        long IdQMS = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        private void LaySokham(int status)
        {

            try
            {
                if (PropertyLib._HISQMSProperties.TestMode || b_HasSecondScreen)
                {
                    int sokham = Utility.Int32Dbnull(txtSoQMS.Text);
                    QMS_IdDichvuKcb = -1;
                    IdQMS = -1;
                    string sSoKham = Utility.sDbnull(sokham);
                    if (!globalVariables.b_QMS_Stop)
                    {
                        if (globalVariables.MA_KHOA_THIEN == "KYC")//Chỉ có duy nhất số thường
                        {
                            _KCB_QMS.LaySoKhamQMS(PropertyLib._HISQMSProperties.MaQuay, globalVariables.MA_KHOA_THIEN, PropertyLib._HISQMSProperties.MaDoituongKCB, ref sokham, ref QMS_IdDichvuKcb, ref IdQMS, (byte)0, "TIEPDON", PropertyLib._HISQMSProperties.LoaiQMS_bo);
                        }
                        else//Các khoa khác
                        {
                            int isUuTien = 0;

                            if (PropertyLib._HISQMSProperties.Chopheplaysouutien)
                            {
                                SqlQuery sqlQuery1 = new Select().From(KcbQm.Schema)
                                    .Where(KcbQm.Columns.MaKhoakcb).IsEqualTo(globalVariables.MA_KHOA_THIEN)
                                    .And(KcbQm.Columns.TrangThai).In(0, 1)
                                    .AndExpression(KcbQm.Columns.MaDoituongKcb).IsEqualTo("ALL").Or(KcbQm.Columns.MaDoituongKcb).IsEqualTo(PropertyLib._HISQMSProperties.MaDoituongKCB).CloseExpression()
                                    .And(KcbQm.Columns.UuTien).IsEqualTo(1)
                                    .And(KcbQm.Columns.LoaiQms).IsEqualTo(PropertyLib._HISQMSProperties.LoaiQMS);
                                isUuTien = sqlQuery1.GetRecordCount() > 0 ? 1 : 0;
                            }
                            if (PropertyLib._HISQMSProperties.Chilaysouutien)
                                isUuTien = 1;
                            if (!PropertyLib._HISQMSProperties.Chopheplaysouutien)
                                isUuTien = 0;
                            chkUuTien.Checked = isUuTien == 1;

                            Utility.SetMsg(lblQMS, isUuTien == 1 ? "SỐ ƯU TIÊN" : (isUuTien == 0 ? "SỐ THƯỜNG" : PropertyLib._HISQMSProperties.TenLoaiQMS), isUuTien == 1);
                            //_KCB_QMS.LaySoKhamQMS(PropertyLib._HISQMSProperties.MaQuay, globalVariables.MA_KHOA_THIEN, PropertyLib._HISQMSProperties.MaDoituongKCB, ref sokham, ref QMS_IdDichvuKcb, ref IdQMS, (byte)isUuTien, PropertyLib._HISQMSProperties.LoaiQMS, PropertyLib._HISQMSProperties.LoaiQMS_bo);

                        }
                    }
                    if (sokham < 10)
                    {
                        sSoKham = Utility.FormatNumberToString(sokham, "00");
                    }
                    else
                    {
                        sSoKham = Utility.sDbnull(sokham);
                    }

                    int tongso = Utility.Int32Dbnull(txtTS.Text);
                    string sTongSo = Utility.sDbnull(tongso);
                    //Lấy tổng số QMS của khoa trong ngày
                    StoredProcedure sp = SPs.QmsGetQMSCount(globalVariables.MA_KHOA_THIEN, PropertyLib._HISQMSProperties.MaDoituongKCB, tongso, PropertyLib._HISQMSProperties.LoaiQMS, 0);
                    sp.Execute();
                    tongso = Utility.Int32Dbnull(sp.OutputValues[0]);
                    int tongsoUuTien = 0;
                    //Lấy tổng số QMS ưu tiên của khoa trong ngày
                    sp = SPs.QmsGetQMSCount(globalVariables.MA_KHOA_THIEN, "ALL", tongsoUuTien, PropertyLib._HISQMSProperties.LoaiQMS, 1);
                    sp.Execute();
                    tongsoUuTien = Utility.Int32Dbnull(sp.OutputValues[0]);
                    if (!PropertyLib._HISQMSProperties.Chopheplaysouutien)
                        tongsoUuTien = 0;
                    int Total = tongso + tongsoUuTien;
                    if (PropertyLib._HISQMSProperties.Chilaysouutien)
                        Total = tongsoUuTien;
                    UIAction.SetTextStatus(txtSoQMS, sSoKham, chkUuTien.Checked ? Color.Red : txtTS.ForeColor);
                    if (Total < 10)
                    {
                        sSoKham = Utility.FormatNumberToString(Total, "00");
                    }
                    else
                    {
                        sSoKham = Utility.sDbnull(Total);
                    }

                    txtTS.Text = Utility.sDbnull(sSoKham);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private bool Nhieuhon2Manhinh()
        {
            IEnumerable<Screen> query = from mh in Screen.AllScreens
                                        select mh;
            if (PropertyLib._HISQMSProperties.TestMode || query.Count() >= 2)
                return true;
            else return false;
        }
        void try2StopQMS()
        {
            try
            {
                if (_QMSScreen != null)
                {
                    new Update(KcbQm.Schema)
                  .Set(KcbQm.Columns.TrangThai).EqualTo(0)
                  .Set(KcbQm.Columns.MaQuay).EqualTo(string.Empty)
                  .Where(KcbQm.Columns.TrangThai).IsEqualTo(1)
                  .And(KcbQm.Columns.MaQuay).IsEqualTo(PropertyLib._HISQMSProperties.MaQuay)
                  .And(KcbQm.Columns.MaKhoakcb).IsEqualTo(globalVariables.MA_KHOA_THIEN)
                  .AndExpression(KcbQm.Columns.MaDoituongKcb).IsEqualTo("ALL").Or(KcbQm.Columns.MaDoituongKcb).IsEqualTo(PropertyLib._HISQMSProperties.MaDoituongKCB).CloseExpression()
                  .And(KcbQm.Columns.UuTien).IsEqualTo(chkUuTien.Checked ? 1 : 0)
                  .And(KcbQm.Columns.LoaiQms).IsEqualTo(PropertyLib._HISQMSProperties.LoaiQMS)
                  .Execute();
                    if (_QMSScreen != null && (!isQMSActive(_QMSScreen.Name)))
                    {
                        _QMSScreen.Close();
                        _QMSScreen.Dispose();
                        _QMSScreen = null;
                    }
                }
            }
            catch
            {
            }
        }

        private void grdListKhoa_FormattingRow(object sender, RowLoadEventArgs e)
        {

        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {

            frm_Properties frm = new frm_Properties(PropertyLib._KCBProperties);
            frm.ShowDialog();
            CauHinhKCB();
        }

        private void cmdQMSProperty_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._HISQMSProperties);
            frm.ShowDialog();
            CauHinhQMS();
        }

        private void txtSoBA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtSoBA.Text) != "")
            {
                KcbBenhAn _objBA = new Select().From(KcbBenhAn.Schema).Where(KcbBenhAn.Columns.SoBenhAn).IsEqualTo(txtSoBA.Text).ExecuteSingle<KcbBenhAn>();
                if (_objBA != null)
                {
                    txtMaLankham.Text = _objBA.MaLuotkham;
                    txtMaLankham_KeyDown(txtMaLankham, e);
                }
            }
        }
        void ResetLuotkham()
        {
            new Update(KcbDmucLuotkham.Schema)
                      .Set(KcbDmucLuotkham.Columns.TrangThai).EqualTo(0)
                      .Set(KcbDmucLuotkham.Columns.UsedBy).EqualTo(DBNull.Value)
                      .Set(KcbDmucLuotkham.Columns.StartTime).EqualTo(DBNull.Value)
                      .Set(KcbDmucLuotkham.Columns.EndTime).EqualTo(null)
                      .Where(KcbDmucLuotkham.Columns.MaLuotkham).IsEqualTo(m_strMaluotkham)
                      .And(KcbDmucLuotkham.Columns.TrangThai).IsEqualTo(1)
                      .And(KcbDmucLuotkham.Columns.UsedBy).IsEqualTo(globalVariables.UserName)
                      .And(KcbDmucLuotkham.Columns.Nam).IsEqualTo(globalVariables.SysDate.Year).Execute();
        }
        private void txtMaLankham_KeyDown(object sender, KeyEventArgs e)
        {

            //Chỉ reset lại mã lượt khám cũ nếu mã cũ và mã mới khác nhau. Tránh việc bỏ mã khi người dùng thao tác: Thêm mới-->Enter trên ô Mã lượt khám
            if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtMaLankham.Text) != "" && Utility.DoTrim(txtMaLankham.Text) != m_strMaluotkham)
            {
                isAutoFinding = true;
                string patient_ID = Utility.GetYY(globalVariables.SysDate) +
                                    Utility.FormatNumberToString(Utility.Int32Dbnull(txtMaLankham.Text, 0), "000000");
                txtMaLankham.Text = patient_ID;
                ResetLuotkham();
                FindPatientIDbyMaLanKham(txtMaLankham.Text.Trim());
                isAutoFinding = false;
            }
        }

        private void ModifyButtonCommandRegExam()
        {
            if (Utility.isValidGrid(grdRegExam))
            {
                cmdXoaKham.Enabled = Utility.Int32Dbnull(grdRegExam.GetValue(KcbDangkyKcb.Columns.TrangthaiThanhtoan), 0) == 0;
                cmdInBienlai.Enabled = !cmdXoaKham.Enabled;
                cmdInhoadon.Enabled = cmdInBienlai.Enabled;
                cmdThanhToanKham.Text = cmdXoaKham.Enabled ? "T.Toán" : "Hủy TT";
                cmdThanhToanKham.Tag = cmdXoaKham.Enabled ? "TT" : "HTT";
                cmdInPhieuKham.Enabled = grdRegExam.RowCount > 0 && grdRegExam.CurrentRow.RowType == RowType.Record;

                grdRegExam.RootTable.Columns["colThanhtoan"].ButtonText = cmdThanhToanKham.Text;
                pnlPrint.Visible = !cmdXoaKham.Enabled;
                cmdInBienlai.Visible = !cmdXoaKham.Enabled;
                cmdInhoadon.Visible = cmdInBienlai.Visible;
                //cmdThanhToanKham.Visible = cmdXoaKham.Enabled;
            }
            else
            {
                cmdXoaKham.Enabled = cmdInBienlai.Enabled = cmdInPhieuKham.Enabled = cmdThanhToanKham.Enabled = false;
                pnlPrint.Visible = false;
                cmdInBienlai.Visible = false;
                cmdInhoadon.Visible = false;
                cmdThanhToanKham.Visible = false;
            }
        }
        private void ModifyCommand()
        {
            cmdXoaKham.Enabled = grdRegExam.RowCount > 0;
            cmdInPhieuKham.Enabled = grdRegExam.RowCount > 0;
            cmdSave.Enabled = Utility.DoTrim(txtTEN_BN.Text).Length > 0;
            ModifyButtonCommandRegExam();
            ModifyQms();
        }
        private void ModifyQms()
        {
            cmdStop.Enabled = !globalVariables.b_QMS_Stop;
            cmdStart.Enabled = globalVariables.b_QMS_Stop;
            cmdGoiSoKham.Enabled = !globalVariables.b_QMS_Stop;
            cmdXoaSoKham.Enabled = !globalVariables.b_QMS_Stop;
            txtSoQMS.Enabled = !globalVariables.b_QMS_Stop;
        }
        private void setMsg(UIStatusBarPanel item, string msg, bool isError)
        {
            try
            {
                item.Text = msg;
                if (isError) item.FormatStyle.ForeColor = Color.Red;
                else item.FormatStyle.ForeColor = Color.DarkBlue;

                Application.DoEvents();
            }
            catch
            {
            }
        }
        #region "Su kien autocomplete của thành phố"

        private bool AllowTextChanged;
        private string _rowFilter = "1=1";


        #endregion

        #region "Su kien autocomplete của quận huyện"

        private string _rowFilterQuanHuyen = "1=1";
        #endregion
        void SetActionStatus()
        {
            lblStatus.Text = MEnAction == action.Insert ? "BỆNH NHÂN MỚI" : (MEnAction == action.Add ? "THÊM LẦN KHÁM" : "CẬP NHẬT");
        }
        private void ClearControl()
        {
            setMsg(uiStatusBar1.Panels["MSG"], "", false);
            m_blnHasJustInsert = false;
            txtSolankham.Text = "1";
            txtTEN_BN.Clear();
            dtpBOD.Value = globalVariables.SysDate;
            txtTuoi.Clear();
            txtCMT.Clear();
            txtNgheNghiep.Clear();
            txtDiachi.Clear();
            // txtDantoc__OnShowData();
            //  txtDantoc.Clear();
            txtTrieuChungBD.Clear();
            txtSoDT.Clear();
            txtNguoiLienhe.Clear();
            txtKieuKham.ClearMe();
            txtPhongkham.ClearMe();
            txtSoBA.Clear();
            txtLoaiBN.SetCode("-1");
            txtNoigioithieu.Clear();
            txtEmail.Clear();
            if (THU_VIEN_CHUNG.IsNgoaiGio())
            {
                this.Text = "Bệnh nhân đang khám dịch vụ ngoài giờ";
            }

            ModifyCommand();
            EnumerableRowCollection<DataRow> query = from kham in m_dtDanhsachDichvuKCB.AsEnumerable()
                                                     select kham;
            if (query.Count() > 0)
            {
                cboKieuKham.SelectedIndex = -1;
                cboKieuKham.Text = "CHỌN PHÒNG KHÁM";
            }
            txtTEN_BN.Focus();
            AllowTextChanged = true;
            //Chuyển về trạng thái thêm mới
            MEnAction = action.Insert;
            if (PropertyLib._KCBProperties.SexInput) cboPatientSex.SelectedIndex = -1;
            m_dtDangkyPhongkham.Clear();
            if (MEnAction == action.Insert)
            {
                dtpInputDate.Value = globalVariables.SysDate;
                dtCreateDate.Value = globalVariables.SysDate;
            }
            SetActionStatus();
            DmucDichvukcb objDmucDichvukcb = DmucDichvukcb.FetchByID(QMS_IdDichvuKcb);
            if (objDmucDichvukcb != null)
            {

                txtKieuKham.SetId(objDmucDichvukcb.IdKieukham);
                txtPhongkham.SetId(objDmucDichvukcb.IdPhongkham);
                txtExamtypeCode.SetCode(objDmucDichvukcb.MaDichvukcb);
                cboKieuKham.Text = objDmucDichvukcb.TenDichvukcb;

            }
            else
            {
                txtExamtypeCode.SetCode("-1");
            }
            txtExamtypeCode.TabStop = objDmucDichvukcb == null;
            cboKieuKham.TabStop = objDmucDichvukcb == null;
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
                if (temdt.Rows.Count <= 0)
                {
                    ClearControl();
                    return;
                }
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

        byte _IdLoaidoituongKcb = 1;
        Int16 _IdDoituongKcb = 1;
        string _MaDoituongKcb = "DV";
        string _TenDoituongKcb = "Dịch vụ";
        bool _allowAgeChanged = true;
        decimal PtramBhytCu = 0m;
        decimal PtramBhytGocCu = 0m;
        private KcbLuotkham objLuotkham = null;
        private DmucDoituongkcb _objDoituongKcb = null;
        KcbDanhsachBenhnhan objBenhnhan = null;

        private void LoadThongtinBenhnhan()
        {
            PtramBhytCu = 0m;
            PtramBhytGocCu = 0m;
            AllowTextChanged = false;
            objBenhnhan = KcbDanhsachBenhnhan.FetchByID(txtMaBN.Text);
            if (objBenhnhan != null)
            {
                txtTEN_BN.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan);
                txtSoDT.Text = Utility.sDbnull(objBenhnhan.DienThoai);

                txtSoBATCQG.Text = Utility.sDbnull(objBenhnhan.SoTiemchungQg);
                txtDiachi._Text = Utility.sDbnull(objBenhnhan.DiaChi);
                _allowAgeChanged = false;
                if (objBenhnhan.NgaySinh != null) dtpBOD.Value = objBenhnhan.NgaySinh.Value;
                else dtpBOD.Value = new DateTime((int) objBenhnhan.NamSinh, 1, 1);

                txtNgheNghiep._Text = Utility.sDbnull(objBenhnhan.NgheNghiep);
                cboPatientSex.SelectedIndex = Utility.GetSelectedIndex(cboPatientSex,
                    Utility.sDbnull(objBenhnhan.IdGioitinh));
                if (Utility.Int32Dbnull(objBenhnhan.DanToc) > 0)
                {

                    //DmucChung objdantoc =
                    //    new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("DAN_TOC").And(
                    //        DmucChung.Columns.Ma).IsEqualTo(objBenhnhan.DanToc).ExecuteSingle<DmucChung>();
                    var objdantoc = (from p in globalVariables.gv_dtDantoc.AsEnumerable()
                        where p[DmucChung.Columns.Ma].Equals(objBenhnhan.DanToc)
                        select p).FirstOrDefault();
                    //  txtDantoc.myCode = objBenhnhan.DanToc;
                    if (objdantoc != null)
                    {
                        txtDantoc.SetCode(objBenhnhan.DanToc);
                        txtDantoc._Text = Utility.sDbnull(objdantoc["TEN"], "Kinh");
                    }
                }


                txtEmail.Text = Utility.sDbnull(objBenhnhan.Email);
                txtCMT.Text = Utility.sDbnull(objBenhnhan.Cmt);
                objLuotkham = new Select().From(KcbLuotkham.Schema)
                    .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(txtMaLankham.Text.Trim(), ""))
                    .And(KcbLuotkham.Columns.IdBenhnhan)
                    .IsEqualTo(Utility.Int32Dbnull(txtMaBN.Text.Trim(), -1))
                    .ExecuteSingle<KcbLuotkham>();
                if (objLuotkham != null)
                {

                    KcbDangkySokham objSoKCB = new Select().From(KcbDangkySokham.Schema)
                        .Where(KcbDangkySokham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(KcbDangkySokham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<KcbDangkySokham>();
                    if (objSoKCB != null)
                    {
                        chkLaysokham.Checked = true;
                        txtSoKcb.SetCode(objSoKCB.MaSokcb);
                    }
                    else
                    {
                        chkLaysokham.Checked = false;
                        txtSoKcb.SetDefaultItem();
                    }
                    txtSoBA.Text = Utility.sDbnull(objLuotkham.SoBenhAn, "-1");
                    m_strMaluotkham = objLuotkham.MaLuotkham;
                    txtLoaikham.SetCode(objLuotkham.KieuKham);
                    txtSolankham.Text = Utility.sDbnull(objLuotkham.SolanKham);
                    _IdDoituongKcb = objLuotkham.IdDoituongKcb;
                    dtpInputDate.Value = objLuotkham.NgayTiepdon;
                    // dtCreateDate.Value = objLuotkham.NgayTiepdon;
                    txtEmail.Text = objLuotkham.Email;
                    txtNguoiLienhe.Text = objBenhnhan.NguoiLienhe;

                    txtNoigioithieu.Text = objLuotkham.NoiGioithieu;
                    txtLoaiBN.SetCode(objLuotkham.NhomBenhnhan);
                    if (objLuotkham.LoaiTuoi > 0)
                    {
                        dtpBOD.CustomFormat = "dd/MM/yyyy HH:mm";
                        lblLoaituoi.Visible = true;
                    }
                    else
                    {
                        dtpBOD.CustomFormat = "yyyy";
                        lblLoaituoi.Visible = false;
                    }
                    if (dtpBOD.CustomFormat != "yyyy")
                    {
                        txtTuoi.Text = Utility.sDbnull(objLuotkham.Tuoi, "0");
                        UIAction.SetText(lblLoaituoi,
                            objLuotkham.LoaiTuoi == 0 ? "" : (objLuotkham.LoaiTuoi == 1 ? "Tháng" : "Tuần"));
                    }
                    else
                    {
                        txtTuoi.Text =
                            Utility.sDbnull(globalVariables.SysDate.Year - Utility.Int32Dbnull(objBenhnhan.NamSinh, 0));
                    }
                    _allowAgeChanged = true;
                    _MaDoituongKcb = Utility.sDbnull(objLuotkham.MaDoituongKcb);
                    _objDoituongKcb =
                        new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(
                            _MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();

                    ChangeObjectRegion();
                    PtramBhytCu = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                    PtramBhytGocCu = Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0);
                    _IdDoituongKcb = _objDoituongKcb.IdDoituongKcb;
                    _TenDoituongKcb = _objDoituongKcb.TenDoituongKcb;
                    cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _MaDoituongKcb);

                    txtTrieuChungBD._Text = Utility.sDbnull(objLuotkham.TrieuChung);
                }
            }
        }

        void ChangeObjectRegion()
        {
            if (_objDoituongKcb == null) return;
            _IdDoituongKcb = _objDoituongKcb.IdDoituongKcb;
            _IdLoaidoituongKcb = _objDoituongKcb.IdLoaidoituongKcb;
            _TenDoituongKcb = _objDoituongKcb.TenDoituongKcb;
            PtramBhytCu = _objDoituongKcb.PhantramTraituyen.Value;
            PtramBhytGocCu = PtramBhytCu;
            txtTEN_BN.Focus();
        }
        private void LaydanhsachdangkyKcb()
        {
            m_dtDangkyPhongkham = _kcbDangky.LayDsachCongkhamDadangki(txtMaLankham.Text, Utility.Int64Dbnull(txtMaBN.Text),0);
            Utility.SetDataSourceForDataGridEx(grdRegExam, m_dtDangkyPhongkham, false, true, "", "Id_kham desc");
            //if (grdRegExam.RowCount > 0)
            //{
            //    txtTongChiPhiKham.Text = (m_dtDangkyPhongkham.Compute("SUM(" + KcbDangkyKcb.Columns.RegFee +  ")","1=1")
            //        +m_dtDangkyPhongkham.Compute("SUM("  + KcbDangkyKcb.Columns.SurchargePrice + ")","1=1").ToString()).ToString();
            //}
            //else
            //{
            //    txtTongChiPhiKham.Text = "0";
            //}
            ModifyButtonCommandRegExam();
        }

        private bool NotPayment(string patient_ID, ref string NgayKhamGanNhat)
        {
            try
            {
                // DataTable temdt = _KCB_DANGKY.KcbLaythongtinBenhnhan(Utility.Int64Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan)));
                DataTable temdt = _kcbDangky.KcbLaythongtinBenhnhan(Utility.Int64Dbnull(patient_ID));
                if (temdt != null && Utility.ByteDbnull(temdt.Rows[0][KcbLuotkham.Columns.TrangthaiNoitru], 0) > 0 && Utility.ByteDbnull(temdt.Rows[0][KcbLuotkham.Columns.TrangthaiNoitru], 0) < 4)
                {
                    Utility.ShowMsg("Bệnh nhân đang ở trạng thái nội trú và chưa ra viện nên không thể thêm lần khám mới. Đề nghị bạn xem lại");
                    return true;
                }

                if (temdt != null && temdt.Rows.Count <= 0)
                {
                    NgayKhamGanNhat = "NOREG";
                    //Chưa đăngký khám lần nào(mới gõ thông tin BN)-->Trạng thái sửa
                    return true; //Chưa thanh toán-->Cho về trạng thái sửa
                }
                if (temdt != null && temdt.Rows.Count > 0 && temdt.Select("trangthai_thanhtoan=0").Length > 0)
                {
                    NgayKhamGanNhat = temdt.Select("trangthai_thanhtoan=0", "ma_luotkham")[0]["Ngay_Kham"].ToString();
                    return true; //Chưa thanh toán-->Có thể cho về trạng thái sửa
                }
                else //Đã thanh toán--.Thêm lần khám mới
                    return false;
            }
            catch (Exception ex)
            {
                return false; //Đã thanh toán--.Thêm lần khám mới
            }
        }

        private void AutoFindLastExamandFetchIntoControls(string patientID, string sobhyt)
        {
            try
            {
                if (!Utility.CheckLockObject(m_strMaluotkham, "Tiếp đón", "TD"))
                    return;
                //Trả lại mã lượt khám nếu chưa được dùng đến


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
                    MEnAction = action.Update;
                    AllowTextChanged = false;
                    LoadThongtinBenhnhan();
                   // CanhbaoInphoi();
                    LaydanhsachdangkyKcb();
                    string ngay_kham = globalVariables.SysDate.ToString("dd/MM/yyyy");
                    if (!NotPayment(txtMaBN.Text.Trim(), ref ngay_kham))//Đã thanh toán-->Kiểm tra ngày hẹn xem có được phép khám tiếp
                    {

                        KcbChandoanKetluan _Info = new Select().From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.MaLuotkhamColumn).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<KcbChandoanKetluan>();
                        if (_Info != null && _Info.SongayDieutri != null)
                        {
                            int SoNgayDieuTri = 0;
                            if (_Info.SongayDieutri.Value.ToString() == "")
                            {
                                SoNgayDieuTri = 0;
                            }
                            else
                            {
                                SoNgayDieuTri = _Info.SongayDieutri.Value;
                            }
                            DateTime ngaykhamcu = _Info.NgayTao; ;
                            DateTime ngaykhammoi = globalVariables.SysDate;
                            TimeSpan songay = ngaykhammoi - ngaykhamcu;

                            int kt = songay.Days;
                            int kt1 = SoNgayDieuTri - kt;
                            kt1 = Utility.Int32Dbnull(kt1);
                            // nếu khoảng cách từ lần khám trước đến ngày hiện tại lớn hơn ngày điều trị.
                            if (kt >= SoNgayDieuTri)
                            {
                                MEnAction = action.Add;
                                SinhMaLanKham();
                                //txtTongChiPhiKham.Text = "0";
                                m_dtDangkyPhongkham.Rows.Clear();
                                txtKieuKham.Select();
                            }
                            else if (kt < SoNgayDieuTri)
                            {
                                DialogResult dialogResult =
                                    MessageBox.Show(
                                        "Bác Sỹ hẹn :  " + SoNgayDieuTri + "ngày" + "\nNgày khám gần nhất:  " +
                                        ngaykhamcu + "\nCòn: " + kt1 + " ngày nữa mới được tái khám" +
                                        "\nBạn có muốn tiếp tục thêm lần khám ", "Thông Báo", MessageBoxButtons.YesNo);

                                if (dialogResult == DialogResult.Yes)
                                {
                                    MEnAction = action.Add;
                                    SinhMaLanKham();
                                    //Reset dịch vụ KCB
                                    //txtTongChiPhiKham.Text = "0";
                                    m_dtDangkyPhongkham.Rows.Clear();
                                    txtKieuKham.Select();
                                }
                                else if (dialogResult == DialogResult.No)
                                {
                                    ClearControl();
                                    SinhMaLanKham();

                                    return;
                                }
                            }
                        }
                        else//Chưa thăm khám-->Để nguyên trạng thái cập nhật
                        {
                        }
                    }
                    else//Còn lần khám chưa thanh toán-->Kiểm tra
                    {
                        //nếu là ngày hiện tại thì đặt về trạng thái sửa
                        if (ngay_kham == "NOREG" || ngay_kham == globalVariables.SysDate.ToString("dd/MM/yyyy"))
                        {
                            //LoadThongtinBenhnhan();
                            if (ngay_kham == "NOREG")//Bn chưa đăng ký phòng khám nào cả. 
                            {
                                //Nếu ngày hệ thống=Ngày đăng ký gần nhất-->Sửa
                                if (globalVariables.SysDate.ToString("dd/MM/yyyy") == dtpInputDate.Value.ToString("dd/MM/yyyy"))
                                {
                                    MEnAction = action.Update;

                                    Utility.ShowMsg(
                                       "Bệnh nhân vừa được đăng ký ngày hôm nay nên hệ thống sẽ chuyển về chế độ Sửa thông tin. Nhấn OK để bắt đầu sửa");
                                    //LaydanhsachdangkyKCB();
                                    txtTEN_BN.Select();
                                }
                                else//Thêm lần khám cho ngày mới
                                {
                                    MEnAction = action.Add;
                                    SinhMaLanKham();
                                    //Reset dịch vụ KCB
                                    //txtTongChiPhiKham.Text = "0";
                                    m_dtDangkyPhongkham.Rows.Clear();
                                    txtKieuKham.Select();
                                }
                            }
                            else//Quay về trạng thái sửa
                            {
                                MEnAction = action.Update;

                                Utility.ShowMsg(
                                   "Bệnh nhân vừa được đăng ký ngày hôm nay nên hệ thống sẽ chuyển về chế độ Sửa thông tin. Nhấn OK để bắt đầu sửa");
                                //LaydanhsachdangkyKCB();
                                txtTEN_BN.Select();
                            }
                        }
                        else //Không cho phép thêm lần khám khác nếu chưa thanh toán lần khám của ngày hôm trước
                        {
                            Utility.ShowMsg(
                                "Bệnh nhân đang có lần khám chưa được thanh toán. Cần thanh toán hết các lần đến khám bệnh của Bệnh nhân trước khi thêm lần khám mới. Nhấn OK để hệ thống chuyển về trạng thái thêm mới Bệnh nhân");
                            cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
                        }
                    }
                    StatusControl();

                    ModifyCommand();
                    txtKieuKham.SetCode("-1");
                    txtPhongkham.SetCode("-1");
                    if (PropertyLib._KCBProperties.GoMaDvu)
                    {
                        txtExamtypeCode.SelectAll();
                        txtExamtypeCode.Focus();
                    }
                    else
                    {
                        txtKieuKham.SelectAll();
                        txtKieuKham.Focus();
                    }
                }
                else
                {
                    Utility.ShowMsg(
                        "Bệnh nhân này chưa có lần khám nào-->Có thể bị lỗi dữ liệu. Đề nghị liên hệ với VNS để được giải đáp");
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("AutoFindLastExam().Exception-->" + ex.Message);
            }
            finally
            {
                SetActionStatus();
                AllowTextChanged = true;
            }
        }
        private void SinhMaLanKham()
        {
            txtSolankham.Text = string.Empty;
            if (MEnAction == action.Insert)
            {
                txtMaBN.Text = "Tự sinh";
            }
            txtMaLankham.Text = THU_VIEN_CHUNG.KCB_SINH_MALANKHAM(0);
            m_strMaluotkham = txtMaLankham.Text;
            //Tạm bỏ
            //LaySoThuTuDoiTuong();
            SqlQuery sqlQuery = new Select(Aggregate.Max(KcbLuotkham.Columns.SolanKham)).From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtMaBN.Text, -1));
            var soThuTuKham = sqlQuery.ExecuteScalar<Int32>();
            txtSolankham.Text = Utility.sDbnull(soThuTuKham + 1);
        }
        private void StatusControl()
        {
            if (THU_VIEN_CHUNG.IsNgoaiGio())
            {
                this.Text = "Bệnh nhân đang khám dịch vụ ngoài giờ";
            }

        }

        private void cmdThemMoiBN_Click(object sender, EventArgs e)
        {
            ResetLuotkham();
            ClearControl();
            SinhMaLanKham();
        }

        private void cmdThemmoiDiachinh_Click(object sender, EventArgs e)
        {
            frm_themmoi_diachinh_new themmoiDiachinh = new frm_themmoi_diachinh_new();
            themmoiDiachinh.ShowDialog();
            if (themmoiDiachinh.m_blnHasChanged)
            {

                AddAutoCompleteDiaChi();
            }
        }

        private void AddAutoCompleteDiaChi()
        {
            txtDiachi.dtData = globalVariables.dtAutocompleteAddress.Copy();
            this.txtDiachi.AutoCompleteList = globalVariables.LstAutocompleteAddressSource;
            this.txtDiachi.CaseSensitive = false;
            this.txtDiachi.MinTypedCharacters = 1;
        }
        private bool blnManual;

        private void cmdGoiSoKham_Click(object sender, EventArgs e)
        {
            try
            {
                blnManual = true;
                Utility.EnableButton(cmdGoiSoKham, false);
                Utility.WaitNow(this);
                new Update(KcbQm.Schema)
                    .Set(KcbQm.Columns.TrangThai).EqualTo(3)
                    .Where(KcbQm.Columns.SoQms).IsEqualTo(Utility.Int32Dbnull(txtSoQMS.Text))
                    .And(KcbQm.Columns.MaKhoakcb).IsEqualTo(globalVariables.MA_KHOA_THIEN)
                    .And(KcbQm.Columns.TrangThai).IsEqualTo(1)
                    .And(KcbQm.Columns.MaQuay).IsEqualTo(PropertyLib._HISQMSProperties.MaQuay)
                    .AndExpression(KcbQm.Columns.MaDoituongKcb).IsEqualTo("ALL").Or(KcbQm.Columns.MaDoituongKcb).IsEqualTo(PropertyLib._HISQMSProperties.MaDoituongKCB).CloseExpression()
                    .And(KcbQm.Columns.UuTien).IsEqualTo(chkUuTien.Checked ? 1 : 0)
                    .And(KcbQm.Columns.LoaiQms).IsEqualTo(PropertyLib._HISQMSProperties.LoaiQMS)
                    .Execute();
                LaySokham(2);
                DmucDichvukcb objDmucDichvukcb = DmucDichvukcb.FetchByID(QMS_IdDichvuKcb);
                if (objDmucDichvukcb != null)
                {

                    txtKieuKham.SetId(objDmucDichvukcb.IdKieukham);
                    txtPhongkham.SetId(objDmucDichvukcb.IdPhongkham);
                    txtExamtypeCode.SetCode(objDmucDichvukcb.MaDichvukcb);
                    cboKieuKham.Text = objDmucDichvukcb.TenDichvukcb;

                }

                Thread.Sleep(Utility.Int32Dbnull(1000 * PropertyLib._HISQMSProperties.ThoiGianCho, 100));
                Utility.DefaultNow(this);
                Utility.EnableButton(cmdGoiSoKham, true);
            }
            catch (Exception)
            {
                Utility.DefaultNow(this);
                Utility.EnableButton(cmdGoiSoKham, true);
            }
            finally
            {
                Utility.DefaultNow(this);
                Utility.EnableButton(cmdGoiSoKham, true);
            }
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            globalVariables.b_QMS_Stop = true;
            try
            {
                if (_QMSScreen != null && !(isQMSActive(_QMSScreen.Name)))
                {
                    _QMSScreen.Close();
                    _QMSScreen.Dispose();
                    _QMSScreen = null;
                }
                if (PropertyLib._HISQMSProperties.IsQMS)
                {
                    Utility.EnableButton(cmdStop, false);
                    new Update(KcbQm.Schema)
                        .Set(KcbQm.Columns.TrangThai).EqualTo(0)
                        .Set(KcbQm.Columns.MaQuay).EqualTo(string.Empty)
                        .Where(KcbQm.Columns.MaQuay).IsEqualTo(PropertyLib._HISQMSProperties.MaQuay)
                        .And(KcbQm.Columns.MaKhoakcb).IsEqualTo(globalVariables.MA_KHOA_THIEN)
                        .AndExpression(KcbQm.Columns.MaDoituongKcb).IsEqualTo("ALL").Or(KcbQm.Columns.MaDoituongKcb).IsEqualTo(PropertyLib._HISQMSProperties.MaDoituongKCB).CloseExpression()
                        .And(KcbQm.Columns.UuTien).IsEqualTo(chkUuTien.Checked ? 1 : 0)
                        .And(KcbQm.Columns.LoaiQms).IsEqualTo(PropertyLib._HISQMSProperties.LoaiQMS)
                        .And(KcbQm.Columns.TrangThai).IsEqualTo(1)
                        .Execute();
                    Thread.Sleep(200);
                    Utility.EnableButton(cmdStop, true);
                }
            }
            catch (Exception exception)
            {
            }
            ModifyQms();
        }

        private void cmdXoaSoKham_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn hủy số khám {0} hay không? Nếu hủy hệ thống tự động nhảy tới số kế tiếp(Bạn có thể khôi phục lại số đã bỏ qua hoặc hủy bằng cách chọn vào mục Khôi phục số khám bị bỏ qua, hủy...)", txtSoQMS.Text), "Xác nhận hủy số khám", true))
                {
                    Utility.EnableButton(cmdXoaSoKham, false);
                    Utility.WaitNow(this);
                    new Update(KcbQm.Schema)
                        .Set(KcbQm.Columns.TrangThai).EqualTo(4)
                        .Where(KcbQm.Columns.SoQms).IsEqualTo(Utility.Int32Dbnull(txtSoQMS.Text))
                        .And(KcbQm.Columns.MaKhoakcb).IsEqualTo(globalVariables.MA_KHOA_THIEN)
                        .And(KcbQm.Columns.TrangThai).IsEqualTo(1)
                        .AndExpression(KcbQm.Columns.MaDoituongKcb).IsEqualTo("ALL").Or(KcbQm.Columns.MaDoituongKcb).IsEqualTo(PropertyLib._HISQMSProperties.MaDoituongKCB).CloseExpression()
                        .And(KcbQm.Columns.UuTien).IsEqualTo(chkUuTien.Checked ? 1 : 0)
                        .And(KcbQm.Columns.LoaiQms).IsEqualTo(PropertyLib._HISQMSProperties.LoaiQMS)
                        .Execute();
                    LaySokham(2);
                    Thread.Sleep(50);
                    Utility.DefaultNow(this);
                    Utility.EnableButton(cmdXoaSoKham, true);
                }
            }
            catch (Exception)
            {

                Utility.DefaultNow(this);
                Utility.EnableButton(cmdGoiSoKham, true);
                // throw;
            }
            finally
            {

                Utility.DefaultNow(this);
                Utility.EnableButton(cmdGoiSoKham, true);
            }
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            globalVariables.b_QMS_Stop = false;
            ShowQMSOnScreen2();
            ModifyQms();
        }

        private void chkTudongthemmoi_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._KCBProperties.Tudongthemmoi = chkTudongthemmoi.Checked;
            PropertyLib.SaveProperty(PropertyLib._KCBProperties);
        }

        private void cmdThanhToanKham_Click(object sender, EventArgs e)
        {

            if (!Utility.isValidGrid(grdRegExam)) return;
            if (Utility.sDbnull(cmdThanhToanKham.Tag, "ABCD") == "TT")
                Thanhtoan(true);
            else
                HuyThanhtoan();
        }
        private List<int> GetIDKham()
        {
            List<int> lstRegID = new List<int>();
            int IdKham = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1);
            DataRow[] arrDr = null;
            if (PropertyLib._KCBProperties.Thanhtoancaphidichvukemtheo)
                arrDr = m_dtDangkyPhongkham.Select(KcbDangkyKcb.IdKhamColumn.ColumnName + "=" + IdKham.ToString() + " OR " + KcbDangkyKcb.IdChaColumn.ColumnName + "=" + IdKham.ToString());
            else
                arrDr = m_dtDangkyPhongkham.Select(KcbDangkyKcb.IdKhamColumn.ColumnName + "=" + IdKham.ToString());
            foreach (DataRow dr in arrDr)
            {
                if (Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdThanhtoan], -1) > 0)
                {
                    IdKham = Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdKham], -1);
                    if (!lstRegID.Contains(IdKham)) lstRegID.Add(IdKham);
                }
            }
            return lstRegID;
        }
        private void HuyThanhtoan()
        {
            try
            {
                string ma_lydohuy = "";
                if (!Utility.isValidGrid(grdRegExam)) return;

                if (objLuotkham == null)
                {
                    objLuotkham = TaoLuotkham();
                }
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Không lấy được thông tin bệnh nhân dựa vào dữ liệu trên lưới danh sách bệnh nhân. Liên hệ bộ phận IT để được trợ giúp");
                    return;
                }

                if (Utility.Int32Dbnull(objLuotkham.TrangthaiNoitru, 0) >= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
                {
                    Utility.ShowMsg("Bệnh nhân này đã ở trạng thái nội trú nên hệ thống không cho phép hủy thanh toán ngoại trú nữa");
                    return;
                }
                KcbDangkyKcb objDangky = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1));
                if (objDangky == null)
                {
                    Utility.ShowMsg("Không lấy được thông tin Đăng ký dịch vụ KCB. Liên hệ bộ phận IT để được trợ giúp");
                    return;
                }
                if (Utility.Byte2Bool(objDangky.DachidinhCls))
                {
                    Utility.ShowMsg("Dịch vụ Khám đang chọn đã được bác sĩ chỉ định dịch vụ cận lâm sàng nên bạn không thể hủy thanh toán");
                    return;
                }
                if (Utility.Byte2Bool(objDangky.DakeDonthuoc))
                {
                    Utility.ShowMsg("Dịch vụ Khám đang chọn đã được bác sĩ kê đơn thuốc nên bạn không thể hủy thanh toán");
                    return;
                }
                if (Utility.Byte2Bool(objDangky.TrangThai))
                {
                    Utility.ShowMsg("Dịch vụ Khám đang chọn đã được khám xong nên bạn không thể hủy thanh toán");
                    return;
                }


                int v_intIdThanhtoan = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells["Id_thanhtoan"].Value, -1);
                if (v_intIdThanhtoan != -1)
                {
                    List<int> lstRegID = GetIDKham();
                    if (PropertyLib._ThanhtoanProperties.Hienthihuythanhtoan)
                    {
                        frm_HuyThanhtoan frm = new frm_HuyThanhtoan("1");
                        frm.objLuotkham = objLuotkham;
                        frm.v_Payment_Id = v_intIdThanhtoan;
                        frm.Chuathanhtoan = 0;
                        frm.ShowCancel = true;
                        frm.ShowDialog();
                        if (!frm.m_blnCancel)
                        {
                            foreach (DataRow _row in m_dtDangkyPhongkham.Rows)
                            {
                                if (lstRegID.Contains(Utility.Int32Dbnull(_row[KcbDangkyKcb.Columns.IdKham], -1)))
                                {
                                    _row["ten_trangthai_thanhtoan"] = "Chưa thanh toán";
                                    _row[KcbDangkyKcb.Columns.IdThanhtoan] = -1;
                                    _row[KcbDangkyKcb.Columns.TrangthaiThanhtoan] = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (PropertyLib._ThanhtoanProperties.Hoitruockhihuythanhtoan)
                            if (!Utility.AcceptQuestion(string.Format("Bạn có muốn hủy lần thanh toán với Mã thanh toán {0}", v_intIdThanhtoan.ToString()), "Thông báo", true))
                            {
                                return;
                            }
                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_BATNHAPLYDO_HUYTHANHTOAN", "1", false) == "1")
                        {
                            frm_Chondanhmucdungchung _Nhaplydohuythanhtoan = new frm_Chondanhmucdungchung("LYDOHUYTHANHTOAN", "Hủy thanh toán tiền Bệnh nhân", "Nhập lý do hủy thanh toán trước khi thực hiện...", "Lý do hủy thanh toán",false);
                            _Nhaplydohuythanhtoan.ShowDialog();
                            if (_Nhaplydohuythanhtoan.m_blnCancel) return;
                            ma_lydohuy = _Nhaplydohuythanhtoan.ma;
                        }
                        bool HUYTHANHTOAN_HUYBIENLAI = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_HUYTHANHTOAN_HUYBIENLAI", "1", true) == "1";
                        ActionResult actionResult = new KCB_THANHTOAN().HuyThanhtoan(KcbThanhtoan.FetchByID(v_intIdThanhtoan), objLuotkham, ma_lydohuy, Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbThanhtoan.Columns.IdHdonLog], -1), HUYTHANHTOAN_HUYBIENLAI);
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                foreach (DataRow _row in m_dtDangkyPhongkham.Rows)
                                {
                                    if (lstRegID.Contains(Utility.Int32Dbnull(_row[KcbDangkyKcb.Columns.IdKham], -1)))
                                    {
                                        _row["ten_trangthai_thanhtoan"] = "Chưa thanh toán";
                                        _row[KcbDangkyKcb.Columns.IdThanhtoan] = -1;
                                        _row[KcbDangkyKcb.Columns.TrangthaiThanhtoan] = 0;
                                    }
                                }
                                break;
                            case ActionResult.ExistedRecord:
                                break;
                            case ActionResult.Error:
                                Utility.ShowMsg("Lỗi trong quá trình hủy thông tin thanh toán", "Thông báo", MessageBoxIcon.Error);
                                break;
                            case ActionResult.UNKNOW:
                                Utility.ShowMsg("Lỗi không xác định", "Thông báo", MessageBoxIcon.Error);
                                break;
                            case ActionResult.Cancel:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi hủy thanh toán", "Thông báo", MessageBoxIcon.Error);
            }
            finally
            {
                ModifyButtonCommandRegExam();
            }

        }
        private KcbLuotkham TaoLuotkham()
        {
            try
            {

                if (MEnAction == action.Insert || MEnAction == action.Add)
                {
                    objLuotkham = new KcbLuotkham();
                    //Bỏ đi do đã sinh theo cơ chế bảng danh mục mã lượt khám. Nếu ko sẽ mất mã lượt khám hiện thời.
                    // txtMaLankham.Text = THU_VIEN_CHUNG.KCB_SINH_MALANKHAM();
                    objLuotkham.IsNew = true;
                }
                else
                {
                    if (objLuotkham == null)
                        objLuotkham = KcbLuotkham.FetchByID(m_strMaluotkham);
                    objLuotkham.MarkOld();
                    objLuotkham.IsNew = false;
                }

                objLuotkham.KieuKham = txtLoaikham.myCode;
                //switch (objLuotkham.KieuKham)
                //{
                //    case "KSK":
                //        objLuotkham.NhomBenhnhan = "KSK";
                //        break;
                //    case "KTC":
                //        objLuotkham.NhomBenhnhan = "KTC";
                //        break;
                //    default: 
                //        break;

                //}
                objLuotkham.NhomBenhnhan = objLuotkham.KieuKham;
                objLuotkham.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
                objLuotkham.Noitru = 0;
                objLuotkham.IdDoituongKcb = _IdDoituongKcb;
                objLuotkham.IdLoaidoituongKcb = _IdLoaidoituongKcb;
                objLuotkham.Locked = 0;
                objLuotkham.HienthiBaocao = 1;
                objLuotkham.TrangthaiCapcuu = 0;
                objLuotkham.IdKhoatiepnhan = globalVariables.idKhoatheoMay;
                objLuotkham.NguoiTao = globalVariables.UserName;
                objLuotkham.NgayTao = globalVariables.SysDate;
                objLuotkham.Cmt = Utility.sDbnull(txtCMT.Text, "");
                objLuotkham.DiaChi = txtDiachi.Text;
                objLuotkham.CachTao = 0;
                objLuotkham.SoBenhAn = Utility.sDbnull(txtSoBA.Text, "-1");
                objLuotkham.Email = txtEmail.Text;
                objLuotkham.NoiGioithieu = txtNoigioithieu.Text;
                long week = Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.WeekOfYear, dtpBOD.Value, dtCreateDate.Value);
                long Month = Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Month, dtpBOD.Value, dtCreateDate.Value);
                long Year = Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Year, dtpBOD.Value, dtCreateDate.Value);
                int Tinhtuoitheotuan = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_TINHTUOI_THEOTUAN", "6", false));
                int Tinhtuoitheothang = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_TINHTUOI_THEOTHANG", "17", false));
                objLuotkham.Tuoi = (int)(dtpBOD.CustomFormat == "yyyy" ? Year : (Month <= Tinhtuoitheotuan ? week : (Month <= Tinhtuoitheothang ? Month : Year)));
                objLuotkham.LoaiTuoi = (byte)(dtpBOD.CustomFormat == "yyyy" ? 0 : (Month <= Tinhtuoitheotuan ? 2 : (Month <= Tinhtuoitheothang ? 1 : 0)));
                objLuotkham.NhomBenhnhan = txtLoaiBN.myCode;
                objLuotkham.IdBenhvienDen = -1;
                objLuotkham.TthaiChuyenden = (byte)(0);
                if (THU_VIEN_CHUNG.IsBaoHiem(_IdLoaidoituongKcb))
                {
                    
                    objLuotkham.MaKcbbd = "";
                    objLuotkham.NoiDongtrusoKcbbd = "";
                    objLuotkham.MaNoicapBhyt = "";
                    objLuotkham.LuongCoban = globalVariables.LUONGCOBAN;
                    objLuotkham.MatheBhyt = "";
                    objLuotkham.MaDoituongBhyt = "";
                    objLuotkham.MaQuyenloi = null;
                    objLuotkham.DungTuyen = 1;

                    objLuotkham.MadtuongSinhsong ="";
                    objLuotkham.GiayBhyt = 0;

                    objLuotkham.NgayketthucBhyt = null;
                    objLuotkham.NgaybatdauBhyt = null;
                    objLuotkham.NoicapBhyt = "";
                    objLuotkham.DiachiBhyt = "";

                }
                else
                {
                    objLuotkham.GiayBhyt = 0;
                    objLuotkham.MadtuongSinhsong = "";
                    objLuotkham.MaKcbbd = "";
                    objLuotkham.NoiDongtrusoKcbbd = "";
                    objLuotkham.MaNoicapBhyt = "";
                    objLuotkham.LuongCoban = globalVariables.LUONGCOBAN;
                    objLuotkham.MatheBhyt = "";
                    objLuotkham.MaDoituongBhyt = "";
                    objLuotkham.MaQuyenloi = -1;
                    objLuotkham.DungTuyen = 0;

                    objLuotkham.NgayketthucBhyt = null;
                    objLuotkham.NgaybatdauBhyt = null;
                    objLuotkham.NoicapBhyt = "";
                    objLuotkham.DiachiBhyt = "";

                }

                objLuotkham.SolanKham = Utility.Int16Dbnull(txtSolankham.Text, 0);
                objLuotkham.TrieuChung = Utility.ReplaceStr(txtTrieuChungBD.Text);
                //Tránh lỗi khi update người dùng nhập mã lần khám lung tung
                if (MEnAction == action.Update) txtMaLankham.Text = m_strMaluotkham;
                objLuotkham.MaLuotkham = Utility.sDbnull(txtMaLankham.Text, "");
                objLuotkham.IdBenhnhan = Utility.Int64Dbnull(txtMaBN.Text, -1);
                DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(_IdDoituongKcb);
                if (objectType != null)
                {
                    objLuotkham.MaDoituongKcb = Utility.sDbnull(objectType.MaDoituongKcb, "");
                }
                if (MEnAction == action.Update)
                {
                    objLuotkham.NgayTiepdon = dtCreateDate.Value;
                    objLuotkham.NguoiSua = globalVariables.UserName;
                    objLuotkham.NgaySua = globalVariables.SysDate;
                    objLuotkham.IpMaysua = globalVariables.gv_strIPAddress;
                    objLuotkham.TenMaysua = globalVariables.gv_strComputerName;
                }
                if (MEnAction == action.Add || MEnAction == action.Insert)
                {
                    objLuotkham.NgayTiepdon = dtCreateDate.Value;
                    objLuotkham.NguoiTiepdon = globalVariables.UserName;

                    objLuotkham.IpMaytao = globalVariables.gv_strIPAddress;
                    objLuotkham.TenMaytao = globalVariables.gv_strComputerName;
                }
                objLuotkham.PtramBhytGoc = 0;
                objLuotkham.PtramBhyt = 0;//chkTraiTuyen.Visible ?Utility.DecimaltoDbnull(txtPtramBHYT.Text, 0):(objLuotkham.DungTuyen == 0 ? 0 : Utility.DecimaltoDbnull(txtPtramBHYT.Text, 0));
                return objLuotkham;
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi tạo thông tin lượt khám", ex);
                return null;
            }

        }
        /// <summary>
        /// hàm thực hiện lần thanh toán 
        /// </summary>
        /// <returns></returns>
        private KcbThanhtoan CreatePayment()
        {
            var objPayment = new KcbThanhtoan();
            objPayment.IdThanhtoan = -1;
            objPayment.MaLuotkham = Utility.sDbnull(txtMaLankham.Text);
            objPayment.IdBenhnhan = Utility.Int32Dbnull(txtMaBN.Text, -1);
            objPayment.NgayThanhtoan = globalVariables.SysDate;
            objPayment.IdNhanvienThanhtoan = globalVariables.gv_intIDNhanvien;
            objPayment.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
            objPayment.KieuThanhtoan = 0;
            objPayment.TrangthaiIn = 0;
            objPayment.NoiTru = 0;
            objPayment.NgayIn = null;
            objPayment.NguoiIn = string.Empty;
            objPayment.NgayTonghop = null;
            objPayment.NguoiTonghop = string.Empty;
            objPayment.NgayChot = null;
            objPayment.TrangthaiChot = 0;
            objPayment.TongTien = 0;
            //2 mục này được tính lại ở Business
            objPayment.BnhanChitra = -1;
            objPayment.BhytChitra = -1;
            objPayment.TileChietkhau = 0;
            objPayment.KieuChietkhau = "%";
            objPayment.TongtienChietkhau = 0;
            objPayment.TongtienChietkhauChitiet = 0;
            objPayment.TongtienChietkhauHoadon = 0;
            objPayment.MaLydoChietkhau = "";
            objPayment.NgayTao = globalVariables.SysDate;
            objPayment.NguoiTao = globalVariables.UserName;
            objPayment.IpMaytao = globalVariables.gv_strIPAddress;
            objPayment.TenMaytao = globalVariables.gv_strComputerName;
            objPayment.MaPttt = "TM";

            return objPayment;
        }
        void Thanhtoan(bool askbeforepayment)
        {
            try
            {
                if (!Utility.isValidGrid(grdRegExam)) return;
                int IdKham = Utility.Int32Dbnull(grdRegExam.GetValue(KcbDangkyKcb.Columns.IdKham));
                SqlQuery sqlQuery = new Select().From(KcbDangkyKcb.Schema)
                    .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(IdKham)
                    .And(KcbDangkyKcb.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Bản ghi này đã thanh toán. Mời bạn xem lại", "Thông báo", MessageBoxIcon.Information);
                    return;
                }
                if (PropertyLib._KCBProperties.Hoitruockhithanhtoan)
                    if (askbeforepayment)
                        if (!Utility.AcceptQuestion("Bạn có muốn thực hiện việc thanh toán khám bệnh cho bệnh nhân không ?",
                                                   "Thông báo", true))
                            return;

                long Payment_Id = -1;
                if (objLuotkham == null)
                    objLuotkham = TaoLuotkham();
                if (Utility.Int32Dbnull(objLuotkham.TrangthaiNoitru, 0) >= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
                {
                    Utility.ShowMsg("Bệnh nhân này đã ở trạng thái nội trú nên hệ thống không cho phép thanh toán ngoại trú nữa");
                    return;
                }
                KcbThanhtoan objPayment = CreatePayment();
                List<int> lstRegID = new List<int>();
                decimal TTBN_Chitrathucsu = 0;
                string ErrMsg = "";
                ActionResult actionResult = new KCB_THANHTOAN().ThanhtoanChiphiDvuKcb(objPayment, objLuotkham,null, Taodulieuthanhtoanchitiet(ref lstRegID).ToList<KcbThanhtoanChitiet>(),null,
                                                   ref Payment_Id, -1, false,false,"", ref TTBN_Chitrathucsu, ref ErrMsg);

                switch (actionResult)
                {
                    case ActionResult.Success:
                        if (objPayment.IdThanhtoan != Payment_Id)
                        {
                            Payment_Id = Utility.Int64Dbnull(objPayment.IdThanhtoan);
                        }

                        foreach (DataRow _row in m_dtDangkyPhongkham.Rows)
                        {
                            if (lstRegID.Contains(Utility.Int32Dbnull(_row[KcbDangkyKcb.Columns.IdKham], -1)))
                            {
                                _row["ten_trangthai_thanhtoan"] = "Đã thanh toán";
                                _row[KcbDangkyKcb.Columns.IdThanhtoan] = Payment_Id;
                                _row[KcbDangkyKcb.Columns.TrangthaiThanhtoan] = 1;
                            }
                        }
                        m_dtDangkyPhongkham.AcceptChanges();
                        Payment_Id = Utility.Int64Dbnull(objPayment.IdThanhtoan);
                        if (PropertyLib._MayInProperties.TudonginhoadonSaukhiThanhtoan && TTBN_Chitrathucsu > 0)
                        {
                            int KCB_THANHTOAN_KIEUINHOADON = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_KIEUINHOADONTUDONG_SAUKHITHANHTOAN", "1", false));
                            if (KCB_THANHTOAN_KIEUINHOADON == 1 || KCB_THANHTOAN_KIEUINHOADON == 3)
                                InHoadon(Payment_Id);
                            if (KCB_THANHTOAN_KIEUINHOADON == 2 || KCB_THANHTOAN_KIEUINHOADON == 3)
                                new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, Payment_Id,-1, objLuotkham, 0);
                        }
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình thanh toán phí khám chữa bệnh", "Thông báo");
                        break;
                    case ActionResult.Cancel:
                        Utility.ShowMsg(ErrMsg);
                        break;
                }
            }
            catch
            {
            }
            finally
            {
                ModifyButtonCommandRegExam();
            }
        }
        private KcbThanhtoanChitiet[] Taodulieuthanhtoanchitiet(ref List<int> lstRegId)
        {
            int IdKham = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1);
            DataRow[] arrDr = null;
            if (PropertyLib._KCBProperties.Thanhtoancaphidichvukemtheo)
                arrDr = m_dtDangkyPhongkham.Select(KcbDangkyKcb.IdKhamColumn.ColumnName + "=" + IdKham.ToString() + " OR " + KcbDangkyKcb.IdChaColumn.ColumnName + "=" + IdKham.ToString());
            else
                arrDr = m_dtDangkyPhongkham.Select(KcbDangkyKcb.IdKhamColumn.ColumnName + "=" + IdKham.ToString());
            List<KcbThanhtoanChitiet> lstPaymentDetail = new List<KcbThanhtoanChitiet>();

            foreach (DataRow dr in arrDr)
            {

                KcbThanhtoanChitiet newItem = new KcbThanhtoanChitiet();
                newItem.IdThanhtoan = -1;
                newItem.TinhChiphi = 1;
                newItem.IdChitiet = -1;
                if (!lstRegId.Contains(IdKham)) lstRegId.Add(IdKham);
                newItem.PtramBhyt = objLuotkham.PtramBhyt.Value;
                newItem.PtramBhytGoc = objLuotkham.PtramBhytGoc.Value;
                newItem.SoLuong = 1;
                //Phần tiền BHYT chi trả,BN chi trả sẽ tính lại theo % mới nhất của bệnh nhân trong phần Business
                newItem.BnhanChitra = -1;
                newItem.BhytChitra = -1;
                newItem.DonGia = Utility.DecimaltoDbnull(dr[KcbDangkyKcb.Columns.DonGia], 0);
                newItem.PhuThu = Utility.DecimaltoDbnull(dr[KcbDangkyKcb.Columns.PhuThu], 0);
                newItem.TuTuc = Utility.ByteDbnull(dr[KcbDangkyKcb.Columns.TuTuc], 0);
                newItem.IdPhieu = Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdKham], -1);
                newItem.IdKham = Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdKham], -1);
                newItem.IdPhieuChitiet = Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdKham], -1);
                newItem.IdDichvu = Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdDichvuKcb], -1);
                newItem.IdChitietdichvu = Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdKieukham], -1);
                newItem.TenChitietdichvu = Utility.sDbnull(dr[KcbDangkyKcb.Columns.TenDichvuKcb], "Không xác định").Trim();
                newItem.TenBhyt = Utility.sDbnull(dr[KcbDangkyKcb.Columns.TenDichvuKcb], "Không xác định").Trim();
                newItem.SttIn = 0;
                newItem.IdPhongkham = Utility.Int16Dbnull(dr[KcbDangkyKcb.Columns.IdPhongkham], -1);
                newItem.IdBacsiChidinh = globalVariables.gv_intIDNhanvien;
                newItem.IdLoaithanhtoan = (byte)(Utility.Int32Dbnull(dr[KcbDangkyKcb.LaPhidichvukemtheoColumn.ColumnName], 0) == 1 ? 0 : 1);
                newItem.TenLoaithanhtoan = THU_VIEN_CHUNG.MaKieuThanhToan(newItem.IdLoaithanhtoan);
                newItem.MaDoituongKcb = _MaDoituongKcb;
                newItem.DonviTinh = "Lượt";
                newItem.KieuChietkhau = "%";
                newItem.TileChietkhau = 0;
                newItem.TienChietkhau = 0m;
                newItem.IdThanhtoanhuy = -1;
                newItem.TrangthaiHuy = 0;
                newItem.TrangthaiBhyt = 0;
                newItem.TrangthaiChuyen = 0;
                newItem.NoiTru = 0;
                newItem.NguonGoc = (byte)0;
                newItem.NgayTao = globalVariables.SysDate;
                newItem.NguoiTao = globalVariables.UserName;
                lstPaymentDetail.Add(newItem);
                //Các thông tin ptram_bhyt,bnhan_chitra...được tính tại Business
            }
            KcbThanhtoanChitiet objChitietsokham = Taodulieuthanhtoansokham();
            if (objChitietsokham != null) lstPaymentDetail.Add(objChitietsokham);
            return lstPaymentDetail.ToArray(); ;
        }

        private KcbDangkySokham TaosoKCB()
        {
            KcbDangkySokham objSokham = null;
            if (_objDoituongKcb == null) return null;
            if (chkLaysokham.Checked && txtSoKcb.myCode != "-1")
            {

                DmucChung objDmucchung = THU_VIEN_CHUNG.LaydoituongDmucChung(txtSoKcb.LOAI_DANHMUC, txtSoKcb.myCode);
                if (objDmucchung != null)
                {
                    objSokham = new KcbDangkySokham();
                    if (_objDoituongKcb != null)
                    {
                        objSokham.IdLoaidoituongkcb = _objDoituongKcb.IdLoaidoituongKcb;
                        objSokham.MaDoituongkcb = _objDoituongKcb.MaDoituongKcb;
                        objSokham.IdDoituongkcb = _objDoituongKcb.IdDoituongKcb;
                    }

                    objSokham.MaSokcb = txtSoKcb.myCode;
                    objSokham.PhuThu = 0;
                    objSokham.TrongGoi = 0;
                    objSokham.IdGoi = -1;
                    objSokham.IdNhanvien = globalVariables.gv_intIDNhanvien;
                    objSokham.DonGia = Utility.DecimaltoDbnull(objDmucchung.VietTat, 0);
                    objSokham.BhytChitra = 0;
                    objSokham.BnhanChitra = objSokham.DonGia;
                    objSokham.PtramBhyt = 0;
                    objSokham.PtramBhytGoc = 0;
                    objSokham.TrangthaiThanhtoan = 0;
                    objSokham.IdThanhtoan = -1;
                    objSokham.NgayThanhtoan = null;
                    objSokham.Noitru = 0;
                    objSokham.NguonThanhtoan = 0;
                    objSokham.TuTuc = Utility.Bool2byte(THU_VIEN_CHUNG.IsBaoHiem(objSokham.IdLoaidoituongkcb));
                    objSokham.IdKhoakcb = globalVariablesPrivate.objKhoaphong.IdKhoaphong;
                }
            }
            return objSokham;
        }


        KcbThanhtoanChitiet Taodulieuthanhtoansokham()
        {
            KcbThanhtoanChitiet newItem = null;
            //Tiền sổ KCB
            KcbDangkySokham objDangkySokham = TaosoKCB();
            KcbDangkySokham temp = new Select().From(KcbDangkySokham.Schema).Where(KcbDangkySokham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                              .And(KcbDangkySokham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                              .ExecuteSingle<KcbDangkySokham>();
            if (temp == null)
            {
                temp = objDangkySokham;
            }
            if (temp != null && Utility.Int64Dbnull(temp.IdThanhtoan, 0) <= 0)
            {
                DmucChung objDmuc = THU_VIEN_CHUNG.LaydoituongDmucChung(txtSoKcb.LOAI_DANHMUC, temp.MaSokcb);
                if (objDmuc != null)
                {
                    newItem = new KcbThanhtoanChitiet();
                    newItem.IdThanhtoan = -1;
                    newItem.TinhChiphi = 1;
                    newItem.IdChitiet = -1;
                    newItem.PtramBhyt = temp.PtramBhyt;
                    newItem.PtramBhytGoc = temp.PtramBhytGoc;
                    newItem.SoLuong = 1;
                    //Phần tiền BHYT chi trả,BN chi trả sẽ tính lại theo % mới nhất của bệnh nhân trong phần Business
                    newItem.BnhanChitra = temp.BnhanChitra;
                    newItem.BhytChitra = temp.BhytChitra;
                    newItem.DonGia = temp.DonGia;
                    newItem.PhuThu = temp.PhuThu;
                    newItem.TuTuc = temp.TuTuc;
                    newItem.IdPhieu = Utility.Int32Dbnull(temp.IdSokcb, -1);
                    newItem.IdKham = -1;
                    newItem.IdPhieuChitiet = Utility.Int32Dbnull(temp.IdSokcb, -1);
                    newItem.IdDichvu = Utility.Int32Dbnull(temp.IdSokcb, -1);
                    newItem.IdChitietdichvu = Utility.Int32Dbnull(temp.IdSokcb, -1);
                    newItem.TenChitietdichvu = objDmuc.Ten;
                    newItem.TenBhyt = objDmuc.Ten;
                    newItem.SttIn = 0;
                    newItem.IdPhongkham = -1;
                    newItem.IdBacsiChidinh = globalVariables.gv_intIDNhanvien;
                    newItem.IdLoaithanhtoan = 10;
                    newItem.TenLoaithanhtoan = THU_VIEN_CHUNG.MaKieuThanhToan(newItem.IdLoaithanhtoan);
                    newItem.MaDoituongKcb = _MaDoituongKcb;
                    newItem.DonviTinh = "Quyển";
                    newItem.KieuChietkhau = "%";
                    newItem.TileChietkhau = 0;
                    newItem.TienChietkhau = 0m;
                    newItem.IdThanhtoanhuy = -1;
                    newItem.TrangthaiHuy = 0;
                    newItem.TrangthaiBhyt = 0;
                    newItem.TrangthaiChuyen = 0;
                    newItem.NoiTru = 0;
                    newItem.NguonGoc = (byte)0;
                    newItem.NgayTao = globalVariables.SysDate;
                    newItem.NguoiTao = globalVariables.UserName;

                }
            }
            return newItem;
        }
        private KcbPhieuthu CreatePhieuThu(KcbThanhtoan objPayment, decimal TONG_TIEN)
        {
            var objPhieuThu = new KcbPhieuthu();
            objPhieuThu.IdThanhtoan = objPayment.IdThanhtoan;
            objPhieuThu.MaPhieuthu = THU_VIEN_CHUNG.GetMaPhieuThu(globalVariables.SysDate, 0);
            objPhieuThu.SoluongChungtugoc = 1;
            objPhieuThu.LoaiPhieuthu = Convert.ToByte(0);
            objPhieuThu.NgayThuchien = globalVariables.SysDate;
            objPhieuThu.SoTien = TONG_TIEN;
            objPhieuThu.NguoiNop = globalVariables.UserName;
            objPhieuThu.IdNhanvien = globalVariables.gv_intIDNhanvien;
            objPhieuThu.IdKhoaThuchien = globalVariables.idKhoatheoMay;
            objPhieuThu.TaikhoanCo = "";
            objPhieuThu.TaikhoanNo = "";
            objPhieuThu.IdBenhnhan = objPayment.IdBenhnhan;
            objPhieuThu.MaLuotkham = objPayment.MaLuotkham;
            objPhieuThu.LydoNop = "Thu phí KCB bệnh nhân";
            objPhieuThu.NguoiTao = globalVariables.UserName;
            objPhieuThu.NgayTao = globalVariables.SysDate;
            return objPhieuThu;
        }
        void InHoadon(long paymentId)
        {
            try
            {
                KcbThanhtoan objPayment = new Select().From(KcbThanhtoan.Schema).Where(KcbThanhtoan.IdThanhtoanColumn).IsEqualTo(paymentId).ExecuteSingle<KcbThanhtoan>();
                if (objPayment == null)
                {
                    Utility.ShowMsg("Không lấy được thông tin hóa đơn thanh toán", "Thông báo lỗi", MessageBoxIcon.Information);
                    return;
                }
                decimal tongTien = Utility.Int32Dbnull(objPayment.TongTien, -1);
                ActionResult actionResult = new KCB_THANHTOAN().UpdateDataPhieuThu(CreatePhieuThu(objPayment, tongTien));
                switch (actionResult)
                {
                    case ActionResult.Success:
                        //for (int i = 0; i <= 100; i++)
                        //{
                        new INPHIEU_THANHTOAN_NGOAITRU().IN_HOADON(paymentId);
                        //Thread.Sleep(1000);
                        // this.Text = i.ToString();
                        // }
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình in hóa đơn", "Thông báo lỗi", MessageBoxIcon.Information);
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi trong quá trình in hóa đơn\n" + ex.Message, "Thông báo lỗi");
            }
        }

        private void cmdInPhieuKham_Click(object sender, EventArgs e)
        {

            InPhieu(); 
        }
        void InPhieu()
        {
            try
            {
                if (!Utility.isValidGrid(grdRegExam)) return;
                if (!grdRegExam.GetDataRows().Any() || grdRegExam.CurrentRow.RowType != RowType.Record)
                    return;
                if (PropertyLib._MayInProperties.KieuInPhieuKCB == KieuIn.Innhiet)
                    InPhieuKCB();
                else
                    InphieuKham();
            }
            catch (Exception ex)
            {

                Utility.ShowMsg("Lỗi khi in phiếu khám\n" + ex.Message);
            }
        }
        int GetrealRegID()
        {
            int reg_id = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.IdKhamColumn.ColumnName].Value, -1);
            int idphongchidinh = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.IdChaColumn.ColumnName].Value, -1);
            int LaphiDVkemtheo = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.LaPhidichvukemtheoColumn.ColumnName].Value, -1);
            if (LaphiDVkemtheo == 1)
            {
                foreach (GridEXRow _row in grdRegExam.GetDataRows())
                {
                    if (Utility.Int32Dbnull(_row.Cells[KcbDangkyKcb.IdKhamColumn.ColumnName].Value, -1) == idphongchidinh)
                        return Utility.Int32Dbnull(_row.Cells[KcbDangkyKcb.IdKhamColumn.ColumnName].Value, -1);
                }
            }
            else
                return reg_id;
            return reg_id;
        }
        private void InphieuKham()
        {
            int reg_id = GetrealRegID();
            KcbDangkyKcb objRegExam = KcbDangkyKcb.FetchByID(reg_id);
            if (objRegExam != null)
            {
                new Update(KcbDangkyKcb.Schema)
                    .Set(KcbDangkyKcb.Columns.TrangthaiIn).EqualTo(1)
                    .Set(KcbDangkyKcb.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                    .Set(KcbDangkyKcb.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                    .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objRegExam.IdKham).Execute();
                IEnumerable<GridEXRow> query = from kham in grdRegExam.GetDataRows()
                                               where
                                                   kham.RowType == RowType.Record &&
                                                   Utility.Int32Dbnull(kham.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1) ==
                                                   Utility.Int32Dbnull(objRegExam.IdKham)
                                               select kham;
                if (query.Count() > 0)
                {
                    GridEXRow gridExRow = query.FirstOrDefault();
                    gridExRow.BeginEdit();
                    gridExRow.Cells[KcbDangkyKcb.Columns.TrangthaiIn].Value = 1;
                    gridExRow.EndEdit();
                    grdRegExam.UpdateData();
                }
                DataTable v_dtData = _kcbDangky.LayThongtinInphieuKCB(reg_id);
                Utility.CreateBarcodeData(ref v_dtData, Utility.sDbnull(grdRegExam.GetValue(KcbDangkyKcb.Columns.MaLuotkham)));
                v_dtData.AcceptChanges();
                if (v_dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy bản ghi nào", "Thông báo");
                    return;
                }
                if (objLuotkham == null)
                    objLuotkham = TaoLuotkham();
                if (objLuotkham != null)
                    KcbInphieu.INPHIEU_KHAM(Utility.sDbnull(objLuotkham.MaDoituongKcb), v_dtData,
                                                  "PHIẾU KHÁM BỆNH", PropertyLib._MayInProperties.CoGiayInPhieuKCB == Papersize.A5 ? "A5" : "A4");
            }
        }
        private void InPhieuKCB()
        {

            int reg_id = -1;
            string tieude = "", reportname = "";
            //VMS.HISLink.Report.Report.tiepdon_PHIEUKHAM_NHIET crpt = new VMS.HISLink.Report.Report.tiepdon_PHIEUKHAM_NHIET();
            ReportDocument crpt = Utility.GetReport("tiepdon_PHIEUKHAM_NHIET", ref tieude, ref reportname);
            if (crpt == null) return;
            try
            {
                var objPrint = new frmPrintPreview("IN PHIẾU KHÁM", crpt, true, true);
                reg_id = GetrealRegID();
                new Update(KcbDangkyKcb.Schema)
                       .Set(KcbDangkyKcb.Columns.TrangthaiIn).EqualTo(1)
                       .Set(KcbDangkyKcb.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                       .Set(KcbDangkyKcb.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                       .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(reg_id).Execute();
                IEnumerable<GridEXRow> query = from kham in grdRegExam.GetDataRows()
                                               where
                                                   kham.RowType == RowType.Record &&
                                                   Utility.Int32Dbnull(kham.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1) ==
                                                   Utility.Int32Dbnull(reg_id)
                                               select kham;
                if (query.Count() > 0)
                {
                    GridEXRow gridExRow = query.FirstOrDefault();
                    gridExRow.BeginEdit();
                    gridExRow.Cells[KcbDangkyKcb.Columns.TrangthaiIn].Value = 1;
                    gridExRow.EndEdit();
                    grdRegExam.UpdateData();
                }
                KcbDangkyKcb objRegExam = KcbDangkyKcb.FetchByID(reg_id);
                DmucKhoaphong lDepartment = DmucKhoaphong.FetchByID(objRegExam.IdPhongkham);
                Utility.SetParameterValue(crpt, "PHONGKHAM", Utility.sDbnull(lDepartment.MaKhoaphong));
                Utility.SetParameterValue(crpt, "STT", Utility.sDbnull(objRegExam.SttKham, ""));
                Utility.SetParameterValue(crpt, "BENHAN", txtMaLankham.Text);
                Utility.SetParameterValue(crpt, "TENBN", txtTEN_BN.Text);
                Utility.SetParameterValue(crpt, "GT_TUOI", cboPatientSex.Text + " - " + txtTuoi.Text + " tuổi");
                string SOTHE = "Không có thẻ";
                string HANTHE = "Không có hạn";
               // LaySoTheBHYT();
                //if (pnlBHYT.Enabled)
                //{
                //    SOTHE = SoBHYT;
                //    HANTHE = dtInsToDate.Value.ToString("dd/MM/yyyy");
                //}

                Utility.SetParameterValue(crpt, "SOTHE", SOTHE);
                Utility.SetParameterValue(crpt, "HANTHE", HANTHE);
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInPhieuKCB, PropertyLib._MayInProperties.PreviewPhieuKCB))
                    objPrint.ShowDialog();
                else
                {
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInPhieuKCB;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch
            {
            }
            finally
            {
                Utility.FreeMemory(crpt);
                GC.Collect();
            }

        }

        private void cmdXoaKham_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdRegExam)) return;
            HuyThamKham();
        }

        private void HuyThamKham()
        {
            if (grdRegExam.CurrentRow != null)
            {
                int v_RegId = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1);
                if (Utility.AcceptQuestion("Bạn có muốn thực hiện việc xóa thông tin khám đang chọn không ?",
                                           "Thông báo", true))
                {
                    ActionResult actionResult = _kcbDangky.PerformActionDeleteRegExam(v_RegId);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            DataRow[] arrDr = m_dtDangkyPhongkham.Select("id_kham=" + v_RegId + " OR  " + KcbDangkyKcb.IdChaColumn.ColumnName + "=" + v_RegId);


                            if (arrDr.GetLength(0) > 0)
                            {
                                int _count = arrDr.Length;
                                List<string> lstregid = (from p in arrDr.AsEnumerable()
                                                         select p.Field<long>(KcbDangkyKcb.IdKhamColumn.ColumnName).ToString()
                                                      ).ToList<string>();
                                for (int i = 1; i <= _count; i++)
                                {
                                    DataRow[] tempt = m_dtDangkyPhongkham.Select("id_kham=" + lstregid[i - 1]);
                                    if (tempt.Length > 0)
                                        tempt[0].Delete();
                                    m_dtDangkyPhongkham.AcceptChanges();
                                }
                            }
                            m_dtDangkyPhongkham.AcceptChanges();
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Bạn thực hiện xóa dịch vụ khám không thành công. Liên hệ đơn vị cung cấp phần mềm để được trợ giúp", "Thông báo");
                            break;
                    }
                }
            }
            ModifyButtonCommandRegExam();
        }

        private void cmdInBienlai_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdRegExam)) return;
                int Payment_Id = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdThanhtoan].Value);
                new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, Payment_Id,-1, objLuotkham,0);
            }
            catch
            { }
        }

        private void cmdInhoadon_Click(object sender, EventArgs e)
        {

            try
            {
                if (!Utility.isValidGrid(grdRegExam)) return;
                int paymentId = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdThanhtoan].Value);
                InHoadon(paymentId);
            }
            catch (Exception ex)
            {

            }
        }

        private void dtpBOD_TextChanged(object sender, EventArgs e)
        {
            _allowAgeChanged = false;
            long week = Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.WeekOfYear, dtpBOD.Value, dtCreateDate.Value);
            long month = Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Month, dtpBOD.Value, dtCreateDate.Value);
            long year = Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Year, dtpBOD.Value, dtCreateDate.Value);
            int tinhtuoitheotuan = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_TINHTUOI_THEOTUAN", "6", false));
            int tinhtuoitheothang = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_TINHTUOI_THEOTHANG", "17", false));
            int tuoi = (int)(month <= tinhtuoitheotuan ? week : (month <= tinhtuoitheothang ? month : year));
            string loaituoi = (month <= tinhtuoitheotuan ? "Tuần" : (month <= tinhtuoitheothang ? "Tháng" : ""));
            if (dtpBOD.CustomFormat == "yyyy")
            {
                tuoi = (int)year;
            }
            txtTuoi.Text = tuoi.ToString();
            UIAction.SetText(lblLoaituoi, loaituoi);
            _allowAgeChanged = true;
        }

        private void ctxBOD_Click(object sender, EventArgs e)
        {
            dtpBOD.CustomFormat = mnuBOD.Checked ? "dd/MM/yyyy HH:mm" : "yyyy";
            txtTuoi.Enabled = dtpBOD.CustomFormat == "yyyy";
            lblLoaituoi.Visible = dtpBOD.CustomFormat != "yyyy";
            dtpBOD_TextChanged(dtpBOD, e);
        }
        void txtMaLankham_LostFocus(object sender, EventArgs e)
        {
            if (Utility.DoTrim(txtMaLankham.Text).Length >= 8 && Utility.DoTrim(txtMaLankham.Text) != m_strMaluotkham)//Đã bị thay đổi do nhập tay
            {
                //Kiểm tra nếu mã đã được sử dụng thì tự động đặt về chế độ tìm kiếm Bệnh nhân
                KcbLuotkham objTemp = KcbLuotkham.FetchByID(Utility.DoTrim(txtMaLankham.Text));
                if (objTemp != null)
                {
                    txtMaLankham_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                }
                else
                {
                    int reval = 0;
                    StoredProcedure spitem = SPs.KcbKiemtraMalankhamNhaptay(globalVariables.UserName, 0, m_strMaluotkham, Utility.DoTrim(txtMaLankham.Text), reval);
                    spitem.Execute();
                    reval = Utility.Int32Dbnull(spitem.OutputValues[0], -1);
                    if (reval != 0)
                    {
                        Utility.ShowMsg(
                            string.Format(
                                "Mã lượt khám bạn vừa nhập {0} không có trong danh mục hoặc đang được sử dụng bởi người dùng khác. Hãy nhấn OK để hệ thống tự động sinh mã lần khám mới nhất",
                                Utility.DoTrim(txtMaLankham.Text)));
                        SinhMaLanKham();
                        txtMaLankham.SelectAll();
                        txtMaLankham.Focus();
                    }
                    else
                    {
                        m_strMaluotkham = Utility.DoTrim(txtMaLankham.Text);
                    }
                }
            }
        }
    }
}
