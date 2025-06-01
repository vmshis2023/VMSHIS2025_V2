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
    
    public partial class ucCombobox_AutoComplete : ucCtrl
    {
       
        public bool AllowSave = true;
        bool AutoSaveWhenEnterKey = false;
        public bool isSaved = false;
        public delegate void OnEnterKey(ucCombobox_AutoComplete obj);
        public event OnEnterKey _OnEnterKey;
        bool AllowMultiline = false;
        public long IdChidinhchitiet = -1;
        public bool onlyView = false;
        public ucCombobox_AutoComplete()
        {
            InitializeComponent();
        }
        public ucCombobox_AutoComplete(DataRow dr)
        {
            InitializeComponent();
            cbo.KeyDown += cbo_KeyDown;
            Code = Utility.sDbnull(dr[DynamicField.Columns.Ma], "-1");
            Group_Id = Utility.sDbnull(dr[SysDynamicControl.Columns.GroupId], "-1");
            this.dr = dr;
            if (Utility.sDbnull(dr[DynamicField.Columns.AllowEmpty], "0") == "0")
                lblName.ForeColor = Color.Red;
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
                    string Display_member = Utility.sDbnull(dr["Display_member"], "");
                    string Value_Member = Utility.sDbnull(dr["Value_Member"], "");
                    string Default_Option = Utility.sDbnull(dr["Default_Option"], "");
                    if (Sql.Length > 0)
                    {
                        if (Sql.ToUpper().Contains("SELECT") || Sql.ToUpper().Contains("FROM") || Sql.ToUpper().Contains("WHERE"))
                        {
                            QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandSql = Sql;
                            DataTable dtSource = DataService.GetDataSet(cmd).Tables[0];
                            BindDataCombobox(cbo, dtSource, Value_Member, Display_member, Default_Option, "-1", true);
                            if (cbo.Items.Count >= 1)
                                cbo.SelectedIndex = 0;
                        }
                        else//Dạng List MA1#TEN1;MA2#TEN2
                        {
                            QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandSql = string.Format("select '' as {0},'' as {1} where 1=2", Value_Member, Display_member);
                            DataTable dtSource = DataService.GetDataSet(cmd).Tables[0];
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
                            BindDataCombobox(cbo, dtSource, Value_Member, Display_member, Default_Option, "-1", true);
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
        void BindDataCombobox(VNS.HIS.UCs.EasyCompletionComboBox objCombobox, DataTable data,
                                    string dataValueField, string dataTextField, string defaultItem, string defaultValue, bool AddDefaultIfNoData)
        {
            try
            {
                if (data == null) return;
                DataTable dt = new DataTable();
                dt = data.Copy();
                if (dt.Columns.Count <= 0 || !dt.Columns.Contains(dataTextField) || !dt.Columns.Contains(dataValueField)) return;
                if (AddDefaultIfNoData && data.Rows.Count <= 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = defaultItem;
                    dr[dataValueField] = defaultValue;
                    dt.Rows.InsertAt(dr, 0);
                }
                else if (dt.Rows.Count > 1)
                {
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = defaultItem;
                    dr[dataValueField] = defaultValue;
                    dt.Rows.InsertAt(dr, 0);
                }
                objCombobox.DataSource = dt;
                objCombobox.ValueMember = dataValueField;
                objCombobox.DisplayMember = dataTextField;
                if (objCombobox.Items.Count > 0) objCombobox.SelectedIndex = 0;
            }
            catch
            {
            }
        }
    }
    
}
