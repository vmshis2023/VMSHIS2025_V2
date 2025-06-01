using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;

namespace VMS.HIS.Danhmuc.AutoComplete.DynamicReport_Ucs
{
    public class ucCtrl : UserControl
    {
        public DataRow dr = null;
        public string Group_Id = "";
        public string Code = "";
        public string _Name = "";
        public int Id = -1;
        public delegate void OnchangeOfGroup(bool isChecked, string GroupID);
        public virtual  event OnchangeOfGroup _OnchangeOfGroup;
        public delegate void OnSelectedIndexChanged(string parent, string filter);
        public virtual event OnSelectedIndexChanged _OnSelectedIndexChanged;
        public virtual void SetEnable(bool isEnable)
        {
        }
        public virtual void FocusMe()
        {
        }
        public virtual void setTitle(string title)
        {
        }
        public virtual void SetTabIndex(int TabIdx)
        {
            this.TabIndex = TabIdx;
        }
        public virtual string myValue { get; set; }
        public virtual string _Ma { get; set; }
       
        public virtual void Init()
        {

        }
        public virtual void FilterMe(string filter)
        {

        }
        protected virtual void RaiseOnSelectedIndexChanged(string parent, string filter)
        {
            if (_OnSelectedIndexChanged != null)
            {
                _OnSelectedIndexChanged(parent, filter);
            }
        }
        protected virtual void RaiseOnchangeOfGroup(bool isChecked, string GroupID)
        {
            if (_OnchangeOfGroup != null)
            {
                _OnchangeOfGroup(isChecked, GroupID);
            }
        }
        public delegate void OnEnterKey(ucCtrl obj);
        public virtual event OnEnterKey _OnEnterKey;
        public virtual void RaiseOnEnterKey()
        {
            if (_OnEnterKey != null)
                _OnEnterKey(this);
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ucCtrl
            // 
            this.Name = "ucCtrl";
            this.Size = new System.Drawing.Size(350, 22);
            this.ResumeLayout(false);

        }

    }
}
