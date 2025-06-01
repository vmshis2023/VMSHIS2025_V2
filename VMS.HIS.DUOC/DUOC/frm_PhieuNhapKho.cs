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
using SubSonic;
using VMS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.HIS.UI.Forms.Cauhinh;
using System.IO;
namespace VNS.HIS.UI.THUOC
{
    /// <summary>
    /// 
    /// </summary>
    public partial class frm_PhieuNhapKho : Form
    {
        #region "khai báo biến"
        THUOC_NHAPKHO _NHAPKHO = new THUOC_NHAPKHO();
        private int Distance = 488;
        private bool b_Hasloaded = false;
        private string FileName = string.Format("{0}/{1}", Application.StartupPath, string.Format("SplitterDistancefrm_PhieuNhapKho.txt"));
        private DataTable m_dtDataNhapKho=new DataTable();
        string KIEU_THUOC_VT = "THUOC";
        string SplitterPath = "";
        private int SplitterDistance
        {
            get { return Distance; }
            set { Distance = value; }
        }
        #endregion

        #region "Khởi tạo Form"
        public frm_PhieuNhapKho(string KIEU_THUOC_VT)
        {
            InitializeComponent();
            this.FormClosing += frm_PhieuNhapKho_FormClosing;
            SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
            this.Shown += frm_PhieuNhapKho_Shown;
            Utility.SetVisualStyle(this);
            this.KIEU_THUOC_VT = KIEU_THUOC_VT;
            grdPhieuNhapChiTiet.SelectionChanged+=grdPhieuNhapChiTiet_SelectionChanged;
            //txtTieuDe.Text = BusinessHelper.GetTieuDeBaoCao(this.Name, txtTieuDe.Text);
            dtFromdate.Value = globalVariables.SysDate.AddMonths(-1);
            dtToDate.Value = globalVariables.SysDate;
            CauHinh();
            setquyen();
            this.KeyUp += frm_PhieuNhapKho_KeyUp;
        }

        void frm_PhieuNhapKho_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
        }
       
        void grdPhieuNhapChiTiet_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                txtGiaban.Text =Utility.sDbnull( grdPhieuNhapChiTiet.GetValue("Gia_ban"),"0");
            }
            catch (Exception ex)
            {
                
               
            }
        }

        void frm_PhieuNhapKho_FormClosing(object sender, FormClosingEventArgs e)
        {
            Utility.SaveValue2Lines(SplitterPath, new List<string>() { splitContainer1.SplitterDistance.ToString() });
        }
        void Try2Splitter()
        {
            try
            {
                List<int> lstSplitterSize = (from p in File.ReadLines(SplitterPath)
                                             select Utility.Int32Dbnull(p)).ToList<int>();
                if (lstSplitterSize != null && lstSplitterSize.Count == 1)
                {
                    splitContainer1.SplitterDistance = lstSplitterSize[0];
                }
            }
            catch (Exception)
            {

            }
        }
        void frm_PhieuNhapKho_Shown(object sender, EventArgs e)
        {
            Try2Splitter();
        }
        void setquyen()
        {
            try
            {
                cmdThemPhieuNhap.Visible = Utility.Coquyen("thuoc_phieunhapkho_themmoi");
                cmdUpdatePhieuNhap.Visible = Utility.Coquyen("thuoc_phieunhapkho_sua");
                cmdXoaPhieuNhap.Visible = Utility.Coquyen("thuoc_phieunhapkho_xoa");
                cmdNhapKho.Visible = Utility.Coquyen("thuoc_phieunhapkho_xacnhan");
                cmdHuyXacnhan.Visible = Utility.Coquyen("thuoc_phieunhapkho_huyxacnhan");
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        private void txtTieuDe_LostFocus(object sender, EventArgs eventArgs)
        {
            //BusinessHelper.UpdateTieuDe(this.Name, txtTieuDe.Text);
        }
        private void CauHinh()
        {
          
            cmdConfig.Visible = globalVariables.IsAdmin;
            
          
        }
        #endregion

     
       
        /// <summary>
        /// hàm thực hiện viecj load thong tin của Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_PhieuNhapKho_Load(object sender, EventArgs e)
        {
            InitData();
            TIMKIEM_THONGTIN();
            b_Hasloaded = true;
            ModifyCommand();
           
        }
        private DataTable m_dtKhoThuoc=new DataTable();
        /// <summary>
        /// hàm thực hiện việc khởi tạo thông tin của Form
        /// </summary>
        private void InitData()
        {
            DataBinding.BindDataCombobox(cboNhaCungcap, THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHACUNGCAP", true), DmucChung.Columns.Ma, DmucChung.Columns.Ten, "---Chọn NCC---", false);
            if (KIEU_THUOC_VT == "THUOC")
            {
                m_dtKhoThuoc = CommonLoadDuoc.LAYDANHMUCKHO(-1, "ALL", "THUOC,THUOCVT", "CHANLE,CHAN", 0, 100, 1);// CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_CHAN();
            }
            else
            {
                m_dtKhoThuoc = CommonLoadDuoc.LAYDANHMUCKHO(-1,"ALL", "VT,THUOCVT", "CHANLE,CHAN", 0, 100, 1);// CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_CHAN();
            }
            if (m_dtKhoThuoc.Rows.Count > 1)
            {
                DataBinding.BindDataCombobox(cboKhoThuoc, m_dtKhoThuoc, TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho, "---Chọn kho---", false);
            }
            else
            {
                DataBinding.BindDataCombobox(cboKhoThuoc, m_dtKhoThuoc, TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho, "---Chọn---", true);
               
            }
            cboKhoThuoc.Tag = m_dtKhoThuoc;
            //DataBinding.BindDataCombobox(cboKhoNhap, m_dtKhoNhap,
            //                          TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho,
            //                          "---Kho nhập---",false);
            DataBinding.BindDataCombobox(cboNhanvien, CommonLoadDuoc.LAYTHONGTIN_NHANVIEN(), DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "---Chọn---", true);
           
            txtTenthuoc.Init(CommonLoadDuoc.LayThongTinThuoc(KIEU_THUOC_VT),
                             new List<string>()
                                 {DmucThuoc.Columns.IdThuoc, DmucThuoc.Columns.MaThuoc, DmucThuoc.Columns.TenThuoc});

        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện việc trạng thái của phần từ ngay tới ngày
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromdate.Enabled = chkByDate.Checked;
        }

        private void frm_PhieuNhapKho_KeyDown(object sender, KeyEventArgs e)
        {
           // if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if(e.KeyCode==Keys.N && e.Control)cmdThemPhieuNhap.PerformClick();
            if(e.KeyCode==Keys.E && e.Control)cmdUpdatePhieuNhap.PerformClick();
            if(e.KeyCode==Keys.D && e.Control)cmdXoaPhieuNhap.PerformClick();
            if(e.KeyCode==Keys.P && e.Control)cmdInPhieuNhapKho.PerformClick();

            if (e.KeyCode == Keys.X && e.Control) cmdNhapKho.PerformClick();
            if (e.KeyCode == Keys.Z && e.Control) cmdHuyXacnhan.PerformClick();

            if (e.KeyCode == Keys.F3 || (e.KeyCode == Keys.F && e.Control)) cmdSearch.PerformClick();
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
        }
        private void LoadAuCompleteThuoc()
        {
          
        }
        /// <summary>
        /// hàm thực hiện việc tim kiem thong tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            TIMKIEM_THONGTIN();
            
        }
        /// <summary>
        /// HÀM THỰC HIỆN TÌM KIẾM THÔNG TIN 
        /// </summary>
        private void TIMKIEM_THONGTIN()
        {
            int TRANG_THAI = -1;
            if(radDaNhap.Checked) TRANG_THAI = 1;
            if(radChuaNhapKho.Checked) TRANG_THAI = 0;
            string manhacungcap="ALL";
            if(Utility.sDbnull(cboNhaCungcap.SelectedValue)!="")
                manhacungcap = Utility.sDbnull(cboNhaCungcap.SelectedValue);
            string MaKho = "-1";
            
            m_dtDataNhapKho =
                _NHAPKHO.Laydanhsachphieunhapkho(chkByDate.Checked ? dtFromdate.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                                             chkByDate.Checked ? dtToDate.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                                             Utility.Int32Dbnull(txtTenthuoc.MyID,-1),-1,
                                             Utility.Int32Dbnull(cboKhoThuoc.SelectedValue, -1), -1,
                                             Utility.Int32Dbnull(cboNhanvien.SelectedValue, -1),
                                             -1, manhacungcap, Utility.sDbnull(txtSoPhieu.Text), TRANG_THAI, 
                                             (int)LoaiPhieu.PhieuNhapKho, MaKho, 2, KIEU_THUOC_VT);

            Utility.SetDataSourceForDataGridEx_Basic(grdList,m_dtDataNhapKho,true,true,"1=1","");
            if (!Utility.isValidGrid(grdList)) if (m_dtDataPhieuChiTiet != null) m_dtDataPhieuChiTiet.Clear();
            ModifyCommand();
        }
        /// <summary>
        /// hàm thực hiện việc thêm mới thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdThemPhieuNhap_Click(object sender, EventArgs e)
        {
            try
            {
                using (frm_Themmoi_Phieunhapkho frm = new frm_Themmoi_Phieunhapkho())
                {
                    frm.m_enAction = action.Insert;
                    frm._OnActionSuccess += frm__OnActionSuccess;
                    frm.p_mDataPhieuNhapKho = m_dtDataNhapKho;
                    frm.KIEU_THUOC_VT = KIEU_THUOC_VT;
                    frm.grdList = grdList;
                    frm.txtIDPhieuNhapKho.Text = "-1";
                    frm.ShowDialog();
                    if (!frm.b_Cancel)
                    {
                        grdList_SelectionChanged(grdList, new EventArgs());
                    }
                }
            }
            catch (Exception exception)
            {
                if(globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
              
            }
            finally
            {
                ModifyCommand();
            }
            
        }

        void frm__OnActionSuccess()
        {
            grdList_SelectionChanged(grdList, new EventArgs());
        }
        private bool InValiUpdateXoa()
        {
            int IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
            SqlQuery sqlQuery = new Select().From(TPhieuNhapxuatthuoc.Schema)
                .Where(TPhieuNhapxuatthuoc.Columns.TrangThai).IsEqualTo(1)
                .And(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(IdPhieu);
            if(sqlQuery.GetRecordCount()>0)
            {
                Utility.ShowMsg("Phiếu nhập kho đang chọn đã được xác nhận. Bạn không thể sửa hoặc xóa thông tin","Thông báo",MessageBoxIcon.Warning);
                return false;
            }
            TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc = TPhieuNhapxuatthuoc.FetchByID(IdPhieu);
            if (Utility.sDbnull(objTPhieuNhapxuatthuoc.NguoiTao) != globalVariables.UserName && !globalVariables.IsAdmin)// && THU_VIEN_CHUNG.Laygiatrithamsohethong_off("THUOC_PHANQUYEN_USER", "1", false) == "1")
            {
                Utility.ShowMsg(
                    string.Format(
                        "Phiếu số {0} được tạo bởi tài khoản {1}. \n Xin liên hệ với người dùng {1} để sửa hoặc xóa, nhập phiếu&n.Bạn đang vào với UID={2}",
                        objTPhieuNhapxuatthuoc.IdPhieu, objTPhieuNhapxuatthuoc.NguoiTao, globalVariables.UserName));
                return false;
            }
            return true;
        }
        /// <summary>
        /// hàm thực hiện cập nhâp thôn tin phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdUpdatePhieuNhap_Click(object sender, EventArgs e)
        {
            try
            {
                if(!InValiUpdateXoa())return;
                int ITPhieuNhapxuatthuoc = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
                using (frm_Themmoi_Phieunhapkho frm = new frm_Themmoi_Phieunhapkho())
                {
                    frm._OnActionSuccess += frm__OnActionSuccess;
                    frm.m_enAction = action.Update;
                    frm.KIEU_THUOC_VT = KIEU_THUOC_VT;
                    frm.grdList = grdList;
                    frm.p_mDataPhieuNhapKho = m_dtDataNhapKho;
                    frm.txtIDPhieuNhapKho.Text = Utility.sDbnull(ITPhieuNhapxuatthuoc);

                    frm.ShowDialog();
                    if (!frm.b_Cancel)
                    {
                        grdList_SelectionChanged(grdList, new EventArgs());
                    }
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }

            }
            finally
            {
                ModifyCommand();
            }
        }
        /// <summary>
        /// hàm thực hiện việc xóa thông tin chi tiết của phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoaPhieuNhap_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(uiStatusBar1.Panels["MSG"], "", false);
            int ITPhieuNhapxuatthuoc = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
            string khoxuat = Utility.sDbnull(grdList.CurrentRow.Cells["ten_khoxuat"].Value, "");
            string khonhap = Utility.sDbnull(grdList.CurrentRow.Cells["ten_khonhap"].Value, "");
            if (!InValiUpdateXoa())return;
            if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa thông tin phiếu nhập kho với mã phiếu {0} hay không?", ITPhieuNhapxuatthuoc),"Thông báo",true))
            {
                ActionResult actionResult = _NHAPKHO.XoaPhieuNhapKho(ITPhieuNhapxuatthuoc);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        grdList.CurrentRow.Delete();
                        grdList.UpdateData();
                        m_dtDataNhapKho.AcceptChanges();
                         m_dtDataNhapKho.AcceptChanges();
                        Utility.Log(this.Name, globalVariables.UserName,
                                           string.Format(
                                               "Xóa phiếu nhập kho với số phiếu là :{0} -Tại kho {1}",
                                               ITPhieuNhapxuatthuoc, khoxuat), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                        Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn xóa thông tin phiếu nhập kho thành công", false);
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình xóa thông tin của phiếu nhập kho", "Thông báo lỗi", MessageBoxIcon.Error);
                        break;
                }

            }
            ModifyCommand();
        }
        private DataTable m_dtDataPhieuChiTiet = new DataTable();
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            if(grdList.CurrentRow!=null&&grdList.CurrentRow.RowType==RowType.Record)
            {
                int IDPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
                m_dtDataPhieuChiTiet =
                       _NHAPKHO.LaythongtinChitietPhieunhapKho(IDPhieu);
                Utility.SetDataSourceForDataGridEx_Basic(grdPhieuNhapChiTiet, m_dtDataPhieuChiTiet, true, true, "1=1", "");
            }
            else
            {
                grdPhieuNhapChiTiet.DataSource = null;
            }
            ModifyCommand();
        }
        /// <summary>
        /// hàm thực hiện viêc in phiếu nhập kho thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuNhapKho_Click(object sender, EventArgs e)
        {
            try
            {
                int IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
                TPhieuNhapxuatthuoc objPhieuNhap = TPhieuNhapxuatthuoc.FetchByID(IdPhieu);
                if (objPhieuNhap != null)
                {
                    VNS.HIS.UI.Baocao.thuoc_phieuin_nhapxuat.InphieuNhapkho(IdPhieu, "PHIẾU NHẬP", globalVariables.SysDate);
                }
            }
            catch(Exception ex)
            {
                Utility.ShowMsg("Lỗi khi in phiếu nhập kho:\n" + ex.Message);
            }
          

        }
        /// <summary>
        /// HÀM THỰC HIỆN VIỆC XÁC NHẬN NHẬP KHO THUỐC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNhapKho_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.WaitNow(this);
                if (!Utility.Coquyen("thuoc_phieunhapkho_xacnhan"))
                {
                    Utility.ShowMsg("Bạn không có quyền xác nhận phiếu(thuoc_phieunhapkho_xacnhan). Vui lòng liên hệ quản trị hệ thống để được cấp quyền");
                    return;
                }
                cmdNhapKho.Enabled = false;
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "", false);
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_HIENTHI_NGAYXACNHAN", "0", false) == "0")
                    if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn xác nhận phiếu nhập kho đang chọn hay không?\nSau khi xác nhận, thuốc sẽ được cộng vào trong kho nhập", "Thông báo", true))
                    {
                        return;
                    }

                int ITPhieuNhapxuatthuoc = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
                TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc = TPhieuNhapxuatthuoc.FetchByID(ITPhieuNhapxuatthuoc);
                if (objTPhieuNhapxuatthuoc != null)
                {
                    if (Utility.ByteDbnull(objTPhieuNhapxuatthuoc.TrangThai, 0) == 1)
                    {
                        return;
                    }
                    //if (Utility.sDbnull(objTPhieuNhapxuatthuoc.NguoiTao) != globalVariables.UserName && THU_VIEN_CHUNG.Laygiatrithamsohethong_off("THUOC_PHANQUYEN_USER", "1", false) == "1")
                    //{
                    //    Utility.ShowMsg(
                    //        string.Format(
                    //            "Phiếu số {0} được tạo bởi tài khoản {1}. \n Xin liên hệ với người dùng {1} để sửa hoặc xóa, nhập phiếu",
                    //            objTPhieuNhapxuatthuoc.IdPhieu, objTPhieuNhapxuatthuoc.NguoiTao));
                    //    return;
                    //}
                    DateTime _ngayxacnhan = globalVariables.SysDate;
                    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_HIENTHI_NGAYXACNHAN", "0", false) == "1")
                    {
                        frm_ChonngayXacnhan _ChonngayXacnhan = new frm_ChonngayXacnhan();
                        _ChonngayXacnhan.pdt_InputDate = objTPhieuNhapxuatthuoc.NgayHoadon;
                        _ChonngayXacnhan.ShowDialog();
                        if (_ChonngayXacnhan.b_Cancel)
                            return;
                        else
                            _ngayxacnhan = _ChonngayXacnhan.pdt_InputDate;
                    }
                    ActionResult actionResult =
                        _NHAPKHO.XacNhanPhieuNhapKho(objTPhieuNhapxuatthuoc, _ngayxacnhan);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.ShowMsg("Bạn thực hiện xác nhận phiếu nhập kho thành công. Nhấn OK để kết thúc");
                            Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Xác nhận phiếu nhập kho thành công", false);
                            grdList.CurrentRow.BeginEdit();
                            grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.TrangThai].Value = 1;
                            grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.NgayXacnhan].Value = _ngayxacnhan;
                            grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.NguoiXacnhan].Value = globalVariables.UserName;
                            grdList.CurrentRow.EndEdit();
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi khi xác nhận phiếu nhập kho thuốc", "Thông báo lỗi", MessageBoxIcon.Error);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                Utility.DefaultNow(this);
                ModifyCommand();
            }

        }
        private void ModifyCommand()
        {
            cmdThemPhieuNhap.Enabled = true;
            bool isvalidGrid = Utility.isValidGrid(grdList);
            cmdInPhieuNhapKho.Enabled = isvalidGrid;
            int Trang_thai = 0;
            if (isvalidGrid) 
                Trang_thai= Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.TrangThai), 0);
            cmdXoaPhieuNhap.Enabled = isvalidGrid && Trang_thai == 0;
            cmdUpdatePhieuNhap.Enabled = cmdXoaPhieuNhap.Enabled;
            cmdNhapKho.Enabled = isvalidGrid && Trang_thai == 0;
            cmdHuyXacnhan.Enabled = isvalidGrid && !cmdNhapKho.Enabled;

        }
        /// <summary>
        /// hàm thực hiện việc cho phép lọc thông tin trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdList_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }

        private void cmdHuyXacnhan_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.WaitNow(this);
                if (!Utility.Coquyen("thuoc_phieunhapkho_huyxacnhan"))
                {
                    Utility.ShowMsg("Bạn không có quyền hủy xác nhận phiếu(thuoc_phieunhapkho_huyxacnhan). Vui lòng liên hệ quản trị hệ thống để được cấp quyền");
                    return;
                }
                cmdHuyXacnhan.Enabled = false;
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "", false);
                if (Utility.AcceptQuestion("Bạn có muốn hủy xác nhận phiếu nhập kho đang chọn hay không?\nSau khi hủy, thuốc sẽ được trừ ra khỏi kho nhập", "Thông báo", true))
                {
                    int ITPhieuNhapxuatthuoc = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
                    string khonhap = Utility.sDbnull(grdList.CurrentRow.Cells["ten_khonhap"].Value, "");
                    TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc = TPhieuNhapxuatthuoc.FetchByID(ITPhieuNhapxuatthuoc);
                    if (objTPhieuNhapxuatthuoc != null)
                    {
                        if (Utility.ByteDbnull(objTPhieuNhapxuatthuoc.TrangThai, 0) == 0)
                        {
                            return;
                        }
                        //if (!globalVariables.IsAdmin)
                        //{
                        //    if ( Utility.sDbnull(objTPhieuNhapxuatthuoc.NguoiXacnhan) != globalVariables.UserName)// || THU_VIEN_CHUNG.Laygiatrithamsohethong_off("THUOC_PHANQUYEN_USER", "1", false) == "1")
                        //    {
                        //        Utility.ShowMsg(
                        //            string.Format(
                        //                "Phiếu số {0} được tạo bởi tài khoản {1}. \n Xin liên hệ với người dùng {1} để sửa hoặc xóa, nhập phiếu",
                        //                objTPhieuNhapxuatthuoc.IdPhieu, objTPhieuNhapxuatthuoc.NguoiTao));
                        //        return;
                        //    }
                        //}
                        string errMsg = "";
                        ActionResult actionResult =
                            _NHAPKHO.HuyXacNhanPhieuNhapKho(objTPhieuNhapxuatthuoc, ref errMsg);
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn thực hiện hủy phiếu nhập kho thành công", false);
                                Utility.ShowMsg("Bạn thực hiện hủy nhập kho thành công. Nhấn OK để kết thúc");
                                grdList.CurrentRow.BeginEdit();
                                grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.TrangThai].Value = 0;
                                grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.NgayXacnhan].Value = DBNull.Value;
                                grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.NguoiXacnhan].Value = DBNull.Value;
                                grdList.CurrentRow.EndEdit();
                                Utility.Log(this.Name, globalVariables.UserName,
                                     string.Format(
                                         "Hủy phiếu nhập kho với số phiếu là :{0} - Tại kho {1}",
                                         ITPhieuNhapxuatthuoc, khonhap), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                                break;
                            case ActionResult.DataUsed:
                                Utility.ShowMsg(string.Format("Thuốc trong phiếu nhập kho đã được sử dụng nên bạn không thể xóa\n{0}", errMsg), "Thông báo lỗi", MessageBoxIcon.Error);
                                break;
                            case ActionResult.Exceed:
                                Utility.ShowMsg("Dữ liệu trong bảng thuốc kho không chứa thuốc này(Có thể do người khác vào xóa trực tiêp trong CSDL)", "Thông báo lỗi", MessageBoxIcon.Error);
                                break;
                            case ActionResult.NotEnoughDrugInStock:
                                Utility.ShowMsg(string.Format("Thuốc trong phiếu nhập kho đã được sử dụng và hiện không còn đủ để trừ hủy. Vui lòng kiểm tra lại\n{0}", errMsg), "Thông báo lỗi", MessageBoxIcon.Error);
                                break;
                            case ActionResult.Error:
                                break;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                Utility.DefaultNow(this);
                ModifyCommand();
            }
            
        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Properties frm = new frm_Properties( PropertyLib._NhapkhoProperties);
                frm.ShowDialog();
                CauHinh();

            }
            catch (Exception exception)
            {

            }
        }

        private void radTatCa_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radDaNhap_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radChuaNhapKho_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dtToDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtFromdate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void mnuUpdateGia_Click(object sender, EventArgs e)
        {
            try
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_PHIEUNHAPXUAT_CAPNHATGIABAN", "0", true) == "0")
                {
                    Utility.ShowMsg("Tính năng này tạm dừng hỗ trợ. Vui lòng rà soát kĩ giá thuốc khi làm phiếu nhập");
                    return;
                }
                if (grdPhieuNhapChiTiet.GetCheckedRows().Length <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất 1 thuốc cần cập nhật lại giá bán");
                    return;
                }
                bool Exists=true;
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn cập nhật giá bán cho các thuốc đang chọn?"), "Xác nhận cập nhật giá", true))
                {
                    foreach (GridEXRow _row in grdPhieuNhapChiTiet.GetCheckedRows())
                    {

                        long id_phieu = Utility.Int64Dbnull(_row.Cells["id_phieu"].Value, -1);
                        long id_phieuchitiet = Utility.Int64Dbnull(_row.Cells["id_phieuchitiet"].Value, -1);
                        long id_thuoc = Utility.Int64Dbnull(_row.Cells["id_thuoc"].Value, -1);
                        long id_thuockho = Utility.Int64Dbnull(_row.Cells["id_thuockho"].Value, -1);
                        decimal gia_ban = Utility.Int64Dbnull(_row.Cells["gia_ban"].Value, -1);
                        decimal gia_bhyt = Utility.Int64Dbnull(_row.Cells["gia_bhyt"].Value, -1);
                        if (id_thuockho <= 0)
                        {
                            Utility.ShowMsg("Hệ thống phát hiện phiếu này chưa xác nhận nên có thể sửa lại phiếu.\nTính năng cập nhật giá bán chỉ hỗ trợ tạm thời cho các phiếu nhập từ nhà cung cấp sai giá bán và đã xác nhận phiếu, đã chuyển thuốc đi các kho khác và kê cho khách hàng.\nNhắc lại: Với các phiếu chưa xác nhận đề nghị bấm vào nút sửa để thực hiện sửa lại giá bán");
                            return;
                        }
                        //SPs.ThuocPhieunhapkhoCapnhatgia(id_thuoc, id_thuockho, id_phieu, id_phieuchitiet, gia_ban, gia_bhyt).Execute();
                        //Vòng lặp đệ qui tìm Id chuyển của id_thuoc kho cập nhật tiếp
                        UpdateGiachoIdThuockho(id_thuockho, gia_ban);
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật giá thuốc trong phiếu nhập với id thuốc:{0}, id_thuockho={1}, id_phieu={2}, id_phieuchitiet={3}, giá bán={4}", id_thuoc, id_thuockho, id_phieu, id_phieuchitiet, gia_ban), newaction.Update, this.GetType().Assembly.ManifestModule.Name);


                    }
                }
                Utility.ShowMsg("Cập nhật giá thuốc thành công");
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);  
            }
        }
        void UpdateGiachoIdThuockho(long id_thuockho, decimal gia_ban)
        {
            SPs.ThuocPhieunhapkhoCapnhatgia(-1, id_thuockho, -1, -1, gia_ban, gia_ban).Execute();
            DataTable dtIdChuyen = SPs.ThuocPhieunhapkhoCapnhatgiaTimIdChuyen(-1, id_thuockho).GetDataSet().Tables[0];
            if (dtIdChuyen.Rows.Count > 0)
            {
                foreach (DataRow dr in dtIdChuyen.Rows)
                {
                    long id_tk = Utility.Int64Dbnull(dr["id_thuockho"], -1);
                    UpdateGiachoIdThuockho(id_tk, gia_ban);
                }
            }
        }
        

        private void cmdAddDetail_Click(object sender, EventArgs e)
        {
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_PHIEUNHAPXUAT_CAPNHATGIABAN", "0", true) == "0")
            {
                Utility.ShowMsg("Tính năng này tạm dừng hỗ trợ. Vui lòng rà soát kĩ giá thuốc khi làm phiếu nhập");
                return;
            }
            if (Utility.DecimaltoDbnull(txtGiaban.Text, 0) <= 0)
            {
                if (!Utility.AcceptQuestion("Cảnh báo giá bán đang <=0. Nhấn Yes để tiếp tục thực hiện, nhấn No để quay trở lại nhập giá bán", "Cảnh báo giá đơn thuốc<=0", true))
                {
                    txtGiaban.Focus();
                    return ;
                }
            }
                DataRow _row = ((DataRowView)grdPhieuNhapChiTiet.CurrentRow.DataRow).Row;
                _row["GIA_BAN"] = Utility.DecimaltoDbnull(txtGiaban.Text, 0);
            
        }

        private void cmdView_Click(object sender, EventArgs e)
        {
            try
            {
               
                int ITPhieuNhapxuatthuoc = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
                using (frm_Themmoi_Phieunhapkho frm = new frm_Themmoi_Phieunhapkho())
                {
                    frm._OnActionSuccess += frm__OnActionSuccess;
                    frm.m_enAction = action.View;
                    frm.KIEU_THUOC_VT = KIEU_THUOC_VT;
                    frm.grdList = grdList;
                    frm.p_mDataPhieuNhapKho = m_dtDataNhapKho;
                    frm.txtIDPhieuNhapKho.Text = Utility.sDbnull(ITPhieuNhapxuatthuoc);

                    frm.ShowDialog();
                    if (!frm.b_Cancel)
                    {
                        grdList_SelectionChanged(grdList, new EventArgs());
                    }
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }

            }
            finally
            {
                ModifyCommand();
            }
        }

        private void mnuXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (!InValiUpdateXoa()) return;
                long id_phieu  = Utility.Int64Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
                string id_chitiet=string.Join(",", (from p in grdPhieuNhapChiTiet.GetCheckedRows() select p.Cells[TPhieuNhapxuatthuocChitiet.Columns.IdPhieuchitiet].Value.ToString()).ToArray<string>());
                SPs.ThuocXoachitietphieu(id_phieu, id_chitiet).Execute();
                grdList_SelectionChanged(grdList, e);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
    }
}
