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

namespace VNS.RISLink.Bussiness.UI
{
    public partial class frm_log : Form
    {
        public frm_log()
        {
            InitializeComponent();
            dtFromDate.Value = dtToDate.Value = globalVariables.SysDate;
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {

            int idx = cboAct.SelectedIndex;
            if (idx >= 0) idx -= 1;
            DataTable dt_Log = SPs.SpSearchLog(Utility.sDbnull(txtNguoiTao.Text.Trim(), "")
                , Utility.sDbnull(txtNoiDung.Text.Trim(), ""),
                chkByDate.Checked ? dtFromDate.Value : Convert.ToDateTime("01/01/1900"),
                chkByDate.Checked ? dtToDate.Value : Convert.ToDateTime("31/12/9999"), idx, Utility.sDbnull(txtIp.Text.Trim(), "")).GetDataSet().Tables[0];
           Utility.SetDataSourceForDataGridEx(grdThongTin, dt_Log, true, true, "1=1", "Log_ID desc");
           
        }

        private void linkF5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtFromDate.Value = dtToDate.Value = globalVariables.SysDate;
            txtNguoiTao.Text = txtNoiDung.Text = "";
        }

        private void frm_log_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3) cmdSearch.PerformClick();
        }
    }
}
