using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using Janus.Windows.UI.Tab;
using VNS.HandyTools;
using VNS.Libs;
using SubSonic;
using VNS.HIS.DAL;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.UI.Cauhinh;
namespace VNS.MultiReport
{
    public partial class frm_BaocaoMulti : Form
    {
        private string sqlConn;
        private bool bIsAdmin;
        string Args = "ALL";
        action m_enAct = action.FirstOrFinished;
        private DataTable dtConfig, dtSheetConfig;
        SysMultiReport objBaocao = null;
        public frm_BaocaoMulti(string Args)
        {
            InitializeComponent();
            this.Args = Args;
            this.Shown += frm_BaocaoMulti_Shown;
            grdReportList.MouseDoubleClick += grdReportList_MouseDoubleClick;
            cmdThemmoi.Click += cmdThemmoi_Click;
            cmdXoa.Click += cmdXoa_Click;
            cmdCapnhat.Click += cmdCapnhat_Click;
            cmdSave.Click += cmdSave_Click;
            txtReportType._OnShowData += txtReportType__OnShowData;
            this.cmdExportExcel.Click += new System.EventHandler(this.cmdExportExcel_Click);
            this.cmdExportExcelTemplate.Click += new System.EventHandler(this.cmdExportExcelTemplate_Click);
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            this.cmdExecute.Click += new System.EventHandler(this.btnSearch_Click);
            this.grdReportList.SelectionChanged += new System.EventHandler(this.grdReportList_SelectionChanged);
            this.csmReloadReportList.Click += new System.EventHandler(this.csmReloadReportList_Click);
            this.Load += new System.EventHandler(this.frmMultiReport_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMultiReport_KeyDown);
            grdReportList.UpdatingCell += grdReportList_UpdatingCell;
        }

        void grdReportList_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                GridEXRow currentRow = grdReportList.CurrentRow;
                if (e.Column.Key == "MultiReport_Name")
                {
                    if(objBaocao==null)
                        objBaocao = SysMultiReport.FetchByID(Utility.Int32Dbnull(currentRow.Cells[SysMultiReport.Columns.MultiReportId].Value, -1));
                    if (objBaocao == null)
                    {
                        Utility.ShowMsg("Không xác định được báo cáo đang sửa(Có thể bị người khác xóa trong lúc bạn đang thực hiện sửa). Đề nghị hủy thao tác và nhấn F5 để làm mới lại danh sách báo cáo", "Thông báo", MessageBoxIcon.Information);
                        return;
                    }
                    string _name=Utility.DoTrim(Utility.sDbnull(e.Value, ""));
                    if (_name.Length <= 0)
                    {
                        Utility.ShowMsg("Bạn phải nhập tên báo cáo", "Thông báo", MessageBoxIcon.Information);
                        e.Cancel = true;
                        return;
                    }
                    QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandSql = "select 1 from sys_multi_report where MultiReport_Name=N'" + _name + "' and MultiReport_ID<>" + objBaocao.MultiReportId.ToString();
                    if (DataService.GetDataSet(cmd).Tables[0].Rows.Count > 0)
                    {
                        Utility.ShowMsg("Đã tồn tại báo cáo khác có tên " + Utility.DoTrim(txtReport_Name.Text) + " Đề nghị nhập tên khác", "Thông báo", MessageBoxIcon.Information);
                        e.Cancel = true;
                        return;
                    }
                    objBaocao.MultiReportName = _name;
                    objBaocao.IsNew = false;
                    objBaocao.MarkOld();
                    objBaocao.Save();
                }
            }
            catch (Exception)
            {


            }
             
        }

        void txtReportType__OnShowData()
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(txtReportType.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtReportType.myCode;
                txtReportType.Init();
                txtReportType.SetCode(oldCode);
                txtReportType.Focus();
            }
        }
        void doSave()
        {
            try
            {
                PerformAction();
                UpdateExcelConfig();
                UpdateSheetConfig();
                
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        void cmdSave_Click(object sender, EventArgs e)
        {
            doSave();
        }

        void cmdCapnhat_Click(object sender, EventArgs e)
        {
            EnterEditMode(action.Update);
        }

        void cmdXoa_Click(object sender, EventArgs e)
        {
            string ten_baocao=Utility.getValueOfGridCell(grdReportList, "MultiReport_Name").ToString();
            if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa báo cáo {0} không?", ten_baocao), "", true)) return;
            int id_baocao = Utility.Int32Dbnull(Utility.getValueOfGridCell(grdReportList, "MultiReport_ID"), -1);
            QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandSql = string.Format("select * from qhe_nhanvien_baocaomulti where id_baocao={0}", id_baocao.ToString());
            if (DataService.GetDataSet(cmd).Tables[0].Rows.Count > 0)
            {
                Utility.ShowMsg(string.Format("Báo cáo {0} đã phân quyền cho người dùng nên bạn không thể xóa", ten_baocao));
                return;
            };
            int _count=new Delete().From(SysMultiReport.Schema).Where(SysMultiReport.Columns.MultiReportId).IsEqualTo(id_baocao).Execute();
            if (_count > 0) Utility.RemoveRowfromDataTable(string.Format("{0}={1}", SysMultiReport.Columns.MultiReportId, id_baocao), m_dtList);
            ModifyButtons();
        }
        void InsertOrUpdateDataSource()
        {
            switch (m_enAct)
            {
                case action.Insert:
                    DataRow dr = m_dtList.NewRow();
                    dr[SysMultiReport.Columns.MultiReportName] = objBaocao.MultiReportName;
                    dr[SysMultiReport.Columns.MultiReportQueryString] = objBaocao.MultiReportQueryString;
                    dr[SysMultiReport.Columns.MultiReportQueryType] = objBaocao.MultiReportQueryType;
                    dr[SysMultiReport.Columns.MultiReportSequence] = objBaocao.MultiReportSequence;
                    dr[SysMultiReport.Columns.MultiReportType] = objBaocao.MultiReportType;
                    dr[SysMultiReport.Columns.TrangThai] = objBaocao.TrangThai;
                    dr[SysMultiReport.Columns.MultiReportId] = objBaocao.MultiReportId;
                    dr["ten_loai"] = txtReportType.Text;
                    m_dtList.Rows.Add(dr);
                    m_dtList.AcceptChanges();
                    Utility.GotoNewRowJanus(grdReportList, SysMultiReport.Columns.MultiReportId, objBaocao.MultiReportId.ToString());
                    break;
                case action.Update:
                    DataRow[] arrDr = m_dtList.Select(string.Format("{0}={1}", SysMultiReport.Columns.MultiReportId, objBaocao.MultiReportId.ToString()));
                    if (arrDr.Length > 0)
                    {
                        arrDr[0][SysMultiReport.Columns.MultiReportName] = objBaocao.MultiReportName;
                        arrDr[0][SysMultiReport.Columns.MultiReportQueryString] = objBaocao.MultiReportQueryString;
                        arrDr[0][SysMultiReport.Columns.MultiReportQueryType] = objBaocao.MultiReportQueryType;
                        arrDr[0][SysMultiReport.Columns.MultiReportSequence] = objBaocao.MultiReportSequence;
                        arrDr[0][SysMultiReport.Columns.MultiReportType] = objBaocao.MultiReportType;
                        arrDr[0][SysMultiReport.Columns.TrangThai] = objBaocao.TrangThai;
                        arrDr[0]["ten_loai"] = txtReportType.Text;
                    }
                    m_dtList.AcceptChanges();
                    Utility.GotoNewRowJanus(grdReportList, SysMultiReport.Columns.MultiReportId, objBaocao.MultiReportId.ToString());
                    break;
                default:
                    break;
            }
        }
        void cmdThemmoi_Click(object sender, EventArgs e)
        {
            EnterEditMode(action.Insert);
        }

        void grdReportList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EnterEditMode(action.Update);
        }
        void EnterEditMode(action act)
        {
            //Update thì cần có dữ liệu trên lưới
            if (act==action.Update && !Utility.isValidGrid(grdReportList)) return;
            m_enAct = act;
            tabthuchien.Enabled = false;
            uiTabAct.SelectedTab = tabCapnhat;
            switch (m_enAct)
            {
                case action.Insert:
                    Prepare4Insert();
                    break;
                case action.Update:
                    objBaocao = SysMultiReport.FetchByID(Utility.Int32Dbnull(Utility.getValueOfGridCell(grdReportList,SysMultiReport.Columns.MultiReportId),-1));
                    Prepare4Update();
                    break;
                default:
                    break;
            }
        }
        void frm_BaocaoMulti_Shown(object sender, EventArgs e)
        {
            if (Args.ToUpper() == "USER" || !globalVariables.IsAdmin)
            {
                cmdThemmoi.Visible = cmdXoa.Visible = cmdCapnhat.Visible = tabCapnhat.TabVisible = false;
                splitContainer2.Panel2Collapsed = true;
            }
            tabthuchien.Enabled = Utility.isValidGrid(grdReportList) && m_enAct == action.FirstOrFinished;
            
        }
        #region "Thực thi báo cáo"
        private void frmMultiReport_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F3 && m_enAct==action.FirstOrFinished)
                {
                    DoQuery();
                    return;
                }
                else if (e.KeyCode == Keys.F5 && m_enAct == action.FirstOrFinished)
                {
                    GetData();
                    return;
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    if (m_enAct != action.FirstOrFinished)
                    {
                        if (Utility.AcceptQuestion("Bạn có chắc chắn muốn hủy thao tác đang thực hiện", "Cảnh báo", true))
                        {
                            FinishOrCancelAct();
                        }
                    }
                    else
                        if (Utility.AcceptQuestion("Bạn có chắc chắn muốn thoát khỏi chức năng", "Cảnh báo", true))
                            this.Close();
                    return;
                }
                else if (e.Control && e.KeyCode == Keys.S && m_enAct == action.FirstOrFinished)
                {
                    doSave();
                    return;
                }
                else if (e.Control && e.KeyCode == Keys.E && m_enAct == action.FirstOrFinished)
                {
                    cmdCapnhat.PerformClick();
                    return;
                }
                else if (e.Control && e.KeyCode == Keys.N && m_enAct == action.FirstOrFinished)
                {
                    cmdThemmoi.PerformClick();
                    return;
                }
                else if (e.Control && e.KeyCode == Keys.D && m_enAct == action.FirstOrFinished)
                {
                    cmdXoa.PerformClick();
                    return;
                }
                else if (e.Control && e.KeyCode == Keys.X && m_enAct == action.FirstOrFinished)
                {
                    cmdExportExcel.PerformClick();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmMultiReport_Load(object sender, EventArgs e)
        {
            try
            {
                bIsAdmin = Utility.sDbnull(this.Tag).ToUpper().Contains("ADMIN");
                grdReportList.RootTable.Columns["MultiReport_IsAdmin"].Visible = false ;
                splitContainer2.Panel2Collapsed = !bIsAdmin;
                dtpToDate.Value = dtpToDate.Value.AddDays(1).AddMilliseconds(-1);
                txtReportType.Init();
                GetData();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                
            }
            
        }
        DataTable m_dtList = new DataTable();
        private void GetData()
        {
            QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandSql = string.Format("select *,(select TOP 1 TEN from dmuc_chung where LOAI='LOAI_BAOCAOMULTI' AND MA=p.MultiReport_Type) as ten_loai from sys_multi_report p where trang_thai=1 and exists(select 1 from qhe_nhanvien_baocaomulti where id_baocao=p.MultiReport_ID and id_nhanvien={0} )order by [MultiReport_Sequence]",globalVariables.gv_intIDNhanvien.ToString());
            m_dtList = DataService.GetDataSet(cmd).Tables[0];
            grdReportList.DataSource = m_dtList;
            grdReportList.MoveFirst();
            ModifyButtons();
        }

        List<string> arrTenThamSo = new List<string>();
        void DoQuery()
        {
            try
            {
                if (grdReportList.CurrentRow == null)
                {
                    txtQuery.Text = string.Empty;
                    return;
                }
                if (grdReportList.CurrentRow.RowType != RowType.Record)
                {
                    txtQuery.Text = string.Empty;
                    return;
                }
                uiTab2.TabPages.Clear();
                string sQuery = Utility.sDbnull(grdReportList.GetValue("MultiReport_Query_String"));
                sQuery = sQuery.Replace("{TUNGAY}", dtpFromDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                sQuery = sQuery.Replace("{DENNGAY}", dtpToDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                string[] arrThamSo = txtParameter.Text.Split(',');
                for (int i = 0; i < arrTenThamSo.Count(); i++)
                {
                    //So sánh số lượng tham số trong query và số lượng tham số được nhập vào
                    if (i < arrThamSo.Count()) sQuery = sQuery.Replace("{" + arrTenThamSo[i] + "}", arrThamSo[i]);
                    else
                    {
                        int idx = sQuery.IndexOf("{" + arrTenThamSo[i] + "}");
                        if (idx <= 0) sQuery = sQuery.Replace("{" + arrTenThamSo[i] + "}", "-1");
                        else if (sQuery[idx - 1].ToString() != "'") sQuery = sQuery.Replace("{" + arrTenThamSo[i] + "}", "-1");
                        else sQuery = sQuery.Replace("{" + arrTenThamSo[i] + "}", "");
                    }
                }
                string sQueryType = Utility.sDbnull(grdReportList.GetValue("MultiReport_Query_Type"));
                txtQuery.Text = sQuery;
                if (!isValidQuery(sQuery))
                {
                    return;
                }
                GenerateResult(sQuery, sQueryType);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        List<string> lstAct = new List<string>() {"DELETE","UPDATE","INSERT","TRUNCATE" };
        bool isValidQuery(string query)
        {
            bool isValid = !lstAct.Any(s=>query.ToUpper().Contains(s));
            if (!isValid)
            {
                Utility.ShowMsg(string.Format("Hệ thống phát hiện trong câu lệnh có một số từ khóa {0} bị cấm nên sẽ không cho phép thực thi. Vui lòng kiểm tra lại", string.Join(",", lstAct.ToArray<string>())));
            }
            return isValid;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DoQuery();
            
        }

        private void GenerateResult(string sQuery, string sQueryType)
        {
            QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandSql = sQuery;
            if (sQueryType == "EXECUTE")
            {
                DataService.ExecuteQuery(cmd);
                Utility.ShowMsg("Thực hiện thành công !");
                return;
            }

            DataSet ds = DataService.GetDataSet(cmd);
            int count = 1;
            foreach (DataTable dtResult in ds.Tables)
            {
                UITabPage tabPage = new UITabPage();
                GridEX gridEx = new GridEX();
                gridEx.RecordNavigator = true;
                gridEx.FilterMode = FilterMode.Automatic;
                gridEx.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                gridEx.FilterRowFormatStyle.BackColor = Color.FromArgb(255, 255, 192);
                gridEx.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                gridEx.GroupByBoxVisible = false;
                gridEx.AllowEdit = InheritableBoolean.False;
                gridEx.RootTable = new GridEXTable();
                gridEx.Dock =  DockStyle.Fill;
                gridEx.RowHeaders = InheritableBoolean.True;
                tabPage.Controls.Add(gridEx);
                tabPage.Text = string.Format("Bảng {0}", count);
                count ++;
                uiTab2.TabPages.Add(tabPage);

                foreach (DataColumn dc in dtResult.Columns)
                {
                    GridEXColumn col = new GridEXColumn();
                    col.Key = Utility.sDbnull(dc.ColumnName);
                    col.DataMember = Utility.sDbnull(dc.ColumnName);
                    string[] arrStrings = dc.ColumnName.Split('#');
                    col.Caption = Utility.sDbnull(arrStrings[0]);
                    if (arrStrings.Count() > 1) col.Width = Utility.Int32Dbnull(arrStrings[1], 100);
                    if (arrStrings.Count() > 2) col.Visible = Utility.Int16Dbnull(arrStrings[2], -1) != 1;
                    col.FilterRowComparison = ConditionOperator.Contains;

                    gridEx.RootTable.Columns.Add(col);
                    if (col.Caption == "colColour")
                    {
                        GridEXFormatCondition condition = new GridEXFormatCondition();
                        condition.Column = col;
                        condition.Value1 = 1;
                        condition.FormatStyle.BackColor = Color.FromArgb(255, 255, 192);
                        gridEx.RootTable.FormatConditions.Add(condition);
                    }
                }
                gridEx.DataSource = dtResult;
            }
        }

        private void cmdExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (uiTab2.SelectedTab == null) return;
                GridEX grdResult = (GridEX)uiTab2.SelectedTab.Controls[0];
                if (grdResult.RowCount <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu !");
                    return;
                }

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                    {
                        gridEXExporter1.GridEX = grdResult;
                        gridEXExporter1.Export(s);    

                    }
                    Utility.ShowMsg("Xuất XML thành công !");
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void csmReloadReportList_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void grdReportList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_enAct != action.FirstOrFinished || !Utility.isValidGrid(grdReportList)) return;
                objBaocao = SysMultiReport.FetchByID(Utility.Int32Dbnull(Utility.getValueOfGridCell(grdReportList,SysMultiReport.Columns.MultiReportId)));
                FillData4Update();
                //if (grdReportList.CurrentRow == null)
                //{
                //    lblThamSo.Text = string.Empty; return;
                //}
                //if (grdReportList.CurrentRow.RowType != RowType.Record)
                //{
                //    lblThamSo.Text = string.Empty; return;
                //}
                //switch (Utility.sDbnull(grdReportList.GetValue("MultiReport_Query_Type")))
                //{
                //    case "GETDATATABLE" : cmdExecute.Text = "Tìm kiếm (F3)"; break;
                //    case "EXECUTE": cmdExecute.Text = "Thực hiện (F3)"; break;
                //}
                //arrTenThamSo.Clear();
                //string s = Utility.sDbnull(grdReportList.GetValue("MultiReport_Query_String")).Trim();
                //txtQuery.Text = s;
                //s = s.Replace("{TUNGAY}", "").Replace("{DENNGAY}", "");
                //int idx = s.IndexOf('{');
                //while (idx >=0)
                //{
                //    string sTenThamSo = s.Substring(idx + 1, s.IndexOf('}') - idx - 1);
                //    if (!arrTenThamSo.Contains(sTenThamSo)) arrTenThamSo.Add(sTenThamSo);
                //    s = s.Substring(s.IndexOf('}') + 1);
                //    idx = s.IndexOf('{');
                //}
                //lblThamSo.Text = "* " + arrTenThamSo.Count + " tham số: " + string.Join(",", arrTenThamSo.ToArray());
                //if (arrTenThamSo.Count <= 0) lblThamSo.Text = lblThamSo.Text.Replace(":", "");

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void cmdExportExcelTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdReportList.CurrentRow == null) {return;}
                if (grdReportList.CurrentRow.RowType != RowType.Record) {return;}
                if (uiTab2.TabPages.Count <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu");
                    return;
                }

                string sQuery = string.Format("select * from sys_multi_report_sheet_config where MultiReport_ID = {0} order by Sheet_Sequence" +
                                                                       "\r\n" +
                                                                       "select * from sys_multi_report_config where [MultiReport_ID] = {0} order by MultiReport_Config_ExcelColPosition",grdReportList.GetValue("MultiReport_ID"));
                QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandSql = sQuery;
                DataSet dsConfig = DataService.GetDataSet(cmd);
                if (dsConfig.Tables[0].Rows.Count <= 0 | dsConfig.Tables[1].Rows.Count <= 0)
                {
                    Utility.ShowMsg("Cấu hình file Excel không đủ thông tin");
                    return;
                }
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "xls files (*.xls)|*.xls|All files (*.*)|*.*";
                if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
                PerformSaveExcel(dsConfig, saveFileDialog1.FileName);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        Microsoft.Office.Interop.Excel.Application oXL;
        Microsoft.Office.Interop.Excel._Workbook oWB;
        Microsoft.Office.Interop.Excel._Worksheet oSheet;
        Microsoft.Office.Interop.Excel.Range oRng;
        object misvalue = System.Reflection.Missing.Value;
        private void PerformSaveExcel(DataSet dsConfig, string fileName)
        {
            try
            {
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;
                oXL.DisplayAlerts = false;
                oWB = oXL.Workbooks.Add("");

                for (int idx = dsConfig.Tables[0].Rows.Count-1; idx >= 0; idx--)
                {
                    int tableIdx = Utility.Int16Dbnull(dsConfig.Tables[0].Rows[idx]["MultiReport_Config_Table_Index"]);
                    DataRow[] dtExcelConfigForTable = dsConfig.Tables[1].Select("MultiReport_Config_Table_Index = " + dsConfig.Tables[0].Rows[idx]["MultiReport_Config_Table_Index"]);
                    if (dtExcelConfigForTable.Any())
                    {
                        GridEX gridEx = (GridEX)uiTab2.TabPages[tableIdx - 1].Controls[0];
                        GridEXRow[] arrRow = gridEx.GetRows();
                        oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.Sheets.Add();
                        oSheet.Name = Utility.sDbnull(dsConfig.Tables[0].Rows[idx]["Sheet_Name"], "Bảng " + (idx + 1));
                        for (int i = 0; i < dtExcelConfigForTable.Count(); i++)
                        {
                            oRng = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[1, i + 1];
                            oRng.Value = Utility.sDbnull(dtExcelConfigForTable[i]["MultiReport_Config_ExcelColName"]);
                            oRng.ColumnWidth = Utility.Int32Dbnull(dtExcelConfigForTable[i]["MultiReport_Config_ExcelColWidth"], 15);

                            string colName = Utility.sDbnull(dtExcelConfigForTable[i]["MultiReport_Config_SqlColName"]);
                            if (gridEx.RootTable.Columns.Contains(colName))
                            {
                                string colNumberFormat = Utility.sDbnull(dtExcelConfigForTable[i]["MultiReport_Config_ExcelColNumberFormat"]).Trim();
                                string colAggregateFunction = Utility.sDbnull(dtExcelConfigForTable[i]["MultiReport_Config_ExcelColAggregateFunction"]).Trim();
                                if (!string.IsNullOrEmpty(colNumberFormat)) oRng.EntireColumn.NumberFormat = colNumberFormat;
                                for (int j = 0; j < arrRow.Count(); j++)
                                {
                                    oRng = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[j + 2, i + 1];
                                    oRng.Value = Utility.sDbnull(arrRow[j].Cells[colName].Value);
                                }
                                if (!string.IsNullOrEmpty(colAggregateFunction))
                                {
                                    oRng = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[arrRow.Count() + 2, i + 1];
                                    string columnLetter = oRng.get_AddressLocal(true, false, Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1, misvalue, misvalue).Split('$')[0];
                                    if (!string.IsNullOrEmpty(columnLetter))
                                        oRng.Formula = string.Format("={0}({1}2:{1}{2})", colAggregateFunction, columnLetter, arrRow.Count()+1);
                                }
                            }
                        }
                        oSheet.Application.ActiveWindow.SplitRow = 1;
                        oSheet.Application.ActiveWindow.FreezePanes = true;
                        oRng = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[1, 1];
                        oRng.EntireRow.Font.Bold = true;
                        int vHeader_Blank_Row_Count = Utility.Int16Dbnull(dsConfig.Tables[0].Rows[idx]["Header_Blank_Row_Count"]);
                        if (vHeader_Blank_Row_Count > 0)
                        {
                            for (int i = 0; i < vHeader_Blank_Row_Count; i++)
                            {
                                oRng.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                            }
                            oRng = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[1, 1];
                            oRng.Value = Utility.sDbnull(dsConfig.Tables[0].Rows[idx]["Header_Title"]);
                            oRng.Font.Size = 16;
                            oRng.Font.Bold = true;
                            int vHeader_Title_Cell_Count = Utility.Int16Dbnull(dsConfig.Tables[0].Rows[idx]["Header_Title_Cell_Count"]);
                            if (vHeader_Title_Cell_Count > 1)
                            {
                                oSheet.Range[oSheet.Cells[1, 1], oSheet.Cells[1, vHeader_Title_Cell_Count]].Merge();
                                oRng.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            }
                        }
                    }
                }
                string[] arrSheetName = (from dr in dsConfig.Tables[0].AsEnumerable() select Utility.sDbnull(dr.Field<object>("Sheet_Name")))
                        .ToArray();
                foreach (Microsoft.Office.Interop.Excel._Worksheet sheet in oWB.Sheets)
                {
                    if (!arrSheetName.Contains(sheet.Name)) sheet.Delete();
                }
                //oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.Sheets["Sheet1"];if (oSheet !=null) oSheet.Delete();
                oXL.UserControl = false;
                oWB.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8, Type.Missing, Type.Missing,
                            false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                Utility.ShowMsg("Xuất Excel thành công !");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(oRng) != 0) { }
                oRng = null;
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(oSheet) != 0) { }
                oSheet = null;
                if (oWB != null) oWB.Close(Missing.Value, Missing.Value, Missing.Value);
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(oWB) != 0) { }
                oWB = null;
                if (oXL != null) oXL.Quit();
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(oXL) != 0) { }
                oXL = null;
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            //System.Diagnostics.Process.Start(fileName);
        }
        void ModifyButtons()
        {
            cmdXoa.Enabled = cmdCapnhat.Enabled =cmdAdmin.Enabled= Utility.isValidGrid(grdReportList) && m_enAct == action.FirstOrFinished;
            cmdThemmoi.Enabled = m_enAct == action.FirstOrFinished;
            cmdExportExcelTemplate.Enabled = cmdExit.Enabled = cmdExportExcel.Enabled = m_enAct == action.FirstOrFinished;
            cmdCancel.Enabled = m_enAct != action.FirstOrFinished;
            tabthuchien.Enabled = Utility.isValidGrid(grdReportList) && m_enAct == action.FirstOrFinished;
        }
        #endregion
        #region "Cấu hình báo cáo: Thêm mới, Cập nhật"
        void PerformAction()
        {
            switch (m_enAct)
            {
                case action.Insert:
                    PerformInsertAction();
                    break;
                case action.Update:
                    PerformUpdateAction();
                    break;
                default:
                    break;
            }
            ModifyButtons();
        }
        /// <summary>
        /// Hàm thực hiện thêm mới
        /// </summary>
        void PerformInsertAction()
        {
            try
            {
                
                if (Utility.DoTrim(txtReport_Name.Text).Length <= 0)
                {
                    Utility.ShowMsg("Bạn phải nhập tên báo cáo", "Thông báo",MessageBoxIcon.Information);
                    txtReport_Name.Focus();
                    return;
                }
                QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandSql = "select 1 from sys_multi_report where MultiReport_Name=N'" + Utility.DoTrim(txtReport_Name.Text)+"'";
                if (DataService.GetDataSet(cmd).Tables[0].Rows.Count > 0)
                {
                    Utility.ShowMsg("Đã tồn tại báo cáo có tên " + Utility.DoTrim(txtReport_Name.Text)+" Đề nghị nhập tên khác", "Thông báo", MessageBoxIcon.Information);
                    txtReport_Name.Focus();
                    return;
                }
                if (Utility.DoTrim(txtReportType.MyCode)=="-1")
                {
                    Utility.ShowMsg("Bạn phải nhập loại báo cáo", "Thông báo", MessageBoxIcon.Information);
                    txtReportType.Focus();
                    return;
                }
                if (cboQueryType.SelectedIndex<0)
                {
                    Utility.ShowMsg("Bạn phải chọn loại thực thi", "Thông báo", MessageBoxIcon.Information);
                    cboQueryType.Focus();
                    return;
                }
               objBaocao = new SysMultiReport();
               objBaocao.MultiReportName = Utility.DoTrim(txtReport_Name.Text);
               objBaocao.MultiReportQueryString = Utility.DoTrim(txtSQL.Text);
               objBaocao.MultiReportQueryType = Utility.sDbnull(cboQueryType.SelectedValue, "ERROR");
               objBaocao.MultiReportSequence = (short)txtSequence.Value;
               objBaocao.MultiReportType = Utility.DoTrim(txtReportType.myCode);
               objBaocao.MultiReportIsAdmin = 0;
               objBaocao.TrangThai = chkTrangthai.Checked;
               objBaocao.IsNew = true;
               objBaocao.Save();
               InsertOrUpdateDataSource();
               nmrReportID.Value = objBaocao.MultiReportId;
                if (chkContinue.Checked)
                {
                    m_enAct = action.Insert;
                }
                else
                    FinishOrCancelAct();
                Utility.ShowMsg("Thêm mới báo cáo thành công !");
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }
        /// <summary>
        /// Hàm khi nhấn nút hủy thêm mới, cập nhật
        /// </summary>
        void FinishOrCancelAct()
        {
            m_enAct = action.FirstOrFinished;
            //uiTabAct.SelectedTab = tabthuchien;
            ModifyButtons();
        }
        /// <summary>
        /// Hàm cập nhật
        /// </summary>
        void PerformUpdateAction()
        {
            try
            {
                objBaocao = SysMultiReport.FetchByID(Utility.Int32Dbnull(nmrReportID.Value, -1));
                if (objBaocao == null)
                {
                    Utility.ShowMsg("Không xác định được báo cáo đang sửa(Có thể bị người khác xóa trong lúc bạn đang thực hiện sửa). Đề nghị hủy thao tác và nhấn F5 để làm mới lại danh sách báo cáo", "Thông báo", MessageBoxIcon.Information);
                    return;
                }
                if (Utility.DoTrim(txtReport_Name.Text).Length <= 0)
                {
                    Utility.ShowMsg("Bạn phải nhập tên báo cáo", "Thông báo", MessageBoxIcon.Information);
                    txtReport_Name.Focus();
                    return;
                }
                QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandSql = "select 1 from sys_multi_report where MultiReport_Name=N'" + Utility.DoTrim(txtReport_Name.Text) + "' and MultiReport_ID<>" + objBaocao.MultiReportId.ToString();
                if (DataService.GetDataSet(cmd).Tables[0].Rows.Count > 0)
                {
                    Utility.ShowMsg("Đã tồn tại báo cáo khác có tên " + Utility.DoTrim(txtReport_Name.Text) + " Đề nghị nhập tên khác", "Thông báo", MessageBoxIcon.Information);
                    txtReport_Name.Focus();
                    return;
                }
                if (Utility.DoTrim(txtReportType.MyCode) == "-1")
                {
                    Utility.ShowMsg("Bạn phải nhập loại báo cáo", "Thông báo", MessageBoxIcon.Information);
                    txtReportType.Focus();
                    return;
                }
                if (cboQueryType.SelectedIndex < 0)
                {
                    Utility.ShowMsg("Bạn phải chọn loại thực thi", "Thông báo", MessageBoxIcon.Information);
                    cboQueryType.Focus();
                    return;
                }
                
                objBaocao.MultiReportName = Utility.DoTrim(txtReport_Name.Text);
                objBaocao.MultiReportQueryString = Utility.DoTrim(txtSQL.Text);
                objBaocao.MultiReportQueryType = Utility.sDbnull(cboQueryType.SelectedValue, "ERROR");
                objBaocao.MultiReportSequence = (short)txtSequence.Value;
                objBaocao.MultiReportType = Utility.DoTrim(txtReportType.myCode);
                objBaocao.MultiReportIsAdmin = 0;
                objBaocao.IsNew = false;
                objBaocao.TrangThai = chkTrangthai.Checked;
                objBaocao.MarkOld();
                objBaocao.Save();
                InsertOrUpdateDataSource();
                FinishOrCancelAct();
                Utility.ShowMsg("Cập nhật nội dung báo cáo thành công !");
            }
            catch (Exception)
            {

                throw;
            }
        }
        

        private void UpdateExcelConfig()
        {
            foreach (GridEXRow row in grdConfig.GetRows())
            {
                if (Utility.Int16Dbnull(row.Cells["IsUpdated"].Value) == 1)
                {
                    List<SqlData> lstData = new List<SqlData>();
                    lstData.Add(new SqlData("MultiReport_ID", Utility.sDbnull(nmrReportID.Value)));
                    lstData.Add(new SqlData("MultiReport_Config_Table_Index", Utility.sDbnull(Utility.Int32Dbnull(row.Cells["MultiReport_Config_Table_Index"].Value)).Trim()));
                    lstData.Add(new SqlData("MultiReport_Config_SqlColName", SqlUtility.SetValueText(SqlUtility.GetVerifiedDbColNameString(Utility.sDbnull(row.Cells["MultiReport_Config_SqlColName"].Value)).Trim())));
                    lstData.Add(new SqlData("MultiReport_Config_ExcelColName", SqlUtility.SetValueText(SqlUtility.GetVerifiedDbColNameString(Utility.sDbnull(row.Cells["MultiReport_Config_ExcelColName"].Value)).Trim())));
                    lstData.Add(new SqlData("MultiReport_Config_ExcelColWidth", Utility.Int32Dbnull(row.Cells["MultiReport_Config_ExcelColWidth"].Value, 15).ToString()));
                    lstData.Add(new SqlData("MultiReport_Config_ExcelColPosition", Utility.sDbnull(Utility.Int32Dbnull(Utility.sDbnull(row.Cells["MultiReport_Config_ExcelColPosition"].Value).Trim(), 0))));
                    lstData.Add(new SqlData("MultiReport_Config_ExcelColNumberFormat", SqlUtility.SetValueText(SqlUtility.GetVerifiedDbColNameString(Utility.sDbnull(row.Cells["MultiReport_Config_ExcelColNumberFormat"].Value).Trim()))));
                    lstData.Add(new SqlData("MultiReport_Config_ExcelColAggregateFunction", SqlUtility.SetValueText(SqlUtility.GetVerifiedDbColNameString(Utility.sDbnull(row.Cells["MultiReport_Config_ExcelColAggregateFunction"].Value).Trim()))));
                    if (string.IsNullOrEmpty(Utility.sDbnull(row.Cells["MultiReport_Config_ID"].Value)))
                    {
                        row.BeginEdit();
                        row.Cells["MultiReport_Config_ID"].Value = Utility.Int32Dbnull(SqlUtility.InsertScopeIdentity("sys_multi_report_config", lstData));
                        row.EndEdit();
                    }
                    else SqlUtility.Update("sys_multi_report_config", lstData, "MultiReport_Config_ID = " + Utility.sDbnull(row.Cells["MultiReport_Config_ID"].Value));
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
                    lstData.Add(new SqlData("MultiReport_ID", Utility.sDbnull(nmrReportID.Value)));
                    lstData.Add(new SqlData("MultiReport_Config_Table_Index", Utility.sDbnull(Utility.Int32Dbnull(row.Cells["MultiReport_Config_Table_Index"].Value)).Trim()));
                    lstData.Add(new SqlData("Header_Blank_Row_Count", Utility.Int16Dbnull(row.Cells["Header_Blank_Row_Count"].Value, 0).ToString()));
                    lstData.Add(new SqlData("Header_Title", SqlUtility.SetValueText(SqlUtility.GetVerifiedDbColNameString(Utility.sDbnull(row.Cells["Header_Title"].Value)).Trim())));
                    lstData.Add(new SqlData("Header_Title_Cell_Count", Utility.ByteDbnull(row.Cells["Header_Title_Cell_Count"].Value, 1).ToString()));
                    lstData.Add(new SqlData("Sheet_Name", SqlUtility.SetValueText(SqlUtility.GetVerifiedDbColNameString(Utility.sDbnull(row.Cells["Sheet_Name"].Value)).Trim())));
                    lstData.Add(new SqlData("Sheet_Sequence", Utility.Int16Dbnull(row.Cells["Sheet_Sequence"].Value, 0).ToString()));
                    if (string.IsNullOrEmpty(Utility.sDbnull(row.Cells["MultiReport_Sheet_Config_ID"].Value)))
                    {
                        row.BeginEdit();
                        row.Cells["MultiReport_Sheet_Config_ID"].Value = Utility.Int32Dbnull(SqlUtility.InsertScopeIdentity("sys_multi_report_sheet_config", lstData));
                        row.EndEdit();
                    }
                    else SqlUtility.Update("sys_multi_report_sheet_config", lstData, "MultiReport_Sheet_Config_ID = " + Utility.sDbnull(row.Cells["MultiReport_Sheet_Config_ID"].Value));
                    row.BeginEdit();
                    row.Cells["IsUpdated"].Value = 0;
                    row.EndEdit();
                }
            }
        }
        int reportID = -1;
        void Prepare4Update()
        {
            cmdThemmoi.Enabled = cmdCapnhat.Enabled = cmdXoa.Enabled = cmdExportExcel.Enabled = cmdExportExcelTemplate.Enabled = cmdExit.Enabled =cmdAdmin.Enabled= false;
            FillData4Update();
        }
        void FillData4Update()
        {
            if (objBaocao == null) return;
            cmdSave.Enabled = cmdCancel.Enabled = true;
            nmrReportID.Value = objBaocao.MultiReportId;
            txtReport_Name.Text = objBaocao.MultiReportName;
            txtSQL.Text = objBaocao.MultiReportQueryString;
            cboQueryType.SelectedValue = objBaocao.MultiReportQueryType;
            txtSequence.Value = (int)objBaocao.MultiReportSequence;
            txtReportType.SetCode(objBaocao.MultiReportType);
            chkTrangthai.Checked = Utility.Bool2Bool(objBaocao.TrangThai);
            GetExcelConfig();

            ActiveControl = txtSQL;
        }
        private void nmrReportID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    objBaocao = SysMultiReport.FetchByID(Utility.Int32Dbnull( nmrReportID.Value));
                    Prepare4Update();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void GetExcelConfig()
        {
            QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandSql = string.Format("select *, 0 as [IsUpdated] from sys_multi_report_config where MultiReport_ID = {0} order by MultiReport_Config_Table_Index,MultiReport_Config_ExcelColPosition", reportID) ;
            dtConfig = DataService.GetDataSet(cmd).Tables[0];
            cmd.CommandSql = string.Format("select *, 0 as [IsUpdated] from sys_multi_report_sheet_config where MultiReport_ID = {0} order by Sheet_Sequence", reportID);
            dtSheetConfig = DataService.GetDataSet(cmd).Tables[0];
            grdConfig.DataSource = dtConfig;
            grdSheetConfig.DataSource = dtSheetConfig;
        }

        private void Prepare4Insert()
        {
            try
            {
                cmdThemmoi.Enabled = cmdCapnhat.Enabled = cmdXoa.Enabled = cmdExportExcel.Enabled = cmdExportExcelTemplate.Enabled = cmdExit.Enabled = cmdAdmin.Enabled = false;
                cmdSave.Enabled = cmdCancel.Enabled = true;
                nmrReportID.Value = -1;
                txtSQL.Text = "SELECT GETDATE()";
                txtReport_Name.Text = "BÁO CÁO MỚI";
                cboQueryType.SelectedValue = "GETDATATABLE";
                txtSequence.Value = 0;
                txtReportType.Clear();
                nmrReportID.Value = reportID;
                chkTrangthai.Checked = false;
                GetExcelConfig();
                ActiveControl = txtSQL;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

       
       

      
        #endregion

        bool openQuery = false;
        private void cmdAdmin_Click(object sender, EventArgs e)
        {
            if (!openQuery)
            {
                frm_SpecialPass _frm = new frm_SpecialPass(THU_VIEN_CHUNG.Laygiatrithamsohethong("MATKHAUDACBIET", "Nnq01111983!@#$/", true));
                if (_frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    openQuery = true;
                    cmdAdmin.Image = global::VNS.HIS.UI.Properties.Resources.Lock;
                    splitContainer2.Panel2Collapsed = false;
                }
            }
            else
            {
                openQuery = false;
                cmdAdmin.Image = global::VNS.HIS.UI.Properties.Resources.ADMIN;
                splitContainer2.Panel2Collapsed = !globalVariables.IsAdmin;
            }

        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            FinishOrCancelAct();
        }
    }

}
