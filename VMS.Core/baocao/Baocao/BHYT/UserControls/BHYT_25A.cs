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

using NLog;
using SubSonic;

namespace VNS.HIS.UI.Baocao
{
    public partial class BHYT_25A : UserControl
    {
        private DataTable m_dataTH;
        private DataTable m_dataCT;
        private Logger log;
        public BHYT_25A()
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
            this.KeyDown += new KeyEventHandler(BHYT_25A_KeyDown);
            this.Load += new EventHandler(BHYT_25A_Load);
           
            optChitiet.CheckedChanged += new EventHandler(optChitiet_CheckedChanged);
            optTonghop.CheckedChanged += new EventHandler(optTonghop_CheckedChanged);
            txtTinhthanh._OnSelectionChanged += new VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox.OnSelectionChanged(txtTinhthanh__OnSelectionChanged);
            optMau1.CheckedChanged += new EventHandler(optMau1_CheckedChanged);
        }

        void optMau1_CheckedChanged(object sender, EventArgs e)
        {
            if (optMau2.Checked) baocaO_TIEUDE1.Init("BHYT_25A_CT_MAU2");
            else baocaO_TIEUDE1.Init("BHYT_25A_CT_MAU1");
        }

        void txtTinhthanh__OnSelectionChanged()
        {
            AutocompleteKCBBD();
        }

      

        void BHYT_25A_Load(object sender, EventArgs e)
        {

        }

       

        void optTonghop_CheckedChanged(object sender, EventArgs e)
        {
            tabGrid.SelectedTab = tabTH;
            grbMau.Visible = optChitiet.Checked;
            ModifyCommands();
            baocaO_TIEUDE1.Init("BHYT_25A_TH");
            
        }

        void optChitiet_CheckedChanged(object sender, EventArgs e)
        {
            tabGrid.SelectedTab = tabCt;
            grbMau.Visible = optChitiet.Checked;
            ModifyCommands();
           if (optMau2.Checked) baocaO_TIEUDE1.Init("BHYT_25A_CT_MAU2");
           else baocaO_TIEUDE1.Init("BHYT_25A_CT_MAU1");
            
        }

       
        public void Init()
        {
            log = LogManager.GetCurrentClassLogger();
            AutocompleteDmuc();
            dtpNgayIn.Value = globalVariables.SysDate;
            dtpFromDate.Value = globalVariables.SysDate;
            dtpToDate.Value = globalVariables.SysDate;


            txtNhomBHYT.Init();
            if (cboTuyen.Items.Count > 0)
                cboTuyen.SelectedIndex = 0;
            if (cboKieuBaoCao.Items.Count > 0)
                cboKieuBaoCao.SelectedIndex = 0;
            baocaO_TIEUDE1.Init("BHYT_25A_TH");
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
                            select p[DmucDiachinh.MaDiachinhColumn.ColumnName].ToString() + "#" + p[DmucDiachinh.MaDiachinhColumn.ColumnName].ToString() + "@" + p[DmucDiachinh.TenDiachinhColumn.ColumnName].ToString() + "@" + p.Field<string>("shortcut").ToString();
                source = query.ToList();
                this.txtTinhthanh.AutoCompleteList = source;
                this.txtTinhthanh.TextAlign = HorizontalAlignment.Center;
                this.txtTinhthanh.CaseSensitive = false;
                this.txtTinhthanh.MinTypedCharacters = 1;

            }
        }
        void BHYT_25A_KeyDown(object sender, KeyEventArgs e)
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
            saveFileDialog1.FileName = "BaoCaoBHYT-79a.xls";
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
                if (optMau2.Checked)
                    INMAU2(view);
                else
                    INMAU1(view);



            }
        }
        private void INMAU1(bool view)
        {
            if (m_dataCT != null)
            {
                if (m_dataCT.Rows.Count <= 0 || m_dataCT.Columns.Count <= 0)
                    return;
                 string tieude="", reportname = "";
                ReportDocument crpt = Utility.GetReport("BHYT_25A_CT_MAU1" ,ref tieude,ref reportname);
                if (crpt == null) return;
                frmPrintPreview frmPrintView =
                    new frmPrintPreview("BÁO CÁO DANH SÁCH ĐỀ NGHỊ THANH TOÁN CHI PHÍ KCB NGOẠI TRÚ", crpt, false, true);
                Utility.UpdateLogotoDatatable(ref m_dataCT);
                frmPrintView.mv_sReportFileName = Path.GetFileName(reportname);
                frmPrintView.mv_sReportCode = "BHYT_25A_CT_MAU1";
                crpt.SetDataSource(m_dataCT);
                Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt,"Telephone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "TITLE",
                                       Utility.DoTrim(baocaO_TIEUDE1.TIEUDE));
                Utility.SetParameterValue(crpt, "sCondition",

                                       "Từ ngày: " + dtpFromDate.Value.ToString("dd/MM/yyyy") + " đến ngày " +
                                       dtpToDate.Value.ToString("dd/MM/yyyy"));
                Utility.SetParameterValue(crpt,"TongTien", ChuyenDoiSoThanhChu());
                Utility.SetParameterValue(crpt,"NTN", Utility.FormatDateTimeWithThanhPho(dtpNgayIn.Value));
                frmPrintView.crptViewer.ReportSource = crpt;
                if (view)
                {
                    frmPrintView.ShowDialog();
                }
                else
                {
                    crpt.PrintOptions.PaperSize = PaperSize.PaperLetter;
                    crpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                    crpt.PrintToPrinter(1, false,1,1);
                }
            }
        }
        private void INMAU2(bool view)
        {
            if (m_dataCT != null)
            {
                if (m_dataCT.Rows.Count <= 0 || m_dataCT.Columns.Count <= 0)
                    return;
                 string tieude="", reportname = "";
                ReportDocument crpt = Utility.GetReport("BHYT_25A_CT_MAU2",ref tieude,ref reportname);
                if (crpt == null) return;
                frmPrintPreview frmPrintView =
                    new frmPrintPreview("BÁO CÁO DANH SÁCH ĐỀ NGHỊ THANH TOÁN CHI PHÍ KCB NGOẠI TRÚ", crpt, false, true);
                Utility.UpdateLogotoDatatable(ref m_dataCT);
                frmPrintView.mv_sReportFileName = Path.GetFileName(reportname);
                frmPrintView.mv_sReportCode = "BHYT_25A_CT_MAU2";
                crpt.SetDataSource(m_dataCT);

                Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt,"Telephone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt,"TITLE",
                                       Utility.DoTrim(baocaO_TIEUDE1.TIEUDE));
                Utility.SetParameterValue(crpt, "sCondition",
                                      
                                       "Từ ngày: "+ dtpFromDate.Value.ToString("dd/MM/yyyy") + " đến ngày " +
                                       dtpToDate.Value.ToString("dd/MM/yyyy"));
                Utility.SetParameterValue(crpt,"TongTien", ChuyenDoiSoThanhChu());
                Utility.SetParameterValue(crpt,"NTN", Utility.FormatDateTimeWithThanhPho(dtpNgayIn.Value));
                frmPrintView.crptViewer.ReportSource = crpt;
                if (view)
                {
                    frmPrintView.ShowDialog();
                }
                else
                {
                    crpt.PrintOptions.PaperSize = PaperSize.PaperLetter;
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
                    ReportDocument crpt = Utility.GetReport("BHYT_25A_TH",ref tieude,ref reportname);
                    if (crpt == null) return;
                    frmPrintPreview objForm = new frmPrintPreview("BÁO CÁO 25a TỔNG HỢP", crpt, true, true);
                    objForm.mv_sReportFileName = Path.GetFileName(reportname);
                    objForm.mv_sReportCode = "BHYT_25A_TH";

                    Utility.UpdateLogotoDatatable(ref m_dataTH);
                    crpt.SetDataSource(m_dataTH);
                    Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                    Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
                    Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                    Utility.SetParameterValue(crpt,"TIEUDE", Utility.DoTrim(baocaO_TIEUDE1.TIEUDE));
                    Utility.SetParameterValue(crpt,"DataTime",
                                           "Từ ngày: " + dtpFromDate.Value.ToShortDateString() + " đến ngày: " +
                                           dtpToDate.Value.ToShortDateString());
                    Utility.SetParameterValue(crpt,"sTongTien", new MoneyByLetter().sMoneyToLetter(Utility.DecimaltoDbnull(m_dataTH.Compute("SUM(BHCT)", "1=1"), 0).ToString()));
                    Utility.SetParameterValue(crpt,"NgayIn", Utility.FormatDateTimeWithThanhPho(dtpNgayIn.Value));
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
                if (radBHCT.Checked)
                    Tongtien += Utility.DecimaltoDbnull(row["BHCT"], 0);
                else
                {
                    Tongtien += Utility.DecimaltoDbnull(row["TONGCONG"], 0);
                }
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
                    new BAOCAO_BHYT().BHYT_25A_TH(dtpFromDate.Value.Date, dtpToDate.Value.Date,
                                         "BHYT",
                                         Utility.Int32Dbnull(cboTuyen.SelectedValue, -1));
                if (!m_dataTH.Columns.Contains("HOvaTEN")) m_dataTH.Columns.Add("HOvaTEN", typeof(string));
                if (!m_dataTH.Columns.Contains("sTuyen")) m_dataTH.Columns.Add("sTuyen", typeof(string));

                THU_VIEN_CHUNG.CreateXML(m_dataTH, "BHYT25A_TH.xml");
                foreach (DataRow dr in m_dataTH.Rows)
                {
                    switch (dr["DOITUONG"].ToString())
                    {
                        case "A":
                            dr["HOvaTEN"] = globalVariables.GvStrTendoituongNoiTinhKcbbd;
                            if (dr["dung_tuyen"].ToString() == "1")
                                dr["sTuyen"] = "Đúng tuyến";
                            else
                                dr["sTuyen"] = "Trái tuyến";
                            break;
                        case "B":
                            dr["HOvaTEN"] = globalVariables.GvStrTendoituongNoitinhKhongKcbbd;
                            if (dr["dung_tuyen"].ToString() == "1")
                                dr["sTuyen"] = "Đúng tuyến";
                            else
                                dr["sTuyen"] = "Trái tuyến";
                            break;
                        case "C":
                            dr["HOvaTEN"] = globalVariables.GvStrTendoituongNgoaitinh;
                            if (dr["dung_tuyen"].ToString() == "1")
                                dr["sTuyen"] = "Đúng tuyến";
                            else
                                dr["sTuyen"] = "Trái tuyến";
                            break;
                        default:
                            dr["HOvaTEN"] = "D.BỆNH NHÂN DỊCH VỤ";
                            dr["sTuyen"] = "Tất cả";
                            break;
                    }
                }
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
                 new BAOCAO_BHYT().BHYT_25A_CT(dtpFromDate.Value, dtpToDate.Value,
                                            "BHYT", -1, Utility.Int32Dbnull(cboTuyen.SelectedIndex-1, -1),
                                            cboKieuBaoCao.SelectedIndex==0?"25a":"25d",
                                            globalVariables.gv_strNoiDKKCBBD, -1);


            UpdateDataAlterLoad();
            if (!m_dataCT.Columns.Contains("IsDungTuyen")) m_dataCT.Columns.Add("IsDungTuyen", typeof(string));
            THU_VIEN_CHUNG.CreateXML(m_dataCT, "BHYT25A_CT.xml");
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
            
            m_dataCT.AcceptChanges();
            Utility.SetDataSourceForDataGridEx(grdListCT, m_dataCT, true, true, "1=1", "");
             }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        void UpdateDataAlterLoad()
        {
            if (!m_dataCT.Columns.Contains("NamQT")) m_dataCT.Columns.Add("NamQT", typeof(string), dtpToDate.Value.Year.ToString());
            if (!m_dataCT.Columns.Contains("ThangQT")) m_dataCT.Columns.Add("ThangQT", typeof(string), dtpToDate.Value.Month.ToString());
            if (!m_dataCT.Columns.Contains("LoaiKCB")) m_dataCT.Columns.Add("LoaiKCB", typeof(string));
            if (!m_dataCT.Columns.Contains("NoiTT")) m_dataCT.Columns.Add("NoiTT", typeof(string));
            if (!m_dataCT.Columns.Contains("DoiTuong")) m_dataCT.Columns.Add("DoiTuong", typeof(string));
           
          
            foreach (DataRow row in m_dataCT.Rows)
            {
                if (row[DmucDoituongkcb.Columns.IdLoaidoituongKcb].ToString() == "0")
                {

                    if (row[KcbLuotkham.Columns.MaNoicapBhyt].ToString() == globalVariables.gv_strNoicapBHYT)// "InsObject_CodeTP"
                    {
                        if (row[KcbLuotkham.Columns.MaKcbbd].ToString() == globalVariables.gv_strNoiDKKCBBD)//"InsClinic_Code"
                        {
                            row["DoiTuong"] = globalVariables.GvStrTendoituongNoiTinhKcbbd;
                        }
                        else
                        {
                            row["DoiTuong"] = globalVariables.GvStrTendoituongNoitinhKhongKcbbd;
                        }
                    }
                    else
                    {
                        row["DoiTuong"] = globalVariables.GvStrTendoituongNgoaitinh;
                    }
                }
                else
                {
                    row["DoiTuong"] = "D. BỆNH NHÂN DỊCH VỤ";
                }

                if (string.IsNullOrEmpty(row["ten_nhombhyt"].ToString()))
                    row["ten_nhombhyt"] = "Dịch vụ";
                row["NoiTT"] = "CSKCB";
                row["LoaiKCB"] = "Ngoại";
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

        private void txtKCBBD_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
