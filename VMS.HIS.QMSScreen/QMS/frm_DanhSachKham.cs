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


using SortOrder = Janus.Windows.GridEX.SortOrder;
using VNS.Properties;
using VMS.QMS.Class;

namespace VMS.QMS
{
    public partial class frm_DanhSachKham : Form
    {
        QMSChucNang _qms = new QMSChucNang();
        public frm_DanhSachKham()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            timer1.Tick += new System.EventHandler(OnTimerEvent);
            timer1.Tick += timer1_Tick;
            //timer2.Tick += new System.EventHandler(OnTimerEvent);
            CauHinh();
        }
        public void OnTimerEvent(object source, EventArgs e)
        {
            LoadDanhSach();
           
        }
        //public void OnTimerEvent2(object source, EventArgs e)
        //{
        //    string bacsykham = string.Format("{0}", Utility.sDbnull(globalVariables.gv_strTenNhanvien, ""));
        //    lblBacSykham.Text = bacsykham;
        //}
        private void CauHinh()
        {
            
            if (PropertyLib._HISQMSProperties!=null)
            {

                if (PropertyLib._HISQMSProperties.ThoiGianTuDongLay <= 0) PropertyLib._HISQMSProperties.ThoiGianTuDongLay = 1000;

                timer1.Interval = PropertyLib._HISQMSProperties.ThoiGianTuDongLay;
                timer1.Enabled = true;
                timer1.Start();
                
            }
            else
            {
                timer1.Enabled = true;
                timer1.Start();
                timer1.Interval =1000;
            }
           
        }
        private DataTable m_dtDanhSachChoKham=new DataTable();
        private void LoadDanhSach()
        {
            try
            {
                m_dtDanhSachChoKham = null;
                  
                Utility.SetDataSourceForDataGridEx(grdList, m_dtDanhSachChoKham, false, true, "1=1", "");
                Janus.Windows.GridEX.GridEXColumn gridExColumn = grdList.RootTable.Columns["STT"];
                Utility.SetGridEXSortKey(grdList, gridExColumn,SortOrder.Ascending);
                if(PropertyLib._HISQMSProperties.HienThiBacSyKham)
                {
                    splitContainer1.Panel2Collapsed = false;
                    string bacsykham = string.Format("{0}", Utility.sDbnull(PropertyLib._HISQMSProperties.TenBacSyHienThi, globalVariables.gv_strTenNhanvien));
                    lblBacSykham.Text = bacsykham;
                }
                else
                {
                    splitContainer1.Panel2Collapsed = true;
                }
                
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
                
            }
           
        }
        private void frm_DanhSachKham_Load(object sender, EventArgs e)
        {
           // LoadDanhSach();
            timer2.Start();
        }
        /// <summary>
        /// hàm thực hiện viec
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Properties frm = new frm_Properties( PropertyLib._HISQMSProperties);
                frm.ShowDialog();
                CauHinh();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
          
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
        }
        public int i = 10;
        private void timer2_Tick(object sender, EventArgs e)
        {
            lblBacSykham.Left += i;
            if (lblBacSykham.Left >= this.Width - lblBacSykham.Width || lblBacSykham.Left <= 0)
            { i = -i; }
            timer2.Start();
            timer2.Interval = 500;
        }
       
    }
}
