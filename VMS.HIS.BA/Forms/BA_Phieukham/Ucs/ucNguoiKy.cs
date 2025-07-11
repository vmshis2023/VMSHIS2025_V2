using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;

namespace VMS.HIS.EMR.Forms.BA_Phieukham.Ucs
{
    public partial class ucNguoiKy : UserControl
    {
        public delegate void OnClickMe(string ten_vitri_ky);
        public event OnClickMe _OnClickMe;
        public ucNguoiKy(DataRow drInfor)
        {
            InitializeComponent();
            lnkNguoiky.Text = Utility.sDbnull(drInfor["ten_nguoiky"]);
            lnkNguoiky.Tag= Utility.sDbnull(drInfor["ten_vitri_ky"]);
            if (Utility.ByteDbnull(drInfor["tthai_ky"]) ==1)
            {
                pic.Width = 30;
                lnkNguoiky.ForeColor = Color.DarkGreen;
                lnkNguoiky.Font = new Font(lnkNguoiky.Font.FontFamily, lnkNguoiky.Font.Size, FontStyle.Bold);
            }   
            else
            {
                pic.Width = 0;
                lnkNguoiky.ForeColor = Color.Red;
                lnkNguoiky.Font = new Font(lnkNguoiky.Font.FontFamily, lnkNguoiky.Font.Size, FontStyle.Regular);
            }
            this.Width = pic.Width + lnkNguoiky.Width + 15;
            lnkNguoiky.AutoSize = false;
        }
       
        private void lnkNguoiky_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_OnClickMe != null) _OnClickMe((sender as LinkLabel).Tag.ToString());
        }
    }
}
