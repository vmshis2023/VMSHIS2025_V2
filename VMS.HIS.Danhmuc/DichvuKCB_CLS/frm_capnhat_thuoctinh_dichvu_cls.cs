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
    public partial class frm_capnhat_thuoctinh_dichvu_cls : Form
    {
        #region Khai bao bien
        public SysReport objReport;
        public action m_enAct = action.Insert;
        public Janus.Windows.GridEX.GridEX grdList;
        public DataTable dt_data = new DataTable();
      
        #endregion

        public frm_capnhat_thuoctinh_dichvu_cls()
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



        private void frm_capnhat_thuoctinh_dichvu_cls_Load(object sender, EventArgs e)
        {
            DataTable m_dtDichvuCLS = new Select().From(DmucDichvucl.Schema).ExecuteDataSet().Tables[0];
            DataBinding.BindDataCombobox(cbo_loai_dichvu, m_dtDichvuCLS, DmucDichvucl.Columns.IdDichvu, DmucDichvucl.Columns.TenDichvu);
            DataBinding.BindDataCombobox(cbo_LoaiPttt, THU_VIEN_CHUNG.LayDulieuDanhmucChung("LOAIPTTT", true), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
        }

        private bool CheckValidData()
        {

            if (Utility.sDbnull(cbo_loai_dichvu.SelectedValue) == "" || Utility.sDbnull(cbo_loai_dichvu.SelectedValue) == "-1")
            {
                Utility.ShowMsg("Cần chọn Loại dịch vụ để cập nhật cho các dịch vụ đang chọn. Nhấn thoát nếu muốn hủy thao tác", "Thông báo", MessageBoxIcon.Information);
                cbo_loai_dichvu.Focus();
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
        private void frm_capnhat_thuoctinh_dichvu_cls_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if(e.KeyCode==Keys.S&&e.Control)cmdSave.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
           

        }

        private void chk_Loaidvu_CheckedChanged(object sender, EventArgs e)
        {
            cbo_loai_dichvu.Enabled = ((CheckBox)sender).Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            cbo_LoaiPttt.Enabled = ((CheckBox)sender).Checked;
        }
    }
}
