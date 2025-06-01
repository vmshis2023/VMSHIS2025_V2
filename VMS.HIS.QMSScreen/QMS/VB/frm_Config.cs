using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Properties;
using VNS.Libs;

namespace VMS.QMS
{
  
    public partial class frm_Config : Form
    {
       // public SetCauHinh SetCauHinhParameter;

        public frm_Config()
        {
            InitializeComponent();
            if (PropertyLib._QMSColorProperties == null) PropertyLib._QMSColorProperties = PropertyLib.GetQMSColorProperties();
            this.FormClosing += new FormClosingEventHandler(frm_CauHinh_FormClosing);
            
        }
        public QMSPrintProperties _qmsPrintProperties;
        private void frm_Config_Load(object sender, EventArgs e)
        {
            grdProperties.SelectedObject = _qmsPrintProperties;
        }

     
        private void frm_CauHinh_FormClosing(object sender, FormClosingEventArgs e)
        {

         PropertyLib.SaveProperty(_qmsPrintProperties);
        }

        private void grdProperties_SelectedObjectsChanged(object sender, EventArgs e)
        {

        }

        private void cmdColor_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._QMSColorProperties);
            frm.ShowDialog();
        }
    }
}
