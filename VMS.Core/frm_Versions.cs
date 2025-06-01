using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
using System.IO;
using System.Diagnostics;
namespace CIS2008
{
    public partial class frm_Versions : Form
    {
        public frm_Versions()
        {
            InitializeComponent();
            this.KeyDown += frm_Versions_KeyDown;
            this.Load += frm_Versions_Load;
            grdList.UpdatingCell += grdList_UpdatingCell;
            this.FormClosing += frm_Versions_FormClosing;
        }

        void frm_Versions_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        void frm_Versions_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtData = new DataTable();
                dtData.Columns.AddRange(new DataColumn[] { new DataColumn("sDllName", typeof(string)), new DataColumn("sVersion", typeof(string)), new DataColumn("sDate", typeof(string)), new DataColumn("createddate", typeof(DateTime)) });

                List<string> lstDllFiles = Directory.GetFiles(Application.StartupPath, "*VMS.*.dll", SearchOption.TopDirectoryOnly).ToList<string>();
                foreach (string sfile in lstDllFiles)
                {
                    FileVersionInfo _FileVersionInfo = FileVersionInfo.GetVersionInfo(sfile);
                    System.IO.FileInfo fI = new FileInfo(sfile);
                    long ticks = fI.LastWriteTime.Ticks;
                   

                    DataRow newRow = dtData.NewRow();
                    newRow["sDllName"] = Path.GetFileName(sfile);
                    newRow["sVersion"] = _FileVersionInfo.ProductVersion;
                    newRow["sDate"] = fI.LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss");
                    newRow["createddate"] = fI.LastWriteTime;
                    dtData.Rows.Add(newRow);
                }
                dtData.AcceptChanges();
                Utility.SetDataSourceForDataGridEx_Basic(grdList, dtData, true, true, "1=1", "createddate desc");
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            
        }

        void grdList_UpdatingCell(object sender, Janus.Windows.GridEX.UpdatingCellEventArgs e)
        {
            
           
        }

        void frm_Versions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();

        }
    }
}
