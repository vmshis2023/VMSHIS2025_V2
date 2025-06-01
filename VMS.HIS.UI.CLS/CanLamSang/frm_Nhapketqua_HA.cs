using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;

namespace VNS.HIS.UI.Forms.HinhAnh
{
    public partial class frm_Nhapketqua_HA : Form
    {
        public bool b_Cancel = false;
        public string KetQua = "";
        public frm_Nhapketqua_HA()
        {
            InitializeComponent();
            
            this.KeyPreview = true;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Nhapketqua_Load(object sender, EventArgs e)
        {
            //txtsDesc.Rtf = sDesc;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            KetQua = txtsDesc.Rtf;
            b_Cancel = true;
            this.Close();
        }

        private void frm_Nhapketqua_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
        }

        private void uiGroupBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
