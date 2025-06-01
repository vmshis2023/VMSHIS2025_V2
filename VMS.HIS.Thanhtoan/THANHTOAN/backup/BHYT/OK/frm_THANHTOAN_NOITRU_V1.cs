using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using NLog;
using SubSonic;
//using VMS.HIS.BHYT.Class;
using VMS.HIS.HLC.ASTM;
using VNS.HIS.UI.Classess;
using VNS.HIS.UI.Forms.Dungchung;
using VNS.HIS.UI.Forms.THANHTOAN;
using VNS.Libs;
using VMS.HIS.DAL;
using TextAlignment = Janus.Windows.GridEX.TextAlignment;
using VNS.Properties;
using VNS.HIS.UI.NGOAITRU;
using VNS.HIS.Classes;
using VNS.HIS.BusRule.Classes;
using CrystalDecisions.CrystalReports.Engine;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.UI.Forms.Cauhinh;
using System.Transactions;
using Janus.Windows.UI.Tab;
using VNS.HIS.UI.HOADONDO;
using VNS.HIS.UI.NOITRU;

namespace  VNS.HIS.UI.THANHTOAN
{
    public partial class frm_THANHTOAN_NOITRU_V1 : Form
    {
        KCB_THANHTOAN _THANHTOAN = new KCB_THANHTOAN();
        private readonly string FileName = string.Format("{0}/{1}", Application.StartupPath, "SplitterDistance.txt");
        private int Distance = 400;
        private int HOADON_CAPPHAT_ID = -1;
        private bool INPHIEU_CLICK=false;
        private bool m_blnHasloaded;
        private bool blnJustPayment;
        private DataTable dtCapPhat;
        private DataTable dtPatientPayment;
        private DataTable m_dtChiPhiDaThanhToan = new DataTable();
        private DataTable m_dtChiPhiThanhtoan;
        string Args = "ALL";
        private string lst_IDLoaithanhtoan = "";
        private bool _Malankham_keydown = false;
        private bool _ID_keydown = false;
        string _help = "";
        /// <summary>
        ///     05-11-2013
        /// </summary>
        #region "khai báo biến "
        private DataTable m_dtDataTimKiem = new DataTable();
        private DataTable m_dtPayment, m_dtPhieuChi = new DataTable();
        private DataTable m_dtReportPhieuThu;
        private KcbLuotkham objLuotkham;
        private KcbDanhsachBenhnhan objBenhnhan;
        private string sFileName = "RedInvoicePrinterConfig.txt";
        private long v_Payment_ID = -1;
        private NLog.Logger log;
        byte v_bytNoitru = 0;//0= ngoại trú;1= nội trú
        string LoaiForm = "ALL";
        #endregion
        public frm_THANHTOAN_NOITRU_V1(string pv_sArgs)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.Args = pv_sArgs.Split('-')[0]; ;//Loai khám(KT, KSK, KN)-Loại dịch vụ được phép hiển thị(1: Khám;2=CSL;3=Thuốc,...)
            lst_IDLoaithanhtoan = pv_sArgs.Split('-')[1];
            v_bytNoitru = Utility.ByteDbnull(pv_sArgs.Split('-')[2], 0);
            LoaiForm = pv_sArgs.Split('-')[3];//ALL-TAICHINH-THUNGAN
            cmdChuyenCLS.Visible = cmdDungChuyenCLS.Visible = cmdDanhsachinphoi.Visible = cmdHoanung.Visible =cmdChiphithem.Visible= cmdHuyInPhoiBHYT.Visible = cmdChuyenDT.Visible = !this.Args.Contains("KN");
            KeyPreview = true;
            dtFromDate.Value =
                dtPaymentDate.Value = dtInput_Date.Value = dtToDate.Value = globalVariables.SysDate;
            //cmdHuyThanhToan.Visible = (globalVariables.IsAdmin || globalVariables.quyenh);
            Utility.grdExVisiableColName(grdPayment, "cmdHUY_PHIEUTHU", globalVariables.IsAdmin || Utility.Coquyen("thanhtoan_huythanhtoan"));
            Utility.grdExVisiableColName(grdPhieuChi, "cmdHuyPhieuChi", globalVariables.IsAdmin || Utility.Coquyen("thanhtoan_huyphieuchi"));
            if (grdPayment.RootTable.Columns.Contains(KcbThanhtoan.Columns.NgayThanhtoan))
                grdPayment.RootTable.Columns[KcbThanhtoan.Columns.NgayThanhtoan].Selectable =
                     Utility.Coquyen("thanhtoan_suangaythanhtoan") || globalVariables.IsAdmin;
            if (grdPhieuChi.RootTable.Columns.Contains(KcbThanhtoan.Columns.NgayThanhtoan))
                grdPhieuChi.RootTable.Columns[KcbThanhtoan.Columns.NgayThanhtoan].Selectable =
                     Utility.Coquyen("thanhtoan_suangaythanhtoan") || globalVariables.IsAdmin;
            cmdCreatePres.Visible = Utility.Coquyen("thanhtoan_coquyen_thamkham") || globalVariables.IsAdmin;
            cmdCauHinh.Visible = Utility.Coquyen("thanhtoan_coquyen_cauhinh") || globalVariables.IsAdmin;
            LoadLaserPrinters();
            CauHinh();
            log = LogManager.GetLogger(Name);
            getColorMessage = lblMessage.BackColor;
            InitEvents();
            PropertyLib._xmlproperties = PropertyLib.GetXMLProperties();
            log.Trace("Constructor finished");
        }
        void InitEvents()
        {
            Load += new EventHandler(frm_THANHTOAN_NOITRU_V1_Load);
            Shown += frm_THANHTOAN_NOITRU_V1_Shown;
            FormClosing += new FormClosingEventHandler(frm_THANHTOAN_NOITRU_V1_FormClosing);
            KeyDown += new KeyEventHandler(frm_THANHTOAN_NOITRU_V1_KeyDown);

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
            grdPayment.MouseDoubleClick += grdPayment_MouseDoubleClick;
            grdPayment.SelectionChanged += grdPayment_SelectionChanged;
            grdPhieuChi.UpdatingCell += grdPhieuChi_UpdatingCell;
            grdPhieuChi.ColumnButtonClick += grdPhieuChi_ColumnButtonClick;
            cmdChuyenDT.Click += cmdChuyenDT_Click;
            

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
          //  cmdChuyenCLS.Click += new EventHandler(cmdChuyenCLS_Click);
            //cmdDungChuyenCLS.Click += new EventHandler(cmdDungChuyenCLS_Click);
            cmdCauHinh.Click += new EventHandler(this.cmdCauHinh_Click);
            dtFromDate.Enabled = dtToDate.Enabled = chkCreateDate.Checked;

            chkHoixacnhanhuythanhtoan.CheckedChanged += new EventHandler(_CheckedChanged);
            chkHoixacnhanthanhtoan.CheckedChanged += new EventHandler(_CheckedChanged);
            chkPreviewHoadon.CheckedChanged += new EventHandler(_CheckedChanged);
            chkPreviewInBienlai.CheckedChanged += new EventHandler(_CheckedChanged);
            chkPreviewInphoiBHYT.CheckedChanged += new EventHandler(_CheckedChanged);
            chkTudonginhoadonsauthanhtoan.CheckedChanged += new EventHandler(_CheckedChanged);
            chkViewtruockhihuythanhtoan.CheckedChanged += new EventHandler(_CheckedChanged);
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
            cmdCalculator.Click += cmdCalculator_Click;
            mnuCapnhatPTTT.Click += mnuCapnhatPTTT_Click;
            cmdChiphithem.Click += cmdChiphithem_Click;
            mnuPhanbotientheoPTTT.Click += mnuPhanbotientheoPTTT_Click;
            mnuPhanboPTTT_PhieuChi.Click += mnuPhanboPTTT_PhieuChi_Click;
            cmdHoanung.Click += cmdHoanung_Click;
            mnuTutuc.Click += mnuTutuc_Click;
            grdThongTinChuaThanhToan.SelectionChanged += grdThongTinChuaThanhToan_SelectionChanged;
            cmdChuyenNguon.Click += cmdChuyenNguon_Click;
            cboKieuin.SelectedIndexChanged += cboKieuin_SelectedIndexChanged;
            ucTamung1._OnChangedData += ucTamung1__OnChangedData;
            optAll.CheckedChanged+=optAll_CheckedChanged;
            optNoitru.CheckedChanged += optAll_CheckedChanged;
            optNgoaitru.CheckedChanged += optAll_CheckedChanged;
            txtTilemiengiamAll.KeyDown += txtTilemiengiamAll_KeyDown;
            grdPhieudieutri.SelectionChanged += grdPhieudieutri_SelectionChanged;
            grdPayment.CellValueChanged += grdPayment_CellValueChanged;
            grdPayment.UpdatingCell += grdPayment_UpdatingCell;
            txtID.KeyDown += txtID_KeyDown;
            txtptramtanggia_nguoinuocngoai.KeyDown+=txtptramtanggia_nguoinuocngoai_KeyDown;
        }
        void mnuPhanboPTTT_PhieuChi_Click(object sender, EventArgs e)
        {
            if (!Utility.Coquyen("THANHTOAN_QUYEN_PHANBOPTTT"))
            {
                Utility.ShowMsg("Bạn không có quyền phân bổ hình thức thanh toán(THANHTOAN_QUYEN_PHANBOPTTT). Vui lòng liên hệ quản trị để được cấp quyền");
                return;
            }
            PhanboPhieuChi();
        }
        bool CapnhatDongia(GridEXRow _row, decimal tile, decimal don_giamoi, bool manual)
        {
            if (Utility.sDbnull(_row.Cells["trangthai_thanhtoan"].Value, "0") == "0")
            {
                _row.BeginEdit();
                //Cập nhật luôn vào bảng trong CSDL để in bảng kê chi phí cho người bệnh xem trước khi thanh toán
                byte id_loaithanhtoan = Utility.ByteDbnull(_row.Cells["id_loaithanhtoan"].Value);

                long id_phieu = Utility.Int64Dbnull(_row.Cells["id_phieu"].Value);
                long id_phieuchitiet = Utility.Int64Dbnull(_row.Cells["id_phieu_chitiet"].Value);
                if (!manual)
                {
                    _row.Cells["don_gia"].Value = Utility.DecimaltoDbnull(_row.Cells["don_gia_goc"].Value, 0) * (1 + tile / 100);
                }
                else
                {
                    _row.Cells["don_gia"].Value = don_giamoi;
                }
                _row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value = _row.Cells["don_gia"].Value;

                decimal don_gia = Utility.DecimaltoDbnull(_row.Cells["don_gia"].Value, 0);
                CapnhatChietkhau_DonGia(1, id_loaithanhtoan, "%", 0, don_gia, id_phieu, id_phieuchitiet);
                //Tính lại các khoản tiền
                _row.Cells["TT_BHYT"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BhytChitra], 0)) *
                  (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0) / 100) *
                  Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 0);

                _row.Cells["TT_BN"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0) *
                                  (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0) / 100) +
                                  Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) *
                                 Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 0);

                _row.Cells["TT"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) *
                               (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0) / 100) +
                               Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) *
                              Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 0);

                _row.Cells["TT_PHUTHU"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) *
                                     Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 0);

                _row.Cells["TT_KHONG_PHUTHU"].Value = Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) *
                                           (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0) / 100) *
                                           Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 0);
                _row.Cells["TT_BN_KHONG_TUTUC"].Value = Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0) *
                                          (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0) / 100) *
                                          Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 0);
                _row.Cells["TT_BN_KHONG_PHUTHU"].Value =
                    Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0) *
                    (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0) / 100) *
                    Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 0);
                _row.Cells["thuc_thu"].Value = Utility.DecimaltoDbnull(_row.Cells["TT_BN_KHONG_PHUTHU"].Value, 0) - Utility.DecimaltoDbnull(_row.Cells["tien_chietkhau"].Value, 0);
                _row.EndEdit();
                return true;
            }
            else
            {
                Utility.ShowMsg("Bản ghi đã được thanh toán nên bạn không thể chỉnh sửa đơn giá");
                return false;
            }
        }
        void txtptramtanggia_nguoinuocngoai_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("thanhtoan_tanggiam_tile_dongia"))
                {
                    Utility.ShowMsg("Bạn không có quyền tăng giảm đơn giá thanh toán (thanhtoan_tanggiam_tile_dongia). Vui lòng liên hệ quản trị hệ thống để được phân quyền");
                    return;
                }
                if (!chkDoituongnguoinuocngoai.Checked)
                {
                    Utility.ShowMsg("Tính năng này yêu cầu bạn phải chọn mục % tăng giá đối với người nước ngoài. Vui lòng check chọn");
                    chkDoituongnguoinuocngoai.Focus();
                    return;
                }
                decimal tile = Utility.DecimaltoDbnull(txtptramtanggia_nguoinuocngoai.Text, 0);//<0 = giảm giá;>0= tăng giá
                if (e.KeyCode == Keys.Enter)
                {
                    if (grdThongTinChuaThanhToan.GetCheckedRows().Count() <= 0)
                    {
                        Utility.ShowMsg("Bạn cần chọn ít nhất 1 dịch vụ trước khi thực hiện cập tăng/giảm đơn giá");
                        return;
                    }
                    string ask = string.Format("Bạn có chắc chắn muốn {0} giá {1} % cho toàn bộ các dịch vụ đang được chọn?", tile > 0 ? "tăng" : "giảm", Convert.ToInt16(tile).ToString());

                    if (!Utility.AcceptQuestion(ask, string.Format("Xác nhận {0} cho các dịch vụ đang chọn", tile > 0 ? "tăng giá" : "giảm giá"), true)) return;
                    string dsach_dvu = String.Join(",", (from p in grdThongTinChuaThanhToan.GetCheckedRows()
                                                         select Utility.sDbnull(p.Cells["ten_chitietdichvu"].Value, "")).ToArray<string>());


                    foreach (GridEXRow _row in grdThongTinChuaThanhToan.GetCheckedRows())
                    {
                        CapnhatDongia(_row, tile,0,false);
                    }
                    grdThongTinChuaThanhToan.UpdateData();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("{0} {1} % đơn giá cho các dịch vụ {2} thành công ", tile > 0 ? "tăng" : "giảm", tile, dsach_dvu), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                    Utility.ShowMsg(string.Format("Bạn vừa thực hiện {0} {1} % đơn giá cho các dịch vụ đang chọn thành công.\nNhấn OK để kết thúc", tile > 0 ? "tăng" : "giảm", tile));


                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                SetSumTotalProperties();
            }
        }
        void txtID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (!string.IsNullOrEmpty(txtID.Text))
                {
                    _ID_keydown = true;
                    cmdSearch_Click(cmdSearch, new EventArgs());
                    _ID_keydown = false;
                    if (grdList.RowCount == 1)
                    {
                        grdList.MoveFirst();
                        grdList_DoubleClick(grdList, new EventArgs());
                    }
                    //cmdSearch.Focus();
                }
            }
        }

        void grdPayment_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
             if (e.Column.Key == "ghichu")
                {
                    Suaghichu();
                }
        }

       
        
        void grdPayment_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            try
            {
                //if (objLuotkham != null && objLuotkham.TrangthaiNoitru >= 1)
                //{

                //    Utility.ShowMsg("Người bệnh đã ở trạng thái nội trú nên bạn không được quyền sửa các thông tin liên quan đến phiếu thanh toán ngoại trú. Vui lòng kiểm tra lại");
                //    return;
                //}
                if (!Kiemtradieukienhuytt_theongay()) return;
                if (e.Column.Key == KcbThanhtoan.Columns.NgayThanhtoan)
                {
                    if (Utility.Coquyen("thanhtoan_suangaythanhtoan"))
                    {
                        UpdatePaymentDate();
                    }
                    else
                    {
                        Utility.ShowMsg("Bạn không có quyền cập nhật thông tin ngày thanh toán(quyen_suangay_thanhtoan). Vui lòng liên hệ quản trị hệ thống để được cấp quyền");
                    }
                }
               
                else if (e.Column.Key == "ma_pttt")
                {
                    if (!Utility.Coquyen("THANHTOAN_SUA_PTTT"))
                    {
                        Utility.ShowMsg("Bạn không có quyền sửa đổi phương thức thanh toán (THANHTOAN_SUA_PTTT). Vui lòng liên hệ quản trị hệ thống để được phân quyền");
                        return;
                    }
                    //Cập nhật lại PTTT
                    if (!Utility.isValidGrid(grdPayment)) return;

                    CapnhatPTTT_Grid();
                }
                else if (e.Column.Key == "ma_nganhang")
                {
                    if (!Utility.Coquyen("THANHTOAN_SUA_PTTT"))
                    {
                        Utility.ShowMsg("Bạn không có quyền sửa đổi phương thức thanh toán (THANHTOAN_SUA_PTTT). Vui lòng liên hệ quản trị hệ thống để được phân quyền");
                        return;
                    }
                    string ma_pttt = grdPayment.GetValue("ma_pttt").ToString();
                    string ma_nganhang = Utility.sDbnull(grdPayment.GetValue("ma_nganhang").ToString(), "-1");
                    List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
                    if (lstPTTT.Contains(ma_pttt) && (ma_nganhang == "-1" || ma_nganhang.Length == 0))//Đợi chọn ngân hàng
                    {
                        return;
                    }
                    CapnhatPTTT_Grid();
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
           
        }
        void Suaghichu()
        {
            try
            {
                long id_thanhtoan = Utility.Int64Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan));
                KcbThanhtoan _objthanhtoan = KcbThanhtoan.FetchByID(id_thanhtoan);
                if (_objthanhtoan == null)
                {
                    Utility.ShowMsg("Không tìm được bản ghi thanh toán(Có thể vừa bị xóa khỏi hệ thống). Vui lòng chọn lại người bệnh để refresh lại");
                    return;
                }
                string oldValue = Utility.sDbnull(_objthanhtoan.Ghichu, "");
                string newValue = Utility.sDbnull(grdPayment.GetValue("ghichu"), "");
                int numofA = new Update(KcbThanhtoan.Schema).Set(KcbThanhtoan.Columns.Ghichu).EqualTo(newValue).Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(id_thanhtoan).Execute();
                if (numofA > 0)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin ghi chú của bản tin thanh toán Id={0}, ghi chú cũ={1}, ghi chú mới ={2} thành công ", id_thanhtoan.ToString(), oldValue, newValue), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                    Utility.ShowMsg("Cập nhật ghi chú thành công. Nhấn OK để kết thúc");
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        bool Kiemtradieukienhuytt_theongay()
        {
            KcbThanhtoan _vobjTT = KcbThanhtoan.FetchByID(Utility.Int64Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan)));
            if (_vobjTT == null)
            {
                Utility.ShowMsg("Không lấy được thông tin thanh toán từ lưới danh sách phiếu thu. Vui lòng kiểm tra lại");
                return false;
            }
            int kcbThanhtoanSongayHuythanhtoan =
                           Utility.Int32Dbnull(
                               THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_SONGAY_HUYTHANHTOAN", "0", true), 0);
            var chenhlech =
                (int)Math.Ceiling((globalVariables.SysDate.Date - _vobjTT.NgayThanhtoan.Date).TotalDays);
            if (chenhlech > kcbThanhtoanSongayHuythanhtoan)
            {
                Utility.ShowMsg(string.Format("Ngày thanh toán: {0}. Hệ thống không cho phép bạn thay đổi thông tin thanh toán khi đã quá {1} ngày. Cần liên hệ quản trị hệ thống để được trợ giúp", _vobjTT.NgayThanhtoan.Date.ToString("dd/MM/yyyy"), kcbThanhtoanSongayHuythanhtoan.ToString()));
                return false;
            }
            if (Utility.Byte2Bool(_vobjTT.TrangthaiChot))
            {
                Utility.ShowMsg("Thanh toán đang chọn đã được chốt nên bạn không thể thay đổi thông tin thanh toán. Vui lòng kiểm tra lại!");
                return false;
            }
            return true;
        }
        void CapnhatPTTT_Grid()
        {
            try
            {
                List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
                Int32 IdThanhtoan = Utility.Int32Dbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1);
                KcbThanhtoan objThanhtoan = KcbThanhtoan.FetchByID(IdThanhtoan);
                string ma_pttt = grdPayment.GetValue("ma_pttt").ToString();
                string ma_pttt_cu = objThanhtoan.MaPttt;
                string ten_pttt_cu = grdPayment.CurrentRow.Cells["ten_pttt"].Value.ToString();
                string ma_nganhang = Utility.sDbnull(grdPayment.GetValue("ma_nganhang").ToString(), "-1");
                if (!lstPTTT.Contains(ma_pttt))
                    if (ma_pttt == ma_pttt_cu) return;
                
                DataRow[] arrDr = m_dtPayment.Select("id_thanhtoan=" + IdThanhtoan.ToString());
                if (arrDr.Length <= 0) return;
                //List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
                //if (cboNganhang.Enabled && cboNganhang.SelectedValue.ToString() == "-1")
                //{
                //    Utility.ShowMsg(string.Format("Bạn phải chọn ngân hàng khi chọn phương thức thanh toán {0}", cboPttt.Text));
                //    cboNganhang.Focus();
                //    return;
                //}
               
                if (!lstPTTT.Contains(ma_pttt))
                    ma_nganhang = "";
                if (lstPTTT.Contains(ma_pttt) && (ma_nganhang == "-1" || ma_nganhang.Length == 0))//Đợi chọn ngân hàng
                {
                    if (objThanhtoan.KieuThanhtoan == 1)
                    {
                        Utility.ShowMsg("Bạn cần chọn ngân hàng");
                        Utility.focusCell(grdPayment, "ma_nganhang");
                        return;
                    }
                }
                if (!isValidPttt_Nganhang_Grid())
                    return;
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn thay đổi phương thức thanh toán từ {0} sang {1}", arrDr[0]["ten_pttt"], ma_pttt), "Xác nhận cập nhật PTTT", true))
                {
                    Utility.ShowMsg("Bạn vừa chọn hủy cập nhật phương thức thanh toán. Nhấn OK để kết thúc");
                    return;
                }
                List<string> lstPhanbo = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_BATBUOCPHANBO", false).Split(',').ToList<string>();
                DataTable dtPhanbo = _THANHTOAN.KcbThanhtoanLaydulieuphanbothanhtoanTheoPttt(IdThanhtoan, -1l, -1l).Tables[0];
                //Check nếu chọn Pttt=phân bổ và số dòng phân bổ <1 thì tự động hiển thị form phân bổ
                if (lstPhanbo.Contains(ma_pttt))
                {
                    if (dtPhanbo.Select("so_tien>0").Length <= 1)
                        Phanbo();
                }
                else
                {
                    if (dtPhanbo.Select("so_tien>0").Length > 1)//Không phải chọn phân bổ mà số lượng dòng phân bổ >1 thì thông báo người dùng lựa chọn
                    {
                        if (Utility.AcceptQuestion(string.Format("Lần phân bổ gần nhất bạn đang phân bổ tiền cho nhiều phương thức thanh toán. Trong khi hình thức thanh toán bạn đang chọn ({0}) không phải là phân bổ. Bạn có muốn hệ thống tự động chuyển tất cả số tiền sang hình thức vừa chọn {1} hay không?\n.Nhấn Yes để đồng ý. Nhấn No để xem lại thông tin phân bổ", ma_pttt, ma_pttt), "Cảnh báo và gợi ý", true))
                        {
                            Capnhatphanbo11(dtPhanbo, objThanhtoan, ma_pttt, ma_nganhang);
                        }
                        else
                            Phanbo();
                    }
                    else if (dtPhanbo.Select("so_tien>0").Length == 1)//Thực hiện cập nhật luôn
                    {
                        Capnhatphanbo11(dtPhanbo, objThanhtoan, ma_pttt, ma_nganhang);
                    }


                }

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        bool isValidPttt_Nganhang_Grid()
        {
            List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
            string ma_pttt = grdPayment.GetValue("ma_pttt").ToString();
            string ma_nganhang = Utility.sDbnull(grdPayment.GetValue("ma_nganhang").ToString(), "-1");
            if (lstPTTT.Contains(ma_pttt) && (ma_nganhang.Length <= 0 || ma_nganhang == "-1"))
            {
                Utility.ShowMsg(string.Format("Bạn phải chọn ngân hàng khi chọn phương thức thanh toán {0}", ma_pttt));
                cboNganhang.Focus();
                return false;
            }
            return true;
        }

        void InitPTTTColumns()
        {
            try
            {
                DataTable dtPTTT = dtPttt;
                GridEXColumn _colmaPttt = grdPayment.RootTable.Columns["ma_pttt"];
                _colmaPttt.HasValueList = true;
                _colmaPttt.LimitToList = true;

                GridEXValueListItemCollection _colmaPttt_Collection = grdPayment.RootTable.Columns["ma_pttt"].ValueList;
                foreach (DataRow item in dtPTTT.Rows)
                {
                    _colmaPttt_Collection.Add(item["MA"].ToString(), item["TEN"].ToString());
                }
                _colmaPttt = grdPhieuChi.RootTable.Columns["ma_pttt"];
                _colmaPttt.HasValueList = true;
                _colmaPttt.LimitToList = true;

                _colmaPttt_Collection = grdPhieuChi.RootTable.Columns["ma_pttt"].ValueList;
                foreach (DataRow item in dtPTTT.Rows)
                {
                    _colmaPttt_Collection.Add(item["MA"].ToString(), item["TEN"].ToString());
                }

                DataTable dtNganhang = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("NGANHANG").And(DmucChung.Columns.TrangThai).IsEqualTo(1).ExecuteDataSet().Tables[0];
                GridEXColumn _colIDNganHang = grdPayment.RootTable.Columns["ma_nganhang"];
                _colIDNganHang.HasValueList = true;
                _colIDNganHang.LimitToList = true;

                GridEXValueListItemCollection _colIDNganHang_Collection = grdPayment.RootTable.Columns["ma_nganhang"].ValueList;
                foreach (DataRow item in dtNganhang.Rows)
                {
                    _colIDNganHang_Collection.Add(item["MA"].ToString(), item["TEN"].ToString());
                }
                _colIDNganHang = grdPhieuChi.RootTable.Columns["ma_nganhang"];
                _colIDNganHang.HasValueList = true;
                _colIDNganHang.LimitToList = true;

                _colIDNganHang_Collection = grdPhieuChi.RootTable.Columns["ma_nganhang"].ValueList;
                foreach (DataRow item in dtNganhang.Rows)
                {
                    _colIDNganHang_Collection.Add(item["MA"].ToString(), item["TEN"].ToString());
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
           

        }
        void grdPhieudieutri_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
               LaythongtinPhieudieutri();
            }
            catch (Exception ex)
            {
                
            }
        }
        void VisibleTaichinhduyet()
        {
            try
            {
                cauhinhtaichinhduyet = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_TAICHINH_DUYET_TRUOCKHITHANHTOAN", "0", true) == "1";
                uiTabPageTaichinhduyet.TabVisible = cauhinhtaichinhduyet;
                if (LoaiForm == "ALL")
                {
                    tabpageThongTinThanhToan.TabVisible = true;
                    uiTabPageTaichinhduyet.TabVisible = true && cauhinhtaichinhduyet;
                    tabPageThongTinChiTietThanhToan.TabVisible = false;
                    tabPageThongTinDaThanhToan.TabVisible = true;
                    uiTabLSuPhieuTT.TabVisible = false;
                    TabPageTamung.TabVisible = true;
                    TabPageTamung.TabVisible = true;
                    TabpageCauhinh.TabVisible = true;
                }
                else if (LoaiForm == "TAICHINH")
                {
                    tabpageThongTinThanhToan.TabVisible = false;
                    uiTabPageTaichinhduyet.TabVisible = true && cauhinhtaichinhduyet;
                    tabPageThongTinChiTietThanhToan.TabVisible = false;
                    tabPageThongTinDaThanhToan.TabVisible = false;
                    uiTabLSuPhieuTT.TabVisible = false;
                    TabPageTamung.TabVisible = false;
                    TabPageTamung.TabVisible = false;
                    TabpageCauhinh.TabVisible = false;
                }
                else if (LoaiForm == "THUNGAN")
                {
                    tabpageThongTinThanhToan.TabVisible = true;
                    uiTabPageTaichinhduyet.TabVisible = false && cauhinhtaichinhduyet;
                    tabPageThongTinChiTietThanhToan.TabVisible = false;
                    tabPageThongTinDaThanhToan.TabVisible = true;
                    uiTabLSuPhieuTT.TabVisible = false;
                    TabPageTamung.TabVisible = true;
                    TabPageTamung.TabVisible = true;
                    TabpageCauhinh.TabVisible = true;
                }
            }
            catch (Exception ex)
            {


            }
        }
        void frm_THANHTOAN_NOITRU_V1_Shown(object sender, EventArgs e)
        {
            VisibleTaichinhduyet();
        }
        #region lấy dữ liệu cho tài chính phê duyệt
        private readonly KCB_CHIDINH_CANLAMSANG _KCB_CHIDINH_CANLAMSANG = new KCB_CHIDINH_CANLAMSANG();
        private readonly KCB_THAMKHAM _KCB_THAMKHAM = new KCB_THAMKHAM();
        void Getthongtintaichinhpheduyet()
        {
            LaydanhsachPhieudieutri();
            LayLichsuBuongGiuong();
            LaythongtinPhieudieutri();
        }
        private void LaydanhsachPhieudieutri()
        {
            try
            {
                bool isAdmin = true;

                DataTable m_dtPhieudieutri = _KCB_THAMKHAM.NoitruTimkiemphieudieutriTheoluotkham(Utility.Bool2byte(isAdmin), "01/01/1900", objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan, "-1", -1);
                Utility.SetDataSourceForDataGridEx_Basic(grdPhieudieutri, m_dtPhieudieutri, false, true, "1=1",
                    NoitruPhieudieutri.Columns.NgayDieutri + " desc");
                grdPhieudieutri.MoveFirst();
            }
            catch (Exception ex)
            {
                log.Trace("Lỗi:" + ex.Message);
            }
        }
        private void LayLichsuBuongGiuong()
        {
            try
            {
                bool isAdmin = true;
                DataTable m_dtBuongGiuong = _KCB_THAMKHAM.NoitruTimkiemlichsuBuonggiuong(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan, "-1",-1);
                //Tính số lượng ngày nằm khoa cuối cùng nếu chưa xuất viện
                foreach (DataRow dr in m_dtBuongGiuong.Rows)
                {
                    if (Utility.Int32Dbnull(dr[NoitruPhanbuonggiuong.Columns.Id], 0) ==
                        Utility.Int32Dbnull(objLuotkham.IdRavien, 0))
                    {
                        NoitruPhanbuonggiuong objNoitruPhanbuonggiuong =
                            NoitruPhanbuonggiuong.FetchByID(Utility.Int64Dbnull(dr[NoitruPhanbuonggiuong.Columns.Id], 0));
                        if (objNoitruPhanbuonggiuong.NgayKetthuc == null)
                        {
                            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_TUDONG_TINHNGAYGIUONG", "0", false) == "1")
                            {
                                dr[NoitruPhanbuonggiuong.Columns.NgayKetthuc] = globalVariables.SysDate;
                                dr[NoitruPhanbuonggiuong.Columns.SoLuong] = THU_VIEN_CHUNG.SongayChuaRaVien(objNoitruPhanbuonggiuong.NgayVaokhoa, globalVariables.SysDate);
                                dr["thanh_tien"] = Utility.DecimaltoDbnull(dr[NoitruPhanbuonggiuong.Columns.DonGia], 0) *
                                                   Utility.DecimaltoDbnull(dr[NoitruPhanbuonggiuong.Columns.SoLuong], 0);
                            }

                        }
                    }
                }
                m_dtBuongGiuong.AcceptChanges();
                Utility.SetDataSourceForDataGridEx_Basic(grdBuongGiuong, m_dtBuongGiuong, false, true, "1=1",
                    NoitruPhanbuonggiuong.Columns.NgayVaokhoa + " desc");
                grdBuongGiuong.MoveFirst();
            }
            catch (Exception ex)
            {
                log.Trace("Lỗi:" + ex.Message);
            }
        }
        private void LaythongtinPhieudieutri()
        {
            bool IsAdmin = true;
            int id_phieudieutri = Utility.Int32Dbnull(grdPhieudieutri.GetValue("id_phieudieutri"),-1);
            DataSet dsData = _KCB_THAMKHAM.NoitruLaythongtinclsThuocTheophieudieutri(Utility.Int32Dbnull(objLuotkham.IdBenhnhan), objLuotkham.MaLuotkham, id_phieudieutri, 0, "-1");
           DataTable m_dtAssignDetail = dsData.Tables[0];
           DataTable m_dtDonthuoc = dsData.Tables[1];
           DataTable m_dtVTTH = dsData.Tables[2];
           DataTable m_dtGoidichvu = dsData.Tables[3];
           DataTable m_dtChandoanKCB = dsData.Tables[4];
           DataTable m_dtChedoDinhduong = dsData.Tables[5];
           DataTable m_dtChiphithem = dsData.Tables[6];
            //chkNgoaitru.Visible = m_dtAssignDetail.Select("noitru=0").Length > 0;



            Utility.SetDataSourceForDataGridEx(grdAssignDetail, m_dtAssignDetail, false, true, "",
                "stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");
            Utility.SetDataSourceForDataGridEx(grdGoidichvu, m_dtGoidichvu, false, true, "",
                "stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");
            Utility.SetDataSourceForDataGridEx(grdChiphithem, m_dtChiphithem, false, true, "",
                "stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");

            

            Utility.SetDataSourceForDataGridEx(grdPresDetail, m_dtDonthuoc, false, true, "",
               KcbDonthuocChitiet.Columns.SttIn);

            
            Utility.SetDataSourceForDataGridEx(grdVTTH, m_dtVTTH, false, true, "",
                KcbDonthuocChitiet.Columns.SttIn);

        }
        #endregion
        void txtTilemiengiamAll_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                decimal tile = Utility.DecimaltoDbnull(txtTilemiengiamAll.Text, 0);
                if (e.KeyCode == Keys.Enter && cmdThanhToan.Enabled)
                {
                   
                    string ask = string.Format("Bạn có chắc chắn muốn miễn giảm {0} % cho toàn bộ các dịch vụ đang được chọn?", Convert.ToInt16(tile).ToString());
                    if (!Utility.AcceptQuestion(ask, "Xác nhận miễn giảm cho các dịch vụ đang chọn", true)) return;
                    if (chkPercent.Checked)
                    {
                        if (tile > 100m)
                        {
                            Utility.ShowMsg("Tỉ lệ miễn giảm không được vượt quá 100%");
                            return;
                        }
                        foreach(GridEXRow _row in grdThongTinChuaThanhToan.GetCheckedRows())
                        {
                            _row.BeginEdit();
                            if (Utility.sDbnull(_row.Cells["trangthai_thanhtoan"].Value, "0") == "0" && _row.Cells["CHON"].Value.Equals(true))
                            {
                                _row.Cells["tien_chietkhau"].Value = Utility.DecimaltoDbnull(_row.Cells["TT_BN_KHONG_PHUTHU"].Value, 0) * tile / 100;
                                _row.Cells["tile_chietkhau"].Value = tile;
                            }
                            _row.Cells["thuc_thu"].Value = Utility.DecimaltoDbnull(_row.Cells["TT_BN_KHONG_PHUTHU"].Value, 0) - Utility.DecimaltoDbnull(_row.Cells["tien_chietkhau"].Value, 0);
                            _row.EndEdit();
                            //Cập nhật luôn vào bảng trong CSDL để in bảng kê chi phí cho người bệnh xem trước khi thanh toán
                            byte id_loaithanhtoan = Utility.ByteDbnull(_row.Cells["id_loaithanhtoan"].Value);
                            string kieu_chietkhau = "%";
                            decimal tile_chietkhau = Utility.DecimaltoDbnull(_row.Cells["tile_chietkhau"].Value, 0);
                            decimal tien_chietkhau = Utility.DecimaltoDbnull(_row.Cells["tien_chietkhau"].Value, 0);
                            long id_phieu = Utility.Int64Dbnull(_row.Cells["id_phieu"].Value);
                            long id_phieuchitiet = Utility.Int64Dbnull(_row.Cells["id_phieu_chitiet"].Value);
                           
                            CapnhatChietkhau_DonGia(0,id_loaithanhtoan, kieu_chietkhau, tile_chietkhau, tien_chietkhau, id_phieu, id_phieuchitiet);
                        }
                    }
                    else//Nhập tiền nếu vượt quá số tiền thì tự = số tiền
                    {
                        foreach (GridEXRow _row in grdThongTinChuaThanhToan.GetCheckedRows())
                        {
                            _row.BeginEdit();
                            if (Utility.sDbnull(_row.Cells["trangthai_thanhtoan"].Value, "0") == "0" && _row.Cells["CHON"].Value.Equals(true))
                            {
                                if (tile > Utility.DecimaltoDbnull(_row.Cells["TT_BN_KHONG_PHUTHU"].Value, 0))
                                {
                                    _row.Cells["tien_chietkhau"].Value = _row.Cells["TT_BN_KHONG_PHUTHU"].Value;
                                    _row.Cells["tile_chietkhau"].Value = 100;
                                }
                                else
                                {
                                    _row.Cells["tien_chietkhau"].Value = tile;
                                    _row.Cells["tile_chietkhau"].Value = (tile / Utility.DecimaltoDbnull(_row.Cells["TT_BN_KHONG_PHUTHU"].Value, 0)) * 100;
                                }
                            }
                            _row.Cells["thuc_thu"].Value = Utility.DecimaltoDbnull(_row.Cells["TT_BN_KHONG_PHUTHU"].Value, 0) - Utility.DecimaltoDbnull(_row.Cells["tien_chietkhau"].Value, 0);
                            _row.EndEdit();
                            //Cập nhật luôn vào bảng trong CSDL để in bảng kê chi phí cho người bệnh xem trước khi thanh toán
                            byte id_loaithanhtoan = Utility.ByteDbnull(_row.Cells["id_loaithanhtoan"].Value);
                            string kieu_chietkhau = "%";
                            decimal tile_chietkhau = Utility.DecimaltoDbnull(_row.Cells["tile_chietkhau"].Value, 0);
                            decimal tien_chietkhau = Utility.DecimaltoDbnull(_row.Cells["tien_chietkhau"].Value, 0);
                            long id_phieu = Utility.Int64Dbnull(_row.Cells["id_phieu"].Value);
                            long id_phieuchitiet = Utility.Int64Dbnull(_row.Cells["id_phieu_chitiet"].Value);
                            CapnhatChietkhau_DonGia(0, id_loaithanhtoan, kieu_chietkhau, tile_chietkhau, tien_chietkhau, id_phieu, id_phieuchitiet);
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
                SetSumTotalProperties();
            } 
        }
        void CapnhatChietkhau_DonGia(byte loai_capnhat, byte id_loaithanhtoan, string kieu_chietkhau, decimal tile_chietkhau, decimal tien_chietkhau, long id_phieu, long id_phieuchitiet)
        {
            try
            {
                StoredProcedure sp = SPs.SpUpdateThongtinchietkhauDongia(loai_capnhat,id_loaithanhtoan, kieu_chietkhau, tile_chietkhau, tien_chietkhau, id_phieu, id_phieuchitiet);
                sp.Execute();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        void MienGiam_bak(KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && cmdThanhToan.Enabled)
                {
                    decimal tile = Utility.DecimaltoDbnull(txtTilemiengiamAll.Text, 0);
                    if (chkPercent.Checked)
                    {
                        if (tile > 100m)
                        {
                            Utility.ShowMsg("Tỉ lệ miễn giảm không được vượt quá 100%");
                            return;
                        }
                        foreach (DataRow dr in m_dtChiPhiThanhtoan.Rows)
                        {
                            if (Utility.sDbnull(dr["trangthai_thanhtoan"], "0") == "0" && Utility.sDbnull(dr["colChon"], "0") == "1")
                            {
                                dr["tien_chietkhau"] = Utility.DecimaltoDbnull(dr["TT_BN_KHONG_PHUTHU"], 0) * tile / 100;
                                dr["tile_chietkhau"] = tile;
                            }
                        }
                    }
                    else//Nhập tiền nếu vượt quá số tiền thì tự = số tiền
                    {
                        foreach (DataRow dr in m_dtChiPhiThanhtoan.Rows)
                        {
                            if (Utility.sDbnull(dr["trangthai_thanhtoan"], "0") == "0" && Utility.sDbnull(dr["colChon"], "0") == "1")
                            {
                                if (tile > Utility.DecimaltoDbnull(dr["TT_BN_KHONG_PHUTHU"], 0))
                                {
                                    dr["tien_chietkhau"] = dr["TT_BN_KHONG_PHUTHU"];
                                    dr["tile_chietkhau"] = 100;
                                }
                                else
                                {
                                    dr["tien_chietkhau"] = tile;
                                    dr["tile_chietkhau"] = (tile / Utility.DecimaltoDbnull(dr["TT_BN_KHONG_PHUTHU"], 0)) * 100;
                                }
                            }
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
                SetSumTotalProperties();
            }
        }
        void optAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string RowFilter = "1=1";
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
                PropertyLib.SavePropertyV1(PropertyLib._ThanhtoanProperties);
            }
            catch (Exception ex)
            {


            }
        }
        void grdPhieuChi_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            if (e.Column.Key == KcbThanhtoan.Columns.NgayThanhtoan)
            {
                if (Utility.Coquyen("thanhtoan_suangaythanhtoan"))
                {
                    if (!Kiemtradieukienhuytt_theongay()) return;
                    //UpdatePaymentDate();
                    UpdatePhieuChiPaymentDate();
                }
                else
                {
                    e.Cancel = true;
                    Utility.ShowMsg("Bạn không có quyền cập nhật thông tin ngày tạo phiếu chi");
                }
            }
        }

       

        void cboPttt__OnEnterMe()
        {
            
        }
        private void cboPttt_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
            cboNganhang.Enabled = lstPTTT.Contains(Utility.sDbnull(cboPttt.SelectedValue, "-1"));
            if (!cboNganhang.Enabled) cboNganhang.SelectedIndex = -1;
        }
        void ucTamung1__OnChangedData()
        {
            SetSumTotalProperties();
        }

        void cboKieuin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._MayInProperties.KieuInBienlai = cboKieuin.SelectedIndex == 0 ? KieuIn.Innhiet : KieuIn.InLaser;
            PropertyLib.SavePropertyV1(PropertyLib._MayInProperties);
        }

        void cmdChuyenNguon_Click(object sender, EventArgs e)
        {
            if (Utility.AcceptQuestion("Bạn có chắc chắn muốn chuyển toàn bộ chi phí từ nguồn giới thiệu thành chiết khấu cho Bệnh nhân hay không?", "Xác nhận chuyển chi phí nguồn", true))
            {
                grdThongTinChuaThanhToan.SuspendLayout();
                foreach (GridEXRow row in grdThongTinChuaThanhToan.GetCheckedRows())
                {
                    if (Utility.sDbnull(row.Cells["tinh_chkhau"].Value, "0") == "1")
                    {
                        row.BeginEdit();
                        row.Cells["tile_chietkhau"].Value = Utility.Int16Dbnull(objLuotkham.ChiphiGioithieu, 0);
                        row.Cells["tien_chietkhau"].Value = Utility.DecimaltoDbnull(row.Cells["TT_BN_KHONG_PHUTHU"].Value, 0) * Utility.DecimaltoDbnull(objLuotkham.ChiphiGioithieu, 0) / 100;
                        row.Cells["ck_nguongt"].Value = 1;
                        row.EndEdit();
                    }

                    //grdThongTinChuaThanhToan.Refetch();
                    grdThongTinChuaThanhToan.ResumeLayout();
                    SetSumTotalProperties();
                }
            }
        }

        void grdPayment_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CallPhieuThu();
        }

        void grdThongTinChuaThanhToan_SelectionChanged(object sender, EventArgs e)
        {
            ChangeMenu(grdThongTinChuaThanhToan.CurrentRow);
        }

        bool UpdateTutuc(long id, int idLoaithanhtoan,byte tuTuc,ref decimal BNCT,ref decimal BHCT)
        {
            bool reval = false;
            try
            {
                switch (idLoaithanhtoan)
                {
                    case 0:
                    case 1:
                        KcbDangkyKcb objKcbDangkyKcb = KcbDangkyKcb.FetchByID(id);
                        if (objKcbDangkyKcb != null)
                        {
                            reval = TinhCLS.CapnhatTrangthaiTutuc(objKcbDangkyKcb, objLuotkham, false, tuTuc, Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0));
                        }
                        break;
                    case 8://Gói dịch vụ
                    case 11://Công tiêm chủng
                    case 9://Chi phí thêm
                    case 2://Phí CLS
                        KcbChidinhclsChitiet objChidinhclsChitiet = KcbChidinhclsChitiet.FetchByID(id);
                        if (objChidinhclsChitiet != null)
                        {
                           reval= TinhCLS.CapnhatTrangthaiTutuc(objChidinhclsChitiet, objLuotkham, false,tuTuc, Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0));
                        }
                        break;
                    case 3://Đơn thuốc ngoại trú,nội trú
                    case 5://Vật tư tiêu hao
                        KcbDonthuocChitiet objDonthuocChitiet = KcbDonthuocChitiet.FetchByID(id);
                        if (objDonthuocChitiet != null)
                        {
                            reval = TinhCLS.CapnhatTrangthaiTutuc(objDonthuocChitiet, objLuotkham, false, tuTuc, Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0));
                        }
                        break;
                    case 4://Giường bệnh
                        NoitruPhanbuonggiuong objPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(id);
                        if (objPhanbuonggiuong != null)
                        {
                            reval = TinhCLS.CapnhatTrangthaiTutuc(objPhanbuonggiuong, objLuotkham, false, tuTuc, Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0));
                        }
                        break;
                    case 10://Sổ khám
                        KcbDangkySokham objDangkySokham = KcbDangkySokham.FetchByID(id);
                        if (objDangkySokham != null)
                        {
                            reval = TinhCLS.CapnhatTrangthaiTutuc(objDangkySokham, objLuotkham, false, tuTuc, Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0));
                        }
                        break;
                    default:
                        break;
                }
                return reval;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return false;
            }
           
        }
        void UpdateAllValues()
        {
          decimal  BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
            foreach (DataRowView drv in m_dtChiPhiThanhtoan.DefaultView)
            {
                if (Utility.Int32Dbnull(drv["tinh_chiphi"], 0) == 1 && Utility.Int32Dbnull(drv["trangthai_huy"], 0) == 0)
                {
                    if (Utility.Int32Dbnull(drv[KcbChidinhclsChitiet.Columns.TuTuc], 0) == 0)
                    {
                        decimal BHCT = 0m;
                        if (objLuotkham.DungTuyen == 1)
                        {
                            BHCT = Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.DonGia], 0) * Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.TyleTt], 0) / 100 * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                        }
                        else
                        {
                            if (objLuotkham.TrangthaiNoitru <= 0)
                                BHCT = Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.DonGia], 0) * Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.TyleTt], 0) / 100 * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                            else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                                BHCT = Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.DonGia], 0) * Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.TyleTt], 0) / 100 * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                        }
                        decimal BNCT = Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) * Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.TyleTt], 0) / 100 - BHCT;
                        drv[KcbChidinhclsChitiet.Columns.BhytChitra] = BHCT;
                        drv[KcbChidinhclsChitiet.Columns.BnhanChitra] = BNCT;
                        drv["TT_TUTUC"] = 0;
                        drv["TT_BN_KHONG_TUTUC"] = Utility.Int32Dbnull(drv[KcbChidinhclsChitiet.Columns.BnhanChitra], 0) * Utility.Int32Dbnull(drv[KcbChidinhclsChitiet.Columns.SoLuong], 0);

                    }
                    else//Tự túc
                    {
                        drv[KcbChidinhclsChitiet.Columns.BhytChitra] = 0;
                        drv[KcbChidinhclsChitiet.Columns.BnhanChitra] =
                            Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) * Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.TyleTt], 0) / 100;
                        drv["TT_TUTUC"] = Utility.Int32Dbnull(drv[KcbChidinhclsChitiet.Columns.BnhanChitra], 0) * Utility.Int32Dbnull(drv[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                        drv["TT_BN_KHONG_TUTUC"] = 0;

                    }
                    drv["TT_BHYT"] = (Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.BhytChitra], 0)) * Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                    drv["TT_BN"] = (Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.PhuThu], 0)) * Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                    drv["TT"] = (Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.DonGia], 0) * Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.TyleTt], 0)/100 + Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.PhuThu], 0)) * Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                    drv["TT_PHUTHU"] = (Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.PhuThu], 0)) * Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                    drv["TT_KHONG_PHUTHU"] = Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.DonGia], 0) * Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.TyleTt], 0) / 100 * Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                    drv["TT_BN_KHONG_PHUTHU"] = Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.BnhanChitra], 0) * Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                }
            }
            SetSumTotalProperties();
        }
        void mnuTutuc_Click(object sender, EventArgs e)
        {
            try
            {
                decimal BNCT = 0m;
                decimal BHCT = 0m;
                bool foundNotValid = false;
                foreach (GridEXSelectedItem item in grdThongTinChuaThanhToan.SelectedItems)
                {
                    GridEXRow row=item.GetRow();
                    if (row.RowType == RowType.Record)
                    {
                        long Id = Utility.Int64Dbnull(Utility.GetValueFromGridColumn(row, "id_phieu_chitiet"), -1);
                        byte id_loaithanhtoan = Utility.ByteDbnull(Utility.GetValueFromGridColumn(row, "id_loaithanhtoan"), -1);
                        int TrangthaiThanhtoan = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan, "0"), 0);
                        if (mnuTutuc.Tag.ToString() == "0")//Tự túc
                        {
                            if (TrangthaiThanhtoan > 0)//Đã thanh toán
                            {
                                foundNotValid = true;
                                Utility.ShowMsg("Chỉ định bạn đang chọn đã thanh toán nên không cho phép thay đổi trạng thái tự túc. Đề nghị bạn kiểm tra lại");
                                return;
                            }
                            if (UpdateTutuc(Id, id_loaithanhtoan, (byte)1, ref BNCT, ref BHCT))
                            {
                                //grdThongTinChuaThanhToan.CurrentRow.BeginEdit();
                                //grdThongTinChuaThanhToan.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TuTuc].Value = 1;
                                //grdThongTinChuaThanhToan.CurrentRow.EndEdit();
                                //ChangeMenu(grdThongTinChuaThanhToan.CurrentRow);
                            }
                        }
                        else//Không tự túc
                        {
                            if (TrangthaiThanhtoan > 0)//Đã thanh toán
                            {
                                foundNotValid = true;

                                return;
                            }
                            if (UpdateTutuc(Id, id_loaithanhtoan, (byte)0, ref BNCT, ref BHCT))
                            {
                                //grdThongTinChuaThanhToan.CurrentRow.BeginEdit();
                                //grdThongTinChuaThanhToan.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TuTuc].Value = 0;
                                //grdThongTinChuaThanhToan.CurrentRow.EndEdit();
                                //ChangeMenu(grdThongTinChuaThanhToan.CurrentRow);
                            }
                        }
                    }
                }
                if(foundNotValid)
                    Utility.ShowMsg("Một số dịch vụ bạn đang chọn đã thanh toán nên không cho phép thay đổi trạng thái tự túc. Đề nghị bạn kiểm tra lại\n Nhấn OK để kết thúc việc cập nhật");
                GetData();
                
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }
        void ChangeMenu(GridEXRow _row)
        {
            mnuTutuc.Text = Utility.GetValueFromGridColumn(_row, KcbThanhtoanChitiet.Columns.TuTuc) == "1" ? "Giá đối tượng" : "Tự túc";
            mnuTutuc.Tag = Utility.GetValueFromGridColumn(_row, KcbThanhtoanChitiet.Columns.TuTuc);
        }
        void HoanUng()
        {
            try
            {
                objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text), Utility.DoTrim(txtPatient_Code.Text));
                if (cmdHoanung.Tag.ToString() == "0")//Hoàn ứng
                {
                    if (objLuotkham.TrangthaiNoitru <= 3)
                    {
                        Utility.ShowMsg("Bệnh nhân chưa được xác nhận chuyển thanh toán nội trú nên bạn không được phép hoàn ứng");
                        return;
                    }

                    string maphieu = THU_VIEN_CHUNG.SinhmaVienphi("HKQ");
                        SPs.NoitruHoanung(-1l,maphieu, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, dtPaymentDate.Value,
                            globalVariables.gv_intIDNhanvien, globalVariables.UserName,
                            Utility.Int32Dbnull(objLuotkham.IdKhoanoitru, -1), Utility.Int64Dbnull(objLuotkham.IdRavien, -1),
                            Utility.Int32Dbnull(objLuotkham.IdBuong, -1), Utility.Int32Dbnull(objLuotkham.IdGiuong, -1),
                            (byte)1,"TM","").Execute();
                        cmdHoanung.Tag = "1";
                        cmdHoanung.Text = "Hủy hoàn ứng";
                   
                }
                else
                {
                    if (objLuotkham.TrangthaiNoitru == 6)
                    {
                        Utility.ShowMsg("Bệnh nhân đã thanh toán nội trú nên bạn không được phép hủy hoàn ứng. Muốn hủy hoàn ứng phải hủy thanh toán trước");
                        return;
                    }
                    SPs.NoitruHuyhoanung(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, -1l, (byte)1).Execute();
                    cmdHoanung.Tag = "0";
                    cmdHoanung.Text = "Hoàn ứng";
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        void cmdHoanung_Click(object sender, EventArgs e)
        {
            HoanUng();
        }
        bool PhanboPhieuChi()
        {
            if (!Utility.isValidGrid(grdPhieuChi)) return false;
            v_Payment_ID = Utility.Int64Dbnull(grdPhieuChi.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1);
            string ma_pttt = Utility.sDbnull(grdPhieuChi.CurrentRow.Cells[KcbThanhtoan.Columns.MaPttt].Value, "TM");
            string ma_nganhang = Utility.sDbnull(grdPhieuChi.CurrentRow.Cells[KcbThanhtoan.Columns.MaNganhang].Value, "TM");
            frm_PhanbotientheoPTTT _PhanbotientheoPTTT = new frm_PhanbotientheoPTTT(v_Payment_ID, -1, -1, ma_pttt, ma_nganhang);
            _PhanbotientheoPTTT.objLuotkham = this.objLuotkham;
            _PhanbotientheoPTTT._OnChangePTTT += _PhanbotientheoPTTT__OnChangePTTT_PhieuChi;
            return _PhanbotientheoPTTT.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }

        void _PhanbotientheoPTTT__OnChangePTTT_PhieuChi(long id_thanhtoan, string ma_pttt, string ten_pttt, string ma_nganhang, string ten_nganhang)
        {
            try
            {
                DataRow dr = ((DataRowView)grdPhieuChi.CurrentRow.DataRow).Row;
                dr["ma_pttt"] = ma_pttt;
                dr["ten_pttt"] = ten_pttt;
                dr["ma_nganhang"] = ma_nganhang;
                dr["ten_nganhang"] = ten_nganhang;
                m_dtPhieuChi.AcceptChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        void Phanbo()
        {
            if (!Utility.isValidGrid(grdPayment)) return;
            v_Payment_ID = Utility.Int64Dbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1);
            string ma_pttt = Utility.sDbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.MaPttt].Value, "TM");
            string ma_nganhang = Utility.sDbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.MaNganhang].Value, "TM");
            frm_PhanbotientheoPTTT _PhanbotientheoPTTT = new frm_PhanbotientheoPTTT(v_Payment_ID,-1,-1, ma_pttt,ma_nganhang);
            _PhanbotientheoPTTT.objLuotkham = this.objLuotkham;
            _PhanbotientheoPTTT._OnChangePTTT += _PhanbotientheoPTTT__OnChangePTTT;
            _PhanbotientheoPTTT.ShowDialog();
        }

        void _PhanbotientheoPTTT__OnChangePTTT(long id_thanhtoan, string ma_pttt, string ten_pttt, string ma_nganhang, string ten_nganhang)
        {
            try
            {
                DataRow dr = ((DataRowView)grdPayment.CurrentRow.DataRow).Row;
                dr["ma_pttt"] = ma_pttt;
                dr["ten_pttt"] = ten_pttt;
                dr["ma_nganhang"] = ma_nganhang;
                dr["ten_nganhang"] = ten_nganhang;
                m_dtPayment.AcceptChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        
        void mnuPhanbotientheoPTTT_Click(object sender, EventArgs e)
        {
            if (!Utility.Coquyen("THANHTOAN_QUYEN_PHANBOPTTT"))
            {
                Utility.ShowMsg("Bạn không có quyền phân bổ PTTT(THANHTOAN_QUYEN_PHANBOPTTT). Yêu cầu quản trị hệ thống để được cấp quyền");
                return;
            }
            Phanbo();
        }
        void KeChiphithem()
        {
            DataRow[] arrDr = m_dtChiPhiThanhtoan.Select(KcbThanhtoanChitiet.Columns.IdLoaithanhtoan + "=9");
            if (arrDr.Length <= 0)
                ThemChiphithem();
            else
                CapnhatChiphithem(Utility.Int64Dbnull(arrDr[0]["id_phieu"], 0));
        }
        void cmdChiphithem_Click(object sender, EventArgs e)
        {
            KeChiphithem();
        }
        private void ThemChiphithem()
        {
            try
            {
                frm_KCB_CHIDINH_CLS frm = new frm_KCB_CHIDINH_CLS("CHIPHITHEM", 2);
                frm.txtAssign_ID.Text = "-100";
                frm.Exam_ID = -1;
                frm.objLuotkham = objLuotkham;
                frm.objBenhnhan = objBenhnhan;
                frm.objPhieudieutriNoitru = null;
                frm.m_eAction = action.Insert;
                frm.txtAssign_ID.Text = "-1";
                frm.noitru = v_bytNoitru;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    GetData();
                }
            }
            catch (Exception ex)
            {
                log.Trace("Loi:"+ ex.Message);
                //throw;
            }
            finally
            {
                txtMaLanKham.Focus();
                txtMaLanKham.SelectAll();
            }
        }
        private void CapnhatChiphithem(long idChidinh)
        {
            try
            {
                frm_KCB_CHIDINH_CLS frm = new frm_KCB_CHIDINH_CLS("CHIPHITHEM", 2);
                frm.txtAssign_ID.Text = "-100";
                frm.Exam_ID = -1;
                frm.objLuotkham = objLuotkham;
                frm.objBenhnhan = objBenhnhan;
                frm.objPhieudieutriNoitru = null;
                frm.m_eAction = action.Update;
                frm.txtAssign_ID.Text = idChidinh.ToString();
                frm.noitru = v_bytNoitru;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    GetData();
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                txtMaLanKham.Focus();
                txtMaLanKham.SelectAll();
            }
        }
        

        void mnuCapnhatPTTT_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdPayment)) return;
            
            CapnhatPTTT();
        }
        bool isValidPttt_Nganhang()
        {
            List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
            if (cboNganhang.Enabled && Utility.sDbnull(cboNganhang.SelectedValue, "") == "")
            {
                Utility.ShowMsg(string.Format("Bạn phải chọn ngân hàng khi chọn phương thức thanh toán {0}", cboPttt.Text));
                cboNganhang.Focus();
                return false;
            }
            return true;
        }
        void CapnhatPTTT()
        {
            try
            {
                
                Int32 IdThanhtoan=Utility.Int32Dbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1);
                KcbThanhtoan objThanhtoan = KcbThanhtoan.FetchByID(IdThanhtoan);
                DataRow[] arrDr = m_dtPayment.Select("id_thanhtoan=" + IdThanhtoan.ToString());
                if (arrDr.Length <= 0) return;
                //List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
                //if (cboNganhang.Enabled && cboNganhang.SelectedValue.ToString() == "-1")
                //{
                //    Utility.ShowMsg(string.Format("Bạn phải chọn ngân hàng khi chọn phương thức thanh toán {0}", cboPttt.Text));
                //    cboNganhang.Focus();
                //    return;
                //}
                if (!isValidPttt_Nganhang())
                    return;
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn thay đổi phương thức thanh toán từ {0} sang {1}", arrDr[0]["ten_pttt"], cboPttt.Text), "Xác nhận cập nhật PTTT", true))
                {
                    Utility.ShowMsg("Bạn vừa chọn hủy cập nhật phương thức thanh toán. Nhấn OK để kết thúc");
                    return;
                }
                List<string> lstPhanbo = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_BATBUOCPHANBO", false).Split(',').ToList<string>();
                DataTable dtPhanbo = _THANHTOAN.KcbThanhtoanLaydulieuphanbothanhtoanTheoPttt(IdThanhtoan,-1l,-1l).Tables[0];
                //Check nếu chọn Pttt=phân bổ và số dòng phân bổ <1 thì tự động hiển thị form phân bổ
                if (lstPhanbo.Contains(cboPttt.SelectedValue.ToString()))
                {
                    if (dtPhanbo.Select("so_tien>0").Length <= 1)
                        Phanbo();
                }
                else
                {
                    if (dtPhanbo.Select("so_tien>0").Length > 1)//Không phải chọn phân bổ mà số lượng dòng phân bổ >1 thì thông báo người dùng lựa chọn
                    {
                        if (Utility.AcceptQuestion("Lần phân bổ gần nhất bạn đang phân bổ tiền cho nhiều phương thức thanh toán. Trong khi hình thức thanh toán bạn đang chọn ({0}) không phải là phân bổ. Bạn có muốn hệ thống tự động chuyển tất cả số tiền sang hình thức vừa chọn {1} hay không?\n.Nhấn Yes để đồng ý. Nhấn No để xem lại thông tin phân bổ", "Cảnh báo và gợi ý", true))
                        {
                            Capnhatphanbo11(dtPhanbo, objThanhtoan, cboPttt.SelectedValue.ToString(), cboNganhang.Enabled ? Utility.sDbnull(cboNganhang.SelectedValue, "") : "");
                        }
                        else
                            Phanbo();
                    }
                    else if (dtPhanbo.Select("so_tien>0").Length == 1)//Thực hiện cập nhật luôn
                    {
                        Capnhatphanbo11(dtPhanbo, objThanhtoan, cboPttt.SelectedValue.ToString(), cboNganhang.Enabled ? Utility.sDbnull(cboNganhang.SelectedValue, "") : "");
                    }

                   
                }
                
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void Capnhatphanbo11(DataTable dtPhanbo, KcbThanhtoan objThanhtoan, string ma_pttt, string ma_nganhang)
        {
            decimal so_tien = Utility.DecimaltoDbnull(dtPhanbo.Compute("sum(so_tien)", "1=1"), 0);

            using (var scope = new TransactionScope())
            {
                using (var sh = new SharedDbConnectionScope())
                {

                    new Delete().From(KcbThanhtoanPhanbotheoPTTT.Schema).Where(KcbThanhtoanPhanbotheoPTTT.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();

                    KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(objThanhtoan.IdThanhtoan);
                    if (objPayment != null)
                    {
                        objPayment.MaPttt = ma_pttt;
                        objPayment.MaNganhang = ma_nganhang;
                        objPayment.MarkOld();
                        objPayment.IsNew = false;
                        objPayment.Save();
                        SPs.SpKcbThanhtoanPhanbotheoPTTTInsert(objThanhtoan.IdThanhtoan, -1l, -1l, objPayment.MaPttt, objPayment.MaNganhang,
          objThanhtoan.IdBenhnhan, objThanhtoan.MaLuotkham,
          objThanhtoan.NoiTru, so_tien, so_tien,
          objThanhtoan.NguoiTao, objThanhtoan.NgayTao, "", objThanhtoan.NgayTao, -1,0, (byte)1).Execute();

                        if (objPayment.NoiTru == 1)//Cập nhật cho bản ghi hoàn ứng
                        {
                            new Update(NoitruTamung.Schema)
                            .Set(NoitruTamung.Columns.MaPttt).EqualTo(objPayment.MaPttt)
                            .Set(NoitruTamung.Columns.MaNganhang).EqualTo(objPayment.MaNganhang)
                                .Where(NoitruTamung.Columns.IdThanhtoan).IsEqualTo(objPayment.IdThanhtoan)
                                .And(NoitruTamung.Columns.KieuTamung).IsEqualTo(1).Execute();
                        }
                    }
                }
                scope.Complete();
            }
            foreach (DataRow dr in m_dtPayment.Rows)
                if (dr["id_thanhtoan"].ToString() == objThanhtoan.IdThanhtoan.ToString())
                {
                    dr["ma_pttt"] = ma_pttt;
                    dr["ten_pttt"] = cboPttt.Text;
                    dr["ma_nganhang"] = ma_nganhang;
                    dr["ten_nganhang"] = ma_nganhang;
                }
            m_dtPayment.AcceptChanges();
            Utility.ShowMsg("Cập nhật thông tin hình thức thanh toán thành công");
        }
        
        void cmdCalculator_Click(object sender, EventArgs e)
        {
            Utility.OpenProcess("Calc");
        }

        void cmdSaveICD_Click(object sender, EventArgs e)
        {
            if (!isValidICD()) return;
            _THANHTOAN.UpdateIcd10(objLuotkham, Utility.DoTrim(txtICD.MyCode), Utility.DoTrim(txtICD.MyText));
            objLuotkham.MabenhChinh = Utility.DoTrim(txtICD.MyCode);
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
                Utility.ShowMsg("Mã bệnh chính bạn nhập không tồn tại trong hệ thống của chúng tôi\n Bạn có thể kiểm tra danh mục ICD 10 và thêm mã này vào.\nMời bạn nhấn OK để chọn mã bệnh từ danh mục");
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
                        //Utility.ShowMsg(string.Format("Seri đã được in cho bệnh nhân mã {0}. Mời bạn kiểm tra và chọn seri khác. Chú ý: Nếu bạn vẫn muốn in serie này thì cần tìm thanh toán của bệnh nhân ID: {1}- Mã: {2} để hủy serie đó", _HoadonLog.MaLuotkham, objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham));
                        //txtSerie.Focus();
                        //return false;
                        LoadInvoiceInfo();
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
            try
            {
                if (!blnLoaded) return;
                bool isCheck = e.CheckState == RowCheckState.Checked;
                foreach (GridEXRow r in grdThongTinChuaThanhToan.GetCheckedRows())
                {
                    r.BeginEdit();
                    if (Utility.sDbnull(r.Cells["trangthai_thanhtoan"].Value, "0") == "1")
                    {
                        r.IsChecked = false;
                    }
                    r.EndEdit();
                    ((DataRowView)r.DataRow).Row["colChon"] = r.IsChecked ? 1 : 0;
                    ((DataRowView)r.DataRow).Row["CHON"] = r.IsChecked ? 1 : 0;

                }
                foreach (GridEXRow r in grdThongTinChuaThanhToan.GetDataRows())
                {
                    if (!r.IsChecked)
                    {
                        ((DataRowView)r.DataRow).Row["colChon"] = r.IsChecked ? 1 : 0;
                        ((DataRowView)r.DataRow).Row["CHON"] = r.IsChecked ? 1 : 0;
                    }

                }

            }
            catch (Exception)
            {
            }
            finally
            {
                //Thay hàm TinhToanSoTienPhaithu= hàm SetSumTotalProperties để tính lại tiền BHYT chi trả
                SetSumTotalProperties();
                //TinhToanSoTienPhaithu();
                ModifyCommand();
            }
            
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
                        _row.Cells["ck_nguongt"].Value=0;
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
                    GetData();
                }
            }
        }

        void grdList_KeyDown(object sender, KeyEventArgs e)
        {
            if (Utility.isValidGrid(grdList) && e.KeyCode == Keys.Enter)
            {
                GetData();
            }
        }

        void tabThongTinCanThanhToan_SelectedTabChanged(object sender, Janus.Windows.UI.Tab.TabEventArgs e)
        {
            //tabThongTinThanhToan.Height = tabThongTinCanThanhToan.SelectedTab == TabpageCauhinh ? 0 : 168;
        }

        void cmdInphieuDCT_Click(object sender, EventArgs e)
        {
            InPhieuDCT();
        }

        void cmdInBienlaiTonghop_Click(object sender, EventArgs e)
        {
            int _Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
          new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(true, _Payment_ID,objLuotkham,1);
        }

        void cmdInBienlai_Click(object sender, EventArgs e)
        {
            int _Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
            byte kieuthanhtoan = Utility.ByteDbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.KieuThanhtoan].Value, 0);
            if (kieuthanhtoan == 0 || kieuthanhtoan == 5)
            {
                if (chkIntonghop.Visible && chkIntonghop.Checked)
                    new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(true, _Payment_ID, objLuotkham, 1);
                else
                    new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, _Payment_ID, objLuotkham, 1);
            }
            else//Phiếu chi
            {
                KcbThanhtoan objKcbThanhtoan = KcbThanhtoan.FetchByID(_Payment_ID);
                new INPHIEU_THANHTOAN_NGOAITRU().InBienlaiPhieuChi(chkIntonghop.Visible && chkIntonghop.Checked, _Payment_ID, objLuotkham, objKcbThanhtoan.NoiTru);
            }

            
           // new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, _Payment_ID, objLuotkham, 1);
            cbomayinphoiBHYT.Text = PropertyLib._MayInProperties.TenMayInBienlai;
        }
       
        void chkHienthichuathanhtoan_CheckedChanged(object sender, EventArgs e)
        {
            if (m_blnHasloaded && m_dtChiPhiThanhtoan != null && m_dtChiPhiThanhtoan.Columns.Count > 0 && m_dtChiPhiThanhtoan.Rows.Count > 0)
                m_dtChiPhiThanhtoan.DefaultView.RowFilter = "trangthai_huy=0" + (chkHienthiDichvusaukhinhannutthanhtoan.Checked ? " and trangthai_thanhtoan=0" : "");

          
           
        }

        void cmdSaveforNext_Click(object sender, EventArgs e)
        {
            PropertyLib.SavePropertyV1(PropertyLib._ThanhtoanProperties);
            PropertyLib.SavePropertyV1(PropertyLib._MayInProperties);
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

        void _CheckedChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
           
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
                dtPaymentDate.Enabled = Utility.Coquyen("thanhtoan_suangaythanhtoan");
                
                cbomayinhoadon.Text = PropertyLib._MayInProperties.TenMayInHoadon;
                cbomayinhoadonNhiet.Text = PropertyLib._MayInProperties.TenMayInBienlai_Nhiet;
                cbomayinphoiBHYT.Text = PropertyLib._MayInProperties.TenMayInBienlai;

                cboKieuin.SelectedIndex = PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? 0 : 1;
                //tabPageThongTinDaThanhToan.TabVisible = !PropertyLib._ThanhtoanProperties.AnTabDaThanhtoan;
                cmdHoanung.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("NGOAITRU_TUDONGHOANUNG_KHITHANHTOANNGOAITRU", "0", false) == "0";
                chkLayHoadon.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false)=="1";
                pnlSeri.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false)=="1";
                tabpageHoaDon.TabVisible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false)=="1";
                grdList.Height = PropertyLib._ThanhtoanProperties.ChieucaohienthiLuoidanhsachBNthanhtoan <= 0 ? 0 : PropertyLib._ThanhtoanProperties.ChieucaohienthiLuoidanhsachBNthanhtoan;
                grdPayment.RootTable.Columns[KcbThanhtoan.Columns.Serie].Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false)=="1";
               // if (!hasLoadedRedInvoice && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false)=="1") LoadInvoiceInfo();
                uiStatusBar1.Visible = !PropertyLib._ThanhtoanProperties.HideStatusBar;
                if (  THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false)=="1") LoadInvoiceInfo();
                bool RedInvoice= THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false)=="1";
                serperator1.Visible = serperator2.Visible = mnuInLaiBienLai.Visible =  RedInvoice;
                // mnuHuyHoaDon.Visible = RedInvoice; //huỷ số biên lai, huỷ cả thanh toán
                //mnuSuaSoBienLai.Visible = RedInvoice // sửa số biên lai
               mnuSuaSoBienLai.Visible = Utility.Coquyen("thanhtoan_quyen_suasobienlai") && RedInvoice;
               mnuLayhoadondo.Visible = Utility.Coquyen("thanhtoan_quyen_laysobienlai") &&  RedInvoice;
                TabPageTamung.TabVisible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_KICHHOAT_TAMUNG_NGOAITRU", "0", false) == "1";
               //cmdHoanung.Visible= lblThuathieu.Visible = txtThuathieu.Visible = TabPageTamung.TabVisible;
               lblTiennop.Text = TabPageTamung.TabVisible ? "Tổng tiền DV:" : "BN Nộp tiền";
               ////Bỏ 1 loạt các cấu hình bên dưới 10/05/2024
               //string HIENTHIPHANTICHGIA_TRENLUOI = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_HIENTHIPHANTICHGIA_TRENLUOI", "0", false);
               //grdThongTinChuaThanhToan.RootTable.Columns["TT_KHONG_PHUTHU"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";
               //grdThongTinChuaThanhToan.RootTable.Columns["TT_BN_KHONG_PHUTHU"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";
               //grdThongTinChuaThanhToan.RootTable.Columns["TT_BHYT"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";
               //grdThongTinChuaThanhToan.RootTable.Columns["TT_PHUTHU"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";
               //grdThongTinChuaThanhToan.RootTable.Columns["TT_BN"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";
               //grdThongTinChuaThanhToan.RootTable.Columns["bnhan_chitra"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";
               //grdThongTinChuaThanhToan.RootTable.Columns["phu_thu"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";
               //grdThongTinChuaThanhToan.RootTable.Columns["bhyt_chitra"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";

               //grdThongTinDaThanhToan.RootTable.Columns["TT_KHONG_PHUTHU"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";
               //grdThongTinDaThanhToan.RootTable.Columns["TT_BN_KHONG_PHUTHU"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";
               //grdThongTinDaThanhToan.RootTable.Columns["TT_BHYT"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";
               //grdThongTinDaThanhToan.RootTable.Columns["TT_PHUTHU"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";
               //grdThongTinDaThanhToan.RootTable.Columns["TT_BN"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";
               //grdThongTinDaThanhToan.RootTable.Columns["bnhan_chitra"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";
               //grdThongTinDaThanhToan.RootTable.Columns["phu_thu"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";
               //grdThongTinDaThanhToan.RootTable.Columns["bhyt_chitra"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";



               // grdThongTinChuaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TileChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
               // grdThongTinChuaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TienChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
               // grdThongTinChuaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.KieuChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
               // grdThongTinChuaThanhToan.RootTable.Columns["CHON"].EditType = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_CHOPHEP_CHONCHITIET_THANHTOAN", "0", false) == "1" ? EditType.CheckBox : EditType.NoEdit;
              
               // switch (PropertyLib._ThanhtoanProperties.CachChietkhau)
               // {
               //     case 0:
               //         grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TileChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
               //         grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TienChietkhau].EditType = EditType.NoEdit;
               //         break;
               //     case 1:
               //         grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TileChietkhau].Visible = false;
               //         grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TienChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
               //         grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TienChietkhau].EditType = EditType.TextBox;
               //         break;
               //     case 2:
               //         grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TileChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
               //         grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TileChietkhau].EditType = EditType.TextBox;
               //         grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TienChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
               //         grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TienChietkhau].EditType = EditType.TextBox;
               //         break;
               // }
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
                String pkInstalledPrinters;
                cbomayinphoiBHYT.Items.Clear();
                //cboPrinter.Items.Clear();
                cbomayinhoadon.Items.Clear();
                for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                {
                    pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                    cbomayinphoiBHYT.Items.Add(pkInstalledPrinters);
                    //cboPrinter.Items.Add(pkInstalledPrinters);
                    cbomayinhoadon.Items.Add(pkInstalledPrinters);
                    cbomayinhoadonNhiet.Items.Add(pkInstalledPrinters);
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

        private void setProperties()
        {
            try
            {
                foreach (Control control in pnlThongtintien.Controls)
                {
                    if (control is EditBox)
                    {
                        var txtFormantTongTien = new EditBox();
                        txtFormantTongTien = ((EditBox) (control));
                        if (txtFormantTongTien.Name != txtGhichu.Name)
                        {
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
                }
                foreach (Control control in pnlBHYTMoney.Controls)
                {
                    if (control is EditBox)
                    {
                        var txtFormantTongTien = new EditBox();
                        txtFormantTongTien = ((EditBox)(control));
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
            }
        }

        private void ModifyCommand()
        {
            try
            {
                TuybiennutchuyenCLS();
              
                cmdChiphithem.Enabled = Utility.isValidGrid(grdList) && objLuotkham != null;
                cmdChuyenDT.Enabled = Utility.isValidGrid(grdList) && objLuotkham != null;
                //Gọi cho khi bấm nút thanh toán
                Utility.SetMsg(lblTrangthainoitru, Utility.Laythongtintrangthainguoibenh(objLuotkham), false);
                if (cauhinhtaichinhduyet)
                    pnlTaichinhxetduyet.Enabled = objLuotkham.TrangthaiNoitru >= 3 && objLuotkham.TthaiThopNoitru >= 1 && objLuotkham.TrangthaiNoitru <= 5;//Chưa thanh toán
                else
                    pnlTaichinhxetduyet.Enabled = false;

                bool taichinhxetduyet = cauhinhtaichinhduyet && objLuotkham.TrangthaiNoitru == 5;
                cmdHuyThanhToan.Enabled=Utility.isValidGrid(grdList) && Utility.isValidGrid(grdPayment)  && objLuotkham != null;
                if (cauhinhtaichinhduyet)
                {
                    cmdThanhToan.Enabled = Utility.isValidGrid(grdList) && grdThongTinChuaThanhToan.GetCheckedRows().Length > 0 && objLuotkham != null && Utility.Byte2Bool(objLuotkham.TthaiThopNoitru) && objLuotkham.TrangthaiNoitru == 5; // Utility.isValidGrid(grdList) && grdThongTinChuaThanhToan.GetCheckedRows().Length > 0 && objLuotkham != null;
                }
                else
                {
                    cmdThanhToan.Enabled = Utility.isValidGrid(grdList) && grdThongTinChuaThanhToan.GetCheckedRows().Length > 0 && objLuotkham != null && Utility.Byte2Bool(objLuotkham.TthaiThopNoitru) && objLuotkham.TrangthaiNoitru == 4; // Utility.isValidGrid(grdList) && grdThongTinChuaThanhToan.GetCheckedRows().Length > 0 && objLuotkham != null;
                }
                cmdHoanung.Enabled = !cmdHuyThanhToan.Enabled && ucTamung1.grdTamung.GetDataRows().Length > 0 && objLuotkham != null;
                //cmdHuyThanhToan.Enabled = Utility.Coquyen("thanhtoan_huythanhtoan");
                cmdTraLaiTien.Enabled = Utility.isValidGrid(grdList) && grdThongTinDaThanhToan.GetCheckedRows().Length > 0 && objLuotkham != null;
                cmdInPhieuChi.Enabled = Utility.isValidGrid(grdList) && grdPhieuChi.GetDataRows().Length > 0 && objLuotkham != null;
                cmdInhoadon.Enabled = Utility.isValidGrid(grdList) && Utility.isValidGrid(grdPayment) && objLuotkham != null;
                cmdInBienlai.Visible = Utility.isValidGrid(grdList) && Utility.isValidGrid(grdPayment) && objLuotkham != null;
                pnlBHYT.Visible = Utility.isValidGrid(grdList) && objLuotkham.MaDoituongKcb == "BHYT" && objLuotkham != null;
                cmdInphoiBHYT.Visible = Utility.isValidGrid(grdList) && _chuathanhtoan <= 0 && Utility.DecimaltoDbnull(txtSoTienCanNop.Text) <= 0 && objLuotkham.MaDoituongKcb == "BHYT" && grdPayment.GetDataRows().Length > 0 && objLuotkham != null;
                cmdInphieuDCT.Visible = Utility.isValidGrid(grdList) && objLuotkham.MaDoituongKcb == "BHYT" && grdPayment.GetDataRows().Length > 0 && objLuotkham != null;
                //cmdInBienlaiTonghop.Visible = Utility.isValidGrid(grdList) && Utility.isValidGrid(grdPayment) && grdPayment.GetDataRows().Length > 1 && objLuotkham != null;//Điều chỉnh bằng dòng code bên dưới
                int TotalPayment = grdPayment.GetDataRows().Length;
                if (TotalPayment > 1 && objLuotkham != null)
                {
                    string _value = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_KIEUHIEUTHI_INBIENLAITONGHOP", "0", false);
                    if (_value == "0")
                    {
                        cmdInBienlaiTonghop.Visible = true;
                        chkIntonghop.Visible = false;
                    }
                    else
                    {
                        cmdInBienlaiTonghop.Visible = false;
                        chkIntonghop.Visible = true;
                    }

                }
                
                else
                {
                    cmdInBienlaiTonghop.Visible = false;
                    chkIntonghop.Visible = false;
                    chkIntonghop.Checked = false;
                }

                cmdChiphithem.Visible = cmdNhapDichVu.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_KECHIPHITHEM", "0", false) == "1";
                cmdCreatePres.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_KEDONTHUOC", "0", false) == "1";
                ModifyHoanUngButtons();
            }
            catch (Exception exception)
            {
                log.Trace("Loi: "+ exception);
                //throw;
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
        private void TimKiemBenhNhan()
        {
            try
            {
                string KieuTimKiem = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_NGOAITRU_KIEUTIMKIEMNGAY", "DANGKY", true);
                //if (radDangKyCLS.Checked) KieuTimKiem = "CLS";
                //if (radDangKyThuoc.Checked) KieuTimKiem = "THUOC";
                DateTime fromdate = dtFromDate.Value;
                DateTime enddate = dtToDate.Value; 
                int id_benhnhan = Utility.Int32Dbnull(txtID.Text, -1);
                string ma_lankham = Utility.sDbnull(txtMaLanKham.Text);
                string ten_benhnhan = Utility.sDbnull(txtTenBenhNhan.Text);
                if (_Malankham_keydown)//Nếu gõ theo mã BN-->Bỏ điều kiện tìm kiếm theo ngày
                {
                    fromdate = new DateTime(1990, 1, 1);
                    enddate = globalVariables.SysDate;
                    id_benhnhan = -1;
                    ten_benhnhan = "";
                }

                if (_ID_keydown)//Nếu gõ theo mã BN-->Bỏ điều kiện tìm kiếm theo ngày
                {
                    fromdate = new DateTime(1990, 1, 1);
                    enddate = globalVariables.SysDate;
                    ma_lankham = "";
                    ten_benhnhan = "";
                }
                //Nếu không gõ theo mã BN tức là tìm kiếm theo ngày. Hệ thống đưa ra lời cảnh báo tìm kiếm trong vòng 1 tháng
                if (!_ID_keydown && !_Malankham_keydown && !chkCreateDate.Checked)
                {
                    if (Utility.AcceptQuestion("Việc tìm kiếm không theo khoảng thời gian nào có thể gây mất thời gian. Do vậy hệ thống sẽ tự động tìm kiếm dữ liệu trong vòng 1 tháng. Bạn có muốn thực hiện tiếp(Nhấn Yes) hay hủy bỏ tìm kiếm(Nhấn No)", "Hỏi và xác nhận", true))
                    {
                        fromdate = globalVariables.SysDate.AddMonths(-1);
                        enddate = globalVariables.SysDate;
                    }
                    else
                        return;
                }

                //if (chkCreateDate.Checked)//Nếu chọn theo ngày
                //{
                //    if (_Malankham_keydown)//Nếu gõ theo mã BN-->Bỏ điều kiện tìm kiếm theo ngày
                //    {
                //        fromdate = new DateTime(1990, 1, 1);
                //        enddate = globalVariables.SysDate;
                //    }
                //    else//Nếu không gõ theo mã BN tức là tìm kiếm theo ngày
                //    {
                //        fromdate = dtFromDate.Value;
                //        enddate = dtToDate.Value;
                //    }
                //}
                //else//Không chọn theo ngày
                //{
                //    if (_Malankham_keydown)//Nếu gõ theo mã BN-->Bỏ điều kiện tìm kiếm theo ngày
                //    {
                //        fromdate = new DateTime(1990, 1, 1);
                //        enddate = globalVariables.SysDate;
                //    }
                //    else//Nếu không gõ theo mã BN tức là tìm kiếm theo ngày. Hệ thống đưa ra lời cảnh báo tìm kiếm trong vòng 1 tháng
                //    {
                //        if (Utility.AcceptQuestion("Việc tìm kiếm không theo khoảng thời gian nào có thể gây mất thời gian. Do vậy hệ thống sẽ tự động tìm kiếm dữ liệu trong vòng 1 tháng. Bạn có muốn thực hiện tiếp(Nhấn Yes) hay hủy bỏ tìm kiếm(Nhấn No)", "Hỏi và xác nhận", true))
                //        {
                //            fromdate = globalVariables.SysDate.AddMonths(-1);
                //            enddate = globalVariables.SysDate;
                //        }
                //        else
                //            return;
                //    }
                //}
                m_dtDataTimKiem =
                    _THANHTOAN.LayDsachBenhnhanThanhtoan(id_benhnhan,
                        ma_lankham,
                       ten_benhnhan,
                          fromdate,
                       enddate,
                        Utility.sDbnull(cboObjectType_ID.SelectedValue), 0, 1,
                        KieuTimKiem, globalVariables.MA_KHOA_THIEN, this.Args);
                Utility.AddColumToDataTable(ref m_dtDataTimKiem, "CHON", typeof(Int32));

                Utility.SetDataSourceForDataGridEx(grdList, m_dtDataTimKiem, true, true, "1=1", "");
                FilterThanhToan();
                ClearControl();
                ClearAll();//Reset toàn bộ dữ liệu trên các lưới nếu không có người bệnh nào
                //if (m_dtDataTimKiem.Rows.Count <= 0)
                //{
                objLuotkham = null;
                objBenhnhan = null;
                mnuUpdatePrice.Enabled = objLuotkham != null;
                //}
                grdList.MoveFirst();
                UpdateGroup();
                Utility.GonewRowJanus(grdList, KcbLuotkham.Columns.MaLuotkham, txtPatient_Code.Text);
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Messge:" + ex.Message);
            }
            finally
            {
                if (PropertyLib._ThanhtoanProperties.AutoSelectpatientAfterSearch && grdList.RowCount == 1)
                {
                    grdList.MoveFirst();
                    grdList_DoubleClick(grdList, new EventArgs());
                }
            }
        }
        void ClearAll()
        {
            try
            {
                if (m_dtDataTimKiem == null || m_dtDataTimKiem.Rows.Count <= 0)
                {
                    ClearControl();
                    grdThongTinChuaThanhToan.DataSource = null;
                    grdPhieuChi.DataSource = null;
                    grdDSKCB.DataSource = null;
                    grdPayment.DataSource = null;
                    grdThongTinDaThanhToan.DataSource = null;
                    ucTamung1.ChangePatients(null, "", v_bytNoitru);
                    ucThuchikhac1.ChangePatients(null, "");
                }
            }
            catch (Exception)
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
                GetData();
            }
        }
        void UpdateGroup()
        {
            try
            {
                var counts = m_dtDataTimKiem.AsEnumerable().GroupBy(x => x.Field<string>("ten_doituong_kcb"))
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
                txtThongtinMG.Clear();
            }
            catch (Exception)
            {
            }
        }
        void RestoredefaultPTTT()
        {
            txtGhichu.Clear();
            cboPttt.SelectedValue = THU_VIEN_CHUNG.Laygiatrimacdinh(dtPttt);
            List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
            cboNganhang.Enabled = lstPTTT.Contains(Utility.sDbnull(cboPttt.SelectedValue, "-1"));
            if (!cboNganhang.Enabled) cboNganhang.SelectedIndex = -1;
        }
        bool blnLoaded = true;
        bool cauhinhtaichinhduyet = false;
        private void GetData()
        {
            try
            {
                dtPaymentDate.Value = DateTime.Now;
                Utility.SetMsg(lblTrangthainoitru, "", false);
                blnLoaded = false;
                ClearControl();
                RestoredefaultPTTT();
                VisibleTaichinhduyet();
                Utility.FreeLockObject(txtPatient_Code.Text);
                txtPatient_Code.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                if (!Utility.CheckLockObject(txtPatient_Code.Text, "Thanh toán", "TT"))
                    return;
                txtPatient_ID.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                objBenhnhan = KcbDanhsachBenhnhan.FetchByID(txtPatient_ID.Text);
                objLuotkham = CreatePatientExam();
                mnuUpdatePrice.Enabled = objLuotkham != null;
                DataTable mDtThongTin =
                    m_dtDataTimKiem.Select("ma_luotkham = '" + txtPatient_Code.Text.Trim() + "' And id_benhnhan = " +
                                           Utility.Int32Dbnull(txtPatient_ID.Text, -1)).CopyToDataTable();
                    //_THANHTOAN.LaythongtinBenhnhan(txtPatient_Code.Text,
                    //Utility.Int32Dbnull(txtPatient_ID.Text, -1));
                gridExRow = grdList.GetRow();
                if (!Utility.isValidGrid(grdList) )
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
                        txtPatientName.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan], "") + " - " +
                                          Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.GioiTinh], "") + " - " +
                                          Utility.sDbnull(globalVariables.SysDate.Year -
                                                          Utility.Int32Dbnull(txtYear_Of_Birth.Text)) + " tuổi ";
                        txtObjectType_Name.Text = Utility.sDbnull(dr[DmucDoituongkcb.Columns.TenDoituongKcb], "");
                        txtSoBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.MatheBhyt], "");
                        switch (Utility.sDbnull(dr[KcbLuotkham.Columns.MaDoituongKcb], "DV"))
                        {
                            case "BHYT":
                                txtDTTT.Text = Utility.Int32Dbnull(dr[KcbLuotkham.Columns.DungTuyen], 0) == 1 ? "Đúng tuyến" : "Trái tuyến";
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
                    if (objLuotkham != null)
                    {
                        if (objLuotkham.NgayRavien != null)
                        {
                            dtpNgayravien.Value = objLuotkham.NgayRavien.Value;
                        }
                        else
                        {
                        }
                        Utility.SetMsg(lblTrangthainoitru, Utility.Laythongtintrangthainguoibenh(objLuotkham),false);
                        txtThongtinMG.Text = objLuotkham.ThongtinMg;
                        if (LoaiForm != "THUNGAN" && cauhinhtaichinhduyet)
                            Getthongtintaichinhpheduyet();
                        //if (objLuotkham.TrangthaiNoitru > 0)
                        //{
                        //    ucTamung1.pnlFunctions.Enabled = objLuotkham.TrangthaiNoitru<=6;
                        //    ucThuchikhac1.pnlFunctions.Enabled = objLuotkham.TrangthaiNoitru <= 6;
                        //    Utility.SetMsg(lblTrangthainoitru, string.Format("Chú ý: Người bệnh {0} đã nhập viện điều trị nên bạn không thể thao tác thanh toán ngoại trú", txtPatientName.Text), true);
                        //}
                        if (objLuotkham.TrangthaiNoitru == 3) optKhoachuaduyet.Checked = true;
                        else if (objLuotkham.TrangthaiNoitru == 4) optKhoadaduyet.Checked = true;
                        else if (objLuotkham.TrangthaiNoitru == 5) optChuyenthanhtoan.Checked = true;
                        else if (objLuotkham.TrangthaiNoitru == 6) optChuyenthanhtoan.Checked = true;
                        if (cauhinhtaichinhduyet)
                            pnlTaichinhxetduyet.Enabled = objLuotkham.TrangthaiNoitru >= 3 && objLuotkham.TthaiThopNoitru>=1 && objLuotkham.TrangthaiNoitru<=5;//Chưa thanh toán
                        else
                            pnlTaichinhxetduyet.Enabled = false;
                        cmdCapnhatngayBHYT.Visible =
                            THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) && Utility.Coquyen("thanhtoan_suathongtintheBHYT");
                        txtICD.ReadOnly = !Utility.Coquyen("thanhtoan_nhapICD10");
                        cmdSaveICD.Visible = Utility.Coquyen("thanhtoan_nhapICD10");                       
                        txtObjectType_Code.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.MaDoituongKcb], "");
                        txtPtramBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.PtramBhyt], "0")+"%";
                        txtICD.SetCode(Utility.sDbnull(dr[KcbLuotkham.Columns.MabenhChinh], "")) ;
                        txtDiaChi.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.DiaChi], "");
                        txtDiachiBHYT.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.DiachiBhyt], "");
                        toolTip1.SetToolTip(lblBHYT, Utility.DoTrim(txtDiachiBHYT.Text));
                        if (TabPageTamung.TabVisible)//Các nút hoàn ứng xử lý ở hàm Modifycommand
                        {

                            ucTamung1.ChangePatients(objLuotkham, "LYDOTAMUNG_NOITRU", v_bytNoitru);
                            ucThuchikhac1.ChangePatients(objLuotkham, "LYDOTHU");
                           
                           
                        }
                        ucThuchikhac1.ChangePatients(objLuotkham, "LYDOTHU");
                        ucMiengiamkhac1.ChangePatients(objLuotkham, "LYDOCHIETKHAU", txtSoTienCanNop.Text, txtDachietkhau.Text,v_bytNoitru);
                    }
                    KiemTraDaInPhoiBhyt();
                    GetDataChiTiet();
                    LaydanhsachLichsuthanhtoan_phieuchi();
                    
                }
                
            }
            catch (Exception ex)
            { 
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
            finally
            {
                GetThongtincanhbao(Utility.Int32Dbnull(txtPatient_ID.Text, -1));
                ModifyCommand();

                if (PropertyLib._ThanhtoanProperties.AutoTab) 
                    tabThongTinCanThanhToan.SelectedIndex = 0;
                blnLoaded = true;
            }
        }

        private KcbPhieuDct objPhieuDct;
        private void KiemTraDaInPhoiBhyt()
        {
            SqlQuery sqlQuery = new Select().From(KcbPhieuDct.Schema)
                .Where(KcbPhieuDct.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(txtPatient_Code.Text))
                .And(KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtPatient_ID.Text))
                .And(KcbPhieuDct.Columns.KieuThanhtoan).IsEqualTo(Utility.Int32Dbnull(KieuThanhToan.NgoaiTru));
            if (sqlQuery.GetRecordCount() > 0)
            {
                pnlSuangayinphoi.Visible = true;
                objPhieuDct = new KcbPhieuDct();
                objPhieuDct = sqlQuery.ExecuteSingle<KcbPhieuDct>();
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
                    if (objPhieuDct.TrangthaiXml == 1)
                    {
                        Utility.SetMsg(lblMessage,
                       string.Format("Đã tạo dữ liệu BHYT {0}, vào lúc: {1}", objPhieuDct.NguoiTao,
                           objPhieuDct.NgayTao), false);
                        cmdChuyenGiamDinh.Enabled = false;
                    }
                    if (objPhieuDct.TrangthaiXml > 1)
                    {
                        Utility.SetMsg(lblMessage,
                       string.Format("Đã chuyển giám định {0}, vào lúc: {1}", objPhieuDct.NguoiTao,
                           objPhieuDct.NgayTao), false);
                        cmdChuyenGiamDinh.Enabled = false;
                    }
                    else
                    {
                        cmdChuyenGiamDinh.Enabled = true;
                    }
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
                    //lblMsg.Font = Color.Red;
                }
                else
                {
                    if (cmdThanhToan.Enabled == false)
                    {
                        lblMessage.Visible = true;
                        Utility.SetMsg(lblMessage, string.Format("Bệnh nhân dịch vụ đã thanh toán"), false);
                    }
                    else
                    {
                        lblMessage.Visible = true;
                        Utility.SetMsg(lblMessage, string.Format("Bệnh nhân dịch vụ chưa thanh toán"), true);
                    }
                    lblMessage.Visible = false;
                }

            }
        }

        private void TuybiennutchuyenCLS()
        {
            try
            {
                if (objLuotkham != null)
                {
                    var q = from p in grdThongTinChuaThanhToan.GetDataRows().AsEnumerable()
                            where Utility.Int32Dbnull(p.Cells["trangthai_chuyencls"].Value, -1) == 1
                            && Utility.Int32Dbnull(p.Cells["id_loaithanhtoan"].Value, -1) == 2
                            select p;

                    cmdDungChuyenCLS.Enabled = q.Count()>0;
                    q = from p in grdThongTinChuaThanhToan.GetDataRows().AsEnumerable()
                        where Utility.Int32Dbnull(p.Cells["trangthai_chuyencls"].Value, -1) == 0
                        && Utility.Int32Dbnull(p.Cells["id_loaithanhtoan"].Value, -1) == 2
                        select p;
                    cmdChuyenCLS.Enabled = q.Count() > 0;
                }
            }
            catch (Exception ex)
            {
                cmdChuyenCLS.Enabled = true;
                cmdDungChuyenCLS.Enabled = true;
            }
        }

        private void GetDataChiTiet()
        {
            try
            {
                m_dtChiPhiThanhtoan =
                    _THANHTOAN.LayThongtinChuaThanhtoan(txtPatient_Code.Text, Utility.Int32Dbnull(txtPatient_ID.Text), v_bytNoitru,
                        globalVariables.MA_KHOA_THIEN, Utility.sDbnull(txtObjectType_Code.Text),lst_IDLoaithanhtoan);
                //flowMiengiam.Enabled = m_dtChiPhiThanhtoan.Rows.Count > 0;
                LayDSChiPhiThanhToan();
                Utility.AddColumToDataTable(ref m_dtChiPhiThanhtoan, "colCHON", typeof(byte));
                Utility.AddColumToDataTable(ref m_dtChiPhiThanhtoan, "ck_nguongt", typeof(byte));
                m_dtChiPhiThanhtoan.AcceptChanges();
                    Utility.SetDataSourceForDataGridEx(grdThongTinChuaThanhToan, m_dtChiPhiThanhtoan, false, true, "trangthai_huy=0" + (PropertyLib._ThanhtoanProperties.Hienthidichvuchuathanhtoan ? " and trangthai_thanhtoan=0" : ""), "");
                GetChiPhiDaThanhToan();
                UpdateTuCheckKhiChuaThanhToan();
                SetSumTotalProperties();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ex.Message);
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
                    DataRow[] arrDr = m_dtChiPhiThanhtoan.Select("trangthai_thanhtoan = 0 AND trangthai_huy = 0 AND id_loaithanhtoan =" +grd.Cells["MA"].Value);
                     int record = arrDr.Length;
                    grd.IsChecked = record > 0;
                    grd.EndEdit();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }

        private void GetChiPhiDaThanhToan()
        {
            try
            {
                m_dtChiPhiDaThanhToan =
                    _THANHTOAN.LayThongtinDaThanhtoan(txtPatient_Code.Text, Utility.Int32Dbnull(txtPatient_ID.Text), v_bytNoitru, lst_IDLoaithanhtoan);
                Utility.SetDataSourceForDataGridEx(grdThongTinDaThanhToan, m_dtChiPhiDaThanhToan, false, true, "1=1", "");
                if (m_dtChiPhiDaThanhToan.Rows.Count > 0)
                {
                    dtPaymentDate.ReadOnly = true;
                    //cboPttt.Enabled = false;
                    //dtPaymentDate.Value = Convert.ToDateTime(m_dtChiPhiDaThanhToan.Rows[0]["ngay_thanhtoan"].ToString());
                }
                else
                {
                    dtPaymentDate.ReadOnly = false;
                    //cboPttt.Enabled = true;
                    dtPaymentDate.Value = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        private void UpdateTuCheckKhiChuaThanhToan()
        {
            try
            {
                int pk = 1;
                foreach (GridEXRow gridExRow in grdThongTinChuaThanhToan.GetDataRows())
                {
                    gridExRow.BeginEdit();
                    gridExRow.Cells["privatekey"].Value = pk;
                    pk++;
                    if (gridExRow.RowType == RowType.Record)
                    {
                        gridExRow.IsChecked = Utility.Int32Dbnull(gridExRow.Cells["trangthai_thanhtoan"].Value, 0) == 0
                                              && Utility.Int32Dbnull(gridExRow.Cells["trangthai_huy"].Value, 0) == 0;
                        gridExRow.Cells["colChon"].Value = gridExRow.IsChecked ? 1 : 0;
                    }
                    gridExRow.EndEdit();
                }
                grdThongTinChuaThanhToan.UpdateData();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        decimal _chuathanhtoan = 0m;
        private void SetSumTotalProperties()
        {
            try
            {
                if (objLuotkham == null)
                {
                    objLuotkham = CreatePatientExam();
                }
                string errMsg = "";
                decimal newBhyt = Utility.DecimaltoDbnull(txtPtramBHChiTra.Text, 0);
                _THANHTOAN.TinhlaitienBhyTtruocThanhtoan(m_dtChiPhiThanhtoan, TaophieuThanhtoan(), objLuotkham, Taodulieuthanhtoanchitiet(ref errMsg), ref newBhyt);
                txtPtramBHChiTra.Text = newBhyt.ToString();
                decimal tt = 0m;
                decimal tt_bhyt = 0m;
                decimal tt_bhyt_cct = 0m;
                decimal tt_bn_cct = 0m;
                decimal tt_bn_ttt = 0m;
                decimal TT_BN = 0m;
                decimal tt_phuthu = 0m;
                decimal tt_tutuc = 0m;
                _chuathanhtoan = 0m;
                foreach (DataRowView drv in m_dtChiPhiThanhtoan.DefaultView)
                {
                    if (Utility.Int32Dbnull(drv["tinh_chiphi"], 0) == 1 && Utility.Int32Dbnull(drv["trangthai_huy"], 0) == 0 && Utility.Int32Dbnull(drv["tthai_tamthu"], 0) == 0)
                    {
                        tt += Utility.DecimaltoDbnull(drv["TT"], 0);
                        tt_bhyt += Utility.DecimaltoDbnull(drv["TT_BHYT"], 0);
                        tt_bhyt_cct += Utility.DecimaltoDbnull(drv["tt_bhyt_cct"], 0);
                        tt_bn_cct += Utility.DecimaltoDbnull(drv["tt_bn_cct"], 0);
                        tt_bn_ttt += Utility.DecimaltoDbnull(drv["tt_bn_ttt"], 0);
                        TT_BN += Utility.DecimaltoDbnull(drv["TT_BN"], 0);
                        if (Utility.Int32Dbnull(drv["trangthai_thanhtoan"], 0) == 0) _chuathanhtoan += Utility.DecimaltoDbnull(drv["TT_BN"], 0);
                        tt_phuthu += Utility.DecimaltoDbnull(drv["TT_PHUTHU"], 0);
                        if (Utility.Int32Dbnull(drv["tu_tuc"], 0) == 1) tt_tutuc += Utility.DecimaltoDbnull(drv["TT_TUTUC"], 0);

                    }
                }
                txtTongChiPhi.Text = Utility.sDbnull(tt);
                txtTongtienDCT.Text = !THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) ? "0" : Utility.sDbnull(tt_bhyt);
                txtPhuThu.Text = Utility.sDbnull(tt_phuthu);
                if (Utility.DecimaltoDbnull(tt_tutuc) > 0)
                {
                    txtTuTuc.BackColor = Color.Yellow;
                }
                else
                {
                    txtTuTuc.BackColor = Color.Honeydew;
                }
                txtTuTuc.Text = Utility.sDbnull(tt_tutuc);
                txtBHYT_CCT.Text = Utility.sDbnull(tt_bhyt_cct, "0");
                txtBN_CCT.Text = Utility.sDbnull(tt_bn_cct, "0");
                txtBN_TTT.Text = Utility.sDbnull(tt_bn_ttt, "0");
                txtBNPhaiTra.Text = Utility.sDbnull(TT_BN);
                TinhToanSoTienPhaithu();
                ThongtinTamung();
                ModifyCommand();

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        bool ketchuyen_tamthu = false;
        void ThongtinTamung()
        {
            SysSystemParameter _objLabel = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("THANHTOAN_THUATHIEU").ExecuteSingle<SysSystemParameter>();
            decimal tongTamung = 0;
            txtTongTU.Clear();
            if (TabPageTamung.TabVisible)
            {
                if (ucTamung1.m_dtTamung != null)
                {
                    tongTamung = Utility.DecimaltoDbnull(ucTamung1.m_dtTamung.Compute("SUM(so_tien)", "trang_thai=0"), 0);

                }
            }
            else
            {
                lblTiennop.Text = _objLabel == null ? @"BN Nộp tiền" : _objLabel.SValue.Split(';')[0];
            }
            if (ketchuyen_tamthu)
            {
                //Lấy thông tin tạm thu
                DataTable dtTamthu = new Select().From(KcbPhieuthu.Schema)
                    .Where(KcbPhieuthu.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(KcbPhieuthu.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .And(KcbPhieuthu.Columns.LoaiPhieuthu).IsEqualTo(5)
                    .And(KcbPhieuthu.Columns.TrangThai).IsEqualTo(0)
                    .ExecuteDataSet().Tables[0];
                decimal tt_tamthu_chuaketchuyen = Utility.DecimaltoDbnull(dtTamthu.Compute("sum(so_tien)", "1=1"));
                tongTamung += tt_tamthu_chuaketchuyen;
            }
            txtTongTU.Text = tongTamung.ToString();
            if (Math.Abs(tongTamung) != 0)
            {
                decimal chenhlech = _chuathanhtoan - tongTamung;
                if (chenhlech > 0)
                {
                    lblThuathieu.Text = _objLabel == null ? @"BN Nộp tiền" : _objLabel.SValue.Split(';')[0];
                    lblThuathieu.ForeColor = Color.DarkBlue;
                    txtThuathieu.Text = chenhlech.ToString();
                }
                else
                {
                    lblThuathieu.ForeColor = Color.DarkRed;
                    lblThuathieu.Text = _objLabel == null ? @"Trả lại BN" : _objLabel.SValue.Split(';')[1];
                    txtThuathieu.Text = Math.Abs(chenhlech).ToString();
                }
            }
            if (tongTamung == 0)
            {
                lblThuathieu.Text = _objLabel == null ? @"BN Nộp tiền" : _objLabel.SValue.Split(';')[0];
                txtThuathieu.Text = txtSoTienCanNop.Text;
                lblThuathieu.ForeColor = Color.DarkBlue;
            }

            //SysSystemParameter _objLabel = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("THANHTOAN_THUATHIEU").ExecuteSingle<SysSystemParameter>();
            //decimal tongTamung = 0;
            //txtTongTU.Clear();
            //if (TabPageTamung.TabVisible)
            //{
            //    if (ucTamung1.m_dtTamung != null)
            //    {
            //        tongTamung = Utility.DecimaltoDbnull(ucTamung1.m_dtTamung.Compute("SUM(so_tien)", "trang_thai=0"), 0);
            //        txtTongTU.Text = tongTamung.ToString();
            //        if (Math.Abs(tongTamung) != 0)
            //        {
            //            decimal chenhlech = _chuathanhtoan - tongTamung;
            //            if (chenhlech > 0)
            //            {
            //                lblThuathieu.Text = _objLabel == null ? @"BN Nộp tiền" : _objLabel.SValue.Split(';')[0];
            //                txtThuathieu.Text = chenhlech.ToString();
            //            }
            //            else
            //            {
            //                lblThuathieu.Text = _objLabel == null ? @"Trả lại BN" : _objLabel.SValue.Split(';')[1];
            //                txtThuathieu.Text = Math.Abs(chenhlech).ToString();
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    lblTiennop.Text = _objLabel == null ? @"BN Nộp tiền" : _objLabel.SValue.Split(';')[0];
            //}
            //if (tongTamung == 0)
            //{
            //    lblThuathieu.Text = _objLabel == null ? @"BN Nộp tiền" : _objLabel.SValue.Split(';')[0];
            //    txtThuathieu.Text = txtSoTienCanNop.Text;
            //}
        }
        private void TinhToanSoTienPhaithu()
        {
            try
            {
                List<GridEXRow> query = (from thanhtoan in grdThongTinChuaThanhToan.GetCheckedRows()
                                         where Utility.Int32Dbnull(thanhtoan.Cells["trangthai_huy"].Value) == 0
                                               && Utility.Int32Dbnull(thanhtoan.Cells["trangthai_thanhtoan"].Value) == 0
                                               && Utility.Int32Dbnull(thanhtoan.Cells["tthai_tamthu"].Value) == 0
                                         select thanhtoan).ToList();

                List<GridEXRow> query1 = (from thanhtoan in grdThongTinChuaThanhToan.GetCheckedRows()
                                         where Utility.Int32Dbnull(thanhtoan.Cells["trangthai_huy"].Value) == 0
                                          && Utility.Int32Dbnull(thanhtoan.Cells["tthai_tamthu"].Value) == 0
                                         select thanhtoan).ToList();
               
                decimal thanhtien = query.Sum(c => Utility.DecimaltoDbnull(c.Cells["TT_BN"].Value));
                decimal Chietkhauchitiet = query1.Sum(c => Utility.DecimaltoDbnull(c.Cells["tien_chietkhau"].Value));
                txtSoTienCanNop.Text = Utility.sDbnull(thanhtien - Chietkhauchitiet);
                _chuathanhtoan = thanhtien - Chietkhauchitiet;
                txtTienChietkhau.Text = Utility.sDbnull( Chietkhauchitiet);
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        private GridEXColumn getGridExColumn(GridEX gridEx, string colName)
        {
            return gridEx.RootTable.Columns[colName];
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        void LoadUserConfigs()
        {
            chkHoixacnhanhuythanhtoan.Checked = Utility.getUserConfigValue(chkHoixacnhanhuythanhtoan.Tag.ToString(), Utility.Bool2byte(chkHoixacnhanhuythanhtoan.Checked)) == 1;
            chkHienthiDichvusaukhinhannutthanhtoan.Checked = Utility.getUserConfigValue(chkHienthiDichvusaukhinhannutthanhtoan.Tag.ToString(), Utility.Bool2byte(chkHienthiDichvusaukhinhannutthanhtoan.Checked)) == 1;
            chkHoixacnhanthanhtoan.Checked = Utility.getUserConfigValue(chkHoixacnhanthanhtoan.Tag.ToString(), Utility.Bool2byte(chkHoixacnhanthanhtoan.Checked)) == 1;
            chkRestoreDefaultPTTT.Checked = Utility.getUserConfigValue(chkRestoreDefaultPTTT.Tag.ToString(), Utility.Bool2byte(chkRestoreDefaultPTTT.Checked)) == 1;
            chkPreviewHoadon.Checked = Utility.getUserConfigValue(chkPreviewHoadon.Tag.ToString(), Utility.Bool2byte(chkPreviewHoadon.Checked)) == 1;
            chkPreviewInBienlai.Checked = Utility.getUserConfigValue(chkPreviewInBienlai.Tag.ToString(), Utility.Bool2byte(chkPreviewInBienlai.Checked)) == 1;
            chkPreviewInphoiBHYT.Checked = Utility.getUserConfigValue(chkPreviewInphoiBHYT.Tag.ToString(), Utility.Bool2byte(chkPreviewInphoiBHYT.Checked)) == 1;
            chkTudonginhoadonsauthanhtoan.Checked = Utility.getUserConfigValue(chkTudonginhoadonsauthanhtoan.Tag.ToString(), Utility.Bool2byte(chkTudonginhoadonsauthanhtoan.Checked)) == 1;
            chkViewtruockhihuythanhtoan.Checked = Utility.getUserConfigValue(chkViewtruockhihuythanhtoan.Tag.ToString(), Utility.Bool2byte(chkViewtruockhihuythanhtoan.Checked)) == 1;
        }
        private string PathXml = "";
        private void frm_THANHTOAN_NOITRU_V1_Load(object sender, EventArgs e)
        {
            mnuFixError.Enabled = globalVariables.isSuperAdmin;
            LoadUserConfigs();
            pnlTangGiamDonGia.Enabled = Utility.Coquyen("thanhtoan_tanggiam_tile_dongia");
            InitData();
            log.Trace("InitData finished");
            LoadPtttNganhang();
            InitPTTTColumns();
            setProperties();
            log.Trace("Init,setProperties finished");
            LoadPrinter();
            log.Trace("LoadPrinter finished");
            AutocompleteIcd();
            log.Trace("AutocompleteIcd finished");
            LoadInvoiceInfo();
            log.Trace("LoadInvoiceInfo finished");
            ClearControl();
            optKhoachuaduyet.Enabled = Utility.Coquyen("NOITRU_QUYEN_HUYTONGHOPRAVIEN");
            if (PropertyLib._ThanhtoanProperties.SearchWhenStart) cmdSearch_Click(cmdSearch, e);
            log.Trace("cmdSearch_Click finished");
            m_blnHasloaded = true;
            txtMaLanKham.Focus();
            txtMaLanKham.SelectAll();
            ModifyCommand();
            if (File.Exists(Application.StartupPath + "\\PathXML.txt"))
            {
                string readText = File.ReadAllText(Application.StartupPath + "\\PathXML.txt");
                PathXml = readText.Trim();
            }
            else
            {
                PathXml = @"C:";
            }
        }
        DataTable dtPttt = new DataTable();
        DataTable dtNganhang = new DataTable();
        void LoadPtttNganhang()
        {
            DataTable dtData = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { "PHUONGTHUCTHANHTOAN", "NGANHANG" }, true);
            dtPttt = THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, "PHUONGTHUCTHANHTOAN");
            dtNganhang = THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, "NGANHANG");
            cboPttt.DataSource = dtPttt;
            cboPttt.ValueMember = DmucChung.Columns.Ma;
            cboPttt.DisplayMember = DmucChung.Columns.Ten;
            cboNganhang.DataSource = dtNganhang;
            cboNganhang.ValueMember = DmucChung.Columns.Ma;
            cboNganhang.DisplayMember = DmucChung.Columns.Ten;

            cboPttt.SelectedValue = THU_VIEN_CHUNG.Laygiatrimacdinh(dtPttt);
            cboNganhang.SelectedValue = THU_VIEN_CHUNG.Laygiatrimacdinh(dtNganhang);
        }
        void AutocompleteIcd()
        {
            try
            {
                if (globalVariables.gv_dtDmucBenh == null) return;
                if (!globalVariables.gv_dtDmucBenh.Columns.Contains("ShortCut"))
                    globalVariables.gv_dtDmucBenh.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                txtICD.Init(globalVariables.gv_dtDmucBenh, new List<string>() { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });
               
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
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
                        LoadHoaDonDoNgoaiTruTheoCoSo(1);
                  

                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        private void LoadHoaDonDoNgoaiTruTheoCoSo(int status)
        {
            try
            {
                string mahoadon = string.Empty;
                string kihieu = string.Empty;
                string ma_quyen = string.Empty;
                string serie = string.Empty;
                int HoaDon_Mau_ID = Utility.Int32Dbnull(grdHoaDonCapPhat.GetValue(HoadonMau.Columns.IdHoadonMau));
                string error = string.Empty;

                HoadonMau objHoadonMau = HoadonMau.FetchByID(HoaDon_Mau_ID);
                if (objHoadonMau != null)
                {


                    var sp1 = SPs.SinhSysHoadonMau(HoaDon_Mau_ID, objHoadonMau.MauHoadon, objHoadonMau.KiHieu, objHoadonMau.MaQuyen,"BV01", 1);
                    DataTable histemp = sp1.GetDataSet().Tables[0];
                    if (histemp.Rows.Count > 0)
                    {
                        mahoadon = Utility.sDbnull(histemp.Rows[0][SysHoadonMau.Columns.MauHoadon]);
                        serie = Utility.sDbnull(histemp.Rows[0][SysHoadonMau.Columns.SerieHientai]);
                        kihieu = Utility.sDbnull(histemp.Rows[0][SysHoadonMau.Columns.KiHieu]);
                        ma_quyen = Utility.sDbnull(histemp.Rows[0][SysHoadonMau.Columns.MaQuyen]);

                    }

                }
                txtMauHD.Text = mahoadon;
                txtserihientai.Text = serie;
                txtKiHieu.Text = kihieu;
                    txtSerieDau.Text = Utility.sDbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.SerieDau].Value, "");
                    txtSerieCuoi.Text = Utility.sDbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.SerieCuoi].Value, "");
                    int sSerie = Utility.Int32Dbnull(serie);
                    txtSerie.Text = Utility.sDbnull(sSerie <= 0 ? Utility.Int32Dbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.SerieDau].Value, 0) : sSerie);
                    txtSerie.MaxLength = Utility.DoTrim(txtSerieCuoi.Text).Length;
                    txtSerie.Text = txtSerie.Text.PadLeft(Utility.sDbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.SerieDau].Value).Length, '0');
                    txtMaQuyen.Text = ma_quyen;
                    HOADON_CAPPHAT_ID =
                        Utility.Int32Dbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.IdCapphat].Value,
                            0);
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.Message);
            }

        }
        /// <summary>
        ///     hà thực hiện việc khởi tạo thông tin của Form khi load
        /// </summary>
        private void InitData()
        {
            try
            {
               
                DataTable m_dtDoiTuong = globalVariables.gv_dtDoituong;
                    //THU_VIEN_CHUNG.LaydanhsachDoituongKcb();
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
                if (!PayCheckDate(dtInput_Date.Value)) return;
                if (dtPaymentDate.Value < dtpNgayravien.Value)
                {
                    Utility.ShowMsg("Thời gian thanh toán phải lớn hơn hoặc bằng thời gian cho ra viện. Vui lòng chọn lại");
                    dtPaymentDate.Focus();
                    return;
                }
                PerformAction();
                blnJustPayment = false;
                chkLayHoadon.Checked = false;
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

        private void LaydanhsachLichsuthanhtoan_phieuchi()
        {
            try
            {
                 m_dtPayment = null;
                m_dtPhieuChi = null;
                //DataTable m_dtthanhtoan = new DataTable();
                //m_dtthanhtoan =
                //  _THANHTOAN.LaythongtinCacLanthanhtoan(Utility.sDbnull(txtPatient_Code.Text, ""),
                //      Utility.Int32Dbnull(txtPatient_ID.Text, -1), 0, 0, 0,
                //      globalVariables.MA_KHOA_THIEN);
              //  m_dtPayment = m_dtthanhtoan.Select("Kieu_ThanhToan = 0").CopyToDataTable();
             DataTable   dtData =
                    _THANHTOAN.LaythongtinCacLanthanhtoan(Utility.sDbnull(txtPatient_Code.Text, ""),
                        Utility.Int32Dbnull(txtPatient_ID.Text, -1), 0, v_bytNoitru, 0,
                        globalVariables.MA_KHOA_THIEN,lst_IDLoaithanhtoan,-1);
             DataRow[] arrDR = dtData.Select("Kieu_ThanhToan = 0");
                if(arrDR.Length>0) m_dtPayment=arrDR.CopyToDataTable();
                else
                    m_dtPayment=dtData.Clone();
                arrDR = dtData.Select("Kieu_ThanhToan = 1");
                if (arrDR.Length > 0) m_dtPhieuChi = arrDR.CopyToDataTable();
                else
                    m_dtPhieuChi = dtData.Clone();

              //  m_dtPhieuChi = m_dtthanhtoan.Select("Kieu_ThanhToan = 1").CopyToDataTable();
                //m_dtPhieuChi = _THANHTOAN.LaythongtinCacLanthanhtoan(Utility.sDbnull(txtPatient_Code.Text, ""),
                //        Utility.Int32Dbnull(txtPatient_ID.Text, -1), 0, 0, 1,
                //        globalVariables.MA_KHOA_THIEN);
                //uiTabLSuPhieuTT.TabVisible = m_dtPayment.Rows.Count > 0;
                grdPayment.Visible = m_dtPayment.Rows.Count > 0;
                Utility.SetDataSourceForDataGridEx(grdPayment, dtData, false, true, "1=1", "");
                //Utility.SetDataSourceForDataGridEx(grdPayment, m_dtPayment, false, true, "1=1", "");
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

        /// <summary>
        ///     hàm thực hiện khởi tạo thông tin của lần khám bệnh
        /// </summary>
        /// <returns></returns>
        private KcbLuotkham CreatePatientExam()
        {
            var objLuotkham1 = new KcbLuotkham();
            objLuotkham1 = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtPatient_Code.Text)
                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtPatient_ID.Text)).ExecuteSingle<KcbLuotkham>();
            return objLuotkham1;
        }
        public decimal TongtienCk = 0m;
        public decimal TongtienCkHoadon = 0m;
        public decimal TongtienCkChitiet = 0m;
        public string MaLdoCk = "";
        public string Lydo_chietkhau = "";
        bool ttoan_dthuoc = false;
        private void PerformAction()
        {
            try
            {
                globalVariables.MaphieuHoanung = "";
                objLuotkham = CreatePatientExam();
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

                   
                    TongtienCkChitiet = Utility.DecimaltoDbnull(txtTienChietkhau.Text);
                    bool bo_ckchitiet = true;
                    string ma_uudai = "";
                    MaLdoCk = "";
                    Lydo_chietkhau = "";
                    if (chkChietkhauthem.Checked || TongtienCkChitiet > 0)
                    {
                        frm_ChietkhauTrenHoadon chietkhauTrenHoadon = new frm_ChietkhauTrenHoadon();
                        chietkhauTrenHoadon.TongCKChitiet = Utility.DecimaltoDbnull(txtTienChietkhau.Text);
                        chietkhauTrenHoadon.TongtienBN = Utility.DecimaltoDbnull(txtSoTienCanNop.Text) + Utility.DecimaltoDbnull(txtTienChietkhau.Text);
                        chietkhauTrenHoadon.ckthem = chkChietkhauthem.Checked;
                        chietkhauTrenHoadon.ShowDialog();
                        if (!chietkhauTrenHoadon.m_blnCancel)
                        {
                            ma_uudai = chietkhauTrenHoadon.autoUudai.myCode;
                            bo_ckchitiet = chietkhauTrenHoadon.chkAll.Checked;
                            if (chietkhauTrenHoadon.chkBoChitiet.Checked)
                            {
                                TongtienCkChitiet = 0;
                                mnuHuyChietkhau.PerformClick();
                            }
                            TongtienCk = chietkhauTrenHoadon.TongtienCK;
                            TongtienCkHoadon = chietkhauTrenHoadon.TongCKHoadon;
                            MaLdoCk = chietkhauTrenHoadon.ma_ldoCk;
                            Lydo_chietkhau = chietkhauTrenHoadon.txtLydochietkhau.Text;
                        }
                        else
                        {
                            if (TongtienCkChitiet > 0)
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
                    decimal ttbnChitrathucsu = 0;
                    ErrMsg = "";
                    KcbThanhtoan v_objPayment =TaophieuThanhtoan();
                    DateTime q = (from p in grdThongTinChuaThanhToan.GetCheckedRows()
                                  select Convert.ToDateTime(p.Cells["CreatedDate"].Value)).Max();
                    v_objPayment.MaxNgayTao = q;
                    List<KcbChietkhau> lstChietkhau = new List<KcbChietkhau>();
                    List<KcbThanhtoanChitiet> lstItems = Taodulieuthanhtoanchitiet(ref ErrMsg);
                    if (Utility.DoTrim(ErrMsg).Length > 0)
                    {
                        Utility.ShowMsg(ErrMsg);
                        return;
                    }
                    if (lstItems == null)
                    {
                        Utility.ShowMsg("Lỗi khi tạo dữ liệu thanh toán chi tiết. Liên hệ đơn vị cung cấp phần mềm để được hỗ trợ\n" + ErrMsg);
                        return;
                    }
                    ActionResult actionResult = _THANHTOAN.ThanhtoanChiphiDvuKcb(v_objPayment, objLuotkham,
                        lstItems,lstChietkhau, ref v_Payment_ID, IdHdonLog,
                        chkLayHoadon.Checked &&
                        THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false) == "1",bo_ckchitiet,ma_uudai,
                        ref ttbnChitrathucsu, ref ErrMsg);
                    IN_HOADON = ttbnChitrathucsu > 0;
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Thanh toán tiền cho bệnh nhân ID={0}, PID={1}, Tên={2}, sô tiền={3} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan, v_objPayment.TongTien.ToString()), newaction.Insert, this.GetType().Assembly.ManifestModule.Name);
                            LaydanhsachLichsuthanhtoan_phieuchi();
                            GetDataChiTiet();
                            ucMiengiamkhac1.Refresh();
                            Utility.GotoNewRowJanus(grdPayment, KcbThanhtoan.Columns.IdThanhtoan, v_Payment_ID.ToString());
                            if (v_Payment_ID <= 0)
                            {
                                grdPayment.MoveFirst();
                            }
                            //Kiểm tra nếu hình thức thanh toán phân bổ thì hiển thị chức năng phân bổ
                            List<string> lstPhanbo = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_BATBUOCPHANBO", false).Split(',').ToList<string>();
                            //Check nếu chọn Pttt=phân bổ và số dòng phân bổ <1 thì tự động hiển thị form phân bổ
                            if (lstPhanbo.Contains(cboPttt.SelectedValue.ToString()))
                            {
                                Phanbo();
                            }
                            txtMaLanKham.Focus();
                            txtMaLanKham.SelectAll();
                            //Tạm rem phần hóa đơn đỏ lại
                            if (IN_HOADON && chkTudonginhoadonsauthanhtoan.Checked)
                            {
                                int kcbThanhtoanKieuinhoadon=Utility.Int32Dbnull( THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_KIEUINHOADONTUDONG_SAUKHITHANHTOAN", "1", false));
                                if (kcbThanhtoanKieuinhoadon == 1 || kcbThanhtoanKieuinhoadon == 3)
                                    InHoadon();
                                if (kcbThanhtoanKieuinhoadon == 2 || kcbThanhtoanKieuinhoadon == 3)
                                    new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, v_Payment_ID, objLuotkham, 1);
                            }
                            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false)=="1")
                            {
                                if (grdHoaDonCapPhat.CurrentRow != null)
                                {
                                    if (grdHoaDonCapPhat.CurrentRow.RowType == RowType.Record)
                                    {
                                        LoadHoaDonDoNgoaiTruTheoCoSo(1);
                                    }
                                }
                            }
                            if (globalVariables.MaphieuHoanung != "")
                                Inphieuhoanung();
                            if (chkHienthiDichvusaukhinhannutthanhtoan.Checked)
                            {
                                ShowPaymentDetail(v_Payment_ID);
                            }
                            if (TabPageTamung.TabVisible)//Các nút hoàn ứng xử lý ở hàm Modifycommand
                            {
                                ucTamung1.ChangePatients(objLuotkham, "LYDOTAMUNG_NOITRU", v_bytNoitru);
                            }
                            SetSumTotalProperties();
                            if (chkRestoreDefaultPTTT.Checked)
                                RestoredefaultPTTT();
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
               Utility.ShowMsg("Lỗi:"+ exception.Message);
            }
            finally
            {
                TongtienCk = 0m;
                TongtienCkChitiet = 0m;
                TongtienCkHoadon = 0m;
                MaLdoCk = "";
                ModifyCommand();
                GC.Collect();
            }
        }
        private void Inphieuhoanung()
        {
            try
            {
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Bạn cần chọn bệnh nhân in phiếu hoàn ứng");
                    return;
                }

                DataTable m_dtReport = SPs.KcbInphieuhoanung(globalVariables.MaphieuHoanung).GetDataSet().Tables[0];
                THU_VIEN_CHUNG.CreateXML(m_dtReport, "thanhtoan_phieuhoanung.xml");
                if (m_dtReport == null || m_dtReport.Rows.Count <= 0)
                {
                    Utility.ShowMsg(string.Format( "Không tìm thấy dữ liệu in hoàn ứng theo mã phiếu {0}. Có thể do người dùng hoàn ứng bằng tay. Vui lòng sang tab Tạm ứng-->Hoàn ứng và bấm in tay", globalVariables.MaphieuHoanung), "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                List<string> lstCode = (from p in m_dtReport.AsEnumerable()
                                        select Utility.sDbnull(p["ma_tamung"], "")).Distinct().ToList<string>();
                string sophieutamung = "";
                if (lstCode.Count > 0)
                    sophieutamung = string.Join(",", lstCode.ToArray<string>());
                Int64 tongtienhoanung = Utility.Int64Dbnull(m_dtReport.Compute("SUM(so_tien)", "1=1"), 0);
                foreach (DataRow dr in m_dtReport.Rows)
                {
                    dr["so_tien"] = tongtienhoanung;
                }
                string tieude = "", reportname = "";
                var crpt = Utility.GetReport("thanhtoan_phieuhoanung", ref tieude, ref reportname);
                if (crpt == null) return;

                MoneyByLetter _moneyByLetter = new MoneyByLetter();
                var objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
                Utility.UpdateLogotoDatatable(ref m_dtReport);

                crpt.SetDataSource(m_dtReport);


                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "thanhtoan_phieuhoanung";
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "sophieutamung", sophieutamung);
                Utility.SetParameterValue(crpt, "TelePhone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "sMoneyLetter", _moneyByLetter.sMoneyToLetter(tongtienhoanung.ToString()).ToString());
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(DateTime.Now));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;

                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewPhieuTamung))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    objForm.ShowDialog();
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(objForm.getPrintNumber, false, 0, 0);
                }


            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        void ShowPaymentDetail(long v_Payment_ID)
        {
            if (objLuotkham == null)
            {
                objLuotkham = CreatePatientExam();
            }
            if (objLuotkham != null)
            {
                frm_HuyThanhtoan frm = new frm_HuyThanhtoan(lst_IDLoaithanhtoan);
                frm.objLuotkham = objLuotkham;
                frm.v_Payment_Id = v_Payment_ID;
                frm.Chuathanhtoan = _chuathanhtoan;
                frm.txtSoTienCanNop.Text = txtSoTienCanNop.Text;
                frm.TotalPayment = grdPayment.GetDataRows().Length;
                frm.ShowCancel = false;
                frm.ShowDialog();
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
            mnuHoanung.Text = cmdHoanung.Text;
            mnuHoanung.Enabled = cmdHoanung.Enabled;
        }
        /// <summary>
        ///     hàm thực hiện mảng của chi tiết thanh toán chi tiết
        /// </summary>
        /// <returns></returns>
        private List< KcbThanhtoanChitiet> Taodulieuthanhtoanchitiet(ref string errMsg)
        {
            try
            {
                DataTable dtDataCheck = new DataTable();
                byte ErrType = 0;//0= xóa dịch vụ sau khi tnv chọn người bệnh-->có trong bảng tt chi tiết, ko có trong các bảng dịch vụ khám,thuốc/vtth,cls;1= đã bị người khác thanh toán;
                List<KcbThanhtoanChitiet> lstItems = new List<KcbThanhtoanChitiet>();
                foreach (GridEXRow row in grdThongTinChuaThanhToan.GetCheckedRows())
                {
                   KcbThanhtoanChitiet newItem = new KcbThanhtoanChitiet();
                    newItem.IdThanhtoan = -1;
                    newItem.IdChitiet = -1;
                    newItem.TinhChiphi = 1;
                    if (objLuotkham.PtramBhyt != null) newItem.PtramBhyt = objLuotkham.PtramBhyt.Value;
                    if (objLuotkham.PtramBhytGoc != null) newItem.PtramBhytGoc = objLuotkham.PtramBhytGoc.Value;
                    newItem.SoLuong = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.SoLuong].Value, 0);
                    //Phần tiền BHYT chi trả,BN chi trả sẽ tính lại theo % mới nhất của bệnh nhân trong phần Business
                    newItem.BnhanChitra = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.BnhanChitra].Value, 0);
                    newItem.BhytChitra = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.BhytChitra].Value, 0);
                    newItem.DonGia = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.DonGia].Value, 0);
                    newItem.GiaGoc = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.GiaGoc].Value, 0);
                    newItem.TyleTt = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.TyleTt].Value, 0);
                    newItem.PhuThu = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.PhuThu].Value, 0);
                    newItem.TinhChkhau = Utility.ByteDbnull(row.Cells[KcbThanhtoanChitiet.Columns.TinhChkhau].Value, 0);
                    newItem.CkNguongt = Utility.ByteDbnull(row.Cells[KcbThanhtoanChitiet.Columns.CkNguongt].Value, 0);
                    newItem.TuTuc = Utility.ByteDbnull(row.Cells[KcbThanhtoanChitiet.Columns.TuTuc].Value, 0);
                    newItem.IdPhieu = Utility.Int32Dbnull(row.Cells["id_phieu"].Value);
                    newItem.IdKham = Utility.Int32Dbnull(row.Cells["Id_Kham"].Value);
                    newItem.IdPhieuChitiet = Utility.Int32Dbnull(row.Cells["Id_Phieu_Chitiet"].Value, -1);
                    newItem.IdDichvu = Utility.Int16Dbnull(row.Cells["Id_dichvu"].Value, -1);
                    newItem.IdChitietdichvu = Utility.Int16Dbnull(row.Cells["Id_Chitietdichvu"].Value, -1);
                    newItem.TenChitietdichvu = Utility.sDbnull(row.Cells["Ten_Chitietdichvu"].Value, "Không xác định").Trim();
                    newItem.TenBhyt = Utility.sDbnull(row.Cells["ten_bhyt"].Value, "Không xác định").Trim();
                    newItem.DonviTinh =Utility.chuanhoachuoi(Utility.sDbnull(row.Cells["Ten_donvitinh"].Value, "Lượt"));
                    newItem.SttIn = Utility.Int16Dbnull(row.Cells["stt_in"].Value, 0);
                    newItem.IdKhoakcb = Utility.Int16Dbnull(row.Cells["id_khoakcb"].Value, -1);
                    newItem.IdPhongkham = Utility.Int16Dbnull(row.Cells["id_phongkham"].Value, -1);
                    newItem.IdBacsiChidinh = Utility.Int16Dbnull(row.Cells["id_bacsi"].Value, -1);
                    newItem.IdLoaithanhtoan = Utility.ByteDbnull(row.Cells["Id_Loaithanhtoan"].Value, -1);
                    newItem.IdLichsuDoituongKcb = Utility.Int64Dbnull(row.Cells[KcbThanhtoanChitiet.Columns.IdLichsuDoituongKcb].Value, -1);
                    newItem.MatheBhyt = Utility.sDbnull(row.Cells[KcbThanhtoanChitiet.Columns.MatheBhyt].Value, -1);
                    newItem.TenLoaithanhtoan = THU_VIEN_CHUNG.MaKieuThanhToan(Utility.Int32Dbnull(row.Cells["Id_Loaithanhtoan"].Value, -1));
                    newItem.TienChietkhau =Math.Round( Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.TienChietkhau].Value, 0m),3);
                    newItem.TileChietkhau =Math.Round( Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.TileChietkhau].Value, 0m),3);
                    newItem.MaDoituongKcb = objLuotkham.MaDoituongKcb;
                    newItem.UserTao = Utility.sDbnull(row.Cells["User_tao"].Value, "UKN").Trim();
                    newItem.KieuChietkhau = "%";
                    newItem.IdThanhtoanhuy = -1;
                    newItem.TrangthaiHuy = 0;
                    newItem.TrongGoi = Utility.ByteDbnull(row.Cells["trong_goi"].Value);
                    newItem.IdGoi = Utility.Int32Dbnull(row.Cells["id_goi"].Value);
                    newItem.TrangthaiBhyt = 0;
                    newItem.TrangthaiChuyen = 0;
                    newItem.NoiTru = 1;
                    newItem.NguonGoc = Utility.ByteDbnull(row.Cells["noi_tru"].Value, -1);
                    newItem.NgayTao = globalVariables.SysDate;
                    newItem.NguoiTao = globalVariables.UserName;
                    lstItems.Add(newItem);
                    dtDataCheck = SPs.ThanhtoanKiemtratontaitruockhithanhtoan(newItem.IdPhieu, newItem.IdPhieuChitiet, newItem.IdLoaithanhtoan).GetDataSet().Tables[0];
                    if (dtDataCheck.Rows.Count <= 0)
                    {
                        ErrType = 0;
                        errMsg += newItem.TenChitietdichvu + "\n";
                        break;
                    }
                    else//Kiểm tra trạng thái thanh toán tránh việc thanh toán 2 lần(2 user cùng chọn và sau đó từng người bấm nút thanh toán)
                        if (dtDataCheck.Rows[0]["trangthai_thanhtoan"].ToString() == "1")
                        {
                            ErrType = 1;
                            errMsg += newItem.TenChitietdichvu + "\n";
                            break;
                        }
                }
                if (errMsg.Length > 0)
                    if (ErrType == 0)
                        errMsg = "Một số dịch vụ đang chọn thanh toán đã bị xóa/hủy bởi người khác. Vui lòng chọn lại người bệnh để lấy lại dữ liệu mới nhất. Kiểm tra các dịch vụ không tồn tại dưới đây:\n" + errMsg;
                    else if (ErrType == 1)
                        errMsg = "Một số dịch vụ đang chọn thanh toán đã được thanh toán bởi TNV khác(trong lúc bạn chọn và chưa bấm thanh toán). Vui lòng chọn lại người bệnh để lấy lại dữ liệu mới nhất. Kiểm tra các dịch vụ đã được thanh toán dưới đây:\n" + errMsg;
                return lstItems;
            }
            catch(Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }

        private bool PayCheckDate(DateTime InputDate)
        {
            if (globalVariables.SysDate.Date != InputDate.Date && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_CHOPHEPCHONGAYTHANHTOAN","1",false) =="1")
            {
                frm_ChonngayThanhtoan frm = new frm_ChonngayThanhtoan();
                frm.pdt_InputDate = dtInput_Date.Value;
                frm.ShowDialog();
                if (!frm.mv_blnCancel)
                {
                    dtPaymentDate.Value = frm.pdt_InputDate;
                    return true;
                }
                else
                    return false;
            }
            return true;
        }

        private KcbThanhtoan TaophieuThanhtoan()
        {
            KcbThanhtoan objPayment = new KcbThanhtoan();
            objPayment.IdThanhtoan = -1;
            objPayment.MaLuotkham = Utility.sDbnull(txtPatient_Code.Text, "");
            objPayment.IdBenhnhan = Utility.Int32Dbnull(txtPatient_ID.Text, -1);
            objPayment.NgayThanhtoan = dtPaymentDate.Value;
            objPayment.IdNhanvienThanhtoan = globalVariables.gv_intIDNhanvien;
            objPayment.KieuThanhtoan = 0;//0=Thanh toán thường;1= trả lại tiền;2= thanh toán bỏ viện
            objPayment.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
            objPayment.NoiTru = v_bytNoitru;
            objPayment.TrangthaiIn = 0;
            objPayment.NgayIn = null;
            objPayment.NguoiIn = string.Empty;
            objPayment.MaPttt = cboPttt.SelectedValue.ToString();
            objPayment.MaNganhang = cboNganhang.Enabled ?  Utility.sDbnull( cboNganhang.SelectedValue,"")  : "-1";
            objPayment.NgayTonghop = null;
            objPayment.NguoiTonghop = string.Empty;
            objPayment.NgayChot = null;
            objPayment.TrangthaiChot = 0;
            objPayment.TongTien = Utility.DecimaltoDbnull(txtSoTienCanNop.Text, 0);
            objPayment.Ghichu = Utility.DoTrim(txtGhichu.Text);
            //2 mục này được tính lại ở Business
            objPayment.BnhanChitra = -1;
            objPayment.BhytChitra = -1;
            objPayment.TileChietkhau = 0;
            objPayment.KieuChietkhau = "T";
            objPayment.TongtienChietkhau = TongtienCk;
            objPayment.TongtienChietkhauChitiet = TongtienCkChitiet;
            objPayment.TongtienChietkhauHoadon = TongtienCkHoadon;
            if (chkLayHoadon.Checked &&THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false)=="1")
            {
                objPayment.MauHoadon =Utility.DoTrim( txtMauHD.Text);
                objPayment.KiHieu =Utility.DoTrim( txtKiHieu.Text);
                objPayment.IdCapphat = Utility.Int32Dbnull(grdHoaDonCapPhat.GetValue(HoadonCapphat.Columns.IdCapphat), -1);
                objPayment.MaQuyen = Utility.DoTrim(txtMaQuyen.Text);
                objPayment.Serie = Utility.DoTrim(txtSerie.Text);
            }

            objPayment.MaLydoChietkhau = MaLdoCk;
            objPayment.LydoChietkhau = Lydo_chietkhau;
            objPayment.NgayTao = globalVariables.SysDate;
            objPayment.NguoiTao = globalVariables.UserName;
            objPayment.IpMaytao = globalVariables.gv_strIPAddress;
            objPayment.TenMaytao = globalVariables.gv_strComputerName;
            return objPayment;
        }

        private bool IsValidata()
        {
            bool bCheckPayment = false;
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHOPHEP_0_DONG", true) == "0")
            {
                if (Utility.DecimaltoDbnull(txtSoTienCanNop.Text, 0) == 0)
                {
                    Utility.ShowMsg("Hệ thống chỉ cho phép thanh toán khi số tiền thu phải > 0. Mời bạn kiểm tra lại", "Thông báo", MessageBoxIcon.Warning);
                    return false;
                }
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
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_TUDONGHOANUNG_KHITHANHTOANNOITRU", "0", false) == "0")
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
                    bCheckPayment = true;
                    break;
                }
            }
            if (bCheckPayment)
            {
                Utility.ShowMsg("Bạn phải chọn các bản ghi chưa thực hiện thanh toán mới thanh toán được", "Thông báo", MessageBoxIcon.Warning);
                grdThongTinChuaThanhToan.Focus();
                return false;
            }
            foreach (GridEXRow gridExRow in grdThongTinChuaThanhToan.GetCheckedRows())
            {
                if (gridExRow.Cells["trangthai_huy"].Value.ToString() == "1")
                {
                    bCheckPayment = true;
                    break;
                }
            }
            if (bCheckPayment)
            {
                Utility.ShowMsg("Bạn phải bỏ chọn bản ghi bị hủy trước khi thanh toán.Vui lòng kiểm tra lại", "Thông báo",
                    MessageBoxIcon.Warning);
                grdThongTinChuaThanhToan.Focus();
                return false;
            }
            if (cboPttt.SelectedValue.ToString() == "-1")
            {
                Utility.ShowMsg("Bạn phải chọn phương thức thanh toán trước khi thực hiện thanh toán");
                cboPttt.Focus();
                return false;
            }
            if (!isValidPttt_Nganhang())
                return false;
            objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text, -1), Utility.sDbnull(txtPatient_Code.Text));
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Không lấy được thông tin bệnh nhân cần thanh toán. Đề nghị liên hệ IT để được giải quyết");
                return false;
            }
            if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) && Utility.DoTrim( objLuotkham.MatheBhyt)=="")
            {
                Utility.ShowMsg("Bệnh nhân BHYT cần nhập mã thẻ BHYT trước khi thanh toán");
                return false;
            }
            //if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) 
            //    && Utility.Int16Dbnull(objLuotkham.TrangthaiNgoaitru, 0) < 1
            //    && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_BHYT_ISKETTHUC", "0", false) == "1")
            //{
            //    Utility.ShowMsg("Bệnh nhân cần được kết thúc trước khi thanh toán");
            //    return false;
            //}
            //if (objLuotkham.TrangthaiNoitru >= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
            //{
            //    Utility.ShowMsg("Bệnh nhân này đã phát sinh dịch vụ nội trú(Nộp tiền tạm ứng, Lập phiếu điều trị...) nên hệ thống không cho phép thanh toán ngoại trú nữa");
            //    return false;
            //}
            //if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_BHYT_NHIEULAN", "0", false) == "0")
            //{
            //    KcbThanhtoan objthanhtoan = new Select().From(KcbThanhtoan.Schema)
            //        .Where(KcbThanhtoan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
            //        .And(KcbThanhtoan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
            //        .ExecuteSingle<KcbThanhtoan>();
            //    if (objthanhtoan != null)
            //    {
            //        Utility.ShowMsg(string.Format("Bệnh nhân {0} thuộc đối tượng BHYT đã được thanh toán ít nhất một lần.\nHệ thống đang cấu hình không cho phép đối tượng BHYT thanh toán nhiều lần\nDo vậy bạn cần hủy thanh toán của các lần thanh toán trước để thực hiện một lần thanh toán duy nhất cho đối tượng này", txtTenBenhNhan.Text));
            //        return false;
            //    }
            //}
            //if (THU_VIEN_CHUNG.Laygiatrithamsohethong("NGOAITRU_TUDONGHOANUNG_KHITHANHTOANNGOAITRU", "0", false) == "0")
            //{
            //    NoitruTamung objTamung = new Select().From(NoitruTamung.Schema).Where(NoitruTamung.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
            //        .And(NoitruTamung.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
            //        .And(NoitruTamung.Columns.TrangThai).IsEqualTo(0)
            //        .And(NoitruTamung.Columns.KieuTamung).IsEqualTo(0)
            //        .And(NoitruTamung.Columns.Noitru).IsEqualTo(0)
            //        .ExecuteSingle<NoitruTamung>();
            //    if (objTamung != null)
            //    {
            //        Utility.ShowMsg("Bạn cần thực hiện thao tác hoàn ứng tiền cho bệnh nhân trước khi thực hiện thanh toán ngoại trú");
            //        return false;
            //    }
            //}
           
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
            if (grdPayment.GetValue("kieu_thanhtoan").ToString() == "1")
            {
                if (e.Column.Key == "cmdPHIEU_THU")
                {
                    CallPhieuChi(grdPayment);
                }
                if (e.Column.Key == "cmdHUY_PHIEUTHU")
                {
                    HuyPhieuchi(grdPayment);
                }
            }
            else
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
        }
        string ma_lydohuy = "";
        string lydo_huy = "";
        private void HuyThanhtoan()
        {
            try
            {
                ma_lydohuy = "";
                if (!Utility.isValidGrid(grdPayment)) return;
                if (grdPayment.CurrentRow != null)
                {
                    if (objLuotkham == null)
                    {
                        objLuotkham = CreatePatientExam();
                    }
                    if (objLuotkham.Noitru==0 && objLuotkham.TrangthaiNoitru >=
                        Utility.Int32Dbnull(
                            THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
                    {
                        Utility.ShowMsg(
                            "Bệnh nhân này đã ở trạng thái nội trú nên hệ thống không cho phép hủy thanh toán ngoại trú nữa");
                        return;
                    }
                    //if (objPhieuDct.TrangthaiXml > Utility.Int32Dbnull(1))
                    //{
                    //    Utility.ShowMsg("Bệnh nhân đã được gửi dữ liệu BHYT!");
                    //    return;
                    //}

                    v_Payment_ID =
                        Utility.Int32Dbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1);
                    KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_Payment_ID);

                    if (objPayment != null)
                    {
                        //Kiểm tra ngày hủy
                        int kcbThanhtoanSongayHuythanhtoan =
                            Utility.Int32Dbnull(
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_SONGAY_HUYTHANHTOAN", "0", true), 0);
                        var chenhlech =
                            (int) Math.Ceiling((globalVariables.SysDate.Date - objPayment.NgayThanhtoan.Date).TotalDays);
                        if (chenhlech > kcbThanhtoanSongayHuythanhtoan)
                        {
                            Utility.ShowMsg(
                               string.Format( "Ngày thanh toán: {0}. Hệ thống không cho phép bạn hủy thanh toán khi đã quá {1} ngày. Cần liên hệ quản trị hệ thống để được trợ giúp",objPayment.NgayThanhtoan.Date.ToString("dd/MM/yyyy"),kcbThanhtoanSongayHuythanhtoan.ToString()));
                            return;
                        }
                        if (Utility.Byte2Bool(objPayment.TrangthaiChot))
                        {
                            Utility.ShowMsg(
                                "Thanh toán đang chọn đã được chốt nên bạn không thể hủy thanh toán. Mời bạn xem lại!");
                            return;
                        }
                        if (chkViewtruockhihuythanhtoan.Checked)
                        {
                            var frm = new frm_HuyThanhtoan(lst_IDLoaithanhtoan);
                            frm.objLuotkham = objLuotkham;
                            frm.v_Payment_Id = Utility.Int32Dbnull(objPayment.IdThanhtoan, -1);
                            frm.Chuathanhtoan = _chuathanhtoan;
                            frm.TotalPayment = grdPayment.GetDataRows().Length;
                            frm.txtSoTienCanNop.Text = txtSoTienCanNop.Text;
                            frm.ShowCancel = true;
                            frm.ShowDialog();
                            if (!frm.m_blnCancel)
                            {
                                GetData();
                            }
                        }
                        else
                        {
                            if (PropertyLib._ThanhtoanProperties.Hoitruockhihuythanhtoan)
                                if (
                                    !Utility.AcceptQuestion(
                                        string.Format("Bạn có muốn hủy lần thanh toán với Mã thanh toán {0}",
                                            objPayment.IdThanhtoan), "Thông báo", true))
                                {
                                    return;
                                }
                            if (
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_BATNHAPLYDO_HUYTHANHTOAN", "1",
                                    false) == "1")
                            {
                                var nhaplydohuythanhtoan = new frm_Chondanhmucdungchung("LYDOHUYTHANHTOAN",
                                    "Hủy thanh toán tiền Bệnh nhân", "Nhập lý do hủy thanh toán trước khi thực hiện",
                                    "Lý do hủy thanh toán",false);
                                nhaplydohuythanhtoan.ShowDialog();
                                if (nhaplydohuythanhtoan.m_blnCancel) return;
                                ma_lydohuy = nhaplydohuythanhtoan.ma;
                                lydo_huy = nhaplydohuythanhtoan.ten;
                            }
                            int idHdonLog =
                                Utility.Int32Dbnull(grdPayment.CurrentRow.Cells[HoadonLog.Columns.IdHdonLog].Value, -1);
                            bool huythanhtoanHuybienlai =
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_HUYTHANHTOAN_HUYBIENLAI", "1", true) == "1";
                            ActionResult actionResult = _THANHTOAN.HuyThanhtoanNoitru(objPayment, objLuotkham, lydo_huy,
                                idHdonLog, huythanhtoanHuybienlai);
                            switch (actionResult)
                            {
                                case ActionResult.Success:
                                    
                                    GetData();
                                    //Utility.Log(Name, globalVariables.UserName,
                                    //    string.Format(
                                    //        "Hủy thanh toán ngoại trú của bệnh nhân có mã lần khám {0} và ID bệnh nhân là: {1} Tên= {2}",
                                    //        objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan),
                                    //    newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                                    break;
                                case ActionResult.ExistedRecord:
                                    break;
                                case ActionResult.Error:
                                    Utility.ShowMsg("Lỗi trong quá trình hủy thông tin thanh toán", "Thông báo",
                                        MessageBoxIcon.Error);
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
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
            finally
            {
                GC.Collect();
            }
           
        }
        private void CallPhieuThu()
        {
            if (grdPayment.CurrentRow != null)
            {
                v_Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
               

                KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_Payment_ID);
                if (objLuotkham == null)
                {
                    objLuotkham = CreatePatientExam();
                }
                if (objPayment != null)
                {
                    if (objLuotkham != null)
                    {
                        frm_HuyThanhtoan frm = new frm_HuyThanhtoan(lst_IDLoaithanhtoan);
                        frm.objLuotkham = objLuotkham;
                        frm.v_Payment_Id = Utility.Int32Dbnull(objPayment.IdThanhtoan, -1);
                        frm.Chuathanhtoan = _chuathanhtoan;
                        frm.TotalPayment = grdPayment.GetDataRows().Length;
                        frm.txtSoTienCanNop.Text = txtSoTienCanNop.Text;
                        frm.ShowCancel = false;
                        frm.ShowDialog();
                    }
                }
            }
        }
       
        private void cmdInphoiBHYT_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(lblMessage, "", false);
            if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb.Value))
                return ;
            if (objLuotkham == null)
            {
                Utility.SetMsg(lblMessage, "Bạn cần chọn Bệnh nhân cần thanh toán", true);
                return;
            }
            if (string.IsNullOrEmpty(objLuotkham.MabenhChinh))
            {
                Utility.SetMsg(lblMessage, "Chưa có mã bệnh ICD. Quay lại phòng khám của bác sĩ để nhập", true);
                txtICD.Focus();
                return;
            }
            if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb)
              && Utility.Int16Dbnull(objLuotkham.TrangthaiNgoaitru, 0) < 1
              && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_BHYT_ISKETTHUC", "0", false) == "1")
            {
                Utility.ShowMsg("Bệnh nhân cần được kết thúc trước khi in phôi BHYT");
                return ;
            }
            INPHIEU_CLICK = true;
            if (!THANHTOAN_BHYT_INPHIEU()) return;
            InPhoiBhyt();
        }
        private bool THANHTOAN_BHYT_INPHIEU()
        {
            try
            {
                //Nếu đối tượng dịch vụ thì không tự thanh toán
                if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb.Value))// Utility.sDbnull(objLuotkham.MaDoituongKcb) != "BHYT")
                    return false;
                List<GridEXRow> ChuaThanhToan = (from thanhtoan in grdThongTinChuaThanhToan.GetDataRows()
                    where Utility.Int32Dbnull(thanhtoan.Cells[KcbThanhtoanChitiet.Columns.TrangthaiHuy].Value) == 0
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
             byte kieuthanhtoan = Utility.ByteDbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.KieuThanhtoan].Value, 0);
             if (kieuthanhtoan == 0)
             {
                 if (!Utility.Coquyen("thanhtoan_huythanhtoan"))
                 {
                     Utility.ShowMsg("Bạn không được cấp quyền hủy thanh toán(thanhtoan_huythanhtoan)");
                     return;
                 }

                 HuyThanhtoan();
             }
             else if (kieuthanhtoan == 5)
             {
                 if (!Utility.Coquyen("thanhtoan_huytamthu"))
                 {
                     Utility.ShowMsg("Bạn không được cấp quyền hủy tạm thu (thanhtoan_huytamthu)");
                     return;
                 }
                 v_Payment_ID = Utility.Int32Dbnull(grdPayment.CurrentRow.Cells["Id_thanhtoan"].Value, -1);
                 KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_Payment_ID);
                 HuyTamthu(objPayment);
             }
             else
             {
                 if (!Utility.Coquyen("thanhtoan_huyphieuchi"))
                 {
                     Utility.ShowMsg("Bạn không được cấp quyền hủy phiếu chi (thanhtoan_huyphieuchi)");
                     return;
                 }
                 HuyPhieuchi(grdPhieuChi);
             }
        }
        private void HuyTamthu(KcbThanhtoan objPayment)
        {
            try
            {
                ma_lydohuy = "";
                if (!Utility.isValidGrid(grdPayment)) return;
                if (grdPayment.CurrentRow != null)
                {
                    if (objLuotkham == null)
                    {
                        objLuotkham = CreatePatientExam();
                    }
                    if (objLuotkham.TrangthaiNoitru >= 1)//                       Utility.Int32Dbnull(                            THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
                    {
                        Utility.ShowMsg(
                            "Bệnh nhân này đã ở trạng thái nội trú nên hệ thống không cho phép hủy tạm thu ngoại trú nữa");
                        return;
                    }
                    //if (objPhieuDct.TrangthaiXml > Utility.Int32Dbnull(1))
                    //{
                    //    Utility.ShowMsg("Bệnh nhân đã được gửi dữ liệu BHYT!");
                    //    return;
                    //}



                    if (objPayment != null)
                    {
                        //Kiểm tra ngày hủy
                        int kcbThanhtoanSongayHuythanhtoan =
                            Utility.Int32Dbnull(
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_SONGAY_HUYTHANHTOAN", "0", true), 0);
                        var chenhlech =
                            (int)Math.Ceiling((globalVariables.SysDate.Date - objPayment.NgayThanhtoan.Date).TotalDays);
                        if (chenhlech > kcbThanhtoanSongayHuythanhtoan)
                        {
                            Utility.ShowMsg(
                               string.Format("Ngày tạm thu: {0}. Hệ thống không cho phép bạn hủy tạm thu khi đã quá {1} ngày. Cần liên hệ quản trị hệ thống để được trợ giúp", objPayment.NgayThanhtoan.Date.ToString("dd/MM/yyyy"), kcbThanhtoanSongayHuythanhtoan.ToString()));
                            return;
                        }
                        if (Utility.Byte2Bool(objPayment.TrangthaiChot))
                        {
                            Utility.ShowMsg(
                                "tạm thu đang chọn đã được chốt nên bạn không thể hủy tạm thu. Mời bạn xem lại!");
                            return;
                        }
                        if (PropertyLib._ThanhtoanProperties.Hienthihuythanhtoan)
                        {
                            var frm = new frm_HuyThanhtoan(lst_IDLoaithanhtoan);
                            frm.objLuotkham = objLuotkham;
                            frm.v_Payment_Id = Utility.Int32Dbnull(objPayment.IdThanhtoan, -1);
                            frm.Chuathanhtoan = _chuathanhtoan;
                            frm.TotalPayment = grdPayment.GetDataRows().Length;
                            frm.txtSoTienCanNop.Text = txtSoTienCanNop.Text;
                            frm.ShowCancel = true;
                            frm.ShowDialog();
                            if (!frm.m_blnCancel)
                            {
                                GetData();
                            }
                        }
                        else
                        {
                            if (PropertyLib._ThanhtoanProperties.Hoitruockhihuythanhtoan)
                                if (
                                    !Utility.AcceptQuestion(
                                        string.Format("Bạn có muốn hủy lần tạm thu với Mã tạm thu {0}",
                                            objPayment.IdThanhtoan), "Thông báo", true))
                                {
                                    return;
                                }
                            if (
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_BATNHAPLYDO_HUYTHANHTOAN", "1",
                                    false) == "1")
                            {
                                var nhaplydohuythanhtoan = new frm_Chondanhmucdungchung("LYDOHUYTHANHTOAN",
                                    "Hủy tạm thu tiền Bệnh nhân", "Nhập lý do hủy tạm thu trước khi thực hiện",
                                    "Lý do hủy tạm thu", false);
                                nhaplydohuythanhtoan.ShowDialog();
                                if (nhaplydohuythanhtoan.m_blnCancel) return;
                                ma_lydohuy = nhaplydohuythanhtoan.ma;
                                lydo_huy = nhaplydohuythanhtoan.ten;
                            }
                            int idHdonLog =
                                Utility.Int32Dbnull(grdPayment.CurrentRow.Cells[HoadonLog.Columns.IdHdonLog].Value, -1);
                            bool huythanhtoanHuybienlai =
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_HUYTHANHTOAN_HUYBIENLAI", "1", true) == "1";
                            ActionResult actionResult = _THANHTOAN.HuyTamthu(objPayment, objLuotkham, lydo_huy,
                                idHdonLog, huythanhtoanHuybienlai);
                            switch (actionResult)
                            {
                                case ActionResult.Success:
                                    GetData();
                                    //Utility.Log(Name, globalVariables.UserName,
                                    //    string.Format(
                                    //        "Hủy tạm thu ngoại trú của bệnh nhân có mã lần khám {0} và ID bệnh nhân là: {1} Tên= {2}",
                                    //        objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan),
                                    //    newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                                    break;
                                case ActionResult.ExistedRecord:
                                    break;
                                case ActionResult.Error:
                                    Utility.ShowMsg("Lỗi trong quá trình hủy thông tin tạm thu", "Thông báo",
                                        MessageBoxIcon.Error);
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
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                GC.Collect();
            }

        }
        private void grdPayment_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == KcbThanhtoan.Columns.NgayThanhtoan)
            {
                UpdatePaymentDate();
            }
        }

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
                    DateTime newDate = Convert.ToDateTime(grdPayment.GetValue(KcbThanhtoan.Columns.NgayThanhtoan));
                    if (!globalVariables.isSuperAdmin)
                    {
                        if (objPayment.MaxNgayTao.HasValue && newDate.Date < objPayment.MaxNgayTao.Value.Date)
                        {
                            Utility.ShowMsg(string.Format("Bạn cần chọn ngày thanh toán cần >= {0}", objPayment.MaxNgayTao.Value.ToString("dd/MM/yyyy")), "Thông báo");
                            return;
                        }
                    }
                    string noi_dung = string.Format("Đổi ngày thanh toán của hóa đơn Id thanh toán ={0}, id bệnh nhân={1}, Mã lượt khám ={2} từ {3} thành {4} thành công", objPayment.IdThanhtoan, objPayment.IdBenhnhan, objPayment.MaLuotkham, objPayment.NgayThanhtoan, newDate);
                    objPayment.NgayThanhtoan = newDate;
                    ActionResult actionResult = _THANHTOAN.UpdateNgayThanhtoan(objPayment,noi_dung);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.ShowMsg("Bạn chỉnh sửa ngày thanh toán thành công", "Thông báo");
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình chỉnh ngày thanh toán", "Thông báo lỗi", MessageBoxIcon.Error);
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
                    DateTime newDate = Convert.ToDateTime(grdPhieuChi.GetValue(KcbThanhtoan.Columns.NgayThanhtoan));
                    string noi_dung = string.Format("Đổi ngày Phiếu chi Id phiếu ={0}, id bệnh nhân={1}, Mã lượt khám ={2} từ {3} thành {4} thành công", objPayment.IdThanhtoan, objPayment.IdBenhnhan, objPayment.MaLuotkham, objPayment.NgayThanhtoan, newDate);
                    objPayment.NgayThanhtoan = newDate;

                    ActionResult actionResult = _THANHTOAN.UpdateNgayThanhtoan(objPayment,noi_dung);
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

        private void grdPhieuChi_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            //if (e.Column.Key == KcbThanhtoan.Columns.NgayThanhtoan)
            //{
            //    //UpdatePaymentDate();
            //    UpdatePhieuChiPaymentDate();
            //}
        }

        private void frm_THANHTOAN_NOITRU_V1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ActiveControl != null && TabPageTamung.ActiveControl != null )
                {
                    ucTamung1.EnterNextControl(ucTamung1.ActiveControl);
                }
                else if (ActiveControl != null && uiTabPageMG.ActiveControl != null)
                {
                    ucMiengiamkhac1.EnterNextControl(ucMiengiamkhac1.ActiveControl);
                }
                else if (ActiveControl != null && uiTabPageThuchikhac.ActiveControl != null)
                {
                    ucThuchikhac1.EnterNextControl(ucThuchikhac1.ActiveControl);
                }
                else
                ProcessTabKey(true);
            }
            if (e.KeyCode == Keys.Escape) Close();
            if (e.KeyCode == Keys.F3 ||(e.Control && e.KeyCode==Keys.F))
            {
                txtMaLanKham.Focus();
                txtMaLanKham.Select();
                cmdSearch.PerformClick();
            }
            if (e.KeyCode == Keys.F5) cmdChuyenGiamDinh.PerformClick();
            if (e.KeyCode == Keys.F4) cmdInphoiBHYT.PerformClick();
            if (e.KeyCode == Keys.T && e.Control) cmdThanhToan.PerformClick();
            if (e.Alt && e.KeyCode == Keys.F1) tabThongTinCanThanhToan.SelectedIndex = 0;
            if (e.Alt && e.KeyCode == Keys.F2) tabThongTinCanThanhToan.SelectedIndex = 1;
            if (e.Alt && e.KeyCode == Keys.F3) tabThongTinCanThanhToan.SelectedIndex = 2;
            if (e.Alt && e.KeyCode == Keys.F5) tabThongTinCanThanhToan.SelectedIndex = 3;
            if (e.Alt && e.KeyCode == Keys.F6) tabThongTinCanThanhToan.SelectedIndex = 4;
            if (e.Alt && e.KeyCode == Keys.F7) tabThongTinCanThanhToan.SelectedIndex = 5;
            if (e.Alt && e.KeyCode == Keys.F8) tabThongTinCanThanhToan.SelectedIndex = 6;
            //if (e.KeyCode == Keys.F7) uiTabHoadon_chiphi.SelectedIndex = 0;
            //if (e.KeyCode == Keys.F8) uiTabHoadon_chiphi.SelectedIndex = 1;
            //if (e.KeyCode == Keys.D4 && e.Alt) tabThongTinThanhToan.SelectedTab = tabPagePayment;
            //if (e.KeyCode == Keys.D5 && e.Alt) tabThongTinThanhToan.SelectedTab = tabPagePhieuChi;
        }

        private void cmdLaylaiThongTin_Click(object sender, EventArgs e)
        {
            GetData();
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
        bool AllowHeaderCheckedChanged = false;
        /// <summary>
        ///     hàm thực hiện viecj trạng thái của grid header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdThongTinChuaThanhToan_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (!blnLoaded) return;
                AllowHeaderCheckedChanged = false;
                grdThongTinChuaThanhToan.RowCheckStateChanged -= grdThongTinChuaThanhToan_RowCheckStateChanged;
                if(grdThongTinChuaThanhToan.CurrentRow!=null)
                    foreach (GridEXRow row in grdDSKCB.GetDataRows())
                    {
                        if (Utility.sDbnull(row.Cells["trangthai_thanhtoan"].Value, "0") == "0")
                        {
                            row.BeginEdit();
                            row.IsChecked = grdThongTinChuaThanhToan.CurrentRow.IsChecked;
                            row.EndEdit();
                        }
                    }
                AllowHeaderCheckedChanged = true;
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
                if (e.Column.Key == "cmdPHIEU_THU")
                {
                    CallPhieuChi(grdPhieuChi);
                }
                if (e.Column.Key == "cmdHUY_PHIEUTHU")
                {
                    HuyPhieuchi(grdPhieuChi);
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
        private void HuyPhieuchi(GridEX grd)
        {
            ma_lydohuy = "";
            if (!Utility.isValidGrid(grd)) return;
            if (grd.CurrentRow != null)
            {
                if (objLuotkham == null)
                {
                    objLuotkham = CreatePatientExam();
                }
                if (objLuotkham.TrangthaiNoitru >= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
                {
                    Utility.ShowMsg("Bệnh nhân này đã ở trạng thái nội trú nên hệ thống không cho phép hủy phiếu chi ngoại trú nữa");
                    return;
                }
                v_Payment_ID = Utility.Int32Dbnull(grd.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1);
                KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_Payment_ID);

                if (objPayment != null)
                {
                    //Kiểm tra ngày hủy
                    int KCB_THANHTOAN_SONGAY_HUYPHIEUCHI = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SONGAY_HUYPHIEUCHI", "0", true), 0);
                    int Chenhlech = (int)Math.Ceiling((globalVariables.SysDate.Date - objPayment.NgayThanhtoan.Date).TotalDays);
                    if (Chenhlech > KCB_THANHTOAN_SONGAY_HUYPHIEUCHI)
                    {
                        Utility.ShowMsg(string.Format("Ngày lập phiếu chi {0} - Ngày hủy phiếu chi {1}. Hệ thống không cho phép bạn hủy phiếu chi đã quá {2} ngày. Cần liên hệ quản trị hệ thống để được trợ giúp", objPayment.NgayThanhtoan.ToString("dd/MM/yyyy"), globalVariables.SysDate.ToString("dd/MM/yyyy"), KCB_THANHTOAN_SONGAY_HUYPHIEUCHI.ToString()));
                        return;
                    }
                    if (PropertyLib._ThanhtoanProperties.Hienthihuyphieuchi)
                    {
                        frm_Tralaitien frm = new frm_Tralaitien();
                        frm.objLuotkham = objLuotkham;
                        frm.v_Payment_Id = Utility.Int32Dbnull(objPayment.IdThanhtoan, -1);
                        frm.Chuathanhtoan = _chuathanhtoan;
                        frm.TotalPayment = grdPayment.GetDataRows().Length;
                        frm.ShowCancel = true;
                        frm.ShowDialog();
                        if (!frm.m_blnCancel)
                        {
                            GetData();
                        }
                    }
                    else
                    {
                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_BATNHAPLYDO_HUYPHIEUCHI", "1", false) == "1")
                        {
                            frm_Chondanhmucdungchung _Nhaplydohuythanhtoan = new frm_Chondanhmucdungchung("LYDOHUYPHIEUCHI", "Hủy phiếu chi ", "Nhập lý do hủy phiếu chi trước khi thực hiện...", "Lý do Hủy phiếu chi",false);
                            _Nhaplydohuythanhtoan.ShowDialog();
                            if (_Nhaplydohuythanhtoan.m_blnCancel) return;
                            ma_lydohuy = _Nhaplydohuythanhtoan.ma;
                            lydo_huy = _Nhaplydohuythanhtoan.ten;
                        }
                        ActionResult actionResult = _THANHTOAN.HuyPhieuchi(objPayment, objLuotkham, lydo_huy);
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy phiếu chi của bệnh nhân có mã lần khám {0} và ID bệnh nhân là: {1} Tên= {2}", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan), newaction.CancelData, this.GetType().Assembly.ManifestModule.Name);
                                grd.CurrentRow.Delete();
                                ModifyCommand();
                                Utility.ShowMsg("Bạn hủy thông tin phiếu chi thành công", "Thông báo");
                                GetData();
                                break;
                            case ActionResult.Error:
                                Utility.ShowMsg("Lỗi trong quá trình hủy phiếu chi", "Thông báo lỗi", MessageBoxIcon.Error);
                                break;
                            case ActionResult.AssignIsConfirmed:
                                Utility.ShowMsg("Tồn tại dịch vụ CLS bạn chọn hủy đã được chuyển CLS và có kết quả nên không thể trả lại. Vui lòng kiểm tra nội bộ để xác nhận", "Thông báo lỗi", MessageBoxIcon.Error);
                                break;
                        }
                    }
                }
                ModifyCommand();
            }
        }

        /// <summary>
        ///     hàm thực hiện viecj lấy thông tin phiếu chi
        /// </summary>
        private void CallPhieuChi(GridEX grd)
        {
            try
            {
                frm_Tralaitien frm = new frm_Tralaitien();
                frm.objLuotkham = objLuotkham;
                frm.v_Payment_Id = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grd,KcbThanhtoan.Columns.IdThanhtoan), -1);
                frm.Chuathanhtoan = _chuathanhtoan;
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
                decimal tile_chietkhau = 0;
                decimal tien_chietkhau = 0;
                string kieu_chietkhau = "%";
                if (Utility.isValidGrid(grdThongTinChuaThanhToan) && (Utility.Int64Dbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["trangthai_thanhtoan"].Value, 1) == 1 || Utility.Int64Dbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["tthai_tamthu"].Value, 1) == 1))
                {

                    Utility.ShowMsg("Chi tiết bạn chọn đã được thanh toán(hoặc tạm thu) nên bạn không thể chiết khấu được nữa. Mời bạn kiểm tra lại");
                    e.Value = e.InitialValue;
                    return;
                }
                else
                {
                    if (e.Column.Key == "tile_chietkhau")
                    {
                        tile_chietkhau = Utility.DecimaltoDbnull(e.Value, 0);
                        //Tính lại tiền chiết khấu theo tỉ lệ %
                        if (tile_chietkhau > 100)
                        {
                            Utility.ShowMsg("Tỉ lệ chiết khấu không được phép vượt quá 100 %. Mời bạn kiểm tra lại");
                            e.Cancel = true;
                            return;
                        }
                        grdThongTinChuaThanhToan.CurrentRow.Cells["tien_chietkhau"].Value = Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["TT_BN_KHONG_PHUTHU"].Value, 0) * Utility.DecimaltoDbnull(e.Value, 0) / 100;
                        tien_chietkhau = Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["tien_chietkhau"].Value);
                    }
                    else
                    {
                        kieu_chietkhau = "T";
                        tien_chietkhau = Utility.DecimaltoDbnull(e.Value, 0);
                        if (tien_chietkhau > Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["TT_BN_KHONG_PHUTHU"].Value, 0))
                        {
                            Utility.ShowMsg("Tiền chiết khấu không được lớn hơn(>) tiền Bệnh nhân chi trả(" + Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["TT_BN_KHONG_PHUTHU"].Value, 0).ToString() + "). Mời bạn kiểm tra lại");
                            e.Cancel = true;
                            return;
                        }
                        grdThongTinChuaThanhToan.CurrentRow.Cells["tile_chietkhau"].Value = (Utility.DecimaltoDbnull(e.Value, 0) / Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["TT_BN_KHONG_PHUTHU"].Value, 0)) * 100;
                        tile_chietkhau = Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["tile_chietkhau"].Value);
                    }
                    grdThongTinChuaThanhToan.CurrentRow.Cells["thuc_thu"].Value = Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["TT_KHONG_PHUTHU"].Value, 0) - Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["tien_chietkhau"].Value, 0);
                    //Cập nhật luôn vào bảng trong CSDL để in bảng kê chi phí cho người bệnh xem trước khi thanh toán
                    byte id_loaithanhtoan = Utility.ByteDbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["id_loaithanhtoan"].Value);

                    long id_phieu = Utility.Int64Dbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["id_phieu"].Value);
                    long id_phieuchitiet = Utility.Int64Dbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["id_phieu_chitiet"].Value);
                    CapnhatChietkhau_DonGia(Convert.ToByte(0), id_loaithanhtoan, kieu_chietkhau, tile_chietkhau, tien_chietkhau, id_phieu, id_phieuchitiet);
                }
            }
            else if (e.Column.Key == KcbChidinhclsChitiet.Columns.DonGia)
            {
                if (!Utility.IsNumeric(e.Value.ToString()))
                {
                    Utility.ShowMsg("Bạn phải nhập thông tin đơn giá. Vui lòng nhập lại", "Thông báo", MessageBoxIcon.Warning);
                    e.Value = e.InitialValue;
                    return;
                }
                decimal dongia_cu = Utility.DecimaltoDbnull(e.InitialValue, 1);
                decimal dongia_moi = Utility.DecimaltoDbnull(e.Value);
                if (dongia_moi == 0)
                {
                    if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn đổi giá dịch vụ cls {0} thành 0 đồng hay không?", grdThongTinChuaThanhToan.GetValue("ten_chitietdichvu")), "Xác nhận giá 0 đồng", true))
                    {
                        e.Value = e.InitialValue;
                        return;
                    }
                }
                if (dongia_moi < 0)
                {
                    Utility.ShowMsg("Đơn giá phải >=0. Vui lòng nhập lại", "Thông báo", MessageBoxIcon.Warning);
                    e.Value = e.InitialValue;
                    return;
                }
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn đổi giá dịch vụ cls {0} từ {1} thành {2} hay không?", grdThongTinChuaThanhToan.GetValue("ten_chitietdichvu"), Utility.FormatCurrencyHIS(Utility.DecimaltoDbnull(e.InitialValue, 0)), Utility.FormatCurrencyHIS(Utility.DecimaltoDbnull(e.Value, 0))), "Xác nhận đổi giá", true))
                {
                    if (CapnhatDongia(grdThongTinChuaThanhToan.CurrentRow, 0, dongia_moi, true))
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Sửa đơn giá {0} từ {1} thành {2} thành công ", grdThongTinChuaThanhToan.GetValue("ten_chitietdichvu"), Utility.FormatCurrencyHIS(Utility.DecimaltoDbnull(e.InitialValue, 0)), Utility.FormatCurrencyHIS(Utility.DecimaltoDbnull(e.Value, 0))), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                    else
                    {
                        e.Value = e.InitialValue;
                        return;
                    }
                    
                }
            }
            ModifyCommand();
        }

        private void grdList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GetData();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
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
                    if (this.Args == "KN" && !Maluotkham.Contains("KN")) Maluotkham = "KN" + Maluotkham;
                    txtMaLanKham.Text = Maluotkham;
                    txtMaLanKham.Select(txtMaLanKham.Text.Length, txtMaLanKham.Text.Length);
                }
                if (!string.IsNullOrEmpty(txtMaLanKham.Text))
                {
                    _Malankham_keydown = true;
                    cmdSearch_Click(cmdSearch, new EventArgs());
                    _Malankham_keydown = false;
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
                    Utility.SetParameterValue(report, "NGUOIIN", globalVariables.gv_strTenNhanvien);
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
                        GetData();
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
                    Utility.SetParameterValue(report, "NGUOIIN", Utility.sDbnull(globalVariables.gv_strTenNhanvien, ""));

                    Utility.SetParameterValue(report, "ParentBranchName", globalVariables.ParentBranch_Name);
                    Utility.SetParameterValue(report, "BranchName", globalVariables.Branch_Name);
                    Utility.SetParameterValue(report, "DateTime", Utility.FormatDateTime(globalVariables.SysDate));
                    Utility.SetParameterValue(report, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                    Utility.SetParameterValue(report, "sTitleReport", tieude);
                    //report.SetParameterValue("CharacterMoney", new MoneyByLetter().sMoneyToLetter(TONG_TIEN.ToString()));
                    objForm.crptViewer.ReportSource = report;

                    if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInHoadon, PropertyLib._MayInProperties.PreviewInHoadon))
                    {
                        objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInHoadon, 1);
                        objForm.ShowDialog();
                    }
                    else
                    {
                        objForm.addTrinhKy_OnFormLoad();
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
                mnuSuaSoBienLai.Visible = IdHdonLog > 0  && Utility.Coquyen("thanhtoan_quyen_suasobienlai") ;
                mnuLayhoadondo.Visible = IdHdonLog <= 0  && Utility.Coquyen("thanhtoan_quyen_laysobienlai") ;
               // mnuHuyHoaDon.Visible = IdHdonLog > 0 && TrangthaiChot == 0;
                mnuInLaiBienLai.Visible = IdHdonLog > 0;
                if (TrangthaiChot == 0 && IdHdonLog <= 0)
                    //if (IdHdonLog > 0) -- không cho phép bỏ hoá đơn đã in vì lần thanh  toán khác có thể đã được thanh toán với số hoá đơn lớn hơn
                    //{
                    //    mnuLayhoadondo.Text = "Bỏ hóa đơn đỏ cho thanh toán đang chọn";
                    //    mnuLayhoadondo.Tag = 0;
                    //}
                    //else
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
                if (!blnLoaded) return;
                foreach (GridEXRow exRow in grdThongTinChuaThanhToan.GetDataRows())
                {
                   exRow.BeginEdit();
                    if (Utility.Int32Dbnull(exRow.Cells["Id_Loaithanhtoan"].Value, -1) ==
                        Utility.Int32Dbnull(grdDSKCB.GetValue("MA")))
                    {
                        if (Utility.Int32Dbnull(exRow.Cells["trangthai_huy"].Value, 0) == 0 &&
                           // Utility.Int32Dbnull(exRow.Cells["trang_thai"].Value, 0) == 0 && 
                                Utility.Int32Dbnull(exRow.Cells["trangthai_thanhtoan"].Value, 0) == 0)
                        {
                            exRow.CheckState = e.CheckState;
                           
                        }
                        else
                        {
                            exRow.IsChecked = false;
                            
                        }
                        exRow.Cells["colChon"].Value = Utility.Bool2byte(exRow.IsChecked);
                    }
                   
                    exRow.EndEdit();
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
                if (!blnLoaded) return;
                grdDSKCB.RowCheckStateChanged -= grdDSKCB_RowCheckStateChanged;
                foreach (GridEXRow row in grdDSKCB.GetDataRows())
                {
                    foreach (GridEXRow exRow in grdThongTinChuaThanhToan.GetDataRows())
                    {
                        exRow.BeginEdit();
                        if (Utility.Int32Dbnull(exRow.Cells["Id_Loaithanhtoan"].Value, -1) ==
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
                            exRow.Cells["colChon"].Value = Utility.Bool2byte(exRow.IsChecked);
                        }
                        exRow.EndEdit();
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

        private void InPhoiBhyt()
        {
            try
            {
                if (new INPHIEU_THANHTOAN_NGOAITRU().InPhoiBHYT(objLuotkham, m_dtPayment, pnlSuangayinphoi.Visible ? dtNgayInPhoi.Value : dtPaymentDate.Value))
                {
                    cbomayinphoiBHYT.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                    LaydanhsachLichsuthanhtoan_phieuchi();
                    KiemTraDaInPhoiBhyt();
                    cmdChuyenGiamDinh.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
            finally
            {
                GC.Collect();
            }
        }

        private KcbPhieuDct CreatePhieuDongChiTra()
        {
                KcbPhieuDct objPhieuDct = new KcbPhieuDct();
                objPhieuDct.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                objPhieuDct.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                objPhieuDct.KieuThanhtoan = 0;
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
                        .And(KcbPhieuDct.Columns.KieuThanhtoan).IsEqualTo(Utility.Int32Dbnull(KieuThanhToan.NgoaiTru))
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
        ///     hàm thực hiện  var lstIdchidinhchitiet = new List<string>();việc chuyển cận lâm sàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChuyenCLS_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.EnableButton(cmdChuyenCLS, false);
                Utility.WaitNow(this);
                var lstIdchidinhchitiet = new List<string>();
                if (objLuotkham != null)
                {
                    DataRow[] arrDr = m_dtChiPhiThanhtoan.Select("id_loaithanhtoan=2 and trangthai_chuyencls=0");
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
                        m_dtChiPhiThanhtoan.AcceptChanges();
                        int result = new Update(KcbChidinhclsChitiet.Schema)
                        .Set(KcbChidinhclsChitiet.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                        .Set(KcbChidinhclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                        .Set(KcbChidinhclsChitiet.Columns.TrangThai).EqualTo(1)
                        .Where(KcbChidinhclsChitiet.Columns.TrangThai).IsEqualTo(0)
                        .And(KcbChidinhclsChitiet.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(KcbChidinhclsChitiet.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .Execute();

                        #region send2HIS
                        //DataSet dsData =
                        //          SPs.HisLisLaydulieuchuyensangLis(dtInput_Date.Value.ToString("dd/MM/yyyy"),
                        //                                           objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).
                        //              GetDataSet();
                        //DataTable dt2Lis = dsData.Tables[1].Copy();
                        //List<long> lstIchidinh = (from q in grdThongTinChuaThanhToan.GetDataRows()
                        //                          select
                        //                              Utility.Int64Dbnull(
                        //                                  q.Cells["id_phieu"].Value, 0)).
                        //    ToList
                        //    <long>();
                        //List<DataRow> lstData2Send = (from p in dsData.Tables[0].AsEnumerable()
                        //                              where
                        //                                  lstIchidinh.Contains(
                        //                                      Utility.Int64Dbnull(
                        //                                          p.Field<Int64>("id_chidinh")))
                        //                                  && Utility.Int64Dbnull(p["trang_thai"], 0) == 1
                        //                              select p).ToList<DataRow>();
                        //List<DataRow> lstData2SendReal = (from p in dsData.Tables[1].AsEnumerable()
                        //                                   where
                        //                                       lstIchidinh.Contains(
                        //                                           Utility.Int64Dbnull(
                        //                                               p.Field<Int64>("id_chidinh")))
                        //                                       && Utility.Int64Dbnull(p["trang_thai"], 0) == 1
                        //                                   select p).ToList<DataRow>();
                        //if (lstData2Send.Any())
                        //{
                        //    dt2Lis = lstData2SendReal.CopyToDataTable();
                        //    lstIdchidinhchitiet = (from p in lstData2Send
                        //                           select
                        //                               Utility.sDbnull(
                        //                                  p.Field<Int64>("id_chidinh"), 0))
                        //        .
                        //        Distinct().ToList();
                        //    int recoder =
                        //        RocheCommunication.WriteOrderMessage(
                        //            THU_VIEN_CHUNG.Laygiatrithamsohethong("ASTM_ORDERS_FOLDER",
                        //                                                  @"\\192.168.1.254\Orders", false),
                        //            dt2Lis);
                        //    if (recoder == 0) //Thành công
                        //    {
                        //        SPs.HisLisCapnhatdulieuchuyensangLis(
                        //            string.Join(",", lstIdchidinhchitiet.ToArray()), 2, 1).Execute();
                        //        dsData.Tables[0].AsEnumerable()
                        //            .Where(
                        //                c =>
                        //                lstIdchidinhchitiet.Contains(
                        //                    Utility.sDbnull(
                        //                        c.Field<long>("id_chidinh"))))
                        //            .ToList()
                        //            .ForEach(c1 =>
                        //            {
                        //                c1["trang_thai"] = 2;
                        //                //   c1["ten_trangthai"] = "Đang thực hiện";
                        //            });
                        //        dsData.Tables[1].AsEnumerable()
                        //            .Where(
                        //                c =>
                        //                lstIdchidinhchitiet.Contains(
                        //                    Utility.sDbnull(
                        //                        c.Field<long>("id_chidinh"))))
                        //            .ToList()
                        //            .ForEach(c1 =>
                        //            {
                        //                c1["trang_thai"] = 2;
                        //                // c1["ten_trangthai"] = "Đang thực hiện";
                        //            });
                        //        dsData.AcceptChanges();
                        //        Utility.SetMsg(lblMessage,
                        //                       string.Format(
                        //                           "Các dữ liệu dịch vụ cận lâm sàng của Bệnh nhân đã được gửi thành công sang LIS"),
                        //                       false);
                        //    }
                        //}
                        #endregion

                        Utility.SetMsg(lblMessage, string.Format("Bạn vừa chuyển CLS thành công {0} dịch vụ", result.ToString()), false);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
            finally
            {
                TuybiennutchuyenCLS();
                Utility.DefaultNow(this);
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

        /// <summary>
        ///     hàm thực hiện việc hiển thị thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDungChuyenCLS_Click(object sender, EventArgs e)
        {
            bool hasFound = false;
            try
            {
                Utility.EnableButton(cmdDungChuyenCLS, false);
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
                DataRow[] arrDr = m_dtChiPhiThanhtoan.Select("id_loaithanhtoan=2 and trangthai_chuyencls=1");
                foreach (DataRow dr in arrDr)
                {
                    dr["trangthai_chuyencls"] = 0;
                }
                m_dtChiPhiThanhtoan.AcceptChanges();
               int result= new Update(KcbChidinhclsChitiet.Schema)
                .Set(KcbChidinhclsChitiet.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                .Set(KcbChidinhclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                .Set(KcbChidinhclsChitiet.Columns.TrangThai).EqualTo(0)
                .Where(KcbChidinhclsChitiet.Columns.TrangThai).IsEqualTo(2)
                .And(KcbChidinhclsChitiet.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(KcbChidinhclsChitiet.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .Execute();
               Utility.SetMsg(lblMessage, string.Format("Bạn vừa hủy chuyển CLS thành công {0} dịch vụ", result.ToString()), false);
               
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:"+ exception.Message);
            }
            finally
            {
                TuybiennutchuyenCLS();
                Utility.DefaultNow(this);
            }
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
                    RefreshData(frm.objLuotkhamMoi, frm.txtNoidangkyKCBBD.Text);
                    UpdateGroup();
                    GetData();
                   
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
                DataRow[] arrDr =
                    m_dtDataTimKiem.Select(KcbLuotkham.Columns.IdBenhnhan + "=" + objNew.IdBenhnhan + " AND " +
                                           KcbLuotkham.Columns.MaLuotkham + "='" + objNew.MaLuotkham + "'");
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
            byte kieuthanhtoan = Utility.ByteDbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.KieuThanhtoan].Value, 0);
            if (kieuthanhtoan == 0 || kieuthanhtoan == 5)
                InHoadon();
            else
                new INPHIEU_THANHTOAN_NGOAITRU().InPhieuchi(Utility.Int32Dbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1));
            string seria = Utility.sDbnull(grdPayment.GetValue(KcbThanhtoan.Columns.Serie), "");
            if (seria != "")
            {
                InHoaDon_BanHang();
            }
            
        }

        void InHoaDon_BanHang()
        {
            try
            {
                int _Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
                new INPHIEU_THANHTOAN_NGOAITRU().InHoaDon_BanHang(_Payment_ID,v_bytNoitru);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi trong quá trình in hóa đơn\n" + ex.Message, "Thông báo lỗi");
                log.Trace(ex.Message);
            }
            
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
                log.Trace(ex.Message);
            }
        }
        void InPhieuchi()
        {
            try
            {
                int _Payment_ID = Utility.Int32Dbnull(grdPhieuChi.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
                decimal TONG_TIEN = Utility.Int32Dbnull(grdPhieuChi.CurrentRow.Cells["BN_CT"].Value, -1);
                ActionResult actionResult = new KCB_THANHTOAN().Capnhattrangthaithanhtoan(_Payment_ID);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        new INPHIEU_THANHTOAN_NGOAITRU().InPhieuchi(_Payment_ID);
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình in phiếu chi", "Thông báo lỗi", MessageBoxIcon.Warning);
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
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
        /// <param name="TONG_TIEN"></param>
        private void PrintPhieuThu_YHHQ(string sTitleReport, decimal TONG_TIEN)
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
            Utility.SetParameterValue(crpt,"CharacterMoney", new MoneyByLetter().sMoneyToLetter(TONG_TIEN.ToString()));
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
                    objLuotkham = CreatePatientExam();
                }
                if (objLuotkham != null)
                {
                    if (!BAOCAO_BHYT.BhytKiemtratruockhiHuyinphoiBHYT(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham))
                    {
                        Utility.ShowMsg("Bệnh nhân đã được cấp phát thuốc nên bạn không được phép hủy in phôi BHYT. Mời bạn kiểm tra lại");
                        return;
                    }

                    ActionResult actionResult = _THANHTOAN.UpdateHuyInPhoiBHYT(objLuotkham,
                        KieuThanhToan.NgoaiTru);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            KiemTraDaInPhoiBhyt();
                            Utility.Log(this.Name, globalVariables.UserName,
                               string.Format("Hủy in phôi của bệnh nhân có mã lần khám {0} và ID bệnh nhân là: {1} Tên= {2}", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
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
        private void GetThongtincanhbao(int patientId)
        {
            try
            {
                Utility.SetMsg(lblwarningMsg, "", false);

                cmdsave.Enabled = true;
                var lst =
                    new Select().From(DmucCanhbao.Schema)
                        .Where(DmucCanhbao.MaBnColumn)
                        .IsEqualTo(patientId)
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
                    var newItem = new DmucCanhbao();
                    newItem.CanhBao = txtCanhbao.Text.TrimStart().TrimEnd();
                    newItem.MaBn = Utility.Int32Dbnull(txtPatient_ID.Text, -1);
                    newItem.NgayTao = globalVariables.SysDate.Date;
                    newItem.NguoiTao = globalVariables.UserName;
                    newItem.IsNew = true;
                    newItem.Save();
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



        void SaveUserConfigs()
        {
            Utility.SaveUserConfig(chkHoixacnhanhuythanhtoan.Tag.ToString(), Utility.Bool2byte(chkHoixacnhanhuythanhtoan.Checked));
            Utility.SaveUserConfig(chkHoixacnhanthanhtoan.Tag.ToString(), Utility.Bool2byte(chkHoixacnhanthanhtoan.Checked));
            Utility.SaveUserConfig(chkPreviewHoadon.Tag.ToString(), Utility.Bool2byte(chkPreviewHoadon.Checked));
            Utility.SaveUserConfig(chkPreviewInBienlai.Tag.ToString(), Utility.Bool2byte(chkPreviewInBienlai.Checked));
            Utility.SaveUserConfig(chkPreviewInphoiBHYT.Tag.ToString(), Utility.Bool2byte(chkPreviewInphoiBHYT.Checked));
            Utility.SaveUserConfig(chkTudonginhoadonsauthanhtoan.Tag.ToString(), Utility.Bool2byte(chkTudonginhoadonsauthanhtoan.Checked));

            Utility.SaveUserConfig(chkViewtruockhihuythanhtoan.Tag.ToString(), Utility.Bool2byte(chkViewtruockhihuythanhtoan.Checked));
            Utility.SaveUserConfig(chkRestoreDefaultPTTT.Tag.ToString(), Utility.Bool2byte(chkRestoreDefaultPTTT.Checked));


        }
        /// <summary>
        ///     hàmh tực hiện việc dóng form lưu lại thông tin cấu hình
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_THANHTOAN_NOITRU_V1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
            Utility.FreeLockObject(txtPatient_Code.Text);
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
        private bool IsValidCancelData()
        {
            if (grdThongTinDaThanhToan.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn cần chọn các dịch vụ cần trả lại tiền ", "Thông báo", MessageBoxIcon.Warning);
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
                        if (objKcbDangkyKcb != null && Utility.Byte2Bool( objKcbDangkyKcb.TrangThai))
                        {
                            Utility.ShowMsg(
                                "Dịch vụ khám đã thực hiện,Bạn không thể hủy, Mời bạn xem lại\n Mời bạn chọn những bản ghi chưa làm dịch vụ hoàn trả",
                                "Thông báo", MessageBoxIcon.Warning);
                            grdThongTinDaThanhToan.Focus();
                            return false;
                        }
                        if (objKcbDangkyKcb != null && Utility.Byte2Bool(objKcbDangkyKcb.TrangthaiHuy))
                        {
                            Utility.ShowMsg(
                                "Dịch vụ khám đã trả lại tiền,Bạn không thể trả lại tiếp, Mời bạn xem lại\n Mời bạn chọn những bản ghi chưa làm dịch vụ hoàn trả",
                                "Thông báo", MessageBoxIcon.Warning);
                            grdThongTinDaThanhToan.Focus();
                            return false;
                        }
                        break;
                    case 2:
                        KcbChidinhclsChitiet objKcbChidinhclsChitiet = KcbChidinhclsChitiet.FetchByID(IdPhieuChitiet);
                        if (objKcbChidinhclsChitiet != null && objKcbChidinhclsChitiet.TrangThai>=3)
                        {
                            Utility.ShowMsg(
                                "Dịch vụ cận lâm sàng đã trả kết quả,Bạn không thể hủy, Mời bạn xem lại \n  Mời bạn chọn những bản ghi chưa thực hiện",
                                "Thông báo", MessageBoxIcon.Warning);
                            grdThongTinDaThanhToan.Focus();
                            return false;
                        }
                        if (objKcbChidinhclsChitiet != null && Utility.Byte2Bool( objKcbChidinhclsChitiet.TrangthaiHuy))
                        {
                            Utility.ShowMsg(
                                "Dịch vụ cận lâm sàng đã hủy,Bạn không thể hủy, Mời bạn xem lại \n  Mời bạn chọn những bản ghi chưa hủy thông tin",
                                "Thông báo", MessageBoxIcon.Warning);
                            grdThongTinDaThanhToan.Focus();
                            return false;
                        }
                        break;
                    case 3:
                    case 5:
                        KcbDonthuocChitiet objKcbDonthuocChitiet = KcbDonthuocChitiet.FetchByID(IdPhieuChitiet);
                        if (objKcbDonthuocChitiet != null && Utility.Byte2Bool( objKcbDonthuocChitiet.TrangThai))
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
                        objLuotkham = CreatePatientExam();
                    }
                    if (objLuotkham.TrangthaiNoitru >= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
                    {
                        Utility.ShowMsg("Bệnh nhân này đã ở trạng thái nội trú nên hệ thống không cho phép trả lại tiền ngoại trú nữa");
                        return;
                    }
                    KcbThanhtoan objPayment = TaophieuThanhtoanHuy();
                    string[] query = (from p in grdThongTinDaThanhToan.GetCheckedRows()
                                      select Utility.sDbnull(p.Cells["ten_chitietdichvu"].Value, "")).ToArray();
                    string noidung = string.Join(";", query);
                    frm_Chondanhmucdungchung _Chondanhmucdungchung = new frm_Chondanhmucdungchung("LYDOTRATIEN", "TRẢ TIỀN LẠI CHO BỆNH NHÂN", "Chọn lý do trả trước khi thực hiện", "Lý do trả tiền",false);
                    _Chondanhmucdungchung.Lydomacdinh = string.Format("Người bệnh trả lại: {0}", noidung);
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
                                CallPhieuChi(grdPhieuChi);
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

        private KcbThanhtoan TaophieuThanhtoanHuy()
        {
           
            KcbThanhtoan objPayment = new KcbThanhtoan();
            objPayment.MaLuotkham = Utility.sDbnull(txtPatient_Code.Text, "");
            objPayment.IdBenhnhan = Utility.Int32Dbnull(txtPatient_ID.Text, -1);
            objPayment.NgayTao = globalVariables.SysDate;
            objPayment.NguoiTao = globalVariables.UserName;
            objPayment.KieuThanhtoan = 1; //0=Thanh toán thường;1= trả lại tiền;2= thanh toán bỏ viện
            objPayment.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
            objPayment.NoiTru = v_bytNoitru;
            objPayment.TrangthaiIn = 0;
            objPayment.NgayIn = null;
            objPayment.NguoiIn = string.Empty;
            objPayment.NgayTonghop = null;
            objPayment.NguoiTonghop = string.Empty;
            objPayment.NgayChot = null;
            objPayment.TrangthaiChot = 0;
            objPayment.MaPttt = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_TRALAITIEN_PTTT_MACDINH", "TM", false);
            objPayment.MaThanhtoan = THU_VIEN_CHUNG.TaoMathanhtoan(globalVariables.SysDate);
            objPayment.NgayThanhtoan = globalVariables.SysDate;
            objPayment.IdNhanvienThanhtoan = globalVariables.gv_intIDNhanvien;
            objPayment.IpMaytao = globalVariables.gv_strIPAddress;
            objPayment.TenMaytao = globalVariables.gv_strComputerName;
            return objPayment;
        }

        private List<Int64> TaodulieuthanhtoanchitietHuy()
        {
            List<Int64> lstIdChitiet = (from q in grdThongTinDaThanhToan.GetCheckedRows()
                     select Utility.Int64Dbnull(q.Cells[KcbThanhtoanChitiet.Columns.IdChitiet].Value)).ToList<Int64>();
            return lstIdChitiet;
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
                CallPhieuChi(grdPhieuChi);
            }
        }

        #endregion

        private void cmdDanhsachinphoi_Click(object sender, EventArgs e)
        {
            //var frm = new VMS.HIS.BHYT.FrmDanhsachBenhnhanInphoiBhyt();
            //frm.ShowDialog();
        }
        private void FilterThanhToan()
        {
            try
            {
                string _rowFilter = "1=1";
                if (radChuathanhtoan.Checked) _rowFilter = string.Format("{0}={1}", "trangthai_thanhtoan", 0);
                if (radDaThanhtoan.Checked) _rowFilter = string.Format("{0}={1}", "trangthai_thanhtoan", 1);
                m_dtDataTimKiem.DefaultView.RowFilter = _rowFilter;
                m_dtDataTimKiem.AcceptChanges();
            }
            catch (Exception)
            {
                Utility.ShowMsg("Lỗi trong quá trình Defaultview");
            }

        }
        private void radTatca_CheckedChanged(object sender, EventArgs e)
        {
            FilterThanhToan();
        }

        private void radDaThanhtoan_CheckedChanged(object sender, EventArgs e)
        {
            FilterThanhToan();
        }

        private void radChuathanhtoan_CheckedChanged(object sender, EventArgs e)
        {
            FilterThanhToan();
        }
        //VMS.HIS.BHYT.Class.KCB_BHYT kcbBhyt = new VMS.HIS.BHYT.Class.KCB_BHYT();
        //private void cmdChuyenGiamDinh_Click(object sender, EventArgs e)
        //{
        //    cmdChuyenGiamDinh.Enabled = false;
        //    Thread.Sleep(80);
        //    bool kttemp = false;

        //    bool ktxml = false;
        //    if (txtPatient_Code.Text.Trim() != "")
        //    {
        //       kttemp = kcbBhyt.ProcessCreateTemp(Utility.sDbnull(txtPatient_Code.Text.Trim()), Utility.Int64Dbnull(txtPatient_ID.Text));
             
        //    }
        //    else
        //    {
        //        cmdChuyenGiamDinh.Enabled = true;
        //    }
        //    if (kttemp)
        //    {
        //        ktxml = kcbBhyt.ProcessCreateXml(Utility.sDbnull(txtPatient_Code.Text.Trim()), Utility.sDbnull(PathXml),
        //            Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong_off("XML_CONGVAN", "3", false)));
        //        if (ktxml)
        //        {
        //            new Update(KcbPhieuDct.Schema).Set(KcbPhieuDct.Columns.TrangthaiXml)
        //                .EqualTo(2)
        //                .Where(KcbPhieuDct.Columns.MaLuotkham)
        //                .IsEqualTo(txtPatient_Code.Text.Trim())
        //                .Execute();
        //            KiemTraDaInPhoiBhyt();
        //        }
        //    }
        //    else
        //    {
        //        cmdChuyenGiamDinh.Enabled = true;
        //    }
        //    Thread.Sleep(50);
        //}
        void KeDichvukhac()
        {
            DataRow[] arrDr = m_dtChiPhiThanhtoan.Select(KcbThanhtoanChitiet.Columns.IdLoaithanhtoan + "=9");
            if (arrDr.Length <= 0)
                ThemMoiDichVu();
            else
                CapnhatChiPhiDichVu(Utility.Int64Dbnull(arrDr[0]["id_phieu"], 0));
        }
        private void cmdNhapDichVu_Click(object sender, EventArgs e)
        {
            KeDichvukhac();
        }
        private void ThemMoiDichVu()
        {
            try
            {
                frm_KCB_CHIDINH_CLS frm = new frm_KCB_CHIDINH_CLS("-GOI,-TIEN,-CHIPHITHEM", 0);
                frm.txtAssign_ID.Text = @"-100";
                frm.Exam_ID = Utility.Int32Dbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["id_kham"].Value, -1);
                frm.objLuotkham = objLuotkham;
                frm.objBenhnhan = objBenhnhan;
                frm.objPhieudieutriNoitru = null;
                frm.m_eAction = action.Insert;
                frm.txtAssign_ID.Text = @"-1";
                frm.noitru = v_bytNoitru;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    GetData();
                }
            }
            catch (Exception ex)
            {
                log.Trace("Loi:" + ex.Message);
                //throw;
            }
            finally
            {
                txtMaLanKham.Focus();
                txtMaLanKham.SelectAll();
            }
        }

        private void CapnhatChiPhiDichVu(long idChidinh)
        {
            try
            {
                frm_KCB_CHIDINH_CLS frm = new frm_KCB_CHIDINH_CLS("-GOI,-TIEN,-CHIPHITHEM", 0);
                frm.txtAssign_ID.Text = @"-100";
                frm.Exam_ID = Utility.Int32Dbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["id_kham"].Value, -1);
                frm.objLuotkham = objLuotkham;
                frm.objBenhnhan = objBenhnhan;
                frm.objPhieudieutriNoitru = null;
                frm.m_eAction = action.Update;
                frm.txtAssign_ID.Text = idChidinh.ToString();
                frm.noitru = v_bytNoitru;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    GetData();
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                txtMaLanKham.Focus();
                txtMaLanKham.SelectAll();
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
        private bool ExistsDonThuoc()
        {
            try
            {
                string kenhieudon = THU_VIEN_CHUNG.Laygiatrithamsohethong("KE_NHIEU_DON", "N", false);
                var lstPres =
                    new Select()
                        .From(KcbDonthuoc.Schema)
                        .Where(KcbDonthuoc.MaLuotkhamColumn).IsEqualTo(Utility.sDbnull(objLuotkham.MaLuotkham))
                        .And(KcbDonthuoc.IdBenhnhanColumn).IsEqualTo(Utility.sDbnull(objLuotkham.IdBenhnhan)).
                        ExecuteAsCollection<KcbDonthuocCollection>();

                IEnumerable<KcbDonthuoc> lstPres1 = from p in lstPres
                                                    where p.IdKham == objkcbdangky.IdKham
                                                    select p;
                if (objLuotkham.MaDoituongKcb == "BHYT")
                {
                    if (kenhieudon == "Y" && lstPres1.Count() <= 0) //Được phép kê mỗi phòng khám 1 đơn thuốc
                        return false;
                    if (kenhieudon == "N" && lstPres.Count > 0 && lstPres1.Count() <= 0)
                    //Cảnh báo ko được phép kê đơn tiếp
                    {
                        Utility.ShowMsg(
                            "Chú ý: Bệnh nhân này thuộc đối tượng BHYT và đã được kê đơn thuốc tại phòng khám khác. Bạn cần trao đổi với Quản trị hệ thống để được cấu hình kê đơn thuốc tại nhiều phòng khác khác nhau với đối tượng BHYT này",
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
        private DataTable dt_ICD = new DataTable();
        private DataTable dt_ICD_PHU = new DataTable();
        KcbDangkyKcb objkcbdangky = new KcbDangkyKcb();
        KcbChandoanKetluan _KcbChandoanKetluan = new KcbChandoanKetluan();
        private void cmdCreatePres_Click(object sender, EventArgs e)
        {
            //if (cmdThanhToan.Enabled == false && txtObjectType_Code.Text.Trim() == "BHYT")
            //{
            //    Utility.ShowMsg("Bệnh nhân đã được thanh toán! Không thể kê thêm thuốc được");
            //}
            //else
            //{
            //    frm_KCB_THAMKHAM frm = new frm_KCB_THAMKHAM("ALL");
            //    frm.ma_luotkham = txtPatient_Code.Text;
            //    frm.ShowDialog();
            //    if (!frm.m_blnCancel)
            //    {
            //        GetData();
            //    }
            //}
        }

        private void ctxBienlai_Opening(object sender, CancelEventArgs e)
        {

        }

        private void cmdLoadLaiHoaDon_Click(object sender, EventArgs e)
        {
            LoadInvoiceInfo();
        }

        private void cmdThanhToan_Click_1(object sender, EventArgs e)
        {

        }

        private void mnuLayhoadondo_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdupdatethongtinngay_Click(object sender, EventArgs e)
        {
            var frm = new FrmUpdateNgaykham(Utility.sDbnull(txtPatient_Code.Text.Trim()));
            frm.ShowDialog();
        }

        private void cmdInBienlai_Click_1(object sender, EventArgs e)
        {

        }

        private void cbomayinhoadonNhiet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._MayInProperties.TenMayInBienlai_Nhiet = cbomayinhoadonNhiet.Text;
        }

        private void mnuHuyChietkhau_Click_1(object sender, EventArgs e)
        {

        }

        private void mnuCapnhatPTTT_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdChiphithem_Click_1(object sender, EventArgs e)
        {

        }

        private void mnuTonghopravien_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPatient_Code.Text))
                {
                    frm_Xemtonghopchiphi _Xemtonghopchiphi = new frm_Xemtonghopchiphi(false, "-1");

                    _Xemtonghopchiphi.objLuotkham = objLuotkham;
                    _Xemtonghopchiphi.ShowDialog();

                    //frm_TonghopRavien frm = new frm_TonghopRavien("0");//0= khoa tổng hợp
                    //frm.ma_luotkham = Utility.sDbnull(txtPatient_Code.Text, "");
                    //frm.ShowDialog();
                    //if (!frm.bCancel)
                    //{
                    //    GetData();
                    //}

                }
                else
                {
                    Utility.ShowMsg("Bạn chưa chọn bệnh nhân để xem tổng hợp ra viện!", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Trace("Loi: " + ex.Message);
            }
        }

        private void chkPercent_CheckedChanged(object sender, EventArgs e)
        {
            lbltilemiengiam.Text = chkPercent.Checked ? "Nhập % miễn giảm cho tất cả dịch vụ" : "Nhập số tiền miễn giảm cho tất cả dịch vụ";
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void cmdChapnhanpheduyet_Click(object sender, EventArgs e)
        {
            try
            {
                objLuotkham = Utility.getKcbLuotkham(objLuotkham);
                if (objLuotkham.TrangthaiNoitru <= 3)
                {
                    Utility.ShowMsg("Người bệnh mới được cho ra viện và chưa duyệt dữ liệu tại khoa điều trị cuối cùng nên chưa thể thanh toán.");
                    return;
                }
                if (objLuotkham.TrangthaiNoitru >= 6)
                {
                    Utility.ShowMsg("Người bệnh đã được thanh toán và hoàn tất việc ra viện nên bạn không được phép phê duyệt tiếp.");
                    return;
                }
                byte tthai_noitru = 1;
                byte tthai_noitru_cu = objLuotkham.TrangthaiNoitru;
                if (optKhoachuaduyet.Checked)
                    tthai_noitru = 3;//Mới cho ra viện và chưa xác nhận khoa tổng hợp
                if (optKhoadaduyet.Checked)
                    tthai_noitru = 4;//Đưa về trạng thái khoa đã xác nhận
                if (optChuyenthanhtoan.Checked)
                    tthai_noitru = 5;//Chuyển cho thu ngân thanh toán ra viện
                objLuotkham.TrangthaiNoitru = tthai_noitru;
                if (objLuotkham.TrangthaiNoitru <= 3)
                    objLuotkham.TthaiThopNoitru = 0;
                else
                    objLuotkham.TthaiThopNoitru = 1;

                if (tthai_noitru_cu>3 && objLuotkham.TrangthaiNoitru == 3 && !Utility.Coquyen("NOITRU_QUYEN_HUYTONGHOPRAVIEN"))
                {
                    Utility.ShowMsg("Bạn không được cấp quyền Chuyển về khoa (mã quyền:NOITRU_QUYEN_HUYTONGHOPRAVIEN). Đề nghị liên hệ quản trị hệ thống để được phân quyền");
                    return ;
                }
                objLuotkham.Save();
                Utility.ShowMsg(String.Format("Bạn đã {0}", tthai_noitru == 3 ? "chuyển dữ liệu về trạng thái khoa chưa tổng hợp. Vui lòng thông báo khoa điều trị cuối cùng xem xét lại dữ liệu" : (tthai_noitru == 4 ? "chuyển dữ liệu về trạng thái khoa đã tổng hợp. Vui lòng kiểm tra kĩ thông tin trước khi xét duyệt lại cho bộ phận thu ngân thanh toán" : "xét duyệt dữ liệu để chuyển tới bộ phận thu ngân viên thanh toán ra viện cho bệnh nhân")));
            }
            catch (Exception ex)
            {


            }
            finally
            {
                Utility.SetMsg(lblTrangthainoitru, Utility.Laythongtintrangthainguoibenh(objLuotkham), false);
                ModifyCommand();
                ModifyHoanUngButtons();
            }
        }

        private void mnuHoanung_Click(object sender, EventArgs e)
        {
            try
            {
                NoitruTamung _tamung = new Select().From(NoitruTamung.Schema)
                    .Where(NoitruTamung.Columns.TrangThai).IsEqualTo(0)
                    .And(NoitruTamung.Columns.KieuTamung).IsEqualTo(0)
                    .AndExpression(NoitruTamung.Columns.IdThanhtoan).IsNull().OrExpression(NoitruTamung.Columns.IdThanhtoan).IsEqualTo(-1).CloseExpression()
                    .ExecuteSingle<NoitruTamung>();
                if (_tamung == null)
                {
                    if (Utility.AcceptQuestion("Hệ thống phát hiện Người bệnh đã được hoàn ứng hết. Bạn có muốn in phiếu hoàn ứng hay không?", "Thông báo", true))
                    {
                        Inphieuhoanung();
                    }
                    return;
                }
                else//Thực hiện hoàn ứng và in phiếu hoàn ứng
                {
                    if (!Utility.isValidGrid(grdPayment))
                    {
                        Utility.ShowMsg("Bạn cần chọn bản ghi thanh toán để thực hiện hoàn ứng theo lần thanh toán này");
                        return;
                    }
                    if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn hoàn ứng cho người bệnh {0}", txtTenBenhNhan.Text), "Thông báo", true))
                    {
                        string maphieu = THU_VIEN_CHUNG.SinhmaVienphi("HKQ");
                        long id_thanhtoan = Utility.Int64Dbnull(grdPayment.GetValue("id_thanhtoan"), -1);
                        KcbThanhtoan objTT = KcbThanhtoan.FetchByID(id_thanhtoan);
                        if (objTT != null)
                        {
                            SPs.NoitruHoanung(id_thanhtoan, maphieu, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objTT.NgayThanhtoan,
                        globalVariables.gv_intIDNhanvien, globalVariables.UserName,
                        Utility.Int32Dbnull(objLuotkham.IdKhoanoitru, -1), Utility.Int64Dbnull(objLuotkham.IdRavien, -1),
                        Utility.Int32Dbnull(objLuotkham.IdBuong, -1), Utility.Int32Dbnull(objLuotkham.IdGiuong, -1),
                        (byte)1, "TM", "").Execute();
                        }
                        Inphieuhoanung();
                    }
                }

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void cmdchucnang_Click(object sender, EventArgs e)
        {
            ctxChucnangkhac.Show(cmdchucnang, new Point(0, cmdchucnang.Height));
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Phanbo();
        }

        private void TabDiagInfo_SelectedTabChanged(object sender, TabEventArgs e)
        {

        }

        private void mnuKethem_Click(object sender, EventArgs e)
        {
            KeChiphithem();
        }

        private void cmdTonghop_Click(object sender, EventArgs e)
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

        private void mnuInchiphichuathanhtoan_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtData = SPs.NoitruTonghopChiphiRavien(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan, 1, "-1", "-1").GetDataSet().Tables[0];
                new INPHIEU_THANHTOAN_NGOAITRU().Inbienlai_DichvuChuathanhtoan(dtData, true, 1);
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void mnuPrintHU_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdPayment)) return;
                long id_thanhtoan = Utility.Int64Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
                DataTable dtHU = new Select().From(NoitruTamung.Schema)
                    .Where(NoitruTamung.Columns.IdThanhtoan).IsEqualTo(id_thanhtoan)
                    .And(NoitruTamung.Columns.KieuTamung).IsEqualTo(1).ExecuteDataSet().Tables[0];
                if (dtHU.Rows.Count > 0)
                {
                    globalVariables.MaphieuHoanung = Utility.sDbnull(dtHU.Rows[0]["code"], "");
                    if (globalVariables.MaphieuHoanung != "")
                        Inphieuhoanung();
                    else
                        Utility.ShowMsg("Không tìm được mã hoàn ứng. Vui lòng kiểm tra lại");
                }
                else
                    Utility.ShowMsg("Bản ghi thanh toán này không có dữ liệu hoàn ứng. Vui lòng kiểm tra lại");
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void mnuFixError_Click(object sender, EventArgs e)
        {
            if (!globalVariables.isSuperAdmin) return;
            long IdThanhtoan = Utility.Int64Dbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1);
            KcbThanhtoan objThanhtoan = KcbThanhtoan.FetchByID(IdThanhtoan);
            frm_CheckError _CheckError = new frm_CheckError(IdThanhtoan, v_bytNoitru, lst_IDLoaithanhtoan);
            _CheckError.objLuotkham = this.objLuotkham;
            _CheckError.ShowDialog();
        }

        private void mnuInlaigiayRavien_Click(object sender, EventArgs e)
        {
            try
            {

                Utility.WaitNow(this);
                DataTable dtData =
                    SPs.NoitruInphieuravien(Utility.DoTrim(objLuotkham.MaLuotkham)).GetDataSet().Tables[0];

                if (dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg(string.Format( "Không tìm thấy thông tin phiếu ra viện của người bệnh với mã lượt khám: {0}",objLuotkham.MaLuotkham), "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                THU_VIEN_CHUNG.CreateXML(dtData, "noitru_phieuravien.XML");
                Utility.UpdateLogotoDatatable(ref dtData);
                string StaffName = globalVariables.gv_strTenNhanvien;
                if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;

                string tieude = "", reportname = "";
                ReportDocument crpt = Utility.GetReport("noitru_phieuravien", ref tieude, ref reportname);
                if (crpt == null) return;
                var objForm = new frmPrintPreview(tieude, crpt, true,
                    dtData.Rows.Count <= 0 ? false : true);
                crpt.SetDataSource(dtData);

                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.nguoi_thuchien = Utility.sDbnull(dtData.Rows[0]["ten_bacsi_chuyenvien"], "");
                objForm.mv_sReportCode = "noitru_phieuravien";
                Utility.SetParameterValue(crpt, "StaffName", StaffName);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(Convert.ToDateTime(dtData.Rows[0]["ngay_ravien"].ToString())));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                          Utility.getTrinhky(objForm.mv_sReportFileName, globalVariables.SysDate));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
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

        private void mnuSuagia_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("thanhtoan_tanggiam_tile_dongia"))
                {
                    Utility.thongbaokhongcoquyen("thanhtoan_tanggiam_tile_dongia", " sửa giá dịch vụ");
                    return;
                }

                grdThongTinChuaThanhToan.RootTable.Columns["don_gia"].EditType = Utility.Coquyen("thanhtoan_tanggiam_tile_dongia") ? EditType.TextBox : EditType.NoEdit;
                Utility.focusCellofCurrentRow(grdThongTinChuaThanhToan, "don_gia");
            }
            catch (Exception ex)
            {

            }
        }

        private void mnuTamthu_Click(object sender, EventArgs e)
        {

            Tamthu();
        }
        void Tamthu()
        {
            try
            {
                DataRow[] arrDr = m_dtChiPhiThanhtoan.Select("trangthai_huy=0 and tthai_tamthu=0 and trangthai_thanhtoan=0");
                DataTable dtData = m_dtChiPhiThanhtoan.Clone();
                if (arrDr.Length <= 0)
                {
                    Utility.ShowMsg("Toàn bộ các dịch vụ bạn đang nhìn thấy đã được thanh toán hoặc tạm thu. Vui lòng chọn lại người bệnh để làm mới lại các dữ liệu có thể tạm thu");
                    return;
                }
                dtData = arrDr.CopyToDataTable();
                frm_tamthu _tamthu = new frm_tamthu(dtData, this.log, this.v_bytNoitru, this.lst_IDLoaithanhtoan);
                _tamthu.objLuotkham = this.objLuotkham;
                _tamthu.ShowDialog();
                if (!_tamthu.isCancel && _tamthu.v_Payment_ID > 0)
                {
                    SimpleRefresh(_tamthu.v_Payment_ID);

                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void SimpleRefresh(long v_Payment_ID)
        {
            LaydanhsachLichsuthanhtoan_phieuchi();
            GetDataChiTiet();
            Utility.GotoNewRowJanus(grdPayment, KcbThanhtoan.Columns.IdThanhtoan, v_Payment_ID.ToString());
            if (v_Payment_ID <= 0)
            {
                grdPayment.MoveFirst();
            }

            SetSumTotalProperties();
        }
        private void mnuKetchuyen_Click(object sender, EventArgs e)
        {
            KetchuyenTamthu();
        }

        void KetchuyenTamthu()
        {
            ketchuyen_tamthu = true;
            SetSumTotalProperties();
        }

    }
}