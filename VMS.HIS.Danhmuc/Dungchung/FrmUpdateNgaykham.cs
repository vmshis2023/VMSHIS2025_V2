using System;
using System.Data;
using System.Transactions;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.UI.Forms.Dungchung
{
    public partial class FrmUpdateNgaykham : Form
    {
        private readonly string ma_lk = "";

        public FrmUpdateNgaykham(string maLankham)
        {
            InitializeComponent();
            ma_lk = maLankham;
        }

        private void FrmUpdateNgaykham_Load(object sender, EventArgs e)
        {
            cmdUpdate.Focus();
            DataTable dt = SPs.KcbGetthongtinNgaykham(ma_lk).GetDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                txtidbenhnhan.Text = Utility.sDbnull(dt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan], "-1");
                txtHoTen.Text = Utility.sDbnull(dt.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                txtNamSinh.Text = Utility.sDbnull(dt.Rows[0][KcbDanhsachBenhnhan.Columns.NamSinh], "");
                txtDiaChi.Text = Utility.sDbnull(dt.Rows[0][KcbDanhsachBenhnhan.Columns.DiaChi], "");
                cboPatientSex.SelectedIndex = Utility.GetSelectedIndex(cboPatientSex,
                    Utility.sDbnull(dt.Rows[0][KcbDanhsachBenhnhan.Columns.IdGioitinh]));
                txtDoiTuong.Text = Utility.sDbnull(dt.Rows[0][DmucDoituongkcb.Columns.TenDoituongKcb], "");
                txtNgoaitru.Text = Utility.sDbnull(dt.Rows[0][KcbLuotkham.Columns.Noitru], "");
                txtSoDT.Text = Utility.sDbnull(dt.Rows[0][KcbDanhsachBenhnhan.Columns.DienThoai], "");
                txtmathe.Text = Utility.sDbnull(dt.Rows[0][KcbLuotkham.Columns.MatheBhyt], "");
                txtnoidongtruso.Text = Utility.sDbnull(dt.Rows[0][KcbLuotkham.Columns.NoiDongtrusoKcbbd], "");
                txtnoidkkcb.Text = Utility.sDbnull(dt.Rows[0][KcbLuotkham.Columns.MaKcbbd], "");
                if (dt.Rows[0][KcbLuotkham.Columns.NgayTiepdon] == null)
                {
                    dtpNgayTiepDon.IsNullDate = true;
                    lblngaytiepdon.Text = @"Chưa tiếp đón";
                }
                else
                {
                    dtpNgayTiepDon.Value = Convert.ToDateTime(dt.Rows[0][KcbLuotkham.Columns.NgayTiepdon]);
                }
                if (dt.Rows[0][KcbLuotkham.Columns.NgayNhapvien].ToString() == "")
                {
                    dtpNgayNhapVien.IsNullDate = true;
                    lblngaynhapvien.Text = @"Chưa nhập viện";
                }
                else
                {
                    dtpNgayNhapVien.Value = Convert.ToDateTime(dt.Rows[0][KcbLuotkham.Columns.NgayNhapvien]);
                }
                if (dt.Rows[0][KcbLuotkham.Columns.NgayRavien].ToString() == "")
                {
                    dtpNgayRaVien.IsNullDate = true;
                    lblngayravien.Text = @"Chưa ra viện";
                }
                else
                {
                    dtpNgayRaVien.Value = Convert.ToDateTime(dt.Rows[0][KcbLuotkham.Columns.NgayRavien]);
                }
                if (dt.Rows[0][KcbLuotkham.Columns.NgayThanhtoan].ToString() == "")
                {
                    dtpNgayTToan.IsNullDate = true;
                    lblngayttoan.Text = @"Chưa thanh toán";
                }
                else
                {
                    dtpNgayTToan.Value = Convert.ToDateTime(dt.Rows[0][KcbLuotkham.Columns.NgayThanhtoan]);
                }


                Text = txtHoTen.Text + "-" + txtNamSinh.Text;
            }
            else
            {
                Utility.ShowMsg("Không thể load được thông tin bệnh nhân");
                Close();
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool IsvaliUpdate()
        {
            if (dtpNgayTToan.Value < dtpNgayTiepDon.Value)
            {
                Utility.ShowMsg("Thời gian thanh toán không thể nhỏ hơn thời gian tiếp đón!");
                return false;
            }
            if (dtpNgayTToan.Value < dtpNgayRaVien.Value && txtNgoaitru.Text.Trim() == @"Nội trú")
            {
                Utility.ShowMsg("Thời gian thanh toán không thể nhỏ hơn  thời gian ra viện!");
                return false;
            }
            if (dtpNgayTToan.Value < dtpNgayNhapVien.Value && txtNgoaitru.Text.Trim() == @"Nội trú")
            {
                Utility.ShowMsg("Thời gian thanh toán không thể nhỏ hơn  thời gian nhập viện!");
                return false;
            }

            if (dtpNgayNhapVien.Value < dtpNgayTiepDon.Value && txtNgoaitru.Text.Trim() == @"Nội trú")
            {
                Utility.ShowMsg("Thời gian nhập viện không thể nhỏ hơn  thời gian tiếp đón!");
                return false;
            }

            if (dtpNgayRaVien.Value < dtpNgayNhapVien.Value && txtNgoaitru.Text.Trim() == @"Nội trú")
            {
                {
                    Utility.ShowMsg("Thời gian nhập viện không thể nhỏ hơn  thời gian ra viện!");
                    return false;
                }
            }
            return true;
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsvaliUpdate()) return;
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        new Update(KcbDanhsachBenhnhan.Schema).Set(KcbDanhsachBenhnhan.Columns.DienThoai).EqualTo(txtSoDT.Text.Trim())
                            .Set(KcbDanhsachBenhnhan.Columns.TenBenhnhan).EqualTo(txtHoTen.Text.Trim())
                            .Set(KcbDanhsachBenhnhan.Columns.IdGioitinh).EqualTo(cboPatientSex.SelectedIndex)
                            .Set(KcbDanhsachBenhnhan.Columns.GioiTinh).EqualTo(cboPatientSex.Text)
                            .Set(KcbDanhsachBenhnhan.Columns.NamSinh).EqualTo(Utility.Int16Dbnull(txtNamSinh.Text.Trim()))
                            .Set(KcbDanhsachBenhnhan.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                            .Set(KcbDanhsachBenhnhan.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(KcbDanhsachBenhnhan.Columns.IpMaysua).EqualTo(globalVariables.gv_strIPAddress)
                            .Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan).IsEqualTo(Utility.Int64Dbnull(txtidbenhnhan.Text)).Execute();
                        Utility.Log(Name, globalVariables.UserName,
                                            string.Format("Thực hiện update thông tin bệnh nhân!"), newaction.Update , this.GetType().Assembly.ManifestModule.Name);
                        new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.NgayTiepdon).EqualTo(dtpNgayTiepDon.Value)
                            .Set(KcbLuotkham.Columns.NgayRavien).EqualTo(dtpNgayRaVien.Value)
                            .Set(KcbLuotkham.Columns.NgayThanhtoan).EqualTo(dtpNgayTToan.Value)
                            .Set(KcbLuotkham.Columns.MaKcbbd).EqualTo(txtnoidkkcb.Text.Trim())
                            .Set(KcbLuotkham.Columns.MatheBhyt).EqualTo(txtmathe.Text.Trim())
                            .Set(KcbLuotkham.Columns.NoiDongtrusoKcbbd).EqualTo(txtnoidongtruso.Text.Trim())
                            .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(KcbLuotkham.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                            .Set(KcbLuotkham.Columns.IpMaysua).EqualTo(globalVariables.gv_strIPAddress)
                            .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(ma_lk).Execute();
                        new Update(KcbPhieuDct.Schema).Set(KcbPhieuDct.Columns.NgayTao).EqualTo(dtpNgayTToan.Value) 
                            .Set(KcbPhieuDct.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                            .Set(KcbPhieuDct.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(KcbPhieuDct.Columns.IpMaysua).EqualTo(globalVariables.gv_strIPAddress)
                            .Where(KcbPhieuDct.Columns.MaLuotkham)
                            .IsEqualTo(ma_lk)
                            .Execute();
                        Utility.Log(Name, globalVariables.UserName,
                        string.Format("Thực hiện update mã lần khám thành công!"), newaction.Update, this.GetType().Assembly.ManifestModule.Name);

                        Utility.ShowMsg("Update thông tin thành công");
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        private void FrmUpdateNgaykham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
            if (e.Control && e.KeyCode == Keys.S) cmdUpdate.PerformClick();
        }
    }
}