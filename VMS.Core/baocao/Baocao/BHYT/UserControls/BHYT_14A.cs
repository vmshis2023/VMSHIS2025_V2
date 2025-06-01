using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VNS.HIS.DAL;
using VNS.Libs.AppUI;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.VisualBasic;
using CrystalDecisions.Shared;
using VNS.HIS.BusRule.Classes;


namespace VNS.HIS.UI.Baocao
{
    public partial class BHYT_14A : UserControl
    {
        private DataTable mdtReport;
        private MoneyByLetter sMoneyByLetter = new MoneyByLetter();
        public BHYT_14A()
        {
            InitializeComponent();
            InitEvents();
        }
        void InitEvents()
        {
            cmdSearch.Click += new EventHandler(cmdSearch_Click);
            cmdPrint.Click += new EventHandler(cmdPrint_Click);
            cmdPreview.Click += new EventHandler(cmdPreview_Click);
            cmdExcel.Click += new EventHandler(cmdExcel_Click);
            dtpFromDate.ValueChanged += new EventHandler(dtpFromDate_ValueChanged);
            dtpToDate.ValueChanged += new EventHandler(dtpToDate_ValueChanged);
            this.KeyDown += new KeyEventHandler(BHYT_14A_KeyDown);
            this.Load += new EventHandler(BHYT_14A_Load);
            optNew.CheckedChanged += new EventHandler(optNew_CheckedChanged);
            optOld.CheckedChanged += new EventHandler(optOld_CheckedChanged);
           
            
        }

        void optOld_CheckedChanged(object sender, EventArgs e)
        {
            baocaO_TIEUDE1.Init("BHYT_14A_CU");
        }

        void optNew_CheckedChanged(object sender, EventArgs e)
        {
            baocaO_TIEUDE1.Init("BHYT_14A_MOI");
        }

       

        void BHYT_14A_Load(object sender, EventArgs e)
        {

        }

       

       
        public void Init()
        {
            dtPrintDate.Value = globalVariables.SysDate;
            dtpFromDate.Value = globalVariables.SysDate;
            dtpToDate.Value = globalVariables.SysDate;

            DataBinding.BindData(cboObject, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(new List<string>(){ "BHYT"}),
                                DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb);


            baocaO_TIEUDE1.Init("BHYT_14A_MOI");
            txtNhomBHYT.Init();
        }
        void BHYT_14A_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }

        void cmdExcel_Click(object sender, EventArgs e)
        {
            gridEXExporter1.GridEX = grdList;
            //Janus.Windows.GridEX.GridEXRow[] gridExRows = grdList.GetCheckedRows();
            if (gridEXExporter1.GridEX.RowCount <= 0)
            {
                Utility.ShowMsg("Không có dữ liệu", "Thông báo");
                //grdList.Focus();
                return;
            }
            saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
            saveFileDialog1.FileName = "BaoCaoBHYT-14Aa.xls";
            //saveFileDialog1.ShowDialog();

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sPath = saveFileDialog1.FileName;
                FileStream fs = new FileStream(sPath, FileMode.Create);
                fs.CanWrite.CompareTo(true);
                fs.CanRead.CompareTo(true);
                gridEXExporter1.Export(fs);
                fs.Dispose();
            }
            saveFileDialog1.Dispose();
            saveFileDialog1.Reset();
        }

        void cmdPreview_Click(object sender, EventArgs e)
        {
            PrintReport(true);
        }

        void cmdPrint_Click(object sender, EventArgs e)
        {
            PrintReport(false);
        }
        private void PrintReport(bool view)
        {
            if (optOld.Checked)
                PrintViewOld14a(view);
            else
                PrintViewNew14a(view);
            
        }
        private void PrintViewNew14a(bool view)
        {
            if (mdtReport != null)
            {
                if (mdtReport.Rows.Count <= 0 || mdtReport.Columns.Count <= 0)
                    return;
                 string tieude="", reportname = "";
                ReportDocument crpt = Utility.GetReport("BHYT_14A_MOI",ref tieude,ref reportname);
                if (crpt == null) return;
                crpt.SetDataSource(mdtReport);
                frmPrintPreview objForm = new frmPrintPreview(Utility.DoTrim(baocaO_TIEUDE1.TIEUDE), crpt, true, mdtReport.Rows.Count <= 0 ? false : true);
                Utility.UpdateLogotoDatatable(ref mdtReport);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "BHYT_14A_MOI";
                Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt,"sBangChu", sMoneyByLetter.sMoneyToLetter(SumOfTotal(mdtReport)));
                Utility.SetParameterValue(crpt,"sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtPrintDate.Value));
                Utility.SetParameterValue(crpt,"title", baocaO_TIEUDE1.TIEUDE);
                string Condition = string.Format("Từ ngày {0} đến {1} - Đối tượng : {2} - Nhóm :{3}", dtpFromDate.Text, dtpToDate.Text,
                                  cboObject.SelectedIndex > 0
                                      ? Utility.sDbnull(cboObject.Text)
                                      : "Tất cả",
                                  txtNhomBHYT.myCode!="-1"? txtNhomBHYT.myCode: "Tất cả");
                Utility.SetParameterValue(crpt,"sCondition", Condition);
               
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "            Lập biểu                                     Trưởng phòng giám định BHYT                                  Trưởng phòng KHTC                                                Giám đốc".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                objForm.crptViewer.ReportSource = crpt;
                if (view)
                {

                    objForm.ShowDialog();
                    

                }
                else
                {
                    crpt.PrintOptions.PaperSize = PaperSize.PaperLetter;
                    crpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                    crpt.PrintToPrinter(1, false, 1, 1);
                }
            }
        }

        private void PrintViewOld14a(bool view)
        {
            if (mdtReport != null)
            {
                if (mdtReport.Rows.Count <= 0 || mdtReport.Columns.Count <= 0)
                    return;
                 string tieude="", reportname = "";
                ReportDocument crpt = Utility.GetReport("BHYT_14A_CU",ref tieude,ref reportname);
                if (crpt == null) return;
                frmPrintPreview frmPrintView = new frmPrintPreview(Utility.DoTrim(baocaO_TIEUDE1.TIEUDE), crpt, true, true);
                Utility.UpdateLogotoDatatable(ref mdtReport);
                crpt.SetDataSource(mdtReport);
                frmPrintView.mv_sReportFileName = Path.GetFileName(reportname);
                frmPrintView.mv_sReportCode = "BHYT_14A_CU";
                Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt,"sBangChu", sMoneyByLetter.sMoneyToLetter(SumOfTotal(mdtReport)));
                Utility.SetParameterValue(crpt,"sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtPrintDate.Value));
                Utility.SetParameterValue(crpt,"Title", baocaO_TIEUDE1.TIEUDE);
                Utility.SetParameterValue(crpt, "sCondition", "Đối tượng : " + cboObject.Text + "; Nhóm :" + txtNhomBHYT.myCode != "-1" ? txtNhomBHYT.myCode : "Tất cả" + "; Từ ngày :" + dtpFromDate.Text + " đến ngày " + dtpToDate.Text);
               
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "        NGƯỜI LẬP                                       THỦ TRƯỞNG ĐƠN VỊ           ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                frmPrintView.crptViewer.ReportSource = crpt;
                if (view)
                {
                        frmPrintView.ShowDialog();
                }
                else
                {
                    crpt.PrintOptions.PaperSize = PaperSize.PaperA4;
                    crpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                    crpt.PrintToPrinter(1, false, 1, 1);
                }
            }
        }
        private string SumOfTotal(DataTable dataTable)
        {
            return Utility.sDbnull(dataTable.Compute("SUM(BHTT)", "1=1"), 0);
        }
      
        void cmdSearch_Click(object sender, EventArgs e)
        {
            LoadDataView();
        }
        void LoadDataView()
        {
            try
            {
            mdtReport = new BAOCAO_BHYT().BHYT_14A_NEW(dtpFromDate.Value, dtpToDate.Value,"BHYT", txtNhomBHYT.myCode);
            ModifyCommands();
            THU_VIEN_CHUNG.CreateXML(mdtReport, "BHYT14A.xml");
            grdList.DataSource = mdtReport;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }

        }
        void ModifyCommands()
        {
            cmdPrint.Enabled = mdtReport != null && mdtReport.Rows.Count > 0;
            cmdPreview.Enabled = cmdPrint.Enabled;
            cmdExcel.Enabled = cmdPrint.Enabled;
        }
     
        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFromDate.Value > dtpToDate.Value)
            {
                dtpToDate.Value = dtpFromDate.Value;
            }
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFromDate.Value > dtpToDate.Value)
            {
                dtpFromDate.Value = dtpToDate.Value;
            }
        }
    }
}
