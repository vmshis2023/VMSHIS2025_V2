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

using NLog;
using SubSonic;

namespace VNS.HIS.UI.Baocao
{
    public partial class BHYT_20A : UserControl
    {
        private DataTable m_dataTH;
        private DataTable m_dataThuoc;

        private Logger log;
        public BHYT_20A()
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
            this.KeyDown += new KeyEventHandler(BHYT_20A_KeyDown);
            this.Load += new EventHandler(BHYT_20A_Load);
           
            optChitiet.CheckedChanged += new EventHandler(optChitiet_CheckedChanged);
            optTonghop.CheckedChanged += new EventHandler(optTonghop_CheckedChanged);
            txtTinhthanh._OnSelectionChanged += new VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox.OnSelectionChanged(txtTinhthanh__OnSelectionChanged);
            txtLoaithuoc._OnSelectionChanged += new VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox.OnSelectionChanged(txtLoaithuoc__OnSelectionChanged);
            txtLoaithuoc._OnEnterMe += new VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox.OnEnterMe(txtLoaithuoc__OnEnterMe);
            
        }

        void txtLoaithuoc__OnEnterMe()
        {
            AutocompleteThuocTheoLoaithuoc();
        }

        void txtLoaithuoc__OnEnter()
        {
            
        }

        void txtLoaithuoc__OnSelectionChanged()
        {
            AutocompleteThuocTheoLoaithuoc();
        }

        void txtTinhthanh__OnSelectionChanged()
        {
            AutocompleteKCBBD();
        }

       

        void BHYT_20A_Load(object sender, EventArgs e)
        {
            radChonNgay.Checked = true;
            radChonNgay.Focus();
            dtpFromDate.Value = dtpToDate.Value = dtpNam.Value = globalVariables.SysDate;
        }

       
        void optTonghop_CheckedChanged(object sender, EventArgs e)
        {

            ModifyCommands();
            baocaO_TIEUDE1.Init("BHYT_20A");
            
        }

        void optChitiet_CheckedChanged(object sender, EventArgs e)
        {

            ModifyCommands();
            baocaO_TIEUDE1.Init("BHYT_20A");
            
        }

        
        public void Init()
        {
            log = LogManager.GetCurrentClassLogger();
            baocaO_TIEUDE1.Init();
            m_dataThuoc = new Select().From(DmucThuoc.Schema).Where(DmucThuoc.KieuThuocvattuColumn).IsEqualTo("THUOC").ExecuteDataSet().Tables[0];
            Autocomplete_TinhTP();
            AutocompleteThuoc();
            AutocompleteLoaithuoc();
            dtpNgayIn.Value = globalVariables.SysDate;
            dtpFromDate.Value = globalVariables.SysDate;
            dtpToDate.Value = globalVariables.SysDate;
            baocaO_TIEUDE1.Init("BHYT_20A");
           
            DataBinding.BindData(cboObjectType, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(new List<string> { "DV", "BHYT" }),
                                 DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb);

            txtNhomBHYT.Init();
            if (cboTuyen.Items.Count > 0)
                cboTuyen.SelectedIndex = 0;
        }
      
        private void AutocompleteKCBBD()
        {
            DataTable dtKCBBD = null;
            try
            {
                DataRow[] arrDR = globalVariables.gv_dtDmucNoiKCBBD.Select(DmucNoiKCBBD.MaDiachinhColumn.ColumnName + "='" + Utility.DoTrim(txtTPCode.Text) + "'");
                if (arrDR.Length <= 0)
                {
                    this.txtTinhthanh.AutoCompleteList = null;
                    return;
                }
                dtKCBBD = arrDR.CopyToDataTable();
                if (dtKCBBD == null) return;
                if (!dtKCBBD.Columns.Contains("ShortCut"))
                    dtKCBBD.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in dtKCBBD.Rows)
                {
                    string shortcut = "";
                    string realName = dr[DmucNoiKCBBD.TenKcbbdColumn.ColumnName].ToString().Trim() + " " +
                                      Utility.Bodau(dr[DmucNoiKCBBD.TenKcbbdColumn.ColumnName].ToString().Trim());
                    shortcut = dr[DmucNoiKCBBD.MaKcbbdColumn.ColumnName].ToString().Trim();
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
                var query = from p in dtKCBBD.AsEnumerable()
                            select p[DmucNoiKCBBD.IdKcbbdColumn.ColumnName].ToString() + "#" 
                            + p[DmucNoiKCBBD.MaKcbbdColumn.ColumnName].ToString() + "@" 
                            + p[DmucNoiKCBBD.TenKcbbdColumn.ColumnName].ToString() + "@" + p["shortcut"].ToString();
                source = query.ToList();
                this.txtKCBBD.AutoCompleteList = source;
                this.txtKCBBD.TextAlign = HorizontalAlignment.Center;
                this.txtKCBBD.CaseSensitive = false;
                this.txtKCBBD.MinTypedCharacters = 1;

            }
        }
        private void Autocomplete_TinhTP()
        {
            DataTable dtTinhTP = null;
            try
            {
                dtTinhTP =
     new Select(DmucDiachinh.Columns.TenDiachinh, DmucDiachinh.Columns.MaDiachinh).From(DmucDiachinh.Schema).Where(
         DmucDiachinh.Columns.LoaiDiachinh).IsEqualTo(0).ExecuteDataSet().Tables[0];

                if (dtTinhTP == null) return;
                if (!dtTinhTP.Columns.Contains("ShortCut"))
                    dtTinhTP.Columns.Add(new DataColumn("ShortCut", typeof(string)));
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
                var query = from p in dtTinhTP.AsEnumerable()
                            select p[DmucDiachinh.MaDiachinhColumn.ColumnName].ToString() + "#" 
                            + p[DmucDiachinh.MaDiachinhColumn.ColumnName].ToString() + "@" 
                            + p[DmucDiachinh.TenDiachinhColumn.ColumnName].ToString() + "@" + p.Field<string>("shortcut").ToString();
                source = query.ToList();
                this.txtTinhthanh.AutoCompleteList = source;
                this.txtTinhthanh.TextAlign = HorizontalAlignment.Center;
                this.txtTinhthanh.CaseSensitive = false;
                this.txtTinhthanh.MinTypedCharacters = 1;

            }
        }
        private void AutocompleteLoaithuoc()
        {
            DataTable dtLoaithuoc = null;
            try
            {
                dtLoaithuoc =
     new Select().From(DmucLoaithuoc.Schema).Where(DmucLoaithuoc.KieuThuocvattuColumn).IsEqualTo("THUOC").ExecuteDataSet().Tables[0];

                if (dtLoaithuoc == null) return;
                if (!dtLoaithuoc.Columns.Contains("ShortCut"))
                    dtLoaithuoc.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in dtLoaithuoc.Rows)
                {
                    string shortcut = "";
                    string realName = dr[DmucLoaithuoc.TenLoaithuocColumn.ColumnName].ToString().Trim() + " " +
                                      Utility.Bodau(dr[DmucLoaithuoc.TenLoaithuocColumn.ColumnName].ToString().Trim());
                    shortcut = dr[DmucLoaithuoc.MaLoaithuocColumn.ColumnName].ToString().Trim();
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
                var query = from p in dtLoaithuoc.AsEnumerable()
                            select p[DmucLoaithuoc.IdLoaithuocColumn.ColumnName].ToString() + "#" 
                            + p[DmucLoaithuoc.MaLoaithuocColumn.ColumnName].ToString()
                            + "@" + p[DmucLoaithuoc.TenLoaithuocColumn.ColumnName].ToString() + "@" + p.Field<string>("shortcut").ToString();
                source = query.ToList();
                this.txtLoaithuoc.AutoCompleteList = source;
                this.txtLoaithuoc.TextAlign = HorizontalAlignment.Center;
                this.txtLoaithuoc.CaseSensitive = false;
                this.txtLoaithuoc.MinTypedCharacters = 1;

            }
        }
        private void AutocompleteThuocTheoLoaithuoc()
        {
            DataTable dtKCBBD = null;
            try
            {
                DataRow[] arrDR = m_dataThuoc.Select(Utility.Int32Dbnull(txtdrugtype_id.Text, -1) == -1 ? "1=1" : DmucThuoc.IdLoaithuocColumn.ColumnName + "=" + Utility.DoTrim(txtdrugtype_id.Text));// + (Utility.Int32Dbnull(txtDrugID.Text, -1) == -1 ? "" : " AND " + LDrug.DrugIdColumn.ColumnName + "=" + Utility.Int32Dbnull(txtDrugID.Text, -1)));
                if (arrDR.Length <= 0)
                {
                    txtthuoc.dtData = null;
                    txtthuoc.ChangeDataSource();
                    txtthuoc.setDefaultValue();
                    txtthuoc._Text = "";
                    return;
                }
                else
                {
                    dtKCBBD = arrDR.CopyToDataTable();
                    if (dtKCBBD.Select(Utility.Int32Dbnull(txtDrugID.Text, -1) == -1 ? "1=1" : DmucThuoc.IdThuocColumn.ColumnName + "=" + Utility.Int32Dbnull(txtDrugID.Text, -1)).Length <= 0)
                    {
                        txtthuoc.dtData = null;
                        txtthuoc.ChangeDataSource();
                        txtthuoc.setDefaultValue();
                        txtthuoc._Text = "";
                        return;
                    }
                }
                //Kiểm tra thuốc nếu ko thuộc chủng loại này thì xóa đi
                
                if (dtKCBBD == null) return;
                if (dtKCBBD == null || dtKCBBD.Rows.Count == 0)
                {
                    txtthuoc.dtData = null;
                    txtthuoc.ChangeDataSource();
                    txtthuoc.setDefaultValue();
                    txtthuoc._Text = "";
                    return;
                }
                txtthuoc.dtData = dtKCBBD;
                txtthuoc.ChangeDataSource();
            }
            catch
            {
            }
            finally
            {

            }
        }
        private void AutocompleteThuoc()
        {

            try
            {

                if (m_dataThuoc == null)
                {
                    txtthuoc.dtData = null;
                    return;
                }
                txtthuoc.dtData = m_dataThuoc;
                txtthuoc.ChangeDataSource();
            }
            catch
            {
            }
        }
        void BHYT_20A_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }

        void cmdExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (radTheo20AHD.Checked)
                {
                    gridEXExporter1.GridEX = grdExcel;

                    if (gridEXExporter1.GridEX.RowCount <= 0)
                    {
                        Utility.ShowMsg("Không có dữ liệu", "Thông báo");
                        return;
                    }
                    saveFileDialog1.Filter = @"Excel File(*.xlsx)|*.xls";
                    saveFileDialog1.FileName = "BaoCaoBHYT-19a.xlsx";
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
                    string reportcode = "BHYT_20A_EXCEL";
                    string duongdan = Utility.GetPathExcel(reportcode);
                    var book = new C1XLBook();
                    book.Load(duongdan);
                    book.DefaultFont = new Font("Time New Roman", 11, FontStyle.Regular);
                    XLSheet sheet = book.Sheets[0];
                    DataTable dt = m_dataTH;
                    int idxRow = 5;
                    int idxColSh = 0;
                    string codintion = string.Format("Từ ngày {0} đến ngày {1}. Tuyến {2}",
                        dtpFromDate.Value.ToString("dd/MM/yyyy"), dtpToDate.Value.ToString("dd/MM/yyyy"), cboTuyen.Text);
                    sheet[3, idxColSh].SetValue(codintion, HamDungChung.styleStringCenter(book));
                    int sttLoaithuoc = 1;
                    if (chktuyen.Checked)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                sheet[idxRow, idxColSh].SetValue(Convert.ToString(dt.Rows[i]["DoiTuong"]),
                                    HamDungChung.styleStringLeft_Bold(book));
                                idxRow = idxRow + 1;
                                sheet[idxRow, idxColSh].SetValue(
                                    string.Format("{0}.{1}", sttLoaithuoc, Convert.ToString(dt.Rows[i]["loai_thuoc"])),
                                    HamDungChung.styleStringLeft_Bold(book));
                                sttLoaithuoc = sttLoaithuoc + 1;
                                idxRow = idxRow + 1;
                            }
                            else
                            {
                                if (dt.Rows[i]["DoiTuong"].ToString() != dt.Rows[i - 1]["DoiTuong"].ToString())
                                {
                                    sheet[idxRow, idxColSh].SetValue(Convert.ToString(dt.Rows[i]["DoiTuong"]),
                                        HamDungChung.styleStringLeft_Bold(book));
                                    idxRow = idxRow + 1;
                                    sheet[idxRow, idxColSh].SetValue(
                                        string.Format("{0}.{1}", sttLoaithuoc,
                                            Convert.ToString(dt.Rows[i]["loai_thuoc"])),
                                        HamDungChung.styleStringLeft_Bold(book));
                                    sttLoaithuoc = sttLoaithuoc + 1;
                                    idxRow = idxRow + 1;
                                }
                                if (dt.Rows[i]["id_loaithuoc"].ToString() != dt.Rows[i - 1]["id_loaithuoc"].ToString())
                                {
                                    sheet[idxRow, idxColSh].SetValue(
                                        string.Format("{0}.{1}", sttLoaithuoc,
                                            Convert.ToString(dt.Rows[i]["loai_thuoc"])),
                                        HamDungChung.styleStringLeft_Bold(book));
                                    sttLoaithuoc = sttLoaithuoc + 1;
                                    idxRow = idxRow + 1;
                                }
                            }
                            sheet[idxRow, idxColSh].SetValue(Convert.ToString(dt.Rows[i]["ma_QD40"]),
                                HamDungChung.styleStringCenter(book));
                            sheet[idxRow, idxColSh + 1].SetValue(Convert.ToString(dt.Rows[i]["ma_QDTinh"]),
                                HamDungChung.styleStringCenter(book));
                            sheet[idxRow, idxColSh + 2].SetValue(Convert.ToString(dt.Rows[i]["hoat_chat"]),
                                HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 3].SetValue(Convert.ToString(dt.Rows[i]["ten_bhyt"]),
                                HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 4].SetValue(Convert.ToString(dt.Rows[i]["dang_baoche"]),
                                HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 5].SetValue(Convert.ToString(dt.Rows[i]["ham_luong"]),
                                HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 6].SetValue(Convert.ToString(dt.Rows[i]["so_dangky"]),
                                HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 7].SetValue(Convert.ToString(dt.Rows[i]["ten_donvitinh"]),
                                HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 8].SetValue(Convert.ToDecimal(dt.Rows[i]["so_luong"]),
                                HamDungChung.styleNumber(book));
                            sheet[idxRow, idxColSh + 9].SetValue(Convert.ToDecimal(dt.Rows[i]["so_luongnt"]),
                                HamDungChung.styleNumber(book));
                            sheet[idxRow, idxColSh + 10].SetValue(Convert.ToDecimal(dt.Rows[i]["don_gia"]),
                                HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 11].SetValue(Convert.ToDecimal(dt.Rows[i]["thanh_tien"]),
                                HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            idxRow = idxRow + 1;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            sheet[idxRow, idxColSh].SetValue(Convert.ToString(dt.Rows[i]["ma_QD40"]),
                                HamDungChung.styleStringCenter(book));
                            sheet[idxRow, idxColSh + 1].SetValue(Convert.ToString(dt.Rows[i]["ma_QDTinh"]),
                                HamDungChung.styleStringCenter(book));
                            sheet[idxRow, idxColSh + 2].SetValue(Convert.ToString(dt.Rows[i]["hoat_chat"]),
                                HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 3].SetValue(Convert.ToString(dt.Rows[i]["ten_bhyt"]),
                                HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 4].SetValue(Convert.ToString(dt.Rows[i]["dang_baoche"]),
                                HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 5].SetValue(Convert.ToString(dt.Rows[i]["ham_luong"]),
                                HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 6].SetValue(Convert.ToString(dt.Rows[i]["so_dangky"]),
                                HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 7].SetValue(Convert.ToString(dt.Rows[i]["ten_donvitinh"]),
                                HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 8].SetValue(Convert.ToDecimal(dt.Rows[i]["so_luong"]),
                                HamDungChung.styleNumber(book));
                            sheet[idxRow, idxColSh + 9].SetValue(Convert.ToDecimal(dt.Rows[i]["so_luongnt"]),
                                HamDungChung.styleNumber(book));
                            sheet[idxRow, idxColSh + 10].SetValue(Convert.ToDecimal(dt.Rows[i]["don_gia"]),
                                HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            sheet[idxRow, idxColSh + 11].SetValue(Convert.ToDecimal(dt.Rows[i]["thanh_tien"]),
                                HamDungChung.styleDecimalBoldAllBorder_Money(book));
                            idxRow = idxRow + 1;
                        }

                    }

                    sheet[idxRow, idxColSh + 8].SetValue(Convert.ToDecimal(dt.Compute("Sum(so_luong)", "1=1")),
                        HamDungChung.styleNumber(book));
                    sheet[idxRow, idxColSh + 9].SetValue(Convert.ToDecimal(dt.Compute("Sum(so_luongnt)", "1=1")),
                        HamDungChung.styleNumber(book));
                    sheet[idxRow, idxColSh + 11].SetValue(Convert.ToDecimal(dt.Compute("Sum(thanh_tien)", "1=1")),
                        HamDungChung.styleDecimalBoldAllBorder_Money(book));
                    string getdate = string.Format("Ngày {0} tháng {1} năm {2}", dtpNgayIn.Value.Day,
                        dtpNgayIn.Value.Month, dtpNgayIn.Value.Year);
                    sheet[idxRow + 2, 10].SetValue(getdate, HamDungChung.styleStringCenter_UnBorder(book));

                    sheet[idxRow + 3, 2].SetValue("NGƯỜI LẬP BẢNG", HamDungChung.styleDecimalBoldAllBorder_Money(book));
                    sheet[idxRow + 3, 6].SetValue("PHÒNG TÀI CHÍNH KẾ TOÁN",
                        HamDungChung.styleDecimalBoldAllBorder_Money(book));
                    sheet[idxRow + 3, 10].SetValue("GIÁM ĐỐC BỆNH VIỆN",
                        HamDungChung.styleDecimalBoldAllBorder_Money(book));

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
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
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
                        reportcode = "BHYT_20A_PHANTUYEN";
                    }
                    else
                    {
                        reportcode = "BHYT_20A";
                    }
                    if (m_dataTH.Rows.Count <= 0 || m_dataTH.Columns.Count <= 0)
                        return;
                     string tieude="", reportname = "";
                     ReportDocument crpt = Utility.GetReport(reportcode, ref tieude, ref reportname);
                    if (crpt == null) return;
                    var objForm = new frmPrintPreview(Utility.DoTrim(baocaO_TIEUDE1.TIEUDE), crpt, true, true);
                    if (m_dataTH.Rows.Count <= 0)
                        return;
                    Utility.UpdateLogotoDatatable(ref m_dataTH);
                    objForm.mv_sReportFileName = Path.GetFileName(reportname);
                    objForm.mv_sReportCode = reportcode;
                    crpt.SetDataSource(m_dataTH);
                    //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "        NGƯỜI LẬP                                       THỦ TRƯỞNG ĐƠN VỊ           ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                    Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                    Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                    Utility.SetParameterValue(crpt,"ReportCondition", GetReportCondition());
                    Utility.SetParameterValue(crpt,"NTN", Utility.FormatDateTimeWithThanhPho(dtpNgayIn.Value));
                    Utility.SetParameterValue(crpt,"sBangChu", new MoneyByLetter().sMoneyToLetter(SumOfTotal(m_dataTH)));
                    Utility.SetParameterValue(crpt,"sTitleReport", baocaO_TIEUDE1.TIEUDE);
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
           

                reval += "Từ ngày " + dtpFromDate.Value.ToString("dd/MM/yyyy") + " đến ngày " + dtpToDate.Value.ToString("dd/MM/yyyy");
                reval += "Nhóm BHYT: " + txtNhomBHYT.myCode == "-1" ? "Tất cả" : txtNhomBHYT.Text + "; Tuyến: " + cboTuyen.Text + " ;";

            return reval;
        }
        private string SumOfTotal(DataTable dataTable)
        {
            return Utility.sDbnull(dataTable.Compute("SUM(THANH_TIEN)", "1=1"));
        }

        void cmdSearch_Click(object sender, EventArgs e)
        {

            LoadTH();

            ModifyCommands();
        }
        void LoadTH()
        {
            try
            {
                if (chktuyen.Checked)
                {
                    m_dataTH =
                    new BAOCAO_BHYT().BHYT_20A_PHANTUYEN(dtpFromDate.Value.ToString("dd/MM/yyyy"),
                                              dtpToDate.Value.ToString("dd/MM/yyyy"),
                                             "BHYT", -1,
                                             Utility.Int32Dbnull(txtDrugID.Text, -1), Utility.Int16Dbnull(txtdrugtype_id.Text, -1),
                                            txtNhomBHYT.myCode,
                                             Utility.Int32Dbnull(cboTuyen.SelectedValue, -1),
                                             Utility.sDbnull(globalVariables.gv_strNoiDKKCBBD, "01"),
                                             globalVariables.gv_strNoicapBHYT, Utility.sDbnull(txtKCBBDCode.Text, -1),
                                             chkKhacMa.Checked ? "KHAC" : "BANG", -1);


                    THU_VIEN_CHUNG.CreateXML(m_dataTH, "BHYT20A_PHANTUYEN.xml");
                    Utility.SetDataSourceForDataGridEx(grdListPhanTuyen, m_dataTH, true, true, "1=1", "");
                    grdExcel.DataSource = m_dataTH;
                }
                else
                {
                    m_dataTH =
                    new BAOCAO_BHYT().BHYT_20A(dtpFromDate.Value.ToString("dd/MM/yyyy"),
                                              dtpToDate.Value.ToString("dd/MM/yyyy"),
                                             "BHYT", -1,
                                             Utility.Int32Dbnull(txtDrugID.Text, -1), Utility.Int16Dbnull(txtdrugtype_id.Text, -1),
                                            txtNhomBHYT.myCode,
                                             Utility.Int32Dbnull(cboTuyen.SelectedValue, -1),
                                             Utility.sDbnull(globalVariables.gv_strNoiDKKCBBD, "01"),
                                             globalVariables.gv_strNoicapBHYT, Utility.sDbnull(txtKCBBDCode.Text, -1),
                                             chkKhacMa.Checked ? "KHAC" : "BANG", -1);


                    THU_VIEN_CHUNG.CreateXML(m_dataTH, "BHYT20A.xml");
                    Utility.SetDataSourceForDataGridEx(grdList, m_dataTH, true, true, "1=1", "");
                    grdExcel.DataSource = m_dataTH;
                }
                
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }



        void ModifyCommands()
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

        private void baocaO_TIEUDE1_Load(object sender, EventArgs e)
        {

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
