using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using VNS.Libs;
using SubSonic;
using VMS.HIS.DAL;

namespace VietBaIT
{
    public partial class frm_RegEprd : Form
    {
        string realDate = "";
        DateTime _dtmRealDate = DateTime.Now;
        public bool reActivate = false;
        string pKey = "";
        string DisplayLanguage = "";
        public long CurrentExp = 0;
        VNS.Libs.AppType.AppEnum.AppName _AppName = VNS.Libs.AppType.AppEnum.AppName.QLPK;
        public frm_RegEprd(string pKey, string DisplayLanguage, long CurrentExp, VNS.Libs.AppType.AppEnum.AppName _AppName)
        {
            InitializeComponent();
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            this._AppName = _AppName;
            this.CurrentExp = CurrentExp;
            this.DisplayLanguage = DisplayLanguage;
            this.pKey = pKey;
            this.KeyDown += new KeyEventHandler(frm_RegEprd_KeyDown);
            this.txtexpd.DoubleClick += new EventHandler(txtexpd_DoubleClick);
            txtKey.Text = pKey;
        }

        void txtexpd_DoubleClick(object sender, EventArgs e)
        {
            txtExpValue.Visible = true;
        }

        void frm_RegEprd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdCancel.PerformClick();
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if (e.Alt && e.Control && e.Shift && e.KeyCode == Keys.R)
            {
                txtExpValue.Text = realDate;
            }
        }
        string expfile = Application.StartupPath + @"\xvexplic.exp";
        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (txtExpValue.Text.Trim().Length != 10)
            {
                AppChecker.CheckHrk.ShowMsg("Invalid license file");
                txtexpd.Focus();
                return;
            }
            if (CurrentExp > 0)
            {
                long _VALUE = Utility.Int64Dbnull(_dtmRealDate.ToString("yyyyMMdd"));
                SysSystemParameter p = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("SHOWEXP").ExecuteSingle<SysSystemParameter>();
                if (p != null && p.SValue == "1")
                    Utility.ShowMsg(string.Format("VALUE={0} CurrentExp={1}", _VALUE, CurrentExp));
                if (_VALUE <= CurrentExp)
                {
                    Utility.ShowMsg("the value of expabdable day must be greater then the last expired date(>) " + new DateTime(Convert.ToInt32(CurrentExp.ToString().Substring(0, 4)), Convert.ToInt32(CurrentExp.ToString().Substring(4, 2)), Convert.ToInt32(CurrentExp.ToString().Substring(6, 2))).ToString("dd/MM/yyyy"));
                    return;
                }
            }
            this.DialogResult = DialogResult.OK;
            SaveExpd(txtexpd.Text.Trim());
        }
        string getValidValue(string value)
        {
            try
            {
                realDate = "";
                xvect.Encrypt _ect = new xvect.Encrypt();
                _ect.UpdateAlgName(_ect.AlgName);
                _ect.sPwd = Utility.getAPPName(_AppName) + pKey;
                string _exp = _ect.GiaiMa(value.Split('|')[0]);
                //_exp = _exp.Split('|')[0];
                if (_exp.Length == 8)
                {
                    _dtmRealDate = new DateTime(Convert.ToInt32(_exp.Substring(0, 4)), Convert.ToInt32(_exp.Substring(4, 2)), Convert.ToInt32(_exp.Substring(6, 2)));
                    realDate = _dtmRealDate.ToString("dd/MM/yyyy");
                    return realDate;// new DateTime(Convert.ToInt32(_exp.Substring(0, 4)), Convert.ToInt32(_exp.Substring(4, 2)), Convert.ToInt32(_exp.Substring(6, 2))).AddYears(5).ToString("dd/MM/yyyy");
                }
                else
                    realDate = "";
                return "Invalid";
            }
            catch
            {
                return "Invalid";
            }
        }
        void SaveExpd(string expd)
        {
            try
            {
                using (StreamWriter _streamW = new StreamWriter(expfile))
                {
                    _streamW.WriteLine(expd);
                    _streamW.Flush();
                    _streamW.Close();
                }
              if(reWrite)  AutosaveMaxSystemStudyDate();
                ReActivate();
            }
            catch (Exception ex)
            {
                AppChecker.CheckHrk.ShowMsg("Error when saving activate key:\n" + ex.Message);
            }
        }
        public bool reWrite = false;
        string maxstudydatefilepath = Application.StartupPath + @"\ms4chkr.dll";
        void AutosaveMaxSystemStudyDate()
        {
            try
            {
                xvect.Encrypt _ect = new xvect.Encrypt();
                _ect.UpdateAlgName(_ect.AlgName);
                _ect.sPwd = Utility.getAPPName(_AppName) + "ms4chkr.dll";
                long _now = Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd"));
                Utility.SaveValue2File(maxstudydatefilepath, _ect.Mahoa(_now.ToString()));
            }
            catch
            {
            }
        }
        static string appexrd = Environment.SystemDirectory + @"\sysappexpd.dll";
        static string appexrd1 = Application.StartupPath + @"\sysappexpd.dll";
        void ReActivate()
        {
            Utility.Try2DelFile(appexrd);
            Utility.Try2DelFile(appexrd1);
        }
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtexpd_TextChanged(object sender, EventArgs e)
        {
            txtExpValue.Text = getValidValue(txtexpd.Text.Trim());
        }

        private void cmdBrowseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog _OpenFileDialog = new OpenFileDialog();
            _OpenFileDialog.Multiselect = false;
            _OpenFileDialog.Title = "Select expandable file";
            _OpenFileDialog.Filter = "Extend file(*.exp)|*.exp;*.lic|All files (*.*)|*.*";
            if (_OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string _value=Utility.GetFirstValueFromFile(_OpenFileDialog.FileName);
                txtexpd.Text = _value.Split('|')[0] + "|" + _value.Split('|')[1];
            }
        }

    }
}
