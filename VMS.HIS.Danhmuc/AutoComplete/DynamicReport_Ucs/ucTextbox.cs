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
    public partial class ucTextbox : ucCtrl
    {
        public bool AllowSave = true;
        bool AutoSaveWhenEnterKey = false;
        public bool isSaved = false;
        public delegate void OnEnterKey(ucTextbox obj);
        public event OnEnterKey _OnEnterKey;
        bool AllowMultiline = false;
        public long IdChidinhchitiet = -1;
        public bool onlyView = false;
        public  ucTextbox()
        {
            InitializeComponent();
        }
        public ucTextbox(DataRow dr)
        {
            InitializeComponent();
            txt.KeyDown += txt_KeyDown;
            Code = Utility.sDbnull(dr[DynamicField.Columns.Ma], "-1");
            Group_Id = Utility.sDbnull(dr[SysDynamicControl.Columns.GroupId], "-1");
            this.dr = dr;
            if (Utility.sDbnull(dr[DynamicField.Columns.AllowEmpty], "0") == "0")
                lblName.ForeColor = Color.Red;
        }
        public override void FilterMe(string filter)
        {
        }
        void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                base.RaiseOnEnterKey();
        }
        public override void FocusMe()
        {
            txt.Select();
            txt.Focus();
        }
        public override void SetTabIndex(int TabIdx)
        {
            txt.TabStop = true;
            txt.TabIndex = TabIdx;
        }
        public override void SetEnable(bool isEnable)
        {
            txt.Enabled = isEnable;
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
                return Utility.sDbnull(txt.Text);
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
                    //lblName.TextAlign = Utility.Byte2Bool(dr[DynamicField.Columns.TopLabel]) ? ContentAlignment.MiddleCenter : ContentAlignment.TopRight;
                    lblName.Text = Utility.sDbnull(dr[DynamicField.Columns.Mota], "");
                    _Name = lblName.Text;
                    lblName.Width = Utility.Int32Dbnull(dr[SysDynamicControl.Columns.LblW], 0);
                }
                else
                {
                    lblName.Text = "UnKnown";
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
