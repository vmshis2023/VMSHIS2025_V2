using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VMS.HIS.DAL;
using AggregateFunction = Janus.Windows.GridEX.AggregateFunction;
using System.IO;
using VNS.Libs;
using VNS.Properties;
using CrystalDecisions.CrystalReports.Engine;
using System.Drawing.Printing;
using VNS.HIS.Classes;
using VNS.HIS.UI.Baocao;
using VNS.HIS.UI.THANHTOAN;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.HIS.UI.DANHMUC;
using Excel;
using VNS.HIS.BusRule.Goikham;
namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_ChitietdichvuTronggoi : Form
    {
        private DataTable dtData=new DataTable();
        public delegate void OnSelectionChanged(DataRow dr);
        public event OnSelectionChanged _OnSelectionChanged;
        public frm_ChitietdichvuTronggoi(int id_goi)
        {
            InitializeComponent();
            this.id_goi = id_goi;
            Utility.SetVisualStyle(this);
            this.KeyPreview = true;
            InitEvents();
        }
        public frm_ChitietdichvuTronggoi(DataTable dtData)
        {
            InitializeComponent();
          
            Utility.SetVisualStyle(this);
            this.dtData = dtData;
            InitEvents();
        }
        void InitEvents()
        {
            cmdCancel.Click += cmdCancel_Click;
            cmdSave.Click += cmdSave_Click;
            this.Load+=frm_ChitietdichvuTronggoi_Load;
            grdChiTietGoiKham.SelectionChanged += grdChiTietGoiKham_SelectionChanged;
        }

        void grdChiTietGoiKham_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdChiTietGoiKham)) return;
            if (_OnSelectionChanged != null) _OnSelectionChanged(((DataRowView) grdChiTietGoiKham.CurrentRow.DataRow).Row);
        }

        void cmdSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        int id_goi=-1;
        /// <summary>
        /// hàm thực hiện việc load thông tin của tiếp đón lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_ChitietdichvuTronggoi_Load(object sender, EventArgs e)
        {
            try
            {
                GoiDanhsach objCTKM = GoiDanhsach.FetchByID(id_goi);
                if (objCTKM != null)
                {
                    txtID.Text = objCTKM.IdGoi.ToString();;
                    txtMaGoi.Text = objCTKM.MaGoi;
                    txtTenGoi.Text = objCTKM.TenGoi;
                    dtpHieuLucDenNgay.Value = objCTKM.HieulucDenngay;
                    dtpHieuLucTuNgay.Value = objCTKM.HieulucTungay;
                    txtTienGoi.Text = objCTKM.SoTien.ToString();
                    cboKieuCK.Text = objCTKM.KieuChietkhau;
                    chkKieuKhuyenmai.Checked = Utility.Byte2Bool(objCTKM.KhuyenmaiTatcadvu);
                    DataTable m_dtChitietgoi = new clsGoikham().LayChiTietGoiKhamTheoIdGoi(id_goi);
                    grdChiTietGoiKham.DataSource = m_dtChitietgoi;
                }
            }
            catch (Exception ex) 
            {
                Utility.CatchException(ex);
            }
        }

        
    }
}
