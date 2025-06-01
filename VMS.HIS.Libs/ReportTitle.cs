using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VMS.HIS.DAL;
using SubSonic;

namespace VNS.Libs
{
    public partial class ReportTitle : UserControl
    {
        public ReportTitle()
        {
            InitializeComponent();
            txtTieuDe.LostFocus += new EventHandler(txtTieuDe_LostFocus);
            txtTieuDe.GotFocus += new EventHandler(txtTieuDe_GotFocus);
            txtTieuDe.KeyDown += new KeyEventHandler(txtTieuDe_KeyDown);
           
            cmdSave.Click += new EventHandler(cmdSave_Click);
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
                CapnhatgiatriTieudebaocao(MA_BAOCAO, Utility.DoTrim(txtTieuDe.Text));
        }
        void cmdSave_Click(object sender, EventArgs e)
        {
            CapnhatgiatriTieudebaocao(MA_BAOCAO, Utility.DoTrim(txtTieuDe.Text));
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
            txtTieuDe.Text = LaygiatriTieudebaocao(MA_BAOCAO, txtTieuDe.Text, true);
        }
        public void Init()
        {
            cmdSave.Visible = cmdSave.Visible = globalVariables.IsAdmin || (globalVariablesPrivate.objNhanvien != null && Utility.Coquyen("quyen_suatieudebaocao"));
            txtTieuDe.Text = LaygiatriTieudebaocao(MA_BAOCAO, txtTieuDe.Text, true);
        }
          void CapnhatgiatriTieudebaocao(string Matieude, string _value)
        {
            try
            {
                if (Utility.DoTrim(Matieude) == "") return;
                DataRow[] arrDR = globalVariables.gv_dtSysTieude.Select(SysTieude.MaTieudeColumn.ColumnName + " ='" + Matieude + "'");
                SysReport _Item = new Select().From(SysReport.Schema).Where(SysReport.Columns.MaBaocao).IsEqualTo(
                            Matieude).ExecuteSingle<SysReport>();
                if (_Item != null)
                {
                    //arrDR[0][SysTieude.NoiDungColumn.ColumnName] = _value;
                    //globalVariables.gv_dtSysTieude.AcceptChanges();
                    new Update(SysReport.Schema).Set(SysReport.TieuDeColumn).EqualTo(_value).Where(SysReport.MaBaocaoColumn).IsEqualTo(Matieude).Execute();
                }
                else
                {
                    SysReport newItem = new SysReport();
                    newItem.MaBaocao = Matieude;
                    newItem.TieuDe = _value;

                    newItem.Save();
                    //DataRow newrow = globalVariables.gv_dtSysTieude.NewRow();
                    //newrow[SysTieude.MaTieudeColumn.ColumnName] = Matieude;
                    //newrow[SysTieude.NoiDungColumn.ColumnName] = _value;

                    //globalVariables.gv_dtSysTieude.Rows.Add(newrow);
                    //globalVariables.gv_dtSysTieude.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi cập nhật giá trị tiêu đề báo cáo:\n" + ex.Message);
            }
        }
        string LaygiatriTieudebaocao(string Matieude, string defaultval, bool fromDB)
        {
            try
            {
                string reval = defaultval;
                if (fromDB)
                {
                    SqlQuery sqlQuery =
                        new Select().From(SysReport.Schema).Where(SysReport.Columns.MaBaocao).IsEqualTo(
                            Matieude);
                    SysReport objSystemParameter = sqlQuery.ExecuteSingle<SysReport>();
                    if (objSystemParameter != null) reval = objSystemParameter.TieuDe;
                }
                else
                {
                    DataRow[] arrDR = globalVariables.gv_dtSysTieude.Select(SysTieude.MaTieudeColumn.ColumnName + " ='" + Matieude + "'");
                    if (arrDR.Length > 0) reval = Utility.sDbnull(arrDR[0][SysTieude.NoiDungColumn.ColumnName]);
                }
                return reval;
            }
            catch
            {
                return defaultval;
            }
        }
          string LaygiatriTieudeBaocao(string Matieude, bool fromDB)
        {
            try
            {
                string reval = "";
                if (fromDB)
                {
                    SqlQuery sqlQuery =
                        new Select().From(SysReport.Schema).Where(SysReport.Columns.MaBaocao).IsEqualTo(
                            Matieude);
                    SysReport objSystemParameter = sqlQuery.ExecuteSingle<SysReport>();
                    if (objSystemParameter != null) reval = objSystemParameter.TieuDe;
                }
                else
                {
                    DataRow[] arrDR = globalVariables.gv_dtSysTieude.Select(SysTieude.MaTieudeColumn.ColumnName + " ='" + Matieude + "'");
                    if (arrDR.Length > 0) reval = Utility.sDbnull(arrDR[0][SysTieude.NoiDungColumn.ColumnName]);
                }
                return reval;
            }
            catch
            {
                return "";
            }
        }

          private void cmdSave_Click_1(object sender, EventArgs e)
          {

          }
    }
}
