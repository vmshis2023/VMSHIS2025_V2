using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using NLog;
using SubSonic;
using VNS.HIS.UI.Classess;
using VNS.HIS.UI.Forms.THANHTOAN;
using VNS.Libs;
using VNS.HIS.DAL;
using AggregateFunction = Janus.Windows.GridEX.AggregateFunction;
using BorderStyle = Janus.Windows.GridEX.BorderStyle;
using TextAlignment = Janus.Windows.GridEX.TextAlignment;
using VNS.Properties;
using VNS.HIS.UI.NGOAITRU;
using VNS.HIS.Classes;
using VNS.HIS.BusRule.Classes;
using CrystalDecisions.CrystalReports.Engine;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.UI.Forms.Dungchung;
namespace  VNS.HIS.UI.THANHTOAN
{
    public partial class frm_THANHTOAN_NOITRU_old : Form
    {
        KCB_THANHTOAN _THANHTOAN = new KCB_THANHTOAN();
        private readonly string FileName = string.Format("{0}/{1}", Application.StartupPath, "SplitterDistance.txt");
        private int Distance = 400;
        private int HOADON_CAPPHAT_ID = -1;
        private bool INPHIEU_CLICK;
        private bool m_blnHasloaded;
        private bool blnJustPayment;
        private DataTable dtCapPhat;
        private DataTable dtPatientPayment;
        private DataTable m_dtChiPhiDaThanhToan = new DataTable();
        private DataTable m_dtChiPhiThanhtoan;
        string Args = "ALL";
        private Logger log;
        /// <summary>
        ///     05-11-2013
        /// </summary>
        #region "khai báo biến "
        private DataTable m_dtDataTimKiem = new DataTable();
        private DataTable m_dtPayment, m_dtPhieuChi = new DataTable();
        private DataTable m_dtReportPhieuThu;
        private KcbLuotkham objLuotkham;
        private string sFileName = "RedInvoicePrinterConfig.txt";
        private long v_Payment_ID = -1;
        #endregion
        public frm_THANHTOAN_NOITRU_old(string Args)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            KeyPreview = true;
            this.Args = Args;
            dtFromDate.Value =
                dtPaymentDate.Value = dtInput_Date.Value = dtToDate.Value = globalVariables.SysDate;
            //cmdHuyThanhToan.Visible = (globalVariables.IsAdmin || globalVariables.quyenh);
            Utility.grdExVisiableColName(grdPayment, "cmdHUY_PHIEUTHU", globalVariables.IsAdmin);
            Utility.grdExVisiableColName(grdPhieuChi, "cmdHuyPhieuChi", globalVariables.IsAdmin);
            if (grdPayment.RootTable.Columns.Contains(KcbThanhtoan.Columns.NgayThanhtoan))
                grdPayment.RootTable.Columns[KcbThanhtoan.Columns.NgayThanhtoan].Selectable =
                     Utility.Coquyen("quyen_suangay_thanhtoan") || globalVariables.IsAdmin;
            if (grdPhieuChi.RootTable.Columns.Contains(KcbThanhtoan.Columns.NgayThanhtoan))
                grdPhieuChi.RootTable.Columns[KcbThanhtoan.Columns.NgayThanhtoan].Selectable =
                     Utility.Coquyen("quyen_suangay_thanhtoan") || globalVariables.IsAdmin;
            cmdCauHinh.Visible = globalVariables.IsAdmin;
            LoadLaserPrinters();
            CauHinh();
            log = LogManager.GetLogger(Name);
            getColorMessage = lblMessage.BackColor;
            InitEvents();
        }
        void InitEvents()
        {
            Load += new EventHandler(frm_THANHTOAN_NOITRU_old_Load);
            FormClosing += new FormClosingEventHandler(frm_THANHTOAN_NOITRU_old_FormClosing);
            KeyDown += new KeyEventHandler(frm_THANHTOAN_NOITRU_old_KeyDown);

            txtMaLanKham.TextChanged += new EventHandler(txtMaLanKham_TextChanged);
            txtMaLanKham.KeyDown += new KeyEventHandler(txtMaLanKham_KeyDown);
            txtMaLanKham.LostFocus += txtMaLanKham_LostFocus;
            txtTenBenhNhan.KeyDown += new KeyEventHandler(txtTenBenhNhan_KeyDown);

            cmdSearch.Click += new EventHandler(cmdSearch_Click);

            cmdCapnhatngayinphoiBHYT.Click += cmdCapnhatngayinphoiBHYT_Click;
            grdHoaDonCapPhat.SelectionChanged += grdHoaDonCapPhat_SelectionChanged;
            cmdThanhToan.Click += cmdThanhToan_Click;
            cmdInphoiBHYT.Click += cmdInphoiBHYT_Click;
            cmdHuyThanhToan.Click += cmdHuyThanhToan_Click;
            cmdsave.Click += cmdsave_Click;
            cmdxoa.Click += cmdxoa_Click;
            //cmdPrint.Click += cmdPrintHoaDon_Click;
            txtSerie.Leave += txtSerie_Leave;
            txtSerie.KeyDown += txtSerie_KeyDown;
            grdPayment.CellUpdated += grdPayment_CellUpdated;
            grdPayment.ColumnButtonClick += grdPayment_ColumnButtonClick;
            grdPayment.SelectionChanged += grdPayment_SelectionChanged;
            grdPhieuChi.CellUpdated += grdPhieuChi_CellUpdated;
            grdPhieuChi.ColumnButtonClick += grdPhieuChi_ColumnButtonClick;
            cmdChuyenDT.Click += cmdChuyenDT_Click;
            
            cmdInphoiBHYT.Click += new EventHandler(cmdInphoiBHYT_Click_2);

            cmdInhoadon.Click += new EventHandler(cmdInhoadon_Click);
            
            chkCreateDate.CheckedChanged += new EventHandler(chkCreateDate_CheckedChanged);
            cmdHuyInPhoiBHYT.Click += new EventHandler(cmdHuyInPhoiBHYT_Click);
            mnuSuaSoBienLai.Click += new EventHandler(mnuSuaSoBienLai_Click);
            mnuInLaiBienLai.Click += new EventHandler(mnuInLaiBienLai_Click);
            mnuHuyHoaDon.Click += new EventHandler(mnuHuyHoaDon_Click);
            mnuLayhoadondo.Click += new EventHandler(mnuLayhoadondo_Click);
            grdDSKCB.RowCheckStateChanged += new Janus.Windows.GridEX.RowCheckStateChangeEventHandler(grdDSKCB_RowCheckStateChanged);
            grdDSKCB.ColumnHeaderClick += new Janus.Windows.GridEX.ColumnActionEventHandler(grdDSKCB_ColumnHeaderClick);
            grdList.DoubleClick += new EventHandler(grdList_DoubleClick);
            grdList.ColumnButtonClick += new Janus.Windows.GridEX.ColumnActionEventHandler(grdList_ColumnButtonClick);
            grdList.FormattingRow += new Janus.Windows.GridEX.RowLoadEventHandler(grdList_FormattingRow);
            grdList.KeyDown += new KeyEventHandler(grdList_KeyDown);

            grdThongTinChuaThanhToan.UpdatingCell += new Janus.Windows.GridEX.UpdatingCellEventHandler(grdThongTinChuaThanhToan_UpdatingCell);
            grdThongTinChuaThanhToan.CellValueChanged += new Janus.Windows.GridEX.ColumnActionEventHandler(grdThongTinChuaThanhToan_CellValueChanged);
            grdThongTinChuaThanhToan.CellUpdated += new Janus.Windows.GridEX.ColumnActionEventHandler(grdThongTinChuaThanhToan_CellUpdated);
            grdThongTinChuaThanhToan.ColumnHeaderClick += new Janus.Windows.GridEX.ColumnActionEventHandler(grdThongTinChuaThanhToan_ColumnHeaderClick);
            grdThongTinChuaThanhToan.RowCheckStateChanged += new RowCheckStateChangeEventHandler(grdThongTinChuaThanhToan_RowCheckStateChanged);
            grdThongTinChuaThanhToan.GroupsChanged += new Janus.Windows.GridEX.GroupsChangedEventHandler(grdThongTinChuaThanhToan_GroupsChanged);
            grdThongTinChuaThanhToan.EditingCell += new EditingCellEventHandler(grdThongTinChuaThanhToan_EditingCell);
            grdThongTinDaThanhToan.CellValueChanged += new Janus.Windows.GridEX.ColumnActionEventHandler(grdThongTinDaThanhToan_CellValueChanged);
            grdThongTinDaThanhToan.CellUpdated += new Janus.Windows.GridEX.ColumnActionEventHandler(grdThongTinDaThanhToan_CellUpdated);
            grdThongTinDaThanhToan.ColumnHeaderClick += new Janus.Windows.GridEX.ColumnActionEventHandler(grdThongTinDaThanhToan_ColumnHeaderClick);
            

            cmdLayThongTinDaThanhToan.Click += new EventHandler(cmdLayThongTinDaThanhToan_Click);
            cmdTraLaiTien.Click += new EventHandler(cmdTraLaiTien_Click);
            cmdInPhieuChi.Click += new EventHandler(cmdInPhieuChi_Click);
            cmdLuuLai.Click += new EventHandler(cmdLuuLai_Click);
            cmdCapnhatngayBHYT.Click += new EventHandler(cmdCapnhatngayBHYT_Click);
            cmdLaylaiThongTin.Click += new EventHandler(cmdLaylaiThongTin_Click);
            cmdPrintProperties.Click += new EventHandler(cmdPrintProperties_Click);
            txtYear_Of_Birth.TextChanged += new EventHandler(txtYear_Of_Birth_TextChanged);
            cmdCauHinh.Click += new EventHandler(this.cmdCauHinh_Click);
            dtFromDate.Enabled = dtToDate.Enabled = chkCreateDate.Checked;

            chkHoixacnhanhuythanhtoan.CheckedChanged += new EventHandler(chkHoixacnhanhuythanhtoan_CheckedChanged);
            chkHoixacnhanthanhtoan.CheckedChanged += new EventHandler(chkHoixacnhanthanhtoan_CheckedChanged);
            chkPreviewHoadon.CheckedChanged += new EventHandler(chkPreviewHoadon_CheckedChanged);
            chkPreviewInBienlai.CheckedChanged += new EventHandler(chkPreviewInBienlai_CheckedChanged);
            chkPreviewInphoiBHYT.CheckedChanged += new EventHandler(chkPreviewInphoiBHYT_CheckedChanged);
            chkTudonginhoadonsauthanhtoan.CheckedChanged += new EventHandler(chkTudonginhoadonsauthanhtoan_CheckedChanged);
            chkViewtruockhihuythanhtoan.CheckedChanged += new EventHandler(chkViewtruockhihuythanhtoan_CheckedChanged);
            cbomayinhoadon.SelectedIndexChanged += new EventHandler(cbomayinhoadon_SelectedIndexChanged);
            cbomayinphoiBHYT.SelectedIndexChanged += new EventHandler(cbomayinphoiBHYT_SelectedIndexChanged);
            chkHienthiDichvusaukhinhannutthanhtoan.CheckedChanged += new EventHandler(chkHienthichuathanhtoan_CheckedChanged);
            cmdSaveforNext.Click += new EventHandler(cmdSaveforNext_Click);
            

            cmdInBienlai.Click += new EventHandler(cmdInBienlai_Click);
            cmdInBienlaiTonghop.Click += new EventHandler(cmdInBienlaiTonghop_Click);
            cmdInphieuDCT.Click += new EventHandler(cmdInphieuDCT_Click);
            mnuUpdatePrice.Click += new EventHandler(mnuUpdatePrice_Click);
            tabThongTinCanThanhToan.SelectedTabChanged += new Janus.Windows.UI.Tab.TabEventHandler(tabThongTinCanThanhToan_SelectedTabChanged);
            mnuHuyChietkhau.Click += new EventHandler(mnuHuyChietkhau_Click);
            cmdKhaibaoHoadondo.Click += cmdKhaibaoHoadondo_Click;
            cmdSaveICD.Click += cmdSaveICD_Click;
            optAll.CheckedChanged += optAll_CheckedChanged;
            optNoitru.CheckedChanged += optAll_CheckedChanged;
            optNgoaitru.CheckedChanged += optAll_CheckedChanged;
            cmdCalculator.Click += cmdCalculator_Click;
            cmdHoanung.Click += cmdHoanung_Click;
            txtPttt._OnShowData += txtPttt__OnShowData;
        }

        void txtPttt__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtPttt.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtPttt.myCode;
                txtPttt.Init();
                txtPttt.SetCode(oldCode);
                txtPttt.Focus();
            }
        }

        void cmdHoanung_Click(object sender, EventArgs e)
        {
            objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text), Utility.DoTrim(txtPatient_Code.Text));
            if (cmdHoanung.Tag.ToString() == "0")//Hoàn ứng
            {
                if (objLuotkham.TrangthaiNoitru <=3)
                {
                    Utility.ShowMsg("Bệnh nhân chưa được xác nhận chuyển thanh toán nội trú nên bạn không được phép hoàn ứng");
                    return;
                }
               
                SPs.NoitruHoanung(-1l,THU_VIEN_CHUNG.SinhmaVienphi("HKQ"),objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, dtPaymentDate.Value,
                    globalVariables.gv_intIDNhanvien, globalVariables.UserName,
                    Utility.Int32Dbnull(objLuotkham.IdKhoanoitru, -1), Utility.Int64Dbnull(objLuotkham.IdRavien, -1),
                    Utility.Int32Dbnull(objLuotkham.IdBuong, -1), Utility.Int32Dbnull(objLuotkham.IdGiuong, -1),
                    (byte)1).Execute();//Hủy tay thì truyền giá trị thanh toán =-1 cho id thanh toán vào
                ;
                cmdHoanung.Tag = "1";
                cmdHoanung.Text = @"Hủy hoàn ứng";
            }
            else
            {
                if (objLuotkham.TrangthaiNoitru == 6)
                {
                    Utility.ShowMsg("Bệnh nhân đã thanh toán nội trú nên bạn không được phép hủy hoàn ứng");
                    return;
                }
                SPs.NoitruHuyhoanung(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, -1l, (byte)1).Execute();//Hủy tay thì truyền giá trị thanh toán =-1 cho id thanh toán vào
                cmdHoanung.Tag = "0";
                cmdHoanung.Text = @"Hoàn ứng";
            }
        }

        void cmdCalculator_Click(object sender, EventArgs e)
        {
            Utility.OpenProcess("Calc");
        }

        void optAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string RowFilter="1=1";
                PropertyLib._ThanhtoanProperties.CachhienthidulieuNoitru = DisplayType.Tatca;
                if (optNoitru.Checked)
                {
                    PropertyLib._ThanhtoanProperties.CachhienthidulieuNoitru = DisplayType.Noitru;
                    RowFilter = "noi_tru=1";
                }
                if (optNgoaitru.Checked)
                {
                    PropertyLib._ThanhtoanProperties.CachhienthidulieuNoitru = DisplayType.Ngoaitru;
                    RowFilter = "noi_tru=0";
                }
                m_dtChiPhiThanhtoan.DefaultView.RowFilter = RowFilter;
                m_dtChiPhiThanhtoan.AcceptChanges();
                PropertyLib.SaveProperty(PropertyLib._ThanhtoanProperties);
            }
            catch (Exception ex)
            {
                
                
            }
        }

        void cmdSaveICD_Click(object sender, EventArgs e)
        {
            if (!isValidICD()) return;
            _THANHTOAN.UpdateIcd10(objLuotkham, Utility.DoTrim(txtICD.MyCode), Utility.DoTrim(txtICD.MyText));
        }
        bool isValidICD()
        {
            if (Utility.DoTrim(txtICD.MyCode) == "-1")
            {
                Utility.ShowMsg("Bạn cần nhập mã bệnh chính theo chuẩn ICD 10 trước khi lưu");
                txtICD.Focus();
            }
            if (globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + "='" + Utility.DoTrim(txtICD.MyCode) + "'").Length <= 0)
            {
                Utility.ShowMsg("Mã bệnh chính bạn nhập không tồn tại trong hệ thống của chúng tôi\nBạn có thể kiểm tra danh mục ICD 10 và thêm mã này vào.\nMời bạn nhấn OK để chọn mã bệnh từ danh mục");


            }
            return true;
        }
        void cmdKhaibaoHoadondo_Click(object sender, EventArgs e)
        {
            frm_List_RedInvoice _RedInvoice = new frm_List_RedInvoice();
            _RedInvoice.ShowDialog();
            LoadInvoiceInfo();
        }

        void cmdPrintProperties_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._MayInProperties);
            frm.ShowDialog();
            CauHinh();
        }

        void chkPreviewInBienlai_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._MayInProperties.PreviewInBienlai = chkPreviewInBienlai.Checked;
        }

        void grdThongTinChuaThanhToan_EditingCell(object sender, EditingCellEventArgs e)
        {
            if (grdThongTinChuaThanhToan.CurrentColumn != null) grdThongTinChuaThanhToan.CurrentColumn.InputMask = "";
        }

        void mnuLayhoadondo_Click(object sender, EventArgs e)
        {
            try
            {
                if (mnuLayhoadondo.Tag.ToString() == "1")
                {
                    long IdHdonLog = -1;
                    long IdHdonLog_huy = -1;
                    if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn cập nhật lấy hóa đơn đỏ với số serie {0} cho thanh toán đang chọn hay không?", Utility.DoTrim(txtSerie.Text)), "Cảnh báo", true)) return;
                    if (!checkSerie(ref IdHdonLog_huy)) return;

                    if (_THANHTOAN.LayHoadondo(Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1), Utility.DoTrim(txtMauHD.Text)
                        , Utility.DoTrim(txtKiHieu.Text), Utility.DoTrim(txtMaQuyen.Text), Utility.DoTrim(txtSerie.Text), Utility.Int32Dbnull(grdHoaDonCapPhat.GetValue(HoadonCapphat.Columns.IdCapphat), -1), IdHdonLog_huy, ref IdHdonLog) == ActionResult.Success)
                    {
                        grdPayment.CurrentRow.BeginEdit();
                        grdPayment.CurrentRow.Cells[HoadonLog.Columns.MauHoadon].Value = Utility.DoTrim(txtMauHD.Text);
                        grdPayment.CurrentRow.Cells[HoadonLog.Columns.KiHieu].Value = Utility.DoTrim(txtKiHieu.Text);
                        grdPayment.CurrentRow.Cells[HoadonLog.Columns.MaQuyen].Value = Utility.DoTrim(txtMaQuyen.Text);
                        grdPayment.CurrentRow.Cells[HoadonLog.Columns.Serie].Value = Utility.DoTrim(txtSerie.Text);
                        grdPayment.CurrentRow.Cells[HoadonLog.Columns.IdHdonLog].Value = IdHdonLog;
                        grdPayment.CurrentRow.Cells[HoadonLog.Columns.IdCapphat].Value = Utility.Int32Dbnull(grdHoaDonCapPhat.GetValue(HoadonCapphat.Columns.IdCapphat), -1);
                        grdPayment.CurrentRow.EndEdit();

                        grdHoaDonCapPhat.CurrentRow.BeginEdit();
                        grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.SerieHientai].Value = Utility.sDbnull(txtSerie.Text);
                        grdHoaDonCapPhat.CurrentRow.EndEdit();
                        txtSerie.Text = Utility.sDbnull(Utility.Int32Dbnull(txtSerie.Text) + 1);
                        txtSerie.Text = txtSerie.Text.PadLeft(Utility.sDbnull(txtSerieDau.Text).Length, '0');
                        Utility.ShowMsg("Đã thực hiện cập nhật lấy hóa đơn đỏ cho thanh toán đang chọn thành công");
                    }
                    else
                    {
                        Utility.ShowMsg("Lỗi khi cập nhật lấy hóa đơn đỏ cho thanh toán đang chọn. Liên hệ nhà cung cấp phần mềm để được trợ giúp");
                    }
                }
                else
                {
                    if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn hủy lấy hóa đơn đỏ cho thanh toán đang chọn hay không?", Utility.DoTrim(txtSerie.Text)), "Cảnh báo", true)) return;
                    if (_THANHTOAN.BoHoadondo(Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdHdonLog), -1)) == ActionResult.Success)
                    {
                        grdPayment.CurrentRow.BeginEdit();
                        grdPayment.CurrentRow.Cells[HoadonLog.Columns.MauHoadon].Value = "";
                        grdPayment.CurrentRow.Cells[HoadonLog.Columns.KiHieu].Value = "";
                        grdPayment.CurrentRow.Cells[HoadonLog.Columns.MaQuyen].Value = "";
                        grdPayment.CurrentRow.Cells[HoadonLog.Columns.Serie].Value = "";
                        grdPayment.CurrentRow.Cells[HoadonLog.Columns.IdHdonLog].Value = -1;
                        grdPayment.CurrentRow.Cells[HoadonLog.Columns.IdCapphat].Value = -1;
                        grdPayment.CurrentRow.EndEdit();
                        Utility.ShowMsg("Đã thực hiện hủy hóa đơn đỏ cho thanh toán đang chọn thành công");
                    }
                    else
                    {
                        Utility.ShowMsg("Lỗi khi hủy hóa đơn đỏ cho thanh toán đang chọn. Liên hệ nhà cung cấp phần mềm để được trợ giúp");
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyContextMenu();
            }
        }
        bool checkSerie(ref long  IdHdonLog)
        {
            try
            {

                if (string.IsNullOrEmpty(txtMauHD.Text))
                {
                    Utility.ShowMsg("Mẫu số biên lai không được để trống");
                    txtMauHD.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtMaQuyen.Text))
                {
                    Utility.ShowMsg("Mã quyển không được để trống");
                    txtMaQuyen.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtKiHieu.Text))
                {
                    Utility.ShowMsg("Ký hiệu biên lai không được để trống");
                    txtKiHieu.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtSerie.Text))
                {
                    Utility.ShowMsg("Số biên lai không được để trống");
                    txtSerie.Focus();
                    return false;
                }

                QueryCommand cmd = HoadonLog.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandSql = "SELECT * FROM hoadon_capphat lhm " +
                                 "WHERE lhm.mau_hoadon = '" + Utility.DoTrim(txtMauHD.Text) + "' AND lhm.MA_QUYEN = '" + Utility.DoTrim(txtMaQuyen.Text) + "' AND lhm.KI_HIEU ='" + Utility.DoTrim(txtKiHieu.Text) + "' " +
                                 "AND (CONVERT(INT,lhm.SERIE_DAU) <= CONVERT(INT,'" + Utility.DoTrim(txtSerie.Text) + "') " +
                                 "AND CONVERT(INT, lhm.SERIE_CUOI) >= CONVERT(INT,'" + Utility.DoTrim(txtSerie.Text) + "'))";
                DataTable temp = DataService.GetDataSet(cmd).Tables[0];
                if (temp.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tồn tại số serie trong dải serie của lần cấp phát đang chọn. Mời bạn kiểm tra lại");
                    return false;
                }
                HoadonLog _HoadonLog = new Select().From(HoadonLog.Schema)
                 .Where(HoadonLog.Columns.MauHoadon).IsEqualTo(Utility.DoTrim(txtMauHD.Text))
                 .And(HoadonLog.Columns.KiHieu).IsEqualTo(Utility.DoTrim(txtKiHieu.Text))
                 .And(HoadonLog.Columns.MaQuyen).IsEqualTo(Utility.DoTrim(txtMaQuyen.Text))
                 .And(HoadonLog.Columns.Serie).IsEqualTo(Utility.DoTrim(txtSerie.Text))
                 .ExecuteSingle<HoadonLog>();
                if (_HoadonLog != null)
                {
                    if (Utility.Int32Dbnull(_HoadonLog.TrangThai) > 0)//Seri bị hủy. Có thể dùng cho hóa đơn của bệnh nhân khác
                    {
                        if (Utility.AcceptQuestion(string.Format("Số seri {0} đã được hủy. Bạn có chắc chắn muốn sử dụng lại serie này cho thanh toán đang chọn ?", txtSerie.Text), "Xác nhận", true))
                        {
                            IdHdonLog = _HoadonLog.IdHdonLog;
                        }
                    }
                    else//Trạng thái seri=0-->Vừa mới in
                    {
                        Utility.ShowMsg(string.Format("Seri đã được in cho bệnh nhân mã {0}. Mời bạn kiểm tra và chọn seri khác. Chú ý: Nếu bạn vẫn muốn in serie này thì cần tìm thanh toán của bệnh nhân ID: {1}- Mã: {2} để hủy serie đó", _HoadonLog.MaLuotkham, objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham));
                        txtSerie.Focus();
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        void grdThongTinChuaThanhToan_RowCheckStateChanged(object sender, RowCheckStateChangeEventArgs e)
        {
            //Thay hàm TinhToanSoTienPhaithu= hàm SetSumTotalProperties để tính lại tiền BHYT chi trả
            SetSumTotalProperties();
            //TinhToanSoTienPhaithu();
            ModifyCommand();
        }

        void mnuHuyChietkhau_Click(object sender, EventArgs e)
        {
            try
            {
                
                foreach (GridEXRow _row in grdThongTinChuaThanhToan.GetDataRows())
                {
                    if (Utility.Int64Dbnull(_row.Cells["trangthai_thanhtoan"].Value, 1) == 0)//Chỉ reset các mục chưa thanh toán
                    {
                        _row.BeginEdit();
                        _row.Cells["tile_chietkhau"].Value = 0;
                        _row.Cells["tien_chietkhau"].Value = 0;
                        _row.EndEdit();
                    }
                }
            }
            catch
            {
            }
            finally
            {
                //Thay hàm TinhToanSoTienPhaithu= hàm SetSumTotalProperties để tính lại tiền BHYT chi trả
                SetSumTotalProperties();
                //TinhToanSoTienPhaithu();
            }
        }

        void mnuUpdatePrice_Click(object sender, EventArgs e)
        {
            if (objLuotkham != null)
            {
                objLuotkham=new Select().From(KcbLuotkham.Schema)
                    .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .ExecuteSingle<KcbLuotkham>();
                if (THU_VIEN_CHUNG.UpdatePtramBhyt(objLuotkham, -1) == ActionResult.Success)
                {
                    getData();
                }
            }
        }

        void grdList_KeyDown(object sender, KeyEventArgs e)
        {
            if (Utility.isValidGrid(grdList) && e.KeyCode == Keys.Enter)
            {
                getData();
            }
        }

        void tabThongTinCanThanhToan_SelectedTabChanged(object sender, Janus.Windows.UI.Tab.TabEventArgs e)
        {
            tabThongTinThanhToan.Height = tabThongTinCanThanhToan.SelectedTab == TabpageCauhinh ? 0 : 168;
        }

        void cmdInphieuDCT_Click(object sender, EventArgs e)
        {
            InPhieuDCT();
        }

        void cmdInBienlaiTonghop_Click(object sender, EventArgs e)
        {
            int _Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
            new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(true, _Payment_ID, objLuotkham, 1);
        }

        void cmdInBienlai_Click(object sender, EventArgs e)
        {
            int _Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
            new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, _Payment_ID, objLuotkham, 1);
            cbomayinphoiBHYT.Text = PropertyLib._MayInProperties.TenMayInBienlai;
        }

        

        void chkHienthichuathanhtoan_CheckedChanged(object sender, EventArgs e)
        {
            if (m_blnHasloaded && m_dtChiPhiThanhtoan != null && m_dtChiPhiThanhtoan.Columns.Count > 0 && m_dtChiPhiThanhtoan.Rows.Count > 0)
                m_dtChiPhiThanhtoan.DefaultView.RowFilter = "trangthai_huy=0" + (PropertyLib._ThanhtoanProperties.Hienthidichvuchuathanhtoan ? " and trangthai_thanhtoan=0" : "");
            
            PropertyLib._ThanhtoanProperties.HienthidichvuNgaysaukhithanhtoan = chkHienthiDichvusaukhinhannutthanhtoan.Checked;
           
        }

        void cmdSaveforNext_Click(object sender, EventArgs e)
        {
            PropertyLib.SaveProperty(PropertyLib._ThanhtoanProperties);
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void cbomayinphoiBHYT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._MayInProperties.TenMayInBienlai = cbomayinphoiBHYT.Text;
        }

        void cbomayinhoadon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._MayInProperties.TenMayInHoadon = cbomayinhoadon.Text;
        }

        void chkViewtruockhihuythanhtoan_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._ThanhtoanProperties.Hienthihuythanhtoan = chkViewtruockhihuythanhtoan.Checked;
        }

        void chkTudonginhoadonsauthanhtoan_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._MayInProperties.TudonginhoadonSaukhiThanhtoan = chkTudonginhoadonsauthanhtoan.Checked;
        }

        void chkPreviewInphoiBHYT_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._MayInProperties.PreviewInPhoiBHYT = chkPreviewInphoiBHYT.Checked;
        }

        void chkPreviewHoadon_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._MayInProperties.PreviewInHoadon = chkPreviewHoadon.Checked;
        }

        void chkHoixacnhanthanhtoan_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._ThanhtoanProperties.Hoitruockhithanhtoan = chkHoixacnhanthanhtoan.Checked;
        }

        void chkHoixacnhanhuythanhtoan_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._ThanhtoanProperties.Hoitruockhihuythanhtoan = chkHoixacnhanhuythanhtoan.Checked;
        }
        private int SplitterDistance
        {
            get { return Distance; }
            set { Distance = value; }
        }

        /// <summary>
        ///     hàm thực hiện việc lấy thông tin
        /// </summary>
        private GridEXRow gridExRow { set; get; }

        private Color getColorMessage { get; set; }
        private string Maluotkham { get; set; }


        private void CauHinh()
        {
            try
            {
                dtPaymentDate.ReadOnly = !Utility.Coquyen("quyen_suangay_thanhtoan");
                chkHoixacnhanhuythanhtoan.Checked = PropertyLib._ThanhtoanProperties.Hoitruockhihuythanhtoan;
                chkHienthiDichvusaukhinhannutthanhtoan.Checked = PropertyLib._ThanhtoanProperties.HienthidichvuNgaysaukhithanhtoan;
                chkHoixacnhanthanhtoan.Checked = PropertyLib._ThanhtoanProperties.Hoitruockhithanhtoan;
                chkPreviewHoadon.Checked = PropertyLib._MayInProperties.PreviewInHoadon;
                chkPreviewInBienlai.Checked = PropertyLib._MayInProperties.PreviewInBienlai;
                chkPreviewInphoiBHYT.Checked = PropertyLib._MayInProperties.PreviewInPhoiBHYT;
                chkTudonginhoadonsauthanhtoan.Checked = PropertyLib._MayInProperties.TudonginhoadonSaukhiThanhtoan;
                chkViewtruockhihuythanhtoan.Checked = PropertyLib._ThanhtoanProperties.Hienthihuythanhtoan;
                cbomayinhoadon.Text = PropertyLib._MayInProperties.TenMayInHoadon;
                cbomayinphoiBHYT.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                tabPageThongTinDaThanhToan.TabVisible = !PropertyLib._ThanhtoanProperties.AnTabDaThanhtoan;
                chkLayHoadon.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false) == "1";
                pnlSeri.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false) == "1";
                tabpageHoaDon.TabVisible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false) == "1";
                cmdHoanung.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_TUDONGHOANUNG_KHITHANHTOANNOITRU", "0", false) == "0";
                grdList.Height = PropertyLib._ThanhtoanProperties.ChieucaohienthiLuoidanhsachBNthanhtoan <= 0 ? 0 : PropertyLib._ThanhtoanProperties.ChieucaohienthiLuoidanhsachBNthanhtoan;
                grdPayment.RootTable.Columns[KcbThanhtoan.Columns.Serie].Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false) == "1";
                // if (!hasLoadedRedInvoice && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false)=="1") LoadInvoiceInfo();
                uiStatusBar1.Visible = !PropertyLib._ThanhtoanProperties.HideStatusBar;
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false) == "1") LoadInvoiceInfo();
                bool redInvoice = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false) == "1";
                serperator1.Visible = serperator2.Visible = mnuHuyHoaDon.Visible = mnuInLaiBienLai.Visible = mnuLayhoadondo.Visible = mnuSuaSoBienLai.Visible = redInvoice;
                string hienthiphantichgiaTrenluoi = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_HIENTHIPHANTICHGIA_TRENLUOI", "0", false);
                grdThongTinChuaThanhToan.RootTable.Columns["TT_KHONG_PHUTHU"].Visible = hienthiphantichgiaTrenluoi == "1";
                grdThongTinChuaThanhToan.RootTable.Columns["TT_BN_KHONG_PHUTHU"].Visible = hienthiphantichgiaTrenluoi == "1";
                grdThongTinChuaThanhToan.RootTable.Columns["TT_BHYT"].Visible = hienthiphantichgiaTrenluoi == "1";
                grdThongTinChuaThanhToan.RootTable.Columns["TT_PHUTHU"].Visible = hienthiphantichgiaTrenluoi == "1";
                grdThongTinChuaThanhToan.RootTable.Columns["TT_BN"].Visible = hienthiphantichgiaTrenluoi == "1";
                grdThongTinChuaThanhToan.RootTable.Columns["bnhan_chitra"].Visible = hienthiphantichgiaTrenluoi == "1";
                grdThongTinChuaThanhToan.RootTable.Columns["phu_thu"].Visible = hienthiphantichgiaTrenluoi == "1";
                grdThongTinChuaThanhToan.RootTable.Columns["bhyt_chitra"].Visible = hienthiphantichgiaTrenluoi == "1";
                grdThongTinChuaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TileChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
                grdThongTinChuaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TienChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
                grdThongTinChuaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.KieuChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
                grdThongTinChuaThanhToan.RootTable.Columns["CHON"].Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_CHOPHEP_CHONCHITIET_THANHTOAN", "0", false) == "1";
                grdThongTinChuaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.BnhanChitra].Visible = PropertyLib._ThanhtoanProperties.HienthiChiTra;
                grdThongTinChuaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.BhytChitra].Visible = PropertyLib._ThanhtoanProperties.HienthiChiTra;
                grdThongTinChuaThanhToan.RootTable.Columns["TT_BN"].Visible = PropertyLib._ThanhtoanProperties.HienthiChiTra;
                grdThongTinChuaThanhToan.RootTable.Columns["TT_BHYT"].Visible = PropertyLib._ThanhtoanProperties.HienthiChiTra;
                grdThongTinChuaThanhToan.RootTable.Columns["TT_BN_KHONG_PHUTHU"].Visible = PropertyLib._ThanhtoanProperties.HienthiChiTra;
                grdThongTinChuaThanhToan.RootTable.Columns["TT_KHONG_PHUTHU"].Visible = PropertyLib._ThanhtoanProperties.HienthiChiTra;
                grdThongTinChuaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.PhuThu].Visible = PropertyLib._ThanhtoanProperties.Hienthiphuthu;
                grdThongTinChuaThanhToan.RootTable.Columns["TT_PHUTHU"].Visible = PropertyLib._ThanhtoanProperties.HienthiChiTra;
                grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TileChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
                grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TienChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
                grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.KieuChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
                grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.BnhanChitra].Visible = PropertyLib._ThanhtoanProperties.HienthiChiTra;
                grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.BhytChitra].Visible = PropertyLib._ThanhtoanProperties.HienthiChiTra;
                grdThongTinDaThanhToan.RootTable.Columns["TT_BN"].Visible = PropertyLib._ThanhtoanProperties.HienthiChiTra;
                grdThongTinDaThanhToan.RootTable.Columns["TT_BHYT"].Visible = PropertyLib._ThanhtoanProperties.HienthiChiTra;
                grdThongTinDaThanhToan.RootTable.Columns["TT_BN_KHONG_PHUTHU"].Visible = PropertyLib._ThanhtoanProperties.HienthiChiTra;
                grdThongTinDaThanhToan.RootTable.Columns["TT_KHONG_PHUTHU"].Visible = PropertyLib._ThanhtoanProperties.HienthiChiTra;
                grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.PhuThu].Visible = PropertyLib._ThanhtoanProperties.Hienthiphuthu;
                grdThongTinDaThanhToan.RootTable.Columns["TT_PHUTHU"].Visible = PropertyLib._ThanhtoanProperties.HienthiChiTra;
                switch (PropertyLib._ThanhtoanProperties.CachChietkhau)
                {
                    case 0:
                        grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TileChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
                        grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TienChietkhau].EditType = EditType.NoEdit;
                        break;
                    case 1:
                        grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TileChietkhau].Visible = false;
                        grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TienChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
                        grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TienChietkhau].EditType = EditType.TextBox;
                        break;
                    case 2:
                        grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TileChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
                        grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TileChietkhau].EditType = EditType.TextBox;
                        grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TienChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
                        grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TienChietkhau].EditType = EditType.TextBox;
                        break;
                }
            }
            catch (Exception exception)
            {
                Utility.CatchException(exception);
            }
        }

        private void LoadLaserPrinters()
        {
            try
            {
                //khoi tao may in
                cbomayinphoiBHYT.Items.Clear();
                //cboPrinter.Items.Clear();
                cbomayinhoadon.Items.Clear();
                for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                {
                    String pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                    cbomayinphoiBHYT.Items.Add(pkInstalledPrinters);
                    //cboPrinter.Items.Add(pkInstalledPrinters);
                    cbomayinhoadon.Items.Add(pkInstalledPrinters);
                }
            }
            catch(Exception ex)
            {
                Utility.ShowMsg("Lỗi kho nạp danh sách máy in \n" + ex.Message);
            }
            finally
            {
                if (cbomayinphoiBHYT.Items.Count <= 0)
                    Utility.ShowMsg("Không tìm thấy máy in cài đặt trong máy tính của bạn", "Cảnh báo");
            }
        }

        private void SetProperties()
        {
            try
            {
                foreach (Control control in pnlThongtintien.Controls)
                {
                    if (control is EditBox)
                    {
                        var txtFormantTongTien = new EditBox();
                        txtFormantTongTien = ((EditBox) (control));
                        txtFormantTongTien.Clear();
                        txtFormantTongTien.ReadOnly = true;
                        //if (txtFormantTongTien.Font.Size < 9)
                        //    txtFormantTongTien.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold,
                        //        GraphicsUnit.Point, 0);
                        txtFormantTongTien.TextAlignment = TextAlignment.Far;
                        txtFormantTongTien.KeyPress += txtEventTongTien_KeyPress;
                        txtFormantTongTien.TextChanged += txtEventTongTien_TextChanged;
                    }
                }
                foreach (Control control in pnlThongtinBN.Controls)
                {
                    if (control is EditBox)
                    {
                        var txtControl = new EditBox();
                        if (txtControl.Tag != "NO")//Đánh dấu một số Control cho phép chỉnh sửa. Ví dụ Hạn thẻ BHYT 
                            //để người dùng có thể sửa nếu phía Tiếp đón gõ sai
                        {
                            txtControl = ((EditBox) (control));
                            txtControl.ReadOnly = true;
                            txtControl.BackColor = Color.White;
                        }
                        txtControl.ForeColor = Color.Black;
                    }

                    if (control is UICheckBox)
                    {
                        var chkControl = new UICheckBox();
                        if (chkControl.Tag != "NO")
                        {
                            chkControl = (UICheckBox) control;
                            chkControl.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                log.Trace("Loi: "+ exception.Message);
            }
        }

        private void ModifyCommand()
        {
            try
            {
                cmdHoanung.Enabled = ucTamung1.grdTamung.GetDataRows().Length > 0 && objLuotkham != null;
                cmdChuyenDT.Enabled = Utility.isValidGrid(grdList) && objLuotkham != null;

                cmdHuyThanhToan.Enabled =Utility.isValidGrid(grdList) && Utility.isValidGrid(grdPayment)  && objLuotkham != null;
                cmdThanhToan.Enabled = Utility.isValidGrid(grdList) && grdThongTinChuaThanhToan.GetCheckedRows().Length > 0 && objLuotkham != null && Utility.Byte2Bool(objLuotkham.TthaiThopNoitru) && objLuotkham.TrangthaiNoitru==4;

                cmdTraLaiTien.Enabled = Utility.isValidGrid(grdList) && grdThongTinDaThanhToan.GetCheckedRows().Length > 0 && objLuotkham != null;
                cmdInPhieuChi.Enabled = Utility.isValidGrid(grdList) && grdPhieuChi.GetDataRows().Length > 0 && objLuotkham != null;
                cmdInhoadon.Enabled = Utility.isValidGrid(grdList) && Utility.isValidGrid(grdPayment) && objLuotkham != null;
                cmdInBienlai.Visible = Utility.isValidGrid(grdList) && Utility.isValidGrid(grdPayment) && objLuotkham != null;
                pnlBHYT.Visible = Utility.isValidGrid(grdList) && objLuotkham.MaDoituongKcb == "BHYT" && objLuotkham != null;
                cmdInphoiBHYT.Visible = Utility.isValidGrid(grdList) && Chuathanhtoan <= 0 &&
                                        Utility.DecimaltoDbnull(txtSoTienCanNop.Text) <= 0 &&
                                        objLuotkham.MaDoituongKcb == "BHYT" && grdPayment.GetDataRows().Length > 0 &&
                                        objLuotkham != null;
                cmdInphieuDCT.Visible = Utility.isValidGrid(grdList) && objLuotkham.MaDoituongKcb == "BHYT" && grdPayment.GetDataRows().Length > 0 && objLuotkham != null;
                cmdInBienlaiTonghop.Visible = Utility.isValidGrid(grdList) && Utility.isValidGrid(grdPayment) && grdPayment.GetDataRows().Length > 1 && objLuotkham != null;              
            }
            catch (Exception exception)
            {
                log.Trace("Loi: " + exception);
            }
        }

        private void txtEventTongTien_KeyPress(Object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        private void txtEventTongTien_TextChanged(Object sender, EventArgs e)
        {
            var txtTongTien = ((EditBox) (sender));
            Utility.FormatCurrencyHIS(txtTongTien);
        }

       

       

        private void chkCreateDate_CheckedChanged(object sender, EventArgs e)
        {
            dtFromDate.Enabled = dtToDate.Enabled = chkCreateDate.Checked;
        }

        /// <summary>
        ///     hàm thực hiện việc tìm kiếm thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            TimKiemBenhNhan();
        }
        private void FilterThanhToan()
        {
            try
            {
                string _rowFilter = "1=1";
                //if (radChuaTT.Checked) _rowFilter = string.Format("{0}={1}", "TT_ThanhToan", 0);
                //if (radDaTT.Checked) _rowFilter = string.Format("{0}={1}", "TT_ThanhToan", 1);
                m_dtDataTimKiem.DefaultView.RowFilter = _rowFilter;
                m_dtDataTimKiem.AcceptChanges();
            }
            catch (Exception)
            {
                Utility.ShowMsg("Lỗi trong quá trình Defaultview");
            }

        }
        private void TimKiemBenhNhan()
        {
            try
            {
                string KieuTimKiem = "DANGKY";
                //if (radDangKyCLS.Checked) KieuTimKiem = "CLS";
                //if (radDangKyThuoc.Checked) KieuTimKiem = "THUOC";
                m_dtDataTimKiem =
                    _THANHTOAN.LayDsachBenhnhanThanhtoan(-1,
                        Utility.sDbnull(txtMaLanKham.Text),
                        Utility.sDbnull(txtTenBenhNhan.Text),
                        chkCreateDate.Checked
                            ? dtFromDate.Value
                            : Convert.ToDateTime("01/01/1900"),
                        chkCreateDate.Checked
                            ? dtToDate.Value
                            : globalVariables.SysDate,
                        Utility.sDbnull(cboObjectType_ID.SelectedValue), -1,1,
                        KieuTimKiem, globalVariables.MA_KHOA_THIEN, this.Args);
                Utility.AddColumToDataTable(ref m_dtDataTimKiem, "CHON", typeof(Int32));
                Utility.SetDataSourceForDataGridEx(grdList, m_dtDataTimKiem, true, true, "1=1", "");
                ClearControl();
                //if (m_dtDataTimKiem.Rows.Count <= 0)
                //{
                    objLuotkham = null;
                    
                    mnuUpdatePrice.Enabled = objLuotkham != null;
                //}
                grdList.MoveFirst();
                UpdateGroup();
                Utility.GonewRowJanus(grdList, KcbLuotkham.Columns.MaLuotkham, txtPatient_Code.Text);
                ModifyCommand();
            }
            catch
            {
            }
        }

        /// <summary>
        ///     hàm thực hiện việc lấy thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdList_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "CHONBN")
            {
                getData();
               
            }
        }
        void UpdateGroup()
        {
            try
            {
                var counts = m_dtDataTimKiem.AsEnumerable().GroupBy(x => x.Field<string>("ma_doituong_kcb"))
                    .Select(g => new { g.Key, Count = g.Count() });
                if (counts.Count() >= 2)
                {
                    if (grdList.RootTable.Groups.Count <= 0)
                    {
                        GridEXColumn gridExColumn = grdList.RootTable.Columns["ma_doituong_kcb"];
                        var gridExGroup = new GridEXGroup(gridExColumn);
                        gridExGroup.GroupPrefix = "Nhóm đối tượng KCB: ";
                        grdList.RootTable.Groups.Add(gridExGroup);
                    }
                }
                else
                {
                    GridEXColumn gridExColumn = grdList.RootTable.Columns["ma_doituong_kcb"];
                    var gridExGroup = new GridEXGroup(gridExColumn);
                    grdList.RootTable.Groups.Clear();
                }
                grdList.UpdateData();
                grdList.Refresh();
            }
            catch
            {
            }
        }
        private void ClearControl()
        {
            try
            {
                foreach (Control control in pnlThongtintien.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox) (control)).Clear();
                    }
                }
                foreach (Control control in pnlThongtinBN.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox)(control)).Clear();
                    }
                }
                dtPaymentDate.Value = dtNgayInPhoi.Value = globalVariables.SysDate;
            }
            catch (Exception)
            {
            }
        }

        private void getData()
        {
            try
            {
               

                ClearControl();
                txtPatient_Code.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                txtPatient_ID.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text),Utility.DoTrim(txtPatient_Code.Text));
                mnuUpdatePrice.Enabled = objLuotkham != null;
                DataTable  mDtThongTin = _THANHTOAN.LaythongtinBenhnhan(txtPatient_Code.Text,
                    Utility.Int32Dbnull(txtPatient_ID.Text, -1));
                gridExRow = grdList.GetRow();
                if (!Utility.isValidGrid(grdList))
                {
                    return;
                }
                if (mDtThongTin.Rows.Count > 0)
                {
                    DataRow dr = mDtThongTin.Rows[0];
                    //if (dr != null)
                    //{
                      
                        dtInput_Date.Value = Convert.ToDateTime(dr[KcbLuotkham.Columns.NgayTiepdon]);
                        txtPatient_ID.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.IdBenhnhan], "");
                        txtPatient_Code.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.MaLuotkham], "");
                        txtYear_Of_Birth.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.NamSinh], globalVariables.SysDate.Year);
                        txtPatientName.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan], "") + ", " + Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.GioiTinh], "") + ", " + Utility.sDbnull(globalVariables.SysDate.Year - Utility.Int32Dbnull(txtYear_Of_Birth.Text)) + " tuổi ";
                        txtObjectType_Name.Text = Utility.sDbnull(dr[DmucDoituongkcb.Columns.TenDoituongKcb], "");
                        txtSoBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.MatheBhyt], "");
                        switch (Utility.sDbnull(dr[KcbLuotkham.Columns.MaDoituongKcb], "DV"))
                        {
                            case "BHYT":
                                if (Utility.Int32Dbnull(dr[KcbLuotkham.Columns.DungTuyen], 0) == 1)
                                {
                                    txtDTTT.Text = @"Đúng tuyến";
                                }
                                else
                                {
                                    txtDTTT.Text = @"Trái tuyến";
                                }
                                txtDTTT.Visible = true;
                                dtpBHYTFfromDate.Value = Convert.ToDateTime(dr[KcbLuotkham.Columns.NgaybatdauBhyt]);
                                dtpBHYTToDate.Value = Convert.ToDateTime(dr[KcbLuotkham.Columns.NgayketthucBhyt]);
                                break;
                            case "DV":
                                txtDTTT.Visible = false;
                                dtpBHYTFfromDate.Value = globalVariables.SysDate;

                                dtpBHYTToDate.Value = globalVariables.SysDate;
                               
                                break;
                        }
                        cmdCapnhatngayBHYT.Visible =
                                Utility.sDbnull(dr[KcbLuotkham.Columns.MaDoituongKcb], "DV") == "BHYT" &&  Utility.Coquyen("quyen_suathongtintheBHYT_khithanhtoan");
                        txtICD.ReadOnly = !Utility.Coquyen("quyen_nhapICD_khithanhtoan");
                        cmdSaveICD.Visible =  Utility.Coquyen("quyen_nhapICD_khithanhtoan");
                        
                        txtObjectType_Code.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.MaDoituongKcb], "");
                        
                        txtPtramBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.PtramBhytGoc], "0")+" %";//%đầu thẻ
                        //if (Utility.Byte2Bool(objLuotkham.DungTuyen))
                        //    txtPtramBHChiTra.Text = txtPtramBHYT.Text;
                        //else//Trái tuyến hiển thị % đầu thẻ * % tuyến
                        //{
                        //    decimal ptramBHYT = Utility.Int32Dbnull(dr[KcbLuotkham.Columns.PtramBhytGoc], 0) * Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0);
                        //    txtPtramBHChiTra.Text =Utility.Int32Dbnull( ptramBHYT/100,0).ToString() + " %";
                        //}
                        txtICD._Text = Utility.sDbnull(dr[KcbLuotkham.Columns.MabenhChinh], "");
                        txtDiaChi.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.DiaChi], "");
                        txtDiachiBHYT.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.DiachiBhyt], "");
                        toolTip1.SetToolTip(lblBHYT, Utility.DoTrim(txtDiachiBHYT.Text));
                   
                    //}
                        ucTamung1.ChangePatients(objLuotkham, "LYDOTAMUNG_NOITRU", 1);
                    ModifyHoanUngButtons();
                    KiemTraDaInPhoiBhyt();
                    GetDataChiTiet();
                    LaydanhsachLichsuthanhtoan_phieuchi();
                    
                }
                
                //if (objLuotkham.TrangthaiNoitru <=2)
                //{
                //    Utility.ShowMsg("Bệnh nhân bạn chọn chưa được lập phiếu ra viện nên không thể thanh toán nội trú");
                //    return;
                //}
                //if (objLuotkham.TrangthaiNoitru == 3)
                //{
                //    Utility.ShowMsg("Bệnh nhân bạn chọn mới lập phiếu ra viện và chưa được xác nhận tổng hợp ra viện nên không thể thanh toán nội trú");
                //    return;
                //}
                
            }
            catch(Exception exception)
            {
                log.Trace("Lỗi:"+ exception.Message);
            }
            finally
            {
                getThongtincanhbao(Utility.Int32Dbnull(txtPatient_ID.Text, -1));
                ModifyCommand();
                if (PropertyLib._ThanhtoanProperties.AutoTab) tabThongTinCanThanhToan.SelectedIndex = 0;
            }
        }
        void ModifyHoanUngButtons()
        {
            NoitruTamung objTamung = new Select().From(NoitruTamung.Schema)
                       .Where(NoitruTamung.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                       .And(NoitruTamung.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                       .And(NoitruTamung.Columns.KieuTamung).IsEqualTo(1)//Hoàn ứng. Có thể kiểm tra bằng trường trạng thái=1
                       .And(NoitruTamung.Columns.Noitru).IsEqualTo(1)
                       .ExecuteSingle<NoitruTamung>();
            //}
            cmdHoanung.Text = objTamung == null ? "Hoàn ứng" : "Hủy hoàn ứng";
            cmdHoanung.Tag = objTamung == null ? "0" : "1";
            cmdHoanung.Enabled = ucTamung1.grdTamung.GetDataRows().Length > 0;
        }
        private void KiemTraDaInPhoiBhyt()
        {
                SqlQuery sqlQuery = new Select().From(KcbPhieuDct.Schema)
                    .Where(KcbPhieuDct.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(txtPatient_Code.Text))
                    .And(KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtPatient_ID.Text))
                    .And(KcbPhieuDct.Columns.LoaiThanhtoan).IsEqualTo(Utility.Int32Dbnull(KieuThanhToan.NgoaiTru));
                if (sqlQuery.GetRecordCount() > 0)
                {
                    pnlSuangayinphoi.Visible = true;
                    var objPhieuDct = sqlQuery.ExecuteSingle<KcbPhieuDct>();
                    if (objPhieuDct != null)
                    {
                        dtNgayInPhoi.Value = Convert.ToDateTime(objPhieuDct.NgayTao);
                        cmdHuyInPhoiBHYT.Enabled = true;
                        cmdCapnhatngayinphoiBHYT.Enabled = true;
                        lblMessage.Visible = true;
                        lblMessage.BackColor = getColorMessage;
                        Utility.SetMsg(lblMessage,
                            string.Format("Đã in phôi bởi {0}, vào lúc: {1}", objPhieuDct.NguoiTao,
                                objPhieuDct.NgayTao), false);
                    }
                }
                else
                {
                    pnlSuangayinphoi.Visible = false;
                    dtNgayInPhoi.Value = globalVariables.SysDate;
                    cmdHuyInPhoiBHYT.Enabled = false;
                    dtNgayInPhoi.Enabled = true;
                    cmdCapnhatngayinphoiBHYT.Enabled = false;
                    if (Utility.sDbnull(txtObjectType_Code.Text) == "BHYT")
                    {
                        lblMessage.Visible = true;
                        Utility.SetMsg(lblMessage, string.Format("Bệnh nhân chưa in phôi bảo hiểm y tế"), true);
                        //lblMessage.BackColor = Color.Red;
                    }
                    else
                    {
                        Utility.SetMsg(lblMessage, string.Format(""), true);
                        lblMessage.Visible = false;
                    }
                }
        }

       

        private void GetDataChiTiet()
        {
            try
            {
                m_dtChiPhiThanhtoan =
                    _THANHTOAN.NoitruKcbThanhtoanLaythongtindvuChuathanhtoan(txtPatient_Code.Text, Utility.Int32Dbnull(txtPatient_ID.Text), 1,
                        globalVariables.MA_KHOA_THIEN, Utility.sDbnull(txtObjectType_Code.Text));
                LayDSChiPhiThanhToan();
                m_dtChiPhiThanhtoan.AcceptChanges();
                Utility.SetDataSourceForDataGridEx(grdThongTinChuaThanhToan, m_dtChiPhiThanhtoan, false, true, "trangthai_huy=0" + (PropertyLib._ThanhtoanProperties.Hienthidichvuchuathanhtoan ? " and trangthai_thanhtoan=0" : ""), "");
                GetChiPhiDaThanhToan();
                UpdateTuCheckKhiChuaThanhToan();
                SetSumTotalProperties();
            }
            catch (Exception exception)
            {
                log.Trace("Loi: "+ exception);
            }
        }

        private void LayDSChiPhiThanhToan()
        {
            try
            {
                var p = (from q in m_dtChiPhiThanhtoan.AsEnumerable()
                         where  q["trangthai_huy"].ToString() == "0"
                         group q by new { MA = q[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString(), TEN = q[KcbThanhtoanChitiet.Columns.TenLoaithanhtoan].ToString() } into r
                         select new
                         {
                             CHON = 1,
                             MA = r.Key.MA,
                             TEN = r.Key.TEN,
                             Tong_tien = r.Where(c=>c["trangthai_thanhtoan"].ToString() == "0" ).Sum(g => g.Field<decimal>("TT"))
                         }).ToList();

                grdDSKCB.DataSource = p;
                grdDSKCB.Refetch();
                
                foreach (GridEXRow grd in grdDSKCB.GetRows())
                {
                    grd.BeginEdit();
                    DataRow[] arrDr = m_dtChiPhiThanhtoan.Select("trangthai_thanhtoan = 0 AND trangthai_huy = 0 AND id_loaithanhtoan =" +
                                                  grd.Cells["MA"].Value);
                     int record = arrDr.Length;
                    if (record > 0)
                    {
                        grd.IsChecked = true;
                    }
                    else
                    {
                        grd.IsChecked = false;
                    }
                    grd.EndEdit();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy danh sách chi phí thanh toán");
            }
        }

        private void GetChiPhiDaThanhToan()
        {
            m_dtChiPhiDaThanhToan =
                _THANHTOAN.LayThongtinDaThanhtoan(txtPatient_Code.Text, Utility.Int32Dbnull(txtPatient_ID.Text), 1,"ALL");
            Utility.SetDataSourceForDataGridEx(grdThongTinDaThanhToan, m_dtChiPhiDaThanhToan, false, true, "1=1", "");
            if (m_dtChiPhiDaThanhToan.Rows.Count > 0)
            {
                dtPaymentDate.Enabled = false;
                dtPaymentDate.Value = Convert.ToDateTime(m_dtChiPhiDaThanhToan.Rows[0]["ngay_thanhtoan"].ToString());
            }
           
        }

        private void UpdateTuCheckKhiChuaThanhToan()
        {
            try
            {
                foreach (GridEXRow gridExRow in grdThongTinChuaThanhToan.GetDataRows())
                {
                    if (gridExRow.RowType == RowType.Record)
                    {
                        gridExRow.IsChecked = Utility.Int32Dbnull(gridExRow.Cells["trangthai_thanhtoan"].Value, 0) == 0
                                              && Utility.Int32Dbnull(gridExRow.Cells["trangthai_huy"].Value, 0) == 0;
                    }
                }
                grdThongTinChuaThanhToan.UpdateData();
            }
            catch (Exception)
            {
            }
        }
        decimal Chuathanhtoan = 0m;
        private void SetSumTotalProperties()
        {
            try
            {
                if (objLuotkham == null)
                {
                    objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text), Utility.DoTrim(txtPatient_Code.Text));
                }
                string ErrMsg = "";
                decimal newBHYT = Utility.DecimaltoDbnull(txtPtramBHChiTra.Text, 0);
                _THANHTOAN.TinhlaitienBhyTtruocThanhtoan(m_dtChiPhiThanhtoan, TaophieuThanhtoan(), objLuotkham, Taodulieuthanhtoanchitiet(ref ErrMsg), ref newBHYT);
                txtPtramBHChiTra.Text = newBHYT.ToString();
                GridEXColumn gridExColumntrangthaithanhtoan = getGridExColumn(grdThongTinChuaThanhToan, "trangthai_thanhtoan");
                GridEXColumn gridExColumn = getGridExColumn(grdThongTinChuaThanhToan, "TT_KHONG_PHUTHU");
                GridEXColumn gridExColumn_tutuc = getGridExColumn(grdThongTinChuaThanhToan, "TT_BN_KHONG_TUTUC");
                GridEXColumn gridExColumnTT = getGridExColumn(grdThongTinChuaThanhToan, "TT");
                GridEXColumn gridExColumnTT_chietkhau = getGridExColumn(grdThongTinChuaThanhToan, KcbThanhtoanChitiet.Columns.TienChietkhau);
                GridEXColumn gridExColumnBHYT = getGridExColumn(grdThongTinChuaThanhToan, "TT_BHYT");
                GridEXColumn gridExColumnTTBN = getGridExColumn(grdThongTinChuaThanhToan, "TT_BN");
                GridEXColumn gridExColumntutuc = getGridExColumn(grdThongTinChuaThanhToan, "tu_tuc");
                GridEXColumn gridExColumntrangthai_huy = getGridExColumn(grdThongTinChuaThanhToan, "trangthai_huy");
                GridEXColumn gridExColumnPhuThu = getGridExColumn(grdThongTinChuaThanhToan,
                    "TT_PHUTHU");
                var gridExFilterCondition_khong_Tutuc =
                    new GridEXFilterCondition(gridExColumntutuc, ConditionOperator.Equal, 0);
                var gridExFilterConditionTutuc =
                    new GridEXFilterCondition(gridExColumntutuc, ConditionOperator.Equal, 1);
                var gridExFilterChuathanhtoan =
                    new GridEXFilterCondition(gridExColumntrangthaithanhtoan, ConditionOperator.Equal, 0);
                var gridExFilterDathanhtoan =
                  new GridEXFilterCondition(gridExColumntrangthaithanhtoan, ConditionOperator.Equal, 1);
                var gridExFilterCondition_TuTuc =
                   new GridEXFilterCondition(gridExColumntutuc, ConditionOperator.Equal, 1);

                var gridExFilterConditionKhongTuTuc =
                    new GridEXFilterCondition(gridExColumntutuc, ConditionOperator.Equal, 0);
                var gridExFilterConditiontrangthai_huy =
                    new GridEXFilterCondition(gridExColumntrangthai_huy, ConditionOperator.Equal, 0);
                var gridExFilterConditiontrangthai_huy_va_khongtutuc =
                   new GridEXFilterCondition(gridExColumntrangthai_huy, ConditionOperator.Equal, 0);
                gridExFilterConditiontrangthai_huy_va_khongtutuc.AddCondition(gridExFilterConditionKhongTuTuc);
                GridEXColumn gridExColumnBNCT = getGridExColumn(grdThongTinChuaThanhToan,"bnhan_chitra");
                // Janus.Windows.GridEX.GridEXColumn gridExColumnTuTuc = getGridExColumn(grdThongTinChuaThanhToan, "bnhan_chitra");



                //decimal BN_KHONGTUTUC =
                //  Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.GetTotal(gridExColumn_tutuc, AggregateFunction.Sum,
                //      gridExFilterCondition_khong_Tutuc));
                //decimal TT =
                //    Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.GetTotal(gridExColumnTT, AggregateFunction.Sum,gridExFilterConditiontrangthai_huy));
                //decimal TT_Chietkhau =
                //   Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.GetTotal(gridExColumnTT_chietkhau, AggregateFunction.Sum,
                //       gridExFilterConditiontrangthai_huy));

                //decimal TT_KHONG_PHUTHU =
                //   Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.GetTotal(gridExColumn, AggregateFunction.Sum,
                //       gridExFilterConditiontrangthai_huy));
                //decimal TT_BHYT =
                //    Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.GetTotal(gridExColumnBHYT, AggregateFunction.Sum,
                //        gridExFilterConditiontrangthai_huy));
                //decimal TT_BN =
                //    Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.GetTotal(gridExColumnTTBN, AggregateFunction.Sum,
                //        gridExFilterConditiontrangthai_huy));
                //decimal TT_BN_DaTT =
                //   Utility.DecimaltoDbnull(grdThongTinDaThanhToan.GetTotal(gridExColumnTTBN, AggregateFunction.Sum,
                //       gridExFilterConditiontrangthai_huy));
                //Chuathanhtoan =
                //   Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.GetTotal(gridExColumnTTBN, AggregateFunction.Sum,
                //       gridExFilterChuathanhtoan));
                
                ////Tạm bỏ
                ////decimal PtramBHYT = 0;
                ////_THANHTOAN.LayThongPtramBHYT(TongChiphiBHYT, objLuotkham, ref PtramBHYT);
                //decimal PhuThu =
                //    Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.GetTotal(gridExColumnPhuThu, AggregateFunction.Sum));
                //decimal TuTuc =
                //    Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.GetTotal(gridExColumnBNCT, AggregateFunction.Sum,
                //        gridExFilterConditionTutuc));


                decimal BN_KHONGTUTUC = 0m;
                decimal TT = 0m;
                decimal TT_Chietkhau = 0m;

                decimal TT_KHONG_PHUTHU = 0m;
                decimal TT_BHYT = 0m;
                decimal TT_BN = 0m;
                decimal TT_BN_DaTT = 0m;
                Chuathanhtoan = 0m;

                //Tạm bỏ
                //decimal PtramBHYT = 0;
                //_THANHTOAN.LayThongPtramBHYT(TongChiphiBHYT, objLuotkham, ref PtramBHYT);
                decimal PhuThu = 0m;
                decimal TuTuc = 0m;
                foreach (DataRowView drv in m_dtChiPhiThanhtoan.DefaultView)
                {
                    if (Utility.Int32Dbnull(drv["tinh_chiphi"], 0) == 1)
                    {
                        if (Utility.Int32Dbnull(drv["tu_tuc"], 0) == 0)
                            BN_KHONGTUTUC += Utility.DecimaltoDbnull(drv["TT_BN_KHONG_TUTUC"], 0);
                        if (Utility.Int32Dbnull(drv["trangthai_huy"], 0) == 0)
                        {
                            TT += Utility.DecimaltoDbnull(drv["TT"], 0);
                            TT_Chietkhau += Utility.DecimaltoDbnull(drv[KcbThanhtoanChitiet.Columns.TienChietkhau], 0);
                            TT_KHONG_PHUTHU += Utility.DecimaltoDbnull(drv["TT_KHONG_PHUTHU"], 0);

                            TT_BHYT += Utility.DecimaltoDbnull(drv["TT_BHYT"], 0);
                            TT_BN += Utility.DecimaltoDbnull(drv["TT_BN"], 0);
                            TT_BN_DaTT += Utility.DecimaltoDbnull(drv["TT_BN"], 0);
                        }
                        if (Utility.Int32Dbnull(drv["trangthai_thanhtoan"], 0) == 0)
                            Chuathanhtoan += Utility.DecimaltoDbnull(drv["TT_BN"], 0);
                        PhuThu += Utility.DecimaltoDbnull(drv["TT_PHUTHU"], 0);
                        if (Utility.Int32Dbnull(drv["tu_tuc"], 0) == 1)
                            TuTuc += Utility.DecimaltoDbnull(drv["TT_TUTUC"], 0);
                    }
                }
                txtTongChiPhi.Text = Utility.sDbnull(TT);
                TT_KHONG_PHUTHU -= TuTuc;
                txtTongtienDCT.Text = objLuotkham.MaDoituongKcb == "DV" ? "0" : Utility.sDbnull( TT_BHYT  +BN_KHONGTUTUC);// objLuotkham.MaDoituongKcb == "DV" ? "0" : Utility.sDbnull(TT_KHONG_PHUTHU);
                txtPhuThu.Text = Utility.sDbnull(PhuThu);
                txtTuTuc.Text = Utility.sDbnull(TuTuc);
                //decimal BHCT = TongChiphiBHYT*PtramBHYT/100;
                txtBHCT.Text = Utility.sDbnull(TT_BHYT, "0");
                decimal BNCT = BN_KHONGTUTUC;
                txtBNCT.Text = Utility.sDbnull(BNCT);
                decimal BNPhaiTra = BNCT + Utility.DecimaltoDbnull(txtTuTuc.Text, 0) +
                                    Utility.DecimaltoDbnull(txtPhuThu.Text);
                txtBNPhaiTra.Text = Utility.sDbnull(TT_BN);
                TinhToanSoTienPhaithu();
                decimal Tong_Tamung = 0;
                if (ucTamung1.m_dtTamung != null)
                {
                    Tong_Tamung = Utility.DecimaltoDbnull(ucTamung1.m_dtTamung.Compute("SUM(so_tien)", "1=1"), 0);
                    if (Math.Abs(Tong_Tamung) != 0)
                    {
                        decimal chenhlech = Chuathanhtoan - Tong_Tamung;
                        if (cmdHoanung.Tag.ToString() == "1")//Đã hoàn ứng
                            chenhlech = 0;
                        if (chenhlech > 0)
                        {
                            lblThuathieu.Text = "BN Nộp tiền";
                            txtThuathieu.Text = chenhlech.ToString();
                        }
                        else
                        {
                            lblThuathieu.Text = "Trả lại BN";
                            txtThuathieu.Text = Math.Abs(chenhlech).ToString();
                        }
                    }
                }

                if (Tong_Tamung == 0)
                {
                    lblThuathieu.Text = "BN Nộp tiền";
                    txtThuathieu.Text = txtSoTienCanNop.Text;
                }
                ModifyCommand();
            }
            catch
            { }
        }

        private void TinhToanSoTienPhaithu()
        {
            try
            {
                List<GridEXRow> query = (from thanhtoan in grdThongTinChuaThanhToan.GetCheckedRows()
                                         where Utility.Int32Dbnull(thanhtoan.Cells["trangthai_huy"].Value) == 0
                                               && Utility.Int32Dbnull(thanhtoan.Cells["trangthai_thanhtoan"].Value) == 0
                                               //&& Utility.Int32Dbnull(thanhtoan.Cells["trang_thai"].Value) == 0
                                         select thanhtoan).ToList();

                List<GridEXRow> query1 = (from thanhtoan in grdThongTinChuaThanhToan.GetCheckedRows()
                                         where Utility.Int32Dbnull(thanhtoan.Cells["trangthai_huy"].Value) == 0
                                         select thanhtoan).ToList();
               
                decimal thanhtien = query.Sum(c => Utility.DecimaltoDbnull(c.Cells["TT_BN"].Value));
                decimal Chietkhauchitiet = query1.Sum(c => Utility.DecimaltoDbnull(c.Cells["tien_chietkhau"].Value));
                txtSoTienCanNop.Text = Utility.sDbnull(thanhtien - Chietkhauchitiet);
                txtTienChietkhau.Text = Utility.sDbnull( Chietkhauchitiet);

                ModifyCommand();
            }
            catch (Exception)
            {
            }
            //decimal thanhtien =
        }

        private GridEXColumn getGridExColumn(GridEX gridEx, string colName)
        {
            return gridEx.RootTable.Columns[colName];
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frm_THANHTOAN_NOITRU_old_Load(object sender, EventArgs e)
        {
            InitData();
            txtPttt.Init();
            SetProperties();
            LoadPrinter();
            AutocompleteICD();
            LoadInvoiceInfo();
            ClearControl();
            if (PropertyLib._ThanhtoanProperties.SearchWhenStart) cmdSearch_Click(cmdSearch, e);
            m_blnHasloaded = true;
            txtMaLanKham.Focus();
            txtMaLanKham.SelectAll();
            ModifyCommand();
        }
        void AutocompleteICD()
        {
            try
            {
                if (globalVariables.gv_dtDmucBenh == null) return;
                if (!globalVariables.gv_dtDmucBenh.Columns.Contains("ShortCut"))
                    globalVariables.gv_dtDmucBenh.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in globalVariables.gv_dtDmucBenh.Rows)
                {
                    string shortcut = "";
                    string realName = dr[DmucBenh.Columns.TenBenh].ToString().Trim() + " " +
                                      Utility.Bodau(dr[DmucBenh.Columns.TenBenh].ToString().Trim());
                    shortcut = dr[DmucBenh.Columns.MaBenh].ToString().Trim();
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
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                var source = new List<string>();
                var query = from p in globalVariables.gv_dtDmucBenh.AsEnumerable()
                            select Utility.sDbnull( p[DmucBenh.Columns.IdBenh]) + "#" + Utility.sDbnull( p[DmucBenh.Columns.MaBenh]) + "@" + Utility.sDbnull( p[DmucBenh.Columns.TenBenh]) + "@" + p.Field<string>("shortcut").ToString();
                source = query.ToList();
                this.txtICD.AutoCompleteList = source;
                this.txtICD.TextAlign = HorizontalAlignment.Center;
                this.txtICD.CaseSensitive = false;
                this.txtICD.MinTypedCharacters = 1;

            }
        }
        bool hasLoadedRedInvoice = false;
        private void LoadInvoiceInfo()
        {
            try
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false)!="1") return;
                Utility.ResetMessageError(errorProvider1);
                if (globalVariables.UserName == null)
                    return;
                dtCapPhat = _THANHTOAN.LayHoaDonCapPhat(globalVariables.IsAdmin?"ADMIN": globalVariables.UserName);
                hasLoadedRedInvoice = true;
                if (dtCapPhat.Rows.Count <= 0)
                {
                    gpThongTinHoaDon.Enabled = false;
                    Utility.SetMsgError(errorProvider1, gpThongTinHoaDon, "Đã xử dụng hết hóa đơn được cấp ");
                }
                grdHoaDonCapPhat.DataSource = dtCapPhat;
                grdHoaDonCapPhat.AutoSizeColumns();
                var _HoadonLog =
                    new Select().From(HoadonLog.Schema).Where(HoadonLog.Columns.MaNhanvien).IsEqualTo(
                        globalVariables.UserName).OrderDesc(HoadonLog.Columns.IdHdonLog)
                        .And(HoadonLog.Columns.TrangThai).IsEqualTo(0)
                        .ExecuteSingle<HoadonLog>();
                if (_HoadonLog != null)
                {
                    Utility.GotoNewRowJanus(grdHoaDonCapPhat, HoadonCapphat.Columns.IdCapphat,
                        Utility.sDbnull(_HoadonLog.IdCapphat));
                    //grdHoaDonCapPhat_SelectionChanged(grdHoaDonCapPhat, new EventArgs());
                }
                else
                {
                    grdHoaDonCapPhat.MoveFirst();
                }
            }
            catch
            {
                hasLoadedRedInvoice = false;
            }
        }

        private void grdHoaDonCapPhat_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false)!="1") return;
                if (grdHoaDonCapPhat.CurrentRow != null)
                {
                    if (grdHoaDonCapPhat.CurrentRow.RowType == RowType.Record)
                    {
                        txtMauHD.Text =
                            Utility.sDbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.MauHoadon].Value, "");
                        txtKiHieu.Text =
                            Utility.sDbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.KiHieu].Value, "");
                        txtSerieDau.Text =
                            Utility.sDbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.SerieDau].Value, "");
                        txtSerieCuoi.Text =
                            Utility.sDbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.SerieCuoi].Value,
                                "");
                        int sSerie =
                            Utility.Int32Dbnull(
                                grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.SerieHientai].Value, 0);
                        txtSerie.Text =
                            Utility.sDbnull(sSerie <= 0
                                ? Utility.Int32Dbnull(
                                    grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.SerieDau].Value, 0)
                                : sSerie + 1);
                        txtSerie.MaxLength = Utility.DoTrim(txtSerieCuoi.Text).Length;
                        txtSerie.Text =
                            txtSerie.Text.PadLeft(
                                Utility.sDbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.SerieDau].Value)
                                    .Length, '0');
                        txtMaQuyen.Text =
                            Utility.sDbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.MaQuyen].Value, "");
                        HOADON_CAPPHAT_ID =
                            Utility.Int32Dbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.IdCapphat].Value,
                                0);

                    }
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin mẫu hóa đơn trên lưới.");
            }
        }

        /// <summary>
        ///     hà thực hiện việc khởi tạo thông tin của Form khi load
        /// </summary>
        private void InitData()
        {
            try
            {
                DataTable m_dtDoiTuong = THU_VIEN_CHUNG.LaydanhsachDoituongKcb();
                m_blnHasloaded = true;
                DataBinding.BindDataCombobox(cboObjectType_ID, m_dtDoiTuong, DmucDoituongkcb.Columns.MaDoituongKcb,
                    DmucDoituongkcb.Columns.TenDoituongKcb, "---Đối tượng---",false);
               
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                    Utility.ShowMsg(exception.ToString());
            }
        }


        /// <summary>
        ///     hàm thực hiện việc thanh toán bản ghi đang chọn trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.EnableButton(cmdThanhToan, false);
                if (blnJustPayment) return;
                blnJustPayment = true;
                if (!IsValidata()) return;
                PayCheckDate(dtInput_Date.Value);
                PerformAction();
                blnJustPayment = false;
            }
            catch
            {
                blnJustPayment = false;
            }
            finally
            {
                Utility.EnableButton(cmdThanhToan, true);
                ModifyCommand();
                blnJustPayment = false;
            }
        }
        private void PayCheckDate(DateTime InputDate)
        {
            //if (globalVariables.SysDate.Date != InputDate.Date)
            //{
            frm_ChonngayThanhtoan frm = new frm_ChonngayThanhtoan();
            frm.pdt_InputDate = dtInput_Date.Value;
            frm.ShowDialog();
            if (!frm.mv_blnCancel)
            {
                dtPaymentDate.Value = frm.pdt_InputDate;
            }
            //}
        }

        private void LaydanhsachLichsuthanhtoan_phieuchi()
        {
            try
            {
                m_dtPayment = null;
                m_dtPhieuChi = null;
                m_dtPayment =
                    _THANHTOAN.LaythongtinCacLanthanhtoan(Utility.sDbnull(txtPatient_Code.Text, ""),
                        Utility.Int32Dbnull(txtPatient_ID.Text, -1), 1,1,0,
                        globalVariables.MA_KHOA_THIEN, "ALL");

                m_dtPhieuChi = _THANHTOAN.LaythongtinCacLanthanhtoan(Utility.sDbnull(txtPatient_Code.Text, ""),
                        Utility.Int32Dbnull(txtPatient_ID.Text, -1), 0, 0, 1,
                        globalVariables.MA_KHOA_THIEN, "ALL");

                Utility.SetDataSourceForDataGridEx(grdPayment, m_dtPayment, false, true, "1=1", "");
                Utility.SetDataSourceForDataGridEx(grdPhieuChi, m_dtPhieuChi, false, true, "1=1", "");
                if (m_dtPayment.Rows.Count <= 0)
                {
                    txtsotiendathu.Text = "0";
                    txtDachietkhau.Text = "0";
                }
                else
                {
                    txtDachietkhau.Text = m_dtPayment.Compute("SUM(tongtien_chietkhau)", "1=1").ToString();
                    txtsotiendathu.Text = (Utility.DecimaltoDbnull(m_dtPayment.Compute("SUM(TT_BN)", "1=1"), 0) - Utility.DecimaltoDbnull(txtDachietkhau.Text, 0)).ToString();
                }
            }
            catch (Exception exception)
            {
                Utility.CatchException("Lỗi khi lấy thông tin lịch sử thanh toán của bệnh nhân", exception);
                // throw;
            }
        }

       
        public decimal TongtienCK = 0m;
        public decimal TongtienCK_Hoadon = 0m;
        public decimal TongtienCK_chitiet = 0m;
        public string ma_ldoCk = "";
        private void PerformAction()
        {
            try
            {
                objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text), Utility.DoTrim(txtPatient_Code.Text));
                if (objLuotkham != null)
                {
                    if (INPHIEU_CLICK)
                    {
                        goto INPHIEU;
                    }
                    if (PropertyLib._ThanhtoanProperties.Hoitruockhithanhtoan)
                        if (!Utility.AcceptQuestion("Bạn có muốn thanh toán cho bệnh nhân này không", "Thông báo thanh toán", true))
                        {
                            return;
                        }

                    //Nếu thanh toán khi in phôi thì không cần hỏi
                    INPHIEU:
                    bool IN_HOADON = false;
                    string ErrMsg = "";
                    long IdHdonLog = -1;
                    IN_HOADON = Utility.DecimaltoDbnull(txtSoTienCanNop.Text, 0) > 0;
                    if (IN_HOADON)
                    {
                        if (chkLayHoadon.Checked)//nếu lấy hóa đơn đỏ mới kiểm tra
                        {
                            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false)=="1")//Chỉ khi dùng hóa đơn đỏ mới kiểm tra tiếp.
                            {
                                if (grdHoaDonCapPhat.RowCount<=0)
                                {
                                    uiTabHoadon_chiphi.SelectedTab = tabpageHoaDon;
                                    Utility.ShowMsg("Bạn cần khai báo quyển hóa đơn đỏ trước khi sử dụng tính năng thanh toán với hóa đơn đỏ");
                                    return;
                                }
                                if (!Utility.isValidGrid(grdHoaDonCapPhat))
                                {
                                    uiTabHoadon_chiphi.SelectedTab = tabpageHoaDon;
                                    Utility.ShowMsg("Mời bạn chọn quyển hóa đơn thanh toán");
                                    return;
                                }
                                if (!checkSerie(ref IdHdonLog))
                                {
                                    return;
                                }
                            }
                        }
                    }

                    List<KcbThanhtoanChitiet> lstItems = Taodulieuthanhtoanchitiet(ref ErrMsg);
                    if (lstItems == null)
                    {
                        Utility.ShowMsg("Lỗi khi tạo dữ liệu thanh toán chi tiết. Liên hệ đơn vị cung cấp phần mềm để được hỗ trợ\n" + ErrMsg);
                        return;
                    }
                    bool bo_ckchitiet = true;
                    string ma_uudai = "";
                    TongtienCK_chitiet = Utility.DecimaltoDbnull(txtTienChietkhau.Text);
                    if (chkChietkhauthem.Checked || TongtienCK_chitiet > 0)
                    {
                        frm_ChietkhauTrenHoadon _ChietkhauTrenHoadon = new frm_ChietkhauTrenHoadon();
                        _ChietkhauTrenHoadon.TongCKChitiet = Utility.DecimaltoDbnull(txtTienChietkhau.Text);
                        _ChietkhauTrenHoadon.TongtienBN = Utility.DecimaltoDbnull(txtSoTienCanNop.Text) + Utility.DecimaltoDbnull(txtTienChietkhau.Text);
                        _ChietkhauTrenHoadon.ShowDialog();
                        if (!_ChietkhauTrenHoadon.m_blnCancel)
                        {
                            ma_uudai = _ChietkhauTrenHoadon.autoUudai.myCode;
                            bo_ckchitiet = _ChietkhauTrenHoadon.chkAll.Checked;
                            if (_ChietkhauTrenHoadon.chkBoChitiet.Checked)
                            {
                                TongtienCK_chitiet = 0;
                                mnuHuyChietkhau.PerformClick();
                            }
                            TongtienCK = _ChietkhauTrenHoadon.TongtienCK;
                            TongtienCK_Hoadon = _ChietkhauTrenHoadon.TongCKHoadon;
                            ma_ldoCk = _ChietkhauTrenHoadon.ma_ldoCk;
                        }
                        else
                        {
                            if (TongtienCK_chitiet > 0)
                            {
                                Utility.ShowMsg("Bạn vừa thực hiện hủy thao tác nhập thông tin chiết khấu. Yêu cầu bạn nhập lý do chiết khấu(Do tiền chiết khấu >0). Mời bạn bấm lại nút thanh toán để bắt đầu lại");
                                return;
                            }
                            else
                            {
                                if (!Utility.AcceptQuestion("Bạn vừa thực hiện hủy thao tác nhập thông tin chiết khấu. Bạn có muốn tiếp tục thanh toán không cần chiết khấu hay không?","Xác nhận chiết khấu",true))
                                {
                                    return;
                                }
                            }
                        }
                    }
                    decimal TTBN_Chitrathucsu = 0;
                     ErrMsg = "";
                    ActionResult actionResult = _THANHTOAN.ThanhtoanChiphiDvuKcb(TaophieuThanhtoan(), objLuotkham,
                        lstItems,null, ref v_Payment_ID, IdHdonLog,
                        chkLayHoadon.Checked &&
                        THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false) == "1",bo_ckchitiet,ma_uudai,
                        ref TTBN_Chitrathucsu, ref ErrMsg);

                    IN_HOADON = TTBN_Chitrathucsu > 0;
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            LaydanhsachLichsuthanhtoan_phieuchi();
                            GetDataChiTiet();
                            Utility.GotoNewRowJanus(grdPayment, KcbThanhtoan.Columns.IdThanhtoan, v_Payment_ID.ToString());
                            if (v_Payment_ID <= 0)
                            {
                                grdPayment.MoveFirst();
                            }
                            txtPatient_Code.Focus();
                            txtPatient_Code.SelectAll();
                            //Tạm rem phần hóa đơn đỏ lại
                            if (IN_HOADON && PropertyLib._MayInProperties.TudonginhoadonSaukhiThanhtoan)
                            {
                                int KCB_THANHTOAN_KIEUINHOADON = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_KIEUINHOADON", "1", false));
                                if (KCB_THANHTOAN_KIEUINHOADON == 1 || KCB_THANHTOAN_KIEUINHOADON == 3)
                                    InHoadon();
                                if (KCB_THANHTOAN_KIEUINHOADON == 2 || KCB_THANHTOAN_KIEUINHOADON == 3)
                                    new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, v_Payment_ID, objLuotkham, 1);
                            }
                            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false)=="1")
                            {
                                grdHoaDonCapPhat.CurrentRow.BeginEdit();
                                grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.SerieHientai].Value = Utility.sDbnull(txtSerie.Text);
                                grdHoaDonCapPhat.CurrentRow.EndEdit();
                                txtSerie.Text = Utility.sDbnull(Utility.Int32Dbnull(txtSerie.Text) + 1);
                                txtSerie.Text = txtSerie.Text.PadLeft(Utility.sDbnull(txtSerieDau.Text).Length, '0');
                            }
                            if (PropertyLib._ThanhtoanProperties.HienthidichvuNgaysaukhithanhtoan)
                            {
                                ShowPaymentDetail(v_Payment_ID);
                            }
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình thanh toán", "Thông báo lỗi", MessageBoxIcon.Error);
                            break;
                        case ActionResult.Cancel:
                            Utility.ShowMsg(ErrMsg);
                            break;
                    }
                    IN_HOADON = false;
                    INPHIEU_CLICK = false;
                }
            }
            catch (Exception exception)
            {
               
            }
            finally
            {
                TongtienCK = 0m;
                TongtienCK_chitiet = 0m;
                TongtienCK_Hoadon = 0m;
                ma_ldoCk = "";
                ModifyCommand();
            }
        }
        void ShowPaymentDetail(long v_Payment_ID)
        {
            if (objLuotkham == null)
            {
                objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text), Utility.DoTrim(txtPatient_Code.Text));
            }
            if (objLuotkham != null)
            {
                frm_HuyThanhtoan frm = new frm_HuyThanhtoan("ALL");
                frm.objLuotkham = objLuotkham;
                frm.v_Payment_Id = v_Payment_ID;
                frm.Chuathanhtoan = Chuathanhtoan;
                frm.txtSoTienCanNop.Text = txtSoTienCanNop.Text;
                frm.TotalPayment = grdPayment.GetDataRows().Length;
                frm.ShowCancel = false;
                frm.ShowDialog();
            }
        }
        /// <summary>
        ///     hàm thực hiện mảng của chi tiết thanh toán chi tiết
        /// </summary>
        /// <returns></returns>
        private List< KcbThanhtoanChitiet> Taodulieuthanhtoanchitiet(ref string errMsg)
        {
            try
            {
                List<KcbThanhtoanChitiet> lstItems = new List<KcbThanhtoanChitiet>();
                foreach (GridEXRow gridExRow in grdThongTinChuaThanhToan.GetCheckedRows())
                {
                   KcbThanhtoanChitiet newItem = new KcbThanhtoanChitiet();
                    newItem.IdThanhtoan = -1;
                    newItem.IdChitiet = -1;
                    newItem.TinhChiphi = Utility.ByteDbnull(gridExRow.Cells[KcbThanhtoanChitiet.Columns.TinhChiphi].Value, 1);
                    newItem.PtramBhyt = objLuotkham.PtramBhyt.Value;
                    newItem.PtramBhytGoc = objLuotkham.PtramBhytGoc.Value;
                    newItem.SoLuong = Utility.DecimaltoDbnull(gridExRow.Cells[KcbThanhtoanChitiet.Columns.SoLuong].Value, 0);
                    //Phần tiền BHYT chi trả,BN chi trả sẽ tính lại theo % mới nhất của bệnh nhân trong phần Business
                    newItem.BnhanChitra = Utility.DecimaltoDbnull(gridExRow.Cells[KcbThanhtoanChitiet.Columns.BnhanChitra].Value, 0);
                    newItem.BhytChitra = Utility.DecimaltoDbnull(gridExRow.Cells[KcbThanhtoanChitiet.Columns.BhytChitra].Value, 0);
                    newItem.DonGia =Utility.DecimaltoDbnull(gridExRow.Cells["don_gia"].Value, 0);
                    newItem.TyleTt = Utility.DecimaltoDbnull(gridExRow.Cells["tyle_tt"].Value, 0);
                    newItem.PhuThu = Utility.DecimaltoDbnull(gridExRow.Cells["PHU_THU"].Value, 0);
                    newItem.TuTuc = Utility.ByteDbnull(gridExRow.Cells["tu_tuc"].Value, 0);
                    newItem.IdPhieu = Utility.Int32Dbnull(gridExRow.Cells["id_phieu"].Value);
                    newItem.IdKham = Utility.Int32Dbnull(gridExRow.Cells["Id_Kham"].Value);
                    newItem.IdPhieuChitiet = Utility.Int32Dbnull(gridExRow.Cells["Id_Phieu_Chitiet"].Value, -1);
                    newItem.IdDichvu = Utility.Int16Dbnull(gridExRow.Cells["Id_dichvu"].Value, -1);
                    newItem.IdChitietdichvu = Utility.Int16Dbnull(gridExRow.Cells["Id_Chitietdichvu"].Value, -1);
                    newItem.TenChitietdichvu = Utility.sDbnull(gridExRow.Cells["Ten_Chitietdichvu"].Value, "Không xác định").Trim();
                    newItem.TenBhyt = Utility.sDbnull(gridExRow.Cells["ten_bhyt"].Value, "Không xác định").Trim();
                    newItem.DonviTinh =Utility.chuanhoachuoi(Utility.sDbnull(gridExRow.Cells["Ten_donvitinh"].Value, "Lượt"));
                    newItem.SttIn = Utility.Int16Dbnull(gridExRow.Cells["stt_in"].Value, 0);
                    newItem.IdKhoakcb = Utility.Int16Dbnull(gridExRow.Cells["id_khoakcb"].Value, -1);
                    newItem.IdPhongkham = Utility.Int16Dbnull(gridExRow.Cells["id_phongkham"].Value, -1);
                    newItem.IdLichsuDoituongKcb = Utility.Int64Dbnull(gridExRow.Cells[KcbThanhtoanChitiet.Columns.IdLichsuDoituongKcb].Value, -1);
                    newItem.MatheBhyt = Utility.sDbnull(gridExRow.Cells[KcbThanhtoanChitiet.Columns.MatheBhyt].Value, -1);
                    newItem.IdBacsiChidinh = Utility.Int16Dbnull(gridExRow.Cells[KcbThanhtoanChitiet.Columns.IdBacsiChidinh].Value, -1);
                    newItem.IdLoaithanhtoan = Utility.ByteDbnull(gridExRow.Cells[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].Value, -1);
                    newItem.TenLoaithanhtoan = THU_VIEN_CHUNG.MaKieuThanhToan(Utility.Int32Dbnull(gridExRow.Cells[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].Value, -1));
                    newItem.TienChietkhau = Utility.DecimaltoDbnull(gridExRow.Cells[KcbThanhtoanChitiet.Columns.TienChietkhau].Value, 0m);
                    newItem.TileChietkhau = Utility.DecimaltoDbnull(gridExRow.Cells[KcbThanhtoanChitiet.Columns.TileChietkhau].Value, 0m);
                    newItem.MaDoituongKcb = objLuotkham.MaDoituongKcb;
                    newItem.KieuChietkhau = "%";
                    newItem.IdThanhtoanhuy = -1 ;
                    newItem.TrangthaiHuy = 0;
                    newItem.TrangthaiBhyt = 0;
                    newItem.TrangthaiChuyen = 0;
                    newItem.NoiTru = 1;
                    newItem.NguonGoc = Utility.ByteDbnull(gridExRow.Cells["noi_tru"].Value, -1);
                    newItem.NgayTao = globalVariables.SysDate;
                    newItem.NguoiTao = globalVariables.UserName;
                    lstItems.Add(newItem);
                }
                return lstItems;
            }
            catch(Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }

        

        private KcbThanhtoan TaophieuThanhtoan()
        {
            KcbThanhtoan objPayment = new KcbThanhtoan();
            objPayment.IdThanhtoan = -1;
            objPayment.MaLuotkham = Utility.sDbnull(txtPatient_Code.Text, "");
            objPayment.IdBenhnhan = Utility.Int32Dbnull(txtPatient_ID.Text, -1);
            objPayment.NgayThanhtoan = dtPaymentDate.Value;
            objPayment.IdNhanvienThanhtoan = globalVariables.gv_intIDNhanvien;
            objPayment.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
            objPayment.KieuThanhtoan = 0;
            objPayment.NoiTru = 1;
            objPayment.TrangthaiIn = 0;
            objPayment.NgayIn = null;
            objPayment.NguoiIn = string.Empty;
            objPayment.MaPttt = txtPttt.myCode;
            objPayment.NgayTonghop = null;
            objPayment.NguoiTonghop = string.Empty;
            objPayment.NgayChot = null;
            objPayment.TrangthaiChot = 0;
            objPayment.TongTien = Utility.DecimaltoDbnull(txtSoTienCanNop.Text, 0);
            objPayment.NgayRavien = objLuotkham.NgayRavien;
            //2 mục này được tính lại ở Business
            objPayment.BnhanChitra = -1;
            objPayment.BhytChitra = -1;
            objPayment.TileChietkhau = 0;
            objPayment.KieuChietkhau = "T";
            objPayment.TongtienChietkhau = TongtienCK;
            objPayment.TongtienChietkhauChitiet = TongtienCK_chitiet;
            objPayment.TongtienChietkhauHoadon = TongtienCK_Hoadon;
            if (chkLayHoadon.Checked &&THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false)=="1")
            {
                objPayment.MauHoadon =Utility.DoTrim( txtMauHD.Text);
                objPayment.KiHieu =Utility.DoTrim( txtKiHieu.Text);
                objPayment.IdCapphat = Utility.Int32Dbnull(grdHoaDonCapPhat.GetValue(HoadonCapphat.Columns.IdCapphat), -1);
                objPayment.MaQuyen = Utility.DoTrim(txtMaQuyen.Text);
                objPayment.Serie = Utility.DoTrim(txtSerie.Text);
            }

            objPayment.MaLydoChietkhau = ma_ldoCk;
            objPayment.NgayTao = globalVariables.SysDate;
            objPayment.NguoiTao = globalVariables.UserName;
            objPayment.IpMaytao = globalVariables.gv_strIPAddress;
            objPayment.TenMaytao = globalVariables.gv_strComputerName;
            return objPayment;
        }

        private bool IsValidata()
        {
            objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text), Utility.DoTrim(txtPatient_Code.Text));
            if (objLuotkham ==null)
            {
                Utility.ShowMsg("Không lấy được thông tin bệnh nhân cần thanh toán. Đề nghị liên hệ IT để được giải quyết");
                return false;
            }

            if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) && Utility.DoTrim(objLuotkham.MatheBhyt) == "")
            {
                Utility.ShowMsg("Bệnh nhân BHYT cần nhập mã thẻ BHYT trước khi thanh toán");
                return false;
            }
            if (!Utility.Byte2Bool(objLuotkham.TthaiThopNoitru))
            {
                Utility.ShowMsg("Bệnh nhân chưa được Khoa nội trú tổng hợp xuất viện nên không được phép thanh toán");
                return false;
            }
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_TAICHINH_DUYET_TRUOCKHITHANHTOAN", "0", true) == "1" && Utility.Int32Dbnull(objLuotkham.TrangthaiNoitru, -1) == 4)
            {
                Utility.ShowMsg("Bệnh nhân chưa được duyệt thanh toán nội trú nên không thể thanh toán. Đề nghị bạn kiểm tra lại");
                return false;
            }
            
            if (Utility.Byte2Bool(objLuotkham.TthaiThanhtoannoitru) || objLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã được thanh toán nội trú nên bạn không thể thanh toán tiếp. Đề nghị kiểm tra lại");
                return false;
            }
            if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_BHYT_NHIEULAN", "0", false) == "0")
            {
                KcbThanhtoan objthanhtoan = new Select().From(KcbThanhtoan.Schema)
                    .Where(KcbThanhtoan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(KcbThanhtoan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .ExecuteSingle<KcbThanhtoan>();
                if (objthanhtoan != null)
                {
                    Utility.ShowMsg(string.Format("Bệnh nhân {0} thuộc đối tượng BHYT đã được thanh toán ít nhất một lần.\nHệ thống đang cấu hình không cho phép đối tượng BHYT thanh toán nhiều lần\nDo vậy bạn cần hủy thanh toán của các lần thanh toán trước để thực hiện một lần thanh toán duy nhất cho đối tượng này", txtTenBenhNhan.Text));
                    return false;
                }
            }
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_TUDONGHOANUNG_KHITHANHTOANNOITRU","0",false)=="0" )
            {
                NoitruTamung objTamung = new Select().From(NoitruTamung.Schema).Where(NoitruTamung.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(NoitruTamung.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .And(NoitruTamung.Columns.TrangThai).IsEqualTo(0)
                    .And(NoitruTamung.Columns.KieuTamung).IsEqualTo(0)
                    .And(NoitruTamung.Columns.Noitru).IsEqualTo(1)
                    .ExecuteSingle<NoitruTamung>();
                if (objTamung != null)
                {
                    Utility.ShowMsg("Bạn cần thực hiện thao tác hoàn ứng tiền cho bệnh nhân trước khi thực hiện thanh toán nội trú");
                    return false;
                }
            }
            bool b_CheckPayment = false;
            GridEXRow[] checkList = grdThongTinChuaThanhToan.GetCheckedRows();
            if (grdThongTinChuaThanhToan.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn ít nhất một dịch vụ chưa thanh toán để thực hiện thanh toán", "Thông báo", MessageBoxIcon.Warning);
                grdThongTinChuaThanhToan.Focus();
                return false;
            }
            foreach (GridEXRow gridExRow in grdThongTinChuaThanhToan.GetCheckedRows())
            {
                if (gridExRow.Cells["trangthai_thanhtoan"].Value.ToString() == "1")
                {
                    b_CheckPayment = true;
                    break;
                }
            }
            if (b_CheckPayment)
            {
                Utility.ShowMsg("Bạn phải chọn các bản ghi chưa thực hiện thanh toán mới thanh toán được", "Thông báo", MessageBoxIcon.Warning);
                grdThongTinChuaThanhToan.Focus();
                return false;
            }
            foreach (GridEXRow gridExRow in grdThongTinChuaThanhToan.GetCheckedRows())
            {
                if (gridExRow.Cells["trangthai_huy"].Value.ToString() == "1")
                {
                    b_CheckPayment = true;
                    break;
                }
            }
            if (b_CheckPayment)
            {
                Utility.ShowMsg("Bạn phải chọn bản ghi chưa được hủy mới thanh toán", "Thông báo",
                    MessageBoxIcon.Warning);
                grdThongTinChuaThanhToan.Focus();
                return false;
            }
            if (txtPttt.myCode == "-1")
            {
                Utility.ShowMsg("Bạn phải chọn phương thức thanh toán trước khi thực hiện thanh toán");
                txtPttt.Focus();
                return false;
            }
            return true;
        }

        private void txtLuongCoBan_TextChanged(object sender, EventArgs e)
        {
            //Utility.FormatCurrencyHIS(txtLuongCoBan);
        }

        //private void chkChuaThanhToan_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string _rowFilter = "1=1";
        //        if (chkChuaThanhToan.Checked)
        //        {
        //            _rowFilter = string.Format("{0}={1}", "Payment_status", 0);
        //        }
        //        m_dtChiPhiThanhtoan.DefaultView.RowFilter = _rowFilter;
        //        m_dtChiPhiThanhtoan.AcceptChanges();
        //    }catch(Exception exception)
        //    {
        //    }
        //}
        //private void chkDaThanhToan_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string _rowFilter = "1=1";
        //        if (chkDaThanhToan.Checked)
        //        {
        //            _rowFilter = string.Format("{0}={1}", "Payment_status", 1);
        //        }
        //        m_dtChiPhiThanhtoan.DefaultView.RowFilter = _rowFilter;
        //        m_dtChiPhiThanhtoan.AcceptChanges();
        //    }
        //    catch (Exception exception)
        //    {
        //    }
        //}
        /// <summary>
        ///     /hàm thực hiện việc lọc thông tin chọn thông tin của chưa thanh toana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <summary>
        ///     hàm thực hiện việc khởi tọa sự kiện của lưới thanh toán
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPayment_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "cmdPHIEU_THU")
            {
                CallPhieuThu();
            }
            if (e.Column.Key == "cmdHUY_PHIEUTHU")
            {
                HuyThanhtoan();
            }
        }
        string ma_lydohuy = "";
        
        private void HuyThanhtoan()
        {
            ma_lydohuy = "";
            if (!Utility.isValidGrid(grdPayment)) return;
            if (grdPayment.CurrentRow != null)
            {
                if (objLuotkham == null)
                {
                    objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text), Utility.DoTrim(txtPatient_Code.Text));
                }
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_TUDONGHOANUNG_KHITHANHTOANNOITRU", "0", false) == "0")
                {
                    NoitruTamung objTamung = new Select().From(NoitruTamung.Schema).Where(NoitruTamung.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(NoitruTamung.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .And(NoitruTamung.Columns.TrangThai).IsEqualTo(0)
                        .And(NoitruTamung.Columns.KieuTamung).IsEqualTo(1)
                        .And(NoitruTamung.Columns.Noitru).IsEqualTo(1)
                        .ExecuteSingle<NoitruTamung>();
                    if (objTamung != null)
                    {
                        Utility.ShowMsg("Bạn cần thực hiện thao tác hủy hoàn ứng tiền cho bệnh nhân trước khi thực hiện hủy thanh toán nội trú");
                        return ;
                    }
                }
                v_Payment_ID = Utility.Int32Dbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1);
                KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_Payment_ID);

                if (objPayment != null)
                {
                    //Kiểm tra ngày hủy
                    int SONGAY_HUYTHANHTOAN = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SONGAY_HUYTHANHTOAN", "0", true), 0);
                    int Chenhlech = (int)Math.Ceiling((globalVariables.SysDate.Date - objPayment.NgayThanhtoan.Date).TotalDays);
                    if (Chenhlech > SONGAY_HUYTHANHTOAN)
                    {
                        Utility.ShowMsg("Hệ thống không cho phép bạn hủy thanh toán đã quá ngày. Cần liên hệ quản trị hệ thống để được trợ giúp");
                        return;
                    }
                    if (PropertyLib._ThanhtoanProperties.Hienthihuythanhtoan)
                    {
                        frm_HuyThanhtoan frm = new frm_HuyThanhtoan("ALL");
                        frm.objLuotkham = objLuotkham;
                        frm.v_Payment_Id = Utility.Int32Dbnull(objPayment.IdThanhtoan, -1);
                        frm.Chuathanhtoan = Chuathanhtoan;
                        frm.TotalPayment = grdPayment.GetDataRows().Length;
                        frm.txtSoTienCanNop.Text = txtSoTienCanNop.Text;
                        frm.ShowCancel = true;
                        frm.ShowDialog();
                        if (!frm.m_blnCancel)
                        {
                            getData();
                        }
                    }
                    else
                    {
                        if (PropertyLib._ThanhtoanProperties.Hoitruockhihuythanhtoan)
                            if (!Utility.AcceptQuestion(string.Format("Bạn có muốn hủy lần thanh toán với Mã thanh toán {0}", objPayment.IdThanhtoan.ToString()), "Thông báo", true))
                            {
                                return;
                            }
                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_BATNHAPLYDO_HUYTHANHTOAN", "1", false) == "1")
                        {
                            frm_Chondanhmucdungchung _Nhaplydohuythanhtoan = new frm_Chondanhmucdungchung("LYDOHUYTHANHTOAN", "Hủy thanh toán tiền Bệnh nhân", "Nhập lý do hủy thanh toán trước khi thực hiện...", "Lý do hủy thanh toán");
                            _Nhaplydohuythanhtoan.ShowDialog();
                            if (_Nhaplydohuythanhtoan.m_blnCancel) return;
                            ma_lydohuy = _Nhaplydohuythanhtoan.ma;
                        }
                       int IdHdonLog = Utility.Int32Dbnull(grdPayment.CurrentRow.Cells[HoadonLog.Columns.IdHdonLog].Value, -1);
                       bool HUYTHANHTOAN_HUYBIENLAI = THU_VIEN_CHUNG.Laygiatrithamsohethong("HUYTHANHTOAN_HUYBIENLAI", "1", true) == "1";
                       ActionResult actionResult = _THANHTOAN.HuyThanhtoan(  objPayment , objLuotkham, ma_lydohuy, IdHdonLog, HUYTHANHTOAN_HUYBIENLAI);
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                getData();
                                THU_VIEN_CHUNG.Log(this.Name, globalVariables.UserName,
                                  string.Format("Hủy thanh toán nội trú của bệnh nhân có mã lần khám {0} và mã bệnh nhân là: {1} bởi {2}", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, globalVariables.UserName), action.Delete);
                                break;
                            case ActionResult.ExistedRecord:
                                break;
                            case ActionResult.Error:
                                Utility.ShowMsg("Lỗi trong quá trình hủy thông tin thanh toán", "Thông báo", MessageBoxIcon.Error);
                                break;
                            case ActionResult.UNKNOW:
                                Utility.ShowMsg("Lỗi không xác định", "Thông báo", MessageBoxIcon.Error);
                                break;
                            case ActionResult.Cancel:
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        ///     hàm thực hiện việc gọi phiếu thu
        /// </summary>
        private void CallPhieuThu()
        {
            if (grdPayment.CurrentRow != null)
            {
                v_Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
               

                KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_Payment_ID);
                if (objLuotkham == null)
                {
                    objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text), Utility.DoTrim(txtPatient_Code.Text));
                }
                if (objPayment != null)
                {
                    if (objLuotkham != null)
                    {
                        frm_HuyThanhtoan frm = new frm_HuyThanhtoan("ALL");
                        frm.objLuotkham = objLuotkham;
                        frm.v_Payment_Id = Utility.Int32Dbnull(objPayment.IdThanhtoan, -1);
                        frm.Chuathanhtoan = Chuathanhtoan;
                        frm.TotalPayment = grdPayment.GetDataRows().Length;
                        frm.txtSoTienCanNop.Text = txtSoTienCanNop.Text;
                        frm.ShowCancel = false;
                        frm.ShowDialog();
                    }
                }
            }
        }
       

        /// <summary>
        ///     hàm thực hiện việc gọi phiếu thu lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInphoiBHYT_Click(object sender, EventArgs e)
        {

            Utility.SetMsg(lblMessage, "", false);
            if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb.Value))
                return;
            if (objLuotkham == null)
            {
                Utility.SetMsg(lblMessage, "Bạn cần chọn Bệnh nhân cần thanh toán", true);
                return;
            }
            if (string.IsNullOrEmpty(txtICD.Text))
            {
                Utility.SetMsg(lblMessage, "Chưa có mã bệnh ICD. Quay lại phòng khám của bác sĩ để nhập", true);
                txtICD.Focus();
                return;
            }
            INPHIEU_CLICK = true;
            if (!THANHTOAN_BHYT_INPHIEU()) return;
            InPhoiBHYT();
        }
        /// <summary>
        /// Tự động thanh toán nếu bệnh nhân là BHYT và được hưởng 100%. Người dùng chi việc click vào nút In phôi
        /// </summary>
        private bool THANHTOAN_BHYT_INPHIEU()
        {
            try
            {
                //Nếu đối tượng dịch vụ thì không tự thanh toán
                if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb.Value))// Utility.sDbnull(objLuotkham.MaDoituongKcb) != "BHYT")
                    return false;
                List<GridEXRow> ChuaThanhToan = (from thanhtoan in grdThongTinChuaThanhToan.GetDataRows()
                    where Utility.Int32Dbnull(thanhtoan.Cells["trangthai_huy"].Value) == 0
                        && Utility.Int32Dbnull(thanhtoan.Cells["trangthai_thanhtoan"].Value) == 0
                    select thanhtoan).ToList();
               
                if (ChuaThanhToan.Count > 0 )
                {
                    //Nếu còn dịch vụ chưa thanh toán và là số tiền thanh toán là 0 đồng thì khi in phôi BHYT tự động thanh toán
                    if (Utility.DecimaltoDbnull(txtSoTienCanNop.Text, 0) == 0)
                        cmdThanhToan_Click(cmdThanhToan, new EventArgs());
                    else//Thông báo cần thanh toán hết trước khi in phôi BHYT
                    {
                        Utility.ShowMsg(string.Format("Hệ thống phát hiện còn một số dịch vụ chưa được thanh toán. Bạn cần thanh toán hết các dịch vụ của Bệnh nhân BHYT trước khi thực hiện in phôi BHYT"));
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Có lỗi trong quá trình tự động thanh toán cho đối tượng hưởng BHYT 100% khi nhấn nút In phôi BHYT");
                return false;
            }
        }

        private void cmdHuyThanhToan_Click(object sender, EventArgs e)
        {
            if (!Utility.Coquyen("quyen_huythanhtoan_tatca"))
            {
                Utility.ShowMsg("Bạn không được cấp quyền hủy thanh toán");
                return;
            }
            HuyThanhtoan();
        }

        private void grdPayment_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == KcbThanhtoan.Columns.NgayThanhtoan)
            {
                UpdatePaymentDate();
            }
        }

        /// <summary>
        ///     hàm thực hiện việc update thông tin ngày thanh toán
        /// </summary>
        private void UpdatePaymentDate()
        {
            if (
                Utility.AcceptQuestion(
                    "Bạn có muốn thay đổi thông tin chỉnh ngày thanh toán,Nếu bạn chỉnh ngày thanh toán, sẽ ảnh hưởng tới báo cáo",
                    "Thông báo", true))
            {
                v_Payment_ID = Utility.Int32Dbnull(grdPayment.CurrentRow.Cells["Id_thanhtoan"].Value, -1);
                KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_Payment_ID);
                if (objPayment != null)
                {
                    objPayment.NgayThanhtoan = Convert.ToDateTime(grdPayment.GetValue(KcbThanhtoan.Columns.NgayThanhtoan));
                    ActionResult actionResult = _THANHTOAN.UpdateNgayThanhtoan(objPayment);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.ShowMsg("Bạn chỉnh sửa ngày thanh toán thành công", "Thông báo");
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình thanh toán", "Thông báo lỗi", MessageBoxIcon.Error);
                            break;
                    }
                }
            }
        }

        private void UpdatePhieuChiPaymentDate()
        {
            if (
                Utility.AcceptQuestion(
                    "Bạn có muốn thay đổi thông tin chỉnh ngày thanh toán,Nếu bạn chỉnh ngày thanh toán, sẽ ảnh hưởng tới báo cáo",
                    "Thông báo", true))
            {
                v_Payment_ID = Utility.Int32Dbnull(grdPhieuChi.CurrentRow.Cells["Id_thanhtoan"].Value, -1);
                KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_Payment_ID);
                if (objPayment != null)
                {
                    objPayment.NgayThanhtoan = Convert.ToDateTime(grdPhieuChi.GetValue(KcbThanhtoan.Columns.NgayThanhtoan));
                    ActionResult actionResult = _THANHTOAN.UpdateNgayThanhtoan(objPayment);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.ShowMsg("Bạn chỉnh sửa ngày thanh toán thành công", "Thông báo");
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình thanh toán", "Thông báo lỗi", MessageBoxIcon.Error);
                            break;
                    }
                }
            }
        }

        /// <summary>
        ///     hàm thực hiện việc thay đổi ngày thanh toán
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPhieuChi_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == KcbThanhtoan.Columns.NgayThanhtoan)
            {
                //UpdatePaymentDate();
                UpdatePhieuChiPaymentDate();
            }
        }

        private void frm_THANHTOAN_NOITRU_old_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
            if (e.KeyCode == Keys.F3 ||(e.Control && e.KeyCode==Keys.F))
            {
                //if (txtMaLanKham.Focused)
                //{
                //    cmdSearch.PerformClick();
                //}
                //else
                //{
                //    txtMaLanKham.Focus();
                //    txtMaLanKham.Select();
                //}
                txtMaLanKham.Focus();
                txtMaLanKham.Select();
                cmdSearch.PerformClick();
            }
            //if (e.KeyCode == Keys.Enter)
            //{
            //    ProcessTabKey(true);
            //    return;
            //}
            if (e.KeyCode == Keys.F5) getData();
            if (e.KeyCode == Keys.F4) cmdInphoiBHYT.PerformClick();
            if (e.KeyCode == Keys.T && e.Control) cmdThanhToan.PerformClick();
            if (e.KeyCode == Keys.F1) tabThongTinCanThanhToan.SelectedIndex = 0;
            if (e.KeyCode == Keys.F2) tabThongTinCanThanhToan.SelectedIndex = 1;
            //if (e.KeyCode == Keys.F3) tabThongTinCanThanhToan.SelectedIndex = 2;
            if (e.KeyCode == Keys.F7) uiTabHoadon_chiphi.SelectedIndex = 0;
            if (e.KeyCode == Keys.F8) uiTabHoadon_chiphi.SelectedIndex = 1;
            if (e.KeyCode == Keys.D1 && e.Alt) tabThongTinThanhToan.SelectedTab = tabPagePayment;
            if (e.KeyCode == Keys.D2 && e.Alt) tabThongTinThanhToan.SelectedTab = tabPagePhieuChi;
        }

        private void cmdLaylaiThongTin_Click(object sender, EventArgs e)
        {
            getData();
        }

        /// <summary>
        ///     hàm thực hiện viecj  check trạng thái nút thanh toán
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdThongTinChuaThanhToan_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            try
            {
               // TinhToanSoTienPhaithu();
                //ModifyCommand();
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
        ///     hàm thực hiện viecj trạng thái của grid header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdThongTinChuaThanhToan_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                grdThongTinChuaThanhToan.RowCheckStateChanged -= grdThongTinChuaThanhToan_RowCheckStateChanged;
                //Thay hàm TinhToanSoTienPhaithu= hàm SetSumTotalProperties để tính lại tiền BHYT chi trả
                SetSumTotalProperties();
                //TinhToanSoTienPhaithu();
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
                grdThongTinChuaThanhToan.RowCheckStateChanged += grdThongTinChuaThanhToan_RowCheckStateChanged;
            }
        }

        private void grdPhieuChi_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "cmdPHIEUCHI")
                {
                    CallPhieuChi();
                }
                if (e.Column.Key == "cmdHuyPhieuChi")
                {
                    CallHuyPhieuChi();
                }
            }
            catch (Exception exception)
            {
                //throw;
            }
        }

        /// <summary>
        ///     hàm thực hiện việc hủy thông tin phiếu chi
        /// </summary>
        private void CallHuyPhieuChi()
        {
            if (Utility.AcceptQuestion("Bạn có muốn hủy thông tin phiếu chi đang chọn không ?", "Thông báo", true))
            {
                int Payment_ID = Utility.Int32Dbnull(grdPhieuChi.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
                KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(Payment_ID);
                if (objPayment != null)
                {
                    ActionResult actionResult = _THANHTOAN.HuyPhieuchi(objPayment,objLuotkham,"");
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            grdPhieuChi.CurrentRow.Delete();
                            ModifyCommand();
                            Utility.ShowMsg("Bạn hủy thông tin phiếu chi thành công", "Thông báo");
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình hủy phiếu chi", "Thông báo lỗi", MessageBoxIcon.Error);
                            break;
                    }
                }
                ModifyCommand();
            }
        }

        /// <summary>
        ///     hàm thực hiện viecj lấy thông tin phiếu chi
        /// </summary>
        private void CallPhieuChi()
        {
            try
            {
                frm_Tralaitien frm = new frm_Tralaitien();
                frm.objLuotkham = objLuotkham;
                frm.v_Payment_Id = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdPhieuChi, KcbThanhtoan.Columns.IdThanhtoan), -1);
                frm.Chuathanhtoan = Chuathanhtoan;
                frm.TotalPayment = grdPayment.GetDataRows().Length;
                frm.ShowCancel = false;
                frm.ShowDialog();
            }
            catch (Exception exception)
            {
                //throw;
            }
        }

        /// <summary>
        ///     HÀM THỰC HIỆN VIỆC THAY ĐỔI THÔNG TIN PHIẾU CẬN LÂM SÀNG
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdThongTinDaThanhToan_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == KcbThanhtoanChitiet.Columns.TrangthaiHuy)
            {
            }
        }

        private void grdThongTinDaThanhToan_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            ModifyCommand();
        }

        private void grdThongTinDaThanhToan_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
            ModifyCommand();
        }

        /// <summary>
        ///     hàm thực hiện việc lấy thông tin chi phí đã thanh toán
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdLayThongTinDaThanhToan_Click(object sender, EventArgs e)
        {
            GetChiPhiDaThanhToan();
        }

        private void txtYear_Of_Birth_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    txtTuoi.Text = Utility.sDbnull(globalVariables.SysDate.Year - Utility.Int32Dbnull(txtYear_Of_Birth.Text));
            //}
            //catch (Exception)
            //{
            //    //throw;
            //}
        }

        private void grdList_FormattingRow(object sender, RowLoadEventArgs e)
        {
        }

        /// <summary>
        ///     hàm thực hiện việc dổi trạng thái của chưa thanh toán
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdThongTinChuaThanhToan_GroupsChanged(object sender, GroupsChangedEventArgs e)
        {
            ModifyCommand();
        }

        private void grdThongTinChuaThanhToan_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            if (e.Column.Key == "tile_chietkhau" || e.Column.Key == "tien_chietkhau")
            {
                if (Utility.isValidGrid(grdThongTinChuaThanhToan) && Utility.Int64Dbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["trangthai_thanhtoan"].Value, 1) == 1)
                {
                    Utility.ShowMsg("Chi tiết bạn chọn đã được thanh toán nên bạn không thể chiết khấu được nữa. Mời bạn kiểm tra lại");
                    //e.Cancel = true;
                    return;
                }
                else
                {
                    if (e.Column.Key == "tile_chietkhau")
                    {
                        //Tính lại tiền chiết khấu theo tỉ lệ %
                        if (Utility.DecimaltoDbnull(e.Value, 0) > 100)
                        {
                            Utility.ShowMsg("Tỉ lệ chiết khấu không được phép vượt quá 100 %. Mời bạn kiểm tra lại");
                            e.Cancel = true;
                            return;
                        }
                        grdThongTinChuaThanhToan.CurrentRow.Cells["tien_chietkhau"].Value = Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["TT_BN"].Value, 0) * Utility.DecimaltoDbnull(e.Value, 0) / 100;

                    }
                    else
                    {

                        if (Utility.DecimaltoDbnull(e.Value, 0) > Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["TT_BN"].Value, 0))
                        {
                            Utility.ShowMsg("Tiền chiết khấu không được lớn hơn(>) tiền Bệnh nhân chi trả("+ Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["TT_BN"].Value, 0).ToString()+"). Mời bạn kiểm tra lại");
                            e.Cancel = true;
                            return;
                        }
                        grdThongTinChuaThanhToan.CurrentRow.Cells["tile_chietkhau"].Value = (Utility.DecimaltoDbnull(e.Value, 0) / Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["TT_BN"].Value, 0)) * 100;
                    }
                }
            }
            ModifyCommand();
        }

        private void grdList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                getData();
            }
            catch (Exception)
            {
                //throw;
            }
            //grdList_ColumnButtonClick(grdList,col);
        }

        /// <summary>
        ///     hàm thực hiện việc format thông tin số tiền cần nợp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdThongTinChuaThanhToan_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            //Thay hàm TinhToanSoTienPhaithu= hàm SetSumTotalProperties để tính lại tiền BHYT chi trả
            SetSumTotalProperties();
            //TinhToanSoTienPhaithu();
            e.Column.InputMask = "{0:#,#.##}";
        }

        private void txtMaLanKham_LostFocus(object sender, EventArgs eventArgs)
        {
            try
            {
                Maluotkham = Utility.sDbnull(txtMaLanKham.Text.Trim());
                if (!string.IsNullOrEmpty(Maluotkham) && txtMaLanKham.Text.Length < 8)
                {
                    Maluotkham = Utility.GetYY(globalVariables.SysDate) +
                               Utility.FormatNumberToString(Utility.Int32Dbnull(txtMaLanKham.Text, 0), "000000");
                    txtMaLanKham.Text = Maluotkham;
                }
            }
            catch (Exception)
            {
                // throw;
            }
        }

        private void txtMaLanKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Maluotkham = Utility.sDbnull(txtMaLanKham.Text.Trim());
                if (!string.IsNullOrEmpty(Maluotkham) && txtMaLanKham.Text.Length < 8)
                {
                    Maluotkham = Utility.AutoFullPatientCode(txtMaLanKham.Text);
                    txtMaLanKham.Text = Maluotkham;
                    txtMaLanKham.Select(txtMaLanKham.Text.Length, txtMaLanKham.Text.Length);
                }
                if (!string.IsNullOrEmpty(txtMaLanKham.Text))
                {
                    bool byDate = chkCreateDate.Checked;
                    chkCreateDate.Checked = false;
                    cmdSearch_Click(cmdSearch, new EventArgs());
                    chkCreateDate.Checked = byDate;
                    if (grdList.RowCount == 1)
                    {
                        grdList.MoveFirst();
                        grdList_DoubleClick(grdList, new EventArgs());
                    }
                    //cmdSearch.Focus();
                }
            }
        }

        /// <summary>
        ///     hàm thực hiện việc hiển thị tên bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTenBenhNhan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdSearch_Click(cmdSearch, new EventArgs());
                if (grdList.RowCount == 1)
                {
                    grdList.MoveFirst();
                    grdList_DoubleClick(grdList, new EventArgs());
                }
            }
        }

        /// <summary>
        ///     hàm thực hiện sửa số biên lai
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSuaSoBienLai_Click(object sender, EventArgs e)
        {
            SuaSoBienLai();
        }

        /// <summary>
        ///     hàm thực hiện việc sửa số biên lai của viện
        /// </summary>
        private void SuaSoBienLai()
        {
            try
            {
                v_Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
                var tHoadonLog =
                    new Select().From(HoadonLog.Schema).Where(HoadonLog.Columns.IdThanhtoan).IsEqualTo(v_Payment_ID).
                        And(HoadonLog.Columns.TrangThai).IsEqualTo(0).
                        ExecuteSingle<HoadonLog>();
                if (tHoadonLog != null)
                {
                    var frm = new frm_SUA_SOBIENLAI();
                    frm._HoadonLog = tHoadonLog;
                    frm.ShowDialog();
                    if (!frm.m_blnCancel)
                    {
                        grdPayment.CurrentRow.BeginEdit();
                        grdPayment.CurrentRow.Cells[HoadonLog.Columns.MauHoadon].Value = frm._HoadonLog.MauHoadon;
                        grdPayment.CurrentRow.Cells[HoadonLog.Columns.KiHieu].Value = frm._HoadonLog.KiHieu;
                        grdPayment.CurrentRow.Cells[HoadonLog.Columns.MaQuyen].Value = frm._HoadonLog.MaQuyen;
                        grdPayment.CurrentRow.Cells[HoadonLog.Columns.Serie].Value = frm._HoadonLog.Serie;
                        grdPayment.CurrentRow.EndEdit();
                    }
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình sửa số biên lai");
            }
        }

        private void mnuInLaiBienLai_Click(object sender, EventArgs e)
        {
            try
            {
                v_Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
                DataTable dtPatientPayment = _THANHTOAN.Laythongtinhoadondo(v_Payment_ID);
                 string tieude="", reportname = "";
                ReportDocument report = Utility.GetReport("thanhtoan_RedInvoice",ref tieude,ref reportname);
                if (report == null) return;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    report.PrintOptions.PrinterName = Utility.sDbnull(printDialog1.PrinterSettings.PrinterName);
                    report.SetDataSource(dtPatientPayment);
                    report.PrintToPrinter(0, true, 0, 0);
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình in phiếu");
                throw;
            }
        }

        private void mnuHuyHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                int IdHdonLog = -1;
                if (!Utility.isValidGrid(grdPayment)) return;

                if (
                       !Utility.AcceptQuestion("Bạn có chắc chắn muốn hủy hóa đơn này không. Chú ý: Hủy hóa đơn đồng nghĩa với việc bạn hủy thanh toán?", "Xác nhận hủy hóa đơn", true))
                    return;
                IdHdonLog = Utility.Int32Dbnull(grdPayment.CurrentRow.Cells[HoadonLog.Columns.IdHdonLog].Value, -1);
                if (IdHdonLog > 0)
                {
                    int v_Payment_Id = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
                    DataTable dtKTra = _THANHTOAN.KiemtraTrangthaidonthuocTruockhihuythanhtoan(v_Payment_Id);
                    if (dtKTra.Rows.Count > 0)
                    {
                        Utility.ShowMsg("Đã có thuốc được duyệt cấp phát. Bạn không thể hủy thanh toán.");
                        return;
                    }
                    bool HUYHOADON_XOABIENLAI = THU_VIEN_CHUNG.Laygiatrithamsohethong("HUYHOADON_XOABIENLAI", "0", true) == "1";
                    ActionResult actionResult = _THANHTOAN.HuyThongTinLanThanhToan(KcbThanhtoan.FetchByID(v_Payment_Id), objLuotkham, "", IdHdonLog, HUYHOADON_XOABIENLAI);
                    //nếu hủy hóa đơn và hủy lần thanh toán thành công thì thông báo
                    if (actionResult == ActionResult.Success)
                    {
                        HuyThanhtoan();
                        Utility.ShowMsg("Đã hủy hóa đơn thành công");
                        LaydanhsachLichsuthanhtoan_phieuchi();
                        getData();
                    }
                    else if (actionResult == ActionResult.Error) // hủy lần thanh toán bị lỗi
                    {
                        Utility.ShowMsg("Có lỗi trong quá trình hủy thanh toán");
                    }
                    else // Hủy hóa đơn và thanh toán bị lỗi
                    {
                        Utility.ShowMsg("Có lỗi trong quá trình hủy hóa đơn thanh toán.");
                    }
                }

            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình hủy hóa đơn");
                throw;
            }
        }

      

     
       

        private void IN_HOADON()
        {
            string LyDoIn = "0";
            try
            {
                if (!Utility.isValidGrid(grdPayment))
                    return;
                int payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
                
                try
                {
                    dtPatientPayment = _THANHTOAN.Laythongtinhoadondo(payment_ID);
                    dtPatientPayment.Rows[0]["sotien_bangchu"] =
                        new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(dtPatientPayment.Rows[0]["TONG_TIEN"]));
                    int lengh = txtSerieDau.Text.Length;
                    string tieude="", reportname = "";
                    ReportDocument report = Utility.GetReport("thanhtoan_Hoadondo",ref tieude,ref reportname);
                    if (report == null) return;
                    frmPrintPreview objForm = new frmPrintPreview("", report, true, true);
                    objForm.mv_sReportFileName = Path.GetFileName(reportname);
                    objForm.mv_sReportCode = "thanhtoan_Hoadondo";
                    report.PrintOptions.PrinterName = cbomayinhoadon.Text;
                    report.SetDataSource(dtPatientPayment);
                    report.SetParameterValue("NGUOIIN", Utility.sDbnull(globalVariables.gv_strTenNhanvien, ""));

                    report.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
                    report.SetParameterValue("BranchName", globalVariables.Branch_Name);
                    report.SetParameterValue("DateTime", Utility.FormatDateTime(globalVariables.SysDate));
                    report.SetParameterValue("CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                    report.SetParameterValue("sTitleReport", tieude);
                    //report.SetParameterValue("CharacterMoney", new MoneyByLetter().sMoneyToLetter(TONG_TIEN.ToString()));
                    objForm.crptViewer.ReportSource = report;

                    if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInHoadon, PropertyLib._MayInProperties.PreviewInHoadon))
                    {
                        objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInHoadon, 1);
                        objForm.ShowDialog();
                    }
                    else
                    {
                        report.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInHoadon;
                        report.PrintToPrinter(1,false, 0, 0);
                    }
                }
                catch (Exception ex1)
                {
                    Utility.ShowMsg("Lỗi khi thực hiện in hóa đơn mẫu. Liên hệ IT để được trợ giúp-->" +
                                    ex1.Message);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }


        private void LoadPrinter()
        {
            try
            {
                //foreach (string printer in PrinterSettings.InstalledPrinters)
                //{
                //    cboPrinter.Items.Add(printer, printer);
                //}
                //if (cboPrinter.Items.Count > 0) cboPrinter.SelectedIndex = 0;

                //if (File.Exists(sFileName))
                //{
                //    string[] printerName = File.ReadAllLines(sFileName);
                //    if (printerName.Length > 0)
                //        cboPrinter.SelectedValue = printerName[0];
                //}
            }
            catch
            {
            }
        }

        private void SavePrinterConfig()
        {
            try
            {
               // File.WriteAllLines(sFileName, new[] {Utility.sDbnull(cboPrinter.SelectedValue)});
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private bool ValidDataHoaDon()
        {
            try
            {
                if (txtSerieDau.Text.Length != txtSerie.Text.Length)
                {
                    Utility.ShowMsg("Số ký tự serie không đúng");
                    return false;
                }

                if (Utility.Int32Dbnull(txtSerieDau.Text) > Utility.Int32Dbnull(txtSerie.Text) ||
                    Utility.Int32Dbnull(txtSerie.Text) > Utility.Int32Dbnull(txtSerieCuoi.Text))
                {
                    Utility.ShowMsg(string.Format("Số ký tự serie không trong khoảng cho phép ({0} - {1})",
                        Utility.sDbnull(dtCapPhat.Rows[0]["SERIE_DAU"]),
                        Utility.sDbnull(dtCapPhat.Rows[0]["SERIE_CUOI"])));
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
        void ModifyContextMenu()
        {
            try
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false)!="1") return;
                int IdHdonLog = Utility.Int32Dbnull(grdPayment.GetValue(HoadonLog.Columns.IdHdonLog), -1);
                int TrangthaiChot = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.TrangthaiChot), 0);
                mnuSuaSoBienLai.Visible = IdHdonLog > 0;
                mnuHuyHoaDon.Visible = IdHdonLog > 0 && TrangthaiChot == 0;
                mnuInLaiBienLai.Visible = IdHdonLog > 0;
                if (TrangthaiChot == 0)
                    if (IdHdonLog > 0)
                    {
                        mnuLayhoadondo.Text = "Bỏ hóa đơn đỏ cho thanh toán đang chọn";
                        mnuLayhoadondo.Tag = 0;
                    }
                    else
                    {
                        mnuLayhoadondo.Text = "Lấy hóa đơn đỏ cho thanh toán đang chọn";
                        mnuLayhoadondo.Tag = 1;
                    }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi tùy biến menu biên lai", ex);
            }
        }
        private void grdPayment_SelectionChanged(object sender, EventArgs e)
        {
            ModifyContextMenu();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
           
        }

        private void txtSerie_Leave(object sender, EventArgs e)
        {
            try
            {
                txtSerie.Text = txtSerie.Text.PadLeft(Utility.sDbnull(txtSerieDau.Text).Length, '0');
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình định dạng dữ liệu");
                throw;
            }
        }

        private void txtSerie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    txtSerie.Text = txtSerie.Text.PadLeft(Utility.sDbnull(txtSerieDau.Text).Length, '0');
                }
                catch (Exception)
                {
                    Utility.ShowMsg("Có lỗi trong quá trình định dạng dữ liệu");
                    throw;
                }
            }
        }

        private void grdDSKCB_RowCheckStateChanged(object sender, RowCheckStateChangeEventArgs e)
        {
            try
            {
                foreach (GridEXRow exRow in grdThongTinChuaThanhToan.GetDataRows())
                {
                    if (Utility.Int32Dbnull(exRow.Cells[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].Value, -1) ==
                        Utility.Int32Dbnull(grdDSKCB.GetValue("MA")))
                    {
                        if (Utility.Int32Dbnull(exRow.Cells["trangthai_huy"].Value, 0) == 0 &&
                            //Utility.Int32Dbnull(exRow.Cells["trang_thai"].Value, 0) == 0 && 
                                Utility.Int32Dbnull(exRow.Cells["trangthai_thanhtoan"].Value, 0) == 0)
                        {
                            exRow.CheckState = e.CheckState;
                        }
                        else
                        {
                            exRow.IsChecked = false;
                        }
                    }
                }
                //Thay hàm TinhToanSoTienPhaithu= hàm SetSumTotalProperties để tính lại tiền BHYT chi trả
                SetSumTotalProperties();
                //TinhToanSoTienPhaithu();
                ModifyCommand();
            }
            catch
            {
            }
        }

        private void grdDSKCB_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                grdDSKCB.RowCheckStateChanged -= grdDSKCB_RowCheckStateChanged;
                foreach (GridEXRow row in grdDSKCB.GetDataRows())
                {
                    foreach (GridEXRow exRow in grdThongTinChuaThanhToan.GetDataRows())
                    {
                        if (Utility.Int32Dbnull(exRow.Cells[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].Value, -1) ==
                            Utility.Int32Dbnull(row.Cells["MA"].Value))
                        {
                            if (Utility.Int32Dbnull(exRow.Cells["trangthai_huy"].Value, 0) == 0 &&
                                //Utility.Int32Dbnull(exRow.Cells["trang_thai"].Value, 0) == 0 &&
                                Utility.Int32Dbnull(exRow.Cells["trangthai_thanhtoan"].Value, 0) == 0)
                            {
                                exRow.CheckState = row.CheckState;
                            }
                            else
                            {
                                exRow.IsChecked = false;
                            }
                        }
                    }
                }
                //Thay hàm TinhToanSoTienPhaithu= hàm SetSumTotalProperties để tính lại tiền BHYT chi trả
                SetSumTotalProperties();
                //TinhToanSoTienPhaithu();
                ModifyCommand();
                grdDSKCB.RowCheckStateChanged += grdDSKCB_RowCheckStateChanged;
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình xử lý thông tin");
            }
        }

        private void InPhoiBHYT()
        {
            try
            {
                if (new INPHIEU_THANHTOAN_NOITRU().InPhoiBHYT(objLuotkham, m_dtPayment, dtPaymentDate.Value))
                {
                    cbomayinphoiBHYT.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                    LaydanhsachLichsuthanhtoan_phieuchi();
                    KiemTraDaInPhoiBhyt();
                }

            }
            catch(Exception exception)
            {
                Utility.ShowMsg("Lỗi:"+ exception.Message);
            }
        }

        private KcbPhieuDct CreatePhieuDongChiTra()
        {
                KcbPhieuDct objPhieuDct = new KcbPhieuDct();
                objPhieuDct.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                objPhieuDct.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                objPhieuDct.LoaiThanhtoan = 0;
                objPhieuDct.NguoiTao = globalVariables.UserName;
                objPhieuDct.NgayTao = globalVariables.SysDate;
                objPhieuDct.TongTien = (decimal)m_dtPayment.Compute("SUM(TONGTIEN_GOC)", "1=1");// Utility.DecimaltoDbnull(txtSoTienGoc.Text);
                objPhieuDct.BnhanChitra = (decimal)m_dtPayment.Compute("SUM(BN_CT)", "1=1"); //Utility.DecimaltoDbnull(txtTienBNCT.Text);
                objPhieuDct.BhytChitra = (decimal)m_dtPayment.Compute("SUM(BHYT_CT)", "1=1"); //Utility.DecimaltoDbnull(txtTienBHCT.Text);
                return objPhieuDct;
            
        }

        private void cmdCapnhatngayinphoiBHYT_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.EnableButton(cmdCapnhatngayinphoiBHYT, false);
                int record = -1;
                record =
                    new Update(KcbPhieuDct.Schema).Set(KcbPhieuDct.Columns.NgayTao).EqualTo(dtNgayInPhoi.Value)
                        .Where(KcbPhieuDct.Columns.MaLuotkham).IsEqualTo(txtPatient_Code.Text)
                        .And(KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtPatient_ID.Text))
                        .And(KcbPhieuDct.Columns.LoaiThanhtoan).IsEqualTo(Utility.Int32Dbnull(KieuThanhToan.NgoaiTru))
                        .Execute();
                if (record > 0)
                {
                    Utility.ShowMsg("Đã cập nhật thông tin thành công.");
                }
                else
                {
                    Utility.ShowMsg("Cập nhật thông tin không thành công.");
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Có lỗi trong quá trình chỉnh sửa dữ liệu ngày in phiếu Đồng Chi Trả \n" + ex);
            }
            finally
            {
                Utility.EnableButton(cmdCapnhatngayinphoiBHYT, true);
            }
        }

       

        /// <summary>
        ///     hàm thực hiện việc cấu hình
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._ThanhtoanProperties);
            frm.ShowDialog();
            CauHinh();
        }

       

        private void cmdChuyenDT_Click(object sender, EventArgs e)
        {
            try
            {
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Bạn cần chọn một bệnh nhân trước khi thực hiện tính năng chuyển đối tượng", "Thông báo");
                    return;
                }
                if (grdThongTinDaThanhToan.GetDataRows().Length > 0)
                {
                    Utility.ShowMsg(string.Format("Bệnh nhân {0} đã được thanh toán nên không thể chuyển đối tượng\n.Bạn cần liên hệ với nhân viên quầy thanh toán để hủy thanh toán nếu muốn sử dụng chức năng này", txtTenBenhNhan.Text), "Thông báo");
                    return;
                }
                frm_chuyendoituongkcb frm = new frm_chuyendoituongkcb();
                frm.objLuotkham = objLuotkham;
                frm.ShowDialog();
                if (frm.m_blnSuccess)
                {
                    RefreshData(frm.objLuotkhamMoi, frm.lblClinicName.Text);
                    UpdateGroup();
                    getData();
                   
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình chuyển đối tượng");
            }
        }
        void RefreshData(KcbLuotkham objNew, string ten_kcbbd)
        {
            try
            {
                DataRow[] arrDr = m_dtDataTimKiem.Select(KcbLuotkham.Columns.IdBenhnhan + "=" + objNew.IdBenhnhan.ToString() + " AND " + KcbLuotkham.Columns.MaLuotkham + "='" + objNew.MaLuotkham + "'");
                foreach (DataRow dr in arrDr)
                {
                    dr[KcbLuotkham.Columns.MatheBhyt] = objNew.MatheBhyt;
                    dr[KcbLuotkham.Columns.NgaybatdauBhyt] =Utility.null2DBNull( objNew.NgaybatdauBhyt,DBNull.Value);
                    dr[KcbLuotkham.Columns.NgayketthucBhyt] = Utility.null2DBNull( objNew.NgayketthucBhyt,DBNull.Value);
                    dr[KcbLuotkham.Columns.DiachiBhyt] = objNew.DiachiBhyt;
                    dr[KcbLuotkham.Columns.MaDoituongKcb] = objNew.MaDoituongKcb;
                    dr[KcbLuotkham.Columns.PtramBhyt] = objNew.PtramBhyt;
                    dr[KcbLuotkham.Columns.DungTuyen] = objNew.DungTuyen;
                    dr[KcbLuotkham.Columns.MaKcbbd] = objNew.MaKcbbd;
                    dr["ten_kcbbd"] = objNew.IdLoaidoituongKcb == 1 ? "" : ten_kcbbd;
                }
                m_dtDataTimKiem.AcceptChanges();
            }
            catch
            {
            }
        }
        private void txtMaLanKham_TextChanged(object sender, EventArgs e)
        {
            Maluotkham = Utility.sDbnull(txtMaLanKham.Text);
        }
       
        private void cmdInhoadon_Click(object sender, EventArgs e)
        {
            //Tạm rem đoạn xem phiếu
            //CallPhieuThu();
            if (!Utility.isValidGrid(grdPayment))
            {
                Utility.ShowMsg("Bạn phải chọn ít nhất một phiếu thanh toán để in hóa đơn trong lưới bên dưới","thông báo");
                return;
            }
            InHoadon();
            
        }
      
        void InHoadon()
        {
            try
            {
                int _Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
                decimal TONG_TIEN = Utility.Int32Dbnull(grdPayment.CurrentRow.Cells["BN_CT"].Value, -1);
                ActionResult actionResult = new KCB_THANHTOAN().Capnhattrangthaithanhtoan(_Payment_ID);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        new INPHIEU_THANHTOAN_NGOAITRU().IN_HOADON(_Payment_ID);
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình in hóa đơn", "Thông báo lỗi", MessageBoxIcon.Warning);
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi trong quá trình in hóa đơn\n" + ex.Message, "Thông báo lỗi");
            }
        }
       
       
       
        private void InPhieuDCT()
        {
            new INPHIEU_THANHTOAN_NGOAITRU().InphieuDCT_Benhnhan(objLuotkham, m_dtPayment);
            cbomayinphoiBHYT.Text = PropertyLib._MayInProperties.TenMayInBienlai;
        }
       
     
        private void InPhieuDichVu(string mau, decimal TONG_TIEN)
        {
            //switch (mau)
            //{
                //case "mau1":
                //    PrintPhieuThu_YHHQ("PHIẾU THU", TONG_TIEN);
                //    break;
                //case "mau2":
                    PrintPhieuThu_DM("PHIẾU THU", TONG_TIEN);
            //        break;
            //}
        }
        /// <summary>
        /// Tạm thời ko dùng do report ko có tables
        /// </summary>
        /// <param name="sTitleReport"></param>
        /// <param name="tongTien"></param>
        private void PrintPhieuThu_YHHQ(string sTitleReport, decimal tongTien)
        {
            var crpt = new ReportDocument();
            Utility.WaitNow(this);
             string tieude="", reportname = "";
            crpt = Utility.GetReport("thanhtoan_PhieuThu",ref tieude,ref reportname);
            if (crpt == null) return;
           
            var objForm = new frmPrintPreview("", crpt, true, true);
            //try
            //{
            crpt.SetDataSource(m_dtReportPhieuThu);
            // //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên                                                                   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
            Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
            Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
            Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(globalVariables.SysDate));
            Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
            Utility.SetParameterValue(crpt,"sTitleReport", sTitleReport);
            Utility.SetParameterValue(crpt,"CharacterMoney", new MoneyByLetter().sMoneyToLetter(tongTien.ToString()));
            DataRow dataRow = m_dtReportPhieuThu.Rows[0];
            //if (status != 0)
            //Utility.SetParameterValue(crpt,"NguoiChi", BusinessHelper.GetStaffByUserName(dataRow["NGUOI_NOP"].ToString()));
            objForm.crptViewer.ReportSource = crpt;
            objForm.ShowDialog();
            Utility.DefaultNow(this);
            //}
            //catch (Exception ex)
            //{
            //    Utility.DefaultNow(this);
            //}
        }
        /// <summary>
        /// hàm thực hiện việc in phieus thu của dệt may
        /// </summary>
        /// <param name="sTitleReport"></param>
        private void PrintPhieuThu_DM(string sTitleReport,decimal TONG_TIEN)
        {
            Utility.WaitNow(this);
             string tieude="", reportname = "";
            var crpt = Utility.GetReport("thanhtoan_PhieuThu_DM",ref tieude,ref reportname);
            if (crpt == null) return;
            var objForm = new frmPrintPreview("", crpt, true, true);
            try
            {
            crpt.SetDataSource(m_dtReportPhieuThu);
            // //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên                                                                   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
            Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
            Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
            Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(globalVariables.SysDate));
            Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
            Utility.SetParameterValue(crpt,"sTitleReport", sTitleReport);
            Utility.SetParameterValue(crpt,"CharacterMoney", new MoneyByLetter().sMoneyToLetter(TONG_TIEN.ToString()));
            objForm.crptViewer.ReportSource = crpt;
            objForm.ShowDialog();
            Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.DefaultNow(this);
            }
        }
        private void cmdInphoiBHYT_Click_1(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     hàm thực hiện việc hủy thông tin của phôi bảo hiểm hủy in phôi bảo hiểm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdHuyInPhoiBHYT_Click(object sender, EventArgs e)
        {
            if (
                Utility.AcceptQuestion(
                    "Bạn có chắc sẽ hủy phôi bảo hiểm y tế này không,\n Nếu bạn hủy phôi sẽ hủy kết thúc bệnh nhân",
                    "Thông báo", true))
            {
                if (objLuotkham == null)
                {
                    objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text), Utility.DoTrim(txtPatient_Code.Text));
                }
                if (objLuotkham != null)
                {
                    ActionResult actionResult = _THANHTOAN.UpdateHuyInPhoiBHYT(objLuotkham,
                        KieuThanhToan.NgoaiTru);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            KiemTraDaInPhoiBhyt();
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình hủy in phôi bảo hiểm", "Thông báo",
                                MessageBoxIcon.Error);
                            break;
                    }
                }
            }
        }

        /// <summary>
        ///     hàm thực hiện việc danh sách in phôi bảo hiểm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDanhSachPhoiBHYT_Click(object sender, EventArgs e)
        {
            var frm = new FrmDanhsachBenhnhanInphoiBhyt();
            frm.ShowDialog();
        }

        private void cmdInphoiBHYT_Click_2(object sender, EventArgs e)
        {
        }

        private void getThongtincanhbao(int patientID)
        {
            try
            {
                Utility.SetMsg(lblwarningMsg, "", false);

                cmdsave.Enabled = true;
                var lst =
                    new Select().From(DmucCanhbao.Schema)
                        .Where(DmucCanhbao.MaBnColumn)
                        .IsEqualTo(patientID)
                        .ExecuteAsCollection<DmucCanhbaoCollection>();
                if (lst.Count > 0) //Delete
                {
                    txtCanhbao.Text = lst[0].CanhBao;
                }
                else
                    txtCanhbao.Clear();
                cmdxoa.Enabled = lst.Count > 0;
            }
            catch
            {
            }
        }

        private void cmdsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.Int32Dbnull(txtPatient_ID.Text, -1) <= 0) return;
                var lst =
                    new Select().From(DmucCanhbao.Schema)
                        .Where(DmucCanhbao.MaBnColumn)
                        .IsEqualTo(txtPatient_ID.Text)
                        .ExecuteAsCollection<DmucCanhbaoCollection>();
                if (lst.Count > 0) //Update or Delete
                {
                    if (txtCanhbao.Text.TrimStart().TrimEnd() == "")
                    {
                        new Delete().From(DmucCanhbao.Schema)
                            .Where(DmucCanhbao.Columns.MaBn)
                            .IsEqualTo(txtPatient_ID.Text)
                            .Execute();
                        cmdxoa.Enabled = false;
                    }
                    else
                    {
                        new Update(DmucCanhbao.Schema).Set(DmucCanhbao.CanhBaoColumn)
                            .EqualTo(txtCanhbao.Text.TrimStart().TrimEnd())
                            .Set(DmucCanhbao.NgaySuaColumn).EqualTo(globalVariables.SysDate)
                            .Set(DmucCanhbao.NguoiSuaColumn).EqualTo(globalVariables.UserName)
                            .Where(DmucCanhbao.Columns.MaBn)
                            .IsEqualTo(txtPatient_ID.Text)
                            .Execute();
                    }
                    Utility.SetMsg(lblwarningMsg, "Đã cập nhật thông tin cảnh báo thành công!", false);
                }
                else //Insert
                {
                    if (txtCanhbao.Text.TrimStart().TrimEnd() == "")
                    {
                        Utility.SetMsg(lblwarningMsg, "Bạn cần nhập thông tin cảnh báo!", true);
                        txtCanhbao.Focus();
                        return;
                    }
                    var _newItem = new DmucCanhbao();
                    _newItem.CanhBao = txtCanhbao.Text.TrimStart().TrimEnd();
                    _newItem.MaBn = Utility.Int32Dbnull(txtPatient_ID.Text, -1);
                    _newItem.NgayTao = globalVariables.SysDate.Date;
                    _newItem.NguoiTao = globalVariables.UserName;
                    _newItem.IsNew = true;
                    _newItem.Save();
                    Utility.SetMsg(lblwarningMsg, "Đã lưu thông tin cảnh báo thành công!", false);
                    cmdxoa.Enabled = true;
                }
            }
            catch
            {
            }
        }

        private void cmdxoa_Click(object sender, EventArgs e)
        {
            try
            {
                var lst =
                    new Select().From(DmucCanhbao.Schema)
                        .Where(DmucCanhbao.MaBnColumn)
                        .IsEqualTo(txtPatient_ID.Text)
                        .ExecuteAsCollection<DmucCanhbaoCollection>();
                if (lst.Count > 0) //Delete
                {
                    new Delete().From(DmucCanhbao.Schema)
                        .Where(DmucCanhbao.Columns.MaBn)
                        .IsEqualTo(txtPatient_ID.Text)
                        .Execute();
                    Utility.SetMsg(lblwarningMsg, "Đã xóa thông tin cảnh báo thành công!", false);
                    cmdxoa.Enabled = false;
                    txtCanhbao.Clear();
                }
            }
            catch
            {
            }
        }

       
        

        /// <summary>
        ///     hàmh tực hiện việc dóng form lưu lại thông tin cấu hình
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_THANHTOAN_NOITRU_old_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void cmdCapnhatngayBHYT_Click(object sender, EventArgs e)
        {
            SqlQuery sqlQuery = new Select().From<KcbLuotkham>()
                .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtPatient_ID.Text))
                .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtPatient_Code.Text)
                .And(KcbLuotkham.Columns.MaDoituongKcb).IsEqualTo("BHYT");
            if (sqlQuery.GetRecordCount() <= 0)
            {
                Utility.ShowMsg(
                    "Bệnh nhân không phải là bảo hiểm, Không thể chính sửa được ngày hết hạn thẻ bảo hiểm\n Mời bạn xem lại",
                    "Thông báo", MessageBoxIcon.Warning);

                return;
            }
            if (dtpBHYTFfromDate.Value > dtpBHYTToDate.Value)
            {
                Utility.ShowMsg("Ngày hết hạn nhỏ hơn ngày bắt đầu của thẻ BHYT\n Mời bạn xem lại", "Thông báo",
                    MessageBoxIcon.Warning);
                dtpBHYTFfromDate.Focus();
                return;
            }
            if (dtpBHYTToDate.Value < globalVariables.SysDate)
            {
                Utility.ShowMsg("Ngày hết hạn đã hết hạn, nhỏ hơn ngày hiện tại \n Mời bạn xem lại", "Thông báo",
                    MessageBoxIcon.Warning);
                dtpBHYTToDate.Focus();
                return;
            }
            if (Utility.AcceptQuestion("Bạn có muốn update thông tin ngày thẻ bảo hiểm y tế không", "Thông báo", true))
            {
                new Update(KcbLuotkham.Schema)
                    //.Set(KcbLuotkham.Columns.IpMacSua).EqualTo(globalVariables.IpMacAddress)
                    //.Set(KcbLuotkham.Columns.IpMaySua).EqualTo(globalVariables.IpAddress)
                    .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                    .Set(KcbLuotkham.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                    .Set(KcbLuotkham.Columns.NgaybatdauBhyt).EqualTo(dtpBHYTFfromDate.Value)
                    .Set(KcbLuotkham.Columns.NgayketthucBhyt).EqualTo(dtpBHYTToDate.Value)
                    .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtPatient_Code.Text)
                    .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtPatient_ID.Text)).Execute();
            }
        }

       

        private void cmdUpdateTheBHYT_Click(object sender, EventArgs e)
        {
            //var frm = new frm_NhapTheBHYT();
            //frm.objLuotkham = objLuotkham;
            //frm.ShowDialog();
            //if (frm.b_cancel)
            //{
            //    txtSoBHYT.Text = Utility.sDbnull(objLuotkham.MatheBhyt);
            //    dtpBHYTFfromDate.Value = Convert.ToDateTime(objLuotkham.NgaybatdauBhyt);
            //    dtpBHYTToDate.Value = Convert.ToDateTime(objLuotkham.NgayketthucBhyt);
            //    txtClinic_Code.Text = Utility.sDbnull(objLuotkham.InsClinicCode);
            //}
        }

        private void cmdCapnhatngayinphoiBHYT_Click_1(object sender, EventArgs e)
        {
        }

       

        /// <summary>
        ///     hàm thực hiện việc lưu lại thông tin bệnh nhân lại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdLuuLai_Click(object sender, EventArgs e)
        {
        }

        #region "Phương thức hủy tiền đã thanh toán"

        /// <summary>
        ///     hàm thực hiện hủy thông tin đã thanh toán
        /// </summary>
        /// <returns></returns>
        private bool InValiHuy()
        {
            if (grdThongTinDaThanhToan.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn chọn bản ghi thực hiện hủy thông tin ", "Thông báo", MessageBoxIcon.Warning);
                grdThongTinDaThanhToan.Focus();
                return false;
            }
            foreach (GridEXRow gridExRow in grdThongTinDaThanhToan.GetCheckedRows())
            {
                int ID_Detail = Utility.Int32Dbnull(gridExRow.Cells[KcbThanhtoanChitiet.Columns.IdPhieuChitiet].Value);
                switch (Utility.Int32Dbnull(gridExRow.Cells[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].Value))
                {
                    case 1:
                        SqlQuery sqlQuery = new Select().From(KcbDangkyKcb.Schema)
                            .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(ID_Detail)
                            .And(KcbDangkyKcb.Columns.TrangThai).IsEqualTo(1);
                        if (sqlQuery.GetRecordCount() > 0)
                        {
                            Utility.ShowMsg(
                                "Bản ghi khám đã thực hiện,Bạn không thể hủy, Mời bạn xem lại\n Mời bạn chọn những bản ghi chưa thực hiện",
                                "Thông báo", MessageBoxIcon.Warning);
                            grdThongTinDaThanhToan.Focus();
                            return false;
                        }
                        sqlQuery = new Select().From(KcbDangkyKcb.Schema)
                            .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(ID_Detail)
                            .And(KcbDangkyKcb.Columns.TrangthaiHuy).IsEqualTo(1);
                        if (sqlQuery.GetRecordCount() > 0)
                        {
                            Utility.ShowMsg(
                                "Bản ghi khám đã hủy,Bạn không thể hủy, Mời bạn xem lại\n Mời bạn chọn những bản ghi chưa hủy thông tin",
                                "Thông báo", MessageBoxIcon.Warning);
                            grdThongTinDaThanhToan.Focus();
                            return false;
                        }
                        break;
                    case 2:
                        sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                            .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(ID_Detail)
                            .And(KcbChidinhclsChitiet.Columns.TrangThai).IsGreaterThanOrEqualTo(1);
                        if (sqlQuery.GetRecordCount() > 0)
                        {
                            Utility.ShowMsg(
                                "Bản ghi cận lâm sàng đã thực hiện,Bạn không thể hủy, Mời bạn xem lại \n  Mời bạn chọn những bản ghi chưa thực hiện",
                                "Thông báo", MessageBoxIcon.Warning);
                            grdThongTinDaThanhToan.Focus();
                            return false;
                        }
                        sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                            .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(ID_Detail)
                            .And(KcbChidinhclsChitiet.Columns.TrangthaiHuy).IsEqualTo(1);
                        if (sqlQuery.GetRecordCount() > 0)
                        {
                            Utility.ShowMsg(
                                "Bản ghi cận lâm sàng đã hủy,Bạn không thể hủy, Mời bạn xem lại \n  Mời bạn chọn những bản ghi chưa hủy thông tin",
                                "Thông báo", MessageBoxIcon.Warning);
                            grdThongTinDaThanhToan.Focus();
                            return false;
                        }
                        break;
                    case 3:
                        sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(ID_Detail)
                            .And(KcbDonthuocChitiet.Columns.TrangThai).IsGreaterThanOrEqualTo(1);
                        if (sqlQuery.GetRecordCount() > 0)
                        {
                            Utility.ShowMsg(
                                "Bản ghi thuốc đã thực hiện,Bạn không thể hủy, Mời bạn xem lại \n  Mời bạn chọn những bản ghi chưa thực hiện",
                                "Thông báo", MessageBoxIcon.Warning);
                            grdThongTinDaThanhToan.Focus();
                            return false;
                        }
                        sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(ID_Detail)
                            .And(KcbDonthuocChitiet.Columns.TrangthaiHuy).IsEqualTo(1);
                        if (sqlQuery.GetRecordCount() > 0)
                        {
                            Utility.ShowMsg(
                                "Bản ghi thuốc đã hủy thông tin,Bạn không thể hủy, Mời bạn xem lại \n  Mời bạn chọn những bản ghi chưa hủy",
                                "Thông báo", MessageBoxIcon.Warning);
                            grdThongTinDaThanhToan.Focus();
                            return false;
                        }
                        break;
                }
            }


            return true;
        }

        /// <summary>
        ///     hàm thực hiện việc trả lại thông tin tiền dịch vụ
        ///     để bệnh nhân trả lại tiền hco bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdTraLaiTien_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidCancelData()) return;
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn trả lại tiền các dịch vụ đang được chọn cho Bệnh nhân hay không?",
                    "Thông báo", true))
                {
                    if (objLuotkham == null)
                    {
                        objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text), Utility.DoTrim(txtPatient_Code.Text));
                    }
                    KcbThanhtoan objPayment = TaophieuThanhtoanHuy();
                    string[] query = (from p in grdThongTinDaThanhToan.GetCheckedRows()
                                      select Utility.sDbnull(p.Cells["ten_chitietdichvu"].Value, "")).ToArray();
                    string noidung = string.Join(";", query);
                    frm_Chondanhmucdungchung _Chondanhmucdungchung = new frm_Chondanhmucdungchung("LYDOTRATIEN", "TRẢ TIỀN LẠI CHO BỆNH NHÂN", "Chọn lý do trả trước khi thực hiện", "Lý do trả tiền");
                    _Chondanhmucdungchung.ShowDialog();
                    if (_Chondanhmucdungchung.m_blnCancel) return;
                    ActionResult actionResult = _THANHTOAN.Tratien(objPayment,
                        objLuotkham,
                        TaodulieuthanhtoanchitietHuy(),_Chondanhmucdungchung.ma, noidung, _Chondanhmucdungchung.ten);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            tabThongTinThanhToan.SelectedTab = tabPagePhieuChi;
                            LaydanhsachLichsuthanhtoan_phieuchi();
                            foreach (GridEXRow gridExRow in grdThongTinDaThanhToan.GetCheckedRows())
                            {
                                gridExRow.BeginEdit();
                                gridExRow.Cells[KcbThanhtoanChitiet.Columns.TrangthaiHuy].Value = 1;
                                gridExRow.EndEdit();
                                grdThongTinDaThanhToan.UpdateData();
                                m_dtChiPhiDaThanhToan.AcceptChanges();
                            }
                            Utility.GotoNewRowJanus(grdPhieuChi, KcbThanhtoan.Columns.IdThanhtoan,
                                    Utility.sDbnull(objPayment.IdThanhtoan));
                            if (PropertyLib._MayInProperties.TudonginPhieuchiSaukhitratien)
                            {
                                new INPHIEU_THANHTOAN_NGOAITRU().InPhieuchi(objPayment.IdThanhtoan);
                            }
                            if (PropertyLib._ThanhtoanProperties.HienthidichvuNgaysaukhitratien)
                            {
                                CallPhieuChi();
                            }

                            ModifyCommand();
                            break;
                        case ActionResult.AssignIsConfirmed:
                            Utility.ShowMsg("Đã có dịch vụ được thực hiện rồi. Bạn không thể trả lại tiền !",
                                "Thông báo");
                            break;
                        case ActionResult.PresIsConfirmed:
                            Utility.ShowMsg("Đã có thuốc được xác nhận cấp phát. Bạn không thể trả lại tiền !",
                                "Thông báo");
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin", "Thông báo");
                            break;
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
        }

        private List<Int64> TaodulieuthanhtoanchitietHuy()
        {
            List<Int64> lstIdChitiet = (from q in grdThongTinDaThanhToan.GetCheckedRows()
                                        select Utility.Int64Dbnull(q.Cells[KcbThanhtoanChitiet.Columns.IdChitiet].Value)).ToList<Int64>();
            return lstIdChitiet;
        }
        /// <summary>
        ///     hàm thực hiện hủy thông tin đã thanh toán
        /// </summary>
        /// <returns></returns>
        private bool IsValidCancelData()
        {
            if (grdThongTinDaThanhToan.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn chọn bản ghi thực hiện hủy thông tin ", "Thông báo", MessageBoxIcon.Warning);
                grdThongTinDaThanhToan.Focus();
                return false;
            }
            foreach (GridEXRow gridExRow in grdThongTinDaThanhToan.GetCheckedRows())
            {
                int IdPhieuChitiet = Utility.Int32Dbnull(gridExRow.Cells[KcbThanhtoanChitiet.Columns.IdPhieuChitiet].Value);
                switch (Utility.Int32Dbnull(gridExRow.Cells["Id_Loaithanhtoan"].Value))
                {
                    case 1:
                        KcbDangkyKcb objKcbDangkyKcb = KcbDangkyKcb.FetchByID(IdPhieuChitiet);
                        if (objKcbDangkyKcb != null && Utility.Byte2Bool(objKcbDangkyKcb.TrangThai))
                        {
                            Utility.ShowMsg(
                                "Bản ghi khám đã thực hiện,Bạn không thể hủy, Mời bạn xem lại\n Mời bạn chọn những bản ghi chưa thực hiện",
                                "Thông báo", MessageBoxIcon.Warning);
                            grdThongTinDaThanhToan.Focus();
                            return false;
                        }
                        if (objKcbDangkyKcb != null && Utility.Byte2Bool(objKcbDangkyKcb.TrangthaiHuy))
                        {
                            Utility.ShowMsg(
                                "Bản ghi khám đã hủy,Bạn không thể hủy, Mời bạn xem lại\n Mời bạn chọn những bản ghi chưa hủy thông tin",
                                "Thông báo", MessageBoxIcon.Warning);
                            grdThongTinDaThanhToan.Focus();
                            return false;
                        }
                        break;
                    case 2:
                        KcbChidinhclsChitiet objKcbChidinhclsChitiet = KcbChidinhclsChitiet.FetchByID(IdPhieuChitiet);
                        if (objKcbChidinhclsChitiet != null && objKcbChidinhclsChitiet.TrangThai >= 3)
                        {
                            Utility.ShowMsg(
                                "Bản ghi cận lâm sàng đã thực hiện,Bạn không thể hủy, Mời bạn xem lại \n  Mời bạn chọn những bản ghi chưa thực hiện",
                                "Thông báo", MessageBoxIcon.Warning);
                            grdThongTinDaThanhToan.Focus();
                            return false;
                        }
                        if (objKcbChidinhclsChitiet != null && Utility.Byte2Bool(objKcbChidinhclsChitiet.TrangthaiHuy))
                        {
                            Utility.ShowMsg(
                                "Bản ghi cận lâm sàng đã hủy,Bạn không thể hủy, Mời bạn xem lại \n  Mời bạn chọn những bản ghi chưa hủy thông tin",
                                "Thông báo", MessageBoxIcon.Warning);
                            grdThongTinDaThanhToan.Focus();
                            return false;
                        }
                        break;
                    case 3:
                    case 5:
                        KcbDonthuocChitiet objKcbDonthuocChitiet = KcbDonthuocChitiet.FetchByID(IdPhieuChitiet);
                        if (objKcbDonthuocChitiet != null && Utility.Byte2Bool(objKcbDonthuocChitiet.TrangThai))
                        {
                            Utility.ShowMsg(
                                "Bản ghi thuốc đã thực hiện,Bạn không thể hủy, Mời bạn xem lại \n  Mời bạn chọn những bản ghi chưa thực hiện",
                                "Thông báo", MessageBoxIcon.Warning);
                            grdThongTinDaThanhToan.Focus();
                            return false;
                        }
                        if (objKcbDonthuocChitiet != null && Utility.Byte2Bool(objKcbDonthuocChitiet.TrangthaiHuy))
                        {
                            Utility.ShowMsg(
                                "Bản ghi thuốc đã hủy thông tin,Bạn không thể hủy, Mời bạn xem lại \n  Mời bạn chọn những bản ghi chưa hủy",
                                "Thông báo", MessageBoxIcon.Warning);
                            grdThongTinDaThanhToan.Focus();
                            return false;
                        }
                        break;
                }
            }


            return true;
        }
        private KcbThanhtoan TaophieuThanhtoanHuy()
        {
            KcbThanhtoan objPayment = new KcbThanhtoan();
            objPayment.MaLuotkham = Utility.sDbnull(txtPatient_Code.Text, "");
            objPayment.IdBenhnhan = Utility.Int32Dbnull(txtPatient_ID.Text, -1);
            objPayment.NgayTao = globalVariables.SysDate;
            objPayment.NguoiTao = globalVariables.UserName;
            objPayment.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
            objPayment.KieuThanhtoan = 1;
            objPayment.NoiTru = 0;
            objPayment.TrangthaiIn = 0;
            objPayment.NgayIn = null;
            objPayment.NguoiIn = string.Empty;
            objPayment.NgayTonghop = null;
            objPayment.NguoiTonghop = string.Empty;
            objPayment.NgayChot = null;
            objPayment.TrangthaiChot = 0;
            objPayment.NgayRavien = objLuotkham.NgayRavien;
            objPayment.MaThanhtoan = THU_VIEN_CHUNG.TaoMathanhtoan(globalVariables.SysDate);
            objPayment.NgayThanhtoan = globalVariables.SysDate;
            objPayment.IdNhanvienThanhtoan = globalVariables.gv_intIDNhanvien;
            objPayment.IpMaytao = globalVariables.gv_strIPAddress;
            objPayment.TenMaytao = globalVariables.gv_strComputerName;
            return objPayment;
        }

     

        /// <summary>
        ///     hàm thực hiện việc thực iheenj phiếu cih cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuChi_Click(object sender, EventArgs e)
        {
            if (grdPhieuChi.CurrentRow != null)
            {
                CallPhieuChi();
            }
        }

        #endregion

        private void cmdTongHopRaVien_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPatient_Code.Text))
                {
                    frm_TonghopRavien frm = new frm_TonghopRavien(globalVariables.MA_KHOA_THIEN);
                    frm.ma_luotkham = Utility.sDbnull(txtPatient_Code.Text,"");
                    frm.ShowDialog();
                    if (!frm.bCancel)
                    {
                        getData();
                    }

                }
                else
                {
                    Utility.ShowMsg("Bạn chưa chọn bệnh nhân để xem tổng hợp ra viện!","Thông báo",MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Trace("Loi: "+ ex.Message);
            }
        }

        private void cmdupdatethongtinngay_Click(object sender, EventArgs e)
        {

            var frm = new FrmUpdateNgaykham(Utility.sDbnull(txtPatient_Code.Text.Trim()));
            frm.ShowDialog();
        }

        private void cmdThanhToan_Click_1(object sender, EventArgs e)
        {

        }

        private void optAll_CheckedChanged_1(object sender, EventArgs e)
        {

        }

       

    }
}