using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VMS.QMS.UIControl
{
    public class Marquee : Label
    {
        private  SolidBrush _backBrush;
        private  SolidBrush _textBrush;
        private int _offset;
        string oldText = "";
        public Marquee()
        {
         
            yOffset = 0;
            Speed = 10;
            MarqueeTimer = new Timer();
            MarqueeTimer.Interval = 50;
            MarqueeTimer.Enabled = true;
            MarqueeTimer.Tick += (aSender, eArgs) =>
            {
                if (Speed > 0)
                {
                    yOffset = Convert.ToInt32(Convert.ToDecimal(this.Height) / 2m - Convert.ToDecimal(this.Font.GetHeight()) / 2m);
                    _offset = (_offset - Speed);
                    if (_offset < -(ClientSize.Width)) _offset = 0;
                    Invalidate();
                }
                else
                {
                    _offset = 0;
                    this.TextAlign = ContentAlignment.MiddleCenter;
                    Invalidate();
                }
            };
        }

        public Timer MarqueeTimer { get; set; }
        public int Speed { get; set; }
        public int yOffset { get; set; }
        
        public void Start()
        {
            MarqueeTimer.Start();
        }

        public void Stop()
        {
            MarqueeTimer.Stop();
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            if (Speed > 0)
            {
                _textBrush = new SolidBrush(ForeColor);
                _backBrush = new SolidBrush(BackColor);
                base.OnPaint(e);
                e.Graphics.FillRectangle(_backBrush, e.ClipRectangle);
                e.Graphics.DrawString(Text, Font, _textBrush, _offset, yOffset);
                e.Graphics.DrawString(Text, Font, _textBrush, ClientSize.Width + _offset, yOffset);
            }
            else
                base.OnPaint(e);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Marquee
            // 
            this.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ResumeLayout(false);

        }
    }
}