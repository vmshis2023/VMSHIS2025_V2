using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SubSonic;
using System.Linq;
using VNS.Libs;
using VMS.HIS.DAL;
namespace VNS.HIS.UI.Forms.HinhAnh
{
    public partial class UC_Image : System.Windows.Forms.UserControl
    {
        public delegate void OnChonIn(UC_Image obj);
        public event OnChonIn _OnChonIn;

        public delegate void OnChangeMota(UC_Image obj);
        public event OnChangeMota _OnChangeMota;


        public delegate void OnClick(UC_Image obj);
        public event OnClick _OnClick;
        public delegate void OnDelete(UC_Image obj);
        public event OnDelete _OnDelete;
        public byte [] ImageBytes { get; set; }
        public System.Drawing.Image ImgData
        {
            get
            {
                return PIC_Image.Image;
            }
            set
            {
                if (value != null)
                    PIC_Image.Image = value;
                else if (ImageBytes != null)
                    PIC_Image.Image = byteArrayToImage(ImageBytes);
            }
        }
        public string  Mota { get; set; }
        public long IdHinhAnh = -1;
        private bool chon_in = false;
        public bool raiseEvt = true;
        public bool AllowRaiseEvt = false;
        int currentSTT = 0;
        public string DuongdanLocal = "";
        public string ftpPath = "";
        public UC_Image(FlowLayoutPanel LayoutPanel)
        {
            InitializeComponent();
            this.LayoutPanel = LayoutPanel;
            //this.LayoutPanel.AllowDrop = true;
            txtMota.LostFocus += txtMota_LostFocus;
            
            chkChonAnhIn.CheckedChanged+=chkChonAnhIn_CheckedChanged;
            txtSTT.KeyDown += txtSTT_KeyDown;
            cmdXoa.Click += cmdXoa_Click;

            //PIC_Image.MouseDown += PIC_Image_MouseDown;
            PIC_Image.MouseClick += PIC_Image_MouseClick;
            LayoutPanel.DragDrop += LayoutPanel_DragDrop;
            VisibleControl(false);

        }
        public void Realse1()
        {
            try
            {
                if (ImgData != null)
                {
                    ImgData.Dispose();
                    ImgData = null;
                }

            }
            catch (Exception ex)
            {
               Utility.ShowMsg(ex.Message);
            }
            finally
            {
            }

        }
        public void Realse()
        {
            try
            {
                if (PIC_Image.Image != null)
                {
                    PIC_Image.Image.Dispose();
                    PIC_Image.Image = null;
                }
                if (ImgData != null)
                {
                    ImgData.Dispose();
                    ImgData = null;
                }
                if (ImageBytes != null)
                {
                    ImageBytes = null;
                }
                GC.Collect();

            }
            catch (Exception ex)
            {
               Utility.ShowMsg(ex.Message);
            }
            finally
            {

            }
        }
        void PIC_Image_MouseClick(object sender, MouseEventArgs e)
        {
            if (_OnClick != null) _OnClick(this);
        }

       

        void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (_OnClick != null) _OnClick(this);
        }
        public int STT
        {
            get { return currentSTT; }
            set
            {
                currentSTT = value;
                txtSTT.Text = value.ToString();
            }
        }
        public bool Chonin
        {
            get { return chon_in; }
            set { chon_in = value;
            pnlCheck.Visible = chon_in;
            }
        }
        void LayoutPanel_DragDrop(object sender, DragEventArgs e)
        {
            // is another dragable
            if (e.Data.GetData(typeof(UC_Image)) != null)
            {
                FlowLayoutPanel p = (FlowLayoutPanel)(sender as UC_Image).Parent;
                //Current Position             
                int myIndex = p.Controls.GetChildIndex((sender as UC_Image));

                //Dragged to control to location of next picturebox
                UC_Image q = (UC_Image)e.Data.GetData(typeof(UC_Image));
                p.Controls.SetChildIndex(q, myIndex);
            }    
        }

        void PIC_Image_DragOver(object sender, DragEventArgs e)
        {
           // base.OnDragOver(e);
            
        }

        void PIC_Image_DragDrop(object sender, DragEventArgs e)
        {
            //base.OnDragDrop(e);
            //// is another dragable
            //if (e.Data.GetData(typeof(PictureBox)) != null)
            //{
            //    FlowLayoutPanel p = (FlowLayoutPanel)(sender as PictureBox).Parent;
            //    //Current Position             
            //    int myIndex = p.Controls.GetChildIndex((sender as PictureBox));

            //    //Dragged to control to location of next picturebox
            //    PictureBox q = (PictureBox)e.Data.GetData(typeof(PictureBox));
            //    p.Controls.SetChildIndex(q, myIndex);
            //}   
        }

        void PIC_Image_MouseDown(object sender, MouseEventArgs e)
        {
            //base.OnMouseDown(e);
            //DoDragDrop(sender, DragDropEffects.Copy);
        }

        void txtSTT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (Convert.ToInt32(txtSTT.Text) > LayoutPanel.Controls.Count)
                    {
                      Utility.ShowMsg(string.Format("Bạn cần nhập STT in lớn hơn hoặc bằng 0 và nhỏ hơn hoặc bằng {0}", LayoutPanel.Controls.Count));
                        return;
                    }
                    else
                        AutoOrder(Convert.ToInt32(txtSTT.Text));
                }
                catch (Exception)
                {

                   Utility.ShowMsg("Số thứ tự in phải là chữ số >=0");
                }
                
            }
        }
        void AutoOrder(int STT)
        {
            UC_Image findobj = null;
            foreach (UC_Image uc in LayoutPanel.Controls)
            {
                if (uc.IdHinhAnh != this.IdHinhAnh && uc.currentSTT == STT)
                {
                    findobj = uc;
                    break;
                }
            }
            if (findobj != null)
            {
                findobj.currentSTT = currentSTT;
                UpdateSTT(findobj, currentSTT,false);
            }
            this.currentSTT = STT;
            UpdateSTT(this, STT,false);
            RearrangeControls();
        }
        public void RearrangeControls()
        {
            List<UC_Image> lstCtrl = new List<UC_Image>();
            foreach (UC_Image uc in LayoutPanel.Controls)
            {
                lstCtrl.Add(uc);
            }
            LayoutPanel.Controls.Clear();

            foreach (UC_Image uc in lstCtrl.AsEnumerable().OrderBy(t => t.STT).ToList<UC_Image>())
            {
                LayoutPanel.Controls.Add(uc);
            }
            LayoutPanel.Refresh();
            Application.DoEvents();
        }
        public void UpdateSTT(UC_Image findobj, int newSTT,bool raiseevt)
        {
            findobj.STT = newSTT;
            new Update(KcbKetquaHa.Schema)
                     .Set(KcbKetquaHa.Columns.Sttin).EqualTo(newSTT)
                     .Where(KcbKetquaHa.Columns.Id).IsEqualTo(findobj.IdHinhAnh).Execute();
            if (raiseevt && Chonin) findobj.txtSTT_KeyDown(findobj.txtSTT, new KeyEventArgs(Keys.Enter));
        }
        public void UpdateSTT(int newSTT, bool raiseevt)
        {
            this.STT = newSTT;
            new Update(KcbKetquaHa.Schema)
                     .Set(KcbKetquaHa.Columns.Sttin).EqualTo(newSTT)
                     .Where(KcbKetquaHa.Columns.Id).IsEqualTo(this.IdHinhAnh).Execute();
            if (raiseevt && Chonin) this.txtSTT_KeyDown(this.txtSTT, new KeyEventArgs(Keys.Enter));
        }
        void txtMota_LostFocus(object sender, EventArgs e)
        {
            if (!AllowRaiseEvt) return;
            Mota = txtMota.Text.TrimStart().TrimEnd();
            if (IdHinhAnh > 0)
            {
                new Update(KcbKetquaHa.Schema)
                    .Set(KcbKetquaHa.Columns.Mota).EqualTo(Mota)
                    .Where(KcbKetquaHa.Columns.Id).IsEqualTo(IdHinhAnh).Execute();
            }
            if (_OnChangeMota != null) _OnChangeMota(this);
        }
        public void VisibleControl(bool visible)
        {
            chkChonAnhIn.Visible = visible;
            //lblID.Visible = visible;
            txtMota.Visible = visible;
            cmdXoa.Visible = visible;
            lblMota.Visible = visible;
            pnlInfor.Height = visible ? 44 : 0;
            txtSTT.Visible = visible;
            pnlCheck.Visible = chon_in;
            Application.DoEvents();
        }
        public UC_Image(FlowLayoutPanel LayoutPanel, byte[] ImageBytes, string Mota, int IdHinhAnh, bool chon_in,int STTIn)
        {
            InitializeComponent();
            this.LayoutPanel = LayoutPanel;
            this.LayoutPanel.AllowDrop = true;
            this.ImageBytes = ImageBytes;
            this.Mota = Mota;
            this.STT = STTIn;
            this.LayoutPanel = LayoutPanel;
            this.IdHinhAnh = IdHinhAnh;
            this.chon_in = chon_in;
            pnlCheck.Visible = chon_in;
            txtMota.LostFocus +=txtMota_LostFocus;
            chkChonAnhIn.CheckedChanged +=chkChonAnhIn_CheckedChanged;
            cmdXoa.Click += cmdXoa_Click;
            txtSTT.KeyDown += txtSTT_KeyDown;
            //PIC_Image.MouseDown += PIC_Image_MouseDown;
            PIC_Image.MouseClick+=PIC_Image_MouseClick;
            LayoutPanel.DragDrop += LayoutPanel_DragDrop;

        }
        public UC_Image(FlowLayoutPanel LayoutPanel, System.Drawing.Image ImgData, string Mota, long IdHinhAnh, bool chon_in, int STTIn)
        {
            InitializeComponent();
            this.LayoutPanel = LayoutPanel;
            this.LayoutPanel.AllowDrop = true;
            this.ImgData = ImgData;
            this.Mota = Mota;
            this.STT = STTIn;
            this.LayoutPanel = LayoutPanel;
            this.IdHinhAnh = IdHinhAnh;
            this.chon_in = chon_in;
            pnlCheck.Visible = chon_in;
            txtMota.LostFocus += txtMota_LostFocus;
            chkChonAnhIn.CheckedChanged += chkChonAnhIn_CheckedChanged;
            cmdXoa.Click += cmdXoa_Click;
            txtSTT.KeyDown += txtSTT_KeyDown;
            //PIC_Image.MouseDown += PIC_Image_MouseDown;
            PIC_Image.MouseClick+=PIC_Image_MouseClick;
            LayoutPanel.DragDrop += LayoutPanel_DragDrop;

        }
        void PIC_Image_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           
        }
        public UC_Image Copy(FlowLayoutPanel LayoutPanel, bool raiseEvt)
        {
            UC_Image reval = new UC_Image(LayoutPanel, this.ImgData, this.Mota, this.IdHinhAnh, this.chon_in,this.STT);
            reval.Tag = this.Tag;
            reval.raiseEvt = raiseEvt;
            reval.DuongdanLocal = this.DuongdanLocal;
            return reval;
        }
        System.Drawing.Image returnImage;
        private System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
          
            try
            {
                MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
                ms.Write(byteArrayIn, 0, byteArrayIn.Length);
                returnImage = System.Drawing.Image.FromStream(ms, true);//Exception occurs here
            }
            catch { }
            return returnImage;
        }

        private void UC_Image_Load(object sender, EventArgs e)
        {
            
            txtMota.Text = Mota;
            chkChonAnhIn.Checked = chon_in;
            pnlSelect.BackColor = chon_in ? Color.Green : System.Drawing.SystemColors.Control;
            lblID.Text = IdHinhAnh.ToString();
            AllowRaiseEvt = true;
        }
        public void RefreshMe()
        {
            if (File.Exists(DuongdanLocal))
            {
                this.ImgData = GetImage(DuongdanLocal);
                if (ImgData != null)
                    PIC_Image.Image = ImgData;
                else if (ImageBytes != null)
                    PIC_Image.Image = byteArrayToImage(ImageBytes);
                Application.DoEvents();
            }
        }

        private System.Drawing.Image GetImage(string path)
        {
            using (System.Drawing.Image im = System.Drawing.Image.FromFile(path))
            {
                Bitmap bm = new Bitmap(im);
                return bm;
            }
        }
        private void PIC_Image_Click(object sender, EventArgs e)
        {
         
        }

        private void txtMota_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (!AllowRaiseEvt) return;
            if (e.KeyCode == Keys.Enter)
            {
                if (IdHinhAnh > 0)
                {
                    new Update(KcbKetquaHa.Schema)
                        .Set(KcbKetquaHa.Columns.Mota).EqualTo(txtMota.Text)
                        .Where(KcbKetquaHa.Columns.Id).IsEqualTo(IdHinhAnh).Execute();
                }
            }
        }

        private void chkChonAnhIn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowRaiseEvt) return;
                if (IdHinhAnh > 0)
                {
                    chon_in = chkChonAnhIn.Checked;
                    pnlSelect.BackColor = chon_in ? Color.Green : System.Drawing.SystemColors.Control;
                    pnlCheck.Visible = chon_in;
                    new Update(KcbKetquaHa.Schema)
                      .Set(KcbKetquaHa.Columns.Chonin).EqualTo(chkChonAnhIn.Checked)
                      .Where(KcbKetquaHa.Columns.Id).IsEqualTo(IdHinhAnh).Execute();
                }
                if (_OnChonIn != null) _OnChonIn(this);
            }
            catch (Exception)
            {
                
                //throw;
            }
          
        }
        public FlowLayoutPanel LayoutPanel;
        private void cmdXoa_Click(object sender, EventArgs e)
        {
            DeleteMe(true);
        }
        public void DeleteMe(bool ask)
        {
            try
            {
                if (!AllowRaiseEvt) return;
                if (IdHinhAnh > 0)
                {
                    if (ask)
                        if (MessageBox.Show("Bạn có chắc chắn muốn xóa hình ảnh này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            return;
                        }
                    new Delete()
                    .From(KcbKetquaHa.Schema)
                    .Where(KcbKetquaHa.Columns.Id).IsEqualTo(IdHinhAnh).Execute();
                    LayoutPanel.Controls.Remove(this);
                    if (_OnDelete != null) _OnDelete(this);
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
