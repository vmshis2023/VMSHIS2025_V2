using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using Newtonsoft.Json;

using SubSonic;

using NLog;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.HIS.UI.Forms.Dungchung;
using VNS.HIS.BusRule.Classes;
using System.IO;
using VNS.HIS.UI.Forms.Cauhinh;

namespace VMS.Invoice
{
    public partial class frm_Quanlyphathanh_Hoadon : Form
    {
        private DataTable dtData;
        private Logger log;
        private MisaInvoice _MisaInvoices = new MisaInvoice();
        KCB_THANHTOAN _THANHTOAN = new KCB_THANHTOAN();
        string Args = "A|B";//A=100(tất cả),0= ngoại trú,1= nội trú;B=1 or 0(quầy thuốc);C=danh sách loại thanh toán
        string lst_IDLoaithanhtoan = "0,1,2,3,4,5,6,7,8";
        public string BuyerCode = "";
        public string BuyerLegalName = "";
        public string BuyerTaxCode = "";
        public string BuyerAddress = "";
        public string BuyerFullName = "";
        public string BuyerPhoneNumber = "";
        public string BuyerEmail = "";
        public string BuyerBankAccount = "";
        public string BuyerBankName = "";
        public string BuyerIDNumber = "";
        string SplitterPath = "";
        public frm_Quanlyphathanh_Hoadon(string Args)
        {
            InitializeComponent();
            SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
            this.FormClosing += Frm_Quanlyphathanh_Hoadon_FormClosing;
            this.Shown += Frm_Quanlyphathanh_Hoadon_Shown;
            _MisaInvoices._OnStatus += _MisaInvoices__OnStatus;
            log = LogManager.GetCurrentClassLogger();
            Utility.SetVisualStyle(this);
            m_oWorker = new BackgroundWorker();
            m_oWorker.DoWork += m_oWorker_DoWork;
            m_oWorker.ProgressChanged += m_oWorker_ProgressChanged;
            m_oWorker.RunWorkerCompleted += m_oWorker_RunWorkerCompleted;
            m_oWorker.WorkerReportsProgress = true;
            m_oWorker.WorkerSupportsCancellation = true;
            dtDenNgay.Value = dtTuNgay.Value = THU_VIEN_CHUNG.GetSysDateTime();
            txt_IdBenhnhan.KeyDown += Txt_IdBenhnhan_KeyDown;
            txtMaLanKham.KeyDown += TxtMaLanKham_KeyDown;
            grdPayment.SelectionChanged += GrdPayment_SelectionChanged;
            grdPayment.RowCheckStateChanged += GrdPayment_RowCheckStateChanged;
            grdHoadonPhathanh.SelectionChanged += GrdHoadonPhathanh_SelectionChanged;
            grdHoadonHuy.SelectionChanged += GrdHoadonHuy_SelectionChanged;
            grdPayment_Phathanh.SelectionChanged += GrdPayment_Phathanh_SelectionChanged;
            grdPayment_Huy.SelectionChanged += GrdPayment_Huy_SelectionChanged;
            grdHoadonPhathanh.ColumnButtonClick += GrdHoadonPhathanh_ColumnButtonClick;
            grdHoadonHuy.ColumnButtonClick += GrdHoadonHuy_ColumnButtonClick;
            grdMauhoadon.RowCheckStateChanged += GrdMauhoadon_RowCheckStateChanged;
            grdMauhoadon.CellValueChanged += GrdMauhoadon_CellValueChanged;

        }

        private void GrdPayment_RowCheckStateChanged(object sender, RowCheckStateChangeEventArgs e)
        {
           
        }

        private void GrdMauhoadon_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            if (!Utility.isValidGrid(grdMauhoadon)) return;
            if (e.Column.Key=="isActive")
            {
                bool activeValue =Convert.ToBoolean( grdMauhoadon.GetValue(e.Column.Key));
                int id = Utility.Int32Dbnull(grdMauhoadon.GetValue("id"));
                HoadonMauMisa objHoadonMauMisa = HoadonMauMisa.FetchByID(id);
                if (activeValue)
                {


                    new Update(HoadonMauMisa.Schema).Set(HoadonMauMisa.Columns.IsActive).EqualTo(false).Where(HoadonMauMisa.Columns.Id).IsNotEqualTo(id).Execute();
                    objHoadonMauMisa.IsActive = true;
                    objHoadonMauMisa.MarkOld();
                    objHoadonMauMisa.IsNew = false;
                    objHoadonMauMisa.Save();
                }
                else
                {
                    objHoadonMauMisa.IsActive = false;
                    objHoadonMauMisa.MarkOld();
                    objHoadonMauMisa.IsNew = false;
                    objHoadonMauMisa.Save();
                }
            }    
        }

        private void GrdMauhoadon_RowCheckStateChanged(object sender, RowCheckStateChangeEventArgs e)
        {
            
        }

        private void Frm_Quanlyphathanh_Hoadon_Shown(object sender, EventArgs e)
        {
            Try2Splitter();
        }

        void Try2Splitter()
        {
            try
            {
                List<int> lstSplitterSize = (from p in File.ReadLines(SplitterPath)
                                             select Utility.Int32Dbnull(p)).ToList<int>();
                if (lstSplitterSize != null && lstSplitterSize.Count >=4)
                {
                   splitContainer1.SplitterDistance = lstSplitterSize[0];
                    splitContainer2.SplitterDistance = lstSplitterSize[1];
                    if (lstSplitterSize[3] == 1)
                        splitContainer3.Orientation = Orientation.Horizontal;
                    else
                        splitContainer3.Orientation = Orientation.Vertical;
                    splitContainer3.SplitterDistance = lstSplitterSize[2];

                }
            }
            catch (Exception)
            {

            }
        }
        private void Frm_Quanlyphathanh_Hoadon_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
            Utility.SaveValue2Lines(SplitterPath, new List<string>() { splitContainer1.SplitterDistance.ToString(), splitContainer2.SplitterDistance.ToString(), splitContainer3.SplitterDistance.ToString() ,  (splitContainer3.Orientation == Orientation.Horizontal ? 1 : 0).ToString() });
        }

        private void GrdHoadonHuy_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "DOWNLOAD")
            {
                cmdDownload_Huy.PerformClick();
            }
        }

        private void GrdHoadonPhathanh_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
           if(e.Column.Key=="DOWNLOAD")
            {
                cmdDownload.PerformClick();
            }    
        }

        private void TxtMaLanKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string Maluotkham = Utility.sDbnull(txtMaLanKham.Text.Trim());
                if (!string.IsNullOrEmpty(Maluotkham) && txtMaLanKham.Text.Length < 8)
                {
                    Maluotkham = Utility.AutoFullPatientCode(txtMaLanKham.Text);
                    txtMaLanKham.Text = Maluotkham;
                    txtMaLanKham.Select(txtMaLanKham.Text.Length, txtMaLanKham.Text.Length);
                }
                if (!string.IsNullOrEmpty(txtMaLanKham.Text))
                {
                    _Malankham_keydown = true;
                    cmdTimKiem.PerformClick();
                    _Malankham_keydown = false;

                }
            }
        }

        bool isAllowPaymentChanged = false;
        bool isAllowPaymentChanged_Phathanh = false;
        bool isAllowPaymentChanged_Huy = false;
        bool isAllowChanged_Phathanh = false;
        bool isAllowChanged_Huy = false;
        private void GrdPayment_Huy_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdPayment_Huy) || !isAllowPaymentChanged_Huy)
            {
                grdChitiet_Huy.DataSource = null;
                return;
            }
            string str_transactionid = Utility.sDbnull(grdHoadonHuy.GetValue("transaction_id"));
            DataTable dtChitietThanhtoan = _THANHTOAN.LaychitietthanhtoanTheoTransactionId(str_transactionid, (byte)0);
            Utility.SetDataSourceForDataGridEx(grdChitiet_Huy, dtChitietThanhtoan, true, true, "1=1", "");
        }

        private void GrdPayment_Phathanh_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdPayment_Phathanh) || !isAllowPaymentChanged_Phathanh)
            {
                grdChitiet_Phathanh.DataSource = null;
                return;
            }
            string str_transactionid = Utility.sDbnull(grdHoadonPhathanh.GetValue("transaction_id"));
            DataTable dtChitietThanhtoan = _THANHTOAN.LaychitietthanhtoanTheoTransactionId(str_transactionid, (byte)0);
            Utility.SetDataSourceForDataGridEx(grdChitiet_Phathanh, dtChitietThanhtoan, true, true, "1=1", "");
        }

        private void GrdHoadonHuy_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                isAllowPaymentChanged_Huy = false;
                if (!Utility.isValidGrid(grdHoadonHuy))
                {
                    grdPayment_Huy.DataSource = null;
                    grdChitiet_Huy.DataSource = null;
                    return;
                }
             
                long id_hdon_log = Utility.Int64Dbnull(grdHoadonHuy.GetValue("id_hdon_log"));
                DataTable dtChitietThanhtoan = SPs.EInvoiceLaydanhsachThanhtoanTheoHoadon(id_hdon_log).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdPayment_Huy, dtChitietThanhtoan, true, true, "1=1", "ten_benhnhan,ma_luotkham,ngay_thanhtoan");
                isAllowPaymentChanged_Huy = true;
                grdPayment_Huy.MoveFirst();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                isAllowPaymentChanged_Huy = true;
            }

        }

        private void GrdHoadonPhathanh_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                isAllowPaymentChanged_Phathanh = false;
                if (!Utility.isValidGrid(grdHoadonPhathanh) || !isAllowChanged_Phathanh)
                {
                    grdPayment_Phathanh.DataSource = null;
                    grdChitiet_Phathanh.DataSource = null;
                    return;
                }

                long id_hdon_log = Utility.Int64Dbnull(grdHoadonPhathanh.GetValue("id_hdon_log"));
                DataTable dtChitietThanhtoan = SPs.EInvoiceLaydanhsachThanhtoanTheoHoadon(id_hdon_log).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdPayment_Phathanh, dtChitietThanhtoan, true, true, "1=1", "ten_benhnhan,ma_luotkham,ngay_thanhtoan");
                isAllowPaymentChanged_Phathanh = true;
                GrdPayment_Phathanh_SelectionChanged(grdPayment_Phathanh, e);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                isAllowPaymentChanged_Phathanh = true;
            }
        }

        private void _MisaInvoices__OnStatus(string status,bool isErr)
        {
           
            LogText(status, isErr ? Color.Red: Color.DarkBlue);
        }

        void SetUI()
        {
            try
            {
                //List<string> lstArgs = Args.Split('|');
            }
            catch (Exception ex)
            {

               
            }
        }
        private void GrdPayment_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdPayment) || !isAllowPaymentChanged)
                {
                    grdChitietThanhtoan.DataSource = null;
                    cmdHoadonThaythe.Visible = false;
                    return;
                }
                long v_id_thanhtoan = Utility.Int64Dbnull(grdPayment.GetValue("id_thanhtoan"));
                DataTable dtChitietThanhtoan = _THANHTOAN.Laychitietthanhtoan(v_id_thanhtoan, (byte)0);
                Utility.SetDataSourceForDataGridEx(grdChitietThanhtoan, dtChitietThanhtoan, true, true, chkAnChitietdaPhathanh.Checked ? "tthai_xuat_hddt=0" : "1=1", "");
                grdChitietThanhtoan.CheckAllRecords();
                List<string> lstVAT = (from p in grdChitietThanhtoan.GetCheckedRows() select Utility.sDbnull(p.Cells["VAT"].Value)).Distinct().ToList<string>();
                lblVAT.Visible = cboVAT.Visible = lstVAT.Count > 1;
                lstVAT.Insert(0, "");
                cboVAT.DataSource = lstVAT;
                cmdHoadonThaythe.Visible = Utility.Int32Dbnull(grdPayment.GetValue("co_tralai")) == 1;
            }
            catch (Exception)
            {


            }
        }

        bool _ID_keydown = false;
        private void Txt_IdBenhnhan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (!string.IsNullOrEmpty(txt_IdBenhnhan.Text))
                {
                    _ID_keydown = true;
                    cmdTimKiem.PerformClick();
                    _ID_keydown = false;
                   
                }
            }
        }

        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.WaitNow(this);
                isAllowPaymentChanged = false;
                isAllowChanged_Phathanh = false;
                isAllowChanged_Huy = false;
                int idthungan = Utility.Int32Dbnull(cboNhanvien.SelectedValue);
                
                int status = -1;
                if (optDaphathanh.Checked) status = 1;
                if (optChuaphathanh.Checked) status = 0;
                if (optDahuy.Checked) status = 2;
                string kieuthanhtoan = "";
               
                DateTime fromdate = dtTuNgay.Value;
                DateTime enddate = dtDenNgay.Value;
                int id_benhnhan = Utility.Int32Dbnull(txt_IdBenhnhan.Text, -1);
                string ma_lankham = Utility.sDbnull(txtMaLanKham.Text);
                string ten_benhnhan = Utility.sDbnull(txtTenBenhNhan.Text);
                string ma_tnv = Utility.sDbnull(cboNhanvien.SelectedValue,"-1");
                if ( !chkTuNgay.Checked)
                {

                    fromdate = Convert.ToDateTime("01/01/1900");
                    enddate = THU_VIEN_CHUNG.GetSysDateTime();
                }
                
                byte v_bytNoitru = 100;
                if (optTatca.Checked)
                    v_bytNoitru = 100; 
                else if (optNgoaitru.Checked)
                    v_bytNoitru = 0;
                else
                    v_bytNoitru = 1;
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
                
                if (optChuaphathanh.Checked)
                {
                    dtData = SPs.EInvoiceLaydanhsachCaclanthanhtoan(fromdate, enddate, ma_lankham,
                        id_benhnhan,ten_benhnhan,ma_tnv, -1, v_bytNoitru, Utility.Bool2byte(optQuaythuoc.Checked),
                        globalVariables.MA_KHOA_THIEN, lst_IDLoaithanhtoan, 0).GetDataSet().Tables[0];
                    // Utility.SetDataSourceForDataGridEx(grdPayment, dtData, true, true, "tthai_xuat_hddt= false or tthai_xuat_hddt is null", "ten_benhnhan,ma_luotkham,ngay_thanhtoan");
                    Utility.SetDataSourceForDataGridEx(grdPayment, dtData, true, true, chkBocacthanhtoandaxuathoadon.Checked ? "tthai_xuat_hddt= false or tthai_xuat_hddt is null" : "1=1", "ten_benhnhan,ngay_thanhtoan");
                    if (!Utility.isValidGrid(grdPayment))
                    {
                        grdChitietThanhtoan.DataSource = null;
                    }
                    isAllowPaymentChanged = true;
                    GrdPayment_SelectionChanged(grdPayment, e);
                }
                if (optDaphathanh.Checked)
                {
                    DataTable dtPhathanh = SPs.EInvoiceLaydanhsachCaclanthanhtoan(fromdate, enddate, ma_lankham,
                        id_benhnhan, ten_benhnhan, ma_tnv, -1, v_bytNoitru, Utility.Bool2byte(optQuaythuoc.Checked),
                        globalVariables.MA_KHOA_THIEN, lst_IDLoaithanhtoan, 1).GetDataSet().Tables[0];
                    Utility.SetDataSourceForDataGridEx(grdHoadonPhathanh, dtPhathanh, true, true, "tthai_huy=false", "ngay_tao");
                    if (!Utility.isValidGrid(grdHoadonPhathanh))
                    {
                        grdPayment_Phathanh.DataSource = null;
                        grdChitiet_Phathanh.DataSource = null;
                    }
                    isAllowChanged_Phathanh = true;
                    GrdHoadonPhathanh_SelectionChanged(grdPayment, e);
                }
                if (optDahuy.Checked)
                {
                  DataTable  dtHuy = SPs.EInvoiceLaydanhsachCaclanthanhtoan(fromdate, enddate, ma_lankham,
                         id_benhnhan, ten_benhnhan, ma_tnv, -1, v_bytNoitru, Utility.Bool2byte(optQuaythuoc.Checked),
                         globalVariables.MA_KHOA_THIEN, lst_IDLoaithanhtoan, 2).GetDataSet().Tables[0];
                    Utility.SetDataSourceForDataGridEx(grdHoadonHuy, dtHuy, true, true, "tthai_huy=true", "ngay_tao");
                    if (!Utility.isValidGrid(grdHoadonHuy))
                    {
                        grdPayment_Huy.DataSource = null;
                        grdChitiet_Huy.DataSource = null;
                    }
                    isAllowChanged_Huy = true;
                    GrdHoadonHuy_SelectionChanged(grdPayment, e);
                }
            }
            catch (Exception ex)
            { 
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                isAllowPaymentChanged = true;
                isAllowChanged_Phathanh = true;
                isAllowChanged_Huy = true;
                Utility.DefaultNow(this);
            }

        }

        /// <summary>
        /// On completed do the appropriate task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_oWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cmdPhathanhHDon.Enabled = true;
        }

        private void m_oWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            UpdateHoaDon(e);
        }

        private void m_oWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Quanlyphathanh_Hoadon_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                ProcessTabKey(true);
                return;
            }    
            else if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            else if (e.KeyCode == Keys.S && e.Control) cmdPhathanhHDon.PerformClick();
            else if (e.KeyCode == Keys.M && e.Control) cmdManualInvoice.PerformClick();
            else if (e.KeyCode == Keys.P && e.Control) cmdPReview.PerformClick();
            else if ((e.Control && e.KeyCode==Keys.T) || e.KeyCode == Keys.F3) cmdTimKiem.PerformClick();
        }
        void LoadUserConfigs()
        {
            try
            {
                optTheoThanhtoan.Checked = Utility.getUserConfigValue(optTheoThanhtoan.Tag.ToString(), Utility.Bool2byte(optTheoThanhtoan.Checked)) == 1;
                optTheoluotkham.Checked = Utility.getUserConfigValue(optTheoluotkham.Tag.ToString(), Utility.Bool2byte(optTheoluotkham.Checked)) == 1;
                chkBocacthanhtoandaxuathoadon.Checked= Utility.getUserConfigValue(chkBocacthanhtoandaxuathoadon.Tag.ToString(), Utility.Bool2byte(chkBocacthanhtoandaxuathoadon.Checked)) == 1;
                chkBodichvutronggoi.Checked = Utility.getUserConfigValue(chkBodichvutronggoi.Tag.ToString(), Utility.Bool2byte(chkBodichvutronggoi.Checked)) == 1;
                chkAnChitietdaPhathanh.Checked = Utility.getUserConfigValue(chkAnChitietdaPhathanh.Tag.ToString(), Utility.Bool2byte(chkAnChitietdaPhathanh.Checked)) == 1;

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
                Utility.SaveUserConfig(optTheoThanhtoan.Tag.ToString(), Utility.Bool2byte(optTheoThanhtoan.Checked));
                Utility.SaveUserConfig(optTheoluotkham.Tag.ToString(), Utility.Bool2byte(optTheoluotkham.Checked));
                Utility.SaveUserConfig(chkBocacthanhtoandaxuathoadon.Tag.ToString(), Utility.Bool2byte(chkBocacthanhtoandaxuathoadon.Checked));
                Utility.SaveUserConfig(chkBodichvutronggoi.Tag.ToString(), Utility.Bool2byte(chkBodichvutronggoi.Checked));
                Utility.SaveUserConfig(chkAnChitietdaPhathanh.Tag.ToString(), Utility.Bool2byte(chkAnChitietdaPhathanh.Checked));

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        DataTable dtMau;
        private void frm_Quanlyphathanh_Hoadon_Load(object sender, EventArgs e)
        {
            DataBinding.BindDataCombobox(cboNhanvien, THU_VIEN_CHUNG.LaydanhsachThunganvien(),
                                      DmucNhanvien.Columns.UserName, DmucNhanvien.Columns.TenNhanvien, "Chọn nhân viên thu ngân", true);
            DataBinding.BindDataCombobox(cboMaCoso, THU_VIEN_CHUNG.LayDulieuDanhmucChung("COSOKCB", true),
                                     DmucChung.Columns.Ma, DmucChung.Columns.Ten, "Chọn mã cơ sở KCB", true);
            dtMau = new Select().From(HoadonMauMisa.Schema).ExecuteDataSet().Tables[0];
            grdMauhoadon.DataSource = dtMau;
            PhanQuyenChucNang();
            LoadUserConfigs();
        }

        private void TimKiem()
        {
           
        }
        private DataTable dtCapPhat;
        private int HOADON_CAPPHAT_ID = -1;
        bool _Malankham_keydown = false;
        private void txtPatient_Code_KeyDown(object sender, KeyEventArgs e)
        {
           
                
        }

        private int HoaDon_Mau_ID = -1;

        private BackgroundWorker m_oWorker;

        /// <summary>
        /// hàm thưc hiện lưu thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.WaitNow(this);
                if (dtMau == null || dtMau.Rows.Count <= 0 || dtMau.Select("isActive=true").Length <= 0)
                {
                    Utility.ShowMsg("Chưa có mẫu hóa đơn để phát hành hóa đơn.\nVui lòng chuyển sang tab Danh sách mẫu hóa đơn để kích hoạt mẫu hóa đơn hiện có hoặc lấy mẫu hóa đơn mới từ nhà cung cấp HĐĐT");
                    uiTabHDDT.SelectedTab = uiTabPageMauHdon;
                    cmdlaymauhoadon.Focus();
                    return;
                }
                DataRow dr = dtMau.AsEnumerable().Where(c => Utility.Obj2Bool(c["isActive"])).FirstOrDefault();
                if (dr == null)
                {
                    Utility.ShowMsg("Chưa có mẫu hóa đơn để phát hành hóa đơn.\nVui lòng chuyển sang tab Danh sách mẫu hóa đơn để kích hoạt mẫu hóa đơn hiện có hoặc lấy mẫu hóa đơn mới từ nhà cung cấp HĐĐT");
                    uiTabHDDT.SelectedTab = uiTabPageMauHdon;
                    cmdlaymauhoadon.Focus();
                    return;
                }
                _MisaInvoices.SetMauhoadon(Utility.sDbnull(dr["InvSeries"]), Utility.sDbnull(dr["IPTemplateID"]), Utility.sDbnull(dr["TemplateName"]));
                string sDataRequest = "";
                string result = string.Empty;
                int _tongso = grdPayment.GetCheckedRows().Count();
                Utility.ResetProgressBarJanus(ProgressBar, _tongso, true);
                bool kt = false;
                string str_IdThanhtoan = "";
                string eMessage = "";
                if (grdPayment.GetCheckedRows().Count() <= 0 && Utility.isValidGrid(grdPayment))
                {
                    grdPayment.CurrentRow.BeginEdit();
                    grdPayment.CurrentRow.IsChecked = true;
                    grdPayment.CurrentRow.EndEdit();
                }
                bool isNoitru = (from p in grdPayment.GetCheckedRows() where Utility.sDbnull(p.Cells["noi_tru"].Value) == "1" select p).Count() > 0;
                bool isQuaythuoc = (from p in grdPayment.GetCheckedRows() where Utility.sDbnull(p.Cells["ttoan_thuoc"].Value) == "1" select p).Count() > 0;
                List<string> lst_maluotkham = (from p in grdPayment.GetCheckedRows() select Utility.sDbnull(p.Cells["ma_luotkham"].Value)).Distinct().ToList<string>();
                List<long> lst_Idbenhnhan = (from p in grdPayment.GetCheckedRows() select Utility.Int64Dbnull(p.Cells["id_benhnhan"].Value)).Distinct().ToList<long>();
                List<string> lst_ngay_ttoan_check = (from p in grdPayment.GetCheckedRows() select Utility.sDbnull(p.Cells["ngay_ttoan_check"].Value)).Distinct().ToList<string>();
                string str_IdThanhtoanChitiet = string.Join(",", (from p in grdChitietThanhtoan.GetCheckedRows() select Utility.sDbnull(p.Cells["id_chitiet"].Value)).Distinct().ToArray<string>());
                List<long> lstCanhbao = (from p in grdChitietThanhtoan.GetCheckedRows() where Utility.sDbnull(p.Cells["transaction_id"].Value, "") != "" select Utility.Int64Dbnull(p.Cells["id_chitiet"].Value)).Distinct().ToList<long>();
                List<long> lstCanhbao_VAT = (from p in grdChitietThanhtoan.GetCheckedRows() select Utility.Int64Dbnull(p.Cells["VAT"].Value)).Distinct().ToList<long>();
                if (isQuaythuoc)
                {
                    List<Int16> lstCanhbao_VAT_Thuoc_0_phantram = (from p in grdChitietThanhtoan.GetCheckedRows() where Utility.Int16Dbnull(p.Cells["VAT"].Value) <= 0 select Utility.Int16Dbnull(p.Cells["VAT"].Value)).Distinct().ToList<Int16>();
                    if (lstCanhbao_VAT_Thuoc_0_phantram.Count > 0)
                    {

                        Utility.ShowMsg("Chỉ cho phép phát hành HĐĐT với các thuốc có thuế suất GTGT(VAT)>0.\nVui lòng kiểm tra lại các mặt hàng chi tiết đang chọn và loại bỏ các mặt hàng có thuế suất =0");
                        cboVAT.Focus();
                        return;
                    }
                }
                if (!isNoitru)
                {
                    if (lstCanhbao_VAT.Count > 1)
                    {

                        Utility.ShowMsg("Các chi tiết bạn chọn để phát hành HĐĐT có mức VAT khác nhau nên hệ thống không cho phép.\nBạn có thể dùng tính năng Lọc VAT để tìm các mặt hàng có mức VAT giống nhau trước khi thực hiện phát hành HĐĐT");
                        cboVAT.Focus();
                        return;
                    }
                }
                _MisaInvoices.VAT = lstCanhbao_VAT.Count == 1 ? Utility.Int32Dbnull(lstCanhbao_VAT[0]) : 0;
                if (lstCanhbao.Count > 0)
                {
                    Utility.ShowMsg("Một số chi tiết bạn chọn phát hành HĐĐT đã được sử dụng phát hành. Vui lòng kiểm tra lại");
                    return;
                }
                if (lst_Idbenhnhan.Count <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn phiếu thu cần phát hành HĐĐT điện tử");
                    return;
                }
                if (lst_Idbenhnhan.Count > 1)
                {
                    if (!Utility.AcceptQuestion("Bạn đang chọn phát hành HĐĐT cho nhiều người bệnh. Điều này có thể sẽ tốn nhiều thời gian xủ lý.\nBạn có chắc chắn muốn tiếp tục hay không?", "Xác nhận", true))
                    {
                        return;
                    }
                   
                }
                _MisaInvoices.transaction_id = "";
                if (optTheoThanhtoan.Checked)
                {
                    foreach (GridEXRow gridExRow in grdPayment.GetCheckedRows())
                    {
                        str_IdThanhtoan = Utility.sDbnull(gridExRow.Cells["id_thanhtoan"].Value, 0);
                        str_IdThanhtoanChitiet = string.Join(",", (from p in grdChitietThanhtoan.GetCheckedRows() where Utility.sDbnull(p.Cells["id_thanhtoan"].Value, 0)== str_IdThanhtoan select Utility.sDbnull(p.Cells["id_chitiet"].Value)).Distinct().ToArray<string>());
                        kt = _MisaInvoices.phathanh_hoadon(str_IdThanhtoan, 0, str_IdThanhtoanChitiet, ref eMessage);
                        if (kt)
                        {
                            (from p in dtData.AsEnumerable()
                             where str_IdThanhtoan == Utility.sDbnull(p["id_thanhtoan"], "-1")
                             select p).ToList()
                             .ForEach(x =>
                             {
                                 x["tthai_xuat_hddt"] = true;
                                 x["transaction_id"] = _MisaInvoices.transaction_id;
                             }
                             );
                            LogText(eMessage, Color.DarkBlue);
                        }
                        else
                        {
                            LogText(eMessage, Color.Red);
                        }
                        SetValue4Prg(ProgressBar, 1);
                        gridExRow.IsChecked = false;
                    }
                }
                else if (optTheoluotkham.Checked)
                {

                    if (lst_ngay_ttoan_check.Count >= 2)
                    {
                        if (!Utility.AcceptQuestion("Chú ý: Các phiếu thu khác ngày. Bạn có chắc chắn muốn phát hành HĐĐT điện tử cho các phiếu này?", "Cảnh báo các phiếu thu khác ngày", true))
                        {
                            return;
                        }
                    }
                    foreach (string maluotkham in lst_maluotkham)
                    {
                        List<long> lstIdThanhtoan = (from p in grdPayment.GetCheckedRows() where Utility.sDbnull(p.Cells["ma_luotkham"].Value) == maluotkham select Utility.Int64Dbnull(p.Cells["id_thanhtoan"].Value)).Distinct().ToList<long>();

                        str_IdThanhtoan = string.Join(",", lstIdThanhtoan.Select(l => l.ToString()).ToArray());
                        
                         str_IdThanhtoanChitiet = string.Join(",", (from p in grdChitietThanhtoan.GetCheckedRows() where lstIdThanhtoan.Contains(Utility.Int64Dbnull(p.Cells["id_thanhtoan"].Value)) select Utility.sDbnull(p.Cells["id_chitiet"].Value)).Distinct().ToArray<string>());
                        kt = _MisaInvoices.phathanh_hoadon(str_IdThanhtoan, 1, str_IdThanhtoanChitiet, ref eMessage);
                        if (kt)
                        {
                            (from p in dtData.AsEnumerable()
                             where str_IdThanhtoan == Utility.sDbnull(p["id_thanhtoan"], "-1")
                             select p).ToList()
                             .ForEach(x =>
                             {
                                 x["tthai_xuat_hddt"] = true;
                                 x["transaction_id"] = _MisaInvoices.transaction_id;
                             }
                             );
                            LogText(eMessage, Color.DarkBlue);
                        }
                        else
                        {
                            LogText(eMessage, Color.Red);
                        }
                        SetValue4Prg(ProgressBar, 1);
                        str_IdThanhtoan = "";
                    }

                }
                else
                {
                    foreach (long idbenhnhan in lst_Idbenhnhan)
                    {
                        List<long> lstIdThanhtoan = (from p in grdPayment.GetCheckedRows() where Utility.Int64Dbnull(p.Cells["id_benhnhan"].Value) == idbenhnhan select Utility.Int64Dbnull(p.Cells["id_thanhtoan"].Value)).Distinct().ToList<long>();

                        str_IdThanhtoan = string.Join(",", lstIdThanhtoan.Select(l => l.ToString()).ToArray());

                        str_IdThanhtoanChitiet = string.Join(",", (from p in grdChitietThanhtoan.GetCheckedRows() where lstIdThanhtoan.Contains(Utility.Int64Dbnull(p.Cells["id_thanhtoan"].Value)) select Utility.sDbnull(p.Cells["id_chitiet"].Value)).Distinct().ToArray<string>());
                        kt = _MisaInvoices.phathanh_hoadon(str_IdThanhtoan, 1, str_IdThanhtoanChitiet, ref eMessage);
                        if (kt)
                        {
                            (from p in dtData.AsEnumerable()
                             where str_IdThanhtoan == Utility.sDbnull(p["id_thanhtoan"], "-1")
                             select p).ToList()
                             .ForEach(x =>
                             {
                                 x["tthai_xuat_hddt"] = true;
                                 x["transaction_id"] = _MisaInvoices.transaction_id;
                             }
                             );
                            LogText(eMessage, Color.DarkBlue);
                        }
                        else
                        {
                            LogText(eMessage, Color.Red);
                        }
                        SetValue4Prg(ProgressBar, 1);
                        str_IdThanhtoan = "";
                    }
                }
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Lỗi khi phát hành HĐĐT: {0} ",
                                     ex.Message), newaction.Insert, "UI");
            }
            finally
            {
                VNS.Libs.AppUI.UIAction._Visible(ProgressBar, false);
                Utility.DefaultNow(this);
                cmdPhathanhHDon.Enabled = true;
            }

        }
        void backup_phathanhhoadon()
        {
            try
            {
                Utility.WaitNow(this);
                if (dtMau == null || dtMau.Rows.Count <= 0 || dtMau.Select("isActive=true").Length <= 0)
                {
                    Utility.ShowMsg("Chưa có mẫu hóa đơn để phát hành hóa đơn.\nVui lòng chuyển sang tab Danh sách mẫu hóa đơn để kích hoạt mẫu hóa đơn hiện có hoặc lấy mẫu hóa đơn mới từ nhà cung cấp HĐĐT");
                    uiTabHDDT.SelectedTab = uiTabPageMauHdon;
                    cmdlaymauhoadon.Focus();
                    return;
                }
                DataRow dr = dtMau.AsEnumerable().Where(c => Utility.Obj2Bool(c["isActive"])).FirstOrDefault();
                if (dr == null)
                {
                    Utility.ShowMsg("Chưa có mẫu hóa đơn để phát hành hóa đơn.\nVui lòng chuyển sang tab Danh sách mẫu hóa đơn để kích hoạt mẫu hóa đơn hiện có hoặc lấy mẫu hóa đơn mới từ nhà cung cấp HĐĐT");
                    uiTabHDDT.SelectedTab = uiTabPageMauHdon;
                    cmdlaymauhoadon.Focus();
                    return;
                }
                _MisaInvoices.SetMauhoadon(Utility.sDbnull(dr["InvSeries"]), Utility.sDbnull(dr["IPTemplateID"]), Utility.sDbnull(dr["TemplateName"]));
                string sDataRequest = "";
                string result = string.Empty;
                int _tongso = grdPayment.GetCheckedRows().Count();
                Utility.ResetProgressBarJanus(ProgressBar, _tongso, true);
                bool kt = false;
                string str_IdThanhtoan = "";
                string eMessage = "";
                if (grdPayment.GetCheckedRows().Count() <= 0 && Utility.isValidGrid(grdPayment))
                {
                    grdPayment.CurrentRow.BeginEdit();
                    grdPayment.CurrentRow.IsChecked = true;
                    grdPayment.CurrentRow.EndEdit();
                }
                bool isNoitru = (from p in grdPayment.GetCheckedRows() where Utility.sDbnull(p.Cells["noi_tru"].Value) == "1" select p).Count() > 0;
                bool isQuaythuoc = (from p in grdPayment.GetCheckedRows() where Utility.sDbnull(p.Cells["ttoan_thuoc"].Value) == "1" select p).Count() > 0;
                List<string> lst_maluotkham = (from p in grdPayment.GetCheckedRows() select Utility.sDbnull(p.Cells["ma_luotkham"].Value)).Distinct().ToList<string>();
                List<string> lst_Idbenhnhan = (from p in grdPayment.GetCheckedRows() select Utility.sDbnull(p.Cells["id_benhnhan"].Value)).Distinct().ToList<string>();
                List<string> lst_ngay_ttoan_check = (from p in grdPayment.GetCheckedRows() select Utility.sDbnull(p.Cells["ngay_ttoan_check"].Value)).Distinct().ToList<string>();
                string str_IdThanhtoanChitiet = string.Join(",", (from p in grdChitietThanhtoan.GetCheckedRows() select Utility.sDbnull(p.Cells["id_chitiet"].Value)).Distinct().ToArray<string>());
                List<long> lstCanhbao = (from p in grdChitietThanhtoan.GetCheckedRows() where Utility.sDbnull(p.Cells["transaction_id"].Value, "") != "" select Utility.Int64Dbnull(p.Cells["id_chitiet"].Value)).Distinct().ToList<long>();
                List<long> lstCanhbao_VAT = (from p in grdChitietThanhtoan.GetCheckedRows() select Utility.Int64Dbnull(p.Cells["VAT"].Value)).Distinct().ToList<long>();
                if (isQuaythuoc)
                {
                    List<Int16> lstCanhbao_VAT_Thuoc_0_phantram = (from p in grdChitietThanhtoan.GetCheckedRows() where Utility.Int16Dbnull(p.Cells["VAT"].Value) <= 0 select Utility.Int16Dbnull(p.Cells["VAT"].Value)).Distinct().ToList<Int16>();
                    if (lstCanhbao_VAT_Thuoc_0_phantram.Count > 0)
                    {

                        Utility.ShowMsg("Chỉ cho phép phát hành HĐĐT với các thuốc có thuế suất GTGT(VAT)>0.\nVui lòng kiểm tra lại các mặt hàng chi tiết đang chọn và loại bỏ các mặt hàng có thuế suất =0");
                        cboVAT.Focus();
                        return;
                    }
                }
                if (!isNoitru)
                {
                    if (lstCanhbao_VAT.Count > 1)
                    {

                        Utility.ShowMsg("Các chi tiết bạn chọn để phát hành HĐĐT có mức VAT khác nhau nên hệ thống không cho phép.\nBạn có thể dùng tính năng Lọc VAT để tìm các mặt hàng có mức VAT giống nhau trước khi thực hiện phát hành HĐĐT");
                        cboVAT.Focus();
                        return;
                    }
                }
                _MisaInvoices.VAT = lstCanhbao_VAT.Count == 1 ? Utility.Int32Dbnull(lstCanhbao_VAT[0]) : 0;
                if (lstCanhbao.Count > 0)
                {
                    Utility.ShowMsg("Một số chi tiết bạn chọn phát hành HĐĐT đã được sử dụng phát hành. Vui lòng kiểm tra lại");
                    return;
                }
                if (lst_Idbenhnhan.Count <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn phiếu thu cần phát hành HĐĐT điện tử");
                    return;
                }
                _MisaInvoices.transaction_id = "";
                if (optTheoThanhtoan.Checked)
                {
                    foreach (GridEXRow gridExRow in grdPayment.GetCheckedRows())
                    {
                        str_IdThanhtoan = Utility.sDbnull(gridExRow.Cells["id_thanhtoan"].Value, 0);
                        kt = _MisaInvoices.phathanh_hoadon(str_IdThanhtoan, 0, str_IdThanhtoanChitiet, ref eMessage);
                        if (kt)
                        {
                            (from p in dtData.AsEnumerable()
                             where str_IdThanhtoan == Utility.sDbnull(p["id_thanhtoan"], "-1")
                             select p).ToList()
                             .ForEach(x =>
                             {
                                 x["tthai_xuat_hddt"] = true;
                                 x["transaction_id"] = _MisaInvoices.transaction_id;
                             }
                             );
                            LogText(eMessage, Color.DarkBlue);
                        }
                        else
                        {
                            LogText(eMessage, Color.Red);
                        }
                        SetValue4Prg(ProgressBar, 1);
                        gridExRow.IsChecked = false;
                    }
                }
                else if (optTheoluotkham.Checked)
                {

                    if (lst_ngay_ttoan_check.Count >= 2)
                    {
                        if (!Utility.AcceptQuestion("Chú ý: Các phiếu thu khác ngày. Bạn có chắc chắn muốn phát hành HĐĐT điện tử cho các phiếu này?", "Cảnh báo các phiếu thu khác ngày", true))
                        {
                            return;
                        }
                    }
                    foreach (string maluotkham in lst_maluotkham)
                    {
                        str_IdThanhtoan = string.Join(",", (from _row in grdPayment.GetCheckedRows()
                                                            where Utility.sDbnull(_row.Cells["ma_luotkham"].Value, "") == maluotkham
                                                            select Utility.sDbnull(_row.Cells["id_thanhtoan"].Value)).ToArray<string>());
                        kt = _MisaInvoices.phathanh_hoadon(str_IdThanhtoan, 1, str_IdThanhtoanChitiet, ref eMessage);
                        if (kt)
                        {
                            (from p in dtData.AsEnumerable()
                             where str_IdThanhtoan == Utility.sDbnull(p["id_thanhtoan"], "-1")
                             select p).ToList()
                             .ForEach(x =>
                             {
                                 x["tthai_xuat_hddt"] = true;
                                 x["transaction_id"] = _MisaInvoices.transaction_id;
                             }
                             );
                            LogText(eMessage, Color.DarkBlue);
                        }
                        else
                        {
                            LogText(eMessage, Color.Red);
                        }
                        SetValue4Prg(ProgressBar, 1);
                        str_IdThanhtoan = "";
                    }

                }
                else
                {
                    foreach (string idbenhnhan in lst_Idbenhnhan)
                    {
                        str_IdThanhtoan = string.Join(",", (from _row in grdPayment.GetCheckedRows()
                                                            where Utility.sDbnull(_row.Cells["id_benhnhan"].Value, "") == idbenhnhan
                                                            select Utility.sDbnull(_row.Cells["id_thanhtoan"].Value)).ToArray<string>());
                        kt = _MisaInvoices.phathanh_hoadon(str_IdThanhtoan, 1, str_IdThanhtoanChitiet, ref eMessage);
                        if (kt)
                        {
                            (from p in dtData.AsEnumerable()
                             where str_IdThanhtoan == Utility.sDbnull(p["id_thanhtoan"], "-1")
                             select p).ToList()
                             .ForEach(x =>
                             {
                                 x["tthai_xuat_hddt"] = true;
                                 x["transaction_id"] = _MisaInvoices.transaction_id;
                             }
                             );
                            LogText(eMessage, Color.DarkBlue);
                        }
                        else
                        {
                            LogText(eMessage, Color.Red);
                        }
                        SetValue4Prg(ProgressBar, 1);
                        str_IdThanhtoan = "";
                    }
                }
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Lỗi khi phát hành HĐĐT: {0} ",
                                     ex.Message), newaction.Insert, "UI");
            }
            finally
            {
                VNS.Libs.AppUI.UIAction._Visible(ProgressBar, false);
                Utility.DefaultNow(this);
                cmdPhathanhHDon.Enabled = true;
            }
        }
        private void UpdateHoaDon(DoWorkEventArgs e)
        {
          
        }
        private delegate void SetPrgValue(Janus.Windows.EditControls.UIProgressBar Prg, int _Value);
        private void SetValue4Prg(Janus.Windows.EditControls.UIProgressBar Prg, int _Value)
        {
            try
            {
                if (Prg.InvokeRequired)
                {
                    Prg.Invoke(new SetPrgValue(SetValue4Prg), new object[] { Prg, _Value });
                }
                else
                {
                    if (Prg.Value + _Value <= Prg.Maximum) Prg.Value += _Value;
                    Prg.Refresh();
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                    Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        private const string _sNewline = "\r\n";
        private void AddAction(string sLogText, Color color)
        {
            if (sLogText.Length > 0)
            {
                Color oldColor = rtxtLogs.SelectionColor;
                rtxtLogs.SelectionLength = 0;
                rtxtLogs.SelectionStart = rtxtLogs.Text.Length;
                rtxtLogs.SelectionColor = color;
                rtxtLogs.SelectionFont = new Font(rtxtLogs.SelectionFont, FontStyle.Bold);
                rtxtLogs.AppendText(sLogText);
                rtxtLogs.SelectionColor = oldColor;
            }
        }
        public delegate void AddLog(string logText, Color sActionColor);
        public void LogText(string sLogText, Color sActionColor)
        {
            if (InvokeRequired)
            {
                Invoke(new AddLog(LogText), new object[] { sLogText, sActionColor });
            }
            else
            {
                VNS.Libs.AppUI.UIAction.SetText(lblStatus, sLogText);
                AddAction(sLogText, sActionColor);
                rtxtLogs.AppendText(_sNewline);
            }
        }

        private void cmdCancelinvoices_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.WaitNow(this);
                if (!Utility.Coquyen("HOADONDIENTU_HUY"))
                {
                    Utility.thongbaokhongcoquyen("HOADONDIENTU_HUY", " hủy hóa đơn điện tử");
                    return;
                }
                var nhaplydohuythanhtoan = new frm_Chondanhmucdungchung("LYDOHUYHOADONDIENTU",
                                    "Hủy hóa đơn điện tử", "Nhập lý do hủy HĐĐT trước khi thực hiện",
                                    "Lý do hủy HĐĐT", false);
                nhaplydohuythanhtoan.ShowDialog();
                if (nhaplydohuythanhtoan.m_blnCancel) return;
               string ma_lydohuy = nhaplydohuythanhtoan.ma;
                string lydo_huy = nhaplydohuythanhtoan.ten;
                string returnMessage = "";
                decimal tongBntt = 0; //grdPayment.GetDataRows().Sum();   
                int _tong_sohuy = grdHoadonPhathanh.GetCheckedRows().Count();
                Utility.ResetProgressBarJanus(ProgressBar, _tong_sohuy, true);
                foreach (GridEXRow gridExRow in grdHoadonPhathanh.GetCheckedRows())
                {
                    string _str_IdThanhtoan = "";
                    int IdHdonLog = Utility.Int32Dbnull(gridExRow.Cells[HoadonLog.Columns.IdHdonLog].Value);
                    string serie = Utility.sDbnull(gridExRow.Cells[HoadonLog.Columns.Serie].Value);
                    //Int16 trangthai = Utility.Int16Dbnull(gridExRow.Cells[HoadonLog.Columns.TrangThai].Value);
                    string MauHoadon = Utility.sDbnull(gridExRow.Cells[HoadonLog.Columns.MauHoadon].Value);
                    string kihieu = Utility.sDbnull(gridExRow.Cells[HoadonLog.Columns.KiHieu].Value);
                    bool thanhcong = false;

                    log.Trace("Bat dau Huy hoa don dien tu cho hóa đơn: " + serie);
                    string transaction_id = Utility.sDbnull(gridExRow.Cells[HoadonLog.Columns.TransactionId].Value, "");
                    thanhcong = _MisaInvoices.huy_hoadon(transaction_id, serie, globalVariables.SysDate, lydo_huy, ref returnMessage);
                    if (!thanhcong)
                    {
                        log.Trace("Lỗi khi hủy hóa đơn điện tử: " + returnMessage);
                        LogText(returnMessage, Color.Red);
                        Utility.ShowMsg(string.Format("Hủy hóa đơn không thành công, lỗi: {0}, SESIE :{1}, IdHdonLog : {2} ", returnMessage, serie, IdHdonLog));
                        Utility.Log(this.Name, globalVariables.UserName,
                            string.Format(
                                "Hủy hóa đơn không thành công, lỗi: {0}, SESIE :{1}, IdHdonLog : {2} ",
                                returnMessage, serie, IdHdonLog), newaction.Update, "UI");
                    }
                    else
                    {
                        log.Trace("Huy hoa don: Susscess " + returnMessage);
                        // update bảng thanh toán và bảng hóa đơn đỏ                        
                        SPs.EInvoiceCapnhattrangthaihuy(transaction_id, globalVariables.UserName, THU_VIEN_CHUNG.GetSysDateTime(), THU_VIEN_CHUNG.GetMACAddress()).Execute();
                        gridExRow.BeginEdit();
                        gridExRow.Cells["tthai_huy"].Value = 1;
                        gridExRow.Cells["CHON"].Value = 0;
                        gridExRow.EndEdit();
                        Utility.ShowMsg(string.Format("Hủy hóa đơn thành công: {0}, SESIE :{1}, IdHdonLog : {2} ", returnMessage, serie, IdHdonLog));
                        Utility.Log(this.Name, globalVariables.UserName,
                            string.Format("Hủy hóa đơn đỏ, InvInvoiceAuthId hóa đơn: {0}, SESIE :{1}",transaction_id, serie),newaction.Update, "UI");
                    }


                    SetValue4Prg(ProgressBar, 1);
                    Utility.Log(this.Name, globalVariables.UserName,
                        string.Format("Hủy hóa đơn với mã tra cứu: {0}, serie: {1}, mẫu hóa đơn: {2}, kí hiệu: {3}", transaction_id, serie
                            , MauHoadon, kihieu), newaction.Update, "UI");
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                log.Trace(ex.Message);
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Lỗi khi hủy hóa đơn: {0}", ex.Message), newaction.Update, "UI");

            }
            finally
            {
                VNS.Libs.AppUI.UIAction._Visible(ProgressBar, false);
                Utility.DefaultNow(this);
            }
        }
        private void cmddownload_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.WaitNow(this);
                if (!Utility.Coquyen("HOADONDIENTU_DOWNLOAD"))
                {
                    Utility.thongbaokhongcoquyen("HOADONDIENTU_DOWNLOAD", " tải về hóa đơn điện tử");
                    return;
                }
                if (grdHoadonPhathanh.GetCheckedRows().Count() <= 0)
                {
                    grdHoadonPhathanh.CurrentRow.BeginEdit();
                    grdHoadonPhathanh.CurrentRow.IsChecked = true;
                    grdHoadonPhathanh.CurrentRow.EndEdit();
                }
                //bool thongbaomofile = THU_VIEN_CHUNG.Laygiatrithamsohethong("MISA_THONGBAOMOFILE", true) == "1";

                string eMessge = "";
                Utility.ResetProgressBarJanus(ProgressBar, grdHoadonPhathanh.GetCheckedRows().Count(), true);
                log.Trace("Tải File invInvoiceAuthId: " + grdHoadonPhathanh.GetCheckedRows().Count().ToString());
                foreach (GridEXRow gridExRow in grdHoadonPhathanh.GetCheckedRows())
                {
                    string TransactionID = Utility.sDbnull(gridExRow.Cells[HoadonLog.Columns.TransactionId].Value, "");
                    Int16 trangthai = Utility.Int16Dbnull(gridExRow.Cells[HoadonLog.Columns.TthaiHuy].Value);
                    string serie = Utility.sDbnull(gridExRow.Cells[HoadonLog.Columns.Serie].Value, "");
                    string MauHoadon = Utility.sDbnull(gridExRow.Cells[HoadonLog.Columns.MauHoadon].Value, "");
                    string kihieu = Utility.sDbnull(gridExRow.Cells[HoadonLog.Columns.KiHieu].Value, "");
                    if (TransactionID != "")
                    {
                        log.Trace("Tải File TransactionID: " + TransactionID);
                        if (string.IsNullOrEmpty(TransactionID)) return;
                        bool kt = _MisaInvoices.tai_hoadon(TransactionID, false, ref eMessge);
                        if (kt)
                        {
                            StoredProcedure sp =
                                SPs.EInvoiceCapnhapHoadonLog(TransactionID,
                                    globalVariables.UserName.ToString(), globalVariables.SysDate, 1);
                            sp.Execute();
                            gridExRow.IsChecked = false;
                        }
                    }

                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Tải thành công hóa đơn: {0}, trạng thái: {1}, mẫu hóa đơn: {2}, kí hiệu: {3}", serie, trangthai, MauHoadon, kihieu), newaction.Update, "UI");
                    SetValue4Prg(ProgressBar, 1);
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Tải hóa đơn bị lỗi: {0},", ex.Message), newaction.Update, "UI");

            }
            finally
            {
                VNS.Libs.AppUI.UIAction._Visible(ProgressBar, false);
                Utility.DefaultNow(this);
            }
        }
        int huybo = 0;
        private void uiTab2_SelectedTabChanged(object sender, Janus.Windows.UI.Tab.TabEventArgs e)
        {
            if (uiTabHDDT.SelectedTab == uitabChuaTaoHoaDon)
            {
                optChuaphathanh.Checked = true;
                cmdPhathanhHDon.Enabled = true;
                cmdDownload.Enabled = false;
                cmdCancelinvoices.Enabled = false;
                huybo = 0;
            }
            if (uiTabHDDT.SelectedTab == uiTabDanhSachBenhNhanDaCoHoaDon)
            {
                optDaphathanh.Checked = true;
                cmdPhathanhHDon.Enabled = false;
                cmdDownload.Enabled = true;
                cmdCancelinvoices.Enabled = true;
                huybo = 0;
            }
            if (uiTabHDDT.SelectedTab == uiTabDanhSachHoaDonDaHuy)
            {
                optDahuy.Checked = true;
                cmdPhathanhHDon.Enabled = false;
                cmdDownload.Enabled = true;
                cmdCancelinvoices.Enabled = false;
                huybo = 1;
            }
            PhanQuyenChucNang();
        }

        private void radChuaHoaDon_CheckedChanged(object sender, EventArgs e)
        {
            if (optChuaphathanh.Checked)
            {
                uiTabHDDT.SelectedTab = uitabChuaTaoHoaDon;
                chkTuNgay.Text = "Ngày Ttoán từ:";
            }
        }

        private void radDaHoaDon_CheckedChanged(object sender, EventArgs e)
        {
            if (optDaphathanh.Checked)
            {
                uiTabHDDT.SelectedTab = uiTabDanhSachBenhNhanDaCoHoaDon;
                chkTuNgay.Text = "Ngày tạo HĐ từ:";
            }
        }

        private void radDaHuy_CheckedChanged(object sender, EventArgs e)
        {
            if (optDahuy.Checked)
            {
                uiTabHDDT.SelectedTab = uiTabDanhSachHoaDonDaHuy;
                chkTuNgay.Text = "Ngày hủy HĐ từ:";
            }
        }

  

        private void cmdLayHoaDonTong_Click(object sender, EventArgs e)
        {
            try
            {
                //frm_Misa_ListPatient frm = new frm_Misa_ListPatient();
                //frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
          
        }

        private void cmdlaythongbaophathanh_Click(object sender, EventArgs e)
        {
            List<TemplateData> listhoadonphathanh = _MisaInvoices.lay_danhsach_mauhoadon();
           foreach(TemplateData mauhd in listhoadonphathanh)
            {
                if(dtMau.Select(string.Format("IPTemplateID='{0}' and InvSeries='{1}'", mauhd.IPTemplateID, mauhd.InvSeries)).Length<=0)
                {
                    HoadonMauMisa newItem = new HoadonMauMisa();
                    newItem.IPTemplateID= mauhd.IPTemplateID;
                    newItem.TemplateName = mauhd.TemplateName;
                    newItem.InvSeries = mauhd.InvSeries;
                    newItem.IsActive = false;
                    newItem.Save();
                    DataRow dr = dtMau.NewRow();
                    Utility.FromObjectToDatarow(newItem, ref dr);
                    dtMau.Rows.Add(dr);
                }    
            }
        }

        private void cmdChangeDinhDanh_Click(object sender, EventArgs e)
        {
            try
            {
                //string invInvoiceAuthId =
                //    Utility.sDbnull(grdListDaTaoHoaDon.CurrentRow.Cells["inv_InvoiceAuth_id"].Value, "");
                //if (!string.IsNullOrEmpty(invInvoiceAuthId))
                //{
                //    FrmThongtinDieuchinh frm = new FrmThongtinDieuchinh();
                //    frm.InvInvoiceAuthId = invInvoiceAuthId;
                //    //frm._invoicesConnectionDieuchinh = _invoicesConnection;
                //    frm.ShowDialog();
                //    if (frm.isCancel)
                //    {
                //        grdListDaTaoHoaDon.SelectionChanged += grdListDaTaoHoaDon_SelectionChanged;
                //    }
                //}
                //else
                //{
                //    Utility.ShowMsg("Không tồn tại dữ liệu với inv_InvoiceAuth_id = " + " ");
                //}

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void cmdCapnhatthongtinthue_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.isValidGrid(grdPayment))
                {
                    string patientCode = Utility.sDbnull(grdPayment.CurrentRow.Cells["ma_luotkham"].Value, -1);
                    int patienId = Utility.Int32Dbnull(grdPayment.CurrentRow.Cells["id_benhnhan"].Value, -1);
                    if (patientCode != "")
                    { 
                        var frm = new Frm_CapNhat_Thongtin_Thue(patientCode, patienId);
                        frm.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin) Utility.ShowMsg(ex.Message);
                log.Trace(ex.Message);
            }
        }
        private void PhanQuyenChucNang()
        {
            cmdPhathanhHDon.Visible = Utility.Coquyen("HOADONDIENTU_CREATE");
            cmdDownload.Visible = Utility.Coquyen("HOADONDIENTU_DOWNLOAD");
            cmdCancelinvoices.Visible = Utility.Coquyen("HOADONDIENTU_CANCEL");
        }

        private void cmdInHoaDonChuyenDoi_Click(object sender, EventArgs e)
        {
            //string eMessge = "";
            //Utility.ResetProgressBarJanus(ProgressBar, grdListDaTaoHoaDon.GetCheckedRows().Count(), true);
            //foreach (GridEXRow gridExRow in grdListDaTaoHoaDon.GetCheckedRows())
            //{
            //    int IdHdonLog = Utility.Int32Dbnull(gridExRow.Cells[HoadonLog.Columns.HdonLogId].Value);
            //    string serie = Utility.sDbnull(gridExRow.Cells[HoadonLog.Columns.Serie].Value);
            //    Int16 trangthai = Utility.Int16Dbnull(gridExRow.Cells[HoadonLog.Columns.TrangThai].Value);
            //    string MauHoadon = Utility.sDbnull(gridExRow.Cells[HoadonLog.Columns.MauHoadon].Value);
            //    string kihieu = Utility.sDbnull(gridExRow.Cells[HoadonLog.Columns.KiHieu].Value);
            //    DataSet dshoadonlog = SPs.SpGetinvoiceSerie(serie, MauHoadon, kihieu, 0).GetDataSet();

            //    // lay thong tin hóa đơn hủy bỏ 



            //    foreach (DataRow row in dshoadonlog.Tables[0].AsEnumerable())
            //    {
            //        if (Utility.sDbnull(row[HoadonLog.Columns.DaGui], 0) == "1" &&
            //            Utility.sDbnull(row[HoadonLog.Columns.InvInvoiceAuthId], "") != "")
            //        {
            //            string invInvoiceAuthId = Utility.sDbnull(row[HoadonLog.Columns.InvInvoiceAuthId], "");

            //            if (string.IsNullOrEmpty(invInvoiceAuthId)) return;
            //            bool kt = _HiloInvoices.Inchuyendoi(invInvoiceAuthId, false, ref eMessge);
            //            if (kt)
            //            {
            //                StoredProcedure sp =
            //                    SPs.SpCapnhapHoadonLog(
            //                        Utility.sDbnull(row[HoadonLog.Columns.InvInvoiceAuthId], ""),
            //                        globalVariables.gv_StaffID.ToString(), globalVariables.SysDate, 1);
            //                sp.Execute();
            //                gridExRow.IsChecked = false;
            //            }
            //        }
            //    }
            //    Utility.Log(this.Name, globalVariables.UserName, string.Format("Tải thành công hóa đơn chuyển đổi: {0}, trạng thái: {1}, mẫu hóa đơn: {2}, kí hiệu: {3}", serie, trangthai, MauHoadon, kihieu), action.Update, "UI");
            //    SetValue4Prg(ProgressBar, 1);
            //    Application.DoEvents();
            //}
        }

       

        private void chkTuNgay_CheckedChanged(object sender, EventArgs e)
        {
            if(chkTuNgay.Checked)
            {
                dtTuNgay.Enabled = true;
                dtDenNgay.Enabled = true;
            }
            else
            {
                dtTuNgay.Enabled = false;
                dtDenNgay.Enabled = false;
            }
        }

        private void cmdPReview_Click(object sender, EventArgs e)
        {
            try
            {

                Utility.WaitNow(this);
                if (dtMau == null || dtMau.Rows.Count <= 0 || dtMau.Select("isActive=true").Length <= 0)
                {
                    Utility.ShowMsg("Chưa có mẫu hóa đơn để phát hành hóa đơn.\nVui lòng chuyển sang tab Danh sách mẫu hóa đơn để kích hoạt mẫu hóa đơn hiện có hoặc lấy mẫu hóa đơn mới từ nhà cung cấp HĐĐT");
                    uiTabHDDT.SelectedTab = uiTabPageMauHdon;
                    cmdlaymauhoadon.Focus();
                    return;
                }
                DataRow dr = dtMau.AsEnumerable().Where(c => Utility.Obj2Bool(c["isActive"])).FirstOrDefault();
                if (dr == null)
                {
                    Utility.ShowMsg("Chưa có mẫu hóa đơn để phát hành hóa đơn.\nVui lòng chuyển sang tab Danh sách mẫu hóa đơn để kích hoạt mẫu hóa đơn hiện có hoặc lấy mẫu hóa đơn mới từ nhà cung cấp HĐĐT");
                    uiTabHDDT.SelectedTab = uiTabPageMauHdon;
                    cmdlaymauhoadon.Focus();
                    return;
                }
                _MisaInvoices.SetMauhoadon(Utility.sDbnull(dr["InvSeries"]), Utility.sDbnull(dr["IPTemplateID"]), Utility.sDbnull(dr["TemplateName"]));
                string sDataRequest = "";
                string result = string.Empty;
                int _tongso = grdPayment.GetCheckedRows().Count();

                bool kt = false;
                string _str_IdThanhtoan = "";
                string eMessage = "";
                if (grdPayment.GetCheckedRows().Count() <= 0 && Utility.isValidGrid(grdPayment))
                {
                    grdPayment.CurrentRow.BeginEdit();
                    grdPayment.CurrentRow.IsChecked = true;
                    grdPayment.CurrentRow.EndEdit();
                }
                bool isNoitru = (from p in grdPayment.GetCheckedRows() where Utility.sDbnull(p.Cells["noi_tru"].Value) == "1" select p).Count() > 0;
                bool isQuaythuoc = (from p in grdPayment.GetCheckedRows() where Utility.sDbnull(p.Cells["ttoan_thuoc"].Value) == "1" select p).Count() > 0;
                List<string> lst_maluotkham = (from p in grdPayment.GetCheckedRows() select Utility.sDbnull(p.Cells["ma_luotkham"].Value)).Distinct().ToList<string>();
                List<long> lst_Idbenhnhan = (from p in grdPayment.GetCheckedRows() select Utility.Int64Dbnull(p.Cells["id_benhnhan"].Value)).Distinct().ToList<long>();
                List<string> lst_ngay_ttoan_check = (from p in grdPayment.GetCheckedRows() select Utility.sDbnull(p.Cells["ngay_ttoan_check"].Value)).Distinct().ToList<string>();

                List<long> lstCanhbao = (from p in grdChitietThanhtoan.GetCheckedRows() where Utility.sDbnull(p.Cells["transaction_id"].Value, "") != "" select Utility.Int64Dbnull(p.Cells["id_chitiet"].Value)).Distinct().ToList<long>();
                List<long> lstCanhbao_VAT = (from p in grdChitietThanhtoan.GetCheckedRows() select Utility.Int64Dbnull(p.Cells["VAT"].Value)).Distinct().ToList<long>();
                Utility.ResetProgressBarJanus(ProgressBar, lst_Idbenhnhan.Count(), true);
                if (isQuaythuoc)
                {
                    List<Int16> lstCanhbao_VAT_Thuoc_0_phantram = (from p in grdChitietThanhtoan.GetCheckedRows() where Utility.Int16Dbnull(p.Cells["VAT"].Value) <= 0 select Utility.Int16Dbnull(p.Cells["VAT"].Value)).Distinct().ToList<Int16>();
                    if (lstCanhbao_VAT_Thuoc_0_phantram.Count > 0)
                    {

                        Utility.ShowMsg("Chỉ cho phép phát hành HĐĐT với các thuốc có thuế suất GTGT(VAT)>0.\nVui lòng kiểm tra lại các mặt hàng chi tiết đang chọn và loại bỏ các mặt hàng có thuế suất =0");
                        cboVAT.Focus();
                        return;
                    }
                }
                if (!isNoitru)
                {
                    if (lstCanhbao_VAT.Count > 1)
                    {

                        Utility.ShowMsg("Các chi tiết bạn chọn để phát hành HĐĐT có mức VAT khác nhau nên hệ thống không cho phép.\nBạn có thể dùng tính năng Lọc VAT để tìm các mặt hàng có mức VAT giống nhau trước khi thực hiện phát hành HĐĐT");
                        cboVAT.Focus();
                        return;
                    }
                }
                _MisaInvoices.VAT = lstCanhbao_VAT.Count == 1 ? Utility.Int32Dbnull(lstCanhbao_VAT[0]) : 0;
                if (lstCanhbao.Count > 0)
                {
                    Utility.ShowMsg("Một số chi tiết bạn chọn phát hành HĐĐT đã được sử dụng phát hành. Vui lòng kiểm tra lại");
                    return;
                }
                if (lst_Idbenhnhan.Count <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất 1 phiếu thu để thực hiện xem trước hóa đơn điện tử");
                    return;
                }
                if (lst_Idbenhnhan.Count > 1)
                {
                    if (!Utility.AcceptQuestion("Bạn đang chọn phát hành HĐĐT cho nhiều người bệnh. Điều này có thể sẽ tốn nhiều thời gian xủ lý.\nBạn có chắc chắn muốn tiếp tục hay không?","Xác nhận",true))
                    {
                        return;
                    }
                    //frm_ChonKhachhang _ChonKhachhang = new frm_ChonKhachhang(lstIdBenhnhan);
                    //if (_ChonKhachhang.ShowDialog() == DialogResult.OK)
                    //{
                    //    BuyerCode = _ChonKhachhang.BuyerCode;
                    //    BuyerLegalName = _ChonKhachhang.BuyerLegalName; ;
                    //    BuyerTaxCode = _ChonKhachhang.BuyerTaxCode;
                    //    BuyerAddress = _ChonKhachhang.BuyerAddress;
                    //    BuyerFullName = _ChonKhachhang.BuyerFullName;
                    //    BuyerPhoneNumber = _ChonKhachhang.BuyerPhoneNumber;
                    //    BuyerEmail = _ChonKhachhang.BuyerEmail;
                    //    BuyerBankAccount = _ChonKhachhang.BuyerBankAccount;
                    //    BuyerBankName = _ChonKhachhang.BuyerBankName;
                    //    BuyerIDNumber = "";
                    //}
                    //else
                    //{
                    //    return;
                    //}
                }
                string str_IdThanhtoanChitiet = "";
                string str_IdThanhtoan = "";
                if (optTheoThanhtoan.Checked)
                {
                    foreach (GridEXRow gridExRow in grdPayment.GetCheckedRows())
                    {
                        str_IdThanhtoan = Utility.sDbnull(gridExRow.Cells["id_thanhtoan"].Value, 0);
                        str_IdThanhtoanChitiet = string.Join(",", (from p in grdChitietThanhtoan.GetCheckedRows() where Utility.sDbnull(p.Cells["id_thanhtoan"].Value, 0) == str_IdThanhtoan select Utility.sDbnull(p.Cells["id_chitiet"].Value)).Distinct().ToArray<string>());
                       _MisaInvoices.xemtruoc_hoadon(str_IdThanhtoan, 0, str_IdThanhtoanChitiet, ref eMessage);
                        if (!string.IsNullOrEmpty(eMessage))
                        {
                            Utility.ShowMsg(eMessage);
                            LogText(eMessage, Color.DarkBlue);
                        }
                        SetValue4Prg(ProgressBar, 1);
                        Application.DoEvents();
                    }
                }
                else if (optTheoluotkham.Checked)
                {

                    if (lst_ngay_ttoan_check.Count >= 2)
                    {
                        if (!Utility.AcceptQuestion("Chú ý: Các phiếu thu khác ngày. Bạn có chắc chắn muốn phát hành HĐĐT điện tử cho các phiếu này?", "Cảnh báo các phiếu thu khác ngày", true))
                        {
                            return;
                        }
                    }
                    foreach (string maluotkham in lst_maluotkham)
                    {
                        List<long> lstIdThanhtoan = (from p in grdPayment.GetCheckedRows() where Utility.sDbnull(p.Cells["ma_luotkham"].Value) == maluotkham select Utility.Int64Dbnull(p.Cells["id_thanhtoan"].Value)).Distinct().ToList<long>();

                        str_IdThanhtoan = string.Join(",", lstIdThanhtoan.Select(l => l.ToString()).ToArray());

                        str_IdThanhtoanChitiet = string.Join(",", (from p in grdChitietThanhtoan.GetCheckedRows() where lstIdThanhtoan.Contains(Utility.Int64Dbnull(p.Cells["id_thanhtoan"].Value)) select Utility.sDbnull(p.Cells["id_chitiet"].Value)).Distinct().ToArray<string>());
                       _MisaInvoices.xemtruoc_hoadon(str_IdThanhtoan, 1, str_IdThanhtoanChitiet, ref eMessage);
                        if (!string.IsNullOrEmpty(eMessage))
                        {
                            Utility.ShowMsg(eMessage);
                            LogText(eMessage, Color.DarkBlue);
                        }
                        SetValue4Prg(ProgressBar, 1);
                        Application.DoEvents();
                        str_IdThanhtoan = "";
                    }

                }
                else
                {
                    foreach (long idbenhnhan in lst_Idbenhnhan)
                    {
                        List<long> lstIdThanhtoan = (from p in grdPayment.GetCheckedRows() where Utility.Int64Dbnull(p.Cells["id_benhnhan"].Value) == idbenhnhan select Utility.Int64Dbnull(p.Cells["id_thanhtoan"].Value)).Distinct().ToList<long>();

                        str_IdThanhtoan = string.Join(",", lstIdThanhtoan.Select(l => l.ToString()).ToArray());

                        str_IdThanhtoanChitiet = string.Join(",", (from p in grdChitietThanhtoan.GetCheckedRows() where lstIdThanhtoan.Contains(Utility.Int64Dbnull(p.Cells["id_thanhtoan"].Value)) select Utility.sDbnull(p.Cells["id_chitiet"].Value)).Distinct().ToArray<string>());
                       _MisaInvoices.xemtruoc_hoadon(str_IdThanhtoan, 1, str_IdThanhtoanChitiet, ref eMessage);
                        if (!string.IsNullOrEmpty(eMessage))
                        {
                            Utility.ShowMsg(eMessage);
                            LogText(eMessage, Color.DarkBlue);
                        }
                        SetValue4Prg(ProgressBar, 1);
                        Application.DoEvents();
                        str_IdThanhtoan = "";
                    }
                }

                //foreach (long id_benhnhan in lst_Idbenhnhan)
                //{
                //    List<long> lstIdThanhtoan = (from p in grdPayment.GetCheckedRows() where Utility.Int64Dbnull(p.Cells["id_benhnhan"].Value) == id_benhnhan select Utility.Int64Dbnull(p.Cells["id_thanhtoan"].Value)).Distinct().ToList<long>();

                //    _str_IdThanhtoan = string.Join(",", lstIdThanhtoan.Select(l => l.ToString()).ToArray());
                //    //grdPayment.UnCheckAllRecords();
                //    //if(lstIdBenhnhan.Count>1)
                //    //{
                //    //    _MisaInvoices.BuyerCode = BuyerCode;
                //    //    _MisaInvoices.BuyerLegalName = BuyerLegalName; ;
                //    //    _MisaInvoices.BuyerTaxCode = BuyerTaxCode;
                //    //    _MisaInvoices.BuyerAddress = BuyerAddress;
                //    //    _MisaInvoices.BuyerFullName = BuyerFullName;
                //    //    _MisaInvoices.BuyerPhoneNumber = BuyerPhoneNumber;
                //    //    _MisaInvoices.BuyerEmail = BuyerEmail;
                //    //    _MisaInvoices.BuyerBankAccount = BuyerBankAccount;
                //    //    _MisaInvoices.BuyerBankName = BuyerBankName;
                //    //    _MisaInvoices.BuyerIDNumber = "";
                //    //}    
                //     str_IdThanhtoanChitiet = string.Join(",", (from p in grdChitietThanhtoan.GetCheckedRows() where lstIdThanhtoan.Contains(Utility.Int64Dbnull(p.Cells["id_thanhtoan"].Value)) select Utility.sDbnull(p.Cells["id_chitiet"].Value)).Distinct().ToArray<string>());
                //    _MisaInvoices.xemtruoc_hoadon(_str_IdThanhtoan, 0, str_IdThanhtoanChitiet, ref eMessage);
                //    if (!string.IsNullOrEmpty(eMessage))
                //    {
                //        Utility.ShowMsg(eMessage);
                //        LogText(eMessage, Color.DarkBlue);
                //    }
                //    SetValue4Prg(ProgressBar, 1);
                //    Application.DoEvents();
                //}


            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                VNS.Libs.AppUI.UIAction._Visible(ProgressBar, false);
                Utility.DefaultNow(this);
            }
        }

        private void lnkReset_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtTuNgay.Value = dtDenNgay.Value = DateTime.Now;
            txtMaLanKham.Clear();
            txtTenBenhNhan.Clear();
            txt_IdBenhnhan.Clear();
            cboNhanvien.SelectedIndex = 0;
            optTatca.Checked=true;
            optChuaphathanh.Checked = true;
            dtTuNgay.Focus();
        }

        private void cmdTaiHoaDonHuy_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.WaitNow(this);
                if (!Utility.Coquyen("HOADONDIENTU_DOWNLOAD"))
                {
                    Utility.thongbaokhongcoquyen("HOADONDIENTU_DOWNLOAD", " tải về hóa đơn điện tử");
                    return;
                }
                if(grdHoadonHuy.GetCheckedRows().Count()<=0)
                {
                    grdHoadonHuy.CurrentRow.BeginEdit();
                    grdHoadonHuy.CurrentRow.IsChecked = true;
                    grdHoadonHuy.CurrentRow.EndEdit();
                }    
               // bool thongbaomofile = THU_VIEN_CHUNG.Laygiatrithamsohethong("MISA_THONGBAOMOFILE", true) == "1";

                string eMessge = "";
                Utility.ResetProgressBarJanus(ProgressBar, grdHoadonHuy.GetCheckedRows().Count(), true);
                log.Trace("Tải File invInvoiceAuthId: " + grdHoadonHuy.GetCheckedRows().Count().ToString());
                foreach (GridEXRow gridExRow in grdHoadonHuy.GetCheckedRows())
                {
                    string TransactionID = Utility.sDbnull(gridExRow.Cells[HoadonLog.Columns.TransactionId].Value, "");
                    Int16 trangthai = Utility.Int16Dbnull(gridExRow.Cells[HoadonLog.Columns.TthaiHuy].Value);
                    string serie=Utility.sDbnull(gridExRow.Cells[HoadonLog.Columns.Serie].Value, "");
                    string MauHoadon= Utility.sDbnull(gridExRow.Cells[HoadonLog.Columns.MauHoadon].Value, "");
                    string kihieu= Utility.sDbnull(gridExRow.Cells[HoadonLog.Columns.KiHieu].Value, ""); 
                    if (TransactionID != "")
                    {
                        log.Trace("Tải File TransactionID: " + TransactionID);
                        if (string.IsNullOrEmpty(TransactionID)) return;
                        bool kt = _MisaInvoices.tai_hoadon(TransactionID, false, ref eMessge);
                        if (kt)
                        {
                            StoredProcedure sp =
                                SPs.EInvoiceCapnhapHoadonLog(TransactionID,
                                    globalVariables.UserName.ToString(), globalVariables.SysDate, 1);
                            sp.Execute();
                            gridExRow.IsChecked = false;
                        }
                    }

                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Tải thành công hóa đơn: {0}, trạng thái: {1}, mẫu hóa đơn: {2}, kí hiệu: {3}", serie, trangthai, MauHoadon, kihieu), newaction.Update, "UI");
                    SetValue4Prg(ProgressBar, 1);
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Tải hóa đơn bị lỗi: {0},", ex.Message), newaction.Update, "UI");

            }
            finally
            {
                VNS.Libs.AppUI.UIAction._Visible(ProgressBar, false);
                Utility.DefaultNow(this);
            }
        }

        private void cmdChange_Click(object sender, EventArgs e)
        {
            if (splitContainer3.Orientation == Orientation.Horizontal)
                splitContainer3.Orientation = Orientation.Vertical;
            else
                splitContainer3.Orientation = Orientation.Horizontal;
        }

        private void lnkViewAllCheckedData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdPayment) || !isAllowPaymentChanged)
                {
                    grdChitietThanhtoan.DataSource = null;
                    return;
                }
                string str_IdThanhtoan = string.Join(",", (from p in grdPayment.GetCheckedRows() select Utility.sDbnull(p.Cells["id_thanhtoan"].Value)).Distinct().ToArray<string>());
                DataTable dtChitietThanhtoan = _THANHTOAN.Laychitietthanhtoan(str_IdThanhtoan, (byte)0);
                Utility.SetDataSourceForDataGridEx(grdChitietThanhtoan, dtChitietThanhtoan, true, true, "1=1", "");
                grdChitietThanhtoan.CheckAllRecords();
                List<string> lstVAT = (from p in grdChitietThanhtoan.GetCheckedRows() select Utility.sDbnull(p.Cells["VAT"].Value)).Distinct().ToList<string>();
                lblVAT.Visible = cboVAT.Visible = lstVAT.Count > 1;
                lstVAT.Insert(0, "");
                cboVAT.DataSource = lstVAT;
            }
            catch (Exception)
            {


            }
        }

        private void chkBodichvutronggoi_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ((DataView)grdChitietThanhtoan.DataSource).RowFilter = chkBodichvutronggoi.Checked ? "trong_goi=0" : "1=1";
            }
            catch (Exception ex)
            {

            }
           
        }

        private void chkBocacthanhtoandaxuathoadon_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                grdChitietThanhtoan.UnCheckAllRecords();
                ((DataView)grdPayment.DataSource).RowFilter = chkBocacthanhtoandaxuathoadon.Checked ? "tthai_xuat_hddt= false or tthai_xuat_hddt is null" : "1=1";
                grdChitietThanhtoan.CheckAllRecords();
            }
            catch (Exception ex)
            {

            }
        }

        private void chkAnChitietdaPhathanh_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                grdChitietThanhtoan.UnCheckAllRecords();
                ((DataView)grdChitietThanhtoan.DataSource).RowFilter = chkAnChitietdaPhathanh.Checked ? "tthai_xuat_hddt=0" : "1=1";
                grdChitietThanhtoan.CheckAllRecords();
            }
            catch (Exception ex)
            {

            }

        }

        private void cmdManualInvoice_Click(object sender, EventArgs e)
        {
            if(!Utility.isValidGrid(grdPayment))
            {
                Utility.ShowMsg("Bạn cần chọn người bệnh trong hệ thống trước khi tạo HĐĐT bằng tay.");
                return;
            }    
            if (dtMau == null || dtMau.Rows.Count <= 0 || dtMau.Select("isActive=true").Length <= 0)
            {
                Utility.ShowMsg("Chưa có mẫu hóa đơn để phát hành hóa đơn.\nVui lòng chuyển sang tab Danh sách mẫu hóa đơn để kích hoạt mẫu hóa đơn hiện có hoặc lấy mẫu hóa đơn mới từ nhà cung cấp HĐĐT");
                uiTabHDDT.SelectedTab = uiTabPageMauHdon;
                cmdlaymauhoadon.Focus();
                return;
            }
            DataRow dr = dtMau.AsEnumerable().Where(c => Utility.Obj2Bool(c["isActive"])).FirstOrDefault();
            if (dr == null)
            {
                Utility.ShowMsg("Chưa có mẫu hóa đơn để phát hành hóa đơn.\nVui lòng chuyển sang tab Danh sách mẫu hóa đơn để kích hoạt mẫu hóa đơn hiện có hoặc lấy mẫu hóa đơn mới từ nhà cung cấp HĐĐT");
                uiTabHDDT.SelectedTab = uiTabPageMauHdon;
                cmdlaymauhoadon.Focus();
                return;
            }
           
            DataRow drInfor = ((DataRowView)grdPayment.CurrentRow.DataRow).Row;
            
            BuyerInfor _buyer = new BuyerInfor();
            _buyer.Id_benhnhan = drInfor!=null? Utility.Int64Dbnull(drInfor["Id_benhnhan"]):-1;
            _buyer.MaLuotkham = drInfor != null ? Utility.sDbnull(drInfor["Ma_luotkham"]):"XYZ";
            _buyer.BuyerCode = drInfor != null ? Utility.sDbnull(drInfor["Ma_luotkham"]) : "XYZ";
            _buyer.BuyerLegalName = drInfor != null ? Utility.sDbnull(drInfor["ten_benhnhan"]) : "Tên công ty";
            _buyer.BuyerTaxCode = "";
            _buyer.BuyerAddress = drInfor != null ? Utility.sDbnull(drInfor["dia_chi"]) : "Địa chỉ";
            _buyer.BuyerFullName = drInfor != null ? Utility.sDbnull(drInfor["ten_benhnhan"]) : "Họ và tên";
            _buyer.BuyerPhoneNumber = drInfor != null ? Utility.sDbnull(drInfor["dien_thoai"]) : "SĐT";
            _buyer.BuyerEmail = drInfor != null ? Utility.sDbnull(drInfor["email"]) : "email";
            _buyer.BuyerBankAccount = "";
            _buyer.BuyerBankName = "";
            _buyer.BuyerIDNumber = "";
            frm_hoadon_taotay _hoadon_taotay = new frm_hoadon_taotay(_buyer, dr);
            _hoadon_taotay.ShowDialog();
        }

        private void cboVAT_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grdChitietThanhtoan.UnCheckAllRecords();
                ((DataView)grdChitietThanhtoan.DataSource).RowFilter = cboVAT.Text==""?"1=1": "VAT=" + cboVAT.Text;
                grdChitietThanhtoan.CheckAllRecords();
            }
            catch (Exception ex)
            {

            }
        }

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            ((DataView)grdChitietThanhtoan.DataSource).RowFilter = !chkCotralai.Checked? "1=1" : "co_tralai=1";
        }

        private void cmdHoadonThaythe_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.WaitNow(this);
                if (dtMau == null || dtMau.Rows.Count <= 0 || dtMau.Select("isActive=true").Length <= 0)
                {
                    Utility.ShowMsg("Chưa có mẫu hóa đơn để phát hành hóa đơn.\nVui lòng chuyển sang tab Danh sách mẫu hóa đơn để kích hoạt mẫu hóa đơn hiện có hoặc lấy mẫu hóa đơn mới từ nhà cung cấp HĐĐT");
                    uiTabHDDT.SelectedTab = uiTabPageMauHdon;
                    cmdlaymauhoadon.Focus();
                    return;
                }
                DataRow dr = dtMau.AsEnumerable().Where(c => Utility.Obj2Bool(c["isActive"])).FirstOrDefault();
                if (dr == null)
                {
                    Utility.ShowMsg("Chưa có mẫu hóa đơn để phát hành hóa đơn.\nVui lòng chuyển sang tab Danh sách mẫu hóa đơn để kích hoạt mẫu hóa đơn hiện có hoặc lấy mẫu hóa đơn mới từ nhà cung cấp HĐĐT");
                    uiTabHDDT.SelectedTab = uiTabPageMauHdon;
                    cmdlaymauhoadon.Focus();
                    return;
                }
                _MisaInvoices.SetMauhoadon(Utility.sDbnull(dr["InvSeries"]), Utility.sDbnull(dr["IPTemplateID"]), Utility.sDbnull(dr["TemplateName"]));
                string sDataRequest = "";
                string result = string.Empty;
                int _tongso = grdPayment.GetCheckedRows().Count();
                Utility.ResetProgressBarJanus(ProgressBar, _tongso, true);
                bool kt = false;
                string str_IdThanhtoan = "";
                string eMessage = "";
                if (grdPayment.GetCheckedRows().Count() <= 0 && Utility.isValidGrid(grdPayment))
                {
                    grdPayment.CurrentRow.BeginEdit();
                    grdPayment.CurrentRow.IsChecked = true;
                    grdPayment.CurrentRow.EndEdit();
                }
                if(grdPayment.GetCheckedRows().Count()>1)
                {
                    Utility.ShowMsg("Hóa đơn thay thế chỉ cho phép làm trên một phiếu thanh toán. Vui lòng bỏ check các phiếu thanh toán không bị trả lại tiền");
                    return;
                }    
                string id_thanhtoan = Utility.sDbnull(grdPayment.GetValue("id_thanhtoan"), "");
                
                bool isNoitru = (from p in grdPayment.GetCheckedRows() where Utility.sDbnull(p.Cells["noi_tru"].Value) == "1" select p).Count() > 0;
                bool isQuaythuoc = (from p in grdPayment.GetCheckedRows() where Utility.sDbnull(p.Cells["ttoan_thuoc"].Value) == "1" select p).Count() > 0;
                List<string> lst_maluotkham = (from p in grdPayment.GetCheckedRows() select Utility.sDbnull(p.Cells["ma_luotkham"].Value)).Distinct().ToList<string>();
                List<string> lst_Idbenhnhan = (from p in grdPayment.GetCheckedRows() select Utility.sDbnull(p.Cells["id_benhnhan"].Value)).Distinct().ToList<string>();
                List<string> lst_ngay_ttoan_check = (from p in grdPayment.GetCheckedRows() select Utility.sDbnull(p.Cells["ngay_ttoan_check"].Value)).Distinct().ToList<string>();
                string str_IdThanhtoanChitiet = string.Join(",", (from p in grdChitietThanhtoan.GetCheckedRows() select Utility.sDbnull(p.Cells["id_chitiet"].Value)).Distinct().ToArray<string>());
                List<long> lstCanhbao = (from p in grdChitietThanhtoan.GetCheckedRows() where Utility.sDbnull(p.Cells["transaction_id"].Value, "") != "" select Utility.Int64Dbnull(p.Cells["id_chitiet"].Value)).Distinct().ToList<long>();
                List<long> lstCanhbao_VAT = (from p in grdChitietThanhtoan.GetCheckedRows() select Utility.Int64Dbnull(p.Cells["VAT"].Value)).Distinct().ToList<long>();
                if (isQuaythuoc)
                {
                    List<Int16> lstCanhbao_VAT_Thuoc_0_phantram = (from p in grdChitietThanhtoan.GetCheckedRows() where Utility.Int16Dbnull(p.Cells["VAT"].Value) <= 0 select Utility.Int16Dbnull(p.Cells["VAT"].Value)).Distinct().ToList<Int16>();
                    if (lstCanhbao_VAT_Thuoc_0_phantram.Count > 0)
                    {

                        Utility.ShowMsg("Chỉ cho phép phát hành HĐĐT với các thuốc có thuế suất GTGT(VAT)>0.\nVui lòng kiểm tra lại các mặt hàng chi tiết đang chọn và loại bỏ các mặt hàng có thuế suất =0");
                        cboVAT.Focus();
                        return;
                    }
                }
                if (!isNoitru)
                {
                    if (lstCanhbao_VAT.Count > 1)
                    {

                        Utility.ShowMsg("Các chi tiết bạn chọn để phát hành HĐĐT có mức VAT khác nhau nên hệ thống không cho phép.\nBạn có thể dùng tính năng Lọc VAT để tìm các mặt hàng có mức VAT giống nhau trước khi thực hiện phát hành HĐĐT");
                        cboVAT.Focus();
                        return;
                    }
                }
                _MisaInvoices.VAT = lstCanhbao_VAT.Count == 1 ? Utility.Int32Dbnull(lstCanhbao_VAT[0]) : 0;
                if (lstCanhbao.Count > 0)
                {
                    Utility.ShowMsg("Một số chi tiết bạn chọn phát hành HĐĐT đã được sử dụng phát hành. Vui lòng kiểm tra lại");
                    return;
                }
                if (lst_Idbenhnhan.Count <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn phiếu thu cần phát hành HĐĐT điện tử");
                    return;
                }
                //Kiểm tra xem phiếu thanh toán này có bị gộp không, nếu bị gộp thì không cho làm hóa đơn thay thế
                DataTable dtHoadon_Phieuthanhtoan = Utility.ExecuteSql(string.Format( "select * from qhe_hoadondientu_phieuthanhtoan where id_thanhtoan={0}", id_thanhtoan), CommandType.Text).Tables[0];
                if(dtHoadon_Phieuthanhtoan.Rows.Count>1)
                {
                    Utility.ShowMsg("Phiếu thanh toán đang chọn được phát hành cùng nhiều phiếu thanh toán khác nên không thể lập hóa đơn thay thế. Vui lòng kiểm tra lại");
                    return;
                }
                var nhaplydohuythanhtoan = new frm_Chondanhmucdungchung("LYDOTHAYTHEHOADONDIENTU",
                                    "Thay thế hóa đơn điện tử", "Nhập lý do thay thế HĐĐT trước khi thực hiện",
                                    "Lý do thay thế HĐĐT", false);
                nhaplydohuythanhtoan.ShowDialog();
                if (nhaplydohuythanhtoan.m_blnCancel) return;
                string ma_lydo_thaythe = nhaplydohuythanhtoan.ma;
                string lydo_thaythey = nhaplydohuythanhtoan.ten;
                _MisaInvoices.transaction_id = "";
                //if (optTheoThanhtoan.Checked)
                //{
                    foreach (GridEXRow gridExRow in grdPayment.GetCheckedRows())
                    {
                        str_IdThanhtoan = Utility.sDbnull(gridExRow.Cells["id_thanhtoan"].Value, 0);
                        kt = _MisaInvoices.phathanh_hoadonthaythe(str_IdThanhtoan, 0, str_IdThanhtoanChitiet, lydo_thaythey,ref eMessage);
                        if (kt)
                        {
                            (from p in dtData.AsEnumerable()
                             where str_IdThanhtoan == Utility.sDbnull(p["id_thanhtoan"], "-1")
                             select p).ToList()
                             .ForEach(x =>
                             {
                                 x["tthai_xuat_hddt"] = true;
                                 x["transaction_id"] = _MisaInvoices.transaction_id;
                             }
                             );
                            LogText(eMessage, Color.DarkBlue);
                        }
                        else
                        {
                            LogText(eMessage, Color.Red);
                        }
                        SetValue4Prg(ProgressBar, 1);
                        gridExRow.IsChecked = false;
                    }
                //}
                //else if (optTheoluotkham.Checked)
                //{

                //    if (lst_ngay_ttoan_check.Count >= 2)
                //    {
                //        if (!Utility.AcceptQuestion("Chú ý: Các phiếu thu khác ngày. Bạn có chắc chắn muốn phát hành HĐĐT điện tử cho các phiếu này?", "Cảnh báo các phiếu thu khác ngày", true))
                //        {
                //            return;
                //        }
                //    }
                //    foreach (string maluotkham in lst_maluotkham)
                //    {
                //        str_IdThanhtoan = string.Join(",", (from _row in grdPayment.GetCheckedRows()
                //                                            where Utility.sDbnull(_row.Cells["ma_luotkham"].Value, "") == maluotkham
                //                                            select Utility.sDbnull(_row.Cells["id_thanhtoan"].Value)).ToArray<string>());
                //        kt = _MisaInvoices.phathanh_hoadonthaythe(str_IdThanhtoan, 1, str_IdThanhtoanChitiet, ref eMessage);
                //        if (kt)
                //        {
                //            (from p in dtData.AsEnumerable()
                //             where str_IdThanhtoan == Utility.sDbnull(p["id_thanhtoan"], "-1")
                //             select p).ToList()
                //             .ForEach(x =>
                //             {
                //                 x["tthai_xuat_hddt"] = true;
                //                 x["transaction_id"] = _MisaInvoices.transaction_id;
                //             }
                //             );
                //            LogText(eMessage, Color.DarkBlue);
                //        }
                //        else
                //        {
                //            LogText(eMessage, Color.Red);
                //        }
                //        SetValue4Prg(ProgressBar, 1);
                //        str_IdThanhtoan = "";
                //    }

                //}
                //else
                //{
                //    foreach (string idbenhnhan in lst_Idbenhnhan)
                //    {
                //        str_IdThanhtoan = string.Join(",", (from _row in grdPayment.GetCheckedRows()
                //                                            where Utility.sDbnull(_row.Cells["id_benhnhan"].Value, "") == idbenhnhan
                //                                            select Utility.sDbnull(_row.Cells["id_thanhtoan"].Value)).ToArray<string>());
                //        kt = _MisaInvoices.phathanh_hoadonthaythe(str_IdThanhtoan, 1, str_IdThanhtoanChitiet, ref eMessage);
                //        if (kt)
                //        {
                //            (from p in dtData.AsEnumerable()
                //             where str_IdThanhtoan == Utility.sDbnull(p["id_thanhtoan"], "-1")
                //             select p).ToList()
                //             .ForEach(x =>
                //             {
                //                 x["tthai_xuat_hddt"] = true;
                //                 x["transaction_id"] = _MisaInvoices.transaction_id;
                //             }
                //             );
                //            LogText(eMessage, Color.DarkBlue);
                //        }
                //        else
                //        {
                //            LogText(eMessage, Color.Red);
                //        }
                //        SetValue4Prg(ProgressBar, 1);
                //        str_IdThanhtoan = "";
                //    }
                //}
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Lỗi khi phát hành HĐĐT: {0} ",
                                     ex.Message), newaction.Insert, "UI");
            }
            finally
            {
                VNS.Libs.AppUI.UIAction._Visible(ProgressBar, false);
                Utility.DefaultNow(this);
                cmdPhathanhHDon.Enabled = true;
            }
        }
    }
}
