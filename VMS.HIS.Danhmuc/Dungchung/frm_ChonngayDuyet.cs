using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.Libs;
namespace VNS.HIS.UI.Forms.Cauhinh
{
    public partial class frm_ChonngayDuyet : Form
    {
        public string v_Patient_Code = "";
        public DateTime ngay_lapphieu = globalVariables.SysDate;
        public DateTime ngay_xacnhan = globalVariables.SysDate;
        public bool b_Cancel = true;
        public bool _yeucaunhaplydo = false;
        public frm_ChonngayDuyet(DateTime ngay_lapphieu,bool _yeucaunhaplydo, string ten_lydo)
        {
            InitializeComponent();
            this.ngay_lapphieu = ngay_lapphieu;
            this._yeucaunhaplydo = _yeucaunhaplydo;
            HienthiLydo(_yeucaunhaplydo, ten_lydo);
            this.KeyDown+=frm_ChonngayDuyet_KeyDown;
           radCurrentDate.CheckedChanged+=new EventHandler(radCurrentDate_CheckedChanged);
            radEditDate.CheckedChanged+=new EventHandler(radEditDate_CheckedChanged);
            opt_ngay_hoadon.CheckedChanged+=new EventHandler(radRegisterDate_CheckedChanged);
            dtCreateDate.Value = globalVariables.SysDate;
        }
        public void HienthiLydo(bool _visible,string ten_lydo)
        {
            _yeucaunhaplydo = _visible;
            lblNhanvien.Visible = _visible;
            txt_lydo.Visible = _visible;
            if (_yeucaunhaplydo)
            {
                Utility.SetMsg(lblNhanvien, ten_lydo, true);
                this.AcceptButton = null;
            }
           
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(lblMsg, "", true);
            if (dtCreateDate.Value < ngay_lapphieu)
            {
                Utility.SetMsg(lblMsg, string.Format("Ngày xác nhận phải >= ngày lập phiếu: {0}", ngay_lapphieu.ToString("dd/MM/yyyy HH:mm:ss")), true);
                dtCreateDate.Focus();
                return;
            }
            if (_yeucaunhaplydo && Utility.sDbnull(txt_lydo.Text)=="")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập "+lblNhanvien.Text, true);
                txt_lydo.SelectAll();
                txt_lydo.Focus();
                return;
            }

            b_Cancel = false;
            ngay_xacnhan = dtCreateDate.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frm_ChonngayDuyet_Load(object sender, EventArgs e)
        {

        }

        private void radRegisterDate_CheckedChanged(object sender, EventArgs e)
        {
            dtCreateDate.Value = ngay_lapphieu;
        }

        private void frm_ChonngayDuyet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) this.ProcessTabKey(true);
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if(e.Control&&(e.KeyCode==Keys.A || e.KeyCode==Keys.S))cmdAccept.PerformClick();
        }

        private void radEditDate_CheckedChanged(object sender, EventArgs e)
        {
            dtCreateDate.Enabled = radEditDate.Checked;
            dtCreateDate.Focus();
        }

        private void radCurrentDate_CheckedChanged(object sender, EventArgs e)
        {
            dtCreateDate.Value = DateTime.Now;
        }

        private void dtCreateDate_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
