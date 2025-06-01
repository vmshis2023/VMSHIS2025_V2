using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using VNS.HIS.UI.DANHMUC;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_ChandoanICD : Form
    {
        public KcbLuotkham objLuotkham;
        public int id_kham;
        public short id_bskham;
        public bool b_Cancel = false;
        public bool CallfromParent = false;
        public string tenkhoadieutri = "";
        public frm_ChandoanICD()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KeyDown += new KeyEventHandler(frm_ChandoanICD_KeyDown);
            ucThongtinnguoibenh1._OnEnterMe += ucThongtinnguoibenh1__OnEnterMe;
        }

        void ucThongtinnguoibenh1__OnEnterMe()
        {
            ucChandoanICD1.objBenhnhan = ucThongtinnguoibenh1.objBenhnhan;
            ucChandoanICD1.ChangePatients(ucThongtinnguoibenh1.objLuotkham, ucThongtinnguoibenh1.txtKhoanoitru.Text);
        }
       

        void frm_ChandoanICD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.Control && e.KeyCode == Keys.S) ucChandoanICD1.cmdGhi.PerformClick();
            if (e.Control && e.KeyCode == Keys.D) ucChandoanICD1.cmdxoa.PerformClick();
        }
       
        private void frm_ChandoanICD_Load(object sender, EventArgs e)
        {
            ucThongtinnguoibenh1.noitrungoaitru = 1;
            ucChandoanICD1.AutoCompleteTextBox();
            ucThongtinnguoibenh1.txtMaluotkham.Focus();
            if(CallfromParent && objLuotkham!=null)
            {
                ucThongtinnguoibenh1.txtMaluotkham.Text = objLuotkham.MaLuotkham;
                ucThongtinnguoibenh1.Refresh();
            }

        }
      
    }
}
