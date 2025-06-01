using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SView.UCs
{
    public partial class UCError : UserControl
    {
        Color _color1 = Color.Yellow;
        Color _color2 = Color.Red;
        int _EllapsedTime = 1000;//miliseconds
        int _NumberofBrlink = 5;
        string msg = "";
        public bool IsWorking = false;
        Thread t;
        int idx = 0;
       
        int iCount = 0;
        public UCError()
        {
            InitializeComponent();
            lblError.Click += new EventHandler(lblError_Click);
            
        }
        public void ResetIdx()
        {
            //IsWorking = false;
            idx = 0;
            iCount = 0;
           
        }
        public void Reset()
        {
            //IsWorking = false;
            idx = 0;
            IsWorking = false;
            iCount = 0;
            msg = "";
            if (t!=null && t.IsAlive) t.Abort();
            Thread.Sleep(100);
        }
        public void Reset(int _NumberofBrlink)
        {
            this._NumberofBrlink = _NumberofBrlink;
            //IsWorking = false;
            idx = 0;
            IsWorking = false;
            iCount = 0;
            msg = "";
            if (t != null && t.IsAlive) t.Abort();
            Thread.Sleep(100);
        }
        public void AddMsg(string addMsg)
        {
            if (!msg.ToUpper().Contains(addMsg.ToUpper()))
            {
                msg += "\n" + addMsg;
                ToolTip _ToolTip = new ToolTip();
                _ToolTip.SetToolTip(lblError, msg);
            }

        }
        public void Start(string msg)
        {
            try
            {
                this.Visible = true;
                IsWorking = true;
                Application.DoEvents();
                this.msg = msg;
                toolTip1.SetToolTip(lblError, "");
                toolTip1.SetToolTip(lblError, msg);
                t = new Thread(new ThreadStart(BlinkingNow));
                t.Start();
            }
            catch
            {
            }
        }
        void BlinkingNow()
        {
            try
            {
                while (iCount < _NumberofBrlink)
                {
                    if (idx % 2 == 0)
                        SetBackColor(lblError, _color1);
                    else
                        SetBackColor(lblError, _color2);
                    idx++;
                    iCount++;
                    Thread.Sleep(EllapsedTime);
                }
            }
            catch
            {
            }
        }
        delegate void _SetBackColor(Control ctr, Color color);
        void SetBackColor(Control ctr, Color color)
        {
            try
            {
                if (ctr.InvokeRequired)
                    ctr.Invoke(new _SetBackColor(SetBackColor), new object[] { ctr, color });
                else
                {
                    ctr.BackColor = color;
                    Application.DoEvents();
                }
            }
            catch
            {
            }
        }

        void lblError_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(msg, "warning");
                Reset();
                Application.DoEvents();
                //SetBackColor(lblError, _color2);
                this.Visible = false;
            }
            catch
            {
            }
        }
        public int EllapsedTime
        {
            set { _EllapsedTime = value; }
            get { return _EllapsedTime; }
        }
        public string ErrText
        {
            set { lblError.Text = value; }
            get { return lblError.Text; }
        }
        public int NumberofBrlink
        {
            set { _NumberofBrlink = value; }
            get { return _NumberofBrlink; }
        }
        public Color Color1
        {
            set { _color1 = value; }
            get { return _color1; }
        }
        public Color Color2
        {
            set { _color2 = value; }
            get { return _color2; }
        }

        private void UCError_Load(object sender, EventArgs e)
        {
          
        }
    }
}
