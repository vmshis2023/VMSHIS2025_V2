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

namespace VNS.HIS.UI.HinhAnh
{
    public partial class frm_DynamicSetup : Form
    {
        public DmucVungkhaosat objvks = null;
        
        public long ImageID = -1;
        public long Id_chidinhchitiet = -1;
        public string MafileDoc ="-1";
        bool hasDeleted = false;
        DataRow _currentDr = null;
        public frm_DynamicSetup()
        {
            InitializeComponent();
            Config();
            this.Load += frm_DynamicSetup_Load;
            this.KeyDown += frm_DynamicSetup_KeyDown;
            this.FormClosing += frm_DynamicSetup_FormClosing;
            cmdExit.Click+=cmdExit_Click;
            grdList.UpdatingCell += grdList_UpdatingCell;
            grdList.SelectionChanged += grdList_SelectionChanged;
            grdList.DeletingRecords += grdList_DeletingRecords;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            cmdConfig.Click += cmdConfig_Click;
        }

        void grdList_SelectionChanged(object sender, EventArgs e)
        {
            if (Utility.isValidGrid(grdList) && Utility.Int32Dbnull(grdList.CurrentRow.Cells["ID"].Value, "-1") > 0)
                _currentDr = (((DataRowView)grdList.CurrentRow.DataRow).Row);
        }
        DataTable dtData = null;
        void FillDynamicValues()
        {
            try
            {
                pnlDynamicValues.Controls.Clear();

               dtData =  SPs.HinhanhGetDynamicFieldsValues(objvks.Id,-1).GetDataSet().Tables[0];

                foreach (DataRow dr in dtData.Select("1=1", "Stt_hthi"))
                {
                    VNS.UCs.ucAutoCompleteParam _ucp= new VNS.UCs.ucAutoCompleteParam(dr, true);
                    _ucp.txtValue.VisibleDefaultItem = true;
                    _ucp.lblName.Font = PropertyLib._HinhAnhProperties.DynamicFontChu;
                    _ucp.txtValue.Font = PropertyLib._HinhAnhProperties.DynamicFontChu;
                    _ucp.txtValue.ReadOnly = false;
                    _ucp.txtValue._OnShowData += txtValue__OnShowData;
                    _ucp.TabStop = true;
                    _ucp.txtValue.Multiline = Utility.Int32Dbnull(dr[DynamicField.Columns.Multiline], 0)==1;
                    _ucp.Width = Utility.Int32Dbnull(dr[DynamicField.Columns.W], 0);
                    _ucp.Height = Utility.Int32Dbnull(dr[DynamicField.Columns.H], 0);
                    _ucp.lblName.Width = Utility.Int32Dbnull(dr[DynamicField.Columns.LblW], 0);
                    _ucp.TabIndex = 10 + Utility.Int32Dbnull(dr[DynamicField.Columns.Stt], 0);
                    _ucp.Init();
                    //_ucp.Size = PropertyLib._DynamicInputProperties.DynamicSize;
                    //_ucp.txtValue.Size = PropertyLib._DynamicInputProperties.TextSize;
                    //_ucp.lblName.Size = PropertyLib._DynamicInputProperties.LabelSize;
                    pnlDynamicValues.Controls.Add(_ucp);
                }
            }
            catch (Exception ex)
            {


            }
        }

        void txtValue__OnShowData(UCs.AutoCompleteTextbox_DynamicField txt)
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(txt.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt.myCode;
                txt.Init();
                txt.SetCode(oldCode);
                txt.Focus();
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
            FillDynamicValues();
        }
        void frm_DynamicSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hasDeleted)
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        void grdList_DeletingRecords(object sender, CancelEventArgs e)
        {
            try
            {
                int _Id=Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdList,DynamicField.Columns.Id), -1);
                if (_Id > 0)
                {
                     DynamicField.Delete(_Id);
                     hasDeleted = true;
                }
            }
            catch (Exception)
            {
                
                
            }
        }

        void frm_DynamicSetup_KeyDown(object sender, KeyEventArgs e)
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
                var q = from p in grdList.GetDataRows().AsEnumerable()
                        where p != grdList.CurrentRow
                        && Utility.sDbnull(p.Cells[DynamicField.Columns.Ma], "") == e.Value
                        select p;
                if (q.Count() > 0)
                {
                    Utility.ShowMsg("Mã này đã tồn tại, bạn cần nhập mã khác!");
                    e.Cancel = true;
                }
            }
            catch (Exception)
            {
            }
        }

        void frm_DynamicSetup_Load(object sender, EventArgs e)
        {
            LoadData();
            Utility.focusCell(grdList, DynamicField.Columns.Ma);
        }
        void LoadData()
        {
            try
            {
                if(objvks==null)
                {
                    Utility.ShowMsg("Không xác định được danh mục dịch vụ CĐHA. Bạn cần kiểm tra lại danh mục hệ thống");
                    cmdSave.Enabled = false;
                }
                dtData = SPs.HinhanhGetDynamicFieldsValues(objvks.Id,-1).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdList, dtData, true, true, "1=1", "Stt_hthi");
                FillDynamicValues();
            }
            catch (Exception ex)
            {
            }
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<DynamicField> lstFields = new List<DynamicField>();
                foreach (GridEXRow _row in grdList.GetDataRows())
                {
                    DynamicField obj = null;

                    if (Utility.Int32Dbnull(_row.Cells[DynamicField.Columns.Id].Value, -1) > 0)
                    {
                        obj = DynamicField.FetchByID(Utility.Int32Dbnull(_row.Cells[DynamicField.Columns.Id].Value, -1));
                        obj.IsNew = false;
                        obj.MarkOld();
                    }
                    else
                    {
                        obj = new DynamicField();
                        obj.IsNew = true;
                    }

                    obj.IdVungks = objvks.Id;
                    obj.Ma = Utility.sDbnull(_row.Cells[DynamicField.Columns.Ma].Value, "-1");
                    obj.Mota = Utility.sDbnull(_row.Cells[DynamicField.Columns.Mota].Value, "-1");
                    obj.Stt = Utility.Int16Dbnull(_row.Cells[DynamicField.Columns.Stt].Value, 0);
                    obj.Multiline = Utility.ByteDbnull(_row.Cells[DynamicField.Columns.Multiline].Value, 0);
                    obj.TopLabel = Utility.ByteDbnull(_row.Cells[DynamicField.Columns.TopLabel].Value, 0);
                    //obj.Multiline = Utility.ByteDbnull(_row.Cells[DynamicField.Columns.Multiline].Value, 0);
                    obj.AllowEmpty = Utility.ByteDbnull(_row.Cells[DynamicField.Columns.AllowEmpty].Value, 0);
                    obj.W = Utility.Int16Dbnull(_row.Cells[DynamicField.Columns.W].Value, 0);
                    obj.H = Utility.Int16Dbnull(_row.Cells[DynamicField.Columns.H].Value, 0);
                    obj.LblW = Utility.Int16Dbnull(_row.Cells[DynamicField.Columns.LblW].Value, 0);
                    lstFields.Add(obj);
                }
                ActionResult _actionResult = UpdateDynamicFields(lstFields);
                if (_actionResult == ActionResult.Success)
                {
                   // this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    LoadData();
                    //FillDynamicValues();
                    Utility.focusCell(grdList, DynamicField.Columns.Ma);
                }

            }
            catch (Exception ex)
            {
            }
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        public DataTable GetDynamicFieldsValues()
        {
            try
            {
                return SPs.HinhanhGetDynamicFieldsValues(objvks.Id,-1).GetDataSet().Tables[0];
            }
            catch (Exception)
            {

                return null;
            }
        }
        public ActionResult UpdateDynamicFields(List<DynamicField> lstFields)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sp = new SharedDbConnectionScope())
                    {
                        foreach (DynamicField _object in lstFields)
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
                Utility.SetDataSourceForDataGridEx(grdList, dtData, true, true, "1=1", "Stt_hthi");
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
                if (Utility.Int32Dbnull(dr.Cells["W"].Value, 0) == 0)
                    dr.Cells["W"].Value = _currentDr["W"];
                if (Utility.Int32Dbnull(dr.Cells["H"].Value, 0) == 0)
                    dr.Cells["H"].Value = _currentDr["H"];
                if (Utility.Int32Dbnull(dr.Cells["lblW"].Value, 0) == 0)
                    dr.Cells["lblW"].Value = _currentDr["lblW"];
                dr.EndEdit();
            }
            grdList.Refetch();
            //dtData.AcceptChanges();
        }
    }
}
