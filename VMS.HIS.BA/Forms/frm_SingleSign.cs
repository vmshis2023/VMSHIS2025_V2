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
using VMS.HIS.Bus.Emr;
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
using Aspose.Words.Markup;
using Aspose.Words.BuildingBlocks;
using VMS.HIS.EMR.Forms.BA_Phieukham.Ucs;
using VMS.HIS.EMR.Classes;
using VNS.HIS.UI.Forms.NGOAITRU;
using VMS.API.Libs;
using VNS.HIS.UI.NGOAITRU;

namespace VMS.HIS.UI.EMR
{
    public partial class frm_SingleSign : Form
    {
        KcbLuotkham objLuotkham;
       public bool isAutoLoad = false;
        bool isAllowSelectionChanged = false;
        bool isAllowSelectedIndexChanged = false;//Cbobox
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

        //Panel panelZoom;
        //TrackBar trackBarZoom;
        //Label lblZoom;
        Timer hideTimer;
        public frm_SingleSign(string docfile)
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
            grdEmrDocuments.SelectionChanged += grdEmrDocuments_SelectionChanged;
            grdEmrDocuments.MouseDoubleClick += GrdEmrDocuments_MouseDoubleClick;
            grdEmrDocuments.KeyDown += GrdEmrDocuments_KeyDown;
           
           
            SetDocView(true);
            PhanquyenTinhnang();
            cboLoaiphieuEmr.KeyDown += CboLoaiphieuEmr_KeyDown;
            cboLoaiphieuHIS.KeyDown += CboLoaiphieuHIS_KeyDown;
            txtNguoiKy._OnEnterMe += TxtNguoiKy__OnEnterMe;

           
            panelZoom.BackColor = Color.FromArgb(150, 0, 0, 0);


            trackBarZoom.Minimum = 10;  // 10%
                trackBarZoom.Maximum = 500; // 500%
            trackBarZoom.Value = 100;   // 100% mặc định
            trackBarZoom.TickFrequency = 10;

            trackBarZoom.Scroll += TrackBarZoom_Scroll;
                lblZoom.ForeColor = Color.White;
                lblZoom.AutoSize = true;
            lblZoom.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            lblZoom.BackColor = Color.Transparent;
      
            lblZoom.Text = "100%";
            //panelZoom.Controls.Add(lblZoom);

            // ===== Timer ẩn panel =====
            hideTimer = new Timer { Interval = 1500 };
            hideTimer.Tick += (s, e) =>
            {
                panelZoom.Visible = false;
                hideTimer.Stop();
            };

        }
        // Xử lý zoom khi kéo TrackBar
        private void TrackBarZoom_Scroll(object sender, EventArgs e)
        {
            // Zoom nội dung RichEditControl
            richEdit.ActiveView.ZoomFactor = trackBarZoom.Value / 100f;

            // Cập nhật % zoom hiển thị
            lblZoom.Text = $"{trackBarZoom.Value}%";
        }

        // Hiện panel khi chuột gần đáy RichEditControl
        private void RichEditControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Y > richEdit.Height - 80) // gần đáy
            {
                panelZoom.Visible = true;
                hideTimer.Stop(); // dừng hẳn đếm ẩn
            }
            else
            {
                // Nếu chuột không ở đáy thì bắt đầu đếm ẩn
                if (panelZoom.Visible && !hideTimer.Enabled)
                    hideTimer.Start();
            }
        }
     
        private void GrdDocs_SelectionChanged(object sender, EventArgs e)
        {
          
        }

       
        private void TxtNguoiKy__OnEnterMe()
        {
            globalVariablesPrivate.objNhanvien = DmucNhanvien.FetchByID(Utility.Int32Dbnull(txtNguoiKy.MyID));
            LoadAnhChuKy();
            globalVariables.UserName = txtNguoiKy.MyCode;
            globalVariables.gv_strTenNhanvien = txtNguoiKy.Text;
            InitTitle();
        }
        void InitTitle()
        {
           // this.Text = string.Format("Quản lý hồ sơ Bệnh án điện từ EMR - Xin chào người dùng {0} - {1}",globalVariables.UserName, globalVariables.gv_strTenNhanvien);
        }    
        private void GrdEmrDocuments_KeyDown(object sender, KeyEventArgs e)
        {
           if(e.KeyCode==Keys.Enter)
                grdEmrDocuments_SelectionChanged(grdEmrDocuments, new EventArgs());
        }

        private void GrdEmrDocuments_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            isLinkClicked = false;
            grdEmrDocuments_SelectionChanged(grdEmrDocuments, new EventArgs());
        }

        private void CboLoaiphieuHIS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.X)
                cboLoaiphieuHIS.SelectedIndex = 0;
        }

        private void CboLoaiphieuEmr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.X)
                cboLoaiphieuEmr.SelectedIndex = 0;
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

            cmdReset.Enabled = Utility.Coquyen("EMR_RESET");
            mnuPT01.Enabled = Utility.Coquyen("EMR_PT01");
            mnuPT02.Enabled = Utility.Coquyen("EMR_PT02");
            mnuPT03.Enabled = Utility.Coquyen("EMR_PT03");
            mnuPT04.Enabled = Utility.Coquyen("EMR_PT04");
            mnuPT05.Enabled = Utility.Coquyen("EMR_PT05");
            mnuPT06.Enabled = Utility.Coquyen("EMR_PT06");
            mnuPT07.Enabled = Utility.Coquyen("EMR_PT07");
            mnuPT08.Enabled = Utility.Coquyen("EMR_PT08");
            mnuPT09.Enabled = Utility.Coquyen("EMR_PT09");
            mnuPT10.Enabled = Utility.Coquyen("EMR_PT10");
            mnuPT11.Enabled = Utility.Coquyen("EMR_PT11");
            mnuPT12.Enabled = Utility.Coquyen("EMR_PT12");

            mnu01BV_BANoikhoa.Enabled= Utility.Coquyen("EMR_01BV_BENHAN_NOIKHOA");
            mnu10BV_BANgoaikhoa.Enabled = Utility.Coquyen("EMR_10BV_BENHAN_NGOAIKHOA");
            mnu15BV_BANgoaitru.Enabled = Utility.Coquyen("EMR_15BV_BENHAN_NGOAITRU");
        }
    
        private void InitializePdfToolbar()
        {
            // Tạo BarManager và gán Form trước khi gọi CreateBars
            var manager = new BarManager();
            manager.Form = this;
            //this.components.Add(manager); // thêm vào components

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
        void ResetView()
        {
            if (stream != null)
            {
                stream.Close();
                stream = null;
            }
            if (pdfViewer1 != null)
            {
                pdfViewer1.CloseDocument();
                return;
            }
            if(richEdit!=null)
            {
                richEdit.CreateNewDocument();
            }    
        }
     
        GridEXRow currRow = null;
        string nguoi_tao = "";
        SysSystemParameter sysSignsize = null;
       void LoadSignInfor()
        {
            try
            {
                flowSignInfor.Controls.Clear();
                flowSignInfor.SuspendLayout();
               // List<string> lstNguoiKy = globalVariables.dtSignInfor.AsEnumerable().Select(c => Utility.sDbnull(c["ten_nguoiky"])).Distinct().ToList<string>();
                List<string> lstNguoiKy = new List<string>();
                foreach (DataRow dr in globalVariables.dtSignInfor.Rows)
                {
                    string ttin_ky =string.Format("{0}@{1}", Utility.sDbnull(dr["nguoi_ky"]), Utility.sDbnull(dr["ten_vitri_ky"]));
                    string nguoiky = Utility.sDbnull(dr["nguoi_ky"]);
                    if (!lstNguoiKy.Contains(ttin_ky))
                    {
                        lstNguoiKy.Add(nguoiky);
                        ucNguoiKy _nguoiky = new ucNguoiKy(dr);
                        _nguoiky._OnClickMe += _nguoiky__OnClickMe;
                        flowSignInfor.Controls.Add(_nguoiky);
                    }
                }
                flowSignInfor.Height = flowSignInfor.Controls.Count > 0 ? 50 : 0;


            }
            catch (Exception)
            {

              
            }
            finally
            {
                flowSignInfor.ResumeLayout();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ten_vitri_ky">lười chưa thay đổi tên, bản chất=nguoiky+@+ten_vitri_ky</param>
        private void _nguoiky__OnClickMe(string ten_vitri_ky)
        {
            try
            {
                List<string> lstThongtinKy = ten_vitri_ky.Split('@').ToList<string>();
                if (globalVariables.IsAdmin || globalVariables.isSuperAdmin)
                {
                    try
                    {
                        DmucNhanvien objNhanvien = new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.Columns.UserName).IsEqualTo(lstThongtinKy[0]).ExecuteSingle<DmucNhanvien>();
                        if (objNhanvien != null)
                        {
                            txtNguoiKy.SetId(objNhanvien.IdNhanvien);
                        }
                        txtNguoiKy.RaiseEnterEvents();
                    }
                    catch (Exception)
                    {

                      
                    }
                }   
                var document = richEdit.Document;

                DevExpress.XtraRichEdit.API.Native.Bookmark bookmark = document.Bookmarks[lstThongtinKy[1]];
                if (bookmark != null)
                {
                    //document.CaretPosition = document.CreatePosition(bookmark.Range.Start.ToInt());
                    //richEdit.ScrollToCaret();
                    int bmStart = bookmark.Range.Start.ToInt();

                    foreach (DocumentImage img in document.Images)
                    {
                        int imgStart = img.Range.Start.ToInt();

                        if (imgStart >= bmStart)
                        {
                            // Di chuyển caret đến ảnh
                            document.CaretPosition = document.CreatePosition(img.Range.Start.ToInt());
                            document.Selection = document.CreateRange(img.Range.Start, img.Range.Length);
                            richEdit.ScrollToCaret();
                            break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {


            }
        }

        DataTable dtkhoachuyen = new DataTable();
        DataTable dtkhoanhapvien = new DataTable();
        DataTable dtCacKhoa = new DataTable();
        DataTable dtPhieuPttt = new DataTable();
        DataTable dt_tssk = new DataTable();
        void  LoadEmrData()
        {
            dtCacKhoa = new KCB_THAMKHAM().NoitruTimkiemlichsuBuonggiuong(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, "-1", -1);
            dtkhoachuyen = dtCacKhoa.Clone();
            DataRow[] arrKhoachuyen = dtCacKhoa.Select("id_chuyen>0");
            if (arrKhoachuyen.Length > 0) 
                dtkhoachuyen = arrKhoachuyen.CopyToDataTable();
            DataRow[] arrKhoanhapvien = dtCacKhoa.Select("id_chuyen<=0");
            if (arrKhoanhapvien.Length > 0)
            {
                dtkhoanhapvien = arrKhoanhapvien.CopyToDataTable();
            }
            dt_tssk = new Select().From(EmrTiensuSankhoa.Schema)
                   .Where(EmrQuatrinhThaiky.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
               .And(EmrQuatrinhThaiky.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
               .ExecuteDataSet().Tables[0];
            string sql = "select  format(ngay_pttt,' HH:mm - dd/MM/yyyy') as ngay_pttt,concat(phuongphap_pttt,'/',ten_phuongphap_vocam) as phuongphap_pt_vc, ";
            sql += " (select top 1 ten_nhanvien from dmuc_nhanvien where id_nhanvien in (select t.Number from dbo.fromStringintoIntTable(p.idbacsi_pttt)t)) as ten_bacsy_phauthuat, ";
            sql += " (select top 1 ten_nhanvien from dmuc_nhanvien where id_nhanvien in (select t.Number from dbo.fromStringintoIntTable(p.idbacsi_gayme) t)) as ten_bacsy_gayme ";
            sql += " from kcb_phieupttt p ";
            sql += " where id_benhnhan={0} and ma_luotkham='{1}' order by ngay_pttt";
            sql = string.Format(sql, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham);
            dtPhieuPttt = Utility.ExecuteSql(sql, CommandType.Text).Tables[0];
        }
        void InPhieuBanGiaoNguoiBenhChuyenKhoa(string Loaiphieu_HIS)
        {
            try
            {
                EmrPhieubangiaonguoibenhchuyenkhoa phieubangiao = new Select().From(EmrPhieubangiaonguoibenhchuyenkhoa.Schema)
                       .Where(EmrPhieubangiaonguoibenhchuyenkhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                       .And(EmrPhieubangiaonguoibenhchuyenkhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                       .ExecuteSingle<EmrPhieubangiaonguoibenhchuyenkhoa>();
                if (phieubangiao.IdPhieu <= 0)
                {
                    Utility.ShowMsg("Bạn cần lưu thông tin Phiếu bàn giao người bệnh chuyển khoa trước khi thực hiện in phiếu");
                    return;
                }
                DataTable dtData = SPs.EmrPhieubangiaonguoibenhchuyenkhoaLaythongtinIn(phieubangiao.IdPhieu).GetDataSet().Tables[0];
                dtData.TableName = "PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA";
                dtData.Rows[0]["sngay_bangiao"] = phieubangiao != null ? Utility.FormatDateTime_gio_ngay_thang_nam(phieubangiao.NgayBangiao, "") : "Ngày ......./......./..........";
                if (Loaiphieu_HIS== "PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA_BACSI")
                    WordPrinter.InPhieu(dtData, "PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA_BACSI.doc", "", false, @"doc\PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA_CHECKED_FIELDS.txt");
                else
                    WordPrinter.InPhieu(dtData, "PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA_DIEUDUONG.doc", "", false, @"doc\PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA_CHECKED_FIELDS.txt");


            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        string File2View = "";
        string FileKiso= "";
        string FileIn = "";
        long IdPhieu = -1;
        private void grdEmrDocuments_SelectionChanged(object sender, EventArgs e)
        {

            File2View = "";
            FileKiso = "";
            FileIn = "";
            if (!isAllowSelectionChanged)
            {
                if (pdfViewer1 != null)
                    pdfViewer1.CloseDocument();
                return;
            }
            
            if (stream != null)
            {
                stream.Close();
                stream = null;
            }
            if (!Utility.isValidGrid(grdEmrDocuments) || (grdEmrDocuments.CurrentColumn != null && grdEmrDocuments.CurrentColumn.Key == "CHON")) return;//Người dùng check chọn thì không phản ứng gì
            if (!isLinkClicked)//Giữ lại panel khi click vào link phiếu KQ
                flowKQCLS.Height = 0;
            currRow = Utility.findthelastChild(grdEmrDocuments.CurrentRow);
            if (currRow == null)
            {
                pdfViewer1.CloseDocument();
                return;
            }
            globalVariables.emr_id_file = -1;
            try
            {
                Utility.WaitNow(this);
                sysSignsize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("signsize").ExecuteSingle<SysSystemParameter>();
                string loaiphieuhis = "";
                string loaiphieu_cha = "";
                string reportcode = "";
                
                long IdFile = Utility.Int64Dbnull(currRow.Cells[EmrDocument.Columns.IdFile].Value);
                bool tthai_kiso = false;
                //Bắt đầu xử lý sinh lại file dựa vào loại phiếu HIS và report_code
                EmrDocument objDoc = EmrDocument.FetchByID(Utility.Int64Dbnull(currRow.Cells[EmrDocument.Columns.IdFile].Value));
                if (objDoc != null)
                {
                    globalVariables.emr_id_file = objDoc.IdFile;
                    IdPhieu = Utility.Int64Dbnull(objDoc.IdPhieu);// Utility.Int64Dbnull(currRow.Cells[EmrDocument.Columns.IdPhieu].Value);
                    loaiphieuhis = objDoc.LoaiPhieuHis;
                    loaiphieu_cha = objDoc.LoaiphieuCha;
                    reportcode = objDoc.ReportCode;
                    FileKiso =Utility.sDbnull( objDoc.FileKiso,"");
                    FileIn = objDoc.FileIn;
                }
                else
                {
                    loaiphieuhis = Utility.sDbnull(currRow.Cells[EmrDocument.Columns.LoaiPhieuHis].Value);
                    loaiphieuhis = Utility.sDbnull(currRow.Cells[EmrDocument.Columns.LoaiphieuCha].Value);
                    reportcode = Utility.sDbnull(currRow.Cells[EmrDocument.Columns.ReportCode].Value);
                }
                AddSignInfor(objDoc);
                nguoi_tao = Utility.sDbnull(currRow.Cells[EmrDocument.Columns.NguoiTao].Value);
                globalVariables.dtSignInfor = SPs.EmrLaythongtinChukyTrenphieu(IdFile.ToString(),"",1).GetDataSet().Tables[0];
                LoadSignInfor();
                
                DataTable v_dtEmrData;
                SysReport objReport;
                if(FileKiso!=null)//File này dạng PDF
                {

                }    
                if (loaiphieuhis == Loaiphieu_HIS.FILE_DINHKEM)
                {
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUDANGKYKCB)
                {
                   

                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEU_CAMKET_PTTT)
                {
                  
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUKHAM_TIENME)
                {
                  
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUPTTT || loaiphieu_cha == Loaiphieu_HIS.PHIEUPTTT)
                {
                    

                }
                else if(loaiphieuhis==Loaiphieu_HIS.PHIEU_TTBA)
                {
                  
                }
                
                else if (LoaiBA.All.Contains(loaiphieu_cha))
                {
                    int ToBA = 0;
                    if (loaiphieuhis == Loaiphieu_HIS.BENHAN_TO1) ToBA = 1;
                    else if (loaiphieuhis == Loaiphieu_HIS.BENHAN_TO2) ToBA = 2;
                    else if (loaiphieuhis == Loaiphieu_HIS.BENHAN_TO3) ToBA = 3;
                    else if (loaiphieuhis == Loaiphieu_HIS.BENHAN_TO4) ToBA = 4;
                    else if (loaiphieuhis == Loaiphieu_HIS.BENHAN_BIA) ToBA = 0;
                    else ToBA = 100;
                    string ma_ba = "";
                    File2View = clsInBA.InBA(IdPhieu, ma_ba, loaiphieu_cha, objLuotkham, dtkhoanhapvien, dtkhoachuyen, dt_tssk, dtPhieuPttt, ToBA, true);
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUTOMTATDIEUTRINGOAITRU)//Phiếu tóm tắt điều trị ngoại trú
                {
                  
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEURAVIEN)
                {
                  
                }
                
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUNHAPVIEN)
                {
                
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUCHUYENVIEN)
                {

                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA_BACSI || loaiphieuhis == Loaiphieu_HIS.PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA_DIEUDUONG )
                {
                 
                }
                else if (loaiphieuhis == Loaiphieu_HIS.BIENBANHOICHAN)
                {
                  
                }
                else if (loaiphieuhis == Loaiphieu_HIS.TT25_GIAYCHUNGNHAN_TAINANTHUONGTICH)
                {
                   

                }
                else if (loaiphieuhis == Loaiphieu_HIS.TT25_GIAYXACNHAN_NGHIDUONGTHAI)
                {
                  
                }
                else if (loaiphieuhis == Loaiphieu_HIS.TT25_GIAYXACNHAN_NGUOIMEKHONGDUSUCKHOE_CHAMSOCCON)
                {
                   
                }
                else if (loaiphieuhis == Loaiphieu_HIS.TT25_GIAYXACNHAN_QUATRINHDIEUTRINOITRU)
                {
                   
                }
                else if (loaiphieuhis == Loaiphieu_HIS.TT25_GIAYXACNHAN_QUATRINHDIEUTRIVOSINH)
                {
                   
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUCHIDINH)
                {
                  
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEU_KQCDHA || loaiphieuhis == Loaiphieu_HIS.PHIEU_KQXN)
                {
                  
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUDIEUTRI)
                {
                   
                    
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUTHEODOI_TRUYENDICH)
                {
                    
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEU_CONGKHAI)
                {
                  
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUCHIDINH)
                {

                }
                if(!File2View.ToLower().Contains(".pdf"))
                {
                    SetDocView(true);
                    LoadWordFile(File2View, true);
                }   
                else
                {
                    #region PDFViewer
                    SetDocView(false);
                    if (pdfViewer1 != null) pdfViewer1.CloseDocument();
                    if (File2View != "" && File.Exists(File2View))
                    {
                        if (stream == null)
                            stream = new FileStream(File2View, FileMode.Open);
                        pdfViewer1.LoadDocument(stream);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (FileKiso != "")
                {
                    cmdKidientu.Enabled = cmdHuyKyDientu.Enabled = false;
                }
                Utility.DefaultNow(this);
            }
        }
       
        private string InBienBanHoiChan(long id_phieu)
        {

            try
            {
                KcbBienbanhoichan bbhc = KcbBienbanhoichan.FetchByID(id_phieu);
                if (bbhc == null || bbhc.Id <= 0)
                {
                    Utility.ShowMsg("Bạn cần tạo biên bản hội chẩn trước khi thực hiện in");
                    return "";
                }
                DataTable dtData = SPs.KcbLaythongtinBienbanhoichanIn(bbhc.Id).GetDataSet().Tables[0];

                List<string> lstAddedFields = new List<string>() { "khamtimmach_binhthuong", "khamtimmach_batthuong", "khamtimmach_khac",
                "khamhohap_binhthuong", "khamhohap_copd", "khamhohap_khac",
                "phanloaivetmo_sach","phanloaivetmo_sachnhiem",
                "phanloaivetmo_nhiem", "phanloaivetmo_ban"};

                dtData.TableName = "kcb_bienbanhoichan";
                DataTable dtMergeField = dtData.Clone();
                Utility.AddColums2DataTable(ref dtMergeField, lstAddedFields, typeof(string));
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
                drData["ten_phieu"] = "BIÊN BẢN HỘI CHẨN";
                drData["sngay_hoichan_full"] = Utility.FormatDateTime_giophut_ngay_thang_nam(bbhc.NgayHoichan, "");
                drData["sngay_hoichan"] = Utility.FormatDateTime(bbhc.NgayHoichan);
                drData["ngay_in"] = Utility.FormatDateTime(DateTime.Now);
                drData["sngay_nhapvien"] = Utility.FormatDateTime_giophut_ngay_thang_nam(objLuotkham.NgayNhapvien, "");
                drData["sngay_dukienpttt"] = Utility.FormatDateTime_giophut_ngay_thang_nam(bbhc.DukienthoigianPttt.Value, "");
                Dictionary<string, string> dicMF = new Dictionary<string, string>();
                dicMF.Add("khamtimmach_binhthuong", bbhc.Timach.Value == 0 ? "1" : "0");
                dicMF.Add("khamtimmach_batthuong", bbhc.Timach.Value == 1 ? "1" : "0");
                dicMF.Add("khamtimmach_khac", bbhc.Timach.Value == 2 ? "1" : "0");
                dicMF.Add("khamhohap_binhthuong", bbhc.Hohap.Value == 0 ? "1" : "0");
                dicMF.Add("khamhohap_copd", bbhc.Hohap.Value == 1 ? "1" : "0");
                dicMF.Add("khamhohap_khac", bbhc.Hohap.Value == 2 ? "1" : "0");
                dicMF.Add("phanloaivetmo_sach", bbhc.PhanloaiVetmo.Value == 0 ? "1" : "0");
                dicMF.Add("phanloaivetmo_sachnhiem", bbhc.PhanloaiVetmo.Value == 1 ? "1" : "0");
                dicMF.Add("phanloaivetmo_nhiem", bbhc.PhanloaiVetmo.Value == 2 ? "1" : "0");
                dicMF.Add("phanloaivetmo_ban", bbhc.PhanloaiVetmo.Value == 3 ? "1" : "0");
                List<string> fieldNames = new List<string>();

                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\BIENBAN_HOICHAN.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtMergeField);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport("BIENBAN_HOICHAN", ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu Biên bản hội chẩn tại thư mục sau :" + PathDoc);
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
                               Path.GetFileNameWithoutExtension(PathDoc), "BIENBAN_HOICHAN", objLuotkham.MaLuotkham, Utility.sDbnull(bbhc.Id), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


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
                    Utility.MergeFieldsCheckBox2Doc(builder, dicMF, null, drData);
                    //Các hàm MoveToMergeField cần thực hiện trước dòng MailMerge.Execute bên dưới
                    doc.MailMerge.Execute(drData);
                    Utility.SignDoc(doc, builder, sysLogosize != null ? sysLogosize.SValue : "");
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    return fileKetqua;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy biểu mẫu", "TThông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return "";
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return "";
            }
        }
        bool isLinkClicked = false;
        private void LnkItem_Click(object sender, EventArgs e)
        {
            try
            {
                isLinkClicked = true;
                Utility.GotoNewRowJanus(grdEmrDocuments,"guid",Utility.sDbnull((sender as LinkLabel).Tag));
            }
            catch (Exception ex)
            {
            }
        }
        string LayThongTinNguoiKyToDieuTri(long id_phieu)
        {
            var q = globalVariables.dtSignInfor.AsEnumerable().Where(c => Utility.Int64Dbnull(c["id_phieu"]) == id_phieu).FirstOrDefault();
            if (q != null)
                return Utility.sDbnull(q["nguoi_ky"]);
            return "";
        }
       
        #region Các phiếu PTTT
        private string InPhieuChungNhanPTTT(DataTable dtEmrDocuments, KcbPhieupttt objpttt, string ma_loaidvu)
        {
            try
            {

                dtEmrDocuments.TableName = "kcb_phieu_pttt";
                List<string> lst_ten_phieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("PTTT_TENPHIEU", "GIẤY CHỨNG NHẬN PHẪU THUẬT-THỦ THUẬT", true).Split('@').ToList<string>();

                Document doc;
                DataRow drData = dtEmrDocuments.Rows[0];
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
                drData["sngay_pttt"] = Utility.FormatDateTime(Utility.sDbnull(drData["sngay_pttt"], ""), "ngày......tháng......năm.........");//BHYT giá trị đến
                List<string> fieldNames = new List<string>();

                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\PHIEU_CHUNGNHAN_PTTT.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtEmrDocuments);
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
                    sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("signsize").ExecuteSingle<SysSystemParameter>();
                    Utility.SignDoc(doc, builder, sysLogosize != null ? sysLogosize.SValue : "");
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);

                    //Lưu ra pdf
                    //string pdf2sign = Application.StartupPath + @"\pdf2sign";
                    //Utility.Try2CreateFolder(pdf2sign);
                    //string pdfFile = pdf2sign + @"\" + Guid.NewGuid().ToString() + ".pdf";
                    //KisoBookmarks(fileKetqua, pdfFile,"","");
                    //doc.Save(pdfFile, Aspose.Words.SaveFormat.Pdf);
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
        void KisoBookmarks(List<SignatureLocation> lstSignLoc, string wordPath, string pdfOutPath, string certPath, string certPassword)
        {
            try
            {
                // Step 1: Load Word document
                var doc = new Aspose.Words.Document(wordPath);

                // Step 2: Prepare layout tools
                var collector = new Aspose.Words.Layout.LayoutCollector(doc);
                var enumerator = new Aspose.Words.Layout.LayoutEnumerator(doc);

                List<VMSDigitalSignatureLocation> bookmarkLocations = new List<VMSDigitalSignatureLocation>();
                foreach (SignatureLocation signloc in lstSignLoc)
                {
                    var pdfRect = new VMSDigitalSignatureRect() { StartX = Convert.ToInt32(signloc.PdfRect.X), StartY = Convert.ToInt32(signloc.PdfRect.Y), EndX = Convert.ToInt32(signloc.PdfRect.X + signloc.PdfRect.Width), EndY = Convert.ToInt32(signloc.PdfRect.Y + signloc.PdfRect.Height) };
                    bookmarkLocations.Add(new VMSDigitalSignatureLocation
                    {
                        SignName = signloc.SignerName,
                        pageSign = signloc.Page,
                        lstRect = new List<VMSDigitalSignatureRect>() { pdfRect }
                    });
                }
                //foreach (Aspose.Words.Bookmark bookmark in doc.Range.Bookmarks)
                //{
                //    var startNode = bookmark.BookmarkStart;
                //    var entity = collector.GetEntity(startNode);
                //    if (entity == null) continue;

                //    enumerator.Current = entity;
                //    RectangleF rect = enumerator.Rectangle;
                //    int pageIndex = collector.GetStartPageIndex(startNode);

                //    var section = (Aspose.Words.Section)doc.GetChild(NodeType.Section, pageIndex - 1, true);
                //    double pageHeight = section.PageSetup.PageHeight;
                //    float pdfY = (float)(pageHeight - rect.Y - rect.Height);

                //    var pdfRect = new VMSDigitalSignatureRect() { StartX = Convert.ToInt32(rect.X), StartY = Convert.ToInt32(pdfY), EndX = Convert.ToInt32(rect.X + rect.Width), EndY = Convert.ToInt32(pdfY + rect.Height) };
                //    bookmarkLocations.Add(new VMSDigitalSignatureLocation
                //    {
                //        SignName = bookmark.Name,
                //        pageSign = pageIndex,
                //        lstRect =new List<VMSDigitalSignatureRect>() { pdfRect }
                //    });
                //}

                // Step 3: Save Word to PDF
                doc.Save(pdfOutPath, SaveFormat.Pdf);
                string pdfOutPath_signed = string.Format(@"{0}\{1}_signed.pdf",Path.GetDirectoryName(pdfOutPath),Path.GetFileNameWithoutExtension(pdfOutPath));
                byte[] fileContent = File.ReadAllBytes(pdfOutPath);
                string dataTobeSign = Convert.ToBase64String(fileContent);

                //Gọi hàm kí số
                string webApiLink = THU_VIEN_CHUNG.Laygiatrithamsohethong("CHUKISO_API", "https://localhost:44378/api/Viettel", true);
                string errMsg = "";
                var objDigitalSignature = new VMSDigitalSignature();
                objDigitalSignature.base64Pdf = dataTobeSign;
                objDigitalSignature.base64Signature = globalVariables.bytHinhChuKy == null ? "" : Convert.ToBase64String(globalVariables.bytHinhChuKy);
                objDigitalSignature.signatureType = globalVariables.bytHinhChuKy == null ? "2":"1"; //1: sign with image, 2: sign with text,  3: sign with text and image
                //objDigitalSignature.signatureName = "CKS_BACSI";
                objDigitalSignature.pdfFileName = pdfOutPath;
                objDigitalSignature.userName = "001081007147";


                objDigitalSignature.userFullName = "";
                objDigitalSignature.userDesc = "";
                //objDigitalSignature.appId = "";
               // objDigitalSignature.secret ="pwd";
                objDigitalSignature.locations = bookmarkLocations;
                // objDigitalSignature.dateSigned = DateTime.Now;
                ApiRequestResponse ret = HisLisWebApi.INST.KysoPDF(webApiLink, objDigitalSignature, ref errMsg);
                if (ret != null && ret.Data != null)
                {
                    var pdfBytes = Convert.FromBase64String(ret.Data.ToString());
                    File.WriteAllBytes(pdfOutPath_signed, pdfBytes);
                }
            }
            catch (Exception ex)
            {

                
            }
           
        }
        private string InPhieuPTTT(DataTable dtEmrDocuments, KcbPhieupttt objpttt)
        {
            try
            {

                dtEmrDocuments.TableName = "kcb_phieu_pttt";
                Document doc;
                DataRow drData = dtEmrDocuments.Rows[0];
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
                drData["sngay_pttt"] = Utility.FormatDateTime(Utility.sDbnull(drData["sngay_pttt"], ""), "ngày......tháng......năm.........");//BHYT giá trị đến
                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\PHIEU_PTTT.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtEmrDocuments);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport("PHIEU_PTTT", ref tieude, ref PathDoc);
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
                               Path.GetFileNameWithoutExtension(PathDoc), "PHIEU_PTTT", objLuotkham.MaLuotkham, Utility.sDbnull(objpttt.IdPhieu), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


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
                    sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("signsize").ExecuteSingle<SysSystemParameter>();
                    Utility.SignDoc(doc, builder, sysLogosize != null ? sysLogosize.SValue : "");
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    ////Lấy tọa độ
                    //List<SignatureLocation> lstSignLoc = GetSignatureLineLocation(fileKetqua,new List<string>() { "CKS_BACSI" });
                    ////Lưu ra pdf
                    //string pdf2sign = Application.StartupPath + @"\pdf2sign";
                    //Utility.Try2CreateFolder(pdf2sign);
                    //string pdfFile = pdf2sign + @"\" + Guid.NewGuid().ToString() + ".pdf";
                    //KisoBookmarks(lstSignLoc, fileKetqua, pdfFile, "", "");
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
       static void RemoveShape()
        {

        }
        public static List<SignatureLocation> GetSignatureLineLocation(string wordPath,List<string> lstVitriKy  )
        {
            List<SignatureLocation> lstSignLoc = new List<SignatureLocation>();
            var doc = new Aspose.Words.Document(wordPath);
            var collector = new Aspose.Words.Layout.LayoutCollector(doc);
            var enumerator = new Aspose.Words.Layout.LayoutEnumerator(doc);
            NodeCollection lstNode = doc.GetChildNodes(NodeType.Shape, true);
            foreach (Aspose.Words.Drawing.Shape shape in lstNode)
            {
                if (lstVitriKy.Contains( shape.Name))// == signerName)
                {
                    var entity = collector.GetEntity(shape);
                    if (entity == null)
                        continue;

                    enumerator.Current = entity;
                    var rect = enumerator.Rectangle;

                    int pageIndex = collector.GetStartPageIndex(shape);
                    //int pageIndex = collector.GetStartPageIndex(shape);
                    var section = (Aspose.Words.Section)shape.GetAncestor(NodeType.Section);
                    if (section == null)
                        continue;
                    //var section = (Aspose.Words.Section)doc.GetChild(NodeType.Section, pageIndex - 1, true);
                    double pageHeight = section.PageSetup.PageHeight;

                    // Chuyển sang toạ độ PDF (gốc dưới)
                    float pdfY = (float)(pageHeight - rect.Y - rect.Height);
                    var pdfRect = new RectangleF(rect.X, pdfY, rect.Width, rect.Height);

                    lstSignLoc.Add(new SignatureLocation
                    {
                        SignerName = shape.Name,
                        Page = pageIndex,
                        PdfRect = pdfRect
                    });
                }
            }
            //Xóa hình Ký ở đây
          
            foreach (Aspose.Words.Drawing.Shape shape in lstNode)
            {
                if (lstVitriKy.Contains(shape.Name))// == signerName)
                {
                    shape.Remove();
                }
            }
            return lstSignLoc;
        }
        private string InPhieuCamKetPTTT(DataTable dtEmrDocuments, KcbPhieupttt objpttt, string ma_loaidvu)
        {
            try
            {

                dtEmrDocuments.TableName = "kcb_phieu_pttt";
                List<string> lst_ten_phieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("PTTT_TENPHIEU_CAMKET", "GIẤY CAM ĐOAN CHẤP NHẬN PHẪU THUẬT, THỦ THUẬT VÀ GÂY MÊ HỒI SỨC@GIẤY CAM ĐOAN CHẤP NHẬN PHẪU THUẬT, THỦ THUẬT VÀ GÂY MÊ HỒI SỨC", true).Split('@').ToList<string>();

                Document doc;
                DataRow drData = dtEmrDocuments.Rows[0];
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
                drData["sngay_pttt"] = Utility.FormatDateTime(Utility.sDbnull(drData["sngay_pttt"], ""), "ngày......tháng......năm.........");//BHYT giá trị đến
                List<string> fieldNames = new List<string>();

                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\PHIEU_CAMKET_PTTT.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtEmrDocuments);
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
                    sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("signsize").ExecuteSingle<SysSystemParameter>();
                    Utility.SignDoc(doc, builder, sysLogosize != null ? sysLogosize.SValue : "");
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
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
        private string InPhieuTuongTrinhPTTT(DataTable dtEmrDocuments, KcbPhieupttt objpttt, string ma_loaidvu)
        {
            try
            {

                long ID_PHIEUPTTT = Utility.Int64Dbnull(objpttt.IdPhieu);
                dtEmrDocuments.TableName = "kcb_phieu_pttt";
                Utility.AddColums2DataTable(ref dtEmrDocuments, new List<string>() { "thogian_vaovien", "thoigian_batdau_phauthuat", "thoigian_ketthuc_phauthuat" }, typeof(string));
                List<string> lst_ten_phieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("PTTT_TENPHIEU_TUONGTRINH", "PHIẾU TƯỜNG TRÌNH PHẪU THUẬT@PHIẾU TƯỜNG TRÌNH THỦ THUẬT", true).Split('@').ToList<string>();
                Document doc;
                DataRow drData = dtEmrDocuments.Rows[0];
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
                drData["sngay_pttt"] = Utility.FormatDateTime(Utility.sDbnull(drData["sngay_pttt"], ""), "ngày......tháng......năm.........");//BHYT giá trị đến
                List<string> fieldNames = new List<string>();


                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\PHIEU_TUONGTRINH_PTTT.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtEmrDocuments);
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
                    sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("signsize").ExecuteSingle<SysSystemParameter>();
                    Utility.SignDoc(doc, builder, sysLogosize != null ? sysLogosize.SValue : "");
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }

                    doc.Save(fileKetqua, SaveFormat.Doc);
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
        DataTable dtEmrDocuments = new DataTable();
        private void UcThongtinnguoibenh_emr_basic1__OnEnterMe()
        {
            isAllowSelectionChanged = false;
            if (optPdfView.Checked)

            {
                if (pdfViewer1 != null) 
                    pdfViewer1.CloseDocument();
            }
            else
                richEdit.CreateNewDocument();
            if (ucThongtinnguoibenh_emr_basic1.objLuotkham != null)
            {

                objLuotkham = ucThongtinnguoibenh_emr_basic1.objLuotkham;
                FtpClientCurrentDirectoryAttatchmentFiles = FtpClientCurrentDirectoryAttatchmentFiles + "//" + objLuotkham.MaLuotkham;//Thư mục+mã lượt khám
                _baseDirectoryRIS = string.Format(@"{0}\EMR_DOCUMENTS\{1}\pdfRIS", Application.StartupPath, objLuotkham.MaLuotkham);
                _baseDirectoryLIS = string.Format(@"{0}\EMR_DOCUMENTS\{1}\pdfLIS", Application.StartupPath, objLuotkham.MaLuotkham);
                dtEmrDocuments = SPs.EmrLaydanhsachDocuments(objLuotkham.MaLuotkham, -1, globalVariables.UserName, Utility.ByteDbnull(globalVariables.IsAdmin || globalVariables.isSuperAdmin || Utility.Coquyen("EMR_FULL") ? 1 : 0),"").GetDataSet().Tables[0];
                foreach (DataRow dr in dtEmrDocuments.Rows)
                    dr["guid"] = Guid.NewGuid().ToString();
                Utility.SetDataSourceForDataGridEx_Basic(grdEmrDocuments, dtEmrDocuments, true, true, "1=1", "stt_gay,ten_gay,stt_phieu_emr,stt_report");
                //Nạp các thông tin để in các tờ bệnh án
                LoadEmrData();
                LoadDocsAndSigns();
                isAllowSelectionChanged = true;
                TuybienMenuBenhAn();
            }
            else
            {
               
                grdEmrDocuments.DataSource = null;
            }    
        }
        void TuybienMenuBenhAn()
        {
            if(Utility.Int64Dbnull( objLuotkham.IdBa)<=0)//Thực hiện khởi tạo
            {
                mnu01BV_BANoikhoa.Enabled = false;
                mnu10BV_BANgoaikhoa.Enabled = false;
                mnu15BV_BANgoaitru.Enabled = false;
                mnuBAPhukhoa.Enabled = false;
                mnuBASanKhoa.Enabled = false;

                Utility.SetMsg(uiStatusBar1.Panels[0], "Người bệnh chưa tạo Hồ sơ bệnh án");
            }  
            else
            {
                mnu01BV_BANoikhoa.Enabled = objLuotkham.LoaiBenhAn == LoaiBA.BA_NOIKHOA;
                mnu10BV_BANgoaikhoa.Enabled = objLuotkham.LoaiBenhAn == LoaiBA.BA_NGOAIKHOA;
                mnu15BV_BANgoaitru.Enabled = objLuotkham.LoaiBenhAn == LoaiBA.BA_NGOAITRU;
                mnuBAPhukhoa.Enabled = objLuotkham.LoaiBenhAn == LoaiBA.BA_PHUKHOA;
                mnuBASanKhoa.Enabled = objLuotkham.LoaiBenhAn == LoaiBA.BA_SANKHOA;
                Utility.SetMsg(uiStatusBar1.Panels[0], string.Format("Người bệnh đã có {0}",Utility.GetTenLoaiBenhAn(objLuotkham.LoaiBenhAn)));
            }    
        }
        void LoadDocsAndSigns()
        {
            
        }
        void _CheckedChanged(object sender, EventArgs e)
        {

        }
        private void InitFtp()
        {
            try
            {
                string FTPServer = "";
                string UID = "";
                string PWD = "";
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


                FTPInfor = THU_VIEN_CHUNG.Laygiatrithamsohethong("EMR_ATTATCHMENTFILE_SERVER", string.Format("{0}-{1}-{2}", "127.0.0.1", "emrfile", "emrfile"), true).Split('-').ToList<string>();
                if (FTPInfor.Count > 0 )
                {
                    FTPServer = FTPInfor[0];
                    UID = FTPInfor[1];
                    PWD = FTPInfor[2];
                }
                if (!Directory.Exists(_baseDirectoryAttatchmentFiles))
                {
                    Directory.CreateDirectory(_baseDirectoryAttatchmentFiles);
                }

                FtpClientAttatchmentFiles = new FTPclient(FTPServer, UID, PWD);
                FtpClientAttatchmentFiles.UsePassive = true;
                FtpClientCurrentDirectoryAttatchmentFiles = FtpClientAttatchmentFiles.CurrentDirectory;
                

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

        void LoadAnhChuKy()
        {
            try
            {
                var old = picSignImg.Image;
                picSignImg.Image = null;    // ✳️ Bỏ tham chiếu đến ảnh cũ
                old?.Dispose();
                if (globalVariablesPrivate.objNhanvien != null)
                {
                    if (globalVariablesPrivate.objNhanvien.ChuKy != null)
                    {
                        using (var ms = new MemoryStream(globalVariablesPrivate.objNhanvien.ChuKy))
                        {
                            picSignImg.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        picSignImg.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            
        }
        private void frm_SingleSign_Load(object sender, EventArgs e)
        {
            try
            {
                globalVariablesPrivate.objNhanvien = DmucNhanvien.FetchByID(globalVariables.gv_intIDNhanvien);
                LoadAnhChuKy();
                InitTitle();

                txtNguoiKy.Init(globalVariables.gv_dtDmucNhanvien,
                                            new List<string>
                                 {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.UserName,
                                      DmucNhanvien.Columns.TenNhanvien
                                 });
                txtNguoiKy.Enabled = globalVariables.isSuperAdmin || globalVariables.IsAdmin;
                txtNguoiKy.SetId(globalVariables.gv_intIDNhanvien);
                txtNguoiKy.RaiseEnterEvents();
                LoadUserConfigs();
                DataTable dtPhieuEMR = new Select("*").From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("EMR_PHIEU")
           .OrderAsc(DmucChung.Columns.SttHthi)
           .ExecuteDataSet().Tables[0];
                DataRow dr = dtPhieuEMR.NewRow();
                dr[DmucChung.Columns.Ten] = "--Chọn phiếu--";
                dr[DmucChung.Columns.Ma] = "-1";

                dtPhieuEMR.Rows.InsertAt(dr, 0);
                DataBinding.BindDataCombobox(cboLoaiphieuEmr, dtPhieuEMR, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
               
                DataTable dtPhieuHIS = new Select("*").From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("EMR_LOAIPHIEU_HIS")
         .OrderAsc(DmucChung.Columns.SttHthi)
         .ExecuteDataSet().Tables[0];
                 dr = dtPhieuHIS.NewRow();
                dr[DmucChung.Columns.Ten] = "--Chọn phiếu--";
                dr[DmucChung.Columns.Ma] = "-1";

                dtPhieuHIS.Rows.InsertAt(dr, 0);
                DataBinding.BindDataCombobox(cboLoaiphieuHIS, dtPhieuHIS, DmucChung.Columns.Ma, DmucChung.Columns.Ten);

                DataTable dtGayEMR = new Select("*").From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("EMR_GAYBA")
             .OrderAsc(DmucChung.Columns.SttHthi)
             .ExecuteDataSet().Tables[0];
                dr = dtGayEMR.NewRow();
                dr[DmucChung.Columns.Ten] = "--Chọn phiếu--";
                dr[DmucChung.Columns.Ma] = "-1";

                dtGayEMR.Rows.InsertAt(dr, 0);
                DataBinding.BindDataCombobox(cboGay, dtGayEMR, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                cboLoaiphieuEmr.SelectedIndex = 0;
                cboLoaiphieuHIS.SelectedIndex = 0;
                cboGay.SelectedIndex = 0;
                //Tránh tự selectall xong cứ chậm chậm đéo hiểu
                cboLoaiphieuEmr.SelectionLength = 0;
                cboLoaiphieuHIS.SelectionLength = 0;
                cboGay.SelectionLength = 0;
                isAllowSelectedIndexChanged = true;
                if (isAutoLoad)
                {
                    uiTab.SelectedTab = uiTabPageEmr;
                    ucThongtinnguoibenh_emr_basic1.Refresh();
                }
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
        private void frm_SingleSign_FormClosing(object sender, FormClosingEventArgs e)
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

        private void frm_SingleSign_KeyDown(object sender, KeyEventArgs e)
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
        void SetDocView(bool isDocView)
        {
            richEdit.Visible = isDocView;
            pdfViewer1.Visible = !isDocView;
        }
        private void optDocView_CheckedChanged(object sender, EventArgs e)
        {
            SetDocView(true);
        }

        private void optPdfView_CheckedChanged(object sender, EventArgs e)
        {
            SetDocView(false);
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
                    // Tải tài liệu
                    if (Path.GetExtension(filePath).Equals(".docx", StringComparison.OrdinalIgnoreCase))
                    {
                        richEdit.LoadDocument(filePath, DocumentFormat.OpenXml);
                    }
                    else
                    {
                        richEdit.LoadDocument(filePath, DocumentFormat.Doc);
                    }

                    // Đặt chế độ Read only cấm sửa. Chỉ bật chế độ sửa khi insert chữ kí
                    richEdit.ReadOnly = isReadOnly;
                    if (isReadOnly)
                    {
                        richEdit.ActiveViewType = RichEditViewType.Simple;
                        richEdit.Options.Behavior.ShowPopupMenu = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
                    }

                    richEdit.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.PrintLayout;
                    richEdit.ActiveView.ZoomFactor = trackBarZoom.Value / 100f;
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
            document.Fields.Create(document.CaretPosition, string.Format("MERGEFIELD {0}", cboTagFields.Text));
            document.Fields.Update();
        }
        string templatefile = string.Format(@"{0}\TemplateDocs\Test.docx", Application.StartupPath);
        private void uiButton2_Click(object sender, EventArgs e)
        {
            richEdit.SaveDocument(templatefile, DocumentFormat.OpenXml);
        }
        
        private void cmdSign_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File2View.ToLower().Contains(".pdf"))
                {
                    //Lấy tọa độ
                    List<SignatureLocation> lstSignLoc = GetSignatureLineLocation(File2View, new List<string>() { "CKS_BACSI" });
                    //Lưu ra pdf
                    string pdf2sign = Application.StartupPath + @"\pdf2sign";
                    Utility.Try2CreateFolder(pdf2sign);
                    string pdfFile = pdf2sign + @"\" + Guid.NewGuid().ToString() + ".pdf";
                    KisoBookmarks(lstSignLoc, File2View, pdfFile, "", "");
                }
            }
            catch (Exception ex)
            {


            }
            finally
            {
                grdEmrDocuments.CurrentRow.BeginEdit();
                grdEmrDocuments.CurrentRow.IsChecked = false;
                grdEmrDocuments.CurrentRow.EndEdit();
            }
        }

        private void mnuAnPhieu_Click(object sender, EventArgs e)
        {
            cmdAn.PerformClick();

        }
        /// <summary>
        /// Chú ý: Người ký chưa chắc đã là người tạo. Vì đa phần điều dưỡng hỗ trợ nhập liệu cho bác sỹ và có thể không sử dụng tài khoản của bác sỹ
        /// Người ký=== người tạo nếu dùng chính tài khoản của bác sỹ để thao tác.
        /// Với các phiếu có nhiều người ký thì người tạo chỉ là 1 trong các người ký trong case bác sỹ dùng chính tài khoản của mình để thao tác
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdKidientu_Click(object sender, EventArgs e)
        {
            try
            {
                EmrDocument objDoc = EmrDocument.FetchByID(Utility.Int64Dbnull(currRow.Cells[EmrDocument.Columns.IdFile].Value));
                if (objDoc.LoaiPhieuHis == Loaiphieu_HIS.PHIEUCHIDINH)
                {
                    KyphieuChidinh();
                    return;
                }
                bool refresh = false;
                if(grdEmrDocuments.GetCheckedRows().Count()<=0)
                {
                    grdEmrDocuments.CurrentRow.BeginEdit();
                    grdEmrDocuments.CurrentRow.IsChecked = true;
                    grdEmrDocuments.CurrentRow.EndEdit();
                }    
                List<string> lstIdFiles= (from p in grdEmrDocuments.GetCheckedRows()  select Utility.sDbnull(p.Cells[EmrDocument.Columns.IdFile].Value)).Distinct().ToList<string>();
                //Lấy về thông tin kí của người dùng trên các file đang chọn
                DataTable dtSignInfo = SPs.EmrLaythongtinChukyTrenphieu(string.Join(",", lstIdFiles), globalVariablesPrivate.objNhanvien.UserName, 100).GetDataSet().Tables[0];
                //Lấy về danh sách các file liên quan đến người dùng
                List<long> lstIdFiles_NguoiKy = dtSignInfo.AsEnumerable().Select(c => Utility.Int64Dbnull(c["file_id"])).Distinct().ToList<long>();
                //Lấy về các phiếu đang chọn mà không liên quan đến người dùng(gặp người dùng ẩu chọn bừa khi ký)
                List<long> lstIdPhieu_Other = (from p in grdEmrDocuments.GetCheckedRows() where !lstIdFiles_NguoiKy.Contains( Utility.Int64Dbnull(p.Cells["id_file"].Value))  select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                List<long> lstIdPhieu_Daky = (from p in dtSignInfo.AsEnumerable() where lstIdFiles_NguoiKy.Contains(Utility.Int64Dbnull(p["file_id"]))  && Utility.ByteDbnull(p["tthai_ky"]) == 1 select Utility.Int64Dbnull(p[EmrDocument.Columns.IdPhieu])).Distinct().ToList<long>();
                List<long> lstIdPhieu_ChuaKy = (from p in dtSignInfo.AsEnumerable() where lstIdFiles_NguoiKy.Contains(Utility.Int64Dbnull(p["file_id"])) &&  Utility.ByteDbnull(p["tthai_ky"]) == 0  select Utility.Int64Dbnull(p[EmrDocument.Columns.IdPhieu])).Distinct().ToList<long>();
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
                        GridEXRow[] lstCheckedRows = grdEmrDocuments.GetCheckedRows();
                        foreach (GridEXRow _row in lstCheckedRows)
                        {
                            if (lstIdFiles_NguoiKy.Contains(Utility.Int64Dbnull(_row.Cells["id_file"].Value)) )//&& Utility.sDbnull(_row.Cells["tthai_kyso"].Value) == "0" )
                            {
                                long id_phieu = Utility.Int64Dbnull(_row.Cells[EmrDocument.Columns.IdPhieu].Value);
                                long IdFile = Utility.Int64Dbnull(_row.Cells[EmrDocument.Columns.IdFile].Value);
                                string LoaiPhieuHis = Utility.sDbnull(_row.Cells[EmrDocument.Columns.LoaiPhieuHis].Value);
                                if (LoaiPhieuHis == Loaiphieu_HIS.PHIEUDIEUTRI && id_phieu == -1)
                                {
                                    frm_chonky_todieutri _chonky_todieutri = new frm_chonky_todieutri(objLuotkham, globalVariables.UserName);
                                  if(  _chonky_todieutri.ShowDialog()==DialogResult.OK)
                                    {
                                        foreach (long id_p in _chonky_todieutri.lstIdphieu)
                                            num += SPs.EmrThaydoitrangthai(IdFile, id_p, LoaiPhieuHis, 2, true, globalVariables.UserName, DateTime.Now).Execute();
                                    }    
                                }
                                else
                                {
                                    num += SPs.EmrThaydoitrangthai(IdFile, id_phieu, LoaiPhieuHis, 2, true, globalVariables.UserName, DateTime.Now).Execute();
                                }
                                if(num > 0)
                                {
                                    refresh = true;
                                    _row.BeginEdit();
                                    _row.IsChecked = false;
                                    _row.EndEdit();
                                }    
                                //Bản chất chỉ đánh dấu phiếu đã được kí để ngăn hủy, xóa hoặc bắt chặt thao tác trên HIS.
                                ////Khi nào đóng hồ sơ bệnh án sẽ đẩy PDF chính thức lên server và update đường dẫn file pdf vào emr documents để phục vụ tra cứu và lưu trữ
                            }
                        }
                        if (num > 0)
                        {
                            Utility.ShowMsg("Đã ký các phiếu thành công");
                        }
                    }
                }
                //Load lại với chữ ký
              if(refresh)  grdEmrDocuments_SelectionChanged(grdEmrDocuments, new EventArgs());
            }
            catch (Exception ex)
            {


            }
            finally
            {
                grdEmrDocuments.CurrentRow.BeginEdit();
                grdEmrDocuments.CurrentRow.IsChecked = false;
                grdEmrDocuments.CurrentRow.EndEdit();
            }
        }
        /// <summary>
        /// Hàm riêng vì 1 phiếu chỉ định, nhiều tờ tách riêng, nhưng chỉ có 1 dòng thông tin ký theo id phiếu
        /// </summary>
        private void KyphieuChidinh()
        {
            try
            {
                bool refresh = false;
                if (grdEmrDocuments.GetCheckedRows().Count() <= 0)
                {
                    grdEmrDocuments.CurrentRow.BeginEdit();
                    grdEmrDocuments.CurrentRow.IsChecked = true;
                    grdEmrDocuments.CurrentRow.EndEdit();
                }
                List<string> lstIdFiles = (from p in grdEmrDocuments.GetCheckedRows() select Utility.sDbnull(p.Cells[EmrDocument.Columns.IdFile].Value)).Distinct().ToList<string>();
                //Lấy về thông tin kí của người dùng trên các file đang chọn
                DataTable dtSignInfo = SPs.EmrLaythongtinChukyTrenphieu(string.Join(",", lstIdFiles), globalVariables.UserName, 100).GetDataSet().Tables[0];
                //Lấy về danh sách các file liên quan đến người dùng
                List<long> lstIdFiles_NguoiKy = dtSignInfo.AsEnumerable().Select(c => Utility.Int64Dbnull(c["id_phieu"])).Distinct().ToList<long>();
                //Lấy về các phiếu đang chọn mà không liên quan đến người dùng(gặp người dùng ẩu chọn bừa khi ký)
                List<long> lstIdPhieu_Other = (from p in grdEmrDocuments.GetCheckedRows() where !lstIdFiles_NguoiKy.Contains(Utility.Int64Dbnull(p.Cells["id_phieu"].Value)) select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                List<long> lstIdPhieu_Daky = (from p in dtSignInfo.AsEnumerable() where lstIdFiles_NguoiKy.Contains(Utility.Int64Dbnull(p["id_phieu"])) && Utility.ByteDbnull(p["tthai_ky"]) == 1 select Utility.Int64Dbnull(p[EmrDocument.Columns.IdPhieu])).Distinct().ToList<long>();
                List<long> lstIdPhieu_ChuaKy = (from p in dtSignInfo.AsEnumerable() where lstIdFiles_NguoiKy.Contains(Utility.Int64Dbnull(p["id_phieu"])) && Utility.ByteDbnull(p["tthai_ky"]) == 0 select Utility.Int64Dbnull(p[EmrDocument.Columns.IdPhieu])).Distinct().ToList<long>();
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
                        GridEXRow[] lstCheckedRows = grdEmrDocuments.GetCheckedRows();
                        foreach (GridEXRow _row in lstCheckedRows)
                        {
                            if (lstIdFiles_NguoiKy.Contains(Utility.Int64Dbnull(_row.Cells["id_phieu"].Value)))//&& Utility.sDbnull(_row.Cells["tthai_kyso"].Value) == "0" )
                            {
                                long id_phieu = Utility.Int64Dbnull(_row.Cells[EmrDocument.Columns.IdPhieu].Value);
                                long IdFile = Utility.Int64Dbnull(_row.Cells[EmrDocument.Columns.IdFile].Value);
                                string LoaiPhieuHis = Utility.sDbnull(_row.Cells[EmrDocument.Columns.LoaiPhieuHis].Value);
                                if (LoaiPhieuHis == Loaiphieu_HIS.PHIEUDIEUTRI && id_phieu == -1)
                                {
                                    frm_chonky_todieutri _chonky_todieutri = new frm_chonky_todieutri(objLuotkham, globalVariables.UserName);
                                    if (_chonky_todieutri.ShowDialog() == DialogResult.OK)
                                    {
                                        foreach (long id_p in _chonky_todieutri.lstIdphieu)
                                            num += SPs.EmrThaydoitrangthai(IdFile, id_p, LoaiPhieuHis, 2, true, globalVariables.UserName, DateTime.Now).Execute();
                                    }
                                }
                                else
                                {
                                    num += SPs.EmrThaydoitrangthai(-1, id_phieu, LoaiPhieuHis, 2, true, globalVariables.UserName, DateTime.Now).Execute();
                                }
                                if (num > 0)
                                {
                                    refresh = true;
                                    _row.BeginEdit();
                                    _row.IsChecked = false;
                                    _row.EndEdit();
                                }
                                //Bản chất chỉ đánh dấu phiếu đã được kí để ngăn hủy, xóa hoặc bắt chặt thao tác trên HIS.
                                ////Khi nào đóng hồ sơ bệnh án sẽ đẩy PDF chính thức lên server và update đường dẫn file pdf vào emr documents để phục vụ tra cứu và lưu trữ
                            }
                        }
                        if (num > 0)
                        {
                            Utility.ShowMsg("Đã ký các phiếu thành công");
                        }
                    }
                }
                //Load lại với chữ ký
                if (refresh) grdEmrDocuments_SelectionChanged(grdEmrDocuments, new EventArgs());
            }
            catch (Exception ex)
            {


            }
            finally
            {
                grdEmrDocuments.CurrentRow.BeginEdit();
                grdEmrDocuments.CurrentRow.IsChecked = false;
                grdEmrDocuments.CurrentRow.EndEdit();
            }
        }
        /// <summary>
        /// HỦy chữ ký điện tử cho phiếu chỉ định
        /// </summary>
        private void HuyChuKyDientu()
        {
            try
            {
                bool refresh = false;
                if (grdEmrDocuments.GetCheckedRows().Count() <= 0)
                {
                    grdEmrDocuments.CurrentRow.BeginEdit();
                    grdEmrDocuments.CurrentRow.IsChecked = true;
                    grdEmrDocuments.CurrentRow.EndEdit();
                }
                List<string> lstIdFiles = (from p in grdEmrDocuments.GetCheckedRows() select Utility.sDbnull(p.Cells[EmrDocument.Columns.IdFile].Value)).Distinct().ToList<string>();
                //Lấy về thông tin kí của người dùng trên các file đang chọn
                DataTable dtSignInfo = SPs.EmrLaythongtinChukyTrenphieu(string.Join(",", lstIdFiles), globalVariables.UserName, 100).GetDataSet().Tables[0];
                //Lấy về danh sách các file liên quan đến người dùng
                List<long> lstIdFiles_NguoiKy = dtSignInfo.AsEnumerable().Select(c => Utility.Int64Dbnull(c["id_phieu"])).Distinct().ToList<long>();
                //Lấy về các phiếu đang chọn mà không liên quan đến người dùng(gặp người dùng ẩu chọn bừa khi ký)
                List<long> lstIdPhieu_Other = (from p in grdEmrDocuments.GetCheckedRows() where !lstIdFiles_NguoiKy.Contains(Utility.Int64Dbnull(p.Cells["id_phieu"].Value)) select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                List<long> lstIdPhieu_Daky = (from p in dtSignInfo.AsEnumerable() where lstIdFiles_NguoiKy.Contains(Utility.Int64Dbnull(p["id_phieu"])) && Utility.ByteDbnull(p["tthai_ky"]) == 1 select Utility.Int64Dbnull(p[EmrDocument.Columns.IdPhieu])).Distinct().ToList<long>();
                List<long> lstIdPhieu_ChuaKy = (from p in dtSignInfo.AsEnumerable() where lstIdFiles_NguoiKy.Contains(Utility.Int64Dbnull(p["id_phieu"])) && Utility.ByteDbnull(p["tthai_ky"]) == 0 select Utility.Int64Dbnull(p[EmrDocument.Columns.IdPhieu])).Distinct().ToList<long>();
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
                        GridEXRow[] lstCheckedRows = grdEmrDocuments.GetCheckedRows();
                        foreach (GridEXRow _row in lstCheckedRows)
                        {
                            if (lstIdFiles_NguoiKy.Contains(Utility.Int64Dbnull(_row.Cells["id_phieu"].Value)))//&& Utility.sDbnull(_row.Cells["tthai_kyso"].Value) == "1")
                            {
                                long id_phieu = Utility.Int64Dbnull(_row.Cells[EmrDocument.Columns.IdPhieu].Value);
                                long IdFile = Utility.Int64Dbnull(_row.Cells[EmrDocument.Columns.IdFile].Value);
                                string LoaiPhieuHis = Utility.sDbnull(_row.Cells[EmrDocument.Columns.LoaiPhieuHis].Value);
                                num += SPs.EmrThaydoitrangthai(-1, id_phieu, LoaiPhieuHis, 2, false, globalVariables.UserName, null).Execute();
                                if (num > 0)
                                {
                                    refresh = true;
                                    _row.BeginEdit();
                                    _row.IsChecked = false;
                                    _row.EndEdit();
                                }
                                //Bản chất chỉ đánh dấu phiếu đã được kí để ngăn hủy, xóa hoặc bắt chặt thao tác trên HIS.
                                ////Khi nào đóng hồ sơ bệnh án sẽ đẩy PDF chính thức lên server và update đường dẫn file pdf vào emr documents để phục vụ tra cứu và lưu trữ
                            }
                        }
                        if (num > 0)
                        {
                            Utility.ShowMsg("Đã Hủy ký các phiếu thành công");
                        }
                    }
                }
                if (refresh) grdEmrDocuments_SelectionChanged(grdEmrDocuments, new EventArgs());
            }
            catch (Exception ex)
            {


            }
            finally
            {
                grdEmrDocuments.CurrentRow.BeginEdit();
                grdEmrDocuments.CurrentRow.IsChecked = false;
                grdEmrDocuments.CurrentRow.EndEdit();
            }
        }
        private void cmdHuyKyDientu_Click(object sender, EventArgs e)
        {
            try
            {
                EmrDocument objDoc = EmrDocument.FetchByID(Utility.Int64Dbnull(currRow.Cells[EmrDocument.Columns.IdFile].Value));
                if (objDoc.LoaiPhieuHis == Loaiphieu_HIS.PHIEUCHIDINH)
                {
                    HuyChuKyDientu();
                    return;
                }
                bool refresh = false;
                if (grdEmrDocuments.GetCheckedRows().Count() <= 0)
                {
                    grdEmrDocuments.CurrentRow.BeginEdit();
                    grdEmrDocuments.CurrentRow.IsChecked = true;
                    grdEmrDocuments.CurrentRow.EndEdit();
                }
                List<string> lstIdFiles = (from p in grdEmrDocuments.GetCheckedRows() select Utility.sDbnull(p.Cells[EmrDocument.Columns.IdFile].Value)).Distinct().ToList<string>();
                //Lấy về thông tin kí của người dùng trên các file đang chọn
                DataTable dtSignInfo = SPs.EmrLaythongtinChukyTrenphieu(string.Join(",", lstIdFiles), globalVariables.UserName,100).GetDataSet().Tables[0];
                //Lấy về danh sách các file liên quan đến người dùng
                List<long> lstIdFiles_NguoiKy = dtSignInfo.AsEnumerable().Select(c => Utility.Int64Dbnull(c["file_id"])).Distinct().ToList<long>();
                //Lấy về các phiếu đang chọn mà không liên quan đến người dùng(gặp người dùng ẩu chọn bừa khi ký)
                List<long> lstIdPhieu_Other = (from p in grdEmrDocuments.GetCheckedRows() where !lstIdFiles_NguoiKy.Contains(Utility.Int64Dbnull(p.Cells["id_file"].Value)) select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                List<long> lstIdPhieu_Daky = (from p in dtSignInfo.AsEnumerable() where lstIdFiles_NguoiKy.Contains(Utility.Int64Dbnull(p["file_id"])) && Utility.ByteDbnull(p["tthai_ky"]) == 1 select Utility.Int64Dbnull(p[EmrDocument.Columns.IdPhieu])).Distinct().ToList<long>();
                List<long> lstIdPhieu_ChuaKy = (from p in dtSignInfo.AsEnumerable() where lstIdFiles_NguoiKy.Contains(Utility.Int64Dbnull(p["file_id"])) && Utility.ByteDbnull(p["tthai_ky"]) == 0 select Utility.Int64Dbnull(p[EmrDocument.Columns.IdPhieu])).Distinct().ToList<long>();
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
                        GridEXRow[] lstCheckedRows = grdEmrDocuments.GetCheckedRows();
                        foreach (GridEXRow _row in lstCheckedRows)
                        {
                            if (lstIdFiles_NguoiKy.Contains(Utility.Int64Dbnull(_row.Cells["id_file"].Value)) )//&& Utility.sDbnull(_row.Cells["tthai_kyso"].Value) == "1")
                            {
                                long id_phieu = Utility.Int64Dbnull(_row.Cells[EmrDocument.Columns.IdPhieu].Value);
                                long IdFile = Utility.Int64Dbnull(_row.Cells[EmrDocument.Columns.IdFile].Value);
                                string LoaiPhieuHis = Utility.sDbnull(_row.Cells[EmrDocument.Columns.LoaiPhieuHis].Value);
                                num += SPs.EmrThaydoitrangthai(IdFile,id_phieu, LoaiPhieuHis, 2, false, globalVariables.UserName, null).Execute();
                                if (num > 0)
                                {
                                    refresh = true;
                                    _row.BeginEdit();
                                    _row.IsChecked = false;
                                    _row.EndEdit();
                                }
                                //Bản chất chỉ đánh dấu phiếu đã được kí để ngăn hủy, xóa hoặc bắt chặt thao tác trên HIS.
                                ////Khi nào đóng hồ sơ bệnh án sẽ đẩy PDF chính thức lên server và update đường dẫn file pdf vào emr documents để phục vụ tra cứu và lưu trữ
                            }
                        }
                        if (num > 0)
                        {
                            Utility.ShowMsg("Đã Hủy ký các phiếu thành công");
                        }
                    }
                }
                if (refresh) grdEmrDocuments_SelectionChanged(grdEmrDocuments, new EventArgs());
            }
            catch (Exception ex)
            {


            }
            finally
            {
                grdEmrDocuments.CurrentRow.BeginEdit();
                grdEmrDocuments.CurrentRow.IsChecked = false;
                grdEmrDocuments.CurrentRow.EndEdit();
            }
        }
        private void cmdChuyenGay_Click(object sender, EventArgs e)
        {
            List<string> lstIdFile = (from p in grdEmrDocuments.GetCheckedRows() select Utility.sDbnull(p.Cells[EmrDocument.Columns.IdFile].Value)).ToList<string>();
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
                List<long> lstIdPhieu_Other = (from p in grdEmrDocuments.GetCheckedRows() where Utility.sDbnull(p.Cells["nguoi_tao"].Value) != globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                List<long> lstIdPhieu_DaAn = (from p in grdEmrDocuments.GetCheckedRows() where Utility.sDbnull(p.Cells["tthai_an"].Value) == "1" && Utility.sDbnull(p.Cells["nguoi_tao"].Value) == globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                List<long> lstIdPhieu_ChuaAn = (from p in grdEmrDocuments.GetCheckedRows() where Utility.sDbnull(p.Cells["tthai_an"].Value) == "0" && Utility.sDbnull(p.Cells["nguoi_tao"].Value) == globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
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
                        foreach (GridEXRow _row in grdEmrDocuments.GetCheckedRows())
                        {
                            if (Utility.sDbnull(_row.Cells["tthai_an"].Value) == "0" && Utility.sDbnull(_row.Cells["nguoi_tao"].Value) == globalVariables.UserName)
                            {
                                long id_phieu = Utility.Int64Dbnull(_row.Cells[EmrDocument.Columns.IdPhieu].Value);
                                long IdFile = Utility.Int64Dbnull(_row.Cells[EmrDocument.Columns.IdFile].Value);
                                string LoaiPhieuHis = Utility.sDbnull(_row.Cells[EmrDocument.Columns.LoaiPhieuHis].Value);
                                num += SPs.EmrThaydoitrangthai(IdFile,id_phieu, LoaiPhieuHis, 0, true, globalVariables.UserName, DateTime.Now).Execute();

                            }
                        }
                        if (num > 0)
                        {
                            Utility.ShowMsg("Đã ẩn các phiếu thành công");
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
                List<long> lstIdPhieu_Other = (from p in grdEmrDocuments.GetCheckedRows() where Utility.sDbnull(p.Cells["nguoi_tao"].Value) != globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                List<long> lstIdPhieu_DaAn = (from p in grdEmrDocuments.GetCheckedRows() where Utility.sDbnull(p.Cells["tthai_an"].Value) == "1" && Utility.sDbnull(p.Cells["nguoi_tao"].Value) == globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                List<long> lstIdPhieu_ChuaAn = (from p in grdEmrDocuments.GetCheckedRows() where Utility.sDbnull(p.Cells["tthai_an"].Value) == "0" && Utility.sDbnull(p.Cells["nguoi_tao"].Value) == globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
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
                        foreach (GridEXRow _row in grdEmrDocuments.GetCheckedRows())
                        {
                            if (Utility.sDbnull(_row.Cells["tthai_an"].Value) == "1" && Utility.sDbnull(_row.Cells["nguoi_tao"].Value) == globalVariables.UserName)
                            {
                                long id_phieu = Utility.Int64Dbnull(_row.Cells[EmrDocument.Columns.IdPhieu].Value);
                                long IdFile = Utility.Int64Dbnull(_row.Cells[EmrDocument.Columns.IdFile].Value);
                                string LoaiPhieuHis = Utility.sDbnull(_row.Cells[EmrDocument.Columns.LoaiPhieuHis].Value);
                                num += SPs.EmrThaydoitrangthai(IdFile, id_phieu, LoaiPhieuHis, 0, false, globalVariables.UserName, DateTime.Now).Execute();

                            }
                        }
                        if (num > 0)
                        {
                            Utility.ShowMsg("Đã Hiển thị lại các phiếu thành công");
                        }
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }
        public FTPclient FtpClientAttatchmentFiles;
        private string FtpClientCurrentDirectoryAttatchmentFiles = "";
        private readonly string _baseDirectoryAttatchmentFiles = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "emr_attachmentfiles\\");
        int num = 0;

        private void cmdXoaphieu_Click(object sender, EventArgs e)
        {

            try
            {

                if (grdEmrDocuments.GetCheckedRows().Count() <= 0)
                {
                    grdEmrDocuments.CurrentRow.BeginEdit();
                    grdEmrDocuments.CurrentRow.IsChecked = true;
                    grdEmrDocuments.CurrentRow.EndEdit();
                }

                grdEmrDocuments.SelectionChanged -= grdEmrDocuments_SelectionChanged;

                if(!Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa các phiếu đang chọn hay không?","Xác nhận xóa",true))
                {
                    return;
                }    
                foreach (GridEXRow row in grdEmrDocuments.GetCheckedRows())
                {
                    string nguon_tao = Utility.sDbnull(row.Cells["nguon_tao"].Value);
                    string nguoi_tao = Utility.sDbnull(row.Cells["nguoi_tao"].Value);
                    string ten_phieu = Utility.sDbnull(row.Cells["ten_phieu"].Value);
                    string loai_phieu_his = Utility.sDbnull(row.Cells["loai_phieu_his"].Value);
                    string file_path = Utility.sDbnull(row.Cells["file_path"].Value);
                    string file_name = Utility.sDbnull(row.Cells["file_in"].Value);
                    long id_phieu = Utility.Int64Dbnull(row.Cells["id_phieu"].Value);
                    long id_file = Utility.Int64Dbnull(row.Cells["id_file"].Value);
                    if (Utility.Coquyen("EMR_XOA_PHIEU") || globalVariables.UserName == nguoi_tao)
                    {
                        EmrDocument objEmrDoc = EmrDocument.FetchByID(id_file);
                        if (objEmrDoc == null)
                        {
                            if (Utility.AcceptQuestion("Phiếu không tồn tại (Có thể bị xóa bởi người khác). Vui lòng kiểm tra nội bộ để biết thêm chi tiết\nBạn có muốn tiếp tục xóa các phiếu còn lại hay không?", "Thông báo", true))
                            {
                                continue;
                            }
                            else
                                break;
                        }
                        else
                        {
                            if (Utility.Bool2Bool(objEmrDoc.TthaiKydientu))
                            {
                                if (Utility.AcceptQuestion(string.Format("Phiếu {0} được kí điện tử bởi {1} nên bạn không được phép xóa khỏi hệ thống\nLiên hệ người kí thực hiện hủy kí trước khi xóa\nBạn có muốn tiếp tục xóa các phiếu còn lại hay không?", ten_phieu, objEmrDoc.NguoiKydientu), "Thông báo", true))
                                {
                                    continue;
                                }
                                else
                                    break;
                            }
                            num = SPs.EmrXoaPhieu(id_file, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, id_phieu, loai_phieu_his, "").Execute();// new Delete().From(EmrDocument.Schema).Where(EmrDocument.Columns.IdFile).IsEqualTo(id_file).Execute();
                            if (num > 0)
                            {
                                if (nguon_tao == "5")
                                {
                                    //Thực hiện xóa phiếu, xóa khỏi cả từ Server
                                    if (FtpClientAttatchmentFiles.FtpFileExists(string.Format("{0}/{1}", file_path, file_name)))
                                    {
                                        FtpClientAttatchmentFiles.FtpDelete(string.Format("{0}/{1}", file_path, file_name));

                                    }
                                }
                                row.Delete();
                            }
                        }
                    }
                    else
                    {
                        if (Utility.AcceptQuestion(string.Format("Phiếu {0} được tạo bởi người dùng {1} nên bạn không được phép xóa khỏi hệ thống.\nVui lòng kiểm tra lại\nBạn có muốn tiếp tục xóa các phiếu còn lại hay không?", ten_phieu, nguoi_tao), "Thông báo", true))
                        {
                            continue;
                        }
                        else
                            break;
                    }
                }
                if (num > 0)
                {
                    ResetView();
                    Utility.ShowMsg("Đã Hủy các phiếu thành công");
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                grdEmrDocuments.SelectionChanged += grdEmrDocuments_SelectionChanged;
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
                List<long> lstIdPhieu_Other = (from p in grdEmrDocuments.GetCheckedRows() where Utility.sDbnull(p.Cells["nguoi_tao"].Value) != globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                List<long> lstIdPhieu_DaHuy = (from p in grdEmrDocuments.GetCheckedRows() where Utility.sDbnull(p.Cells["tthai_huy"].Value) == "1" && Utility.sDbnull(p.Cells["nguoi_tao"].Value) == globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
                List<long> lstIdPhieu_ChuaHuy = (from p in grdEmrDocuments.GetCheckedRows() where Utility.sDbnull(p.Cells["tthai_huy"].Value) == "0" && Utility.sDbnull(p.Cells["nguoi_tao"].Value) == globalVariables.UserName select Utility.Int64Dbnull(p.Cells[EmrDocument.Columns.IdPhieu].Value)).Distinct().ToList<long>();
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
                        foreach (GridEXRow _row in grdEmrDocuments.GetCheckedRows())
                        {
                            if (Utility.sDbnull(_row.Cells["tthai_huy"].Value) == "1" && Utility.sDbnull(_row.Cells["nguoi_tao"].Value) == globalVariables.UserName)
                            {
                                long id_phieu = Utility.Int64Dbnull(_row.Cells[EmrDocument.Columns.IdPhieu].Value);
                                long IdFile = Utility.Int64Dbnull(_row.Cells[EmrDocument.Columns.IdFile].Value);
                                string LoaiPhieuHis = Utility.sDbnull(_row.Cells[EmrDocument.Columns.LoaiPhieuHis].Value);
                                num += SPs.EmrThaydoitrangthai(IdFile,id_phieu, LoaiPhieuHis, 1, false, "", null).Execute();

                            }
                        }
                        if (num > 0)
                        {
                            Utility.ShowMsg("Đã khôi phục các phiếu Hủy thành công");
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
            List<string> lstIdFile = (from p in grdEmrDocuments.GetCheckedRows() select Utility.sDbnull(p.Cells[EmrDocument.Columns.IdFile].Value)).ToList<string>();
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
            try
            {
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Bạn cần chọn người bệnh trước khi thực hiện tạo hồ sơ EMR");
                    return;
                }
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn hệ thống quét toàn bộ các phiếu của người bệnh {0}-{1} để làm hồ sơ EMR hay không?\nChú ý:Tính năng này chỉ nên được dùng đối với các ca phát sinh trước khi triển khai EMR hoặc có sự cố thiếu phiếu(Do nguyên nhân chủ quan, khách quan nào đó)", ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text, ucThongtinnguoibenh_emr_basic1.txtTenBN.Text), "Xác nhận tạo phiếu làm hồ sơ EMR", true))
                {
                    int num = 0;
                    StoredProcedure sp = SPs.EmrLaydanhsachDocumentsFromTables(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, "", 0, num);
                    DataTable dtDocs = sp.GetDataSet().Tables[0];//Lấy về để thêm thông tin ký trên các phiếu
                    sp.OutputValues.ForEach(delegate (object objOutput)
                    {
                        num = Utility.Int32Dbnull(objOutput);
                    });
                    if (num > 0)
                        Utility.ShowMsg(string.Format("Đã đưa tổng số {0} phiếu quét được từ hệ thống liên quan đến người bệnh {1}-{2}. Nhấn OK để bắt đầu dựng hồ sơ EMR", num, ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text, ucThongtinnguoibenh_emr_basic1.txtTenBN.Text));
                    isAllowSelectionChanged = false;
                    if (optPdfView.Checked)
                        pdfViewer1.CloseDocument();
                    else
                        richEdit.CreateNewDocument();
                    dtEmrDocuments = SPs.EmrLaydanhsachDocuments(objLuotkham.MaLuotkham, -1, globalVariables.UserName, Utility.ByteDbnull(globalVariables.IsAdmin || globalVariables.isSuperAdmin || Utility.Coquyen("EMR_FULL") ? 1 : 0), "").GetDataSet().Tables[0];
                    Utility.SetDataSourceForDataGridEx_Basic(grdEmrDocuments, dtEmrDocuments, true, true, "1=1", "");
                    isAllowSelectionChanged = true;

                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Bạn cần chọn người bệnh trước khi thực hiện tạo hồ sơ EMR");
                    return;
                }
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn reset lại toàn bộ hồ sơ của người bệnh đang chọn hay không?\nChú ý: Các phiếu đã được Duyệt, Xác nhận, Ký số, Ký điện tử sẽ không bị ảnh hưởng", ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text, ucThongtinnguoibenh_emr_basic1.txtTenBN.Text), "Xác nhận reset hồ sơ EMR", true))
                {
                    int num = 0;
                    StoredProcedure sp = SPs.EmrLaydanhsachDocumentsFromTables(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, "", 1, num);
                    DataTable dtDocs = sp.GetDataSet().Tables[0];//Lấy về để thêm thông tin ký trên các phiếu
                    //new EmrDocuments().AddSignInfor(dtDocs,)
                    Utility.ShowMsg(string.Format("Đã reset toàn bộ các phiếu liên quan đến người bệnh {0}-{1}. Nhấn OK để bắt đầu dựng hồ sơ EMR", ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text, ucThongtinnguoibenh_emr_basic1.txtTenBN.Text));
                    isAllowSelectionChanged = false;
                    if (optPdfView.Checked)
                    {
                        if (pdfViewer1 != null) pdfViewer1.CloseDocument();
                    }
                    else
                        richEdit.CreateNewDocument();
                    dtEmrDocuments = SPs.EmrLaydanhsachDocuments(objLuotkham.MaLuotkham, -1, globalVariables.UserName, Utility.ByteDbnull(globalVariables.IsAdmin || globalVariables.isSuperAdmin || Utility.Coquyen("EMR_FULL") ? 1 : 0), "").GetDataSet().Tables[0];
                    Utility.SetDataSourceForDataGridEx_Basic(grdEmrDocuments, dtEmrDocuments, true, true, "1=1", "");
                    isAllowSelectionChanged = true;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
           
        }
        EmrDocuments emrDoc = new EmrDocuments();
        public void AddSignInfor(EmrDocument objDoc)
        {
            try
            {
                if (objDoc == null || !Utility.Bool2Bool(objDoc.ManualGen)) return;
                List<KeyValuePair<string, string>> lstNguoiKy = new List<KeyValuePair<string, string>>();

                DataTable dtCheck = new Select().From(EmrFileSignInfor.Schema)
                 .Where(EmrFileSignInfor.Columns.FileId).IsEqualTo(objDoc.IdFile)
                 .ExecuteDataSet().Tables[0];
                if (dtCheck.Rows.Count > 0) return;//Không thêm lại nữa
                if (objDoc.LoaiPhieuHis == Loaiphieu_HIS.PHIEUDIEUTRI)
                {
                    DataTable dtSignInfor = SPs.EmrLaythongtinChukyPhieu(objDoc.MaLuotkham, objDoc.IdBenhnhan, objDoc.IdPhieu, objDoc.LoaiPhieuHis, objDoc.LoaiphieuCha).GetDataSet().Tables[0];
                    foreach (DataRow dr in dtSignInfor.Rows)
                    {
                        DmucNhanvien objBacsi = DmucNhanvien.FetchByID(Utility.Int16Dbnull(dr["id_bacsi"]));
                        if (objBacsi != null)
                        {
                            lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                            EmrFileSignInfor fsi = new EmrFileSignInfor();
                            fsi.NguoiKy = objBacsi.UserName;
                            fsi.IdBenhnhan = objLuotkham.IdBenhnhan;
                            fsi.MaLuotkham = objLuotkham.MaLuotkham;
                            fsi.LoaiphieuHis = objDoc.LoaiPhieuHis;
                            fsi.LoaiphieuCha = objDoc.LoaiphieuCha;
                            fsi.IdPhieu = Utility.Int64Dbnull(dr["id_phieudieutri"]);
                            fsi.TenVitriKy = "CKS_BACSI";
                            fsi.TthaiKy = false;
                            fsi.FileId = objDoc.IdFile;
                            fsi.Save();
                        }
                        objBacsi = DmucNhanvien.FetchByID(Utility.Int16Dbnull(dr["id_dieuduong"]));
                        if (objBacsi != null)
                        {
                            lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_DIEUDUONG"));
                            EmrFileSignInfor fsi = new EmrFileSignInfor();
                            fsi.NguoiKy = objBacsi.UserName;
                            fsi.IdBenhnhan = objLuotkham.IdBenhnhan;
                            fsi.MaLuotkham = objLuotkham.MaLuotkham;
                            fsi.LoaiphieuHis = objDoc.LoaiPhieuHis;
                            fsi.LoaiphieuCha = objDoc.LoaiphieuCha;
                            fsi.IdPhieu = Utility.Int64Dbnull(dr["id_phieudieutri"]);
                            fsi.TenVitriKy = "CKS_DIEUDUONG";
                            fsi.TthaiKy = false;
                            fsi.FileId = objDoc.IdFile;
                            fsi.Save();
                        }
                    }
                }
                else
                {
                    lstNguoiKy = emrDoc.GetThongtinKy(objDoc.IdPhieu.Value, objDoc.LoaiPhieuHis, objDoc.LoaiphieuCha);//username+ vị trí ký

                    foreach (var nguoiky in lstNguoiKy)
                    {
                        EmrFileSignInfor fsi = new EmrFileSignInfor();
                        fsi.NguoiKy = nguoiky.Key;
                        fsi.IdBenhnhan = objLuotkham.IdBenhnhan;
                        fsi.MaLuotkham = objLuotkham.MaLuotkham;
                        fsi.LoaiphieuHis = objDoc.LoaiPhieuHis;
                        fsi.LoaiphieuCha = objDoc.LoaiphieuCha;
                        fsi.IdPhieu = objDoc.IdPhieu;
                        fsi.TenVitriKy = nguoiky.Value;
                        fsi.TthaiKy = false;
                        fsi.FileId = objDoc.IdFile;
                        fsi.Save();
                    }
                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }

        }
        private void uiButton4_Click(object sender, EventArgs e)
        {

        }

       

        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void lnkClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          
        }

        private void cmdLoaiphieuHIS_SelectedIndexChanged(object sender, EventArgs e)
        {
            isAllowSelectionChanged = false;
            DoFilter();
            isAllowSelectionChanged = true;
            if (grdEmrDocuments.GetDataRows().Count() <= 0 || !Utility.isValidGrid(grdEmrDocuments))
                ResetView();
            else
                grdEmrDocuments.MoveFirst();
            grdEmrDocuments_SelectionChanged(grdEmrDocuments, e);
        }
        void DoFilter()
        {
            try
            {
                string Filter = "";
                if (Utility.sDbnull(cboLoaiphieuEmr.SelectedValue) == "" || Utility.sDbnull(cboLoaiphieuEmr.SelectedValue) == "-1")
                {

                }
                else
                    Filter = string.Format("ma_phieu_emr ='{0}'", Utility.sDbnull(cboLoaiphieuEmr.SelectedValue));
                if (Utility.sDbnull(cboLoaiphieuHIS.SelectedValue) == "" || Utility.sDbnull(cboLoaiphieuHIS.SelectedValue) == "-1")
                {

                }
                else//Có lọc loại phiếu HIS
                {
                    if (Filter == "")
                    {

                    }
                    else
                        Filter = string.Format("{0} and loai_phieu_his='{1}'", Filter, Utility.sDbnull(cboLoaiphieuHIS.SelectedValue));
                }
                if (optChuaky.Checked)
                {
                    if (Filter == "")
                    {
                        Filter = "tthai_kydientu=0";
                    }
                    else
                        Filter = string.Format("{0} and tthai_kydientu=0", Filter);
                }
                if (optDaky.Checked)
                {
                    if (Filter == "")
                    {
                        Filter = "tthai_kydientu=1";
                    }
                    else
                        Filter = string.Format("{0} and tthai_kydientu=1", Filter);
                }
                if (chkIsMe.Checked)
                {
                    if (Filter == "")
                    {
                        Filter = "isMine=1";
                    }
                    else
                        Filter = string.Format("{0} and isMine=1", Filter);
                }
                if (Filter == "")
                    Filter = "1=1";
                //uiStatusBar1.Panels["Filter"].Text = Filter;
              if(dtEmrDocuments!=null && dtEmrDocuments.Columns.Count>0 && dtEmrDocuments.Rows.Count>0)  dtEmrDocuments.DefaultView.RowFilter = Filter;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
           
        }
        private void cboLoaiphieuEmr_SelectedIndexChanged(object sender, EventArgs e)
        {
            isAllowSelectionChanged = false;
            DoFilter();
             isAllowSelectionChanged = true;
            if (grdEmrDocuments.GetDataRows().Count() <= 0 || !Utility.isValidGrid(grdEmrDocuments))
                ResetView();
            else
                grdEmrDocuments.MoveFirst();
            grdEmrDocuments_SelectionChanged(grdEmrDocuments, e);
        }

        private void optOrderbyGay_CheckedChanged(object sender, EventArgs e)
        {
            GroupByGay();
        }

        private void optOrderbyTime_CheckedChanged(object sender, EventArgs e)
        {
            GroupByThoiGian();
        }
        void GroupByGay()
        {
            try
            {
                var counts = dtEmrDocuments.AsEnumerable().GroupBy(x => x.Field<string>("ten_gay"))
                    .Select(g => new { g.Key, Count = g.Count() });
                var table = grdEmrDocuments.RootTable;
                var item = new GridEXGroupHeaderTotal();
                item.Column = table.Columns["report_code"];
                item.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count;
                item.TotalFormatMode = FormatMode.UseStringFormat;
                item.TotalFormatString = "(SL: {0} tờ)";
                item.Key = "GroupHeader_Count";
                // Thêm vào collection
                table.GroupHeaderTotals.Add(item);

                if (grdEmrDocuments.RootTable.Groups.Count <= 0)
                {
                    GridEXColumn gridExColumn = grdEmrDocuments.RootTable.Columns["ten_gay"];
                    var gridExGroup = new GridEXGroup(gridExColumn);
                    gridExGroup.GroupPrefix = "";
                    grdEmrDocuments.RootTable.Groups.Add(gridExGroup);
                }
                //Lọc theo thời gian lập phiếu tăng dần
                dtEmrDocuments.DefaultView.Sort =  "ngay_phieu,stt_phieu,ten_phieu";
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        void GroupByThoiGian()
        {
            try
            {
                grdEmrDocuments.RootTable.Groups.Clear();
                grdEmrDocuments.RootTable.GroupHeaderTotals.Clear();
                //Lọc theo thời gian lập phiếu tăng dần
                dtEmrDocuments.DefaultView.Sort = "ngay_phieu,stt_phieu,ten_phieu";
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void cmdNhaplieu_Click(object sender, EventArgs e)
        {
            ctxOtherFunctions.Show(cmdNhaplieu, new Point(0, cmdNhaplieu.Height));
        }

        private void chkIsMe_CheckedChanged(object sender, EventArgs e)
        {
            DoFilter();
        }

        private void optAll_CheckedChanged(object sender, EventArgs e)
        {
            DoFilter();
        }

        private void optChuaky_CheckedChanged(object sender, EventArgs e)
        {
            DoFilter();
        }

        private void optDaky_CheckedChanged(object sender, EventArgs e)
        {
            DoFilter();
        }

        private void cmdAddWord_Click(object sender, EventArgs e)
        {

        }

       

        private void mnuMove2Sign_Click(object sender, EventArgs e)
        {
            try
            {
                var document = richEdit.Document;
                foreach (string chuky in globalVariables.lstVitriky.Keys)
                {
                    DevExpress.XtraRichEdit.API.Native.Bookmark bookmark = document.Bookmarks[chuky];
                    if (bookmark != null)
                    {
                        
                        int bmStart = bookmark.Range.Start.ToInt();

                        foreach (DocumentImage img in document.Images)
                        {
                            int imgStart = img.Range.Start.ToInt();

                            if (imgStart >= bmStart)
                            {

                                // Di chuyển caret đến ảnh
                                document.CaretPosition = document.CreatePosition(img.Range.Start.ToInt());
                                document.Selection = document.CreateRange(img.Range.Start, img.Range.Length);
                                richEdit.ScrollToCaret();
                                break;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {

               
            }
        }

        private void cboTagFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(Utility.sDbnull(cboTagFields.SelectedValue,"").Length>0)
            //EmrUtils.InsertTagField(richEdit,Utility.sDbnull( cboTagFields.SelectedValue), Guid.NewGuid().ToString());
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
           //EmrUtils.LoadTemplateAndReplace(richEdit, templatefile, new Dictionary<string, string>
           // {
           //     ["HoTen"] = "Nguyễn Văn A",
           //     ["NgaySinh"] = "01/01/1990",
           //     ["GioiTinh"] = "Nam"
           // });
        }

        private void cmdPrintPreview_Click(object sender, EventArgs e)
        {
            richEdit.ShowPrintDialog();
        }

        private void cmdCollapse_Click(object sender, EventArgs e)
        {
            if (ucThongtinnguoibenh_emr_basic1.Height == 30)
            {
                cmdCollapse.Image = global::VMS.HIS.EMR.Properties.Resources.Up;
                ucThongtinnguoibenh_emr_basic1.Height = 204;
            }
            else
            {
                cmdCollapse.Image= global::VMS.HIS.EMR.Properties.Resources.Down;
                ucThongtinnguoibenh_emr_basic1.Height = 30;
            }
        }

    }
   
}
