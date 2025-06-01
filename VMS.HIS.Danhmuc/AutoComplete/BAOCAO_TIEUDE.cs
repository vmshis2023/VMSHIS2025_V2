using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using SubSonic;
using VMS.HIS.DAL;
using System.IO;

namespace VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls
{
    public partial class BAOCAO_TIEUDE : UserControl
    {
        bool hasLoaded = false;
        public BAOCAO_TIEUDE()
        {
            InitializeComponent();
            txtTieuDe.LostFocus += new EventHandler(txtTieuDe_LostFocus);
            txtTieuDe.GotFocus += new EventHandler(txtTieuDe_GotFocus);
            txtTieuDe.KeyDown += new KeyEventHandler(txtTieuDe_KeyDown);
            
            cmdSave.Click += new EventHandler(cmdSave_Click);
            //Init();
        }
        
        public string Phimtat
        {
            get { return lblPhimtat.Text; }
            set { lblPhimtat.Text = value; }
        }
        public bool showHelp
        {
            get { return lblPhimtat.Visible; }
            set { lblPhimtat.Visible = false; }
        }
       
        public Image PicImg
        {
            get { return pnlImg.BackgroundImage; }
            set { pnlImg.BackgroundImage = value; }
        }
        public Color BackGroundColor
        {
            set
            {
                this.BackColor = value;
                lblPhimtat.BackColor = value;
                txtTieuDe.BackColor = value;
                pnlImg.BackColor = value;
            }
        }
        public Font TitleFont
        {
            get { return txtTieuDe.Font; }
            set { txtTieuDe.Font = value; }
        }
        public Font ShortcutFont
        {
            get { return lblPhimtat.Font; }
            set { lblPhimtat.Font = value; }
        }
        public ContentAlignment ShortcutAlignment
        {
            get { return lblPhimtat.TextAlign; }
            set
            {
                lblPhimtat.TextAlign = value;
            }
        }
        public Point ShortcutLocation
        {
            set { lblPhimtat.Location = value; }
        }

        public string MA_BAOCAO
        {
            get;
            set;
        }
        public bool ShowSaveCommand
        {
            set { cmdSave.Visible = value; }
        }
        public string TIEUDE
        {
            get { return Utility.DoTrim(txtTieuDe.Text); }
            set { txtTieuDe.Text = value; }
        }
        void txtTieuDe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && cmdSave.Visible)
                THU_VIEN_CHUNG.CapnhatgiatriTieudebaocao(MA_BAOCAO, Utility.DoTrim(txtTieuDe.Text));
        }
        void cmdSave_Click(object sender, EventArgs e)
        {
            if (globalVariables.IsAdmin || globalVariables.isSuperAdmin)
                THU_VIEN_CHUNG.CapnhatgiatriTieudebaocao(MA_BAOCAO, Utility.DoTrim(txtTieuDe.Text));
            else
                Utility.ShowMsg("Bạn không có quyền sửa tiêu đề báo cáo. Liên hệ quản trị hệ thống để được hỗ trợ");
        }
        void txtTieuDe_GotFocus(object sender, EventArgs e)
        {
            if (cmdSave.Visible) txtTieuDe.ReadOnly = false;
        }

        void txtTieuDe_LostFocus(object sender, EventArgs e)
        {
            if (cmdSave.Visible) txtTieuDe.ReadOnly = true;
        }
        public void Init(string MA_BAOCAO)
        {
            this.MA_BAOCAO = MA_BAOCAO;
            if (hasLoaded) return;
            SysReport rp = new Select().From(SysReport.Schema).Where(SysReport.Columns.MaBaocao).IsEqualTo(MA_BAOCAO).ExecuteSingle<SysReport>();
            if (rp != null)
            {
                nmrLine.Value = Utility.DecimaltoDbnull(rp.ExcelStartLine, 10);
                lblExcelFile.Text = Utility.sDbnull(rp.ExcelStartLine, "");
            }
            txtTieuDe.Text = THU_VIEN_CHUNG.LaygiatriTieudebaocao(MA_BAOCAO, txtTieuDe.Text, true);
            cmdSave.Visible = globalVariablesPrivate.objNhanvien != null && Utility.Coquyen("quyen_suatieudebaocao");
            hasLoaded = true;
        }
        public void Init()
        {
            if (hasLoaded) return;
            SysReport rp = new Select().From(SysReport.Schema).Where(SysReport.Columns.MaBaocao).IsEqualTo(MA_BAOCAO).ExecuteSingle<SysReport>();
            if (rp != null)
            {
                nmrLine.Value = Utility.DecimaltoDbnull(rp.ExcelStartLine, 10);
                lblExcelFile.Text = Utility.sDbnull(rp.ExcelStartLine, "");
            }
            
            txtTieuDe.Text = THU_VIEN_CHUNG.LaygiatriTieudebaocao(MA_BAOCAO, txtTieuDe.Text, true);
            cmdSave.Visible = globalVariablesPrivate.objNhanvien != null && Utility.Coquyen("quyen_suatieudebaocao");
            hasLoaded = true;
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            //if (!hasLoaded) return;
            //if (globalVariables.IsAdmin || globalVariables.isSuperAdmin)
            //{
            //    OpenFileDialog _opendlg = new OpenFileDialog();
            //    if (_opendlg.ShowDialog() == DialogResult.OK)
            //    {
            //        lblExcelFile.Text = Path.GetFileNameWithoutExtension(_opendlg.FileName);
            //        new Update(SysReport.Schema).Set(SysReport.ExcelFileNameColumn).EqualTo(Path.GetFileNameWithoutExtension(_opendlg.FileName)).Where(SysReport.MaBaocaoColumn).IsEqualTo(MA_BAOCAO).Execute();
            //    }
            //}
            //else
            //    Utility.ShowMsg("Bạn không có quyền sửa thông tin xuất Excel. Liên hệ quản trị hệ thống để được hỗ trợ");
        }

        private void nmrLine_ValueChanged(object sender, EventArgs e)
        {
            //if (!hasLoaded) return;
            //if(globalVariables.IsAdmin || globalVariables.isSuperAdmin)
            //    new Update(SysReport.Schema).Set(SysReport.ExcelStartLineColumn).EqualTo(nmrLine.Value).Where(SysReport.MaBaocaoColumn).IsEqualTo(MA_BAOCAO).Execute();
            //else
            //    Utility.ShowMsg("Bạn không có quyền sửa thông tin xuất Excel. Liên hệ quản trị hệ thống để được hỗ trợ");
        }
    }
}
