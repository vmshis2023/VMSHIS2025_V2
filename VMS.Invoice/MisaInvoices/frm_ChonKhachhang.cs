using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.UI.Forms.Dungchung
{
    public partial class frm_ChonKhachhang : Form
    {
        List<long> lstIdBenhnhan = new List<long>();
        public string BuyerCode = "";
        public string BuyerLegalName = "";
        public string BuyerTaxCode = "";
        public string BuyerAddress = "";
        public string BuyerFullName = "";
        public string BuyerPhoneNumber = "";
        public string BuyerEmail = "";
        public string BuyerBankAccount = "";
        public string BuyerBankName = "";
        public string BuyerIDNumber = "";
        public frm_ChonKhachhang(List<long> lstIdBenhnhan)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.lstIdBenhnhan = lstIdBenhnhan;
        }
        public frm_ChonKhachhang()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            grdList.SelectionChanged += GrdList_SelectionChanged;
            grdList.ColumnButtonClick += GrdList_ColumnButtonClick;
            grdList.MouseDoubleClick += GrdList_MouseDoubleClick;
            this.KeyDown += Frm_ChonKhachhang_KeyDown;
        }

        private void Frm_ChonKhachhang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            else if (e.Control && e.KeyCode == Keys.S) cmdCapnhat.PerformClick();
            else if (e.KeyCode == Keys.Escape) cmdthoat.PerformClick();
        }

        private void GrdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            chkKhac.Checked = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void GrdList_ColumnButtonClick(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            chkKhac.Checked = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void GrdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

                string thue_diachi = Utility.sDbnull(grdList.GetValue("thue_diachi"), "");
                string dia_chi = Utility.sDbnull(grdList.GetValue("dia_chi"), "");
                BuyerCode = Utility.sDbnull(grdList.GetValue("ma_luotkham"), "");
                BuyerLegalName = Utility.sDbnull(grdList.GetValue("ten_benhnhan"), "");
                BuyerTaxCode = Utility.sDbnull(grdList.GetValue("thue_maso"), "");
                BuyerAddress = thue_diachi.Length <= 0 ? dia_chi : thue_diachi;
                BuyerFullName = Utility.sDbnull(grdList.GetValue("ten_benhnhan"), "");
                BuyerPhoneNumber = Utility.sDbnull(grdList.GetValue("dien_thoai"), "");
                BuyerEmail = Utility.sDbnull(grdList.GetValue("email"), "");
                BuyerBankAccount = "";
                BuyerBankName = "";
                BuyerIDNumber = "";
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Chọn người mua từ danh sách với BuyerCode={0}," +
                   " BuyerLegalName={1},BuyerTaxCode={2}, BuyerAddress={3},BuyeBuyerFullNamerCode={4}, " +
                   "BuyerPhoneNumber={5},BuyerEmail={6}, BuyerBankAccount={7}, BuyerBankName={8}"
                   , BuyerCode, BuyerLegalName, BuyerTaxCode, BuyerAddress, BuyerFullName, BuyerPhoneNumber, BuyerEmail, BuyerBankAccount, BuyerBankName), newaction.Update, "UI");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void frm_ChonKhachhang_Load(object sender, EventArgs e)
        {
            DataTable dtData = new Select().From(KcbDanhsachBenhnhan.Schema).Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan).In(lstIdBenhnhan).ExecuteDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, dtData, true, true, "1=1", "ten_benhnhan asc");
        }

        private void cmdthoat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmdCapnhat_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkKhac.Checked)
                {
                   
                    BuyerCode = Utility.DoTrim(txtManguoimua.Text);
                    BuyerLegalName = Utility.DoTrim(txttencongty.Text);
                    BuyerTaxCode = Utility.DoTrim(txtmasothue.Text);
                    BuyerAddress = Utility.DoTrim(txtdiachi.Text);
                    BuyerFullName = Utility.DoTrim(txthovaten.Text);
                    BuyerPhoneNumber = Utility.DoTrim(txtDienthoai.Text);
                    BuyerEmail = Utility.DoTrim(txtEmail.Text);
                    BuyerBankAccount = Utility.DoTrim(txtsotaikhoan.Text);
                    BuyerBankName = Utility.DoTrim(txtNganhang.Text);
                    BuyerIDNumber = "";
                    if (BuyerFullName.Length<=0)
                    {
                        Utility.ShowMsg("Bạn cần nhập họ tên người mua");
                        return;
                    }
                    if (BuyerAddress.Length <= 0)
                    {
                        Utility.ShowMsg("Bạn cần nhập địa chỉ người mua");
                        return;
                    }
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Nhập tin người mua khác với BuyerCode={0}," +
                        " BuyerLegalName={1},BuyerTaxCode={2}, BuyerAddress={3},BuyeBuyerFullNamerCode={4}, " +
                        "BuyerPhoneNumber={5},BuyerEmail={6}, BuyerBankAccount={7}, BuyerBankName={8}"
                        , BuyerCode, BuyerLegalName, BuyerTaxCode, BuyerAddress, BuyerFullName, BuyerPhoneNumber, BuyerEmail, BuyerBankAccount, BuyerBankName), newaction.Update, "UI");
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void chkKhac_CheckedChanged(object sender, EventArgs e)
        {
            txtNganhang.Enabled = txtManguoimua.Enabled = txtDienthoai.Enabled = txtdiachi.Enabled = txthovaten.Enabled = txtmasothue.Enabled = txtsotaikhoan.Enabled = txttencongty.Enabled = txtEmail.Enabled = chkKhac.Enabled;
        }
    }
}
