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
using SubSonic;
namespace VMS.HIS.Duoc.DUOC
{
    public partial class frm_bienban_kiemnhap : Form
    {
        long id_phieu = -1;
        string loai_phieu = "";
        public frm_bienban_kiemnhap(long id_phieu,string loai_phieu)
        {
            InitializeComponent();
            this.id_phieu = id_phieu;
            this.loai_phieu = loai_phieu;
        }

        private void frm_bienban_kiemnhap_Load(object sender, EventArgs e)
        {
            InitData();
        }
void InitData()
        {
            try
            {

            }
            catch (Exception ex)
            {

              
            }
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {

        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {

        }
    }
}
