using System;
using System.Data;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VNS.Libs;
using VMS.HIS.DAL;
using SubSonic;
namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_dmuc_phongban : Form
    {
        #region "THUOC TINH"
        private string RowFilter = "1=1";
        private bool m_blnLoaded = false;
        private  DataTable dsTable=new DataTable();
        private DataTable dsDepartment = new DataTable();
        #endregion

        #region "KHOI TAO FORM"
        public frm_dmuc_phongban()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            printPreviewDialog1.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            IntitalEventControl();
            grdPhongBan.UpdatingCell += GrdPhongBan_UpdatingCell;
            this.KeyPreview = true;
            this.KeyDown+=new KeyEventHandler(frm_dmuc_phongban_KeyDown);
        }

        private void GrdPhongBan_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                int id_khoaphong = Utility.Int32Dbnull(grdPhongBan.GetValue(DmucKhoaphong.Columns.IdKhoaphong));
                if (e.Column.Key == "ma_xml")
                {
                   
                    if (globalVariables.isSuperAdmin && Utility.sDbnull(e.Value).Length > 0)
                    {
                        SqlQuery q =
                  new Select().From(DmucKhoaphong.Schema).Where(DmucKhoaphong.Columns.MaXml).IsEqualTo(
                      Utility.sDbnull(e.Value)).And(DmucKhoaphong.Columns.IdKhoaphong).IsNotEqualTo(id_khoaphong);

                        if (q.GetRecordCount() > 0)
                        {
                            Utility.ShowMsg("Đã tồn tại khoa phòng có mã XML" + Utility.sDbnull(e.Value) + ". Mời bạn nhập mã khác");
                            e.Value = e.InitialValue;
                            return;
                        }
                        int num = new Update(DmucKhoaphong.Schema).Set(DmucKhoaphong.Columns.MaXml).EqualTo(e.Value).Where(DmucKhoaphong.Columns.IdKhoaphong).IsEqualTo(id_khoaphong).Execute();
                    }
                    else
                    {
                        e.Value = e.InitialValue;
                        return;
                    }
                }
                else if (e.Column.Key == DmucKhoaphong.Columns.SttHthi)
                {
                    if (!SubSonic.Sugar.Numbers.IsInteger(Utility.sDbnull(e.Value)))
                    {
                       
                        Utility.ShowMsg("Chỉ được phép nhập số");
                        e.Value = e.InitialValue;
                        return;
                    }
                    else if (Utility.Int32Dbnull(e.Value) < 0)
                    {
                        
                        Utility.ShowMsg("Giá trị nhập vào phải lớn hơn 0");
                        e.Value = e.InitialValue;
                        return;
                    }
                    int num = new Update(DmucKhoaphong.Schema).Set(e.Column.Key).EqualTo(Utility.Int32Dbnull(e.Value)).Where(DmucKhoaphong.Columns.IdKhoaphong).IsEqualTo(id_khoaphong).Execute();
                }
                else if (e.Column.Key == DmucKhoaphong.Columns.MaKhoaphong)
                {
                    SqlQuery q = new Select().From(DmucKhoaphong.Schema)
                    .Where(DmucKhoaphong.Columns.MaKhoaphong).IsEqualTo(Utility.sDbnull(e.Value))
                    .And(DmucKhoaphong.Columns.IdKhoaphong)
                    .IsNotEqualTo(Utility.Int32Dbnull(grdPhongBan.CurrentRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value, -1));
                    if (string.IsNullOrEmpty(e.Value.ToString().Trim()))
                    {
                      
                        Utility.ShowMsg("Mã khoa(phòng) không được để trống", "Thông báo thiếu thông tin", MessageBoxIcon.Warning);
                        e.Value = e.InitialValue;
                        return;
                    }
                    if (q.GetRecordCount() > 0)
                    {
                       
                        Utility.ShowMsg("Đã tồn tại mã khoa(phòng)", "Thông báo tồn tại", MessageBoxIcon.Warning);
                        e.Value = e.InitialValue;
                        return;
                    }
                    int num = new Update(DmucKhoaphong.Schema).Set(e.Column.Key).EqualTo(Utility.sDbnull(e.Value)).Where(DmucKhoaphong.Columns.IdKhoaphong).IsEqualTo(id_khoaphong).Execute();
                }
                else if (e.Column.Key == DmucKhoaphong.Columns.TenKhoaphong)
                {
                    if (string.IsNullOrEmpty(e.Value.ToString().Trim()))
                    {
                        Utility.ShowMsg("Tên khoa(phòng) không được để trống", "Thông báo thiếu thông tin", MessageBoxIcon.Warning);
                        e.Value = e.InitialValue;
                        return;
                    }
                    int num = new Update(DmucKhoaphong.Schema).Set(e.Column.Key).EqualTo(Utility.sDbnull(e.Value)).Where(DmucKhoaphong.Columns.IdKhoaphong).IsEqualTo(id_khoaphong).Execute();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        #endregion
        #region "HAM DUNG CHO SỰ KIỆN CỦA FORM"

        /// <summary>
        /// THUC HIEN THAO TAC THEM MOI KHOA PHONG
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNew_Click(object sender, EventArgs e)
        {
            frm_themmoi_phongban frm = new frm_themmoi_phongban();
            frm.dsKhoaphong = dsTable;
            frm.m_enAction = action.Insert;
            frm.ShowDialog();
            ModifyCommand();

        }
        /// <summary>
        /// THUC HIEN SUA THONG TIN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEdit_Click(object sender, EventArgs e)
        {
            if (grdPhongBan.CurrentRow != null)
            {
                if(v_Department_id<=-1)return;
                frm_themmoi_phongban frm = new frm_themmoi_phongban();
                frm.dsKhoaphong = dsTable;
                frm.txtID.Text = v_Department_id.ToString();
                frm.drKhoaphong = Utility.FetchOnebyCondition(dsTable,DmucKhoaphong.Columns.IdKhoaphong+ "=" + v_Department_id);
                frm.m_enAction = action.Update;
                frm.ShowDialog();
                
                ModifyCommand();
            }
        }

        /// <summary>
        /// THUC HIEN XOA THONG TIN RECORD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private int v_Department_id = -1;
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (grdPhongBan.RowCount <= 0)
            {
                Utility.ShowMsg("Hiện chưa có bản ghi nào chọn", "Thông báo");
                grdPhongBan.Focus();
                return;
            }
            foreach (GridEXRow row in grdPhongBan.GetCheckedRows())
            {
                v_Department_id = Utility.Int32Dbnull(row.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value, -1);
                SqlQuery q = new Select().From(KcbDangkyKcb.Schema)
                    .Where(KcbDangkyKcb.Columns.IdKhoakcb).IsEqualTo(v_Department_id)
                    .Or(KcbDangkyKcb.Columns.IdCha).IsEqualTo(v_Department_id)
                    .Or(KcbDangkyKcb.Columns.IdPhongkham).IsEqualTo(v_Department_id);
                if (q.GetRecordCount() > 0)
                {

                    Utility.ShowMsg("Khoa phòng này đã sử dụng nên không thể xóa", "Thông báo");
                    grdPhongBan.Focus();
                    return;

                }
                if (grdPhongBan.CurrentRow != null)
                {
                    if (Utility.AcceptQuestion("Bạn có muốn xoá bản ghi này không", "Thông bảo", true))
                    {
                        DmucKhoaphong.Delete(DmucKhoaphong.Columns.IdKhoaphong, v_Department_id);
                        DataRow[] array = dsTable.Select(DmucKhoaphong.Columns.IdKhoaphong + "=" + v_Department_id);
                        if (array.GetLength(0) > 0)
                        {
                            array[0].Delete();
                            dsTable.AcceptChanges();
                        }
                        row.Delete();
                    }

                    ModifyCommand();
                }
            }
           
        }
        
        private void RemoveObjectChecked()
        {
            try
                 {
         int Count =dsTable.DefaultView.Count;
         int i = 0;
         m_blnLoaded = false;
         _Continue:
         foreach (DataRowView drv in dsTable.DefaultView)
         {
             i++;
             if (drv["CHON"].ToString() == "0")
             {
                 dsTable.Rows.Remove(drv.Row);
                 DmucKhoaphong.Delete(Utility.Int32Dbnull(drv[DmucKhoaphong.Columns.IdKhoaphong], -1));
                 if (i < Count)
                 {
                     goto _Continue;
                 }
                 else
                     break;
             }

             dsTable.AcceptChanges();
             m_blnLoaded = true;
             }
             }
             catch
             {
             }
        }
        /// <summary>
        /// HÀM THỰC HIỆN TÌM KIẾM THÔNG TIN  KHOA PHÒNG
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            Search();
            ModifyCommand();
        }
        /// <summary>
        /// HÀM THỰC HIỆN LOAD THÔNG TIN KHI LOAD FORM LÊN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_dmuc_phongban_Load(object sender, EventArgs e)
        {
            //BindParent_ID();
            Search();
            ModifyCommand();
            
        }
        /// <summary>
        /// HÀM THỰC HIỆN ĐÓNG FORM KHI NHẤN VÀO THOÁT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
      
        /// <summary>
        /// HÀM THỰC HIỆN DÙNG PHÍM TẮT CỦA FORM KHI THỰC HIỆN DÙNG THAO TÁC BÀN PHÍM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_dmuc_phongban_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1) cmdPrint.PerformClick();
            if (e.KeyCode == Keys.Escape) toolStripButton1.PerformClick();
            if (e.Control && e.KeyCode == Keys.D) cmdDelete.PerformClick();
            if (e.Control && e.KeyCode == Keys.E) cmdEdit.PerformClick();
            if (e.Control && e.KeyCode == Keys.N) cmdNew.PerformClick();
            if (e.KeyCode == Keys.F4) cmdPrint.PerformClick();
            if (e.KeyCode == Keys.F5) Search();

        }
        #endregion
      
        #region "HAM DUNG CHUNG"
        /// <summary>
        /// HÀM THỰC HIỆN GÁN SỰ KIỆN CỦA CÁC NÚT THỰC HIỆN TRÊN FORM
        /// THỰC HIỆN GỌI CÁC SỰ KIỆN HIỆN CÓ TRÊN FORM
        /// </summary>
        void IntitalEventControl()
        {
            this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
          //  this.cmdPrint.Click += new System.EventHandler(this.cmdSearchGrid_Click);
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
           // this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
           // this.txtServiceDetailName.TextChanged += new System.EventHandler(this.txtServiceDetailName_TextChanged);
            grdPhongBan.SelectionChanged +=new EventHandler(grdPhongBan_SelectionChanged);
            grdPhongBan.ApplyingFilter +=new System.ComponentModel.CancelEventHandler(grdPhongBan_ApplyingFilter);
        }
        /// <summary>
        /// THUC HIEN SEARCH THONG TIN KHOA PHONG
        /// </summary>
        void Search()
        {
            try
            {
                dsTable =
                new Select().From(VDmucKhoaphong.Schema).ExecuteDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdPhongBan, dsTable, true, true, "1=1", DmucKhoaphong.Columns.SttHthi);
                ModifyCommand();

            }
            catch (Exception)
            {
                
               ModifyCommand();
            }
         

        }
      
        /// <summary>
        /// THUC HIEN XEM TRANG THAI CUA CAC NÚT
        /// </summary>
        void ModifyCommand()
        {
            try
            {
                cmdEdit.Enabled = grdPhongBan.RowCount > 0&&grdPhongBan.CurrentRow.RowType==RowType.Record;
                //cmdDeleteALL.Enabled = grdPhongBan.RowCount > 0;
                cmdDelete.Enabled = grdPhongBan.RowCount > 0 && grdPhongBan.CurrentRow.RowType == RowType.Record;
                cmdPrint.Enabled = grdPhongBan.RowCount > 0 && grdPhongBan.CurrentRow.RowType == RowType.Record;
            }
            catch (Exception)
            {
                
               
            }
          
        }
        #endregion

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
           // printPreviewDialog1.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void grdPhongBan_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdPhongBan.CurrentRow != null)
                {
                    v_Department_id = Utility.Int32Dbnull(grdPhongBan.CurrentRow.Cells[ DmucKhoaphong.Columns.IdKhoaphong].Value, -1);
                }
                ModifyCommand();
            }catch(Exception ex)
            {
                ModifyCommand();
            }
           
        }

     
        private void grdPhongBan_ApplyingFilter(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ModifyCommand();
        }

        /// <summary>
        /// sửa thông tin trực tiếp trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPhongBan_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            int Parent_ID = 0;
            int id = Utility.Int32Dbnull(grdPhongBan.CurrentRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value);
            int record = new Update(DmucKhoaphong.Schema)
                   .Set(DmucKhoaphong.Columns.TenKhoaphong).EqualTo(grdPhongBan.CurrentRow.Cells[DmucKhoaphong.Columns.TenKhoaphong].Text)
                   .Set(DmucKhoaphong.Columns.SttHthi).EqualTo(grdPhongBan.CurrentRow.Cells[DmucKhoaphong.Columns.SttHthi].Text)
                   .Where(DmucKhoaphong.Columns.IdKhoaphong).IsEqualTo(Utility.Int32Dbnull(grdPhongBan.CurrentRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Text, -1))
                   .Execute();
            if (record > 0)
            {
                grdPhongBan.UpdateData();
                grdPhongBan.Refresh();
                dsDepartment.AcceptChanges();
            }
            else
            {
                Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin ");
            }
            ModifyCommand();
        }

        private void grdPhongBan_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            var objValue = new object();
            if (e.Column.Key == DmucKhoaphong.Columns.SttHthi)
            {
                objValue = e.Value;
                if (!SubSonic.Sugar.Numbers.IsInteger(Utility.sDbnull(objValue)))
                {
                    e.Cancel = true;
                    Utility.ShowMsg("Chỉ được phép nhập số");
                }
                else if (Utility.Int32Dbnull(objValue) < 0)
                {
                    e.Cancel = true;
                    Utility.ShowMsg("Giá trị nhập vào phải lớn hơn 0");
                }
            }
            else if (e.Column.Key == DmucKhoaphong.Columns.MaKhoaphong)
            {
                objValue = e.Value;
                SqlQuery q = new Select().From(DmucKhoaphong.Schema)
                .Where(DmucKhoaphong.Columns.MaKhoaphong).IsEqualTo(Utility.sDbnull(objValue))
                .And(DmucKhoaphong.Columns.IdKhoaphong )
                .IsNotEqualTo(Utility.Int32Dbnull(grdPhongBan.CurrentRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value, -1));
                if (string.IsNullOrEmpty(objValue.ToString().Trim()))
                {
                    e.Cancel = true;
                    Utility.ShowMsg("Mã khoa(phòng) không được để trống", "Thông báo thiếu thông tin", MessageBoxIcon.Warning);
                }
                if (q.GetRecordCount() > 0)
                {
                    e.Cancel = true;
                    Utility.ShowMsg("Đã tồn tại mã khoa(phòng)", "Thông báo tồn tại", MessageBoxIcon.Warning);
                }
            }
            else if (e.Column.Key == DmucKhoaphong.Columns.TenKhoaphong)
            {
                objValue = e.Value;
                if (string.IsNullOrEmpty(objValue.ToString().Trim()))
                {
                    e.Cancel = true;
                    Utility.ShowMsg("Tên khoa(phòng) không được để trống", "Thông báo thiếu thông tin", MessageBoxIcon.Warning);
                }
            }
        }

       
    }
}
