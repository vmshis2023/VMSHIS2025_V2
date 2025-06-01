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
    public partial class ucBacsinoitru : UserControl
    {
        private Color SelectedColor = Color.DarkOrange;
        private Color HoverColor = Color.Red;
        private Color NormalColor = Color.DimGray;
        public bool isPressed = false;
        public delegate void OnClick(ucBacsinoitru obj);
        public event OnClick _OnClick;
        public delegate void OnSelectme(ucBacsinoitru obj);
        public event OnSelectme _OnSelectme;
        public DataRow drData;
        public string ID = "-1";
        public ucBacsinoitru()
        {
            InitializeComponent();
            picBSi.MouseHover += _MouseHover;
            picBSi.MouseLeave += _MouseLeave;
            picBSi.MouseClick += _MouseClick;
            //picBSi.MouseHover += _MouseHover;
            //lblKhoa.MouseHover += _MouseHover;
            //lblName.MouseHover += _MouseHover;
            //lblTheodoi.MouseHover += _MouseHover;
            //this.MouseHover += _MouseHover;

            //picBSi.MouseLeave += _MouseLeave;
            //lblKhoa.MouseLeave += _MouseLeave;
            //lblName.MouseLeave += _MouseLeave;
            //lblTheodoi.MouseLeave += _MouseLeave;
            //this.MouseLeave += _MouseLeave;

            //picBSi.MouseClick += _MouseClick;
            //lblKhoa.MouseClick += _MouseClick;
            //lblName.MouseClick += _MouseClick;
            //lblTheodoi.MouseClick += _MouseClick;
            //this.MouseClick += _MouseClick;
        }
        //protected override void OnEnter(EventArgs e)
        //{
        //    //if (!isPressed)
        //    //{
        //        SetBackGroundColor(HoverColor);
        //    //}
        //    base.OnEnter(e); // this will raise the Enter event
        //}

        //protected override void OnLeave(EventArgs e)
        //{
        //    //if (!isPressed)
        //        SetBackGroundColor(NormalColor);
        //    base.OnLeave(e); // this will raise the Leave event
        //}
        public ucBacsinoitru(DataRow drData)
        {
            InitializeComponent();
            this.drData = drData;
            if(Utility.sDbnull( drData[DmucNhanvien.Columns.IdGioitinh],"-1")=="0")
                picBSi.Image = global::VMS.Resources.Properties.Resources.BSNAM;
            else
                picBSi.Image = global::VMS.Resources.Properties.Resources.BSNU;
            LoadData();
            ID = drData[DmucNhanvien.Columns.IdNhanvien].ToString();
            picBSi.MouseHover += _MouseHover;
            picBSi.MouseLeave += _MouseLeave;
            picBSi.MouseClick += _MouseClick;
            picBSi.MouseDoubleClick += picBSi_MouseDoubleClick;
            //lblKhoa.MouseHover += _MouseHover;
            //lblName.MouseHover += _MouseHover;
            //lblTheodoi.MouseHover += _MouseHover;
           
            //lblKhoa.MouseLeave += _MouseLeave;
            //lblName.MouseHover += _MouseLeave;
            //lblTheodoi.MouseHover += _MouseLeave;
            
            //lblKhoa.MouseClick += _MouseClick;
            //lblName.MouseClick += _MouseClick;
            //lblTheodoi.MouseClick += _MouseClick;
        }

        void picBSi_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_OnSelectme != null) _OnSelectme(this);
        }
        void LoadData()
        {
            lblName.Text = drData["ten_nhanvien"].ToString();
            lblTheodoi.Text = drData["theodoi"].ToString();
            lblKhoa.Text = drData["ten_khoaphong"].ToString();
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
            //BodypartObject.BackColor = Color.WhiteSmoke;
            //BodypartObject.ForeColor = Color.Black;
            //ResetStatus(this.Status);

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
