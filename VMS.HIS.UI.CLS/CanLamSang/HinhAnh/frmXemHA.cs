using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VNS.Properties;
namespace VNS.HIS.UI.Forms.HinhAnh
{
    
    public partial class frmXemHA : Form
    {
        public delegate void OnCut(Rectangle r);
        public event OnCut _OnCut;

        private Image _originalImage;
        private Image _originalImage_cut;
        private bool _selecting;
        private Rectangle _selection;
        string path = "";
        public bool CloseConfirmed = true;
        public bool hasChanged = false;
        public string patientInfor = "";
        public Logger _log = null;
        public frmXemHA(string path)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.path = path;
            this.KeyDown += frmXemHA_KeyDown;
            this.Load += frmXemHA_Load;
            this.Text = patientInfor;
            this.FormClosing += frmXemHA_FormClosing;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            pictureBox1.Paint += pictureBox1_Paint;
            button1.Click += button1_Click;
        }
        public void ZoomIn()
        {
            //Multiplier = new Size(2, 2);

            Image MyImage = _originalImage_cut.Clone() as Image;

            Bitmap MyBitMap = new Bitmap(MyImage, Convert.ToInt32(Convert.ToDecimal(MyImage.Width) * ratioZoom),
                Convert.ToInt32(Convert.ToDecimal(MyImage.Height) * ratioZoom));

            Graphics Graphic = Graphics.FromImage(MyBitMap);

            Graphic.InterpolationMode = InterpolationMode.High;

            pictureBox1.Image = MyBitMap;

        }

        public void ZoomOut()
        {
            // Multiplier = new Size(2, 2);

            Image MyImage = _originalImage_cut.Clone() as Image;

            Bitmap MyBitMap = new Bitmap(MyImage, Convert.ToInt32(Convert.ToDecimal(MyImage.Width) / ratioZoom),
                Convert.ToInt32(Convert.ToDecimal(MyImage.Height) / ratioZoom));

            Graphics Graphic = Graphics.FromImage(MyBitMap);

            Graphic.InterpolationMode = InterpolationMode.High;

            pictureBox1.Image = MyBitMap;

        }
        public void ZoomMe()
        {
            CultureInfo ci = new CultureInfo("en-us");
            // Multiplier = new Size(2, 2);
            this.Text = string.Format(patientInfor + " - Zoom {0}: {1}", ratio > 1 ? "out" : "in", (ratio).ToString("P00", ci));
            Application.DoEvents();
            Image MyImage = _originalImage_cut.Clone() as Image;
            if (ratio == 0 || ratio >= PropertyLib._HinhAnhProperties.ZoomRatio)
            {
                ratio = 1;
            }
            Bitmap MyBitMap = new Bitmap(MyImage, Convert.ToInt32(Convert.ToDecimal(MyImage.Width) * (Convert.ToDecimal(Math.Abs(ratio)))),
                Convert.ToInt32(Convert.ToDecimal(MyImage.Height) * (Convert.ToDecimal(Math.Abs(ratio)))));

            Graphics Graphic = Graphics.FromImage(MyBitMap);

            Graphic.InterpolationMode = InterpolationMode.High;

            pictureBox1.Image = MyBitMap;

        }
        Decimal ratio = 1;
        decimal ratioZoom = 1m;
        protected override void OnMouseWheel(MouseEventArgs e)
        {

                if (e.Delta != 0)
                {
                    if (e.Delta <= 0)
                    {
                        ratio -= 0.05m;
                    }
                    else
                    {
                        ratio += 0.05m;
                    }
                    ZoomMe();
                }
        }
        void frmXemHA_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hasChanged)
            {
                if (CloseConfirmed)
                {
                    if (Utility.AcceptQuestion("Bạn có chắc chắn muốn lưu lại hình ảnh đã thay đổi này?", "Xác nhận lưu", true))
                        pictureBox1.Image.Save(path);
                    else
                        hasChanged = false;
                }
                else
                    pictureBox1.Image.Save(path);
            }

        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
                _selecting = true;
                _selection = new Rectangle(new Point(e.X, e.Y), new Size());
        }
        //---------------------------------------------------------------------
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // Update the actual size of the selection:
            if ( _selecting)
            {
                _selection.Width = e.X - _selection.X;
                _selection.Height = e.Y - _selection.Y;

                // Redraw the picturebox:
                pictureBox1.Refresh();
            }
        }
        //---------------------------------------------------------------------
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if ( e.Button == MouseButtons.Left && (_selecting && _selection.Width > 0 && _selection.Height > 0))
            {
               // this.Text = string.Format(" X={0},Y={1}, Width={2} height={3}",_selection.X.ToString(),_selection.Y.ToString(), _selection.Width.ToString(), _selection.Height.ToString());
                // Create cropped image:
                
                    _selection.X = Convert.ToInt32(_selection.X) - (Convert.ToInt32(this.ClientSize.Width / 2) - Convert.ToInt32(pictureBox1.Image.Width / 2));
                    _selection.Y = Convert.ToInt32(_selection.Y) - (Convert.ToInt32(this.ClientSize.Height / 2) - Convert.ToInt32(pictureBox1.Image.Height / 2));
                
                Image img = pictureBox1.Image.Crop(_selection);
                if (_OnCut != null) _OnCut(_selection);
                _log.Trace(string.Format("AutoCut image with REC=[{0}]", string.Format(" X={0},Y={1}, Width={2} height={3}", _selection.X.ToString(), _selection.Y.ToString(), _selection.Width.ToString(), _selection.Height.ToString())));
                // Fit image to the picturebox:
                pictureBox1.Image = img;// img.Fit2PictureBox(pictureBox1);
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                hasChanged = true;
                _selecting = false;
            }
        }
        //---------------------------------------------------------------------
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
            if (_selecting &&  _selection.Width > 0 && _selection.Height > 0)
            {
                // Draw a rectangle displaying the current selection
                Pen pen = Pens.GreenYellow;
                //pen.Width = 2f;
                //pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                e.Graphics.DrawRectangle(pen, _selection);
               // this.Text = string.Format(" X={0},Y={1}, Width={2} height={3}", _selection.X.ToString(), _selection.Y.ToString(), _selection.Width.ToString(), _selection.Height.ToString());
            }
        }
        //---------------------------------------------------------------------
        private void button1_Click(object sender, System.EventArgs e)
        {
            hasChanged = false;
            ratioZoom = 1m;
            ratio = 1;
            _originalImage_cut = _originalImage.Clone() as Image;
            pictureBox1.Image = _originalImage.Clone() as Image;
        }
        void frmXemHA_Load(object sender, EventArgs e)
        {
            _originalImage = pictureBox1.Image.Clone() as Image;
            _originalImage_cut = _originalImage.Clone() as Image;
        }

        void frmXemHA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
            else if (e.Control && e.KeyCode == Keys.A)
            {
                AutoC(true);
            }
        }
        public void AutoC(bool CloseConfirmed)
        {
            this.CloseConfirmed = CloseConfirmed;
            Image img = pictureBox1.Image.Crop(PropertyLib._HinhAnhProperties.CropRec);
            // Fit image to the picturebox:
            pictureBox1.Image = img;// img.Fit2PictureBox(pictureBox1);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            hasChanged = true;
            _selecting = false;
            if (!CloseConfirmed)
                pictureBox1.Image.Save(path);
            this.Close();
        }
        public void SetImg(Image img)
        {
            pictureBox1.Image = img;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            hasChanged = false;
            this.Close();
        }
    }
    public class XemHinhAnh
    {
        static frmXemHA frmXemanh = new frmXemHA("");
        public static void SetImg(Image img)
        {
            frmXemanh.ShowDialog();
            frmXemanh.SetImg(img);
        }
    }
}
