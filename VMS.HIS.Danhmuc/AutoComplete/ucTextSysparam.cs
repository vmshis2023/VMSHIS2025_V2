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

namespace VNS.UCs
{
    public partial class ucTextSysparam : UserControl
    {
        string _pName = "";
        public ucTextSysparam()
        {
            InitializeComponent();
        }
        public void Init()
        {
            try
            {
               SysSystemParameter obj =THU_VIEN_CHUNG.Laythamsohethong(_pName);
               if (obj != null)
               {
                   txtValue.Text = obj.SValue;
                   lblDesc.Text = obj.SDesc;
               }
               else
               {
                   _paramName = "UNKNOWN";
               }

            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public string _paramName
        {
            set { _pName = value; }
            get { return _pName; }
        }
        public string _paramTitle
        {
            set { lblName.Text = value; }
            get { return lblName.Text; }
        }
        public string _paramValue
        {
            set { txtValue.Text = value; }
            get { return txtValue.Text; }
        }
        public void Save()
        {
            THU_VIEN_CHUNG.Capnhatgiatrithamsohethong(_pName, Utility.DoTrim(txtValue.Text));
        }
    }
}
