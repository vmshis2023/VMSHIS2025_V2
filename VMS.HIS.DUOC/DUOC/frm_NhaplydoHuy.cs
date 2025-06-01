using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.HIS.DAL;
using VNS.HIS.UI.NGOAITRU;
using SubSonic;
using VNS.Libs;
using VNS.HIS.BusRule.Classes;
using VNS.Properties;
using VNS.HIS.UI.DANHMUC;
using Janus.Windows.GridEX.EditControls;
using Janus.Windows.EditControls;

namespace VNS.HIS.UI.THUOC
{
    public partial class frm_NhaplydoHuy : Form
    {
        public DateTime ngay_thuchien = DateTime.Now;
        public string ma="";
        public string ten = "";
        public bool m_blnCancel = true;
        string _name = "";
        public frm_NhaplydoHuy()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            dtNgaythuchien.Value = DateTime.Now;
            this._name = lblName.Text;
            InitEvents();
        }
        public frm_NhaplydoHuy(string loaidanhmuc,string title1,string title2,string _name,string ngaythuchien)
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            dtNgaythuchien.Value = DateTime.Now;
            this.Text = title1;
            lblDate.Text = ngaythuchien;
            txtDmucchung.LOAI_DANHMUC = loaidanhmuc;
            lblTitle1.Text = title1;
            lblTitle2.Text = title2;
            lblName.Text = _name;
            this._name = _name;
            InitEvents();
        }
        void InitEvents()
        {
            this.FormClosing += new FormClosingEventHandler(frm_NhaplydoHuy_FormClosing);
            this.Load += new EventHandler(frm_NhaplydoHuy_Load);
            this.KeyDown += new KeyEventHandler(frm_NhaplydoHuy_KeyDown);
            txtDmucchung._OnShowData += txtDmucchung__OnShowData;
            cmdClose.Click+=new EventHandler(cmdClose_Click);
            cmdSave.Click+=new EventHandler(cmdSave_Click);
        }

        void txtDmucchung__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtDmucchung.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtDmucchung.myCode;
                txtDmucchung.Init();
                txtDmucchung.SetCode(oldCode);
                txtDmucchung.Focus();
            } 
        }

        void cmdClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        void frm_NhaplydoHuy_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Escape)
                cmdClose_Click(cmdClose, new EventArgs());
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }

        void frm_NhaplydoHuy_Load(object sender, EventArgs e)
        {
            try
            {

                txtDmucchung.Init();
                txtDmucchung.Focus();
                txtDmucchung.Select();
            }
            catch
            {
            }
            finally
            {
            }
        }
     

        void frm_NhaplydoHuy_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
       

        void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.EnableButton(cmdSave, false);
                if (!isValidData()) return;
                ma = txtDmucchung.myCode;
                ten = Utility.DoTrim(txtDmucchung.Text);
                ngay_thuchien = dtNgaythuchien.Value;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                m_blnCancel = false;
                this.Close();
            }
            catch (Exception ex)
            {
                Utility.EnableButton(cmdSave, true);
                Utility.ShowMsg("Lỗi khi nhấn nút chấp nhận:\n" + ex.Message);
                throw;
            }
            finally
            {
                Utility.EnableButton(cmdSave, true);
            }
        }
     
        private bool isValidData()
        {
            Utility.SetMsg(lblMsg, "", true);
            if (txtDmucchung.myCode == "-1" || Utility.DoTrim(txtDmucchung.myCode) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập "+_name, true);
                txtDmucchung.Focus();
                txtDmucchung.SelectAll();
                return false;
            }
            return true;
        }
       
    }
}
