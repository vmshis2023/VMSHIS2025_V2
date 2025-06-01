using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.HIS.DAL;
using SubSonic;
using Janus.Windows.GridEX;
using VNS.Libs;
using System.Transactions;
using VNS.Properties;
using VNS.HIS.UI.DANHMUC;

namespace VNS.HIS.UI.HinhAnh
{
    public partial class frm_DynamicFields : Form
    {
        public DmucVungkhaosat objvks = null;
        
        public long ImageID = -1;
        public long Id_chidinhchitiet = -1;
        public string MafileDoc ="-1";
        bool hasDeleted = false;
        public frm_DynamicFields()
        {
            InitializeComponent();
            Config();
            this.Load += frm_DynamicFields_Load;
            this.KeyDown += frm_DynamicFields_KeyDown;
            grdList.UpdatingCell += grdList_UpdatingCell;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
        }
        DataTable dtData = null;
       

        

       
        void cmdConfig_Click(object sender, EventArgs e)
        {
            //frm_Properties _Properties = new frm_Properties(PropertyLib._DynamicInputProperties);
            //_Properties.ShowDialog();
            Config();
        }
        void Config()
        {
        }
       

       
        void frm_DynamicFields_KeyDown(object sender, KeyEventArgs e)
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
        

        void frm_DynamicFields_Load(object sender, EventArgs e)
        {
            LoadData();
            Utility.focusCell(grdList, DynamicField.Columns.Ma);
        }
        void LoadData()
        {
            try
            {
                dtData = SPs.HinhanhGetDynamicFieldsValues(-1,-1).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdList, dtData, true, true, "1=1", "Stt_hthi");
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
                    FillDynamicValues();
                    Utility.focusCell(grdList, DynamicField.Columns.Ma);
                }

            }
            catch (Exception ex)
            {
            }
        }
        

        private void grdList_FormattingRow(object sender, RowLoadEventArgs e)
        {

        }
    }
}
