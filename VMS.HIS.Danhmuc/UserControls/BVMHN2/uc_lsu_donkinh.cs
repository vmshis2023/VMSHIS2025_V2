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
namespace VMS.HIS.Danhmuc.UserControls
{
    public partial class uc_lsu_donkinh : UserControl
    {
        private Color SelectedColor = Color.DarkOrange;
        private Color HoverColor = Color.Red;
        private Color NormalColor = Color.DimGray;
        public bool isPressed = false;
        public delegate void OnClick(uc_lsu_donkinh obj);
        public event OnClick _OnClick;
        public delegate void OnSelectme(uc_lsu_donkinh obj);
        public event OnSelectme _OnSelectme;
        public DataRow drData;
        public long id_donkinh = -1;
        public uc_lsu_donkinh()
        {
            InitializeComponent();
            picKhoa.MouseHover += _MouseHover;
            picKhoa.MouseLeave += _MouseLeave;
            picKhoa.MouseClick += _MouseClick;
            
        }
       
        public uc_lsu_donkinh(DataRow drData)
        {
            InitializeComponent();
            this.drData = drData;
            lnkNgayKedon.Click += ClickMe;
            picKhoa.Click += ClickMe;
            lblBacsi.Click += ClickMe;
            lblDays.Click += ClickMe;
            pictureBox1.Click += ClickMe;

            picKhoa.MouseHover += _MouseHover;
            picKhoa.MouseLeave += _MouseLeave;
            picKhoa.MouseClick += _MouseClick;
            picKhoa.MouseDoubleClick += picBSi_MouseDoubleClick;
          
        }

        private void ClickMe(object sender, EventArgs e)
        {
            if (_OnClick != null)
                _OnClick(this);
        }

        void picBSi_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_OnSelectme != null) _OnSelectme(this);
        }
        public void LoadData()
        {
            id_donkinh = Utility.Int64Dbnull(drData[KcbDonkinh.Columns.IdDonkinh], -1);

            lnkNgayKedon.Text =string.Format("Ngày kê đơn: {0}", drData[KcbDonkinh.Columns.NgayKedon].ToString());
            lblBacsi.Text = string.Format("Bác sĩ: {0}", drData["ten_bacsi"].ToString());
            DateTime ngay_kedon = Convert.ToDateTime(drData[KcbDonkinh.Columns.NgayKedon]);
            if (ngay_kedon.Date == DateTime.Now.Date)
            { 
                lnkNgayKedon.Text = "Hôm nay";
            }    
            lblDays.Text = string.Format("Cách đây {0} ngày",THU_VIEN_CHUNG.Songay(ngay_kedon, DateTime.Now,false,24));
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
