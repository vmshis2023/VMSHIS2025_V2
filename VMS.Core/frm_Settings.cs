using System;
using System.Windows.Forms;
using VNS.Properties;
using VNS.Libs;
using CIS2008;
using System.Collections.Generic;

namespace CIS.CoreApp
{
    public partial class frm_Settings : Form
    {
        public frm_Settings()
        {
            InitializeComponent();
            cmdLoadSysparams.Click += new EventHandler(cmdLoadSysparams_Click);
        }

        private void frm_Settings_Load(object sender, EventArgs e)
        {
            grdProperties.SelectedObject = PropertyLib._AppProperties;
        }
        void cmdLoadSysparams_Click(object sender, EventArgs e)
        {
            try
            {
                CIS2008.frm_Sysparams _Sysparams = new CIS2008.frm_Sysparams();
                _Sysparams.ShowDialog();
                //globalVariables.gv_dtSysparams = new Select().From(SysSystemParameter.Schema).ExecuteDataSet().Tables[0];
                //globalVariables.gv_dtSysTieude = new Select().From(SysTieude.Schema).ExecuteDataSet().Tables[0];
                //THU_VIEN_CHUNG.LoadThamSoHeThong();
                //Utility.ShowMsg("Đã nạp lại toàn bộ tham số hệ thống");
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            PropertyLib.SaveProperty(PropertyLib._AppProperties);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
            ToolStripMenuItem a = new ToolStripMenuItem();
            a.Click += new EventHandler(a_Click);
        }

        void a_Click(object sender, EventArgs e)
        {
            
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void cmdPrintSettings_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._MayInProperties);
            _Properties.ShowDialog();
        }
    }
}
