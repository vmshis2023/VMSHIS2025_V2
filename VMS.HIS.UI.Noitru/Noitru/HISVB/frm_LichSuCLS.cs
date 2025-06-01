using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VietBaIT.CommonLibrary;
using VietBaIT.HISLink.DataAccessLayer;

namespace VietBaIT.HISLink.UI.ControlUtility.LichSuCLS
{
    public partial class frm_LichSuCLS : Form
    {
        public int Patient_ID = -1;
        private DataTable m_dtKetQua;
        private DataSet ds;
        public frm_LichSuCLS()
        {
            InitializeComponent();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_LichSuCLS_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            m_dtKetQua = SPs.HistoryLichSuCLs(Patient_ID, string.Empty, Utility.Int32Dbnull(txtNam.Text), DateTime.Now.Month, 0)
                .GetDataSet()
                .Tables[0];
         
               DataTable secondTable = m_dtKetQua.AsEnumerable()
                .GroupBy(row => new
                {

                    Ten_KQ = row.Field<string>("Ten_KQ"),
                })
                .Select(group => group.First())
                .CopyToDataTable();

            if (secondTable!=null) grdList.DataSource = secondTable;
            else grdList.DataSource = null;
            Utility.SetDataSourceForDataGridEx(grdKetQua, m_dtKetQua, true, true, "1=1", "");
            grdList_SelectionChanged(grdList,new EventArgs());
        }

        private string _rowfilter = "1=1";
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                _rowfilter = "1=2";
                if (grdList.CurrentRow != null)
                {
                    string tenketqua = Utility.sDbnull(grdList.GetValue("Ten_KQ"));
                    _rowfilter = string.Format("Ten_KQ='{0}'", tenketqua);
                 
                }
                else
                {
                    _rowfilter = "1=2";
                }
                m_dtKetQua.DefaultView.RowFilter = _rowfilter;
                m_dtKetQua.AcceptChanges();
            }
            catch (Exception exception)
            {
               // Console.WriteLine(exception);
               // throw;
            }
        }

        private void cmdExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //Janus.Windows.GridEX.GridEXRow[] gridExRows = grdList.GetCheckedRows();
                if (grdKetQua.RowCount <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu", "Thông báo");
                    grdKetQua.Focus();
                    return;
                }
                string tieude =string.Format("{0}_{1}", Utility.sDbnull(grdList.GetValue("Ten_KQ"))) ;
                saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                saveFileDialog1.FileName = string.Format("{0}.xls", tieude);
                //saveFileDialog1.ShowDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string sPath = saveFileDialog1.FileName;
                    var fs = new FileStream(sPath, FileMode.Create);
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

        private void cmdGetData_Click(object sender, EventArgs e)
        {
            GetData();
        }
    }
}

