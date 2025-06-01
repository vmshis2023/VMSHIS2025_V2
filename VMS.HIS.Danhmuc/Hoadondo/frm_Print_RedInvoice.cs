using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX;
using VNS.Libs;
using SubSonic;
using VMS.HIS.DAL;
using System.Transactions;
using VNS.HIS.UI.HOADONDO;

using VNS.HIS.BusRule.Classes;
using VNS.HIS.Classes;
namespace VNS.HIS.UI.HOADONDO
{

    public partial class frm_Print_RedInvoice : Form
    {
      
        public int payment_ID;
        public DataTable dtList;
        private DataTable dtPatientPayment;
        private DataTable dtCapPhat;
        string sFileName = "RedInvoicePrinterConfig.txt";
        public frm_Print_RedInvoice()
        {
            InitializeComponent();
        }

        private void frm_Print_RedInvoice_Load(object sender, EventArgs e)
        {
            try
            {
               
                LoadPrinter();
                Load_LyDo();
                LoadPatientPaymentInfo();
                LoadInvoiceInfo();
                if (Utility.Int32Dbnull(grdList.GetValue("HDON_LOG_ID")) > 0)
                {
                    cboLy_Do.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            
        }

        private void LoadPrinter()
        {
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cboPrinter.Items.Add(printer,printer);
            }
            if (cboPrinter.Items.Count > 0) cboPrinter.SelectedIndex = 0;

            if (System.IO.File.Exists(sFileName))
            {
                string[] printerName = System.IO.File.ReadAllLines(sFileName);
                if (printerName.Length > 0)
                    cboPrinter.SelectedValue = printerName[0];
            }
        }

        private void Load_LyDo()
        {
            DataTable dt_LyDo =
                new Select("*").From(HoadonTrangthai.Schema).Where(HoadonTrangthai.Columns.MessageType).IsEqualTo("LyDoInHD").
                    OrderAsc(HoadonTrangthai.Columns.MessageOrder).ExecuteDataSet().Tables[0];
            DataBinding.BindData(cboLy_Do,dt_LyDo,HoadonTrangthai.Columns.MessageId,HoadonTrangthai.Columns.MessageName);
        }
        private void LoadInvoiceInfo()
        {
            lblStaff_Name.Text = string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien) ? "ADMIN" : globalVariables.gv_strTenNhanvien;
            if (globalVariables.UserName != null)
                dtCapPhat = SPs.HoadondoLaydanhsachHoadonDacapphatTheouser(globalVariables.UserName).GetDataSet().Tables[0];
            if (dtCapPhat.Rows.Count <= 0 )
            {
                Utility.ShowMsg("Đã xử dụng hết hóa đơn được cấp");
                Close();
            }
            grdHoaDonCapPhat.DataSource = dtCapPhat;
            grdHoaDonCapPhat.AutoSizeColumns();
            HoadonLog HoadonLog =
                new Select().From(HoadonLog.Schema).Where(HoadonLog.Columns.MaNhanvien).IsEqualTo(
                    globalVariables.UserName).OrderDesc(HoadonLog.Columns.IdHdonLog)
                    .And(HoadonLog.Columns.TrangThai).IsEqualTo(0)
                    .ExecuteSingle<HoadonLog>();
            if(HoadonLog != null)
            {
                Utility.GonewRowJanus(grdHoaDonCapPhat, HoadonCapphat.Columns.IdCapphat, Utility.sDbnull(HoadonCapphat.Columns.IdCapphat));
            }
            //txtMauHD.Text = Utility.sDbnull(dtCapPhat.Rows[0]["MAU_HDON"]);
            //txtKiHieu.Text = Utility.sDbnull(dtCapPhat.Rows[0]["KI_HIEU"]);
            //int sSerie = Utility.Int32Dbnull(dtCapPhat.Rows[0]["SERIE_HTAI"]);
            //txtSerie.Text = Utility.sDbnull(sSerie <= 0 ? Utility.Int32Dbnull(dtCapPhat.Rows[0]["SERIE_DAU"]) : sSerie + 1);
            //txtSerie.Text = txtSerie.Text.PadLeft(Utility.sDbnull(dtCapPhat.Rows[0]["SERIE_DAU"]).Length, '0');
        }

        private void LoadPatientPaymentInfo()
        {
            try
            {
                dtPatientPayment = SPs.HoadondoLaythongtinhoadonTheothanhtoan(payment_ID).GetDataSet().Tables[0];
                dtPatientPayment.Rows[0]["sotien_bangchu"] = new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(dtPatientPayment.Rows[0]["TONG_TIEN"]));
                Utility.SetDataSourceForDataGridEx(grdList,dtPatientPayment,false,true,"","",true);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frm_Print_RedInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F4 :
                    cmdPrint.PerformClick();
                    break;
                case Keys.Escape :
                    cmdThoat.PerformClick();
                    break;
            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            bool hasError = false;
            try
            {
                if (!ValidData()) return;
                try
                {
                    using (var Scope = new TransactionScope())
                    {

                        using (var dbScope = new SharedDbConnectionScope())
                        {
                            if (Utility.Int32Dbnull(grdList.GetValue(HoadonLog.Columns.IdHdonLog)) > 0)
                                new Update(HoadonLog.Schema).Set(HoadonLog.Columns.TrangThai).EqualTo(1).Where(
                                    HoadonLog.Columns.IdHdonLog).IsEqualTo(Utility.Int32Dbnull(grdList.GetValue(HoadonLog.Columns.IdHdonLog))).
                                    Execute();
                            HoadonLog obj = new HoadonLog();
                            obj.IdThanhtoan = payment_ID.ToString();
                            obj.TongTien = Utility.DecimaltoDbnull(grdList.GetValue(HoadonLog.Columns.TongTien));
                            obj.IdBenhnhan = Utility.Int32Dbnull(grdList.GetValue(HoadonLog.Columns.IdBenhnhan));
                            obj.MaLuotkham = Utility.sDbnull(grdList.GetValue(HoadonLog.Columns.MaLuotkham));
                            obj.MauHoadon = txtMauHD.Text;
                            obj.KiHieu = txtKiHieu.Text;
                            obj.IdCapphat = Utility.Int32Dbnull(grdHoaDonCapPhat.GetValue(HoadonLog.Columns.IdCapphat));
                            obj.MaQuyen = txtMaQuyen.Text;
                            obj.Serie = txtSerie.Text;
                            obj.MaNhanvien = globalVariables.UserName;
                            obj.MaLydo = Utility.sDbnull(cboLy_Do.SelectedValue);
                            obj.NgayIn =globalVariables.SysDate;
                            obj.TrangThai = 0;
                            obj.IsNew = true;
                            obj.Save();
                            new Update(HoadonCapphat.Schema).Set(HoadonCapphat.Columns.SerieHientai).EqualTo(txtSerie.Text).
                                Set(HoadonCapphat.Columns.TrangThai).EqualTo(1).
                                Where(HoadonCapphat.Columns.IdCapphat).IsEqualTo(Utility.Int32Dbnull(grdHoaDonCapPhat.GetValue(HoadonCapphat.Columns.IdCapphat))).
                                Execute();
                        }
                        Scope.Complete();
                    }
                }
                catch (Exception ex1)
                {
                    Utility.ShowMsg("Lỗi khi thực hiện in hóa đơn mẫu. Liên hệ VNS để được trợ giúp-->"+ex1.Message);      
                    hasError = true;
                }
                if (!hasError)
                {
                    ActionResult actionResult = new KCB_THANHTOAN().Capnhattrangthaithanhtoan(payment_ID);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            new INPHIEU_THANHTOAN_NGOAITRU().IN_HOADON(payment_ID);
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình in hóa đơn", "Thông báo lỗi", MessageBoxIcon.Warning);
                            break;
                    }
                }
                //DataRow dr = dtList.NewRow();
                //ProcessData(ref dr);
                //dtList.Rows.InsertAt(dr, 0);
                //dtList.AcceptChanges();  
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);                
            }
            Close();
        }

        private void SavePrinterConfig()
        {
            try
            {
                 System.IO.File.WriteAllLines(sFileName,new string[] {Utility.sDbnull(cboPrinter.SelectedValue)});
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            
        }

        private bool ValidData()
        {
            try
            {
                //if (Utility.sDbnull(grdHoaDonCapPhat.GetValue("SERIE_DAU")).Length != txtSerie.Text.Length)
                //{
                //    Utility.ShowMsg("Số ký tự serie không đúng");
                //    return false;
                //}

                if (Utility.Int32Dbnull(grdHoaDonCapPhat.GetValue(HoadonCapphat.Columns.SerieDau)) > Utility.Int32Dbnull(txtSerie.Text) |
                    Utility.Int32Dbnull(grdHoaDonCapPhat.GetValue(HoadonCapphat.Columns.SerieCuoi)) < Utility.Int32Dbnull(txtSerie.Text))
                {
                    Utility.ShowMsg(string.Format("Số ký tự serie không trong khoảng cho phép ({0} - {1})",
                                                  Utility.sDbnull(grdHoaDonCapPhat.GetValue(HoadonCapphat.Columns.SerieDau)),
                                                  Utility.sDbnull(grdHoaDonCapPhat.GetValue(HoadonCapphat.Columns.SerieCuoi))));
                    return false;
                }

                if (HoadonLog.CreateQuery().WHERE(HoadonLog.Columns.MauHoadon,txtMauHD.Text).AND(HoadonLog.Columns.KiHieu,txtKiHieu.Text).AND(HoadonLog.Columns.Serie,txtSerie.Text).GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Serie đã được sử dụng");
                    return false;
                }
                if (Utility.Int32Dbnull(grdList.GetValue(HoadonLog.Columns.IdHdonLog)) > 0)
                {
                    if (cboLy_Do.SelectedIndex == 0)
                    {
                        Utility.ShowMsg("Lần thanh toán đã được in hóa đơn. Mời bạn chọn kiểu in thay thế hoặc hủy hóa đơn cũ để in mới");
                        return false;
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

        private void ProcessData(ref DataRow dr)
        {
            try
            {
                //dr["Trang_Thai_Message"]
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        private void grdHoaDonCapPhat_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdHoaDonCapPhat.CurrentRow != null)
                {
                    if (grdHoaDonCapPhat.CurrentRow.RowType == RowType.Record)
                    {
                        txtMauHD.Text = Utility.sDbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.MauHoadon].Value, "");
                        txtKiHieu.Text = Utility.sDbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.KiHieu].Value, "");
                        int sSerie = Utility.Int32Dbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.SerieHientai].Value,0);
                        txtSerie.Text = Utility.sDbnull(sSerie <= 0 ? Utility.Int32Dbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.SerieDau].Value,0) : sSerie + 1);
                        txtSerie.Text = txtSerie.Text.PadLeft(Utility.sDbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.SerieDau].Value).Length, '0');
                        txtMaQuyen.Text = Utility.sDbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.MaQuyen].Value, "");
                    }
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin mẫu hóa đơn trên lưới.");               
            }
        }

        private void cboLy_Do_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình chọn kiểu in");               
            }
        }

        private void txtSerie_Leave(object sender, EventArgs e)
        {
            try
            {
                txtSerie.Text = txtSerie.Text.PadLeft(Utility.sDbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.SerieDau].Value).Length, '0');
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình nhập dữ liệu");
            }
        }

        private void txtSerie_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                try
                {
                    txtSerie.Text = txtSerie.Text.PadLeft(Utility.sDbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.SerieDau].Value).Length, '0');
                }
                catch (Exception)
                {
                    Utility.ShowMsg("Có lỗi trong quá trình nhập dữ liệu");
                }
            }
        }
    }
}
