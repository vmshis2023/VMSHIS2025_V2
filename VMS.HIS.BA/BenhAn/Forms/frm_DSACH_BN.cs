using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VNS.Libs;
using VNS.HIS.NGHIEPVU;
using VMS.HIS.DAL;
using SubSonic;
namespace VNS.HIS.UI.BENH_AN
{
    public partial class frm_DSACH_BN : Form
    {
        public string IdBenhnhan = "";
        public string MaLuotkham = "";
        public bool has_Cancel = true;
        public int DepartmentId = -1;
        public int Lan_Vao_Vien_Thu;
        public frm_DSACH_BN()
        {            
            InitializeComponent();
            Utility.SetVisualStyle(this);
        }
        public DataTable dtPatient;
        private void frm_DSACH_BN_Load(object sender, EventArgs e)
        {
            try
            {
               // dtPatient = SPs.NoitietTimkiemBenhnhan(MaLuotkham, DepartmentId).GetDataSet().Tables[0];              
                grdPatient.DataSource = dtPatient;                
                grdPatient.ColumnAutoSizeMode = ColumnAutoSizeMode.AllCellsAndHeader;

            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình load thông tin bệnh nhân");                
            }
        }

        private void grdPatient_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyCode == Keys.Enter)
                {
                    MaLuotkham = Utility.sDbnull(grdPatient.CurrentRow.Cells[KcbLuotkham.Columns.MaLuotkham].Value, "");
                    IdBenhnhan = Utility.sDbnull(grdPatient.CurrentRow.Cells[KcbLuotkham.Columns.IdBenhnhan].Value, "");
                    Lan_Vao_Vien_Thu = grdPatient.CurrentRow.RowIndex +1 ;
                    has_Cancel = false;
                    Close();
                }else if (e.KeyCode == Keys.Escape)
                {
                    Close();
                }
           
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình chọn bệnh nhân");
                
            }
        }

        private void grdPatient_DoubleClick(object sender, EventArgs e)
        {

            try
            {
                    MaLuotkham = Utility.sDbnull(grdPatient.CurrentRow.Cells[KcbLuotkham.Columns.MaLuotkham].Value, "");
                    IdBenhnhan = Utility.sDbnull(grdPatient.CurrentRow.Cells[KcbLuotkham.Columns.IdBenhnhan].Value, "");
                    Lan_Vao_Vien_Thu = grdPatient.CurrentRow.RowIndex +1 ;
                    has_Cancel = false;
                    Close();
               
              

            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình chọn bệnh nhân");

            }
        }
    }
}
