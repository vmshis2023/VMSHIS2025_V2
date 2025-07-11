using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX.EditControls;
using SubSonic;
using VMS.HIS.DAL;
using VMS.Invoice;
using VNS.Libs;

namespace VNS.HIS.UI.Forms.Dungchung
{
    public partial class frm_hoadon_taotay_v3 : Form
    {
        public BuyerInfor _buyer;
        DataRow dr;
        DataTable dtData = new DataTable();
        MisaInvoice _MisaInvoices;
        KcbThanhtoan objThanhtoan;
        public frm_hoadon_taotay_v3(MisaInvoice _MisaInvoices, BuyerInfor _buyer, DataRow dr)
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
            this.KeyDown += frm_hoadon_taotay_v3_KeyDown;
            _MisaInvoices._OnStatus += _MisaInvoices__OnStatus;
            Utility.SetVisualStyle(this);
            this._buyer = _buyer;
            this.dr = dr;
            this._MisaInvoices = _MisaInvoices;
            _MisaInvoices.SetMauhoadon(Utility.sDbnull(dr["InvSeries"]), Utility.sDbnull(dr["IPTemplateID"]), Utility.sDbnull(dr["TemplateName"]));
            txtCCCD.TextChanged += TxtCC_TextChanged;
            setDefaultInfor();
            txtDongia.KeyPress += TxtSotien_KeyPress;
            txtDongia.TextChanged += TxtSotien_TextChanged;
            txtVAT.TextChanged += TxtVAT_TextChanged;
            this.Shown += frm_hoadon_taotay_v3_Shown;
            this.FormClosing += frm_hoadon_taotay_v3_FormClosing;
            grdChitietThanhtoan.ColumnButtonClick += GrdChitietThanhtoan_ColumnButtonClick;
            dtData.Columns.AddRange(new DataColumn[] {new DataColumn("id",typeof(string)) , new DataColumn("ten_hang", typeof(string)) , new DataColumn("donvitinh", typeof(string))
                , new DataColumn("soluong", typeof(int))             ,new DataColumn("dongia",typeof(decimal)),new DataColumn("thanhtien",typeof(decimal))
                ,new DataColumn("VATName",typeof(string)) ,new DataColumn("VAT",typeof(int)),new DataColumn("tienVAT",typeof(decimal))
            ,new DataColumn("tongtien",typeof(decimal))
            });
            grdChitietThanhtoan.UpdatingCell += GrdChitietThanhtoan_UpdatingCell;
            mnu0.Click += Mnu0_Click;
            mnu5.Click += Mnu0_Click;
            mnu7.Click += Mnu0_Click;
            mnu8.Click += Mnu0_Click;
            mnu10.Click += Mnu0_Click;
            mnu12.Click += Mnu0_Click;
            nmrSoluong.GotFocus += numericUpDown1_GotFocus;
            nmrSoluong.MouseUp += numericUpDown1_MouseUp;
            nmrSoluong.Leave += numericUpDown1_Leave;
        }


        private bool _selectAllDone = false;

        private void numericUpDown1_GotFocus(object sender, EventArgs e)
        {
            // focus từ bàn phím (TAB), chưa có mouse event, chọn toàn bộ
            nmrSoluong.Select(0, nmrSoluong.Text.Length);
            _selectAllDone = true;
        }

        private void numericUpDown1_MouseUp(object sender, MouseEventArgs e)
        {
            // chọn toàn bộ sau khi click, tránh lặp lại nếu đã chọn
            if (!_selectAllDone)
            {
                nmrSoluong.Select(0, nmrSoluong.Text.Length);
                _selectAllDone = true;
            }
        }

        private void numericUpDown1_Leave(object sender, EventArgs e)
        {
            _selectAllDone = false; // reset khi rời khỏi control
        }
        private void Mnu0_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem tsi = sender as ToolStripItem;
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn áp VAT {0} cho toàn bộ các mặt hàng hay không?", tsi.Text), "Xác nhận thay đổi VAT", true)) return;
                int VAT = Utility.Int32Dbnull(tsi.Tag, 0);
                foreach(DataRow dr in dtData.Rows)
                {
                    decimal thanhtien = Utility.DecimaltoDbnull(dr["thanhtien"], 0);
                    decimal tienVAT = thanhtien * Utility.DecimaltoDbnull(VAT, 0) / 100;
                    decimal tongtien = thanhtien + tienVAT;

                    dr["VAT"] = VAT;
                    dr["VATName"] = VAT <= 0 ? "KCT" : VAT.ToString() + "%";
                    dr["tienVAT"] = tienVAT;
                    dr["tongtien"] = tongtien;
                }    
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void GrdChitietThanhtoan_UpdatingCell(object sender, Janus.Windows.GridEX.UpdatingCellEventArgs e)
        {
            try
            {
              
                if(e.Column.Key=="soluong")
                {
                    int soluong = Utility.Int32Dbnull(e.Value);
                    if(soluong<=0)
                    {
                        e.Value = e.InitialValue;
                    }
                    else
                    {
                        decimal thanhtien = Utility.DecimaltoDbnull(grdChitietThanhtoan.GetValue("dongia"), 0) * Utility.DecimaltoDbnull(soluong, 0);
                        decimal tienVAT = thanhtien * Utility.DecimaltoDbnull(grdChitietThanhtoan.GetValue("VAT"), 0) / 100;
                        decimal tongtien = thanhtien + tienVAT;
                        grdChitietThanhtoan.CurrentRow.BeginEdit();
                        grdChitietThanhtoan.CurrentRow.Cells["thanhtien"].Value = thanhtien;
                        grdChitietThanhtoan.CurrentRow.Cells["tienVAT"].Value = tienVAT;
                        grdChitietThanhtoan.CurrentRow.Cells["tongtien"].Value = tongtien;
                        grdChitietThanhtoan.CurrentRow.EndEdit();
                    }    
                }
                grdChitietThanhtoan.Refetch();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void GrdChitietThanhtoan_ColumnButtonClick(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            if (e.Column.Key == "XOA")
            {
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa {0} khỏi danh sách mặt hàng", grdChitietThanhtoan.GetValue("ten_hang").ToString()), "", true))
                    DeleteRowsById(grdChitietThanhtoan.GetValue("id").ToString());
            }
        }
        public void DeleteRowsById( string idValue)
        {
            dtData.AsEnumerable()
              .Where(r => r.Field<string>("id") == idValue)
              .ToList()
              .ForEach(r => r.Delete());
            dtData.AcceptChanges();
        }
        private void TxtVAT_TextChanged(object sender, EventArgs e)
        {
            txtTienVAT.Text = (Utility.DecimaltoDbnull(txtThanhtien.Text, 0) * Utility.DecimaltoDbnull(txtVAT.Text, 0) / 100).ToString();
            txtTongtien.Text = (Utility.DecimaltoDbnull(txtThanhtien.Text, 0) * (1m + Utility.DecimaltoDbnull(txtVAT.Text, 0) / 100)).ToString();
            lblTongtien.Text = "Bằng chữ: " + new MoneyByLetter().sMoneyToLetter(txtTongtien.Text);
        }

        private void TxtSotien_TextChanged(object sender, EventArgs e)
        {
            txtThanhtien.Text = (Utility.DecimaltoDbnull(txtDongia.Text, 0) * Utility.DecimaltoDbnull(nmrSoluong.Text, 0)).ToString();
            txtTienVAT.Text = (Utility.DecimaltoDbnull(txtThanhtien.Text, 0) * Utility.DecimaltoDbnull(txtVAT.Text, 0) / 100).ToString();
            txtTongtien.Text = (Utility.DecimaltoDbnull(txtThanhtien.Text, 0) * (1m + Utility.DecimaltoDbnull(txtVAT.Text, 0) / 100)).ToString();
            lblTongtien.Text = "Bằng chữ: " + new MoneyByLetter().sMoneyToLetter(txtTongtien.Text);
        }

        private void TxtSotien_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }
        private void TxtCC_TextChanged(object sender, EventArgs e)
        {
            lblCCCDLength.Text = Utility.sDbnull(txtCCCD.Text).Length.ToString();
        }

        void LoadUserConfigs()
        {
            try
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
        private void frm_hoadon_taotay_v3_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
        }

        private void frm_hoadon_taotay_v3_Shown(object sender, EventArgs e)
        {
            LoadUserConfigs();
            txtTenhang.Focus();
        }

       

        public frm_hoadon_taotay_v3()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KeyDown += frm_hoadon_taotay_v3_KeyDown;
        }

        private void frm_hoadon_taotay_v3_KeyDown(object sender, KeyEventArgs e)
        {
            Control activeCtrl = Utility.getActiveControl(this);
            if (e.KeyCode == Keys.Enter)
            {

                if (activeCtrl.GetType().Equals(typeof(EditBox)))
                {
                    EditBox box = activeCtrl as EditBox;
                    if (box.Multiline)
                    {
                        return;
                    }
                    else
                        SendKeys.Send("{TAB}");
                }
                else if (activeCtrl.GetType().Equals(typeof(TextBox)))
                {
                    TextBox box = activeCtrl as TextBox;
                    if (box.Multiline)
                    {
                        return;
                    }
                    else
                        SendKeys.Send("{TAB}");
                }
                else
                    SendKeys.Send("{TAB}");

            }
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
                txtCCCD.Text = _buyer.BuyerIDNumber;
                chkCCCD.Enabled =chkCCCD.Checked= Utility.DoTrim(txtCCCD.Text).Length>0;
                txtDiachi.Text = _buyer.BuyerAddress;

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        string _file = Application.StartupPath + @"\KihieuHdon.txt";
        private void frm_hoadon_taotay_v3_Load(object sender, EventArgs e)
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
                dtpNgayhoadon.Value = DateTime.Now;
                Utility.SetDataSourceForDataGridEx(grdChitietThanhtoan, dtData, true, true, "1=1", "");
                txtDonvitinh.SetDefaultWhenInit = false;
                txtDonvitinh.Init();
               
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
        bool isValidData()
        {
            int CCCDLength = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("MISA_DODAI_CCCD", "12", true), 12);
            if (_buyer.BuyerFullName.Length <= 0)
            {
                Utility.ShowMsg("Bạn cần nhập họ tên người mua");
                txthovaten.Focus();
                return false;
            }
            if (_buyer.BuyerAddress.Length <= 0)
            {
                Utility.ShowMsg("Bạn cần nhập địa chỉ người mua");
                txtDiachi.Focus();
                return false;
            }
            if (chkCCCD.Checked && Utility.sDbnull(_buyer.BuyerIDNumber).Length != CCCDLength)
            {
                if (!Utility.AcceptQuestion(string.Format("Căn cước công dân {0} có độ dài khác {1}. Bạn có muốn tiếp tục phát hành hóa đơn với CCCD này {2}.\nNhấn No để hủy thao tác. Nhấn Yes để tiếp tục phát hành", _buyer.BuyerIDNumber, CCCDLength, _buyer.BuyerIDNumber), "Cảnh báo độ dài CCCD chưa phù hợp qui định", true))
                {
                    txtCCCD.Focus();
                    return false;
                }
            }
            if (chkSendEmail.Checked)
            {
                if (_buyer.BuyerEmail.Split(';').Length > 1)
                {
                    Utility.ShowMsg("Mục Email chỉ được nhập duy nhất 1 email nhận chính. Muốn gửi nhiều email thì nhập các email khác ở mục CC và cách nhau bởi dấu ;");
                    txtEmail.Focus();
                    return false;
                }
                if (_buyer.ReceiverName.Length <= 0)
                {
                    Utility.ShowMsg("Bạn cần nhập họ tên người nhận");
                    txtTennguoinhan.Focus();
                    return false;
                }
                if (_buyer.ReceiverEmail.Length <= 0)
                {
                    Utility.ShowMsg("Bạn cần nhập email người nhận. Các email cách nhau bởi dấy chấm phẩy ;");
                    txtEmail.Focus();
                    return false;
                }
            }
            return true;
        }
        private void cmdPhathanhHDon_Click(object sender, EventArgs e)
        {
            try
            {
                int CCCDLength = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("MISA_DODAI_CCCD", "12", true), 12);
                _buyer.BuyerFullName = chkFullName.Checked ? Utility.DoTrim(txthovaten.Text) : "";
                _buyer.BuyerAddress= Utility.DoTrim(txtDiachi.Text);
                _buyer.BuyerTaxCode = Utility.DoTrim(txtMST.Text);
                _buyer.BuyerBankAccount= Utility.DoTrim(txtSTK.Text);
                _buyer.BuyerLegalName = Utility.DoTrim(txttencongty.Text);
                _buyer.BuyerIDNumber = chkCCCD.Checked ? Utility.sDbnull(txtCCCD.Text) : "";
                _buyer.BuyerEmail = Utility.DoTrim(txtEmail.Text);
                _buyer.IsSendEmail = chkSendEmail.Checked;
                _buyer.ReceiverEmail = Utility.DoTrim(txtCC.Text);
                _buyer.ReceiverName = Utility.DoTrim(txtTennguoinhan.Text);

                if (!isValidData()) return;
                CreateItemsList();
                string eMessage = "";
                
                    bool kt = false;
                _MisaInvoices._buyer = _buyer;//Cần xem xét
                    kt = _MisaInvoices.phathanh_hoadon(_buyer, ref eMessage);
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
                
                int CCCDLength =Utility.Int32Dbnull( THU_VIEN_CHUNG.Laygiatrithamsohethong("MISA_DODAI_CCCD","12", true),12);
                _buyer.BuyerFullName = chkFullName.Checked ? Utility.DoTrim(txthovaten.Text) : "";
                _buyer.BuyerAddress = Utility.DoTrim(txtDiachi.Text);
                _buyer.BuyerTaxCode = Utility.DoTrim(txtMST.Text);
                _buyer.BuyerBankAccount = Utility.DoTrim(txtSTK.Text);
                _buyer.BuyerLegalName = Utility.DoTrim(txttencongty.Text);
                _buyer.BuyerIDNumber = chkCCCD.Checked ? Utility.sDbnull(txtCCCD.Text) : "";
                _buyer.BuyerEmail = Utility.DoTrim(txtEmail.Text);
                _buyer.IsSendEmail = chkSendEmail.Checked;
                _buyer.ReceiverEmail = Utility.DoTrim(txtCC.Text);
                _buyer.ReceiverName = Utility.DoTrim(txtTennguoinhan.Text);
                if (!isValidData()) return;
                CreateItemsList();
                string eMessage = "";
                _MisaInvoices._buyer = _buyer;
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

        private void cmdAddItems_Click(object sender, EventArgs e)
        {

            DataRow newDr = dtData.NewRow();
            newDr["id"] = Guid.NewGuid().ToString();
            newDr["ten_hang"] = Utility.sDbnull(txtTenhang.Text);
            newDr["donvitinh"]= Utility.sDbnull(txtDonvitinh.Text);
            newDr["soluong"] = Utility.Int32Dbnull(nmrSoluong.Value);
            newDr["dongia"] = Utility.DecimaltoDbnull(txtDongia.Text);
            newDr["thanhtien"] = Utility.DecimaltoDbnull(txtThanhtien.Text);
            newDr["VAT"] = Utility.Int32Dbnull(txtVAT.Text, 0);
            newDr["VATName"] = Utility.DecimaltoDbnull(txtVAT.Text, 0) <= 0 ? "KCT" : Utility.DoTrim(txtVAT.Text) + "%";
            newDr["tienVAT"] = Utility.DecimaltoDbnull(txtTienVAT.Text);
            newDr["tongtien"] = Utility.DecimaltoDbnull(txtTongtien.Text);
            if (Utility.sDbnull(txtTenhang.Text).Length <= 0)
            {
                Utility.ShowMsg("Bạn cần nhập tên hàng hóa");
                txtTenhang.Focus();
                return;
            }
            //Kiểm tra trùng tên hàng thì gợi ý sửa tên hàng trên lưới
            if(dtData.AsEnumerable().Where(c=>Utility.sDbnull(c["ten_hang"]).ToLower()== Utility.sDbnull(txtTenhang.Text).ToLower()).Any())
            {
                Utility.ShowMsg(string.Format("Đã có mặt hàng tên: \"{0}\".\nBạn cần nhập tên khác", Utility.sDbnull(txtTenhang.Text)));
                txtTenhang.SelectAll();
                txtTenhang.Focus();
                return;
            }    
            if (nmrSoluong.Value <= 0)
            {
                Utility.ShowMsg("Bạn cần nhập số lượng >=1");
                nmrSoluong.Focus();
                return;
            }
            if (Utility.DecimaltoDbnull(txtDongia.Text) <= 0)
            {
                Utility.ShowMsg("Bạn cần nhập đơn giá >0");
                txtDongia.Focus();
                return;
            }
            dtData.Rows.Add(newDr);
            ResetInput();
        }
        bool CreateItemsList()
        {
            _buyer.lstItems.Clear();
            foreach (DataRow dr in dtData.Rows)
            {
                ItemInfor newItem = new ItemInfor();
                newItem.id = Utility.sDbnull(dr["id"]);
                newItem.ten_hang = Utility.sDbnull(dr["ten_hang"]);
                newItem.donvitinh = Utility.sDbnull(dr["donvitinh"]);
                newItem.soluong = Utility.Int32Dbnull(dr["soluong"]);
                newItem.dongia = Utility.DecimaltoDbnull(dr["dongia"]);
                newItem.thanhtien = Utility.DecimaltoDbnull(dr["thanhtien"]);
                newItem.VAT = Utility.sDbnull(dr["VATName"]);
                newItem.tienVAT = Utility.DecimaltoDbnull(dr["tienVAT"]);
                newItem.tongtien = Utility.DecimaltoDbnull(dr["tongtien"]);
                if (newItem.ten_hang=="")
                {
                    Utility.ShowMsg("Tên hàng không được phép để trống. Vui lòng nhập đầy đủ");
                    return false;
                }    
                _buyer.lstItems.Add(newItem);
            }
            return true;
        }
        void ResetInput()
        {
            txtDongia.Text = "";
            nmrSoluong.Value = 1;
            txtVAT.Enabled = dtData.Rows.Count <= 0;
            txtTenhang.SelectAll();
            txtTenhang.Focus();
        }
    }
}
