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
    public partial class ucBuong : UserControl
    {
        private Color SelectedColor = Color.DarkOrange;
        private Color HoverColor = Color.Red;
        private Color NormalColor = Color.DimGray;
        public bool isPressed = false;
        public delegate void OnClick(ucBuong obj);
        public event OnClick _OnClick;
        public delegate void OnSelectme(ucBuong obj);
        public event OnSelectme _OnSelectme;
        public DataRow drData;
        public int id_khoa = -1;
        public int id_buong = -1;
        public ucBuong()
        {
            InitializeComponent();
            picKhoa.MouseHover += _MouseHover;
            picKhoa.MouseLeave += _MouseLeave;
            picKhoa.MouseClick += _MouseClick;
            
        }
       
        public ucBuong(DataRow drData)
        {
            InitializeComponent();
            this.drData = drData;
            if (Utility.sDbnull(drData["trang_thai"], "1") == "0")
                lblName.ForeColor = Color.Red;
            LoadData();
            id_khoa = Utility.Int32Dbnull(drData[NoitruDmucBuong.Columns.IdKhoanoitru], -1);
            id_buong = Utility.Int32Dbnull(drData[NoitruDmucBuong.Columns.IdBuong], -1);
            picKhoa.MouseHover += _MouseHover;
            picKhoa.MouseLeave += _MouseLeave;
            picKhoa.MouseClick += _MouseClick;
            picKhoa.MouseDoubleClick += picBSi_MouseDoubleClick;
          
        }

        void picBSi_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_OnSelectme != null) _OnSelectme(this);
        }
        void LoadData()
        {
            lblName.Text =string.Format(@"{0}:{1}-{2}",drData["id_buong"].ToString(), drData["ma_buong"].ToString(),drData["ten_buong"].ToString());
           lblsogiuong.Text =string.Format("Tổng giường: {0}", drData["soluong_giuong"].ToString());
            lblgiuongtrong.Text =string.Format("Giường trống: {0}", drData["sluong_giuong_trong"].ToString());
            if (Utility.Int32Dbnull(drData["sluong_giuong_trong"], 0) <= 0)
                lblgiuongtrong.ForeColor = Color.Red;
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
    }
     
}
