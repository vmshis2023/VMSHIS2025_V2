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

namespace VMS.HIS.Danhmuc.AutoComplete.DynamicReport_Ucs
{
    public partial class ucDatetime_Checked : ucCtrl
    {
       
        public bool AllowSave = true;
        public bool isSaved = false;
        public delegate void OnEnterKey(ucDatetime_Checked obj);
        public event OnEnterKey _OnEnterKey;
       
        

        public long IdChidinhchitiet = -1;
        public bool onlyView = false;
        public ucDatetime_Checked()
        {
            InitializeComponent();
        }
        public ucDatetime_Checked(DataRow dr)
        {
            InitializeComponent();
            dtpdate.KeyDown += dtpdate_KeyDown;
            dtpdate.Value = DateTime.Now;
            chk.CheckedChanged += chk_CheckedChanged;
            Code = Utility.sDbnull(dr[DynamicField.Columns.Ma], "-1");
            Group_Id = Utility.sDbnull(dr[SysDynamicControl.Columns.GroupId], "-1");
            this.dr = dr;
            if (Utility.sDbnull(dr[DynamicField.Columns.AllowEmpty], "0") == "0")
                chk.ForeColor = Color.Red;
        }
        public override void FilterMe(string filter)
        {
        }
        void dtpdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                base.RaiseOnEnterKey();
        }
        public override void SetEnable(bool isEnable)
        {
            dtpdate.Enabled = isEnable;
        }
        void chk_CheckedChanged(object sender, EventArgs e)
        {
            RaiseOnchangeOfGroup(chk.Checked, Group_Id);
        }
        protected override void RaiseOnchangeOfGroup(bool isChecked, string GroupID)
        {
            base.RaiseOnchangeOfGroup(isChecked, GroupID);
        }

        public override void FocusMe()
        {
            dtpdate.Select();
            dtpdate.Focus();
        }
        public override void SetTabIndex(int TabIdx)
        {
            dtpdate.TabStop = true;
            dtpdate.TabIndex = TabIdx;
        }
        public override string myValue
        {
            get
            {
                if (chk.Checked)
                    return dtpdate.Text;
                else return "01/01/1900";
            }
            set
            {

            }
        }
        public override string _Ma
        {
            set { Code = value; }
            get { return Utility.sDbnull(dr[DynamicField.Columns.Ma], "-1"); }
        }
        public override void setTitle(string title)
        {
            chk.Text = title;
            _Name = chk.Text;
        }
        public override void Init()
        {
            try
            {
                if (dr != null)
                {
                    //chk.TextAlign = Utility.Byte2Bool(dr[DynamicField.Columns.TopLabel]) ? ContentAlignment.TopCenter : ContentAlignment.TopRight;
                    chk.Text = Utility.sDbnull(dr[DynamicField.Columns.Mota], "");
                    _Name = chk.Text;
                    chk.Width = Utility.Int32Dbnull(dr[SysDynamicControl.Columns.LblW], 0);
                }
                else
                {
                    chk.Text = "UnKnown";
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
