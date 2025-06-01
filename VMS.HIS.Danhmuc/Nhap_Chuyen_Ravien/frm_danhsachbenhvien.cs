using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
namespace VNS.HIS.UI.Forms.NGOAITRU
{
    public partial class frm_danhsachbenhvien : Form
    {
        public string mabenhvien = "";
        public int idBenhvien = -1;
        public bool mv_blnCancel=true;
        public frm_danhsachbenhvien()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.KeyDown += new KeyEventHandler(frm_danhsachbenhvien_KeyDown);
            grd_List.KeyDown += new KeyEventHandler(grd_List_KeyDown);
            grd_List.MouseDoubleClick += new MouseEventHandler(grd_List_MouseDoubleClick);
            this.Load += new EventHandler(frm_danhsachbenhvien_Load);
        }

        void grd_List_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            grd_List_KeyDown(grd_List, new KeyEventArgs(Keys.Enter));
        }

        void frm_danhsachbenhvien_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = SPs.DmucLaydanhsachbenhvien("NULL", "NULL").GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grd_List,dt,true,true,"1=1",DmucBenhvien.Columns.SttHthi);
                Utility.focusCell(grd_List, DmucBenhvien.Columns.TenBenhvien);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        void grd_List_KeyDown(object sender, KeyEventArgs e)
        {
            if(!Utility.isValidGrid(grd_List)) return;
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    idBenhvien = Utility.Int32Dbnull(grd_List.GetValue(DmucBenhvien.Columns.IdBenhvien),-1);
                    mabenhvien = Utility.sDbnull(grd_List.GetValue(DmucBenhvien.Columns.MaBenhvien), "-1");
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    mv_blnCancel = false;
                }
            }
            catch (Exception ex)
            {
             
            }
        }

        void frm_danhsachbenhvien_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
    }
}
