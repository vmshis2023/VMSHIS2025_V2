using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.HIS.UI.NGOAITRU;
using SubSonic;
using VNS.Libs;
using VNS.HIS.BusRule.Classes;
using VNS.Properties;
using VNS.HIS.UI.DANHMUC;
using Janus.Windows.GridEX.EditControls;
using Janus.Windows.EditControls;

namespace VNS.HIS.UI.Forms.Cauhinh
{
    public partial class frm_ChonLydohuyTUHU : Form
    {
      
        public string ma="";
        public string ten = "";
        public bool m_blnCancel = true;
        string _name = "";
        List<string> lstPTTTBatchonNganhang = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
        List<string> lstPhanbo = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_BATBUOCPHANBO", false).Split(',').ToList<string>();
        public frm_ChonLydohuyTUHU(string loaidanhmuc,string title1,string title2,string _name,string ngaytao)
        {
            InitializeComponent();
            dtNgayHuy.Value = DateTime.Now;
            this.Text = title1;
            txtLydoHuy.LOAI_DANHMUC = loaidanhmuc;
            lblTitle1.Text = title1;
            lblTitle2.Text = title2;
            lblName.Text = _name;
            this._name = _name;
            lblNgayTUHU.Text = ngaytao;
            InitEvents();
        }
        void InitEvents()
        {
            this.FormClosing += new FormClosingEventHandler(frm_ChonLydohuyTUHU_FormClosing);
            this.Load += new EventHandler(frm_ChonLydohuyTUHU_Load);
            this.KeyDown += new KeyEventHandler(frm_ChonLydohuyTUHU_KeyDown);
            txtLydoHuy._OnShowData += txtDmucchung__OnShowData;
            cmdClose.Click+=new EventHandler(cmdClose_Click);
            cmdSave.Click+=new EventHandler(cmdSave_Click);
        }

        void txtDmucchung__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtLydoHuy.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtLydoHuy.myCode;
                txtLydoHuy.Init();
                txtLydoHuy.SetCode(oldCode);
                txtLydoHuy.Focus();
            } 
        }

        void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void frm_ChonLydohuyTUHU_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Escape)
                cmdClose_Click(cmdClose, new EventArgs());
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }

        void frm_ChonLydohuyTUHU_Load(object sender, EventArgs e)
        {
            try
            {
                dtNgayHuy.Enabled = Utility.Coquyen("tamung_huy_chonngay");
                LoadPtttNganhang();
                txtLydoHuy.Init();
                txtLydoHuy.Focus();
                txtLydoHuy.Select();
            }
            catch
            {
            }
            finally
            {
            }
        }
        DataTable dtPttt = new DataTable();
        DataTable dtPttt_rieng = new DataTable();
        DataTable dtNganhang = new DataTable();
        void LoadPtttNganhang()
        {
            DataTable dtData = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { "PHUONGTHUCTHANHTOAN", "NGANHANG" }, true);
            dtPttt = THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, "PHUONGTHUCTHANHTOAN");
            dtNganhang = THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, "NGANHANG");
            dtPttt_rieng = getPTTT();
            cboPttt.DataSource = dtPttt_rieng;
            cboPttt.ValueMember = DmucChung.Columns.Ma;
            cboPttt.DisplayMember = DmucChung.Columns.Ten;
            cboNganhang.DataSource = dtNganhang;
            cboNganhang.ValueMember = DmucChung.Columns.Ma;
            cboNganhang.DisplayMember = DmucChung.Columns.Ten;
            cboPttt.SelectedValue = THU_VIEN_CHUNG.Laygiatrimacdinh(dtPttt_rieng);
            cboNganhang.SelectedValue = THU_VIEN_CHUNG.Laygiatrimacdinh(dtNganhang);
        }

        DataTable getPTTT()
        {
            DataTable dt = dtPttt.Clone();
            foreach (DataRow dr in dtPttt.Rows)
            {
                if (!lstPhanbo.Contains(Utility.sDbnull(dr["MA"], "")))
                    dt.ImportRow(dr);
            }
            return dt;
        }

        void frm_ChonLydohuyTUHU_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
       

        void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.EnableButton(cmdSave, false);
                if (!isValidData()) return;
                ma = txtLydoHuy.myCode;
                ten = Utility.DoTrim(txtLydoHuy.Text);
                m_blnCancel = false;
                this.Close();
            }
            catch (Exception ex)
            {
                Utility.EnableButton(cmdSave, true);
                Utility.ShowMsg("Lỗi khi nhấn nút chấp nhận:\n" + ex.Message);
                throw;
            }
            finally
            {
                Utility.EnableButton(cmdSave, true);
            }
        }
     
        private bool isValidData()
        {
            Utility.SetMsg(lblMsg, "", true);
            if (dtNgayHuy.Value.Date < dtNgayTUHU.Value.Date)
            {
                Utility.SetMsg(lblMsg, "Ngày hủy phải >= ngày tạo. Vui lòng kiểm tra lại ", true);
                dtNgayHuy.Focus();
                return false;
            }
            if (txtLydoHuy.myCode == "-1" || Utility.DoTrim(txtLydoHuy.myCode) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập "+_name, true);
                txtLydoHuy.Focus();
                txtLydoHuy.SelectAll();
                return false;
            }
            if (Utility.sDbnull(cboPttt.SelectedValue, "") == "")
            {
                Utility.ShowMsg(string.Format("Bạn phải chọn Hình thức thanh toán"));
                cboPttt.Focus();
                return false;
            }
            if (cboNganhang.Enabled && Utility.sDbnull(cboNganhang.SelectedValue, "") == "")
            {
                Utility.ShowMsg(string.Format("Bạn phải chọn ngân hàng khi chọn phương thức thanh toán {0}", cboPttt.Text));
                cboNganhang.Focus();
                return false;
            }
            return true;
        }
        public string ma_pttt = "";
        private void cboPttt_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ma_pttt = Utility.sDbnull(cboPttt.SelectedValue);
            cboNganhang.Enabled = lstPTTTBatchonNganhang.Contains(this.ma_pttt);
        }
       
    }
}
