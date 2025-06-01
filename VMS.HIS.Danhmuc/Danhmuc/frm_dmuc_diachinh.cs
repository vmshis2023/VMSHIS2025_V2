using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using Janus.Windows.GridEX;

namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_dmuc_diachinh : Form
    {

        bool m_blnLoaded = false;
        public frm_dmuc_diachinh()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KeyPreview = true;
            grdDiachinh.SelectionChanged+=new EventHandler(grdSurveys_SelectionChanged);
            grdDiachinh.FilterApplied+=new EventHandler(grdSurveys_FilterApplied);
            grdDiachinh.CellUpdated += grdDiachinh_CellUpdated;
            grdDiachinh.EditingCell += grdDiachinh_EditingCell;
            grdDiachinh.UpdatingCell += grdDiachinh_UpdatingCell;
            cboSurvery.SelectedIndexChanged += new EventHandler(cboSurvery_SelectedIndexChanged);
            cmdRefresh.Click += new EventHandler(cmdRefresh_Click);
        }

        void grdDiachinh_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                 if (e.Column.Key == "mota_them" )
            {
                int num = 0;
                if (Utility.sDbnull(e.Value, "").Length <= 0)
                {
                    Utility.SetMsg(lblMsg, "Gõ tắt không được phép xóa trắng", false);
                    e.Value = e.InitialValue;
                }
                else
                {
                     num = new Update(DmucDiachinh.Schema)
                        .Set(DmucDiachinh.Columns.MotaThem).EqualTo(e.Value.ToString())
                        .Where(DmucDiachinh.Columns.MaDiachinh).IsEqualTo(grdDiachinh.GetValue(DmucDiachinh.Columns.MaDiachinh)).Execute();
                }
                if (num > 0)
                    Utility.SetMsg(lblMsg, "Cập nhật gõ tắt thành công", false);
            }
            }
            catch (Exception ex)
            {
                
               Utility.CatchException(ex);
            }
        }

        void grdDiachinh_EditingCell(object sender, EditingCellEventArgs e)
        {
           
        }

        void grdDiachinh_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            
        }

        void cmdRefresh_Click(object sender, EventArgs e)
        {
            Utility.WaitNow(this);
            globalVariables.gv_dtDmucDiachinh = new Select().From(VDmucDiachinh.Schema).ExecuteDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdDiachinh, globalVariables.gv_dtDmucDiachinh, true, true, "1=1", DmucDiachinh.Columns.SttHthi);
            cboLoaiDiachinh_SelectedIndexChanged(cboLoaiDiachinh, e);
            Utility.DefaultNow(this);
        }

        void cboSurvery_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blnLoaded) return;
                Utility.WaitNow(this);
                string rowfilter = "1=1";
                globalVariables.gv_dtDmucDiachinh.DefaultView.RowFilter = rowfilter;
                if (cboLoaiDiachinh.SelectedIndex - 1<= 1)
                {
                    if (cboSurvery.SelectedIndex > 0)
                        rowfilter = DmucDiachinh.Columns.MaCha + "='" + cboSurvery.SelectedValue.ToString() + "'";
                    else
                        rowfilter = DmucDiachinh.Columns.LoaiDiachinh + "=" + (cboLoaiDiachinh.SelectedIndex - 1).ToString();
                }
                else
                    rowfilter = DmucDiachinh.Columns.LoaiDiachinh + "=" + (cboLoaiDiachinh.SelectedIndex - 1).ToString();
                globalVariables.gv_dtDmucDiachinh.DefaultView.RowFilter = rowfilter;
            }
            catch
            {
            }
            finally
            {
                if (grdDiachinh.GetDataRows().Length > 0) grdDiachinh.MoveFirst();
                ModifyCommand();
                Utility.DefaultNow(this);
            }
        }

        private void frm_dmuc_diachinh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) toolStripButton1.PerformClick();
            
            if (e.Control && e.KeyCode == Keys.N) cmdNew.PerformClick();
            if (e.Control && e.KeyCode == Keys.E) cmdEdit.PerformClick();
            if (e.Control && e.KeyCode == Keys.D) cmdDelete.PerformClick();
            if (e.KeyCode == Keys.F4) cmdPrint.PerformClick();
        }

        private void frm_dmuc_diachinh_Load(object sender, EventArgs e)
        {
            cboLoaiDiachinh.SelectedIndex = 0;
            Utility.SetDataSourceForDataGridEx(grdDiachinh, globalVariables.gv_dtDmucDiachinh, true, true, "1=1", DmucDiachinh.Columns.SttHthi);
           
            ModifyCommand();
            m_blnLoaded = true;
            cboLoaiDiachinh_SelectedIndexChanged(cboLoaiDiachinh, e);
        }
       
        
        /// <summary>
        /// trạng thái của nút
        /// </summary>
        private void ModifyCommand()
        {
            bool _enabled = Utility.isValidGrid(grdDiachinh);
            cmdEdit.Enabled = _enabled;
            cmdDelete.Enabled = _enabled;
            cmdPrint.Enabled = _enabled;
        }

        private string v_Survey_code ="";
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!isValidData()) return;
                //if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin đang chọn hay không", "Thông báo", true))
                //{
                //    int record = new Delete().From(DmucDiachinh.Schema)
                //        .Where(DmucDiachinh.Columns.MaDiachinh).IsEqualTo(v_Survey_code).Execute();

                //    if (record > 0)
                //    {
                //        grdSurveys.CurrentRow.Delete();
                //        grdSurveys.UpdateData();
                //        grdSurveys.Refresh();
                //        DataRow[] arrDr = globalVariables.gv_dtDmucDiachinh.Select(DmucDiachinh.Columns.MaDiachinh + "='" + v_Survey_code + "'");
                //        if (arrDr.Length > 0) globalVariables.gv_dtDmucDiachinh.Rows.Remove(arrDr[0]);
                //        Utility.ShowMsg("Bạn xóa thông tin địa chính đang chọn thành công", "Thông báo");
                //    }
                //    else
                //    {
                //        Utility.ShowMsg("Có lỗi trong khi xóa thông tin địa chính", "Thông báo");
                //    }
                //}
                //ModifyCommand();
                DataTable dtCheck =new DataTable();
                if (grdDiachinh.GetCheckedRows().Count() <= 0)
                {
                    if (grdDiachinh.CurrentRow != null)
                    {
                        if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin địa chính đang chọn hay không", "Thông báo", true))
                        {
                            string ma_diachinh = Utility.sDbnull(grdDiachinh.CurrentRow.Cells[DmucDiachinh.Columns.MaDiachinh].Value);
                            dtCheck = SPs.DmucDiachinhCheckDelete(ma_diachinh).GetDataSet().Tables[0];
                            if (dtCheck.Rows.Count > 0)
                            {
                                Utility.ShowMsg("Mã địa chính đã được sử dụng trong bảng Lượt khám nên không thể xóa. Vui lòng kiểm tra lại", "Thông báo");
                                return;
                            }

                            int record = new Delete().From(DmucDiachinh.Schema)
                                .Where(DmucDiachinh.Columns.MaDiachinh).IsEqualTo(ma_diachinh).Execute();
                            if (record > 0)
                            {
                                DataRow[] arrDr = globalVariables.gv_dtDmucDiachinh.Select(DmucDiachinh.Columns.MaDiachinh + "=" + ma_diachinh);
                                if (arrDr.GetLength(0) > 0)
                                {
                                    arrDr[0].Delete();
                                }
                                globalVariables.gv_dtDmucDiachinh.AcceptChanges();
                            }
                            else
                            {
                                Utility.ShowMsg("Lỗi trong quá trình xóa thông tin địa chính");
                            }
                        }
                    }
                }
                else
                {
                    List<string> lstDiachinh = new List<string>();
                    foreach (GridEXRow row in grdDiachinh.GetCheckedRows())
                    {
                        string ma_diachinh = Utility.sDbnull(row.Cells[DmucDiachinh.Columns.MaDiachinh].Value);
                        dtCheck = SPs.DmucDiachinhCheckDelete(ma_diachinh).GetDataSet().Tables[0];
                        if (dtCheck.Rows.Count > 0)
                        {
                            lstDiachinh.Add(string.Format("Mã: {0} - Tên: {1}", row.Cells[DmucDiachinh.Columns.MaDiachinh].Value,row.Cells[DmucDiachinh.Columns.TenDiachinh].Value));
                        }
                        else
                        {
                            DataRow dr = (from datarow in globalVariables.gv_dtDmucDiachinh.AsEnumerable()
                                          where datarow.Field<string>(DmucDiachinh.Columns.MaDiachinh) == ma_diachinh
                                          select datarow).First();
                            DmucDiachinh.Delete(ma_diachinh);
                            if (dr != null) globalVariables.gv_dtDmucDiachinh.Rows.Remove(dr);
                        }
                    }
                    if (lstDiachinh.Count > 0)
                    {
                        Utility.ShowMsg(string.Format("Một số đơn vị địa chính bạn chọn đã được đăng ký cho người bệnh nên bạn không thể xóa. Vui lòng kiểm tra lại\n{0}", string.Join(",\n", lstDiachinh.ToArray<string>())));
                    }

                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
           
        }
        private bool  isValidData()
        {
            v_Survey_code = Utility.sDbnull(grdDiachinh.GetValue(DmucDiachinh.Columns.MaDiachinh),"");
            SqlQuery sqlQuery = new Select().From(DmucNoiKCBBD.Schema)
                .Where(DmucNoiKCBBD.Columns.MaDiachinh).IsEqualTo(v_Survey_code);
            if(sqlQuery.GetRecordCount()>0)
            {
                Utility.ShowMsg("Mã địa chính đã được sử dụng trong bảng Nơi KCBBD. Bạn không thể xóa","Thông báo");
                grdDiachinh.Focus();
                return false;
            }
            v_Survey_code = Utility.sDbnull(grdDiachinh.GetValue("ma_diachinh"), "");
            SqlQuery sqlQuery1 = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.NoicapBhyt).IsEqualTo(v_Survey_code);
            if (sqlQuery1.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Mã địa chính đã được sử dụng trong bảng Lượt khám. Bạn không thể xóa","Thông báo");
                grdDiachinh.Focus();
                return false;
            }
            return true;
        }
        private bool isValidData(string ma_diachinh)
        {
            DataTable dtCheck = SPs.DmucDiachinhCheckDelete(ma_diachinh).GetDataSet().Tables[0];
            if (dtCheck.Rows.Count > 0)
            {
                Utility.ShowMsg("Mã địa chính đã được sử dụng trong bảng Lượt khám nên không thể xóa. Vui lòng kiểm tra lại", "Thông báo");
                grdDiachinh.Focus();
                return false;
            }
            return true;
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            frm_themmoi_diachinh frm = new frm_themmoi_diachinh();
            frm.grdList = grdDiachinh;
            frm.em_Action = action.Insert;
            frm.ShowDialog();
            ModifyCommand();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            frm_themmoi_diachinh frm = new frm_themmoi_diachinh();
           
            frm.em_Action = action.Update;
            frm.grdList = grdDiachinh;
            frm.txtSurveyCode.Text = v_Survey_code.ToString();
            frm.txtSurveyCode.Enabled = false;
            frm.ShowDialog();
            ModifyCommand();
        }

        private void grdSurveys_SelectionChanged(object sender, EventArgs e)
        {
            if(grdDiachinh.CurrentRow!=null)
            {
                v_Survey_code = Utility.sDbnull(grdDiachinh.CurrentRow.Cells[DmucDiachinh.Columns.MaDiachinh ].Value, "");
            }
            ModifyCommand();
        }

       
        /// <summary>
        /// trạng thái của nút khi lọc dữ liệu trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdSurveys_FilterApplied(object sender, EventArgs e)
        {
            ModifyCommand();
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void grdSurveys_Enter(object sender, EventArgs e)
        {
           //Janus.Windows.GridEX.GridEXCell gridExCell=
        }

        private void grdSurveys_Leave(object sender, EventArgs e)
        {

        }

        private void cboLoaiDiachinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blnLoaded) return;
                lbldiachinh.Text = "Chọn " + cboLoaiDiachinh.Text + ":";
                if (cboLoaiDiachinh.SelectedIndex <= 0)
                {
                    
                    DataBinding.BindDataCombox(cboSurvery, null, DmucDiachinh.Columns.MaDiachinh, DmucDiachinh.Columns.TenDiachinh);
                    globalVariables.gv_dtDmucDiachinh.DefaultView.RowFilter = "1=1";
                    lbldiachinh.Enabled = cboSurvery.Enabled = false;
                    if (grdDiachinh.GetDataRows().Length > 0)
                        grdDiachinh.MoveFirst();
                    ModifyCommand();
                    return;
                }
                DataRow[] arrDR = globalVariables.gv_dtDmucDiachinh.Select(DmucDiachinh.Columns.LoaiDiachinh+ "=" + (cboLoaiDiachinh.SelectedIndex-1).ToString());
                if (cboLoaiDiachinh.SelectedIndex - 1 <= 1)
                {
                    lbldiachinh.Enabled= cboSurvery.Enabled = true;
                    if (arrDR.Length > 0)
                        DataBinding.BindDataCombox(cboSurvery, arrDR.CopyToDataTable(), DmucDiachinh.Columns.MaDiachinh, DmucDiachinh.Columns.TenDiachinh, "Chọn", true);
                    else
                        DataBinding.BindDataCombox(cboSurvery, null, DmucDiachinh.Columns.MaDiachinh, DmucDiachinh.Columns.TenDiachinh, "Chọn", true);
                    if (arrDR.Length > 0)
                        cboSurvery_SelectedIndexChanged(cboSurvery, e);
                }
                else
                {
                    DataBinding.BindDataCombox(cboSurvery, null, DmucDiachinh.Columns.MaDiachinh, DmucDiachinh.Columns.TenDiachinh);
                    lbldiachinh.Enabled = cboSurvery.Enabled = false;
                    if (arrDR.Length > 0)
                        cboSurvery_SelectedIndexChanged(cboSurvery, e);
                }
               
            }
            catch
            {
            }
        }
    }
}
