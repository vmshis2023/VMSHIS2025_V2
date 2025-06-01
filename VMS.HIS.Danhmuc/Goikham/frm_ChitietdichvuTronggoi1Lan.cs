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
    public partial class frm_ChitietdichvuTronggoi1Lan : Form
    {
        private DataTable dtData=new DataTable();
        public delegate void OnSelectionChanged(DataRow dr);
        public event OnSelectionChanged _OnSelectionChanged;
        public delegate void OnAccept(DataTable dtChitietdichvutronggoi);
        public event OnAccept _OnAccept;
        DataTable dtChidinhCLS = new DataTable();
        public frm_ChitietdichvuTronggoi1Lan(int id_goi,DataTable dtChidinhCLS)
        {
            InitializeComponent();
            this.dtChidinhCLS = dtChidinhCLS;
            this.id_goi = id_goi;
            Utility.SetVisualStyle(this);
            this.KeyPreview = true;
            InitEvents();
        }
        public frm_ChitietdichvuTronggoi1Lan(DataTable dtData)
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
            this.Load+=frm_ChitietdichvuTronggoi1Lan_Load;
            grdChiTietGoiKham.SelectionChanged += grdChiTietGoiKham_SelectionChanged;
        }

        void grdChiTietGoiKham_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdChiTietGoiKham)) return;
            if (_OnSelectionChanged != null) _OnSelectionChanged(((DataRowView) grdChiTietGoiKham.CurrentRow.DataRow).Row);
        }

        void cmdSave_Click(object sender, EventArgs e)
        {
            DataTable dtResult = m_dtChitietgoi.Clone();
            if (m_dtChitietgoi.Select("CHON=true").Length > 0)
                dtResult = m_dtChitietgoi.Select("CHON=true").CopyToDataTable();
            if(dtResult.Rows.Count<=0)
            {
                Utility.ShowMsg("Bạn chưa chọn dịch vụ nào trong gói để thực hiện chỉ định CLS");
                return;
            }    
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
            if (_OnAccept != null) _OnAccept(dtResult);
        }

        void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        DataTable m_dtChitietgoi = new DataTable();
        int id_goi=-1;
        /// <summary>
        /// hàm thực hiện việc load thông tin của tiếp đón lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_ChitietdichvuTronggoi1Lan_Load(object sender, EventArgs e)
        {
            try
            {
                GoiDanhsach objCTKM = GoiDanhsach.FetchByID(id_goi);
                if (objCTKM != null)
                {
                    txtID.Text = objCTKM.IdGoi.ToString();
                    txtMaGoi.Text = objCTKM.MaGoi;
                    txtTenGoi.Text = objCTKM.TenGoi;
                    dtpHieuLucDenNgay.Value = objCTKM.HieulucDenngay;
                    dtpHieuLucTuNgay.Value = objCTKM.HieulucTungay;
                    txtTienGoi.Text = objCTKM.SoTien.ToString();
                    cboKieuCK.Text = objCTKM.KieuChietkhau;
                    chkKieuKhuyenmai.Checked = Utility.Byte2Bool(objCTKM.KhuyenmaiTatcadvu);
                     m_dtChitietgoi = new clsGoikham().LayChiTietGoiKhamTheoIdGoi(id_goi);
                    if (!m_dtChitietgoi.Columns.Contains("ton_tai")) m_dtChitietgoi.Columns.Add(new DataColumn("ton_tai", typeof(byte)));
                    if (!m_dtChitietgoi.Columns.Contains("CHON")) m_dtChitietgoi.Columns.Add(new DataColumn("CHON", typeof(bool)));
                    foreach (DataRow dr in m_dtChitietgoi.Rows)
                    {
                        dr["CHON"] = true;
                        if (dtChidinhCLS.Select("id_chitietdichvu=" + dr["id_chitietdichvu"]).Length>0)
                            dr["ton_tai"] = (byte)1;
                        else
                            dr["ton_tai"] = (byte)0;
                    }    
                    grdChiTietGoiKham.DataSource = m_dtChitietgoi;
                    grdChiTietGoiKham.CheckAllRecords();
                }
            }
            catch (Exception ex) 
            {
                Utility.CatchException(ex);
            }
        }

        private void chkCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            (from p in m_dtChitietgoi.AsEnumerable()  select p).ToList().ForEach(x => x["CHON"] = chkCheckAll.Checked);
        }


        private void mnuCheckAll_Click(object sender, EventArgs e)
        {
            (from p in m_dtChitietgoi.AsEnumerable() select p).ToList().ForEach(x => x["CHON"] = true);
        }

        private void mnuUncheckAll_Click(object sender, EventArgs e)
        {
            (from p in m_dtChitietgoi.AsEnumerable() select p).ToList().ForEach(x => x["CHON"] = false);
        }

        private void mnuBochonDvuDachidinh_Click(object sender, EventArgs e)
        {
            (from p in m_dtChitietgoi.AsEnumerable() where Utility.ByteDbnull( p["ton_tai"]) == 1 select p).ToList().ForEach(x => x["CHON"] = false);
        }
    }
}
