using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_dmuc_noiKCBBD : Form
    {
        #region "Declare Variable private"
     
      
        private int v_ClinicID = -1;

        #endregion
        #region "Contructor"
        public frm_dmuc_noiKCBBD()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            printPreviewDialog1.WindowState = System.Windows.Forms.FormWindowState.Maximized;
           // m_dtClinicCode=DmucNoiKCBBD.FetchByID(-1)
            grdInsClinic.SelectionChanged+=new EventHandler(grdInsClinic_SelectionChanged);
            grdInsClinic.ApplyingFilter+=new CancelEventHandler(grdInsClinic_ApplyingFilter);
           
        }
        #endregion

        #region "Method Of Common"
        /// <summary>
        /// đóng fỏm hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện khởi tạo thôgn tin 
        /// </summary>
        private void IntialData()
        {


            DataBinding.BindDataCombox(cboDieaseType, globalVariables.gv_dtDmucDiachinh.Select(DmucDiachinh.LoaiDiachinhColumn+"=0").CopyToDataTable(), DmucDiachinh.Columns.MaDiachinh, DmucDiachinh.Columns.TenDiachinh);
        }
        /// <summary>
        /// trạng thái của các nút
        /// </summary>
        private void ModifyCommand()
        {
            try
            {
                cmdEdit.Enabled = grdInsClinic.RowCount > 0;
                cmdDelete.Enabled = grdInsClinic.RowCount > 0;
                if (grdInsClinic.RowCount > 0)
                {
                    cmdEdit.Enabled = grdInsClinic.RowCount > 0 && grdInsClinic.CurrentRow.RowType == RowType.Record;
                    cmdDelete.Enabled = grdInsClinic.RowCount > 0 && grdInsClinic.CurrentRow.RowType == RowType.Record;
                }
                toolStripButton2.Enabled = grdInsClinic.RowCount > 0 && grdInsClinic.CurrentRow.RowType == RowType.Record;
            }catch(Exception exception)
            {
                
            }
           
        }
        /// <summary>
        /// tìm kiếm thông tin của Form
        /// </summary>
        private void MethodSearch()
        {
            SqlQuery q = new Select().From(DmucNoiKCBBD.Schema);
            string filter = "";
            if (!string.IsNullOrEmpty(txtClinicCode.Text))
            {
                if (Utility.DoTrim(filter) == "")
                    filter = DmucNoiKCBBD.Columns.MaKcbbd + "='" + Utility.DoTrim(txtClinicCode.Text) + "'";
                //q.And(DmucNoiKCBBD.Columns.MaKcbbd).IsEqualTo(txtClinicCode.Text);
            }
            if (!string.IsNullOrEmpty(txtClinicName.Text) && Utility.DoTrim( filter)!="")
            {
                q.And(DmucNoiKCBBD.Columns.TenKcbbd).Like("%" + txtClinicName.Text + "%");
                if (Utility.DoTrim(filter) == "")
                filter = DmucNoiKCBBD.Columns.TenKcbbd + " like %" + txtClinicName.Text + "%";
                else
                    filter =" AND "+ DmucNoiKCBBD.Columns.TenKcbbd + " like %" + txtClinicName.Text + "%";
            }
            if (cboDieaseType.SelectedIndex > 0)
            {

                q.And(DmucNoiKCBBD.Columns.MaDiachinh).IsEqualTo(Utility.sDbnull(cboDieaseType.SelectedValue, ""));
                if (Utility.DoTrim(filter) == "")
                    filter = DmucNoiKCBBD.Columns.MaDiachinh + " ='" + Utility.sDbnull(cboDieaseType.SelectedValue, "") + "'";
                else
                    filter = " AND " + DmucNoiKCBBD.Columns.MaDiachinh + " ='" + Utility.sDbnull(cboDieaseType.SelectedValue, "") + "'";
            }
            if (Utility.DoTrim(filter) == "")
                filter = "1=1";
            globalVariables.gv_dtDmucNoiKCBBD.DefaultView.RowFilter = filter;
            Utility.SetDataSourceForDataGridEx(grdInsClinic, globalVariables.gv_dtDmucNoiKCBBD, true, true, filter, DmucNoiKCBBD.TenKcbbdColumn.ColumnName);
        }
        #endregion

      
        #region "Method Event Form"
        /// <summary>
        /// hàm thực hiện tìm kiếm thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            MethodSearch();
            ModifyCommand();
        }
        private void frm_dmuc_noiKCBBD_Load(object sender, EventArgs e)
        {
            IntialData();
            MethodSearch();
            ModifyCommand();
        }
        /// <summary>
        /// hàm thư hiện thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_dmuc_noiKCBBD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) toolStripButton1.PerformClick();
            if (e.KeyCode == Keys.F3) cmdSearch.PerformClick();
            if (e.Control && e.KeyCode == Keys.N) cmdNew.PerformClick();
            if (e.Control && e.KeyCode == Keys.E) cmdEdit.PerformClick();
            if (e.Control && e.KeyCode == Keys.D) cmdDelete.PerformClick();
            if (e.KeyCode == Keys.F4) toolStripButton2.PerformClick();
        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            try
            {
                frm_themmoi_noiKCBBD frm = new frm_themmoi_noiKCBBD();
                frm.grdList = grdInsClinic;
               
                frm.em_Action = action.Insert;
                frm.ShowDialog();
            }
            catch (Exception)
            {
                
               
            }
         
        }
        /// <summary>
        /// hàm thực hiện việc sửa thôn tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdInsClinic.CurrentRow != null&&grdInsClinic.CurrentRow.RowType==RowType.Record)
                {
                    v_ClinicID = Utility.Int32Dbnull(grdInsClinic.CurrentRow.Cells[DmucNoiKCBBD.Columns.IdKcbbd].Value, -1);
                    frm_themmoi_noiKCBBD frm = new frm_themmoi_noiKCBBD();
                    frm.grdList = grdInsClinic;
                    frm.txtClinic_ID.Text = v_ClinicID.ToString();
                   
                    frm.em_Action = action.Update;
                    frm.ShowDialog();
                }
                 
            }
            catch (Exception)
            {
                
               
            }
           
        }
        /// <summary>
        /// di chuyển thông tin trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdInsClinic_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdInsClinic.CurrentRow != null)
                    v_ClinicID = Utility.Int32Dbnull(grdInsClinic.CurrentRow.Cells[DmucNoiKCBBD.Columns.IdKcbbd ].Value, -1);
                ModifyCommand();
            }
            catch (Exception)
            {
                
               
            }
           
        }
        /// <summary>
        /// xóa thông tin của ban ghi đang chọn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if(grdInsClinic.CurrentRow!=null&&grdInsClinic.CurrentRow.RowType==RowType.Record)
            {
                 string sClinicCode = Utility.sDbnull(grdInsClinic.GetValue(DmucNoiKCBBD.Columns.MaKcbbd),"");
                 string sClinicName = Utility.sDbnull(grdInsClinic.GetValue(DmucNoiKCBBD.Columns.TenKcbbd), "");
                SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema).Where(KcbLuotkham.Columns.MaKcbbd)
                    .IsEqualTo(sClinicCode);
                if(sqlQuery.GetRecordCount()>0)
                {
                    string strQuestion =
                        string.Format("Hiện tại {0} đã được dùng trong phần thông tin bệnh nhân,Bạn không thể xóa",
                                      sClinicName);

                    Utility.ShowMsg(strQuestion,"Thông báo",MessageBoxIcon.Warning);
                    grdInsClinic.Focus();
                    return;
                }
            if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin đang chọn hay không", "Thông báo", true))
            {
                int record = new Delete().From(DmucNoiKCBBD.Schema)
                    .Where(DmucNoiKCBBD.Columns.IdKcbbd).IsEqualTo(v_ClinicID).Execute();

                if (record > 0)
                {
                    grdInsClinic.CurrentRow.Delete();
                    grdInsClinic.UpdateData();
                    grdInsClinic.Refresh();
                    Utility.ShowMsg("Bạn xóa thông tin bệnh đang chọn thành công", "Thông báo");
                }
                else
                {
                    Utility.ShowMsg("Có lỗi trong khi xóa thông tin bệnh", "Thông báo");
                }
            }
            }
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtClinicName.Clear();
            txtClinicCode.Clear();
            cboDieaseType.SelectedIndex = 0;
        }

        private void grdInsClinic_FilterApplied(object sender, EventArgs e)
        {
            ModifyCommand();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
           
        }

        #endregion 
        /// <summary>
        /// hàm thực hiện lọc thông tin khi lọc trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdInsClinic_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }
    }
}
