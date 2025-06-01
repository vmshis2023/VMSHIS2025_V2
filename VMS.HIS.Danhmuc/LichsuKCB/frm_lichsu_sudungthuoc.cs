using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using NLog;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.DAL;
using VNS.HIS.UCs;
using VNS.HIS.UI.Classess;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.Libs;
using VNS.Properties;
using VNS.UCs;
using System.Threading;
using VNS.HIS.UI.HinhAnh;
using VNS.HIS.UI.Forms.HinhAnh;
using Aspose.Words;
using System.Diagnostics;
using VNS.HIS.Classes;
using System.Transactions;
using VNS.HIS.UI.NOITRU;
using VMS.HIS.Danhmuc;

namespace VNS.HIS.UI.NGOAITRU
{
    /// <summary>
    /// 06/11/2013 3h57
    /// </summary>
    public partial class frm_lichsu_sudungthuoc : Form
    {
        
        public frm_lichsu_sudungthuoc()
        {
            InitializeComponent();
           
            InitEvents();

        }
        bool AllowSelectionChanged = false;
        bool AllowSelectionChanged1 = false;
        private void InitEvents()
        {

           
            KeyDown += frm_lichsu_sudungthuoc_KeyDown;
            cmdSearch.Click += CmdSearch_Click;
            grdList.KeyDown += GrdList_KeyDown;
            grdList.DoubleClick += GrdList_DoubleClick;
            grdList.MouseClick += GrdList_MouseClick;
            grdList.SelectionChanged += GrdList_SelectionChanged;
           
            txtMaluotkham.KeyDown += txtMaluotkham_KeyDown;
            txtID.KeyDown += txtID_KeyDown;
            txtID.KeyPress += txtID_KeyPress;
            optNoitru.CheckedChanged += optAll_CheckedChanged;
            optNgoaitru.CheckedChanged += optAll_CheckedChanged;
        }

        private void GrdList_SelectionChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GrdList_MouseClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GrdList_DoubleClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GrdList_KeyDown(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CmdSearch_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void optAll_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    string RowFilter = "1=1";
            //    PropertyLib._ThanhtoanProperties.CachhienthidulieuNoitru = DisplayType.Tatca;
            //    if (optNoitru.Checked)
            //    {
            //        RowFilter = "noitru=1";
            //    }
            //    if (optNgoaitru.Checked)
            //    {
            //        RowFilter = "noitru=0";
            //    }
            //    if (m_dtChidinhCLS != null && m_dtChidinhCLS.Rows.Count > 0 && m_dtChidinhCLS.Columns.Count > 0)
            //    {
            //        m_dtChidinhCLS.DefaultView.RowFilter = RowFilter;
            //        m_dtChidinhCLS.AcceptChanges();
            //    }
            //    if (m_dtDonthuoc != null && m_dtDonthuoc.Rows.Count > 0 && m_dtDonthuoc.Columns.Count > 0)
            //    {
            //        m_dtDonthuoc.DefaultView.RowFilter = RowFilter;
            //        m_dtDonthuoc.AcceptChanges();
            //    }
            //    if (m_dtVTTH != null && m_dtVTTH.Rows.Count > 0 && m_dtVTTH.Columns.Count > 0)
            //    {
            //        m_dtVTTH.DefaultView.RowFilter = RowFilter;
            //        m_dtVTTH.AcceptChanges();
            //    }
            //}
            //catch (Exception ex)
            //{


            //}
        }
        void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        void txtID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.sDbnull(txtID.Text).Length > 0)
                cmdSearch.PerformClick();
        }
        public void SearchmeNow()
        {
            try
            {
                AllowSelectionChanged = false;
                txtMaluotkham.Text = Utility.AutoFullPatientCode(txtMaluotkham.Text);
                if (dtLuotkham != null && dtLuotkham.Columns.Contains("ma_luotkham") && dtLuotkham.Select("ma_luotkham='" + txtMaluotkham.Text + "'").Length > 0)//Tìm trên lưới
                {
                    Utility.GotoNewRowJanus(grdLuotkham, "ma_luotkham", txtMaluotkham.Text);
                    string id_benhnhan = grdLuotkham.GetValue(KcbLuotkham.Columns.IdBenhnhan).ToString();
                    Utility.GotoNewRowJanus(grdList, KcbLuotkham.Columns.IdBenhnhan, id_benhnhan);
                }
                else//Tìm từ CSDL
                {
                    SearchPatient(new DateTime(1900, 1, 1), new DateTime(1900, 1, 1),-1, Utility.DoTrim(txtMaluotkham.Text), "");
                }
                AllowSelectionChanged = true;
                grdList_SelectionChanged(grdList, new EventArgs());
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }



        }

        void txtMaluotkham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.sDbnull(txtMaluotkham.Text).Length > 0)
            {
                SearchmeNow();
            }

        }

    
        /// <summary>
        /// hàm thực hiện việc dùng phím tắt in phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_lichsu_sudungthuoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                Utility.Showhelps(this.GetType().Assembly.ManifestModule.Name, this.Name);
            if (e.KeyCode == Keys.Enter)
            {
                    SendKeys.Send("{TAB}");
            }


            if (e.Control & e.KeyCode == Keys.F) cmdSearch.PerformClick();
            if (e.KeyCode == Keys.Escape) Close();

            if (e.Control && e.KeyCode == Keys.F5)
            {
                //splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            }
            if (e.KeyCode == Keys.F11 && PropertyLib._AppProperties.ShowActiveControl)
                if (ActiveControl != null) Utility.ShowMsg(ActiveControl.Name);
            
            }


        }

      

        private void cmdClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtFromDate.Value = dtToDate.Value = DateTime.Now;
            txtMaluotkham.Clear();
            txtTenBN.Clear();
            txtMaluotkham.Focus();
        }

       


    }
}