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
    public partial class frm_dmuc_benhvien : Form
    {
          //Vùng này khai báo các biến cục bộ dùng trong Class
        
        #region "Public Variables(Class Level)
        public bool m_blnCancel = true;
        /// <summary>
        /// Biến xác định xem form được gọi từ đâu
        /// true: Gọi từ Menu
        /// false: Gọi từ một Form khác và thường trả về bệnh viện khi chọn trên lưới hoặc nhấn nút chọn
        /// </summary>
        public bool m_blnCallFromMenu = true;
        /// <summary>
        /// Biến trả về khi thực hiện DoubleClick trên lưới với điều kiện blnCallFromMenu=false
        /// </summary>
        public  DmucBenhvien m_objObjectReturn = null; 
        #endregion

        #region "Private Variables(Class Level)"
        
        /// <summary>
        /// Tên của DLL chứa Form này. Được sử dụng cho mục đích SetMultilanguage và cấu hình DataGridView
        /// </summary>
        private string AssName = "";
        /// <summary>
        /// Datasource là danh sách Country hiển thị trên lưới
        /// </summary>
        private DmucBenhvienCollection m_DataSource = new DmucBenhvienCollection();
        /// <summary>
        /// Có cho phép phản ánh dữ liệu trên lưới vào các Control hay không? 
        /// Mục đích khi nhấn Insert, Delete thì khi chọn trên lưới sẽ ko thay đổi dữ liệu trong các Control bên dưới.
        /// Ở chế độ bình thường thì khi chọn trên lưới sẽ phản ánh dữ liệu xuống các Control để sẵn sàng thao tác.
        /// </summary>
        private bool _mBlnAllowCurrentCellChangedOnGridView = true;
        /// <summary>
        /// Thao tác đang thực hiện là gì: Insert, Delete, Update hay Select...
        /// </summary>
        private action _mEnAction;
        private DataTable _dsTable=new DataTable(); 
        /// <summary>
        /// Biến để tránh trường hợp khi gán Datasource cho GridView thì xảy ra sự kiện CurrentCellChanged
        /// Điều này là do 2 Thread thực hiện song song nhau. Do vậy ta cần xử lý nếu chưa Loaded xong thì
        /// chưa cho phép binding dữ liệu từ Gridview vào Control trên Form trong sự kiện CurrentCellChanged
        /// </summary>
        private bool _mBlnLoaded = false;
        private Int16 _mShtOldPos = 0;
        private SubSonic.Query _mQuery;
        #endregion

        //Các phương thức khởi tạo của Class
        #region "Constructors"
        public frm_dmuc_benhvien()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            _mQuery = DmucBenhvien.CreateQuery();
            
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

        //Vùng này chứa các thuộc tính để thao tác với các bệnh viện khác 
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
                errorProvider1.SetError(txtID, "Bạn cần nhập mã loại bệnh viện.");
                txtID.Focus();
                return false;
            }
            SqlQuery q = new Select().From(DmucBenhvien.Schema).Where(DmucBenhvien.Columns.MaBenhvien).IsEqualTo(Utility.DoTrim(txtObjectCode.Text));
            if (_mEnAction == action.Update)
                q.And(DmucBenhvien.Columns.IdBenhvien).IsNotEqualTo(Utility.Int32Dbnull(txtID.Text, -1));

            if (q.GetRecordCount() > 0)
            {
                errorProvider1.SetError(txtObjectCode, "Đã tồn tại bệnh viện có mã " + Utility.DoTrim(txtObjectCode.Text) + ". Mời bạn nhập mã khác");
                txtObjectCode.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtName.Text))
            {
                errorProvider1.SetError(txtName, "Bạn cần nhập tên loại bệnh viện.");
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
            switch (_mEnAction)
            {
                case action.Insert:
                    //Cho phép nhập liệu mã loại bệnh viện,vị trí, tên loại bệnh viện và mô tả thêm
                    Utility.DisabledTextBox(txtID);
                   // Utility.EnabledTextBox(txtfee);
                    Utility.EnabledTextBox(txtName);
                    Utility.EnabledTextBox(txtPos);
                    Utility.EnabledTextBox(txtThanhpho);
                    txtObjectCode.Enabled = true;
                    txtPos.Clear();
                    cboTuyen.SelectedIndex = 0;
                    txtName.Clear();
                    txtThanhpho.Clear();
                    Int16 MaxPos = Utility.Int16Dbnull(DmucBenhvien.CreateQuery().GetMax(DmucBenhvien.Columns.SttHthi), 0);
                    MaxPos += 1;
                    txtPos.Text = MaxPos.ToString();
                    _mShtOldPos = Convert.ToInt16(txtPos.Text);
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
                    _mBlnAllowCurrentCellChangedOnGridView = false;
                    //Tự động Focus đến mục ID để người dùng nhập liệu
                    txtID.Text = "Tự sinh";
                    txtPos.Focus();
                    break;
                case action.Update:
                    //Không cho phép cập nhật lại mã loại bệnh viện
                    Utility.DisabledTextBox(txtID);
                    //Cho phép cập nhật lại vị trí, tên loại bệnh viện và mô tả thêm
                    Utility.EnabledTextBox(txtName);
                    Utility.EnabledTextBox(txtPos);
                    Utility.EnabledTextBox(txtThanhpho);
                     txtObjectCode.Enabled = true;
                    //Utility.EnabledTextBox(txtfee);
                    _mShtOldPos = Utility.Int16Dbnull(grdList.GetValue(DmucBenhvien.Columns.SttHthi), 0);
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
                    _mBlnAllowCurrentCellChangedOnGridView = false;
                    //Tự động Focus đến mục Position để người dùng nhập liệu
                    txtPos.Focus();
                    break;
                case action.FirstOrFinished://Hủy hoặc trạng thái ban đầu khi mới hiển thị Form
                    //Không cho phép nhập liệu mã loại bệnh viện, tên loại bệnh viện và mô tả thêm
                    Utility.DisabledTextBox(txtID);
                    Utility.DisabledTextBox(txtName);
                    Utility.DisabledTextBox(txtThanhpho);
                    //Utility.DisabledTextBox(txtfee);
                    Utility.DisabledTextBox(txtPos);
                    txtObjectCode.Enabled = false;
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
                    _mBlnAllowCurrentCellChangedOnGridView = true;
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
           
            //Kiểm tra trùng tên bệnh viện và cảnh báo
            DmucBenhvienCollection v_arrSameNameObject = new DmucBenhvienController().FetchByQuery(_mQuery.AddWhere(DmucBenhvien.Columns.TenBenhvien,txtName.Text.Trim().ToUpper()));
            if (v_arrSameNameObject.Count > 0)
            {
                if (!Utility.AcceptQuestion("Đã có bệnh viện có tên:" + txtName.Text.Trim() + ". Bạn có muốn tiếp tục ghi hay không?", "Cảnh báo", true))
                {
                    //Create Again to ignore Where Clause
                    txtName.Focus();
                    _mQuery = DmucBenhvien.CreateQuery();
                    return;
                }
            }
            //Create Again to ignore Where Clause
            _mQuery = DmucBenhvien.CreateQuery();
           
           // Lấy về MaxID vừa được thêm vào CSDL
            int actionResult = CreateHospital();
            if (actionResult > 0)
            {
                ProcessData(actionResult);
                //Return to the InitialStatus
                _mEnAction = action.FirstOrFinished;
                //Nhảy đến bản ghi vừa thêm mới trên lưới. Do txtID chưa bị reset nên dùng luôn
                Utility.GotoNewRowJanus(grdList, DmucBenhvien.Columns.IdBenhvien, actionResult.ToString());
                Utility.ShowMsg("Thêm mới dữ liệu thành công!");
                SetControlStatus();
                this.Activate();
            }
            else//Có lỗi xảy ra
                Utility.ShowMsg("Thêm mới không thành công. Mời bạn xem lại");
        }
        private int CreateHospital()
        {
            try
            {
                DmucBenhvien objBenhvien = new DmucBenhvien();
                if (_mEnAction == action.Update) objBenhvien = new Select().From(DmucBenhvien.Schema).Where(DmucBenhvien.Columns.IdBenhvien).IsEqualTo(Utility.Int16Dbnull(txtID.Text, -1)).ExecuteSingle<DmucBenhvien>();
                objBenhvien.MaBenhvien = txtObjectCode.Text;
                objBenhvien.TenBenhvien = Utility.GetValue(txtName.Text, false);
                objBenhvien.Tuyen = Utility.sDbnull(cboTuyen.SelectedIndex + 1,"-1");
                objBenhvien.SttHthi = Utility.Int16Dbnull(txtPos.Text);
                objBenhvien.MaThanhpho = txtThanhpho.MyCode;
                objBenhvien.Trangthai = Utility.Byte2Bool(chkhoatdong.Checked);
                if (_mEnAction == action.Update)
                {
                    objBenhvien.MarkOld();
                    objBenhvien.IsNew = false;
                }
                else
                    objBenhvien.IsNew = true;
                objBenhvien.Save();
                return objBenhvien.IdBenhvien;
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
            _mQuery = DmucBenhvien.CreateQuery();
            //Gọi Business cập nhật dữ liệu
            int v_intObjectTypeID = Convert.ToInt32(txtID.Text);
            
           
            //Kiểm tra trùng tên bệnh viện và cảnh báo
            DmucBenhvienCollection v_arrSameNameObject = new DmucBenhvienController()
                .FetchByQuery(_mQuery.AddWhere(DmucBenhvien.Columns.TenBenhvien,Comparison.Equals, txtName.Text.Trim().ToUpper())
                .AND(DmucBenhvien.Columns.IdBenhvien, Comparison.NotEquals, v_intObjectTypeID));
            if (v_arrSameNameObject.Count > 0)
            {
                if (!Utility.AcceptQuestion("Đã có bệnh viện có tên:" + txtName.Text.Trim() + ". Bạn có muốn tiếp tục ghi hay không?", "Cảnh báo", true)) return;
            }
            int actionResult = CreateHospital();
            if (actionResult >-1)
            {
                m_DataSource.Sort(DmucBenhvien.Columns.SttHthi, true);
                ProcessData1();
                //Return to the InitialStatus
                _mEnAction = action.FirstOrFinished;
                //Nhảy đến bản ghi vừa cập nhật trên lưới. Do txtID chưa bị reset nên dùng luôn
                Utility.GotoNewRowJanus(grdList,DmucBenhvien.Columns.IdBenhvien, txtID.Text.Trim());
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
            DataRow dr = _dsTable.NewRow();
            dr[DmucBenhvien.Columns.MaThanhpho] = txtThanhpho.MyCode;
            dr[DmucBenhvien.Columns.SttHthi] = Convert.ToInt16(txtPos.Text);
            dr[DmucBenhvien.Columns.TenBenhvien] = Utility.sDbnull(txtName.Text, "");
            dr[DmucBenhvien.Columns.MaBenhvien] = Utility.sDbnull(txtObjectCode.Text, "");
            dr[DmucBenhvien.Columns.Tuyen] = Utility.sDbnull(cboTuyen.SelectedIndex + 1,"-1");
            dr[DmucBenhvien.Columns.IdBenhvien] = v_intObjectTypeID;
            dr[DmucBenhvien.Columns.Trangthai] = Utility.Byte2Bool(chkhoatdong.Checked);
            _dsTable.Rows.Add(dr);
            _dsTable.AcceptChanges();


        }
       
        private void ProcessData1()
        {
           foreach(DataRow dr in _dsTable.Rows)
           {
              if(dr[DmucBenhvien.Columns.IdBenhvien].ToString()==txtID.Text)
              {
                  dr[DmucBenhvien.Columns.MaThanhpho] = txtThanhpho.MyCode;
                  dr[DmucBenhvien.Columns.SttHthi] = Convert.ToInt16(txtPos.Text);
                  dr[DmucBenhvien.Columns.TenBenhvien] = Utility.sDbnull(txtName.Text, "");
                  dr[DmucBenhvien.Columns.MaBenhvien] = Utility.sDbnull(txtObjectCode.Text, "");
                  dr[DmucBenhvien.Columns.Tuyen] = Utility.sDbnull(cboTuyen.SelectedIndex + 1, "-1");
                  dr[DmucBenhvien.Columns.Trangthai] = Utility.Byte2Bool(chkhoatdong.Checked);
                 break;
              }
           }
            _dsTable.AcceptChanges();
        }
        /// <summary>
        /// Thực hiện nghiệp vụ Delete dữ liệu
        /// </summary>
        private void PerformDeleteAction()
        {
            int v_intObjectTypeID = Convert.ToInt32(txtID.Text.Trim());
            KcbPhieuchuyenvien item0 = new Select().From(KcbPhieuchuyenvien.Schema)
               .Where(KcbPhieuchuyenvien.Columns.IdBenhvienChuyenden).IsEqualTo(v_intObjectTypeID)
               .ExecuteSingle<KcbPhieuchuyenvien>();
            if (item0 != null)
            {
                Utility.ShowMsg("Bệnh viện bạn đang chọn xóa đã được sử dụng trong các phiếu chuyển viện nên bạn không thể xóa");
                return;
            }
            KcbLuotkham item = new Select().From(KcbLuotkham.Schema)
                .WhereExpression(KcbLuotkham.Columns.IdBenhvienDen).IsEqualTo(v_intObjectTypeID)
                .OrExpression(KcbLuotkham.Columns.IdBenhvienDi).IsEqualTo(v_intObjectTypeID).CloseExpression()
                .ExecuteSingle<KcbLuotkham>();
            if (item != null)
            {
                Utility.ShowMsg("Bệnh viện bạn đang chọn xóa đã được sử dụng trong hệ thống nên bạn không thể xóa");
                return;
            }
            NoitruPhieuravien item1 = new Select().From(NoitruPhieuravien.Schema)
               .Where(NoitruPhieuravien.Columns.IdBenhvienDi).IsEqualTo(v_intObjectTypeID)
               .ExecuteSingle<NoitruPhieuravien>();
            if (item != null)
            {
                Utility.ShowMsg("Bệnh viện bạn đang chọn xóa đã được sử dụng trong các phiếu ra viện nên bạn không thể xóa");
                return;
            }
            if (Utility.AcceptQuestion("Bạn có muốn xóa bệnh viện đang chọn hay không?", "Xác nhận xóa", true))
            {
                //Kiểm tra ko được xóa nếu đã sử dụng trong bảng khác
               
                DataRow [] v_DeleteObject = _dsTable.Select(DmucBenhvien.Columns.IdBenhvien+ "=" + v_intObjectTypeID);
                
                //Gọi nghiệp vụ xóa dữ liệu\
                //int Count = DmucBenhvien.Delete(v_intObjectTypeID);

                int record = new Delete().From(DmucBenhvien.Schema).Where(DmucBenhvien.IdBenhvienColumn).IsEqualTo(v_intObjectTypeID).Execute();
                if (record>0)//Nếu xóa thành công trong CSDL
                {
                    v_DeleteObject[0].Delete();
                    _mEnAction = action.FirstOrFinished;
                    SetControlStatus();
                    Utility.ShowMsg("Đã xóa bệnh viện có mã: " + v_intObjectTypeID + " ra khỏi hệ thống.");
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
            
            switch (_mEnAction)
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
            _dsTable = SPs.SpDmucBenhVienSelectAll().GetDataSet().Tables[0];
            _dsTable.DefaultView.Sort = DmucBenhvien.Columns.SttHthi;
          
            Utility.SetDataSourceForDataGridEx(grdList, _dsTable, true, true,"1=1",DmucBenhvien.Columns.SttHthi);
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
            cboTuyen.SelectedIndex = 0;
            AutocompleteThanhpho();
            //Lấy về danh sách các loại bệnh viện để hiển thị lên DataGridView
            GetData();
            
            //Sau khi Binding dữ liệu vào GridView thì mới cho phép thực hiện lệnh trong sự kiện CurrentCellChanged
            _mBlnLoaded = true;
            //Gọi sự kiện CurrentCellChanged để hiển thị dữ liệu từ trên lưới xuống Controls
            grdList_CurrentCellChanged(grdList, e);
            //Thiết lập giá trị mặc định của action
            _mEnAction = action.FirstOrFinished;
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
                if (_mBlnLoaded && _mBlnAllowCurrentCellChangedOnGridView && grdList.RowCount > 0 && grdList.CurrentRow != null && grdList.CurrentRow.RowType == RowType.Record)
                {
                    txtID.Text = Utility.sDbnull(grdList.GetValue(DmucBenhvien.Columns.IdBenhvien), "-1");
                    txtName.Text = Utility.sDbnull(grdList.GetValue(DmucBenhvien.Columns.TenBenhvien), "");
                    txtPos.Text = Utility.sDbnull(grdList.GetValue(DmucBenhvien.Columns.SttHthi),"100");
                    txtThanhpho.SetCode(Utility.sDbnull(grdList.GetValue(DmucBenhvien.Columns.MaThanhpho),"-1"));
                    txtObjectCode.Text = Utility.sDbnull(grdList.GetValue(DmucBenhvien.Columns.MaBenhvien), "");
                    cboTuyen.Text = Utility.sDbnull(grdList.GetValue(DmucBenhvien.Columns.Tuyen), "");
                    chkhoatdong.Checked = Utility.Byte2Bool(grdList.GetValue(DmucBenhvien.Columns.Trangthai));
                }
            }
            catch(Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }

        private void AutocompleteThanhpho()
        {
            DataTable m_dtThanhpho = new Select().From(DmucDiachinh.Schema).Where(DmucDiachinh.Columns.LoaiDiachinh).IsEqualTo(0).ExecuteDataSet().Tables[0];
            try
            {
                if (m_dtThanhpho == null) return;
                if (!m_dtThanhpho.Columns.Contains("ShortCut"))
                    m_dtThanhpho.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in m_dtThanhpho.Rows)
                {
                    string shortcut = "";
                    string realName = dr[DmucDiachinh.Columns.TenDiachinh].ToString().Trim() + " " +
                                      Utility.Bodau(dr[DmucDiachinh.Columns.TenDiachinh].ToString().Trim());
                    shortcut = dr[DmucDiachinh.Columns.MaDiachinh].ToString().Trim();
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
            }
            catch
            {
            }
            finally
            {
                var source = new List<string>();
                var query = from p in m_dtThanhpho.AsEnumerable()
                              select p[DmucDiachinh.MaDiachinhColumn.ColumnName].ToString() + "#" 
                            + p[DmucDiachinh.MaDiachinhColumn.ColumnName].ToString() + "@" 
                            + p[DmucDiachinh.TenDiachinhColumn.ColumnName].ToString() + "@" +
                                p.Field<string>("shortcut").ToString();
                source = query.ToList();
                this.txtThanhpho.AutoCompleteList = source;
                this.txtThanhpho.TextAlign = HorizontalAlignment.Left;
                this.txtThanhpho.CaseSensitive = false;
                this.txtThanhpho.MinTypedCharacters = 1;

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
            if (_mBlnLoaded && _mBlnAllowCurrentCellChangedOnGridView && grdList.RowCount > 0 && grdList.CurrentRow != null && !m_blnCallFromMenu)
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
            _mEnAction = action.Insert;
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
            _mEnAction = action.Update;
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
            _mEnAction = action.Delete;
            PerformAction();
            _mEnAction = action.FirstOrFinished;
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
                _mEnAction = action.FirstOrFinished;
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

        private void frm_dmuc_benhvien_KeyDown(object sender, KeyEventArgs e)
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
        private void txtInsurance_Level_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }    
    }
}
