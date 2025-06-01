using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using VMS.HIS.Danhmuc.AutoComplete;

namespace VMS.HIS.Danhmuc.AutoComplete.DynamicReport_Ucs
{
    public partial class ucAutoComplete : ucCtrl
    {
        
      
        public bool AllowSave = true;
        bool AutoSaveWhenEnterKey = false;
        public bool isSaved=false;
        public delegate void OnEnterKey(ucAutoComplete obj);
        public event OnEnterKey _OnEnterKey;
        bool AllowMultiline = false;
        public long IdChidinhchitiet = -1;
        public bool onlyView = false;
        public ucAutoComplete()
        {
            InitializeComponent();
           
        }
        public ucAutoComplete(DataRow dr)
        {
            InitializeComponent();
            Code = Utility.sDbnull(dr[DynamicField.Columns.Ma], "-1");
            Group_Id = Utility.sDbnull(dr[SysDynamicControl.Columns.GroupId], "-1");
            txtValue._OnEnterKey += txtValue__OnEnterKey;
            txtValue.KeyDown += txtValue_KeyDown;
            txtValue.LostFocus += txtValue_LostFocus;
            txtValue.TextChanged += txtValue_TextChanged;
            this.dr = dr;
            txtValue.LOAI_DANHMUC = dr["Loai_danhmuc"].ToString();
           
            if (Utility.sDbnull(dr[DynamicField.Columns.AllowEmpty], "0") == "0")
                lblName.ForeColor = Color.Red;
            
            
        }
        public override void FilterMe(string filter)
        {
        }
            void txtValue__OnEnterKey(VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung obj)
        {

            base.RaiseOnEnterKey();

        }
        void txtValue_LostFocus(object sender, EventArgs e)
        {
            //if (!isSaved)
            //    Save();
        }
        void txtValue_TextChanged(object sender, EventArgs e)
        {
            isSaved = false;
        }

        void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
        void txtValue__OnEnterMe()
        {
          
           
        }

        public bool _AllowMultiline
        {
            get { return AllowMultiline; }
            set
            {
                AllowMultiline = value;
            }
        }
        public override void FocusMe()
        {
            txtValue.Select();
            txtValue.Focus();
        }
        public override void SetEnable(bool isEnable)
        {
            txtValue.Enabled = isEnable;
        }
        public override void SetTabIndex(int TabIdx)
        {
            txtValue.TabStop = true;
            txtValue.TabIndex = TabIdx;
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
               if (dr != null )
               {
                   
                   //lblName.TextAlign = Utility.Byte2Bool(dr[DynamicField.Columns.TopLabel]) ? ContentAlignment.TopCenter : ContentAlignment.TopRight;
                   lblName.Text = Utility.sDbnull(dr[DynamicField.Columns.Mota], "");
                   _Name = lblName.Text;
                    lblName.Width = Utility.Int32Dbnull(dr[SysDynamicControl.Columns.LblW], 0);
                    string LOAI_DANHMUC = Utility.sDbnull(dr["Loai_danhmuc"], "");
                   if (Utility.sDbnull(LOAI_DANHMUC).Length > 0)
                   {
                       txtValue.LOAI_DANHMUC = LOAI_DANHMUC;
                       txtValue.Init();
                   }
                   else
                   {
                       string Sql = Utility.sDbnull(dr["Data_Source"], "");
                       string Display_member = Utility.sDbnull(dr["Display_member"], "");
                       string Value_Member = Utility.sDbnull(dr["Value_Member"], "");
                       string Default_Option = Utility.sDbnull(dr["Default_Option"], "");
                       if (Sql.Length > 0)
                       {
                           QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
                           cmd.CommandType = CommandType.Text;
                           cmd.CommandSql = Sql;
                           DataTable dtSource = DataService.GetDataSet(cmd).Tables[0];
                           txtValue.Init(dtSource, new List<string>() { "id", Value_Member, Display_member });
                       }
                   }
                   
               }
               else
               {
                   lblName.Text = "UnKnown";
                   txtValue._Text = "";
               }

            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public override string myValue
        {
            get
            {
                return txtValue.myCode;
            }
            set
            {
                txtValue.SetCode(value);
            }
        }
        public string _Mota
        {
            set { lblName.Text = value; }
            get { return lblName.Text; }
        }
        public string _Giatri
        {
            set { txtValue._Text = value; }
            get { return txtValue.Text; }
        }
        public override string _Ma
        {
            set { Code = value; }
            get { return Utility.sDbnull(dr[DynamicField.Columns.Ma], "-1"); }
        }
        
        
    }
}
