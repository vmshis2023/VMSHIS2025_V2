using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VNS.UCs;

namespace VNS.HIS.UI.Forms.Cauhinh
{
    public partial class frm_cauhinhthamsohethong : Form
    {
        public frm_cauhinhthamsohethong()
        {
            InitializeComponent();
            
            cmdClose.Click += new EventHandler(cmdClose_Click);
            cmdSave.Click += new EventHandler(cmdSave_Click);
            this.KeyDown += new KeyEventHandler(frm_cauhinhthamsohethong_KeyDown);
            this.Load += new EventHandler(frm_cauhinhthamsohethong_Load);
        }

        void frm_cauhinhthamsohethong_Load(object sender, EventArgs e)
        {
            try
            {
                InitNow(pnlBHYT);
                InitNow(pnlCLS);
            }
            catch (Exception ex)
            {

                VNS.Libs.Utility.CatchException(ex);
            }
        }

        void frm_cauhinhthamsohethong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
                return;
            }
            if (e.Control && e.KeyCode == Keys.S)
            {
                cmdSave.PerformClick();
                return;
            }
        }

        void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveNow(pnlBHYT);
                SaveNow(pnlCLS);
            }
            catch (Exception ex)
            {

                VNS.Libs.Utility.CatchException(ex);
            }
           
        }
        void InitNow(Panel pnlParent)
        {
            foreach (ucTextSysparam ctrl in pnlParent.Controls)
            {
                ctrl.Init();
            }
        }
        void SaveNow(Panel pnlParent)
        {
            foreach (ucTextSysparam ctrl in pnlParent.Controls)
            {
                ctrl.Save();
            }
        }
        void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click_1(object sender, EventArgs e)
        {

        }
    }
}
