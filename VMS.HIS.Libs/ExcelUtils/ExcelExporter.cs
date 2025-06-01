using Janus.Windows.GridEX;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VNS.Libs;

namespace VMS.HIS.Libs.ExcelUtils
{
    public class ExcelExporter
    {
        public static DataTable LoadFromFileExcelToDataTable(string filePath, string sheetName, int headerRowIndex)
        {
            DataTable dt = null;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (FileStream fs = File.OpenRead(filePath))
            {
                using (ExcelPackage excelPackage = new ExcelPackage(fs))
                {
                    ExcelWorkbook excelWorkBook = excelPackage.Workbook;
                    ExcelWorksheet firstWorksheet = excelWorkBook.Worksheets.First();
                    
                    var startRowIndex = firstWorksheet.Dimension.Start.Row;
                    var endRowIndex = firstWorksheet.Dimension.End.Row;
                    var startColumnIndex = firstWorksheet.Dimension.Start.Column;
                    var endColumnIndex = firstWorksheet.Dimension.End.Column;
                    dt = new DataTable();
                    for(int i = startColumnIndex; i <= endColumnIndex; i++)
                    {
                        var headerValue = Utility.sDbnull(firstWorksheet.Cells[headerRowIndex, i].Value);
                        if (!dt.Columns.Contains(headerValue))
                        {
                            dt.Columns.Add(headerValue);
                        }
                    }

                    for(int i = headerRowIndex + 1;i <= endRowIndex; i++)
                    {
                        var newRow = dt.NewRow();
                        dt.Rows.Add(newRow);
                        var count = 0;
                        for(int j = startColumnIndex; j <= endColumnIndex; j++)
                        {
                            var headerValue = Utility.sDbnull(firstWorksheet.Cells[headerRowIndex, j].Value);
                            
                            var cellValue = Utility.sDbnull(firstWorksheet.Cells[i, j].Value);
                            newRow[count] = cellValue;                            
                            
                            count++;
                        }
                    }

                    return dt;
                }
            }
        }

        public static void ExportGridWithTemplate(GridEX grd, string sTemplateFilePath, string fileName, int firstRowIndex)
        {
            var dtData = Utility.ConvertGridEXToDataTable(grd, false);

            var ds = new DataSet();
            ds.Tables.Add(dtData);
            ExportWithTemplate(ds, sTemplateFilePath, fileName, false, firstRowIndex);
        }

        public static void ExportGridWithTemplate(GridEX grd, string sTemplateFilePath, string fileName, bool isUseConfig, int firstRowIndex)
        {
            var dtData = Utility.ConvertGridEXToDataTable(grd, false);

            var ds = new DataSet();
            ds.Tables.Add(dtData);
            ExportWithTemplate(ds, sTemplateFilePath, fileName, isUseConfig, firstRowIndex);
        }

        public static void ExportWithTemplate(DataTable dtData, string sTemplateFilePath, string fileName, bool isUseConfig, int configRowIndex,
            bool isReplaceConfigRow = false, int groupConfigRowIndex = -1, string groupByColumn = "")
        {
            if (dtData.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không có dữ liệu để in !");
                return;
            }
            var ds = new DataSet();
            ds.Tables.Add(dtData.Copy());
            ExportWithTemplate(ds, sTemplateFilePath, fileName, isUseConfig, configRowIndex, isReplaceConfigRow, groupConfigRowIndex, groupByColumn);
        }

        public static void ExportWithTemplate(DataSet dsData, string sTemplateFilePath, string fileName, bool isUseConfig, int configRowIndex)
        {
            ExportWithTemplate(dsData, sTemplateFilePath, fileName, isUseConfig, configRowIndex, false, -1, string.Empty);
        }
        public static void ExportWithTemplate(DataSet dsData, string sTemplateFilePath, string fileName, bool isUseConfig, int configRowIndex, 
            bool isReplaceConfigRow = false, int groupConfigRowIndex = -1, string groupByColumn = "")
        {
            if (dsData.Tables.Count <= 0)
            {
                Utility.ShowMsg("Không có dữ liệu !");
                return;
            }
            var lstTemplateFolder = new List<string> { @"\Excels\Baocao\" };
            var fileTemplatePath = string.Empty;
            foreach(string templateFolder in lstTemplateFolder)
            {
                var xlsTemplatePath = Application.StartupPath + templateFolder + sTemplateFilePath + ".xls";
                var xlsxTemplatePath = Application.StartupPath + templateFolder + sTemplateFilePath + ".xlsx";
                if (File.Exists(xlsTemplatePath))
                {
                    fileTemplatePath = xlsTemplatePath;
                    break;
                }
                else if (File.Exists(xlsxTemplatePath))
                {
                    fileTemplatePath = xlsxTemplatePath;
                    break;
                }
            }
            if (string.IsNullOrEmpty(fileTemplatePath))
            {
                Utility.ShowMsg(string.Format("File mẫu {0} không tồn tại. Vui lòng liên hệ ADMIN",sTemplateFilePath));
                return;
            }
            var now = Utility.getSysDate();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (FileStream fs = File.OpenRead(fileTemplatePath))
            {
                using (ExcelPackage excelPackage = new ExcelPackage(fs))
                {
                    ExcelWorkbook excelWorkBook = excelPackage.Workbook;
                    ExcelWorksheet firstWorksheet = excelWorkBook.Worksheets.First();
                    firstWorksheet.Name = dsData.Tables[0].TableName;
                    
                    for (int k = 1; k < dsData.Tables.Count; k++)
                    {
                        var dt = dsData.Tables[k];
                        excelWorkBook.Worksheets.Copy(firstWorksheet.Name, dt.TableName);
                            //ExcelWorksheetCopy(excelWorkBook, firstWorksheet.Name, dt.TableName));
                    }

                    for (int k = 0; k < dsData.Tables.Count; k++)
                    {
                        var dt = dsData.Tables[k];
                        AddCustomColumn(dt, now);                       

                        var usingWorkshet = excelWorkBook.Worksheets[k];

                        var lstChart = (from c in usingWorkshet.Drawings.AsEnumerable()
                                        where c is ExcelChart
                                        select c).ToList();
                        foreach(ExcelChart chart in lstChart)
                        {
                            foreach(ExcelChartSerie serie in chart.Series)
                            {
                                var range = serie.Series.Split('!')[1];
                                serie.Series = "'" + dt.TableName + "'!" + range;
                            }                            

                            foreach(DataColumn column in dt.Columns)
                            {
                                if (column.ColumnName.ToLower().Contains(chart.Name.ToLower()))
                                {
                                    if (column.ColumnName.ToLower().Contains("min"))
                                    {
                                        chart.YAxis.MinValue = Utility.DoubletoDbnull(dt.Rows[0][column]);
                                    }
                                    else if (column.ColumnName.ToLower().Contains("max"))
                                    {
                                        chart.YAxis.MaxValue = Utility.DoubletoDbnull(dt.Rows[0][column]);
                                    }
                                    else if (column.ColumnName.ToLower().Contains("major"))
                                    {
                                        chart.YAxis.MajorUnit = Utility.DoubletoDbnull(dt.Rows[0][column]);
                                    }
                                }
                                
                            }
                            
                        }

                        var startRowIndex = usingWorkshet.Dimension.Start.Row;
                        var endRowIndex = usingWorkshet.Dimension.End.Row;
                        var startColumnIndex = usingWorkshet.Dimension.Start.Column;
                        var endColumnIndex = usingWorkshet.Dimension.End.Column;


                        var rangeHasParam = new Dictionary<string, List<ExcelRange>>();
                        var rangeHasFormula = new Dictionary<ExcelRange, string>();
                        FindSpecialRange(usingWorkshet, ref rangeHasParam, ref rangeHasFormula);

                        var firstDataRow = dt.Rows[0];
                        foreach (KeyValuePair<string, List<ExcelRange>> entry in rangeHasParam)
                        {
                            var columnName = entry.Key;
                            if (dt.Columns.Contains(columnName))
                            {
                                foreach (ExcelRange range in entry.Value)
                                {
                                    var oldValue = usingWorkshet.Cells[range.Address].Value.ToString();
                                    var newValue = oldValue.Replace("{" + columnName + "}", Utility.sDbnull(firstDataRow[columnName]));
                                    if (Utility.IsNumeric(newValue))
                                    {
                                        usingWorkshet.Cells[range.Address].Value = Utility.DoubletoDbnull(newValue);
                                    }
                                    else
                                    {
                                        usingWorkshet.Cells[range.Address].Value = newValue;
                                    }                                    
                                }
                            }
                        }

                        // Insert 1 loạt row trắng và copy config
                        if (isUseConfig)
                        {                            
                            if (!isReplaceConfigRow)
                            {
                                if(groupConfigRowIndex > 0)
                                {
                                    var lstGroupData = (from row in dt.AsEnumerable()
                                                        select Utility.sDbnull(row[groupByColumn])).Distinct().ToList();
                                    var lastUsingRowIndex = groupConfigRowIndex;
                                    for(int i = 0; i < lstGroupData.Count; i++)
                                    {
                                        var groupData = lstGroupData[i];
                                        var dtByGroup = (from row in dt.AsEnumerable()
                                                         where Utility.sDbnull(row[groupByColumn]) == groupData
                                                         select row);
                                        var groupRowIndex = lastUsingRowIndex + 2;
                                        var dataRowIndex = lastUsingRowIndex + 3;
                                        usingWorkshet.InsertRow(groupRowIndex, 1, groupConfigRowIndex);
                                        usingWorkshet.InsertRow(dataRowIndex, dtByGroup.Count(), configRowIndex);

                                        lastUsingRowIndex = dataRowIndex + dtByGroup.Count() - 2;
                                    }
                                }
                                else
                                {
                                    usingWorkshet.InsertRow(configRowIndex + 1, dt.Rows.Count, configRowIndex);
                                }
                                
                            }
                        }
                        else
                        {
                            if (!isReplaceConfigRow)
                            {
                                usingWorkshet.InsertRow(configRowIndex + 2, dt.Rows.Count, configRowIndex);
                            }
                            
                            var count = 0;
                            foreach(DataColumn dataColumn in dt.Columns)
                            {
                                if (!dataColumn.ReadOnly && dataColumn.ColumnName != "Row_Number")
                                {
                                    var header = dataColumn.ColumnName;
                                    usingWorkshet.Cells[configRowIndex, count + 1].Value = header;
                                    count++;
                                }
                            }
                        }

                        //foreach(KeyValuePair<ExcelRange, string> pair in rangeHasFormula)
                        //{
                        //    var formula = pair.Value;
                        //    var range = pair.Key;
                            
                        //    // Gặp formula dạng D9:D9 và với 9 là dòng configRowIndex thì mới xử lý
                        //    if (formula.Contains("(") && formula.Contains(")"))
                        //    {
                        //        var cell = formula.Split('(')[1].Split(')')[0];
                        //        if (cell.EndsWith(configRowIndex.ToString()))
                        //        {
                        //            var newToCellIndex = configRowIndex + dt.Rows.Count;
                        //            var newToCell = cell.Replace(configRowIndex.ToString(), newToCellIndex.ToString());
                        //            var newFormula = formula.Replace(cell, cell + ":" + newToCell);
                        //            usingWorkshet.Cells[range.Address].Formula = newFormula;
                        //        }
                                
                        //    }
                        //}

                        //var dictColumnCode = new Dictionary<int, string>();
                        //for (int j = startColumnIndex; j <= (isUseConfig ? endColumnIndex : dt.Columns.Count); j++)
                        //{
                        //    var cellRange = usingWorkshet.Cells[configRowIndex, j];
                        //    var formula = cellRange.Formula;
                        //    var columnCodeName = Utility.sDbnull(cellRange.Value);
                        //    dictColumnCode.Add(j, !string.IsNullOrEmpty(formula) ? formula : columnCodeName);
                        //}
                        if(groupConfigRowIndex > 0)
                        {
                            ProcessGroupExcel(dt, usingWorkshet, configRowIndex, groupConfigRowIndex, groupByColumn, isUseConfig);
                        }
                        else
                        {
                            ProcessNoGroupExcel(dt, usingWorkshet, configRowIndex, isUseConfig, isReplaceConfigRow);
                        }
                        
                        if (!isReplaceConfigRow)
                        {
                            if (isUseConfig)
                            {
                                usingWorkshet.DeleteRow(configRowIndex, 1);
                                if (groupConfigRowIndex > 0)
                                {
                                    usingWorkshet.DeleteRow(groupConfigRowIndex, 1);
                                }
                            }
                            else
                            {
                                usingWorkshet.DeleteRow(configRowIndex + dt.Rows.Count + 1, 1);
                            }
                        }
                    }

                    
                    excelPackage.SaveAs(new FileInfo(fileName)); // This is the important part.
                }
            }
            Utility.ShowMsg("Xuất excel kết thúc");
        }

        private static void ProcessGroupExcel(DataTable dt, ExcelWorksheet usingWorkshet, int configRowIndex, int groupConfigRowIndex, string groupByColumn, 
            bool isUseConfig)
        {
            var endColumnIndex = usingWorkshet.Dimension.End.Column;


            var lastGroupByColumn = string.Empty;
            var lastUsingRowIndex = configRowIndex;
            var columnIndex = (isUseConfig ? endColumnIndex : dt.Columns.Count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Row_Number"] = i + 1;
                var count = 1;
                var groupByValue = Utility.sDbnull(dt.Rows[i][groupByColumn]);
                var dtByGroup = (from row in dt.AsEnumerable()
                                 where Utility.sDbnull(row[groupByColumn]) == groupByValue
                                 select row);
                if (lastGroupByColumn != groupByValue)
                {
                    for (int j = 1; j <= columnIndex; j++)
                    {
                    
                        var groupConfigCellRange = usingWorkshet.Cells[groupConfigRowIndex, j];
                        var formula = groupConfigCellRange.Formula;
                        var groupConfigCellValue = Utility.sDbnull(groupConfigCellRange.Value);
                        var groupValueCellRange = usingWorkshet.Cells[lastUsingRowIndex + 1, j];
                        if (!string.IsNullOrEmpty(formula))
                        {
                            var matches = Regex.Matches(formula, @"\d+");
                            if(matches.Count > 0)
                            {
                                var match = matches[0];
                                if (formula.StartsWith("SUM") && !formula.Contains(":"))
                                {
                                    var insideFormulaMatch = Regex.Matches(formula, @"(\w*\d+)")[0];
                                    var formulaColumnValue = insideFormulaMatch.Value.Replace(match.Value, "").Trim();
                                    var formulaRowValue = Utility.sDbnull(Utility.Int32Dbnull(match.Value) + 2);
                                    var newInsideFormulaMatch = insideFormulaMatch.Value.Replace(match.Value, Utility.sDbnull(lastUsingRowIndex + 1 + dtByGroup.Count()));
                                    formula = formula.Replace(insideFormulaMatch.Value,
                                        formulaColumnValue + Utility.sDbnull(lastUsingRowIndex + 2) + ":" + newInsideFormulaMatch);
                                }
                                else
                                {
                                    formula = formula.Replace(match.Value, Utility.sDbnull(lastUsingRowIndex + 1));
                                }
                            }

                            groupValueCellRange.Formula = formula;
                        }
                        else if (groupConfigCellValue == groupByColumn)
                        {
                            groupValueCellRange.Value = groupByValue;
                        }
                    }
                    lastGroupByColumn = groupByValue;
                    lastUsingRowIndex++;
                }
                for (int j = 1; j <= columnIndex; j++)
                {
                    var value = string.Empty;
                    if (isUseConfig)
                    {
                        var configCellRange = usingWorkshet.Cells[configRowIndex, j];
                        var formula = configCellRange.Formula;
                        var formulaR1C1 = configCellRange.FormulaR1C1;
                        //var columnCodeName = dictColumnCode[j];
                        var columnCodeName = Utility.sDbnull(configCellRange.Value);
                        ExcelRange valueCellRange = usingWorkshet.Cells[lastUsingRowIndex + 1, j];

                        if (string.IsNullOrEmpty(formula))
                        {
                            if (dt.Columns.Contains(columnCodeName) && !string.IsNullOrEmpty(columnCodeName))
                            {
                                value = Utility.sDbnull(dt.Rows[i][columnCodeName]);
                                value = value.ToLower() == "true" ? "1" : value.ToLower() == "false" ? "0" : value;
                                if (Utility.IsNumeric(value))
                                {
                                    valueCellRange.Value = Utility.DoubletoDbnull(value);
                                }
                                else if (!string.IsNullOrEmpty(value))
                                {
                                    valueCellRange.Value = value;
                                }

                            }
                        }
                        // Cell nào có formula thì giữ nguyên
                        else
                        {
                            var matches = Regex.Matches(formula, @"\d+");
                            if (matches.Count > 0)
                            {
                                formula = formula.Replace(matches[0].Value, Utility.sDbnull(lastUsingRowIndex + 1));
                                valueCellRange.Formula = formula;
                            }

                        }
                    }
                    else
                    {
                        var dataColumn = dt.Columns[j - 1];
                        if (!dataColumn.ReadOnly && dataColumn.ColumnName != "Row_Number")
                        {
                            value = Utility.sDbnull(dt.Rows[i][j - 1]);
                            value = value.ToLower() == "true" ? "1" : value.ToLower() == "false" ? "0" : value;
                            if (Utility.IsNumeric(value))
                            {
                                usingWorkshet.Cells[configRowIndex + i + 1, count].Value = Utility.DoubletoDbnull(value);
                            }
                            else
                            {
                                usingWorkshet.Cells[configRowIndex + i + 1, count].Value = value;
                            }
                            count++;
                        }
                    }
                }    
                
                lastUsingRowIndex++;
                              
            }
        }

        private static void ProcessNoGroupExcel(DataTable dt, ExcelWorksheet usingWorkshet, int configRowIndex, bool isUseConfig, bool isReplaceConfigRow)
        {
            var endColumnIndex = usingWorkshet.Dimension.End.Column;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Row_Number"] = i + 1;
                var count = 1;
                for (int j = 1; j <= (isUseConfig ? endColumnIndex : dt.Columns.Count); j++)
                {
                    //if (string.IsNullOrEmpty(columnCodeName)) continue;
                    var value = string.Empty;
                    if (isUseConfig)
                    {
                        var configCellRange = usingWorkshet.Cells[configRowIndex, j];
                        var formula = configCellRange.Formula;
                        var formulaR1C1 = configCellRange.FormulaR1C1;
                        //var columnCodeName = dictColumnCode[j];
                        var columnCodeName = Utility.sDbnull(configCellRange.Value);
                        ExcelRange valueCellRange = usingWorkshet.Cells[(isReplaceConfigRow ? configRowIndex + i : configRowIndex + i + 1), j];

                        // Cell nào có formula thì giữ nguyên
                        if (string.IsNullOrEmpty(formula))
                        {
                            if (dt.Columns.Contains(columnCodeName) && !string.IsNullOrEmpty(columnCodeName))
                            {
                                value = Utility.sDbnull(dt.Rows[i][columnCodeName]);
                                value = value.ToLower() == "true" ? "1" : value.ToLower() == "false" ? "0" : value;
                                if (Utility.IsNumeric(value))
                                {
                                    valueCellRange.Value = Utility.DoubletoDbnull(value);
                                }
                                else if (!string.IsNullOrEmpty(value))
                                {
                                    valueCellRange.Value = value;
                                }

                            }
                        }
                        else
                        {

                            var matches = Regex.Matches(formula, @"\d+");

                            foreach (Match match in matches)
                            {

                                if (formula.StartsWith("SUM") && !formula.Contains(":"))
                                {
                                    //var insideFormulaMatch = Regex.Matches(formula, @"(\w*\d+)")[0];
                                    //var formulaColumnValue = insideFormulaMatch.Value.Replace(match.Value, "").Trim();
                                    //var formulaRowValue = Utility.sDbnull(Utility.Int32Dbnull(match.Value) + 2);
                                    //var newInsideFormulaMatch = insideFormulaMatch.Value.Replace(match.Value, Utility.sDbnull(lastUsingRowIndex + dtByGroup.Count()));
                                    //formula = formula.Replace(insideFormulaMatch.Value,
                                    //    formulaColumnValue + Utility.sDbnull(lastUsingRowIndex + 1) + ":" + newInsideFormulaMatch);
                                }
                                else
                                {
                                    formula = formula.Replace(match.Value, Utility.sDbnull(Utility.Int32Dbnull(match.Value) + i + 1));
                                }
                            }
                            valueCellRange.Formula = formula;
                        }
                    }
                    else
                    {
                        var dataColumn = dt.Columns[j - 1];
                        if (!dataColumn.ReadOnly && dataColumn.ColumnName != "Row_Number")
                        {
                            value = Utility.sDbnull(dt.Rows[i][j - 1]);
                            value = value.ToLower() == "true" ? "1" : value.ToLower() == "false" ? "0" : value;
                            if (Utility.IsNumeric(value))
                            {
                                usingWorkshet.Cells[configRowIndex + i + 1, count].Value = Utility.DoubletoDbnull(value);
                            }
                            else
                            {
                                usingWorkshet.Cells[configRowIndex + i + 1, count].Value = value;
                            }
                            count++;
                        }
                    }
                    //if ((dt.Columns.Contains(columnCodeName) && !string.IsNullOrEmpty(columnCodeName))
                    //    || !isUseConfig)
                    //{
                    //    var value = Utility.sDbnull(dt.Rows[i][columnCodeName]);
                    //    value = value.ToLower() == "true" ? "1" : value.ToLower() == "false" ? "0" : value;
                    //    usingWorkshet.Cells[firstRowIndex + i + 1, j].Value = value;
                    //}
                }
            }
        }

        private static void AddCustomColumn(DataTable dt, DateTime now)
        {
            if (!dt.Columns.Contains("Row_Number"))
            {
                var newColumn = new DataColumn();
                newColumn.ColumnName = "Row_Number";
                dt.Columns.Add(newColumn);
            }
            if (!dt.Columns.Contains("currentUserName"))
            {
                var newColumn = new DataColumn();
                newColumn.ColumnName = "Ten_nguoidung";
                dt.Columns.Add(newColumn);
                dt.Rows[0]["Ten_nguoidung"] = globalVariables.Tennguoidung;
                dt.Columns["Ten_nguoidung"].ReadOnly = true;
            }
            if (!dt.Columns.Contains("NgayThangNam"))
            {
                var newColumn = new DataColumn();
                newColumn.ColumnName = "NgayThangNam";
                dt.Columns.Add(newColumn);
                dt.Rows[0]["NgayThangNam"] = now.ToString("dd/MM/yyyy");
                dt.Columns["NgayThangNam"].ReadOnly = true;
            }
            if (!dt.Columns.Contains("Ngay_Full"))
            {
                var newColumn = new DataColumn();
                newColumn.ColumnName = "Ngay_Full";
                dt.Columns.Add(newColumn);
                dt.Rows[0]["Ngay_Full"] = now.ToString("dd/MM/yyyy HH:mm:ss");
                dt.Columns["Ngay_Full"].ReadOnly = true;
            }
           
            
            //if (!dt.Columns.Contains("DepartmentCLSName"))
            //{
            //    var newColumn = new DataColumn();
            //    newColumn.ColumnName = "DepartmentCLSName";
            //    dt.Columns.Add(newColumn);
            //    dt.Rows[0]["DepartmentCLSName"] = globalVariables.ten_khoathuchien;
            //    dt.Columns["DepartmentCLSName"].ReadOnly = true;
            //}
        }

        private static ExcelWorksheet ExcelWorksheetCopy(ExcelWorkbook workbook, string existingWorksheetName, string newWorksheetName)
        {
            ExcelWorksheet worksheet = workbook.Worksheets.Copy(existingWorksheetName, newWorksheetName);
            return worksheet;
        }

        // Tìm range có param dạng {abc} hoặc formula = ()
        private static void FindSpecialRange(ExcelWorksheet ws, ref Dictionary<string, List<ExcelRange>> rangeHasParam, ref Dictionary<ExcelRange, string> rangeHasFormula)
        {
            var startColumnIndex = ws.Dimension.Start.Column;
            var endColumnIndex = ws.Dimension.End.Column;
            var startRowIndex = ws.Dimension.Start.Row;
            var endRowIndex = ws.Dimension.End.Row;
            Regex regex = new Regex(@"(?<=\{)[^}]*(?=\})");
            for (int i = startRowIndex; i <= endRowIndex; i++)
            {
                for(int j = startColumnIndex; j <= endColumnIndex; j++)
                {
                    var range = ws.Cells[i, j];
                    var rangeValue = Utility.sDbnull(range.Value);
                    foreach (Match match in regex.Matches(rangeValue))
                    {
                        var param = match.Value;
                        if (rangeHasParam.ContainsKey(param))
                        {
                            var lstExcelRange = rangeHasParam[param];
                            lstExcelRange.Add(range);
                            rangeHasParam[param] = lstExcelRange;
                        }
                        else
                        {
                            rangeHasParam.Add(param, new List<ExcelRange> { range });
                        }
                    }

                    if (!string.IsNullOrEmpty(range.Formula))
                    {                        
                        rangeHasFormula.Add(range, range.Formula);
                    }
                }
            }
        }
    }
}
