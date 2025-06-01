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
using Janus.Windows.GridEX;
using SubSonic;
namespace VNS.HIS.UI.DANHMUC
{
    public partial class DMUC_DCHUNG : Form
    {
        #region Khai báo các biến cấp Module
        DMUC_CHUNG_BUSRULE m_BusRules = new DMUC_CHUNG_BUSRULE();
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
        CallAction _callAct = CallAction.FromMenu;
        public bool m_blnCancel = true;
        //Khai báo biến xác định xem đã chạy Method Loaded của UserControl hay chưa để tránh việc bị chạy lại hàm này nhiều lần
        public bool m_blnHasLoad = false;
        //Khai báo biến cho phép bắt sự kiện Currentcell changed trên lưới hay không?
        bool m_blnAllowCurrentCellChanged = true;
        //Biến xác định Index của dòng hiện thời trên lưới
        int m_intCurrIdx = 0;
        const string _CANCEL = "Hủy";
        const string _THOAT = "Thoát";
        #endregion
         bool SaveAs = false;
        string _name = "";
        private string loaidanhmuc = "";
        DmucKieudmuc kieudanhmuc = null;
        public DMUC_DCHUNG(string p_strArgs)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            //Khởi tạo sự kiện
            InitEvents();
            loaidanhmuc = p_strArgs;
            
            //Phân tích tham số để khởi tạo User interface(UI)
            AnalyzeArguments(p_strArgs);
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
                this.Load += new EventHandler(DMUC_DCHUNG_Load);
                //Bắt sự kiện KeyDown trên form
                this.KeyDown += new KeyEventHandler(DMUC_DCHUNG_KeyDown);
                //Bắt sự kiện chọn 1 dòng trên lưới sẽ gán giá trị từ lưới vào các Control nhập liệu phía dưới
                grdList.SelectionChanged += new EventHandler(grdList_SelectionChanged);
                grdList.UpdatingCell += grdList_UpdatingCell;
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

        void grdList_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == "TEN")
                {
                    if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn đổi tên danh mục từ {0} sang {1}", e.InitialValue.ToString(), e.Value.ToString()), "Xác nhận", true))
                    {
                        DmucChung objdmuc = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo(m_strListType).And(DmucChung.Columns.Ma).IsEqualTo(Utility.sDbnull(grdList.CurrentRow.Cells["MA"].Value, "XXXYYYZZZ")).ExecuteSingle<DmucChung>();
                        if (objdmuc != null)
                        {
                            objdmuc.MarkOld();
                            objdmuc.IsNew = false;
                            objdmuc.Ten = Utility.sDbnull(e.Value, e.InitialValue);
                            objdmuc.Save();
                        }
                    }
                    else
                        e.Value = e.InitialValue;
                }
                if (e.Column.Key == "STT_HTHI")
                {
                    DmucChung objdmuc = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo(m_strListType).And(DmucChung.Columns.Ma).IsEqualTo(Utility.sDbnull(grdList.CurrentRow.Cells["MA"].Value, "XXXYYYZZZ")).ExecuteSingle<DmucChung>();
                    if (objdmuc != null)
                    {
                        objdmuc.MarkOld();
                        objdmuc.IsNew = false;
                        objdmuc.SttHthi = Utility.Int16Dbnull(e.Value, e.InitialValue);
                        objdmuc.Save();
                    }
                }
                if (e.Column.Key == "VIET_TAT")
                {
                    DmucChung objdmuc = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo(m_strListType).And(DmucChung.Columns.Ma).IsEqualTo(Utility.sDbnull(grdList.CurrentRow.Cells["MA"].Value, "XXXYYYZZZ")).ExecuteSingle<DmucChung>();
                    if (objdmuc != null)
                    {
                        objdmuc.MarkOld();
                        objdmuc.IsNew = false;
                        objdmuc.VietTat = Utility.sDbnull(e.Value, e.InitialValue);
                        objdmuc.Save();
                    }
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            
        }
        public void SetStatus(bool SaveAs, string _name)
        {
            this.SaveAs = SaveAs;
            this._name = _name;
           
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
        void AnalyzeArguments(string p_strArgs)
        {
            try
            {
                string[] arrValues = p_strArgs.Split('|');
                m_strListType = arrValues[0];
                if (arrValues.Length > 1)
                {
                    autoPhanloai.LOAI_DANHMUC = arrValues[1];
                    autoPhanloai.Init();
                    //grdList.RootTable.Columns["VIET_TAT"].Caption = arrValues[1];
                    //lblViettat.Text = arrValues[1];
                }
                DmucKieudmuc objKieuDMUC = m_BusRules.GetKieuDanhMuc(m_strListType);
                m_strListName = objKieuDMUC == null ? "Chưa khai báo loại danh mục chung" : objKieuDMUC.TenLoai;
                this.Text = m_strListName;
            }
            catch (Exception ex)
            {
            }
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

                drNewRow[DmucChung.Columns.Ma] =Utility.DoTrim( txtMa.Text);
                drNewRow[DmucChung.Columns.Ten] = Utility.DoTrim(txtTen.Text);
                drNewRow[DmucChung.Columns.Loai] = m_strListType;
                drNewRow[DmucChung.Columns.VietTat] = Utility.DoTrim(txtViettat.Text);

                drNewRow[DmucChung.Columns.SttHthi] = Convert.ToInt16(txtSTT.Text);
                drNewRow[DmucChung.Columns.Phanloai] = autoPhanloai.myCode;

                drNewRow[DmucChung.Columns.TrangThai] = chkTrangthai.Checked ? 1 : 0;
                drNewRow[DmucChung.Columns.TrangthaiMacdinh] = Utility.Bool2byte(chkDefault.Checked);

                drNewRow[DmucChung.Columns.MotaThem] = Utility.DoTrim(txtMotathem.Text);
                drNewRow[DmucChung.Columns.NguoiTao] = globalVariables.UserName;
                drNewRow[DmucChung.Columns.NgayTao] = globalVariables.SysDate;

                m_dtData.Rows.Add(drNewRow);
                if (chkDefault.Checked)
                {
                    foreach (DataRow dr in m_dtData.Rows)
                    {
                        if (dr[DmucChung.Columns.Ma].ToString() != drNewRow[DmucChung.Columns.Ma].ToString())
                            dr[DmucChung.Columns.TrangthaiMacdinh] = 0;
                    }
                }
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
                DataRow drRow = Utility.FetchOnebyCondition(m_dtData, "Ma='" + m_strOldCode + "'");
                if (drRow == null) return;
                drRow[DmucChung.Columns.Ten] = Utility.DoTrim(txtTen.Text);
                drRow[DmucChung.Columns.Loai] = m_strListType;
                drRow[DmucChung.Columns.Ma] =Utility.DoTrim( txtMa.Text);
                drRow[DmucChung.Columns.SttHthi] = short.Parse(txtSTT.Text);
                drRow[DmucChung.Columns.TrangThai] = chkTrangthai.Checked ? 1 : 0;
                drRow[DmucChung.Columns.Phanloai] = autoPhanloai.myCode;
                drRow[DmucChung.Columns.TrangthaiMacdinh] =Utility.Bool2byte( chkDefault.Checked );
                drRow[DmucChung.Columns.MotaThem] = Utility.DoTrim(txtMotathem.Text);
                drRow[DmucChung.Columns.VietTat] = Utility.DoTrim(txtViettat.Text);
               
                if (chkDefault.Checked)
                {
                    foreach (DataRow dr in m_dtData.Rows)
                    {
                        if (dr[DmucChung.Columns.Ma].ToString() != drRow[DmucChung.Columns.Ma].ToString())
                            dr[DmucChung.Columns.TrangthaiMacdinh] = 0;
                    }
                }
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
            cmdDelete.Visible = Utility.isValidGrid(grdList);
            
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
                        v_blnActResult = true;
                        PerformDeleteAct();
                        break;
                    default:
                        break;
                }
                if (m_blnCancel)
                    m_blnCancel = !v_blnActResult;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                txtMa.Init(m_dtData, new List<string>() { DmucChung.Columns.Ma, DmucChung.Columns.Ma, DmucChung.Columns.Ten });
               // txtTen.Init(m_dtData, new List<string>() { DmucChung.Columns.Ten, DmucChung.Columns.Ten, DmucChung.Columns.Ten });
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
        private DmucChung GetObject()
        {
            try
            {

                DmucChung obj = new DmucChung();
                obj.Ma = Utility.DoTrim(txtMa.Text);
                obj.Ten = Utility.DoTrim(txtTen.Text);
                obj.Loai = m_strListType;
                obj.TrangThai = Utility.Bool2byte(chkTrangthai.Checked);
                obj.TrangthaiMacdinh = Utility.Bool2byte(chkDefault.Checked );
                obj.MotaThem = Utility.DoTrim(txtMotathem.Text);
                obj.Phanloai = autoPhanloai.myCode;
                obj.VietTat = Utility.DoTrim(txtViettat.Text);
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
                m_BusRules.InsertList(GetObject(), m_intOldOrder, ref ActResult);
                if (ActResult == ActionResult.Success.ToString())
                {
                    //Cho phép chọn trên lưới để fill dữ liệu xuống Vùng nhập liệu
                    m_blnAllowCurrentCellChanged = true;
                    //Thêm mới dòng này vào DataTable để phản ánh lại lên lưới
                    InsertDataTable();
                    //Update lại STT nếu có
                    UpdateSTT(m_intOldOrder);
                    //Tự động nhảy đến dòng mới thêm trên lưới
                    Utility.GonewRowJanus(grdList, DmucChung.Columns.Ma, txtMa.Text.Trim());
                   
                    //Gán biến dòng hiện thời trên lưới
                    m_intCurrIdx = grdList.CurrentRow.Position;
                    //Quay về trạng thái cancel
                    PerformCancelAction();
                    //Hiển thị thông báo thành công
                    Utility.SetMsg(lblMsg, "Thêm mới " + m_strListName + " thành công", false);
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
                    Utility.SetMsg(lblMsg, "Cập nhật " + m_strListName + " thành công!", false);
                    //Update lại vào DataTable để phản ánh lên lưới
                    UpdateDataTable();
                    //Tự động nhảy đến dòng mới thêm trên lưới
                    Utility.GonewRowJanus(grdList, DmucChung.Columns.Ma, txtMa.Text.Trim());
                    //Quay về trạng thái cancel
                    PerformCancelAction();
                }
                else if (ActResult == ActionResult.ExistedRecord.ToString())
                {

                    Utility.SetMsg(lblMsg, "Mã " + m_strListName + "(" + txtMa.Text + ") đã được sử dụng. Đề nghị bạn nhập mã khác!", true);
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
        bool isvalidDelete(string MA, string LOAI)
        {
            try
            {
                string errMsg = "";
                StoredProcedure sp = SPs.DmucChungCheckDelete(MA, LOAI, errMsg);
                DataTable dtexists = sp.GetDataSet().Tables[0];
                errMsg = Utility.sDbnull(sp.OutputValues[0]);
                if (errMsg.Length > 0 || dtexists.Rows.Count > 0)
                {
                    Utility.ShowMsg(string.Format("Mã {0} :{1} đã được sử dụng trong hệ thống nên bạn không thể xóa", kieudanhmuc != null ? kieudanhmuc.TenLoai : "", MA));
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return false;
            }
        }
        /// <summary>
        /// Thực hiện Xóa dữ liệu
        /// </summary>
        private void PerformDeleteAct()
        {
            try
            {
                
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa bản ghi đang được chọn trên lưới danh sách?", "Cảnh báo", true)) return;
                if (grdList.GetCheckedRows().Count() > 0)
                {
                    GridEXRow[] lst=grdList.GetCheckedRows();
                    foreach (GridEXRow _r in lst)
                    {
                        try
                        {
                            string ActResult = "";
                            if (!isvalidDelete(txtMa.Text.Trim(), m_strListType)) continue;//Bỏ qua để kiểm tra danh mục kế tiếp
                            StoredProcedure sp = SPs.DmucChungCheckDelete(_r.Cells["MA"].Value.ToString().Trim(), m_strListType, ActResult);
                            sp.Execute();

                            ActResult = Utility.sDbnull(sp.OutputValues[0]);

                            if (ActResult != "")
                            {
                                if (!Utility.AcceptQuestion(string.Format("{0}\nBạn có muốn tiếp tục thực hiện xóa các bản ghi khác. Nhấn YES để tiếp tục. Nhấn No để dừng", ActResult), "Cảnh báo", true))
                                    return;
                                else
                                    continue;
                            }
                           ActResult = "";
                            //Thực hiện hành động xóa
                           m_BusRules.DeleteList(_r.Cells["MA"].Value.ToString().Trim(), m_strListType, ref ActResult);
                            if (ActResult == ActionResult.Success.ToString())
                            {
                                RemoveRowfromDataTable(_r.Cells["MA"].Value.ToString().Trim());
                              
                            }
                            else if (ActResult == ActionResult.Exception.ToString())
                            {
                                Utility.ShowMsg(ActResult);
                            }

                        }
                        catch (Exception ex)
                        {
                            Utility.ShowMsg(ex.Message);
                        }
                    }
                }
                else
                {
                    string ActResult = "";
                    string DeleteContent = m_strListName + " (" + txtMa.Text.Trim() + "-" + txtTen.Text.Trim() + ")";
                    if (!isvalidDelete(txtMa.Text.Trim(), m_strListType)) return;
                    //if (!Utility.AcceptQuestion("Bạn có muốn xóa " + DeleteContent + " hay không?", "Xác nhận trước khi xóa danh mục", true)) return ;
                    StoredProcedure sp = SPs.DmucChungCheckDelete(txtMa.Text.Trim(), m_strListType, ActResult);
                    sp.Execute();

                     ActResult = Utility.sDbnull(sp.OutputValues[0]);

                     if (ActResult != "")
                    {
                        Utility.ShowMsg(ActResult);
                        return;
                    }
                     ActResult = "";
                    //Thực hiện hành động xóa
                     m_BusRules.DeleteList(txtMa.Text.Trim(), m_strListType, ref ActResult);
                    if (ActResult == ActionResult.Success.ToString())
                    {
                        RemoveRowfromDataTable(txtMa.Text.Trim());
                        //Thông báo xóa thành công
                        Utility.SetMsg(lblMsg, "Đã xóa " + DeleteContent + " thành công ?", false);

                    }

                    else if (ActResult == ActionResult.Exception.ToString())
                    {
                        Utility.ShowMsg(ActResult);
                    }
                }


            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }


        }
        void RemoveRowfromDataTable(string Ma)
        {
            try
            {
                //Xóa khỏi DataTable để phản ánh lên lưới
                DataRow drDeleteRow = Utility.FetchOnebyCondition(m_dtData, "Ma='" + Ma+"'");
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
                m_dtData = m_BusRules.dsGetList( m_strListType).Tables[0];
                if (m_dtData == null)
                {
                        Utility.ShowMsg("Có lỗi khi tìm kiếm dữ liệu:\n" , "Thông báo");

                }
                txtMa.Init(m_dtData, new List<string>() { DmucChung.Columns.Ma, DmucChung.Columns.Ma, DmucChung.Columns.Ma });
                //txtTen.Init(m_dtData, new List<string>() { DmucChung.Columns.Ten, DmucChung.Columns.Ten, DmucChung.Columns.Ten });
                //Gán dữ liệu vào lưới
                Utility.SetDataSourceForDataGridEx(grdList, m_dtData, true, true, "1=1", "STT_HTHI,TEN");
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
            txtViettat.Enabled = isEnable;
            chkTrangthai.Enabled = isEnable;
            chkDefault.Enabled = isEnable;
            txtMotathem.Enabled = isEnable;
        }
        /// <summary>
        /// Xóa trắng vùng nhập liệu khi Thêm mới hoặc Khi không có dữ liệu trên lưới
        /// </summary>
        void ResetDataRegion()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            txtSTT.Text = "";
            txtViettat.Clear();
            txtMotathem.Text = "";
            chkDefault.Checked = false;
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
        void DMUC_DCHUNG_Load(object sender, EventArgs e)
        {
            try
            {
                //chức năng trên Tab của Avalon
                if (m_blnHasLoad) return;
                //Chạy BasicFlow
                BasicFlow();
                kieudanhmuc = new Select().From(DmucKieudmuc.Schema).Where(DmucKieudmuc.Columns.MaLoai).IsEqualTo(m_strListType).ExecuteSingle<DmucKieudmuc>();
                grdList.Focus();
                //Gán biến xác định hàm này đã từng chạy để không bị chạy lại
                m_blnHasLoad = true;
                if (SaveAs)
                {
                    cmdNew_Click(cmdNew, new EventArgs());
                    txtTen.Text = _name;
                    txtMa.Focus();
                }
                //cmdDelete.Visible = globalVariables.IsAdmin;
            }
            catch (Exception ex)
            {
            }
        }
        public static Control FindFocusedControl(Control control)
        {
            var container = control as IContainerControl;
            while (container != null)
            {
                control = container.ActiveControl;
                container = control as IContainerControl;
            }
            return control;
        }


        /// <summary>
        /// Bắt sự kiện KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

         void DMUC_DCHUNG_KeyDown(object sender, KeyEventArgs e)
        {
            
            try
            {
                if (e.KeyCode == Keys.F5)
                    SearchData();
                if (e.KeyCode == Keys.Enter)
                {
                    //var activeControl = FindFocusedControl(this);
                    if (uiTabPage1.ActiveControl != null && uiTabPage1.ActiveControl.Name == txtTen.Name && chkMultiLine.Checked)
                    {
                    }
                    else
                    {
                        SendKeys.Send("{TAB}");
                        return;
                    }
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
                    txtMa._Text = dr[DmucChung.Columns.Ma].ToString();
                    txtTen.Text = dr[DmucChung.Columns.Ten].ToString();
                    txtViettat.Text = dr[DmucChung.Columns.VietTat].ToString();
                    txtSTT.Text = dr[DmucChung.Columns.SttHthi].ToString();
                    autoPhanloai.SetCode(dr[DmucChung.Columns.Phanloai].ToString());
                    chkDefault.Checked = Utility.Byte2Bool(dr[DmucChung.Columns.TrangthaiMacdinh]);
                    chkTrangthai.Checked = dr[DmucChung.Columns.TrangThai].ToString() == "1" ? true : false;
                    txtMotathem.Text = dr[DmucChung.Columns.MotaThem].ToString();
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

        private void chkMultiLine_CheckedChanged(object sender, EventArgs e)
        {
            //txtTen.Multiline = chkMultiLine.Checked;
            //if (chkMultiLine.Checked)
            //{
            //    txtTen.Height = 80;
            //}
        }

        private void cmdExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_dtData != null)
                    m_dtData.DataSet.WriteXml(string.Format(@"{0}\{1}.xml", Application.StartupPath, m_strListType), XmlWriteMode.WriteSchema);
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            
        }

        private void cmdImport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog _openFd = new OpenFileDialog();
                if (_openFd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(_openFd.FileName);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                        if (m_dtData.Select(string.Format("MA='{0}' and LOAI='{1}'", dr["MA"].ToString(), m_strListType)).Length <= 0)
                        {
                            DmucChung _newItem = new DmucChung();
                            _newItem.IsNew = true;
                            _newItem.Loai = m_strListType;
                            _newItem.Ma = dr["MA"].ToString();
                            _newItem.Ten = dr["TEN"].ToString();
                            _newItem.NguoiTao = globalVariables.UserName;
                            _newItem.NgayTao = DateTime.Now;
                            _newItem.SttHthi = Utility.Int32Dbnull(dr["STT_HTHI"], 100);
                            _newItem.TrangThai = Utility.Int32Dbnull(dr["TRANG_THAI"], 0);
                            _newItem.TrangthaiMacdinh = Utility.ByteDbnull(dr["TRANGTHAI_MACDINH"], 0);
                            _newItem.Save();
                            m_dtData.ImportRow(dr);

                        }
                    m_dtData.AcceptChanges();
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            
        }

        private void cmdUpdateGroup_Click(object sender, EventArgs e)
        {
            if (grdList.GetCheckedRows().Count()<=0 || autoPhanloai.myCode == "-1")
            {
                Utility.ShowMsg("Bạn cần chọn phân nhóm và ít nhất 1 hạng mục trên danh sách lưới dữ liệu trước khi thực hiện cập nhật phân nhóm");
                return;
            }
            if (Utility.AcceptQuestion("Bạn có chắc chắn muốn cập nhật thông tin phân loại danh mục cho các mục đang được chọn", "Xác nhận", true))
            {
                foreach (GridEXRow _row in grdList.GetCheckedRows())
                {
                    try
                    {
                        new Update(DmucChung.Schema).Set(DmucChung.Columns.Phanloai).EqualTo(autoPhanloai.myCode).Where(DmucChung.Columns.Ma).IsEqualTo(_row.Cells["ma"].Value).And(DmucChung.Columns.Loai).IsEqualTo(m_strListType).Execute();
                       
                    }
                    catch (Exception ex)
                    {

                        Utility.CatchException(ex);
                    }
                }
                Utility.ShowMsg("Cập nhật phân nhóm thành công. Nhấn OK để kết thúc");
                SearchData();
            }
        }

        private void cmdCapnhatLoaiDmuc_Click(object sender, EventArgs e)
        {
            if(!Utility.Coquyen("DANHMUCCHUNG_CAPNHAT_LOAI"))
            {
                Utility.thongbaokhongcoquyen("DANHMUCCHUNG_CAPNHAT_LOAI", "cập nhật loại danh mục chung");
                return;
            }    
           
            if(Utility.sDbnull(txtLoai.Text).Length<=0)
            {
                Utility.ShowMsg("Bạn cần nhập thông tin loại danh mục mới");
                txtLoai.Focus();
                return;
            }    
            if(grdList.GetCheckedRows().Count()<=0)
            {
                Utility.ShowMsg("Bạn cần chọn các dữ liệu cần cập nhật trên lưới danh sách phía trên trước khi thực hiện");
                return;
            }    
            if (Utility.AcceptQuestion(string.Format( "Bạn có chắc chắn muốn cập nhật thông tin loại danh mục cho các dữ liệu được chọn từ {0} thành {1} hay không?", m_strListType,Utility.sDbnull(txtLoai.Text)), "Xác nhận", true))
            {
                foreach (GridEXRow _row in grdList.GetCheckedRows())
                {
                    try
                    {
                        new Update(DmucChung.Schema).Set(DmucChung.Columns.Loai).EqualTo(Utility.sDbnull(txtLoai.Text)).Where(DmucChung.Columns.Ma).IsEqualTo(_row.Cells["ma"].Value).And(DmucChung.Columns.Loai).IsEqualTo(m_strListType).Execute();
                    }
                    catch (Exception ex)
                    {

                        Utility.CatchException(ex);
                    }
                }
                Utility.ShowMsg("Cập nhật loại danh mục thành công. Nhấn OK để kết thúc");
                m_strListType = Utility.sDbnull(txtLoai.Text);
                SearchData();
            }
        }

        private void cmdCapnhatTatca_Click(object sender, EventArgs e)
        {
            if (!Utility.Coquyen("DANHMUCCHUNG_CAPNHAT_LOAI"))
            {
                Utility.thongbaokhongcoquyen("DANHMUCCHUNG_CAPNHAT_LOAI", "cập nhật loại danh mục chung");
                return;
            }
            if (Utility.sDbnull(txtLoai.Text).Length <= 0)
            {
                Utility.ShowMsg("Bạn cần nhập thông tin loại danh mục mới");
                txtLoai.Focus();
                return;
            }
            if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn cập nhật thông tin loại danh mục từ {0} thành {1} hay không?", m_strListType, Utility.sDbnull(txtLoai.Text)), "Xác nhận", true))
            {
                try
                {
                    new Update(DmucChung.Schema).Set(DmucChung.Columns.Loai).EqualTo(Utility.sDbnull(txtLoai.Text)).Where(DmucChung.Columns.Loai).IsEqualTo(m_strListType).Execute();
                }
                catch (Exception ex)
                {

                    Utility.CatchException(ex);
                }

                Utility.ShowMsg("Cập nhật loại danh mục thành công. Nhấn OK để kết thúc");
                m_strListType = Utility.sDbnull(txtLoai.Text);
                SearchData();
            }
        }
    }
}
