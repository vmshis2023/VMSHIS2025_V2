using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;

namespace VNS.HIS.UI.FORMs.BAOCAO.BHYT
{
    public partial class frm_BHYT : Form
    {
        
        public frm_BHYT()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KeyDown += new KeyEventHandler(frm_BHYT_KeyDown);
            TabBHYT.SelectedTabChanged += new Janus.Windows.UI.Tab.TabEventHandler(TabBHYT_SelectedTabChanged);
            
            bhyT_79A1.Init();
            bhyT_14A1.Init();
            bhyT_05A1.Init();
            bhyT_25A1.Init();
            bhyT_21A1.Init();
            bhyT_20A1.Init();
        }

        void TabBHYT_SelectedTabChanged(object sender, Janus.Windows.UI.Tab.TabEventArgs e)
        {
            
        }

        void frm_BHYT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) this.Close();
        }
    }
}
