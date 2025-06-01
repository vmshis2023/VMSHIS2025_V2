using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Transactions;
using System.Windows.Annotations;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using Janus.Windows.UI.StatusBar;
using SubSonic;
using VNS.HIS.UI.Classess;
using VNS.HIS.UI.Classess.API;
using VNS.HIS.UI.Forms.Dungchung;
using VNS.Libs;
using VMS.HIS.DAL;
using NLog;
using VMS.QMS;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.Classes;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.THANHTOAN;
using VNS.HIS.UI.DANHMUC;
using VNS.Libs.AppUI;
using VNS.HIS.UI.Forms.Cauhinh;
using CrystalDecisions.CrystalReports.Engine;
using VMS.HIS.Ngoaitru.CheckCard;

namespace VNS.HIS.UI.NGOAITRU
{
   
    public partial class frm_KCB_DANGKY_CAPNHAT : Form
    {
        public delegate void OnActionSuccess();
        public event OnActionSuccess _OnActionSuccess;
        public string Maluotkham = "";
        public int _mabenhnhan = -1;
        readonly KCB_DANGKY _kcbDangky = new KCB_DANGKY();
        readonly KCB_QMS _KCB_QMS = new KCB_QMS();
        private readonly string _strSaveandprintPath = Application.StartupPath + @"\CAUHINH\SaveAndPrintConfig.txt";

        private string MA_DTUONG = "DV";
        private string SoBHYT = "";
        private string TrongGio = "";
        
        public bool MBlnCancel;
        private bool b_HasLoaded;
        private bool b_HasSecondScreen;
        private bool b_NhapNamSinh;
        public SysTrace myTrace;
        public GridEX grdList;
        private bool _hasjustpressBackKey;
        private bool isAutoFinding;
        private bool m_blnHasJustInsert = false;
        private DataTable m_DC;

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
        private Logger log;
        public frm_KCB_DANGKY_CAPNHAT(string Args)
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
                dtInsFromDate.Value = new DateTime(globalVariables.SysDate.Year, 01, 01);
                dtInsToDate.Value = new DateTime(globalVariables.SysDate.Year, 12, 31);
                log = LogManager.GetLogger(Name);

                InitEvents();
                CauHinhQMS();
                CauHinhKCB();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                
            }
       }

        void InitEvents()
        {
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_KCB_DANGKY_CAPNHAT_FormClosing);
            this.Load += new System.EventHandler(this.frm_KCB_DANGKY_CAPNHAT_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_KCB_DANGKY_CAPNHAT_KeyDown);

            txtMaBN.KeyDown += new KeyEventHandler(txtMaBN_KeyDown);
            txtMaLankham.KeyDown += new KeyEventHandler(txtMaLankham_KeyDown);
            txtMaLankham.TextChanged += txtMaLankham_TextChanged;

            dtpBOD.TextChanged += dtpBOD_TextChanged;
            txtMaDtuong_BHYT.KeyDown += txtMaDtuong_BHYT_KeyDown;
            txtMaDtuong_BHYT.TextChanged += new EventHandler(txtMaDtuong_BHYT_TextChanged);
            txtMaDtuong_BHYT.LostFocus += txtMaQuyenloi_BHYT_LostFocus;

            txtMaQuyenloi_BHYT.KeyDown += txtMaQuyenloi_BHYT_KeyDown;
            txtMaQuyenloi_BHYT.TextChanged += new EventHandler(txtMaQuyenloi_BHYT_TextChanged);
            txtMaQuyenloi_BHYT.PreviewKeyDown += txtMaQuyenloi_BHYT_PreviewKeyDown;
            txtMaQuyenloi_BHYT.KeyPress += txtMaQuyenloi_BHYT_KeyPress;
            txtMaQuyenloi_BHYT.LostFocus += txtMaQuyenloi_BHYT_LostFocus;

            txtNoiphattheBHYT.TextChanged += new EventHandler(txtNoiphattheBHYT_TextChanged);
            txtNoiphattheBHYT.KeyDown += txtNoiphattheBHYT_KeyDown;
            txtOthu4.KeyDown += txtOthu4_KeyDown;
            txtOthu4.TextChanged += new EventHandler(txtOthu4_TextChanged);
            txtOthu5.KeyDown += txtOthu5_KeyDown;
            txtOthu5.TextChanged += new EventHandler(txtOthu5_TextChanged);
            txtOthu6.TextChanged += new EventHandler(txtOthu6_TextChanged);
            txtOthu6.KeyDown += txtOthu6_KeyDown;
            txtOthu6.LostFocus += _LostFocus;
            txtNoiDKKCBBD.LostFocus += txtNoiDKKCBBD_LostFocus;
            txtNoiDKKCBBD.KeyDown += txtNoiDKKCBBD_KeyDown;
            txtNoiDKKCBBD.TextChanged += new EventHandler(txtNoiDKKCBBD_TextChanged);
            txtQRCode.LostFocus += txtQRCode_LostFocus;
            txtQRCode.TextChanged += txtQRCode_TextChanged;
            txtNoiDongtrusoKCBBD.TextChanged += new EventHandler(txtNoiDongtrusoKCBBD_TextChanged);
            txtNoiDongtrusoKCBBD.KeyDown += txtNoiDongtrusoKCBBD_KeyDown;

            txtTEN_BN.TextChanged+=new EventHandler(txtTEN_BN_TextChanged);
            txtTEN_BN.LostFocus += txtTEN_BN_LostFocus;
            txtCMT.KeyDown += txtCMT_KeyDown;
            chkTraiTuyen.CheckedChanged += chkTraiTuyen_CheckedChanged;
            txtKieuKham._OnSelectionChanged += new VNS.HIS.UCs.AutoCompleteTextbox.OnSelectionChanged(txtKieuKham__OnSelectionChanged);
            cboPatientSex.SelectedIndex = 0;
            txtPhongkham._OnSelectionChanged += new VNS.HIS.UCs.AutoCompleteTextbox.OnSelectionChanged(txtPhongkham__OnSelectionChanged);
            cmdConfig.Click += new EventHandler(cmdConfig_Click);
            
            cmdThemMoiBN.Click += new System.EventHandler(cmdThemMoiBN_Click);
            cmdSave.Click += new System.EventHandler(cmdSave_Click);

            lnkRestoreIgnoreQMS.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkRestoreIgnoreQMS_LinkClicked);
            cmdStart.Click += new System.EventHandler(cmdStart_Click);
            cmdStop.Click += new System.EventHandler(cmdStop_Click);
            cmdGoiSoKham.Click += new System.EventHandler(cmdGoiSoKham_Click);
            cmdXoaSoKham.Click += new System.EventHandler(cmdXoaSoKham_Click);
            lnkThem.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkThem_LinkClicked);
            txtTuoi.TextChanged += new System.EventHandler(txtTuoi_TextChanged);
            txtTuoi.Click += new System.EventHandler(txtTuoi_Click);
            txtTuoi.KeyDown += new System.Windows.Forms.KeyEventHandler(txtTuoi_KeyDown);

            grdRegExam.ColumnButtonClick+=new ColumnActionEventHandler(grdRegExam_ColumnButtonClick);
            grdRegExam.SelectionChanged+=new EventHandler(grdRegExam_SelectionChanged);
            txtTuoi.LostFocus += txtTuoi_LostFocus;
            txtDiachi.LostFocus += txtDiachi_LostFocus;
            txtSoQMS.TextChanged+=new EventHandler(txtSoQMS_TextChanged);
            chkTudongthemmoi.CheckedChanged += new EventHandler(chkTudongthemmoi_CheckedChanged);
            cmdQMSProperty.Click += new EventHandler(cmdQMSProperty_Click);

            cmdThanhToanKham.Click += new EventHandler(cmdThanhToanKham_Click);
            cmdXoaKham.Click += new EventHandler(cmdXoaKham_Click);
            cmdInPhieuKham.Click+=new EventHandler(cmdInPhieuKham_Click);
            txtExamtypeCode._OnSelectionChanged += new UCs.AutoCompleteTextbox.OnSelectionChanged(txtExamtypeCode__OnSelectionChanged);
            txtPhongkham._OnEnterMe += new VNS.HIS.UCs.AutoCompleteTextbox.OnEnterMe(txtPhongkham__OnEnterMe);
            txtKieuKham._OnEnterMe += new VNS.HIS.UCs.AutoCompleteTextbox.OnEnterMe(txtKieuKham__OnEnterMe);
            cboKieuKham.ValueChanged += new EventHandler(cboKieuKham_ValueChanged);
            chkChuyenVien.CheckedChanged += new EventHandler(chkChuyenVien_CheckedChanged);
            cboDoituongKCB.SelectedIndexChanged += new EventHandler(cboDoituongKCB_SelectedIndexChanged);
           
            cmdInBienlai.Click += new EventHandler(cmdInlaihoadon_Click);
            cmdInhoadon.Click += new EventHandler(cmdInhoadon_Click);

           

            txtTrieuChungBD._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtTrieuChungBD__OnShowData);
            txtDantoc._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtDantoc__OnShowData);
            txtNgheNghiep._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtNgheNghiep__OnShowData);

            txtLoaiBN._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtLoaiBN__OnShowData);
            txtLoaiBN._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtLoaiBN__OnSaveAs);

            txtMaDTsinhsong._OnEnterMe += txtMaDTsinhsong__OnEnterMe;
        //    chkGiayBHYT.CheckedChanged += chkGiayBHYT_CheckedChanged;
            cmdGetBV.Click += new EventHandler(cmdGetBV_Click);
            cmdThemmoiDiachinh.Click += cmdThemmoiDiachinh_Click;
            chkLaysokham.CheckedChanged += chkLaysokham_CheckedChanged;
            cmdRestore.Click += cmdRestore_Click;
            txtLoaikham._OnShowData += txtLoaikham__OnShowData;
            mnuBOD.Click += mnuBOD_Click;
            txtMaLankham.LostFocus += txtMaLankham_LostFocus;
            txtSoBA.KeyDown += txtSoBA_KeyDown;
        }

        void txtDiachi_LostFocus(object sender, EventArgs e)
        {
            string s=Utility.DoTrim(txtDiachi.Text);
            txtDiachi.Text = s.Substring(0, 1).ToUpper() + s.Substring(1);
        }

        void txtSoBA_KeyDown(object sender, KeyEventArgs e)
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

        void txtMaLankham_LostFocus(object sender, EventArgs e)
        {
            if (Utility.DoTrim(txtMaLankham.Text).Length >= 8 && Utility.DoTrim(txtMaLankham.Text)!=m_strMaluotkham)//Đã bị thay đổi do nhập tay
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

        void txtMaLankham_TextChanged(object sender, EventArgs e)
        {
            
        }

        void mnuBOD_Click(object sender, EventArgs e)
        {
            PropertyLib._KCBProperties.Nhapngaythangnamsinh = mnuBOD.Checked;
            PropertyLib.SaveProperty(PropertyLib._KCBProperties);
            dtpBOD.CustomFormat = mnuBOD.Checked ? "dd/MM/yyyy HH:mm" : "yyyy";
            txtTuoi.Enabled = dtpBOD.CustomFormat == "yyyy";
            lblLoaituoi.Visible = dtpBOD.CustomFormat != "yyyy";
            dtpBOD_TextChanged(dtpBOD, e);
        }

        void txtLoaikham__OnShowData()
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(txtLoaikham.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtLoaikham.myCode;
                txtLoaikham.Init();
                txtLoaikham.SetCode(oldCode);
                txtLoaikham.Focus();
            } 
        }

        void cmdRestore_Click(object sender, EventArgs e)
        {
            var frm = new frm_SoKham_GoiLai();
            frm._OnActiveQMS += frm__OnActiveQMS;
            frm.ShowDialog();
        }

        void chkLaysokham_CheckedChanged(object sender, EventArgs e)
        {
            txtSoKcb.Enabled =chkLaysokham.Enabled && chkLaysokham.Checked;
        }

        void cmdThemmoiDiachinh_Click(object sender, EventArgs e)
        {
            frm_themmoi_diachinh_new themmoiDiachinh = new frm_themmoi_diachinh_new();
            themmoiDiachinh.ShowDialog();
            if (themmoiDiachinh.m_blnHasChanged)
            {
               
                AddAutoCompleteDiaChi();
            }
        }
        bool _allowAgeChanged = true;
        void dtpBOD_TextChanged(object sender, EventArgs e)
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

       

        void chkGiayBHYT_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkTraiTuyen.Checked && chkGiayBHYT.Checked)
            //    chkTraiTuyen.Checked = false;
            lbldu5nam.Visible = dtpNgaydu5nam.Visible = false;
            TinhPtramBhyt();
            if(chkGiayBHYT.Checked)
            {
                chkTraiTuyen.Checked = false;
                lbldu5nam.Visible = dtpNgaydu5nam.Visible = true;
                dtpNgaydu5nam.Value = globalVariables.SysDate;
            }
            else
            {
                lbldu5nam.Visible = dtpNgaydu5nam.Visible = false;
            }
        }

        void txtMaDTsinhsong__OnEnterMe()
        {
            if (txtMaDTsinhsong.myCode != "-1")
            {
                if ( chkTraiTuyen.Checked)
                {
                    chkTraiTuyen.Checked = false;
                    lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
                }
            }
            TinhPtramBhyt();
        }

        void txtLoaiBN__OnSaveAs()
        {
            if (Utility.DoTrim(txtLoaiBN.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(txtLoaiBN.LOAI_DANHMUC);
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

        void cmdGetBV_Click(object sender, EventArgs e)
        {
            frm_danhsachbenhvien danhsachbenhvien = new frm_danhsachbenhvien();
            if (danhsachbenhvien.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtNoichuyenden.SetId(danhsachbenhvien.idBenhvien);
            }
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
            txtKieuKham._Text = cboKieuKham.Text;
            txtIDKieuKham.Text = Utility.sDbnull(txtExamtypeCode.MyID);
        }

        void cmdInhoadon_Click(object sender, EventArgs e)
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


        void cboDoituongKCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowTextChanged) return;
                _maDoituongKcb = Utility.sDbnull(cboDoituongKCB.SelectedValue);
                _objDoituongKcb = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(_maDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
                ChangeObjectRegion();
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void chkChuyenVien_CheckedChanged(object sender, EventArgs e)
        {
            txtNoichuyenden.Enabled = chkChuyenVien.Checked;
            cmdGetBV.Enabled = chkChuyenVien.Checked;
            lblChandoantuyenduoi.Visible = txtchandoantuyenduoi.Visible = chkChuyenVien.Checked;
            //Tạm bỏ 2014-12-04
            //LoadClinicCode();
        }

        void cboKieuKham_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (AutoLoad || cboKieuKham.SelectedIndex == -1) return;
                int iddichvukcb = Utility.Int32Dbnull(cboKieuKham.Value);
                DmucDichvukcb objDichvuKCB =
                DmucDichvukcb.FetchByID(Utility.Int32Dbnull(cboKieuKham.Value));
                _objDoituongKcb =
                    new Select().From(DmucDoituongkcb.Schema)
                        .Where(DmucDoituongkcb.MaDoituongKcbColumn)
                        .IsEqualTo(_maDoituongKcb)
                        .ExecuteSingle<DmucDoituongkcb>();
                DmucKhoaphong objdepartment =
                    new Select().From(DmucKhoaphong.Schema)
                        .Where(DmucKhoaphong.MaKhoaphongColumn)
                        .IsEqualTo(globalVariables.MA_KHOA_THIEN)
                        .ExecuteSingle<DmucKhoaphong>();
                if (objDichvuKCB != null)
                {
                    txtKieuKham.SetId(objDichvuKCB.IdKieukham);
                    txtIDPkham.Text = Utility.sDbnull(objDichvuKCB.IdPhongkham);
                    cboKieuKham.Text = Utility.sDbnull(objDichvuKCB.TenDichvukcb);
                    //txtPhongkham._Text=
                }
                else
                    txtKieuKham.SetId(-1);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
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

        void cmdInlaihoadon_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdRegExam)) return;
                int Payment_Id = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdThanhtoan].Value);
                new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, Payment_Id,-1, _objLuotkham, 0);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        void cmdXoaKham_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdRegExam)) return;

            HuyThamKham();
        }

        private bool IsvalidHuyKham(int idkham)
        {
            SqlQuery sqlchandoan = new Select().From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(idkham);
            SqlQuery sqldonthuoc = new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.Columns.IdKham).IsEqualTo(idkham);
            SqlQuery sqlchidinh = new Select().From(KcbChidinhcl.Schema).Where(KcbChidinhcl.Columns.IdKham).IsEqualTo(idkham);
            SqlQuery sqlthanhtoan = new Select().From(KcbThanhtoanChitiet.Schema).Where(KcbThanhtoanChitiet.Columns.IdKham).IsEqualTo(idkham);
            if (sqlchandoan.GetRecordCount() <= 0 && sqldonthuoc.GetRecordCount() <= 0 && sqlchidinh.GetRecordCount() <= 0 && sqlthanhtoan.GetRecordCount()<=0)
            {
                return true;
            }
            return false;
        }
        void cmdThanhToanKham_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdRegExam)) return;
            if (Utility.sDbnull(cmdThanhToanKham.Tag, "ABCD") == "TT")
                Thanhtoan(true);
            else
                HuyThanhtoan();
        }

        void chkTudongthemmoi_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._KCBProperties.Tudongthemmoi = chkTudongthemmoi.Checked;
            PropertyLib.SaveProperty(PropertyLib._KCBProperties);
        }

        void cmdQMSProperty_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._HISQMSProperties);
            frm.ShowDialog();
            CauHinhQMS();
        }

        void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._KCBProperties);
            frm.ShowDialog();
            CauHinhKCB();
        }

        void txtPhongkham__OnSelectionChanged()
        {
            AutoLoadKieuKham();
        }

        void txtKieuKham__OnSelectionChanged()
        {
            AutoLoadKieuKham();
        }

        private string GetSoBHYT
        {
            get { return SoBHYT; }
            set { SoBHYT = value; }
        }

        private void txtTEN_BN_LostFocus(object sender, EventArgs e)
        {
            txtTEN_BN.Text =Utility.CapitalizeWords(txtTEN_BN.Text.Trim());
        }

        private void _LostFocus(object sender, EventArgs e)
        {
            if (isAutoFinding) return;
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_KIEMTRA20KYTU_BHYT", "0", false) == "1")
            {
                return;
            }
            string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() +
                             txtNoiphattheBHYT.Text.Trim() +
                             txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
            string noiDangky = txtNoiDongtrusoKCBBD.Text.Trim() + txtNoiDKKCBBD.Text.Trim();
            if (MA_BHYT.Length == 15 && noiDangky.Length == 5) FindPatientIDbyBhyt(MA_BHYT, noiDangky);
        }

        private void txtNoiphattheBHYT_KeyDown(object sender, KeyEventArgs e)
        {
            _hasjustpressBackKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                return;
            }
            if (e.KeyCode == Keys.Back)
            {
                _hasjustpressBackKey = true;
                if (txtNoiphattheBHYT.Text.Length <= 0)
                {
                    txtMaQuyenloi_BHYT.Focus();
                    txtMaQuyenloi_BHYT.Select(txtMaQuyenloi_BHYT.Text.Length, 0);
                }
            }
        }

        private void chkTraiTuyen_CheckedChanged(object sender, EventArgs e)
        {
            lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
            TinhPtramBhyt();
            if (chkTraiTuyen.Checked) chkGiayBHYT.Checked = false;
            
        }

        private void txtMaQuyenloi_BHYT_LostFocus(object sender, EventArgs e)
        {
        }

        private bool isAutobinding = true;
        private void txtNoiDKKCBBD_LostFocus(object sender, EventArgs e)
        {
            if (isAutobinding)
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_KIEMTRA20KYTU_BHYT", "0", false) == "1")
                {

                    string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() +
                                     txtNoiphattheBHYT.Text.Trim() + txtOthu4.Text.Trim() + txtOthu5.Text.Trim() +
                                     txtOthu6.Text.Trim();
                    string noiDangky = txtNoiDongtrusoKCBBD.Text.Trim() + txtNoiDKKCBBD.Text.Trim();
                    if (MA_BHYT.Length == 15 && noiDangky.Length == 5)
                    {
                        if (MEnAction != action.Update)
                        {
                            FindPatientIDbyBhyt(MA_BHYT, noiDangky);
                            isAutobinding = true;
                        }
                    }
                }
            }
          
        }

        private void txtMaQuyenloi_BHYT_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txtMaQuyenloi_BHYT_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }

        private void txtMaLankham_KeyDown(object sender, KeyEventArgs e)
        {
            //Chỉ reset lại mã lượt khám cũ nếu mã cũ và mã mới khác nhau. Tránh việc bỏ mã khi người dùng thao tác: Thêm mới-->Enter trên ô Mã lượt khám
            if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtMaLankham.Text) != "" && Utility.DoTrim(txtMaLankham.Text) != m_strMaluotkham)
            {
                txtNoiDKKCBBD.Clear();
                txtNoiphattheBHYT.Clear();
                isAutoFinding = true;
                string patientId = "";
                if (txtMaLankham.Text.Trim().Length < 8)
                {
                     patientId = Utility.GetYY(globalVariables.SysDate) +
                                    Utility.FormatNumberToString(Utility.Int32Dbnull(txtMaLankham.Text, 0), "000000");
                }
                else
                {
                    patientId = txtMaLankham.Text.Trim();
                }
                txtMaLankham.Text = patientId;
                ResetLuotkham();
                FindPatientIDbyMaLanKham(txtMaLankham.Text.Trim());
                isAutoFinding = false;
            }
        }

        private void FindPatientIDbyBhyt(string insuranceNum, string noidangky)
        {
            try
            {
                DataTable temdt = SPs.KcbTimkiembenhnhantheomathebhyt(insuranceNum + noidangky).GetDataSet().Tables[0];
                if (temdt.Rows.Count <= 0) return;
                if (temdt.Rows.Count == 1)
                {
                    if (!KT_20_Ky_Tu_BHYT(temdt.Rows[0]["id_benhnhan"].ToString(), insuranceNum, noidangky)) return;
                    AutoFindLastExamandFetchIntoControls(temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), insuranceNum, noidangky);
                }
                else //Show dialog for select
                {
                    DataRow[] arrDr =
                        temdt.Select(KcbLuotkham.Columns.MatheBhyt + "='" + insuranceNum + "' AND " +
                                     KcbLuotkham.Columns.NoiDongtrusoKcbbd + "= '" + noidangky.Substring(0, 2) +
                                     "' AND " + KcbLuotkham.Columns.MaKcbbd + "= '" + noidangky.Substring(2, 3) + "'");
                    if (arrDr.Length == 1)
                    {
                        if (!KT_20_Ky_Tu_BHYT(temdt.Rows[0]["id_benhnhan"].ToString(), insuranceNum, noidangky)) return;
                        AutoFindLastExamandFetchIntoControls(arrDr[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), insuranceNum, noidangky);
                    }
                    else
                    {
                        isAutobinding = false;
                        var chonBn = new frm_CHON_BENHNHAN();
                        chonBn.temdt = temdt;
                        chonBn.ShowDialog();
                        if (!chonBn.mv_bCancel)
                        {
                            if (!KT_20_Ky_Tu_BHYT(temdt.Rows[0]["id_benhnhan"].ToString(), insuranceNum, noidangky)) return;
                            AutoFindLastExamandFetchIntoControls(chonBn.Patient_ID, insuranceNum, noidangky);
                            //return;
                        }
                      
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
            }
        }

        private void FindPatientIDbyCMT(string CMT)
        {
            try
            {
                DataTable temdt = SPs.KcbTimkiembenhnhantheosocmt(CMT).GetDataSet().Tables[0];
                if (temdt.Rows.Count <= 0) return;
                if (temdt.Rows.Count == 1)
                {
                    AutoFindLastExamandFetchIntoControls(temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty,string.Empty);
                }
                else //Show dialog for select
                {
                    DataRow[] arrDr = temdt.Select(KcbDanhsachBenhnhan.Columns.Cmt+ "='" + CMT + "'");
                    if (arrDr.Length == 1)
                        AutoFindLastExamandFetchIntoControls(arrDr[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty,string.Empty);
                    else
                    {
                        var _ChonBN = new frm_CHON_BENHNHAN();
                        _ChonBN.temdt = temdt;
                        _ChonBN.ShowDialog();
                        if (!_ChonBN.mv_bCancel)
                        {
                            AutoFindLastExamandFetchIntoControls(_ChonBN.Patient_ID, string.Empty, string.Empty);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
            }
        }

        private void txtMaBN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtMaBN.Text.Trim() != "")
            {

                if (Utility.IsNumeric(txtMaBN.Text))
                {
                    txtNoiDKKCBBD.Clear();
                    txtNoiphattheBHYT.Clear();
                    isAutoFinding = true;
                    FindPatient(txtMaBN.Text.Trim());
                    isAutoFinding = false;
                }
                else
                {
                    if (Utility.DoTrim(txtMaBN.Text).ToUpper() == "TỰ SINH")
                    {
                    }
                    else
                    {
                        Utility.ShowMsg("Muốn tìm kiếm theo ID Bệnh nhân thì bạn phải nhập toàn bộ là chữ số. Mời bạn nhập lại");
                        txtMaBN.Focus();
                        txtMaBN.SelectAll();
                    }
                }
            }
        }

        private bool NotPayment(string patient_ID, ref string NgayKhamGanNhat)
        {
            try
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEPDON_BOQUATHANHTOAN", "1", false) == "1")
                {
                    return false;
                }
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
                    AutoFindLastExamandFetchIntoControls(temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty, string.Empty);
                }
                else //Show dialog for select
                {
                    DataRow[] arrDr = temdt.Select("id_benhnhan=" + patient_ID);
                    if (arrDr.Length == 1)
                        AutoFindLastExamandFetchIntoControls(arrDr[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty, string.Empty);
                    else
                    {
                        var _ChonBN = new frm_CHON_BENHNHAN();
                        _ChonBN.temdt = temdt;
                        _ChonBN.ShowDialog();
                        if (!_ChonBN.mv_bCancel)
                        {
                            AutoFindLastExamandFetchIntoControls(_ChonBN.Patient_ID, string.Empty, string.Empty);
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
                if (temdt.Rows.Count <= 0)
                {
                    ClearControl();
                    return;
                }
                if (temdt.Rows.Count == 1)
                {
                    AutoFindLastExamandFetchIntoControls(temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty, string.Empty);
                }
                else //Show dialog for select
                {
                    var _ChonBN = new frm_CHON_BENHNHAN();
                    _ChonBN.temdt = temdt;
                    _ChonBN.ShowDialog();
                    if (!_ChonBN.mv_bCancel)
                    {
                        AutoFindLastExamandFetchIntoControls(_ChonBN.Patient_ID, string.Empty, string.Empty);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
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
        private bool KT_20_Ky_Tu_BHYT(string patientId, string sobhyt, string noiDangky)
        {

            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_KIEMTRA20KYTU_BHYT", "0", false) == "1")
            {
                    SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema)
                        .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(patientId);
                    if (!string.IsNullOrEmpty(sobhyt))
                    {
                        sqlQuery.And(KcbLuotkham.Columns.MatheBhyt).IsEqualTo(sobhyt).And(KcbLuotkham.Columns.NoiDongtrusoKcbbd).IsEqualTo(noiDangky.Substring(0,2))
                            .And(KcbLuotkham.Columns.MaKcbbd).IsEqualTo(noiDangky.Substring(2,3));
                    }
                    sqlQuery.OrderDesc(KcbLuotkham.Columns.NgayTiepdon);
                    var objPatientExam = sqlQuery.ExecuteSingle<KcbLuotkham>();
                    if (objPatientExam != null)
                    {
                        txtMaBN.Text = patientId;
                        txtMaLankham.Text = Utility.sDbnull(objPatientExam.MaLuotkham);
                        if (txtNoiDongtrusoKCBBD.Text.ToUpper() !=
                            Utility.sDbnull(objPatientExam.NoiDongtrusoKcbbd).ToUpper() ||
                            txtNoiDKKCBBD.Text.ToUpper() != Utility.sDbnull(objPatientExam.MaKcbbd).ToUpper())
                        { return false; }
                            
                    }
                    return true;
                }
                else
                    return true;

        }
        private void AutoFindLastExamandFetchIntoControls(string patientId, string sobhyt, string noiDangky)
        {
            try
            {
                if (!Utility.CheckLockObject(m_strMaluotkham, "Tiếp đón", "TD"))
                    return;
                //Trả lại mã lượt khám nếu chưa được dùng đến
                SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema)
                    .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(patientId);
                if (!string.IsNullOrEmpty(sobhyt))
                {
                    sqlQuery.And(KcbLuotkham.Columns.MatheBhyt)
                        .IsEqualTo(sobhyt)
                        .And(KcbLuotkham.Columns.NoiDongtrusoKcbbd)
                        .IsEqualTo(noiDangky.Substring(0, 2))
                        .And(KcbLuotkham.Columns.MaKcbbd)
                        .IsEqualTo(noiDangky.Substring(2, 3));
                }
                sqlQuery.OrderDesc(KcbLuotkham.Columns.NgayTiepdon);

                var objLuotkham = sqlQuery.ExecuteSingle<KcbLuotkham>();
                if (objLuotkham != null)
                {
                    txtMaBN.Text = patientId;
                    txtMaLankham.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                    m_strMaluotkham = objLuotkham.MaLuotkham;
                    MEnAction = action.Update;
                    AllowTextChanged = false;
                    LoadThongtinBenhnhan();
                    CanhbaoInphoi();
                    LaydanhsachdangkyKcb();
                    string ngayKham = globalVariables.SysDate.ToString("dd/MM/yyyy");
                    if (!NotPayment(txtMaBN.Text.Trim(), ref ngayKham))//Đã thanh toán-->Kiểm tra ngày hẹn xem có được phép khám tiếp
                    {

                        KcbChandoanKetluan _Info = new Select().From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.MaLuotkhamColumn).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<KcbChandoanKetluan>();
                        if (_Info != null && _Info.SongayDieutri != null)
                        {
                            int soNgayDieuTri = 0;
                            if (_Info.SongayDieutri.Value.ToString() == "")
                            {
                                soNgayDieuTri = 0;
                            }
                            else
                            {
                                soNgayDieuTri = _Info.SongayDieutri.Value;
                            }
                            DateTime ngaykhamcu = _Info.NgayTao; ;
                            DateTime ngaykhammoi = globalVariables.SysDate;
                            TimeSpan songay = ngaykhammoi - ngaykhamcu;

                            int kt = songay.Days;
                            int kt1 = soNgayDieuTri - kt;
                            kt1 = Utility.Int32Dbnull(kt1);
                            // nếu khoảng cách từ lần khám trước đến ngày hiện tại lớn hơn ngày điều trị.
                            if (kt >= soNgayDieuTri)
                            {
                                MEnAction = action.Add;
                                SinhMaLanKham();
                                if (MEnAction == action.Insert || MEnAction == action.Add)
                                {
                                    dtCreateDate.Value = globalVariables.SysDate;
                                }
                                else
                                {
                                    dtCreateDate.Value = _objLuotkham.NgayTiepdon;
                                }
                                //txtTongChiPhiKham.Text = "0";
                                m_dtDangkyPhongkham.Rows.Clear();
                                txtKieuKham.Select();
                            }
                            else if (kt < soNgayDieuTri)
                            {
                                DialogResult dialogResult =
                                    MessageBox.Show(
                                        @"Bác Sỹ hẹn :  " + soNgayDieuTri + @"ngày" + @"\nNgày khám gần nhất:  " +
                                        ngaykhamcu + @"\nCòn: " + kt1 + @" ngày nữa mới được tái khám" +
                                        @"\nBạn có muốn tiếp tục thêm lần khám ", @"Thông Báo", MessageBoxButtons.YesNo);

                                if (dialogResult == DialogResult.Yes)
                                {
                                    MEnAction = action.Add;
                                    SinhMaLanKham();
                                    if (MEnAction == action.Insert || MEnAction == action.Add)
                                    {
                                        dtCreateDate.Value = globalVariables.SysDate;
                                    }
                                    else
                                    {
                                        dtCreateDate.Value = _objLuotkham.NgayTiepdon;
                                    }
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
                        if (ngayKham == "NOREG" || ngayKham == globalVariables.SysDate.ToString("dd/MM/yyyy"))
                        {
                            //LoadThongtinBenhnhan();
                            if (ngayKham == "NOREG")//Bn chưa đăng ký phòng khám nào cả. 
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

    
        private void cmdLayLaiThongTin_Click(object sender, EventArgs e)
        {
            LoadThongTinChoKham();
        }

        /// <summary>
        /// hàm thực hiên việc load thông tin của hơi khám
        /// </summary>
        private void LoadThongTinChoKham()
        {
            m_dtChoKham = _kcbDangky.LayDsachBnhanChoKham();
            
            Utility.SetDataSourceForDataGridEx(grdListKhoa, m_dtChoKham, false, true, "1=1", "", true);
        }
        
        // private  b_QMSStop=false;
        /// <summary>
        /// hàm thực hiện việc lấy thông tin của phần dữ liệu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_KCB_DANGKY_CAPNHAT_Load(object sender, EventArgs e)
        {
            try
            {
                txtDiachi.lblWords = lblAdd1;
                txtDiachi.lblTest = lblAdd0;
                if (this.Args.Split('-')[0] == "KTC") lblBATC.Visible = txtSoBATCQG.Visible = true;
                AllowTextChanged = false;
                b_HasLoaded = false;
                dtInsFromDate.Value = new DateTime(globalVariables.SysDate.Year, 1, 1);
                dtInsToDate.Value = new DateTime(globalVariables.SysDate.Year, 12, 31);
                Utility.SetColor(lblDiachiBHYT, THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATNHAP_DIACHI_BHYT", "1", false) == "1" ? lblHoten.ForeColor : lblMatheBHYT.ForeColor);
                Utility.SetColor(lblDiachiBN, THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATNHAP_DIACHI_BENHNHAN", "1", false) == "1" ? lblHoten.ForeColor : lblMatheBHYT.ForeColor);
                chkTraiTuyen.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_CHOPHEPTIEPDON_TRAITUYEN", "1", false) == "1";
                chkLaysokham.Enabled = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATBUOCLAY_SOKHAMCHUABENH", "0", false) == "0";
                txtQRCode.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEPDON_SUDUNG_QRCODE", "1", false) == "1";
                txtSoKcb.Enabled = chkLaysokham.Enabled;
                dtpBOD.CustomFormat = PropertyLib._KCBProperties.Nhapngaythangnamsinh ? "dd/MM/yyyy HH:mm" : "yyyy";
                txtTuoi.Enabled = dtpBOD.CustomFormat == @"yyyy";
                lblLoaituoi.Visible = dtpBOD.CustomFormat != @"yyyy";
                dtpBOD_TextChanged(dtpBOD, e);
                XoathongtinBHYT(true);
                AddAutoCompleteDiaChi();
                Get_DanhmucChung();
                AutocompleteBenhvien();
                LoadThongTinChoKham();
                NapThongtinDichvuKcb();
                DataBinding.BindDataCombobox(cboDoituongKCB,globalVariables.gv_dtDoituong, DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb,"", false);
                _objDoituongKcb =  new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(_maDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _maDoituongKcb);
                ChangeObjectRegion();
                if (MEnAction == action.Insert)//Thêm mới BN
                {
                    _objLuotkham = null;
                    if (PropertyLib._KCBProperties.SexInput)
                        cboPatientSex.SelectedIndex = -1;
                    SinhMaLanKham();
                    txtTEN_BN.Select();
                }
                else if (MEnAction == action.Update)//Cập nhật thông tin Bệnh nhân
                {
                    LoadThongtinBenhnhan();
                    LaydanhsachdangkyKcb();
                    txtTEN_BN.Select();
                }
                else if (MEnAction == action.Add) //Thêm mới lần khám
                {
                    _objLuotkham = null;
                    string ngayKham = globalVariables.SysDate.ToString("dd/MM/yyyy");
                    if (!NotPayment(txtMaBN.Text.Trim(), ref ngayKham))//Nếu đã thanh toán xong hết thì thêm lần khám mới
                    {
                        SinhMaLanKham();
                        LoadThongtinBenhnhan();
                        LaydanhsachdangkyKcb();
                        if (pnlChonphongkham.Visible)
                        {
                            txtExamtypeCode.Focus();
                            txtExamtypeCode.Select();
                        }
                        else
                        {
                            txtKieuKham.Focus();
                            txtKieuKham.Select();
                        }
                    }
                    else//Còn lần khám chưa thanh toán-->Kiểm tra
                    {
                        //nếu là ngày hiện tại thì đặt về trạng thái sửa
                        if (ngayKham == "NOREG" || ngayKham==globalVariables.SysDate.ToString("dd/MM/yyyy"))
                        {
                            LoadThongtinBenhnhan();
                            if (ngayKham == "NOREG")//Bn chưa đăng ký phòng khám nào cả. 
                            {
                                //Nếu ngày hệ thống=Ngày đăng ký gần nhất-->Sửa
                                if (globalVariables.SysDate.ToString("dd/MM/yyyy") == dtpInputDate.Value.ToString("dd/MM/yyyy"))
                                {
                                    MEnAction = action.Update;

                                    Utility.ShowMsg(
                                       "Bệnh nhân vừa được đăng ký ngày hôm nay nên hệ thống sẽ chuyển về chế độ Sửa thông tin. Nhấn OK để bắt đầu sửa");
                                    LaydanhsachdangkyKcb();
                                    txtTEN_BN.Select();
                                }
                                else//Thêm lần khám cho ngày mới
                                {
                                    MEnAction = action.Add;
                                    SinhMaLanKham();
                                    LaydanhsachdangkyKcb();
                                    txtKieuKham.Select();
                                }
                            }
                            else//Quay về trạng thái sửa
                            {
                                MEnAction = action.Update;
                                Utility.ShowMsg(
                                   "Bệnh nhân vừa được đăng ký ngày hôm nay nên hệ thống sẽ chuyển về chế độ Sửa thông tin. Nhấn OK để bắt đầu sửa");
                                LaydanhsachdangkyKcb();
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
                    //if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_NGUONGTHIEU_LAST", "1", false) == "1")
                    //    txtNguonGthieu.SetCode(objBenhnhan.LastNoigioithieu);
                    //else
                    //    txtNguonGthieu.SetCode(objBenhnhan.FirstNoigioithieu);
                }
                StatusControl();
                ModifyCommand();
                AllowTextChanged = true;
            }
            catch (Exception ex)
            {
                log.Trace("Loi: " + ex);
            }
            finally
            {
                if (PropertyLib._ConfigProperties.HIS_AppMode != VNS.Libs.AppType.AppEnum.AppMode.License)
                    this.Text = @"Đăng ký KCB";
                SetActionStatus(); 
                if (Nhieuhon2Manhinh())
                {
                    pThongTinQMS.Enabled = true;
                    b_HasSecondScreen = true;
                    ShowScreen();
                }
                else
                {
                    b_HasSecondScreen = false;
                    pThongTinQMS.Enabled = false;
                }

                ModifyCommand();
                ModifyButtonCommandRegExam();

                b_HasLoaded = true;
                CanhbaoInphoi();
                
            }
            
            

        }
        void CanhbaoInphoi()
        {
            try
            {
                int patient_ID = Utility.Int32Dbnull(txtMaBN.Text, -1);
                if (patient_ID <= 0) return;
                DmucCanhbaoCollection lst =
                    new Select().From(DmucCanhbao.Schema).Where(DmucCanhbao.MaBnColumn).IsEqualTo(patient_ID).
                        ExecuteAsCollection<DmucCanhbaoCollection>();
                if (lst.Count > 0)//Delete
                {
                    if (lst[0].CanhBao.TrimStart().TrimEnd() != "")
                        Utility.ShowMsg(lst[0].CanhBao,"Thông tin cảnh báo dành cho Bệnh nhân");
                }
            }
            catch(Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ex.Message);
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
        byte _idLoaidoituongKcb = 1;
        Int16 _idDoituongKcb = 1;
        string _maDoituongKcb = "DV";
        string _tenDoituongKcb = "Dịch vụ";
        decimal _ptramBhytCu = 0m;
        decimal _ptramBhytGocCu = 0m;
        KcbLuotkham _objLuotkham = null;
        KcbDanhsachBenhnhan objBenhnhan = null;

        private void LoadLichSuKham(Int64 id_benhnhan)
        {
            grdLichSu.DataSource = null;
            DataTable dtLichSuKham = SPs.KcbTiepDonThongTinLichSuKham(id_benhnhan).GetDataSet().Tables[0];
                //new Select(KcbLuotkham.Columns.NgayTiepdon, KcbLuotkham.Columns.MaLuotkham).From(KcbLuotkham.Schema)
                //    .Where(KcbLuotkham.Columns.IdBenhnhan)
                //    .IsEqualTo(id_benhnhan)
                //    .ExecuteDataSet()
                //    .Tables[0];
            if (dtLichSuKham.Rows.Count > 0)
            {
                Utility.SetDataSourceForDataGridEx(grdLichSu,dtLichSuKham,false,true,"","");
            }
        }
        private void LoadThongtinBenhnhan()
        {
            _ptramBhytCu = 0m;
            _ptramBhytGocCu = 0m;
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
                else dtpBOD.Value = new DateTime((int)objBenhnhan.NamSinh, 1, 1);
               
                txtNgheNghiep._Text = Utility.sDbnull(objBenhnhan.NgheNghiep);
                cboPatientSex.SelectedIndex = Utility.GetSelectedIndex(cboPatientSex, Utility.sDbnull(objBenhnhan.IdGioitinh));
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
                        txtDantoc._Text = Utility.sDbnull(objdantoc["TEN"],"Kinh");
                    }
                }
                 
                
                txtEmail.Text = Utility.sDbnull(objBenhnhan.Email);
                txtCMT.Text = Utility.sDbnull(objBenhnhan.Cmt);
                _objLuotkham = new Select().From(KcbLuotkham.Schema)
                   .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(txtMaLankham.Text.Trim(),""))
                   .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtMaBN.Text.Trim(),-1)).ExecuteSingle <KcbLuotkham>();
                if (_objLuotkham != null)
                {
                    LoadLichSuKham(_objLuotkham.IdBenhnhan);
                    KcbDangkySokham objSoKCB=new Select().From(KcbDangkySokham.Schema)
                        .Where(KcbDangkySokham.Columns.IdBenhnhan).IsEqualTo(_objLuotkham.IdBenhnhan)
                        .And(KcbDangkySokham.Columns.MaLuotkham).IsEqualTo(_objLuotkham.MaLuotkham)
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
                    txtSoBA.Text = Utility.sDbnull(_objLuotkham.SoBenhAn,"-1");
                    m_strMaluotkham = _objLuotkham.MaLuotkham;
                    txtLoaikham.SetCode(_objLuotkham.KieuKham);
                    txtSolankham.Text = Utility.sDbnull(_objLuotkham.SolanKham);
                    txtTtinNguonGt.Text = _objLuotkham.ThongtinNguongt;
                    _idDoituongKcb = _objLuotkham.IdDoituongKcb;
                    dtpInputDate.Value = _objLuotkham.NgayTiepdon;
                    if (MEnAction == action.Insert || MEnAction == action.Add)
                    {
                        dtCreateDate.Value = globalVariables.SysDate;
                    }
                    else
                    {
                        dtCreateDate.Value = _objLuotkham.NgayTiepdon;
                    }
                    chkCapCuu.Checked = Utility.Int32Dbnull(_objLuotkham.TrangthaiCapcuu, 0) == 1;
                    chkTraiTuyen.Checked = Utility.Int32Dbnull(_objLuotkham.DungTuyen, 0) == 0;
                    lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
                    txtEmail.Text = _objLuotkham.Email;
                    txtNguoiLienhe.Text = objBenhnhan.NguoiLienhe;

                    txtchandoantuyenduoi.Text = _objLuotkham.ChandoanChuyenden;
                    if (Utility.DoTrim(_objLuotkham.NoiGioithieu).Length > 0)
                        txtNguonGthieu.SetCode(_objLuotkham.NoiGioithieu);
                    txtLoaiBN.SetCode(_objLuotkham.NhomBenhnhan);
                    if (PropertyLib._KCBProperties.Nhapngaythangnamsinh)
                    {
                        dtpBOD.CustomFormat = @"dd/MM/yyyy HH:mm";
                        lblLoaituoi.Visible = true;
                    }
                    else
                    {
                        if (_objLuotkham.LoaiTuoi > 0)
                        {
                            dtpBOD.CustomFormat = @"dd/MM/yyyy HH:mm";
                            lblLoaituoi.Visible = true;
                        }
                        else
                        {
                            dtpBOD.CustomFormat = @"yyyy";
                            lblLoaituoi.Visible = false;
                        }
                    }
                  
                    if (dtpBOD.CustomFormat != @"yyyy")
                    {
                        txtTuoi.Text = Utility.sDbnull(_objLuotkham.Tuoi, "0");
                        UIAction.SetText(lblLoaituoi, _objLuotkham.LoaiTuoi == 0 ? "" : (_objLuotkham.LoaiTuoi == 1 ? "Tháng" : "Tuần"));
                    }
                    else
                    {
                        txtTuoi.Text = Utility.sDbnull(globalVariables.SysDate.Year - Utility.Int32Dbnull(objBenhnhan.NamSinh, 0));
                    }
                    _allowAgeChanged = true;
                    _maDoituongKcb = Utility.sDbnull(_objLuotkham.MaDoituongKcb);
                    _objDoituongKcb = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(
                            _maDoituongKcb).ExecuteSingle<DmucDoituongkcb>();

                    ChangeObjectRegion();
                    _ptramBhytCu = Utility.DecimaltoDbnull(_objLuotkham.PtramBhyt, 0);
                    _ptramBhytGocCu = Utility.DecimaltoDbnull(_objLuotkham.PtramBhytGoc, 0);
                    _idDoituongKcb = _objDoituongKcb.IdDoituongKcb;
                    _tenDoituongKcb = _objDoituongKcb.TenDoituongKcb;
                    cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _maDoituongKcb);
                    chkChuyenVien.Checked = Utility.Int32Dbnull(_objLuotkham.TthaiChuyenden, 0) == 1;
                    txtNoichuyenden.SetId(Utility.Int32Dbnull(_objLuotkham.IdBenhvienDen, -1));
                    if (!string.IsNullOrEmpty(_objLuotkham.MatheBhyt))//Thông tin BHYT
                    {
                        txtTrieuChungBD._Text = Utility.sDbnull(_objLuotkham.TrieuChung);
                        if (!string.IsNullOrEmpty(Utility.sDbnull(_objLuotkham.NgaybatdauBhyt)))
                            dtInsFromDate.Value = Convert.ToDateTime(_objLuotkham.NgaybatdauBhyt);
                        if (!string.IsNullOrEmpty(Utility.sDbnull(_objLuotkham.NgayketthucBhyt)))
                            dtInsToDate.Value = Convert.ToDateTime(_objLuotkham.NgayketthucBhyt);
                        txtPtramBHYT.Text = Utility.sDbnull(_objLuotkham.PtramBhyt, "0");
                        txtptramDauthe.Text = Utility.sDbnull(_objLuotkham.PtramBhytGoc, "0");
                        //HS7010340000005
                        txtMaDtuong_BHYT.Text = Utility.sDbnull(_objLuotkham.MaDoituongBhyt);

                        txtMaQuyenloi_BHYT.Text = Utility.sDbnull(_objLuotkham.MaQuyenloi);
                        txtNoiDongtrusoKCBBD.Text = Utility.sDbnull(_objLuotkham.NoiDongtrusoKcbbd);
                        txtOthu4.Text = Utility.sDbnull(_objLuotkham.MatheBhyt).Substring(5, 2);
                      //  txtOthu4.TextChanged -= new EventHandler(txtOthu4_TextChanged);
                        txtOthu5.Text = Utility.sDbnull(_objLuotkham.MatheBhyt).Substring(7, 3);
                       // txtOthu5.TextChanged -= new EventHandler(txtOthu5_TextChanged);
                        txtOthu6.Text = Utility.sDbnull(_objLuotkham.MatheBhyt).Substring(10, 5);
                        //txtOthu6.TextChanged -= new EventHandler(txtOthu6_TextChanged);
                        txtDiachi_bhyt._Text = Utility.sDbnull(objBenhnhan.DiachiBhyt);
                        txtMaDTsinhsong.SetCode(_objLuotkham.MadtuongSinhsong);
                        chkGiayBHYT.Checked = Utility.Byte2Bool(_objLuotkham.GiayBhyt);
                        if (!string.IsNullOrEmpty(Utility.sDbnull(_objLuotkham.NgayDu5nam)))
                            dtpNgaydu5nam.Value = Convert.ToDateTime(_objLuotkham.NgayDu5nam);
                        txtNoiphattheBHYT.Text = Utility.sDbnull(_objLuotkham.MaNoicapBhyt);
                        txtNoiDKKCBBD.Text = Utility.sDbnull(_objLuotkham.MaKcbbd);
                        pnlBHYT.Enabled = true;
                    }
                    else
                    {
                        XoathongtinBHYT(true);
                    }
                }
            }
            chkChuyenVien_CheckedChanged(chkChuyenVien, new EventArgs());
        }

        void XoathongtinBHYT(bool forcetodel)
        {
            if (forcetodel)
            {
                _idDoituongKcb = 1;
                _maDoituongKcb = "DV";
                _tenDoituongKcb = "Dịch vụ";
                dtInsFromDate.Value = new DateTime(globalVariables.SysDate.Year, 1, 1);
                dtInsToDate.Value = new DateTime(globalVariables.SysDate.Year, 12, 31);
                txtPtramBHYT.Text = "";
                txtptramDauthe.Text = "";
                lblNoiCapThe.Text = "";
                txtNoiCapThe.Text = "";
                txtClinicName.Text = "";
                lblClinicName.Text = "";
                txtMaDtuong_BHYT.Clear();
                txtMaDTsinhsong.ResetText();
                chkGiayBHYT.Checked = false;
                txtMaQuyenloi_BHYT.Clear();
                txtNoiDongtrusoKCBBD.Clear();
                txtOthu4.Clear();
                txtOthu5.Clear();
                txtOthu6.Clear();
                chkTraiTuyen.Checked = false;
                lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
                txtNoiphattheBHYT.Clear();
                txtDiachi_bhyt.Clear();
                txtNoiDKKCBBD.Clear();
                //pnlBHYT.Enabled = false;
            }            
        } 
        private void Get_DanhmucChung()
        {
           
            AutoCompleteDmucChung();
            AutocompleteDautheBHYT();
        }
        private void AddAutoCompleteDiaChi()
        {
            txtDiachi_bhyt.dtData = globalVariables.dtAutocompleteAddress;
            txtDiachi.dtData = globalVariables.dtAutocompleteAddress.Copy();
            this.txtDiachi_bhyt.AutoCompleteList = globalVariables.LstAutocompleteAddressSource;
            this.txtDiachi_bhyt.CaseSensitive = false;
            this.txtDiachi_bhyt.MinTypedCharacters = 1;
            this.txtDiachi.AutoCompleteList = globalVariables.LstAutocompleteAddressSource;
            this.txtDiachi.CaseSensitive = false;
            this.txtDiachi.MinTypedCharacters = 1;
        }
        private void AutocompleteDautheBHYT()
        {
            try
            {
                return;
                DataTable dt_dataDoituongBHYT = new Select().From(DmucDoituongbhyt.Schema).ExecuteDataSet().Tables[0];
                if (!dt_dataDoituongBHYT.Columns.Contains("ShortCut")) dt_dataDoituongBHYT.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in dt_dataDoituongBHYT.Rows)
                {
                    string shortcut = "";
                    string realName = dr[DmucDoituongbhyt.TenDoituongbhytColumn.ColumnName].ToString().Trim() + " " + Utility.Bodau(dr[DmucDoituongbhyt.TenDoituongbhytColumn.ColumnName].ToString().Trim());
                    shortcut = dr[DmucDoituongbhyt.MaDoituongbhytColumn.ColumnName].ToString().Trim();
                    string[] arrWords = realName.ToLower().Split(' ');
                    string _space = "";
                    string _Nospace = "";
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                        {
                            _space += word + " ";
                            //_Nospace += word;
                        }
                    }
                    shortcut += _space; // +_Nospace;
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                            shortcut += word.Substring(0, 1);
                    }
                    dr["ShortCut"] = shortcut;
                }

                var source = new List<string>();
                var query = from p in dt_dataDoituongBHYT.AsEnumerable()
                            select p[DmucDoituongbhyt.IdDoituongbhytColumn.ColumnName].ToString()+"#" + p[DmucDoituongbhyt.MaDoituongbhytColumn.ColumnName].ToString() + "@" +p[DmucDoituongbhyt.MaDoituongbhytColumn.ColumnName].ToString()+"-"+ p[DmucDoituongbhyt.TenDoituongbhytColumn.ColumnName].ToString() + "@" + p.Field<string>("shortcut").ToString();

                source = query.ToList<string>();
                this.txtMaDtuong_BHYT2.AutoCompleteList = source;
                this.txtMaDtuong_BHYT2.TextAlign = HorizontalAlignment.Center;
                this.txtMaDtuong_BHYT2.CaseSensitive = false;
                this.txtMaDtuong_BHYT2.MinTypedCharacters = 1;
                
              
            }
            catch
            {
            }
            finally
            {


            }
        }

        private void AutoCompleteDmucChung()
        {
            txtLoaikham.Init(this.Args.Split('-')[0]);
            txtMaDTsinhsong.Init();
            txtDantoc.Init();
            txtNgheNghiep.Init();
            txtTrieuChungBD.Init();
            txtLoaiBN.Init();
            txtSoKcb.Init();
            txtNguonGthieu.Init();
        }

        private void AutocompletePhongKham()
        {
            if (m_PhongKham == null) return;
            if (!m_PhongKham.Columns.Contains("ShortCut"))
                m_PhongKham.Columns.Add(new DataColumn("ShortCut", typeof(string)));
            txtPhongkham.Init(m_PhongKham, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
        }
        private void AutocompleteMaDvu()
        {
            DataRow[] arrDr = null;
            if (m_dtDanhsachDichvuKCB == null) return;
            if (!m_dtDanhsachDichvuKCB.Columns.Contains("ShortCut"))
                m_dtDanhsachDichvuKCB.Columns.Add(new DataColumn("ShortCut", typeof(string)));
            arrDr = m_dtDanhsachDichvuKCB.Select(DmucDoituongkcb.Columns.MaDoituongKcb + "='ALL' OR " + DmucDoituongkcb.Columns.MaDoituongKcb + "='" + MA_DTUONG + "'");
            if (arrDr.Length <= 0)
            {
                this.txtExamtypeCode.AutoCompleteList = new List<string>();
                return;
            }
            txtExamtypeCode.Init(m_dtDanhsachDichvuKCB, new List<string>() { DmucDichvukcb.Columns.IdDichvukcb, DmucDichvukcb.Columns.MaDichvukcb, DmucDichvukcb.Columns.TenDichvukcb });
        }

        private void AutocompleteBenhvien()
        {
            //DataTable m_dtBenhvien = new Select().From(DmucBenhvien.Schema).ExecuteDataSet().Tables[0];
            DataTable m_dtBenhvien = globalVariables.gv_dtDmucBenhVien;
            try
            {
                if (m_dtBenhvien == null) return;
                if (!m_dtBenhvien.Columns.Contains("ShortCut"))
                    m_dtBenhvien.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                txtNoichuyenden.Init(m_dtBenhvien, new List<string>() { DmucBenhvien.Columns.IdBenhvien, DmucBenhvien.Columns.MaBenhvien, DmucBenhvien.Columns.TenBenhvien });
                
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void AutocompleteKieuKham()
        {
            try
            {
                if (m_kieuKham == null) return;
                if (!m_kieuKham.Columns.Contains("ShortCut"))
                    m_kieuKham.Columns.Add(new DataColumn("ShortCut", typeof (string)));
                    txtKieuKham.Init(m_kieuKham, new List<string>() { DmucKieukham.Columns.IdKieukham, DmucKieukham.Columns.MaKieukham, DmucKieukham.Columns.TenKieukham });

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private string BoTp_old(string value)
        {
            string reval = "";
            try
            {
                string[] arrWords = value.Split(' ');
                foreach (string word in arrWords)
                {
                    if (word.Trim() != "")
                    {
                        if (!globalVariables.gv_strBOTENDIACHINH.ToUpper().Contains("," + word.Trim().ToUpper() + ","))
                            reval += word + " ";
                    }
                }
                return reval.Trim();
            }
            catch
            {
                return value;
            }
        }

        private string BoTp(int Type, string value)
        {
            string reval = "";
            try
            {
                if (value.ToUpper().Trim() == "KHÔNG XÁC ĐỊNH") return "Không xác";
                string TTP = "THỊ TRẤN,P.,TP,TỈNH,THÀNH PHỐ,";
                string QH = "THỊ TRẤN,P.,QUẬN,HUYỆN,THÀNH PHỐ,TP,THỊ XÃ, XÃ,TX,H.,";
                string XP = "THỊ TRẤN,P.,XÃ,PHƯỜNG,THỊ XÃ,TX,H.,";
                string[] arrWords = value.Split(' ');

                foreach (string word in arrWords)
                {
                    if (word.Trim() != "")
                    {
                        if (Type == 0) //Bỏ Tỉnh, Thành Phố,Tp
                            if (!TTP.Contains("," + word.Trim().ToUpper() + ","))
                                reval += word + " ";
                        if (Type == 1) //Bỏ Tỉnh, Thành Phố,Tp
                            if (!QH.Contains("," + word.Trim().ToUpper() + ","))
                                reval += word + " ";
                        if (Type == 2) //Bỏ Tỉnh, Thành Phố,Tp
                        {
                            if (!XP.Contains("," + word.Trim().ToUpper() + ","))
                                reval += word + " ";
                        }
                    }
                }
                return reval.Trim();
            }
            catch
            {
                return value;
            }
        }

        private string getshortcut(string _value)
        {
            string[] arrWords = _value.ToLower().Split(' ');
            string reval = "";
            foreach (string word in arrWords)
            {
                if (word.Trim() != "" && reval.Trim().Length < 2)
                    reval += word.Substring(0, 1);
            }
            return reval;
        }

        private void AutoUpdate(DataTable m_dtDataThanhPho)
        {
            try
            {
                foreach (DataRow dr in m_dtDataThanhPho.Rows)
                {
                    //if (dr[DmucDiachinh.Columns.LoaiDiachinh].ToString().Trim() == "2")
                    //{
                    string[] arrname = dr[DmucDiachinh.Columns.TenDiachinh].ToString().Trim().Split(',');
                    string name = arrname[0];
                    string name1 = name;
                    string TTP = "THỊ TRẤN,P.,TP,TỈNH,THÀNH PHỐ,";
                    string QH = "THỊ TRẤN,P.,QUẬN,HUYỆN,THÀNH PHỐ,TP,THỊ XÃ, XÃ,TX,H.,";
                    string XP = "THỊ TRẤN,P.,XÃ,PHƯỜNG,THỊ XÃ,TX,H.,";
                    if (dr[DmucDiachinh.Columns.LoaiDiachinh].ToString().Trim() == "0")
                        name =
                            Utility.CapitalizeWords(name.ToUpper().Replace("THÀNH PHỐ", "").Replace("TỈNH", "").Replace("TP", ""));
                    if (dr[DmucDiachinh.Columns.LoaiDiachinh].ToString().Trim() == "2")
                        name =
                            Utility.CapitalizeWords(
                                name.ToUpper().Replace("QUẬN", "").Replace("HUYỆN", "").Replace("PHƯỜNG", "").Replace(
                                    "XÃ", "").Replace("THÀNH PHỐ", "").Replace("TỈNH", "").Replace("THỊ XÃ", "").Replace
                                    ("TX", "").Replace("H.", "").Replace("THỊ TRẤN", "").Replace("P.", ""));
                    if (dr[DmucDiachinh.Columns.LoaiDiachinh].ToString().Trim() == "1")
                        name =
                            Utility.CapitalizeWords(
                                name.ToUpper().Replace("QUẬN", "").Replace("HUYỆN", "").Replace("PHƯỜNG", "").Replace(
                                    "XÃ", "").Replace("THÀNH PHỐ", "").Replace("TỈNH", "").Replace("THỊ XÃ", "").Replace
                                    ("TX", "").Replace("H.", "").Replace("THỊ TRẤN", "").Replace("P.", ""));
                    string viettat = getshortcut(Utility.Bodau(BoTp(Convert.ToInt32(dr[DmucDiachinh.Columns.LoaiDiachinh]), name)));
                    QueryCommand cmd = DmucDiachinh.CreateQuery().BuildCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandSql = "Update dmuc_diachinh set ten_diachinh=N'" + name1 + "',mota_them='" + viettat +
                                     "' WHERe ma_diachinh='" + dr[DmucDiachinh.Columns.MaDiachinh] + "'";
                    DataService.ExecuteQuery(cmd);
                    //}
                }
            }
            catch
            {
            }
        }
        private void CreateTable()
        {
            if (m_DC == null || m_DC.Columns.Count <= 0)
            {
                m_DC = new DataTable();
                m_DC.Columns.AddRange(new[]
                                          {
                                              new DataColumn("ShortCutXP", typeof (string)),
                                              new DataColumn("ShortCutQH", typeof (string)),
                                              new DataColumn("ShortCutTP", typeof (string)),
                                              new DataColumn("Value", typeof (string)),
                                              new DataColumn("ComparedValue", typeof (string))
                                          });
            }
        }
        private void AddShortCut_DC()
        {
            //try
            //{
            //    CreateTable();
            //    if (m_DC == null) return;
            //    if (!m_DC.Columns.Contains("ShortCut")) m_DC.Columns.Add(new DataColumn("ShortCut", typeof(string)));
            //    foreach (DataRow dr in m_dtDataThanhPho.Select("loai_diachinh=0"))
            //    {
            //        DataRow drShortcut = m_DC.NewRow();
            //        string _Value = "";
            //        string _ComparedValue = "";
            //        string realName = "";

            //        DataRow[] arrQuanHuyen =
            //            m_dtDataThanhPho.Select("ma_cha='" + Utility.sDbnull(dr[DmucDiachinh.Columns.MaDiachinh], "") + "'");
            //        foreach (DataRow drQH in arrQuanHuyen)
            //        {
            //            DataRow[] arrXaPhuong =
            //                m_dtDataThanhPho.Select("ma_cha='" + Utility.sDbnull(drQH[DmucDiachinh.Columns.MaDiachinh], "") + "'");
            //            foreach (DataRow drXP in arrXaPhuong)
            //            {
            //                drShortcut = m_DC.NewRow();
            //                realName = "";
            //                _Value = drXP[DmucDiachinh.Columns.TenDiachinh] + ", ";
            //                _ComparedValue = drXP[DmucDiachinh.Columns.TenDiachinh] + ", ";
            //                _ComparedValue += Utility.Bodau(Utility.sDbnull(drXP[DmucDiachinh.Columns.TenDiachinh], "")) + ", ";
            //                drShortcut["ShortCutXP"] = drXP["mota_them"].ToString().Trim();

            //                #region addShortcut

            //                _Value += drQH[DmucDiachinh.Columns.TenDiachinh] + ", ";
            //                _ComparedValue = drQH[DmucDiachinh.Columns.TenDiachinh] + ", ";
            //                _ComparedValue += Utility.Bodau(Utility.sDbnull(drQH[DmucDiachinh.Columns.TenDiachinh], "")) + ", ";

            //                drShortcut["ShortCutQH"] = drQH["mota_them"].ToString().Trim();

            //                _Value += dr[DmucDiachinh.Columns.TenDiachinh].ToString();
            //                _ComparedValue = dr[DmucDiachinh.Columns.TenDiachinh] + ", ";
            //                _ComparedValue += Utility.Bodau(Utility.sDbnull(dr[DmucDiachinh.Columns.TenDiachinh], "")) + ", ";
            //                drShortcut["ShortCutTP"] = dr["mota_them"].ToString().Trim();
            //                //Ghép chuỗi

            //                drShortcut["ComparedValue"] = _ComparedValue;
            //                drShortcut["Value"] = _Value;
            //                m_DC.Rows.Add(drShortcut);

            //                #endregion
            //            }

            //            if (arrXaPhuong.Length <= 0)
            //            {
            //                #region addShortcut

            //                drShortcut = m_DC.NewRow();
            //                realName = "";
            //                drShortcut["ShortCutXP"] = "kx";
            //                _Value = drQH[DmucDiachinh.Columns.TenDiachinh] + ", ";
            //                _ComparedValue = drQH[DmucDiachinh.Columns.TenDiachinh] + ", ";
            //                _ComparedValue += Utility.Bodau(Utility.sDbnull(drQH[DmucDiachinh.Columns.TenDiachinh], "")) + ", ";
            //                drShortcut["ShortCutQH"] = drQH["mota_them"].ToString().Trim();

            //                _Value += dr[DmucDiachinh.Columns.TenDiachinh].ToString();
            //                _ComparedValue = dr[DmucDiachinh.Columns.TenDiachinh] + ", ";
            //                _ComparedValue += Utility.Bodau(Utility.sDbnull(dr[DmucDiachinh.Columns.TenDiachinh], "")) + ", ";
            //                drShortcut["ShortCutTP"] = dr["mota_them"].ToString().Trim();

            //                drShortcut["ComparedValue"] = _ComparedValue;
            //                drShortcut["Value"] = _Value;
            //                m_DC.Rows.Add(drShortcut);

            //                #endregion
            //            }
            //        }
            //        if (arrQuanHuyen.Length <= 0)
            //        {
            //            #region addShortcut

            //            drShortcut = m_DC.NewRow();
            //            realName = "";
            //            drShortcut["ShortCutXP"] = "kx";
            //            drShortcut["ShortCutQH"] = "kx";
            //            drShortcut["ShortCutTP"] = dr["mota_them"].ToString().Trim();
            //            _Value = dr[DmucDiachinh.Columns.TenDiachinh].ToString();
            //            _ComparedValue = dr[DmucDiachinh.Columns.TenDiachinh] + ", ";

            //            drShortcut["ComparedValue"] = _ComparedValue;
            //            drShortcut["Value"] = _Value;
            //            m_DC.Rows.Add(drShortcut);

            //            #endregion
            //        }
            //    }
            //}
            //catch
            //{
            //}
            //finally
            //{
            //    var source = new List<string>();
            //    var query = from p in m_DC.AsEnumerable()
            //                select p.Field<string>("ShortCutTP").ToString() + "#" + p.Field<string>("ShortCutQH").ToString() + "#" + p.Field<string>("ShortCutXP").ToString() + "@" + p.Field<string>("Value").ToString() + "@" + p.Field<string>("Value").ToString();
            //    source = query.ToList();
            //    txtDiachi_bhyt.dtData = m_DC;
            //    txtDiachi.dtData = m_DC.Copy();
            //    this.txtDiachi_bhyt.AutoCompleteList = source;
            //    this.txtDiachi_bhyt.CaseSensitive = false;
            //    this.txtDiachi_bhyt.MinTypedCharacters = 1;

            //    this.txtDiachi.AutoCompleteList = source;
            //    this.txtDiachi.CaseSensitive = false;
            //    this.txtDiachi.MinTypedCharacters = 1;
            //}
        }
        private void AddShortCut_DC_old()
        {
            //try
            //{
            //    DataTable m_dtDataThanhPho = THU_VIEN_CHUNG.LayDmucDiachinh();
            //    //AutoUpdate(m_dtDataThanhPho);
            //    //AutoAdd_Khong_xac_dinh();
            //    CreateTable();
            //    if (m_DC == null) return;
            //    if (!m_DC.Columns.Contains("ShortCut")) m_DC.Columns.Add(new DataColumn("ShortCut", typeof(string)));
            //    foreach (DataRow dr in m_dtDataThanhPho.Select("loai_diachinh=0"))
            //    {
            //        DataRow drShortcut = m_DC.NewRow();
            //        string _Value = "";
            //        string realName = "";

            //        DataRow[] arrQuanHuyen =
            //            m_dtDataThanhPho.Select("ma_cha='" + Utility.sDbnull(dr[DmucDiachinh.Columns.MaDiachinh], "") + "'");
            //        foreach (DataRow drQH in arrQuanHuyen)
            //        {
            //            DataRow[] arrXaPhuong =
            //                m_dtDataThanhPho.Select("ma_cha='" + Utility.sDbnull(drQH[DmucDiachinh.Columns.MaDiachinh], "") + "'");
            //            foreach (DataRow drXP in arrXaPhuong)
            //            {
            //                drShortcut = m_DC.NewRow();
            //                realName = "";
            //                _Value = drXP[DmucDiachinh.Columns.TenDiachinh] + ", ";

            //                drShortcut["ShortCutXP"] = drXP["mota_them"].ToString().Trim();

            //                #region addShortcut

            //                _Value += drQH[DmucDiachinh.Columns.TenDiachinh] + ", ";
            //                drShortcut["ShortCutQH"] = drQH["mota_them"].ToString().Trim();

            //                _Value += dr[DmucDiachinh.Columns.TenDiachinh].ToString();
            //                drShortcut["ShortCutTP"] = dr["mota_them"].ToString().Trim();
            //                //Ghép chuỗi

            //                drShortcut["Value"] = _Value;
            //                m_DC.Rows.Add(drShortcut);

            //                #endregion
            //            }

            //            if (arrXaPhuong.Length <= 0)
            //            {
            //                #region addShortcut

            //                drShortcut = m_DC.NewRow();
            //                realName = "";
            //                drShortcut["ShortCutXP"] = "kx";
            //                _Value = drQH[DmucDiachinh.Columns.TenDiachinh] + ", ";
            //                drShortcut["ShortCutQH"] = drQH["mota_them"].ToString().Trim();

            //                _Value += dr[DmucDiachinh.Columns.TenDiachinh].ToString();
            //                drShortcut["ShortCutTP"] = dr["mota_them"].ToString().Trim();

            //                drShortcut["Value"] = _Value;
            //                m_DC.Rows.Add(drShortcut);

            //                #endregion
            //            }
            //        }
            //        if (arrQuanHuyen.Length <= 0)
            //        {
            //            #region addShortcut

            //            drShortcut = m_DC.NewRow();
            //            realName = "";
            //            drShortcut["ShortCutXP"] = "kx";
            //            drShortcut["ShortCutQH"] = "kx";
            //            drShortcut["ShortCutTP"] = dr["mota_them"].ToString().Trim();
            //            _Value = dr[DmucDiachinh.Columns.TenDiachinh].ToString();
            //            drShortcut["Value"] = _Value;
            //            m_DC.Rows.Add(drShortcut);

            //            #endregion
            //        }
            //    }
            //}
            //catch
            //{
            //}
            //finally
            //{
            //    var source = new List<string>();
            //    var query = from p in m_DC.AsEnumerable()
            //                select p.Field<string>("ShortCutTP").ToString() + "#" +p.Field<string>("ShortCutQH").ToString() + "#"+p.Field<string>("ShortCutXP").ToString() + "@"+ p.Field<string>("Value").ToString() + "@" + p.Field<string>("Value").ToString();
            //    source = query.ToList();
            //    txtDiachi_bhyt.dtData = m_DC;
            //    txtDiachi.dtData = m_DC.Copy();
            //    this.txtDiachi_bhyt.AutoCompleteList = source;
            //    this.txtDiachi_bhyt.CaseSensitive = false;
            //    this.txtDiachi_bhyt.MinTypedCharacters = 1;

            //    this.txtDiachi.AutoCompleteList = source;
            //    this.txtDiachi.CaseSensitive = false;
            //    this.txtDiachi.MinTypedCharacters = 1;
            //}
        }

        //private void cboThanhPho_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        try
        //        {
        //            if (b_HasLoaded)
        //            {
        //                mdt_DataQuyenhuyen = THU_VIEN_CHUNG.LayThongTinQuanHuyen(Utility.sDbnull(cboThanhPho.SelectedValue, ""));
        //                DataBinding.BindDataCombox(cboQuanHuyen, mdt_DataQuyenhuyen, DmucDiachinh.Columns.MaDiachinh, DmucDiachinh.Columns.TenDiachinh, "---Chọn quận huyện---");
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        private void pnlBHYT_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// hàm thực hiện việc đánh nhanh thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaDtuong_BHYT_TextChanged(object sender, EventArgs e)
        {
            if (_maDoituongKcb == "DV") return;
            if (txtMaDtuong_BHYT.Text.Length < 2) return;
            if (!IsValidTheBhyt()) return;
            TinhPtramBhyt();
            txtMaQuyenloi_BHYT.Focus();
            txtMaQuyenloi_BHYT.SelectAll();
        }

        private bool IsValidTheBhyt()
        {
            if (!string.IsNullOrEmpty(txtMaDtuong_BHYT.Text))
            {
                SqlQuery sqlQuery = new Select().From(DmucDoituongbhyt.Schema)
                    .Where(DmucDoituongbhyt.Columns.MaDoituongbhyt).IsEqualTo(txtMaDtuong_BHYT.Text);
                if (sqlQuery.GetRecordCount() <= 0)
                {
                    Utility.ShowMsg(
                        "Mã đối tượng BHYT không tồn tại trong hệ thống. Mời bạn kiểm tra lại",
                        "Thông báo", MessageBoxIcon.Information);
                    txtMaDtuong_BHYT.Focus();
                    txtMaDtuong_BHYT.SelectAll();
                    return false;
                }
            }
            if (Utility.DoTrim(txtMaDtuong_BHYT.Text) != "" && Utility.DoTrim(txtMaQuyenloi_BHYT.Text) != "")
            {
                QheDautheQloiBhyt objQheDautheQloiBhyt = new Select().From(QheDautheQloiBhyt.Schema).Where(QheDautheQloiBhyt.Columns.MaDoituongbhyt).IsEqualTo(Utility.DoTrim(txtMaDtuong_BHYT.Text))
                    .And(QheDautheQloiBhyt.Columns.MaQloi).IsEqualTo(Utility.Int32Dbnull(txtMaQuyenloi_BHYT.Text, 0)).ExecuteSingle<QheDautheQloiBhyt>();
                if (objQheDautheQloiBhyt == null)
                {
                    Utility.ShowMsg(string.Format("Đầu thẻ BHYT: {0} chưa được cấu hình gắn với mã quyền lợi: {1}. Mời bạn kiểm tra lại", txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text));
                    txtMaQuyenloi_BHYT.Focus();
                    txtMaQuyenloi_BHYT.SelectAll();
                    return false;
                }
            }
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_KIEMTRAMATHE","1",true) == "1")
            {
                if (!string.IsNullOrEmpty(txtMaQuyenloi_BHYT.Text))
                {
                    if (Utility.Int32Dbnull(txtMaQuyenloi_BHYT.Text, 0) < 1 || Utility.Int32Dbnull(txtMaQuyenloi_BHYT.Text, 0) > 9)
                    {
                        Utility.ShowMsg("Số thứ tự 2 của mã bảo hiểm nằm trong khoảng từ 1->9", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtMaQuyenloi_BHYT.Focus();
                        txtMaQuyenloi_BHYT.SelectAll();
                        return false;
                    }
                    var lstqhe =
                        new Select().From(QheDautheQloiBhyt.Schema)
                            .Where(QheDautheQloiBhyt.Columns.MaDoituongbhyt)
                            .IsEqualTo(txtMaDtuong_BHYT.Text)
                            .ExecuteAsCollection<QheDautheQloiBhytCollection>();
                    if (lstqhe.Count > 0)
                    {
                        var q = from p in lstqhe
                                where p.MaQloi == Utility.ByteDbnull(txtMaQuyenloi_BHYT.Text, -1)
                                select _objDoituongKcb;

                        if (!q.Any())
                        {

                            Utility.ShowMsg(
                                string.Format(
                                    "Đầu thẻ :{0} chưa được tạo quan hệ với mã quyền lợi {1}\n Đề nghị bạn kiểm tra lại danh mục đối tượng tham gia BHYT",
                                    txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text));
                            txtMaQuyenloi_BHYT.Focus();
                            txtMaQuyenloi_BHYT.SelectAll();
                            return false;
                        }
                    }
                    else
                    {
                        Utility.ShowMsg(
                            string.Format(
                                "Đầu thẻ :{0} chưa được tạo quan hệ với mã quyền lợi {1}\n Đề nghị bạn kiểm tra lại danh mục đối tượng tham gia BHYT",
                                txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text));
                        txtMaQuyenloi_BHYT.Focus();
                        txtMaQuyenloi_BHYT.SelectAll();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(txtNoiphattheBHYT.Text))
                {
                    if (txtNoiphattheBHYT.Text.Length <= 1)
                    {
                        Utility.ShowMsg("Mã nơi phát thẻ BHYT phải nằm trong khoảng từ 00->99", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtNoiphattheBHYT.Focus();
                        txtNoiphattheBHYT.SelectAll();
                        return false;
                    }
                    if (Utility.Int32Dbnull(txtNoiphattheBHYT.Text, 0) <= 0)
                    {
                        Utility.ShowMsg("Mã nơi phát thẻ BHYT không được phép có chữ cái và phải nằm trong khoảng từ 00->99", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtNoiphattheBHYT.Focus();
                        txtNoiphattheBHYT.SelectAll();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(txtOthu4.Text))
                {
                    if (txtOthu4.Text.Length <= 1)
                    {
                        Utility.ShowMsg("Hai kí tự ô số 4 của mã bảo hiểm nằm trong khoảng từ 01->99", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtOthu4.Focus();
                        txtOthu4.SelectAll();
                        return false;
                    }

                    if (Utility.Int32Dbnull(txtOthu4.Text, 0) <= 0)
                    {
                        Utility.ShowMsg("Hai kí tự ô số 4 của mã bảo hiểm không được phép có chữ cái và phải nằm trong khoảng từ 01->99", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtOthu4.Focus();
                        txtOthu4.SelectAll();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(txtOthu5.Text))
                {
                    if (txtOthu5.Text.Length <= 2)
                    {
                        Utility.ShowMsg("3 kí tự ô số 5 của mã bảo hiểm nằm trong khoảng từ 001->999", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtOthu5.Focus();
                        txtOthu5.SelectAll();
                        return false;
                    }

                    if (Utility.Int32Dbnull(txtOthu5.Text, 0) <= 0)
                    {
                        Utility.ShowMsg("3 kí tự ô số 5 của mã bảo hiểm không được phép có chữ cái và phải nằm trong khoảng từ 001->999", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtOthu5.Focus();
                        txtOthu5.SelectAll();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(txtOthu6.Text))
                {
                    if (txtOthu6.Text.Length <= 4)
                    {
                        Utility.ShowMsg("5 kí tự ô số 6 của mã bảo hiểm nằm trong khoảng từ 00001->99999", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtOthu6.Focus();
                        txtOthu6.SelectAll();
                        return false;
                    }

                    if (Utility.Int32Dbnull(txtOthu6.Text, 0) <= 0)
                    {
                        Utility.ShowMsg("5 kí tự ô số 6 của mã bảo hiểm không được phép có chữ cái và phải nằm trong khoảng từ 00001->99999", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtOthu6.Focus();
                        txtOthu6.SelectAll();
                        return false;
                    }
                }
            }
            if (!string.IsNullOrEmpty(txtNoiDongtrusoKCBBD.Text))
            {
                if (txtNoiDongtrusoKCBBD.Text.Length <=1)
                {
                    Utility.ShowMsg("2 kí tự nơi đóng trụ sợ KCBBD phải nhập từ 01->99", "Thông báo",
                                    MessageBoxIcon.Information);
                    txtNoiDongtrusoKCBBD.Focus();
                    txtNoiDongtrusoKCBBD.SelectAll();
                    return false;
                }

                if (Utility.Int32Dbnull(txtNoiDongtrusoKCBBD.Text, 0) <= 0)
                {
                    Utility.ShowMsg("2 kí tự nơi đóng trụ sợ KCBBD không được phép có chữ cái và phải nằm trong khoảng từ 01->99", "Thông báo",
                                    MessageBoxIcon.Information);
                    txtNoiDongtrusoKCBBD.Focus();
                    txtNoiDongtrusoKCBBD.SelectAll();
                    return false;
                }

                SqlQuery sqlQuery = new Select().From(DmucDiachinh.Schema)
                    .Where(DmucDiachinh.Columns.MaDiachinh).IsEqualTo(txtNoiDongtrusoKCBBD.Text);
                if (sqlQuery.GetRecordCount() <= 0)
                {
                    Utility.ShowMsg(
                        "Mã thành phố nơi đăng ký khám hiện không tồn tại trong CSDL\n Mời bạn liên hệ với quản trị mạng để nhập thêm",
                        "Thông báo", MessageBoxIcon.Information);
                    txtNoiDongtrusoKCBBD.Focus();
                    txtNoiDongtrusoKCBBD.SelectAll();
                    return false;
                }
            }
            if (!string.IsNullOrEmpty( txtNoiDKKCBBD.Text))
            {
                string maDiachinh = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_DANGKY_CACHXACDINH_NOIDKKCBBD", true) == "0" ? txtNoiphattheBHYT.Text : txtNoiDongtrusoKCBBD.Text;
                int i = (from p in globalVariables.gv_dtDmucNoiKCBBD.AsEnumerable()
                    where p[DmucNoiKCBBD.Columns.MaDiachinh].Equals(maDiachinh) 
                    select p).Count();
                //SqlQuery sqlQuery = new Select().From(DmucNoiKCBBD.Schema)
                //    .Where(DmucNoiKCBBD.Columns.MaKcbbd).IsEqualTo(txtNoiDKKCBBD.Text)
                //    .And(DmucNoiKCBBD.Columns.MaDiachinh).IsEqualTo(maDiachinh);
                if (i <= 0)
                {
                    Utility.ShowMsg(
                        "Mã  nơi đăng ký khám hiện không tồn tại trong CSDL\n Mời bạn liên hệ với quản trị mạng để nhập thêm",
                        "Thông báo", MessageBoxIcon.Information);
                    txtNoiDKKCBBD.Focus();
                    txtNoiDKKCBBD.SelectAll();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// hàm thực hiện việc số thứ tự của BHYT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaQuyenloi_BHYT_TextChanged(object sender, EventArgs e)
        {
            if (_maDoituongKcb == "DV") return;
            if (_hasjustpressBackKey && txtMaQuyenloi_BHYT.Text.Length <= 0)
            {
                txtMaDtuong_BHYT.Focus();
                if (txtMaDtuong_BHYT.Text.Length > 0) txtMaDtuong_BHYT.Select(txtMaDtuong_BHYT.Text.Length, 0);
            }
            if (txtMaQuyenloi_BHYT.Text.Length < 1) return;
            if (!IsValidTheBhyt()) return;
            TinhPtramBhyt();
            txtNoiphattheBHYT.Focus();
            txtNoiphattheBHYT.SelectAll();
        }

        /// <summary>
        /// hàm thực hiện việc thay đổi thông tin của phần 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNoiDongtrusoKCBBD_TextChanged(object sender, EventArgs e)
        {
            if (_maDoituongKcb == "DV") return;
            if (_hasjustpressBackKey && txtNoiDongtrusoKCBBD.Text.Length <= 0)
            {
                txtOthu6.Focus();
                if (txtOthu6.Text.Length > 0) txtOthu6.Select(txtOthu6.Text.Length, 0);
                return;
            }
            if (txtNoiDongtrusoKCBBD.Text.Length < 2)  return;
            if (!IsValidTheBhyt()) return;
            GetNoiDangKy();
           // LoadClinicCode();
            txtNoiDKKCBBD.Focus();
            txtNoiDKKCBBD.SelectAll();
        }

        private void txtOthu4_TextChanged(object sender, EventArgs e)
        {
            if (_maDoituongKcb == "DV") return;
            if (_hasjustpressBackKey && txtOthu4.Text.Length <= 0)
            {
                txtNoiphattheBHYT.Focus();
                if (txtNoiphattheBHYT.Text.Length > 0) txtNoiphattheBHYT.Select(txtNoiphattheBHYT.Text.Length, 0);
                return;
            }
            if (txtOthu4.Text.Length < 2) return;
            if (!IsValidTheBhyt()) return;
            txtOthu5.Focus();
            txtOthu5.SelectAll();
        }

        private void txtOthu5_TextChanged(object sender, EventArgs e)
        {
            if (_maDoituongKcb == "DV") return;
            if (_hasjustpressBackKey && txtOthu5.Text.Length <= 0)
            {
                txtOthu4.Focus();
                if (txtOthu4.Text.Length > 0) txtOthu4.Select(txtOthu4.Text.Length, 0);
                return;
            }
            if (txtOthu5.Text.Length < 3) return;
            if (!IsValidTheBhyt()) return;
            txtOthu6.Focus();
            txtOthu6.SelectAll();
        }

        private void txtOthu6_TextChanged(object sender, EventArgs e)
        {
            if (_maDoituongKcb == "DV") return;
                if (_hasjustpressBackKey && txtOthu6.Text.Length <= 0)
                {
                    txtOthu5.Focus();
                    if (txtOthu5.Text.Length > 0) txtOthu5.Select(txtOthu5.Text.Length, 0);
                    return;
                }
                if (txtOthu6.Text.Length < 5) return;
                if (!IsValidTheBhyt()) return;
                txtNoiDongtrusoKCBBD.Focus();
                txtNoiDongtrusoKCBBD.SelectAll();
        }

        private void txtNoiphattheBHYT_TextChanged(object sender, EventArgs e)
        {
            if (_maDoituongKcb == "DV") return;
            if (txtNoiphattheBHYT.Text.Length < 2)
            {
                txtNoiCapThe.Text = "";
                Utility.SetMsg(lblNoiCapThe, "", false);
                return;
            }
            else
                GetNoiDangKy();
            if (!IsValidTheBhyt()) return;
            txtOthu4.Focus();
            txtOthu4.SelectAll();
            
        }

        private void GetNoiDangKy()
        {
            //SqlQuery sqlQuery = new Select().From(DmucDiachinh.Schema)
            //    .Where(DmucDiachinh.Columns.MaDiachinh).IsEqualTo(txtNoiDongtrusoKCBBD.Text);
            var objdiachinh = (from p in globalVariables.gv_dtDmucDiachinh.AsEnumerable()
                where p[DmucDiachinh.Columns.MaDiachinh].Equals(txtNoiDongtrusoKCBBD.Text)
                select p).FirstOrDefault();
            //var objDiachinh = sqlQuery.ExecuteSingle<DmucDiachinh>();
            if (objdiachinh != null)
            {
                txtNoiCapThe.Text = Utility.sDbnull(objdiachinh["ten_diachinh"]);
                lblNoiCapThe.Visible = true;
                Utility.SetMsg(lblNoiCapThe, Utility.sDbnull(objdiachinh["ten_diachinh"]), false);
                //LoadClinicCode();
            }
            else
            {
                lblNoiCapThe.Visible = false;
                txtNoiCapThe.Text = "";
            }
        }

        private void txtNoiDKKCBBD_TextChanged(object sender, EventArgs e)
        {
            if (_maDoituongKcb == "DV") return;
            if (txtNoiDKKCBBD.Text.Length < 3)
            {
                txtClinicName.Text = "";
                //Utility.SetMsg(lblClinicName, "", false);
                return;
            }
            LoadClinicCode();
            if (lnkThem.Visible) lnkThem.Focus();
            else
                dtInsFromDate.Focus();
        }

        private void LaySoTheBhyt()
        {
            string SoBHYT = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text,
                                          txtNoiphattheBHYT.Text, txtOthu4.Text, txtOthu5.Text, txtOthu6.Text,
                                          txtNoiDongtrusoKCBBD.Text, txtNoiDKKCBBD.Text);
            GetSoBHYT = SoBHYT;
        }
        private string mathe_bhyt_full()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text,
                                          txtNoiphattheBHYT.Text, txtOthu4.Text, txtOthu5.Text, txtOthu6.Text,
                                          txtNoiDongtrusoKCBBD.Text, txtNoiDKKCBBD.Text);
           
        }

        private string Laymathe_BHYT()
        {
            string SoBHYT = string.Format("{0}{1}{2}{3}{4}{5}", txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text,
                                          txtNoiphattheBHYT.Text, txtOthu4.Text, txtOthu5.Text, txtOthu6.Text);
            return SoBHYT;
        }

        /// <summary>
        /// hàm thực hiện việc tính phàn trăm bảo hiểm
        /// </summary>
        private void TinhPtramBhyt()
        {
            try
            {
                LaySoTheBhyt();
                if (!string.IsNullOrEmpty(Laymathe_BHYT()) && Laymathe_BHYT().Length >= 15)
                {
                    if ((!string.IsNullOrEmpty(GetSoBHYT)) && (!string.IsNullOrEmpty(txtNoiDKKCBBD.Text)))
                    {
                        var objLuotkham = new KcbLuotkham();
                        objLuotkham.MaNoicapBhyt = Utility.sDbnull(txtNoiphattheBHYT.Text);
                        objLuotkham.NoiDongtrusoKcbbd = Utility.sDbnull(txtNoiDongtrusoKCBBD.Text);
                        objLuotkham.MatheBhyt = Laymathe_BHYT();
                        objLuotkham.MaDoituongBhyt = txtMaDtuong_BHYT.Text;
                        objLuotkham.DungTuyen = !chkTraiTuyen.Visible ? 1 : (((byte?)(chkTraiTuyen.Checked ? 0 : 1)));
                        objLuotkham.MadtuongSinhsong = txtMaDTsinhsong.myCode;
                        objLuotkham.GiayBhyt = Utility.Bool2byte(chkGiayBHYT.Checked);
                        objLuotkham.MaKcbbd = Utility.sDbnull(txtNoiDKKCBBD.Text);
                        objLuotkham.IdDoituongKcb = _idDoituongKcb;
                        objLuotkham.MaQuyenloi = Utility.Int32Dbnull(txtMaQuyenloi_BHYT.Text);
                        THU_VIEN_CHUNG.TinhPtramBhyt(objLuotkham);
                        txtPtramBHYT.Text = objLuotkham.PtramBhyt.ToString();
                        txtptramDauthe.Text = objLuotkham.PtramBhytGoc.ToString();
                    }
                    else
                    {
                        txtPtramBHYT.Text = "0";
                        txtptramDauthe.Text = "0";
                    }
                }
                else
                {
                    txtPtramBHYT.Text = "0";
                    txtptramDauthe.Text = "0";
                }
            }
            catch (Exception)
            {
                txtPtramBHYT.Text = "0";
                txtptramDauthe.Text = "0";
            }
        }

        /// <summary>
        /// hàm thực hiện việc load thông tin của nơi khám chữa bệnh ban đầu
        /// </summary>
        private void LoadClinicCode()
        {
            try
            {
                string maDiachinh = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_DANGKY_CACHXACDINH_NOIDKKCBBD", true) == "0" ? txtNoiphattheBHYT.Text : txtNoiDongtrusoKCBBD.Text;
                //Lấy mã Cơ sở KCBBD
                string vCliniCode = maDiachinh + txtNoiDKKCBBD.Text.Trim();
                string strClinicName = "";
                DataTable dataTable = _kcbDangky.GetClinicCode(vCliniCode);
                if (dataTable.Rows.Count > 0)
                {
                    strClinicName = dataTable.Rows[0][DmucNoiKCBBD.Columns.TenKcbbd].ToString();
                    txtClinicName.Text = strClinicName.Trim();
                  //  Utility.SetMsg(lblClinicName, strClinicName, string.IsNullOrEmpty(txtNoiDKKCBBD.Text));
                }
                else
                {
                   // Utility.SetMsg(lblClinicName, strClinicName, false);
                    txtClinicName.Text = "";
                }
               // lblClinicName.Visible = dataTable.Rows.Count > 0;
                lnkThem.Visible = dataTable.Rows.Count <= 0;
                //txtNamePresent.Text = strClinicName;
                //Check đúng tuyến cần lấy mã nơi cấp BHYT+mã kcbbd thay vì mã cơ sở kcbbd
                if (!chkCapCuu.Checked) //Nếu không phải trường hợp cấp cứu
                {
                    if (globalVariables.gv_intBHYT_TUDONGCHECKTRAITUYEN == 1)
                        //Nếu có chế độ tự động kiểm tra trái tuyến đúng tuyến
                        chkTraiTuyen.Checked =
                            !(THU_VIEN_CHUNG.KiemtraDungtuyenTraituyen(txtNoiDongtrusoKCBBD.Text.Trim() +
                                                                    txtNoiDKKCBBD.Text.Trim()) ||
                              (!THU_VIEN_CHUNG.KiemtraDungtuyenTraituyen(txtNoiDongtrusoKCBBD.Text.Trim() +
                                                                      txtNoiDKKCBBD.Text.Trim()) &&
                               chkChuyenVien.Checked));
                }
                else //Nếu là BN cấp cứu
                {
                    if (globalVariables.gv_intBHYT_TUDONGCHECKTRAITUYEN == 1)
                        //Nếu có chế độ tự động kiểm tra trái tuyến đúng tuyến
                        chkTraiTuyen.Checked =
                            (!(THU_VIEN_CHUNG.KiemtraDungtuyenTraituyen(txtNoiDongtrusoKCBBD.Text.Trim() +
                                                                     txtNoiDKKCBBD.Text.Trim()) ||
                               (!THU_VIEN_CHUNG.KiemtraDungtuyenTraituyen(txtNoiDongtrusoKCBBD.Text.Trim() +
                                                                       txtNoiDKKCBBD.Text.Trim()) &&
                                chkChuyenVien.Checked))) && (!chkCapCuu.Checked);
                }

                if (txtMaDTsinhsong.myCode != "-1")
                {
                    if (chkTraiTuyen.Checked)
                        chkTraiTuyen.Checked = false;
                }
                TinhPtramBhyt();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
            }
            finally
            {
                lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
            }
        }

     

        /// <summary>
        /// hàm thực hiện load thông tin khám bệnh
        /// </summary>
        private void NapThongtinDichvuKcb()
        {
            bool oldStatus = AllowTextChanged;
            try
            {
                cboKieuKham.DataSource = null;
                //Khởi tạo danh mục Loại khám
                string objecttypeCode = "DV";
                DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(_idDoituongKcb);
                if (objectType != null)
                {
                    objecttypeCode = Utility.sDbnull(objectType.MaDoituongKcb);
                }
                MA_DTUONG = objecttypeCode;
                m_dtDanhsachDichvuKCB = THU_VIEN_CHUNG.LayDsach_Dvu_KCB(objecttypeCode, this.Args.Split('-')[1], -1, -1);
                Get_KIEUKHAM(objecttypeCode);
                Get_PHONGKHAM(objecttypeCode);
                AutocompleteMaDvu();
                AutocompletePhongKham();
                AutocompleteKieuKham();
                m_dtDanhsachDichvuKCB.AcceptChanges();
                cboKieuKham.DataSource = m_dtDanhsachDichvuKCB;
                cboKieuKham.DataMember = DmucDichvukcb.Columns.IdDichvukcb;
                cboKieuKham.ValueMember = DmucDichvukcb.Columns.IdDichvukcb;
                cboKieuKham.DisplayMember = DmucDichvukcb.Columns.TenDichvukcb;
                //cboKieuKham.ValueChanged += new EventHandler(cboKieuKham_ValueChanged);
              //  cboKieuKham.Visible = globalVariables.UserName == "ADMIN";
                if (m_dtDanhsachDichvuKCB == null || m_dtDanhsachDichvuKCB.Columns.Count <= 0) return;
                AllowTextChanged = true;
                if (m_dtDanhsachDichvuKCB.Rows.Count == 1 && MEnAction != action.Update)
                {
                    cboKieuKham.SelectedIndex = 0;
                    var idKieukham = (from s in m_dtDanhsachDichvuKCB.AsEnumerable()
                                          select s).FirstOrDefault();
                    if (idKieukham != null)
                    {
                        txtIDPkham.Text = Utility.sDbnull(idKieukham["id_phongkham"]);
                        txtIDKieuKham.Text = Utility.sDbnull(idKieukham["id_kieukham"]);
                    }
                    // txtIDKieuKham.Text = Utility.sDbnull(txtExamtypeCode.MyID);
                   // txtIDKieuKham.Text = Utility.sDbnull(cboKieuKham.Value);
                   // cboKieuKham.Value = txtExamtypeCode.txtMyID;
                   // txtKieuKham.Text = cboKieuKham.Text;
                    AutoLoadKieuKham();
                }
                AllowTextChanged = oldStatus;
            }
            catch
            {
                AllowTextChanged = oldStatus;
            }
        }
        private void Get_PHONGKHAM(string MA_DTUONG)
        {
            m_PhongKham = THU_VIEN_CHUNG.Get_PHONGKHAM(MA_DTUONG);
        }

        private void Get_KIEUKHAM(string MA_DTUONG)
        {
            m_kieuKham =THU_VIEN_CHUNG.Get_KIEUKHAM(MA_DTUONG,-1);
        }
        private void AddShortCut_KieuKham()
        {
            try
            {
                if (m_dtDanhsachDichvuKCB == null || m_dtDanhsachDichvuKCB.Columns.Count <= 0) return;

                if (!m_dtDanhsachDichvuKCB.Columns.Contains("ShortCut"))
                    m_dtDanhsachDichvuKCB.Columns.Add(new DataColumn("ShortCut", typeof (string)));
                foreach (DataRow dr in m_dtDanhsachDichvuKCB.Rows)
                {
                    string shortcut = "";
                    string realName = dr[DmucDichvukcb.Columns.TenDichvukcb].ToString().Trim() + " " + Utility.Bodau(dr[DmucDichvukcb.Columns.TenDichvukcb].ToString().Trim());
                    shortcut = dr[DmucDichvukcb.Columns.MaDichvukcb].ToString().Trim();
                    string[] arrWords = realName.ToLower().Split(' ');
                    string _space = "";
                    string _Nospace = "";
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                        {
                            _space += word + " ";
                            _Nospace += word;
                        }
                    }
                    shortcut += _space; // +_Nospace;
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                            shortcut += word.Substring(0, 1);
                    }
                    dr["ShortCut"] = shortcut;
                }
            }
            catch
            {
            }
            
        }

       

       

        private void txtTuoi_LostFocus(object sender, EventArgs e)
        {
            //txtNamSinh.TextChanged += new EventHandler(txtNamSinh_TextChanged);   
        }

        /// <summary>
        /// hàm thực hiện việc tính toán tuổi của bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTuoi_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!_allowAgeChanged) return;
                if (!string.IsNullOrEmpty(txtTuoi.Text))
                {
                    if (dtpBOD.CustomFormat == "yyyy")
                    {
                        dtpBOD.Value =
                            new DateTime(
                                Utility.Int32Dbnull(globalVariables.SysDate.Year - Utility.Int32Dbnull(txtTuoi.Text, 0)),
                                dtpBOD.Value.Month, dtpBOD.Value.Day, dtpBOD.Value.Hour, dtpBOD.Value.Minute, 0);
                    }
                    else
                    {
                        if(objBenhnhan != null && _objLuotkham !=null)
                        {
                            dtpBOD.Value = Convert.ToDateTime(objBenhnhan.NgaySinh);
                            UIAction.SetText(lblLoaituoi, _objLuotkham.LoaiTuoi == 0 ? "" : (_objLuotkham.LoaiTuoi == 1 ? "Tháng" : "Tuần"));
                        }
                         
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }

        private void SinhMaLanKham()
        {
            txtSolankham.Text = string.Empty;
            if (MEnAction == action.Insert)
            {
                txtMaBN.Text = @"Tự sinh";
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
        /// <summary>
        /// Hàm này hơi vô nghĩa vì số lần khám tính theo id_benhnhan
        /// </summary>
        private void LaySoThuTuDoiTuong()
        {
            txtSolankham.Text = THU_VIEN_CHUNG.LaySTTKhamTheoDoituong(_idDoituongKcb).ToString();
        }

        /// <summary>
        /// hàm thực hiện việc làm sách thông tin của bệnh nhân
        /// </summary>
        private void ClearControl()
        {
            setMsg(uiStatusBar1.Panels["MSG"], "", false);
            m_blnHasJustInsert = false;
            txtSolankham.Text = @"1";
            txtTEN_BN.Clear();
            dtpBOD.Value = globalVariables.SysDate;
            txtTuoi.Clear();
            txtCMT.Clear();
            txtNgheNghiep.Clear();
            txtDiachi.Clear();
            txtdiachichitiet.Clear();
           // txtDantoc__OnShowData();
          //  txtDantoc.Clear();
            txtTrieuChungBD.Clear();
            txtSoDT.Clear();
            txtNguoiLienhe.Clear();
            chkChuyenVien.Checked = false;
            txtNoichuyenden.SetCode("-1");
            txtKieuKham.ClearMe();
            txtPhongkham.ClearMe();
            txtSoBA.Clear();
            txtLoaiBN.SetCode("-1");
            txtNoigioithieu.ResetText();
            txtchandoantuyenduoi.Clear();
            txtEmail.Clear();
            if (THU_VIEN_CHUNG.IsNgoaiGio())
            {
               this.Text= @"Bệnh nhân đang khám dịch vụ ngoài giờ";
            }
            
            ModifyCommand();
            EnumerableRowCollection<DataRow> query = from kham in m_dtDanhsachDichvuKCB.AsEnumerable()
                                                     select kham;
            if (query.Count() > 0)
            {
                cboKieuKham.SelectedIndex = -1;
                cboKieuKham.Text = @"CHỌN PHÒNG KHÁM";
            }
            AllowTextChanged = false;
            XoathongtinBHYT(true);

            _maDoituongKcb = Utility.sDbnull(cboDoituongKCB.SelectedValue);
            _objDoituongKcb = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(_maDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
            if (_objDoituongKcb == null) return;
            _idDoituongKcb = _objDoituongKcb.IdDoituongKcb;
            _idLoaidoituongKcb = _objDoituongKcb.IdLoaidoituongKcb;
            _tenDoituongKcb = _objDoituongKcb.TenDoituongKcb;
            _ptramBhytCu = _objDoituongKcb.PhantramTraituyen.Value;
            _ptramBhytGocCu = _ptramBhytCu;
            txtPtramBHYT.Text = _objDoituongKcb.PhantramTraituyen.ToString();
            txtptramDauthe.Text = _objDoituongKcb.PhantramTraituyen.ToString();
            if (_objDoituongKcb.IdLoaidoituongKcb == 0)//ĐỐi tượng BHYT
            {
                pnlBHYT.Enabled = true;
                lblPtram.Text = @"Mức hưởng:";
                TinhPtramBhyt();
                NapThongtinDichvuKcb();
                txtMaDtuong_BHYT.SelectAll();
                txtMaDtuong_BHYT.Focus();
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEPDON_SUDUNG_QRCODE", "0", false) == "1")
                {
                    txtQRCode.Clear();
                    txtQRCode.Focus();
                }
            }
            else//Đối tượng khác BHYT
            {
                pnlBHYT.Enabled = false;
                lblPtram.Text = @"Mức hưởng:";
                //XoathongtinBHYT(PropertyLib._KCBProperties.XoaBHYT);
                NapThongtinDichvuKcb();
                txtTEN_BN.Focus();
            }

            chkTraiTuyen.Checked = false;
            lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
            lblPtramdauthe.Visible = _objDoituongKcb.IdLoaidoituongKcb == 0;
            txtptramDauthe.Visible = _objDoituongKcb.IdLoaidoituongKcb == 0;
            chkChuyenVien.Checked = false;
            chkCapCuu.Checked = false;
            txtPtramBHYT.Text = "0";
            txtptramDauthe.Text = "0";
            AllowTextChanged = true;
            //Chuyển về trạng thái thêm mới
            MEnAction = action.Insert;
            if (PropertyLib._KCBProperties.SexInput) cboPatientSex.SelectedIndex = -1;
            lnkThem.Visible = false;
           
            m_dtDangkyPhongkham.Clear();
            if (pnlBHYT.Enabled)
            {
                lblPtram.Text = @"Mức hưởng:";
                txtMaDtuong_BHYT.Focus();
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEPDON_SUDUNG_QRCODE", "0", false) == "1")
                {
                    txtQRCode.Clear();
                    txtQRCode.Focus();
                }
            }
            else
            {
                lblPtram.Text = @"Mức hưởng:";
                _ptramBhytCu = _objDoituongKcb.PhantramTraituyen.Value;
                _ptramBhytGocCu = _ptramBhytCu;
                txtPtramBHYT.Text = _objDoituongKcb.PhantramTraituyen.ToString();
                txtptramDauthe.Text = _objDoituongKcb.PhantramTraituyen.ToString();
                txtTEN_BN.Focus();
            }
            if (MEnAction == action.Insert)
            {
                dtpInputDate.Value = globalVariables.SysDate;
                dtCreateDate.Value = globalVariables.SysDate;
                dtInsFromDate.Value = new DateTime(globalVariables.SysDate.Year, 1, 1);
                dtInsToDate.Value = new DateTime(globalVariables.SysDate.Year, 12, 31);
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

        private void cmdThemMoiBN_Click(object sender, EventArgs e)
        {
            //Cập nhật lại mã lượt khám chưa dùng tới trong trường hợp nhấn New liên tục
            ResetLuotkham();
            ClearControl();
            SinhMaLanKham();
        }

        /// <summary>
        /// hàm thực hiện viecj thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private string ketquacheckthe = "";
        /// <summary>
        /// hàm thực hiện việc lưu thông tin của đối tượng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsExceedData())
                {
                    Utility.ShowMsg("Phiên bản Demo chỉ cho phép bạn tiếp đón tối đa 100 lượt khám. Mời bạn liên hệ 0915150148 để được trợ giúp");
                    return;
                }
                cmdSave.Enabled = false;
                if(MEnAction==action.Update)
                if (txtKieuKham.Text.Trim() != "" && txtPhongkham.Text.Trim() != "" && cboKieuKham.SelectedIndex == -1)
                    AutoLoadKieuKham();
                if (!IsValidData()) return;
               // log.Trace("Lay tham so");
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("XML_CHECK_DATA", "0", false) == "1"
                    && Utility.sDbnull(cboDoituongKCB.SelectedValue) == "BHYT"
                    &&
                    THU_VIEN_CHUNG.IsAddressAvailable(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEPDON_DIACHI_CHECKTHE", "gdbhyt.baohiemxahoi.gov.vn", false),
                        Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEPDON_THOIGIAN_TIMEOUT", "100",
                            false))))
                {
                    //log.Trace("Qua cho Lay tham so");
                    if (!IsValidCard()) return;
                    // log.Trace("Kiem tra xong roi ");
                }
                else
                {
                    string maThe = string.Format("{0}{1}{2}{3}{4}{5}", txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text,
                                        txtNoiphattheBHYT.Text, txtOthu4.Text, txtOthu5.Text, txtOthu6.Text);
                    log.Trace(string.Format("{0} - Khong the ket noi den trang {1}", maThe, THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEPDON_DIACHI_CHECKTHE", "gdbhyt.baohiemxahoi.gov.vn", false)));
                }
                PerformAction();
                Thread.Sleep(10);//Nghir 0.1 giay
            }
            catch(Exception ex)
            {
                if(globalVariables.IsAdmin)
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
            finally
            {
                cmdSave.Enabled = true;
                blnManual = false;
                DiachiBNCu = false;
                DiachiBHYTCu = false;
                cmdSave.Enabled = true;
            }
        }
        bool IsExceedData()
        {
            try
            {
                //if (PropertyLib._ConfigProperties.HIS_AppMode != VNS.Libs.AppType.AppEnum.AppMode.License)
                //{
                //var lst = new Select().From(KcbLuotkham.Schema).ExecuteAsCollection<KcbLuotkhamCollection>();
                //return lst.Count >= 100;
                //}
                return false;
            }
            catch(Exception ex)
            {
                Utility.CatchException("isExceedData()-->",ex);
                return true;
            }
        }
        private void StatusControl()
        {
            if (THU_VIEN_CHUNG.IsNgoaiGio())
            {
                this.Text = @"Bệnh nhân đang khám dịch vụ ngoài giờ";
            }
            
        }
        public KQNhanLichSuKCBBS ObjLichSuKcb2018;
        private string maketqua = "";
        private bool IsValidCard()
        {
            //var objApiTheBhyt = new VMS.HIS.Ngoaitru.CheckCard.ApiTheBHYT();
            //objApiTheBhyt.maThe = string.Format("{0}{1}{2}{3}{4}{5}", txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text,
            //                              txtNoiphattheBHYT.Text, txtOthu4.Text, txtOthu5.Text, txtOthu6.Text);

            //objApiTheBhyt.hoTen = txtTEN_BN.Text.Trim();
            //objApiTheBhyt.ngaySinh = dtpBOD.Value.ToString("yyyy");
            //objApiTheBhyt.gioiTinh = Convert.ToSByte(Utility.Int16Dbnull(cboPatientSex.SelectedValue) == 0 ? 1 : 2);
            //objApiTheBhyt.maCSKCB = string.Format("{0}{1}", txtNoiDongtrusoKCBBD.Text.Trim(), txtNoiDKKCBBD.Text.Trim());
            //objApiTheBhyt.ngayBD = dtInsFromDate.Value.ToString("yyyyMMdd");
            //objApiTheBhyt.ngayKT = dtInsToDate.Value.ToString("yyyyMMdd");
            //ObjLichSuKcb2018 = new CheckCard_BHYT().CheckCard366(objApiTheBhyt);
            //maketqua = ObjLichSuKcb2018.maKetQua;
            //Utility.Log(this.Name, globalVariables.UserName, string.Format("Thông báo : {0} . Họ tên: {1} Mã thẻ: {2} giới tính : {3} Ngày sinh: " +
            //                                                                     "{4} Mã CSKBĐ: {5} Hạn thẻ từ: {6} đến :{7}! ",
            //                                                                     maketqua, objApiTheBhyt.hoTen, objApiTheBhyt.maThe, objApiTheBhyt.gioiTinh,
            //                                                                     objApiTheBhyt.ngaySinh, objApiTheBhyt.maCSKCB, objApiTheBhyt.ngayBD, objApiTheBhyt.ngayKT));
            //bool sThongBao = THU_VIEN_CHUNG.Laygiatrithamsohethong_off("BHYT_CHECKCARD_CANHBAO", "1", false) == "1";
            
            //if (ObjLichSuKcb2018.maloi != "000" && ObjLichSuKcb2018.maloi != "001" && ObjLichSuKcb2018.maloi != "002" &&
            //    ObjLichSuKcb2018.maloi != "003" && ObjLichSuKcb2018.maloi != "004")
            //{
            //    if (sThongBao)
            //    {
            //        Utility.ShowMsg(ObjLichSuKcb2018.ghiChu, "Thông báo");
            //        return false;
            //    }
            //    else
            //    {
            //        FrmThongTinCheckThe frm = new FrmThongTinCheckThe();
            //        //frm.Kcbnhanlichsu = ObjLichSuKcb2018;
            //        frm.ShowDialog();
            //        if (!frm.Chapnhan)
            //        {
            //            return true;
            //        }
            //    }
            //    return false;
            //}
            //if (ObjLichSuKcb2018.maloi == "000")
            //{
            //    try
            //    {
            //        dtInsFromDate.Value = Convert.ToDateTime(ObjLichSuKcb2018.gtTheTu);
            //        dtInsToDate.Value = Convert.ToDateTime(ObjLichSuKcb2018.gtTheDen);
            //        string makhuvuc = Utility.sDbnull(ObjLichSuKcb2018.maKV);
            //        if (!string.IsNullOrEmpty(makhuvuc))
            //        {
            //            txtMaDTsinhsong.SetCode(Utility.sDbnull(makhuvuc)); 
            //        }
            //        if (ObjLichSuKcb2018.ngayDu5Nam.Length > 1)
            //        {
            //            //chkGiay_Bhyt.Checked = true;
            //            dtpNgaydu5nam.Value = Convert.ToDateTime(ObjLichSuKcb2018.ngayDu5Nam);
            //        }
            //        if (globalVariables.IsAdmin)
            //        {
            //            if (sThongBao)
            //            {
            //                Utility.ShowMsg(ObjLichSuKcb2018.ghiChu, "Thông báo");
            //            }
            //            else
            //            {
            //                FrmThongTinCheckThe frm = new FrmThongTinCheckThe();
            //                frm.Kcbnhanlichsu = ObjLichSuKcb2018;
            //                frm.ShowDialog();
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Utility.ShowMsg(ex.ToString());
            //        return false;
            //    }
            //}
            //if (ObjLichSuKcb2018.maloi == "001" || ObjLichSuKcb2018.maloi == "002")
            //{
            //    Utility.ShowMsg(ObjLichSuKcb2018.ghiChu, "Thông báo");
            //    return true;
            //}
            //if (ObjLichSuKcb2018.maloi == "004")
            //{
            //    Utility.ShowMsg(ObjLichSuKcb2018.ghiChu, "Thông báo");
            //    return true;
            //}
            //if (ObjLichSuKcb2018.maloi == "003")
            //{
            //    if (sThongBao)
            //    {
            //        Utility.ShowMsg(ObjLichSuKcb2018.ghiChu, "Thông báo");
            //        return false;
            //    }
            //    else
            //    {
            //        var frm = new FrmThongTinCheckThe();
            //        frm.Kcbnhanlichsu = ObjLichSuKcb2018;
            //        frm.ShowDialog();
            //        if (frm.Chapnhan)
            //        {
            //            if (Utility.sDbnull(ObjLichSuKcb2018.maTheMoi, "") != "")
            //            {
            //                txtMaDtuong_BHYT.Text = ObjLichSuKcb2018.maTheMoi.Substring(0, 2); //BT
            //                txtMaQuyenloi_BHYT.Text = ObjLichSuKcb2018.maTheMoi.Substring(2, 1); //4
            //                txtNoiphattheBHYT.Text = ObjLichSuKcb2018.maTheMoi.Substring(3, 2); //24
            //                txtOthu4.Text = ObjLichSuKcb2018.maTheMoi.Substring(5, 2); //10
            //                txtOthu5.Text = ObjLichSuKcb2018.maTheMoi.Substring(7, 3);
            //                txtOthu6.Text = ObjLichSuKcb2018.maTheMoi.Substring(10, 5);
            //            }
            //            if (Utility.sDbnull(ObjLichSuKcb2018.gtTheTuMoi, "") != "")
            //            {
            //                dtInsFromDate.Value = Convert.ToDateTime(ObjLichSuKcb2018.gtTheTuMoi);
            //            }
            //            if (Utility.sDbnull(ObjLichSuKcb2018.gtTheDenMoi, "") != "")
            //            {
            //                dtInsToDate.Value = Convert.ToDateTime(ObjLichSuKcb2018.gtTheDenMoi);
            //            }
            //            if (Utility.sDbnull(txtDiachi_bhyt.Text, "") == "" &&
            //                Utility.sDbnull(ObjLichSuKcb2018.diaChi, "") != "")
            //            {
            //                txtDiachi_bhyt.Text = ObjLichSuKcb2018.diaChi;
            //            }
            //        }
            //        return true;
            //    }
            //}
            //if (ObjLichSuKcb2018.dsLichSuKCB2018 != null)
            //{
            //    grdLichSuKCB.DataSource = ObjLichSuKcb2018.dsLichSuKCB2018;
            //}
            return true;
        }
       
        private bool IsValidData()
        {
            Utility.SetMsg(uiStatusBar1.Panels["MSG"], "", false);
            TimeSpan songaychothuoc = Convert.ToDateTime(dtInsToDate.Value).Subtract(globalVariables.SysDate);
            int songay = Utility.Int32Dbnull(songaychothuoc.TotalDays);
            if (Utility.Int32Dbnull(songay) <= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEPDON_SONGAYBATHANTHE","30",true)) 
                && Utility.Int16Dbnull(cboDoituongKCB.SelectedValue) ==2)
            {
                Utility.ShowMsg(string.Format("Hạn thẻ BHYT còn {0} ngày",songay), "Cảnh Báo");
            }
            if (MEnAction==action.Insert && dtCreateDate.Value.ToString("dd/MM/yyyy") != globalVariables.SysDate.ToString("dd/MM/yyyy"))
            {
                if (!Utility.AcceptQuestion("Ngày tiếp đón khác ngày hiện tại. Bạn có chắc chắn hay không?","Cảnh báo",true))
                {
                    dtCreateDate.Focus();
                    return false;
                }
            }
            if (txtLoaikham.myCode == "-1")
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải chọn loại khám", true);
                txtLoaikham.Focus();
                txtLoaikham.SelectAll();
                return false;
            }
            if (THU_VIEN_CHUNG.IsBaoHiem(_idLoaidoituongKcb))
            {
                if (!IsValidBhyt()) return false;
                if (!IsValidTheBhyt()) return false;
            }
            if (THU_VIEN_CHUNG.IsBaoHiem(_objDoituongKcb.IdLoaidoituongKcb))
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATNHAP_DIACHI_BHYT", "0", false) == "1")
                {
                    if (Utility.DoTrim(txtDiachi_bhyt.Text)=="")
                    {
                        Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải nhập địa chỉ thẻ BHYT", true);
                        txtDiachi_bhyt.Focus();
                        return false;
                    }
                }
                if (Utility.DoTrim(txtMaDTsinhsong.Text) != "" && txtMaDTsinhsong.myCode == "-1")
                {
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Mã đối tượng sinh sống chưa đúng. Mời bạn nhập lại", true);
                    txtMaDTsinhsong.SelectAll();
                    txtMaDTsinhsong.Focus();
                    return false;
                }

                
            }
            if (chkChuyenVien.Checked)
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("BATNHAPNOICHUYENDEN", "0", false) == "1")
                {
                    if (txtNoichuyenden.MyCode == "-1")
                    {
                        Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải nhập bệnh viện chuyển đến", true);
                        txtNoichuyenden.SelectAll();
                        txtNoichuyenden.Focus();
                        return false;
                    }
                }
            }
            if (string.IsNullOrEmpty(txtTEN_BN.Text))
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải nhập tên Bệnh nhân", true);
                txtTEN_BN.Focus();
                return false;
            }
           
            if (Utility.Int32Dbnull(txtTuoi.Text,0)<0)
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Tuổi Bệnh nhân phải lớn hơn hoặc bằng không. Mời bạn kiểm tra lại", true);
                txtTuoi.Focus();
                return false;
            }
            if (cboPatientSex.SelectedIndex<0)
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải chọn giới tính của Bệnh nhân",true);
                cboPatientSex.Focus();
                return false;
            }

            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATNHAP_DIACHI_BENHNHAN", "0", false) == "1")
            {
                if (Utility.DoTrim(txtDiachi.Text) == "")
                {
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải nhập địa chỉ Bệnh nhân", true);
                    txtDiachi.Focus();
                    return false;
                }
            }

            if (txtNguonGthieu.MyCode == "-1")
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải nhập nguồn giới thiệu", true);
                txtNguonGthieu.SelectAll();
                txtNguonGthieu.Focus();
                return false;
            }

            if (PropertyLib._KCBProperties.Hoikhikhongdangkyphongkham)
                if (grdRegExam.GetDataRows().Length <= 0 && cboKieuKham.SelectedIndex < 0 && pnlChonphongkham.Visible)
                    if (Utility.AcceptQuestion("Bệnh nhân chưa có phòng khám. Bạn nên chọn một phòng khám cho Bệnh nhân trước khi lưu. " +
                                               "Bạn có muốn tạm dừng để nhập thông tin phòng khám cho bệnh nhân không?\n Nhấn Yes để tạm dừng việc Ghi và chọn lại phòng khám.", "Cảnh báo", true))
                    {
                        txtExamtypeCode.Focus();
                        txtExamtypeCode.Select();
                        //if (pnlChonphongkham.Visible)
                        //{
                        //    txtExamtypeCode.Focus();
                        //    txtExamtypeCode.Select();
                        //}
                        //else
                        //{
                        //    txtKieuKham.Focus();
                        //    txtKieuKham.Select();
                        //}

                    }
                    else
                    {
                        return false;
                    }
            if (grdRegExam.GetDataRows().Length <= 0 && Utility.Int32Dbnull(txtKieuKham.MyID, -1) < 0 && !pnlChonphongkham.Visible)
                if (Utility.AcceptQuestion("Bệnh nhân chưa có kiểu khám. Bạn nên chọn một kiểu khám cho Bệnh nhân trước khi lưu. " +
                                           "Bạn có muốn tạm dừng để nhập thông tin phòng khám cho bệnh nhân không?\n Nhấn Yes để tạm dừng việc Ghi và chọn lại phòng khám.", "Cảnh báo", true))
                {
                    txtKieuKham.Focus();
                    txtKieuKham.Select();
                    //if (pnlChonphongkham.Visible)
                    //{
                    //    txtExamtypeCode.Focus();
                    //    txtExamtypeCode.Select();
                    //}
                    //else
                    //{
                    //    txtKieuKham.Focus();
                    //    txtKieuKham.Select();
                    //}

                }
                else
                {
                    return false;
                }
            //Kiểm tra xem đã chọn phòng khám chưa

            if (Utility.Int32Dbnull(cboKieuKham.Value) > 0)
            {
                DmucDichvukcb objDichvuKcb =
                  DmucDichvukcb.FetchByID(Utility.Int32Dbnull(cboKieuKham.Value));
                if (objDichvuKcb != null)
                {
                    if (objDichvuKcb.IdPhongkham < 0 && Utility.Int32Dbnull(txtPhongkham.MyID, -1) == -1)
                    {
                        Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn cần chọn phòng khám", true);
                        txtPhongkham.Focus();
                        return false;
                    }
                }
            }
            return isValidIdentifyNum();
        }

        /// <summary>
        /// hàm thực hiện viecj kiểm tra thông tin cảu đối tượng bảo hiểm
        /// </summary>
        /// <returns></returns>
        private bool IsValidBhyt()
        {
            if (string.IsNullOrEmpty(txtMaDtuong_BHYT.Text))
            {
                Utility.ShowMsg("Bạn phải nhập Mã đối tượng tham gia BHYT", "Thông báo",
                                MessageBoxIcon.Information);
                txtMaDtuong_BHYT.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtMaQuyenloi_BHYT.Text))
            {
                Utility.ShowMsg("Bạn phải nhập mã quyền lợi(mức hưởng) BHYT", "Thông báo");
                txtMaQuyenloi_BHYT.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNoiDongtrusoKCBBD.Text))
            {
                Utility.ShowMsg("Bạn phải nhập Mã tỉnh, thành phố trực thuộc Trung ương, nơi phát hành thẻ BHYT", "Thông báo");
                txtNoiDongtrusoKCBBD.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtOthu4.Text))
            {
                Utility.ShowMsg("Bạn phải nhập nơi đăng ký ô thứ 4  cho bảo hiểm không bỏ trống", "Thông báo");
                txtOthu4.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtOthu5.Text))
            {
                Utility.ShowMsg("Bạn phải nhập nơi đăng ký ô thứ 5  cho bảo hiểm không bỏ trống", "Thông báo");
                txtOthu5.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtOthu6.Text))
            {
                Utility.ShowMsg("Bạn phải nhập nơi đăng ký ô thứ 6  cho bảo hiểm không bỏ trống", "Thông báo");
                txtOthu6.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNoiphattheBHYT.Text))
            {
                Utility.ShowMsg("Bạn phải nhập mã tỉnh, thành phố trực thuộc Trung ương, nơi cơ sở KCB ban đầu đóng trụ sở", "Thông báo",
                                MessageBoxIcon.Information);
                txtNoiphattheBHYT.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNoiDKKCBBD.Text))
            {
                Utility.ShowMsg("Bạn phải nhập số thứ tự cơ sở KCB BHYT",
                                "Thông báo");
                txtNoiDKKCBBD.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtClinicName.Text))
            {
                Utility.ShowMsg("Nơi đăng ký khám chữa bệnh ban đầu chưa tồn tại trong hệ thống", "Thông báo");
                var newItem = new frm_ThemnoiKCBBD();
                newItem.m_dtDataThanhPho = globalVariables.gv_dtDmucDiachinh;
                newItem.SetInfor(txtNoiDKKCBBD.Text, txtNoiphattheBHYT.Text);
                if (newItem.ShowDialog() == DialogResult.OK)
                {
                    txtNoiDKKCBBD.Text = "";
                    txtNoiphattheBHYT.Text = "";
                    txtNoiDKKCBBD.Text = newItem.txtMa.Text.Trim();
                    txtNoiphattheBHYT.Text = newItem.txtMaThanhPho.Text.Trim();
                    dtInsFromDate.Focus();
                }
                return false;
            }
            if (dtInsToDate.Value < dtInsFromDate.Value)
            {
                Utility.ShowMsg("Ngày hết hạn thẻ BHYT phải lớn hơn hoặc bằng ngày đăng ký thẻ BHYT", "Thông báo");
                dtInsToDate.Focus();
                return false;
            }
            if (dtInsFromDate.Value > globalVariables.SysDate)
            {
                Utility.ShowMsg("Ngày bắt đầu thẻ BHYT không thể lớn hơn ngày hiện tại", "Thông báo");
                dtInsFromDate.Focus();
                return false;
            }
            if (Convert.ToDateTime(dtInsToDate.Value.ToString("dd/MM/yyyy")) < Convert.ToDateTime(globalVariables.SysDate.ToString("dd/MM/yyyy")))
            {
                Utility.ShowMsg("Ngày hết hạn thẻ BHYT phải lớn hơn hoặc bằng ngày hiện tại", "Thông báo");
                dtInsToDate.Focus();
                return false;
            }
            return true;
        }

        private void  ModifyCommand()
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

        /// <summary>
        ///  Thêm mới PatietCode khi thêm ới dữ liệu
        /// </summary>
        /// <summary>
        /// Hàm 
        /// </summary>
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

        private void PerformAction()
        {
            switch (MEnAction)
            {
                case action.Update:
                    if (!InValiExistsBn()) return;
                    UpdatePatient();
                    break;
                case action.Insert:
                    InsertPatient();
                    break;
                case action.Add:
                    ThemLanKham();
                    break;
            }
           
            ModifyCommand();
            ModifyButtonCommandRegExam();
        }

        private bool InValiExistsBn()
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaLankham.Text))
                {
                    Utility.ShowMsg("Mã lần khám không bỏ trống", "Thông báo", MessageBoxIcon.Error);
                    txtMaLankham.Focus();
                    txtMaLankham.SelectAll();
                    return false;
                }
                SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema)
                    .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(txtMaLankham.Text));
                if (sqlQuery.GetRecordCount() <= 0)
                {
                    Utility.ShowMsg("Mã lần khám này không tồn tại trong CSDL,Mời bạn xem lại", "Thông báo",
                                    MessageBoxIcon.Error);
                    txtMaLankham.Focus();
                    txtMaLankham.SelectAll();
                    return false;
                }
                //Kiểm tra xem có thay đổi phần trăm BHYT
                if (Utility.DecimaltoDbnull(_objLuotkham.PtramBhyt, 0) != Utility.DecimaltoDbnull(txtPtramBHYT.Text))
                {
                    KcbThanhtoanCollection _lstthanhtoan = new Select().From(KcbThanhtoan.Schema)
                        .Where(KcbThanhtoan.Columns.IdBenhnhan).IsEqualTo(_objLuotkham.IdBenhnhan)
                        .And(KcbThanhtoan.Columns.MaLuotkham).IsEqualTo(_objLuotkham.MaLuotkham)
                        .And(KcbThanhtoan.Columns.PtramBhyt).IsEqualTo(Utility.DecimaltoDbnull(_objLuotkham.PtramBhyt, 0))
                        .ExecuteAsCollection<KcbThanhtoanCollection>();
                    if (_lstthanhtoan.Count > 0)
                    {
                        Utility.ShowMsg(string.Format("Bệnh nhân này đã thanh toán với mức BHYT {0}. Do đó hệ thống không cho phép bạn thay đổi phần trăm BHYT.\nMuốn thay đổi đề nghị bạn hủy hết các thanh toán", Utility.DecimaltoDbnull(_objLuotkham.PtramBhyt, 0).ToString()));
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi kiểm tra hợp lệ dữ liệu trước khi cập nhật Bệnh nhân", ex);
                return false;
            }
        }

        private string getLowerValue(string _value)
        {
            string reval = "";
            string[] arrWords = _value.Trim().ToLower().Split(' ');
            foreach (string word in arrWords)
            {
                if (word.Trim() != "")
                    reval += word + " ";
            }
            return reval.Trim();
        }

        private string GetShortCut(string fullForm)
        {
            try
            {
                string shortcut = "";
                string realName = fullForm.Trim() + " " + Utility.Bodau(fullForm);
                string[] arrWords = realName.ToLower().Split(' ');
                string _space = "";
                foreach (string word in arrWords)
                {
                    if (word.Trim() != "")
                    {
                        _space += word + " ";
                    }
                }
                shortcut += _space; // +_Nospace;
                foreach (string word in arrWords)
                {
                    if (word.Trim() != "")
                        shortcut += word.Substring(0, 1);
                }
                return shortcut.Trim();
            }
            catch
            {
                return fullForm;
            }
        }

      

        private void txtTuoi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab) cboPatientSex.Focus();
        }

        void ChangeObjectRegion()
        {
            if (_objDoituongKcb == null) return;
            _idDoituongKcb = _objDoituongKcb.IdDoituongKcb;
            _idLoaidoituongKcb = _objDoituongKcb.IdLoaidoituongKcb;
            _tenDoituongKcb = _objDoituongKcb.TenDoituongKcb;
            _ptramBhytCu = _objDoituongKcb.PhantramTraituyen.Value;
            _ptramBhytGocCu = _ptramBhytCu;
            txtPtramBHYT.Text = _objDoituongKcb.PhantramTraituyen.ToString();
            txtptramDauthe.Text = _objDoituongKcb.PhantramTraituyen.ToString();
            if (_objDoituongKcb.IdLoaidoituongKcb == 0)//ĐỐi tượng BHYT
            {
                pnlBHYT.Enabled = true;
                lblPtram.Text = @"Mức hưởng:";
                TinhPtramBhyt();
                lblTuyenBHYT.Visible = true;
                NapThongtinDichvuKcb();
                txtMaDtuong_BHYT.SelectAll();
                txtMaDtuong_BHYT.Focus();
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEPDON_SUDUNG_QRCODE", "0", false) == "1")
                {
                    txtQRCode.Visible = true; 
                     txtQRCode.Focus();
                }
            }
            else//Đối tượng khác BHYT
            {
                lblTuyenBHYT.Visible = false;
                pnlBHYT.Enabled = false;
                lblPtram.Text = @"Mức hưởng:";
                XoathongtinBHYT(PropertyLib._KCBProperties.XoaBHYT);
                NapThongtinDichvuKcb();
                txtTEN_BN.Focus();
              //  txtQRCode.Visible = false;
            }
        }
        DmucDoituongkcb _objDoituongKcb = null;
        /// <summary>
        /// hàm thực hienej phím tắt của form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_KCB_DANGKY_CAPNHAT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                if (this.ActiveControl != null && this.ActiveControl.Name == txtTEN_BN.Name && Utility.DoTrim(txtTEN_BN.Text)!="")
                {
                    frm_DSACH_BN_TKIEM Timkiem_Benhnhan = new frm_DSACH_BN_TKIEM(Args,0);
                    Timkiem_Benhnhan.AutoSearch = true;
                    Timkiem_Benhnhan.FillAndSearchData(false, "", "", Utility.DoTrim(txtTEN_BN.Text), "", "", "-1");
                    if (Timkiem_Benhnhan.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        txtNoiDKKCBBD.Clear();
                        txtNoiphattheBHYT.Clear();
                        isAutoFinding = true;
                        FindPatient(Timkiem_Benhnhan.IdBenhnhan.ToString());
                        isAutoFinding = false;
                    }
                }
                else if (this.ActiveControl != null && this.ActiveControl.Name == txtCMT.Name && Utility.DoTrim(txtCMT.Text) != "")
                {
                    frm_DSACH_BN_TKIEM Timkiem_Benhnhan = new frm_DSACH_BN_TKIEM(Args,0);
                    Timkiem_Benhnhan.AutoSearch = true;
                    Timkiem_Benhnhan.FillAndSearchData(false, "", "", "", Utility.DoTrim(txtCMT.Text), "", "-1");
                    if (Timkiem_Benhnhan.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        txtNoiDKKCBBD.Clear();
                        txtNoiphattheBHYT.Clear();
                        isAutoFinding = true;
                        FindPatient(Timkiem_Benhnhan.IdBenhnhan.ToString());
                        isAutoFinding = false;
                    }
                }
                else if (this.ActiveControl != null && this.ActiveControl.Name == txtSoDT.Name && Utility.DoTrim(txtSoDT.Text) != "")
                {
                    frm_DSACH_BN_TKIEM Timkiem_Benhnhan = new frm_DSACH_BN_TKIEM(Args,0);
                    Timkiem_Benhnhan.AutoSearch = true;
                    Timkiem_Benhnhan.FillAndSearchData(false, "", "", "", "", Utility.DoTrim(txtSoDT.Text), "-1");
                    if (Timkiem_Benhnhan.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        txtNoiDKKCBBD.Clear();
                        txtNoiphattheBHYT.Clear();
                        isAutoFinding = true;
                        FindPatient(Timkiem_Benhnhan.IdBenhnhan.ToString());
                        isAutoFinding = false;
                    }
                }
                //else if (this.ActiveControl != null && this.ActiveControl.Name == txtSDT.Name && Utility.DoTrim(txtSDT.Text) != "")
                //{
                //    frm_DSACH_BN_TKIEM Timkiem_Benhnhan = new frm_DSACH_BN_TKIEM(Args, 0);
                //    Timkiem_Benhnhan.AutoSearch = true;
                //    Timkiem_Benhnhan.FillAndSearchData(false, "", "", "", "", Utility.DoTrim(txtSDT.Text), "-1");
                //    if (Timkiem_Benhnhan.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //    {
                //        txtNoiDKKCBBD.Clear();
                //        txtNoiphattheBHYT.Clear();
                //        isAutoFinding = true;
                //        FindPatient(Timkiem_Benhnhan.IdBenhnhan.ToString());
                //        isAutoFinding = false;
                //    }
                //}
                return;
            }

            if (e.Control && e.KeyCode == Keys.D)
            {
               
                _maDoituongKcb = "DV";
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _maDoituongKcb);
                txtQRCode.Visible = false;
                return;
            }
            if (e.Control && e.KeyCode == Keys.B)
            {
                _maDoituongKcb = "BHYT";
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _maDoituongKcb);
               
                return;
            }
            if (e.Control && e.KeyCode == Keys.Y)
            {
                _maDoituongKcb = "YC";
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _maDoituongKcb);

                return;
            }
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.P))
            {
                AllowTextChanged = false;
                txtDiachi._Text = txtDiachi_bhyt.Text;
                DiachiBNCu = DiachiBHYTCu;
                AllowTextChanged = true;
                return;
            }
            
            string ngay_kham = globalVariables.SysDate.ToString("dd/MM/yyyy");
            if (e.Control && e.KeyCode == Keys.K)
            {
                if (!NotPayment(txtMaBN.Text.Trim(), ref ngay_kham))
                {
                    MEnAction = action.Add;
                    SinhMaLanKham();
                    LaydanhsachdangkyKcb();
                    if (pnlChonphongkham.Visible)
                    {
                        txtExamtypeCode.Focus();
                        txtExamtypeCode.Select();
                    }
                    else
                    {
                        txtKieuKham.Focus();
                        txtKieuKham.Select();
                    }
                }
                else
                {
                    //nếu là ngày hiện tại thì đặt về trạng thái sửa
                    if (ngay_kham == globalVariables.SysDate.ToString("dd/MM/yyyy"))
                    {
                        Utility.ShowMsg(
                            "Bệnh nhân đang có lần khám chưa được thanh toán. Cần thanh toán hết các lần đến khám bệnh của Bệnh nhân trước khi thêm lần khám mới.Nhấn OK để hệ thống quay về trạng thái sửa thông tin BN");
                        MEnAction = action.Update;
                        AllowTextChanged = false;
                        LoadThongtinBenhnhan();
                        LaydanhsachdangkyKcb();
                        txtTEN_BN.Focus();
                    }
                    else //Không cho phép thêm lần khám khác nếu chưa thanh toán lần khám của ngày hôm trước
                    {
                        Utility.ShowMsg(
                            "Bệnh nhân đang có lần khám chưa được thanh toán. Cần thanh toán hết các lần đến khám bệnh của Bệnh nhân trước khi thêm lần khám mới. Nhấn OK để hệ thống chuyển về trạng thái thêm mới Bệnh nhân");
                        cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
                    }
                }
                return;
            }
            if (e.Control && e.KeyCode == Keys.F)
            {
                txtMaBN.SelectAll();
                txtMaBN.Focus();
            }
            
            if (e.KeyCode == Keys.F10) LoadThongTinChoKham();
            if (e.KeyCode == Keys.F1)
            {
                tabControl1.SelectedTab = tabControl1.TabPages[0];
                return;
            }
            if (e.KeyCode == Keys.F2)
            {
                tabControl1.SelectedTab = tabControl1.TabPages[1];
                return;
            }
            if (e.KeyCode == Keys.F11) Utility.ShowMsg(this.ActiveControl.Name);
            if (e.KeyCode == Keys.F4) cmdInPhieuKham.PerformClick();
            if (e.KeyCode == Keys.Escape && this.ActiveControl != null && this.ActiveControl.GetType()!=txtDantoc.GetType())
            {

                Close();
            }
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.T && e.Control) cmdThanhToanKham.PerformClick();
            // if(e.KeyCode==Keys.P&&e.Control)cmdSaveAndPrint.PerformClick();
            if (e.KeyCode == Keys.N && e.Control) cmdThemMoiBN.PerformClick();
            if (e.KeyCode == Keys.Enter)
            {
               // lnkRestoreIgnoreQMS.Enabled = false;
              //  SendKeys.Send("{TAB}");
                ProcessTabKey(true);
                // lnkRestoreIgnoreQMS.Enabled = PropertyLib._HISQMSProperties.IsQMS;
            }
        }

        private void txtTEN_BN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cmdSave.Enabled = Utility.DoTrim(txtTEN_BN.Text).Length > 0;
            }
            catch (Exception exception)
            {
            }
        }
       

        private void txtTuoi_Click(object sender, EventArgs e)
        {
        }

       


        private void txtMaQuyenloi_BHYT_Click(object sender, EventArgs e)
        {
        }

        private void lnkCungDC_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AllowTextChanged = false;
            txtDiachi._Text = txtDiachi_bhyt.Text;
            AllowTextChanged = true;
        }

        private void txtCMT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter && txtCMT.Text.Trim() != "")
            {
                FindPatientIDbyCMT(txtCMT.Text.Trim());
            }
        }

        private void txtMaDtuong_BHYT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                string noiDangky = txtNoiDongtrusoKCBBD.Text.Trim() + txtNoiDKKCBBD.Text.Trim();
                if (MA_BHYT.Length == 15 && noiDangky.Length == 5) 
                    FindPatientIDbyBhyt(MA_BHYT, noiDangky);
            }
        }

        private void txtMaQuyenloi_BHYT_KeyDown(object sender, KeyEventArgs e)
        {
            _hasjustpressBackKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                string noiDangky = txtNoiDongtrusoKCBBD.Text.Trim() + txtNoiDKKCBBD.Text.Trim();
                if (MA_BHYT.Length == 15 && noiDangky.Length == 5) FindPatientIDbyBhyt(MA_BHYT, noiDangky);
                return;
            }
            if (e.KeyCode == Keys.Back)
            {
                _hasjustpressBackKey = true;
                if (txtMaQuyenloi_BHYT.Text.Length <= 0)
                {
                    txtMaDtuong_BHYT.Focus();
                    txtMaDtuong_BHYT.Select(txtMaDtuong_BHYT.Text.Length, 0);
                }
                return;
            }
            if (txtMaQuyenloi_BHYT.Text.Length == 1 && (Char.IsDigit((char) e.KeyCode) || Char.IsLetter((char) e.KeyCode)))
            {
                if (txtNoiphattheBHYT.Text.Length > 0)
                {
                    // txtNoiDongtrusoKCBBD.Text = ((char)e.KeyCode).ToString() + txtNoiDongtrusoKCBBD.Text.Substring(1);
                    txtNoiphattheBHYT.Focus();
                    txtNoiphattheBHYT.SelectAll();
                }
                return;
            }
            
        }

        private void txtNoiDongtrusoKCBBD_KeyDown(object sender, KeyEventArgs e)
        {
            _hasjustpressBackKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                //Không cần tìm
                //string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                //                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                //if (MA_BHYT.Length == 15) FindPatientIDbyBHYT(MA_BHYT);
                //return;
            }
            else if (e.KeyCode == Keys.Back)
            {
                _hasjustpressBackKey = true;
                if (txtNoiDongtrusoKCBBD.Text.Length <= 0)
                {
                    txtOthu6.Focus();
                    txtOthu6.Select(txtOthu6.Text.Length, 0);
                }
            }
        }

        private void txtOthu4_KeyDown(object sender, KeyEventArgs e)
        {
            _hasjustpressBackKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                string noiDangky = txtNoiDongtrusoKCBBD.Text.Trim() + txtNoiDKKCBBD.Text.Trim();
                  if (MA_BHYT.Length == 15 && noiDangky.Length == 5)  FindPatientIDbyBhyt(MA_BHYT, noiDangky);
            }
            else if (e.KeyCode == Keys.Back)
            {
                _hasjustpressBackKey = true;
                if (txtOthu4.Text.Length <= 0)
                {
                    txtNoiphattheBHYT.Focus();
                    txtNoiphattheBHYT.Select(txtNoiphattheBHYT.Text.Length, 0);
                }
            }
        }

        private void txtOthu5_KeyDown(object sender, KeyEventArgs e)
        {
            _hasjustpressBackKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                string noiDangky = txtNoiDongtrusoKCBBD.Text.Trim() + txtNoiDKKCBBD.Text.Trim();
                if (MA_BHYT.Length == 15 && noiDangky.Length == 5) FindPatientIDbyBhyt(MA_BHYT, noiDangky);
            }
            else if (e.KeyCode == Keys.Back)
            {
                _hasjustpressBackKey = true;
                if (txtOthu5.Text.Length <= 0)
                {
                    txtOthu4.Focus();
                    txtOthu4.Select(txtOthu4.Text.Length, 0);
                }
            }
        }

        private void txtOthu6_KeyDown(object sender, KeyEventArgs e)
        {
            _hasjustpressBackKey = false;

            if (e.KeyCode == Keys.Enter)
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_KIEMTRA20KYTU_BHYT", "0", false) == "1")
                {
                    return;
                }
                string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() +
                                 txtNoiphattheBHYT.Text.Trim() +
                                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                string noiDangky = txtNoiDongtrusoKCBBD.Text.Trim() + txtNoiDKKCBBD.Text.Trim();
                if (MA_BHYT.Length == 15 && noiDangky.Length == 5) FindPatientIDbyBhyt(MA_BHYT, noiDangky);
            }
            else if (e.KeyCode == Keys.Back)
            {
                _hasjustpressBackKey = true;
                if (txtOthu6.Text.Length <= 0)
                {
                    txtOthu5.Focus();
                    txtOthu5.Select(txtOthu5.Text.Length, 0);
                }
            }
        }
        private void txtNoiDKKCBBD_KeyDown(object sender, KeyEventArgs e)
        {
            _hasjustpressBackKey = false;
            if (e.KeyCode == Keys.Enter)
            {

                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_KIEMTRA20KYTU_BHYT", "0", false) == "1")
                {

                    string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() +
                                     txtNoiphattheBHYT.Text.Trim() + txtOthu4.Text.Trim() + txtOthu5.Text.Trim() +
                                     txtOthu6.Text.Trim();
                    string noiDangky = txtNoiDongtrusoKCBBD.Text.Trim() + txtNoiDKKCBBD.Text.Trim();
                    if (MA_BHYT.Length == 15 && noiDangky.Length == 5) FindPatientIDbyBhyt(MA_BHYT, noiDangky);
                    return;
                }
            }
            if (e.KeyCode == Keys.Back)
            {
                _hasjustpressBackKey = true;
                if (txtNoiDKKCBBD.Text.Length <= 0)
                {
                    txtNoiDongtrusoKCBBD.Focus();
                    txtNoiDongtrusoKCBBD.Select(txtNoiDongtrusoKCBBD.Text.Length, 0);
                }
            }
        }
        private void lnkThem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var newItem = new frm_ThemnoiKCBBD();
            newItem.m_dtDataThanhPho = globalVariables.gv_dtDmucDiachinh;
            newItem.SetInfor(txtNoiDKKCBBD.Text, txtNoiphattheBHYT.Text);
            if (newItem.ShowDialog() == DialogResult.OK)
            {
                txtNoiDKKCBBD.Text = "";
                txtNoiphattheBHYT.Text = "";
                txtNoiDKKCBBD.Text = newItem.txtMa.Text.Trim();
                txtNoiphattheBHYT.Text = newItem.txtMaThanhPho.Text.Trim();
                dtInsFromDate.Focus();
            }
        }

        private void txtKieuKham_Enter(object sender, EventArgs e)
        {
            if (THU_VIEN_CHUNG.IsNgoaiGio())
            {
                this.Text= @"Bệnh nhân đang khám dịch vụ ngoài giờ";
            }
            
        }

        private void txtPhongkham_Enter(object sender, EventArgs e)
        {
            if (THU_VIEN_CHUNG.IsNgoaiGio())
            {
                this.Text = @"Bệnh nhân đang khám dịch vụ ngoài giờ";
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

        private void frm_KCB_DANGKY_CAPNHAT_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Utility.FreeLockObject(m_strMaluotkham);
                //Trả lại mã lượt khám nếu chưa được dùng đến
                ResetLuotkham();
                if (PropertyLib._HISQMSProperties.IsQMS)
                {
                    new Update(KcbQm.Schema)
                        .Set(KcbQm.Columns.TrangThai).EqualTo(0)
                        .Set(KcbQm.Columns.MaQuay).EqualTo(string.Empty)
                        .Where(KcbQm.Columns.TrangThai).IsEqualTo(1)
                        .And(KcbQm.Columns.MaQuay).IsEqualTo(PropertyLib._HISQMSProperties.MaQuay)
                        .AndExpression(KcbQm.Columns.MaDoituongKcb).IsEqualTo("ALL").Or(KcbQm.Columns.MaDoituongKcb).IsEqualTo(PropertyLib._HISQMSProperties.MaDoituongKCB).CloseExpression()
                        .And(KcbQm.Columns.UuTien).IsEqualTo(chkUuTien.Checked ? 1 : 0)
                        .And(KcbQm.Columns.LoaiQms).IsEqualTo(PropertyLib._HISQMSProperties.LoaiQMS)
                        .And(KcbQm.Columns.MaKhoakcb).IsEqualTo(globalVariables.MA_KHOA_THIEN)
                        .Execute();
                    if (_QMSScreen != null && (!isQMSActive(_QMSScreen.Name)))
                    {
                        _QMSScreen.Close();
                        _QMSScreen.Dispose();
                        _QMSScreen = null;
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }

        private void ShowQmsOnScreen2()
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
                            ShowQmsOnScreen2();
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
        void SetActionStatus()
        {
            lblStatus.Text = MEnAction == action.Insert ? "BỆNH NHÂN MỚI" : (MEnAction==action.Add?"THÊM LẦN KHÁM":"CẬP NHẬT");
        }
        private void CauHinhKCB()
        {
            
            dtpBOD.Value = globalVariables.SysDate;
            dtpBOD.CustomFormat = PropertyLib._KCBProperties.Nhapngaythangnamsinh? "dd/MM/yyyy HH:mm" : "yyyy";
            txtTuoi.Enabled = dtpBOD.CustomFormat =="yyyy";
            lblLoaituoi.Visible = dtpBOD.CustomFormat != "yyyy";
            mnuBOD.Checked = dtpBOD.CustomFormat != "yyyy";
            if (PropertyLib._KCBProperties != null)
            {
                chkTudongthemmoi.Checked = PropertyLib._KCBProperties.Tudongthemmoi;
                //cmdThanhToanKham.Enabled = PropertyLib._KCBProperties.Chophepthanhtoan;
                //cmdThanhToanKham.Visible = cmdThanhToanKham.Enabled;

                grdRegExam.RootTable.Columns["colThanhtoan"].Visible = PropertyLib._KCBProperties.Chophepthanhtoan &&
                                                                       (PropertyLib._KCBProperties.Kieuhienthi ==
                                                                        Kieuhienthi.Trenluoi ||
                                                                        PropertyLib._KCBProperties.Kieuhienthi ==
                                                                        Kieuhienthi.Cahai);
                grdRegExam.RootTable.Columns["colDelete"].Visible = PropertyLib._KCBProperties.Kieuhienthi ==
                                                                    Kieuhienthi.Trenluoi ||
                                                                    PropertyLib._KCBProperties.Kieuhienthi ==
                                                                    Kieuhienthi.Cahai;
                grdRegExam.RootTable.Columns["colIn"].Visible = PropertyLib._KCBProperties.Kieuhienthi ==
                                                                Kieuhienthi.Trenluoi ||
                                                                PropertyLib._KCBProperties.Kieuhienthi ==
                                                                Kieuhienthi.Cahai;
                //pnlnutchucnang.Visible = PropertyLib._KCBProperties.Kieuhienthi != Kieuhienthi.Trenluoi;
               // pnlnutchucnang.Height = PropertyLib._KCBProperties.Kieuhienthi == Kieuhienthi.Trenluoi ? 0 : 33;

                pnlChonKieukham.Visible = !PropertyLib._KCBProperties.GoMaDvu;
                pnlChonphongkham.Visible = PropertyLib._KCBProperties.GoMaDvu;

            }
        }
        bool currentQMS = false;
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

        /// <summary>
        /// hàm thực hiện việc stop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void cmdStart_Click(object sender, EventArgs e)
        {
            globalVariables.b_QMS_Stop = false;
            ShowQmsOnScreen2();
            ModifyQms();
        }

        private void txtSoDT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (pnlChonphongkham.Visible)
                {
                    txtExamtypeCode.Focus();
                    txtExamtypeCode.Select();
                }
                else
                {
                    txtKieuKham.Focus();
                    txtKieuKham.Select();
                }
        }

        private void lnkRestoreIgnoreQMS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new frm_SoKham_GoiLai();
            frm._OnActiveQMS += frm__OnActiveQMS;
            frm.ShowDialog();
        }

        void frm__OnActiveQMS()
        {
            try
            {
                blnManual = true;
                Utility.EnableButton(cmdGoiSoKham, false);
                Utility.WaitNow(this);
                new Update(KcbQm.Schema)
                    .Set(KcbQm.Columns.TrangThai).EqualTo(0)
                    .Set(KcbQm.Columns.MaQuay).EqualTo(string.Empty)
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

        #region "Sự kiện hiển thị phần số thứ tự trong QMS"

        private bool blnManual;

       

        //private bool b_HasScreenmonitor = false;
        /// <summary>
        /// hàm thực hiện việc số khám thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSoQMS_TextChanged(object sender, EventArgs e)
        {
            _QMSScreen.SetQmsValue(IdQMS, txtSoQMS.Text, chkUuTien.Checked ? 1 : 0);
        }

        /// <summary>
        /// hàm thực hiện việc gọi số 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                Thread.Sleep(Utility.Int32Dbnull(1000 * PropertyLib._HISQMSProperties.ThoiGianCho,100));
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


        /// <summary>
        /// hàm thực hiện việc xóa thông tin số khám hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        int QMS_IdDichvuKcb = 0;
        long IdQMS = 0;
        /// <summary>
        /// hàm thực hiện việc lấy số khám của bệnh nhân
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
                            _KCB_QMS.LaySoKhamQMS(PropertyLib._HISQMSProperties.MaQuay, globalVariables.MA_KHOA_THIEN,
                                PropertyLib._HISQMSProperties.MaDoituongKCB, ref sokham, ref QMS_IdDichvuKcb, ref IdQMS,
                                (byte) 0, "TIEPDON", PropertyLib._HISQMSProperties.LoaiQMS_bo);
                        }
                        else//Các khoa khác
                        {
                            int isUuTien = 0;

                            if (PropertyLib._HISQMSProperties.Chopheplaysouutien)
                            {
                                SqlQuery sqlQuery1 = new Select().From(KcbQm.Schema)
                                    .Where(KcbQm.Columns.MaKhoakcb).IsEqualTo(globalVariables.MA_KHOA_THIEN)
                                    .And(KcbQm.Columns.TrangThai).In(0, 1)
                                    .AndExpression(KcbQm.Columns.MaDoituongKcb)
                                    .IsEqualTo("ALL")
                                    .Or(KcbQm.Columns.MaDoituongKcb)
                                    .IsEqualTo(PropertyLib._HISQMSProperties.MaDoituongKCB)
                                    .CloseExpression()
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
                            //_KCB_QMS.LaySoKhamQMS(PropertyLib._HISQMSProperties.MaQuay, globalVariables.MA_KHOA_THIEN, PropertyLib._HISQMSProperties.MaDoituongKCB, ref sokham, ref QMS_IdDichvuKcb, ref IdQMS,(byte) isUuTien,PropertyLib._HISQMSProperties.LoaiQMS, PropertyLib._HISQMSProperties.LoaiQMS_bo);

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
                    StoredProcedure sp = SPs.QmsGetQMSCount(globalVariables.MA_KHOA_THIEN, PropertyLib._HISQMSProperties.MaDoituongKCB, tongso, PropertyLib._HISQMSProperties.LoaiQMS,0);
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
            catch(Exception ex)
            {
            }
        }

        #endregion

        #region "Su kien autocomplete của thành phố"

        private bool AllowTextChanged;
        private string _rowFilter = "1=1";


        #endregion

        #region "Su kien autocomplete của quận huyện"

        private string _rowFilterQuanHuyen = "1=1";

       

        private void AutoLoadKieuKham()
        {
            try
            {
                if ((Utility.Int32Dbnull(txtIDKieuKham.Text, -1) == -1 || Utility.Int32Dbnull(txtIDPkham.Text, -1) == -1) && !pnlChonKieukham.Visible )
                {
                    cboKieuKham.Text = @"CHỌN PHÒNG KHÁM";
                    cboKieuKham.SelectedIndex = -1;
                    return;
                }
                //if (Utility.Int32Dbnull(txtKieuKham.MyID, -1) == -1 && pnlChonKieukham.Visible )
                //{
                //    //txtKieuKham.SetId(-1);
                //    txtKieuKham.Focus();
                //    return;
                //}
                //if ( Utility.Int32Dbnull(txtPhongkham.MyID, -1) == -1 && pnlChonKieukham.Visible)
                //{
                //    //txtPhongkham.SetId(-1);
                //    txtPhongkham.Focus();
                //    return;
                //}
                DataRow[] arrDr = null;
                if (!pnlChonKieukham.Visible)
                {
                       arrDr =
                    m_dtDanhsachDichvuKCB.Select("(ma_doituong_kcb='ALL' OR ma_doituong_kcb='" + MA_DTUONG + "') AND id_kieukham= " +
                                                  txtIDKieuKham.Text.Trim() + " AND  id_phongkham = " + txtIDPkham.Text.Trim());
                       if (arrDr.Length <= 0)
                           arrDr =
                          m_dtDanhsachDichvuKCB.Select("(ma_doituong_kcb='ALL' OR ma_doituong_kcb='" + MA_DTUONG + "') AND id_kieukham=" +
                                                         txtIDKieuKham.Text.Trim() + " AND id_phongkham = '-1' ");
                }
                else
                {
                     arrDr =
                m_dtDanhsachDichvuKCB.Select("(ma_doituong_kcb='ALL' OR ma_doituong_kcb='" + MA_DTUONG + "') AND id_kieukham= " +
                                             Utility.sDbnull(txtKieuKham.MyID) + " AND  id_phongkham = " + Utility.sDbnull(txtPhongkham.MyID));
                }
                //nếu ko có đích danh phòng thì lấy dịch vụ bất kỳ theo kiểu khám và đối tượng
             
                if (arrDr.Length <= 0)
                {
                    cboKieuKham.Text = @"CHỌN PHÒNG KHÁM";
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
                Utility.ShowMsg("Lỗi:"+ exception.Message);
            }
            finally
            {
                AutoLoad = false;
            }
        }

       

      

     

        #region Diachi

        private bool DiachiBHYTCu;
        private bool DiachiBNCu;

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

       

        #endregion

        #region Diachi the BHYT

       
        private void TudongthemDiachinh_test(string value)
        {
            //Tạm thời khoa lại tìm giải pháp khác trực quan hơn
            return;
            bool success = false;
            bool added = false;
            string[] arrvalues = value.Split(',');
            if (arrvalues.Length != 3) return;
            string TenTP = arrvalues[2];
            string TenQH = arrvalues[1];
            string TenXP = arrvalues[0];
            string CodeTP = "";
            string CodeQH = "";
            string CodeXP = "";

            string SurveyCodeTP = Utility.GetYYMMDDHHMMSS(globalVariables.SysDate);
            string SurveyCodeQH = SurveyCodeTP + "1";
            string SurveyCodeXP = SurveyCodeQH + "1";
            DmucDiachinh _TP = null;
            var _newTP = new DmucDiachinh();

            DmucDiachinh _QH = null;
            var _newQH = new DmucDiachinh();

            DmucDiachinh _XP = null;
            var _newXP = new DmucDiachinh();

            string ShortCutXP = "kx";
            string ShortCutTP = "kx";
            string ShortCutQH = "kx";
            try
            {
                _TP =
                    new Select().From(DmucDiachinh.Schema).Where(DmucDiachinh.TenDiachinhColumn).IsEqualTo(TenTP).ExecuteSingle
                        <DmucDiachinh>();

                if (_TP == null)
                {
                    _newTP.MaDiachinh = SurveyCodeTP;
                    _newTP.TenDiachinh = TenTP;
                    _newTP.SttHthi = 1;
                    _newTP.LoaiDiachinh = 0;
                    _newTP.MotaThem = getshortcut(Utility.Bodau(BoTp(0, TenTP)));
                    ShortCutTP = _newTP.MotaThem;
                }
                else
                {
                    CodeTP = _TP.MaDiachinh;
                    ShortCutTP = _TP.MotaThem;
                }
                SqlQuery sqlQueryQH = new Select().From(DmucDiachinh.Schema).Where(DmucDiachinh.TenDiachinhColumn).IsEqualTo(TenQH);
                if (_TP != null)
                {
                    sqlQueryQH.And(DmucDiachinh.MaChaColumn).IsEqualTo(_TP.MaDiachinh);
                    _QH = sqlQueryQH.ExecuteSingle<DmucDiachinh>();
                }
                else
                    _QH = null;

                if (_QH == null)
                {
                    _newQH.MaDiachinh = SurveyCodeQH;
                    _newQH.TenDiachinh = TenQH;
                    _newQH.SttHthi = 1;
                    _newQH.LoaiDiachinh = 1;
                    _newQH.MotaThem = getshortcut(Utility.Bodau(BoTp(1, TenQH)));
                    ShortCutQH = _newQH.MotaThem;
                }
                else
                {
                    CodeQH = _QH.MaDiachinh;
                    ShortCutQH = _QH.MotaThem;
                }
                SqlQuery sqlQueryXP = new Select().From(DmucDiachinh.Schema).Where(DmucDiachinh.TenDiachinhColumn).IsEqualTo(TenXP);
                if (_QH != null)
                {
                    sqlQueryXP.And(DmucDiachinh.MaChaColumn).IsEqualTo(_QH.MaDiachinh);
                    _XP = sqlQueryXP.ExecuteSingle<DmucDiachinh>();
                }
                else
                    _XP = null;

                if (_XP == null)
                {
                    _newXP.MaDiachinh = SurveyCodeXP;
                    _newXP.TenDiachinh = TenXP;
                    _newXP.SttHthi = 1;
                    _newXP.LoaiDiachinh = 2;
                    _newXP.MotaThem = getshortcut(Utility.Bodau(BoTp(2, TenXP)));
                    ShortCutXP = _newXP.MotaThem;
                }
                else
                {
                    CodeXP = _XP.MaDiachinh;
                    ShortCutXP = _XP.MotaThem;
                }

                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        int maxCode = 0;
                        QueryCommand cmd = DmucDiachinh.CreateQuery().BuildCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandSql = "select MAX(ma_diachinh) from dmuc_diachinh where ISNUMERIC(ma_diachinh)=1";
                        DataTable temdt = DataService.GetDataSet(cmd).Tables[0];
                        maxCode = Utility.Int32Dbnull(temdt.Rows[0][0], 0);
                        if (_TP == null)
                        {
                            added = true;
                            _newTP.MaDiachinh = (maxCode + 1).ToString();
                            _newTP.Save();
                            CodeTP = _newTP.MaDiachinh;
                        }
                        if (_QH == null)
                        {
                            added = true;
                            _newQH.MaDiachinh = (maxCode + 2).ToString();
                            _newQH.MaCha = CodeTP;
                            _newQH.Save();
                            CodeQH = _newQH.MaDiachinh;
                        }
                        if (_XP == null)
                        {
                            added = true;
                            _newXP.MaDiachinh = (maxCode + 3).ToString();
                            _newXP.MaCha = CodeQH;
                            _newXP.Save();
                        }
                    }
                    scope.Complete();
                    success = true;
                }
            }
            catch
            {
            }
            if (success && added) //Thêm vào Datatable để không có thể sử dụng luôn
            {
                DataRow drShortcut = m_DC.NewRow();
                drShortcut["ShortCutXP"] = ShortCutXP;
                drShortcut["ShortCutQH"] = ShortCutQH;
                drShortcut["ShortCutTP"] = ShortCutTP;
                drShortcut["Value"] = value;
                m_DC.Rows.Add(drShortcut);
            }
        }

        #endregion

       

        #endregion

        #region "Sự kiện bắt cho phần khám bệnh"

        /// <summary>
        /// hàm thực hiện việc in phiếu khám chữa bệnh cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        private void InPhieuKCB()
        {
          
                int reg_id = -1;
                 string tieude="", reportname = "";
                 //VMS.HISLink.Report.Report.tiepdon_PHIEUKHAM_NHIET crpt = new VMS.HISLink.Report.Report.tiepdon_PHIEUKHAM_NHIET();
                ReportDocument crpt = Utility.GetReport("tiepdon_PHIEUKHAM_NHIET",ref tieude,ref reportname);
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
                KcbDangkyKcb objCongkham = KcbDangkyKcb.FetchByID(reg_id);
                DmucKhoaphong lDepartment = DmucKhoaphong.FetchByID(objCongkham.IdPhongkham);
                Utility.SetParameterValue(crpt,"PHONGKHAM", Utility.sDbnull(lDepartment.MaKhoaphong));
                Utility.SetParameterValue(crpt,"STT", Utility.sDbnull(objCongkham.SttKham, ""));
                Utility.SetParameterValue(crpt,"BENHAN", txtMaLankham.Text);
                Utility.SetParameterValue(crpt,"TENBN", txtTEN_BN.Text);
                Utility.SetParameterValue(crpt,"GT_TUOI", cboPatientSex.Text + " - " + txtTuoi.Text + " tuổi");
                string SOTHE = "Không có thẻ";
                string HANTHE = "Không có hạn";
                LaySoTheBhyt();
                if (pnlBHYT.Enabled)
                {
                    SOTHE = SoBHYT;
                    HANTHE = dtInsToDate.Value.ToString("dd/MM/yyyy");
                }
                   
                Utility.SetParameterValue(crpt,"SOTHE", SOTHE);
                Utility.SetParameterValue(crpt,"HANTHE",HANTHE);
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

        private void InphieuKham()
        {
            int reg_id = GetrealRegID();
            KcbDangkyKcb objCongkham = KcbDangkyKcb.FetchByID(reg_id);
            if (objCongkham != null)
            {
                new Update(KcbDangkyKcb.Schema)
                    .Set(KcbDangkyKcb.Columns.TrangthaiIn).EqualTo(1)
                    .Set(KcbDangkyKcb.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                    .Set(KcbDangkyKcb.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                    .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objCongkham.IdKham).Execute();
                IEnumerable<GridEXRow> query = from kham in grdRegExam.GetDataRows()
                                               where
                                                   kham.RowType == RowType.Record &&
                                                   Utility.Int32Dbnull(kham.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1) ==
                                                   Utility.Int32Dbnull(objCongkham.IdKham)
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
                if (_objLuotkham == null)
                    _objLuotkham = TaoLuotkham();
                if (_objLuotkham != null)
                    KcbInphieu.INPHIEU_KHAM(Utility.sDbnull(_objLuotkham.MaDoituongKcb), v_dtData,
                                                  "PHIẾU KHÁM BỆNH", PropertyLib._MayInProperties.CoGiayInPhieuKCB == Papersize.A5 ? "A5" : "A4");
            }
        }

        private bool CheckBeforeDelete()
        {
            if (!Utility.isValidGrid(grdRegExam)) return false;
            if (grdRegExam.CurrentRow == null) return false;
            int vRegId = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1);
            KcbDangkyKcb objCongkham = KcbDangkyKcb.FetchByID(vRegId);
            if (objCongkham != null)
            {
                SqlQuery sqlQuery = new Select().From(KcbDangkyKcb.Schema)
                    .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objCongkham.IdKham)
                    .And(KcbDangkyKcb.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Đăng ký khám đang chọn đã thanh toán, Bạn không thể xóa", "Thông báo");
                    grdRegExam.Focus();
                    return false;
                }
                if (objCongkham.IdKham <= 0) return true;

                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEPDON_XOATHONGTINLANKHAM_FULL", "0", false) == "1")
                {
                    SqlQuery q =
                        new Select().From(KcbChidinhclsChitiet.Schema).Where(KcbChidinhclsChitiet.Columns.IdKham).IsEqualTo(
                            objCongkham.IdKham).And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                    if (q.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Đăng ký khám đang chọn đã được bác sĩ chỉ định CLS và đã được thanh toán. Yêu cầu Hủy thanh toán các chỉ định CLS trước khi hủy phòng khám", "Thông báo");
                        grdRegExam.Focus();
                        return false;
                    }
                    SqlQuery qPres =
                        new Select().From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.Columns.IdKham).IsEqualTo(
                            objCongkham.IdKham).And(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                    if (qPres.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Đăng ký khám đang chọn đã được bác sĩ kê đơn thuốc và đã được thanh toán. Yêu cầu hủy thanh toán đơn thuốc trước khi hủy phòng khám", "Thông báo");
                        grdRegExam.Focus();
                        return false;
                    }
                }
                else//Nếu có chỉ định CLS hoặc thuốc thì không cho phép xóa
                {
                    SqlQuery q =
                        new Select().From(KcbChidinhcl.Schema).Where(KcbChidinhcl.Columns.IdKham).IsEqualTo(
                            objCongkham.IdKham);
                    if (q.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Đăng ký khám đang chọn đã được bác sĩ chỉ định CLS. Yêu cầu xóa chỉ định CLS trước khi hủy phòng khám", "Thông báo");
                        grdRegExam.Focus();
                        return false;
                    }
                    SqlQuery qPres =
                        new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.Columns.IdKham).IsEqualTo(objCongkham.IdKham);
                    if (qPres.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Đăng ký khám đang chọn đã được bác sĩ kê đơn thuốc. Yêu cầu xóa đơn thuốc trước khi hủy phòng khám", "Thông báo");
                        grdRegExam.Focus();
                        return false;
                    }
                }
            }
            return true;
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
                            DataRow[] arrDr =
                                m_dtDangkyPhongkham.Select("id_kham=" + v_RegId + " OR  " +
                                                           KcbDangkyKcb.IdChaColumn.ColumnName + "=" + v_RegId);


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
                            Utility.ShowMsg(
                                "Bạn thực hiện xóa dịch vụ khám không thành công. Liên hệ đơn vị cung cấp phần mềm để được trợ giúp",
                                "Thông báo");
                            break;
                    }
                }
            }
            ModifyButtonCommandRegExam();
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
        private void HuyThanhtoan()
        {
            try
            {
                string ma_lydohuy = "";
                if (!Utility.isValidGrid(grdRegExam)) return;

                if (_objLuotkham == null)
                {
                    _objLuotkham = TaoLuotkham();
                }
                if (_objLuotkham == null)
                {
                    Utility.ShowMsg("Không lấy được thông tin bệnh nhân dựa vào dữ liệu trên lưới danh sách bệnh nhân. Liên hệ bộ phận IT để được trợ giúp");
                    return;
                }
               
                if (Utility.Int32Dbnull( _objLuotkham.TrangthaiNoitru,0) >= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
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
                        frm.objLuotkham = _objLuotkham;
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
                        ActionResult actionResult = new KCB_THANHTOAN().HuyThanhtoan(KcbThanhtoan.FetchByID(v_intIdThanhtoan), _objLuotkham, ma_lydohuy, Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbThanhtoan.Columns.IdHdonLog], -1), HUYTHANHTOAN_HUYBIENLAI);
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
                if (_objLuotkham == null)
                    _objLuotkham = TaoLuotkham();
                if (Utility.Int32Dbnull(_objLuotkham.TrangthaiNoitru, 0) >= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
                {
                    Utility.ShowMsg("Bệnh nhân này đã ở trạng thái nội trú nên hệ thống không cho phép thanh toán ngoại trú nữa");
                    return;
                }
                KcbThanhtoan objPayment = CreatePayment();
                List<int> lstRegID = new List<int>();
                decimal TTBN_Chitrathucsu = 0;
                string ErrMsg = "";
                ActionResult actionResult = new KCB_THANHTOAN().ThanhtoanChiphiDvuKcb(objPayment, _objLuotkham,null, Taodulieuthanhtoanchitiet(ref lstRegID).ToList<KcbThanhtoanChitiet>(),null,
                                                   ref Payment_Id, -1, false, false, "", ref TTBN_Chitrathucsu, ref ErrMsg);

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
                        if (PropertyLib._MayInProperties.TudonginhoadonSaukhiThanhtoan && TTBN_Chitrathucsu>0)
                        {
                            int KCB_THANHTOAN_KIEUINHOADON = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_KIEUINHOADONTUDONG_SAUKHITHANHTOAN", "1", false));
                            if (KCB_THANHTOAN_KIEUINHOADON == 1 || KCB_THANHTOAN_KIEUINHOADON == 3)
                                InHoadon(Payment_Id);
                            if (KCB_THANHTOAN_KIEUINHOADON == 2 || KCB_THANHTOAN_KIEUINHOADON == 3)
                                new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, Payment_Id,-1, _objLuotkham, 0);
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


        void ThanhtoanSoKham(bool askbeforepayment)
        {
            try
            {
               
                if (PropertyLib._KCBProperties.Hoitruockhithanhtoan)
                    if (askbeforepayment)
                        if (!Utility.AcceptQuestion("Bạn có muốn thực hiện việc thanh toán sổ khám bệnh cho bệnh nhân không ?",
                                                   "Thông báo", true))
                            return;

                long Payment_Id = -1;
                if (_objLuotkham == null)
                    _objLuotkham = TaoLuotkham();
                if (Utility.Int32Dbnull(_objLuotkham.TrangthaiNoitru, 0) >= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
                {
                    Utility.ShowMsg("Bệnh nhân này đã ở trạng thái nội trú nên hệ thống không cho phép thanh toán ngoại trú nữa");
                    return;
                }
                KcbThanhtoan objPayment = CreatePayment();
                List<int> lstRegID = new List<int>();
                decimal TTBN_Chitrathucsu = 0;
               KcbThanhtoanChitiet objTemp=Taodulieuthanhtoansokham();
               if (objTemp == null)
               {
                   return;
               }
               string ErrMsg = "";
               ActionResult actionResult = new KCB_THANHTOAN().ThanhtoanChiphiDvuKcb(objPayment, _objLuotkham,null, new List<KcbThanhtoanChitiet>() { objTemp },null,
                                                   ref Payment_Id, -1, false, false, "", ref TTBN_Chitrathucsu, ref ErrMsg);

                switch (actionResult)
                {
                    case ActionResult.Success:
                        if (objPayment.IdThanhtoan != Payment_Id)
                        {
                            Payment_Id = Utility.Int64Dbnull(objPayment.IdThanhtoan);
                        }

                        Payment_Id = Utility.Int64Dbnull(objPayment.IdThanhtoan);
                        if (PropertyLib._MayInProperties.TudonginhoadonSaukhiThanhtoan && TTBN_Chitrathucsu > 0)
                        {
                            int KCB_THANHTOAN_KIEUINHOADON = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_KIEUINHOADONTUDONG_SAUKHITHANHTOAN", "1", false));
                            if (KCB_THANHTOAN_KIEUINHOADON == 1 || KCB_THANHTOAN_KIEUINHOADON == 3)
                                InHoadon(Payment_Id);
                            if (KCB_THANHTOAN_KIEUINHOADON == 2 || KCB_THANHTOAN_KIEUINHOADON == 3)
                                new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, Payment_Id,-1, _objLuotkham,0);
                        }
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình thanh toán tiền sổ khám", "Thông báo");
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
            }
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
                newItem.PtramBhyt = _objLuotkham.PtramBhyt.Value;
                newItem.PtramBhytGoc = _objLuotkham.PtramBhytGoc.Value;
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
                newItem.MaDoituongKcb =_maDoituongKcb;
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

        KcbThanhtoanChitiet Taodulieuthanhtoansokham()
        {
            KcbThanhtoanChitiet newItem = null;
            //Tiền sổ KCB
            KcbDangkySokham objDangkySokham = TaosoKCB();
            KcbDangkySokham temp = new Select().From(KcbDangkySokham.Schema).Where(KcbDangkySokham.Columns.IdBenhnhan).IsEqualTo(_objLuotkham.IdBenhnhan)
                              .And(KcbDangkySokham.Columns.MaLuotkham).IsEqualTo(_objLuotkham.MaLuotkham)
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
                    newItem.MaDoituongKcb = _maDoituongKcb;
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
        private void grdRegExam_SelectionChanged(object sender, EventArgs e)
        {
            ModifyButtonCommandRegExam();
        }
        private void grdRegExam_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "colDelete")
            {
                if (!CheckBeforeDelete())
                    return;
                HuyThamKham();
            }
            if (e.Column.Key == "colIn")
            {
                InPhieu();
            }
            if (e.Column.Key == "colThanhtoan")
            {
                if (Utility.sDbnull(cmdThanhToanKham.Tag, "ABCD") == "TT")
                    Thanhtoan(true);
                else
                    HuyThanhtoan();
            }
        }
       
        #endregion

        #region "khởi tạo sự kiện để lưu lại thông tin của bệnh nhân"

        private string mavuasinh = "";

        private void ThemMoiLanKhamVaoLuoi()
        {
            DataRow dr = m_dtPatient.NewRow();
            dr[KcbDanhsachBenhnhan.Columns.IdBenhnhan] = Utility.sDbnull(txtMaBN.Text, "-1");
            dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan] = Utility.sDbnull(txtTEN_BN.Text, "");

            dr[KcbDanhsachBenhnhan.Columns.GioiTinh] = cboPatientSex.Text;
            dr[KcbDanhsachBenhnhan.Columns.IdGioitinh] =Utility.Int16Dbnull( cboPatientSex.SelectedValue);

            dr[KcbDanhsachBenhnhan.Columns.NamSinh] =dtpBOD.Value.Year.ToString();
            
            dr[KcbDanhsachBenhnhan.Columns.Email] = Utility.sDbnull(txtEmail.Text, "");
            dr[KcbDanhsachBenhnhan.Columns.DiachiBhyt] = Utility.sDbnull(txtDiachi_bhyt.Text, "");
            dr[KcbDanhsachBenhnhan.Columns.DiaChi] = Utility.sDbnull(txtDiachi.Text, "");



            dr["Tuoi"] = Utility.Int32Dbnull(txtTuoi.Text, 0); ;// globalVariables.SysDate.Year - Utility.Int32Dbnull(txtNamSinh.Text, 0);


            dr[KcbDanhsachBenhnhan.Columns.NguoiTao] = globalVariables.UserName;
            dr[KcbDanhsachBenhnhan.Columns.NgheNghiep] = txtNgheNghiep.Text;

           
            dr[KcbDanhsachBenhnhan.Columns.DienThoai] = Utility.sDbnull(txtSoDT.Text, "");
           
            dr[KcbDanhsachBenhnhan.Columns.Cmt] = Utility.sDbnull(txtCMT.Text, "");
            dr[KcbLuotkham.Columns.MaLuotkham] = Utility.sDbnull(txtMaLankham.Text, "");
            DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(_idDoituongKcb);
            if (objectType != null)
                dr[KcbLuotkham.Columns.MaDoituongKcb] = Utility.sDbnull(objectType.MaDoituongKcb, "");
            //SỬA LẠI PHẦN LOAD THÔNG TIN TRIỆU CHỨNG BAN ĐẦU
            dr[KcbLuotkham.Columns.TrieuChung] = Utility.sDbnull(txtTrieuChungBD.Text, "");
            dr[KcbLuotkham.Columns.IdDoituongKcb] = _idDoituongKcb;
            dr[DmucDoituongkcb.Columns.TenDoituongKcb] = _tenDoituongKcb;
            dr[KcbLuotkham.Columns.MaKcbbd] = Utility.sDbnull(txtNoiDKKCBBD.Text, "");
            dr[DmucNoiKCBBD.Columns.TenKcbbd] = txtClinicName.Text;// lblClinicName.Text;

            dr[KcbLuotkham.Columns.TrangthaiNoitru] = 0;
            dr[KcbLuotkham.Columns.TrangthaiNgoaitru] = 0;

            dr[KcbLuotkham.Columns.Locked] = 0;

            dr[KcbLuotkham.Columns.NgayTiepdon] = dtCreateDate.Value;
            dr["sNgay_tiepdon"] = dtCreateDate.Value; 
            dr["Loai_benhnhan"] = "Ngoại trú";
            dr[KcbLuotkham.Columns.MatheBhyt] = Laymathe_BHYT();
            dr[KcbLuotkham.Columns.NgaybatdauBhyt] = dtInsFromDate.Value;
            dr[KcbLuotkham.Columns.NgayketthucBhyt] = dtInsToDate.Value;
            dr[KcbLuotkham.Columns.TthaiChuyenden] = chkChuyenVien.Checked ? 1 : 0;
            dr["Noicap_KCBBD"] = Utility.sDbnull(txtNoiDongtrusoKCBBD.Text + txtNoiDKKCBBD.Text, "");
            dr["mathe_bhyt_full"] = mathe_bhyt_full();
            dr["ten_bhyt"] = THU_VIEN_CHUNG.IsBaoHiem(_idLoaidoituongKcb)
                             ? chkTraiTuyen.Checked ? "Trái tuyến" : "Đúng tuyến"
                             : "";

            dr[KcbLuotkham.Columns.TrangthaiCapcuu] = chkCapCuu.Checked ? 1 : 0;
            m_dtPatient.Rows.InsertAt(dr, 0);
        }

        private void UpdateBNVaoTrenLuoi()
        {
            EnumerableRowCollection<DataRow> query = from bn in m_dtPatient.AsEnumerable()
                                                     where
                                                         Utility.sDbnull(bn[KcbLuotkham.Columns.MaLuotkham]) ==
                                                         txtMaLankham.Text
                                                     select bn;
            if (query.Count() > 0)
            {
                DataRow dr = query.FirstOrDefault();
                dr[KcbDanhsachBenhnhan.Columns.IdBenhnhan] = Utility.sDbnull(txtMaBN.Text, "-1");
                dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan] = Utility.sDbnull(txtTEN_BN.Text, "");
                dr[KcbDanhsachBenhnhan.Columns.GioiTinh] = cboPatientSex.Text;

                dr["gioi_tinh"] = cboPatientSex.Text;
                dr[KcbDanhsachBenhnhan.Columns.IdGioitinh] =Utility.Int16Dbnull( cboPatientSex.SelectedValue);

                dr[KcbDanhsachBenhnhan.Columns.NamSinh] =dtpBOD.Value.Year.ToString();
               
                dr[KcbDanhsachBenhnhan.Columns.Email] = Utility.sDbnull(txtEmail.Text, "");
                dr[KcbDanhsachBenhnhan.Columns.DiachiBhyt] = Utility.sDbnull(txtDiachi_bhyt.Text, "");
                dr[KcbDanhsachBenhnhan.Columns.DiaChi] = Utility.sDbnull(txtDiachi.Text, "");




                dr["Tuoi"] = Utility.Int32Dbnull(txtTuoi.Text, 0); ;// globalVariables.SysDate.Year - Utility.Int32Dbnull(txtNamSinh.Text, 0);


                dr[KcbDanhsachBenhnhan.Columns.NguoiTao] = globalVariables.UserName;
                dr[KcbDanhsachBenhnhan.Columns.NgheNghiep] = txtNgheNghiep.Text;

                
                dr[KcbDanhsachBenhnhan.Columns.DienThoai] = Utility.sDbnull(txtSoDT.Text, "");
                
                DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(_idDoituongKcb);
                if (objectType != null)
                {
                    dr[KcbLuotkham.Columns.MaDoituongKcb] = Utility.sDbnull(objectType.MaDoituongKcb, "");
                }

             

                dr[KcbLuotkham.Columns.MaLuotkham] = Utility.sDbnull(txtMaLankham.Text, "");
                //SỬA LẠI PHẦN LOAD THÔNG TIN TRIỆU CHỨNG BAN ĐẦU
                dr[KcbLuotkham.Columns.TrieuChung] = Utility.sDbnull(txtTrieuChungBD.Text, "");
                dr[KcbLuotkham.Columns.IdDoituongKcb] = _idDoituongKcb;
                dr[DmucDoituongkcb.Columns.TenDoituongKcb] = _tenDoituongKcb;


                dr[KcbLuotkham.Columns.MaKcbbd] = Utility.sDbnull(txtNoiDKKCBBD.Text, "");
                dr[DmucNoiKCBBD.Columns.TenKcbbd] = txtClinicName.Text; // lblClinicName.Text;

                dr[KcbLuotkham.Columns.TrangthaiNoitru] = 0;
                dr[KcbLuotkham.Columns.TrangthaiNgoaitru] = 0;

                dr[KcbLuotkham.Columns.Locked] = 0;

                dr[KcbLuotkham.Columns.NgayTiepdon] = dtCreateDate.Value;
                dr["sNgay_tiepdon"] = dtCreateDate.Value; //globalVariables.SysDate;
                //globalVariables.SysDate;
                dr[KcbLuotkham.Columns.MatheBhyt] = Laymathe_BHYT();
                dr[KcbLuotkham.Columns.NgaybatdauBhyt] = dtInsFromDate.Text;
                dr[KcbLuotkham.Columns.NgayketthucBhyt] = dtInsToDate.Text;
                dr[KcbLuotkham.Columns.TthaiChuyenden] = chkChuyenVien.Checked ? 1 : 0;
                dr["Noicap_KCBBD"] = Utility.sDbnull(txtNoiDongtrusoKCBBD.Text + txtNoiDKKCBBD.Text, "");
                dr["mathe_bhyt_full"] = mathe_bhyt_full();
                dr["ten_bhyt"] = THU_VIEN_CHUNG.IsBaoHiem(_idLoaidoituongKcb)
                                 ? chkTraiTuyen.Checked ? "Trái tuyến" : "Đúng tuyến"
                                 : "";

                dr[KcbLuotkham.Columns.TrangthaiCapcuu] = chkCapCuu.Checked ? 1 : 0;
                m_dtPatient.AcceptChanges();
            }
        }

        private void ThemLanKham()
        {
            
            objBenhnhan = TaoBenhnhan();
            _objLuotkham = TaoLuotkham();
            KcbDangkyKcb objCongkham = TaoDangkyKcb();
            KcbDangkySokham objSokham = TaosoKCB();
            long v_id_kham = -1;
            string msg = "";
            errorProvider1.Clear();
            ActionResult actionResult = _kcbDangky.ThemmoiLuotkham(this.myTrace, objBenhnhan, _objLuotkham, objCongkham,null,objSokham,
                                                                             Utility.Int32Dbnull(cboKieuKham.Value, -1), ref v_id_kham, ref msg);

            if (msg.Trim() != "")
            {
                errorProvider1.SetError(txtSoKcb, msg);
            }
            switch (actionResult)
            {
                case ActionResult.Success:
                   
                    _ptramBhytCu = Utility.DecimaltoDbnull(txtPtramBHYT.Text, 0);
                    _ptramBhytGocCu = Utility.DecimaltoDbnull(txtptramDauthe.Text, 0);
                    txtMaLankham.Text = Utility.sDbnull(_objLuotkham.MaLuotkham);
                    txtMaBN.Text = Utility.sDbnull(_objLuotkham.IdBenhnhan);
                    UpdateStatusQMS(objCongkham);
                    m_blnHasJustInsert = true;
                    MEnAction = action.Update;
                    LaydanhsachdangkyKcb();
                    setMsg(uiStatusBar1.Panels["MSG"], "Bạn thêm mới lần khám bệnh nhân thành công", false);
                    ThemMoiLanKhamVaoLuoi();
                    cmdSave.Enabled = false;
                    if (_OnActionSuccess != null) _OnActionSuccess();
                    if (objCongkham != null)
                    {
                        Utility.GotoNewRowJanus(grdRegExam, KcbDangkyKcb.Columns.IdKham, Utility.sDbnull(objCongkham.IdKham));
                        if (PropertyLib._MayInProperties.InPhieuKCBsaukhiluu)
                        {
                            InPhieu();
                        }
                        else
                        {
                            cmdInPhieuKham.Focus();
                        }
                    }
                    if (Utility.Byte2Bool(_objDoituongKcb.TudongThanhtoan)) Thanhtoan(false);
                    LoadThongTinChoKham();
                    Utility.GotoNewRowJanus(grdList, KcbLuotkham.Columns.MaLuotkham, txtMaLankham.Text);
                    Utility.GotoNewRowJanus(grdRegExam, KcbDangkyKcb.Columns.IdKham, v_id_kham.ToString());
                     cboKieuKham.Text = "CHỌN PHÒNG KHÁM";
                    cboKieuKham.SelectedIndex = -1;
                    txtKieuKham.ClearMe();
                    txtPhongkham.ClearMe();
                    cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
                    MBlnCancel = false;
                    this.Close();
                    break;
                case ActionResult.Error:
                    setMsg(uiStatusBar1.Panels["MSG"], "Lỗi trong quá trình thêm lần khám !", true);
                    cmdSave.Focus();
                    break;
            }
        }
        private KcbDangkyKcb TaoDangkyKcb()
        {
            bool b_HasKham = false;
            int id_kieukham = Utility.Int32Dbnull(txtKieuKham.MyID);
            EnumerableRowCollection<DataRow> query = from phong in m_dtDangkyPhongkham.AsEnumerable().Cast<DataRow>()
                                                     where
                                                         Utility.Int32Dbnull(phong[KcbDangkyKcb.Columns.IdDichvuKcb],
                                                                             -100) ==
                                                         Utility.Int32Dbnull(cboKieuKham.Value, -1)
                                                     select phong;
            if (query.Count() > 0)
            {
                Utility.ShowMsg("Bệnh nhân đã đăng ký dịch vụ khám này. Đề nghị bạn xem lại");
                b_HasKham = true;
            }
            else
            {
                b_HasKham = false;
            }

            if (!b_HasKham)
            {
                var objCongkham = new KcbDangkyKcb();
                int min = 1;
                int max = 255;
                //List<int> levels = m_dtDangkyPhongkham.AsEnumerable().Select(al => al.Field<int>(KcbDangkyKcb.Columns.SttTt37)).Distinct().ToList();
                // min = levels.Min();
                // max = levels.Max();
                ////Hoặc
                min = Convert.ToInt32(m_dtDangkyPhongkham.AsEnumerable()
                       .Min(row => row[KcbDangkyKcb.Columns.SttTt37]));
                max = Convert.ToInt32(m_dtDangkyPhongkham.AsEnumerable()
                       .Max(row => row[KcbDangkyKcb.Columns.SttTt37]));
               // KcbQm objQMS = KcbQm.FetchByID(IdQMS);
                DmucDichvukcb objDichvuKcb = DmucDichvukcb.FetchByID(Utility.Int32Dbnull(cboKieuKham.Value));

                _objDoituongKcb =
                    new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(
                        _maDoituongKcb).ExecuteSingle<DmucDoituongkcb>();

                DmucKhoaphong objdepartment =
                    new Select().From(DmucKhoaphong.Schema).Where(DmucKhoaphong.IdKhoaphongColumn).IsEqualTo(
                        Utility.Int16Dbnull(txtIDPkham.Text, -1)).ExecuteSingle<DmucKhoaphong>();
                if (objDichvuKcb != null)
                {
                    objCongkham.IdDichvuKcb = Utility.Int16Dbnull(objDichvuKcb.IdDichvukcb, -1);
                    objCongkham.IdKieukham = objDichvuKcb.IdKieukham;
                    objCongkham.NhomBaocao = objDichvuKcb.NhomBaocao;
                    objCongkham.DonGia = Utility.DecimaltoDbnull(objDichvuKcb.DonGia, 0);
                    if (min > 1)
                        objCongkham.SttTt37 = Utility.ByteDbnull(min - 1);
                    else
                        objCongkham.SttTt37 = Utility.ByteDbnull(max + 1);
                    objCongkham.TyleTt = THU_VIEN_CHUNG.Bhyt_Laytyle_tt_congkham(THU_VIEN_CHUNG.IsBaoHiem(_objLuotkham.IdLoaidoituongKcb), objCongkham.SttTt37);
                    objCongkham.MadoituongGia = _maDoituongKcb;
                    objCongkham.NguoiTao = globalVariables.UserName;
                    objCongkham.LaPhidichvukemtheo = 0;
                    objCongkham.SttKham = -1;
                    objCongkham.SttKham = THU_VIEN_CHUNG.LaySothutuKCB(Utility.Int32Dbnull(objDichvuKcb.IdPhongkham, -1));
                    objCongkham.IdCha = -1;
                    if (objdepartment != null)
                    {
                        objCongkham.IdKhoakcb = objdepartment.MaCha;
                        objCongkham.MaPhongStt = objdepartment.MaPhongStt;
                    }
                    if (_objDoituongKcb != null)
                    {
                        objCongkham.IdLoaidoituongkcb = _objDoituongKcb.IdLoaidoituongKcb;
                        objCongkham.MaDoituongkcb = _objDoituongKcb.MaDoituongKcb;
                        objCongkham.IdDoituongkcb = _objDoituongKcb.IdDoituongKcb;
                    }
                    if (Utility.Int16Dbnull(objDichvuKcb.IdPhongkham, -1) > -1)
                        objCongkham.IdPhongkham = Utility.Int16Dbnull(objDichvuKcb.IdPhongkham, -1);
                    else
                        objCongkham.IdPhongkham = Utility.Int16Dbnull(txtIDPkham.Text, -1);
                   
                    objCongkham.PhuThu = !chkTraiTuyen.Checked
                                                    ? Utility.DecimaltoDbnull(objDichvuKcb.PhuthuDungtuyen)
                                                    : Utility.DecimaltoDbnull(objDichvuKcb.PhuthuTraituyen);

                    if (!THU_VIEN_CHUNG.IsBaoHiem(objCongkham.IdLoaidoituongkcb))
                        objCongkham.PhuThu = 0;
                    objCongkham.NgayDangky = globalVariables.SysDate;
                    objCongkham.IdBenhnhan = Utility.Int32Dbnull(txtMaBN.Text, -1);
                    objCongkham.TrangthaiThanhtoan = 0;
                    objCongkham.TrangthaiHuy = 0;
                    objCongkham.Noitru = 0;
                    objCongkham.TrangthaiIn = 0;
                    objCongkham.IpMaytao = globalVariables.gv_strIPAddress;
                    objCongkham.TenMaytao = globalVariables.gv_strComputerName;

                    objCongkham.TuTuc =Utility.ByteDbnull( objDichvuKcb.TuTuc,0);
                    if (pnlBHYT.Enabled && chkTraiTuyen.Checked && THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_TRAITUYENNGOAITRU_GIADICHVU", false) == "1")
                        objCongkham.TuTuc = 1;
                    objCongkham.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
                    objCongkham.TenDichvuKcb = cboKieuKham.Text;
                    objCongkham.NgayTiepdon = globalVariables.SysDate;
                    objCongkham.MaLuotkham = Utility.sDbnull(txtMaLankham.Text, "");
                    //Bỏ đi do sinh lại ở mục business
                    if (THU_VIEN_CHUNG.IsNgoaiGio())
                    {
                        objCongkham.KhamNgoaigio = 1;
                        objCongkham.DonGia = Utility.DecimaltoDbnull(objDichvuKcb.DongiaNgoaigio, 0);
                        objCongkham.PhuThu = chkTraiTuyen.Checked ? Utility.DecimaltoDbnull(objDichvuKcb.PhuthuNgoaigio, 0) : Utility.DecimaltoDbnull(objDichvuKcb.PhuthuDungtuyen);
                    }
                    else
                    {
                        objCongkham.KhamNgoaigio = 0;
                    }
                }
                else
                {
                    objCongkham = null;
                }
                //if (THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_TINHGIAKHAM_THONGTU37", "0", false) == "1" && m_dtDangkyPhongkham.Rows.Count > 0 )
                //{
                //    THU_VIEN_CHUNG.TinhlaiGiaChiphiKcb(m_dtDangkyPhongkham, ref objCongkham);
                //}
                return objCongkham;
            }


            return null;
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

        private bool isValidIdentifyNum()
        {
            try
            {
                if (txtCMT.Text.Trim() == "") return true;
                string sql = "";
                QueryCommand cmd = KcbDanhsachBenhnhan.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                sql =
                    "Select cmt,id_benhnhan,ten_benhnhan,gioi_tinh from kcb_danhsach_benhnhan ";
                sql += " where cmt = '" + txtCMT.Text.Trim() + "'";
                if (MEnAction == action.Insert)
                    sql += "";
                else //Là update hoặc thêm mới lần khám cần kiểm tra có trùng với BN khác chưa
                    sql += " AND id_benhnhan <> " + txtMaBN.Text.Trim();
                cmd.CommandSql = sql;
                DataTable temdt = DataService.GetDataSet(cmd).Tables[0];
                if (temdt.Rows.Count > 0)
                {
                    Utility.ShowMsg(
                        string.Format("Số CMT này đang được sử dụng cho Bệnh nhân {0}:{1}\nMời bạn kiểm tra lại",
                                      temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan], temdt.Rows[0]["ten_benhnhan"]));
                    txtCMT.Focus();
                    return false;
                }
                return temdt.Rows.Count <= 0;
            }
            catch
            {
                return false;
            }
        }

        private void UpdateStatusQMS(KcbDangkyKcb objDangkyKcb)
        {
            if (PropertyLib._HISQMSProperties.TestMode ||( PropertyLib._HISQMSProperties.IsQMS && b_HasSecondScreen))
            {
               
                new Update(KcbQm.Schema)
                    .Set(KcbQm.Columns.TrangThai).EqualTo(2)
                    .Set(KcbQm.Columns.MaLankham).EqualTo(txtMaLankham.Text)
                    .Set(KcbQm.Columns.IdBenhnhan).EqualTo(Utility.Int32Dbnull(txtMaBN.Text))
                    .Set(KcbQm.Columns.IdPhongkham).EqualTo(Utility.Int32Dbnull(objDangkyKcb.IdPhongkham))
                    .Set(KcbQm.Columns.IdKhoakcb).EqualTo(Utility.Int32Dbnull(objDangkyKcb.IdKhoakcb))
                    .Set(KcbQm.Columns.IdDichvukcb).EqualTo(Utility.Int32Dbnull(objDangkyKcb.IdDichvuKcb))
                    .Set(KcbQm.Columns.IdKieukham).EqualTo(Utility.Int32Dbnull(objDangkyKcb.IdKieukham))
                    .Where(KcbQm.Columns.SoQms).IsEqualTo(Utility.Int32Dbnull(txtSoQMS.Text))
                    .And(KcbQm.Columns.MaQuay).IsEqualTo(PropertyLib._HISQMSProperties.MaQuay)
                    .And(KcbQm.Columns.TrangThai).IsEqualTo(1)
                    .AndExpression(KcbQm.Columns.MaDoituongKcb).IsEqualTo("ALL").Or(KcbQm.Columns.MaDoituongKcb).IsEqualTo(PropertyLib._HISQMSProperties.MaDoituongKCB).CloseExpression()
                    .And(KcbQm.Columns.UuTien).IsEqualTo(chkUuTien.Checked ? 1 : 0)
                    .And(KcbQm.Columns.LoaiQms).IsEqualTo(PropertyLib._HISQMSProperties.LoaiQMS)
                    .And(KcbQm.Columns.MaKhoakcb).IsEqualTo(globalVariables.MA_KHOA_THIEN)
                    .Execute();
                LaySokham(2);
            }
        }

        private void InsertPatient()
        {
            objBenhnhan = TaoBenhnhan();
            _objLuotkham = TaoLuotkham();
            KcbDangkyKcb objCongkham = TaoDangkyKcb();
            KcbDangkySokham objSokham = TaosoKCB();
            long v_id_kham = -1;
            string msg = "";
            
            errorProvider1.Clear();
            ActionResult actionResult = _kcbDangky.ThemmoiBenhnhan(this.myTrace, objBenhnhan, _objLuotkham, objCongkham,null, objSokham,
                                                                            Utility.Int32Dbnull(cboKieuKham.Value, -1), ref v_id_kham, ref msg);

            if (msg.Trim() != "")
            {
                errorProvider1.SetError(txtSoKcb, msg);
            }
            switch (actionResult)
            {
                case ActionResult.Success:
                    objBenhnhan = KcbDanhsachBenhnhan.FetchByID(objBenhnhan.IdBenhnhan);
                    objBenhnhan.FirstNoigioithieu = _objLuotkham.NoiGioithieu;
                    objBenhnhan.LastNoigioithieu = _objLuotkham.NoiGioithieu;
                    objBenhnhan.IsNew = false;
                    objBenhnhan.MarkOld();
                    objBenhnhan.Save();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới bệnh nhân ID={0}, PID={1}, Tên={2} ", _objLuotkham.IdBenhnhan.ToString(), _objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan), newaction.Insert, this.GetType().Assembly.ManifestModule.Name);
                    _ptramBhytCu = Utility.DecimaltoDbnull(txtPtramBHYT.Text, 0);
                    _ptramBhytGocCu = Utility.DecimaltoDbnull(txtptramDauthe.Text, 0);
                    txtMaLankham.Text = Utility.sDbnull(_objLuotkham.MaLuotkham);
                    txtMaBN.Text = Utility.sDbnull(_objLuotkham.IdBenhnhan);
                    mavuasinh = Utility.sDbnull(_objLuotkham.IdBenhnhan);
                    UpdateStatusQMS(objCongkham);
                    MEnAction = action.Update;
                    m_blnHasJustInsert = true;
                    m_strMaluotkham = txtMaLankham.Text;
                    LaydanhsachdangkyKcb();
                    ThemMoiLanKhamVaoLuoi();
                    if (_OnActionSuccess != null) _OnActionSuccess();
                    setMsg(uiStatusBar1.Panels["MSG"], "Bạn thêm mới bệnh nhân thành công", false);
                    Utility.GotoNewRowJanus(grdList, KcbLuotkham.Columns.MaLuotkham, txtMaLankham.Text);
                    Utility.GotoNewRowJanus(grdRegExam, KcbDangkyKcb.Columns.IdKham, v_id_kham.ToString());
                    cmdSave.Enabled = false;
                    MBlnCancel = false;
                    if (objCongkham != null)
                    {
                        Utility.GotoNewRowJanus(grdRegExam, KcbDangkyKcb.Columns.IdKham, Utility.sDbnull(objCongkham.IdKham));
                        if (PropertyLib._MayInProperties.InPhieuKCBsaukhiluu && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEPDON_INPHIEUKCB", "0", false) == "1")
                        {
                            InPhieu();
                        }
                        else
                        {
                            cmdInPhieuKham.Focus();
                        }
                    }
                    if (Utility.Byte2Bool(_objDoituongKcb.TudongThanhtoan)) Thanhtoan(false);
                    cboKieuKham.Text = @"CHỌN PHÒNG KHÁM";
                    cboKieuKham.SelectedIndex = -1;
                    txtKieuKham.ClearMe();
                    txtPhongkham.ClearMe();
                    if (PropertyLib._KCBProperties.Tudongthemmoi) cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
                    txtMaBN.Text = Utility.sDbnull(mavuasinh);
                    LoadThongTinChoKham();
                    break;
                case ActionResult.Error:
                    setMsg(uiStatusBar1.Panels["MSG"], "Bạn thực hiện thêm dữ liệu không thành công !", true);
                    cmdSave.Focus();
                    break;
            }
        }
       
        private void THEM_PHI_DVU_KYC(KcbLuotkham objLuotkham)
        {
            //DmucDichvukcb lexam =
            //    DmucDichvukcb.FetchByID(Utility.Int32Dbnull(cboKieuKham.Value));
            //if (lexam != null)
            //{
            //    if (Utility.Int32Dbnull(lexam.IdPhikemtheo, -1) > 0)
            //    {
            //        SqlQuery sql = new Select().From(KcbChidinhcl.Schema).Where(KcbChidinhcl.Columns.MaLuotkham).
            //            IsEqualTo(objLuotkham.MaLuotkham)
            //            .And(KcbChidinhcl.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(
            //                KcbChidinhcl.Columns.IsPHIDvuKtheo).IsEqualTo(1);
            //        if (sql.GetRecordCount() > 0)
            //        {
            //            return;
            //        }
            //        int IdDv = -1;
            //        string[] Ma_UuTien = globalVariables.MA_UuTien.Split(',');
            //        if (globalVariables.MA_KHOA_THIEN != "KYC")
            //        {
            //            if (THU_VIEN_CHUNG.IsNgoaiGio())
            //            {
            //                IdDv = Utility.Int32Dbnull(lexam.IdPhiKtheoNgio, -1);
            //            }
            //            else
            //            {
            //                if (!Ma_UuTien.Contains(Utility.sDbnull(txtMaQuyenloi_BHYT.Text)))
            //                {
            //                    IdDv = Utility.Int32Dbnull(lexam.IdPhiKtheo, -1);
            //                }
            //                else
            //                {
            //                    IdDv = -1;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            IdDv = Utility.Int32Dbnull(lexam.IdPhiKtheo, -1);
            //        }
            //        LServiceDetail lServiceDetail = LServiceDetail.FetchByID(IdDv);
            //        if (lServiceDetail != null)
            //        {
            //            var objAssignInfo = new KcbChidinhcl();
            //            objAssignInfo.IdKham = -1;
            //            objAssignInfo.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham, "");
            //            objAssignInfo.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan, "");
            //            objAssignInfo.ServiceId = -1;
            //            objAssignInfo.ServiceTypeId = -1;
            //            objAssignInfo.RegDate = globalVariables.SysDate;
            //            objAssignInfo.DepartmentId = globalVariables.DepartmentID;
            //            objAssignInfo.TrangthaiThanhtoan = 0;
            //            objAssignInfo.CreatedBy = globalVariables.UserName;
            //            objAssignInfo.NgayTao = globalVariables.SysDate;
            //            objAssignInfo.Actived = 0;
            //            objAssignInfo.MaKhoaThien = globalVariables.MA_KHOA_THIEN;
            //            objAssignInfo.NoiTru = 0;
            //            objAssignInfoIdDoituongKcb = _IdDoituongKcb;
            //            objAssignInfo.DiagPerson = globalVariables.gv_StaffID;
            //            objAssignInfo.DepartmentId = globalVariables.DepartmentID;
            //            objAssignInfo.IsPHIDvuKtheo = 1;
            //            objAssignInfo.IsNew = true;
            //            objAssignInfo.Save();

                      

            //            var objAssignDetail = new TAssignDetail();
            //            objAssignDetail.IdKham = -1;
            //            objAssignDetail.AssignId = objAssignInfo.AssignId;
            //            objAssignDetail.ServiceId = lServiceDetail.ServiceId;
            //            objAssignDetail.ServiceDetailId = lServiceDetail.ServiceDetailId;
            //            objAssignDetail.DiscountPrice = 0;
            //            objAssignDetail.PtramBhyt = 0;
            //            objAssignDetail.DiscountType = 0;
            //            objAssignDetail.OriginPrice = Utility.DecimaltoDbnull(lServiceDetail.Price, 0);
            //            objAssignDetail.DiscountPrice = Utility.DecimaltoDbnull(lServiceDetail.Price, 0);
            //            objAssignDetail.SurchargePrice = 0;
            //            objAssignDetail.UserId = globalVariables.UserName;
            //            objAssignDetail.AssignTypeId = 0;
            //            objAssignDetail.NgayTiepdon = globalVariables.SysDate;
            //            objAssignDetail.TrangthaiThanhtoan = 0;
            //            objAssignDetail.IsPayment = (byte?) (Utility.sDbnull(objLuotkham.MaDoituongKcb) == "DV" ? 0 : 1);
            //            objAssignDetail.Quantity = 1;
            //            objAssignDetail.AssignDetailStatus = 0;
            //            objAssignDetail.SDesc = "Chi phí đi kèm thêm phòng khám khi đăng ký khám bệnh theo yêu cầu";
            //            objAssignDetail.BhytStatus = 0;
            //            objAssignDetail.DisplayOnReport = 1;
            //            objAssignDetail.GiaBhytCt = 0;
            //            objAssignDetail.GiaBnct = Utility.DecimaltoDbnull(lServiceDetail.Price, 0);
            //            objAssignDetail.IpMayTao = globalVariables.IpAddress;
            //            objAssignDetail.IpMacTao = globalVariables.IpMacAddress;
            //            objAssignDetail.ChoPhepIn = 0;
            //            objAssignDetail.AssignDetailStatus = 0;
            //            objAssignDetail.DiagPerson = globalVariables.gv_StaffID;
            //            objAssignDetailIdDoituongKcb = _IdDoituongKcb;
            //            objAssignDetail.Stt = 1;
            //            objAssignDetail.IsNew = true;
            //            objAssignDetail.Save();
            //        }
            //    }
            //}
        }

        private void UpdatePatient()
        {
            objBenhnhan = TaoBenhnhan();
            _objLuotkham = TaoLuotkham();
            KcbDangkyKcb objCongkham = TaoDangkyKcb();
            KcbDangkySokham objSokham = TaosoKCB();
            string msg = "";
            errorProvider1.Clear();
            Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin bệnh nhân ID={0}, PID={1}, Tên={2} ", _objLuotkham.IdBenhnhan.ToString(), _objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
            ActionResult actionResult = _kcbDangky.UpdateLanKham(this.myTrace, objBenhnhan, _objLuotkham, null, null,null,
                                                                         Utility.Int32Dbnull(cboKieuKham.Value, -1), _ptramBhytCu, _ptramBhytGocCu, ref msg);
            // THEM_PHI_DVU_KYC(objLuotkham);
            if (msg.Trim() != "")
            {
                errorProvider1.SetError(txtSoKcb, msg);
            }
            switch (actionResult)
            {
                case ActionResult.Success:
                   
                    //gọi lại nếu thay đổi địa chỉ
                    m_blnHasJustInsert = false;
                    _ptramBhytCu = Utility.DecimaltoDbnull(txtPtramBHYT.Text, 0);
                    _ptramBhytGocCu = Utility.DecimaltoDbnull(txtptramDauthe.Text, 0);
                    setMsg(uiStatusBar1.Panels["MSG"], "Bạn sửa thông tin Bệnh nhân thành công", false);
                    LaydanhsachdangkyKcb();
                    UpdateBNVaoTrenLuoi();
                    cmdSave.Enabled = false;
                    if (_OnActionSuccess != null) _OnActionSuccess();
                    if (objCongkham != null)
                    {
                        Utility.GotoNewRowJanus(grdRegExam, KcbDangkyKcb.Columns.IdKham, Utility.sDbnull(objCongkham.IdKham));
                        if (PropertyLib._MayInProperties.InPhieuKCBsaukhiluu)
                        {
                            InPhieu();
                        }
                        else
                        {
                            cmdInPhieuKham.Focus();
                        }
                    }
                    if (string.IsNullOrEmpty(_objLuotkham.MatheBhyt))
                    {
                        dtInsFromDate.Value = globalVariables.SysDate;
                        dtInsToDate.Value = globalVariables.SysDate;
                        txtPtramBHYT.Text = "";
                        txtptramDauthe.Text = "";
                        txtMaDtuong_BHYT.Clear();
                        txtMaQuyenloi_BHYT.Clear();
                        txtNoiDongtrusoKCBBD.Clear();
                        txtOthu4.Clear();
                        txtOthu5.Clear();
                        txtOthu6.Clear();
                        chkTraiTuyen.Checked = false;
                        lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
                        chkChuyenVien.Checked = false;
                        txtNoiphattheBHYT.Clear();
                        txtNoiDKKCBBD.Clear();
                    }
                    LoadThongTinChoKham();
                    Utility.GotoNewRowJanus(grdList, KcbLuotkham.Columns.MaLuotkham, txtMaLankham.Text);
                    MBlnCancel = false;
                    this.Close();
                    break;
                case ActionResult.Error:
                    setMsg(uiStatusBar1.Panels["MSG"], "Bạn thực hiện sửa thông tin không thành công !", true);
                    break;
                case ActionResult.Cancel:
                    Utility.ShowMsg(string.Format( "Bệnh nhân này đã thanh toán một số dịch vụ nên bạn không được phép chuyển đối tượng hoặc thay đổi phần trăm BHYT\nPhần trăm cũ {0} % - Phần trăm mới {1} %",_ptramBhytCu.ToString(),txtPtramBHYT.Text),"Cảnh báo");
                    break;
            }
        }

        /// <summary>
        /// Insert dữ liệu khi thêm mới hoàn toàn
        /// </summary>hàm chen du lieu moi tin day, benhnhan kham benh moi tinh
        private KcbDanhsachBenhnhan TaoBenhnhan()
        {

            if (objBenhnhan == null)
            {
                objBenhnhan = new KcbDanhsachBenhnhan();
                objBenhnhan.IsNew = true;
            }
            if (MEnAction != action.Insert)
            {
                objBenhnhan = KcbDanhsachBenhnhan.FetchByID(Utility.Int64Dbnull(txtMaBN.Text, -1));
                objBenhnhan.IsNew = false;
            }
            objBenhnhan.TenBenhnhan = txtTEN_BN.Text;
            if (string.IsNullOrEmpty(txtdiachichitiet.Text.Trim()))
            {
                objBenhnhan.DiaChi =txtDiachi.Text;
            }
            else
            {
                objBenhnhan.DiaChi = txtdiachichitiet.Text + ", " + txtDiachi.Text;
            }
            objBenhnhan.KieuBenhnhan = 0;
            objBenhnhan.DiachiBhyt = Utility.sDbnull(txtDiachi_bhyt.Text);
            objBenhnhan.DienThoai = txtSoDT.Text;
            objBenhnhan.Email = Utility.sDbnull(txtEmail.Text, "");
            objBenhnhan.SoTiemchungQg = Utility.sDbnull(txtSoBATCQG.Text, "");
            objBenhnhan.NguoiLienhe = Utility.sDbnull(txtNguoiLienhe.Text);
            objBenhnhan.NgayTao = globalVariables.SysDate;
            objBenhnhan.NguoiTao = globalVariables.UserName;
            objBenhnhan.NguonGoc = "KCB";
            objBenhnhan.Cmt = Utility.sDbnull(txtCMT.Text, "");
            objBenhnhan.CoQuan = string.Empty;
            objBenhnhan.NgheNghiep = txtNgheNghiep.Text;
            objBenhnhan.GioiTinh = cboPatientSex.Text;
            objBenhnhan.IdGioitinh = Utility.ByteDbnull(cboPatientSex.SelectedValue, 0);
            objBenhnhan.NamSinh =  Utility.Int16Dbnull(dtpBOD.Value.Year);
            objBenhnhan.KieuBenhnhan = 0;//0= Bệnh nhân thường đến khám chữa bệnh;1= Người gửi mẫu kiểm nghiệm cá nhân;2= Tổ chức gửi mẫu kiểm nghiệm
            objBenhnhan.DanToc = Utility.sDbnull(txtDantoc.myCode, "01");
            objBenhnhan.NgaySinh = dtpBOD.Value;

            if (MEnAction == action.Insert)
            {
                objBenhnhan.NgayTiepdon = dtCreateDate.Value;
                objBenhnhan.NguoiTao = globalVariables.UserName;
                objBenhnhan.IpMaytao = globalVariables.gv_strIPAddress;
                objBenhnhan.TenMaytao = globalVariables.gv_strComputerName;
            }
            if (MEnAction == action.Update)
            {
                objBenhnhan.NgaySua = globalVariables.SysDate;
                objBenhnhan.NguoiSua = globalVariables.UserName;
                objBenhnhan.NgayTiepdon = dtCreateDate.Value;
                objBenhnhan.IpMaysua = globalVariables.gv_strIPAddress;
                objBenhnhan.TenMaysua = globalVariables.gv_strComputerName;
            }
          
            return objBenhnhan;
        }

        /// <summary>
        /// hàm thực hiện việc khwoir tạo thoog tin PatietnExam
        /// </summary>
        /// <returns></returns>
        private KcbLuotkham TaoLuotkham()
        {
            try
            {
               
                if (MEnAction == action.Insert || MEnAction == action.Add)
                {
                    _objLuotkham = new KcbLuotkham();
                    //Bỏ đi do đã sinh theo cơ chế bảng danh mục mã lượt khám. Nếu ko sẽ mất mã lượt khám hiện thời.
                    // txtMaLankham.Text = THU_VIEN_CHUNG.KCB_SINH_MALANKHAM();
                    _objLuotkham.IsNew = true;
                }
                else
                {
                    if (_objLuotkham == null)
                        _objLuotkham = KcbLuotkham.FetchByID(m_strMaluotkham);
                    _objLuotkham.MarkOld();
                    _objLuotkham.IsNew = false;
                }
                
                _objLuotkham.KieuKham = txtLoaikham.myCode;
                _objLuotkham.NhomBenhnhan = _objLuotkham.KieuKham;
                _objLuotkham.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
                _objLuotkham.Noitru = 0;
                _objLuotkham.IdDoituongKcb = _idDoituongKcb;
                _objLuotkham.IdLoaidoituongKcb = _idLoaidoituongKcb;
                _objLuotkham.Locked = 0;
                _objLuotkham.HienthiBaocao = 1;
                _objLuotkham.TrangthaiCapcuu = chkCapCuu.Checked ? 1 : 0;
                _objLuotkham.IdKhoatiepnhan = globalVariables.idKhoatheoMay;
                _objLuotkham.NguoiTao = globalVariables.UserName;
                _objLuotkham.NgayTao = globalVariables.SysDate;
                _objLuotkham.Cmt = Utility.sDbnull(txtCMT.Text, "");
                _objLuotkham.DiaChi = txtDiachi.Text;
                _objLuotkham.ThongtinNguongt =Utility.DoTrim( txtTtinNguonGt.Text);
                _objLuotkham.CachTao = 0;
                _objLuotkham.SoBenhAn = Utility.sDbnull(txtSoBA.Text, "-1");
                _objLuotkham.Email = Utility.sDbnull(txtEmail.Text,"");
                _objLuotkham.NoiGioithieu = txtNguonGthieu.myCode;
                long week = Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.WeekOfYear, dtpBOD.Value, dtCreateDate.Value);
                long month = Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Month, dtpBOD.Value, dtCreateDate.Value);
                long year = Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Year, dtpBOD.Value, dtCreateDate.Value);
                int tinhtuoitheotuan = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_TINHTUOI_THEOTUAN", "6", false));
                int tinhtuoitheothang = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_TINHTUOI_THEOTHANG", "17", false));
                _objLuotkham.Tuoi = (int)(dtpBOD.CustomFormat == "yyyy" ? year : (month <= tinhtuoitheotuan ? week : (month <= tinhtuoitheothang ? month : year)));
                _objLuotkham.LoaiTuoi = (byte)(dtpBOD.CustomFormat == "yyyy" ? 0 : (month <= tinhtuoitheotuan ? 2 : (month <= tinhtuoitheothang ? 1 : 0)));
                _objLuotkham.NhomBenhnhan = txtLoaiBN.myCode;
                _objLuotkham.IdBenhvienDen = Utility.Int16Dbnull(txtNoichuyenden.MyID, -1);
                _objLuotkham.TthaiChuyenden = (byte)(chkChuyenVien.Checked ? 1 : 0);
                _objLuotkham.ChandoanChuyenden = Utility.ReplaceStr(txtchandoantuyenduoi.Text);

                if (THU_VIEN_CHUNG.IsBaoHiem(_idLoaidoituongKcb))
                {
                    Laymathe_BHYT();
                    _objLuotkham.MaKcbbd = Utility.sDbnull(txtNoiDKKCBBD.Text, "");
                    _objLuotkham.NoiDongtrusoKcbbd = Utility.sDbnull(txtNoiDongtrusoKCBBD.Text, "");
                    _objLuotkham.MaNoicapBhyt = Utility.sDbnull(txtNoiphattheBHYT.Text);
                    _objLuotkham.LuongCoban = globalVariables.LUONGCOBAN;
                    _objLuotkham.MatheBhyt = Laymathe_BHYT();
                    _objLuotkham.MaDoituongBhyt = Utility.sDbnull(txtMaDtuong_BHYT.Text);
                    _objLuotkham.MaQuyenloi = Utility.Int32Dbnull(txtMaQuyenloi_BHYT.Text, null);
                    _objLuotkham.DungTuyen = !chkTraiTuyen.Visible ? 1 : (((byte?)(chkTraiTuyen.Checked ? 0 : 1)));
                    _objLuotkham.MadtuongSinhsong = txtMaDTsinhsong.myCode;
                    _objLuotkham.GiayBhyt = Utility.Bool2byte(chkGiayBHYT.Checked);
                    if (chkGiayBHYT.Checked)
                    {
                        _objLuotkham.NgayDu5nam = dtpNgaydu5nam.Value.Date;
                    }
                    else
                    {
                        _objLuotkham.NgayDu5nam = null;
                    }
                    _objLuotkham.NgayketthucBhyt = dtInsToDate.Value.Date;
                    _objLuotkham.NgaybatdauBhyt = dtInsFromDate.Value.Date;
                    _objLuotkham.NoicapBhyt = Utility.GetValue(lblNoiCapThe.Text, false);
                    _objLuotkham.DiachiBhyt = Utility.sDbnull(txtDiachi_bhyt.Text);

                }
                else
                {
                    _objLuotkham.GiayBhyt = 0;
                    _objLuotkham.MadtuongSinhsong = "";
                    _objLuotkham.MaKcbbd = "";
                    _objLuotkham.NoiDongtrusoKcbbd = "";
                    _objLuotkham.MaNoicapBhyt = "";
                    _objLuotkham.LuongCoban = globalVariables.LUONGCOBAN;
                    _objLuotkham.MatheBhyt = "";
                    _objLuotkham.MaDoituongBhyt = "";
                    _objLuotkham.MaQuyenloi = -1;
                    _objLuotkham.DungTuyen = 0;
                    _objLuotkham.NgayketthucBhyt = null;
                    _objLuotkham.NgaybatdauBhyt = null;
                    _objLuotkham.NgayDu5nam = null;
                    _objLuotkham.NoicapBhyt = "";
                    _objLuotkham.DiachiBhyt = "";

                }
                _objLuotkham.PtramBhytGoc = Utility.DecimaltoDbnull(txtptramDauthe.Text, 0);
                _objLuotkham.PtramBhyt = Utility.DecimaltoDbnull(txtPtramBHYT.Text, 0);
                //chkTraiTuyen.Visible ?Utility.DecimaltoDbnull(txtPtramBHYT.Text, 0):(objLuotkham.DungTuyen == 0 ? 0 : Utility.DecimaltoDbnull(txtPtramBHYT.Text, 0));
                _objLuotkham.SolanKham = Utility.Int16Dbnull(txtSolankham.Text, 0);
                _objLuotkham.TrieuChung = Utility.ReplaceStr(txtTrieuChungBD.Text);
                _objLuotkham.TrangthaiNgoaitru = 0;
                _objLuotkham.TrangthaiNoitru = 0;
                int PHUONGPHAPTINHNGUONGT = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEPDON_PHUONGPHAPTINHNGUONGT", "0", false));

                //Lấy thông tin nguồn giới thiệu+ chi phí giới thiệu.
                
                if (txtNguonGthieu.myCode != "-1")
                {
                    DmucChung objNguon = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Ma).IsEqualTo(txtNguonGthieu.myCode).And(DmucChung.Columns.Loai).IsEqualTo(txtNguonGthieu.LOAI_DANHMUC).ExecuteSingle<DmucChung>();
                    if (objNguon != null)
                    {
                        if (PHUONGPHAPTINHNGUONGT == 0)
                        {
                            _objLuotkham.NoiGioithieu = objNguon.Ma;
                            if (Utility.DoTrim(objNguon.MotaThem) == "")
                                _objLuotkham.ChiphiGioithieu = 0;
                            else
                            {
                                if (objNguon.MotaThem.Contains("-"))
                                {
                                    string congthuc = objNguon.MotaThem;
                                    string cphilandau = congthuc.Split('-')[0];
                                    List<string> lstcphicaclansau = congthuc.Split('-')[1].Split(',').ToList<string>();
                                    if (_objLuotkham.SolanKham.Value == 1)
                                    {
                                        _objLuotkham.ChiphiGioithieu = Utility.DecimaltoDbnull(cphilandau, 0);
                                    }
                                    else//Nếu khám từ các lần 2 lần 3 thì kiểm tra
                                    {
                                        if (lstcphicaclansau.Count >= _objLuotkham.SolanKham.Value - 1)//Cho phép khai được tính chi phí đến lần khám thứ mấy
                                        {
                                            _objLuotkham.ChiphiGioithieu = Utility.DecimaltoDbnull(lstcphicaclansau[_objLuotkham.SolanKham.Value - 2], 0);

                                        }
                                        else //if (lstcphicaclansau.Count == 1)//Cho phép khai được tính chi phí đến lần khám thứ mấy
                                        {
                                            _objLuotkham.ChiphiGioithieu = 0;
                                            //_objLuotkham.ChiphiGioithieu = Utility.DecimaltoDbnull(lstcphicaclansau[0], 0);
                                        }

                                    }
                                }
                                else
                                {
                                    _objLuotkham.ChiphiGioithieu = Utility.DecimaltoDbnull(objNguon.MotaThem, 0);
                                }
                            }
                        }
                        else//Tính theo mức % duy nhất
                        {
                            _objLuotkham.ChiphiGioithieu = Utility.DecimaltoDbnull(objNguon.MotaThem, 0);
                        }
                    }
                }
                else
                {
                    _objLuotkham.NoiGioithieu = "";
                    _objLuotkham.ChiphiGioithieu = 0;
                }

                //Tránh lỗi khi update người dùng nhập mã lần khám lung tung
                if (MEnAction == action.Update) txtMaLankham.Text = m_strMaluotkham;
                _objLuotkham.MaLuotkham = Utility.sDbnull(txtMaLankham.Text, "");
                _objLuotkham.IdBenhnhan = Utility.Int64Dbnull(txtMaBN.Text, -1);
                _objLuotkham.MotaThem = Utility.sDbnull(ketquacheckthe, "");
                _objLuotkham.LastActionName = action.Add.ToString();
                DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(_idDoituongKcb);
                if (objectType != null)
                {
                    _objLuotkham.MaDoituongKcb = Utility.sDbnull(objectType.MaDoituongKcb, "");
                }
                if (MEnAction == action.Update)
                {
                    _objLuotkham.NgayTiepdon = dtCreateDate.Value;
                    _objLuotkham.NguoiSua = globalVariables.UserName;
                    _objLuotkham.NgaySua = globalVariables.SysDate;
                    _objLuotkham.IpMaysua = globalVariables.gv_strIPAddress;
                    _objLuotkham.TenMaysua = globalVariables.gv_strComputerName;
                }
                if (MEnAction == action.Add || MEnAction == action.Insert)
                {
                    _objLuotkham.NgayTiepdon = dtCreateDate.Value;
                    _objLuotkham.NguoiTiepdon = globalVariables.UserName;

                    _objLuotkham.IpMaytao = globalVariables.gv_strIPAddress;
                    _objLuotkham.TenMaytao = globalVariables.gv_strComputerName;
                }
                
                return _objLuotkham;
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi tạo thông tin lượt khám",ex);
                return null;
            }
          
        }

        #endregion

        #region ImportExcel()

        public void AutoAdd_Khong_xac_dinh()
        {
            //try
            //{
            //    DataTable tempdt = THU_VIEN_CHUNG.LayDmucDiachinh();
            //    DataRow[] arrTinhThanh = tempdt.Select("loai_diachinh=1");
            //    foreach (DataRow drTP in arrTinhThanh)
            //    {
            //        DataRow[] arrQuanHuyen =
            //            tempdt.Select("ma_cha='" + Utility.sDbnull(drTP[DmucDiachinh.Columns.MaDiachinh], "") +
            //                          "' AND ten_diachinh='Không xác định'");
            //        if (arrQuanHuyen.Length <= 0)
            //        {
            //            var newItem = new DmucDiachinh();
            //            newItem.MaDiachinh = Utility.sDbnull(drTP[DmucDiachinh.Columns.MaDiachinh], "") + "_KXD";
            //            newItem.TenDiachinh = "Không xác định";
            //            object parent_Code = Utility.sDbnull(drTP[DmucDiachinh.Columns.MaDiachinh], "");
            //            newItem.MaCha = parent_Code.ToString();
            //            newItem.LoaiDiachinh = Convert.ToByte(2);
            //            newItem.MotaThem = "kx";
            //            newItem.SttHthi = 0;
            //            newItem.IsNew = true;
            //            newItem.Save();
            //        }
            //        foreach (DataRow drQH in arrQuanHuyen)
            //        {
            //            DataRow[] arrXaPhuong =
            //                tempdt.Select("ma_cha='" + Utility.sDbnull(drQH[DmucDiachinh.Columns.MaDiachinh], "") +
            //                              "' AND ten_diachinh='Không xác định'");
            //            if (arrXaPhuong.Length <= 0)
            //            {
            //                var newItem = new DmucDiachinh();
            //                newItem.MaDiachinh = Utility.sDbnull(drQH[DmucDiachinh.Columns.MaDiachinh], "") + "_KXD";
            //                newItem.TenDiachinh = "Không xác định";
            //                object parent_Code = Utility.sDbnull(drQH[DmucDiachinh.Columns.MaDiachinh], "");
            //                newItem.MaCha = parent_Code.ToString();
            //                newItem.LoaiDiachinh = Convert.ToByte(3);
            //                newItem.MotaThem = "kx";
            //                newItem.SttHthi = 0;
            //                newItem.IsNew = true;
            //                newItem.Save();
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
            //finally
            //{
            //}
        }
       

        #endregion

       
        /// <summary>
        /// hàm thực hiện việc enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDantoc.Focus();
            }
        }
        
       

        private void txtQRCode_LostFocus(object sender, EventArgs e)
        {
            List<string> _list = new List<string>();
            if (txtQRCode.Text.EndsWith("$"))
            {
                try
                {
                    string[] qrcode = txtQRCode.Text.Split('|');
                    foreach (string s in qrcode)
                    {
                        _list.Add(s);
                    }
                    txtMaDtuong_BHYT.Text = _list[0].Substring(0, 2);
                    txtMaQuyenloi_BHYT.Text = _list[0].Substring(2, 1);
                    txtNoiphattheBHYT.Text = _list[0].Substring(3, 2);
                    txtOthu4.Text = _list[0].Substring(5, 2);
                    txtOthu5.Text = _list[0].Substring(7, 3);
                    txtOthu6.Text = _list[0].Substring(10, 5);
                    txtNoiDongtrusoKCBBD.Text = _list[5].Substring(0, 2);
                    txtNoiDKKCBBD.Text = _list[5].Substring(5, 3);
                    dtInsFromDate.Value = Convert.ToDateTime(_list[6]);
                    dtInsToDate.Value = Convert.ToDateTime(_list[7]);
                    txtTEN_BN.Text = Utility.ConvertHexStrToUnicode(_list[1]);
                    txtDiachi_bhyt.Text = Utility.ConvertHexStrToUnicode(_list[4]);
                    txtDiachi.Text = Utility.ConvertHexStrToUnicode(_list[4]);
                    if (_list[2].Length <= 4)
                    {
                        dtpBOD.CustomFormat = "yyyy";
                    }
                    else
                    {
                        dtpBOD.CustomFormat = "dd/MM/yyyy HH:mm";
                    }
                    dtpBOD.Value = _list[2].Length <= 4 ? DateTime.ParseExact(_list[2], "yyyy", null) : Convert.ToDateTime(_list[2]);
                    cboPatientSex.SelectedIndex = Utility.Int16Dbnull(_list[3]) == 2 ? 1 : 0;
                    switch (Convert.ToInt16(_list[11]))
                    {
                        case 5:
                            txtMaDTsinhsong.SetCode("K1");
                            break;
                        case 6:
                            txtMaDTsinhsong.SetCode("K2");
                            break;
                        case 7:
                            txtMaDTsinhsong.SetCode("K3");
                            break;
                        default:
                            txtMaDTsinhsong.ClearText();
                            break;
                    }
                    txtExamtypeCode.SelectAll();
                    txtExamtypeCode.Focus();
                }
                catch (Exception exception)
                {
                    log.Trace("Loi: " + exception);
                }
            }

        }

        private void txtQRCode_TextChanged(object sender, EventArgs e)
        {
            List<string> _list = new List<string>();
            if (txtQRCode.Text.EndsWith("$"))
            {
                try
                {
                    string[] qrcode = txtQRCode.Text.Split('|');

                    _list.AddRange(qrcode);

                    txtMaDtuong_BHYT.Text = _list[0].Substring(0, 2);
                    txtMaQuyenloi_BHYT.Text = _list[0].Substring(2, 1);
                    txtNoiphattheBHYT.Text = _list[0].Substring(3, 2);
                    txtOthu4.Text = _list[0].Substring(5, 2);
                    txtOthu5.Text = _list[0].Substring(7, 3);
                    txtOthu6.Text = _list[0].Substring(10, 5);
                    txtNoiDongtrusoKCBBD.Text = _list[5].Substring(0, 2);
                    txtNoiDKKCBBD.Text = _list[5].Substring(5, 3);
                    dtInsFromDate.Value = Convert.ToDateTime(_list[6]);
                    if (_list[7].Length > 8)
                    {
                        dtInsToDate.Value = Convert.ToDateTime(_list[7]);
                    }
                    else
                    {
                        dtInsToDate.Value = dtInsFromDate.Value.AddYears(1).AddDays(-1);
                    }
                    txtTEN_BN.Text = Utility.ConvertHexStrToUnicode(_list[1]);
                    txtDiachi_bhyt.Text = Utility.ConvertHexStrToUnicode(_list[4]);
                    txtDiachi.Text = Utility.ConvertHexStrToUnicode(_list[4]);
                    dtpBOD.Value = _list[2].Length <= 4 ? DateTime.ParseExact(_list[2], "yyyy", null) : Convert.ToDateTime(_list[2]);
                    cboPatientSex.SelectedIndex = Utility.Int16Dbnull(_list[3]) == 2 ? 1 : 0;
                    switch (Convert.ToInt16(_list[11]))
                    {
                        case 5:
                            txtMaDTsinhsong.SetCode("K1");
                            break;
                        case 6:
                            txtMaDTsinhsong.SetCode("K2");
                            break;
                        case 7:
                            txtMaDTsinhsong.SetCode("K3");
                            break;
                        default:
                            txtMaDTsinhsong.ClearText();
                            break;
                    }
                    txtExamtypeCode.SelectAll();
                    txtExamtypeCode.Focus();
                }
                catch (Exception exception)
                {
                    log.Trace("Loi: "+ exception);
                }
            }
        }

        private void cboKieuKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdSave.Focus();
            }
        }

        private void cmdCheckCard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IsValidCard();
        }

        private void cmdSave_Click_1(object sender, EventArgs e)
        {

        }
    }
}

       