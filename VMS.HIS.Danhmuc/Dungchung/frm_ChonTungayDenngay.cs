using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.Libs;
namespace VNS.HIS.UI.Forms.Cauhinh
{
    public partial class frm_ChonTungayDenngay : Form
    {
        public string v_Patient_Code = "";
        public DateTime pdt_InputDate = globalVariables.SysDate;
        public bool b_Cancel = true;
        public bool _hienthinhanvien = false;
        public frm_ChonTungayDenngay()
        {
            InitializeComponent();
            this.KeyDown+=frm_ChonTungayDenngay_KeyDown;
        
            dtp_tungay.Value =dtp_denngay.Value= globalVariables.SysDate;
            dtp_denngay.Enabled = false;
        }
       
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(lblMsg, "", true);
            if (dtp_tungay.Value < pdt_InputDate)
            {
                Utility.SetMsg(lblMsg, string.Format("Từ ngày phải >= đến ngày "), true);
                dtp_tungay.Focus();
                return;
            }
            b_Cancel = false;
            this.Close();
        }

        private void frm_ChonTungayDenngay_Load(object sender, EventArgs e)
        {
            dtp_tungay.Focus();
        }

        private void radRegisterDate_CheckedChanged(object sender, EventArgs e)
        {
            dtp_tungay.Value = pdt_InputDate;
        }

        private void frm_ChonTungayDenngay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && _hienthinhanvien) this.ProcessTabKey(true);
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if(e.Control&&(e.KeyCode==Keys.A || e.KeyCode==Keys.S))cmdAccept.PerformClick();
        }


        private void opt_trongngay_CheckedChanged(object sender, EventArgs e)
        {
            dtp_tungay.Value = dtp_denngay.Value = globalVariables.SysDate;
            dtp_denngay.Enabled = false;
            dtp_tungay.Focus();
        }

        private void opt_tungay_denngay_CheckedChanged(object sender, EventArgs e)
        {
            dtp_tungay.Enabled= dtp_denngay.Enabled = true;
            dtp_tungay.Focus();
        }
    }
}
