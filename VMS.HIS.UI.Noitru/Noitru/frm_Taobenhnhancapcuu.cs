using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using NLog;
using SubSonic;
using SubSonic.Sugar;
using VNS.Libs;
using VNS.HIS.NGHIEPVU;
using VMS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.UI.Forms.Cauhinh;

namespace VNS.HIS.UI.NOITRU
{
   
    /// <summary>
    /// Đẩy thử code=Github
    /// </summary>
    public partial class frm_Taobenhnhancapcuu : Form
    {
        public delegate void OnActionSuccess();
        public event OnActionSuccess _OnActionSuccess;
        NoitruPhanbuonggiuong _objBuonggiuong = null;
        NoitruPhanbuonggiuongCollection _lstNoitruPhanbuonggiuong = new NoitruPhanbuonggiuongCollection();
        KCB_DANGKY _KCB_DANGKY = new KCB_DANGKY();
        KCB_QMS _KCB_QMS = new KCB_QMS();
        DMUC_CHUNG _DMUC_CHUNG = new DMUC_CHUNG();
        private readonly AutoCompleteStringCollection _namesCollectionThanhPho = new AutoCompleteStringCollection();
        private readonly string _strSaveandprintPath = Application.StartupPath + @"\CAUHINH\SaveAndPrintConfig.txt";
        private readonly string _strSaveandprintPath1 = Application.StartupPath +
                                                       @"\CAUHINH\DefaultPrinter_PhieuHoaSinh.txt";
        private string MA_DTUONG = "DV";
        private string SoBHYT = "";
        private string TrongGio = "";
        public bool m_blnCancel;
        private bool b_HasLoaded;
        private bool AllowTextChanged;
        private bool AllowGridSelecttionChanged=true;
        private string _rowFilter = "1=1";
        private bool b_NhapNamSinh;

        public GridEX grdList;
        private bool hasjustpressBACKKey;
        private bool isAutoFinding;
        bool m_blnHasJustInsert = false;
        private DataTable m_DC;
        public SysTrace myTrace;
        private DataTable m_dtDataRoom = new DataTable();
        private DataTable m_dtDatabed = new DataTable();
        private DataTable m_PhongKham = new DataTable();
        private DataTable m_kieuKham;
        private DataTable m_dtChoKham = new DataTable();
        private DataTable m_dtDoiTuong = new DataTable();
        private DataTable m_dtTrieuChung = new DataTable();
        public action m_enAction = action.Insert;
        private Logger log;
        private DataTable m_dataDataRegExam = new DataTable();
        private DataTable mdt_DataQuyenhuyen;
        public DataTable m_dtPatient = new DataTable();
        string m_strMaluotkham = "";//Lưu giá trị patientcode khi cập nhật để người dùng ko được phép gõ Patient_code lung tung
        KcbDanhsachBenhnhan objBenhnhan = null;
        public frm_Taobenhnhancapcuu()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            InitEvents();
            txtTEN_BN.CharacterCasing = globalVariables.CHARACTERCASING == 0
                                            ? CharacterCasing.Normal
                                            : CharacterCasing.Upper;
            dtCreateDate.Value =dtNgayChuyen.Value= globalVariables.SysDate;
            ucBHYT1.Init();
            CauHinhKCB();
            log = LogManager.GetLogger(this.Name);
        }

        void InitEvents()
        {
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_Taobenhnhancapcuu_FormClosing);
            this.Load += new System.EventHandler(this.frm_Taobenhnhancapcuu_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Taobenhnhancapcuu_KeyDown);

            txtMaBN.KeyDown += new KeyEventHandler(txtMaBN_KeyDown);
            txtMaLankham.KeyDown += new KeyEventHandler(txtMaLankham_KeyDown);

            dtpBOD.TextChanged += dtpBOD_TextChanged;

            txtTEN_BN.TextChanged+=new EventHandler(txtTEN_BN_TextChanged);
            txtTEN_BN.LostFocus += txtTEN_BN_LostFocus;
            txtCMT.KeyDown += txtCMT_KeyDown;
            cboPatientSex.SelectedIndex = 0;
            
            cmdThemMoiBN.Click += new System.EventHandler(cmdThemMoiBN_Click);
            cmdSave.Click += new System.EventHandler(cmdSave_Click);
            txtTuoi.TextChanged += new System.EventHandler(txtTuoi_TextChanged);
            txtTuoi.Click += new System.EventHandler(txtTuoi_Click);
            txtTuoi.KeyDown += new System.Windows.Forms.KeyEventHandler(txtTuoi_KeyDown);

            txtTuoi.LostFocus += txtTuoi_LostFocus;
            txtNamSinh.TextChanged += txtNamSinh_TextChanged;

            txtNamSinh.LostFocus += txtNamSinh_LostFocus;

            chkChuyenVien.CheckedChanged += new EventHandler(chkChuyenVien_CheckedChanged);
            cboDoituongKCB.SelectedIndexChanged += new EventHandler(cboDoituongKCB_SelectedIndexChanged);

            txtTrieuChungBD._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtTrieuChungBD__OnShowData);
            txtDantoc._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtDantoc__OnShowData);
            txtNgheNghiep._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtNgheNghiep__OnShowData);


            cmdGetBV.Click += new EventHandler(cmdGetBV_Click);
            cmdThemmoiDiachinh.Click += cmdThemmoiDiachinh_Click;
            chkLaysokham.CheckedChanged += chkLaysokham_CheckedChanged;
            dtNgayChuyen.TextChanged += new EventHandler(dtNgayChuyen_TextChanged);
            dtNgayChuyen.ValueChanged += new EventHandler(dtNgayChuyen_ValueChanged);
            txtGia._OnSelectionChanged += txtGia__OnSelectionChanged;
            txtKhoanoitru._OnEnterMe += txtKhoanoitru__OnEnterMe;
            txtRoom_code._OnEnterMe += txtRoom_code__OnEnterMe;
            grdBuong.SelectionChanged+=grdBuong_SelectionChanged;
            grdBuong.KeyDown+=grdBuong_KeyDown;
            grdGiuong.KeyDown+=grdGiuong_KeyDown;
            grdGiuong.SelectionChanged += grdGiuong_SelectionChanged;
            grdGiuong.MouseDoubleClick+=grdGiuong_MouseDoubleClick;
            txtBedCode._OnEnterMe += txtBedCode__OnEnterMe;
            cmdLaysoBANoitru.Click += cmdLaysoBANoitru_Click;
            ucBHYT1._OnFindPatientByMatheBHYT += ucBHYT1__OnFindPatientByMatheBHYT;
        }

        void ucBHYT1__OnFindPatientByMatheBHYT(string matheBHYT)
        {
            FindPatientIDbyBhyt(matheBHYT);
        }

        void cmdLaysoBANoitru_Click(object sender, EventArgs e)
        {
            txtSoBenhAn.Text = THU_VIEN_CHUNG.LaySoBenhAn();
        }

        void grdGiuong_SelectionChanged(object sender, EventArgs e)
        {
            if (!AllowGridSelecttionChanged || !Utility.isValidGrid(grdGiuong)) return;
            ChonGiuong();
        }
        void ChonGiuong()
        {
            string IdGiuong = Utility.sDbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong), -1);
            txtBedCode.SetId(IdGiuong);
        }
        void txtBedCode__OnEnterMe()
        {
            Utility.GotoNewRowJanus(grdGiuong, NoitruDmucGiuongbenh.Columns.IdGiuong, Utility.sDbnull(txtBedCode.MyID, ""));
        }

        void txtRoom_code__OnEnterMe()
        {
            Utility.GotoNewRowJanus(grdBuong, NoitruDmucBuong.Columns.IdBuong, Utility.sDbnull(txtRoom_code.MyID, ""));
        }
        void grdGiuong_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            grdGiuong_KeyDown(sender, new KeyEventArgs(Keys.Enter));
        }
        private void grdGiuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.isValidGrid(grdGiuong))
            {
                cmdSave_Click(cmdSave, new EventArgs());
            }
        }
        private void grdBuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBedCode.Focus();
                txtRoom_code.Text = Utility.sDbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.TenBuong));
            }
        }
        private void grdBuong_SelectionChanged(object sender, EventArgs e)
        {
            if (!AllowGridSelecttionChanged || !Utility.isValidGrid(grdBuong)) return;
            ChonBuong();
        }
        void ChonBuong()
        {
            string idBuong = Utility.sDbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.IdBuong), -1);
            txtRoom_code.SetId(idBuong);
            m_dtDatabed = THU_VIEN_CHUNG.NoitruTimkiemgiuongTheobuong(Utility.Int32Dbnull(txtKhoanoitru.MyID),
                                                           Utility.Int32Dbnull(idBuong), 0);
            Utility.SetDataSourceForDataGridEx(grdGiuong, m_dtDatabed, true, true, "1=1", "isFull asc,dang_nam ASC,ten_giuong");
            string oldBed = txtBedCode.MyCode;
            txtBedCode.Init(m_dtDatabed, new List<string>() { NoitruDmucGiuongbenh.Columns.IdGiuong, NoitruDmucGiuongbenh.Columns.MaGiuong, NoitruDmucGiuongbenh.Columns.TenGiuong });
            txtBedCode.SetCode(oldBed);
            if (grdGiuong.DataSource != null)
            {
                grdGiuong.MoveFirst();

            }
            if (txtBedCode.MyCode == "-1")
            {
                string idGiuong = Utility.sDbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong), -1);
                txtBedCode.SetId(idGiuong);
            }
        }
        void txtKhoanoitru__OnEnterMe()
        {
            m_dtDataRoom = THU_VIEN_CHUNG.NoitruTimkiembuongTheokhoa(Utility.Int32Dbnull(txtKhoanoitru.MyID));

            Utility.SetDataSourceForDataGridEx(grdBuong, m_dtDataRoom, true, true, "1=1", "sluong_giuong_trong desc,ten_buong");
            txtRoom_code.Init(m_dtDataRoom, new List<string>() { NoitruDmucBuong.Columns.IdBuong, NoitruDmucBuong.Columns.MaBuong, NoitruDmucBuong.Columns.TenBuong });
            if (grdBuong.DataSource != null)
            {
                grdBuong.MoveFirst();
            }
        }
        void txtGia__OnSelectionChanged()
        {
            cboGia.Text = txtGia.MyText;
        }
        void dtNgayChuyen_ValueChanged(object sender, EventArgs e)
        {
            txtGio.Text = Utility.sDbnull(dtNgayChuyen.Value.Hour);
            txtPhut.Text = Utility.sDbnull(dtNgayChuyen.Value.Minute);
        }

        void dtNgayChuyen_TextChanged(object sender, EventArgs e)
        {
            txtGio.Text = Utility.sDbnull(dtNgayChuyen.Value.Hour);
            txtPhut.Text = Utility.sDbnull(dtNgayChuyen.Value.Minute);

        }
        void chkLaysokham_CheckedChanged(object sender, EventArgs e)
        {
            txtSoKcb.Enabled =chkLaysokham.Enabled && chkLaysokham.Checked;
        }

        void cmdThemmoiDiachinh_Click(object sender, EventArgs e)
        {
            frm_themmoi_diachinh_new _themmoi_diachinh = new frm_themmoi_diachinh_new();
            _themmoi_diachinh.ShowDialog();
            if (_themmoi_diachinh.m_blnHasChanged)
            {
               
                AddAutoCompleteDiaChi();
            }
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

        void txtNgheNghiep__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtNgheNghiep.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtNgheNghiep.myCode;
                txtNgheNghiep.Init();
                txtNgheNghiep.SetCode(oldCode);
                txtNgheNghiep.Focus();
            } 
        }

        void txtDantoc__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtDantoc.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtDantoc.myCode;
                txtDantoc.Init();
                txtDantoc.SetCode(oldCode);
                txtDantoc.Focus();
            }
        }

        void txtTrieuChungBD__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtTrieuChungBD.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtTrieuChungBD.myCode;
                txtTrieuChungBD.Init();
                txtTrieuChungBD.SetCode(oldCode);
                txtTrieuChungBD.Focus();
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
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void chkChuyenVien_CheckedChanged(object sender, EventArgs e)
        {
            txtNoichuyenden.Enabled = chkChuyenVien.Checked;
            cmdGetBV.Enabled = chkChuyenVien.Checked;
            //Tạm bỏ 2014-12-04
            //LoadClinicCode();
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
            txtTEN_BN.Text =Utility.CapitalizeWords(txtTEN_BN.Text.Trim());
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

        private void FindPatientIDbyBhyt(string insuranceNum)
        {
            try
            {

                DataTable temdt = SPs.KcbTimkiembenhnhantheomathebhyt(insuranceNum).GetDataSet().Tables[0];
                if (temdt.Rows.Count <= 0) return;
                if (temdt.Rows.Count == 1)
                {
                    AutoFindLastExamandFetchIntoControls(temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), insuranceNum);
                }
                else //Show dialog for select
                {
                    DataRow[] arrDr = temdt.Select(KcbLuotkham.Columns.MatheBhyt+ "='" + insuranceNum + "'");
                    if (arrDr.Length == 1)
                        AutoFindLastExamandFetchIntoControls(arrDr[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), insuranceNum);
                    else
                    {
                        var chonBN = new frm_CHON_BENHNHAN();
                        chonBN.temdt = temdt;
                        chonBN.ShowDialog();
                        if (!chonBN.mv_bCancel)
                        {
                            AutoFindLastExamandFetchIntoControls(chonBN.Patient_ID, insuranceNum);
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
                    AutoFindLastExamandFetchIntoControls(temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty);
                }
                else //Show dialog for select
                {
                    DataRow[] arrDr = temdt.Select(KcbDanhsachBenhnhan.Columns.Cmt+ "='" + CMT + "'");
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

        private bool NotPayment(string patient_ID, ref string NgayKhamGanNhat)
        {
            try
            {
                DataTable temdt = _KCB_DANGKY.KcbLaythongtinBenhnhan(Utility.Int64Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan)));
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
                    m_enAction = action.Update;
                    AllowTextChanged = false;
                    LoadThongtinBenhnhan();
                    CanhbaoInphoi();
                    string ngay_kham = globalVariables.SysDate.ToString("dd/MM/yyyy");
                    //Nếu ngày hệ thống=Ngày đăng ký gần nhất-->Sửa
                    if (globalVariables.SysDate.ToString("dd/MM/yyyy") == dtpInputDate.Value.ToString("dd/MM/yyyy"))
                    {
                        m_enAction = action.Update;
                        Utility.ShowMsg(
                           "Bệnh nhân vừa được đăng ký ngày hôm nay nên hệ thống sẽ chuyển về chế độ Sửa thông tin. Nhấn OK để bắt đầu sửa");
                        //LaydanhsachdangkyKCB();
                        txtTEN_BN.Select();
                    }
                    else//Thêm lần khám cho ngày mới
                    {
                        m_enAction = action.Add;
                        SinhMaLanKham();
                        //Reset dịch vụ KCB
                        //txtTongChiPhiKham.Text = "0";
                        m_dataDataRegExam.Rows.Clear();

                    }
                    StatusControl();
                    ModifyCommand();
                }
                else
                {
                    Utility.ShowMsg(
                        "Bệnh nhân này chưa có lần khám nào-->Có thể bị lỗi dữ liệu. Đề nghị liên hệ với VMS để được giải đáp");
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

    
     

        
        
        // private  b_QMSStop=false;
        /// <summary>
        /// hàm thực hiện việc lấy thông tin của phần dữ liệu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Taobenhnhancapcuu_Load(object sender, EventArgs e)
        {
            try
            {
                AllowTextChanged = false;
                b_HasLoaded = false;
                Utility.SetColor(lblDiachiBN, THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATNHAP_DIACHI_BENHNHAN", "1", false) == "1" ? lblHoten.ForeColor : lblNgheNghiep.ForeColor);
                
                chkLaysokham.Enabled = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATBUOCLAY_SOKHAMCHUABENH", "0", false) == "0";
                txtSoKcb.Enabled = chkLaysokham.Enabled && chkLaysokham.Checked;

                XoathongtinBHYT(true);
                AddAutoCompleteDiaChi();
                Get_DanhmucChung();
                AutocompleteBenhvien();
                txtKhoanoitru.Init(THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0), new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
                
                DataTable dtGia = new dmucgiagiuong_busrule().dsGetList("-1").Tables[0];
                dtGia.DefaultView.Sort = NoitruGiabuonggiuong.Columns.SttHthi + "," + NoitruGiabuonggiuong.Columns.TenGia;
                txtGia.Init(dtGia, new System.Collections.Generic.List<string>() { NoitruGiabuonggiuong.Columns.IdGia, NoitruGiabuonggiuong.Columns.MaGia, NoitruGiabuonggiuong.Columns.TenGia });
                cboGia.DataSource = dtGia;
                cboGia.DataMember = NoitruGiabuonggiuong.Columns.IdGia;
                cboGia.ValueMember = NoitruGiabuonggiuong.Columns.IdGia;
                cboGia.DisplayMember = NoitruGiabuonggiuong.Columns.TenGia;

                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(), DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "", false);
                objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(_MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _MaDoituongKcb);
                ChangeObjectRegion();
                if (m_enAction == action.Insert)//Thêm mới BN
                {
                    objLuotkham = null;
                    if (PropertyLib._KCBProperties.SexInput)
                        cboPatientSex.SelectedIndex = -1;
                    SinhMaLanKham();
                    txtSoBenhAn.Text = THU_VIEN_CHUNG.LaySoBenhAn();
                    txtTEN_BN.Select();
                }
                else if (m_enAction == action.Update)//Cập nhật thông tin Bệnh nhân
                {
                    LoadThongtinBenhnhan();
                    txtTEN_BN.Select();
                }
                else if (m_enAction == action.Add) //Thêm mới lần khám
                {
                    objLuotkham = null;
                    string ngay_kham = globalVariables.SysDate.ToString("dd/MM/yyyy");
                    if (!NotPayment(txtMaBN.Text.Trim(), ref ngay_kham))//Nếu đã thanh toán xong hết thì thêm lần khám mới
                    {
                        SinhMaLanKham();
                        LoadThongtinBenhnhan();
                        
                    }
                    else//Còn lần khám chưa thanh toán-->Kiểm tra
                    {
                        //nếu là ngày hiện tại thì đặt về trạng thái sửa
                        if (ngay_kham == "NOREG" || ngay_kham==globalVariables.SysDate.ToString("dd/MM/yyyy"))
                        {
                            LoadThongtinBenhnhan();
                            if (ngay_kham == "NOREG")//Bn chưa đăng ký phòng khám nào cả. 
                            {
                                //Nếu ngày hệ thống=Ngày đăng ký gần nhất-->Sửa
                                if (globalVariables.SysDate.ToString("dd/MM/yyyy") == dtpInputDate.Value.ToString("dd/MM/yyyy"))
                                {
                                    m_enAction = action.Update;

                                    Utility.ShowMsg(
                                       "Bệnh nhân vừa được đăng ký ngày hôm nay nên hệ thống sẽ chuyển về chế độ Sửa thông tin. Nhấn OK để bắt đầu sửa");
                                    txtTEN_BN.Select();
                                }
                                else//Thêm lần khám cho ngày mới
                                {
                                    m_enAction = action.Add;
                                    SinhMaLanKham();
                                    
                                }
                            }
                            else//Quay về trạng thái sửa
                            {
                                m_enAction = action.Update;

                                Utility.ShowMsg(
                                   "Bệnh nhân vừa được đăng ký ngày hôm nay nên hệ thống sẽ chuyển về chế độ Sửa thông tin. Nhấn OK để bắt đầu sửa");
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
                }
                StatusControl();
                ModifyCommand();
                AllowTextChanged = true;
            }
            catch
            {
            }
            finally
            {
                if (PropertyLib._ConfigProperties.HIS_AppMode != VNS.Libs.AppType.AppEnum.AppMode.License)
                    this.Text = "Đăng ký KCB -->Demo 1500";
                SetActionStatus(); 
               
                ModifyCommand();

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
                DmucCanhbaoCollection lst = new Select().From(DmucCanhbao.Schema).Where(DmucCanhbao.MaBnColumn).IsEqualTo(patient_ID).ExecuteAsCollection<DmucCanhbaoCollection>();
                if (lst.Count > 0)//Delete
                {
                    if (lst[0].CanhBao.TrimStart().TrimEnd() != "")
                        Utility.ShowMsg(lst[0].CanhBao,"Thông tin cảnh báo dành cho Bệnh nhân");
                }
            }
            catch
            {
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
            _lstNoitruPhanbuonggiuong = new NoitruPhanbuonggiuongCollection();
            _objBuonggiuong = null;
            objBenhnhan = KcbDanhsachBenhnhan.FetchByID(txtMaBN.Text);
            if (objBenhnhan != null)
            {
                txtTEN_BN.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan);
                txtNamSinh.Text = Utility.sDbnull(objBenhnhan.NamSinh);
                txtSoDT.Text = Utility.sDbnull(objBenhnhan.DienThoai);
                
                txtDiachi._Text = Utility.sDbnull(objBenhnhan.DiaChi);
                if (objBenhnhan.NgaySinh != null) dtpBOD.Value = objBenhnhan.NgaySinh.Value;
                else dtpBOD.Value = new DateTime((int)objBenhnhan.NamSinh, 1, 1);
                txtNamSinh.Text = Utility.sDbnull(objBenhnhan.NamSinh);
                txtTuoi.Text = Utility.sDbnull(globalVariables.SysDate.Year - Utility.Int32Dbnull(objBenhnhan.NamSinh));
                txtNgheNghiep._Text = Utility.sDbnull(objBenhnhan.NgheNghiep);
                cboPatientSex.SelectedIndex = Utility.GetSelectedIndex(cboPatientSex, Utility.sDbnull(objBenhnhan.IdGioitinh));
                if (Utility.Int32Dbnull(objBenhnhan.DanToc) > 0)
                    txtDantoc._Text = objBenhnhan.DanToc;
                txtCMT.Text = Utility.sDbnull(objBenhnhan.Cmt);


                objLuotkham = new Select().From(KcbLuotkham.Schema)
                   .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtMaLankham.Text)
                   .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtMaBN.Text, -1)).ExecuteSingle
                   <KcbLuotkham>();
                if (objLuotkham != null)
                {
                     _lstNoitruPhanbuonggiuong = new Select().From(NoitruPhanbuonggiuong.Schema)
              .Where(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(1)
              .And(NoitruPhanbuonggiuong.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
              .And(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteAsCollection<NoitruPhanbuonggiuongCollection>();
                     if (_lstNoitruPhanbuonggiuong.Count > 0)
                         _objBuonggiuong = _lstNoitruPhanbuonggiuong[0];
                         
                     ucTamung1.ChangePatients(objLuotkham,"LYDOTAMUNGCAPCUU",1);
                    KcbDangkySokham objSoKCB=new Select().From(KcbDangkySokham.Schema)
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
                    if (string.IsNullOrEmpty(Utility.sDbnull(objLuotkham.SoBenhAn, "")))
                    {
                        txtSoBenhAn.Text = THU_VIEN_CHUNG.LaySoBenhAn();
                    }
                    else
                    {
                        txtSoBenhAn.Text = Utility.sDbnull(objLuotkham.SoBenhAn, "");
                    }

                    m_strMaluotkham = objLuotkham.MaLuotkham;
                    if (_objBuonggiuong != null)
                    {
                        AllowGridSelecttionChanged = false;
                        txtKhoanoitru.SetId(_objBuonggiuong.IdKhoanoitru);
                        txtKhoanoitru__OnEnterMe();
                        txtGia.SetId(_objBuonggiuong.IdGia);
                        txtRoom_code.SetId(_objBuonggiuong.IdBuong);
                        txtRoom_code__OnEnterMe();
                        ChonBuong();
                        txtBedCode.SetId(_objBuonggiuong.IdGiuong);
                        txtBedCode__OnEnterMe();
                        AllowGridSelecttionChanged = true;
                        dtNgayChuyen.Value = Convert.ToDateTime(_objBuonggiuong.NgayVaokhoa);
                        txtGio.Text = Utility.sDbnull(dtNgayChuyen.Value.Hour);
                        txtPhut.Text = Utility.sDbnull(dtNgayChuyen.Value.Minute);
                    }
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
                        txtTrieuChungBD._Text = Utility.sDbnull(objLuotkham.TrieuChung);
                        ucBHYT1.SetValues(chkChuyenVien.Checked, objDoituongKCB, objLuotkham);
                    }
                    else
                    {
                        XoathongtinBHYT(true);
                    }
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
      
        private void Get_DanhmucChung()
        {
           
            AutoCompleteDmucChung();
        }
        private void AddAutoCompleteDiaChi()
        {
            ucBHYT1.DoAutoComplete();
            txtDiachi.dtData = globalVariables.dtAutocompleteAddress.Copy();
            this.txtDiachi.AutoCompleteList = globalVariables.LstAutocompleteAddressSource;
            this.txtDiachi.CaseSensitive = false;
            this.txtDiachi.MinTypedCharacters = 1;
        }
        private void AutoCompleteDmucChung()
        {
            
            txtDantoc.Init();
            txtNgheNghiep.Init();
            txtTrieuChungBD.Init();
            txtSoKcb.Init();
        }
        private void AutocompleteBenhvien()
        {
          
            try
            {
                DataTable m_dtBenhvien = new Select().From(DmucBenhvien.Schema).ExecuteDataSet().Tables[0];
                if (m_dtBenhvien == null) return;
                txtNoichuyenden.Init(m_dtBenhvien, new List<string>() { DmucBenhvien.Columns.IdBenhvien, DmucBenhvien.Columns.MaBenhvien, DmucBenhvien.Columns.TenBenhvien });
               
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
              

            }
        }
       
      
        private void pnlBHYT_Click(object sender, EventArgs e)
        {
        }


        
      

        /// <summary>
        /// hàm thực hiện viecj tính tuổi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNamSinh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (PropertyLib._KCBProperties.Nhapngaythangnamsinh) return;
                if (txtNamSinh.Text.Length < 4) return;
                if (!string.IsNullOrEmpty(txtNamSinh.Text))
                {
                    txtTuoi.Text = Utility.sDbnull(globalVariables.SysDate.Year - Utility.Int32Dbnull(txtNamSinh.Text, 0));
                }
                else
                {
                    txtTuoi.Clear();
                }
                if (txtNamSinh.Focused)
                {
                    txtTuoi.Focus();
                    txtTuoi.SelectAll();
                }

                StatusControl();
            }
            catch (Exception exception)
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
                if (!string.IsNullOrEmpty(txtTuoi.Text))
                {
                    if (!PropertyLib._KCBProperties.Nhapngaythangnamsinh)
                        txtNamSinh.Text = Utility.sDbnull(globalVariables.SysDate.Year - Utility.Int32Dbnull(txtTuoi.Text, 0));
                    else
                        dtpBOD.Value = new DateTime(Utility.Int32Dbnull(globalVariables.SysDate.Year - Utility.Int32Dbnull(txtTuoi.Text, 0)),dtpBOD.Value.Month, dtpBOD.Value.Day);
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:"+exception.Message);
            }
        }

      

        private void SinhMaLanKham()
        {
            txtSolankham.Text = string.Empty;
            if (m_enAction == action.Insert)
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
            txtSolankham.Text =
                THU_VIEN_CHUNG.LaySTTKhamTheoDoituong(_IdDoituongKcb).ToString();
        }

        /// <summary>
        /// hàm thực hiện việc lọc thông tin nhanh của quận huyện
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaQuan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //string _rowFilter = "1=1";
                //if (!string.IsNullOrEmpty(txtMaQuan.Text))
                //{
                //    _rowFilter = string.Format("{0}='{1}'", DmucDiachinh.Columns.MaDiachinh, txtMaQuan.Text.Trim());
                //}
                //mdt_DataQuyenhuyen.DefaultView.RowFilter = _rowFilter;
                //mdt_DataQuyenhuyen.AcceptChanges();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// hàm thực hiện việc làm sách thông tin của bệnh nhân
        /// </summary>
        private void ClearControl()
        {
            Utility.SetMsg(lblMsg, "", false);
            tabControl1.SelectedTab = tabControl1.TabPages[0];
            _objBuonggiuong = null;
            objLuotkham = null;
            _lstNoitruPhanbuonggiuong = new NoitruPhanbuonggiuongCollection();
            m_blnHasJustInsert = false;
            txtSolankham.Text = "1";
            txtTEN_BN.Clear();
            txtNamSinh.Clear();
            dtpBOD.Value = globalVariables.SysDate;
            txtTuoi.Clear();
            txtCMT.Clear();
            txtNgheNghiep.Clear();
            txtDiachi.Clear();
            txtDantoc.Clear();
            txtTrieuChungBD.Clear();
            txtSoDT.Clear();
            chkChuyenVien.Checked = false;
            txtNoichuyenden.SetCode("-1");
            txtKhoanoitru.SetCode("-1");
            txtGia.SetCode("-1");
            txtRoom_code.SetCode("-1");
            txtBedCode.SetCode("-1");
            if (m_dtDataRoom != null) m_dtDataRoom.Clear();
            if (m_dtDatabed != null) m_dtDatabed.Clear();
            if (THU_VIEN_CHUNG.IsNgoaiGio())
            {
               this.Text= "Bệnh nhân đang khám dịch vụ ngoài giờ";
            }
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
            m_enAction = action.Insert;
            if (PropertyLib._KCBProperties.SexInput) cboPatientSex.SelectedIndex = -1;
           
            SinhMaLanKham();
            txtSoBenhAn.Text = THU_VIEN_CHUNG.LaySoBenhAn();
            m_dataDataRegExam.Clear();
            ucBHYT1.ResetMe(objDoituongKCB);
            if (ucBHYT1.IsBHYT)
            {
               
            }
            else
            {
                PtramBhytCu = objDoituongKCB.PhantramTraituyen.Value;
                PtramBhytGocCu = PtramBhytCu;
                txtTEN_BN.Focus();
            }
            if (m_enAction == action.Insert)
            {
                dtpInputDate.Value = globalVariables.SysDate;
                dtCreateDate.Value = globalVariables.SysDate;
               
            }
            SetActionStatus();
           
        }

        private void cmdThemMoiBN_Click(object sender, EventArgs e)
        {
            //Cập nhật lại mã lượt khám chưa dùng tới trong trường hợp nhấn New liên tục
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

            ClearControl();
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

        /// <summary>
        /// hàm thực hiện việc lưu thông tin của đối tượng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (isExceedData())
                {
                    Utility.ShowMsg("Phiên bản Demo chỉ cho phép bạn tiếp đón tối đa 1500 lượt khám. Mời bạn liên hệ để được trợ giúp");
                    return;
                }
                cmdSave.Enabled = false;
                PerformAction();
                cmdSave.Enabled = true;
            }
            catch(Exception ex)
            {
                log.Trace("Lỗi:"+ ex.Message);
            }
            finally
            {
                cmdSave.Enabled = true;
            }
        }
        bool isExceedData()
        {
            return false;
            try
            {
                if (PropertyLib._ConfigProperties.HIS_AppMode != VNS.Libs.AppType.AppEnum.AppMode.License)
                {
                    KcbLuotkhamCollection lst = new Select().From(KcbLuotkham.Schema).ExecuteAsCollection<KcbLuotkhamCollection>();
                    return lst.Count >= 1500;
                }
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
                this.Text = "Bệnh nhân đang khám dịch vụ ngoài giờ";
            }
            
        }

        private bool IsValidData()
        {
            if (m_enAction==action.Insert && dtCreateDate.Value.ToString("dd/MM/yyyy") != globalVariables.SysDate.ToString("dd/MM/yyyy"))
            {
                if (!Utility.AcceptQuestion("Ngày tiếp đón khác ngày hiện tại. Bạn có chắc chắn hay không?","Cảnh báo",true))
                {
                    dtCreateDate.Focus();
                    return false;
                }
            }
            if (THU_VIEN_CHUNG.IsBaoHiem(_IdLoaidoituongKcb))
            {
                if (!ucBHYT1.IsValidBHYT()) return false;
                if (!ucBHYT1.IsValidTheBHYT()) return false;
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
            if (cboPatientSex.SelectedIndex<0)
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn giới tính của Bệnh nhân",true);
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
            if (txtKhoanoitru.MyCode=="-1")
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn khoa nội trú nhập viện", true);
                txtKhoanoitru.Focus();
                return false;
            }

            if (txtRoom_code.MyCode != "-1" && txtBedCode.MyCode != "-1" && txtGia.MyCode=="-1")
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn giá buồng giường", true);
                txtGia.Focus();
                return false;
            }
            if (txtRoom_code.MyCode == "-1" || txtBedCode.MyCode == "-1" )
            {
                if (!Utility.AcceptQuestion("Chú ý: Bạn chưa phân buồng giường cho Bệnh nhân. Nhấn No để quay lại phân buồng giường, nhấn YES để tiếp tục lưu Bệnh nhân mà không có thông tin Buồng giường", "Cảnh báo chưa phân buồng giường cho BN cấp cứu", true))
                {
                    if (txtRoom_code.MyCode == "-1")
                    {
                        txtRoom_code.Focus();
                    }
                    else if (txtBedCode.MyCode == "-1")
                    {
                        txtBedCode.Focus();
                    }
                    return false;
                }
            }
            return isValidIdentifyNum();
        }

       

        private void  ModifyCommand()
        {
            cmdSave.Enabled = Utility.DoTrim(txtTEN_BN.Text).Length > 0;
        }
        private void PerformAction()
        {
            if (!IsValidData()) return;
            switch (m_enAction)
            {
                case action.Update:
                    if (!IsValid4Update()) return;
                    CapnhatthongtinBenhnhan();
                    break;
                case action.Insert:
                    InsertPatient();
                    break;
                case action.Add:
                    ThemLanKham();
                    break;
            }
           
            ModifyCommand();
        }

        private bool IsValid4Update()
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
                if (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) != Utility.DecimaltoDbnull(ucBHYT1.txtPtramBHYT.Text))
                {
                    KcbThanhtoanCollection _lstthanhtoan = new Select().From(KcbThanhtoan.Schema)
                        .Where(KcbThanhtoan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(KcbThanhtoan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .And(KcbThanhtoan.Columns.PtramBhyt).IsEqualTo(Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0))
                        .ExecuteAsCollection<KcbThanhtoanCollection>();
                    if (_lstthanhtoan.Count > 0)
                    {
                        Utility.ShowMsg(string.Format("Bệnh nhân này đã thanh toán với mức BHYT {0}. Do đó hệ thống không cho phép bạn thay đổi phần trăm BHYT.\nMuốn thay đổi đề nghị bạn hủy hết các thanh toán",Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0).ToString()));
                        return false;
                    }
                }
                if (_lstNoitruPhanbuonggiuong != null && _lstNoitruPhanbuonggiuong.Count > 1)
                {
                    Utility.SetMsg(lblMsg, "Bệnh nhân đã chuyển khoa hoặc chuyển giường nên bạn không thể thay đổi thông tin Khoa - Buồng - Giường nội trú", true);
                    return false;
                }
                if (objLuotkham.SoBenhAn != txtSoBenhAn.Text)
                {
                    if (!Utility.AcceptQuestion(string.Format( "Số bệnh án nội trú cũ {0} khác với số BA nội trú mới bạn vừa thay đổi. Bạn có chắc chắn lấy số BA mới này để cập nhật cho Bệnh nhân hay không?\nNhấn Yes để đồng ý. Nhấn No để lưu Bệnh nhân với số Bệnh án cũ",objLuotkham.SoBenhAn), "Cảnh báo thay đổi số Bệnh án nội trú", true))
                    {
                        txtSoBenhAn.Text = objLuotkham.SoBenhAn;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi kiểm tra hợp lệ dữ liệu trước khi cập nhật Bệnh nhân",ex);
                return false;
            }
        }

       
        private void txtTuoi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab) cboPatientSex.Focus();
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
               
            }
            else//Đối tượng khác BHYT
            {
                XoathongtinBHYT(PropertyLib._KCBProperties.XoaBHYT);
                txtTEN_BN.Focus();
            }
        }
        DmucDoituongkcb objDoituongKCB = null;
        /// <summary>
        /// hàm thực hienej phím tắt của form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Taobenhnhancapcuu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                if (this.ActiveControl != null && this.ActiveControl.Name == txtTEN_BN.Name && Utility.DoTrim(txtTEN_BN.Text)!="")
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
                else if (this.ActiveControl != null && this.ActiveControl.Name == txtSoDT.Name && Utility.DoTrim(txtSoDT.Text) != "")
                {
                    frm_DSACH_BN_TKIEM Timkiem_Benhnhan = new frm_DSACH_BN_TKIEM("ALL",100);
                    Timkiem_Benhnhan.AutoSearch = true;
                    Timkiem_Benhnhan.FillAndSearchData(false, "", "", "", "", Utility.DoTrim(txtSoDT.Text), "-1");
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
                txtDiachi._Text = ucBHYT1.txtDiachi_bhyt.Text;
                AllowTextChanged = true;
                return;
            }
            
            string ngay_kham = globalVariables.SysDate.ToString("dd/MM/yyyy");
            if (e.Control && e.KeyCode == Keys.K)
            {
                if (!NotPayment(txtMaBN.Text.Trim(), ref ngay_kham))
                {
                    m_enAction = action.Add;
                    SinhMaLanKham();
                    txtKhoanoitru.Focus();
                }
                else
                {
                    //nếu là ngày hiện tại thì đặt về trạng thái sửa
                    if (ngay_kham == globalVariables.SysDate.ToString("dd/MM/yyyy"))
                    {
                        Utility.ShowMsg(
                            "Bệnh nhân đang có lần khám chưa được thanh toán. Cần thanh toán hết các lần đến khám bệnh của Bệnh nhân trước khi thêm lần khám mới.Nhấn OK để hệ thống quay về trạng thái sửa thông tin BN");
                        m_enAction = action.Update;
                        AllowTextChanged = false;
                        LoadThongtinBenhnhan();
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
            if (e.KeyCode == Keys.Escape && this.ActiveControl != null
                && (
                this.ActiveControl.GetType()!=txtDantoc.GetType()
                || this.ActiveControl.GetType()!=txtGia.GetType()
                ))
            {

                Close();
            }
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.N && e.Control) cmdThemMoiBN.PerformClick();
            if (this.ActiveControl != null && this.ActiveControl.Name != ucTamung1.Name && e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }


        private void cboThanhPho_SelectedIndexChanged_1(object sender, EventArgs e)
        {
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
        private void txtNamSinh_LostFocus(object sender, EventArgs e)
        {
            if (PropertyLib._KCBProperties.Nhapngaythangnamsinh) return;
            if (!string.IsNullOrEmpty(txtNamSinh.Text))
            {
                if (txtNamSinh.Text.Length < 4)
                {
                    Utility.ShowMsg("Năm sinh của bệnh nhân phải là 4 số", "Thông báo", MessageBoxIcon.Information);
                    txtNamSinh.Focus();
                    txtNamSinh.SelectAll();
                }
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
            txtDiachi._Text = ucBHYT1.txtDiachi_bhyt.Text;
            AllowTextChanged = true;
        }

        private void txtCMT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter && txtCMT.Text.Trim() != "")
            {
                FindPatientIDbyCMT(txtCMT.Text.Trim());
            }
        }

       

   

       

      

        private void txtKieuKham_Enter(object sender, EventArgs e)
        {
            if (THU_VIEN_CHUNG.IsNgoaiGio())
            {
                this.Text= "Bệnh nhân đang khám dịch vụ ngoài giờ";
            }
            
        }

        private void txtPhongkham_Enter(object sender, EventArgs e)
        {
            if (THU_VIEN_CHUNG.IsNgoaiGio())
            {
                this.Text = "Bệnh nhân đang khám dịch vụ ngoài giờ";
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

        private void frm_Taobenhnhancapcuu_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Utility.FreeLockObject(m_strMaluotkham);
                //Trả lại mã lượt khám nếu chưa được dùng đến
                new Update(KcbDmucLuotkham.Schema)
                       .Set(KcbDmucLuotkham.Columns.TrangThai).EqualTo(0)
                       .Set(KcbDmucLuotkham.Columns.UsedBy).EqualTo(DBNull.Value)
                       .Set(KcbDmucLuotkham.Columns.StartTime).EqualTo(DBNull.Value)
                       .Set(KcbDmucLuotkham.Columns.EndTime).EqualTo(null)
                       .Where(KcbDmucLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.Int32Dbnull( m_strMaluotkham,"-1"))
                       .And(KcbDmucLuotkham.Columns.TrangThai).IsEqualTo(1)
                       .And(KcbDmucLuotkham.Columns.UsedBy).IsEqualTo(globalVariables.UserName)
                       .And(KcbDmucLuotkham.Columns.Nam).IsEqualTo(globalVariables.SysDate.Year).Execute();
                       ;
               
            }
            catch (Exception exception)
            {
            }
        }

      

     
        void SetActionStatus()
        {
           this.Text = m_enAction == action.Insert ? "BỆNH NHÂN MỚI" : (m_enAction==action.Add?"THÊM LẦN KHÁM":"CẬP NHẬT");
        }
        private void CauHinhKCB()
        {
            dtpBOD.Value = globalVariables.SysDate;
            dtpBOD.Visible = PropertyLib._KCBProperties.Nhapngaythangnamsinh;
            txtNamSinh.Visible = !PropertyLib._KCBProperties.Nhapngaythangnamsinh;
            if (dtpBOD.Visible)
                txtTuoi.Text = Utility.sDbnull(globalVariables.SysDate.Year - dtpBOD.Value.Year);
           
        }

        private void txtSoDT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtKhoanoitru.Focus();
        }

        #region "Sự kiện bắt cho phần khám bệnh"
                     
        #endregion

        #region "khởi tạo sự kiện để lưu lại thông tin của bệnh nhân"

        private string mavuasinh = "";

        private void ThemMoiLanKhamVaoLuoi()
        {
            try
            {
                DataRow dr = m_dtPatient.NewRow();
                dr[KcbDanhsachBenhnhan.Columns.IdBenhnhan] = objBenhnhan.IdBenhnhan;
                dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan] = objBenhnhan.TenBenhnhan;

                dr[KcbDanhsachBenhnhan.Columns.GioiTinh] = objBenhnhan.GioiTinh;
                dr[KcbDanhsachBenhnhan.Columns.IdGioitinh] = objBenhnhan.IdGioitinh;

                dr[KcbDanhsachBenhnhan.Columns.NamSinh] = objBenhnhan.NamSinh;

                dr[KcbLuotkham.Columns.MatheBhyt] = objLuotkham.MatheBhyt;
                dr[KcbDanhsachBenhnhan.Columns.DiaChi] = objLuotkham.DiaChi;
                dr["SO_HSBA"] = objLuotkham.MaLuotkham;
                dr["Tuoi"] = Utility.Int32Dbnull(txtTuoi.Text, 0);
                dr[KcbLuotkham.Columns.NgayTiepdon] = dtCreateDate.Value;
                dr[KcbLuotkham.Columns.NgayRavien] = null;
                dr[KcbLuotkham.Columns.NgayNhapvien] = new DateTime(dtNgayChuyen.Value.Year, dtNgayChuyen.Value.Month,
                                               dtNgayChuyen.Value.Day, Utility.Int32Dbnull(txtGio.Text),
                                               Utility.Int32Dbnull(txtPhut.Text), 00);
                dr[KcbLuotkham.Columns.IdKhoanoitru] = objLuotkham.IdKhoanoitru;
                dr[KcbLuotkham.Columns.IdDoituongKcb] = objLuotkham.IdDoituongKcb;
                dr[KcbLuotkham.Columns.MaLuotkham] = objLuotkham.MaLuotkham;
                dr[KcbLuotkham.Columns.MaDoituongKcb] = objLuotkham.MaDoituongKcb;
                dr["Id"] = _objBuonggiuong != null ? _objBuonggiuong.Id : -1;
                dr[KcbLuotkham.Columns.TrangthaiNoitru] = 1;
                dr[KcbLuotkham.Columns.TthaiThopNoitru] = 0;
                dr[KcbLuotkham.Columns.TrangthaiCapcuu] = 1;
                dr[KcbLuotkham.Columns.IdBuong] = _objBuonggiuong != null ? _objBuonggiuong.IdBuong : -1;
                dr[KcbLuotkham.Columns.IdGiuong] = _objBuonggiuong != null ? _objBuonggiuong.IdGiuong : -1;

                dr["ten_doituong_kcb"] = objDoituongKCB != null ? objDoituongKCB.TenDoituongKcb : "";
                dr["ten_trangthai_kcb"] = "Nội trú";
                dr["ten_buong"] = Utility.GetValueFromGridColumn(grdBuong, NoitruDmucBuong.Columns.TenBuong);
                dr["ten_giuong"] = Utility.GetValueFromGridColumn(grdGiuong, NoitruDmucGiuongbenh.Columns.TenGiuong);
                dr["ten_khoanoitru"] = txtKhoanoitru.Text;
                dr["songay"] = 0;
                dr["id_chuyen"] = -1;
                m_dtPatient.Rows.InsertAt(dr, 0);
            }
            catch (Exception)
            {
                
               
            }
            
        }

        private void UpdateBNVaoTrenLuoi()
        {
            try
            {
                EnumerableRowCollection<DataRow> query = from bn in m_dtPatient.AsEnumerable()
                                                         where
                                                             Utility.sDbnull(bn[KcbLuotkham.Columns.MaLuotkham]) ==
                                                             txtMaLankham.Text
                                                         select bn;
                if (query.Count() > 0)
                {
                    DataRow dr = query.FirstOrDefault();
                    dr[KcbDanhsachBenhnhan.Columns.IdBenhnhan] = objBenhnhan.IdBenhnhan;
                    dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan] = objBenhnhan.TenBenhnhan;

                    dr[KcbDanhsachBenhnhan.Columns.GioiTinh] = objBenhnhan.GioiTinh;
                    dr[KcbDanhsachBenhnhan.Columns.IdGioitinh] = objBenhnhan.IdGioitinh;

                    dr[KcbDanhsachBenhnhan.Columns.NamSinh] = objBenhnhan.NamSinh;

                    dr[KcbLuotkham.Columns.MatheBhyt] = objLuotkham.MatheBhyt;
                    dr[KcbDanhsachBenhnhan.Columns.DiaChi] = objLuotkham.DiaChi;
                    dr["SO_HSBA"] = objLuotkham.MaLuotkham;
                    dr["Tuoi"] = Utility.Int32Dbnull(txtTuoi.Text, 0);
                    dr[KcbLuotkham.Columns.NgayTiepdon] = dtCreateDate.Value;
                    dr[KcbLuotkham.Columns.NgayRavien] = null;
                    dr[KcbLuotkham.Columns.NgayNhapvien] = new DateTime(dtNgayChuyen.Value.Year, dtNgayChuyen.Value.Month,
                                                   dtNgayChuyen.Value.Day, Utility.Int32Dbnull(txtGio.Text),
                                                   Utility.Int32Dbnull(txtPhut.Text), 00);
                    dr[KcbLuotkham.Columns.IdKhoanoitru] = objLuotkham.IdKhoanoitru;
                    dr[KcbLuotkham.Columns.IdDoituongKcb] = objLuotkham.IdDoituongKcb;
                    dr[KcbLuotkham.Columns.MaLuotkham] = objLuotkham.MaLuotkham;
                    dr[KcbLuotkham.Columns.MaDoituongKcb] = objLuotkham.MaDoituongKcb;
                   
                    dr["Id"] = _objBuonggiuong != null ? _objBuonggiuong.Id : -1;
                    dr[KcbLuotkham.Columns.TrangthaiNoitru] = 1;
                    dr[KcbLuotkham.Columns.TthaiThopNoitru] = 0;
                    dr[KcbLuotkham.Columns.TrangthaiCapcuu] = 1;
                    dr[KcbLuotkham.Columns.IdBuong] = _objBuonggiuong != null ? _objBuonggiuong.IdBuong : -1;
                    dr[KcbLuotkham.Columns.IdGiuong] = _objBuonggiuong != null ? _objBuonggiuong.IdGiuong : -1;

                    dr["ten_doituong_kcb"] = objDoituongKCB != null ? objDoituongKCB.TenDoituongKcb : "";
                    dr["ten_trangthai_kcb"] = "Nội trú";
                    dr["ten_buong"] = Utility.GetValueFromGridColumn(grdBuong, NoitruDmucBuong.Columns.TenBuong);
                    dr["ten_giuong"] = Utility.GetValueFromGridColumn(grdGiuong, NoitruDmucGiuongbenh.Columns.TenGiuong);
                    dr["ten_khoanoitru"] = txtKhoanoitru.Text;
                    dr["songay"] = 0;
                    dr["id_chuyen"] = -1;
                    m_dtPatient.AcceptChanges();
                }
            }
            catch (Exception)
            {
                
            }
           
        }

        private void ThemLanKham()
        {
            DateTime ngaychuyenkhoa = new DateTime(dtNgayChuyen.Value.Year, dtNgayChuyen.Value.Month,
                                             dtNgayChuyen.Value.Day, Utility.Int32Dbnull(txtGio.Text),
                                             Utility.Int32Dbnull(txtPhut.Text), 00);
            objBenhnhan = TaoBenhNhan();
            objLuotkham = TaoLuotkham();
            _objBuonggiuong = TaodulieuBuonggiuong();
            KcbDangkySokham objSokham = TaosoKCB();
            long v_id_kham = -1;
            string msg = "";
            errorProvider1.Clear();
            //this.myTrace.FunctionID = globalVariables.FunctionID;
            //this.myTrace.FunctionName = globalVariables.FunctionName;
            ActionResult actionResult = _KCB_DANGKY.ThemmoiLuotkhamCapcuu(this.myTrace, objBenhnhan, objLuotkham, objSokham, _objBuonggiuong, ngaychuyenkhoa, ref msg);

            if (msg.Trim() != "")
            {
                errorProvider1.SetError(txtSoKcb, msg);
            }
            switch (actionResult)
            {
                case ActionResult.Success:
                    if (objLuotkham.SoBenhAn!=null && objLuotkham.SoBenhAn != txtSoBenhAn.Text)
                    {
                        Utility.ShowMsg(string.Format( "Chú ý: Số bệnh án nội trú {0} đã được Bệnh nhân khác sử dụng nên số bệnh án nội trú mới của Bệnh nhân là {1}",txtSoBenhAn.Text,objLuotkham.SoBenhAn ));
                    }
                    txtSoBenhAn.Text = objLuotkham.SoBenhAn;
                    PtramBhytCu = Utility.DecimaltoDbnull(ucBHYT1.txtPtramBHYT.Text, 0);
                    PtramBhytGocCu = Utility.DecimaltoDbnull(ucBHYT1.txtptramDauthe.Text, 0);
                    txtMaLankham.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                    txtMaBN.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                    m_blnHasJustInsert = true;
                    m_enAction = action.Update;
                    Utility.SetMsg(lblMsg, "Bạn thêm mới lần khám bệnh nhân thành công", false);
                    ThemMoiLanKhamVaoLuoi();
                    if (_OnActionSuccess != null) _OnActionSuccess();
                   
                    Utility.GotoNewRowJanus(grdList, KcbLuotkham.Columns.MaLuotkham, txtMaLankham.Text);
                    if (chkTudongthemmoi.Checked)
                        cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
                    else
                    {
                        ucTamung1.ChangePatients(objLuotkham, "LYDOTAMUNGCAPCUU",1);
                        tabControl1.SelectedTab = tabControl1.TabPages[1];
                        ucTamung1.Themmoi();
                    }
                    m_blnCancel = false;
                    break;
                case ActionResult.Error:
                    Utility.SetMsg(lblMsg, "Lỗi trong quá trình thêm lần khám !", true);
                    cmdSave.Focus();
                    break;
            }
        }
      
        private KcbDangkySokham TaosoKCB()
        {
            KcbDangkySokham objSokham = null;
            if (objDoituongKCB == null) return null;
            if (chkLaysokham.Checked &&  txtSoKcb.myCode != "-1")
            {
                
                DmucChung objDmucchung = THU_VIEN_CHUNG.LaydoituongDmucChung(txtSoKcb.LOAI_DANHMUC, txtSoKcb.myCode);
                if (objDmucchung != null)
                {
                    objSokham = new KcbDangkySokham();
                    if (objDoituongKCB != null)
                    {
                        objSokham.IdLoaidoituongkcb = objDoituongKCB.IdLoaidoituongKcb;
                        objSokham.MaDoituongkcb = objDoituongKCB.MaDoituongKcb;
                        objSokham.IdDoituongkcb = objDoituongKCB.IdDoituongKcb;
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
                if (m_enAction == action.Insert)
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
        private void InsertPatient()
        {
            DateTime ngaychuyenkhoa = new DateTime(dtNgayChuyen.Value.Year, dtNgayChuyen.Value.Month,
                                           dtNgayChuyen.Value.Day, Utility.Int32Dbnull(txtGio.Text),
                                           Utility.Int32Dbnull(txtPhut.Text), 00);
            objBenhnhan = TaoBenhNhan();
            objLuotkham = TaoLuotkham();
           
                _objBuonggiuong = TaodulieuBuonggiuong();
            KcbDangkySokham objSokham = TaosoKCB();
            long v_id_kham = -1;
            string msg = "";
            errorProvider1.Clear();
            //this.myTrace.FunctionID = globalVariables.FunctionID;
            //this.myTrace.FunctionName = globalVariables.FunctionName;
            ActionResult actionResult = _KCB_DANGKY.ThemmoiBenhnhanCapcuu(this.myTrace, objBenhnhan, objLuotkham, objSokham, _objBuonggiuong,
                                                                            ngaychuyenkhoa, ref msg);

            if (msg.Trim() != "")
            {
                errorProvider1.SetError(txtSoKcb, msg);
            }
            switch (actionResult)
            {
                case ActionResult.Success:

                    if (objLuotkham.SoBenhAn!=null && objLuotkham.SoBenhAn != txtSoBenhAn.Text)
                    {
                        Utility.ShowMsg(string.Format( "Chú ý: Số bệnh án nội trú {0} đã được Bệnh nhân khác sử dụng nên số bệnh án nội trú mới của Bệnh nhân là {1}",txtSoBenhAn.Text,objLuotkham.SoBenhAn ));
                    }
                    txtSoBenhAn.Text = objLuotkham.SoBenhAn;

                    PtramBhytCu = Utility.DecimaltoDbnull(ucBHYT1.txtPtramBHYT.Text, 0);
                    PtramBhytGocCu = Utility.DecimaltoDbnull(ucBHYT1.txtptramDauthe.Text, 0);
                    txtMaLankham.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                    txtMaBN.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                    mavuasinh = Utility.sDbnull(objLuotkham.IdBenhnhan);
                    m_enAction = action.Update;
                    m_blnHasJustInsert = true;
                    m_strMaluotkham = txtMaLankham.Text;
                    ThemMoiLanKhamVaoLuoi();
                    if (_OnActionSuccess != null) _OnActionSuccess();
                    Utility.SetMsg(lblMsg, "Bạn thêm mới bệnh nhân thành công", false);
                    Utility.GotoNewRowJanus(grdList, KcbLuotkham.Columns.MaLuotkham, txtMaLankham.Text);
                    m_blnCancel = false;
                    if (chkTudongthemmoi.Checked)
                    {
                        cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
                    }
                     else
                    {
                        ucTamung1.ChangePatients(objLuotkham, "LYDOTAMUNGCAPCUU",1);
                        tabControl1.SelectedTab = tabControl1.TabPages[1];
                        ucTamung1.Themmoi();
                    }
                        
                    txtMaBN.Text = Utility.sDbnull(mavuasinh);
                    break;
                case ActionResult.Error:
                    Utility.SetMsg(lblMsg, "Bạn thực hiện thêm dữ liệu không thành công !", true);
                    cmdSave.Focus();
                    break;
            }
        }
       
        private NoitruPhanbuonggiuong TaodulieuBuonggiuong()
        {

            NoitruPhanbuonggiuong objPhanbuonggiuong = null;
            if (_objBuonggiuong != null)
                objPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(_objBuonggiuong.Id);
            else
                objPhanbuonggiuong = new NoitruPhanbuonggiuong();
            if (objPhanbuonggiuong == null) objPhanbuonggiuong = new NoitruPhanbuonggiuong();
                
            objPhanbuonggiuong.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
            objPhanbuonggiuong.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan, -1);
            objPhanbuonggiuong.IdKhoanoitru = Utility.Int16Dbnull(txtKhoanoitru.MyID, -1);
            objPhanbuonggiuong.NgayTao = globalVariables.SysDate;
            objPhanbuonggiuong.NgayVaokhoa = new DateTime(dtNgayChuyen.Value.Year, dtNgayChuyen.Value.Month,
                                           dtNgayChuyen.Value.Day, Utility.Int32Dbnull(txtGio.Text),
                                           Utility.Int32Dbnull(txtPhut.Text), 00);
            objPhanbuonggiuong.IdKham = -1;
            objPhanbuonggiuong.NguoiTao = globalVariables.UserName;
            objPhanbuonggiuong.IdBacsiChidinh = globalVariables.gv_intIDNhanvien;
            objPhanbuonggiuong.NoiTru = 1;
            objPhanbuonggiuong.TrangthaiThanhtoan = 0;
            objPhanbuonggiuong.TrangThai = 0;
            objPhanbuonggiuong.DuyetBhyt = 0;
            objPhanbuonggiuong.CachtinhSoluong = 0;
            objPhanbuonggiuong.SoluongGio = 0;
            objPhanbuonggiuong.IdBuong = Utility.Int16Dbnull(txtRoom_code.MyID,-1);
            objPhanbuonggiuong.IdGiuong = Utility.Int16Dbnull(txtBedCode.MyID,-1);
            objPhanbuonggiuong.IdGia = Utility.Int32Dbnull(txtGia.MyID, -1);
            return objPhanbuonggiuong;
        }
        private void CapnhatthongtinBenhnhan()
        {
            DateTime ngaychuyenkhoa = new DateTime(dtNgayChuyen.Value.Year, dtNgayChuyen.Value.Month,
                                          dtNgayChuyen.Value.Day, Utility.Int32Dbnull(txtGio.Text),
                                          Utility.Int32Dbnull(txtPhut.Text), 00);
            objBenhnhan = TaoBenhNhan();
            
            objLuotkham = TaoLuotkham();
            
                _objBuonggiuong = TaodulieuBuonggiuong();
            KcbDangkySokham objSokham = TaosoKCB();
            string msg = "";
            errorProvider1.Clear();
            //this.myTrace.FunctionID = globalVariables.FunctionID;
            //this.myTrace.FunctionName = globalVariables.FunctionName;
            ActionResult actionResult = _KCB_DANGKY.UpdateBenhnhanCapcuu(this.myTrace, objBenhnhan, objLuotkham, objSokham, _objBuonggiuong, ngaychuyenkhoa, PtramBhytCu, PtramBhytGocCu, ref msg);
            // THEM_PHI_DVU_KYC(objLuotkham);
            if (msg.Trim() != "")
            {
                errorProvider1.SetError(txtSoKcb, msg);
            }
            switch (actionResult)
            {
                case ActionResult.Success:
                   
                    if (objLuotkham.SoBenhAn!=null && objLuotkham.SoBenhAn != txtSoBenhAn.Text)
                    {
                        Utility.ShowMsg(string.Format( "Chú ý: Số bệnh án nội trú {0} đã được Bệnh nhân khác sử dụng nên số bệnh án nội trú mới của Bệnh nhân là {1}",txtSoBenhAn.Text,objLuotkham.SoBenhAn ));
                    }
                    txtSoBenhAn.Text=objLuotkham.SoBenhAn;
                    //gọi lại nếu thay đổi địa chỉ
                    m_blnHasJustInsert = false;
                    PtramBhytCu = Utility.DecimaltoDbnull(ucBHYT1.txtPtramBHYT.Text, 0);
                    PtramBhytGocCu = Utility.DecimaltoDbnull(ucBHYT1.txtptramDauthe.Text, 0);
                    Utility.SetMsg(lblMsg, "Bạn sửa thông tin Bệnh nhân thành công", false);
                    UpdateBNVaoTrenLuoi();
                    if (_OnActionSuccess != null) _OnActionSuccess();
                    
                    if (string.IsNullOrEmpty(objLuotkham.MatheBhyt))
                    {
                        ucBHYT1.ClearMe();
                        chkChuyenVien.Checked = false;
                    }
                    Utility.GotoNewRowJanus(grdList, KcbLuotkham.Columns.MaLuotkham, txtMaLankham.Text);
                    m_blnCancel = false;

                    break;
                case ActionResult.Error:
                    Utility.SetMsg(lblMsg, "Bạn thực hiện sửa thông tin không thành công !", true);
                    break;
                case ActionResult.Cancel:
                    Utility.ShowMsg(string.Format("Bệnh nhân này đã thanh toán một số dịch vụ nên bạn không được phép chuyển đối tượng hoặc thay đổi phần trăm BHYT\nPhần trăm cũ {0} % - Phần trăm mới {1} %", PtramBhytCu.ToString(), ucBHYT1.txtPtramBHYT.Text), "Cảnh báo");
                    break;
            }
        }

        /// <summary>
        /// Insert dữ liệu khi thêm mới hoàn toàn
        /// </summary>hàm chen du lieu moi tin day, benhnhan kham benh moi tinh
        private KcbDanhsachBenhnhan TaoBenhNhan()
        {
            
            var objBenhnhan = new KcbDanhsachBenhnhan();
            if (m_enAction == action.Add) objBenhnhan.IdBenhnhan = Utility.Int64Dbnull(txtMaBN.Text, -1);
            if (m_enAction == action.Update) objBenhnhan = KcbDanhsachBenhnhan.FetchByID(Utility.Int64Dbnull(txtMaBN.Text, -1));
            objBenhnhan.TenBenhnhan = txtTEN_BN.Text;
            objBenhnhan.DiaChi = txtDiachi.Text;
            //Tạm REM lại tìm hiểu tại sao lại gán ="" với đối tượng dịch vụ
            //if (_IdDoituongKcb == 1) //Đối tượng dịch vụ
            //    objBenhnhan.DiaChi = "";
            //else //Đối tượng BHYT
            objBenhnhan.DiachiBhyt = Utility.sDbnull(ucBHYT1.txtDiachi_bhyt.Text);
            objBenhnhan.DienThoai = txtSoDT.Text;
            objBenhnhan.Email = "";
            objBenhnhan.KieuBenhnhan = 0;
            //objBenhnhan.Locked = 0;
            objBenhnhan.NgayTao = globalVariables.SysDate;
            objBenhnhan.NguoiTao = globalVariables.UserName;
            objBenhnhan.NguonGoc = "KCB";
            objBenhnhan.Cmt = Utility.sDbnull(txtCMT.Text, "");
            objBenhnhan.CoQuan = string.Empty;
            objBenhnhan.NgheNghiep = txtNgheNghiep.Text;
            objBenhnhan.GioiTinh = cboPatientSex.Text;
            objBenhnhan.IdGioitinh = Utility.ByteDbnull(cboPatientSex.SelectedValue, 0);
            objBenhnhan.NamSinh = txtNamSinh.Visible ? Utility.Int16Dbnull(txtNamSinh.Text, null) : Utility.Int16Dbnull(dtpBOD.Value.Year);
            string BirthDate = txtNamSinh.Visible ? string.Format("{0}/{1}/{2}", 1, 1, txtNamSinh.Text) : dtpBOD.Value.ToString("dd/MM/yyyy");
            if (Dates.IsDate(BirthDate))
            {
                objBenhnhan.NgaySinh = Convert.ToDateTime(BirthDate);
            }
            else
            {
                objBenhnhan.NgaySinh = null;
            }

            if (m_enAction == action.Insert)
            {
                objBenhnhan.NgayTiepdon = dtCreateDate.Value;
                objBenhnhan.NguoiTao = globalVariables.UserName;
                objBenhnhan.IpMaytao = globalVariables.gv_strIPAddress;
                objBenhnhan.TenMaytao = globalVariables.gv_strComputerName;
            }
            if (m_enAction == action.Update)
            {
                objBenhnhan.NgaySua = globalVariables.SysDate;
                objBenhnhan.NguoiSua = globalVariables.UserName;
                objBenhnhan.NgayTiepdon = dtCreateDate.Value;

                objBenhnhan.IpMaysua = globalVariables.gv_strIPAddress;
                objBenhnhan.TenMaysua = globalVariables.gv_strComputerName;
            }
            objBenhnhan.DanToc = txtDantoc.Text;
            return objBenhnhan;
        }

        /// <summary>
        /// hàm thực hiện việc khwoir tạo thoog tin PatietnExam
        /// </summary>
        /// <returns></returns>
        private KcbLuotkham TaoLuotkham()
        {
           
            if (m_enAction == action.Insert || m_enAction == action.Add)
            {
                objLuotkham = new KcbLuotkham();
                objLuotkham.IsNew = true;
            }
            else
            {
                objLuotkham.IsLoaded = true;
                objLuotkham.MarkOld();
                objLuotkham.IsNew = false;
            }
            if (string.IsNullOrEmpty(Utility.sDbnull(objLuotkham.SoBenhAn, "")))
            {
                txtSoBenhAn.Text = THU_VIEN_CHUNG.LaySoBenhAn();
            }
            else
            {
                txtSoBenhAn.Text = Utility.sDbnull(objLuotkham.SoBenhAn, "");
            }
            objLuotkham.SoBenhAn = Utility.sDbnull(txtSoBenhAn.Text);
            objLuotkham.MotaNhapvien = Utility.DoTrim(txtGhiChu.Text);
            objLuotkham.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
            objLuotkham.Noitru = Utility.ByteDbnull(TrangthaiNoitru.NoiTru);
            objLuotkham.IdDoituongKcb = _IdDoituongKcb;
            objLuotkham.IdLoaidoituongKcb = _IdLoaidoituongKcb;
            objLuotkham.Locked = 0;
            objLuotkham.HienthiBaocao = 1;
            objLuotkham.TrangthaiCapcuu = 1;
            objLuotkham.CachTao = 1;
            objLuotkham.IdKhoatiepnhan = globalVariables.idKhoatheoMay;
            objLuotkham.NguoiTao = globalVariables.UserName;
            objLuotkham.NgayTao = globalVariables.SysDate;
            objLuotkham.Cmt = Utility.sDbnull(txtCMT.Text, "");
            objLuotkham.DiaChi = txtDiachi.Text;
            objLuotkham.Email = "";
            objLuotkham.NoiGioithieu = "";
            objLuotkham.NhomBenhnhan = "-1";
            objLuotkham.KieuKham = "KT";
            objLuotkham.IdBenhvienDen = Utility.Int16Dbnull(txtNoichuyenden.MyID, -1);
            objLuotkham.TthaiChuyenden = (byte)(chkChuyenVien.Checked ? 1 : 0);
            if (THU_VIEN_CHUNG.IsBaoHiem(_IdLoaidoituongKcb))
            {
                ucBHYT1.Laymathe_BHYT();
                objLuotkham.MaKcbbd = Utility.sDbnull(ucBHYT1.txtNoiDKKCBBD.Text, "");
                objLuotkham.NoiDongtrusoKcbbd = Utility.sDbnull(ucBHYT1.txtNoiDongtrusoKCBBD.Text, "");
                objLuotkham.MaNoicapBhyt = Utility.sDbnull(ucBHYT1.txtNoiphattheBHYT.Text);
                objLuotkham.LuongCoban = globalVariables.LUONGCOBAN;
                objLuotkham.MatheBhyt = ucBHYT1.Laymathe_BHYT();
                objLuotkham.MaDoituongBhyt = Utility.sDbnull(ucBHYT1.txtMaDtuong_BHYT.Text);
                objLuotkham.MaQuyenloi = Utility.Int32Dbnull(ucBHYT1.txtMaQuyenloi_BHYT.Text, null);
                objLuotkham.DungTuyen = !ucBHYT1.chkTraiTuyen.Visible ? 1 : (((byte?)(ucBHYT1.chkTraiTuyen.Checked ? 0 : 1)));

                objLuotkham.MadtuongSinhsong = ucBHYT1.txtMaDTsinhsong.myCode;
                objLuotkham.GiayBhyt = Utility.Bool2byte(ucBHYT1.chkGiayBHYT.Checked);

                objLuotkham.NgayketthucBhyt = ucBHYT1.dtInsToDate.Value.Date;
                objLuotkham.NgaybatdauBhyt = ucBHYT1.dtInsFromDate.Value.Date;
                objLuotkham.NoicapBhyt = Utility.GetValue(ucBHYT1.lblNoiCapThe.Text, false);
                objLuotkham.DiachiBhyt = Utility.sDbnull(ucBHYT1.txtDiachi_bhyt.Text);
                
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
            if (m_enAction == action.Update) txtMaLankham.Text = m_strMaluotkham;
            objLuotkham.MaLuotkham = Utility.sDbnull(txtMaLankham.Text, "");
            objLuotkham.IdBenhnhan = Utility.Int64Dbnull(txtMaBN.Text, -1);
            DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(_IdDoituongKcb);
            if (objectType != null)
            {
                objLuotkham.MaDoituongKcb = Utility.sDbnull(objectType.MaDoituongKcb, "");
            }
            if (m_enAction == action.Update)
            {
                objLuotkham.NgayTiepdon = dtCreateDate.Value;
                objLuotkham.NguoiSua = globalVariables.UserName;
                objLuotkham.NgaySua = globalVariables.SysDate;
                objLuotkham.IpMaysua = globalVariables.gv_strIPAddress;
                objLuotkham.TenMaysua = globalVariables.gv_strComputerName;
            }
            if (m_enAction == action.Add || m_enAction == action.Insert)
            {
                objLuotkham.NgayTiepdon = dtCreateDate.Value;
                objLuotkham.NguoiTiepdon = globalVariables.UserName;

                objLuotkham.IpMaytao = globalVariables.gv_strIPAddress;
                objLuotkham.TenMaytao = globalVariables.gv_strComputerName;
            }
            objLuotkham.PtramBhytGoc = Utility.DecimaltoDbnull(ucBHYT1.txtptramDauthe.Text, 0);
            objLuotkham.PtramBhyt = Utility.DecimaltoDbnull(ucBHYT1.txtPtramBHYT.Text, 0);
            return objLuotkham;
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
    }
}