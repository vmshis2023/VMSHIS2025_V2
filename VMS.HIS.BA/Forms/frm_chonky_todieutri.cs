using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using NLog;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;

using VNS.HIS.UI.NGOAITRU;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;

using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.NOITRU;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.Classes;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.VisualBasic;
using System.IO;

namespace VMS.HIS.UI.EMR
{
    public partial class frm_chonky_todieutri : Form
    {
        private string _rowFilter = "1=1";
        public DataTable m_dtPhieuDieuTriChonIn = new DataTable();
        public KcbLuotkham objLuotkham;
        public DataTable m_dtPhieudieutri = new DataTable();
        bool m_blnLoaded = false;
        public List<long> lstIdphieu = new List<long>();
        string nguoi_ky;
        public frm_chonky_todieutri(KcbLuotkham objLuotkham,string nguoi_ky)
        {
            InitializeComponent();
            this.nguoi_ky = nguoi_ky;
            this.objLuotkham = objLuotkham;


        }
       
        void LoadData()
        {
            m_dtPhieudieutri = new KCB_THAMKHAM().NoitruTimkiemphieudieutriTheoluotkham(1, "01/01/1900", objLuotkham.MaLuotkham,
                    (int)objLuotkham.IdBenhnhan, "-1", 0);
            _rowFilter = "1=1";
            DataTable dtDaky = m_dtPhieudieutri.Clone();
            DataTable dtChuaky = m_dtPhieudieutri.Clone();
            DataRow[] arrDr = m_dtPhieudieutri.Select(string.Format("nguoi_ky = '{0}' and tthai_ky=0",nguoi_ky));
            if (arrDr.Length > 0)
                dtChuaky = arrDr.CopyToDataTable();
            arrDr = m_dtPhieudieutri.Select(string.Format("nguoi_ky = '{0}' and tthai_ky=1", nguoi_ky));
            if (arrDr.Length > 0)
                dtDaky = arrDr.CopyToDataTable();
            Utility.SetDataSourceForDataGridEx_Basic(grdDaky, dtDaky, false, true, _rowFilter, NoitruPhieudieutri.Columns.NgayDieutri + " desc");
            Utility.SetDataSourceForDataGridEx_Basic(grdChuaky, dtChuaky, false, true, _rowFilter, NoitruPhieudieutri.Columns.NgayDieutri + " desc");
        }
       

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// hàm thực hiện việc load thông tin của Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_chonky_todieutri_Load(object sender, EventArgs e)
        {
            LoadData();
            grdChuaky.CheckAllRecords();
            cmdKy.Enabled = grdChuaky.GetDataRows().Length > 0;
        }
       
       

        /// <summaắtry>
        /// hàm thực hiện việc phím t
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_chonky_todieutri_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode==Keys.A) cmdKy.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
        }

        private void cmdKy_Click(object sender, EventArgs e)
        {
            if (grdChuaky.GetCheckedRows().Count() <= 0)
            {
                grdChuaky.CurrentRow.BeginEdit();
                grdChuaky.CurrentRow.IsChecked = true;
                grdChuaky.CurrentRow.EndEdit();
            }
              
            lstIdphieu = grdChuaky.GetCheckedRows().Select(c => Utility.Int64Dbnull(c.Cells["id_phieudieutri"].Value)).ToList<long>();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}