using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VMS.HIS.DAL;

namespace VNS.HIS.UI.Forms.Dungchung
{
    public partial class FrmBarCodePrint : Form
    {
        public DataTable m_dtReport;
        private readonly Margins _margins = new Margins(25, 25, 5, 5);
        private PrinterSettings printerSettings = new PrinterSettings();
        private PageSettings pageSettings;
        int loai_mau = 2;
        public FrmBarCodePrint(int loai_mau)
        {
            InitializeComponent();
            this.loai_mau = loai_mau;
            this.KeyDown += FrmBarCodePrint_KeyDown;
        }

        void FrmBarCodePrint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
                cmdPrint.PerformClick();
            else if (e.KeyCode == Keys.Escape)
            {
                if (cmdPrint.Enabled)
                {
                    if (Utility.AcceptQuestion("Bạn có chắc chắn muốn thoát khỏi chức năng?", "Xác nhận", true))
                        this.Close();
                }
                else
                    this.Close();
            }
        }
        private string barcodeConfigFileName =Application.StartupPath +  "\\Config\\BarcodePrinterConfig.txt";
        private string[] BarcodeConfig;
        public Margins margins = new Margins();
        public Int32 marginLeft;
        public Int32 marginTop;
        public Int32 marginleft2;
        private int ConfigCount = 3;
        private void CreateConfigFile()
        {
            BarcodeConfig = new string[ConfigCount];
            BarcodeConfig[0] = "-1";
            BarcodeConfig[1] = "3";
            BarcodeConfig[2] = "1";
        }

        private void FrmBarCodePrint_Load(object sender, EventArgs e)
        {
            
            if (m_dtReport.Rows.Count > 0)
            {
                txtHovaTen.Text = Utility.sDbnull(m_dtReport.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan]);
                txtngaysinh.Text = Utility.sDbnull(m_dtReport.Rows[0][KcbDanhsachBenhnhan.Columns.NamSinh]);
                txtgioitinh.Text = Utility.sDbnull(m_dtReport.Rows[0][KcbDanhsachBenhnhan.Columns.GioiTinh]);
                txtPatientcode.Text = Utility.sDbnull(m_dtReport.Rows[0][KcbLuotkham.Columns.MaLuotkham]);
                txtPatientId.Text = Utility.sDbnull(m_dtReport.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan]);
                cmdPrint.Enabled = true;
            }
            else
            {
                txtHovaTen.Text = "";
                txtngaysinh.Text = "";
                txtgioitinh.Text = "";
                txtPatientcode.Text = "";
                txtPatientId.Text = "";
                cmdPrint.Enabled = false;
                Utility.ShowMsg("Không tồn tại bệnh nhân", "Cảnh báo", MessageBoxIcon.Warning);
                return;
            }
            if (File.Exists(barcodeConfigFileName))
            {
                BarcodeConfig = File.ReadAllLines(barcodeConfigFileName);
            }
            else
            {
                CreateConfigFile();
            }
            if (BarcodeConfig.Length < ConfigCount)
            {
                CreateConfigFile();
            }
            if (PrinterSettings.InstalledPrinters.Count > 0)
            {
                foreach (string strPrinter in PrinterSettings.InstalledPrinters)
                {
                    cboPrinters.Items.Add(strPrinter);
                }
            }
            nmrSoLuong.Text = Utility.sDbnull(BarcodeConfig[1]);
            chkPreview.Checked = Utility.sDbnull(BarcodeConfig[2]) == "1";

            try
            {
                cboPrinters.Text = BarcodeConfig[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //Nếu không có máy in nào thì hiện thông báo và huỷ
            if (cboPrinters.Items.Count == 0)
            {
                MessageBox.Show(
                    @"Không có máy in nào trong hệ thống\r\n" +
                    @"Hoặc máy in chưa được kết nối với máy tính", @"Thông Báo", MessageBoxButtons.OK);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Utility.CleanTemporaryFolders();
        }

        private void SaveConfig()
        {
            BarcodeConfig[0] = cboPrinters.Text;
            BarcodeConfig[1] = nmrSoLuong.Value.ToString();
            BarcodeConfig[2] = chkPreview.Checked ? "1" : "0";
            File.WriteAllLines(barcodeConfigFileName, BarcodeConfig);
        }
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PrintBarCode(cboPrinters.Text, 2, Utility.Int16Dbnull(nmrSoLuong.Text), chkPreview.Checked);
                SaveConfig();
                this.Close();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        void PrintBarCode(string printerName, int sohang, short NumOfCopies, bool isView)
        {
            for (int i = 0; i < NumOfCopies; i++)
            {
                string BarcodeSize = loai_mau == 2 ? "BarcodeSize_2TEM" : "BarcodeSize_3TEM";
                string BarcodeMargins = loai_mau == 2 ? "BarcodeMargins_2TEM" : "BarcodeMargins_3TEM";

                List<string> lstSize = THU_VIEN_CHUNG.Laygiatrithamsohethong(BarcodeSize, "500x900", true).Split('x').ToList<string>();
                List<string> lstMargins = THU_VIEN_CHUNG.Laygiatrithamsohethong(BarcodeMargins, "25, 25, 5, 5", true).Split(',').ToList<string>();
                BarcodeConfig[0] = cboPrinters.Text;
                printerSettings.PrinterName = printerName;
                //printerSettings.Copies = NumOfCopies;
                pageSettings = new PageSettings(printerSettings);
                pageSettings.Margins = new Margins(Convert.ToInt32(lstMargins[0]), Convert.ToInt32(lstMargins[1]), Convert.ToInt32(lstMargins[2]), Convert.ToInt32(lstMargins[3]));
                pageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Barcode", Convert.ToInt32(lstSize[0]), Convert.ToInt32(lstSize[1]));

                var barcodeID = new KeepAutomation.Barcode.Crystal.BarCode();
                barcodeID.DPI = 72;
                barcodeID.Orientation = KeepAutomation.Barcode.Orientation.Degree90;
                barcodeID.DisplayText = false;
                barcodeID.CodeToEncode = Utility.sDbnull(txtPatientcode.Text, "None");
                byte[] bytData = barcodeID.generateBarcodeToByteArray();
                (from p in m_dtReport.AsEnumerable() select p).ToList().ForEach(x => x["barcode"] = bytData);
                barcodeID.CodeToEncode = Utility.sDbnull(txtPatientId.Text, "None");
                bytData = barcodeID.generateBarcodeToByteArray();
                (from p in m_dtReport.AsEnumerable() select p).ToList().ForEach(x => x["barcodeID"] = bytData);

                var reportDocument = new ReportDocument();
                string tieude = "", reportname = "";
                string report_code = string.Format("{0}", loai_mau == 2 ? "BARCODE_2" : "BARCODE_3");
                reportDocument = Utility.GetReport(report_code, ref tieude, ref reportname);

                if (reportDocument == null)
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy báo cáo với mã {0}", report_code));
                    return;
                }

                ReportDocument crpt = reportDocument;
                var objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count > 0);
                //Utility.UpdateLogotoDatatable(ref m_dtReport);
                try
                {
                    m_dtReport.AcceptChanges();
                    crpt.SetDataSource(m_dtReport);
                    objForm.mv_sReportFileName = Path.GetFileName(reportname);
                    objForm.mv_sReportCode = report_code;
                    //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                    //Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                    Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, globalVariables.SysDate));
                    objForm.crptViewer.ReportSource = crpt;
                    if (isView)
                    {
                        objForm.SetDefaultPrinter(BarcodeConfig[0], 0);
                        objForm.ShowDialog();
                    }
                    else
                    {
                        objForm.addTrinhKy_OnFormLoad();
                        crpt.PrintOptions.PrinterName = BarcodeConfig[0];

                        crpt.PrintToPrinter(printerSettings, pageSettings, false);
                        //crpt.PrintToPrinter(NumOfCopies, false, 0, 0);
                    }
                    m_dtReport.Dispose();
                    crpt.Close();
                    crpt.Dispose();
                    objForm.Dispose();
                    GC.Collect();
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg(ex.ToString());
                }
                finally
                {

                }
            }
        }
        void PrintBarCode_V1(string printerName, int sohang, int copy, bool isView)
        {
            //for (int i = 0; i < copy; i++)
            //{
            BarcodeConfig[0] = cboPrinters.Text;
            //printerSettings.PrinterName = printerName;

            //pageSettings = new PageSettings(printerSettings);
            //pageSettings.Margins = new Margins(0, 0, 0, 0);
            //pageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Barcode", 500, 90);
            var reportDocument = new ReportDocument();
            string tieude = "", reportname = "";
            string report_code = string.Format("{0}_{1}", "BARCODE", sohang);
            reportDocument = Utility.GetReport(report_code, ref tieude, ref reportname);

            if (reportDocument == null)
            {
                Utility.ShowMsg(string.Format("Không tìm thấy báo cáo với mã {0}", report_code));
                return;
            }

            ReportDocument crpt = reportDocument;
            var objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count > 0);
            //Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = report_code;
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                //Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, globalVariables.SysDate));
                objForm.crptViewer.ReportSource = crpt;
                if (isView)
                {
                    objForm.SetDefaultPrinter(BarcodeConfig[0], 0);
                    objForm.ShowDialog();
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = BarcodeConfig[0];
                    //crpt.PrintToPrinter(printerSettings, pageSettings, false);
                    crpt.PrintToPrinter(copy, false, 0, 0);
                }
                m_dtReport.Dispose();
                crpt.Close();
                crpt.Dispose();
                objForm.Dispose();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
            finally
            {

            }
        }
    }
}
