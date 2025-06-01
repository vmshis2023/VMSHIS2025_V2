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
    public partial class frm_themnhanh_nhomcls : Form
    {
        public delegate void OnActionSuccess();
        public event OnActionSuccess _OnActionSuccess;
        private readonly Logger log;
        private string Help =
            "Tạo nhóm chỉ định cận lâm sàng";
        KCB_CHIDINH_CANLAMSANG CHIDINH_CANLAMSANG = new KCB_CHIDINH_CANLAMSANG();
        KCB_THAMKHAM __THAMKHAM = new KCB_THAMKHAM();
        public GridEX grdAssignDetail;
        private ActionResult actionResult = ActionResult.Error;
        public action m_eAction = action.Insert;
        public bool m_blnCancel = true;
        #region "khai báo khởi tạo ban đầu"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nhomchidinh">Mô tả thêm của nhóm chỉ định CLS dùng cho việc tách form kê chỉ định CLS và kê gói dịch vụ</param>
        public frm_themnhanh_nhomcls(string nhomchidinh)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            InitEvents();
            log = LogManager.GetCurrentClassLogger();
        }

        void InitEvents()
        {
            Load += new EventHandler(frm_themnhanh_nhomcls_Load);
            KeyDown += new KeyEventHandler(frm_themnhanh_nhomcls_KeyDown);
            txtLoainhom._OnShowData += txtLoainhom__OnShowData;
            cmdExit.Click += new EventHandler(cmdExit_Click);
            cmdSave.Click += new EventHandler(cmdSave_Click);
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
              
        #endregion

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
        private void frm_themnhanh_nhomcls_Load(object sender, EventArgs e)
        {
            try
            {
                txtLoainhom.Init();
                InitData();
                if (m_eAction == action.Insert)
                    txtManhom.Focus();
            }
            catch
            {
            }
            finally
            {
            }
        }
        /// <summary>
        /// hàm thực hiện việc lưu lại thông tin của dịch vụ cận lâm sàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!IsValidData()) return;
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
                Utility.SetMsg(lblMsg, "Bạn cần nhập mã nhóm chỉ định", true);
                txtManhom.Focus();
                return false;
            }
            if (Utility.DoTrim(txtTennhom.Text) == "")
            {
                 Utility.SetMsg(lblMsg, "Bạn cần nhập tên nhóm chỉ định", true);
                txtTennhom.Focus();
                return false;
            }
            if (Utility.DoTrim(txtLoainhom.myCode) == "-1")
            {
                 Utility.SetMsg(lblMsg, "Bạn cần chọn loại nhóm chỉ định", true);
                txtLoainhom.Focus();
                return false;
            }
           

            return true;
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
                    default:
                        break;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                m_blnCancel = false;
                
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
            foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                  DmucNhomcanlamsangChitiet  newItem = new DmucNhomcanlamsangChitiet();
                  int newID = -1;
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
        /// hàm thực hiện việc phím tắt của form thực hiện
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themnhanh_nhomcls_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
                return;
            }
            if (e.KeyCode == Keys.F1) Utility.ShowMsg(Help);
            if (e.KeyCode == Keys.F11) Utility.ShowMsg(this.ActiveControl.Name);
          
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
           
        }
        private int ID_Goi_Dvu = -1;
        /// <summary>
        /// hàm thực hiện việc lấy thông tin 
        /// </summary>
        private void InitData()
        {
            DmucNhomcanlamsang objDmucNhomcanlamsangs = DmucNhomcanlamsang.FetchByID(Utility.Int32Dbnull(txtId.Text, -1));
            if (objDmucNhomcanlamsangs != null)
            {
                txtId.Text = objDmucNhomcanlamsangs.Id.ToString();
                txtManhom.Text = objDmucNhomcanlamsangs.MaNhom;
                txtTennhom.Text = objDmucNhomcanlamsangs.TenNhom;
                txtMotathem.Text = objDmucNhomcanlamsangs.MotaThem;
                txtLoainhom.SetCode(objDmucNhomcanlamsangs.MaLoainhom);
            }
            else
            {
                txtId.Text = "Tự sinh";
                txtManhom.Text = "";
                txtTennhom.Text = "";
                txtMotathem.Text = "";
                txtManhom.Focus();
            }
        }
    }
}