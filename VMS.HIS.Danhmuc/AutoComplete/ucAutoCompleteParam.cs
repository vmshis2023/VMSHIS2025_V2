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

namespace VNS.UCs
{
    public partial class ucAutoCompleteParam : UserControl
    {
        
        DataRow dr = null;
        public bool AllowSave = true;
        bool AutoSaveWhenEnterKey = false;
        public bool isSaved=false;
        public delegate void OnEnterKey(ucAutoCompleteParam obj);
        public event OnEnterKey _OnEnterKey;
        bool AllowMultiline = false;
        public long IdChidinhchitiet = -1;
        public bool onlyView = false;
        public string Code = "";
        public ucAutoCompleteParam()
        {
            InitializeComponent();
           
        }
        public ucAutoCompleteParam(DataRow dr,bool AutoSaveWhenEnterKey)
        {
            InitializeComponent();
            Code = Utility.sDbnull(dr[DynamicField.Columns.Ma], "-1");
            txtValue._OnEnterMe += txtValue__OnEnterMe;
            txtValue.KeyDown += txtValue_KeyDown;
            txtValue.LostFocus += txtValue_LostFocus;
            txtValue.TextChanged += txtValue_TextChanged;
            this.dr = dr;
            txtValue.LOAI_DANHMUC = dr["MA"].ToString();
            this.AutoSaveWhenEnterKey = AutoSaveWhenEnterKey;
            if (Utility.sDbnull(dr[DynamicField.Columns.AllowEmpty], "0") == "0")
                lblName.ForeColor = Color.Red;
            
            
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
            if (e.KeyCode == Keys.Enter)
            {
                if (AutoSaveWhenEnterKey)
                    Save();
                if (_OnEnterKey != null)
                {
                    _OnEnterKey(this);
                }
            }
            
        }
        void txtValue__OnEnterMe()
        {
            if (this.AutoSaveWhenEnterKey)
                Save();
            if (_OnEnterKey != null)
            {
                _OnEnterKey(this);
            }
        }

        public bool _AllowMultiline
        {
            get { return AllowMultiline; }
            set
            {
                AllowMultiline = value;
            }
        }
        public void FocusMe()
        {
            txtValue.Select();
            txtValue.Focus();
        }
        public void Init()
        {
            try
            {
               if (dr != null )
               {
                   txtValue.Init();
                   lblName.TextAlign = Utility.Byte2Bool(dr[DynamicField.Columns.TopLabel]) ? ContentAlignment.TopCenter : ContentAlignment.TopRight;
                   _AllowMultiline = Utility.Byte2Bool(dr[DynamicField.Columns.Multiline]);
                   if (Utility.Int64Dbnull(dr["Idkq"], -1) > 0)
                       txtValue._Text = Utility.sDbnull(dr[DynamicValue.Columns.Giatri], "");
                   if (onlyView)
                   {
                       _AllowMultiline = false;
                       txtValue.ReadOnly = true;
                   }
                   txtValue.Multiline = _AllowMultiline;
                   lblName.Text = Utility.sDbnull(dr[DynamicField.Columns.Mota], "");
                   toolTip1.SetToolTip(lblName, Utility.sDbnull(dr[DynamicField.Columns.Ma], ""));
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
        public string _Ma
        {
            set { Code = value; }
            get { return Utility.sDbnull(dr[DynamicField.Columns.Ma], "-1"); }
        }
        public void Save()
        {
            try
            {
                if (!AllowSave) return;
                if (!txtValue.AllowEmpty && Utility.DoTrim(txtValue.Text) == "") return;
                List<DynamicValue> lstValues = new List<DynamicValue>();
                if (dr != null )
                {
                    DynamicValue objVal = new DynamicValue();
                    //if (Utility.Int32Dbnull(dr["Idkq"], -1) > 0)
                    //{
                    //    objVal = DynamicValue.FetchByID(Utility.Int32Dbnull(dr["Idkq"], -1));
                    //    objVal.IsNew = false;
                    //    objVal.MarkOld();
                    //}
                    //else
                    //{
                    //    objVal = new DynamicValue();
                    //    objVal.IsNew = true;
                    //}

                    objVal.Ma = Utility.sDbnull(dr[DynamicField.Columns.Ma], "-1");
                    objVal.Giatri = Utility.DoTrim(txtValue.Text);
                    objVal.IdChidinhchitiet = IdChidinhchitiet;
                    lstValues.Add(objVal);
                    clsHinhanh.UpdateDynamicValues(lstValues);
                    dr["Idkq"] = objVal.Id;
                    isSaved = true;
                }
            }
            catch (Exception ex)
            {
                
            }  
        }
        
    }
}
