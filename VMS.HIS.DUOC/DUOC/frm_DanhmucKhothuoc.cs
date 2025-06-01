using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
namespace VNS.HIS.UI.THUOC
{
    public partial class frm_DanhmucKhothuoc : Form
    {
        private DataTable m_dtKhoThuoc=new DataTable();
        public string KIEU_THUOC_VT = "THUOC";
        public bool AutoNew = false;
        public bool m_blnCancel = true;
        public frm_DanhmucKhothuoc(string KIEU_THUOC_VT)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            grdKhoThuoc.SelectionChanged += grdKhoThuoc_SelectionChanged;
            this.Shown += frm_DanhmucKhothuoc_Shown;
            this.KIEU_THUOC_VT = KIEU_THUOC_VT;
            
        }
        DataTable m_dtdutru = new DataTable();
        void grdKhoThuoc_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdKhoThuoc))
                {
                    if (m_dtdutru != null)
                    {
                        m_dtdutru.Clear();
                        m_dtdutru.AcceptChanges();
                    }
                    return;
                }
                else
                {
                    m_dtdutru = SPs.ThuocLaythongtinDutruthuoc(-1, grdKhoThuoc.GetValue("kho_thuoc_vt").ToString(), Utility.Int16Dbnull(grdKhoThuoc.GetValue("id_kho")),-1).GetDataSet().Tables[0];
                    Utility.SetDataSourceForDataGridEx_Basic(grdDutru, m_dtdutru, true, true, "1=1", "ten_thuoc");
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        void frm_DanhmucKhothuoc_Shown(object sender, EventArgs e)
        {
            if (AutoNew)
                cmdThemMoi_Click(cmdThemMoi, e);
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// kiểm tra có kho nào được chọn để xóa chưa
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            if (grdKhoThuoc.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một thông tin để thực hiện xóa", "Thông báo", MessageBoxIcon.Warning);
                grdKhoThuoc.Focus();
                return false;
            }
            bool bExist = false;

            return true;
        }
        /// <summary>
        /// Hàm thực hiện xóa kho
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoa_Click(object sender, EventArgs e)
        {
             try
            {
                if (!IsValidData()) return;
                List<string> lstNodelete = new List<string>();
                if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin của kho thuốc vật tư đang chọn không", "Thông báo",true))
                {
                    foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoThuoc.GetCheckedRows())
                    {
                        DataTable dtCheck = SPs.ThuocKiemtrakhoxoa(Utility.Int32Dbnull(gridExRow.Cells["ID_KHO"].Value, -1)).GetDataSet().Tables[0];
                        if (dtCheck.Rows.Count > 0)
                        {
                            lstNodelete.Add(Utility.sDbnull(gridExRow.Cells["ten_kho"].Value, ""));
                            continue;
                        }
                        new Delete().From(QheDoituongKho.Schema)
                            .Where(QheDoituongKho.Columns.IdKho).IsEqualTo(Utility.Int32Dbnull(gridExRow.Cells["ID_KHO"].Value, -1))
                            .Execute();
                        if (new Delete().From(TDmucKho.Schema).Where(TDmucKho.Columns.IdKho).IsEqualTo(Utility.Int32Dbnull(gridExRow.Cells[TDmucKho.Columns.IdKho].Value, -1)).Execute() > 0)
                        {
                            m_blnCancel = false;
                            gridExRow.Delete();
                        }
                        
                    }
                  
                    grdKhoThuoc.UpdateData();
                    grdKhoThuoc.Refresh();
                   
                    m_dtKhoThuoc.AcceptChanges();
                    if (lstNodelete.Count > 0)
                    {
                        Utility.ShowMsg(string.Format("Các kho dưới dây đã được sử dụng nên không thể xóa\n{0}", string.Join(",", lstNodelete.ToArray<string>())));
                    }
                  //  Utility.ShowMsg("Bạn xóa thông tin thành công", "thông báo");
                    ModifyCommand();
                  
                }

            }
            catch (Exception exception)
            {
               
            }
        }
        void ModifyCommand()
        {
            try
            {
                cmdSua.Enabled = cmdXoa.Enabled = grdKhoThuoc.RowCount > 0 && grdKhoThuoc.CurrentRow.RowType == RowType.Record;
            }
            catch (Exception)
            {

                //throw;
            }
        }
        /// <summary>
        /// hàm thực hiện việc sửa thông tin kho thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSua_Click(object sender, EventArgs e)
        {
            frm_themmoi_kho frm = new frm_themmoi_kho();
            frm.txtIDKHO.Text = Utility.sDbnull(grdKhoThuoc.GetValue(TDmucKho.Columns.IdKho));
            frm.em_Action = action.Update;
            frm.p_dtDataChung = m_dtKhoThuoc;
            frm.grdList = grdKhoThuoc;
            frm.ShowDialog();
            m_blnCancel = frm.m_blnCancel;
        }
        /// <summary>
        /// hàm thực hiện load thông tin kho thuốc
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_DanhmucKhothuoc_Load(object sender, EventArgs e)
        {

            m_dtKhoThuoc =CommonLoadDuoc.LAYDANHMUCKHO(-1,"ALL","ALL","ALL",100,100,1);
            Utility.SetDataSourceForDataGridEx(grdKhoThuoc,m_dtKhoThuoc,true,true,"1=1","");
            ModifyCommand();
           
        }
        /// <summary>
        /// hàm thực hiện việc thêm mới kho thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdThemMoi_Click(object sender, EventArgs e)
        {
            frm_themmoi_kho frm=new frm_themmoi_kho();
            frm.txtIDKHO.Text = "-1";
            frm.em_Action = action.Insert;
            frm.p_dtDataChung = m_dtKhoThuoc;
            frm.grdList = grdKhoThuoc;
            frm.ShowDialog();
            m_blnCancel = frm.m_blnCancel;
        }
        /// <summary>
        /// phím tắt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void frm_DanhmucKhothuoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N) cmdThemMoi.PerformClick();
            if (e.Control && e.KeyCode == Keys.E) cmdSua.PerformClick();
            if (e.Control && e.KeyCode == Keys.D) cmdXoa.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdThoat.PerformClick();
            if (e.KeyCode == Keys.F5)
            {
                m_dtKhoThuoc = CommonLoadDuoc.LAYDANHMUCKHO(-1,"ALL", "ALL", "ALL",100,100, 1);
                Utility.SetDataSourceForDataGridEx(grdKhoThuoc, m_dtKhoThuoc, true, true, "1=1", "");
                ModifyCommand();
            }
           
           
        }

        private void grdKhoThuoc_DoubleClick(object sender, EventArgs e)
        {
            cmdSua.PerformClick();
        }
    }
}
