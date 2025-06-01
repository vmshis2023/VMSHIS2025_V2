using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VNS.HandyTools;

namespace VNS.MultiReport
{
    public partial class frmMultiReportQueryEditor_V2 : Form
    {
        public frmMultiReportQueryEditor_V2()
        {
            InitializeComponent();
        }

        private int reportID = -1;

        private void frmMultiReportQueryEditor_Load(object sender, EventArgs e)
        {
            cboQueryType.SelectedIndex = 0;
            ActiveControl = nmrReportID;
        }


        private void frmMultiReportQueryEditor_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control)
                    switch (e.KeyCode)
                    {
                        case Keys.S : 
                            btnSave.PerformClick();
                            break;
                        case Keys.N:
                            btnAddNew.PerformClick();
                            break;
                    }
                else
                switch (e.KeyCode)
                {
                    case Keys.Escape: Close();
                        break;
                    case Keys.F3: ActiveControl = nmrReportID;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportID <= 0) return;
                if (MessageBox.Show(string.Format("Thực hiện lưu query với ID = {0} ?",reportID),"Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question) != DialogResult.Yes) return;
                List<SqlData> lstData = new List<SqlData>();
                lstData.Add(new SqlData("MultiReport_Name", SqlUtility.SetValueText(txtReport_Name.Text.Trim())));
                lstData.Add(new SqlData("MultiReport_Type", SqlUtility.SetValueText(txtReportType.Text.Trim())));
                lstData.Add(new SqlData("MultiReport_Query_Type", SqlUtility.SetValueText(cboQueryType.Text)));
                lstData.Add(new SqlData("MultiReport_Query_String", SqlUtility.SetValueText(txtQuery.Text.Trim())));
                lstData.Add(new SqlData("MultiReport_Sequence", Utility.sDbnull(txtSequence.Value)));
                lstData.Add(new SqlData("MultiReport_IsAdmin", ckbIsAdmin.Checked ? "1" : "0"));
                SqlUtility.Update("D_Multi_Report", lstData, "MultiReport_ID = " + reportID);
                UpdateExcelConfig();
                UpdateSheetConfig();
                Utility.ShowMsg("Lưu query thành công !");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void UpdateExcelConfig()
        {
            foreach (GridEXRow row in grdConfig.GetRows())
            {
                if (Utility.Int16Dbnull(row.Cells["IsUpdated"].Value) == 1)
                {
                    List<SqlData> lstData = new List<SqlData>();
                    lstData.Add(new SqlData("MultiReport_ID", Utility.sDbnull(reportID)));
                    lstData.Add(new SqlData("MultiReport_Config_Table_Index", Utility.sDbnull(Utility.Int32Dbnull(row.Cells["MultiReport_Config_Table_Index"].Value)).Trim()));
                    lstData.Add(new SqlData("MultiReport_Config_SqlColName", SqlUtility.SetValueText(SqlUtility.GetVerifiedDbColNameString(Utility.sDbnull(row.Cells["MultiReport_Config_SqlColName"].Value)).Trim())));
                    lstData.Add(new SqlData("MultiReport_Config_ExcelColName", SqlUtility.SetValueText(SqlUtility.GetVerifiedDbColNameString(Utility.sDbnull(row.Cells["MultiReport_Config_ExcelColName"].Value)).Trim())));
                    lstData.Add(new SqlData("MultiReport_Config_ExcelColWidth", Utility.Int32Dbnull(row.Cells["MultiReport_Config_ExcelColWidth"].Value,15).ToString()));
                    lstData.Add(new SqlData("MultiReport_Config_ExcelColPosition", Utility.sDbnull(Utility.Int32Dbnull(Utility.sDbnull(row.Cells["MultiReport_Config_ExcelColPosition"].Value).Trim(),0))));
                    lstData.Add(new SqlData("MultiReport_Config_ExcelColNumberFormat", SqlUtility.SetValueText(SqlUtility.GetVerifiedDbColNameString(Utility.sDbnull(row.Cells["MultiReport_Config_ExcelColNumberFormat"].Value).Trim()))));
                    lstData.Add(new SqlData("MultiReport_Config_ExcelColAggregateFunction", SqlUtility.SetValueText(SqlUtility.GetVerifiedDbColNameString(Utility.sDbnull(row.Cells["MultiReport_Config_ExcelColAggregateFunction"].Value).Trim()))));
                    if (string.IsNullOrEmpty(Utility.sDbnull(row.Cells["MultiReport_Config_ID"].Value)))
                    {
                        row.BeginEdit();
                        row.Cells["MultiReport_Config_ID"].Value = Utility.Int32Dbnull(SqlUtility.InsertScopeIdentity("D_Multi_Report_Config", lstData));
                        row.EndEdit();
                    }
                    else SqlUtility.Update("D_Multi_Report_Config", lstData, "MultiReport_Config_ID = " + Utility.sDbnull(row.Cells["MultiReport_Config_ID"].Value));
                    row.BeginEdit();
                    row.Cells["IsUpdated"].Value = 0;
                    row.EndEdit();
                }
            }
        }

        private void UpdateSheetConfig()
        {
            foreach (GridEXRow row in grdSheetConfig.GetRows())
            {
                if (Utility.Int16Dbnull(row.Cells["IsUpdated"].Value) == 1)
                {
                    List<SqlData> lstData = new List<SqlData>();
                    lstData.Add(new SqlData("MultiReport_ID", Utility.sDbnull(reportID)));
                    lstData.Add(new SqlData("MultiReport_Config_Table_Index", Utility.sDbnull(Utility.Int32Dbnull(row.Cells["MultiReport_Config_Table_Index"].Value)).Trim()));
                    lstData.Add(new SqlData("Header_Blank_Row_Count", Utility.Int16Dbnull(row.Cells["Header_Blank_Row_Count"].Value, 0).ToString()));
                    lstData.Add(new SqlData("Header_Title", SqlUtility.SetValueText(SqlUtility.GetVerifiedDbColNameString(Utility.sDbnull(row.Cells["Header_Title"].Value)).Trim())));
                    lstData.Add(new SqlData("Header_Title_Cell_Count", Utility.ByteDbnull(row.Cells["Header_Title_Cell_Count"].Value, 1).ToString()));
                    lstData.Add(new SqlData("Sheet_Name", SqlUtility.SetValueText(SqlUtility.GetVerifiedDbColNameString(Utility.sDbnull(row.Cells["Sheet_Name"].Value)).Trim())));
                    lstData.Add(new SqlData("Sheet_Sequence", Utility.Int16Dbnull(row.Cells["Sheet_Sequence"].Value, 0).ToString()));
                    if (string.IsNullOrEmpty(Utility.sDbnull(row.Cells["MultiReport_Sheet_Config_ID"].Value)))
                    {
                        row.BeginEdit();
                        row.Cells["MultiReport_Sheet_Config_ID"].Value = Utility.Int32Dbnull(SqlUtility.InsertScopeIdentity("D_Multi_Report_Sheet_Config", lstData));
                        row.EndEdit();
                    }
                    else SqlUtility.Update("D_Multi_Report_Sheet_Config", lstData, "MultiReport_Sheet_Config_ID = " + Utility.sDbnull(row.Cells["MultiReport_Sheet_Config_ID"].Value));
                    row.BeginEdit();
                    row.Cells["IsUpdated"].Value = 0;
                    row.EndEdit();
                }
            }
        }

        private void nmrReportID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    reportID = Utility.Int32Dbnull(nmrReportID.Value);
                    DataTable dtTmp = SqlUtility.GetDataSet(string.Format("select * from D_Multi_Report where MultiReport_ID = {0}", reportID)).Tables[0];
                    if (dtTmp.Rows.Count <= 0)
                    {
                        Utility.ShowMsg("ID không tồn tại");
                        return;
                    }
                    txtReport_Name.Text = Utility.sDbnull(dtTmp.Rows[0]["MultiReport_Name"]);
                    txtQuery.Text = Utility.sDbnull(dtTmp.Rows[0]["MultiReport_Query_String"]);
                    cboQueryType.SelectedValue = Utility.sDbnull(dtTmp.Rows[0]["MultiReport_Query_Type"]);
                    txtSequence.Value = Utility.Int32Dbnull(dtTmp.Rows[0]["MultiReport_Sequence"]);
                    txtReportType.Text = Utility.sDbnull(dtTmp.Rows[0]["MultiReport_Type"]);
                    ckbIsAdmin.Checked = Utility.Int32Dbnull(dtTmp.Rows[0]["MultiReport_IsAdmin"]) == 1 ? true : false;
                    GetExcelConfig();

                    ActiveControl = txtQuery;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void GetExcelConfig()
        {
            dtConfig = SqlUtility.GetDataSet(string.Format("select *, 0 as [IsUpdated] from D_Multi_Report_Config where MultiReport_ID = {0} order by MultiReport_Config_Table_Index,MultiReport_Config_ExcelColPosition", reportID)).Tables[0];
            dtSheetConfig = SqlUtility.GetDataSet(string.Format("select *, 0 as [IsUpdated] from D_Multi_Report_Sheet_Config where MultiReport_ID = {0} order by Sheet_Sequence", reportID)).Tables[0];
            grdConfig.DataSource = dtConfig;
            grdSheetConfig.DataSource = dtSheetConfig;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                List<SqlData> lstData = new List<SqlData>();
                lstData.Add(new SqlData("MultiReport_Name", "N'BÁO CÁO MỚI'"));
                lstData.Add(new SqlData("MultiReport_Query_Type", "N'GETDATATABLE'"));
                lstData.Add(new SqlData("MultiReport_Query_String", "N'SELECT GETDATE()'"));
                lstData.Add(new SqlData("MultiReport_Sequence", "0"));
                lstData.Add(new SqlData("MultiReport_IsAdmin", "0"));
                reportID = Utility.Int32Dbnull(SqlUtility.InsertScopeIdentity("D_Multi_Report", lstData));

                txtQuery.Text = "SELECT GETDATE()";
                txtReport_Name.Text = "BÁO CÁO MỚI";
                cboQueryType.SelectedValue = "GETDATATABLE";
                txtSequence.Value = 0;
                txtReportType.Clear();
                nmrReportID.Value = reportID;
                ckbIsAdmin.Checked = false;
                GetExcelConfig();
                ActiveControl = txtQuery;
                Utility.ShowMsg("Tạo mới thành công !");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private DataTable dtConfig, dtSheetConfig;
        private void tsmAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportID <= 0)
                {
                    Utility.ShowMsg("ID báo cáo chưa được chọn");
                    return;
                }

                switch (uiTab2.SelectedTab.Key)
                {
                    case "tabColumn":
                        dtConfig.Rows.InsertAt(dtConfig.NewRow(), grdConfig.CurrentRow.RowIndex);
                        dtConfig.AcceptChanges();
                        grdConfig.MoveToNewRecord();
                        break;
                    case "tabSheet":
                        dtSheetConfig.Rows.InsertAt(dtSheetConfig.NewRow(), grdSheetConfig.CurrentRow.RowIndex);
                        dtSheetConfig.AcceptChanges();
                        grdSheetConfig.MoveToNewRecord();
                        break;
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void grdConfig_CellEdited(object sender, ColumnActionEventArgs e)
        {
            grdConfig.SetValue("IsUpdated",1);
        }

        private void tslDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportID <= 0)
                {
                    Utility.ShowMsg("ID báo cáo chưa được chọn");
                    return;
                }
                // Try to cast the sender to a ToolStripItem
                ToolStripItem menuItem = sender as ToolStripItem;
                if (menuItem != null)
                {
                    // Retrieve the ContextMenuStrip that owns this ToolStripItem
                    ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                    if (owner != null)
                    {
                        // Get the control that is displaying this context menu
                        Control sourceControl = owner.SourceControl;
                        switch (sourceControl.Name)
                        {
                            case "grdConfig":
                                if (MessageBox.Show(string.Format("Thực hiện xóa cấu hình với ID = {0}", grdConfig.GetValue("MultiReport_Config_ID")), "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                {
                                    SqlUtility.Delete("D_Multi_Report_Config", "MultiReport_Config_ID = " + grdConfig.GetValue("MultiReport_Config_ID"));
                                    grdConfig.CurrentRow.Delete();
                                    grdConfig.UpdateData();
                                    dtConfig.AcceptChanges();
                                }
                                break;
                            case "grdSheetConfig":
                                if (MessageBox.Show(string.Format("Thực hiện xóa cấu hình với ID = {0}", grdSheetConfig.GetValue("MultiReport_Sheet_Config_ID")), "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                {
                                    SqlUtility.Delete("D_Multi_Report_Sheet_Config", "MultiReport_Sheet_Config_ID = " + grdSheetConfig.GetValue("MultiReport_Sheet_Config_ID"));
                                    grdSheetConfig.CurrentRow.Delete();
                                    grdSheetConfig.UpdateData();
                                    dtSheetConfig.AcceptChanges();
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void gridEX1_CellEdited(object sender, ColumnActionEventArgs e)
        {
            grdSheetConfig.SetValue("IsUpdated", 1);
        }
    }
}
