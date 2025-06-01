using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VNS.HIS.UI.THUOC
{
    public partial class frm_ChonNgayCapPhat : Form
    {
        public DateTime ngaycapphat { get; set; }
        public bool b_Cancel = false;
        public frm_ChonNgayCapPhat()
        {
            InitializeComponent();
            dtNgaycapPhat.Value = DateTime.Now;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_ChonNgayCapPhat_Load(object sender, EventArgs e)
        {
            dtNgaycapPhat.Value = ngaycapphat;
        }

        private void cmdChapNhan_Click(object sender, EventArgs e)
        {
            ngaycapphat = dtNgaycapPhat.Value;
            b_Cancel = true;
            this.Close();
        }
    }
}
