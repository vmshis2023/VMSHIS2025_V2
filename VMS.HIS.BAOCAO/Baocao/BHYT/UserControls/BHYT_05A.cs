using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.Libs.AppUI;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.VisualBasic;
using CrystalDecisions.Shared;
using VNS.HIS.BusRule.Classes;

using NLog;
using SubSonic;

namespace VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls
{
    public partial class BHYT_05A : UserControl
    {
        private DataTable m_dataTH;
        private DataTable m_dataCT;
        private Logger log;
        public BHYT_05A()
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
            this.KeyDown += new KeyEventHandler(BHYT_05A_KeyDown);
            this.Load += new EventHandler(BHYT_05A_Load);
           

           
            optChitiet.CheckedChanged += new EventHandler(optChitiet_CheckedChanged);
            optTonghop.CheckedChanged += new EventHandler(optTonghop_CheckedChanged);
            txtTinhthanh._OnSelectionChanged += new VNS.HIS.UCs.AutoCompleteTextbox.OnSelectionChanged(txtTinhthanh__OnSelectionChanged);
        }

        void txtTinhthanh__OnSelectionChanged()
        {
            AutocompleteKCBBD();
        }

       
        void BHYT_05A_Load(object sender, EventArgs e)
        {

        }

       

        void optTonghop_CheckedChanged(object sender, EventArgs e)
        {
            tabGrid.SelectedTab = tabTH;
            ModifyCommands();
            baocaO_TIEUDE1.Init("BHYT_05A_TH");
        }

        void optChitiet_CheckedChanged(object sender, EventArgs e)
        {
            tabGrid.SelectedTab = tabCt;
            ModifyCommands();
            baocaO_TIEUDE1.Init("BHYT_05A_CT");
        }

       
        public void Init()
        {
            log = LogManager.GetCurrentClassLogger();
            AutocompleteDmuc();
            dtpNgayIn.Value = globalVariables.SysDate;
            dtpFromDate.Value = globalVariables.SysDate;
            dtpToDate.Value = globalVariables.SysDate;

            baocaO_TIEUDE1.Init("BHYT_05A_TH");
            DataBinding.BindDataCombox(cboNhom, THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOMBHYT", true), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
            if (cboNhom.DataSource != null && cboNhom.Items.Count > 0)
                cboNhom.SelectedIndex = 0;
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
                            select p[DmucNoiKCBBD.IdKcbbdColumn.ColumnName].ToString() + "#" + p[DmucNoiKCBBD.MaKcbbdColumn.ColumnName].ToString() + "@" + p[DmucNoiKCBBD.TenKcbbdColumn.ColumnName].ToString() + "@" + p["shortcut"].ToString();
                source = query.ToList();
                this.txtKCBBD.AutoCompleteList = source;
                this.txtKCBBD.TextAlign = HorizontalAlignment.Center;
                this.txtKCBBD.CaseSensitive = false;
                this.txtKCBBD.MinTypedCharacters = 1;

            }
        }
        private void AutocompleteDmuc()
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
                            + p[DmucDiachinh.TenDiachinhColumn.ColumnName].ToString() + "@" 
                            + p.Field<string>("shortcut").ToString();
                source = query.ToList();
                this.txtTinhthanh.AutoCompleteList = source;
                this.txtTinhthanh.TextAlign = HorizontalAlignment.Center;
                this.txtTinhthanh.CaseSensitive = false;
                this.txtTinhthanh.MinTypedCharacters = 1;

            }
        }
        void BHYT_05A_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }

        void cmdExcel_Click(object sender, EventArgs e)
        {
            gridEXExporter1.GridEX = optTonghop.Checked ? grdListTH : grdListCT;
            //Janus.Windows.GridEX.GridEXRow[] gridExRows = grdList.GetCheckedRows();
            if (gridEXExporter1.GridEX.RowCount <= 0)
            {
                Utility.ShowMsg("Không có dữ liệu", "Thông báo");
                //grdList.Focus();
                return;
            }
            saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
            saveFileDialog1.FileName = "BaoCaoBHYT-05A.xls";
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
        private void PrintChitiet(bool view)
        {
            if (m_dataCT != null)
            {
                if (m_dataCT.Rows.Count <= 0 || m_dataCT.Columns.Count <= 0)
                    return;
                 string tieude="", reportname = "";
                ReportDocument crpt = Utility.GetReport("BHYT_05A_CT" ,ref tieude,ref reportname);
                if (crpt == null) return;
                frmPrintPreview frmPrintView =
                    new frmPrintPreview("BÁO CÁO BHYT 05A CHI TIẾT", crpt, true, true);
                Utility.UpdateLogotoDatatable(ref m_dataCT);
                frmPrintView.mv_sReportFileName = Path.GetFileName(reportname);
                frmPrintView.mv_sReportCode = "BHYT_05A_CT";
                crpt.SetDataSource(m_dataCT);
                Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
                //Utility.SetParameterValue(crpt,"Telephone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt,"TIEUDE",
                                       Utility.DoTrim( baocaO_TIEUDE1.TIEUDE));
                Utility.SetParameterValue(crpt,"DATETIME", "Từ ngày: " + dtpFromDate.Value.ToString("dd/MM/yyyy") + " đến ngày: " + dtpToDate.Value.ToString("dd/MM/yyyy"));

                Utility.SetParameterValue(crpt,"sTONGTIEN", ChuyenDoiSoThanhChu());
                Utility.SetParameterValue(crpt,"NGAYIN", Utility.FormatDateTimeWithThanhPho(dtpNgayIn.Value));

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
        private void PrintTonghop(bool view)
        {
            try
            {
                prgBar.Visible = true;
                if (m_dataTH != null)
                {
                    if (m_dataTH.Rows.Count <= 0 || m_dataTH.Columns.Count <= 0)
                        return;
                     string tieude="", reportname = "";
                    ReportDocument crpt = Utility.GetReport("BHYT_05A_TH" ,ref tieude,ref reportname);
                    if (crpt == null) return;
                    var Preview = new frmPrintPreview("BÁO CÁO BHYT 05A TỔNG HỢP", crpt, true, true);
                    if (m_dataTH.Rows.Count <= 0)
                        return;
                    Utility.UpdateLogotoDatatable(ref m_dataTH);
                    Preview.mv_sReportFileName = Path.GetFileName(reportname);
                    Preview.mv_sReportCode = "BHYT_05A_TH";
                    crpt.SetDataSource(m_dataTH);
                    Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                    Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
                    Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                    Utility.SetParameterValue(crpt,"TIEUDE", Utility.DoTrim(baocaO_TIEUDE1.TIEUDE));
                    Utility.SetParameterValue(crpt,"DateTime",
                                           "Từ ngày: " + dtpFromDate.Value.ToShortDateString() + " đến ngày: " +
                                           dtpToDate.Value.ToShortDateString());
                    Utility.SetParameterValue(crpt,"sTongTien", new MoneyByLetter().sMoneyToLetter(Utility.DecimaltoDbnull(m_dataTH.Compute("SUM(CONGTIEN)", "1=1"), 0).ToString()));
                    Utility.SetParameterValue(crpt,"NgayIn", Utility.FormatDateTimeWithThanhPho(dtpNgayIn.Value));
                    Preview.crptViewer.ReportSource = crpt;
                    if (view)
                    {
                        Preview.ShowDialog();
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
        void PrintReport(bool view)
        {
            tabGrid.SelectedTab = optTonghop.Checked ? tabTH : tabCt;
            if (optTonghop.Checked)
                PrintTonghop(view);
            else
                PrintChitiet(view);
        }
        string ChuyenDoiSoThanhChu()
        {
            decimal Tongtien = 0;
            foreach (DataRow row in m_dataCT.Rows)
            {
                Tongtien += Utility.DecimaltoDbnull(row["BHCT"], 0);
            }
            return new MoneyByLetter().sMoneyToLetter(Tongtien.ToString());
        }
        void cmdSearch_Click(object sender, EventArgs e)
        {
            if (optTonghop.Checked)
                LoadTH();
            else
                LoadCT();
            ModifyCommands();
        }
        void LoadTH()
        {
            try
            {
                m_dataTH =
                    new BAOCAO_BHYT().BHYT_05A_TH(dtpFromDate.Value, dtpToDate.Value,
                                         "BHYT",
                                         cboNhom.SelectedValue.ToString(), Utility.Int32Dbnull(cboTuyen.SelectedValue, -1),
                                         txtTPCode.Text,
                                 -1);
                THU_VIEN_CHUNG.CreateXML(m_dataTH, "BHYT05A_TH.xml");
                m_dataTH.AcceptChanges();
                //THU_VIEN_CHUNG.CreateXML(m_dataTH, @"Xml4Reports\BHYT05A_TH.xml");
                Utility.SetDataSourceForDataGridEx(grdListTH, m_dataTH, true, true, "1=1", "");
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        void LoadCT()
        {
             try
            {
            m_dataCT =
                 new BAOCAO_BHYT().BHYT_05A_CT(dtpFromDate.Value.Date, dtpToDate.Value.Date,
                                         "BHYT", -1,
                                         Utility.Int32Dbnull(cboTuyen.SelectedValue, -1),
                                         Utility.sDbnull(txtKCBBDCode.Text, -1), Utility.sDbnull(txtTPCode.Text, -1));


            ProcessData();
            if (!m_dataCT.Columns.Contains("IsDungTuyen")) m_dataCT.Columns.Add("IsDungTuyen", typeof(string));
            THU_VIEN_CHUNG.CreateXML(m_dataCT, "BHYT05A_CT.xml");
            foreach (DataRow drv in m_dataCT.Rows)
            {
                switch (drv["dung_tuyen"].ToString())
                {
                    case "0":
                        drv["IsDungTuyen"] = "Trái tuyến";
                        break;
                    case "1":
                        drv["IsDungTuyen"] = "Đúng tuyến";
                        break;
                    default:
                        drv["IsDungTuyen"] = "Khác";
                        break;
                }
            }
            //THU_VIEN_CHUNG.CreateXML(m_dataCT, @"Xml4Reports\BHYT05A_CT.xml");
            m_dataCT.AcceptChanges();
            Utility.SetDataSourceForDataGridEx(grdListCT, m_dataCT, true, true, "1=1", "");
            }
             catch (Exception ex)
             {
                 Utility.CatchException(ex);
             }
        }
        void ProcessData()
        {
            if (!m_dataCT.Columns.Contains("NamQT")) m_dataCT.Columns.Add("NamQT", typeof(string), dtpToDate.Value.Year.ToString());
            if (!m_dataCT.Columns.Contains("ThangQT")) m_dataCT.Columns.Add("ThangQT", typeof(string), dtpToDate.Value.Month.ToString());
            if (!m_dataCT.Columns.Contains("LoaiKCB")) m_dataCT.Columns.Add("LoaiKCB", typeof(string));
            if (!m_dataCT.Columns.Contains("NoiTT")) m_dataCT.Columns.Add("NoiTT", typeof(string));
            if (!m_dataCT.Columns.Contains("DoiTuong")) m_dataCT.Columns.Add("DoiTuong", typeof(string));
            string sInsCityCode = globalVariables.gv_strNoicapBHYT;
            string sClinicCode = globalVariables.gv_strNoiDKKCBBD;
            foreach (DataRow row in m_dataCT.Rows)
            {
                if (Utility.DoTrim( row[KcbLuotkham.Columns.MatheBhyt].ToString()) != "")
                {
                    if (row[KcbLuotkham.Columns.NoiDongtrusoKcbbd].ToString() == sInsCityCode)
                    {
                        if (row[KcbLuotkham.Columns.MaKcbbd].ToString() == sClinicCode)
                        {
                            row["DoiTuong"] = "A. BỆNH NHÂN ĐĂNG KÝ KCB BAN ĐẦU TẠI CƠ SỞ KCB";
                        }
                        else
                        {
                            row["DoiTuong"] = "B. BỆNH NHÂN NỘI TỈNH KHÔNG KCB BAN ĐẦU TẠI CƠ SỞ KCB";
                        }
                    }
                    else
                    {
                        row["DoiTuong"] = "C. BỆNH NHÂN NGOẠI TỈNH ĐIỀU TRỊ TẠI CƠ SỞ KCB";
                    }
                }
                else
                {
                    row["DoiTuong"] = "D. BỆNH NHÂN KHÁC";
                }
                if (string.IsNullOrEmpty(Utility.DoTrim(row["ten_nhom_bhyt"].ToString())))
                    row["ten_nhom_bhyt"] = "Dịch vụ";
                row["NoiTT"] = "CSKCB";
                row["LoaiKCB"] = "Ngoại";

                UIAction.SetValue4Prg(prgBar, 1);
                
            }
            m_dataCT.AcceptChanges();
        }
        
        void ModifyCommands()
        {
            cmdPrint.Enabled = optTonghop.Checked ? m_dataTH != null && m_dataTH.Rows.Count > 0 : m_dataCT != null && m_dataCT.Rows.Count > 0;
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

        private void tabGrid_SelectedTabChanged(object sender, Janus.Windows.UI.Tab.TabEventArgs e)
        {

        }
    }
}
