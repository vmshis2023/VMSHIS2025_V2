using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.CalendarCombo;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Janus.Windows.UI.Tab;
using Microsoft.VisualBasic;
using NLog;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;

using VNS.HIS.UI.NGOAITRU;
using VMS.QMS;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;

using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.Classes;
using VNS.UCs;
using VNS.HIS.UI.Forms.Cauhinh;

namespace VNS.HIS.UI.NGOAITRU
{
    /// <summary>
    /// 06/11/2013 3h57
    /// </summary>
    public partial class frm_KCB_Thamkham_Tiemchung : Form
    {
        KCB_THAMKHAM _KCB_THAMKHAM = new KCB_THAMKHAM();
        KCB_KEDONTHUOC _KCB_KEDONTHUOC = new KCB_KEDONTHUOC();
        KCB_CHIDINH_CANLAMSANG _KCB_CHIDINH_CANLAMSANG = new KCB_CHIDINH_CANLAMSANG();
        DMUC_CHUNG _DMUC_CHUNG = new DMUC_CHUNG();
        private readonly string FileName = string.Format("{0}/{1}", Application.StartupPath,
                                                         "SplitterDistanceThamKham.txt");

        private readonly MoneyByLetter MoneyByLetter = new MoneyByLetter();
      
        private readonly Logger log;
        private readonly AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
        private readonly AutoCompleteStringCollection namesCollectionBenhChinh = new AutoCompleteStringCollection();
        private readonly AutoCompleteStringCollection namesCollectionBenhPhu = new AutoCompleteStringCollection();
        private readonly AutoCompleteStringCollection namesCollectionKetLuan = new AutoCompleteStringCollection();
        private readonly AutoCompleteStringCollection namesCollection_m_strMaLuotkham = new AutoCompleteStringCollection();

        private readonly string strSaveandprintPath = Application.StartupPath +
                                                      @"\CAUHINH\DefaultPrinter_PhieuHoaSinh.txt";

        private bool AllowTextChanged;
        bool _buttonClick = false;
        private int Distance = 488;
        private int Exam_ID = -1;
        public KcbLuotkham objLuotkham=null;
        public KcbDanhsachBenhnhan objBenhnhan=null;
        KcbDangkyKcb objkcbdangky = null;
        private bool Selected;
        private string TEN_BENHPHU = "";
        private bool hasMorethanOne = true;
        private string _rowFilter = "1=1";
        private bool b_Hasloaded;
        private DataSet ds = new DataSet();
        private DataTable dt_ICD = new DataTable();
        private bool hasLoaded;
       
        private DataTable m_dtVTTH = new DataTable();
        private DataTable m_dtDoctorAssign;
        private DataTable m_dtKhoaNoiTru = new DataTable();
        private DataTable m_dtChitietDonthuoc = new DataTable();
        private DataTable m_dtDonthuocChitiet_View = new DataTable();
        private DataTable m_dtVTTHChitiet_View = new DataTable();
        
        private DataTable m_dtReport = new DataTable();
        private DataTable m_hdt = new DataTable();
        private DataTable m_kl;
        private string m_strDefaultLazerPrinterName = "";

        /// <summary>
        /// hàm thực hiện việc chọn bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string m_strMaLuotkham = "";

        private DataTable m_dtDanhsachbenhnhanthamkham = new DataTable();
        private frm_DanhSachKham objSoKham;
        private GridEXRow row_Selected;
        private bool trieuchung;
        List<string> lstVisibleColumns = new List<string>();
        List<string> lstResultColumns = new List<string>() { "ten_chitietdichvu", "ketqua_cls", "binhthuong_nam", "binhthuong_nu" };

        List<string> lstKQKhongchitietColumns = new List<string>() { "Ket_qua", "bt_nam", "bt_nu" };
        List<string> lstKQCochitietColumns = new List<string>() { "ten_chitietdichvu", "Ket_qua", "bt_nam", "bt_nu" };

        string Args = "ALL";
        public frm_KCB_Thamkham_Tiemchung(string sThamso)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            KeyPreview = true;
            Args = sThamso;
            log = LogManager.GetCurrentClassLogger();
           
            
           
            dtInput_Date.Value =
                dtpCreatedDate.Value=dtNgayNhapVien.Value = globalVariables.SysDate;

            dtFromDate.Value = globalVariables.SysDate;
            dtToDate.Value = globalVariables.SysDate;

            LoadLaserPrinters();
            CauHinhQMS();
            CauHinhThamKham();
            txtIdKhoaNoiTru.Visible = txtPatientDept_ID.Visible = globalVariables.IsAdmin;
            InitEvents();
        }
        void InitEvents()
        {
            FormClosing += frm_LAOKHOA_Add_TIEPDON_BN_FormClosing;
            Load+=new EventHandler(frm_KCB_Thamkham_Tiemchung_Load);
            KeyDown+=new KeyEventHandler(frm_KCB_Thamkham_Tiemchung_KeyDown);
           
            txtSoKham.LostFocus += txtSoKham_LostFocus;
            txtSoKham.Click+=new EventHandler(txtSoKham_Click);
            txtSoKham.KeyDown+=new KeyEventHandler(txtSoKham_KeyDown);

            cmdSearch.Click+=new EventHandler(cmdSearch_Click);
            radChuaKham.CheckedChanged+=new EventHandler(radChuaKham_CheckedChanged);
            radDaKham.CheckedChanged+=new EventHandler(radDaKham_CheckedChanged);
          
            txtPatient_Code.KeyDown+=new KeyEventHandler(txtPatient_Code_KeyDown);

            grdList.ColumnButtonClick+=new ColumnActionEventHandler(grdList_ColumnButtonClick);
            grdList.KeyDown += new KeyEventHandler(grdList_KeyDown);
            grdList.DoubleClick += new EventHandler(grdList_DoubleClick);
            grdList.MouseClick += new MouseEventHandler(grdList_MouseClick);

            grdPresDetail.UpdatingCell += grdPresDetail_UpdatingCell;
            cmdSave.Click+=new EventHandler(cmdSave_Click);

            cmdChuyenVien.Click += cmdChuyenVien_Click;
            cmdCauHinh.Click+=new EventHandler(cmdCauHinh_Click);
            
            cboLaserPrinters.SelectedIndexChanged+=new EventHandler(cboLaserPrinters_SelectedIndexChanged);

            cboA4Donthuoc.SelectedIndexChanged += new EventHandler(cboA4_SelectedIndexChanged);
            cboPrintPreviewDonthuoc.SelectedIndexChanged += new EventHandler(cboPrintPreview_SelectedIndexChanged);
            cmdCreateNewPres.Click+=new EventHandler(cmdCreateNewPres_Click);
            cmdUpdatePres.Click+=new EventHandler(cmdUpdatePres_Click);
            cmdDeletePres.Click+=new EventHandler(cmdDeletePres_Click);
            cmdPrintPres.Click+=new EventHandler(cmdPrintPres_Click);

            mnuDelDrug.Click+=new EventHandler(mnuDelDrug_Click);
            mnuDelVTTH.Click += mnuDelVTTH_Click;
            cmdThamkhamConfig.Click += new EventHandler(cmdThamkhamConfig_Click);

            cmdUnlock.Click += new EventHandler(cmdUnlock_Click);
            cmdChuyenPhong.Click += new EventHandler(cmdChuyenPhong_Click);

          
            txtKet_Luan._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtKet_Luan__OnShowData);
            txtChongchidinhkhac._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtChongchidinhkhac__OnShowData);
            txtHuongdieutri._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtHuongdieutri__OnShowData);
            txtKet_Luan._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtKet_Luan__OnSaveAs);
            txtChongchidinhkhac._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtChongchidinhkhac__OnSaveAs);
            txtHuongdieutri._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtHuongdieutri__OnSaveAs);
            
           

            cmdThemphieuVT.Click += cmdThemphieuVT_Click;
            cmdSuaphieuVT.Click += cmdSuaphieuVT_Click;
            cmdXoaphieuVT.Click += cmdXoaphieuVT_Click;
            cmdInphieuVT.Click += cmdInphieuVT_Click;
            cmdLuuChandoan.Click += cmdLuuChandoan_Click;
            //txtChongchidinhkhac._OnShowData += txtChongchidinhkhac__OnShowData;
            txtPhanungSautiem._OnShowData += txtPhanungSautiem__OnShowData;
            chkKPL_All.CheckedChanged += chkKPL_All_CheckedChanged;
            chkKPL_Daochon.CheckedChanged += chkKPL_Daochon_CheckedChanged;
            chkKL_All.CheckedChanged += chkKL_All_CheckedChanged;
            chkKL_Daochon.CheckedChanged += chkKL_Daochon_CheckedChanged;


            chkKPL1.CheckedChanged += _CheckedChanged;
            chkKPL2.CheckedChanged += _CheckedChanged;
            chkKPL3.CheckedChanged += _CheckedChanged;
            chkKPL4.CheckedChanged += _CheckedChanged;
            chkKPL5.CheckedChanged += _CheckedChanged;
            chkKPL6.CheckedChanged += _CheckedChanged;
            chkKPL7.CheckedChanged += _CheckedChanged;
            chkKPL8.CheckedChanged += _CheckedChanged;

            chkKL2.CheckedChanged += _CheckedChanged;
            chkKL3.CheckedChanged += _CheckedChanged;

            chkPhanungVacxin.CheckedChanged += chkPhanungVacxin_CheckedChanged;
            txtVacxin._OnEnterMe += txtVacxin__OnEnterMe;
            cmdDatiem.Click += cmdDatiem_Click;
            cmdChuatiem.Click += cmdChuatiem_Click;

            chkMuithu.CheckedChanged += chkMuithu_CheckedChanged;
            chkHennhaclai.CheckedChanged += chkHennhaclai_CheckedChanged;

            cmdInbangke.Click += cmdInbangke_Click;
            txtKQ._OnShowData += txtKQ__OnShowData;
            txtLydotiem._OnShowData += txtLydotiem__OnShowData;
            chkNgaytiem.CheckedChanged += chkNgaytiem_CheckedChanged;
            cmdKecongtiem.Click += cmdKecongtiem_Click;
            cmdInbangke_1.Click += cmdInbangke_1_Click;
            cmdLapphieuhenTC.Click += cmdLapphieuhenTC_Click;
        }

        void cmdLapphieuhenTC_Click(object sender, EventArgs e)
        {
            if (objBenhnhan != null && objLuotkham != null && objkcbdangky != null)
            {
                frm_KCB_Lapphieuhen_Tiemchung _Lapphieuhen_Tiemchung = new frm_KCB_Lapphieuhen_Tiemchung();
                _Lapphieuhen_Tiemchung.objLuotkham = objLuotkham;
                _Lapphieuhen_Tiemchung.objBN = objBenhnhan;
                _Lapphieuhen_Tiemchung.ShowDialog();
            }
        }

        void cmdInbangke_1_Click(object sender, EventArgs e)
        {
            cmdInbangke_Click(cmdInbangke, e);
        }
        DataTable m_dtCongtiem = new DataTable();
        void LayCongtiem()
        {
            m_dtCongtiem = new Select().From(KcbChidinhcl.Schema)
                .Where(KcbChidinhcl.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(KcbChidinhcl.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .And(KcbChidinhcl.Columns.KieuChidinh).IsEqualTo(4)
                .ExecuteDataSet().Tables[0];
            if (m_dtCongtiem != null && m_dtCongtiem.Rows.Count > 0)
            {
                errorProvider1.Clear();
            }
            else
                errorProvider1.SetError(cmdKecongtiem, "Chú ý: Chưa kê khai công tiêm chủng");
        }
        void cmdKecongtiem_Click(object sender, EventArgs e)
        {
            if (m_dtChitietDonthuoc != null && m_dtChitietDonthuoc.Rows.Count <= 0)
            {
                Utility.ShowMsg("Chú ý: Bạn nên kê vắc xin tiêm trước khi kê công tiêm chủng");
            }
            DataRow[] arrDr = m_dtCongtiem.Select("1=1");
            if (arrDr.Length <= 0)
                ThemCongtiem();
            else
                CapnhatCongtiem(Utility.Int64Dbnull(arrDr[0]["id_chidinh"], 0));
        }
        private void ThemCongtiem()
        {
            try
            {
                frm_KCB_CHIDINH_CLS frm = new frm_KCB_CHIDINH_CLS("CONGTIEM", 4);
                frm.txtAssign_ID.Text = "-100";
                frm.AutoAddAfterCheck = true;
                frm.Exam_ID = Utility.Int32Dbnull(txtExam_ID.Text);
                frm.objLuotkham = objLuotkham;
                frm.objBenhnhan = objBenhnhan;
                frm.objPhieudieutriNoitru = null;
                frm.m_eAction = action.Insert;
                frm.txtAssign_ID.Text = "-1";
                frm.noitru = 0;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    LayCongtiem();
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                txtPatient_Code.Focus();
                txtPatient_Code.SelectAll();
            }
        }
        private void CapnhatCongtiem(long idChidinh)
        {
            try
            {
                frm_KCB_CHIDINH_CLS frm = new frm_KCB_CHIDINH_CLS("CONGTIEM", 4);
                frm.txtAssign_ID.Text = "-100";
                frm.AutoAddAfterCheck = true;
                frm.Exam_ID = Utility.Int32Dbnull(txtExam_ID.Text); 
                frm.objLuotkham = objLuotkham;
                frm.objBenhnhan = objBenhnhan;
                frm.objPhieudieutriNoitru = null;
                frm.m_eAction = action.Update;
                frm.txtAssign_ID.Text = idChidinh.ToString();
                frm.noitru = 0;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    LayCongtiem();
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                txtPatient_Code.Focus();
                txtPatient_Code.SelectAll();
            }
        }
        void chkNgaytiem_CheckedChanged(object sender, EventArgs e)
        {
            dtpNgaysudung.Enabled = chkNgaytiem.Checked;
            if (chkNgaytiem.Checked)
                dtpNgaysudung.Focus();
        }

        void txtLydotiem__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtLydotiem.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtLydotiem.myCode;
                txtLydotiem.Init();
                txtLydotiem.SetCode(oldCode);
                txtLydotiem.Focus();
            }    
        }

        void txtKQ__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtKQ.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtKQ.myCode;
                txtKQ.Init();
                txtKQ.SetCode(oldCode);
                txtKQ.Focus();
            }    
        }

        void cmdInbangke_Click(object sender, EventArgs e)
        {
            if (_kcbChandoanKetluan == null)
            {
                Utility.ShowMsg("Bạn cần lưu thông tin khám phân loại và phải kê đơn vắc xin trước khi thực hiện in bảng kê trước tiêm chủng");
                return;
            }
            if (_kcbChandoanKetluan != null && objLuotkham != null && objkcbdangky != null)
            {
                DataTable _dtData = _KCB_THAMKHAM.KcbTiemchungInbangketruocTiemchung(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, _kcbChandoanKetluan.IdKham, -1, -1);
            //    THU_VIEN_CHUNG.CreateXML(_dtData, "thamkham_tiemchung_bangketruoctiemchung.xml");
                if (_dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu báo cáo theo điều kiện bạn chọn", "Thông báo", MessageBoxIcon.Information);
                    return;
                }
                string tieude = "", reportname = "";
                Utility.UpdateLogotoDatatable(ref _dtData);
                string reportCode = "thamkham_Inbangketruoctiemchung_nguoilon";
                Dotuoi _enDotuoi = Utility.Laydotuoi(DateTime.Now.Year - objBenhnhan.NgaySinh.Value.Year);
                if (_enDotuoi == Dotuoi.TreSosinh)
                    reportCode = "thamkham_Inbangketruoctiemchung_tresosinh";
                else if (_enDotuoi == Dotuoi.TreEm)
                    reportCode = "thamkham_Inbangketruoctiemchung_treem";
                else
                    reportCode = "thamkham_Inbangketruoctiemchung_nguoilon";
                var crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
                if (crpt == null) return;

                string StaffName = globalVariables.gv_strTenNhanvien;
                if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;
                try
                {
                    frmPrintPreview objForm = new frmPrintPreview(tieude, crpt, true, _dtData.Rows.Count <= 0 ? false : true);
                    crpt.SetDataSource(_dtData);
                    objForm.mv_sReportFileName = Path.GetFileName(reportname);
                    objForm.mv_sReportCode = reportCode;
                    Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                    Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                    Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                    Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                    Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                    Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                    Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName,objLuotkham.NgayTiepdon));
                    objForm.crptViewer.ReportSource = crpt;
                    objForm.ShowDialog();
                }
                catch (Exception exception)
                {
                    Utility.CatchException(exception);

                }
            }
        }

        void chkHennhaclai_CheckedChanged(object sender, EventArgs e)
        {
            dtpHennhaclai.Enabled = chkHennhaclai.Checked;
            if (chkHennhaclai.Checked)
                dtpHennhaclai.Focus();
        }

        void chkMuithu_CheckedChanged(object sender, EventArgs e)
        {
            txtMuithu.Enabled = chkMuithu.Checked;
            txtMuithu.ReadOnly = !chkMuithu.Checked;
            if (chkMuithu.Checked)
                txtMuithu.Focus();
        }

        void cmdChuatiem_Click(object sender, EventArgs e)
        {
            try
            {
               
                _KCB_THAMKHAM.DanhdautrangthaiTiem(objChitiet, objkcbdangky.IdKham, false);
                if (objChitiet != null)
                {
                    objChitiet.DaDung = 0;
                    DataRow[] arrDr = m_dtChitietDonthuoc.Select(KcbDonthuocChitiet.Columns.IdChitietdonthuoc + "=" + objChitiet.IdChitietdonthuoc.ToString());
                    if (arrDr.Length > 0)
                    {
                        arrDr[0][KcbDonthuocChitiet.Columns.DaDung] = 0;
                        m_dtChitietDonthuoc.AcceptChanges();
                    }

                }
                else
                {
                    foreach (DataRow dr in m_dtChitietDonthuoc.Rows)
                    {
                        dr[KcbDonthuocChitiet.Columns.DaDung] = 0;

                    }
                    m_dtChitietDonthuoc.AcceptChanges();
                }
                ModifyButtonTiemChung();
            }
            catch (Exception)
            {
                
            }
           
        }

        void cmdDatiem_Click(object sender, EventArgs e)
        {
            try
            {
                _KCB_THAMKHAM.DanhdautrangthaiTiem(objChitiet, objkcbdangky.IdKham, true);
                if (objChitiet != null)
                {
                    objChitiet.DaDung = 1;
                    DataRow[] arrDr = m_dtChitietDonthuoc.Select(KcbDonthuocChitiet.Columns.IdChitietdonthuoc + "=" + objChitiet.IdChitietdonthuoc.ToString());
                    if (arrDr.Length > 0)
                    {
                        arrDr[0][KcbDonthuocChitiet.Columns.DaDung] = 1;
                        m_dtChitietDonthuoc.AcceptChanges();
                    }

                }
                else
                {
                    foreach (DataRow dr in m_dtChitietDonthuoc.Rows)
                    {
                        dr[KcbDonthuocChitiet.Columns.DaDung] = 1;

                    }
                    m_dtChitietDonthuoc.AcceptChanges();
                }
                ModifyButtonTiemChung();
            }
            catch (Exception)
            {

            }
        }

        void Luurieng()
        {
            if (!chkPhanungVacxin.Checked)
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn vắc xin cần lưu riêng",true);
                chkPhanungVacxin.Focus();
                return;
            }
            if (chkPhanungVacxin.Checked && objChitiet==null)
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn vắc xin cần lưu riêng", true);
                txtVacxin.Focus();
                return;
            }
            if (Utility.Int32Dbnull(txtMuithu.Text,-1)<=0)
            {
                Utility.SetMsg(lblMsg, "Số mũi phải lớn hơn hoặc bằng 1", true);
                txtMuithu.Focus();
                return;
            }
            if (objChitiet != null)
            {
                objChitiet.PhanungSautiem = txtPhanungSautiem.myCode;
                objChitiet.Xutri = txtHuongdieutri.myCode;
                objChitiet.KetluanNguyennhan = txtKet_Luan.myCode;
                objChitiet.NgaySudung = dtpNgaysudung.Value;
                objChitiet.LydoTiemchung = txtLydotiem.myCode;
                objChitiet.KetQua = txtKQ.myCode;
                objChitiet.MuiThu = Utility.ByteDbnull(txtMuithu.Text, 1);
                if (chkHennhaclai.Checked)
                    objChitiet.NgayhenMuiketiep = dtpHennhaclai.Value;
                else
                    objChitiet.NgayhenMuiketiep = null;
                objChitiet.NguoiTiem = Utility.Int16Dbnull(txtNguoitiem.MyID,-1);
                _KCB_THAMKHAM.LuuHoibenhvaChandoan(null, null, objChitiet, null, true,false);
            }
        }
        KcbDonthuocChitiet objChitiet = null;
        void txtVacxin__OnEnterMe()
        {
            try
            {
                errorProvider1.Clear();
                if (chkPhanungVacxin.Checked)
                {
                    objChitiet = KcbDonthuocChitiet.FetchByID(Utility.sDbnull(txtVacxin.MyID, "-1"));
                    if (objChitiet != null)
                    {
                        txtPhanungSautiem.SetCode(objChitiet.PhanungSautiem);
                        txtHuongdieutri.SetCode(objChitiet.Xutri);
                        txtKet_Luan.SetCode( objChitiet.KetluanNguyennhan);
                        txtKQ.SetCode(objChitiet.KetQua);
                        txtMuithu.Text = Utility.sDbnull(objChitiet.MuiThu);
                        txtNguoitiem.SetId(objChitiet.NguoiTiem);
                        txtLydotiem.SetCode(objChitiet.LydoTiemchung);
                        chkMuithu.Enabled = chkPhanungVacxin.Checked && objChitiet != null;
                        chkHennhaclai.Enabled = chkNgaytiem.Enabled = chkMuithu.Enabled;
                        chkHennhaclai.Checked =chkHennhaclai.Enabled && objChitiet.NgayhenMuiketiep != null;
                        if (chkHennhaclai.Checked)
                            if (objChitiet.NgayhenMuiketiep != null)
                                dtpHennhaclai.Value = objChitiet.NgayhenMuiketiep.Value;
                            else
                                dtpHennhaclai.Value = globalVariables.SysDate;
                        dtpNgaysudung.Value = objChitiet.NgaySudung.Value;
                        cmdChuatiem.Enabled = Utility.Byte2Bool(objChitiet.DaDung);
                        cmdDatiem.Enabled = !cmdChuatiem.Enabled;
                        pnlPhanungsautiem.Enabled = cmdChuatiem.Enabled;
                        if (!pnlPhanungsautiem.Enabled) errorProvider1.SetError(chkPhanungVacxin, "Cần phát vắc xin cho Bệnh nhân và đánh dấu trạng thái Đã tiêm trước khi thực hiện nhập liệu thông tin phản ứng sau tiêm chủng");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        string code1 = "";
        string code2 = "";
        string code3 = "";
        string code4 = "";
        void chkPhanungVacxin_CheckedChanged(object sender, EventArgs e)
        {
            code1 = txtPhanungSautiem.myCode;
            code2 = txtHuongdieutri.myCode;
            code3 = txtKQ.myCode;
            code4 = txtKet_Luan.myCode;

            errorProvider1.Clear();
            txtVacxin.Enabled = chkPhanungVacxin.Checked;
            chkMuithu.Enabled = chkPhanungVacxin.Checked;
            dtpHennhaclai.Enabled = chkPhanungVacxin.Checked;
            txtNguoitiem.Enabled = chkPhanungVacxin.Checked;
            txtLydotiem.Enabled = txtNguoitiem.Enabled;
            chkNgaytiem.Enabled = chkMuithu.Enabled;
            txtVacxin.Focus();
            txtVacxin.SelectAll();
            if (!chkPhanungVacxin.Checked)
            {
                objChitiet = null;
                if (_kcbChandoanKetluan != null)
                {
                    txtPhanungSautiem.SetCode(_kcbChandoanKetluan.PhanungSautiemchung);
                    txtHuongdieutri.SetCode(_kcbChandoanKetluan.HuongDieutri);
                    txtKet_Luan.SetCode(_kcbChandoanKetluan.KetluanNguyennhan);
                    txtKQ.SetCode(_kcbChandoanKetluan.Ketluan);
                }
                ModifyButtonTiemChung();
            }
            else
            {
                objChitiet = KcbDonthuocChitiet.FetchByID(Utility.sDbnull(txtVacxin.MyID, "-1"));
                if (objChitiet != null)
                {
                    txtPhanungSautiem.SetCode(objChitiet.PhanungSautiem);
                    txtHuongdieutri.SetCode(objChitiet.Xutri);
                    txtKet_Luan.SetCode(objChitiet.KetluanNguyennhan);
                    txtKQ.SetCode(objChitiet.KetQua);
                    txtMuithu.Text = Utility.sDbnull(objChitiet.MuiThu);
                    txtNguoitiem.SetId(objChitiet.NguoiTiem);
                    txtLydotiem.SetCode(objChitiet.LydoTiemchung);
                    chkMuithu.Enabled = chkPhanungVacxin.Checked && objChitiet != null;
                    chkHennhaclai.Checked = chkHennhaclai.Enabled = chkMuithu.Enabled;
                    if (chkHennhaclai.Checked && objChitiet.NgayhenMuiketiep!=null)
                        dtpHennhaclai.Value = objChitiet.NgayhenMuiketiep.Value;
                    dtpNgaysudung.Value = objChitiet.NgaySudung.Value;
                    cmdChuatiem.Enabled = Utility.Byte2Bool(objChitiet.DaDung);
                    cmdDatiem.Enabled = !cmdChuatiem.Enabled;
                    pnlPhanungsautiem.Enabled = cmdChuatiem.Enabled;
                    if (!pnlPhanungsautiem.Enabled) errorProvider1.SetError(chkPhanungVacxin, "Cần phát vắc xin cho Bệnh nhân và đánh dấu trạng thái Đã tiêm trước khi thực hiện nhập liệu thông tin phản ứng sau tiêm chủng");
                }
                else
                {
                    txtPhanungSautiem.SetCode("-1");
                    txtHuongdieutri.SetCode("-1");
                    txtKQ.SetCode("-1");
                    txtKet_Luan.SetCode("-1");
                    chkHennhaclai.Checked = false;
                    cmdChuatiem.Enabled = cmdDatiem.Enabled = pnlPhanungsautiem.Enabled = false;
                }
            }
            
        }

        void _CheckedChanged(object sender, EventArgs e)
        {
           bool notEnough1 = chkKPL1.Checked || chkKPL2.Checked || chkKPL3.Checked || chkKPL4.Checked || chkKPL5.Checked || chkKPL6.Checked || chkKPL7.Checked || chkKPL8.Checked;
           bool notEnough2 = chkKL2.Checked || chkKL3.Checked;

           chkKL1.Enabled=  !notEnough1 && !notEnough2;
           chkKL1.Checked = notEnough1 || notEnough2 ? false : chkKL1.Checked;
            errorProvider1.Clear();
            if (grdPresDetail.GetDataRows().Length > 0 && ((UICheckBox)sender).Checked)
            {
                ((UICheckBox)sender).Checked = false;
                errorProvider1.SetError(cmdLuuChandoan, "Đã có đơn vắc xin nên không cho phép đánh dấu các điều kiện không đủ điều kiện tiêm chủng");
            }
        }

        void chkKL_Daochon_CheckedChanged(object sender, EventArgs e)
        {
            chkKL1.Checked = !chkKL1.Checked;
            chkKL2.Checked = !chkKL2.Checked;
            chkKL3.Checked = !chkKL3.Checked; 
        }

        void chkKL_All_CheckedChanged(object sender, EventArgs e)
        {
            chkKL1.Checked = chkKL2.Checked = chkKL3.Checked = chkKL_All.Checked;
        }

        void chkKPL_Daochon_CheckedChanged(object sender, EventArgs e)
        {
            chkKPL1.Checked = !chkKPL1.Checked;
            chkKPL2.Checked = !chkKPL2.Checked;
            chkKPL3.Checked = !chkKPL3.Checked;
            chkKPL4.Checked = !chkKPL4.Checked;
            chkKPL5.Checked = !chkKPL5.Checked;
            chkKPL6.Checked = !chkKPL6.Checked;
            chkKPL7.Checked = !chkKPL7.Checked;
            chkKPL8.Checked = !chkKPL8.Checked; 
        }

        void chkKPL_All_CheckedChanged(object sender, EventArgs e)
        {
            chkKPL1.Checked =
                chkKPL2.Checked =
                chkKPL3.Checked =
                chkKPL4.Checked =
                chkKPL5.Checked = chkKPL6.Checked = chkKPL7.Checked = chkKPL8.Checked = chkKPL_All.Checked;

        }

        void txtPhanungSautiem__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtPhanungSautiem.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtPhanungSautiem.myCode;
                txtPhanungSautiem.Init();
                txtPhanungSautiem.SetCode(oldCode);
                txtPhanungSautiem.Focus();
            } 
        }
        void txtChongchidinhkhac__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtChongchidinhkhac.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtChongchidinhkhac.myCode;
                txtChongchidinhkhac.Init();
                txtChongchidinhkhac.SetCode(oldCode);
                txtChongchidinhkhac.Focus();
            }
        }
        void cmdChuyenVien_Click(object sender, EventArgs e)
        {
            frm_chuyenvien _chuyenvien = new frm_chuyenvien();
            _chuyenvien.ucThongtinnguoibenh1.txtMaluotkham.Text = objLuotkham.MaLuotkham;
            _chuyenvien.txtChandoan1.Text = "KHÔNG";
            _chuyenvien.txtdauhieucls._Text = "KHÔNG";
            _chuyenvien.txtHuongdieutri._Text = "KHÔNG";
            _chuyenvien.txtketquaCls.Text = "KHÔNG";
            _chuyenvien.txtThuocsudung.Text = "KHÔNG";
            _chuyenvien.txtTrangthainguoibenh._Text = txtPhanungSautiem.Text;
            _chuyenvien.ucThongtinnguoibenh1.txtMaluotkham_KeyDown(_chuyenvien.ucThongtinnguoibenh1.txtMaluotkham, new KeyEventArgs(Keys.Enter));
            _chuyenvien.ShowDialog();
        }

        void grdAssignDetail_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        void cmdLuuChandoan_Click(object sender, EventArgs e)
        {
            if (chkPhanungVacxin.Checked)
                Luurieng();
            else
                _KCB_THAMKHAM.LuuHoibenhvaChandoan(TaoDulieuChandoanKetluan(), null, null, null, true,false);
        }

        void grdPresDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            if (!Utility.isValidGrid(grdPresDetail)) return;
            if (e.Column.Key == "stt_in")
            {
                long IdChitietdonthuoc = Utility.Int64Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, 0);
                if (IdChitietdonthuoc > -1)
                    new KCB_KEDONTHUOC().Capnhatchidanchitiet(IdChitietdonthuoc, KcbDonthuocChitiet.Columns.SttIn, e.Value.ToString());
                this.grdPresDetail.UpdateData();
            }
            if (e.Column.Key == "mota_them_chitiet" )
            {
                long IdChitietdonthuoc = Utility.Int64Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, 0);
                if (IdChitietdonthuoc > -1)
                    new KCB_KEDONTHUOC().Capnhatchidanchitiet(IdChitietdonthuoc,KcbDonthuocChitiet.Columns.MotaThem, e.Value.ToString());
                this.grdPresDetail.UpdateData();
            }
        }

        
        #region VTTH
        void mnuDelVTTH_Click(object sender, EventArgs e)
        {
            if (!IsValidDeleteSelectedVTTH()) return;
            PerformActionDeleteSelectedVTTH();
            ModifyCommmands();
        }
        void cmdXoaphieuVT_Click(object sender, EventArgs e)
        {
            if (!CheckPatientSelected()) return;
            if (!IsValidVTTH_delete()) return;
            PerformActionDeleteVTTH();
            ModifyCommmands();
        }

        void cmdInphieuVT_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdVTTH)) return;
            int Pres_ID = Utility.Int32Dbnull(grdVTTH.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            PrintPres(Pres_ID, "PHIẾU VẬT TƯ NGOÀI GÓI");
        }

        void cmdSuaphieuVT_Click(object sender, EventArgs e)
        {
            if (!CheckPatientSelected()) return;
            if (!cmdSuaphieuVT.Enabled) return;
            SuaphieuVattu();
        }

        void cmdThemphieuVT_Click(object sender, EventArgs e)
        {
            if (!CheckPatientSelected()) return;
            if (!cmdThemphieuVT.Enabled) return;
            ThemphieuVattu();
        }
        private void ThemphieuVattu()
        {
            try
            {
                // KeDonThuocTheoDoiTuong();
                frm_KCB_KeVacxin_Tiemchung frm = new frm_KCB_KeVacxin_Tiemchung("VT");
                frm.em_Action = action.Insert;
                frm.objLuotkham = this.objLuotkham;
                frm.objBenhnhan = this.objBenhnhan;
                frm._KcbChandoanKetluan = _kcbChandoanKetluan;
                frm.dt_ICD = dt_ICD;
                frm.id_kham = Utility.Int32Dbnull(txtExam_ID.Text);
                frm.objPhieudieutriNoitru = null;
                frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                frm.txtPres_ID.Text = "-1";
                frm.noitru = 0;
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    if (frm._KcbChandoanKetluan != null) _kcbChandoanKetluan = frm._KcbChandoanKetluan;
                    Laythongtinchidinhngoaitru();
                    Utility.GotoNewRowJanus(grdVTTH, KcbDonthuoc.Columns.IdDonthuoc,
                                            Utility.sDbnull(frm.txtPres_ID.Text));
                   
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
            finally
            {
                ModifyCommmands();
                txtPatient_Code.Focus();
                txtPatient_Code.SelectAll();
            }
        }
        private void SuaphieuVattu()
        {
            try
            {
                if (!CheckPatientSelected()) return;
                if (!Utility.isValidGrid(grdVTTH)) return;

                KcbLuotkham objPatientExam = this.objLuotkham;
                if (objPatientExam != null)
                {
                    int Pres_ID = Utility.Int32Dbnull(grdVTTH.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                    if (Donthuoc_DangXacnhan(Pres_ID))
                    {
                        Utility.ShowMsg(
                            "Phiếu vật tư này đang ở trạng thái đã duyệt cho Bệnh nhân nên không thể chỉnh sửa. Đề nghị quay lại hỏi bộ phận cấp phát vật tư tại phòng vật tư");
                        return;
                    }
                    var v_collect = new Select().From(KcbDonthuocChitiet.Schema.TableName)
                        .Where(KcbDonthuocChitiet.TrangthaiThanhtoanColumn.ColumnName).IsEqualTo(1)
                        .And(KcbDonthuocChitiet.IdDonthuocColumn.ColumnName).IsEqualTo(Pres_ID)
                        .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                    if (v_collect.Count > 0)
                    {
                        Utility.ShowMsg(
                            "Phiếu vật tư bạn đang chọn sửa đã được thanh toán. Muốn sửa lại Phiếu vật tư Bạn cần phải liên hệ với bộ phận Thanh toán để hủy thanh toán và Bộ phận cấp vật tư để hủy xác nhận Phiếu vật tư tại kho vật tư");
                        return;
                    }
                    KcbDonthuoc objPrescription = KcbDonthuoc.FetchByID(Pres_ID);
                    if (objPrescription != null)
                    {
                        frm_KCB_KeVacxin_Tiemchung frm = new frm_KCB_KeVacxin_Tiemchung("VT");
                        frm.em_Action = action.Update;
                        frm._KcbChandoanKetluan = _kcbChandoanKetluan;
                        frm.dt_ICD = dt_ICD;
                        frm.noitru = 0;
                        frm.objLuotkham = this.objLuotkham;
                        frm.objBenhnhan = this.objBenhnhan;
                        frm.id_kham = Utility.Int32Dbnull(txtExam_ID.Text);
                        frm.objPhieudieutriNoitru = null;
                        frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                        frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                        frm.txtPres_ID.Text = Utility.sDbnull(objPrescription.IdDonthuoc);
                        frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                        frm.ShowDialog();
                        if (!frm.m_blnCancel)
                        {
                            if (frm._KcbChandoanKetluan != null) _kcbChandoanKetluan = frm._KcbChandoanKetluan;
                            Laythongtinchidinhngoaitru();
                            Utility.GotoNewRowJanus(grdVTTH, KcbDonthuocChitiet.Columns.IdDonthuoc,
                                                    Utility.sDbnull(frm.txtPres_ID.Text));

                        }
                    }
                }

            }
            catch
            {
            }
            finally
            {
                ModifyCommmands();
            }
        }
        private void PerformActionDeleteVTTH()
        {
            string s = "";
            int Pres_ID = Utility.Int32Dbnull(grdVTTH.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            if (Donthuoc_DangXacnhan(Pres_ID))
            {
                Utility.ShowMsg(
                    "Phiếu vật tư này đang ở trạng thái đã duyệt cho Bệnh nhân nên không thể chỉnh sửa. Đề nghị quay lại hỏi bộ phận cấp phát vật tư tại phòng vật tư");
                return;
            }
            var v_collect = new Select().From(KcbDonthuocChitiet.Schema.TableName)
                .Where(KcbDonthuocChitiet.TrangthaiThanhtoanColumn.ColumnName).IsEqualTo(1)
                .And(KcbDonthuocChitiet.IdDonthuocColumn.ColumnName).IsEqualTo(Pres_ID)
                .ExecuteAsCollection<KcbDonthuocChitietCollection>();
            if (v_collect.Count > 0)
            {
                Utility.ShowMsg(
                    "Phiếu vật tư bạn đang chọn sửa đã được thanh toán. Muốn sửa lại Phiếu vật tư Bạn cần phải liên hệ với bộ phận Thanh toán để hủy thanh toán và Bộ phận cấp vật tư để hủy xác nhận Phiếu vật tư tại kho vật tư");
                return;
            }
            List<int> lstIdchitiet = new List<int>();
            foreach (GridEXRow gridExRow in grdVTTH.GetCheckedRows())
            {
                string stempt = "";
                int id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, 0m);
                decimal dongia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, 0m);
                int IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value, 0m);
                List<int> _temp = GetIdChitietVTTH(IdDonthuoc, id_thuoc, dongia, ref stempt);
                s += "," + stempt;
                lstIdchitiet.AddRange(_temp);
                gridExRow.Delete();
                grdVTTH.UpdateData();

            }
            _KCB_KEDONTHUOC.XoaChitietDonthuoc(s);
            XoaVTTHKhoiBangDulieu(lstIdchitiet);
            m_dtVTTH.AcceptChanges();
        }
        void XoaVTTHKhoiBangDulieu(List<int> lstIdChitietDonthuoc)
        {
            try
            {
                var p = (from q in m_dtVTTH.Select("1=1").AsEnumerable()
                         where lstIdChitietDonthuoc.Contains(Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]))
                         select q).ToArray<DataRow>();
                for (int i = 0; i <= p.Length - 1; i++)
                    m_dtVTTH.Rows.Remove(p[i]);
                m_dtVTTH.AcceptChanges();
            }
            catch
            {
            }
        }
        List<int> GetIdChitietVTTH(int IdDonthuoc, int id_thuoc, decimal don_gia, ref string s)
        {
            DataRow[] arrDr = m_dtVTTH.Select(KcbDonthuocChitiet.Columns.IdDonthuoc + "=" + IdDonthuoc.ToString() + " AND " + KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc.ToString()
                    + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + don_gia.ToString());
            if (arrDr.Length > 0)
            {
                var p1 = (from q in arrDr.AsEnumerable()
                          select Utility.sDbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                s = string.Join(",", p1.ToArray());
                var p = (from q in arrDr.AsEnumerable()
                         select Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                return p.ToList<int>();
            }
            return new List<int>();
        }
        private bool IsValidVTTH_delete()
        {
            bool b_Cancel = false;
            if (grdVTTH.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện việc xóa thông tin Vật tư tiêu hao ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdVTTH.Focus();
                return false;
            }
            foreach (GridEXRow gridExRow in grdVTTH.GetCheckedRows())
            {
                if (Utility.Coquyen("quyen_suadonthuoc") || Utility.sDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "") == globalVariables.UserName)
                {
                }
                else
                {
                    Utility.ShowMsg("Trong các VTTH bạn chọn xóa, có một số VTTH được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các VTTH do chính bạn kê để thực hiện xóa");
                    return false;

                }
            }
            KcbLuotkham _item = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text, 0), m_strMaLuotkham);
            if (_item == null)
            {
                Utility.ShowMsg("Bạn phải chọn Bệnh nhân hoặc bệnh nhân không tồn tại!", "Thông báo",
                                MessageBoxIcon.Warning);
                txtPatient_Code.Focus();
                return false;
            }

            if (_item != null && Utility.Int32Dbnull(_item.TrangthaiNoitru, -1) >= 1)
            {
                Utility.ShowMsg("Bệnh nhân đã được điều trị nội trú. Bạn chỉ có thể xem thông tin và không được phép làm các công việc liên quan đến ngoại trú", "Thông báo");
                cmdSave.Focus();
                return false;
            }
            KcbDonthuocChitiet objKcbDonthuocChitiet = null;
            foreach (GridEXRow gridExRow in grdVTTH.GetCheckedRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    int v_intIDDonthuoc =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                    int v_intIDThuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1);
                    objKcbDonthuocChitiet = new Select().From(KcbDonthuocChitiet.Schema)
                       .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(v_intIDDonthuoc)
                       .ExecuteSingle<KcbDonthuocChitiet>();
                    if (objKcbDonthuocChitiet != null)
                    {
                        if (Utility.Byte2Bool(objKcbDonthuocChitiet.TrangthaiThanhtoan))
                        {
                            Utility.ShowMsg("Bản ghi đã thanh toán, Bạn không thể xóa thông tin được ", "Thông báo",
                                            MessageBoxIcon.Warning);
                            b_Cancel = true;
                            break;
                        }
                        if (Utility.Byte2Bool(objKcbDonthuocChitiet.TrangThai))
                        {
                            Utility.ShowMsg("Bản ghi đã xác nhận, Bạn không thể xóa thông tin được ", "Thông báo",
                                            MessageBoxIcon.Warning);
                            b_Cancel = true;
                            break;
                        }
                    }
                }
            }
            if (b_Cancel)
            {
                grdVTTH.Focus();
                return false;
            }
            return true;
        }
        private bool IsValidDeleteSelectedVTTH()
        {
            bool b_Cancel = false;
            if (grdVTTH.RowCount <= 0 || grdVTTH.CurrentRow.RowType != RowType.Record)
            {
                Utility.ShowMsg("Bạn phải chọn một VTTH để xóa ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdVTTH.Focus();
                return false;
            }
            if (Utility.Coquyen("quyen_suadonthuoc") || Utility.sDbnull(grdVTTH.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "") == globalVariables.UserName)
            {
            }
            else
            {
                Utility.ShowMsg("VTTH đang chọn xóa được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các VTTH do chính bạn kê để thực hiện xóa");
                return false;
            }
            KcbLuotkham _item = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text, 0), m_strMaLuotkham);
            if (_item == null)
            {
                Utility.ShowMsg("Bạn phải chọn Bệnh nhân hoặc bệnh nhân không tồn tại!", "Thông báo",
                                MessageBoxIcon.Warning);
                txtPatient_Code.Focus();
                return false;
            }

            if (_item != null && Utility.Int32Dbnull(_item.TrangthaiNoitru, -1) >= 1)
            {
                Utility.ShowMsg("Bệnh nhân đã được điều trị nội trú. Bạn chỉ có thể xem thông tin và không được phép làm các công việc liên quan đến ngoại trú", "Thông báo");
                cmdSave.Focus();
                return false;
            }
            if (grdVTTH.CurrentRow.RowType == RowType.Record)
            {
                int v_intIDDonthuoc =
                    Utility.Int32Dbnull(grdVTTH.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                                        -1);
                int v_intIDThuoc =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1);
                SqlQuery sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(v_intIDDonthuoc)
                    .And(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                }
            }

            if (b_Cancel)
            {
                Utility.ShowMsg("Vật tư tiêu hao đang chọn xóa đã được thanh toán, Bạn không thể xóa thông tin được ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdVTTH.Focus();
                return false;
            }
            if (grdVTTH.CurrentRow.RowType == RowType.Record)
            {
                int v_intIDDonthuoc =
                    Utility.Int32Dbnull(grdVTTH.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                                        -1);
                int v_intIDThuoc =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1);
                SqlQuery sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(v_intIDDonthuoc)
                    .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                }
            }

            if (b_Cancel)
            {
                Utility.ShowMsg("Vật tư tiêu hao đang chọn xóa đã được được xác nhận nên bạn không thể xóa thông tin được ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdVTTH.Focus();
                return false;
            }
            return true;
        }
        private void PerformActionDeleteSelectedVTTH()
        {
            try
            {
                int Pres_ID =
                    Utility.Int32Dbnull(grdVTTH.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value,
                                        -1);
                int v_intIDDonthuoc =
                    Utility.Int32Dbnull(grdVTTH.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                                        -1);
                int v_intIDThuoc =
                    Utility.Int32Dbnull(grdVTTH.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value,
                                        -1);
                _KCB_KEDONTHUOC.XoaChitietDonthuoc(v_intIDDonthuoc);
                grdVTTH.CurrentRow.Delete();
                grdVTTH.UpdateData();
                m_dtVTTH.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message + "-->" +
                                "Bạn nên dùng chức năng xóa vắc xin bằng cách chọn vắc xin và sử dụng nút xóa vắc xin");
            }
        }
        #endregion
       

        void txtHuongdieutri__OnSaveAs()
        {
            if (Utility.DoTrim(txtHuongdieutri.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtHuongdieutri.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtHuongdieutri.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtHuongdieutri.myCode;
                txtHuongdieutri.Init();
                txtHuongdieutri.SetCode(oldCode);
                txtHuongdieutri.Focus();
            }    
        }
        void txtChongchidinhkhac__OnSaveAs()
        {
            if (Utility.DoTrim(txtChongchidinhkhac.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtChongchidinhkhac.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtChongchidinhkhac.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtChongchidinhkhac.myCode;
                txtChongchidinhkhac.Init();
                txtChongchidinhkhac.SetCode(oldCode);
                txtChongchidinhkhac.Focus();
            }
        }

        void txtKet_Luan__OnSaveAs()
        {
            if (Utility.DoTrim(txtKet_Luan.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtKet_Luan.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtKet_Luan.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtKet_Luan.myCode;
                txtKet_Luan.Init();
                txtKet_Luan.SetCode(oldCode);
                txtKet_Luan.Focus();
            }    
        }
        void txtHuongdieutri__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtHuongdieutri.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtHuongdieutri.myCode;
                txtHuongdieutri.Init();
                txtHuongdieutri.SetCode(oldCode);
                txtHuongdieutri.Focus();
            } 
        }

        void txtKet_Luan__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtKet_Luan.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtKet_Luan.myCode;
                txtKet_Luan.Init();
                txtKet_Luan.SetCode(oldCode);
                txtKet_Luan.Focus();
            } 
        }

        void cmdChuyenPhong_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                if (objkcbdangky == null)
                {
                    Utility.ShowMsg("Bạn cần chọn bệnh nhân trước khi thực hiện chuyển phòng khám", "");
                    return;
                }
                //Kiểm tra nếu BN chưa kết thúc hoặc bác sĩ chưa thăm khám mới được phép chuyển phòng
                //SqlQuery sqlQuery = new Select().From(KcbDonthuoc.Schema)
                //    .Where(KcbDonthuoc.Columns.IdKham).IsEqualTo(objkcbdangky.IdKham);
                //if (sqlQuery.GetRecordCount() > 0)
                //{
                //    Utility.ShowMsg("Bác sĩ đã kê đơn vắc xin cho lần khám này nên không thể chuyển phòng. Đề nghị hủy đơn vắc xin trước khi chuyển phòng khám", "");
                //    return;
                //}
                //sqlQuery = new Select().From(KcbChidinhcl.Schema)
                //    .Where(KcbChidinhcl.Columns.IdKham).IsEqualTo(objkcbdangky.IdKham);
                //if (sqlQuery.GetRecordCount() > 0)
                //{
                //    Utility.ShowMsg("Bác sĩ đã chỉ định cận lâm sàng cho lần khám này nên không thể chuyển phòng. Đề nghị hủy chỉ định cận lâm sàng trước khi chuyển phòng khám", "");
                //    return;
                //}
                frm_ChuyenPhongkham _ChuyenPhongkham = new frm_ChuyenPhongkham(true);
                _ChuyenPhongkham.objCongkham_Cu = objkcbdangky;
                _ChuyenPhongkham.MA_DTUONG = objkcbdangky.MaDoituongkcb;
                _ChuyenPhongkham.dongia = objkcbdangky.DonGia;
                _ChuyenPhongkham.txtPKcu.Text = txtPhongkham.Text;
                
                _ChuyenPhongkham.ShowDialog();
                if (!_ChuyenPhongkham.m_blnCancel)
                {
                    Utility.SetMsg(lblMsg, "Chuyển phòng khám thành công. Mời bạn chọn bệnh nhân khác để thăm khám", false);
                    DataRow[] arrDr = m_dtDanhsachbenhnhanthamkham.Select("id_kham=" + objkcbdangky.IdKham.ToString());
                    if (arrDr.Length > 0)
                    {
                        arrDr[0]["ten_phongkham"] = _ChuyenPhongkham.txtPKMoi.Text;
                        arrDr[0]["id_phongkham"] = _ChuyenPhongkham._DmucDichvukcb.IdPhongkham;
                        m_dtDanhsachbenhnhanthamkham.AcceptChanges();
                    }
                    string filter = "";
                    filter = "id_phongkham=" + Utility.Int32Dbnull(cboPhongKhamNgoaiTru.SelectedValue, -1).ToString();
                    if (Utility.Int32Dbnull(cboPhongKhamNgoaiTru.SelectedValue, -1) > -1)
                    {
                        m_dtDanhsachbenhnhanthamkham.DefaultView.RowFilter = "1=1";
                        m_dtDanhsachbenhnhanthamkham.DefaultView.RowFilter = filter;
                    }
                    else//BN vẫn hiển thị như vậy-->Cần load lại dữ liệu
                    {
                        GetData();
                    }
                    txtPatient_Code.Focus();
                    txtPatient_Code.SelectAll();
                }
            }
            catch
            {
            }
        }

        void UpdateDatatable()
        {
        }

        void cmdUnlock_Click(object sender, EventArgs e)
        {
            Unlock();
        }

       

       

       
    

      
        void cmdThamkhamConfig_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._ThamKhamProperties);
            frm.ShowDialog();
            CauHinhThamKham();
        }
        private void CauHinhThamKham()
        {
            if (PropertyLib._ThamKhamProperties!=null)
            {
                cboA4Donthuoc.Text = PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4 ? "A4" : "A5";
                cboPrintPreviewDonthuoc.SelectedIndex = PropertyLib._MayInProperties.PreviewInDonthuoc ? 0 : 1;

                cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                cmdPrintPres.Visible = PropertyLib._ThamKhamProperties.ChophepIndonthuoc;
                grdList.Height = PropertyLib._ThamKhamProperties.ChieucaoluoidanhsachBenhnhan <= 0 ? 0 : PropertyLib._ThamKhamProperties.ChieucaoluoidanhsachBenhnhan;
                grpSearch.Height = PropertyLib._ThamKhamProperties.AntimkiemNangcao ? 0 : 145;
                
            }   
        }
        void grdList_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
                HienthithongtinBn();
        }

      

       

        void grdList_MouseClick(object sender, MouseEventArgs e)
        {
            if (PropertyLib._ThamKhamProperties.ViewAfterClick && !_buttonClick )
                HienthithongtinBn();
            _buttonClick = false;
        }

        void cboPrintPreview_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewInDonthuoc = cboPrintPreviewDonthuoc.SelectedIndex == 0;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void cboA4_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.CoGiayInDonthuoc = cboA4Donthuoc.SelectedIndex == 0 ? Papersize.A4 : Papersize.A5;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }
       

        void grdList_DoubleClick(object sender, EventArgs e)
        {
            HienthithongtinBn();
        }
        private int SplitterDistance
        {
            get { return Distance; }
            set { Distance = value; }
        }


        private void showOnMonitor2()
        {
            try
            {
                Screen[] sc;
                sc = Screen.AllScreens;
                IEnumerable<Screen> query = from mh in Screen.AllScreens
                                            select mh;
                //get all the screen width and heights

                if (query.Count() >= 2)
                {
                    objSoKham = new frm_DanhSachKham();
                    if (!CheckOpened(objSoKham.Name))
                    {
                        //SetParameterValueCallback += objSoKham.SetParamValueCallbackFn;
                        objSoKham.FormBorderStyle = FormBorderStyle.None;
                        objSoKham.Left = sc[1].Bounds.Width;
                        objSoKham.Top = sc[1].Bounds.Height;
                        objSoKham.StartPosition = FormStartPosition.CenterScreen;
                        objSoKham.Location = sc[1].Bounds.Location;
                        var p = new Point(sc[1].Bounds.Location.X, sc[1].Bounds.Location.Y);
                        objSoKham.Location = p;
                        objSoKham.WindowState = FormWindowState.Maximized;
                        objSoKham.Show();
                        //b_HasScreenmonitor = true;
                        // txtSoKham_TextChanged(txtSoKham, new EventArgs());
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        // private bool b_HasSecondMonitor=false;

        private void ShowScreen()
        {
            try
            {
                if (PropertyLib._HISQMSProperties != null)
                {
                    if (PropertyLib._HISQMSProperties.IsQMS)
                    {
                        showOnMonitor2();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private bool CheckOpened(string name)
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

        private void frm_LAOKHOA_Add_TIEPDON_BN_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Utility.FreeLockObject(m_strMaLuotkham);
                if (objSoKham != null && !(CheckOpened(objSoKham.Name))) objSoKham.Close();
            }
            catch (Exception exception)
            {
            }
        }
        private void CauHinhQMS()
        {
            
            if (PropertyLib._HISQMSProperties.IsQMS)
            {
                ShowScreen();
            }
           
        }


        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin của thăm khám
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            SearchPatient();
        }

        /// <summary>
        /// Hàm thực hiện load lên Khoa nội trú
        /// </summary>
        private void InitData()
        {
            try
            {
                m_dtKhoaNoiTru = (from p in globalVariables.gv_dtKhoaPhong.AsEnumerable()
                    where
                        p[DmucKhoaphong.Columns.KieuKhoaphong].Equals("KHOA") &&
                        p[DmucKhoaphong.Columns.NoitruNgoaitru].Equals("NOI")
                    select p).CopyToDataTable();
              //  m_dtKhoaNoiTru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI",0);
            }
            catch (Exception)
            {
            }
        }

        private void SearchPatient()
        {
            try
            {
                ClearControl();
                m_strMaLuotkham = "";
                objkcbdangky = null;
                objBenhnhan = null;
                objLuotkham = null;
              
                if (m_dtChitietDonthuoc != null) m_dtChitietDonthuoc.Clear();
                DateTime dt_FormDate = dtFromDate.Value;
                DateTime dt_ToDate = dtToDate.Value;
                int Status = -1;
                int SoKham = -1;
                if (!string.IsNullOrEmpty(txtSoKham.Text))
                {
                    SoKham = Utility.Int32Dbnull(txtSoKham.Text, -1);
                    Status = -1;
                }
                else
                {
                    Status = radChuaKham.Checked ? 0 : 1;
                }

                m_dtDanhsachbenhnhanthamkham = _KCB_THAMKHAM.LayDSachBnhanThamkhamTiemchung(!chkByDate.Checked ? globalVariables.SysDate.AddDays(-7) : dt_FormDate, !chkByDate.Checked ? globalVariables.SysDate : dt_ToDate, txtTenBN.Text, Status,
                                                          SoKham,
                                                          Utility.Int32Dbnull(cboPhongKhamNgoaiTru.SelectedValue, -1), this.Args.Split('-')[0],
                                                          globalVariables.MA_KHOA_THIEN);
                //m_dtDanhsachbenhnhanthamkham.Select()
                if (!m_dtDanhsachbenhnhanthamkham.Columns.Contains("MAUSAC"))
                    m_dtDanhsachbenhnhanthamkham.Columns.Add("MAUSAC", typeof(int));

                Utility.SetDataSourceForDataGridEx_Basic(grdList, m_dtDanhsachbenhnhanthamkham, true, true, "", KcbDangkyKcb.Columns.SttKham); //"locked=0", "");

                cmdUnlock.Visible = false;
                ModifyCommmands();
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }    

        private void AddAutoComplete_m_strMaLuotkham()
        {
            try
            {
                DataTable dt_m_strMaLuotkham =
                    new Select(KcbDangkyKcb.MaLuotkhamColumn.ColumnName).From(KcbDangkyKcb.Schema).Distinct().
                        Where(KcbDangkyKcb.Columns.TrangthaiThanhtoan).IsEqualTo(1).
                        ExecuteDataSet().Tables[0];
                foreach (DataRow dr in dt_m_strMaLuotkham.Rows)
                {
                    namesCollection_m_strMaLuotkham.Add(dr[KcbLuotkham.Columns.MaLuotkham].ToString());
                }
                txtPatient_Code.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtPatient_Code.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtPatient_Code.AutoCompleteCustomSource = namesCollection_m_strMaLuotkham;
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy danh sách mã lần khám");
            }
        }

        private void LaydanhsachbacsiChidinh()
        {
            try
            {
               // m_dtDoctorAssign = THU_VIEN_CHUNG.LaydanhsachBacsi(-1,0);
                txtBacsi.Init(globalVariables.gv_dtDmucNhanvien, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    txtBacsi.SetId(-1);
                }
                else
                {
                    txtBacsi.SetId(globalVariables.gv_intIDNhanvien);
                }
               // DataTable Nguoitiem =  THU_VIEN_CHUNG.Laydanhsachnhanvien("NGUOITIEM");
                DataTable Nguoitiem = (from p in globalVariables.gv_dtDmucNhanvien.AsEnumerable()
                    where p[DmucNhanvien.Columns.MaLoainhanvien].Equals("NGUOITIEM")
                    select p).CopyToDataTable();
                txtNguoitiem.Init(Nguoitiem, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
                txtNguoitiem.SetCode(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEMCHUNG_MANHANVIEN_MACDINH",false));
                if (txtNguoitiem.MyCode == "-1" && globalVariables.gv_intIDNhanvien > 0)
                {
                    txtNguoitiem.SetId(globalVariables.gv_intIDNhanvien);
                }
            }
            catch (Exception exception)
            {
                // throw;
            }
           
        }

       

        /// <summary>
        /// hàm thực hiện trạng thái của nút
        /// </summary>
        private void ModifyCommmands()
        {
            try
            {
               
                cmdPrintPres.Enabled = !string.IsNullOrEmpty(m_strMaLuotkham);
                cmdSave.Enabled = !string.IsNullOrEmpty(m_strMaLuotkham);
                cmdPrintPres.Enabled =Utility.isValidGrid( grdPresDetail) && !string.IsNullOrEmpty(m_strMaLuotkham);
               
                tabDiagInfo.Enabled = !string.IsNullOrEmpty(m_strMaLuotkham);
                cmdPrintPres.Enabled =
                    cmdDeletePres.Enabled =
                    cmdUpdatePres.Enabled = Utility.isValidGrid(grdPresDetail) && !string.IsNullOrEmpty(m_strMaLuotkham);

                cmdInphieuVT.Enabled =
                   cmdXoaphieuVT.Enabled =
                   cmdSuaphieuVT.Enabled =Utility.isValidGrid( grdVTTH)  && !string.IsNullOrEmpty(m_strMaLuotkham);

                cmdInbangke.Enabled = _kcbChandoanKetluan != null && grdPresDetail.GetDataRows().Length > 0;
                chkDaThucHien.Visible = chkDaThucHien.Checked;
               
                if (objLuotkham.Locked == 1 || objLuotkham.TrangthaiNoitru>=1)
                {
                                                                  cmdCreateNewPres.Enabled =
                                                                  cmdUpdatePres.Enabled = cmdDeletePres.Enabled =
                                                                  cmdThemphieuVT.Enabled =
                                                                  cmdSuaphieuVT.Enabled = cmdXoaphieuVT.Enabled = false;
                }
                else
                {
                    cmdCreateNewPres.Enabled = true && !string.IsNullOrEmpty(m_strMaLuotkham);
                    cmdThemphieuVT.Enabled = true && !string.IsNullOrEmpty(m_strMaLuotkham);
                }
                ctxDelDrug.Enabled = cmdUpdatePres.Enabled;
            }
            catch (Exception exception)
            {
            }
        }

        void ModifyButtonTiemChung()
        {
            chkPhanungVacxin.Enabled = m_dtChitietDonthuoc != null && m_dtChitietDonthuoc.Rows.Count > 0 ;
            if (chkPhanungVacxin.Checked)
            {
                cmdDatiem.Enabled = objChitiet != null && Utility.Byte2Bool(objChitiet.TrangThai);
                cmdChuatiem.Enabled = objChitiet != null && Utility.Byte2Bool(objChitiet.DaDung);
                pnlPhanungsautiem.Enabled = objChitiet != null && Utility.Byte2Bool(objChitiet.DaDung);
            }
            else
            {
                cmdDatiem.Enabled = m_dtChitietDonthuoc != null && m_dtChitietDonthuoc.Select("trangthai_chitiet=1").Length > 0;
                cmdChuatiem.Enabled = m_dtChitietDonthuoc != null && m_dtChitietDonthuoc.Select("da_dung=1").Length > 0;
                pnlPhanungsautiem.Enabled = m_dtChitietDonthuoc != null && m_dtChitietDonthuoc.Select("da_dung=1").Length > 0;
            }
            if (!pnlPhanungsautiem.Enabled) errorProvider1.SetError(chkPhanungVacxin, "Cần phát vắc xin cho Bệnh nhân và đánh dấu trạng thái Đã tiêm trước khi thực hiện nhập liệu thông tin phản ứng sau tiêm chủng");
        }
        private void Laythongtinchidinhngoaitru()
        {
            errorProvider1.Clear();
            LayCongtiem();
            ds =
                _KCB_THAMKHAM.LaythongtinCLSVaThuoc(Utility.Int32Dbnull(txtPatient_ID.Text, -1),
                                                         Utility.sDbnull(m_strMaLuotkham, ""),
                                                         Utility.Int32Dbnull(txtExam_ID.Text));
            m_dtChitietDonthuoc = ds.Tables[1];
            m_dtVTTH = ds.Tables[2];
            ModifyButtonTiemChung();
            txtVacxin.Init(m_dtChitietDonthuoc, new List<string>() { KcbDonthuocChitiet.Columns.IdChitietdonthuoc, KcbDonthuocChitiet.Columns.IdThuoc, DmucThuoc.Columns.TenThuoc });
            m_dtDonthuocChitiet_View = m_dtChitietDonthuoc.Clone();
            foreach (DataRow dr in m_dtChitietDonthuoc.Rows)
            {
                dr["CHON"] = 0;
                DataRow[] drview
                    = m_dtDonthuocChitiet_View
                    .Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.IdThuoc], "-1")
                    + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.DonGia], "-1"));
                if (drview.Length <= 0)
                {
                    m_dtDonthuocChitiet_View.ImportRow(dr);
                }
                else
                {

                    drview[0][KcbDonthuocChitiet.Columns.SoLuong] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong], 0) + Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SoLuong], 0);
                    drview[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]);
                    drview[0]["TT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu]));
                    drview[0]["TT_BHYT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                    drview[0]["TT_BN"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                    drview[0]["TT_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                    drview[0]["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);

                    drview[0][KcbDonthuocChitiet.Columns.SttIn] = Math.Min(Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SttIn], 0), Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                    m_dtDonthuocChitiet_View.AcceptChanges();
                }
            }

            Utility.SetDataSourceForDataGridEx(grdPresDetail, m_dtDonthuocChitiet_View, false, true, "", KcbDonthuocChitiet.Columns.SttIn);

            m_dtVTTHChitiet_View = m_dtVTTH.Clone();
            foreach (DataRow dr in m_dtVTTH.Rows)
            {
                dr["CHON"] = 0;
                DataRow[] drview
                    = m_dtVTTHChitiet_View
                    .Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.IdThuoc], "-1")
                    + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.DonGia], "-1"));
                if (drview.Length <= 0)
                {
                    m_dtVTTHChitiet_View.ImportRow(dr);
                }
                else
                {

                    drview[0][KcbDonthuocChitiet.Columns.SoLuong] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong], 0) + Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SoLuong], 0);
                    drview[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]);
                    drview[0]["TT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu]));
                    drview[0]["TT_BHYT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                    drview[0]["TT_BN"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                    drview[0]["TT_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                    drview[0]["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);

                    drview[0][KcbDonthuocChitiet.Columns.SttIn] = Math.Min(Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SttIn], 0), Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                    m_dtVTTHChitiet_View.AcceptChanges();
                }
            }
            //Old-->Utility.SetDataSourceForDataGridEx
            Utility.SetDataSourceForDataGridEx(grdVTTH, m_dtVTTHChitiet_View, false, true, "", KcbDonthuocChitiet.Columns.SttIn);
        }
       
        private void Get_DanhmucChung()
        {
            txtPhanungSautiem.Init();
            txtHuongdieutri.Init();
            txtKet_Luan.Init();
            txtKQ.Init();
            txtLydotiem.Init();
            txtChongchidinhkhac.Init();
           
        }

        private void frm_KCB_Thamkham_Tiemchung_Load(object sender, EventArgs e)
        {
            try
            {
                AllowTextChanged = false;
                Get_DanhmucChung();
                bool tudongdanhdauTrangthaisudung = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEMCHUNG_TUDONGDANHDAU_TRANGTHAISUDUNG", "0", false) == "1";
                cmdDatiem.Visible = cmdChuatiem.Visible = !tudongdanhdauTrangthaisudung;
                DataTable dtNhomin = globalVariables.gv_dtNhomInCLS;
               // DataTable dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOM_INPHIEU_CLS", true);
                Load_DSach_ICD();
                LoadPhongkhamngoaitru();
                LaydanhsachbacsiChidinh();
                SearchPatient();
                AllowTextChanged = true;
                hasLoaded = true;
                InitData();
                ClearControl();
            }
            catch
            {
            }
            finally
            {
                ModifyCommmands();
                txtPatient_Code.Focus();
                txtPatient_Code.Select();
                

            }
        }

        private void LoadTrangTraiIn()
        {
            try
            {
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy trạng thái in");
            }
        }

        private void Load_DSach_ICD()
        {
            try
            {
                dt_ICD = globalVariables.gv_dtDmucBenh;//_KCB_THAMKHAM.LaydanhsachBenh();
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy danh sách ICD");
            }
        }

        private void LoadPhongkhamngoaitru()
        {

            DataBinding.BindDataCombobox(cboPhongKhamNgoaiTru,
                                                 THU_VIEN_CHUNG.DmucLaydanhsachCacphongkhamTheoBacsi(globalVariables.UserName,globalVariables.idKhoatheoMay, Utility.Bool2byte(globalVariables.IsAdmin), (byte)0),
                                                 DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                                 "---Chọn phòng khám---", false);
        }
        
        private void cboDoctorAssign_TextChanged(object sender, EventArgs e)
        {
           
        }

       


        private void ClearControl()
        {
            try
            {
                grdPresDetail.DataSource = null;
                txtReg_ID.Text = "";
                txtPatientDept_ID.Clear();
                txtIdKhoaNoiTru.Clear();
                txtKhoaNoiTru.Clear();
                foreach (Control control in pnlPatientInfor.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox)(control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is VNS.HIS.UCs.AutoCompleteTextbox)
                    {
                        ((VNS.HIS.UCs.AutoCompleteTextbox)control)._Text = "";
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox)(control)).Clear();
                    }
                }
                foreach (Control control in pnlThongtinBNKCB.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox)(control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is VNS.HIS.UCs.AutoCompleteTextbox)
                    {
                        ((VNS.HIS.UCs.AutoCompleteTextbox)control)._Text = "";
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox)(control)).Clear();
                    }
                }
                
                foreach (Control control in pnlKetluan.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox)(control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is VNS.HIS.UCs.AutoCompleteTextbox)
                    {
                        ((VNS.HIS.UCs.AutoCompleteTextbox)control)._Text = "";
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox)(control)).Clear();
                    }
                }

                foreach (Control control in pnlother.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox)(control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is VNS.HIS.UCs.AutoCompleteTextbox)
                    {
                        ((VNS.HIS.UCs.AutoCompleteTextbox)control)._Text = "";
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox)(control)).Clear();
                    }
                    
                }
               
            }
            catch (Exception)
            {
            }
        }

       

        KcbChandoanKetluan _kcbChandoanKetluan = null;
        bool isRoom = false;
        private bool isNhapVien = false;
        /// <summary>
        /// Lấy về thông tin bệnh nhâ nội trú
        /// </summary>
        private void GetData()
        {
            try
            {

                Utility.SetMsg(lblMsg, "", false);
                if(!Utility.isValidGrid(grdList))
                {
                    return;
                }
                bool khamcheoCackhoa = THU_VIEN_CHUNG.Laygiatrithamsohethong("KHAMCHEO_CACKHOA", "0", true)=="1";
                chkPhanungVacxin.Checked = false;
                txtVacxin.SetId("-1");
               // Utility.SetMsg(lblMsg, "", false);
                string patientCode = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham), "");
                m_strMaLuotkham = patientCode;
                if (!Utility.CheckLockObject(m_strMaLuotkham,"Thăm khám","TK"))
                    return;

                int idBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                objLuotkham = new Select().From(KcbLuotkham.Schema)
                    .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(patientCode)
                    .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(idBenhnhan).ExecuteSingle<KcbLuotkham>();

                objBenhnhan = KcbDanhsachBenhnhan.FetchByID(objLuotkham.IdBenhnhan);
              
                isRoom = false;
                if (objLuotkham != null)
                {
                    string tenKhoaphong=Utility.sDbnull( grdList.GetValue("ten_khoaphong"),"");
                    
                    cmdChuyenPhong.Visible = objLuotkham.TrangthaiNoitru == 0;
                    int hienThi = Utility.Int32Dbnull(grdList.GetValue("hien_thi"), 0);
                    if (hienThi == 0)
                    {
                        Utility.ShowMsg("Bệnh nhân " + objBenhnhan.TenBenhnhan + " CHƯA NỘP TIỀN KHÁM trong khi thuộc đối tượng khám chữa bệnh CẦN THANH TOÁN TRƯỚC KHI VÀO PHÒNG KHÁM.\nYêu cầu Bệnh nhân đi NỘP TIỀN KHÁM TRƯỚC");
                        objLuotkham = null;
                        objBenhnhan = null;
                        m_strMaLuotkham = "";
                        return;
                    }
                    if (!khamcheoCackhoa && globalVariables.MA_KHOA_THIEN != objLuotkham.MaKhoaThuchien)
                    {
                        Utility.ShowMsg("Bệnh nhân này được tiếp đón và chỉ định khám cho khoa " + tenKhoaphong + ". Trong khi máy bạn đang cấu hình khám chữa bệnh cho khoa " + globalVariablesPrivate.objKhoaphong.TenKhoaphong + "\nHệ thống không cho phép khám chéo giữa các khoa. Đề nghị liên hệ Bộ phận IT trong đơn vị để được trợ giúp");
                        objLuotkham = null;
                        objBenhnhan = null;
                        m_strMaLuotkham = "";
                        return;
                    }
                    ClearControl();
                    txt_idchidinhphongkham.Text = Utility.sDbnull(grdList.GetValue(KcbDangkyKcb.Columns.IdKham));

                    objkcbdangky = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txt_idchidinhphongkham.Text));
                    var objStaff = (from p in globalVariables.gv_dtDmucNhanvien.AsEnumerable()
                                    where p[DmucNhanvien.Columns.UserName].Equals(objLuotkham.NguoiKetthuc)
                                    select p).FirstOrDefault();
                   // DmucNhanvien objStaff = new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.UserNameColumn).IsEqualTo(Utility.sDbnull(objLuotkham.NguoiKetthuc, "")).ExecuteSingle<DmucNhanvien>();
                    string tenNhanvien = objLuotkham.NguoiKetthuc;
                    if (objStaff != null)
                        tenNhanvien = Utility.sDbnull(objStaff["ten_nhanvien"], "");
                    pnlDonthuoc.Enabled = true;
                    if (objkcbdangky != null)
                    {
                       
                        cmdUnlock.Visible =objLuotkham.TrangthaiNoitru==0 && objLuotkham.Locked.ToString() == "1";
                        cmdUnlock.Enabled = cmdUnlock.Visible && (Utility.Coquyen("quyen_mokhoa_tatca") || objLuotkham.NguoiKetthuc == globalVariables.UserName);
                        if (!cmdUnlock.Enabled)
                            toolTip1.SetToolTip(cmdUnlock, "Bạn không có quyền mở khóa Bệnh nhân này. Đề nghị liên hệ " + tenNhanvien + "(" + objLuotkham.NguoiKetthuc + " - Là người khóa BN này) để được họ mở khóa. Hoặc liên hệ Quản trị hệ thống");
                        else
                            toolTip1.SetToolTip(cmdUnlock, "Nhấn vào đây để mở khóa cho bệnh nhân đang chọn(Phím tắt Ctrl+U). Điều kiện là chỉ mở khóa đối với đối tượng Dịch vụ. Muốn mở khóa đối tượng BHYT thì cần liên lạc với bộ phận thanh toán hủy in phôi BHYT");
                        cmdChuyenVien.Enabled = objkcbdangky.TrangThai == 1;

                        DataTable m_dtThongTin = _KCB_THAMKHAM.LayThongtinBenhnhanKCB(objLuotkham.MaLuotkham,
                                                                          Utility.Int32Dbnull(objLuotkham.IdBenhnhan,
                                                                                              -1),
                                                                          Utility.Int32Dbnull(txt_idchidinhphongkham.Text));
                               
                        if (m_dtThongTin.Rows.Count > 0)
                        {
                            DataRow dr = m_dtThongTin.Rows[0];
                            if (dr != null)
                            {
                               
                                dtInput_Date.Value = Convert.ToDateTime(dr[KcbLuotkham.Columns.NgayTiepdon]);
                                txtExam_ID.Text = Utility.sDbnull(grdList.GetValue(KcbDangkyKcb.Columns.IdKham));
                              

                                Exam_ID = Utility.Int32Dbnull(txtExam_ID.Text, -1);
                                txtGioitinh.Text =
                                    Utility.sDbnull(grdList.GetValue(KcbDanhsachBenhnhan.Columns.GioiTinh), "");
                                txt_idchidinhphongkham.Text = Utility.sDbnull(grdList.GetValue(KcbDangkyKcb.Columns.IdKham));
                                lblSOkham.Text = Utility.sDbnull(grdList.GetValue(KcbDangkyKcb.Columns.SttKham));
                                txtPatient_Name.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                                txtPatient_ID.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.IdBenhnhan], "");
                                txtPatient_Code.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.MaLuotkham], "");
                                
                                txtDiaChi.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.DiaChi], "");
                                txtDiachiBHYT.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.DiachiBhyt], "");
                                txtObjectType_Name.Text = Utility.sDbnull(dr[DmucDoituongkcb.Columns.TenDoituongKcb], "");
                                txtSoBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.MatheBhyt], "");
                                txtBHTT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.PtramBhyt], "0");
                                //txtNgheNghiep.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.NgheNghiep], "");
                                txtHanTheBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.NgayketthucBhyt], "");
                                dtpNgayhethanBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.NgayketthucBhyt], globalVariables.SysDate.ToString("dd/MM/yyyy"));
                                txtTuoi.Text = Utility.sDbnull(Utility.Int32Dbnull(globalVariables.SysDate.Year) -
                                                               objBenhnhan.NgaySinh.Value.Year);

                                if (objkcbdangky != null)
                                {
                                    // txtSTTKhamBenh.Text = Utility.sDbnull(objkcbdangky.SoKham);
                                    txtReg_ID.Text = Utility.sDbnull(objkcbdangky.IdKham);
                                    dtpCreatedDate.Value = Convert.ToDateTime(objkcbdangky.NgayDangky);
                                    txtDepartment_ID.Text = Utility.sDbnull(objkcbdangky.IdPhongkham);
                                    var department = (from p in globalVariables.gv_dtDmucPhongban.AsEnumerable()
                                                       where p[DmucKhoaphong.Columns.IdKhoaphong].Equals(objkcbdangky.IdPhongkham)
                                                       select p).FirstOrDefault();
                                  //  DmucKhoaphong _department = new Select().From(DmucKhoaphong.Schema).Where(DmucKhoaphong.IdKhoaphongColumn).IsEqualTo(objkcbdangky.IdPhongkham).ExecuteSingle<DmucKhoaphong>();
                                    if (department != null)
                                    {
                                        txtPhongkham.Text = Utility.sDbnull(department["ten_khoaphong"], "");
                                    }
                                    txtTenDvuKham.Text = Utility.sDbnull(objkcbdangky.TenDichvuKcb);
                                    txtNguoiTiepNhan.Text = Utility.sDbnull(objkcbdangky.NguoiTao);
                                    if (Utility.Int16Dbnull(objkcbdangky.IdBacsikham, -1) != -1)
                                    {
                                        txtBacsi.SetId(Utility.sDbnull(objkcbdangky.IdBacsikham, -1));
                                    }
                                    else
                                    {
                                        txtBacsi.SetId(globalVariables.gv_intIDNhanvien);
                                    }

                                    chkDaThucHien.Checked = Utility.Int32Dbnull(objkcbdangky.TrangThai) == 1;
                                }
                                 _kcbChandoanKetluan = new Select().From(KcbChandoanKetluan.Schema)
                                         .Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(objkcbdangky.IdKham).ExecuteSingle
                                         <KcbChandoanKetluan>();
                                 if (_kcbChandoanKetluan != null)
                                 {
                                     txtKQ.SetCode(_kcbChandoanKetluan.Ketluan);
                                     txtKet_Luan.SetCode(_kcbChandoanKetluan.KetluanNguyennhan);
                                     txtHuongdieutri.SetCode(_kcbChandoanKetluan.HuongDieutri);
                                     txtPhanungSautiem.SetCode(_kcbChandoanKetluan.PhanungSautiemchung);
                                     txtChongchidinhkhac.SetCode(_kcbChandoanKetluan.ChongchidinhKhac);
                                     chkKPL1.Checked = Utility.Byte2Bool(_kcbChandoanKetluan.KPL1);
                                     chkKPL2.Checked = Utility.Byte2Bool(_kcbChandoanKetluan.KPL2);
                                     chkKPL3.Checked = Utility.Byte2Bool(_kcbChandoanKetluan.KPL3);
                                     chkKPL4.Checked = Utility.Byte2Bool(_kcbChandoanKetluan.KPL4);
                                     chkKPL5.Checked = Utility.Byte2Bool(_kcbChandoanKetluan.KPL5);
                                     chkKPL6.Checked = Utility.Byte2Bool(_kcbChandoanKetluan.KPL6);
                                     chkKPL7.Checked = Utility.Byte2Bool(_kcbChandoanKetluan.KPL7);
                                     chkKPL8.Checked = Utility.Byte2Bool(_kcbChandoanKetluan.KPL8);
                                     chkKL1.Checked = Utility.Byte2Bool(_kcbChandoanKetluan.KL1);
                                     chkKL2.Checked = Utility.Byte2Bool(_kcbChandoanKetluan.KL2);
                                     chkKL3.Checked = Utility.Byte2Bool(_kcbChandoanKetluan.KL3);
                                    
                                 }
                                 else
                                 {
                                     txtKQ.SetCode("-1");
                                     txtKet_Luan.SetCode("-1");
                                     txtHuongdieutri.SetCode("-1");
                                     txtPhanungSautiem.SetCode("-1");
                                     chkKPL1.Checked = false;
                                     chkKPL2.Checked = false;
                                     chkKPL3.Checked = false;
                                     chkKPL4.Checked = false;
                                     chkKPL5.Checked = false;
                                     chkKPL6.Checked = false;
                                     chkKPL7.Checked = false;
                                     chkKPL8.Checked = false;
                                     chkKL1.Checked = false;
                                     chkKL2.Checked = false;
                                     chkKL3.Checked = false;
                                     txtChanDoanKemTheo.Text = "";
                                     txtChongchidinhkhac.SetCode("-1");
                                 }
                                
                                Laythongtinchidinhngoaitru();
                            }
                        }
                        //cmdKETTHUC.Visible = objLuotkham.Locked == 0 && objkcbdangky.TrangThai > 0;
                        cmdKETTHUC.Enabled = true;
                    }
                    else
                    {
                        ClearControl();
                        cmdKETTHUC.Enabled = false;
                    }
                }
               
            }
            catch
            {
            }
            finally
            {
                ModifyCommmands();
            }
        }
       
        void ModifyByLockStatus(byte lockstatus)
        {
            cmdCreateNewPres.Enabled = !Utility.isTrue(lockstatus);
            cmdUpdatePres.Enabled = grdPresDetail.RowCount > 0 && !Utility.isTrue(lockstatus);
            cmdDeletePres.Enabled = grdPresDetail.RowCount > 0 && !Utility.isTrue(lockstatus);
            cmdPrintPres.Enabled = grdPresDetail.RowCount > 0 && !string.IsNullOrEmpty(m_strMaLuotkham);
            ctxDelDrug.Enabled = cmdUpdatePres.Enabled;
        }
        private string GetTenBenh(string MaBenh)
        {
            string TenBenh = "";
            DataRow[] arrMaBenh = globalVariables.gv_dtDmucBenh.Select(string.Format(DmucBenh.Columns.MaBenh+ "='{0}'", MaBenh));
            if (arrMaBenh.GetLength(0) > 0) TenBenh = Utility.sDbnull(arrMaBenh[0][DmucBenh.Columns.TenBenh], "");
            return TenBenh;
        }


        void HienthithongtinBn()
        {
            try
            {
                Utility.FreeLockObject(m_strMaLuotkham);
                if (!Utility.isValidGrid(grdList))
                {
                    return;
                    ClearControl();
                }
                AllowTextChanged = false;
                GetData();
                //ModifyByLockStatus(objLuotkham.Locked);
                //  GetData_Noitru();
                //foreach (GridEXRow row in grdList.GetDataRows())
                //{
                //    row.BeginEdit();
                //    row.Cells["MAUSAC"].Value = 0;
                //    row.EndEdit();
                //}
                //grdList.CurrentRow.Cells["MAUSAC"].Value = 1;
                //grdList.Refresh();
                //if (Utility.Int32Dbnull(grdList.CurrentRow.Cells["MAUSAC"].Value, 0) == 1)
                //{
                //    grdList.SelectedFormatStyle.ForeColor = Color.White;
                //    grdList.SelectedFormatStyle.BackColor = Color.SteelBlue;
                ////}
                chkKPL1.Focus();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                    Utility.ShowMsg(exception.ToString());
            }
            finally
            {
                setChanDoan();
                AllowTextChanged = true;
            }
        }
        private void grdList_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "cmdCHONBN")
                {
                    Utility.FreeLockObject(m_strMaLuotkham);
                    _buttonClick = true;
                    Janus.Windows.GridEX.GridEXColumn gridExColumn = grdList.RootTable.Columns[KcbDangkyKcb.Columns.SttKham];
                    grdList.Col = gridExColumn.Position;
                    HienthithongtinBn();
                }
            }
            catch (Exception exception)
            {
               
            }
            
        }

        /// <summary>
        /// hàm thực hiện viecj dóng form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        void Unlock()
        {
            try
            {
                if (objLuotkham == null)
                    return;
                //Kiểm tra nếu đã in phôi thì cần hủy in phôi
                KcbPhieuDct _item = new Select().From(KcbPhieuDct.Schema)
                    .Where(KcbPhieuDct.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .And(KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).ExecuteSingle<KcbPhieuDct>();
                if (_item != null)
                {
                    Utility.ShowMsg("Bệnh nhân này thuộc đối tượng BHYT đã được in phôi. Bạn cần liên hệ bộ phận thanh toán hủy in phôi để mở khóa bệnh nhân");
                    return;
                }
                new Update(KcbLuotkham.Schema)
                                   .Set(KcbLuotkham.Columns.Locked).EqualTo(0)
                                   .Set(KcbLuotkham.Columns.TrangthaiNgoaitru).EqualTo(0)
                                   .Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(string.Empty)
                                   .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(null)
                                   .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(
                                       objLuotkham.MaLuotkham)
                                   .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                objLuotkham.Locked = 0;
                objLuotkham.TrangthaiNgoaitru = 0;
                //ModifyByLockStatus(objLuotkham.Locked);
                cmdUnlock.Visible = objLuotkham.Locked.ToString() == "1";
                GetData();
            }
            catch
            {
            }
        }
        /// <summary>
        /// hàm thực hiện việc dùng phím tắt in phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_KCB_Thamkham_Tiemchung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                //if ((ActiveControl!=null && ActiveControl.Name == grdList.Name) || (this.tabPageChanDoan.ActiveControl != null ))
                //    return;
                //else
                    SendKeys.Send("{TAB}");
            if (e.Control && e.KeyCode == Keys.P)
            {
                cmdPrintPres_Click(cmdPrintPres, new EventArgs());
            }

            if (e.Control & e.KeyCode==Keys.F) cmdSearch.PerformClick();
            if (e.KeyCode == Keys.Escape) Close();
            if (e.Control && e.KeyCode == Keys.U)
                Unlock();
            if (e.Control && e.KeyCode == Keys.F5)
            {
                //splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            }
            if (e.KeyCode == Keys.F11 && PropertyLib._AppProperties.ShowActiveControl) Utility.ShowMsg(this.ActiveControl.Name);
            if (e.KeyCode == Keys.F6)
            {
                txtPatient_Code.SelectAll();
                txtPatient_Code.Focus();
                return;
            }
            if (e.KeyCode == Keys.F1)
            {
                tabDiagInfo.SelectedTab = tabPageChanDoan;
                chkKPL1.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                
            }
           
            if (e.KeyCode == Keys.F3)
            {
                tabDiagInfo.SelectedTab = tabPageChidinhThuoc;
                if (grdPresDetail.RowCount <= 0)
                {
                    cmdCreateNewPres.Focus();
                    cmdCreateNewPres_Click(cmdCreateNewPres, new EventArgs());
                }
                else
                {
                    cmdUpdatePres.Focus();
                    cmdUpdatePres_Click(cmdUpdatePres, new EventArgs());
                }
            }
            if (e.KeyCode == Keys.F4)
            {
                if (tabDiagInfo.SelectedTab == tabPageChidinhThuoc) cmdPrintPres.PerformClick();
            }
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();

            if (e.Control && e.KeyCode == Keys.N)
            {
                cmdCreateNewPres_Click(cmdCreateNewPres, new EventArgs());
            }

        }

        private void txtSoKham_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSoKham.Text))
            {
                chkByDate.Checked = false;
                cmdSearch.PerformClick();
            }
        }

        private void txtSoKham_Click(object sender, EventArgs e)
        {
        }

        private void cmdClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtFromDate.Value = dtToDate.Value = globalVariables.SysDate;
            txtSoKham.Clear();
            txtTenBN.Clear();
            radChuaKham.Checked = true;
        }

        private void txtSoKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmdSearch.PerformClick();
        }

        private void grdAssignDetail_SelectionChanged(object sender, EventArgs e)
        {
            ModifyCommmands();
        }
       
        private void txtPatient_Code_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                var dtPatient = new DataTable();
                if (e.KeyCode == Keys.Enter)
                {
                    Utility.FreeLockObject(m_strMaLuotkham);
                    string _patient_Code = Utility.AutoFullPatientCode(txtPatient_Code.Text);
                    ClearControl();
                    txtPatient_Code.Text = _patient_Code;
                    if (grdList.RowCount>0 && PropertyLib._ThamKhamProperties.Timtrenluoi)
                    {
                        DataRow[] arrData_tempt = null;
                        arrData_tempt = m_dtDanhsachbenhnhanthamkham.Select(KcbLuotkham.Columns.MaLuotkham + "='" + _patient_Code + "'");
                        if (arrData_tempt.Length > 0)
                        {
                            string _status=radChuaKham.Checked?"0":"1";
                            string regStatus=Utility.sDbnull(arrData_tempt[0][KcbDangkyKcb.Columns.TrangThai],"0");
                            if (_status != regStatus)
                            {
                                if (regStatus == "1") radDaKham.Checked = true;
                                else
                                    radChuaKham.Checked = true;
                            }
                            AllowTextChanged = false;
                            Utility.GotoNewRowJanus(grdList, KcbLuotkham.Columns.MaLuotkham, _patient_Code);
                            if (Utility.isValidGrid(grdList)) grdList_DoubleClick(grdList, new EventArgs());
                            return;
                        }
                    }
                    
                    dtPatient = _KCB_THAMKHAM.TimkiemBenhnhan(txtPatient_Code.Text,
                                                   Utility.Int32Dbnull(cboPhongKhamNgoaiTru.SelectedValue, -1),(byte)0, 0);
                   
                    DataRow[] arrPatients = dtPatient.Select(KcbLuotkham.Columns.MaLuotkham + "='" + _patient_Code + "'");
                    if (arrPatients.GetLength(0) <= 0)
                    {
                        if (dtPatient.Rows.Count > 1)
                        {
                            var frm = new frm_DSACH_BN_TKIEM(Args,0);
                            frm.MaLuotkham = txtPatient_Code.Text;
                            frm.dtPatient = dtPatient;
                            frm.ShowDialog();
                            if (!frm.has_Cancel)
                            {
                                txtPatient_Code.Text = frm.MaLuotkham;
                            }
                        }
                    }
                    else
                    {
                        txtPatient_Code.Text = _patient_Code;
                    }
                    DataTable dt_Patient = _KCB_THAMKHAM.TimkiemThongtinBenhnhansaukhigoMaBN
                        (txtPatient_Code.Text, Utility.Int32Dbnull(cboPhongKhamNgoaiTru.SelectedValue, -1),globalVariables.MA_KHOA_THIEN);
                    
                    grdList.DataSource = null;
                    grdList.DataSource = dt_Patient;
                    if (dt_Patient.Rows.Count > 0)
                    {
                        grdList.MoveToRowIndex(0);
                        grdList.CurrentRow.BeginEdit();
                        grdList.CurrentRow.Cells["MAUSAC"].Value = 1;
                        grdList.CurrentRow.EndEdit();
                        AllowTextChanged = false;
                        GetData();
                        txtPatient_Code.SelectAll();
                    }
                    else
                    {
                        string sPatientTemp = txtPatient_Code.Text;
                        m_strMaLuotkham = "";
                        objLuotkham = null;
                        objkcbdangky = null;
                        objBenhnhan = null;
                        ClearControl();
                        
                        txtPatient_Code.Text = sPatientTemp;
                        txtPatient_Code.SelectAll();
                        //Utility.SetMsg(lblMsg, "Không tìm thấy bệnh nhân có mã lần khám đang chọn",true);
                    }
                    chkKPL1.Focus();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin bệnh nhân");
            }
            finally
            {
                ModifyCommmands();
                AllowTextChanged = true;
            }
        }


        private void cmdKETTHUC_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.AcceptQuestion("Bạn có chắn chắn muốn kết thúc lần khám của bệnh nhân không", "Xác nhận",
                                           true))
                {
                    int record = -1;
                    record = new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.Locked).EqualTo(1)
                        .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtPatient_Code.Text).Execute();
                    if (record > 0)
                    {
                        DataRow[] arrDr = m_dtDanhsachbenhnhanthamkham.Select("id_kham=" + txtReg_ID.Text);
                        if (arrDr.Length > 0)
                            arrDr[0]["Locked"] = 1;
                        Utility.ShowMsg("Đã cập nhật thông tin thành công");
                        tabDiagInfo.Enabled = false;
                    }
                    else
                    {
                        Utility.ShowMsg("Chưa lưu được thông tin vào cơ sở dữ liệu");
                    }

                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lưu thông tin");
            }
            finally
            {
                ModifyCommmands();
            }
        }



        private void radChuaKham_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int status = radChuaKham.Checked ? 0 : 1;
                m_dtDanhsachbenhnhanthamkham.DefaultView.RowFilter = "1=1";
                m_dtDanhsachbenhnhanthamkham.DefaultView.RowFilter = "trang_thai=" + status;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

       
        private void radDaKham_CheckedChanged(object sender, EventArgs e)
        {
            cmdSearch_Click(cmdSearch, e);
        }

        private void mnuDelDrug_Click(object sender, EventArgs e)
        {
            if (!IsValidDeleteSelectedDrug()) return;
            PerformActionDeleteSelectedDrug();
            ModifyCommmands();
        }

       

        private void cboLaserPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveDefaultPrinter();
        }

        private void LoadLaserPrinters()
        {
            if (string.IsNullOrEmpty(PropertyLib._MayInProperties.TenMayInBienlai))
            {
                PropertyLib._MayInProperties.TenMayInBienlai = Utility.GetDefaultPrinter();
                m_strDefaultLazerPrinterName = Utility.sDbnull(PropertyLib._MayInProperties.TenMayInBienlai);
            }
            if (PropertyLib._ThamKhamProperties != null)
            {
                try
                {
                    //khoi tao may in
                    String pkInstalledPrinters;
                    cboLaserPrinters.Items.Clear();
                    for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                    {
                        pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                        cboLaserPrinters.Items.Add(pkInstalledPrinters);
                    }
                }
                catch (Exception ex)
                {

                    Utility.CatchException(ex);
                }
                finally
                {
                    m_strDefaultLazerPrinterName = Utility.sDbnull(PropertyLib._MayInProperties.TenMayInBienlai);

                    cboLaserPrinters.Text = m_strDefaultLazerPrinterName;
                }
            }
        }

        private void SaveDefaultPrinter()
        {
            try
            {
                PropertyLib._MayInProperties.TenMayInBienlai = Utility.sDbnull(cboLaserPrinters.Text);
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lưu trạng thái-->" + ex.Message);
            }
        }

       

        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties( PropertyLib._HISQMSProperties);
            frm.ShowDialog();
            CauHinhQMS();
        }

        private void txtNhietDo_Click(object sender, EventArgs e)
        {
        }

        private void txtKhoaNoiTru_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtKhoaNoiTru.Text))
                {
                    DataRow query = (from khoa in m_dtKhoaNoiTru.AsEnumerable().Cast<DataRow>()
                                     let y = Utility.sDbnull(khoa[DmucKhoaphong.Columns.TenKhoaphong])
                                     let z = Utility.sDbnull(khoa[DmucKhoaphong.Columns.IdKhoaphong])
                                     where
                                         Utility.Int32Dbnull(khoa[DmucKhoaphong.Columns.MaKhoaphong]) ==
                                         Utility.Int32Dbnull(txtKhoaNoiTru.Text)
                                     select khoa).FirstOrDefault();
                    if (query != null)
                    {
                        txtKhoaNoiTru.Text = Utility.sDbnull(query[DmucKhoaphong.Columns.TenKhoaphong]);
                        txtIdKhoaNoiTru.Text = Utility.sDbnull(query[DmucKhoaphong.Columns.IdKhoaphong]);
                        cmdSave.Focus();
                    }
                    else
                    {
                        TimKiemKhoaNoiTru();
                    }
                }
                else
                {
                    TimKiemKhoaNoiTru();
                }
            }
            if (e.KeyCode == Keys.F3)
            {
                TimKiemKhoaNoiTru();
            }
        }

        private void TimKiemKhoaNoiTru()
        {
            
        }

        private void txtDeparmentID_TextChanged(object sender, EventArgs e)
        {
            EnumerableRowCollection<string> query = from khoa in m_dtKhoaNoiTru.AsEnumerable()
                                                    let y = Utility.sDbnull(khoa[DmucKhoaphong.Columns.TenKhoaphong])
                                                    where
                                                        Utility.Int32Dbnull(khoa[DmucKhoaphong.Columns.IdKhoaphong]) ==
                                                        Utility.Int32Dbnull(txtIdKhoaNoiTru.Text)
                                                    select y;
            if (query.Any())
            {
                txtKhoaNoiTru.Text = Utility.sDbnull(query.FirstOrDefault());
            }
            else
            {
                txtKhoaNoiTru.Text = string.Empty;
            }
        }

       

        #region "khởi tạo các sụ kienj thông tin của vắc xin"

        private bool ExistsDonThuoc()
        {
            try
            {
                string _kenhieudon = THU_VIEN_CHUNG.Laygiatrithamsohethong("KE_NHIEU_DON", "N", true);
                KcbDonthuocCollection lstPres =
                    new Select()
                        .From(KcbDonthuoc.Schema)
                        .Where(KcbDonthuoc.MaLuotkhamColumn).IsEqualTo(Utility.sDbnull(objLuotkham.MaLuotkham)).
                        ExecuteAsCollection<KcbDonthuocCollection>();

                var lstPres1 = from p in lstPres
                               where p.IdKham == Exam_ID
                               select p;
                if (objLuotkham.MaDoituongKcb == "BHYT")
                {
                    if (_kenhieudon == "Y" && lstPres1.Count() <= 0)//Được phép kê mỗi phòng khám 1 đơn vắc xin
                        return false;
                    if (_kenhieudon == "N" && lstPres.Count > 0 && lstPres1.Count() <= 0)//Cảnh báo ko được phép kê đơn tiếp
                    {
                        Utility.ShowMsg("Chú ý: Bệnh nhân này thuộc đối tượng BHYT và đã được kê đơn vắc xin tại phòng khám khác. Bạn cần trao đổi với bộ phận khác để được cấu hình kê đơn vắc xin tại nhiều phòng khác khác nhau với đối tượng BHYT này", "Thông báo");
                        return false;
                    }
                }
                else//Bệnh nhân dịch vụ-->cho phép kê 1 đơn nếu đơn chưa thanh toán và nhiều đơn nếu các đơn trước đã thanh toán
                {
                    if (lstPres1.Count() > 0)
                        if (lstPres1.FirstOrDefault().TrangthaiThanhtoan == 0)//Chưa thanh toán-->Cần sửa đơn
                            return true;
                        else//Đã thanh toán-->Cho phép thêm đơn mới
                            return false;
                    return false;
                }
                return lstPres.Count > 0;
                //Tạm thời rem lại do vẫn có BN kê được >1 đơn vắc xin
                //var query = from thuoc in grdPresDetail.GetDataRows().AsEnumerable()
                //                    select thuoc;
                //if (query.Any()) return true;
                //else return false;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi kiểm tra số lượng đơn vắc xin của lần khám\n" + ex.Message);
                return false;
            }
        }
        bool CheckPatientSelected()
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn phải chọn Bệnh nhân trước khi thực hiện các công việc chỉ định Thăm khám, CLS, Kê đơn");
                return false;
            }
            return true;
        }
        bool KiemtraDudieukienTiemchung()
        {
            bool notEnough1 = true;
            bool notEnough2 = true;
            bool Enough = true;
            notEnough1 = chkKPL1.Checked || chkKPL2.Checked || chkKPL3.Checked || chkKPL4.Checked || chkKPL5.Checked || chkKPL6.Checked || chkKPL7.Checked || chkKPL8.Checked;
            notEnough2 = chkKL2.Checked || chkKL3.Checked;
            Enough = chkKL1.Checked;

            bool tempt = Enough && !notEnough1 && !notEnough2;
            if (_kcbChandoanKetluan == null)
            {
                cmdLuuChandoan_Click(cmdLuuChandoan, new EventArgs());
                return Enough && !notEnough1 && !notEnough2;
            }
            else
            {
                //notEnough1 = Utility.Byte2Bool(_KcbChandoanKetluan.KPL1) || Utility.Byte2Bool(_KcbChandoanKetluan.KPL2) || Utility.Byte2Bool(_KcbChandoanKetluan.KPL3)
                //    || Utility.Byte2Bool(_KcbChandoanKetluan.KPL4) || Utility.Byte2Bool(_KcbChandoanKetluan.KPL5)
                //    || Utility.Byte2Bool(_KcbChandoanKetluan.KPL6) || Utility.Byte2Bool(_KcbChandoanKetluan.KPL7) || Utility.Byte2Bool(_KcbChandoanKetluan.KPL8);
                //notEnough2 = Utility.Byte2Bool(_KcbChandoanKetluan.KL2) || Utility.Byte2Bool(_KcbChandoanKetluan.KL3);
                //Enough = Utility.Byte2Bool(_KcbChandoanKetluan.KL1);
                cmdLuuChandoan_Click(cmdLuuChandoan, new EventArgs());
                return Enough && !notEnough1 && !notEnough2;
            }
        }
        private void cmdCreateNewPres_Click(object sender, EventArgs e)
        {
            if (!KiemtraDudieukienTiemchung())
            {
                Utility.ShowMsg("Bạn đang đánh dấu Bệnh nhân có dấu hiệu chưa đủ điều kiện để tiêm chủng nên hệ thống không cho phép bạn kê Vắc xin tiêm cho Bệnh nhân\nMời bạn kiểm tra lại");
                tabDiagInfo.SelectedTab = tabPageChanDoan;
                return;
            }
            if (!CheckPatientSelected()) return;
            if (!cmdCreateNewPres.Enabled) return;
            if (!ExistsDonThuoc())
            {
                ThemMoiDonThuoc();
            }
            else
            {
                UpdateDonThuoc();
            }
        }

        private void ThemMoiDonThuoc()
        {
            try
            {
                // KeDonThuocTheoDoiTuong();
                frm_KCB_KeVacxin_Tiemchung frm = new frm_KCB_KeVacxin_Tiemchung("THUOC");
                frm.em_Action = action.Insert;
                frm.objLuotkham = this.objLuotkham;
                frm.objBenhnhan = this.objBenhnhan;
                frm._KcbChandoanKetluan = _kcbChandoanKetluan;
                frm.dt_ICD = dt_ICD;
                frm.id_kham = Utility.Int32Dbnull(txtExam_ID.Text);
                frm.objRegExam = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txtReg_ID.Text));
                frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                frm.txtPres_ID.Text = "-1";
                frm.noitru = 0;
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    if (frm._KcbChandoanKetluan != null) _kcbChandoanKetluan = frm._KcbChandoanKetluan;
                    Laythongtinchidinhngoaitru();
                    Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuoc.Columns.IdDonthuoc,
                                            Utility.sDbnull(frm.txtPres_ID.Text));
                    Utility.Log(this.Name, globalVariables.UserName,
                                            string.Format(
                                                "Thêm mới vắc xin của bệnh nhân có mã lượt khám là: {0}, và mã bệnh nhân là {1}",
                                                objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan), newaction.Insert, this.GetType().Assembly.ManifestModule.Name);
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
            finally
            {
                ModifyCommmands();
                txtPatient_Code.Focus();
                txtPatient_Code.SelectAll();
            }
        }

        private void setChanDoan()
        {
            return;
           
        }

       

        /// <summary>
        /// ham thực hiện việc update thông tin của vắc xin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdUpdatePres_Click(object sender, EventArgs e)
        {
            if (!cmdUpdatePres.Enabled) return;
            UpdateDonThuoc();
        }

        private bool Donthuoc_DangXacnhan(int pres_id)
        {
            var _item =
                new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.IdDonthuocColumn).IsEqualTo(pres_id).And(
                    KcbDonthuoc.TrangThaiColumn).IsEqualTo(1).ExecuteSingle<KcbDonthuoc>();
            if (_item != null) return true;
            return false;
        }

        private void UpdateDonThuoc()
        {
            try
            {
                if (grdPresDetail.CurrentRow != null && grdPresDetail.CurrentRow.RowType == RowType.Record)
                {
                    if (this.objLuotkham != null)
                    {
                        int Pres_ID = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                        if (Donthuoc_DangXacnhan(Pres_ID))
                        {
                            Utility.ShowMsg(
                                "Đơn vắc xin này đang ở trạng thái đã duyệt cho Bệnh nhân nên không thể chỉnh sửa. Đề nghị quay lại hỏi bộ phận cấp phát vắc xin để hủy vắc xin trước khi cập nhật");
                            return;
                        }
                        var vCollect = new Select().From(KcbDonthuocChitiet.Schema.TableName)
                            .Where(KcbDonthuocChitiet.TrangthaiThanhtoanColumn.ColumnName).IsEqualTo(1)
                            .And(KcbDonthuocChitiet.IdDonthuocColumn.ColumnName).IsEqualTo(Pres_ID)
                            .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                        if (vCollect.Count > 0)
                        {
                            Utility.ShowMsg(
                                "Đơn vắc xin bạn đang chọn sửa đã được thanh toán. Muốn sửa lại đơn vắc xin Bạn cần phải liên hệ với bộ phận Thanh toán để hủy thanh toán và Bộ phận cấp vắc xin để hủy xác nhận đơn vắc xin tại kho vắc xin");
                            return;
                        }
                        if (!KiemtraDudieukienTiemchung())
                        {
                            Utility.ShowMsg("Bạn đang đánh dấu Bệnh nhân có dấu hiệu chưa đủ điều kiện để tiêm chủng nên hệ thống không cho phép bạn cập nhật Vắc xin tiêm cho Bệnh nhân\nMời bạn kiểm tra lại");
                            return;
                        }
                        KcbDonthuoc objPrescription = KcbDonthuoc.FetchByID(Pres_ID);
                        if (objPrescription != null)
                        {
                            frm_KCB_KeVacxin_Tiemchung frm = new frm_KCB_KeVacxin_Tiemchung("THUOC");
                            frm.em_Action = action.Update;
                            frm._KcbChandoanKetluan = _kcbChandoanKetluan;
                            frm.dt_ICD = dt_ICD;
                            frm.noitru = 0;
                            frm.objLuotkham = this.objLuotkham;
                            frm.objBenhnhan = this.objBenhnhan;
                            frm.id_kham = Utility.Int32Dbnull(txtExam_ID.Text);
                            frm.objRegExam = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txtReg_ID.Text));
                            frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                            frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                            frm.txtPres_ID.Text = Utility.sDbnull(objPrescription.IdDonthuoc);
                            frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                            frm.ShowDialog();
                            if (!frm.m_blnCancel)
                            {
                                if (frm._KcbChandoanKetluan != null) _kcbChandoanKetluan = frm._KcbChandoanKetluan;
                                Laythongtinchidinhngoaitru();
                                Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuocChitiet.Columns.IdDonthuoc,
                                                        Utility.sDbnull(frm.txtPres_ID.Text));
                                Utility.Log(this.Name, globalVariables.UserName,
                                            string.Format(
                                                "Sửa vắc xin của có mã lượt khám là: {0}, và mã bệnh nhân là {1}",
                                                objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                ModifyCommmands();
            }
        }

        List<int> GetIdChitiet(int IdDonthuoc,int id_thuoc, decimal don_gia, ref string s)
        {
            DataRow[] arrDr = m_dtChitietDonthuoc.Select(KcbDonthuocChitiet.Columns.IdDonthuoc + "=" + IdDonthuoc.ToString() + " AND " + KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc.ToString()
                    + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + don_gia.ToString());
            if (arrDr.Length > 0)
            {
                var p1 = (from q in arrDr.AsEnumerable()
                          select Utility.sDbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                s = string.Join(",", p1.ToArray());
                var p = (from q in arrDr.AsEnumerable()
                         select Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                return p.ToList<int>();
            }
            return new List<int>();
        }
        void DeletefromDatatable(List<int> lstIdChitietDonthuoc)
        {
            try
            {
                var p = (from q in m_dtChitietDonthuoc.Select("1=1").AsEnumerable()
                         where lstIdChitietDonthuoc.Contains(Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]))
                         select q).ToArray<DataRow>();
                for (int i = 0; i <= p.Length - 1; i++)
                    m_dtChitietDonthuoc.Rows.Remove(p[i]);
                m_dtChitietDonthuoc.AcceptChanges();
            }
            catch
            {
            }
        }
        private void PerformActionDeletePres()
        {
            try
            {
                string s = "";
                List<int> lstIdchitiet = new List<int>();
                foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
                {
                    string stempt = "";
                    int id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, 0m);
                    int IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value, 0m);
                    decimal dongia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, 0m);
                    List<int> _temp = GetIdChitiet(IdDonthuoc, id_thuoc, dongia, ref stempt);
                    s += "," + stempt;
                    lstIdchitiet.AddRange(_temp);
                    gridExRow.Delete();
                    grdPresDetail.UpdateData();

                }
                _KCB_KEDONTHUOC.XoaChitietDonthuoc(s);
                DeletefromDatatable(lstIdchitiet);
                m_dtChitietDonthuoc.AcceptChanges();
                Utility.Log(this.Name, globalVariables.UserName,
                                             string.Format(
                                                 "Xóa vắc xin của có mã lượt khám là: {0}, và mã bệnh nhân là {1}",
                                                 objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }
        private void PerformActionDeletePres_old()
        {
            string s = "";
            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                int presId = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value,
                                                  -1);
                int vIntIdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                                                        -1);
                int vIntIdThuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value,
                                                  -1);
                _KCB_KEDONTHUOC.XoaChitietDonthuoc(vIntIdDonthuoc);
                gridExRow.Delete();
                grdPresDetail.UpdateData();
                m_dtChitietDonthuoc.AcceptChanges();
            }
        }
       
        private void PerformActionDeleteSelectedDrug()
        {
            try
            {
                int Pres_ID =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value,
                                        -1);
                int v_intIDDonthuoc =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                                        -1);
                int v_intIDThuoc =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value,
                                        -1);
                _KCB_KEDONTHUOC.XoaChitietDonthuoc(v_intIDDonthuoc);
                grdPresDetail.CurrentRow.Delete();
                grdPresDetail.UpdateData();
                m_dtChitietDonthuoc.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message + "-->" +
                                "Bạn nên dùng chức năng xóa vắc xin bằng cách chọn vắc xin và sử dụng nút xóa vắc xin");
            }
        }

        private bool KiemtraThuocTruockhixoa()
        {
            bool b_Cancel = false;
            if (grdPresDetail.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện việc xóa thông tin vắc xin ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }

            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                if (Utility.sDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "") != globalVariables.UserName)
                {
                    Utility.ShowMsg("Trong các vắc xin bạn chọn xóa, có một số vắc xin được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các vắc xin do chính bạn kê để thực hiện xóa");
                    return false;

                }
            }
            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    int v_IdChitietdonthuoc =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                    int v_intIDThuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1);
                    KcbDonthuocChitiet _KcbDonthuocChitiet = KcbDonthuocChitiet.FetchByID(v_IdChitietdonthuoc);
                    if (_KcbDonthuocChitiet != null && (Utility.Byte2Bool(_KcbDonthuocChitiet.TrangthaiThanhtoan) || Utility.Byte2Bool(_KcbDonthuocChitiet.TrangThai)))
                    {
                        b_Cancel = true;
                        break;
                    }
                }
            }
            if (b_Cancel)
            {
                Utility.ShowMsg("Một số thuốc bạn chọn đã thanh toán hoặc đã phát thuốc cho Bệnh nhân nên bạn không được phép xóa. Mời bạn kiểm tra lại ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }
            return true;
        }

      
       

        private bool IsValidDeleteSelectedDrug()
        {
            bool b_Cancel = false;
            KcbLuotkham _item = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text, 0), m_strMaLuotkham);
            if (_item == null)
            {
                Utility.ShowMsg("Bạn phải chọn Bệnh nhân hoặc bệnh nhân không tồn tại!", "Thông báo",
                                MessageBoxIcon.Warning);
                txtPatient_Code.Focus();
                return false;
            }

            if (_item != null && Utility.Int32Dbnull(_item.TrangthaiNoitru, -1) >= 1)
            {
                Utility.ShowMsg("Bệnh nhân đã được điều trị nội trú. Bạn chỉ có thể xem thông tin và không được phép làm các công việc liên quan đến ngoại trú", "Thông báo");
                cmdSave.Focus();
                return false;
            }
            if (grdPresDetail.RowCount <= 0 || grdPresDetail.CurrentRow.RowType != RowType.Record)
            {
                Utility.ShowMsg("Bạn phải chọn một vắc xin để xóa ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }
            if (Utility.Coquyen("quyen_suadonthuoc") || Utility.sDbnull(grdPresDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "") == globalVariables.UserName)
            {
            }
            else
            {
                Utility.ShowMsg("vắc xin đang chọn xóa được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các vắc xin do chính bạn kê để thực hiện xóa");
                return false;
            }

            if (grdPresDetail.CurrentRow.RowType == RowType.Record)
            {
                int v_IdChitietdonthuoc =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                                        -1);
                KcbDonthuocChitiet _KcbDonthuocChitiet = KcbDonthuocChitiet.FetchByID(v_IdChitietdonthuoc);
                if (_KcbDonthuocChitiet != null && (Utility.Byte2Bool(_KcbDonthuocChitiet.TrangthaiThanhtoan) || Utility.Byte2Bool(_KcbDonthuocChitiet.TrangThai)))
                {
                    b_Cancel = true;
                }
            }

            if (b_Cancel)
            {
                Utility.ShowMsg("Một số thuốc bạn chọn đã thanh toán hoặc đã phát thuốc cho Bệnh nhân nên bạn không được phép xóa. Mời bạn kiểm tra lại ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }
            return true;
        }

        private void cmdDeletePres_Click(object sender, EventArgs e)
        {
            if (!KiemtraThuocTruockhixoa()) return;
            PerformActionDeletePres();
            ModifyCommmands();
        }

        /// <summary>
        /// ham thực hiện việc in phiếu thuôc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPrintPres_Click(object sender, EventArgs e)
        {
            try
            {
               
                int Pres_ID = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                PrintPres(Pres_ID,"");
            }
            catch (Exception)
            {
                // throw;
            }
        }

        /// <summary>
        /// hàm thực hiện việc in đơn vắc xin
        /// </summary>
        /// <param name="presId"></param>
        private void PrintPres(int presId, string forcedTitle)
        {
            DataTable v_dtData = _KCB_KEDONTHUOC.LaythongtinDonthuoc_In(presId);
            Utility.AddColumToDataTable(ref v_dtData, "BarCode", typeof (byte[]));
            Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            //barcode.Data = Utility.sDbnull(Pres_ID);
            byte[] Barcode = null;
            Utility.CreateBarcodeData(ref v_dtData, m_strMaLuotkham, ref Barcode);
            string ICD_Name = "";
            string ICD_Code = "";
            
            foreach (DataRow drv in v_dtData.Rows)
            {
                drv["BarCode"] = Barcode;
                drv["chan_doan"] = Utility.sDbnull(drv["chan_doan"]).Trim() == ""
                                    ? ICD_Name
                                    : Utility.sDbnull(drv["chan_doan"]) + ";" + ICD_Name;
                drv["ma_icd"] = ICD_Code;
            }
        //    THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA4.xml");
            v_dtData.AcceptChanges();
            // log.Info("Thuc hien in don thuoc");
            Utility.UpdateLogotoDatatable(ref v_dtData);
            string KhoGiay = "A5";
            if (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4) KhoGiay = "A4";
            ReportDocument reportDocument = new ReportDocument();
             string tieude="", reportname = "",reportCode="";
            switch (KhoGiay)
            {
                case "A5":
                    reportCode = "thamkham_InDonthuocA5";
                    reportDocument =  Utility.GetReport("thamkham_InDonthuocA5",ref tieude,ref reportname);
                    break;
                case "A4":
                    reportCode = "thamkham_InDonthuocA4";
                    reportDocument = Utility.GetReport("thamkham_InDonthuocA4" ,ref tieude,ref reportname);
                    break;
                default:
                    reportCode = "thamkham_InDonthuocA5";
                    reportDocument = Utility.GetReport("thamkham_InDonthuocA5" ,ref tieude,ref reportname);
                    break;
            }
            if (reportDocument == null) return;
            if (Utility.DoTrim(forcedTitle).Length > 0)
                tieude = forcedTitle;
            Utility.WaitNow(this);
            ReportDocument crpt = reportDocument;
            var objForm = new frmPrintPreview("IN ĐƠN vắc xin BỆNH NHÂN", crpt, true, true);
            try
            {
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                crpt.SetDataSource(v_dtData);
                Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt,"Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt,"ReportTitle", "ĐƠN vắc xin");
                Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInDonthuoc))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    objForm.ShowDialog();
                    cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.DefaultNow(this);
            }
        }
        #endregion

        #region "Xử lý tác vụ của phần lưu thông tin "

        private void cmdSave_Click(object sender, EventArgs e)
        {
            PerformAction();
        }

        string GetDanhsachBenhphu()
        {
            var sMaICDPHU = new StringBuilder("");
            try
            {
                return sMaICDPHU.ToString();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// HÀM KHƠI TẠO PHẦN CHỈ ĐỊNH CHUẨN ĐOÁN
        /// </summary>
        /// <returns></returns>
        private KcbChandoanKetluan TaoDulieuChandoanKetluan()
        {
            try
            {
                if (_kcbChandoanKetluan == null)
                {
                    _kcbChandoanKetluan = new KcbChandoanKetluan();
                    _kcbChandoanKetluan.IsNew = true;
                }
                else
                {
                    _kcbChandoanKetluan.IsNew = false;
                    _kcbChandoanKetluan.MarkOld();
                }
                _kcbChandoanKetluan.IdKham = Utility.Int64Dbnull(txtExam_ID.Text, -1);
                _kcbChandoanKetluan.MaLuotkham = Utility.sDbnull(m_strMaLuotkham, "");
                _kcbChandoanKetluan.IdBenhnhan = Utility.Int64Dbnull(txtPatient_ID.Text, "-1");
                _kcbChandoanKetluan.MabenhChinh = objLuotkham.MabenhChinh;
                _kcbChandoanKetluan.Nhommau = "";
                _kcbChandoanKetluan.Nhietdo = "";
                _kcbChandoanKetluan.TrieuchungBandau = objLuotkham.TrieuChung;
                _kcbChandoanKetluan.Huyetap = "";
                _kcbChandoanKetluan.Mach = "";
                _kcbChandoanKetluan.Nhiptim = "";
                _kcbChandoanKetluan.Nhiptho = "";
                _kcbChandoanKetluan.Chieucao = "";
                _kcbChandoanKetluan.Cannang = "";
                _kcbChandoanKetluan.HuongDieutri = txtHuongdieutri.myCode.Trim();
                _kcbChandoanKetluan.SongayDieutri = 0;
                _kcbChandoanKetluan.Ketluan = txtKQ.myCode;
                _kcbChandoanKetluan.KetluanNguyennhan = txtKet_Luan.myCode;
                _kcbChandoanKetluan.PhanungSautiemchung = txtPhanungSautiem.myCode;
                _kcbChandoanKetluan.ChongchidinhKhac = Utility.sDbnull(txtChongchidinhkhac.myCode);
                _kcbChandoanKetluan.KPL1 = Utility.Bool2byte(chkKPL1.Checked);
                _kcbChandoanKetluan.KPL2 = Utility.Bool2byte(chkKPL2.Checked);
                _kcbChandoanKetluan.KPL3 = Utility.Bool2byte(chkKPL3.Checked);
                _kcbChandoanKetluan.KPL4 = Utility.Bool2byte(chkKPL4.Checked);
                _kcbChandoanKetluan.KPL5 = Utility.Bool2byte(chkKPL5.Checked);
                _kcbChandoanKetluan.KPL6 = Utility.Bool2byte(chkKPL6.Checked);
                _kcbChandoanKetluan.KPL7 = Utility.Bool2byte(chkKPL7.Checked);
                _kcbChandoanKetluan.KPL8 = Utility.Bool2byte(chkKPL8.Checked);

                _kcbChandoanKetluan.KL1 = Utility.Bool2byte(chkKL1.Checked);
                _kcbChandoanKetluan.KL2 = Utility.Bool2byte(chkKL2.Checked);
                _kcbChandoanKetluan.KL3 = Utility.Bool2byte(chkKL3.Checked);

                if (Utility.Int16Dbnull(txtBacsi.MyID,-1)> 0)
                    _kcbChandoanKetluan.IdBacsikham = Utility.Int16Dbnull(txtBacsi.MyID, -1);
                else
                {
                    _kcbChandoanKetluan.IdBacsikham = globalVariables.gv_intIDNhanvien;
                }
                _kcbChandoanKetluan.MabenhPhu = objLuotkham.MabenhPhu;
                if (objkcbdangky != null)
                {
                    _kcbChandoanKetluan.IdKhoanoitru = Utility.Int32Dbnull(objkcbdangky.IdKhoakcb, -1);
                    _kcbChandoanKetluan.IdPhongkham = Utility.Int32Dbnull(objkcbdangky.IdPhongkham, -1);
                    DmucKhoaphong objDepartment = DmucKhoaphong.FetchByID(Utility.Int32Dbnull(objkcbdangky.IdPhongkham, -1));
                    if (objDepartment != null)
                    {
                        _kcbChandoanKetluan.TenPhongkham = Utility.sDbnull(objDepartment.TenKhoaphong, "");
                    }
                }
                else
                {
                    _kcbChandoanKetluan.IdKhoanoitru = globalVariables.idKhoatheoMay;
                    _kcbChandoanKetluan.IdPhongkham = globalVariables.idKhoatheoMay;
                }
                _kcbChandoanKetluan.IdKham = Utility.Int32Dbnull(txt_idchidinhphongkham.Text, -1);
                _kcbChandoanKetluan.NgayTao = dtpCreatedDate.Value;
                _kcbChandoanKetluan.NguoiTao = globalVariables.UserName;
                _kcbChandoanKetluan.NgayChandoan = dtpCreatedDate.Value;
                _kcbChandoanKetluan.Chandoan = objLuotkham.ChanDoan;
                _kcbChandoanKetluan.ChandoanKemtheo = objLuotkham.ChandoanKemtheo;
                _kcbChandoanKetluan.IdPhieudieutri = -1;
                _kcbChandoanKetluan.Noitru = 0;
                return _kcbChandoanKetluan;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của dữ liệu trước khi đóng gói dữ liệu vào Entity
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            Utility.SetMsg(lblMsg, "", false);
            if (Utility.Int32Dbnull(txtBacsi.MyID, -1) <= 0)
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn bác sĩ khám trước khi kết thúc khám ngoại trú cho Bệnh nhân", true);
                txtBacsi.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtPatient_Code.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn Bệnh nhân để thực hiện thăm khám", true);
                txtPatient_Code.Focus();
                return false;
            }
            KcbLuotkham _item = Utility.getKcbLuotkham( Utility.Int64Dbnull(txtPatient_ID.Text,0),m_strMaLuotkham);
            if (_item==null)
            {
                Utility.ShowMsg("Bạn phải chọn Bệnh nhân hoặc bệnh nhân không tồn tại!", "Thông báo",
                                MessageBoxIcon.Warning);
                txtPatient_Code.Focus();
                return false;
            }
          
            if (_item!=null && Utility.Int32Dbnull(_item.TrangthaiNoitru,-1)>=2)
            {
                Utility.ShowMsg("Bệnh nhân đã được điều trị nội trú. Bạn chỉ có thể xem thông tin và không được phép làm các công việc liên quan đến ngoại trú", "Thông báo");
                cmdSave.Focus();
                return false;
            }
            if (Utility.DoTrim(txtKQ.Text) == "" && chkKL1.Checked)
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập kết quả tiêm chủng cho bệnh nhân", true);
                tabDiagInfo.SelectedTab = tabPageChanDoan;
                txtKQ.Focus();
                return false;
            }
            
            chkDaThucHien.Checked = true;
            chkDaThucHien.Visible = chkDaThucHien.Checked;
            
            return true;
        }

        /// <summary>
        /// Thực hiện thao tác Insert,Update,Delete tới CSDL theo m_enAction
        /// </summary>
        private void PerformAction()
        {
            try
            {
                //Kiểm tra tính hợp lệ của dữ liệu trước khi thêm mới
                if (!IsValidData())
                {
                    return;
                }
                objkcbdangky.TrangThai = (byte?) (chkDaThucHien.Checked ? 1 : 0);
                DataRow[] arrDr = m_dtDanhsachbenhnhanthamkham.Select("id_kham=" + txtReg_ID.Text);
                if (arrDr.Length > 0)
                {

                    arrDr[0]["trang_thai"] = chkDaThucHien.Checked ? 1 : 0;
                }
                objkcbdangky.IdBacsikham = Utility.Int16Dbnull(txtBacsi.MyID, -1);
                if (!THU_VIEN_CHUNG.IsBaoHiem((byte) objLuotkham.IdLoaidoituongKcb))//Đối tượng dịch vụ được khóa ngay sau khi kết thúc khám
                {
                        objLuotkham.NguoiKetthuc =chkDaThucHien.Checked? globalVariables.UserName:"";
                        if (chkDaThucHien.Checked)
                            objLuotkham.NgayKetthuc = globalVariables.SysDate;
                        else
                            objLuotkham.NgayKetthuc = null;
                        objLuotkham.Locked = chkDaThucHien.Checked ? (byte)1 : (byte)0;
                        objLuotkham.TrangthaiNgoaitru = objLuotkham.Locked;
                }
                ActionResult actionResult =
                   _KCB_THAMKHAM.UpdateExamInfo(
                         TaoDulieuChandoanKetluan(), objkcbdangky, objLuotkham);
                switch (actionResult)
                {
                    case ActionResult.Success:
                       
                        IEnumerable<GridEXRow> query = from kham in grdList.GetDataRows()
                                                       where
                                                           kham.RowType == RowType.Record &&
                                                           Utility.Int32Dbnull(kham.Cells[KcbDangkyKcb.Columns.IdKham].Value) ==
                                                           Utility.Int32Dbnull(txt_idchidinhphongkham.Text)
                                                       select kham;
                        if (query.Count() > 0)
                        {
                            GridEXRow gridExRow = query.FirstOrDefault();
                            //gridExRow.BeginEdit();
                            gridExRow.Cells[KcbDangkyKcb.Columns.TrangThai].Value = (byte?) (chkDaThucHien.Checked ? 1 : 0);
                            gridExRow.Cells[KcbLuotkham.Columns.NguoiKetthuc].Value = globalVariables.UserName;
                            gridExRow.Cells[KcbLuotkham.Columns.NgayKetthuc].Value = globalVariables.SysDate;
                            //gridExRow.EndEdit();
                            grdList.UpdateData();
                            Utility.GotoNewRowJanus(grdList, KcbDangkyKcb.Columns.IdKham, Utility.sDbnull(txt_idchidinhphongkham.Text));
                        }
                        chkDaThucHien.Checked = true;
                        //Tự động ẩn BN về tab đã khám
                        int Status = radChuaKham.Checked ? 0 : 1;
                        if (Status == 0)
                        {
                            m_dtDanhsachbenhnhanthamkham.DefaultView.RowFilter = "1=1";
                            m_dtDanhsachbenhnhanthamkham.DefaultView.RowFilter = "trang_thai=" + Status;
                        }
                        if (objLuotkham.Locked == 1 && objLuotkham.TrangthaiNoitru <= 0)//Đối tượng dịch vụ được khóa ngay sau khi kết thúc khám
                        {
                            cmdUnlock.Visible = true;
                            cmdUnlock.Enabled = true;
                        }
                        var objStaff = (from p in globalVariables.gv_dtDmucNhanvien.AsEnumerable()
                                        where p[DmucNhanvien.Columns.UserName].Equals(objLuotkham.NguoiKetthuc)
                                        select p).FirstOrDefault();
                       // DmucNhanvien objStaff = new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.UserNameColumn).IsEqualTo(Utility.sDbnull(objLuotkham.NguoiKetthuc, "")).ExecuteSingle<DmucNhanvien>();
                        string TenNhanvien = objLuotkham.NguoiKetthuc;
                        if (objStaff != null)
                            TenNhanvien = Utility.sDbnull(objStaff["ten_nhanvien"], "");
                        if (!cmdUnlock.Enabled)
                            toolTip1.SetToolTip(cmdUnlock, "Bạn không có quyền mở khóa Bệnh nhân này. Đề nghị liên hệ " + TenNhanvien + "(" + objLuotkham.NguoiKetthuc + " - Là người khóa BN này) để được họ mở khóa. Hoặc liên hệ Quản trị hệ thống");
                        else
                            toolTip1.SetToolTip(cmdUnlock, "Nhấn vào đây để mở khóa cho bệnh nhân đang chọn(Phím tắt Ctrl+U). Điều kiện là chỉ mở khóa đối với đối tượng Dịch vụ. Muốn mở khóa đối tượng BHYT thì cần liên lạc với bộ phận thanh toán hủy in phôi BHYT");
                        //Tự động bật tính năng nhập viện nội trú nếu hướng điều trị chọn là Nội trú và Bệnh nhân chưa nhập viện
                        if (objLuotkham.TrangthaiNoitru == 0 && txtHuongdieutri.myCode.ToUpper() == THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_MAHUONGDIEUTRI_CHUYENVIEN", false).ToUpper())
                        {
                            cmdChuyenVien_Click(cmdChuyenVien, new EventArgs()); ;
                        }
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá lưu thông tin ", "Thông báo lỗi", MessageBoxIcon.Error);
                        break;
                }
                ModifyCommmands();
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
        }

        #endregion

        private void nmrSongayDT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                dtNgayNhapVien.Focus();
            }
        }

        private void dtNgayNhapVien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtKhoaNoiTru.Focus();
                txtKhoaNoiTru.SelectAll();
                
            }
        }

        private void cmdTimKiemKhoaNoiTru_Click(object sender, EventArgs e)
        {
            TimKiemKhoaNoiTru();
        }

        private void chkKPL8_CheckedChanged(object sender, EventArgs e)
        {
            if (chkKPL8.Checked)
                txtChongchidinhkhac.Enabled = true;
            else
            {
                txtChongchidinhkhac.Enabled = false;
            }
        }

        private void chkKL1_EnabledChanged(object sender, EventArgs e)
        {
            //if (chkKPL1.Checked || chkKPL2.Checked || chkKPL3.Checked || chkKPL4.Checked || chkKPL5.Checked || chkKPL6.Checked || chkKPL7.Checked || chkKPL8.Checked || chkKL2.Checked || chkKL3.Checked)
            //{
            //    chkKL1.Enabled = false;
            //}
            //else
            //{
            //    chkKL1.Enabled = true;
            //}
                
        }

        private void cmdNewPatient_Click(object sender, EventArgs e)
        {
            //frm_KCB_DANGKY frm = new frm_KCB_DANGKY("KTC");
            //frm.ShowDialog();
        }

    }
}