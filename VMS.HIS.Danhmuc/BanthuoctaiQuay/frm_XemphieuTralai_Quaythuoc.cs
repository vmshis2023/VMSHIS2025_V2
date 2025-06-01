using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using AggregateFunction = Janus.Windows.GridEX.AggregateFunction;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using VNS.Properties;
using VNS.HIS.UI.Forms.NGOAITRU;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using VNS.HIS.Classes;
using VNS.HIS.UI.Forms.Cauhinh;
namespace VNS.HIS.UI.THANHTOAN
{
    public partial class frm_XemphieuTralai_Quaythuoc : Form
    {
        public bool m_blnCancel = true;
        long id_phieutralai  = -1;
        public frm_XemphieuTralai_Quaythuoc(long id_phieutralai)
        {
            InitializeComponent();
            this.id_phieutralai = id_phieutralai;
            Utility.SetVisualStyle(this);
            this.KeyDown+=new KeyEventHandler(frm_XemphieuTralai_Quaythuoc_KeyDown);

            cmdClose1.Click += new EventHandler(cmdClose1_Click);
        }

        void cmdClose1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        KCB_THANHTOAN _THANHTOAN = new KCB_THANHTOAN();
        /// <summary>
        /// /hàm thực hiện load thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_XemphieuTralai_Quaythuoc_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtData = SPs.ThuocLichsutralaithuoctaiquayXemchitiet(id_phieutralai).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdDSThuoctralai, dtData, false, true, "1=1", "ten_thuoc");
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
       
        /// <summary>
        /// hàm thực hiện thoát khỏi form hienj tại
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thự hiện việc hủy thanh toán
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_XemphieuTralai_Quaythuoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F4) cmdInphieu.PerformClick();
        }
        private void ModifyComamd()
        {
            cmdInphieu.Enabled = grdDSThuoctralai.GetDataRows().Count() > 0;
        }

        private void cmdInphieu_Click(object sender, EventArgs e)
        {
            try
            {
                new INPHIEU_THANHTOAN_NGOAITRU().In_Phieuchi_TraLaithuoc(id_phieutralai);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        
       
    }
}
