
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Janus.Windows.GridEX.Export;
using Microsoft.Office.Interop.Excel;
using NLog.LogReceiverService;
using System.Linq;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;
using NLog;
using VerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment;
using Excel = Microsoft.Office.Interop.Excel;
using VNS.Properties;
using VMS.HIS.DAL;
using Aspose.Cells;
using System.Collections.Generic;
namespace VNS.Libs
{

    public class ExcelUtlity
    {

        private string sStartCell = "C";
        private string sEndCell = "T";
        private readonly Logger log;
        public ExcelUtlity()
        {
            log = LogManager.GetCurrentClassLogger();
        }
        public static void Insotamtra_theophieulinh(System.Data.DataTable m_dtExportExcel, string worksheetName, string saveAsLocation, string ReporType, string tenkhoa, string ngay_linh,int STTBatdau,  bool isPrinpreview)
        {
            try
            {
                string sfileName = AppDomain.CurrentDomain.BaseDirectory + "sotamtra\\sotamtra.xls";
                string sfileNameSave = AppDomain.CurrentDomain.BaseDirectory + "sotamtra" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
                object misValue = System.Reflection.Missing.Value;
                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();
                workbook.Open(sfileName);
                Aspose.Cells.Worksheet worksheet = workbook.Worksheets[0];

                //Aspose.Cells.Range range = worksheet.Cells.CreateRange("C1","T1");
                //range.Merge();
                worksheet.Cells["C1"].PutValue(string.Format("SỔ TỔNG HỢP THUỐC HÀNG NGÀY :{0}", ReporType));

                //range = worksheet.Cells.CreateRange("C2","H2");
                //range.Merge();
                worksheet.Cells["C2"].PutValue(string.Format("TÊN KHOA :{0}", tenkhoa));

                // worksheet.Cells["C2"].SetStyle(style);
                Cells objcells = worksheet.Cells;// Get a particular Cell.
                Cell objcell = objcells["B3"];
                objcell.PutValue(string.Format("Ngày lĩnh :{0}", ngay_linh));

                DataRow totalsRow = m_dtExportExcel.NewRow();
                totalsRow["ten_benhnhan"] = "Tổng cộng :";
                int icol = 0;
                foreach (DataColumn col in m_dtExportExcel.Columns)
                {
                    decimal colTotal = 0;
                    if (icol >= STTBatdau)
                    {
                        colTotal = (from p in m_dtExportExcel.AsEnumerable()
                                 select Utility.DecimaltoDbnull(p[col.ColumnName], 0)).Sum();

                        totalsRow[col.ColumnName] = colTotal;
                    }
                    icol++;
                }
                m_dtExportExcel.Rows.Add(totalsRow);

                icol = 0;
                int irow = 5;

                foreach (DataColumn column in m_dtExportExcel.Columns)
                {
                    string colname = Utility.sDbnull(column.ColumnName);
                    bool isBold = false;
                    colname = ReplaceTieuDe(colname, ref isBold);
                    worksheet.Cells[irow, icol].PutValue(colname);
                    var objstyle = worksheet.Cells[irow, icol].GetStyle();
                    objstyle.Font.IsBold = isBold;
                    objstyle.Font.Name = "Times New Roman";
                    //objstyle.Font.=new Font("Times New Roman", 14, FontStyle.Bold);      
                    //Font.FontStyle =           // Get>=5 associated style object of the cell.
                    if (icol >= STTBatdau)
                    {


                        // Specify the angle of rotation of the text.
                        objstyle.Rotation = 90;
                        // Assign the updated style object back to the cell

                    }

                    //  Aspose.Cells.Range _range;
                    //Setting the line style of the top border
                    objstyle.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;

                    //Setting the color of the top border
                    objstyle.Borders[BorderType.TopBorder].Color = Color.Black;

                    //Setting the line style of the bottom border
                    objstyle.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

                    //Setting the color of the bottom border
                    objstyle.Borders[BorderType.BottomBorder].Color = Color.Black;

                    //Setting the line style of the left border
                    objstyle.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;

                    //Setting the color of the left border
                    objstyle.Borders[BorderType.LeftBorder].Color = Color.Black;

                    //Setting the line style of the right border
                    objstyle.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;

                    //Setting the color of the right border
                    objstyle.Borders[BorderType.RightBorder].Color = Color.Black;
                    worksheet.Cells[irow, icol].SetStyle(objstyle);


                    icol++;
                }

                int iStartRowContent = 6;
                int imaxrow = 0;
                for (int i = 0; i < m_dtExportExcel.Rows.Count; i++)
                {
                    for (int j = 0; j < m_dtExportExcel.Columns.Count; j++)
                    {
                        string sValue = Utility.sDbnull(m_dtExportExcel.Rows[i][j]);
                        worksheet.Cells[i + iStartRowContent, j].PutValue(sValue);

                        //  Aspose.Cells.Range _range;
                        //Setting the line style of the top border
                        var objstyle = worksheet.Cells[i + iStartRowContent, j].GetStyle();
                        objstyle.Font.Name = "Times New Roman";
                        objstyle.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;

                        //Setting the color of the top border
                        objstyle.Borders[BorderType.TopBorder].Color = Color.Black;

                        //Setting the line style of the bottom border
                        objstyle.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

                        //Setting the color of the bottom border
                        objstyle.Borders[BorderType.BottomBorder].Color = Color.Black;

                        //Setting the line style of the left border
                        objstyle.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;

                        //Setting the color of the left border
                        objstyle.Borders[BorderType.LeftBorder].Color = Color.Black;

                        //Setting the line style of the right border
                        objstyle.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;

                        //Setting the color of the right border
                        objstyle.Borders[BorderType.RightBorder].Color = Color.Black;
                        if (m_dtExportExcel.Rows.Count == i)
                        {
                            objstyle.Font.IsBold = true;
                            objstyle.Font.Size = 12;
                        }
                        worksheet.Cells[i + iStartRowContent, j].SetStyle(objstyle);
                        //sheetData.Cells[i + iStartRowContent, j].SetStyle(s3);

                    }
                    imaxrow++;

                }

                // worksheet.AutoFitRow();
                worksheet.AutoFitRows();
                worksheet.AutoFitColumns();


                // worksheet.Cells.ImportDataTable(m_dtExportExcel, true, "A5");

                if (System.IO.File.Exists(saveAsLocation)) File.Delete(saveAsLocation);
                workbook.Save(saveAsLocation);
                if (isPrinpreview)
                {

                    PrintMyExcelFile(saveAsLocation);
                }
                else
                {
                    System.Diagnostics.Process.Start(saveAsLocation);
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
          


        }
        public static void Insotamtra(System.Data.DataTable m_dtExportExcel, string worksheetName, string saveAsLocation, string ReporType, string tenkhoa, string tungay, string denngay, int STTBatdau, bool isPrinpreview)
        {

            string sfileName = AppDomain.CurrentDomain.BaseDirectory + "sotamtra\\sotamtra.xls";
            string sfileNameSave = AppDomain.CurrentDomain.BaseDirectory + "sotamtra" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
            object misValue = System.Reflection.Missing.Value;
            Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();
            workbook.Open(sfileName);
            Aspose.Cells.Worksheet worksheet = workbook.Worksheets[0];

            //Aspose.Cells.Range range = worksheet.Cells.CreateRange("C1","T1");
            //range.Merge();
            worksheet.Cells["C1"].PutValue(string.Format("SỔ TỔNG HỢP THUỐC HÀNG NGÀY :{0}", ReporType));

            //range = worksheet.Cells.CreateRange("C2","H2");
            //range.Merge();
            worksheet.Cells["C2"].PutValue(string.Format("TÊN KHOA :{0}", tenkhoa));

            // worksheet.Cells["C2"].SetStyle(style);
            Cells objcells = worksheet.Cells;// Get a particular Cell.
            Cell objcell = objcells["B3"];
            objcell.PutValue(string.Format("Ngày lĩnh :{0}-{1}", tungay, denngay));

            DataRow totalsRow = m_dtExportExcel.NewRow();
            totalsRow["ten_benhnhan"] = "Tổng cộng :";
            int icol = 0;
            int m = 0;
            int n = 0;
            foreach (DataColumn col in m_dtExportExcel.Columns)
            {
                m += 1;
                decimal colTotal = 0;
                if (icol >= STTBatdau)
                {
                    //foreach (DataRow row in col.Table.Rows)
                    //{
                    //    n += 1;
                    //    colTotal += Utility.DecimaltoDbnull(row[col].ToString());
                    //}

                     colTotal = (from p in m_dtExportExcel.AsEnumerable()
                             select Utility.DecimaltoDbnull(p[col.ColumnName], 0)).Sum();
                          
                    //colTotal =Utility.DecimaltoDbnull( m_dtExportExcel.Compute(string.Format("SUM({0})", col.ColumnName), "1=1"));
                     totalsRow[col.ColumnName] = colTotal;
                }
                icol++;
            }
            m_dtExportExcel.Rows.Add(totalsRow);

            icol = 0;
            int irow = 5;

            foreach (DataColumn column in m_dtExportExcel.Columns)
            {
                string colname = Utility.sDbnull(column.ColumnName);
                bool isBold = false;
                colname = ReplaceTieuDe(colname, ref isBold);
                worksheet.Cells[irow, icol].PutValue(colname);
                var objstyle = worksheet.Cells[irow, icol].GetStyle();
                objstyle.Font.IsBold = isBold;
                objstyle.Font.Name = "Times New Roman";
                //objstyle.Font.=new Font("Times New Roman", 14, FontStyle.Bold);      
                //Font.FontStyle =           // Get>=5 associated style object of the cell.
                if (icol >= STTBatdau)
                {


                    // Specify the angle of rotation of the text.
                    objstyle.Rotation = 90;
                    // Assign the updated style object back to the cell

                }

                //  Aspose.Cells.Range _range;
                //Setting the line style of the top border
                objstyle.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;

                //Setting the color of the top border
                objstyle.Borders[BorderType.TopBorder].Color = Color.Black;

                //Setting the line style of the bottom border
                objstyle.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

                //Setting the color of the bottom border
                objstyle.Borders[BorderType.BottomBorder].Color = Color.Black;

                //Setting the line style of the left border
                objstyle.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;

                //Setting the color of the left border
                objstyle.Borders[BorderType.LeftBorder].Color = Color.Black;

                //Setting the line style of the right border
                objstyle.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;

                //Setting the color of the right border
                objstyle.Borders[BorderType.RightBorder].Color = Color.Black;
                worksheet.Cells[irow, icol].SetStyle(objstyle);


                icol++;
            }

            int iStartRowContent = 6;
            int imaxrow = 0;
            for (int i = 0; i < m_dtExportExcel.Rows.Count; i++)
            {
                for (int j = 0; j < m_dtExportExcel.Columns.Count; j++)
                {
                    string sValue = Utility.sDbnull(m_dtExportExcel.Rows[i][j]);
                    worksheet.Cells[i + iStartRowContent, j].PutValue(sValue);

                    //  Aspose.Cells.Range _range;
                    //Setting the line style of the top border
                    var objstyle = worksheet.Cells[i + iStartRowContent, j].GetStyle();
                    objstyle.Font.Name = "Times New Roman";
                    objstyle.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;

                    //Setting the color of the top border
                    objstyle.Borders[BorderType.TopBorder].Color = Color.Black;

                    //Setting the line style of the bottom border
                    objstyle.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

                    //Setting the color of the bottom border
                    objstyle.Borders[BorderType.BottomBorder].Color = Color.Black;

                    //Setting the line style of the left border
                    objstyle.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;

                    //Setting the color of the left border
                    objstyle.Borders[BorderType.LeftBorder].Color = Color.Black;

                    //Setting the line style of the right border
                    objstyle.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;

                    //Setting the color of the right border
                    objstyle.Borders[BorderType.RightBorder].Color = Color.Black;
                    if (m_dtExportExcel.Rows.Count == i)
                    {
                        objstyle.Font.IsBold = true;
                        objstyle.Font.Size = 12;
                    }
                    worksheet.Cells[i + iStartRowContent, j].SetStyle(objstyle);
                    //sheetData.Cells[i + iStartRowContent, j].SetStyle(s3);

                }
                imaxrow++;

            }

            // worksheet.AutoFitRow();
            worksheet.AutoFitRows();
            worksheet.AutoFitColumns();


            // worksheet.Cells.ImportDataTable(m_dtExportExcel, true, "A5");

            if (System.IO.File.Exists(saveAsLocation)) File.Delete(saveAsLocation);
            workbook.Save(saveAsLocation);
            if (isPrinpreview)
            {

                PrintMyExcelFile(saveAsLocation);
            }
            else
            {
                System.Diagnostics.Process.Start(saveAsLocation);
            }


        }
        static void SetBorderStyle(Aspose.Cells.Style objstyle, Color _mycolor, string fontname, int fontsize, TextAlignmentType HA, TextAlignmentType VA,bool IsBold)
        {
            objstyle.Font.Name = fontname;
            objstyle.Font.Size = fontsize;
            objstyle.Font.IsBold = IsBold;
            objstyle.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            objstyle.HorizontalAlignment = HA;
            objstyle.VerticalAlignment = VA;
            //Setting the color of the top border
            objstyle.Borders[BorderType.TopBorder].Color = _mycolor;

            //Setting the line style of the bottom border
            objstyle.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            //Setting the color of the bottom border
            objstyle.Borders[BorderType.BottomBorder].Color = _mycolor;

            //Setting the line style of the left border
            objstyle.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;

            //Setting the color of the left border
            objstyle.Borders[BorderType.LeftBorder].Color = _mycolor;

            //Setting the line style of the right border
            objstyle.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;

            //Setting the color of the right border
            objstyle.Borders[BorderType.RightBorder].Color = _mycolor;
        }
        public static void Inphieucongkhai(System.Data.DataSet dsData, string worksheetName, string saveAsLocation, string ReporType)
        {
            try
            {
                DataTable m_dtExportExcel = dsData.Tables[0];
                string sfileName = AppDomain.CurrentDomain.BaseDirectory + "\\phieucongkhai\\phieucongkhai.xls";
                string sfileNameSave = AppDomain.CurrentDomain.BaseDirectory + "\\phieucongkhai" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
                object misValue = System.Reflection.Missing.Value;
                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();
                workbook.Open(sfileName);
                Aspose.Cells.Worksheet worksheet = workbook.Worksheets[0];


                worksheet.Cells["A3"].PutValue(Utility.sDbnull(string.Format("{0} Buồng: {1}  Giường: {2} ",Utility.sDbnull(dsData.Tables[1].Rows[0]["ten_khoanoitru"]), 
                    Utility.sDbnull(dsData.Tables[1].Rows[0]["ten_buong"], ""), Utility.sDbnull(dsData.Tables[1].Rows[0]["ten_giuong"], ""))));
               // worksheet.Cells["F3"].PutValue(Utility.sDbnull(string.Format("Buồng: {0}  Giường: {1} ", Utility.sDbnull(dsData.Tables[1].Rows[0]["ten_buong"], ""), Utility.sDbnull(dsData.Tables[1].Rows[0]["ten_giuong"], ""))));
                worksheet.Cells["A4"].PutValue(Utility.sDbnull(string.Format("Tên bệnh nhân: {0} Tuổi: {1}  Giới tính: {2}", dsData.Tables[1].Rows[0]["ten_benhnhan"],dsData.Tables[1].Rows[0]["tuoi"], dsData.Tables[1].Rows[0]["gioi_tinh"])));
                //worksheet.Cells["C4"].PutValue(Utility.sDbnull(string.Format("Tuổi: {0}  Giới tính: {1}", dsData.Tables[1].Rows[0]["tuoi"], dsData.Tables[1].Rows[0]["gioi_tinh"])));
                worksheet.Cells["A5"].PutValue(Utility.sDbnull(string.Format("Ngày vào viện: {0} - Ngày ra viện: {1}", Utility.sDbnull(dsData.Tables[1].Rows[0]["sngay_nhapvien"], ""), Utility.sDbnull(dsData.Tables[1].Rows[0]["ngay_ravien"], ""))));
                worksheet.AutoFitColumn(0, 2, 4);
                worksheet.AutoFitColumn(1, 2, 4);
                worksheet.AutoFitColumn(2, 2, 4);
                Cells objcells = worksheet.Cells;
                Cell objcell = objcells["B3"];
                int icol = 0;
                int iStartRowContent = 7;
                int iCurrentRow = 7;
                int imaxrow = 0;
                int colngayfrom = 12;
                int startcolExcel = 2;//Cột 1 tên dịch vụ, cột 2 tên đơn vị tính
                List<int> lstCol2print = new List<int>() { 8, 9 };
                int totaldays = 0;
                Cell _mycell;
                Aspose.Cells.Range range;
                Aspose.Cells.Style objstyle;
                //Tạo dữ liệu ngày
                for (int j = colngayfrom; j < m_dtExportExcel.Columns.Count; j++)
                {
                    totaldays++;
                    _mycell = worksheet.Cells[6, startcolExcel];
                    _mycell.PutValue(m_dtExportExcel.Columns[j].ColumnName);
                    objstyle = _mycell.GetStyle();
                    SetBorderStyle(objstyle, Color.DarkRed, "Times New Roman", 12,TextAlignmentType.Center,TextAlignmentType.Center, true);
                    _mycell.SetStyle(objstyle);
                    startcolExcel++;
                }
                //Merge cột ngày
                range = worksheet.Cells.CreateRange(5, 2, 1, totaldays);
                range.Merge();
                //Cột tổng cộng sau ngày. Merge 2 hàng 1 cột
                _mycell = worksheet.Cells[5, 2 + totaldays];
                _mycell.PutValue("Tổng cộng");
               //Merge và set border cho cột tổng cộng
                range = worksheet.Cells.CreateRange(5, 2 + totaldays, 2,1);
                range.Merge();
                worksheet.AutoFitColumn(2 + totaldays, 5, 6);//Fit để hiển thị toàn bộ chữ tổng cộng
                range.SetOutlineBorders(CellBorderType.Thin, Color.Black);

                //Set style
                objstyle = _mycell.GetStyle();
                SetBorderStyle(objstyle, Color.DarkBlue, "Times New Roman", 12, TextAlignmentType.Center, TextAlignmentType.Center, true);
                _mycell.SetStyle(objstyle);
                List<int> lstLoai = (from p in m_dtExportExcel.AsEnumerable()
                                     orderby Utility.Int32Dbnull(p["stt_in"], -1)
                                     select Utility.Int32Dbnull(p["id_loaithanhtoan"], -1)
                                     
                                     ).Distinct().ToList<int>();
                int totalColunm = 0;
               
                foreach (int id_loaithanhtoan in lstLoai)
                {
                    DataTable dtLoaiData = m_dtExportExcel.Select("id_loaithanhtoan="+id_loaithanhtoan.ToString(),"stt_in,stt_hthi_loaidichvu ,stt_hthi_dichvu,stt_hthi_chitiet,ten").CopyToDataTable();
                    #region "Nhóm loại thanh toán"
                    worksheet.Cells.InsertRow( iCurrentRow);
                    _mycell = worksheet.Cells[iCurrentRow, 0];
                   range = worksheet.Cells.CreateRange(iCurrentRow, 0, 1, totaldays+3);//+3 cho 3 cột tên dịch vụ, đơn vị tính, tổng cộng
                    range.Merge();
                    range.SetOutlineBorders(CellBorderType.Thin, Color.Black);
                    _mycell.PutValue(Utility.sDbnull(dtLoaiData.Rows[0]["ten_loaithanhtoan"]));
                    objstyle = _mycell.GetStyle();
                    SetBorderStyle(objstyle, Color.DarkBlue, "Times New Roman", 14, TextAlignmentType.Left, TextAlignmentType.Center, true);
                    _mycell.SetStyle(objstyle);
                    #endregion
                    //Tăng để ghi chi tiết
                    iCurrentRow++;
                    //reset lại để in
                    startcolExcel = 10;
                   
                    if (id_loaithanhtoan != 2)//Chẩn đoán hình ảnh tách tiếp bên trong
                    {
                        for (int i = 0; i < dtLoaiData.Rows.Count; i++)
                        {
                            worksheet.Cells.InsertRow(i + iCurrentRow);
                            totalColunm = 0;
                            for (int j = startcolExcel; j < dtLoaiData.Columns.Count; j++)
                            {
                                _mycell = worksheet.Cells[i + iCurrentRow, j - startcolExcel];
                                string sValue = Utility.sDbnull(dtLoaiData.Rows[i][j]);
                                _mycell.PutValue(sValue);
                                if (j >= 12)//Các cột giá trị. Cột 10 là tên; Cột 11 là đơn vị tính
                                    totalColunm += Utility.Int32Dbnull(sValue, 0);
                                //  Aspose.Cells.Range _range;
                                //Setting the line style of the top border
                                objstyle = _mycell.GetStyle();
                                objstyle.Font.Name = "Times New Roman";
                                SetBorderStyle(objstyle, Color.Black, "Times New Roman", 12, TextAlignmentType.Left, TextAlignmentType.Center, false);
                                if (dtLoaiData.Rows.Count == i)
                                {
                                    objstyle.Font.IsBold = true;
                                    objstyle.Font.Size = 12;
                                }
                                objstyle.IsTextWrapped = true;
                                _mycell.SetStyle(objstyle);
                                //sheetData.Cells[i + iStartRowContent, j].SetStyle(s3);

                            }
                            //Điền giá trị cho cột tổng cộng sau các cột ngày. 
                            _mycell = worksheet.Cells[i + iCurrentRow, 2 + totaldays];
                            _mycell.PutValue(totalColunm.ToString());
                            objstyle = _mycell.GetStyle();
                            objstyle.Font.IsBold = true;
                            objstyle.Font.Size = 14;
                            SetBorderStyle(objstyle, Color.Black, "Times New Roman", 12, TextAlignmentType.Center, TextAlignmentType.Center, true);
                            _mycell.SetStyle(objstyle);
                            imaxrow++;

                        }
                        iCurrentRow += dtLoaiData.Rows.Count;
                    }
                    else//Nhóm CLS tách tiếp theo các loại dịch vụ XN, XQ,SA,...
                    {
                        DataTable dtCDHA = dtLoaiData.Clone();
                        List<string> lstid_loaidvu = (from p in dtLoaiData.AsEnumerable()
                                                      orderby Utility.Int32Dbnull(p["stt_in"], -1), Utility.Int32Dbnull(p["stt_hthi_loaidichvu"], -1)
                                                      select Utility.sDbnull(p["id_loaidichvu"], "-1")
                                    ).Distinct().ToList<string>();
                        foreach (string id_loaidichvu in lstid_loaidvu)
                        {
                            dtCDHA = dtLoaiData.Select("id_loaidichvu='" + id_loaidichvu.ToString() + "'", "stt_in,stt_hthi_loaidichvu ,stt_hthi_dichvu,stt_hthi_chitiet,ten").CopyToDataTable();
                            #region "Nhóm loại thanh toán"
                            worksheet.Cells.InsertRow(iCurrentRow);
                            _mycell = worksheet.Cells[iCurrentRow, 0];
                            range = worksheet.Cells.CreateRange(iCurrentRow, 0, 1,  totaldays+3);//+3 cho 3 cột tên dịch vụ, đơn vị tính, tổng cộng
                            range.Merge();
                            range.SetOutlineBorders(CellBorderType.Thin, Color.Black);
                            _mycell.PutValue(Utility.sDbnull(dtCDHA.Rows[0]["ten_loaidichvu"]));
                            objstyle = _mycell.GetStyle();
                            SetBorderStyle(objstyle, Color.DarkBlue, "Times New Roman", 14, TextAlignmentType.Left, TextAlignmentType.Center, true);
                            _mycell.SetStyle(objstyle);
                            #endregion
                            //Tăng để ghi chi tiết
                            iCurrentRow++;
                            //reset lại để in
                            startcolExcel = 10;
                            //range = worksheet.Cells.CreateRange(5, 2, 1, totaldays);
                            //range.Merge();

                            for (int i = 0; i < dtCDHA.Rows.Count; i++)
                            {
                                worksheet.Cells.InsertRow(i + iCurrentRow);
                                totalColunm = 0;
                                for (int j = startcolExcel; j < dtCDHA.Columns.Count; j++)
                                {
                                    _mycell = worksheet.Cells[i + iCurrentRow, j - startcolExcel];
                                    string sValue = Utility.sDbnull(dtCDHA.Rows[i][j]);
                                    _mycell.PutValue(sValue);
                                    if (j >= 12)//Các cột giá trị. Cột 10 là tên; Cột 11 là đơn vị tính
                                        totalColunm += Utility.Int32Dbnull(sValue, 0);
                                    //  Aspose.Cells.Range _range;
                                    //Setting the line style of the top border
                                    objstyle = _mycell.GetStyle();
                                    objstyle.Font.Name = "Times New Roman";
                                    SetBorderStyle(objstyle, Color.Black, "Times New Roman", 12, TextAlignmentType.Left, TextAlignmentType.Center, false);
                                    if (dtCDHA.Rows.Count == i)
                                    {
                                        objstyle.Font.IsBold = true;
                                        objstyle.Font.Size = 12;
                                    }
                                    objstyle.IsTextWrapped = true;
                                    _mycell.SetStyle(objstyle);
                                    //sheetData.Cells[i + iStartRowContent, j].SetStyle(s3);

                                }
                                //Điền giá trị cho cột tổng cộng sau các cột ngày. 
                                _mycell = worksheet.Cells[i + iCurrentRow, 2 + totaldays];
                                _mycell.PutValue(totalColunm.ToString());
                                objstyle = _mycell.GetStyle();
                                objstyle.Font.IsBold = true;
                                objstyle.Font.Size = 14;
                                SetBorderStyle(objstyle, Color.Black, "Times New Roman", 12, TextAlignmentType.Center, TextAlignmentType.Center, true);
                                _mycell.SetStyle(objstyle);
                                imaxrow++;

                            }
                            iCurrentRow += dtCDHA.Rows.Count;
                        }
                        
                    }
                    
                   
                }
                // range = worksheet.Cells.CreateRange(iCurrentRow, 0, 1,  2);
                //            range.Merge();
                //            range.SetOutlineBorders(CellBorderType.Thin, Color.Black);
                //_mycell = worksheet.Cells[iCurrentRow,0];
                //_mycell.PutValue("Người lập phiếu(hằng ngày ghi tên vào ô:");
                //objstyle = _mycell.GetStyle();
                //SetBorderStyle(objstyle, Color.Black, "Times New Roman", 12, true);
                //range = worksheet.Cells.CreateRange(iCurrentRow, 0, 1, 2);
                //range.Merge();
                //range.SetOutlineBorders(CellBorderType.Thin, Color.Black);
                //_mycell = worksheet.Cells[iCurrentRow, 0];
                //_mycell.PutValue("Ký xác nhận của người bệnh/người nhà:");
                //objstyle = _mycell.GetStyle();
                //SetBorderStyle(objstyle, Color.Black, "Times New Roman", 12, true);
                // worksheet.AutoFitRow();
                worksheet.AutoFitRows();
                //worksheet.AutoFitColumns();


                // worksheet.Cells.ImportDataTable(m_dtExportExcel, true, "A5");

                if (System.IO.File.Exists(saveAsLocation)) File.Delete(saveAsLocation);
                workbook.Save(saveAsLocation);
                System.Diagnostics.Process.Start(saveAsLocation);
                //if (isPrinpreview)
                //{

                //    PrintMyExcelFile(saveAsLocation);
                //}
                //else
                //{
                //    
                //}
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            


        }
        static void PrintMyExcelFile(string sFileName)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

            // Open the Workbook:
            Microsoft.Office.Interop.Excel.Workbook wb = excelApp.Workbooks.Open(sFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            // Get the first worksheet.
            // (Excel uses base 1 indexing, not base 0.)
            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];

            var _with1 = ws.PageSetup;
            // A4 papersize
            _with1.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
            // Landscape orientation
            _with1.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;
            // Fit Sheet on One Page 
            _with1.FitToPagesWide = 1;
            _with1.FitToPagesTall = 1;
            // Normal Margins
            _with1.LeftMargin = excelApp.InchesToPoints(0.7);
            _with1.RightMargin = excelApp.InchesToPoints(0.7);
            _with1.TopMargin = excelApp.InchesToPoints(0.75);
            _with1.BottomMargin = excelApp.InchesToPoints(0.75);
            _with1.HeaderMargin = excelApp.InchesToPoints(0.3);
            _with1.FooterMargin = excelApp.InchesToPoints(0.3);
            object misValue = System.Reflection.Missing.Value;
            // Print out 1 copy to the default printer:

            ws.PrintOut(
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            // Cleanup:
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Marshal.FinalReleaseComObject(ws);

            wb.Close(false, Type.Missing, Type.Missing);
            Marshal.FinalReleaseComObject(wb);

            excelApp.Quit();
            Marshal.FinalReleaseComObject(excelApp);
        }

        private static string ReplaceTieuDe(string colName, ref  bool isBold)
        {
            string replaceName = "";
            switch (colName)
            {
                case "ten_benhnhan":
                    isBold = true;
                    replaceName = "Họ và tên BN";
                    break;
                case "Tuoi":
                    replaceName = "Tuổi";
                    isBold = true;
                    break;
                case "ten_doituong_kcb":
                    replaceName = "Đối tượng";
                    isBold = true;
                    break;
                case "ten_giuong":
                    replaceName = "Giường";
                    isBold = true;
                    break;
                default:
                    replaceName = colName;
                    isBold = false;
                    break;
            }
            return replaceName;
        }
        private string GetCapTion(string colName, ref  bool isBold)
        {
            string replaceName = "";
            switch (colName)
            {
                case "ten_benhnhan":
                    isBold = true;
                    replaceName = "Họ và tên BN";
                    break;
                case "Tuoi":
                    replaceName = "Tuổi";
                    isBold = true;
                    break;
                case "ten_doituong_kcb":
                    replaceName = "Đối tượng";
                    isBold = true;
                    break;
                case "ten_giuong":
                    replaceName = "Giường";
                    isBold = true;
                    break;
                default:
                    replaceName = colName;
                    isBold = false;
                    break;
            }
            return replaceName;
        }
        public static void ExportGridEx(Janus.Windows.GridEX.GridEX gridEx)
        {
            Stream sw = null;
            try
            {
                var sd = new SaveFileDialog { Filter = "Excel File (*.xml)|*.xml" };
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    //sw = new FileStream(sd.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    sw = new FileStream(sd.FileName, FileMode.Create);
                    GridEXExporter grdListExporter = new GridEXExporter();
                    grdListExporter.IncludeExcelProcessingInstruction = true;
                    grdListExporter.IncludeFormatStyle = true;
                    grdListExporter.IncludeHeaders = true;
                    grdListExporter.GridEX = gridEx;
                    grdListExporter.Export(sw);
                    Utility.ShowMsg("Xuất dữ liệu thành công");
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
        }

    }
}
