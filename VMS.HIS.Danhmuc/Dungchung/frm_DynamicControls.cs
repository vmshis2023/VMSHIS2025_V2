using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using SubSonic;
using Janus.Windows.GridEX;
using VNS.Libs;
using System.Transactions;
using VNS.Properties;
using VNS.HIS.UI.DANHMUC;
using VMS.HIS.Danhmuc.AutoComplete;
using VNS.UCs;
using VMS.HIS.Danhmuc.AutoComplete.DynamicReport_Ucs;

namespace VNS.HIS.UI.HinhAnh
{
    public partial class frm_DynamicControls : Form
    {
        public SysMultiReport objDynamicReport = null;
        
        public long ImageID = -1;
        public long Id_chidinhchitiet = -1;
        public string MafileDoc ="-1";
        bool hasDeleted = false;
        DataRow _currentDr = null;
        public int MultiReportId = -1;
        public bool hasChanged = false;
        public bool isAllowSelectionChanged = false;
        public frm_DynamicControls(int MultiReportId)
        {
            InitializeComponent();
            this.MultiReportId = MultiReportId;
            Config();
            InitComboBoxColumns();
            this.Load += frm_DynamicControls_Load;
            this.KeyDown += frm_DynamicControls_KeyDown;
            this.FormClosing += frm_DynamicControls_FormClosing;
            grdList.UpdatingCell += grdList_UpdatingCell;
            grdList.SelectionChanged += grdList_SelectionChanged;
            grdList.DeletingRecords += grdList_DeletingRecords;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            cmdRefresh.Click += cmdConfig_Click;
            cmdNew.Click += cmdNew_Click;
        }

        void cmdNew_Click(object sender, EventArgs e)
        {
            try
            {
                SysDynamicControl _newItem = new SysDynamicControl();
                _newItem.Ma = "";
                _newItem.Mota = "New Item";
                _newItem.Stt = 100;
                _newItem.MultiReportId = MultiReportId;
                _newItem.Rtxt = 0;
                _newItem.TopLabel = 0;
                _newItem.Multiline = 0;
                _newItem.X = 0;
                _newItem.Y = 0;
                _newItem.W = 200;
                _newItem.H = 25;
                _newItem.LblW = 100;
                _newItem.AllowEmpty = 0;
                _newItem.Bold = 0;
                _newItem.DataSource = "";
                _newItem.DisplayMember = "";
                _newItem.ValueMember = "";
                _newItem.ControlType = 0;
                _newItem.DefaultOption = "";
                //_newItem.r
                _newItem.Save();
                hasChanged = true;
                DataRow newDr = dtData.NewRow();
                Utility.FromObjectToDatarow(_newItem,ref newDr);
                dtData.Rows.Add(newDr);
                Utility.SetDataSourceForDataGridEx(grdList, dtData, true, true, "1=1", "stt");
                Utility.GotoNewRowJanus(grdList, "Id", _newItem.Id.ToString());
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void InitComboBoxColumns()
        {
            try
            {
                DataTable dtPTTT = new DataTable();
                dtPTTT.Columns.AddRange(new DataColumn[] { new DataColumn("Id", typeof(int)), new DataColumn("ten", typeof(string)) });
                DataRow _newRow = dtPTTT.NewRow();
                _newRow["Id"] = 0;
                _newRow["ten"] = "TextBox";
                dtPTTT.Rows.Add(_newRow);
                _newRow = dtPTTT.NewRow();
                _newRow["Id"] = 1;
                _newRow["ten"] = "Autocomplete";
                dtPTTT.Rows.Add(_newRow);
                _newRow = dtPTTT.NewRow();
                _newRow["Id"] = 2;
                _newRow["ten"] = "Combobox";
                dtPTTT.Rows.Add(_newRow);
                _newRow = dtPTTT.NewRow();
                _newRow["Id"] = 3;
                _newRow["ten"] = "DateTimePicker";
                dtPTTT.Rows.Add(_newRow);
                _newRow = dtPTTT.NewRow();
                _newRow["Id"] = 4;
                _newRow["ten"] = "DateTimePicker Checkbox";
                dtPTTT.Rows.Add(_newRow);
                _newRow = dtPTTT.NewRow();
                _newRow["Id"] = 5;
                _newRow["ten"] = "Auto complete Combobox";
                dtPTTT.Rows.Add(_newRow);
                GridEXColumn _colmaPttt = grdList.RootTable.Columns["Control_Type"];
                _colmaPttt.HasValueList = true;
                _colmaPttt.LimitToList = true;

                GridEXValueListItemCollection _colmaPttt_Collection = grdList.RootTable.Columns["Control_Type"].ValueList;
                foreach (DataRow item in dtPTTT.Rows)
                {
                    _colmaPttt_Collection.Add(item["Id"].ToString(), item["ten"].ToString());
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }


        }
        void grdList_SelectionChanged(object sender, EventArgs e)
        {
            if (Utility.isValidGrid(grdList) && Utility.Int32Dbnull(grdList.CurrentRow.Cells["ID"].Value, "-1") > 0)
                _currentDr = (((DataRowView)grdList.CurrentRow.DataRow).Row);
        }
        DataTable dtData = null;
        void BuildControls()
        {
            try
            {
                Padding m_objPadding = new System.Windows.Forms.Padding(3);
                flowPnlDynamic.Controls.Clear();
                dtData = new Select().From(SysDynamicControl.Schema).Where(SysDynamicControl.Columns.MultiReportId).IsEqualTo(MultiReportId).ExecuteDataSet().Tables[0];
                flowPnlDynamic.SuspendLayout();
                foreach (DataRow dr in dtData.Select("1=1", "stt"))
                {
                    byte controlType = Utility.ByteDbnull(dr["Control_Type"], 0);
                    ucCtrl _ucs = null;

                    switch (controlType)
                    {
                        case 0:
                            _ucs = new ucTextbox(dr);
                            break;
                        case 1:
                            _ucs = new ucAutoComplete(dr);
                            break;
                        case 2:
                            _ucs = new ucCombobox(dr);
                           
                            break;
                        case 3:
                            _ucs = new ucDatetime(dr);
                            break;
                        case 4:
                            _ucs = new ucDatetime_Checked(dr);
                            break;
                        case 5:
                            _ucs = new ucComboboxAutoComplete(dr);
                            break;
                    }
                    //ctrl.lblName.Font = PropertyLib._HinhAnhProperties.DynamicFontChu;
                    //ctrl.txt.Font = PropertyLib._HinhAnhProperties.DynamicFontChu;
                    _ucs.Width = Utility.Int32Dbnull(dr[SysDynamicControl.Columns.W], 0);
                    _ucs.Height = Utility.Int32Dbnull(dr[SysDynamicControl.Columns.H], 0);
                    _ucs.SetTabIndex(10 + Utility.Int32Dbnull(dr[SysDynamicControl.Columns.Stt], 0));
                    flowPnlDynamic.Controls.Add(_ucs);
                    _ucs.Init();
                   
                    _ucs.Size = new Size(Utility.Int32Dbnull(dr[SysDynamicControl.Columns.W], 0), Utility.Int32Dbnull(dr[SysDynamicControl.Columns.H], 0));
                    _ucs.Margin = m_objPadding;
                }
                
            }
            catch (Exception ex)
            {
            }
            finally
            {
                flowPnlDynamic.ResumeLayout();
            }
        }
        void ucdtp2__OnchangeOfGroup(bool isChecked, string GroupID)
        {
            try
            {
                foreach (ucCtrl ctrl in flowPnlDynamic.Controls)
                {
                    if (ctrl.Group_Id == GroupID)
                        ctrl.SetEnable(isChecked);
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void cmdConfig_Click(object sender, EventArgs e)
        {
            //frm_Properties _Properties = new frm_Properties(PropertyLib._DynamicInputProperties);
            //_Properties.ShowDialog();
            Config();
        }
        void Config()
        {
            BuildControls();
        }
        void frm_DynamicControls_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hasDeleted)
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        void grdList_DeletingRecords(object sender, CancelEventArgs e)
        {
            try
            {
                int _Id=Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdList,SysDynamicControl.Columns.Id), -1);
                if (_Id > 0)
                {
                     SysDynamicControl.Delete(_Id);
                     hasDeleted = true;
                     hasChanged = true;
                }
            }
            catch (Exception)
            {
                
                
            }
        }

        void frm_DynamicControls_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }
             if (e.Control && e.KeyCode == Keys.S)
            {
                cmdSave.PerformClick();
                return;
            }
        }
        void grdList_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                string colName=e.Column.Key;
                int _id = Utility.Int32Dbnull(grdList.GetValue("id"));
              int num=  new Update(SysDynamicControl.Schema)
                    .Set(colName).EqualTo(e.Value)
                    .Where(SysDynamicControl.Columns.Id).IsEqualTo(_id)
                    .Execute();
                if (e.Column.Key == "mota")
                    Refresh(_id, Utility.sDbnull(e.Value));
                hasChanged = true;
                //var q = from p in grdList.GetDataRows().AsEnumerable()
                //        where p != grdList.CurrentRow
                //        && Utility.sDbnull(p.Cells[SysDynamicControl.Columns.Ma], "") == e.Value
                //        select p;
                //if (q.Count() > 0)
                //{
                //    Utility.ShowMsg("Mã này đã tồn tại, bạn cần nhập mã khác!");
                //    e.Cancel = true;
                //}
            }
            catch (Exception)
            {
            }
        }
        void Refresh(int id, string mota)
        {
            foreach (ucCtrl _uc in flowPnlDynamic.Controls)
                if (Utility.Int32Dbnull(_uc.dr["id"], -1) == id)
                    _uc.setTitle(mota);
        }
        void frm_DynamicControls_Load(object sender, EventArgs e)
        {
            if (PropertyLib._DynamicInputProperties == null)
                PropertyLib._DynamicInputProperties = PropertyLib.GetDynamicInputProperties();
            LoadData();
            Utility.focusCell(grdList, SysDynamicControl.Columns.Ma);
            isAllowSelectionChanged = true;
        }
        void LoadData()
        {
            try
            {
                //Load các report có dạng Dynamic
                DataTable dtMultireport = new Select().From(SysMultiReport.Schema).Where(SysMultiReport.Columns.IsDynamic).IsEqualTo(1).ExecuteDataSet().Tables[0];
                DataBinding.BindDataCombobox(cboMultiReport, dtMultireport, "MultiReport_ID", "MultiReport_Name","Chọn nguồn cần sao chép", "-1", true);
                cboMultiReport.SelectedIndex = 0;
                dtData = new Select().From(SysDynamicControl.Schema).Where(SysDynamicControl.Columns.MultiReportId).IsEqualTo(MultiReportId).ExecuteDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdList, dtData, true, true, "1=1", "stt");
                BuildControls();
            }
            catch (Exception ex)
            {
            }
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<SysDynamicControl> lstFields = new List<SysDynamicControl>();
                foreach (GridEXRow _row in grdList.GetDataRows())
                {
                    SysDynamicControl obj = null;

                    if (Utility.Int32Dbnull(_row.Cells[SysDynamicControl.Columns.Id].Value, -1) > 0)
                    {
                        obj = SysDynamicControl.FetchByID(Utility.Int32Dbnull(_row.Cells[SysDynamicControl.Columns.Id].Value, -1));
                        obj.IsNew = false;
                        obj.MarkOld();
                    }
                    else
                    {
                        obj = new SysDynamicControl();
                        obj.IsNew = true;
                    }

                    obj.MultiReportId = MultiReportId;
                    obj.Ma = Utility.sDbnull(_row.Cells[SysDynamicControl.Columns.Ma].Value, "-1");
                    obj.Mota = Utility.sDbnull(_row.Cells[SysDynamicControl.Columns.Mota].Value, "-1");
                    obj.Stt = Utility.Int16Dbnull(_row.Cells[SysDynamicControl.Columns.Stt].Value, 0);
                    obj.Multiline = Utility.ByteDbnull(_row.Cells[SysDynamicControl.Columns.Multiline].Value, 0);
                    obj.TopLabel = Utility.ByteDbnull(_row.Cells[SysDynamicControl.Columns.TopLabel].Value, 0);
                    //obj.Multiline = Utility.ByteDbnull(_row.Cells[SysDynamicControl.Columns.Multiline].Value, 0);
                    obj.AllowEmpty = Utility.ByteDbnull(_row.Cells[SysDynamicControl.Columns.AllowEmpty].Value, 0);
                    obj.W = Utility.Int16Dbnull(_row.Cells[SysDynamicControl.Columns.W].Value, 0);
                    obj.H = Utility.Int16Dbnull(_row.Cells[SysDynamicControl.Columns.H].Value, 0);
                    obj.DisplayMember = Utility.sDbnull(_row.Cells[SysDynamicControl.Columns.DisplayMember].Value, "");
                    obj.ValueMember = Utility.sDbnull(_row.Cells[SysDynamicControl.Columns.ValueMember].Value, "");
                    obj.DataSource = Utility.sDbnull(_row.Cells[SysDynamicControl.Columns.DataSource].Value, "");

                    obj.ControlType = Utility.ByteDbnull(_row.Cells[SysDynamicControl.Columns.ControlType].Value, 0);
                    obj.LblW = Utility.Int16Dbnull(_row.Cells[SysDynamicControl.Columns.LblW].Value, 0);
                    lstFields.Add(obj);
                }
                ActionResult _actionResult = UpdateSysDynamicControls(lstFields);
                if (_actionResult == ActionResult.Success)
                {
                   // this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    LoadData();
                    BuildControls();
                    hasChanged = true;
                    Utility.focusCell(grdList, SysDynamicControl.Columns.Ma);
                }

            }
            catch (Exception ex)
            {
            }
        }
        public ActionResult UpdateSysDynamicControls(List<SysDynamicControl> lstFields)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sp = new SharedDbConnectionScope())
                    {
                        foreach (SysDynamicControl _object in lstFields)
                        {
                            if (_object.Id > 0)
                            {
                                _object.MarkOld();
                                _object.IsNew = false;
                                _object.Save();
                            }
                            else//Insert
                            {
                                _object.IsNew = true;

                                _object.Save();
                            }
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
                return ActionResult.Error;
            }

        }
       
       

        private void grdList_FormattingRow(object sender, RowLoadEventArgs e)
        {

        }

        private void cmdSelectList_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdList)) return;
            if (_currentDr != null)
            {
                DataRow dr = dtData.NewRow();
                Utility.CopyData(_currentDr, ref dr);
                dr["id"] = -1;
                dtData.Rows.Add(dr);
                dtData.AcceptChanges();
                Utility.SetDataSourceForDataGridEx(grdList, dtData, true, true, "1=1", "stt");
            }
            
        }

        private void cmdSave_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdSameSize_Click(object sender, EventArgs e)
        {
            foreach (GridEXRow dr in grdList.GetRows())
            {
                dr.BeginEdit();
                if (chkUpdateOnlyZero.Checked)
                {
                    if (Utility.Int32Dbnull(dr.Cells["W"].Value, 0) == 0)
                        dr.Cells["W"].Value = _currentDr["W"];
                    if (Utility.Int32Dbnull(dr.Cells["H"].Value, 0) == 0)
                        dr.Cells["H"].Value = _currentDr["H"];
                    if (Utility.Int32Dbnull(dr.Cells["lblW"].Value, 0) == 0)
                        dr.Cells["lblW"].Value = _currentDr["lblW"];
                }
                else
                {
                    dr.Cells["W"].Value = _currentDr["W"];
                    dr.Cells["H"].Value = _currentDr["H"];
                    dr.Cells["lblW"].Value = _currentDr["lblW"];
                }
                dr.EndEdit();
            }
            grdList.Refetch();
            //dtData.AcceptChanges();
        }

        private void cmdConfig_Click_1(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._DynamicInputProperties);
            _Properties.ShowDialog();
        }

        private void cboMultiReport_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            try
            {
                int sourceId = Utility.Int32Dbnull(cboMultiReport.SelectedValue, -1);
                if (sourceId > 0)
                {
                    string sql = string.Format("insert into sys_dynamic_controls(Ma,mota,stt,MultiReport_ID,Rtxt,topLabel,multiline,X,Y,W,H,lblW,AllowEmpty,Bold,Data_Source,Display_member,Value_Member,Control_Type,Default_Option,Group_Id) select Ma,mota,stt,{0},Rtxt,topLabel,multiline,X,Y,W,H,lblW,AllowEmpty,Bold,Data_Source,Display_member,Value_Member,Control_Type,Default_Option,Group_Id from sys_dynamic_controls where MultiReport_ID={1}",  MultiReportId,sourceId);
                    QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandSql = sql;
                    DataService.ExecuteQuery(cmd);
                    Utility.ShowMsg(string.Format("Đã sao chép cấu hình từ báo cáo {0} thành công. Nhấn Ok để nạp lại dữ liệu", cboMultiReport.Text));
                    LoadData();
                }
            }
            catch (Exception ex)
            {


            }
        }

       
    }
}
