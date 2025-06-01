using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VietBaIT.CommonLibrary;
using VietBaIT.HISLink.LoadDataCommon.CheckThe_BH;


namespace VietBaIT.HISLink.UI.ControlUtility.LichSuKcb_CheckThe
{
    public partial class frm_LichSuKCB : Form
    {
        public KQNhanLichSuKCB objLichSuKcb;
        public KQNhanLichSuKCBBS objLichSuKcb2018;
        private string _kieuthongtu = "366";
        public frm_LichSuKCB(string kieuthongtu)
        {
            _kieuthongtu = kieuthongtu;
            InitializeComponent();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_LichSuKCB_Load(object sender, EventArgs e)
        {
            try
            {
                if (_kieuthongtu == "366")
                {
                    grdLichSuKCB.DataSource = objLichSuKcb2018.dsLichSuKCB2018;
                }
                else
                {
                    grdLichSuKCB.DataSource = objLichSuKcb.dsLichSuKCB;
                }
                
            }
            catch (Exception exception)
            {
               Utility.ShowMsg(exception.Message);
            }
        }

        private void frm_LichSuKCB_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void frm_LichSuKCB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit_Click(cmdExit, new EventArgs());
        }

       
    }
}
