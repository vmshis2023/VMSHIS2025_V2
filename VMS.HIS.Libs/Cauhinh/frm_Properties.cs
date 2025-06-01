using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace VNS.Properties
{
    public partial class frm_Properties : Form
    {
        
       
        object _property = null;
        public delegate void OnRefreshData(object _property);
        public event OnRefreshData _OnRefreshData;
        string Folder = "";
        public frm_Properties(object _property)
        {
            InitializeComponent();
           
            this._property = _property;
            this.Load += new EventHandler(frm_Properties_Load);
            this.FormClosing += new FormClosingEventHandler(frm_Properties_FormClosing);
            this.KeyDown += new KeyEventHandler(frm_Properties_KeyDown);
            grdProperties.PropertyValueChanged += new PropertyValueChangedEventHandler(grdProperties_PropertyValueChanged);
        }
        public frm_Properties(object _property, string Folder)
        {
            InitializeComponent();
            this.Folder = Folder;
            this._property = _property;
            this.Load += new EventHandler(frm_Properties_Load);
            this.FormClosing += new FormClosingEventHandler(frm_Properties_FormClosing);
            this.KeyDown += new KeyEventHandler(frm_Properties_KeyDown);
            grdProperties.PropertyValueChanged += new PropertyValueChangedEventHandler(grdProperties_PropertyValueChanged);
        }
        void grdProperties_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (_OnRefreshData != null) _OnRefreshData(_property);
        }

        void frm_Properties_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdCancel_Click(cmdCancel, new EventArgs());
        }

        void frm_Properties_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        void frm_Properties_Load(object sender, EventArgs e)
        {
            grdProperties.SelectedObject = _property;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if(Folder.Length>0)
                PropertyLib.SaveProperty( Folder,_property);
            else
            PropertyLib.SaveProperty(_property);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            if (Folder.Length > 0)
                PropertyLib.SaveProperty(Folder, _property);
            else
                PropertyLib.SaveProperty(_property);
            if (_OnRefreshData != null) _OnRefreshData(_property);
        }
        
    }
}
