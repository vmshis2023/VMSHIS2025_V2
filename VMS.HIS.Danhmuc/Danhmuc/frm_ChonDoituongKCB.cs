using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VNS.Libs;
using VNS.HIS.NGHIEPVU;
using VMS.HIS.DAL;
namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_ChonDoituongKCB : Form
    {
        public DataTable m_dtObjectDataSource=new DataTable();
        public decimal Original_Price = 0;
        public int DetailService = -1;
        public string MaGoiDV = "";
        public bool m_blnCancel = true;
        public enObjectType _enObjectType = enObjectType.DichvuCLS;
        public string MaKhoaTHIEN = "";
        public frm_ChonDoituongKCB()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            cmdAccept.Click+=new EventHandler(cmdAccept_Click);
            cmdClose.Click+=new EventHandler(cmdClose_Click);
        }
        /// <summary>
        /// hàm thực hiện đóng Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_ChonDoituongKCB_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)cmdClose.PerformClick();
            if(e.Control && e.KeyCode==Keys.S)cmdAccept.PerformClick();
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {

            AcceptServiceDetail();
                    
            
        }
        private void AcceptServiceDetail()
        {
            try
            {
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdObjectTypeList.GetCheckedRows())
                {
                    DataRow newDr = m_dtObjectDataSource.NewRow();
                    newDr[QheDoituongDichvucl.Columns.MaDoituongKcb] = Utility.sDbnull(gridExRow.Cells[DmucDoituongkcb.Columns.MaDoituongKcb].Value, "");
                    newDr[VQheDoituongDichvucl.Columns.TenDoituongKcb] = Utility.sDbnull(gridExRow.Cells[DmucDoituongkcb.Columns.TenDoituongKcb].Value, "");
                    newDr[QheDoituongDichvucl.Columns.PhuthuDungtuyen] = 0;
                    newDr[QheDoituongDichvucl.Columns.PhuthuTraituyen] = 0;
                    switch (_enObjectType)
                    {
                        case enObjectType.DichvuCLS:
                            newDr[QheDoituongDichvucl.Columns.IdChitietdichvu] = DetailService;
                            break;
                        case enObjectType.Thuoc:
                            newDr[QheDoituongThuoc.Columns.IdThuoc] = DetailService;
                            break;
                        case enObjectType.GoiDichVu:
                            newDr[GoiDanhsach.Columns.IdGoi] = DetailService;
                            break;
                        default:
                            break;
                    }
                    newDr[QheDoituongDichvucl.Columns.TyleTt] = 100;
                    newDr[QheDoituongDichvucl.Columns.IdLoaidoituongKcb] = Utility.Int32Dbnull(gridExRow.Cells[DmucDoituongkcb.Columns.IdLoaidoituongKcb].Value, -1);
                    newDr[QheDoituongDichvucl.Columns.DonGia] = Original_Price;
                    newDr[QheDoituongDichvucl.Columns.MaKhoaThuchien] = MaKhoaTHIEN;
                    m_dtObjectDataSource.Rows.Add(newDr);
                    m_blnCancel = false;
                } 
                m_dtObjectDataSource.AcceptChanges();
                this.Close();
            }
            catch
            {
            }
        }
       
        private DataTable m_dtObjectType=new DataTable();
       
        private void LoadData()
        {
            try
            {

                m_dtObjectType = new Select().From(DmucDoituongkcb.Schema).OrderAsc(DmucDoituongkcb.Columns.SttHthi).ExecuteDataSet().Tables[0];
                grdObjectTypeList.DataSource = m_dtObjectType;
                foreach (DataRowView drv in m_dtObjectDataSource.DefaultView)
                {
                    DataRow[] arrDr = m_dtObjectType.Select(DmucDoituongkcb.Columns.MaDoituongKcb + "='" + Utility.sDbnull(drv[QheDoituongDichvucl.Columns.MaDoituongKcb], "-1") + "'");
                    if (arrDr.GetLength(0) > 0)
                    {
                        m_dtObjectType.Rows.Remove(arrDr[0]);
                        m_dtObjectType.AcceptChanges();
                    }
                }
                grdObjectTypeList.CheckAllRecords();

            }
            catch
            {
            }
            
        }
      
        private void frm_ChonDoituongKCB_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void chkCheckAllorNone_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCheckAllorNone.Checked)
                grdObjectTypeList.CheckAllRecords();
            else
                grdObjectTypeList.UnCheckAllRecords();
        }
    }
}
