using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using SubSonic;
using VMS.HIS.DAL;

using VNS.Libs;
using TextAlignment = Janus.Windows.GridEX.TextAlignment;
using System.IO;

namespace VNS.HIS.UI.NGOAITRU
{
    public partial class frm_ThemnoiKCBBD : Form
    {
        public DataTable m_dtDataThanhPho = new DataTable();
        public frm_ThemnoiKCBBD()
        {
            InitializeComponent();
            this.Load += new EventHandler(frm_ThemnoiKCBBD_Load);
            txtMaThanhPho.TextChanged += new EventHandler(txtMaThanhPho_TextChanged);
            this.KeyDown += new KeyEventHandler(frm_ThemnoiKCBBD_KeyDown);
            txtTen.KeyDown += new KeyEventHandler(txtTen_KeyDown);
            
           
        }

        void txtMaThanhPho_TextChanged(object sender, EventArgs e)
        {
            DataRow[] arrDR = m_dtDataThanhPho.Select("ma_diachinh='" + txtMaThanhPho.Text.Trim() + "'");
            if (arrDR.Length > 0)
                txtTenThanhPho.Text = Utility.sDbnull(arrDR[0]["ten_diachinh"], "");
            else
                txtTenThanhPho.Text = "";
        }

       
        void txtTen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtTen.Text.Trim()!="")
            {
                cmdAccept_Click(cmdAccept, new EventArgs());
            }
        }

        void frm_ThemnoiKCBBD_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        void frm_ThemnoiKCBBD_Load(object sender, EventArgs e)
        {
            txtTen.Focus();
        }
        public void SetInfor(string MA, string NOI_CAP)
        {
           
            txtMa.Text = MA;
            txtMaThanhPho.Text = NOI_CAP;
            
        }
       

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            try
            {
                QueryCommand cmd = DmucNoiKCBBD.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandSql = "Select * from dmuc_noiKCBBD WHERE ma_kcbbd='" + txtMa.Text + "' AND ma_diachinh='" + txtMaThanhPho.Text.Trim() + "'";
                DataTable temdt = DataService.GetDataSet(cmd).Tables[0];
                if (temdt.Rows.Count <= 0)
                {
                    DmucNoiKCBBD newItem = new DmucNoiKCBBD();
                    newItem.MaDiachinh = txtMaThanhPho.Text.Trim();
                    newItem.MaKcbbd = txtMa.Text.Trim();
                    newItem.TenKcbbd = txtTen.Text.Trim();
                    newItem.SttHthi = 9999;
                    newItem.IsNew = true;
                    newItem.Save();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    Utility.ShowMsg(string.Format("Mã {0}: đang thuộc về nơi KCBBĐ {1}. Mời bạn kiểm tra lại", txtMa, Utility.sDbnull(temdt.Rows[0]["ten_kcbbd"].ToString(), "")));
                    txtMa.Focus();
                }
            }
            catch
            {
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
