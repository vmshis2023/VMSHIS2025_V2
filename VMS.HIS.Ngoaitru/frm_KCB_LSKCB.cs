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
using VMS.HIS.HLC.ASTM;
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
using VNS.HIS.UI.NOITRU;

namespace VNS.HIS.UI.NGOAITRU
{
    /// <summary>
    /// 06/11/2013 3h57
    /// </summary>
    public partial class frm_KCB_LSKCB : Form
    {
        private readonly string g_vsArgs = "ALL";

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

      

        private readonly string strSaveandprintPath = Application.StartupPath +
                                                      @"\CAUHINH\DefaultPrinter_PhieuHoaSinh.txt";

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
        private KcbDangkyKcb objkcbdangky;
        private GridEXRow row_Selected;
        private bool trieuchung;
        public long id_benhnhan = -1;
        private string ma_luotkham = "";
        public frm_KCB_LSKCB(string sthamso)
        {
            InitializeComponent();

            Utility.SetVisualStyle(this);
            if (PropertyLib._HinhAnhProperties == null) PropertyLib._HinhAnhProperties = PropertyLib.GetHinhAnhProperties();
            KeyPreview = true;
            g_vsArgs = sthamso;
            log = LogManager.GetCurrentClassLogger();
            log.Trace("Begin Constructor...");
            dtInput_Date.Value = dtpCreatedDate.Value = globalVariables.SysDate;
            dtFromDate.Value = globalVariables.SysDate;
            dtToDate.Value = globalVariables.SysDate;
            webBrowser1.Url = new Uri(Application.StartupPath.ToString() + @"\editor\ckeditor_simple.html");
           
            LoadLaserPrinters();
            CauHinhQMS();
            CauHinhThamKham();
            // txtIdKhoaNoiTru.Visible = globalVariables.IsAdmin;
            InitEvents();
            InitFtp();
            log.Trace("Constructor finished");
        }

        private int SplitterDistance
        {
            get { return Distance; }
            set { Distance = value; }
        }

        private void InitEvents()
        {
            FormClosing += frm_LAOKHOA_Add_TIEPDON_BN_FormClosing;
            Load += frm_KCB_LSKCB_Load;
            KeyDown += frm_KCB_LSKCB_KeyDown;

            txtSoKham.LostFocus += txtSoKham_LostFocus;
            txtSoKham.Click += txtSoKham_Click;
            txtSoKham.KeyDown += txtSoKham_KeyDown;

            cmdSearch.Click += cmdSearch_Click;
            radChuaKham.CheckedChanged += radChuaKham_CheckedChanged;
            radDaKham.CheckedChanged += radDaKham_CheckedChanged;

            txtPatient_Code.KeyDown += txtPatient_Code_KeyDown;

            grdList.ColumnButtonClick += grdList_ColumnButtonClick;
            grdList.KeyDown += grdList_KeyDown;
            grdList.DoubleClick += grdList_DoubleClick;
            grdList.MouseClick += grdList_MouseClick;

            grdPresDetail.UpdatingCell += grdPresDetail_UpdatingCell;

            txtMaBenhChinh.GotFocus += txtMaBenhChinh_GotFocus;
            txtMaBenhChinh.KeyDown += txtMaBenhChinh_KeyDown;
            txtMaBenhChinh.TextChanged += txtMaBenhChinh_TextChanged;
            txtMach.LostFocus += txtMach_LostFocus;
            txtSongaydieutri.LostFocus += txtSongaydieutri_LostFocus;
            // txtSoNgayHen.LostFocus += txtSoNgayHen_LostFocus;
            txtNhipTim.LostFocus += txtNhipTim_LostFocus;
            txtMaBenhphu.GotFocus += txtMaBenhphu_GotFocus;
            txtMaBenhphu.KeyDown += txtMaBenhphu_KeyDown;
            txtMaBenhphu.TextChanged += txtMaBenhphu_TextChanged;

            cmdSave.Click += cmdSave_Click;
            cmdInTTDieuTri.Click += cmdInTTDieuTri_Click;
            cmdNhapVien.Click += cmdNhapVien_Click;
            cmdHuyNhapVien.Click += cmdHuyNhapVien_Click;
            cmdCauHinh.Click += cmdCauHinh_Click;


            grdAssignDetail.CellUpdated += grdAssignDetail_CellUpdated;
            grdAssignDetail.SelectionChanged += grdAssignDetail_SelectionChanged;
            grdAssignDetail.MouseDoubleClick += grdAssignDetail_MouseDoubleClick;
            cmdInsertAssign.Click += cmdInsertAssign_Click;
            cmdUpdate.Click += cmdUpdate_Click;
            cmdDelteAssign.Click += cmdDelteAssign_Click;
            cboLaserPrinters.SelectedIndexChanged += cboLaserPrinters_SelectedIndexChanged;
            cmdPrintAssign.Click += cmdPrintAssign_Click;

            cboA4Donthuoc.SelectedIndexChanged += cboA4_SelectedIndexChanged;
            cboPrintPreviewDonthuoc.SelectedIndexChanged += cboPrintPreview_SelectedIndexChanged;
            cmdCreateNewPres.Click += cmdCreateNewPres_Click;
            cmdUpdatePres.Click += cmdUpdatePres_Click;
            cmdDeletePres.Click += cmdDeletePres_Click;
            cmdPrintPres.Click += cmdPrintPres_Click;

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
            txtKet_Luan._OnShowData += txtKet_Luan__OnShowData;
            txtHuongdieutri._OnShowData += txtHuongdieutri__OnShowData;
            txtNhommau._OnShowData += txtNhommau__OnShowData;
            txtNhanxet._OnShowData += txtNhanxet__OnShowData;
            txtCheDoAn._OnShowData += txtCheDoAn__OnShowData;

            txtNhommau._OnSaveAs += txtNhommau__OnSaveAs;
            txtKet_Luan._OnSaveAs += txtKet_Luan__OnSaveAs;
            txtHuongdieutri._OnSaveAs += txtHuongdieutri__OnSaveAs;
            txtChanDoan._OnSaveAs += txtChanDoan__OnSaveAs;
            txtChanDoanKemTheo._OnSaveAs += txtChanDoanKemTheo__OnSaveAs;
            txtNhanxet._OnSaveAs += txtNhanxet__OnSaveAs;
            txtCheDoAn._OnSaveAs += txtCheDoAn__OnSaveAs;

            cmdThemphieuVT.Click += cmdThemphieuVT_Click;
            cmdSuaphieuVT.Click += cmdSuaphieuVT_Click;
            cmdXoaphieuVT.Click += cmdXoaphieuVT_Click;
            cmdInphieuVT.Click += cmdInphieuVT_Click;
            mnuShowResult.Click += mnuShowResult_Click;
            cmdLuuChandoan.Click += cmdLuuChandoan_Click;
            mnuNhapKQCDHA.Click += mnuNhapKQCDHA_Click;
            lnkMore.Click += lnkMore_Click;
            timer1.Tick += timer1_Tick;
            cmdConfig.Click += cmdConfig_Click;
            this.FormClosing += frm_KCB_LSKCB_FormClosing;
            mnuInvert.Click += mnuInvert_Click;
            mnuCheck_UncheckAll.Click += mnuCheck_UncheckAll_Click;
            grdLuotkham.SelectionChanged += grdLuotkham_SelectionChanged;
            grdRegExam.SelectionChanged += grdRegExam_SelectionChanged;
            grdList.SelectionChanged += grdList_SelectionChanged;
        }
        public bool AllowSelectionChanged = false;
        void grdList_SelectionChanged(object sender, EventArgs e)
        {
            AllowSelectionChanged = false;
            int idbenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
            DataTable dtLSKCB = _KCB_THAMKHAM.KcbLichsuKcbLuotkham(idbenhnhan);
            Utility.SetDataSourceForDataGridEx_Basic(grdLuotkham, dtLSKCB, true, true,"1=1", "ngay_tiepdon desc");
            UpdateGroupLSKCB();
            AllowSelectionChanged = true;
            grdLuotkham.MoveFirst();
            grdLuotkham_SelectionChanged(grdLuotkham, e);

        }

        void grdRegExam_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowSelectionChanged) return;
                if (!Utility.isValidGrid(grdRegExam))
                {
                    ClearControl();
                    return;
                }
                HienthithongtinBn();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void grdLuotkham_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowSelectionChanged) return;
                if (!Utility.isValidGrid(grdLuotkham))
                {
                    ClearControl();
                    return;
                }
                AllowSelectionChanged = false;
                string maluotkham = Utility.sDbnull(grdLuotkham.GetValue(KcbLuotkham.Columns.MaLuotkham), "");
                DataTable dtKham = _KCB_THAMKHAM.KcbLichsuKcbTimkiemphongkham(objLuotkham.IdBenhnhan, maluotkham,0);
                Utility.SetDataSourceForDataGridEx_Basic(grdRegExam, dtKham, true, true,"", "ngay_dangky desc");
                //grbRegs.Height = grdRegExam.GetDataRows().Length <= 1 ? 0 : 50;
                AllowSelectionChanged = true;
                grdRegExam.MoveFirst();
                grdRegExam_SelectionChanged(grdRegExam, e);
                
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
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

        void frm_KCB_LSKCB_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (id_benhnhan > 0)
            {
                PropertyLib._ThamKhamProperties.LastSize = this.Size;
                PropertyLib.SaveProperty(PropertyLib._ThamKhamProperties);
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
            BeginExam();
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
            BeginExam();
        }

       
        private void grdAssignDetail_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BeginExam();
        }

        private void cmdLuuChandoan_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaBenhChinh.Text))
            {
                if (string.IsNullOrEmpty(txtTenBenhChinh.Text))
                {
                    Utility.ShowMsg("Tên bệnh chính không được để trống khi chẩn đoán mã bệnh", "Thông báo");
                }
                else
                {
                    if (!Utility.Coquyen("quyen_khamtatcacacphong_ngoaitru") &&
                 (Utility.Int16Dbnull(objkcbdangky.IdPhongkham) !=
                  Utility.Int16Dbnull(globalVariables.IdPhongNhanvien)))
                    {
                        Utility.ShowMsg(
                            "Bệnh nhân không thuộc phòng khám này. \n Người dùng không được phân quyền khám tất cả các phòng!",
                            "Thông báo");
                    }
                    else
                    {
                        cmdLuuChandoan.Enabled = false;
                        DataTable dtkt = SPs.KcbGetthongtinLuotkham(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan).GetDataSet().Tables[0];
                        if (dtkt.Rows.Count <= 0)
                        {
                            Utility.ShowMsg("Không tồn tại bệnh nhân! Bạn cần nạp lại thông tin dữ liệu", "Thông Báo");
                            return;
                        }
                        else
                        {
                            ActionResult act = _KCB_THAMKHAM.LuuHoibenhvaChandoan(TaoDulieuChandoanKetluan(),null, null,null, false);
                            if (act == ActionResult.Success)
                            {
                                _KcbChandoanKetluan.IsNew = false;
                                _KcbChandoanKetluan.MarkOld();
                                //_KcbChandoanKetluan.Save();
                            }
                        }

                        //Thread.Sleep(2000);
                        cmdLuuChandoan.Enabled = true;
                    }
                }
            }
            else
            {
                if (!Utility.Coquyen("quyen_khamtatcacacphong_ngoaitru") &&
                    (Utility.Int16Dbnull(objkcbdangky.IdPhongkham) !=
                     Utility.Int16Dbnull(globalVariables.IdPhongNhanvien)))
                {
                    Utility.ShowMsg(
                        "Bệnh nhân không thuộc phòng khám này. \n Người dùng không được phân quyền khám tất cả các phòng!",
                        "Thông báo");
                }
                else
                {
                    cmdLuuChandoan.Enabled = false;
                    ActionResult act = _KCB_THAMKHAM.LuuHoibenhvaChandoan(TaoDulieuChandoanKetluan(), null, null, null, false);
                    if (act == ActionResult.Success)
                    {
                        _KcbChandoanKetluan.IsNew = false;
                        _KcbChandoanKetluan.MarkOld();
                        //_KcbChandoanKetluan.Save();
                    }
                    //Thread.Sleep(2000);
                    cmdLuuChandoan.Enabled = true;
                }
            }
           
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
            mnuShowResult.Tag = mnuShowResult.Checked ? "1" : "0";
            if (PropertyLib._ThamKhamProperties.HienthiKetquaCLSTrongluoiChidinh)
            {
                Utility.ShowColumns(grdAssignDetail, mnuShowResult.Checked ? lstResultColumns : lstVisibleColumns);
            }
            else
                grdAssignDetail_SelectionChanged(grdAssignDetail, e);
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

        private void txtHuongdieutri__OnSaveAs()
        {
            if (Utility.DoTrim(txtHuongdieutri.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(txtHuongdieutri.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txtHuongdieutri.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtHuongdieutri.myCode;
                txtHuongdieutri.Init();
                txtHuongdieutri.SetCode(oldCode);
                txtHuongdieutri.Focus();
            }
        }

        private void txtKet_Luan__OnSaveAs()
        {
            if (Utility.DoTrim(txtKet_Luan.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(txtKet_Luan.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txtKet_Luan.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtKet_Luan.myCode;
                txtKet_Luan.Init();
                txtKet_Luan.SetCode(oldCode);
                txtKet_Luan.Focus();
            }
        }

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

        private void txtHuongdieutri__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(txtHuongdieutri.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtHuongdieutri.myCode;
                txtHuongdieutri.Init();
                txtHuongdieutri.SetCode(oldCode);
                txtHuongdieutri.Focus();
            }
        }

        private void txtKet_Luan__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(txtKet_Luan.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtKet_Luan.myCode;
                txtKet_Luan.Init();
                txtKet_Luan.SetCode(oldCode);
                txtKet_Luan.Focus();
            }
        }
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

        private void cmdChuyenPhong_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                if (objkcbdangky == null)
                {
                    Utility.ShowMsg("Bạn cần chọn bệnh nhân trước khi thực hiện chuyển phòng khám", "");
                    return;
                }
                //Kiểm tra nếu BN chưa kết thúc hoặc bác sĩ chưa thăm khám mới được phép chuyển phòng
                //SqlQuery sqlQuery = new Select().From(KcbDonthuoc.Schema)
                //    .Where(KcbDonthuoc.Columns.IdKham).IsEqualTo(objkcbdangky.IdKham);
                //if (sqlQuery.GetRecordCount() > 0)
                //{
                //    Utility.ShowMsg("Bác sĩ đã kê đơn thuốc cho lần khám này nên không thể chuyển phòng. Đề nghị hủy đơn thuốc trước khi chuyển phòng khám", "");
                //    return;
                //}
                //sqlQuery = new Select().From(KcbChidinhcl.Schema)
                //    .Where(KcbChidinhcl.Columns.IdKham).IsEqualTo(objkcbdangky.IdKham);
                //if (sqlQuery.GetRecordCount() > 0)
                //{
                //    Utility.ShowMsg("Bác sĩ đã chỉ định cận lâm sàng cho lần khám này nên không thể chuyển phòng. Đề nghị hủy chỉ định cận lâm sàng trước khi chuyển phòng khám", "");
                //    return;
                //}
                var chuyenPhongkham = new frm_ChuyenPhongkham();
                chuyenPhongkham.objDangkyKcb_Cu = objkcbdangky;
                chuyenPhongkham.MA_DTUONG = objkcbdangky.MaDoituongkcb;
                chuyenPhongkham.dongia = objkcbdangky.DonGia;
                chuyenPhongkham.txtPhonghientai.Text = txtPhongkham.Text;

                chuyenPhongkham.ShowDialog();
                if (!chuyenPhongkham.m_blnCancel)
                {
                    Utility.SetMsg(lblMsg, "Chuyển phòng khám thành công. Mời bạn chọn bệnh nhân khác để thăm khám",
                                   false);
                    DataRow[] arrDr = m_dtDanhsachbenhnhanthamkham.Select("id_kham=" + objkcbdangky.IdKham.ToString());
                    if (arrDr.Length > 0)
                    {
                        arrDr[0]["ten_phongkham"] = chuyenPhongkham.cboKieuKham.Text;
                        arrDr[0]["id_phongkham"] = chuyenPhongkham.objDichvuKCB.IdPhongkham;
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

        private void UpdateDatatable()
        {
        }

        private void cmdUnlock_Click(object sender, EventArgs e)
        {
            Unlock();
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
                                                (Convert.ToDouble(Utility.DoubletoDbnull(txtCannang.Text, 0))/
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
                            Where(KcbBenhAn.Columns.IdBnhan)
                            .IsEqualTo(Utility.sDbnull(txtPatient_ID.Text))
                            .And(KcbBenhAn.Columns.LoaiBa)
                            .IsEqualTo(frm.Loaibenhan);
                }
                else
                {
                      soKham =
                  new Select().From(KcbBenhAn.Schema).
                      Where(KcbBenhAn.Columns.IdBnhan).IsEqualTo(Utility.sDbnull(txtPatient_ID.Text));
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

        private void chkHienthichitiet_CheckedChanged(object sender, EventArgs e)
        {
            grdAssignDetail.GroupMode = chkHienthichitiet.Checked ? GroupMode.Expanded : GroupMode.Collapsed;
            grdAssignDetail.Refresh();
            Application.DoEvents();
        }

        private void chkAutocollapse_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._ThamKhamProperties.TudongthugonCLS = chkAutocollapse.Checked;
            PropertyLib.SaveProperty(PropertyLib._ThamKhamProperties);
            if (PropertyLib._ThamKhamProperties.TudongthugonCLS)
                grdAssignDetail.GroupMode = GroupMode.Collapsed;
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

                uiStatusBar1.Visible = !PropertyLib._ThamKhamProperties.HideStatusBar;
                cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                chkAutocollapse.Checked = PropertyLib._ThamKhamProperties.TudongthugonCLS;
                cmdPrintPres.Visible = PropertyLib._ThamKhamProperties.ChophepIndonthuoc;
                grdList.Height =id_benhnhan>0 || PropertyLib._ThamKhamProperties.ChieucaoluoidanhsachBenhnhanLSKCB <= 0
                                     ? 0
                                     : PropertyLib._ThamKhamProperties.ChieucaoluoidanhsachBenhnhanLSKCB;
                //grpSearch.Height = PropertyLib._ThamKhamProperties.AntimkiemNangcao ? 0 : 145;
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
            //if (PropertyLib._ThamKhamProperties.ViewAfterClick && !_buttonClick)
            //    HienthithongtinBn();
            //_buttonClick = false;
        }

        private void cboPrintPreview_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewInDonthuoc = cboPrintPreviewDonthuoc.SelectedIndex == 0;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        private void cboA4_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.CoGiayInDonthuoc = cboA4Donthuoc.SelectedIndex == 0
                                                                ? Papersize.A4
                                                                : Papersize.A5;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        private void cboPrintPreviewCLS_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewInCLS = cboPrintPreviewCLS.SelectedIndex == 0;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        private void cboA4Cls_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.CoGiayInCLS = cboA4Cls.SelectedIndex == 0 ? Papersize.A4 : Papersize.A5;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        private void cboPrintPreviewTomtatdieutringoaitru_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewInTomtatDieutriNgoaitru =
                cboPrintPreviewTomtatdieutringoaitru.SelectedIndex == 0;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        private void cboA4Tomtatdieutringoaitru_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.CoGiayInTomtatDieutriNgoaitru = cboA4Tomtatdieutringoaitru.SelectedIndex == 0
                                                                             ? Papersize.A4
                                                                             : Papersize.A5;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        private void grdList_DoubleClick(object sender, EventArgs e)
        {
            //HienthithongtinBn();
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

        private void frm_LAOKHOA_Add_TIEPDON_BN_FormClosing(object sender, FormClosingEventArgs e)
        {
                //Utility.FreeLockObject(m_strMaLuotkham);
                if (objSoKham != null && !(CheckOpened(objSoKham.Name))) objSoKham.Close();
          
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

        private void txtMaBenhChinh_GotFocus(object sender, EventArgs e)
        {
            //txtMaBenhChinh_TextChanged(txtMaBenhChinh, e);
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
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }
        public void AutoSearch()
        {
            try
            {
                ClearControl();
                m_strMaLuotkham = "";
                objkcbdangky = null;
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
                    status = radChuaKham.Checked ? 0 : 1;
                }
                if (cboPhongKhamNgoaiTru.SelectedIndex == 0)
                {
                    m_dtDanhsachbenhnhanthamkham =
                        _KCB_THAMKHAM.KcbTracuuLskcb2019(
                            !chkByDate.Checked ? globalVariables.SysDate.AddYears(-3) : dtFormDate,
                            !chkByDate.Checked ? globalVariables.SysDate : dt_ToDate, txtTenBN.Text, id_benhnhan, status,
                            soKham, globalVariables.StrQheNvpk, g_vsArgs.Split('-')[0],
                            globalVariables.MA_KHOA_THIEN);
                }
                else
                {
                    m_dtDanhsachbenhnhanthamkham =
                     _KCB_THAMKHAM.KcbTracuuLskcb2019(
                         !chkByDate.Checked ? globalVariables.SysDate.AddDays(-7) : dtFormDate,
                         !chkByDate.Checked ? globalVariables.SysDate : dt_ToDate, txtTenBN.Text, id_benhnhan, status,
                         soKham, Utility.sDbnull(cboPhongKhamNgoaiTru.SelectedValue), g_vsArgs.Split('-')[0],
                         globalVariables.MA_KHOA_THIEN);
                }


                if (!m_dtDanhsachbenhnhanthamkham.Columns.Contains("MAUSAC"))
                    m_dtDanhsachbenhnhanthamkham.Columns.Add("MAUSAC", typeof(int));


                Utility.SetDataSourceForDataGridEx_Basic(grdList, m_dtDanhsachbenhnhanthamkham, true, true, "",
                                                         KcbDangkyKcb.Columns.SttKham); //"locked=0", "");

                if (dt_ICD_PHU != null && !dt_ICD_PHU.Columns.Contains(DmucBenh.Columns.MaBenh))
                {
                    dt_ICD_PHU.Columns.Add(DmucBenh.Columns.MaBenh, typeof(string));
                }
                if (dt_ICD_PHU != null && !dt_ICD_PHU.Columns.Contains(DmucBenh.Columns.TenBenh))
                {
                    dt_ICD_PHU.Columns.Add(DmucBenh.Columns.TenBenh, typeof(string));
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
        public void SearchPatient()
        {
            try
            {
                ClearControl();
                m_strMaLuotkham = "";
                objkcbdangky = null;
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
                    status = radChuaKham.Checked ? 0 : 1;
                }
                if (cboPhongKhamNgoaiTru.SelectedIndex == 0)
                {
                    m_dtDanhsachbenhnhanthamkham =
                        _KCB_THAMKHAM.KcbTracuuLskcb2019(
                            !chkByDate.Checked ? globalVariables.SysDate.AddYears(-3) : dtFormDate,
                            !chkByDate.Checked ? globalVariables.SysDate : dt_ToDate, txtTenBN.Text,id_benhnhan, status,
                            soKham, globalVariables.StrQheNvpk, g_vsArgs.Split('-')[0],
                            globalVariables.MA_KHOA_THIEN);
                }
                else
                {
                    m_dtDanhsachbenhnhanthamkham =
                     _KCB_THAMKHAM.KcbTracuuLskcb2019(
                         !chkByDate.Checked ? globalVariables.SysDate.AddDays(-7) : dtFormDate,
                         !chkByDate.Checked ? globalVariables.SysDate : dt_ToDate, txtTenBN.Text, id_benhnhan, status,
                         soKham, Utility.sDbnull(cboPhongKhamNgoaiTru.SelectedValue), g_vsArgs.Split('-')[0],
                         globalVariables.MA_KHOA_THIEN);
                }
               

                if (!m_dtDanhsachbenhnhanthamkham.Columns.Contains("MAUSAC"))
                    m_dtDanhsachbenhnhanthamkham.Columns.Add("MAUSAC", typeof (int));


                Utility.SetDataSourceForDataGridEx_Basic(grdList, m_dtDanhsachbenhnhanthamkham, true, true, "",
                                                         KcbDangkyKcb.Columns.SttKham); //"locked=0", "");

                if (dt_ICD_PHU != null && !dt_ICD_PHU.Columns.Contains(DmucBenh.Columns.MaBenh))
                {
                    dt_ICD_PHU.Columns.Add(DmucBenh.Columns.MaBenh, typeof (string));
                }
                if (dt_ICD_PHU != null && !dt_ICD_PHU.Columns.Contains(DmucBenh.Columns.TenBenh))
                {
                    dt_ICD_PHU.Columns.Add(DmucBenh.Columns.TenBenh, typeof (string));
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
                //if (globalVariables.gv_intIDNhanvien <= 0)
                //{
                //    txtBacsi.SetId(-1);
                //}
                //else
                //{
                //    txtBacsi.SetId(globalVariables.gv_intIDNhanvien);
                //}
                //if (globalVariables.IsAdmin)
                //{
                //    txtBacsi.Enabled = true;
                //}
                //else
                //{
                //    txtBacsi.Enabled = false;
                //}
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }


        /// <summary>
        /// hàm thực hiện trạng thái của nút
        /// </summary>
        private void ModifyCommmands()
        {
            try
            {
                cmdPrintPres.Enabled = !string.IsNullOrEmpty(m_strMaLuotkham);
                
                cmdPrintPres.Enabled = Utility.isValidGrid(grdPresDetail) && !string.IsNullOrEmpty(m_strMaLuotkham);

                cmdPrintAssign.Enabled = Utility.isValidGrid(grdAssignDetail) && !string.IsNullOrEmpty(m_strMaLuotkham);

                chkIntach.Enabled = cmdPrintAssign.Enabled;
                cboServicePrint.Enabled = cmdPrintAssign.Enabled;
                tabDiagInfo.Enabled = objLuotkham != null && !string.IsNullOrEmpty(m_strMaLuotkham);
                cmdPrintPres.Enabled =
                    cmdDeletePres.Enabled =
                    cmdUpdatePres.Enabled = Utility.isValidGrid(grdPresDetail) && !string.IsNullOrEmpty(m_strMaLuotkham);

                cmdInphieuVT.Enabled =
                    cmdXoaphieuVT.Enabled =
                    cmdSuaphieuVT.Enabled = Utility.isValidGrid(grdVTTH) && !string.IsNullOrEmpty(m_strMaLuotkham);

                cmdUpdate.Enabled =cmdFileAttach.Enabled=
                    cmdDelteAssign.Enabled =
                    Utility.isValidGrid(grdAssignDetail) && !string.IsNullOrEmpty(m_strMaLuotkham);
                cmdConfirm.Enabled = Utility.isValidGrid(grdAssignDetail) && !string.IsNullOrEmpty(m_strMaLuotkham);

                chkDaThucHien.Visible = chkDaThucHien.Checked;
                if (objLuotkham != null)
                {
                    if (objLuotkham.Locked == 1 || objLuotkham.TrangthaiNoitru >= 1)
                    {
                        cmdInsertAssign.Enabled = cmdSave.Enabled = cmdUpdate.Enabled = cmdDelteAssign.Enabled =
                                                                                        cmdCreateNewPres.Enabled =
                                                                                        cmdUpdatePres.Enabled =
                                                                                        cmdDeletePres.Enabled =
                                                                                        cmdThemphieuVT.Enabled =
                                                                                        cmdSuaphieuVT.Enabled =
                                                                                        cmdXoaphieuVT.Enabled =
                                                                                        false;
                    }
                    else
                    {
                        cmdInsertAssign.Enabled = true && !string.IsNullOrEmpty(m_strMaLuotkham);
                        cmdCreateNewPres.Enabled = true && !string.IsNullOrEmpty(m_strMaLuotkham);
                        cmdThemphieuVT.Enabled = true && !string.IsNullOrEmpty(m_strMaLuotkham);
                    }
                    mnuDeleteCLS.Enabled = cmdUpdate.Enabled;
                    cmdphieupttt.Enabled = cmdUpdate.Enabled; 
                    cmdgiaychapnhanpttt.Enabled = cmdUpdate.Enabled; 
                    ctxDelDrug.Enabled = cmdUpdatePres.Enabled;
                }

                if (objkcbdangky != null)
                {
                    if (!Utility.Coquyen("quyen_khamtatcacacphong_ngoaitru"))
                    {
                        cmdInsertAssign.Enabled = cmdUpdate.Enabled = cmdDelteAssign.Enabled =
                            cmdCreateNewPres.Enabled =
                                cmdUpdatePres.Enabled =
                                    cmdDeletePres.Enabled = 
                        (Utility.Int16Dbnull(objkcbdangky.IdPhongkham) ==
                         Utility.Int16Dbnull(globalVariables.IdPhongNhanvien));
                    }
                }
             
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
                cmdConfirm.Text = @"Hủy chuyển CLS";
                cmdConfirm.Tag = 2;
            }
            if (m_dtAssignDetail.Select("trangthai_chuyencls=0 and id_chidinh = '" + idChidinh + "'").Length > 0)
            {
                cmdConfirm.Enabled = true;
                cmdConfirm.Text = @"Chuyển CLS";
                cmdConfirm.Tag = 1;
            }
        }

        private void Laythongtinchidinhngoaitru()
        {

            ds =
                _KCB_THAMKHAM.LaythongtinCLSVaThuoc(Utility.Int32Dbnull(txtPatient_ID.Text, -1),
                                                    Utility.sDbnull(m_strMaLuotkham, ""),
                                                    Utility.Int32Dbnull(txtExam_ID.Text));
            m_dtAssignDetail = ds.Tables[0];
            m_dtPresDetail = ds.Tables[1].Clone();
            m_dtVTTH = ds.Tables[1].Clone();
            DataRow[] arrtempt = ds.Tables[1].Select("kieu_thuocvattu = 'THUOC'");
            if (arrtempt.Length > 0) m_dtPresDetail = arrtempt.CopyToDataTable();
            arrtempt = ds.Tables[1].Select("kieu_thuocvattu = 'VT'");
            if (arrtempt.Length > 0) m_dtVTTH = arrtempt.CopyToDataTable();
            Utility.SetDataSourceForDataGridEx(grdAssignDetail, m_dtAssignDetail, false, true, "","");
            grdAssignDetail.MoveFirst();
            HienThiChuyenCan();
            m_dtDonthuocChitiet_View = m_dtPresDetail.Clone();
            foreach (DataRow dr in m_dtPresDetail.Rows)
            {
                dr["CHON"] = 0;
                DataRow[] drview
                    = m_dtDonthuocChitiet_View
                        .Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" +
                                Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.IdThuoc], "-1")
                                + " AND " + KcbDonthuocChitiet.Columns.DonGia + "=" +
                                Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.DonGia], "-1")
                                + " AND " + KcbDonthuocChitiet.Columns.TuTuc + "=" +
                                Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.TuTuc], "-1")
                        );
                if (drview.Length <= 0)
                {
                    m_dtDonthuocChitiet_View.ImportRow(dr);
                }
                else
                {
                    drview[0][KcbDonthuocChitiet.Columns.SoLuong] =
                        Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong], 0) +
                        Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SoLuong], 0);
                    drview[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                                   Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]);
                    drview[0]["TT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                      (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]) +
                                       Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu]));
                    drview[0]["TT_BHYT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                           Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                    drview[0]["TT_BN"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                         (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                                          Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                    drview[0]["TT_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                             Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                    drview[0]["TT_BN_KHONG_PHUTHU"] =
                        Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                        Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);

                    drview[0][KcbDonthuocChitiet.Columns.SttIn] =
                        Math.Min(Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SttIn], 0),
                                 Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                    m_dtDonthuocChitiet_View.AcceptChanges();
                }
            }

            Utility.SetDataSourceForDataGridEx(grdPresDetail, m_dtDonthuocChitiet_View, false, true, "",
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
                    drview[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                                   Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]);
                    drview[0]["TT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                      (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]) +
                                       Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu]));
                    drview[0]["TT_BHYT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                           Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                    drview[0]["TT_BN"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                         (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                                          Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                    drview[0]["TT_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                             Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                    drview[0]["TT_BN_KHONG_PHUTHU"] =
                        Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                        Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);

                    drview[0][KcbDonthuocChitiet.Columns.SttIn] =
                        Math.Min(Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SttIn], 0),
                                 Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                    m_dtVTTHChitiet_View.AcceptChanges();
                }
            }
            //Old-->Utility.SetDataSourceForDataGridEx
            Utility.SetDataSourceForDataGridEx(grdVTTH, m_dtVTTHChitiet_View, false, true, "",
                                               KcbDonthuocChitiet.Columns.SttIn);
            ResetNhominCLS();
        }

        private void Get_DanhmucChung()
        {
            txtTrieuChungBD.Init();
            txtChanDoan.Init();
            txtHuongdieutri.Init();
            txtKet_Luan.Init();
            txtNhommau.Init();
            txtNhanxet.Init();
            txtChanDoanKemTheo.Init();
            txtCheDoAn.Init(); 

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
        private void frm_KCB_LSKCB_Load(object sender, EventArgs e)
        {
            try
            {
                if (id_benhnhan > 0) grdList.Height = 0;
                CKEditorInput = THU_VIEN_CHUNG.Laygiatrithamsohethong("HINHANH_CKEDITOR", "1", true) == "1";
                log.Trace("Get_DanhmucChung finished");
                Get_DanhmucChung();
                lstVisibleColumns = Utility.GetVisibleColumns(grdAssignDetail);
                DataTable dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung(globalVariables.DC_NHOMIN_CLS, true);
                DataBinding.BindDataCombobox(cboServicePrint, dtNhomin, DmucChung.Columns.Ma, DmucChung.Columns.Ten,
                                           "Tất cả", true);
                Load_DSach_ICD();
                log.Trace("Load_DSach_ICD finished");
                LoadPhongkhamngoaitru();
                log.Trace("LoadPhongkhamngoaitru finished");
                LaydanhsachbacsiChidinh();
                log.Trace("LaydanhsachbacsiChidinh finished");
                //if (id_benhnhan > 0)
                //    AutoSearch();
                //else
                //    SearchPatient();
                log.Trace("LaydanhsachbacsiChidinh finished");
                if (cboServicePrint.Items.Count > 0) cboServicePrint.SelectedIndex = 0;
                cmdNhapVien.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU", "0", false) == "1";
                cmdConfirm.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_CHOPHEPBACSY_CHUYENCAN", "0", false) == "1";
                cmdHuyNhapVien.Visible = cmdNhapVien.Visible;
                hasLoaded = true;
                InitData();
                log.Trace("InitData finished");
                if (id_benhnhan > 0)
                {
                    if (PropertyLib._ThamKhamProperties.AutoSize)
                    {
                        this.Size = PropertyLib._ThamKhamProperties.LastSize;
                        this.WindowState = FormWindowState.Normal;
                        this.StartPosition = FormStartPosition.CenterParent;
                    }
                    AutoSearch();
                }
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
                    LayThongTinQuaMaLanKham();
                }
            }
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

        /// <summary>
        /// hàm thực hiện việc mã bệnh chính
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaBenhChinh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                hasMorethanOne = true;
                DataRow[] arrDr;
                isLike = false;
                //txtMaBenhChinh.Text. = txtMaBenhChinh.Text.ToUpper();
                if (isLike)
                    arrDr =
                        globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " like '" +
                                                             Utility.sDbnull(txtMaBenhChinh.Text, "") +
                                                             "%'");
                else
                    arrDr =
                        globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " = '" +
                                                             Utility.sDbnull(txtMaBenhChinh.Text, "") +
                                                             "'");
                if (!string.IsNullOrEmpty(txtMaBenhChinh.Text))
                {
                    if (arrDr.GetLength(0) == 1)
                    {
                        hasMorethanOne = false;
                        txtTenBenhChinh.Text = Utility.sDbnull(arrDr[0][DmucBenh.Columns.TenBenh], "");
                        txtMaBenhChinh.Text = arrDr[0][DmucBenh.Columns.MaBenh].ToString();
                    }
                    else
                    {
                        //txtDisease_ID.Text = "-1";
                        //ShowDiseaseList();
                        txtTenBenhChinh.Text = "";
                    }
                }
                else
                {
                    txtTenBenhChinh.Text = "";
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
             txtbenhan.Visible = Utility.DoTrim(txtMaBenhChinh.Text) != "" &&
                                        THU_VIEN_CHUNG.Laygiatrithamsohethong("BENH_AN", "0", false) == "1" &&
                                        (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_ICD_BENH_AN_NGOAI_TRU", "ALL", false) ==
                                         "ALL" ||
                                         THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_ICD_BENH_AN_NGOAI_TRU", "ALL", false)
                                             .Contains(Utility.DoTrim(txtMaBenhChinh.Text)));
             lblBenhan.Visible = txtbenhan.Visible;
             setChanDoan();
            }
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
                    txtMaBenhChinh.Text = frm.v_DiseasesCode;
                    txtMaBenhChinh.Focus();
                    txtMaBenhChinh.SelectAll();
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
                grdAssignDetail.DataSource = null;
                grdPresDetail.DataSource = null;
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
        private void GetData()
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                if (!Utility.isValidGrid(grdList))
                {
                    return;
                }
                bool khamcheoCackhoa = THU_VIEN_CHUNG.Laygiatrithamsohethong("KHAMCHEO_CACKHOA", "0", false) == "1";
                string patientCode = Utility.sDbnull(grdLuotkham.GetValue(KcbLuotkham.Columns.MaLuotkham), "");
                m_strMaLuotkham = patientCode;
                //if (!Utility.CheckLockObject(m_strMaLuotkham, "Thăm khám", "TK"))
                //    return;

                int idBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                objLuotkham = new Select().From(KcbLuotkham.Schema)
                    .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(patientCode)
                    .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(idBenhnhan).ExecuteSingle<KcbLuotkham>();

                objBenhnhan = KcbDanhsachBenhnhan.FetchByID(objLuotkham.IdBenhnhan);

                isRoom = false;
                if (objLuotkham != null)
                {
                    string tenKhoaphong = Utility.sDbnull(grdRegExam.GetValue("ten_phongkham"), "");

                    cmdChuyenPhong.Visible = objLuotkham.TrangthaiNoitru == 0;
                   
                   // ClearControl();
                    lblSongaydieutri.ForeColor = THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb)
                                                     ? lblKetluan.ForeColor
                                                     : lblBenhphu.ForeColor;
                    lblBenhchinh.ForeColor = lblSongaydieutri.ForeColor;
                    lblBANoitru.Text = Utility.Int32Dbnull(objLuotkham.SoBenhAn, -1) <= 0
                                           ? ""
                                           : "Số B.A Nội trú: " + objLuotkham.SoBenhAn;
                    txt_idchidinhphongkham.Text = Utility.sDbnull(grdRegExam.GetValue(KcbDangkyKcb.Columns.IdKham));

                    objkcbdangky = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txt_idchidinhphongkham.Text));
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
                    if (objkcbdangky != null)
                    {
                        cmdUnlock.Visible = objLuotkham.TrangthaiNoitru == 0 && objLuotkham.Locked.ToString() == "1";
                        cmdUnlock.Enabled = cmdUnlock.Visible &&
                                            (Utility.Coquyen("quyen_mokhoa_tatca") ||
                                             objLuotkham.NguoiKetthuc == globalVariables.UserName);
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
                        cmdNhapVien.Enabled = objkcbdangky.TrangThai == 1;
                        cmdHuyNhapVien.Enabled = objLuotkham.TrangthaiNoitru >= 1;
                        cmdNhapVien.Tag = objLuotkham.TrangthaiNoitru == 0 ? "0" : "1";
                        cmdNhapVien.Text = objLuotkham.TrangthaiNoitru == 0 ? "Nhập viện" : "Cập nhật";

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
                                dtInput_Date.Value = Convert.ToDateTime(dr[KcbLuotkham.Columns.NgayTiepdon]);
                                txtExam_ID.Text = Utility.sDbnull(grdRegExam.GetValue(KcbDangkyKcb.Columns.IdKham));


                                txtGioitinh.Text =
                                    Utility.sDbnull(grdList.GetValue(KcbDanhsachBenhnhan.Columns.GioiTinh), "");
                                txt_idchidinhphongkham.Text =
                                    Utility.sDbnull(grdRegExam.GetValue(KcbDangkyKcb.Columns.IdKham));
                                lblSOkham.Text = Utility.sDbnull(grdRegExam.GetValue(KcbDangkyKcb.Columns.SttKham));
                                txtPatient_Name.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                                txtPatient_ID.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.IdBenhnhan], "");
                                txtPatient_Code.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.MaLuotkham], "");
                                txtDiaChi.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.DiaChi], "");
                                txtDiachiBHYT.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.DiachiBhyt], "");
                                txtTrieuChungBD._Text = Utility.sDbnull(dr[KcbLuotkham.Columns.TrieuChung], "");
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
                                if (objkcbdangky != null)
                                {
                                    // txtSTTKhamBenh.Text = Utility.sDbnull(objkcbdangky.SoKham);
                                    txtReg_ID.Text = Utility.sDbnull(objkcbdangky.IdKham);
                                    dtpCreatedDate.Value = Convert.ToDateTime(objkcbdangky.NgayDangky);
                                    txtDepartment_ID.Text = Utility.sDbnull(objkcbdangky.IdPhongkham);
                                    var department = (from p in globalVariables.gv_dtDmucPhongban.AsEnumerable()
                                        where p[DmucKhoaphong.Columns.IdKhoaphong].Equals(objkcbdangky.IdPhongkham)
                                        select p).FirstOrDefault();
                                    //var _department =
                                    //    new Select().From(DmucKhoaphong.Schema).Where(DmucKhoaphong.IdKhoaphongColumn).
                                    //        //IsEqualTo(objkcbdangky.IdPhongkham).ExecuteSingle<DmucKhoaphong>();
                                    if (department != null)
                                    {
                                        txtPhongkham.Text = Utility.sDbnull(department["ten_khoaphong"],"");
                                    }
                                    txtTenDvuKham.Text = Utility.sDbnull(objkcbdangky.TenDichvuKcb);
                                    txtNguoiTiepNhan.Text = Utility.sDbnull(objkcbdangky.NguoiTao);
                                    if (Utility.Int16Dbnull(objkcbdangky.IdBacsikham, -1) != -1)
                                    {
                                        txtBacsi.SetId(Utility.sDbnull(objkcbdangky.IdBacsikham, -1));
                                    }
                                    else
                                    {
                                       // txtBacsi.SetId(globalVariables.gv_intIDNhanvien);
                                    }

                                    chkDaThucHien.Checked = Utility.Int32Dbnull(objkcbdangky.TrangThai) == 1;
                                }
                                _KcbChandoanKetluan = new Select().From(KcbChandoanKetluan.Schema)
                                    .Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(objkcbdangky.IdKham).
                                    ExecuteSingle
                                    <KcbChandoanKetluan>();
                                if (_KcbChandoanKetluan != null)
                                {
                                    txtKet_Luan._Text = Utility.sDbnull(_KcbChandoanKetluan.Ketluan);
                                    txtidchandoan.Text = Utility.sDbnull(_KcbChandoanKetluan.IdChandoan, "-1");
                                    // txtHuongdieutri.SetCode(_KcbChandoanKetluan.HuongDieutri);
                                    txtHuongdieutri._Text = _KcbChandoanKetluan.HuongDieutri;
                                    txtSongaydieutri.Text = Utility.sDbnull(_KcbChandoanKetluan.SongayDieutri, "0");
                                    txtSoNgayHen.Text = Utility.sDbnull(_KcbChandoanKetluan.SoNgayhen, "0");
                                    dtpNgayHen.Value = dtpCreatedDate.Value.AddDays(Utility.Int32Dbnull( _KcbChandoanKetluan.SoNgayhen,0));
                                    txtHa.Text = Utility.sDbnull(_KcbChandoanKetluan.Huyetap);
                                    txtTrieuChungBD._Text = Utility.sDbnull(_KcbChandoanKetluan.TrieuchungBandau);
                                    txtMach.Text = Utility.sDbnull(_KcbChandoanKetluan.Mach);
                                    txtNhipTim.Text = Utility.sDbnull(_KcbChandoanKetluan.Nhiptim);
                                    txtNhipTho.Text = Utility.sDbnull(_KcbChandoanKetluan.Nhiptho);
                                    txtNhietDo.Text = Utility.sDbnull(_KcbChandoanKetluan.Nhietdo);
                                    txtCannang.Text = Utility.sDbnull(_KcbChandoanKetluan.Cannang);
                                    txtChieucao.Text = Utility.sDbnull(_KcbChandoanKetluan.Chieucao);
                                    txtibm.Text = Utility.sDbnull(_KcbChandoanKetluan.ChisoIbm);
                                    txtthiluc_mp.Text = Utility.sDbnull(_KcbChandoanKetluan.ThilucMp);
                                    txtthiluc_mt.Text = Utility.sDbnull(_KcbChandoanKetluan.ThilucMt);
                                    txtnhanap_mp.Text = Utility.sDbnull(_KcbChandoanKetluan.NhanapMp);
                                    txtnhanap_mt.Text = Utility.sDbnull(_KcbChandoanKetluan.NhanapMt);
                                    txtNhanxet._Text = Utility.sDbnull(_KcbChandoanKetluan.NhanXet);
                                    txtCheDoAn._Text = Utility.sDbnull(_KcbChandoanKetluan.ChedoDinhduong, "");
                                    if (!string.IsNullOrEmpty(Utility.sDbnull(_KcbChandoanKetluan.Nhommau)) &&
                                        Utility.sDbnull(_KcbChandoanKetluan.Nhommau) != "-1")
                                        txtNhommau._Text = Utility.sDbnull(_KcbChandoanKetluan.Nhommau);
                                    isLike = false;
                                    txtChanDoan._Text = Utility.sDbnull(_KcbChandoanKetluan.Chandoan);
                                    txtChanDoanKemTheo._Text = Utility.sDbnull(_KcbChandoanKetluan.ChandoanKemtheo);
                                    txtMaBenhChinh.Text = Utility.sDbnull(_KcbChandoanKetluan.MabenhChinh);
                                    txtTenBenhChinh.Text = Utility.sDbnull(_KcbChandoanKetluan.MotaBenhchinh);
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
                                Laythongtinchidinhngoaitru();
                            }
                        }
                

                        //cmdKETTHUC.Visible = objLuotkham.Locked == 0 && objkcbdangky.TrangThai > 0;
                        //Tạm thời comment bỏ 2 dòng dưới để luôn hiển thị 2 nút này(190803)
                        //cmdInTTDieuTri.Visible =
                        //    cmdInphieuhen.Visible = objkcbdangky.TrangThai != 0 && objLuotkham.TrangthaiNoitru == 0;
                        txtbenhan.Visible = objkcbdangky.TrangThai != 0 &&
                                                THU_VIEN_CHUNG.Laygiatrithamsohethong("BENH_AN", "0", false) == "1" &&
                                                objLuotkham.TrangthaiNoitru == 0;
                      //  lblBenhan.Visible = cboChonBenhAn.Visible;
                        // cmdBenhAnNgoaiTru.Visible = objkcbdangky.TrangThai != 0;
                        cmdKETTHUC.Enabled = true;
                    }
                    else
                    {
                        ClearControl();
                        cmdKETTHUC.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                ModifyCommmands();
                KiemTraDaInPhoiBhyt();
                getResult();
                GetTamUng();
            }
        }

        private void GetTamUng()
        {
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
                txtMaBenhChinh.Text = Utility.sDbnull(_KcbChandoanKetluan.MabenhChinh);
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
            cmdCreateNewPres.Enabled = !Utility.isTrue(lockstatus);
            cmdUpdatePres.Enabled = grdPresDetail.RowCount > 0 && !Utility.isTrue(lockstatus);
            cmdDeletePres.Enabled = grdPresDetail.RowCount > 0 && !Utility.isTrue(lockstatus);
            cmdPrintPres.Enabled = grdPresDetail.RowCount > 0 && !string.IsNullOrEmpty(m_strMaLuotkham);
            ctxDelDrug.Enabled = cmdUpdatePres.Enabled;

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
                        cmdSave.Enabled =  Utility.Coquyen("suachandoansaukhiinphoi");
                     //   cmdSave.Enabled = false;
                        grd_ICD.Enabled = false;
                        cmdChuyenPhong.Enabled = false;
                        cmdLuuChandoan.Enabled = false;
                        toolTip1.SetToolTip(cmdSave, "Bệnh nhân đã kết thúc nên bạn không thể sửa thông tin được nữa");
                    }
                }
                else
                {
                    grd_ICD.Enabled = true;
                    cmdLuuChandoan.Enabled = true;
                    cmdChuyenPhong.Enabled = true;
                    cmdSave.Enabled = true;
                    toolTip1.SetToolTip(cmdSave, "Nhấn vào đây để kết thúc khám cho Bệnh nhân(Phím tắt Ctrl+S)");
                    lblMessage.Visible = false;
                }
            } //Đối tượng dịch vụ sẽ luôn hiển thị nút lưu
            else
            {
                grd_ICD.Enabled = true;
                cmdSave.Enabled = true;
                cmdLuuChandoan.Enabled = true;
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
        private void UpdateGroupLSKCB()
        {
            if (!Utility.isValidGrid(grdLuotkham)) return;
            var counts = ((DataView)grdLuotkham.DataSource).Table.AsEnumerable()
                .GroupBy(x => x.Field<string>("ma_doituong_kcb"))
                .Select(g => new { g.Key, Count = g.Count() });
            if (counts.Count() >= 2)
            {
                if (grdLuotkham.RootTable.Groups.Count <= 0)
                {
                    GridEXColumn gridExColumn = grdList.RootTable.Columns["ma_doituong_kcb"];
                    var gridExGroup = new GridEXGroup(gridExColumn) { GroupPrefix = "Nhóm đối tượng KCB: " };
                    grdLuotkham.RootTable.Groups.Add(gridExGroup);
                }
            }
            else
            {
                GridEXColumn gridExColumn = grdLuotkham.RootTable.Columns["ma_doituong_kcb"];
                var gridExGroup = new GridEXGroup(gridExColumn);
                grdLuotkham.RootTable.Groups.Clear();
            }
            grdLuotkham.UpdateData();
            grdLuotkham.Refresh();
        }

        private int i = 0;
        private void HienthithongtinBn()
        {
            try
            {
                //Utility.FreeLockObject(m_strMaLuotkham);
                uiTabKqCls.Width = 0;
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
                dtpNgayHen.MinDate = dtpCreatedDate.Value;
                setChanDoan();
            }
        }

        private void grdList_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            //try
            //{
            //    if (e.Column.Key == "cmdCHONBN")
            //    {
            //        Utility.FreeLockObject(m_strMaLuotkham);
            //        _buttonClick = true;
            //        GridEXColumn gridExColumn = grdList.RootTable.Columns[KcbDangkyKcb.Columns.SttKham];
            //        grdList.Col = gridExColumn.Position;
            //        uiTabKqCls.Width = 0;
            //        HienthithongtinBn();
            //    }
            //}
            //catch (Exception exception)
            //{
            //    Utility.ShowMsg(exception.Message);
            //}
        }

        private void Unlock()
        {
            try
            {
                if (objLuotkham == null)
                    return;
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
                //ModifyByLockStatus(objLuotkham.Locked);
                cmdUnlock.Visible = objLuotkham.TrangthaiNoitru == 0 && objLuotkham.Locked.ToString() == "1";
                cmdUnlock.Enabled = cmdUnlock.Visible &&
                                    (Utility.Coquyen("quyen_mokhoa_tatca") ||
                                     objLuotkham.NguoiKetthuc == globalVariables.UserName);
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
        private void frm_KCB_LSKCB_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.F4)
            {
                pnlThongtinBNKCB.Height = pnlThongtinBNKCB.Height == 0 ? 160 : 0;
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
            ModifyCommmands();
            HienThiChuyenCan();
            ShowResult();
            ResetNhominCLS();
            pnlAttachedFiles.Height = Utility.isValidGrid(grdAssignDetail) ? 60 : 0;
        }
        void LoadHTML()
        {
            string noidung = txtNoiDung.Text;
            webBrowser1.Document.InvokeScript("setValue", new[] { noidung });
        }
        DmucVungkhaosat vks = null;
        void ShowEditor(int id_VungKS)
        {
            pnlCKEditor.BringToFront();

            vks = DmucVungkhaosat.FetchByID(id_VungKS);
            txtNoiDung.Text = vks != null ? vks.MotaHtml : "";
            richtxtKetluan.Text = Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "ket_luan"));
            txtDenghi.Text = Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "de_nghi"));
            string html = Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "mo_ta_HTML"), "");
            if (html != "")
                txtNoiDung.Text = html;
            timer1.Start();
            LoadHTML();
        }
        private void ShowResult()
        {
            try
            {
                uiTabKqCls.Width = 0;
                mnuNhapKQCDHA.Visible = false;
                LoadAttachedFiles();
                CKEditorInput = Utility.GetValueFromGridColumn(grdAssignDetail, KcbChidinhclsChitiet.Columns.ResultType)=="1";
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

                int idChitietchidinh =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietchidinh), 0);
                int idChidinh =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChidinh), 0);
                string ketluanHa =
                  Utility.sDbnull(
                      Utility.GetValueFromGridColumn(grdAssignDetail, "ketluan_ha"), "");
                string maloaiDichvuCls =
                    Utility.sDbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaLoaidichvu), "XN");
                string maChidinh =
                    Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaChidinh),
                                    "XN");
                string maBenhpham =
                    Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaBenhpham),
                                    "XN");
                if (trangthaiChitiet <= 2)
                    //0=Mới chỉ định;1=Đã chuyển CLS;2=Đang thực hiện;3= Đã có kết quả CLS;4=Đã xác nhận kết quả
                {
                    if (maloaiDichvuCls != "XN")
                    {
                        uiTabKqCls.Width = 0;
                        mnuNhapKQCDHA.Visible = true;
                    }
                    else
                    {
                        
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
                    Utility.ShowColumns(grdKetqua, coChitiet == 1 ? lstKQCochitietColumns : lstKQKhongchitietColumns);
                    //Lấy dữ liệu CLS
                    if (maloaiDichvuCls == "XN")
                    {
                        DataTable dt =
                            SPs.ClsTimkiemketquaXNChitiet(objLuotkham.MaLuotkham, maChidinh, maBenhpham, idChidinh,
                                                          coChitiet, idDichvu, idChitietdichvu).GetDataSet().Tables[0];
                        Utility.SetDataSourceForDataGridEx_Basic(grdKetqua, dt, true, true, "1=1",
                                                                 "stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");

                        Utility.focusCell(grdKetqua, KcbKetquaCl.Columns.KetQua);
                    }
                    else //XQ,SA,DT,NS
                    {
                        mnuNhapKQCDHA.Visible = true;
                        if (CKEditorInput)
                        {
                            ShowEditor(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "id_VungKS"), 0));
                        }
                        else
                        {
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
                DataTable dtDynamicData = SPs.HinhanhGetDynamicFieldsValues(id_vungks, v_id_chitietchidinh).GetDataSet().Tables[0];

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

       

        private void LayThongTinQuaMaLanKham()
        {
            var dtPatient = new DataTable();
           // Utility.FreeLockObject(m_strMaLuotkham);
            string _patient_Code = Utility.AutoFullPatientCode(txtPatient_Code.Text);
            ClearControl();
            txtPatient_Code.Text = _patient_Code;
            if (grdList.RowCount > 0 && PropertyLib._ThamKhamProperties.Timtrenluoi)
            {
                DataRow[] arrData_tempt = null;
                arrData_tempt =
                    m_dtDanhsachbenhnhanthamkham.Select(KcbLuotkham.Columns.MaLuotkham + "='" + _patient_Code +
                                                        "'");
                if (arrData_tempt.Length > 0)
                {
                    string _status = radChuaKham.Checked ? "0" : "1";
                    string regStatus = Utility.sDbnull(arrData_tempt[0][KcbDangkyKcb.Columns.TrangThai], "0");
                    if (_status != regStatus)
                    {
                        if (regStatus == "1") radDaKham.Checked = true;
                        else
                            radChuaKham.Checked = true;
                    }
                    Utility.GotoNewRowJanus(grdList, KcbLuotkham.Columns.MaLuotkham, _patient_Code);
                    if (Utility.isValidGrid(grdList)) grdList_DoubleClick(grdList, new EventArgs());
                    return;
                }
            }

            dtPatient = _KCB_THAMKHAM.TimkiemBenhnhan(txtPatient_Code.Text,
                                                      Utility.Int32Dbnull(cboPhongKhamNgoaiTru.SelectedValue, -1),
                                                      0, 0);

            DataRow[] arrPatients = dtPatient.Select(KcbLuotkham.Columns.MaLuotkham + "='" + _patient_Code + "'");
            if (arrPatients.GetLength(0) <= 0)
            {
                if (dtPatient.Rows.Count > 1)
                {
                    var frm = new frm_DSACH_BN_TKIEM(g_vsArgs,100);
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
            DataTable dt_Patient = _KCB_THAMKHAM.TimkiemThongtinBenhnhansaukhigoMaBN
                (txtPatient_Code.Text, Utility.Int32Dbnull(cboPhongKhamNgoaiTru.SelectedValue, -1),
                 globalVariables.MA_KHOA_THIEN);

            grdList.DataSource = null;
            grdList.DataSource = dt_Patient;
            if (dt_Patient.Rows.Count > 0)
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
                objkcbdangky = null;
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
                    LayThongTinQuaMaLanKham();
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

        private void txtTenBenhChinh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!InValiMaBenh(txtMaBenhChinh.Text))
                {
                    DSACH_ICD(txtTenBenhChinh, DmucChung.Columns.Ten, 0);
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
                        txtMaBenhChinh.Text = "";
                        txtMaBenhChinh.Text = Utility.sDbnull(dataRows[0][DmucBenh.Columns.MaBenh], "");
                        hasMorethanOne = false;
                        txtMaBenhChinh_TextChanged(txtMaBenhChinh, new EventArgs());
                        txtMaBenhChinh.Focus();
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
                            isLike = false;
                            txtMaBenhChinh.Text = "";
                            txtMaBenhChinh.Text =
                                Utility.sDbnull(lstSelectedRows[0].Cells[DmucBenh.Columns.MaBenh].Value, "");
                            hasMorethanOne = false;
                            txtMaBenhChinh_TextChanged(txtMaBenhChinh, new EventArgs());
                            txtMaBenhChinh_KeyDown(txtMaBenhChinh, new KeyEventArgs(Keys.Enter));
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

        private void txtMaBenhChinh_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter )
            //{
            //    if (!InValiMaBenh(txtMaBenhChinh.Text))
            //    {
            //        DSACH_ICD(txtMaBenhChinh, DmucChung.Columns.Ma, 0);
            //        hasMorethanOne = false;
            //         txtMaBenhphu.Focus();
            //    }
            //    else
            //        txtMaBenhphu.Focus();
            //}
            if (e.KeyCode == Keys.Enter)
            {
                if (hasMorethanOne)
                {
                    DSACH_ICD(txtMaBenhChinh, DmucChung.Columns.Ma, 0);
                    hasMorethanOne = false;
                    //txtMaBenhphu.Focus();
                }
                //else
                //    txtMaBenhphu.Focus();
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
                setChanDoan();
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

        private void cmdInTTDieuTri_Click(object sender, EventArgs e)
        {
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

            if (IN_TTAT_DTRI_NGOAITRU())
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
                            ModifyByLockStatus(objLuotkham.Locked);
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
                                    gridExRow.Cells[KcbLuotkham.Columns.Locked].Value = (byte?) 1;
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
                                    tenNhanvien = Utility.sDbnull(objStaff["ten_nhanvien"],"");
                                cmdUnlock.Visible = objLuotkham.TrangthaiNoitru == 0 &&
                                                    objLuotkham.Locked.ToString() == "1";
                                cmdUnlock.Enabled = cmdUnlock.Visible &&
                                                    (Utility.Coquyen("quyen_mokhoa_tatca") ||
                                                     objLuotkham.NguoiKetthuc == globalVariables.UserName);
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
                Utility.ShowMsg("Lỗi khi nạp báo cáo " + fileName + "-->\n" + ex.Message);
                //ErrMsg = ex.Message;
                return null;
            }
        }

        private bool IN_TTAT_DTRI_NGOAITRU()
        {
            try
            {
                string icdName = "";
                string icdCode = "";
                DataSet dsData = _KCB_THAMKHAM.LaythongtinInphieuTtatDtriNgoaitru(objkcbdangky.IdKham,txtPatient_Code.Text.Trim());
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
                    vDtData = SPs.KcbThamkhamLaythongtinInphieutomtat(objkcbdangky.IdKham).GetDataSet().Tables[0];
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
        private void cmdNhapVien_Click(object sender, EventArgs e)
        {
            try
            {
                if (objLuotkham.TrangthaiNoitru > 1)
                {
                    Utility.ShowMsg(
                        "Bệnh nhân đã được điều trị nội trú nên bạn chỉ có thể xem và không được phép sửa các thông tin thăm khám");
                    return;
                }
                var frm = new frm_Nhapvien();
                frm.CallfromParent = true;
                frm.id_kham = Utility.Int32Dbnull(txtExam_ID.Text, -1);

                frm.objLuotkham = objLuotkham;
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
                    cmdChuyenPhong.Visible = objLuotkham.TrangthaiNoitru == 0;
                    //Tạm thời comment bỏ 2 dòng dưới để luôn hiển thị 2 nút này(190803)
                    //cmdInTTDieuTri.Visible = objLuotkham.TrangthaiNoitru == 0;
                    //cmdInphieuhen.Visible = objLuotkham.TrangthaiNoitru == 0;
                    cmdNhapVien.Tag = objLuotkham.TrangthaiNoitru == 0 ? "0" : "1";
                    cmdNhapVien.Text = objLuotkham.TrangthaiNoitru == 0 ? "Nhập viện" : "Cập nhật";
                    cmdUnlock.Visible = objLuotkham.TrangthaiNoitru == 0 && objLuotkham.Locked.ToString() == "1";
                    cmdUnlock.Enabled = cmdUnlock.Visible &&
                                        (Utility.Coquyen("quyen_mokhoa_tatca") ||
                                         objLuotkham.NguoiKetthuc == globalVariables.UserName);
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


        private bool IsValidHuyNhapVien()
        {
            SqlQuery query = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .And(KcbLuotkham.Columns.TrangthaiNoitru).IsLessThanOrEqualTo(0);
            if (query.GetRecordCount() > 0)
            {
                Utility.SetMsg(lblMsg, "Bệnh nhân ngoại trú,Bạn không thể thực hiện chức năng này", true);
                // cmdCancelNhapVien.Focus();
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

        private void cmdHuyNhapVien_Click(object sender, EventArgs e)
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
                        cmdChuyenPhong.Visible = objLuotkham.TrangthaiNoitru == 0;
                        //Tạm thời comment bỏ 2 dòng dưới để luôn hiển thị 2 nút này(190803)
                        //cmdInTTDieuTri.Visible = objLuotkham.TrangthaiNoitru == 0;
                        //cmdInphieuhen.Visible = objLuotkham.TrangthaiNoitru == 0;
                        var objStaff =
                            new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.UserNameColumn).IsEqualTo(
                                Utility.sDbnull(objLuotkham.NguoiKetthuc, "")).ExecuteSingle<DmucNhanvien>();
                        string TenNhanvien = objLuotkham.NguoiKetthuc;
                        if (objStaff != null)
                            TenNhanvien = objStaff.TenNhanvien;
                        cmdUnlock.Visible = objLuotkham.TrangthaiNoitru == 0 && objLuotkham.Locked.ToString() == "1";
                        cmdUnlock.Enabled = cmdUnlock.Visible &&
                                            (Utility.Coquyen("quyen_mokhoa_tatca") ||
                                             objLuotkham.NguoiKetthuc == globalVariables.UserName);
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
            try
            {
                int status = radChuaKham.Checked ? 0 : 1;
                m_dtDanhsachbenhnhanthamkham.DefaultView.RowFilter = "1=1";
                m_dtDanhsachbenhnhanthamkham.DefaultView.RowFilter = "trang_thai=" + status;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
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
            var frm = new frm_Properties(PropertyLib._HISQMSProperties);
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

        private void cmdInphieuhen_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtphienhen =
                    SPs.KcbThamkhamInphieuhenBenhnhan(m_strMaLuotkham, Utility.Int64Dbnull(txtPatient_ID.Text, -1)).
                        GetDataSet().Tables[0];
                //THU_VIEN_CHUNG.CreateXML(dtphienhen, "thamkham_inphieuhen_benhnhan.xml");
                KcbInphieu.INPHIEU_HEN(dtphienhen, "PHIẾU HẸN KHÁM");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
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
            try
            {
                Utility.WaitNow(this);
                PushDataIntoRIS();
                return;
                var lstIdchidinhchitiet = new List<string>();
                if (objLuotkham != null)
                {
                    // cmdConform = 1 thì là chuyển cận
                    if (cmdConfirm.Tag.ToString() == "1")
                    {
                        int id_chidinh =
                            Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                        DataRow[] arrDr =
                            m_dtAssignDetail.Select("trangthai_chuyencls=0 and id_chidinh = '" + id_chidinh + "' ");
                        if (arrDr.Length == 0)
                        {
                            Utility.SetMsg(lblMsg, string.Format("Các chỉ định CLS đã được chuyển hết"), false);
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
                            Utility.SetMsg(lblMsg,
                                           string.Format("Bạn vừa chuyển CLS thành công"),
                                           false);
                            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("CHOPHEP_BACSY_CHUYENKETNOI_HISLIS", "0", false) ==
                                "1")
                            {
                                #region Hàm bác sỹ  thực hiện đẩy kết nối his - lis

                                DataSet dsData =
                                    SPs.HisLisLaydulieuchuyensangLis(dtInput_Date.Value.ToString("dd/MM/yyyy"),
                                                                     objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).
                                        GetDataSet();
                                DataTable dt2LIS = dsData.Tables[1].Copy();
                                List<long> lstIchidinh = (from q in grdAssignDetail.GetDataRows()
                                                          where Utility.Int64Dbnull(
                                                                  q.Cells[KcbChidinhcl.Columns.IdChidinh].Value, 0) == id_chidinh
                                                          select Utility.Int64Dbnull(
                                                                  q.Cells[KcbChidinhcl.Columns.IdChidinh].Value, 0)).
                                    ToList
                                    <long>();
                                List<DataRow> lstData2Send = (from p in dsData.Tables[0].AsEnumerable()
                                                              where
                                                                  lstIchidinh.Contains(
                                                                      Utility.Int64Dbnull(
                                                                          p[KcbChidinhclsChitiet.Columns.IdChidinh]))
                                                                  && Utility.Int64Dbnull(p["trang_thai"], 0) == 1
                                                              select p).ToList<DataRow>();
                                List<DataRow> lstData2Send_real = (from p in dsData.Tables[1].AsEnumerable()
                                                                   where
                                                                       lstIchidinh.Contains(
                                                                           Utility.Int64Dbnull(
                                                                               p[KcbChidinhclsChitiet.Columns.IdChidinh]))
                                                                       && Utility.Int64Dbnull(p["trang_thai"], 0) == 1
                                                                   select p).ToList<DataRow>();
                                if (lstData2Send.Any())
                                {
                                    dt2LIS = lstData2Send_real.CopyToDataTable();
                                    lstIdchidinhchitiet = (from p in lstData2Send
                                                           select
                                                               Utility.sDbnull(
                                                                   p[KcbChidinhclsChitiet.Columns.IdChitietchidinh], 0))
                                        .
                                        Distinct().ToList();
                                    int recoder =
                                        RocheCommunication.WriteOrderMessage(
                                            THU_VIEN_CHUNG.Laygiatrithamsohethong("ASTM_ORDERS_FOLDER",
                                                                                  @"\\192.168.1.254\Orders", false),
                                            dt2LIS);
                                    if (recoder == 0) //Thành công
                                    {
                                        SPs.HisLisCapnhatdulieuchuyensangLis(
                                            string.Join(",", lstIdchidinhchitiet.ToArray()), 2, 1).Execute();
                                        dsData.Tables[0].AsEnumerable()
                                            .Where(
                                                c =>
                                                lstIdchidinhchitiet.Contains(
                                                    Utility.sDbnull(
                                                        c.Field<long>(KcbChidinhclsChitiet.Columns.IdChitietchidinh))))
                                            .ToList()
                                            .ForEach(c1 =>
                                                         {
                                                             c1["trang_thai"] = 2;
                                                             //   c1["ten_trangthai"] = "Đang thực hiện";
                                                         });
                                        dsData.Tables[1].AsEnumerable()
                                            .Where(
                                                c =>
                                                lstIdchidinhchitiet.Contains(
                                                    Utility.sDbnull(
                                                        c.Field<long>(KcbChidinhclsChitiet.Columns.IdChitietchidinh))))
                                            .ToList()
                                            .ForEach(c1 =>
                                                         {
                                                             c1["trang_thai"] = 2;
                                                             // c1["ten_trangthai"] = "Đang thực hiện";
                                                         });
                                        dsData.AcceptChanges();
                                        Utility.SetMsg(lblMsg,
                                                       string.Format(
                                                           "Các dữ liệu dịch vụ cận lâm sàng của Bệnh nhân đã được gửi thành công sang LIS"),
                                                       false);
                                    }
                                }

                                #endregion
                            }
                        }
                    }
                    else
                    {
                        // = 2 là hủy chuyển cận lâm sàng
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
                                //SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                                //    .Where(KcbChidinhclsChitiet.Columns.IdChidinh).In(
                                //        new Select(KcbChidinhcl.Columns.IdChidinh).From(KcbChidinhcl.Schema).Where(
                                //            KcbChidinhcl.Columns.MaLuotkham)
                                //            .IsEqualTo(txtPatient_Code.Text)
                                //            .And(KcbChidinhcl.Columns.IdBenhnhan)
                                //            .IsEqualTo(Utility.Int32Dbnull(txtPatient_ID.Text)))
                                //    .And(KcbChidinhclsChitiet.Columns.TrangThai).In(1, 2, 3)
                                //    .And(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(
                                //        Utility.Int32Dbnull(id_chidinh));
                                //hasFound = sqlQuery.GetRecordCount() > 0;
                                if (dt.Rows.Count <= 0)
                                {
                                    Utility.SetMsg(lblMsg, string.Format("Không có chỉ định CLS có thể hủy chuyển"),
                                                   false);
                                    Utility.DefaultNow(this);
                                    return;
                                }
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
                                //int result = new Update(KcbChidinhclsChitiet.Schema)
                                //    .Set(KcbChidinhclsChitiet.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                                //    .Set(KcbChidinhclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                //    .Set(KcbChidinhclsChitiet.Columns.TrangThai).EqualTo(0)
                                //    .Where(KcbChidinhclsChitiet.Columns.TrangThai).In(1, 2, 3)
                                //    .And(KcbChidinhclsChitiet.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                                //    .And(KcbChidinhclsChitiet.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                //    .And(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(
                                //        Utility.Int32Dbnull(id_chidinh))
                                //    .Execute();

                                Utility.SetMsg(lblMsg,  string.Format("Bạn vừa hủy chuyển CLS thành công"),  false);
                            }
                        }
                        if (cmdConfirm.Tag.ToString() == "3")
                        {
                        }
                    }
                }
                ModifyCommmands();
                HienThiChuyenCan();
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
            dtpNgayHen.Value = dtpCreatedDate.Value.AddDays(Utility.Int32Dbnull(txtSongaydieutri.Text, 0));
            txtSoNgayHen.Text = txtSongaydieutri.Text;
        }

        private void txtSoNgayHen_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSoNgayHen.Text))
            {
                txtSongaydieutri.Text = txtSoNgayHen.Text;
                dtpNgayHen.Value = dtpCreatedDate.Value.AddDays(Utility.Int32Dbnull(txtSongaydieutri.Text, 0));
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
                //if (e.Column.Key == "Ghi_Chu")
                //{
                //    string GhiChu = Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.MotaThem), "");

                //    new Update(KcbChidinhclsChitiet.Schema)
                //        .Set(KcbChidinhclsChitiet.Columns.MotaThem).EqualTo(GhiChu)
                //        .Set(KcbChidinhclsChitiet.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                //        .Set(KcbChidinhclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                //        .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(
                //            Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChitietchidinh))).Execute
                //        ();
                //    grdAssignDetail.CurrentRow.BeginEdit();
                //    grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NgaySua].Value = globalVariables.SysDate;
                //    grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NguoiSua].Value = globalVariables.UserName;
                //    grdAssignDetail.CurrentRow.EndEdit();
                //}
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
                if (!IsValidCheckedAssignDetails()) return;
                string maChidinh = Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhcl.Columns.MaChidinh));
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
            if (grdAssignDetail.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện xóa chỉ định CLS", "Thông báo",
                                MessageBoxIcon.Warning);
                grdAssignDetail.Focus();
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

            if (item != null && Utility.Int32Dbnull(item.TrangthaiNoitru, -1) >= 1)
            {
                Utility.ShowMsg(
                    "Bệnh nhân đã được điều trị nội trú. Bạn chỉ có thể xem thông tin và không được phép làm các công việc liên quan đến ngoại trú",
                    "Thông báo");
                cmdSave.Focus();
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
                //int i = m_dtAssignDetail.Select(KcbChidinhclsChitiet.Columns.IdChitietchidinh + " = " + idChidinhchitiet +
                //                            " And " + KcbChidinhclsChitiet.Columns.TrangThai + " >= 1" + 
                //                            " OR " + KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan +" >=1").Count();
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(idChidinhchitiet)
                    .AndExpression(KcbChidinhclsChitiet.Columns.TrangThai).IsGreaterThanOrEqualTo(1).Or(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1).CloseExpression();
                if (sqlQuery.GetRecordCount() > 0)
                {
                    bCancel = true;
                    break;
                }
            }
            if (bCancel)
            {
                Utility.ShowMsg(
                    "Chỉ định bạn chọn đã được chuyển làm cận lâm sàng hoặc đã có kết quả nên không thể xóa. Đề nghị kiểm tra lại");
                return false;
            }
            return true;
        }

        /// <summary>
        /// hàm thực hiện viễ xóa thông tin chỉ định
        /// </summary>
        private void PerforActionDeleteAssign()
        {
            string lstvalues = "";
            foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int idChidinhchitiet =
                    Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                        -1);
                int idChidinh = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value,
                                                     -1);
                _KCB_CHIDINH_CANLAMSANG.XoaChiDinhCLSChitiet(idChidinhchitiet);
                lstvalues += idChidinhchitiet + ",";
            }
            DataRow[] rows;
            if (lstvalues.Length > 0) lstvalues = lstvalues.Substring(0, lstvalues.Length - 1);
            rows = m_dtAssignDetail.Select(KcbChidinhclsChitiet.Columns.IdChitietchidinh + " IN (" + lstvalues + ")");
                // UserName is Column Name
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
                int idChitietDichvu =
                    Utility.Int32Dbnull(
                        grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietdichvu].Value,
                        -1);
                int idChidinh =
                    Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value,
                                        -1);
                _KCB_CHIDINH_CANLAMSANG.XoaChiDinhCLSChitiet(idChidinhchitiet);
                Utility.Log(Name, globalVariables.UserName,
                                   string.Format(
                                       "Xóa chỉ định {0} của bệnh nhân có mã lần khám: {1} và mã bệnh nhân là: {2}",
                                       idChitietDichvu, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
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
            if (!Utility.isValidGrid(grdAssignDetail)) return false;
            int idChidinh = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), "-1");
            SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                .Where(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(idChidinh).And(
                    KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsGreaterThanOrEqualTo(1)
                //.Or(KcbChidinhclsChitiet.Columns.TrangThai).IsGreaterThanOrEqualTo(1)
                .And(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(idChidinh);

            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Phiếu này đã thanh toán hoặc chuyển đã cận \n Mời bạn thêm phiếu mới để thực hiện",
                                "Thông báo");
                cmdInsertAssign.Focus();
                return false;
            }

            SqlQuery sqlQueryKq = new Select().From(KcbKetquaCl.Schema)
                .Where(KcbKetquaCl.Columns.MaChidinh).IsEqualTo(idChidinh);

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
                var frm = new frm_KCB_CHIDINH_CLS("-GOI,-TIEN,-CHIPHITHEM", 0);
                frm.noitru = 0;
                frm.objCongkham = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txtReg_ID.Text));
                frm.Exam_ID = Utility.Int32Dbnull(txtExam_ID.Text, -1);
                frm.txtidkham.Text = Utility.sDbnull(txtReg_ID.Text);
                frm.objLuotkham = objLuotkham; // CreatePatientExam();
                frm.objBenhnhan = objBenhnhan;
                frm.m_eAction = action.Update;
                frm.txtAssign_ID.Text = Utility.sDbnull(
                    grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), "-1");
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
            if (objLuotkham == null)
            {
                Utility.ShowMsg(
                    "Bạn phải chọn Bệnh nhân trước khi thực hiện các công việc chỉ định Thăm khám, CLS, Kê đơn");
                return false;
            }
            if (objkcbdangky == null)
            {
                Utility.ShowMsg(
                    "Bệnh nhân bạn chọn chưa đăng ký phòng khám nên không được phép thăm khám. Mời bạn kiểm tra lại");
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
                if (Utility.Coquyen("quyen_suaphieuchidinhcls") ||  Utility.Int32Dbnull(objkcbdangky.IdBacsikham, -1) <= 0 || objkcbdangky.IdBacsikham == globalVariables.gv_intIDNhanvien)
                {
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
                        if (PropertyLib._ThamKhamProperties.TudongthugonCLS)
                            grdAssignDetail.GroupMode = GroupMode.Collapsed;
                        Utility.GotoNewRowJanus(grdAssignDetail, KcbChidinhclsChitiet.Columns.IdChidinh,
                                                frm.txtAssign_ID.Text);
                        if (PropertyLib._HISCLSProperties.InNgaySauKhiLuu && PropertyLib._HISCLSProperties.ThoatSauKhiLuu)
                            cmdPrintAssign.PerformClick();
                    }
                    frm.Dispose();
                    frm = null;
                    GC.Collect();
                }
                else
                {
                    Utility.ShowMsg(
                        string.Format(
                            "Bệnh nhân này đã được khám bởi Bác sĩ khác nên bạn không được phép thêm phiếu chỉ định dịch vụ "));
                    return;
                }
              
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
                List<long> lstSelectedPrint = (from p in grdAssignDetail.GetCheckedRows().AsEnumerable()
                                               select Utility.Int64Dbnull(p.Cells["id_chitietchidinh"].Value, 0)).ToList();
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
                if (cboServicePrint.SelectedIndex > 0 || chkIntach.Checked)
                {
                    actionResult = KcbInphieu.InTachToanBoPhieuCls(lstSelectedPrint,(int) objLuotkham.IdBenhnhan,
                                                                     objLuotkham.MaLuotkham, vAssignId,
                                                                     vAssignCode, nhomcls, Utility.sDbnull(cboServicePrint.SelectedValue, "ALL"),
                                                                     cboServicePrint.SelectedIndex, chkIntach.Checked,
                                                                     ref mayin);
                }
                else
                {
                    actionResult = KcbInphieu.InphieuChidinhCls((int) objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham,
                                                                  vAssignId,
                                                                  vAssignCode, nhomincls, cboServicePrint.SelectedIndex,
                                                                  chkIntach.Checked,
                                                                  ref mayin);
                }
                if (actionResult == ActionResult.Success)
                {
                    if (PropertyLib._ThamKhamProperties.Chophepchuyencansaukhiinphieu && cmdConfirm.Visible)
                    {
                        if (cmdConfirm.Tag.ToString() == "1") cmdConfirm.PerformClick();
                    }
                }
                if (mayin != "") cboLaserPrinters.Text = mayin;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                GC.Collect();
            }
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
                            Utility.ShowMsg("Dịch vụ này chưa gán vùng khảo sát nên không thể nhập kết quả.\nNhấn OK để thực hiện gán vùng khảo sát cho dịch vụ đang chọn");
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
                        if (FtpClient != null) FtpClient.CurrentDirectory = _FtpClientCurrentDirectory;
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
                        frm._IsReadonly = true;
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
                        .Where(KcbDonthuoc.Columns.IdKham).IsEqualTo(Utility.sDbnull(objkcbdangky.IdKham)).
                        //.And(KcbDonthuoc.IdBenhnhanColumn).IsEqualTo(Utility.sDbnull(objLuotkham.IdBenhnhan)).
                        ExecuteAsCollection<KcbDonthuocCollection>();

                IEnumerable<KcbDonthuoc> lstPres1 = from p in lstPres
                                                    where p.IdKham == objkcbdangky.IdKham
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
                Utility.ShowMsg("Lỗi khi kiểm tra số lượng đơn thuốc của lần khám\n" + ex.Message);
                return false;
            }
        }

        private void cmdCreateNewPres_Click(object sender, EventArgs e)
        {
            if (!CheckPatientSelected()) return;
            if (!cmdCreateNewPres.Enabled) return;
            if (Utility.Coquyen("quyen_suadonthuoc") || Utility.Int32Dbnull(objkcbdangky.IdBacsikham, -1) <= 0  || objkcbdangky.IdBacsikham == globalVariables.gv_intIDNhanvien)
            {

            }
            else
            {
                Utility.ShowMsg(
                    string.Format(
                        "Bệnh nhân này đã được khám bởi Bác sĩ khác nên bạn không được phép thêm đơn thuốc cho Bệnh nhân"));
                return;
            }
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
                cmdLuuChandoan.PerformClick();
                // KeDonThuocTheoDoiTuong();
                var frm = new frm_KCB_KE_DONTHUOC("THUOC");
                frm.em_Action = action.Insert;
                frm.objLuotkham = objLuotkham;
                
                frm._KcbCDKL = _KcbChandoanKetluan;
                frm._MabenhChinh = txtMaBenhChinh.Text;
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
                frm.txtPres_ID.Text = "-1";
                frm.dtNgayKhamLai.MinDate = dtpCreatedDate.Value;
                frm._ngayhenkhamlai = dtpCreatedDate.Value.ToString("yyMMdd") == dtpNgayHen.Value.ToString("yyMMdd") ? "" : dtpNgayHen.Text;
                frm.noitru = 0;
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();

                if (!frm.m_blnCancel)
                {
                    txtMaBenhChinh.Text = frm._MabenhChinh;
                    txtChanDoan._Text = frm._Chandoan;
                    dt_ICD_PHU = frm.dt_ICD_PHU;
                    if (frm._KcbCDKL != null)
                        _KcbChandoanKetluan = frm._KcbCDKL;
                    if (frm.chkNgayTaiKham.Checked)
                    {
                        dtpNgayHen.Value = frm.dtNgayKhamLai.Value;
                        cmdLuuChandoan.PerformClick();
                    }
                    else
                    {
                        dtpNgayHen.Value = dtpCreatedDate.Value;
                        cmdLuuChandoan.PerformClick();
                    }
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
            //        Laythongtinchidinhngoaitru();
            //        Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuocChitiet.Columns.IdDonthuoc,
            //                                Utility.sDbnull(frm.txtPres_ID.Text));
            //        ModifyCommmands();
            //    }
            //}
        }

        /// <summary>
        /// ham thực hiện việc update thông tin của thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdUpdatePres_Click(object sender, EventArgs e)
        {
            if (!cmdUpdatePres.Enabled) return;
            UpdateDonThuoc();
        }

        private bool Donthuoc_DangXacnhan(int pres_id)
        {
            var _item =
                new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.IdDonthuocColumn).IsEqualTo(pres_id).And(
                    KcbDonthuoc.TrangThaiColumn).IsEqualTo(1).ExecuteSingle<KcbDonthuoc>();
            if (_item != null) return true;
            return false;
        }

        private void UpdateDonThuoc()
        {
            try
            {
                if (grdPresDetail.CurrentRow != null && grdPresDetail.CurrentRow.RowType == RowType.Record)
                {
                    if (objLuotkham != null)
                    {
                        int Pres_ID = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
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
                            frm.em_Action = action.Update;
                            frm._KcbCDKL = _KcbChandoanKetluan;
                            frm._MabenhChinh = txtMaBenhChinh.Text;
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
                            frm.dtNgayKhamLai.MinDate = dtpCreatedDate.Value;
                            frm._ngayhenkhamlai = dtpCreatedDate.Value.ToString("yyMMdd") == dtpNgayHen.Value.ToString("yyMMdd") ? "" : dtpNgayHen.Text;

                            frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                            frm.ShowDialog();
                            if (!frm.m_blnCancel)
                            {
                                txtMaBenhChinh.Text = frm._MabenhChinh;
                                txtChanDoan._Text = frm._Chandoan;
                                dt_ICD_PHU = frm.dt_ICD_PHU;
                                if (frm._KcbCDKL != null) _KcbChandoanKetluan = frm._KcbCDKL;
                                if (frm.chkNgayTaiKham.Checked)
                                {
                                    dtpNgayHen.Value = frm.dtNgayKhamLai.Value;
                                    cmdLuuChandoan.PerformClick();
                                }
                                else
                                {
                                    dtpNgayHen.Value = dtpCreatedDate.Value;
                                    cmdLuuChandoan.PerformClick();
                                }

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
            DataRow[] arrDr =
                m_dtPresDetail.Select(KcbDonthuocChitiet.Columns.IdDonthuoc + "=" + IdDonthuoc.ToString() + " AND " +
                                      KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc.ToString()
                                      + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + don_gia.ToString());
            if (arrDr.Length > 0)
            {
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
            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                string stempt = "";
                int id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, 0m);
                int IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value, 0m);
                decimal dongia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, 0m);
                List<int> _temp = GetIdChitiet(IdDonthuoc, id_thuoc, dongia, ref stempt);
                s += "," + stempt;
                lstIdchitiet.AddRange(_temp);
                gridExRow.Delete();
                grdPresDetail.UpdateData();
            }
            _KCB_KEDONTHUOC.XoaChitietDonthuoc(s);
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
            if (grdPresDetail.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện việc xóa thông tin thuốc ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }

            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                if (Utility.Coquyen("quyen_suadonthuoc") ||
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
                cmdSave.Focus();
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
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong_off("KCB_THAMKHAM_TACHDONTHUOC", "TRUE", false) == "TRUE")
                {
                    int presId = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
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

        /// <summary>
        /// hàm thực hiện việc in đơn thuốc
        /// </summary>
        /// <param name="presID"></param>
        /// <param name="forcedTitle"></param>
        private void PrintPres(int presID, string forcedTitle)
        {
            DataTable v_dtData = _KCB_KEDONTHUOC.LaythongtinDonthuoc_In(presID);
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

        #endregion

        #region "Xử lý tác vụ của phần lưu thông tin "

        private void cmdSave_Click(object sender, EventArgs e)
        {
            DataTable dtkt = SPs.KcbGetthongtinLuotkham(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan).GetDataSet().Tables[0];
            if (dtkt.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tồn tại bệnh nhân! Bạn cần nạp lại thông tin dữ liệu", "Thông Báo");
                return;
            }
            else
            {
                PerformAction();
            }
            
        }

        private string GetDanhsachBenhphu()
        {
            var sMaICDPHU = new StringBuilder("");
            try
            {
                int recordRow = 0;

                if (dt_ICD.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_ICD_PHU.Rows)
                    {
                        if (recordRow > 0)
                            sMaICDPHU.Append(",");
                        sMaICDPHU.Append(Utility.sDbnull(row[DmucBenh.Columns.MaBenh], ""));
                        recordRow++;
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
        private KcbChandoanKetluan TaoDulieuChandoanKetluan()
        {
            try
            {

                SqlQuery sqlkt =
                    new Select().From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(
                        objkcbdangky.IdKham);
              
                _KcbChandoanKetluan = new KcbChandoanKetluan();
                if (_KcbChandoanKetluan == null || sqlkt.GetRecordCount() <= 0)
                {
                    _KcbChandoanKetluan.IsNew = true;
                    _KcbChandoanKetluan.IdChandoan = -1;
                    _KcbChandoanKetluan.NgayTao = globalVariables.SysDate;
                    _KcbChandoanKetluan.NguoiTao = globalVariables.UserName;
                }
                else
                {
                   KcbChandoanKetluan  objchuandoan =  sqlkt.ExecuteSingle<KcbChandoanKetluan>();
                    _KcbChandoanKetluan.IsNew = false;
                    _KcbChandoanKetluan.MarkOld();
                    _KcbChandoanKetluan.IdChandoan = Utility.Int64Dbnull(objchuandoan.IdChandoan, -1);
                    _KcbChandoanKetluan.NguoiSua = globalVariables.UserName;
                    _KcbChandoanKetluan.NgaySua = globalVariables.SysDate;
                    _KcbChandoanKetluan.IpMaysua = globalVariables.gv_strIPAddress;
                }
                _KcbChandoanKetluan.IdKham = Utility.Int64Dbnull(txtExam_ID.Text, -1);
                _KcbChandoanKetluan.MaLuotkham = Utility.sDbnull(m_strMaLuotkham, "");
                _KcbChandoanKetluan.IdBenhnhan = Utility.Int64Dbnull(txtPatient_ID.Text, "-1");
                _KcbChandoanKetluan.MabenhChinh = Utility.sDbnull(txtMaBenhChinh.Text, "");
                _KcbChandoanKetluan.MotaBenhchinh = Utility.sDbnull(txtTenBenhChinh.Text, "");
                _KcbChandoanKetluan.Nhommau = txtNhommau.Text;
                _KcbChandoanKetluan.Nhietdo = Utility.sDbnull(txtNhietDo.Text);
                _KcbChandoanKetluan.TrieuchungBandau = txtTrieuChungBD.Text;
                _KcbChandoanKetluan.Huyetap = txtHa.Text;
                _KcbChandoanKetluan.Mach = txtMach.Text;
                _KcbChandoanKetluan.Nhiptim = Utility.sDbnull(txtNhipTim.Text);
                _KcbChandoanKetluan.Nhiptho = Utility.sDbnull(txtNhipTho.Text);
                _KcbChandoanKetluan.Chieucao = Utility.sDbnull(txtChieucao.Text);
                _KcbChandoanKetluan.Cannang = Utility.sDbnull(txtCannang.Text);
                _KcbChandoanKetluan.HuongDieutri = txtHuongdieutri.Text; //.myCode.Trim();
                _KcbChandoanKetluan.SongayDieutri = (Int16) Utility.DecimaltoDbnull(txtSongaydieutri.Text, 0);
                _KcbChandoanKetluan.SoNgayhen = (Int16) Utility.DecimaltoDbnull(txtSoNgayHen.Text, 0);
                _KcbChandoanKetluan.Ketluan = Utility.sDbnull(txtKet_Luan.Text, "");
                _KcbChandoanKetluan.NhanXet = Utility.sDbnull(txtNhanxet.Text, "");
                _KcbChandoanKetluan.ChedoDinhduong = Utility.sDbnull(txtCheDoAn.Text, "");
                _KcbChandoanKetluan.ChisoIbm = Utility.DecimaltoDbnull(txtibm.Text,0);
                _KcbChandoanKetluan.ThilucMp = Utility.sDbnull(txtthiluc_mp.Text, "");
                _KcbChandoanKetluan.ThilucMt = Utility.sDbnull(txtthiluc_mt.Text, "");
                _KcbChandoanKetluan.NhanapMp = Utility.sDbnull(txtnhanap_mp.Text, "");
                _KcbChandoanKetluan.NhanapMt = Utility.sDbnull(txtnhanap_mt.Text, "");
                if (Utility.Int16Dbnull(txtBacsi.MyID, -1) > 0)
                    _KcbChandoanKetluan.IdBacsikham = Utility.Int16Dbnull(txtBacsi.MyID);
                else
                {
                    _KcbChandoanKetluan.IdBacsikham = globalVariables.gv_intIDNhanvien;
                }
                string sMaICDPHU = GetDanhsachBenhphu();
                _KcbChandoanKetluan.MabenhPhu = Utility.sDbnull(sMaICDPHU, "");
                if (objLuotkham != null)
                {
                    objLuotkham.MabenhPhu = Utility.sDbnull(sMaICDPHU, "");
                    objLuotkham.MabenhChinh = Utility.sDbnull(txtMaBenhChinh.Text, "");
                    objLuotkham.ChandoanKemtheo = Utility.sDbnull(txtChanDoanKemTheo, "");
                    objLuotkham.SongayHen = _KcbChandoanKetluan.SoNgayhen;
                }

                if (objkcbdangky != null)
                {
                    _KcbChandoanKetluan.IdKhoanoitru = Utility.Int32Dbnull(objkcbdangky.IdKhoakcb, -1);
                    _KcbChandoanKetluan.IdPhongkham = Utility.Int32Dbnull(objkcbdangky.IdPhongkham, -1);
                    DmucKhoaphong objDepartment =
                        DmucKhoaphong.FetchByID(Utility.Int32Dbnull(objkcbdangky.IdPhongkham, -1));
                    if (objDepartment != null)
                    {
                        _KcbChandoanKetluan.TenPhongkham = Utility.sDbnull(objDepartment.TenKhoaphong, "");
                    }
                }
                else
                {
                    _KcbChandoanKetluan.IdKhoanoitru = globalVariables.idKhoatheoMay;
                    _KcbChandoanKetluan.IdPhongkham = globalVariables.idKhoatheoMay;
                }
                //_KcbChandoanKetluan.IdKham = Utility.Int32Dbnull(txt_idchidinhphongkham.Text, -1);
             
                _KcbChandoanKetluan.NgayChandoan = dtpCreatedDate.Value;
                _KcbChandoanKetluan.Ketluan = Utility.sDbnull(txtKet_Luan.Text);
                _KcbChandoanKetluan.Chandoan = Utility.ReplaceString(txtChanDoan.Text);
                _KcbChandoanKetluan.ChandoanKemtheo = Utility.sDbnull(txtChanDoanKemTheo.Text);
                _KcbChandoanKetluan.IdPhieudieutri = -1;
                _KcbChandoanKetluan.Noitru = 0;
                return _KcbChandoanKetluan;
            }
            catch (Exception ex)
            {
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
                txtBacsi.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtPatient_Code.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn Bệnh nhân để thực hiện thăm khám", true);
                txtPatient_Code.Focus();
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

            if (_item != null && Utility.Int32Dbnull(_item.TrangthaiNoitru, -1) >= 2)
            {
                Utility.ShowMsg(
                    "Bệnh nhân đã được điều trị nội trú. Bạn chỉ có thể xem thông tin và không được phép làm các công việc liên quan đến ngoại trú",
                    "Thông báo");
                cmdSave.Focus();
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
                    txtSongaydieutri.Focus();
                    tabDiagInfo.SelectedTab = tabPageChanDoan;
                    return false;
                }
            }

            if (objLuotkham != null && objLuotkham.MaDoituongKcb == "BHYT" && (Utility.DoTrim(txtMaBenhChinh.Text) == "" || Utility.DoTrim(txtTenBenhChinh.Text) == ""))
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập Mã bệnh chính cho đối tượng BHYT trước khi kết thúc khám", true);
                tabDiagInfo.SelectedTab = tabPageChanDoan;
                txtMaBenhChinh.Focus();
                return false;
            }
            if (Utility.DoTrim(txtKet_Luan.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập kết quả khám cho bệnh nhân", true);
                tabDiagInfo.SelectedTab = tabPageChanDoan;
                txtKet_Luan.Focus();
                return false;
            }
            if (Utility.DoTrim(txtHuongdieutri.myCode) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập hướng điều trị cho bệnh nhân", true);
                tabDiagInfo.SelectedTab = tabPageChanDoan;
                txtHuongdieutri.Focus();
                return false;
            }
           
            //  = chkDaThucHien.Checked;
            return true;
        }

        /// <summary>
        /// Thực hiện thao tác Insert,Update,Delete tới CSDL theo m_enAction
        /// </summary>
        private void PerformAction()
        {
            try
            {
                //Kiểm tra tính hợp lệ của dữ liệu trước khi thêm mới
                cmdSave.Enabled = false;
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
                                "Số ngày cho thuốc vượt quá hạn thẻ BHYT của bệnh nhân {0}. \n Có đồng ý tiếp tục kết thúc không?",
                                objBenhnhan.TenBenhnhan), "Cảnh Báo", true))
                    {
                        return;
                    }
                }
                if (objLuotkham != null)
                    chkDaThucHien.Visible = chkDaThucHien.Checked = true;
                objkcbdangky.TrangThai = 1;
                DataRow[] arrDr = m_dtDanhsachbenhnhanthamkham.Select("id_kham=" + txtReg_ID.Text);
                if (arrDr.Length > 0)
                {
                    arrDr[0]["trang_thai"] = chkDaThucHien.Checked ? 1 : 0;
                }
                objkcbdangky.IdBacsikham = Utility.Int16Dbnull(txtBacsi.MyID, -1);
                objLuotkham.TrieuChung = txtTrieuChungBD.Text;
                if (!THU_VIEN_CHUNG.IsBaoHiem((byte)objLuotkham.IdLoaidoituongKcb))
                //Đối tượng dịch vụ được khóa ngay sau khi kết thúc khám
                {
                    objLuotkham.NguoiKetthuc = chkDaThucHien.Checked ? globalVariables.UserName : "";
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
                    objLuotkham.NguoiKetthuc = chkDaThucHien.Checked ? globalVariables.UserName : "";
                    objLuotkham.NgayKetthuc = globalVariables.SysDate;
                }
                objLuotkham.SongayDieutri = Utility.Int32Dbnull(txtSongaydieutri.Text, 0);
                objLuotkham.HuongDieutri = Utility.sDbnull(txtHuongdieutri.Text, "");
                objLuotkham.KetLuan = Utility.sDbnull(txtKet_Luan.Text);

                ActionResult actionResult =
                    _KCB_THAMKHAM.UpdateExamInfo(
                        TaoDulieuChandoanKetluan(), objkcbdangky, objLuotkham);
                switch (actionResult)
                {
                    case ActionResult.Success:
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
                            gridExRow.Cells[KcbLuotkham.Columns.NguoiKetthuc].Value = globalVariables.UserName;
                            gridExRow.Cells[KcbLuotkham.Columns.NgayKetthuc].Value = globalVariables.SysDate;
                            //gridExRow.EndEdit();
                            grdList.UpdateData();
                            Utility.GotoNewRowJanus(grdList, KcbDangkyKcb.Columns.IdKham,
                                                    Utility.sDbnull(txt_idchidinhphongkham.Text));
                        }
                        //Tạm thời comment bỏ 2 dòng dưới để luôn hiển thị 2 nút này(190803)
                        //cmdInTTDieuTri.Visible = objLuotkham.TrangthaiNgoaitru >= 1;
                        //cmdInphieuhen.Visible = objLuotkham.TrangthaiNgoaitru >= 1;
                        txtbenhan.Visible = true &&
                                                THU_VIEN_CHUNG.Laygiatrithamsohethong("BENH_AN", "0", false) == "1" &&
                                                (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_ICD_BENH_AN_NGOAI_TRU",
                                                                                       "ALL", false) == "ALL" ||
                                                 THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_ICD_BENH_AN_NGOAI_TRU",
                                                                                       "ALL", false).Contains(
                                                                                           Utility.DoTrim(
                                                                                               txtMaBenhChinh.Text)));
                        //   lblBenhan.Visible = cboChonBenhAn.Visible;


                        //Tự động ẩn BN về tab đã khám
                        int Status = radChuaKham.Checked ? 0 : 1;
                        if (Status == 0)
                        {
                            m_dtDanhsachbenhnhanthamkham.DefaultView.RowFilter = "1=1";
                            m_dtDanhsachbenhnhanthamkham.DefaultView.RowFilter = "trang_thai=" + Status;
                        }
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
                        if (cmdNhapVien.Visible && objLuotkham.TrangthaiNoitru == 0 &&
                            txtHuongdieutri.myCode.ToUpper() == THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_MAHUONGDIEUTRI_NOITRU", false).ToUpper())
                        {
                            cmdNhapVien_Click(cmdNhapVien, new EventArgs());
                        }
                        Utility.Log(Name, globalVariables.UserName, string.Format(
                                               "Bệnh nhân có mã lần khám {0} và mã bệnh nhân {1} được kết thúc bởi {2}  ",
                                               objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, globalVariables.UserName),
                                           newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                        cmdUnlock.Visible = objLuotkham.TrangthaiNoitru == 0 && objLuotkham.Locked.ToString() == "1";
                        cmdUnlock.Enabled = cmdUnlock.Visible &&
                                            (Utility.Coquyen("quyen_mokhoa_tatca") ||
                                             objLuotkham.NguoiKetthuc == globalVariables.UserName);
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
                cmdNhapVien.Enabled = objkcbdangky.TrangThai == 1;
                cmdHuyNhapVien.Enabled = objLuotkham.TrangthaiNoitru >= 1;
                cmdNhapVien.Tag = objLuotkham.TrangthaiNoitru == 0 ? "0" : "1";
                cmdNhapVien.Text = objLuotkham.TrangthaiNoitru == 0 ? "Nhập viện" : "Cập nhật";
                //Thread.Sleep(500);
                cmdSave.Enabled = true;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                
            }
            finally
            {
                cmdSave.Enabled = true;
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
            if (!Utility.isValidGrid(grdVTTH)) return;
            int Pres_ID = Utility.Int32Dbnull(grdVTTH.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            PrintPres(Pres_ID, "PHIẾU VẬT TƯ NGOÀI GÓI");
        }

        private void cmdSuaphieuVT_Click(object sender, EventArgs e)
        {
            if (!CheckPatientSelected()) return;
            if (!cmdSuaphieuVT.Enabled) return;
            SuaphieuVattu();
        }

        private void cmdThemphieuVT_Click(object sender, EventArgs e)
        {
            if (!CheckPatientSelected()) return;
            if (!cmdThemphieuVT.Enabled) return;
            if (Utility.Coquyen("quyen_suadonthuoc") || Utility.Int32Dbnull(objkcbdangky.IdBacsikham, -1) <= 0 ||
                objkcbdangky.IdBacsikham == globalVariables.gv_intIDNhanvien)
            {
            }
            else
            {
                Utility.ShowMsg(
                    string.Format(
                        "Bệnh nhân này đã được khám bởi Bác sĩ khác nên bạn không được phép thêm phiếu vật tư cho Bệnh nhân"));
                return;
            }

            ThemphieuVattu();
        }

        private void ThemphieuVattu()
        {
            try
            {
                // KeDonThuocTheoDoiTuong();
                var frm = new frm_KCB_KE_DONTHUOC("VT");
                frm.em_Action = action.Insert;
                frm.objLuotkham = objLuotkham;

                frm._KcbCDKL = _KcbChandoanKetluan;
                frm._MabenhChinh = txtMaBenhChinh.Text;
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
                frm.dtNgayKhamLai.MinDate = dtpCreatedDate.Value;
                frm._ngayhenkhamlai = dtpCreatedDate.Value.ToString("yyMMdd") == dtpNgayHen.Value.ToString("yyMMdd") ? "" : dtpNgayHen.Text;
                frm.noitru = 0;
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();

                if (!frm.m_blnCancel)
                {
                    txtMaBenhChinh.Text = frm._MabenhChinh;
                    txtChanDoan._Text = frm._Chandoan;
                    dt_ICD_PHU = frm.dt_ICD_PHU;
                    if (frm._KcbCDKL != null) _KcbChandoanKetluan = frm._KcbCDKL;
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
                        frm.em_Action = action.Update;
                        frm._KcbCDKL = _KcbChandoanKetluan;
                        frm._MabenhChinh = txtMaBenhChinh.Text;
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
                        frm.dtNgayKhamLai.MinDate = dtpCreatedDate.Value;
                        frm._ngayhenkhamlai = dtpCreatedDate.Value.ToString("yyMMdd") == dtpNgayHen.Value.ToString("yyMMdd") ? "" : dtpNgayHen.Text;

                        frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                        frm.ShowDialog();
                        if (!frm.m_blnCancel)
                        {
                            txtMaBenhChinh.Text = frm._MabenhChinh;
                            txtChanDoan._Text = frm._Chandoan;
                            dt_ICD_PHU = frm.dt_ICD_PHU;
                            if (frm._KcbCDKL != null) _KcbChandoanKetluan = frm._KcbCDKL;
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
            int Pres_ID = Utility.Int32Dbnull(grdVTTH.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
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
            foreach (GridEXRow gridExRow in grdVTTH.GetCheckedRows())
            {
                string stempt = "";
                int id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, 0m);
                decimal dongia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, 0m);
                int IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value, 0m);
                List<int> _temp = GetIdChitietVTTH(IdDonthuoc, id_thuoc, dongia, ref stempt);
                s += "," + stempt;
                lstIdchitiet.AddRange(_temp);
                gridExRow.Delete();
                grdVTTH.UpdateData();
            }
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
            DataRow[] arrDr =
                m_dtVTTH.Select(KcbDonthuocChitiet.Columns.IdDonthuoc + "=" + IdDonthuoc.ToString() + " AND " +
                                KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc.ToString()
                                + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + don_gia.ToString());
            if (arrDr.Length > 0)
            {
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
            if (grdVTTH.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện việc xóa thông tin Vật tư tiêu hao ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdVTTH.Focus();
                return false;
            }
            foreach (GridEXRow gridExRow in grdVTTH.GetCheckedRows())
            {
                if (Utility.Coquyen("quyen_suadonthuoc") ||
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
                cmdSave.Focus();
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
                cmdSave.Focus();
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

        private void frm_KCB_LSKCB_FormClosed(object sender, FormClosedEventArgs e)
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
                Decimal IBM = Utility.DecimaltoDbnull(txtCannang.Text, 0) / ((Utility.DecimaltoDbnull(txtChieucao.Text, 0) / 100) *
                                   (Utility.DecimaltoDbnull(txtChieucao.Text, 0) / 100));
                txtibm.Text = String.Format("{0:0.##}", IBM);
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
            //long idchitietchidinh = Utility.Int64Dbnull(
            //    grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value);
            //if (idchitietchidinh > 0)
            //{
            //    var frm = new FrmPhauThuatThuThuat(idchitietchidinh);
            //    frm.ShowDialog();
            //}
            
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
                pnlAttachedFiles.Controls.Clear();
                int idChidinh = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                DataTable dtAttachedFiles = new Select().From(TblFiledinhkem.Schema).Where(TblFiledinhkem.Columns.IdChidinh).IsEqualTo(idChidinh).ExecuteDataSet().Tables[0];
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
            Label obj = sender as Label;
            byte[] fileData = obj.Tag as byte[];
            //Save to temp folder
            string fileName = Application.StartupPath + @"\tempdpf\" + obj.Text;
            Utility.CreateFolder(fileName);
            File.WriteAllBytes(fileName, fileData);
            System.Diagnostics.Process.Start(fileName);
        }
        private void cmdFileAttach_Click(object sender, EventArgs e)
        {
            int idChidinh = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
            string MaChidinh = Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhcl.Columns.MaChidinh), "xxx");
            OpenFileDialog _openfile = new OpenFileDialog();
            _openfile.Multiselect=true;
            if (_openfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (_openfile.FileNames.Count() >= 5)
                {
                    if (!Utility.AcceptQuestion(string.Format("Bạn đang chọn nhiều ({0}) file kết quả cho phiếu chỉ định này. Bạn có chắc chắn?", _openfile.FileNames.Count().ToString()), "Cảnh báo", true))
                    {
                        return;
                    }
                }
                foreach (string sfile in _openfile.FileNames)
                {
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
            txtSongaydieutri.Text = txtSoNgayHen.Text = (dtpNgayHen.Value.Date - dtpCreatedDate.Value.Date).Days.ToString();
        }

        private void lnkClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtSoKham.Clear();
            txtTenBN.Clear();
            cboPhongKhamNgoaiTru.SelectedIndex = 0;
            dtFromDate.Value = dtToDate.Value = DateTime.Now;
            cmdSearch.Focus();

        }
    }
}