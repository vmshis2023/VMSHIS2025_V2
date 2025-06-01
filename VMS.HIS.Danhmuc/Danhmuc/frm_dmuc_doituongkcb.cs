using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VNS.Libs;
using VMS.HIS.DAL;
using SubSonic;
using VNS.HIS.NGHIEPVU;
namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_dmuc_doituongkcb : Form
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
        public  DmucDoituongkcb m_objObjectReturn = null; 
        #endregion

        #region "Private Variables(Class Level)"
        
        /// <summary>
        /// Tên của DLL chứa Form này. Được sử dụng cho mục đích SetMultilanguage và cấu hình DataGridView
        /// </summary>
        private string AssName = "";
        /// <summary>
        /// Datasource là danh sách Country hiển thị trên lưới
        /// </summary>
        private DmucDoituongkcbCollection m_DataSource = new DmucDoituongkcbCollection();
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
        private DataTable dsTable=new DataTable(); 
        /// <summary>
        /// Biến để tránh trường hợp khi gán Datasource cho GridView thì xảy ra sự kiện CurrentCellChanged
        /// Điều này là do 2 Thread thực hiện song song nhau. Do vậy ta cần xử lý nếu chưa Loaded xong thì
        /// chưa cho phép binding dữ liệu từ Gridview vào Control trên Form trong sự kiện CurrentCellChanged
        /// </summary>
        private bool m_blnLoaded = false;
        private Int16 m_shtOldPos = 0;
        private SubSonic.Query m_Query;
        #endregion

        //Các phương thức khởi tạo của Class
        #region "Constructors"
        public frm_dmuc_doituongkcb()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            m_Query = DmucDoituongkcb.CreateQuery();
            
            InitEvents();
                 
        }

        private void InitEvents()
        {
            grdList.CurrentCellChanged += new EventHandler(grdList_CurrentCellChanged);
            grdList.DoubleClick += new EventHandler(grdList_DoubleClick);
            grdList.KeyDown += new KeyEventHandler(grdList_KeyDown);
            cmdClose.Click += new EventHandler(cmdClose_Click);
            cmdInsert.Click += new EventHandler(cmdInsert_Click);
            cmdSave.Click += new EventHandler(cmdSave_Click);
            cmdUpdate.Click += new EventHandler(cmdUpdate_Click);
            cmdDelete.Click += new EventHandler(cmdDelete_Click);
            this.Load += new EventHandler(frmMeasureUnitList_Load);
            //txtfee.KeyPress += new KeyPressEventHandler(txtfee_KeyPress);
            txtPos.KeyPress += new KeyPressEventHandler(txtPos_KeyPress);
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
            errorProvider1.Clear();
            if (String.IsNullOrEmpty(txtID.Text))
            {
                errorProvider1.SetError(txtID, "Bạn cần nhập mã loại đối tượng.");
                txtID.Focus();
                return false;
            }
            SqlQuery q = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.Columns.MaDoituongKcb).IsEqualTo(Utility.DoTrim(txtObjectCode.Text));
            if (m_enAction == action.Update)
                q.And(DmucDoituongkcb.Columns.IdDoituongKcb).IsNotEqualTo(Utility.Int32Dbnull(txtID.Text, -1));

            if (q.GetRecordCount() > 0)
            {
                errorProvider1.SetError(txtObjectCode, "Đã tồn tại đối tượng có mã " + Utility.DoTrim(txtObjectCode.Text) + ". Mời bạn nhập mã khác");
                txtObjectCode.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtName.Text))
            {
                errorProvider1.SetError(txtName, "Bạn cần nhập tên loại đối tượng.");
                txtName.Focus();
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
                    //Cho phép nhập liệu mã loại đối tượng,vị trí, tên loại đối tượng và mô tả thêm
                    Utility.DisabledTextBox(txtID);
                   // Utility.EnabledTextBox(txtfee);
                    Utility.EnabledTextBox(txtName);
                    Utility.EnabledTextBox(txtPos);
                    Utility.EnabledTextBox(txtDesc);
                    txtObjectCode.Enabled = true;
                    txtDiscountCorrectLine.Enabled = true;
                    txtDiscountDiscorrectLine.Enabled = true;
                    txtDiscountCorrectLine.Clear();
                    txtDiscountDiscorrectLine.Clear();
                    chkThanhtoantruockhikham.Enabled = true;
                    chkAutoPayment.Enabled = true;
                    cboKieulaygiathuoc.Enabled = true;
                    txtPos.Clear();
                    txtName.Clear();
                    txtDesc.Clear();
                    
                    chkThanhtoantruockhikham.Checked = false;
                    chkAutoPayment.Checked = false;
                    Int16 MaxPos = Utility.Int16Dbnull(DmucDoituongkcb.CreateQuery().GetMax(DmucDoituongkcb.Columns.SttHthi), 0);
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
                    txtPos.Focus();
                    break;
                case action.Update:
                    //Không cho phép cập nhật lại mã loại đối tượng
                    Utility.DisabledTextBox(txtID);
                    //Cho phép cập nhật lại vị trí, tên loại đối tượng và mô tả thêm
                    Utility.EnabledTextBox(txtName);
                    Utility.EnabledTextBox(txtPos);
                    Utility.EnabledTextBox(txtDesc);
                     txtObjectCode.Enabled = true;
                    txtDiscountCorrectLine.Enabled = true;
                    txtDiscountDiscorrectLine.Enabled = true;
                    cboKieulaygiathuoc.Enabled = true;
                    chkThanhtoantruockhikham.Enabled = true;
                    chkAutoPayment.Enabled = true;
                    //Utility.EnabledTextBox(txtfee);
                    m_shtOldPos = Utility.Int16Dbnull(grdList.GetValue(DmucDoituongkcb.Columns.SttHthi), 0);
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
                    txtPos.Focus();
                    break;
                case action.FirstOrFinished://Hủy hoặc trạng thái ban đầu khi mới hiển thị Form
                    //Không cho phép nhập liệu mã loại đối tượng, tên loại đối tượng và mô tả thêm
                    Utility.DisabledTextBox(txtID);
                    Utility.DisabledTextBox(txtName);
                    Utility.DisabledTextBox(txtDesc);
                    //Utility.DisabledTextBox(txtfee);
                    Utility.DisabledTextBox(txtPos);
                    txtObjectCode.Enabled = false;
                     txtDiscountCorrectLine.Enabled = false;
                    txtDiscountDiscorrectLine.Enabled = false;
                    chkThanhtoantruockhikham.Enabled = false;
                    cboKieulaygiathuoc.Enabled = false;
                    chkAutoPayment.Enabled = false;
                    //--------------------------------------------------------------
                    //Thiết lập trạng thái các nút Insert, Update, Delete...
                    //Sau khi nhấn Ghi thành công hoặc Hủy thao tác thì quay về trạng thái ban đầu
                    //Cho phép thêm mới
                    cmdInsert.Enabled = true;
                    //Tùy biến nút Update và Delete tùy theo việc có hay không dữ liệu trên lưới
                    cmdUpdate.Enabled = grdList.RowCount <= 0 ? false : true;
                    cmdDelete.Enabled = grdList.RowCount <= 0 ? false : true;
                    cmdSave.Enabled = false;
                    chkThanhtoantruockhikham.Checked = false;
                    chkAutoPayment.Checked = false;
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
           
        }
        #endregion

        #region "Insert, Delete, Update,Select,..."
        /// <summary>
        /// Thực hiện nghiệp vụ Insert dữ liệu
        /// </summary>
        private void PerformInsertAction()
        {
           
            //Kiểm tra trùng tên đối tượng và cảnh báo
            DmucDoituongkcbCollection v_arrSameNameObject = new DmucDoituongkcbController().FetchByQuery(m_Query.AddWhere(DmucDoituongkcb.Columns.TenDoituongKcb,txtName.Text.Trim().ToUpper()));
            if (v_arrSameNameObject.Count > 0)
            {
                if (!Utility.AcceptQuestion("Đã có đối tượng có tên:" + txtName.Text.Trim() + ". Bạn có muốn tiếp tục ghi hay không?", "Cảnh báo", true))
                {
                    //Create Again to ignore Where Clause
                    txtName.Focus();
                    m_Query = DmucDoituongkcb.CreateQuery();
                    return;
                }
            }
            //Create Again to ignore Where Clause
            m_Query = DmucDoituongkcb.CreateQuery();
           
           // Lấy về MaxID vừa được thêm vào CSDL
            int actionResult = CreateObjectType();
            if (actionResult > 0)
            {
                ProcessData(actionResult);
                //Return to the InitialStatus
                m_enAction = action.FirstOrFinished;
                //Nhảy đến bản ghi vừa thêm mới trên lưới. Do txtID chưa bị reset nên dùng luôn
                Utility.GotoNewRowJanus(grdList, DmucDoituongkcb.Columns.IdLoaidoituongKcb, actionResult.ToString());
                Utility.ShowMsg("Thêm mới dữ liệu thành công!");
                SetControlStatus();
                this.Activate();
            }
            else//Có lỗi xảy ra
                Utility.ShowMsg("Thêm mới không thành công. Mời bạn xem lại");
        }
        private Int16 CreateObjectType()
        {
            try
            {
                DmucDoituongkcb objectType = new DmucDoituongkcb();
                if (m_enAction == action.Update) objectType = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.Columns.IdDoituongKcb).IsEqualTo(Utility.Int16Dbnull(txtID.Text, -1)).ExecuteSingle<DmucDoituongkcb>();
                objectType.MaDoituongKcb = txtObjectCode.Text;
                objectType.TenDoituongKcb = Utility.GetValue(txtName.Text, false);
                objectType.IdLoaidoituongKcb = Utility.ByteDbnull(cboLoaidoituong.SelectedIndex,0);// Utility.ByteDbnull(objectType.MaDoituongKcb == "DV" ? 1 : 0);
                objectType.PhantramDungtuyen = Utility.DecimaltoDbnull(txtDiscountCorrectLine.Value, 0);
                objectType.PhantramTraituyen = Utility.DecimaltoDbnull(txtDiscountDiscorrectLine.Value, 0);
                objectType.SttHthi = Convert.ToInt16(txtPos.Text);
                objectType.GiathuocQuanhe = (byte)(cboKieulaygiathuoc.SelectedIndex);
                objectType.ThanhtoanTruockhikham =Utility.Bool2byte(chkThanhtoantruockhikham.Checked );
                objectType.TudongThanhtoan = Utility.Bool2byte(chkAutoPayment.Checked);
                objectType.MotaThem = Utility.sDbnull(txtDesc.Text, "");
                if (m_enAction == action.Update)
                {
                    objectType.MarkOld();
                    objectType.IsNew = false;
                }
                else
                    objectType.IsNew = true;
                objectType.Save();
                return objectType.IdDoituongKcb;
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// Thực hiện nghiệp vụ Update dữ liệu
        /// </summary>
        private void PerformUpdateAction()
        {
            //Create Again to ignore Where Clause
            m_Query = DmucDoituongkcb.CreateQuery();
            //Gọi Business cập nhật dữ liệu
            int v_intObjectTypeID = Convert.ToInt32(txtID.Text);
            
           
            //Kiểm tra trùng tên đối tượng và cảnh báo
            DmucDoituongkcbCollection v_arrSameNameObject = new DmucDoituongkcbController()
                .FetchByQuery(m_Query.AddWhere(DmucDoituongkcb.Columns.TenDoituongKcb,Comparison.Equals, txtName.Text.Trim().ToUpper())
                .AND(DmucDoituongkcb.Columns.IdDoituongKcb, Comparison.NotEquals, v_intObjectTypeID));
            if (v_arrSameNameObject.Count > 0)
            {
                if (!Utility.AcceptQuestion("Đã có đối tượng có tên:" + txtName.Text.Trim() + ". Bạn có muốn tiếp tục ghi hay không?", "Cảnh báo", true)) return;
            }
            int actionResult = CreateObjectType();
            if (actionResult >-1)
            {
                m_DataSource.Sort(DmucDoituongkcb.Columns.SttHthi, true);
                ProcessData1();
                //Return to the InitialStatus
                m_enAction = action.FirstOrFinished;
                //Nhảy đến bản ghi vừa cập nhật trên lưới. Do txtID chưa bị reset nên dùng luôn
                Utility.GotoNewRowJanus(grdList,DmucDoituongkcb.Columns.IdLoaidoituongKcb, txtID.Text.Trim());
                SetControlStatus();
                Utility.ShowMsg("Cập nhật dữ liệu thành công.");
            }else
            {
                Utility.ShowMsg("Bạn gặp lỗi trong quá trình cập  nhập", "Thông báo");
                return;
            }
        }

        private void  ProcessData(int v_intObjectTypeID)
        {
            DataRow dr = dsTable.NewRow();
            dr[DmucDoituongkcb.Columns.MotaThem] = Utility.sDbnull(txtDesc.Text, "");
            dr[DmucDoituongkcb.Columns.SttHthi] = Convert.ToInt16(txtPos.Text);
            dr[DmucDoituongkcb.Columns.TenDoituongKcb] = Utility.sDbnull(txtName.Text, "");
            dr[DmucDoituongkcb.Columns.PhantramDungtuyen] = Utility.DecimaltoDbnull(txtDiscountCorrectLine.Value, 0);
            dr[DmucDoituongkcb.Columns.PhantramTraituyen] = Utility.DecimaltoDbnull(txtDiscountDiscorrectLine.Value, 0);
            dr[DmucDoituongkcb.Columns.MaDoituongKcb] = Utility.sDbnull(txtObjectCode.Text, "");
            dr[DmucDoituongkcb.Columns.IdLoaidoituongKcb] = Utility.ByteDbnull(cboLoaidoituong.SelectedIndex, 0);
            dr[DmucDoituongkcb.Columns.IdDoituongKcb] = v_intObjectTypeID;
            dr[DmucDoituongkcb.Columns.GiathuocQuanhe] = Utility.ByteDbnull(cboKieulaygiathuoc.SelectedIndex);
            dr[DmucDoituongkcb.Columns.TudongThanhtoan] = Utility.Bool2byte(chkAutoPayment.Checked);
            dr[DmucDoituongkcb.Columns.ThanhtoanTruockhikham] = Utility.Bool2byte(chkThanhtoantruockhikham.Checked);
            dsTable.Rows.Add(dr);
            dsTable.AcceptChanges();


        }
       
        private void ProcessData1()
        {
           foreach(DataRow dr in dsTable.Rows)
           {
              if(dr[DmucDoituongkcb.Columns.IdDoituongKcb].ToString()==txtID.Text)
              {
                  dr[DmucDoituongkcb.Columns.MotaThem] = Utility.sDbnull(txtDesc.Text, "");
                  dr[DmucDoituongkcb.Columns.SttHthi] = Convert.ToInt16(txtPos.Text);
                  dr[DmucDoituongkcb.Columns.TenDoituongKcb] = Utility.sDbnull(txtName.Text, "");
                  dr[DmucDoituongkcb.Columns.PhantramDungtuyen] = Utility.DecimaltoDbnull(txtDiscountCorrectLine.Value, 0);
                  dr[DmucDoituongkcb.Columns.PhantramTraituyen] = Utility.DecimaltoDbnull(txtDiscountDiscorrectLine.Value, 0);
                  dr[DmucDoituongkcb.Columns.MaDoituongKcb] = Utility.sDbnull(txtObjectCode.Text, "");
                  dr[DmucDoituongkcb.Columns.IdLoaidoituongKcb] = Utility.ByteDbnull(cboLoaidoituong.SelectedIndex, 0);
                  dr[DmucDoituongkcb.Columns.GiathuocQuanhe] = Utility.ByteDbnull(cboKieulaygiathuoc.SelectedIndex);
                  dr[DmucDoituongkcb.Columns.TudongThanhtoan] = Utility.Bool2byte(chkAutoPayment.Checked);
                  dr[DmucDoituongkcb.Columns.ThanhtoanTruockhikham] = Utility.Bool2byte(chkThanhtoantruockhikham.Checked);
                 break;
              }
           }
            dsTable.AcceptChanges();
        }
        /// <summary>
        /// Thực hiện nghiệp vụ Delete dữ liệu
        /// </summary>
        private void PerformDeleteAction()
        {
            if (Utility.AcceptQuestion("Bạn có muốn xóa đối tượng đang chọn hay không?", "Xác nhận xóa", true))
            {
                int v_intObjectTypeID = Convert.ToInt32(txtID.Text.Trim());
                KcbLuotkham _item = new Select().From(KcbLuotkham.Schema).Where(KcbLuotkham.Columns.IdDoituongKcb).IsEqualTo(v_intObjectTypeID).ExecuteSingle<KcbLuotkham>();
                if (_item != null)
                {
                    Utility.ShowMsg("Đối tượng KCB này đã được sử dụng trong bảng Kcb_Luotkham nên bạn không thể xóa");
                    return;
                }
                QheDoituongDichvucl _item1 = new Select().From(QheDoituongDichvucl.Schema).Where(QheDoituongDichvucl.Columns.IdDoituongKcb).IsEqualTo(v_intObjectTypeID).ExecuteSingle<QheDoituongDichvucl>();
                if (_item1 != null)
                {
                    Utility.ShowMsg("Đối tượng KCB này đã được sử dụng trong bảng quan hệ đối tượng-Cls Qhe_DoituongDichvucls nên bạn không thể xóa");
                    return;
                }
                QheDoituongThuoc _item2 = new Select().From(QheDoituongThuoc.Schema).Where(QheDoituongThuoc.Columns.IdDoituongKcb).IsEqualTo(v_intObjectTypeID).ExecuteSingle<QheDoituongThuoc>();
                if (_item2 != null)
                {
                    Utility.ShowMsg("Đối tượng KCB này đã được sử dụng trong bảng quan hệ đối tượng-thuốc Qhe_DoituongThuoc nên bạn không thể xóa");
                    return;
                }
                

                DataRow [] v_DeleteObject = dsTable.Select(DmucDoituongkcb.Columns.IdDoituongKcb+ "=" + v_intObjectTypeID);
                
                //Gọi nghiệp vụ xóa dữ liệu\
                //int Count = DmucDoituongkcb.Delete(v_intObjectTypeID);

                int record = new Delete().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.IdDoituongKcbColumn).IsEqualTo(v_intObjectTypeID).Execute();
                if (record>0)//Nếu xóa thành công trong CSDL
                {
                    v_DeleteObject[0].Delete();
                    m_enAction = action.FirstOrFinished;
                    SetControlStatus();
                    Utility.ShowMsg("Đã xóa đối tượng có mã: " + v_intObjectTypeID + " ra khỏi hệ thống.");
                }
                else//Có lỗi xảy ra
                    Utility.ShowMsg("Lỗi khi xóa ");

            }
        }
        /// <summary>
        /// Thực hiện thao tác Insert,Update,Delete tới CSDL theo m_enAction
        /// </summary>
        private void PerformAction()
        {
            
            switch (m_enAction)
            {
                case action.Insert:
                    //Kiểm tra tính hợp lệ của dữ liệu trước khi thêm mới
                    if (!IsValidData())
                    {
                        return;
                    }
                    PerformInsertAction();
                    break;
                case action.Update:
                    //Kiểm tra tính hợp lệ của dữ liệu trước khi thêm mới
                    if (!IsValidData())
                    {
                        return;
                    }
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
            dsTable = new Select().From(DmucDoituongkcb.Schema).ExecuteDataSet().Tables[0];
            dsTable.DefaultView.Sort = DmucDoituongkcb.Columns.SttHthi;
          
            Utility.SetDataSourceForDataGridEx(grdList, dsTable, true, true,"1=1",DmucDoituongkcb.Columns.SttHthi);
        }
        #endregion
        #endregion

        #region "Event Handlers: Form Events,GridView Events, Button Events"
        /// <summary>
        /// Sự kiện Load của Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMeasureUnitList_Load(object sender, EventArgs e)
        {
            cboLoaidoituong.SelectedIndex = 0;
            //Lấy về danh sách các loại đối tượng để hiển thị lên DataGridView
            GetData();
            //Sau khi Binding dữ liệu vào GridView thì mới cho phép thực hiện lệnh trong sự kiện CurrentCellChanged
            m_blnLoaded = true;
            //Gọi sự kiện CurrentCellChanged để hiển thị dữ liệu từ trên lưới xuống Controls
            grdList_CurrentCellChanged(grdList, e);
            //Thiết lập giá trị mặc định của action
            m_enAction = action.FirstOrFinished;
            //Thiết lập các giá trị mặc định cho các Control
            SetControlStatus();
        }
        /// <summary>
        /// Xử lý sự kiện CurrentCellChanged của DataGridView
        /// Đưa dữ liệu đang chọn từ GridView vào các Controls để người dùng sẵn sàng thao tác Delete hoặc Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void grdList_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                //Chỉ cho phép khi m_blnAllowCurrentCellChangedOnGridView=true và lưới có dữ liệu
                if (m_blnLoaded && m_blnAllowCurrentCellChangedOnGridView && grdList.RowCount > 0 && grdList.CurrentRow != null && grdList.CurrentRow.RowType == RowType.Record)
                {
                    txtID.Text = Utility.sDbnull(grdList.GetValue(DmucDoituongkcb.Columns.IdDoituongKcb), "-1");
                    txtName.Text = Utility.sDbnull(grdList.GetValue(DmucDoituongkcb.Columns.TenDoituongKcb), "");
                    txtPos.Text = Utility.sDbnull(grdList.GetValue(DmucDoituongkcb.Columns.SttHthi));
                    txtDesc.Text = Utility.sDbnull(grdList.GetValue(DmucDoituongkcb.Columns.MotaThem));
                    txtDiscountCorrectLine.Text = Utility.sDbnull(grdList.GetValue(DmucDoituongkcb.Columns.PhantramDungtuyen));
                    txtDiscountDiscorrectLine.Text = Utility.sDbnull(grdList.GetValue(DmucDoituongkcb.Columns.PhantramTraituyen));
                    txtObjectCode.Text = Utility.sDbnull(grdList.GetValue(DmucDoituongkcb.Columns.MaDoituongKcb), "");
                    cboKieulaygiathuoc.SelectedIndex = Utility.Int32Dbnull(grdList.GetValue(DmucDoituongkcb.Columns.GiathuocQuanhe));
                    chkThanhtoantruockhikham.Checked = Utility.ByteDbnull(grdList.GetValue(DmucDoituongkcb.Columns.ThanhtoanTruockhikham), 0) == 1;
                    chkAutoPayment.Checked = Utility.ByteDbnull(grdList.GetValue(DmucDoituongkcb.Columns.TudongThanhtoan), 0) == 1;
                    cboLoaidoituong.SelectedIndex = Utility.Int32Dbnull(grdList.GetValue(DmucDoituongkcb.Columns.IdLoaidoituongKcb), 0);
                }
            }
            catch
            {
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
       
        void txtPos_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Utility.NumbersOnly(e.KeyChar, txtPos);
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
        #endregion

        private void frm_dmuc_doituongkcb_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F5) GetAllDepartments();

            // Ctrl + N => btnNewClick
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.N) && cmdInsert.Enabled) cmdInsert.PerformClick();

            // Ctrl + S => Save
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.S) && cmdSave.Enabled) cmdSave.PerformClick();

            // Ctrl + C => Cập nhật
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.C) && cmdUpdate.Enabled) cmdUpdate.PerformClick();

            // Esc => Thoát khỏi Form hay huỷ form
            if ((e.KeyCode == Keys.Escape) && (cmdClose.Enabled)) cmdClose.PerformClick();

            // Del => Xoá
            if ((e.KeyCode == Keys.Delete) && (cmdDelete.Enabled)) cmdDelete.PerformClick();
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtInsurance_Level_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        private void frm_dmuc_doituongkcb_Load(object sender, EventArgs e)
        {

        }



       
    }
}
