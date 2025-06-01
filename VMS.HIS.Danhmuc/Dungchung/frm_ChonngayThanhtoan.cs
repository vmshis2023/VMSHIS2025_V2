using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
namespace VNS.HIS.UI.Forms.Cauhinh
{
    public partial class frm_ChonngayThanhtoan : Form
    {
        public string v_Patient_Code = "";
        public DateTime pdt_InputDate = globalVariables.SysDate;
        public bool mv_blnCancel = true;
        public frm_ChonngayThanhtoan()
        {
            InitializeComponent();
           radCurrentDate.CheckedChanged+=new EventHandler(radCurrentDate_CheckedChanged);
            radEditDate.CheckedChanged+=new EventHandler(radEditDate_CheckedChanged);
            radRegisterDate.CheckedChanged+=new EventHandler(radRegisterDate_CheckedChanged);
            dtCreateDate.Value = globalVariables.SysDate;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            mv_blnCancel = true;
            this.Close();
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            mv_blnCancel = false;
            pdt_InputDate = dtCreateDate.Value;
            this.Close();
        }

        private void frm_ChonngayThanhtoan_Load(object sender, EventArgs e)
        {

        }

        private void radRegisterDate_CheckedChanged(object sender, EventArgs e)
        {
            dtCreateDate.Value = pdt_InputDate;
        }

        private void frm_ChonngayThanhtoan_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if(e.Control&&(e.KeyCode==Keys.A || e.KeyCode==Keys.S))cmdAccept.PerformClick();
        }

        private void radEditDate_CheckedChanged(object sender, EventArgs e)
        {
            dtCreateDate.ReadOnly = !radEditDate.Checked;
            dtCreateDate.Focus();
        }

        private void radCurrentDate_CheckedChanged(object sender, EventArgs e)
        {
            dtCreateDate.Value = globalVariables.SysDate;
        }

        private void dtCreateDate_ValueChanged(object sender, EventArgs e)
        {

        }

    }
}
