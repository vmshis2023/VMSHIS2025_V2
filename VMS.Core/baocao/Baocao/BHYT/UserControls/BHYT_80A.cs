using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.C1Excel;
using VNS.Libs;
using VNS.HIS.DAL;
using VNS.Libs.AppUI;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.VisualBasic;
using CrystalDecisions.Shared;
using VNS.HIS.BusRule.Classes;
using VNS.Properties;


namespace VNS.HIS.UI.Baocao
{
    public partial class BHYT_80A : UserControl
    {
        private DataTable _mdtReport;
        public BHYT_80A()
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
            dtpFromDate.ValueChanged+=new EventHandler(dtpFromDate_ValueChanged);
            dtpToDate.ValueChanged+=new EventHandler(dtpToDate_ValueChanged);
            this.KeyDown += new KeyEventHandler(BHYT_80A_KeyDown);
            this.Load += new EventHandler(BHYT_80A_Load);
            optChitiet.CheckedChanged += new EventHandler(optChitiet_CheckedChanged);
            optTonghop.CheckedChanged += new EventHandler(optTonghop_CheckedChanged);
        }

        void BHYT_80A_Load(object sender, EventArgs e)
        {
            radChonNgay.Checked = true;
            radChonNgay.Focus();
            dtpFromDate.Value = dtpToDate.Value = dtpNam.Value = globalVariables.SysDate;
        }
        void optTonghop_CheckedChanged(object sender, EventArgs e)
        {
            baocaO_TIEUDE1.Init("BHYT_80A_TH");
        }

        void optChitiet_CheckedChanged(object sender, EventArgs e)
        {
            baocaO_TIEUDE1.Init("BHYT_80A_CT");
        }
        public void Init()
        {
            dtCreateDate.Value = globalVariables.SysDate;
            dtpFromDate.Value = globalVariables.SysDate;
            dtpToDate.Value = globalVariables.SysDate;
            baocaO_TIEUDE1.Init("BHYT_80A_TH");
            DataBinding.BindData(cboObject, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(new List<string>(){ "BHYT"}),
                                 DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb);
            if (cboObject.Items.Count > 0) cboObject.SelectedIndex = 0;
        }
        void BHYT_80A_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }

        void cmdExcel_Click(object sender, EventArgs e)
        {
            if (optChitiet.Checked)
            {
                #region Xuất Excel chi tiết
                if (rad3360.Checked)
                {
                    gridEXExporter1.GridEX = grdExcel;

                    if (gridEXExporter1.GridEX.RowCount <= 0)
                    {
                        Utility.ShowMsg("Không có dữ liệu", "Thông báo");
                        return;
                    }
                    saveFileDialog1.Filter = @"Excel Files|*.xls;*.xlsx;*.xlsm";
                    saveFileDialog1.FileName = "BaoCaoBHYT-80a";
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
                else
                {
                    DataRow[] _rows = _mdtReport.Select().OrderBy(u => u["DoiTuong"]).ToArray();
                    string reportcode = "BHYT_80A_CT";
                    string duongdan = Utility.GetPathExcel(reportcode);
                    var book = new C1XLBook();
                    book.Load(duongdan);
                    book.DefaultFont = new Font("Time New Roman", 11, FontStyle.Regular);
                    XLSheet sheet = book.Sheets[0];
                    DataTable dt = _rows.CopyToDataTable();
                    int idxRow = 12;
                    int idxColSh = 0;
                    string codintion = string.Format("Từ ngày {0} đến ngày {1}",
                        dtpFromDate.Value.ToString("dd/MM/yyyy"), dtpToDate.Value.ToString("dd/MM/yyyy"));
                    sheet[5, idxColSh].SetValue(codintion, HamDungChung.styleStringCenter(book));
                    int sttLoaithuoc = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            sheet[idxRow, idxColSh].SetValue(Convert.ToString(dt.Rows[i]["DoiTuong"]), HamDungChung.styleStringLeft_Bold(book));
                            sheet[idxRow, idxColSh + 9].SetValue(Convert.ToDecimal(dt.Compute("sum(songay_dieutri)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 10].SetValue(Convert.ToDecimal(dt.Compute("sum(tongcong)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 11].SetValue(Convert.ToDecimal(dt.Compute("sum(xn)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 12].SetValue(Convert.ToDecimal(dt.Compute("sum(cdha)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 13].SetValue(Convert.ToDecimal(dt.Compute("sum(thuoc)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 14].SetValue(Convert.ToDecimal(dt.Compute("sum(mau)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 15].SetValue(Convert.ToDecimal(dt.Compute("sum(pttt)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 16].SetValue(Convert.ToDecimal(dt.Compute("sum(vtyt)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 17].SetValue(Convert.ToDecimal(dt.Compute("sum(t_dvkt_tyle)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 18].SetValue(Convert.ToDecimal(dt.Compute("sum(t_thuoc_tyle)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 19].SetValue(Convert.ToDecimal(dt.Compute("sum(t_vtyt_tyle)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 20].SetValue(Convert.ToDecimal(dt.Compute("sum(tiencong)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 21].SetValue(Convert.ToDecimal(dt.Compute("sum(t_giuong)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 22].SetValue(Convert.ToDecimal(dt.Compute("sum(VanChuyen)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 23].SetValue(Convert.ToDecimal(dt.Compute("sum(bnct)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 24].SetValue(Convert.ToDecimal(dt.Compute("sum(bhct)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 25].SetValue(Convert.ToDecimal(dt.Compute("sum(qds)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            idxRow = idxRow + 1;
                            sheet[idxRow, idxColSh].SetValue(string.Format("{0}", Convert.ToString(dt.Rows[i]["ten_dung_tuyen"])),
                                HamDungChung.styleStringLeft_Bold(book));
                            sttLoaithuoc = sttLoaithuoc + 1;
                            idxRow = idxRow + 1;
                        }
                        else
                        {
                            if (dt.Rows[i]["DoiTuong"].ToString() != dt.Rows[i - 1]["DoiTuong"].ToString())
                            {
                                sttLoaithuoc = 1;
                                sheet[idxRow, idxColSh].SetValue(Convert.ToString(dt.Rows[i]["DoiTuong"]), HamDungChung.styleStringLeft_Bold(book));
                                // idxRow = idxRow + 1;
                                sheet[idxRow, idxColSh + 9].SetValue(Convert.ToDecimal(dt.Compute("sum(songay_dieutri)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                                sheet[idxRow, idxColSh + 10].SetValue(Convert.ToDecimal(dt.Compute("sum(tongcong)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                                sheet[idxRow, idxColSh + 11].SetValue(Convert.ToDecimal(dt.Compute("sum(xn)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                                sheet[idxRow, idxColSh + 12].SetValue(Convert.ToDecimal(dt.Compute("sum(cdha)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                                sheet[idxRow, idxColSh + 13].SetValue(Convert.ToDecimal(dt.Compute("sum(thuoc)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                                sheet[idxRow, idxColSh + 14].SetValue(Convert.ToDecimal(dt.Compute("sum(mau)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                                sheet[idxRow, idxColSh + 15].SetValue(Convert.ToDecimal(dt.Compute("sum(pttt)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                                sheet[idxRow, idxColSh + 16].SetValue(Convert.ToDecimal(dt.Compute("sum(vtyt)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                                sheet[idxRow, idxColSh + 17].SetValue(Convert.ToDecimal(dt.Compute("sum(t_dvkt_tyle)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                                sheet[idxRow, idxColSh + 18].SetValue(Convert.ToDecimal(dt.Compute("sum(t_thuoc_tyle)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                                sheet[idxRow, idxColSh + 19].SetValue(Convert.ToDecimal(dt.Compute("sum(t_vtyt_tyle)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                                sheet[idxRow, idxColSh + 20].SetValue(Convert.ToDecimal(dt.Compute("sum(tiencong)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                                sheet[idxRow, idxColSh + 21].SetValue(Convert.ToDecimal(dt.Compute("sum(t_giuong)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                                sheet[idxRow, idxColSh + 22].SetValue(Convert.ToDecimal(dt.Compute("sum(VanChuyen)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                                sheet[idxRow, idxColSh + 23].SetValue(Convert.ToDecimal(dt.Compute("sum(bnct)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                                sheet[idxRow, idxColSh + 24].SetValue(Convert.ToDecimal(dt.Compute("sum(bhct)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                                sheet[idxRow, idxColSh + 25].SetValue(Convert.ToDecimal(dt.Compute("sum(qds)", "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                                idxRow = idxRow + 1;
                            }
                            if (dt.Rows[i]["ten_dung_tuyen"].ToString() != dt.Rows[i - 1]["ten_dung_tuyen"].ToString())
                            {
                                sheet[idxRow, idxColSh].SetValue(
                                    string.Format("{0}", Convert.ToString(dt.Rows[i]["ten_dung_tuyen"])),
                                    HamDungChung.styleStringLeft_Bold(book));
                                sttLoaithuoc = sttLoaithuoc + 1;
                                idxRow = idxRow + 1;
                            }
                        }

                        sheet[idxRow, idxColSh].SetValue(Convert.ToString((i + 1).ToString()), HamDungChung.styleStringCenter(book));
                        sheet[idxRow, idxColSh + 1].SetValue(Convert.ToString(dt.Rows[i]["ten_benhnhan"]), HamDungChung.styleStringCenter(book));
                        sheet[idxRow, idxColSh + 2].SetValue(Convert.ToString(dt.Rows[i]["nam_sinh_nam"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 3].SetValue(Convert.ToString(dt.Rows[i]["nam_sinh_nu"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 4].SetValue(Convert.ToString(dt.Rows[i]["mathe_bhyt"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 5].SetValue(Convert.ToString(dt.Rows[i]["kcb"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 6].SetValue(Convert.ToString(dt.Rows[i]["mabenh_chinh"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 7].SetValue(Convert.ToString(dt.Rows[i]["ngay_tiepdon"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 8].SetValue(Convert.ToString(dt.Rows[i]["ngay_ketthuc"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 9].SetValue(Convert.ToDecimal(dt.Rows[i]["songay_dieutri"]), HamDungChung.styleNumber(book));
                        sheet[idxRow, idxColSh + 10].SetValue(Convert.ToDecimal(dt.Rows[i]["tongcong"]), HamDungChung.styleNumber_Right(book));
                        sheet[idxRow, idxColSh + 11].SetValue(Convert.ToDecimal(dt.Rows[i]["xn"]), HamDungChung.styleNumber_Right(book));
                        sheet[idxRow, idxColSh + 12].SetValue(Convert.ToDecimal(dt.Rows[i]["cdha"]), HamDungChung.styleNumber_Right(book));
                        sheet[idxRow, idxColSh + 13].SetValue(Convert.ToDecimal(dt.Rows[i]["thuoc"]), HamDungChung.styleNumber_Right(book));
                        sheet[idxRow, idxColSh + 14].SetValue(Convert.ToDecimal(dt.Rows[i]["mau"]), HamDungChung.styleNumber_Right(book));
                        sheet[idxRow, idxColSh + 15].SetValue(Convert.ToDecimal(dt.Rows[i]["pttt"]), HamDungChung.styleNumber_Right(book));
                        sheet[idxRow, idxColSh + 16].SetValue(Convert.ToDecimal(dt.Rows[i]["vtyt"]), HamDungChung.styleNumber_Right(book));
                        sheet[idxRow, idxColSh + 17].SetValue(Convert.ToDecimal(dt.Rows[i]["t_dvkt_tyle"]), HamDungChung.styleNumber_Right(book));
                        sheet[idxRow, idxColSh + 18].SetValue(Convert.ToDecimal(dt.Rows[i]["t_thuoc_tyle"]), HamDungChung.styleNumber_Right(book));
                        sheet[idxRow, idxColSh + 19].SetValue(Convert.ToDecimal(dt.Rows[i]["t_vtyt_tyle"]), HamDungChung.styleNumber_Right(book));
                        sheet[idxRow, idxColSh + 20].SetValue(Convert.ToDecimal(dt.Rows[i]["tiencong"]), HamDungChung.styleNumber_Right(book));
                        sheet[idxRow, idxColSh + 21].SetValue(Convert.ToDecimal(dt.Rows[i]["t_giuong"]), HamDungChung.styleNumber_Right(book));
                        sheet[idxRow, idxColSh + 22].SetValue(Convert.ToDecimal(dt.Rows[i]["VanChuyen"]), HamDungChung.styleNumber_Right(book));
                        sheet[idxRow, idxColSh + 23].SetValue(Convert.ToDecimal(dt.Rows[i]["bnct"]), HamDungChung.styleNumber_Right(book));
                        sheet[idxRow, idxColSh + 24].SetValue(Convert.ToDecimal(dt.Rows[i]["bhct"]), HamDungChung.styleNumber_Right(book));
                        sheet[idxRow, idxColSh + 25].SetValue(Convert.ToDecimal(dt.Rows[i]["qds"]), HamDungChung.styleNumber_Right(book));
                        idxRow = idxRow + 1;
                    }
                    sheet[idxRow, idxColSh].SetValue(Convert.ToString("TỔNG CỘNG: "), HamDungChung.styleStringLeft_Bold(book));
                    sheet[idxRow, idxColSh + 9].SetValue(Convert.ToDecimal(dt.Compute("sum(songay_dieutri)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                    sheet[idxRow, idxColSh + 10].SetValue(Convert.ToDecimal(dt.Compute("sum(tongcong)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                    sheet[idxRow, idxColSh + 11].SetValue(Convert.ToDecimal(dt.Compute("sum(xn)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                    sheet[idxRow, idxColSh + 12].SetValue(Convert.ToDecimal(dt.Compute("sum(cdha)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                    sheet[idxRow, idxColSh + 13].SetValue(Convert.ToDecimal(dt.Compute("sum(thuoc)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                    sheet[idxRow, idxColSh + 14].SetValue(Convert.ToDecimal(dt.Compute("sum(mau)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                    sheet[idxRow, idxColSh + 15].SetValue(Convert.ToDecimal(dt.Compute("sum(pttt)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                    sheet[idxRow, idxColSh + 16].SetValue(Convert.ToDecimal(dt.Compute("sum(vtyt)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                    sheet[idxRow, idxColSh + 17].SetValue(Convert.ToDecimal(dt.Compute("sum(t_dvkt_tyle)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                    sheet[idxRow, idxColSh + 18].SetValue(Convert.ToDecimal(dt.Compute("sum(t_thuoc_tyle)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                    sheet[idxRow, idxColSh + 19].SetValue(Convert.ToDecimal(dt.Compute("sum(t_vtyt_tyle)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                    sheet[idxRow, idxColSh + 20].SetValue(Convert.ToDecimal(dt.Compute("sum(tiencong)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                    sheet[idxRow, idxColSh + 21].SetValue(Convert.ToDecimal(dt.Compute("sum(t_giuong)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                    sheet[idxRow, idxColSh + 22].SetValue(Convert.ToDecimal(dt.Compute("sum(VanChuyen)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                    sheet[idxRow, idxColSh + 23].SetValue(Convert.ToDecimal(dt.Compute("sum(bnct)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                    sheet[idxRow, idxColSh + 24].SetValue(Convert.ToDecimal(dt.Compute("sum(bhct)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                    sheet[idxRow, idxColSh + 25].SetValue(Convert.ToDecimal(dt.Compute("sum(qds)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                    idxRow = idxRow + 1;
                    string getdate = string.Format("Ngày {0} tháng {1} năm {2}", dtCreateDate.Value.Day,
                       dtCreateDate.Value.Month, dtCreateDate.Value.Year);
                    sheet[idxRow + 2, 23].SetValue(getdate, HamDungChung.styleStringCenter_UnBorder(book));

                    sheet[idxRow + 3, 2].SetValue("NGƯỜI LẬP BẢNG", HamDungChung.styleStringCenter_UnBorder(book));
                    sheet[idxRow + 3, 9].SetValue("PHÒNG KẾ HOẠCH TỔNG HỢP",
                    HamDungChung.styleStringCenter_UnBorder(book));
                    sheet[idxRow + 3, 15].SetValue("PHÒNG TÀI CHÍNH KẾ TOÁN",
                        HamDungChung.styleStringCenter_UnBorder(book));
                    sheet[idxRow + 3, 23].SetValue("GIÁM ĐỐC BỆNH VIỆN", HamDungChung.styleStringCenter_UnBorder(book));

                    // vị trí dòng dữ liệu của table tiếp theo, vị trí cột bắt đầu t? 0                             
                    string getTime = Convert.ToString(DateTime.Now.ToString("yyyyMMddhhmmss"));
                    string pathDirectory = AppDomain.CurrentDomain.BaseDirectory + "TemplateExcel\\ExportExcel\\";
                    if (!Directory.Exists(pathDirectory))
                    {
                        Directory.CreateDirectory(pathDirectory);
                    }

                    book.Save(AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExcel\\ExportExcel\\" + reportcode +
                              getTime + ".xls");
                    Process.Start(
                        new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExcel\\ExportExcel\\" +
                                             reportcode + getTime + ".xls"));
                }
                #endregion 
            }
            else
            {
                #region Xuất Excel tổng hợp
                DataRow[] _rows = _mdtReport.Select().OrderBy(u => u["DoiTuong"]).ToArray();
                string reportcode = "BHYT_80A_TH";
                string duongdan = Utility.GetPathExcel(reportcode);
                var book = new C1XLBook();
                book.Load(duongdan);
                book.DefaultFont = new Font("Time New Roman", 11, FontStyle.Regular);
                XLSheet sheet = book.Sheets[0];
                DataTable dt = _rows.CopyToDataTable();
                int idxRow = 12;
                int idxColSh = 0;
                string codintion = string.Format("Từ ngày {0} đến ngày {1}",
                    dtpFromDate.Value.ToString("dd/MM/yyyy"), dtpToDate.Value.ToString("dd/MM/yyyy"));
                sheet[5, idxColSh].SetValue(codintion, HamDungChung.styleStringCenter(book));
                int sttLoaithuoc = 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        sheet[idxRow, idxColSh].SetValue(Convert.ToString(dt.Rows[i]["DoiTuong"]),
                            HamDungChung.styleStringLeft_Bold(book));
                        sheet[idxRow, idxColSh + 9].SetValue(
                            Convert.ToDecimal(dt.Compute("sum(songay_dieutri)",
                                "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                            HamDungChung.styleDecimalBoldAllBorder_Money(book));
                        sheet[idxRow, idxColSh + 10].SetValue(
                            Convert.ToDecimal(dt.Compute("sum(tongcong)",
                                "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                            HamDungChung.styleDecimalBoldAllBorder_Money(book));
                        sheet[idxRow, idxColSh + 11].SetValue(
                            Convert.ToDecimal(dt.Compute("sum(xn)",
                                "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                            HamDungChung.styleDecimalBoldAllBorder_Money(book));
                        sheet[idxRow, idxColSh + 12].SetValue(
                            Convert.ToDecimal(dt.Compute("sum(cdha)",
                                "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                            HamDungChung.styleDecimalBoldAllBorder_Money(book));
                        sheet[idxRow, idxColSh + 13].SetValue(
                            Convert.ToDecimal(dt.Compute("sum(thuoc)",
                                "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                            HamDungChung.styleDecimalBoldAllBorder_Money(book));
                        sheet[idxRow, idxColSh + 14].SetValue(
                            Convert.ToDecimal(dt.Compute("sum(mau)",
                                "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                            HamDungChung.styleDecimalBoldAllBorder_Money(book));
                        sheet[idxRow, idxColSh + 15].SetValue(
                            Convert.ToDecimal(dt.Compute("sum(pttt)",
                                "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                            HamDungChung.styleDecimalBoldAllBorder_Money(book));
                        sheet[idxRow, idxColSh + 16].SetValue(
                            Convert.ToDecimal(dt.Compute("sum(vtyt)",
                                "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                            HamDungChung.styleDecimalBoldAllBorder_Money(book));
                        sheet[idxRow, idxColSh + 17].SetValue(
                            Convert.ToDecimal(dt.Compute("sum(t_dvkt_tyle)",
                                "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                            HamDungChung.styleDecimalBoldAllBorder_Money(book));
                        sheet[idxRow, idxColSh + 18].SetValue(
                            Convert.ToDecimal(dt.Compute("sum(t_thuoc_tyle)",
                                "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                            HamDungChung.styleDecimalBoldAllBorder_Money(book));
                        sheet[idxRow, idxColSh + 19].SetValue(
                            Convert.ToDecimal(dt.Compute("sum(t_vtyt_tyle)",
                                "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                            HamDungChung.styleDecimalBoldAllBorder_Money(book));
                        sheet[idxRow, idxColSh + 20].SetValue(
                            Convert.ToDecimal(dt.Compute("sum(tiencong)",
                                "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                            HamDungChung.styleDecimalBoldAllBorder_Money(book));
                        sheet[idxRow, idxColSh + 21].SetValue(
                            Convert.ToDecimal(dt.Compute("sum(t_giuong)",
                                "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                            HamDungChung.styleDecimalBoldAllBorder_Money(book));
                        sheet[idxRow, idxColSh + 22].SetValue(
                            Convert.ToDecimal(dt.Compute("sum(VanChuyen)",
                                "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                            HamDungChung.styleDecimalBoldAllBorder_Money(book));
                        sheet[idxRow, idxColSh + 23].SetValue(
                            Convert.ToDecimal(dt.Compute("sum(bnct)",
                                "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                            HamDungChung.styleDecimalBoldAllBorder_Money(book));
                        sheet[idxRow, idxColSh + 24].SetValue(
                            Convert.ToDecimal(dt.Compute("sum(bhct)",
                                "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                            HamDungChung.styleDecimalBoldAllBorder_Money(book));
                        sheet[idxRow, idxColSh + 25].SetValue(
                            Convert.ToDecimal(dt.Compute("sum(qds)",
                                "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                            HamDungChung.styleDecimalBoldAllBorder_Money(book));
                        idxRow = idxRow + 1;
                        //sheet[idxRow, idxColSh].SetValue(string.Format("{0}", Convert.ToString(dt.Rows[i]["ten_dung_tuyen"])),
                        //    HamDungChung.styleStringLeft_Bold(book));
                        //sttLoaithuoc = sttLoaithuoc + 1;
                        //idxRow = idxRow + 1;
                    }
                    else
                    {
                        if (dt.Rows[i]["DoiTuong"].ToString() != dt.Rows[i - 1]["DoiTuong"].ToString())
                        {
                            sttLoaithuoc = 1;
                            sheet[idxRow, idxColSh].SetValue(Convert.ToString(dt.Rows[i]["DoiTuong"]),
                                HamDungChung.styleStringLeft_Bold(book));
                            // idxRow = idxRow + 1;
                            sheet[idxRow, idxColSh + 9].SetValue(
                                Convert.ToDecimal(dt.Compute("sum(songay_dieutri)",
                                    "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                                HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 10].SetValue( Convert.ToDecimal(dt.Compute("sum(tongcong)","Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 11].SetValue(
                                Convert.ToDecimal(dt.Compute("sum(xn)",
                                    "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                                HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 12].SetValue(
                                Convert.ToDecimal(dt.Compute("sum(cdha)",
                                    "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                                HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 13].SetValue(
                                Convert.ToDecimal(dt.Compute("sum(thuoc)",
                                    "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                                HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 14].SetValue(
                                Convert.ToDecimal(dt.Compute("sum(mau)",
                                    "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                                HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 15].SetValue(
                                Convert.ToDecimal(dt.Compute("sum(pttt)",
                                    "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                                HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 16].SetValue(
                                Convert.ToDecimal(dt.Compute("sum(vtyt)",
                                    "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                                HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 17].SetValue(
                                Convert.ToDecimal(dt.Compute("sum(t_dvkt_tyle)",
                                    "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                                HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 18].SetValue(
                                Convert.ToDecimal(dt.Compute("sum(t_thuoc_tyle)",
                                    "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                                HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 19].SetValue(
                                Convert.ToDecimal(dt.Compute("sum(t_vtyt_tyle)",
                                    "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                                HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 20].SetValue(
                                Convert.ToDecimal(dt.Compute("sum(tiencong)",
                                    "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                                HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 21].SetValue(
                                Convert.ToDecimal(dt.Compute("sum(t_giuong)",
                                    "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                                HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 22].SetValue(
                                Convert.ToDecimal(dt.Compute("sum(VanChuyen)",
                                    "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                                HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 23].SetValue(
                                Convert.ToDecimal(dt.Compute("sum(bnct)",
                                    "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                                HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 24].SetValue(
                                Convert.ToDecimal(dt.Compute("sum(bhct)",
                                    "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                                HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 25].SetValue(
                                Convert.ToDecimal(dt.Compute("sum(qds)",
                                    "Doituong = '" + Convert.ToString(dt.Rows[i]["DoiTuong"]) + "' ")),
                                HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            idxRow = idxRow + 1;
                        }
                    }
                }
                sheet[idxRow, idxColSh].SetValue(Convert.ToString("TỔNG CỘNG: "), HamDungChung.styleStringLeft_Bold(book));
                sheet[idxRow, idxColSh + 9].SetValue(Convert.ToDecimal(dt.Compute("sum(songay_dieutri)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                sheet[idxRow, idxColSh + 10].SetValue(Convert.ToDecimal(dt.Compute("sum(tongcong)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                sheet[idxRow, idxColSh + 11].SetValue(Convert.ToDecimal(dt.Compute("sum(xn)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                sheet[idxRow, idxColSh + 12].SetValue(Convert.ToDecimal(dt.Compute("sum(cdha)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                sheet[idxRow, idxColSh + 13].SetValue(Convert.ToDecimal(dt.Compute("sum(thuoc)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                sheet[idxRow, idxColSh + 14].SetValue(Convert.ToDecimal(dt.Compute("sum(mau)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                sheet[idxRow, idxColSh + 15].SetValue(Convert.ToDecimal(dt.Compute("sum(pttt)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                sheet[idxRow, idxColSh + 16].SetValue(Convert.ToDecimal(dt.Compute("sum(vtyt)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                sheet[idxRow, idxColSh + 17].SetValue(Convert.ToDecimal(dt.Compute("sum(t_dvkt_tyle)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                sheet[idxRow, idxColSh + 18].SetValue(Convert.ToDecimal(dt.Compute("sum(t_thuoc_tyle)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                sheet[idxRow, idxColSh + 19].SetValue(Convert.ToDecimal(dt.Compute("sum(t_vtyt_tyle)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                sheet[idxRow, idxColSh + 20].SetValue(Convert.ToDecimal(dt.Compute("sum(tiencong)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                sheet[idxRow, idxColSh + 21].SetValue(Convert.ToDecimal(dt.Compute("sum(t_giuong)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                sheet[idxRow, idxColSh + 22].SetValue(Convert.ToDecimal(dt.Compute("sum(VanChuyen)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                sheet[idxRow, idxColSh + 23].SetValue(Convert.ToDecimal(dt.Compute("sum(bnct)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                sheet[idxRow, idxColSh + 24].SetValue(Convert.ToDecimal(dt.Compute("sum(bhct)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                sheet[idxRow, idxColSh + 25].SetValue(Convert.ToDecimal(dt.Compute("sum(qds)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                idxRow = idxRow + 1;
                    string getdate = string.Format("Ngày {0} tháng {1} năm {2}", dtCreateDate.Value.Day,
                      dtCreateDate.Value.Month, dtCreateDate.Value.Year);
                    sheet[idxRow + 2, 23].SetValue(getdate, HamDungChung.styleStringCenter_UnBorder(book));

                    sheet[idxRow + 3, 2].SetValue("NGƯỜI LẬP BẢNG", HamDungChung.styleStringCenter_UnBorder(book));
                    sheet[idxRow + 3, 9].SetValue("PHÒNG KẾ HOẠCH TỔNG HỢP",
                    HamDungChung.styleStringCenter_UnBorder(book));
                    sheet[idxRow + 3, 15].SetValue("PHÒNG TÀI CHÍNH KẾ TOÁN",
                        HamDungChung.styleStringCenter_UnBorder(book));
                    sheet[idxRow + 3, 23].SetValue("GIÁM ĐỐC BỆNH VIỆN", HamDungChung.styleStringCenter_UnBorder(book));

                    // vị trí dòng dữ liệu của table tiếp theo, vị trí cột bắt đầu t? 0                             
                    string getTime = Convert.ToString(DateTime.Now.ToString("yyyyMMddhhmmss"));
                    string pathDirectory = AppDomain.CurrentDomain.BaseDirectory + "TemplateExcel\\ExportExcel\\";
                    if (!Directory.Exists(pathDirectory))
                    {
                        Directory.CreateDirectory(pathDirectory);
                    }

                    book.Save(AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExcel\\ExportExcel\\" + reportcode +
                              getTime + ".xls");
                    Process.Start(
                        new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExcel\\ExportExcel\\" +
                                             reportcode + getTime + ".xls"));
                
                #endregion 
            }
            
               
        }

        void cmdPreview_Click(object sender, EventArgs e)
        {
            PrintReport(true);
        }

        void cmdPrint_Click(object sender, EventArgs e)
        {
            PrintReport(false);
        }
        void PrintReport(bool view)
        {
            try
            {
                if (_mdtReport != null)
                {
                    if (_mdtReport.Rows.Count <= 0 || _mdtReport.Columns.Count <= 0)
                        return;
                    //Báo cáo chi tiết
                     string tieude="", reportname = "";
                    string reportCode = "";
                    THU_VIEN_CHUNG.CreateXML(_mdtReport, "BHYT_80A_CT.XML");
                    if (optChitiet.Checked)
                    {
                        reportCode = "BHYT_80A_CT";
                        ReportDocument crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
                        if (crpt == null) return;

                        var objForm =
                            new frmPrintPreview(
                               baocaO_TIEUDE1.txtTieuDe.Text, crpt,
                                true, _mdtReport.Rows.Count > 0);
                        Utility.UpdateLogotoDatatable(ref _mdtReport);
                        crpt.SetDataSource(_mdtReport);
                       
                        objForm.mv_sReportFileName = Path.GetFileName(reportname);
                        objForm.mv_sReportCode = reportCode;
                        Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                        Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
                        Utility.SetParameterValue(crpt,"Telephone", globalVariables.Branch_Phone);
                        Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                        Utility.SetParameterValue(crpt,"FromDateToDate", dtpFromDate.Value.ToString("dd/MM/yyyy") + " ĐẾN NGÀY " +
                                               dtpToDate.Value.ToString("dd/MM/yyyy"));
                        Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                        Utility.SetParameterValue(crpt,"NTN", Utility.FormatDateTimeWithThanhPho(dtCreateDate.Value));
                        Utility.SetParameterValue(crpt,"TongTien", ChuyenDoiSoThanhChu());
                        Utility.SetParameterValue(crpt, "txtTrinhky",
                                                           Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                              globalVariables.SysDate));
                        objForm.crptViewer.ReportSource = crpt;
                        if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, view))
                        {
                            objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 1);
                            objForm.ShowDialog();
                        }
                        else
                        {
                            crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                            crpt.PrintToPrinter(1, false, 0, 0);
                        }

                        if(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_80A_CT_THEMCOT_MALANKHAM",false)=="1")
                        {
                            ReportDocument crpt1 = Utility.GetReport("BHYT_80A_CT_THEMCOT_MALANKHAM", ref tieude, ref reportname);
                            if (crpt1 == null) return;

                            frmPrintPreview objForm1 =
                                new frmPrintPreview(
                                   baocaO_TIEUDE1.txtTieuDe.Text, crpt1,
                                    true, _mdtReport.Rows.Count <= 0 ? false : true);
                            Utility.UpdateLogotoDatatable(ref _mdtReport);
                            crpt1.SetDataSource(_mdtReport);

                            objForm1.mv_sReportFileName = Path.GetFileName(reportname);
                            objForm1.mv_sReportCode = "BHYT_80A_CT";
                            Utility.SetParameterValue(crpt1, "ParentBranchName", globalVariables.ParentBranch_Name);
                            Utility.SetParameterValue(crpt1, "Address", globalVariables.Branch_Address);
                            Utility.SetParameterValue(crpt1, "Telephone", globalVariables.Branch_Phone);
                            Utility.SetParameterValue(crpt1, "BranchName", globalVariables.Branch_Name);
                            Utility.SetParameterValue(crpt1, "FromDateToDate", dtpFromDate.Value.ToString("dd/MM/yyyy") + " ĐẾN NGÀY " +
                                                   dtpToDate.Value.ToString("dd/MM/yyyy"));
                            Utility.SetParameterValue(crpt1, "sTitleReport", tieude);
                            Utility.SetParameterValue(crpt1, "NTN", Utility.FormatDateTimeWithThanhPho(dtCreateDate.Value));
                            Utility.SetParameterValue(crpt1, "TongTien", ChuyenDoiSoThanhChu());
                            Utility.SetParameterValue(crpt1, "txtTrinhky",
                                                               Utility.getTrinhky(objForm1.mv_sReportFileName,
                                                                                  globalVariables.SysDate));
                            objForm1.crptViewer.ReportSource = crpt1;
                            if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, view))
                            {
                                objForm1.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 1);
                                objForm1.ShowDialog();
                            }
                            else
                            {
                                crpt1.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                                crpt1.PrintToPrinter(1, false, 0, 0);
                            }
                            Utility.FreeMemory(crpt1);
                        }


                        Utility.FreeMemory(crpt);

                    }
                    //Báo cáo tổng hợp
                    else if (optTonghop.Checked)
                    {
                        reportCode = "BHYT_80A_TH";
                        ReportDocument crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
                        if (crpt == null) return;
                        frmPrintPreview objForm =
                            new frmPrintPreview(
                                baocaO_TIEUDE1.txtTieuDe.Text, crpt, true, _mdtReport.Rows.Count <= 0 ? false : true);
                        Utility.UpdateLogotoDatatable(ref _mdtReport);
                        crpt.SetDataSource(_mdtReport);
                       
                        objForm.mv_sReportFileName = Path.GetFileName(reportname);
                        objForm.mv_sReportCode = reportCode;
                        Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                        Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
                        Utility.SetParameterValue(crpt,"Telephone", globalVariables.Branch_Phone);
                        Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                        Utility.SetParameterValue(crpt,"FromDateToDate", dtpFromDate.Value.ToString("dd/MM/yyyy") + " ĐẾN NGÀY " +
                                               dtpToDate.Value.ToString("dd/MM/yyyy"));
                        Utility.SetParameterValue(crpt,"sTitleReport",tieude);
                        Utility.SetParameterValue(crpt,"NTN", Utility.FormatDateTimeWithThanhPho(dtCreateDate.Value));
                        Utility.SetParameterValue(crpt,"TongTien", ChuyenDoiSoThanhChu());
                        Utility.SetParameterValue(crpt, "txtTrinhky",
                                                           Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                              globalVariables.SysDate));
                        objForm.crptViewer.ReportSource = crpt;
                        if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, view))
                        {
                            objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 1);
                            objForm.ShowDialog();
                        }
                        else
                        {
                            crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                            crpt.PrintToPrinter(1, false, 0, 0);
                        }
                        Utility.FreeMemory(crpt);
                    }
                    else
                    {
                        MessageBox.Show(@"Chọn lựa không chính xác", @"THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        string ChuyenDoiSoThanhChu()
        {
            decimal Tongtien = 0;

            var query = from tongtien in _mdtReport.AsEnumerable()
                        let x =
                            Utility.DecimaltoDbnull(tongtien["BHCT"], 0) + Utility.DecimaltoDbnull(tongtien["QDS"], 0)
                        select x;

            Tongtien = query.Sum();
            return new MoneyByLetter().sMoneyToLetter(Tongtien.ToString());
        }
        void cmdSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }
        void SearchData()
        {
            try
            {
               _mdtReport =  new BAOCAO_BHYT().BHYT_80A(dtpFromDate.Value, dtpToDate.Value, "BHYT");
                //THU_VIEN_CHUNG.CreateXML(mdtReport, @"Xml4Reports\BHYT_80A.xml");
                ModifyCommands();
                ProcessData();
                grdList.DataSource = _mdtReport;
                grdExcel.DataSource = _mdtReport;
                grExcel9324.DataSource = _mdtReport;
            }
            catch (Exception ex)
            {
                 Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }
        void ModifyCommands()
        {
            cmdPrint.Enabled = _mdtReport != null && _mdtReport.Rows.Count > 0;
            cmdPreview.Enabled = cmdPrint.Enabled;
            cmdExcel.Enabled = cmdPrint.Enabled;
        }
        void ProcessData()
        {
            try
            {
                Utility.ResetProgressBar(prgBar, _mdtReport.Rows.Count, true);
                if (!_mdtReport.Columns.Contains("NamQT")) _mdtReport.Columns.Add("NamQT", typeof(string), dtpToDate.Value.Year.ToString());
                if (!_mdtReport.Columns.Contains("ThangQT")) _mdtReport.Columns.Add("ThangQT", typeof(string), dtpToDate.Value.Month.ToString());
                if (!_mdtReport.Columns.Contains("LoaiKCB")) _mdtReport.Columns.Add("LoaiKCB", typeof(string));
                if (!_mdtReport.Columns.Contains("NoiTT")) _mdtReport.Columns.Add("NoiTT", typeof(string));
                if (!_mdtReport.Columns.Contains("DoiTuong")) _mdtReport.Columns.Add("DoiTuong", typeof(string));
                if (!_mdtReport.Columns.Contains("Tuyen")) _mdtReport.Columns.Add("Tuyen", typeof(int));
                foreach (DataRow row in _mdtReport.Rows)
                {
                    if (row["dung_tuyen"].ToString() == "1")
                    {
                        row["ten_dung_tuyen"] = "I. Đúng Tuyến";
                    }
                    else if (row["dung_tuyen"].ToString() == "0")
                    {
                        row["ten_dung_tuyen"] = "II. Trái Tuyến";
                    }
                    else
                    {
                        row["ten_dung_tuyen"] = "III. Dịch Vụ";
                    }
                    if (row[KcbLuotkham.Columns.MaNoicapBhyt].ToString() == globalVariables.gv_strNoicapBHYT)
                    {
                        if (row[KcbLuotkham.Columns.MaKcbbd].ToString() == globalVariables.gv_strNoiDKKCBBD)
                        {
                            row["DoiTuong"] = globalVariables.GvStrTendoituongNoiTinhKcbbd;
                            row["Tuyen"] = 1;
                        }
                        else
                        {
                            row["DoiTuong"] = globalVariables.GvStrTendoituongNoitinhKhongKcbbd;
                            row["Tuyen"] = 2;
                        }
                    }
                    else
                    {
                        row["DoiTuong"] = globalVariables.GvStrTendoituongNgoaitinh;
                        row["Tuyen"] = 3;
                    }
                    if (string.IsNullOrEmpty(row["ten_nhombhyt"].ToString()))
                    {
                        row["ten_nhombhyt"] = "Dịch vụ";
                        row["Tuyen"] = 0;
                    }
                    row["NoiTT"] = "CSKCB";
                    row["LoaiKCB"] = "Ngoại";
                    UIAction.SetValue4Prg(prgBar, 1);
                }
                _mdtReport.AcceptChanges();
            }
            catch(Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
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

        private void radChonQuy_CheckedChanged(object sender, EventArgs e)
        {
            if (radChonQuy.Checked)
            {
                var fromdate = new DateTime();
                var todate = new DateTime();
                switch (Utility.sDbnull(cboQuy.SelectedValue))
                {
                    case "1":
                        fromdate = new DateTime(dtpNam.Value.Year, 1, 1);
                        todate = new DateTime(dtpNam.Value.Year, 3, 31);
                        break;
                    case "2":
                        fromdate = new DateTime(dtpNam.Value.Year, 4, 1);
                        todate = new DateTime(dtpNam.Value.Year, 6, 30);
                        break;
                    case "3":
                        fromdate = new DateTime(dtpNam.Value.Year, 7, 1);
                        todate = new DateTime(dtpNam.Value.Year, 9, 30);
                        break;
                    case "4":
                        fromdate = new DateTime(dtpNam.Value.Year, 10, 1);
                        todate = new DateTime(dtpNam.Value.Year, 12, 31);
                        break;
                    default:
                        fromdate = new DateTime(dtpNam.Value.Year, 1, 1);
                        todate = new DateTime(dtpNam.Value.Year, 12, 31);
                        break;
                }
                dtpFromDate.Value = fromdate;
                dtpToDate.Value = todate;
            }
        }

        private void radChonThang_CheckedChanged(object sender, EventArgs e)
        {
            if (radChonThang.Checked)
            {
                cboThang.SelectedIndex = 0;
                var myDate = cboThang.SelectedValue;
                var startOfMonth = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(myDate), 1);
                dtpFromDate.Value = startOfMonth;
                var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
                dtpToDate.Value = endOfMonth;
            }
        }

        private void radChonNam_CheckedChanged(object sender, EventArgs e)
        {
            if (radChonNam.Checked)
            {
                var myDate = dtpNam.Value;
                var startOfMonth = new DateTime(dtpNam.Value.Year, 1, 1);
                dtpFromDate.Value = startOfMonth;
                var endOfMonth = new DateTime(dtpNam.Value.Year, 12, 31);
                dtpToDate.Value = endOfMonth;
            }
          
        }

        private void cboThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radChonThang.Checked)
            {
                var myDate = cboThang.SelectedValue;
                var startOfMonth = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(myDate), 1);
                dtpFromDate.Value = startOfMonth;
                var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
                dtpToDate.Value = endOfMonth;
            }
        }

        private void cboQuy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radChonQuy.Checked)
            {
                var fromdate = new DateTime();
                var todate = new DateTime();
                switch (Utility.sDbnull(cboQuy.SelectedValue))
                {
                    case "1":
                        fromdate = new DateTime(dtpNam.Value.Year, 1, 1);
                        todate = new DateTime(dtpNam.Value.Year, 3, 31);
                        break;
                    case "2":
                        fromdate = new DateTime(dtpNam.Value.Year, 4, 1);
                        todate = new DateTime(dtpNam.Value.Year, 6, 30);
                        break;
                    case "3":
                        fromdate = new DateTime(dtpNam.Value.Year, 7, 1);
                        todate = new DateTime(dtpNam.Value.Year, 9, 30);
                        break;
                    case "4":
                        fromdate = new DateTime(dtpNam.Value.Year, 10, 1);
                        todate = new DateTime(dtpNam.Value.Year, 12, 31);
                        break;
                    default:
                        fromdate = new DateTime(dtpNam.Value.Year, 1, 1);
                        todate = new DateTime(dtpNam.Value.Year, 12, 31);
                        break;
                }
                dtpFromDate.Value = fromdate;
                dtpToDate.Value = todate;
            }
        }

        private void dtpNam_ValueChanged(object sender, EventArgs e)
        {
            if (radChonNam.Checked)
            {
                var myDate = dtpNam.Value;
                var startOfMonth = new DateTime(dtpNam.Value.Year, 1, 1);
                dtpFromDate.Value = startOfMonth;
                var endOfMonth = new DateTime(dtpNam.Value.Year, 12, 31);
                dtpToDate.Value = endOfMonth;
            }
          
        }
    }
}
