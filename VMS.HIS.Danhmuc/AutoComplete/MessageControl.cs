using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VNS.Libs.UserControls
{
    public partial class MessageControl : UserControl
    {
        public MessageControl()
        {
            InitializeComponent();
        }
        public string Title
        {
            get { return lblMessage.Text; }
            set { lblMessage.Text = value; }
        }
        //public string ShortcutGuide
        //{
        //    get { return lblShortcutGuide.Text; }
        //    set { lblShortcutGuide.Text = value; }
        //}
        public Image PicImg
        {
            get { return pictureBox.Image; }
            set { pictureBox.Image = value; }
        }
        public Color BackGroundColor
        {
            set
            {
                this.BackColor = value;
                lblMessage.BackColor = value;
                //lblTitle.BackColor = value;
                pictureBox.BackColor = value;
            }
        }
        public Font TitleFont
        {
            get { return lblMessage.Font; }
            set { lblMessage.Font = value; }
        }
       
    }
}
