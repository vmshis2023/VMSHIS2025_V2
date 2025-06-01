using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VMS.HIS.DAL;
using VNS.Libs;
using SubSonic;

namespace VMS.HIS.Danhmuc.UI
{
    public partial class frm_dmucthamsohethong : Form
    {
        private DataTable m_dtThamso = new DataTable();
        private string thamSo = "";
        public frm_dmucthamsohethong()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            thamSo = "";
        }
        public frm_dmucthamsohethong(string sThamSo)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            thamSo = sThamSo;
        }
        private bool InVali()
        {
            if (grdList.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một thông tin để thực hiện xóa", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            bool bExist = false;

            return true;
        }
        void ModifyCommand()
        {
            try
            {
                cmdAdd.Enabled = cmdDelete.Enabled = grdList.RowCount > 0 && grdList.CurrentRow.RowType == RowType.Record;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        private void frm_dmucthamsohethong_Load(object sender, EventArgs e)
        {
            if (thamSo != "")
            {
                m_dtThamso =
                    new Select().From(SysSystemParameter.Schema)
                        .Where(SysSystemParameter.Columns.SDataType)
                        .IsEqualTo(thamSo)
                        .ExecuteDataSet()
                        .Tables[0];
            }
            else
            {
                m_dtThamso = new Select().From(SysSystemParameter.Schema).ExecuteDataSet().Tables[0]; 
            }
            Utility.SetDataSourceForDataGridEx(grdList, m_dtThamso, true, true, "1=1", "");
            ModifyCommand();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frm_themmoi_thamsohethong frm = new frm_themmoi_thamsohethong();
                frm.txtID.Text = "-1";
                frm.em_Action = action.Insert;
                frm.p_dtThamsohethong = m_dtThamso;
                frm.grd = grdList;
                frm.ShowDialog();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }

}

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            try
            {
                frm_themmoi_thamsohethong frm = new frm_themmoi_thamsohethong();

                frm.txtID.Text = Utility.sDbnull(grdList.GetValue(SysSystemParameter.Columns.Id));
                frm._sValue = Utility.sDbnull(grdList.GetValue(SysSystemParameter.Columns.SValue));
                frm.em_Action = action.Update;
                frm.p_dtThamsohethong = m_dtThamso;
                frm.grd = grdList;
                frm.ShowDialog();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {

            try
            {
                if (!InVali()) return;
                if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin  không", "Thông báo", true))
                {
                    foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdList.GetCheckedRows())
                    {
                        if (new Delete().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.Id).IsEqualTo(Utility.Int32Dbnull(gridExRow.Cells[SysSystemParameter.Columns.Id].Value, -1)).Execute() > 0)
                        {
                            gridExRow.Delete();
                        }

                    }
                    grdList.UpdateData();
                    grdList.Refresh();

                    m_dtThamso.AcceptChanges();
                    Utility.ShowMsg("Bạn xóa thông tin thành công", "thông báo");
              
                    ModifyCommand();

                }

            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
