using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;


using VNS.Properties;

using VNS.HIS.BusRule.Classes;
using VNS.HIS.UI.Baocao;

namespace VNS.HIS.UI.BaoCao.Form_BaoCao
{
    public partial class frm_baocao_nhapxuaton_theokhoanoitru : Form
    {
        private HisDuocProperties HisDuocProperties;
        string KIEU_THUOC_VT = "THUOC";
        TDmucKho _item = null;
        bool allowChanged = false;
        string KieuKho = "";
        public frm_baocao_nhapxuaton_theokhoanoitru(string args)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KieuKho = args.Split('-')[0];
            this.KIEU_THUOC_VT = args.Split('-')[1];
            
            dtNgayIn.Value = dtFromDate.Value = dtToDate.Value =dtpNam.Value= globalVariables.SysDate;
         
            cmdExit.Click+=new EventHandler(cmdExit_Click);
            this.Load+=new EventHandler(frm_baocao_nhapxuaton_theokhoanoitru_Load);
            cmdBaoCao.Click+=new EventHandler(cmdBaoCao_Click);
            this.KeyDown+=new KeyEventHandler(frm_baocao_nhapxuaton_theokhoanoitru_KeyDown);
            txtLoaithuoc._OnSelectionChanged +=new VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox_Thuoc.OnSelectionChanged(txtLoaithuoc__OnSelectionChanged);
            txtLoaithuoc._OnEnterMe +=new VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox_Thuoc.OnEnterMe(txtLoaithuoc__OnEnterMe);
            chkTheoNhomThuoc.CheckedChanged += new EventHandler(chkTheoNhomThuoc_CheckedChanged);
            optThang.CheckedChanged += _CheckedChanged;
            optQuy.CheckedChanged += _CheckedChanged;
            optNam.CheckedChanged += _CheckedChanged;

            gridEXExporter1.GridEX = grdList;
            CauHinh();
        }

        void _CheckedChanged(object sender, EventArgs e)
        {
           

        }
        void modifyTieude()
        {
            if (KIEU_THUOC_VT == "THUOC")
            {
                if (chkTheoNhomThuoc.Checked)
                {

                    baocaO_TIEUDE1.Init("thuoc_baocao_nhapxuatton_theokhoanoitru_theonhom");
                    grdList.BringToFront();

                }
                else
                {

                    baocaO_TIEUDE1.Init("thuoc_baocao_nhapxuatton_theokhoanoitru");
                    grdList.BringToFront();

                }
            }
            else
            {
                if (chkTheoNhomThuoc.Checked)
                {
                    baocaO_TIEUDE1.Init("vt_baocao_nhapxuatton_theokhoanoitru_theonhom");
                    grdList.BringToFront();

                }
                else
                {

                    baocaO_TIEUDE1.Init("vt_baocao_nhapxuatton_theokhoanoitru");
                    grdList.BringToFront();

                }
            }
        }
        void chkTheoNhomThuoc_CheckedChanged(object sender, EventArgs e)
        {
            modifyTieude();
        }

       
        void txtLoaithuoc__OnEnterMe()
        {

        }
        void txtLoaithuoc__OnSelectionChanged()
        {

        }
        
       
        void BindThuocVT()
        {
            AutocompleteThuoc();
            AutocompleteLoaithuoc();
        }
        private void AutocompleteLoaithuoc()
        {
            DataTable dtLoaithuoc = null;
            try
            {
                dtLoaithuoc =
     new Select().From(DmucLoaithuoc.Schema).Where(DmucLoaithuoc.KieuThuocvattuColumn).IsEqualTo(KIEU_THUOC_VT).ExecuteDataSet().Tables[0];

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
                            select p[DmucLoaithuoc.IdLoaithuocColumn.ColumnName].ToString() + "#" + p[DmucLoaithuoc.MaLoaithuocColumn.ColumnName].ToString()
                            + "@" + p[DmucLoaithuoc.TenLoaithuocColumn.ColumnName].ToString() + "@" + p.Field<string>("shortcut").ToString();
                source = query.ToList();
                this.txtLoaithuoc.AutoCompleteList = source;
                this.txtLoaithuoc.TextAlign = HorizontalAlignment.Left;
                this.txtLoaithuoc.CaseSensitive = false;
                this.txtLoaithuoc.MinTypedCharacters = 1;

            }
        }
        private void AutocompleteThuoc()
        {

            try
            {
                DataTable _dataThuoc = new Select().From(DmucThuoc.Schema).Where(DmucThuoc.KieuThuocvattuColumn).IsEqualTo(KIEU_THUOC_VT).And(DmucThuoc.TrangThaiColumn).IsEqualTo(1).ExecuteDataSet().Tables[0];
                if (_dataThuoc == null)
                {
                    txtthuoc.dtData = null;
                    return;
                }
                txtthuoc.dtData = _dataThuoc;
                txtthuoc.ChangeDataSource();
            }
            catch
            {
            }
        }
        private void CauHinh()
        {
            HisDuocProperties =PropertyLib._HisDuocProperties;
        }
        
        /// <summary>
        /// hàm thực hiện việc đống form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private DataTable m_dtDrugData = new DataTable();
        /// <summary>
        /// load thông tin 
        /// của form hiện tai
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_baocao_nhapxuaton_theokhoanoitru_Load(object sender, EventArgs e)
        {
            DataTable m_dtKhoaNoitru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
            txtKhoanoitru.Init(m_dtKhoaNoitru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            DataTable m_dtNhomThuoc = new Select().From(DmucLoaithuoc.Schema).Where(DmucLoaithuoc.Columns.KieuThuocvattu).IsEqualTo(KIEU_THUOC_VT)
                .OrderAsc(DmucLoaithuoc.Columns.SttHthi).ExecuteDataSet().Tables[0];
            allowChanged = true;
            cboThang.SelectedIndex = globalVariables.SysDate.Month - 1;
        }
        
        /// <summary>
        /// hàm thực hiện in phiếu báo cáo 
        /// thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                string nhomthuoc = "-1";

                nhomthuoc = txtLoaithuoc.MyID.ToString();
                string fromdate="01/01/2000";
                string todate="01/01/2000";
                string _value = "1";
                string _tondau = "Tồn đầu";
                string _toncuoi = "Tồn cuối";
                string FromDateToDate = Utility.FromToDateTime(dtFromDate.Text, dtToDate.Text);
                if (optThang.Checked)
                {
                    if (cboThang.SelectedIndex < 0)
                    {
                        Utility.ShowMsg("Bạn phải chọn Tháng báo cáo");
                        cboThang.Focus();
                        return;
                    }
                    _value = cboThang.SelectedValue.ToString();
                    _tondau = "Tồn đầu tháng " + _value;
                    _toncuoi = "Tồn cuối tháng " + _value;
                    FromDateToDate = "Tháng " + _value;
                    switch (_value)
                    {
                        case "2":
                            fromdate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 29).ToString("dd/MM/yyyy");
                            break;
                        case "4":
                        case "6":
                        case "9":
                        case "11":
                            fromdate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 30).ToString("dd/MM/yyyy");
                            break;
                        default:
                            fromdate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 31).ToString("dd/MM/yyyy");
                            break;
                    }
                }
                else if (optQuy.Checked)
                {
                    if (cboQuy.SelectedIndex < 0)
                    {
                        Utility.ShowMsg("Bạn phải chọn Quý báo cáo");
                        cboQuy.Focus();
                        return;
                    }
                    _value = cboQuy.SelectedValue.ToString();
                    _tondau = "Tồn đầu quý " + _value;
                    _toncuoi = "Tồn cuối quý " + _value;
                    FromDateToDate = "Quý " + _value;
                    switch (_value)
                    {
                        case "1":
                            fromdate = new DateTime(dtpNam.Value.Year, 1, 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, 3, 31).ToString("dd/MM/yyyy");
                            break;
                        case "2":
                            fromdate = new DateTime(dtpNam.Value.Year, 4, 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, 6, 30).ToString("dd/MM/yyyy");
                            break;
                        case "3":
                            fromdate = new DateTime(dtpNam.Value.Year, 7, 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, 9, 30).ToString("dd/MM/yyyy");
                            break;
                        case "4":
                            fromdate = new DateTime(dtpNam.Value.Year, 10, 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, 12, 31).ToString("dd/MM/yyyy");
                            break;
                        default:
                            fromdate = new DateTime(dtpNam.Value.Year, 1, 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, 12, 31).ToString("dd/MM/yyyy");
                            break;
                    }
                }
                else if (optNam.Checked)
                {
                    FromDateToDate = "Năm " + dtpNam.Value.Year.ToString();
                    _tondau = "Tồn " + dtpNam.Value.AddYears(-1).Year.ToString();
                    _toncuoi = "Tồn " + dtpNam.Value.Year.ToString(); 
                    fromdate = new DateTime(dtpNam.Value.Year, 1, 1).ToString("dd/MM/yyyy");
                    todate = new DateTime(dtpNam.Value.Year, 12, 31).ToString("dd/MM/yyyy");
                }
                else
                {
                    _tondau = "Tồn đầu " + dtFromDate.Value.ToString("dd/MM/yyyy");
                    _toncuoi = "Tồn cuối " + dtToDate.Value.ToString("dd/MM/yyyy");
                    fromdate = dtFromDate.Value.ToString("dd/MM/yyyy");
                    todate = dtToDate.Value.ToString("dd/MM/yyyy");
                }
                DataTable m_dtReport = null;

                m_dtReport = BAOCAO_THUOC.ThuocNoitruBaocaonhapxuattonTheokhoa(fromdate,
                                        todate,
                                       Utility.Int32Dbnull(txtKhoanoitru.MyID, -1), nhomthuoc, Utility.Int32Dbnull(txtthuoc.MyID, -1),KIEU_THUOC_VT, chkBiendong.Checked ? 1 : 0);


                Utility.SetDataSourceForDataGridEx(grdList, m_dtReport, true, true, "1=1", "");
                THU_VIEN_CHUNG.CreateXML(m_dtReport, "baocao_xuatnhapton_theokhoanoitru.xml");
                if (m_dtReport.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }


                thuoc_baocao.BaocaoNhapxuattonTheoKhoanoitru(m_dtReport,KIEU_THUOC_VT, baocaO_TIEUDE1.TIEUDE, _tondau, _toncuoi,
                                                                                      dtNgayIn.Value, FromDateToDate,
                                                                                      txtKhoanoitru.Text, chkTheoNhomThuoc.Checked);
            }
            catch (Exception)
            {


            }
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_baocao_nhapxuaton_theokhoanoitru_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdBaoCao.PerformClick();
            if (e.KeyCode == Keys.F5) cmdExportToExcel.PerformClick();
        }

       

       

        private void cmdBaoCao_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {

                if (grdList.RowCount <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu để xuất file excel", "Thông báo");
                    return;
                }
                gridEXExporter1.GridEX = grdList;

                saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                saveFileDialog1.FileName = string.Format("{0}.xls", baocaO_TIEUDE1.TIEUDE);
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
            catch (Exception exception)
            {

            }
        }

        private void frm_baocao_nhapxuaton_theokhoanoitru_KeyDown_1(object sender, KeyEventArgs e)
        {

        }
        /// <summary>
        /// hàm thực hiện việc 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdGetDataDrug_Click(object sender, EventArgs e)
        {
           
        }
    }
}
