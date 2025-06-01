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
    public partial class frm_dmuc_kieukham : Form
    {
        #region Khai báo các biến cấp Module
        dmuckieukham_busrule m_BusRules = new dmuckieukham_busrule();
        //Khai báo biến chứa Loại danh mục ứng với D_DMUC_CHUNG.LOAI
        private string m_strListType = "";
        //Biến chứa tên danh mục
        private string m_strListName = string.Empty;
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
        //Số thứ tự cũ
        int m_intOldOrder = 0;
        //Khai báo biến xác định xem đã chạy Method Loaded của UserControl hay chưa để tránh việc bị chạy lại hàm này nhiều lần
        public bool m_blnHasLoad = false;
        //Khai báo biến cho phép bắt sự kiện Currentcell changed trên lưới hay không?
        bool m_blnAllowCurrentCellChanged = true;
        //Biến xác định Index của dòng hiện thời trên lưới
        int m_intCurrIdx = 0;
        const string _CANCEL = "Hủy";
        const string _THOAT = "Thoát";
        #endregion
        public frm_dmuc_kieukham()
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
                this.Load += new EventHandler(frm_dmuc_kieukham_Load);
                //Bắt sự kiện KeyDown trên form
                this.KeyDown += new KeyEventHandler(frm_dmuc_kieukham_KeyDown);
                //Bắt sự kiện chọn 1 dòng trên lưới sẽ gán giá trị từ lưới vào các Control nhập liệu phía dưới
                grdList.SelectionChanged += new EventHandler(grdList_SelectionChanged);
                grdList.KeyDown += new KeyEventHandler(grdList_KeyDown);
                cmdNew.Click+=new EventHandler(cmdNew_Click);
                cmdDelete.Click+=new EventHandler(cmdDelete_Click);
                cmdSave.Click+=new EventHandler(cmdSave_Click);
                cmdPrint.Click+=new EventHandler(cmdPrint_Click);
                cmdCancel.Click+=new EventHandler(cmdCancel_Click);
                cmdUpdate.Click+=new EventHandler(cmdUpdate_Click);

                txtSTT.KeyPress += new KeyPressEventHandler(txtSTT_KeyPress);
                mnuInsert.Click += new EventHandler(mnuInsert_Click);
                mnuUpdate.Click += new EventHandler(mnuUpdate_Click);
                mnuDelete.Click += new EventHandler(mnuDelete_Click);
                mnuPrint.Click += new EventHandler(mnuPrint_Click);

                mnuRefresh.Click += new EventHandler(mnuRefresh_Click);

            }
            catch (Exception ex)
            {
            }
        }

        void mnuRefresh_Click(object sender, EventArgs e)
        {
            SearchData();
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
            DataBinding.BindDataCombox(cbonhombaocao, THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOMBAOCAOCLS", true), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
            DataBinding.BindDataCombox(cbodoituongkcb, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(), DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "---Tất cả---", true);
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

            //Khởi tạo UI dựa vào tham số đầu vào
            InitUI();
            //Khởi tạo dữ liệu theo tham số đầu vào
            InitData();
        }
        /// <summary>
        /// Tách tham số đầu vào thành các phần khác nhau
        /// </summary>
        
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

                    Utility.SetMsg(lblMsg, "Bạn cần nhập mã " + m_strListName, true);
                    txtMa.Focus();
                    return false;
                }
                if (txtTen.Text.Trim() == string.Empty)
                {
                    Utility.SetMsg(lblMsg, "Bạn cần nhập tên " + m_strListName, true);
                    txtTen.Focus();
                    return false;
                }
                if (txtSTT.Text.Trim() == string.Empty)
                {
                    Utility.SetMsg(lblMsg, "Bạn cần nhập Số thứ tự", true);
                    txtSTT.Focus();
                    return false;
                }
                if (!Utility.IsNumeric(txtSTT.Text.Trim()))
                {
                    Utility.SetMsg(lblMsg, "Số thứ tự phải là số", true);
                    txtSTT.Focus();
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
                drNewRow[DmucKieukham.Columns.IdKieukham] = Utility.Int32Dbnull(txtID.Text);
                drNewRow[DmucKieukham.Columns.MaKieukham] =Utility.DoTrim( txtMa.Text);
                drNewRow[DmucKieukham.Columns.TenKieukham] = Utility.DoTrim(txtTen.Text);
                drNewRow[DmucKieukham.Columns.NhomBaocao] = cbonhombaocao.SelectedValue.ToString();
                drNewRow[DmucKieukham.Columns.MaDoituongkcb] = cbodoituongkcb.SelectedValue.ToString();
                drNewRow["ten_nhom_baocao"] =cbonhombaocao.SelectedIndex==0?"Chưa có": cbonhombaocao.Text;
                drNewRow[DmucDoituongkcb.Columns.TenDoituongKcb] =cbonhombaocao.SelectedIndex==0?"Tất cả": cbodoituongkcb.Text;
                drNewRow[DmucKieukham.Columns.SttHthi] = Convert.ToInt16(txtSTT.Text);
                drNewRow[DmucKieukham.Columns.DonGia] = nmrDongia.Value;
                drNewRow[DmucKieukham.Columns.TrangThai] = chkTrangthai.Checked ? 1 : 0;

                drNewRow[DmucKieukham.Columns.NguoiTao] = globalVariables.UserName;
                drNewRow[DmucKieukham.Columns.NgayTao] = globalVariables.SysDate;

                m_dtData.Rows.Add(drNewRow);
                m_dtData.AcceptChanges();

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi thêm dữ liệu vào lưới: \n" + ex.Message, "Thông báo lỗi");
            }
        }
        /// <summary>
        /// Update STT hiển thị vào DataTable để hiển thị lại lên lưới
        /// </summary>
        /// <param name="STTMoi"></param>
        public void UpdateSTT(int STTMoi)
        {
            try
            {
                //B1: Tim ban ghi co STT=STT moi
                DataRow[] v_arrDR = Utility.FetchAllsbyCondition(m_dtData, "STT_HTHI=" + STTMoi);
                if (v_arrDR.Length > 0)
                {
                    v_arrDR[0]["STT_HTHI"] = m_intOldOrder;
                    m_dtData.AcceptChanges();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Cập nhật vào DataTable để phản ánh lại dữ liệu được thay đổi sau khi thực hiện thao tác Ghi cập nhật
        /// </summary>
        private void UpdateDataTable()
        {
            try
            {
                DataRow drRow = Utility.FetchOnebyCondition(m_dtData,DmucKieukham.Columns.IdKieukham+ "=" + txtID.Text.ToString());
                if (drRow == null) return;
                drRow[DmucKieukham.Columns.MaKieukham] = Utility.DoTrim(txtMa.Text);
                drRow[DmucKieukham.Columns.TenKieukham] = Utility.DoTrim(txtTen.Text);
                drRow[DmucKieukham.Columns.NhomBaocao] = cbonhombaocao.SelectedValue.ToString();
                drRow[DmucKieukham.Columns.MaDoituongkcb] = cbodoituongkcb.SelectedValue.ToString();
                drRow["ten_nhom_baocao"] = cbonhombaocao.SelectedIndex == 0 ? "Chưa có" : cbonhombaocao.Text;
                drRow[DmucDoituongkcb.Columns.TenDoituongKcb] = cbonhombaocao.SelectedIndex == 0 ? "Tất cả" : cbodoituongkcb.Text;
                drRow[DmucKieukham.Columns.SttHthi] = Convert.ToInt16(txtSTT.Text);
                drRow[DmucKieukham.Columns.DonGia] = nmrDongia.Value;
                drRow[DmucKieukham.Columns.TrangThai] = chkTrangthai.Checked ? 1 : 0;
                m_dtData.AcceptChanges();
                UpdateSTT(short.Parse(txtSTT.Text));
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
                if (v_blnActResult && !chkAutoNew.Checked)
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
        private DmucKieukham GetObject()
        {
            try
            {

                DmucKieukham obj = new DmucKieukham();
                obj.MaKieukham = Utility.DoTrim(txtMa.Text);
                obj.TenKieukham = Utility.DoTrim(txtTen.Text);
                obj.NhomBaocao = Utility.sDbnull(cbonhombaocao.SelectedValue, "-1");
                obj.TrangThai = Convert.ToByte(chkTrangthai.Checked ? 1 : 0);
                obj.DonGia =nmrDongia.Value;
                obj.MaDoituongkcb = cbodoituongkcb.SelectedIndex == 0 ? "ALL" : cbodoituongkcb.SelectedValue.ToString();
                if (m_enAct == action.Update)
                {
                    obj.IdKieukham = Utility.Int16Dbnull(txtID.Text);
                    obj.NguoiSua = globalVariables.UserName;
                    obj.NgaySua = globalVariables.SysDate;
                }
                else
                {
                    obj.NguoiTao = globalVariables.UserName;
                    obj.NgayTao = globalVariables.SysDate;
                }
                obj.SttHthi = short.Parse(txtSTT.Text);
                return obj;
               
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi xảy ra khi đóng gói dữ liệu vào DataTable để gửi lên Webservice\n" + ex.Message, "Thông báo");
                return null;
            }


        }

        /// <summary>
        /// Thực hiện thêm mới dữ liệu
        /// </summary>
        private bool PerformInserAct()
        {
            try
            {
                //Kiem tra su hop le cua du lieu
                if (!IsValidInputData()) return false;
                m_intOldOrder = Convert.ToInt32(txtSTT.Text);
                string ActResult = "";
                DmucKieukham _item = GetObject();
                m_BusRules.InsertList(_item, m_intOldOrder, ref ActResult);
                if (ActResult == ActionResult.Success.ToString())
                {
                    txtID.Text = _item.IdKieukham.ToString();
                    //Cho phép chọn trên lưới để fill dữ liệu xuống Vùng nhập liệu
                    m_blnAllowCurrentCellChanged = true;
                    //Thêm mới dòng này vào DataTable để phản ánh lại lên lưới
                    InsertDataTable();
                    //Update lại STT nếu có
                    UpdateSTT(m_intOldOrder);
                    //Tự động nhảy đến dòng mới thêm trên lưới
                    Utility.GonewRowJanus(grdList, DmucKieukham.Columns.IdKieukham, txtID.Text.Trim());
                   
                    //Gán biến dòng hiện thời trên lưới
                    m_intCurrIdx = grdList.CurrentRow.Position;
                    //Quay về trạng thái cancel
                    PerformCancelAction();
                    //Hiển thị thông báo thành công
                    Utility.SetMsg(lblMsg, "Thêm mới dịch vụ khám thành công", false);
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
                    Utility.SetMsg(lblMsg, m_lstHeaders[0] + "(" + txtMa.Text + ") đã được sử dụng. Đề nghị bạn nhập mã khác!", true);
                    txtMa.Focus();
                    return false;
                }
                else if (ActResult == ActionResult.Exception.ToString())
                {

                    Utility.ShowMsg("Lỗi khi thực hiện thêm mới " + m_strListName + "\n" + ActResult, "Thông báo");
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
                m_BusRules.UpdateList(GetObject(), m_strOldCode, m_intOldOrder, ref ActResult);
                if (ActResult == ActionResult.Success.ToString())
                {
                    //Cho phép chọn trên lưới để fill dữ liệu xuống Vùng nhập liệu
                    m_blnAllowCurrentCellChanged = true;
                    Utility.SetMsg(lblMsg, "Cập nhật dịch vụ khám thành công!", false);
                    //Update lại vào DataTable để phản ánh lên lưới
                    UpdateDataTable();
                    //Tự động nhảy đến dòng mới thêm trên lưới
                    Utility.GonewRowJanus(grdList, DmucKieukham.Columns.IdKieukham, txtID.Text.Trim());
                    //Quay về trạng thái cancel
                    PerformCancelAction();
                }
                else if (ActResult == ActionResult.ExistedRecord.ToString())
                {

                    Utility.SetMsg(lblMsg, "Mã "  + txtMa.Text + " đã được sử dụng. Đề nghị bạn nhập mã khác!", true);
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
                string ActResult = "";

                if (!Utility.AcceptQuestion("Bạn có muốn xóa dịch vụ khám đang chọn hay không?", "Xác nhận trước khi xóa danh mục", true)) return false;
                //Thực hiện hành động xóa
                DmucDichvukcb objItem=new Select().From(DmucDichvukcb.Schema).Where(DmucDichvukcb.Columns.IdKieukham).IsEqualTo(Utility.Int16Dbnull( txtID.Text.Trim()))
                    .ExecuteSingle<DmucDichvukcb>();
                if (objItem != null)
                {
                    Utility.ShowMsg("Kiểu khám đang chọn xóa đã được sử dụng trong danh mục dịch vụ khám nên bạn không thể xóa");
                    return false;
                }
                m_BusRules.DeleteList(Utility.Int16Dbnull( txtID.Text.Trim()), ref ActResult);
                if (ActResult == ActionResult.Success.ToString())
                {
                    RemoveRowfromDataTable(txtID.Text.Trim());
                    //Thông báo xóa thành công
                    Utility.SetMsg(lblMsg, "Đã xóa dịch vụ khám thành công ?", false);
                   
                }

                else if (ActResult == ActionResult.Exception.ToString())
                {
                    Utility.ShowMsg(ActResult);
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
        void RemoveRowfromDataTable(string id)
        {
            try
            {
                //Xóa khỏi DataTable để phản ánh lên lưới
                DataRow drDeleteRow = Utility.FetchOnebyCondition(m_dtData, DmucKieukham.Columns.IdKieukham + "=" + id);
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
                m_dtData = m_BusRules.dsGetList("-1","ALL").Tables[0];
                if (m_dtData == null)
                {
                        Utility.ShowMsg("Có lỗi khi tìm kiếm dữ liệu:\n" , "Thông báo");

                }
                //Gán dữ liệu vào lưới
                Utility.SetDataSourceForDataGridEx(grdList, m_dtData, true, true, "1=1", DmucKieukham.Columns.SttHthi+","+DmucKieukham.Columns.TenKieukham);
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
            txtSTT.Enabled = isEnable;
            cbodoituongkcb.Enabled = isEnable;
            chkTrangthai.Enabled = isEnable;
            cbonhombaocao.Enabled = isEnable;
            nmrDongia.Enabled = isEnable;
        }
        /// <summary>
        /// Xóa trắng vùng nhập liệu khi Thêm mới hoặc Khi không có dữ liệu trên lưới
        /// </summary>
        void ResetDataRegion()
        {
            txtID.Text = "-1";
            txtMa.Text = "";
            txtTen.Text = "";
            txtSTT.Text = "";
            nmrDongia.Value = 0;
            cbonhombaocao.SelectedIndex = 0;
            cbodoituongkcb.SelectedIndex = 0;
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
                #region Tự động lấy Max STT hiển thị
                txtSTT.Text = m_BusRules.GetMaxSTT(m_strListType).ToString();
                #endregion

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
                //txtMa.Enabled = false;
                //Lấy Mã cũ để hoán đổi Mã nếu người dùng sửa cả Mã=Mã của một danh mục có sẵn
                m_strOldCode = txtMa.Text.Trim();
                //Lấy STT cũ để hoán đổi STT nếu người dùng sửa cả STT=STT của một danh mục có sẵn
                m_intOldOrder = Convert.ToInt32(txtSTT.Text);
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
        void frm_dmuc_kieukham_Load(object sender, EventArgs e)
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

         void frm_dmuc_kieukham_KeyDown(object sender, KeyEventArgs e)
        {
            
            try
            {
                if (e.KeyCode == Keys.F5)
                    SearchData();
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{TAB}");
                    return;
                }
                if (e.KeyCode == Keys.Escape)
                {
                    cmdCancel_Click(cmdCancel, new EventArgs());
                    return;
                }
                if (e.Control && e.KeyCode == Keys.N) cmdNew_Click(cmdNew, new EventArgs());
                if (e.Control && e.KeyCode == Keys.U) cmdUpdate_Click(cmdUpdate, new EventArgs());
                if (e.Control && e.KeyCode == Keys.D) cmdDelete_Click(cmdDelete, new EventArgs());
                if (e.Control && e.KeyCode == Keys.P) cmdPrint_Click(cmdPrint, new EventArgs());
                if (e.Control && e.KeyCode == Keys.S) cmdSave_Click(cmdSave, new EventArgs());

            }
            catch
            {
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
                    txtID.Text = dr[DmucKieukham.Columns.IdKieukham].ToString();
                    txtMa.Text = dr[DmucKieukham.Columns.MaKieukham].ToString();
                    txtTen.Text = dr[DmucKieukham.Columns.TenKieukham].ToString();
                    cbonhombaocao.SelectedIndex = Utility.GetSelectedIndex(cbonhombaocao, dr[DmucKieukham.Columns.NhomBaocao].ToString(),0);
                    cbodoituongkcb.SelectedIndex = Utility.GetSelectedIndex(cbodoituongkcb, dr[DmucKieukham.Columns.MaDoituongkcb].ToString(),0);
                    txtSTT.Text = dr[DmucKieukham.Columns.SttHthi].ToString();
                    nmrDongia.Value = Utility.DecimaltoDbnull(dr[DmucKieukham.Columns.DonGia], 0);
                    chkTrangthai.Checked = dr[DmucKieukham.Columns.TrangThai].ToString() == "1" ? true : false;
                    
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
               
            }
            catch
            {

            }
        }

        void window_Closed(object sender, EventArgs e)
        {
        }
        #endregion
    }
}
