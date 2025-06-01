using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
namespace VNS.HIS.UI.Forms.Cauhinh
{
    public partial class frm_ChonTrangthai : Form
    {
        public string v_Patient_Code = "";
        public DateTime pdt_InputDate = globalVariables.SysDate;
        public bool b_Cancel = false;
        public VNS.Properties.LoaiphieuIn _kieuin;
        public frm_ChonTrangthai(VNS.Properties.LoaiphieuIn _kieuin)
        {
            InitializeComponent();
            this._kieuin = _kieuin;
            optCahai.Checked = _kieuin == VNS.Properties.LoaiphieuIn.Cahai;
            optChitiet.Checked = _kieuin == VNS.Properties.LoaiphieuIn.Chitiet;
            optTonghop.Checked = _kieuin == VNS.Properties.LoaiphieuIn.Tonghop;
            dtCreateDate.Value = globalVariables.SysDate;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            b_Cancel = true;
            pdt_InputDate = dtCreateDate.Value;
            if (optCahai.Checked) _kieuin = VNS.Properties.LoaiphieuIn.Cahai;
            if (optChitiet.Checked) _kieuin = VNS.Properties.LoaiphieuIn.Chitiet;
            if (optTonghop.Checked) _kieuin = VNS.Properties.LoaiphieuIn.Tonghop;
            this.Close();
        }

        private void frm_ChonTrangthai_Load(object sender, EventArgs e)
        {

        }

       

        private void frm_ChonTrangthai_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if(e.Control&&(e.KeyCode==Keys.A || e.KeyCode==Keys.S))cmdAccept.PerformClick();
        }

       
    }
}
