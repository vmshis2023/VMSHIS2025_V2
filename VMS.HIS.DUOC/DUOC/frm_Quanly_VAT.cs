 using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;

using VNS.Libs;
using VNS.HIS.NGHIEPVU;
using VMS.HIS.DAL;
using VNS.HIS.UI.THUOC;
using VNS.HIS.UI.DANHMUC;
using VNS.Properties;
using VNS.HIS.UI.Forms.DUOC;


namespace VNS.HIS.UI.THUOC
{
    public partial class frm_QuanlyThuoc_VAT : Form
    {
        private DataTable m_dtQheDoituongThuoc=new DataTable();
        private DataTable m_dataThuoc=new DataTable();
        action m_enAction = action.Insert;
        string arg = "QHEGIATHUOC-THUOC";
        string Kieuthuoc_vt = "THUOC";
        public bool m_blnCancel = true;
        public bool AutoNew = false;
        public frm_QuanlyThuoc_VAT(string arg)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.arg = arg;
            if (arg.Split('-').Length == 2)
            {
                this.arg = arg.Split('-')[0];
                Kieuthuoc_vt = arg.Split('-')[1];
            }
            InitEvents();
            printPreviewDialog1.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyPreview = true;
        }
      
        void InitEvents()
        {
          
            grdList.UpdatingCell += grdList_UpdatingCell;
            this.KeyDown += Frm_QuanlyThuoc_VAT_KeyDown;
            this.Shown += frm_QuanlyThuoc_VAT_Shown;

        }

        private void Frm_QuanlyThuoc_VAT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                Laydanhmucthuoc();
        }

        void Laydanhmucthuoc()
        {
                m_dataThuoc = SPs.DmucLaydanhsachThuocTheoquyennhanvien(-1, -1, Kieuthuoc_vt, 2, 2).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, m_dataThuoc, true, true, "1=1", VDmucThuoc.Columns.SttHthiLoaithuoc + "," + DmucThuoc.Columns.TenThuoc);
            grdList.MoveFirst();
        }
        void grdList_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            if (e.Column.Key == "VAT_Invoice")
            {
                DmucThuoc objthuoc = DmucThuoc.FetchByID(Utility.Int64Dbnull( grdList.CurrentRow.Cells["id_thuoc"].Value,0));
                if (objthuoc != null)
                {
                    objthuoc.MarkOld();
                    objthuoc.IsNew = false;
                    objthuoc.VatInvoice = Utility.ByteDbnull(e.Value, 0);
                    objthuoc.Save();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Sửa VAT invoice ID thuốc={0}, tên thuốc={1}, Giá cũ={2}, giá mới={3} thành công ", grdList.CurrentRow.Cells["id_thuoc"].Value.ToString(), grdList.CurrentRow.Cells["ten_thuoc"].Value.ToString(), e.InitialValue, e.Value), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                }
            }
           

        }

       

        void frm_QuanlyThuoc_VAT_Shown(object sender, EventArgs e)
        {
            Laydanhmucthuoc();
        }

        private void optQhe_tatca_CheckedChanged(object sender, EventArgs e)
        {
            FilterMe();
        }

        private void optVAT_CheckedChanged(object sender, EventArgs e)
        {
            FilterMe();
        }

        private void optNoVAT_CheckedChanged(object sender, EventArgs e)
        {
            FilterMe();
        }
        void FilterMe()
        {
            string strFilter = "1=1";
            if (optNoVAT.Checked)
                strFilter = "VAT_Invoice <=0 or VAT_Invoice is null";
            else if (optVAT.Checked)
                strFilter = "VAT_Invoice >0 ";
            m_dataThuoc.DefaultView.RowFilter = strFilter;
        }
    }
}
