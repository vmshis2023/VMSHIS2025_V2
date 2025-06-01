using System;
using System.Data;
using System.Windows.Forms;
using VNS.Libs;
using SubSonic;
using VMS.HIS.DAL;


namespace VNS.HIS.UI.HOADONDO
{
    public partial class frm_AddRedInvoice : Form
    {
        public DataTable dtList;
        private DataTable dtTrang_Thai;
        private DataRow currentDr;

        public frm_AddRedInvoice()
        {
            InitializeComponent();
        }

        private void frm_AddRedInvoice_Load(object sender, EventArgs e)
        {
            try
            {
                dtTrang_Thai =
                    new Select("*").From(HoadonTrangthai.Schema).Where(HoadonTrangthai.Columns.MessageType).IsEqualTo("MauHD").
                        OrderAsc(HoadonTrangthai.Columns.MessageOrder).
                        ExecuteDataSet().Tables[0];
                DataBinding.BindData(cboTrang_Thai, dtTrang_Thai, "Message_ID", "Message_Name");
                cboTrang_Thai.SelectedIndex = 0;
                dtpHieuLuc.Value = globalVariables.SysDate;
                dtpHetHieuLuc.Value = globalVariables.SysDate;
                txtNguoi_Tao.Text = globalVariables.UserName;

                if (!string.IsNullOrEmpty(txtMau_HD.Text))
                    LoadInvoice();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void LoadInvoice()
        {
            HoadonMau obj =
                new Select().From(HoadonMau.Schema).Where(HoadonMau.Columns.MauHoadon).IsEqualTo(txtMau_HD.Text).And(
                    HoadonMau.Columns.KiHieu).IsEqualTo(txtKi_Hieu.Text).ExecuteSingle<HoadonMau>();
            txtSerie_Dau.Text = obj.SerieDau;
            txtSerie_Cuoi.Text = obj.SerieCuoi;
            txtSoQuyen.Text = obj.MaQuyen;
            txtNguoi_Tao.Text = obj.NguoiTao;
            txtNgay_Tao.Text = obj.NgayTao.ToString("dd/MM/yyyy");
            dtpHieuLuc.Value = obj.NgayHieuluc;
            dtpHetHieuLuc.Value = obj.NgayHethieuluc;
            txtNguoi_Sua.Text = Utility.sDbnull(obj.NguoiSua);
            txtNgay_Sua.Text = obj.NgaySua == null ? "" : obj.NgaySua.Value.ToString("dd/MM/yyyy");
            cboTrang_Thai.SelectedValue = obj.TrangThai;

            currentDr = Utility.GetDataRow(dtList, new string[] { HoadonMau.Columns.MauHoadon, HoadonMau.Columns.KiHieu }, new object[] { txtMau_HD.Text, txtKi_Hieu.Text });
            txtNguoi_Sua.Text = globalVariables.UserName;
            //HoadonMau obj  = HoadonMau.CreateQuery().WHERE(HoadonMau.Columns.MauHdon,txtMau_HD.Text).AND(HoadonMau.Columns.KiHieu,txtKi_Hieu.Text).ge
        }

        private void cmdGhi_Click(object sender, EventArgs e)
        {
            if (!ValidObj()) return;

            try
            {
                if (currentDr == null)
                {
                    DateTime dateTime = globalVariables.SysDate;
                    txtNgay_Tao.Text = dateTime.ToString();

                    HoadonMau obj = new HoadonMau();
                    obj.MauHoadon = txtMau_HD.Text;
                    obj.KiHieu = txtKi_Hieu.Text;
                    obj.MaQuyen = txtSoQuyen.Text;
                    obj.SerieDau = txtSerie_Dau.Text;
                    obj.SerieCuoi = txtSerie_Cuoi.Text;
                    obj.NgayHieuluc = dtpHieuLuc.Value.Date;
                    obj.NgayHethieuluc = dtpHetHieuLuc.Value.Date;
                    obj.TrangThai = 0;
                    obj.NgayTao = dateTime;
                    obj.NguoiTao = globalVariables.UserName;
                    obj.IsNew = true;
                    obj.Save();

                    DataRow dr = dtList.NewRow();
                    ProcessData(ref dr, Utility.Int32Dbnull(obj.IdHoadonMau, -1));
                    dtList.Rows.InsertAt(dr, 0);
                    dtList.AcceptChanges();    
                }
                else
                {
                    DateTime dateTime =globalVariables.SysDate;
                    txtNgay_Sua.Text = dateTime.ToString();
                    txtNguoi_Sua.Text = globalVariables.UserName;

                    new Update(HoadonMau.Schema).Set(HoadonMau.Columns.MauHoadon).EqualTo(txtMau_HD.Text).
                        Set(HoadonMau.Columns.KiHieu).EqualTo(txtKi_Hieu.Text).
                        Set(HoadonMau.Columns.SerieDau).EqualTo(txtSerie_Dau.Text).
                        Set(HoadonMau.Columns.MaQuyen).EqualTo(txtSoQuyen.Text).
                        Set(HoadonMau.Columns.SerieCuoi).EqualTo(txtSerie_Cuoi.Text).
                        Set(HoadonMau.Columns.NgayHieuluc).EqualTo(dtpHieuLuc.Value.Date).
                        Set(HoadonMau.Columns.NgayHethieuluc).EqualTo(dtpHetHieuLuc.Value.Date).
                        Set(HoadonMau.Columns.NguoiSua).EqualTo(txtNguoi_Sua.Text).
                        Set(HoadonMau.Columns.NgaySua).EqualTo(dateTime).
                        Where(HoadonMau.Columns.IdHoadonMau).IsEqualTo(Utility.sDbnull(currentDr[HoadonMau.Columns.IdHoadonMau])).                       
                        //And(HoadonMau.Columns.MaQuyen).IsEqualTo(Utility.sDbnull(txtSoQuyen.Text)).
                        Execute();

                    ProcessData(ref currentDr, Utility.Int32Dbnull(currentDr[HoadonMau.Columns.IdHoadonMau]));
                    currentDr.AcceptChanges();
                }

                cmdThoat.PerformClick();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void ProcessData(ref DataRow dr, int HDMau_Id)
        {
            dr[HoadonMau.Columns.IdHoadonMau] = HDMau_Id;
            dr[HoadonMau.Columns.MauHoadon] = txtMau_HD.Text;
            dr[HoadonMau.Columns.KiHieu] = txtKi_Hieu.Text;
            dr[HoadonMau.Columns.SerieDau] = txtSerie_Dau.Text;
            dr[HoadonMau.Columns.SerieCuoi] = txtSerie_Cuoi.Text;
            dr[HoadonMau.Columns.NgayHieuluc] = dtpHieuLuc.Value.ToString("dd/MM/yyyy");
            dr[HoadonMau.Columns.NgayHethieuluc] = dtpHetHieuLuc.Value.ToString("dd/MM/yyyy");
            dr[HoadonMau.Columns.TrangThai] = cboTrang_Thai.SelectedIndex;
            dr["TRANG_THAI_Message"] = cboTrang_Thai.Text;
            dr[HoadonMau.Columns.NgayTao] = txtNgay_Tao.Text;
            dr[HoadonMau.Columns.NguoiTao] = txtNguoi_Tao.Text;
            if (!string.IsNullOrEmpty(txtNgay_Sua.Text))
            {
                dr[HoadonMau.Columns.NgaySua] = txtNgay_Sua.Text;
                dr[HoadonMau.Columns.NguoiSua] = txtNguoi_Sua.Text;    
            }
        }

        private bool ValidObj()
        {
            try
            {
                if (string.IsNullOrEmpty(txtMau_HD.Text) || string.IsNullOrEmpty(txtKi_Hieu.Text))
                {
                    Utility.ShowMsg("[Mã hóa đơn] & [Kí hiệu] & [Số quyển] hóa đơn không được để trống");
                    return false;
                }

                if (string.IsNullOrEmpty(txtSerie_Dau.Text) || string.IsNullOrEmpty(txtSerie_Cuoi.Text))
                {
                    Utility.ShowMsg("Số serie hóa đơn không được để trống");
                    return false;
                }

                if (txtSerie_Dau.Text.Length != txtSerie_Cuoi.Text.Length)
                {
                    Utility.ShowMsg("Số ký tự của serie đầu và serie cuối phải giống nhau");
                    return false;
                }

                if (string.Compare(txtSerie_Dau.Text,txtSerie_Cuoi.Text,StringComparison.InvariantCulture) > 0)
                {
                    Utility.ShowMsg("Serie đầu phải nhỏ hơn serie cuối");
                    return false;
                }

                if (dtpHetHieuLuc.Value.Date - dtpHieuLuc.Value.Date < TimeSpan.FromDays(30))
                {
                    if (!Utility.AcceptQuestion("Đề nghị xác nhận sử dụng hóa đơn dưới 30 ngày", "Thông báo",false))
                        return false;
                }

                if (currentDr == null)
                    if (HoadonMau.CreateQuery().WHERE(HoadonMau.Columns.MauHoadon, txtMau_HD.Text).AND(HoadonMau.Columns.KiHieu, txtKi_Hieu.Text)
                        .AND(HoadonMau.Columns.MaQuyen, txtSoQuyen.Text)
                        .GetRecordCount() > 0)
                    {
                        Utility.ShowMsg(string.Format("Tồn tại mã hóa đơn {0} -  kí hiệu {1} - Số Quyển {2}" , txtMau_HD.Text,
                                                      txtKi_Hieu.Text, txtSoQuyen.Text));
                        return false;
                    }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private void dtpHieuLuc_ValueChanged(object sender, EventArgs e)
        {
            if (dtpHetHieuLuc.Value < dtpHieuLuc.Value)
                dtpHetHieuLuc.Value = dtpHieuLuc.Value;
        }

        private void dtpHetHieuLuc_ValueChanged(object sender, EventArgs e)
        {
            if (dtpHetHieuLuc.Value < dtpHieuLuc.Value)
                dtpHieuLuc.Value = dtpHetHieuLuc.Value;
        }

        private void frm_AddRedInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.S:
                            cmdGhi.PerformClick();
                            break;
                    }
                }
                else
                    switch (e.KeyCode)
                    {
                        case Keys.Escape:
                            cmdThoat.PerformClick();
                            break;
                    }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtSerie_Dau_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        private void txtSerie_Cuoi_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }       
    }
}
