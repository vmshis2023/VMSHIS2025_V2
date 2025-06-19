using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;

namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_capnhat_nhombc : Form
    {
        #region Khai bao bien
        public SysReport objReport;
        public action m_enAct = action.Insert;
        public Janus.Windows.GridEX.GridEX grdList;
        public DataTable dt_data = new DataTable();
      
        #endregion

        public frm_capnhat_nhombc()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
            Utility.SetVisualStyle(this);
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

      

        private void frm_capnhat_nhombc_Load(object sender, EventArgs e)
        {
            DataBinding.BindDataCombox(cboNhomBC, THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOMBAOCAO",true), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
           
          
        }

        private bool CheckValidData()
        {

            if (Utility.sDbnull(cboNhomBC.SelectedValue) == "" || Utility.sDbnull(cboNhomBC.SelectedValue) == "-1")
            {
                Utility.ShowMsg("Cần chọn nhóm báo cáo. Nhấn thoát nếu muốn hủy thao tác", "Thông báo", MessageBoxIcon.Information);
                cboNhomBC.Focus();
                return false;
            }
           
          
            return true;
        }

      

       

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!CheckValidData()) return;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void frm_capnhat_nhombc_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if(e.KeyCode==Keys.S&&e.Control)cmdSave.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
           

        }

    }
}
