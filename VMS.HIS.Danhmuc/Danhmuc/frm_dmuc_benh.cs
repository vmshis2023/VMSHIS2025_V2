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
    public partial class frm_dmuc_benh : Form
    {
        private int v_Disease_Id = -1;
        private DataTable m_dtDiseaseType=new DataTable();
        public frm_dmuc_benh()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            printPreviewDialog1.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            m_dtDiseaseType = new Select().From(DmucLoaibenh.Schema).ExecuteDataSet().Tables[0];
            grdDiease.ApplyingFilter+=new CancelEventHandler(grdDiease_ApplyingFilter);
            grdDiease.SelectionChanged+=new EventHandler(grdDiease_SelectionChanged);
            grdDiease.FilterApplied+=new EventHandler(grdDiease_FilterApplied);
        }

        private void frm_dmuc_benh_Load(object sender, EventArgs e)
        {
            
            IntialData();
            SearchMethod();
            ModifyCommand();
        }
        private void IntialData()
        {
            DataBinding.BindDataCombox(cboDieaseType, m_dtDiseaseType, DmucLoaibenh.Columns.MaLoaibenh, DmucLoaibenh.Columns.TenLoaibenh);
        }
        /// <summary>
        /// hàm thực hiện trạng thais của các nút trong form
        /// </summary>
        private void ModifyCommand()
        {
            try
            {
                cmdEdit.Enabled = grdDiease.RowCount > 0;
                cmdDelete.Enabled = grdDiease.RowCount > 0;
                cmdPrint.Enabled = grdDiease.RowCount > 0;
                if (grdDiease.RowCount > 0)
                {
                    cmdEdit.Enabled = grdDiease.CurrentRow.RowType == RowType.Record;
                    cmdDelete.Enabled = grdDiease.CurrentRow.RowType == RowType.Record;
                }
            }
            catch (Exception)
            {
                
                
            }
           
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_dmuc_benh_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)toolStripButton1.PerformClick();
            if(e.KeyCode==Keys.F3)cmdSearch.PerformClick();
            if (e.Control&&e.KeyCode == Keys.N) cmdNew.PerformClick();
            if (e.Control&&e.KeyCode == Keys.E) cmdEdit.PerformClick();
            if (e.Control && e.KeyCode == Keys.D) cmdDelete.PerformClick();
            if(e.KeyCode==Keys.F4)cmdPrint.PerformClick();
        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            try
            {
                
                frm_themmoi_benh frm = new frm_themmoi_benh();
                frm.m_dtDisease = m_dtBenh;
                frm.grdDiease = grdDiease;
                frm.em_Action = action.Insert;
                frm.ShowDialog();
                ModifyCommand();
            }catch(Exception exception)
            {
                ModifyCommand();
            }
           
           
        }
        private DataTable m_dtBenh=new DataTable();
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            SearchMethod();
        }

        private void SearchMethod()
        {
            try
            {
                m_dtBenh = SPs.DmucBenhGetlist().GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdDiease, m_dtBenh, true, true, "", "");
                ModifyCommand();
            }
            catch (Exception)
            {
                
                ModifyCommand();
            }
           
            //grdDiease.DataSource = m_dtBenh;
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isValidDelete()) return;
                if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin đang chọn  hay không", "Thông báo", true))
                {
                    int record = new Delete().From(DmucBenh.Schema)
                        .Where(DmucBenh.Columns.IdBenh).IsEqualTo(v_Disease_Id).Execute();
                    if (record > 0)
                    {
                        grdDiease.CurrentRow.Delete();
                        grdDiease.UpdateData();
                        grdDiease.Refresh();
                        m_dtBenh.AcceptChanges();
                        DataRow[] arrDr = globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.IdBenh + "=" + v_Disease_Id.ToString());
                        if (arrDr.Length > 0)
                            globalVariables.gv_dtDmucBenh.Rows.Remove(arrDr[0]);
                        globalVariables.gv_dtDmucBenh.AcceptChanges();
                        Utility.ShowMsg("Bạn xóa thông tin bệnh đang chọn thành công", "Thông báo");
                    }
                    else
                    {
                        Utility.ShowMsg("Có lỗi trong khi xóa thông tin bệnh", "Thông báo");
                    }
                }
                ModifyCommand();
                
            }catch( Exception exception)
            {
                
            }
          
        }

        private bool isValidDelete()
        {
            
            SqlQuery q = new Select().From(KcbChandoanKetluan.Schema);
            SqlQuery q1 = new Select().From(KcbChandoanKetluan.Schema);

            if (q.Where(KcbChandoanKetluan.Columns.MabenhChinh).IsEqualTo(v_Disease_Id).GetRecordCount() > 0)
            {
                Utility.ShowMsg("ICD bạn đang chọn xóa đã được sử dụng nên bạn không thể xóa");
                return false;
            }
            if (q1.Where(KcbChandoanKetluan.Columns.MabenhPhu).IsEqualTo(v_Disease_Id).GetRecordCount() > 0)
            {
                Utility.ShowMsg("ICD bạn đang chọn xóa đã được sử dụng nên bạn không thể xóa");
                return false;
            }
            return true;
        }

       
        private void grdDiease_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdDiease.CurrentRow != null && grdDiease.CurrentRow.RowType == RowType.Record)
                    v_Disease_Id = Utility.Int32Dbnull(grdDiease.CurrentRow.Cells[DmucBenh.Columns.IdBenh].Value, -1);
                ModifyCommand();
            }
            catch (Exception)
            {

                ModifyCommand();
            }
          
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (v_Disease_Id <= -1) return;
                frm_themmoi_benh frm = new frm_themmoi_benh();
                frm.m_dtDisease = m_dtBenh;
                frm.grdDiease = grdDiease;
                frm.em_Action = action.Update;
                frm.txtDiease_ID.Text = v_Disease_Id.ToString();
                frm.ShowDialog();
                ModifyCommand();
            }
            catch (Exception)
            {

                ModifyCommand();
            }
           
        }

        private void grdDiease_FilterApplied(object sender, EventArgs e)
        {
            ModifyCommand();
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
           // printPreviewDialog1.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void grdDiease_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }

        private void cmdSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdDiease.GetCheckedRows())
                {
                    new Update(DmucBenh.Schema)
                        .Set(DmucBenh.Columns.MaLoaibenh).EqualTo(Utility.Int32Dbnull(cboDieaseType.SelectedValue, -1))
                        .Where(DmucBenh.Columns.IdBenh).IsEqualTo(Utility.Int32Dbnull(gridExRow.Cells[DmucBenh.Columns.IdBenh].Value, -1))
                        .Execute();

                }
                Utility.ShowMsg("Bạn thực hiện thành công");
            }
            catch (Exception)
            {
                
                
            }
           
        }

        private void grdDiease_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            var objValue = new object();
            if (e.Column.Key == DmucBenh.Columns.MaBenh)
            {
                objValue = e.Value;
                SqlQuery q = new Select().From(DmucBenh.Schema)
                .Where(DmucBenh.Columns.MaBenh).IsEqualTo(Utility.sDbnull(objValue))
                .And(DmucBenh.Columns.IdBenh).IsNotEqualTo(Utility.Int32Dbnull(grdDiease.CurrentRow.Cells[DmucBenh.Columns.IdBenh].Value, -1));
                if (string.IsNullOrEmpty(objValue.ToString().Trim()))
                {
                    e.Cancel = true;
                    Utility.ShowMsg("Mã bệnh không được để trống", "Thông báo thiếu thông tin", MessageBoxIcon.Warning);
                }
                if (q.GetRecordCount() > 0)
                {
                    e.Cancel = true;
                    Utility.ShowMsg("Đã tồn tại mã bệnh", "Thông báo tồn tại", MessageBoxIcon.Warning);
                }
            }
            else if (e.Column.Key == DmucBenh.Columns.TenBenh)
            {
                objValue = e.Value;
                if (string.IsNullOrEmpty(objValue.ToString().Trim()))
                {
                    e.Cancel = true;
                    Utility.ShowMsg("Tên bệnh không được để trống", "Thông báo thiếu thông tin", MessageBoxIcon.Warning);
                }
            }
        }

        private void grdDiease_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            try
            {
                int record = new Update(DmucBenh.Schema)
                 .Set(DmucBenh.Columns.TenBenh).EqualTo(Utility.sDbnull(grdDiease.CurrentRow.Cells[DmucBenh.Columns.TenBenh].Value, ""))
                 .Set(DmucBenh.Columns.MaBenh).EqualTo(Utility.sDbnull(grdDiease.CurrentRow.Cells[DmucBenh.Columns.MaBenh].Value, ""))
                 .Set(DmucBenh.Columns.MotaThem).EqualTo(Utility.sDbnull(grdDiease.CurrentRow.Cells[DmucBenh.Columns.MotaThem].Value, ""))
                 .Where(DmucBenh.Columns.IdBenh).IsEqualTo(Utility.Int32Dbnull(grdDiease.CurrentRow.Cells[DmucBenh.Columns.IdBenh].Value, -1)).Execute();

                if (record > 0)
                {
                    grdDiease.UpdateData();
                    grdDiease.Refresh();
                    m_dtBenh.AcceptChanges();
                    Utility.ShowMsg("Bạn thực hiện cập nhập thông tin thành công");
                }
                else
                {
                    Utility.ShowMsg("Lỗi trong quá trình cập nhập dữ liệu");
                    return;
                }
            }
            catch (Exception exception)
            {

            }
        }

    }
}
