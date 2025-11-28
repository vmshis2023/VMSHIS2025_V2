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

namespace VMS.HIS.EMR.Forms.BA_Phieukham
{
    public partial class frm_danhsach_giaychungsinh_theome : Form
    {
        public long id_giaychungsinh = 0;
        public frm_danhsach_giaychungsinh_theome(KcbLuotkham objLuotkham, DataTable dtData)
        {
            InitializeComponent();
            grdList.MouseDoubleClick += GrdList_MouseDoubleClick;
           
            Utility.SetDataSourceForDataGridEx(grdList, dtData, true, true, "1=1", "id,maso_giaychungsinh");
            cmdAccept.Enabled = Utility.isValidGrid(grdList) && dtData.Rows.Count > 0;
        }

        private void GrdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            cmdAccept.PerformClick();
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            if (Utility.isValidGrid(grdList))
            {
                id_giaychungsinh = Utility.Int64Dbnull(grdList.GetValue("id"));
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
