using System;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.UI.Forms.DUOC
{
    public partial class FrmUpdateByIdThuocKho : Form
    {
        public FrmUpdateByIdThuocKho()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
        }
        public long IdThuockho = 0;
        public DateTime Ngayhethan = DateTime.Now;
        public DateTime Ngaynhap = DateTime.Now;
        public decimal Gianhap = 0;
        public decimal Giadv = 0;
        public decimal Giabhyt = 0;
        public decimal phuthu_dt = 0;
        public decimal phuthu_tt = 0;
        public string  Solo = "";
        public int IdThuoc = -1;
        public string TenThuoc = "";
        public string sodangky = "";
        public string stt_thau = "";
        private void FrmUpdateByIdThuocKho_Load(object sender, EventArgs e)
        {
            lblThongtin.Text = string.Format("Bạn đang sửa thông tin của thuốc: {0} - {1}",IdThuoc, TenThuoc);
            dtpngayhethan_new.Focus();
            txtidthuockho_old.Text = Utility.sDbnull(IdThuockho,-1);
            dtpngayhethan_old.Value = Convert.ToDateTime(Ngayhethan);
            dtpngaynhap_old.Value = Convert.ToDateTime(Ngaynhap);
            txtDongia_old.Text = Utility.sDbnull(Gianhap,0);
            txtgiadv_old.Text = Utility.sDbnull(Giadv,0);
            txtgiabhyt_old.Text = Utility.sDbnull(Giabhyt,0);
            txtsolo_old.Text = Utility.sDbnull(Solo,"");
            txtptdt_old.Text = Utility.sDbnull(phuthu_dt, 0);
            txtpttt_old.Text = Utility.sDbnull(phuthu_tt, 0);
            txtsodangky_old.Text = Utility.sDbnull(sodangky, "");
            txtsottthau_old.Text = Utility.sDbnull(stt_thau, "");
        }

        private void cmdCopy_Click(object sender, EventArgs e)
        {
            txtidthuockho_new.Text = txtidthuockho_old.Text;
            dtpngayhethan_new.Value = dtpngayhethan_old.Value;
            dtpngaynhap_new.Value = dtpngaynhap_old.Value;
            txtDongia_new.Text = txtDongia_old.Text;
            txtgiadv_new.Text = txtgiadv_old.Text;
            txtgiabhyt_new.Text = txtgiabhyt_old.Text;
            txtptdt_new.Text = txtptdt_old.Text;
            txtpttt_new.Text = txtpttt_old.Text;
            txtsolo_new.Text = txtsolo_old.Text;
            txtsodangky_new.Text = txtsodangky_old.Text;
            txtsottthau_new.Text = txtsottthau_old.Text;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool Ivalidata()
        {
            if (dtpngayhethan_new.Value <= globalVariables.SysDate)
            {
                Utility.ShowMsg("Ngày hết hạn không thể nhỏ hơn ngày hiện tại được");
                dtpngayhethan_new.Focus();
                return false;
            }
            //if (txtDongia_new.Text == "")
            //{
            //    Utility.ShowMsg("Đơn giá không được bỏ trống");
            //    txtDongia_new.Focus();
            //    return false;
            //}
            //if (txtgiabhyt_new.Text == "")
            //{
            //    Utility.ShowMsg("Giá bán bhyt không được bỏ trống");
            //    txtgiabhyt_new.Focus();
            //    return false;
            //}
            //if (txtgiadv_new.Text == "")
            //{
            //    Utility.ShowMsg("Giá bán dịch vụ không được bỏ trống");
            //    txtgiadv_new.Focus();
            //    return false;
            //}
            return true;
        }
        private void cmdupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.Coquyen("quyen_suathongtin_thuoc"))
                {
                    if (!Ivalidata()) return;
                    if (
                        Utility.AcceptQuestion(
                            string.Format("Bạn có chắc chắn muốn sửa thông tin của thuốc {0} không?", TenThuoc),
                            "Question", true))
                    {
                        StoredProcedure sp = SPs.SpThuocUpdateByIdThuockho(Utility.Int64Dbnull(txtidthuockho_new.Text),
                            dtpngayhethan_new.Value, dtpngaynhap_new.Value,
                            Utility.DecimaltoDbnull(txtDongia_new.Text, 0),
                            Utility.DecimaltoDbnull(txtgiadv_new.Text, 0),
                            Utility.DecimaltoDbnull(txtgiabhyt_new.Text, 0),
                            Utility.sDbnull(txtsolo_new.Text), Utility.DecimaltoDbnull(txtptdt_new.Text, 0),
                            Utility.DecimaltoDbnull(txtpttt_new.Text), Utility.sDbnull(txtsodangky_new.Text, ""),
                            Utility.sDbnull(txtsottthau_new.Text, ""));
                        sp.Execute();
                        Utility.ShowMsg("Bạn thực hiện sửa thông tin thuốc thành công");
                        Utility.Log(Name, globalVariables.UserName,
                            string.Format("Thực hiện sửa thông tin thuốc {0} - {1} có id thuốc là {2} - {3}", IdThuoc, TenThuoc,
                                IdThuockho, sp), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                    }
                }
                else
                {
                    Utility.ShowMsg("Bạn chưa được phân quyền sửa thông tin thuốc!");
                }
                
               

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }

        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            //new frm_Properties(PropertyLib._ThuocProperties);
            //Cauhinh();
        }

        private void FrmUpdateByIdThuocKho_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if(e.Control && e.KeyCode == Keys.S) cmdupdate.PerformClick();
            if(e.Control && e.KeyCode == Keys.C) cmdCopy.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }


    }
}
