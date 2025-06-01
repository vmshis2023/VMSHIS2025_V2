using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
namespace VNS.Properties
{
    public partial class frm_Properties : Form
    {
        
       
        object _property = null;
        public frm_Properties()
        {
            InitializeComponent();
           
            
            this.Load += new EventHandler(frm_Properties_Load);
            this.FormClosing += new FormClosingEventHandler(frm_Properties_FormClosing);
            this.KeyDown += new KeyEventHandler(frm_Properties_KeyDown);
            grdProperties.PropertyValueChanged += new PropertyValueChangedEventHandler(grdProperties_PropertyValueChanged);
            cmdTestDB.Click+=cmdTestDB_Click;
        }
        void cmdTestDB_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                SetMsg(lblMsg1, "", true);
                string sConnectionString = "Server=" + PropertyLib._ConfigProperties.DataBaseServer + ";Database=" + PropertyLib._ConfigProperties.DataBaseName + ";uid=" + PropertyLib._ConfigProperties.UID + ";pwd=" + PropertyLib._ConfigProperties.PWD;
                using (SqlConnection Conn = new SqlConnection(sConnectionString))
                {
                    Conn.Open();
                    SetMsg(lblMsg1, "Connect suceeded", true);
                }
            }
            catch (Exception ex)
            {
                SetMsg(lblMsg1, ex.Message, true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        public void SetMsg(Label lblMsg, string Message, bool isErr)
        {
            if (isErr)
            {
                lblMsg.ForeColor = Color.Red;
            }
            else
            {
                lblMsg.ForeColor = Color.DarkBlue;
            }
            lblMsg.Text = Message;
        }
        void grdProperties_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            
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
            globalVariables.m_strPropertiesFolder = Application.StartupPath + @"\Properties\";
            PropertyLib._ConfigProperties = PropertyLib.GetConfigProperties();
            
            this._property = PropertyLib._ConfigProperties ;
            grdProperties.SelectedObject = _property;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
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
            PropertyLib.SaveProperty(_property);
            SetMsg(lblMsg1, "Saved", true);
        }

        private void cmdTestDB_Click_1(object sender, EventArgs e)
        {

        }
        
    }
}
