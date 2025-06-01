using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VMS.HIS.DAL;
using AggregateFunction = Janus.Windows.GridEX.AggregateFunction;
using System.IO;
using VNS.Libs;
using VNS.Properties;
using CrystalDecisions.CrystalReports.Engine;
using System.Drawing.Printing;
using VNS.HIS.Classes;
using VNS.HIS.UI.Baocao;
using VNS.HIS.UI.THANHTOAN;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.HIS.UI.DANHMUC;
using Excel;
namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_xemthongtingia : Form
    {
        private DataTable dtData=new DataTable();
      
        public frm_xemthongtingia()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KeyPreview = true;
            InitEvents();
        }
        public frm_xemthongtingia(DataTable dtData)
        {
            InitializeComponent();
          
            Utility.SetVisualStyle(this);
            this.dtData = dtData;
            InitEvents();
        }
        void InitEvents()
        {
            cmdCancel.Click += cmdCancel_Click;
            cmdSave.Click += cmdSave_Click;
            this.Load+=frm_xemthongtingia_Load;
        }

        void cmdSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
        /// <summary>
        /// hàm thực hiện việc load thông tin của tiếp đón lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_xemthongtingia_Load(object sender, EventArgs e)
        {
            Utility.SetDataSourceForDataGridEx(grdGiaCLS, dtData, true, true, "1=1", "stt_hthi_loaidvu,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");
        }
       
    }
}
