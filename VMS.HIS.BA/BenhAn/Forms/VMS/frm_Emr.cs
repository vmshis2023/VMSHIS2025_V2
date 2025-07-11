using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using NLog;
using VNS.Libs;
using VMS.HIS.DAL;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using SubSonic;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraPdfViewer;
using Janus.Windows.GridEX;
using VMS.Emr;
using VNS.HIS.UI.Classess;
using Aspose.Words;
using Document = Aspose.Words.Document;
using System.Diagnostics;
using VMS.HIS.Bus;
using VNS.HIS.BusRule.Classes;
using DevExpress.XtraBars;
using System.Text;
using System.Drawing;
using Aspose.Words.MailMerging;
using BarcodeLib;

namespace VNS.HIS.UI.EMR
{
    public partial class frm_Emr : Form
    {
        KcbLuotkham objLuotkham;
        bool isAutoLoad = false;
        bool isAllowSelectionChanged = false;
        private Logger _log;
        public FTPclient FtpClientRIS;
        public FTPclient FtpClientLIS;
        private string FtpClientCurrentDirectoryRIS = "";
        private string _baseDirectoryRIS = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "pdfRIS\\");

        private string FtpClientCurrentDirectoryEMR = "";
        private string _baseDirectoryEMR = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "EMR\\");


        private string FtpClientCurrentDirectoryLIS = "";
        private string _baseDirectoryLIS = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "pdfLIS\\");
        byte noitru = 0;
        RichEditControl richEdit = new DevExpress.XtraRichEdit.RichEditControl();
        byte SearchTypeKeyDown = 0;//0= mã lượt khám;1= id bệnh nhân;2= mã bệnh án;3= số vào viện;4= tên người bệnh;10=bấm nút tìm kiếm
        BarManager barManager;
        public frm_Emr()
        {
            InitializeComponent();
            InitializePdfToolbar();
            richEdit.Dock = DockStyle.Fill;
            //splitContainer1.Panel2.Controls.Add(richEdit); // hoặc Panel1
            richEdit.Visible = true;

            Utility.SetVisualStyle(this);
            ucThongtinnguoibenh_emr_basic1._OnEnterMe += UcThongtinnguoibenh_emr_basic1__OnEnterMe;

            _log = LogManager.GetCurrentClassLogger();
            InitFtp();
            grdList.SelectionChanged += GrdList_SelectionChanged;
            txtPatientCode.KeyDown += new KeyEventHandler(txtPatientCode_KeyDown);
            txtPatient_ID.KeyDown += TxtPatient_ID_KeyDown;
            txtSoBA.KeyDown += TxtSoBA_KeyDown;
            txtSovaovien.KeyDown += TxtSovaovien_KeyDown;
        }

        private void TxtSovaovien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Utility.sDbnull(txtSovaovien.Text).Length > 0)
                    SearchTypeKeyDown = 4;
                else
                    SearchTypeKeyDown = 10;
                TimKiemThongTin(false);
            }
        }

        private void TxtSoBA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Utility.sDbnull(txtSoBA.Text).Length > 0)
                    SearchTypeKeyDown = 3;
                else
                    SearchTypeKeyDown = 10;
                TimKiemThongTin(false);
            }
        }

        private void TxtPatient_ID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Utility.sDbnull(txtPatient_ID.Text).Length > 0)
                    SearchTypeKeyDown = 1;
                else
                    SearchTypeKeyDown = 10;
                TimKiemThongTin(false);
            }

        }

        void txtPatientCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Utility.sDbnull(txtPatientCode.Text).Length > 0)
                    SearchTypeKeyDown = 0;
                else
                    SearchTypeKeyDown = 10;
                TimKiemThongTin(false);
            }



        }
        void SearchWhenKeyDown(KeyEventArgs e)
        {
            try
            {
                var dtPatient = new DataTable();
                if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtPatientCode.Text.Trim()) != "")
                {
                    string _ID = txtPatient_ID.Text.Trim();
                    string _Name = txtPatientName.Text.Trim();
                    int _Idx = cboObjectType.SelectedIndex;
                    txtPatient_ID.Clear();
                    txtPatientName.Clear();
                    cboObjectType.SelectedIndex = -1;
                    txtPatientCode.Text = Utility.AutoFullPatientCode(txtPatientCode.Text);
                    optTatCa.Checked = true;
                    TimKiemThongTin(false);
                    cboObjectType.SelectedIndex = _Idx;
                    txtPatientName.Text = _Name;
                    txtPatient_ID.Text = _ID;
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin bệnh nhân");
                //throw;
            }
        }
        void PhanquyenTinhnang()
        {
            cmdAn.Enabled = mnuAnPhieu.Enabled = Utility.Coquyen("EMR_AN_PHIEU");
            cmdHienthi.Enabled = mnuHuyAnPhieu.Enabled = Utility.Coquyen("EMR_HUY_AN_PHIEU");
            cmdXoaphieu.Enabled = mnuXoaPhieu.Enabled = Utility.Coquyen("EMR_XOA_PHIEU");
            cmdRestore.Enabled = mnuHuyXoaPhieu.Enabled = Utility.Coquyen("EMR_RESTORE_PHIEU");
            //cmdChuyenGay.Enabled = mnuChuyenGay.Enabled = Utility.Coquyen("EMR_CHUYENGAY");
            cmdRestoreDefault_Gay.Enabled = mnuRestoreDefault_Gay.Enabled = Utility.Coquyen("EMR_CHUYENGAY_MACDINH");
            cmdLaythongtin.Enabled= Utility.Coquyen("EMR_KHOITAOTHONGTIN");
            cmdReset.Enabled = Utility.Coquyen("EMR_RESET");
        }
        //private void InitializePdfToolbar()
        //{
        //    // 1. Tạo BarManager và gán Form
        //    barManager = new BarManager { Form = this };
        //    this.components.Add(barManager);

        //    // 2. Tạo vùng dock toolbar ngay bên phải pnlPdf
        //    standalone = new StandaloneBarDockControl();
        //    standalone.Dock = DockStyle.Right;
        //    pnlPdf.Controls.Add(standalone);

        //    // 3. Tạo Bar (tự động thêm nhiều nút)
        //    var bar = new Bar(barManager, "PDF Main")
        //    {
        //        DockStyle = BarDockStyle.Standalone,
        //        StandaloneBarDockControl = standalone
        //    };
        //    bar.OptionsBar.RotateWhenVertical = false;
        //    barManager.Bars.Add(bar);

        //    // 4. Thêm các nút tương ứng với CreateBars(Main)
        //    // – Open / Save / Print
        //    AddBarItem("Open", PdfViewerCommands.OpenFile);
        //    AddBarItem("Save", PdfViewerCommands.SaveFile);
        //    AddBarItem("Print", PdfViewerCommands.PrintFile);

        //    barManager.Bars["PDF Main"].AddItem(new BarSubItem(barManager, "Page")
        //    {
        //        LinkPersistInfo = {
        //        new LinkPersistInfo(AddBarItem("Prev", PdfViewerCommands.PrevPage)),
        //        new LinkPersistInfo(AddBarItem("Next", PdfViewerCommands.NextPage)),
        //        new LinkPersistInfo(AddBarItem("Go to...", PdfViewerCommands.SetPageNumber)),
        //    }
        //    });

        //    // – Zoom controls
        //    barManager.Bars["PDF Main"].AddItem(AddBarItem("Zoom In", PdfViewerCommands.ZoomIn));
        //    barManager.Bars["PDF Main"].AddItem(AddBarItem("Zoom Out", PdfViewerCommands.ZoomOut));
        //    barManager.Bars["PDF Main"].AddItem(AddBarItem("Zoom Combo", PdfViewerCommands.ViewExactZoomList));

        //    // – Rotate
        //    barManager.Bars["PDF Main"].AddItem(AddBarItem("Rotate CW", PdfViewerCommands.RotateClockwise));
        //    barManager.Bars["PDF Main"].AddItem(AddBarItem("Rotate CCW", PdfViewerCommands.RotateCounterclockwise));

        //    // – Find, Navigation Pane
        //    barManager.Bars["PDF Main"].AddItem(AddBarItem("Find", PdfViewerCommands.FindText));
        //    barManager.Bars["PDF Main"].AddItem(AddBarItem("Thumbs Pane", PdfViewerCommands.ShowThumbnailsPane));
        //    barManager.Bars["PDF Main"].AddItem(AddBarItem("Bookmarks Pane", PdfViewerCommands.ShowBookmarksPane));
        //}

        // Hàm tiện lợi để map command sang BarButtonItem
        //private BarItem AddBarItem(string caption, ICommand command)
        //{
        //    var item = new BarButtonItem(barManager, caption);
        //    item.ItemClick += (s, e) => command.Execute(pdfViewer1);
        //    barManager.Items.Add(item);
        //    return item;
        //}
        private void InitializePdfToolbar()
        {
            // Tạo BarManager và gán Form trước khi gọi CreateBars
            var manager = new BarManager();
            manager.Form = this;
            this.components.Add(manager); // thêm vào components

            // Tạo vùng dock cố định
            var standalone = new StandaloneBarDockControl();
            standalone.Dock = DockStyle.Right;
            pnlPdf.Controls.Add(standalone);
            pdfViewer1.Dock = DockStyle.Fill;
            richEdit.Dock= DockStyle.Fill;
            pnlPdf.Controls.Add(pdfViewer1);
            pnlPdf.Controls.Add(richEdit);
            // Gọi CreateBars để DevExpress tạo toolbar
            pdfViewer1.CreateBars(PdfViewerToolbarKind.All); // trả về void :contentReference[oaicite:1]{index=1}

            // Gán hết các Bar vào vùng dock bạn vừa tạo
            foreach (Bar bar in manager.Bars)
            {
                bar.DockStyle = BarDockStyle.Standalone;
                bar.StandaloneBarDockControl = standalone;
                bar.OptionsBar.RotateWhenVertical = false;
            }
        }
        GridEXRow currRow = null;
        string nguoi_tao = "";
        SysSystemParameter sysSignsize = null;
        private void GrdList_SelectionChanged(object sender, EventArgs e)
        {

            sysSignsize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("signsize").ExecuteSingle<SysSystemParameter>();
            if (stream != null)
            {
                stream.Close();
                stream = null;
            }
            if (!isAllowSelectionChanged)
            {
                pdfViewer1.CloseDocument();
                return;
            }
            currRow = Utility.findthelastChild(grdList.CurrentRow);
            if (currRow == null)
            {
                pdfViewer1.CloseDocument();
                return;
            }

            try
            {
                Utility.WaitNow(this);
                string loaiphieuhis = "";
                string reportcode = "";
                long IdPhieu = -1;
                //Bắt đầu xử lý sinh lại file dựa vào loại phiếu HIS và report_code
                EmrDocument objDoc = EmrDocument.FetchByID(Utility.Int64Dbnull(currRow.Cells[EmrDocument.Columns.IdFile].Value));
                if (objDoc != null)
                {
                    IdPhieu = Utility.Int64Dbnull(objDoc.IdPhieu);// Utility.Int64Dbnull(currRow.Cells[EmrDocument.Columns.IdPhieu].Value);
                    loaiphieuhis = objDoc.LoaiPhieuHis;
                    reportcode = objDoc.ReportCode;
                }
                else
                {
                    loaiphieuhis = Utility.sDbnull(currRow.Cells[EmrDocument.Columns.LoaiPhieuHis].Value);
                    reportcode = Utility.sDbnull(currRow.Cells[EmrDocument.Columns.ReportCode].Value);
                }
                nguoi_tao = Utility.sDbnull(currRow.Cells[EmrDocument.Columns.NguoiTao].Value);

                DataTable v_dtData;
                SysReport objReport;
                string pdfFileName = "";
                if (loaiphieuhis == Loaiphieu_HIS.PHIEUDANGKYKCB)
                {
                    objReport = new Select().From(SysReport.Schema).Where(SysReport.Columns.MaBaocao).IsEqualTo(reportcode).ExecuteSingle<SysReport>();
                    if (objReport == null)
                    {
                        Utility.ShowMsg("Không tồn tại báo cáo có mã:" + reportcode + "\nKiểm tra lại chức năng khai báo");
                        return;
                    }
                    v_dtData = SPs.KcbTiepdonInphieukcb((int)IdPhieu).GetDataSet().Tables[0];
                    if (v_dtData.Rows.Count <= 0)
                    {
                        Utility.ShowMsg(string.Format("Không tồn tại dữ liệu của phiếu {0} với id={1}. Có thể đã bị xóa. Vui lòng kiểm tra lại", loaiphieuhis, IdPhieu));
                        return;
                    }
                    pdfFileName = WordPrinter.InPhieu(null, v_dtData, "PHIEUDANGKYKCB.doc", true);// Utility.sDbnull(objReport.FileWord));

                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUPTTT)
                {
                    KcbPhieupttt objPttt = KcbPhieupttt.FetchByID(IdPhieu);
                    DataTable dtData = SPs.KcbPtttInphieu(IdPhieu).GetDataSet().Tables[0];
                    if (dtData.Rows.Count <= 0)
                    {
                        Utility.ShowMsg(string.Format("Không tồn tại dữ liệu của phiếu {0} với id={1}. Có thể đã bị xóa. Vui lòng kiểm tra lại", loaiphieuhis, IdPhieu));
                        return;
                    }
                    dtData.TableName = "kcb_phieu_pttt";
                    string ma_loaidvu = "PTTT";
                    if (reportcode == "PHIEU_CAMKET_PTTT")
                    {
                        pdfFileName = InPhieuCamKetPTTT(dtData, objPttt, ma_loaidvu);
                    }
                    else if (reportcode == "PHIEU_CHUNGNHAN_PTTT")
                    {
                        pdfFileName = InPhieuChungNhanPTTT(dtData, objPttt, ma_loaidvu);
                    }
                    else if (reportcode == "PHIEU_PTTT_NOITRU")
                    {
                        pdfFileName = InPhieuPTTT(dtData, objPttt);
                    }
                    else if (reportcode == "PHIEU_TUONGTRINH_PTTT")
                    {
                        pdfFileName = InPhieuTuongTrinhPTTT(dtData, objPttt, ma_loaidvu);
                    }

                }
                else if (loaiphieuhis == Loaiphieu_HIS.BENHAN)
                {
                    EmrBa emr_ba = new Select().From(EmrBa.Schema)
              .Where(EmrBa.Columns.IdBa).IsEqualTo(IdPhieu)
              .ExecuteSingle<EmrBa>();
                    pdfFileName = clsInBA.InBA(emr_ba, objLuotkham, reportcode, true);
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUTOMTATDIEUTRINGOAITRU)//Phiếu tóm tắt điều trị ngoại trú
                {
                    DataSet dsData = new KCB_THAMKHAM().LaythongtinInphieuTtatDtriNgoaitru_2023(IdPhieu, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, Utility.ByteDbnull(0));//Phần giới tính để lấy giá trị BT nam, nữ nhưng ko cần vì phiếu này ko in giá trị đó
                    v_dtData = dsData.Tables[1];
                    if (v_dtData.Rows.Count <= 0)
                    {
                        Utility.ShowMsg(string.Format("Không tồn tại dữ liệu của phiếu {0} với id={1}. Có thể đã bị xóa. Vui lòng kiểm tra lại", loaiphieuhis, IdPhieu));
                        return;
                    }
                    DataRow drInfor = null;
                    if(dsData.Tables[0].Rows.Count>0) drInfor=dsData.Tables[0].Rows[0];

                    ChangeData(ref v_dtData, drInfor, dsData.Tables[2], dsData.Tables[4]);
                    //List<string> lstBarcodeFields = new List<string>() { "ma_luotkham_barcode" };
                    //List<string> lstBarcodeValues = new List<string>() { Utility.sDbnull(drInfor["ma_luotkham_barcode"]) };
                    pdfFileName = WordPrinter.InPhieu(null, v_dtData, "PHIEUTOMTATDIEUTRINGOAITRU.doc", null, null, true);// Utility.sDbnull(objReport.FileWord));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEURAVIEN)
                {
                    DataTable dtData = SPs.NoitruInphieuravien(objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                    if (dtData.Rows.Count <= 0)
                    {
                        Utility.ShowMsg(string.Format("Không tồn tại dữ liệu của phiếu {0} với id={1}. Có thể đã bị xóa. Vui lòng kiểm tra lại", loaiphieuhis, IdPhieu));
                        return;
                    }
                    pdfFileName = VMS.HIS.Bus.WordPrinter.InPhieu(null, dtData, "PHIEU_RAVIEN.doc", true);
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUNHAPVIEN)
                {
                    pdfFileName = IN_PHIEU_KHAM_VAO_VIEN(reportcode);
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUCHUYENVIEN)
                {

                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUCHIDINH)
                {
                    pdfFileName = string.Format(@"{0}\EMR_DOCUMENTS\{1}\{2}\{3}_{4}.pdf", Application.StartupPath, objLuotkham.MaLuotkham, loaiphieuhis, IdPhieu, reportcode);
                    Utility.Try2CreateFolder(Path.GetDirectoryName(pdfFileName));
                    if (objDoc != null)
                    {
                        DataTable m_dtChitietPhieuCLS = new KCB_CHIDINH_CANLAMSANG().LaythongtinCLS_Thuoc((int)IdPhieu, "DICHVU");
                        ResetNhominCLS(m_dtChitietPhieuCLS, reportcode, ref pdfFileName, Utility.Bool2Bool(objDoc.LaPhieutach));
                    }
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEU_KQCDHA || loaiphieuhis == Loaiphieu_HIS.PHIEU_KQXN)
                {
                    try
                    {
                        if (stream != null)
                        {
                            stream.Close();
                            stream = null;
                        }
                        if (!Utility.isValidGrid(grdList)) return;
                        string fileName = grdList.GetValue("file_path").ToString();
                        string ma_nhom = grdList.GetValue("ma_nhom").ToString();
                        string localFile = "";
                        string ftpFile = "";
                        if (ma_nhom == "XN")
                        {
                            localFile = string.Format(@"{0}{1}", _baseDirectoryLIS, fileName.Replace(@"/", @"\"));
                            ftpFile = string.Format(@"{0}{1}", FtpClientCurrentDirectoryLIS, fileName);
                        }
                        else
                        {
                            localFile = string.Format(@"{0}{1}", _baseDirectoryRIS, fileName.Replace(@"/", @"\"));
                            ftpFile = string.Format(@"{0}{1}", FtpClientCurrentDirectoryRIS, fileName);
                        }
                        string parentFolder = Path.GetDirectoryName(localFile);
                        Utility.Try2CreateFolder(Directory.GetParent(parentFolder).FullName);
                        Utility.Try2CreateFolder(parentFolder);
                        if (File.Exists(localFile))
                        {
                            if (chkForced2Download.Checked)
                            {
                                if (ma_nhom == "XN")
                                    FtpClientLIS.Download(ftpFile, localFile, true);
                                else
                                    FtpClientRIS.Download(ftpFile, localFile, true);
                            }
                        }
                        else//Download and open
                        {
                            if (ma_nhom == "XN")
                                FtpClientLIS.Download(ftpFile, localFile, true);
                            else
                                FtpClientRIS.Download(ftpFile, localFile, true);
                        }
                        pdfFileName = localFile;
                        //string Url = string.Format("{0}?zoom=100%#navpanes=1&toolbar=1", localFile);
                        //this.Text = string.Format("Xem kết quả PDF từ file: {0}", Url);

                    }
                    catch (Exception ex)
                    {
                        Utility.ShowMsg(ex.Message);
                    }
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUDIEUTRI)
                {
                    pdfFileName = string.Format(@"{0}\EMR_DOCUMENTS\{1}\{2}\{3}_{4}.pdf", Application.StartupPath, objLuotkham.MaLuotkham, loaiphieuhis, IdPhieu, reportcode);
                    InphieuDieutri(pdfFileName, IdPhieu.ToString(), false);
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUCHIDINH)
                {

                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUCHIDINH)
                {

                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUCHIDINH)
                {

                }
                pdfViewer1.CloseDocument();
                if (pdfFileName != "" && File.Exists(pdfFileName))
                {
                    if (stream == null)
                        stream = new FileStream(pdfFileName, FileMode.Open);
                    pdfViewer1.LoadDocument(stream);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                Utility.DefaultNow(this);
            }
        }
        void InphieuDieutri(string pdfFileName, string TreatmentId, bool KyDientu)
        {
            try
            {
                List<string> lstSign = new List<string>();
                DataSet dsPrint = new noitru_phieudieutri().NoitruLaythongtinphieudieutriIn(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, TreatmentId.ToString());
                DataTable m_dtPhieuDieutri;
                m_dtPhieuDieutri = dsPrint.Tables[0];
                m_dtPhieuDieutri.TableName = "Phieudieutri";
                List<string> lstMoreColumns = new List<string>() { "ten_benhvien", "ten_SYT", "diahchi_benhvien", "SDT_bv", "Hotline_bv", "Fax_bv", "website_bv", "email_bv" };
                Utility.AddColums2DataTable(ref m_dtPhieuDieutri, lstMoreColumns, typeof(string));
                Document doc;
                DataRow drData = m_dtPhieuDieutri.Rows[0];
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                drData["SDT_bv"] = globalVariables.Branch_Phone;
                drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                drData["Fax_bv"] = globalVariables.Branch_Fax;
                drData["website_bv"] = globalVariables.Branch_Website;
                drData["email_bv"] = globalVariables.Branch_Email;
                List<string> fieldNames = new List<string>();

                string PathDoc = string.Format(@"{0}\Doc\{1}", AppDomain.CurrentDomain.BaseDirectory, "PHIEUDIEUTRI.doc");
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(m_dtPhieuDieutri);

                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu:" + PathDoc);
                    return;
                }



                string checkboxFieldsFile = AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\BA_CHECKED_FIELDS.txt";
                List<string> lstcheckboxfields = Utility.GetFirstValueFromFile(checkboxFieldsFile).Split(',').ToList<string>();
                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo");
                    }
                    Utility.MergeFieldsCheckBox2Doc(builder, null, lstcheckboxfields, drData);
                    //Tạo thông tin y lệnh trong tờ điều trị
                    foreach (DataRow row in m_dtPhieuDieutri.Rows)
                    {
                        nguoi_tao = Utility.sDbnull(row["nguoi_tao"]);
                        var YLENH = new StringBuilder("");

                        //Tạo thông tin thuốc. 
                        List<DataRow> query = (dsPrint.Tables[1].AsEnumerable().Where(
                           chidinh => Utility.Int32Dbnull(chidinh["id_phieudieutri"]) == Utility.Int32Dbnull(row["id_phieudieutri"])
                                      &&
                                      Utility.Int32Dbnull(chidinh["id_loaithanhtoan"]) ==
                                      Utility.Int32Dbnull(KieuLoaiThanhToan.Thuoc))).ToList();
                        if (query.Any())
                        {
                            foreach (DataRow dr in query)
                            {
                                YLENH.Append("<p>");
                                YLENH.Append(string.Format("<b>{0} ( {1} )</b>", Utility.sDbnull(dr["TEN"]), Utility.sDbnull(dr["ten_hoatchat"])));
                                YLENH.Append("<span > x </span> <b>");
                                YLENH.Append(Utility.sDbnull(dr["SOLUONG"]));
                                YLENH.Append(" ");
                                YLENH.Append(Utility.sDbnull(dr["DONVI"]));
                                YLENH.Append("</b>");
                                if (Utility.sDbnull(dr["sDesc"]).Length > 0)
                                    YLENH.Append(string.Format("</br><i>{0}</i>", Utility.sDbnull(dr["sDesc"])));
                                YLENH.Append("</p>");
                            }
                        }
                        //Tạo thông tin chỉ định
                        query = (from chidinh in dsPrint.Tables[1].AsEnumerable()
                                 where
                                     Utility.Int32Dbnull(chidinh["id_phieudieutri"]) ==
                                     Utility.Int32Dbnull(row["id_phieudieutri"])
                                     &&
                                     Utility.Int32Dbnull(chidinh["id_loaithanhtoan"]) ==
                                     Utility.Int32Dbnull(KieuLoaiThanhToan.CLS)
                                 select chidinh).ToList();
                        if (query.Any())
                        {
                            var q = (from p in query
                                     select Utility.sDbnull(p["TEN"]));
                            string dichvu = string.Join(",", q.ToArray<string>());
                            //foreach (DataRow dr in query)
                            //{
                            YLENH.Append("<p>");
                            YLENH.Append(string.Format("{0}", dichvu));
                            YLENH.Append("</p>");
                            //}
                        }
                        row["YLENH"] = YLENH.ToString();
                        //Đã tạo xong y lệnh-->Ghi luôn vào các rows
                        Aspose.Words.Tables.Table tab = doc.FirstSection.Body.Tables[1];
                        int idx = 1;
                        Aspose.Words.Tables.Row newRow = (Aspose.Words.Tables.Row)tab.LastRow.Clone(true);
                        //newRow.RowFormat.Borders.Shadow = false;
                        //newRow.Cells[0].CellFormat.Shading.BackgroundPatternColor = Color.White;
                        //newRow.Cells[1].CellFormat.Shading.BackgroundPatternColor = Color.White;
                        //newRow.Cells[2].CellFormat.Shading.BackgroundPatternColor = Color.White;


                        newRow.Cells[0].RemoveAllChildren();

                        newRow.Cells[1].RemoveAllChildren();

                        newRow.Cells[2].RemoveAllChildren();
                        newRow.Cells[0].EnsureMinimum();
                        newRow.Cells[1].EnsureMinimum();
                        newRow.Cells[2].EnsureMinimum();

                        Run r = new Run(doc);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.Font.Bold = false;
                        //r.Font.Color = Color.FromArgb(102, 0, 102);
                        r.Text = Utility.sDbnull(row["NGAY_LAPPHIEU"], "");
                        newRow.Cells[0].FirstParagraph.AppendChild(r);
                        newRow.Cells[0].FirstParagraph.ParagraphFormat.Alignment = Aspose.Words.ParagraphAlignment.Center;
                        int i = 0;
                        while (i < newRow.Cells[0].Paragraphs.Count)
                        {
                            var para = newRow.Cells[0].Paragraphs[i];
                            if (string.IsNullOrWhiteSpace(para.ToString(SaveFormat.Text)))
                                para.Remove();
                            else
                                i++;
                        }
                        i = 0;
                        r = new Run(doc);
                        r.Font.Name = "Times New Roman";
                        r.Font.Bold = false;
                        r.Font.Size = 12;
                        //r.Font.Color = Color.FromArgb(102, 0, 102);
                        r.Text = Utility.sDbnull(row["DIENBIEN"], "");
                        newRow.Cells[1].FirstParagraph.AppendChild(r);
                        newRow.Cells[1].CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Top;
                        newRow.Cells[1].FirstParagraph.ParagraphFormat.Alignment = Aspose.Words.ParagraphAlignment.Left;
                        while (i < newRow.Cells[1].Paragraphs.Count)
                        {
                            var para = newRow.Cells[1].Paragraphs[i];
                            if (string.IsNullOrWhiteSpace(para.ToString(SaveFormat.Text)))
                                para.Remove();
                            else
                                i++;
                        }
                        i = 0;
                        r = new Run(doc);
                        r.Font.Name = "Times New Roman";
                        r.Font.Bold = false;
                        r.Font.Size = 12;
                        //r.Font.Color = Color.FromArgb(102, 0, 102);
                        //r.Text = Utility.sDbnull(row["YLENH"], "");
                        newRow.Cells[2].CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Top;
                        newRow.Cells[2].FirstParagraph.ParagraphFormat.Alignment = Aspose.Words.ParagraphAlignment.Left;
                        builder.MoveTo(newRow.Cells[2].FirstParagraph);  // Di chuyển con trỏ vào đoạn đầu của cell

                        builder.InsertHtml(Utility.sDbnull(row["YLENH"], ""));
                        builder.Writeln();  // hoặc dùng builder.InsertParagraph();

                        // Bước 3: Chèn merge field cho chữ ký bác sĩ
                        if (KyDientu) builder.InsertField(string.Format("MERGEFIELD {0} \\* MERGEFORMAT", nguoi_tao), "");


                        while (i < newRow.Cells[2].Paragraphs.Count)
                        {
                            var para = newRow.Cells[2].Paragraphs[i];
                            if (string.IsNullOrWhiteSpace(para.ToString(SaveFormat.Text)))
                                para.Remove();
                            i++;
                        }

                        tab.AppendChild(newRow);
                        idx += 1;
                    }
                    doc.MailMerge.PreserveUnusedTags = true;
                    //Merge các field thông tin chung của người bệnh
                    doc.MailMerge.Execute(drData);

                    if (KyDientu)//Tìm các vùng chữ kí để đưa ảnh vào
                    {
                        string[] remaining = doc.MailMerge.GetFieldNames();
                        lstSign = m_dtPhieuDieutri.AsEnumerable().Select(c => Utility.sDbnull(c.Field<string>("nguoi_tao"))).Distinct().ToList<string>();
                        if (remaining.Length > 0)
                        {

                            foreach (var name in remaining)
                            {
                                if (lstSign.Contains(name))
                                {
                                    string _defaultSign = string.Format(@"{0}\{1}\default", Application.StartupPath, "sign");
                                    string _signFile = string.Format(@"{0}\{1}\{2}", Application.StartupPath, "sign", name);
                                    byte[] _sign = null;
                                    if (File.Exists(_signFile))
                                    {
                                        _sign = Utility.fromimagepath2byte(_signFile);
                                    }
                                    else
                                    {
                                        if (File.Exists(_defaultSign))
                                            _sign = Utility.fromimagepath2byte(_defaultSign);
                                    }

                                    if (builder.MoveToMergeField(name))
                                        if (_sign != null)
                                        {
                                            if (sysSignsize != null)
                                            {
                                                int w = Utility.Int32Dbnull(sysSignsize.SValue.Split('x')[0], 0);
                                                int h = Utility.Int32Dbnull(sysSignsize.SValue.Split('x')[1], 0);
                                                if (w > 0 && h > 0)
                                                    builder.InsertImage(_sign, w, h);
                                                else
                                                    builder.InsertImage(_sign);
                                            }
                                            else
                                                if (_sign != null)
                                                builder.InsertImage(_sign);
                                        }
                                    //else//Không cần vì mergefield này ẩn
                                    //    builder.InsertImage(NoImage, 10, 10);
                                }
                            }
                        }
                        else
                        {

                        }

                    }

                    if (File.Exists(pdfFileName))
                    {
                        File.Delete(pdfFileName);
                    }
                    doc.Save(pdfFileName, SaveFormat.Pdf);

                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        void ChangeData(ref DataTable vDtLastData, DataRow drInfor, DataTable dtXn, DataTable dtcdha)
        {

            bool GhepTTCLS = THU_VIEN_CHUNG.Laygiatrithamsohethong("THAMKHAM_INTOMTATDIEUTRINGOAITRU_GHEPTOMTAT_CLS", "0", true) == "1";
            bool GhepCHANDOAN_ICD = THU_VIEN_CHUNG.Laygiatrithamsohethong("THAMKHAM_INTOMTATDIEUTRINGOAITRU_GHEPCHANDOAN_ICD", "0", true) == "1";
            string icdCode = "";
            string tomtatcls = "";
            string icdName = "";
            string kqcdha = "";
            string kqxn = "";
            bool rebuildTomtatCLS = false;
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THAMKHAM_INTOMTATDIEUTRINGOAITRU_TUDONGLAYDULIEUKQCLS", "0", true) == "1")
            {
                rebuildTomtatCLS = true;
                foreach (DataRow dr in dtXn.Rows)
                {
                    kqxn += string.Format("{0}:{1},", Utility.sDbnull(dr["ten_thongso"]), Utility.sDbnull(dr["ket_qua"]));
                }
                foreach (DataRow dr in dtcdha.Rows)
                {
                    kqcdha += string.Format("{0}:{1}\r\n", Utility.sDbnull(dr["ten_chitietdichvu"]), Utility.sDbnull(dr["ket_qua"]));
                }
            }
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THAMKHAM_INTOMTATDIEUTRINGOAITRU_TUDONGLAYDULIEUKQCLS_XN", "0", true) == "1")
            {
                tomtatcls += kqxn;
            }
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THAMKHAM_INTOMTATDIEUTRINGOAITRU_TUDONGLAYDULIEUKQCLS_CDHA", "0", true) == "1")
            {
                tomtatcls += kqcdha;
            }
            if (drInfor != null)
                GetChanDoan(Utility.sDbnull(drInfor["mabenh_chinh"], ""),
                            Utility.sDbnull(drInfor["mabenh_phu"], ""), ref icdName, ref icdCode);
            foreach (DataRow dr in vDtLastData.Rows)
            {
                if (GhepCHANDOAN_ICD)
                {
                    dr["chan_doan"] = string.Format("{0};{1},{2}", Utility.sDbnull(dr["chan_doan"]), Utility.sDbnull(dr["tenbenh_chinh"]), Utility.sDbnull(dr["tenbenh_phu"]));
                    //dr[DmucBenh.Columns.MaBenh] = ICD_Code;
                    dr["ma_icd"] = icdCode;
                }
                else
                {
                    //dr["chan_doan"] = Utility.sDbnull(dr["chan_doan"]).Trim() == ""
                    //    ? icdName
                    //    : Utility.sDbnull(dr["chan_doan"]) + "; " + icdName;
                    ////dr[DmucBenh.Columns.MaBenh] = ICD_Code;
                    //dr["ma_icd"] = icdCode;
                }

                if (Utility.sDbnull(tomtatcls).Length > 0)
                {
                    if (GhepTTCLS)
                    {
                        if (Utility.sDbnull(dr["tomtat_cls"], "").Length > 0)
                            dr["tomtat_cls"] += ", " + tomtatcls;
                        else
                            dr["tomtat_cls"] = tomtatcls;
                    }
                    else
                        dr["tomtat_cls"] = tomtatcls;
                }
            }
        }
        void ResetNhominCLS(DataTable dt_Data, string reportcode, ref string PdfFilePath, bool isInTachPhieu)
        {
            try
            {
                List<string> nhomcls = new List<string>();
                if (dt_Data.Rows.Count <= 0) return;
                long id_phieu = Utility.Int64Dbnull(dt_Data.Rows[0][KcbChidinhclsChitiet.Columns.IdChidinh],
                                                    -1);
                string ma_chidinh = Utility.sDbnull(dt_Data.Rows[0][KcbChidinhcl.Columns.MaChidinh], "");



                foreach (DataRow dr in dt_Data.Rows)
                {
                    if (Utility.Int64Dbnull(dr[KcbChidinhclsChitiet.Columns.IdChidinh]) == id_phieu)
                        if (!nhomcls.Contains(Utility.sDbnull(dr["nhom_in_cls"])))
                        {
                            nhomcls.Add(Utility.sDbnull(dr["nhom_in_cls"]));
                        }
                }
                DataTable dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung(globalVariables.DC_NHOMIN_CLS, true);
                if (!dtNhomin.Columns.Contains("ma_phieu"))
                    dtNhomin.Columns.AddRange(new DataColumn[] { new DataColumn(KcbChidinhcl.Columns.IdChidinh, typeof(Int64)), new DataColumn(KcbChidinhcl.Columns.MaChidinh, typeof(string)) });
                DataTable dttempt = dtNhomin.Clone();
                foreach (DataRow dr in dtNhomin.Rows)
                {
                    dr[KcbChidinhcl.Columns.IdChidinh] = id_phieu;
                    dr[KcbChidinhcl.Columns.MaChidinh] = ma_chidinh;
                    if (nhomcls.Contains(Utility.sDbnull(dr[DmucChung.Columns.Ma], "")))
                        dttempt.ImportRow(dr);
                }
                //Thực hiện in tách
                List<long> lstSelectedPrint = (from p in dt_Data.AsEnumerable()
                                               select Utility.Int64Dbnull(p[KcbChidinhclsChitiet.Columns.IdChitietchidinh], 0)).ToList();
                List<string> lstNhominCLS = new List<string>() { reportcode };
                if (lstNhominCLS.Count <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất một nhóm phiếu cần in tách");
                    return;
                }
                string mayin = "";
                if (isInTachPhieu)
                {
                    PdfFilePath = KcbInphieu.InTachToanBoPhieuCls_Doc(lstSelectedPrint, (int)objLuotkham.IdBenhnhan,
                                                                    objLuotkham.MaLuotkham, id_phieu,
                                                                    ma_chidinh, lstNhominCLS, "",
                                                                    -1, true,
                                                                    ref mayin, true, PdfFilePath);
                }
                else
                {
                    string nhomincls = "ALL";
                    PdfFilePath = KcbInphieu.InphieuChidinhCls_doc(lstSelectedPrint, (int)objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham,
                                                                  id_phieu,
                                                                  ma_chidinh, nhomincls, -1,
                                                                  false,
                                                                  ref mayin, true, PdfFilePath);
                }


            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());

            }
        }
        private string IN_PHIEU_KHAM_VAO_VIEN(string reportcode)
        {
            DataTable dsTable =
               new noitru_nhapvien().NoitruLaythongtinInphieunhapvien(objLuotkham.MaLuotkham, Utility.Int32Dbnull(objLuotkham.IdBenhnhan));
            if (dsTable.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy bản ghi nào\n Mời bạn xem lại", "Thông báo", MessageBoxIcon.Error);
                return "";
            }

            SqlQuery sqlQuery = new Select().From(KcbChandoanKetluan.Schema)
                  .Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                  .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                  .And(KcbChandoanKetluan.Columns.Noitru).IsEqualTo(0)
                  .OrderAsc(KcbChandoanKetluan.Columns.NgayChandoan);
            var objInfoCollection = sqlQuery.ExecuteAsCollection<KcbChandoanKetluanCollection>();
            string chandoan = "";
            string machandoan = "";
            string mabenh = "";
            string phongkhamvaovien = "";
            string khoanoitru = "";
            string ten_benhcp = "";
            foreach (KcbChandoanKetluan objDiagInfo in objInfoCollection)
            {
                string ICD_Name = "";
                string ICD_Code = "";
                GetChanDoan(Utility.sDbnull(objDiagInfo.MabenhChinh, ""),
                            Utility.sDbnull(objDiagInfo.MabenhPhu, ""), ref ICD_Name, ref ICD_Code);
                chandoan += string.IsNullOrEmpty(objDiagInfo.Chandoan)
                                ? ICD_Name
                                : Utility.sDbnull(objDiagInfo.Chandoan);
                mabenh += ICD_Code;
                ten_benhcp += ICD_Name;
            }
            //DataTable dtDataChandoan = SPs.ThamkhamLaythongtinchandoan(machandoan).GetDataSet().Tables[0];
            //txtkbMa.Text = Utility.sDbnull(mabenh);
            //if (dtDataChandoan.Rows.Count > 0) chandoan = Utility.sDbnull(dtDataChandoan.Rows[0][0], "");
            chandoan += "," + ten_benhcp;
            DataSet ds = new noitru_nhapvien().KcbLaythongtinthuocKetquaCls(objLuotkham.MaLuotkham, Utility.Int32Dbnull(objLuotkham.IdBenhnhan), (byte)0);
            DataTable dtThuoc = ds.Tables[0];
            DataTable dtketqua = ds.Tables[1];

            string[] query = (from thuoc in dtThuoc.AsEnumerable()
                              let y = Utility.sDbnull(thuoc["ten_thuoc"])
                              select y).ToArray();
            string donthuoc = string.Join(";", query);
            string[] querykq = (from kq in dtketqua.AsEnumerable()
                                let y = Utility.sDbnull(kq["ketqua"])
                                select y).ToArray();
            string ketquaCLS = string.Join("; ", querykq);

            bool tudongnaplai_thuoc_cls_khiin = THU_VIEN_CHUNG.Laygiatrithamsohethong("NHAPVIEN_TUDONGNAP_THUOC_KQCLS_KHIIN", "0", true) == "1";
            bool donthuoclaytubangdulieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("NHAPVIEN_THUOCDADUNG_LAYTUBANGDULIEU", "0", true) == "1";
            bool chandoanlaytubangdulieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("NHAPVIEN_CHANDOAN_LAYTUBANGDULIEU", "0", true) == "1";
            bool kqclslaytubangdulieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("NHAPVIEN_KQCLS_LAYTUBANGDULIEU", "0", true) == "1";
            //foreach (DataRow dr in dsTable.Rows)
            //{
            if (tudongnaplai_thuoc_cls_khiin)
            {
                DataRow dr = dsTable.Rows[0];
                if (dr != null)
                {
                    if (donthuoclaytubangdulieu)
                        dr["thuockedon"] = donthuoc;
                    if (chandoanlaytubangdulieu)
                        dr["CHANDOAN_VAOVIEN"] = chandoan;
                    if (kqclslaytubangdulieu)
                        dr["KETQUA_CLS"] = ketquaCLS;
                }
            }
            SysReport objReport = new Select().From(SysReport.Schema).Where(SysReport.Columns.MaBaocao).IsEqualTo(reportcode).ExecuteSingle<SysReport>();
            if (objReport == null)
            {
                Utility.ShowMsg("Không tồn tại báo cáo có mã:" + reportcode + "\nKiểm tra lại chức năng khai báo");
                return "";
            }
            return WordPrinter.InPhieu(null, dsTable, Utility.sDbnull(objReport.FileWord), true);
        }
        private void GetChanDoan(string ICD_chinh, string IDC_Phu, ref string ICD_Name, ref string ICD_Code)
        {
            try
            {
                List<string> lstICD = ICD_chinh.Split(',').ToList();
                DmucBenhCollection _list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _item in _list)
                {
                    ICD_Name += _item.TenBenh + ";";
                    ICD_Code += _item.MaBenh + ";";
                }
                lstICD = IDC_Phu.Split(',').ToList();
                _list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _item in _list)
                {
                    ICD_Name += _item.TenBenh + ";";
                    ICD_Code += _item.MaBenh + ";";
                }
                if (ICD_Name.Trim() != "") ICD_Name = ICD_Name.Substring(0, ICD_Name.Length - 1);
                if (ICD_Code.Trim() != "") ICD_Code = ICD_Code.Substring(0, ICD_Code.Length - 1);
            }
            catch
            {
            }
        }
        #region Các phiếu PTTT
        private string InPhieuChungNhanPTTT(DataTable dtData, KcbPhieupttt objpttt, string ma_loaidvu)
        {
            try
            {

                dtData.TableName = "kcb_phieu_pttt";
                List<string> lst_ten_phieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("PTTT_TENPHIEU", "GIẤY CHỨNG NHẬN PHẪU THUẬT-THỦ THUẬT", true).Split('@').ToList<string>();

                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                drData["SDT_bv"] = globalVariables.Branch_Phone;
                drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                drData["Fax_bv"] = globalVariables.Branch_Fax;
                drData["website_bv"] = globalVariables.Branch_Website;
                drData["email_bv"] = globalVariables.Branch_Email;
                drData["ten_phieu"] = ma_loaidvu == "PTTT" ? lst_ten_phieu[0] : (ma_loaidvu == "PHAUTHUAT" ? lst_ten_phieu[1] : lst_ten_phieu[2]);
                List<string> fieldNames = new List<string>();

                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\PHIEU_CHUNGNHAN_PTTT.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtData);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport("PHIEU_CHUNGNHAN_PTTT", ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu PTTT tại thư mục sau :" + PathDoc);
                    return "";
                }


                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return "";
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), "PHIEU_CHUNGNHAN_PTTT", objLuotkham.MaLuotkham, Utility.sDbnull(objpttt.IdPhieu), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return "";
                    }
                    if (builder.MoveToMergeField("logo") && globalVariables.SysLogo != null)
                        if (sysLogosize != null)
                        {
                            int w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            int h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(globalVariables.SysLogo, w, h);
                            else
                                builder.InsertImage(globalVariables.SysLogo);
                        }
                        else
                            if (globalVariables.SysLogo != null)
                            builder.InsertImage(globalVariables.SysLogo);
                    byte[] NoImage = Utility.fromimagepath2byte(AppDomain.CurrentDomain.BaseDirectory + "Noimage\\Noimage.png");
                    if (builder.MoveToMergeField("anh1"))
                    {
                        byte[] myimage = null;

                        if (objpttt != null && objpttt.MaHinhanh != null)
                        {
                            if (objpttt.MaHinhanh == "0" || objpttt.MaHinhanh == null)
                            {
                                myimage = null;
                            }
                            else //if (objpttt.MaHinhanh == "1")
                            {
                                myimage = Utility.fromimagepath2byte(Application.StartupPath + string.Format(@"\Hinhanh_PTTT\pttt0{0}.png", objpttt.MaHinhanh));
                            }
                            //else if (objpttt.MaHinhanh == "2")
                            //{
                            //    myimage = Utility.fromimagepath2byte(Application.StartupPath + @"\Hinhanh_PTTT\pttt02.png");
                            //}
                            //else if (objpttt.MaHinhanh == "3")
                            //{
                            //    myimage = Utility.fromimagepath2byte(Application.StartupPath + @"\Hinhanh_PTTT\pttt03.png");
                            //}

                        }
                        if (myimage != null)
                            builder.InsertImage(myimage);
                        else
                            builder.InsertImage(new List<byte>().ToArray(), 10, 10);
                    }
                    else
                    {
                        if (builder.MoveToMergeField("anh1"))
                            builder.InsertImage(NoImage, 10, 10);
                    }

                    doc.MailMerge.Execute(drData);
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Pdf);
                    return fileKetqua;

                    //if (File.Exists(path))
                    //{
                    //    Process process = new Process();
                    //    try
                    //    {
                    //        process.StartInfo.FileName = path;
                    //        process.Start();
                    //        process.WaitForInputIdle();
                    //    }
                    //    catch
                    //    {
                    //    }
                    //}
                }
                else
                {
                    MessageBox.Show("Không tìm thấy biểu mẫu", "TThông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                return "";
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return "";
            }
        }

        private string InPhieuPTTT(DataTable dtData, KcbPhieupttt objpttt)
        {
            try
            {

                dtData.TableName = "kcb_phieu_pttt";
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                drData["SDT_bv"] = globalVariables.Branch_Phone;
                drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                drData["Fax_bv"] = globalVariables.Branch_Fax;
                drData["website_bv"] = globalVariables.Branch_Website;
                drData["email_bv"] = globalVariables.Branch_Email;
                List<string> fieldNames = new List<string>();

                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\PHIEU_PTTT_NOITRU.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtData);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport("PHIEU_PTTT_NOITRU", ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu PTTT tại thư mục sau :" + PathDoc);
                    return "";
                }


                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return "";
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), "PHIEU_PTTT_NOITRU", objLuotkham.MaLuotkham, Utility.sDbnull(objpttt.IdPhieu), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return "";
                    }
                    if (builder.MoveToMergeField("logo") && globalVariables.SysLogo != null)
                        if (sysLogosize != null)
                        {
                            int w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            int h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(globalVariables.SysLogo, w, h);
                            else
                                builder.InsertImage(globalVariables.SysLogo);
                        }
                        else
                            if (globalVariables.SysLogo != null)
                            builder.InsertImage(globalVariables.SysLogo);

                    byte[] NoImage = Utility.fromimagepath2byte(AppDomain.CurrentDomain.BaseDirectory + "Noimage\\Noimage.png");
                    if (builder.MoveToMergeField("anh1"))
                    {
                        byte[] myimage = null;

                        if (objpttt != null && objpttt.MaHinhanh != null)
                        {
                            if (objpttt.MaHinhanh == "0" || objpttt.MaHinhanh == null)
                            {
                                myimage = null;
                            }
                            else //if (objpttt.MaHinhanh == "1")
                            {
                                myimage = Utility.fromimagepath2byte(Application.StartupPath + string.Format(@"\Hinhanh_PTTT\pttt0{0}.png", objpttt.MaHinhanh));
                            }
                            //else if (objpttt.MaHinhanh == "2")
                            //{
                            //    myimage = Utility.fromimagepath2byte(Application.StartupPath + @"\Hinhanh_PTTT\pttt02.png");
                            //}
                            //else if (objpttt.MaHinhanh == "3")
                            //{
                            //    myimage = Utility.fromimagepath2byte(Application.StartupPath + @"\Hinhanh_PTTT\pttt03.png");
                            //}

                        }
                        if (myimage != null)
                            builder.InsertImage(myimage);
                        else
                            builder.InsertImage(new List<byte>().ToArray(), 10, 10);
                    }
                    else
                    {
                        if (builder.MoveToMergeField("anh1"))
                            builder.InsertImage(NoImage, 10, 10);
                    }
                    doc.MailMerge.Execute(drData);
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Pdf);
                    return fileKetqua;

                    //if (File.Exists(path))
                    //{
                    //    Process process = new Process();
                    //    try
                    //    {
                    //        process.StartInfo.FileName = path;
                    //        process.Start();
                    //        process.WaitForInputIdle();
                    //    }
                    //    catch
                    //    {
                    //    }
                    //}
                }
                else
                {
                    MessageBox.Show("Không tìm thấy biểu mẫu", "TThông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                return "";
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return "";
            }
        }

        private string InPhieuCamKetPTTT(DataTable dtData, KcbPhieupttt objpttt, string ma_loaidvu)
        {
            try
            {

                dtData.TableName = "kcb_phieu_pttt";
                List<string> lst_ten_phieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("PTTT_TENPHIEU_CAMKET", "GIẤY CAM ĐOAN CHẤP NHẬN PHẪU THUẬT, THỦ THUẬT VÀ GÂY MÊ HỒI SỨC@GIẤY CAM ĐOAN CHẤP NHẬN PHẪU THUẬT, THỦ THUẬT VÀ GÂY MÊ HỒI SỨC", true).Split('@').ToList<string>();

                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                drData["SDT_bv"] = globalVariables.Branch_Phone;
                drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                drData["Fax_bv"] = globalVariables.Branch_Fax;
                drData["website_bv"] = globalVariables.Branch_Website;
                drData["email_bv"] = globalVariables.Branch_Email;
                drData["ten_phieu"] = ma_loaidvu == "PTTT" ? lst_ten_phieu[0] : (ma_loaidvu == "PHAUTHUAT" ? lst_ten_phieu[1] : lst_ten_phieu[2]);
                List<string> fieldNames = new List<string>();

                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\PHIEU_CAMKET_PTTT.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtData);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport("PHIEU_CAMKET_PTTT", ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu PTTT tại thư mục sau :" + PathDoc);
                    return "";
                }


                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return "";
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), "PHIEU_CAMKET_PTTT", objLuotkham.MaLuotkham, Utility.sDbnull(objpttt.IdPhieu), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return "";
                    }
                    if (builder.MoveToMergeField("logo") && globalVariables.SysLogo != null)
                        if (sysLogosize != null)
                        {
                            int w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            int h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(globalVariables.SysLogo, w, h);
                            else
                                builder.InsertImage(globalVariables.SysLogo);
                        }
                        else
                            if (globalVariables.SysLogo != null)
                            builder.InsertImage(globalVariables.SysLogo);

                    byte[] NoImage = Utility.fromimagepath2byte(AppDomain.CurrentDomain.BaseDirectory + "Noimage\\Noimage.png");
                    if (builder.MoveToMergeField("anh1"))
                    {
                        byte[] myimage = null;

                        if (objpttt != null && objpttt.MaHinhanh != null)
                        {
                            if (objpttt.MaHinhanh == "0" || objpttt.MaHinhanh == null)
                            {
                                myimage = null;
                            }
                            else //if (objpttt.MaHinhanh == "1")
                            {
                                myimage = Utility.fromimagepath2byte(Application.StartupPath + string.Format(@"\Hinhanh_PTTT\pttt0{0}.png", objpttt.MaHinhanh));
                            }
                            //else if (objpttt.MaHinhanh == "2")
                            //{
                            //    myimage = Utility.fromimagepath2byte(Application.StartupPath + @"\Hinhanh_PTTT\pttt02.png");
                            //}
                            //else if (objpttt.MaHinhanh == "3")
                            //{
                            //    myimage = Utility.fromimagepath2byte(Application.StartupPath + @"\Hinhanh_PTTT\pttt03.png");
                            //}

                        }
                        if (myimage != null)
                            builder.InsertImage(myimage);
                        else
                            builder.InsertImage(new List<byte>().ToArray(), 10, 10);
                    }
                    else
                    {
                        if (builder.MoveToMergeField("anh1"))
                            builder.InsertImage(NoImage, 10, 10);
                    }
                    doc.MailMerge.Execute(drData);
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Pdf);
                    return fileKetqua;

                    //if (File.Exists(path))
                    //{
                    //    Process process = new Process();
                    //    try
                    //    {
                    //        process.StartInfo.FileName = path;
                    //        process.Start();
                    //        process.WaitForInputIdle();
                    //    }
                    //    catch
                    //    {
                    //    }
                    //}
                }
                else
                {
                    MessageBox.Show("Không tìm thấy biểu mẫu", "TThông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                return "";
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return "";
            }
        }
        private string InPhieuTuongTrinhPTTT(DataTable dtData, KcbPhieupttt objpttt, string ma_loaidvu)
        {
            try
            {

                long ID_PHIEUPTTT = Utility.Int64Dbnull(objpttt.IdPhieu);
                dtData.TableName = "kcb_phieu_pttt";
                Utility.AddColums2DataTable(ref dtData, new List<string>() { "thogian_vaovien", "thoigian_batdau_phauthuat", "thoigian_ketthuc_phauthuat" }, typeof(string));
                List<string> lst_ten_phieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("PTTT_TENPHIEU_TUONGTRINH", "PHIẾU TƯỜNG TRÌNH PHẪU THUẬT@PHIẾU TƯỜNG TRÌNH THỦ THUẬT", true).Split('@').ToList<string>();
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                drData["SDT_bv"] = globalVariables.Branch_Phone;
                drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                drData["Fax_bv"] = globalVariables.Branch_Fax;
                drData["website_bv"] = globalVariables.Branch_Website;
                drData["email_bv"] = globalVariables.Branch_Email;
                drData["ten_phieu"] = ma_loaidvu == "PTTT" ? lst_ten_phieu[0] : (ma_loaidvu == "PHAUTHUAT" ? lst_ten_phieu[1] : lst_ten_phieu[2]);
                drData["thogian_vaovien"] = Utility.FormatDateTime_giophut_ngay_thang_nam(objLuotkham.NgayNhapvien, "");
                drData["thoigian_batdau_phauthuat"] = Utility.FormatDateTime_giophut_ngay_thang_nam(objpttt.NgayPttt, "Từ");
                drData["thoigian_ketthuc_phauthuat"] = Utility.FormatDateTime_giophut_ngay_thang_nam(objpttt.NgayKetthuc, "Đến");
                List<string> fieldNames = new List<string>();


                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\PHIEU_TUONGTRINH_PTTT.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtData);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport("PHIEU_TUONGTRINH_PTTT", ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu PTTT tại thư mục sau :" + PathDoc);
                    return "";
                }


                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return "";
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), "PHIEU_TUONGTRINH_PTTT", objLuotkham.MaLuotkham, Utility.sDbnull(ID_PHIEUPTTT), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));

                int w = 100;
                int h = 100;
                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo");
                        return "";
                    }
                    if (builder.MoveToMergeField("logo") && globalVariables.SysLogo != null)
                        if (sysLogosize != null)
                        {
                            w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(globalVariables.SysLogo, w, h);
                            else
                                builder.InsertImage(globalVariables.SysLogo);
                        }
                        else
                            if (globalVariables.SysLogo != null)
                            builder.InsertImage(globalVariables.SysLogo);
                    byte[] NoImage = Utility.fromimagepath2byte(AppDomain.CurrentDomain.BaseDirectory + "Noimage\\Noimage.png");
                    sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("PTTTsize").ExecuteSingle<SysSystemParameter>();
                    if (builder.MoveToMergeField("anh1"))
                    {
                        byte[] myimage = null;
                        w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                        h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                        if (objpttt != null && objpttt.MaHinhanh != null)
                        {
                            if (objpttt.MaHinhanh == "0" || objpttt.MaHinhanh == null)
                            {
                                myimage = null;
                            }
                            else //if (objpttt.MaHinhanh == "1")
                            {
                                myimage = Utility.fromimagepath2byte(Application.StartupPath + string.Format(@"\Hinhanh_PTTT\pttt0{0}.png", objpttt.MaHinhanh));
                            }
                            //else if (objpttt.MaHinhanh == "2")
                            //{
                            //    myimage = Utility.fromimagepath2byte(Application.StartupPath + @"\Hinhanh_PTTT\pttt02.png");
                            //}
                            //else if (objpttt.MaHinhanh == "3")
                            //{
                            //    myimage = Utility.fromimagepath2byte(Application.StartupPath + @"\Hinhanh_PTTT\pttt03.png");
                            //}

                        }
                        if (myimage != null)
                            builder.InsertImage(myimage, w, h);
                        else
                            builder.InsertImage(new List<byte>().ToArray(), 10, 10);
                    }
                    else
                    {
                        if (builder.MoveToMergeField("anh1"))
                            builder.InsertImage(NoImage, 10, 10);
                    }
                    doc.MailMerge.Execute(drData);
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Pdf);
                    return fileKetqua;

                    //if (File.Exists(path))
                    //{
                    //    Process process = new Process();
                    //    try
                    //    {
                    //        process.StartInfo.FileName = path;
                    //        process.Start();
                    //        process.WaitForInputIdle();
                    //    }
                    //    catch
                    //    {
                    //    }
                    //}
                }
                else
                {
                    MessageBox.Show("Không tìm thấy biểu mẫu", "TThông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                return "";
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return "";
            }

        }
        #endregion
        DataTable dtData = new DataTable();
        private void UcThongtinnguoibenh_emr_basic1__OnEnterMe()
        {
            isAllowSelectionChanged = false;
            if (optPdfView.Checked)
                pdfViewer1.CloseDocument();
            else
                richEdit.CreateNewDocument();
            if (ucThongtinnguoibenh_emr_basic1.objLuotkham != null)
            {
                objLuotkham = ucThongtinnguoibenh_emr_basic1.objLuotkham;
                _baseDirectoryRIS = string.Format(@"{0}\EMR_DOCUMENTS\{1}\pdfRIS", Application.StartupPath, objLuotkham.MaLuotkham);
                _baseDirectoryLIS = string.Format(@"{0}\EMR_DOCUMENTS\{1}\pdfLIS", Application.StartupPath, objLuotkham.MaLuotkham);
                dtData = SPs.EmrLaydanhsachDocuments(objLuotkham.MaLuotkham, -1, globalVariables.UserName, Utility.ByteDbnull(globalVariables.IsAdmin || globalVariables.isSuperAdmin || Utility.Coquyen("EMR_FULL") ? 1 : 0),"").GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdList, dtData, true, true, "1=1", "");
                isAllowSelectionChanged = true;
            }
        }

        void _CheckedChanged(object sender, EventArgs e)
        {

        }
        private void InitFtp()
        {
            try
            {
                List<string> FTPInfor = THU_VIEN_CHUNG.Laygiatrithamsohethong("FTP_PDFRIS", string.Format("{0}-{1}-{2}", "127.0.0.1", "pdf2his", "pdf2his"), true).Split('-').ToList<string>();

                FtpClientRIS = new FTPclient(FTPInfor[0], FTPInfor[1], FTPInfor[2]);
                FtpClientRIS.UsePassive = true;
                FtpClientCurrentDirectoryRIS = FtpClientRIS.CurrentDirectory;
                if (!Directory.Exists(_baseDirectoryRIS))
                {
                    Directory.CreateDirectory(_baseDirectoryRIS);
                }
                FTPInfor = THU_VIEN_CHUNG.Laygiatrithamsohethong("FTP_PDFLIS", string.Format("{0}-{1}-{2}", "127.0.0.1", "pdf2his", "pdf2his"), true).Split('-').ToList<string>();
                FtpClientLIS = new FTPclient(FTPInfor[0], FTPInfor[1], FTPInfor[2]);
                FtpClientLIS.UsePassive = true;
                FtpClientCurrentDirectoryLIS = FtpClientLIS.CurrentDirectory;
                if (!Directory.Exists(_baseDirectoryLIS))
                {
                    Directory.CreateDirectory(_baseDirectoryLIS);
                }
            }
            catch
            {
            }
        }
        void SearchData()
        {

        }

        public static string[] GetFiles(string ftpServer, NetworkCredential Credentials, SearchOption searchOption)
        {
            var request = (FtpWebRequest)WebRequest.Create(ftpServer);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.Credentials = Credentials;
            List<string> files = new List<string>();
            using (var response = (FtpWebResponse)request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    var reader = new System.IO.StreamReader(responseStream);
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line) == false)
                        {
                            if (!line.Contains("<DIR>"))
                            {
                                string[] details = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                                string file = line.Replace(details[0], "")
                                    .Replace(details[1], "")
                                    .Replace(details[2], "")
                                    .Trim();
                                files.Add(file);
                            }
                            else
                            {
                                if (searchOption == SearchOption.AllDirectories)
                                {
                                    string dirName = line.Split(
                                            new string[] { "<DIR>" },
                                            StringSplitOptions.RemoveEmptyEntries
                                            ).Last().Trim();
                                    string dirFullName = String.Format("{0}/{1}", ftpServer.Trim('/'), dirName);
                                    files.AddRange(GetFiles(dirFullName, Credentials, searchOption));
                                }
                            }
                        }
                    }
                }
            }
            return files.ToArray();
        }
        void LoadPdf(string ngay_chidinh, string ma_chidnh, FTPclient ftp, string baseDirectory, string FtpDir2Scan, string fileName)
        {
            this.Cursor = Cursors.WaitCursor;
            GC.Collect();
            string oldTitle = this.Text;
            try
            {
                if (!Directory.Exists(baseDirectory))
                    Directory.CreateDirectory(baseDirectory);

                string ftpPath = FtpDir2Scan + ngay_chidinh + "/" + ma_chidnh;
                if (ftp.FtpFileExists(ftpPath))
                {
                    List<string> lstFiles = GetFiles(ftp.Hostname, new NetworkCredential(ftp.Username, ftp.Password), SearchOption.AllDirectories).ToList<string>();
                }


            }
            catch (Exception ex1)
            {
                // Utility.ShowMsg(ex1.Message);

            }
            finally
            {

                this.Cursor = Cursors.Default;
                this.Text = oldTitle;
            }
        }
        private string pathPdf = "";


        private void frm_Emr_Load(object sender, EventArgs e)
        {
            try
            {

                LoadUserConfigs();
                DataTable dtPhieuEMR = new Select("*").From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("EMR_GAYBA")
             .OrderAsc(DmucChung.Columns.SttHthi)
             .ExecuteDataSet().Tables[0];
                DataBinding.BindDataCombobox(cboGay, dtPhieuEMR, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                if (isAutoLoad)
                    ucThongtinnguoibenh_emr_basic1.Refresh();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void btnOpenPDF_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = @"PDF Files(*.pdf) |*.pdf;";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != null)
            {
                pdfViewer1.LoadDocument(openFileDialog.FileName);
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            //axAcroPDF.gotoFirstPage();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            //axAcroPDF.gotoPreviousPage();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //axAcroPDF.gotoNextPage();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            //axAcroPDF.gotoLastPage();
        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            //axAcroPDF.setCurrentPage(Convert.ToInt32(nudPage.Value));
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }
        FileStream stream;
        private void grdThongTin_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (stream != null)
                {
                    stream.Close();
                    stream = null;
                }
                if (!Utility.isValidGrid(grdList)) return;
                string fileName = grdList.GetValue("duongdan_file").ToString();
                string ma_nhom = grdList.GetValue("ma_nhom").ToString();
                string localFile = "";
                string ftpFile = "";
                if (ma_nhom == "XN")
                {
                    localFile = string.Format(@"{0}{1}", _baseDirectoryLIS, fileName.Replace(@"/", @"\"));
                    ftpFile = string.Format(@"{0}{1}", FtpClientCurrentDirectoryLIS, fileName);
                }
                else
                {
                    localFile = string.Format(@"{0}{1}", _baseDirectoryRIS, fileName.Replace(@"/", @"\"));
                    ftpFile = string.Format(@"{0}{1}", FtpClientCurrentDirectoryRIS, fileName);
                }
                string parentFolder = Path.GetDirectoryName(localFile);
                Utility.Try2CreateFolder(Directory.GetParent(parentFolder).FullName);
                Utility.Try2CreateFolder(parentFolder);
                if (File.Exists(localFile))
                {
                    if (chkForced2Download.Checked)
                    {
                        if (ma_nhom == "XN")
                            FtpClientLIS.Download(ftpFile, localFile, true);
                        else
                            FtpClientRIS.Download(ftpFile, localFile, true);
                    }
                }
                else//Download and open
                {
                    if (ma_nhom == "XN")
                        FtpClientLIS.Download(ftpFile, localFile, true);
                    else
                        FtpClientRIS.Download(ftpFile, localFile, true);
                }
                string Url = string.Format("{0}?zoom=100%#navpanes=1&toolbar=1", localFile);
                this.Text = string.Format("Xem kết quả PDF từ file: {0}", Url);
                if (File.Exists(localFile))
                {
                    if (stream == null)
                        stream = new FileStream(localFile, FileMode.Open);
                    pdfViewer1.LoadDocument(stream);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            // webBrowser1.Navigate(string.Format("{0}#zoom={1}%&navpanes=1&toolbar=1",pathPdf, numericUpDown1.Text)); 
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //DataTable dt = SPs.KetquaLaydanhsachFile(txtMaluotkham.Text).GetDataSet().Tables[0];
            //Utility.SetDataSourceForDataGridEx(grdKQ, dt, true, true, "", "");
        }
        void LoadUserConfigs()
        {
            try
            {
                chkForced2Download.Checked = Utility.getUserConfigValue(chkForced2Download.Tag.ToString(), Utility.Bool2byte(chkForced2Download.Checked)) == 1;

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void SaveUserConfigs()
        {
            try
            {
                Utility.SaveUserConfig(chkForced2Download.Tag.ToString(), Utility.Bool2byte(chkForced2Download.Checked));

            }

            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void frm_Emr_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                SaveUserConfigs();
                if (stream != null)
                {
                    stream.Close();
                    stream = null;
                }
                if (pdfViewer1 != null)
                {

                    pdfViewer1 = null;
                }
            }
            catch (Exception ex)
            {


            }

        }

        private void frm_Emr_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.P) || e.KeyCode == Keys.F4) cmdPrint.PerformClick();

            else if (e.KeyCode == Keys.O && e.Control) cmdOpen.PerformClick();

        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdOpen_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "PDF Files|*.pdf";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    pdfViewer1.LoadDocument(ofd.FileName);
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }

        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                // webBrowser1.Print();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }

        }

        private void optDocView_CheckedChanged(object sender, EventArgs e)
        {
             richEdit.Visible = true;
            pdfViewer1.Visible = false;
        }

        private void optPdfView_CheckedChanged(object sender, EventArgs e)
        {
             richEdit.Visible = false;
            pdfViewer1.Visible = true;
        }

        private void cmdOpenDoc_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Word Documents (*.doc;*.docx)|*.doc;*.docx";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadWordFile(ofd.FileName, false);
            }
        }
        private void LoadWordFile(string filePath, bool isReadOnly)
        {
            try
            {
                //Mở file Word
                if (System.IO.File.Exists(filePath))
                {
                    var doc = new Aspose.Words.Document(filePath);
                    if (doc.ProtectionType != ProtectionType.NoProtection)
                    {
                        doc.Unprotect("Vms@123"); // nếu có mật khẩu
                    }

                    doc.MailMerge.Execute(ucThongtinnguoibenh_emr_basic1.dt_ThongtinNguoibenh.Rows[0]);
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    builder.MoveToBookmark("txtName"); // hoặc dùng MoveTo đoạn nào đó
                    builder.StartEditableRange();
                    builder.Write(" ");
                    builder.EndEditableRange();
                    builder.MoveToBookmark("txtAge"); // hoặc dùng MoveTo đoạn nào đó
                    builder.StartEditableRange();
                    builder.Write(" ");
                    builder.EndEditableRange();
                    // Khóa phần còn lại
                    doc.Protect(ProtectionType.ReadOnly, "Vms@123");

                    //// 3. Khóa lại sau khi merge
                    doc.Protect(ProtectionType.AllowOnlyFormFields, "Vms@123");
                    string newfilePath = string.Format(@"{0}\{1}.doc", Path.GetDirectoryName(filePath), Guid.NewGuid().ToString());
                    //// 4. Lưu file
                    doc.Save(newfilePath);
                    // Tải tài liệu
                    if (Path.GetExtension(newfilePath).Equals(".docx", StringComparison.OrdinalIgnoreCase))
                    {
                        richEdit.LoadDocument(newfilePath, DocumentFormat.OpenXml);
                    }
                    else
                    {
                        richEdit.LoadDocument(newfilePath);
                    }

                    // Đặt chế độ: Đọc hoặc chỉnh sửa
                    //richEdit.ReadOnly = isReadOnly;

                    // Nếu read-only, ẩn caret và disable chỉnh sửa
                    //if (isReadOnly)
                    //{
                    //    richEdit.ActiveViewType = RichEditViewType.Simple;
                    //    richEdit.Options.Behavior.ShowPopupMenu = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
                    //}
                    richEdit.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.PrintLayout;
                    richEdit.ActiveView.ZoomFactor = 1.0f; // 100%
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tệp Word.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
           
        }
        private void ProtectSelection()
        {

        }
        private void optReadOnly_CheckedChanged(object sender, EventArgs e)
        {
            richEdit.ReadOnly = optReadOnly.Checked; 
        }

        private void optEdit_CheckedChanged(object sender, EventArgs e)
        {
             richEdit.ReadOnly = optReadOnly.Checked; 
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            var document = richEdit.Document;
            document.Fields.Create(document.CaretPosition, string.Format("MERGEFIELD {0}", comboBox1.Text));
            document.Fields.Update();
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            richEdit.SaveDocument(string.Format(@"D:\Temp\{0}",Guid.NewGuid().ToString("N")), DocumentFormat.Doc);
        }

        private void cmdSign_Click(object sender, EventArgs e)
        {
            if (stream != null)
            {
                stream.Close();
                stream = null;
            }
            pdfViewer1.CloseDocument();
            string loaiphieuhis = "";
            string reportcode = "";
            long IdPhieu = -1;
            //Bắt đầu xử lý sinh lại file dựa vào loại phiếu HIS và report_code
            EmrDocument objDoc = EmrDocument.FetchByID(Utility.Int64Dbnull(currRow.Cells[EmrDocument.Columns.IdFile].Value));
            if (objDoc != null)
            {
                IdPhieu = Utility.Int64Dbnull(objDoc.IdPhieu);// Utility.Int64Dbnull(currRow.Cells[EmrDocument.Columns.IdPhieu].Value);
                loaiphieuhis = objDoc.LoaiPhieuHis;
                reportcode = objDoc.ReportCode;
            }
            else
            {
                loaiphieuhis = Utility.sDbnull(currRow.Cells[EmrDocument.Columns.LoaiPhieuHis].Value);
                reportcode = Utility.sDbnull(currRow.Cells[EmrDocument.Columns.ReportCode].Value);
            }
            nguoi_tao = Utility.sDbnull(currRow.Cells[EmrDocument.Columns.NguoiTao].Value);

            DataTable v_dtData;
            SysReport objReport;
            string pdfFileName = "";
            if (loaiphieuhis == Loaiphieu_HIS.PHIEUDIEUTRI)
            {
                pdfFileName = string.Format(@"{0}\EMR_DOCUMENTS\{1}\{2}\{3}_{4}.pdf", Application.StartupPath, objLuotkham.MaLuotkham, loaiphieuhis, IdPhieu, reportcode);
                InphieuDieutri(pdfFileName, IdPhieu.ToString(), true);
            }
            pdfViewer1.CloseDocument();
            if (pdfFileName != "" && File.Exists(pdfFileName))
            {
                if (stream == null)
                    stream = new FileStream(pdfFileName, FileMode.Open);
                pdfViewer1.LoadDocument(stream);
            }
        }

        private void mnuAnPhieu_Click(object sender, EventArgs e)
        {
            cmdAn.PerformClick();

        }

        private void cmdKidientu_Click(object sender, EventArgs e)
        {
            try
            {
                List<long> lstIdPhieu_Other = (from p in grdList.GetCheckedRows() where Utility.sDbnull(p.Cells["nguoi_tao"].Value) != globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                List<long> lstIdPhieu_Daky = (from p in grdList.GetCheckedRows() where Utility.sDbnull(p.Cells["tthai_kyso"].Value) == "1" && Utility.sDbnull(p.Cells["nguoi_tao"].Value) == globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                List<long> lstIdPhieu_ChuaKy = (from p in grdList.GetCheckedRows() where Utility.sDbnull(p.Cells["tthai_kyso"].Value) == "0" && Utility.sDbnull(p.Cells["nguoi_tao"].Value) == globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                if (lstIdPhieu_Other.Count > 0)
                {
                    Utility.ShowMsg("Bạn đang chọn lẫn các phiếu của người khác để ký(Có thể do bạn đang có Full quyền xem EMR nên nhìn thấy phiếu của người khác).\nChú ý: Tất cả các phiếu lẫn này hệ thống sẽ loại bỏ không ký.\nNhấn OK để tiếp tục");
                }
                if (lstIdPhieu_Daky.Count > 0)
                {
                    Utility.ShowMsg("Bạn đang chọn lẫn cả các phiếu đã ký.\nChú ý: Tất cả các phiếu lẫn này hệ thống sẽ không tác động cập nhật lại trạng thái.\nNhấn OK để tiếp tục");
                }
                int num = 0;
                if (lstIdPhieu_ChuaKy.Count > 0)
                {
                    if (Utility.AcceptQuestion("Bạn có chắc chắn muốn ký điện tử cho các phiếu đang chọn. Sau khi ký điện tử xong, bạn sẽ không được quyền sửa, xóa phiếu đó trong toàn bộ hệ thống HIS/EMR", "Xác nhận", true))
                    {
                        foreach (GridEXRow _row in grdList.GetCheckedRows())
                        {
                            if (Utility.sDbnull(_row.Cells["tthai_kyso"].Value) == "0" && Utility.sDbnull(_row.Cells["nguoi_tao"].Value) == globalVariables.UserName)
                            {
                                long id_phieu = Utility.Int64Dbnull(_row.Cells[EmrDocument.Columns.IdPhieu].Value);
                                string LoaiPhieuHis = Utility.sDbnull(_row.Cells[EmrDocument.Columns.LoaiPhieuHis].Value);
                                num += SPs.EmrThaydoitrangthai(id_phieu, LoaiPhieuHis, 2, true, globalVariables.UserName, DateTime.Now).Execute();
                                //Bản chất chỉ đánh dấu phiếu đã được kí để ngăn hủy, xóa hoặc bắt chặt thao tác trên HIS.
                                ////Khi nào đóng hồ sơ bệnh án sẽ đẩy PDF chính thức lên server và update đường dẫn file pdf vào emr documents để phục vụ tra cứu và lưu trữ
                            }
                        }
                        if (num > 0)
                        {
                            Utility.ShowMsg("Đã ký các phiếu thành công. Nhấn OK để kết thúc");
                        }
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }
        private void cmdHuyKyDientu_Click(object sender, EventArgs e)
        {
            try
            {
                List<long> lstIdPhieu_Other = (from p in grdList.GetCheckedRows() where Utility.sDbnull(p.Cells["nguoi_tao"].Value) != globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                List<long> lstIdPhieu_Daky = (from p in grdList.GetCheckedRows() where Utility.sDbnull(p.Cells["tthai_kyso"].Value) == "1" && Utility.sDbnull(p.Cells["nguoi_tao"].Value) == globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                List<long> lstIdPhieu_ChuaKy = (from p in grdList.GetCheckedRows() where Utility.sDbnull(p.Cells["tthai_kyso"].Value) == "0" && Utility.sDbnull(p.Cells["nguoi_tao"].Value) == globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                if (lstIdPhieu_Other.Count > 0)
                {
                    Utility.ShowMsg("Bạn đang chọn lẫn các phiếu của người khác để hủy ký(Có thể do bạn đang có Full quyền xem EMR nên nhìn thấy phiếu của người khác).\nChú ý: Tất cả các phiếu lẫn này hệ thống sẽ loại bỏ không hủy ký.\nNhấn OK để tiếp tục");
                }
                if (lstIdPhieu_ChuaKy.Count > 0)
                {
                    Utility.ShowMsg("Bạn đang chọn lẫn cả các phiếu chưa ký.\nChú ý: Tất cả các phiếu lẫn này hệ thống sẽ không tác động cập nhật lại trạng thái.\nNhấn OK để tiếp tục");
                }
                int num = 0;
                if (lstIdPhieu_Daky.Count > 0)
                {
                    if (Utility.AcceptQuestion("Bạn có chắc chắn muốn HỦY ký điện tử cho các phiếu đang chọn. Sau khi Hủy ký điện tử xong, bạn sẽ có thể được quyền sửa, xóa phiếu đó trong hệ thống HIS/EMR", "Xác nhận", true))
                    {
                        foreach (GridEXRow _row in grdList.GetCheckedRows())
                        {
                            if (Utility.sDbnull(_row.Cells["tthai_kyso"].Value) == "1" && Utility.sDbnull(_row.Cells["nguoi_tao"].Value) == globalVariables.UserName)
                            {
                                long id_phieu = Utility.Int64Dbnull(_row.Cells[EmrDocument.Columns.IdPhieu].Value);
                                string LoaiPhieuHis = Utility.sDbnull(_row.Cells[EmrDocument.Columns.LoaiPhieuHis].Value);
                                num += SPs.EmrThaydoitrangthai(id_phieu, LoaiPhieuHis, 2, false, "", null).Execute();
                                //Bản chất chỉ đánh dấu phiếu đã được kí để ngăn hủy, xóa hoặc bắt chặt thao tác trên HIS.
                                ////Khi nào đóng hồ sơ bệnh án sẽ đẩy PDF chính thức lên server và update đường dẫn file pdf vào emr documents để phục vụ tra cứu và lưu trữ
                            }
                        }
                        if (num > 0)
                        {
                            Utility.ShowMsg("Đã Hủy ký các phiếu thành công. Nhấn OK để kết thúc");
                        }
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }
        private void cmdChuyenGay_Click(object sender, EventArgs e)
        {
            List<string> lstIdFile = (from p in grdList.GetCheckedRows() select Utility.sDbnull(p.Cells[EmrDocument.Columns.IdFile].Value)).ToList<string>();
            if (lstIdFile.Count <= 0)
            {
                Utility.ShowMsg("Bạn cần chọn ít nhất 1 phiếu để thực hiện chuyển gáy");
                return;
            }
            frm_capnhat_gayEmr _capnhat_gayEmr = new frm_capnhat_gayEmr();
            if (_capnhat_gayEmr.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    int num = SPs.EmrCapnhatGay(string.Join(",", lstIdFile.ToArray<string>()), Utility.sDbnull(_capnhat_gayEmr.cboGay.SelectedValue)).Execute();
                    Utility.ShowMsg("Cập nhật gáy cho các phiếu đang chọn thành công. Nhấn OK để làm mới lại dữ liệu");

                    if (stream != null)
                    {
                        stream.Close();
                        stream = null;
                    }
                    if (pdfViewer1 != null)
                    {
                        pdfViewer1.CloseDocument();
                    }
                    ucThongtinnguoibenh_emr_basic1.Refresh();
                }
                catch (Exception ex)
                {
                    Utility.CatchException(ex);
                }
            }
        }

        private void cmdAn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("EMR_AN_PHIEU"))
                {
                    Utility.thongbaokhongcoquyen("EMR_AN_PHIEU", " ẩn phiếu");
                    return;
                }
                List<long> lstIdPhieu_Other = (from p in grdList.GetCheckedRows() where Utility.sDbnull(p.Cells["nguoi_tao"].Value) != globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                List<long> lstIdPhieu_DaAn = (from p in grdList.GetCheckedRows() where Utility.sDbnull(p.Cells["tthai_an"].Value) == "1" && Utility.sDbnull(p.Cells["nguoi_tao"].Value) == globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                List<long> lstIdPhieu_ChuaAn = (from p in grdList.GetCheckedRows() where Utility.sDbnull(p.Cells["tthai_an"].Value) == "0" && Utility.sDbnull(p.Cells["nguoi_tao"].Value) == globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                if (lstIdPhieu_Other.Count > 0)
                {
                    Utility.ShowMsg("Bạn đang chọn lẫn các phiếu của người khác để Ẩn (Có thể do bạn đang có Full quyền xem EMR nên nhìn thấy phiếu của người khác).\nChú ý: Tất cả các phiếu lẫn này hệ thống sẽ loại bỏ không ẩn.\nNhấn OK để tiếp tục");
                }
                if (lstIdPhieu_DaAn.Count > 0)
                {
                    Utility.ShowMsg("Bạn đang chọn lẫn cả các phiếu đã ẩn.\nChú ý: Tất cả các phiếu lẫn này hệ thống sẽ không tác động cập nhật lại trạng thái.\nNhấn OK để tiếp tục");
                }
                int num = 0;
                if (lstIdPhieu_ChuaAn.Count > 0)
                {
                    if (Utility.AcceptQuestion("Bạn có chắc chắn muốn Ẩn cho các phiếu đang chọn. Sau khi Ẩn xong, Những người dùng khác sẽ không nhìn thấy các phiếu này(Trừ Admin hoặc người có full Quyền xem hồ sơ EMR)", "Xác nhận", true))
                    {
                        foreach (GridEXRow _row in grdList.GetCheckedRows())
                        {
                            if (Utility.sDbnull(_row.Cells["tthai_an"].Value) == "0" && Utility.sDbnull(_row.Cells["nguoi_tao"].Value) == globalVariables.UserName)
                            {
                                long id_phieu = Utility.Int64Dbnull(_row.Cells[EmrDocument.Columns.IdPhieu].Value);
                                string LoaiPhieuHis = Utility.sDbnull(_row.Cells[EmrDocument.Columns.LoaiPhieuHis].Value);
                                num += SPs.EmrThaydoitrangthai(id_phieu, LoaiPhieuHis, 0, true, globalVariables.UserName, DateTime.Now).Execute();

                            }
                        }
                        if (num > 0)
                        {
                            Utility.ShowMsg("Đã ẩn các phiếu thành công. Nhấn OK để kết thúc");
                        }
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }

        private void cmdHienthi_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("EMR_HUY_AN_PHIEU"))
                {
                    Utility.thongbaokhongcoquyen("EMR_HUY_AN_PHIEU", " khôi phục các phiếu đã ẩn");
                    return;
                }
                List<long> lstIdPhieu_Other = (from p in grdList.GetCheckedRows() where Utility.sDbnull(p.Cells["nguoi_tao"].Value) != globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                List<long> lstIdPhieu_DaAn = (from p in grdList.GetCheckedRows() where Utility.sDbnull(p.Cells["tthai_an"].Value) == "1" && Utility.sDbnull(p.Cells["nguoi_tao"].Value) == globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                List<long> lstIdPhieu_ChuaAn = (from p in grdList.GetCheckedRows() where Utility.sDbnull(p.Cells["tthai_an"].Value) == "0" && Utility.sDbnull(p.Cells["nguoi_tao"].Value) == globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                if (lstIdPhieu_Other.Count > 0)
                {
                    Utility.ShowMsg("Bạn đang chọn lẫn các phiếu của người khác để Ẩn (Có thể do bạn đang có Full quyền xem EMR nên nhìn thấy phiếu của người khác).\nChú ý: Tất cả các phiếu lẫn này hệ thống sẽ loại bỏ không ẩn.\nNhấn OK để tiếp tục");
                }
                if (lstIdPhieu_ChuaAn.Count > 0)
                {
                    Utility.ShowMsg("Bạn đang chọn lẫn cả các phiếu chưa ẩn.\nChú ý: Tất cả các phiếu lẫn này hệ thống sẽ không tác động cập nhật lại trạng thái.\nNhấn OK để tiếp tục");
                }
                int num = 0;
                if (lstIdPhieu_DaAn.Count > 0)
                {
                    if (Utility.AcceptQuestion("Bạn có chắc chắn muốn Hiển thị các phiếu đang chọn. Sau khi Hiển thị xong, Những người dùng khác sẽ lại nhìn thấy các phiếu này", "Xác nhận", true))
                    {
                        foreach (GridEXRow _row in grdList.GetCheckedRows())
                        {
                            if (Utility.sDbnull(_row.Cells["tthai_an"].Value) == "1" && Utility.sDbnull(_row.Cells["nguoi_tao"].Value) == globalVariables.UserName)
                            {
                                long id_phieu = Utility.Int64Dbnull(_row.Cells[EmrDocument.Columns.IdPhieu].Value);
                                string LoaiPhieuHis = Utility.sDbnull(_row.Cells[EmrDocument.Columns.LoaiPhieuHis].Value);
                                num += SPs.EmrThaydoitrangthai(id_phieu, LoaiPhieuHis, 0, false, globalVariables.UserName, DateTime.Now).Execute();

                            }
                        }
                        if (num > 0)
                        {
                            Utility.ShowMsg("Đã Hiển thị lại các phiếu thành công. Nhấn OK để kết thúc");
                        }
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }

        private void cmdXoaphieu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("EMR_XOA_PHIEU"))
                {
                    Utility.thongbaokhongcoquyen("EMR_XOA_PHIEU", " xóa phiếu");
                    return;
                }
                List<long> lstIdPhieu_Other = (from p in grdList.GetCheckedRows() where Utility.sDbnull(p.Cells["nguoi_tao"].Value) != globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                List<long> lstIdPhieu_DaHuy = (from p in grdList.GetCheckedRows() where Utility.sDbnull(p.Cells["tthai_huy"].Value) == "1" && Utility.sDbnull(p.Cells["nguoi_tao"].Value) == globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                List<long> lstIdPhieu_ChuaHuy = (from p in grdList.GetCheckedRows() where Utility.sDbnull(p.Cells["tthai_huy"].Value) == "0" && Utility.sDbnull(p.Cells["nguoi_tao"].Value) == globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                if (lstIdPhieu_Other.Count > 0)
                {
                    Utility.ShowMsg("Bạn đang chọn lẫn các phiếu của người khác để Xóa (Có thể do bạn đang có Full quyền xem EMR nên nhìn thấy phiếu của người khác).\nChú ý: Tất cả các phiếu lẫn này hệ thống sẽ không tác động cập nhật lại trạng thái.\nNhấn OK để tiếp tục");
                }
                if (lstIdPhieu_DaHuy.Count > 0)
                {
                    Utility.ShowMsg("Bạn đang chọn lẫn cả các phiếu đã Hủy.\nChú ý: Tất cả các phiếu lẫn này hệ thống sẽ không tác động cập nhật lại trạng thái.\nNhấn OK để tiếp tục");
                }
                int num = 0;
                if (lstIdPhieu_ChuaHuy.Count > 0)
                {
                    if (Utility.AcceptQuestion("Bạn có chắc chắn muốn Hủy cho các phiếu đang chọn. Sau khi Hủy xong, Những người dùng khác sẽ không nhìn thấy các phiếu này(Trừ Admin hoặc người có full Quyền xem hồ sơ EMR)", "Xác nhận", true))
                    {
                        foreach (GridEXRow _row in grdList.GetCheckedRows())
                        {
                            if (Utility.sDbnull(_row.Cells["tthai_huy"].Value) == "0" && Utility.sDbnull(_row.Cells["nguoi_tao"].Value) == globalVariables.UserName)
                            {
                                long id_phieu = Utility.Int64Dbnull(_row.Cells[EmrDocument.Columns.IdPhieu].Value);
                                string LoaiPhieuHis = Utility.sDbnull(_row.Cells[EmrDocument.Columns.LoaiPhieuHis].Value);
                                num += SPs.EmrThaydoitrangthai(id_phieu, LoaiPhieuHis, 1, true, globalVariables.UserName, DateTime.Now).Execute();

                            }
                        }
                        if (num > 0)
                        {
                            Utility.ShowMsg("Đã Hủy các phiếu thành công. Nhấn OK để kết thúc");
                        }
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }

        private void cmdRestore_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("EMR_RESTORE_PHIEU"))
                {
                    Utility.thongbaokhongcoquyen("EMR_RESTORE_PHIEU", " khôi phục các phiếu đã xóa");
                    return;
                }
                List<long> lstIdPhieu_Other = (from p in grdList.GetCheckedRows() where Utility.sDbnull(p.Cells["nguoi_tao"].Value) != globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                List<long> lstIdPhieu_DaHuy = (from p in grdList.GetCheckedRows() where Utility.sDbnull(p.Cells["tthai_huy"].Value) == "1" && Utility.sDbnull(p.Cells["nguoi_tao"].Value) == globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                List<long> lstIdPhieu_ChuaHuy = (from p in grdList.GetCheckedRows() where Utility.sDbnull(p.Cells["tthai_huy"].Value) == "0" && Utility.sDbnull(p.Cells["nguoi_tao"].Value) == globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                if (lstIdPhieu_Other.Count > 0)
                {
                    Utility.ShowMsg("Bạn đang chọn lẫn các phiếu của người khác để khôi phục hủy (Có thể do bạn đang có Full quyền xem EMR nên nhìn thấy phiếu của người khác).\nChú ý: Tất cả các phiếu lẫn này hệ thống sẽ không tác động cập nhật lại trạng thái.\nNhấn OK để tiếp tục");
                }
                if (lstIdPhieu_ChuaHuy.Count > 0)
                {
                    Utility.ShowMsg("Bạn đang chọn lẫn cả các phiếu chưa Hủy.\nChú ý: Tất cả các phiếu lẫn này hệ thống sẽ không tác động cập nhật lại trạng thái.\nNhấn OK để tiếp tục");
                }
                int num = 0;
                if (lstIdPhieu_DaHuy.Count > 0)
                {
                    if (Utility.AcceptQuestion("Bạn có chắc chắn muốn khôi phục các phiếu đang chọn. Sau khi khôi phục xong, Những người dùng khác sẽ  nhìn thấy các phiếu này", "Xác nhận", true))
                    {
                        foreach (GridEXRow _row in grdList.GetCheckedRows())
                        {
                            if (Utility.sDbnull(_row.Cells["tthai_huy"].Value) == "1" && Utility.sDbnull(_row.Cells["nguoi_tao"].Value) == globalVariables.UserName)
                            {
                                long id_phieu = Utility.Int64Dbnull(_row.Cells[EmrDocument.Columns.IdPhieu].Value);
                                string LoaiPhieuHis = Utility.sDbnull(_row.Cells[EmrDocument.Columns.LoaiPhieuHis].Value);
                                num += SPs.EmrThaydoitrangthai(id_phieu, LoaiPhieuHis, 1, false, "", null).Execute();

                            }
                        }
                        if (num > 0)
                        {
                            Utility.ShowMsg("Đã khôi phục các phiếu Hủy thành công. Nhấn OK để kết thúc");
                        }
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }

        private void mnuHuyAnPhieu_Click(object sender, EventArgs e)
        {
            cmdHienthi.PerformClick();
        }

        private void mnuXoaPhieu_Click(object sender, EventArgs e)
        {
            cmdXoaphieu.PerformClick();
        }

        private void mnuHuyXoaPhieu_Click(object sender, EventArgs e)
        {
            cmdRestore.PerformClick();
        }

        private void cmdRestoreDefault_Gay_Click(object sender, EventArgs e)
        {
            List<string> lstIdFile = (from p in grdList.GetCheckedRows() select Utility.sDbnull(p.Cells[EmrDocument.Columns.IdFile].Value)).ToList<string>();
            if (lstIdFile.Count <= 0)
            {
                Utility.ShowMsg("Bạn cần chọn ít nhất 1 phiếu để thực hiện khôi phục về gáy theo phiếu in cấu hình trong hệ thống");
                return;
            }

            try
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn chuyển gáy cho các phiếu đang chọn về gáy theo phiếu in cấu hình trong hệ thống?", "Xác nhận", true))
                {
                    int num = SPs.EmrKhoiphucGaytheophieuin(string.Join(",", lstIdFile.ToArray<string>())).Execute();
                    Utility.ShowMsg("Khôi gáy cho các phiếu đang chọn về gáy theo phiếu in cấu hình trong hệ thống thành công. Nhấn OK để làm mới lại dữ liệu");

                    if (stream != null)
                    {
                        stream.Close();
                        stream = null;
                    }
                    if (pdfViewer1 != null)
                    {
                        pdfViewer1.CloseDocument();
                    }
                    ucThongtinnguoibenh_emr_basic1.Refresh();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }

        }

        private void cmdLaythongtin_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn cần chọn người bệnh trước khi thực hiện tạo hồ sơ EMR");
                return;
            }
            if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn hệ thống quét toàn bộ các phiếu của người bệnh {0}-{1} để làm hồ sơ EMR hay không?\nChú ý:Tính năng này chỉ nên được dùng đối với các ca phát sinh trước khi triển khai EMR hoặc có sự cố thiếu phiếu(Do nguyên nhân chủ quan, khách quan nào đó)", ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text, ucThongtinnguoibenh_emr_basic1.txtTenBN.Text), "Xác nhận tạo phiếu làm hồ sơ EMR", true))
            {
                int num = 0;
                StoredProcedure sp = SPs.EmrLaydanhsachDocumentsFromTables(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan,0, num);
                sp.Execute();
                sp.OutputValues.ForEach(delegate (object objOutput)
                {
                    num =Utility.Int32Dbnull( objOutput);
                });
                if (num > 0)
                    Utility.ShowMsg(string.Format("Đã đưa tổng số {0} phiếu quét được từ hệ thống liên quan đến người bệnh {1}-{2}. Nhấn OK để bắt đầu dựng hồ sơ EMR", num, ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text, ucThongtinnguoibenh_emr_basic1.txtTenBN.Text));
                isAllowSelectionChanged = false;
                if (optPdfView.Checked)
                    pdfViewer1.CloseDocument();
                else
                    richEdit.CreateNewDocument();
                 dtData = SPs.EmrLaydanhsachDocuments(objLuotkham.MaLuotkham, -1, globalVariables.UserName, Utility.ByteDbnull(globalVariables.IsAdmin || globalVariables.isSuperAdmin || Utility.Coquyen("EMR_FULL") ? 1 : 0),"").GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdList, dtData, true, true, "1=1", "");
                isAllowSelectionChanged = true;

            }
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn cần chọn người bệnh trước khi thực hiện tạo hồ sơ EMR");
                return;
            }
            if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn reset lại toàn bộ hồ sơ của người bệnh đang chọn hay không?\nChú ý: Các phiếu đã được Duyệt, Xác nhận, Ký số, Ký điện tử sẽ không bị ảnh hưởng", ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text, ucThongtinnguoibenh_emr_basic1.txtTenBN.Text), "Xác nhận reset hồ sơ EMR", true))
            {
                int num = 0;
                StoredProcedure sp = SPs.EmrLaydanhsachDocumentsFromTables(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, 1, num);
                sp.Execute();
               
                    Utility.ShowMsg(string.Format("Đã reset toàn bộ các phiếu liên quan đến người bệnh {0}-{1}. Nhấn OK để bắt đầu dựng hồ sơ EMR",  ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text, ucThongtinnguoibenh_emr_basic1.txtTenBN.Text));
                isAllowSelectionChanged = false;
                if (optPdfView.Checked)
                    pdfViewer1.CloseDocument();
                else
                    richEdit.CreateNewDocument();
                 dtData = SPs.EmrLaydanhsachDocuments(objLuotkham.MaLuotkham, -1, globalVariables.UserName, Utility.ByteDbnull(globalVariables.IsAdmin || globalVariables.isSuperAdmin || Utility.Coquyen("EMR_FULL") ? 1 : 0),"").GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdList, dtData, true, true, "1=1", "");
                isAllowSelectionChanged = true;
            }
        }

        private void uiButton4_Click(object sender, EventArgs e)
        {

        }

        private void cmdHosoKhac_Click(object sender, EventArgs e)
        {
               string sFilter = "All|*.doc;*.docx;*.xls;*.pdf;*.txt;*.xml;*.xlsx;*.jpg;*.PNG;*.BMP;*.Gif|doc|*.doc;*.docx|pdf|*.pdf|Excel|*.xls;*.xlsx|JPG|*.jpg|PNG|*.PNG|BMP|*.BMP|GIF|*.Gif";
        OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = sFilter;
            ofd.Title = "Chọn các hồ sơ lưu trữ kèm EMR";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                frm_emrfile_attatchments _emrfile_attatchments = new frm_emrfile_attatchments(objLuotkham, ofd.FileNames.ToList<string>());
              if(  _emrfile_attatchments.ShowDialog()==DialogResult.OK)
                {
                    isAllowSelectionChanged = false;
                    if (optPdfView.Checked)
                        pdfViewer1.CloseDocument();
                    else
                        richEdit.CreateNewDocument();

                    DataTable dtNewData = SPs.EmrLaydanhsachDocuments(objLuotkham.MaLuotkham, -1, globalVariables.UserName, Utility.ByteDbnull(globalVariables.IsAdmin || globalVariables.isSuperAdmin || Utility.Coquyen("EMR_FULL") ? 1 : 0), string.Join(",",_emrfile_attatchments.lstNewID)).GetDataSet().Tables[0];
                    foreach (DataRow drnew in dtNewData.Rows) dtData.ImportRow(drnew);
                    isAllowSelectionChanged = true;
                }    
            }
        }

        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemThongTin(true);
        }
        #region Tab danh sách người bệnh
        public string MaLuotkham = "";
        public string HovaTen = "";
        public int IdBenhnhan = -1;
        int id_doituongkcb = -1;
        public bool has_Cancel = true;
        public int DepartmentId = -1;
        public bool AutoSearch = false;
        string _args = "ALL";
        public byte noitrungoaitru = 100;
        public byte trangthai_noitru = 100;
        public string huongdieutri = "ALL"; //ALL,DTRI_NOITRU,DTRI_NGOAITRU
        string tungay = "";
        string denngay = "";
        string CMT = "";
        DateTime ngay_sinh;
        byte gioi_tinh = 100;
        string dien_thoai = "";
        int id_khoa = -1;
        void TimKiemThongTin(bool theongay)
        {
            try
            {
                id_doituongkcb = -1;// Utility.Int32Dbnull(cboObjectType.SelectedValue, -1);
                tungay= theongay ? (chkByDate.Checked ? dtmFrom.Value.ToString("dd/MM/yyyy") : "01/01/1900") : "01/01/1900";
                denngay = theongay ? (chkByDate.Checked ? dtmTo.Value.ToString("dd/MM/yyyy") : "01/01/1900") : "01/01/1900";
                CMT = Utility.sDbnull(txtCMT.Text);
                gioi_tinh = Utility.ByteDbnull(cboPatientSex.SelectedValue, 100);
                ngay_sinh = chkNgaysinh.Checked ? dtpNgaysinh.Value : new DateTime(1900, 1, 1);
                dien_thoai = Utility.sDbnull(txtDienthoai.Text);
                id_khoa = Utility.Int32Dbnull(cboKhoa.SelectedValue,-1);
                int Hos_status = -1;
                if (optNgoaiTru.Checked) Hos_status = 0;
                if (optNoiTru.Checked) Hos_status = 1;
                byte trangthai_dieutri = Utility.ByteDbnull(cboTrangthainoitru.SelectedValue, 100);
                DataTable mDtPatient = new KCB_DANGKY().KcbTimkiemDanhsachBenhnhan(tungay, denngay,
                                                     id_doituongkcb, Hos_status,
                                                     HovaTen, IdBenhnhan, MaLuotkham,
                                                    CMT, ngay_sinh, gioi_tinh,
                                                     dien_thoai, globalVariables.MA_KHOA_THIEN, 0,
                                                     trangthai_noitru, trangthai_dieutri,
                                                     Utility.sDbnull(this._args.Split('-')[0], huongdieutri));
                Utility.SetDataSourceForDataGridEx(grdPatient, mDtPatient, true, true, "1=1", KcbDanhsachBenhnhan.Columns.IdBenhnhan + " desc");
                grdPatient.MoveFirst();
                Utility.focusCell(grdPatient, KcbDanhsachBenhnhan.Columns.TenBenhnhan);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                // ModifyCommand();
            }
        }
        void NapTrangthaiDieutri()
        {
            DataTable dtTthai = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("TRANGTHAI_DIEUTRI").And(DmucChung.Columns.TrangThai).IsEqualTo(1).OrderAsc(DmucChung.Columns.SttHthi).ExecuteDataSet().Tables[0];
            DataBinding.BindDataCombobox(cboTrangthainoitru, dtTthai, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
            cboTrangthainoitru.SelectedValue = THU_VIEN_CHUNG.Laygiatrimacdinh(dtTthai); //cboTrangthai.Items.Count > 0 ? 0 : -1;
        }
        #endregion

        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtmTo.Enabled = dtmFrom.Enabled = chkByDate.Checked;
        }

        private void lnkClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtmTo.Value = dtmFrom.Value = globalVariables.SysDate;
            txtSoBA.Clear();
            txtSovaovien.Clear();
            txtPatient_ID.Clear();
            txtPatientCode.Clear();
        }
    }
    class HandleMergeBarcode : IFieldMergingCallback
    {
        public void FieldMerging(FieldMergingArgs e)
        {
            if (e.FieldName.ToUpper() == "MA_LUOTKHAM" || e.FieldName.ToUpper() == "MA_CHIDINH")
            {
                int resolution = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BARCODE_RESOLUTION", "300", false), 300);
                int Width = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BARCODE_WIDTH", "600", false), 600);
                int Height = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BARCODE_HEIGHT", "200", false), 200);

                string ma = e.FieldValue.ToString();
                if (!ma.Contains("*")) ma = string.Format("*{0}*", ma);
                //if (builder.MoveToMergeField(e.FieldName))
                //    builder.InsertImage(Utility.GetBarcodeDataLeadtools(ma));
                //int minWidth = CalculateMinimumWidth(ma);
                // Khởi tạo barcode
                Barcode barcode = new Barcode();

                barcode.IncludeLabel = true; // Hiện số mã bên dưới
                barcode.LabelFont = new System.Drawing.Font("Times New Roman", 12, System.Drawing.FontStyle.Bold); // Font của text dưới
                barcode.Alignment = AlignmentPositions.CENTER;
                barcode.Width = Width; // Chiều rộng (px)
                barcode.Height = Height; // Chiều cao (px)
                barcode.Encode(TYPE.CODE128B, ma);
                //Image img = barcode.Encode(TYPE.CODE128, ma, Color.Black, Color.White, minWidth, Height);
                // Tạo ảnh stream để chèn vào word
                using (MemoryStream stream = new MemoryStream())
                {
                    barcode.SaveImage(stream, SaveTypes.PNG);
                    stream.Position = 0;

                    // Chèn ảnh vào vị trí merge field
                    DocumentBuilder builder = new DocumentBuilder(e.Document);
                    builder.MoveToMergeField(e.FieldName);
                    builder.InsertImage(stream, Width, Height);

                    //// Tùy chỉnh kích thước ảnh
                    //image.Width = Width;  // nhỏ lại cho vừa
                    //image.Height = Height;
                }

                e.Text = ""; // Xóa text gốc (tránh bị lặp)
            }
        }
        public static int CalculateMinimumWidth(string data)
        {
            int modulePerChar = 11;              // mỗi ký tự chiếm ~11 module
            int startStopModules = 35;           // start + checksum + stop
            int quietZoneModules = 10;           // vùng trắng ở 2 đầu

            int totalModules = (data.Length * modulePerChar) + startStopModules + quietZoneModules;
            int pixelsPerModule = 1;

            return totalModules * pixelsPerModule;
        }
        public void ImageFieldMerging(ImageFieldMergingArgs e)
        {
            // Không dùng ở đây
        }
    }
    public class FileInfo
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileGroup { get; set; }
    }

    public class ResponseFile
    {
        public bool IsSuccess { get; set; }
        public string Messge { get; set; }
        public FileAudio data { get; set; }
    }

    public class FileAudio
    {

        // Properties
        public byte[] fileByte { get; set; }
        public string fileName { get; set; }
    }
}
