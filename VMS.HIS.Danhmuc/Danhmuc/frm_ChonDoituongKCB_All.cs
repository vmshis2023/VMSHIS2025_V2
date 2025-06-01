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
    public partial class frm_ChonDoituongKCB_All : Form
    {
        public DataTable m_dtQheDoituong_CLS = null;
        public DataTable m_dtQheDoituong_Thuoc = null;
        public decimal Original_Price = 0;
        public int DetailService = -1;
        public string MaGoiDV = "";
        public bool m_blnCancel = true;
        public enObjectType _enObjectType = enObjectType.DichvuCLS;
        public string MaKhoaTHIEN = "";
        public Int16 id_doituongkcb_thamchieu = -1;
        public frm_ChonDoituongKCB_All()
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

        private void frm_ChonDoituongKCB_All_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)cmdClose.PerformClick();
            if(e.Control && e.KeyCode==Keys.S)cmdAccept.PerformClick();
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            if (m_dtQheDoituong_CLS != null)
                TaodulieuQheDoituong_CLS();
            else
                TaodulieuQheDoituong_Thuoc();
            
        }
        private void TaodulieuQheDoituong_Thuoc()
        {
            try
            {
                id_doituongkcb_thamchieu = -1;
                m_dtQheDoituong_Thuoc.Rows.Clear();
                if (grdObjectTypeList.GetCheckedRows().Length <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất 1 đối tượng khám chữa bệnh");
                    return;
                }
                if (txtDoituongThamchieu.MyID == "-1")
                {
                    Utility.ShowMsg("Bạn cần chọn đối tượng tham chiếu giá");
                    return;
                }
                id_doituongkcb_thamchieu = Utility.Int16Dbnull(txtDoituongThamchieu.MyID);
                string cacdoituongchon = string.Join(", ", (from p in grdObjectTypeList.GetCheckedRows()
                                                            select Utility.sDbnull(p.Cells[DmucDoituongkcb.Columns.TenDoituongKcb].Value, "")).ToArray<string>());
                string question = string.Format("Bạn có chắc chắn muốn tạo giá Thuốc-VTTH cho các đối tượng {0} dựa vào giá của đối tượng {1} hay không?\nNhấn Yes để thực hiện.\nNhấn No để hủy thao tác", cacdoituongchon, txtDoituongThamchieu.Text);
                if (!Utility.AcceptQuestion(question, "Xác nhận", true)) return;
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdObjectTypeList.GetCheckedRows())
                {
                    DataRow newDr = m_dtQheDoituong_Thuoc.NewRow();
                    newDr[QheDoituongThuoc.Columns.MaDoituongKcb] = Utility.sDbnull(gridExRow.Cells[DmucDoituongkcb.Columns.MaDoituongKcb].Value, "");
                    newDr[QheDoituongThuoc.Columns.IdDoituongKcb] = Utility.sDbnull(gridExRow.Cells[DmucDoituongkcb.Columns.IdDoituongKcb].Value, "");
                    newDr[VQheDoituongThuoc.Columns.TenDoituongKcb] = Utility.sDbnull(gridExRow.Cells[DmucDoituongkcb.Columns.TenDoituongKcb].Value, "");
                    newDr[QheDoituongThuoc.Columns.PhuthuDungtuyen] = 0;
                    newDr[QheDoituongThuoc.Columns.PhuthuTraituyen] = 0;
                    switch (_enObjectType)
                    {
                        case enObjectType.DichvuCLS:
                            newDr[QheDoituongDichvucl.Columns.IdChitietdichvu] = DetailService;
                            break;
                        case enObjectType.Thuoc:
                            newDr[QheDoituongThuoc.Columns.IdThuoc] = DetailService;
                            break;
                        default:
                            break;
                    }
                    newDr[QheDoituongThuoc.Columns.TyleTt] = 100;
                    newDr[QheDoituongDichvucl.Columns.IdLoaidoituongKcb] = Utility.Int32Dbnull(gridExRow.Cells[DmucDoituongkcb.Columns.IdLoaidoituongKcb].Value, -1);
                    newDr[QheDoituongThuoc.Columns.DonGia] = Original_Price;
                    newDr[QheDoituongThuoc.Columns.MaKhoaThuchien] = MaKhoaTHIEN;
                    m_dtQheDoituong_Thuoc.Rows.Add(newDr);
                    m_blnCancel = false;
                }
                m_dtQheDoituong_Thuoc.AcceptChanges();
                this.Close();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void TaodulieuQheDoituong_CLS()
        {
            try
            {
                id_doituongkcb_thamchieu = -1;
                m_dtQheDoituong_CLS.Rows.Clear();
                if (grdObjectTypeList.GetCheckedRows().Length <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất 1 đối tượng khám chữa bệnh");
                    return;
                }
                if (txtDoituongThamchieu.MyID == "-1")
                {
                    Utility.ShowMsg("Bạn cần chọn đối tượng tham chiếu giá");
                    return;
                }
                id_doituongkcb_thamchieu = Utility.Int16Dbnull(txtDoituongThamchieu.MyID);
                string cacdoituongchon = string.Join(", ", (from p in grdObjectTypeList.GetCheckedRows()
                                                           select Utility.sDbnull(p.Cells[DmucDoituongkcb.Columns.TenDoituongKcb].Value, "")).ToArray<string>());
                string question = string.Format("Bạn có chắc chắn muốn tạo giá dịch vụ Cận lâm sàng cho các đối tượng {0} dựa vào giá CLS của đối tượng {1} hay không?\nNhấn Yes để thực hiện.\nNhấn No để hủy thao tác", cacdoituongchon, txtDoituongThamchieu.Text);
                if (!Utility.AcceptQuestion(question,"Xác nhận",true)) return;
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdObjectTypeList.GetCheckedRows())
                {
                    DataRow newDr = m_dtQheDoituong_CLS.NewRow();
                    newDr[DmucDoituongkcb.Columns.MaDoituongKcb] = Utility.sDbnull(gridExRow.Cells[DmucDoituongkcb.Columns.MaDoituongKcb].Value, "");
                    newDr[DmucDoituongkcb.Columns.IdDoituongKcb] = Utility.sDbnull(gridExRow.Cells[DmucDoituongkcb.Columns.IdDoituongKcb].Value, "");
                    newDr[DmucDoituongkcb.Columns.TenDoituongKcb] = Utility.sDbnull(gridExRow.Cells[DmucDoituongkcb.Columns.TenDoituongKcb].Value, "");
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
                        default:
                            break;
                    }
                    newDr[QheDoituongDichvucl.Columns.TyleTt] = 100;
                    newDr[QheDoituongDichvucl.Columns.IdLoaidoituongKcb] = Utility.Int32Dbnull(gridExRow.Cells[DmucDoituongkcb.Columns.IdLoaidoituongKcb].Value, -1);
                    newDr[QheDoituongDichvucl.Columns.DonGia] = Original_Price;
                    newDr[QheDoituongDichvucl.Columns.MaKhoaThuchien] = MaKhoaTHIEN;
                    m_dtQheDoituong_CLS.Rows.Add(newDr);
                    m_blnCancel = false;
                } 
                m_dtQheDoituong_CLS.AcceptChanges();
                this.Close();
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
       
        private DataTable m_dtObjectType=new DataTable();
       
        private void LoadData()
        {
            try
            {

                m_dtObjectType = new Select().From(DmucDoituongkcb.Schema).OrderAsc(DmucDoituongkcb.Columns.SttHthi).ExecuteDataSet().Tables[0];
                grdObjectTypeList.DataSource = m_dtObjectType;
                txtDoituongThamchieu.Init(m_dtObjectType, new List<string>() { DmucDoituongkcb.Columns.IdDoituongKcb, DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb });
                //foreach (DataRowView drv in m_dtQheDoituong_CLS.DefaultView)
                //{
                //    DataRow[] arrDr = m_dtObjectType.Select(DmucDoituongkcb.Columns.MaDoituongKcb + "='" + Utility.sDbnull(drv[QheDoituongDichvucl.Columns.MaDoituongKcb], "-1") + "'");
                //    if (arrDr.GetLength(0) > 0)
                //    {
                //        m_dtObjectType.Rows.Remove(arrDr[0]);
                //        m_dtObjectType.AcceptChanges();
                //    }
                //}
                //grdObjectTypeList.CheckAllRecords();

            }
            catch
            {
            }
            
        }
      
        private void frm_ChonDoituongKCB_All_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void chkCheckAllorNone_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}
