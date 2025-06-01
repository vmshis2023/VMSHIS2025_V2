using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;

using SubSonic;
using SortOrder = Janus.Windows.GridEX.SortOrder;
using VNS.Properties;
using VMS.QMS.Class;
using VMS.QMS.DAL;

namespace VMS.QMS
{
    public partial class frm_SoKham_GoiLai : Form
    {
        QMSChucNang _qms = new QMSChucNang();
        private DataTable m_dtData=new DataTable();
        public delegate void OnActiveQMS();
        public event OnActiveQMS _OnActiveQMS;
        public frm_SoKham_GoiLai()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            cmdReset.Click+=cmdReset_Click;
            grdListGoiLaiSoKham.DoubleClick += grdListGoiLaiSoKham_DoubleClick;
            grdListGoiLaiSoKham.ColumnHeaderClick += grdListGoiLaiSoKham_ColumnHeaderClick;
            grdListGoiLaiSoKham.RowCheckStateChanged += grdListGoiLaiSoKham_RowCheckStateChanged;
            cmdReset4me.Click += cmdReset4me_Click;
        }

        void grdListGoiLaiSoKham_RowCheckStateChanged(object sender, Janus.Windows.GridEX.RowCheckStateChangeEventArgs e)
        {
            ModifyCommands();
        }

        void grdListGoiLaiSoKham_ColumnHeaderClick(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            ModifyCommands();
        }

        void cmdReset4me_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdListGoiLaiSoKham))
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất 1 dòng QMS cần khôi phục để dùng cho máy của bạn");
                    grdListGoiLaiSoKham.MoveFirst();
                    return;
                }
                int id = Utility.Int32Dbnull(grdListGoiLaiSoKham.CurrentRow.Cells[QmsTiepdon.Columns.Id].Value);
                _qms.ResetQMS(id, PropertyLib._HISQMSProperties.MaQuay, 0);
               
                DataRow[] arrdr = m_dtData.Select(QmsTiepdon.Columns.Id + "=" + id.ToString());
                if (arrdr.Length > 0)
                {
                    arrdr[0][QmsTiepdon.Columns.TrangThai] = 1;
                }

                m_dtData.AcceptChanges();
                if (_OnActiveQMS != null) _OnActiveQMS();
                ModifyCommands();
            }
            catch (Exception)
            {
            }
        }

        void grdListGoiLaiSoKham_DoubleClick(object sender, EventArgs e)
        {
            
        }
        private void ModifyCommands()
        {
            cmdReset.Enabled = grdListGoiLaiSoKham.GetCheckedRows().Length>0;
        }
        private void cmdReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdListGoiLaiSoKham.GetCheckedRows().Length <= 0 && Utility.isValidGrid(grdListGoiLaiSoKham))
                {
                    grdListGoiLaiSoKham.CurrentRow.BeginEdit();
                    grdListGoiLaiSoKham.CurrentRow.IsChecked = true;
                    grdListGoiLaiSoKham.CurrentRow.EndEdit();
                }
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdListGoiLaiSoKham.GetCheckedRows())
                {
                    int id = Utility.Int32Dbnull(gridExRow.Cells[QmsTiepdon.Columns.Id].Value);
                   _qms.ResetQMS(id,"",0);
                    DataRow[] arrdr = m_dtData.Select(QmsTiepdon.Columns.Id + "=" + id.ToString());
                    if (arrdr.Length > 0)
                    {
                        arrdr[0][QmsTiepdon.Columns.TrangThai] = 0;
                    }
                }
                if (_OnActiveQMS != null) _OnActiveQMS();
                m_dtData.AcceptChanges();
                ModifyCommands();
            }
            catch (Exception)
            {
            }
        }

        private void frm_SoKham_GoiLai_Load(object sender, EventArgs e)
        {
            GetData();
            ModifyCommands();
        }

        private void GetData()
        {
            m_dtData = _qms.VmsQmsLaysoQMSGoilai("-1", globalVariables.MA_KHOA_THIEN, PropertyLib._HISQMSProperties.LoaiQMS.ToString());
            Utility.SetDataSourceForDataGridEx(grdListGoiLaiSoKham,m_dtData,false,true,"trang_thai>=3","So_qms asc");
        }

        private void cmdReset4me_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._QMSPrintProperties);
            frm.TopMost = true;
            frm.ShowDialog();
        }

        
    }
}
