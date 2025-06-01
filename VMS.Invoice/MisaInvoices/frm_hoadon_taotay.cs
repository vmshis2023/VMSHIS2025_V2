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
using VMS.Invoice;
using VNS.Libs;

namespace VNS.HIS.UI.Forms.Dungchung
{
    public partial class frm_hoadon_taotay : Form
    {
        public BuyerInfor _buyer;
       
        private MisaInvoice _MisaInvoices = new MisaInvoice();
        public frm_hoadon_taotay(BuyerInfor _buyer,DataRow dr)
        {
            InitializeComponent();
            this.KeyDown += frm_hoadon_taotay_KeyDown;
            _MisaInvoices._OnStatus += _MisaInvoices__OnStatus;
            Utility.SetVisualStyle(this);
            this._buyer = _buyer;
            setDefaultInfor();
           
            txtSotien.KeyPress += TxtSotien_KeyPress;
            txtSotien.TextChanged += TxtSotien_TextChanged;
            _MisaInvoices.SetMauhoadon(Utility.sDbnull(dr["InvSeries"]), Utility.sDbnull(dr["IPTemplateID"]), Utility.sDbnull(dr["TemplateName"]));
            txtVAT.TextChanged += TxtVAT_TextChanged;
            this.Shown += Frm_hoadon_taotay_Shown;
            this.FormClosing += Frm_hoadon_taotay_FormClosing;
        }
        void LoadUserConfigs()
        {
            try
            {
              
                chkCloseAfterSaving.Checked = Utility.getUserConfigValue(chkCloseAfterSaving.Tag.ToString(), Utility.Bool2byte(chkCloseAfterSaving.Checked)) == 1;

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void SaveUserConfigs()
        {
            try
            {
                Utility.SaveUserConfig(chkCloseAfterSaving.Tag.ToString(), Utility.Bool2byte(chkCloseAfterSaving.Checked));
              

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        private void Frm_hoadon_taotay_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
        }

        private void Frm_hoadon_taotay_Shown(object sender, EventArgs e)
        {
            LoadUserConfigs();
        }

        private void TxtVAT_TextChanged(object sender, EventArgs e)
        {
            txtTienthueGTGT.Text = (Utility.DecimaltoDbnull(txtSotien.Text, 0) * Utility.DecimaltoDbnull(txtVAT.Text, 0) / 100).ToString();
            txtThanhtienDonhang.Text = (Utility.DecimaltoDbnull(txtSotien.Text, 0) * (1m + Utility.DecimaltoDbnull(txtVAT.Text, 0) / 100)).ToString();
            lblTongtien.Text = "Bằng chữ: " + new MoneyByLetter().sMoneyToLetter(txtThanhtienDonhang.Text);
        }

        private void TxtSotien_TextChanged(object sender, EventArgs e)
        {
            txtTienthueGTGT.Text = (Utility.DecimaltoDbnull(txtSotien.Text, 0) * Utility.DecimaltoDbnull(txtVAT.Text, 0) / 100).ToString();
            txtThanhtienDonhang.Text= (Utility.DecimaltoDbnull(txtSotien.Text, 0) *(1m+ Utility.DecimaltoDbnull(txtVAT.Text, 0) / 100)).ToString();
            lblTongtien.Text = "Bằng chữ: " + new MoneyByLetter().sMoneyToLetter(txtThanhtienDonhang.Text);
        }

        private void TxtSotien_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

       

        public frm_hoadon_taotay()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KeyDown += frm_hoadon_taotay_KeyDown;
        }

        private void frm_hoadon_taotay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            else if (e.Control && e.KeyCode == Keys.S) cmdPhathanhHDon.PerformClick();
            else if (e.Control && e.KeyCode == Keys.P) cmdPReview.PerformClick();
            else if (e.KeyCode == Keys.Escape) cmdthoat.PerformClick();
        }
        private void setDefaultInfor()
        {
            try
            {
                txtManguoimua.Text = _buyer.BuyerCode;
                txttencongty.Text = _buyer.BuyerLegalName;
                txtmasothue.Text = _buyer.BuyerTaxCode;
                txtdiachi.Text = _buyer.BuyerAddress;
                txthovaten.Text = _buyer.BuyerFullName;
                txtDienthoai.Text = _buyer.BuyerPhoneNumber;
                txtEmail.Text = _buyer.BuyerEmail;
                txtsotaikhoan.Text = _buyer.BuyerBankAccount;
                txtNganhang.Text = _buyer.BuyerBankName;

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void frm_hoadon_taotay_Load(object sender, EventArgs e)
        {
            txtTenhang.Focus();
        }

        private void cmdthoat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void _MisaInvoices__OnStatus(string status, bool isErr)
        {
            VNS.Libs.AppUI.UIAction.SetTextStatus(lblMsg, status, isErr);
        }
        private void cmdPhathanhHDon_Click(object sender, EventArgs e)
        {
            try
            {

                _buyer.BuyerCode = Utility.DoTrim(txtManguoimua.Text);
                _buyer.BuyerLegalName = Utility.DoTrim(txttencongty.Text);
                _buyer.BuyerTaxCode = Utility.DoTrim(txtmasothue.Text);
                _buyer.BuyerAddress = Utility.DoTrim(txtdiachi.Text);
                _buyer.BuyerFullName = Utility.DoTrim(txthovaten.Text);
                _buyer.BuyerPhoneNumber = Utility.DoTrim(txtDienthoai.Text);
                _buyer.BuyerEmail = Utility.DoTrim(txtEmail.Text);
                _buyer.BuyerBankAccount = Utility.DoTrim(txtsotaikhoan.Text);
                _buyer.BuyerBankName = Utility.DoTrim(txtNganhang.Text);
                _buyer.BuyerIDNumber = "";
                //Thông tin hàng hóa
                _buyer.TenHangHoa = Utility.sDbnull(txtTenhang.Text);
                _buyer.Tongtienhang = Utility.DecimaltoDbnull(txtSotien.Text);
                _buyer.VAT = Utility.DecimaltoDbnull(txtVAT.Text, 0) <= 0 ? "KCT" : Utility.DoTrim( txtVAT.Text) + "%";
                _buyer.TongtienThue = Utility.DecimaltoDbnull(txtTienthueGTGT.Text);
                _buyer.ThanhtienDonhang = Utility.DecimaltoDbnull(txtThanhtienDonhang.Text);
                if (_buyer.BuyerFullName.Length <= 0)
                {
                    Utility.ShowMsg("Bạn cần nhập họ tên người mua");
                    txthovaten.Focus();
                    return;
                }
                if (_buyer.BuyerAddress.Length <= 0)
                {
                    Utility.ShowMsg("Bạn cần nhập địa chỉ người mua");
                    txtdiachi.Focus();
                    return;
                }
                string eMessage = "";
              bool  kt = _MisaInvoices.phathanh_hoadon(_buyer, ref eMessage);
                if (kt)
                {
                                    }
                else
                {
                    
                }
                if (chkCloseAfterSaving.Checked)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void cmdPReview_Click(object sender, EventArgs e)
        {
            try
            {

                _buyer.BuyerCode = Utility.DoTrim(txtManguoimua.Text);
                _buyer.BuyerLegalName = Utility.DoTrim(txttencongty.Text);
                _buyer.BuyerTaxCode = Utility.DoTrim(txtmasothue.Text);
                _buyer.BuyerAddress = Utility.DoTrim(txtdiachi.Text);
                _buyer.BuyerFullName = Utility.DoTrim(txthovaten.Text);
                _buyer.BuyerPhoneNumber = Utility.DoTrim(txtDienthoai.Text);
                _buyer.BuyerEmail = Utility.DoTrim(txtEmail.Text);
                _buyer.BuyerBankAccount = Utility.DoTrim(txtsotaikhoan.Text);
                _buyer.BuyerBankName = Utility.DoTrim(txtNganhang.Text);
                _buyer.BuyerIDNumber = "";
                //Thông tin hàng hóa
                _buyer.TenHangHoa = Utility.sDbnull(txtTenhang.Text);
                _buyer.Tongtienhang = Utility.DecimaltoDbnull(txtSotien.Text);
                _buyer.VAT = Utility.DecimaltoDbnull(txtVAT.Text, 0) <= 0 ?"KCT" : Utility.DoTrim(txtVAT.Text) + "%";
                _buyer.TongtienThue = Utility.DecimaltoDbnull(txtTienthueGTGT.Text);
                _buyer.ThanhtienDonhang = Utility.DecimaltoDbnull(txtThanhtienDonhang.Text);
                if (_buyer.BuyerFullName.Length <= 0)
                {
                    Utility.ShowMsg("Bạn cần nhập họ tên người mua");
                    txthovaten.Focus();
                    return;
                }
                if (_buyer.BuyerAddress.Length <= 0)
                {
                    Utility.ShowMsg("Bạn cần nhập địa chỉ người mua");
                    txtdiachi.Focus();
                    return;
                }
                string eMessage = "";
                 _MisaInvoices.xemtruoc_hoadon(_buyer, ref eMessage);
                   
                
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void nmrVAT_ValueChanged(object sender, EventArgs e)
        {
           
        }
    }
}
