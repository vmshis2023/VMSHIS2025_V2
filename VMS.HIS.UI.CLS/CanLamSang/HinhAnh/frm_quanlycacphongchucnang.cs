using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using Janus.Windows.UI.Tab;
using Microsoft.VisualBasic;
using NLog;
using SubSonic;
using VNS.HIS.UI.DANHMUC;
using VNS.Libs;
using System.Linq;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using System.Collections.Generic;
using WPF.UCs;
using VNS.Properties;
using VNS.UCs;
using Aspose.Words;
using System.Diagnostics;
using System.Drawing.Printing;
using Aspose.Words.Tables;
using Aspose.Words.Drawing;
using VNS.HIS.UI.HinhAnh;
using System.Runtime.InteropServices;
using System.Transactions;
using System.Threading;
using VMS.QMS;
using VNS.Libs.AppUI;
using VNS.HIS.UI.NGOAITRU;


namespace VNS.HIS.UI.Forms.HinhAnh
{
    //0=Mới chỉ định;1=Đã chuyển CLS;2=Đang thực hiện;3= Đã có kết quả CLS;4=Đã xác nhận kết quả
    public partial class frm_quanlycacphongchucnang : Form
    {
        #region "biến thực hiện việc xử lý ảnh"
        private  string _baseDirectory = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "Radio\\");
        private readonly string m_strMaDichvu = "SA";
        //private  UNCAccessWithCredentials unc = new UNCAccessWithCredentials();
        private short ObjectType_ID = -1;
        private string _rowFilter = "1=1";
        private bool b_PathImage1;
        private bool b_PathImage2;
        private Logger log;
        private DataTable m_dKcbChidinhclsChitiet = new DataTable();
        private DataTable m_dtDataForm = new DataTable();
        private DataTable m_dtFormListBookmark = new DataTable();
        private DataTable m_dtFormServiceDetail = new DataTable();
        KcbChidinhclsChitiet objKcbChidinhclsChitiet = null;
        /// <summary>
        ///     hàm thuc chiện
        /// </summary>
        /// <param name="dataTable"></param>
        private DataTable m_dtReportHinhanh = new DataTable();

        private DataTable m_dtRoleUser = new DataTable();

        private KcbLuotkham objPatientExam;
        private KcbDanhsachBenhnhan objPatientInfo;
        private string sPathServer = new KCB_HinhAnh().GetImageServerPath();
        public string sPatient_Code = "";
        private int v_id_chitietchidinh = -1;
        private int v_FormRadio_Id = -1;
        public int v_Patient_ID = -1;

        #endregion

        private readonly string sTitleReport = "";

        #region "Khởi tạo form thực hiện "

        public FTPclient FtpClient;
        public string _FtpClientCurrentDirectory;

        public string mabaocao = "SA";
        public string docChuan = "SA";
        /// <summary>
        ///     khởi tạo dữ liệu
        /// </summary>
        /// <param name="ServiceCode"></param>
        public frm_quanlycacphongchucnang(string ServiceCode)
        {
            InitializeComponent();
            cmdScanFinger.Visible = true;
            Utility.SetVisualStyle(this);
            if (PropertyLib._HinhAnhProperties == null) PropertyLib._HinhAnhProperties = PropertyLib.GetHinhAnhProperties();
            log = LogManager.GetCurrentClassLogger();
            sTitleReport = ServiceCode;
            dtmFrom.Value = globalVariables.SysDate;
            dtmTo.Value = dtmFrom.Value;
            cboPatientSex.SelectedIndex = 0;
            m_strMaDichvu = ServiceCode;
            cmdConfig.Visible=globalVariables.IsAdmin;
            cmdConfig.Click+=cmdConfig_Click;
            grdVungKs.MouseDoubleClick += grdVungKs_MouseDoubleClick;
            grdVungKs.UpdatingCell += grdVungKs_UpdatingCell;
            grdList.SelectionChanged += grdList_SelectionChanged;
            optNgoaiNoitru.CheckedChanged += radChoXacNhan_CheckedChanged;
            optNgoaitru.CheckedChanged += radChoXacNhan_CheckedChanged;
            optNoitru.CheckedChanged += radChoXacNhan_CheckedChanged;
            txtMaluotkham_tk.KeyDown += txtMaluotkham_tk_KeyDown;
            txtMaphieuCD.KeyDown += txtMaphieuCD_KeyDown;
            InitFtp();
        }
        bool tkmalankham = false;
        bool tkmachidinh = false;
        void txtMaphieuCD_KeyDown(object sender, KeyEventArgs e)
        {
            tkmachidinh = true;
            SearchData();
        }

        void txtMaluotkham_tk_KeyDown(object sender, KeyEventArgs e)
        {
            tkmalankham = true;
            txtMaluotkham_tk.Text = Utility.AutoFullPatientCode(txtMaluotkham_tk.Text);
            SearchData();
        }

      

        void grdVungKs_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                DmucVungkhaosat objvks = DmucVungkhaosat.FetchByID(Utility.Int32Dbnull(grdVungKs.GetValue(DmucVungkhaosat.Columns.Id)));
                if (e.Column.Key == "ten_vungkhaosat")
                {
                    objvks.TenVungkhaosat = e.Value.ToString();
                }
                else if (e.Column.Key == "tenfile_KQ")
                {
                    objvks.TenfileKq = e.Value.ToString();
                }
                else if (e.Column.Key == "Kichthuocanh")
                {
                    objvks.Kichthuocanh = e.Value.ToString();
                }
                else if (e.Column.Key == "ma_loaidvu")
                {
                    objvks.Kichthuocanh = e.Value.ToString();
                }
                objvks.MarkOld();
                objvks.IsNew = false;
                objvks.Save();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }


        
        void grdList_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdList)) return;
            grdVungKs.UnCheckAllRecords();
            string vungks=Utility.DoTrim(Utility.sDbnull(grdList.CurrentRow.Cells["dsach_vungkhaosat"].Value, ""));
            if (vungks != "")
            {
                foreach (GridEXRow item in grdVungKs.GetDataRows())
                {
                    if (("," + vungks + ",").Contains("," + item.Cells[DmucVungkhaosat.Columns.Id].Value.ToString() + ","))
                    {

                        item.BeginEdit();
                        item.IsChecked = true;
                        item.EndEdit();
                    }
                }
            }
        }

      
       
      
        void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._HinhAnhProperties);
            _Properties.ShowDialog();
            InitFtp();
        }

        private void InitFtp()
        {
            try
            {
               
                FtpClient = new FTPclient(PropertyLib._HinhAnhProperties.FTPServer, PropertyLib._HinhAnhProperties.UID, PropertyLib._HinhAnhProperties.PWD);
                FtpClient.UsePassive = true;
                _FtpClientCurrentDirectory = FtpClient.CurrentDirectory;
                _baseDirectory = Utility.DoTrim(PropertyLib._HinhAnhProperties.ImageFolder);
                if (_baseDirectory.EndsWith(@"\")) _baseDirectory = _baseDirectory.Substring(0, _baseDirectory.Length - 1);
                if (!Directory.Exists(_baseDirectory))
                {
                    Directory.CreateDirectory(_baseDirectory);
                }
            }
            catch
            {
            }
        }

        #endregion

        #region "hàm thực hiện sự kiện của form"

        private DataTable m_dtServiceDetail = new DataTable();

        /// <summary>
        ///     tìm kiếm thông tin tìm kiếm thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            SearchData();
            // KcbChidinhclsChitiet.Columns.g
        }

        /// <summary>
        ///     tìm kiếm nhanh thông tin của form
        /// </summary>
        private void SearchData()
        {
            try
            {
                objKcbChidinhclsChitiet = null;
                string maphieu_cd = Utility.DoTrim(txtMaphieuCD.Text);
                string ma_luotkham = Utility.DoTrim(txtMaluotkham_tk.Text);
                if (ma_luotkham == "") ma_luotkham = "ALL";
                string ten_benhnhan = Utility.DoTrim(txtTenbenhnhan_tk.Text);
                if (ten_benhnhan == "") ten_benhnhan = "ALL";
                byte trangthai_xacnhan = (byte)255;//Lấy tất cả sau đó mới lọc HinhAnhTimkiemBnhan
                DateTime fromdate = dtmFrom.Value;
                DateTime todate = dtmTo.Value;
                int gioitinh=Utility.Int32Dbnull(cboPatientSex.SelectedValue, -1);
                int doituong=Utility.Int32Dbnull(cboObjectType.SelectedValue, -1);
                if(!chkByDate.Checked )
                {
                    fromdate=new DateTime(1900,1,1);
                    todate=new DateTime(1900,1,1);
                }
                if (tkmachidinh)
                {
                    fromdate = new DateTime(1900, 1, 1);
                    todate = new DateTime(1900, 1, 1);
                    ma_luotkham = "ALL";
                    ten_benhnhan = "ALL";
                    gioitinh = -1;
                    doituong = -1;
                }
                if (tkmalankham)
                {
                    fromdate = new DateTime(1900, 1, 1);
                    todate = new DateTime(1900, 1, 1);
                    maphieu_cd = "ALL";
                    ten_benhnhan = "ALL";
                    gioitinh = -1;
                    doituong = -1;
                }
                m_dKcbChidinhclsChitiet =
                    SPs.HinhanhTimkiembnhan(maphieu_cd, ma_luotkham, ten_benhnhan, trangthai_xacnhan
                    , fromdate, todate, doituong, gioitinh, m_strMaDichvu.Split('@')[0]).GetDataSet().Tables[0];
                THU_VIEN_CHUNG.CreateXML(m_dKcbChidinhclsChitiet, "HINHANH.xml");
                SetFilter();
                tkmachidinh = false;
                tkmalankham = false;
                if (grdList.RowCount > 0)
                {
                    Utility.SetMsg(lblMsg, "Mời bạn tiếp tục thực hiện công việc", false);
                    grdList.MoveFirst();
                }
                else
                {
                    Utility.SetMsg(lblMsg, "Không có dữ liệu theo điều kiện bạn chọn", true);
                }
                if (grdList.RowCount > 0)
                {
                    grdList.MoveFirst();
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
        
        private void chkBydate_CheckedChanged(object sender, EventArgs e)
        {
            dtmFrom.Enabled = chkByDate.Checked;
            dtmTo.Enabled = chkByDate.Checked;
        }

      
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frm_quanlycacphongchucnang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if (e.KeyCode == Keys.F3) cmdSearch.PerformClick();
            if (e.KeyCode == Keys.F5) cmdSearch.PerformClick();
            if (e.Control && e.KeyCode == Keys.B) BeginExam();
            if (e.KeyCode == Keys.F11 && PropertyLib._AppProperties.ShowActiveControl) Utility.ShowMsg(this.ActiveControl.Name);
        }
        /// <summary>
        ///     hàm thực hiện việc hiên thị các nút thông tin của trạng thía
        /// </summary>
        private void ModifyButtonCommand()
        {
            try
            {
                toolChooseBN.Enabled = Utility.isValidGrid(grdList);
                toolPrintRadio.Enabled = Utility.isValidGrid(grdList) && Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangThai].Value)>=3;

                toolAccept.Enabled = Utility.isValidGrid(grdList) && Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangThai].Value) ==3 ;
                toolUnAccept.Enabled =  Utility.isValidGrid(grdList) && Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangThai].Value) == 4 ;
                toolUnAccept.Visible = globalVariables.IsAdmin;
            }
            catch (Exception)
            {
            }
        }
        bool AutoGoiLoa_Theomay = false;
        int numberofDisplay = 5;
        /// <summary>
        ///     thực hienj việc load thông tin của Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_quanlycacphongchucnang_Load(object sender, EventArgs e)
        {
            layout1 = THU_VIEN_CHUNG.Laygiatrithamsohethong("QMSPK_LAYOUT1", "STT,Họ và tên,Giới tính,Năm sinh@194, 701, 260, 309", true);
            layout0 = THU_VIEN_CHUNG.Laygiatrithamsohethong("QMSPK_LAYOUT0", "STT,Họ và tên,Giới tính,Năm sinh@194, 701, 260, 309", true);
            numberofDisplay = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("QMSCN_SOLUONGCHO", "5", true), 5);
            AutoGoiLoa_Theomay = THU_VIEN_CHUNG.Laygiatrithamsohethong("QMS_TUDONGOILOA_THEOMAY", "0", true) == "1";
            AutoGoiLoa_WhenNext = THU_VIEN_CHUNG.Laygiatrithamsohethong("QMS_TUDONGOILOA_KHINEXT", "0", true) == "1";
            InitQMS();
            InitData();
            log = LogManager.GetCurrentClassLogger();
            cmdSearch.PerformClick();
        }

      
        private void grdList_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyButtonCommand();
        }

        private void TabInfo_TabIndexChanged(object sender, EventArgs e)
        {
            ModifyButtonCommand();
        }

        private void TabInfo_ChangingSelectedTab(object sender, TabCancelEventArgs e)
        {
            ModifyButtonCommand();
        }

        private void TabInfo_SelectedTabChanged(object sender, TabEventArgs e)
        {
            ModifyButtonCommand();
        }
        /// <summary>
        ///     hàm thực hiện chọn thông tin bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdList_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (!Utility.isValidGrid(grdList)) return;
            if (e.Column.Key == "colChooseBN")
            {
                BeginExam();
            }
            else
            {
              
            }

        }
        KcbChidinhclsChitiet objChitiet = null;
        private void BeginExam()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    if (grdList.CurrentRow != null && grdList.CurrentRow.RowType == RowType.Record)
                    {

                        v_id_chitietchidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdList, "id_chitietchidinh"), -1);
                        objChitiet = KcbChidinhclsChitiet.FetchByID(v_id_chitietchidinh);
                        if (objChitiet == null) {
                            Utility.ShowMsg("Không lấy được dữ liệu ca chụp đang chọn. Có thể đã bị xóa trong lúc bạn đang mở chức năng và chưa thực hiện. Vui lòng nhấn lại nút tìm kiếm để làm mới lại dữ liệu danh sách các ca chụp.");
                            return;
                        }
                        if (Utility.ByteDbnull( objChitiet.TrangthaiHuy,0) > 0)
                        {
                            Utility.ShowMsg("Dịch vụ bạn chọn đã hủy hoặc trả lại tiền nên không thể thao tác nhập kết quả. Vui lòng kiểm tra lại");
                            return;
                        }
                        if (Utility.ByteDbnull(objChitiet.ChanGuiCls, 0) > 0)
                        {
                            Utility.ShowMsg("Dịch vụ bạn chọn đã được chặn không cho thực hiện cận lâm sàng nên bạn không thể thao tác nhập kết quả. Vui lòng kiểm tra lại");
                            return;
                        }
                        KcbLuotkham objluotkham = Utility.getKcbLuotkham(objChitiet.IdBenhnhan, objChitiet.MaLuotkham);
                        KcbChidinhcl objChidinh=KcbChidinhcl.FetchByID(objChitiet.IdChidinh);
                        if (objluotkham != null && objChidinh != null && objChidinh.Noitru == 0 && objluotkham.TrangthaiCapcuu == 0 && objChitiet.TrangThai<=0)
                        {
                            //Lấy tiền tạm ứng ngoại trú
                            decimal tstamung = noitru_TamungHoanung.LaySoTienTamUng(objluotkham.MaLuotkham, Utility.Int64Dbnull(objluotkham.IdBenhnhan), 0);
                            if (tstamung <= 0)//Nếu ko có tạm ứng thì chỉ thanh toán mới được phép thực hiện
                            {
                                if (objChitiet.TrangthaiThanhtoan <= 0)
                                {
                                    Utility.ShowMsg("Dịch vụ bạn chọn thuộc ngoại trú và chưa được thanh toán nên không thể thực hiện nhập trả kết quả");
                                    return;
                                }
                            }
                            else
                            {
                                //Kiểm tra tiền tạm ứng có > tiền dịch vụ hay không
                                decimal TongChiPhi = KCB_CHIDINH_CANLAMSANG.LayTongSoTienChuaThanhToan(objluotkham.MaLuotkham, objluotkham.IdBenhnhan, Utility.Int32Dbnull(objluotkham.Noitru));
                                if (tstamung - TongChiPhi < 0)
                                {
                                    string sTU = String.Format(Utility.FormatDecimal(), tstamung);
                                    string sTCP = String.Format(Utility.FormatDecimal(), TongChiPhi);
                                    string sChenhlech = String.Format(Utility.FormatDecimal(), tstamung - TongChiPhi);
                                    Utility.ShowMsg(string.Format("Tổng tạm ứng: {0} đồng\nTổng chi phí={1} đồng\nTổng chênh lệch=Tổng tạm ứng - Tổng chi phí={2} đồng\n Người bệnh cần nộp thêm tiền tạm ứng ít nhất {3} đồng trước khi thực hiện các dịch vụ", sTU, sTCP, sChenhlech, sChenhlech));
                                    return;
                                }
                            }

                        }

                        
                        if (Utility.DoTrim(Utility.sDbnull(grdList.CurrentRow.Cells["dsach_vungkhaosat"].Value, "")) == "")
                        {
                            Utility.ShowMsg("Dịch vụ này chưa gán vùng khảo sát nên không thể nhập kết quả.\nNhấn OK để thực hiện gán vùng khảo sát cho dịch vụ đang chọn");
                            frm_chonvungksat _chonvungks = new frm_chonvungksat(new List<string>());
                            _chonvungks.Hthi_Chon = true;
                            _chonvungks.ten_dvu = Utility.GetValueFromGridColumn(grdList, "ten_chitietdichvu");
                            if (_chonvungks.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                if (Utility.DoTrim(_chonvungks.vungks).Length > 0)
                                {
                                    grdList.CurrentRow.BeginEdit();
                                    grdList.CurrentRow.Cells["dsach_vungkhaosat"].Value = _chonvungks.vungks;
                                    grdList.CurrentRow.EndEdit();
                                    grdList.UpdateData();
                                    grdList.Refetch();
                                    DmucDichvuclsChitiet objDvu = DmucDichvuclsChitiet.FetchByID(Utility.GetValueFromGridColumn(grdList, "id_chitietdichvu"));
                                    if (objDvu != null)
                                    {
                                        objDvu.DsachVungkhaosat = _chonvungks.vungks;
                                        objDvu.Save();
                                        m_dKcbChidinhclsChitiet.AsEnumerable().Where(c => c.Field<Int32>("id_chitietdichvu") == Utility.Int64Dbnull(Utility.GetValueFromGridColumn(grdList, "id_chitietdichvu"))).ToList().ForEach(x1 => { x1["dsach_vungkhaosat"] = _chonvungks.vungks; });
                                    }
                                }
                            }
                            else
                            {
                                Utility.ShowMsg("Bạn vừa hủy chọn vùng khảo sát nên không thể nhập kết quả");
                                return;
                            }

                        }
                        DataRowView dr = grdList.CurrentRow.DataRow as DataRowView;
                        Utility.SetMsg(lblMsg, "Đang nạp thông tin bệnh nhân...", false);
                        dr["trang_thai"] = objChitiet.TrangThai;
                        dr["ten_trangthai"] = Utility.LaythongtinTrangthaiCLS(Utility.ByteDbnull(objChitiet.TrangThai, 0));
                        FtpClient.CurrentDirectory = _FtpClientCurrentDirectory;
                        int Status = Utility.Int32Dbnull(grdList.CurrentRow.Cells["trang_thai"].Value, -1);
                        if (Status <= 2)
                        {
                            new KCB_HinhAnh().UpdateXacNhanDaThucHien(v_id_chitietchidinh, 2);
                            dr["ten_trangthai"] = Utility.LaythongtinTrangthaiCLS(2);
                        }
                        int id_VungKS = Utility.Int32Dbnull(grdList.CurrentRow.Cells["id_VungKS"].Value, -1);
                        if (id_VungKS > 0)
                        {
                            DmucVungkhaosat objVks = DmucVungkhaosat.FetchByID(id_VungKS);
                            if (objVks != null)
                                goto _EnterResult;
                            else
                            {
                                Utility.ShowMsg(string.Format("Dịch vụ {0} chưa gắn vùng khảo sát hoặc vùng khảo sát được dùng gần nhất cho dịch vụ CLS đã bị xóa. Vui lòng kiểm tra và gắn lại vùng khảo sát cho dịch vụ {1}", Utility.GetValueFromGridColumn(grdList, "ten_chitietdichvu"), Utility.GetValueFromGridColumn(grdList, "ten_chitietdichvu")));
                                return;
                            }
                        }
                        List<string> lstID = new List<string>();
                        lstID = Utility.sDbnull(grdList.CurrentRow.Cells["dsach_vungkhaosat"].Value, "-1").Split(',').ToList<string>();
                        if (lstID.Count >= 2)
                        {
                            frm_chonvungksat _chonvungksat = new frm_chonvungksat(lstID);
                            if (_chonvungksat.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                id_VungKS = _chonvungksat.Id;
                            else
                                return;
                        }
                        else
                            if (lstID.Count > 0)
                                id_VungKS = Utility.Int32Dbnull(lstID[0], -1);
                    _EnterResult:
                        bool isV1 = THU_VIEN_CHUNG.Laygiatrithamsohethong("CAPTURE_VERSION", "0", true) == "1";
                    if (isV1  ||File.Exists(Application.StartupPath+@"\V1.txt"))
                    {
                        frm_NhaptraKQ_V1 frm = new frm_NhaptraKQ_V1();
                        DataTable dtWorkList = SPs.HinhanhTimkiembnhanTheoIDchidinhchitiet(v_id_chitietchidinh).GetDataSet().Tables[0];
                        frm.drWorklistDetail = dr.Row;
                        frm.drWorklist = dtWorkList.Rows[0];
                        if (frm.FtpClient == null) frm.FtpClient = this.FtpClient;
                        frm.ID_Study_Detail = v_id_chitietchidinh;
                        frm.lstID = Utility.sDbnull(grdList.CurrentRow.Cells["dsach_vungkhaosat"].Value, "-1").Split(',').ToList<string>();
                        frm.id_VungKS = id_VungKS;
                        frm.StrServiceCode = m_strMaDichvu;
                        frm.ShowDialog();
                        if (!frm.mv_blnCancel)
                        {
                            grdList_SelectionChanged(grdList, new EventArgs());
                        }
                        frm.Dispose();
                        frm = null;
                    }
                    else
                    {
                        frm_NhaptraKQ frm = new frm_NhaptraKQ();
                        
                        DataTable dtWorkList = SPs.HinhanhTimkiembnhanTheoIDchidinhchitiet(v_id_chitietchidinh).GetDataSet().Tables[0];
                        frm.drWorklistDetail = dr.Row;
                        frm.drWorklist = dtWorkList.Rows[0];
                        if (frm.FtpClient == null) frm.FtpClient = this.FtpClient;
                        frm.ID_Study_Detail = v_id_chitietchidinh;
                        frm.lstID = Utility.sDbnull(grdList.CurrentRow.Cells["dsach_vungkhaosat"].Value, "-1").Split(',').ToList<string>();
                        frm.id_VungKS = id_VungKS;
                        frm.StrServiceCode = m_strMaDichvu;
                        frm.ShowDialog();
                        if (!frm.mv_blnCancel)
                        {
                            grdList_SelectionChanged(grdList, new EventArgs());
                        }
                        frm.Dispose();
                        frm = null;
                    }

                       
                    }
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg("Lỗi:" + ex.Message);
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
            finally
            {
                ModifyButtonCommand();
                Utility.SetMsg(lblMsg, "Mời bạn tiếp tục làm việc...", false);
                this.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        ///     hàm thực hiện việc load thông tin của form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdGetPatient_Click(object sender, EventArgs e)
        {
            TabInfo.SelectedTab = TabInfo.TabPages[1];
        }
        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
        }
        private void toolAccept_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList)) return;
                
                string ten_dichvu = grdList.GetValue("ten_chitietdichvu").ToString();
                string id_benhnhan = grdList.GetValue("id_benhnhan").ToString();
                string ma_luotkham = grdList.GetValue("ma_luotkham").ToString();
                string ten_benhnhan = grdList.GetValue("ten_benhnhan").ToString();
                if (!Utility.Coquyen("cdha_duyet_ketqua"))
                {
                    Utility.ShowMsg("Bạn không có quyền duyệt kết quả CĐHA(cdha_duyet_ketqua). Vui lòng liên hệ quản trị hệ thống để được cấp quyền");
                    return;
                }
                v_id_chitietchidinh = Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
                KcbChidinhclsChitiet objKcbChidinhclsChitiet = KcbChidinhclsChitiet.FetchByID(v_id_chitietchidinh);
                
                if (objKcbChidinhclsChitiet != null && objKcbChidinhclsChitiet.TrangThai == 3)
                {
                    if (Utility.ByteDbnull(objKcbChidinhclsChitiet.TrangthaiHuy, 0) > 0)
                    {
                        Utility.ShowMsg("Dịch vụ bạn chọn đã hủy hoặc trả lại tiền nên không thể thao tác duyệt. Vui lòng kiểm tra lại");
                        return;
                    }
                    if (Utility.AcceptQuestion("Bạn có chắc chắn muốn duyệt kết quả này?\nSau khi duyệt, kết quả sẽ được gửi về các khoa phòng khác và các bác sĩ có thể xem kết quả.", "", true))
                    {

                        ActionResult actionResult =
                            new KCB_HinhAnh().UpdateXacNhanDaThucHien(
                                v_id_chitietchidinh, 4);
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                Utility.Log(this.Name, globalVariables.UserName, string.Format("Duyệt kết quả cho bệnh nhân ID={0}, PID={1}, Tên={2}, Dịch vụ duyệt ={3} ", id_benhnhan, ma_luotkham, ten_benhnhan, ten_dichvu), newaction.ConfirmData, this.GetType().Assembly.ManifestModule.Name);
                                Utility.ShowMsg("Đã duyệt kết quả thành công. Nhấn OK để kết thúc");

                                grdList.CurrentRow.BeginEdit();
                                grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangThai].Value = 4;
                                grdList.CurrentRow.Cells["ten_trangthai"].Value = GetAsssignDetailStatus(4);
                                grdList.CurrentRow.EndEdit();
                                grdList.UpdateData();
                                grdList.Refresh();
                                m_dKcbChidinhclsChitiet.AcceptChanges();
                                break;
                            case ActionResult.Error:
                                Utility.ShowMsg("Có lỗi trong quá trình xác nhận", "Thông báo", MessageBoxIcon.Error);
                                break;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        
            
        /// <summary>
        ///     hàm thực hiện viêc hủy bỏ kết quả
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolUnAccept_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdList)) return;
            string ten_dichvu = grdList.GetValue("ten_chitietdichvu").ToString();
            string id_benhnhan = grdList.GetValue("id_benhnhan").ToString();
            string ma_luotkham = grdList.GetValue("ma_luotkham").ToString();
            string ten_benhnhan = grdList.GetValue("ten_benhnhan").ToString();
            if (!Utility.Coquyen("cdha_huyduyet_ketqua"))
            {
                Utility.ShowMsg("Bạn không có quyền hủy duyệt kết quả CĐHA(cdha_huyduyet_ketqua). Vui lòng liên hệ quản trị hệ thống để được cấp quyền");
                return;
            }
            
            v_id_chitietchidinh = Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
            KcbChidinhclsChitiet objKcbChidinhclsChitiet = KcbChidinhclsChitiet.FetchByID(v_id_chitietchidinh);
            
            if (objKcbChidinhclsChitiet != null && objKcbChidinhclsChitiet.TrangThai == 4)
            {
                if (Utility.ByteDbnull(objKcbChidinhclsChitiet.TrangthaiHuy, 0) > 0)
                {
                    Utility.ShowMsg("Dịch vụ bạn chọn đã hủy hoặc trả lại tiền nên không thể thao tác hủy duyệt. Vui lòng kiểm tra lại");
                    return;
                }
                //    if (globalVariables.UserName == "ADMIN" || Utility.sDbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NguoiThuchien].Value, "") == globalVariables.UserName)
                //    {

                ActionResult actionResult =
                    new KCB_HinhAnh().UpdateXacNhanDaThucHien(
                        v_id_chitietchidinh, 3);//Trạng thái đang nhập kết quả

                switch (actionResult)
                {
                    case ActionResult.Success:
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy duyệt kết quả cho bệnh nhân ID={0}, PID={1}, Tên={2}, Dịch vụ hủy duyệt ={3} ", id_benhnhan, ma_luotkham, ten_benhnhan, ten_dichvu), newaction.CancelData, this.GetType().Assembly.ManifestModule.Name);
                            Utility.ShowMsg("Đã HỦY duyệt kết quả thành công. Nhấn OK để kết thúc");
                        grdList.CurrentRow.BeginEdit();
                        grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangThai].Value = 3;
                        grdList.CurrentRow.Cells["ten_trangthai"].Value = GetAsssignDetailStatus(3);
                        grdList.CurrentRow.EndEdit();
                        grdList.UpdateData();
                        grdList.Refresh();
                        m_dKcbChidinhclsChitiet.AcceptChanges();
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Có lỗi trong quá trình xác nhận", "Thông báo", MessageBoxIcon.Error);
                        break;
                }

                //}
                //else
                //{
                //    Utility.ShowMsg("Kết quả này được xác nhận bởi bác sĩ khác nên bạn không được phép hủy hoặc thay đổi. Muốn thay đổi bạn cần đăng nhập là Admin hoặc liên hệ bác sĩ xác nhận kết quả này");
                //    return;
                //}
            }

        }

        /// <summary>
        ///     nhan chuột phải thực hiện việc xử lý thông tin của phần chuẩn đoán đưa bệnh nhân vào chẩn đoán
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolChooseBN_Click(object sender, EventArgs e)
        {
            // if (!InValiRadio()) return;
            BeginExam();
        }

        #endregion

      

        #region "Khu vực xử lý thông tin ảnh"

        private void InitData()
        {
            try
            {
                GetVungKS();
                DataBinding.BindDataCombobox(this.cboObjectType, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(), DmucDoituongkcb.Columns.IdDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "---Đối tượng KCB---",true);
            }
            catch
            {
            }
        }

      
        private string GetAsssignDetailStatus(int AssginDetail_Status)
        {
            string AssginDetailStatus_Name = "Chưa thực hiện";
            switch (AssginDetail_Status)
            {
                case 0:
                    AssginDetailStatus_Name = "Chưa thực hiện";
                    break;
                case 1:
                    AssginDetailStatus_Name = "Đã chuyển CLS";
                    break;
                case 2:
                    AssginDetailStatus_Name = "Đang thực hiện";
                    break;
                case 3:
                    AssginDetailStatus_Name = "Đã có kết quả";
                    break;
                case 4:
                    AssginDetailStatus_Name = "Đã xác nhận";
                    break;
            }
            return AssginDetailStatus_Name;
        }

        #endregion

     
     

        private void chkHasHinhAnh_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._HinhAnhProperties.IamLocal = PropertyLib._HinhAnhProperties.IamLocal;
            PropertyLib.SaveProperty(PropertyLib._HinhAnhProperties);
        }


        void SetFilter()
        {
            string filter = "";
            if (optChuacoKQ.Checked)
                if (filter.Length <= 0)
                    filter = " trang_thai<=1";
                else
                    filter += " AND trang_thai<=1";
            else if (optCoKQ.Checked)
                if (filter.Length <= 0)
                    filter = " trang_thai>=3";
                else
                    filter += " AND trang_thai>=3";
            if (optNoitru.Checked)
                if (filter.Length <= 0)
                    filter += " noitru_ngoaitru=1";
                else
                    filter += " AND noitru_ngoaitru=1";
            else if (optNgoaitru.Checked)
                if (filter.Length <= 0)
                    filter += " noitru_ngoaitru=0";
                else
                    filter += " AND noitru_ngoaitru=0";
            if (filter.Length <= 0) filter = "1=1";
            Utility.SetDataSourceForDataGridEx_Basic(grdList, m_dKcbChidinhclsChitiet, true, true, filter, "ngay_chidinh desc,ten_benhnhan asc");
        }
        private void radChuaXacNhan_CheckedChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void radChoXacNhan_CheckedChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void radDaXacNhan_CheckedChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        /// <summary>
        /// ham thực heienj việc cọn 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdList_DoubleClick(object sender, EventArgs e)
        {
           if(!Utility.isValidGrid(grdList)) return;
                BeginExam();
        }
        private DataTable _dtVungKS;
        private void GetVungKS()
        {
            try
            {
                //if (this.m_strMaDichvu != "ALL")
                //{
                //    //System.Collections.Generic.List<Int16> lstIdDvu = new Select(DmucDichvucl.Columns.IdDichvu).From(DmucDichvucl.Schema).Where(DmucDichvucl.Columns.IdLoaidichvu).In(m_strMaDichvu.Split(',').ToList<string>()).ExecuteTypedList<Int16>();
                //    _dtVungKS = new Select().From(DmucVungkhaosat.Schema).Where(DmucVungkhaosat.Columns.MaLoaidvu).In(m_strMaDichvu.Split(',').ToList<string>()).ExecuteDataSet().Tables[0];
                //}
                //else
                
                _dtVungKS = SPs.ClsCdhaGetvungks(globalVariables.IsAdmin?"ALL": this.m_strMaDichvu).GetDataSet().Tables[0];// new Select().From(DmucVungkhaosat.Schema).ExecuteDataSet().Tables[0];
                grdVungKs.DataSource = _dtVungKS;
            }
            catch (Exception ex)
            {
            }
        }
        private void cmdThemMoi_Click(object sender, EventArgs e)
        {
            var obj = new DmucVungkhaosat { NgayTao = DateTime.Now };
            var f = new frm_themmoi_vungkhaosat(this.m_strMaDichvu) { m_enAct = action.Insert, Table = _dtVungKS, Obj = obj };
            f.ShowDialog();
        }

        private void cmdSua_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdVungKs)) return;
            var obj = new DmucVungkhaosat(grdVungKs.GetValue(DmucVungkhaosat.Columns.Id));
            var f = new frm_themmoi_vungkhaosat(this.m_strMaDichvu) { m_enAct = action.Update, Obj = obj, Table = _dtVungKS };
            f.ShowDialog();
        }

        private void cmdXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa các vùng khảo sát đang được chọn", "Xác nhận xóa vùng khảo sát", true))
                {
                    if (grdVungKs.GetCheckedRows().Count() > 0)
                    {
                        foreach (GridEXRow row in grdVungKs.GetCheckedRows())
                        {
                            var keyId = (int)row.Cells[DmucVungkhaosat.Columns.Id].Value;
                            DataRow dr = (from datarow in _dtVungKS.AsEnumerable()
                                          where datarow.Field<int>(DmucVungkhaosat.Columns.Id) == keyId
                                          select datarow).First();

                            DmucVungkhaosat.Delete(keyId);
                            if (dr != null) _dtVungKS.Rows.Remove(dr);
                        }
                    }
                    else
                    {
                        if (grdVungKs.CurrentRow == null) return;
                        if (!grdVungKs.CurrentRow.RowType.Equals(RowType.Record)) return;
                        var keyId = (int)grdVungKs.GetValue(DmucVungkhaosat.Columns.Id);
                        DataRow row = (from datarow in _dtVungKS.AsEnumerable()
                                       where datarow.Field<int>(DmucVungkhaosat.Columns.Id) == keyId
                                       select datarow).First();
                        DmucVungkhaosat.Delete(keyId);
                        if (row != null) _dtVungKS.Rows.Remove(row);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnLayDuLieu_Click(object sender, EventArgs e)
        {
            GetVungKS();
        }
       
        void grdVungKs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Utility.isValidGrid(grdVungKs))
                cmdSua.PerformClick();
        }

        private void cmdGanVungKS_Click(object sender, EventArgs e)
        {
            try
            {
                string idvungks = "";
                DmucDichvuclsChitiet objDvu = DmucDichvuclsChitiet.FetchByID(Utility.GetValueFromGridColumn(grdList, "id_chitietdichvu"));
                if (objDvu != null)
                {
                    if (grdVungKs.GetCheckedRows().Count() > 0)
                    {
                       
                        var query = (from chk in grdVungKs.GetCheckedRows()
                                     let x = Utility.sDbnull(chk.Cells[DmucVungkhaosat.Columns.Id].Value)
                                     select x).ToArray();
                        if (query != null && query.Count() > 0)
                        {
                            idvungks = string.Join(",", query);
                        }
                    }
                    else
                    {

                        idvungks = Utility.GetValueFromGridColumn(grdList, DmucVungkhaosat.Columns.Id);
                    }
                    objDvu.DsachVungkhaosat = idvungks;
                    objDvu.Save();
                    foreach (DataRow dr in m_dKcbChidinhclsChitiet.Rows)
                        if (Utility.Int32Dbnull(dr["id_chitietdichvu"], -1) == objDvu.IdChitietdichvu)
                            dr["dsach_vungkhaosat"] = idvungks;
                    m_dKcbChidinhclsChitiet.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void mnuCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList)) return;
                
                long idChidinhchitiet = Utility.Int64Dbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
                KcbChidinhclsChitiet objChitiet = KcbChidinhclsChitiet.FetchByID(idChidinhchitiet);
                if (Utility.ByteDbnull( objChitiet.TrangthaiHuy,0) > 0)
                {
                    Utility.ShowMsg("Dịch vụ bạn chọn đã hủy hoặc trả lại tiền nên không thể thao tác hủy kết quả. Vui lòng kiểm tra lại");
                    return;
                }
                string ten_dichvu = grdList.GetValue("ten_chitietdichvu").ToString();
                string id_benhnhan = grdList.GetValue("id_benhnhan").ToString();
                string ma_luotkham = grdList.GetValue("ma_luotkham").ToString();
                string ten_benhnhan = grdList.GetValue("ten_benhnhan").ToString();
                if (objChitiet.TrangThai <= 2)
                {
                    Utility.ShowMsg(string.Format("Dịch vụ {0} chưa có kết quả nên không thể hủy. Vui lòng kiểm tra lại", ten_dichvu));
                    return;
                }
                if (objChitiet.TrangThai > 3)
                {
                    Utility.ShowMsg(string.Format("Dịch vụ {0} đã duyệt kết quả nên muốn hủy kết quả thì bạn phải thực hiện hủy duyệt kết quả trước. Vui lòng kiểm tra lại", ten_dichvu));
                    return;
                }
                if (!Utility.Coquyen("cdha_xoa_ketqua"))
                {
                    Utility.ShowMsg("Bạn không có quyền hủy kết quả CĐHA(cdha_xoa_ketqua). Vui lòng liên hệ quản trị hệ thống để được cấp quyền");
                    return;
                }
                if (objChitiet != null)
                {
                    if (!globalVariables.IsAdmin)
                    {
                        if (Utility.sDbnull(objChitiet.NguoiThuchien).Length > 0)
                        {
                            if (globalVariables.UserName != objChitiet.NguoiThuchien)
                            {
                                Utility.ShowMsg(string.Format("Kết quả CĐHA của dịch vụ đang chọn được thực hiện bởi {0}. Bạn không có quyền hủy kết quả của người khác. Vui lòng liên hệ {1} hoặc Admin của hệ thống để thực hiện việc hủy", objChitiet.NguoiThuchien, objChitiet.NguoiThuchien));
                                return;
                            }
                            int KCB_CDHA_SONGAY_HUYKETQUA =
                                         Utility.Int32Dbnull(
                                             THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_CDHA_SONGAY_HUYKETQUA", "0", true), 0);
                            var chenhlech =
                                (int)Math.Ceiling((globalVariables.SysDate.Date - objChitiet.NgayThuchien.Value.Date).TotalDays);
                            if (chenhlech > KCB_CDHA_SONGAY_HUYKETQUA)
                            {
                                Utility.ShowMsg(string.Format("Ngày thực hiện: {0}. Hệ thống không cho phép bạn hủy kết quả CĐHA khi đã quá {1} ngày. Cần liên hệ quản trị hệ thống để được trợ giúp", objChitiet.NgayThuchien.Value.Date.ToString("dd/MM/yyyy"), KCB_CDHA_SONGAY_HUYKETQUA.ToString()));
                                return;
                            }
                        }
                    }

                    if (Utility.AcceptQuestion("Bạn có chắc chắn muốn hủy KQ CĐHA hay không?", "Xác nhận hủy", true))
                    {

                        objChitiet.MarkOld();
                        objChitiet.TrangThai = 0;
                        objChitiet.KetLuanCdha = "";
                        objChitiet.KetQua = "";
                        objChitiet.NgayThuchien = null;
                        objChitiet.NguoiThuchien = "";
                        objChitiet.IdVungks = "-1";
                        using (var scope = new TransactionScope())
                        {
                            using (var sh = new SharedDbConnectionScope())
                            {
                                objChitiet.Save();
                                SPs.ClsCdhaDelete(objChitiet.IdChitietchidinh).Execute();
                            }
                            scope.Complete();
                            
                        }
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy kết quả cho bệnh nhân ID={0}, PID={1}, Tên={2}, Dịch vụ hủy KQ ={3} ", id_benhnhan, ma_luotkham, ten_benhnhan, ten_dichvu), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                        Utility.ShowMsg("Đã hủy kết quả CĐHA thành công. Nhấn OK để kết thúc");
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdDynamicFields_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Utility.isValidGrid(grdList))
                //{
                //    Utility.ShowMsg("Bạn cần chọn một dịch vụ liên quan đến hình ảnh(XQuang,Siêu Âm, Nội soi...) để thực hiện cấu hình");
                //    return;
                //}
                int idvungks = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdVungKs, DmucVungkhaosat.Columns.Id), -1);

                DmucVungkhaosat objvks = DmucVungkhaosat.FetchByID(idvungks);
                try
                {
                    if (objvks == null)
                    {
                        Utility.ShowMsg("Bạn cần chọn chỉ định chi tiết cần cập nhật kết quả");
                        return;
                    }
                    frm_DynamicSetup _DynamicSetup = new frm_DynamicSetup();
                    _DynamicSetup.objvks = objvks;
                    _DynamicSetup.ImageID = -1;
                    _DynamicSetup.Id_chidinhchitiet = -1;
                    if (_DynamicSetup.ShowDialog() == DialogResult.OK)
                    {

                    }
                }
                catch (Exception)
                {

                }
            }
            catch (Exception)
            {
            }
        }
        private void cmdCopy_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdVungKs)) return;
            var obj = new DmucVungkhaosat(grdVungKs.GetValue(DmucVungkhaosat.Columns.Id));
            var f = new frm_themmoi_vungkhaosat("ALL") { m_enAct = action.Duplicate, Obj = obj, Table = _dtVungKS };
            f.ShowDialog();
        }

        private void lnkDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtMaphieuCD.Clear();
            txtMaluotkham_tk.Clear();
            txtTenbenhnhan_tk.Clear();
            cboPatientSex.SelectedIndex = 0;
            optAllKQ.Checked = true;
            optNgoaiNoitru.Checked = true;
            dtmFrom.Value = globalVariables.SysDate;
            dtmTo.Value = globalVariables.SysDate;
            txtMaphieuCD.Focus();
        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void cmdScanFinger_Click(object sender, EventArgs e)
        {
            RegisterFinger();
        }
        internal static IntPtr hWnd;
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern void SendMessageW(IntPtr hWnd, uint msg, uint wParam, uint lParam);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowW(string className, string windowName);
        internal static Process process;

        void RegisterFinger()
        {
            try
            {
                string patientID = "-1";
                //if (Utility.Int32Dbnull(patientID, -1) > 0)
                //{
                    List<string> _list = new List<string>();
                    _list.Add(patientID.ToString());
                    _list.Add(0.ToString());
                    string sPatientInforFile = Application.StartupPath + @"\IVF_FR\PatientInfor.txt";
                    string appName = Application.StartupPath + @"\IVF_FR\IVF_FingerPrint.exe";
                    if (File.Exists(sPatientInforFile))
                    {
                        File.WriteAllLines(sPatientInforFile, _list.ToArray());
                    }
                    else
                    {
                        File.CreateText(sPatientInforFile);
                        File.WriteAllLines(sPatientInforFile, _list.ToArray());
                    }
                    Utility.KillProcess(appName);
                    Thread.Sleep(100);
                    process = Process.Start(Application.StartupPath + @"\IVF_FR\IVF_FingerPrint.exe");
                    if (process != null) process.WaitForExit();
                    WaitForSingleObject(process.Handle, 0xffffffff);
               // }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void toolPrintRadio_Click(object sender, EventArgs e)
        {

        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
             if (!isOpened)
                OpenQMS();
            else
                StopQMS();
        }
        #region QMS
        bool AutoGoiLoa_WhenNext = false;
        bool isOpened = false;
        bool isEnable = false;
        string ScreenType = "0";//"0","1","2": 3 kiểu màn hình khác nhau
        public QMSXQ frmXQCTMRI = null;//0
        public FrmShowScreen_Type1 frmSA_NS = null;//1
        public FrmShowScreen frmSA_NS_old = null;//2
        int ThoiGianTuDongLay = 5000;//Cấu hình bằng tham số hệ thống
        DataRow currentQMSRow = null;
        DataRow NextQMSRow = null;
        VMS.QMS.Class.QMSChucNang _qms = new VMS.QMS.Class.QMSChucNang();
        string layout1 = "STT,Họ và tên,Giới tính,Năm sinh@194, 701, 260, 309";
        string layout0 = "STT,Họ và tên,Giới tính,Năm sinh@194, 701, 260, 309";
        void StopQMS()
        {
            globalVariables.b_QMS_Stop = true;
            _qms._qmspro = PropertyLib._QMSPrintProperties;
            isOpened = false;
            txtSoQMS.Text = "0";
            txtTS.Text = "0";
            toolTip1.SetToolTip(cmdStart, isOpened ? "Tắt QMS" : "Bật QMS");
            cmdGoiloa.Enabled = cmdNext.Enabled = cmdRestore.Enabled = cmdIgnore.Enabled = isOpened;
            cmdStart.Image = isOpened ? global::VMS.HIS.Cls.Properties.Resources.Stop : global::VMS.HIS.Cls.Properties.Resources.Start;
            cmdStart.Text = isOpened ? "Tắt" : "Bật";
            try
            {
                DestroyQMS_XQCTMRI();
                DestroyQMSOld();
                DestroyQMSType1();
                if (isUsingQMS())
                {
                    _qms.QmsPK_CapnhatTrangthai(Utility.Int64Dbnull(currentQMSRow["id_kham"], -1), Utility.Int64Dbnull(currentQMSRow["id"], -1), 1,0);
                    Thread.Sleep(100);
                }
                currentQMSRow = null;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        void OpenQMS()
        {
            globalVariables.b_QMS_Stop = false;
            ScreenType = Utility.sDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("QMSPK_SCREENTYPE", "1", true), "1");
            if (ScreenType == "1")
            {
                if (!isOpened)//Chưa bật QMS
                {
                    DestroyQMS_XQCTMRI();
                    DestroyQMSType1();
                    ShowQMSOld();
                }
                else
                {
                    DestroyQMSOld();
                }
            }
            else if (ScreenType == "2")
            {
                if (!isOpened)
                {

                    DestroyQMSOld();
                    DestroyQMS_XQCTMRI();
                    ShowQMSType1();

                }
                else
                {
                    DestroyQMSType1();
                }
            }
            else //3
            {
                if (!isOpened)
                {

                    DestroyQMSOld();
                    DestroyQMSType1();
                    ShowQMSXQCTMRI();

                }
                else
                {
                    DestroyQMS_XQCTMRI();
                }
            }
            isOpened = true;
            cmdGoiloa.Enabled = cmdNext.Enabled = cmdRestore.Enabled = cmdIgnore.Enabled = isOpened;
            // mnuEnableQMS.Checked = !mnuEnableQMS.Checked;
            toolTip1.SetToolTip(cmdStart, isOpened ? "Tắt QMS" : "Bật QMS");
            cmdStart.Image = isOpened ? global::VMS.HIS.Cls.Properties.Resources.Stop : global::VMS.HIS.Cls.Properties.Resources.Start;
            cmdStart.Text = isOpened ? "Tắt" : "Bật";
        }
        bool isUsingQMS()
        {
            return isEnable && isOpened && currentQMSRow != null;
        }
        public void DestroyQMS_XQCTMRI()
        {

            lblQMS_Current.Text = "";
            lblQMS_Next.Text = "";
            if (frmXQCTMRI != null)
            {
                frmXQCTMRI._OnRefreshData -= _OnRefreshData;

                frmXQCTMRI._closeme = true;
                frmXQCTMRI.Close();
                frmXQCTMRI.Dispose();
                frmXQCTMRI = null;
            }
        }
        public void DestroyQMSType1()
        {
            lblQMS_Current.Text = "";
            lblQMS_Next.Text = "";
            if (frmSA_NS != null)
            {
                frmSA_NS._OnRefreshData -= _OnRefreshData;
                frmSA_NS._OnsaveLayout -= _OnsaveLayout;
                frmSA_NS._closeme = true;
                frmSA_NS.Close();
                frmSA_NS.Dispose();
                frmSA_NS = null;
            }
        }
        public void DestroyQMSOld()
        {
            lblQMS_Current.Text = "";
            lblQMS_Next.Text = "";
            if (frmSA_NS_old != null)
            {
                frmSA_NS_old._OnRefreshData -= _OnRefreshData;
                frmSA_NS_old._OnsaveLayout -= _OnsaveLayout;
                frmSA_NS_old._closeme = true;
                frmSA_NS_old.Close();
                frmSA_NS_old.Dispose();
                frmSA_NS_old = null;
            }
        }
        public void ShowQMSType1()
        {
            Screen[] sc;
            sc = Screen.AllScreens;
            IEnumerable<Screen> query = from mh in Screen.AllScreens
                                        select mh;
            //get all the screen width and heights
            if (frmSA_NS == null)
            {
                frmSA_NS = new FrmShowScreen_Type1(true, 1, numberofDisplay, layout1);
                frmSA_NS.ThoiGianTuDongLay = this.ThoiGianTuDongLay;
                frmSA_NS._OnRefreshData += _OnRefreshData;
                frmSA_NS._OnsaveLayout += _OnsaveLayout;
            }
            if (query.Count() >= 2)
            {

                if (!CheckOpened(frmSA_NS.Name))
                {
                    frmSA_NS.FormBorderStyle = FormBorderStyle.None;
                    frmSA_NS.Left = sc[1].Bounds.Width;
                    frmSA_NS.Top = sc[1].Bounds.Height;
                    frmSA_NS.StartPosition = FormStartPosition.CenterScreen;
                    frmSA_NS.Location = sc[1].Bounds.Location;
                    var p = new Point(sc[1].Bounds.Location.X, sc[1].Bounds.Location.Y);
                    frmSA_NS.Location = p;
                    frmSA_NS.WindowState = FormWindowState.Maximized;
                    frmSA_NS.Show();
                }
            }
            else
                if (!CheckOpened(frmSA_NS.Name))
                {
                    frmSA_NS.Show();
                }

        }
        public void ShowQMSOld()
        {
            Screen[] sc;
            sc = Screen.AllScreens;
            IEnumerable<Screen> query = from mh in Screen.AllScreens
                                        select mh;
            //get all the screen width and heights
            if (frmSA_NS_old == null)
            {
                frmSA_NS_old = new FrmShowScreen(true, 1, numberofDisplay, layout0);
                frmSA_NS_old.ThoiGianTuDongLay = this.ThoiGianTuDongLay;
                frmSA_NS_old._OnRefreshData += _OnRefreshData;
                frmSA_NS_old._OnsaveLayout += _OnsaveLayout;
            }
            if (query.Count() >= 2)
            {

                if (!CheckOpened(frmSA_NS_old.Name))
                {
                    frmSA_NS_old.FormBorderStyle = FormBorderStyle.None;
                    frmSA_NS_old.Left = sc[1].Bounds.Width;
                    frmSA_NS_old.Top = sc[1].Bounds.Height;
                    frmSA_NS_old.StartPosition = FormStartPosition.CenterScreen;
                    frmSA_NS_old.Location = sc[1].Bounds.Location;
                    var p = new Point(sc[1].Bounds.Location.X, sc[1].Bounds.Location.Y);
                    frmSA_NS_old.Location = p;
                    frmSA_NS_old.WindowState = FormWindowState.Maximized;
                    frmSA_NS_old.Show();
                }
            }
            else
                if (!CheckOpened(frmSA_NS_old.Name))
                {
                    frmSA_NS_old.Show();
                }

        }
        public void ShowQMSXQCTMRI()
        {
            Screen[] sc;
            sc = Screen.AllScreens;
            IEnumerable<Screen> query = from mh in Screen.AllScreens
                                        select mh;
            //get all the screen width and heights
            if (frmXQCTMRI == null)
            {
                frmXQCTMRI = new QMSXQ(true, globalVariables.SysLogo, 1);
                frmXQCTMRI.ThoiGianTuDongLay = this.ThoiGianTuDongLay;
                frmXQCTMRI._OnRefreshData += _OnRefreshData;
            }
            if (query.Count() >= 2)
            {

                if (!CheckOpened(frmXQCTMRI.Name))
                {
                    frmXQCTMRI.FormBorderStyle = FormBorderStyle.None;
                    frmXQCTMRI.Left = sc[1].Bounds.Width;
                    frmXQCTMRI.Top = sc[1].Bounds.Height;
                    frmXQCTMRI.StartPosition = FormStartPosition.CenterScreen;
                    frmXQCTMRI.Location = sc[1].Bounds.Location;
                    var p = new Point(sc[1].Bounds.Location.X, sc[1].Bounds.Location.Y);
                    frmXQCTMRI.Location = p;
                    frmXQCTMRI.WindowState = FormWindowState.Maximized;
                    frmXQCTMRI.Show();
                }
            }
            else
                if (!CheckOpened(frmXQCTMRI.Name))
                {
                    frmXQCTMRI.Show();
                }
        }
        void RefreshQMS()
        {
            try
            {
                if (frmXQCTMRI != null) frmXQCTMRI.RefreshQMS();
                if (frmSA_NS != null) frmSA_NS.RefreshQMS();
                if (frmSA_NS_old != null) frmSA_NS_old.RefreshQMS();
            }
            catch (Exception)
            {

            }
        }
        void _OnsaveLayout(string layout, int type)
        {
            try
            {
                string sName = type == 0 ? "QMSPK_LAYOUT0" : "QMSPK_LAYOUT1";
                SysSystemParameter p = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo(sName).ExecuteSingle<SysSystemParameter>();
                if (p == null)
                {
                    p = new SysSystemParameter();
                    p.SName = sName;
                    p.SValue = layout;
                    p.IsNew = true;
                    p.Save();
                }
                else
                {
                    p.SValue = layout;
                    p.IsNew = false;
                    p.MarkOld();
                    p.Save();
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void _OnRefreshData(string infor, string current, string next, DataRow currentQMSRow, DataRow NextQMSRow, int totalQMS)
        {
            lblQMS_Current.Text = currentQMSRow == null && NextQMSRow == null ? "HẾT SỐ" : current;
            lblQMS_Next.Text = next;
            this.NextQMSRow = NextQMSRow;
            if (currentQMSRow != null)
            {
                int STT = Utility.Int32Dbnull(currentQMSRow["So_Kham"], 0);
                string sTotal = "";
                string sCurrent = "";
                if (STT < 10)
                {
                    sCurrent = Utility.FormatNumberToString(STT, "00");
                }
                else
                {
                    sCurrent = Utility.sDbnull(STT);
                }


                if (totalQMS < 10)
                {
                    sTotal = Utility.FormatNumberToString(totalQMS, "00");
                }
                else
                {
                    sTotal = Utility.sDbnull(totalQMS);
                }
                UIAction.SetTextStatus(txtSoQMS, sCurrent, false);
                UIAction.SetTextStatus(txtTS, sTotal, false);
                Application.DoEvents();
            }
            this.currentQMSRow = currentQMSRow;
        }
        private bool CheckOpened(string name)
        {
            FormCollection fc = Application.OpenForms;

            return fc.Cast<Form>().Any(frm => frm.Text == name);
        }

        void Goiloa(DataRow drQMS)
        {
            try
            {
                if (drQMS != null)
                {
                    _qms._qmspro = PropertyLib._QMSPrintProperties;
                     List<string> lstSuspend = THU_VIEN_CHUNG.Laygiatrithamsohethong("QMS_SUSPEND", ":;:;;;;", false).Split(';').ToList<string>();
                    string so_kham = drQMS["So_Kham"].ToString();
                    string ten_benhnhan = drQMS["TEN_BENHNHAN"].ToString();
                     string tuoi = Utility.sDbnull(drQMS["tuoi"], "");
                    tuoi = tuoi.Length > 0 ? string.Format("{0} tuổi ", tuoi) : "";
                    _qms.InsertGoiLoa(so_kham, _qms._qmspro.MaPhongQMS,
                                          globalVariables.gv_strIPAddress, _qms._qmspro.MaKhoaQMS, 0, 1,
                                          globalVariables.UserName, globalVariables.SysDate, globalVariables.gv_strMacAddress,
                                          _qms._qmspro.MaLoaGoi,
                                            string.Format("{0} {1} {2} {3} {4} {5}", _qms._qmspro.Loimoi, lstSuspend[0], ten_benhnhan, tuoi, lstSuspend[1], _qms._qmspro.TenPhong));//Mời bệnh nhân A 30 tuổi vào phòng khám số 3
                                          //string.Format("{0} {1} {2} {3}", _qms._qmspro.Loimoi, so_kham, ten_benhnhan, _qms._qmspro.TenPhong));
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        void _Properties__OnRefreshData(object _property)
        {
            try
            {
                if (frmXQCTMRI != null)
                    frmXQCTMRI.CauHinh();
                if (frmSA_NS != null)
                    frmSA_NS.CauHinh();
                if (frmSA_NS_old != null)
                    frmSA_NS_old.CauHinh();
            }
            catch (Exception)
            {


            }
        }
        void InitQMS()
        {

            ThoiGianTuDongLay = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("QMSPCN_ThoigiantudongLayso", "5000", true), 5000);

            isEnable = Utility.sDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("QMSPCN_ENABLE", "0", true), "0") == "1";
            pnlQMS.Height = isEnable ? 57 : 0;
            grpTimKiem.Height = isEnable ? 203 : 144;
        }
        #endregion

        private void cmdNext_Click(object sender, EventArgs e)
        {
            cmdNext.Enabled = false;
            _qms._qmspro = PropertyLib._QMSPrintProperties;
            if (currentQMSRow != null)
                _qms.QmsPK_CapnhatTrangthai(Utility.Int64Dbnull(currentQMSRow["id_kham"], -1), Utility.Int64Dbnull(currentQMSRow["id"], -1), 3,1);
            if (AutoGoiLoa_Theomay)
            {
                if (_qms._qmspro.Tudonggoiloa_Next_Ignore) Goiloa(NextQMSRow);
            }
            else
                if (AutoGoiLoa_WhenNext)
                    Goiloa(NextQMSRow);
            RefreshQMS();
            cmdNext.Enabled = true;
        }

        private void cmdGoiloa_Click(object sender, EventArgs e)
        {
            Goiloa(currentQMSRow);
        }

        private void cmdIgnore_Click(object sender, EventArgs e)
        {
            _qms._qmspro = PropertyLib._QMSPrintProperties;
            cmdIgnore.Enabled = false;
            if (currentQMSRow != null && Utility.AcceptQuestion("Bạn có chắc chắn muốn bỏ qua bệnh nhân này để gọi bệnh nhân kế tiếp. Bạn có thể vào danh sách nhỡ(F10) để gọi lại bệnh nhân này", "Xác nhận bỏ nhỡ bệnh nhân", true))
                _qms.QmsPK_CapnhatTrangthai(Utility.Int64Dbnull(currentQMSRow["id_kham"], -1), Utility.Int64Dbnull(currentQMSRow["id"], -1), 0, 1);
            else
            {
                cmdIgnore.Enabled = true;
                return;
            }
            //if ( _qms._qmspro.Tudonggoiloa_Next_Ignore) Goiloa(NextQMSRow);
            if (AutoGoiLoa_Theomay)
            {
                if (_qms._qmspro.Tudonggoiloa_Next_Ignore) Goiloa(NextQMSRow);
            }
            else
                if (AutoGoiLoa_WhenNext)
                    Goiloa(NextQMSRow);
            RefreshQMS();
            cmdIgnore.Enabled = true;
        }

        private void cmdRestore_Click(object sender, EventArgs e)
        {
            ctxQMSFunction.Show(cmdRestore, new Point(0, cmdRestore.Height));
        }

        private void mnuQMSConfig_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._QMSPrintProperties);
            _Properties._OnRefreshData += _Properties__OnRefreshData;
            _Properties.ShowDialog();
        }

        private void mnuQmsColor_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._QMSColorProperties);
            _Properties._OnRefreshData += _Properties__OnRefreshData;
            _Properties.ShowDialog();
        }

        private void mnuCallbyQMS_Click(object sender, EventArgs e)
        {

        }

        private void mnuQmsMan_Click(object sender, EventArgs e)
        {
            _qms._qmspro = PropertyLib._QMSPrintProperties;
            _qms.ShowListData(true, 0);
        }
        KcbDanhsachBenhnhan objBenhnhan = null;
        KcbLuotkham objLuotkham = null;
        private void mnuKeVTTH_Click(object sender, EventArgs e)
        {
            try
            {
                v_id_chitietchidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdList, "id_chitietchidinh"), -1);
                objChitiet = KcbChidinhclsChitiet.FetchByID(v_id_chitietchidinh);
                if (objChitiet == null)
                {
                    Utility.ShowMsg("Không lấy được dữ liệu ca chụp đang chọn. Có thể đã bị xóa trong lúc bạn đang mở chức năng và chưa thực hiện. Vui lòng nhấn lại nút tìm kiếm để làm mới lại dữ liệu danh sách các ca chụp.");
                    return;
                }
                if (Utility.ByteDbnull(objChitiet.TrangthaiHuy, 0) > 0)
                {
                    Utility.ShowMsg("Dịch vụ bạn chọn đã hủy hoặc trả lại tiền nên không thể thao tác nhập kết quả. Vui lòng kiểm tra lại");
                    return;
                }
                if (Utility.ByteDbnull(objChitiet.ChanGuiCls, 0) > 0)
                {
                    Utility.ShowMsg("Dịch vụ bạn chọn đã được chặn không cho thực hiện cận lâm sàng nên bạn không thể thao tác nhập kết quả. Vui lòng kiểm tra lại");
                    return;
                }
                KcbLuotkham objluotkham = Utility.getKcbLuotkham(objChitiet.IdBenhnhan, objChitiet.MaLuotkham);
                KcbChidinhcl objChidinh = KcbChidinhcl.FetchByID(objChitiet.IdChidinh);
                if (objluotkham != null && objChidinh != null && objChidinh.Noitru == 0 && objluotkham.TrangthaiCapcuu == 0 && objChitiet.TrangThai <= 0)
                {
                    //Lấy tiền tạm ứng ngoại trú
                    decimal tstamung = noitru_TamungHoanung.LaySoTienTamUng(objluotkham.MaLuotkham, Utility.Int64Dbnull(objluotkham.IdBenhnhan), 0);
                    if (tstamung <= 0)//Nếu ko có tạm ứng thì chỉ thanh toán mới được phép thực hiện
                    {
                        if (objChitiet.TrangthaiThanhtoan <= 0)
                        {
                            Utility.ShowMsg("Dịch vụ bạn chọn thuộc ngoại trú và chưa được thanh toán nên không thể thực hiện nhập trả kết quả");
                            return;
                        }
                    }
                    else
                    {
                        //Kiểm tra tiền tạm ứng có > tiền dịch vụ hay không
                        decimal TongChiPhi = KCB_CHIDINH_CANLAMSANG.LayTongSoTienChuaThanhToan(objluotkham.MaLuotkham, objluotkham.IdBenhnhan, Utility.Int32Dbnull(objluotkham.Noitru));
                        if (tstamung - TongChiPhi < 0)
                        {
                            string sTU = String.Format(Utility.FormatDecimal(), tstamung);
                            string sTCP = String.Format(Utility.FormatDecimal(), TongChiPhi);
                            string sChenhlech = String.Format(Utility.FormatDecimal(), tstamung - TongChiPhi);
                            Utility.ShowMsg(string.Format("Tổng tạm ứng: {0} đồng\nTổng chi phí={1} đồng\nTổng chênh lệch=Tổng tạm ứng - Tổng chi phí={2} đồng\n Người bệnh cần nộp thêm tiền tạm ứng ít nhất {3} đồng trước khi thực hiện các dịch vụ", sTU, sTCP, sChenhlech, sChenhlech));
                            return;
                        }
                    }
                }
                objBenhnhan = KcbDanhsachBenhnhan.FetchByID(objChitiet.IdBenhnhan);
                objLuotkham = KcbLuotkham.FetchByID(objChitiet.MaLuotkham);
                //Kiểm tra xem có đơn VTTH theo chỉ định đang chọn hay chưa
                KcbDonthuoc objdonthuoc = new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.Columns.KieuDonthuoc).IsEqualTo(4).And(KcbDonthuoc.Columns.IdChitietchidinh).IsEqualTo(objChitiet.IdChitietchidinh).ExecuteSingle<KcbDonthuoc>();
                if (objdonthuoc != null)
                {
                    if (!IsValid_UpdateDonthuoc(objdonthuoc.IdDonthuoc, "thuốc"))
                    {
                        return;
                    }
                    if (Utility.Coquyen("quyen_suadonthuoc") || objdonthuoc.NguoiTao == globalVariables.UserName)
                    {
                        CapnhatDonVTTH(objChitiet, objdonthuoc.IdDonthuoc);
                    }
                    else
                    {
                        Utility.ShowMsg("Đơn thuốc đang chọn sửa được tạo bởi bác sĩ khác hoặc bạn không được gán quyền sửa(quyen_suadonthuoc). Vui lòng kiểm tra lại");
                        return;
                    }
                }
                else
                {
                    ThemMoiDonVTTH(objChitiet);
                }
                
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
            finally
            {
                //cmdCreateNewPres.Enabled = true;
            }
        }
        void CapnhatDonVTTH(KcbChidinhclsChitiet objChitiet,long id_donthuoc)
        {
            var frm = new frm_KCB_KE_DONTHUOC("VT");
            frm.em_Action = action.Update;
            frm._KcbCDKL = null;
            frm._MabenhChinh = "";
            frm.id_chitietchidinh = objChitiet.IdChitietchidinh;
            frm.id_chitietdichvu = objChitiet.IdChitietdichvu;
            frm.ten_dichvu = grdList.GetValue("ten_chitietdichvu").ToString();
            frm._Chandoan = "";
            frm.DtIcd = null;
            frm.dt_ICD_PHU = null;
            frm.noitru = 0;
            frm.objLuotkham = objLuotkham;
            frm.id_kham = -1;
            frm.objCongkham = null;
            frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
            frm.txtPatientID.Text = Utility.sDbnull(objBenhnhan.IdBenhnhan, "-1");
            frm.txtSoDT.Text = Utility.sDbnull(objBenhnhan.DienThoai, "");
            frm.txtPatientName.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan, "");
            frm.txtYearBirth.Text = Utility.sDbnull(objBenhnhan.NamSinh, "");
            frm.txtSex.Text = Utility.sDbnull(objBenhnhan.GioiTinh, "");
            frm.txtPres_ID.Text = Utility.sDbnull(id_donthuoc);
            frm.dtNgayKhamLai.MinDate = globalVariables.SysDate;
            frm._ngayhenkhamlai = "";

            frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
            frm.ShowDialog();
            frm.Dispose();
            frm = null;
            GC.Collect();
        }
        private bool IsValid_UpdateDonthuoc(long id_donthuoc, string thuoc_vt)
        {
            TPhieuCapphatChitiet _capphat = new Select().From(TPhieuCapphatChitiet.Schema).Where(TPhieuCapphatChitiet.Columns.IdDonthuoc).IsEqualTo(id_donthuoc)
                .ExecuteSingle<TPhieuCapphatChitiet>();
            if (_capphat != null)
            {
                Utility.ShowMsg("Đơn " + thuoc_vt + " đã được tổng hợp lĩnh " + thuoc_vt + " hao phí khoa phòng chức năng nên bạn không được phép sửa. Đề nghị kiểm tra lại");
                return false;
            }
            KcbDonthuoc _item =
                new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.IdDonthuocColumn).IsEqualTo(id_donthuoc)
                .And(KcbDonthuoc.TrangThaiColumn).IsEqualTo(1).ExecuteSingle<KcbDonthuoc>();
            if (_item != null)
            {
                Utility.ShowMsg("Đơn " + thuoc_vt + " này đang ở trạng thái đã duyệt cho Bệnh nhân nên không thể chỉnh sửa. Đề nghị kiểm tra lại");
                return false;
            }
            
            return true;
        }
        
        private void ThemMoiDonVTTH(KcbChidinhclsChitiet objChitiet)
        {
            try
            {
               
                // KeDonThuocTheoDoiTuong();
                frm_KCB_KE_DONTHUOC frm = new frm_KCB_KE_DONTHUOC("VT");
                frm.em_Action = action.Insert;
                frm.KieuDonthuoc = 4;
                frm.objLuotkham = objLuotkham;
                frm._KcbCDKL = null;
                frm._MabenhChinh = "";
                frm.id_chitietchidinh = objChitiet.IdChitietchidinh;
                frm.id_chitietdichvu = objChitiet.IdChitietdichvu;
                frm.ten_dichvu = grdList.GetValue("ten_chitietdichvu").ToString();
                frm._Chandoan = "";
                frm.DtIcd = null;
                frm.dt_ICD_PHU = null;
                frm.id_kham = -1;
                frm.objCongkham = null;
                frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan, "-1");
                frm.txtSoDT.Text = objBenhnhan.DienThoai;
                frm.txtPatientName.Text = objBenhnhan.TenBenhnhan;
                frm.txtYearBirth.Text = Utility.sDbnull(objBenhnhan.NamSinh, "");
                frm.txtSex.Text = Utility.sDbnull(objBenhnhan.GioiTinh, "");
                frm.txtPres_ID.Text = "-1";
                frm.dtNgayKhamLai.MinDate = DateTime.Now;
                frm._ngayhenkhamlai = "";
                frm.noitru = 0;
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();
                frm.Dispose();
                frm = null;
                GC.Collect();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {

            }
        }

        private void mnuHuytiepnhan_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList)) return;

                long idChidinhchitiet = Utility.Int64Dbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
                KcbChidinhclsChitiet objChitiet = KcbChidinhclsChitiet.FetchByID(idChidinhchitiet);
                if (Utility.ByteDbnull(objChitiet.TrangthaiHuy, 0) > 0)
                {
                    Utility.ShowMsg("Dịch vụ bạn chọn đã hủy hoặc trả lại tiền nên không thể thao tác hủy tiếp nhận. Vui lòng kiểm tra lại");
                    return;
                }
                string ten_dichvu = grdList.GetValue("ten_chitietdichvu").ToString();
                string id_benhnhan = grdList.GetValue("id_benhnhan").ToString();
                string ma_luotkham = grdList.GetValue("ma_luotkham").ToString();
                string ten_benhnhan = grdList.GetValue("ten_benhnhan").ToString();
                if (objChitiet.TrangThai !=2)
                {
                    Utility.ShowMsg(string.Format("Dịch vụ {0} không ở trạng thái đang thực hiện(2) nên không thể hủy tiếp nhận. Vui lòng kiểm tra lại", ten_dichvu));
                    return;
                }
                if (objChitiet.TrangThai > 2)
                {
                    Utility.ShowMsg(string.Format("Dịch vụ {0} đã có kết quả nên muốn hủy tiếp nhận thì bạn phải thực hiện hủy duyệt kết quả/Hủy kết quả trước. Vui lòng kiểm tra lại", ten_dichvu));
                    return;
                }
                //if (!Utility.Coquyen("cdha_xoa_ketqua"))
                //{
                //    Utility.ShowMsg("Bạn không có quyền hủy kết quả CĐHA(cdha_xoa_ketqua). Vui lòng liên hệ quản trị hệ thống để được cấp quyền");
                //    return;
                //}
                if (objChitiet != null)
                {
                    if (!globalVariables.IsAdmin)
                    {
                        if (Utility.sDbnull(objChitiet.NguoiThuchien).Length > 0)
                        {
                            if (globalVariables.UserName != objChitiet.NguoiThuchien)
                            {
                                Utility.ShowMsg(string.Format("Kết quả CĐHA của dịch vụ đang chọn được thực hiện bởi {0}. Bạn không có quyền hủy tiếp nhận ca của người khác. Vui lòng liên hệ {1} hoặc Admin của hệ thống để thực hiện việc hủy", objChitiet.NguoiThuchien, objChitiet.NguoiThuchien));
                                return;
                            }
                        }
                    }

                    if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn hủy tiếp nhận ca {0} hay không?", ten_dichvu), "Xác nhận hủy", true))
                    {

                        objChitiet.MarkOld();
                        objChitiet.TrangThai = 0;
                        objChitiet.KetLuanCdha = "";
                        objChitiet.ThoigianBatdau = null;
                        objChitiet.KetQua = "";
                        objChitiet.NgayThuchien = null;
                        objChitiet.NguoiThuchien = "";
                        objChitiet.IdVungks = "-1";
                        using (var scope = new TransactionScope())
                        {
                            using (var sh = new SharedDbConnectionScope())
                            {
                                objChitiet.Save();
                                SPs.ClsCdhaDelete(objChitiet.IdChitietchidinh).Execute();
                            }
                            scope.Complete();

                        }
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy tiếp nhận CĐHA cho bệnh nhân ID={0}, PID={1}, Tên={2}, Dịch vụ hủy tiếp nhận ={3} ", id_benhnhan, ma_luotkham, ten_benhnhan, ten_dichvu), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                        Utility.ShowMsg("Đã hủy tiếp nhận dịch vụ CĐHA thành công. Bác sĩ tại phòng khám có thể sửa, xóa chỉ định. Nhấn OK để kết thúc");
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void mnuBatdaukham_Click(object sender, EventArgs e)
        {
            grdList_DoubleClick(grdList, e);
        }
    }
}
