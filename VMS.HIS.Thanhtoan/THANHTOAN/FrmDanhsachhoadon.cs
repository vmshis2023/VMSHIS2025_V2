using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VMS.HIS.DAL;
using VNS.HIS.UI.Classess;
using VNS.HIS.UI.THANHTOAN;
using VNS.Libs;
using VNS.Libs.AppUI;

namespace VNS.HIS.UI.Forms.THANHTOAN
{
    public partial class FrmDanhsachhoadon : Form
    {
        public FrmDanhsachhoadon()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
        }

        private void radinrieng_CheckedChanged(object sender, EventArgs e)
        {
            if (radinrieng.Checked)
            {
                grdList.BringToFront();
                grdListIngop.SendToBack();
            }
            else
            {
                grdList.SendToBack();
                grdListIngop.BringToFront();
            }
        }

        private void radingop_CheckedChanged(object sender, EventArgs e)
        {
            if (radingop.Checked)
            {
                grdList.SendToBack();
                grdListIngop.BringToFront();
                
            }
            else
            {
                grdList.BringToFront();
                grdListIngop.SendToBack();
            }
        }

        private void FrmDanhsachhoadon_Load(object sender, EventArgs e)
        {
            dtTuNgay.Value = dtDenNgay.Value = globalVariables.SysDate;
        }

        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            grdList.DataSource = null;
            grdListIngop.DataSource = null;
            int dagui = -1;
            if (radall.Checked) dagui = -1;
            if (radchuagui.Checked) dagui = 0;
            if (raddagui.Checked) dagui = 1;
            int ingop = 0;
            if (radinrieng.Checked) ingop = 0;
            if (radingop.Checked) ingop = 1;

            DataTable dtDanhsach =
                SPs.HoadonLaythongtinDanhsachhoadonDasudung(dtTuNgay.Value, dtDenNgay.Value, Utility.Int16Dbnull(dagui), Utility.ByteDbnull(ingop),
                    txtPatient_Code.Text.Trim(), txtTenBN.Text.Trim()).GetDataSet().Tables[0];
            if (radingop.Checked)
            {
                grdListIngop.DataSource = dtDanhsach;
            }
            else
            {
                grdList.DataSource = dtDanhsach;
            }
        }
        public void LogText(string sLogText, Color sActionColor)
        {
            if (InvokeRequired)
            {
                Invoke(new frm_InGopBenhNhan.AddLog(LogText), new object[] { sLogText, sActionColor });
            }
            else
            {
                AddAction(sLogText, sActionColor);
                //rtxtLogs.AppendText(sLogText);
                rtxtLogs.AppendText(_sNewline);
                //TextBoxTraceListener.SendMessage(_richTextBoxLog.Handle, TextBoxTraceListener.WM_VSCROLL, TextBoxTraceListener.SB_BOTTOM, 0);
            }
        }
        public delegate void AddLog(string logText, Color sActionColor);
        private const string _sNewline = "\r\n";
        private void AddAction(string sLogText, Color color)
        {
            if (sLogText.Length > 0)
            {
                Color oldColor = rtxtLogs.SelectionColor;
                rtxtLogs.SelectionLength = 0;
                rtxtLogs.SelectionStart = rtxtLogs.Text.Length;
                rtxtLogs.SelectionColor = color;
                rtxtLogs.SelectionFont = new Font(rtxtLogs.SelectionFont, FontStyle.Bold);
                rtxtLogs.AppendText(sLogText);
                rtxtLogs.SelectionColor = oldColor;
            }
        }
        
        //KetNoiHoaDonDienTu _ketNoi = new KetNoiHoaDonDienTu();
        private void cmdSend_Click(object sender, EventArgs e)
        {
            //if (radinrieng.Checked)
            //{
            //    //if (grdList.GetCheckedRows().Any())
            //    //{
            //    //    Utility.ShowMsg("Bạn phải chọn dữ liệu để gửi lên cổng hóa đơn điện tử");
            //    //    return;
            //    //}
            //    //else
            //    //{
            //        int i = 0;
            //        int j = 0;
            //        Utility.ResetProgressBar(prgBar, grdList.GetCheckedRows().Count(), true);
            //        foreach (GridEXRow row in grdList.GetCheckedRows())
            //        {
            //            string sohoadon = Utility.sDbnull(row.Cells["serie"].Value);
            //            bool kt = _ketNoi.SendInvoice(sohoadon, 0);
            //            if (kt)
            //            {
            //                LogText(string.Format("Gửi thành công số hóa đơn {0} lên cổng hóa đơn điện tử",sohoadon), Color.DarkBlue);
            //                i = i + 1;
            //            }
            //            else
            //            {
            //                LogText(string.Format("Gửi không thành công số hóa đơn {0} lên cổng hóa đơn điện tử", sohoadon), Color.Red);
            //                j = j + 1;
            //            }
            //            UIAction.SetValue4Prg(prgBar, 1);
            //            Application.DoEvents();
            //            row.IsChecked = false;
            //            Thread.Sleep(10);
            //        }
            //        LogText(string.Format("------------------------------------------------"), Color.DarkBlue);
            //        LogText(string.Format("Gửi thành công tổng cộng: {0} --- Gửi không thành công tổng cộng: {1}", i, j), Color.DarkBlue);
            //    //}
            //}
            //else
            //{
            //    //if (grdListIngop.GetCheckedRows().Count() > 0)
            //    //{
            //    //    Utility.ShowMsg("Bạn phải chọn dữ liệu để gửi lên cổng hóa đơn điện tử");
            //    //    return;
            //    //}
            //    //else
            //    //{
            //        int i = 0;
            //        int j = 0;
            //        Utility.ResetProgressBar(prgBar, grdListIngop.GetCheckedRows().Count(), true);
            //        foreach (GridEXRow row in grdListIngop.GetCheckedRows())
            //        {
            //            string sohoadon = Utility.sDbnull(row.Cells["serie"].Value);
            //            bool kt = _ketNoi.SendInvoice(sohoadon, 1);
            //            if (kt)
            //            {
            //                LogText(string.Format("Gửi thành công số hóa đơn {0} lên cổng hóa đơn điện tử", sohoadon), Color.DarkBlue);
            //                i = i + 1;
            //            }
            //            else
            //            {
            //                LogText(string.Format("Gửi không thành công số hóa đơn {0} lên cổng hóa đơn điện tử", sohoadon), Color.Red);
            //                j = j + 1;
            //            }
            //            UIAction.SetValue4Prg(prgBar, 1);
            //            Application.DoEvents();
            //            row.IsChecked = false;
            //            Thread.Sleep(10);
            //        }
            //        LogText(string.Format("------------------------------------------------"), Color.DarkBlue);
            //        LogText(string.Format("Ghép thành công tổng cộng: {0} --- Ghép không thành công tổng cộng: {1}", i, j), Color.DarkBlue);
            //    //}
            //}
        }
    }
}
