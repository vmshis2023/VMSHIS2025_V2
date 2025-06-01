using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VMS.HIS.DAL;
using SubSonic;
using VNS.HIS.BusRule.Classes;


namespace VNS.HIS.UI.BaoCao.Form_BaoCao
{
    public partial class frm_baocao_nhapthuoctheoNhacungcap : Form
    {
        private DataTable m_dtDataNhaCungCap=new DataTable();
        string KIEU_THUOC_VT = "THUOC";
        public frm_baocao_nhapthuoctheoNhacungcap(string KIEU_THUOC_VT)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KIEU_THUOC_VT = KIEU_THUOC_VT;
            this.KeyPreview = true;
            
            dtNgayIn.Value = dtFromDate.Value = dtToDate.Value = globalVariables.SysDate;
            txtLoaithuoc._OnSelectionChanged += new VNS.HIS.UCs.AutoCompleteTextbox.OnSelectionChanged(txtLoaithuoc__OnSelectionChanged);
            txtLoaithuoc._OnEnterMe += new VNS.HIS.UCs.AutoCompleteTextbox.OnEnterMe(txtLoaithuoc__OnEnterMe);
            this.KeyDown+=new KeyEventHandler(frm_baocao_nhapthuoctheoNhacungcap_KeyDown);
            chkPhieuvay.CheckedChanged += chkPhieuvay_CheckedChanged;
            //cmdAddNhaCCap.Click+=new EventHandler(cmdAddNhaCCap_Click);
        }

        void chkPhieuvay_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPhieuvay.Checked)
            {
                if (KIEU_THUOC_VT == "THUOC")
                    baocaO_TIEUDE1.Init("thuoc_baocao_vaythuoc");
                else
                    baocaO_TIEUDE1.Init("vt_baocao_vayvattu");
            }
            else
            {
                if (KIEU_THUOC_VT == "THUOC")
                    baocaO_TIEUDE1.Init("thuoc_baocao_nhapkho_theonhacungcap");
                else
                    baocaO_TIEUDE1.Init("vt_baocao_nhapkho_theonhacungcap");
            }
        }
        void txtLoaithuoc__OnEnterMe()
        {

        }



        void txtLoaithuoc__OnSelectionChanged()
        {

        }

        private void frm_baocao_nhapthuoctheoNhacungcap_Load(object sender, EventArgs e)
        {
            if(KIEU_THUOC_VT == "THUOC")
            baocaO_TIEUDE1.Init("thuoc_baocao_nhapkho_theonhacungcap");
            else
                baocaO_TIEUDE1.Init("vt_baocao_nhapkho_theonhacungcap");
            txtNhacungcap.Init();
            DataBinding.BindData(cboKho, KIEU_THUOC_VT == "THUOC" ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_TATCA() : CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_TATCA(), TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho);
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
        /// <summary>
        /// hàm thực hiện việc thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện việc trạng thái của ngày 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromDate.Enabled = chkByDate.Checked;
        }

        private void cmdBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                string manhacungcap = Utility.DoTrim(txtNhacungcap.myCode);
                if (manhacungcap == "") manhacungcap = "ALL";
                DataTable m_dtReport =
              BAOCAO_THUOC.ThuocBaocaoThuoctheonhacungcap(chkByDate.Checked ? dtFromDate.Value : Convert.ToDateTime("01/01/1900"),
                                           chkByDate.Checked ? dtToDate.Value : globalVariables.SysDate,
                                           Utility.Int32Dbnull(cboKho.SelectedValue), manhacungcap, KIEU_THUOC_VT,Utility.Bool2byte(chkPhieuvay.Checked));
                Utility.SetDataSourceForDataGridEx(grdList, m_dtReport, true, true, "1=1", "");
                if (m_dtReport.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                string FromDateToDate = Utility.FromToDateTime(dtFromDate.Text, dtToDate.Text);
                VNS.HIS.UI.Baocao.thuoc_baocao.baocao_nhapthuoctheo_nhacungcap(m_dtReport,KIEU_THUOC_VT=="THUOC"?"thuoc_baocao_nhapkho_theonhacungcap":"vt_baocao_nhapkho_theonhacungcap", baocaO_TIEUDE1.TIEUDE, dtNgayIn.Value, FromDateToDate);
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
        private void frm_baocao_nhapthuoctheoNhacungcap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdBaoCao.PerformClick();
            if (e.KeyCode == Keys.F5) cmdExportToExcel.PerformClick();
        }
        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //Janus.Windows.GridEX.GridEXRow[] gridExRows = grdList.GetCheckedRows();
                if (grdList.RowCount <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu", "Thông báo");
                    grdList.Focus();
                    return;
                }
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

        private void cmdCboDownHTTT_Click(object sender, EventArgs e)
        {
            txtthuoc.ShowMe();
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            txtNhacungcap.ShowMe();
        }
    }
}
