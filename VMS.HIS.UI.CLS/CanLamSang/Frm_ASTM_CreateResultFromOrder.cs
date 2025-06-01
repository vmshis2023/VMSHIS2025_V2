using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.HLC.ASTM;
using VNS.Libs;

namespace VNS.HIS.UI.Forms.CanLamSang
{
    public partial class Frm_ASTM_CreateResultFromOrder : Form
    {
        DataTable dt = new DataTable();
        public Frm_ASTM_CreateResultFromOrder()
        {
            InitializeComponent();
            txtFileName._OnEnterMe += txtFileName__OnEnterMe;
            this.Load += Frm_ASTM_CreateResultFromOrder_Load;
        }

        void Frm_ASTM_CreateResultFromOrder_Load(object sender, EventArgs e)
        {
            cmdRefresh_Click(cmdRefresh, e);
        }

        void txtFileName__OnEnterMe()
        {
            try
            {
                if (txtFileName.myCode != "-1")
                {
                    dt = RocheCommunication.ReadOrders(txtFileName.myCode);
                    Utility.SetDataSourceForDataGridEx_Basic(grdList, dt, true, true, "1=1", "");
                    Utility.focusCell(grdList, "KQ");
                }
            }
            catch (Exception ex)
            {
                
                
            }
            
        }

        private void cmdCreate_Click(object sender, EventArgs e)
        {
            RocheCommunication.WriteResultMessage(Utility.Laygiatrithamsohethong("ASTM_RESULTS_FOLDER", @"\\192.168.1.254\Results\", false), dt);
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                string[] lstFiles = Directory.GetFiles(THU_VIEN_CHUNG.Laygiatrithamsohethong("ASTM_ORDERS_FOLDER", @"\\192.168.1.254\Orders", false));
                txtFileName.Init(lstFiles.ToList<string>());
            }
            catch (Exception ex)
            {
                
                
            }
        }
    }
}
