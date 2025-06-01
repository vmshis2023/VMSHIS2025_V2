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
    public partial class frm_danhmuc_loaithuoc : Form
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
        public DmucLoaithuoc m_objObjectReturn = null; 

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
        private DataTable m_dtLoaithuoc = new DataTable();
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
        public frm_danhmuc_loaithuoc(string kieu_thuoc_vt)
        {
            InitializeComponent();
            this.kieu_thuoc_vt = kieu_thuoc_vt;
            Utility.SetVisualStyle(this);
            m_Query = DmucLoaithuoc.CreateQuery();
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
            this.Load += new System.EventHandler(this.frm_danhmuc_loaithuoc_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_danhmuc_loaithuoc_KeyDown);
            txtName.LostFocus += new EventHandler(txtName_LostFocus);
            grdList.CurrentCellChanged += new EventHandler(grdList_CurrentCellChanged);
            grdList.DoubleClick += new EventHandler(grdList_DoubleClick);
            grdList.UpdatingCell += grdList_UpdatingCell;
            grdList.KeyDown += new KeyEventHandler(grdList_KeyDown);
            txtPos.KeyPress += new KeyPressEventHandler(txtPos_KeyPress);
            txtNhom._OnShowData += txtNhom__OnShowData;
            txtKieuthuocVT._OnShowData += txtKieuthuocVT__OnShowData;
        }

        void grdList_UpdatingCell(object sender, Janus.Windows.GridEX.UpdatingCellEventArgs e)
        {
            if (e.Column.Key == "stt_hthi")
            {
                DmucLoaithuoc objDmucLoaithuoc = DmucLoaithuoc.FetchByID(Utility.Int64Dbnull(grdList.CurrentRow.Cells[DmucLoaithuoc.Columns.IdLoaithuoc].Value, 0));
                if (objDmucLoaithuoc != null)
                {
                    objDmucLoaithuoc.MarkOld();
                    objDmucLoaithuoc.IsNew = false;
                    objDmucLoaithuoc.SttHthi = Utility.Int16Dbnull(e.Value, 0);
                    objDmucLoaithuoc.Save();
                }
            }
        }
       
        void txtKieuthuocVT__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtKieuthuocVT.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtKieuthuocVT.myCode;
                txtKieuthuocVT.Init();
                txtKieuthuocVT.SetCode(oldCode);
                txtKieuthuocVT.Focus();
            }
        }

        void txtNhom__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtNhom.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtNhom.myCode;
                txtNhom.Init();
                txtNhom.SetCode(oldCode);
                txtNhom.Focus();
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
                Utility.SetMsg(lblMsg, "Bạn cần nhập mã chủng loại" , true);
                txtID.Focus();
                return false;
            }
            if (!Utility.IsNumeric(txtPos.Text))
            {
                Utility.SetMsg(lblMsg, "Số thứ tự phải là chữ số.",true);
                txtPos.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtName.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập tên chủng loại" , true);
                txtName.Focus();
                return false;
            }
            if (txtNhom.myCode=="-1")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập Nhóm thuốc" , true);
                txtNhom.Focus();
                return false;
            }
            if (txtKieuthuocVT.myCode == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập Kiểu nhóm thuốc" , true);
                txtKieuthuocVT.Focus();
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
                    //Cho phép nhập liệu mã chủng loại thuốc,vị trí, tên chủng loại thuốc và mô tả thêm
                    Utility.DisabledTextBox(txtID);
                    Utility.EnabledTextBox(txtPos);
                    Utility.EnabledTextBox(txtName);
                    Utility.EnabledTextBox(txtDesc);
                    txtNhom.Enabled = true;
                    txtKieuthuocVT.Enabled = true;
                    Utility.EnabledTextBox(txtDrug_Code);
                    txtDrug_Code.Clear();
                    txtPos.Clear();
                    txtName.Clear();
                    txtDesc.Clear();
                    Int16 MaxPos = Utility.Int16Dbnull(DmucLoaithuoc.CreateQuery().GetMax("stt_hthi"), 0);
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
                    txtDrug_Code.Focus();
                    break;
                case action.Update:
                    //Không cho phép cập nhật lại mã chủng loại thuốc
                    Utility.DisabledTextBox(txtID);
                    //Cho phép cập nhật lại vị trí, tên chủng loại thuốc và mô tả thêm
                    Utility.EnabledTextBox(txtName);
                    txtNhom.Enabled = true;
                    txtKieuthuocVT.Enabled = true;
                    Utility.EnabledTextBox(txtDesc);
                    Utility.EnabledTextBox(txtPos);
                    Utility.EnabledTextBox(txtDrug_Code);
                    
                    m_shtOldPos = Utility.Int16Dbnull(Utility.GetValueFromGridColumn(grdList, "stt_hthi"), 0);
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
                    txtDrug_Code.Focus();
                    break;
                case action.FirstOrFinished://Hủy hoặc trạng thái ban đầu khi mới hiển thị Form
                    //Không cho phép nhập liệu mã chủng loại thuốc, tên chủng loại thuốc và mô tả thêm
                    Utility.DisabledTextBox(txtID);
                    Utility.DisabledTextBox(txtName);
                    Utility.DisabledTextBox(txtDesc);
                    txtNhom.Enabled = false;
                    txtKieuthuocVT.Enabled = false;
                    Utility.DisabledTextBox(txtPos);
                    Utility.DisabledTextBox(txtDrug_Code);
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
            DataRow[] arrDr = m_dtLoaithuoc.Select(DmucLoaithuoc.Columns.IdLoaithuoc + "=" + txtID.Text);

            if (arrDr.Length>0)
            {
                m_objObjectReturn = DmucLoaithuoc.FetchByID(Convert.ToInt32(txtID.Text));
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
            DmucLoaithuocCollection v_arrSameObject = new DmucLoaithuocController().FetchByQuery(m_Query.AddWhere(DmucLoaithuoc.Columns.MaLoaithuoc, txtDrug_Code.Text.Trim().ToUpper()));
            if (v_arrSameObject.Count > 0)
            {
                if (!Utility.AcceptQuestion("Đã có nhóm thuốc có mã:" + txtDrug_Code.Text.Trim() + ". Bạn có muốn tiếp tục ghi hay không?", "Cảnh báo", true))
                {
                    //Create Again to ignore Where Clause
                    m_Query = DmucLoaithuoc.CreateQuery();
                    txtDrug_Code.Focus();
                    return;
                }
            }
            v_arrSameObject = new DmucLoaithuocController().FetchByQuery(m_Query.AddWhere("ten_loaithuoc", txtName.Text.Trim().ToUpper()));
            if (v_arrSameObject.Count > 0)
            {
                if (!Utility.AcceptQuestion("Đã có nhóm có tên:" + txtName.Text.Trim() + ". Bạn có muốn tiếp tục ghi hay không?", "Cảnh báo", true))
                {
                    //Create Again to ignore Where Clause
                    m_Query = DmucLoaithuoc.CreateQuery();
                    txtName.Focus();
                    return;
                }
            }
            //Create Again to ignore Where Clause
            m_Query = DmucLoaithuoc.CreateQuery();
            //Gọi nghiệp vụ Insert dữ liệu
           
            DmucLoaithuoc objDrugType=new DmucLoaithuoc();
            objDrugType.MaLoaithuoc = Utility.sDbnull(txtDrug_Code.Text);
            objDrugType.TenLoaithuoc = Utility.sDbnull(txtName.Text);
            objDrugType.MotaThem = Utility.sDbnull(txtDesc.Text);
            objDrugType.MaNhomthuoc= Utility.sDbnull(txtNhom.myCode, "");
            objDrugType.SttHthi = Convert.ToInt16(txtPos.Text);
            objDrugType.InRieng = Convert.ToInt16(chkInrieng.Checked ? 1 : 0);
            objDrugType.KieuThuocvattu = txtKieuthuocVT.myCode; ;
            objDrugType.IsNew = true;
            objDrugType.Save();
            //Lấy về MaxID vừa được thêm vào CSDL
            int v_shtIdLoaithuoc = objDrugType.IdLoaithuoc;
            //Lấy về Object vừa tạo
            DmucLoaithuocCollection v_arrNewObject = new DmucLoaithuocController().FetchByID(v_shtIdLoaithuoc);
            if (v_arrNewObject.Count > 0)//-->Thêm mới thành công
            {
                DataRow newitem=m_dtLoaithuoc.NewRow();
                Utility.FromObjectToDatarow(v_arrNewObject[0], ref newitem);
                newitem["ten_nhomthuoc"] = txtNhom.Text;
                m_dtLoaithuoc.Rows.Add(newitem);
                //Return to the InitialStatus
                m_enAction = action.FirstOrFinished;
                //Nhảy đến bản ghi vừa thêm mới trên lưới. Do txtID chưa bị reset nên dùng luôn
                Utility.GotoNewRowJanus(grdList, DmucLoaithuoc.Columns.IdLoaithuoc, v_shtIdLoaithuoc.ToString());
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
            int v_shtIdLoaithuoc = Convert.ToInt32(txtID.Text);
            //Kiểm tra trùng tên đối tượng và cảnh báo
            DmucLoaithuocCollection v_arrSameObject = new DmucLoaithuocController().FetchByQuery(m_Query.AddWhere(DmucLoaithuoc.Columns.MaLoaithuoc, txtDrug_Code.Text.Trim().ToUpper()).AND(DmucLoaithuoc.Columns.IdLoaithuoc, Comparison.NotEquals, v_shtIdLoaithuoc));
            if (v_arrSameObject.Count > 0)
            {
                if (!Utility.AcceptQuestion("Đã có nhóm có mã:" + txtDrug_Code.Text.Trim() + ". Bạn có muốn tiếp tục ghi hay không?", "Cảnh báo", true))
                {
                    //Create Again to ignore Where Clause
                    m_Query = DmucDoituongkcb.CreateQuery();
                    return;
                }
            }
            v_arrSameObject = new DmucLoaithuocController().FetchByQuery(m_Query.AddWhere("ten_loaithuoc", txtName.Text.Trim().ToUpper()).AND(DmucLoaithuoc.Columns.IdLoaithuoc, Comparison.NotEquals, v_shtIdLoaithuoc));
            if (v_arrSameObject.Count > 0)
            {
                if (!Utility.AcceptQuestion("Đã có nhóm có tên:" + txtName.Text.Trim() + ". Bạn có muốn tiếp tục ghi hay không?", "Cảnh báo", true))
                {
                    //Create Again to ignore Where Clause
                    m_Query = DmucDoituongkcb.CreateQuery();
                    return;
                }
            }
            //Create Again to ignore Where Clause
            m_Query = DmucLoaithuoc.CreateQuery();
            DmucLoaithuoc v_NewObjectChangePos = null;
            
            DmucLoaithuoc objDrugType = DmucLoaithuoc.FetchByID(Convert.ToInt16(txtID.Text));
            if (objDrugType != null)
            {
                objDrugType.MaLoaithuoc = Utility.sDbnull(txtDrug_Code.Text);
                objDrugType.TenLoaithuoc = Utility.sDbnull(txtName.Text);
                objDrugType.MotaThem = Utility.sDbnull(txtDesc.Text);
                objDrugType.MaNhomthuoc = Utility.sDbnull(txtNhom.myCode, "");
                objDrugType.SttHthi = Convert.ToInt16(txtPos.Text);
                objDrugType.InRieng = Convert.ToInt16(chkInrieng.Checked ? 1 : 0);
                objDrugType.KieuThuocvattu = txtKieuthuocVT.myCode; 
                objDrugType.IsNew = false;
                objDrugType.MarkOld();
                objDrugType.Save();
                
            }
            DataRow[] arrDr = m_dtLoaithuoc.Select(DmucLoaithuoc.Columns.IdLoaithuoc + "=" + txtID.Text);
            if (arrDr.Length > 0)
            {
                arrDr[0][DmucLoaithuoc.Columns.MaLoaithuoc] = Utility.sDbnull(txtDrug_Code.Text);
                arrDr[0][DmucLoaithuoc.Columns.TenLoaithuoc] = Utility.sDbnull(txtName.Text);
                arrDr[0][DmucLoaithuoc.Columns.MotaThem] = Utility.sDbnull(txtDesc.Text);
                arrDr[0][DmucLoaithuoc.Columns.MaNhomthuoc] = Utility.sDbnull(txtNhom.myCode, "");
                arrDr[0][DmucLoaithuoc.Columns.SttHthi] = Convert.ToInt16(txtPos.Text);
                arrDr[0][DmucLoaithuoc.Columns.InRieng] = Convert.ToInt16(chkInrieng.Checked ? 1 : 0);
                arrDr[0][DmucLoaithuoc.Columns.KieuThuocvattu] = txtKieuthuocVT.myCode;
                arrDr[0]["ten_nhomthuoc"] = txtNhom.Text;
            }
            //Return to the InitialStatus
            m_enAction = action.FirstOrFinished;
            //Nhảy đến bản ghi vừa cập nhật trên lưới. Do txtID chưa bị reset nên dùng luôn
            Utility.GotoNewRowJanus(grdList, DmucLoaithuoc.Columns.IdLoaithuoc, txtID.Text.Trim());
            SetControlStatus();
            Utility.SetMsg(lblMsg, "Cập nhật dữ liệu thành công.",false);
        }
        /// <summary>
        /// Thực hiện nghiệp vụ Delete dữ liệu
        /// </summary>
        private void PerformDeleteAction()
        {
            if (Utility.AcceptQuestion("Bạn có muốn xóa Loại thuốc đang chọn hay không?", "Xác nhận xóa", true))
            {
                

                Int16 v_shtIdLoaithuoc = Convert.ToInt16(txtID.Text.Trim());
                //Kiểm tra xem đã được sử dụng trong bảng khác chưa

                if (new DmucThuocController().FetchByQuery(DmucThuoc.CreateQuery().AddWhere(DmucLoaithuoc.Columns.IdLoaithuoc, Comparison.Equals, v_shtIdLoaithuoc)).Count > 0)
                {
                    Utility.SetMsg(lblMsg, "Loại thuốc này đã được sử dụng trong danh mục thuốc(vật tư) nên bạn không thể xóa",true);
                    return;
                }
                DataRow[] arrDr = m_dtLoaithuoc.Select(DmucLoaithuoc.Columns.IdLoaithuoc + "=" + txtID.Text);
                //Gọi nghiệp vụ xóa dữ liệu\
                int Count = DmucLoaithuoc.Delete(v_shtIdLoaithuoc);

                if (arrDr.Length > 0)//Nếu xóa thành công trong CSDL
                {
                    m_dtLoaithuoc.Rows.Remove(arrDr[0]);
                    m_dtLoaithuoc.AcceptChanges();
                    //Return to the InitialStatus
                    m_enAction = action.FirstOrFinished;
                    SetControlStatus();
                    Utility.SetMsg(lblMsg, "Đã xóa Loại có mã: " + v_shtIdLoaithuoc + " ra khỏi hệ thống.",false);
                }
                else//Có lỗi xảy ra
                    Utility.SetMsg(lblMsg, "Lỗi khi xóa loại thuốc" , true);

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
            m_dtLoaithuoc = SPs.ThuocLaydulieuDanhmucloaithuoc(this.kieu_thuoc_vt).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, m_dtLoaithuoc, true, true, "", "stt_hthi,ten_loaithuoc");
        }
        #endregion
        #endregion

        #region "Event Handlers: Form Events,GridView Events, Button Events"
        /// <summary>
        /// Sự kiện Load của Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_danhmuc_loaithuoc_Load(object sender, EventArgs e)
        {
            InitData();  
        }
        void InitData()
        {
            if (kieu_thuoc_vt == "VT")
                txtNhom.LOAI_DANHMUC = "NHOMVATTU";
            txtNhom.Init();
            txtKieuthuocVT.Init();
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
                txtID.Text = Utility.GetValueFromGridColumn(grdList, DmucLoaithuoc.Columns.IdLoaithuoc);
                txtDrug_Code.Text = Utility.GetValueFromGridColumn(grdList, DmucLoaithuoc.Columns.MaLoaithuoc);
                txtName.Text = Utility.GetValueFromGridColumn(grdList, DmucLoaithuoc.Columns.TenLoaithuoc);
                txtDesc.Text = Utility.GetValueFromGridColumn(grdList, DmucLoaithuoc.Columns.MotaThem);
                txtPos.Text = Utility.GetValueFromGridColumn(grdList, DmucLoaithuoc.Columns.SttHthi);
                txtNhom.SetCode( Utility.GetValueFromGridColumn(grdList, "MA_NHOMTHUOC"));
                txtKieuthuocVT.SetCode(Utility.GetValueFromGridColumn(grdList, DmucLoaithuoc.Columns.KieuThuocvattu));
                chkInrieng.Checked = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdList, "IN_RIENG"), 0)==1;
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
        private void frm_danhmuc_loaithuoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                InitData();
            }
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
