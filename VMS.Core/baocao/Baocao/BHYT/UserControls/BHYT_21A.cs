using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using C1.C1Excel;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using NLog;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.DAL;
using VNS.Libs;
using VNS.Libs.AppUI;

namespace VNS.HIS.UI.Baocao
{
    public partial class BHYT_21A : UserControl
    {
        private Logger log;
        private DataTable m_dataTH;

        public BHYT_21A()
        {
            InitializeComponent();
            InitEvents();
        }

        private void InitEvents()
        {
            cmdSearch.Click += cmdSearch_Click;
            cmdPrint.Click += cmdPrint_Click;
            cmdPreview.Click += cmdPreview_Click;
            cmdExcel.Click += cmdExcel_Click;
            dtpFromDate.ValueChanged += dtpFromDate_ValueChanged;
            dtpToDate.ValueChanged += dtpToDate_ValueChanged;
            KeyDown += BHYT_21A_KeyDown;
            Load += BHYT_21A_Load;

            optChitiet.CheckedChanged += optChitiet_CheckedChanged;
            optTonghop.CheckedChanged += optTonghop_CheckedChanged;
            //     txtTinhthanh._OnSelectionChanged += new VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox.OnSelectionChanged(txtTinhthanh__OnSelectionChanged);
        }

        //void txtTinhthanh__OnSelectionChanged()
        //{
        //    AutocompleteKCBBD();
        //}


        private void BHYT_21A_Load(object sender, EventArgs e)
        {
            radChonNgay.Checked = true;
            radChonNgay.Focus();
            dtpFromDate.Value = dtpToDate.Value = dtpNam.Value = globalVariables.SysDate;
        }


        private void optTonghop_CheckedChanged(object sender, EventArgs e)
        {
            ModifyCommands();
        }

        private void optChitiet_CheckedChanged(object sender, EventArgs e)
        {
            ModifyCommands();
        }


        public void Init()
        {
            log = LogManager.GetCurrentClassLogger();
            AutocompleteDmuc();
            dtpNgayIn.Value = globalVariables.SysDate;
            dtpFromDate.Value = globalVariables.SysDate;
            dtpToDate.Value = globalVariables.SysDate;
            baocaO_TIEUDE1.Init("BHYT_21A");
            AutocompleteKcbbd();
            DataBinding.BindData(cboObjectType, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(new List<string> {"DV", "BHYT"}),
                DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb);
            txtNhomBHYT.Init();
            if (cboObjectType.DataSource != null && cboObjectType.Items.Count > 0)
                cboObjectType.SelectedIndex = 0;
            if (cboTuyen.Items.Count > 0)
                cboTuyen.SelectedIndex = 0;
        }

        private void AutocompleteKcbbd()
        {
            DataTable dtKcbbd = null;
            try
            {
                DataRow[] arrDR =
                    globalVariables.gv_dtDmucNoiKCBBD.Select(DmucNoiKCBBD.MaKcbbdColumn.ColumnName + "='" +
                                                             Utility.DoTrim(txtTPCode.Text) + "' OR " + Utility.DoTrim(txtTPCode.Text) + "= '-1'" );
                if (arrDR.Length <= 0)
                {
                    txtTinhthanh.AutoCompleteList = null;
                    return;
                }
                dtKcbbd = arrDR.CopyToDataTable();
                if (dtKcbbd == null) return;
                if (!dtKcbbd.Columns.Contains("ShortCut"))
                    dtKcbbd.Columns.Add(new DataColumn("ShortCut", typeof (string)));
                foreach (DataRow dr in dtKcbbd.Rows)
                {
                    string shortcut = "";
                    string realName = dr[DmucNoiKCBBD.TenKcbbdColumn.ColumnName].ToString().Trim() + " " +
                                      Utility.Bodau(dr[DmucNoiKCBBD.TenKcbbdColumn.ColumnName].ToString().Trim());
                    shortcut = dr[DmucNoiKCBBD.MaKcbbdColumn.ColumnName].ToString().Trim();
                    string[] arrWords = realName.ToLower().Split(' ');
                    string nospace = "";
                    string space = arrWords.Where(word => word.Trim() != "").Aggregate("", (current, word) => current + (word + " "));
                    shortcut += space; // +_Nospace;
                    shortcut = arrWords.Where(word => word.Trim() != "").Aggregate(shortcut, (current, word) => current + word.Substring(0, 1));
                    dr["ShortCut"] = shortcut;
                }
            }
            catch(Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
            finally
            {
                var source = new List<string>();
                EnumerableRowCollection<string> query = from p in dtKcbbd.AsEnumerable()
                    select
                        p[DmucNoiKCBBD.IdKcbbdColumn.ColumnName] + "#" + p[DmucNoiKCBBD.MaKcbbdColumn.ColumnName] + "@" +
                        p[DmucNoiKCBBD.TenKcbbdColumn.ColumnName] + "@" + p["shortcut"];
                source = query.ToList();
                txtKCBBD.AutoCompleteList = source;
                txtKCBBD.TextAlign = HorizontalAlignment.Center;
                txtKCBBD.CaseSensitive = false;
                txtKCBBD.MinTypedCharacters = 1;
            }
        }

        private void AutocompleteDmuc()
        {
            DataTable dtTinhTP = null;
            try
            {
                dtTinhTP =
                    new Select(DmucDiachinh.Columns.TenDiachinh, DmucDiachinh.Columns.MaDiachinh).From(
                        DmucDiachinh.Schema).Where(
                            DmucDiachinh.Columns.LoaiDiachinh).IsEqualTo(0).ExecuteDataSet().Tables[0];

                if (dtTinhTP == null) return;
                if (!dtTinhTP.Columns.Contains("ShortCut"))
                    dtTinhTP.Columns.Add(new DataColumn("ShortCut", typeof (string)));
                foreach (DataRow dr in dtTinhTP.Rows)
                {
                    string shortcut = "";
                    string realName = dr[DmucDiachinh.TenDiachinhColumn.ColumnName].ToString().Trim() + " " +
                                      Utility.Bodau(dr[DmucDiachinh.TenDiachinhColumn.ColumnName].ToString().Trim());
                    shortcut = dr[DmucDiachinh.MaDiachinhColumn.ColumnName].ToString().Trim();
                    string[] arrWords = realName.ToLower().Split(' ');
                    string _space = "";
                    string _Nospace = "";
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                        {
                            _space += word + " ";
                            //_Nospace += word;
                        }
                    }
                    shortcut += _space; // +_Nospace;
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                            shortcut += word.Substring(0, 1);
                    }
                    dr["ShortCut"] = shortcut;
                }
            }
            catch
            {
            }
            finally
            {
                var source = new List<string>();
                EnumerableRowCollection<string> query = from p in dtTinhTP.AsEnumerable()
                    select
                        p[DmucDiachinh.MaDiachinhColumn.ColumnName] + "#" + p[DmucDiachinh.MaDiachinhColumn.ColumnName] +
                        "@" + p[DmucDiachinh.TenDiachinhColumn.ColumnName] + "@" + p.Field<string>("shortcut");
                source = query.ToList();
                txtTinhthanh.AutoCompleteList = source;
                txtTinhthanh.TextAlign = HorizontalAlignment.Center;
                txtTinhthanh.CaseSensitive = false;
                txtTinhthanh.MinTypedCharacters = 1;
            }
        }

        private void BHYT_21A_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string reportcode = "BHYT_21A_EXCEL";
                string duongdan = Utility.GetPathExcel(reportcode);
                var book = new C1XLBook();
                book.Load(duongdan);
                book.DefaultFont = new Font("Time New Roman", 11, FontStyle.Regular);
                XLSheet sheet = book.Sheets[0];
                DataTable dt = m_dataTH;
                int idxRow = 6;
                int idxColSh = 0;
                string codintion = string.Format("Từ ngày {0} đến ngày {1}. Tuyến {2}",
                    dtpFromDate.Value.ToString("dd/MM/yyyy"), dtpToDate.Value.ToString("dd/MM/yyyy"), cboTuyen.Text);
                sheet[3, idxColSh].SetValue(Convert.ToString(codintion), HamDungChung.styleStringCenter(book));
                int sttloaidichvu = 1;
                if (chktuyen.Checked)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            sheet[idxRow, idxColSh].SetValue(Convert.ToString(dt.Rows[i]["DoiTuong"]),   HamDungChung.styleStringLeft_Bold(book));
                            idxRow = idxRow + 1;
                            sheet[idxRow, idxColSh].SetValue( string.Format("{0}.{1}", sttloaidichvu,  Convert.ToString(dt.Rows[i]["Ten_nhombaocao_dichvu"])),  HamDungChung.styleStringLeft_Bold(book));
                            sttloaidichvu = sttloaidichvu + 1;
                            idxRow = idxRow + 1;
                        }
                        else
                        {
                            if (dt.Rows[i]["DoiTuong"].ToString() != dt.Rows[i - 1]["DoiTuong"].ToString())
                            {
                                sheet[idxRow, idxColSh].SetValue(Convert.ToString(dt.Rows[i]["DoiTuong"]),  HamDungChung.styleStringLeft_Bold(book));
                                idxRow = idxRow + 1;
                                sttloaidichvu = 1;
                                sheet[idxRow, idxColSh].SetValue( string.Format("{0}.{1}", sttloaidichvu,  Convert.ToString(dt.Rows[i]["Ten_nhombaocao_dichvu"])), HamDungChung.styleStringLeft_Bold(book));
                                sttloaidichvu = sttloaidichvu + 1;
                                idxRow = idxRow + 1;
                            }
                            if (dt.Rows[i]["DoiTuong"].ToString() == dt.Rows[i - 1]["DoiTuong"].ToString() && dt.Rows[i]["Ten_nhombaocao_dichvu"].ToString() != dt.Rows[i - 1]["Ten_nhombaocao_dichvu"].ToString())
                            {
                                sheet[idxRow, idxColSh].SetValue(string.Format("{0}.{1}", sttloaidichvu,  Convert.ToString(dt.Rows[i]["Ten_nhombaocao_dichvu"])),  HamDungChung.styleStringLeft_Bold(book));
                                sttloaidichvu = sttloaidichvu + 1;
                                idxRow = idxRow + 1;
                            }
                        }
                        sheet[idxRow, idxColSh].SetValue(Convert.ToString(dt.Rows[i]["ma_QD"]),
                            HamDungChung.styleStringCenter(book));
                        sheet[idxRow, idxColSh + 1].SetValue(Convert.ToString(dt.Rows[i]["ma_chitiet_bhyt"]),
                            HamDungChung.styleStringCenter(book));
                        sheet[idxRow, idxColSh + 2].SetValue(Convert.ToString(dt.Rows[i]["ten_chitietdichvu"]),
                            HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 3].SetValue(Convert.ToDecimal(dt.Rows[i]["SO_LUONG"]),
                            HamDungChung.styleNumber(book));
                        sheet[idxRow, idxColSh + 4].SetValue(Convert.ToDecimal(dt.Rows[i]["soluong_nt"]),
                            HamDungChung.styleNumber(book));
                        sheet[idxRow, idxColSh + 5].SetValue(Convert.ToDecimal(dt.Rows[i]["DON_GIA"]),
                            HamDungChung.styleDecimalBoldAllBorder_Money(book));
                        sheet[idxRow, idxColSh + 6].SetValue(Convert.ToDecimal(dt.Rows[i]["thanh_tien"]),
                            HamDungChung.styleDecimalBoldAllBorder_Money(book));
                        idxRow = idxRow + 1;
                    }
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            sheet[idxRow, idxColSh].SetValue(string.Format("{0}.{1}", sttloaidichvu, Convert.ToString(dt.Rows[i]["Ten_nhombaocao_dichvu"])), HamDungChung.styleStringLeft_Bold(book));
                            sttloaidichvu = sttloaidichvu + 1;
                            idxRow = idxRow + 1;
                        }
                        else
                        {
                            if (dt.Rows[i]["Ten_nhombaocao_dichvu"].ToString() != dt.Rows[i - 1]["Ten_nhombaocao_dichvu"].ToString())
                            {
                                sheet[idxRow, idxColSh].SetValue(string.Format("{0}.{1}", sttloaidichvu, Convert.ToString(dt.Rows[i]["Ten_nhombaocao_dichvu"])), HamDungChung.styleStringLeft_Bold(book));
                                sttloaidichvu = sttloaidichvu + 1;
                                idxRow = idxRow + 1;
                            }
                        }
                        sheet[idxRow, idxColSh].SetValue(Convert.ToString(dt.Rows[i]["ma_QD"]),
                            HamDungChung.styleStringCenter(book));
                        sheet[idxRow, idxColSh + 1].SetValue(Convert.ToString(dt.Rows[i]["ma_chitiet_bhyt"]),
                            HamDungChung.styleStringCenter(book));
                        sheet[idxRow, idxColSh + 2].SetValue(Convert.ToString(dt.Rows[i]["ten_chitietdichvu"]),
                            HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 3].SetValue(Convert.ToDecimal(dt.Rows[i]["SO_LUONG"]),
                            HamDungChung.styleNumber(book));
                        sheet[idxRow, idxColSh + 4].SetValue(Convert.ToDecimal(dt.Rows[i]["soluong_nt"]),
                            HamDungChung.styleNumber(book));
                        sheet[idxRow, idxColSh + 5].SetValue(Convert.ToDecimal(dt.Rows[i]["DON_GIA"]),
                            HamDungChung.styleDecimalBoldAllBorder_Money(book));
                        sheet[idxRow, idxColSh + 6].SetValue(Convert.ToDecimal(dt.Rows[i]["thanh_tien"]),
                            HamDungChung.styleDecimalBoldAllBorder_Money(book));
                        idxRow = idxRow + 1;
                    }
                }
                sheet[idxRow, idxColSh + 3].SetValue(Convert.ToDecimal(dt.Compute("Sum(SO_LUONG)", "1=1")), HamDungChung.styleNumber(book));
                sheet[idxRow, idxColSh + 4].SetValue(Convert.ToDecimal(dt.Compute("Sum(soluong_nt)", "1=1")), HamDungChung.styleNumber(book));
                sheet[idxRow, idxColSh + 6].SetValue(Convert.ToDecimal(dt.Compute("Sum(thanh_tien)", "1=1")), HamDungChung.styleDecimalBoldAllBorder_Money(book));
                string getdate = string.Format("Ngày {0} tháng {1} năm {2}", dtpNgayIn.Value.Day,
                    dtpNgayIn.Value.Month, dtpNgayIn.Value.Year);
                sheet[idxRow + 2, 5].SetValue(getdate, HamDungChung.styleStringCenter_UnBorder(book));

                sheet[idxRow + 3, 1].SetValue("NGƯỜI LẬP BẢNG", HamDungChung.styleDecimalBoldAllBorder_Money(book));
                sheet[idxRow + 3, 4].SetValue("PHÒNG TÀI CHÍNH KẾ TOÁN",
                    HamDungChung.styleDecimalBoldAllBorder_Money(book));
                sheet[idxRow + 3, 5].SetValue("GIÁM ĐỐC BỆNH VIỆN", HamDungChung.styleDecimalBoldAllBorder_Money(book));
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
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }

        private void cmdPreview_Click(object sender, EventArgs e)
        {
            PrintReport(true);
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            PrintReport(false);
        }

        private void PrintReport(bool view)
        {
            try
            {
                prgBar.Visible = true;
                if (m_dataTH != null)
                {
                    string reportcode;
                    if (chktuyen.Checked)
                    {
                        reportcode = "BHYT_21A_PHANTUYEN";
                    }
                    else
                    {
                        reportcode = "BHYT_21A";
                    }
                    if (m_dataTH.Rows.Count <= 0 || m_dataTH.Columns.Count <= 0)
                        return;
                    string tieude = "", reportname = "";
                    ReportDocument crpt = Utility.GetReport(reportcode, ref tieude, ref reportname);
                    if (crpt == null) return;
                    var objForm = new frmPrintPreview(Utility.DoTrim(baocaO_TIEUDE1.TIEUDE), crpt, true, true);
                    if (m_dataTH.Rows.Count <= 0)
                        return;
                    Utility.UpdateLogotoDatatable(ref m_dataTH);
                    THU_VIEN_CHUNG.CreateXML(m_dataTH, string.Format("{0}.xml", reportcode));
                    crpt.SetDataSource(m_dataTH);
                    objForm.mv_sReportFileName = Path.GetFileName(reportname);
                    objForm.mv_sReportCode = reportcode;
                    Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                    Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                    Utility.SetParameterValue(crpt, "ReportCondition", GetReportCondition());
                    Utility.SetParameterValue(crpt, "NTN", Utility.FormatDateTimeWithThanhPho(dtpNgayIn.Value));
                    Utility.SetParameterValue(crpt, "sBangChu", new MoneyByLetter().sMoneyToLetter(SumOfTotal(m_dataTH)));
                    Utility.SetParameterValue(crpt, "sTitleReport", baocaO_TIEUDE1.TIEUDE);
                    objForm.crptViewer.ReportSource = crpt;
                    if (view)
                    {
                        objForm.ShowDialog();
                    }
                    else
                    {
                        crpt.PrintOptions.PaperSize = PaperSize.PaperA4;
                        crpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                        crpt.PrintToPrinter(1, false, 1, 1);
                    }
                    prgBar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                log.Error("Loi trong qua trinh in bao cao : (0)", ex);
                prgBar.Visible = false;
            }
        }

        private string GetReportCondition()
        {
            string reval = "";
           
                reval += "Từ ngày " + dtpFromDate.Value.ToString("dd/MM/yyyy") + " đến ngày " +
                         dtpToDate.Value.ToString("dd/MM/yyyy");
                reval += "Nhóm BHYT: " + txtNhomBHYT.myCode == "-1"
                    ? "Tất cả"
                    : txtNhomBHYT.Text + "; Tuyến: " + cboTuyen.Text + " ;";
          
            return reval;
        }

        private string SumOfTotal(DataTable dataTable)
        {
            return Utility.sDbnull(dataTable.Compute("SUM(THANH_TIEN)", "1=1"));
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            LoadTh();
            //ProcessData();
            ModifyCommands();
        }
        void ProcessData()
        {
            try
            {
                Utility.ResetProgressBar(prgBar, m_dataTH.Rows.Count, true);
                if (!m_dataTH.Columns.Contains("LoaiKCB")) m_dataTH.Columns.Add("LoaiKCB", typeof(string));
                if (!m_dataTH.Columns.Contains("NoiTT")) m_dataTH.Columns.Add("NoiTT", typeof(string));
                if (!m_dataTH.Columns.Contains("DoiTuong")) m_dataTH.Columns.Add("DoiTuong", typeof(string));
                if (!m_dataTH.Columns.Contains("Tuyen")) m_dataTH.Columns.Add("Tuyen", typeof(int));
                foreach (DataRow row in m_dataTH.Rows)
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
                m_dataTH.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        private void LoadTh()
        {
            try
            {
                if (chktuyen.Checked)
                {
                    m_dataTH =
                  new BAOCAO_BHYT().BHYT_21A_PHANTUYEN(dtpFromDate.Value, dtpToDate.Value,
                      "BHYT", -1, txtNhomBHYT.myCode, Utility.Int32Dbnull(cboTuyen.SelectedIndex - 1, -1),
                      Utility.sDbnull(globalVariables.gv_strNoiDKKCBBD, "01"), globalVariables.gv_strNoicapBHYT, Utility.sDbnull(txtKCBBDCode.Text, -1),
                      chkKhacMa.Checked ? "KHAC" : "BANG");

                    THU_VIEN_CHUNG.CreateXML(m_dataTH, "BHYT21A.xml");
                    Utility.SetDataSourceForDataGridEx(grdListPhanTuyen, m_dataTH, true, true, "1=1", "");
                    grdExcel.DataSource = m_dataTH;
                }
                else
                {
                    m_dataTH =
                    new BAOCAO_BHYT().BHYT_21A(dtpFromDate.Value, dtpToDate.Value,
                        "BHYT", -1, txtNhomBHYT.myCode, Utility.Int32Dbnull(cboTuyen.SelectedIndex - 1, -1),
                        Utility.sDbnull(globalVariables.gv_strNoiDKKCBBD, "01"), globalVariables.gv_strNoicapBHYT, Utility.sDbnull(txtKCBBDCode.Text, -1),
                        chkKhacMa.Checked ? "KHAC" : "BANG");

                    THU_VIEN_CHUNG.CreateXML(m_dataTH, "BHYT21A.xml");
                    Utility.SetDataSourceForDataGridEx(grdList, m_dataTH, true, true, "1=1", "");
                    grdExcel.DataSource = m_dataTH;
                }
                
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void ModifyCommands()
        {
            cmdPrint.Enabled = m_dataTH != null && m_dataTH.Rows.Count > 0;
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

        private void chktuyen_CheckedChanged(object sender, EventArgs e)
        {
            if (chktuyen.Checked)
            {
                m_dataTH = null;
                grdList.DataSource = grdListPhanTuyen.DataSource = null;
                grdList.SendToBack();
                grdListPhanTuyen.BringToFront();
            }
            else
            {
              m_dataTH = null;
              grdList.DataSource = grdListPhanTuyen.DataSource = null;
              grdListPhanTuyen.SendToBack();
              grdList.BringToFront();
            }
        }
    }
}