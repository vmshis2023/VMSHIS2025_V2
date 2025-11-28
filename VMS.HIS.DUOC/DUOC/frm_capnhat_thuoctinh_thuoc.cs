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

namespace VNS.HIS.UI.THUOC
{
    public partial class frm_capnhat_thuoctinh_thuoc : Form
    {
        #region Khai bao bien
        public SysReport objReport;
        public action m_enAct = action.Insert;
        public Janus.Windows.GridEX.GridEX grdList;
        public DataTable dt_data = new DataTable();
      
        #endregion

        public frm_capnhat_thuoctinh_thuoc()
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



        private void frm_capnhat_thuoctinh_thuoc_Load(object sender, EventArgs e)
        {
            try
            {
                DataBinding.BindDataCombobox(cbo_phanloaithuoc, THU_VIEN_CHUNG.LayDulieuDanhmucChung("PHANLOAITHUOC", true), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                DataBinding.BindDataCombobox(cbo_duongdung, THU_VIEN_CHUNG.LayDulieuDanhmucChung("DUONGDUNG", true), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                DataTable dtData = new Select().From(DmucLoaithuoc.Schema)
     .Where(DmucLoaithuoc.KieuThuocvattuColumn).IsEqualTo("THUOC")
     .ExecuteDataSet().Tables[0];
                DataBinding.BindDataCombobox(cbo_nhomduocly, dtData, DmucLoaithuoc.Columns.IdLoaithuoc, DmucLoaithuoc.Columns.TenLoaithuoc);//IdLoaithuoc
                dtData = new Select().From(DmucHoatchat.Schema)
     .Where(DmucHoatchat.Columns.TrangThai).IsEqualTo(1)
     .ExecuteDataSet().Tables[0];
                DataBinding.BindDataCombobox(cbo_hoatchat, dtData, DmucHoatchat.Columns.MaHoatchat, DmucHoatchat.Columns.TenHoatchat);//MaHoatchat
                dtData = new Select().From(DmucTinhchatthuoc.Schema)
     .ExecuteDataSet().Tables[0];
                DataBinding.BindDataCombobox(cbo_nhomduocly, dtData, DmucTinhchatthuoc.Columns.MaTinhchat, DmucTinhchatthuoc.Columns.TenTinhchat);//MaTinhchat
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }

        }

        private bool CheckValidData()
        {

            if (chk_phanloaithuoc.Checked &&  Utility.sDbnull(cbo_phanloaithuoc.SelectedValue) == "" || Utility.sDbnull(cbo_phanloaithuoc.SelectedValue) == "-1")
            {
                Utility.ShowMsg("Cần chọn Phân loại thuốc trước khi ghi. Nhấn thoát nếu muốn hủy thao tác");
                chk_phanloaithuoc.Focus();
                return false;
            }
            if (chk_duongdung.Checked && Utility.sDbnull(cbo_duongdung.SelectedValue) == "" || Utility.sDbnull(cbo_duongdung.SelectedValue) == "-1")
            {
                Utility.ShowMsg("Cần chọn Đường dùng trước khi ghi. Nhấn thoát nếu muốn hủy thao tác");
                cbo_duongdung.Focus();
                return false;
            }
            if (chk_nhomduocly.Checked && Utility.sDbnull(cbo_nhomduocly.SelectedValue) == "" || Utility.sDbnull(cbo_nhomduocly.SelectedValue) == "-1")
            {
                Utility.ShowMsg("Cần chọn Nhóm dược lý trước khi ghi. Nhấn thoát nếu muốn hủy thao tác");
                cbo_nhomduocly.Focus();
                return false;
            }
            if (chk_hoatchat.Checked && Utility.sDbnull(cbo_hoatchat.SelectedValue) == "" || Utility.sDbnull(cbo_hoatchat.SelectedValue) == "-1")
            {
                Utility.ShowMsg("Cần chọn Hoạt chất trước khi ghi. Nhấn thoát nếu muốn hủy thao tác");
                cbo_hoatchat.Focus();
                return false;
            }
            if (chk_tinhchat.Checked && Utility.sDbnull(cbo_tinhchat.SelectedValue) == "" || Utility.sDbnull(cbo_tinhchat.SelectedValue) == "-1")
            {
                Utility.ShowMsg("Cần chọn Tính chất trước khi ghi. Nhấn thoát nếu muốn hủy thao tác");
                cbo_tinhchat.Focus();
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
        private void frm_capnhat_thuoctinh_thuoc_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if(e.KeyCode==Keys.S&&e.Control)cmdSave.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
           

        }

        private void chk_phanloaithuoc_CheckedChanged(object sender, EventArgs e)
        {
            cbo_phanloaithuoc.Enabled = ((CheckBox)sender).Checked;
        }

        private void chk_duongdung_CheckedChanged(object sender, EventArgs e)
        {
            cbo_duongdung.Enabled = ((CheckBox)sender).Checked;
        }

        private void chk_nhomduocly_CheckedChanged(object sender, EventArgs e)
        {
            cbo_nhomduocly.Enabled = ((CheckBox)sender).Checked;
        }

        private void chk_hoatchat_CheckedChanged(object sender, EventArgs e)
        {
            cbo_hoatchat.Enabled = ((CheckBox)sender).Checked;
        }

        private void chk_tinhchat_CheckedChanged(object sender, EventArgs e)
        {
            cbo_tinhchat.Enabled = ((CheckBox)sender).Checked;
        }
    }
}
