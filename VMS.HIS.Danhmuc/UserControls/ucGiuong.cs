using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.Libs;
namespace VNS.HIS.UI.Forms.Dungchung.UCs
{
    public partial class ucGiuong : UserControl
    {
        private Color SelectedColor = Color.DarkOrange;
        private Color HoverColor = Color.Red;
        private Color NormalColor = Color.DimGray;
        public bool isPressed = false;
        public delegate void OnClick(ucGiuong obj);
        public event OnClick _OnClick;
        public delegate void OnSelectme(ucGiuong obj);
        public event OnSelectme _OnSelectme;
        public DataRow drData;
        public int id_giuong = -1;
        public int id_khoa = -1;
        public int id_buong = -1;
        public int tongsolan_nguoibenh = 0;//Tổng số người bệnh đã nằm
        public int tongsongay_dung = 0;
        public decimal tongso_tien = 0;
        public ucGiuong()
        {
            InitializeComponent();
            picKhoa.MouseHover += _MouseHover;
            picKhoa.MouseLeave += _MouseLeave;
            picKhoa.MouseClick += _MouseClick;
            
        }
       
        public ucGiuong(DataRow drData)
        {
            InitializeComponent();
            this.drData = drData;
            if (Utility.sDbnull(drData["trang_thai"], "1") == "0")
            {
                lblName.ForeColor = Color.Red;
                lblDangnam.BackColor = Color.FromArgb(255, 192, 192);
                lblDangnam.Text = string.Format("Đang sử dụng");
            }
            else
                lblDangnam.Text = string.Format("Giường trống");
            LoadData();
            id_giuong = Utility.Int32Dbnull(drData[NoitruDmucGiuongbenh.Columns.IdGiuong], -1);
            id_khoa = Utility.Int32Dbnull(drData[NoitruDmucGiuongbenh.Columns.IdKhoanoitru], -1);
            id_buong = Utility.Int32Dbnull(drData[NoitruDmucGiuongbenh.Columns.IdBuong], -1);
            tongso_tien = Utility.DecimaltoDbnull(drData["tong_tien"], 0);
            tongsolan_nguoibenh = Utility.Int32Dbnull(drData["so_luotkham"], 0);
            tongsongay_dung = Utility.Int32Dbnull(drData["so_ngay"], 0);

           
            lblSoluotkham.Text = string.Format("Tổng lượt nằm: {0}", tongsolan_nguoibenh.ToString());
            lblSotien.Text = string.Format("Tổng tiền nằm: {0}", Utility.FormatCurrencyHIS( tongso_tien.ToString()));
            picKhoa.MouseHover += _MouseHover;
            picKhoa.MouseLeave += _MouseLeave;
            picKhoa.MouseClick += _MouseClick;
            picKhoa.MouseDoubleClick += picBSi_MouseDoubleClick;
            lnkViewHistory.Click += lnkViewHistory_Click;

          
        }

        void lnkViewHistory_Click(object sender, EventArgs e)
        {
            
        }

        void picBSi_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_OnSelectme != null) _OnSelectme(this);
        }
        void LoadData()
        {
            lblName.Text = string.Format(@"{0}:{1}-{2}", drData["id_giuong"].ToString(), drData["ma_giuong"].ToString(), drData["ten_giuong"].ToString());
            lblSoluotkham.Text = drData["songuoi_toida"].ToString();
        }
        public void SetSelected(bool _isSelected)
        {
            isPressed = _isSelected;
            this.BackColor = _isSelected ? SelectedColor : NormalColor;
            Application.DoEvents();
        }
        public void SetHover (bool _isHover)
        {
            this.BackColor = _isHover ?HoverColor : NormalColor;
            Application.DoEvents();
        }
        void _MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left )
            {
                if (_OnClick != null)
                    _OnClick(this);
            }
        }
        public void Reset()
        {
            isPressed = false;

        }
        void _MouseLeave(object sender, EventArgs e)
        {
            if (!isPressed)
                SetBackGroundColor(NormalColor);
        }

        void _MouseHover(object sender, EventArgs e)
        {
            if (!isPressed)
            {
                SetBackGroundColor(HoverColor);
            }
        }
        void SetBackGroundColor(Color newColor)
        {
            this.BackColor = newColor;
            this.Invalidate();
        }

        public void UpdateColor(Color SelectedColor, Color HoverColor, Color NormalColor)
        {
            this.SelectedColor = SelectedColor;
            this.HoverColor = HoverColor;
            this.NormalColor = NormalColor;
        }

        private void lnkViewHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
     
}
