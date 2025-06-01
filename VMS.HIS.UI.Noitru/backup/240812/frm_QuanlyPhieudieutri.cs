using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.CalendarCombo;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Janus.Windows.UI.Tab;
using Microsoft.VisualBasic;
using NLog;
using SubSonic;
using VNS.HIS.UI.Classess;
using VNS.Libs;
using VMS.HIS.DAL;

using VNS.HIS.UI.NGOAITRU;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;

using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.NOITRU;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.Classes;
using VNS.Libs.AppUI;
using VNS.UCs;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.HIS.UI.Forms.Cauhinh;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using VMS.HIS.Danhmuc;
using Aspose.Words;
using System.Transactions;

namespace VNS.HIS.UI.NOITRU
{
    /// <summary>
    /// 06/11/2013 3h57
    /// </summary>
    public partial class frm_QuanlyPhieudieutri : Form
    {
        KCB_THAMKHAM _KCB_THAMKHAM = new KCB_THAMKHAM();
        KCB_KEDONTHUOC _KCB_KEDONTHUOC = new KCB_KEDONTHUOC();
        KCB_CHIDINH_CANLAMSANG _KCB_CHIDINH_CANLAMSANG = new KCB_CHIDINH_CANLAMSANG();
        DMUC_CHUNG _DMUC_CHUNG = new DMUC_CHUNG();
       
        string SplitterPath = "";
        private readonly MoneyByLetter MoneyByLetter = new MoneyByLetter();
        private readonly Logger log;
        private readonly AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
        private readonly AutoCompleteStringCollection namesCollectionBenhChinh = new AutoCompleteStringCollection();
        private readonly AutoCompleteStringCollection namesCollectionBenhPhu = new AutoCompleteStringCollection();
        private readonly AutoCompleteStringCollection namesCollectionKetLuan = new AutoCompleteStringCollection();
        private readonly AutoCompleteStringCollection namesCollection_MaLanKham = new AutoCompleteStringCollection();

        private readonly string strSaveandprintPath = Application.StartupPath +
                                                      @"\CAUHINH\DefaultPrinter_PhieuHoaSinh.txt";
        private bool AllowTextChanged;
        bool _buttonClick = false;
        private int Distance = 488;
        public KcbLuotkham objLuotkham=null;
        public KcbDanhsachBenhnhan objBenhnhan=null;
        NoitruPhieudieutri objPhieudieutri = null;
        private bool Selected;
        private string TEN_BENHPHU = "";
        private bool hasMorethanOne = true;
        private string _rowFilter = "1=1";
        private bool b_Hasloaded;
        private DataSet ds = new DataSet();
        private DataTable dt_ICD_PHU = new DataTable();
        private bool m_blnHasLoaded;
        private bool isLike = true;
        private DataTable m_dtAssignDetail;
        private DataTable m_dtGoidichvu;
        private DataTable m_dtChandoanKCB=new DataTable();
        private DataTable m_dtDataVTYT = new DataTable();
        private DataTable m_dtChedoDinhduong = new DataTable();
        private DataTable m_dtDoctorAssign;
        private DataTable m_dtDonthuoc_ravien = new DataTable();
        private DataTable m_dtDonthuoc = new DataTable();
        private DataTable m_dtVTTH = new DataTable();
        private DataTable m_dtVTTH_tronggoi = new DataTable();
        private DataTable m_dtDonthuocChitiet_View = new DataTable();
        private DataTable m_dtDonthuocRavienChitiet_View = new DataTable();
        private DataTable m_dtVTTHChitiet_View = new DataTable();
        
        private DataTable m_dtReport = new DataTable();
        private DataTable m_hdt = new DataTable();
        private DataTable m_kl;
        private string m_strDefaultLazerPrinterName = "";
        action m_enActChandoan = action.FirstOrFinished;
        /// <summary>
        /// hàm thực hiện việc chọn bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string malankham = "";

        private DataTable m_dtPatients = new DataTable();
        private GridEXRow row_Selected;
        private bool trieuchung;
        List<string> lstVisibleColumns = new List<string>();
        List<string> lstResultColumns = new List<string>() { "ten_chitietdichvu", "ketqua_cls", "binhthuong_nam", "binhthuong_nu" };

        List<string> lstKQKhongchitietColumns = new List<string>() { "Ket_qua", "bt_nam", "bt_nu" };
        List<string> lstKQCochitietColumns = new List<string>() { "ten_chitietdichvu", "Ket_qua", "bt_nam", "bt_nu" };
        string MA_KHOA_THIEN = globalVariables.MA_KHOA_THIEN;
        string thamso = "DTRI_NOITRU";//ALL,DTRI_NOITRU,DTRI_NGOAITRU
         bool ycphanBG=true;
        /// <summary>
        /// 
        /// </summary>
         /// <param name="thamso">DTRI_NOITRU-1(hoặc 0)</param>
        public frm_QuanlyPhieudieutri(string thamso)
        {
            InitializeComponent();
            cmdScanFinger.Visible = true;
            this.thamso = thamso.Split('-')[0] ;
            ycphanBG = thamso.Split('-')[1] == "1";
            Utility.SetVisualStyle(this);
            KeyPreview = true;
            InitTitle();
            log = LogManager.GetCurrentClassLogger();
            SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
            dtInput_Date.Value =
                dtpCreatedDate.Value=dtpNgaylapphieu.Value = globalVariables.SysDate;
            webBrowser1.Url = new Uri(Application.StartupPath.ToString() + @"\editor\ckeditor_simple.html");
            dtFromDate.Value = globalVariables.SysDate;
            dtToDate.Value = globalVariables.SysDate;

            LoadLaserPrinters();
          
            CauHinhThamKham();
            Cauhinh();
            InitEvents();
        }
        void InitTitle()
        {
            switch (thamso)
            {
                case "DTRI_NOITRU":
                    Text = @"Quản lý điều trị nội trú";
                    break;
                case "DTRI_NGOAITRU":
                    Text = @"DQuản lý điều trị ngoại trú";
                    break;
                default:
                    Text = @"Quản lý điều trị nội trú";
                    break;
            }
        }
        void InitEvents()
        {
            FormClosing += frm_QuanlyPhieudieutri_FormClosing;
            Shown += frm_QuanlyPhieudieutri_Shown;
            Load+=new EventHandler(frm_QuanlyPhieudieutri_Load);
            KeyDown+=new KeyEventHandler(frm_QuanlyPhieudieutri_KeyDown);
           
            cmdSearch.Click+=new EventHandler(cmdSearch_Click);
            txtPatient_Code.KeyDown+=new KeyEventHandler(txtPatient_Code_KeyDown);
            grdList.ColumnButtonClick+=new ColumnActionEventHandler(grdPatientList_ColumnButtonClick);
            grdList.KeyDown += new KeyEventHandler(grdPatientList_KeyDown);
            grdList.DoubleClick += new EventHandler(grdPatientList_DoubleClick);
            grdList.MouseClick += new MouseEventHandler(grdPatientList_MouseClick);

            grdGoidichvu.SelectionChanged += grdGoidichvu_SelectionChanged;
            grdGoidichvu.DoubleClick += grdGoidichvu_DoubleClick;
            
            grdAssignDetail.CellUpdated += grdAssignDetail_CellUpdated;
            grdAssignDetail.SelectionChanged+=new EventHandler(grdAssignDetail_SelectionChanged);
            
            cmdInsertAssign.Click+=new EventHandler(cmdInsertAssign_Click);
            cmdUpdate.Click+=new EventHandler(cmdUpdate_Click);
            cmdDelteAssign.Click+=new EventHandler(cmdDelteAssign_Click);
            cboLaserPrinters.SelectedIndexChanged+=new EventHandler(cboLaserPrinters_SelectedIndexChanged);
            //cmdPrintAssign.Click+=new EventHandler(cmdPrintAssign_Click);

            cboA4Donthuoc.SelectedIndexChanged += new EventHandler(cboA4_SelectedIndexChanged);
            cboPrintPreviewDonthuoc.SelectedIndexChanged += new EventHandler(cboPrintPreview_SelectedIndexChanged);
            cmdCreateNewPres.Click+=new EventHandler(cmdCreateNewPres_Click);
            cmdUpdatePres.Click+=new EventHandler(cmdUpdatePres_Click);
            cmdDeletePres.Click+=new EventHandler(cmdDeletePres_Click);
           // cmdIndonthuoc.Click += new EventHandler(cmdPrintPres_Click);

            mnuDelDrug.Click+=new EventHandler(mnuDelDrug_Click);
            mnuDeleteCLS.Click+=new EventHandler(mnuDeleteCLS_Click);

            
            cmdThamkhamConfig.Click += new EventHandler(cmdThamkhamConfig_Click);
            cmdNoitruConfig.Click += cmdNoitruConfig_Click;
            chkAutocollapse.CheckedChanged += new EventHandler(chkAutocollapse_CheckedChanged);

            grd_ICD.ColumnButtonClick+=new ColumnActionEventHandler(grd_ICD_ColumnButtonClick);
            chkHienthichitiet.CheckedChanged += new EventHandler(chkHienthichitiet_CheckedChanged);

           
            cboA4Cls.SelectedIndexChanged += new EventHandler(cboA4Cls_SelectedIndexChanged);
            cboPrintPreviewCLS.SelectedIndexChanged += new EventHandler(cboPrintPreviewCLS_SelectedIndexChanged);

            cboA4Tomtatdieutringoaitru.SelectedIndexChanged += new EventHandler(cboA4Tomtatdieutringoaitru_SelectedIndexChanged);
            cboPrintPreviewTomtatdieutringoaitru.SelectedIndexChanged += new EventHandler(cboPrintPreviewTomtatdieutringoaitru_SelectedIndexChanged);

            txtChanDoan._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtChanDoan__OnShowData);
          
            txtNhommau._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtNhommau__OnShowData);

            txtNhommau._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtNhommau__OnSaveAs);
          
            txtChanDoan._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtChanDoan__OnSaveAs);

            grdPhieudieutri.ColumnButtonClick += new ColumnActionEventHandler(grdPhieudieutri_ColumnButtonClick);
            grdPhieudieutri.SelectionChanged += new EventHandler(grdPhieudieutri_SelectionChanged);

            cmdthemphieudieutri.Click += new EventHandler(cmdthemphieudieutri_Click);
            cmdSuaphieudieutri.Click += new EventHandler(cmdSuaphieudieutri_Click);
            cmdxoaphieudieutri.Click += new EventHandler(cmdxoaphieudieutri_Click);
            cmdInphieudieutri.Click += new EventHandler(cmdInphieudieutri_Click);
            cmdSaochep.Click += new EventHandler(cmdSaochep_Click);
            cmdCauHinh.Click += new EventHandler(cmdCauHinh_Click);

            cmdThemgoiDV.Click += cmdThemgoiDV_Click;
            cmdSuagoiDV.Click += cmdSuagoiDV_Click;
            cmdXoagoiDV.Click += cmdXoagoiDV_Click;
            cmdIngoiDV.Click += cmdIngoiDV_Click;

            cmdThemphieuVT.Click += cmdThemphieuVT_Click;
            cmdSuaphieuVT.Click += cmdSuaphieuVT_Click;
            cmdXoaphieuVT.Click += cmdXoaphieuVT_Click;
            cmdInphieuVT.Click += cmdInphieuVT_Click;

            cmdThemphieuVT_tronggoi.Click += cmdThemphieuVT_tronggoi_Click;
            cmdSuaphieuVT_tronggoi.Click += cmdSuaphieuVT_tronggoi_Click;
            cmdXoaphieuVT_tronggoi.Click += cmdXoaphieuVT_tronggoi_Click;
            cmdInphieuVT_tronggoi.Click += cmdInphieuVT_tronggoi_Click;

            grdPresDetail.SelectionChanged += grdPresDetail_SelectionChanged;
            grdDonthuocravien.SelectionChanged += grdDonthuocravien_SelectionChanged;
            cmdThemchandoan.Click += cmdThemchandoan_Click;
            cmdSuachandoan.Click += cmdSuachandoan_Click;
            cmdXoachandoan.Click += cmdXoachandoan_Click;
            cmdHuychandoan.Click += cmdHuychandoan_Click;
            cmdGhichandoan.Click += cmdGhichandoan_Click;
            grdChandoan.SelectionChanged += grdChandoan_SelectionChanged;

            cmdChuyengoi.Click += cmdChuyengoi_Click;
            cmdXoaDinhduong.Click += cmdXoaDinhduong_Click;
            grdChedoDinhduong.ColumnButtonClick += grdChedoDinhduong_ColumnButtonClick;
            cmdAdd.Click += cmdAdd_Click;
            txtSongay.KeyDown += txtSongay_KeyDown;
            dtpNgaylapphieu.KeyDown += dtpNgaylapphieu_KeyDown;
            chkViewAll.CheckedChanged += chkViewAll_CheckedChanged;
            txtHoly._OnShowData += txtHoly__OnShowData;
            txtHoly._OnEnterMe += txtHoly__OnEnterMe;
            txtChedodinhduong._OnShowData += txtChedodinhduong__OnShowData;
            grdBuongGiuong.MouseDoubleClick += grdBuongGiuong_MouseDoubleClick;
            txtMaluotkham.KeyDown += txtMaluotkham_KeyDown;

           
            grdPresDetail.UpdatingCell += grdPresDetail_UpdatingCell;
            txtMaBenhphu._OnEnterMe += txtMaBenhphu__OnEnterMe;
            grdChandoan.ColumnButtonClick += grdChandoan_ColumnButtonClick;

            cboKieuKham.ValueChanged += new EventHandler(cboKieuKham_ValueChanged);
            txtExamtypeCode._OnSelectionChanged += txtExamtypeCode__OnSelectionChanged;
            txtExamtypeCode._OnEnterMe += txtExamtypeCode__OnEnterMe;
            cmdXoaCongkham.Click += cmdXoaCongkham_Click;
            cmdInphieukham.Click += cmdInphieukham_Click;

            grdVTTH.UpdatingCell += grdVTTH_UpdatingCell;
            grdVTTH.SelectionChanged+=grdVTTH_SelectionChanged;
            txtCanhbao.LostFocus += txtCanhbao_LostFocus;
            txtCanhbao.GotFocus += txtCanhbao_GotFocus;

            grdDonthuocravien.UpdatingCell += grdDonthuocravien_UpdatingCell;
        }

        void grdDonthuocravien_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == "so_luongtralai")
                {
                    if (!Utility.Coquyen("thuoc_phieudieutri_nhaptrathuocthuanoitru"))
                    {
                        Utility.ShowMsg("Bạn không có quyền nhập số lượng trả thuốc thừa (thuoc_phieudieutri_nhaptrathuocthuanoitru). Vui lòng lien hệ quản trị hệ thống để được trợ giúp");
                        e.Value = e.InitialValue;
                        return;
                    }
                    if (!Utility.isValidGrid(grdDonthuocravien))
                    {
                        e.Value = e.InitialValue;
                        return;
                    }
                    if (isReadOnly)
                    {
                        e.Value = e.InitialValue;
                        return;
                    }
                    int TrangthaiTralai = Utility.Int32Dbnull(Utility.getValueOfGridCell(grdDonthuocravien, TPhieuCapphatChitiet.Columns.TrangthaiTralai), 0);
                    int IdPhieutralai = Utility.Int32Dbnull(Utility.getValueOfGridCell(grdDonthuocravien, TPhieuCapphatChitiet.Columns.IdPhieutralai), 0);
                    long IdChitiet = Utility.Int64Dbnull(Utility.getValueOfGridCell(grdDonthuocravien, TPhieuCapphatChitiet.Columns.IdChitiet), 0);
                    TPhieuCapphatChitiet objcapphatct = TPhieuCapphatChitiet.FetchByID(IdChitiet);
                    if (objcapphatct == null)
                    {
                        Utility.ShowMsg("Chi tiết này chưa được tổng hợp lĩnh thuốc nội trú nên bạn không thể sửa lại số lượng thực lĩnh(hoặc số lượng trả lại). Đề nghị kiểm tra lại");
                        e.Value = e.InitialValue;
                        return;
                    }
                    if (Utility.Int64Dbnull(objcapphatct.IdPhieuxuatthuocBenhnhan, -1) <= 0)//Bị hủy cấp phát trong khi vẫn đang mở form-->Chặn không cho sửa 
                    {
                        Utility.ShowMsg("Chi tiết này chưa được tổng hợp lĩnh thuốc nội trú nên bạn không thể sửa lại số lượng thực lĩnh(hoặc số lượng trả lại). Đề nghị kiểm tra lại");
                        e.Value = e.InitialValue;
                        return;
                    }
                    decimal soluong = Utility.DecimaltoDbnull(grdDonthuocravien.CurrentRow.Cells[TPhieuCapphatChitiet.Columns.SoLuong].Value, -1);
                    decimal soluongtralai = 0;
                    decimal thuclinh = soluong;
                    if (IdChitiet <= 0)
                    {
                        Utility.ShowMsg("Chi tiết này chưa được tổng hợp lĩnh thuốc nội trú nên bạn không thể sửa lại số lượng thực lĩnh(hoặc số lượng trả lại). Đề nghị kiểm tra lại");
                        return;
                    }
                    if (TrangthaiTralai == 1)
                    {
                        Utility.ShowMsg("Chi tiết này đã được tổng hợp thành phiếu trả thuốc thừa nên bạn không thể thay đổi lại thông tin số lượng thực lĩnh(hoặc số lượng trả lại) nữa\nMời bạn kiểm tra lại");
                        e.Value = e.InitialValue;
                        return;
                    }
                    if (TrangthaiTralai == 2)
                    {
                        Utility.ShowMsg("Chi tiết này đã được tổng hợp thành phiếu trả thuốc thừa và đã trả lại kho thuốc nên bạn không thể thay đổi lại thông tin số lượng thực lĩnh(hoặc số lượng trả lại) nữa\nMời bạn kiểm tra lại");
                        e.Value = e.InitialValue;
                        return;
                    }
                    if (IdPhieutralai > 0)
                    {
                        Utility.ShowMsg("Chi tiết này đã được tổng hợp thành phiếu trả thuốc thừa nên bạn không thể thay đổi lại thông tin số lượng thực lĩnh(hoặc số lượng trả lại) nữa\nMời bạn kiểm tra lại");
                        e.Value = e.InitialValue;
                        return;
                    }
                    decimal soluongsua = Utility.DecimaltoDbnull(e.Value, 0);
                    if (soluongsua > soluong)
                    {
                        Utility.ShowMsg(string.Format("Số lượng thực lĩnh (hoặc trả lại) phải nhỏ hơn hoặc bằng số lượng kê {0}", soluong.ToString()));
                        e.Value = e.InitialValue;
                        return;
                    }
                    grdDonthuocravien.CurrentRow.BeginEdit();
                    if (e.Column.Key == TPhieuCapphatChitiet.Columns.SoLuongtralai)
                    {
                        soluongtralai = soluongsua;
                        thuclinh = soluong - soluongsua;
                        grdDonthuocravien.CurrentRow.Cells[TPhieuCapphatChitiet.Columns.ThucLinh].Value = soluong - soluongsua;
                    }
                    else
                    {
                        thuclinh = soluongsua;
                        soluongtralai = soluong - soluongsua;
                        grdDonthuocravien.CurrentRow.Cells[TPhieuCapphatChitiet.Columns.SoLuongtralai].Value = soluong - soluongsua;
                    }
                    grdDonthuocravien.CurrentRow.EndEdit();
                    grdDonthuocravien.Refetch();
                    CapphatThuocKhoa.CapnhatThuclinh(
                        IdChitiet
                        , thuclinh
                        , soluongtralai
                        );
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void txtHoly__OnEnterMe()
        {
            txtChedodinhduong.RefreshGroup(txtHoly.myCode);
        }

        void grdDonthuocravien_SelectionChanged(object sender, EventArgs e)
        {
            RowThuocRavien = Utility.findthelastChild(grdDonthuocravien.CurrentRow);
            ModifyCommmands();
        }

        void txtCanhbao_GotFocus(object sender, EventArgs e)
        {
            txtCanhbao.PasswordChar = '\0';
        }

        void txtCanhbao_LostFocus(object sender, EventArgs e)
        {
            txtCanhbao.PasswordChar = '*';
        }
        

        void grdVTTH_SelectionChanged(object sender, EventArgs e)
        {
            RowVTTH =Utility.findthelastChild(grdVTTH.CurrentRow);
        }

        void grdVTTH_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == "so_luongtralai" )
                {
                    if (!Utility.isValidGrid(grdVTTH))
                    {
                        e.Value = e.InitialValue;
                        return;
                    }
                    if (isReadOnly)
                    {
                        e.Value = e.InitialValue;
                        return;
                    }
                    int TrangthaiTralai = Utility.Int32Dbnull(Utility.getValueOfGridCell(grdVTTH, TPhieuCapphatChitiet.Columns.TrangthaiTralai), 0);
                    int IdPhieutralai = Utility.Int32Dbnull(Utility.getValueOfGridCell(grdVTTH, TPhieuCapphatChitiet.Columns.IdPhieutralai), 0);
                    long IdChitiet = Utility.Int64Dbnull(Utility.getValueOfGridCell(grdVTTH, TPhieuCapphatChitiet.Columns.IdChitiet), 0);
                    TPhieuCapphatChitiet objcapphatct = TPhieuCapphatChitiet.FetchByID(IdChitiet);
                    if (objcapphatct == null)
                    {
                        Utility.ShowMsg("Chi tiết này chưa được tổng hợp lĩnh vật tư nội trú nên bạn không thể sửa lại số lượng thực lĩnh(hoặc số lượng trả lại). Đề nghị kiểm tra lại");
                        e.Value = e.InitialValue;
                        return;
                    }
                    if (Utility.Int64Dbnull(objcapphatct.IdPhieuxuatthuocBenhnhan, -1) <= 0)//Bị hủy cấp phát trong khi vẫn đang mở form-->Chặn không cho sửa 
                    {
                        Utility.ShowMsg("Chi tiết này chưa được tổng hợp lĩnh vật tư nội trú nên bạn không thể sửa lại số lượng thực lĩnh(hoặc số lượng trả lại). Đề nghị kiểm tra lại");
                        e.Value = e.InitialValue;
                        return;
                    }
                    decimal soluong = Utility.DecimaltoDbnull(grdVTTH.CurrentRow.Cells[TPhieuCapphatChitiet.Columns.SoLuong].Value, -1);
                    decimal soluongtralai = 0;
                    decimal thuclinh = soluong;
                    if (IdChitiet <= 0)
                    {
                        Utility.ShowMsg("Chi tiết này chưa được tổng hợp lĩnh vật tư nội trú nên bạn không thể sửa lại số lượng thực lĩnh(hoặc số lượng trả lại). Đề nghị kiểm tra lại");
                        return;
                    }
                    if (TrangthaiTralai == 1)
                    {
                        Utility.ShowMsg("Chi tiết này đã được tổng hợp thành phiếu trả vật tư thừa nên bạn không thể thay đổi lại thông tin số lượng thực lĩnh(hoặc số lượng trả lại) nữa\nMời bạn kiểm tra lại");
                        e.Value = e.InitialValue;
                        return;
                    }
                    if (TrangthaiTralai == 2)
                    {
                        Utility.ShowMsg("Chi tiết này đã được tổng hợp thành phiếu trả vật tư thừa và đã trả lại kho thuốc nên bạn không thể thay đổi lại thông tin số lượng thực lĩnh(hoặc số lượng trả lại) nữa\nMời bạn kiểm tra lại");
                        e.Value = e.InitialValue;
                        return;
                    }
                    if (IdPhieutralai > 0)
                    {
                        Utility.ShowMsg("Chi tiết này đã được tổng hợp thành phiếu trả vật tư thừa nên bạn không thể thay đổi lại thông tin số lượng thực lĩnh(hoặc số lượng trả lại) nữa\nMời bạn kiểm tra lại");
                        e.Value = e.InitialValue;
                        return;
                    }
                    decimal soluongsua = Utility.DecimaltoDbnull(e.Value, 0);
                    if (soluongsua > soluong)
                    {
                        Utility.ShowMsg(string.Format("Số lượng thực lĩnh (hoặc trả lại) phải nhỏ hơn hoặc bằng số lượng kê {0}", soluong.ToString()));
                        e.Value = e.InitialValue;
                        return;
                    }
                    grdVTTH.CurrentRow.BeginEdit();
                    if (e.Column.Key == TPhieuCapphatChitiet.Columns.SoLuongtralai)
                    {
                        soluongtralai = soluongsua;
                        thuclinh = soluong - soluongsua;
                        grdVTTH.CurrentRow.Cells[TPhieuCapphatChitiet.Columns.ThucLinh].Value = soluong - soluongsua;
                    }
                    else
                    {
                        thuclinh = soluongsua;
                        soluongtralai = soluong - soluongsua;
                        grdVTTH.CurrentRow.Cells[TPhieuCapphatChitiet.Columns.SoLuongtralai].Value = soluong - soluongsua;
                    }
                    grdVTTH.CurrentRow.EndEdit();
                    grdVTTH.Refetch();
                    CapphatThuocKhoa.CapnhatThuclinh(
                        IdChitiet
                        , thuclinh
                        , soluongtralai
                        );
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        

        void frm_QuanlyPhieudieutri_Shown(object sender, EventArgs e)
        {
            Try2Splitter();
        }

        void cmdInphieukham_Click(object sender, EventArgs e)
        {
            InCongkham();
        }
        #region In phiếu khám chuyên khoa
        void InCongkham()
        {
            try
            {
                if (grdCongkham.GetDataRows().Count() <= 0 || grdCongkham.CurrentRow.RowType != RowType.Record)
                    return;
                if (PropertyLib._MayInProperties.KieuInPhieuKCB == KieuIn.Innhiet)
                    InPhieuKCB();
                else
                    InphieuKham();
            }
            catch (Exception ex)
            {

                Utility.ShowMsg("Lỗi khi in phiếu khám\n" + ex.Message);
            }
        }
        private void InPhieuKCB()
        {

            int IdKham = -1;
            string tieude = "", reportname = "";
            //VMS.HISLink.Report.Report.tiepdon_PHIEUKHAM_NHIET crpt = new VMS.HISLink.Report.Report.tiepdon_PHIEUKHAM_NHIET();
            ReportDocument crpt = Utility.GetReport("tiepdon_PHIEUKHAM_NHIET", ref tieude, ref reportname);
            if (crpt == null) return;
            var objPrint = new frmPrintPreview("IN PHIẾU KHÁM", crpt, true, true);
            IdKham = GetrealRegID();
            try
            {
                KcbDangkyKcb objRegExam = KcbDangkyKcb.FetchByID(IdKham);
                DmucKhoaphong lDepartment = DmucKhoaphong.FetchByID(objRegExam.IdPhongkham);
                Utility.SetParameterValue(crpt, "PHONGKHAM", Utility.sDbnull(lDepartment.MaKhoaphong));
                Utility.SetParameterValue(crpt, "STT", Utility.sDbnull(objRegExam.SttKham, ""));
                Utility.SetParameterValue(crpt, "BENHAN", Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.MaLuotkham].Value, ""));
                Utility.SetParameterValue(crpt, "TENBN", Utility.sDbnull(grdList.CurrentRow.Cells[KcbDanhsachBenhnhan.Columns.TenBenhnhan].Value, ""));
                Utility.SetParameterValue(crpt, "GT_TUOI", Utility.sDbnull(grdList.CurrentRow.Cells[KcbDanhsachBenhnhan.Columns.GioiTinh].Value, "") + " - " + Utility.sDbnull(grdList.CurrentRow.Cells["Tuoi"].Value, "") + " tuổi");
                string SOTHE = "Không có thẻ";
                string HANTHE = "Không có hạn";
                if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                {
                    SOTHE =objLuotkham.MatheBhyt ;// Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.MatheBhyt].Value, "Không có thẻ");
                    HANTHE = objLuotkham.NgayketthucBhyt.Value.ToString("dd/MM/yyyy");// Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.NgayketthucBhyt].Value);
                }
                Utility.SetParameterValue(crpt, "SOTHE", SOTHE);
                Utility.SetParameterValue(crpt, "HANTHE", HANTHE);
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInPhieuKCB, PropertyLib._MayInProperties.PreviewPhieuKCB))
                    objPrint.ShowDialog();
                else
                {
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInPhieuKCB;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Có lỗi trong quá trình in phiếu khám-->\n" + ex.Message);
            }
            finally
            {
                Utility.FreeMemory(crpt);
            }
        }

        private void InphieuKham()
        {
            int IdKham = GetrealRegID();
            KcbDangkyKcb objRegExam = KcbDangkyKcb.FetchByID(IdKham);
            if (objRegExam != null)
            {
                new Update(KcbDangkyKcb.Schema)
                    .Set(KcbDangkyKcb.Columns.TrangthaiIn).EqualTo(1)
                    .Set(KcbDangkyKcb.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                    .Set(KcbDangkyKcb.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                    .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objRegExam.IdKham).Execute();
                IEnumerable<GridEXRow> query = from kham in grdCongkham.GetDataRows()
                                               where
                                                   kham.RowType == RowType.Record &&
                                                   Utility.Int32Dbnull(kham.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1) ==
                                                   Utility.Int32Dbnull(objRegExam.IdKham)
                                               select kham;
                if (query.Count() > 0)
                {
                    GridEXRow gridExRow = query.FirstOrDefault();
                    gridExRow.BeginEdit();
                    gridExRow.Cells[KcbDangkyKcb.Columns.TrangthaiIn].Value = 1;
                    gridExRow.EndEdit();
                    grdCongkham.UpdateData();
                }
                DataTable v_dtData = _KCB_DANGKY.LayThongtinInphieuKCB(IdKham);
                THU_VIEN_CHUNG.CreateXML(v_dtData, Application.StartupPath + @"\Xml4Reports\PhieuKCB.XML");
                Utility.CreateBarcodeData(ref v_dtData, Utility.sDbnull(grdCongkham.GetValue(KcbDangkyKcb.Columns.MaLuotkham)));
                if (v_dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy bản ghi nào", "Thông báo");
                    return;
                }
                KcbLuotkham objLuotkham = CreatePatientExam();
                if (objLuotkham != null)
                    KcbInphieu.INPHIEU_KHAM(Utility.sDbnull(objLuotkham.MaDoituongKcb), v_dtData,
                                                  "PHIẾU KHÁM BỆNH", PropertyLib._MayInProperties.CoGiayInPhieuKCB == Papersize.A5 ? "A5" : "A4");
            }
        }
        int GetrealRegID()
        {
            int IdKham = Utility.Int32Dbnull(grdCongkham.CurrentRow.Cells[KcbDangkyKcb.IdKhamColumn.ColumnName].Value, -1);
            int idphongchidinh = Utility.Int32Dbnull(grdCongkham.CurrentRow.Cells[KcbDangkyKcb.IdChaColumn.ColumnName].Value, -1);
            int LaphiDVkemtheo = Utility.Int32Dbnull(grdCongkham.CurrentRow.Cells[KcbDangkyKcb.LaPhidichvukemtheoColumn.ColumnName].Value, -1);
            if (LaphiDVkemtheo == 1)
            {
                foreach (GridEXRow _row in grdCongkham.GetDataRows())
                {
                    if (Utility.Int32Dbnull(_row.Cells[KcbDangkyKcb.IdKhamColumn.ColumnName].Value, -1) == idphongchidinh)
                        return Utility.Int32Dbnull(_row.Cells[KcbDangkyKcb.IdKhamColumn.ColumnName].Value, -1);
                }
            }
            else
                return IdKham;
            return IdKham;
        }
        #endregion
      
        void cmdXoaCongkham_Click(object sender, EventArgs e)
        {

            if (!Utility.isValidGrid(grdCongkham))
            {
                Utility.ShowMsg("Bạn phải chọn ít nhất 1 bệnh nhân để xóa khám");
                return;
            }
            if (!IsValidDataCongkham()) return;
            if (Utility.AcceptQuestion(string.Format("Bạn có muốn thực hiện việc xóa thông tin công khám chuyên khoa: {0} không ?", grdCongkham.GetValue("ten_dichvukcb")), "Thông báo", true))
            {
                HuyThamKham();
            }
        }
        private bool IsValidDataCongkham()
        {
            if (!Utility.isValidGrid(grdCongkham)) return false;
            if (grdCongkham.CurrentRow == null) return false;
            if (!globalVariables.IsAdmin)
            
            {
                if (!Utility.Coquyen("KCB_XOACONGKHAM") || (globalVariables.UserName != grdCongkham.GetValue("nguoi_tao")))
                {
                    Utility.ShowMsg("Bạn không được cấp quyền xóa công khám. Liên hệ quản trị hệ thống để được cấp thêm quyền", "Thông báo");
                    return false;
                }
            }

            int v_RegId = Utility.Int32Dbnull(grdCongkham.CurrentRow.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1);
            KcbDangkyKcb objRegExam = KcbDangkyKcb.FetchByID(v_RegId);
            if (objRegExam != null)
            {
                SqlQuery sqlQuery = new Select().From(KcbDangkyKcb.Schema)
                    .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objRegExam.IdKham)
                    .And(KcbDangkyKcb.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Đăng ký khám đang chọn đã thanh toán, Bạn không thể xóa", "Thông báo");
                    grdCongkham.Focus();
                    return false;
                }
                if (objRegExam.IdKham <= 0) return true;

                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEPDON_XOATHONGTINLANKHAM_FULL", "0", false) == "1")
                {
                    SqlQuery q =
                        new Select().From(KcbChidinhclsChitiet.Schema).Where(KcbChidinhclsChitiet.Columns.IdKham).IsEqualTo(
                            objRegExam.IdKham).And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                    if (q.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Đăng ký khám đang chọn đã được bác sĩ chỉ định CLS và đã được thanh toán. Yêu cầu Hủy thanh toán các chỉ định CLS trước khi hủy phòng khám", "Thông báo");
                        grdCongkham.Focus();
                        return false;
                    }
                    SqlQuery qPres =
                        new Select().From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.Columns.IdKham).IsEqualTo(
                            objRegExam.IdKham).And(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                    if (qPres.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Đăng ký khám đang chọn đã được bác sĩ kê đơn thuốc và đã được thanh toán. Yêu cầu hủy thanh toán đơn thuốc trước khi hủy phòng khám", "Thông báo");
                        grdCongkham.Focus();
                        return false;
                    }
                }
                else//Nếu có chỉ định CLS hoặc thuốc thì không cho phép xóa
                {
                    SqlQuery q =
                        new Select().From(KcbChidinhcl.Schema).Where(KcbChidinhcl.Columns.IdKham).IsEqualTo(
                            objRegExam.IdKham);
                    if (q.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Đăng ký khám đang chọn đã được bác sĩ chỉ định CLS. Yêu cầu xóa chỉ định CLS trước khi hủy phòng khám", "Thông báo");
                        grdCongkham.Focus();
                        return false;
                    }
                    SqlQuery qPres =
                        new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.Columns.IdKham).IsEqualTo(objRegExam.IdKham);
                    if (qPres.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Đăng ký khám đang chọn đã được bác sĩ kê đơn thuốc. Yêu cầu xóa đơn thuốc trước khi hủy phòng khám", "Thông báo");
                        grdCongkham.Focus();
                        return false;
                    }
                }

            }
            return true;
        }
        private void HuyThamKham()
        {

            if (grdCongkham.CurrentRow != null)
            {

                int v_RegId = Utility.Int32Dbnull(grdCongkham.CurrentRow.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1);

                //if (Utility.AcceptQuestion("Bạn muốn hủy dịch vụ khám đang chọn ", "Thông báo", true))
                //{

                    ActionResult actionResult = _KCB_DANGKY.PerformActionDeleteRegExam(v_RegId);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.ShowMsg("Hủy công khám thành công. Nhấn OK để kết thúc");
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy phòng khám của bệnh nhân ID={0}, PID={1}, Tên={2}, dịch vụ khám={3} ", Utility.getValueOfGridCell(grdList, "id_benhnhan"), Utility.getValueOfGridCell(grdList, "ma_luotkham"), Utility.getValueOfGridCell(grdList, "ten_benhnhan"), Utility.getValueOfGridCell(grdCongkham, "ten_dichvukcb")), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                            DataRow[] arrDr = m_dtDangkyPhongkham.Select(KcbDangkyKcb.Columns.IdKham + "=" + v_RegId + " OR  " + KcbDangkyKcb.IdChaColumn.ColumnName + "=" + v_RegId);
                            if (arrDr.GetLength(0) > 0)
                            {
                                int _count = arrDr.Length;
                                List<string> lstregid = (from p in arrDr.AsEnumerable()
                                                         select p.Field<long>(KcbDangkyKcb.IdKhamColumn.ColumnName).ToString()
                                                      ).ToList<string>();
                                for (int i = 1; i <= _count; i++)
                                {
                                    DataRow[] tempt = m_dtDangkyPhongkham.Select(KcbDangkyKcb.Columns.IdKham + "=" + lstregid[i - 1]);
                                    if (tempt.Length > 0)
                                        tempt[0].Delete();
                                    m_dtDangkyPhongkham.AcceptChanges();
                                }
                            }
                            m_dtDangkyPhongkham.AcceptChanges();
                            Loaddanhsachcongkhamdadangki();
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Bạn thực hiện xóa dịch vụ khám không thành công. Liên hệ đơn vị cung cấp phần mềm để được trợ giúp", "Thông báo");
                            break;
                    }
                //}
            }

        }

        void txtExamtypeCode__OnEnterMe()
        {
            cboKieuKham.Text = txtMyNameEdit.Text;
            //cboKieuKham.Value = txtExamtypeCode.MyID;
            txtKieuKham._Text = cboKieuKham.Text;
            txtIDKieuKham.Text = Utility.sDbnull(txtExamtypeCode.MyID);
        }

        void txtExamtypeCode__OnSelectionChanged()
        {
            cboKieuKham.Text = txtMyNameEdit.Text;
            //cboKieuKham.Value = txtExamtypeCode.MyID;
            txtKieuKham._Text = cboKieuKham.Text;
            txtIDKieuKham.Text = Utility.sDbnull(txtExamtypeCode.MyID);
        }

        void grdChandoan_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "cmdXoa")
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa chẩn đoán đang chọn?", "Xác nhận xóa", true))
                {
                    if (grdChandoan.GetCheckedRows().Length <= 0)
                    {
                        grdChandoan.CurrentRow.IsChecked = true;
                    }
                    XoaChandoan();
                    ModifyCommmands();
                }
            }
        }

        void txtMaBenhphu__OnEnterMe()
        {
            if (txtMaBenhphu.MyCode != "-1")
            {
                EnumerableRowCollection<DataRow> query = from benh in dt_ICD_PHU.AsEnumerable()
                                                         where Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == txtMaBenhphu.MyCode
                                                         select benh;


                if (!query.Any())
                    AddMaBenh(txtMaBenhphu.MyCode, txtMaBenhphu.Text);
            }

            txtMaBenhphu.SetCode("-1");
            txtMaBenhphu.Focus();
            txtMaBenhphu.SelectAll();
        }
        void Modify_Thuoctralai()
        {
            try
            {
                bool _dacapphat = false;
                if (grdPresDetail.GetDataRows().Length > 0)
                {
                    _dacapphat = m_dtDonthuoc.Select("trangthai_tonghop=1").Length > 0;
                    //(from p in grdPresDetail.GetDataRows().AsEnumerable()
                    //            where Utility.ByteDbnull( p.Cells["trangthai_tonghop"],0)>0
                    //             select p).Any(); // m_dtDonthuoc.Select(TPhieuCapphatChitiet.Columns.IdChitiet + ">0").Length > 0;
                }
                grdPresDetail.RootTable.Columns[TPhieuCapphatChitiet.Columns.ThucLinh].Visible = _dacapphat;//&& grdPresDetail.GetDataRows().Length > 0 && THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_HIENTHI_THUCLINH_PHATTHUOC_BENHNHAN", "0", false) == "1" && THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NOITRU_XACNHANDALINH_KHIXACNHANDONTHUOC", "0", false) == "1";
                grdPresDetail.RootTable.Columns[TPhieuCapphatChitiet.Columns.SoLuongtralai].Visible = _dacapphat;//&& grdPresDetail.GetDataRows().Length > 0 && !grdPresDetail.RootTable.Columns[TPhieuCapphatChitiet.Columns.ThucLinh].Visible   && THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NOITRU_XACNHANDALINH_KHIXACNHANDONTHUOC", "0", false) == "1";
                if (grdVTTH.GetDataRows().Length > 0)
                {
                    _dacapphat = m_dtVTTH.Select("trangthai_tonghop=1").Length > 0;
                    //(from p in grdPresDetail.GetDataRows().AsEnumerable()
                    //            where Utility.ByteDbnull( p.Cells["trangthai_tonghop"],0)>0
                    //             select p).Any(); // m_dtDonthuoc.Select(TPhieuCapphatChitiet.Columns.IdChitiet + ">0").Length > 0;
                }
                grdVTTH.RootTable.Columns[TPhieuCapphatChitiet.Columns.ThucLinh].Visible = _dacapphat;
                grdVTTH.RootTable.Columns[TPhieuCapphatChitiet.Columns.SoLuongtralai].Visible = _dacapphat;

            }
            catch (Exception ex)
            {
            }
        }
        void grdPresDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == "so_luongtralai")
                {
                    if (!Utility.Coquyen("thuoc_phieudieutri_nhaptrathuocthuanoitru"))
                    {
                        Utility.ShowMsg("Bạn không có quyền nhập số lượng trả thuốc thừa (thuoc_phieudieutri_nhaptrathuocthuanoitru). Vui lòng lien hệ quản trị hệ thống để được trợ giúp");
                        e.Value = e.InitialValue;
                        return;
                    }
                    if (!Utility.isValidGrid(grdPresDetail))
                    {
                        e.Value = e.InitialValue;
                        return;
                    }
                    if (isReadOnly)
                    {
                        e.Value = e.InitialValue;
                        return;
                    }
                    int TrangthaiTralai = Utility.Int32Dbnull(Utility.getValueOfGridCell(grdPresDetail, TPhieuCapphatChitiet.Columns.TrangthaiTralai), 0);
                    int IdPhieutralai = Utility.Int32Dbnull(Utility.getValueOfGridCell(grdPresDetail, TPhieuCapphatChitiet.Columns.IdPhieutralai), 0);
                    long IdChitiet = Utility.Int64Dbnull(Utility.getValueOfGridCell(grdPresDetail, TPhieuCapphatChitiet.Columns.IdChitiet), 0);
                    TPhieuCapphatChitiet objcapphatct = TPhieuCapphatChitiet.FetchByID(IdChitiet);
                    if (objcapphatct == null)
                    {
                        Utility.ShowMsg("Chi tiết này chưa được tổng hợp lĩnh thuốc nội trú nên bạn không thể sửa lại số lượng thực lĩnh(hoặc số lượng trả lại). Đề nghị kiểm tra lại");
                        e.Value = e.InitialValue;
                        return;
                    }
                    if (Utility.Int64Dbnull( objcapphatct.IdPhieuxuatthuocBenhnhan,-1)<=0)//Bị hủy cấp phát trong khi vẫn đang mở form-->Chặn không cho sửa 
                    {
                        Utility.ShowMsg("Chi tiết này chưa được tổng hợp lĩnh thuốc nội trú nên bạn không thể sửa lại số lượng thực lĩnh(hoặc số lượng trả lại). Đề nghị kiểm tra lại");
                        e.Value = e.InitialValue;
                        return;
                    }
                    decimal soluong = Utility.DecimaltoDbnull(grdPresDetail.CurrentRow.Cells[TPhieuCapphatChitiet.Columns.SoLuong].Value, -1);
                    decimal soluongtralai = 0;
                    decimal thuclinh = soluong;
                    if (IdChitiet <= 0)
                    {
                        Utility.ShowMsg("Chi tiết này chưa được tổng hợp lĩnh thuốc nội trú nên bạn không thể sửa lại số lượng thực lĩnh(hoặc số lượng trả lại). Đề nghị kiểm tra lại");
                        return;
                    }
                    if (TrangthaiTralai == 1)
                    {
                        Utility.ShowMsg("Chi tiết này đã được tổng hợp thành phiếu trả thuốc thừa nên bạn không thể thay đổi lại thông tin số lượng thực lĩnh(hoặc số lượng trả lại) nữa\nMời bạn kiểm tra lại");
                        e.Value = e.InitialValue;
                        return;
                    }
                    if (TrangthaiTralai == 2)
                    {
                        Utility.ShowMsg("Chi tiết này đã được tổng hợp thành phiếu trả thuốc thừa và đã trả lại kho thuốc nên bạn không thể thay đổi lại thông tin số lượng thực lĩnh(hoặc số lượng trả lại) nữa\nMời bạn kiểm tra lại");
                        e.Value = e.InitialValue;
                        return;
                    }
                    if (IdPhieutralai > 0)
                    {
                        Utility.ShowMsg("Chi tiết này đã được tổng hợp thành phiếu trả thuốc thừa nên bạn không thể thay đổi lại thông tin số lượng thực lĩnh(hoặc số lượng trả lại) nữa\nMời bạn kiểm tra lại");
                        e.Value = e.InitialValue;
                        return;
                    }
                    decimal soluongsua = Utility.DecimaltoDbnull(e.Value, 0);
                    if (soluongsua > soluong)
                    {
                        Utility.ShowMsg(string.Format("Số lượng thực lĩnh (hoặc trả lại) phải nhỏ hơn hoặc bằng số lượng kê {0}", soluong.ToString()));
                        e.Value = e.InitialValue;
                        return;
                    }
                    grdPresDetail.CurrentRow.BeginEdit();
                    if (e.Column.Key == TPhieuCapphatChitiet.Columns.SoLuongtralai)
                    {
                        soluongtralai = soluongsua;
                        thuclinh = soluong - soluongsua;
                        grdPresDetail.CurrentRow.Cells[TPhieuCapphatChitiet.Columns.ThucLinh].Value = soluong - soluongsua;
                    }
                    else
                    {
                        thuclinh = soluongsua;
                        soluongtralai = soluong - soluongsua;
                        grdPresDetail.CurrentRow.Cells[TPhieuCapphatChitiet.Columns.SoLuongtralai].Value = soluong - soluongsua;
                    }
                    grdPresDetail.CurrentRow.EndEdit();
                    grdPresDetail.Refetch();
                    CapphatThuocKhoa.CapnhatThuclinh(
                        IdChitiet
                        , thuclinh
                        , soluongtralai
                        );
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

       

        void txtMaluotkham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtMaluotkham.Text) != "")
            {
                txtPatient_Code.Text = txtMaluotkham.Text;
                txtPatient_Code_KeyDown(txtPatient_Code, e);
            }
        }

        void grdBuongGiuong_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!Utility.isValidGrid(grdBuongGiuong))
            {
                return;
            }
            //if (Utility.Int32Dbnull(Utility.getValueOfGridCell(grdBuongGiuong, NoitruPhanbuonggiuong.Columns.IdKhoanoitru.ToString()), 0) !=idKhoaNoitru)
            //{
            //    Utility.ShowMsg(string.Format( "Khoa lập phiếu điều trị {0} khác so với khoa tìm kiếm {1} nên bạn không thể lập phiếu điều trị. Đề nghị chọn lại",Utility.GetValueFromGridColumn(grdBuongGiuong, "ten_khoanoitru"),tenkhoanoitru));
            //    return;
            //}
            if (Utility.Int32Dbnull(Utility.getValueOfGridCell(grdBuongGiuong, NoitruPhanbuonggiuong.Columns.Id.ToString()), 0) != Utility.Int32Dbnull(objLuotkham.IdRavien, 0))
            {
                string khoabuonggiuong = Utility.GetValueFromGridColumn(grdBuongGiuong, "ten_khoanoitru") + " - " + Utility.GetValueFromGridColumn(grdBuongGiuong, "ten_buong") + " - " + Utility.GetValueFromGridColumn(grdBuongGiuong, "ten_giuong");
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn lập phiếu điều trị cho thời điểm Bệnh nhân nằm tại " + khoabuonggiuong + " đang chọn hay không?", "Cảnh báo", true))
                    return;
            }
            GetNoitruPhanbuonggiuong();
            LaydanhsachPhieudieutri();
            uiTabPhieudieutri.SelectedIndex = 0;
            
        }
        void GetNoitruPhanbuonggiuong()
        {
            objNoitruPhanbuonggiuong = null;
            if (!Utility.isValidGrid(grdBuongGiuong))
            {
                txtKhoanoitru_lapphieu.Clear();
                txtBuong_lapphieu.Clear();
                txtGiuong_lapphieu.Clear();
                return;
            }
            objNoitruPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(Utility.Int32Dbnull(Utility.getValueOfGridCell(grdBuongGiuong, NoitruPhanbuonggiuong.Columns.Id.ToString()), 0));
            txtKhoanoitru_lapphieu.Text = Utility.GetValueFromGridColumn(grdBuongGiuong, "ten_khoanoitru");
            txtBuong_lapphieu.Text = Utility.GetValueFromGridColumn(grdBuongGiuong, "ten_buong");
            txtGiuong_lapphieu.Text = Utility.GetValueFromGridColumn(grdBuongGiuong, "ten_giuong");
        }
        void txtChedodinhduong__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(string.Format("{0}|{1}", txtChedodinhduong.LOAI_DANHMUC,txtHoly.LOAI_DANHMUC));
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtChedodinhduong.myCode;
                txtChedodinhduong.Init();
                txtChedodinhduong.SetCode(oldCode);
                txtChedodinhduong.Focus();
            }
        }
       
        void txtHoly__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtHoly.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtHoly.myCode;
                txtHoly.Init();
                txtHoly.SetCode(oldCode);
                txtHoly.Focus();
            }   
        }

        void dtpNgaylapphieu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LaydanhsachPhieudieutri();
            }
        }

        void chkViewAll_CheckedChanged(object sender, EventArgs e)
        {
            LaydanhsachPhieudieutri();
        }

        void txtSongay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PropertyLib._NoitruProperties.Songayhienthi = Utility.Int32Dbnull(txtSongay.Text, 0);
                PropertyLib.SaveProperty(PropertyLib._NoitruProperties);
                LaydanhsachPhieudieutri();
            }
        }

        void cmdAdd_Click(object sender, EventArgs e)
        {
             if (txtHoly.myCode == "-1")
             {
                 txtHoly.Focus();
                 return;
             }
             if (txtChedodinhduong.myCode == "-1")
             {
                 txtChedodinhduong.Focus();
                 return;
             }
             if (m_dtChedoDinhduong.Select(NoitruPhieudinhduong.Columns.MaDinhduong + "='" + txtChedodinhduong.myCode + "'").Length <= 0)
             {
                 NoitruPhieudinhduong _newItem = new NoitruPhieudinhduong();
                 _newItem.IdPhieudieutri = objPhieudieutri.IdPhieudieutri;
                 _newItem.MaHoly = txtHoly.myCode;
                 _newItem.MaDinhduong = txtChedodinhduong.myCode;
                 _newItem.NgayTao = globalVariables.SysDate;
                 _newItem.NguoiTao = globalVariables.UserName;
                 _newItem.NgayLap = objPhieudieutri.NgayDieutri.Value;
                 _newItem.IsNew = true;
                 _newItem.Save();
                 DataRow newDr = m_dtChedoDinhduong.NewRow();
                 Utility.FromObjectToDatarow(_newItem, ref newDr);
                 newDr["ten_holy"] = txtHoly.Text;
                 newDr["ten_dinhduong"] = txtChedodinhduong.Text;
                 m_dtChedoDinhduong.Rows.Add(newDr);
                 m_dtChedoDinhduong.AcceptChanges();
                 Utility.GotoNewRowJanus(grdChedoDinhduong, NoitruPhieudinhduong.Columns.Id, _newItem.Id.ToString());

                
             }
             ModifyCommmands();
             txtChedodinhduong.SetCode("-1");
             txtChedodinhduong.Focus();
        }

        void grdChedoDinhduong_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "cmdXoa")
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa chế độ dinh dưỡng đang chọn?", "Xác nhận xóa", true))
                {
                    if (grdChedoDinhduong.GetCheckedRows().Length <= 0)
                    {
                        grdChedoDinhduong.CurrentRow.IsChecked = true;
                    }
                    XoaDinhduong(grdChedoDinhduong.CurrentRow);
                    ModifyCommmands();
                }
            }
        }

        void cmdXoaDinhduong_Click(object sender, EventArgs e)
        {
            if (CheckDachuyenkhoa()) return;
            int _checkedCount = grdChedoDinhduong.GetCheckedRows().Length;
            if (!Utility.isValidGrid(grdChedoDinhduong) && _checkedCount <= 0)
            {
                Utility.ShowMsg("Bạn cần chọn ít nhất một dòng chế độ dinh dưỡng cần xóa");
                return;
            }
           
            if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa các chế độ dinh dưỡng đang chọn?", "Xác nhận xóa", true))
            {
                if (grdChedoDinhduong.GetCheckedRows().Length <= 0)
                {
                    grdChedoDinhduong.CurrentRow.IsChecked = true;
                }
                XoaDinhduong();
                ModifyCommmands();
            }
        }

        void lnkSize_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._DynamicInputProperties);
            if (_Properties.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ShowResult();
            }
        }

        void mnuShowResult_Click(object sender, EventArgs e)
        {
            if (RowCLS == null || objLuotkham == null || objPhieudieutri == null) return;
            frm_PdfViewer _PdfViewer = new frm_PdfViewer(1);
            _PdfViewer.ma_luotkham = objLuotkham.MaLuotkham;
            _PdfViewer.ma_chidinh =  Utility.sDbnull(RowCLS.Cells[KcbChidinhcl.Columns.MaChidinh].Value);
            _PdfViewer.ShowDialog();
        }

        void cmdChuyengoi_Click(object sender, EventArgs e)
        {
            frm_chuyenVTTHvaotronggoiDV _chuyenVTTHvaotronggoiDV = new frm_chuyenVTTHvaotronggoiDV();
            _chuyenVTTHvaotronggoiDV.objLuotkham = objLuotkham;
            _chuyenVTTHvaotronggoiDV.ShowDialog();
            if (!_chuyenVTTHvaotronggoiDV.m_blnCancel)
            {
                LaythongtinPhieudieutri();
            }
        }
        void grdChandoan_SelectionChanged(object sender, EventArgs e)
        {
            string NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI", "1", false);
            if (objLuotkham == null || !Utility.isValidGrid(grdChandoan) || (NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI == "0" || (NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI == "1" && objPhieudieutri == null)))
            {
                ClearChandoan();
                EnableChandoan(false);
                ModifyCommmands();
                return;
            }
            else
            {
                txtIdChandoan.Text = Utility.getValueOfGridCell(grdChandoan, KcbChandoanKetluan.Columns.IdChandoan).ToString();
                KcbChandoanKetluan objKcbChandoanKetluan = KcbChandoanKetluan.FetchByID(Utility.Int32Dbnull(txtIdChandoan.Text, -1));
                if (objKcbChandoanKetluan != null)
                {
                    txtMach.Text = objKcbChandoanKetluan.Mach;
                    txtNhietDo.Text = objKcbChandoanKetluan.Nhietdo;
                    txtHa.Text = objKcbChandoanKetluan.Huyetap;
                    txtNhipTho.Text = objKcbChandoanKetluan.Nhiptho;
                    txtNhipTim.Text = objKcbChandoanKetluan.Nhiptim;
                    txtChanDoan._Text = objKcbChandoanKetluan.Chandoan;
                    txtChanDoanKemTheo._Text = objKcbChandoanKetluan.ChandoanKemtheo;
                    dtpNgaychandoan.Value = objKcbChandoanKetluan.NgayChandoan;
                    txtMaBenhChinh.SetCode( objKcbChandoanKetluan.MabenhChinh);
                    dt_ICD_PHU.Clear();
                    string dataString = objKcbChandoanKetluan.MabenhPhu;
                    if (!string.IsNullOrEmpty(dataString))
                    {
                        string[] rows = dataString.Split(',');
                        foreach (string row in rows)
                        {
                            if (!string.IsNullOrEmpty(row))
                            {
                                DataRow newDr = dt_ICD_PHU.NewRow();
                                newDr[DmucBenh.Columns.MaBenh] = row;
                                newDr[DmucBenh.Columns.TenBenh] = GetTenBenh(row);
                                dt_ICD_PHU.Rows.Add(newDr);
                                dt_ICD_PHU.AcceptChanges();
                            }
                        }
                        grd_ICD.DataSource = dt_ICD_PHU;
                    }
                    cmdXoachandoan.Enabled = cmdSuachandoan.Enabled = Utility.Byte2Bool(objKcbChandoanKetluan.Noitru);
                }
                else
                {
                    ClearChandoan();
                    EnableChandoan(false);
                }
            }
        }
        void ClearChandoan()
        {
            txtChanDoan.ResetText();
            txtChanDoanKemTheo.ResetText();
            txtMach.Clear();
            txtNhietDo.Clear();
            txtHa.Clear();
            txtNhipTho.Clear();
            txtNhipTim.Clear();
            dtpNgaychandoan.Value = globalVariables.SysDate;
            txtMaBenhphu.Clear();
            txtMaBenhChinh.Clear();
            if (dt_ICD_PHU != null) dt_ICD_PHU.Clear();
        }
        void EnableChandoan(bool _enable)
        {


            txtMach.Enabled = _enable;
            txtNhietDo.Enabled = _enable;
            txtChanDoan.Enabled = _enable;
            txtChanDoanKemTheo.Enabled = _enable;
            txtHa.Enabled = _enable;
            txtNhipTim.Enabled = _enable;
            txtNhipTho.Enabled = _enable;
            dtpNgaychandoan.Enabled = _enable;
            txtMaBenhphu.Enabled = _enable;
            txtMaBenhChinh.Enabled = _enable;
            
        }
        bool isValidChandoan()
        {
            if (objLuotkham != null &&  Utility.DoTrim(txtMaBenhChinh.MyCode) == "-1")
            {
                Utility.ShowMsg("Bạn cần nhập ít nhất Mã bệnh chính để tạo dữ liệu chẩn đoán");
                txtMaBenhChinh.Focus();
                return false;
            }
            return true;
            
        }
        void cmdGhichandoan_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isValidChandoan()) return;
                KcbChandoanKetluan objKcbChandoanKetluan = new KcbChandoanKetluan();
                if (m_enActChandoan == action.Update)
                {
                    objKcbChandoanKetluan = KcbChandoanKetluan.FetchByID(Utility.Int32Dbnull(txtIdChandoan.Text, -1));
                    objKcbChandoanKetluan.MarkOld();
                    objKcbChandoanKetluan.IsNew = false;
                }
                else
                {
                    objKcbChandoanKetluan = new KcbChandoanKetluan();
                    objKcbChandoanKetluan.IsNew = true;
                }
                objKcbChandoanKetluan.MaLuotkham = objLuotkham.MaLuotkham;
                objKcbChandoanKetluan.IdBenhnhan = objLuotkham.IdBenhnhan;
                objKcbChandoanKetluan.MabenhChinh = Utility.sDbnull(txtMaBenhChinh.MyCode, "");
                objKcbChandoanKetluan.Nhommau = txtNhommau.Text;
                objKcbChandoanKetluan.Nhietdo = Utility.sDbnull(txtNhietDo.Text);
                objKcbChandoanKetluan.Huyetap = txtHa.Text;
                objKcbChandoanKetluan.Mach = txtMach.Text;
                objKcbChandoanKetluan.Nhiptim = Utility.sDbnull(txtNhipTim.Text);
                objKcbChandoanKetluan.Nhiptho = Utility.sDbnull(txtNhipTho.Text);
                objKcbChandoanKetluan.Chieucao = Utility.sDbnull(txtChieucao.Text);
                objKcbChandoanKetluan.Cannang = Utility.sDbnull(txtCannang.Text);
                objKcbChandoanKetluan.HuongDieutri = "";
                objKcbChandoanKetluan.SongayDieutri = 0;

                if (cboBSDieutri.SelectedIndex > 0)
                    objKcbChandoanKetluan.IdBacsikham = Utility.Int16Dbnull(cboBSDieutri.SelectedValue, -1);
                else
                {
                    objKcbChandoanKetluan.IdBacsikham = globalVariables.gv_intIDNhanvien;
                }
                string sMaICDPHU = GetDanhsachBenhphu();
                objKcbChandoanKetluan.MabenhPhu = Utility.sDbnull(sMaICDPHU.ToString(), "");
                objKcbChandoanKetluan.IdKhoanoitru = objLuotkham.IdKhoanoitru;
                objKcbChandoanKetluan.IdBuong = objLuotkham.IdBuong;
                objKcbChandoanKetluan.IdGiuong = objLuotkham.IdGiuong;
                objKcbChandoanKetluan.IdBuonggiuong = objLuotkham.IdRavien;

                objKcbChandoanKetluan.IdKham = objPhieudieutri == null ? -1 : objPhieudieutri.IdPhieudieutri;
                objKcbChandoanKetluan.NgayTao = dtpCreatedDate.Value;
                objKcbChandoanKetluan.NguoiTao = globalVariables.UserName;
                objKcbChandoanKetluan.NgayChandoan = dtpNgaychandoan.Value;
                objKcbChandoanKetluan.Ketluan = "";
                objKcbChandoanKetluan.Chandoan = Utility.ReplaceString(txtChanDoan.Text);
                objKcbChandoanKetluan.ChandoanKemtheo = Utility.sDbnull(txtChanDoanKemTheo.Text);
                objKcbChandoanKetluan.IdPhieudieutri = objPhieudieutri == null ? -1 : objPhieudieutri.IdPhieudieutri;
                objKcbChandoanKetluan.Noitru = 1;
                objKcbChandoanKetluan.Save();
                DataRow[] arrDr = m_dtChandoanKCB.Select(KcbChandoanKetluan.Columns.IdChandoan + "=" + objKcbChandoanKetluan.IdChandoan.ToString());
                if (arrDr.Length > 0)
                {
                    Utility.FromObjectToDatarow(objKcbChandoanKetluan, ref arrDr[0]);
                    arrDr[0]["sNgay_chandoan"] = dtpNgaychandoan.Text;
                    Utility.GotoNewRowJanus(grdChandoan, KcbChandoanKetluan.Columns.IdChandoan, objKcbChandoanKetluan.IdChandoan.ToString());
                    m_dtChandoanKCB.AcceptChanges();
                }
                else
                {
                    DataRow newDr = m_dtChandoanKCB.NewRow();
                    Utility.FromObjectToDatarow(objKcbChandoanKetluan, ref newDr);
                    newDr["sNgay_chandoan"] = dtpNgaychandoan.Text;
                    m_dtChandoanKCB.Rows.Add(newDr);
                    m_dtChandoanKCB.AcceptChanges();
                    Utility.GotoNewRowJanus(grdChandoan, KcbChandoanKetluan.Columns.IdChandoan, objKcbChandoanKetluan.IdChandoan.ToString());
                }
                EnableChandoan(false);
                cmdGhichandoan.Enabled = cmdHuychandoan.Enabled = false;
                ModifyCommmands();
                grdChandoan_SelectionChanged(grdChandoan, e);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
           
        }
       
        void cmdHuychandoan_Click(object sender, EventArgs e)
        {
            m_enActChandoan = action.FirstOrFinished;
            EnableChandoan(false);
            ModifyCommmands();
            grdChandoan_SelectionChanged(grdChandoan, e);
            cmdGhichandoan.Enabled = cmdHuychandoan.Enabled = false;
            cmdHuychandoan.SendToBack(); 
        }

        void cmdXoachandoan_Click(object sender, EventArgs e)
        {
            string NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI", "1", false);
            if(objLuotkham!=null && !Utility.isValidGrid(grdChandoan) && (NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI == "0" || (NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI == "1" && objPhieudieutri != null)))
            {
                Utility.ShowMsg("Bạn phải chọn chẩn đoán trên lưới trước khi thực hiện xóa.");
                return;
            }
            if (grdChandoan.GetCheckedRows().Length <= 0)
            {
                grdChandoan.CurrentRow.IsChecked = true;
            }
            XoaChandoan();
            ModifyCommmands(); 
        }

        void cmdSuachandoan_Click(object sender, EventArgs e)
        {
          cmdThemchandoan.Enabled=  cmdSuachandoan.Enabled = cmdXoachandoan.Enabled = false;
          cmdGhichandoan.Enabled = cmdHuychandoan.Enabled = true;
          cmdHuychandoan.BringToFront();
          EnableChandoan(true);
          m_enActChandoan = action.Update;
          txtMach.Focus();
        }

        void cmdThemchandoan_Click(object sender, EventArgs e)
        {
            cmdThemchandoan.Enabled = cmdSuachandoan.Enabled = cmdXoachandoan.Enabled = false;
            cmdGhichandoan.Enabled = cmdHuychandoan.Enabled = true;
            cmdHuychandoan.BringToFront();
            ClearChandoan();
            EnableChandoan(true);
            m_enActChandoan = action.Insert;
            dtpNgaychandoan.Value = globalVariables.SysDate;
            txtMach.Focus();
        }

        void grdGoidichvu_DoubleClick(object sender, EventArgs e)
        {
            grdGoidichvu_SelectionChanged(grdGoidichvu,e);
        }

        void cmdInphieuVT_tronggoi_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdVTTH_tronggoi)) return;
            int Pres_ID = Utility.Int32Dbnull(grdVTTH_tronggoi.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            PrintPres(objLuotkham.IdBenhnhan,objLuotkham.MaLuotkham,1,objPhieudieutri.NgayDieutri.Value, Pres_ID,"PHIẾU VẬT TƯ TRONG GÓI",false);
        }

        void cmdXoaphieuVT_tronggoi_Click(object sender, EventArgs e)
        {
            if (CheckDachuyenkhoa()) return;
            if (!CheckPatientSelected()) return;
            if (!IsValidVTTH_delete_trongoi()) return;
            PerformActionDeleteVTTH_tronggoi();
            ModifyCommmands(); 
        }

        void cmdSuaphieuVT_tronggoi_Click(object sender, EventArgs e)
        {
            if (!CheckPatientSelected()) return;
            if (!cmdSuaphieuVT_tronggoi.Enabled) return;
            SuaphieuVattu_tronggoi(); 
        }

        void cmdThemphieuVT_tronggoi_Click(object sender, EventArgs e)
        {
            if (!CheckPatientSelected()) return;
            if (!cmdThemphieuVT_tronggoi.Enabled) return;
            //if (grdVTTH_tronggoi.GetDataRows().Length <= 0 || !CanUpdateVTTH())
            //{
                ThemphieuVattu_tronggoi();
            //}
            //else
            //{
            //    SuaphieuVattu_tronggoi();
            //}
        }
        /// <summary>
        /// Kiểm tra xem còn phiếu VTTH nào chưa cấp phát
        /// </summary>
        /// <returns></returns>
        bool CanUpdateVTTH()
        {
            //Lấy về phiếu VTTH trong gói chưa được cấp phát vật tư
            var q = from p in m_dtVTTH_tronggoi.AsEnumerable()
                    where Utility.Int32Dbnull(p[KcbDonthuocChitiet.Columns.TrangThai], 0) == 0
                    select p;
            return q.Count() > 0;
        }
        void grdGoidichvu_SelectionChanged(object sender, EventArgs e)
        {
            LayVTTHtronggoi();
            ModifyCommmands();
        }
        void LayVTTHtronggoi()
        {
            try
            {
                if (Utility.isValidGrid(grdGoidichvu))
                {
                    m_dtVTTH_tronggoi =
                     _KCB_THAMKHAM.NoitruLaythongtinVTTHTrongoi(Utility.Int32Dbnull(txtPatient_ID.Text, -1),
                                                              Utility.sDbnull(malankham, ""),
                                                              Utility.Int32Dbnull(txtIdPhieudieutri.Text), Utility.Int32Dbnull(Utility.getValueOfGridCell(grdGoidichvu, KcbChidinhclsChitiet.Columns.IdChitietchidinh))).Tables[0];

                    Utility.SetDataSourceForDataGridEx(grdVTTH_tronggoi, m_dtVTTH_tronggoi, false, true, "", KcbDonthuocChitiet.Columns.SttIn);
                }
                else
                {
                    if (m_dtVTTH_tronggoi != null) m_dtVTTH_tronggoi.Clear();
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void cmdNoitruConfig_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._NoitruProperties);
            frm.ShowDialog();
            
        }

        void grdPresDetail_SelectionChanged(object sender, EventArgs e)
        {
            RowThuoc = Utility.findthelastChild(grdPresDetail.CurrentRow);
            ModifyCommmands();
        }

        void cmdXoaphieuVT_Click(object sender, EventArgs e)
        {
            if (CheckDachuyenkhoa()) return;
            if (!CheckPatientSelected()) return;
            if (!IsValidVTTH_delete()) return;
            PerformActionDeleteVTTH();
            ModifyCommmands(); 
        }

        void cmdInphieuVT_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdVTTH)) return;
            int Pres_ID = Utility.Int32Dbnull(grdVTTH.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            PrintPres(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, 1, objPhieudieutri.NgayDieutri.Value, Pres_ID, "PHIẾU VẬT TƯ NGOÀI GÓI",false);
        }

        void cmdSuaphieuVT_Click(object sender, EventArgs e)
        {
            if (CheckDachuyenkhoa()) return;
            if (!CheckPatientSelected()) return;
            if (!cmdSuaphieuVT.Enabled) return;
            SuaphieuVattu(); 
        }

        void cmdThemphieuVT_Click(object sender, EventArgs e)
        {
            if (CheckDachuyenkhoa()) return;
            if (!CheckPatientSelected()) return;
            if (!cmdThemphieuVT.Enabled) return;
            //if (!ExistsDonThuoc())
            //{
                ThemphieuVattu();
            //}
            //else
            //{
               // SuaphieuVattu();
            //}
        }
       
        void cmdIngoiDV_Click(object sender, EventArgs e)
        {
            try
            {
                string mayin = "";
                int v_AssignId = Utility.Int32Dbnull(grdGoidichvu.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                string v_AssignCode = Utility.sDbnull(grdGoidichvu.GetValue(KcbChidinhcl.Columns.MaChidinh), -1);
                string nhomincls = "ALL";
                
                //if (cboServicePrint.SelectedIndex > 0)
                //{
                //    nhomincls = Utility.sDbnull(cboServicePrint.SelectedValue, "ALL");

                //}

                KcbInphieu.InphieuChidinhCls((int)objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, v_AssignId, v_AssignCode, nhomincls, 0, chkintachgoidichvu.Checked, ref mayin);
                if (mayin != "") cboLaserPrinters.Text = mayin;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void cmdXoagoiDV_Click(object sender, EventArgs e)
        {
            if (CheckDachuyenkhoa()) return;
            if (!IsValidGoidichvu()) return;
            XoaGoidichvu();
            ModifyCommmands();
        }

        void cmdSuagoiDV_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckDachuyenkhoa()) return;
                if (!CheckPatientSelected()) return;
                if (!InValiUpdateChiDinh()) return;
                frm_KCB_CHIDINH_CLS frm = new frm_KCB_CHIDINH_CLS("GOI", 1);
                frm.noitru = 1;
                frm.objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtIdPhieudieutri.Text));
                frm.Exam_ID = -1;
                frm.objLuotkham = objLuotkham;
                frm.objBenhnhan = objBenhnhan;
                frm.m_eAction = action.Update;
                frm.txtAssign_ID.Text = Utility.sDbnull(grdGoidichvu.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), "-1");
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    LaythongtinPhieudieutri();
                    TinhtoanTongchiphi();
                    ModifyCommmands();
                }
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg("Lỗi trong quá trình sửa phiếu :" + e);
                }
                //throw;
            } 
        }

        void cmdThemgoiDV_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckDachuyenkhoa()) return;
                if (!CheckPatientSelected()) return;
                if (!cmdInsertAssign.Enabled) return;
                frm_KCB_CHIDINH_CLS frm = new frm_KCB_CHIDINH_CLS("GOI", 1);
                frm.txtAssign_ID.Text = "-100";
                frm.Exam_ID = -1;
                frm.objLuotkham = objLuotkham;
                frm.objBenhnhan = objBenhnhan;
                frm.objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtIdPhieudieutri.Text));
                frm.m_eAction = action.Insert;
                frm.txtAssign_ID.Text = "-1";
                frm.noitru = 1;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {

                    LaythongtinPhieudieutri();
                    TinhtoanTongchiphi();
                    if (PropertyLib._ThamKhamProperties.TudongthugonCLS)
                        grdGoidichvu.GroupMode = GroupMode.Collapsed;
                    Utility.GotoNewRowJanus(grdGoidichvu, KcbChidinhclsChitiet.Columns.IdChidinh, frm.txtAssign_ID.Text);
                    ModifyCommmands();
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                txtPatient_Code.Focus();
                txtPatient_Code.SelectAll();
            }
        }

        void cmdCauHinh_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._NoitruProperties);
            _Properties.ShowDialog();
            Cauhinh();
        }
        void Cauhinh()
        {
            txtSongay.Text = PropertyLib._NoitruProperties.Songayhienthi.ToString();
            string _val=THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_HIENTHIKE_VTTH_THEOGOI","0",false);
            pnlVTTHTronggoi.Height = _val == "1" ? 50 : 0;
            pnlVTTHTronggoi.Visible = _val == "1";
            pnlKieukham.Visible = !PropertyLib._KCBProperties.GoMaDvu;
            pnlChonCongkham.Visible = PropertyLib._KCBProperties.GoMaDvu;
        }
        void cmdSaochep_Click(object sender, EventArgs e)
        {
            if (CheckDachuyenkhoa()) return;
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn phải chọn bệnh nhân trước khi thực hiện sao chép phiếu điều trị");
                return;
            }
            //if (Utility.Int32Dbnull(objLuotkham.IdKhoanoitru, 0) != idKhoaNoitru)
            //{
            //    Utility.ShowMsg(string.Format("Khoa nội trú hiện tại của Bệnh nhân đang khác khoa tìm kiếm nên bạn không được phép sao chép phiếu điều trị.\nBạn có thể chọn vào thời điểm nằm nội trú tại khoa "+tenkhoanoitru+"Sau đó nhấn đúp chuột để thêm mới(hoặc sửa) phiếu điều trị cho khoảng thời gian đó.", Utility.GetValueFromGridColumn(grdBuongGiuong, "ten_khoanoitru"), tenkhoanoitru));
            //    return;
            //}
            frm_Saochep_phieudieutri _Saochep_phieudieutri = new frm_Saochep_phieudieutri();
            _Saochep_phieudieutri.objLuotkham = objLuotkham;
            _Saochep_phieudieutri._OnCopyComplete += new frm_Saochep_phieudieutri.OnCopyComplete(_Saochep_phieudieutri__OnCopyComplete);
            _Saochep_phieudieutri.ShowDialog();
        }

        void _Saochep_phieudieutri__OnCopyComplete()
        {
            HienthithongtinBN();
        }

        void cmdInphieudieutri_Click(object sender, EventArgs e)
        {
            frm_InPhieudieutri _InPhieudieutri = new frm_InPhieudieutri();
            _InPhieudieutri.objLuotkham = this.objLuotkham;
            _InPhieudieutri.ShowDialog();
        }

        void cmdxoaphieudieutri_Click(object sender, EventArgs e)
        {
            if (CheckDachuyenkhoa()) return;
            XoaPhieudieutri();
        }

        void cmdSuaphieudieutri_Click(object sender, EventArgs e)
        {
            if (CheckDachuyenkhoa()) return;
            SuaPhieudieutri();
        }

        void cmdthemphieudieutri_Click(object sender, EventArgs e)
        {
            //if (CheckDachuyenkhoa()) return;
            Themphieudieutri();
        }
        bool IsValidCommon()
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn cần chọn Bệnh nhân!");
                return false;
            }
            if (Utility.Byte2Bool( objNoitruPhanbuonggiuong.TrangthaiChotkhoa ))
            {
                Utility.ShowMsg("Thời điểm nằm buồng giường bạn đang chọn đã được chốt khoa nên bạn không thể thao tác");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 3)
            {
                Utility.ShowMsg("Bệnh nhân đã được tạo phiếu ra viện nên bạn không thể thao tác");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 4)
            {
                Utility.ShowMsg("Bệnh nhân đã được xác nhận dữ liệu nội trú để chuyển thanh toán(hoặc tài chính duyệt trước khi thanh toán) nên bạn không thể thao tác");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã được duyệt thanh toán nội trú để ra viện nên bạn không thể thao tácn");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã kết thúc điều trị nội trú(Đã thanh toán xong) nên bạn không thể thao tác");
                return false;
            }
            return true;
        }
        void Themphieudieutri()
        {
            try
            {
                //Utility.SetMsg(lblMsg, "", false);
                //Kiểm tra lần nhập viện hoặc chuyển khoa gần nhất phải được phân buồng giường trước khi ra viện
                if (objNoitruPhanbuonggiuong == null)
                {
                    uiTabPhieudieutri.SelectedTab = uiTabPhieudieutri.TabPages["Buonggiuong"];
                    Utility.ShowMsg("Bạn cần chọn Thông tin Khoa-Buồng-Giường(Thời điểm) lập phiếu điều trị cho Bệnh nhân");
                    return;
                }
                //Bỏ để cho phép ko cần phân buồng giường cũng lập được phiếu điều trị
                //bool isValid = Utility.Int16Dbnull(objNoitruPhanbuonggiuong.IdBuong, 0) > 0 && Utility.Int16Dbnull(objNoitruPhanbuonggiuong.IdGiuong, 0) > 0;
                //if (!isValid)
                //{
                //    Utility.ShowMsg(string.Format( "Hệ thống phát hiện Bệnh nhân chưa được phân buồng giường tại {0} nên bạn không thể lập phiếu điều trị được.",objkhoaphonghientai.TenKhoaphong));
                //    return;
                //}
                //if (Utility.Int32Dbnull(Utility.getValueOfGridCell(grdBuongGiuong, NoitruPhanbuonggiuong.Columns.IdKhoanoitru.ToString()), 0) != idKhoaNoitru)
                //{
                //    Utility.ShowMsg(string.Format("Khoa lập phiếu điều trị {0} khác so với khoa tìm kiếm {1} nên bạn không thể lập phiếu điều trị. Đề nghị chọn lại", Utility.GetValueFromGridColumn(grdBuongGiuong, "ten_khoanoitru"), tenkhoanoitru));
                //    return;
                //}

                if (!IsValidCommon()) return;
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_KIEMTRATHANHTOANHET_CHIPHINGOAITRU_TRUOCKHILAPPHIEUDIEUTRI", "0", true) == "1")
                {
                    if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) && !new noitru_TamungHoanung().DathanhtoanhetNgoaitru(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham))
                    {
                        Utility.ShowMsg("Bệnh nhân Dịch vụ chưa thanh toán hết tiền ngoại trú nên không được phép lập phiếu điều trị");
                        return;
                    }
                }
                frm_themphieudieutri frm = new frm_themphieudieutri();
                frm.id_khoasua = this.id_khoasua;
                frm.txtTreat_ID.Text = "-1";
                frm.grdList = grdPhieudieutri;
                frm.em_Action = action.Insert;
                frm.p_TreatMent = m_dtPhieudieutri;
                frm.objBuongGiuong = objNoitruPhanbuonggiuong;
                frm.objLuotkham = objLuotkham;
                frm.objPhieudieutri = new NoitruPhieudieutri();
                frm.ShowDialog();
                if (frm.b_Cancel)
                {
                    Utility.UpdateGroup(grdPhieudieutri, m_dtPhieudieutri, "id_khoanoitru", "Khoa điều trị");
                    if (uiTabPhieudieutri.SelectedTab != tabPagePhieuDieuTri)
                        uiTabPhieudieutri.SelectedTab = tabPagePhieuDieuTri;
                    Utility.GotoNewRowJanus(grdPhieudieutri, NoitruPhieudieutri.Columns.IdPhieudieutri,
                                            Utility.sDbnull(frm.txtTreat_ID.Text, -1));
                    //Thêm các đơn thuốc sao chép
                    //if (frm.lstPresID.Count > 0)
                    //{
                    //    KcbDonthuocCollection lstpres = new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.IdDonthuocColumn).In(frm.lstPresID).ExecuteAsCollection<KcbDonthuocCollection>();
                    //    if (lstpres.Count > 0)
                    //        if (new noitru_phieudieutri().SaochepDonthuoc(_CurIdPhieudieutri, objLuotkham, lstpres, frm.objPhieudieutri.NgayDieutri.Value) == ActionResult.Success)
                    //        {
                    //            grdPhieudieutri_SelectionChanged(grdPhieudieutri, new EventArgs());
                    //        }
                    //        else
                    //        {
                    //            Utility.ShowMsg("Lỗi khi sao chép đơn thuốc. Liên hệ VSS để được trợ giúp!");
                    //        }
                    //}
                }
                ModifyCommandPhieudieutri();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(string.Format("Lỗi trong quá trình :{0}", exception));
                }
            }
            finally
            {
            }
        }
        private void SuaPhieudieutri()
        {
            try
            {
                if (!CheckPatientSelected()) return;
                int ID = Utility.Int32Dbnull(grdPhieudieutri.GetValue(NoitruPhieudieutri.Columns.IdPhieudieutri), -1);
                b_Hasloaded = false;
                frm_themphieudieutri frm = new frm_themphieudieutri();
                frm.id_khoasua = this.id_khoasua;
                frm.objBuongGiuong = null;//Tự động nạp tại form load
                frm.objLuotkham = objLuotkham;
                frm.grdList = grdPhieudieutri;
                frm.em_Action = action.Update;
                frm.objBuongGiuong = objNoitruPhanbuonggiuong;
                frm.p_TreatMent = m_dtPhieudieutri;
                frm.txtTreat_ID.Text = ID.ToString();
                frm.objPhieudieutri = NoitruPhieudieutri.FetchByID(ID);
                frm.ShowDialog();
                if (frm.b_Cancel)
                {
                    Utility.UpdateGroup(grdPhieudieutri, m_dtPhieudieutri, "id_khoanoitru", "Khoa điều trị");
                    //if (frm.lstPresID.Count > 0)
                    //{
                    //    KcbDonthuocCollection lstpres = new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.IdDonthuocColumn).In(frm.lstPresID).ExecuteAsCollection<KcbDonthuocCollection>();
                    //    if (lstpres.Count > 0)
                    //        if (new noitru_phieudieutri().SaochepDonthuoc(_CurIdPhieudieutri, objLuotkham, lstpres, frm.objPhieudieutri.NgayDieutri.Value) == ActionResult.Success)
                    //        {
                    //            grdPhieudieutri_SelectionChanged(grdPhieudieutri, new EventArgs());
                    //        }
                    //        else
                    //        {
                    //            Utility.ShowMsg("Lỗi khi sao chép đơn thuốc. Liên hệ VSS để được trợ giúp!");
                    //        }
                    //}
                }
                ModifyCommandPhieudieutri();
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                b_Hasloaded = true;
            }
        }
        private void XoaPhieudieutri()
        {


            try
            {
                m_blnHasLoaded = false;
                List<int> lstIdPhieudieutri = new List<int>();
                if (grdPhieudieutri.GetCheckedRows().Length < 0)
                {
                    if (Utility.isValidGrid(grdPhieudieutri))
                    {
                        Utility.ShowMsg("Bạn phải thực hiện chọn một phiếu điều trị để xóa");
                        grdPhieudieutri.Focus();
                        return;
                    }
                }
                foreach (GridEXRow gridExRow in grdPhieudieutri.GetCheckedRows())
                {
                    int TreatId = Utility.Int32Dbnull(grdPhieudieutri.GetValue(NoitruPhieudieutri.Columns.IdPhieudieutri), -1);
                    if (!IsValidBeforeDelete(TreatId))
                    {
                        return;
                    }
                }
                lstIdPhieudieutri = (from p in grdPhieudieutri.GetCheckedRows()
                                     select Utility.Int32Dbnull(p.Cells[NoitruPhieudieutri.Columns.IdPhieudieutri].Value, -1)).ToList<int>();
                string _question = grdPhieudieutri.GetCheckedRows().Length > 0 ? "Bạn có chắc chắn muốn xóa các phiếu điều trị đang chọn hay không?"
                    : string.Format("Bạn có chắc chắn muốn xóa phiếu điều trị của ngày {0} hay không?", Utility.sDbnull(grdPhieudieutri.GetValue("sngay_dieutri")));
                if (Utility.AcceptQuestion(_question,
                                           "Thông báo", true))
                {
                    ActionResult actionResult = new noitru_phieudieutri().Xoaphieudieutri(lstIdPhieudieutri);
                    if (grdPhieudieutri.GetCheckedRows().Length > 0)
                    {
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                foreach (GridEXRow gridExRow in grdPhieudieutri.GetCheckedRows())
                                {
                                    gridExRow.Delete();
                                    grdPhieudieutri.UpdateData();
                                    grdPhieudieutri.Refresh();
                                    m_dtPhieudieutri.AcceptChanges();
                                }
                                Utility.UpdateGroup(grdPhieudieutri, m_dtPhieudieutri, "id_khoanoitru", "Khoa điều trị");
                                Utility.ShowMsg("Xóa phiếu điều trị thành công");
                                break;
                            case ActionResult.Error:
                                Utility.ShowMsg("Lỗi trong quá trình xóa thông tin", "Thông báo", MessageBoxIcon.Error);
                                break;
                            case ActionResult.IsNotUserName:
                                Utility.ShowMsg("Phiếu chỉ định này không phải của bạn, Bạn không có quyền xóa",
                                                "Thông báo", MessageBoxIcon.Warning);
                                break;
                        }

                    }
                    else//Xóa dòng hiện tại
                    {
                        XoaPhieuDieuTri(Utility.Int32Dbnull(grdPhieudieutri.GetValue(NoitruPhieudieutri.Columns.IdPhieudieutri), -1));
                    }
                }
            }
            catch (Exception)
            {
                // Utility.ShowMsg("Lỗi trong quá trình xóa thông tin ","Thông báo lỗi",MessageBoxIcon.Error);
            }
            finally
            {
                m_blnHasLoaded = true;
                grdPhieudieutri_SelectionChanged(grdPhieudieutri, new EventArgs());
                ModifyCommandPhieudieutri();
                ModifyCommmands();
            }
        }
        private void XoaPhieuDieuTri(int IdPhieudieutri)
        {
            try
            {
                m_blnHasLoaded = false;
                if (!IsValidBeforeDelete(IdPhieudieutri)) return;

                ActionResult act = new noitru_phieudieutri().Xoaphieudieutri(new List<int>() { IdPhieudieutri });
                switch (act)
                {
                    case ActionResult.Success:
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa phiếu điều trị ID={0} của người bệnh id={1}, mã khám ={2} thành công ", IdPhieudieutri,objLuotkham.IdBenhnhan,objLuotkham.MaLuotkham), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                        grdPhieudieutri.CurrentRow.Delete();
                        grdPhieudieutri.UpdateData();
                        grdPhieudieutri.Refresh();
                        m_dtPhieudieutri.AcceptChanges();
                        Utility.UpdateGroup(grdPhieudieutri, m_dtPhieudieutri, "id_khoanoitru", "Khoa điều trị");
                        
                        Utility.ShowMsg("Xóa phiếu điều trị thành công");
                        if (!Utility.isValidGrid(grdPhieudieutri))
                        {
                            _CurIdPhieudieutri = -1;
                            objPhieudieutri = null;
                        }
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình xóa thông tin", "Thông báo", MessageBoxIcon.Error);
                        break;
                }

            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.CatchException(exception);
                }
            }
            finally
            {
                m_blnHasLoaded = true;
                grdPhieudieutri_SelectionChanged(grdPhieudieutri, new EventArgs());
                ModifyCommandPhieudieutri();
                ModifyCommmands();
            }
        }
        private bool IsValidBeforeDelete(int Treat_ID)
        {
            objLuotkham = Utility.getKcbLuotkham(objLuotkham);
            if (objLuotkham.TrangthaiNoitru >= 3)
            {
                Utility.ShowMsg("Người bệnh đã được cho ra viện nên bạn không thể xóa phiếu điều trị.");
                return false;
            }
            if (!globalVariables.IsAdmin)
            {
                if (globalVariables.UserName != Utility.GetValueFromGridColumn(grdPhieudieutri, NoitruPhieudieutri.Columns.NguoiTao))
                {
                    Utility.ShowMsg("Phiếu điều trị không phải của bạn tạo,Bạn không có quyền xóa thông tin ");
                    return false;
                }


            }
            int reval = -1;
            //Nhập lại kho thanh lý
            StoredProcedure sp = SPs.NoitruKiemtraXoaphieudieutri(Treat_ID, reval);
            sp.Execute();
            reval = Utility.Int32Dbnull(sp.OutputValues[0], -1);
            switch (reval)
            {
                case -1:
                    break;
                case 1:

                    Utility.ShowMsg(string.Format("Đơn thuốc thuộc phiếu điều trị Id={0} đã được tổng hợp(hoặc đã lĩnh thuốc) nên bạn không thể xóa. Đề nghị kiểm tra lại!", Treat_ID.ToString()));
                    return false;
                case 2:
                    Utility.ShowMsg(string.Format("Đơn thuốc thuộc phiếu điều trị Id={0} đã được thanh toán nên bạn không thể xóa. Đề nghị kiểm tra lại!", Treat_ID.ToString()));
                    return false;
                case 3:
                    Utility.ShowMsg(string.Format("Một số chỉ dịnh Cận lâm sàng thuộc phiếu điều trị Id={0} đã được chuyển cận(hoặc có kết quả) nên bạn không thể xóa. Đề nghị kiểm tra lại!", Treat_ID.ToString()));
                    return false;
                case 4:
                    Utility.ShowMsg(string.Format("Một số chỉ dịnh Cận lâm sàng thuộc phiếu điều trị Id={0} đã được thanh toán. Đề nghị kiểm tra lại!", Treat_ID.ToString()));
                    return false;

            }
            return true;

        }
        void ModifyCommandPhieudieutri()
        {
            cmdthemphieudieutri.Enabled = Utility.isValidGrid(grdList) && objLuotkham !=null && objLuotkham.TrangthaiNoitru <= 2;
            cmdSuaphieudieutri.Enabled = Utility.isValidGrid(grdPhieudieutri) && objPhieudieutri != null && objLuotkham != null && objLuotkham.TrangthaiNoitru <= 2;
            cmdxoaphieudieutri.Enabled = Utility.isValidGrid(grdPhieudieutri) && objPhieudieutri != null && objLuotkham != null && objLuotkham.TrangthaiNoitru <= 2;
            cmdInphieudieutri.Enabled = Utility.isValidGrid(grdPhieudieutri) && objPhieudieutri != null && objLuotkham != null;// && objLuotkham.TrangthaiNoitru <= 2;
            cmdSaochep.Enabled = Utility.isValidGrid(grdList) && objLuotkham != null && objLuotkham.TrangthaiNoitru <= 2;
            cmdIntachPhieu.Enabled = cmdInchungPhieu.Enabled = cmdViewPDF.Enabled = true;
        }
        void grdPhieudieutri_SelectionChanged(object sender, EventArgs e)
        {
            if (!m_blnHasLoaded) return;
            if (PropertyLib._NoitruProperties.ViewOnClick && !_buttonClick) 
                Selectionchanged();
            _buttonClick = false;
        }
        /// <summary>
        /// Tất cả các thủ tục tùy biến nút chức năng ko đặt được ở phần Finnally vì sẽ ghi đè lên vùng tùy biến cho các khoa sửa
        /// </summary>
        void Selectionchanged()
        {
            try
            {
                if (Utility.isValidGrid(grdPhieudieutri))
                {
                    

                    _CurIdPhieudieutri = Utility.Int32Dbnull(Utility.sDbnull(grdPhieudieutri.GetValue(NoitruPhieudieutri.Columns.IdPhieudieutri), -1), -1);
                    txtIdPhieudieutri.Text = _CurIdPhieudieutri.ToString();
                    objPhieudieutri = NoitruPhieudieutri.FetchByID(_CurIdPhieudieutri);
                    if (objPhieudieutri != null)
                    {
                       


                        dtpNgaylapphieu.Value = objPhieudieutri.NgayDieutri.Value;
                        cboBSDieutri.SelectedIndex = Utility.GetSelectedIndex(cboBSDieutri, Utility.sDbnull(objPhieudieutri.IdBacsi, "-1"));
                        BuildContextMenu();
                        LaythongtinPhieudieutri();
                        //Duyệt chế độ xem hoặc cho phép thao tác
                         Int16 idkhoahientai = Utility.Int16Dbnull(objNoitruPhanbuonggiuong.IdKhoanoitru);//Khoa người bệnh đang nằm
                            Int16 idkhoalapphieudieutri = Utility.Int16Dbnull(objPhieudieutri.IdKhoanoitru);
                            Int16 idkhoadangchonlamviec = Utility.Int16Dbnull(cboKhoanoitru.SelectedValue);
                        if (id_khoasua > 0)
                        {
                           
                            if (idkhoahientai != idkhoadangchonlamviec)//Nếu khoa bệnh nhân đang nằm khác với khoa đang chọn làm việc thì chỉ hiển thị cho xem
                            {
                                if (idkhoalapphieudieutri != idkhoadangchonlamviec)//Chỉ cho xem
                                {
                                    DisableButton(flowCls, false);
                                    //DisableButton(flowCongkham, false);
                                    DisableButton(flowGoi, false);
                                    DisableButton(FlowGoi1, false);
                                    DisableButton(flowThuoc, false);
                                    DisableButton(flowVTTH, false);
                                    DisableButton(flowPhieudieutri, false);
                                    DisableButton(pnlDinhDuong, false);
                                    cmdthemphieudieutri.Enabled = true;
                                }
                            }
                            else//Nếu khoa bệnh nhân đang nằm chính là khoa đang chọn làm việc thì cho thực hiện thao tác
                            {
                                ModifyCommandPhieudieutri();
                                ModifyCommmands();
                            }
                        }
                        else//Là khoa hiện tại thì kiểm tra ở mức phiếu điều trị
                        {
                            if (idkhoalapphieudieutri != idkhoadangchonlamviec)//Chỉ cho xem
                            {
                                DisableButton(flowCls, false);
                                //DisableButton(flowCongkham, false);
                                DisableButton(flowGoi, false);
                                DisableButton(FlowGoi1, false);
                                DisableButton(flowThuoc, false);
                                DisableButton(flowVTTH, false);
                                DisableButton(pnlDinhDuong, false);
                                DisableButton(flowPhieudieutri, false);
                                cmdthemphieudieutri.Enabled = true;
                            }
                            else//Cho thao tác
                            {
                                DisableButton(flowCls, true);
                                //DisableButton(flowCongkham, true);
                                DisableButton(flowGoi, true);
                                DisableButton(FlowGoi1, true);
                                DisableButton(flowThuoc, true);
                                DisableButton(flowVTTH, true);
                                DisableButton(pnlDinhDuong, true);
                                DisableButton(flowPhieudieutri, true);
                                ModifyCommandPhieudieutri();
                                ModifyCommmands();
                            }
                        }
                    }
                    else
                    {
                        txtIdPhieudieutri.Text = "-1";
                        grdPresDetail.DataSource = null;
                        grdAssignDetail.DataSource = null;
                        grdVTTH.DataSource = null;
                        ModifyCommandPhieudieutri();
                        ModifyCommmands();
                    }
                }
                else
                {
                    txtIdPhieudieutri.Text = "-1";
                    grdPresDetail.DataSource = null;
                    grdAssignDetail.DataSource = null;
                    grdVTTH.DataSource = null;
                    ModifyCommandPhieudieutri();
                    ModifyCommmands();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                ModifyCommandPhieudieutri();
                ModifyCommmands();
                return;
            }
            finally
            {
               
            }
        }
        int _CurIdPhieudieutri = -1;
       
        int date2Int(string date)
        {
            string[] arrdate = date.Split('/');
            return Utility.Int32Dbnull(arrdate[2] + arrdate[1] + arrdate[0], 0);
        }
        void BuildContextMenu()
        {
            ctxPhieudieutri.Items.Clear();


            if (!Utility.isValidGrid(grdPhieudieutri))
            {
                ToolStripMenuItem newItem = new ToolStripMenuItem("Bạn cần chọn phiếu điều trị trước khi thực hiện sao chép");
                ctxPhieudieutri.Items.Add(newItem);
                return;
            }
            _CurIdPhieudieutri = Utility.Int32Dbnull(grdPhieudieutri.CurrentRow.Cells[NoitruPhieudieutri.Columns.IdPhieudieutri].Value.ToString());
            string _date = grdPhieudieutri.CurrentRow.Cells["sngay_dieutri"].Value.ToString();
            int _intDate = date2Int(_date);
            DataRow[] arrDR = m_dtPhieudieutri.Select("1=1", "sngay_dieutri DESC");
            //foreach (GridEXRow row in grdPhieudieutri.GetDataRows())
            int idx = 1;
            foreach (DataRow row in arrDR)
            {
                string _datetempt = row["sngay_dieutri"].ToString();
                int _intDatetempt = date2Int(_datetempt);
                int _IdPhieudieutri = Utility.Int32Dbnull(row[NoitruPhieudieutri.Columns.IdPhieudieutri].ToString(), -1);
                if (_IdPhieudieutri != _CurIdPhieudieutri &&
                    (PropertyLib._NoitruProperties.Songaysaochep == 0 || idx <= PropertyLib._NoitruProperties.Songaysaochep) &&
                    ((!PropertyLib._NoitruProperties.Saochepngaytruocdo)
                    || (PropertyLib._NoitruProperties.Saochepngaytruocdo && _intDatetempt < _intDate)
                    ))
                {
                    idx += 1;
                    string _text = "Sao chép dữ liệu của phiếu điều trị ngày:" + row["sngay_dieutri"].ToString();

                    ToolStripMenuItem mnuNgaydieutri = new ToolStripMenuItem(_text);
                    mnuNgaydieutri.Tag = _IdPhieudieutri + "!" + _datetempt;
                    ctxPhieudieutri.Items.Add(mnuNgaydieutri);
                    mnuNgaydieutri.Click += new EventHandler(newItem_Click);//Tạm khóa
                    ////Thêm các menu sao chép CLS,Thuốc,VTTH,Y lệnh 
                    //ToolStripMenuItem newItemLevel1= new ToolStripMenuItem("Tất cả");
                    //newItemLevel1.Tag = _IdPhieudieutri + "!" + _datetempt + "@100";
                    //mnuNgaydieutri.DropDownItems.Add(newItemLevel1);
                    //newItemLevel1.Click += new EventHandler(newItem_Click);

                    //newItemLevel1 = new ToolStripMenuItem("Chỉ định CLS");
                    //newItemLevel1.Tag = _IdPhieudieutri + "!" + _datetempt + "@1";
                    //mnuNgaydieutri.DropDownItems.Add(newItemLevel1);
                    //newItemLevel1.Click += new EventHandler(newItem_Click);

                    //newItemLevel1 = new ToolStripMenuItem("Đơn thuốc");
                    //newItemLevel1.Tag = _IdPhieudieutri + "!" + _datetempt + "@3";
                    //mnuNgaydieutri.DropDownItems.Add(newItemLevel1);
                    //newItemLevel1.Click += new EventHandler(newItem_Click);

                    //newItemLevel1 = new ToolStripMenuItem("Vật tư tiêu hao");
                    //newItemLevel1.Tag = _IdPhieudieutri + "!" + _datetempt + "@5";
                    //mnuNgaydieutri.DropDownItems.Add(newItemLevel1);
                    //newItemLevel1.Click += new EventHandler(newItem_Click);

                    //newItemLevel1 = new ToolStripMenuItem("Y lệnh");
                    //newItemLevel1.Tag = _IdPhieudieutri + "!" + _datetempt + "@50";
                    //mnuNgaydieutri.DropDownItems.Add(newItemLevel1);
                    //newItemLevel1.Click += new EventHandler(newItem_Click);
                }
            }
        }
        void newItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem selectedItem = sender as ToolStripMenuItem;
            string[] arrVals = selectedItem.Tag.ToString().Split('!');
            long selectedtTreatID = Utility.Int64Dbnull(arrVals[0], -1);
            if (selectedtTreatID != -1)
            {
                KcbDonthuocCollection lstPres = new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.IdPhieudieutriColumn).IsEqualTo(selectedtTreatID).ExecuteAsCollection<KcbDonthuocCollection>();
                if (lstPres.Count <= 0)
                {
                    Utility.ShowMsg("Phiếu điều trị bạn chọn để sao chép không có đơn thuốc. Mời bạn kiểm tra lại");
                    return;
                }
                string[] arrDate = arrVals[1].Split('/');
                DateTime pres_date = new DateTime(Convert.ToInt32(arrDate[2]), Convert.ToInt32(arrDate[1]), Convert.ToInt32(arrDate[0]));
                NoitruPhieudieutri objPhieudieutrigoc = NoitruPhieudieutri.FetchByID(selectedtTreatID);
                if (CheckDachuyenkhoa()) return;
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Bạn phải chọn bệnh nhân trước khi thực hiện sao chép phiếu điều trị");
                    return;
                }
                //if (Utility.Int32Dbnull(objLuotkham.IdKhoanoitru, 0) != idKhoaNoitru)
                //{
                //    Utility.ShowMsg(string.Format("Khoa nội trú hiện tại của Bệnh nhân đang khác khoa tìm kiếm nên bạn không được phép sao chép phiếu điều trị.\nBạn có thể chọn vào thời điểm nằm nội trú tại khoa "+tenkhoanoitru+"Sau đó nhấn đúp chuột để thêm mới(hoặc sửa) phiếu điều trị cho khoảng thời gian đó.", Utility.GetValueFromGridColumn(grdBuongGiuong, "ten_khoanoitru"), tenkhoanoitru));
                //    return;
                //}
                frm_Saochep_phieudieutri_Single _Saochep_phieudieutri = new frm_Saochep_phieudieutri_Single(objPhieudieutrigoc, objPhieudieutri);
                _Saochep_phieudieutri.objLuotkham = objLuotkham;
                _Saochep_phieudieutri._OnCopyComplete +=_Saochep_phieudieutri__OnCopyComplete;
                _Saochep_phieudieutri.ShowDialog();

                //ActionResult actionResult = new noitru_phieudieutri().SaoChepPhieuDieuTrisi(objPhieudieutrigoc, objPhieudieutri, objLuotkham, loai);
                //switch (actionResult)
                //{
                //    case ActionResult.Success:
                //        Utility.ShowMsg("Bạn sao chép thành công. Nhấn OK để kết thúc", "Thông báo");
                //        Selectionchanged();
                //        break;
                //    case ActionResult.Error:
                //        Utility.ShowMsg("Lỗi trong quá trình sao chép", "Thông báo");
                //        break;
                //}


                //if (new noitru_phieudieutri().SaochepDonthuoc(_CurIdPhieudieutri, objLuotkham, lstPres, pres_date) == ActionResult.Success)
                //    Utility.ShowMsg("Đã sao chép đơn thuốc thành công. Nhấn OK để kết thúc");
                //else
                //    Utility.ShowMsg("Lỗi khi sao chép đơn thuốc. Liên hệ VSS để được trợ giúp!");
                
            }
        }
        void grdPhieudieutri_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "colView")
            {
                _buttonClick = true;
                Selectionchanged();
            }
            if (e.Column.Key == "colDelete")
            {

                if (Utility.AcceptQuestion("Bạn có muốn thực hiện việc xóa thông tin phiếu điều trị", "Thông báo", true))
                {
                    if (CheckDachuyenkhoa()) return;
                    XoaPhieuDieuTri(Utility.Int32Dbnull(grdPhieudieutri.GetValue(NoitruPhieudieutri.Columns.IdPhieudieutri), -1));
                }
            }
        }

      
        NoitruPhanbuonggiuong objNoitruPhanbuonggiuong = null;
       
       
        void txtChanDoan__OnSaveAs()
        {
            if (Utility.DoTrim(txtChanDoan.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtChanDoan.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtChanDoan.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtChanDoan.myCode;
                txtChanDoan.Init();
                txtChanDoan.SetCode(oldCode);
                txtChanDoan.Focus();
            }   
        }

       

        void txtNhommau__OnSaveAs()
        {
            if (Utility.DoTrim(txtNhommau.Text)=="") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtNhommau.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtNhommau.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtNhommau.myCode;
                txtNhommau.Init();
                txtNhommau.SetCode(oldCode);
                txtNhommau.Focus();
            }   
        }

        void txtNhommau__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtNhommau.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtNhommau.myCode;
                txtNhommau.Init();
                txtNhommau.SetCode(oldCode);
                txtNhommau.Focus();
            } 
        }

       

        void txtChanDoan__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtChanDoan.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtChanDoan.myCode;
                txtChanDoan.Init();
                txtChanDoan.SetCode(oldCode);
                txtChanDoan.Focus();
            } 
        }

        void cmdUnlock_Click(object sender, EventArgs e)
        {
            Unlock();
        }
      
        void chkHienthichitiet_CheckedChanged(object sender, EventArgs e)
        {
           
            //grdAssignDetail.GroupMode = chkHienthichitiet.Checked ? GroupMode.Expanded : GroupMode.Collapsed;
            //grdAssignDetail.Refresh();
            Application.DoEvents();
        }

        void chkAutocollapse_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._ThamKhamProperties.TudongthugonCLS = chkAutocollapse.Checked;
            PropertyLib.SaveProperty(PropertyLib._ThamKhamProperties);
            //if (PropertyLib._ThamKhamProperties.TudongthugonCLS)
            //    grdAssignDetail.GroupMode = GroupMode.Collapsed;
        }

       
        void cmdThamkhamConfig_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._ThamKhamProperties);
            frm.ShowDialog();
            CauHinhThamKham();
        }
        private void CauHinhThamKham()
        {
            if (PropertyLib._ThamKhamProperties!=null)
            {
                cboA4Donthuoc.Text = PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4 ? "A4" : "A5";
                cboPrintPreviewDonthuoc.SelectedIndex = PropertyLib._MayInProperties.PreviewInDonthuoc ? 0 : 1;

                cboA4Cls.Text = PropertyLib._MayInProperties.CoGiayInCLS == Papersize.A4 ? "A4" : "A5";
                cboPrintPreviewCLS.SelectedIndex = PropertyLib._MayInProperties.PreviewInCLS ? 0 : 1;

                cboA4Tomtatdieutringoaitru.Text = PropertyLib._MayInProperties.CoGiayInTomtatDieutriNgoaitru == Papersize.A4 ? "A4" : "A5";
                cboPrintPreviewTomtatdieutringoaitru.SelectedIndex = PropertyLib._MayInProperties.PreviewInTomtatDieutriNgoaitru ? 0 : 1;


                cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                chkAutocollapse.Checked = PropertyLib._ThamKhamProperties.TudongthugonCLS;
               // cmdIndonthuoc.Visible = PropertyLib._ThamKhamProperties.ChophepIndonthuoc;
            }   
        }
        void grdPatientList_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
                HienthithongtinBN();
        }

        void grdPatientList_MouseClick(object sender, MouseEventArgs e)
        {
            if (PropertyLib._ThamKhamProperties.ViewAfterClick && !_buttonClick )
                HienthithongtinBN();
            _buttonClick = false;
        }

        void cboPrintPreview_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewInDonthuoc = cboPrintPreviewDonthuoc.SelectedIndex == 0;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void cboA4_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.CoGiayInDonthuoc = cboA4Donthuoc.SelectedIndex == 0 ? Papersize.A4 : Papersize.A5;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }
        void cboPrintPreviewCLS_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewInCLS = cboPrintPreviewCLS.SelectedIndex == 0;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void cboA4Cls_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.CoGiayInCLS = cboA4Cls.SelectedIndex == 0 ? Papersize.A4 : Papersize.A5;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }
        void cboPrintPreviewTomtatdieutringoaitru_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewInTomtatDieutriNgoaitru = cboPrintPreviewTomtatdieutringoaitru.SelectedIndex == 0;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void cboA4Tomtatdieutringoaitru_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.CoGiayInTomtatDieutriNgoaitru = cboA4Tomtatdieutringoaitru.SelectedIndex == 0 ? Papersize.A4 : Papersize.A5;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void grdPatientList_DoubleClick(object sender, EventArgs e)
        {
            HienthithongtinBN();
        }
        private int SplitterDistance
        {
            get { return Distance; }
            set { Distance = value; }
        }



        private void frm_QuanlyPhieudieutri_FormClosing(object sender, FormClosingEventArgs e)
        {
            TrytosaveSplitter();
            timer1.Stop();
        }
        void TrytosaveSplitter()
        {
            try
            {
                if (!File.Exists(SplitterPath))
                    File.Create(SplitterPath);
                Utility.SaveValue2Lines(SplitterPath, new List<string>() { splitContainer1.SplitterDistance.ToString(), splitContainer2.SplitterDistance.ToString(), splitContainer3.SplitterDistance.ToString() });
                //File.WriteAllText(SplitterPath, string.Format("{0},{1},{2}", splitContainer1.SplitterDistance.ToString(), splitContainer2.SplitterDistance.ToString(), splitContainer3.SplitterDistance.ToString()));
            }
            catch (Exception ex)
            {
                
            }
        }

       

        private void AddShortCut_KETLUAN()
        {
            try
            {
                if (m_kl == null) return;
                if (!m_kl.Columns.Contains("ShortCut")) m_kl.Columns.Add(new DataColumn("ShortCut", typeof (string)));
                foreach (DataRow dr in m_kl.Rows)
                {
                    string shortcut = "";
                    string realName = dr[DmucChung.Columns.Ten].ToString().Trim() + " " + Utility.Bodau(dr[DmucChung.Columns.Ten].ToString().Trim());
                    shortcut = dr[DmucChung.Columns.Ma].ToString().Trim();
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
        }
        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin của thăm khám
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            //if (!chkByDate.Checked)
            //    if (Utility.AcceptQuestion("Bạn đang tìm kiếm không theo điều kiện thời gian!\nViệc tìm kiếm sẽ mất nhiều thời gian nếu dữ liệu đã có nhiều.\nBạn có muốn dừng việc tìm kiếm để chọn lại khoảng thời gian tìm kiếm hay không?","Xác nhận",true))
            //        return;
            //if (Utility.Int32Dbnull(cboKhoanoitru.SelectedValue, -1) < 0)
            //{
            //    Utility.ShowMsg("Bạn cần chọn khoa nội trú để tìm bệnh nhân cần lập phiếu điều trị");
            //    cboKhoanoitru.Focus();
            //    return;
            //}
            SearchPatient();
        }

        
        private void SearchPatient()
        {
            try
            {
                ClearControl();
                malankham = "";
                objPhieudieutri = null;
                objBenhnhan = null;
                objLuotkham = null;
                if (dt_ICD_PHU != null) dt_ICD_PHU.Clear();
                if (m_dtAssignDetail != null) m_dtAssignDetail.Clear();
                if (m_dtDonthuoc != null) m_dtDonthuoc.Clear();
                DateTime dt_FormDate = dtFromDate.Value;
                DateTime dt_ToDate = dtToDate.Value;
                int status = -1;
                int soKham = -1;
                byte trang_thai =Utility.ByteDbnull( cboTrangthai.SelectedValue,100);
                if (txtMaluotkham.Text.Trim().Length == 8)
                {
                    m_dtPatients =
                        _KCB_THAMKHAM.NoitruTimkiembenhnhan(
                             "01/01/1900" ,
                            "01/01/1900", "",
                            Utility.Int16Dbnull(-1), Utility.DoTrim(txtMaluotkham.Text),
                            Utility.Int32Dbnull(cboKhoanoitru.SelectedValue, -1),
                            -1, chkChuyenkhoa.Checked ? 1 : 0, globalVariables.gv_intIDNhanvien, trang_thai, thamso);
                }
                else
                {
                    m_dtPatients =
                 _KCB_THAMKHAM.NoitruTimkiembenhnhan(
                     !chkByDate.Checked ? "01/01/1900" : dt_FormDate.ToString("dd/MM/yyyy"),
                     !chkByDate.Checked ? "01/01/1900" : dt_ToDate.ToString("dd/MM/yyyy"), "",
                     Utility.Int16Dbnull(-1), Utility.DoTrim(txtMaluotkham.Text),
                     Utility.Int32Dbnull(cboKhoanoitru.SelectedValue, -1),
                     -1, chkChuyenkhoa.Checked ? 1 : 0, globalVariables.gv_intIDNhanvien, trang_thai, thamso);
                }
             

                if (!m_dtPatients.Columns.Contains("MAUSAC"))
                    m_dtPatients.Columns.Add("MAUSAC", typeof(int));

                Utility.SetDataSourceForDataGridEx_Basic(grdList, m_dtPatients, true, true, "", KcbDanhsachBenhnhan.Columns.TenBenhnhan); //"locked=0", "");
                if (grdList.GetDataRows().Length == 1)
                {
                    grdList.MoveFirst();
                }
                if (!dt_ICD_PHU.Columns.Contains(DmucBenh.Columns.MaBenh))
                {
                    dt_ICD_PHU.Columns.Add(DmucBenh.Columns.MaBenh, typeof(string));
                }
                if (!dt_ICD_PHU.Columns.Contains(DmucBenh.Columns.TenBenh))
                {
                    dt_ICD_PHU.Columns.Add(DmucBenh.Columns.TenBenh, typeof(string));
                }
                grd_ICD.DataSource = dt_ICD_PHU;

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommmands();
                ModifyCommandPhieudieutri();
            }
        }
        
   
        private void BindDoctorAssignInfo()
        {
            try
            {
                m_dtDoctorAssign = THU_VIEN_CHUNG.LaydanhsachBacsi(-1,1);
                DataBinding.BindDataCombox(cboBSDieutri, m_dtDoctorAssign, DmucNhanvien.Columns.IdNhanvien,
                                           DmucNhanvien.Columns.TenNhanvien, "---Bác sỹ chỉ định---", true);
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    if (cboBSDieutri.Items.Count > 0)
                        cboBSDieutri.SelectedIndex = 0;
                }
                else
                {
                    if (cboBSDieutri.Items.Count > 0)
                        cboBSDieutri.SelectedIndex = Utility.GetSelectedIndex(cboBSDieutri,
                                                                                 globalVariables.gv_intIDNhanvien.ToString());
                }
            }
            catch (Exception exception)
            {
                // throw;
            }
           
        }


        List<string> lstMaduongdung = new List<string>() { "2.05", "2.14", "2.15" };
        /// <summary>
        /// hàm thực hiện trạng thái của nút
        /// </summary>
        private void ModifyCommmands()
        {
            string noitruHienthiChandoankcbTheophieudieutri = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI","1", false);
            string noitruHienthiGoidichvuTheophieudieutri = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_HIENTHI_GOIDICHVU_THEOPHIEUDIEUTRI","1", false);
            string noitruHienthiPhieuvtthTheophieudieutri = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_HIENTHI_PHIEUVTTH_THEOPHIEUDIEUTRI","1", false);
            try
            {
               
                cmdXoaDinhduong.Enabled = !isReadOnly && grdChedoDinhduong.GetDataRows().Length > 0 && objLuotkham.TrangthaiNoitru < 3;

                cmdIndonthuocravien.Enabled = objLuotkham != null && grdDonthuocravien.RowCount > 0 && objPhieudieutri != null;//&& !isReadOnly &&objLuotkham.TrangthaiNoitru < 3;
                cmdIndonthuoc.Enabled = objLuotkham != null && grdPresDetail.RowCount > 0 && objPhieudieutri != null;//&& !isReadOnly &&objLuotkham.TrangthaiNoitru < 3;
                cmdInphieuVT.Enabled =  objLuotkham != null && grdVTTH.RowCount > 0 && objPhieudieutri != null;//&& !isReadOnly &&objLuotkham.TrangthaiNoitru < 3;
                cmdPrintAssign.Enabled = objLuotkham != null && grdAssignDetail.RowCount > 0 && objPhieudieutri != null;//&& !isReadOnly &&objLuotkham.TrangthaiNoitru < 3;
                cmdIngoiDV.Enabled = objLuotkham != null && grdGoidichvu.RowCount > 0 && objPhieudieutri != null;//&& !isReadOnly &&objLuotkham.TrangthaiNoitru < 3;

                chkintachgoidichvu.Enabled = !isReadOnly && objLuotkham != null && cmdIngoiDV.Enabled && objLuotkham.TrangthaiNoitru < 3;
                chkIntach.Enabled = !isReadOnly && objLuotkham != null && cmdPrintAssign.Enabled && objLuotkham.TrangthaiNoitru < 3;
                cboServicePrint.Enabled = !isReadOnly && objLuotkham != null && cmdPrintAssign.Enabled && objLuotkham.TrangthaiNoitru < 3;
                //tabDiagInfo.Enabled = !isReadOnly && objLuotkham != null && objPhieudieutri != null && objLuotkham.TrangthaiNoitru < 3;
                var hasPTD=from p in m_dtDonthuoc.AsEnumerable()
                            where lstMaduongdung.Contains(Utility.sDbnull( p["ma_duongdung"],""))
                            select p;
                cmdInPhieutruyendich.Visible =objLuotkham!=null && objPhieudieutri!=null && Utility.isValidGrid(grdPresDetail) && hasPTD.Any();
                cmdDeletePres.Enabled = cmdUpdatePres.Enabled = !isReadOnly && objLuotkham != null && grdPresDetail.RowCount > 0 && objPhieudieutri != null;// && IsValidCommon();
                cmdSuadonthuocravien.Enabled = cmdXoadonthuocravien.Enabled = !isReadOnly && objLuotkham != null && grdDonthuocravien.RowCount > 0 && objPhieudieutri != null && RowThuocRavien!=null;// && IsValidCommon();
                cmdConfirm.Enabled = cmdUpdate.Enabled = cmdDelteAssign.Enabled = !isReadOnly && objLuotkham != null && grdAssignDetail.RowCount > 0 && objPhieudieutri != null && objLuotkham.TrangthaiNoitru < 3; // && IsValidCommon();
                cmdViewPDF.Enabled = objLuotkham != null && grdAssignDetail.RowCount > 0 && objPhieudieutri != null;
                cmdSuagoiDV.Enabled = cmdXoagoiDV.Enabled = !isReadOnly && objLuotkham != null && Utility.isValidGrid(grdGoidichvu) && (noitruHienthiGoidichvuTheophieudieutri == "0" || (noitruHienthiGoidichvuTheophieudieutri == "1" && objPhieudieutri != null)) && objLuotkham.TrangthaiNoitru < 3;
                cmdSuaphieuVT_tronggoi.Enabled = cmdXoaphieuVT_tronggoi.Enabled
                    = cmdInphieuVT_tronggoi.Enabled = !isReadOnly && objLuotkham != null && Utility.isValidGrid(grdVTTH_tronggoi) && Utility.isValidGrid(grdGoidichvu) && (noitruHienthiGoidichvuTheophieudieutri == "0" || (noitruHienthiGoidichvuTheophieudieutri == "1" && objPhieudieutri != null)) && objLuotkham.TrangthaiNoitru < 3;

                cmdXoachandoan.Enabled =
                cmdSuachandoan.Enabled = !isReadOnly && objLuotkham != null && Utility.isValidGrid(grdChandoan) && (noitruHienthiChandoankcbTheophieudieutri == "0" || (noitruHienthiChandoankcbTheophieudieutri == "1" && objPhieudieutri != null)) && objLuotkham.TrangthaiNoitru < 3;

                cmdXoaphieuVT.Enabled =
                  cmdSuaphieuVT.Enabled = !isReadOnly && objLuotkham != null && Utility.isValidGrid(grdVTTH) && (noitruHienthiPhieuvtthTheophieudieutri == "0" || (noitruHienthiPhieuvtthTheophieudieutri == "1" && objPhieudieutri != null)) && objLuotkham.TrangthaiNoitru < 3;

                //0=Ngoại trú;1=Nội trú;2=Đã điều trị(Lập phiếu);3=Đã tổng hợp chờ ra viện;4=Ra viện
                if (objLuotkham.TrangthaiNoitru >= 3)
                {
                    cmdThemchandoan.Enabled = cmdInsertAssign.Enabled = cmdConfirm.Enabled = cmdUpdate.Enabled = cmdDelteAssign.Enabled = cmdThemgoiDV.Enabled = cmdSuagoiDV.Enabled = cmdXoagoiDV.Enabled =
                                                                    cmdCreateNewPres.Enabled = cmdUpdatePres.Enabled = cmdDeletePres.Enabled =
                                                                    cmdThemdonthuocravien.Enabled = cmdSuadonthuocravien.Enabled = cmdXoadonthuocravien.Enabled =
                                                                    cmdThemphieuVT.Enabled = cmdSuaphieuVT.Enabled = cmdXoaphieuVT.Enabled = !isReadOnly && false;
                }
                else
                {
                    cmdThemchandoan.Enabled = !isReadOnly && objLuotkham != null && (noitruHienthiChandoankcbTheophieudieutri == "0" || (noitruHienthiChandoankcbTheophieudieutri == "1" && objPhieudieutri != null)) && objLuotkham.TrangthaiNoitru < 3;
                    cmdInsertAssign.Enabled = !isReadOnly && objLuotkham != null && objPhieudieutri != null;
                    cmdThemgoiDV.Enabled = !isReadOnly && objLuotkham != null && (noitruHienthiGoidichvuTheophieudieutri == "0" || (noitruHienthiGoidichvuTheophieudieutri == "1" && objPhieudieutri != null)) && objLuotkham.TrangthaiNoitru < 3;
                    cmdThemphieuVT_tronggoi.Enabled = !isReadOnly && objLuotkham != null && Utility.isValidGrid(grdGoidichvu);
                    cmdThemphieuVT.Enabled = !isReadOnly && objLuotkham != null && (noitruHienthiPhieuvtthTheophieudieutri == "0" || (noitruHienthiPhieuvtthTheophieudieutri == "1" && objPhieudieutri != null)) && objLuotkham.TrangthaiNoitru < 3;
                    cmdCreateNewPres.Enabled = !isReadOnly && objLuotkham != null && objPhieudieutri != null;
                    cmdThemdonthuocravien.Enabled = !isReadOnly && objLuotkham != null && objPhieudieutri != null;
                }
            }
            catch (Exception exception)
            {
            }
            finally
            {
                Modify_Thuoctralai();
            }
        }

        private void LaythongtinPhieudieutri()
        {
            try
            {
                ds = _KCB_THAMKHAM.NoitruLaythongtinclsThuocTheophieudieutri(Utility.Int32Dbnull(txtPatient_ID.Text, -1), Utility.sDbnull(malankham, ""),
                                                                         Utility.Int32Dbnull(txtIdPhieudieutri.Text), 0, objPhieudieutri.IdKhoanoitru.ToString());
                m_dtAssignDetail = ds.Tables[0];
                m_dtDonthuoc_ravien = ds.Tables[1].Clone();
                m_dtDonthuoc = ds.Tables[1].Clone();
                DataRow[] arrThuoc = ds.Tables[1].Select("kieu_donthuoc=3");
                if (arrThuoc.Length > 0) m_dtDonthuoc_ravien = arrThuoc.CopyToDataTable();
                arrThuoc = ds.Tables[1].Select("kieu_donthuoc<>3");
                if (arrThuoc.Length > 0) m_dtDonthuoc = arrThuoc.CopyToDataTable();
                //m_dtDonthuoc = ds.Tables[1];

                m_dtVTTH = ds.Tables[2];
                m_dtGoidichvu = ds.Tables[3];
                m_dtChandoanKCB = ds.Tables[4];
                m_dtChedoDinhduong = ds.Tables[5];
                Utility.SetDataSourceForDataGridEx_Basic(grdChedoDinhduong, m_dtChedoDinhduong, false, true, "", DmucChung.Columns.SttHthi);
                Utility.SetDataSourceForDataGridEx_Basic(grdChandoan, m_dtChandoanKCB, false, true, "", "");
                Utility.SetDataSourceForDataGridEx_Basic(grdAssignDetail, m_dtAssignDetail, false, true, "",
                                                   "stt_hthi_loaidvu,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");
                Utility.SetDataSourceForDataGridEx_Basic(grdGoidichvu, m_dtGoidichvu, false, true, "",
                                                  "stt_hthi_loaidvu,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");

                //m_dtDonthuocChitiet_View = m_dtDonthuoc.Clone();
                //foreach (DataRow dr in m_dtDonthuoc.Rows)
                //{
                //    dr["CHON"] = 0;
                //    DataRow[] drview
                //        = m_dtDonthuocChitiet_View
                //        .Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.IdThuoc], "-1")
                //        + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.DonGia], "-1")
                //        + "AND " + KcbDonthuocChitiet.Columns.BnhanChitra + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.BnhanChitra], "-1")
                //        + "AND " + KcbDonthuocChitiet.Columns.BhytChitra + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.BhytChitra], "-1")
                //        + "AND " + KcbDonthuocChitiet.Columns.PhuThu + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.PhuThu], "-1")
                //        + "AND " + KcbDonthuocChitiet.Columns.TuTuc + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.TuTuc], "-1")
                //        + "AND " + KcbDonthuocChitiet.Columns.IdDonthuoc + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.IdDonthuoc], "-1")
                //        );
                //    if (drview.Length <= 0)
                //    {
                //        m_dtDonthuocChitiet_View.ImportRow(dr);
                //    }
                //    else
                //    {

                //        drview[0][KcbDonthuocChitiet.Columns.SoLuong] = Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong], 0) + Utility.DecimaltoDbnull(dr[KcbDonthuocChitiet.Columns.SoLuong], 0);
                //        drview[0]["TT_KHONG_PHUTHU"] = Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]);
                //        drview[0]["TT"] = Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu]));
                //        drview[0]["TT_BHYT"] = Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                //        drview[0]["TT_BN"] = Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                //        drview[0]["TT_PHUTHU"] = Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                //        drview[0]["TT_BN_KHONG_PHUTHU"] = Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);

                //        drview[0][KcbDonthuocChitiet.Columns.SttIn] = Math.Min(Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SttIn], 0), Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                //        m_dtDonthuocChitiet_View.AcceptChanges();
                //    }
                //}
                if (chkShowGroup.Checked)
                {
                    CreateViewTable();
                }
                else
                {
                    Utility.SetDataSourceForDataGridEx_Basic(grdVTTH, m_dtVTTH, true, true, "1=1", KcbDonthuocChitiet.Columns.SttIn);
                    Utility.SetDataSourceForDataGridEx_Basic(grdPresDetail, m_dtDonthuoc, true, true, "1=1", KcbDonthuocChitiet.Columns.SttIn);
                    Utility.SetDataSourceForDataGridEx_Basic(grdDonthuocravien, m_dtDonthuoc_ravien, true, true, "1=1", KcbDonthuocChitiet.Columns.SttIn);
                }

                grdPresDetail.RootTable.Columns["so_luongtralai"].EditType = chkShowGroup.Checked ? EditType.NoEdit : EditType.TextBox;
                grdVTTH.RootTable.Columns["so_luongtralai"].EditType = chkShowGroup.Checked ? EditType.NoEdit : EditType.TextBox;
                grdDonthuocravien.RootTable.Columns["so_luongtralai"].EditType = chkShowGroup.Checked ? EditType.NoEdit : EditType.TextBox;
                //m_dtVTTHChitiet_View = m_dtVTTH.Clone();
                //foreach (DataRow dr in m_dtVTTH.Rows)
                //{
                //    dr["CHON"] = 0;
                //    DataRow[] drview
                //        = m_dtVTTHChitiet_View
                //        .Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.IdThuoc], "-1")
                //        + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.DonGia], "-1"));
                //    if (drview.Length <= 0)
                //    {
                //        m_dtVTTHChitiet_View.ImportRow(dr);
                //    }
                //    else
                //    {

                //        drview[0][KcbDonthuocChitiet.Columns.SoLuong] = Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong], 0) + Utility.DecimaltoDbnull(dr[KcbDonthuocChitiet.Columns.SoLuong], 0);
                //        drview[0]["TT_KHONG_PHUTHU"] = Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]);
                //        drview[0]["TT"] = Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu]));
                //        drview[0]["TT_BHYT"] = Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                //        drview[0]["TT_BN"] = Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                //        drview[0]["TT_PHUTHU"] = Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                //        drview[0]["TT_BN_KHONG_PHUTHU"] = Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);

                //        drview[0][KcbDonthuocChitiet.Columns.SttIn] = Math.Min(Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SttIn], 0), Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                //        m_dtVTTHChitiet_View.AcceptChanges();
                //    }
                //}
                //Old-->Utility.SetDataSourceForDataGridEx

                HienThiChuyenCan();
                ModifyCommmands();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                grdAssignDetail_SelectionChanged(grdAssignDetail, new EventArgs());
            }
            
        }
        void CreateViewTable()
        {
            try
            {
                #region Gom đơn thuốc-VTTH Tạm rem lại code để mở hết các đơn thuốc 230524
                m_dtDonthuocChitiet_View = m_dtDonthuoc.Clone();
                //Utility.ShowMsg("OK2");
                foreach (DataRow dr in m_dtDonthuoc.Rows)
                {
                    dr["CHON"] = 0;
                    string filter = KcbDonthuocChitiet.Columns.IdThuoc + "=" +
                                    Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.IdThuoc], "-1")
                                    + " AND " + KcbDonthuocChitiet.Columns.DonGia + "=" +
                                    Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.DonGia], "-1")
                                    + " AND " + KcbDonthuocChitiet.Columns.TuTuc + "=" +
                                    Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.TuTuc], "-1");
                    //Utility.ShowMsg(filter);
                    DataRow[] drview
                        = m_dtDonthuocChitiet_View.Select(filter);
                    if (drview.Length <= 0)
                    {
                        m_dtDonthuocChitiet_View.ImportRow(dr);
                    }
                    else
                    {
                        drview[0][KcbDonthuocChitiet.Columns.SoLuong] =
                            Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong], 0) +
                            Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SoLuong], 0);
                        drview[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                                       Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]);
                        drview[0]["TT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                          (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]) +
                                           Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu]));
                        drview[0]["TT_BHYT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                               Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                        drview[0]["TT_BN"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                             (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                                              Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                        drview[0]["TT_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                                 Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                        drview[0]["TT_BN_KHONG_PHUTHU"] =
                            Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                            Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);

                        drview[0][KcbDonthuocChitiet.Columns.SttIn] =
                            Math.Min(Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SttIn], 0),
                                     Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                        m_dtDonthuocChitiet_View.AcceptChanges();
                    }
                }

                Utility.SetDataSourceForDataGridEx(grdPresDetail, m_dtDonthuocChitiet_View, true, true, "",
                                                   KcbDonthuocChitiet.Columns.SttIn);

                m_dtDonthuocRavienChitiet_View = m_dtDonthuoc_ravien.Clone();
                foreach (DataRow dr in m_dtDonthuoc_ravien.Rows)
                {
                    dr["CHON"] = 0;
                    string filter = KcbDonthuocChitiet.Columns.IdThuoc + "=" +
                                    Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.IdThuoc], "-1")
                                    + " AND " + KcbDonthuocChitiet.Columns.DonGia + "=" +
                                    Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.DonGia], "-1")
                                    + " AND " + KcbDonthuocChitiet.Columns.TuTuc + "=" +
                                    Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.TuTuc], "-1");
                    //Utility.ShowMsg(filter);
                    DataRow[] drview
                        = m_dtDonthuocRavienChitiet_View.Select(filter);
                    if (drview.Length <= 0)
                    {
                        m_dtDonthuocRavienChitiet_View.ImportRow(dr);
                    }
                    else
                    {
                        drview[0][KcbDonthuocChitiet.Columns.SoLuong] =
                            Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong], 0) +
                            Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SoLuong], 0);
                        drview[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                                       Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]);
                        drview[0]["TT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                          (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]) +
                                           Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu]));
                        drview[0]["TT_BHYT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                               Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                        drview[0]["TT_BN"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                             (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                                              Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                        drview[0]["TT_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                                 Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                        drview[0]["TT_BN_KHONG_PHUTHU"] =
                            Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                            Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);

                        drview[0][KcbDonthuocChitiet.Columns.SttIn] =
                            Math.Min(Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SttIn], 0),
                                     Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                        m_dtDonthuocRavienChitiet_View.AcceptChanges();
                    }
                }

                Utility.SetDataSourceForDataGridEx(grdDonthuocravien, m_dtDonthuocRavienChitiet_View, true, true, "",
                                                   KcbDonthuocChitiet.Columns.SttIn);

                m_dtVTTHChitiet_View = m_dtVTTH.Clone();
                foreach (DataRow dr in m_dtVTTH.Rows)
                {
                    dr["CHON"] = 0;
                    DataRow[] drview
                        = m_dtVTTHChitiet_View
                            .Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" +
                                    Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.IdThuoc], "-1")
                                    + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" +
                                    Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.DonGia], "-1"));
                    if (drview.Length <= 0)
                    {
                        m_dtVTTHChitiet_View.ImportRow(dr);
                    }
                    else
                    {
                        drview[0][KcbDonthuocChitiet.Columns.SoLuong] =
                            Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong], 0) +
                            Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SoLuong], 0);
                        drview[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                                       Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]);
                        drview[0]["TT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                          (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]) +
                                           Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu]));
                        drview[0]["TT_BHYT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                               Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                        drview[0]["TT_BN"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                             (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                                              Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                        drview[0]["TT_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                                 Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                        drview[0]["TT_BN_KHONG_PHUTHU"] =
                            Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                            Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);

                        drview[0][KcbDonthuocChitiet.Columns.SttIn] =
                            Math.Min(Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SttIn], 0),
                                     Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                        m_dtVTTHChitiet_View.AcceptChanges();
                    }
                }

                Utility.SetDataSourceForDataGridEx(grdVTTH, m_dtVTTHChitiet_View, true, true, "",
                                                   KcbDonthuocChitiet.Columns.SttIn);
                #endregion
            }
            catch (Exception ex)
            {

            }
        }
        private void Get_DanhmucChung()
        {
            txtChanDoan.Init();
            txtNhommau.Init();
            txtChedodinhduong.Init();
            txtHoly.Init();
            txtMaBenhChinh.Init(globalVariables.gv_dtDmucBenh, new List<string>() { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });
            txtMaBenhphu.Init(globalVariables.gv_dtDmucBenh, new List<string>() { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });
        }
        bool AutoSeach = true;
        void NapTrangthaiDieutri()
        {
            DataTable dtTthai = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("TRANGTHAI_DIEUTRI").And(DmucChung.Columns.TrangThai).IsEqualTo(1).OrderAsc(DmucChung.Columns.SttHthi).ExecuteDataSet().Tables[0];
            DataBinding.BindDataCombobox(cboTrangthai, dtTthai, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
            cboTrangthai.SelectedValue = THU_VIEN_CHUNG.Laygiatrimacdinh(dtTthai); //cboTrangthai.Items.Count > 0 ? 0 : -1;
        }
        private void frm_QuanlyPhieudieutri_Load(object sender, EventArgs e)
        {
            try
            {
                dtpPrintDate.Value = dtpNgaychandoan.Value = globalVariables.SysDate;
                AllowTextChanged = false;
                ClearControl();
                lstVisibleColumns = Utility.GetVisibleColumns(grdAssignDetail);
                CKEditorInput = THU_VIEN_CHUNG.Laygiatrithamsohethong("HINHANH_CKEDITOR", "1", true) == "1";
                Get_DanhmucChung();
                NapTrangthaiDieutri();
                
                lblCongkhamMsg.Text = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_CONGKHAMCHUYENKHOA_TINHTIEN", "0", false) == "0" ? "Hệ thống đang được cấu hình KHÔNG TÍNH TIỀN cho các công khám chuyên khoa" : "Hệ thống đang được cấu hình CÓ TÍNH TIỀN cho các công khám chuyên khoa";
                DataTable dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOM_INPHIEU_CLS", true);
                DataBinding.BindDataCombobox(cboServicePrint, dtNhomin, DmucChung.Columns.Ma, DmucChung.Columns.Ten, "Tất cả", true);
                LoadPhongkhamNoitru();
                BindDoctorAssignInfo();
               
                if (cboServicePrint.Items.Count > 0) cboServicePrint.SelectedIndex = 0;
                cboKhoanoitru.SelectedIndex = Utility.GetSelectedIndex(cboKhoanoitru, globalVariablesPrivate.objKhoaphong.IdKhoaphong.ToString());//Tự chọn theo khoa đăng nhập
                //cboKhoanoitru.Enabled = globalVariables.IsAdmin || cboKhoanoitru.Items.Count > 0;// THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_LAPPHIEUDIETRI_CHONKHOANOITRU", "0", false) == "1";
                if (cboKhoanoitru.Items.Count >=1) cboKhoanoitru.SelectedIndex = 0;
                AllowTextChanged = true;
                m_blnHasLoaded = true;
                //Rem thay bằng label cố định 240420
                //ucError1.EllapsedTime = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_CANHBAOTAMUNG_THOIGIAN", "1000", false), 1000);
                //ucError1.NumberofBrlink = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_CANHBAOTAMUNG_SOLAN_NHAPNHAY", "10", false), 10);
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_HIENTHI_GOIDICHVU", "0", false) == "1")
                {
                    tabDiagInfo.TabPages["tabPagegoimo"].TabVisible  = true;
                }
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_PHIEUDIEUTRI_TUDONGTIMKIEMKHIVAOFORM", "0", false) == "1")
                {
                    SearchPatient();
                }
            }
            catch (Exception ex)
            {
                log.Trace("Loi "+ ex.Message);
            }
            finally
            {
                timer1.Enabled = true;
                timer1.Interval = 900;
                if (File.Exists(Application.StartupPath + "\\CAUHINH\\chkintachphieu.txt"))
                {
                    chkIntach.Checked =
                        Convert.ToInt16(File.ReadAllText(Application.StartupPath + "\\CAUHINH\\chkintachphieu.txt")) ==
                        1
                            ? true
                            : false;
                }
                ModifyCommmands();
                m_blnHasLoaded = true;
                txtPatient_Code.Focus();
                txtPatient_Code.Select();
            }
        }
        #region Công khám chuyên khoa
        private DataTable m_dtDanhsachDichvuKCB = new DataTable();
        Int16 _idDoituongKcb = 1;
        private DataTable m_kieuKham = new DataTable();
        private DataTable m_PhongKham = new DataTable();
        DmucDoituongkcb _objDoituongKcb;
        bool AutoLoad = false;
        KCB_DANGKY _KCB_DANGKY = new KCB_DANGKY();
        DataTable m_dtDangkyPhongkham = new DataTable();
        bool TinhchiphiCongkhamCK = true;
        private void cmdThemCongkham_Click(object sender, EventArgs e)
        {
            if (!PropertyLib._KCBProperties.GoMaDvu)
                AutoselectcongkhambyKieukham_Phongkham();
            TinhchiphiCongkhamCK = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_CONGKHAMCHUYENKHOA_TINHTIEN", "0", false) == "1";
            lblCongkhamMsg.Text = !TinhchiphiCongkhamCK ? "Hệ thống đang được cấu hình KHÔNG TÍNH TIỀN cho các công khám chuyên khoa" : "Hệ thống đang được cấu hình CÓ TÍNH TIỀN cho các công khám chuyên khoa";

            if (Utility.Int32Dbnull(cboKieuKham.Value) <= -1)
            {
                Utility.ShowMsg("Bạn phải chọn dịch vụ khám cần thêm cho bệnh nhân", "Thông báo", MessageBoxIcon.Warning);
                return;
            }
            DmucDichvukcb objDichvuKCB =
                   DmucDichvukcb.FetchByID(Utility.Int32Dbnull(cboKieuKham.Value));
            if (objDichvuKCB != null)
            {
                //Utility.SetMsg(lblDonGia, Utility.sDbnull(objDichvuKCB.DonGia), true);
                //Utility.SetMsg(lblPhuThu, Utility.sDbnull(Utility.Int32Dbnull(objLuotkham.DungTuyen.Value, 0) == 1 ? objDichvuKCB.PhuthuDungtuyen : objDichvuKCB.PhuthuTraituyen), true);
                if ((m_dtDangkyPhongkham.Select(KcbDangkyKcb.Columns.IdPhongkham + "=" + objDichvuKCB.IdPhongkham + "").GetLength(0) <= 0))
                {
                    ProcessData();
                }
                else
                {
                    if (Utility.AcceptQuestion("Bệnh nhân đã được đăng ký khám chữa bệnh tại phòng khám này. Bạn có muốn tiếp tục đăng ký dịch vụ KCB mới vừa chọn hay không?", "Thông báo", true))
                    {
                        ProcessData();
                    }
                }
            }
        }
        void ProcessData()
        {
            long v_RegId = -1;
            if (objLuotkham == null) objLuotkham = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).ExecuteSingle<KcbLuotkham>();
            if (objLuotkham != null)
            {
                KcbDangkyKcb objCongkham = CreateNewRegExam();
                objCongkham.TinhChiphi =Utility.Bool2byte( TinhchiphiCongkhamCK);
                if (objCongkham != null)
                {
                    objCongkham.MaLuotkham = objLuotkham.MaLuotkham;
                    objCongkham.IdBenhnhan = objLuotkham.IdBenhnhan;

                    ActionResult actionResult = _KCB_DANGKY.InsertRegExam(objCongkham, objLuotkham, ref v_RegId, Utility.Int32Dbnull(cboKieuKham.Value));
                    if (actionResult == ActionResult.Success)
                    {
                        Utility.ShowMsg(string.Format("Bạn đã đăng ký phòng khám cho người bệnh thành công. Vui lòng hướng dẫn người bệnh sang phòng khám mới đăng kí để tiếp tục kcb"), "Thông báo", MessageBoxIcon.Information);
                        Loaddanhsachcongkhamdadangki();
                        return;
                    }
                    if (actionResult == ActionResult.Error)
                    {
                        Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin ", "Thông báo lỗi", MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                Utility.ShowMsg(string.Format("Không tìm được thông tin người bệnh dựa vào Id người bệnh={0} và Mã lượt khám={1}. Vui lòng liên hệ để được trợ giúp", objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham), "Thông báo lỗi", MessageBoxIcon.Error);
            }
        }
        private KcbDangkyKcb CreateNewRegExam()
        {
            bool b_HasKham = false;
            var query = from phong in m_dtDangkyPhongkham.AsEnumerable().Cast<DataRow>()
                        where
                            Utility.Int32Dbnull(phong[KcbDangkyKcb.Columns.IdDichvuKcb], -100) ==
                            Utility.Int32Dbnull(cboKieuKham.Value, -1)
                        select phong;
            if (query.Count() > 0)
            {
                Utility.ShowMsg("Bệnh nhân đã đăng ký dịch vụ khám này. Đề nghị bạn xem lại");
                b_HasKham = true;
            }
            else
            {
                b_HasKham = false;
            }

            if (!b_HasKham)
            {
                int min = 1;
                int max = 255;
                //List<int> levels = m_dtDangkyPhongkham.AsEnumerable().Select(al => al.Field<int>(KcbDangkyKcb.Columns.SttTt37)).Distinct().ToList();
                // min = levels.Min();
                // max = levels.Max();
                ////Hoặc
                min = Convert.ToInt32(m_dtDangkyPhongkham.AsEnumerable()
                       .Min(row => row[KcbDangkyKcb.Columns.SttTt37]));
                max = Convert.ToInt32(m_dtDangkyPhongkham.AsEnumerable()
                       .Max(row => row[KcbDangkyKcb.Columns.SttTt37]));
                KcbDangkyKcb objCongkham = new KcbDangkyKcb();

                DmucDichvukcb objDichvuKCB = DmucDichvukcb.FetchByID(Utility.Int32Dbnull(cboKieuKham.Value));
                DmucKhoaphong objdepartment = new Select().From(DmucKhoaphong.Schema).Where(DmucKhoaphong.IdKhoaphongColumn).IsEqualTo(objDichvuKCB.IdKhoaphong).ExecuteSingle<DmucKhoaphong>();
                DmucDoituongkcb objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(objLuotkham.MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
                if (objDichvuKCB != null)
                {

                    int dungtuyen = 1;
                    objCongkham.IdDichvuKcb = Utility.Int16Dbnull(objDichvuKCB.IdDichvukcb, -1);
                    objCongkham.IdKieukham = objDichvuKCB.IdKieukham;
                    objCongkham.NhomBaocao = objDichvuKCB.NhomBaocao;
                    objCongkham.LaPhidichvukemtheo = 0;
                    objCongkham.SttKham = -1;
                    objCongkham.IdCha = -1;
                    if (min > 1)
                        objCongkham.SttTt37 = Utility.ByteDbnull(min - 1);
                    else
                        objCongkham.SttTt37 = Utility.ByteDbnull(max + 1);
                    objCongkham.TyleTt = THU_VIEN_CHUNG.Bhyt_Laytyle_tt_congkham(THU_VIEN_CHUNG.IsBaoHiem(objCongkham.IdLoaidoituongkcb), objCongkham.SttTt37);
                    objCongkham.IdKhoakcb = objDichvuKCB.IdKhoaphong;// objdepartment.IdKhoaphong;
                    if (objLuotkham != null)
                    {
                        objCongkham.MadoituongGia = objLuotkham.MadoituongGia;
                    }

                    objCongkham.DonGia = Utility.DecimaltoDbnull(objDichvuKCB.DonGia, 0);
                    objCongkham.NguoiTao = globalVariables.UserName;
                    if (objdepartment != null)
                    {
                        
                        objCongkham.MaPhongStt = objdepartment.MaPhongStt;

                    }
                    if (objDoituongKCB != null)
                    {
                        objCongkham.IdLoaidoituongkcb = objDoituongKCB.IdLoaidoituongKcb;
                        objCongkham.MaDoituongkcb = objDoituongKCB.MaDoituongKcb;
                        objCongkham.IdDoituongkcb = objDoituongKCB.IdDoituongKcb;
                    }
                    if (Utility.Int16Dbnull(objDichvuKCB.IdPhongkham, -1) > -1)
                        objCongkham.IdPhongkham = Utility.Int16Dbnull(objDichvuKCB.IdPhongkham, -1);
                    else
                        objCongkham.IdPhongkham = Utility.Int16Dbnull(txtIDPkham.Text, -1);

                    if (Utility.Int32Dbnull(objDichvuKCB.IdBacsy) > 0)
                        objCongkham.IdBacsikham = Utility.Int16Dbnull(objDichvuKCB.IdBacsy);
                   
                    objCongkham.PhuThu = THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) ? (dungtuyen == 1 ? Utility.DecimaltoDbnull(objDichvuKCB.PhuthuDungtuyen) : Utility.DecimaltoDbnull(objDichvuKCB.PhuthuTraituyen)) : 0m;
                    objCongkham.NgayDangky = globalVariables.SysDate;
                    objCongkham.IdBenhnhan = objLuotkham.IdBenhnhan;
                    objCongkham.TrangthaiThanhtoan = 0;
                    objCongkham.TrangthaiHuy = 0;
                    objCongkham.Noitru = 1;
                    objCongkham.IdPhieudieutri = objPhieudieutri.IdPhieudieutri;
                    objCongkham.IdKhoadieutri = objPhieudieutri.IdKhoanoitru;
                    objCongkham.IdBuongGiuong = objPhieudieutri.IdBuongGiuong;
                    objCongkham.TrangthaiIn = 0;
                    objCongkham.TuTuc = Utility.ByteDbnull(objDichvuKCB.TuTuc, 0);
                    objCongkham.MaKhoaThuchien = objdepartment.MaKhoaphong;
                    objCongkham.TenDichvuKcb = cboKieuKham.Text;
                    objCongkham.NgayTiepdon = globalVariables.SysDate;
                    objCongkham.IpMaytao = globalVariables.gv_strIPAddress;
                    objCongkham.TenMaytao = globalVariables.gv_strComputerName;
                    objCongkham.MaLuotkham = objLuotkham.MaLuotkham;
                    if (THU_VIEN_CHUNG.IsNgoaiGio())
                    {
                        objCongkham.KhamNgoaigio = 1;
                        objCongkham.DonGia = Utility.DecimaltoDbnull(objDichvuKCB.DongiaNgoaigio, 0);
                        objCongkham.PhuThu = Utility.Byte2Bool(objLuotkham.DungTuyen) ? Utility.DecimaltoDbnull(objDichvuKCB.PhuthuNgoaigio, 0) : Utility.DecimaltoDbnull(objDichvuKCB.PhuthuDungtuyen);
                    }
                    else
                    {
                        objCongkham.KhamNgoaigio = 0;
                    }
                }
                else
                {
                    objCongkham = null;
                }
                //if (THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_TINHGIAKHAM_THONGTU37", "0", false) == "1" && objLuotkham.IdLoaidoituongKcb == 0)//0= BHYT;1= DV
                //{
                //    m_dtDangkyPhongkham = _KCB_DANGKY.LayDsachCongkhamDadangki(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, 1);
                //    THU_VIEN_CHUNG.TinhlaiGiaChiphiKcb(m_dtDangkyPhongkham, ref objCongkham);
                //}
                return objCongkham;
            }
            return null;
        }
        private void Loaddanhsachcongkhamdadangki()
        {
             m_dtDangkyPhongkham = _KCB_DANGKY.LayDsachCongkhamDadangki(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan,100);
            Utility.SetDataSourceForDataGridEx(grdCongkham, m_dtDangkyPhongkham, false, true, "", KcbDangkyKcb.Columns.IdKham + " desc");

        }
        private void NapthongtindanhmucCongkham()
        {
            bool oldStatus = AllowTextChanged;
            try
            {
                
                cboKieuKham.DataSource = null;
                //Khởi tạo danh mục Loại khám
                string objecttypeCode = "DV";
                DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(_idDoituongKcb);
                if (objectType != null)
                {
                    objecttypeCode = Utility.sDbnull(objectType.MaDoituongKcb);
                }
                m_dtDanhsachDichvuKCB = THU_VIEN_CHUNG.LayDsach_Dvu_KCB(objecttypeCode, "ALL", -1,-1);
                Get_KIEUKHAM(objecttypeCode);
                Get_PHONGKHAM(objecttypeCode);
                AutocompleteMaDvu();
                AutocompleteKieuKham();
                //AutocompletePhongKham();

                m_dtDanhsachDichvuKCB.AcceptChanges();
                cboKieuKham.DataSource = m_dtDanhsachDichvuKCB;
                cboKieuKham.DataMember = DmucDichvukcb.Columns.IdDichvukcb;
                cboKieuKham.ValueMember = DmucDichvukcb.Columns.IdDichvukcb;
                cboKieuKham.DisplayMember = DmucDichvukcb.Columns.TenDichvukcb;
                //cboKieuKham.ValueChanged += new EventHandler(cboKieuKham_ValueChanged);
                //  cboKieuKham.Visible = globalVariables.UserName == "ADMIN";
                if (m_dtDanhsachDichvuKCB == null || m_dtDanhsachDichvuKCB.Columns.Count <= 0) return;
                AllowTextChanged = true;
                if (m_dtDanhsachDichvuKCB.Rows.Count == 1 )
                {
                    cboKieuKham.SelectedIndex = 0;
                    var idKieukham = (from s in m_dtDanhsachDichvuKCB.AsEnumerable()
                                      select s).FirstOrDefault();
                    if (idKieukham != null)
                    {
                        txtIDPkham.Text = Utility.sDbnull(idKieukham["id_phongkham"]);
                        txtIDKieuKham.Text = Utility.sDbnull(idKieukham["id_kieukham"]);
                    }
                   
                    AutoLoadKieuKham();
                }
                AllowTextChanged = oldStatus;
            }
            catch
            {
                AllowTextChanged = oldStatus;
            }
        }
        private void AutocompleteKieuKham()
        {
            try
            {
                if (m_kieuKham == null) return;
                if (!m_kieuKham.Columns.Contains("ShortCut"))
                    m_kieuKham.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                txtKieuKham.Init(m_kieuKham, new List<string>() { DmucKieukham.Columns.IdKieukham, DmucKieukham.Columns.MaKieukham, DmucKieukham.Columns.TenKieukham });
                txtKieuKham.RaiseEnterEvents();

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        void cboKieuKham_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (AutoLoad || cboKieuKham.SelectedIndex == -1) return;
                int iddichvukcb = Utility.Int32Dbnull(cboKieuKham.Value);
                DmucDichvukcb objDichvuKCB =
                DmucDichvukcb.FetchByID(Utility.Int32Dbnull(cboKieuKham.Value));
                _objDoituongKcb =
                    new Select().From(DmucDoituongkcb.Schema)
                        .Where(DmucDoituongkcb.MaDoituongKcbColumn)
                        .IsEqualTo(_objDoituongKcb.MaDoituongKcb)
                        .ExecuteSingle<DmucDoituongkcb>();
                DmucKhoaphong objdepartment =
                    new Select().From(DmucKhoaphong.Schema)
                        .Where(DmucKhoaphong.MaKhoaphongColumn)
                        .IsEqualTo(globalVariables.MA_KHOA_THIEN)
                        .ExecuteSingle<DmucKhoaphong>();
                if (objDichvuKCB != null)
                {
                    txtKieuKham.SetId(objDichvuKCB.IdKieukham);
                    txtIDPkham.Text = Utility.sDbnull(objDichvuKCB.IdPhongkham);
                    cboKieuKham.Text = Utility.sDbnull(objDichvuKCB.TenDichvukcb);
                    //txtPhongkham._Text=
                }
                else
                    txtKieuKham.SetId(-1);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }

        }
       
        private void AutocompleteMaDvu()
        {
            DataRow[] arrDr = null;
            if (m_dtDanhsachDichvuKCB == null) return;
            if (!m_dtDanhsachDichvuKCB.Columns.Contains("ShortCut"))
                m_dtDanhsachDichvuKCB.Columns.Add(new DataColumn("ShortCut", typeof(string)));
            arrDr = m_dtDanhsachDichvuKCB.Select(DmucDoituongkcb.Columns.MaDoituongKcb + "='ALL' OR " + DmucDoituongkcb.Columns.MaDoituongKcb + "='" + _objDoituongKcb.MaDoituongKcb + "'");
            if (arrDr.Length <= 0)
            {
                this.txtExamtypeCode.AutoCompleteList = new List<string>();
                return;
            }
            txtExamtypeCode.Init(m_dtDanhsachDichvuKCB, new List<string>() { DmucDichvukcb.Columns.IdDichvukcb, DmucDichvukcb.Columns.MaDichvukcb, DmucDichvukcb.Columns.TenDichvukcb });
        }
        private void Get_PHONGKHAM(string MA_DTUONG)
        {
            List<int> lstIdCongkham = (from p in m_dtDanhsachDichvuKCB.AsEnumerable()
                                       select Utility.Int32Dbnull(p.Field<int>(DmucDichvukcb.Columns.IdDichvukcb))).Distinct().ToList<int>();
            m_PhongKham = new Select().From(DmucKieukham.Schema).Where(DmucKieukham.Columns.IdKieukham).In(lstIdCongkham).ExecuteDataSet().Tables[0];
        }

        private void Get_KIEUKHAM(string MA_DTUONG)
        {
            List<Int16> lstIdKieuKham = (from p in m_dtDanhsachDichvuKCB.AsEnumerable()
                                         select Utility.Int16Dbnull(p.Field<Int16>(DmucDichvukcb.Columns.IdKieukham))).Distinct().ToList<Int16>();
            m_kieuKham = new Select().From(DmucKieukham.Schema).Where(DmucKieukham.Columns.IdKieukham).In(lstIdKieuKham).ExecuteDataSet().Tables[0];
        }
        void AutoselectcongkhambyKieukham_Phongkham()
        {
            if (txtPhongkham.MyID.ToString() != "-1")
            {
                DataRow[] arrDr = m_dtDanhsachDichvuKCB.Select(string.Format("id_dichvukcb={0} ", txtPhongkham.MyID.ToString()));
                //DataRow[] arrDr = m_dtDanhsachDichvuKCB.Select(string.Format("id_kieukham={0} and id_phongkham={1}", txtKieuKham.MyID.ToString(), txtPhongkham.MyID.ToString()));
                if (arrDr.Length > 0)
                    cboKieuKham.SelectedIndex = Utility.GetSelectedIndex(cboKieuKham, arrDr[0][DmucDichvukcb.Columns.IdDichvukcb].ToString());
            }
        }
        void txtPhongkham__OnEnterMe()
        {
            AutoLoad = true;
            DataRow[] arrDr = m_dtDanhsachDichvuKCB.Select("id_dichvukcb=" + txtPhongkham.MyID);
            AutoselectcongkhambyKieukham_Phongkham();
            //cboKieuKham.SelectedIndex = Utility.GetSelectedIndex(cboKieuKham, txtPhongkham.MyID.ToString());//Text = arrDr.Length <= 0 ? "---Chọn công khám----" : arrDr[0]["ten_dichvukcb"].ToString();
            //cboKieuKham_ValueChanged(cboKieuKham, new EventArgs());
            ////AutoLoadKieuKham();
        }

        void txtKieuKham__OnEnterMe()
        {
            AutoLoad = true;
            // AutoLoadKieuKham();
            AutoloadPhongkham_Congkham();
        }
        void AutoloadPhongkham_Congkham()
        {
            m_PhongKham = m_dtDanhsachDichvuKCB.Clone();

            DataRow[] arrdr = m_dtDanhsachDichvuKCB.Select("id_kieukham=" + txtKieuKham.MyID);
            if (arrdr.Length > 0)
                m_PhongKham = arrdr.CopyToDataTable();
            if (!m_PhongKham.Columns.Contains("TEN"))
                m_PhongKham.Columns.AddRange(new DataColumn[] { new DataColumn("Id", typeof(string)), new DataColumn("Ma", typeof(string)), new DataColumn("TEN", typeof(string)) });
            foreach (DataRow dr in m_PhongKham.Rows)
            {
                dr["id"] = dr["id_dichvukcb"].ToString();// string.Format("{0}-{1}", dr["id_phongkham"].ToString(), dr["id_dichvukcb"].ToString());
                dr["ma"] = dr["ma_phongkham"].ToString();// string.Format("{0}-{1}", dr["ma_phongkham"].ToString(), dr["ma_dichvukcb"].ToString());
                dr["ten"] = string.Format("{0}-{1}-Đơn giá:{2}", dr["ten_phongkham"].ToString(), dr["ten_dichvukcb"].ToString(), dr["don_gia"].ToString());
            }
            txtPhongkham.Init(m_PhongKham, new List<string>() { "id", "ma", "ten" });
        }
        void txtPhongkham__OnSelectionChanged()
        {
            AutoLoadKieuKham();
        }

        void txtKieuKham__OnSelectionChanged()
        {
            AutoLoadKieuKham();
        }
        private void AutoLoadKieuKham()
        {
            try
            {
                if ((Utility.Int32Dbnull(txtIDKieuKham.Text, -1) == -1 || Utility.Int32Dbnull(txtIDPkham.Text, -1) == -1) && !pnlChonKieukham.Visible)
                {
                    cboKieuKham.Text = @"CHỌN PHÒNG KHÁM";
                    cboKieuKham.SelectedIndex = -1;
                    return;
                }
                
                DataRow[] arrDr = null;
                if (!pnlChonKieukham.Visible)
                {
                    arrDr =
                 m_dtDanhsachDichvuKCB.Select("(ma_doituong_kcb='ALL' OR ma_doituong_kcb='" + _objDoituongKcb.MaDoituongKcb + "') AND id_kieukham= " +
                                               txtIDKieuKham.Text.Trim() + " AND  id_phongkham = " + txtIDPkham.Text.Trim());
                    if (arrDr.Length <= 0)
                        arrDr =
                       m_dtDanhsachDichvuKCB.Select("(ma_doituong_kcb='ALL' OR ma_doituong_kcb='" + _objDoituongKcb.MaDoituongKcb + "') AND id_kieukham=" +
                                                      txtIDKieuKham.Text.Trim() + " AND id_phongkham = '-1' ");
                }//Chọn theo hình thức kiểu khám+ công khám của các phòng
                else
                {
                    arrDr =
               m_dtDanhsachDichvuKCB.Select("(ma_doituong_kcb='ALL' OR ma_doituong_kcb='" + _objDoituongKcb.MaDoituongKcb + "') AND id_kieukham= " +
                                            Utility.sDbnull(txtKieuKham.MyID) + " AND  id_phongkham = " + Utility.sDbnull(txtPhongkham.MyID));
                }
                //nếu ko có đích danh phòng thì lấy dịch vụ bất kỳ theo kiểu khám và đối tượng

                if (arrDr.Length <= 0)
                {
                    cboKieuKham.Text = @"CHỌN PHÒNG KHÁM";
                    cboKieuKham.SelectedIndex = -1;
                    return;
                }
                else
                {
                    cboKieuKham.Text = arrDr[0][DmucDichvukcb.Columns.TenDichvukcb].ToString();
                    txtIDPkham.Text = arrDr[0][DmucDichvukcb.Columns.IdPhongkham].ToString();
                    return;
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
            finally
            {
                AutoLoad = false;
            }
        }
        #endregion
       
        private void LoadTrangTraiIn()
        {
            try
            {
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy trạng thái in");
            }
        }

       
        private void LoadPhongkhamNoitru()
        {
            DataBinding.BindDataCombobox(cboKhoanoitru,
                                                 THU_VIEN_CHUNG.LaydanhsachKhoanoitruTheoBacsi(globalVariables.UserName,Utility.Bool2byte( globalVariables.IsAdmin),(byte)1),
                                                 DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                                 "---Chọn khoa nội trú---",false,true);
           
        }
        DataTable m_ExamTypeRelationList = new DataTable();
        private void LoadExamTypeRelation()
        {
            bool oldStatus = AllowTextChanged;
            try
            {
                //cboKieuKham.DataSource = null;
                ////Khởi tạo danh mục Loại khám
                //string objecttype_code = "DV";
                //DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(ObjectTypeId);
                //if (objectType != null)
                //{
                //    objecttype_code = Utility.sDbnull(objectType.MaDoituongKcb);
                //}
                //VNS.HIS.UCs.Libs.AutocompleteKieuKham(objLuotkham.MaDoituongKcb, txtKieuKham);
                //VNS.HIS.UCs.Libs.AutocompletePhongKham(objLuotkham.MaDoituongKcb, txtACPhongkham);
               
                //m_ExamTypeRelationList = THU_VIEN_CHUNG.LayDsach_Dvu_KCB(objecttype_code);
                
                //m_ExamTypeRelationList.AcceptChanges();
                //cboKieuKham.DataSource = m_ExamTypeRelationList;
                //cboKieuKham.DataMember = DmucDichvukcb.Columns.IdDichvukcb;
                //cboKieuKham.ValueMember = DmucDichvukcb.Columns.IdDichvukcb;
                //cboKieuKham.DisplayMember = DmucDichvukcb.Columns.TenDichvukcb;
                //cboKieuKham.Visible = globalVariables.UserName == "ADMIN";
                //if (m_ExamTypeRelationList == null || m_ExamTypeRelationList.Columns.Count <= 0) return;
                //AllowTextChanged = true;
                //if (m_ExamTypeRelationList.Rows.Count == 1 && m_enAction != action.Update)
                //{
                //    cboKieuKham.SelectedIndex = 0;

                //}
                AllowTextChanged = oldStatus;
            }
            catch
            {
                AllowTextChanged = oldStatus;
            }
        }
        private void cboDoctorAssign_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cboBSDieutri.Text))
                {
                    cboBSDieutri.DroppedDown = true;
                }
                else
                {
                    cboBSDieutri.DroppedDown = false;
                }
            }
            catch (Exception)
            {
                // throw;
            }
        }

       
        private KcbLuotkham CreatePatientExam()
        {
            SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(malankham)
                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtPatient_ID.Text));
            var objPatientExam = sqlQuery.ExecuteSingle<KcbLuotkham>();
            return objPatientExam;
        }

        void ClearControls(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is EditBox)
                {
                    ((EditBox)(control)).Clear();
                }
                else if (control is MaskedEditBox)
                {
                    control.Text = "";
                }
                else if (control is VNS.HIS.UCs.AutoCompleteTextbox)
                {
                    ((VNS.HIS.UCs.AutoCompleteTextbox)control)._Text = "";
                }
                else if (control is TextBox)
                {
                    ((TextBox)(control)).Clear();
                }
            }
        }
        private void ClearControl()
        {
            try
            {
                grd_ICD.DataSource = null;
                grdAssignDetail.DataSource = null;
                grdPresDetail.DataSource = null;
                grdVTTH.DataSource = null;
                grdPhieudieutri.DataSource = null;
                grdBuongGiuong.DataSource = null;
                grdChedoDinhduong.DataSource = null;
                grdTamung.DataSource = null;
                grdGoidichvu.DataSource = null;
                grdVTTH_tronggoi.DataSource = null;
                txtIdPhieudieutri.Text = "-1";
                txtGiuong.Clear();
                txtBuong.Clear();
                txtKhoanoitru.Clear();
                ClearControls(pnlPatientInfor);
                ClearControls(pnlThongtinBNKCB);
                ClearControls(pnlKetluan);
                ClearControls(pnlother);
            }
            catch (Exception)
            {
            }
        }

        private void getResult()
        {
            try
            {
                //QueryCommand cmd = KcbKetquaCl.CreateQuery().BuildCommand();
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandSql = "Select *," +
                //                 "(select TOP 1 id_dichvu from dmuc_dichvucls_chitiet where id_chitietdichvu=p.Loai_XN ) as id_dichvu," +
                //                 "(select TOP 1 IntOrder from dmuc_dichvucls_chitiet where id_chitietdichvu=p.Loai_XN ) as sIntOrder," +
                //                 "(select TOP 1 ten_dichvu from dmuc_dichvucls where id_dichvu in(select TOP 1 id_dichvu from dmuc_dichvucls_chitiet where id_chitietdichvu=p.Loai_XN ) ) as ten_dichvu," +
                //                 "(select TOP 1 intOrder from dmuc_dichvucls where id_dichvu in(select TOP 1 id_dichvu from dmuc_dichvucls_chitiet where id_chitietdichvu=p.Loai_XN ) ) as stt_hthi_dichvu " +
                //                 "from kcb_ketqua_cls p " +
                //                 "where ID_CHI_DINH in(Select id_chidinh  from kcb_chidinhcls where ma_luotkham='" +
                //                 malankham.Trim() + "') order by stt_hthi_dichvu,sIntOrder,Ten_KQ";
                //DataTable temdt = DataService.GetDataSet(cmd).Tables[0];
                //uiGroupBox6.Width = temdt != null && temdt.Rows.Count > 0 ? 300 : 0;
                //Utility.SetDataSourceForDataGridEx(grdKetQua, temdt, true, true, "", "");
            }
            catch
            {
            }
        }

        void DisableButton(Control parent,bool Enabled)
        {
            foreach (Control ctr in parent.Controls)
                if (ctr.GetType().Equals(cmdSaochep.GetType()) || ctr.GetType().Equals(cmdSaochep.GetType()))
                    ctr.Enabled = Enabled;
            Application.DoEvents();
        }
        Int16 id_khoasua = -1;
        bool CheckDachuyenkhoa()
        {
            try
            {
                //return false;
                if (objNoitruPhanbuonggiuong == null)
                {
                    Utility.ShowMsg("Vui lòng chọn người bệnh từ Danh sách bệnh nhân trước khi thực hiện chức năng");
                    return false;
                }
                id_khoasua = Utility.Int16Dbnull(grdList.GetValue("id_khoasua"), -1);
                if (lstIdkhoaLienkhoa.Contains(idKhoaNoitru.ToString()) && id_khoasua <= 0)
                    id_khoasua = (Int16)idKhoaNoitru;

                if (id_khoasua > 0)//Chỉ cần kiểm tra phiếu điều trị là của khoa người dùng đang chọn là cho thực hiện
                {
                    //Xử lý ở phần chọn phiếu điều trị
                }
                else
                {
                    objLuotkham = new Select().From(KcbLuotkham.Schema)
                                      .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                      .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).ExecuteSingle<KcbLuotkham>();
                    if (objNoitruPhanbuonggiuong != null && objPhieudieutri != null && objNoitruPhanbuonggiuong.IdKhoanoitru != objPhieudieutri.IdKhoanoitru)
                    {
                        Utility.ShowMsg("Phiếu điều trị đang chọn được thực hiện bởi khoa khác nên bạn chỉ có thể xem và không được phép thao tác sửa-xóa-sao chép");
                        DisableButton(flowCls, false);
                        //DisableButton(flowCongkham, false);
                        DisableButton(flowGoi, false);
                        DisableButton(FlowGoi1, false);
                        DisableButton(flowThuoc, false);
                        DisableButton(flowVTTH, false);
                        DisableButton(pnlDinhDuong, false);
                        cmdSaochep.Enabled = cmdSuaphieudieutri.Enabled = cmdxoaphieudieutri.Enabled = objNoitruPhanbuonggiuong.IdKhoanoitru == objPhieudieutri.IdKhoanoitru;
                        cmdthemphieudieutri.Enabled = true;
                        return true;
                    }
                    if (objPhieudieutri != null && !globalVariables.IsAdmin && objPhieudieutri.NguoiTao != globalVariables.UserName)
                    {
                        Utility.ShowMsg(string.Format("Phiếu điều trị đang chọn được thực hiện bởi nhân viên {0} nên bạn chỉ có thể xem và không được phép thao tác sửa-xóa", objPhieudieutri.NguoiTao));
                        DisableButton(flowCls, false);
                        //DisableButton(flowCongkham, false);
                        DisableButton(flowGoi, false);
                        DisableButton(FlowGoi1, false);
                        DisableButton(flowThuoc, false);
                        DisableButton(flowVTTH, false);
                        DisableButton(pnlDinhDuong, false);
                        cmdSuaphieudieutri.Enabled = cmdxoaphieudieutri.Enabled = objPhieudieutri.NguoiTao == globalVariables.UserName;
                        cmdthemphieudieutri.Enabled = true;
                        return true;
                    }
                    if (objLuotkham.IdKhoanoitru != objNoitruPhanbuonggiuong.IdKhoanoitru)//Bệnh nhân đã chuyển khoa nên không được phép thao tác và chỉ được phép xem thông tin
                    {
                        if (Utility.Laygiatrithamsohethong("NOITRU_KHOATINHNANGSAUCHUYENKHOA", "0", true) == "1")
                        {
                            Utility.ShowMsg(string.Format("Khoa nội trú bạn đang chọn làm việc {0} khác so với khoa hiện tại của người bệnh({1}-Do có thể nhân viên nào đó đã thực hiện thao tác chuyển khoa điều trị cho người bệnh). Do vậy, bạn chỉ được phép xem lại các thông tin và không được phép hiệu chỉnh.\nMuốn hiệu chỉnh thì cần liên hệ nội bộ hoặc với khoa {2} để được chuyển khoa sửa.", grdList.GetValue("ten_khoanoitru").ToString(), objkhoaphonghientai.TenKhoaphong, objkhoaphonghientai.TenKhoaphong));
                            DisableButton(flowCls, false);
                            //DisableButton(flowCongkham, false);
                            DisableButton(flowGoi, false);
                            DisableButton(FlowGoi1, false);
                            DisableButton(flowThuoc, false);
                            DisableButton(flowVTTH, false);
                            DisableButton(pnlDinhDuong, false);
                            cmdSaochep.Enabled = cmdSuaphieudieutri.Enabled = cmdxoaphieudieutri.Enabled = false;
                            cmdthemphieudieutri.Enabled = true;
                            return true;
                        }
                        //return;
                    }
                    else
                    {
                        flowCls.Enabled = flowCongkham.Enabled = flowGoi.Enabled = FlowGoi1.Enabled = flowThuoc.Enabled = flowVTTH.Enabled = flowPhieudieutri.Enabled =pnlDinhDuong.Enabled= true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return false;
            }

        }
        bool isReadOnly = false;
        DmucKhoaphong objkhoaphonghientai = null;
        int idKhoaNoitru = -1;
        string tenkhoanoitru = "";
        KcbChandoanKetluan _KcbChandoanKetluan = null;
        private bool isNhapVien = false;
        List<string> lstIdkhoaLienkhoa = new List<string>();
        /// <summary>
        /// Lấy về thông tin bệnh nhâ nội trú
        /// </summary>
        private void GetData()
        {
            try
            {
                isReadOnly = false;
                lstIdkhoaLienkhoa = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_KHOASUA", "-1", true).Split(',').ToList<string>();
                objNoitruPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(Utility.Int32Dbnull(grdList.GetValue("id"), 0));
                id_khoasua = Utility.Int16Dbnull(grdList.GetValue("id_khoasua"), -1);
                
                txtKhoanoitru_lapphieu.Text = Utility.GetValueFromGridColumn(grdBuongGiuong, "ten_khoanoitru");
                txtBuong_lapphieu.Text = Utility.GetValueFromGridColumn(grdBuongGiuong, "ten_buong");
                txtGiuong_lapphieu.Text = Utility.GetValueFromGridColumn(grdBuongGiuong, "ten_giuong");

                idKhoaNoitru = Utility.Int32Dbnull(cboKhoanoitru.SelectedValue,-1);
                if (lstIdkhoaLienkhoa.Contains(idKhoaNoitru.ToString()) && id_khoasua <= 0)
                    id_khoasua =(Int16) idKhoaNoitru ;
                tenkhoanoitru = cboKhoanoitru.Text;
                objPhieudieutri = null;
               // Utility.SetMsg(lblMsg, "", false);
                string PatientCode = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham), "");
                malankham = PatientCode;
                int Patient_ID = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                objLuotkham = new Select().From(KcbLuotkham.Schema)
                    .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(PatientCode)
                    .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Patient_ID).ExecuteSingle<KcbLuotkham>();
                objBenhnhan = KcbDanhsachBenhnhan.FetchByID(objLuotkham.IdBenhnhan);
               
                if (objLuotkham != null)
                {
                    if (id_khoasua > 0)//Nếu id khoa sửa >0 thì lấy lại bản ghi phân buồng giường gần nhất tại khoa đó để căn cứ làm phiếu điều trị, pdt bổ sung cho chuẩn. Nếu ko sẽ lấy nhầm sang khoa hiện tại
                    {
                        DataTable dtBG = SPs.NoitruBuongiuongLaybuongiuonggannhattheokhoa(id_khoasua, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                        if (dtBG != null && dtBG.Rows.Count > 0)
                            objNoitruPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(Utility.Int32Dbnull(dtBG.Rows[0]["id"], 0));
                    }
                    if (objLuotkham.TrangthaiNoitru >= 3)
                    {
                        Utility.SetMsg(lblMsg, Utility.Laythongtintrangthainguoibenh(objLuotkham), true);
                        Utility.ShowMsg(string.Format("{0} nên bạn chỉ có thể xem và không được phép thực hiện các thao tác khác", lblMsg.Text));
                        isReadOnly = true;
                        //return;
                    }
                    Utility.SetMsg(lblMsg, Utility.Laythongtintrangthainguoibenh(objLuotkham), false);
                    txtBsdieutrichinh.Text = grdList.GetValue("ten_bsi_dieutrichinh").ToString();
                    txtbsUyquyen.Text = grdList.GetValue("ten_bsi_dieutriphu").ToString();
                    objkhoaphonghientai = DmucKhoaphong.FetchByID(objLuotkham.IdKhoanoitru);
                    if (CheckDachuyenkhoa()) return;
                    //if (objLuotkham.IdKhoanoitru != objNoitruPhanbuonggiuong.IdKhoanoitru)//Bệnh nhân đã chuyển khoa nên không được phép thao tác và chỉ được phép xem thông tin
                    //{
                    //    if (Utility.Laygiatrithamsohethong("NOITRU_KHOATINHNANGSAUCHUYENKHOA", "0", true) == "1")
                    //    {
                    //        Utility.ShowMsg(string.Format("Khoa nội trú bạn đang chọn làm việc {0} khác so với khoa hiện tại của người bệnh({1}). Do vậy, bạn chỉ được phép xem lại các thông tin và không được phép hiệu chỉnh.\nMuốn hiệu chỉnh thì cần liên hệ nội bộ với khoa {2} để được chuyển khoa sửa.", grdPatientList.GetValue("ten_khoanoitru").ToString(), objkhoaphonghientai.TenKhoaphong, objkhoaphonghientai.TenKhoaphong));
                    //        flowCls.Enabled = flowCongkham.Enabled = flowGoi.Enabled = FlowGoi1.Enabled = flowThuoc.Enabled = flowVTTH.Enabled = flowPhieudieutri.Enabled = false;
                    //    }
                    //    //return;
                    //}
                    //else
                    //    flowCls.Enabled = flowCongkham.Enabled = flowGoi.Enabled = FlowGoi1.Enabled = flowThuoc.Enabled = flowVTTH.Enabled = flowPhieudieutri.Enabled = true;
                    ClearControl();
                    _idDoituongKcb = objLuotkham.IdDoituongKcb;
                    _objDoituongKcb = DmucDoituongkcb.FetchByID(_idDoituongKcb);
                    Loaddanhsachcongkhamdadangki();
                    NapthongtindanhmucCongkham();//Đoạn này ngày sau có BHYT thì đưa vào hàm getData() để lấy đúng dữ liệu công khám theo đối tượng người bệnh đang chọn
                    DmucNhanvien objStaff =
                        new Select().From(DmucNhanvien.Schema)
                            .Where(DmucNhanvien.UserNameColumn)
                            .IsEqualTo(Utility.sDbnull(objLuotkham.NguoiKetthuc, ""))
                            .ExecuteSingle<DmucNhanvien>();
                    string TenNhanvien = objLuotkham.NguoiKetthuc;
                    if (objStaff != null)
                        TenNhanvien = objStaff.TenNhanvien;
                    
                    DataTable m_dtThongTin = _KCB_THAMKHAM.NoitruLaythongtinBenhnhan(objLuotkham.MaLuotkham,
                                                                          Utility.Int32Dbnull(objLuotkham.IdBenhnhan,-1));
                               
                        if (m_dtThongTin.Rows.Count > 0)
                        {
                            DataRow dr = m_dtThongTin.Rows[0];
                            if (dr != null)
                            {
                               
                                dtInput_Date.Value = Convert.ToDateTime(dr[KcbLuotkham.Columns.NgayTiepdon]);
                               
                                txtGioitinh.Text =
                                    Utility.sDbnull(grdList.GetValue(KcbDanhsachBenhnhan.Columns.GioiTinh), "");
                                txtPatient_Name.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                                txtPatient_ID.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.IdBenhnhan], "");
                                txtPatient_Code.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.MaLuotkham], "");
                                txtCanhbao.Text = objBenhnhan.CanhBao;
                                txtCanhbao.Visible = lblCanhbao.Visible = Utility.sDbnull(txtCanhbao.Text).Length > 0;
                                txtDiaChi.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.DiaChi], "");
                                txtDiachiBHYT.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.DiachiBhyt], "");
                               
                                txtObjectType_Name.Text = Utility.sDbnull(dr[DmucDoituongkcb.Columns.TenDoituongKcb], "");
                                txtSoBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.MatheBhyt], "");
                                txtBHTT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.PtramBhytGoc], "0");
                                txtNgheNghiep.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.NgheNghiep], "");
                                txtHanTheBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.NgayketthucBhyt], "");
                                dtpNgayhethanBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.NgayketthucBhyt], globalVariables.SysDate.ToString("dd/MM/yyyy"));
                                txtTuoi.Text = Utility.sDbnull(Utility.Int32Dbnull(globalVariables.SysDate.Year) -
                                                               objBenhnhan.NgaySinh.Value.Year);
                                txtKhoanoitru.Text = Utility.sDbnull(dr["ten_khoanoitru"], "");
                                txtBuong.Text = Utility.sDbnull(dr["ten_buong"], "");
                                txtGiuong.Text = Utility.sDbnull(dr["ten_giuong"], "");
                               // LayLichsuBuongGiuong();
                                LaydanhsachPhieudieutri();
                                LayLichsuTamung();
                                TinhtoanTongchiphi();
                            }
                        }
                    

                }
                ModifyCommmands();
            }
            catch
            {
            }
            finally
            {

                ShowResult();
            }
        }
        DataTable m_dtTamung = null;
        DataTable m_dtPhieudieutri = null;
        DataTable m_dtBuongGiuong = null;
        void LayLichsuTamung()
        {
            try
            {
                DataTable dt_AllData = new KCB_THAMKHAM().NoitruTimkiemlichsuNoptientamung(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan, 0, -1, (byte)1);
                DataRow[] arrDr = dt_AllData.Select("Kieu_tamung=0");
                m_dtTamung = dt_AllData.Clone();
                if (arrDr.Length > 0) m_dtTamung = arrDr.CopyToDataTable();
                Utility.SetDataSourceForDataGridEx_Basic(grdTamung, m_dtTamung, false, true, "1=1", NoitruTamung.Columns.NgayTamung + " desc");
                //Utility.SetDataSourceForDataGridEx_Basic(grdTamung, m_dtTamung, false, true, "1=1", NoitruTamung.Columns.NgayTamung + " desc");
                grdTamung.MoveFirst();
            }
            catch (Exception ex)
            {

            }
            finally
            {
               
            }
        }
       
        void LayLichsuBuongGiuong()
        {
            try
            {
                if (m_dtBuongGiuong != null) m_dtBuongGiuong.Rows.Clear();
                m_dtBuongGiuong = _KCB_THAMKHAM.NoitruTimkiemlichsuBuonggiuong(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan,"-1",-1);
                Utility.SetDataSourceForDataGridEx_Basic(grdBuongGiuong, m_dtBuongGiuong, false, true, "1=1", NoitruPhanbuonggiuong.Columns.NgayVaokhoa + " desc");
               //Tự động dò tới khoa hiện tại
                Utility.GotoNewRowJanus(grdBuongGiuong, NoitruPhanbuonggiuong.Columns.Id, Utility.Int32Dbnull(objLuotkham.IdRavien, 0).ToString());
                GetNoitruPhanbuonggiuong();

            }
            catch (Exception ex)
            {

            }
        }
        void LaydanhsachPhieudieutri()
        {
            try
            {
                string v_intIdKhoanoitru = Utility.sDbnull(objLuotkham.IdKhoanoitru, "-1");
                if(objNoitruPhanbuonggiuong!=null)
                    v_intIdKhoanoitru = Utility.sDbnull(objNoitruPhanbuonggiuong.IdKhoanoitru, "-1");
                if (id_khoasua > 0)
                    v_intIdKhoanoitru = id_khoasua.ToString();
                bool IsAdmin = Utility.Coquyen("quyen_xemphieudieutricuabacsinoitrukhac")
                    || THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_CHOPHEPXEM_PHIEUDIEUTRI_CUABACSIKHAC", "0", false) == "1";
                m_dtPhieudieutri = _KCB_THAMKHAM.NoitruTimkiemphieudieutriTheoluotkham(Utility.Bool2byte(IsAdmin),chkViewAll.Checked?"01/01/1900": dtpNgaylapphieu.Value.ToString("dd/MM/yyyy"),
                    objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan, v_intIdKhoanoitru, Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtSongay.Text, -1)));
                Utility.SetDataSourceForDataGridEx_Basic(grdPhieudieutri,m_dtPhieudieutri,false,true,"1=1",NoitruPhieudieutri.Columns.NgayDieutri+" desc");
                grdPhieudieutri.MoveFirst();
                Utility.UpdateGroup(grdPhieudieutri, m_dtPhieudieutri, "ten_khoanoitru", "Khoa điều trị");
            }
            catch (Exception ex)
            {
                
            }
        }
        
        void LoadBenh()
        {
            try
            {
                    AllowTextChanged = true;
                    isLike = false;
                    txtChanDoan._Text = Utility.sDbnull(_KcbChandoanKetluan.Chandoan);
                    txtChanDoanKemTheo.Text = Utility.sDbnull(_KcbChandoanKetluan.ChandoanKemtheo);
                    txtMaBenhChinh.SetCode( Utility.sDbnull(_KcbChandoanKetluan.MabenhChinh));
                    string dataString = Utility.sDbnull(_KcbChandoanKetluan.MabenhPhu, "");
                    isLike = true;
                    AllowTextChanged = false;
                    dt_ICD_PHU.Clear();
                    if (!string.IsNullOrEmpty(dataString))
                    {
                        string[] rows = dataString.Split(',');
                        foreach (string row in rows)
                        {
                            if (!string.IsNullOrEmpty(row))
                            {
                                DataRow newDr = dt_ICD_PHU.NewRow();
                                newDr[DmucBenh.Columns.MaBenh] = row;
                                newDr[DmucBenh.Columns.TenBenh] = GetTenBenh(row);
                                dt_ICD_PHU.Rows.Add(newDr);
                                dt_ICD_PHU.AcceptChanges();
                            }
                        }
                        grd_ICD.DataSource = dt_ICD_PHU;
                    }
                
            }
            catch
            {
            }
        }
        void ModifyByLockStatus(byte lockstatus)
        {
            cmdCreateNewPres.Enabled = !Utility.isTrue(lockstatus);
            cmdUpdatePres.Enabled = grdPresDetail.RowCount > 0 && !Utility.isTrue(lockstatus);
            cmdDeletePres.Enabled = grdPresDetail.RowCount > 0 && !Utility.isTrue(lockstatus);
            cmdIndonthuoc.Enabled = grdPresDetail.RowCount > 0 && !string.IsNullOrEmpty(malankham);
            ctxDelDrug.Enabled = cmdUpdatePres.Enabled;
           
            cmdInsertAssign.Enabled = !Utility.isTrue(lockstatus);
            cmdUpdate.Enabled = grdAssignDetail.RowCount > 0 && !Utility.isTrue(lockstatus);
            cmdDelteAssign.Enabled = grdAssignDetail.RowCount > 0 && !Utility.isTrue(lockstatus);
            cmdPrintAssign.Enabled = grdAssignDetail.RowCount > 0 && !string.IsNullOrEmpty(malankham);
            chkIntach.Enabled = cmdPrintAssign.Enabled;
            cboServicePrint.Enabled = cmdPrintAssign.Enabled;
            //ctxDelCLS.Enabled = cmdUpdate.Enabled;
        }
      
        private string GetTenBenh(string MaBenh)
        {
            string TenBenh = "";
            DataRow[] arrMaBenh = globalVariables.gv_dtDmucBenh.Select(string.Format(DmucBenh.Columns.MaBenh+ "='{0}'", MaBenh));
            if (arrMaBenh.GetLength(0) > 0) TenBenh = Utility.sDbnull(arrMaBenh[0][DmucBenh.Columns.TenBenh], "");
            return TenBenh;
        }

       
        void HienthithongtinBN()
        {
            try
            {
                
                if (!Utility.isValidGrid(grdList))
                {
                    ClearControl();
                    return;

                }
                if (ycphanBG)
                {
                    int id_buong = Utility.Int32Dbnull(grdList.GetValue("id_buong"), -1);
                    if (id_buong <= 0)
                    {
                        Utility.ShowMsg("Người bệnh chưa được phân buồng - giường nên không thể lập phiếu điều trị(Y lệnh) được. Vui lòng kiểm tra lại");
                        return;
                    }
                }
                m_blnHasLoaded = false;
                AllowTextChanged = false;
                if (dt_ICD_PHU != null) dt_ICD_PHU.Rows.Clear();
                GetData();
                ModifyByLockStatus(objLuotkham.Locked);
               
                txtMach.Focus();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                    Utility.ShowMsg(exception.ToString());
                //throw;
            }
            finally
            {
                setChanDoan();
                AllowTextChanged = true;
                m_blnHasLoaded = true;
                grdPhieudieutri_SelectionChanged(grdPhieudieutri, new EventArgs());
            }
        }
        private void grdPatientList_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "colChon")
                {
                    //_buttonClick = true;
                    HienthithongtinBN();
                }
            }
            catch (Exception exception)
            {
               
            }
            
        }

        /// <summary>
        /// hàm thực hiện viecj dóng form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        void Unlock()
        {
            try
            {
                if (objLuotkham == null)
                    return;
                //Kiểm tra nếu đã in phôi thì cần hủy in phôi
                KcbPhieuDct _item = new Select().From(KcbPhieuDct.Schema)
                    .Where(KcbPhieuDct.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .And(KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).ExecuteSingle<KcbPhieuDct>();
                if (_item != null)
                {
                    Utility.ShowMsg("Bệnh nhân này thuộc đối tượng BHYT đã được in phôi. Bạn cần liên hệ bộ phận thanh toán hủy in phôi để mở khóa bệnh nhân");
                    return;
                }
                new Update(KcbLuotkham.Schema)
                                   .Set(KcbLuotkham.Columns.Locked).EqualTo(0)
                                   .Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(string.Empty)
                                   .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(null)
                                   .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(
                                       objLuotkham.MaLuotkham)
                                   .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                objLuotkham.Locked = 0;
                ModifyByLockStatus(objLuotkham.Locked);
                GetData();
            }
            catch
            {
            }
        }
        /// <summary>
        /// hàm thực hiện việc dùng phím tắt in phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_QuanlyPhieudieutri_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if ((ActiveControl!=null && ActiveControl.Name == grdList.Name) || (this.TabPageChanDoan.ActiveControl != null && this.TabPageChanDoan.ActiveControl.Name == txtMaBenhphu.Name))
                    return;
                else
                    SendKeys.Send("{TAB}");
            if ((e.Control && e.KeyCode == Keys.P) || e.KeyCode==Keys.F4)
            {
                if (tabDiagInfo.SelectedTab == tabPageChiDinhCLS)
                    cmdIntachPhieu_Click(cmdIntachPhieu, new EventArgs());
                else
                    cmdPrintPres_Click(cmdInPhieutruyendich, new EventArgs());
            }
            else if (e.Control & e.KeyCode == Keys.C) SaochepMaluotkham();
            if (e.Control & e.KeyCode==Keys.F) cmdSearch.PerformClick();
            if (e.KeyCode == Keys.Escape) Close();
            if (e.Control && e.KeyCode == Keys.U)
                Unlock();
            if (e.Control && e.KeyCode == Keys.F5)
            {
                //splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            }
            //Keyvalue=49-->1
            if (e.KeyCode == Keys.F11 && PropertyLib._AppProperties.ShowActiveControl) Utility.ShowMsg(this.ActiveControl.Name);
            if (e.KeyCode == Keys.F6)
            {
                txtPatient_Code.SelectAll();
                txtPatient_Code.Focus();
                return;
            }
            if (e.KeyCode == Keys.F1)
            {
                Utility.Showhelps(this.GetType().Assembly.ManifestModule.Name.Replace(".DLL","").Replace(".dll",""),this.Name);
            }
            if (e.Control && e.KeyValue == 49)
            {
                tabDiagInfo.SelectedTab = TabPageChanDoan;
                txtMach.Focus();
            }
            if (e.Control && e.KeyValue == 50)
            {
                tabDiagInfo.SelectedTab = tabPageChiDinhCLS;
                if (grdAssignDetail.RowCount <= 0)
                {
                    cmdInsertAssign.Focus();
                    cmdInsertAssign_Click(cmdInsertAssign, new EventArgs());
                }
                else
                {
                    cmdUpdate.Focus();
                    cmdUpdate_Click(cmdUpdate, new EventArgs());
                }
            }

            if (e.Control && e.KeyValue == 51)
            {
                tabDiagInfo.SelectedTab = tabPageChidinhThuoc;
                if (grdPresDetail.RowCount <= 0)
                {
                    cmdCreateNewPres.Focus();
                    cmdCreateNewPres_Click(cmdCreateNewPres, new EventArgs());
                }
                else
                {
                    cmdUpdatePres.Focus();
                    cmdUpdatePres_Click(cmdUpdatePres, new EventArgs());
                }
            }
            if (e.Control && e.KeyValue == 52)
            {
                if (tabDiagInfo.SelectedTab == tabPageChidinhThuoc) cmdInPhieutruyendich.PerformClick();
                if (tabDiagInfo.SelectedTab == tabPageChiDinhCLS) cmdPrintAssign.PerformClick();
            }
            
            if (e.Control && e.KeyCode == Keys.N)
            {
                if (tabDiagInfo.SelectedTab == tabPageChidinhThuoc)
                    cmdCreateNewPres_Click(cmdCreateNewPres, new EventArgs());
                else
                    cmdInsertAssign_Click(cmdInsertAssign, new EventArgs());
            }

        }
        void SaochepIdBN()
        {
            try
            {
                if (!Utility.isValidGrid(grdList)) return;
                string idbn = Utility.sDbnull(grdList.GetValue("id_benhnhan"), "-1");
                System.Windows.Forms.Clipboard.SetText(string.Format("{0}", idbn));
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void SaochepMaluotkham()
        {
            try
            {
                if (!Utility.isValidGrid(grdList)) return;
                string mlk = Utility.sDbnull(grdList.GetValue("ma_luotkham"), "-1");
                System.Windows.Forms.Clipboard.SetText(string.Format("{0}", mlk));
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void grdAssignDetail_SelectionChanged(object sender, EventArgs e)
        {
            RowCLS =Utility.findthelastChild(grdAssignDetail.CurrentRow);
            ModifyCommmands();
            ShowResult();
        }
        
        void Try2Splitter()
        {
            try
            {
                if (!File.Exists(SplitterPath))
                    return;

                List<int> lstSplitterSize = (from p in File.ReadLines(SplitterPath)
                                             select Utility.Int32Dbnull(p)).ToList<int>();
                if (lstSplitterSize != null && lstSplitterSize.Count == 3)
                {
                    splitContainer1.SplitterDistance = lstSplitterSize[0];
                    splitContainer2.SplitterDistance = Utility.Int32Dbnull(lstSplitterSize[1]);
                    splitContainer3.SplitterDistance = lstSplitterSize[2];
                    SplitterKQ = splitContainer3.SplitterDistance;
                }
            }
            catch (Exception ex)
            {
                //Utility.CatchException(ex);
            }
        }
        void ShowEditor(int id_chidinhchitiet)
        {
            #region "Backup"
            //pnlCKEditor.BringToFront();

            //vks = DmucVungkhaosat.FetchByID(id_VungKS);
            //txtNoiDung.Text = vks != null ? vks.MotaHtml : "";
            //richtxtKetluan.Text = Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "ket_luan"));
            //txtDenghi.Text = Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "de_nghi"));
            //string html = Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "mo_ta_HTML"), "");
            //if (html != "")
            //    txtNoiDung.Text = html;
            //timer1.Start();
            //LoadHTML();
            #endregion
            pnlCKEditor.BringToFront();
            DataTable dtKQCDHA = SPs.ClsLayketquaHa(id_chidinhchitiet).GetDataSet().Tables[0];
            if (dtKQCDHA.Rows.Count > 0)
            {
                txtNoiDung.Text = Utility.sDbnull(dtKQCDHA.Rows[0]["mota_html"], "");
                timer1.Start();
                LoadHTML();
            }
        }
        void LoadHTML()
        {
            string noidung = txtNoiDung.Text;
            webBrowser1.Document.InvokeScript("setValue", new[] { noidung });
        }
        void Try2HideResult()
        {
            if (!Utility.isValidGrid(grdAssignDetail))
            {
                grdKetqua.DataSource = null;
                txtNoiDung.Clear();
                splitContainer3.Panel2Collapsed = true;
                currRowIdx = -1;
                return;
            }
        }
        DataTable dtKQXN = null;
        bool CKEditorInput = true;
        int SplitterKQ = -1;
        private void ShowResult()
        {
            try
            {
                Try2HideResult();
                bool VisibleKQXN = Utility.Laygiatrithamsohethong("THAMKHAM_NHAPKQ_XN", "0", true) == "1";
                dtKQXN = null;
                //uiTabKqCls.Width = 0;
                CKEditorInput = Utility.GetValueFromGridColumn(grdAssignDetail, KcbChidinhclsChitiet.Columns.ResultType) == "1";
                Int16 trangthaiChitiet =
                    Utility.Int16Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.TrangthaiChitiet), 0);
                Int16 coChitiet =
                    Utility.Int16Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.CoChitiet), 0);

                int idChitietdichvu =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietdichvu), 0);
                int idDichvu =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdDichvu), 0);


                int idChidinh =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChidinh), 0);
                string ketluanHa =
                  Utility.sDbnull(
                      Utility.GetValueFromGridColumn(grdAssignDetail, "ketluan_ha"), "");
                string maloaiDichvuCls =
                    Utility.sDbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaLoaidichvu), "XN");
                int idChitietchidinh =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietchidinh), 0);
                string maChidinh =
                    Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaChidinh),
                                    "XN");
                string maBenhpham =
                    Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaBenhpham),
                                    "XN");
                if (Utility.Coquyen("quyen_nhap_kqxn"))
                    grdKetqua.RootTable.Columns["Ket_qua"].EditType = EditType.TextBox;
                else
                    grdKetqua.RootTable.Columns["Ket_qua"].EditType = EditType.NoEdit;

                if (trangthaiChitiet <= 2)
                //0=Mới chỉ định;1=Đã chuyển CLS;2=Đang thực hiện;3= Đã có kết quả CLS;4=Đã xác nhận kết quả
                {
                    if (maloaiDichvuCls != "XN")
                    {
                        pnlXN.SendToBack();
                        splitContainer3.Panel2Collapsed = true;
                    }
                    else
                    {
                        pnlXN.BringToFront();
                        splitContainer3.Panel2Collapsed = true;
                        ShowKQXN();
                        //mnuNhapKQXN.Visible = true; 
                    }

                    Application.DoEvents();
                }
                else//Có kết quả CLS
                {
                    if (maloaiDichvuCls == "XN")
                        pnlXN.BringToFront();
                    else
                        pnlXN.SendToBack();
                    splitContainer3.Panel2Collapsed = false;
                    if (SplitterKQ > 0)
                        splitContainer3.SplitterDistance = SplitterKQ;
                    //Utility.ShowColumns(grdKetqua, coChitiet == 1 ? lstKQCochitietColumns : lstKQKhongchitietColumns);
                    //Lấy dữ liệu CLS
                    if (maloaiDichvuCls == "XN")
                    {
                        //mnuNhapKQXN.Visible = true;

                        ShowKQXN();
                        //dtKQXN =
                        //    SPs.ClsTimkiemketquaXNChitiet(objLuotkham.MaLuotkham, maChidinh, maBenhpham, idChidinh,
                        //                                  coChitiet, idDichvu, idChitietchidinh).GetDataSet().Tables[0];
                        //Utility.SetDataSourceForDataGridEx_Basic(grdKetqua, dtKQXN, true, true, "1=1",
                        //                                         "stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");

                        //Utility.focusCell(grdKetqua, KcbKetquaCl.Columns.KetQua);
                    }
                    else //XQ,SA,DT,NS
                    {
                        // mnuNhapKQCDHA.Visible = true;//Mở nếu dùng cho phòng khám
                        if (CKEditorInput)
                        {
                            pnlCKEditor.BringToFront();
                            ShowEditor(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "id_chitietchidinh"), 0));
                        }
                        else
                        {
                            pnlCKEditor.SendToBack();
                            FillDynamicValues();
                        }
                    }
                    Application.DoEvents();
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
            }
        }
        DataTable dtDynamicData = null;
        void FillDynamicValues()
        {
            try
            {
                pnlCKEditor.SendToBack();
                long v_id_chitietchidinh = Utility.Int64Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "id_chitietchidinh"), -1);
                int id_vungks = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "id_vungks"), -1);
                if (v_id_chitietchidinh <= 0) return;

                flowDynamics.SuspendLayout();
                flowDynamics.Controls.Clear();
                dtDynamicData = SPs.HinhanhGetDynamicFieldsValues(id_vungks, v_id_chitietchidinh).GetDataSet().Tables[0];
                if (dtDynamicData.Rows.Count == 0)
                {
                    pnlCKEditor.BringToFront();
                    ShowEditor(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "id_VungKS"), 0));
                    return;
                }
                foreach (DataRow dr in dtDynamicData.Select("1=1", "Stt_hthi"))
                {
                    VNS.UCs.ucAutoCompleteParam _ucp = new VNS.UCs.ucAutoCompleteParam(dr, true);
                    _ucp.txtValue.VisibleDefaultItem = false;
                    _ucp.txtValue.ReadOnly = true;
                    _ucp.IdChidinhchitiet = v_id_chitietchidinh;
                    _ucp.txtValue.RaiseEventEnter = true;
                    //_ucp.lblName.Font = PropertyLib._HinhAnhProperties.DynamicFontChu;
                    //_ucp.txtValue.Font = PropertyLib._HinhAnhProperties.DynamicFontChu;
                    _ucp.TabStop = true;
                    _ucp.txtValue.AllowEmpty = Utility.Int32Dbnull(dr[DynamicField.Columns.AllowEmpty], 0) == 1;
                    _ucp.txtValue.Multiline = Utility.Int32Dbnull(dr[DynamicField.Columns.Multiline], 0) == 1;
                    _ucp.Width = Utility.Int32Dbnull(dr[DynamicField.Columns.W], 0);
                    _ucp.Height = Utility.Int32Dbnull(dr[DynamicField.Columns.H], 0);
                    _ucp.lblName.Width = Utility.Int32Dbnull(dr[DynamicField.Columns.LblW], 0);
                    _ucp.TabIndex = 10 + Utility.Int32Dbnull(dr[DynamicField.Columns.Stt], 0);

                    if (_ucp.Width >= flowDynamics.Width)
                    {
                        _ucp.Width = flowDynamics.Width - PropertyLib._HinhAnhProperties.AutoCompleteMargin;
                    }

                    _ucp.Init();

                    flowDynamics.Controls.Add(_ucp);
                }
            }
            catch (Exception ex)
            {


            }
            finally
            {
                flowDynamics.ResumeLayout(true);
            }
        }
        string MaBenhpham = "";
        string MaChidinh = "";
        int IdChitietdichvu = -1;
        int currRowIdx = -1;
        int id_chidinh = -1;
        int id_dichvu = -1;
        int co_chitiet = -1;
        void ShowKQXN()
        {
            if (!Utility.isValidGrid(grdAssignDetail)) return;
            int tempRowIdx = grdAssignDetail.CurrentRow.RowIndex;
            if (currRowIdx == -1 || currRowIdx != tempRowIdx)
            {
                currRowIdx = tempRowIdx;
                id_dichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdDichvu), 0);
                IdChitietdichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietdichvu), 0);
                co_chitiet = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.CoChitiet), 0);
                id_chidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChidinh), 0);
                MaChidinh = Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaChidinh);
                MaBenhpham = Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaBenhpham);
                HienthiNhapketqua(id_dichvu, co_chitiet);
            }
        }
        void HienthiNhapketqua(int id_dichvu, int co_chitiet)
        {
            try
            {
                // DataTable dt = SPs.ClsTimkiemthongsoXNNhapketqua(ma_luotkham, MaChidinh, MaBenhpham, id_chidinh, co_chitiet, id_dichvu, IdChitietdichvu).GetDataSet().Tables[0];
                DataTable dt = SPs.ClsLayKetquaXn(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, MaChidinh, id_chidinh, 1, objBenhnhan.IdGioitinh).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdKetqua, dt, true, true, "1=1", "stt_hthi_dichvu,stt_hthi_chitiet,stt_in");

                Utility.focusCell(grdKetqua, KcbKetquaCl.Columns.KetQua);
            }
            catch (Exception ex)
            {


            }
        }
        void ShowResult_bak_230930()
        {
            try
            {
                Int16 trangthai_chitiet = Utility.Int16Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.TrangthaiChitiet), 0);
                Int16 CoChitiet = Utility.Int16Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.CoChitiet), 0);

                int IdChitietdichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietdichvu), 0);
                int IdDichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdDichvu), 0);

                int IdChitietchidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietchidinh), 0);
                int IdChidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChidinh), 0);

                string maloai_dichvuCLS = Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaLoaidichvu), "XN");
                string MaChidinh = Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaChidinh), "XN");
                string MaBenhpham = Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaBenhpham), "XN");
                if (trangthai_chitiet < 3)//0=Mới chỉ định;1=Đã chuyển CLS;2=Đang thực hiện;3= Đã có kết quả CLS;4=Đã xác nhận kết quả
                {
                    uiTabKqCls.Width = 0;
                    Application.DoEvents();
                }
                else
                {

                    if (maloai_dichvuCLS == "XN")
                        pnlXN.BringToFront();
                    else
                        pnlXN.SendToBack();
                    if (PropertyLib._ThamKhamProperties.HienthiKetquaCLSTrongluoiChidinh)
                    {
                        if (CoChitiet == 1 || maloai_dichvuCLS != "XN")
                            uiTabKqCls.Width = PropertyLib._ThamKhamProperties.DorongVungKetquaCLS;
                        else
                            uiTabKqCls.Width = 0;
                    }
                    else
                    {
                        uiTabKqCls.Width = PropertyLib._ThamKhamProperties.DorongVungKetquaCLS;
                    }
                    if (CoChitiet == 1)
                        Utility.ShowColumns(grdKetqua, lstKQCochitietColumns);
                    else
                        Utility.ShowColumns(grdKetqua, lstKQKhongchitietColumns);
                    //Lấy dữ liệu CLS
                    if (maloai_dichvuCLS == "XN")
                    {
                        DataTable dt = SPs.ClsTimkiemketquaXNChitiet(objLuotkham.MaLuotkham, MaChidinh, MaBenhpham, IdChidinh, CoChitiet, IdDichvu, IdChitietdichvu).GetDataSet().Tables[0];
                        Utility.SetDataSourceForDataGridEx_Basic(grdKetqua, dt, true, true, "1=1", "stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");

                        Utility.focusCell(grdKetqua, KcbKetquaCl.Columns.KetQua);
                    }
                    else//XQ,SA,DT,NS
                    {
                        FillDynamicValues(IdChitietdichvu, IdChitietchidinh);
                    }
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {


            }
        }
        void FillDynamicValues(int IdDichvuChitiet, int idchidinhchitiet)
        {
            //try
            //{
            //    pnlDynamicValues.Controls.Clear();

            //    DataTable dtData = clsHinhanh.GetDynamicFieldsValues(IdDichvuChitiet, txtMauKQ.myCode, "", "", -1, idchidinhchitiet);

            //    foreach (DataRow dr in dtData.Select("1=1", "Stt_hthi"))
            //    {
            //        dr[DynamicValue.Columns.IdChidinhchitiet] = Utility.Int32Dbnull(idchidinhchitiet);
            //        ucDynamicParam _ucTextSysparam = new ucDynamicParam(dr, true);
            //        _ucTextSysparam._ReadOnly = true;
            //        _ucTextSysparam.TabStop = true;
            //        _ucTextSysparam.TabIndex = 10 + Utility.Int32Dbnull(dr[DynamicField.Columns.Stt], 0);
            //        _ucTextSysparam.Init();
            //        _ucTextSysparam.Size = PropertyLib._DynamicInputProperties.DynamicSize;
            //        _ucTextSysparam.txtValue.Size = PropertyLib._DynamicInputProperties.TextSize;
            //        _ucTextSysparam.lblName.Size = PropertyLib._DynamicInputProperties.LabelSize;
            //        pnlDynamicValues.Controls.Add(_ucTextSysparam);
            //    }
            //}
            //catch (Exception ex)
            //{


            //}
        }
        private void txtPatient_Code_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                
                if (e.KeyCode == Keys.Enter)
                {
                    if (Utility.Int32Dbnull(cboKhoanoitru.SelectedValue, -1) < 0)
                    {
                        Utility.ShowMsg("Bạn cần chọn khoa nội trú trước khi chọn Bệnh nhân để lập phiếu điều trị");
                        cboKhoanoitru.Focus();
                        return;
                    }
                    string patientCode = Utility.AutoFullPatientCode(txtPatient_Code.Text);
                    ClearControl();
                    

                    DataTable dtPatient = _KCB_THAMKHAM.TimkiemBenhnhan(txtPatient_Code.Text,
                        -1,(byte)1, 0);
                   
                    DataRow[] arrPatients = dtPatient.Select(KcbLuotkham.Columns.MaLuotkham + "='" + patientCode + "'");
                    if (arrPatients.GetLength(0) <= 0)
                    {
                        if (dtPatient.Rows.Count > 1)
                        {
                            //frm_TimkiemBenhnhanNoitru _TimkiemBenhnhanNoitru = new frm_TimkiemBenhnhanNoitru("ALL", 1);
                            //if (_TimkiemBenhnhanNoitru.ShowDialog() == DialogResult.OK)
                            //{
                            //    txtPatient_Code.Text = _TimkiemBenhnhanNoitru.MaLuotkham;
                            //    txtPatient_Code_KeyDown(txtPatient_Code, new KeyEventArgs(Keys.Enter));
                            //}

                            var frm = new frm_DSACH_BN_TKIEM("ALL", 1);
                            frm.MaLuotkham = txtPatient_Code.Text;
                            frm.dtPatient = dtPatient;
                            frm.ShowDialog();
                            if (!frm.has_Cancel)
                            {
                                txtPatient_Code.Text = frm.MaLuotkham;
                            }
                        }
                    }
                    else
                    {
                        txtPatient_Code.Text = patientCode;
                    }
                    byte trang_thai = Utility.ByteDbnull(cboTrangthai.SelectedValue, 100);
                    txtMaluotkham.Text = patientCode;
                    m_dtPatients = _KCB_THAMKHAM.NoitruTimkiembenhnhan("01/01/1900", "01/01/1900", "",-1, Utility.DoTrim(txtMaluotkham.Text),
                                                         -1,
                                                         -1, chkChuyenkhoa.Checked ? 1 : 0, globalVariables.gv_intIDNhanvien, trang_thai, thamso);

                    if (!m_dtPatients.Columns.Contains("MAUSAC"))
                        m_dtPatients.Columns.Add("MAUSAC", typeof(int));

                    Utility.SetDataSourceForDataGridEx_Basic(grdList, m_dtPatients, true, true, "", KcbDanhsachBenhnhan.Columns.TenBenhnhan); //"locked=0", "");

                    if (m_dtPatients.Rows.Count > 0)
                    {
                        AllowTextChanged = false;
                        if (dt_ICD_PHU != null) dt_ICD_PHU.Rows.Clear();
                        GetData();
                        txtPatient_Code.SelectAll();
                    }
                    else
                    {
                        string sPatientTemp = txtPatient_Code.Text;
                        ClearControl();
                        ModifyCommmands();
                        txtPatient_Code.Text = sPatientTemp;
                        txtPatient_Code.SelectAll();
                    }
                    txtMach.SelectAll();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin bệnh nhân");
            }
            finally
            {
                AllowTextChanged = true;
            }
        }
        private void AddMaBenh(string MaBenh, string TenBenh)
        {
            //DataRow[] arrDr = dt_ICD_PHU.Select(string.Format("MA_ICD='{0}'", MaBenh));
            EnumerableRowCollection<DataRow> query = from benh in dt_ICD_PHU.AsEnumerable()
                                                     where Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == MaBenh
                                                     select benh;
            if (!query.Any())
            {
                DataRow drv = dt_ICD_PHU.NewRow();
                drv[DmucBenh.Columns.MaBenh] = MaBenh;
                EnumerableRowCollection<string> query1 = from benh in globalVariables.gv_dtDmucBenh.AsEnumerable()
                                                         where
                                                             Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) ==
                                                             MaBenh
                                                         select Utility.sDbnull(benh[DmucBenh.Columns.TenBenh]);
                if (query1.Any())
                {
                    drv[DmucBenh.Columns.TenBenh] = Utility.sDbnull(query1.FirstOrDefault());
                }

                dt_ICD_PHU.Rows.Add(drv);
                dt_ICD_PHU.AcceptChanges();
                grd_ICD.AutoSizeColumns();
            }
        }

      
        private void grd_ICD_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "XOA")
                {
                    grd_ICD.CurrentRow.Delete();
                    dt_ICD_PHU.AcceptChanges();
                    grd_ICD.Refetch();
                    grd_ICD.AutoSizeColumns();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình xóa thông tin Mã ICD");
                throw;
            }
            finally
            {
                setChanDoan();
            }
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

        private DataTable getChitietCLS()
        {
            try
            {
                var sub_dtData = new DataTable("Temp");
                sub_dtData.Columns.AddRange(new[]
                                                {
                                                    new DataColumn("LOAI_CLS", typeof (string)),
                                                    new DataColumn("KETQUA", typeof (string)),
                                                    new DataColumn("STT", typeof (string))
                                                });

                DataTable temdt = SPs.KcbThamkhamLayketquacls(malankham).GetDataSet().Tables[0];
                if (!temdt.Columns.Contains("id_dichvu_temp"))
                    temdt.Columns.Add(new DataColumn("id_dichvu_temp", typeof (string)));
                var lstid_dichvu = new List<string>();
                foreach (DataRow dr in temdt.Rows)
                {
                    string service_ID = Utility.sDbnull(dr[ "id_dichvu"], "");
                    if (service_ID.Trim() == "") service_ID = "0";
                    dr["id_dichvu_temp"] = service_ID;
                    if (!lstid_dichvu.Contains(service_ID)) lstid_dichvu.Add(service_ID);
                }
                string reval = "";
                string NhomCLS = "";
                int STT = 0;
                foreach (string service_ID in lstid_dichvu)
                {
                    STT++;
                    DataRow[] arrChitiet = temdt.Select("id_dichvu_temp='" + service_ID + "'");
                    NhomCLS = "";
                    string kq = "";
                    foreach (DataRow drchitiet in arrChitiet)
                    {
                        if (NhomCLS == "") NhomCLS = Utility.sDbnull(drchitiet["ten_dichvu"], "");
                        kq += Utility.sDbnull(drchitiet["Ten_KQ"], "") + ":" + Utility.sDbnull(drchitiet["Ket_Qua"], "") +
                              " ; ";
                    }
                    if (kq.Length > 0) kq = kq.Substring(0, kq.Length - 2);
                    DataRow newDR = sub_dtData.NewRow();

                    if (service_ID == "0") NhomCLS = "#";
                    newDR["STT"] = STT.ToString();
                    newDR["LOAI_CLS"] = NhomCLS;
                    newDR["KETQUA"] = kq;
                    sub_dtData.Rows.Add(newDR);
                }
                return sub_dtData;
            }
            catch
            {
                return new DataTable();
            }
        }
        public static ReportDocument GetReport(string fileName)//, ref string ErrMsg)
        {
            try
            {
                ReportDocument crpt = new ReportDocument();
                if (File.Exists(fileName))
                {
                    crpt.Load(fileName);
                }
                else
                {
                    return null;
                }
                return crpt;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi nạp báo cáo " + fileName + "-->\n" + ex.Message);
                //ErrMsg = ex.Message;
                return null;
            }
        }
       

      
        private void mnuDelDrug_Click(object sender, EventArgs e)
        {
            cmdDeletePres.PerformClick();
            //if (!IsValidThuoc_delete()) return;
            //PerformActionDeleteSelectedDrug();
            //ModifyCommmands();
        }

        private void mnuDeleteCLS_Click(object sender, EventArgs e)
        {
            if (!InValiSelectedCLS()) return;
            PerforActionDeleteSelectedCLS();
            ModifyCommmands();
        }

        private void cboLaserPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveDefaultPrinter();
        }

        private void LoadLaserPrinters()
        {
            if (!string.IsNullOrEmpty(PropertyLib._MayInProperties.TenMayInBienlai))
            {
                PropertyLib._MayInProperties.TenMayInBienlai = Utility.GetDefaultPrinter();
                m_strDefaultLazerPrinterName = Utility.sDbnull(PropertyLib._MayInProperties.TenMayInBienlai);
            }
            if (PropertyLib._ThamKhamProperties != null)
            {
                try
                {
                    //khoi tao may in
                    String pkInstalledPrinters;
                    cboLaserPrinters.Items.Clear();
                    for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                    {
                        pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                        cboLaserPrinters.Items.Add(pkInstalledPrinters);
                    }
                }
                catch
                {
                }
                finally
                {
                    m_strDefaultLazerPrinterName = Utility.sDbnull(PropertyLib._MayInProperties.TenMayInBienlai);

                    cboLaserPrinters.Text = m_strDefaultLazerPrinterName;
                }
            }
        }

        private void SaveDefaultPrinter()
        {
            try
            {
                PropertyLib._MayInProperties.TenMayInBienlai = Utility.sDbnull(cboLaserPrinters.Text);
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lưu trạng thái-->" + ex.Message);
            }
        }

        private void Try2CreateFolder()
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(strSaveandprintPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(strSaveandprintPath));
            }
            catch
            {
            }
        }

     

        private void cmdBenhAnNgoaiTru_Click(object sender, EventArgs e)
        {
           
        }

       

       

        private void cmdHistory_Click_1(object sender, EventArgs e)
        {
        }

        private void GetChanDoanChinhPhu(string ICD_chinh, string IDC_Phu, ref string ICD_chinh_Name,
                                         ref string ICD_chinh_Code, ref string ICD_Phu_Name, ref string ICD_Phu_Code)
        {
            try
            {
                List<string> lstICD = ICD_chinh.Split(',').ToList();
                DmucBenhCollection _list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _item in _list)
                {
                    ICD_chinh_Name += _item.TenBenh + ";";
                    ICD_chinh_Code += _item.MaBenh + ";";
                }
                lstICD = IDC_Phu.Split(',').ToList();
                _list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _item in _list)
                {
                    ICD_Phu_Name += _item.TenBenh + ";";
                    ICD_Phu_Code += _item.MaBenh + ";";
                }
            }
            catch
            {
            }
        }

       
        private void txtNhietDo_Click(object sender, EventArgs e)
        {
        }

       

        #region "chỉ định cận lâm sàng"

       
        private DataTable m_dtReportAssignInfo;

        /// <summary>
        /// hàm thực hiện việc update thông tin của cell update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdAssignDetail_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            //try
            //{
            //    if (e.Column.Key == "Ghi_Chu")
            //    {

            //        new Update(KcbChidinhclsChitiet.Schema)
            //            .Set(KcbChidinhclsChitiet.Columns.NgaySua).EqualTo(globalVariables.SysDate)
            //            .Set(KcbChidinhclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
            //            .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(
            //                Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChitietchidinh))).Execute
            //            ();
            //        grdAssignDetail.CurrentRow.BeginEdit();
            //        grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NgaySua].Value = globalVariables.SysDate;
            //        grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NguoiSua].Value = globalVariables.UserName;
            //        grdAssignDetail.CurrentRow.EndEdit();
            //    }
            //}
            //catch (Exception exception)
            //{
            //}
        }

        /// <summary>
        /// hàm thực hiện việc xóa thông tin chỉ định cận lâm sàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDelteAssign_Click(object sender, EventArgs e)
        {
            if (CheckDachuyenkhoa()) return;
            if (!IsValidChidinhCLS()) return;
            PerforActionDeleteAssign();
            ModifyCommmands();
        }

        private bool IsValidChidinhCLS()
        {
            bool b_Cancel = false;
            if (grdAssignDetail.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện xóa chỉ định CLS", "Thông báo",
                                MessageBoxIcon.Warning);
                grdAssignDetail.Focus();
                return false;
            }
            if (grdAssignDetail.GetCheckedRows().Count() <= 0 && RowCLS != null)//Cho chắc ăn
            {
                try
                {
                    RowCLS.BeginEdit();
                    RowCLS.IsChecked = true;
                    RowCLS.EndEdit();
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất 1 chi tiết cận lâm sàng để xóa");
                    return false;
                }

            }
            foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int AssignDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                                          -1);
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                    .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                    break;
                }
            }
            if (b_Cancel)
            {
                Utility.ShowMsg("Bạn chỉ có thể xóa những chỉ định chưa thanh toán !", "Thông báo",
                                MessageBoxIcon.Warning);
                return false;
            }
            foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int AssignDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                                          -1);
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                    .And(KcbChidinhclsChitiet.Columns.TrangThai).IsGreaterThanOrEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                    break;
                }
            }
            if (b_Cancel)
            {
                Utility.ShowMsg("Chỉ định bạn chọn đã được chuyển làm cận lâm sàng hoặc đã có kết quả nên không thể xóa. Đề nghị kiểm tra lại");
                return false;
            }
            return true;
        }
        private bool IsValidGoidichvu()
        {
            bool b_Cancel = false;
            if (grdGoidichvu.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện xóa gói dịch vụ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdGoidichvu.Focus();
                return false;
            }

            foreach (GridEXRow gridExRow in grdGoidichvu.GetCheckedRows())
            {
                int AssignDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                                          -1);
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                    .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                    break;
                }
            }
            if (b_Cancel)
            {
                Utility.ShowMsg("Gói dịch vụ bạn chọn đã thanh toán nên không thể xóa. Mời bạn chọn lại !", "Thông báo",
                                MessageBoxIcon.Warning);
                return false;
            }
            
            return true;
        }
        /// <summary>
        /// hàm thực hiện viễ xóa thông tin chỉ định
        /// </summary>
        private void PerforActionDeleteAssign()
        {
            foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int AssignDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                                          -1);
                int id_chidinh = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value,
                                                    -1);
                _KCB_CHIDINH_CANLAMSANG.XoaChiDinhCLSChitiet(AssignDetail_ID);
                gridExRow.Delete();
                m_dtAssignDetail.AcceptChanges();
            }
        }
        private void XoaGoidichvu()
        {
            foreach (GridEXRow gridExRow in grdGoidichvu.GetCheckedRows())
            {
                int AssignDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                                          -1);
                int id_chidinh = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value,
                                                    -1);
                _KCB_CHIDINH_CANLAMSANG.GoidichvuXoachitiet(AssignDetail_ID);
                gridExRow.Delete();
                m_dtGoidichvu.AcceptChanges();
            }
        }
        private void PerforActionDeleteSelectedCLS()
        {
            try
            {
                int AssignDetail_ID =
                    Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                        -1);
                int id_chidinh =
                    Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value,
                                        -1);
                _KCB_CHIDINH_CANLAMSANG.XoaChiDinhCLSChitiet(AssignDetail_ID);
                grdAssignDetail.CurrentRow.Delete();
                m_dtAssignDetail.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Bạn nên xóa CLS bằng cách chọn và nhấn nút Xóa CLS-->" + ex.Message);
            }
        }

        private bool InValiUpdateChiDinh()
        {
            if (grdAssignDetail.RowCount <= 0) return false;
            if (RowCLS == null)
            {
                Utility.ShowMsg("Vui lòng chọn 1 dòng chỉ định trong đơn để thực hiện cập nhật phiếu chỉ định CLS");
                return false;
            }
            int id_chidinh = Utility.Int32Dbnull(Utility.getCellValuefromGridEXRow(RowCLS, KcbChidinhclsChitiet.Columns.IdChidinh), -1);
            SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                .Where(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(id_chidinh)
                .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsGreaterThanOrEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Phiếu này đã thanh toán, Mời bạn thêm phiếu mới để thực hiện", "Thông báo");
                cmdInsertAssign.Focus();
                return false;
            }

            SqlQuery sqlQueryKq = new Select().From(KcbChidinhclsChitiet.Schema)
                .Where(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(id_chidinh)
                .And(KcbChidinhclsChitiet.Columns.TrangThai).IsGreaterThanOrEqualTo(2); 

            if (sqlQueryKq.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Phiếu này đã có kết quả, Mời bạn Thêm phiếu mới để thực hiện", "Thông báo");
                cmdInsertAssign.Focus();
                return false;
            }


            return true;
        }

        /// <summary>
        /// hàm thực hiện viêc jsu thôn gtin chỉ dịnh cận lâm sàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckDachuyenkhoa()) return;
                if (!CheckPatientSelected()) return;
                if (!InValiUpdateChiDinh()) return;
                frm_KCB_CHIDINH_CLS frm = new frm_KCB_CHIDINH_CLS("-GOI,-TIEN,-CHIPHITHEM", 0);
                frm.noitru = 1;
                frm.objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtIdPhieudieutri.Text));
                frm.Exam_ID = -1;
                frm.objLuotkham = objLuotkham;// CreatePatientExam();
                frm.objBenhnhan = objBenhnhan;
                frm.m_eAction = action.Update;
                frm.txtAssign_ID.Text = Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), "-1");
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    LaythongtinPhieudieutri();
                    TinhtoanTongchiphi();
                    ModifyCommmands();
                }
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg("Lỗi trong quá trình sửa phiếu :" + e);
                }
                //throw;
            }
        }
        bool CheckPatientSelected()
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn phải chọn Bệnh nhân trước khi thực hiện các công việc chỉ định Thăm khám, CLS, Kê đơn");
                return false;
            }
            if (objPhieudieutri == null)
            {
                Utility.ShowMsg("Bạn phải chọn Phiếu chỉ định trước khi thực hiện các công việc chỉ định Thăm khám, CLS, Kê đơn");
                return false;
            }
            return true;
        }
        /// <summary>
        /// hàm thực hiện việc thêm mới thông itn 
        /// của phần chính định
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInsertAssign_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckDachuyenkhoa()) return;
                if (!CheckPatientSelected()) return;
                if (!cmdInsertAssign.Enabled) return;
                frm_KCB_CHIDINH_CLS frm = new frm_KCB_CHIDINH_CLS("-GOI,-TIEN,-CHIPHITHEM", 0);
                frm.txtAssign_ID.Text = "-100";
                frm.Exam_ID =-1;
                frm.objLuotkham = objLuotkham;// CreatePatientExam();
                frm.objBenhnhan = objBenhnhan;
                frm.objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtIdPhieudieutri.Text));
                frm.m_eAction = action.Insert;
                frm.txtAssign_ID.Text = "-1";
                frm.noitru = 1;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    
                    LaythongtinPhieudieutri();
                    TinhtoanTongchiphi();
                    //if (PropertyLib._ThamKhamProperties.TudongthugonCLS)
                    //    grdAssignDetail.GroupMode = GroupMode.Collapsed;
                    Utility.GotoNewRowJanus(grdAssignDetail, KcbChidinhclsChitiet.Columns.IdChidinh, frm.txtAssign_ID.Text);
                    ModifyCommmands();
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                txtPatient_Code.Focus();
                txtPatient_Code.SelectAll();
            }
        }

        /// <summary>
        /// hàm thực hiện việc in phiếu chỉ định cận lâm sàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPrintAssign_Click(object sender, EventArgs e)
        {
            try
            {
                var actionResult = ActionResult.Error;
                string mayin = "";
                int vAssignId = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh),
                                                     -1);
                string vAssignCode = Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhcl.Columns.MaChidinh), -1);
                var nhomcls = new List<string>();
                foreach (GridEXRow gridExRow in grdAssignDetail.GetDataRows())
                {
                    if (Utility.Int64Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value) == vAssignId)
                        if (!nhomcls.Contains(Utility.sDbnull(gridExRow.Cells["nhom_in_cls"].Value)))
                        {
                            nhomcls.Add(Utility.sDbnull(gridExRow.Cells["nhom_in_cls"].Value));
                        }
                }
                string nhomincls = "ALL";
                if (cboServicePrint.SelectedIndex > 0)
                {
                    nhomincls = Utility.sDbnull(cboServicePrint.SelectedValue, "ALL");
                    switch (cboServicePrint.SelectedIndex)
                    {
                        case 1:
                            break;
                    }
                }
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THAMKHAM_INTACHTOANBO_CLS", "0", false) == "1" &&
                    chkIntach.Checked && cboServicePrint.SelectedIndex <= 0)
                {
                    actionResult = KcbInphieu.NoitruInTachToanBoPhieuCls((int)objLuotkham.IdBenhnhan,
                                                                     objLuotkham.MaLuotkham, vAssignId,
                                                                     vAssignCode, nhomcls,
                                                                     cboServicePrint.SelectedIndex, chkIntach.Checked, dtpPrintDate.Value,
                                                                     ref mayin);
                }
                else
                {
                    actionResult = KcbInphieu.NoitruInphieuChidinhCls((int)objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham,
                                                                  vAssignId,
                                                                  vAssignCode, nhomincls, cboServicePrint.SelectedIndex,
                                                                  chkIntach.Checked,
                                                                  ref mayin);
                }
                //if (actionResult == ActionResult.Success)
                //{
                //    if (PropertyLib._ThamKhamProperties.Chophepchuyencansaukhiinphieu)
                //    {
                //        if (cmdConfirm.Tag.ToString() == "1") cmdConfirm.PerformClick();
                //    }
                //}
                if (mayin != "") cboLaserPrinters.Text = mayin;
                //string mayin = "";
                //int v_AssignId = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh),
                //    -1);
                //string v_AssignCode = Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhcl.Columns.MaChidinh), -1);
                //string nhomincls = "ALL";
                //if (cboServicePrint.SelectedIndex > 0)
                //{
                //    nhomincls = Utility.sDbnull(cboServicePrint.SelectedValue, "ALL");
                //}
                //KcbInphieu.InphieuChidinhCls((int) objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, v_AssignId,
                //    v_AssignCode, nhomincls, cboServicePrint.SelectedIndex, chkIntach.Checked, ref mayin);
                //if (mayin != "") cboLaserPrinters.Text = mayin;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

      

        private decimal GetTotalDatatable(DataTable dataTable, string FiledName, string Filer)
        {
            return Utility.DecimaltoDbnull(dataTable.Compute("SUM(" + FiledName + ")", Filer), 0);
        }
        private void GetPatienInfoAddreport(ref DataTable dtReportPhieuXetNghiem)
        {
            // Kiểm tra và cộng cột
            // Kiểm tra và cộng cột
            if (!dtReportPhieuXetNghiem.Columns.Contains(KcbDanhsachBenhnhan.Columns.DiaChi))
                dtReportPhieuXetNghiem.Columns.Add(KcbDanhsachBenhnhan.Columns.DiaChi, typeof(string));
            if (!dtReportPhieuXetNghiem.Columns.Contains(KcbDanhsachBenhnhan.Columns.NamSinh))
                dtReportPhieuXetNghiem.Columns.Add(KcbDanhsachBenhnhan.Columns.NamSinh, typeof (int));


            if (!dtReportPhieuXetNghiem.Columns.Contains(KcbDanhsachBenhnhan.Columns.TenBenhnhan))
                dtReportPhieuXetNghiem.Columns.Add(KcbDanhsachBenhnhan.Columns.TenBenhnhan, typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains(KcbDanhsachBenhnhan.Columns.GioiTinh))
                dtReportPhieuXetNghiem.Columns.Add(KcbDanhsachBenhnhan.Columns.GioiTinh, typeof(string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("Tuoi"))
                dtReportPhieuXetNghiem.Columns.Add("Tuoi", typeof(int));
            if (!dtReportPhieuXetNghiem.Columns.Contains(KcbDanhsachBenhnhan.Columns.GioiTinh))
                dtReportPhieuXetNghiem.Columns.Add(KcbDanhsachBenhnhan.Columns.GioiTinh, typeof(string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("Rank_Name"))
                dtReportPhieuXetNghiem.Columns.Add("Rank_Name", typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("Position_Name"))
                dtReportPhieuXetNghiem.Columns.Add("Position_Name", typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("Unit_Name"))
                dtReportPhieuXetNghiem.Columns.Add("Unit_Name", typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("chan_doan"))
                dtReportPhieuXetNghiem.Columns.Add("chan_doan", typeof(string));
            if (!dtReportPhieuXetNghiem.Columns.Contains(DmucDoituongkcb.Columns.TenDoituongKcb))
                dtReportPhieuXetNghiem.Columns.Add(DmucDoituongkcb.Columns.TenDoituongKcb, typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("mathe_bhyt"))
                dtReportPhieuXetNghiem.Columns.Add("mathe_bhyt", typeof(string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("Barcode"))
                dtReportPhieuXetNghiem.Columns.Add("Barcode", typeof (byte[]));
            byte[] byteArray = null;
            Utility.CreateBarcodeData(ref dtReportPhieuXetNghiem, malankham, ref byteArray);
            foreach (DataRow dr in dtReportPhieuXetNghiem.Rows)
            {
                dr["Barcode"] = byteArray;
                dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan] = txtPatient_Name.Text;

                dr[KcbLuotkham.Columns.DiaChi] = txtDiaChi.Text;
                dr[KcbDanhsachBenhnhan.Columns.NamSinh] = objBenhnhan.NgaySinh.Value.Year.ToString();

                dr["Tuoi"] = globalVariables.SysDate.Year - objBenhnhan.NgaySinh.Value.Year;
                dr["gioi_tinh"] = txtGioitinh.Text;
                dr["gioi_tinh"] = txtGioitinh.Text;
                // dr["Rank_Name"] = txtRank.Text;
                //dr["Position_Name"] = txtPosition.Text;
                //dr["Unit_Name"] = unitName;
                dr["chan_doan"] = txtChanDoan.Text;
                dr[DmucDoituongkcb.Columns.TenDoituongKcb] = txtObjectType_Name.Text;
                dr["mathe_bhyt"] = txtSoBHYT.Text;
            }
            dtReportPhieuXetNghiem.AcceptChanges();
        }

        #endregion

        #region "khởi tạo các sụ kienj thông tin của thuốc"

        private bool ExistsDonThuoc()
        {
            try
            {
                string _kenhieudon = THU_VIEN_CHUNG.Laygiatrithamsohethong("KE_NHIEU_DON", "N", true);
                KcbDonthuocCollection lstPres =
                    new Select()
                        .From(KcbDonthuoc.Schema)
                        .Where(KcbDonthuoc.MaLuotkhamColumn).IsEqualTo(Utility.sDbnull(objLuotkham.MaLuotkham)).
                        ExecuteAsCollection<KcbDonthuocCollection>();

                var lstPres1 = from p in lstPres
                               where p.IdPhieudieutri == objPhieudieutri.IdPhieudieutri
                               select p;
                if (objLuotkham.MaDoituongKcb == "BHYT")
                {
                    if (_kenhieudon == "Y" && lstPres1.Count() <= 0)//Được phép kê mỗi phòng khám 1 đơn thuốc
                        return false;
                    if (_kenhieudon == "N" && lstPres.Count > 0 && lstPres1.Count() <= 0)//Cảnh báo ko được phép kê đơn tiếp
                    {
                        Utility.ShowMsg("Chú ý: Bệnh nhân này thuộc đối tượng BHYT và đã được kê đơn thuốc tại phòng khám khác. Bạn cần trao đổi với bộ phận khác để được cấu hình kê đơn thuốc tại nhiều phòng khác khác nhau với đối tượng BHYT này", "Thông báo");
                        return false;
                    }
                }
                else//Bệnh nhân dịch vụ-->cho phép kê 1 đơn nếu đơn chưa thanh toán và nhiều đơn nếu các đơn trước đã thanh toán
                {
                    if (lstPres1.Count() > 0)
                        if (lstPres1.FirstOrDefault().TrangthaiThanhtoan == 0)//Chưa thanh toán-->Cần sửa đơn
                            return true;
                        else//Đã thanh toán-->Cho phép thêm đơn mới
                            return false;
                    return false;
                }
                return lstPres.Count > 0;
                //Tạm thời rem lại do vẫn có BN kê được >1 đơn thuốc
                //var query = from thuoc in grdPresDetail.GetDataRows().AsEnumerable()
                //                    select thuoc;
                //if (query.Any()) return true;
                //else return false;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi kiểm tra số lượng đơn thuốc của lần khám\n" + ex.Message);
                return false;
            }
        }
        
        private void cmdCreateNewPres_Click(object sender, EventArgs e)
        {
            if (CheckDachuyenkhoa()) return;
            if (!CheckPatientSelected()) return;
            if (!cmdCreateNewPres.Enabled) return;
            if (objLuotkham.TrangthaiNoitru>=1 ||(objLuotkham.TrangthaiNoitru==0 && !ExistsDonThuoc()))
            {
                ThemMoiDonThuoc();
            }
            else
            {
                UpdateDonThuoc();
            }
        }

        private void ThemMoiDonThuoc()
        {
            try
            {
                // KeDonThuocTheoDoiTuong();
                frm_KCB_KE_DONTHUOC frm = new frm_KCB_KE_DONTHUOC("THUOC");
                frm.em_Action = action.Insert;
                frm.id_khoa = Utility.Int16Dbnull(cboKhoanoitru.SelectedValue, -1);
                frm.objLuotkham = CreatePatientExam();
                frm._KcbCDKL = _KcbChandoanKetluan;
                frm._MabenhChinh = txtMaBenhChinh.MyCode;
                frm._Chandoan = txtChanDoan.Text;
                frm.DtIcd = globalVariables.gv_dtDmucBenh;
                if (objPhieudieutri != null)
                    frm.forced2Add =Utility.Byte2Bool( objPhieudieutri.TthaiBosung);
                frm.dt_ICD_PHU = dt_ICD_PHU;
                frm.objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtIdPhieudieutri.Text));
                frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                frm.id_kham = -1;
                frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                frm.txtPatientID.Text = Utility.sDbnull(objBenhnhan.IdBenhnhan, "-1");
                frm.txtSoDT.Text = Utility.sDbnull(objBenhnhan.DienThoai, "");
                frm.txtPatientName.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan, "");
                frm.txtYearBirth.Text = Utility.sDbnull(objBenhnhan.NamSinh, "");
                frm.txtSex.Text = Utility.sDbnull(objBenhnhan.GioiTinh, "");
                frm.txtSDT.Text = objLuotkham.Sdt;
                frm.txtPres_ID.Text = "-1";
                frm.noitru = 1;
                frm.dtNgayKhamLai.MinDate = DateTime.Now;
                frm._ngayhenkhamlai = "";
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    txtMaBenhChinh.SetCode(frm._MabenhChinh);
                    txtChanDoan._Text = frm._Chandoan;
                    dt_ICD_PHU = frm.dt_ICD_PHU;
                    if (frm._KcbCDKL != null) _KcbChandoanKetluan = frm._KcbCDKL;
                    LaythongtinPhieudieutri();
                    TinhtoanTongchiphi();
                    Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuoc.Columns.IdDonthuoc,
                                            Utility.sDbnull(frm.txtPres_ID.Text));
                    ModifyCommmands();
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
                
                txtPatient_Code.Focus();
                txtPatient_Code.SelectAll();
            }
        }
        private void ThemMoiDonThuoc_ravien()
        {
            try
            {
                // KeDonThuocTheoDoiTuong();
                frm_KCB_KE_DONTHUOC frm = new frm_KCB_KE_DONTHUOC("THUOC");
                frm.em_Action = action.Insert;
                frm.id_khoa = Utility.Int16Dbnull(cboKhoanoitru.SelectedValue, -1);
                frm.KieuDonthuoc = 3;
                frm.objLuotkham = CreatePatientExam();
                frm._KcbCDKL = _KcbChandoanKetluan;
                frm._MabenhChinh = txtMaBenhChinh.MyCode;
                frm._Chandoan = txtChanDoan.Text;
                frm.DtIcd = globalVariables.gv_dtDmucBenh;
                if (objPhieudieutri != null)
                    frm.forced2Add = Utility.Byte2Bool(objPhieudieutri.TthaiBosung);
                frm.dt_ICD_PHU = dt_ICD_PHU;
                frm.objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtIdPhieudieutri.Text));
                frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                frm.id_kham = -1;
                frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                frm.txtPatientID.Text = Utility.sDbnull(objBenhnhan.IdBenhnhan, "-1");
                frm.txtSoDT.Text = Utility.sDbnull(objBenhnhan.DienThoai, "");
                frm.txtPatientName.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan, "");
                frm.txtYearBirth.Text = Utility.sDbnull(objBenhnhan.NamSinh, "");
                frm.txtSex.Text = Utility.sDbnull(objBenhnhan.GioiTinh, "");
                frm.txtSDT.Text = objLuotkham.Sdt;
                frm.txtPres_ID.Text = "-1";
                frm.noitru = 1;
                frm.dtNgayKhamLai.MinDate = DateTime.Now;
                frm._ngayhenkhamlai = "";
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    txtMaBenhChinh.SetCode(frm._MabenhChinh);
                    txtChanDoan._Text = frm._Chandoan;
                    dt_ICD_PHU = frm.dt_ICD_PHU;
                    if (frm._KcbCDKL != null) _KcbChandoanKetluan = frm._KcbCDKL;
                    LaythongtinPhieudieutri();
                    TinhtoanTongchiphi();
                    Utility.GotoNewRowJanus(grdDonthuocravien, KcbDonthuoc.Columns.IdDonthuoc,
                                            Utility.sDbnull(frm.txtPres_ID.Text));
                    ModifyCommmands();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {

                txtPatient_Code.Focus();
                txtPatient_Code.SelectAll();
            }
        }
        private void setChanDoan()
        {
            return;
            //string _value = txtTenBenhChinh.Text.Trim() + ";";
            //try
            //{
            //    foreach (DataRow dr in dt_ICD_PHU.Rows)
            //        _value += dr[DmucBenh.Columns.TenBenh].ToString() + ";";
            //    txtChanDoan.Text = _value.Substring(0, _value.Length - 1);
            //}
            //catch
            //{
            //}
        }

        /// <summary>
        /// hàm thực hiện việc kê đơnt huốc theo đối tượng
        /// </summary>
        private void KeDonThuocTheoDoiTuong()
        {
            //KcbLuotkham objPatientExam = CreatePatientExam();
            //if (objPatientExam != null)
            //{
            //    var frm = new frm_DM_CreateNewPres_V2();
            //    frm.Text = string.Format("Kê đơn thuốc  ngoại trú  theo đối tượng -{0}-{1}-{2}", txtTenBN.Text,
            //                             Utility.sDbnull(radNam.Checked ? "Nam" : "Nữ"),
            //                             Utility.sDbnull(txtYear_Of_Birth.Text));
            //    frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
            //    frm.em_CallAction = CallAction.FromMenu;
            //    frm.MaDoituongKcb = Utility.sDbnull(objPatientExam.MaDoituongKcb, "");
            //    frm.objPatientExam = CreatePatientExam();
            //    frm.Exam_ID = Utility.Int32Dbnull(txtExam_ID.Text, -1);
            //    frm.em_CallAction = CallAction.FromMenu;
            //    frm.radTheoDoiTuong.Checked = true;
            //    frm.radTrongGoi.Visible = false;
            //    frm.txtPres_ID.Text = "-1";
            //    frm.em_Action = action.Insert;
            //    frm.TrangThai = 0;
            //    frm.PreType = 0;
            //    frm.ShowDialog();
            //    if (frm.b_Cancel)
            //    {
            //        LaythongtinPhieudieutri();
            //        Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuocChitiet.Columns.IdDonthuoc,
            //                                Utility.sDbnull(frm.txtPres_ID.Text));
            //        ModifyCommmands();
            //    }
            //}
        }
        GridEXRow RowThuocRavien = null;
        GridEXRow RowThuoc = null;
        GridEXRow RowCLS = null;
        GridEXRow RowVTTH = null;
        /// <summary>
        /// ham thực hiện việc update thông tin của thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdUpdatePres_Click(object sender, EventArgs e)
        {
            if (CheckDachuyenkhoa()) return;
            if (!CheckPatientSelected()) return;
            if (!cmdUpdatePres.Enabled) return;
            if (RowThuoc == null)
            {
                Utility.ShowMsg("Vui lòng chọn 1 dòng thuốc trong đơn để thực hiện cập nhật đơn thuốc");
                return;
            }
            if (Utility.Coquyen("quyen_suadonthuoc") || Utility.sDbnull(Utility.getCellValuefromGridEXRow(RowThuoc, KcbDonthuocChitiet.Columns.NguoiTao)) == globalVariables.UserName)
            {
                UpdateDonThuoc();
            }
            else
            {
                Utility.ShowMsg("Đơn thuốc đang chọn sửa được tạo bởi bác sĩ khác hoặc bạn không được gán quyền sửa(quyen_suadonthuoc). Vui lòng kiểm tra lại");
                return;
            }
        }

        private bool IsValid_UpdateDonthuoc(int pres_id,string thuoc_vt)
        {
            TPhieuCapphatChitiet _capphat = new Select().From(TPhieuCapphatChitiet.Schema).Where(TPhieuCapphatChitiet.Columns.IdDonthuoc).IsEqualTo(pres_id)
                .ExecuteSingle<TPhieuCapphatChitiet>();
            if (_capphat != null)
            {
                Utility.ShowMsg("Đơn " + thuoc_vt + " đã được tổng hợp lĩnh " + thuoc_vt + " nội trú nên bạn không được phép sửa. Đề nghị kiểm tra lại");
                return false;
            }
            KcbDonthuoc _item =
                new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.IdDonthuocColumn).IsEqualTo(pres_id)
                .And(KcbDonthuoc.TrangThaiColumn).IsEqualTo(1).ExecuteSingle<KcbDonthuoc>();
            if (_item != null)
            {
                Utility.ShowMsg("Đơn " + thuoc_vt + " này đang ở trạng thái đã duyệt cho Bệnh nhân nên không thể chỉnh sửa. Đề nghị kiểm tra lại");
                return false;
            }
            return true;
        }

        private void UpdateDonThuoc()
        {
            try
            {
                if (grdPresDetail.CurrentRow != null && grdPresDetail.CurrentRow.RowType == RowType.Record)
                {
                    KcbLuotkham objPatientExam = CreatePatientExam();
                    if (objPatientExam != null)
                    {
                        int Pres_ID = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                        if (!IsValid_UpdateDonthuoc(Pres_ID,"thuốc"))
                        {
                            return;
                        }
                        var v_collect = new Select().From(KcbDonthuocChitiet.Schema.TableName)
                            .Where(KcbDonthuocChitiet.TrangthaiThanhtoanColumn.ColumnName).IsEqualTo(1)
                            .And(KcbDonthuocChitiet.IdDonthuocColumn.ColumnName).IsEqualTo(Pres_ID)
                            .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                        if (v_collect.Count > 0)
                        {
                            Utility.ShowMsg(
                                "Đơn thuốc bạn đang chọn sửa đã được thanh toán. Muốn sửa lại đơn thuốc Bạn cần phải liên hệ với bộ phận Thanh toán để hủy thanh toán và Bộ phận cấp thuốc để hủy xác nhận đơn thuốc tại kho thuốc");
                            return;
                        }
                        KcbDonthuoc objPrescription = KcbDonthuoc.FetchByID(Pres_ID);
                        if (objPrescription != null)
                        {
                            frm_KCB_KE_DONTHUOC frm = new frm_KCB_KE_DONTHUOC("THUOC");
                            frm.id_khoa = Utility.Int16Dbnull(cboKhoanoitru.SelectedValue, -1);
                            frm.em_Action = action.Update;
                            frm._KcbCDKL = _KcbChandoanKetluan;
                            frm._MabenhChinh = txtMaBenhChinh.MyCode;
                            frm._Chandoan = txtChanDoan.Text;
                            frm.DtIcd = globalVariables.gv_dtDmucBenh;
                            if (objPhieudieutri != null)
                                frm.forced2Add = Utility.Byte2Bool(objPhieudieutri.TthaiBosung);
                            frm.dt_ICD_PHU = dt_ICD_PHU;
                            frm.noitru = 1;
                            frm.objLuotkham = CreatePatientExam();
                            frm.id_kham =-1;
                            frm.objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtIdPhieudieutri.Text));
                            frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                            frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                            frm.txtSoDT.Text = Utility.sDbnull(objBenhnhan.DienThoai, "");
                            frm.txtPatientName.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan, "");
                            frm.txtYearBirth.Text = Utility.sDbnull(objBenhnhan.NamSinh, "");
                            frm.txtSex.Text = Utility.sDbnull(objBenhnhan.GioiTinh, "");
                            frm.txtSDT.Text = objLuotkham.Sdt;

                            frm.txtPres_ID.Text = Utility.sDbnull(objPrescription.IdDonthuoc);
                            frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                            frm.ShowDialog();
                            if (!frm.m_blnCancel)
                            {
                                txtMaBenhChinh.SetCode( frm._MabenhChinh);
                                txtChanDoan._Text = frm._Chandoan;
                                dt_ICD_PHU = frm.dt_ICD_PHU;
                                if (frm._KcbCDKL != null) _KcbChandoanKetluan = frm._KcbCDKL;
                                LaythongtinPhieudieutri();
                                TinhtoanTongchiphi();
                                Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuocChitiet.Columns.IdDonthuoc,
                                                        Utility.sDbnull(frm.txtPres_ID.Text));
                                ModifyCommmands();
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
        private void UpdateDonThuoc_Ravien()
        {
            try
            {
                if (grdDonthuocravien.CurrentRow != null && grdDonthuocravien.CurrentRow.RowType == RowType.Record)
                {
                    KcbLuotkham objPatientExam = CreatePatientExam();
                    if (objPatientExam != null)
                    {
                        int Pres_ID = Utility.Int32Dbnull(grdDonthuocravien.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                        if (!IsValid_UpdateDonthuoc(Pres_ID, "thuốc"))
                        {
                            return;
                        }
                        var v_collect = new Select().From(KcbDonthuocChitiet.Schema.TableName)
                            .Where(KcbDonthuocChitiet.TrangthaiThanhtoanColumn.ColumnName).IsEqualTo(1)
                            .And(KcbDonthuocChitiet.IdDonthuocColumn.ColumnName).IsEqualTo(Pres_ID)
                            .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                        if (v_collect.Count > 0)
                        {
                            Utility.ShowMsg(
                                "Đơn thuốc bạn đang chọn sửa đã được thanh toán. Muốn sửa lại đơn thuốc Bạn cần phải liên hệ với bộ phận Thanh toán để hủy thanh toán và Bộ phận cấp thuốc để hủy xác nhận đơn thuốc tại kho thuốc");
                            return;
                        }
                        KcbDonthuoc objPrescription = KcbDonthuoc.FetchByID(Pres_ID);
                        if (objPrescription != null)
                        {
                            frm_KCB_KE_DONTHUOC frm = new frm_KCB_KE_DONTHUOC("THUOC");
                            frm.id_khoa = Utility.Int16Dbnull(cboKhoanoitru.SelectedValue, -1);
                            frm.em_Action = action.Update;
                            frm.KieuDonthuoc = 3;
                            frm._KcbCDKL = _KcbChandoanKetluan;
                            frm._MabenhChinh = txtMaBenhChinh.MyCode;
                            frm._Chandoan = txtChanDoan.Text;
                            frm.DtIcd = globalVariables.gv_dtDmucBenh;
                            if (objPhieudieutri != null)
                                frm.forced2Add = Utility.Byte2Bool(objPhieudieutri.TthaiBosung);
                            frm.dt_ICD_PHU = dt_ICD_PHU;
                            frm.noitru = 1;
                            frm.objLuotkham = CreatePatientExam();
                            frm.id_kham = -1;
                            frm.objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtIdPhieudieutri.Text));
                            frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                            frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                            frm.txtSoDT.Text = Utility.sDbnull(objBenhnhan.DienThoai, "");
                            frm.txtPatientName.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan, "");
                            frm.txtYearBirth.Text = Utility.sDbnull(objBenhnhan.NamSinh, "");
                            frm.txtSex.Text = Utility.sDbnull(objBenhnhan.GioiTinh, "");
                            frm.txtSDT.Text = objLuotkham.Sdt;

                            frm.txtPres_ID.Text = Utility.sDbnull(objPrescription.IdDonthuoc);
                            frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                            frm.ShowDialog();
                            if (!frm.m_blnCancel)
                            {
                                txtMaBenhChinh.SetCode(frm._MabenhChinh);
                                txtChanDoan._Text = frm._Chandoan;
                                dt_ICD_PHU = frm.dt_ICD_PHU;
                                if (frm._KcbCDKL != null) _KcbChandoanKetluan = frm._KcbCDKL;
                                LaythongtinPhieudieutri();
                                TinhtoanTongchiphi();
                                Utility.GotoNewRowJanus(grdDonthuocravien, KcbDonthuocChitiet.Columns.IdDonthuoc,
                                                        Utility.sDbnull(frm.txtPres_ID.Text));
                                ModifyCommmands();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        List<int> GetIdChitietVTTH(int IdDonthuoc, int id_thuoc, decimal don_gia, ref string s)
        {
            var _data = from p in m_dtVTTH.AsEnumerable()
                        where Utility.Int32Dbnull(p[KcbDonthuocChitiet.Columns.IdDonthuoc]) == IdDonthuoc
                        && Utility.Int32Dbnull(p[KcbDonthuocChitiet.Columns.IdThuoc]) == id_thuoc
                        && Utility.DecimaltoDbnull(p[KcbDonthuocChitiet.Columns.DonGia]) == don_gia
                        select p;
            if (_data.Any())
            {
                DataRow[] arrDr = _data.ToArray<DataRow>();
                var p1 = (from q in arrDr.AsEnumerable()
                          select Utility.sDbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                s = string.Join(",", p1.ToArray());
                var p = (from q in arrDr.AsEnumerable()
                         select Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                return p.ToList<int>();
            }
            return new List<int>();
        }
        List<int> GetIdChitietVTTHtronggoi(int IdDonthuoc,int id_thuoc, decimal don_gia, ref string s)
        {
            var _data = from p in m_dtVTTH_tronggoi.AsEnumerable()
                        where Utility.Int32Dbnull(p[KcbDonthuocChitiet.Columns.IdDonthuoc]) == IdDonthuoc
                        && Utility.Int32Dbnull(p[KcbDonthuocChitiet.Columns.IdThuoc]) == id_thuoc
                        && Utility.DecimaltoDbnull(p[KcbDonthuocChitiet.Columns.DonGia]) == don_gia
                        select p;
            if (_data.Any())
            {
                DataRow[] arrDr = _data.ToArray<DataRow>();
                var p1 = (from q in arrDr.AsEnumerable()
                          select Utility.sDbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                s = string.Join(",", p1.ToArray());
                var p = (from q in arrDr.AsEnumerable()
                         select Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                return p.ToList<int>();
            }
            return new List<int>();
        }
        List<int> GetIdChitiet(int IdDonthuoc, int id_thuoc, decimal don_gia, ref string s)
        {
            var _data = from p in m_dtDonthuoc.AsEnumerable()
                        where Utility.Int32Dbnull(p[KcbDonthuocChitiet.Columns.IdDonthuoc]) == IdDonthuoc
                        && Utility.Int32Dbnull(p[KcbDonthuocChitiet.Columns.IdThuoc]) == id_thuoc
                        && Utility.DecimaltoDbnull(p[KcbDonthuocChitiet.Columns.DonGia]) == don_gia
                        select p;
            if (_data.Any())
            {
                DataRow[] arrDr = _data.ToArray<DataRow>();
                var p1 = (from q in arrDr.AsEnumerable()
                          select Utility.sDbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                s = string.Join(",", p1.ToArray());
                var p = (from q in arrDr.AsEnumerable()
                         select Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                return p.ToList<int>();
            }
            return new List<int>();
        }
        List<int> GetIdChitiet(DataTable dtData, int IdDonthuoc, int id_thuoc, decimal don_gia, ref string s)
        {
            var _data = from p in dtData.AsEnumerable()
                        where Utility.Int32Dbnull(p[KcbDonthuocChitiet.Columns.IdDonthuoc]) == IdDonthuoc
                        && Utility.Int32Dbnull(p[KcbDonthuocChitiet.Columns.IdThuoc]) == id_thuoc
                        && Utility.DecimaltoDbnull(p[KcbDonthuocChitiet.Columns.DonGia]) == don_gia
                        select p;
            if (_data.Any())
            {
                DataRow[] arrDr = _data.ToArray<DataRow>();
                var p1 = (from q in arrDr.AsEnumerable()
                          select Utility.sDbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                s = string.Join(",", p1.ToArray());
                var p = (from q in arrDr.AsEnumerable()
                         select Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                return p.ToList<int>();
            }
            return new List<int>();
        }
        void XoachandoanKcb(List<int> lstIdChandoanKcb)
        {
            try
            {
                var p = (from q in m_dtChandoanKCB.Select("1=1").AsEnumerable()
                         where lstIdChandoanKcb.Contains(Utility.Int32Dbnull(q[KcbChandoanKetluan.Columns.IdChandoan]))
                         select q).ToArray<DataRow>();
                for (int i = 0; i <= p.Length - 1; i++)
                    m_dtChandoanKCB.Rows.Remove(p[i]);
                m_dtChandoanKCB.AcceptChanges();
            }
            catch (Exception ex)
            {
                log.Trace("Loi: " + ex.Message);
            }
        }
        void XoaDinhduong(List<int> lstIdDinhduong)
        {
            try
            {
                var p = (from q in m_dtChedoDinhduong.Select("1=1").AsEnumerable()
                         where lstIdDinhduong.Contains(Utility.Int32Dbnull(q[NoitruPhieudinhduong.Columns.Id]))
                         select q).ToArray<DataRow>();
                for (int i = 0; i <= p.Length - 1; i++)
                    m_dtChedoDinhduong.Rows.Remove(p[i]);
                m_dtChedoDinhduong.AcceptChanges();
            }
            catch (Exception ex)
            {
                log.Trace("Loi: " + ex.Message);
            }
        }
        void XoaVTTHKhoiBangDulieu_trongoi(List<int> lstIdChitietDonthuoc)
        {
            try
            {
                var p = (from q in m_dtVTTH_tronggoi.Select("1=1").AsEnumerable()
                         where lstIdChitietDonthuoc.Contains(Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]))
                         select q).ToArray<DataRow>();
                for (int i = 0; i <= p.Length - 1; i++)
                    m_dtVTTH_tronggoi.Rows.Remove(p[i]);
                m_dtVTTH_tronggoi.AcceptChanges();
            }
            catch (Exception ex)
            {
                log.Trace("Loi: " + ex.Message);
            }
        }
        void XoaVtthKhoiBangDulieu(List<int> lstIdChitietDonthuoc)
        {
            try
            {
                var p = (from q in m_dtVTTH.Select("1=1").AsEnumerable()
                         where lstIdChitietDonthuoc.Contains(Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]))
                         select q).ToArray<DataRow>();
                for (int i = 0; i <= p.Length - 1; i++)
                    m_dtVTTH.Rows.Remove(p[i]);
                m_dtVTTH.AcceptChanges();
            }
            catch (Exception ex)
            {
                log.Trace("Loi: "+ ex.Message);
            }
        }
        void XoaThuocKhoiBangdulieu(DataTable dtData, List<int> lstIdChitietDonthuoc)
        {
            try
            {
                var p = (from q in dtData.Select("1=1").AsEnumerable()
                         where lstIdChitietDonthuoc.Contains(Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]))
                         select q).ToArray<DataRow>();
                for (int i = 0; i <= p.Length - 1; i++)
                    dtData.Rows.Remove(p[i]);
                dtData.AcceptChanges();
            }
            catch (Exception ex)
            {
                log.Trace("Loi: " + ex.Message);
            }
        }

        private void ThemphieuVattu_tronggoi()
        {
            try
            {
                // KeDonThuocTheoDoiTuong();
                frm_KCB_KE_DONTHUOC frm = new frm_KCB_KE_DONTHUOC("VT");
                frm.id_khoa = Utility.Int16Dbnull(cboKhoanoitru.SelectedValue, -1);
                frm.em_Action = action.Insert;
                frm.objLuotkham = CreatePatientExam();
                frm._KcbCDKL = _KcbChandoanKetluan;
                frm._MabenhChinh = txtMaBenhChinh.MyCode;
                frm._Chandoan = txtChanDoan.Text;
                if (objPhieudieutri != null)
                    frm.forced2Add = Utility.Byte2Bool(objPhieudieutri.TthaiBosung);
                frm.DtIcd = globalVariables.gv_dtDmucBenh;
                frm.dt_ICD_PHU = dt_ICD_PHU;
                frm.id_kham = Utility.Int32Dbnull(txtExam_ID.Text);
                frm.objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtIdPhieudieutri.Text));
                frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                frm.id_kham = -1;
                frm.id_goidv=Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdGoidichvu,KcbChidinhclsChitiet.Columns.IdChitietchidinh),-1) ;
                frm.trong_goi=1;
                frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                frm.txtPres_ID.Text = "-1";
                frm.noitru = 1;
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    txtMaBenhChinh.SetCode( frm._MabenhChinh);
                    txtChanDoan._Text = frm._Chandoan;
                    dt_ICD_PHU = frm.dt_ICD_PHU;
                    if (frm._KcbCDKL != null) _KcbChandoanKetluan = frm._KcbCDKL;
                    LaythongtinPhieudieutri();
                    TinhtoanTongchiphi();
                    Utility.GotoNewRowJanus(grdVTTH_tronggoi, KcbDonthuoc.Columns.IdDonthuoc,
                                            Utility.sDbnull(frm.txtPres_ID.Text));
                    ModifyCommmands();
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
                txtPatient_Code.Focus();
                txtPatient_Code.SelectAll();
            }
        }
        private void SuaphieuVattu_tronggoi()
        {
            try
            {
                if (!CheckPatientSelected()) return;
                if (!Utility.isValidGrid(grdVTTH_tronggoi))
                {
                    if (grdVTTH_tronggoi.GetDataRows().Length > 0)
                        grdVTTH_tronggoi.MoveFirst();
                }
                //Check lại cho chắc ăn
                if (!Utility.isValidGrid(grdVTTH_tronggoi))
                {
                    return;
                }
                KcbLuotkham objPatientExam = CreatePatientExam();
                if (objPatientExam != null)
                {
                    int Pres_ID = Utility.Int32Dbnull(grdVTTH.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                    if (!IsValid_UpdateDonthuoc(Pres_ID,"vật tư"))
                    {
                        return;
                    }
                    var v_collect = new Select().From(KcbDonthuocChitiet.Schema.TableName)
                        .Where(KcbDonthuocChitiet.TrangthaiThanhtoanColumn.ColumnName).IsEqualTo(1)
                        .And(KcbDonthuocChitiet.IdDonthuocColumn.ColumnName).IsEqualTo(Pres_ID)
                        .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                    if (v_collect.Count > 0)
                    {
                        Utility.ShowMsg(
                            "Phiếu vật tư bạn đang chọn sửa đã được thanh toán. Muốn sửa lại Phiếu vật tư Bạn cần phải liên hệ với bộ phận Thanh toán để hủy thanh toán và Bộ phận cấp vật tư tiêu hao để hủy xác nhận Phiếu vật tư tại kho vật tư");
                        return;
                    }
                    KcbDonthuoc objPrescription = KcbDonthuoc.FetchByID(Pres_ID);
                    if (objPrescription != null)
                    {
                        frm_KCB_KE_DONTHUOC frm = new frm_KCB_KE_DONTHUOC("VT");
                        frm.id_khoa = Utility.Int16Dbnull(cboKhoanoitru.SelectedValue, -1);
                        frm.em_Action = action.Update;
                        frm._KcbCDKL = _KcbChandoanKetluan;
                        frm._MabenhChinh = txtMaBenhChinh.MyCode;
                        frm._Chandoan = txtChanDoan.Text;
                        frm.DtIcd = globalVariables.gv_dtDmucBenh;
                        frm.dt_ICD_PHU = dt_ICD_PHU;
                        if (objPhieudieutri != null)
                            frm.forced2Add = Utility.Byte2Bool(objPhieudieutri.TthaiBosung);
                        frm.noitru = 1;
                        frm.id_goidv = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdGoidichvu, KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                        frm.trong_goi = 1;
                        frm.objLuotkham = CreatePatientExam();
                        frm.id_kham = -1;
                        frm.objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtIdPhieudieutri.Text));
                        frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                        frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                        frm.txtPres_ID.Text = Utility.sDbnull(objPrescription.IdDonthuoc);
                        frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                        frm.ShowDialog();
                        if (!frm.m_blnCancel)
                        {
                            txtMaBenhChinh.SetCode( frm._MabenhChinh);
                            txtChanDoan._Text = frm._Chandoan;
                            dt_ICD_PHU = frm.dt_ICD_PHU;
                            if (frm._KcbCDKL != null) _KcbChandoanKetluan = frm._KcbCDKL;
                            LaythongtinPhieudieutri();
                            TinhtoanTongchiphi();
                            Utility.GotoNewRowJanus(grdVTTH_tronggoi, KcbDonthuocChitiet.Columns.IdDonthuoc,
                                                    Utility.sDbnull(frm.txtPres_ID.Text));
                            ModifyCommmands();
                        }
                    }
                }
            }
            catch
            {
            }
        }
        private void PerformActionDeleteVTTH_tronggoi()
        {
            try
            {
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa các VTTH đang chọn?", "Xác nhận xóa VTTH", true))
                    return;
                string s = "";
                int Pres_ID = Utility.Int32Dbnull(grdVTTH_tronggoi.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                if (!IsValid_UpdateDonthuoc(Pres_ID,"vật tư"))
                {
                    return;
                }
                var v_collect = new Select().From(KcbDonthuocChitiet.Schema.TableName)
                    .Where(KcbDonthuocChitiet.TrangthaiThanhtoanColumn.ColumnName).IsEqualTo(1)
                    .And(KcbDonthuocChitiet.IdDonthuocColumn.ColumnName).IsEqualTo(Pres_ID)
                    .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                if (v_collect.Count > 0)
                {
                    Utility.ShowMsg(
                        "Phiếu vật tư bạn đang chọn sửa đã được thanh toán. Muốn sửa lại Phiếu vật tư Bạn cần phải liên hệ với bộ phận Thanh toán để hủy thanh toán và Bộ phận cấp vật tư tiêu hao để hủy xác nhận Phiếu vật tư tại kho vật tư");
                    return;
                }
                List<int> lstIdchitiet = new List<int>();
                foreach (GridEXRow gridExRow in grdVTTH_tronggoi.GetCheckedRows())
                {
                    string stempt = "";
                    int id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, 0m);
                    int IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value, 0m);
                    decimal dongia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, 0m);
                    List<int> _temp = GetIdChitietVTTHtronggoi(IdDonthuoc,id_thuoc, dongia, ref stempt);
                    s += "," + stempt;
                    lstIdchitiet.AddRange(_temp);
                    gridExRow.Delete();
                    grdVTTH.UpdateData();

                }
                _KCB_KEDONTHUOC.XoaChitietDonthuoc(s);
                DataRow[] rows =
                     m_dtVTTH_tronggoi.Select(KcbDonthuocChitiet.Columns.IdChitietdonthuoc + " IN (" + String.Join(",", lstIdchitiet.ToArray()) + ")");
                string _deleteitems = string.Join(",", (from p in rows.AsEnumerable()
                                                        select Utility.sDbnull(p["ten_thuoc"])).ToList<string>());
                // UserName is Column Name
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa VTTH trong gói của bệnh nhân ID={0}, PID={1}, Tên={2}, DS thuốc xóa={3} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan, _deleteitems), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                XoaVTTHKhoiBangDulieu_trongoi(lstIdchitiet);
                m_dtVTTH_tronggoi.AcceptChanges();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
           
        }
        private void XoaDinhduong(GridEXRow gridExRow)
        {
            try
            {
                string s = "";
                List<int> lstId = new List<int>();
                string stempt = "";
                int Id = Utility.Int32Dbnull(gridExRow.Cells[NoitruPhieudinhduong.Columns.Id].Value, 0m);
                s += "," + Id.ToString();
                lstId.Add(Id);
                grdChedoDinhduong.Delete();
                grdChedoDinhduong.UpdateData();


                _KCB_KEDONTHUOC.NoitruXoaDinhduong(s);
                XoaDinhduong(lstId);
                m_dtChedoDinhduong.AcceptChanges();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }

        }
        private void XoaDinhduong()
        {
            try
            {
                string s = "";
                List<int> lstId = new List<int>();
                foreach (GridEXRow gridExRow in grdChedoDinhduong.GetCheckedRows())
                {
                    string stempt = "";
                    int Id = Utility.Int32Dbnull(gridExRow.Cells[NoitruPhieudinhduong.Columns.Id].Value, 0m);
                    s += "," + Id.ToString();
                    lstId.Add(Id);
                    grdChedoDinhduong.Delete();
                    grdChedoDinhduong.UpdateData();

                }
                _KCB_KEDONTHUOC.NoitruXoaDinhduong(s);
                XoaDinhduong(lstId);
                m_dtChedoDinhduong.AcceptChanges();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }

        }
        private void XoaChandoan()
        {
            try
            {
                string s = "";
                int Pres_ID = Utility.Int32Dbnull(grdChandoan.GetValue(KcbChandoanKetluan.Columns.IdChandoan));
                List<int> lstIdchitiet = new List<int>();
                foreach (GridEXRow gridExRow in grdChandoan.GetCheckedRows())
                {
                    string stempt = "";
                    int IdChandoan = Utility.Int32Dbnull(gridExRow.Cells[KcbChandoanKetluan.Columns.IdChandoan].Value, 0m);
                    s += "," + IdChandoan.ToString();
                    lstIdchitiet.Add(IdChandoan);
                    grdChandoan.Delete();
                    grdChandoan.UpdateData();

                }
                _KCB_KEDONTHUOC.NoitruXoachandoan(s);
                XoachandoanKcb(lstIdchitiet);
                m_dtChandoanKCB.AcceptChanges();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }

        }
        private void ThemphieuVattu()
        {
            try
            {
               
                frm_KCB_KE_DONTHUOC frm = new frm_KCB_KE_DONTHUOC("VT");
                frm.id_khoa = Utility.Int16Dbnull(cboKhoanoitru.SelectedValue, -1);
                frm.em_Action = action.Insert;
                frm.objLuotkham = CreatePatientExam();
                frm._KcbCDKL = _KcbChandoanKetluan;
                frm._MabenhChinh = txtMaBenhChinh.MyCode;
                frm._Chandoan = txtChanDoan.Text;
                frm.DtIcd = globalVariables.gv_dtDmucBenh;
                if (objPhieudieutri != null)
                    frm.forced2Add = Utility.Byte2Bool(objPhieudieutri.TthaiBosung);
                frm.dt_ICD_PHU = dt_ICD_PHU;
                frm.id_kham = Utility.Int32Dbnull(txtExam_ID.Text);
                frm.objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtIdPhieudieutri.Text));
                frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                frm.id_kham = -1;
                frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                frm.txtPres_ID.Text = "-1";
                frm.noitru = 1;
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    txtMaBenhChinh.SetCode( frm._MabenhChinh);
                    txtChanDoan._Text = frm._Chandoan;
                    dt_ICD_PHU = frm.dt_ICD_PHU;
                    if (frm._KcbCDKL != null) _KcbChandoanKetluan = frm._KcbCDKL;
                    LaythongtinPhieudieutri();
                    TinhtoanTongchiphi();
                    Utility.GotoNewRowJanus(grdVTTH, KcbDonthuoc.Columns.IdDonthuoc,
                                            Utility.sDbnull(frm.txtPres_ID.Text));
                    ModifyCommmands();
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
                txtPatient_Code.Focus();
                txtPatient_Code.SelectAll();
            }
        }
        private void SuaphieuVattu()
        {
            try
            {
                if (!CheckPatientSelected()) return;
                if (!Utility.isValidGrid(grdVTTH)) return;

                KcbLuotkham objPatientExam = CreatePatientExam();
                if (objPatientExam != null)
                {
                    int Pres_ID = Utility.Int32Dbnull(grdVTTH.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                    if (!IsValid_UpdateDonthuoc(Pres_ID,"vật tư"))
                    {
                        return;
                    }
                    var v_collect = new Select().From(KcbDonthuocChitiet.Schema.TableName)
                        .Where(KcbDonthuocChitiet.TrangthaiThanhtoanColumn.ColumnName).IsEqualTo(1)
                        .And(KcbDonthuocChitiet.IdDonthuocColumn.ColumnName).IsEqualTo(Pres_ID)
                        .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                    if (v_collect.Count > 0)
                    {
                        Utility.ShowMsg(
                            "Phiếu vật tư bạn đang chọn sửa đã được thanh toán. Muốn sửa lại Phiếu vật tư Bạn cần phải liên hệ với bộ phận Thanh toán để hủy thanh toán và Bộ phận cấp vật tư để hủy xác nhận Phiếu vật tư tại kho vật tư");
                        return;
                    }
                    KcbDonthuoc objPrescription = KcbDonthuoc.FetchByID(Pres_ID);
                    if (objPrescription != null)
                    {
                        frm_KCB_KE_DONTHUOC frm = new frm_KCB_KE_DONTHUOC("VT");
                        frm.id_khoa = Utility.Int16Dbnull(cboKhoanoitru.SelectedValue, -1);
                        frm.em_Action = action.Update;
                        frm._KcbCDKL = _KcbChandoanKetluan;
                        frm._MabenhChinh = txtMaBenhChinh.MyCode;
                        if (objPhieudieutri != null)
                            frm.forced2Add = Utility.Byte2Bool(objPhieudieutri.TthaiBosung);
                        frm._Chandoan = txtChanDoan.Text;
                        frm.DtIcd = globalVariables.gv_dtDmucBenh;
                        frm.dt_ICD_PHU = dt_ICD_PHU;
                        frm.noitru = 1;
                        frm.objLuotkham = CreatePatientExam();
                        frm.id_kham = -1;
                        frm.objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtIdPhieudieutri.Text));
                        frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                        frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                        frm.txtPres_ID.Text = Utility.sDbnull(objPrescription.IdDonthuoc);
                        frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                        frm.ShowDialog();
                        if (!frm.m_blnCancel)
                        {
                            txtMaBenhChinh.SetCode( frm._MabenhChinh);
                            txtChanDoan._Text = frm._Chandoan;
                            dt_ICD_PHU = frm.dt_ICD_PHU;
                            if (frm._KcbCDKL != null) _KcbChandoanKetluan = frm._KcbCDKL;
                            LaythongtinPhieudieutri();
                            TinhtoanTongchiphi();
                            Utility.GotoNewRowJanus(grdVTTH, KcbDonthuocChitiet.Columns.IdDonthuoc,
                                                    Utility.sDbnull(frm.txtPres_ID.Text));
                            ModifyCommmands();
                        }
                    }
                }

            }
            catch
            {
            }
        }
        private void PerformActionDeleteVTTH()
        {
            if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa các VTTH đang chọn?", "Xác nhận xóa VTTH", true))
                return;
            string s = "";
            int Pres_ID = Utility.Int32Dbnull(grdVTTH.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            if (!IsValid_UpdateDonthuoc(Pres_ID,"vật tư"))
            {
                return;
            }
            var v_collect = new Select().From(KcbDonthuocChitiet.Schema.TableName)
                .Where(KcbDonthuocChitiet.TrangthaiThanhtoanColumn.ColumnName).IsEqualTo(1)
                .And(KcbDonthuocChitiet.IdDonthuocColumn.ColumnName).IsEqualTo(Pres_ID)
                .ExecuteAsCollection<KcbDonthuocChitietCollection>();
            if (v_collect.Count > 0)
            {
                Utility.ShowMsg(
                    "Phiếu vật tư bạn đang chọn sửa đã được thanh toán. Muốn sửa lại Phiếu vật tư Bạn cần phải liên hệ với bộ phận Thanh toán để hủy thanh toán và Bộ phận cấp vật tư để hủy xác nhận Phiếu vật tư tại kho vật tư");
                return;
            }
            List<int> lstIdchitiet = new List<int>();
            foreach (GridEXRow gridExRow in grdVTTH.GetCheckedRows())
            {
                string stempt = "";
                int id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, 0m);
                decimal dongia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, 0m);
                int IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value, 0m);
                List<int> _temp = GetIdChitietVTTH(IdDonthuoc,id_thuoc, dongia, ref stempt);
                s += "," + stempt;
                lstIdchitiet.AddRange(_temp);
                //gridExRow.Delete();
                //grdVTTH.UpdateData();

            }
            _KCB_KEDONTHUOC.XoaChitietDonthuoc(s);
            DataRow[] rows =
                      m_dtVTTH.Select(KcbDonthuocChitiet.Columns.IdChitietdonthuoc + " IN (" + String.Join(",", lstIdchitiet.ToArray()) + ")");
            string _deleteitems = string.Join(",", (from p in rows.AsEnumerable()
                                                    select Utility.sDbnull(p["ten_thuoc"])).ToList<string>());
            // UserName is Column Name
            Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa VTTH trong đơn VTTH của bệnh nhân ID={0}, PID={1}, Tên={2}, DS thuốc xóa={3} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan, _deleteitems), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
            XoaVtthKhoiBangDulieu(lstIdchitiet);
            m_dtVTTH.AcceptChanges();
        }
        private void PerformActionDeletePres()
        {
            if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa các thuốc đang chọn?", "Xác nhận xóa thuốc", true))
                return;
            string s = "";
            int Pres_ID = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            if (!IsValid_UpdateDonthuoc(Pres_ID,"thuốc"))
            {
                return;
            }
            var v_collect = new Select().From(KcbDonthuocChitiet.Schema.TableName)
                .Where(KcbDonthuocChitiet.TrangthaiThanhtoanColumn.ColumnName).IsEqualTo(1)
                .And(KcbDonthuocChitiet.IdDonthuocColumn.ColumnName).IsEqualTo(Pres_ID)
                .ExecuteAsCollection<KcbDonthuocChitietCollection>();
            if (v_collect.Count > 0)
            {
                Utility.ShowMsg(
                    "Đơn thuốc bạn đang chọn sửa đã được thanh toán. Muốn sửa lại Đơn thuốc Bạn cần phải liên hệ với bộ phận Thanh toán để hủy thanh toán và Bộ phận cấp vật tư để hủy xác nhận thuốc tại kho thuốc");
                return;
            }
            List<int> lstIdchitiet = new List<int>();
            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                string stempt = "";
                int id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value,0m);
                decimal dongia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value,0m);
                int IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value, 0m);
                List<int> _temp = GetIdChitiet(IdDonthuoc,id_thuoc, dongia, ref stempt);
                s += "," + stempt;
                lstIdchitiet.AddRange(_temp);
                //gridExRow.Delete();
                //grdPresDetail.UpdateData();
               
            }
            _KCB_KEDONTHUOC.XoaChitietDonthuoc(s);
            DataRow[] rows =
                       m_dtDonthuoc.Select(KcbDonthuocChitiet.Columns.IdChitietdonthuoc + " IN (" + String.Join(",", lstIdchitiet.ToArray()) + ")");
            string _deleteitems = string.Join(",", (from p in rows.AsEnumerable()
                                                    select Utility.sDbnull(p["ten_thuoc"])).ToList<string>());
            // UserName is Column Name
            Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa thuốc trong đơn thuốc của bệnh nhân ID={0}, PID={1}, Tên={2}, DS thuốc xóa={3} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan, _deleteitems), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
            XoaThuocKhoiBangdulieu(m_dtDonthuoc, lstIdchitiet);
            m_dtDonthuoc.AcceptChanges();
        }
        private void PerformActionDeletePres_old()
        {
            string s = "";
            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                int Pres_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value,
                                                  -1);
                int PresDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                                                        -1);
                int Drug_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value,
                                                  -1);
                _KCB_KEDONTHUOC.XoaChitietDonthuoc(PresDetail_ID);
                gridExRow.Delete();
                grdPresDetail.UpdateData();
                m_dtDonthuoc.AcceptChanges();
            }
        }
        private void PerformActionDeleteSelectedDrug()
        {
            try
            {
                int Pres_ID =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value,
                                        -1);
                int PresDetail_ID =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                                        -1);
                int Drug_ID =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value,
                                        -1);
                _KCB_KEDONTHUOC.XoaChitietDonthuoc(PresDetail_ID);
                grdPresDetail.CurrentRow.Delete();
                grdPresDetail.UpdateData();
                m_dtDonthuoc.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message + "-->" +
                                "Bạn nên dùng chức năng xóa thuốc bằng cách chọn thuốc và sử dụng nút xóa thuốc");
            }
        }
        private bool IsValidVTTH_delete()
        {
            bool b_Cancel = false;
            if (chkShowGroup.Checked)
            {
                Utility.ShowMsg("Bạn cần bỏ check mục Nhóm thuốc khi thực hiện thao tác xóa");
                grdVTTH.UnCheckAllRecords();
                return false;
            }
            if (grdVTTH.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện việc xóa thông tin Vật tư tiêu hao ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdVTTH.Focus();
                return false;
            }

            KcbDonthuocChitiet objKcbDonthuocChitiet = null;
            foreach (GridEXRow gridExRow in grdVTTH.GetCheckedRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    int PresDetail_ID =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                    int Drug_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1);
                     objKcbDonthuocChitiet = new Select().From(KcbDonthuocChitiet.Schema)
                        .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(PresDetail_ID)
                        .ExecuteSingle<KcbDonthuocChitiet>();
                    if (objKcbDonthuocChitiet!=null)
                    {
                        if (Utility.Byte2Bool(objKcbDonthuocChitiet.TrangthaiThanhtoan))
                        {
                            Utility.ShowMsg("Bản ghi đã thanh toán, Bạn không thể xóa thông tin được ", "Thông báo",
                                            MessageBoxIcon.Warning);
                            b_Cancel = true;
                            break;
                        }
                        if (Utility.Byte2Bool(objKcbDonthuocChitiet.TrangThai))
                        {
                            Utility.ShowMsg("Bản ghi đã xác nhận, Bạn không thể xóa thông tin được ", "Thông báo",
                                            MessageBoxIcon.Warning);
                            b_Cancel = true;
                            break;
                        }
                    }
                }
            }
            if (b_Cancel)
            {
                grdVTTH.Focus();
                return false;
            }
            return true;
        }
        private bool IsValidVTTH_delete_trongoi()
        {
            bool b_Cancel = false;
            if (chkShowGroup.Checked)
            {
                Utility.ShowMsg("Bạn cần bỏ check mục Nhóm thuốc khi thực hiện thao tác xóa");
                grdVTTH_tronggoi.UnCheckAllRecords();
                return false;
            }
            if (grdVTTH_tronggoi.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện việc xóa thông tin Vật tư tiêu hao ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdVTTH_tronggoi.Focus();
                return false;
            }
            KcbDonthuocChitiet objKcbDonthuocChitiet = null;
            foreach (GridEXRow gridExRow in grdVTTH_tronggoi.GetCheckedRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    int PresDetail_ID =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                    int Drug_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1);
                     objKcbDonthuocChitiet = new Select().From(KcbDonthuocChitiet.Schema)
                        .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(PresDetail_ID)
                        .ExecuteSingle<KcbDonthuocChitiet>();
                    if (objKcbDonthuocChitiet!=null)
                    {
                        if (Utility.Byte2Bool(objKcbDonthuocChitiet.TrangthaiThanhtoan))
                        {
                            Utility.ShowMsg("Bản ghi đã thanh toán, Bạn không thể xóa thông tin được ", "Thông báo",
                                            MessageBoxIcon.Warning);
                            b_Cancel = true;
                            break;
                        }
                        if (Utility.Byte2Bool(objKcbDonthuocChitiet.TrangThai))
                        {
                            Utility.ShowMsg("Bản ghi đã xác nhận, Bạn không thể xóa thông tin được ", "Thông báo",
                                            MessageBoxIcon.Warning);
                            b_Cancel = true;
                            break;
                        }
                    }
                }
            }
            if (b_Cancel)
            {
                grdVTTH_tronggoi.Focus();
                return false;
            }
            return true;
        }
        private bool IsValidThuoc_delete()
        {
            bool b_Cancel = false;
            if (chkShowGroup.Checked)
            {
                Utility.ShowMsg("Bạn cần bỏ check mục Nhóm thuốc khi thực hiện thao tác xóa");
                grdPresDetail.UnCheckAllRecords();
                return false;
            }
            if (grdPresDetail.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện việc xóa thông tin thuốc ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }

            KcbDonthuocChitiet objKcbDonthuocChitiet = null;
            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    int PresDetail_ID =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                    int Drug_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1);
                    objKcbDonthuocChitiet = new Select().From(KcbDonthuocChitiet.Schema)
                        .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(PresDetail_ID)
                        .ExecuteSingle<KcbDonthuocChitiet>();
                    if (objKcbDonthuocChitiet != null)
                    {
                        if (Utility.Byte2Bool(objKcbDonthuocChitiet.TrangthaiThanhtoan))
                        {
                            Utility.ShowMsg("Bản ghi đã thanh toán, Bạn không thể xóa thông tin được ", "Thông báo",
                                            MessageBoxIcon.Warning);
                            b_Cancel = true;
                            break;
                        }
                        if (Utility.Byte2Bool(objKcbDonthuocChitiet.TrangthaiTonghop))
                        {
                            Utility.ShowMsg("Thuốc đã được tổng hợp cấp phát nội trú nên bạn không thể xóa. Vui lòng kiểm tra lại", "Thông báo",
                                            MessageBoxIcon.Warning);
                            b_Cancel = true;
                            break;
                        }
                        if (Utility.Byte2Bool(objKcbDonthuocChitiet.TrangThai))
                        {
                            Utility.ShowMsg("Thuốc đã được duyệt cấp phát nội trú nên bạn không thể xóa. Vui lòng kiểm tra lại", "Thông báo",
                                            MessageBoxIcon.Warning);
                            b_Cancel = true;
                            break;
                        }
                    }
                }
            }
            if (b_Cancel)
            {
                grdPresDetail.Focus();
                return false;
            }
            return true;
        }

        private bool InValiSelectedCLS()
        {
            bool b_Cancel = false;
            if (grdAssignDetail.RowCount <= 0 || grdAssignDetail.CurrentRow.RowType != RowType.Record)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện xóa chỉ định CLS", "Thông báo",
                                MessageBoxIcon.Warning);
                grdAssignDetail.Focus();
                return false;
            }


            int AssignDetail_ID =
                Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                    -1);
            SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                b_Cancel = true;
            }

            if (b_Cancel)
            {
                Utility.ShowMsg("Chỉ định bạn chọn đã được thanh toán nên bạn không thể xóa. Đề nghị kiểm tra lại");
                return false;
            }

            AssignDetail_ID =
                Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                    -1);
            sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                .And(KcbChidinhclsChitiet.Columns.TrangThai).IsGreaterThanOrEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                b_Cancel = true;
            }

            if (b_Cancel)
            {
                Utility.ShowMsg("Chỉ định bạn chọn đã được chuyển làm cận lâm sàng hoặc đã có kết quả nên không thể xóa. Đề nghị kiểm tra lại");
                return false;
            }
            return true;
        }

        private bool InValidDeleteSelectedDrug()
        {
            bool b_Cancel = false;
            if (grdPresDetail.RowCount <= 0 || grdPresDetail.CurrentRow.RowType != RowType.Record)
            {
                Utility.ShowMsg("Bạn phải chọn một thuốc để xóa ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }


            if (grdPresDetail.CurrentRow.RowType == RowType.Record)
            {
                int presDetailId =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                                        -1);
                int drugId =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1);
                SqlQuery sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(presDetailId)
                    .And(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1)
                    .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                }
            }

            if (b_Cancel)
            {
                Utility.ShowMsg("Bản ghi đã thanh toán hoặc đã được tổng hợp , Bạn không thể xóa thông tin được ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }
            return true;
        }

        private void cmdDeletePres_Click(object sender, EventArgs e)
        {
            if (CheckDachuyenkhoa()) return;
            if (!IsValidThuoc_delete()) return;
            PerformActionDeletePres();
            ModifyCommmands();
        }

        /// <summary>
        /// ham thực hiện việc in phiếu thuôc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPrintPres_Click(object sender, EventArgs e)
        {
            try
            {
                //frm_YHHQ_IN_DONTHUOC frm = new frm_YHHQ_IN_DONTHUOC();
                //frm.TrongGoi = 0;
                //frm.Exam_ID = Utility.Int32Dbnull(txtExam_ID.Text);
                //frm.Text = string.Format("In đơn thuốc  ngoại trú  theo đối tượng -{0}-{1}-{2}", txtTenBN.Text,
                //     Utility.sDbnull(radNam.Checked ? "Nam" : "Nữ"), Utility.sDbnull(txtYear_Of_Birth.Text));
                //frm.CallActionInDonThuoc = CallActionInDonThuoc.Exam_ID;
                //frm.ShowDialog();
                if (RowThuoc == null)
                {
                    Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện việc xóa thông tin thuốc ", "Thông báo", MessageBoxIcon.Warning);
                    grdPresDetail.Focus();
                    return;
                }
                int Pres_ID = Utility.Int32Dbnull(RowThuoc.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value);
                PrintPres(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, 1, objPhieudieutri.NgayDieutri.Value, Pres_ID, "",false);
            }
            catch (Exception)
            {
                // throw;
            }
        }

        /// <summary>
        /// hàm thực hiện việc in đơn thuốc
        /// </summary>
        /// <param name="PresID"></param>
        private void PrintPres_Backup(int PresID,string forcedTitle)
        {
            DataTable v_dtData = _KCB_KEDONTHUOC.LaythongtinDonthuoc_In(PresID);
            Utility.AddColumToDataTable(ref v_dtData, "BarCode", typeof (byte[]));
            int Pres_ID = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            //barcode.Data = Utility.sDbnull(Pres_ID);
            byte[] Barcode = null;
            Utility.CreateBarcodeData(ref v_dtData, PresID.ToString(), ref Barcode);
            string ICD_Name = "";
            string ICD_Code = "";
            bool chandoangiunguyen = THU_VIEN_CHUNG.Laygiatrithamsohethong("DONTHUOC_INCHANDOANTHEOBACSI_KE", "1", true) == "1";
            if (!chandoangiunguyen)
                if (v_dtData != null && v_dtData.Rows.Count > 0)
                    GetChanDoan(Utility.sDbnull(v_dtData.Rows[0]["mabenh_chinh"], ""),
                                Utility.sDbnull(v_dtData.Rows[0]["mabenh_phu"], ""), ref ICD_Name, ref ICD_Code);
           
            foreach (DataRow drv in v_dtData.Rows)
            {
                drv["BarCode"] = Barcode;
                if (!chandoangiunguyen)
                drv["chan_doan"] = Utility.sDbnull(drv["chan_doan"]).Trim() == ""
                                    ? ICD_Name
                                    : Utility.sDbnull(drv["chan_doan"]) + ";" + ICD_Name;
                drv["ma_icd"] = ICD_Code;
            }
            THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA4.xml");
            v_dtData.AcceptChanges();
            // log.Info("Thuc hien in don thuoc");
            Utility.UpdateLogotoDatatable(ref v_dtData);
            string KhoGiay = "A5";
            if (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4) KhoGiay = "A4";
            ReportDocument reportDocument = new ReportDocument();
            string reportCode = "";
             string tieude="", reportname = "";
            switch (KhoGiay)
            {
                case "A5":
                    reportCode = "thamkham_InDonthuocA5";
                    reportDocument =  Utility.GetReport("thamkham_InDonthuocA5",ref tieude,ref reportname);
                    break;
                case "A4":
                    reportCode = "thamkham_InDonthuocA4";
                    reportDocument = Utility.GetReport("thamkham_InDonthuocA4" ,ref tieude,ref reportname);
                    break;
                default:
                    reportCode = "thamkham_InDonthuocA5";
                    reportDocument = Utility.GetReport("thamkham_InDonthuocA5" ,ref tieude,ref reportname);
                    break;
            }
            if (reportDocument == null) return;
            //v_dtData.AcceptChanges();
            Utility.WaitNow(this);
            if (Utility.DoTrim(forcedTitle).Length > 0)
                tieude = forcedTitle;
            ReportDocument crpt = reportDocument;
            var objForm = new frmPrintPreview("IN ĐƠN THUỐC BỆNH NHÂN", crpt, true, true);
            try
            {
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                crpt.SetDataSource(v_dtData);
                Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt,"Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "ReportTitle", tieude);
                Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInDonthuoc))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    objForm.ShowDialog();
                    cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.DefaultNow(this);
            }
        }
        private void PrintPres(long id_benhnhan, string ma_luotkham, byte noitru, DateTime ngay_kedon, int presID, string forcedTitle, bool donravien)
        {
            try
            {
                DataTable v_dtDataOrg = _KCB_KEDONTHUOC.LaythongtinDonthuoc_In(presID);

                DataRow[] arrDR = v_dtDataOrg.Select("tuvan_them=0");
                if (arrDR.Length <= 0)
                {
                    PrintTuvanthem(presID, forcedTitle, v_dtDataOrg);
                    return;
                }
                DataTable v_dtData = arrDR.CopyToDataTable();


                Utility.AddColumToDataTable(ref v_dtData, "BarCode", typeof(byte[]));
                int Pres_ID = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA5.xml");
                byte[] Barcode = null;
                Utility.CreateBarcodeData(ref v_dtData, objLuotkham.MaLuotkham, ref Barcode);
                string ICD_Name = "";
                string ICD_Code = "";
                string chan_doan = "";
                if (v_dtData != null && v_dtData.Rows.Count > 0)
                    GetChanDoan(Utility.sDbnull(v_dtData.Rows[0]["mabenh_chinh"], ""),
                                Utility.sDbnull(v_dtData.Rows[0]["mabenh_phu"], ""), ref ICD_Name, ref ICD_Code);
                Utility.GetChandoanNoitru(id_benhnhan, ma_luotkham, ngay_kedon, ref ICD_Code, ref ICD_Name, ref chan_doan);
                string chandoan_ravien = Utility.sDbnull(v_dtData.Rows[0]["chandoan_ravien"]);
                foreach (DataRow drv in v_dtData.Rows)
                {
                    drv["BarCode"] = Barcode;
                    if (noitru == 0)
                        drv["chan_doan"] = Utility.sDbnull(drv["chan_doan"]).Trim() == ""
                                               ? ICD_Name
                                               : Utility.sDbnull(drv["chan_doan"]) + ";" + ICD_Name;
                    else
                        drv["chan_doan"] = string.Format("{0}, {1}", ICD_Name, chan_doan);
                    drv["ma_icd"] = ICD_Code;
                    if (donravien)
                        drv["chan_doan"] = chandoan_ravien;
                }
                //  THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA4.xml");
                v_dtData.AcceptChanges();
                // log.Info("Thuc hien in don thuoc");
                Utility.UpdateLogotoDatatable(ref v_dtData);
                List<string> lstmatinhchat = (from p in v_dtData.AsEnumerable()
                                              select Utility.sDbnull(p["ma_tinhchat"], "")).Distinct().ToList<string>();
                foreach (string ma_tinhchat in lstmatinhchat)
                {
                    DataRow[] arrTemp= v_dtData.Select(string.Format("(ma_tinhchat='{0}' or ma_tinhchat is null) and printed=0", ma_tinhchat));
                    DataTable v_PrintData = v_dtData.Clone();
                    if (arrTemp.Length > 0) v_PrintData = arrTemp.CopyToDataTable();//Chắc chắn có dữ liệu nên hàm copy ko bị lỗi
                    if (v_PrintData.Rows.Count <= 0) continue;
                    //Lấy danh sách các reportcode của từng tính chất thuốc
                    string report_code = Utility.sDbnull(v_PrintData.Rows[0]["report_code"], "DONTHUOC_THUONG");
                    //Lấy lại dữ liệu của tất cả các thuốc có cùng report nhưng khác tính chất để in đảm bảo ko bị tách đơn
                    v_PrintData = v_dtData.Select(string.Format("report_code='{0}' and printed=0", report_code)).CopyToDataTable();
                    //Đánh dấu trạng thái đã in để tránh in lại ở vòng for tính chất
                    (from p in v_dtData.AsEnumerable() where Utility.sDbnull(p["report_code"], "") == report_code select p).ToList().ForEach(x => x["printed"] = 1);

                    List<string> lstReportCode = v_PrintData.Rows[0]["report_code"].ToString().Split('@')[0].Split(';').ToList<string>();
                    if (lstReportCode.Count <= 0) lstReportCode.Add("thamkham_InDonthuocA4");
                    foreach (string _rcode in lstReportCode)
                    {
                        string KhoGiay = "A100";// "A5";//Truyền giá trị này để giữ nguyên report
                        if (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4) KhoGiay = "A4";
                        var reportDocument = new ReportDocument();
                        string tieude = "", reportname = "", reportCode = "";
                        reportCode = _rcode;
                        reportDocument = Utility.GetReport(reportCode, KhoGiay, ref tieude, ref reportname);
                        if (reportDocument == null)
                        {
                            //Lấy mặc định do chưa được khai báo trong danh mục tính chất thuốc
                            switch (KhoGiay)
                            {
                                case "A5":
                                    reportCode = "thamkham_InDonthuocA5";
                                    reportDocument = Utility.GetReport("thamkham_InDonthuocA5", ref tieude, ref reportname);
                                    break;
                                case "A4":
                                    reportCode = "thamkham_InDonthuocA4";
                                    reportDocument = Utility.GetReport("thamkham_InDonthuocA4", ref tieude, ref reportname);
                                    break;
                                default:
                                    reportCode = "thamkham_InDonthuocA5";
                                    reportDocument = Utility.GetReport("thamkham_InDonthuocA5", ref tieude, ref reportname);
                                    break;
                            }
                        }
                        if (reportDocument == null) return;
                        if (Utility.DoTrim(forcedTitle).Length > 0)
                            tieude = forcedTitle;
                        Utility.WaitNow(this);
                        ReportDocument crpt = reportDocument;
                        frmPrintPreview objForm = new frmPrintPreview("IN ĐƠN THUỐC BỆNH NHÂN", crpt, true, true);
                        objForm.nguoi_thuchien = Utility.sDbnull(v_dtData.Rows[0]["ten_bacsikedon"], "");
                        try
                        {
                            objForm.NGAY = ngay_kedon;
                            objForm.mv_sReportFileName = Path.GetFileName(reportname);
                            objForm.mv_sReportCode = reportCode;
                            crpt.SetDataSource(v_PrintData);
                            Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                            Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                            Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                            Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                            Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                            Utility.SetParameterValue(crpt, "ReportTitle", "ĐƠN THUỐC");
                            Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                            Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                            objForm.crptViewer.ReportSource = crpt;
                            if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                                                       PropertyLib._MayInProperties.PreviewInDonthuoc))
                            {
                                objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                                objForm.ShowDialog();
                                cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                            }
                            else
                            {
                                objForm.addTrinhKy_OnFormLoad();
                                crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                                crpt.PrintToPrinter(1, false, 0, 0);
                            }

                            Utility.DefaultNow(this);
                        }
                        catch (Exception ex)
                        {
                            Utility.DefaultNow(this);
                        }
                        finally
                        {

                        }
                    }//Kết thúc vòng for qua các liên trong tính chất
                }//Kết thúc vòng for tính chất
                //In đơn tư vấn thêm(nếu có)
                PrintTuvanthem(presID, forcedTitle, v_dtDataOrg);
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }

        }
        private void PrintTuvanthem(int presID, string forcedTitle, DataTable p_dtData)
        {

            DataRow[] arrDR = p_dtData.Select("tuvan_them=1");
            if (arrDR.Length <= 0) return;
            DataTable v_dtData = arrDR.CopyToDataTable();
            Utility.AddColumToDataTable(ref v_dtData, "BarCode", typeof(byte[]));
            int Pres_ID = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA5.xml");
            byte[] Barcode = null;
            Utility.CreateBarcodeData(ref v_dtData, objLuotkham.MaLuotkham, ref Barcode);
            string ICD_Name = "";
            string ICD_Code = "";
            if (v_dtData != null && v_dtData.Rows.Count > 0)
                GetChanDoan(Utility.sDbnull(v_dtData.Rows[0]["mabenh_chinh"], ""),
                            Utility.sDbnull(v_dtData.Rows[0]["mabenh_phu"], ""), ref ICD_Name, ref ICD_Code);

            foreach (DataRow drv in v_dtData.Rows)
            {
                drv["BarCode"] = Barcode;
                drv["chan_doan"] = Utility.sDbnull(drv["chan_doan"]).Trim() == ""
                                       ? ICD_Name
                                       : Utility.sDbnull(drv["chan_doan"]) + ";" + ICD_Name;
                drv["ma_icd"] = ICD_Code;
            }
            //  THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA4.xml");
            v_dtData.AcceptChanges();
            // log.Info("Thuc hien in don thuoc");
            Utility.UpdateLogotoDatatable(ref v_dtData);
            string KhoGiay = "A5";
            if (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4) KhoGiay = "A4";
            var reportDocument = new ReportDocument();
            string tieude = "", reportname = "", reportCode = "";
            switch (KhoGiay)
            {
                case "A5":
                    reportCode = "thamkham_InDonTuvanA4";
                    reportDocument = Utility.GetReport("thamkham_InDonTuvanA4", ref tieude, ref reportname);
                    break;
                case "A4":
                    reportCode = "thamkham_InDonTuvanA4";
                    reportDocument = Utility.GetReport("thamkham_InDonTuvanA4", ref tieude, ref reportname);
                    break;
                default:
                    reportCode = "thamkham_InDonTuvanA4";
                    reportDocument = Utility.GetReport("thamkham_InDonTuvanA4", ref tieude, ref reportname);
                    break;
            }
            if (reportDocument == null) return;
            if (Utility.DoTrim(forcedTitle).Length > 0)
                tieude = forcedTitle;
            Utility.WaitNow(this);
            ReportDocument crpt = reportDocument;
            frmPrintPreview objForm = new frmPrintPreview("IN ĐƠN TƯ VẤN", crpt, true, true);
            objForm.nguoi_thuchien = Utility.sDbnull(v_dtData.Rows[0]["ten_bacsikedon"], "");
            try
            {
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                crpt.SetDataSource(v_dtData);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "ReportTitle", tieude);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                                           PropertyLib._MayInProperties.PreviewInDonthuoc))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    objForm.ShowDialog();
                    cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.DefaultNow(this);
            }
        }
        #endregion

        #region "Xử lý tác vụ của phần lưu thông tin "

        private void cmdSave_Click(object sender, EventArgs e)
        {
            PerformAction();
        }

        string GetDanhsachBenhphu()
        {
            var sMaICDPHU = new StringBuilder("");
            try
            {
                int recordRow = 0;

                foreach (DataRow row in dt_ICD_PHU.Rows)
                {
                    if (recordRow > 0)
                        sMaICDPHU.Append(",");
                    sMaICDPHU.Append(Utility.sDbnull(row[DmucBenh.Columns.MaBenh], ""));
                    recordRow++;
                }
                return sMaICDPHU.ToString();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// HÀM KHƠI TẠO PHẦN CHỈ ĐỊNH CHUẨN ĐOÁN
        /// </summary>
        /// <returns></returns>
        private KcbChandoanKetluan CreateDiagInfo()
        {
            if (_KcbChandoanKetluan == null)
            {
                _KcbChandoanKetluan = new KcbChandoanKetluan();
                _KcbChandoanKetluan.IsNew = true;
            }
            else
            {
                _KcbChandoanKetluan.IsNew = false;
                _KcbChandoanKetluan.MarkOld();
            }
            _KcbChandoanKetluan.IdKham = Utility.Int64Dbnull(txtExam_ID.Text, -1);
            _KcbChandoanKetluan.MaLuotkham = Utility.sDbnull(malankham, "");
            _KcbChandoanKetluan.IdBenhnhan = Utility.Int64Dbnull(txtPatient_ID.Text, "-1");
            _KcbChandoanKetluan.MabenhChinh = Utility.sDbnull(txtMaBenhChinh.MyCode, "");
            _KcbChandoanKetluan.Nhommau = txtNhommau.Text;
            _KcbChandoanKetluan.Nhietdo = Utility.sDbnull(txtNhietDo.Text);
            _KcbChandoanKetluan.Huyetap = txtHa.Text;
            _KcbChandoanKetluan.Mach = txtMach.Text;
            _KcbChandoanKetluan.Nhiptim = Utility.sDbnull(txtNhipTim.Text);
            _KcbChandoanKetluan.Nhiptho = Utility.sDbnull(txtNhipTho.Text);
            _KcbChandoanKetluan.Chieucao = Utility.sDbnull(txtChieucao.Text);
            _KcbChandoanKetluan.Cannang = Utility.sDbnull(txtCannang.Text);
            _KcbChandoanKetluan.HuongDieutri = "";
            _KcbChandoanKetluan.SongayDieutri = 0;
            _KcbChandoanKetluan.Ketluan = "";
            if (cboBSDieutri.SelectedIndex > 0)
                _KcbChandoanKetluan.IdBacsikham = Utility.Int16Dbnull(cboBSDieutri.SelectedValue, -1);
            else
            {
                _KcbChandoanKetluan.IdBacsikham = globalVariables.gv_intIDNhanvien;
            }
            string sMaICDPHU = GetDanhsachBenhphu();
            _KcbChandoanKetluan.MabenhPhu = Utility.sDbnull(sMaICDPHU.ToString(), "");
            if (objPhieudieutri != null)
            {
                _KcbChandoanKetluan.IdPhongkham = Utility.Int32Dbnull(objPhieudieutri.IdKhoanoitru, -1);
                DmucKhoaphong objDepartment = DmucKhoaphong.FetchByID(Utility.Int32Dbnull(objPhieudieutri.IdKhoanoitru, -1));
                if (objDepartment != null)
                {
                    _KcbChandoanKetluan.TenPhongkham = Utility.sDbnull(objDepartment.TenKhoaphong, "");
                }
                _KcbChandoanKetluan.IdPhongkham = Utility.Int32Dbnull(objPhieudieutri.IdKhoanoitru);
            }
            else
            {
                _KcbChandoanKetluan.IdPhongkham = globalVariables.idKhoatheoMay;
            }
            _KcbChandoanKetluan.IdKham = Utility.Int32Dbnull(txtIdPhieudieutri.Text, -1);
            _KcbChandoanKetluan.NgayTao = dtpCreatedDate.Value;
            _KcbChandoanKetluan.NguoiTao = globalVariables.UserName;
            _KcbChandoanKetluan.NgayChandoan = dtpCreatedDate.Value;
            _KcbChandoanKetluan.Ketluan = "";
            _KcbChandoanKetluan.Chandoan = Utility.ReplaceString(txtChanDoan.Text);
            _KcbChandoanKetluan.ChandoanKemtheo = Utility.sDbnull(txtChanDoanKemTheo.Text);

            _KcbChandoanKetluan.Noitru = (byte)1;
            return _KcbChandoanKetluan;
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của dữ liệu trước khi đóng gói dữ liệu vào Entity
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            errorProvider1.Clear();
            Utility.SetMsg(lblMsg, "", false);
            if (string.IsNullOrEmpty(txtPatient_Code.Text))
            {
                Utility.ShowMsg("Bạn phải chọn Bệnh nhân để thực hiện lập phiếu điều trị");
               // errorProvider1.SetError(txtPatient_Code, lblMsg.Text);
                txtPatient_Code.Focus();
                return false;
            }
            KcbLuotkham _item = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(malankham)
                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(txtPatient_ID.Text).ExecuteSingle<KcbLuotkham>();
            if (_item==null)
            {
                Utility.ShowMsg("Bạn phải chọn Bệnh nhân hoặc bệnh nhân không tồn tại!", "Thông báo",
                                MessageBoxIcon.Warning);
                txtPatient_Code.Focus();
                return false;
            }
          
            
           

            if (objLuotkham != null && objLuotkham.MaDoituongKcb == "BHYT" && Utility.DoTrim( txtMaBenhChinh.MyCode)=="-1")
            {
                Utility.ShowMsg("Bạn cần nhập Mã bệnh chính cho đối tượng BHYT");
                tabDiagInfo.SelectedTab = TabPageChanDoan;
                txtMaBenhChinh.Focus();
                return false;
            }
            
            return true;
        }
        void TinhtoanTongchiphi()
        {
            try
            {
                DataSet dsData = SPs.NoitruTongchiphi(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan).GetDataSet();
                decimal Tong_CLS = Utility.DecimaltoDbnull(dsData.Tables[0].Compute("SUM(TT_BN)", "1=1"));
                decimal Tong_Thuoc = Utility.DecimaltoDbnull(dsData.Tables[1].Compute("SUM(TT_BN)", "1=1"));
                decimal Tong_VTTH = Utility.DecimaltoDbnull(dsData.Tables[2].Compute("SUM(TT_BN)", "1=1"));
                decimal Tong_Giuong = Utility.DecimaltoDbnull(dsData.Tables[3].Compute("SUM(TT_BN)", "1=1"));
                decimal Tong_Goi = Utility.DecimaltoDbnull(dsData.Tables[4].Compute("SUM(TT_BN)", "1=1"));
                decimal Tong_Tamung = Utility.DecimaltoDbnull(m_dtTamung.Compute("SUM(so_tien)", "1=1"));
                decimal Tong_chiphi = Tong_CLS + Tong_Thuoc + Tong_Giuong + Tong_Goi + Tong_VTTH;

                lblCLS.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(Tong_CLS.ToString()));
                lblThuoc.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(Tong_Thuoc.ToString()));
                lblVTTH.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(Tong_VTTH.ToString()));
                lblBuonggiuong.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(Tong_Giuong.ToString()));
                lblDichvu.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(Tong_Goi.ToString()));
                lblTongChiphi.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(Tong_chiphi.ToString()));
                string canhbaotamung = THU_VIEN_CHUNG.Canhbaotamung(objLuotkham, dsData, m_dtTamung);
                lblWarning.Text = canhbaotamung;
                //if (THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_CANHBAOTAMUNG_PHIEUDIEUTRI", "1", false) == "1")
                //{
                //    if (canhbaotamung.Trim() != "")
                //        ShowErrorStatus(canhbaotamung);
                //    else
                //    {
                //        if (ucError1 != null)
                //        {
                //            UIAction._Visible(ucError1, false);
                //            ucError1.Reset();
                //        }
                //    }
                //}
                Utility.SetMsg(lblChenhlech, "Tổng tạm ứng - Tổng chi phí = " + String.Format(Utility.FormatDecimal(), Convert.ToDecimal((Tong_Tamung - Tong_chiphi).ToString())), Tong_Tamung - Tong_chiphi > 0 ? false : true);
                lblCLS.Text = Utility.DoTrim(lblCLS.Text) == "" ? "0" : lblCLS.Text;
                lblThuoc.Text = Utility.DoTrim(lblThuoc.Text) == "" ? "0" : lblThuoc.Text;
                lblVTTH.Text = Utility.DoTrim(lblVTTH.Text) == "" ? "0" : lblVTTH.Text;
                lblBuonggiuong.Text = Utility.DoTrim(lblBuonggiuong.Text) == "" ? "0" : lblBuonggiuong.Text;
                lblDichvu.Text = Utility.DoTrim(lblDichvu.Text) == "" ? "0" : lblDichvu.Text;
                lblTongChiphi.Text = Utility.DoTrim(lblTongChiphi.Text) == "" ? "0" : lblTongChiphi.Text;
                //lblChenhlech.Text = Utility.DoTrim(lblChenhlech.Text) == "" ? "Tổng tạm ứng - Tổng chi phí =0" : lblTongChiphi.Text;
            }
            catch (Exception ex)
            {

                
            }
        }
        void HideErrorStatus()
        {
            try
            {
                UIAction._Visible(ucError1, false);
                ucError1.Reset();
            }
            catch
            {
            }
        }
        void ShowErrorStatus(string msg)
        {
            try
            {
                UIAction._Visible(ucError1, true);
                ucError1.Reset();
                ucError1.Start(msg);


            }
            catch
            {
            }
        }
        /// <summary>
        /// Thực hiện thao tác Insert,Update,Delete tới CSDL theo m_enAction
        /// </summary>
        private void PerformAction()
        {
            try
            {
                ////Kiểm tra tính hợp lệ của dữ liệu trước khi thêm mới
                //if (!IsValidData())
                //{
                //    return;
                //}
                //objPhieudieutri.TrangThai = (byte?) (chkDaThucHien.Checked ? 1 : 0);
                //DataRow[] arrDr = m_dtPatients.Select("id_kham=" + txtIdPhieudieutri.Text);
                //if (arrDr.Length > 0)
                //{

                //    arrDr[0]["trang_thai"] = chkDaThucHien.Checked ? 1 : 0;
                //}
                //objPhieudieutri.IdBacsikham = Utility.Int16Dbnull(cboDoctorAssign.SelectedValue, -1);
                //if (!THU_VIEN_CHUNG.IsBaoHiem((byte) objLuotkham.IdLoaidoituongKcb))//Đối tượng dịch vụ được khóa ngay sau khi kết thúc khám
                //{
                //    objLuotkham.NguoiKetthuc = globalVariables.UserName;
                //    objLuotkham.NgayKetthuc = globalVariables.SysDate;
                //    objLuotkham.Locked = 1;
                //}
                //ActionResult actionResult =
                //   _KCB_THAMKHAM.UpdateExamInfo(
                //         CreateDiagInfo(), objPhieudieutri, objLuotkham);
                //switch (actionResult)
                //{
                //    case ActionResult.Success:
                //        IEnumerable<GridEXRow> query = from kham in grdList.GetDataRows()
                //                                       where
                //                                           kham.RowType == RowType.Record &&
                //                                           Utility.Int32Dbnull(kham.Cells[KcbDangkyKcb.Columns.IdKham].Value) ==
                //                                           Utility.Int32Dbnull(txtIdPhieudieutri.Text)
                //                                       select kham;
                //        if (query.Count() > 0)
                //        {
                //            GridEXRow gridExRow = query.FirstOrDefault();
                //            //gridExRow.BeginEdit();
                //            gridExRow.Cells[KcbDangkyKcb.Columns.TrangThai].Value = (byte?) (chkDaThucHien.Checked ? 1 : 0);
                //            gridExRow.Cells[KcbLuotkham.Columns.NguoiKetthuc].Value = globalVariables.UserName;
                //            gridExRow.Cells[KcbLuotkham.Columns.NgayKetthuc].Value = globalVariables.SysDate;
                //            //gridExRow.EndEdit();
                //            grdList.UpdateData();
                //            Utility.GotoNewRowJanus(grdList, KcbDangkyKcb.Columns.IdKham, Utility.sDbnull(txtIdPhieudieutri.Text));
                //        }
                //        cmdInphieudieutri.Visible = true;
                //        cboChonBenhAn.Visible = true && THU_VIEN_CHUNG.Laygiatrithamsohethong("BENH_AN", "0", true) == "1"; ;
                //        lblBenhan.Visible = cboChonBenhAn.Visible;
                //        chkDaThucHien.Checked = true;

                //        //Tự động ẩn BN về tab đã khám
                //        int Status = radChuaKham.Checked ? 0 : 1;
                //        if (Status == 0)
                //        {
                //            m_dtPatients.DefaultView.RowFilter = "1=1";
                //            m_dtPatients.DefaultView.RowFilter = "trang_thai=" + Status;
                //        }
                //        if (objLuotkham.Locked == 1)//Đối tượng dịch vụ được khóa ngay sau khi kết thúc khám
                //        {
                //            cmdUnlock.Visible = true;
                //            cmdUnlock.Enabled = true;
                //        }
                //        DmucNhanvien objStaff = new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.UserNameColumn).IsEqualTo(Utility.sDbnull(objLuotkham.NguoiKetthuc, "")).ExecuteSingle<DmucNhanvien>();
                //        string TenNhanvien = objLuotkham.NguoiKetthuc;
                //        if (objStaff != null)
                //            TenNhanvien = objStaff.TenNhanvien;
                //        if (!cmdUnlock.Enabled)
                //            toolTip1.SetToolTip(cmdUnlock, "Bạn không có quyền mở khóa Bệnh nhân này. Đề nghị liên hệ " + TenNhanvien + "(" + objLuotkham.NguoiKetthuc + " - Là người khóa BN này) để được họ mở khóa. Hoặc liên hệ Quản trị hệ thống");
                //        else
                //            toolTip1.SetToolTip(cmdUnlock, "Nhấn vào đây để mở khóa cho bệnh nhân đang chọn(Phím tắt Ctrl+U). Điều kiện là chỉ mở khóa đối với đối tượng Dịch vụ. Muốn mở khóa đối tượng BHYT thì cần liên lạc với bộ phận thanh toán hủy in phôi BHYT");
                //        break;
                //    case ActionResult.Error:
                //        Utility.ShowMsg("Lỗi trong quá lưu thông tin ", "Thông báo lỗi", MessageBoxIcon.Error);
                //        break;
                //}
                //ModifyCommmands();
                //cmdNhapVien.Enabled = objPhieudieutri.TrangThai == 1;
                //cmdHuyNhapVien.Enabled = objLuotkham.TrangthaiNoitru >= 1;
                //cmdNhapVien.Tag = objLuotkham.TrangthaiNoitru == 0 ? "0" : "1";
                //cmdNhapVien.Text = objLuotkham.TrangthaiNoitru == 0 ? "Nhập viện" : "Cập nhật";
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
        }

        #endregion

        private void tabDiagInfo_SelectedTabChanged(object sender, TabEventArgs e)
        {

        }

        private void chkIntach_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIntach.Checked)
            {
                File.WriteAllText(Application.StartupPath + "\\CAUHINH\\chkintachphieu.txt", "1");
            }
            else
            {
                File.WriteAllText(Application.StartupPath + "\\CAUHINH\\chkintachphieu.txt", "0");
            }
        }
        private void HienThiChuyenCan()
        {
            int idChidinh = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
            if (m_dtAssignDetail.Select("trangthai_chitiet >3 and id_chidinh = '" + idChidinh + "'").Length > 0)
            {
                cmdConfirm.Enabled = false;
                cmdConfirm.Text = @"Đã có kết quả";
                cmdConfirm.Tag = 3;
            }
            if (m_dtAssignDetail.Select("trangthai_chuyencls in(1,2,3) and id_chidinh = '" + idChidinh + "'").Length >
                0)
            {
                // cmdConfirm.Enabled = Utility.Coquyen("coquyenhuyketnoihislis");
                cmdConfirm.Text = @"Hủy xác nhận";
                cmdConfirm.Tag = 2;
            }
            if (m_dtAssignDetail.Select("trangthai_chuyencls=0 and id_chidinh = '" + idChidinh + "'").Length > 0)
            {
                cmdConfirm.Enabled = true;
                cmdConfirm.Text = @"Xác nhận";
                cmdConfirm.Tag = 1;
            }
        }
        private void cmdXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmdConfirm.Tag.ToString() == "1")
                {
                    int id_chidinh =
                        Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                    DataRow[] arrDr =
                        m_dtAssignDetail.Select("trangthai_chuyencls=0 and id_chidinh = '" + id_chidinh + "' ");
                    if (arrDr.Length == 0)
                    {
                        Utility.ShowMsg("Các chỉ định CLS đã được chuyển hết");
                        Utility.DefaultNow(this);
                        return;
                    }
                    else
                    {
                        foreach (DataRow dr in arrDr)
                        {
                            dr["trangthai_chuyencls"] = 1;
                        }
                        m_dtAssignDetail.AcceptChanges();

                        SPs.KcbThamkhamUpdatetrangthaiXacnhanchidinh(Utility.Int64Dbnull(objLuotkham.IdBenhnhan),
                            Utility.sDbnull(objLuotkham.MaLuotkham), Utility.Int64Dbnull(id_chidinh),
                            globalVariables.SysDate, globalVariables.UserName).Execute();
                        Utility.ShowMsg("Bạn vừa chuyển CLS thành công");
                    }
                }
                else
                {
                    foreach (GridEXRow row in grdAssignDetail.GetCheckedRows())
                    {
                        int id_chidinh =
                            Utility.Int32Dbnull(row.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value, -1);
                        DataRow[] arrDr =
                            m_dtAssignDetail.Select("trangthai_chuyencls in (1,2,3) and id_chidinh = '" +
                                                    id_chidinh + "'");
                        foreach (DataRow dr in arrDr)
                        {
                            dr["trangthai_chuyencls"] = 0;
                        }
                        m_dtAssignDetail.AcceptChanges();
                        SPs.KcbThamkhamUpdatetrangthaiHuyxacnhanchidinh(Utility.Int64Dbnull(objLuotkham.IdBenhnhan),
                            Utility.sDbnull(objLuotkham.MaLuotkham), Utility.Int64Dbnull(id_chidinh),
                            globalVariables.SysDate, globalVariables.UserName).Execute();

                        Utility.ShowMsg("Bạn vừa hủy chuyển CLS thành công");
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                HienThiChuyenCan();
            }
        }

        private void cmdthemphieudieutri_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdCreateNewPres_Click_1(object sender, EventArgs e)
        {

        }

        private void uiCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            pnlChonCongkham.Visible = uiCheckBox1.Checked;
            pnlKieukham.Visible = !pnlChonCongkham.Visible;
            if (uiCheckBox1.Checked)
            {
                txtExamtypeCode.Focus();
                txtExamtypeCode.SelectAll();
            }
            else
            {
                txtKieuKham.Focus();
                txtKieuKham.SelectAll();
            }
            Utility.SaveUserConfig(uiCheckBox1.Tag.ToString(), Utility.Bool2byte(uiCheckBox1.Checked));
        }
        private void cboKhoanoitru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnHasLoaded) return;
                Selectionchanged();
        }

        private void cmdIntachPhieu_Click(object sender, EventArgs e)
        {
            ChoninphieuCLS(false);
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            ChoninphieuCLS(true);
        }

        void ChoninphieuCLS(bool inchung)
        {
            try
            {
                if (grdAssignDetail.GetDataRows().Length <= 0 || RowCLS == null)
                {
                    Utility.ShowMsg("");
                    return;
                }
                DataRow[] arrDr = m_dtAssignDetail.Select("id_chidinh=" + RowCLS.Cells["id_chidinh"].Value.ToString());
                DataTable dtTempData = m_dtAssignDetail.Clone();
                if (arrDr.Length > 0)
                    dtTempData = arrDr.CopyToDataTable();
                frm_InphieuCLS _InphieuCLS = new frm_InphieuCLS(dtTempData, Utility.getUserConfigValue("CHIDINHCLS_TUDONGCHONCACPHIEUKHI_INTACH", (byte)1) == 1);
                _InphieuCLS.objLuotkham = this.objLuotkham;
                if (!inchung)
                    _InphieuCLS.ShowDialog();
                else
                {
                    _InphieuCLS.InChungphieu();
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
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
                //}
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void mnuXoacongkham_Click(object sender, EventArgs e)
        {
            cmdXoaCongkham.PerformClick();
        }

        private void mnuBotinhtien_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdCongkham))
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất 1 công khám chuyên khoa trên lưới để thực hiện chức năng hủy tính chi phí. Vui lòng chọn lại");
                    return;
                }
                Int16 noitru = Utility.Int16Dbnull(grdCongkham.GetValue("noitru"));
                if (noitru == 0)
                {
                    Utility.ShowMsg("Công khám bạn chọn phát sinh từ khoa ngoại trú nên không được phép thay đổi. Vui lòng chọn công khám chuyên khoa kê từ các khoa nội trú");
                    return;
                }
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn hủy tính tiền cho công khám đang chọn.Chú ý: Chỉ các công khám chuyên khoa được kê từ chức năng Quản lý phiếu điều trị mới được phép hủy tính chi phí", "Xác nhận", true))
                {
                   long id_kham =Utility.Int64Dbnull(grdCongkham.GetValue("id_kham"));
                   int ra = new Update(KcbDangkyKcb.Schema)
                   .Set(KcbDangkyKcb.Columns.TinhChiphi).EqualTo(0)
                       .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(id_kham).Execute();
                   if (ra > 0)
                   {
                       Utility.ShowMsg(string.Format("Đã cập nhật không tính chi phí cho công khám {0} thành công. Nhấn OK để kết thúc", grdCongkham.GetValue("ten_dichvukcb")));
                       DataRow dr = ((DataRowView)grdCongkham.CurrentRow.DataRow).Row;
                       dr["tinh_chiphi"] = 0;
                       dr.Table.AcceptChanges();
                   }

                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            
        }

        private void mnuTinhtien_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdCongkham))
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất 1 công khám chuyên khoa trên lưới để thực hiện chức năng tính chi phí. Vui lòng chọn lại");
                    return;
                }
                Int16 noitru = Utility.Int16Dbnull(grdCongkham.GetValue("noitru"));
                if (noitru == 0)
                {
                    Utility.ShowMsg("Công khám bạn chọn phát sinh từ khoa ngoại trú nên không được phép thay đổi. Vui lòng chọn công khám chuyên khoa kê từ các khoa nội trú");
                    return;
                }
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn tính tiền cho công khám đang chọn.Chú ý: Chỉ các công khám chuyên khoa được kê từ chức năng Quản lý phiếu điều trị mới dùng được chức năng này", "Xác nhận", true))
                {
                    long id_kham = Utility.Int64Dbnull(grdCongkham.GetValue("id_kham"));
                    
                    int ra = new Update(KcbDangkyKcb.Schema)
                    .Set(KcbDangkyKcb.Columns.TinhChiphi).EqualTo(1)
                        .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(id_kham).Execute();
                    if (ra > 0)
                    {
                        Utility.ShowMsg(string.Format("Đã cập nhật tính chi phí cho công khám {0} thành công. Nhấn OK để kết thúc", grdCongkham.GetValue("ten_dichvukcb")));
                        DataRow dr = ((DataRowView)grdCongkham.CurrentRow.DataRow).Row;
                        dr["tinh_chiphi"] = 1;
                        dr.Table.AcceptChanges();
                    }

                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void mnuShowResult_Click_1(object sender, EventArgs e)
        {

        }

        private void cdViewPDF_Click(object sender, EventArgs e)
        {
            if (RowCLS == null || objLuotkham == null || objPhieudieutri == null) return;
            frm_PdfViewer _PdfViewer = new frm_PdfViewer(1);
            _PdfViewer.ma_luotkham = objLuotkham.MaLuotkham;
            _PdfViewer.ma_chidinh = Utility.sDbnull(RowCLS.Cells[KcbChidinhcl.Columns.MaChidinh].Value);
            _PdfViewer.ShowDialog();
        }

     
        private void PrintPresGop(string maLanKham,int id_donthuoc, string forcedTitle)
        {
            DataTable v_dtData = _KCB_KEDONTHUOC.LaythongtinDonthuoc_In(id_donthuoc); ;// _KCB_KEDONTHUOC.LaythongtinDonthuoc_InGop(maLanKham);
            Utility.AddColumToDataTable(ref v_dtData, "BarCode", typeof(byte[]));
            int Pres_ID = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA5.xml");
            byte[] Barcode = null;
            Utility.CreateBarcodeData(ref v_dtData, objLuotkham.MaLuotkham, ref Barcode);
            string ICD_Name = "";
            string ICD_Code = "";
            if (v_dtData != null && v_dtData.Rows.Count > 0)
                GetChanDoan(Utility.sDbnull(v_dtData.Rows[0]["mabenh_chinh"], ""),
                            Utility.sDbnull(v_dtData.Rows[0]["mabenh_phu"], ""), ref ICD_Name, ref ICD_Code);

            foreach (DataRow drv in v_dtData.Rows)
            {
                drv["BarCode"] = Barcode;
                drv["chan_doan"] = Utility.sDbnull(drv["chan_doan"]).Trim() == ""
                                       ? ICD_Name
                                       : Utility.sDbnull(drv["chan_doan"]) + ";" + ICD_Name;
                drv["ma_icd"] = ICD_Code;
            }
            //  THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA4.xml");
            v_dtData.AcceptChanges();
            // log.Info("Thuc hien in don thuoc");
            Utility.UpdateLogotoDatatable(ref v_dtData);
            string KhoGiay = "A5";
            if (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4) KhoGiay = "A4";
            var reportDocument = new ReportDocument();
            string tieude = "", reportname = "", reportCode = "";
            switch (KhoGiay)
            {
                case "A5":
                    reportCode = "thamkham_InDonthuocA5";
                    reportDocument = Utility.GetReport("thamkham_InDonthuocA5", ref tieude, ref reportname);
                    break;
                case "A4":
                    reportCode = "thamkham_InDonthuocA4";
                    reportDocument = Utility.GetReport("thamkham_InDonthuocA4", ref tieude, ref reportname);
                    break;
                default:
                    reportCode = "thamkham_InDonthuocA5";
                    reportDocument = Utility.GetReport("thamkham_InDonthuocA5", ref tieude, ref reportname);
                    break;
            }
            if (reportDocument == null) return;
            if (Utility.DoTrim(forcedTitle).Length > 0)
                tieude = forcedTitle;
            Utility.WaitNow(this);
            ReportDocument crpt = reportDocument;
            var objForm = new frmPrintPreview("IN ĐƠN THUỐC BỆNH NHÂN", crpt, true, true);
            try
            {
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                crpt.SetDataSource(v_dtData);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "ReportTitle", "ĐƠN THUỐC");
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                                           PropertyLib._MayInProperties.PreviewInDonthuoc))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    objForm.ShowDialog();
                    cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.DefaultNow(this);
            }
        }
        private void cmdInphieuVT_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdInPhieutruyendich_Click(object sender, EventArgs e)
        {
            try
            {

                if (RowThuoc != null)
                {
                    string ID_Phieu = string.Join(",", (from p in grdPresDetail.GetCheckedRows() select p.Cells["id_phieu"].Value.ToString()).Distinct().ToArray<string>());
                    string id_thuoc = string.Join(",", (from p in grdPresDetail.GetCheckedRows() select p.Cells["id_thuoc"].Value.ToString()).Distinct().ToArray<string>());

                    foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
                    {
                        gridExRow.BeginEdit();
                        gridExRow.Cells[NoitruPhieudichtruyen.Columns.TrangthaiIn].Value = 1;
                        gridExRow.EndEdit();
                    }
                    grdPresDetail.UpdateData();
                    DataTable dt_dataprint = SPs.NoitruLayThongTinThuocTruyenDichIntam(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objPhieudieutri.IdPhieudieutri, Utility.Int64Dbnull(RowThuoc.Cells["id_donthuoc"].Value)).GetDataSet().Tables[0];

                    if (dt_dataprint.Rows.Count <= 0)
                    {
                        Utility.ShowMsg("Không tìm thấy dữ liệu để in", "Thông báo");
                        return;
                    }
                    //THU_VIEN_CHUNG.CreateXML(dt_dataprint, Application.StartupPath + @"\Xml4Reports\noitru_phieutruyendich.XML");
                    if (dt_dataprint.Rows.Count <= 0)
                    {
                        Utility.ShowMsg("Không tìm thấy thông tin phiếu truyền dịch để in. Vui lòng chọn một phiếu trên danh sách phiếu truyền dịch trước khi nhấn nút in", "Thông báo", MessageBoxIcon.Error);
                        return;
                    }
                    noitru_inphieu.InPhieutheodoi(dt_dataprint, true, "noitru_phieutruyendich", "");
                }
                else
                {
                    Utility.ShowMsg("Bạn cần chọn đơn thuốc có chứa thuốc dịch truyền. Vui lòng kiểm tra lại");
                }

            }
            catch (Exception exception)
            {
                Utility.CatchException(exception);
            }
        }

        private void cmdIndonthuoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdPresDetail)) return;
                int Pres_ID = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong_off("NOITRU_PHIEUDIEUTRI_DONTHUOC_INTACH", "0", false) == "1")
                {
                    if (RowThuoc == null)
                    {
                        Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện việc xóa thông tin thuốc ", "Thông báo", MessageBoxIcon.Warning);
                        grdPresDetail.Focus();
                        return;
                    }
                    int presId = Utility.Int32Dbnull(RowThuoc.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value);
                    PrintPres(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, 1, objPhieudieutri.NgayDieutri.Value, presId, "",false);
                }
                else
                {
                    string maLanKham = Utility.sDbnull(txtPatient_Code.Text.Trim());
                    PrintPresGop(objLuotkham.MaLuotkham,Pres_ID, "");
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
                // throw;
            }
        }

        private void cmdPhieucongkhai_Click(object sender, EventArgs e)
        {
            ctxCongkhai.Show(cmdPhieucongkhai, new Point(0, cmdPhieucongkhai.Height));
        }

        private void cmdSuadonthuocravien_Click(object sender, EventArgs e)
        {
            if (CheckDachuyenkhoa()) return;
            if (!CheckPatientSelected()) return;
            if (!cmdSuadonthuocravien.Enabled) return;
            if (RowThuocRavien == null)
            {
                Utility.ShowMsg("Vui lòng chọn 1 dòng thuốc trong đơn để thực hiện cập nhật đơn thuốc ra viện");
                return;
            }
            if (Utility.Coquyen("quyen_suadonthuoc") || Utility.sDbnull(Utility.getCellValuefromGridEXRow(RowThuocRavien, KcbDonthuocChitiet.Columns.NguoiTao)) == globalVariables.UserName)
            {
                UpdateDonThuoc_Ravien();
            }
            else
            {
                Utility.ShowMsg("Đơn thuốc đang chọn sửa được tạo bởi bác sĩ khác hoặc bạn không được gán quyền sửa(quyen_suadonthuoc). Vui lòng kiểm tra lại");
                return;
            }
        }

        private void cmdThemdonthuocravien_Click(object sender, EventArgs e)
        {
            if (CheckDachuyenkhoa()) return;
            if (!CheckPatientSelected()) return;
            if (!cmdThemdonthuocravien.Enabled) return;
            ThemMoiDonThuoc_ravien();

        }
        private bool IsValidThuocRavien_delete()
        {
            bool b_Cancel = false;
            if (chkShowGroup.Checked)
            {
                Utility.ShowMsg("Bạn cần bỏ check mục Nhóm thuốc khi thực hiện thao tác xóa");
                grdDonthuocravien.UnCheckAllRecords();
                return false;
            }
            if (grdDonthuocravien.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện việc xóa thông tin thuốc ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdDonthuocravien.Focus();
                return false;
            }

            KcbDonthuocChitiet objKcbDonthuocChitiet = null;
            foreach (GridEXRow gridExRow in grdDonthuocravien.GetCheckedRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    int PresDetail_ID =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                    int Drug_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1);
                    objKcbDonthuocChitiet = new Select().From(KcbDonthuocChitiet.Schema)
                        .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(PresDetail_ID)
                        .ExecuteSingle<KcbDonthuocChitiet>();
                    if (objKcbDonthuocChitiet != null)
                    {
                        if (Utility.Byte2Bool(objKcbDonthuocChitiet.TrangthaiThanhtoan))
                        {
                            Utility.ShowMsg("Bản ghi đã thanh toán, Bạn không thể xóa thông tin được ", "Thông báo",
                                            MessageBoxIcon.Warning);
                            b_Cancel = true;
                            break;
                        }
                        if (Utility.Byte2Bool(objKcbDonthuocChitiet.TrangthaiTonghop))
                        {
                            Utility.ShowMsg("Thuốc đã được tổng hợp cấp phát nội trú nên bạn không thể xóa. Vui lòng kiểm tra lại", "Thông báo",
                                            MessageBoxIcon.Warning);
                            b_Cancel = true;
                            break;
                        }
                        if (Utility.Byte2Bool(objKcbDonthuocChitiet.TrangThai))
                        {
                            Utility.ShowMsg("Thuốc đã được duyệt cấp phát nội trú nên bạn không thể xóa. Vui lòng kiểm tra lại", "Thông báo",
                                            MessageBoxIcon.Warning);
                            b_Cancel = true;
                            break;
                        }
                    }
                }
            }
            if (b_Cancel)
            {
                grdDonthuocravien.Focus();
                return false;
            }
            return true;
        }
        private void PerformActionDeletePres_ravien()
        {
            if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa các thuốc đang chọn?", "Xác nhận xóa thuốc", true))
                return;
            string s = "";
            int Pres_ID = Utility.Int32Dbnull(grdDonthuocravien.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            if (!IsValid_UpdateDonthuoc(Pres_ID, "thuốc"))
            {
                return;
            }
            var v_collect = new Select().From(KcbDonthuocChitiet.Schema.TableName)
                .Where(KcbDonthuocChitiet.TrangthaiThanhtoanColumn.ColumnName).IsEqualTo(1)
                .And(KcbDonthuocChitiet.IdDonthuocColumn.ColumnName).IsEqualTo(Pres_ID)
                .ExecuteAsCollection<KcbDonthuocChitietCollection>();
            if (v_collect.Count > 0)
            {
                Utility.ShowMsg(
                    "Đơn thuốc bạn đang chọn sửa đã được thanh toán. Muốn sửa lại Đơn thuốc Bạn cần phải liên hệ với bộ phận Thanh toán để hủy thanh toán và Bộ phận cấp vật tư để hủy xác nhận thuốc tại kho thuốc");
                return;
            }
            List<int> lstIdchitiet = new List<int>();
            foreach (GridEXRow gridExRow in grdDonthuocravien.GetCheckedRows())
            {
                string stempt = "";
                int id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, 0m);
                decimal dongia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, 0m);
                int IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value, 0m);
                List<int> _temp = GetIdChitiet(m_dtDonthuoc_ravien, IdDonthuoc, id_thuoc, dongia, ref stempt);
                s += "," + stempt;
                lstIdchitiet.AddRange(_temp);
                gridExRow.Delete();
                grdDonthuocravien.UpdateData();

            }
            _KCB_KEDONTHUOC.XoaChitietDonthuoc(s);
            DataRow[] rows =
                      m_dtDonthuoc_ravien.Select(KcbDonthuocChitiet.Columns.IdChitietdonthuoc + " IN (" + String.Join(",", lstIdchitiet.ToArray()) + ")");
            string _deleteitems = string.Join(",", (from p in rows.AsEnumerable()
                                                    select Utility.sDbnull(p["ten_thuoc"])).ToList<string>());
            // UserName is Column Name
            Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa thuốc trong đơn thuốc ra viện của bệnh nhân ID={0}, PID={1}, Tên={2}, DS thuốc xóa={3} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan, _deleteitems), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
            XoaThuocKhoiBangdulieu(m_dtDonthuoc_ravien,lstIdchitiet);
        }
        private void cmdXoadonthuocravien_Click(object sender, EventArgs e)
        {
            if (CheckDachuyenkhoa()) return;
            if (!IsValidThuocRavien_delete()) return;
            PerformActionDeletePres_ravien();
            ModifyCommmands();
        }

        private void cmdIndonthuocravien_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdDonthuocravien)) return;
                int Pres_ID = Utility.Int32Dbnull(grdDonthuocravien.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong_off("NOITRU_PHIEUDIEUTRI_DONTHUOC_INTACH", "0", false) == "1")
                {
                    if (RowThuocRavien == null)
                    {
                        Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện việc xóa thông tin thuốc ", "Thông báo", MessageBoxIcon.Warning);
                        grdDonthuocravien.Focus();
                        return;
                    }
                    int presId = Utility.Int32Dbnull(RowThuocRavien.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value);
                    PrintPres(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, 1, objPhieudieutri.NgayDieutri.Value, presId, "",true);
                }
                else
                {
                    string maLanKham = Utility.sDbnull(txtPatient_Code.Text.Trim());
                    PrintPresGop(objLuotkham.MaLuotkham,Pres_ID, "");
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
                // throw;
            }
        }

        private void mnuPhieucongkhai_Click(object sender, EventArgs e)
        {
            frm_phieucongkhai _phieucongkhai = new frm_phieucongkhai();
            _phieucongkhai.objLuotkham = this.objLuotkham;
            _phieucongkhai.AutoLoad = true;
            _phieucongkhai.ShowDialog();
        }

        private void mnuPhieucongkhaiThuoc_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtData = SPs.PhieucongkhaiGetdata(Utility.Int64Dbnull(grdPhieudieutri.GetValue("id_phieudieutri")),objLuotkham.IdBenhnhan,objLuotkham.MaLuotkham, "THUOC").GetDataSet().Tables[0];
                dtData.TableName = "phieucongkhai_thuoc";
                THU_VIEN_CHUNG.CreateXML(dtData, "phieucongkhai_thuoc.xml");
                if (dtData == null || dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu đơn thuốc theo phiếu điều trị đang chọn để in phiếu công khai. Vui lòng kiểm tra lại", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                noitru_inphieu.InPhieu(dtData, DateTime.Now,"", true, "phieucongkhai_thuoc");
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void mnuPhieucongkhaiVTTH_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtData = SPs.PhieucongkhaiGetdata(Utility.Int64Dbnull(grdPhieudieutri.GetValue("id_phieudieutri")),objLuotkham.IdBenhnhan,objLuotkham.MaLuotkham, "VT").GetDataSet().Tables[0];
                dtData.TableName = "phieucongkhai_thuoc";
                THU_VIEN_CHUNG.CreateXML(dtData, "phieucongkhai_thuoc.xml");
                if (dtData == null || dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu đơn vật tư theo phiếu điều trị đang chọn để in phiếu công khai. Vui lòng kiểm tra lại", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                noitru_inphieu.InPhieu(dtData, DateTime.Now, "", true, "phieucongkhai_vt");
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNoiDung.Text)) ClickData();
        }
        private void ClickData()
        {
            webBrowser1.Document.InvokeScript("setValue", new[] { txtNoiDung.Text });
            timer1.Stop();
        }

        private void mnuICD_Click(object sender, EventArgs e)
        {
            if (objLuotkham != null && objLuotkham.TrangthaiNoitru >= 3)
            {
                Utility.ShowMsg("Người bệnh đã được tổng hợp làm thủ tục ra viện nên bạn không thể nhập thêm thông tin chẩn đoán");
                return;
            }
            frm_ChandoanICD _ChandoanICD = new frm_ChandoanICD();
            _ChandoanICD.objLuotkham = this.objLuotkham;
            _ChandoanICD.CallfromParent = true;
            _ChandoanICD.ShowDialog();
        }

        private void mnuHIV_Click(object sender, EventArgs e)
        {
            try
            {

                if (m_dtAssignDetail.Select("isHIV=1").Count() > 0)
                {
                    int idChidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChidinh), 0);
                    KcbChidinhcl _chidinh = KcbChidinhcl.FetchByID(idChidinh);
                    QueryCommand cmd = KcbDanhsachBenhnhan.CreateQuery().BuildCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandSql = string.Format("select id_benhnhan,ma_luotkham,ma_yte,ten_benhnhan,dien_thoai as SDT,CMT,ten_dantoc as dan_toc,gioi_tinh,nam_sinh,dia_chi as hokhau_thuongtru,dia_chi as noio_hientai,nghe_nghiep,ten_doituong_kcb as  doi_tuong,'' as nguyco_laynhiem_hiv,'' as dia_diem from v_kcb_luotkham where id_benhnhan={0} and ma_luotkham='{1}'", objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham);
                    DataTable dtData = DataService.GetDataSet(cmd).Tables[0];

                    dtData.TableName = "tbl_HIV";
                    Document doc;
                    DataRow drData = dtData.Rows[0];
                    drData["dia_diem"] = Utility.FormatDateTimeWithLocation(_chidinh == null ? DateTime.Now : _chidinh.NgayChidinh, globalVariables.gv_strDiadiem);// string.Format("{0}, Ngày         tháng         năm", globalVariables.gv_strDiadiem);// Utility.FormatDateTimeWithLocation(DateTime.Now,globalVariables.gv_strDiadiem));
                    List<string> fieldNames = new List<string>();

                    string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\PHIEU_HIV.doc";
                    string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                    if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);


                    if (!File.Exists(PathDoc))
                    {
                        Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                          MessageBoxIcon.Warning);
                        return;
                    }
                    SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                    string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}",
                                   Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                                   Path.GetFileNameWithoutExtension(PathDoc), "PhieuHIV", objLuotkham.MaLuotkham, Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


                    if ((drData != null) && File.Exists(PathDoc))
                    {
                        doc = new Document(PathDoc);
                        DocumentBuilder builder = new DocumentBuilder(doc);
                        if (doc == null)
                        {
                            Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return;
                        }
                        //if (builder.MoveToMergeField("logo") && globalVariables.SysLogo != null)
                        //    if (sysLogosize != null)
                        //    {
                        //        int w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                        //        int h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                        //        if (w > 0 && h > 0)
                        //            builder.InsertImage(globalVariables.SysLogo, w, h);
                        //        else
                        //            builder.InsertImage(globalVariables.SysLogo);
                        //    }
                        //    else
                        //        if (globalVariables.SysLogo != null)
                        //            builder.InsertImage(globalVariables.SysLogo);
                        doc.MailMerge.Execute(drData);
                        if (File.Exists(fileKetqua))
                        {
                            File.Delete(fileKetqua);
                        }
                        doc.Save(fileKetqua, SaveFormat.Doc);
                        string path = fileKetqua;

                        if (File.Exists(path))
                        {
                            Process process = new Process();
                            try
                            {
                                process.StartInfo.FileName = path;
                                process.Start();
                                process.WaitForInputIdle();
                            }
                            catch
                            {
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy biểu mẫu", "TThông báo", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    Utility.ShowMsg("Phiếu chỉ định bạn đang chọn không có dịch vụ HIV. Vui lòng chọn phiếu chỉ định có chứa dịch vụ xét nghiệm HIV trước khi bấm nút in");
                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void mnuTonghopChiphiKCB_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn cần phải chọn bệnh nhân trước khi xem tổng hợp chi phí nội trú");
                return;
            }
            var _Xemtonghopchiphi = new frm_Xemtonghopchiphi(false, "-1");
            _Xemtonghopchiphi.objLuotkham = objLuotkham;
            _Xemtonghopchiphi.ShowDialog();
        }

        private void chkShowGroup_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkShowGroup.Checked)
                {
                    CreateViewTable();
                }
                else
                {
                    Utility.SetDataSourceForDataGridEx_Basic(grdVTTH, m_dtVTTH, true, true, "1=1", KcbDonthuocChitiet.Columns.SttIn);
                    Utility.SetDataSourceForDataGridEx_Basic(grdPresDetail, m_dtDonthuoc, true, true, "1=1", KcbDonthuocChitiet.Columns.SttIn);
                    Utility.SetDataSourceForDataGridEx_Basic(grdDonthuocravien, m_dtDonthuoc_ravien, true, true, "1=1", KcbDonthuocChitiet.Columns.SttIn);
                }

                grdPresDetail.RootTable.Columns["so_luongtralai"].EditType = chkShowGroup.Checked ? EditType.NoEdit : EditType.TextBox;
                grdVTTH.RootTable.Columns["so_luongtralai"].EditType = chkShowGroup.Checked ? EditType.NoEdit : EditType.TextBox;
                grdDonthuocravien.RootTable.Columns["so_luongtralai"].EditType = chkShowGroup.Checked ? EditType.NoEdit : EditType.TextBox;
            }
            catch (Exception ex)
            {

            }
        }

        private void mnuBarcode_Click(object sender, EventArgs e)
        {
            try
            {
                if (objLuotkham != null)
                {
                    QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandSql = string.Format("select * from v_kcb_luotkham where id_benhnhan={0} and ma_luotkham='{1}'", objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham);
                    DataTable dt = DataService.GetDataSet(cmd).Tables[0];
                    if (!dt.Columns.Contains("barcodeID")) dt.Columns.Add("barcodeID", typeof(byte[]));
                    if (!dt.Columns.Contains("barcode")) dt.Columns.Add("barcode", typeof(byte[]));
                    THU_VIEN_CHUNG.CreateXML(dt, "barcode.xml");
                    if (dt.Rows.Count > 0)
                    {
                        VNS.HIS.UI.Forms.Dungchung.FrmBarCodePrint frm = new VNS.HIS.UI.Forms.Dungchung.FrmBarCodePrint(3);
                        frm.m_dtReport = dt;
                        frm.ShowDialog();
                    }
                }
                else
                {
                    Utility.ShowMsg("Bạn cần chọn người bệnh để thực hiện in tem");
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void mnuSuaPhieunhapvien_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("noitru_suaphieunhapvien_trongkhoanoitru"))
                {
                    Utility.thongbaokhongcoquyen("noitru_suaphieunhapvien_trongkhoanoitru", "sửa thông tin phiếu nhập viện từ khoa nội trú");
                    return;
                }

                var frm = new frm_Nhapvien();
                frm.CallfromParent = true;
                frm.id_kham = -1;//Load lại từ phiếu nhập viện
                frm.id_bskham = -1;//Load lại từ phiếu nhập viện
                frm.objLuotkham = objLuotkham;
                frm.ucThongtinnguoibenh1.txtMaluotkham.Text = objLuotkham.MaLuotkham;
                frm.ShowDialog();
                if (frm.b_Cancel)
                {

                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void mnuChuyendieutrinoitru_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn cần chọn người bệnh điều trị ngoại trú trước khi thực hiện tính năng này");
                    return;
                }
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn chuyển người bệnh {0} từ hướng điều trị ngoại trú sang hướng điều trị nội trú hay không?", grdList.GetValue("ten_benhnhan").ToString()), "Xác nhận chuyển hướng điều trị", true))
                    return;
                objLuotkham = Utility.getKcbLuotkham(grdList.CurrentRow);
                if (objLuotkham != null)
                {
                    new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.HuongDieutri).EqualTo("3").Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).Execute();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Đã chuyển người bệnh ID bệnh nhân={0}, PID={1} vào hướng điều trị nội trú thành công", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                    Utility.ShowMsg("Đã chuyển người bệnh vào hướng điều trị nội trú thành công. Vui lòng thực hiện tính năng chuyển khoa(nếu có) hoặc báo cho khoa hiện tại thực hiện phân buồng giường và lập y lệnh thường qui");
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
        }

        private void mnuChuyendieutringoaitru_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn cần chọn người bệnh điều trị nội trú trước khi thực hiện tính năng này");
                    return;
                }
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn chuyển người bệnh {0} từ hướng điều trị nội trú sang hướng điều trị ngoại trú hay không?", grdList.GetValue("ten_benhnhan").ToString()), "Xác nhận chuyển hướng điều trị", true))
                    return;
                objLuotkham = Utility.getKcbLuotkham(grdList.CurrentRow);
                if (objLuotkham != null)
                {
                    new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.HuongDieutri).EqualTo("4").Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).Execute();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Đã chuyển người bệnh ID bệnh nhân={0}, PID={1} về hướng điều trị ngoại trú thành công", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham), newaction.Update, this.GetType().Assembly.ManifestModule.Name);

                    Utility.ShowMsg("Đã chuyển người bệnh về hướng điều trị ngoại trú thành công. Vui lòng thực hiện tính năng chuyển khoa(nếu có) hoặc báo cho khoa hiện tại thực hiện y lệnh thường qui");
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
        }

        private void mnuChanthuchienCLS_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Utility.Coquyen("CLS_CHAN_THUCHIEN"))
                //{
                //    Utility.thongbaokhongcoquyen("CLS_CHAN_THUCHIEN", "chặn thực hiện dịch vụ cận lâm sàng");
                //}
                if (grdAssignDetail.GetCheckedRows().Count() <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất một dịch vụ CLS cần chặn thực hiện");
                    return;
                }
                int num = -1;
                string[] query = (from p in grdAssignDetail.GetCheckedRows()
                                  select string.Format("{0}", Utility.sDbnull(p.Cells["ten_chitietdichvu"].Value, ""))).ToArray();
                string noidung = string.Join("\n", query);
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn chặn các dịch vụ CLS dưới đây(Sau khi chặn, các khoa phòng XN,CĐHA,TDCN KHÔNG thể lấy dịch vụ CLS về để thực hiện cho người bệnh)?:\n{0}", noidung),
                    "Thông báo", true))
                {
                    query = (from p in grdAssignDetail.GetCheckedRows()
                             select string.Format("{0}", Utility.sDbnull(p.Cells["id_chitietchidinh"].Value, ""))).ToArray();
                    string ids = string.Join(",", query);
                    using (var scope = new TransactionScope())
                    {
                        using (var sh = new SharedDbConnectionScope())
                        {
                            num = SPs.KcbChidinhclsChanChophepChuyencls(ids, (byte)1).Execute();
                            if (num > 0)
                            {
                                Utility.Log(this.Name, globalVariables.UserName, string.Format("Chặn các dịch vụ CLS: {0} thành công ", noidung), newaction.CancelData, this.GetType().Assembly.ManifestModule.Name);
                            }
                        }
                        scope.Complete();
                    }
                    if (num > 0)
                        Utility.ShowMsg(string.Format("Đã chặn các dịch vụ CLS đang chọn. Các khoa phòng XN,CĐHA,TDCN KHÔNG thể lấy dịch vụ CLS này về để thực hiện cho người bệnh"));
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void mnuHuychanthuchienCLS_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Utility.Coquyen("CLS_CHAN_THUCHIEN"))
                //{
                //    Utility.thongbaokhongcoquyen("CLS_CHAN_THUCHIEN", "hủy chặn thực hiện dịch vụ cận lâm sàng");
                //}
                if (grdAssignDetail.GetCheckedRows().Count() <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất một dịch vụ CLS cần hủy chặn thực hiện");
                    return;
                }
                int num = -1;
                string[] query = (from p in grdAssignDetail.GetCheckedRows()
                                  select string.Format("{0}", Utility.sDbnull(p.Cells["ten_chitietdichvu"].Value, ""))).ToArray();
                string noidung = string.Join("\n", query);
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn hủy chặn các dịch vụ CLS dưới đây(Sau khi hủy chặn, các khoa phòng XN,CĐHA,TDCN CÓ thể lấy dịch vụ CLS về để thực hiện cho người bệnh)?:\n{0}", noidung),
                    "Thông báo", true))
                {

                    query = (from p in grdAssignDetail.GetCheckedRows()
                             select string.Format("{0}", Utility.sDbnull(p.Cells["id_chitietchidinh"].Value, ""))).ToArray();
                    string ids = string.Join(",", query);
                    using (var scope = new TransactionScope())
                    {
                        using (var sh = new SharedDbConnectionScope())
                        {
                            num = SPs.KcbChidinhclsChanChophepChuyencls(ids, (byte)0).Execute();
                            if (num > 0)
                                Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy chặn các dịch vụ CLS: {0} thành công ", noidung), newaction.Restore, this.GetType().Assembly.ManifestModule.Name);
                        }
                        scope.Complete();
                    }

                }
                if (num > 0)
                    Utility.ShowMsg(string.Format("Đã hủy chặn các dịch vụ CLS đang chọn. Các khoa phòng XN,CĐHA,TDCN có thể lấy dịch vụ CLS này về để thực hiện cho người bệnh"));
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void mnuSaochepMaLK_Click(object sender, EventArgs e)
        {
            SaochepMaluotkham();
        }

        private void mnuSaochepIDBN_Click(object sender, EventArgs e)
        {
            SaochepIdBN();
        }

        private void cmdSearch_Click_1(object sender, EventArgs e)
        {

        }

        private void mnuXemLichsuKCB_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.isValidGrid(grdList))
                {
                    frm_lichsukcb _lichsukcb = new frm_lichsukcb();
                    _lichsukcb.txtMaluotkham.Text = grdList.GetValue("ma_luotkham").ToString();
                    _lichsukcb.AutoLoad = true;
                    _lichsukcb.Anluoidanhsachbenhnhan = true;
                    _lichsukcb.ShowDialog();
                }
                else
                {
                    Utility.ShowMsg("Cần chọn người bệnh trên lưới danh sách trước khi nhấn xem lịch sử KCB. Vui lòng kiểm tra lại");
                }
            }
            catch (Exception)
            {

            }
        }

        private void mnuLsuChuyenKhoaBG_Click(object sender, EventArgs e)
        {
            try
            {
                frm_TimKiem_BN lsuBG = new frm_TimKiem_BN();
                lsuBG.txtPatientCode.Text = grdList.GetValue("ma_luotkham").ToString();
                lsuBG.MaLuotkham = grdList.GetValue("ma_luotkham").ToString();
                lsuBG.SearchType = 2;
                lsuBG.ShowDialog();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
          
        }

        
    }
}