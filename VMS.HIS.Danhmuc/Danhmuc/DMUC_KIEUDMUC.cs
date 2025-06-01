using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.HIS.NGHIEPVU;
using SubSonic;
namespace VNS.HIS.UI.DANHMUC
{
    public partial class DMUC_KIEUDMUC : Form
    {
        #region Khai báo các biến cấp Module
        DMUC_KIEUDMUC_BUSRULE m_BusRules = new DMUC_KIEUDMUC_BUSRULE();
        //Biến xác định có load ngay dữ liệu lúc mới vào form hay không?
        private bool m_blnIsLoadDataAfterLoadingForm = true;
        //Biến chứa tên các trường trong bảng
        private List<string> m_lstHeaders = new List<string>();
        //Khai báo biến chứa dữ liệu danh mục
        private DataTable m_dtData = null;
        //Biến xác định hành động hiện tại
        private action m_enAct = action.Normal;
        //Ma cu truoc khi thuc hien Update
        string m_strOldCode = "";
        //Khai báo biến xác định xem đã chạy Method Loaded của UserControl hay chưa để tránh việc bị chạy lại hàm này nhiều lần
        public bool m_blnHasLoad = false;
        //Khai báo biến cho phép bắt sự kiện Currentcell changed trên lưới hay không?
        bool m_blnAllowCurrentCellChanged = true;
        //Biến xác định Index của dòng hiện thời trên lưới
        int m_intCurrIdx = 0;
        const string _CANCEL = "Hủy";
        const string _THOAT = "Thoát";
        #endregion
        public DMUC_KIEUDMUC()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            //Khởi tạo sự kiện
            InitEvents();
        }

        #region Khai báo các hàm khởi tạo
        /// <summary>
        /// Khởi tạo các sự kiện của các Control trên form
        /// </summary>
        void InitEvents()
        {
            try
            {
                //Form load
                this.Load += new EventHandler(DMUC_KIEUDMUC_Load);
                //Bắt sự kiện KeyDown trên form
                this.KeyDown += new KeyEventHandler(DMUC_KIEUDMUC_KeyDown);
                //Bắt sự kiện chọn 1 dòng trên lưới sẽ gán giá trị từ lưới vào các Control nhập liệu phía dưới
                grdList.SelectionChanged += new EventHandler(grdList_SelectionChanged);
                grdList.KeyDown += new KeyEventHandler(grdList_KeyDown);
                grdList.UpdatingCell += grdList_UpdatingCell;
                cmdNew.Click+=new EventHandler(cmdNew_Click);
                cmdDelete.Click+=new EventHandler(cmdDelete_Click);
                cmdSave.Click+=new EventHandler(cmdSave_Click);
                cmdPrint.Click+=new EventHandler(cmdPrint_Click);
                cmdCancel.Click+=new EventHandler(cmdCancel_Click);
                cmdUpdate.Click+=new EventHandler(cmdUpdate_Click);

                mnuInsert.Click += new EventHandler(mnuInsert_Click);
                mnuUpdate.Click += new EventHandler(mnuUpdate_Click);
                mnuDelete.Click += new EventHandler(mnuDelete_Click);
                mnuPrint.Click += new EventHandler(mnuPrint_Click);

            }
            catch (Exception ex)
            {
            }
        }

        void grdList_UpdatingCell(object sender, Janus.Windows.GridEX.UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == DmucKieudmuc.Columns.TenLoai)
                {
                    new Update(DmucKieudmuc.Schema).Set(DmucKieudmuc.Columns.TenLoai).EqualTo(e.Value).Where(DmucKieudmuc.Columns.Id).IsEqualTo(Utility.Int32Dbnull(Utility.getValueOfGridCell(grdList, DmucKieudmuc.Columns.Id))).Execute();
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        void mnuPrint_Click(object sender, EventArgs e)
        {
            cmdPrint_Click(cmdPrint, e);
        }

        void mnuDelete_Click(object sender, EventArgs e)
        {
            cmdDelete_Click(cmdDelete, e);
        }

        void mnuUpdate_Click(object sender, EventArgs e)
        {
            cmdUpdate_Click(cmdUpdate, e);
        }

        void mnuInsert_Click(object sender, EventArgs e)
        {
            cmdNew_Click(cmdNew, e);
        }

        void txtSTT_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        void grdList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && Utility.isValidGrid(grdList))
                cmdDelete_Click(cmdDelete, new EventArgs());
        }
        /// <summary>
        /// Khởi tạo dữ liệu lúc mới kích hoạt form
        /// </summary>
        void InitData()
        {
            //Nếu danh mục này được phép tự động Load dữ liệu khi mới kích hoạt thì gọi hàm Tìm kiếm dữ liệu
            if (m_blnIsLoadDataAfterLoadingForm) SearchData();
            else//Nếu không thì gán một mã giả để tìm kiếm trả về không có dòng nào
            {
                //Luôn tìm kiếm khi kích hoạt form

            }
        }

        /// <summary>
        /// Basic Flow lúc mới vào form
        /// </summary>
        void BasicFlow()
        {
            autoPhanloai.Init();
            //Khởi tạo UI dựa vào tham số đầu vào
            InitUI();
            //Khởi tạo dữ liệu theo tham số đầu vào
            InitData();
        }
       
        /// <summary>
        /// Thiết lập tên cột hiển thị trên lưới dữ liệu và các Label hiển thị dựa vào tham số đầu vào
        /// </summary>
        void InitUI()
        {
            try
            {
                EnableDataRegion(false);
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region Khai báo các hàm dùng chung
        /// <summary>
        /// Kiểm tra sự hợp lệ của dữ liệu đầu vào
        /// </summary>
        /// <returns></returns>

        private bool IsValidInputData()
        {
            try
            {
                //Reset lại label hiển thị thông báo lỗi
                Utility.SetMsg(lblMsg, "", false);
                //Bắt đầu kiểm tra sự hợp lệ của dữ liệu
                if (txtMa.Text.Trim() == string.Empty)
                {

                    Utility.SetMsg(lblMsg, "Bạn cần nhập mã kiểu danh mục", true);
                    txtMa.Focus();
                    return false;
                }
                if (txtTen.Text.Trim() == string.Empty)
                {
                    Utility.SetMsg(lblMsg, "Bạn cần nhập tên kiểu danh mục" , true);
                    txtTen.Focus();
                    return false;
                }
                

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// Thực hiện thêm mới vào DataTable
        /// </summary>
        /// <returns></returns>
        /// 
        private void InsertDataTable()
        {

            try
            {
                //nếu bảng chứa dữ liệu chưa được khởi tạo hoặc đã khởi tạo nhưng không có cột nào thì ko làm gì cả
                if (m_dtData == null || m_dtData.Columns.Count <= 0) return;
                DataRow drNewRow = m_dtData.NewRow();
                drNewRow[DmucKieudmuc.Columns.Id] = _item.Id; 
                drNewRow[DmucKieudmuc.Columns.MaLoai] =Utility.DoTrim( txtMa.Text);
                drNewRow[DmucKieudmuc.Columns.TenLoai] = Utility.DoTrim(txtTen.Text);
                drNewRow[DmucKieudmuc.Columns.TrangThai] = chkTrangthai.Checked ? 1 : 0;
                drNewRow[DmucKieudmuc.Columns.MotaThem] = Utility.DoTrim(autoPhanloai.myCode);
                drNewRow[DmucKieudmuc.Columns.NguoiTao] = globalVariables.UserName;
                drNewRow[DmucKieudmuc.Columns.NgayTao] = globalVariables.SysDate;
                m_dtData.Rows.Add(drNewRow);
                m_dtData.AcceptChanges();

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi thêm dữ liệu vào lưới: \n" + ex.Message, "Thông báo lỗi");
            }
        }
        

        /// <summary>
        /// Cập nhật vào DataTable để phản ánh lại dữ liệu được thay đổi sau khi thực hiện thao tác Ghi cập nhật
        /// </summary>
        private void UpdateDataTable()
        {
            try
            {
                DataRow drRow = Utility.FetchOnebyCondition(m_dtData, "ID='" + Utility.DoTrim(txtID.Text) + "'");
                if (drRow == null) return;
                drRow[DmucKieudmuc.Columns.MaLoai] = Utility.DoTrim(txtMa.Text);
                drRow[DmucKieudmuc.Columns.TenLoai] = Utility.DoTrim(txtTen.Text);
                drRow[DmucKieudmuc.Columns.TrangThai] = chkTrangthai.Checked ? 1 : 0;
                drRow[DmucKieudmuc.Columns.MotaThem] = Utility.DoTrim(autoPhanloai.myCode);
                drRow[DmucKieudmuc.Columns.NguoiSua] = globalVariables.UserName;
                drRow[DmucKieudmuc.Columns.NgaySua] = globalVariables.SysDate;
                m_dtData.AcceptChanges();
            }
            catch (Exception)
            {


            }
        }


        /// <summary>
        /// Update các nút điều khiển khi thực hiện hành động nhấn nút Thêm mới hoặc nút Sửa
        /// </summary>
        private void ModifyActButtons_Insert_Update()
        {
            cmdNew.Visible = false;
            cmdUpdate.Visible = false;
            cmdDelete.Visible = false;
            cmdPrint.Enabled = Utility.isValidGrid(grdList);
            cmdSave.Visible = true;
            cmdCancel.Text = _CANCEL;
            cmdCancel.Enabled = true;

            #region contextmenu
            mnuInsert.Visible = cmdNew.Visible;
            mnuUpdate.Visible = cmdUpdate.Visible;
            mnuDelete.Visible = cmdDelete.Visible;

            mnuInsert.Enabled = cmdNew.Enabled;
            mnuUpdate.Enabled = cmdUpdate.Enabled;
            mnuDelete.Enabled = cmdDelete.Enabled;
            mnuPrint.Enabled = cmdPrint.Enabled;
            #endregion
        }
        /// <summary>
        /// Tùy biến các nút sau khi thực hiện các hành động: Tìm kiếm dữ liệu, Ghi thêm mới, Ghi sửa, Xóa và Hủy
        /// </summary>
        private void ModifyActButtons()
        {
            if (!m_blnAllowCurrentCellChanged) return;
            cmdNew.Visible = true;
            cmdUpdate.Visible = true;
            cmdDelete.Visible = true;
            
            cmdUpdate.Enabled = Utility.isValidGrid(grdList);
            cmdDelete.Enabled = Utility.isValidGrid(grdList);
            cmdPrint.Enabled = Utility.isValidGrid(grdList);
            cmdSave.Visible = false;
            cmdCancel.Text = _THOAT;
            cmdCancel.Enabled = true;
            #region contextmenu
            mnuInsert.Visible = cmdNew.Visible;
            mnuUpdate.Visible = cmdUpdate.Visible;
            mnuDelete.Visible = cmdDelete.Visible;

            mnuInsert.Enabled = cmdNew.Enabled;
            mnuUpdate.Enabled = cmdUpdate.Enabled;
            mnuDelete.Enabled = cmdDelete.Enabled;
            mnuPrint.Enabled = cmdPrint.Enabled;
            #endregion
           
        }
       
        /// <summary>
        /// Thực hiện hành động Thêm mới, sửa, xóa,...
        /// </summary>
        private void PerformAction()
        {
            bool v_blnActResult = false;
            try
            {

                switch (m_enAct)
                {
                    case action.Insert:
                        v_blnActResult = PerformInserAct();
                        break;
                    case action.Update:
                        v_blnActResult = PerformUpdateAct();
                        break;
                    case action.Delete:
                        v_blnActResult = PerformDeleteAct();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                //Gọi lại phần tùy biến nút . Chỉ thực hiện tùy biến lại khi thực hiện thành công
                if (v_blnActResult &&  !chkAutoNew.Checked)
                {
                    m_enAct = action.Normal;
                    ModifyActButtons();
                }
            }
        }

        /// <summary>
        /// Gọi hành động hủy Thêm mới+Sửa để quay về trạng thái ban đầu
        /// </summary>
        void PerformCancelAction()
        {
            try
            {
                //Gán lại hành động là Mới bắt đầu hoặc Đã kết thúc
                m_enAct = action.FirstOrFinished;//Hoặc thích thì gán=action.Normal
                //Disable vùng nhập liệu
                EnableDataRegion(false);
            }
            catch
            {

            }
            finally
            {
                //Cuối cùng tùy biến lại các nút
                ModifyActButtons();
            }
        }
        /// <summary>
        /// Đóng gói dữ liệu vào DataTable để gửi lên Webservice xử lý
        /// </summary>
        /// <returns></returns>
        private DmucKieudmuc GetObject()
        {
            try
            {

                DmucKieudmuc obj = new DmucKieudmuc();
                if (m_enAct == action.Update) obj.Id =Utility.Int32Dbnull( txtID.Text,0);
                obj.MaLoai = Utility.DoTrim(txtMa.Text);
                obj.TenLoai = Utility.DoTrim(txtTen.Text);
               
                obj.TrangThai = Convert.ToByte(chkTrangthai.Checked ? 1 : 0);
                obj.MotaThem = Utility.DoTrim(autoPhanloai.myCode);
                if (m_enAct == action.Update)
                {
                    obj.NguoiSua = globalVariables.UserName;
                    obj.NgaySua = globalVariables.SysDate;
                }
                else
                {
                obj.NguoiTao = globalVariables.UserName;
                obj.NgayTao = globalVariables.SysDate;
                }
                return obj;
               
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi xảy ra khi đóng gói dữ liệu vào DataTable để gửi lên Webservice\n" + ex.Message, "Thông báo");
                return null;
            }


        }
        DmucKieudmuc _item = new DmucKieudmuc();
        /// <summary>
        /// Thực hiện thêm mới dữ liệu
        /// </summary>
        private bool PerformInserAct()
        {
            try
            {
                //Kiem tra su hop le cua du lieu
                if (!IsValidInputData()) return false;
                string ActResult = "";
                 _item=GetObject();
                m_BusRules.InsertList(_item, ref ActResult);
                if (ActResult == ActionResult.Success.ToString())
                {
                    //Cho phép chọn trên lưới để fill dữ liệu xuống Vùng nhập liệu
                    m_blnAllowCurrentCellChanged = true;
                    //Thêm mới dòng này vào DataTable để phản ánh lại lên lưới
                    InsertDataTable();
                    txtID.Text = _item.Id.ToString();
                    //Tự động nhảy đến dòng mới thêm trên lưới
                    Utility.GonewRowJanus(grdList, DmucKieudmuc.Columns.Id, txtID.Text.Trim());
                   
                    //Gán biến dòng hiện thời trên lưới
                    m_intCurrIdx = grdList.CurrentRow.Position;
                    //Quay về trạng thái cancel
                    PerformCancelAction();
                    //Hiển thị thông báo thành công
                    Utility.SetMsg(lblMsg, "Thêm mới kiểu danh mục thành công", false);
                    if (chkAutoNew.Checked)
                        cmdNew_Click(cmdNew, new EventArgs());
                    else
                    {
                        //Tự động Focus vào nút Sửa
                        cmdNew.Focus();
                    }
                }
                else if (ActResult == ActionResult.ExistedRecord.ToString())
                {
                    Utility.SetMsg(lblMsg, "Mã kiểu danh mục đã được sử dụng. Đề nghị bạn nhập mã khác!", true);
                    txtMa.Focus();
                    return false;
                }
                else if (ActResult == ActionResult.Exception.ToString())
                {

                    Utility.ShowMsg("Lỗi khi thực hiện thêm mới kiểu danh mục\n" + ActResult, "Thông báo");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                Utility.ShowMsg(ex.Message);
            }
        }
        /// <summary>
        /// Thực hiện Cập nhật dữ liệu
        /// </summary>
        private bool PerformUpdateAct()
        {

            try
            {
                if (!IsValidInputData()) return false;
                string ActResult = "";
                m_BusRules.UpdateList(GetObject(), m_strOldCode, ref ActResult);
                if (ActResult == ActionResult.Success.ToString())
                {
                    //Cho phép chọn trên lưới để fill dữ liệu xuống Vùng nhập liệu
                    m_blnAllowCurrentCellChanged = true;
                    Utility.SetMsg(lblMsg, "Cập nhật kiểu danh mục thành công!", false);
                    //Update lại vào DataTable để phản ánh lên lưới
                    UpdateDataTable();
                    //Tự động nhảy đến dòng mới thêm trên lưới
                    Utility.GonewRowJanus(grdList, DmucKieudmuc.Columns.Id, txtID.Text.Trim());
                    //Quay về trạng thái cancel
                    PerformCancelAction();
                }
                else if (ActResult == ActionResult.ExistedRecord.ToString())
                {

                    Utility.SetMsg(lblMsg, "Mã kiểu danh mục đã được sử dụng. Đề nghị bạn nhập mã khác!", true);
                    txtMa.Focus();
                    return false;
                }
                else if (ActResult == ActionResult.Exception.ToString() || ActResult == ActionResult.Error.ToString())
                {
                    Utility.ShowMsg("Lỗi khi Update danh mục. Liên hệ với VNS để được hỗ trợ");
                    txtTen.Focus();
                    return false;

                }
                return true;

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return false;
            }

        }
        
        /// <summary>
        /// Thực hiện Xóa dữ liệu
        /// </summary>
        private bool PerformDeleteAct()
        {
            try
            {
                ActionResult _ActResult = ActionResult.Success;
                string DeleteContent = "kiểu danh mục (" + txtMa.Text.Trim() + "-" + txtTen.Text.Trim() + ")";
                int _ID = Utility.Int32Dbnull(Utility.getValueOfGridCell(grdList, DmucKieudmuc.IdColumn.ColumnName), 0);
                if (!Utility.AcceptQuestion("Bạn có muốn xóa " + DeleteContent + " hay không?", "Xác nhận trước khi xóa danh mục", true)) return false;
                //if (SPs.DmucKieudanhmucCheckDelete(txtMa.Text.Trim()).GetDataSet().Tables[0].Rows.Count > 0)
                //{
                //    Utility.ShowMsg("Đã có dữ liệu của danh mục này trong bảng Danh mục dùng chung nên bạn không thể xóa.\n Chú ý: Muốn xóa bạn cần vào xóa hết dữ liệu danh mục liên quan đến kiểu danh mục này trước.");
                //    return false;
                //}

                //Thực hiện hành động xóa
               _ActResult= m_BusRules.DeleteList(_ID,txtMa.Text.Trim());
                if (_ActResult == ActionResult.Success)
                {
                    RemoveRowfromDataTable(_ID);
                    //Thông báo xóa thành công
                    Utility.SetMsg(lblMsg, "Đã xóa " + DeleteContent + " thành công ?", false);
                   
                }
                else if (_ActResult == ActionResult.DataHasUsedinAnotherTable)
                {
                    Utility.ShowMsg("Đã có dữ liệu của danh mục này trong bảng Danh mục dùng chung nên bạn không thể xóa.\n Chú ý: Muốn xóa bạn cần vào xóa hết dữ liệu danh mục liên quan đến kiểu danh mục này trước.");
                    return false;
                }
                else if (_ActResult == ActionResult.Exception)
                {
                    Utility.ShowMsg("Lỗi khi xóa kiểu danh mục");
                    return false;
                }
                return true;


            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return false;
            }


        }
        void RemoveRowfromDataTable(int ID)
        {
            try
            {
                //Xóa khỏi DataTable để phản ánh lên lưới
                DataRow drDeleteRow = Utility.FetchOnebyCondition(m_dtData, "ID=" + ID);
                if (drDeleteRow != null)
                {
                    m_dtData.Rows.Remove(drDeleteRow);
                    m_dtData.AcceptChanges();
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// Tìm kiếm dữ liệu khi mới vào Form hoặc khi nhấn nút Tìm kiếm
        /// </summary>
        public void SearchData()
        {
            try
            {
               
                //Thực hiện tìm kiếm dữ liệu
                m_dtData = m_BusRules.dsGetList( ).Tables[0];
                if (m_dtData == null)
                {
                        Utility.ShowMsg("Có lỗi khi tìm kiếm dữ liệu:\n" , "Thông báo");

                }
                //Gán dữ liệu vào lưới
                Utility.SetDataSourceForDataGridEx(grdList, m_dtData, true, true, "1=1", "MA_LOAI");
                txtMa.Init(m_dtData, new List<string>() { DmucKieudmuc.Columns.Id, DmucKieudmuc.Columns.MaLoai, DmucKieudmuc.Columns.TenLoai });
                //Kiểm tra nếu có dữ liệu thì tự động chọn dòng đầu tiên
                if (grdList.RowCount > 0)
                {
                    grdList.MoveFirst();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi exception khi thực hiện tìm kiếm dữ liệu:\n" + ex.Message, "Thông báo");
            }
            finally
            {
                ModifyActButtons();
            }
        }
        /// <summary>
        /// Enable vùng nhập liệu hay không?
        /// </summary>
        /// <param name="isEnable"></param>
        void EnableDataRegion(bool isEnable)
        {
            txtMa.Enabled = isEnable;
            txtTen.Enabled = isEnable;
            chkTrangthai.Enabled = isEnable;
            autoPhanloai.Enabled = isEnable;
        }
        /// <summary>
        /// Xóa trắng vùng nhập liệu khi Thêm mới hoặc Khi không có dữ liệu trên lưới
        /// </summary>
        void ResetDataRegion()
        {
            txtMa.Text = "Tự sinh";
            txtMa.Text = "";
            txtTen.Text = "";
            autoPhanloai.SetCode("-1");
            chkTrangthai.Checked = true;

        }
        //}
        /// <summary>
        /// Subflow1-Thực hiện khi bấm nút thêm mới
        /// </summary>

        private void SubFlow1_StartInsertAct()
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                //Không cho phép chọn trên lưới
                m_blnAllowCurrentCellChanged = false;
                //Enable các mụcí nhập liệu
                EnableDataRegion(true);
                //Reset giá trị các mục nhập liệu về trống để người dùng nhập số liệu mới
                ResetDataRegion();
               
                //Tùy biến các nút về trạng thái sẵn sàng ghi Thêm mới
                ModifyActButtons_Insert_Update();
                //Tự động Focus vào Textbox Mã
                txtMa.Focus();
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// Subflow2-Thực hiện khi bấm nút cập nhật
        /// </summary>
        private void SubFlow2_StartUpdateAct()
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                //Không cho phép chọn trên lưới
                m_blnAllowCurrentCellChanged = false;
                //Thiết lập hành động là Cập nhật để xử lý ở sự kiện Ghi
                m_enAct = action.Update;
                //Enable các mục nhập liệu
                EnableDataRegion(true);
                //Lấy Mã cũ để hoán đổi Mã nếu người dùng sửa cả Mã=Mã của một danh mục có sẵn
                m_strOldCode = txtMa.Text.Trim();
                //Lấy STT cũ để hoán đổi STT nếu người dùng sửa cả STT=STT của một danh mục có sẵn
                //Gọi hàm tùy biến các nút về trạng thái Update
                ModifyActButtons_Insert_Update();
                //Tự động Focus vào Textbox Tên
                txtTen.Focus();
            }
            catch
            {
            }
        }
        
        #endregion

        #region Các sự kiện của các Controls
        /// <summary>
        /// Sự kiện khi mới load Form hoặc khi chọn Tab chức năng trên giao diện Avalon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DMUC_KIEUDMUC_Load(object sender, EventArgs e)
        {
            try
            {
                //chức năng trên Tab của Avalon
                if (m_blnHasLoad) return;
                //Chạy BasicFlow
                BasicFlow();
                grdList.Focus();
                //Gán biến xác định hàm này đã từng chạy để không bị chạy lại
                m_blnHasLoad = true;
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// Bắt sự kiện KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

         void DMUC_KIEUDMUC_KeyDown(object sender, KeyEventArgs e)
        {
            
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{TAB}");
                    return;
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    cmdCancel_Click(cmdCancel, new EventArgs());
                    return;
                }
               else  if (e.Control && e.KeyCode == Keys.N) cmdNew_Click(cmdNew, new EventArgs());
               else  if (e.Control && e.KeyCode == Keys.U) cmdUpdate_Click(cmdUpdate, new EventArgs());
                else if (e.Control && e.KeyCode == Keys.D) cmdDelete_Click(cmdDelete, new EventArgs());
                else if (e.Control && e.KeyCode == Keys.P) cmdPrint_Click(cmdPrint, new EventArgs());
                else if (e.Control && e.KeyCode == Keys.S) cmdSave_Click(cmdSave, new EventArgs());
                else if (e.KeyCode==Keys.F5)
                    Refresh();
            }
            catch
            {
            }
         }
         void Refresh()
         {
             try
             {
                 DataTable dt = new Select(DmucChung.Columns.Loai).From(DmucChung.Schema).Distinct().ExecuteDataSet().Tables[0];
                 bool haschanged = false;
                 foreach (DataRow dr in dt.Rows)
                     if (m_dtData.Select("MA_LOAI='" + Utility.sDbnull(dr["LOAI"]) + "'").Length <= 0)//Auto insert
                     {
                         DmucKieudmuc item = new DmucKieudmuc();
                         item.MaLoai = Utility.sDbnull(dr["LOAI"]);
                         item.TenLoai = item.MaLoai;
                         item.TrangThai = 1;
                         item.MotaThem = "";
                         item.NgayTao = DateTime.Now;
                         item.NguoiTao = globalVariables.UserName;
                         item.IsNew = true;
                         item.Save();
                         haschanged = true;
                     }
                 if (haschanged)
                     SearchData();
             }
             catch (Exception ex)
             {
                 Utility.ShowMsg(ex.ToString());
             }
            
         }
       
        /// <summary>
        /// Xử lý sự kiện chọn trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

                //Nếu không có dòng nào thì gán giá trị trống cho các mục nhập liệu
                if (!Utility.isValidGrid(grdList))
                {
                    ResetDataRegion();
                    //Thoát khỏi hàm không xử lý phần code phía dưới
                    return;
                }
                //Kiểm tra nếu đang thực hiện hành động Thêm mới hoặc Sửa thì không cho phép fill lại dữ liệu
                if (m_enAct == action.Insert || m_enAct == action.Update) return;
                //Nếu không cho phép chọn trên lưới khi đang thực hiện hành động Thêm mới hoặc sửa thì cũng không làm gì cả
                if (!m_blnAllowCurrentCellChanged) return;
                m_intCurrIdx = grdList.CurrentRow.Position;
                //Nếu có dữ liệu thì lấy dữ liệu từ dòng đang chọn để fill vào các Controls nhập liệu cho người dùng sẵn sàng sửa
                DataRow dr = Utility.getCurrentDataRow(grdList);
                if (dr == null) return;
                if (dr != null)
                {
                    txtID.Text = dr[DmucKieudmuc.Columns.Id].ToString();
                    txtMa._Text = dr[DmucKieudmuc.Columns.MaLoai].ToString();
                    txtTen.Text = dr[DmucKieudmuc.Columns.TenLoai].ToString();

                    chkTrangthai.Checked = dr[DmucKieudmuc.Columns.TrangThai].ToString() == "1" ? true : false;
                    autoPhanloai.SetCode( dr[DmucKieudmuc.Columns.MotaThem].ToString());
                }

            }
            catch
            {

            }
            finally
            {
                ModifyActButtons();
            }


        }
        /// <summary>
        /// Sự kiện nhấn nút Ghi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            //Thực hiện nghiệp vụ tùy vào hành động Thêm mới hoặc Sửa
            PerformAction();
        }

        /// <summary>
        /// Sự kiện nhấn nút Sửa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //Đặt trạng thái hành động=Sửa
                m_enAct = action.Update;
                //Thực hiện tùy biến các vùng nhập liệu+Control để người dùng sẵn sàng sửa dữ liệu
                SubFlow2_StartUpdateAct();
            }
            catch
            {

            }
        }
        /// <summary>
        /// Sự kiện nhất nút Thêm mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNew_Click(object sender, EventArgs e)
        {
            //Đặt trạng thái hành động=Thêm mới
            m_enAct = action.Insert;
            //Thực hiện tùy biến các vùng nhập liệu+Control để người dùng sẵn sàng nhập liệu
            SubFlow1_StartInsertAct();

        }
        /// <summary>
        /// Sự kiện nhấn nút Hủy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            if (cmdCancel.Text == _THOAT)
            {
                this.Close();
            }
            else
            {
                //Cho phép chọn trên lưới để fill dữ liệu xuống Vùng nhập liệu
                m_blnAllowCurrentCellChanged = true;
                PerformCancelAction();
                //Chọn lại dòng hiện thời trên lưới để fill vào các Controls bên dưới
                grdList_SelectionChanged(grdList, new EventArgs());
            }
        }


        /// <summary>
        /// Sự kiện nhấn nút Xóa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            //Gán hành động là xóa
            m_enAct = action.Delete;
            //Gọi hàm thực hiện hành động
            PerformAction();



        }


        /// <summary>
        /// Sự kiến nhấn nút tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            //Hủy các hành động Thêm mới hoặc Sửa nếu đang có
            PerformCancelAction();
            //Thực hiện gọi hàm tìm kiếm
            SearchData();

        }
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList)) return;
                DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(Utility.sDbnull(Utility.getValueOfGridCell(grdList, DmucKieudmuc.Columns.MaLoai), ""));
                _DMUC_DCHUNG.ShowDialog();
            }
            catch
            {

            }
        }

        void window_Closed(object sender, EventArgs e)
        {
        }
        #endregion

        private void cmdPrint_Click_1(object sender, EventArgs e)
        {

        }
    }
}
