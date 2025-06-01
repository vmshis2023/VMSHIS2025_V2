using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Janus.Windows.GridEX;
using VMS.HIS.DAL;
using VNS.Libs;
namespace VNS.HIS.UI.Forms.Cauhinh
{
    public partial class frm_CHON_BENHNHAN : Form
    {
        public DataTable temdt;
        public bool mv_bCancel = false;
        public string Patient_ID = "0";
        public frm_CHON_BENHNHAN()
        {
            InitializeComponent();
            this.Load += new EventHandler(frm_CHON_BENHNHAN_Load);
            this.KeyDown += new KeyEventHandler(frm_CHON_BENHNHAN_KeyDown);
            grdPatientList.DoubleClick += new EventHandler(grdPatientList_DoubleClick);
        }

        void grdPatientList_DoubleClick(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdPatientList)) return;
            if (grdPatientList.CurrentRow != null && grdPatientList.CurrentRow.RowType == RowType.Record)
            {
                DataRow dr = ((DataRowView)grdPatientList.CurrentRow.DataRow).Row;
                Patient_ID = dr[KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString();
                mv_bCancel = false;
                this.Close();
            }
            else
            {
                Utility.ShowMsg("Bạn cần chọn một Bệnh nhân trên lưới dữ liệu trước khi nhấn Enter");
            }
        }

        void frm_CHON_BENHNHAN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (grdPatientList.RowCount <= 0) return;
            if (e.KeyCode == Keys.Enter)
            {
                if (!Utility.isValidGrid(grdPatientList)) return;
                if (grdPatientList.CurrentRow != null && grdPatientList.CurrentRow.RowType == RowType.Record)
                {
                    DataRow dr = ((DataRowView)grdPatientList.CurrentRow.DataRow).Row;
                    Patient_ID = dr[KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString();
                    mv_bCancel = true;
                    this.Close();
                }
                else
                {
                    Utility.ShowMsg("Bạn cần chọn một Bệnh nhân trên lưới dữ liệu trước khi nhấn Enter");
                }
            }
        }

        void frm_CHON_BENHNHAN_Load(object sender, EventArgs e)
        {
            Utility.SetDataSourceForDataGridEx(grdPatientList, temdt, true, true, "", "");
        }
    }
}
