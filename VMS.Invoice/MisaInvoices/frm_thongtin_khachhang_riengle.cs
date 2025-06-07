using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;
using VMS.Invoice;
using VNS.Libs;

namespace VNS.HIS.UI.Forms.Dungchung
{
    public partial class frm_thongtin_khachhang_riengle : Form
    {
        public BuyerInfor _buyer;
        DataRow dr;
        string str_IdThanhtoan = "";
             string str_IdThanhtoanChitiet = "";
        byte kieuxuat = 0;
        MisaInvoice _MisaInvoices;
        KcbThanhtoan objThanhtoan;
        public frm_thongtin_khachhang_riengle(MisaInvoice _MisaInvoices, BuyerInfor _buyer, DataRow dr, byte kieuxuat, string str_IdThanhtoan, string str_IdThanhtoanChitiet)
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
            this.KeyDown += frm_thongtin_khachhang_riengle_KeyDown;
            _MisaInvoices._OnStatus += _MisaInvoices__OnStatus;
            Utility.SetVisualStyle(this);
            this._buyer = _buyer;
            this.dr = dr;
            this.str_IdThanhtoan = str_IdThanhtoan;
            this.str_IdThanhtoanChitiet = str_IdThanhtoanChitiet;
            this.kieuxuat = kieuxuat;
            this._MisaInvoices = _MisaInvoices;
            setDefaultInfor();

            this.Shown += frm_thongtin_khachhang_riengle_Shown;
            this.FormClosing += frm_thongtin_khachhang_riengle_FormClosing;
        }
        void LoadUserConfigs()
        {
            try
            {
              
               

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
              

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        private void frm_thongtin_khachhang_riengle_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
        }

        private void frm_thongtin_khachhang_riengle_Shown(object sender, EventArgs e)
        {
            LoadUserConfigs();
        }

       

        public frm_thongtin_khachhang_riengle()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KeyDown += frm_thongtin_khachhang_riengle_KeyDown;
        }

        private void frm_thongtin_khachhang_riengle_KeyDown(object sender, KeyEventArgs e)
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
                txtManguoimua.Text = _buyer.Id_benhnhan.ToString();
                txttencongty.Text = _buyer.BuyerLegalName;
                txthovaten.Text = _buyer.BuyerFullName;
                txtEmail.Text = _buyer.BuyerEmail;
                txtTennguoinhan.Text = _buyer.BuyerFullName;

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        string _file = Application.StartupPath + @"\KihieuHdon.txt";
        private void frm_thongtin_khachhang_riengle_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtMauHoadon = Utility.ExecuteSql("select * from hoadon_mau_misa where isActive=1", CommandType.Text).Tables[0];
                DataBinding.BindDataCombobox(cboSeries, dtMauHoadon,
                                    HoadonMauMisa.Columns.InvSeries, HoadonMauMisa.Columns.InvSeries, "", true);
                if (cboSeries.Items.Count == 1)
                    cboSeries.SelectedIndex = 0;
                else if (cboSeries.Items.Count > 1)
                {
                    if (File.Exists(_file))
                    {
                        string kihieuhoadon =Utility.sDbnull( File.ReadAllText(_file));
                        if (kihieuhoadon != "")
                            cboSeries.SelectedValue = kihieuhoadon;
                        else
                            cboSeries.SelectedIndex = 0;
                    }
                    else
                        cboSeries.SelectedIndex = 0;
                }
                objThanhtoan = KcbThanhtoan.FetchByID(Utility.Int64Dbnull(str_IdThanhtoan));
                dtpNgayhoadon.Value = DateTime.Now;
                if (objThanhtoan != null)
                {
                    dtpNgaythanhtoan.Value = objThanhtoan.NgayThanhtoan;
                    dtpNgayhoadon.Value= objThanhtoan.NgayThanhtoan;
                }
               
                txttencongty.Focus();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
          
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

               
                _buyer.BuyerLegalName = Utility.DoTrim(txttencongty.Text);
               
                _buyer.BuyerEmail = Utility.DoTrim(txtEmail.Text);
                _buyer.IsSendEmail = chkSendEmail.Checked;
                _buyer.ReceiverEmail = Utility.DoTrim(txtCC.Text);
                _buyer.ReceiverName = Utility.DoTrim(txtTennguoinhan.Text);
                if (chkSendEmail.Checked)
                {
                    if (_buyer.BuyerEmail.Split(';').Length>1)
                    {
                        Utility.ShowMsg("Mục Email chỉ được nhập duy nhất 1 email nhận chính. Muốn gửi nhiều email thì nhập các email khác ở mục CC và cách nhau bởi dấu ;");
                        txtEmail.Focus();
                        return;
                    }
                    if (_buyer.ReceiverName.Length <= 0)
                    {
                        Utility.ShowMsg("Bạn cần nhập họ tên người nhận");
                        txtTennguoinhan.Focus();
                        return;
                    }
                    if (_buyer.ReceiverEmail.Length <= 0)
                    {
                        Utility.ShowMsg("Bạn cần nhập email người nhận. Các email cách nhau bởi dấy chấm phẩy ;");
                        txtEmail.Focus();
                        return;
                    }
                }
                if(dtpNgayhoadon.Value.Date<dtpNgaythanhtoan.Value.Date)
                {
                    Utility.ShowMsg("Ngày hóa đơn phải >= ngày chứng từ");
                    dtpNgayhoadon.Focus();
                    return;
                }    
                string eMessage = "";
                
                    bool kt = false;
                _MisaInvoices._buyer = _buyer;
                if (kieuxuat == 0)
                    kt = _MisaInvoices.phathanh_hoadon(str_IdThanhtoan, 0, str_IdThanhtoanChitiet, ref eMessage);// _MisaInvoices.phathanh_hoadon(_buyer, ref eMessage);
                else
                    kt = _MisaInvoices.phathanh_hoadon(str_IdThanhtoan, 1, str_IdThanhtoanChitiet, ref eMessage);
                if (kt)
                {
                    this.DialogResult = DialogResult.OK;
                    cmdPhathanhHDon.Enabled = false;
                    cmdPReview.Enabled = false;
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

                _buyer.BuyerLegalName = Utility.DoTrim(txttencongty.Text);

                _buyer.BuyerEmail = Utility.DoTrim(txtEmail.Text);
                _buyer.IsSendEmail = chkSendEmail.Checked;
                _buyer.ReceiverEmail = Utility.DoTrim(txtCC.Text);
                _buyer.ReceiverName = Utility.DoTrim(txtTennguoinhan.Text);
                if (chkSendEmail.Checked)
                {
                    if (_buyer.ReceiverName.Length <= 0)
                    {
                        Utility.ShowMsg("Bạn cần nhập họ tên người nhận");
                        txtTennguoinhan.Focus();
                        return;
                    }
                    if (_buyer.ReceiverEmail.Length <= 0)
                    {
                        Utility.ShowMsg("Bạn cần nhập email người nhận. Các email cách nhau bởi dấy chấm phẩy ;");
                        txtEmail.Focus();
                        return;
                    }
                }
                string eMessage = "";
                _MisaInvoices._buyer = _buyer;
                _MisaInvoices.xemtruoc_hoadon(str_IdThanhtoan, 0, str_IdThanhtoanChitiet, ref eMessage);// _MisaInvoices.xemtruoc_hoadon(_buyer, ref eMessage);
                   
                
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void nmrVAT_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtNganhang_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkSendEmail_CheckedChanged(object sender, EventArgs e)
        {
            txtTennguoinhan.Enabled = txtEmail.Enabled =txtCC.Enabled= chkSendEmail.Checked;
            txtTennguoinhan.Focus();
        }

        private void cboSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Utility.SaveValue2File(_file,Utility.sDbnull( cboSeries.SelectedValue));
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
    }
}
