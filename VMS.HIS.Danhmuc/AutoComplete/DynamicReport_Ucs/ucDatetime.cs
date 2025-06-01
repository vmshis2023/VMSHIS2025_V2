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
    public partial class ucDatetime : ucCtrl
    {
        
        public bool AllowSave = true;
        bool AutoSaveWhenEnterKey = false;
        public bool isSaved = false;
        public delegate void OnEnterKey(ucDatetime obj);
        public event OnEnterKey _OnEnterKey;
        bool AllowMultiline = false;
        public long IdChidinhchitiet = -1;
        public bool onlyView = false;
        public ucDatetime()
        {
            InitializeComponent();
        }
        public ucDatetime(DataRow dr)
        {
            InitializeComponent();
            dtpdate.KeyDown += dtpdate_KeyDown;
            dtpdate.Value = DateTime.Now;
            Code = Utility.sDbnull(dr[SysDynamicControl.Columns.Ma], "-1");
            Group_Id = Utility.sDbnull(dr[SysDynamicControl.Columns.GroupId], "-1");
            this.dr = dr;
            if (Utility.sDbnull(dr[SysDynamicControl.Columns.AllowEmpty], "0") == "0")
                lblName.ForeColor = Color.Red;
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
                return dtpdate.Text;
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
            lblName.Text = title;
            _Name = lblName.Text;
        }
        public override void Init()
        {
            try
            {
                if (dr != null)
                {
                    //lblName.TextAlign = Utility.Byte2Bool(dr[DynamicField.Columns.TopLabel]) ? ContentAlignment.TopCenter : ContentAlignment.TopRight;
                    lblName.Text = Utility.sDbnull(dr[SysDynamicControl.Columns.Mota], "");
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
