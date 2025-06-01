using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
using Janus.Windows.GridEX;
using System.Threading;

namespace VNS.Libs
{
    public partial class frm_GridConfig : Form
    {
        public delegate void OnAccept(DataTable dtColData,GridEX grd);
        public event OnAccept _OnAccept;
        GridEX grd;
        DataTable dtColumnData = new DataTable();
        bool AllowEvents = false;
        public frm_GridConfig(GridEX grd)
        {
            InitializeComponent();
            this.grd = grd;
            this.Load += frm_GridConfig_Load;
          
            grdList.CellUpdated += GrdList_CellUpdated;
            grdList.CurrentCellChanged += GrdList_CurrentCellChanged;
            grdList.EditingCell += GrdList_EditingCell;
           
        }

        private void GrdList_EditingCell(object sender, EditingCellEventArgs e)
        {
            //string key = e.Column.Key;
            //try
            //{
            //    if (key == "col_index")
            //    {
            //        List<int> lstIdx = dtColumnData.AsEnumerable().Select(al => al.Field<int>("col_index")).Distinct().ToList();
            //        int max = lstIdx.Max();
            //        int CurrValue = Utility.Int32Dbnull(grdList.GetValue(key));
            //        if (CurrValue > max + 1) CurrValue = max + 1;
            //        //Sắp lại stt các phần tử phía sau cho đúng
            //        foreach(DataRow dr in dtColumnData.Rows)
            //            if(Utility.Int32Dbnull(dr["col_index"])>= CurrValue && Utility.Int64Dbnull(dr["id"])!= Utility.Int64Dbnull(grdList.GetValue("id")))
            //            {
            //                dr["col_index"]= Utility.Int32Dbnull(dr["col_index"]) + 1;
            //            }    
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Utility.CatchException(ex);
            //}
        }

        private void GrdList_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                byte source = Utility.ByteDbnull(grdList.GetValue("source"));
                grdList.RootTable.Columns["col_key"].EditType = source == 1 ? EditType.NoEdit : EditType.TextBox;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GrdList_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            string key = e.Column.Key;
            try
            {
                if (key == "col_index")
                {
                    List<int> lstIdx = dtColumnData.AsEnumerable().Select(al => al.Field<int>("col_index")).Distinct().ToList();
                    int max = lstIdx.Max();
                    int CurrValue = Utility.Int32Dbnull(grdList.GetValue(key));
                    if (CurrValue > max + 1) CurrValue = max + 1;
                    //Sắp lại stt các phần tử phía sau cho đúng
                    foreach (DataRow dr in dtColumnData.Rows)
                        if (Utility.Int32Dbnull(dr["col_index"]) >= CurrValue && Utility.Int64Dbnull(dr["id"]) != Utility.Int64Dbnull(grdList.GetValue("id")))
                        {
                            dr["col_index"] = Utility.Int32Dbnull(dr["col_index"]) + 1;
                        }
                }
                else
                {
                    long id = Utility.Int64Dbnull(grdList.GetValue("id"));
                    if (id > 0)
                    {
                        int num = new Update(SysGridUserConfig.Schema).Set(key).EqualTo(grdList.GetValue(key)).Where(SysGridUserConfig.Columns.Id).IsEqualTo(id).Execute();
                    }
                }
                   
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        void LoadUserConfig()
        {
            try
            {
                dtColumnData = new Select().From(SysGridUserConfig.Schema)
                                .Where(SysGridUserConfig.Columns.Username).IsEqualTo(globalVariables.UserName)
                                .And(SysGridUserConfig.Columns.GridName).IsEqualTo(grd.Name)
                                 .And(SysGridUserConfig.Columns.FormName).IsEqualTo(grd.FindForm().Name)
                                .ExecuteDataSet().Tables[0];
                if (dtColumnData != null && !dtColumnData.Columns.Contains("guid"))
                    dtColumnData.Columns.Add(new DataColumn("guid", typeof(string)));

                if (dtColumnData.Rows.Count == 0)//Khởi tạo
                {
                    foreach (GridEXColumn col in grd.RootTable.Columns)
                    {
                        DataRow dr = dtColumnData.NewRow();
                        dr["grid_name"] = grd.Name;
                        dr["col_header"] = col.Caption;
                        dr["col_header_org"] = col.Caption;
                        dr["col_key"] = col.Key;
                        dr["isVisible"] = col.Visible;
                        dr["isVisible_org"] = col.Visible;
                        dr["width"] = col.Width;
                        dr["width_org"] = col.Width;
                        dr["col_index"] = col.Index;
                        dr["col_index_org"] = col.Index;
                        dr["source"] = 1;
                        dr["guid"] = Guid.NewGuid().ToString();
                        dr["username"] = globalVariables.UserName;
                        dtColumnData.Rows.Add(dr);
                        //Thread.Sleep(10);
                    }
                }
                chkApplyAll.Checked = dtColumnData.Select("apply_all=1").Length > 0;
                Utility.SetDataSourceForDataGridEx(grdList, dtColumnData, true, true, "1=1", "col_index");
                grdList.AutoSizeColumns();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }    

        void frm_GridConfig_Load(object sender, EventArgs e)
        {
            chkApplyAll.Visible = globalVariables.IsAdmin || globalVariables.isSuperAdmin;
            LoadUserConfig();
            AllowEvents = true;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            try
            {
                new Delete().From(SysGridUserConfig.Schema)
                    .Where(SysGridUserConfig.Columns.Username).IsEqualTo(globalVariables.UserName)
                    .And(SysGridUserConfig.Columns.GridName).IsEqualTo(grd.Name)
                     .And(SysGridUserConfig.Columns.FormName).IsEqualTo(grd.FindForm().Name)
                    .Execute();
                SysGridUserConfig newitem = new SysGridUserConfig();
                foreach (DataRow dr in dtColumnData.Rows)
                {
                    newitem = new SysGridUserConfig();
                    newitem.IsNew = true;
                    newitem.Username = Utility.sDbnull(dr["username"]);
                    newitem.GridName = Utility.sDbnull(dr["grid_name"]);
                    newitem.ColHeader = Utility.sDbnull(dr["col_header"]);
                    newitem.ColHeaderOrg = Utility.sDbnull(dr["col_header_org"]);
                    newitem.ColKey = Utility.sDbnull(dr["col_key"]);
                    newitem.IsVisible = Convert.ToBoolean(dr["isVisible"]);
                    newitem.IsVisibleOrg = Convert.ToBoolean(dr["isVisible_org"]);
                    newitem.Width = Utility.Int32Dbnull(dr["width"], 0);
                    newitem.WidthOrg = Utility.Int32Dbnull(dr["width_org"], 0);
                    newitem.ColIndex = Utility.Int32Dbnull(dr["col_index"], 0);
                    newitem.ColIndexOrg = Utility.Int32Dbnull(dr["col_index_org"], 0);
                    newitem.Source = Utility.ByteDbnull(dr["source"], 0);
                    newitem.FormName = grd.FindForm().Name;
                    newitem.ApplyAll = Utility.ByteDbnull( chkApplyAll.Visible ? Utility.Bool2byte(chkApplyAll.Checked) : 0);
                    newitem.Save();
                    dr["id"] = newitem.Id;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                Utility.ShowHideColumns(grd);
               // if (_OnAccept != null) _OnAccept(dtColumnData, grd);
            }
            catch (Exception ex)
            {


            }

        }

        private void cmdRestore_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn khôi phục về cấu hình mặc định từ nhà sản xuất phần mềm","Xác nhận",true)) return;
                new Delete().From(SysGridUserConfig.Schema)
                  .Where(SysGridUserConfig.Columns.Username).IsEqualTo(globalVariables.UserName)
                  .And(SysGridUserConfig.Columns.GridName).IsEqualTo(grd.Name)
                   .And(SysGridUserConfig.Columns.FormName).IsEqualTo(grd.FindForm().Name)
                  .Execute();
                LoadUserConfig();
                //foreach (DataRow dr in dtColumnData.Rows)
                //{
                //    dr["isVisible"] = dr["isVisible_org"];
                //    dr["col_header"] = dr["col_header_org"];
                //    dr["width"] = dr["width_org"];
                //    dr["col_index"] = dr["col_index_org"];
                //}
                //dtColumnData.AcceptChanges();
                Utility.ShowMsg("Nhấn chấp nhận để lưu lại cấu hình mới khôi phục");
            }
            catch (Exception ex)
            {
            
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SysGridUserConfig _deleteObj = SysGridUserConfig.FetchByID(Utility.Int64Dbnull(grdList.GetValue("id")));
                if (_deleteObj != null)
                {
                    if (Utility.ByteDbnull( _deleteObj.Source,1) == 0)
                    {
                        int num = new Delete().From(SysGridUserConfig.Schema).Where(SysGridUserConfig.Columns.Id).IsEqualTo(_deleteObj.Id).Execute();
                        if (num > 0)
                        {
                            dtColumnData.AsEnumerable().Where(r => r.Field<Int64>("id") == _deleteObj.Id).ToList().ForEach(row => row.Delete());
                            dtColumnData.AcceptChanges();
                            Utility.ShowMsg(string.Format("Đã xóa cột {0} khỏi hệ thống", _deleteObj.ColHeader));
                        }
                    }
                    else
                    {
                        Utility.ShowMsg("Chỉ cho phép xóa các cột do người dùng tự tạo (Cột nguồn có giá trị =0).\nVui lòng kiểm tra lại");
                    }
                }
                else//Row mới tạo
                {
                    dtColumnData.AsEnumerable().Where(r => r.Field<string>("guid") == Utility.sDbnull(grdList.GetValue("guid"))).ToList().ForEach(row => row.Delete());
                    dtColumnData.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdCopy_Click(object sender, EventArgs e)
        {
            try
            {
                SysGridUserConfig _new = SysGridUserConfig.FetchByID(Utility.Int64Dbnull(grdList.GetValue("id")));
                if (_new != null)
                {
                    _new.IsNew = true;
                    _new.Source = 0;
                    _new.Save();
                    DataRow newDr = dtColumnData.NewRow();
                    Utility.FromObjectToDatarow(_new, ref newDr);
                    newDr["guid"] = Guid.NewGuid().ToString();
                    dtColumnData.Rows.Add(newDr);
                }
                else
                {
                    DataRow sourceRow = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                    DataRow newDr = dtColumnData.NewRow();
                    Utility.CopyData(sourceRow, ref newDr);
                    newDr["source"] = 0;
                    newDr["guid"]= Guid.NewGuid().ToString();
                    dtColumnData.Rows.Add(newDr);
                }    
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            
        }

        private void chkApplyAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowEvents) return;
                int num = new Update(SysGridUserConfig.Schema)
                                .Set(SysGridUserConfig.Columns.ApplyAll).EqualTo(Utility.ByteDbnull(chkApplyAll.Visible ? Utility.Bool2byte(chkApplyAll.Checked) : 0))
                                            .Where(SysGridUserConfig.Columns.Username).IsEqualTo(globalVariables.UserName)
                                            .And(SysGridUserConfig.Columns.GridName).IsEqualTo(grd.Name)
                                             .And(SysGridUserConfig.Columns.FormName).IsEqualTo(grd.FindForm().Name)
                                            .Execute();
                if (num > 0)
                    if (chkApplyAll.Checked)
                        Utility.ShowMsg("Cấu hình này đã được áp cho tất cả người dùng trong hệ thống");
                    else
                        Utility.ShowMsg("Cấu hình này đã được bỏ áp dụng cho tất cả người dùng trong hệ thống");
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }

        }
    }
}
