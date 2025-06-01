using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using VNS.Properties;

namespace VNS.UCs
{
    public partial class  ucPhieuxuatthuocBN: UserControl
    {
        public bool isPressed = false;
        public string _Code = "";
        public string _Name = "";
        public long _ID = -1;
        public delegate void OnSelect(ucPhieuxuatthuocBN obj);
        public event OnSelect _OnSelect;
        public delegate void OnClick(ucPhieuxuatthuocBN obj);
        public event OnClick _OnClick;
        public delegate void OnRefresh(ucPhieuxuatthuocBN obj);
        public event OnRefresh _OnRefresh;

        public Button _ScheduleObj
        {
            get { return ScheduleObj; }
        }
        public ucPhieuxuatthuocBN()
        {
            InitializeComponent();
        }
        public ucPhieuxuatthuocBN(long _ID, string _Code, string _Name)
        {
            InitializeComponent();
            this._ID = _ID;
            this._Code = _Code;
            this._Name = _Name;
            ScheduleObj.Click += new EventHandler(ScheduleObj_Click);
           
        }
       
        
        public bool CanAdd
        {
            get { return ScheduleObj.BackColor == PropertyLib._PhieuxuatBNProperty.SelectedBackColor; }
        }
       
        public void Reset()
        {
            isPressed = false;
            _ScheduleObj.Font = PropertyLib._PhieuxuatBNProperty.NormalFont;
            ScheduleObj.BackColor = PropertyLib._PhieuxuatBNProperty.NormalBackColor;
            ScheduleObj.ForeColor = PropertyLib._PhieuxuatBNProperty.NormalForeColor;

        }
        public void UnSelectMe()
        {
            isPressed = false;
            _ScheduleObj.Font = PropertyLib._PhieuxuatBNProperty.NormalFont;
            ScheduleObj.BackColor = PropertyLib._PhieuxuatBNProperty.NormalBackColor;
            ScheduleObj.ForeColor = PropertyLib._PhieuxuatBNProperty.NormalForeColor;

        }
        public void ResetColor()
        {
            if (!isPressed)
            {
                _ScheduleObj.Font = PropertyLib._PhieuxuatBNProperty.NormalFont;
                _ScheduleObj.BackColor = PropertyLib._PhieuxuatBNProperty.NormalBackColor;
                _ScheduleObj.ForeColor = PropertyLib._PhieuxuatBNProperty.NormalForeColor;
            }
            else
            {
                _ScheduleObj.Font = PropertyLib._PhieuxuatBNProperty.SelectedFont;
                _ScheduleObj.BackColor = PropertyLib._PhieuxuatBNProperty.SelectedBackColor;
                _ScheduleObj.ForeColor = PropertyLib._PhieuxuatBNProperty.SelectedForeColor;
            }
        }
        public void SelectMe()
        {
            isPressed = true;
            if (_OnSelect != null) _OnSelect(this);
            _ScheduleObj.Font = PropertyLib._PhieuxuatBNProperty.SelectedFont;
            _ScheduleObj.BackColor = PropertyLib._PhieuxuatBNProperty.SelectedBackColor;
            _ScheduleObj.ForeColor = PropertyLib._PhieuxuatBNProperty.SelectedForeColor;
        }
        public void SetSelected(bool isPressed)
        {
            this.isPressed = isPressed;
            if (!isPressed)
            {
                _ScheduleObj.Font = PropertyLib._PhieuxuatBNProperty.NormalFont;
                _ScheduleObj.BackColor = PropertyLib._PhieuxuatBNProperty.NormalBackColor;
                _ScheduleObj.ForeColor = PropertyLib._PhieuxuatBNProperty.NormalForeColor;
            }
            else
            {
               
                _ScheduleObj.Font = PropertyLib._PhieuxuatBNProperty.SelectedFont;
                _ScheduleObj.BackColor = PropertyLib._PhieuxuatBNProperty.SelectedBackColor;
                _ScheduleObj.ForeColor = PropertyLib._PhieuxuatBNProperty.SelectedForeColor;
            }
            _OnClick(this);
        }
        
        void ScheduleObj_Click(object sender, EventArgs e)
        {
            //if (!isPressed)
                _OnClick(this);
            
            
        }
        private void ucPhieuxuatthuocBN_Load(object sender, EventArgs e)
        {
            ScheduleObj.Text = _Name;
            ScheduleObj.Tag = _ID;
        }

        private void mnuRefreshList_Click(object sender, EventArgs e)
        {
            if (_OnRefresh != null) _OnRefresh(this);
        }
    }
}
