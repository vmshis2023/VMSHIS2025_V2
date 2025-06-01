using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using NLog;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.DAL;
using VNS.HIS.UCs;
using VNS.HIS.UI.Classess;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.UI.Forms.BenhAn.Forms;
using VNS.HIS.UI.Forms.CanLamSang;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.Libs;
using VNS.Properties;
using VNS.UCs;
using VMS.QMS;
using System.Threading;
using VNS.HIS.UI.HinhAnh;
using VNS.HIS.UI.Forms.HinhAnh;
using Aspose.Words;
using System.Diagnostics;
using VNS.HIS.Classes;
using System.Transactions;
using VNS.HIS.UI.NOITRU;
using VMS.HIS.Danhmuc;
using VNS.Libs.AppUI;
using System.Runtime.InteropServices;
using SubSonic.Sugar;
using VNS.HIS.UI.Forms.Dungchung;
using VMS.HIS.Bus.BHYT;
using VMS.EMR.PHIEUKHAM;

namespace VNS.HIS.UI.NGOAITRU
{
    /// <summary>
    /// 06/11/2013 3h57
    /// </summary>
    public partial class frm_KCB_THAMKHAM_V2 : Form
    {
        private readonly string Args = "ALL";

        private readonly string FileName = string.Format("{0}/{1}", Application.StartupPath,
                                                         "SplitterDistanceThamKham.txt");
        public bool m_blnCancel = true;
        private readonly MoneyByLetter MoneyByLetter = new MoneyByLetter();
        private readonly KCB_CHIDINH_CANLAMSANG _KCB_CHIDINH_CANLAMSANG = new KCB_CHIDINH_CANLAMSANG();
        private readonly KCB_KEDONTHUOC _KCB_KEDONTHUOC = new KCB_KEDONTHUOC();
        private readonly KCB_THAMKHAM _KCB_THAMKHAM = new KCB_THAMKHAM();
        private readonly Logger log;

        private readonly List<string> lstKQCochitietColumns = new List<string>
                                                                  {"ten_chitietdichvu", "Ket_qua", "bt_nam", "bt_nu"};

        private readonly List<string> lstKQKhongchitietColumns = new List<string> {"Ket_qua", "bt_nam", "bt_nu"};

        private readonly List<string> lstResultColumns = new List<string>
                                                             {
                                                                 "ten_chitietdichvu",
                                                                 "ketqua_cls",
                                                                 "binhthuong_nam",
                                                                 "binhthuong_nu"
                                                             };

      
           string SplitterPath = "";
        private readonly string strSaveandprintPath = Application.StartupPath +
                                                      @"\CAUHINH\DefaultPrinter_PhieuHoaSinh.txt";
        string helpfile = "";
        private int Distance = 488;
        private string TEN_BENHPHU = "";
        private DMUC_CHUNG _DMUC_CHUNG = new DMUC_CHUNG();
        private KcbChandoanKetluan _KcbChandoanKetluan;
        private bool _buttonClick;
        private DataSet ds = new DataSet();
        private DataTable dt_ICD = new DataTable();
        private DataTable dt_ICD_PHU = new DataTable();
        private bool hasLoaded;
        private bool hasMorethanOne = true;
        bool AutoGoiLoa_WhenNext = false;
        private bool isLike = true;
        private bool isNhapVien = false;
        private bool isRoom;
        private List<string> lstVisibleColumns = new List<string>();
        private DataTable m_dtAssignDetail;
        private DataTable m_dtDanhsachbenhnhanthamkham = new DataTable();
        private DataTable m_dtDoctorAssign;
        private DataTable m_dtDonthuocChitiet_View = new DataTable();
        private DataTable m_dtKhoaNoiTru = new DataTable();
        private DataTable m_dtBenhAn = new DataTable();
        private DataTable m_dtPresDetail = new DataTable();

        private DataTable m_dtReport = new DataTable();
        private DataTable m_dtVTTH = new DataTable();
        private DataTable m_dtVTTHChitiet_View = new DataTable();
        private DataTable m_hdt = new DataTable();
        private DataTable m_kl;
        private string m_strDefaultLazerPrinterName = "";
        private bool status = false;
        /// <summary>
        /// hàm thực hiện việc chọn bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string m_strMaLuotkham = "";

        public KcbDanhsachBenhnhan objBenhnhan = null;
        public KcbLuotkham objLuotkham = null;

        private frm_DanhSachKham objSoKham;
        private KcbDangkyKcb objCongkham;
        private GridEXRow row_Selected;
        private bool trieuchung;
        public string ma_luotkham = "";
        public bool Lakhamthiluc = false;
        public frm_KCB_THAMKHAM_V2(string sthamso)
        {
            InitializeComponent();
            PropertyLib._QMSPrintProperties = PropertyLib.GetQMSPrintProperties(Application.StartupPath + @"\CauHinh_QMS");
            SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
            helpfile = this.Name;
            Utility.SetVisualStyle(this);
            if (PropertyLib._HinhAnhProperties == null) PropertyLib._HinhAnhProperties = PropertyLib.GetHinhAnhProperties();
            KeyPreview = true;
            Args = sthamso;
            
             log = LogManager.GetCurrentClassLogger();
            log.Trace("Begin Constructor...");
            dtpNgaytiepdon.Value = dtpNgaydangky.Value = globalVariables.SysDate;
            dtFromDate.Value = globalVariables.SysDate;
            dtToDate.Value = globalVariables.SysDate;
            dtFromDate.MaxDate = dtToDate.MaxDate = globalVariables.SysDate;
            webBrowser1.Url = new Uri(Application.StartupPath.ToString() + @"\editor\ckeditor_simple.html");
            ConfigKhamThiluc();
            LoadLaserPrinters();
            CauHinhQMS();
            CauHinhThamKham();
            // txtIdKhoaNoiTru.Visible = globalVariables.IsAdmin;
            InitEvents();
            InitFtp();
            log.Trace("Constructor finished");
        }
        void ConfigKhamThiluc()
        {
            try
            {
                Lakhamthiluc = this.Args.Split('-')[0] == "KTL";
                if (Lakhamthiluc)//Công khám thị lực
                {
                    TabPageTailieudinhkem.TabVisible = tabPageChanDoan.TabVisible = tabPageChiDinhCLS.TabVisible = tabPageChidinhThuoc.TabVisible = tabPageHoibenh.TabVisible = tabPageVTTH.TabVisible = tabPhieuDieuTri.TabVisible = false;
                    grpFunction.Visible = false;
                    grpFunction.Height = 0;
                    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THAMKHAM_KEDONKINH_KHAMTHILUC", "0", true) == "1")
                        uiTabPageDonkinh.TabVisible = true;
                    else
                        uiTabPageDonkinh.TabVisible = false;
                }
                else//Công khám chính
                {
                   
                    TabPageKhamThiluc.TabVisible = uiTabPageDonkinh.TabVisible= false;
                    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THAMKHAM_KEDONKINH", "0", true) == "1")
                        uiTabPageDonkinh.TabVisible = true;
                    else
                        uiTabPageDonkinh.TabVisible = false;
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
           
        }
        private int SplitterDistance
        {
            get { return Distance; }
            set { Distance = value; }
        }

        private void InitEvents()
        {
            
            Load += frm_KCB_THAMKHAM_V2_Load;
            Shown += frm_KCB_THAMKHAM_V2_Shown;
            KeyDown += frm_KCB_THAMKHAM_V2_KeyDown;
            mnuCancelResult.Click += mnuCancelResult_Click;
            txtSoKham.LostFocus += txtSoKham_LostFocus;
            txtSoKham.Click += txtSoKham_Click;
            txtSoKham.KeyDown += txtSoKham_KeyDown;

            cmdSearch.Click += cmdSearch_Click;
            //radChuaKham.CheckedChanged += radChuaKham_CheckedChanged;
            //radDaKham.CheckedChanged += radDaKham_CheckedChanged;
            //optAll.CheckedChanged += optAll_CheckedChanged;

            txtPatient_Code.KeyDown += txtPatient_Code_KeyDown;

            grdList.ColumnButtonClick += grdList_ColumnButtonClick;
            grdList.KeyDown += grdList_KeyDown;
            grdList.DoubleClick += grdList_DoubleClick;
            grdList.MouseClick += grdList_MouseClick;

            grdPresDetail.UpdatingCell += grdPresDetail_UpdatingCell;
            grdPresDetail.SelectionChanged += grdPresDetail_SelectionChanged;
           
            txtMach.LostFocus += txtMach_LostFocus;
            txtSongaydieutri.LostFocus += txtSongaydieutri_LostFocus;
            // txtSoNgayHen.LostFocus += txtSoNgayHen_LostFocus;
            txtNhipTim.LostFocus += txtNhipTim_LostFocus;
            txtMaBenhphu.GotFocus += txtMaBenhphu_GotFocus;
            txtMaBenhphu.KeyDown += txtMaBenhphu_KeyDown;
            txtMaBenhphu.TextChanged += txtMaBenhphu_TextChanged;

            cmdKetthuckham.Click += cmdSave_Click;
            cmdInTTDieuTri.Click += cmdInTTDieuTri_Click;
            cmdNhapVien.Click += cmdNhapVien_Click;
            cmdHuyNhapVien.Click += cmdHuyNhapVien_Click;
            cmdCauHinh.Click += cmdCauHinh_Click;

            grdAssignDetail.ColumnHeaderClick += grdAssignDetail_ColumnHeaderClick;
            grdAssignDetail.CellUpdated += grdAssignDetail_CellUpdated;
            grdAssignDetail.EditingCell += grdAssignDetail_EditingCell;
            grdAssignDetail.UpdatingCell += grdAssignDetail_UpdatingCell;
            grdAssignDetail.SelectionChanged += grdAssignDetail_SelectionChanged;
            grdAssignDetail.MouseDoubleClick += grdAssignDetail_MouseDoubleClick;
            cmdInsertAssign.Click += cmdInsertAssign_Click;
            cmdUpdate.Click += cmdUpdate_Click;
            cmdDelteAssign.Click += cmdDelteAssign_Click;
            cboLaserPrinters.SelectedIndexChanged += cboLaserPrinters_SelectedIndexChanged;
            cmdPrintAssign.Click += cmdPrintAssign_Click;

            cboA4Donthuoc.SelectedIndexChanged += cboA4_SelectedIndexChanged;
            cboPrintPreviewDonthuoc.SelectedIndexChanged += cboPrintPreview_SelectedIndexChanged;
            cmdThemDonthuoc.Click += cmdCreateNewPres_Click;
            cmdSuadonthuoc.Click += cmdUpdatePres_Click;
            cmdXoadonthuoc.Click += cmdDeletePres_Click;
            cmdIndonthuoc.Click += cmdPrintPres_Click;

            mnuDelDrug.Click += mnuDelDrug_Click;
            mnuDelVTTH.Click += mnuDelVTTH_Click;
            mnuDeleteCLS.Click += mnuDeleteCLS_Click;

            cmdSearchBenhChinh.Click += cmdSearchBenhChinh_Click;
            cmdSearchBenhPhu.Click += cmdSearchBenhPhu_Click;
            cmdAddMaBenhPhu.Click += cmdAddMaBenhPhu_Click;

            cmdThamkhamConfig.Click += cmdThamkhamConfig_Click;
            chkAutocollapse.CheckedChanged += chkAutocollapse_CheckedChanged;

            grd_ICD.ColumnButtonClick += grd_ICD_ColumnButtonClick;
            chkHienthichitiet.CheckedChanged += chkHienthichitiet_CheckedChanged;
            cboA4Cls.SelectedIndexChanged += cboA4Cls_SelectedIndexChanged;
            cboPrintPreviewCLS.SelectedIndexChanged += cboPrintPreviewCLS_SelectedIndexChanged;

            cboA4Tomtatdieutringoaitru.SelectedIndexChanged += cboA4Tomtatdieutringoaitru_SelectedIndexChanged;
            cboPrintPreviewTomtatdieutringoaitru.SelectedIndexChanged +=
                cboPrintPreviewTomtatdieutringoaitru_SelectedIndexChanged;

            cmdUnlock.Click += cmdUnlock_Click;
            cmdChuyenPhong.Click += cmdChuyenPhong_Click;
            txtChanDoanKemTheo._OnShowData += txtChanDoanKemTheo__OnShowData;
            txtChanDoan._OnShowData += txtChanDoan__OnShowData;
            //cboKQDT._OnShowData += txtKet_Luan__OnShowData;
            //cboHDT._OnShowData += txtHuongdieutri__OnShowData;
            txtNhommau._OnShowData += txtNhommau__OnShowData;
            txtNhanxet._OnShowData += txtNhanxet__OnShowData;
            txtCheDoAn._OnShowData += txtCheDoAn__OnShowData;
           
            txtNhommau._OnSaveAs += txtNhommau__OnSaveAs;
            //cboKQDT._OnSaveAs += txtKet_Luan__OnSaveAs;
            //cboHDT._OnSaveAs += txtHuongdieutri__OnSaveAs;
            txtChanDoan._OnSaveAs += txtChanDoan__OnSaveAs;
            txtChanDoanKemTheo._OnSaveAs += txtChanDoanKemTheo__OnSaveAs;
            txtNhanxet._OnSaveAs += txtNhanxet__OnSaveAs;
            txtCheDoAn._OnSaveAs += txtCheDoAn__OnSaveAs;

            cmdThemphieuVT.Click += cmdThemphieuVT_Click;
            cmdSuaphieuVT.Click += cmdSuaphieuVT_Click;
            cmdXoaphieuVT.Click += cmdXoaphieuVT_Click;
            cmdInphieuVT.Click += cmdInphieuVT_Click;
            mnuViewPDFtheoChidinh.Click += mnuShowResult_Click;
            cmdLuuKetluan.Click += cmdLuuChandoan_Click;
            cmdSave1.Click += cmdLuuChandoan_Click;
            mnuNhapKQCDHA.Click += mnuNhapKQCDHA_Click;
            lnkMore.Click += lnkMore_Click;
            timer1.Tick += timer1_Tick;
            cmdConfig.Click += cmdConfig_Click;
            this.FormClosing += frm_KCB_THAMKHAM_V2_FormClosing;
            mnuInvert.Click += mnuInvert_Click;
            mnuCheck_UncheckAll.Click += mnuCheck_UncheckAll_Click;
            txtLoaiDichvu._OnSelectionChanged += txtLoaiDichvu__OnSelectionChanged;
            grdKetqua.UpdatingCell += grdKetqua_UpdatingCell;
            autoMabenhphu._OnEnterMe += autoMabenhphu__OnEnterMe;
            txtMaluotkham.KeyDown += txtMaluotkham_KeyDown;
            cboKieuin.SelectedIndexChanged += cboKieuin_SelectedIndexChanged;
            //autoLoidan._OnShowData += autoLoidan__OnShowData;
            //autoXutri._OnShowData += autoXutri__OnShowData;
            autoTrangthai._OnShowData += autoTrangthai__OnShowData;
            //Các tính năng thêm
            mnuKetthuckham.Click += _Click;
            mnuKhamlai.Click += _Click;
            mnuNhapvien.Click += _Click;
            mnuHuynhapvien.Click += _Click;
            mnuThemPK.Click += _Click;
            mnuChuyenPK.Click += _Click;
            mnuInphieuhenkham.Click += _Click;
            mnuInphieutuvan.Click += _Click;
            mnuIntomtatdieutri.Click += _Click;
            grdVTTH.SelectionChanged += grdVTTH_SelectionChanged;
            grdList.SelectionChanged += grdList_SelectionChanged;
            //QMS
            cmdStart.Click+=cmdStart_Click;
            cmdIgnore.Click+=cmdXoaSoKham_Click;
            cmdNext.Click+=cmdNext_Click;
            cmdGoiloa.Click+=cmdGoiloa_Click;
            cmdRestore.Click+=cmdRestore_Click;
            //QMS ngang
            cmdStart2.Click += cmdStart_Click;
            cmdIgnore2.Click += cmdXoaSoKham_Click;
            cmdNext2.Click += cmdNext_Click;
            cmdGoiloa2.Click += cmdGoiloa_Click;
            cmdRestore2.Click += cmdRestore_Click;
            lnkGoto2.LinkClicked+=lnkGoto_LinkClicked;

            txtID.KeyDown += txtID_KeyDown;
            txtID.KeyPress += txtID_KeyPress;
            Utility.setEnterEvent(this);
            txtCanhbao.LostFocus += txtCanhbao_LostFocus;
            txtCanhbao.GotFocus += txtCanhbao_GotFocus;
            mnuHuychanthuchienCLS.Click += mnuHuychanthuchienCLS_Click;
            mnuChanthuchienCLS.Click += mnuChanthuchienCLS_Click;
            grdCongkham.MouseDoubleClick += grdCongkham_MouseDoubleClick;
            txtBMI.TextChanged += txtibm_TextChanged;
            txtSPO2.TextChanged += txtSPO2_TextChanged;
            chkQMS_Ngang.CheckedChanged += chkQMS_Ngang_CheckedChanged;
            cmdHuyChuyenCLS.Click += CmdHuyChuyenCLS_Click;
            cmdChuyenCLS.Click += CmdChuyenCLS_Click;
            txtBacsi._OnEnterMe += TxtBacsi__OnEnterMe;

            autoICD_matphai._OnGridSelectionChanged += AutoICD_matphai__OnGridSelectionChanged;
            autoICD_matphai._OnSelectionChanged += AutoICD_matphai__OnSelectionChanged;
            autoICD_mattrai._OnGridSelectionChanged += AutoICD_mattrai__OnGridSelectionChanged;
            autoICD_mattrai._OnSelectionChanged += AutoICD_mattrai__OnSelectionChanged;
            autoICD_2mat._OnGridSelectionChanged += AutoICD_2mat__OnGridSelectionChanged;
            autoICD_2mat._OnSelectionChanged += AutoICD_2mat__OnSelectionChanged;
            autoICD_2mat._OnEnterMe += AutoICD_2mat__OnEnterMe;
            autoICD_matphai._OnEnterMe += AutoICD_matphai__OnEnterMe;
            autoICD_mattrai._OnEnterMe += AutoICD_mattrai__OnEnterMe;
            mnuTronggoi_CLS.Click += _tronggoi;
            mnuNgoaigoi_CLS.Click += _ngoaigoi;
            mnuTronggoi_Thuoc.Click += _tronggoi;
            mnuNgoaigoi_thuoc.Click += _ngoaigoi;
            mnuTronggoi_VTTH.Click += _tronggoi;
            mnuNgoaigoi_VTTH.Click += _ngoaigoi;
            cmdInGoi.Click += CmdInGoi_Click;
            cmdDenghiMG.Click += CmdDenghiMG_Click;
            mnuXoaGoi.Click += MnuXoaGoi_Click;
            txtTAG._OnShowDataV1 += _OnShowDataV1;
            txtTAG._OnEnterMe += TxtTAG__OnEnterMe;
            grdTag.ColumnButtonClick += GrdTag_ColumnButtonClick;
           
        }

        private void GrdTag_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "XOA")
                {
                    string ten_the = Utility.sDbnull(grdTag.GetValue("ten"));
                    if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa Tag: {0}", ten_the), "Xác nhận xóa Tag", true))
                    {
                        int num = new Delete().From(KcbTag.Schema).Where(KcbTag.Columns.IdThe).IsEqualTo(Utility.Int64Dbnull(grdTag.GetValue("id_the"))).Execute();
                        if (num > 0)
                        {
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa Tag của bệnh nhân ID={0}, PID={1}, Tên={2},Tag bị xóa ={3} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan, ten_the), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                            DataRow[] arrDr = m_dtTag.Select("id_the="+ Utility.Int64Dbnull(grdTag.GetValue("id_the")));
                            if (arrDr.Length > 0)
                                m_dtTag.Rows.Remove(arrDr[0]); 
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        DataTable m_dtTag = new DataTable();
        private void TxtTAG__OnEnterMe()
        {
            try
            {
                if (objLuotkham != null || objCongkham != null)
                    if (txtTAG.MyCode != "-1")
                    {
                        var p = from q in m_dtTag.AsEnumerable() where Utility.sDbnull(q["MA"]) == txtTAG.MyCode select q;
                        if (!p.Any())
                        {
                            KcbTag _newTag = new KcbTag();
                            _newTag.IdBenhnhan = objCongkham.IdBenhnhan;
                            _newTag.MaLuotkham = objCongkham.MaLuotkham;
                            _newTag.IdCongkham = objCongkham.IdKham;
                            _newTag.IdBacsi = Utility.Int32Dbnull(txtBacsi.MyID);
                            _newTag.NgayTao = globalVariables.SysDate;
                            _newTag.NguoiTao = globalVariables.UserName;
                            _newTag.MaThe = txtTAG.MyCode;
                            _newTag.TenThe = txtTAG.Text;
                            _newTag.Save();

                            DataRow newItem = m_dtTag.NewRow();
                            Utility.FromObjectToDatarow(_newTag, ref newItem);
                            newItem["MA"] = _newTag.MaThe;
                            newItem["ten"] = _newTag.TenThe;
                            m_dtTag.Rows.Add(newItem);
                        }
                        Utility.GotoNewRowJanus(grdTag, "MA", txtTAG.MyCode);
                    }
            }
            catch (Exception ex)
            {

            }
        }

        void _OnShowDataV1(UCs.AutoCompleteTextbox_Danhmucchung obj)
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(obj.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = obj.myCode;
                obj.Init();
                obj.SetCode(oldCode);
                obj.Focus();
            }
        }

        /// <summary>
        /// Xóa gói 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnuXoaGoi_Click(object sender, EventArgs e)
        {
            try
            {
                long id_dangky = Utility.Int64Dbnull(RowCLS.Cells["id_dangky"].Value);
                long IdChidinh = Utility.Int64Dbnull(RowCLS.Cells["id_chidinh"].Value);
                string ten_goi= Utility.sDbnull(RowCLS.Cells["ten_goi"].Value);
                if (id_dangky>0)
                {
                    GoiDangki _goi = GoiDangki.FetchByID(id_dangky);
                    int num = 0;
                    if (_goi != null)
                    {
                        KcbThanhtoanChitiet _ttct = new Select().From(KcbThanhtoanChitiet.Schema)
                            .Where(KcbThanhtoanChitiet.Columns.IdPhieu).IsEqualTo(id_dangky)
                            .And(KcbThanhtoanChitiet.Columns.IdLoaithanhtoan).IsEqualTo(13)
                            .ExecuteSingle<KcbThanhtoanChitiet>();
                        if (Utility.Bool2Bool(_goi.TthaiTtoan) || _ttct != null)
                        {
                            Utility.ShowMsg(string.Format("Gói {0} đã được thanh toán nên bạn không thể xóa các dịch vụ trong gói. Vui lòng liên hệ quầy thu ngân để kiểm tra", ten_goi));
                            return;
                        }
                        string sql = string.Format("select 1 from kcb_chidinhcls_chitiet where id_dangky={0} and id_goi={1} and trang_thai>0", id_dangky, _goi.IdGoi);
                        DataTable dtCheck = Utility.ExecuteSql(sql, CommandType.Text).Tables[0];
                        if (dtCheck.Rows.Count > 0)
                        {
                            Utility.ShowMsg(string.Format("Gói {0} đã có dịch vụ được chuyển CLS hoặc có kết quả nên bạn không thể xóa. Bạn có thể đưa các dịch vụ đã có kết quả ra ngoài gói trước khi thực hiện xóa gói", ten_goi));
                            return;
                        }
                        //Thực hiện xóa gói
                        using (var scope = new TransactionScope())
                        {
                            using (var sh = new SharedDbConnectionScope())
                            {
                                num = new Delete().From(KcbChidinhclsChitiet.Schema).Where(KcbChidinhclsChitiet.Columns.IdDangky).IsEqualTo(id_dangky).And(KcbChidinhclsChitiet.Columns.IdGoi).IsEqualTo(_goi.IdGoi).Execute();
                                num = new Delete().From(GoiDangki.Schema).Where(GoiDangki.Columns.IdDangky).IsEqualTo(id_dangky).And(GoiDangki.Columns.IdGoi).IsEqualTo(_goi.IdGoi).Execute();
                                num = new Delete().From(GoiTinhtrangsudung.Schema).Where(GoiTinhtrangsudung.Columns.IdDangky).IsEqualTo(id_dangky).And(GoiTinhtrangsudung.Columns.IdGoi).IsEqualTo(_goi.IdGoi).Execute();
                                num = new Delete().From(QheChidinhclsGoi.Schema).Where(QheChidinhclsGoi.Columns.IdChidinh).IsEqualTo(IdChidinh).And(GoiTinhtrangsudung.Columns.IdGoi).IsEqualTo(_goi.IdGoi).Execute();
                                Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa gói của người bệnh mã lần khám {0}, ID bệnh nhân: {1} tên= {2}, tên gói: {3}", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan, Utility.sDbnull(RowCLS.Cells["ten_goi"].Value)), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                            }
                            scope.Complete();
                        }
                        //Xóa khỏi lưới danh sách
                        m_dtAssignDetail.AsEnumerable()
                   .Where(r => r.Field<int>(KcbChidinhclsChitiet.Columns.IdDangky) == _goi.IdDangky && r.Field<int>(KcbChidinhclsChitiet.Columns.IdGoi) == _goi.IdGoi).ToList()
                   .ForEach(row => row.Delete());
                        m_dtAssignDetail.AcceptChanges();

                    }
                }    
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void CmdDenghiMG_Click(object sender, EventArgs e)
        {
            try
            {
                if(grdAssignDetail.GetCheckedRows().Length<=0)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất một dịch vụ trong gói để thực hiện đề nghị MG");
                    return;
                }
                //Kiểm tra các dịch vụ đang chọn có dịch vụ nào được phép MG không

                var q = from p in grdAssignDetail.GetCheckedRows() where Utility.Int32Dbnull(p.Cells["id_goi"].Value) <=0 || Utility.Int32Dbnull(p.Cells["trong_goi"].Value) <= 0 select p;
                if(q.Any())
                {
                    Utility.ShowMsg("Bạn chỉ được phép thực hiện đề nghị miễn giảm đối với các dịch vụ trong gói. Nhấn OK để hệ thống tự dò tìm các dịch vụ này");
                    return;
                }    
                frm_Chondanhmucdungchung nhaplydohuythanhtoan = new frm_Chondanhmucdungchung("LYDOMIENGIAMGOI",
                                   "Đề nghị miễn giảm dịch vụ trong gói không sử dụng", "Nhập lý do đề nghị miễn giảm",
                                   "Lý do MG", false);
                nhaplydohuythanhtoan.ShowDialog();
                if (nhaplydohuythanhtoan.m_blnCancel) return;
              string  ma_lydohuy = nhaplydohuythanhtoan.ma;
              string  lydo_huy = nhaplydohuythanhtoan.ten;

            }
            catch (Exception ex)
            {

             
            }
        }

        private void CmdInGoi_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtData = SPs.KcbThamkhamLaydulieuIngoiChidinhCls(Utility.sDbnull( RowCLS.Cells["ma_chidinh"].Value), objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan).GetDataSet().Tables[0];
                THU_VIEN_CHUNG.CreateXML(dtData, "thamkham_inphieuchidinh_goi.xml");
                if (dtData == null || dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                noitru_inphieu.InPhieu(dtData, DateTime.Now,Utility.sDbnull(dtData.Rows[0]["ten_bacsi_chidinh"]), "",true,"thamkham_inphieuchidinh_goi");
            }
            catch (Exception ex)
            {

              
            }
        }

        private void _ngoaigoi(object sender, EventArgs e)
        {
            Tronggoi_ngoaigoi(false, ((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl);
        }

        private void _tronggoi(object sender, EventArgs e)
        {
            Tronggoi_ngoaigoi(true, ((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl);
        }
        void Tronggoi_ngoaigoi(bool tronggoi, Control sourceGrd)
        {
            try
            {
                GridEX _myGrd = sourceGrd as GridEX;
                if (!Utility.Coquyen("tronggoi_ngoaigoi"))
                {
                    Utility.thongbaokhongcoquyen("tronggoi_ngoaigoi", " chuyển các dịch vụ của người bệnh vào trong gói hoặc ngoài gói. Liên hệ IT bệnh viện để được bổ sung quyền");
                    return;
                }
                objLuotkham = Utility.getKcbLuotkham(objLuotkham);
                if (objLuotkham.TrangthaiNoitru == 5)
                {
                    Utility.ShowMsg("Bệnh nhân đã được duyệt thanh toán nội trú nên bạn không thể thay đổi trạng thái trong gói/ngoài gói");
                    return;
                }

                if (Utility.Byte2Bool(objLuotkham.TthaiThanhtoannoitru) || objLuotkham.TrangthaiNoitru == 6)
                {
                    Utility.ShowMsg("Bệnh nhân đã được thanh toán nội trú nên bạn không thể thay đổi trạng thái trong gói/ngoài gói");
                    return;
                }
                string sDichvu = "";
                string sDichvu_Exception = "";
                byte loai_dvu = 0;
                loai_dvu = Utility.ByteDbnull(_myGrd.Name == grdAssignDetail.Name ? 2 : (_myGrd.Name == grdPresDetail.Name ? 3 : (_myGrd.Name == grdVTTH.Name ? 5 : 0)));
                if (tronggoi) //Đưa vào trong gói
                {
                    DataTable dtGoi = m_dtAssignDetail.Clone();
                    int v_intIdgoi = 0;
                    int v_intIdDangky = 0;
                    string ten_goi = "Ngoài gói";
                    string lstException = "";
                    var q = from p in m_dtAssignDetail.AsEnumerable()
                            where Utility.Int32Dbnull(p["id_goi"]) > 0
                            select p;
                    if (q.Any())
                    {
                        dtGoi = q.CopyToDataTable();
                    }
                    if (dtGoi.Rows.Count > 0)
                    {
                        VMS.HIS.Danhmuc.Goikham.frm_tronggoi_tuychon _tronggoi_tuychon = new VMS.HIS.Danhmuc.Goikham.frm_tronggoi_tuychon(dtGoi);
                        _tronggoi_tuychon.ShowDialog();
                        if (!_tronggoi_tuychon.mv_blnCancel)
                        {
                            DataRow[] arrDr = m_dtAssignDetail.Select("id_dangky=" + _tronggoi_tuychon.id_dangky);
                            if (arrDr.Length > 0)
                            {
                                v_intIdgoi = Utility.Int32Dbnull(arrDr[0]["id_goi"]);
                                v_intIdDangky = Utility.Int32Dbnull(arrDr[0]["id_dangky"]);
                                ten_goi = Utility.sDbnull(arrDr[0]["ten_goi"]);
                                //Kiểm tra điều kiện đưa vào gói là dịch vụ phải là thành phần cấu hình nên gói
                                DataTable dtCheck = Utility.ExecuteSql(string.Format("select id_chitietdichvu,loai_dvu from goi_chitiet where id_goi={0}", v_intIdgoi), CommandType.Text).Tables[0];
                                if (dtCheck.Rows.Count > 0)
                                {
                                    GridEXRow[] lstCheckedRow = _myGrd.GetCheckedRows();
                                    foreach (GridEXRow _row in lstCheckedRow)
                                    {
                                        long id_chitietdichvu = Utility.Int64Dbnull(_row.Cells[KcbChidinhclsChitiet.Columns.IdChitietdichvu].Value, -1);
                                        if (dtCheck.Select(string.Format("id_chitietdichvu={0} and loai_dvu={1}", id_chitietdichvu, loai_dvu)).Length <= 0)
                                        {
                                            _row.IsChecked = false;
                                            lstException += Utility.sDbnull(_row.Cells["ten_chitietdichvu"].Value) + ", ";
                                        }
                                    }
                                    if (lstException.Length > 0)
                                    {
                                        Utility.ShowMsg(string.Format("Một số dịch vụ sau không đưa được vào gói {0} do không phải là thành phần cấu thành lên gói:\r\n{1}", ten_goi, lstException));
                                    }
                                }
                            }
                        }
                        else
                            return;
                    }  

                    if (_myGrd.Name == grdAssignDetail.Name)
                    {
                        if (grdAssignDetail.GetCheckedRows().Count() <= 0 && Utility.isValidGrid(grdAssignDetail))
                        {
                            sDichvu = Utility.sDbnull(grdAssignDetail.GetValue("ten_chitietdichvu"));
                            long Id = Utility.Int64Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                            KcbChidinhclsChitiet objCheck = KcbChidinhclsChitiet.FetchByID(Id);
                            if (objCheck != null)
                            {
                                if (Utility.Byte2Bool(objCheck.TrangthaiThanhtoan)) //Đã thanh toán
                                {
                                    Utility.ShowMsg("Dịch vụ CLS bạn đang chọn đã thanh toán nên không cho phép thay đổi trạng thái trong gói/ngoài gói. Đề nghị bạn kiểm tra lại");
                                    return;
                                }
                            }
                            else
                            {
                                Utility.ShowMsg("Dịch vụ CLS bạn đang chọn không tồn tại (Có thể đã bị xóa bởi người khác trong lúc bạn đang thao tác). Đề nghị bạn kiểm tra lại");
                                return;
                            }
                            new Update(KcbChidinhclsChitiet.Schema)
                                .Set("trong_goi").EqualTo(v_intIdgoi<=0?1:0)
                                .Set("id_goi").EqualTo(v_intIdgoi)
                                .Set("id_dangky").EqualTo(v_intIdDangky)
                                .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(Id).Execute();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Chuyển các dịch vụ CLS của bệnh nhân có mã lần khám {0} và ID bệnh nhân: {1} tên= {2} vào trong gói: {3}", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan, sDichvu), newaction.Move, this.GetType().Assembly.ManifestModule.Name);
                            _myGrd.CurrentRow.BeginEdit();
                            _myGrd.CurrentRow.Cells["trong_goi"].Value = v_intIdgoi <= 0 ? 1 : 0;
                            _myGrd.CurrentRow.Cells["id_goi"].Value = v_intIdgoi;
                            _myGrd.CurrentRow.Cells["ten_goi"].Value = ten_goi;
                            _myGrd.CurrentRow.Cells["id_dangky"].Value = v_intIdDangky;
                            _myGrd.CurrentRow.EndEdit();
                        }
                        else
                        {
                            foreach (GridEXRow _row in grdAssignDetail.GetCheckedRows())
                            {
                                long Id = Utility.Int64Dbnull(_row.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
                                KcbChidinhclsChitiet objCheck = KcbChidinhclsChitiet.FetchByID(Id);
                                if (objCheck != null)
                                {
                                    if (Utility.Byte2Bool(objCheck.TrangthaiThanhtoan)) //Đã thanh toán
                                    {
                                        sDichvu_Exception += Utility.sDbnull(_row.Cells["ten_chitietdichvu"].Value) + ", ";
                                        continue;
                                    }
                                }
                                else
                                {
                                    sDichvu_Exception += Utility.sDbnull(_row.Cells["ten_chitietdichvu"].Value) + ", ";
                                    continue;
                                }
                                sDichvu += Utility.sDbnull(_row.Cells["ten_chitietdichvu"].Value) + ", ";
                                new Update(KcbChidinhclsChitiet.Schema)
                                     .Set("trong_goi").EqualTo(v_intIdgoi <= 0 ? 1 : 0)
                                .Set("id_goi").EqualTo(v_intIdgoi)
                                .Set("id_dangky").EqualTo(v_intIdDangky)
                                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(Id).Execute();
                                _row.BeginEdit();
                                _row.Cells["trong_goi"].Value = v_intIdgoi <= 0 ? 1 : 0;
                                _row.Cells["id_goi"].Value = v_intIdgoi;
                                _row.Cells["ten_goi"].Value = ten_goi;
                                _row.Cells["id_dangky"].Value = v_intIdDangky;
                                _row.EndEdit();


                            }
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Chuyển các dịch vụ CLS của bệnh nhân có mã lần khám {0} và ID bệnh nhân: {1} tên= {2} vào trong gói: {3}", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan, sDichvu), newaction.Move, this.GetType().Assembly.ManifestModule.Name);
                            if (sDichvu_Exception.Length > 0)
                            {
                                Utility.ShowMsg(string.Format("Hệ thống phát hiện một số dịch vụ bạn chọn đã thanh toán(hoặc không tồn tại) nên không cho phép chuyển vào gói:\r\n{0}", sDichvu_Exception));
                                return;
                            }
                        }
                        grdAssignDetail.Refresh();
                        //ChangeMenu(grdAssignDetail.CurrentRow);
                    }
                    else if (_myGrd.Name == grdPresDetail.Name)
                    {
                        if (grdPresDetail.GetCheckedRows().Count() <= 0 && Utility.isValidGrid(grdPresDetail))
                        {
                            sDichvu = Utility.sDbnull(grdPresDetail.GetValue("ten_thuoc"));
                            long Id = Utility.Int64Dbnull(Utility.GetValueFromGridColumn(grdPresDetail, KcbDonthuocChitiet.Columns.IdChitietdonthuoc), -1);
                            KcbDonthuocChitiet objCheck = KcbDonthuocChitiet.FetchByID(Id);
                            if (objCheck != null)
                            {
                                if (Utility.Byte2Bool(objCheck.TrangthaiThanhtoan)) //Đã thanh toán
                                {
                                    Utility.ShowMsg("Thuốc bạn đang chọn đã thanh toán nên không cho phép thay đổi trạng thái trong gói/ngoài gói. Đề nghị bạn kiểm tra lại");
                                    return;
                                }
                            }
                            else
                            {
                                Utility.ShowMsg("Thuốc bạn đang chọn không tồn tại (Có thể đã bị xóa bởi người khác trong lúc bạn đang thao tác). Đề nghị bạn kiểm tra lại");
                                return;
                            }
                            new Update(KcbDonthuocChitiet.Schema).Set("trong_goi").EqualTo(v_intIdgoi <= 0 ? 1 : 0)
                                .Set("id_goi").EqualTo(v_intIdgoi)
                                .Set("id_dangky").EqualTo(v_intIdDangky)
                                .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Id).Execute();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Chuyển các Thuốc của bệnh nhân có mã lần khám {0} và ID bệnh nhân: {1} tên= {2} vào trong gói: {3}", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan, sDichvu), newaction.Move, this.GetType().Assembly.ManifestModule.Name);
                            _myGrd.CurrentRow.BeginEdit();
                            _myGrd.CurrentRow.Cells["trong_goi"].Value = v_intIdgoi <= 0 ? 1 : 0;
                            _myGrd.CurrentRow.Cells["id_goi"].Value = v_intIdgoi;
                            _myGrd.CurrentRow.Cells["ten_goi"].Value = ten_goi;
                            _myGrd.CurrentRow.Cells["id_dangky"].Value = v_intIdDangky;
                            _myGrd.CurrentRow.EndEdit();
                        }
                        else
                        {
                            foreach (GridEXRow _row in grdPresDetail.GetCheckedRows())
                            {
                                long Id = Utility.Int64Dbnull(_row.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                                KcbDonthuocChitiet objCheck = KcbDonthuocChitiet.FetchByID(Id);
                                if (objCheck != null)
                                {
                                    if (Utility.Byte2Bool(objCheck.TrangthaiThanhtoan)) //Đã thanh toán
                                    {
                                        sDichvu_Exception += Utility.sDbnull(_row.Cells["ten_thuoc"].Value) + ", ";
                                        continue;
                                    }
                                }
                                else
                                {
                                    sDichvu_Exception += Utility.sDbnull(_row.Cells["ten_thuoc"].Value) + ", ";
                                    continue;
                                }
                                new Update(KcbDonthuocChitiet.Schema).Set("trong_goi").EqualTo(v_intIdgoi <= 0 ? 1 : 0)
                                .Set("id_goi").EqualTo(v_intIdgoi)
                                .Set("id_dangky").EqualTo(v_intIdDangky)
                                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Id).Execute();
                                sDichvu += Utility.sDbnull(_row.Cells["ten_thuoc"].Value) + ", ";
                                _row.BeginEdit();
                                _row.Cells["trong_goi"].Value = v_intIdgoi <= 0 ? 1 : 0;
                                _row.Cells["id_goi"].Value = v_intIdgoi;
                                _row.Cells["ten_goi"].Value = ten_goi;
                                _row.Cells["id_dangky"].Value = v_intIdDangky;
                                _row.EndEdit();
                            }

                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Chuyển các Thuốc của bệnh nhân có mã lần khám {0} và ID bệnh nhân: {1} tên= {2} vào trong gói: {3}", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan, sDichvu), newaction.Move, this.GetType().Assembly.ManifestModule.Name);
                            if (sDichvu_Exception.Length > 0)
                            {
                                Utility.ShowMsg(string.Format("Hệ thống phát hiện một số dịch vụ bạn chọn đã thanh toán(hoặc không tồn tại) nên không cho phép chuyển vào gói:\r\n{0}", sDichvu_Exception));
                                return;
                            }
                        }
                        grdPresDetail.Refresh();
                        //ChangeMenu(grdPresDetail.CurrentRow);
                    }
                    else if (_myGrd.Name == grdVTTH.Name) //VTTH
                    {
                        if (grdVTTH.GetCheckedRows().Count() <= 0 && Utility.isValidGrid(grdVTTH))
                        {
                            sDichvu = Utility.sDbnull(grdVTTH.GetValue("ten_thuoc"));
                            long Id = Utility.Int64Dbnull(Utility.GetValueFromGridColumn(grdVTTH, KcbDonthuocChitiet.Columns.IdChitietdonthuoc), -1);
                            KcbDonthuocChitiet objCheck = KcbDonthuocChitiet.FetchByID(Id);
                            if (objCheck != null)
                            {
                                if (Utility.Byte2Bool(objCheck.TrangthaiThanhtoan)) //Đã thanh toán
                                {
                                    Utility.ShowMsg("Vật tư bạn đang chọn đã thanh toán nên không cho phép thay đổi trạng thái trong gói/ngoài gói. Đề nghị bạn kiểm tra lại");
                                    return;
                                }
                            }
                            else
                            {
                                Utility.ShowMsg("VTTH bạn đang chọn không tồn tại (Có thể đã bị xóa bởi người khác trong lúc bạn đang thao tác). Đề nghị bạn kiểm tra lại");
                                return;
                            }
                            new Update(KcbDonthuocChitiet.Schema).Set("trong_goi").EqualTo(v_intIdgoi <= 0 ? 1 : 0)
                                .Set("id_goi").EqualTo(v_intIdgoi)
                                .Set("id_dangky").EqualTo(v_intIdDangky)
                                .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Id).Execute();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Chuyển các VTTH của bệnh nhân có mã lần khám {0} và ID bệnh nhân: {1} tên= {2} vào trong gói: {3}", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan, sDichvu), newaction.Move, this.GetType().Assembly.ManifestModule.Name);
                            _myGrd.CurrentRow.BeginEdit();
                            _myGrd.CurrentRow.Cells["trong_goi"].Value = v_intIdgoi <= 0 ? 1 : 0;
                            _myGrd.CurrentRow.Cells["id_goi"].Value = v_intIdgoi;
                            _myGrd.CurrentRow.Cells["ten_goi"].Value = ten_goi;
                            _myGrd.CurrentRow.Cells["id_dangky"].Value = v_intIdDangky;
                            _myGrd.CurrentRow.EndEdit();
                        }
                        else
                        {
                            foreach (GridEXRow _row in grdVTTH.GetCheckedRows())
                            {
                                long Id = Utility.Int64Dbnull(_row.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                                KcbDonthuocChitiet objCheck = KcbDonthuocChitiet.FetchByID(Id);
                                if (objCheck != null)
                                {
                                    if (Utility.Byte2Bool(objCheck.TrangthaiThanhtoan)) //Đã thanh toán
                                    {
                                        sDichvu_Exception += Utility.sDbnull(_row.Cells["ten_thuoc"].Value) + ", ";
                                        continue;
                                    }
                                }
                                else
                                {
                                    sDichvu_Exception += Utility.sDbnull(_row.Cells["ten_thuoc"].Value) + ", ";
                                    continue;
                                }
                                new Update(KcbDonthuocChitiet.Schema).Set("trong_goi").EqualTo(v_intIdgoi <= 0 ? 1 : 0)
                                .Set("id_goi").EqualTo(v_intIdgoi)
                                .Set("id_dangky").EqualTo(v_intIdDangky)
                                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Id).Execute();
                                sDichvu += Utility.sDbnull(_row.Cells["ten_thuoc"].Value) + ", ";
                                _row.BeginEdit();
                                _row.Cells["trong_goi"].Value = v_intIdgoi <= 0 ? 1 : 0;
                                _row.Cells["id_goi"].Value = v_intIdgoi;
                                _row.Cells["ten_goi"].Value = ten_goi;
                                _row.Cells["id_dangky"].Value = v_intIdDangky;
                                _row.EndEdit();
                            }
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Chuyển các VTTH của bệnh nhân có mã lần khám {0} và ID bệnh nhân: {1} tên= {2} vào trong gói: {3}", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan, sDichvu), newaction.Move, this.GetType().Assembly.ManifestModule.Name);
                            if (sDichvu_Exception.Length > 0)
                            {
                                Utility.ShowMsg(string.Format("Hệ thống phát hiện một số dịch vụ bạn chọn đã thanh toán(hoặc không tồn tại) nên không cho phép chuyển vào gói:\r\n{0}", sDichvu_Exception));
                                return;
                            }
                        }
                        grdVTTH.Refresh();
                        //ChangeMenu(grdVTTH.CurrentRow);
                    }
                }
                else //Ngoài gói
                {
                    if (_myGrd.Name == grdAssignDetail.Name)
                    {
                        if (grdAssignDetail.GetCheckedRows().Count() <= 0 && Utility.isValidGrid(grdAssignDetail))
                        {
                            sDichvu = Utility.sDbnull(grdAssignDetail.GetValue("ten_chitietdichvu"));
                            long Id = Utility.Int64Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                            KcbChidinhclsChitiet objCheck = KcbChidinhclsChitiet.FetchByID(Id);
                            if (objCheck != null)
                            {
                                if (Utility.Byte2Bool(objCheck.TrangthaiThanhtoan)) //Đã thanh toán
                                {
                                    Utility.ShowMsg("Dịch vụ CLS bạn đang chọn đã thanh toán nên không cho phép thay đổi trạng thái trong gói/ngoài gói. Đề nghị bạn kiểm tra lại");
                                    return;
                                }
                            }
                            else
                            {
                                Utility.ShowMsg("Dịch vụ CLS bạn đang chọn không tồn tại (Có thể đã bị xóa bởi người khác trong lúc bạn đang thao tác). Đề nghị bạn kiểm tra lại");
                                return;
                            }
                            new Update(KcbChidinhclsChitiet.Schema).Set(KcbChidinhclsChitiet.Columns.TrongGoi).EqualTo(0)
                                .Set(KcbChidinhclsChitiet.Columns.IdGoi).EqualTo(0)
                                .Set(KcbChidinhclsChitiet.Columns.IdDangky).EqualTo(0)
                                .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(Id).Execute();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Chuyển các dịch vụ CLS của bệnh nhân có mã lần khám {0} và ID bệnh nhân: {1} tên= {2} ra ngoài gói: {3}", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan, sDichvu), newaction.Move, this.GetType().Assembly.ManifestModule.Name);
                            _myGrd.CurrentRow.BeginEdit();
                            _myGrd.CurrentRow.Cells["trong_goi"].Value = 0;
                            _myGrd.CurrentRow.Cells["id_goi"].Value = 0;
                            _myGrd.CurrentRow.Cells["ten_goi"].Value = "Ngoài gói";
                            _myGrd.CurrentRow.Cells["id_dangky"].Value = 0;
                            _myGrd.CurrentRow.EndEdit();
                            _myGrd.Refresh();
                           
                        }
                        else
                        {
                            foreach (GridEXRow _row in grdAssignDetail.GetCheckedRows())
                            {
                                long Id = Utility.Int64Dbnull(_row.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
                                KcbChidinhclsChitiet objCheck = KcbChidinhclsChitiet.FetchByID(Id);
                                if (objCheck != null)
                                {
                                    if (Utility.Byte2Bool(objCheck.TrangthaiThanhtoan)) //Đã thanh toán
                                    {
                                        sDichvu_Exception += Utility.sDbnull(_row.Cells["ten_chitietdichvu"].Value) + ", ";
                                        continue;
                                    }
                                }
                                else
                                {
                                    sDichvu_Exception += Utility.sDbnull(_row.Cells["ten_chitietdichvu"].Value) + ", ";
                                    continue;
                                }
                                new Update(KcbChidinhclsChitiet.Schema).Set(KcbChidinhclsChitiet.Columns.TrongGoi).EqualTo(0)
                                    .Set(KcbChidinhclsChitiet.Columns.IdGoi).EqualTo(0)
                                    .Set(KcbChidinhclsChitiet.Columns.IdDangky).EqualTo(0)
                                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(Id).Execute();
                                sDichvu += Utility.sDbnull(_row.Cells["ten_chitietdichvu"].Value) + ", ";
                                _row.BeginEdit();
                                _row.BeginEdit();
                                _row.Cells["trong_goi"].Value = 0;
                                _row.Cells["id_goi"].Value = 0;
                                _row.Cells["ten_goi"].Value = "Ngoài gói";
                                _row.Cells["id_dangky"].Value = 0;
                                _row.EndEdit();
                            }
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Chuyển các dịch vụ CLS của bệnh nhân có mã lần khám {0} và ID bệnh nhân: {1} tên= {2} ra ngoài gói: {3}", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan, sDichvu), newaction.Move, this.GetType().Assembly.ManifestModule.Name);
                            if (sDichvu_Exception.Length > 0)
                            {
                                Utility.ShowMsg(string.Format("Hệ thống phát hiện một số dịch vụ bạn chọn đã thanh toán(hoặc không tồn tại) nên không cho phép chuyển ra ngoài gói:\r\n{0}", sDichvu_Exception));
                                return;
                            }
                        }
                        grdAssignDetail.Refresh();
                        //ChangeMenu(grdAssignDetail.CurrentRow);
                    }
                    else if (_myGrd.Name == grdPresDetail.Name)
                    {
                        if (grdPresDetail.GetCheckedRows().Count() <= 0 && Utility.isValidGrid(grdPresDetail))
                        {
                            sDichvu = Utility.sDbnull(grdVTTH.GetValue("ten_thuoc"));
                            long Id = Utility.Int64Dbnull(Utility.GetValueFromGridColumn(grdPresDetail, KcbDonthuocChitiet.Columns.IdChitietdonthuoc), -1);
                            KcbDonthuocChitiet objCheck = KcbDonthuocChitiet.FetchByID(Id);
                            if (objCheck != null)
                            {
                                if (Utility.Byte2Bool(objCheck.TrangthaiThanhtoan)) //Đã thanh toán
                                {
                                    Utility.ShowMsg("Thuốc bạn đang chọn đã thanh toán nên không cho phép thay đổi trạng thái trong gói/ngoài gói. Đề nghị bạn kiểm tra lại");
                                    return;
                                }
                            }
                            else
                            {
                                Utility.ShowMsg("Thuốc bạn đang chọn không tồn tại (Có thể đã bị xóa bởi người khác trong lúc bạn đang thao tác). Đề nghị bạn kiểm tra lại");
                                return;
                            }
                            new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(0)
                                .Set(KcbDonthuocChitiet.Columns.IdGoi).EqualTo(0)
                                 .Set(KcbDonthuocChitiet.Columns.IdDangky).EqualTo(0)
                                .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Id).Execute();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Chuyển các Thuốc của bệnh nhân có mã lần khám {0} và ID bệnh nhân: {1} tên= {2} ra ngoài gói: {3}", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan, sDichvu), newaction.Move, this.GetType().Assembly.ManifestModule.Name);
                            _myGrd.CurrentRow.BeginEdit();
                            _myGrd.CurrentRow.Cells["trong_goi"].Value = 0;
                            _myGrd.CurrentRow.Cells["id_goi"].Value = 0;
                            _myGrd.CurrentRow.Cells["ten_goi"].Value = "Ngoài gói";
                            _myGrd.CurrentRow.Cells["id_dangky"].Value = 0;
                            _myGrd.CurrentRow.EndEdit();
                            _myGrd.Refresh();
                        }
                        else
                        {
                            foreach (GridEXRow _row in grdPresDetail.GetCheckedRows())
                            {
                                long Id = Utility.Int64Dbnull(_row.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                                KcbDonthuocChitiet objCheck = KcbDonthuocChitiet.FetchByID(Id);
                                if (objCheck != null)
                                {
                                    if (Utility.Byte2Bool(objCheck.TrangthaiThanhtoan)) //Đã thanh toán
                                    {
                                        sDichvu_Exception += Utility.sDbnull(_row.Cells["ten_thuoc"].Value) + ", ";
                                        continue;
                                    }
                                }
                                else
                                {
                                    sDichvu_Exception += Utility.sDbnull(_row.Cells["ten_thuoc"].Value) + ", ";
                                    continue;
                                }
                                new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(0)
                                    .Set(KcbDonthuocChitiet.Columns.IdGoi).EqualTo(0)
                                    .Set(KcbDonthuocChitiet.Columns.IdDangky).EqualTo(0)
                                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Id).Execute();
                                sDichvu += Utility.sDbnull(_row.Cells["ten_thuoc"].Value) + ", ";
                                _row.BeginEdit();
                                _row.Cells["trong_goi"].Value = 0;
                                _row.Cells["id_goi"].Value = 0;
                                _row.Cells["ten_goi"].Value = "Ngoài gói";
                                _row.Cells["id_dangky"].Value = 0;
                                _row.EndEdit();
                            }
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Chuyển các Thuốc của bệnh nhân có mã lần khám {0} và ID bệnh nhân: {1} tên= {2} ra ngoài gói: {3}", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan, sDichvu), newaction.Move, this.GetType().Assembly.ManifestModule.Name);
                            if (sDichvu_Exception.Length > 0)
                            {
                                Utility.ShowMsg(string.Format("Hệ thống phát hiện một số dịch vụ bạn chọn đã thanh toán(hoặc không tồn tại) nên không cho phép chuyển ra ngoài gói:\r\n{0}", sDichvu_Exception));
                                return;
                            }
                        }
                        grdPresDetail.Refresh();
                        //ChangeMenu(grdPresDetail.CurrentRow);
                    }
                    if (_myGrd.Name == grdVTTH.Name)
                    {
                        if (grdVTTH.GetCheckedRows().Count() <= 0 && Utility.isValidGrid(grdVTTH))
                        {
                            sDichvu = Utility.sDbnull(grdVTTH.GetValue("ten_thuoc"));
                            long Id = Utility.Int64Dbnull(Utility.GetValueFromGridColumn(grdVTTH, KcbDonthuocChitiet.Columns.IdChitietdonthuoc), -1);
                            KcbDonthuocChitiet objCheck = KcbDonthuocChitiet.FetchByID(Id);
                            if (objCheck != null)
                            {
                                if (Utility.Byte2Bool(objCheck.TrangthaiThanhtoan)) //Đã thanh toán
                                {
                                    Utility.ShowMsg("Vật tư bạn đang chọn đã thanh toán nên không cho phép thay đổi trạng thái trong gói/ngoài gói. Đề nghị bạn kiểm tra lại");
                                    return;
                                }
                            }
                            else
                            {
                                Utility.ShowMsg("VTTH bạn đang chọn không tồn tại (Có thể đã bị xóa bởi người khác trong lúc bạn đang thao tác). Đề nghị bạn kiểm tra lại");
                                return;
                            }
                            new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(0)
                                .Set(KcbDonthuocChitiet.Columns.IdGoi).EqualTo(0)
                                 .Set(KcbDonthuocChitiet.Columns.IdDangky).EqualTo(0)
                                .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Id).Execute();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Chuyển các VTTH của bệnh nhân có mã lần khám {0} và ID bệnh nhân: {1} tên= {2} ra ngoài gói: {3}", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan, sDichvu), newaction.Move, this.GetType().Assembly.ManifestModule.Name);
                            _myGrd.CurrentRow.BeginEdit();
                            _myGrd.CurrentRow.Cells["trong_goi"].Value = 0;
                            _myGrd.CurrentRow.Cells["id_goi"].Value = 0;
                            _myGrd.CurrentRow.Cells["ten_goi"].Value = "Ngoài gói";
                            _myGrd.CurrentRow.Cells["id_dangky"].Value = 0;
                            _myGrd.CurrentRow.EndEdit();
                            _myGrd.Refresh();
                        }
                        else
                        {
                            foreach (GridEXRow _row in grdVTTH.GetCheckedRows())
                            {
                                long Id = Utility.Int64Dbnull(_row.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                                KcbDonthuocChitiet objCheck = KcbDonthuocChitiet.FetchByID(Id);
                                if (objCheck != null)
                                {
                                    if (Utility.Byte2Bool(objCheck.TrangthaiThanhtoan)) //Đã thanh toán
                                    {
                                        sDichvu_Exception += Utility.sDbnull(_row.Cells["ten_thuoc"].Value) + ", ";
                                        continue;
                                    }
                                }
                                else
                                {
                                    sDichvu_Exception += Utility.sDbnull(_row.Cells["ten_thuoc"].Value) + ", ";
                                    continue;
                                }
                                new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(0)
                                    .Set(KcbDonthuocChitiet.Columns.IdGoi).EqualTo(0)
                                     .Set(KcbDonthuocChitiet.Columns.IdDangky).EqualTo(0)
                                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Id).Execute();
                                sDichvu += Utility.sDbnull(_row.Cells["ten_thuoc"].Value) + ", ";
                                _row.BeginEdit();
                                _row.Cells["trong_goi"].Value = 0;
                                _row.Cells["id_goi"].Value = 0;
                                _row.Cells["ten_goi"].Value = "Ngoài gói";
                                _row.Cells["id_dangky"].Value = 0;
                                _row.EndEdit();
                              
                            }
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Chuyển các VTTH của bệnh nhân có mã lần khám {0} và ID bệnh nhân: {1} tên= {2} ra ngoài gói: {3}", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan, sDichvu), newaction.Move, this.GetType().Assembly.ManifestModule.Name);
                            if (sDichvu_Exception.Length > 0)
                            {
                                Utility.ShowMsg(string.Format("Hệ thống phát hiện một số dịch vụ bạn chọn đã thanh toán(hoặc không tồn tại) nên không cho phép chuyển ra ngoài gói:\r\n{0}", sDichvu_Exception));
                                return;
                            }
                        }
                        grdVTTH.Refresh();
                        // ChangeMenu(grdVTTH.CurrentRow);
                    }

                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void AutoICD_mattrai__OnEnterMe()
        {
            autoICD_mattrai.SetCode(autoICD_mattrai.MyCode);
            autoMabenhphu.Focus();
            autoMabenhphu.SelectAll();
        }

        private void AutoICD_matphai__OnEnterMe()
        {
            autoICD_matphai.SetCode( autoICD_matphai.MyCode);
            autoICD_mattrai.Focus();
            autoICD_mattrai.SelectAll();
        }

        private void AutoICD_2mat__OnEnterMe()
        {
            autoICD_2mat.SetCode(autoICD_2mat.MyCode);
            if (kiemtra)
            {
                if (objLuotkham != null && Utility.sDbnull(objLuotkham.MabenhChinh, "").Length <= 0 || (objLuotkham != null && Utility.sDbnull(objLuotkham.MabenhChinh, "").Length > 0 && _KcbChandoanKetluan != null && Utility.sDbnull(_KcbChandoanKetluan.MabenhChinh, "").Length > 0))//Chỉ kiểm tra khi đã có mã bệnh chính
                {
                    //OK
                }
                else
                {
                    Utility.ShowMsg("Bạn không được quyền thay đổi mã bệnh chính do phòng khám khác nhập");
                    kiemtra = false;
                    autoICD_2mat.SetCode(objLuotkham.MabenhChinh);
                    return;
                }
            }
            kiemtra = true;
            autoICD_matphai.Focus();
            autoICD_matphai.SelectAll();
        }

        private void AutoICD_2mat__OnSelectionChanged()
        {
            
        }

        private void AutoICD_2mat__OnGridSelectionChanged(short id_benh, string ma_benh, string ten_benh)
        {
            txtTenICD2Mat.Text = ten_benh;
           // autoICD_2mat._Text = ma_benh;
        }

        private void AutoICD_mattrai__OnSelectionChanged()
        {
           
        }

        private void AutoICD_mattrai__OnGridSelectionChanged(short id_benh, string ma_benh, string ten_benh)
        {
            txtTenICDMatTrai.Text = ten_benh;
           // autoICD_mattrai._Text = ma_benh;
        }

        private void AutoICD_matphai__OnSelectionChanged()
        {
            //txtTenICDMatPhai.Text = autoICD_matphai.MyText;
        }

        private void AutoICD_matphai__OnGridSelectionChanged(short id_benh, string ma_benh, string ten_benh)
        {
            txtTenICDMatPhai.Text = ten_benh;
            //autoICD_matphai._Text = ma_benh;
        }

        private void TxtBacsi__OnEnterMe()
        {
            try
            {
                if (Lakhamthiluc)
                {
                    //DmucNhanvien objNv = DmucNhanvien.FetchByID(Utility.Int32Dbnull(txtBacsi.MyID, -1));
                    uc_khamthiluc2.SetBacsiKham(txtBacsi.MyID);
                    uc_donkinh1.SetBacsiKham(Utility.Int32Dbnull( txtBacsi.MyID));
                }
               
            }
            catch (Exception ex)
            {


            }
        }

        private void CmdChuyenCLS_Click(object sender, EventArgs e)
        {
            cmdChuyenCLS.Enabled = false;
            Chuyen_Huychuyen_CLS(true);
        }

        private void CmdHuyChuyenCLS_Click(object sender, EventArgs e)
        {
            cmdHuyChuyenCLS.Enabled = false;
            Chuyen_Huychuyen_CLS(false);
        }
        void HuychuyenCLS()
        {
            bool hasFound = false;
            try
            {
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn HỦY chuyển Cận lâm sàng cho các dịch vụ Xét nghiệm, CĐHA và thăm dò chức năng của phiếu chỉ định đang chọn {0} hay không?\r\nNhấn Yes đồng nghĩa với việc các dịch vụ ngoại trú được Hủy chuyển cận lâm sàng sẽ cần thanh toán trước khi được thực hiện tại các khoa XN, CĐHA và TDCN.\r\nNhấn No để hủy thao tác", grdAssignDetail.GetValue("ma_chidinh")), "Xác nhận HỦY chuyển Cận lâm sàng", true)) return;
                Utility.EnableButton(cmdHuyChuyenCLS, false);
                Utility.WaitNow(this);
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChidinh).In(
                        new Select(KcbChidinhcl.Columns.IdChidinh).From(KcbChidinhcl.Schema).Where(
                            KcbChidinhcl.Columns.MaLuotkham)
                            .IsEqualTo(txtPatient_Code.Text)
                            .And(KcbChidinhcl.Columns.IdBenhnhan)
                            .IsEqualTo(Utility.Int32Dbnull(txtPatient_ID.Text)))
                    .And(KcbChidinhclsChitiet.Columns.TrangThai).IsEqualTo(1);
                hasFound = sqlQuery.GetRecordCount() > 0;
                if (sqlQuery.GetRecordCount() <= 0)
                {
                    Utility.SetMsg(lblMessage, string.Format("Không có chỉ định CLS có thể hủy chuyển"), false);
                    return;
                }
                DataRow[] arrDr = m_dtAssignDetail.Select("trangthai_chuyencls=1");

                foreach (DataRow dr in arrDr)
                {
                    dr["trangthai_chuyencls"] = 0;
                }
                m_dtAssignDetail.AcceptChanges();
                int result = new Update(KcbChidinhclsChitiet.Schema)
                 .Set(KcbChidinhclsChitiet.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                 .Set(KcbChidinhclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                 .Set(KcbChidinhclsChitiet.Columns.TrangThai).EqualTo(0)
                 .Set(KcbChidinhclsChitiet.Columns.TthaiChuyencls).EqualTo(0)
                 .Where(KcbChidinhclsChitiet.Columns.TrangThai).IsLessThanOrEqualTo(2)
                 .And(KcbChidinhclsChitiet.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                 .And(KcbChidinhclsChitiet.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                 .Execute();
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy chuyển CLS của bệnh nhân có mã lần khám {0} và ID bệnh nhân là: {1} tên= {2}", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan), newaction.CancelData, this.GetType().Assembly.ManifestModule.Name);

                Utility.SetMsg(lblMessage, string.Format("Bạn vừa hủy chuyển CLS thành công {0} dịch vụ", result.ToString()), false);
                Utility.ShowMsg(lblMessage.Text);
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
            finally
            {
                TuybiennutchuyenCLS();
                Utility.DefaultNow(this);
            }
        }
        void ChuyenCLS()
        {
            try
            {
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn CHUYỂN Cận lâm sàng cho các dịch vụ Xét nghiệm, CĐHA và thăm dò chức năng của người bệnh {0} hay không?\r\nNhấn Yes đồng nghĩa với việc các dịch vụ CLS ngoại trú được chuyển cận lâm sàng sẽ không cần thanh toán mà vẫn được thực hiện tại các khoa XN, CĐHA và TDCN.\r\nNhấn No để hủy thao tác", objBenhnhan.TenBenhnhan), "Xác nhận CHUYỂN Cận lâm sàng", true)) return;
                Utility.EnableButton(cmdChuyenCLS, false);
                Utility.WaitNow(this);
                var lstIdchidinhchitiet = new List<string>();
                if (objLuotkham != null)
                {
                    DataRow[] arrDr = m_dtAssignDetail.Select("trangthai_chuyencls=0");
                    if (arrDr.Length == 0)
                    {
                        Utility.SetMsg(lblMessage, string.Format("Các chỉ định CLS đã được chuyển hết"), false);
                        return;
                    }
                    else
                    {

                        foreach (DataRow dr in arrDr)
                        {
                            dr["trangthai_chuyencls"] = 1;
                        }
                        m_dtAssignDetail.AcceptChanges();
                        int result = new Update(KcbChidinhclsChitiet.Schema)
                        .Set(KcbChidinhclsChitiet.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                        .Set(KcbChidinhclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                        .Set(KcbChidinhclsChitiet.Columns.TrangThai).EqualTo(1)
                         .Set(KcbChidinhclsChitiet.Columns.TthaiChuyencls).EqualTo(1)
                        .Where(KcbChidinhclsChitiet.Columns.TrangThai).IsLessThanOrEqualTo(0)
                        .And(KcbChidinhclsChitiet.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(KcbChidinhclsChitiet.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .Execute();
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Chuyển CLS của bệnh nhân có mã lần khám {0} và ID bệnh nhân là: {1} tên= {2} thành công", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan), newaction.Move, this.GetType().Assembly.ManifestModule.Name);
                      
                        Utility.SetMsg(lblMessage, string.Format("Bạn vừa chuyển CLS thành công {0} dịch vụ", result.ToString()), false);
                        Utility.ShowMsg(lblMessage.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                TuybiennutchuyenCLS();
                Utility.DefaultNow(this);
            }
        }
        void Chuyen_Huychuyen_CLS( bool chuyencls)
        {
            try
            {
                Utility.WaitNow(this);
                var lstIdchidinhchitiet = new List<string>();
                if (objLuotkham != null && objCongkham!=null)
                {
                    if (chuyencls)
                    {
                        int id_chidinh =
                            Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                        DataRow[] arrDr =
                            m_dtAssignDetail.Select("trangthai_chuyencls=0 and id_chidinh = '" + id_chidinh + "' ");
                        if (arrDr.Length == 0)
                        {
                            Utility.SetMsg(lblMsg, string.Format("Các dịch vụ CLS trong phiếu chỉ định đang chọn đã được chuyển hết"), false);
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

                            SPs.KcbClsChuyencls(Utility.Int64Dbnull(objLuotkham.IdBenhnhan),
                                Utility.sDbnull(objLuotkham.MaLuotkham), Utility.Int64Dbnull(id_chidinh),
                                globalVariables.SysDate, globalVariables.UserName).Execute();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Chuyển CLS của bệnh nhân có mã lần khám {0} và ID bệnh nhân là: {1} tên= {2} thành công", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan), newaction.Move, this.GetType().Assembly.ManifestModule.Name);
                            Utility.SetMsg(lblMsg, string.Format("Bạn vừa chuyển CLS thành công"), false);
                        }
                    }
                    else
                    {
                        if (cmdConfirm.Tag.ToString() == "2")
                        {
                            foreach (GridEXRow row in grdAssignDetail.GetCheckedRows())
                            {
                                int id_chidinh =
                                    Utility.Int32Dbnull(row.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value, -1);
                                bool hasFound = false;
                                Utility.WaitNow(this);
                                DataTable dt =
                                    SPs.KcbThamkhamGetchidinh(Utility.Int64Dbnull(txtPatient_ID.Text),
                                        Utility.sDbnull(txtPatient_Code.Text), id_chidinh).GetDataSet().Tables[0];
                                if (dt.Rows.Count <= 0)
                                {
                                    Utility.SetMsg(lblMsg, string.Format("Không có dịch vụ CLS trong phiếu chỉ định đang chọn có thể hủy chuyển"),
                                                   false);
                                    Utility.DefaultNow(this);
                                    return;
                                }
                                DataRow[] arrDr =
                                    m_dtAssignDetail.Select("trangthai_chuyencls in (1,2) and id_chidinh = '" +
                                                            id_chidinh + "'");
                                foreach (DataRow dr in arrDr)
                                {
                                    dr["trangthai_chuyencls"] = 0;
                                }
                                m_dtAssignDetail.AcceptChanges();
                                SPs.KcbClsHuychuyencls(Utility.Int64Dbnull(objLuotkham.IdBenhnhan),
                               Utility.sDbnull(objLuotkham.MaLuotkham), Utility.Int64Dbnull(id_chidinh),
                               globalVariables.SysDate, globalVariables.UserName).Execute();
                                Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy chuyển CLS của bệnh nhân có mã lần khám {0} và ID bệnh nhân là: {1} tên= {2}", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan), newaction.CancelData, this.GetType().Assembly.ManifestModule.Name);

                                Utility.SetMsg(lblMsg, string.Format("Bạn vừa hủy chuyển CLS thành công"), false);
                            }
                        }
                        if (cmdConfirm.Tag.ToString() == "3")
                        {
                        }
                    }
                }
                //ModifyCommmands();
                TuybiennutchuyenCLS();
                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
                Utility.DefaultNow(this);
            }
            finally
            {
                GC.Collect();
            }
        }
        private void TuybiennutchuyenCLS()
        {
            int idChidinh = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
            cmdHuyChuyenCLS.Enabled = false;
            cmdHuyChuyenCLS.Enabled = false;
            if (m_dtAssignDetail.Select("trangthai_chuyencls in(1,2) and id_chidinh = '" + idChidinh + "'").Length >
                0)
            {
                cmdHuyChuyenCLS.Enabled = true;
                cmdHuyChuyenCLS.Text = @"Hủy chuyển CLS";
                cmdHuyChuyenCLS.Tag = 2;
            }
            if (m_dtAssignDetail.Select("trangthai_chuyencls=0 and id_chidinh = '" + idChidinh + "'").Length > 0)
            {
                cmdChuyenCLS.Enabled = true;
                cmdChuyenCLS.Text = @"Chuyển CLS";
                cmdChuyenCLS.Tag = 1;
            }
        }
       
        void txtSPO2_TextChanged(object sender, EventArgs e)
        {
            Utility.CanhbaoSPO2(dtSPO2, txtSPO2.Text, lblSPO2);
        }
       
        void txtibm_TextChanged(object sender, EventArgs e)
        {
            Utility.CanhbaoBMI(dtBMI, txtBMI.Text, lblMotaBMI);
        }

        void grdCongkham_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Utility.isValidGrid(grdCongkham))
                ShowHistory();
        }
        void mnuChanthuchienCLS_Click(object sender, EventArgs e)
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
                string noidung = string.Join("\r\n", query);
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn chặn các dịch vụ CLS dưới đây(Sau khi chặn, các khoa phòng XN,CĐHA,TDCN KHÔNG thể lấy dịch vụ CLS về để thực hiện cho người bệnh)?:\r\n{0}", noidung),
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

        void mnuHuychanthuchienCLS_Click(object sender, EventArgs e)
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
                string noidung = string.Join("\r\n", query);
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn hủy chặn các dịch vụ CLS dưới đây(Sau khi hủy chặn, các khoa phòng XN,CĐHA,TDCN CÓ thể lấy dịch vụ CLS về để thực hiện cho người bệnh)?:\r\n{0}", noidung),
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
        void grdAssignDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                //Kiểm tra xem bản ghi đã thanh toán hay chưa?
                long id_chitietchidinh = Utility.Int64Dbnull(grdAssignDetail.GetValue("id_chitietchidinh"), -1);
                if (id_chitietchidinh > 0)
                {
                    KcbChidinhclsChitiet objchitiet = KcbChidinhclsChitiet.FetchByID(id_chitietchidinh);
                    if (objchitiet != null && Utility.ByteDbnull(objchitiet.TrangthaiThanhtoan, 0) > 0)
                    {
                        Utility.ShowMsg("Bản ghi đã được thanh toán nên bạn không được phép chỉnh sửa số lượng hoặc đơn giá");
                        e.Value = e.InitialValue;
                        return;
                    }
                }

                if (e.Column.Key == KcbChidinhclsChitiet.Columns.SoLuong)
                {
                    //if (!Utility.IsNumeric(e.Value.ToString()))
                    //{
                    //    Utility.ShowMsg("Bạn phải số lượng phải là số", "Thông báo", MessageBoxIcon.Warning);
                    //    e.Cancel = true;
                    //}
                    //decimal quanlity = Utility.DecimaltoDbnull(e.InitialValue, 1);
                    //decimal quanlitynew = Utility.DecimaltoDbnull(e.Value);
                    //if (quanlitynew <= 0)
                    //{
                    //    Utility.ShowMsg("Bạn phải số lượng phải >0", "Thông báo", MessageBoxIcon.Warning);
                    //    e.Value = e.InitialValue;
                    //}
                    //GridEXRow _row = grdAssignDetail.CurrentRow;
                    //string ten_dvu = _row.Cells["ten_chitietdichvu"].Value.ToString();
                    //_row.Cells["TT_BHYT"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BhytChitra].Value, 0)) * quanlitynew;
                    //_row.Cells["TT_BN"].Value =
                    //    (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0) +
                    //     Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * quanlitynew;
                    //_row.Cells["TT_PHUTHU"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * quanlitynew;
                    //_row.Cells["TT_KHONG_PHUTHU"].Value = Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) * (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0)) / 100 * quanlitynew;
                    //_row.Cells["TT_BN_KHONG_PHUTHU"].Value = Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0) * quanlitynew;

                    //_row.Cells["TT"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) *
                    //               (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0) / 100) +
                    //               Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * quanlitynew;
                    //grdAssignDetail.UpdateData();
                    //Utility.Log(this.Name, globalVariables.UserName, string.Format("Sửa số lượng dịch vụ cận lâm sàng {0} từ {1} thành {2} thành công ", ten_dvu, Utility.FormatCurrencyHIS(quanlity), Utility.FormatCurrencyHIS(quanlitynew)), newaction.Update, this.GetType().Assembly.ManifestModule.Name);

                }
                else if (e.Column.Key == KcbChidinhclsChitiet.Columns.DonGia)
                {
                    if (!Numbers.IsNumber(e.Value.ToString()))
                    {
                        Utility.ShowMsg("Bạn phải nhập thông tin đơn giá. Vui lòng nhập lại", "Thông báo", MessageBoxIcon.Warning);
                        e.Value = e.InitialValue;
                    }
                    decimal dongia_cu = Utility.DecimaltoDbnull(e.InitialValue, 1);
                    decimal dongia_moi = Utility.DecimaltoDbnull(e.Value);
                    if (dongia_moi <= 0)
                    {
                        Utility.ShowMsg("Đơn giá phải >0. Vui lòng nhập lại", "Thông báo", MessageBoxIcon.Warning);
                        e.Value = e.InitialValue;
                    }
                    GridEXRow _row = grdAssignDetail.CurrentRow;
                    string ten_dvu = _row.Cells["ten_chitietdichvu"].Value.ToString();
                    int so_luong = Utility.Int32Dbnull(_row.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 0);
                    _row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value = dongia_moi;
                    //_row.Cells["TT_BHYT"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BhytChitra].Value, 0)) * so_luong;
                    //_row.Cells["TT_BN"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0) + Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * so_luong;
                    //_row.Cells["TT_PHUTHU"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * so_luong;
                    //_row.Cells["TT_KHONG_PHUTHU"].Value = (dongia_moi * Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0) / 100) * so_luong;
                    ////_row.Cells["TT_BN_KHONG_PHUTHU"].Value = Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0) * quanlitynew;

                    _row.Cells["TT"].Value = (dongia_moi *
                                   Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * so_luong;
                    grdAssignDetail.UpdateData();
                    int record = new Update(KcbChidinhclsChitiet.Schema)
                        .Set(KcbChidinhclsChitiet.Columns.DonGia).EqualTo(dongia_moi)
                        .Set(KcbChidinhclsChitiet.Columns.BnhanChitra).EqualTo(dongia_moi)
                        .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(id_chitietchidinh).Execute();
                    if (record > 0)
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Sửa đơn giá dịch vụ cận lâm sàng {0} từ {1} thành {2} thành công ", ten_dvu, Utility.FormatCurrencyHIS(dongia_cu), Utility.FormatCurrencyHIS(dongia_moi)), newaction.Update, this.GetType().Assembly.ManifestModule.Name);


                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void txtCanhbao_GotFocus(object sender, EventArgs e)
        {
            txtCanhbao.PasswordChar = '\0';
        }

        void txtCanhbao_LostFocus(object sender, EventArgs e)
        {
            txtCanhbao.PasswordChar = '*';
        }
        private void cmdSaveWarning_Click(object sender, EventArgs e)
        {
            try
            {
                if (objBenhnhan == null || objLuotkham == null)
                {
                    Utility.ShowMsg("Mời bạn chọn người bệnh cần thăm khám trước khi cập nhật thông tin này");
                    return;
                }
                string oldValue = objBenhnhan.CanhBao;
                new Update(KcbDanhsachBenhnhan.Schema).Set(KcbDanhsachBenhnhan.Columns.CanhBao).EqualTo(Utility.sDbnull(txtCanhbao.Text)).Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan).IsEqualTo(objBenhnhan.IdBenhnhan).Execute();
                Utility.Log(this.Name, globalVariables.UserName, string.Format("cập nhật thông tin cảnh báo cho người bệnh ID={0}, PID={1}, Tên={2} từ {3} thành {4} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan, oldValue,Utility.sDbnull(txtCanhbao.Text)), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                Utility.ShowMsg("Cập nhật thông tin thành công. Nhấn OK để kết thúc");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        void txtID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.sDbnull(txtID.Text).Length > 0)
                cmdSearch.PerformClick();
        }

        void grdAssignDetail_EditingCell(object sender, EditingCellEventArgs e)
        {
            
        }

        void grdList_SelectionChanged(object sender, EventArgs e)
        {
            mnuXoacongkham.Enabled =Utility.isValidGrid(grdList) && Utility.sDbnull(grdList.GetValue("isme"), "0") == "1";
        }

        void grdVTTH_SelectionChanged(object sender, EventArgs e)
        {
            RowVTTH = findthelastChild(grdVTTH.CurrentRow);
            ModifyCommmands();
        }

        void grdPresDetail_SelectionChanged(object sender, EventArgs e)
        {
            RowThuoc = findthelastChild(grdPresDetail.CurrentRow);
            ModifyCommmands();
        }

        void grdAssignDetail_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
            int a = 1;
        }
        private void _Click(object sender, EventArgs e)
        {
            try
            {
                if (((ToolStripMenuItem)sender).Name == mnuKhamlai.Name)
                    Huyketthuckham();
                else if (((ToolStripMenuItem)sender).Name == mnuKetthuckham.Name)
                    KetthucKham();
                else if (((ToolStripMenuItem)sender).Name == mnuNhapvien.Name)
                    Nhapvien();
                else if (((ToolStripMenuItem)sender).Name == mnuHuynhapvien.Name)
                    Huynhapvien();
                else if (((ToolStripMenuItem)sender).Name == mnuThemPK.Name)
                    Them_ChuyenPK(false);
                else if (((ToolStripMenuItem)sender).Name == mnuChuyenPK.Name)
                    Them_ChuyenPK(true);
                else if (((ToolStripMenuItem)sender).Name == mnuInphieuhenkham.Name)
                    Inphieuhenkham();
                else if (((ToolStripMenuItem)sender).Name == mnuInphieutuvan.Name)
                    Inphieudinhduong();
                else if (((ToolStripMenuItem)sender).Name == mnuIntomtatdieutri.Name)
                    Intomtatdieutri();
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
        void autoTrangthai__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(autoTrangthai.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = autoTrangthai.myCode;
                autoTrangthai.Init();
                autoTrangthai.SetCode(oldCode);
                autoTrangthai.Focus();
            }
        }

        void autoXutri__OnShowData()
        {
            //var dmucDchung = new DMUC_DCHUNG(autoXutri.LOAI_DANHMUC);
            //dmucDchung.ShowDialog();
            //if (!dmucDchung.m_blnCancel)
            //{
            //    string oldCode = autoXutri.myCode;
            //    autoXutri.Init();
            //    autoXutri.SetCode(oldCode);
            //    autoXutri.Focus();
            //}
        }

        void autoLoidan__OnShowData()
        {
            //var dmucDchung = new DMUC_DCHUNG(autoLoidan.LOAI_DANHMUC);
            //dmucDchung.ShowDialog();
            //if (!dmucDchung.m_blnCancel)
            //{
            //    string oldCode = autoLoidan.myCode;
            //    autoLoidan.Init();
            //    autoLoidan.SetCode(oldCode);
            //    autoLoidan.Focus();
            //}
        }

        void frm_KCB_THAMKHAM_V2_Shown(object sender, EventArgs e)
        {
            Utility.WaitNow(this);
            Try2Splitter();
            LoadUserConfigs();
            splitContainer4.Panel2Collapsed = !chkHienthilichsucongkham.Checked;
            LoadData();
            Application.DoEvents();
            Utility.DefaultNow(this);
        }
        int SplitterKQ = -1;
        void Try2Splitter()
        {
            try
            {


                List<int> lstSplitterSize = (from p in File.ReadLines(SplitterPath)
                                             select Utility.Int32Dbnull(p)).ToList<int>();
                if (lstSplitterSize != null )
                {
                    if (lstSplitterSize.Count > 0) splitContainer1.SplitterDistance = lstSplitterSize[0];
                    if (lstSplitterSize.Count > 1) splitContainer3.SplitterDistance = lstSplitterSize[1];
                    SplitterKQ = splitContainer3.SplitterDistance;
                   if(lstSplitterSize.Count>2) splitContainer4.SplitterDistance = lstSplitterSize[2];
                }
            }
            catch (Exception)
            {

            }
        }
        void txtMaluotkham_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    LayThongTinQuaMaLanKham(Utility.AutoFullPatientCode(txtMaluotkham.Text));
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin bệnh nhân");
            }
            finally
            {
                ModifyCommmands();
            }
        }

        void cboKieuin_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.KieuInDonthuoc = cboKieuin.SelectedIndex == 0 ? KieuIn.Innhiet : KieuIn.InLaser;
            PropertyLib.SavePropertyV1( PropertyLib._MayInProperties);
        }

        void autoMabenhphu__OnEnterMe()
        {
            if (autoMabenhphu.MyID.ToString() != "-1")
            {
                txtMaBenhphu.Text = autoMabenhphu.MyCode;
                txtMaBenhphu_KeyDown(txtMaBenhphu, new KeyEventArgs(Keys.Enter));
                autoMabenhphu.Focus();
                autoMabenhphu.SelectAll();
                txtMaBenhphu.Focus();
                //txtMaBenhphu.SelectAll();
                
            }
            else
            {
                txtMaBenhphu.Text = "";
                cboKQDT.Focus();
            }
        }
        bool kiemtra = true;
        void AutoMabenhchinh__OnEnterMe()
        {
           

        }
        void grdKetqua_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                List<KcbKetquaCl> lstResult = new List<KcbKetquaCl>();
                List<KcbChidinhclsChitiet> lstDetails = new List<KcbChidinhclsChitiet>();
                int id_kq = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, KcbKetquaCl.Columns.IdKq), -1);
                int IdChitietchidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                int IdChitietchidinhcha = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                int CoChitiet = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, DmucDichvuclsChitiet.Columns.CoChitiet), -1);

                int IdChitietdichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, DmucDichvuclsChitiet.Columns.IdChitietdichvu), -1);
                KcbKetquaCl _item = null;
                KcbChidinhclsChitiet _itemchitiet = KcbChidinhclsChitiet.FetchByID(IdChitietchidinh);
                KcbChidinhclsChitiet _itemchitietcha = null;
                if (CoChitiet == 1)
                {
                    _itemchitietcha = KcbChidinhclsChitiet.FetchByID(IdChitietchidinhcha);
                    if (_itemchitietcha != null)
                    {
                        _itemchitietcha.IsNew = false;
                        _itemchitietcha.MarkOld();
                    }
                }
                _itemchitiet.IsNew = false;
                _itemchitiet.MarkOld();
                if (id_kq > 0)
                {
                    _item = KcbKetquaCl.FetchByID(id_kq);
                    _item.IsNew = false;
                    _item.NguoiSua = globalVariables.UserName;
                    _item.NgaySua = globalVariables.SysDate;
                    _item.IpMaysua = globalVariables.gv_strIPAddress;
                    _item.TenMaysua = globalVariables.gv_strComputerName;
                    _item.MarkOld();
                }
                else
                {
                    _item = new KcbKetquaCl();
                    _item.IsNew = true;
                    _item.NguoiTao = globalVariables.UserName;
                    _item.NgayTao = globalVariables.SysDate;
                    _item.IpMaytao = globalVariables.gv_strIPAddress;
                    _item.TenMaytao = globalVariables.gv_strComputerName;
                }
                DmucDichvuclsChitiet objcls = DmucDichvuclsChitiet.FetchByID(IdChitietdichvu);
                if (objcls != null)
                {
                    _item.MaChidinh = Utility.GetValueFromGridColumn(grdAssignDetail, KcbChidinhcl.Columns.MaChidinh);
                    _item.MaBenhpham = Utility.GetValueFromGridColumn(grdAssignDetail, KcbChidinhcl.Columns.MaBenhpham);
                    _item.Barcode = Utility.GetValueFromGridColumn(grdAssignDetail, KcbChidinhcl.Columns.Barcode);
                    _item.IdBenhnhan = objBenhnhan.IdBenhnhan;
                    _item.MaLuotkham = objLuotkham.MaLuotkham;
                    _item.IdChidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                    _item.IdChitietchidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                    _item.IdDichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, KcbChidinhclsChitiet.Columns.IdDichvu), -1);
                    _item.IdDichvuchitiet = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, KcbChidinhclsChitiet.Columns.IdChitietdichvu), -1);
                    _item.Barcode = Utility.GetValueFromGridColumn(grdAssignDetail, KcbChidinhcl.Columns.Barcode);
                    _item.SttIn = objcls.SttHthi;
                    _item.BtNam = objcls.BinhthuongNam;
                    _item.BtNu = objcls.BinhthuongNu;
                    _item.KetQua = Utility.sDbnull(e.Value, "");
                    if (_item.TrangThai < 3)
                        _item.TrangThai = 3;
                    if (true)
                        _item.TrangThai = 4;//Duyệt luôn để hiển thị trên form thăm khám của bác sĩ

                    if (Utility.DoTrim(_item.KetQua) == "")
                        _item.TrangThai = 2;//Quay ve trang thai chuyen đang thực hiện
                    //_item.TenDonvitinh = objcls.TenDonvitinh;
                    _itemchitiet.KetQua = Utility.sDbnull(e.Value, "");
                    if (_itemchitiet.TrangThai < 3)
                        _itemchitiet.TrangThai = 3;
                    if (true)
                        _itemchitiet.TrangThai = 4;//Duyệt luôn để hiển thị trên form thăm khám của bác sĩ
                    if (Utility.DoTrim(_itemchitiet.KetQua) == "")
                        _itemchitiet.TrangThai = 1;//Quay ve trang thai chuyen can

                    if (_itemchitietcha != null && _itemchitietcha.TrangThai < 3)
                        _itemchitietcha.TrangThai = 3;
                    if (_itemchitietcha != null && true)
                        _itemchitietcha.TrangThai = 4;//Duyệt luôn để hiển thị trên form thăm khám của bác sĩ
                    if (_itemchitietcha != null && Utility.DoTrim(Utility.sDbnull(e.Value, "")) == "")
                        _itemchitietcha.TrangThai = 1;//Quay ve trang thai chuyen can

                    _item.TenThongso = "";
                    _item.TenKq = "";
                    _item.LoaiKq = 0;
                    _item.ChophepHienthi = 1;
                    _item.ChophepIn = 1;
                    _item.MotaThem = objcls.MotaThem;
                    lstResult.Add(_item);
                    lstDetails.Add(_itemchitiet);
                    if (_itemchitietcha != null)
                        lstDetails.Add(_itemchitietcha);
                    if (clsXN.UpdateResult(lstResult, lstDetails) != ActionResult.Success)
                        e.Cancel = true;
                }
            }
            catch (Exception)
            {


            }
        }
        void optAll_CheckedChanged(object sender, EventArgs e)
        {
            cmdSearch_Click(cmdSearch, e);
        }

        void txtLoaiDichvu__OnSelectionChanged()
        {
            try
            {
                DataTable dtdata = SPs.DmucLaydanhmucDichvuclsChitiet(1, Utility.Int32Dbnull(txtLoaiDichvu.MyID, 0)).GetDataSet().Tables[0];
                txtDichvu.Init(dtdata, new List<string>() { DmucDichvuclsChitiet.Columns.IdChitietdichvu, DmucDichvuclsChitiet.Columns.MaChitietdichvu, DmucDichvuclsChitiet.Columns.TenChitietdichvu });
            }
            catch
            {
            }
        }

        void mnuCancelResult_Click(object sender, EventArgs e)
        {
            if (Utility.AcceptQuestion("Bạn có chắc chắn muốn hủy trạng thái nhập KQ CĐHA hay không?", "Xác nhận hủy", true))
            {
                int idChidinhchitiet =Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
                KcbChidinhclsChitiet _objtemp = KcbChidinhclsChitiet.FetchByID(idChidinhchitiet);
                if (_objtemp != null)
                {
                    _objtemp.MarkOld();
                    _objtemp.TrangThai = 0;
                    _objtemp.KetLuanCdha = "";
                    _objtemp.KetQua = "";
                    _objtemp.NgayThuchien = null;
                    _objtemp.NguoiThuchien = "";
                    _objtemp.IdVungks = "-1";
                    using (var scope = new TransactionScope())
                    {
                        using (var sh = new SharedDbConnectionScope())
                        {
                            _objtemp.Save();
                            SPs.ClsCdhaDelete(_objtemp.IdChitietchidinh).Execute();
                        }
                        scope.Complete();
                    }
                }
                new Delete().From(DynamicValue.Schema).Where(DynamicValue.Columns.IdChidinhchitiet).IsEqualTo(_objtemp.IdChitietchidinh).Execute();
            }
        }

       

        void mnuCheck_UncheckAll_Click(object sender, EventArgs e)
        {
            if (grdAssignDetail.GetCheckedRows().Count() > 0)
                grdAssignDetail.UnCheckAllRecords();
            else
                grdAssignDetail.CheckAllRecords();
        }

        void mnuInvert_Click(object sender, EventArgs e)
        {
            foreach (GridEXRow _row in grdAssignDetail.GetDataRows())
            {
                _row.BeginEdit();
                _row.IsChecked = !_row.IsChecked;
                _row.EndEdit();
            }
        }
        void LoadUserConfigs()
        {
            try
            {
                chkHienthilichsucongkham.Checked = Utility.getUserConfigValue(chkHienthilichsucongkham.Tag.ToString(), Utility.Bool2byte(chkHienthilichsucongkham.Checked)) == 1;
               
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
                Utility.SaveUserConfig(chkHienthilichsucongkham.Tag.ToString(), Utility.Bool2byte(chkHienthilichsucongkham.Checked));
              
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void frm_KCB_THAMKHAM_V2_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
            Utility.SaveValue2Lines(SplitterPath, new List<string>() { splitContainer1.SplitterDistance.ToString(), splitContainer3.SplitterDistance.ToString(), splitContainer4.SplitterDistance.ToString() });

            Utility.FreeLockObject(m_strMaLuotkham);
            if (PropertyLib._ThamKhamProperties.Tudongluukhithoatform)
            {
                PropertyLib._ThamKhamProperties.Dorongbentrai = splitContainer1.SplitterDistance;
                PropertyLib._ThamKhamProperties.Chieucaoluoitimkiem = splitContainer2.SplitterDistance;
                PropertyLib.SavePropertyV1( PropertyLib._ThamKhamProperties);
            }
            timer1.Stop();
            //this.Dispose();
        }

        void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._HinhAnhProperties);
            _Properties.ShowDialog();
            InitFtp();
        }

        void lnkMore_Click(object sender, EventArgs e)
        {
            //BeginExam();
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
        void mnuNhapKQCDHA_Click(object sender, EventArgs e)
        {
            //BeginExam();
        }

       
        private void grdAssignDetail_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //BeginExam();
        }
        bool isvalidKhamsobo(byte savedType)
        {
            try
            {
                if (objLuotkham == null || objCongkham == null)
                {
                    Utility.ShowMsg("Bạn cần chọn một người bệnh trên danh sách phía bên trái màn hình để bắt đầu thực hiện khám");
                    return false;
                }
                if (dtpThoigian_batdau.Value < dtpNgaydangky.Value)
                {
                    Utility.ShowMsg("Thời gian bắt đầu khám phải sau thời gian đăng ký. Vui lòng kiểm tra lại");
                    dtpThoigian_batdau.Focus();
                    return false;
                }
                if (dtpThoigian_batdau.Value > dtpThoigianKetthuc.Value)
                {
                    Utility.ShowMsg("Thời gian kết thúc khám phải sau thời gian bắt đầu khám. Vui lòng kiểm tra lại");
                    dtpThoigianKetthuc.Focus();
                    return false;
                }
                if (!globalVariables.isSuperAdmin)
                {
                    if (objCongkham.TrangThai == 1)
                    {
                        Utility.ShowMsg("Bệnh nhân đã kết thúc khám nên bạn chỉ được quyền xem thông tin");
                        return false;
                    }
                    if (objLuotkham.TrangthaiNoitru >= 1)
                    {
                        Utility.ShowMsg("Bệnh nhân đã nhập viện điều trị nội trú nên bạn chỉ được quyền xem thông tin");
                        return false;
                    }
                    if (objCongkham.TrangThai == 1 && objLuotkham.TthaiChuyendi == 1)
                    {
                        Utility.ShowMsg("Bệnh nhân đã chuyển viện nên bạn chỉ được quyền xem thông tin");
                        return false;
                    }
                }
                if (Utility.Laygiatrithamsohethong("CANHBAO_CHUCNANGSONG", "0", true) == "1" && savedType!=1)
                {
                    decimal value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtMach.Text), -1);
                    List<string> lstRange = Utility.Laygiatrithamsohethong("MACH", "5-70", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtMach.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Mạch có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép từ {0}-{1}. Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtMach.Focus();
                    }
                    value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtNhietDo.Text), -1);
                    lstRange = Utility.Laygiatrithamsohethong("NHIETDO", "34-43", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtNhietDo.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Nhiệt độ có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép từ {0}-{1}. Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtNhietDo.Focus();
                    }
                    value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtHa.Text), -1);
                    lstRange = Utility.Laygiatrithamsohethong("HUYETAP", "40-250", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtHa.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Huyết áp có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép từ {0}-{1}. Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtHa.Focus();
                    }
                    value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtNhipTho.Text), -1);
                    lstRange = Utility.Laygiatrithamsohethong("NHIPTHO", "40-250", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtNhipTho.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Nhịp thở có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép từ {0}-{1}. Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtNhipTho.Focus();
                    }
                    value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtChieucao.Text), -1);
                    lstRange = Utility.Laygiatrithamsohethong("CHIEUCAO", "10-250", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtChieucao.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Chiều cao có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép chiều cao từ {0}(cm)-{1}(cm). Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtChieucao.Focus();
                    }
                    value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtCannang.Text), -1);
                    lstRange = Utility.Laygiatrithamsohethong("CANNANG", "1-150", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtCannang.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Cân nặng có thể chưa chuẩn xác. Hệ thống đang xác lập mức cân nặng từ {0}(kg)-{1}(kg). Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtCannang.Focus();
                    }
                    value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtNhipTim.Text), -1);
                    lstRange = Utility.Laygiatrithamsohethong("NHIPTIM", "40-130", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtNhipTim.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Nhịp tim có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép từ {0}(kg)-{1}(kg). Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtNhipTim.Focus();
                    }
                    if (Utility.DoTrim(txtNhommau.Text).Length > 0 && txtNhommau.MyCode == "-1")
                    {
                        Utility.ShowMsg(string.Format("Sai thông tin nhóm máu. Yêu cầu nhập lại hoặc xóa trắng nếu không muốn nhập"), "Cảnh báo");
                        txtNhommau.Focus();
                    }
                   
                }
                if (savedType !=0)
                {
                    if (Utility.sDbnull( cboHDT.SelectedValue).ToUpper() != THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_MAHUONGDIEUTRI_CHUYENVIEN", false).ToUpper() && objLuotkham.TthaiChuyendi == 1)
                    {
                        Utility.ShowMsg("Bệnh nhân đã chuyển viện nên bạn muốn chọn hướng điều trị khác thì cần hủy chuyển viện.");
                        return false;
                    }
                    if (!THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_MAHUONGDIEUTRI_NOITRU", false).ToUpper().Split(',').ToList<string>().Contains(Utility.sDbnull(cboHDT.SelectedValue).ToUpper()) && objLuotkham.TrangthaiNoitru >= 1)
                    {
                        Utility.ShowMsg("Bệnh nhân đã nhập viện điều trị nội trú nên bạn muốn chọn hướng điều trị khác thì cần hủy nhập viện");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">0= lưu hỏi bệnh và chẩn đoán;1= Lưu kết luận;2= lưu tất cả</param>
        void LuuChanDoan_KL(byte savedType)
        {
            

            cmdLuuKetluan.Enabled = false;
            cmdLuuChandoan_sobo.Enabled = false;
            errorProvider1.Clear();
            Utility.SetMsg(lblkhamsobo, "", false);
            Utility.SetMsg(lblMsg, "", false);
            if (!isvalidKhamsobo(savedType)) return;

            if (Utility.Coquyen("quyen_khamtatcacacphong_ngoaitru") || globalVariables.StrQheNvpk.Split(',').ToList<string>().Contains(Utility.sDbnull(objCongkham.IdPhongkham))) //!=          Utility.Int16Dbnull(globalVariables.IdPhongNhanvien)))
            {
            }
            else
            {
                Utility.ShowMsg(string.Format("Người dùng không được phân quyền khám tất cả các phòng khám ngoại trú(quyen_khamtatcacacphong_ngoaitru) hoặc chưa được gán khám tại phòng {0}", txtPhongkham.Text), "Thông báo");
                return;
            }
            DataTable dtkt = SPs.KcbGetthongtinLuotkham(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan).GetDataSet().Tables[0];
            if (dtkt.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tồn tại bệnh nhân! Bạn cần nạp lại thông tin dữ liệu", "Thông Báo");
                return;
            }
            else
            {

                TaoDulieuChandoanKetluan(savedType);
                if (savedType == 0)
                {
                    KcbChandoanKetluan objCompare = new Select().From(KcbChandoanKetluan.Schema)
                                         .Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(objCongkham.IdKham).ExecuteSingle<KcbChandoanKetluan>();
                    if (objCompare != null &&
                       ((Utility.sDbnull(_KcbChandoanKetluan.Mach) != Utility.sDbnull(objCompare.Mach))
                       || (Utility.sDbnull(_KcbChandoanKetluan.Nhietdo) != Utility.sDbnull(objCompare.Nhietdo))
                       || (Utility.sDbnull(_KcbChandoanKetluan.Huyetap) != Utility.sDbnull(objCompare.Huyetap))
                       || (Utility.sDbnull(_KcbChandoanKetluan.Nhiptho) != Utility.sDbnull(objCompare.Nhiptho))
                       || (Utility.sDbnull(_KcbChandoanKetluan.Cannang) != Utility.sDbnull(objCompare.Cannang))
                       || (Utility.sDbnull(_KcbChandoanKetluan.Chieucao) != Utility.sDbnull(objCompare.Chieucao))
                       || (Utility.sDbnull(_KcbChandoanKetluan.Nhommau) != Utility.sDbnull(objCompare.Nhommau))
                       || (Utility.sDbnull(_KcbChandoanKetluan.Chandoan) != Utility.sDbnull(objCompare.Chandoan))
                       || (Utility.sDbnull(_KcbChandoanKetluan.ChandoanKemtheo) != Utility.sDbnull(objCompare.ChandoanKemtheo))
                       || (Utility.sDbnull(_KcbChandoanKetluan.NhanXet) != Utility.sDbnull(objCompare.NhanXet))
                       || (Utility.sDbnull(_KcbChandoanKetluan.QuatrinhBenhly) != Utility.sDbnull(objCompare.QuatrinhBenhly))
                        || (Utility.sDbnull(_KcbChandoanKetluan.TiensuBenh) != Utility.sDbnull(objCompare.TiensuBenh)))
                       )

                    {
                        if (!Utility.AcceptQuestion("Hệ thống phát hiện bạn đang sửa dữ liệu chuẩn đoán sơ bộ.\r\nNhấn No để tạm dừng việc lưu và kiểm tra lại.\r\nNhấn Yes để chắc chắn tiếp tục lưu thông tin khám sơ bộ mà bạn đang nhập", "Xác nhận", true))
                        {
                            return;
                        }
                    }


                }
                ActionResult act = _KCB_THAMKHAM.LuuHoibenhvaChandoan(_KcbChandoanKetluan, objLuotkham, null, objCongkham, false, Utility.Byte2Bool(savedType));
                if (act == ActionResult.Success)
                {
                    if (savedType != 1)
                    {
                        string log_UI = string.Format("mạch={0}, nhiệt độ={1}, Huyết áp={2}, Nhịp thở={3}, Cân nặng={4}, Chiều cao={5},para={6}, quai bị={7}, nhóm máu={8}, BMI={9}, chẩn đoán sơ bộ={10}, chẩn đoán phụ ={11}, Dị ứng thuốc={12}, QTBL={13},TS bệnh={14}",
                            txtMach.Text, txtNhietDo.Text, txtHa.Text, txtNhipTho.Text, txtCannang.Text, txtChieucao.Text, txtPara.Text, chkQuaibi.Checked.ToString(), txtNhommau.Text, txtBMI.Text, txtChanDoan.Text, txtChanDoanKemTheo.Text, txtNhanxet.Text, txtQuatrinhbenhly.Text, txtTiensubenh.Text);
                        string log_OBJ = "";
                        string log_infor = string.Format("Lưu chẩn đoán-kết luận của bệnh nhân ID={0}, PID={1}, Tên={2}  và các dữ liệu UI: {3} - AppsessionID: {4} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan, log_UI, globalVariables.AppSessionID);
                        Utility.Log(this.Name, globalVariables.UserName, log_infor, _KcbChandoanKetluan.IdChandoan <= 0 ? newaction.Insert : newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                    }
                    //Lấy lại thông tin tránh lỗi lưu lần 2 khi nhấn kết thúc khám
                    _KcbChandoanKetluan = new Select().From(KcbChandoanKetluan.Schema)
                         .Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(objCongkham.IdKham).
                         ExecuteSingle
                         <KcbChandoanKetluan>();
                    Utility.SetMsg(lblkhamsobo, "Bạn đã lưu thông tin khám thành công", false);
                    Utility.SetMsg(lblMsg, "Bạn đã lưu thông tin khám thành công", false);

                }
            }
            cmdLuuChandoan_sobo.Enabled = true;
            cmdLuuKetluan.Enabled = true;
        }
        private void cmdLuuChandoan_Click(object sender, EventArgs e)
        {

            LuuChanDoan_KL(1);
        }

        private void grdPresDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            if (!Utility.isValidGrid(grdPresDetail)) return;
            if (e.Column.Key == "stt_in")
            {
                long idChitietdonthuoc =
                    Utility.Int64Dbnull(
                        grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, 0);
                if (idChitietdonthuoc > -1)
                    new KCB_KEDONTHUOC().Capnhatchidanchitiet(idChitietdonthuoc, KcbDonthuocChitiet.Columns.SttIn,
                                                              e.Value.ToString());
                grdPresDetail.UpdateData();
            }
            if (e.Column.Key == "mota_them_chitiet")
            {
                long idChitietdonthuoc =
                    Utility.Int64Dbnull(
                        grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, 0);
                if (idChitietdonthuoc > -1)
                    new KCB_KEDONTHUOC().Capnhatchidanchitiet(idChitietdonthuoc, KcbDonthuocChitiet.Columns.MotaThem,
                                                              e.Value.ToString());
                grdPresDetail.UpdateData();
            }
        }

       

        private void mnuShowResult_Click(object sender, EventArgs e)
        {
            if (RowCLS == null || objLuotkham == null || objCongkham == null) return;
            frm_PdfViewer _PdfViewer = new frm_PdfViewer(0);
             _PdfViewer.ma_luotkham = objLuotkham.MaLuotkham;
            _PdfViewer.ma_chidinh =  Utility.sDbnull(RowCLS.Cells[KcbChidinhcl.Columns.MaChidinh].Value);
            _PdfViewer.ShowDialog();
            //mnuShowResult.Tag = mnuShowResult.Checked ? "1" : "0";
            //if (PropertyLib._ThamKhamProperties.HienthiKetquaCLSTrongluoiChidinh)
            //{
            //    Utility.ShowColumns(grdAssignDetail, mnuShowResult.Checked ? lstResultColumns : lstVisibleColumns);
            //}
            //else
            //    grdAssignDetail_SelectionChanged(grdAssignDetail, e);
        }

        private void txtChanDoanKemTheo__OnSaveAs()
        {
            if (Utility.DoTrim(txtChanDoanKemTheo.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(txtChanDoanKemTheo.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txtChanDoanKemTheo.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtChanDoanKemTheo.myCode;
                txtChanDoanKemTheo.Init();
                txtChanDoanKemTheo.SetCode(oldCode);
                txtChanDoanKemTheo.Focus();
            }
        }

        private void txtChanDoan__OnSaveAs()
        {
            if (Utility.DoTrim(txtChanDoan.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(txtChanDoan.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txtChanDoan.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtChanDoan.myCode;
                txtChanDoan.Init();
                txtChanDoan.SetCode(oldCode);
                txtChanDoan.Focus();
            }
        }

        private void txtNhanxet__OnSaveAs()
        {
            if (Utility.DoTrim(txtNhanxet.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(txtNhanxet.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txtNhanxet.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtNhanxet.myCode;
                txtNhanxet.Init();
                txtNhanxet.SetCode(oldCode);
                txtNhanxet.Focus();
            }
        }

        //private void txtHuongdieutri__OnSaveAs()
        //{
        //    if (Utility.DoTrim(cboHDT.Text) == "") return;
        //    var dmucDchung = new DMUC_DCHUNG(cboHDT.LOAI_DANHMUC);
        //    dmucDchung.SetStatus(true, cboHDT.Text);
        //    dmucDchung.ShowDialog();
        //    if (!dmucDchung.m_blnCancel)
        //    {
        //        string oldCode = cboHDT.myCode;
        //        cboHDT.Init();
        //        cboHDT.SetCode(oldCode);
        //        cboHDT.Focus();
        //    }
        //}

        //private void txtKet_Luan__OnSaveAs()
        //{
        //    if (Utility.DoTrim(cboKQDT.Text) == "") return;
        //    var dmucDchung = new DMUC_DCHUNG(cboKQDT.LOAI_DANHMUC);
        //    dmucDchung.SetStatus(true, cboKQDT.Text);
        //    dmucDchung.ShowDialog();
        //    if (!dmucDchung.m_blnCancel)
        //    {
        //        string oldCode = cboKQDT.myCode;
        //        cboKQDT.Init();
        //        cboKQDT.SetCode(oldCode);
        //        cboKQDT.Focus();
        //    }
        //}

        private void txtNhommau__OnSaveAs()
        {
            if (Utility.DoTrim(txtNhommau.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(txtNhommau.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txtNhommau.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtNhommau.myCode;
                txtNhommau.Init();
                txtNhommau.SetCode(oldCode);
                txtNhommau.Focus();
            }
        }
        private void txtCheDoAn__OnSaveAs()
        {
            if (Utility.DoTrim(txtCheDoAn.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(txtCheDoAn.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txtCheDoAn.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtCheDoAn.myCode;
                txtCheDoAn.Init();
                txtCheDoAn.SetCode(oldCode);
                txtCheDoAn.Focus();
            }
        }
        private void txtNhanxet__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(txtNhanxet.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtNhanxet.myCode;
                txtNhanxet.Init();
                txtNhanxet.SetCode(oldCode);
                txtNhanxet.Focus();
            }
        }

        private void txtNhommau__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(txtNhommau.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtNhommau.myCode;
                txtNhommau.Init();
                txtNhommau.SetCode(oldCode);
                txtNhommau.Focus();
            }
        }

        //private void txtHuongdieutri__OnShowData()
        //{
        //    var dmucDchung = new DMUC_DCHUNG(cboHDT.LOAI_DANHMUC);
        //    dmucDchung.ShowDialog();
        //    if (!dmucDchung.m_blnCancel)
        //    {
        //        string oldCode = cboHDT.myCode;
        //        cboHDT.Init();
        //        cboHDT.SetCode(oldCode);
        //        cboHDT.Focus();
        //    }
        //}

        //private void txtKet_Luan__OnShowData()
        //{
        //    var dmucDchung = new DMUC_DCHUNG(cboKQDT.LOAI_DANHMUC);
        //    dmucDchung.ShowDialog();
        //    if (!dmucDchung.m_blnCancel)
        //    {
        //        string oldCode = cboKQDT.myCode;
        //        cboKQDT.Init();
        //        cboKQDT.SetCode(oldCode);
        //        cboKQDT.Focus();
        //    }
        //}
        private void txtCheDoAn__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(txtCheDoAn.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtCheDoAn.myCode;
                txtCheDoAn.Init();
                txtCheDoAn.SetCode(oldCode);
                txtCheDoAn.Focus();
            }
        }
        private void txtChanDoanKemTheo__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(txtChanDoanKemTheo.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtChanDoanKemTheo.myCode;
                txtChanDoanKemTheo.Init();
                txtChanDoanKemTheo.SetCode(oldCode);
                txtChanDoanKemTheo.Focus();
            }
        }

        private void txtChanDoan__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(txtChanDoan.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtChanDoan.myCode;
                txtChanDoan.Init();
                txtChanDoan.SetCode(oldCode);
                txtChanDoan.Focus();
            }
        }
        void Them_ChuyenPK(bool chuyenPK)
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                string them_chuyen = chuyenPK ? "chuyển" : "thêm";
                if (objCongkham == null || objLuotkham == null || !Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg(string.Format("Bạn cần chọn bệnh nhân trước khi thực hiện {0} phòng khám",them_chuyen), "Thông báo");
                    return;
                }
                if (objCongkham != null && chuyenPK &&  Utility.Byte2Bool(objCongkham.DachidinhCls))
                {
                    Utility.ShowMsg("Đã chỉ định cận lâm sàng nên không thể đổi sang phòng khám khác", "Thông báo");
                    return;
                }
                if (objCongkham != null && chuyenPK && Utility.Byte2Bool(objCongkham.DakeDonthuoc))
                {
                    Utility.ShowMsg("Đã kê đơn nên không thể đổi sang phòng khám khác", "Thông báo");
                    return;
                }
                ////Bỏ theo ý Viet 230615
                if (chuyenPK && objCongkham != null && chuyenPK && Utility.Byte2Bool(objCongkham.DakeDonthuoc))
                {
                    Utility.ShowMsg("Đã kết thúc khám nên không thể đổi sang phòng khám khác", "Thông báo");
                    return;
                }
                //if (objLuotkham == null || !Utility.isValidGrid(grdList))
                //{
                //    Utility.ShowMsg("Không lấy được thông tin người bệnh. Đề nghị kiểm tra lại", "Thông Báo");
                //    return;
                //}
                //Kiểm tra nếu BN chưa kết thúc hoặc bác sĩ chưa thăm khám mới được phép chuyển phòng
                //SqlQuery sqlQuery = new Select().From(KcbDonthuoc.Schema)
                //    .Where(KcbDonthuoc.Columns.IdKham).IsEqualTo(objCongkham.IdKham);
                //if (sqlQuery.GetRecordCount() > 0)
                //{
                //    Utility.ShowMsg("Bác sĩ đã kê đơn thuốc cho lần khám này nên không thể chuyển phòng. Đề nghị hủy đơn thuốc trước khi chuyển phòng khám", "");
                //    return;
                //}
                //sqlQuery = new Select().From(KcbChidinhcl.Schema)
                //    .Where(KcbChidinhcl.Columns.IdKham).IsEqualTo(objCongkham.IdKham);
                //if (sqlQuery.GetRecordCount() > 0)
                //{
                //    Utility.ShowMsg("Bác sĩ đã chỉ định cận lâm sàng cho lần khám này nên không thể chuyển phòng. Đề nghị hủy chỉ định cận lâm sàng trước khi chuyển phòng khám", "");
                //    return;
                //}
                var chuyenPhongkham = new frm_ChuyenPhongkham(chuyenPK);
                chuyenPhongkham.objCongkham_Cu = objCongkham;
                chuyenPhongkham.objBenhnhan = this.objBenhnhan;
                
                chuyenPhongkham.MA_DTUONG = objCongkham.MaDoituongkcb;
                chuyenPhongkham.dongia = objCongkham.TrangthaiThanhtoan > 0 ? objCongkham.DonGia : -1;
                chuyenPhongkham.txtPKcu.Text = txtPhongkham.Text;
                chuyenPhongkham.txtCongkhamCu.Text = txtTenDvuKham.Text;
                chuyenPhongkham.ShowDialog();
                if (!chuyenPhongkham.m_blnCancel)
                {
                    Utility.SetMsg(lblMsg, "Chuyển phòng khám thành công. Mời bạn chọn bệnh nhân khác để thăm khám",
                                   false);
                    DataRow[] arrDr = m_dtDanhsachbenhnhanthamkham.Select("id_kham=" + objCongkham.IdKham.ToString());
                    if (arrDr.Length > 0)
                    {
                        arrDr[0]["ten_phongkham"] = chuyenPhongkham.txtPKMoi.Text;
                        arrDr[0]["id_phongkham"] = chuyenPhongkham.objDichvuKcb.IdPhongkham;
                        m_dtDanhsachbenhnhanthamkham.AcceptChanges();
                    }
                    string filter = "";
                    filter = "id_phongkham=" + Utility.Int32Dbnull(cboPhongKhamNgoaiTru.SelectedValue, -1).ToString();
                    if (Utility.Int32Dbnull(cboPhongKhamNgoaiTru.SelectedValue, -1) > -1)
                    {
                        m_dtDanhsachbenhnhanthamkham.DefaultView.RowFilter = "1=1";
                        m_dtDanhsachbenhnhanthamkham.DefaultView.RowFilter = filter;
                    }
                    else //BN vẫn hiển thị như vậy-->Cần load lại dữ liệu
                    {
                        GetData();
                    }
                    txtPatient_Code.Focus();
                    txtPatient_Code.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }
        private void cmdChuyenPhong_Click(object sender, EventArgs e)
        {
            Them_ChuyenPK(true);
        }

        private void UpdateDatatable()
        {
        }

        private void cmdUnlock_Click(object sender, EventArgs e)
        {
            Huyketthuckham();
        }

        private bool KiemTraBenhAn(string loaibenhan)
        {
            SqlQuery soKham = null;
            switch (loaibenhan)
            {
                case "CTT":
                    soKham = new Select().From(KcbBaCaytranhthai.Schema).Where(KcbBaCaytranhthai.Columns.IdBenhnhan).IsEqualTo(Utility.sDbnull(txtPatient_ID.Text));
                    break;
                default:
                    break;
            }
            return true;
        }
        private void BenhAnSanKhoa()
        {
            var frm = new VMS.HIS.BA.Forms.FrmHoSoCayThuocTranhThai();
            frm.Idbenhnhan = Utility.Int64Dbnull(txtPatient_ID.Text);
            frm.Loaibenhan = Utility.sDbnull(txtbenhan.MyCode);
            SqlQuery soKham = null;
            //if (THU_VIEN_CHUNG.Laygiatrithamsohethong_off("KCB_BA_TAONHIEUBENHAN", "0", false) == "1")
            //{
            //    soKham = new Select().From(KcbBenhAn.Schema).
            //    Where(KcbBenhAn.Columns.IdBnhan).IsEqualTo(Utility.sDbnull(txtPatient_ID.Text));
            //}
            switch (Utility.sDbnull(txtbenhan.MyCode))
            {
                case "CTT":
                    soKham = new Select().From(KcbBaCaytranhthai.Schema).
                                  Where(KcbBaCaytranhthai.Columns.IdBenhnhan).IsEqualTo(Utility.sDbnull(txtPatient_ID.Text));
                    break;
                case "THV":
                    soKham = new Select().From(KcbBaThaovong.Schema).
                                  Where(KcbBaThaovong.Columns.IdBenhnhan).IsEqualTo(Utility.sDbnull(txtPatient_ID.Text));
                    break;
                case "TQC":
                    soKham = new Select().From(KcbBaThaoquecay.Schema).
                                  Where(KcbBaThaoquecay.Columns.IdBenhnhan).IsEqualTo(Utility.sDbnull(txtPatient_ID.Text));
                    break;
                case "DDC":
                    soKham = new Select().From(KcbBaDatdungcu.Schema).
                                  Where(KcbBaDatdungcu.Columns.IdBenhnhan).IsEqualTo(Utility.sDbnull(txtPatient_ID.Text));
                    break;
                default:
                    soKham = new Select().From(KcbBaCaytranhthai.Schema).
                                                 Where(KcbBaCaytranhthai.Columns.IdBenhnhan).IsEqualTo(Utility.sDbnull(txtPatient_ID.Text));
                    break;
            }
            //Đã Tồn tại thông tin bệnh án ngoại trú. Sửa
            if (soKham.GetRecordCount() > 0)
            {
                //Execute()
                frm.EmAction = action.Update;
            }
            //chưa tồn tại thông tin ngoại tru. Them
            else
            {
                frm.EmAction = action.Insert;
            }
            frm.ShowDialog();
            ThongBaoBenhAn(txtPatient_ID.Text);
        }
        private void BenhAnThuong()
        {
            try
            {
                var frm = new FrmBenhAnThuong();
                frm.Loaibenhan = txtbenhan.MyCode;
                frm.txtMaBN.Text = txtPatient_ID.Text;
                frm.txtMaLanKham.Text = m_strMaLuotkham;
                frm.txtHuyetApTu.Text = Utility.sDbnull(txtHa.Text);
                frm.txtNhietDo.Text = Utility.sDbnull(txtNhietDo.Text);
                frm.txtNhipTho.Text = Utility.sDbnull(txtNhipTho.Text);
                frm.txtMach.Text = Utility.sDbnull(txtNhipTim.Text);
                frm.txtCanNang.Text = Utility.sDbnull(txtCannang.Text, 0);
                frm.txtChieuCao.Text = Utility.sDbnull(txtChieucao.Text, 0);
                frm.txtBMI.Text = String.Format("{0:0.00}",
                                                (Convert.ToDouble(Utility.DoubletoDbnull(Utility.chuanhoaDecimal(txtCannang.Text), 0))/
                                                 ((Convert.ToDouble(Utility.DoubletoDbnull(txtChieucao.Text, 100))/100)*
                                                  (Convert.ToDouble(Utility.DoubletoDbnull(txtChieucao.Text, 100))/100))));
                string icdName = "";
                string icdCode = "";
                string icdChinhName = "";
                string icdChinhCode = "";
                string icdPhuName = "";
                string icdPhuCode = "";
                DataSet dsData =
                    new Select("*").From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(
                        Utility.Int32Dbnull(txtExam_ID.Text.Trim())).ExecuteDataSet();
                DataTable v_dtData = dsData.Tables[0];
                if (v_dtData != null && v_dtData.Rows.Count > 0)
                {
                    GetChanDoan(Utility.sDbnull(v_dtData.Rows[0][KcbChandoanKetluan.Columns.MabenhChinh], ""),
                                Utility.sDbnull(v_dtData.Rows[0][KcbChandoanKetluan.Columns.MabenhPhu], ""),
                                ref icdName, ref icdCode);
                }
                frm.txtkbChanDoanRaVien.Text = icdName;
                frm.txtkbMa.Text = icdCode;

                if (v_dtData != null && v_dtData.Rows.Count > 0)
                {
                    GetChanDoanChinhPhu(Utility.sDbnull(v_dtData.Rows[0][KcbChandoanKetluan.Columns.MabenhChinh], ""),
                                        Utility.sDbnull(v_dtData.Rows[0][KcbChandoanKetluan.Columns.MabenhPhu], ""),
                                        ref icdChinhName,
                                        ref icdChinhCode, ref icdPhuName, ref icdPhuCode);
                }
                SqlQuery soKham = null;
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong_off("KCB_BA_TAONHIEUBENHAN", "0", false) == "1")
                {
                    soKham =
                        new Select().From(KcbBenhAn.Schema).
                            Where(KcbBenhAn.Columns.MaLuotkham)//Sửa điều kiện theo IdBnhan thành Maluotkham
                            .IsEqualTo(Utility.sDbnull(txtPatient_Code.Text))
                            .And(KcbBenhAn.Columns.LoaiBa)
                            .IsEqualTo(frm.Loaibenhan);
                }
                else
                {
                    soKham =
                new Select().From(KcbBenhAn.Schema).
                    Where(KcbBenhAn.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(txtPatient_Code.Text));//Sửa điều kiện theo IdBnhan thành Maluotkham;
                }
              
                //Đã Tồn tại thông tin bệnh án ngoại trú. Sửa
                if (soKham.GetRecordCount() > 0)
                {
                    //Execute()
                    frm.EmAction = action.Update;
                }
                    //chưa tồn tại thông tin ngoại tru. Them
                else
                {
                    frm.EmAction = action.Insert;
                }
                frm.ShowDialog();
                ThongBaoBenhAn(txtPatient_ID.Text);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }

        private void ThongBaoBenhAn(string idBnhan)
        {
            DataTable dtBenhAn = SPs.KcbBenhanThongbao(Utility.Int64Dbnull(idBnhan)).GetDataSet().Tables[0];
            //SqlQuery soKham =
            //    new Select().From(KcbBenhAn.Schema).
            //        Where(KcbBenhAn.Columns.IdBnhan).IsEqualTo(Utility.sDbnull(idBnhan));
            //Đã Tồn tại thông tin bệnh án ngoại trú.
            if (dtBenhAn.Rows.Count > 0)
            {   txtbenhan.Init(globalVariables.gv_danhmucbenhan , new List<string>() { DmucBenhan.Columns.NhomBa, DmucBenhan.Columns.MaBenhan, DmucBenhan.Columns.TenBenhan });
                lblBANgoaitru.ForeColor = Color.Black;
                lblBANgoaitru.Text = @"Đã có số lưu Bệnh án ngoại trú, Người Tạo:" + Utility.sDbnull(dtBenhAn.Rows[0]["NGUOI_TAO"]);
                txtbenhan.SetCode(dtBenhAn.Rows[0]["LOAI_BA"]);
                txtbenhan.ReadOnly = true;
            }
                //chưa tồn tại thông tin ngoại tru. 
            else
            {
                lblBANgoaitru.ForeColor = Color.Black;
                lblBANgoaitru.Text = @"BN chưa có số lưu Bệnh án ngoại trú";
                if (globalVariables.gv_danhmucbenhan != null)
                {
                    DataRow[] arrdr= globalVariables.gv_danhmucbenhan.Select(" id_gioitinh = 2  OR  id_gioitinh = " + objBenhnhan.IdGioitinh + "");
                    DataTable dtbenhanbyGioiTinh=globalVariables.gv_danhmucbenhan.Clone();
                    if (arrdr.Length > 0)
                        dtbenhanbyGioiTinh = arrdr.CopyToDataTable();
                    txtbenhan.Init(dtbenhanbyGioiTinh, new List<string>() { DmucBenhan.Columns.NhomBa, DmucBenhan.Columns.MaBenhan, DmucBenhan.Columns.TenBenhan });
                    txtbenhan.SetCode("");
                    txtbenhan.ReadOnly = false;
                }
            }
        }
        /// <summary>
        /// Không sài các tính năng tự thu nhóm lại nữa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkHienthichitiet_CheckedChanged(object sender, EventArgs e)
        {
            //grdAssignDetail.GroupMode = chkHienthichitiet.Checked ? GroupMode.Expanded : GroupMode.Collapsed;
            //grdAssignDetail.Refresh();
            //Application.DoEvents();
        }

        private void chkAutocollapse_CheckedChanged(object sender, EventArgs e)
        {
            //PropertyLib._ThamKhamProperties.TudongthugonCLS = chkAutocollapse.Checked;
            //PropertyLib.SaveProperty(PropertyLib._ThamKhamProperties);
            //if (PropertyLib._ThamKhamProperties.TudongthugonCLS)
            //    grdAssignDetail.GroupMode = GroupMode.Collapsed;
        }


        private void cmdThamkhamConfig_Click(object sender, EventArgs e)
        {
            var frm = new frm_Properties(PropertyLib._ThamKhamProperties);
            frm.ShowDialog();
            CauHinhThamKham();
        }

        private void CauHinhThamKham()
        {
            if (PropertyLib._ThamKhamProperties != null)
            {
                cboA4Donthuoc.Text = PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4 ? "A4" : "A5";
                cboPrintPreviewDonthuoc.SelectedIndex = PropertyLib._MayInProperties.PreviewInDonthuoc ? 0 : 1;

                cboA4Cls.Text = PropertyLib._MayInProperties.CoGiayInCLS == Papersize.A4 ? "A4" : "A5";
                cboPrintPreviewCLS.SelectedIndex = PropertyLib._MayInProperties.PreviewInCLS ? 0 : 1;

                cboA4Tomtatdieutringoaitru.Text = PropertyLib._MayInProperties.CoGiayInTomtatDieutriNgoaitru ==
                                                  Papersize.A4
                                                      ? "A4"
                                                      : "A5";
                cboPrintPreviewTomtatdieutringoaitru.SelectedIndex =
                    PropertyLib._MayInProperties.PreviewInTomtatDieutriNgoaitru ? 0 : 1;
                cboKieuin.SelectedIndex = PropertyLib._MayInProperties.KieuInPhieuKCB == KieuIn.Innhiet ? 0 : 1;
                uiStatusBar1.Visible = !PropertyLib._ThamKhamProperties.HideStatusBar;
                cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                chkAutocollapse.Checked = PropertyLib._ThamKhamProperties.TudongthugonCLS;
                cmdIndonthuoc.Visible = PropertyLib._ThamKhamProperties.ChophepIndonthuoc;
                splitContainer1.SplitterDistance = PropertyLib._ThamKhamProperties.Dorongbentrai;
                splitContainer2.SplitterDistance = PropertyLib._ThamKhamProperties.Chieucaoluoitimkiem;
                //grdList.Height = PropertyLib._ThamKhamProperties.ChieucaoluoidanhsachBenhnhan <= 0? 0: PropertyLib._ThamKhamProperties.ChieucaoluoidanhsachBenhnhan;
                //grpSearch.Height = PropertyLib._ThamKhamProperties.AntimkiemNangcao ? 0 : 211;
                if (uiTabKqCls.Width > 0)
                    uiTabKqCls.Width = PropertyLib._ThamKhamProperties.DorongVungKetquaCLS;
                lblthiluc.Visible =
                    lblthilucmp.Visible =
                        lblthilucmt.Visible = lblnhanap.Visible = lblnhanapmp.Visible = lblnhanapmt.Visible
                            =
                            txtnhanap_mp.Visible =
                                txtnhanap_mt.Visible =
                                    txtthiluc_mp.Visible =
                                        txtthiluc_mt.Visible = PropertyLib._ThamKhamProperties.HienThiThongSoMat;
            }
           
        }
        private void grdList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                HienthithongtinBn();
        }

        private void grdList_MouseClick(object sender, MouseEventArgs e)
        {
            if (PropertyLib._ThamKhamProperties.ViewAfterClick && !_buttonClick)
                HienthithongtinBn();
            _buttonClick = false;
        }

        private void cboPrintPreview_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewInDonthuoc = cboPrintPreviewDonthuoc.SelectedIndex == 0;
            PropertyLib.SavePropertyV1( PropertyLib._MayInProperties);
        }

        private void cboA4_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.CoGiayInDonthuoc = cboA4Donthuoc.SelectedIndex == 0
                                                                ? Papersize.A4
                                                                : Papersize.A5;
            PropertyLib.SavePropertyV1( PropertyLib._MayInProperties);
        }

        private void cboPrintPreviewCLS_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewInCLS = cboPrintPreviewCLS.SelectedIndex == 0;
            PropertyLib.SavePropertyV1( PropertyLib._MayInProperties);
        }

        private void cboA4Cls_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.CoGiayInCLS = cboA4Cls.SelectedIndex == 0 ? Papersize.A4 : Papersize.A5;
            PropertyLib.SavePropertyV1( PropertyLib._MayInProperties);
        }

        private void cboPrintPreviewTomtatdieutringoaitru_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewInTomtatDieutriNgoaitru =
                cboPrintPreviewTomtatdieutringoaitru.SelectedIndex == 0;
            PropertyLib.SavePropertyV1( PropertyLib._MayInProperties);
        }

        private void cboA4Tomtatdieutringoaitru_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.CoGiayInTomtatDieutriNgoaitru = cboA4Tomtatdieutringoaitru.SelectedIndex == 0
                                                                             ? Papersize.A4
                                                                             : Papersize.A5;
            PropertyLib.SavePropertyV1( PropertyLib._MayInProperties);
        }

        private void grdList_DoubleClick(object sender, EventArgs e)
        {
            HienthithongtinBn();
        }


        private void ShowOnMonitor2()
        {
            try
            {
                Screen[] sc;
                sc = Screen.AllScreens;
                IEnumerable<Screen> query = from mh in Screen.AllScreens
                                            select mh;
                //get all the screen width and heights

                if (query.Count() >= 2)
                {
                    objSoKham = new frm_DanhSachKham();
                    if (!CheckOpened(objSoKham.Name))
                    {
                        //SetParameterValueCallback += objSoKham.SetParamValueCallbackFn;
                        objSoKham.FormBorderStyle = FormBorderStyle.None;
                        objSoKham.Left = sc[1].Bounds.Width;
                        objSoKham.Top = sc[1].Bounds.Height;
                        objSoKham.StartPosition = FormStartPosition.CenterScreen;
                        objSoKham.Location = sc[1].Bounds.Location;
                        var p = new Point(sc[1].Bounds.Location.X, sc[1].Bounds.Location.Y);
                        objSoKham.Location = p;
                        objSoKham.WindowState = FormWindowState.Maximized;
                        objSoKham.Show();
                        //b_HasScreenmonitor = true;
                        // txtSoKham_TextChanged(txtSoKham, new EventArgs());
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }

        // private bool b_HasSecondMonitor=false;

        private void ShowScreen()
        {
            return;
              if (PropertyLib._HISQMSProperties != null)
                {
                    if (PropertyLib._HISQMSProperties.IsQMS)
                    {
                        ShowOnMonitor2();
                    }
                }
        }

        private bool CheckOpened(string name)
        {
            FormCollection fc = Application.OpenForms;

            return fc.Cast<Form>().Any(frm => frm.Text == name);
        }

       

        private void CauHinhQMS()
        {
            if (PropertyLib._HISQMSProperties.IsQMS)
            {
                ShowScreen();
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
                    string realName = dr[DmucChung.Columns.Ten].ToString().Trim() + " " +
                                      Utility.Bodau(dr[DmucChung.Columns.Ten].ToString().Trim());
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


        private void txtMaBenhphu_GotFocus(object sender, EventArgs e)
        {
            txtMaBenhphu_TextChanged(txtMaBenhphu, e);
        }

        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin của thăm khám
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            SearchPatient();
        }

        /// <summary>
        /// Hàm thực hiện load lên Khoa nội trú
        /// </summary>
        private void InitData()
        {
            try
            {
                m_dtKhoaNoiTru = globalVariables.gv_KhoaNoitru;
                if (File.Exists(Application.StartupPath + "\\CAUHINH\\chkintachphieu.txt"))
                {
                    chkIntach.Checked =
                        Convert.ToInt16(File.ReadAllText(Application.StartupPath + "\\CAUHINH\\chkintachphieu.txt")) ==
                        1
                            ? true
                            : false;
                }
                m_dtBenhAn = globalVariables.gv_danhmucbenhan;
                txtbenhan.Init(m_dtBenhAn, new List<string>() {DmucBenhan.Columns.NhomBa, DmucBenhan.Columns.MaBenhan, DmucBenhan.Columns.TenBenhan});

               DataTable _dtDichvuCLS = new Select().From(DmucDichvucl.Schema).ExecuteDataSet().Tables[0];
               txtLoaiDichvu.Init(_dtDichvuCLS, new List<string>() { DmucDichvucl.Columns.IdDichvu, DmucDichvucl.Columns.MaDichvu, DmucDichvucl.Columns.TenDichvu });
               txtLoaiDichvu__OnSelectionChanged();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }

        private void SearchPatient()
        {
            try
            {
                ClearControl();
                m_strMaLuotkham = "";
                objCongkham = null;
                objBenhnhan = null;
                objLuotkham = null;
                if (dt_ICD_PHU != null) dt_ICD_PHU.Clear();
                if (m_dtAssignDetail != null) m_dtAssignDetail.Clear();
                if (m_dtPresDetail != null) m_dtPresDetail.Clear();
                DateTime dtFormDate = dtFromDate.Value;
                DateTime dt_ToDate = dtToDate.Value;
                int status = -1;
                int soKham = -1;
                if (!string.IsNullOrEmpty(txtSoKham.Text))
                {
                    soKham = Utility.Int32Dbnull(txtSoKham.Text, -1);
                    status = -1;
                }
                else
                {
                    status = Utility.Int32Dbnull(cboTrangthai.SelectedValue); // optAll.Checked ? -1 : (radChuaKham.Checked ? 0 : 1);
                }
                string lstIdPK = Utility.sDbnull(cboPhongKhamNgoaiTru.SelectedValue);
                log.Trace(lstIdPK);
                if( Utility.sDbnull(cboPhongKhamNgoaiTru.SelectedValue)=="-1")lstIdPK= string.Join(",", (from p in globalVariables.gv_dtKhoaPhongNgoaiTru.AsEnumerable()
                                                   select Utility.sDbnull(p["id_khoaphong"], "")).ToArray<string>());
                log.Trace(lstIdPK);
                //if (cboPhongKhamNgoaiTru.SelectedIndex == 0)
                //{
                    m_dtDanhsachbenhnhanthamkham =
                        _KCB_THAMKHAM.LayDSachBnhanThamkham(
                            !chkByDate.Checked ? globalVariables.SysDate.AddYears(-3) : dtFormDate,
                            !chkByDate.Checked ? globalVariables.SysDate : dt_ToDate, Utility.Int64Dbnull(txtID.Text, -1), Utility.AutoFullPatientCode(txtMaluotkham.Text), txtTenBN.Text, status,
                            soKham, lstIdPK, globalVariables.StrQheNvpk, Args.Split('-')[0],
                            globalVariables.MA_KHOA_THIEN, Utility.Int16Dbnull(txtLoaiDichvu.MyID, -1), Utility.Int32Dbnull(txtDichvu.MyID, -1), Utility.DoTrim(txtKQCD.Text), globalVariables.UserName);
                //}
                //else
                //{
                //    m_dtDanhsachbenhnhanthamkham =
                //     _KCB_THAMKHAM.LayDSachBnhanThamkham(
                //         !chkByDate.Checked ? globalVariables.SysDate.AddDays(-7) : dtFormDate,
                //         !chkByDate.Checked ? globalVariables.SysDate : dt_ToDate, Utility.Int64Dbnull(txtID.Text, -1), Utility.AutoFullPatientCode(txtPatient_Code.Text), txtTenBN.Text, status,
                //         soKham,Utility.sDbnull(cboPhongKhamNgoaiTru.SelectedValue), Args.Split('-')[0],
                //         globalVariables.MA_KHOA_THIEN, Utility.Int16Dbnull(txtLoaiDichvu.MyID, -1), Utility.Int32Dbnull(txtDichvu.MyID, -1), Utility.DoTrim(txtKQCD.Text), globalVariables.UserName);
                //}
               

                if (!m_dtDanhsachbenhnhanthamkham.Columns.Contains("MAUSAC"))
                    m_dtDanhsachbenhnhanthamkham.Columns.Add("MAUSAC", typeof (int));


                Utility.SetDataSourceForDataGridEx_Basic(grdList, m_dtDanhsachbenhnhanthamkham, true, true, "", "trang_thai,stt_kham");
                UpdateGroup();
                if (dt_ICD_PHU != null && !dt_ICD_PHU.Columns.Contains(DmucBenh.Columns.MaBenh))
                {
                    dt_ICD_PHU.Columns.Add(DmucBenh.Columns.MaBenh, typeof (string));
                }
                if (dt_ICD_PHU != null && !dt_ICD_PHU.Columns.Contains(DmucBenh.Columns.TenBenh))
                {
                    dt_ICD_PHU.Columns.Add(DmucBenh.Columns.TenBenh, typeof (string));
                }
                if (dt_ICD_PHU != null && !dt_ICD_PHU.Columns.Contains(KcbDangkyKcb.Columns.IdKham))
                {
                    dt_ICD_PHU.Columns.Add(KcbDangkyKcb.Columns.IdKham, typeof(Int64));
                }
                grd_ICD.DataSource = dt_ICD_PHU;
                cmdUnlock.Visible = false;
                ModifyCommmands();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        void UpdateGroup()
        {
            try
            {
                return;//để chạy luồng chờ khám-đã khám
                var counts = m_dtDanhsachbenhnhanthamkham.AsEnumerable().GroupBy(x => x.Field<string>("ten_doituong_kcb"))
                    .Select(g => new { g.Key, Count = g.Count() });
                if (counts.Count() >= 2)
                {
                    if (grdList.RootTable.Groups.Count <= 0)
                    {
                        GridEXColumn gridExColumn = grdList.RootTable.Columns["ten_doituong_kcb"];
                        var gridExGroup = new GridEXGroup(gridExColumn);
                        gridExGroup.GroupPrefix = "Nhóm đối tượng KCB: ";
                        grdList.RootTable.Groups.Add(gridExGroup);
                    }
                }
                else
                {
                    GridEXColumn gridExColumn = grdList.RootTable.Columns["ten_doituong_kcb"];
                    var gridExGroup = new GridEXGroup(gridExColumn);
                    grdList.RootTable.Groups.Clear();
                }
                grdList.UpdateData();
                grdList.Refresh();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        private void SearchPatient(string ma_luotkham)
        {
            try
            {
                if (grdList.RowCount > 0)
                {
                    DataRow[] arrData_tempt = null;
                    arrData_tempt =
                        m_dtDanhsachbenhnhanthamkham.Select(KcbLuotkham.Columns.MaLuotkham + "='" + ma_luotkham +
                                                            "'");
                    if (arrData_tempt.Length > 0)
                    {
                        //Tạm bỏ sau khi dùng Autocomplete trạng thái 230523
                        //string _status = radChuaKham.Checked ? "0" : "1";
                        //string regStatus = Utility.sDbnull(arrData_tempt[0][KcbDangkyKcb.Columns.TrangThai], "0");
                        //if (_status != regStatus)
                        //{
                        //    if (regStatus == "1") radDaKham.Checked = true;
                        //    else
                        //        radChuaKham.Checked = true;
                        //}
                        Utility.GotoNewRowJanus(grdList, KcbLuotkham.Columns.MaLuotkham, ma_luotkham);
                        if (Utility.isValidGrid(grdList)) grdList_DoubleClick(grdList, new EventArgs());
                        return;
                    }
                }
                //Nếu chưa có trên lưới thì tìm từ CSDL
               
                string lstIdPK = Utility.sDbnull(cboPhongKhamNgoaiTru.SelectedValue);
                if (Utility.sDbnull(cboPhongKhamNgoaiTru.SelectedValue) == "-1") lstIdPK = string.Join(",", (from p in globalVariables.gv_dtKhoaPhongNgoaiTru.AsEnumerable()
                                                                                                             select Utility.sDbnull(p["id_khoaphong"], "")).ToArray<string>());

                DataTable dtSearch = _KCB_THAMKHAM.LayDSachBnhanThamkham(dtFromDate.Value, dtToDate.Value, -1, ma_luotkham, "", 100, -1, lstIdPK, "-1", "ALL", globalVariables.MA_KHOA_THIEN, -1, -1, "", globalVariables.UserName);
                if (dtSearch.Rows.Count > 0)
                {
                    m_dtDanhsachbenhnhanthamkham.ImportRow(dtSearch.Rows[0]);
                    Utility.GotoNewRowJanus(grdList, KcbLuotkham.Columns.MaLuotkham, ma_luotkham);
                    if (Utility.isValidGrid(grdList)) grdList_DoubleClick(grdList, new EventArgs());
                    return;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        } 

        private void LaydanhsachbacsiChidinh()
        {
            try
            {
             //   m_dtDoctorAssign = THU_VIEN_CHUNG.LaydanhsachBacsi(-1, 0);
                txtBacsi.Init(globalVariables.gv_dtDmucNhanvien,
                              new List<string>
                                  {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                  });
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    txtBacsi.SetId(-1);
                }
                else
                {
                    txtBacsi.SetId(globalVariables.gv_intIDNhanvien);
                }
                if (Utility.Coquyen("thamkham_chonbacsi") || THU_VIEN_CHUNG.Laygiatrithamsohethong("THAMKHAM_CHONBACSIKHAM", "0", true) == "1")
                {
                    txtBacsi.Enabled = true;
                }
                else
                {
                    txtBacsi.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }

        GridEXRow findthelastChild(GridEXRow parent)
        {
            //if (parent.GridEX.GetDataRows().Count() <= 0) return null;
            if (parent == null) return null;
            if(parent.RowType == RowType.Record) return parent;
            foreach (GridEXRow item in parent.GetChildRows())
            {
                if (item.RowType == RowType.Record) return item;
                else
                    return findthelastChild(item);
            }
            return null;
        }
        /// <summary>
        /// hàm thực hiện trạng thái của nút
        /// </summary>
        private void ModifyCommmands()
        {
            try
            {
                bool trangthai_noitru =objLuotkham!=null && objLuotkham.TrangthaiNoitru >= 1;
                bool trang_thaikhoa = objLuotkham != null && objLuotkham.Locked == 1;
                bool _ketthuckham = objCongkham != null && objCongkham.TrangThai == 1;
                bool khamnoitru = objCongkham != null && objCongkham.Noitru == 1;
                cboHDT.Enabled =objLuotkham!=null && objLuotkham.TrangthaiNoitru <= 0;
                pnlother.Enabled =pnlKetluan.Enabled=cmdchucnang.Enabled= this.objLuotkham != null && objCongkham != null;
                if (!khamnoitru)//Nếu không phải công khám nội trú
                {
                    cmdSuadonthuoc.Enabled = cmdXoadonthuoc.Enabled = grdPresDetail.RowCount > 0 && objLuotkham != null && objCongkham != null && !_ketthuckham;// && !trang_thaikhoa;
                    //Vật tư
                    cmdXoaphieuVT.Enabled = cmdSuaphieuVT.Enabled = grdVTTH.RowCount > 0 && objLuotkham != null && objCongkham != null && !_ketthuckham;//&& !trang_thaikhoa;
                    //Cận lâm sàng
                    cmdUpdate.Enabled = cmdDelteAssign.Enabled = grdAssignDetail.RowCount > 0 && objLuotkham != null && objCongkham != null && !_ketthuckham;// && !trang_thaikhoa;
                    cmdThemDonthuoc.Enabled = cmdInsertAssign.Enabled = cmdThemphieuVT.Enabled = objLuotkham != null && objCongkham != null && !_ketthuckham;//&& !trang_thaikhoa;
                    cmdFileAttach.Enabled = objCongkham!=null ;// && !trang_thaikhoa;
                }
                else//Là khám nội trú. cho phép kê đơn,chỉ định không quan tâm bệnh nhân đã khóa hay chưa
                {
                    //Đơn thuốc
                    cmdSuadonthuoc.Enabled = cmdXoadonthuoc.Enabled = grdPresDetail.RowCount > 0 && objLuotkham != null && objCongkham != null && !_ketthuckham;
                    //Vật tư
                    cmdXoaphieuVT.Enabled =
                    cmdSuaphieuVT.Enabled = grdVTTH.RowCount > 0 && objLuotkham != null && objCongkham != null && !_ketthuckham;
                    //Cận lâm sàng
                    cmdUpdate.Enabled =  cmdDelteAssign.Enabled = grdAssignDetail.RowCount > 0 && objLuotkham != null && objCongkham!=null && !_ketthuckham;
                    cmdThemDonthuoc.Enabled = cmdInsertAssign.Enabled = cmdThemphieuVT.Enabled = objLuotkham != null && objCongkham != null && !_ketthuckham;
                    cmdFileAttach.Enabled = objCongkham != null;// && !trang_thaikhoa;
                }
                cmdPrintAssign.Enabled=cmdIntachPhieu.Enabled=cmdInGoi.Enabled= objLuotkham != null && objCongkham != null && grdAssignDetail.RowCount>0;
                cmdIndonthuoc.Enabled = objLuotkham != null && objCongkham != null && grdPresDetail.RowCount > 0;
                cmdInphieuVT.Enabled = objLuotkham != null && objCongkham != null && grdVTTH.RowCount > 0;
                cmdChuyenPhong.Enabled = mnuThemPK.Enabled = mnuChuyenPK.Enabled = objLuotkham != null && objCongkham != null && !_ketthuckham && !trangthai_noitru;
                cmdInphieukham.Enabled = grdList.RowCount > 0;
                cmdLuuChandoan_sobo.Enabled = cmdLuuKetluan.Enabled = objLuotkham != null && objCongkham != null ;//&& objCongkham.TrangThai == 0;//Bỏ mục && này để cho phép lưu lại các thông tin với quyền superAdmin kể cả người dùng đã kết thúc khám và nhập viện
                ////Tạm khóa toàn bộ các đoạn dưới vì chưa xử lý khám công khám chuyên khoa khi người bệnh ở trạng thái nội trú
                //cmdPrintPres.Enabled = !string.IsNullOrEmpty(m_strMaLuotkham);
                
                //cmdPrintPres.Enabled = Utility.isValidGrid(grdPresDetail) && !string.IsNullOrEmpty(m_strMaLuotkham);

                //cmdPrintAssign.Enabled = Utility.isValidGrid(grdAssignDetail) && !string.IsNullOrEmpty(m_strMaLuotkham);

                //chkIntach.Enabled = cmdPrintAssign.Enabled;
                //cboServicePrint.Enabled = cmdPrintAssign.Enabled;
                //tabDiagInfo.Enabled = objLuotkham != null && !string.IsNullOrEmpty(m_strMaLuotkham);
                //cmdPrintPres.Enabled =
                //    cmdUpdatePres.Enabled = cmdDeletePres.Enabled = grdPresDetail.RowCount > 0 && objLuotkham != null && !string.IsNullOrEmpty(m_strMaLuotkham); //Utility.isValidGrid(grdPresDetail) && !string.IsNullOrEmpty(m_strMaLuotkham);
                //// (Utility.isValidGrid(grdPresDetail) || grdPresDetail.GetCheckedRows().Count() > 0) && !string.IsNullOrEmpty(m_strMaLuotkham);
                //cmdInphieuVT.Enabled =
                //    cmdXoaphieuVT.Enabled =
                //    cmdSuaphieuVT.Enabled =  grdVTTH.RowCount > 0 && objLuotkham != null  && !string.IsNullOrEmpty(m_strMaLuotkham);

                //cmdUpdate.Enabled = cmdFileAttach.Enabled = cmdDelteAssign.Enabled = grdAssignDetail.RowCount > 0 && objLuotkham != null && !string.IsNullOrEmpty(m_strMaLuotkham);
                   
                //    //Utility.isValidGrid(grdAssignDetail) && !string.IsNullOrEmpty(m_strMaLuotkham);
                //// (Utility.isValidGrid(grdAssignDetail) || grdAssignDetail.GetCheckedRows().Count() > 0) && !string.IsNullOrEmpty(m_strMaLuotkham);

                //cmdConfirm.Enabled = Utility.isValidGrid(grdAssignDetail) && !string.IsNullOrEmpty(m_strMaLuotkham);

                //chkDaThucHien.Visible = chkDaThucHien.Checked;
                //if (objLuotkham != null)
                //{
                //    if ((objLuotkham.Locked == 1 || objLuotkham.TrangthaiNoitru >= 1) && objCongkham.Noitru==0 )
                //    {
                //        cmdInsertAssign.Enabled = cmdSave.Enabled = cmdUpdate.Enabled = cmdDelteAssign.Enabled =
                //                                                                        cmdCreateNewPres.Enabled =
                //                                                                        cmdUpdatePres.Enabled =
                //                                                                        cmdDeletePres.Enabled =
                //                                                                        cmdThemphieuVT.Enabled =
                //                                                                        cmdSuaphieuVT.Enabled =
                //                                                                        cmdXoaphieuVT.Enabled =
                //                                                                        false;
                //    }
                //    else
                //    {
                //        cmdInsertAssign.Enabled = true && !string.IsNullOrEmpty(m_strMaLuotkham);
                //        cmdCreateNewPres.Enabled = true && !string.IsNullOrEmpty(m_strMaLuotkham);
                //        cmdThemphieuVT.Enabled = true && !string.IsNullOrEmpty(m_strMaLuotkham);
                //    }
                //    mnuDeleteCLS.Enabled = cmdUpdate.Enabled;
                //    cmdphieupttt.Enabled = cmdUpdate.Enabled; 
                //    cmdgiaychapnhanpttt.Enabled = cmdUpdate.Enabled; 
                //    ctxDelDrug.Enabled = cmdUpdatePres.Enabled;
                //}

                //if (objCongkham != null)
                //{
                //    if (!Utility.Coquyen("quyen_khamtatcacacphong_ngoaitru"))
                //    {
                //        cmdInsertAssign.Enabled = cmdUpdate.Enabled = cmdDelteAssign.Enabled =
                //            cmdCreateNewPres.Enabled =
                //                cmdUpdatePres.Enabled =
                //                    cmdDeletePres.Enabled = 
                //        (Utility.Int16Dbnull(objCongkham.IdPhongkham) ==
                //         Utility.Int16Dbnull(globalVariables.IdPhongNhanvien));
                //    }
                //}
                cmdNhapVien.Enabled = mnuNhapvien.Enabled =  objCongkham != null && objLuotkham != null && !Utility.Byte2Bool( objLuotkham.TthaiChuyendi) && objCongkham.TrangThai == 1 && !Utility.Byte2Bool(objCongkham.Noitru);
                //cmdHuyNhapVien.Enabled = mnuHuynhapvien.Enabled = !cmdNhapVien.Enabled && objLuotkham != null && !Utility.Byte2Bool(objLuotkham.TthaiChuyendi) && objLuotkham != null && objLuotkham.TrangthaiNoitru >= 1;
                cmdNhapVien.Enabled = objCongkham != null && objCongkham.TrangThai == 1 && objLuotkham != null && objLuotkham.TrangthaiNoitru <=0 && !Utility.Byte2Bool(objLuotkham.TthaiChuyendi) && !Utility.Byte2Bool(objCongkham.Noitru);//&& objLuotkham.TrangthaiNoitru == 0);//Không cho nhập viện khi khám chuyên khoa vì bệnh nhân đã vào nội trú
                cmdHuyNhapVien.Enabled = mnuHuynhapvien.Enabled = objLuotkham !=null && objLuotkham.TrangthaiNoitru >= 1 && objLuotkham.IdRavien <= 0;
                cmdInphieuhen.Enabled = cmdInTTDieuTri.Enabled = mnuInphieuhenkham.Enabled = mnuIntomtatdieutri.Enabled =  objCongkham != null && objLuotkham!=null && objCongkham.TrangThai == 1;
                mnuHuynhapvien.Enabled = cmdHuyNhapVien.Enabled && objCongkham != null && objLuotkham != null && !Utility.Byte2Bool(objLuotkham.TthaiChuyendi) && Utility.ByteDbnull(objLuotkham.TrangthaiNoitru) <= 1;
                cmdInKQXN.Enabled = grdKetqua.RowCount > 0 && objCongkham != null && objLuotkham != null && grdAssignDetail.RowCount > 0;
                cmdNhapVien.Visible =  mnuNhapvien.Visible =(objLuotkham!=null && objLuotkham.TrangthaiNoitru > 0) || (objCongkham!=null && objCongkham.TrangThai == 1 && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_MAHUONGDIEUTRI_NOITRU", false).ToUpper().Split(',').ToList<string>().Contains(Utility.sDbnull(cboHDT.SelectedValue).ToUpper()));
                cmdInGoi.Visible = cmdDenghiMG.Visible = m_dtAssignDetail!=null && m_dtAssignDetail.Columns.Count>0 && m_dtAssignDetail.Select("id_dangky>0 and id_goi>0").Length > 0;
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
            }
        }

        /// <summary>
        /// Hàm kiểm tra bệnh nhân nội trú đã được nhập viện chưa?
        /// </summary>
        private bool InVali()
        {
            //SqlQuery sqlQuery = new Select().From(TPatientDept.Schema)
            //    .Where(TPatientDept.Columns.NoiTru).IsEqualTo(1)
            //    .And(TPatientDept.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
            //    .And(TPatientDept.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
            //    .AndExpression(TPatientDept.Columns.RoomId).IsGreaterThan(-1).Or(TPatientDept.Columns.RoomId).IsNotNull()
            //    .CloseExpression();
            //if (sqlQuery.GetRecordCount() > 0)
            //{
            //    Utility.ShowMsg("Bệnh nhân đã được phân buồng giường, Bạn không thể sửa thông tin ", "Thông báo",
            //                    MessageBoxIcon.Warning);
            //    //Utility.SetMsgError(errorProvider1, cboKhoaNoiTru,
            //    //                        "Bệnh nhân đã được phân buồng giường, Bạn không thể sửa thông tin ");
            //    return false;
            //}


            return true;
        }

       

        private void Laythongtinchidinhngoaitru()
        {
            try
            {

            
            ds =
                _KCB_THAMKHAM.LaythongtinCLSVaThuoc(Utility.Int32Dbnull(txtPatient_ID.Text, -1),
                                                    Utility.sDbnull(m_strMaLuotkham, ""),
                                                    Utility.Int32Dbnull(txtExam_ID.Text));
            m_dtAssignDetail = ds.Tables[0];
            m_dtPresDetail = ds.Tables[1].Clone();
            m_dtVTTH = ds.Tables[1].Clone();
                //Tạm Rem lại ko gom thuốc,vtth thành 1 đơn nữa. 230524
            DataRow[] arrtempt = ds.Tables[1].Select("kieu_thuocvattu = 'THUOC'");
            if (arrtempt.Length > 0) m_dtPresDetail = arrtempt.CopyToDataTable();
            arrtempt = ds.Tables[1].Select("kieu_thuocvattu = 'VT'");
            //Utility.ShowMsg("OK1");
            if (arrtempt.Length > 0) m_dtVTTH = arrtempt.CopyToDataTable();
            Utility.SetDataSourceForDataGridEx_Basic(grdAssignDetail, m_dtAssignDetail, true, true, "","");
            grdAssignDetail.MoveFirst();
            TuybiennutchuyenCLS();

            if (chkShowGroup.Checked)
                CreateViewTable();
            else
            {
                Utility.SetDataSourceForDataGridEx_Basic(grdPresDetail, m_dtPresDetail, true, true, "", KcbDonthuocChitiet.Columns.SttIn);
                Utility.SetDataSourceForDataGridEx_Basic(grdVTTH, m_dtVTTH, true, true, "", KcbDonthuocChitiet.Columns.SttIn);
            }
            ResetNhominCLS();
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
        }
        void CreateViewTable()
        {
            try
            {
                #region Gom đơn thuốc-VTTH Tạm rem lại code để mở hết các đơn thuốc 230524
                m_dtDonthuocChitiet_View = m_dtPresDetail.Clone();
                //Utility.ShowMsg("OK2");
                foreach (DataRow dr in m_dtPresDetail.Rows)
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
                        drview[0]["TT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                          (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]) +
                                           Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu]));
                        //drview[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                        //                               Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]);
                        //drview[0]["TT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                        //                  (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]) +
                        //                   Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu]));
                        //drview[0]["TT_BHYT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                        //                       Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                        //drview[0]["TT_BN"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                        //                     (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                        //                      Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                        //drview[0]["TT_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                        //                         Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                        //drview[0]["TT_BN_KHONG_PHUTHU"] =
                        //    Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                        //    Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);

                        drview[0][KcbDonthuocChitiet.Columns.SttIn] =
                            Math.Min(Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SttIn], 0),
                                     Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                        m_dtDonthuocChitiet_View.AcceptChanges();
                    }
                }

                Utility.SetDataSourceForDataGridEx(grdPresDetail, m_dtDonthuocChitiet_View, true, true, "",
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
                        drview[0]["TT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                         (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]) +
                                          Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu]));

                        //drview[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                        //                               Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]);
                        //drview[0]["TT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                        //                  (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]) +
                        //                   Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu]));
                        //drview[0]["TT_BHYT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                        //                       Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                        //drview[0]["TT_BN"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                        //                     (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                        //                      Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                        //drview[0]["TT_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                        //                         Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                        //drview[0]["TT_BN_KHONG_PHUTHU"] =
                        //    Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                        //    Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);

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
            DataTable dtData = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() 
            { 
                txtLydokham.LOAI_DANHMUC, txtChanDoan.LOAI_DANHMUC, autoTrangthai.LOAI_DANHMUC, "KQK" ,
               "HDT", txtNhommau.LOAI_DANHMUC, txtNhanxet.LOAI_DANHMUC, txtChanDoanKemTheo.LOAI_DANHMUC ,
                txtCheDoAn.LOAI_DANHMUC,auto_khammat.LOAI_DANHMUC,txtTAG.LOAI_DANHMUC
            }, true);
            txtTAG.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtTAG.LOAI_DANHMUC));
            txtLydokham.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtLydokham.LOAI_DANHMUC));
            txtChanDoan.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtChanDoan.LOAI_DANHMUC));
            autoTrangthai.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, autoTrangthai.LOAI_DANHMUC));
            txtNhommau.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtNhommau.LOAI_DANHMUC));
            txtNhanxet.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtNhanxet.LOAI_DANHMUC));
            txtChanDoanKemTheo.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtChanDoanKemTheo.LOAI_DANHMUC));
            auto_khammat.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, auto_khammat.LOAI_DANHMUC));
            txtCheDoAn.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtCheDoAn.LOAI_DANHMUC));
            //autoLoidan.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, autoLoidan.LOAI_DANHMUC));
            //autoXutri.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, autoXutri.LOAI_DANHMUC));
            DataBinding.BindDataCombobox(cboKQDT, THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, "KQK"), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
            DataBinding.BindDataCombobox(cboHDT, THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, "HDT"), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
            //txtTrieuChungBD.Init();
            //txtChanDoan.Init();
            //autoTrangthai.Init();
            //cboHDT.Init();
            //cboKQDT.Init();
            //txtNhommau.Init();
            //txtNhanxet.Init();
            //txtChanDoanKemTheo.Init();
            //txtCheDoAn.Init();
            //DataBinding.BindDataCombobox(cbo_icd, globalVariables.gv_dtDmucBenh.Copy(), DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh);
            //DataBinding.BindDataCombobox(cbo_icd_mp, globalVariables.gv_dtDmucBenh.Copy(), DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh);
            //DataBinding.BindDataCombobox(cbo_icd_mt, globalVariables.gv_dtDmucBenh.Copy(), DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh);
            //AutoMabenhchinh.Init(globalVariables.gv_dtDmucBenh, new List<string> { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });
            autoICD_2mat.dtData = globalVariables.gv_dtDmucBenh;
            autoICD_2mat.ChangeDataSource();

            autoMabenhphu.Init(globalVariables.gv_dtDmucBenh.Copy(), new List<string> { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });
            autoICD_matphai.dtData = globalVariables.gv_dtDmucBenh.Copy();
            autoICD_matphai.ChangeDataSource();

            autoICD_mattrai.dtData = globalVariables.gv_dtDmucBenh.Copy();
            autoICD_mattrai.ChangeDataSource();


        }

        //private void LoadDanhSachBenhAn()
        //{
        //    DataTable dtBa = THU_VIEN_CHUNG.LayDulieuDanhmucChung(globalVariables.DC_DM_BENHAN, false);
        //    cboChonBenhAn.DataSource = dtBa;
        //    cboChonBenhAn.DataMember = DmucChung.Columns.Loai;
        //    cboChonBenhAn.ValueMember = DmucChung.Columns.Loai;
        //    cboChonBenhAn.DisplayMember = DmucChung.Columns.Loai;

        //}
        void ResetNhominCLS()
        {
            try
            {
                if (grdAssignDetail.GetDataRows().Length <= 0) return;
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
                DataTable dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung(globalVariables.DC_NHOMIN_CLS, true);
                DataTable dttempt = dtNhomin.Clone();
                foreach (DataRow dr in dtNhomin.Rows)
                    if (nhomcls.Contains(Utility.sDbnull(dr[DmucChung.Columns.Ma], "")))
                        dttempt.ImportRow(dr);
                DataBinding.BindDataCombobox(cboServicePrint, dttempt, DmucChung.Columns.Ma, DmucChung.Columns.Ten,
                                                          "Tất cả", true);
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
        }
        void InitQMS()
        {
            ThoiGianTuDongLay = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("QMSPK_ThoigiantudongLayso", "5000", true), 5000);
            ConfigQMS();
        }
        bool AutoGoiLoa_Theomay = false;
        DataTable dtBMI = new DataTable();
        DataTable dtSPO2 = new DataTable();
        void LoadData()
        {
            try
            {
                uc_khamthiluc1.SetPermision(Lakhamthiluc);
                uc_donkinh1.SetPermision(true);
                //uc_donkinh1.InitData();
                //uc_khamthiluc1.InitData();
                //uc_khamthiluc2.InitData();
                DataTable dt_tempt = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { "BMI", "SPO2" }, true);
                dtBMI = THU_VIEN_CHUNG.LayDulieuDanhmucChung(dt_tempt, "BMI");
                dtSPO2 = THU_VIEN_CHUNG.LayDulieuDanhmucChung(dt_tempt, "SPO2");
                mnuCancelResult.Visible = globalVariables.IsAdmin || Utility.Coquyen("quyen_huyketqua_cdha");
                InitQMS();
                numberofDisplay =Utility.Int32Dbnull( THU_VIEN_CHUNG.Laygiatrithamsohethong("QMSPK_SOLUONGCHO", "5",true),5);
                layout0 = THU_VIEN_CHUNG.Laygiatrithamsohethong("QMSPK_LAYOUT0", "STT,Họ và tên,Giới tính,Năm sinh@194, 701, 260, 309", true);
                layout1 = THU_VIEN_CHUNG.Laygiatrithamsohethong("QMSPK_LAYOUT1", "STT,Họ và tên,Giới tính,Năm sinh@194, 701, 260, 309", true);
                AutoGoiLoa_Theomay = THU_VIEN_CHUNG.Laygiatrithamsohethong("QMS_TUDONGOILOA_THEOMAY", "0", true) == "1";
                AutoGoiLoa_WhenNext = THU_VIEN_CHUNG.Laygiatrithamsohethong("QMS_TUDONGOILOA_KHINEXT", "0", true) == "1";
                CKEditorInput = THU_VIEN_CHUNG.Laygiatrithamsohethong("HINHANH_CKEDITOR", "1", true) == "1";
                log.Trace("Get_DanhmucChung finished");
                Get_DanhmucChung();
                lstVisibleColumns = Utility.GetVisibleColumns(grdAssignDetail);
                DataTable dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung(globalVariables.DC_NHOMIN_CLS, true);
                DataBinding.BindDataCombobox(cboServicePrint, dtNhomin, DmucChung.Columns.Ma, DmucChung.Columns.Ten,
                                           "Tất cả", true);
                DataTable dtTthai = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo(autoTrangthai.LOAI_DANHMUC).And(DmucChung.Columns.TrangThai).IsEqualTo(1).OrderAsc(DmucChung.Columns.SttHthi).ExecuteDataSet().Tables[0];
                DataBinding.BindDataCombobox(cboTrangthai, dtTthai, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                cboTrangthai.SelectedValue = THU_VIEN_CHUNG.Laygiatrimacdinh(dtTthai);
                Load_DSach_ICD();
                log.Trace("Load_DSach_ICD finished");
                LoadPhongkhamngoaitru();
                log.Trace("LoadPhongkhamngoaitru finished");
                LaydanhsachbacsiChidinh();
                log.Trace("LaydanhsachbacsiChidinh finished");
                InitData();
                log.Trace("InitData finished");
                SearchPatient();
                log.Trace("LaydanhsachbacsiChidinh finished");
                if (cboServicePrint.Items.Count > 0) cboServicePrint.SelectedIndex = 0;
                cmdNhapVien.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU", "0", false) == "1";
                cmdChuyenCLS.Visible =cmdHuyChuyenCLS.Visible= THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_CHOPHEPBACSY_CHUYENCLS", "0", false) == "1";
                //cmdHuyNhapVien.Visible = cmdNhapVien.Visible;
                hasLoaded = true;

                ClearControl();
                timer1.Enabled = true;
                timer1.Interval = 900;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                ModifyCommmands();
                txtPatient_Code.Focus();
                txtPatient_Code.Select();
                if (Utility.sDbnull(ma_luotkham, "") != "")
                {
                    txtPatient_Code.Text = ma_luotkham;
                    LayThongTinQuaMaLanKham(ma_luotkham);
                }

            }
        }
        private void frm_KCB_THAMKHAM_V2_Load(object sender, EventArgs e)
        {
          
        }

        private void Load_DSach_ICD()
        {
            try
            {
                dt_ICD = globalVariables.gv_dtDmucBenh;
                //_KCB_THAMKHAM.LaydanhsachBenh();
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy danh sách ICD");
            }
        }

        private void LoadPhongkhamngoaitru()
        {
            DataBinding.BindDataCombobox(cboPhongKhamNgoaiTru, globalVariables.gv_dtKhoaPhongNgoaiTru,
                                       DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                       "---Chọn phòng khám---", false);
            globalVariables.StrQheNvpk = "";
            foreach (DataRow row in globalVariables.gv_dtKhoaPhongNgoaiTru.AsEnumerable())
            {
                if (row[DmucKhoaphong.Columns.IdKhoaphong].ToString() != "")
                {
                    if (globalVariables.StrQheNvpk.Length > 0)
                    {
                        globalVariables.StrQheNvpk = globalVariables.StrQheNvpk + ',' +
                                               row[DmucKhoaphong.Columns.IdKhoaphong].ToString();
                    }
                    else
                    {
                        globalVariables.StrQheNvpk = row[DmucKhoaphong.Columns.IdKhoaphong].ToString();
                    }

                }
            }
        }

       
        bool HienthiLamBenhAn()
        {
            return objCongkham!=null && objLuotkham!=null &&  objCongkham.TrangThai != 0 && objLuotkham.TrangthaiNoitru == 0 && Utility.DoTrim(autoICD_2mat.MyCode) != "" && CanBA(Utility.DoTrim(autoICD_2mat.MyCode));
        }
        /// <summary>
        /// hàm thực hiện việc mã bệnh phụ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaBenhphu_TextChanged(object sender, EventArgs e)
        {
            hasMorethanOne = true;
            DataRow[] arrDr;
            if (isLike)
                arrDr =
                    globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " like '" +
                                                         Utility.sDbnull(txtMaBenhphu.Text, "") +
                                                         "%'");
            else
                arrDr =
                    globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " = '" +
                                                         Utility.sDbnull(txtMaBenhphu.Text, "") +
                                                         "'");
            if (!string.IsNullOrEmpty(txtMaBenhphu.Text))
            {
                if (arrDr.GetLength(0) == 1)
                {
                    hasMorethanOne = false;
                    txtMaBenhphu.Text = arrDr[0][DmucBenh.Columns.MaBenh].ToString();
                    txtTenBenhPhu.Text = Utility.sDbnull(arrDr[0][DmucBenh.Columns.TenBenh], "");
                    TEN_BENHPHU = txtTenBenhPhu.Text;
                    //  txtDisease_ID.Text = Utility.sDbnull(arrDr[0][DmucBenh.Columns.DiseaseId], "-1");
                }
                else
                {
                    //txtDisease_ID.Text = "-1";
                    txtTenBenhPhu.Text = "";
                    TEN_BENHPHU = "";
                }
            }
            else
            {
                //  txtDisease_ID.Text = "-1";

                txtMaBenhphu.Text = "";
                TEN_BENHPHU = "";
                //cmdSearchBenhChinh.PerformClick();
            }
        }

        /// <summary>
        /// hàm thực hiện việc tìm kiếm bệnh phụ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearchBenhPhu_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frm_QuickSearchDiseases();
                frm.DiseaseType_ID = -1;
                frm.ShowDialog();
                if (frm.m_blnCancel)
                {
                    //  m_blnGetAuxDiseasesCodeFromList = true;
                    txtMaBenhphu.Text = frm.v_DiseasesCode;
                    txtMaBenhphu.Focus();
                    txtMaBenhphu.SelectAll();
                    //cboDeaisetype_ID.SelectedIndex = Utility.GetSelectedIndex(cboDeaisetype_ID,
                    //  frm.DiseaseType_ID.ToString());
                    //  lstAuxDiseasesTip.Visible = false;
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
        }

        /// <summary>
        /// hàm thực hiện viecj tim fkieems bệnh chính
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearchBenhChinh_Click(object sender, EventArgs e)
        {
            ShowDiseaseList();
        }

        /// <summary>
        /// hàm thực hiện hsow thông tin cua bệnh
        /// </summary>
        private void ShowDiseaseList()
        {
            try
            {
                var frm = new frm_QuickSearchDiseases();
                frm.DiseaseType_ID = -1;
                frm.ShowDialog();
                if (frm.m_blnCancel)
                {
                   
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
        }

        private void ClearControl()
        {
            try
            {
                if (dt_ICD_PHU != null) dt_ICD_PHU.Rows.Clear();
                if (dtLsuCongkham != null) dtLsuCongkham.Clear();
                grdAssignDetail.DataSource = null;
                grdPresDetail.DataSource = null;
                grdCongkham.DataSource = null;
                grbLichsuCongkham.Text = "Lịch sử khám";
                grdVTTH.DataSource = null;
                //cbo_icd.SelectedValue = "-1";
                //cbo_icd_mp.SelectedValue = "-1";
                //cbo_icd_mt.SelectedValue = "-1";
                txtTenICD2Mat.Clear();
                txtTenBenhPhu.Clear();
                txtReg_ID.Text = "";
                cmdNhapVien.Enabled = false;
                cmdHuyNhapVien.Enabled = false;
                txtTenDvuKham.Clear();
                txtPhongkham.Clear();
                foreach (Control control in pnlPatientInfor.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox) (control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is AutoCompleteTextbox)
                    {
                        ((AutoCompleteTextbox) control)._Text = "";
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox) (control)).Clear();
                    }
                }


                foreach (Control control in pnlKetluan.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox) (control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is AutoCompleteTextbox)
                    {
                        ((AutoCompleteTextbox) control)._Text = "";
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox) (control)).Clear();
                    }
                }

                foreach (Control control in pnlother.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox) (control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is AutoCompleteTextbox)
                    {
                        ((AutoCompleteTextbox) control)._Text = "";
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox) (control)).Clear();
                    }
                }
                txtSongaydieutri.Text = "0";
                txtSoNgayHen.Text = "0";
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
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
                //                 m_strMaLuotkham.Trim() + "') order by stt_hthi_dichvu,sIntOrder,Ten_KQ";
                //DataTable temdt = DataService.GetDataSet(cmd).Tables[0];
                //uiGroupBox6.Width = temdt != null && temdt.Rows.Count > 0 ? 300 : 0;
                //Utility.SetDataSourceForDataGridEx(grdKetQua, temdt, true, true, "", "");
            }
            catch
            {
            }
        }


        /// <summary>
        /// Lấy về thông tin bệnh nhâ nội trú
        /// </summary>
        public void GetData()
        {
            try
            {
                kiemtra = false;
                //reset object
                RowCLS = null;
                RowThuoc = null;
                RowVTTH = null;
                objLuotkham = null;
                objCongkham = null;
                Utility.SetMsg(lblMsg, "", false);
                ClearControl();
                if (!Utility.isValidGrid(grdList))
                {
                    return;
                }
                bool khamcheoCackhoa = THU_VIEN_CHUNG.Laygiatrithamsohethong("KHAMCHEO_CACKHOA", "0", false) == "1";
                string patientCode = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham), "");
                m_strMaLuotkham = patientCode;
                if (!Utility.CheckLockObject(m_strMaLuotkham, "Thăm khám", "TK"))
                    return;

                int idBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                objLuotkham = new Select().From(KcbLuotkham.Schema)
                    .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(patientCode)
                    .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(idBenhnhan).ExecuteSingle<KcbLuotkham>();
                if (objLuotkham == null)
                {
                    Utility.ShowMsg(string.Format("Lượt khám {0} ứng với người bệnh {1} không tồn tại(Có thể bị xóa ở bộ phận tiếp đón). Vui lòng nhấn tìm kiếm lại hoặc liên hệ nội bộ", patientCode, Utility.sDbnull(grdList.GetValue(KcbDanhsachBenhnhan.Columns.TenBenhnhan), "")));
                    return;
                }
                objBenhnhan = KcbDanhsachBenhnhan.FetchByID(objLuotkham.IdBenhnhan);
                if (!Utility.Byte2Bool( objBenhnhan.IdGioitinh ))//Nam
                {
                    lblPara.Visible = txtPara.Visible = false;
                    chkQuaibi.Visible = true;
                }
                else//Nữ
                {
                    lblPara.Visible = txtPara.Visible = true;
                    chkQuaibi.Visible = false;
                }
                isRoom = false;
                if (objLuotkham != null)
                {
                    Utility.SetMsg(lblTrangthainoitru, Utility.Laythongtintrangthainguoibenh(objLuotkham), false);
                    string tenKhoaphong = Utility.sDbnull(grdList.GetValue("ten_khoaphong"), "");
                    cmdDonthuoctutuc.Visible = THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb);
                    //cmdChuyenPhong.Visible = objLuotkham.TrangthaiNoitru == 0;
                    int hienThi = Utility.Int32Dbnull(grdList.GetValue("hien_thi"), 0);
                    if (hienThi == 0)
                    {
                        Utility.ShowMsg("Bệnh nhân " + objBenhnhan.TenBenhnhan +
                                        " CHƯA NỘP TIỀN KHÁM trong khi thuộc đối tượng khám chữa bệnh CẦN THANH TOÁN TRƯỚC KHI VÀO PHÒNG KHÁM.\r\nYêu cầu Bệnh nhân đi NỘP TIỀN KHÁM TRƯỚC");
                        objLuotkham = null;
                        objBenhnhan = null;
                        m_strMaLuotkham = "";
                        return;
                    }
                    txt_idchidinhphongkham.Text = Utility.sDbnull(grdList.GetValue(KcbDangkyKcb.Columns.IdKham));
                    objCongkham = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txt_idchidinhphongkham.Text));
                    if (!khamcheoCackhoa && globalVariables.MA_KHOA_THIEN != objCongkham.MaKhoaThuchien)
                    {
                        Utility.ShowMsg("Bệnh nhân này được tiếp đón và chỉ định khám cho khoa " + tenKhoaphong +
                                        ". Trong khi máy bạn đang cấu hình khám chữa bệnh cho khoa " +
                                        globalVariablesPrivate.objKhoaphong.TenKhoaphong +
                                        "\r\nHệ thống không cho phép khám chéo giữa các khoa. Đề nghị liên hệ Bộ phận IT trong đơn vị để được trợ giúp");
                        objLuotkham = null;
                        objBenhnhan = null;
                        m_strMaLuotkham = "";
                        return;
                    }
                   
                    lblSongaydieutri.ForeColor = THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb)
                                                     ? lblKetluan.ForeColor
                                                     : lblBenhphu.ForeColor;
                    opt2mat.ForeColor = lblSongaydieutri.ForeColor;
                    lblBANoitru.Text = Utility.Int32Dbnull(objLuotkham.SoBenhAn, -1) <= 0
                                           ? ""
                                           : "Số B.A Nội trú: " + objLuotkham.SoBenhAn;
                   

                   
                    var objStaff = (from p in globalVariables.gv_dtDmucNhanvien.AsEnumerable()
                                    where p[DmucNhanvien.Columns.UserName].Equals(objLuotkham.NguoiKetthuc)
                                    select p).FirstOrDefault();
                    //var objStaff = new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.UserNameColumn).IsEqualTo(
                    //    Utility.sDbnull(objLuotkham.NguoiKetthuc, "")).ExecuteSingle<DmucNhanvien>();
                    string tenNhanvien = objLuotkham.NguoiKetthuc;
                    if (objStaff != null)
                        tenNhanvien = Utility.sDbnull(objStaff["ten_nhanvien"], "");// objStaff.TenNhanvien;
                    pnlCLS.Enabled = true;
                    pnlDonthuoc.Enabled = true;
                    pnlVTTH.Enabled = true;
                    if (objCongkham != null)
                    {
                        VisibleLockButton();
                        if (!cmdUnlock.Enabled)
                            toolTip1.SetToolTip(cmdUnlock,
                                                "Bạn không có quyền mở khóa Bệnh nhân này. Đề nghị liên hệ " +
                                                tenNhanvien + "(" + objLuotkham.NguoiKetthuc +
                                                " - Là người khóa BN này) để được họ mở khóa. Hoặc liên hệ Quản trị hệ thống");
                        else
                            toolTip1.SetToolTip(cmdUnlock,
                                                "Nhấn vào đây để mở khóa cho bệnh nhân đang chọn(Phím tắt Ctrl+U). " +
                                                "Điều kiện là chỉ mở khóa đối với đối tượng Dịch vụ. " +
                                                "Muốn mở khóa đối tượng BHYT thì cần liên lạc với bộ phận thanh toán hủy in phôi BHYT");
                        cmdNhapVien.Enabled = objCongkham.TrangThai == 1 && objLuotkham.TrangthaiNoitru == 0;//Không cho nhập viện khi khám chuyên khoa vì bệnh nhân đã vào nội trú
                        cmdHuyNhapVien.Enabled = objLuotkham.TrangthaiNoitru >= 1 && objLuotkham.IdRavien<=0;
                        cmdNhapVien.Tag = objLuotkham.TrangthaiNoitru == 0 ? "0" : "1";
                        cmdNhapVien.Text = objLuotkham.TrangthaiNoitru == 0 ? "Nhập viện" : "Cập nhật";
                        if (objCongkham.ThoigianBatdau.HasValue)
                            dtpThoigian_batdau.Value = objCongkham.ThoigianBatdau.Value;
                        else
                            dtpThoigian_batdau.Value = DateTime.Now;
                        if (objCongkham.ThoigianKetthuc.HasValue)
                            dtpThoigianKetthuc.Value = objCongkham.ThoigianKetthuc.Value;
                        else
                            dtpThoigianKetthuc.Value = DateTime.Now.AddSeconds(30);
                        DataTable m_dtThongTin = _KCB_THAMKHAM.LayThongtinBenhnhanKCB(objLuotkham.MaLuotkham,
                                                                                      Utility.Int32Dbnull(
                                                                                          objLuotkham.IdBenhnhan,
                                                                                          -1),
                                                                                      Utility.Int32Dbnull(
                                                                                          txt_idchidinhphongkham.Text));

                        if (m_dtThongTin.Rows.Count > 0)
                        {
                            DataRow dr = m_dtThongTin.Rows[0];
                            if (dr != null)
                            {
                                dtpNgaytiepdon.Value = Convert.ToDateTime(dr[KcbLuotkham.Columns.NgayTiepdon]);
                                txtExam_ID.Text = Utility.sDbnull(grdList.GetValue(KcbDangkyKcb.Columns.IdKham));


                                txtGioitinh.Text =
                                    Utility.sDbnull(grdList.GetValue(KcbDanhsachBenhnhan.Columns.GioiTinh), "");
                                txt_idchidinhphongkham.Text =
                                    Utility.sDbnull(grdList.GetValue(KcbDangkyKcb.Columns.IdKham));
                                lblSOkham.Text = Utility.sDbnull(grdList.GetValue(KcbDangkyKcb.Columns.SttKham));
                                txtPatient_Name.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                                txtPatient_ID.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.IdBenhnhan], "");
                                txtPatient_Code.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.MaLuotkham], "");
                                txtDiaChi.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.DiaChi], "");
                                txtDiachiBHYT.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.DiachiBhyt], "");
                                txtLydokham._Text = Utility.sDbnull(dr[KcbLuotkham.Columns.TrieuChung], "");
                                txtCanhbao.Text = objBenhnhan.CanhBao;

                                txtObjectType_Name.Text = Utility.sDbnull(dr[DmucDoituongkcb.Columns.TenDoituongKcb], "");
                                txtSoBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.MatheBhyt], "");
                                txtNoiDKKCB.Text = string.Format("{0}-{1}",
                                                                 Utility.sDbnull(
                                                                     dr[KcbLuotkham.Columns.NoiDongtrusoKcbbd], ""),
                                                                 Utility.sDbnull(dr[KcbLuotkham.Columns.MaKcbbd], ""));
                                txtBHTT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.PtramBhyt], "0");
                                txtMaBenhAn.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.SoBenhAn], "");
                                //txtNgheNghiep.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.NgheNghiep], "");
                                txtHanTheBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.NgayketthucBhyt], "");
                                dtpNgayhethanBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.NgayketthucBhyt],
                                                                         globalVariables.SysDate.ToString("dd/MM/yyyy"));
                                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THAMKHAM_HIENTHITUOIORNAMSINH", "1",false)=="1")
                                {
                                    txtTuoi.Text = string.Format("NS: {0} ",
                                        Utility.sDbnull(objBenhnhan.NgaySinh.Value.Year));
                                }
                                else
                                {
                                    txtTuoi.Text = string.Format("{0} tuổi ",
                                       Utility.sDbnull(globalVariables.SysDate.Year - objBenhnhan.NgaySinh.Value.Year));
                                }
                                ThongBaoBenhAn(txtPatient_ID.Text);
                                if (objCongkham != null)
                                {
                                    // txtSTTKhamBenh.Text = Utility.sDbnull(objCongkham.SoKham);
                                    txtReg_ID.Text = Utility.sDbnull(objCongkham.IdKham);
                                    dtpNgaydangky.Value = objCongkham.NgayDangky.Value;
                                    txtDepartment_ID.Text = Utility.sDbnull(objCongkham.IdPhongkham);
                                    var department = (from p in globalVariables.gv_dtDmucPhongban.AsEnumerable()
                                        where p[DmucKhoaphong.Columns.IdKhoaphong].Equals(objCongkham.IdPhongkham)
                                        select p).FirstOrDefault();
                                    //var _department =
                                    //    new Select().From(DmucKhoaphong.Schema).Where(DmucKhoaphong.IdKhoaphongColumn).
                                    //        //IsEqualTo(objCongkham.IdPhongkham).ExecuteSingle<DmucKhoaphong>();
                                    if (department != null)
                                    {
                                        txtPhongkham.Text = Utility.sDbnull(department["ten_khoaphong"],"");
                                    }
                                    txtTenDvuKham.Text = Utility.sDbnull(objCongkham.TenDichvuKcb);
                                    txtNguoiTiepNhan.Text = Utility.sDbnull(objCongkham.NguoiTao);
                                    if (Utility.Int16Dbnull(objCongkham.IdBacsikham, -1) != -1)
                                    {
                                        txtBacsi.SetId(Utility.sDbnull(objCongkham.IdBacsikham, -1));
                                    }
                                    else
                                    {
                                        txtBacsi.SetId(globalVariables.gv_intIDNhanvien);
                                    }
                                    chkDaThucHien.Checked = Utility.Int32Dbnull(objCongkham.TrangThai) == 1;
                                }
                                _KcbChandoanKetluan = new Select().From(KcbChandoanKetluan.Schema)
                                    .Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(objCongkham.IdKham).
                                    ExecuteSingle
                                    <KcbChandoanKetluan>();
                                if (_KcbChandoanKetluan!=null && Utility.Int16Dbnull(_KcbChandoanKetluan.IdBacsikham, -1) != -1)
                                {
                                    txtBacsi.SetId(Utility.sDbnull(_KcbChandoanKetluan.IdBacsikham, -1));
                                }
                                uc_donkinh1.SetData(objLuotkham, objBenhnhan, objCongkham);
                                uc_donkinh1.SetBacsiKham(Utility.Int32Dbnull( txtBacsi.MyID));
                                if (Lakhamthiluc)
                                {
                                    uc_khamthiluc2.SetData(objLuotkham, objBenhnhan, objCongkham);
                                    uc_khamthiluc2.SetBacsiKham(txtBacsi.MyID);
                                }   
                                else
                                {
                                    uc_khamthiluc1.SetData(objLuotkham, objBenhnhan, objCongkham);
                                    uc_khamthiluc2.SetBacsiKham(txtBacsi.MyID);
                                }    
                               
                                FillThongtinHoibenhVaChandoan();
                                Laythongtinchidinhngoaitru();
                                m_dtTag = SPs.KcbTagLaythongtintheonguoibenh(objCongkham.IdBenhnhan, objCongkham.MaLuotkham, objCongkham.IdKham).GetDataSet().Tables[0];
                                Utility.SetDataSourceForDataGridEx_Basic(grdTag, m_dtTag, false, true, "1=1", "ten");
                            }
                        }
                        //cmdKETTHUC.Visible = objLuotkham.Locked == 0 && objCongkham.TrangThai > 0;
                        //Tạm thời comment bỏ 2 dòng dưới để luôn hiển thị 2 nút này(190803)
                        //cmdInTTDieuTri.Visible =
                        //    cmdInphieuhen.Visible = objCongkham.TrangThai != 0 && objLuotkham.TrangthaiNoitru == 0;
                        txtbenhan.Visible = HienthiLamBenhAn(); 
                      //  lblBenhan.Visible = cboChonBenhAn.Visible;
                        // cmdBenhAnNgoaiTru.Visible = objCongkham.TrangThai != 0;
                        //cmdKETTHUC.Enabled = true;
                    }
                    else
                    {
                        ClearControl();
                        //cmdKETTHUC.Enabled = false;
                    }
                    dtLsuCongkham = _KCB_THAMKHAM.KcbLichsuKcbTimkiemphongkham(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham,1);
                    Utility.SetDataSourceForDataGridEx_Basic(grdCongkham, dtLsuCongkham, false, true, "1=1", "ngay_dangky desc,ngay_tiepdon desc");
                    grbLichsuCongkham.Text = string.Format("Lịch sử khám ({0})", dtLsuCongkham.Rows.Count);
                    grdCongkham.AutoSizeColumns();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.ToString());
            }
            finally
            {
                ModifyCommmands();
                KiemTraDaInPhoiBhyt();
                ShowResult();
                GetTamUng();
                kiemtra = true;
            }
        }
        DataTable dtLsuCongkham = new DataTable();
        bool fillmabenhchinh = true;
        void FillThongtinHoibenhVaChandoan()
        {
            //Điền thông tin chung
            isLike = false;
            if (fillmabenhchinh)
            {
                autoICD_2mat.SetCode(Utility.sDbnull(objLuotkham.MabenhChinh));
            }
            //txtTenICD2Mat.Text = Utility.sDbnull(_KcbChandoanKetluan.MotaBenhchinh);
            string mabenhphu_theocongkham = _KcbChandoanKetluan != null ? Utility.sDbnull(_KcbChandoanKetluan.MabenhPhu, "") : "";
            List<string> lstmabenhphu_congkham = mabenhphu_theocongkham.Split(',').ToList<string>();
            isLike = true;
            dt_ICD_PHU.Clear();
            if (!string.IsNullOrEmpty(mabenhphu_theocongkham))
            {
                foreach (string row in lstmabenhphu_congkham)
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        DataRow newDr = dt_ICD_PHU.NewRow();
                        newDr[DmucBenh.Columns.MaBenh] = row;
                        newDr[DmucBenh.Columns.TenBenh] = GetTenBenh(row);
                        newDr[KcbDangkyKcb.Columns.IdKham] = _KcbChandoanKetluan.IdKham;
                        dt_ICD_PHU.Rows.Add(newDr);
                        dt_ICD_PHU.AcceptChanges();
                    }
                }

            }

            //Fill mã bệnh phụ của các phòng khám khác
            string ma_benhphu = Utility.sDbnull(objLuotkham.MabenhPhu, "");
            if (!string.IsNullOrEmpty(ma_benhphu))
            {
                string[] rows = ma_benhphu.Split(',');
                foreach (string row in rows)
                {
                    if (!string.IsNullOrEmpty(row) && !lstmabenhphu_congkham.Contains(row))
                    {
                        DataRow newDr = dt_ICD_PHU.NewRow();
                        newDr[DmucBenh.Columns.MaBenh] = row;
                        newDr[DmucBenh.Columns.TenBenh] = GetTenBenh(row);
                        newDr[KcbDangkyKcb.Columns.IdKham] = -1;
                        dt_ICD_PHU.Rows.Add(newDr);
                        dt_ICD_PHU.AcceptChanges();
                    }
                }

            }
            grd_ICD.DataSource = dt_ICD_PHU;
            txtNhommau._Text = objBenhnhan != null ? objBenhnhan.NhomMau : "";
            KcbPhieukhamThiluc objKhamthiluc = new Select().From(KcbPhieukhamThiluc.Schema)
                 .Where(KcbPhieukhamThiluc.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                 .And(KcbPhieukhamThiluc.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                 .ExecuteSingle<KcbPhieukhamThiluc>();
            //if (objKhamthiluc != null)
            //{
            //    autoChandoanMatphai._Text = objKhamthiluc.ChandoanPhai;
            //    autoChandoanMattrai._Text = objKhamthiluc.ChandoanTrai;
            //}
            if (_KcbChandoanKetluan != null)
            {
                cboKQDT.SelectedValue= Utility.sDbnull(_KcbChandoanKetluan.Ketluan);
                txtidchandoan.Text = Utility.sDbnull(_KcbChandoanKetluan.IdChandoan, "-1");
                // txtHuongdieutri.SetCode(_KcbChandoanKetluan.HuongDieutri);
                cboHDT.SelectedValue=Utility.sDbnull( _KcbChandoanKetluan.HuongDieutri);
                ////Tạm rem 2 dòng dưới xem xét sau
                //cboHDT.SelectedValue = _KcbChandoanKetluan.HuongDieutri;
                //cboKQDT.SelectedValue = _KcbChandoanKetluan.Ketluan;
                autoLoidan.Text = _KcbChandoanKetluan.LoiDan;
                autoXutri.Text = _KcbChandoanKetluan.XuTri;
                txtSongaydieutri.Text = Utility.sDbnull(_KcbChandoanKetluan.SongayDieutri, "0");
                txtSoNgayHen.Text = Utility.sDbnull(_KcbChandoanKetluan.SoNgayhen, "0");
                dtpNgayHen.Value = dtpNgaydangky.Value.AddDays(Utility.Int32Dbnull(_KcbChandoanKetluan.SoNgayhen, 0));
                txtHa.Text = Utility.sDbnull(_KcbChandoanKetluan.Huyetap);
                txtLydokham._Text = Utility.sDbnull(_KcbChandoanKetluan.TrieuchungBandau);
                txtQuatrinhbenhly.Text = Utility.sDbnull(_KcbChandoanKetluan.QuatrinhBenhly);
                txtTiensubenh.Text = Utility.sDbnull(_KcbChandoanKetluan.TiensuBenh);
                txtCLS.Text = Utility.sDbnull(_KcbChandoanKetluan.TomtatCls);
                txtMach.Text = Utility.sDbnull(_KcbChandoanKetluan.Mach);
                txtNhipTim.Text = Utility.sDbnull(_KcbChandoanKetluan.Nhiptim);
                txtNhipTho.Text = Utility.sDbnull(_KcbChandoanKetluan.Nhiptho);
                txtNhietDo.Text = Utility.sDbnull(_KcbChandoanKetluan.Nhietdo);
                txtSPO2.Text = Utility.sDbnull(_KcbChandoanKetluan.SPO2);
                txtCannang.Text = Utility.sDbnull(_KcbChandoanKetluan.Cannang);
                txtChieucao.Text = Utility.sDbnull(_KcbChandoanKetluan.Chieucao);
                txtPara.Text = Utility.sDbnull(_KcbChandoanKetluan.Para);
                chkQuaibi.Checked = Utility.Byte2Bool(_KcbChandoanKetluan.QuaiBi);
                txtBMI.Text = Utility.sDbnull(_KcbChandoanKetluan.ChisoIbm);
                txtthiluc_mp.Text = Utility.sDbnull(_KcbChandoanKetluan.ThilucMp);
                txtthiluc_mt.Text = Utility.sDbnull(_KcbChandoanKetluan.ThilucMt);
                txtnhanap_mp.Text = Utility.sDbnull(_KcbChandoanKetluan.NhanapMp);
                txtnhanap_mt.Text = Utility.sDbnull(_KcbChandoanKetluan.NhanapMt);
                txtNhanxet._Text = Utility.sDbnull(_KcbChandoanKetluan.NhanXet);
                txtCheDoAn._Text = Utility.sDbnull(_KcbChandoanKetluan.ChedoDinhduong, "");
                if (!string.IsNullOrEmpty(Utility.sDbnull(_KcbChandoanKetluan.Nhommau)) &&
                    Utility.sDbnull(_KcbChandoanKetluan.Nhommau) != "-1")
                    txtNhommau._Text = Utility.sDbnull(_KcbChandoanKetluan.Nhommau);

                txtChanDoan._Text = Utility.sDbnull(_KcbChandoanKetluan.Chandoan);
                //txtChanDoan.SetCode(_KcbChandoanKetluan.Chandoan);
                txtChanDoanKemTheo._Text = Utility.sDbnull(_KcbChandoanKetluan.ChandoanKemtheo);

                txt_phantruoc_mp.Text = Utility.sDbnull(_KcbChandoanKetluan.PhantruocMatphai);
                txt_phantruoc_mt.Text = Utility.sDbnull(_KcbChandoanKetluan.PhantruocMattrai);
                txt_daymat_mp.Text = Utility.sDbnull(_KcbChandoanKetluan.DaymatMatphai);
                txt_daymat_mt.Text = Utility.sDbnull(_KcbChandoanKetluan.DaymatMattrai);
                txt_vannhan_mp.Text = Utility.sDbnull(_KcbChandoanKetluan.VannhanMatphai);
                txt_vannhan_mt.Text = Utility.sDbnull(_KcbChandoanKetluan.VannhanMattrai);
                autoICD_matphai._Text = Utility.sDbnull(_KcbChandoanKetluan.IcdMatphai);
                autoICD_mattrai._Text = Utility.sDbnull(_KcbChandoanKetluan.IcdMattrai);
                txtTenICDMatPhai.Text = Utility.sDbnull(_KcbChandoanKetluan.TenIcdMatphai);
                txtTenICDMatTrai.Text = Utility.sDbnull(_KcbChandoanKetluan.TenIcdMattrai);
                txtTenICD2Mat.Text = Utility.sDbnull(_KcbChandoanKetluan.MotaBenhchinh);
                auto_khammat._Text = Utility.sDbnull(_KcbChandoanKetluan.Khammat);
                if (Utility.ByteDbnull(_KcbChandoanKetluan.VitriIcdChinh, 0) == 0)
                    opt2mat.Checked = true;
                else if (Utility.ByteDbnull(_KcbChandoanKetluan.VitriIcdChinh, 0) == 1)
                    optMP.Checked = true;
                else
                    optMT.Checked = true;
                chkSotxuathuyet.Checked = Utility.Bool2Bool(_KcbChandoanKetluan.Sotxuathuyet);
                chkTaychanmieng.Checked = Utility.Bool2Bool(_KcbChandoanKetluan.Taychanmieng);
            }
        }
        private void GetTamUng()
        {
            return;
            if (objLuotkham.MaDoituongKcb == "BHYT")
            {
                lblMsg.Text = "";
            }
            else
            {
                Utility.SetMsg(lblMsg, string.Format(
               "Bệnh nhân có tạm ứng số tổng tiền: {0}. Bác sỹ cần lưu ý khi kê chỉ định cho người bệnh"
               , noitru_TamungHoanung.LaySoTienTamUng(txtPatient_Code.Text, Utility.Int64Dbnull(txtPatient_ID), 0).ToString("N")), true);
            }
        }
        private void LoadBenh()
        {
            try
            {
                isLike = false;
                txtChanDoan._Text = Utility.sDbnull(_KcbChandoanKetluan.Chandoan);
                txtChanDoanKemTheo.Text = Utility.sDbnull(_KcbChandoanKetluan.ChandoanKemtheo);
                autoICD_2mat.SetCode(Utility.sDbnull(_KcbChandoanKetluan.MabenhChinh));
                string dataString = Utility.sDbnull(_KcbChandoanKetluan.MabenhPhu, "");
                isLike = true;
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
            catch (Exception ex)
            {
                log.Trace("Lỗi:"+ ex.Message);
            }
        }

        private void ModifyByLockStatus(byte lockstatus)
        {
            cmdThemDonthuoc.Enabled = !Utility.isTrue(lockstatus);
            cmdSuadonthuoc.Enabled = grdPresDetail.RowCount > 0 && !Utility.isTrue(lockstatus);
            cmdXoadonthuoc.Enabled = grdPresDetail.RowCount > 0 && !Utility.isTrue(lockstatus);
            cmdIndonthuoc.Enabled = grdPresDetail.RowCount > 0 && !string.IsNullOrEmpty(m_strMaLuotkham);
            ctxDelDrug.Enabled = cmdSuadonthuoc.Enabled;

            cmdInsertAssign.Enabled = !Utility.isTrue(lockstatus);
            cmdUpdate.Enabled = grdAssignDetail.RowCount > 0 && !Utility.isTrue(lockstatus);
            cmdDelteAssign.Enabled = grdAssignDetail.RowCount > 0 && !Utility.isTrue(lockstatus);
            cmdPrintAssign.Enabled = grdAssignDetail.RowCount > 0 && !string.IsNullOrEmpty(m_strMaLuotkham);
            chkIntach.Enabled = cmdPrintAssign.Enabled;
            cboServicePrint.Enabled = cmdPrintAssign.Enabled;
            //ctxDelCLS.Enabled = cmdUpdate.Enabled;
        }

        private void KiemTraDaInPhoiBhyt()
        {
            lblMessage.Visible = objLuotkham != null && objLuotkham.MaDoituongKcb == "BHYT";
            if (objLuotkham != null && objLuotkham.MaDoituongKcb == "BHYT")
            {
                SqlQuery sqlQuery = new Select().From(KcbPhieuDct.Schema)
                    .Where(KcbPhieuDct.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(txtPatient_Code.Text))
                    .And(KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtPatient_ID.Text))
                    .And(KcbPhieuDct.Columns.KieuThanhtoan).IsEqualTo(Utility.Int32Dbnull(KieuThanhToan.NgoaiTru));
                if (sqlQuery.GetRecordCount() > 0)
                {
                    var objPhieuDct = sqlQuery.ExecuteSingle<KcbPhieuDct>();
                    if (objPhieuDct != null)
                    {
                        Utility.SetMsg(lblMessage,
                                       string.Format("Đã in phôi bởi {0}, vào lúc: {1}", objPhieuDct.NguoiTao,
                                                     objPhieuDct.NgayTao), false);
                        cmdKetthuckham.Enabled =  Utility.Coquyen("suachandoansaukhiinphoi");
                     //   cmdSave.Enabled = false;
                        grd_ICD.Enabled = false;
                        cmdChuyenPhong.Enabled = false;
                        cmdLuuKetluan.Enabled = false;
                        toolTip1.SetToolTip(cmdKetthuckham, "Bệnh nhân đã kết thúc nên bạn không thể sửa thông tin được nữa");
                    }
                }
                else
                {
                    grd_ICD.Enabled = true;
                    cmdLuuKetluan.Enabled = true;
                    cmdChuyenPhong.Enabled = true;
                    cmdKetthuckham.Enabled = true;
                    toolTip1.SetToolTip(cmdKetthuckham, "Nhấn vào đây để kết thúc khám cho Bệnh nhân(Phím tắt Ctrl+S)");
                    lblMessage.Visible = false;
                }
            } //Đối tượng dịch vụ sẽ luôn hiển thị nút lưu
            else
            {
                grd_ICD.Enabled = true;
                cmdKetthuckham.Enabled = true;
                cmdLuuKetluan.Enabled = true;
                cmdChuyenPhong.Enabled = true;
            }
        }

        private string GetTenBenh(string maBenh)
        {
            string TenBenh = "";
            DataRow[] arrMaBenh =
                globalVariables.gv_dtDmucBenh.Select(string.Format(DmucBenh.Columns.MaBenh + "='{0}'", maBenh));
            if (arrMaBenh.GetLength(0) > 0) TenBenh = Utility.sDbnull(arrMaBenh[0][DmucBenh.Columns.TenBenh], "");
            return TenBenh;
        }
        

        private int i = 0;
        private void HienthithongtinBn()
        {
            try
            {
                Utility.WaitNow(this);
                Utility.FreeLockObject(m_strMaLuotkham);
                
                pnlAttachedFiles.Controls.Clear();
                if (!Utility.isValidGrid(grdList))
                {
                    ClearControl();
                    return;
                }
              
                //Tạm thời comment bỏ 2 dòng dưới để luôn hiển thị 2 nút này(190803)
                //cmdInphieuhen.Visible = false;
                //cmdInTTDieuTri.Visible = false;
                txtbenhan.Visible = false;
                //lblBenhan.Visible = cboChonBenhAn.Visible;
                if (dt_ICD_PHU != null) dt_ICD_PHU.Rows.Clear();
                GetData();
                txtMach.Focus();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                    Utility.ShowMsg(exception.ToString());
            }
            finally
            {
                Utility.DefaultNow(this);
                dtpNgayHen.MinDate = dtpNgaydangky.Value;
            }
        }

        private void grdList_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "cmdCHONBN")
                {
                    if(!Lakhamthiluc && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_COKHAM_DOTHILUC","0",false)=="1")
                    {
                        //Kiểm tra đã khám thị lực chưa. nếu chưa khám xong thì thông báo
                        KcbDangkyKcb objKTL = new Select().From(KcbDangkyKcb.Schema)
                            .Where(KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(Utility.Int64Dbnull(grdList.GetValue("id_benhnhan"),-1))
                            .And(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(Utility.Int64Dbnull(grdList.GetValue("ma_luotkham"), ""))
                            .And(KcbDangkyKcb.Columns.KhamThiluc).IsEqualTo(1)
                            .ExecuteSingle<KcbDangkyKcb>();
                        if(objKTL!=null && objKTL.TrangThai<=0)
                        {
                            Utility.ShowMsg(string.Format("Người bệnh {0} chưa được kết thúc khám Đo thị lực {1}. Vui lòng yêu cầu Phòng đo thị lực kết thúc khám trước khi chuyển người bệnh lên phòng khám",grdList.GetValue("ten_benhnhan").ToString(), objKTL.TenDichvuKcb));
                            return;
                        }    
                    }    
                    Utility.FreeLockObject(m_strMaLuotkham);
                    _buttonClick = true;
                    GridEXColumn gridExColumn = grdList.RootTable.Columns[KcbDangkyKcb.Columns.SttKham];
                    grdList.Col = gridExColumn.Position;
                    uiTabKqCls.Width = 0;
                    HienthithongtinBn();
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
            }
        }

        private void Huyketthuckham()
        {
            try
            {
                if (objLuotkham == null)
                    return;
                if (objCongkham == null)
                    objCongkham = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txt_idchidinhphongkham.Text));
                if (Utility.Int16Dbnull(objCongkham.IdBacsikham, -1) > 0 && Utility.Int16Dbnull(objCongkham.IdBacsikham, -1) != Utility.Int16Dbnull(txtBacsi.MyID, -1))
                {
                    DmucNhanvien objNV = DmucNhanvien.FetchByID(Utility.Int32Dbnull(objCongkham.IdBacsikham, -1));
                    Utility.ShowMsg(string.Format("Ca khám được kết thúc bởi bác sĩ {0} nên bạn cần chọn bác sĩ khám là {1} để có thể thực hiện hủy kết thúc khám", objNV.TenNhanvien, objNV.TenNhanvien));
                    return;
                }
                //Kiểm tra nếu đã in phôi thì cần hủy in phôi
                var item = new Select().From(KcbPhieuDct.Schema)
                    .Where(KcbPhieuDct.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .And(KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).ExecuteSingle<KcbPhieuDct>();
                if (item != null)
                {
                    Utility.ShowMsg(
                        "Bệnh nhân này thuộc đối tượng BHYT đã được in phôi. Bạn cần liên hệ bộ phận thanh toán hủy in phôi để mở khóa bệnh nhân");
                    return;
                }
                objLuotkham = Utility.getKcbLuotkham(objLuotkham);
                if (objLuotkham.TthaiChuyendi > 0)
                {
                    Utility.ShowMsg("Bệnh nhân đã được chuyển viện nên bạn không thể thực hiện thao tác khám lại. Đề nghị hủy chuyển viện trước");
                    return;
                }
                if (objLuotkham.TrangthaiNoitru >= 1)
                {
                    Utility.ShowMsg("Bệnh nhân đã được nhập viện nên bạn không thể thực hiện thao tác khám lại. Đề nghị hủy nhập viện trước");
                    return;
                }
                new Update(KcbLuotkham.Schema)
                    .Set(KcbLuotkham.Columns.Locked).EqualTo(0)
                    .Set(KcbLuotkham.Columns.TrangthaiNgoaitru).EqualTo(0)
                    .Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(string.Empty)
                    .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(null)
                    .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(
                        objLuotkham.MaLuotkham)
                    .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                objLuotkham.Locked = 0;
                objLuotkham.TrangthaiNgoaitru = 0;
               
                if (objCongkham != null)
                {
                    objCongkham.TrangThai = 0;
                    //objCongkham.IdBacsikham = null;
                    objCongkham.MarkOld();
                    objCongkham.Save();
                    chkDaThucHien.Checked = chkDaThucHien.Visible = false;
                    DataRow[] arrDr = m_dtDanhsachbenhnhanthamkham.Select("id_kham=" + objCongkham.IdKham);
                    if (arrDr.Length > 0)
                    {
                        arrDr[0]["trang_thai"] = chkDaThucHien.Checked ? 1 : 0;
                        arrDr[0]["ten_trangthai_congkham"] = "Chưa khám";
                    }
                }
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    txtBacsi.SetId(-1);
                }
                else
                {
                    txtBacsi.SetId(Utility.Int16Dbnull(objCongkham.IdBacsikham, -1) > 0 ? Utility.Int16Dbnull(objCongkham.IdBacsikham, -1) : globalVariables.gv_intIDNhanvien);
                }
                Utility.Log(Name, globalVariables.UserName, string.Format("Bệnh nhân {0} có mã lần khám {1} và ID {2} được Hủy kết thúc khám bởi {2} ",objBenhnhan.TenBenhnhan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, globalVariables.UserName), newaction.CancelData, this.GetType().Assembly.ManifestModule.Name);
                //ModifyByLockStatus(objLuotkham.Locked);
                VisibleLockButton();
                if (!cmdUnlock.Enabled)
                    toolTip1.SetToolTip(cmdUnlock,
                                        "Bạn không có quyền mở khóa Bệnh nhân này. Đề nghị liên hệ Quản trị hệ thống để được mở khóa");
                else
                    toolTip1.SetToolTip(cmdUnlock,
                                        "Nhấn vào đây để mở khóa cho bệnh nhân đang chọn(Phím tắt Ctrl+U). Điều kiện là chỉ mở khóa đối với đối tượng Dịch vụ. Muốn mở khóa đối tượng BHYT thì cần liên lạc với bộ phận thanh toán hủy in phôi BHYT");
                
                GetData();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
            }
        }

        /// <summary>
        /// hàm thực hiện việc dùng phím tắt in phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_KCB_THAMKHAM_V2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                Utility.Showhelps(this.GetType().Assembly.ManifestModule.Name, this.Name);
            if (e.KeyCode == Keys.Enter)
            {
                Control ActControl = Utility.getActiveControl(this);
                 if (ActControl != null && ActControl.GetType().Equals(cboHDT.GetType()))
                {
                        return;
                }    
                   else  if (ActControl!=null && ActControl.GetType().Equals(typeof(EditBox)))
                {
                    EditBox box = ActControl as EditBox;
                    if (box.Multiline)
                        return;
                    else
                        SendKeys.Send("{TAB}");
                }
                else if (ActControl != null && ActControl.GetType().Equals(typeof(TextBox)))
                {
                    TextBox box = ActControl as TextBox;
                    if (box.Multiline)
                    {
                        return;
                    }
                    else
                        SendKeys.Send("{TAB}");
                }
                else if (ActControl != null && ActControl.GetType().Equals(typeof(AutoCompleteTextbox_Danhmucchung)))
                {
                    //AutoCompleteTextbox_Danhmucchung box = ((AutoCompleteTextbox_Danhmucchung)ActControl ;
                    //else if (box.Name == txtTAG.Name)
                    //{

                    //    txtTAG.SelectAll();
                    //    txtTAG.Focus();
                    //    return;
                    //}
                    //else
                    //{
                    //    if (box.Multiline)
                    //    {
                    //        return;
                    //    }
                    //    else
                    //        SendKeys.Send("{TAB}");
                    //}
                }
                else if (ActControl != null && ActControl.Name == autoMabenhphu.Name)
                    if (Utility.DoTrim(autoMabenhphu.Text).Length > 0)
                        return;
                    else
                        SendKeys.Send("{TAB}");
                
                else if (ActControl != null && ActControl.Name == autoICD_2mat.Name)
                {
                    txtTenICDMatPhai.Focus();
                    txtTenICDMatPhai.SelectAll();
                }
                else if (ActControl != null && ActControl.Name == autoICD_matphai.Name)
                {
                    autoICD_mattrai.Focus();
                    autoICD_mattrai.SelectAll();
                }
                else if (ActControl != null && ActControl.Name == autoICD_mattrai.Name)
                {
                    autoMabenhphu.Focus();
                    autoMabenhphu.SelectAll();
                }
                else
                    SendKeys.Send("{TAB}");

                //if (tabPageHoibenh.ActiveControl != null)
                //{
                //    Control ctr = tabPageHoibenh.ActiveControl;
                //    if (ctr.GetType().Equals(typeof(EditBox)))
                //    {
                //        EditBox box = ctr as EditBox;
                //        if (box.Multiline)
                //        {
                //            return;
                //        }
                //        else
                //            SendKeys.Send("{TAB}");
                //    }
                //    else if (ctr.GetType().Equals(typeof(TextBox)))
                //    {
                //        TextBox box = ctr as TextBox;
                //        if (box.Multiline)
                //        {
                //            return;
                //        }
                //        else
                //            SendKeys.Send("{TAB}");
                //    }
                //    //else if (ctr.Name == autoBacsithamgia.Name)
                //    //    if (Utility.DoTrim(autoBacsithamgia.Text).Length > 0)
                //    //        return;
                //    //    else
                //    //        SendKeys.Send("{TAB}");
                //    //else if (ctr.Name == txtChanDoanTuyenDuoiKKB.Name)
                //    //{
                //    //    uiTabInfor.SelectedIndex = 1;
                //    //    txtTomTatDienBienBenh.Focus();
                //    //}
                //    else
                //        SendKeys.Send("{TAB}");

                //}
                //else if (tabPageChanDoan.ActiveControl != null)
                //{
                //    Control ctr = tabPageChanDoan.ActiveControl;
                //    if (ctr.GetType().Equals(typeof(EditBox)))
                //    {
                //        EditBox box = ctr as EditBox;
                //        if (box.Multiline)
                //        {
                //            return;
                //        }
                //        else
                //            SendKeys.Send("{TAB}");
                //    }
                //    else if (ctr.GetType().Equals(typeof(TextBox)))
                //    {
                //        TextBox box = ctr as TextBox;
                //        if (box.Multiline)
                //        {
                //            return;
                //        }
                //        else
                //            SendKeys.Send("{TAB}");
                //    }
                //    else if (ctr.GetType().Equals(typeof(AutoCompleteTextbox_Danhmucchung)))
                //    {
                //        AutoCompleteTextbox_Danhmucchung box = ctr as AutoCompleteTextbox_Danhmucchung;
                //        if (box.Multiline)
                //        {
                //            return;
                //        }
                //        else
                //            SendKeys.Send("{TAB}");
                //    }
                //    else if (ctr.Name == autoMabenhphu.Name)
                //        if (Utility.DoTrim(autoMabenhphu.Text).Length > 0)
                //            return;
                //        else
                //            SendKeys.Send("{TAB}");
                //    else if (ctr.Name == AutoMabenhchinh.Name)
                //    {
                //        autoMabenhphu.Focus();
                //        autoMabenhphu.SelectAll();
                //    }
                //    else
                //        SendKeys.Send("{TAB}");

                //}
                //else if ((ActiveControl != null && ActiveControl.Name == grdList.Name) ||
                //    (tabPageChanDoan.ActiveControl != null && tabPageChanDoan.ActiveControl.Name == autoMabenhphu.Name))
                //    return;
                //else if ((tabPageChanDoan.ActiveControl != null && tabPageChanDoan.ActiveControl.Name == AutoMabenhchinh.Name))
                //{
                //    autoMabenhphu.Focus();
                //    autoMabenhphu.SelectAll();
                //}
                //else
                //    SendKeys.Send("{TAB}");
            }
            
            if ((e.Control && e.KeyCode == Keys.P) || e.KeyCode==Keys.F4)
            {
                if (tabDiagInfo.SelectedTab == tabPageChiDinhCLS)
                    cmdIntachPhieu_Click(cmdIntachPhieu, new EventArgs());
                if (tabDiagInfo.SelectedTab == tabPageChanDoan)
                    cmdInTTDieuTri_Click(cmdInTTDieuTri, new EventArgs());
                else
                    cmdPrintPres_Click(cmdIndonthuoc, new EventArgs());
            }
            if (e.Control & e.KeyCode == Keys.C) SaochepIdBN();
            if (e.Control & e.KeyCode == Keys.F) cmdSearch.PerformClick();
            if (e.KeyCode == Keys.Escape) Close();
            if (e.Control && e.KeyCode == Keys.U)
                Huyketthuckham();
            if (e.Control && e.KeyCode == Keys.F5)
            {
                //splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            }
            if (e.KeyCode == Keys.F11 && PropertyLib._AppProperties.ShowActiveControl)
                if (ActiveControl != null) Utility.ShowMsg(ActiveControl.Name);
            if (e.KeyCode == Keys.F6)
            {
                txtPatient_Code.SelectAll();
                txtPatient_Code.Focus();
                return;
            }
            if (e.Control && e.KeyCode==Keys.D1)//(e.Alt && e.KeyCode == Keys.F1)
            {
                tabDiagInfo.SelectedIndex = 0;
                txtMach.Focus();
                return;
            }
            if (e.Control && e.KeyCode == Keys.D2)//(e.Alt && e.KeyCode == Keys.F2)
            {
                tabDiagInfo.SelectedIndex = 1;
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
                return;
            }
            if (e.Control && e.KeyCode == Keys.D3)//(e.Alt && e.KeyCode == Keys.F3)
            {
                tabDiagInfo.SelectedIndex = 2;
                return;
            }
            if (e.Control && e.KeyCode == Keys.D4)//(e.Alt && e.KeyCode == Keys.F3)
            {
                tabDiagInfo.SelectedTab = tabPageChidinhThuoc;
                if (grdPresDetail.RowCount <= 0)
                {
                    cmdThemDonthuoc.Focus();
                    cmdCreateNewPres_Click(cmdThemDonthuoc, new EventArgs());
                }
                else
                {
                    cmdSuadonthuoc.Focus();
                    cmdUpdatePres_Click(cmdSuadonthuoc, new EventArgs());
                }
            }
            if (e.Control && e.KeyCode == Keys.D4)//(e.Alt && e.KeyCode == Keys.F3)
            {
                tabDiagInfo.SelectedIndex = 3;
                return;
            }
            if (e.Control && e.KeyCode == Keys.D5)//(e.Alt && e.KeyCode == Keys.F3)
            {
                tabDiagInfo.SelectedIndex = 4;
                return;
            }
            if (e.Control && e.KeyCode == Keys.D6)//(e.Alt && e.KeyCode == Keys.F3)
            {
                tabDiagInfo.SelectedIndex = 5;
                return;
            }
            if (e.Control && e.KeyCode == Keys.D7)//(e.Alt && e.KeyCode == Keys.F3)
            {
                tabDiagInfo.SelectedIndex = 6;
                return;
            }

            if (e.Control && e.KeyCode == Keys.S)
            {
                if (tabDiagInfo.SelectedIndex == 0)
                {
                    cmdLuuChandoan_sobo.PerformClick();
                }
                else if (tabDiagInfo.SelectedIndex == 2)
                {
                    cmdLuuKetluan.PerformClick();
                }
                //cmdSave.PerformClick();
            }
            if (e.Control && e.KeyCode == Keys.K)
            {
                cmdKetthuckham.PerformClick();
            }
            if (e.Control && e.KeyCode == Keys.N)
            {
                if (tabDiagInfo.SelectedTab == tabPageChidinhThuoc)
                    cmdCreateNewPres_Click(cmdThemDonthuoc, new EventArgs());
                else
                    cmdInsertAssign_Click(cmdInsertAssign, new EventArgs());
            }
        }

        private void txtSoKham_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSoKham.Text))
            {
                chkByDate.Checked = false;
                cmdSearch.PerformClick();
            }
        }

        private void txtSoKham_Click(object sender, EventArgs e)
        {
        }

        private void txtSoKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmdSearch.PerformClick();
        }
        bool CKEditorInput = true;//true=CKeditor;false=Dynamic
        private void grdAssignDetail_SelectionChanged(object sender, EventArgs e)
        {
            RowCLS =Utility.findthelastChild(grdAssignDetail.CurrentRow);
            ModifyCommmands();
            TuybiennutchuyenCLS();
            ShowResult();
            ResetNhominCLS();
            pnlAttachedFiles.Height = RowCLS!=null ? 60 : 0;
        }
        void LoadHTML()
        {
            string noidung = txtNoiDung.Text;
            webBrowser1.Document.InvokeScript("setValue", new[] { noidung });
        }
        DmucVungkhaosat vks = null;
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
                txtNoiDung.Text =Utility.sDbnull( dtKQCDHA.Rows[0]["mota_html"],"");
                timer1.Start();
                LoadHTML();
            }
        }
        DataTable dtKQXN = null;
        private void ShowResult()
        {
            try
            {
                //if (!Utility.isValidGrid(grdAssignDetail) || RowCLS == null)
                if ( RowCLS==null)
                {
                    currRowIdx = -1;
                    grdKetqua.DataSource = null;
                    txtNoiDung.Clear();
                    splitContainer3.Panel2Collapsed = true;
                    return;
                }
                bool VisibleKQXN = Utility.Laygiatrithamsohethong("THAMKHAM_NHAPKQ_XN", "0", true) == "1";
                dtKQXN = null;
                //uiTabKqCls.Width = 0;
                mnuNhapKQCDHA.Visible = false;
                mnuNhapKQXN.Visible = false;
                LoadAttachedFiles();
                CKEditorInput = Utility.GetValueFromGridColumn(RowCLS, KcbChidinhclsChitiet.Columns.ResultType) == "1";
                Int16 trangthaiChitiet =
                    Utility.Int16Dbnull(
                        Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.TrangthaiChitiet), 0);
                Int16 coChitiet =
                    Utility.Int16Dbnull(
                        Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.CoChitiet), 0);

                int idChitietdichvu =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.IdChitietdichvu), 0);
                int idDichvu =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.IdDichvu), 0);


                int idChidinh =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.IdChidinh), 0);
                string ketluanHa =
                  Utility.sDbnull(
                      Utility.GetValueFromGridColumn(RowCLS, "ketluan_ha"), "");
                string maloaiDichvuCls =
                    Utility.sDbnull(
                        Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.MaLoaidichvu), "XN");
                int idChitietchidinh =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.IdChitietchidinh), 0);
                string maChidinh =
                    Utility.sDbnull(Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.MaChidinh),
                                    "XN");
                string maBenhpham =
                    Utility.sDbnull(Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.MaBenhpham),
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
                        mnuNhapKQCDHA.Visible = true;
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
                            ShowEditor(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(RowCLS, "id_chitietchidinh"), 0));
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
        private void ShowResult_bak()
        {
            try
            {
                bool VisibleKQXN = Utility.Laygiatrithamsohethong("THAMKHAM_NHAPKQ_XN", "0", true)=="1";
                dtKQXN = null;
                //uiTabKqCls.Width = 0;
                mnuNhapKQCDHA.Visible = false;
                mnuNhapKQXN.Visible = false;
                LoadAttachedFiles();
                CKEditorInput = Utility.GetValueFromGridColumn(RowCLS, KcbChidinhclsChitiet.Columns.ResultType) == "1";
                Int16 trangthaiChitiet =
                    Utility.Int16Dbnull(
                        Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.TrangthaiChitiet), 0);
                Int16 coChitiet =
                    Utility.Int16Dbnull(
                        Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.CoChitiet), 0);

                int idChitietdichvu =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.IdChitietdichvu), 0);
                int idDichvu =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.IdDichvu), 0);

                
                int idChidinh =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.IdChidinh), 0);
                string ketluanHa =
                  Utility.sDbnull(
                      Utility.GetValueFromGridColumn(RowCLS, "ketluan_ha"), "");
                string maloaiDichvuCls =
                    Utility.sDbnull(
                        Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.MaLoaidichvu), "XN");
                int idChitietchidinh =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.IdChitietchidinh), 0);
                string maChidinh =
                    Utility.sDbnull(Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.MaChidinh),
                                    "XN");
                string maBenhpham =
                    Utility.sDbnull(Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.MaBenhpham),
                                    "XN");
                if( Utility.Coquyen("quyen_nhap_kqxn"))
                    grdKetqua.RootTable.Columns["Ket_qua"].EditType = EditType.TextBox;
                else
                    grdKetqua.RootTable.Columns["Ket_qua"].EditType = EditType.NoEdit;
                
                if (trangthaiChitiet <= 2)
                    //0=Mới chỉ định;1=Đã chuyển CLS;2=Đang thực hiện;3= Đã có kết quả CLS;4=Đã xác nhận kết quả
                {
                    if (maloaiDichvuCls != "XN")
                    {
                        pnlXN.SendToBack();
                        uiTabKqCls.Width = 0;
                        mnuNhapKQCDHA.Visible = true;
                    }
                    else
                    {
                        pnlXN.BringToFront();
                        uiTabKqCls.Width =VisibleKQXN? PropertyLib._ThamKhamProperties.DorongVungKetquaCLS:0;
                        ShowKQXN();
                        //mnuNhapKQXN.Visible = true; 
                    }
                   
                    Application.DoEvents();
                }
                else
                {
                    if (maloaiDichvuCls == "XN")
                        pnlXN.BringToFront();
                    else
                        pnlXN.SendToBack();
                    if (PropertyLib._ThamKhamProperties.HienthiKetquaCLSTrongluoiChidinh)
                    {
                        if (coChitiet == 1 || maloaiDichvuCls != "XN")
                            uiTabKqCls.Width = PropertyLib._ThamKhamProperties.DorongVungKetquaCLS;
                        else
                            uiTabKqCls.Width = 0;
                    }
                    else
                    {
                        uiTabKqCls.Width = PropertyLib._ThamKhamProperties.DorongVungKetquaCLS;
                    }
                    //Utility.ShowColumns(grdKetqua, coChitiet == 1 ? lstKQCochitietColumns : lstKQKhongchitietColumns);
                    //Lấy dữ liệu CLS
                    if (maloaiDichvuCls == "XN")
                    {
                        //mnuNhapKQXN.Visible = true;
                        uiTabKqCls.Width =VisibleKQXN? PropertyLib._ThamKhamProperties.DorongVungKetquaCLS:0;
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
                        mnuNhapKQCDHA.Visible = true;
                        if (CKEditorInput)
                        {
                            pnlCKEditor.BringToFront();
                            ShowEditor(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(RowCLS, "id_VungKS"), 0));
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
        #region NhapKQXN
        string MaBenhpham = "";
        string MaChidinh = "";
        int IdChitietdichvu = -1;
        int currRowIdx = -1;
        int id_chidinh = -1;
        int id_dichvu = -1;
        int co_chitiet = -1;
        void ShowKQXN()
        {
            if (RowCLS==null) return;
            int tempRowIdx = RowCLS.RowIndex;
            if (currRowIdx == -1 || currRowIdx != tempRowIdx)
            {
                currRowIdx = tempRowIdx;
                id_dichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.IdDichvu), 0);
                IdChitietdichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.IdChitietdichvu), 0);
                co_chitiet = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.CoChitiet), 0);
                id_chidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.IdChidinh), 0);
                ma_luotkham = Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.MaLuotkham);
                MaChidinh = Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.MaChidinh);
                MaBenhpham = Utility.GetValueFromGridColumn(RowCLS, VKcbChidinhcl.Columns.MaBenhpham);
                HienthiNhapketqua(id_dichvu, co_chitiet);
            }
        }
        void HienthiNhapketqua(int id_dichvu, int co_chitiet)
        {
            try
            {
               // DataTable dt = SPs.ClsTimkiemthongsoXNNhapketqua(ma_luotkham, MaChidinh, MaBenhpham, id_chidinh, co_chitiet, id_dichvu, IdChitietdichvu).GetDataSet().Tables[0];
                DataTable dt = SPs.ClsLayKetquaXn(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, MaChidinh, id_chidinh,0,objBenhnhan.IdGioitinh).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdKetqua, dt, true, true, "1=1", "stt_hthi_dichvu,stt_hthi_chitiet,stt_in");

                Utility.focusCell(grdKetqua, KcbKetquaCl.Columns.KetQua);
            }
            catch (Exception ex)
            {


            }
        }
        #endregion
        DataTable dtDynamicData = null;
        void FillDynamicValues()
        {
            try
            {
                pnlCKEditor.SendToBack();
                long v_id_chitietchidinh = Utility.Int64Dbnull(Utility.GetValueFromGridColumn(RowCLS, "id_chitietchidinh"), -1);
                int id_vungks = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(RowCLS, "id_vungks"), -1);
                if (v_id_chitietchidinh <= 0) return;
                
                flowDynamics.SuspendLayout();
                flowDynamics.Controls.Clear();
                 dtDynamicData = SPs.HinhanhGetDynamicFieldsValues(id_vungks, v_id_chitietchidinh).GetDataSet().Tables[0];
                 if (dtDynamicData.Rows.Count==0)
                {
                     pnlCKEditor.BringToFront();
                     ShowEditor(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(RowCLS, "id_VungKS"), 0));
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

       

        private void LayThongTinQuaMaLanKham(string maluotkham)
        {
            DataTable dtPatient = new DataTable();
            Utility.FreeLockObject(m_strMaLuotkham);
            string _patient_Code = maluotkham;
            ClearControl();
            txtPatient_Code.Text = _patient_Code;
            if (grdList.RowCount > 0 )
            {
                DataRow[] arrData_tempt = null;
                arrData_tempt =
                    m_dtDanhsachbenhnhanthamkham.Select(KcbLuotkham.Columns.MaLuotkham + "='" + _patient_Code +
                                                        "'");
                if (arrData_tempt.Length > 0)
                {
                    //Tạm bỏ sau khi dùng Autocomplete trạng thái 230523
                    //string _status = radChuaKham.Checked ? "0" : "1";
                    //string regStatus = Utility.sDbnull(arrData_tempt[0][KcbDangkyKcb.Columns.TrangThai], "0");
                    //if (_status != regStatus)
                    //{
                    //    if (regStatus == "1") radDaKham.Checked = true;
                    //    else
                    //        radChuaKham.Checked = true;
                    //}
                    Utility.GotoNewRowJanus(grdList, KcbLuotkham.Columns.MaLuotkham, _patient_Code);
                    if (Utility.isValidGrid(grdList)) grdList_DoubleClick(grdList, new EventArgs());
                    return;
                }
            }
            

            /////Tìm trên lưới không thấy sẽ tìm trên CSDL
            dtPatient = _KCB_THAMKHAM.TimkiemBenhnhan(txtPatient_Code.Text,
                                                      Utility.Int32Dbnull(cboPhongKhamNgoaiTru.SelectedValue, -1),
                                                      0, 0);

            DataRow[] arrPatients = dtPatient.Select(KcbLuotkham.Columns.MaLuotkham + "='" + _patient_Code + "'");
            if (arrPatients.GetLength(0) <= 0)
            {
                if (dtPatient.Rows.Count > 1)
                {
                    var frm = new frm_DSACH_BN_TKIEM(Args,0);
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
                txtPatient_Code.Text = _patient_Code;
            }
            //Khóa lại 240415 do tìm kiếm mã khám ko đồng nhất với bấm tìm kiếm(ô hiển thị) ca test : mã khám 953
            //DataTable dt_Patient = _KCB_THAMKHAM.TimkiemThongtinBenhnhansaukhigoMaBN
            //    (txtPatient_Code.Text, Utility.Int32Dbnull(cboPhongKhamNgoaiTru.SelectedValue, -1),
            //     globalVariables.MA_KHOA_THIEN);
            string lstIdPK = Utility.sDbnull(cboPhongKhamNgoaiTru.SelectedValue);
            DateTime dtFormDate = dtFromDate.Value;
            DateTime dt_ToDate = dtToDate.Value;
            int status = -1;
            int soKham = -1;
            dtPatient =
                       _KCB_THAMKHAM.LayDSachBnhanThamkham(
                           !chkByDate.Checked ? globalVariables.SysDate.AddYears(-3) : dtFormDate,
                           !chkByDate.Checked ? globalVariables.SysDate : dt_ToDate, -1, Utility.AutoFullPatientCode(txtMaluotkham.Text), "", status,
                           soKham, lstIdPK, globalVariables.StrQheNvpk, Args.Split('-')[0],
                           globalVariables.MA_KHOA_THIEN, -1, -1, "", globalVariables.UserName);
            grdList.DataSource = null;
            grdList.DataSource = dtPatient;
            if (dtPatient.Rows.Count > 0)
            {
                grdList.MoveToRowIndex(0);
                grdList.CurrentRow.BeginEdit();
                grdList.CurrentRow.Cells["MAUSAC"].Value = 1;
                grdList.CurrentRow.EndEdit();
                if (dt_ICD_PHU != null) dt_ICD_PHU.Rows.Clear();
                GetData();
                txtPatient_Code.SelectAll();
            }
            else
            {
                string sPatientTemp = txtPatient_Code.Text;
                m_strMaLuotkham = "";
                objLuotkham = null;
                objCongkham = null;
                objBenhnhan = null;
                ClearControl();

                txtPatient_Code.Text = sPatientTemp;
                txtPatient_Code.SelectAll();
                //Utility.SetMsg(lblMsg, "Không tìm thấy bệnh nhân có mã lần khám đang chọn",true);
            }
            txtMach.SelectAll();
        }
        private void txtPatient_Code_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    LayThongTinQuaMaLanKham(Utility.AutoFullPatientCode(txtPatient_Code.Text));
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin bệnh nhân");
            }
            finally
            {
                ModifyCommmands();
            }
        }

        private bool InValiMaBenh(string mabenh)
        {
            EnumerableRowCollection<DataRow> query = from benh in globalVariables.gv_dtDmucBenh.AsEnumerable()
                                                     where
                                                         Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) ==
                                                         Utility.sDbnull(mabenh)
                                                     select benh;
            if (query.Any()) return true;
            else return false;
        }

        private void txtTenICD2Mat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!InValiMaBenh(autoICD_2mat.MyCode))
                {
                    DSACH_ICD(txtTenICD2Mat, DmucChung.Columns.Ten, 0);
                    txtMaBenhphu.Focus();
                    //hasMorethanOne = false;
                }
                else
                    txtMaBenhphu.Focus();
            }
        }

        private void DSACH_ICD(EditBox tEditBox, string loaitimkiem, int cp)
        {
            try
            {
                string sFillter = "";
                if (loaitimkiem.ToUpper() == DmucChung.Columns.Ten)
                {
                    sFillter = " Disease_Name like '%" + tEditBox.Text + "%' OR FirstChar LIKE '%" + tEditBox.Text +
                               "%'";
                }
                else if (loaitimkiem == DmucChung.Columns.Ma)
                {
                    sFillter = DmucBenh.Columns.MaBenh + " LIKE '%" + tEditBox.Text + "%'";
                }
                DataRow[] dataRows;
                dataRows = dt_ICD.Select(sFillter);
                if (dataRows.Length == 1)
                {
                    if (cp == 0)
                    {
                        autoICD_2mat._Text = "";
                        autoICD_2mat._Text = Utility.sDbnull(dataRows[0][DmucBenh.Columns.MaBenh], "");
                        hasMorethanOne = false;
                        autoICD_2mat.Focus();
                    }
                    else if (cp == 1)
                    {
                        txtMaBenhphu.Text = Utility.sDbnull(dataRows[0][DmucBenh.Columns.MaBenh], "");
                        hasMorethanOne = false;
                        txtMaBenhphu_TextChanged(txtMaBenhphu, new EventArgs());
                        txtMaBenhphu_KeyDown(txtMaBenhphu, new KeyEventArgs(Keys.Enter));
                    }
                }
                else if (dataRows.Length > 1)
                {
                    var frmDanhSachIcd = new frm_DanhSach_ICD(cp);
                    frmDanhSachIcd.dt_ICD = dataRows.CopyToDataTable();
                    frmDanhSachIcd.ShowDialog();
                    if (!frmDanhSachIcd.has_Cancel)
                    {
                        List<GridEXRow> lstSelectedRows = frmDanhSachIcd.lstSelectedRows;
                        if (cp == 0)
                        {
                           
                        }
                        else if (cp == 1)
                        {
                            if (lstSelectedRows.Count == 1)
                            {
                                isLike = false;
                                txtMaBenhphu.Text = "";
                                txtMaBenhphu.Text =
                                    Utility.sDbnull(lstSelectedRows[0].Cells[DmucBenh.Columns.MaBenh].Value, "");
                                hasMorethanOne = false;
                                txtMaBenhphu_TextChanged(txtMaBenhphu, new EventArgs());
                                txtMaBenhphu_KeyDown(txtMaBenhphu, new KeyEventArgs(Keys.Enter));
                            }
                            else
                            {
                                foreach (GridEXRow row in lstSelectedRows)
                                {
                                    isLike = false;
                                    txtMaBenhphu.Text = "";
                                    txtMaBenhphu.Text =
                                        Utility.sDbnull(row.Cells[DmucBenh.Columns.MaBenh].Value, "");
                                    hasMorethanOne = false;
                                    txtMaBenhphu_TextChanged(txtMaBenhphu, new EventArgs());
                                    txtMaBenhphu_KeyDown(txtMaBenhphu, new KeyEventArgs(Keys.Enter));
                                }
                                hasMorethanOne = true;
                            }
                        }
                        tEditBox.Focus();
                    }
                    else
                    {
                        hasMorethanOne = true;
                        tEditBox.Focus();
                    }
                }
                else
                {
                    hasMorethanOne = true;
                    tEditBox.SelectAll();
                }
            }
            catch
            {
            }
            finally
            {
                isLike = true;
            }
        }


        private void cmdAddMaBenhPhu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaBenhphu.Text.TrimStart().TrimEnd() == "" || txtTenBenhPhu.Text.TrimStart().TrimEnd() == "")
                    return;
                //int record = dt_ICD.Select(string.Format(DmucBenh.Columns.MaBenh+ " ='{0}'", txtMaBenhphu.Text)).GetLength(0);
                EnumerableRowCollection<DataRow> query = from benh in dt_ICD_PHU.AsEnumerable()
                                                         where
                                                             Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) ==
                                                             txtMaBenhphu.Text
                                                         select benh;


                if (!query.Any())
                {
                    AddMaBenh(txtMaBenhphu.Text, TEN_BENHPHU);
                    txtMaBenhphu.ResetText();
                    txtTenBenhPhu.ResetText();
                    txtMaBenhphu.Focus();
                    txtMaBenhphu.SelectAll();
                }
                else
                {
                    txtMaBenhphu.ResetText();
                    txtTenBenhPhu.ResetText();
                    txtMaBenhphu.Focus();
                    txtMaBenhphu.SelectAll();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình thêm thông tin vào lưới");
            }
            finally
            {
            }
        }

        private void AddMaBenh(string maBenh, string tenBenh)
        {
            //DataRow[] arrDr = dt_ICD_PHU.Select(string.Format("MA_ICD='{0}'", MaBenh));
            EnumerableRowCollection<DataRow> query = from benh in dt_ICD_PHU.AsEnumerable()
                                                     where Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == maBenh
                                                     select benh;
            if (!query.Any())
            {
                DataRow drv = dt_ICD_PHU.NewRow();
                drv[DmucBenh.Columns.MaBenh] = maBenh;
                drv[KcbDangkyKcb.Columns.IdKham] = objCongkham.IdKham;
                EnumerableRowCollection<string> query1 = from benh in globalVariables.gv_dtDmucBenh.AsEnumerable()
                                                         where
                                                             Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) ==
                                                             maBenh
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

        private void txtTenBenhPhu_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{

            //    if (InValiMaBenh(txtMaBenhphu.Text))
            //        cmdAddMaBenhPhu.PerformClick();
            //    else
            //    {
            //        DSACH_ICD(txtTenBenhPhu, DmucChung.Columns.Ten, 1);
            //    }
            //}
            if (e.KeyCode == Keys.Enter)
            {
                if (hasMorethanOne)
                {
                    DSACH_ICD(txtTenBenhPhu, DmucChung.Columns.Ten, 1);
                    txtTenBenhPhu.Focus();
                }
                else
                    txtTenBenhPhu.Focus();
            }
        }

        private void grd_ICD_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "XOA" )
                {
                    if (Utility.Int64Dbnull(grd_ICD.GetValue("id_kham"), -1) > 0 || Utility.Coquyen("thamkham_xoamabenhphu"))
                    {
                        grd_ICD.CurrentRow.Delete();
                        dt_ICD_PHU.AcceptChanges();
                        grd_ICD.Refetch();
                        grd_ICD.AutoSizeColumns();
                    }
                    else
                    {
                        Utility.ShowMsg("Mã bệnh phụ bạn chọn xóa do bác sĩ ở phòng khám khác nhập(màu đỏ) nên bạn không thể xóa. Vui lòng chọn xóa các bệnh phụ không phải màu đỏ");
                    }
                }
                
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình xóa thông tin Mã ICD");
                throw;
            }
            finally
            {
            }
        }
        void Intomtatdieutri()
        {
          
            try
            {
                if (objLuotkham == null || !Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn chưa chọn người bệnh để thực hiện thăm khám. Đề nghị kiểm tra lại", "Thông Báo");
                    return;
                }
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THAMKHAM_CHOPHEPINTOMTAT_KHONGTHUOC", "0", false) == "0")
                {
                    if (grdPresDetail.GetDataRows().Length <= 0)
                    {
                        Utility.ShowMsg(
                            "Bạn cần kê đơn thuốc cho bệnh nhân trước khi thực hiện in tóm tắt điều trị ngoại trú",
                            "Thông báo");
                        tabDiagInfo.SelectedTab = tabPageChidinhThuoc;
                        return;
                    }
                }

                if (IN_TTAT_DTRI_NGOAITRU_2023())
                {
                    try
                    {
                        DataRow[] arrDr = m_dtDanhsachbenhnhanthamkham.Select("id_kham=" + txtReg_ID.Text);
                        if (arrDr.Length > 0 && objLuotkham.MaDoituongKcb == "DV")
                        {
                            arrDr[0]["Locked"] = 1;
                            objLuotkham.Locked = 1;
                            objLuotkham.NguoiKetthuc = globalVariables.UserName;
                            objLuotkham.NgayKetthuc = globalVariables.SysDate;
                        }
                        ActionResult actionResult =
                            _KCB_THAMKHAM.LockExamInfo(objLuotkham);
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                ModifyByLockStatus(Utility.ByteDbnull(objCongkham.TrangThai, 0));//objLuotkham.Locked);
                                IEnumerable<GridEXRow> query = from kham in grdList.GetDataRows()
                                                               where
                                                                   kham.RowType == RowType.Record &&
                                                                   Utility.Int32Dbnull(
                                                                       kham.Cells[KcbDangkyKcb.Columns.IdKham].Value) ==
                                                                   Utility.Int32Dbnull(txt_idchidinhphongkham.Text)
                                                               select kham;
                                if (query.Any())
                                {
                                    GridEXRow gridExRow = query.FirstOrDefault();
                                    if (gridExRow != null)
                                    {
                                        gridExRow.Cells[KcbLuotkham.Columns.Locked].Value = (byte?)1;
                                        gridExRow.Cells[KcbLuotkham.Columns.NguoiKetthuc].Value = globalVariables.UserName;
                                        gridExRow.Cells[KcbLuotkham.Columns.NgayKetthuc].Value = globalVariables.SysDate;
                                    }
                                    grdList.UpdateData();
                                    m_dtDanhsachbenhnhanthamkham.AcceptChanges();
                                    grdList.Refetch();
                                    Utility.GotoNewRowJanus(grdList, KcbDangkyKcb.Columns.IdKham,
                                                            Utility.sDbnull(txt_idchidinhphongkham.Text));
                                    var objStaff = (from p in globalVariables.gv_dtDmucNhanvien.AsEnumerable()
                                                    where p[DmucNhanvien.Columns.UserName].Equals(objLuotkham.NguoiKetthuc)
                                                    select p).FirstOrDefault();
                                    //var objStaff = 
                                    //    new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.UserNameColumn).IsEqualTo(
                                    //        Utility.sDbnull(objLuotkham.NguoiKetthuc, "")).ExecuteSingle<DmucNhanvien>();
                                    string tenNhanvien = objLuotkham.NguoiKetthuc;
                                    if (objStaff != null)
                                        tenNhanvien = Utility.sDbnull(objStaff["ten_nhanvien"], "");
                                    VisibleLockButton();
                                    if (!cmdUnlock.Enabled)
                                        toolTip1.SetToolTip(cmdUnlock,
                                                            "Bạn không có quyền mở khóa Bệnh nhân này. Đề nghị liên hệ " +
                                                            tenNhanvien + "(" + objLuotkham.NguoiKetthuc +
                                                            " - Là người khóa BN này) để được họ mở khóa. Hoặc liên hệ Quản trị hệ thống");
                                    else
                                        toolTip1.SetToolTip(cmdUnlock,
                                                            "Nhấn vào đây để mở khóa cho bệnh nhân đang chọn(Phím tắt Ctrl+U). Điều kiện là chỉ mở khóa đối với đối tượng Dịch vụ. Muốn mở khóa đối tượng BHYT thì cần liên lạc với bộ phận thanh toán hủy in phôi BHYT");
                                }
                                break;
                            case ActionResult.Error:
                                Utility.ShowMsg("Lỗi trong quá lưu thông tin ", "Thông báo lỗi", MessageBoxIcon.Error);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
               
            }
        }
        private void cmdInTTDieuTri_Click(object sender, EventArgs e)
        {
            cmdInTTDieuTri.Enabled = false;
            Intomtatdieutri();
            cmdInTTDieuTri.Enabled = true;
        }

        private void GetChanDoan(string icdChinh, string idcPhu, ref string icdName, ref string icdCode)
        {
            try
            {
                List<string> lstIcd = icdChinh.Split(',').ToList();
                DmucBenhCollection list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstIcd));
                foreach (DmucBenh item in list)
                {
                    icdName += item.TenBenh + "; ";
                    icdCode += item.MaBenh + "; ";
                }
                lstIcd = idcPhu.Split(',').ToList();
                list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstIcd));
                foreach (DmucBenh _item in list)
                {
                    icdName += _item.TenBenh + "; ";
                    icdCode += _item.MaBenh + "; ";
                }
                if (icdName.Trim() != "") icdName = icdName.Substring(0, icdName.Length - 1);
                if (icdCode.Trim() != "") icdCode = icdCode.Substring(0, icdCode.Length - 1);
            }
            catch(Exception ex)
            {
                if (globalVariables.IsAdmin) Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }

        private DataTable  GetChitietCls()
        {
            try
            {
                var subDtData = new DataTable("Temp");
                subDtData.Columns.AddRange(new[]
                                                {
                                                    new DataColumn("LOAI_CLS", typeof (string)),
                                                    new DataColumn("KETQUA", typeof (string)),
                                                    new DataColumn("STT", typeof (string))
                                                });

                DataTable temdt = SPs.KcbThamkhamLayketquacls(m_strMaLuotkham).GetDataSet().Tables[0];
                if (!temdt.Columns.Contains("id_dichvu_temp"))
                    temdt.Columns.Add(new DataColumn("id_dichvu_temp", typeof (string)));
                var lstidDichvu = new List<string>();
                foreach (DataRow dr in temdt.Rows)
                {
                    string serviceId = Utility.sDbnull(dr["id_dichvu"], "");
                    if (serviceId.Trim() == "") serviceId = "0";
                    dr["id_dichvu_temp"] = serviceId;
                    if (!lstidDichvu.Contains(serviceId)) lstidDichvu.Add(serviceId);
                }
                int stt = 0;
                foreach (string service_ID in lstidDichvu)
                {
                    stt++;
                    DataRow[] arrChitiet = temdt.Select("id_dichvu_temp='" + service_ID + "'");
                    string nhomCls = "";
                  //  StringBuilder kq = new StringBuilder();
                   string   kq = "";
                    foreach (DataRow drchitiet in arrChitiet)
                    {
                        if (nhomCls == "") nhomCls = Utility.sDbnull(drchitiet["ten_dichvu"], "");
                        kq += Utility.sDbnull(drchitiet["Ten_KQ"], "") + ":" + Utility.sDbnull(drchitiet["Ket_Qua"], "") +
                              " ; ";
                    }
                    if (kq.Length > 0) kq = kq.Substring(0, kq.Length - 2);
                    DataRow newDr = subDtData.NewRow();

                    if (service_ID == "0") nhomCls = "#";
                    newDr["STT"] = stt.ToString(CultureInfo.InvariantCulture);
                    newDr["LOAI_CLS"] = nhomCls;
                    newDr["KETQUA"] = kq;
                    subDtData.Rows.Add(newDr);
                }
                return subDtData;
            }
            catch
            {
                return new DataTable();
            }
        }

        public static ReportDocument GetReport(string fileName) //, ref string ErrMsg)
        {
            try
            {
                var crpt = new ReportDocument();
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
                Utility.ShowMsg("Lỗi khi nạp báo cáo " + fileName + "-->\r\n" + ex.Message);
                //ErrMsg = ex.Message;
                return null;
            }
        }
        private bool IN_TTAT_DTRI_NGOAITRU_2023()
        {
            try
            {
                string icdName = "";
                string icdCode = "";
                string kqcdha = "";
                string kqxn = "";
                string tomtatcls = "";
                DataSet dsData = _KCB_THAMKHAM.LaythongtinInphieuTtatDtriNgoaitru_2023(objCongkham.IdKham, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, Utility.ByteDbnull(objBenhnhan.IdGioitinh));
                THU_VIEN_CHUNG.CreateXML(dsData, "thamkham_intomtatdtri.XML");
                string ngayKedon = "";
                string[] arrDate =
                    Utility.sDbnull(
                        dsData.Tables[0].Rows.Count > 0
                            ? dsData.Tables[0].Rows[0]["NGAY_KEDON"]
                            : globalVariables.SysDate.ToString("dd/MM/yyyy"),
                        globalVariables.SysDate.ToString("dd/MM/yyyy")).Split('/');
                ngayKedon = "Ngày " + arrDate[0] + " tháng " + arrDate[1] + " năm " + arrDate[2];

                DataTable vDtData = dsData.Tables[0];
                DataTable vDtDataDucphuc = dsData.Tables[1];
                DataTable dtXn = dsData.Tables[2]; // GetChitietCls();
                DataTable dtcdha = dsData.Tables[4];
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
                //    THU_VIEN_CHUNG.CreateXML(sub_dtData, "sub_report.xml");
                // new DataTable("Temp");
                if (vDtData != null && vDtData.Rows.Count > 0)
                    GetChanDoan(Utility.sDbnull(vDtData.Rows[0]["mabenh_chinh"], ""),
                                Utility.sDbnull(vDtData.Rows[0]["mabenh_phu"], ""), ref icdName, ref icdCode);
                bool Mau_songngu = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THAMKHAM_TOMTATDIEUTRI_MAUSONGNGU", "0", true) == "1";
                bool GhepTTCLS = THU_VIEN_CHUNG.Laygiatrithamsohethong("THAMKHAM_INTOMTATDIEUTRINGOAITRU_GHEPTOMTAT_CLS", "0", true) == "1";
                bool GhepCHANDOAN_ICD = THU_VIEN_CHUNG.Laygiatrithamsohethong("THAMKHAM_INTOMTATDIEUTRINGOAITRU_GHEPCHANDOAN_ICD", "0", true) == "1";
                DataTable vDtLastData = Mau_songngu ? vDtDataDucphuc.Copy() : vDtData.Copy();
                if (vDtData != null && vDtLastData != null)
                {

                    foreach (DataRow dr in vDtData.Rows)
                    {
                        if (GhepCHANDOAN_ICD)
                        {
                            dr["chan_doan"] = string.Format("{0};{1},{2}", Utility.sDbnull(dr["chan_doan"]), Utility.sDbnull(dr["tenbenh_chinh"]), Utility.sDbnull(dr["tenbenh_phu"]));
                            //dr[DmucBenh.Columns.MaBenh] = ICD_Code;
                            dr["ma_icd"] = icdCode;
                        }
                        else
                        {
                            ////Bỏ để chỉ load chẩn đoán sơ bộ lên tóm tắt điều trị ngoại trú
                            //dr["chan_doan"] = Utility.sDbnull(dr["chan_doan"]).Trim() == ""
                            //    ? icdName
                            //    : Utility.sDbnull(dr["chan_doan"]) + "; " + icdName;
                            ////dr[DmucBenh.Columns.MaBenh] = ICD_Code;
                            //dr["ma_icd"] = icdCode;
                        }
                    }

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
                    string chidinhcls = "";
                    vDtData.AcceptChanges();
                    Utility.UpdateLogotoDatatable(ref vDtLastData);
                    THU_VIEN_CHUNG.CreateXML(vDtLastData, "thamkham_intomtatdtri_logo.XML");
                    string tieude = "",
                        reportname = "",
                        reportcode = "";
                    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THAMKHAM_CHOPHEP_INTOMTATDIEUTRI", "0", true) == "1")
                    {
                        reportcode = "thamkham_InPhieutomtatdieutringoaitru_A4";
                        ReportDocument crpt = Utility.GetReport("thamkham_InPhieutomtatdieutringoaitru_A4", ref tieude,
                            ref reportname);


                        if (crpt == null) return false;
                        var objForm = new frmPrintPreview("PHIẾU TÓM TẮT ĐIỀU TRỊ NGOẠI TRÚ", crpt, true, true);
                        crpt.SetDataSource(vDtLastData);
                        if (!Mau_songngu)
                        {
                            crpt.Subreports["crpt_xn"].SetDataSource(dtXn);
                            crpt.Subreports["crpt_cdha"].SetDataSource(dtcdha);
                        }
                        objForm.nguoi_thuchien = txtBacsi.Text;
                        objForm.mv_sReportFileName = Path.GetFileName(reportname);
                        objForm.mv_sReportCode = reportcode;
                        Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                        Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                        Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                        Utility.SetParameterValue(crpt, "email", globalVariables.Branch_Email);
                        Utility.SetParameterValue(crpt, "phone", globalVariables.Branch_Phone);
                        Utility.SetParameterValue(crpt, "website", globalVariables.Branch_Website);
                        Utility.SetParameterValue(crpt, "ReportTitle", tieude);
                        Utility.SetParameterValue(crpt, "NGAY_KEDON", ngayKedon);

                        objForm.crptViewer.ReportSource = crpt;
                        crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;

                        //In ngay
                        if (cboPrintPreviewTomtatdieutringoaitru.SelectedValue.ToString() == "1")
                            objForm.addTrinhKy_OnFormLoad();
                        if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                            PropertyLib._MayInProperties.PreviewInTomtatDieutriNgoaitru))
                        {
                            objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0, 1);
                            objForm.ShowDialog();
                        }
                        else
                        {
                            crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                            int soluongin = 1;
                            string userPrintNumberFile = Application.StartupPath + @"\UserPrintNumber\" +
                                                         globalVariables.UserName + "_" + objForm.mv_sReportFileName +
                                                         ".txt";
                            soluongin = File.Exists(userPrintNumberFile)
                                ? Utility.Int32Dbnull(File.ReadAllText(userPrintNumberFile))
                                : 1;
                            crpt.PrintToPrinter(soluongin, false, 0, 0);
                        }
                        Utility.FreeMemory(crpt);
                    }
                    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THAMKHAM_CHOPHEP_INDONTHUOC", "0", true) == "1" && vDtData.Rows.Count > 0)
                    {
                        foreach (DataRow dr in vDtData.Rows)
                        {
                            dr["chan_doan"] = dr["chandoan_theodon"];
                        }

                        Utility.UpdateLogotoDatatable(ref vDtData);
                        string reportcode1 = "";
                        reportcode1 = "thamkham_phieulinhthuocngoaitru";
                        ReportDocument crpt1 = Utility.GetReport(reportcode1, ref tieude, ref reportname);
                        if (crpt1 == null) return false;

                        var objForm1 = new frmPrintPreview("PHIẾU LĨNH THUỐC NGOẠI TRÚ", crpt1, true, true);
                        crpt1.SetDataSource(vDtData);
                        objForm1.nguoi_thuchien = txtBacsi.Text;
                        objForm1.mv_sReportFileName = Path.GetFileName(reportname);
                        objForm1.mv_sReportCode = reportcode1;
                        Utility.SetParameterValue(crpt1, "ParentBranchName", globalVariables.ParentBranch_Name);
                        Utility.SetParameterValue(crpt1, "BranchName", globalVariables.Branch_Name);
                        Utility.SetParameterValue(crpt1, "Address", globalVariables.Branch_Address);
                        Utility.SetParameterValue(crpt1, "Phone", globalVariables.Branch_Phone);
                        Utility.SetParameterValue(crpt1, "ReportTitle", tieude);
                        Utility.SetParameterValue(crpt1, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                        Utility.SetParameterValue(crpt1, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());

                        objForm1.crptViewer.ReportSource = crpt1;
                        if (cboPrintPreviewTomtatdieutringoaitru.SelectedValue.ToString() == "1")
                            objForm1.addTrinhKy_OnFormLoad();
                        if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                            PropertyLib._MayInProperties.PreviewInTomtatDieutriNgoaitru))
                        {
                            objForm1.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0, 2);
                            objForm1.ShowDialog();
                            Utility.DefaultNow(this);
                        }
                        else
                        {
                            crpt1.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                            crpt1.PrintToPrinter(1, false, 0, 0);
                        }
                        Utility.FreeMemory(crpt1);

                    }
                    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THAMKHAM_INPHIEUHEN_SAUKHIIN_TOMTATDTNT", "0", true) == "1")
                    {
                        Inphieuhenkham();
                    }

                }

                return true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return false;
            }
        }
        private bool IN_TTAT_DTRI_NGOAITRU()
        {
            try
            {
                string icdName = "";
                string icdCode = "";
                DataSet dsData = _KCB_THAMKHAM.LaythongtinInphieuTtatDtriNgoaitru(objCongkham.IdKham,txtPatient_Code.Text.Trim());
                THU_VIEN_CHUNG.CreateXML(dsData, "thamkham_intomtatdtri.XML");
                string ngayKedon = "";
                string[] arrDate =
                    Utility.sDbnull(
                        dsData.Tables[0].Rows.Count > 0
                            ? dsData.Tables[0].Rows[0]["NGAY_KEDON"]
                            : globalVariables.SysDate.ToString("dd/MM/yyyy"),
                        globalVariables.SysDate.ToString("dd/MM/yyyy")).Split('/');
                ngayKedon = "Ngày " + arrDate[0] + " tháng " + arrDate[1] + " năm " + arrDate[2];
                DataTable vDtData = dsData.Tables[0];
                if (vDtData.Rows.Count <= 0)
                {
                    vDtData = SPs.KcbThamkhamLaythongtinInphieutomtat(objCongkham.IdKham).GetDataSet().Tables[0];
                }
                DataTable subDtData = GetChitietCls();
            //    THU_VIEN_CHUNG.CreateXML(sub_dtData, "sub_report.xml");
                // new DataTable("Temp");
                if (vDtData != null && vDtData.Rows.Count > 0)
                    GetChanDoan(Utility.sDbnull(vDtData.Rows[0]["ICD_CHINH"], ""),
                                Utility.sDbnull(vDtData.Rows[0]["ICD_PHU"], ""), ref icdName, ref icdCode);
                if (vDtData != null)
                {
                    foreach (DataRow dr in vDtData.Rows)
                    {
                        dr["chan_doan"] = Utility.sDbnull(dr["chan_doan"]).Trim() == ""
                            ? icdName
                            : Utility.sDbnull(dr["chan_doan"]) + "; " + icdName;
                        //dr[DmucBenh.Columns.MaBenh] = ICD_Code;
                        dr["ma_icd"] = icdCode;
                    }

                    vDtData.AcceptChanges();
                    vDtData.AcceptChanges();
                    Utility.UpdateLogotoDatatable(ref vDtData);
                    string tieude = "",
                        reportname = "",
                        reportcode = "";
                    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THAMKHAM_CHOPHEP_INTOMTATDIEUTRI", "0", false) == "1")
                    {
                        reportcode = "thamkham_InPhieutomtatdieutringoaitru_A4";
                        ReportDocument crpt = Utility.GetReport("thamkham_InPhieutomtatdieutringoaitru_A4", ref tieude,
                            ref reportname);
                        if (PropertyLib._MayInProperties.CoGiayInTomtatDieutriNgoaitru == Papersize.A5)
                        {
                            reportcode = "thamkham_InPhieutomtatdieutringoaitru_A4";
                            crpt = Utility.GetReport("thamkham_InPhieutomtatdieutringoaitru_A5", ref tieude, ref reportname);
                        }

                        if (crpt == null) return false;
                        var objForm = new frmPrintPreview("PHIẾU TÓM TẮT ĐIỀU TRỊ NGOẠI TRÚ", crpt, true, true);
                        crpt.SetDataSource(vDtData);
                        crpt.Subreports[0].SetDataSource(subDtData);
                        objForm.mv_sReportFileName = Path.GetFileName(reportname);
                        objForm.mv_sReportCode = reportcode;
                        Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                        Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                        Utility.SetParameterValue(crpt,"ReportTitle", tieude);
                        Utility.SetParameterValue(crpt, "NGAY_KEDON", ngayKedon);
                        Utility.SetParameterValue(crpt, "txtTrinhky",
                            Utility.getTrinhky(objForm.mv_sReportFileName, globalVariables.SysDate));
                        objForm.crptViewer.ReportSource = crpt;
                        crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;

                        //In ngay
                        if (cboPrintPreviewTomtatdieutringoaitru.SelectedValue.ToString() == "1")
                            objForm.addTrinhKy_OnFormLoad();
                        if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                            PropertyLib._MayInProperties.PreviewInTomtatDieutriNgoaitru))
                        {
                            objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0, 1);
                            objForm.ShowDialog();
                        }
                        else
                        {
                            crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                            int soluongin = 1;
                            string userPrintNumberFile = Application.StartupPath + @"\UserPrintNumber\" +
                                                         globalVariables.UserName + "_" + objForm.mv_sReportFileName +
                                                         ".txt";
                            soluongin = File.Exists(userPrintNumberFile)
                                ? Utility.Int32Dbnull(File.ReadAllText(userPrintNumberFile))
                                : 1;
                            crpt.PrintToPrinter(soluongin, false, 0, 0);
                        }
                        Utility.FreeMemory(crpt);
                    }
                    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THAMKHAM_CHOPHEP_INDONTHUOC", "0", false) == "1")
                    {
                        List<int> lstKho = dsData.Tables[0].AsEnumerable().Select(c => c.Field<int>("id_kho")).Distinct().ToList();
                        List<byte> listtutuc = dsData.Tables[0].AsEnumerable().Select(c => c.Field<byte>("tu_tuc")).Distinct().ToList();
                        foreach (int s in lstKho)
                        {
                            foreach (byte i in listtutuc)
                            {
                                DataTable dt = vDtData.Clone();
                                dt =
                                    dsData.Tables[0].Select("id_kho = " + s + " and tu_tuc = " + i + "", "stt_in").
                                        CopyToDataTable();
                                //foreach (DataRow dr in dt.Rows)
                                //{
                                //    dr["chan_doan"] = Utility.sDbnull(dr["chan_doan"]).Trim() == ""
                                //        ? icdName
                                //        : Utility.sDbnull(dr["chan_doan"]) + ";" + icdName;
                                //    //dr[DmucBenh.Columns.MaBenh] = ICD_Code;
                                //    dr["ma_icd"] = icdCode;
                                //}

                                dt.AcceptChanges();
                                Utility.UpdateLogotoDatatable(ref dt);
                                Utility.CreateBarcodeData(ref dt, dt.Rows[0]["MA_LKHAM"].ToString());
                                string reportcode1 = "";
                                //  reportcode1 = "thamkham_Inphieulinhthuocngoaitru_A4";
                                reportcode1 = i == 1
                                    ? "thamkham_InDonthuocA5_tutuc"
                                    : "thamkham_Inphieulinhthuocngoaitru_A4";
                                ReportDocument crpt1 = Utility.GetReport(reportcode1, ref tieude,
                                    ref reportname);

                                if (PropertyLib._MayInProperties.CoGiayInTomtatDieutriNgoaitru == Papersize.A5)
                                {
                                    reportcode1 = i == 1
                                        ? "thamkham_InDonthuocA5_tutuc"
                                        : "thamkham_Inphieulinhthuocngoaitru_A5";
                                    crpt1 = Utility.GetReport(reportcode1, ref tieude, ref reportname);
                                }

                                if (crpt1 == null) return false;

                                var objForm1 = new frmPrintPreview("PHIẾU LĨNH THUỐC NGOẠI TRÚ", crpt1, true, true);
                                crpt1.SetDataSource(dt);
                                crpt1.Subreports[0].SetDataSource(subDtData);
                                objForm1.mv_sReportFileName = Path.GetFileName(reportname);
                                objForm1.mv_sReportCode = reportcode1;
                                crpt1.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
                                crpt1.SetParameterValue("ReportTitle", tieude);
                                crpt1.SetParameterValue("BranchName", globalVariables.Branch_Name);
                                crpt1.SetParameterValue("NGAY_KEDON", ngayKedon);
                                Utility.SetParameterValue(crpt1, "txtTrinhky",
                                    Utility.getTrinhky(objForm1.mv_sReportFileName,
                                        globalVariables.SysDate));
                                objForm1.crptViewer.ReportSource = crpt1;
                                if (cboPrintPreviewTomtatdieutringoaitru.SelectedValue.ToString() == "1")
                                    objForm1.addTrinhKy_OnFormLoad();
                                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                                    PropertyLib._MayInProperties.PreviewInTomtatDieutriNgoaitru))
                                {
                                    objForm1.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0, 2);
                                    objForm1.ShowDialog();
                                    Utility.DefaultNow(this);
                                }
                                else
                                {
                                    crpt1.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                                    crpt1.PrintToPrinter(1, false, 0, 0);
                                }
                                Utility.FreeMemory(crpt1);
                            }
                        }
                    }
                    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THAMKHAM_CHOPHEP_INDIENBIENBENH", "0", false) == "1")
                    {
                        reportcode = "thamkham_InPhieudienbienbenh_A4";
                        ReportDocument crpt2 = Utility.GetReport("thamkham_InPhieudienbienbenh_A4", ref tieude,
                            ref reportname);
                        if (PropertyLib._MayInProperties.CoGiayInTomtatDieutriNgoaitru == Papersize.A5)
                        {
                            reportcode = "thamkham_InPhieudienbienbenh_A4";
                            crpt2 = Utility.GetReport("thamkham_InPhieudienbienbenh_A4", ref tieude, ref reportname);
                        }

                        if (crpt2 == null) return false;
                        var objForm2 = new frmPrintPreview("TỜ DIỄN BIẾN BỆNH NGOẠI TRÚ", crpt2, true, true);
                        crpt2.SetDataSource(vDtData);
                        crpt2.Subreports[0].SetDataSource(subDtData);
                        crpt2.Subreports[1].SetDataSource(vDtData);
                        objForm2.mv_sReportFileName = Path.GetFileName(reportname);
                        objForm2.mv_sReportCode = reportcode;
                        Utility.SetParameterValue(crpt2, "ParentBranchName", globalVariables.ParentBranch_Name);
                        Utility.SetParameterValue(crpt2, "BranchName", globalVariables.Branch_Name);
                        Utility.SetParameterValue(crpt2, "ReportTitle", tieude);
                        Utility.SetParameterValue(crpt2, "NGAY_KEDON", ngayKedon);
                        Utility.SetParameterValue(crpt2, "txtTrinhky",
                            Utility.getTrinhky(objForm2.mv_sReportFileName, globalVariables.SysDate));
                        objForm2.crptViewer.ReportSource = crpt2;
                        crpt2.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;

                        //In ngay
                        if (cboPrintPreviewTomtatdieutringoaitru.SelectedValue.ToString() == "1")
                            objForm2.addTrinhKy_OnFormLoad();
                        if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                            PropertyLib._MayInProperties.PreviewInTomtatDieutriNgoaitru))
                        {
                            objForm2.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0, 1);
                            objForm2.ShowDialog();
                        }
                        else
                        {
                            crpt2.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                            int soluongin = 1;
                            string userPrintNumberFile = Application.StartupPath + @"\UserPrintNumber\" +
                                                         globalVariables.UserName + "_" + objForm2.mv_sReportFileName +
                                                         ".txt";
                            soluongin = File.Exists(userPrintNumberFile)
                                ? Utility.Int32Dbnull(File.ReadAllText(userPrintNumberFile))
                                : 1;
                            crpt2.PrintToPrinter(soluongin, false, 0, 0);
                        }
                        Utility.FreeMemory(crpt2);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return false;
            }
        }
        void Nhapvien()
        {
            try
            {

                if (objLuotkham == null || objCongkham == null || !Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn chưa chọn người bệnh để thực hiện thăm khám. Đề nghị kiểm tra lại", "Thông Báo");
                    return;
                }
                objLuotkham = Utility.getKcbLuotkham(objLuotkham);
                if (objLuotkham.TthaiChuyendi > 0)
                {
                    Utility.ShowMsg("Bệnh nhân đã được chuyển viện nên bạn không thể thực hiện thao tác nhập viện. Đề nghị hủy chuyển viện trước");
                    return;
                }
                //if (objLuotkham.TrangthaiNoitru > 1)
                //{
                //    Utility.ShowMsg(
                //        "Bệnh nhân đã được điều trị nội trú nên bạn chỉ có thể xem và không được phép sửa các thông tin thăm khám");
                //    return;
                //}
                //Kiểm tra xem có đơn thuốc ngoại trú hay không
                string errMsg = "";
                StoredProcedure v_sp = SPs.KcbKiemtradieukiennhapvien(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, errMsg);
                DataSet dsCheck = v_sp.GetDataSet();
                 //errMsg = Utility.sDbnull(v_sp.OutputValues[0]);
                 //if (Utility.DoTrim(errMsg).Length > 0)
                 //{
                 //    Utility.ShowMsg(errMsg);
                 //    return;
                 //}
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THAMKHAM_KEDONTHUOCNGOAITRU_KHONGCHONHAPVIEN", "0", true) == "1")
                {
                    if (dsCheck.Tables[0].Rows.Count > 0)
                    {
                        Utility.ShowMsg("Hệ thống phát hiện bệnh nhân đã được kê đơn thuốc ngoại trú. Cần thực hiện hủy tất cả các đơn thuốc ngoại trú trước khi thực hiện nhập viện. Mời bạn kiểm tra lại");
                        return;
                    }
                }
                if (dsCheck.Tables[1].Rows.Count > 0)
                {
                    Utility.ShowMsg("Hệ thống phát hiện bệnh nhân đã được đăng kí khám tại phòng khám nhưng chưa kết thúc khám. Cần thực hiện kết thúc khám trước khi thực hiện nhập viện. Mời bạn kiểm tra lại");
                    return;
                }
                var frm = new frm_Nhapvien();
                frm.CallfromParent = true;
                frm.id_kham = Utility.Int32Dbnull(txtExam_ID.Text, -1);
                frm.id_bskham =Utility.Int16Dbnull( objCongkham.IdBacsikham,-1);
                frm.objLuotkham = objLuotkham;
                frm.ucThongtinnguoibenh1.txtMaluotkham.Text = objLuotkham.MaLuotkham;
                frm.ShowDialog();
                if (frm.b_Cancel)
                {
                    objLuotkham.IdKhoanoitru = Utility.Int16Dbnull(frm.objLuotkham.IdKhoanoitru);
                    objLuotkham.SoBenhAn = Utility.sDbnull(frm.objLuotkham.SoBenhAn);
                    objLuotkham.TrangthaiNoitru = frm.objLuotkham.TrangthaiNoitru;
                    objLuotkham.NgayNhapvien = frm.objLuotkham.NgayNhapvien;
                    objLuotkham.MotaNhapvien = frm.objLuotkham.MotaNhapvien;
                    DataRow[] arrDr = m_dtDanhsachbenhnhanthamkham.Select("id_kham=" + txtReg_ID.Text);
                    if (arrDr.Length > 0)
                        arrDr[0]["trangthai_noitru"] = objLuotkham.TrangthaiNoitru;
                    cmdNhapVien.Enabled = true;
                    cmdHuyNhapVien.Enabled = true;
                    //cmdChuyenPhong.Visible = objLuotkham.TrangthaiNoitru == 0;
                    //Tạm thời comment bỏ 2 dòng dưới để luôn hiển thị 2 nút này(190803)
                    //cmdInTTDieuTri.Visible = objLuotkham.TrangthaiNoitru == 0;
                    //cmdInphieuhen.Visible = objLuotkham.TrangthaiNoitru == 0;
                    cmdNhapVien.Tag = objLuotkham.TrangthaiNoitru == 0 ? "0" : "1";
                    cmdNhapVien.Text = objLuotkham.TrangthaiNoitru == 0 ? "Nhập viện" : "Cập nhật";
                    VisibleLockButton();
                    if (!cmdUnlock.Enabled)
                        toolTip1.SetToolTip(cmdUnlock,
                                            "Bạn không có quyền mở khóa Bệnh nhân này. Đề nghị liên hệ Quản trị hệ thống");
                    else
                        toolTip1.SetToolTip(cmdUnlock,
                                            "Nhấn vào đây để mở khóa cho bệnh nhân đang chọn(Phím tắt Ctrl+U). Điều kiện là chỉ mở khóa đối với đối tượng Dịch vụ. Muốn mở khóa đối tượng BHYT thì cần liên lạc với bộ phận thanh toán hủy in phôi BHYT");
                }
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin) Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                ModifyCommmands();
            }
        }
        private void cmdNhapVien_Click(object sender, EventArgs e)
        {
            Nhapvien();
        }


        private bool IsValidHuyNhapVien()
        {
            objLuotkham = Utility.getKcbLuotkham(objLuotkham);
            if (objLuotkham.TrangthaiNoitru<=0)
            {
                Utility.ShowMsg("Người bệnh đang ở trạng thái ngoại trú nên bạn không thể hủy nhập viện. Vui lòng kiểm tra lại");
                // cmdCancelNhapVien.Focus();
                return false;
            }
            DataTable dtTU = new Select().From(NoitruTamung.Schema)
                .Where(NoitruTamung.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .And(NoitruTamung.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(NoitruTamung.Columns.Noitru).IsEqualTo(1)
                .ExecuteDataSet().Tables[0];
            if (dtTU.Rows.Count > 0)
            {
                Utility.SetMsg(lblMsg, "Người bệnh đã phát sinh tiền tạm ứng nội trú. Đề nghị hủy tạm ứng trước khi hủy nhập viện", true);
                Utility.ShowMsg(lblMsg.Text);
                return false;
            }
            var objNoitruPhanbuonggiuong = new Select().From(NoitruPhanbuonggiuong.Schema)
                .Where(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(1)
                .And(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteAsCollection
                <NoitruPhanbuonggiuongCollection>();
            if (objNoitruPhanbuonggiuong != null && objNoitruPhanbuonggiuong.Count > 1)
            {
                Utility.SetMsg(lblMsg,
                               "Bệnh nhân đã chuyển khoa hoặc phân buồng giường, Bạn không thể hủy thông tin nhập viện",
                               true);
                return false;
            }


            if (objNoitruPhanbuonggiuong != null && objNoitruPhanbuonggiuong.Count == 1 &&
                Utility.Int32Dbnull(objNoitruPhanbuonggiuong[0].IdBuong, -1) > 0)
            {
                Utility.ShowMsg("Bệnh nhân đã phân buồng giường,Bạn không thể xóa thông tin ", "Thông báo",
                                MessageBoxIcon.Warning);
                return false;
            }

            SqlQuery sqlQuery2 = new Select().From(NoitruPhieudieutri.Schema)
                .Where(NoitruPhieudieutri.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(m_strMaLuotkham, ""));
            if (sqlQuery2.GetRecordCount() > 0)
            {
                Utility.SetMsg(lblMsg,
                               "Bệnh nhân đã có phiếu điều trị, Bạn không thể xóa hoặc hủy nhập viện được,yêu cầu xem lại",
                               true);
                return false;
            }
            var noitruTamung = new Select().From(NoitruTamung.Schema)
                .Where(NoitruTamung.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .And(NoitruTamung.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).ExecuteSingle<NoitruTamung>();
            if (noitruTamung != null)
            {
                Utility.ShowMsg("Bệnh nhân này đã đóng tiền tạm ứng , Bạn không thể hủy nhập viện", "Thông báo",
                                MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        void Huynhapvien()
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                if (!IsValidHuyNhapVien()) return;
                if (
                    Utility.AcceptQuestion(
                        "Bạn có muốn hủy thông tin nhập viện của bệnh nhân này không,Bệnh nhân sẽ quay lại trạng thái ngoại trú",
                        "Thông báo", true))
                {
                    if (new noitru_nhapvien().Huynhapvien(objLuotkham) == ActionResult.Success)
                    {
                        Utility.Log(Name, globalVariables.UserName, string.Format("Bệnh nhân có mã lần khám {0} và mã bệnh nhân {1} được hủy nhập viện bởi {2} ", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, globalVariables.UserName), newaction.CancelData, this.GetType().Assembly.ManifestModule.Name);
                        DataRow[] arrDr = m_dtDanhsachbenhnhanthamkham.Select("id_kham=" + txtReg_ID.Text);
                        if (arrDr.Length > 0)
                            arrDr[0]["trangthai_noitru"] = 0;
                        objLuotkham.TrangthaiNoitru = 0;
                        objLuotkham.NgayNhapvien = null;
                        objLuotkham.MotaNhapvien = "";
                        cmdNhapVien.Enabled = true;
                        cmdHuyNhapVien.Enabled = false;
                        objLuotkham.SoBenhAn = string.Empty;
                        objLuotkham.IdKhoanoitru = -1;
                        //cmdChuyenPhong.Visible = objLuotkham.TrangthaiNoitru == 0;
                        //Tạm thời comment bỏ 2 dòng dưới để luôn hiển thị 2 nút này(190803)
                        //cmdInTTDieuTri.Visible = objLuotkham.TrangthaiNoitru == 0;
                        //cmdInphieuhen.Visible = objLuotkham.TrangthaiNoitru == 0;
                        var objStaff =
                            new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.UserNameColumn).IsEqualTo(
                                Utility.sDbnull(objLuotkham.NguoiKetthuc, "")).ExecuteSingle<DmucNhanvien>();
                        string TenNhanvien = objLuotkham.NguoiKetthuc;
                        if (objStaff != null)
                            TenNhanvien = objStaff.TenNhanvien;
                        VisibleLockButton();
                        if (!cmdUnlock.Enabled)
                            toolTip1.SetToolTip(cmdUnlock,
                                                "Bạn không có quyền mở khóa Bệnh nhân này. Đề nghị liên hệ " +
                                                TenNhanvien + "(" + objLuotkham.NguoiKetthuc +
                                                " - Là người khóa BN này) để được họ mở khóa. Hoặc liên hệ Quản trị hệ thống");
                        else
                            toolTip1.SetToolTip(cmdUnlock,
                                                "Nhấn vào đây để mở khóa cho bệnh nhân đang chọn(Phím tắt Ctrl+U). Điều kiện là chỉ mở khóa đối với đối tượng Dịch vụ. Muốn mở khóa đối tượng BHYT thì cần liên lạc với bộ phận thanh toán hủy in phôi BHYT");

                        cmdNhapVien.Tag = objLuotkham.TrangthaiNoitru == 0 ? "0" : "1";
                        cmdNhapVien.Text = objLuotkham.TrangthaiNoitru == 0 ? "Nhập viện" : "Cập nhật";
                        Utility.SetMsg(lblMsg, "Bệnh nhân đã quay lại trạng thái ngoại trú", false);
                    }
                }
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin) Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                ModifyCommmands();
            }
        }
        void VisibleLockButton()
        {
            cmdUnlock.Visible = objLuotkham.TrangthaiNoitru == 0 && Utility.Byte2Bool(objCongkham.TrangThai); //objLuotkham.Locked.ToString() == "1";
            cmdUnlock.Enabled = cmdUnlock.Visible &&
                                (Utility.Coquyen("quyen_mokhoa_tatca") || objCongkham.IdBacsikham == globalVariables.gv_intIDNhanvien);
                                // objLuotkham.NguoiKetthuc == globalVariables.UserName);
        }
        private void cmdHuyNhapVien_Click(object sender, EventArgs e)
        {
            Huynhapvien();
        }

        private void txtMaBenhphu_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (hasMorethanOne)
                    {
                        DSACH_ICD(txtMaBenhphu, DmucChung.Columns.Ma, 1);
                        txtMaBenhphu.SelectAll();
                    }
                    else
                    {
                        cmdAddMaBenhPhu_Click(cmdAddMaBenhPhu, new EventArgs());
                        txtMaBenhphu.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }


        private void radChuaKham_CheckedChanged(object sender, EventArgs e)
        {
            cmdSearch_Click(cmdSearch, e);
            //Tạm thời rem lại ngày 16/10/2019
            //try
            //{
            //    int status = radChuaKham.Checked ? 0 : 1;
            //    m_dtDanhsachbenhnhanthamkham.DefaultView.RowFilter = "1=1";
            //    m_dtDanhsachbenhnhanthamkham.DefaultView.RowFilter = "trang_thai=" + status;
            //}
            //catch (Exception ex)
            //{
            //    Utility.CatchException(ex);
            //}
        }
        private void radDaKham_CheckedChanged(object sender, EventArgs e)
        {
            cmdSearch_Click(cmdSearch, e);
        }

        private void mnuDelDrug_Click(object sender, EventArgs e)
        {
            if (!IsValidDeleteSelectedDrug()) return;
            PerformActionDeleteSelectedDrug();
            ModifyCommmands();
        }

        private void mnuDeleteCLS_Click(object sender, EventArgs e)
        {
            if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa các dịch vụ CLS đang chọn?", "Xác nhận", true)) return;
            if (!IsValidCheckedAssignDetails()) return;
            PerforActionDeleteSelectedCls();
            ModifyCommmands();
        }

        private void cboLaserPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveDefaultPrinter();
        }

        private void LoadLaserPrinters()
        {
            if (string.IsNullOrEmpty(PropertyLib._MayInProperties.TenMayInBienlai))
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
                catch (Exception ex)
                {
                    Utility.CatchException(ex);
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
                PropertyLib.SavePropertyV1( PropertyLib._MayInProperties);
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
            catch (Exception ex)
            {
                Utility.CatchException(ex);
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
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            var frm = new frm_Properties(PropertyLib._HISQMSProperties,globalVariables.m_strPropertiesFolderQMS);
            frm.ShowDialog();
            CauHinhQMS();
        }

        private void txtKhoaNoiTru_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (!string.IsNullOrEmpty(txtKhoaNoiTru.Text))
            //    {
            //        DataRow query = (from khoa in m_dtKhoaNoiTru.AsEnumerable().Cast<DataRow>()
            //                         let y = Utility.sDbnull(khoa[DmucKhoaphong.Columns.TenKhoaphong])
            //                         let z = Utility.sDbnull(khoa[DmucKhoaphong.Columns.IdKhoaphong])
            //                         where
            //                             Utility.Int32Dbnull(khoa[DmucKhoaphong.Columns.MaKhoaphong]) ==
            //                             Utility.Int32Dbnull(txtKhoaNoiTru.Text)
            //                         select khoa).FirstOrDefault();
            //        if (query != null)
            //        {
            //            txtKhoaNoiTru.Text = Utility.sDbnull(query[DmucKhoaphong.Columns.TenKhoaphong]);
            //            txtIdKhoaNoiTru.Text = Utility.sDbnull(query[DmucKhoaphong.Columns.IdKhoaphong]);
            //            cmdSave.Focus();
            //        }
            //        else
            //        {
            //            TimKiemKhoaNoiTru();
            //        }
            //    }
            //    else
            //    {
            //        TimKiemKhoaNoiTru();
            //    }
            //}
            //if (e.KeyCode == Keys.F3)
            //{
            //    TimKiemKhoaNoiTru();
            //}
        }

        private void TimKiemKhoaNoiTru()
        {
        }

        private void txtDeparmentID_TextChanged(object sender, EventArgs e)
        {
            //    EnumerableRowCollection<string> query = from khoa in m_dtKhoaNoiTru.AsEnumerable()
            //                                            let y = Utility.sDbnull(khoa[DmucKhoaphong.Columns.TenKhoaphong])
            //                                            where
            //                                                Utility.Int32Dbnull(khoa[DmucKhoaphong.Columns.IdKhoaphong]) ==
            //                                                Utility.Int32Dbnull(txtIdKhoaNoiTru.Text)
            //                                            select y;
            //    if (query.Any())
            //    {
            //        txtKhoaNoiTru.Text = Utility.sDbnull(query.FirstOrDefault());
            //    }
            //    else
            //    {
            //        txtKhoaNoiTru.Text = string.Empty;
            //    }
        }

        private void nmrSongayDT_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            //{
            //    dtNgayNhapVien.Focus();
            //}
        }

        private void dtNgayNhapVien_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            //{
            //    txtKhoaNoiTru.Focus();
            //    txtKhoaNoiTru.SelectAll();

            //}
        }

        private void cmdTimKiemKhoaNoiTru_Click(object sender, EventArgs e)
        {
            TimKiemKhoaNoiTru();
        }
        void Inphieuhenkham()
        {
            try
            {
                if (Utility.DecimaltoDbnull(txtSongaydieutri.Text, 0) > 0)
                {
                    if (objLuotkham == null || !Utility.isValidGrid(grdList))
                    {
                        Utility.ShowMsg("Bạn chưa chọn người bệnh để thực hiện thăm khám. Đề nghị kiểm tra lại", "Thông Báo");
                        return;
                    }
                    DataTable dtphienhen =
                        SPs.KcbThamkhamInphieuhenBenhnhan(m_strMaLuotkham, Utility.Int64Dbnull(txtPatient_ID.Text, -1)).
                            GetDataSet().Tables[0];
                    //THU_VIEN_CHUNG.CreateXML(dtphienhen, "thamkham_inphieuhen_benhnhan.xml");
                    KcbInphieu.INPHIEU_HEN(dtphienhen, "PHIẾU HẸN KHÁM");
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        private void cmdInphieuhen_Click(object sender, EventArgs e)
        {
            Inphieuhenkham();
        }

        private void txtNhietDo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //string original = (sender as Janus.Windows.UI.).Text;
            //if (!char.IsDigit(e.KeyChar))
            //{
            //    e.Handled = true;
            //}
            //if (e.KeyChar == '.')
            //{
            //    if (original.Contains('.'))
            //        e.Handled = true;
            //    else if (!(original.Contains('.')))
            //        e.Handled = false;

            //}
            //else if (char.IsDigit(e.KeyChar) || e.KeyChar == '\b')
            //{
            //    e.Handled = false;
            //}
        }
        void PushDataIntoRIS()
        {
            //DataTable dtData = new DataTable("dt_Benhnhan");
            //dtData.Columns.AddRange(new DataColumn[]{
            //    new DataColumn("Id_benhnhan",typeof(Int64)),
            //    new DataColumn("Ma_luotkham",typeof(string)),
            //    new DataColumn("Ten_benhnhan",typeof(string)),
            //    new DataColumn("Tuoi",typeof(byte)),
            //    new DataColumn("Gioi_tinh",typeof(string)),
            //    new DataColumn("Dia_chi",typeof(string)),
            //    new DataColumn("Trang_thai",typeof(byte)),
            //    new DataColumn("Ma_doituong",typeof(string)),
            //    new DataColumn("Mathe_bhyt",typeof(string)),
            //    new DataColumn("Ma_phieuchup",typeof(string)),
            //    new DataColumn("Kieu_chup",typeof(string)),
            //    new DataColumn("Ten_dichvu",typeof(string))
            //});
            //foreach (DataRow _dr in m_dtAssignDetail.Rows)
            //{
            //    DataRow dr = dtData.NewRow();
            //    dr["Id_benhnhan"] = objLuotkham.IdBenhnhan;
            //    dr["Ma_luotkham"] = objLuotkham.MaLuotkham;
            //    dr["Ten_benhnhan"] = objBenhnhan.TenBenhnhan;
            //    dr["Tuoi"] = objLuotkham.Tuoi;
            //    dr["Gioi_tinh"] = objBenhnhan.IdGioitinh == 0 ? "M" : "F";
            //    dr["Dia_chi"] = objLuotkham.DiaChi;
            //    dr["Trang_thai"] = objLuotkham.TrangthaiNoitru;
            //    dr["Ma_doituong"] = objLuotkham.MaDoituongKcb;
            //    dr["Mathe_bhyt"] = objLuotkham.MatheBhyt;
            //    dr["Ma_phieuchup"] =Utility.sDbnull( _dr["ma_chidinh"],"");
            //    DmucDichvuclsChitiet objDmuc = new Select().From(DmucDichvuclsChitiet.Schema).Where(DmucDichvuclsChitiet.Columns.IdChitietdichvu).IsEqualTo(_dr["Id_Chitietdichvu"]).ExecuteSingle<DmucDichvuclsChitiet>();
            //    if (objDmuc != null)
            //    {
            //        dr["Kieu_chup"] = Utility.sDbnull(_dr["ma_loaidichvu"], "");
            //        dr["Ten_dichvu"] = objDmuc.TenChitietdichvu;
            //        dtData.Rows.Add(dr);
            //    }
            //}
            //string url="http://localhost:4000/HRC.asmx";
            //HRW.HRC _ser = new HRW.HRC();
            //_ser.Url = url;
            //string sMsg = "";
            //_ser.PushDataIntoRIS(dtData, ref sMsg);
            //Utility.ShowMsg(sMsg,"Thông báo");
        }
        private void cmdConfirm_Click(object sender, EventArgs e)
        {
           
        }

        private void txtNhipTim_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMach.Text))
            {
                txtMach.Text = txtNhipTim.Text;
            }
        }

        private void txtMach_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNhipTim.Text))
            {
                txtNhipTim.Text = txtMach.Text;
            }
        }

        private void txtSongaydieutri_LostFocus(object sender, EventArgs e)
        {
            dtpNgayHen.Value = dtpNgaydangky.Value.AddDays(Utility.Int32Dbnull(txtSongaydieutri.Text, 0));
            txtSoNgayHen.Text = txtSongaydieutri.Text;
        }

        private void txtSoNgayHen_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSoNgayHen.Text))
            {
                txtSongaydieutri.Text = txtSoNgayHen.Text;
                dtpNgayHen.Value = dtpNgaydangky.Value.AddDays(Utility.Int32Dbnull(txtSongaydieutri.Text, 0));
            }
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

        private void editBox1_TextChanged(object sender, EventArgs e)
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
            try
            {
                if (e.Column.Key == "ghi_chu")
                {
                    string GhiChu = Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.GhiChu), "");

                    int ra = new Update(KcbChidinhclsChitiet.Schema)
                          .Set(KcbChidinhclsChitiet.Columns.GhiChu).EqualTo(GhiChu)
                          .Set(KcbChidinhclsChitiet.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                          .Set(KcbChidinhclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                          .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(
                              Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChitietchidinh))).Execute();
                    
                }
            }
            catch (Exception exception)
            {
            }
        }

        /// <summary>
        /// hàm thực hiện việc xóa thông tin chỉ định cận lâm sàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDelteAssign_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa các dịch vụ CLS đang chọn?", "Xác nhận", true)) return;
                if (!IsValidCheckedAssignDetails()) return;
                string maChidinh = Utility.sDbnull(Utility.getCellValuefromGridEXRow(RowCLS, KcbChidinhcl.Columns.MaChidinh));
                PerforActionDeleteAssign();
                Utility.Log(Name, globalVariables.UserName,
                                   string.Format(
                                       "Xóa phiếu chỉ định  {0} của bệnh nhân có mã lần khám: {1} và mã bệnh nhân là: {2}",
                                       maChidinh
                                       , objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                ModifyCommmands();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        private bool IsValidCheckedAssignDetails()
        {
            bool bCancel = false;
            if (RowCLS==null )
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện xóa chỉ định CLS", "Thông báo",
                                MessageBoxIcon.Warning);
                grdAssignDetail.Focus();
                return false;
            }
            if (grdAssignDetail.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải thực hiện chọn một bản ghi thực hiện xóa thông tin dịch vụ cận lâm sàng",
                                "Thông báo", MessageBoxIcon.Warning);
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
            var q = from p in grdAssignDetail.GetCheckedRows() where Utility.Int64Dbnull(p.Cells["id_dangky"].Value) > 0 && Utility.Int64Dbnull(p.Cells["id_goi"].Value) > 0 select p;
            if(q.Any())
            {
                Utility.ShowMsg("Dịch vụ bạn chọn xóa thuộc gói. Vui lòng nháy chuột phải chọn xóa gói");
                return false;
            }    
            foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                if (Utility.Coquyen("quyen_suaphieuchidinhcls") ||
                    Utility.sDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "") ==
                    globalVariables.UserName)
                {
                }
                else
                {
                    Utility.ShowMsg(
                        "Trong các chỉ định bạn chọn xóa, có một số chỉ định được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các chỉ định do chính bạn kê để thực hiện xóa");
                    return false;
                }
            }
            KcbLuotkham item = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text, 0), m_strMaLuotkham);
            if (item == null)
            {
                Utility.ShowMsg("Bạn phải chọn Bệnh nhân hoặc bệnh nhân không tồn tại!", "Thông báo",
                                MessageBoxIcon.Warning);
                txtPatient_Code.Focus();
                return false;
            }

           
            //foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            //{
            //    int idChidinhchitiet =
            //        Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
            //                            -1);
            //    //int i = m_dtAssignDetail.Select(KcbChidinhclsChitiet.Columns.IdChitietchidinh + " = " + idChidinhchitiet +
            //    //                            " And " + KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan + " = 1").Count();
            //    KcbChidinhclsChitiet objKcbChidinhclsChitiet = KcbChidinhclsChitiet.FetchByID(idChidinhchitiet);
            //    if (objKcbChidinhclsChitiet.TrangthaiThanhtoan > 0)
            //    {
            //        bCancel = true;
            //        break;
            //    }
            //}
            //if (bCancel)
            //{
            //    Utility.ShowMsg("Chỉ định bạn chọn đã được thanh toán nên bạn không thể xóa. Đề nghị kiểm tra lại","Thông Báo",MessageBoxIcon.Warning);
            //    return false;
            //}
            //bCancel = false;
            foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int idChidinhchitiet =
                    Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                        -1);
                int idChitietDichvu =
                 Utility.Int32Dbnull(
                     grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietdichvu].Value,
                     -1);
                int id_dangky = Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdDangky].Value, -1);
                int id_goi = Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdGoi].Value, -1);
                //int i = m_dtAssignDetail.Select(KcbChidinhclsChitiet.Columns.IdChitietchidinh + " = " + idChidinhchitiet +
                //                            " And " + KcbChidinhclsChitiet.Columns.TrangThai + " >= 1" + 
                //                            " OR " + KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan +" >=1").Count();
                if (id_dangky > 0 && id_goi > 0)//Các dịch vụ ngoài gói thì kiểm tra trạng thái thanh toán
                {
                }
                else
                {
                    SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                        .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(idChidinhchitiet)
                        .AndExpression(KcbChidinhclsChitiet.Columns.TrangThai).IsGreaterThanOrEqualTo(1).Or(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1).CloseExpression();
                    if (sqlQuery.GetRecordCount() > 0)
                    {
                        bCancel = true;
                        break;
                    }
                }
            }
            if (bCancel)
            {
                Utility.ShowMsg(
                    "Trong các dịch vụ bạn chọn xóa có dịch vụ đã nhập kết quả hoặc thanh toán nên không thể xóa. Đề nghị kiểm tra lại");
                return false;
            }
            if (!globalVariables.isSuperAdmin)
            {
                if (item != null && Utility.Int32Dbnull(item.TrangthaiNoitru, -1) >= 1)
                {
                    Utility.ShowMsg(
                        "Bệnh nhân đã được điều trị nội trú. Bạn chỉ có thể xem thông tin và không được phép làm các công việc liên quan đến ngoại trú",
                        "Thông báo");
                    cmdKetthuckham.Focus();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// hàm thực hiện viễ xóa thông tin chỉ định
        /// </summary>
        private void PerforActionDeleteAssign()
        {
            string lstvalues = "";
            if (grdAssignDetail.GetCheckedRows().Count() <= 0 && RowCLS != null)
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
                    return;
                }
               
            }
            foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int idChidinhchitiet =
                    Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                        -1);
                int idChidinh = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value, -1);
                _KCB_CHIDINH_CANLAMSANG.XoaChiDinhCLSChitiet(idChidinhchitiet, idChidinh);
                lstvalues += idChidinhchitiet + ",";
            }
            DataRow[] rows;
            if (lstvalues.Length > 0) lstvalues = lstvalues.Substring(0, lstvalues.Length - 1);
            if (Utility.DoTrim(lstvalues).Length <= 0) return;
            rows = m_dtAssignDetail.Select(KcbChidinhclsChitiet.Columns.IdChitietchidinh + " IN (" + lstvalues + ")");
            string _deleteitems = string.Join(",", (from p in rows.AsEnumerable()
                                             select Utility.sDbnull(p["ten_chitietdichvu"])).ToList<string>());
                // UserName is Column Name
            Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa chỉ định của bệnh nhân ID={0}, PID={1}, Tên={2}, DS chỉ định xóa={3} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan, _deleteitems), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
            foreach (DataRow r in rows)
                r.Delete();
            m_dtAssignDetail.AcceptChanges();
            ResetNhominCLS();
        }

        private void PerforActionDeleteSelectedCls()
        {
            try
            {
                int idChidinhchitiet =
                    Utility.Int32Dbnull(
                        grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                        -1);
                string  tendichvuxoa =
                    Utility.sDbnull(
                        grdAssignDetail.CurrentRow.Cells["ten_chitietdichvu"].Value,
                        -1);
                int idChidinh =
                    Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value,
                                        -1);
                _KCB_CHIDINH_CANLAMSANG.XoaChiDinhCLSChitiet(idChidinhchitiet, idChidinh);
                Utility.Log(Name, globalVariables.UserName,
                                   string.Format(
                                       "Xóa chỉ định {0} của bệnh nhân {1} có mã lần khám: {2} và Id bệnh nhân là: {3}",
                                       tendichvuxoa, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);

                m_dtAssignDetail.AsEnumerable()
                    .Where(r => r.Field<Int64>(KcbChidinhclsChitiet.Columns.IdChitietchidinh) == idChidinhchitiet).
                    ToList()
                    .ForEach(row => row.Delete());
                m_dtAssignDetail.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Bạn nên xóa CLS bằng cách chọn và nhấn nút Xóa CLS-->" + ex.Message);
            }
        }

        private bool IsValidUpdateChidinh()
        {
            if (grdAssignDetail.RowCount <= 0) return false;
            if (RowCLS == null)
            {
                Utility.ShowMsg("Vui lòng chọn 1 dòng chỉ định trong đơn để thực hiện cập nhật phiếu chỉ định CLS");
                return false;
            }
            int idChidinh = Utility.Int32Dbnull(Utility.getCellValuefromGridEXRow(RowCLS, KcbChidinhclsChitiet.Columns.IdChidinh), -1);

           
            SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                .Where(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(idChidinh).And(
                    KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsGreaterThanOrEqualTo(1)
                //.Or(KcbChidinhclsChitiet.Columns.TrangThai).IsGreaterThanOrEqualTo(1)
                .And(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(idChidinh);

            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Phiếu này đã thanh toán hoặc đã chuyển cận lâm sàng nên bạn không thể sửa phiếu",
                                "Thông báo");
                cmdInsertAssign.Focus();
                return false;
            }

            SqlQuery sqlQueryKq = new Select().From(KcbChidinhclsChitiet.Schema)
               .Where(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(idChidinh)
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
                if (!IsValidUpdateChidinh()) return;
                if (grdAssignDetail.RowCount <= 0) return ;
                
                int idChidinh = Utility.Int32Dbnull(Utility.getCellValuefromGridEXRow(RowCLS, KcbChidinhclsChitiet.Columns.IdChidinh), -1);


                if (Utility.Coquyen("quyen_suaphieuchidinhcls") ||Utility.sDbnull(Utility.getCellValuefromGridEXRow(RowCLS,KcbChidinhclsChitiet.Columns.NguoiTao)) == globalVariables.UserName)
                {
                    var frm = new frm_KCB_CHIDINH_CLS("-GOI,-TIEN,-CHIPHITHEM", 0);
                    frm.noitru = 0;
                    frm.objCongkham = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txtReg_ID.Text));
                    frm.Exam_ID = Utility.Int32Dbnull(txtExam_ID.Text, -1);
                    frm.IdBacsikham = _KcbChandoanKetluan != null ? _KcbChandoanKetluan.IdBacsikham : Utility.Int32Dbnull(txtBacsi.MyID, -1);
                    frm.txtidkham.Text = Utility.sDbnull(txtReg_ID.Text);
                    frm.objLuotkham = objLuotkham; // CreatePatientExam();
                    frm.objBenhnhan = objBenhnhan;
                    frm.m_eAction = action.Update;
                    frm.txtAssign_ID.Text = idChidinh.ToString();
                    frm.ShowDialog();
                    if (!frm.m_blnCancel)
                    {
                        ResetNhominCLS();
                        Laythongtinchidinhngoaitru();
                    }
                    frm.Dispose();
                    frm = null;
                    GC.Collect();
                }
                else
                {
                    Utility.ShowMsg(
                        string.Format(
                            "Phiếu chỉ định này được tạo bởi các bác sĩ khác và bạn chưa được gán quyền sửa phiếu chỉ định(quyen_suaphieuchidinhcls) nên không thể tiếp tục thao tác "));
                    return;
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
            finally
            {
                ModifyCommmands();
            }
        }

        private bool CheckPatientSelected()
        {
            objLuotkham = Utility.getKcbLuotkham(m_strMaLuotkham);
            if (objLuotkham == null)
            {
                Utility.ShowMsg(
                    "Bạn phải chọn Bệnh nhân trước khi thực hiện các công việc chỉ định Thăm khám, CLS, Kê đơn");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru >= 3)
            {
                Utility.ShowMsg("Người bệnh đã xác nhận dữ liệu ra viện nên không thể thực hiện công việc chỉ định Thăm khám, CLS, Kê đơn");
                return false;
            }
            objCongkham = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txt_idchidinhphongkham.Text));
            if (objCongkham == null)
            {
                Utility.ShowMsg("Bệnh nhân bạn chọn chưa đăng ký dịch vụ KCB(hoặc bị xóa dịch vụ KCB đang chọn) nên không được phép thăm khám. Mời bạn kiểm tra lại bằng cách nhấn nút tìm kiếm và chọn lại đúng bệnh nhân vừa làm");
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
                if (!CheckPatientSelected()) return;
                //if (Utility.Coquyen("quyen_suaphieuchidinhcls") ||  Utility.Int32Dbnull(objCongkham.IdBacsikham, -1) <= 0 || objCongkham.IdBacsikham == globalVariables.gv_intIDNhanvien)
                //{
                    DataTable dtkt = SPs.KcbGetthongtinLuotkham(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan).GetDataSet().Tables[0];
                    if (dtkt.Rows.Count <= 0)
                    {
                        Utility.ShowMsg("Không tồn tại bệnh nhân! Bạn cần nạp lại thông tin dữ liệu", "Thông Báo");
                        return;
                    }
                    if (!cmdInsertAssign.Enabled) return;
                    var frm = new frm_KCB_CHIDINH_CLS("-GOI,-TIEN,-CHIPHITHEM", 0);
                    frm.txtAssign_ID.Text = @"-100";
                    frm.Exam_ID = Utility.Int32Dbnull(txtExam_ID.Text, -1);
                    frm.IdBacsikham = _KcbChandoanKetluan != null ? _KcbChandoanKetluan.IdBacsikham : Utility.Int32Dbnull(txtBacsi.MyID, -1);
                    frm.objLuotkham = objLuotkham; // CreatePatientExam();
                    frm.objBenhnhan = objBenhnhan;
                    frm.objCongkham = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txtReg_ID.Text));
                    frm.txtidkham.Text = Utility.sDbnull(txtReg_ID.Text, -1);
                    frm.m_eAction = action.Insert;
                    frm.txtAssign_ID.Text = @"-1";
                    frm.noitru = 0;
                    frm.ShowDialog();
                    if (!frm.m_blnCancel)
                    {
                        ResetNhominCLS();
                        Laythongtinchidinhngoaitru();
                        //if (PropertyLib._ThamKhamProperties.TudongthugonCLS)
                        //    grdAssignDetail.GroupMode = GroupMode.Collapsed;
                        Utility.GotoNewRowJanus(grdAssignDetail, KcbChidinhclsChitiet.Columns.IdChidinh,
                                                frm.txtAssign_ID.Text);
                        if (PropertyLib._HISCLSProperties.InNgaySauKhiLuu && PropertyLib._HISCLSProperties.ThoatSauKhiLuu)
                            cmdPrintAssign.PerformClick();
                    }
                    frm.Dispose();
                    frm = null;
                    GC.Collect();
                //}
                //else
                //{
                //    Utility.ShowMsg(
                //        string.Format(
                //            "Bệnh nhân này đã được khám bởi Bác sĩ khác nên bạn không được phép thêm phiếu chỉ định dịch vụ "));
                //    return;
                //}
              
            }
            catch (Exception exception)
            {
                log.Trace("Loi: "+ exception);
                //throw;
            }
            finally
            {
                ModifyCommmands();
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
            ChoninphieuCLS(true);
            //try
            //{
            //    var actionResult = ActionResult.Error;
            //    string mayin = "";
            //    int vAssignId = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh),
            //                                         -1);
            //    string vAssignCode = Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhcl.Columns.MaChidinh), -1);
            //    var nhomcls = new List<string>();
            //    foreach (GridEXRow gridExRow in grdAssignDetail.GetDataRows())
            //    {
            //        if (Utility.Int64Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value) == vAssignId)
            //        if (!nhomcls.Contains(Utility.sDbnull(gridExRow.Cells["nhom_in_cls"].Value)))
            //        {
            //            nhomcls.Add(Utility.sDbnull(gridExRow.Cells["nhom_in_cls"].Value));
            //        }
            //    }
            //    List<long> lstSelectedPrint = (from p in grdAssignDetail.GetCheckedRows().AsEnumerable()
            //                                   select Utility.Int64Dbnull(p.Cells["id_chitietchidinh"].Value, 0)).ToList();
            //    string nhomincls = "ALL";
            //    if (cboServicePrint.SelectedIndex > 0)
            //    {
            //        nhomincls = Utility.sDbnull(cboServicePrint.SelectedValue, "ALL");
            //        switch (cboServicePrint.SelectedIndex)
            //        {
            //            case 1:
            //                break;
            //        }
            //    }
            //    if (cboServicePrint.SelectedIndex > 0 || chkIntach.Checked)
            //    {
            //        actionResult = KcbInphieu.InTachToanBoPhieuCls(lstSelectedPrint,(int) objLuotkham.IdBenhnhan,
            //                                                         objLuotkham.MaLuotkham, vAssignId,
            //                                                         vAssignCode, nhomcls, Utility.sDbnull(cboServicePrint.SelectedValue, "ALL"),
            //                                                         cboServicePrint.SelectedIndex, chkIntach.Checked,
            //                                                         ref mayin);
            //    }
            //    else
            //    {
            //        actionResult = KcbInphieu.InphieuChidinhCls((int) objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham,
            //                                                      vAssignId,
            //                                                      vAssignCode, nhomincls, cboServicePrint.SelectedIndex,
            //                                                      chkIntach.Checked,
            //                                                      ref mayin);
            //    }
            //    if (actionResult == ActionResult.Success)
            //    {
            //        if (PropertyLib._ThamKhamProperties.Chophepchuyencansaukhiinphieu && cmdConfirm.Visible)
            //        {
            //            if (cmdConfirm.Tag.ToString() == "1") cmdConfirm.PerformClick();
            //        }
            //    }
            //    if (mayin != "") cboLaserPrinters.Text = mayin;
            //}
            //catch (Exception ex)
            //{
            //    Utility.CatchException(ex);
            //}
            //finally
            //{
            //    GC.Collect();
            //}
        }
        private decimal GetTotalDatatable(DataTable dataTable, string filedName, string filer)
        {
            return Utility.DecimaltoDbnull(dataTable.Compute("SUM(" + filedName + ")", filer), 0);
        }
        public FTPclient FtpClient;
        public string _FtpClientCurrentDirectory;
        private string _baseDirectory = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "Radio\\");
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
        private void BeginExam()
        {
            
            if (!Utility.isValidGrid(grdAssignDetail)) return;
            int v_id_chitietchidinh = -1;
            try
            {
                if (PropertyLib._HinhAnhProperties == null) PropertyLib._HinhAnhProperties = PropertyLib.GetHinhAnhProperties();
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    if (grdAssignDetail.CurrentRow != null && grdAssignDetail.CurrentRow.RowType == RowType.Record)
                    {
                        v_id_chitietchidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "id_chitietchidinh"), -1);
                        if (Utility.DoTrim(Utility.sDbnull(grdAssignDetail.CurrentRow.Cells["dsach_vungkhaosat"].Value, "")) == "")
                        {
                            Utility.ShowMsg("Dịch vụ này chưa gán vùng khảo sát nên không thể nhập kết quả.\r\nNhấn OK để thực hiện gán vùng khảo sát cho dịch vụ đang chọn");
                            frm_chonvungksat _chonvungks = new frm_chonvungksat(new List<string>());
                            _chonvungks.Hthi_Chon = true;
                            _chonvungks.ten_dvu = Utility.GetValueFromGridColumn(grdAssignDetail, "ten_chitietdichvu");
                            if (_chonvungks.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                if (Utility.DoTrim(_chonvungks.vungks).Length > 0)
                                {
                                    grdAssignDetail.CurrentRow.BeginEdit();
                                    grdAssignDetail.CurrentRow.Cells["dsach_vungkhaosat"].Value = _chonvungks.vungks;
                                    grdAssignDetail.CurrentRow.EndEdit();
                                    grdAssignDetail.UpdateData();
                                    grdAssignDetail.Refetch();
                                    DmucDichvuclsChitiet objDvu = DmucDichvuclsChitiet.FetchByID(Utility.GetValueFromGridColumn(grdAssignDetail, "id_chitietdichvu"));
                                    if (objDvu != null)
                                    {
                                        objDvu.DsachVungkhaosat = _chonvungks.vungks;
                                        objDvu.Save();
                                        m_dtAssignDetail.AsEnumerable().Where(c => c.Field<Int32>("id_chitietdichvu") == Utility.Int64Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "id_chitietdichvu"))).ToList().ForEach(x1 => { x1["dsach_vungkhaosat"] = _chonvungks.vungks; });
                                    }
                                }
                            }
                            else
                            {
                                Utility.ShowMsg("Bạn vừa hủy chọn vùng khảo sát nên không thể nhập kết quả");
                                return;
                            }

                        }
                        DataRowView dr = grdAssignDetail.CurrentRow.DataRow as DataRowView;
                        Utility.SetMsg(lblMsg, "Đang nạp thông tin bệnh nhân...", false);
                       if(FtpClient!=null) FtpClient.CurrentDirectory = _FtpClientCurrentDirectory;
                        int Status = Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells["trang_thai"].Value, -1);
                        if (Status <= 2)
                        {
                            new KCB_HinhAnh().UpdateXacNhanDaThucHien(v_id_chitietchidinh, 2);
                        }
                        int id_VungKS = Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells["id_VungKS"].Value, -1);
                        if (id_VungKS > 0)
                        {
                            goto _EnterResult;
                        }
                        List<string> lstID = new List<string>();
                        lstID = Utility.sDbnull(grdAssignDetail.CurrentRow.Cells["dsach_vungkhaosat"].Value, "-1").Split(',').ToList<string>();
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
                        frm_NhaptraKQ frm = new frm_NhaptraKQ();
                        DataTable dtWorkList = SPs.HinhanhTimkiembnhanTheoIDchidinhchitiet(v_id_chitietchidinh).GetDataSet().Tables[0];
                        frm.drWorklistDetail = (((DataRowView)grdAssignDetail.CurrentRow.DataRow).Row);
                        frm.drWorklist = dtWorkList.Rows[0];
                        if (frm.FtpClient == null) frm.FtpClient = this.FtpClient;
                        frm.ID_Study_Detail = v_id_chitietchidinh;
                        frm.lstID = Utility.sDbnull(grdAssignDetail.CurrentRow.Cells["dsach_vungkhaosat"].Value, "-1").Split(',').ToList<string>();
                        frm.id_VungKS = id_VungKS;
                        frm.StrServiceCode = Utility.sDbnull(grdAssignDetail.CurrentRow.Cells["ma_loaidichvu"].Value);
                        frm.ShowDialog();
                        if (!frm.mv_blnCancel)
                        {
                            grdAssignDetail_SelectionChanged(grdAssignDetail, new EventArgs());
                        }
                        frm.Dispose();
                        frm = null;
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
                Utility.SetMsg(lblMsg, "Mời bạn tiếp tục làm việc...", false);
                this.Cursor = Cursors.Default;
            }
        }
        private void GetPatienInfoAddreport(ref DataTable dtReportPhieuXetNghiem)
        {
            // Kiểm tra và cộng cột
            // Kiểm tra và cộng cột
            if (!dtReportPhieuXetNghiem.Columns.Contains(KcbDanhsachBenhnhan.Columns.DiaChi))
                dtReportPhieuXetNghiem.Columns.Add(KcbDanhsachBenhnhan.Columns.DiaChi, typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains(KcbDanhsachBenhnhan.Columns.NamSinh))
                dtReportPhieuXetNghiem.Columns.Add(KcbDanhsachBenhnhan.Columns.NamSinh, typeof (int));


            if (!dtReportPhieuXetNghiem.Columns.Contains(KcbDanhsachBenhnhan.Columns.TenBenhnhan))
                dtReportPhieuXetNghiem.Columns.Add(KcbDanhsachBenhnhan.Columns.TenBenhnhan, typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains(KcbDanhsachBenhnhan.Columns.GioiTinh))
                dtReportPhieuXetNghiem.Columns.Add(KcbDanhsachBenhnhan.Columns.GioiTinh, typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("Tuoi"))
                dtReportPhieuXetNghiem.Columns.Add("Tuoi", typeof (int));
            if (!dtReportPhieuXetNghiem.Columns.Contains(KcbDanhsachBenhnhan.Columns.GioiTinh))
                dtReportPhieuXetNghiem.Columns.Add(KcbDanhsachBenhnhan.Columns.GioiTinh, typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("Rank_Name"))
                dtReportPhieuXetNghiem.Columns.Add("Rank_Name", typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("Position_Name"))
                dtReportPhieuXetNghiem.Columns.Add("Position_Name", typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("Unit_Name"))
                dtReportPhieuXetNghiem.Columns.Add("Unit_Name", typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("chan_doan"))
                dtReportPhieuXetNghiem.Columns.Add("chan_doan", typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains(DmucDoituongkcb.Columns.TenDoituongKcb))
                dtReportPhieuXetNghiem.Columns.Add(DmucDoituongkcb.Columns.TenDoituongKcb, typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("mathe_bhyt"))
                dtReportPhieuXetNghiem.Columns.Add("mathe_bhyt", typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("Barcode"))
                dtReportPhieuXetNghiem.Columns.Add("Barcode", typeof (byte[]));
            
            byte[] byteArray = null;
            Utility.CreateBarcodeData(ref dtReportPhieuXetNghiem, m_strMaLuotkham, ref byteArray);
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

                string _kenhieudon = THU_VIEN_CHUNG.Laygiatrithamsohethong("KE_NHIEU_DON", "N", false);
                var lstPres = new Select()
                        .From(KcbDonthuoc.Schema)
                        .Where(KcbDonthuoc.Columns.IdKham).IsEqualTo(Utility.sDbnull(objCongkham.IdKham)).
                        //.And(KcbDonthuoc.IdBenhnhanColumn).IsEqualTo(Utility.sDbnull(objLuotkham.IdBenhnhan)).
                        ExecuteAsCollection<KcbDonthuocCollection>();

                IEnumerable<KcbDonthuoc> lstPres1 = from p in lstPres
                                                    where p.IdKham == objCongkham.IdKham
                                                    select p;
                if (objLuotkham.MaDoituongKcb == "BHYT")
                {
                    if (_kenhieudon == "Y" && lstPres1.Count() <= 0) //Được phép kê mỗi phòng khám 1 đơn thuốc
                        return false;
                    if (_kenhieudon == "N" && lstPres.Count > 0 && lstPres1.Count() <= 0)
                        //Cảnh báo ko được phép kê đơn tiếp
                    {
                        Utility.ShowMsg(
                            "Chú ý: Bệnh nhân này thuộc đối tượng BHYT và đã được kê đơn thuốc tại phòng khám khác." +
                            " Bạn cần trao đổi với Quản trị hệ thống để được cấu hình kê đơn thuốc tại nhiều phòng khác khác nhau với đối tượng BHYT này",
                            "Thông báo");
                        return false;
                    }
                }
                else
                    //Bệnh nhân dịch vụ-->cho phép kê 1 đơn nếu đơn chưa thanh toán và nhiều đơn nếu các đơn trước đã thanh toán
                {
                    if (lstPres1.Count() > 0)
                        if (lstPres1.FirstOrDefault().TrangthaiThanhtoan == 0) //Chưa thanh toán-->Cần sửa đơn
                            return true;
                        else //Đã thanh toán-->Cho phép thêm đơn mới
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
                Utility.ShowMsg("Lỗi khi kiểm tra số lượng đơn thuốc của lần khám\r\n" + ex.Message);
                return false;
            }
        }

        private void cmdCreateNewPres_Click(object sender, EventArgs e)
        {
            if (!CheckPatientSelected()) return;
            if (!cmdThemDonthuoc.Enabled) return;
            
            DataTable dtkt = SPs.KcbGetthongtinLuotkham(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan).GetDataSet().Tables[0];
            if (dtkt.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tồn tại bệnh nhân! Bạn cần nạp lại thông tin dữ liệu", "Thông Báo");
                return ;
            }
            //if (!ExistsDonThuoc())
            //{
                ThemMoiDonThuoc();
            //}
            //else
            //{
            //    UpdateDonThuoc();
            //}
        }

        private void ThemMoiDonThuoc()
        {
            try
            {
               // cmdLuuChandoan.PerformClick();
                // KeDonThuocTheoDoiTuong();
                frm_KCB_KE_DONTHUOC frm = new frm_KCB_KE_DONTHUOC("THUOC");
                frm._OnSaveMe += frm__OnSaveMe;
                frm.objCongkham = this.objCongkham;
                frm.em_Action = action.Insert;
                frm.objLuotkham = objLuotkham;
                frm.IdBacsikham =_KcbChandoanKetluan!=null?_KcbChandoanKetluan.IdBacsikham: Utility.Int32Dbnull(txtBacsi.MyID, -1);
                frm._KcbCDKL = _KcbChandoanKetluan;
                frm._MabenhChinh = autoICD_2mat.MyCode;
                frm._Chandoan = txtChanDoan.Text;
                frm.DtIcd = dt_ICD;
                frm.dt_ICD_PHU = dt_ICD_PHU;
                frm.id_kham = Utility.Int32Dbnull(txtExam_ID.Text);
                frm.objCongkham = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txtReg_ID.Text));
                frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                frm.txtPatientID.Text = Utility.sDbnull(objBenhnhan.IdBenhnhan, "-1");
                frm.txtSoDT.Text = Utility.sDbnull(objBenhnhan.DienThoai,"");
                frm.txtPatientName.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan, "");
                frm.txtYearBirth.Text = Utility.sDbnull(objBenhnhan.NamSinh, "");
                frm.txtSex.Text = Utility.sDbnull(objBenhnhan.GioiTinh,"");
                frm.txtSDT.Text = objLuotkham.Sdt;
                frm.txtPres_ID.Text = "-1";
                frm.dtNgayKhamLai.MinDate = dtpNgaydangky.Value;
                frm._ngayhenkhamlai = dtpNgaydangky.Value.ToString("yyMMdd") == dtpNgayHen.Value.ToString("yyMMdd") ? "" : dtpNgayHen.Text;
                frm.noitru = 0;
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();

                if (!frm.m_blnCancel)
                {
                    if (frm._MabenhChinh !=Utility.sDbnull(autoICD_2mat.MyCode))
                    {
                        //cbo_icd.SelectedValue = frm._MabenhChinh;
                        autoICD_2mat.SetCode(frm._MabenhChinh);
                        //autoICD_2mat.RaiseEnterEvents();
                    }
                    txtChanDoan._Text = frm._Chandoan;
                    dt_ICD_PHU = frm.dt_ICD_PHU;
                    //if (frm._KcbCDKL != null)
                    //    _KcbChandoanKetluan = frm._KcbCDKL;
                    if (frm.chkNgayTaiKham.Checked)
                    {
                        dtpNgayHen.Value = frm.dtNgayKhamLai.Value;
                        cmdLuuKetluan.PerformClick();
                    }
                    else
                    {
                        dtpNgayHen.Value = dtpNgaydangky.Value;
                        cmdLuuKetluan.PerformClick();
                    }
                    fillmabenhchinh = false;
                    FillThongtinHoibenhVaChandoan();
                    fillmabenhchinh = true;
                    Laythongtinchidinhngoaitru();
                    Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuoc.Columns.IdDonthuoc,
                                            Utility.sDbnull(frm.txtPres_ID.Text));
                }
                frm.Dispose();
                frm = null;
                GC.Collect();
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
                ModifyCommmands();
                txtPatient_Code.Focus();
                txtPatient_Code.SelectAll();
            }
        }

        void frm__OnSaveMe(long id_donthuoc, string KieuthuocVT)
        {
            if (KieuthuocVT == "THUOC")
                tabDiagInfo.SelectedTab = tabPageChidinhThuoc;
            else
                tabDiagInfo.SelectedTab = tabPageVTTH;
            Application.DoEvents();
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
            //        Laythongtinchidinhngoaitru();
            //        Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuocChitiet.Columns.IdDonthuoc,
            //                                Utility.sDbnull(frm.txtPres_ID.Text));
            //        ModifyCommmands();
            //    }
            //}
        }
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
            if (!cmdSuadonthuoc.Enabled) return;

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
                return ;
            }
           
        }

        private bool Donthuoc_DangXacnhan(int pres_id)
        {
            var _item =
                new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.IdDonthuocColumn).IsEqualTo(pres_id).And(
                    KcbDonthuoc.TrangThaiColumn).IsEqualTo(1).ExecuteSingle<KcbDonthuoc>();
            if (_item != null) return true;
            return false;
        }
        DataTable dtTemp = null;
        private void UpdateDonThuoc()
        {
            try
            {
                if (grdPresDetail.RowCount>0)//grdPresDetail.CurrentRow != null && grdPresDetail.CurrentRow.RowType == RowType.Record)
                {
                    if (objLuotkham != null)
                    {
                       
                        int Pres_ID = Utility.Int32Dbnull(Utility.getCellValuefromGridEXRow(RowThuoc,KcbDonthuocChitiet.Columns.IdDonthuoc),-1);// grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                        dtTemp = SPs.KcbDonthuocChitietKiemtraDieuchinhtaiquay(Pres_ID).GetDataSet().Tables[0];
                        if (dtTemp.Rows.Count > 0)
                        {
                            Utility.ShowMsg("Đơn thuốc này đã được quầy chỉnh sửa số lượng nên bạn không thể chỉnh sửa. Muốn sửa đơn thuốc, cần yêu cầu quầy thuốc chỉnh số lượng sửa về 0");
                            return;
                        }
                        if (Donthuoc_DangXacnhan(Pres_ID))
                        {
                            Utility.ShowMsg(
                                "Đơn thuốc này đang ở trạng thái đã duyệt cho Bệnh nhân nên không thể chỉnh sửa. Đề nghị quay lại hỏi bộ phận cấp phát thuốc tại phòng Dược");
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
                            var frm = new frm_KCB_KE_DONTHUOC("THUOC");
                            frm._OnSaveMe += frm__OnSaveMe;
                            frm.em_Action = action.Update;
                            frm._KcbCDKL = _KcbChandoanKetluan;
                            frm.IdBacsikham = _KcbChandoanKetluan != null ? _KcbChandoanKetluan.IdBacsikham : Utility.Int32Dbnull(txtBacsi.MyID, -1);
                            frm._MabenhChinh = autoICD_2mat.MyCode;
                            frm._Chandoan = txtChanDoan.Text;
                            frm.DtIcd = dt_ICD;
                            frm.dt_ICD_PHU = dt_ICD_PHU;
                            frm.noitru = 0;
                            frm.objLuotkham = objLuotkham;
                            frm.id_kham = Utility.Int32Dbnull(txtExam_ID.Text);
                            frm.objCongkham = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txtReg_ID.Text));
                            frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                            frm.txtPatientID.Text = Utility.sDbnull(objBenhnhan.IdBenhnhan, "-1");
                            frm.txtSoDT.Text = Utility.sDbnull(objBenhnhan.DienThoai, "");
                            frm.txtPatientName.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan, "");
                            frm.txtYearBirth.Text = Utility.sDbnull(objBenhnhan.NamSinh, "");
                            frm.txtSex.Text = Utility.sDbnull(objBenhnhan.GioiTinh, "");
                            frm.txtSDT.Text = objLuotkham.Sdt;
                            frm.txtPres_ID.Text = Utility.sDbnull(objPrescription.IdDonthuoc);
                            frm.dtNgayKhamLai.MinDate = dtpNgaydangky.Value;
                            frm._ngayhenkhamlai = dtpNgaydangky.Value.ToString("yyMMdd") == dtpNgayHen.Value.ToString("yyMMdd") ? "" : dtpNgayHen.Text;

                            frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                            frm.ShowDialog();
                            if (!frm.m_blnCancel)
                            {
                                autoICD_2mat.SetCode( frm._MabenhChinh);
                                txtChanDoan._Text = frm._Chandoan;
                                dt_ICD_PHU = frm.dt_ICD_PHU;
                                //if (frm._KcbCDKL != null) _KcbChandoanKetluan = frm._KcbCDKL;
                                if (frm.chkNgayTaiKham.Checked)
                                {
                                    dtpNgayHen.Value = frm.dtNgayKhamLai.Value;
                                    cmdLuuKetluan.PerformClick();
                                }
                                else
                                {
                                    dtpNgayHen.Value = dtpNgaydangky.Value;
                                    cmdLuuKetluan.PerformClick();
                                }
                                FillThongtinHoibenhVaChandoan();
                                Laythongtinchidinhngoaitru();
                                Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuocChitiet.Columns.IdDonthuoc,
                                                        Utility.sDbnull(frm.txtPres_ID.Text));
                            }
                            frm.Dispose();
                            frm = null;
                            GC.Collect();
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                ModifyCommmands();
            }
        }

        private List<int> GetIdChitiet(int IdDonthuoc, int id_thuoc, decimal don_gia, ref string s)
        {
            //DataRow[] arrDr =
            //    m_dtPresDetail.Select(KcbDonthuocChitiet.Columns.IdDonthuoc + "=" + IdDonthuoc.ToString() + " AND " +
            //                          KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc.ToString()
            //                          + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + don_gia.ToString());
            
            var _data = from p in m_dtPresDetail.AsEnumerable()
                    where Utility.Int32Dbnull( p[KcbDonthuocChitiet.Columns.IdDonthuoc]) == IdDonthuoc
                    && Utility.Int32Dbnull(p[KcbDonthuocChitiet.Columns.IdThuoc]) == id_thuoc
                    && Utility.DecimaltoDbnull(p[KcbDonthuocChitiet.Columns.DonGia]) == don_gia
                    select p;
            if (_data.Any())
            {
                DataRow[] arrDr = _data.ToArray<DataRow>();
                IEnumerable<string> p1 = (from q in arrDr.AsEnumerable()
                                          select Utility.sDbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).
                    Distinct();
                s = string.Join(",", p1.ToArray());
                IEnumerable<int> p = (from q in arrDr.AsEnumerable()
                                      select Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).
                    Distinct();
                return p.ToList();
            }
            return new List<int>();
        }

        private void DeletefromDatatable(List<int> lstIdChitietDonthuoc)
        {
            try
            {
                DataRow[] p = (from q in m_dtPresDetail.Select("1=1").AsEnumerable()
                               where
                                   lstIdChitietDonthuoc.Contains(
                                       Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]))
                               select q).ToArray<DataRow>();
                for (int i = 0; i <= p.Length - 1; i++)
                    m_dtPresDetail.Rows.Remove(p[i]);
                m_dtPresDetail.AcceptChanges();
            }
            catch
            {
            }
        }

        private void PerformActionDeletePres()
        {
            string s = "";
            var lstIdchitiet = new List<int>();

            if (grdPresDetail.GetCheckedRows().Count() <= 0 && RowThuoc != null)
            {
                try
                {
                    RowThuoc.BeginEdit();
                    RowThuoc.IsChecked = true;
                    RowThuoc.EndEdit();
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất 1 chi tiết thuốc để xóa");
                    return;
                }
            }
            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                string stempt = "";
                int id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, 0m);
                int IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value, 0m);
                decimal dongia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, 0m);
                List<int> _temp = GetIdChitiet(IdDonthuoc, id_thuoc, dongia, ref stempt);
                s += "," + stempt;
                lstIdchitiet.AddRange(_temp);
                //gridExRow.Delete();
                //grdPresDetail.UpdateData();
            }
            if (lstIdchitiet.Count <= 0) return;
            if (s.Length > 0) s = s.Substring(1);
            _KCB_KEDONTHUOC.XoaChitietDonthuoc(s);
           DataRow[] rows =
                        m_dtPresDetail.Select(KcbDonthuocChitiet.Columns.IdChitietdonthuoc + " IN (" + String.Join(",", lstIdchitiet.ToArray()) + ")"); 
            string _deleteitems = string.Join(",", (from p in rows.AsEnumerable()
                                                    select Utility.sDbnull(p["ten_thuoc"])).ToList<string>());
            // UserName is Column Name
            Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa đơn thuốc của bệnh nhân ID={0}, PID={1}, Tên={2}, DS thuốc xóa={3} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan, _deleteitems), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
            DeletefromDatatable(lstIdchitiet);
            m_dtPresDetail.AcceptChanges();
        }

        private void PerformActionDeletePres_old()
        {
            string s = "";
            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {                    
                int vIntIdDonthuoc =
                    Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                                        -1);
                _KCB_KEDONTHUOC.XoaChitietDonthuoc(vIntIdDonthuoc);
                gridExRow.Delete();
                grdPresDetail.UpdateData();
                m_dtPresDetail.AcceptChanges();
            }
        }

        private void PerformActionDeleteSelectedDrug()
        {
            try
            {
                int Pres_ID =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value,
                                        -1);
                int v_IdChitietdonthuoc =
                    Utility.Int32Dbnull(
                        grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                        -1);
                int v_intIDThuoc =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value,
                                        -1);
                _KCB_KEDONTHUOC.XoaChitietDonthuoc(v_IdChitietdonthuoc);
                Utility.Log(Name, globalVariables.UserName,
                                   string.Format(
                                       "Xóa thuốc có mã là: {0} - đơn thuôc: {3} của bệnh nhân có mã lần khám: {1} và mã bệnh nhân là: {2}",
                                       Utility.Int32Dbnull(
                                           grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1),
                                       objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, Utility.Int32Dbnull(
                                           grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value,
                                           -1)), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                grdPresDetail.CurrentRow.Delete();
                grdPresDetail.UpdateData();
                m_dtPresDetail.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message + "-->" +
                                "Bạn nên dùng chức năng xóa thuốc bằng cách chọn thuốc và sử dụng nút xóa thuốc");
            }
        }

        private bool KiemtraThuocTruockhixoa()
        {
            bool b_Cancel = false;
            if (chkShowGroup.Checked)
            {
                Utility.ShowMsg("Bạn cần bỏ check mục Nhóm ID thuốc kho khi thực hiện thao tác xóa");
                grdPresDetail.UnCheckAllRecords();
                chkShowGroup.Focus();
                return false;
            }
            if (RowThuoc == null || grdPresDetail.GetCheckedRows().Count() <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện việc xóa thông tin thuốc ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }
            if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa các thuốc đang chọn hay không?", "Xác nhận xóa", true)) return false;
            if (grdPresDetail.GetCheckedRows().Count() <= 0 && RowThuoc != null)//Cho chắc ăn
            {
                try
                {
                    RowThuoc.BeginEdit();
                    RowThuoc.IsChecked = true;
                    RowThuoc.EndEdit();
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất 1 chi tiết thuốc để xóa");
                    return false;
                }

            }
            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                if (Utility.Coquyen("quyen_suadonthuoc") ||globalVariables.IsAdmin ||
                    Utility.sDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "") ==
                    globalVariables.UserName)
                {
                }
                else
                {
                    Utility.ShowMsg(
                        "Trong các thuốc bạn chọn xóa, có một số thuốc được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các thuốc do chính bạn kê để thực hiện xóa");
                    return false;
                }
            }
            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    int vIdChitietdonthuoc =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                KcbDonthuocChitiet kcbDonthuocChitiet = KcbDonthuocChitiet.FetchByID(vIdChitietdonthuoc);
                if (kcbDonthuocChitiet != null &&(Utility.Byte2Bool(kcbDonthuocChitiet.TrangthaiThanhtoan) ||
                     Utility.Byte2Bool(kcbDonthuocChitiet.TrangThai)))
                    {
                        b_Cancel = true;
                        break;
                    }
                }
            }
            if (b_Cancel)
            {
                Utility.ShowMsg(
                    "Một số thuốc bạn chọn đã thanh toán hoặc đã phát thuốc cho Bệnh nhân nên bạn không được phép xóa. Mời bạn kiểm tra lại ",
                    "Thông báo",
                    MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }
            return true;
        }

        private bool IsValidDeleteSelectedDrug()
        {
            bool bCancel = false;
            if (chkShowGroup.Checked)
            {
                Utility.ShowMsg("Bạn cần bỏ check mục Nhóm ID thuốc kho khi thực hiện thao tác xóa");
                grdPresDetail.UnCheckAllRecords();
                chkShowGroup.Focus();
                return false;
            }
            KcbLuotkham item = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text, 0), m_strMaLuotkham);
            if (item == null)
            {
                Utility.ShowMsg("Bạn phải chọn Bệnh nhân hoặc bệnh nhân không tồn tại!", "Thông báo",
                                MessageBoxIcon.Warning);
                txtPatient_Code.Focus();
                return false;
            }

            if (item != null && Utility.Int32Dbnull(item.TrangthaiNoitru, -1) >= 1)
            {
                Utility.ShowMsg(
                    "Bệnh nhân đã được điều trị nội trú. Bạn chỉ có thể xem thông tin và không được phép làm các công việc liên quan đến ngoại trú",
                    "Thông báo");
                cmdKetthuckham.Focus();
                return false;
            }
            if (grdPresDetail.RowCount <= 0 || grdPresDetail.CurrentRow.RowType != RowType.Record)
            {
                Utility.ShowMsg("Bạn phải chọn một thuốc để xóa ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }
            if (Utility.Coquyen("quyen_suadonthuoc") ||
                Utility.sDbnull(grdPresDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "") ==
                globalVariables.UserName)
            {
            }
            else
            {
                Utility.ShowMsg(
                    "Thuốc đang chọn xóa được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các thuốc do chính bạn kê để thực hiện xóa");
                return false;
            }
            if (grdPresDetail.CurrentRow.RowType == RowType.Record)
            {
                int vIdChitietdonthuoc =
                    Utility.Int32Dbnull(
                        grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                        -1);
                //int i =
                //    m_dtPresDetail.Select(KcbDonthuocChitiet.Columns.IdChitietdonthuoc + " = " + vIdChitietdonthuoc +
                //                          "AND (" +
                //                          KcbDonthuocChitiet.Columns.TrangThai + ">=1 OR " +
                //                          KcbDonthuocChitiet.Columns.TrangthaiThanhtoan + " >= 1)").Count();
                KcbDonthuocChitiet kcbDonthuocChitiet = KcbDonthuocChitiet.FetchByID(vIdChitietdonthuoc);

                if (Utility.Int16Dbnull(kcbDonthuocChitiet.TrangthaiThanhtoan,0) > 0)
                {
                    bCancel = true;
                }
            }

            if (bCancel)
            {
                Utility.ShowMsg(
                    "Một số thuốc bạn chọn đã thanh toán hoặc đã phát thuốc cho Bệnh nhân nên bạn không được phép xóa. Mời bạn kiểm tra lại ",
                    "Thông báo",
                    MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }
            return true;
        }

        private void cmdDeletePres_Click(object sender, EventArgs e)
        {
            if (!KiemtraThuocTruockhixoa()) return;
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
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong_off("KCB_THAMKHAM_TACHDONTHUOC", "0", false) == "1")
                {
                    if (RowThuoc == null)
                    {
                        Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện việc xóa thông tin thuốc ", "Thông báo", MessageBoxIcon.Warning);
                        grdPresDetail.Focus();
                        return;
                    }
                    int presId = Utility.Int32Dbnull(RowThuoc.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value);
                    PrintPres(presId, "");
                }
                else
                {
                    string  maLanKham = Utility.sDbnull(txtPatient_Code.Text.Trim());
                    PrintPresGop(maLanKham, "");
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
                // throw;
            }
        }
        private void PrintPresGop(string maLanKham, string forcedTitle)
        {
            DataTable v_dtData = _KCB_KEDONTHUOC.LaythongtinDonthuoc_InGop(maLanKham);
            Utility.AddColumToDataTable(ref v_dtData, "BarCode", typeof(byte[]));
            int Pres_ID = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA5.xml");
            byte[] Barcode = null;
            Utility.CreateBarcodeData(ref v_dtData, m_strMaLuotkham, ref Barcode);
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
          private void PrintTuvanthem(int presID, string forcedTitle, DataTable p_dtData)
        {
            
            DataRow[] arrDR = p_dtData.Select("tuvan_them=1");
              if(arrDR.Length<=0) return;
             DataTable v_dtData = arrDR.CopyToDataTable();
            Utility.AddColumToDataTable(ref v_dtData, "BarCode", typeof (byte[]));
            int Pres_ID = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA5.xml");
            byte[] Barcode = null;
            Utility.CreateBarcodeData(ref v_dtData, m_strMaLuotkham, ref Barcode);
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
        /// <summary>
        /// hàm thực hiện việc in đơn thuốc. bỏ đi để dùng hàm in theo report code tránh lỗi 2 thuốc 2 tính chất tách làm 2 cái khác nhau
        /// </summary>
        /// <param name="presID"></param>
        /// <param name="forcedTitle"></param>
        private void PrintPres(int presID, string forcedTitle)
        {
            DataTable v_dtDataOrg = _KCB_KEDONTHUOC.LaythongtinDonthuoc_In(presID);
            DataRow[] arrDR = v_dtDataOrg.Select("tuvan_them=0");
            if (arrDR.Length <= 0)
            {
                PrintTuvanthem(presID, forcedTitle, v_dtDataOrg);
                return;
            }
            DataTable v_dtData = arrDR.CopyToDataTable();
           

            Utility.AddColumToDataTable(ref v_dtData, "BarCode", typeof (byte[]));
            int Pres_ID = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA5.xml");
            byte[] Barcode = null;
            Utility.CreateBarcodeData(ref v_dtData, m_strMaLuotkham, ref Barcode);
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
          //  THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA4.xml");
            v_dtData.AcceptChanges();
            // log.Info("Thuc hien in don thuoc");
            Utility.UpdateLogotoDatatable(ref v_dtData);
            List<string> lstmatinhchat = (from p in v_dtData.AsEnumerable()
                                          select Utility.sDbnull(p["ma_tinhchat"], "")).Distinct().ToList<string>();
            foreach (string ma_tinhchat in lstmatinhchat)
            {
                DataRow[] arrTemp = v_dtData.Select(string.Format("(ma_tinhchat='{0}' or ma_tinhchat is null) and printed=0", ma_tinhchat));
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
                    reportDocument = Utility.GetReport(reportCode,KhoGiay, ref tieude, ref reportname);
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
                        Utility.SetParameterValue(crpt, "txtTrinhky",
                                Utility.getTrinhky(objForm.mv_sReportFileName,
                                    globalVariables.SysDate));
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
        private void PrintPres_bak(int presID, string forcedTitle)
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
            Utility.CreateBarcodeData(ref v_dtData, m_strMaLuotkham, ref Barcode);
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
            //  THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA4.xml");
            v_dtData.AcceptChanges();
            // log.Info("Thuc hien in don thuoc");
            Utility.UpdateLogotoDatatable(ref v_dtData);
            //List<string> lstReportCode = v_PrintData.Rows[0]["report_code"].ToString().Split('@')[0].Split(';').ToList<string>();

            List<string> lstReportCode = (from p in v_dtData.AsEnumerable()
                                          select Utility.sDbnull(p["report_code"], "").Split('@')[0]).Distinct().ToList<string>();
            foreach (string report_code in lstReportCode)
            {
                DataTable v_PrintData = v_dtData.Select(string.Format("report_code='{0}' or report_code is null", report_code)).CopyToDataTable();//Chắc chắn có dữ liệu nên hàm copy ko bị lỗi
                //Lấy danh sách các reportcode của từng tính chất thuốc
                if (lstReportCode.Count <= 0) lstReportCode.Add("thamkham_InDonthuocA4");
                //foreach (string _rcode in lstReportCode)
                //{
                    string KhoGiay = "A100";// "A5";//Truyền giá trị này để giữ nguyên report
                    if (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4) KhoGiay = "A4";
                    var reportDocument = new ReportDocument();
                    string tieude = "", reportname = "", reportCode = "";
                    reportCode = report_code;
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
                        Utility.SetParameterValue(crpt, "txtTrinhky",
                                Utility.getTrinhky(objForm.mv_sReportFileName,
                                    globalVariables.SysDate));
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
                //}//Kết thúc vòng for qua các liên trong tính chất
            }//Kết thúc vòng for tính chất
            //In đơn tư vấn thêm(nếu có)
            PrintTuvanthem(presID, forcedTitle, v_dtDataOrg);
        }
        #endregion

        #region "Xử lý tác vụ của phần lưu thông tin "
        void KetthucKham()
        {
            try
            {
                errorProvider1.Clear();
                if (objCongkham==null || objLuotkham == null || !Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn chưa chọn người bệnh để thực hiện thăm khám. Đề nghị kiểm tra lại", "Thông Báo");
                  
                    return;
                }
                DataTable dtkt = SPs.KcbGetthongtinLuotkham(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan).GetDataSet().Tables[0];
                if (dtkt.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tồn tại bệnh nhân(Có thể bị xóa sau khi bạn chọn). Vui lòng kiểm tra lại", "Thông Báo");
                    return;
                }
                else
                {
                    if (objCongkham == null)
                        objCongkham = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txt_idchidinhphongkham.Text));
                    if (Utility.Int16Dbnull(objCongkham.IdBacsikham, -1) > 0 && Utility.Int16Dbnull(objCongkham.IdBacsikham, -1) != Utility.Int16Dbnull(txtBacsi.MyID, -1))
                    {
                        DmucNhanvien objNV = DmucNhanvien.FetchByID(Utility.Int32Dbnull(objCongkham.IdBacsikham, -1));
                        Utility.ShowMsg(string.Format("Ca khám được kết thúc bởi bác sĩ {0} nên bạn không được phép kết thúc nữa", objNV.TenNhanvien, objNV.TenNhanvien));
                        return;
                    }

                    PerformAction();
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                Utility.SetMsg(lblTrangthainoitru, Utility.Laythongtintrangthainguoibenh(objLuotkham), false);
                ModifyCommmands();
            }
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            KetthucKham();
            
        }

        private string GetDanhsachBenhphu(bool isAll)
        {
            var sMaICDPHU = new StringBuilder("");
            try
            {
                int recordRow = 0;

                if (dt_ICD_PHU.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_ICD_PHU.Rows)
                    {
                        if (isAll || Utility.Int64Dbnull(row[KcbDangkyKcb.Columns.IdKham], -1) == objCongkham.IdKham)//Chỉ lấy mã bệnh phụ theo công khám
                        {
                            if (recordRow > 0)
                                sMaICDPHU.Append(",");
                            sMaICDPHU.Append(Utility.sDbnull(row[DmucBenh.Columns.MaBenh], ""));
                            recordRow++;
                        }
                    }
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
        private KcbChandoanKetluan TaoDulieuChandoanKetluan(byte savedType)
        {
            try
            {
                if (objCongkham != null) objCongkham.ThoigianBatdau = dtpThoigian_batdau.Value;
                _KcbChandoanKetluan = new Select().From(KcbChandoanKetluan.Schema)
                                        .Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(objCongkham.IdKham).
                                        ExecuteSingle
                                        <KcbChandoanKetluan>();
                if (_KcbChandoanKetluan == null)
                    _KcbChandoanKetluan = new KcbChandoanKetluan();
                if (_KcbChandoanKetluan.IdChandoan<=0)
                {
                    _KcbChandoanKetluan.IsNew = true;
                    _KcbChandoanKetluan.IdChandoan = -1;
                    _KcbChandoanKetluan.NgayTao = globalVariables.SysDate;
                    _KcbChandoanKetluan.NguoiTao = globalVariables.UserName;
                }
                else
                {
                    _KcbChandoanKetluan.IsNew = false;
                    _KcbChandoanKetluan.MarkOld();
                    _KcbChandoanKetluan.NguoiSua = globalVariables.UserName;
                    _KcbChandoanKetluan.NgaySua = globalVariables.SysDate;
                    _KcbChandoanKetluan.IpMaysua = globalVariables.gv_strIPAddress;
                    //Kiểm tra
                    //if (Utility.Coquyen("quyen_suathongtinhoibenhvachandoan") || objchuandoan.NguoiTao ==globalVariables.UserName)
                    //{
                    //}
                    //else
                    //{
                    //    return null;
                    //}
                }
                
                _KcbChandoanKetluan.IdKham = Utility.Int64Dbnull(txtExam_ID.Text, -1);
                _KcbChandoanKetluan.MaLuotkham = Utility.sDbnull(m_strMaLuotkham, "");
                _KcbChandoanKetluan.IdBenhnhan = Utility.Int64Dbnull(txtPatient_ID.Text, "-1");
                if (savedType !=0)//Lưu kết luận hoặc cả 2
                {
                    if (Utility.sDbnull(objLuotkham.MabenhChinh, "").Length > 0 && (_KcbChandoanKetluan.IdChandoan <= 0 || Utility.sDbnull(_KcbChandoanKetluan.MabenhChinh, "").Length <= 0))
                    {
                    }
                    else
                    {
                        _KcbChandoanKetluan.MabenhChinh = Utility.sDbnull(autoICD_2mat.MyCode, "");
                        _KcbChandoanKetluan.MotaBenhchinh = Utility.sDbnull(txtTenICD2Mat.Text, "");
                    }
                    _KcbChandoanKetluan.HuongDieutri = Utility.sDbnull(cboHDT.SelectedValue); //.myCode.Trim();
                    _KcbChandoanKetluan.SongayDieutri = (Int16)Utility.DecimaltoDbnull(txtSongaydieutri.Text, 0);
                    _KcbChandoanKetluan.SoNgayhen = (Int16)Utility.DecimaltoDbnull(txtSoNgayHen.Text, 0);
                    _KcbChandoanKetluan.Ketluan = Utility.sDbnull(cboKQDT.SelectedValue);
                    _KcbChandoanKetluan.VitriIcdChinh = Utility.ByteDbnull(opt2mat.Checked ? 0 : (optMP.Checked ? 1 : 2));
                    _KcbChandoanKetluan.Sotxuathuyet = chkSotxuathuyet.Checked;
                    _KcbChandoanKetluan.Taychanmieng = chkTaychanmieng.Checked;
                    _KcbChandoanKetluan.LoiDan = autoLoidan.Text;
                    _KcbChandoanKetluan.XuTri = autoXutri.Text;
                    _KcbChandoanKetluan.ChandoanKemtheo = Utility.sDbnull(txtChanDoanKemTheo.Text);
                    _KcbChandoanKetluan.ChandoanMatphai = Utility.sDbnull(txtTenICDMatPhai.Text, "");
                    _KcbChandoanKetluan.ChandoanMattrai = Utility.sDbnull(txtTenICDMatTrai.Text, "");
                    _KcbChandoanKetluan.TenIcdMatphai = Utility.sDbnull(txtTenICDMatPhai.Text, "");
                    _KcbChandoanKetluan.TenIcdMattrai = Utility.sDbnull(txtTenICDMatTrai.Text, "");
                    _KcbChandoanKetluan.IcdMatphai = Utility.sDbnull(autoICD_matphai.MyCode, "");
                    _KcbChandoanKetluan.IcdMattrai = Utility.sDbnull(autoICD_mattrai.MyCode, "");
                    string sMaICDPHU = GetDanhsachBenhphu(false);//Lấy cho công khám
                    _KcbChandoanKetluan.MabenhPhu = Utility.sDbnull(sMaICDPHU, "");
                    sMaICDPHU = GetDanhsachBenhphu(true);//Lấy cho lượt khám
                    if (objLuotkham != null)
                    {
                        objLuotkham.MabenhPhu = Utility.sDbnull(sMaICDPHU, "");
                        objLuotkham.MabenhChinh = Utility.sDbnull(autoICD_2mat.MyCode, "");
                        objLuotkham.ChandoanKemtheo = Utility.sDbnull(txtChanDoanKemTheo.Text, "");
                        objLuotkham.SongayHen = _KcbChandoanKetluan.SoNgayhen;
                    }

                }
                if (savedType != 1)//Lưu khám sơ bộ hoặc cả 2
                {
                    _KcbChandoanKetluan.NhanXet = Utility.sDbnull(txtNhanxet.Text, "");
                    _KcbChandoanKetluan.Para = Utility.sDbnull(txtPara.Text);
                    _KcbChandoanKetluan.QuaiBi = Utility.Bool2byte(chkQuaibi.Checked);
                    _KcbChandoanKetluan.Nhommau = txtNhommau.Text;
                    _KcbChandoanKetluan.SPO2 = txtSPO2.Text;
                    _KcbChandoanKetluan.Nhietdo = Utility.sDbnull(txtNhietDo.Text);
                    _KcbChandoanKetluan.TrieuchungBandau = txtLydokham.Text;
                    _KcbChandoanKetluan.QuatrinhBenhly = txtQuatrinhbenhly.Text;
                    _KcbChandoanKetluan.TiensuBenh = txtTiensubenh.Text;
                    _KcbChandoanKetluan.TomtatCls = txtCLS.Text;
                    _KcbChandoanKetluan.Huyetap = txtHa.Text;
                    _KcbChandoanKetluan.Mach = txtMach.Text;
                    _KcbChandoanKetluan.KieuChandoan = 0;
                    _KcbChandoanKetluan.Nhiptim = Utility.sDbnull(txtNhipTim.Text);
                    _KcbChandoanKetluan.Nhiptho = Utility.sDbnull(txtNhipTho.Text);
                    _KcbChandoanKetluan.Chieucao = Utility.sDbnull(txtChieucao.Text);
                    _KcbChandoanKetluan.Cannang = Utility.sDbnull(txtCannang.Text);
                    _KcbChandoanKetluan.ChisoIbm = Utility.DecimaltoDbnull(txtBMI.Text, 0);
                    _KcbChandoanKetluan.ThilucMp = Utility.sDbnull(txtthiluc_mp.Text, "");
                    _KcbChandoanKetluan.ThilucMt = Utility.sDbnull(txtthiluc_mt.Text, "");
                    _KcbChandoanKetluan.NhanapMp = Utility.sDbnull(txtnhanap_mp.Text, "");
                    _KcbChandoanKetluan.NhanapMt = Utility.sDbnull(txtnhanap_mt.Text, "");
                    _KcbChandoanKetluan.ChedoDinhduong = Utility.sDbnull(txtCheDoAn.Text, "");

                    _KcbChandoanKetluan.MaChandoan = Utility.sDbnull(txtChanDoan.myCode);
                    _KcbChandoanKetluan.Chandoan = Utility.sDbnull(txtChanDoan.Text);

                    _KcbChandoanKetluan.PhantruocMatphai = Utility.sDbnull(txt_phantruoc_mp.Text, "");
                    _KcbChandoanKetluan.PhantruocMattrai = Utility.sDbnull(txt_phantruoc_mt.Text, "");
                    _KcbChandoanKetluan.DaymatMatphai = Utility.sDbnull(txt_daymat_mp.Text, "");
                    _KcbChandoanKetluan.DaymatMattrai = Utility.sDbnull(txt_daymat_mt.Text, "");
                    _KcbChandoanKetluan.VannhanMatphai = Utility.sDbnull(txt_vannhan_mp.Text, "");
                    _KcbChandoanKetluan.VannhanMattrai = Utility.sDbnull(txt_vannhan_mt.Text, "");
                    _KcbChandoanKetluan.Khammat = Utility.sDbnull(auto_khammat.Text, "");

                }
               
               
               
                if (Utility.Int16Dbnull(txtBacsi.MyID, -1) > 0)
                    _KcbChandoanKetluan.IdBacsikham = Utility.Int16Dbnull(txtBacsi.MyID);
                else
                {
                    _KcbChandoanKetluan.IdBacsikham = globalVariables.gv_intIDNhanvien;
                }
                

                if (objCongkham != null)
                {
                    _KcbChandoanKetluan.IdKhoanoitru = Utility.Int32Dbnull(objCongkham.IdKhoakcb, -1);
                    _KcbChandoanKetluan.IdPhongkham = Utility.Int32Dbnull(objCongkham.IdPhongkham, -1);
                    DmucKhoaphong objDepartment =
                        DmucKhoaphong.FetchByID(Utility.Int32Dbnull(objCongkham.IdPhongkham, -1));
                    if (objDepartment != null)
                    {
                        _KcbChandoanKetluan.TenPhongkham = Utility.sDbnull(objDepartment.TenKhoaphong, "");
                    }
                }
                else
                {
                    _KcbChandoanKetluan.IdKhoanoitru = -1;// globalVariables.idKhoatheoMay;
                    _KcbChandoanKetluan.IdPhongkham = -1;// globalVariables.idKhoatheoMay;
                }
                //_KcbChandoanKetluan.IdKham = Utility.Int32Dbnull(txt_idchidinhphongkham.Text, -1);
                _KcbChandoanKetluan.NgayChandoan = dtpNgaydangky.Value;
                _KcbChandoanKetluan.IdPhieudieutri = -1;
                _KcbChandoanKetluan.Noitru = 0;
                return _KcbChandoanKetluan;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return null;
            }
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của dữ liệu trước khi đóng gói dữ liệu vào Entity
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            Utility.SetMsg(lblMsg, "", false);
           
            
            if (Utility.Int32Dbnull(txtBacsi.MyID, -1) <= 0)
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn bác sĩ khám trước khi kết thúc khám ngoại trú cho Bệnh nhân", true);
                errorProvider1.SetError(txtBacsi, lblMsg.Text);
                txtBacsi.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtPatient_Code.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn Bệnh nhân để thực hiện thăm khám", true);
                errorProvider1.SetError(txtPatient_Code, lblMsg.Text);
                txtPatient_Code.Focus();
                return false;
            }
            if (dtpThoigian_batdau.Value<dtpNgaydangky.Value)
            {
                Utility.ShowMsg("Thời gian bắt đầu khám phải sau thời gian đăng ký. Vui lòng kiểm tra lại");
                dtpThoigian_batdau.Focus();
                return false;
            }
            if (dtpThoigian_batdau.Value > dtpThoigianKetthuc.Value)
            {
                Utility.ShowMsg("Thời gian kết thúc khám phải sau thời gian bắt đầu khám. Vui lòng kiểm tra lại");
                dtpThoigianKetthuc.Focus();
                return false;
            }
            KcbLuotkham _item = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text, 0), m_strMaLuotkham);
            if (_item == null)
            {
                Utility.ShowMsg("Bạn phải chọn Bệnh nhân hoặc bệnh nhân không tồn tại(Có thể do có người xóa trong lúc bạn đang thao tác). Vui lòng bấm tìm kiếm lại", "Thông báo",
                                MessageBoxIcon.Warning);
                errorProvider1.SetError(txtPatient_Code, lblMsg.Text);
                txtPatient_Code.Focus();
                return false;
            }

            if (_item != null && Utility.Int32Dbnull(_item.TrangthaiNoitru, -1) >= 2)
            {
                Utility.ShowMsg(
                    "Bệnh nhân đã được nhập viện điều trị nội trú. Bạn chỉ có thể xem thông tin và không được phép làm các công việc liên quan đến ngoại trú",
                    "Thông báo");

                cmdKetthuckham.Focus();
                return false;
            }
            if (objLuotkham != null &&
                ((Utility.sDbnull(objLuotkham.MatheBhyt).Trim() != "" &&
                  THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THAMKHAM_BATNHAPSONGAYDIEUTRI_BHYT", "0", false) == "1")
                 ||
                 (Utility.sDbnull(objLuotkham.MatheBhyt).Trim() == "" &&
                  THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THAMKHAM_BATNHAPSONGAYDIEUTRI_DV", "0", false) == "1")))
            {
                if (Utility.DecimaltoDbnull(txtSongaydieutri.Text) <= 0)
                {
                    Utility.SetMsg(lblMsg, "Bạn phải nhập ngày điều trị phải lớn hơn 0", true);
                    errorProvider1.SetError(txtSongaydieutri, lblMsg.Text);
                    txtSongaydieutri.Focus();
                    tabDiagInfo.SelectedTab = tabPageChanDoan;
                    return false;
                }
            }

            if (objLuotkham != null && objLuotkham.MaDoituongKcb == "BHYT" && (Utility.DoTrim(autoICD_2mat.MyCode) == "" || Utility.DoTrim(txtTenICD2Mat.Text) == ""))
               // if (objLuotkham != null && objLuotkham.MaDoituongKcb == "BHYT" && (Utility.sDbnull(cbo_icd.SelectedValue) == "-1"))
                {
                Utility.SetMsg(lblMsg, "Bạn cần nhập Mã bệnh chính cho đối tượng BHYT trước khi kết thúc khám", true);
                errorProvider1.SetError(autoICD_2mat, lblMsg.Text);
                tabDiagInfo.SelectedTab = tabPageChanDoan;
                autoICD_2mat.Focus();
                return false;
            }
            if (Utility.sDbnull(cboKQDT.SelectedValue) == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập kết quả khám cho bệnh nhân", true);
                errorProvider1.SetError(cboKQDT, lblMsg.Text);
                tabDiagInfo.SelectedTab = tabPageChanDoan;
                cboKQDT.Focus();
                return false;
            }
            if (Utility.sDbnull(cboHDT.SelectedValue) == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập hướng điều trị cho bệnh nhân", true);
                errorProvider1.SetError(cboHDT, lblMsg.Text);
                tabDiagInfo.SelectedTab = tabPageChanDoan;
                cboHDT.Focus();
                return false;
            }
            if (Utility.sDbnull(cboHDT.SelectedValue).ToUpper() != THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_MAHUONGDIEUTRI_CHUYENVIEN", false).ToUpper() && objLuotkham.TthaiChuyendi == 1)
            {
                Utility.ShowMsg( "Bệnh nhân đã chuyển viện nên bạn muốn chọn hướng điều trị khác thì cần hủy chuyển viện.");
                return false;
            }
            if (Utility.sDbnull(cboHDT.SelectedValue).ToUpper() != THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_MAHUONGDIEUTRI_NOITRU", false).ToUpper() && objLuotkham.TrangthaiNoitru >= 1)
            {
                Utility.ShowMsg("Bệnh nhân đã nhập viện điều trị nội trú nên bạn muốn chọn hướng điều trị khác thì cần hủy nhập viện");
                return false;
            }
            //  = chkDaThucHien.Checked;
            return true;
        }
        bool CanBA(string mabenh)
        {
            string ICD_BA = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_ICD_BENH_AN_NGOAI_TRU", "ALL", true);
            if (ICD_BA == "ALL" || ICD_BA.Split(',').ToList<string>().Contains(mabenh)) return true;
            return false;
        }
        bool ChophepChuyenvien()
        {
            //Kiểm tra xem có đơn thuốc ngoại trú hay không
            string errMsg = "";
            StoredProcedure v_sp = SPs.KcbKiemtradieukiennhapvien(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, errMsg);
            DataSet dsCheck = v_sp.GetDataSet();
            //errMsg = Utility.sDbnull(v_sp.OutputValues[0]);
            //if (Utility.DoTrim(errMsg).Length > 0)
            //{
            //    Utility.ShowMsg(errMsg);
            //    return;
            //}
            //if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THAMKHAM_KEDONTHUOCNGOAITRU_KHONGCHONHAPVIEN", "0", true) == "1")
            //{
            //    if (dsCheck.Tables[0].Rows.Count > 0)
            //    {
            //        Utility.ShowMsg("Hệ thống phát hiện bệnh nhân đã được kê đơn thuốc ngoại trú. Cần thực hiện hủy tất cả các đơn thuốc ngoại trú trước khi thực hiện nhập viện. Mời bạn kiểm tra lại");
            //        return;
            //    }
            //}
            if (dsCheck.Tables[1].Rows.Count > 0)
            {
                Utility.ShowMsg("Hệ thống phát hiện bệnh nhân đã được đăng kí khám tại nhiều phòng khám nhưng chưa kết thúc khám. Cần thực hiện kết thúc khám tại tất cả các phòng trước khi thực hiện chuyển viện. Mời bạn kiểm tra lại");
                return false;
            }
            return true;
        }
        /// <summary>
        /// Thực hiện thao tác Insert,Update,Delete tới CSDL theo m_enAction
        /// </summary>
        private void PerformAction()
        {
            try
            {
                objLuotkham = Utility.getKcbLuotkham(objCongkham.IdBenhnhan, objCongkham.MaLuotkham);
                //Kiểm tra tính hợp lệ của dữ liệu trước khi thêm mới
                cmdKetthuckham.Enabled = false;
                if (!IsValidData())
                {
                    return;
                }
                TimeSpan songaychothuoc = Convert.ToDateTime(objLuotkham.NgayketthucBhyt).Subtract(globalVariables.SysDate);
                int songay = Utility.Int32Dbnull(songaychothuoc.TotalDays);
                if (Utility.Int32Dbnull(songay) <= Utility.Int32Dbnull(txtSongaydieutri.Text) &&
                    objLuotkham.IdDoituongKcb == 2)
                {
                    if (
                        !Utility.AcceptQuestion(
                            string.Format(
                                "Số ngày cho thuốc vượt quá hạn thẻ BHYT của bệnh nhân {0}. \r\n Có đồng ý tiếp tục kết thúc không?",
                                objBenhnhan.TenBenhnhan), "Cảnh Báo", true))
                    {
                        return;
                    }
                }
                if (objLuotkham != null)
                    chkDaThucHien.Visible = chkDaThucHien.Checked = true;
                objCongkham.TrangThai = 1;
                DmucNhanvien objNV = DmucNhanvien.FetchByID(Utility.Int32Dbnull(txtBacsi.MyID, -1));

                DataRow[] arrDr = m_dtDanhsachbenhnhanthamkham.Select("id_kham=" + txtReg_ID.Text);
                if (arrDr.Length > 0)
                {
                    arrDr[0]["trang_thai"] = chkDaThucHien.Checked ? 1 : 0;
                    arrDr[0]["ten_trangthai_congkham"] = "Đã khám";
                }
                objCongkham.IdBacsikham = Utility.Int16Dbnull(txtBacsi.MyID, -1);
                objLuotkham.TrieuChung = txtLydokham.Text;
                //if (objCongkham != null)
                //{
                //    if (!objCongkham.ThoigianKetthuc.HasValue)
                //    {
                //        dtpThoigianKetthuc.Value = globalVariables.SysDate.AddSeconds(5);
                        objCongkham.ThoigianKetthuc = dtpThoigianKetthuc.Value;
                //    }
                //    else
                //    {
                //        objCongkham.ThoigianKetthuc = dtpThoigianKetthuc.Value;
                //    }
                //}
                
                if (!THU_VIEN_CHUNG.IsBaoHiem((byte)objLuotkham.IdLoaidoituongKcb))
                //Đối tượng dịch vụ được khóa ngay sau khi kết thúc khám
                {
                    objLuotkham.NguoiKetthuc = chkDaThucHien.Checked ?(objNV != null ?objNV.UserName: globalVariables.UserName ): "";
                    if (chkDaThucHien.Checked)
                        objLuotkham.NgayKetthuc = globalVariables.SysDate;
                    else
                        objLuotkham.NgayKetthuc = null;
                    objLuotkham.Locked = chkDaThucHien.Checked ? (byte)1 : (byte)0;
                    objLuotkham.TrangthaiNgoaitru = objLuotkham.Locked;
                }
                else
                {
                    objLuotkham.TrangthaiNgoaitru = chkDaThucHien.Checked ? (byte)1 : (byte)0;
                    objLuotkham.NguoiKetthuc = chkDaThucHien.Checked ? (objNV != null ? objNV.UserName : globalVariables.UserName) : "";
                    objLuotkham.NgayKetthuc = globalVariables.SysDate;
                }
                objLuotkham.SongayDieutri = Utility.Int32Dbnull(txtSongaydieutri.Text, 0);
                objLuotkham.HuongDieutri = Utility.sDbnull(cboHDT.SelectedValue);
                objLuotkham.KetLuan = Utility.sDbnull(cboKQDT.SelectedValue);
                //KcbChandoanKetluan objCompare = new Select().From(KcbChandoanKetluan.Schema)
                //                     .Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(objCongkham.IdKham).ExecuteSingle<KcbChandoanKetluan>();
                TaoDulieuChandoanKetluan(1);
                //if (objCompare != null &&
                //    ((Utility.sDbnull(_KcbChandoanKetluan.Mach).Length <= 0 && Utility.sDbnull(objCompare.Mach).Length > 0)
                //    || (Utility.sDbnull(_KcbChandoanKetluan.Nhietdo).Length <= 0 && Utility.sDbnull(objCompare.Nhietdo).Length > 0)
                //    || (Utility.sDbnull(_KcbChandoanKetluan.Huyetap).Length <= 0 && Utility.sDbnull(objCompare.Huyetap).Length > 0)
                //    || (Utility.sDbnull(_KcbChandoanKetluan.Nhiptho).Length <= 0 && Utility.sDbnull(objCompare.Nhiptho).Length > 0)
                //    || (Utility.sDbnull(_KcbChandoanKetluan.Cannang).Length <= 0 && Utility.sDbnull(objCompare.Cannang).Length > 0)
                //    || (Utility.sDbnull(_KcbChandoanKetluan.Chieucao).Length <= 0 && Utility.sDbnull(objCompare.Chieucao).Length > 0)
                //    || (Utility.sDbnull(_KcbChandoanKetluan.Nhommau).Length <= 0 && Utility.sDbnull(objCompare.Nhommau).Length > 0)
                //    || (Utility.sDbnull(_KcbChandoanKetluan.Chandoan).Length <= 0 && Utility.sDbnull(objCompare.Chandoan).Length > 0)
                //    || (Utility.sDbnull(_KcbChandoanKetluan.ChandoanKemtheo).Length <= 0 && Utility.sDbnull(objCompare.ChandoanKemtheo).Length > 0)
                //    || (Utility.sDbnull(_KcbChandoanKetluan.NhanXet).Length <= 0 && Utility.sDbnull(objCompare.NhanXet).Length > 0)
                //    || (Utility.sDbnull(_KcbChandoanKetluan.QuatrinhBenhly).Length <= 0 && Utility.sDbnull(objCompare.QuatrinhBenhly).Length > 0)
                //     || (Utility.sDbnull(_KcbChandoanKetluan.TiensuBenh).Length <= 0 && Utility.sDbnull(objCompare.TiensuBenh).Length > 0))
                //    )
                //{
                //    if (!Utility.AcceptQuestion("Hệ thống phát hiện người bệnh đã có dữ liệu khám sơ bộ(mạch, nhiệt độ, huyết áp, nhịp thở, Cân nặng, Chiều cao,....) khác so với dữ liệu bạn đang thao tác.\r\nNhấn No để tạm dừng việc lưu, sau đó thực hiện theo các bước:\r\nBước 1: Chọn lại người bệnh trên lưới để nạp lại thông tin khám sơ bộ.\r\nBước 2: Chỉnh sửa thông tin khám sơ bộ và lưu lại.\r\nNhấn Yes để chắc chắn tiếp tục lưu thông tin khám sơ bộ trên máy của bạn", "Xác nhận", true))
                //    {
                //        return;
                //    }
                //}

                ActionResult actionResult =
                    _KCB_THAMKHAM.UpdateExamInfo(
                        _KcbChandoanKetluan, objCongkham, objLuotkham);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        _KcbChandoanKetluan = new Select().From(KcbChandoanKetluan.Schema)
                                    .Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(objCongkham.IdKham).
                                    ExecuteSingle
                                    <KcbChandoanKetluan>();
                        chkDaThucHien.Visible = chkDaThucHien.Checked = objLuotkham.TrangthaiNgoaitru >= 1;
                        IEnumerable<GridEXRow> query = from kham in grdList.GetDataRows()
                                                       where
                                                           kham.RowType == RowType.Record &&
                                                           Utility.Int32Dbnull(
                                                               kham.Cells[KcbDangkyKcb.Columns.IdKham].Value) ==
                                                           Utility.Int32Dbnull(txt_idchidinhphongkham.Text)
                                                       select kham;
                        if (query.Count() > 0)
                        {
                            GridEXRow gridExRow = query.FirstOrDefault();
                            //gridExRow.BeginEdit();
                            gridExRow.Cells[KcbDangkyKcb.Columns.TrangThai].Value = (byte?)(chkDaThucHien.Checked ? 1 : 0);
                            gridExRow.Cells[KcbLuotkham.Columns.NguoiKetthuc].Value = objNV != null ? objNV.UserName : globalVariables.UserName;
                            gridExRow.Cells[KcbLuotkham.Columns.NgayKetthuc].Value = globalVariables.SysDate;
                            //gridExRow.EndEdit();
                            grdList.UpdateData();
                            Utility.GotoNewRowJanus(grdList, KcbDangkyKcb.Columns.IdKham,
                                                    Utility.sDbnull(txt_idchidinhphongkham.Text));
                        }
                        //Tạm thời comment bỏ 2 dòng dưới để luôn hiển thị 2 nút này(190803)
                        //cmdInTTDieuTri.Visible = objLuotkham.TrangthaiNgoaitru >= 1;
                        //cmdInphieuhen.Visible = objLuotkham.TrangthaiNgoaitru >= 1;
                        lblBenhan.Visible = cmdbenhan.Visible = txtbenhan.Visible = HienthiLamBenhAn();
                        //   lblBenhan.Visible = cboChonBenhAn.Visible;


                        //Tự động ẩn BN về tab đã khám. REM 230523
                        //int Status = radChuaKham.Checked ? 0 : 1;
                        //if (Status == 0)
                        //{
                        //    m_dtDanhsachbenhnhanthamkham.DefaultView.RowFilter = "1=1";
                        //    m_dtDanhsachbenhnhanthamkham.DefaultView.RowFilter = "trang_thai=" + Status;
                        //}
                        var objStaff = (from p in globalVariables.gv_dtDmucNhanvien.AsEnumerable()
                                        where p[DmucNhanvien.Columns.UserName].Equals(objLuotkham.NguoiKetthuc)
                                        select p).FirstOrDefault();
                        //var objStaff =
                        //    new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.UserNameColumn).IsEqualTo(
                        //        Utility.sDbnull(objLuotkham.NguoiKetthuc, "")).ExecuteSingle<DmucNhanvien>();
                        string tenNhanvien = objLuotkham.NguoiKetthuc;
                        if (objStaff != null)
                            tenNhanvien = Utility.sDbnull(objStaff["ten_nhanvien"], "");
                        //Tự động bật tính năng nhập viện nội trú nếu hướng điều trị chọn là Nội trú và Bệnh nhân chưa nhập viện
                        
                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU", "0", false) == "1" && objCongkham != null && objLuotkham != null && objCongkham.TrangThai == 1 && !Utility.Byte2Bool(objCongkham.Noitru) &&
                           THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_MAHUONGDIEUTRI_NOITRU", false).ToUpper().Split(',').ToList<string>().Contains(Utility.sDbnull(cboHDT.SelectedValue).ToUpper())  && objLuotkham.TrangthaiNoitru <= 0)
                        {
                            cmdNhapVien_Click(cmdNhapVien, new EventArgs());
                        }
                        else if (objCongkham != null && objLuotkham != null && objCongkham.TrangThai == 1 && !Utility.Byte2Bool(objCongkham.Noitru) &&
                          Utility.sDbnull(cboHDT.SelectedValue).ToUpper() == THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_MAHUONGDIEUTRI_CHUYENVIEN", false).ToUpper() && !Utility.Byte2Bool( objLuotkham.TthaiChuyendi))
                        {
                            if (ChophepChuyenvien())
                            {
                                frm_chuyenvien _chuyenvien = new frm_chuyenvien();
                                _chuyenvien.idbacsikham =Utility.Int32Dbnull( objCongkham.IdBacsikham,-1);
                                _chuyenvien.ucThongtinnguoibenh1.objLuotkham = this.objLuotkham;
                                _chuyenvien.ucThongtinnguoibenh1.txtMaluotkham.Text = objLuotkham.MaLuotkham;
                                _chuyenvien.ucThongtinnguoibenh1.Refresh();
                                _chuyenvien.ShowDialog();
                                if (!_chuyenvien.m_blnCancel)
                                    objLuotkham.TthaiChuyendi = 1;
                            }
                        }
                        Utility.SetMsg(lblTrangthainoitru, Utility.Laythongtintrangthainguoibenh(objLuotkham), false);
                        Utility.Log(Name, globalVariables.UserName, string.Format(
                                               "Bệnh nhân có mã lần khám {0} và mã bệnh nhân {1} được kết thúc khám user {2} với tên bác sĩ khám {3} ",
                                               objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, globalVariables.UserName,txtBacsi.Text),
                                           newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                        VisibleLockButton();
                        if (!cmdUnlock.Enabled)
                            toolTip1.SetToolTip(cmdUnlock,
                                                "Bạn không có quyền mở khóa Bệnh nhân này. Đề nghị liên hệ " +
                                                tenNhanvien + "(" + objLuotkham.NguoiKetthuc +
                                                " - Là người khóa BN này) để được họ mở khóa. Hoặc liên hệ Quản trị hệ thống");
                        else
                            toolTip1.SetToolTip(cmdUnlock,
                                                "Nhấn vào đây để mở khóa cho bệnh nhân đang chọn(Phím tắt Ctrl+U)." +
                                                " Điều kiện là chỉ mở khóa đối với đối tượng Dịch vụ. " +
                                                "Muốn mở khóa đối tượng BHYT thì cần liên lạc với bộ phận thanh toán hủy in phôi BHYT");
                        break;

                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá lưu thông tin ", "Thông báo lỗi", MessageBoxIcon.Error);
                        break;
                }
                ModifyCommmands();
                cmdNhapVien.Enabled = objCongkham.TrangThai == 1;
                cmdHuyNhapVien.Enabled = objLuotkham.TrangthaiNoitru >= 1;
                cmdNhapVien.Tag = objLuotkham.TrangthaiNoitru == 0 ? "0" : "1";
                cmdNhapVien.Text = objLuotkham.TrangthaiNoitru == 0 ? "Nhập viện" : "Cập nhật";
                //Thread.Sleep(500);
                cmdKetthuckham.Enabled = true;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                
            }
            finally
            {
                cmdKetthuckham.Enabled = true;
            }
        }

        #endregion

        #region VTTH

        private void mnuDelVTTH_Click(object sender, EventArgs e)
        {
            if (!IsValidDeleteSelectedVTTH()) return;
            PerformActionDeleteSelectedVTTH();
            ModifyCommmands();
        }

        private void cmdXoaphieuVT_Click(object sender, EventArgs e)
        {
            if (!CheckPatientSelected()) return;
            if (!IsValidVTTH_delete()) return;
            PerformActionDeleteVTTH();
            ModifyCommmands();
        }

        private void cmdInphieuVT_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdVTTH)) return;
                int Pres_ID = Utility.Int32Dbnull(grdVTTH.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                PrintPres(Pres_ID, "PHIẾU VẬT TƯ NGOÀI GÓI");
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
           
        }

        private void cmdSuaphieuVT_Click(object sender, EventArgs e)
        {
            if (!CheckPatientSelected()) return;
            if (!cmdSuaphieuVT.Enabled) return;
            if (Utility.Coquyen("quyen_suadonthuoc") ||
                    Utility.sDbnull(grdVTTH.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "") ==
                    globalVariables.UserName)
            {
                SuaphieuVattu();
            }
            else
            {
                Utility.ShowMsg(
                    "Phiếu VTTH đang chọn sửa được tạo bởi bác sĩ khác và bạn không được gán quyền sửa(quyen_suadonthuoc). Mời bạn thực hiện công việc khác");
                return;
            }
        }

        private void cmdThemphieuVT_Click(object sender, EventArgs e)
        {
            if (!CheckPatientSelected()) return;
            if (!cmdThemphieuVT.Enabled) return;
            //if (Utility.Coquyen("quyen_suadonthuoc") || Utility.Int32Dbnull(objCongkham.IdBacsikham, -1) <= 0 ||
            //    objCongkham.IdBacsikham == globalVariables.gv_intIDNhanvien)
            //{
            //}
            //else
            //{
            //    Utility.ShowMsg(
            //        string.Format(
            //            "Bệnh nhân này đã được khám bởi Bác sĩ khác nên bạn không được phép thêm phiếu vật tư cho Bệnh nhân"));
            //    return;
            //}

            ThemphieuVattu();
        }

        private void ThemphieuVattu()
        {
            try
            {
                // KeDonThuocTheoDoiTuong();
                var frm = new frm_KCB_KE_DONTHUOC("VT");
                frm._OnSaveMe += frm__OnSaveMe;
                frm.em_Action = action.Insert;
                frm.objLuotkham = objLuotkham;

                frm._KcbCDKL = _KcbChandoanKetluan;
                frm.IdBacsikham = _KcbChandoanKetluan != null ? _KcbChandoanKetluan.IdBacsikham : Utility.Int32Dbnull(txtBacsi.MyID, -1);
                frm._MabenhChinh = autoICD_2mat.MyCode;
                frm._Chandoan = txtChanDoan.Text;
                frm.DtIcd = dt_ICD;
                frm.dt_ICD_PHU = dt_ICD_PHU;
                frm.id_kham = Utility.Int32Dbnull(txtExam_ID.Text);
                frm.objCongkham = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txtReg_ID.Text));
                frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                frm.txtPatientID.Text = Utility.sDbnull(objBenhnhan.IdBenhnhan, "-1");
                frm.txtSoDT.Text = Utility.sDbnull(objBenhnhan.DienThoai, "");
                frm.txtPatientName.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan, "");
                frm.txtYearBirth.Text = Utility.sDbnull(objBenhnhan.NamSinh, "");
                frm.txtSex.Text = Utility.sDbnull(objBenhnhan.GioiTinh, "");
                frm.txtPres_ID.Text = "-1";
                frm.dtNgayKhamLai.MinDate = dtpNgaydangky.Value;
                frm._ngayhenkhamlai = dtpNgaydangky.Value.ToString("yyMMdd") == dtpNgayHen.Value.ToString("yyMMdd") ? "" : dtpNgayHen.Text;
                frm.noitru = 0;
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();

                if (!frm.m_blnCancel)
                {
                    autoICD_2mat.SetCode( frm._MabenhChinh);
                    txtChanDoan._Text = frm._Chandoan;
                    dt_ICD_PHU = frm.dt_ICD_PHU;
                    //if (frm._KcbCDKL != null) _KcbChandoanKetluan = frm._KcbCDKL;
                    FillThongtinHoibenhVaChandoan();
                    Laythongtinchidinhngoaitru();
                    Utility.GotoNewRowJanus(grdVTTH, KcbDonthuoc.Columns.IdDonthuoc,
                                            Utility.sDbnull(frm.txtPres_ID.Text));
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
                ModifyCommmands();
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

                KcbLuotkham objPatientExam = objLuotkham;
                if (objPatientExam != null)
                {
                    int Pres_ID = Utility.Int32Dbnull(grdVTTH.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                    dtTemp = SPs.KcbDonthuocChitietKiemtraDieuchinhtaiquay(Pres_ID).GetDataSet().Tables[0];
                    if (dtTemp.Rows.Count > 0)
                    {
                        Utility.ShowMsg("Đơn VT này đã được quầy chỉnh sửa số lượng nên bạn không thể chỉnh sửa. Muốn sửa đơn VT, cần yêu cầu quầy thuốc chỉnh số lượng sửa về 0");
                        return;
                    }
                    if (Donthuoc_DangXacnhan(Pres_ID))
                    {
                        Utility.ShowMsg(
                            "Phiếu vật tư này đang ở trạng thái đã duyệt cho Bệnh nhân nên không thể chỉnh sửa. Đề nghị quay lại hỏi bộ phận cấp phát vật tư tại phòng vật tư");
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
                        var frm = new frm_KCB_KE_DONTHUOC("VT");
                        frm._OnSaveMe += frm__OnSaveMe;
                        frm.em_Action = action.Update;

                        frm._KcbCDKL = _KcbChandoanKetluan;
                        frm.IdBacsikham = _KcbChandoanKetluan != null ? _KcbChandoanKetluan.IdBacsikham : Utility.Int32Dbnull(txtBacsi.MyID, -1);
                        frm._MabenhChinh = autoICD_2mat.MyCode;
                        frm._Chandoan = txtChanDoan.Text;
                        frm.DtIcd = dt_ICD;
                        frm.dt_ICD_PHU = dt_ICD_PHU;
                        frm.noitru = 0;
                        frm.objLuotkham = objLuotkham;
                        frm.id_kham = Utility.Int32Dbnull(txtExam_ID.Text);
                        frm.objCongkham = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txtReg_ID.Text));
                        frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                        frm.txtPatientID.Text = Utility.sDbnull(objBenhnhan.IdBenhnhan, "-1");
                        frm.txtSoDT.Text = Utility.sDbnull(objBenhnhan.DienThoai, "");
                        frm.txtPatientName.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan, "");
                        frm.txtYearBirth.Text = Utility.sDbnull(objBenhnhan.NamSinh, "");
                        frm.txtSex.Text = Utility.sDbnull(objBenhnhan.GioiTinh, "");
                        frm.txtPres_ID.Text = Utility.sDbnull(objPrescription.IdDonthuoc);
                        frm.dtNgayKhamLai.MinDate = dtpNgaydangky.Value;
                        frm._ngayhenkhamlai = dtpNgaydangky.Value.ToString("yyMMdd") == dtpNgayHen.Value.ToString("yyMMdd") ? "" : dtpNgayHen.Text;

                        frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                        frm.ShowDialog();
                        if (!frm.m_blnCancel)
                        {
                            autoICD_2mat.SetCode( frm._MabenhChinh);
                            txtChanDoan._Text = frm._Chandoan;
                            dt_ICD_PHU = frm.dt_ICD_PHU;
                            //if (frm._KcbCDKL != null) _KcbChandoanKetluan = frm._KcbCDKL;
                            FillThongtinHoibenhVaChandoan();
                            Laythongtinchidinhngoaitru();
                            Utility.GotoNewRowJanus(grdVTTH, KcbDonthuocChitiet.Columns.IdDonthuoc,
                                                    Utility.sDbnull(frm.txtPres_ID.Text));
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                ModifyCommmands();
            }
        }

        private void PerformActionDeleteVTTH()
        {
            string s = "";
            int Pres_ID = Utility.Int32Dbnull(Utility.getCellValuefromGridEXRow(RowVTTH, KcbDonthuocChitiet.Columns.IdDonthuoc));
            if (Donthuoc_DangXacnhan(Pres_ID))
            {
                Utility.ShowMsg(
                    "Phiếu vật tư này đang ở trạng thái đã duyệt cho Bệnh nhân nên không thể chỉnh sửa. Đề nghị quay lại hỏi bộ phận cấp phát vật tư tại phòng vật tư");
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
            var lstIdchitiet = new List<int>();
            if (grdVTTH.GetCheckedRows().Count() <= 0 && RowVTTH != null)
            {
                try
                {
                    RowVTTH.BeginEdit();
                    RowVTTH.IsChecked = true;
                    RowVTTH.EndEdit();
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất 1 chi tiết vật tư tiêu hao để xóa");
                    return;
                }
            }
            foreach (GridEXRow gridExRow in grdVTTH.GetCheckedRows())
            {
                string stempt = "";
                int id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, 0m);
                decimal dongia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, 0m);
                int IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value, 0m);
                List<int> _temp = GetIdChitietVTTH(IdDonthuoc, id_thuoc, dongia, ref stempt);
                s += "," + stempt;
                lstIdchitiet.AddRange(_temp);
                //gridExRow.Delete();
                //grdVTTH.UpdateData();
            }
            if (lstIdchitiet.Count <= 0) return;
            _KCB_KEDONTHUOC.XoaChitietDonthuoc(s);
            XoaVTTHKhoiBangDulieu(lstIdchitiet);
            m_dtVTTH.AcceptChanges();
        }

        private void XoaVTTHKhoiBangDulieu(List<int> lstIdChitietDonthuoc)
        {
            try
            {
                DataRow[] p = (from q in m_dtVTTH.Select("1=1").AsEnumerable()
                               where
                                   lstIdChitietDonthuoc.Contains(
                                       Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]))
                               select q).ToArray<DataRow>();
                for (int i = 0; i <= p.Length - 1; i++)
                    m_dtVTTH.Rows.Remove(p[i]);
                m_dtVTTH.AcceptChanges();
            }
            catch
            {
            }
        }

        private List<int> GetIdChitietVTTH(int IdDonthuoc, int id_thuoc, decimal don_gia, ref string s)
        {
            var _data = from p in m_dtVTTH.AsEnumerable()
                        where Utility.Int32Dbnull(p[KcbDonthuocChitiet.Columns.IdDonthuoc]) == IdDonthuoc
                        && Utility.Int32Dbnull(p[KcbDonthuocChitiet.Columns.IdThuoc]) == id_thuoc
                        && Utility.DecimaltoDbnull(p[KcbDonthuocChitiet.Columns.DonGia]) == don_gia
                        select p;
            if (_data.Any())
            {
                DataRow[] arrDr = _data.ToArray<DataRow>();
                IEnumerable<string> p1 = (from q in arrDr.AsEnumerable()
                                          select Utility.sDbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).
                    Distinct();
                s = string.Join(",", p1.ToArray());
                IEnumerable<int> p = (from q in arrDr.AsEnumerable()
                                      select Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).
                    Distinct();
                return p.ToList();
            }
            return new List<int>();
        }

        private bool IsValidVTTH_delete()
        {
            bool b_Cancel = false;
            if (chkShowGroupVTTH.Checked)
            {
                Utility.ShowMsg("Bạn cần bỏ check mục Nhóm ID vật tư kho khi thực hiện thao tác xóa");
                grdVTTH.UnCheckAllRecords();
                chkShowGroupVTTH.Focus();
                return false;
            }
            if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa các vật tư đang chọn hay không?", "Xác nhận xóa", true)) return false;
            if ( RowVTTH==null)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện việc xóa thông tin Vật tư tiêu hao ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdVTTH.Focus();
                return false;
            }
            if (grdVTTH.GetCheckedRows().Count() <= 0 && RowVTTH != null)//Cho chắc ăn
            {
                try
                {
                    RowVTTH.BeginEdit();
                    RowVTTH.IsChecked = true;
                    RowVTTH.EndEdit();
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất 1 chi tiết VTTH để xóa");
                    return false;
                }

            }
            foreach (GridEXRow gridExRow in grdVTTH.GetCheckedRows())
            {
                if (Utility.Coquyen("quyen_suadonthuoc") || globalVariables.IsAdmin||
                    Utility.sDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "") ==
                    globalVariables.UserName)
                {
                }
                else
                {
                    Utility.ShowMsg(
                        "Trong các VTTH bạn chọn xóa, có một số VTTH được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các VTTH do chính bạn kê để thực hiện xóa");
                    return false;
                }
            }
            KcbLuotkham _item = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text, 0), m_strMaLuotkham);
            if (_item == null)
            {
                Utility.ShowMsg("Bạn phải chọn Bệnh nhân hoặc bệnh nhân không tồn tại!", "Thông báo",
                                MessageBoxIcon.Warning);
                txtPatient_Code.Focus();
                return false;
            }

            if (_item != null && Utility.Int32Dbnull(_item.TrangthaiNoitru, -1) >= 1)
            {
                Utility.ShowMsg(
                    "Bệnh nhân đã được điều trị nội trú. Bạn chỉ có thể xem thông tin và không được phép làm các công việc liên quan đến ngoại trú",
                    "Thông báo");
                cmdKetthuckham.Focus();
                return false;
            }
            KcbDonthuocChitiet objKcbDonthuocChitiet = null;
            foreach (GridEXRow gridExRow in grdVTTH.GetCheckedRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    int v_intIDDonthuoc =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                    int v_intIDThuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1);
                    objKcbDonthuocChitiet = new Select().From(KcbDonthuocChitiet.Schema)
                        .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(v_intIDDonthuoc)
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

        private bool IsValidDeleteSelectedVTTH()
        {
            bool b_Cancel = false;
            if (chkShowGroupVTTH.Checked)
            {
                Utility.ShowMsg("Bạn cần bỏ check mục Nhóm ID vật tư kho khi thực hiện thao tác xóa");
                grdVTTH.UnCheckAllRecords();
                chkShowGroupVTTH.Focus();
                return false;
            }
            if (grdVTTH.RowCount <= 0 || grdVTTH.CurrentRow.RowType != RowType.Record)
            {
                Utility.ShowMsg("Bạn phải chọn một VTTH để xóa ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdVTTH.Focus();
                return false;
            }
            if (Utility.Coquyen("quyen_suadonthuoc") ||
                Utility.sDbnull(grdVTTH.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "") ==
                globalVariables.UserName)
            {
            }
            else
            {
                Utility.ShowMsg(
                    "VTTH đang chọn xóa được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các VTTH do chính bạn kê để thực hiện xóa");
                return false;
            }
            KcbLuotkham _item = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text, 0), m_strMaLuotkham);
            if (_item == null)
            {
                Utility.ShowMsg("Bạn phải chọn Bệnh nhân hoặc bệnh nhân không tồn tại!", "Thông báo",
                                MessageBoxIcon.Warning);
                txtPatient_Code.Focus();
                return false;
            }

            if (_item != null && Utility.Int32Dbnull(_item.TrangthaiNoitru, -1) >= 1)
            {
                Utility.ShowMsg(
                    "Bệnh nhân đã được điều trị nội trú. Bạn chỉ có thể xem thông tin và không được phép làm các công việc liên quan đến ngoại trú",
                    "Thông báo");
                cmdKetthuckham.Focus();
                return false;
            }
            if (grdVTTH.CurrentRow.RowType == RowType.Record)
            {
                int v_intIDDonthuoc =
                    Utility.Int32Dbnull(grdVTTH.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                                        -1);
                int v_intIDThuoc =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1);
                SqlQuery sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(v_intIDDonthuoc)
                    .And(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                }
            }

            if (b_Cancel)
            {
                Utility.ShowMsg("Vật tư tiêu hao đang chọn xóa đã được thanh toán, Bạn không thể xóa thông tin được ",
                                "Thông báo",
                                MessageBoxIcon.Warning);
                grdVTTH.Focus();
                return false;
            }
            if (grdVTTH.CurrentRow.RowType == RowType.Record)
            {
                int v_intIDDonthuoc =
                    Utility.Int32Dbnull(grdVTTH.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                                        -1);
                int v_intIDThuoc =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1);
                SqlQuery sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(v_intIDDonthuoc)
                    .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                }
            }

            if (b_Cancel)
            {
                Utility.ShowMsg(
                    "Vật tư tiêu hao đang chọn xóa đã được được xác nhận nên bạn không thể xóa thông tin được ",
                    "Thông báo",
                    MessageBoxIcon.Warning);
                grdVTTH.Focus();
                return false;
            }
            return true;
        }

        private void PerformActionDeleteSelectedVTTH()
        {
            try
            {
                int Pres_ID =
                    Utility.Int32Dbnull(grdVTTH.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value,
                                        -1);
                int v_intIDDonthuoc =
                    Utility.Int32Dbnull(grdVTTH.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                                        -1);
                int v_intIDThuoc =
                    Utility.Int32Dbnull(grdVTTH.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value,
                                        -1);
                _KCB_KEDONTHUOC.XoaChitietDonthuoc(v_intIDDonthuoc);
                grdVTTH.CurrentRow.Delete();
                grdVTTH.UpdateData();
                m_dtVTTH.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message + "-->" +
                                "Bạn nên dùng chức năng xóa thuốc bằng cách chọn thuốc và sử dụng nút xóa thuốc");
            }
        }

        #endregion

        private void frm_KCB_THAMKHAM_V2_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_blnCancel = false;
        }

        private void txtNhanxet_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmdSave_Click_1(object sender, EventArgs e)
        {

        }
        void CalculateIBM()
        {
            if (Utility.DecimaltoDbnull(txtChieucao.Text, 0) > 0 && Utility.DecimaltoDbnull(txtChieucao.Text, 0) > 0)
            {
                Decimal IBM = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal( txtCannang.Text), 0) / ((Utility.DecimaltoDbnull(txtChieucao.Text, 0) / 100) *
                                   (Utility.DecimaltoDbnull(txtChieucao.Text, 0) / 100));
                txtBMI.Text = String.Format("{0:0.##}", IBM);
            }
        }
        
        private void txtChieucao_Leave(object sender, EventArgs e)
        {
            CalculateIBM();
        }

        private void txtCannang_Leave(object sender, EventArgs e)
        {
            CalculateIBM();
        }

        private void cmdphieupttt_Click(object sender, EventArgs e)
        {
            long idchitietchidinh = Utility.Int64Dbnull(
                grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value);
            if (idchitietchidinh > 0)
            {
                //var frm = new FrmPhauThuatThuThuat(idchitietchidinh);
                //frm.ShowDialog();
            }
            
        }

        private void cmdgiaychapnhanpttt_Click(object sender, EventArgs e)
        {

        }

        private void txtbenhan__OnSelectionChanged()
        {
            switch (Utility.sDbnull(txtbenhan.MyID, ""))
            {
                case "BAT":
                    BenhAnThuong();
                    break;
                case "SPK":
                    BenhAnSanKhoa();
                    break;
            }
        }

        private void cmdbenhan_Click(object sender, EventArgs e)
        {
            switch (Utility.sDbnull(txtbenhan.MyID, ""))
            {
                case "BAT":
                    BenhAnThuong();
                    break;
                case "SPK":
                    BenhAnSanKhoa();
                    break;
            }
        }
        
        void LoadAttachedFiles()
        {
            try
            {
                VNS.HIS.UI.Forms.HinhAnh.Util.ReleaseControlMemory(pnlAttachedFiles, "3");
                pnlAttachedFiles.Controls.Clear();
                int idChidinh = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                DataTable dtAttachedFiles = new Select().From(TblFiledinhkem.Schema).Where(TblFiledinhkem.Columns.IdCongkham).IsEqualTo(objCongkham.IdKham).ExecuteDataSet().Tables[0];
                foreach (DataRow dr in dtAttachedFiles.Rows)
                {
                    Label _file = new Label();
                    _file.ContextMenuStrip = ctxDelFile;
                    _file.AutoSize = true;
                    _file.Font = lblSample.Font;
                    _file.ForeColor = lblSample.ForeColor;
                    _file.Text = dr[TblFiledinhkem.Columns.Id].ToString()+"-"+ dr[TblFiledinhkem.Columns.FileName].ToString();
                    _file.Tag = dr[TblFiledinhkem.Columns.FileData];
                    _file.Click += _file_Click;
                    pnlAttachedFiles.Controls.Add(_file);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());

            }
           
        }

        void _file_Click(object sender, EventArgs e)
        {
            try
            {
                Label obj = sender as Label;
                byte[] fileData = obj.Tag as byte[];
                //Save to temp folder
                string fileName = Application.StartupPath + @"\temppdf\" + obj.Text;
                Utility.CreateFolder(fileName);
                if (!File.Exists(fileName))
                    File.WriteAllBytes(fileName, fileData);
                System.Diagnostics.Process.Start(fileName);
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
           
        }
        private void cmdFileAttach_Click(object sender, EventArgs e)
        {
            int idChidinh = -1;
          
            string MaChidinh = "";
            if(Utility.isValidGrid(grdAssignDetail))
            {
                idChidinh= Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                MaChidinh= Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhcl.Columns.MaChidinh), "xxx");
            }    
          
            OpenFileDialog _openfile = new OpenFileDialog();
            _openfile.Multiselect=true;
            if (_openfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (_openfile.FileNames.Count() >= 2)
                {
                    if (!Utility.AcceptQuestion(string.Format("Bạn đang chọn nhiều ({0}) file kết quả cho phiếu chỉ định này. Bạn có chắc chắn?", _openfile.FileNames.Count().ToString()), "Cảnh báo", true))
                    {
                        return;
                    }
                }
                foreach (string sfile in _openfile.FileNames)
                {
                    if (sfile.Length > 255)
                    {
                        Utility.ShowMsg(string.Format("File {0} không hợp lệ vì tên file dài quá 255 kí tự. Đề nghị đổi lại tên file và thực hiện đính kèm lại", sfile));
                        continue;
                    }
                    byte[] file;
                    using (var stream = new FileStream(sfile, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            file = reader.ReadBytes((int)stream.Length);
                        }
                    }

                    //TblFiledinhkem _file = new Select().From(TblFiledinhkem.Schema).Where(TblFiledinhkem.Columns.IdChidinh).IsEqualTo(idChidinh).ExecuteSingle<TblFiledinhkem>();
                    //if (_file == null)
                    //{
                    TblFiledinhkem _file = new TblFiledinhkem();
                    _file.NguoiTao = globalVariables.UserName;
                    _file.NgayTao = globalVariables.SysDate;
                    _file.IsNew = true;
                    _file.IdBenhnhan = objCongkham.IdBenhnhan;
                    _file.MaLuotkham = objCongkham.MaLuotkham;
                    _file.IdCongkham = objCongkham.IdKham;
                    //}
                    //else
                    //{
                    //    _file.IsNew = false;
                    //    _file.MarkOld();
                    //}
                    _file.FileName = Path.GetFileName(sfile);
                    _file.IdChidinh = idChidinh;
                    _file.FileData = file;
                    _file.Save();
                   
                }
                LoadAttachedFiles();
            }
            
        }

        private void mnuDelFile_Click(object sender, EventArgs e)
        {
            // Try to cast the sender to a MenuItem
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    Label sourceControl = owner.SourceControl as Label;
                    int id = Utility.Int32Dbnull(sourceControl.Text.Split('-')[0], 0);
                    if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa file đính kèm vừa chọn?", "Xác nhận xóa", true))
                    {
                        new Delete().From(TblFiledinhkem.Schema).Where(TblFiledinhkem.Columns.Id).IsEqualTo(id).Execute();
                        LoadAttachedFiles();
                    }
                }

                // Get the control that is displaying this context menu
                
            }
        }

        private void dtpNgayHen_ValueChanged(object sender, EventArgs e)
        {
            txtSongaydieutri.Text = txtSoNgayHen.Text = (dtpNgayHen.Value.Date - dtpNgaydangky.Value.Date).Days.ToString();
        }

        private void mnuNhapKQXN_Click(object sender, EventArgs e)
        {
            frm_nhapketquaXN _nhapketquaXN = new frm_nhapketquaXN();
             int idChitietchidinh =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietchidinh), 0);
                string maChidinh =
                    Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaChidinh),
                                    "XN");
                _nhapketquaXN.SetParams(objLuotkham.MaLuotkham, maChidinh, idChitietchidinh);
                _nhapketquaXN.ShowDialog();
        }

        private void cmdInKQXN_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null || objCongkham == null) return;
            frm_chonXN_Print _f = new frm_chonXN_Print(objLuotkham.MaLuotkham, Utility.GetValueFromGridColumn(grdAssignDetail, "ma_chidinh"));
            _f.ShowDialog();
           // InKetQua();
        }
        private void InKetQua()
        {
            try
            {

                this.Cursor = Cursors.WaitCursor;
                if (cboLaserPrinters.SelectedIndex < 0 || cboLaserPrinters.Text.TrimEnd().TrimStart() == "")
                {
                    Utility.ShowMsg("Bạn cần chọn máy in trước khi thực hiện in kết quả");
                    cboLaserPrinters.Focus();
                    return;
                }
                int idChitietchidinh =Utility.Int32Dbnull( Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietchidinh), 0);
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();
                DataTable dtData = SPs.HinhanhLaydulieuinKQChandoan(idChitietchidinh).GetDataSet().Tables[0];
                try
                {
                    if (dtData.Columns.Contains("PhongThucHien"))
                        dtData.Columns.Remove("PhongThucHien");
                }
                catch (Exception)
                {

                }
                dtData.Columns.Add(new DataColumn()
                {
                    ColumnName = "PhongThucHien",
                    DataType = typeof(String),
                    DefaultValue = PropertyLib._HinhAnhProperties.Khoaphongthuchien
                });
                if (dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy bản ghi nào", "Thông báo");
                    return;
                }
                if (!dtData.Columns.Contains("Chan_Doan")) dtData.Columns.Add("Chan_Doan", typeof(string));
                dtData.TableName = "KQHA";
              
                DataRow drData = dtData.Rows[0];

                string ngayin = string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.Day, DateTime.Now.Month,
                    DateTime.Now.Year);
                List<string> Values = new List<string>()
                {
                Utility.sDbnull(drData["id_benhnhan"], ""),
                Utility.sDbnull(drData["ma_luotkham"], ""),
                Utility.sDbnull(drData["ten_benhnhan"], ""),
                Utility.sDbnull(drData["gioi_tinh"], ""),
                Utility.sDbnull(drData["nam_sinh"], ""),
                Utility.sDbnull(drData["mathe_bhyt"], ""),
                Utility.sDbnull(drData["Tuoi"],""),
                Utility.sDbnull("01/01/1900", ""),//Insurance_FromDate
                Utility.sDbnull("01/01/1900", ""),//Insurance_ToDate
                Utility.sDbnull(drData["ten_doituong_kcb"], ""),
                Utility.sDbnull(drData["ngay_sinh"], ""),
                Utility.sDbnull(drData["bacsi_chidinh"], ""),
                Utility.sDbnull(drData["chan_doan"], ""),
               "",
                Utility.sDbnull(drData["phong_chidinh"], ""),
               Utility.FormatDateTimeWithLocation( Utility.sDbnull(drData["ngay_thuchien"], DateTime.Now.ToString("dd/MM/yyyy")),
                            globalVariables.gv_strDiadiem),
                txtBacsi.Text,//Nguoi thuc hien
                PropertyLib._HinhAnhProperties.Khoaphongthuchien,
                Utility.sDbnull(drData["ten_dichvu"], ""),
                Utility.sDbnull(drData["dien_thoai"], ""),
                Utility.sDbnull(drData["dia_chi"], ""),
                Utility.sDbnull(drData["ma_chidinh"], ""),
                 Utility.sDbnull(drData["Khoa"], ""),
                  Utility.sDbnull(drData["Buong"], ""),
                   Utility.sDbnull(drData["Giuong"], ""),
                
                 ngayin,
                globalVariables.gv_strTenNhanvien,
                globalVariables.Branch_Name,
                globalVariables.Branch_Address,
                globalVariables.Branch_Phone,globalVariables.Branch_Hotline,globalVariables.Branch_Fax,
                globalVariables.Branch_Website,
                globalVariables.Branch_Email
            };
                //Utility.ShowMsg(globalVariables.Branch_Phone + globalVariables.Branch_Hotline + globalVariables.Branch_Fax);
                List<string> fieldNames = new List<string>()
            {
               "id_benhnhan",
                "ma_luotkham",
                "ten_benhnhan",
                "gioi_tinh",
                "nam_sinh",
                "mathe_bhyt",
                "Tuoi",
                "ngay_batdauBHYT",
                "ngay_ketthucBHYT",
                "ten_doituong_kcb",
                "ngay_sinh",
                "bacsi_chidinh",
                "chan_doan",
               "ten_vungks",
                "phong_chidinh",
                "ngay_thuchien",
               "nguoi_thuchien",
               "noi_thuchien",
                "ten_dichvu",
                "dien_thoai",
                "dia_chi",
                "ma_chidinh",
                 "Khoa",
                  "Buong",
                   "Giuong",
                
                 "ngay_in",
                "nguoi_in","tenbvien","diachibvien","SDT","SDTNong","Fax","Website","Email"
            };

                if (dtKQXN != null && dtKQXN.Rows.Count>0)
                {
                    foreach (DataRow dr in dtKQXN.Rows)
                    {
                        fieldNames.Add("MA_"+Utility.sDbnull(dr["ma_xn"]));
                        fieldNames.Add("KQ_" + Utility.sDbnull(dr["ma_xn"]));
                        fieldNames.Add("BT_" + Utility.sDbnull(dr["ma_xn"]));
                        Values.Add(Utility.sDbnull(dr["ten_chitietdichvu"]));
                        Values.Add(Utility.sDbnull(dr["Ket_qua"]));
                        Values.Add(objBenhnhan.IdGioitinh == 0 ? Utility.sDbnull(dr["bt_nam"]) : Utility.sDbnull(dr["bt_nu"]));
                    }
                }
                else
                {
                    Values.AddRange(new List<string>(){Utility.sDbnull(drData["Ket_luan"], ""),
                Utility.sDbnull(drData["mo_ta"], ""),
                Utility.sDbnull(drData["de_nghi"], "")});
                    fieldNames.AddRange(new List<string>() { "Ket_luan",
                "Mo_ta",
                "de_nghi",});
                }
                int idx = 1;
               
                string maubaocaogoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\XN.doc";
                string maubaocao = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory + "Doc\\", "XN.doc");
                string tempmau = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(tempmau)) Directory.CreateDirectory(tempmau);
                if (!File.Exists(maubaocao))
                {
                    maubaocao = maubaocaogoc;
                }
                //  string maubaocao = Application.StartupPath + @"\Reports\" + docChuan;
                string tenfile = Guid.NewGuid().ToString();
                string fileExt = Path.GetExtension(maubaocao);
                fileExt = fileExt == "" ? ".doc" : fileExt;
                string fileKetqua = string.Format("{0}{1}{2}{3}{4}{5}_{6}", Path.GetDirectoryName(tempmau), Path.DirectorySeparatorChar, Path.GetFileNameWithoutExtension(maubaocao), "_ketqua_", objBenhnhan.TenBenhnhan, tenfile, fileExt);
                if ((drData != null) && File.Exists(maubaocao))
                {
                    Document doc = new Document(maubaocao);

                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file mẫu trả KQ. Liên hệ phòng IT để được trợ giúp", "Thông báo");
                        return;
                    }
                    
                    DocumentBuilder builder = new DocumentBuilder(doc);

                    int width = 150;
                    
                    doc.MailMerge.RemoveEmptyParagraphs = true;
                    // builder.MoveToField("Noi_dung");
                   

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

                    if (builder.MoveToBookmark("logo") && globalVariables.SysLogo != null)
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

                    doc.MailMerge.Execute(fieldNames.ToArray(), Values.ToArray());
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    string path = fileKetqua;
                    if (PropertyLib._HinhAnhProperties.PrintPreview)
                    {
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
                        PrinterSettings printerSettings = new PrinterSettings();
                        printerSettings.DefaultPageSettings.Margins.Top = 0;
                        printerSettings.Copies = 1;
                        printerSettings.PrinterName = PropertyLib._HinhAnhProperties.TenmayInPhieutraKQ;
                        doc.Print(printerSettings);
                        //doc.Print()
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy biểu mẫu", "TThông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdWords_Click(object sender, EventArgs e)
        {
            
                if (!Utility.isValidGrid(grdPresDetail)) return;
                int presId = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                DataTable v_dtDataOrg = _KCB_KEDONTHUOC.LaythongtinDonthuoc_In(presId);

                DataRow[] arrDR = v_dtDataOrg.Select("tuvan_them=0");
                if (arrDR.Length <= 0)
                {
                    WordDontuvan(v_dtDataOrg);
                    return;
                }
                DataTable v_dtData = arrDR.CopyToDataTable();
                try
                {
                if (v_dtData == null || v_dtData.Rows.Count <= 0) return;
                string ICD_Name = "";
                string ICD_Code = "";
                if (v_dtData != null && v_dtData.Rows.Count > 0)
                    GetChanDoan(Utility.sDbnull(v_dtData.Rows[0]["mabenh_chinh"], ""),
                                Utility.sDbnull(v_dtData.Rows[0]["mabenh_phu"], ""), ref ICD_Name, ref ICD_Code);

                foreach (DataRow drv in v_dtData.Rows)
                {
                    drv["chan_doan"] = Utility.sDbnull(drv["chan_doan"]).Trim() == ""
                                           ? ICD_Name
                                           : Utility.sDbnull(drv["chan_doan"]) + ";" + ICD_Name;
                    drv["ma_icd"] = ICD_Code;
                }
                //  THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA4.xml");
                v_dtData.AcceptChanges();

                DataRow myr = v_dtData.Rows[0];
                Aspose.Words.Document doc = new Aspose.Words.Document(Application.StartupPath + @"\doc\donthuoc.doc");

                DocumentBuilder builder = new DocumentBuilder(doc);
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();
                Aspose.Words.Tables.Table tab = doc.FirstSection.Body.Tables[2];
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

                List<string> Values = new List<string>()
               {
                    Utility.sDbnull(myr["ten_benhnhan"],""), Utility.sDbnull(myr["ma_luotkham"],""), Utility.sDbnull(myr["gioi_tinh"],""), Utility.sDbnull(myr["nam_sinh"],""), Utility.sDbnull(myr["dia_chi"],""),
                     Utility.sDbnull(myr["chan_doan"],""), Utility.sDbnull(myr["ma_icd"],""), Utility.sDbnull(myr["loidan_bacsi"],""), Utility.FormatDateTime(globalVariables.SysDate), Utility.sDbnull(myr["ten_bacsikedon"],""),
                      globalVariables.ParentBranch_Name,
                globalVariables.Branch_Name,
                globalVariables.Branch_Address,
                globalVariables.Branch_Phone,globalVariables.Branch_Hotline,globalVariables.Branch_Fax,
                globalVariables.Branch_Website,
                globalVariables.Branch_Email
               };
                List<string> fieldNames = new List<string>()
            {
                "ten_benhnhan","ma_luotkham","gioi_tinh","nam_sinh","dia_chi","chan_doan","ma_icd","loidan_bacsi","ngay_kedon","bs_kedon",
                "tendvicaptren","tenbvien","diachibvien","SDT","SDTNong","Fax","Website","Email"
            };

                int idx = 1;
                foreach (DataRow dr in v_dtData.Rows)
                {
                    Aspose.Words.Tables.Row newRow = (Aspose.Words.Tables.Row)tab.LastRow.Clone(true);
                    newRow.RowFormat.Borders.Shadow = false;
                    newRow.Cells[0].CellFormat.Shading.BackgroundPatternColor = Color.White;
                    newRow.Cells[1].CellFormat.Shading.BackgroundPatternColor = Color.White;
                    newRow.Cells[2].CellFormat.Shading.BackgroundPatternColor = Color.White;
                    newRow.Cells[3].CellFormat.Shading.BackgroundPatternColor = Color.White;
                    newRow.Cells[4].CellFormat.Shading.BackgroundPatternColor = Color.White;

                    newRow.Cells[0].FirstParagraph.Runs.Clear();
                    newRow.Cells[1].FirstParagraph.Runs.Clear();
                    newRow.Cells[2].FirstParagraph.Runs.Clear();
                    newRow.Cells[3].FirstParagraph.Runs.Clear();
                    newRow.Cells[4].FirstParagraph.Runs.Clear();
                    Run r = new Run(doc);
                    r.Font.Name = "Times New Roman";
                    r.Font.Bold = true;
                    r.Text = idx.ToString();
                    newRow.Cells[0].FirstParagraph.AppendChild(r);

                    r = new Run(doc);
                    r.Font.Name = "Times New Roman";
                    r.Font.Bold = true;
                    r.Text = Utility.sDbnull(dr["ten_thuoc"], "");
                    newRow.Cells[1].FirstParagraph.AppendChild(r);
                    newRow.Cells[1].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                    r = new Run(doc);
                    r.Font.Name = "Times New Roman";
                    r.Font.Bold = false;
                    r.Text = Utility.sDbnull(dr["ten_donvitinh"], "");
                    newRow.Cells[2].FirstParagraph.AppendChild(r);
                    newRow.Cells[2].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                    r = new Run(doc);
                    r.Font.Name = "Times New Roman";
                    r.Font.Bold = true;
                    r.Text = Utility.sDbnull(Utility.Int32Dbnull(dr["so_luong"], 0), "");
                    newRow.Cells[3].FirstParagraph.AppendChild(r);

                    r = new Run(doc);
                    r.Font.Name = "Times New Roman";
                    r.Font.Bold = false;

                    r.Text = Utility.sDbnull(dr["mota_them"], "");
                    newRow.Cells[4].FirstParagraph.AppendChild(r);
                    newRow.Cells[4].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                    tab.AppendChild(newRow);
                    idx += 1;
                }
                doc.MailMerge.Execute(fieldNames.ToArray(), Values.ToArray());
                string fileName = Guid.NewGuid().ToString() + ".doc";
                string path = Application.StartupPath + @"\tempdoc\" + fileName;
                doc.Save(path);
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
            catch (Exception)
            {


            }
            finally
            {
                WordDontuvan(v_dtDataOrg);
            }
           
        }
        private void WordDontuvan(DataTable v_dtDataOrg)
        {
            try
            {
                if (!Utility.isValidGrid(grdPresDetail)) return;
                int presId = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                DataRow[] arrDR = v_dtDataOrg.Select("tuvan_them=1");
                if (arrDR.Length <= 0)
                {
                   // WordDontuvan(v_dtDataOrg);
                    return;
                }
                DataTable v_dtData = arrDR.CopyToDataTable();

                if (v_dtData == null || v_dtData.Rows.Count <= 0) return;

                string ICD_Name = "";
                string ICD_Code = "";
                if (v_dtData != null && v_dtData.Rows.Count > 0)
                    GetChanDoan(Utility.sDbnull(v_dtData.Rows[0]["mabenh_chinh"], ""),
                                Utility.sDbnull(v_dtData.Rows[0]["mabenh_phu"], ""), ref ICD_Name, ref ICD_Code);

                foreach (DataRow drv in v_dtData.Rows)
                {
                    drv["chan_doan"] = Utility.sDbnull(drv["chan_doan"]).Trim() == ""
                                           ? ICD_Name
                                           : Utility.sDbnull(drv["chan_doan"]) + ";" + ICD_Name;
                    drv["ma_icd"] = ICD_Code;
                }
                //  THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA4.xml");
                v_dtData.AcceptChanges();

                DataRow myr = v_dtData.Rows[0];
                Aspose.Words.Document doc = new Aspose.Words.Document(Application.StartupPath + @"\doc\donthuoctuvan.doc");
                DocumentBuilder builder = new DocumentBuilder(doc);
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();
                Aspose.Words.Tables.Table tab = doc.FirstSection.Body.Tables[2];
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

                List<string> Values = new List<string>()
               {
                    Utility.sDbnull(myr["ten_benhnhan"],""), Utility.sDbnull(myr["ma_luotkham"],""), Utility.sDbnull(myr["gioi_tinh"],""), Utility.sDbnull(myr["nam_sinh"],""), Utility.sDbnull(myr["dia_chi"],""),
                     Utility.sDbnull(myr["chan_doan"],""), Utility.sDbnull(myr["ma_icd"],""), Utility.sDbnull(myr["loidan_bacsi"],""), Utility.FormatDateTime(globalVariables.SysDate), Utility.sDbnull(myr["ten_bacsikedon"],""),
                      globalVariables.ParentBranch_Name,
                globalVariables.Branch_Name,
                globalVariables.Branch_Address,
                globalVariables.Branch_Phone,globalVariables.Branch_Hotline,globalVariables.Branch_Fax,
                globalVariables.Branch_Website,
                globalVariables.Branch_Email
               };
                List<string> fieldNames = new List<string>()
            {
                "ten_benhnhan","ma_luotkham","gioi_tinh","nam_sinh","dia_chi","chan_doan","ma_icd","loidan_bacsi","ngay_kedon","bs_kedon",
                "tendvicaptren","tenbvien","diachibvien","SDT","SDTNong","Fax","Website","Email"
            };

                int idx = 1;
                foreach (DataRow dr in v_dtData.Rows)
                {
                    Aspose.Words.Tables.Row newRow = (Aspose.Words.Tables.Row)tab.LastRow.Clone(true);
                    newRow.RowFormat.Borders.Shadow = false;
                    newRow.Cells[0].CellFormat.Shading.BackgroundPatternColor = Color.White;
                    newRow.Cells[1].CellFormat.Shading.BackgroundPatternColor = Color.White;
                    newRow.Cells[2].CellFormat.Shading.BackgroundPatternColor = Color.White;
                    newRow.Cells[3].CellFormat.Shading.BackgroundPatternColor = Color.White;
                    newRow.Cells[4].CellFormat.Shading.BackgroundPatternColor = Color.White;

                    newRow.Cells[0].FirstParagraph.Runs.Clear();
                    newRow.Cells[1].FirstParagraph.Runs.Clear();
                    newRow.Cells[2].FirstParagraph.Runs.Clear();
                    newRow.Cells[3].FirstParagraph.Runs.Clear();
                    newRow.Cells[4].FirstParagraph.Runs.Clear();
                    Run r = new Run(doc);
                    r.Font.Name = "Times New Roman";
                    r.Font.Bold = true;
                    r.Text = idx.ToString();
                    newRow.Cells[0].FirstParagraph.AppendChild(r);

                    r = new Run(doc);
                    r.Font.Name = "Times New Roman";
                    r.Font.Bold = true;
                    r.Text = Utility.sDbnull(dr["ten_thuoc"], "");
                    newRow.Cells[1].FirstParagraph.AppendChild(r);
                    newRow.Cells[1].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                    r = new Run(doc);
                    r.Font.Name = "Times New Roman";
                    r.Font.Bold = false;
                    r.Text = Utility.sDbnull(dr["ten_donvitinh"], "");
                    newRow.Cells[2].FirstParagraph.AppendChild(r);
                    newRow.Cells[2].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                    r = new Run(doc);
                    r.Font.Name = "Times New Roman";
                    r.Font.Bold = true;
                    r.Text = Utility.sDbnull(Utility.Int32Dbnull(dr["so_luong"], 0), "");
                    newRow.Cells[3].FirstParagraph.AppendChild(r);

                    r = new Run(doc);
                    r.Font.Name = "Times New Roman";
                    r.Font.Bold = false;

                    r.Text = Utility.sDbnull(dr["mota_them"], "");
                    newRow.Cells[4].FirstParagraph.AppendChild(r);
                    newRow.Cells[4].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                    tab.AppendChild(newRow);
                    idx += 1;
                }
                doc.MailMerge.Execute(fieldNames.ToArray(), Values.ToArray());
                string fileName = Guid.NewGuid().ToString() + ".doc";
                string path = Application.StartupPath + @"\tempdoc\" + fileName;
                doc.Save(path);
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
            catch (Exception)
            {


            }

        }
        private void Inphieudinhduong()
        {
            try
            {

                if (objLuotkham == null || !Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn chưa chọn người bệnh để thực hiện thăm khám. Đề nghị kiểm tra lại", "Thông Báo");
                    return;
                }
                decimal BMI = Utility.DecimaltoDbnull(txtBMI.Text, 0);
                if (BMI < 18.5m)
                {
                    Utility.ShowMsg("Chưa có mẫu phiếu dinh dưỡng cho chỉ số BMI <18.5");
                    return;
                }
                bool isMorethan22_9 = BMI > 22.9m;
                string phieudinhduong = isMorethan22_9 ? @"\doc\phieudinhduong229.doc" : @"\doc\phieudinhduong185_229.doc";
                Aspose.Words.Document doc = new Aspose.Words.Document(Application.StartupPath + phieudinhduong);
                List<string> Values = new List<string>()
               {
                    objBenhnhan.TenBenhnhan,objLuotkham.MaLuotkham,objBenhnhan.GioiTinh,objBenhnhan.NamSinh.ToString(),objBenhnhan.DiaChi, objLuotkham.Tuoi.ToString(),txtCannang.Text,(Utility.DecimaltoDbnull(txtChieucao.Text, 0) / 100).ToString(),txtBMI.Text
               };
                List<string> fieldNames = new List<string>()
            {
                "ten_benhnhan","ma_luotkham","gioi_tinh","nam_sinh","dia_chi","tuoi","Cannang","Chieucao","BMI"
            };

                doc.MailMerge.Execute(fieldNames.ToArray(), Values.ToArray());
                string fileName = Guid.NewGuid().ToString() + ".doc";
                string path = Application.StartupPath + @"\tempdoc\" + fileName;
                doc.Save(path);
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
            catch (Exception)
            {


            }

        }
        private void cmdPhieuDinhduong_Click(object sender, EventArgs e)
        {
            Inphieudinhduong();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            if (Utility.Coquyen("quyen_huy_kqxn") &&  grdKetqua.SelectedItems.Count > 1)
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn hủy kết quả các xét nghiệm đang chọn", "Hủy kết quả", true))
                    return;
            List<KcbKetquaCl> lstResult = new List<KcbKetquaCl>();
            List<KcbChidinhclsChitiet> lstDetails = new List<KcbChidinhclsChitiet>();
            foreach (GridEXSelectedItem gitem in grdKetqua.SelectedItems)
            {
                GridEXRow row = gitem.GetRow();
                if (row.RowType != RowType.Record) return;
                KcbKetquaCl _item = null;
                KcbChidinhclsChitiet _itemchitiet = null;
                try
                {
                    int id_kq = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, KcbKetquaCl.Columns.IdKq), -1);
                    int IdChitietchidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                    int IdChitietdichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, DmucDichvuclsChitiet.Columns.IdChitietdichvu), -1);
                    _itemchitiet = KcbChidinhclsChitiet.FetchByID(IdChitietchidinh);
                    _itemchitiet.IsNew = false;
                    _itemchitiet.MarkOld();
                    if (id_kq > 0)
                    {
                        _item = KcbKetquaCl.FetchByID(id_kq);
                        _item.IsNew = false;
                        _item.NguoiSua = globalVariables.UserName;
                        _item.NgaySua = globalVariables.SysDate;
                        _item.IpMaysua = globalVariables.gv_strIPAddress;
                        _item.TenMaysua = globalVariables.gv_strComputerName;
                        _item.MarkOld();
                    }
                    else
                    {
                        _item = new KcbKetquaCl();
                        _item.IsNew = true;
                        _item.NguoiTao = globalVariables.UserName;
                        _item.NgayTao = globalVariables.SysDate;
                        _item.IpMaytao = globalVariables.gv_strIPAddress;
                        _item.TenMaytao = globalVariables.gv_strComputerName;
                    }
                    DmucDichvuclsChitiet objcls = DmucDichvuclsChitiet.FetchByID(IdChitietdichvu);
                    if (objcls != null)
                    {
                        _item.MaChidinh = Utility.GetValueFromGridColumn(grdAssignDetail, KcbChidinhcl.Columns.MaChidinh);
                        _item.MaBenhpham = Utility.GetValueFromGridColumn(grdAssignDetail, KcbChidinhcl.Columns.MaBenhpham);
                        _item.Barcode = Utility.GetValueFromGridColumn(grdAssignDetail, KcbChidinhcl.Columns.Barcode);
                        _item.IdBenhnhan = objBenhnhan.IdBenhnhan;
                        _item.MaLuotkham = ma_luotkham;
                        _item.IdChidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                        _item.IdChitietchidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                        _item.IdDichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, KcbChidinhclsChitiet.Columns.IdDichvu), -1);
                        _item.IdDichvuchitiet = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, KcbChidinhclsChitiet.Columns.IdChitietdichvu), -1);
                        _item.Barcode = Utility.GetValueFromGridColumn(grdAssignDetail, KcbChidinhcl.Columns.Barcode);
                        _item.SttIn = objcls.SttHthi;
                        _item.BtNam = objcls.BinhthuongNam;
                        _item.BtNu = objcls.BinhthuongNu;
                        _item.KetQua = "";
                            _item.TrangThai = 2;//Quay ve trang thai chuyen đang thực hiện
                        //_item.TenDonvitinh = objcls.TenDonvitinh;
                        _itemchitiet.KetQua = "";
                        _itemchitiet.TrangThai = 1;
                        _item.TenThongso = "";
                        _item.TenKq = "";
                        _item.LoaiKq = 0;
                        _item.ChophepHienthi = 1;
                        _item.ChophepIn = 1;
                        _item.MotaThem = objcls.MotaThem;

                    }
                    lstResult.Add(_item);
                    lstDetails.Add(_itemchitiet);
                }
                catch (Exception)
                {


                }
            }
            if (clsXN.UpdateResult(lstResult, lstDetails) == ActionResult.Success)
                Utility.ShowMsg("Đã hủy kết quả các xét nghiệm đang chọn thành công");
            else
                Utility.ShowMsg("Lỗi khi thực hiện hủy kết quả xét nghiệm");
            ShowKQXN();
        }

        private void cmdSearchBenhChinh_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdLuuChandoan_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdLuuChandoan_sobo_Click(object sender, EventArgs e)
        {
            LuuChanDoan_KL(0);
        }

        private void cboKieuin_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
        
        private void cmdchucnang_Click(object sender, EventArgs e)
        {

            //ContextMenu _cm = new ContextMenu();
            //MenuItem mnu = new MenuItem("+ Hủy kết thúc khám", _Click);
            //mnu.Name = "KHAMLAI";
            //mnu.Enabled = objCongkham.TrangThai == 1;
            //_cm.MenuItems.Add(mnu);
            //_cm.MenuItems.Add("-");
            //mnu = new MenuItem("+ Hủy kết thúc khám", _Click);
            //mnu.Name = "KHAMLAI";
            //mnu.Enabled = objCongkham.TrangThai == 1;
            //_cm.MenuItems.Add(mnu);

            //_cm.MenuItems.Add(new MenuItem("Nhập viện điều trị nội trú", cmdNhapVien_Click));
            //_cm.MenuItems.Add("-");
            ////_cm.MenuItems.Add(new MenuItem("+ Nhập viện điều trị ngoại trú", _Click));
            ////_cm.MenuItems.Add("-");
            ////_cm.MenuItems.Add(new MenuItem("+ Nhập viện điều trị ban ngày", _Click));
            ////_cm.MenuItems.Add("-");
            //_cm.MenuItems.Add(new MenuItem("Sửa nhập viện", cmdNhapVien_Click));
            //_cm.MenuItems.Add(new MenuItem("Hủy nhập viện", cmdNhapVien_Click));
            //_cm.MenuItems.Add("-");
            ////_cm.MenuItems.Add(new MenuItem("+ Ra viện", _Click));
            //_cm.MenuItems.Add(new MenuItem("+ Chuyển viện", _Click));
            //_cm.MenuItems.Add("-");
            //if (Utility.Coquyen("THAMKHAM_CHUYENPHONG"))
            //{
            //    _cm.MenuItems.Add(new MenuItem("+ Chuyển phòng", cmdChuyenPhong_Click));
            //}
            //if (Utility.Coquyen("THAMKHAM_THEMPHONG"))
            //{
            //    _cm.MenuItems.Add(new MenuItem("+ Thêm phòng", _Click));
            //}
            //if (Utility.Coquyen("CKYC"))
            //{
            //    _cm.MenuItems.Add("-");
            //    _cm.MenuItems.Add(new MenuItem("+ Chuyển khoa", _Click));
            //}
            ////cm.MenuItems.Add("-");
            ////cm.MenuItems.Add(new MenuItem("+ Nhập chức năng sống", cmchucnangsong_Click));
            //_cm.MenuItems.Add("-");
            //_cm.MenuItems.Add(new MenuItem("+ Khám bệnh cơ bản", _Click));
            //_cm.MenuItems.Add("-");
            //_cm.MenuItems.Add(new MenuItem("+ Lịch sử khám bệnh", _Click));
            //_cm.MenuItems.Add("-");
            //_cm.MenuItems.Add(new MenuItem("+ Lịch sử chẩn đoán", _Click));
            //_cm.MenuItems.Add("-");
            //_cm.MenuItems.Add(new MenuItem("+ In chi phí chưa thanh toán ngoại trú", _Click));
            //_cm.MenuItems.Add("-");
            //_cm.MenuItems.Add(new MenuItem("+ Lựa chọn phác đồ điều trị", _Click));
            //_cm.MenuItems.Add("-");
            //_cm.MenuItems.Add(new MenuItem("+ Cấp giấy nghỉ hưởng BHXH", _Click));
            

            //if (objLuotkham != null)
            //{
            //    _cm.MenuItems.Add("-");
            //    if (objLuotkham.ManTinh == true)
            //    {
            //        _cm.MenuItems.Add(new MenuItem("+ Hủy lần khám Mãn tính", _Click));
            //    }
            //    else
            //    {
            //        _cm.MenuItems.Add(new MenuItem("+ Chuyển lần khám sang khám Mãn tính",
            //            _Click));
            //    }

            //}

            ctxFuntions.Show(cmdchucnang, new Point(0, cmdchucnang.Height));
        
        }

        private void mnuThemPK_Click(object sender, EventArgs e)
        {

        }

        private void cmdIntachPhieu_Click(object sender, EventArgs e)
        {
            ChoninphieuCLS(false);
        }
        void ChoninphieuCLS(bool inchung)
        {
            try
            {
                DataRow[] arrDr = m_dtAssignDetail.Select("id_chidinh=" + RowCLS.Cells["id_chidinh"].Value.ToString());
                List<long> lstIdChitiet = (from p in grdAssignDetail.GetCheckedRows() select Utility.Int64Dbnull(p.Cells["id_chitietchidinh"].Value, -1)).ToList<long>();
               
                DataTable dtTempData = m_dtAssignDetail.Clone();
                if (arrDr.Length > 0)
                    dtTempData = arrDr.CopyToDataTable();
                if (lstIdChitiet.Count > 0)
                {
                    var q = (from p in dtTempData.AsEnumerable() where lstIdChitiet.Contains(Utility.Int64Dbnull(p["id_chitietchidinh"], -1)) select p);
                    if (q.Any())
                        dtTempData = q.CopyToDataTable();
                }
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
        int GetrealRegID()
        {
            int IdKham = Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbDangkyKcb.IdKhamColumn.ColumnName].Value, -1);
            //int idphongchidinh = Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbDangkyKcb.IdChaColumn.ColumnName].Value, -1);
            //int LaphiDVkemtheo = Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbDangkyKcb.LaPhidichvukemtheoColumn.ColumnName].Value, -1);
            //if (LaphiDVkemtheo == 1)
            //{
            //    foreach (GridEXRow _row in grdList.GetDataRows())
            //    {
            //        if (Utility.Int32Dbnull(_row.Cells[KcbDangkyKcb.IdKhamColumn.ColumnName].Value, -1) == idphongchidinh)
            //            return Utility.Int32Dbnull(_row.Cells[KcbDangkyKcb.IdKhamColumn.ColumnName].Value, -1);
            //    }
            //}
            //else
            //    return IdKham;
            return IdKham;
        }
        #region In phiếu khám chuyên khoa
        void InCongkham()
        {
            try
            {
                //if (grdList.GetDataRows().Count() <= 0 || grdList.CurrentRow.RowType != RowType.Record)
                //    return;
                //if (PropertyLib._MayInProperties.KieuInPhieuKCB == KieuIn.Innhiet)
                //    InPhieuKCB();
                //else
                    InphieuKham();
            }
            catch (Exception ex)
            {

                Utility.ShowMsg("Lỗi khi in phiếu khám\r\n" + ex.Message);
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
                Utility.SetParameterValue(crpt, "BENHAN", txtPatient_Code.Text);
                Utility.SetParameterValue(crpt, "TENBN", txtPatient_Name.Text);
                Utility.SetParameterValue(crpt, "GT_TUOI", string.Format("{0}-{1}",txtGioitinh.Text,txtTuoi.Text));
                string SOTHE = "Không có thẻ";
                string HANTHE = "Không có hạn";
                objLuotkham = Utility.getKcbLuotkham(objRegExam.IdBenhnhan, objRegExam.MaLuotkham);
                if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                {
                    SOTHE = objLuotkham.MatheBhyt;// Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.MatheBhyt].Value, "Không có thẻ");
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
                Utility.ShowMsg("Có lỗi trong quá trình in phiếu khám-->\r\n" + ex.Message);
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
                //IEnumerable<GridEXRow> query = from kham in grdList.GetDataRows()
                //                               where
                //                                   kham.RowType == RowType.Record &&
                //                                   Utility.Int32Dbnull(kham.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1) ==
                //                                   Utility.Int32Dbnull(objRegExam.IdKham)
                //                               select kham;
                //if (query.Count() > 0)
                //{
                //    GridEXRow gridExRow = query.FirstOrDefault();
                //    gridExRow.BeginEdit();
                //    gridExRow.Cells[KcbDangkyKcb.Columns.TrangthaiIn].Value = 1;
                //    gridExRow.EndEdit();
                //    grdList.UpdateData();
                //}
                DataTable v_dtData =new KCB_DANGKY().LayThongtinInphieuKCB(IdKham);
                THU_VIEN_CHUNG.CreateXML(v_dtData, Application.StartupPath + @"\Xml4Reports\PhieuKCB.XML");
                Utility.CreateBarcodeData(ref v_dtData, Utility.sDbnull(grdList.GetValue(KcbDangkyKcb.Columns.MaLuotkham)));
                if (v_dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy bản ghi nào", "Thông báo");
                    return;
                }
                objLuotkham = Utility.getKcbLuotkham(objRegExam.IdBenhnhan, objRegExam.MaLuotkham);
                if (objLuotkham != null)
                    KcbInphieu.INPHIEU_KHAM(Utility.sDbnull(objLuotkham.MaDoituongKcb), v_dtData,
                                                  "PHIẾU KHÁM BỆNH", PropertyLib._MayInProperties.CoGiayInPhieuKCB == Papersize.A5 ? "A5" : "A4");
            }
        }
       
        #endregion
        private void cmdInphieukham_Click(object sender, EventArgs e)
        {
            InCongkham();
        }

        private void mnuInbangkechiphiKCB_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.WaitNow(this);
                DataTable dtData = SPs.NgoaitruTonghopChiphiChuathanhtoan(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan).GetDataSet().Tables[0];
                THU_VIEN_CHUNG.Sapxepthutuin(ref dtData, false);
                dtData.DefaultView.Sort = "stt_in,stt_hthi_loaidichvu ,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";

                THU_VIEN_CHUNG.CreateXML(dtData, Application.StartupPath + @"\Xml4Reports\Thanhtoan_InBienLai_DV_chuathanhtoan.XML");
                if (dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu in phiếu (KCB_THANHTOAN_LAYTHONGTIN_INPHIEU_DICHVU)", "Thông báo");
                    return;
                }
                Utility.UpdateLogotoDatatable(ref dtData);
                dtData.DefaultView.Sort = "stt_in ,stt_hthi_loaidichvu,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";
                dtData.AcceptChanges();
                var p = (from q in dtData.AsEnumerable()
                         group q by q.Field<long>(KcbThanhtoan.Columns.IdThanhtoan) into r
                         select new
                         {
                             _key = r.Key,
                             tongtien_chietkhau_hoadon = r.Min(g => g.Field<decimal>("tongtien_chietkhau_hoadon")),
                             tongtien_chietkhau_chitiet = r.Min(g => g.Field<decimal>("tongtien_chietkhau_chitiet")),
                             tongtien_chietkhau = r.Min(g => g.Field<decimal>("tongtien_chietkhau"))
                         }).ToList();

                decimal tong = Utility.getSUM(dtData, "TT_BN");
                decimal tong_ck_hoadon = p.Sum(c => c.tongtien_chietkhau_hoadon);
                decimal tong_ck = p.Sum(c => c.tongtien_chietkhau);
                tong = tong - tong_ck;
                ReportDocument reportDocument = new ReportDocument();
                string tieude = "", reportname = "", reportCode = "";
                reportCode = "thanhtoan_bangkechiphiKCB_Noitru";

                reportDocument = Utility.GetReport(reportCode, ref tieude, ref reportname);

                if (reportDocument == null) return;
                var crpt = reportDocument;


                var objForm = new frmPrintPreview("", crpt, true, true);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                //try
                //{
                crpt.SetDataSource(dtData.DefaultView);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Telephone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Contact", string.Format("Điện thoại: {0} Email: {1}", globalVariables.Branch_Phone, globalVariables.Branch_Email));
                Utility.SetParameterValue(crpt, "tienmiengiam_hdon", tong_ck_hoadon);
                Utility.SetParameterValue(crpt, "tong_miengiam", tong_ck);
                Utility.SetParameterValue(crpt, "tongtien_bn", tong);
                //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTimeWithLocation(DateTime.Now, globalVariables.gv_strDiadiem));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "sMoneyCharacter",
                                       new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tong)));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInBienlai))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    objForm.ShowDialog();

                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }


            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                Utility.DefaultNow(this);
            }
            
        }

        private void mnuKCBCoban_Click(object sender, EventArgs e)
        {
            try
            {
                if (objCongkham == null || objLuotkham==null)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất một người bệnh trên danh sách người bệnh để bắt đầu công việc khám cơ bản");
                    return;
                }
                frm_KCBCoban _KCBCoban = new frm_KCBCoban(objLuotkham,objBenhnhan);
                _KCBCoban.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void mnuChuyenVien_Click(object sender, EventArgs e)
        {
            objLuotkham = Utility.getKcbLuotkham(objCongkham.IdBenhnhan, objCongkham.MaLuotkham);
            if (Utility.Byte2Bool(objLuotkham.TthaiChuyendi))
            {
                frm_chuyenvien _chuyenvien = new frm_chuyenvien();
                _chuyenvien.ucThongtinnguoibenh1.objLuotkham = this.objLuotkham;
                _chuyenvien.ucThongtinnguoibenh1.txtMaluotkham.Text = objLuotkham.MaLuotkham;
                _chuyenvien.ucThongtinnguoibenh1.Refresh();
                _chuyenvien.ShowDialog();
            }
        }

        private void mnuNhapvien_Click(object sender, EventArgs e)
        {

        }

        private void mnuKhamlai_Click(object sender, EventArgs e)
        {

        }

        private void cmdInTTDieuTri_Click_1(object sender, EventArgs e)
        {

        }

        private void mnuCancelResult_Click_1(object sender, EventArgs e)
        {

        }

        private void mnuXoacongkham_Click(object sender, EventArgs e)
        {
            if (objCongkham==null || objLuotkham==null)
            {
                Utility.ShowMsg("Bạn phải chọn ít nhất 1 công khám để xóa");
                return;
            }
            if (!IsValidXoaCongkham()) return;
            if (Utility.AcceptQuestion("Bạn có muốn thực hiện việc xóa công khám đang chọn không ?", "Thông báo", true))
            {
                HuyThamKham();
            }
        }
        private bool IsValidXoaCongkham()
        {
            if (!Utility.isValidGrid(grdList)) return false;
            if (grdList.CurrentRow == null) return false;
            if (Utility.Coquyen("KCB_XOACONGKHAM") || (globalVariables.UserName == Utility.sDbnull(grdList.GetValue("nguoitao_congkham"))) || Utility.sDbnull(grdList.GetValue("isme"),"0") =="1")
            {
            }
            else
            {
                Utility.ShowMsg("Bạn không được cấp quyền xóa công khám. Liên hệ quản trị hệ thống để được cấp thêm quyền", "Thông báo");
                return false;
            }
            int v_RegId = Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1);
            KcbDangkyKcb objRegExam = KcbDangkyKcb.FetchByID(v_RegId);
            if (objRegExam != null)
            {
                if (objRegExam.TrangthaiThanhtoan >= 1)
                {
                    Utility.ShowMsg("Công khám đang chọn đã thanh toán, Bạn không thể xóa", "Thông báo");
                    grdList.Focus();
                    return false;
                }
                else if (objRegExam.TrangThai >= 1)
                {
                    Utility.ShowMsg("Công khám bạn chọn đã được bác sĩ kết thúc khám nên bạn không thể xóa", "Thông báo");
                    grdList.Focus();
                    return false;
                }

                else if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEPDON_XOATHONGTINLANKHAM_FULL", "0", false) == "1")
                {
                    SqlQuery q =
                        new Select().From(KcbChidinhclsChitiet.Schema).Where(KcbChidinhclsChitiet.Columns.IdKham).IsEqualTo(
                            objRegExam.IdKham).And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                    if (q.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ chỉ định CLS và đã được thanh toán. Yêu cầu Hủy thanh toán các chỉ định CLS trước khi hủy phòng khám", "Thông báo");
                        grdList.Focus();
                        return false;
                    }
                    SqlQuery qPres =
                        new Select().From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.Columns.IdKham).IsEqualTo(
                            objRegExam.IdKham).And(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                    if (qPres.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ kê đơn thuốc và đã được thanh toán. Yêu cầu hủy thanh toán đơn thuốc trước khi hủy phòng khám", "Thông báo");
                        grdList.Focus();
                        return false;
                    }
                }
                else//Nếu có chỉ định CLS hoặc thuốc thì không cho phép xóa
                {
                    if (objRegExam.DachidinhCls >= 1)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ chỉ định CLS. Yêu cầu xóa chỉ định CLS trước khi xóa công khám", "Thông báo");
                        grdList.Focus();
                        return false;
                    }
                    else if (objRegExam.DakeDonthuoc >= 1)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ kê đơn thuốc. Yêu cầu xóa đơn thuốc trước khi xóa công khám", "Thông báo");
                        grdList.Focus();
                        return false;
                    }
                    SqlQuery q =
                        new Select().From(KcbChidinhcl.Schema).Where(KcbChidinhcl.Columns.IdKham).IsEqualTo(
                            objRegExam.IdKham);
                    if (q.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ chỉ định CLS. Yêu cầu xóa chỉ định CLS trước khi xóa công khám", "Thông báo");
                        grdList.Focus();
                        return false;
                    }
                    SqlQuery qPres =
                        new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.Columns.IdKham).IsEqualTo(objRegExam.IdKham);
                    if (qPres.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ kê đơn thuốc. Yêu cầu xóa đơn thuốc trước khi xóa công khám", "Thông báo");
                        grdList.Focus();
                        return false;
                    }
                }

            }
            return true;
        }
        KCB_DANGKY _KCB_DANGKY = new KCB_DANGKY();
        private void HuyThamKham()
        {

            if (grdList.CurrentRow != null)
            {
                int v_RegId = Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1);
                ActionResult actionResult = _KCB_DANGKY.PerformActionDeleteRegExam(v_RegId);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        string noidung = string.Format("Hủy công khám {0} của bệnh nhân ID={1}, PID={2}, Tên={3} thành công ", Utility.getValueOfGridCell(grdList, "ten_dichvukcb"), Utility.getValueOfGridCell(grdList, "id_benhnhan"), Utility.getValueOfGridCell(grdList, "ma_luotkham"), Utility.getValueOfGridCell(grdList, "ten_benhnhan"));
                        Utility.ShowMsg(noidung, "Thông báo");
                        Utility.Log(this.Name, globalVariables.UserName, noidung, newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                        DataRow dr = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                        m_dtDanhsachbenhnhanthamkham.Rows.Remove(dr);
                        m_dtDanhsachbenhnhanthamkham.AcceptChanges();
                        objCongkham = null;
                        objLuotkham = null;
                        _KcbChandoanKetluan = null;
                        ClearControl();
                        ModifyCommmands();
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Bạn thực hiện xóa công khám không thành công. Liên hệ đơn vị cung cấp phần mềm để được trợ giúp", "Thông báo");
                        break;
                }

            }

        }

        private void lbkHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            ShowHistory(); 
        }
        void ShowHistory()
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
        private void mnuShowResult_Click_1(object sender, EventArgs e)
        {

        }

        private void mnuViewPdfTheoLuotkham_Click(object sender, EventArgs e)
        {
            if (RowCLS == null) return;
            frm_PdfViewer _PdfViewer = new frm_PdfViewer(0);
            _PdfViewer.ma_luotkham = objLuotkham.MaLuotkham;
            _PdfViewer.ma_chidinh = "ALL";
            _PdfViewer.ShowDialog();
        }

        private void grdAssignDetail_FormattingRow(object sender, RowLoadEventArgs e)
        {

        }
        #region "QMS"

        bool isOpened = false;
        bool isEnable = false;
        string QMSType = "0";//"0","1","2": 3 kiểu màn hình khác nhau
        public QMSXQ frmXQCTMRI = null;//0
        public FrmShowScreen_Type1 frmSA_NS = null;//1
        public FrmShowScreen frmSA_NS_old = null;//2
        int ThoiGianTuDongLay = 5000;//Cấu hình bằng tham số hệ thống
        DataRow currentQMSRow = null;
        DataRow NextQMSRow = null;
        VMS.QMS.Class.QMSChucNang _qms = new VMS.QMS.Class.QMSChucNang();
        int numberofDisplay = 5;
        string layout0 = "STT,Họ và tên,Giới tính,Năm sinh@194, 701, 260, 309";
        string layout1 = "STT,Họ và tên,Giới tính,Năm sinh@194, 701, 260, 309";
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
                frmSA_NS = new FrmShowScreen_Type1(true, 0, numberofDisplay, layout1);
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

        void _OnsaveLayout(string layout,int type)
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
        public void ShowQMSOld()
        {
            Screen[] sc;
            sc = Screen.AllScreens;
            IEnumerable<Screen> query = from mh in Screen.AllScreens
                                        select mh;
            //get all the screen width and heights
            if (frmSA_NS_old == null)
            {
                frmSA_NS_old = new FrmShowScreen(true, 0, numberofDisplay, layout0);
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
                frmXQCTMRI = new QMSXQ(true, globalVariables.SysLogo,0);
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
        void frmXQCTMRI__OnRefreshData(string infor, DataRow currentQMSRow)
        {
            lblQMS_Current.Text = infor;
            Application.DoEvents();
            this.currentQMSRow = currentQMSRow;
        }
       
        private void cmdXoaSoKham_Click(object sender, EventArgs e)
        {
            _qms._qmspro = PropertyLib._QMSPrintProperties;
            cmdIgnore.Enabled = false;
            if (currentQMSRow != null && Utility.AcceptQuestion("Bạn có chắc chắn muốn bỏ qua bệnh nhân này để gọi bệnh nhân kế tiếp. Bạn có thể vào danh sách nhỡ(F10) để gọi lại bệnh nhân này", "Xác nhận bỏ nhỡ bệnh nhân", true))
                _qms.QmsPK_CapnhatTrangthai(Utility.Int64Dbnull(currentQMSRow["id_kham"], -1), Utility.Int64Dbnull(currentQMSRow["id"], -1), 0, 0);
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
                if(AutoGoiLoa_WhenNext)
                    Goiloa(NextQMSRow);
            RefreshQMS();
            cmdIgnore.Enabled = true;
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            if (!isOpened)
                OpenQMS();
            else
                StopQMS();
        }
        void StopQMS()
        {
            globalVariables.b_QMS_Stop = true;
            _qms._qmspro = PropertyLib._QMSPrintProperties;
            isOpened = false;
            txtSoQMS.Text = "0";
            txtTS.Text = "0";
            toolTip1.SetToolTip(cmdStart, isOpened ? "Tắt QMS" : "Bật QMS");
            cmdGoiloa.Enabled = cmdNext.Enabled = cmdRestore.Enabled = cmdIgnore.Enabled = isOpened;
            cmdStart.Image = isOpened ? global::VMS.HIS.Ngoaitru.Properties.Resources.QMS_pause_05_48 : global::VMS.HIS.Ngoaitru.Properties.Resources.QMS_Play_06_48;

            cmdStart2.Enabled = cmdStart.Enabled;
            cmdStart2.Image = cmdStart.Image;
            cmdGoiloa2.Enabled = cmdGoiloa.Enabled;
            cmdNext2.Enabled = cmdNext.Enabled;
            cmdRestore2.Enabled = cmdRestore.Enabled;
            cmdIgnore2.Enabled = cmdIgnore.Enabled;

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
        private void cmdRestore_Click(object sender, EventArgs e)
        {
            ctxQMSFunction.Show(cmdRestore, new Point(0, cmdRestore.Height));
        }
        bool isUsingQMS()
        {
            return isEnable && isOpened && currentQMSRow != null;
        }
        private void cmdGoiSoKham_Click(object sender, EventArgs e)
        {
            try
            {
                _qms._qmspro = PropertyLib._QMSPrintProperties;
                DataTable _dtDanhsach = _qms.QmsPK_GetData(_qms._qmspro.MaPhongQMS, DateTime.Now, 100, _qms._qmspro.MaKhoaQMS);
                if (_dtDanhsach == null) return;
                DataRow[] arrOK = _dtDanhsach.Select("trang_thai=3");//Đã xong
                DataRow[] arrDangThien = _dtDanhsach.Select("trang_thai=2");//Đang thực hiện
                DataRow[] arrDANGCHO = _dtDanhsach.Select("trang_thai=1");//Mới tạo
                DataRow[] arrNHO = _dtDanhsach.Select("trang_thai=0");//Nhỡ
                Utility.AddColumToDataTable(ref _dtDanhsach, "IsNo1", typeof(int));
                for (int j = 0; j < _dtDanhsach.Rows.Count; j++)
                {
                    if (j == 0)
                    {
                        _dtDanhsach.Rows[j]["IsNo1"] = 1;
                    }
                    else
                    {
                        _dtDanhsach.Rows[j]["IsNo1"] = 0;
                    }
                }

                var mDtDanhSachChoKhamNext = new DataTable();
                if (arrDANGCHO.Length != 0)
                {
                    mDtDanhSachChoKhamNext = arrDANGCHO.CopyToDataTable();
                    mDtDanhSachChoKhamNext = mDtDanhSachChoKhamNext.AsEnumerable().Take(5).CopyToDataTable();
                }

                var mDtDanhSachChoKhamPass = new DataTable();
                if (arrNHO.Length != 0)
                {
                    mDtDanhSachChoKhamPass = arrNHO.CopyToDataTable();
                    mDtDanhSachChoKhamPass = mDtDanhSachChoKhamPass.AsEnumerable().Take(5).CopyToDataTable();
                }
                string nhacnho = "";
                if (mDtDanhSachChoKhamPass.Rows.Count > 0)
                {
                    nhacnho = "Danh sách bệnh nhân nhỡ: ";
                    foreach (var row in mDtDanhSachChoKhamPass.AsEnumerable())
                    {
                        nhacnho = nhacnho +
                                  string.Format("{0} - {1};  ", Utility.FormatNumberToString(Utility.Int32Dbnull(row["so_kham"]), "00"), row["ten_benhnhan"].ToString());
                    }
                }
                if (mDtDanhSachChoKhamNext.Rows.Count > 0 || arrDangThien.Length > 0)
                {
                    DataTable rowDangkham = mDtDanhSachChoKhamNext.Clone();
                    if (arrDangThien.Length <= 0)
                        rowDangkham = mDtDanhSachChoKhamNext.AsEnumerable().Take(1).CopyToDataTable();
                    else
                        rowDangkham = arrDangThien.CopyToDataTable();
                    if (rowDangkham.Rows.Count > 0)
                    {
                        foreach (DataRow row in rowDangkham.AsEnumerable())
                        {
                            long So_Kham = Utility.Int64Dbnull(row["so_kham"], -1);
                            string tuoi = Utility.sDbnull(row["tuoi"], "");
                            tuoi = tuoi.Length > 0 ? string.Format("{0} tuổi ", tuoi) : "";
                            string TenBenhNhan = Utility.sDbnull(row["ten_benhnhan"], "");
                            if (So_Kham > 0)
                            {
                                List<string> lstSuspend = THU_VIEN_CHUNG.Laygiatrithamsohethong("QMS_SUSPEND", ":;:;;;;", false).Split(';').ToList<string>();
                                _qms.InsertGoiLoa(Utility.sDbnull(So_Kham), _qms._qmspro.MaPhongQMS,
                                    globalVariables.gv_strMacAddress, _qms._qmspro.MaKhoaQMS, 0,1,
                                    globalVariables.UserName, globalVariables.SysDate, globalVariables.gv_strMacAddress,
                                    _qms._qmspro.MaLoaGoi,
                                      string.Format("{0} {1} {2} {3} {4} {5}", _qms._qmspro.Loimoi, lstSuspend[0], TenBenhNhan,tuoi, lstSuspend[1], _qms._qmspro.TenPhong));//Mời bệnh nhân A 30 tuổi vào phòng khám số 3
                                // string.Format("{0} {1} {2} {3} {4} {5}", _qms._qmspro.Loimoi, So_Kham, lstSuspend[0], TenBenhNhan, lstSuspend[1], _qms._qmspro.TenPhong));//MỜI NGƯỜI BỆNH SỐ 1 TRẦN THỊ TƯƠI Vào Phòng Khám 102
                                       
                            }
                        }
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            cmdNext.Enabled = false;
            _qms._qmspro = PropertyLib._QMSPrintProperties;
            if (currentQMSRow != null)
                _qms.QmsPK_CapnhatTrangthai(Utility.Int64Dbnull(currentQMSRow["id_kham"], -1), Utility.Int64Dbnull(currentQMSRow["id"], -1), 3,0);
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
        /// <summary>
        /// QMSType: 1=Old, 2=Type1;3=XQCTMRI
        /// </summary>
        void OpenQMS()
        {
            globalVariables.b_QMS_Stop = false;
            cboPhongKhamNgoaiTru_SelectedIndexChanged(cboPhongKhamNgoaiTru, new EventArgs());
            QMSType = Utility.sDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("QMSPK_SCREENTYPE", "1", true), "1");
            if (QMSType == "1")
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
            else  if (QMSType == "2")
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
            cmdStart.Image = isOpened ? global::VMS.HIS.Ngoaitru.Properties.Resources.QMS_pause_05_48: global::VMS.HIS.Ngoaitru.Properties.Resources.QMS_Play_06_48;

            cmdStart2.Enabled = cmdStart.Enabled;
            cmdStart2.Image = cmdStart.Image;
            cmdGoiloa2.Enabled = cmdGoiloa.Enabled;
            cmdNext2.Enabled = cmdNext.Enabled;
            cmdRestore2.Enabled = cmdRestore.Enabled;
            cmdIgnore2.Enabled = cmdIgnore.Enabled;
        }
        /// <summary>
        /// hàm thực hiện việc lấy số khám của bệnh nhân
        /// </summary>
        /// <param name="status"></param>
        private void LaySokham(int status)
        {

            try
            {
                //if (PropertyLib._HISQMSProperties.TestMode || b_HasSecondScreen)
                //{
                //    int sokham = Utility.Int32Dbnull(txtSoQMS.Text);
                //    QMS_IdDichvuKcb = -1;
                //    IdQMS = -1;
                //    string sSoKham = Utility.sDbnull(sokham);
                //    if (!globalVariables.b_QMS_Stop)
                //    {
                //        if (globalVariables.MA_KHOA_THIEN == "KYC")//Chỉ có duy nhất số thường
                //        {
                //            _qms.LaySoKhamQMS(PropertyLib._HISQMSProperties.MaQuay, globalVariables.MA_KHOA_THIEN,
                //                PropertyLib._HISQMSProperties.MaDoituongKCB, ref sokham, ref QMS_IdDichvuKcb, ref IdQMS,
                //                (byte)0, 0, PropertyLib._HISQMSProperties.LoaiQMS_bo);
                //        }
                //        else//Các khoa khác
                //        {
                //            int isUuTien = 0;

                //            if (PropertyLib._HISQMSProperties.Chopheplaysouutien)
                //            {//qms_tiepdon_laysouutien
                //                DataTable dtDataUT = _KCB_QMS.QmsTiepdonLaysouutien(globalVariables.MA_KHOA_THIEN, "ALL", 100, Utility.ByteDbnull(PropertyLib._HISQMSProperties.LoaiQMS), Utility.ByteDbnull(chkUuTien.Checked ? 1 : 0), (byte)0);
                //                //SqlQuery sqlQuery1 = new Select().From(KcbQm.Schema)
                //                //    .Where(KcbQm.Columns.MaKhoakcb).IsEqualTo(globalVariables.MA_KHOA_THIEN)
                //                //    .And(KcbQm.Columns.TrangThai).In(0, 1)
                //                //    .AndExpression(KcbQm.Columns.MaDoituongKcb)
                //                //    .IsEqualTo("ALL")
                //                //    .Or(KcbQm.Columns.MaDoituongKcb)
                //                //    .IsEqualTo(PropertyLib._HISQMSProperties.MaDoituongKCB)
                //                //    .CloseExpression()
                //                //    .And(KcbQm.Columns.UuTien).IsEqualTo(1)
                //                //    .And(KcbQm.Columns.LoaiQms).IsEqualTo(PropertyLib._HISQMSProperties.LoaiQMS);
                //                isUuTien = dtDataUT != null && dtDataUT.Rows.Count > 0 ? 1 : 0;
                //            }
                //            if (PropertyLib._HISQMSProperties.Chilaysouutien)
                //                isUuTien = 1;
                //            if (!PropertyLib._HISQMSProperties.Chopheplaysouutien)
                //                isUuTien = 0;
                //            chkUuTien.Checked = isUuTien == 1;

                //            Utility.SetMsg(lblThongbaouutien, isUuTien == 1 ? "SỐ ƯU TIÊN" : (isUuTien == 0 ? "SỐ THƯỜNG" : PropertyLib._HISQMSProperties.TenLoaiQMS), isUuTien == 1);
                //            _qms.LaySoKhamQMS(PropertyLib._HISQMSProperties.MaQuay, globalVariables.MA_KHOA_THIEN, PropertyLib._HISQMSProperties.MaDoituongKCB, ref sokham, ref QMS_IdDichvuKcb, ref IdQMS, (byte)isUuTien, Utility.ByteDbnull(PropertyLib._HISQMSProperties.LoaiQMS), PropertyLib._HISQMSProperties.LoaiQMS_bo);

                //        }
                //    }
                //    if (sokham < 10)
                //    {
                //        sSoKham = Utility.FormatNumberToString(sokham, "00");
                //    }
                //    else
                //    {
                //        sSoKham = Utility.sDbnull(sokham);
                //    }

                //    int tongso = Utility.Int32Dbnull(txtTS.Text);
                //    string sTongSo = Utility.sDbnull(tongso);
                //    //Lấy tổng số QMS của khoa trong ngày
                //    StoredProcedure sp = SPs.QmsGetQMSCount(globalVariables.MA_KHOA_THIEN, PropertyLib._HISQMSProperties.MaDoituongKCB, tongso, PropertyLib._HISQMSProperties.LoaiQMS, 0);
                //    sp.Execute();
                //    tongso = Utility.Int32Dbnull(sp.OutputValues[0]);
                //    int tongsoUuTien = 0;
                //    //Lấy tổng số QMS ưu tiên của khoa trong ngày
                //    sp = SPs.QmsGetQMSCount(globalVariables.MA_KHOA_THIEN, "ALL", tongsoUuTien, PropertyLib._HISQMSProperties.LoaiQMS, 1);
                //    sp.Execute();
                //    tongsoUuTien = Utility.Int32Dbnull(sp.OutputValues[0]);
                //    if (!PropertyLib._HISQMSProperties.Chopheplaysouutien)
                //        tongsoUuTien = 0;
                //    int Total = tongso + tongsoUuTien;
                //    if (PropertyLib._HISQMSProperties.Chilaysouutien)
                //        Total = tongsoUuTien;
                //    UIAction.SetTextStatus(txtSoQMS, sSoKham, chkUuTien.Checked ? Color.Red : txtTS.ForeColor);
                //    if (Total < 10)
                //    {
                //        sSoKham = Utility.FormatNumberToString(Total, "00");
                //    }
                //    else
                //    {
                //        sSoKham = Utility.sDbnull(Total);
                //    }

                //    txtTS.Text = Utility.sDbnull(sSoKham);
                //}
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        private void mnuQMSConfig_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._QMSPrintProperties,globalVariables.m_strPropertiesFolderQMS);
            _Properties._OnRefreshData += _Properties__OnRefreshData;
            _Properties.ShowDialog();
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

        private void mnuQmsMan_Click(object sender, EventArgs e)
        {
            _qms._qmspro = PropertyLib._QMSPrintProperties;
            _qms.ShowListData(true,0);
        }

        private void mnuQmsColor_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._QMSColorProperties,globalVariables.m_strPropertiesFolderQMS);
            _Properties._OnRefreshData +=_Properties__OnRefreshData;
            _Properties.ShowDialog();
        }

        private void mnuCallbyQMS_Click(object sender, EventArgs e)
        {
            try
            {

                if (isEnable && isOpened && currentQMSRow != null && lblQMS_Current.Text.Trim().Length > 0)
                {
                    SearchPatient(Utility.sDbnull(currentQMSRow["ma_luotkham"], ""));
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void mnuInphieunhapvien_Click(object sender, EventArgs e)
        {
            try
            {
                if (objLuotkham.TrangthaiNoitru <= 0)
                {
                    Utility.ShowMsg("Người bệnh đang ở trạng thái ngoại trú nên không thể in. Vui lòng kiểm tra lại");
                    return;
                }
                NoitruPhieunhapvien objphieu = new Select().From(NoitruPhieunhapvien.Schema).Where(NoitruPhieunhapvien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(NoitruPhieunhapvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieunhapvien>();
                if (objphieu != null)
                {
                    IN_PHIEU_KHAM_VAO_VIEN();
                }
                else
                {
                    Utility.ShowMsg("Người bệnh chưa lập phiếu vào viện nên không thể in. Vui lòng kiểm tra lại");
                    return;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }

        }
        
        private void IN_PHIEU_KHAM_VAO_VIEN()
        {
            DataTable dsTable =
               new noitru_nhapvien().NoitruLaythongtinInphieunhapvien(objLuotkham.MaLuotkham, Utility.Int32Dbnull(objLuotkham.IdBenhnhan));
            if (dsTable.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy bản ghi nào\r\n Mời bạn xem lại", "Thông báo", MessageBoxIcon.Error);
                return;
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
                //machandoan += string.IsNullOrEmpty(objDiagInfo.Chandoan)
                //                ? ICD_Name
                //                : Utility.sDbnull(objDiagInfo.Chandoan);
                chandoan += string.IsNullOrEmpty(objDiagInfo.Chandoan)
                                ? ICD_Name
                                : Utility.sDbnull(objDiagInfo.Chandoan);
                mabenh += ICD_Code;
            }

           // DataTable dtDataChandoan = SPs.ThamkhamLaythongtinchandoan(machandoan).GetDataSet().Tables[0];
           // if (dtDataChandoan.Rows.Count > 0) chandoan = Utility.sDbnull(dtDataChandoan.Rows[0][0], "");
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



            dsTable.AcceptChanges();
            VNS.HIS.UI.Baocao.noitru_baocao.Inphieunhapvien(dsTable, "PHIẾU NHẬP VIỆN", globalVariables.SysDate);
        }

        private void cdViewPDF_Click(object sender, EventArgs e)
        {
            if (RowCLS == null || objLuotkham==null || objCongkham==null) return;
            frm_PdfViewer _PdfViewer = new frm_PdfViewer(0);
            _PdfViewer.ma_luotkham = objLuotkham.MaLuotkham;
            _PdfViewer.ma_chidinh = Utility.sDbnull(RowCLS.Cells[KcbChidinhcl.Columns.MaChidinh].Value);
            _PdfViewer.ShowDialog();
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

        private void mnuCheckQMS_Click(object sender, EventArgs e)
        {
            InitQMS();
        }

        private void lnkGoto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                
                if (isEnable && isOpened && currentQMSRow != null && lblQMS_Current.Text.Trim().Length > 0)
                {
                    SearchPatient(Utility.sDbnull(currentQMSRow["ma_luotkham"],""));
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void mnuSuagiaCLS_Click(object sender, EventArgs e)
        {
            if (!Utility.Coquyen("chidinhcls_quyen_suadongia"))
            {
                Utility.thongbaokhongcoquyen("chidinhcls_quyen_suadongia", " sửa giá dịch vụ cận lâm sàng");
                return;
            }

            grdAssignDetail.RootTable.Columns["don_gia"].EditType = Utility.Coquyen("chidinhcls_quyen_suadongia") ? EditType.TextBox : EditType.NoEdit;
            Utility.focusCellofCurrentRow(grdAssignDetail, "don_gia");
        }
        void Goiloa(DataRow drQMS)
        {
            try
            {
                if (drQMS != null)
                {
                    List<string> lstSuspend = THU_VIEN_CHUNG.Laygiatrithamsohethong("QMS_SUSPEND", ":;:;;;;", false).Split(';').ToList<string>();
                    _qms._qmspro = PropertyLib._QMSPrintProperties;
                    string so_kham = drQMS["So_Kham"].ToString();
                    string tuoi = Utility.sDbnull(drQMS["tuoi"], "");
                    tuoi = tuoi.Length > 0 ? string.Format("{0} tuổi ", tuoi) : "";
                    string ten_benhnhan = drQMS["TEN_BENHNHAN"].ToString();
                    _qms.InsertGoiLoa(so_kham, _qms._qmspro.MaPhongQMS,
                                          globalVariables.gv_strIPAddress, _qms._qmspro.MaKhoaQMS, 0, 1,
                                          globalVariables.UserName, globalVariables.SysDate, globalVariables.gv_strMacAddress,
                                          _qms._qmspro.MaLoaGoi,
                                            string.Format("{0} {1} {2} {3} {4} {5}", _qms._qmspro.Loimoi, lstSuspend[0], ten_benhnhan, tuoi, lstSuspend[1], _qms._qmspro.TenPhong));//Mời bệnh nhân A 30 tuổi vào phòng khám số 3
                                          //string.Format("{0} {1} {2} {3} {4} {5}", _qms._qmspro.Loimoi, so_kham,lstSuspend[0], ten_benhnhan,lstSuspend[1],                                              _qms._qmspro.TenPhong));
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        private void cmdGoiloa_Click(object sender, EventArgs e)
        {
            Goiloa(currentQMSRow);
            
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
                    cmd.CommandSql = string.Format("select id_benhnhan,ma_luotkham,ma_yte,ten_benhnhan,dien_thoai as SDT,CMT,ten_dantoc as dan_toc,gioi_tinh,nam_sinh,dia_chi as hokhau_thuongtru,dia_chi as noio_hientai,nghe_nghiep,ten_doituong_kcb as  doi_tuong,'' as nguyco_laynhiem_hiv,'' as dia_diem,so_ravien,so_vaovien from v_kcb_luotkham where id_benhnhan={0} and ma_luotkham='{1}'", objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham);
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
                                   Path.GetFileNameWithoutExtension(PathDoc), "PhieuPTTT", objLuotkham.MaLuotkham, Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


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

        private void cboPhongKhamNgoaiTru_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DmucKhoaphong objKP = DmucKhoaphong.FetchByID(Utility.Int32Dbnull( cboPhongKhamNgoaiTru.SelectedValue,-1));
                bool Auto_changePK = THU_VIEN_CHUNG.Laygiatrithamsohethong("QMS_TUDONGTHAYDOIQMS_KHICHONPK","0",false)=="1";
                if (objKP != null && Auto_changePK)
                {
                    PropertyLib._QMSPrintProperties.MaPhongQMS = objKP.MaPhongStt;
                    if (_qms != null)
                        _qms._qmspro = PropertyLib._QMSPrintProperties;
                     if (frmXQCTMRI != null)
                         frmXQCTMRI._qmsPrintProperties = PropertyLib._QMSPrintProperties;
                     if (frmSA_NS != null)
                         frmSA_NS._qmsPrintProperties = PropertyLib._QMSPrintProperties;
                     if (frmSA_NS_old != null)
                         frmSA_NS_old._qmsPrintProperties = PropertyLib._QMSPrintProperties;


                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void mnuInBienlai_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtData = SPs.NgoaitruTonghopChiphiChuathanhtoan(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan).GetDataSet().Tables[0];
                new INPHIEU_THANHTOAN_NGOAITRU().Inbienlai_DichvuChuathanhtoan(dtData, true, 1);
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void cmdNext_Click_1(object sender, EventArgs e)
        {

        }
        void InBarcode()
        {
            try
            {
                KcbLuotkham objluotkham = Utility.getKcbLuotkham(grdList.CurrentRow);
                if (objluotkham != null)
                {
                    QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandSql = string.Format("select * from v_kcb_luotkham where id_benhnhan={0} and ma_luotkham='{1}'", objluotkham.IdBenhnhan, objluotkham.MaLuotkham);
                    DataTable dt = DataService.GetDataSet(cmd).Tables[0];
                    if (!dt.Columns.Contains("barcodeID")) dt.Columns.Add("barcodeID", typeof(byte[]));
                    if (!dt.Columns.Contains("barcode")) dt.Columns.Add("barcode", typeof(byte[]));
                    THU_VIEN_CHUNG.CreateXML(dt, "barcode.xml");
                    if (dt.Rows.Count > 0)
                    {
                        FrmBarCodePrint frm = new FrmBarCodePrint(2);
                        frm.m_dtReport = dt;
                        frm.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        private void mnuBarcode_Click(object sender, EventArgs e)
        {
            InBarcode();
        }

        private void mnuBarcode1_Click(object sender, EventArgs e)
        {
            InBarcode();
        }

        private void cmdUpdateHDT_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn cập nhật hướng điều trị cho người bệnh {0} thành {1} hay không?", txtPatient_Name.Text,cboHDT.Text), "Chuyển hướng điều trị nhanh", true))
                {
                 int num=   new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.HuongDieutri).EqualTo(Utility.sDbnull(cboHDT.SelectedValue))
                        .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .Execute();
                 if (num > 0)
                     Utility.ShowMsg(string.Format("Đã cập nhật hướng điều trị cho người bệnh {0} thành {1} thành công. Nhấn OK để kết thúc", txtPatient_Name.Text, cboHDT.Text));
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void chkShowGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowGroup.Checked)
                CreateViewTable();
            else
            {
                Utility.SetDataSourceForDataGridEx_Basic(grdPresDetail, m_dtPresDetail, true, true, "", KcbDonthuocChitiet.Columns.SttIn);
                Utility.SetDataSourceForDataGridEx_Basic(grdVTTH, m_dtVTTH, true, true, "", KcbDonthuocChitiet.Columns.SttIn);
            }
        }

        private void chkShowGroupVTTH_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowGroupVTTH.Checked)
                CreateViewTable();
            else
            {
                Utility.SetDataSourceForDataGridEx_Basic(grdPresDetail, m_dtPresDetail, true, true, "", KcbDonthuocChitiet.Columns.SttIn);
                Utility.SetDataSourceForDataGridEx_Basic(grdVTTH, m_dtVTTH, true, true, "", KcbDonthuocChitiet.Columns.SttIn);
            }
        }

        private void cmdUpdateIDBacsikham_Click(object sender, EventArgs e)
        {
            if (Utility.Int32Dbnull(txtBacsi.MyID, -1) <= 0)
            {
                Utility.ShowMsg("Bạn cần chọn bác sĩ trong hệ thống để thực hiện cập nhật.");
                return;
            }

            if (Utility.Coquyen("thamkham_capnhatbacsi_CLS_Donthuoc"))
            {
                if (objCongkham != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        using (var sh = new SharedDbConnectionScope())
                        {
                            new Update(KcbChidinhcl.Schema)
                                .Set(KcbChidinhcl.Columns.IdBacsiChidinh).EqualTo(Utility.Int32Dbnull(txtBacsi.MyID, -1))
                                .Where(KcbChidinhcl.Columns.IdKham).IsEqualTo(objCongkham.IdKham)
                                .And(KcbChidinhcl.Columns.IdBenhnhan).IsEqualTo(objCongkham.IdBenhnhan)
                                .And(KcbChidinhcl.Columns.MaLuotkham).IsEqualTo(objCongkham.MaLuotkham)
                                .Execute();
                            new Update(KcbDonthuoc.Schema)
                               .Set(KcbDonthuoc.Columns.IdBacsiChidinh).EqualTo(Utility.Int32Dbnull(txtBacsi.MyID, -1))
                               .Where(KcbDonthuoc.Columns.IdKham).IsEqualTo(objCongkham.IdKham)
                               .And(KcbDonthuoc.Columns.IdBenhnhan).IsEqualTo(objCongkham.IdBenhnhan)
                               .And(KcbDonthuoc.Columns.MaLuotkham).IsEqualTo(objCongkham.MaLuotkham)
                               .Execute();
                        }
                        scope.Complete();
                    }
                    Utility.ShowMsg("Đã cập nhật bác sĩ chỉ định CLS, đơn thuốc thành công. Nhấn OK để kết thúc và in lại các phiếu chỉ định hoặc đơn thuốc");
                }
                else
                    Utility.ShowMsg("Cần chọn công khám trước khi thực hiện chức năng này");
            }
            else
            {
                Utility.thongbaokhongcoquyen("thamkham_capnhatbacsi_CLS_Donthuoc","cập nhật lại id bác sĩ khám cho các phiếu chỉ định CLS hoặc đơn thuốc đã kê");
            }
        }

        private void mnuSaochep_Click(object sender, EventArgs e)
        {
            SaochepIdBN();
        }
        void SaochepIdBN()
        {
            try
            {
                if (!Utility.isValidGrid(grdList)) return;
                string idbn =Utility.sDbnull( grdList.GetValue("id_benhnhan"),"-1");
                System.Windows.Forms.Clipboard.SetText(string.Format("{0}", idbn));
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void cmdDonthuoctutuc_Click(object sender, EventArgs e)
        {
            ThemMoiDonThuoc_Tutuc();
        }
        private void ThemMoiDonThuoc_Tutuc()
        {
            try
            {
                // cmdLuuChandoan.PerformClick();
                // KeDonThuocTheoDoiTuong();
                frm_KCB_KE_DONTHUOC frm = new frm_KCB_KE_DONTHUOC("THUOC");
                frm.donthuoctaiquay = 1;
                frm._OnSaveMe += frm__OnSaveMe;
                frm.objCongkham = this.objCongkham;
                frm.em_Action = action.Insert;
                frm.objLuotkham = objLuotkham;
                frm.IdBacsikham = _KcbChandoanKetluan != null ? _KcbChandoanKetluan.IdBacsikham : Utility.Int32Dbnull(txtBacsi.MyID, -1);
                frm._KcbCDKL = _KcbChandoanKetluan;
                frm._MabenhChinh = autoICD_2mat.MyCode;
                frm._Chandoan = txtChanDoan.Text;
                frm.DtIcd = dt_ICD;
                frm.dt_ICD_PHU = dt_ICD_PHU;
                frm.id_kham = Utility.Int32Dbnull(txtExam_ID.Text);
                frm.objCongkham = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txtReg_ID.Text));
                frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                frm.txtPatientID.Text = Utility.sDbnull(objBenhnhan.IdBenhnhan, "-1");
                frm.txtSoDT.Text = Utility.sDbnull(objBenhnhan.DienThoai, "");
                frm.txtPatientName.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan, "");
                frm.txtYearBirth.Text = Utility.sDbnull(objBenhnhan.NamSinh, "");
                frm.txtSex.Text = Utility.sDbnull(objBenhnhan.GioiTinh, "");
                frm.txtSDT.Text = objLuotkham.Sdt;
                frm.txtPres_ID.Text = "-1";
                frm.dtNgayKhamLai.MinDate = dtpNgaydangky.Value;
                frm._ngayhenkhamlai = dtpNgaydangky.Value.ToString("yyMMdd") == dtpNgayHen.Value.ToString("yyMMdd") ? "" : dtpNgayHen.Text;
                frm.noitru = 0;
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();

                if (!frm.m_blnCancel)
                {
                   // if (frm._MabenhChinh != Utility.sDbnull(cbo_icd.SelectedValue)) 
                        if (frm._MabenhChinh != autoICD_2mat.MyCode)
                    {
                        //cbo_icd.SelectedValue = frm._MabenhChinh;
                        autoICD_2mat.SetCode(frm._MabenhChinh);
                        //autoICD_2mat.RaiseEnterEvents();
                    }
                    txtChanDoan._Text = frm._Chandoan;
                    dt_ICD_PHU = frm.dt_ICD_PHU;
                    //if (frm._KcbCDKL != null)
                    //    _KcbChandoanKetluan = frm._KcbCDKL;
                    if (frm.chkNgayTaiKham.Checked)
                    {
                        dtpNgayHen.Value = frm.dtNgayKhamLai.Value;
                        cmdLuuKetluan.PerformClick();
                    }
                    else
                    {
                        dtpNgayHen.Value = dtpNgaydangky.Value;
                        cmdLuuKetluan.PerformClick();
                    }
                    fillmabenhchinh = false;
                    FillThongtinHoibenhVaChandoan();
                    fillmabenhchinh = true;
                    Laythongtinchidinhngoaitru();
                    Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuoc.Columns.IdDonthuoc,
                                            Utility.sDbnull(frm.txtPres_ID.Text));
                }
                frm.Dispose();
                frm = null;
                GC.Collect();
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
                ModifyCommmands();
                txtPatient_Code.Focus();
                txtPatient_Code.SelectAll();
            }
        }

        private void mnuLaythongtinlankhamtruoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdCongkham)) return;
                KcbChandoanKetluan objHistory = new Select().From(KcbChandoanKetluan.Schema)
                                            .Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(grdCongkham.GetValue(KcbChandoanKetluan.Columns.IdKham)).ExecuteSingle<KcbChandoanKetluan>();
                if (objHistory != null)
                {
                    txtChanDoan.Text = objHistory.Chandoan;
                    txtLydokham.Text = objHistory.TrieuchungBandau;
                    txtNhanxet.Text = objHistory.NhanXet;
                    txtQuatrinhbenhly.Text = objHistory.QuatrinhBenhly;
                    txtTiensubenh.Text = objHistory.TiensuBenh;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void chkHienthilichsucongkham_CheckedChanged(object sender, EventArgs e)
        {
            splitContainer4.Panel2Collapsed = !chkHienthilichsucongkham.Checked;
        }

        private void mnuDisplayme_Click(object sender, EventArgs e)
        {
            try
            {
                dtLsuCongkham.DefaultView.RowFilter = mnuDisplayme.Checked ? string.Format("id_bacsikham={0}", txtBacsi.MyID) : "1=1";
                dtLsuCongkham.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
           
        }
        void ConfigQMS()
        {
            try
            {
                isEnable = Utility.sDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("QMSPK_ENABLE", "0", true), "0") == "1";
                string QMS_Direction = Utility.sDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("QMSPK_DIRECTION", "0", true), "0");
                chkQMS_Ngang.Checked = QMS_Direction == "NGANG";
                if (QMS_Direction == "BOTH")
                {
                    pnlQMS.Height = isEnable ? 60 : 0;
                    //pnlThongtinBNKCB.Height = isEnable ? 232 : 160;
                    pnlQMS2.Width = isEnable ? 450 : 0;
                }
                else
                {
                    if (chkQMS_Ngang.Checked)
                    {
                        pnlQMS.Height = isEnable ? 60 : 0;
                        //pnlThongtinBNKCB.Height = isEnable ? 232 : 160;
                        pnlQMS2.Width = 0;
                    }
                    else
                    {
                        pnlQMS.Height = 0;
                        //pnlThongtinBNKCB.Height = 160;
                        pnlQMS2.Width = isEnable ? 450 : 0;
                    }
                }
            }
            catch (Exception)
            {
                
               
            }
        }
        private void chkQMS_Ngang_CheckedChanged(object sender, EventArgs e)
        {
            ConfigQMS();
        }

        private void cbo_icd_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void mnuInKQKhammat_Click(object sender, EventArgs e)
        {
            InketquaKhamMat();
        }
        void InketquaKhamMat()
        {
            try
            {

                if (_KcbChandoanKetluan == null || _KcbChandoanKetluan.IdChandoan <= 0)
                {
                    Utility.ShowMsg("Bạn cần lưu thông tin Kết quả khám sơ bộ trước khi thực hiện in");
                    return;
                }

                DataTable dtData = SPs.BvmKcbKetquakcbIn(objCongkham.IdKham, objCongkham.IdBenhnhan, objCongkham.MaLuotkham).GetDataSet().Tables[0];
                dtData.TableName = "PhieuKQKCB";
                List<string> lstAddedFields = new List<string>() {"gioitinh_nam","gioitinh_nu","noikhoa_khong", "noikhoa_co", "pttt_khong", "pttt_co",
                "tinhtrangravien_khoi", "tinhtrangravien_do", "tinhtrangravien_khongthaydoi",
                "tinhtrangravien_nanghon", "tinhtrangravien_tuvong", "tinhtrangravien_xinve","tinhtrangravien_khongxacdinh","chkkinh2trong","chkKinhdatrong","chkKinhnhingan","chkKinhdoimau","chkKinhpoly","chkKinhaptrong",};
                DataTable dtMergeField = dtData.Clone();
                Utility.AddColums2DataTable(ref dtMergeField, lstAddedFields, typeof(string));


                THU_VIEN_CHUNG.CreateXML(dtData, "PhieuKQKCB.xml");
                if (dtData == null || dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                dtData.TableName = "PhieuKQKCB";
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["donvi_captren"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diachi_benhvien"] = globalVariables.Branch_Address;
                drData["dienthoai_benhvien"] = globalVariables.Branch_Phone;
                drData["hotline_benhvien"] = globalVariables.Branch_Hotline;
                drData["fax_benhvien"] = globalVariables.Branch_Fax;
                drData["website_benhvien"] = globalVariables.Branch_Website;
                drData["email_benhvien"] = globalVariables.Branch_Email;
                drData["ngay_thuchien"] = Utility.FormatDateTimeWithLocation(Utility.sDbnull(drData["sNgaykedon"], DateTime.Now.ToString("dd/MM/yyyy")), globalVariables.gv_strDiadiem);

               
                List<string> fieldNames = new List<string>();
                Dictionary<string, string> dicMF = new Dictionary<string, string>();
                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\PhieuKQKCB.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtMergeField);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport("PhieuKQKCB", ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu Tóm tắt hồ sơ bệnh án tại thư mục sau :" + PathDoc);
                    return;
                }


                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return;
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), "PhieuKQKCB", objCongkham.MaLuotkham, Utility.sDbnull(objCongkham.IdKham), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return;
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
                    byte[] QRCode = Utility.GetQRCode(objCongkham.MaLuotkham);
                    sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("qrsize").ExecuteSingle<SysSystemParameter>();
                    if (builder.MoveToMergeField("qrcode") && QRCode != null && QRCode.Length > 100)
                        if (sysLogosize != null)
                        {
                            int w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            int h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(QRCode, w, h);
                            else
                                builder.InsertImage(QRCode);
                        }
                        else
                            builder.InsertImage(QRCode);
                    Utility.MergeFieldsCheckBox2Doc(builder, dicMF, null, drData);
                    //Các hàm MoveToMergeField cần thực hiện trước dòng MailMerge.Execute bên dưới
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
                    MessageBox.Show("Không tìm thấy biểu mẫu", "Thông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void cmdInKQKhamMat_Click(object sender, EventArgs e)
        {
            InketquaKhamMat();
        }

        private void mnuInphoi_Click(object sender, EventArgs e)
        {
            InPhoiBHYT();
        }
        private void InPhoiBHYT()
        {
            Utility.SetMsg(lblMessage, "", false);
            if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb.Value))
                return;
            if (objLuotkham == null)
            {
                Utility.SetMsg(lblMessage, "Bạn cần chọn Bệnh nhân cần thanh toán", true);
                return;
            }
            if (string.IsNullOrEmpty(objLuotkham.MabenhChinh))
            {
                Utility.SetMsg(lblMessage, "Chưa có mã bệnh ICD. Quay lại phòng khám của bác sĩ để nhập", true);
                txtTenICD2Mat.Focus();
                return;
            }
            if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb)
              && Utility.Int16Dbnull(objLuotkham.TrangthaiNgoaitru, 0) < 1
              && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_BHYT_ISKETTHUC", "0", false) == "1")
            {
                Utility.ShowMsg("Bệnh nhân cần được kết thúc trước khi in phôi BHYT");
                return;
            }
            SqlQuery sqlQuery = new Select().From(KcbDangkyKcb.Schema)
                                  .Where(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                  .And(KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                                  .And(KcbDangkyKcb.Columns.TrangThai).IsEqualTo(0);
            // yêu cầu kết thúc khám tất cả các phòng khám trước khi in phôi BHYT
            if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) && sqlQuery.GetRecordCount() > 0)
            {
                KcbDangkyKcb objExam = sqlQuery.ExecuteSingle<KcbDangkyKcb>();
                if (objExam != null)
                {
                    DmucKhoaphong objKP = DmucKhoaphong.FetchByID(objExam.IdPhongkham);
                    Utility.ShowMsg(
                        string.Format(
                            "Bệnh nhân Bảo hiểm y tế chưa được kết thúc tại phòng khám ngoại trú {0}, không thể in phôi BHYT",
                            objKP.TenKhoaphong), "Thông báo", MessageBoxIcon.Warning);
                }
                return;
            }
            if (objLuotkham.MaDoituongKcbBhyt == "2")
            {
                sqlQuery = new Select().From<NoitruPhieuravien>()
                    .Where(NoitruPhieuravien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .And(NoitruPhieuravien.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(objLuotkham.IdBenhnhan))
                    //.And(NoitruPhieuravien.Columns.MaLoaiKcb).IsEqualTo("2")
                    ;
                if (sqlQuery.GetRecordCount() <= 0)
                {
                    Utility.ShowMsg("Bệnh nhân phải ra viện mới chấp nhận xét duyệt tổng hợp\r\n Mời bạn xem lại", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
            }
            if (objLuotkham.MaDoituongKcbBhyt == "3")
            {
                Utility.ShowMsg("Người bệnh đã được nhập viện điều trị nội trú nên không thể in phôi BHYT ở chức năng thanh toán ngoại trú", "Thông báo", MessageBoxIcon.Warning);
                return;
            }
            if (objLuotkham.MaDoituongKcbBhyt == "4")
            {
                Utility.ShowMsg("Người bệnh đã được nhập viện điều trị ban ngày nên không thể in phôi BHYT ở chức năng thanh toán ngoại trú", "Thông báo", MessageBoxIcon.Warning);
                return;
            }
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_INPHOI_THANHTOAN", "1", true) == "1")//Được in phôi trước khi thanh toán. Dữ liệu lấy xong sẽ được tính toán % BHYT và các thông tin tiền cùng chi trả
            {
                PerformPrint_PhoiBHYT();
            }
            else//Thanh toán xong mới được in phôi BHYT
            {
                Utility.ShowMsg("Hệ thống đang cấu hình in phôi BHYT sau khi thanh toán. Vui lòng hướng dẫn người bệnh ra quầy thu ngân để thực hiện");
                //if (!THANHTOAN_BHYT_INPHIEU()) return;
                //PerformPrint_PhoiBHYT();
            }

        }
        private void PerformPrint_PhoiBHYT()
        {
            try
            {
                if (new BHYT_InPhieu().InPhoiBHYT(objLuotkham, DateTime.Now, "BHYT_InPhoi_01"))
                {
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}