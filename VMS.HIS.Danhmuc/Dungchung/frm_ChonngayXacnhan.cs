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
    public partial class frm_ChonngayXacnhan : Form
    {
        public string v_Patient_Code = "";
        public DateTime pdt_InputDate = globalVariables.SysDate;
        public bool b_Cancel = true;
        public bool _hienthinhanvien = false;
        public frm_ChonngayXacnhan()
        {
            InitializeComponent();
            this.KeyDown+=frm_ChonngayXacnhan_KeyDown;
           radCurrentDate.CheckedChanged+=new EventHandler(radCurrentDate_CheckedChanged);
            radEditDate.CheckedChanged+=new EventHandler(radEditDate_CheckedChanged);
            radRegisterDate.CheckedChanged+=new EventHandler(radRegisterDate_CheckedChanged);
            dtCreateDate.Value = globalVariables.SysDate;
        }
        public void HienthiNhanvien(bool _visible)
        {
            _hienthinhanvien = _visible;
            lblNhanvien.Visible = _visible;
            txtNhanvien.Visible = _visible;
            if (_hienthinhanvien)
            {
                Utility.SetMsg(lblNhanvien, lblNhanvien.Text, true);
                this.AcceptButton = null;
            }
            if(_visible)
              txtNhanvien.Init(THU_VIEN_CHUNG.Laydanhsachnhanvien("ALL"), new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(lblMsg, "", true);
            if (dtCreateDate.Value < pdt_InputDate)
            {
                Utility.SetMsg(lblMsg, string.Format("Ngày xác nhận phải >= ngày lập phiếu: {0}", pdt_InputDate.ToString("dd/MM/yyyy HH:mm:ss")), true);
                dtCreateDate.Focus();
                return;
            }
            if (_hienthinhanvien && Utility.Int32Dbnull(txtNhanvien.MyID, -1) <= 0)
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập "+lblNhanvien.Text, true);
                txtNhanvien.SelectAll();
                txtNhanvien.Focus();
                return;
            }
            b_Cancel = false;
            pdt_InputDate = dtCreateDate.Value;
            this.Close();
        }

        private void frm_ChonngayXacnhan_Load(object sender, EventArgs e)
        {

        }

        private void radRegisterDate_CheckedChanged(object sender, EventArgs e)
        {
            dtCreateDate.Value = pdt_InputDate;
        }

        private void frm_ChonngayXacnhan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && _hienthinhanvien) this.ProcessTabKey(true);
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
