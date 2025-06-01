using System;
using System.Drawing;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;
namespace VNS.Libs
{
    public partial class frm_SignInfor : Form
    {
        public bool mv_bChapNhan = false;
        public string mv_sFontName;
        public string mv_sFontSize;
        public string mv_sFontStyle;
        private int w;

        public frm_SignInfor()
        {
            InitializeComponent();
            InitializeEvents();
            //cmdUpdateAllUser.Visible = globalVariables.IsAdmin;     
        }

        // public string mv_sSoLuongCanIn ="1";
        private void cmdQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            //thuc hien lay gia tri
            mv_bChapNhan = true;
            Close();
        }

        private void frm_SignInfor_Load(object sender, EventArgs e)
        {
            try
            {
                w = Width;
                txtBaoCao.ReadOnly = true;
                txtMabaocao.ReadOnly = true;
                //txtBaoCao.BackColor = Color.WhiteSmoke;
                // txtTrinhky.SetFontFamily(new FontFamily("Arial"));
                txtTrinhky.Font = new Font("Arial", 10f, FontStyle.Regular);
            }

            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" +ex.Message);
                //SetLanguage(gv_sLanguageDisplay, this, "GOLFMAN", gv_oSqlCnn);
            }
        }

        private void InitializeEvents()
        {
            Load += frm_SignInfor_Load;
            cmdOK.Click += cmdOK_Click;
            cmdQuit.Click += cmdQuit_Click;
            chkPortrait.CheckedChanged += chkPortrait_CheckedChanged;
        }

        private void chkPortrait_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPortrait.Checked)
                Width = 943;
            else
                Width = w;
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_SignInfor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdQuit.PerformClick();
            if (e.KeyCode == Keys.S && e.Control) cmdOK.PerformClick();
            //if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
        }

        /// <summary>
        /// hàm thực hiện toàn bô các user có dạng báo cáo trình ký
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdUpdateAllUser_Click(object sender, EventArgs e)
        {
            //var frm = new frm_SysUsers();
            //frm.TenBaoCao = txtBaoCao.Text;
            //frm.ShowDialog();
        }

        private void frm_SignInfor_ResizeEnd(object sender, EventArgs e)
        {
            try
            {
                Utility.SetResize(this);
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
        }

        private void cmdOK_Click_1(object sender, EventArgs e)
        {

        }
    }
}