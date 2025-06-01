using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using VNS.Libs;
using VMS.HIS.DAL;
using SubSonic;
using VNS.HIS.UI.DANHMUC;

namespace VNS.HIS.UI.THUOC
{
    public partial class frm_danhmuc_doitac : Form
    {
        //Vùng này khai báo các biến cục bộ dùng trong Class

        #region "Public Variables(Class Level)
        public bool m_blnCancel = true;
        /// <summary>
        /// Biến xác định xem form được gọi từ đâu
        /// true: Gọi từ Menu
        /// false: Gọi từ một Form khác và thường trả về đối tượng khi chọn trên lưới hoặc nhấn nút chọn
        /// </summary>
        public bool m_blnCallFromMenu = true;
        /// <summary>
        /// Biến trả về khi thực hiện DoubleClick trên lưới với điều kiện blnCallFromMenu=false
        /// </summary>
        public DmucDoitac m_objObjectReturn = null; 

        //Khai báo biến cho CrytalReport
       // private  CrystalDecisions.CrystalReports.Engine.ReportDocument crpt = new crpt_DrugType();         
        #endregion

        #region "Private Variables(Class Level)"
        
        /// <summary>
        /// Tên của DLL chứa Form này. Được sử dụng cho mục đích SetMultilanguage và cấu hình DataGridView
        /// </summary>
        private string AssName = "";
        /// <summary>
        /// Datasource là danh sách Country hiển thị trên lưới
        /// </summary>
        private DataTable m_dtDoitac = new DataTable();
        /// <summary>
        /// Có cho phép phản ánh dữ liệu trên lưới vào các Control hay không? 
        /// Mục đích khi nhấn Insert, Delete thì khi chọn trên lưới sẽ ko thay đổi dữ liệu trong các Control bên dưới.
        /// Ở chế độ bình thường thì khi chọn trên lưới sẽ phản ánh dữ liệu xuống các Control để sẵn sàng thao tác.
        /// </summary>
        private bool m_blnAllowCurrentCellChangedOnGridView = true;
        /// <summary>
        /// Thao tác đang thực hiện là gì: Insert, Delete, Update hay Select...
        /// </summary>
        private action m_enAction;
        /// <summary>
        /// Biến để tránh trường hợp khi gán Datasource cho GridView thì xảy ra sự kiện CurrentCellChanged
        /// Điều này là do 2 Thread thực hiện song song nhau. Do vậy ta cần xử lý nếu chưa Loaded xong thì
        /// chưa cho phép binding dữ liệu từ Gridview vào Control trên Form trong sự kiện CurrentCellChanged
        /// </summary>
        private bool m_blnLoaded = false;
        private Int16 m_shtOldPos = 0;
        private SubSonic.Query m_Query ;
        private string kieu_thuoc_vt = "THUOC";
        #endregion
        //Các phương thức khởi tạo của Class
        #region "Constructors"
        public frm_danhmuc_doitac()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            m_Query = DmucDoitac.CreateQuery();
            InitEvents();
        }
        void InitEvents()
        {
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            this.Load += new System.EventHandler(this.frm_danhmuc_doitac_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_danhmuc_doitac_KeyDown);
            txtTendoitac.LostFocus += new EventHandler(txtName_LostFocus);
            grdList.CurrentCellChanged += new EventHandler(grdList_CurrentCellChanged);
            grdList.UpdatingCell += grdList_UpdatingCell;
            grdList.DoubleClick += new EventHandler(grdList_DoubleClick);
            grdList.KeyDown += new KeyEventHandler(grdList_KeyDown);
            txtPos.KeyPress += new KeyPressEventHandler(txtPos_KeyPress);
            
            txtNguonGthieu._OnShowData += txtKieuthuocVT__OnShowData;
        }

        void grdList_UpdatingCell(object sender, Janus.Windows.GridEX.UpdatingCellEventArgs e)
        {
            if (e.Column.Key == "stt")
            {
                DmucDoitac objTinhchatthuoc = DmucDoitac.FetchByID(Utility.Int64Dbnull(grdList.CurrentRow.Cells[DmucDoitac.Columns.Id].Value, 0));
                if (objTinhchatthuoc != null)
                {
                    objTinhchatthuoc.MarkOld();
                    objTinhchatthuoc.IsNew = false;
                    objTinhchatthuoc.Stt = Utility.Int16Dbnull(e.Value, 0);
                    objTinhchatthuoc.Save();
                    grdList.Refetch();
                }
            }
            //if (e.Column.Key == "report_code")
            //{
            //    DmucDoitac objTinhchatthuoc = DmucDoitac.FetchByID(Utility.Int64Dbnull(grdList.CurrentRow.Cells[DmucDoitac.Columns.Id].Value, 0));
            //    if (objTinhchatthuoc != null)
            //    {
            //        objTinhchatthuoc.MarkOld();
            //        objTinhchatthuoc.IsNew = false;
            //        objTinhchatthuoc.ReportCode = Utility.sDbnull(e.Value, "");
            //        objTinhchatthuoc.Save();
            //        grdList.Refetch();
            //    }
            //}
        }
       
        void txtKieuthuocVT__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtNguonGthieu.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtNguonGthieu.myCode;
                txtNguonGthieu.Init();
                txtNguonGthieu.SetCode(oldCode);
                txtNguonGthieu.Focus();
            }
        }

       
      
        #endregion

        //Vùng này chứa các thuộc tính để thao tác với các đối tượng khác 
        //Hiện tại ko dùng

        #region "Public Properties"
        #endregion

        #region "Private Methods"

        #region "Private Methods including Common methods and functions: Initialize,IsValidData, SetControlStatus,..."
        /// <summary>
        /// Kiểm tra tính hợp lệ của dữ liệu trước khi đóng gói dữ liệu vào Entity
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            Utility.SetMsg(lblMsg, "", false);
            if (String.IsNullOrEmpty(txtID.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập mã đối tác" , true);
                txtID.Focus();
                return false;
            }
            if (!Utility.IsNumeric(txtPos.Text))
            {
                Utility.SetMsg(lblMsg, "Số thứ tự phải là chữ số.",true);
                txtPos.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtTendoitac.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập tên đối tác" , true);
                txtTendoitac.Focus();
                return false;
            }
            
            if (txtNguonGthieu.myCode == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nguồn giới thiệu" , true);
                txtNguonGthieu.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Thiết lập trạng thái các Control trên Form theo thao tác nghiệp vụ cần thực hiện
        /// Insert, Update hoặc Delete...
        /// </summary>
        private void SetControlStatus()
        {
            switch (m_enAction)
            {
                case action.Insert:
                    //Cho phép nhập liệu mã đối tác thuốc,vị trí, tên đối tác thuốc và mô tả thêm
                    Utility.DisabledTextBox(txtID);
                    Utility.EnabledTextBox(txtPos);
                    Utility.EnabledTextBox(txtTendoitac);
                    Utility.EnabledTextBox(txtDesc);
                    txtNguonGthieu.Enabled = true;
                    Utility.EnabledTextBox(txtMadoitac);
                    txtMadoitac.Clear();
                    txtPos.Clear();
                    txtTendoitac.Clear();
                    txtDesc.Clear();
                    Int16 MaxPos = Utility.Int16Dbnull(DmucDoitac.CreateQuery().GetMax("stt"), 0);
                    MaxPos += 1;
                    txtPos.Text = MaxPos.ToString();
                    m_shtOldPos = Convert.ToInt16(txtPos.Text);
                    //--------------------------------------------------------------
                    //Thiết lập trạng thái các nút Insert, Update, Delete...
                    //Không cho phép nhấn Insert, Update,Delete
                    cmdInsert.Enabled = false;
                    cmdUpdate.Enabled = false;
                    cmdDelete.Enabled = false;
                    //Cho phép nhấn nút Ghi
                    cmdSave.Enabled = true;
                    //Nút thoát biến thành nút hủy
                    cmdClose.Text = "Hủy";
                    //--------------------------------------------------------------
                    //Không cho phép chọn trên lưới dữ liệu được fill vào các Control
                    m_blnAllowCurrentCellChangedOnGridView = false;
                    //Tự động Focus đến mục ID để người dùng nhập liệu
                    txtID.Text = "Tự sinh";
                    txtMadoitac.Focus();
                    break;
                case action.Update:
                    //Không cho phép cập nhật lại mã đối tác thuốc
                    Utility.DisabledTextBox(txtID);
                    //Cho phép cập nhật lại vị trí, tên đối tác thuốc và mô tả thêm
                    Utility.EnabledTextBox(txtTendoitac);
                    txtNguonGthieu.Enabled = true;
                    Utility.EnabledTextBox(txtDesc);
                    Utility.EnabledTextBox(txtPos);
                    Utility.EnabledTextBox(txtMadoitac);
                    
                    m_shtOldPos = Utility.Int16Dbnull(Utility.GetValueFromGridColumn(grdList, "stt"), 0);
                    //--------------------------------------------------------------
                    //Thiết lập trạng thái các nút Insert, Update, Delete...
                    //Không cho phép nhấn Insert, Update,Delete
                    cmdInsert.Enabled = false;
                    cmdUpdate.Enabled = false;
                    cmdDelete.Enabled = false;
                    //Cho phép nhấn nút Ghi
                    cmdSave.Enabled = true;
                    //Nút thoát biến thành nút hủy
                    cmdClose.Text = "Hủy";
                    //--------------------------------------------------------------
                    //Không cho phép chọn trên lưới dữ liệu được fill vào các Control
                    m_blnAllowCurrentCellChangedOnGridView = false;
                    //Tự động Focus đến mục Position để người dùng nhập liệu
                    txtMadoitac.Focus();
                    break;
                case action.FirstOrFinished://Hủy hoặc trạng thái ban đầu khi mới hiển thị Form
                    //Không cho phép nhập liệu mã đối tác thuốc, tên đối tác thuốc và mô tả thêm
                    Utility.DisabledTextBox(txtID);
                    Utility.DisabledTextBox(txtTendoitac);
                    Utility.DisabledTextBox(txtDesc);
                    txtNguonGthieu.Enabled = false;
                    Utility.DisabledTextBox(txtPos);
                    Utility.DisabledTextBox(txtMadoitac);
                    //--------------------------------------------------------------
                    //Thiết lập trạng thái các nút Insert, Update, Delete...
                    //Sau khi nhấn Ghi thành công hoặc Hủy thao tác thì quay về trạng thái ban đầu
                    //Cho phép thêm mới
                    cmdInsert.Enabled = true;
                    //Tùy biến nút Update và Delete tùy theo việc có hay không dữ liệu trên lưới
                    cmdUpdate.Enabled = grdList.RowCount <= 0 ? false : true;
                    cmdDelete.Enabled = grdList.RowCount <= 0 ? false : true;
                    cmdSave.Enabled = false;
                    //Nút Hủy biến thành nút thoát
                    cmdClose.Text = "Thoát";
                    //--------------------------------------------------------------
                    //Cho phép chọn trên lưới dữ liệu được fill vào các Control
                    m_blnAllowCurrentCellChangedOnGridView = true;
                    //Tự động chọn dòng hiện tại trên lưới để hiển thị lại trên Control
                    grdList_CurrentCellChanged(grdList, new EventArgs());
                    //Tự động Focus đến nút thêm mới? 
                    cmdInsert.Focus();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// GetObject after Double click and Keydown (Enter) events of GridView
        /// </summary>
        private void GetObject()
        {
            DataRow[] arrDr = m_dtDoitac.Select(DmucDoitac.Columns.Id + "=" + txtID.Text);

            if (arrDr.Length>0)
            {
                m_objObjectReturn = DmucDoitac.FetchByID(Convert.ToInt32(txtID.Text));
                m_blnCancel = false;
                this.Close();

            }
            else
                Utility.SetMsg(lblMsg, "Bạn hãy chọn một dòng dữ liệu trên lưới và thực hiện lại thao tác",true);
        }
        #endregion

        #region "Insert, Delete, Update,Select,..."
        /// <summary>
        /// Thực hiện nghiệp vụ Insert dữ liệu
        /// </summary>
        private void PerformInsertAction()
        {
            Utility.SetMsg(lblMsg, "", true);
            //Kiểm tra trùng tên đối tượng và cảnh báo
            DmucDoitacCollection v_arrSameObject = new DmucDoitacController().FetchByQuery(m_Query.AddWhere("ma_doitac", txtMadoitac.Text.Trim().ToUpper()));
            if (v_arrSameObject.Count > 0)
            {
                if (!Utility.AcceptQuestion("Đã có đối tác có mã:" + txtMadoitac.Text.Trim() + ". Bạn có muốn tiếp tục ghi hay không?", "Cảnh báo", true))
                {
                    //Create Again to ignore Where Clause
                    m_Query = DmucDoitac.CreateQuery();
                    txtMadoitac.Focus();
                    return;
                }
            }
            v_arrSameObject = new DmucDoitacController().FetchByQuery(m_Query.AddWhere("ten_doitac", txtTendoitac.Text.Trim().ToUpper()));
            if (v_arrSameObject.Count > 0)
            {
                if (!Utility.AcceptQuestion("Đã có đối tác có tên:" + txtTendoitac.Text.Trim() + ". Bạn có muốn tiếp tục ghi hay không?", "Cảnh báo", true))
                {
                    //Create Again to ignore Where Clause
                    m_Query = DmucDoitac.CreateQuery();
                    txtTendoitac.Focus();
                    return;
                }
            }
            //Create Again to ignore Where Clause
            m_Query = DmucDoitac.CreateQuery();
            //Gọi nghiệp vụ Insert dữ liệu
           
            DmucDoitac objDoitac=new DmucDoitac();
            objDoitac.MaDoitac = Utility.sDbnull(txtMadoitac.Text);
            objDoitac.TenDoitac = Utility.sDbnull(txtTendoitac.Text);
            objDoitac.MotaThem = Utility.sDbnull(txtDesc.Text);
            objDoitac.MaNguongioithieu = Utility.sDbnull(txtNguonGthieu.myCode, "-1");
            objDoitac.Stt = Convert.ToInt16(txtPos.Text);
            objDoitac.TrangThai = Utility.Bool2byte(chkTrangthai.Checked);
            objDoitac.TrangthaiMacdinh = Utility.Bool2byte(chkDefault.Checked);
            objDoitac.IsNew = true;
            objDoitac.Save();
            //Lấy về MaxID vừa được thêm vào CSDL
            int v_shtId = objDoitac.Id;
            //Lấy về Object vừa tạo
            DmucDoitacCollection v_arrNewObject = new DmucDoitacController().FetchByID(v_shtId);
            if (v_arrNewObject.Count > 0)//-->Thêm mới thành công
            {
                DataRow newitem=m_dtDoitac.NewRow();
                newitem["ten_nguongioithieu"] = txtNguonGthieu.Text;
                Utility.FromObjectToDatarow(v_arrNewObject[0], ref newitem);
                m_dtDoitac.Rows.Add(newitem);
                //Return to the InitialStatus
                m_enAction = action.FirstOrFinished;
                //Nhảy đến bản ghi vừa thêm mới trên lưới. Do txtID chưa bị reset nên dùng luôn
                Utility.GotoNewRowJanus(grdList, "Id", v_shtId.ToString());
                Utility.SetMsg(lblMsg, "Thêm mới dữ liệu thành công!",false);
                SetControlStatus();
                this.Activate();
            }
            else//Có lỗi xảy ra
                Utility.SetMsg(lblMsg, "Thêm mới không thành công. Mời bạn xem lại",false);
        }
        /// <summary>
        /// Thực hiện nghiệp vụ Update dữ liệu
        /// </summary>
        private void PerformUpdateAction()
        {
            Utility.SetMsg(lblMsg, "", true);

            //Gọi Business cập nhật dữ liệu
            int v_shtId = Convert.ToInt32(txtID.Text);
            //Kiểm tra trùng tên đối tượng và cảnh báo
            DmucDoitacCollection v_arrSameObject = new DmucDoitacController().FetchByQuery(m_Query.AddWhere("ma_doitac", txtMadoitac.Text.Trim().ToUpper()).AND("Id", Comparison.NotEquals, v_shtId));
            if (v_arrSameObject.Count > 0)
            {
                if (!Utility.AcceptQuestion("Đã có đối tác có mã:" + txtMadoitac.Text.Trim() + ". Bạn có muốn tiếp tục ghi hay không?", "Cảnh báo", true))
                {
                    //Create Again to ignore Where Clause
                    m_Query = DmucDoituongkcb.CreateQuery();
                    return;
                }
            }
            v_arrSameObject = new DmucDoitacController().FetchByQuery(m_Query.AddWhere("ten_doitac", txtTendoitac.Text.Trim().ToUpper()).AND("Id", Comparison.NotEquals, v_shtId));
            if (v_arrSameObject.Count > 0)
            {
                if (!Utility.AcceptQuestion("Đã có có đối tác có tên:" + txtTendoitac.Text.Trim() + ". Bạn có muốn tiếp tục ghi hay không?", "Cảnh báo", true))
                {
                    //Create Again to ignore Where Clause
                    m_Query = DmucDoituongkcb.CreateQuery();
                    return;
                }
            }
            //Create Again to ignore Where Clause
            m_Query = DmucDoitac.CreateQuery();
            DmucDoitac v_NewObjectChangePos = null;
            
            DmucDoitac objDoitac = DmucDoitac.FetchByID(Convert.ToInt16(txtID.Text));
            if (objDoitac != null)
            {
                objDoitac.MaDoitac = Utility.sDbnull(txtMadoitac.Text);
                objDoitac.TenDoitac = Utility.sDbnull(txtTendoitac.Text);
                objDoitac.MotaThem = Utility.sDbnull(txtDesc.Text);
                objDoitac.Stt = Convert.ToInt16(txtPos.Text);
                objDoitac.TrangThai = Utility.Bool2byte(chkTrangthai.Checked);
                objDoitac.TrangthaiMacdinh = Utility.Bool2byte(chkDefault.Checked);
                objDoitac.MaNguongioithieu = txtNguonGthieu.myCode; 
                objDoitac.IsNew = false;
                objDoitac.MarkOld();
                objDoitac.Save();
                
            }
            DataRow[] arrDr = m_dtDoitac.Select(DmucDoitac.Columns.Id + "=" + txtID.Text);
            if (arrDr.Length > 0)
            {
                arrDr[0][DmucDoitac.Columns.MaDoitac] = Utility.sDbnull(txtMadoitac.Text);
                arrDr[0][DmucDoitac.Columns.TenDoitac] = Utility.sDbnull(txtTendoitac.Text);
                arrDr[0][DmucDoitac.Columns.MotaThem] = Utility.sDbnull(txtDesc.Text);
                arrDr[0][DmucDoitac.Columns.Stt] = Convert.ToInt16(txtPos.Text);
                arrDr[0][DmucDoitac.Columns.TrangthaiMacdinh] = Utility.Bool2byte(chkTrangthai.Checked);
                arrDr[0][DmucDoitac.Columns.TrangthaiMacdinh] = Utility.Bool2byte(chkDefault.Checked);
                arrDr[0][DmucDoitac.Columns.MaNguongioithieu] = txtNguonGthieu.myCode;
                arrDr[0]["ten_nguongioithieu"] = txtNguonGthieu.Text;
            }
            //Return to the InitialStatus
            m_enAction = action.FirstOrFinished;
            //Nhảy đến bản ghi vừa cập nhật trên lưới. Do txtID chưa bị reset nên dùng luôn
            Utility.GotoNewRowJanus(grdList, "Id", txtID.Text.Trim());
            SetControlStatus();
            Utility.SetMsg(lblMsg, "Cập nhật dữ liệu thành công.",false);
        }
        /// <summary>
        /// Thực hiện nghiệp vụ Delete dữ liệu
        /// </summary>
        private void PerformDeleteAction()
        {
            try
            {
                if (Utility.AcceptQuestion("Bạn có muốn xóa Loại thuốc đang chọn hay không?", "Xác nhận xóa", true))
                {


                    Int16 v_shtId = Convert.ToInt16(txtID.Text.Trim());
                    string v_strMaDoitac = Utility.sDbnull(txtMadoitac.Text.Trim());
                    //Kiểm tra xem đã được sử dụng trong bảng khác chưa
                    DataTable dtCheck = new Select("1").From(KcbLuotkham.Schema).Where(KcbLuotkham.Columns.MaDoitac).IsEqualTo(v_strMaDoitac).ExecuteDataSet().Tables[0];
                    if (dtCheck.Rows.Count > 0)
                    {
                        Utility.ShowMsg(string.Format("Đối tác {0} đã được sử dụng nên bạn không thể xóa", txtTendoitac.Text));
                        return;
                    }
                    DataRow[] arrDr = m_dtDoitac.Select(DmucDoitac.Columns.Id + "=" + txtID.Text);
                    //Gọi nghiệp vụ xóa dữ liệu\
                    int Count = DmucDoitac.Delete(v_shtId);

                    if (arrDr.Length > 0)//Nếu xóa thành công trong CSDL
                    {
                        m_dtDoitac.Rows.Remove(arrDr[0]);
                        m_dtDoitac.AcceptChanges();
                        //Return to the InitialStatus
                        m_enAction = action.FirstOrFinished;
                        SetControlStatus();
                        Utility.SetMsg(lblMsg, "Đã xóa đối tác có mã: " + v_shtId + " ra khỏi hệ thống.", false);
                    }
                    else//Có lỗi xảy ra
                        Utility.SetMsg(lblMsg, "Lỗi khi xóa đối tác", true);

                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            
        }
        /// <summary>
        /// Thực hiện thao tác Insert,Update,Delete tới CSDL theo m_enAction
        /// </summary>
        private void PerformAction()
        {
            //Kiểm tra tính hợp lệ của dữ liệu trước khi thêm mới
            if (!IsValidData())
            {
                return;
            }
            switch (m_enAction)
            {
                case action.Insert:
                    PerformInsertAction();
                    break;
                case action.Update:
                    PerformUpdateAction();
                    break;
                case action.Delete:
                    PerformDeleteAction();
                    break;
                default:
                    break;
            }
            //Refresh to Acceptchanged
            grdList.Refresh();
        }
        /// <summary>
        /// Lấy danh sách quốc gia và Binding vào DataGridView
        /// </summary>
        private void GetData()
        {
            m_dtDoitac = SPs.TiepdonDmucdoitacLaytheonguongioithieu("ALL").GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, m_dtDoitac, true, true, "", "stt,ten_doitac");
        }
        #endregion
        #endregion

        #region "Event Handlers: Form Events,GridView Events, Button Events"
        /// <summary>
        /// Sự kiện Load của Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_danhmuc_doitac_Load(object sender, EventArgs e)
        {
            InitData();
           
        }
        void InitData()
        {
            try
            {
 txtNguonGthieu.Init();
            GetData();
            //Sau khi Binding dữ liệu vào GridView thì mới cho phép thực hiện lệnh trong sự kiện CurrentCellChanged
            m_blnLoaded = true;
            //Gọi sự kiện CurrentCellChanged để hiển thị dữ liệu từ trên lưới xuống Controls
            grdList_CurrentCellChanged(grdList, new EventArgs());
            //Thiết lập giá trị mặc định của action
            m_enAction = action.FirstOrFinished;
            //Thiết lập các giá trị mặc định cho các Control
            SetControlStatus();
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
        }
        void txtPos_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Utility.NumbersOnly(e.KeyChar, txtPos);
        }
        /// <summary>
        /// Xử lý sự kiện CurrentCellChanged của DataGridView
        /// Đưa dữ liệu đang chọn từ GridView vào các Controls để người dùng sẵn sàng thao tác Delete hoặc Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void grdList_CurrentCellChanged(object sender, EventArgs e)
        {
            //Chỉ cho phép khi m_blnAllowCurrentCellChangedOnGridView=true và lưới có dữ liệu
            if (m_blnLoaded && m_blnAllowCurrentCellChangedOnGridView && grdList.RowCount > 0 && grdList.CurrentRow != null)
            {
                txtID.Text = Utility.GetValueFromGridColumn(grdList, DmucDoitac.Columns.Id);
                txtMadoitac.Text = Utility.GetValueFromGridColumn(grdList, DmucDoitac.Columns.MaDoitac);
                txtTendoitac.Text = Utility.GetValueFromGridColumn(grdList, DmucDoitac.Columns.TenDoitac);
                txtDesc.Text = Utility.GetValueFromGridColumn(grdList, DmucDoitac.Columns.MotaThem);
                txtPos.Text = Utility.GetValueFromGridColumn(grdList, DmucDoitac.Columns.Stt);
                txtNguonGthieu.SetCode(Utility.GetValueFromGridColumn(grdList, DmucDoitac.Columns.MaNguongioithieu));
                chkTrangthai.Checked = Utility.Byte2Bool(Utility.GetValueFromGridColumn(grdList, "trang_thai"));
                chkDefault.Checked = Utility.Byte2Bool(Utility.GetValueFromGridColumn(grdList, "trangthai_macdinh"));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void grdList_DoubleClick(object sender, EventArgs e)
        {
            //Chỉ cho phép khi m_blnAllowCurrentCellChangedOnGridView=true và lưới có dữ liệu
            if (m_blnLoaded && m_blnAllowCurrentCellChangedOnGridView && grdList.RowCount > 0 && grdList.CurrentRow != null && !m_blnCallFromMenu)
            {
                GetObject(); 
            } 
        }
        void grdList_KeyDown(object sender, KeyEventArgs e)
        {
            grdList_DoubleClick(grdList, new EventArgs());
        }

        /// <summary>
        /// Sự kiện nhấn nút Thêm mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInsert_Click(object sender, EventArgs e)
        {
            //Đặt mã nghiệp vụ cần thực hiện là Insert. 
            //Chú ý luôn set Giá trị này trước khi gọi hàm SetControlStatus()
            m_enAction = action.Insert;
            //Đưa trạng thái các Control về trạng thái cho phép thêm mới
            SetControlStatus();
        }
        /// <summary>
        /// Sự kiện nhấn nút Cập nhật
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            //Đặt mã nghiệp vụ cần thực hiện là Update
            //Chú ý luôn set Giá trị này trước khi gọi hàm SetControlStatus()
            m_enAction = action.Update;
            //Đưa trạng thái các Control về trạng thái cho phép cập nhật
            SetControlStatus();
        }
        /// <summary>
        /// Sự kiện nhấn nút Xóa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            //Kiểm tra nếu xóa thành công thì thiết lập lại trạng thái các Control
            m_enAction = action.Delete;
            PerformAction();
            m_enAction = action.FirstOrFinished;
            SetControlStatus();
        }
        /// <summary>
        /// Sự kiện nhấn nút Thoát
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdClose_Click(object sender, EventArgs e)
        {
            if (cmdClose.Text.Trim().ToUpper() == "THOÁT")
                this.Close();
            else
            {
                m_enAction = action.FirstOrFinished;
                SetControlStatus();
            }
        }

        /// <summary>
        /// Sự kiện nhấn nút Ghi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            PerformAction();
        }
       
        /// <summary>
        /// hot key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_danhmuc_doitac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) InitData();
            else if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.N) && cmdInsert.Enabled) cmdInsert.PerformClick();
            else if ((e.KeyCode == Keys.Escape) && (cmdClose.Enabled)) cmdClose.PerformClick();
            // Ctrl + S =>Save
            else if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.S) && (cmdSave.Enabled)) cmdSave.PerformClick();
            // Ctrl + C =>Cập nhật
            else if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.C) && (cmdUpdate.Enabled)) cmdUpdate.PerformClick();
            // Del => Xoá 
            else if ((e.KeyCode == Keys.Delete) && (cmdDelete.Enabled)) cmdDelete.PerformClick();
            // Ctrl + P
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.P) cmdPrint.PerformClick();
        }
        #endregion

        private void cmdPrint_Click(object sender, EventArgs e)
        {
        }
        private void txtName_LostFocus(object sender, System.EventArgs e)
        {
           // txtName.Text = Utility.chuanhoachuoi(txtName.Text);
        }
      
    }
}
