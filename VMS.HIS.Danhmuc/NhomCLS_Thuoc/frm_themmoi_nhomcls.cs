using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using NLog;
using SubSonic;
using SubSonic.Sugar;
using VNS.Libs;
using VMS.HIS.DAL;
using SortOrder = Janus.Windows.GridEX.SortOrder;
using Strings = Microsoft.VisualBasic.Strings;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.Classes;
using VNS.HIS.UI.DANHMUC;
namespace VNS.HIS.UI.NGOAITRU
{
    public partial class frm_themmoi_nhomcls : Form
    {
        public delegate void OnActionSuccess();
        public event OnActionSuccess _OnActionSuccess;
        private readonly Logger log;
        private int CurrentRowIndex = -1;
        public int Exam_ID = -1;
        public GridEX grdList;
        private string Help =
            "Tạo nhóm chỉ định cận lâm sàng";
        KCB_CHIDINH_CANLAMSANG CHIDINH_CANLAMSANG = new KCB_CHIDINH_CANLAMSANG();
        KCB_THAMKHAM __THAMKHAM = new KCB_THAMKHAM();

        private int IdChitietdichvu;
        bool m_blnAllowSelectionChanged = false;
        private ActionResult actionResult = ActionResult.Error;
        public bool m_blnCancel=true;


        private bool isSaved;
        private int lastIndex;
        private char lastKey;
        public DataTable m_dtNhom = new DataTable();
        private DataTable m_dtChitietNhom = new DataTable();
        public DataTable m_dtDmucDichvuCLS = new DataTable();
        public action m_eAction = action.Insert;
        private bool neverFound = true;
        public DataTable p_AssignInfo;
        private string rowFilter = "1=1";
        private string strQuestion = "";
        private string strSaveandprintPath = Application.StartupPath + @"\CAUHINH\SaveAndPrintConfigKedonthuoc.txt";
        private int v_AssignId = -1;
        string nhomchidinh = "";
        byte chidinhgoi = 0;
        byte chidinhchiphithem = 0;
        #region "khai báo khởi tạo ban đầu"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nhomchidinh">Mô tả thêm của nhóm chỉ định CLS dùng cho việc tách form kê chỉ định CLS và kê gói dịch vụ</param>
        public frm_themmoi_nhomcls(string nhomchidinh)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            InitEvents();
            this.nhomchidinh = nhomchidinh;
            log = LogManager.GetCurrentClassLogger();
            chkChiDinhNhanh.Visible = globalVariables.IsAdmin;
            if (globalVariables.gv_UserAcceptDeleted) FormatUserNhapChiDinh();
            CauHinh();
        }

        void InitEvents()
        {
            Load += new EventHandler(frm_themmoi_nhomcls_Load);
            KeyDown += new KeyEventHandler(frm_themmoi_nhomcls_KeyDown);
            FormClosing += new FormClosingEventHandler(frm_themmoi_nhomcls_FormClosing);

            grdServiceDetail.CellValueChanged += new ColumnActionEventHandler(grdServiceDetail_CellValueChanged);
            grdServiceDetail.Enter += new EventHandler(grdServiceDetail_Enter);
            grdServiceDetail.KeyDown += new KeyEventHandler(grdServiceDetail_KeyDown);
            grdServiceDetail.KeyPress += new KeyPressEventHandler(grdServiceDetail_KeyPress);
            grdServiceDetail.SelectionChanged += new EventHandler(grdServiceDetail_SelectionChanged);
            grdServiceDetail.UpdatingCell += new UpdatingCellEventHandler(grdServiceDetail_UpdatingCell);

            grdAssignDetail.CellUpdated += new ColumnActionEventHandler(grdAssignDetail_CellUpdated);
            grdAssignDetail.CellValueChanged += new ColumnActionEventHandler(grdAssignDetail_CellValueChanged);
            grdAssignDetail.ColumnHeaderClick += new ColumnActionEventHandler(grdAssignDetail_ColumnHeaderClick);
            grdAssignDetail.FormattingRow += new RowLoadEventHandler(grdAssignDetail_FormattingRow);
            grdAssignDetail.UpdatingCell += new UpdatingCellEventHandler(grdAssignDetail_UpdatingCell);
            grdAssignDetail.KeyDown += new KeyEventHandler(grdAssignDetail_KeyDown);
            cboDichVu.SelectedIndexChanged += new EventHandler(cboDichVu_SelectedIndexChanged);
            txtFilterName.KeyDown += new KeyEventHandler(txtFilterName_KeyDown);
            txtFilterName.TextChanged += new EventHandler(txtFilterName_TextChanged);
            cmdCauHinh.Click += new EventHandler(cmdCauHinh_Click);

            chkChiDinhNhanh.CheckedChanged += new EventHandler(chkChiDinhNhanh_CheckedChanged);
            txtLoainhom._OnShowData += txtLoainhom__OnShowData;
            cmdExit.Click += new EventHandler(cmdExit_Click);
            cmdSave.Click += new EventHandler(cmdSave_Click);
            cmdDelete.Click += new EventHandler(cmdDelete_Click);
            mnuDelete.Click += new EventHandler(mnuDelete_Click);


        }

        void txtLoainhom__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtLoainhom.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtLoainhom.myCode;
                txtLoainhom.Init();
                txtLoainhom.SetCode(oldCode);
                txtLoainhom.Focus();
            }
        }

        void grdAssignDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.Delete) || e.KeyCode == Keys.Delete)
                mnuDelete_Click(mnuDelete, new EventArgs());
        }
        #endregion


        private void CauHinh()
        {
            if (PropertyLib._ThamKhamProperties != null)
            {
                chkChiDinhNhanh.Checked = PropertyLib._ThamKhamProperties.Chidinhnhanh;
            }
        }
        private void frm_themmoi_nhomcls_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (grdAssignDetail.RowCount > 0)
            {
                if (!isSaved)
                {
                    if (m_dtChitietNhom.Select("NoSave=1").Length > 0)
                    {
                        if (
                            Utility.AcceptQuestion(
                                "Bạn đã thêm mới một số chỉ định chi tiết vào nhóm mà chưa nhấn Ghi.\nBạn nhấn yes để hệ thống tự động lưu thông tin.\nNhấn No để hủy bỏ các chỉ định vừa thêm mới.", "Thông báo",
                                true))
                        {
                            cmdSave.PerformClick();
                        }
                    }
                }
            }
            SaveCauHinh();
        }

        private void SaveCauHinh()
        {
            if (PropertyLib._ThamKhamProperties != null)
            {
                PropertyLib._ThamKhamProperties.Chidinhnhanh = chkChiDinhNhanh.Checked;
                PropertyLib.SaveProperty(PropertyLib._ThamKhamProperties);
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
        }
        private void FormatUserNhapChiDinh()
        {
            try
            {
                GridEXColumn gridExColumn =
                    grdAssignDetail.RootTable.Columns[DmucNhomcanlamsangChitiet.Columns.NguoiTao];
                GridEXColumn gridExColumnTarget =
                    grdAssignDetail.RootTable.Columns[DmucDichvuclsChitiet.Columns.TenChitietdichvu];
                var gridExFormatCondition = new GridEXFormatCondition(gridExColumn, ConditionOperator.NotEqual,
                                                                      globalVariables.UserName);
                gridExFormatCondition.FormatStyle.BackColor = Color.Red;
                gridExFormatCondition.TargetColumn = gridExColumnTarget;
                grdAssignDetail.RootTable.FormatConditions.Add(gridExFormatCondition);
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
        }
        /// <summary>
        /// hàm thực hiện việc đống form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// hàm hự hiện việc load form hiện tại lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themmoi_nhomcls_Load(object sender, EventArgs e)
        {
            try
            {
                txtLoainhom.Init();
                DataBinding.BindDataCombobox(cboDichVu, THU_VIEN_CHUNG.LayThongTinDichVuCLS(nhomchidinh),
                                           DmucDichvucl.Columns.IdDichvu, DmucDichvucl.Columns.TenDichvu,
                                           "Lọc thông tin theo loại dịch vụ", false);

                InitData();
                GetData();
                if (m_eAction == action.Insert)
                    txtManhom.Focus();
                else
                    txtFilterName.Focus();
            }
            catch
            {
            }
            finally
            {
                v_AssignId = Utility.Int32Dbnull(txtId.Text, -1);
            }
        }
      
        /// <summary>
        /// hàm thực hiện việc xóa thông tin trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidDataXoaCLS()) return;
                foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
                {
                    long IdChitiet = Utility.Int64Dbnull(gridExRow.Cells[DmucNhomcanlamsangChitiet.Columns.IdChitiet].Value, -1);
                    CHIDINH_CANLAMSANG.XoaCLSChitietKhoinhom(IdChitiet);
                    gridExRow.Delete();
                    grdAssignDetail.UpdateData();
                    grdAssignDetail.Refresh();
                    m_dtChitietNhom.AcceptChanges();
                }
                //SqlQuery sqlQuery = new Select().From(DmucNhomcanlamsangChitiet.Schema)
                //    .Where(DmucNhomcanlamsangChitiet.Columns.IdNhom).IsEqualTo(Utility.Int32Dbnull(txtId.Text, -1));
                //if (sqlQuery.GetRecordCount() <= 0)
                //{
                //    if (
                //        Utility.AcceptQuestion(
                //            "Nhóm chỉ định cận lâm sàng đang chọn không còn chi tiết nữa, Bạn có muốn xóa nhóm này không?",
                //            "Thông báo", true))
                //    {
                //        new Delete().From(DmucNhomcanlamsang.Schema)
                //            .Where(DmucNhomcanlamsang.Columns.Id).IsEqualTo(Utility.Int32Dbnull(txtId.Text, -1)).
                //            Execute();
                //        m_eAction = action.Insert;
                //        txtId.Text = "(Tự sinh)";
                //    }
                //}
                m_blnCancel = false;
                ModifyCommand();
                ModifyButtonCommand();
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// hàm thực hiện kiểm tra thông tin chỉ dịnh cận lâm sàng
        /// </summary>
        /// <returns></returns>
        private bool IsValidDataXoaCLS()
        {
            if (grdAssignDetail.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải thực hiện chọn một chi tiết cận lâm sàng cần xóa khỏi nhóm",
                                "Thông báo", MessageBoxIcon.Warning);
                grdAssignDetail.Focus();
                return false;
            }

            if (!globalVariables.IsAdmin)
            {
                foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
                {
                    int IdChitiet_ID =
                        Utility.Int32Dbnull(gridExRow.Cells[DmucNhomcanlamsangChitiet.Columns.IdChitiet].Value, -1);
                    SqlQuery sqlQuery = new Select().From(DmucNhomcanlamsangChitiet.Schema)
                        .Where(DmucNhomcanlamsangChitiet.Columns.IdChitiet).IsEqualTo(IdChitiet_ID)
                        .And(DmucNhomcanlamsangChitiet.Columns.NguoiTao).IsNotEqualTo(globalVariables.UserName);
                    if (sqlQuery.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Trong các dịch vụ cận lâm sàng bạn chọn xóa, có một số dịch vụ được thêm bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các dịch vụ do chính bạn thêm vào nhóm để thực hiện xóa");
                        return false;
                       
                    }
                }
            }
            return true;
        }

        private bool IsValidDataXoaCLS_Selected()
        {
            if (grdAssignDetail.RowCount <= 0 || grdAssignDetail.CurrentRow.RowType != RowType.Record)
            {
                Utility.ShowMsg("Bạn phải thực hiện chọn một chi tiết cận lâm sàng cần xóa khỏi nhóm",
                                "Thông báo", MessageBoxIcon.Warning);
                grdAssignDetail.Focus();
                return false;
            }
            int IdChitiet_ID = -1;
            SqlQuery sqlQuery = null;
            if (!globalVariables.IsAdmin)
            {

                IdChitiet_ID =
                    Utility.Int32Dbnull(
                        grdAssignDetail.CurrentRow.Cells[DmucNhomcanlamsangChitiet.Columns.IdChitiet].Value, -1);
                sqlQuery = new Select().From(DmucNhomcanlamsangChitiet.Schema)
                    .Where(DmucNhomcanlamsangChitiet.Columns.IdChitiet).IsEqualTo(IdChitiet_ID)
                   .And(DmucNhomcanlamsangChitiet.Columns.NguoiTao).IsNotEqualTo(globalVariables.UserName);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Trong các dịch vụ cận lâm sàng bạn chọn xóa, có một số dịch vụ được thêm bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các dịch vụ do chính bạn thêm vào nhóm để thực hiện xóa");
                    return false;
                }
            }
            
            return true;
        }

        /// <summary>
        /// hàm thực hiện việc lưu lại thông tin của dịch vụ cận lâm sàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!IsValidData()) return;
            isSaved = true;
            SetSaveStatus();
            PerformAction();
        }

        /// <summary>
        /// hàm thực hiện việc kiểm tra lại thông tin 
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            if (Utility.DoTrim(txtManhom.Text)=="")
            {
                Utility.SetMsg(uiStatusBar1.Panels["lblStatus"], "Bạn cần nhập mã nhóm chỉ định", true);
                txtManhom.Focus();
                return false;
            }
            if (Utility.DoTrim(txtTennhom.Text) == "")
            {
                Utility.SetMsg(uiStatusBar1.Panels["lblStatus"], "Bạn cần nhập tên nhóm chỉ định", true);
                txtTennhom.Focus();
                return false;
            }
            if (Utility.DoTrim(txtLoainhom.myCode) == "-1")
            {
                Utility.SetMsg(uiStatusBar1.Panels["lblStatus"], "Bạn cần chọn loại nhóm chỉ định", true);
                txtLoainhom.Focus();
                return false;
            }
            if (grdAssignDetail.RowCount <= 0)
            {
                Utility.ShowMsg("Bạn cần chọn ít nhất một dịch vụ cận lâm sàng vào nhóm", "Thông báo",
                                MessageBoxIcon.Warning);
                cmdSave.Focus();
                return false;
            }

            return true;
        }

        private void ModifyCommand()
        {
            try
            {
                cmdSave.Enabled = grdAssignDetail.RowCount > 0;
                cmdDelete.Enabled = grdAssignDetail.RowCount > 0;
                cmdInPhieuCLS.Enabled = grdAssignDetail.RowCount > 0;
                ModifyButtonCommand();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// hàm thục hiện việc trạng thái của hoạt động của 
        /// 
        /// </summary>
        private void PerformAction()
        {
            try
            {
                Utility.EnableButton(cmdSave, false);
                switch (m_eAction)
                {
                    case action.Insert:
                        InsertDataCLS();
                        break;
                    case action.Update:
                        UpdateDataCLS();
                        break;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                m_blnCancel = false;
                ModifyCommand();
                Utility.EnableButton(cmdSave, true);
            }
        }

       

        /// <summary>
        /// hàm thực hiện việc thêm mới thông tin cận lâm sàng
        /// </summary>
        private void InsertDataCLS()
        {
            DmucNhomcanlamsang objDmucNhomcanlamsangs = TaoDmucNhomcanlamsang();
            actionResult =
               CHIDINH_CANLAMSANG.ThemnhomChidinhCLS(
                    objDmucNhomcanlamsangs, TaoDmucNhomcanlamsangChitiet());
            switch (actionResult)
            {
                case ActionResult.Success:
                    if (objDmucNhomcanlamsangs != null)
                    {
                        txtId.Text = Utility.sDbnull(objDmucNhomcanlamsangs.Id);
                        txtManhom.Text = Utility.sDbnull(objDmucNhomcanlamsangs.MaNhom);
                        txtTennhom.Text = Utility.sDbnull(objDmucNhomcanlamsangs.TenNhom);
                        txtMotathem.Text = Utility.sDbnull(objDmucNhomcanlamsangs.MotaThem);
                        
                        Themmoinhom(objDmucNhomcanlamsangs);
                        if (_OnActionSuccess != null) _OnActionSuccess();

                    }
                    m_eAction = action.Update;
                    m_blnCancel = false;
                     Close();
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình thêm mới thông tin", "Thông báo", MessageBoxIcon.Error);
                    break;
            }
            Utility.EnableButton(cmdSave, true);
            ModifyCommand();
        }
        private void Themmoinhom(DmucNhomcanlamsang objDmucNhomcanlamsangs)
        {
            DataRow dr = m_dtNhom.NewRow();
            dr[DmucNhomcanlamsang.Columns.Id] = objDmucNhomcanlamsangs.Id;
            dr[DmucNhomcanlamsang.Columns.MaNhom] = objDmucNhomcanlamsangs.MaNhom;
            dr[DmucNhomcanlamsang.Columns.TenNhom] = objDmucNhomcanlamsangs.TenNhom;
            dr[DmucNhomcanlamsang.Columns.MaLoainhom] = objDmucNhomcanlamsangs.MaLoainhom;
            dr[DmucNhomcanlamsang.Columns.MotaThem] = objDmucNhomcanlamsangs.MotaThem;
            dr["ten_loainhom"] = txtLoainhom.Text;
            dr[DmucNhomcanlamsang.Columns.NguoiTao] = objDmucNhomcanlamsangs.NguoiTao;
            dr[DmucNhomcanlamsang.Columns.NgayTao] = objDmucNhomcanlamsangs.NgayTao;

            m_dtNhom.Rows.Add(dr);
        }
        private void CapnhatNhom(DmucNhomcanlamsang objDmucNhomcanlamsangs)
        {
            DataRow[] arrDr = m_dtNhom.Select(DmucNhomcanlamsang.Columns.Id + "=" + objDmucNhomcanlamsangs.Id);
            if (arrDr.Length > 0)
            {
                arrDr[0][DmucNhomcanlamsang.Columns.MaNhom] = objDmucNhomcanlamsangs.MaNhom;
                arrDr[0][DmucNhomcanlamsang.Columns.TenNhom] = objDmucNhomcanlamsangs.TenNhom;
                arrDr[0][DmucNhomcanlamsang.Columns.MaLoainhom] = objDmucNhomcanlamsangs.MaLoainhom;
                arrDr[0][DmucNhomcanlamsang.Columns.MotaThem] = objDmucNhomcanlamsangs.MotaThem;
                arrDr[0]["ten_loainhom"] = txtLoainhom.Text;
                arrDr[0][DmucNhomcanlamsang.Columns.NguoiSua] = objDmucNhomcanlamsangs.NguoiSua;
                arrDr[0][DmucNhomcanlamsang.Columns.NgaySua] = objDmucNhomcanlamsangs.NgaySua;
                m_dtNhom.AcceptChanges();
            }
        }
        void modifyRegions()
        {
            pnlQuickSearch.Height = 0;
            pnlLeft.Width = 0;
            cmdSave.Visible = false;
            cmdDelete.Visible = false;
        }
        /// <summary>
        /// hmf thực hiện cập n hập thông tin cận lâm sàng
        /// </summary>
        private void UpdateDataCLS()
        {
            DmucNhomcanlamsang objDmucNhomcanlamsangs = TaoDmucNhomcanlamsang();
            actionResult =
                CHIDINH_CANLAMSANG.CapnhatnhomchidinhCLS(
                    objDmucNhomcanlamsangs, TaoDmucNhomcanlamsangChitiet());
            switch (actionResult)
            {
                case ActionResult.Success:
                    CapnhatNhom(objDmucNhomcanlamsangs);
                    if (_OnActionSuccess != null) _OnActionSuccess();
                    m_blnCancel = false;
                    Close();
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình sửa mới thông tin", "Thông báo", MessageBoxIcon.Error);
                    break;
            }
            Utility.EnableButton(cmdSave, true);
        }

        /// <summary>
        /// hàm thực hiện việc khởi tạo thông tin của phiếu chỉ định cận lâm sàng
        /// </summary>
        /// <returns></returns>
        private DmucNhomcanlamsang TaoDmucNhomcanlamsang()
        {
            DmucNhomcanlamsang objDmucNhomcanlamsangs = new DmucNhomcanlamsang();
            if (m_eAction == action.Insert)
            {
                objDmucNhomcanlamsangs = new DmucNhomcanlamsang();
                objDmucNhomcanlamsangs.IsNew = true;
                objDmucNhomcanlamsangs.NguoiTao = globalVariables.UserName;
                objDmucNhomcanlamsangs.NgayTao = globalVariables.SysDate;
            }
            else
            {
                objDmucNhomcanlamsangs = DmucNhomcanlamsang.FetchByID(Utility.Int16Dbnull(txtId.Text,-1));
                objDmucNhomcanlamsangs.IsNew = false;
                objDmucNhomcanlamsangs.MarkOld();
                objDmucNhomcanlamsangs.IsLoaded = true;
                objDmucNhomcanlamsangs.NguoiSua = globalVariables.UserName;
                objDmucNhomcanlamsangs.NgaySua= globalVariables.SysDate;
            }
            objDmucNhomcanlamsangs.SharedX = Utility.Bool2byte(chkShared.Checked);
            objDmucNhomcanlamsangs.MaNhom = Utility.DoTrim(txtManhom.Text);
            objDmucNhomcanlamsangs.TenNhom = Utility.DoTrim(txtTennhom.Text);
            objDmucNhomcanlamsangs.MaLoainhom = Utility.DoTrim(txtLoainhom.myCode);
            objDmucNhomcanlamsangs.MotaThem = Utility.DoTrim(txtMotathem.Text);
            return objDmucNhomcanlamsangs;
        }

        /// <summary>
        /// hàm thực hiện việc tạo mảng thông tin của chỉ định chi tiết cận lâm sàng
        /// </summary>
        /// <returns></returns>
        private List< DmucNhomcanlamsangChitiet> TaoDmucNhomcanlamsangChitiet()
        {
            List<DmucNhomcanlamsangChitiet> lstChitiet = new List<DmucNhomcanlamsangChitiet>();
            foreach (GridEXRow gridExRow in grdAssignDetail.GetDataRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                  DmucNhomcanlamsangChitiet  newItem = new DmucNhomcanlamsangChitiet();
                    int newID=Utility.Int32Dbnull(gridExRow.Cells[DmucNhomcanlamsangChitiet.Columns.IdChitiet].Value, -1);
                    if (newID > 0)
                    {
                        newItem = DmucNhomcanlamsangChitiet.FetchByID(newID);
                        newItem.IsLoaded = true;
                        newItem.IsNew = false;
                        newItem.MarkOld();
                    }
                    else
                    {
                        newItem = new DmucNhomcanlamsangChitiet();
                        newItem.IsNew = true;
                    }
                    newItem.IdNhom = Utility.Int16Dbnull(txtId.Text, -1);
                    newItem.IdDichvu = Utility.Int16Dbnull(gridExRow.Cells[DmucNhomcanlamsangChitiet.Columns.IdDichvu].Value, -1);
                    newItem.IdChitietdichvu = Utility.Int16Dbnull(
                        gridExRow.Cells[DmucNhomcanlamsangChitiet.Columns.IdChitietdichvu].Value, -1);

                    newItem.SoLuong = Utility.Int16Dbnull(gridExRow.Cells[DmucNhomcanlamsangChitiet.Columns.SoLuong].Value, 1);
                    if (newID <= 0)
                    {
                        newItem.NguoiTao = globalVariables.UserName;
                        newItem.NgayTao = globalVariables.SysDate;
                    }
                    lstChitiet.Add(newItem);
                }
            }
            return lstChitiet;
        }

        /// <summary>
        /// hàm thực hiẹn eviệc thây đổi thông tin trên lưới
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdAssignDetail_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            ModifyButtonCommand();
        }

        /// <summary>
        /// hàm thực hiện việc phím tắt của form thực hiện
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themmoi_nhomcls_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
                return;
            }
            if (e.KeyCode == Keys.F1) Utility.ShowMsg(Help);
            if (e.KeyCode == Keys.F11) Utility.ShowMsg(this.ActiveControl.Name);
            if (e.KeyCode == Keys.A && e.Control) ThemchitietChidinhvaonhom();
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
            if (e.KeyCode == Keys.F2)
            {
                txtFilterName.Focus();
                txtFilterName.SelectAll();
            }
            if ((e.Control && e.KeyCode == Keys.F3) || e.KeyCode == Keys.F3)
            {
                txtFilterName.Focus();
                txtFilterName.SelectAll();
            }
            if (e.KeyCode == Keys.D && e.Control) cmdDelete.PerformClick();
            if (e.Alt && e.KeyCode == Keys.M) grdServiceDetail.Select();
        }

        /// <summary>
        /// hàm thực hiện việc lọc thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilterName_TextChanged(object sender, EventArgs e)
        {
            FilterCLS();
        }

        private void txtFilterName_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Down))
            {
                Utility.focusCell(grdServiceDetail, DmucDichvucl.Columns.TenDichvu);
            }
        }
       
        private void FilterCLS()
        {
            try
            {
                m_blnAllowSelectionChanged = false;
                
                rowFilter = "1=1";
                if (!string.IsNullOrEmpty(txtFilterName.Text))
                {
                    string _value = Utility.DoTrim(txtFilterName.Text.ToUpper());
                    int rowcount = 0;
                    rowcount =
                        m_dtDmucDichvuCLS.Select(DmucDichvucl.Columns.MaDichvu + " ='" + _value +
                                                 "'").GetLength(0);
                    if (rowcount > 0)
                    {
                        rowFilter = DmucDichvucl.Columns.MaDichvu + " ='" + _value + "'";
                    }
                    else
                    {
                        rowFilter = DmucDichvuclsChitiet.Columns.TenChitietdichvu + " like '%" + _value +
                                    "%'  OR  " + DmucDichvuclsChitiet.Columns.MaChitietdichvu + " like '" + _value +
                                    "%'";

                    }
                }
                m_dtDmucDichvuCLS.DefaultView.RowFilter = "1=1";
                m_dtDmucDichvuCLS.DefaultView.RowFilter = rowFilter;
                m_dtDmucDichvuCLS.AcceptChanges();
                grdServiceDetail.Refresh();
                Application.DoEvents();
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh loc thong tin ", exception);
                rowFilter = "1=2";
            }
            finally
            {
                m_blnAllowSelectionChanged = true;
            }
        }

        /// <summary>
        /// hamf thuc hien viec them thong tin cua can lam sang
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThemchitietChidinhvaonhom()
        {
            resetNewItem();
            AddDetail();
            uncheckItems();
        }
        void resetNewItem()
        {
            if (m_dtChitietNhom == null || !m_dtChitietNhom.Columns.Contains("isnew")) return;
            foreach (DataRow dr in m_dtChitietNhom.Rows)
                dr["isnew"] = 0;
            m_dtChitietNhom.AcceptChanges();
        }
        void SetSaveStatus()
        {
            if (m_dtChitietNhom == null || !m_dtChitietNhom.Columns.Contains("isnew")) return;
            foreach (DataRow dr in m_dtChitietNhom.Rows)
                dr["NoSave"] = 0;
            m_dtChitietNhom.AcceptChanges();
        }
        private void AddDetail()
        {
            try
            {

                isSaved = false;
                bool selectnew = false;
                GridEXRow[] ArrCheckList = grdServiceDetail.GetCheckedRows();
                foreach (GridEXRow gridExRow in ArrCheckList)
                {
                    Int32 IdChitietdichvu = Utility.Int32Dbnull(gridExRow.Cells[DmucNhomcanlamsangChitiet.Columns.IdChitietdichvu].Value, -1);
                    EnumerableRowCollection<DataRow> query = from loz in m_dtChitietNhom.AsEnumerable().Cast<DataRow>()
                                                             where
                                                                 Utility.Int32Dbnull(
                                                                     loz[DmucNhomcanlamsangChitiet.Columns.IdChitietdichvu], -1) ==
                                                                 IdChitietdichvu
                                                             select loz;
                    if (query.Count() <= 0)
                    {
                        DataRow newDr = m_dtChitietNhom.NewRow();
                        newDr[DmucNhomcanlamsangChitiet.Columns.IdChitiet] = -1;
                        newDr["stt_hthi_dichvu"] =
                            Utility.Int32Dbnull(gridExRow.Cells["stt_hthi_dichvu"].Value, -1);
                        newDr["stt_hthi_chitiet"] =
                            Utility.Int32Dbnull(gridExRow.Cells["stt_hthi_chitiet"].Value, -1);


                        newDr[DmucNhomcanlamsangChitiet.Columns.IdNhom] = v_AssignId;
                        newDr[DmucNhomcanlamsangChitiet.Columns.IdDichvu] =
                            Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdDichvu].Value, -1);
                        newDr[DmucNhomcanlamsangChitiet.Columns.IdChitietdichvu] =
                            Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                        newDr["IsNew"] = 1;
                        newDr["NoSave"] = 1;
                        newDr["IsLocked"] = 0;
                        newDr[DmucNhomcanlamsangChitiet.Columns.SoLuong] = Utility.Int32Dbnull(gridExRow.Cells[DmucNhomcanlamsangChitiet.Columns.SoLuong].Value, 1);
                        newDr[DmucDichvucl.Columns.TenDichvu] = Utility.sDbnull(gridExRow.Cells[DmucDichvucl.Columns.TenDichvu].Value, "");
                        newDr[DmucDichvuclsChitiet.Columns.TenChitietdichvu] = Utility.sDbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value, "");
                        newDr[DmucNhomcanlamsangChitiet.Columns.NguoiTao] = globalVariables.UserName;
                        newDr[DmucNhomcanlamsangChitiet.Columns.NgayTao] = globalVariables.SysDate;
                        m_dtChitietNhom.Rows.Add(newDr);
                        if (!selectnew)
                        {
                            Utility.GonewRowJanus(grdAssignDetail, DmucNhomcanlamsangChitiet.Columns.IdChitietdichvu, Utility.sDbnull(newDr[DmucNhomcanlamsangChitiet.Columns.IdChitietdichvu], "0"));
                            selectnew = true;
                        }
                    }
                }

                m_dtChitietNhom.AcceptChanges();
                m_dtDmucDichvuCLS.AcceptChanges();
                ModifyButtonCommand();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                //AutoWarning();
            }
        }
       
        private void ModifyButtonCommand()
        {
            cmdDelete.Enabled = grdAssignDetail.GetCheckedRows().Length > 0;
            cmdSave.Enabled = grdAssignDetail.RowCount > 0;
        }

        private void cmdSearchKhoa_Click(object sender, EventArgs e)
        {
          
        }

        private void uncheckItems()
        {
            if (grdServiceDetail.RowCount <= 0) return;
            try
            {
                foreach (GridEXRow _item in grdServiceDetail.GetCheckedRows())
                {
                    _item.IsChecked = false;
                }
            }
            catch
            {
            }
        }

        private void grdServiceDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (grdServiceDetail.Focused)
                if (e.KeyCode == Keys.Enter)
                {
                    ThemchitietChidinhvaonhom();
                    txtFilterName.SelectAll();
                    txtFilterName.Focus();
                }
                else if (e.KeyCode == Keys.Space && Utility.sDbnull(grdServiceDetail.CurrentColumn.Key, "") != "colCHON")
                {
                    grdServiceDetail.CurrentRow.IsChecked = !grdServiceDetail.CurrentRow.IsChecked;
                    if (chkChiDinhNhanh.Checked)
                    {
                        if (grdServiceDetail.CurrentRow.IsChecked)
                        {
                            AddOneRow_ServiceDetail();
                        }
                    }
                }
        }

        /// <summary>
        /// hàm thực hiện việc kiểm tra số lượng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdAssignDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == DmucNhomcanlamsangChitiet.Columns.SoLuong)
                {
                    if (!Numbers.IsNumber(e.Value.ToString()))
                    {
                        Utility.ShowMsg("Bạn phải số lượng phải là số", "Thông báo", MessageBoxIcon.Warning);
                        e.Cancel = true;
                    }
                    int quanlity = Utility.Int32Dbnull(e.InitialValue, 1);
                    int quanlitynew = Utility.Int32Dbnull(e.Value);
                    if (quanlitynew <= 0)
                    {
                        Utility.ShowMsg("Bạn phải số lượng phải >=1", "Thông báo", MessageBoxIcon.Warning);
                        e.Value = quanlity;
                        e.Cancel = true;
                    }
                    GridEXRow _row = grdAssignDetail.CurrentRow;

                  
                    grdAssignDetail.UpdateData();

                }
            }
            catch (Exception exception)
            {
            }
            ModifyButtonCommand();
        }

        /// <summary>
        /// HÀM THỰC HIỆN VIỆC CHO PHÉ THƯC HIỆN CIỆC CHỌN GÓI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radTheoDoiTuong_CheckedChanged(object sender, EventArgs e)
        {
            InitData();
        }

        /// <summary>
        /// HÀM THƯC HIỆN VIỆC LOAD NHỮNG DANH MỤC CLS TRONG GÓI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radTrongGoi_CheckedChanged(object sender, EventArgs e)
        {
            InitData();
        }

        private void rad_TuTuc_CheckedChanged(object sender, EventArgs e)
        {
            InitData();
        }

        /// <summary>
        /// hàm thưc hiện việc nhóm cận lâm sàng thong tin 
        /// khi chọn danh sách cận lâm sàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNhom_CLS_Click(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception exception)
            {
                // throw;
            }
        }

        /// <summary>
        /// hàm thực hiệnv iệc add nhóm cls vào cls đang dùng
        /// </summary>
        /// <param name="gridExRow"></param>
        private void AddNhomCLS(GridEXRow gridExRow)
        {
          
        }

        private void grdServiceDetail_SelectionChanged(object sender, EventArgs e)
        {
            if (!m_blnAllowSelectionChanged) return;
            if (!Utility.isValidGrid(grdServiceDetail)) return;
            if (!grdServiceDetail.Focused && grdServiceDetail.CurrentColumn == null)
            {
                Utility.focusCell(grdServiceDetail, DmucDichvucl.Columns.TenDichvu);
            }
        }

        private void clearGrid(int i)
        {
            grdServiceDetail.MoveFirst();
        }

        private void grdServiceDetail_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsLetter(e.KeyChar))
                {
                    neverFound = false;
                    if (grdServiceDetail.CurrentRow == null) grdServiceDetail.MoveFirst();
                    int oldIdex = grdServiceDetail.CurrentRow.Position;
                    CurrentRowIndex = grdServiceDetail.CurrentRow.Position;
                    bool hasFound = false;
                    bool lastIdx = false;
                    string _Keyvalue = e.KeyChar.ToString().ToUpper();
                    object value;
                    if (CurrentRowIndex + 1 > grdServiceDetail.RowCount - 1)
                    {
                        grdServiceDetail.MoveFirst();
                        grdServiceDetail.Focus();
                        for (int j = 0; j <= oldIdex; j++)
                        {
                            value = grdServiceDetail.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value;
                            if (value != null &&
                                value.ToString().TrimStart().TrimEnd().Substring(0, 1).ToUpper().TrimStart().TrimEnd() ==
                                _Keyvalue)
                                hasFound = true;
                            if (hasFound) break;
                            if (!hasFound && j <= grdServiceDetail.RowCount - 1)
                            {
                                grdServiceDetail.MoveNext();
                                grdServiceDetail.Focus();
                            }
                        }
                    }
                    else
                    {
                        for (int i = CurrentRowIndex + 1; i <= grdServiceDetail.RowCount - 1; i++)
                        {
                            grdServiceDetail.MoveNext();
                            grdServiceDetail.Focus();
                            value = grdServiceDetail.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value;
                            if (value != null &&
                                value.ToString().TrimStart().TrimEnd().Substring(0, 1).ToUpper().TrimStart().TrimEnd() ==
                                _Keyvalue)
                                hasFound = true;

                            if (hasFound) break;
                        }
                        if (!hasFound && oldIdex > 0)
                        {
                            grdServiceDetail.MoveFirst();
                            grdServiceDetail.Focus();

                            for (int j = 0; j <= oldIdex; j++)
                            {
                                value = grdServiceDetail.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value;
                                if (value != null &&
                                    value.ToString().TrimStart().TrimEnd().Substring(0, 1).ToUpper().TrimStart().TrimEnd() ==
                                    _Keyvalue)
                                    hasFound = true;
                                if (hasFound) break;
                                if (!hasFound && j <= grdServiceDetail.RowCount - 1)
                                {
                                    grdServiceDetail.MoveNext();
                                    grdServiceDetail.Focus();
                                }
                            }
                        }
                    }
                    if (!hasFound) grdServiceDetail.MoveToRowIndex(oldIdex - 1);
                    CurrentRowIndex = grdServiceDetail.CurrentRow.Position;
                }
            }
            catch (Exception)
            {
                
                
            }
            
        }

        private void grdAssignDetail_FormattingRow(object sender, RowLoadEventArgs e)
        {
            if (e.Row.RowType == RowType.TotalRow)
            {
                e.Row.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value = "Tổng cộng :";
            }
        }

        private void cmdNhom_CLS_Click_1(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin của dịch vụ khi lọc thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _rowFilter = "1=1";
            try
            {
                if (cboDichVu.SelectedIndex > 0)
                {
                    _rowFilter = string.Format("{0}={1}", DmucDichvuclsChitiet.Columns.IdDichvu,
                                               Utility.Int32Dbnull(cboDichVu.SelectedValue, -1));
                }
            }

            catch (Exception)
            {
                _rowFilter = "1=1";
            }
            m_dtDmucDichvuCLS.DefaultView.RowFilter = _rowFilter;
            m_dtDmucDichvuCLS.AcceptChanges();
            
            if (grdServiceDetail.RowCount > 0)
            {
                grdServiceDetail.Focus();
                grdServiceDetail.MoveFirst();
                Janus.Windows.GridEX.GridEXColumn gridExColumn = grdServiceDetail.RootTable.Columns[DmucDichvucl.Columns.TenDichvu];
                grdServiceDetail.Col = gridExColumn.Position;
            }
        }


        private void grdServiceDetail_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            if (chkChiDinhNhanh.Checked)
            {
                if (grdServiceDetail.CurrentRow.IsChecked)
                {
                    AddOneRow_ServiceDetail();
                }
            }
        }

        private void AddOneRow_ServiceDetail()
        {
            try
            {
                GridEXRow gridExRow = grdServiceDetail.CurrentRow;
                resetNewItem();
                Int32 IdChitietdichvu = Utility.Int32Dbnull(gridExRow.Cells[DmucNhomcanlamsangChitiet.Columns.IdChitietdichvu].Value, -1);
                EnumerableRowCollection<DataRow> query = from loz in m_dtChitietNhom.AsEnumerable().Cast<DataRow>()
                                                         where
                                                             Utility.Int32Dbnull(
                                                                 loz[DmucNhomcanlamsangChitiet.Columns.IdChitietdichvu], -1) ==
                                                             IdChitietdichvu
                                                         select loz;
                if (query.Count() <= 0)
                {
                    DataRow newDr = m_dtChitietNhom.NewRow();
                    newDr[DmucNhomcanlamsangChitiet.Columns.IdChitiet] = -1;
                    newDr["stt_hthi_dichvu"] =
                        Utility.Int32Dbnull(gridExRow.Cells["stt_hthi_dichvu"].Value, -1);
                    newDr["stt_hthi_chitiet"] =
                        Utility.Int32Dbnull(gridExRow.Cells["stt_hthi_chitiet"].Value, -1);


                    newDr[DmucNhomcanlamsangChitiet.Columns.IdNhom] = v_AssignId;
                    newDr[DmucNhomcanlamsangChitiet.Columns.IdDichvu] =
                        Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdDichvu].Value, -1);
                    newDr[DmucNhomcanlamsangChitiet.Columns.IdChitietdichvu] =
                        Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                    newDr["IsNew"] = 1;
                    newDr["NoSave"] = 1;
                    newDr["IsLocked"] = 0;
                    newDr[DmucNhomcanlamsangChitiet.Columns.SoLuong] = Utility.Int32Dbnull(gridExRow.Cells[DmucNhomcanlamsangChitiet.Columns.SoLuong].Value, 1);
                    newDr[DmucDichvucl.Columns.TenDichvu] = Utility.sDbnull(gridExRow.Cells[DmucDichvucl.Columns.TenDichvu].Value, "");
                    newDr[DmucDichvuclsChitiet.Columns.TenChitietdichvu] = Utility.sDbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value, "");

                    newDr[DmucNhomcanlamsangChitiet.Columns.NguoiTao] = globalVariables.UserName;
                    newDr[DmucNhomcanlamsangChitiet.Columns.NgayTao] = globalVariables.SysDate;
                    m_dtChitietNhom.Rows.Add(newDr);
                    Utility.GonewRowJanus(grdAssignDetail, DmucNhomcanlamsangChitiet.Columns.IdChitietdichvu, Utility.sDbnull(newDr[DmucNhomcanlamsangChitiet.Columns.IdChitietdichvu], "0"));
                    m_dtDmucDichvuCLS.AcceptChanges();
                }
            }
            catch
            {
            }
            finally
            {
                ModifyButtonCommand();
            }
        }

        private void chkChiDinhNhanh_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void grdAssignDetail_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
            ModifyButtonCommand();
        }

        private void grdAssignDetail_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            ModifyButtonCommand();
        }
        private void grdServiceDetail_Enter(object sender, EventArgs e)
        {
           // grdServiceDetail.Col = 0;
        }
        private void mnuDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidDataXoaCLS_Selected()) return;

                long IdChitiet =
                    Utility.Int64Dbnull(grdAssignDetail.CurrentRow.Cells[DmucNhomcanlamsangChitiet.Columns.IdChitiet].Value, -1);

                CHIDINH_CANLAMSANG.XoaCLSChitietKhoinhom(IdChitiet);
                if (_OnActionSuccess != null) _OnActionSuccess();
                grdAssignDetail.CurrentRow.Delete();
                grdAssignDetail.UpdateData();
                grdAssignDetail.Refresh();
                m_dtChitietNhom.AcceptChanges();
                m_blnCancel = false;
                ModifyCommand();
                ModifyButtonCommand();
            }
            catch
            {
            }
        }
        /// <summary>
        /// hàm thực hiện việc cấu hình của cls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._HISCLSProperties);
            frm.ShowDialog();
            CauHinh();
            //InitData();
        }
        private void chkSaveAndPrint_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lưu trạng thái-->" + ex.Message);
            }
        }
      

        private void grdServiceDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == DmucNhomcanlamsangChitiet.Columns.SoLuong)
                {
                    if (!Numbers.IsNumber(e.Value.ToString()))
                    {
                        Utility.ShowMsg("Bạn phải số lượng phải là số", "Thông báo", MessageBoxIcon.Warning);
                        e.Cancel = true;
                    }
                    int quanlity = Utility.Int32Dbnull(e.InitialValue, 1);
                    int quanlitynew = Utility.Int32Dbnull(e.Value);
                    if (quanlitynew <= 0)
                    {
                        Utility.ShowMsg("Bạn phải số lượng phải >=1", "Thông báo", MessageBoxIcon.Warning);
                        e.Value = quanlity;
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }
        #region "Event Common"
        private int ID_Goi_Dvu = -1;
        /// <summary>
        /// hàm thực hiện việc lấy thông tin 
        /// </summary>
        private void GetData()
        {
            DmucNhomcanlamsang objDmucNhomcanlamsangs = DmucNhomcanlamsang.FetchByID(Utility.Int32Dbnull(txtId.Text, -1));
            if (objDmucNhomcanlamsangs != null)
            {
                txtId.Text = objDmucNhomcanlamsangs.Id.ToString();
                txtManhom.Text = objDmucNhomcanlamsangs.MaNhom;
                txtTennhom.Text = objDmucNhomcanlamsangs.TenNhom;
                txtMotathem.Text = objDmucNhomcanlamsangs.MotaThem;
                txtLoainhom.SetCode(objDmucNhomcanlamsangs.MaLoainhom);
                chkShared.Checked = Utility.Byte2Bool(objDmucNhomcanlamsangs.SharedX);
            }
            else
            {
                txtId.Text = "Tự sinh";
                txtManhom.Text = "";
                txtTennhom.Text = "";
                txtMotathem.Text = "";
                txtManhom.Focus();
                chkShared.Checked = true;
            }
            LayThongTin_Chitiet_CLS();
        }
       
        /// <summary>
        /// ham thc hien viecj lay thông tin chi tiết của cls
        /// </summary>
        private void LayThongTin_Chitiet_CLS()
        {
            try
            {
                m_dtChitietNhom = CHIDINH_CANLAMSANG.DmucLaychitietNhomchidinhCls(Utility.Int32Dbnull(txtId.Text, -1));
                Utility.SetDataSourceForDataGridEx(grdAssignDetail, m_dtChitietNhom, false, true, "1=1",
                                                   "stt_hthi_dichvu,stt_hthi_chitiet," + DmucDichvuclsChitiet.Columns.TenChitietdichvu);
                grdAssignDetail.MoveFirst();
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lấy dữ liệu CLS chi tiết\n" + ex.Message);
            }
        }
        /// <summary>
        /// khởi tạo thông tin của dữ liệu
        /// </summary>
        private void InitData()
        {
            try
            {
                string MA_KHOA_THIEN = globalVariables.MA_KHOA_THIEN;
                m_dtDmucDichvuCLS = CHIDINH_CANLAMSANG.DmucLaydanhmucclsTaonhomchidinh( nhomchidinh);
                if (!m_dtDmucDichvuCLS.Columns.Contains(DmucNhomcanlamsangChitiet.Columns.SoLuong))
                    m_dtDmucDichvuCLS.Columns.Add(DmucNhomcanlamsangChitiet.Columns.SoLuong, typeof(int));
                if (!m_dtDmucDichvuCLS.Columns.Contains("ten_donvitinh"))
                    m_dtDmucDichvuCLS.Columns.Add("ten_donvitinh", typeof(string));
                m_dtDmucDichvuCLS.AcceptChanges();
                Utility.SetDataSourceForDataGridEx(grdServiceDetail, m_dtDmucDichvuCLS, false, true, "", "");
                GridEXColumn gridExColumnGroupIntOrder = grdServiceDetail.RootTable.Columns["stt_hthi_dichvu"];
                GridEXColumn gridExColumnIntOrder = grdServiceDetail.RootTable.Columns["stt_hthi_chitiet"];
                Utility.SetGridEXSortKey(grdServiceDetail, gridExColumnGroupIntOrder, SortOrder.Ascending);
                Utility.SetGridEXSortKey(grdServiceDetail, gridExColumnIntOrder, SortOrder.Ascending);
                txtFilterName.Focus();
                txtFilterName.SelectAll();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi trong quá trình load thông tin :" + exception);
            }
        }
        #endregion
    }
}