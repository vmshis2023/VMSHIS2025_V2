using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.Libs;
using SubSonic;

namespace VMS.HIS.Danhmuc.AutoComplete.DynamicReport_Ucs
{
    
    public partial class ucComboboxAutoComplete : ucCtrl
    {
       
        public bool AllowSave = true;
        bool AutoSaveWhenEnterKey = false;
        public bool isSaved = false;
        public delegate void OnEnterKey(ucComboboxAutoComplete obj);
        public event OnEnterKey _OnEnterKey;

      

        bool AllowMultiline = false;
        public long IdChidinhchitiet = -1;
        public bool onlyView = false;
        DataTable dtSource = null;
        string Display_member = "";
        string Value_Member = "";
        string Default_Option = "";
        public ucComboboxAutoComplete()
        {
            InitializeComponent();
        }
        public ucComboboxAutoComplete(DataRow dr)
        {
            InitializeComponent();
            cbo.KeyDown += cbo_KeyDown;
            cbo.SelectedIndexChanged += Cbo_SelectedIndexChanged;
            Code = Utility.sDbnull(dr[DynamicField.Columns.Ma], "-1");
            Group_Id = Utility.sDbnull(dr[SysDynamicControl.Columns.GroupId], "-1");
            this.dr = dr;
            if (Utility.sDbnull(dr[DynamicField.Columns.AllowEmpty], "0") == "0")
                lblName.ForeColor = Color.Red;
        }

        private void Cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RaiseOnSelectedIndexChanged(Utility.sDbnull(dr["parent"], ""), string.Format(Utility.sDbnull(dr["filter"], "1={0}"), Utility.sDbnull(cbo.SelectedValue, "-1")));
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        protected override void RaiseOnSelectedIndexChanged(string parent, string filter)
        {
            base.RaiseOnSelectedIndexChanged(parent, filter);
        }

        public override void FilterMe(string filter)
        {
            try
            {
                if (dtSource == null || dtSource.Columns.Count <= 0) return;
                DataRow[] arrDr = dtSource.Select(filter);
                DataTable dtTemp = dtSource.Clone();
                if (arrDr.Length > 0) dtTemp = arrDr.CopyToDataTable();
                DataBinding.BindDataCombobox(cbo, dtTemp, Value_Member, Display_member, Default_Option, "-1", true);
                //var dts = cbo.DataSource as DataTable;
                if (cbo.Items.Count >= 1)
                    cbo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void cbo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                base.RaiseOnEnterKey();
        }
        public override  void SetEnable(bool isEnable)
        {
            cbo.Enabled = isEnable;
        }
        public override void FocusMe()
        {
            cbo.Select();
            cbo.Focus();
        }
        public override void SetTabIndex(int TabIdx)
        {
            cbo.TabStop = true;
            cbo.TabIndex = TabIdx;
        }
        public override string _Ma
        {
            set { Code = value; }
            get { return Utility.sDbnull(dr[DynamicField.Columns.Ma], "-1"); }
        }
        public override string myValue
        {
            get
            {
                return Utility.sDbnull( cbo.SelectedValue,"-1");
            }
            set
            {
                
            }
        }
        public override void setTitle(string title)
        {
            lblName.Text = title;
            _Name = lblName.Text;
        }
        public override void Init()
        {
            try
            {
                if (dr != null)
                {
                   // lblName.TextAlign = Utility.Byte2Bool(dr[DynamicField.Columns.TopLabel]) ? ContentAlignment.TopCenter : ContentAlignment.TopRight;
                    lblName.Text = Utility.sDbnull(dr[DynamicField.Columns.Mota], "");
                    _Name = lblName.Text;
                    lblName.Width = Utility.Int32Dbnull(dr[SysDynamicControl.Columns.LblW], 0);
                    string Sql = Utility.sDbnull(dr["Data_Source"], "");
                     Display_member = Utility.sDbnull(dr["Display_member"], "");
                     Value_Member = Utility.sDbnull(dr["Value_Member"], "");
                     Default_Option = Utility.sDbnull(dr["Default_Option"], "");
                    if (Sql.Length > 0)
                    {
                        if (Sql.ToUpper().Contains("SELECT") || Sql.ToUpper().Contains("FROM") || Sql.ToUpper().Contains("WHERE"))
                        {
                            QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandSql = Sql;
                             dtSource = DataService.GetDataSet(cmd).Tables[0];
                            DataBinding.BindDataCombobox(cbo, dtSource, Value_Member, Display_member, Default_Option, "-1", true);
                            //var dts = cbo.DataSource as DataTable;
                            if (cbo.Items.Count >= 1)
                                cbo.SelectedIndex = 0;
                        }
                        else//Dạng List MA1#TEN1;MA2#TEN2
                        {
                            QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandSql = string.Format("select '' as {0},'' as {1} where 1=2", Value_Member, Display_member);
                             dtSource = DataService.GetDataSet(cmd).Tables[0];
                            List<string> lstPair = Sql.Split(';').ToList<string>();
                            foreach (string _pair in lstPair)
                            {
                                List<string> lstValues=_pair.Split('#').ToList<string>();
                                DataRow newDr = dtSource.NewRow();
                                newDr[Value_Member] = lstValues[0];
                                newDr[Display_member] = lstValues[1];
                                dtSource.Rows.Add(newDr);
                            }
                            dtSource.AcceptChanges();
                            DataBinding.BindDataCombobox(cbo, dtSource, Value_Member, Display_member, Default_Option, "-1", true);
                            //var dts = cbo.DataSource as DataTable;
                            if (cbo.Items.Count >= 1)
                                cbo.SelectedIndex = 0;
                        }
                    }

                }
                else
                {
                    lblName.Text = "UnKnown";
                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
    }
    
}
