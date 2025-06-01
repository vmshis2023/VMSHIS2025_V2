using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;

namespace VNS.UCs
{
    public partial class VBLine : UserControl
    {
        public VBLine()
        {
            InitializeComponent();
        }
        public string YourText
        {
            get { return Label1.Text; }
            set { Label1.Text = value; }
        }
        public Font FontText
        {
            get { return Label1.Font;}
            set { Label1.Font = value; }
        }
        public Color _FontColor
        {
            get { return Label1.ForeColor; }
            set { Label1.ForeColor = value; }
        }
    }
}
