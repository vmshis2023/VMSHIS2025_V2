using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;

namespace VMS.HIS.Danhmuc.UserControls
{
    public partial class uc_tamthu : UserControl
    {
        public delegate void OnAction(long id_thanhtoan,string ActType);

        public event OnAction _OnAction;
        DataTable m_dtChiPhiThanhtoan = null;
        DataTable dtTamthu = null;
        public uc_tamthu()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this,false);
            grdPhieutamthu.SelectionChanged += GrdPhieutamthu_SelectionChanged;
            grdPhieutamthu.ColumnButtonClick += GrdPhieutamthu_ColumnButtonClick;
        }

        private void GrdPhieutamthu_ColumnButtonClick(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            long id_thanhtoan = -1;
            string ActType = "IN";
            if (e.Column.Key == "cmdPHIEU_THU")
            {
                ActType = "IN";
            }
            if (e.Column.Key == "cmdHUY_PHIEUTHU")
            {
                ActType = "HUY";
            }
            if (_OnAction != null) _OnAction(id_thanhtoan, ActType);
        }

        private void GrdPhieutamthu_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdPhieutamthu))
                {
                    m_dtChiPhiThanhtoan.DefaultView.RowFilter = "1=1";
                    m_dtChiPhiThanhtoan.AcceptChanges();
                }
                else
                {
                    m_dtChiPhiThanhtoan.DefaultView.RowFilter = "id_tamthu=" + grdPhieutamthu.GetValue("id_thanhtoan");
                    m_dtChiPhiThanhtoan.AcceptChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ShowData(DataTable m_dtChiPhiThanhtoan,DataTable dtTamthu)
        {
            try
            {
                this.m_dtChiPhiThanhtoan = m_dtChiPhiThanhtoan;
                this.dtTamthu = dtTamthu;
                Utility.AddColumToDataTable(ref m_dtChiPhiThanhtoan, "colCHON", typeof(byte));
                Utility.AddColumToDataTable(ref m_dtChiPhiThanhtoan, "ck_nguongt", typeof(byte));
                m_dtChiPhiThanhtoan.AcceptChanges();
                Utility.SetDataSourceForDataGridEx(grdChitietTamthu, m_dtChiPhiThanhtoan, false, true, "trangthai_huy=0", "");

                Utility.SetDataSourceForDataGridEx(grdPhieutamthu, dtTamthu, false, true, "1=1", "");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
