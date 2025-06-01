using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using NLog;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;


namespace VNS.HIS.UI.NOITRU
{
    
    public partial class frm_TimKiemKhoaPhongNoiTru : Form
    {
        public int idkhoaloaibo = -1;
        public int IdKhoaphong { get; set; }
        public string MaKhoaphong = "";
        public string SearchName { get; set; }
        public bool b_Cancel = false;
        public string _rowFilter = "1=1";
        private DataTable m_dtkhoanoitru=new DataTable();
        public frm_TimKiemKhoaPhongNoiTru()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
           // grdKhoaNoiTru.DoubleClick+=new EventHandler(grdKhoaNoiTru_DoubleClick);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdChapNhan_Click(object sender, EventArgs e)
        {
            AcceptData();
        }
       
        private void AcceptData()
        {
            if (grdKhoaNoiTru.CurrentRow != null)
            {
                IdKhoaphong = Utility.Int32Dbnull(grdKhoaNoiTru.GetValue(DmucKhoaphong.Columns.IdKhoaphong));
                MaKhoaphong = Utility.sDbnull(grdKhoaNoiTru.GetValue(DmucKhoaphong.Columns.MaKhoaphong));
                b_Cancel = true;
                this.Close();
            }
        }

        private void frm_TimKiemKhoaPhongNoiTru_Load(object sender, EventArgs e)
        {
            m_dtkhoanoitru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0, idkhoaloaibo);
            Utility.SetDataSourceForDataGridEx(grdKhoaNoiTru, m_dtkhoanoitru, true, true, _rowFilter, "");
            if (IdKhoaphong > 0)
            {
                Utility.GonewRowJanus(grdKhoaNoiTru, DmucKhoaphong.Columns.IdKhoaphong, IdKhoaphong.ToString());
            }
            txtTimKiem.Text = Utility.sDbnull(SearchName);
        }

        private void grdKhoaNoiTru_DoubleClick(object sender, EventArgs e)
        {
            AcceptData();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string rowFilter = "1=1";
           try
            {
                rowFilter = "ma_khoaphong like '%" + Utility.DoTrim(txtTimKiem.Text) + "%' OR ten_khoaphong like '%" + Utility.DoTrim(txtTimKiem.Text) + "%'";
            }
            catch (Exception exception)
            {
                rowFilter = "1=2";
            }
            finally
            {
                m_dtkhoanoitru.DefaultView.RowFilter = "1=1";
                m_dtkhoanoitru.DefaultView.RowFilter = rowFilter;
                m_dtkhoanoitru.AcceptChanges();
                grdKhoaNoiTru.MoveFirst();
            } 
        }

        private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.PageDown)
            {
                grdKhoaNoiTru.Focus();
            }
        }

        private void grdKhoaNoiTru_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AcceptData(); 
            }
        }
    }
}
