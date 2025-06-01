using System;
using System.Drawing;
using System.Windows.Forms;
using VNS.Libs;

namespace VNS.HIS.UI.Forms.Dungchung
{
    public partial class FrmThongTinCheckThe : Form
    {
        public newBus.CheckCard.KQNhanLichSuKCBBS Kcbnhanlichsu = new newBus.CheckCard.KQNhanLichSuKCBBS();
        public bool Chapnhan = false;
        public FrmThongTinCheckThe()
        {
            InitializeComponent();
            lblMessega.Text = "";
        }

        private void FrmThongTinCheckThe_Load(object sender, EventArgs e)
        {
            try
            {
                if (Kcbnhanlichsu != null)
                {
                    if (Kcbnhanlichsu != null)
                    {
                        if (Kcbnhanlichsu.maloi == "000")
                        {
                            lblMessega.ForeColor = Color.Blue;
                        }
                        else
                        {
                            lblMessega.ForeColor = Color.Red;
                        }
                        lblMessega.Text = string.Format("{0} - {1}", Kcbnhanlichsu.maloi, Kcbnhanlichsu.ghiChu);
                        //lblMessega.Text = string.Format("Mã kết quả: {0}, {1} ", Kcbnhanlichsu.maloi, Kcbnhanlichsu.maKetQua);
                        txthovaten.Text = Kcbnhanlichsu.hoTen;
                        txtnamsinh.Text = Kcbnhanlichsu.ngaySinh;
                        if (Utility.Int32Dbnull(Kcbnhanlichsu.gioiTinh) == 1 ||
                            Utility.sDbnull(Kcbnhanlichsu.gioiTinh).ToUpper() == "NAM")
                        {
                            txtgioitinh.Text = "Nam";
                        }
                        else
                        {
                            txtgioitinh.Text = "Nữ";
                        }
                        //txtgioitinh.Text = Utility.Int32Dbnull(Kcbnhanlichsu.gioiTinh) == 1? "Nam" : "Nữ";
                        txtdiachi.Text = Kcbnhanlichsu.diaChi;
                        txtsobhxh.Text = Kcbnhanlichsu.maSoBHXH;
                        txtmathe.Text = Kcbnhanlichsu.maThe;
                        txtnoikcbbd.Text = Kcbnhanlichsu.maDKBD;
                        txtcoquanbh.Text = Kcbnhanlichsu.cqBHXH;
                        txtmakhuvuc.Text = Kcbnhanlichsu.maKV;
                        if (Utility.sDbnull(Kcbnhanlichsu.gtTheTu, "") != "")
                        {
                            dtptungay.Value = Convert.ToDateTime(Kcbnhanlichsu.gtTheTu);
                        }
                        else
                        {
                            dtptungay.IsNullDate = true;
                        }
                        if (Utility.sDbnull(Kcbnhanlichsu.gtTheDen, "") != "")
                        {
                            dtpdenngay.Value = Convert.ToDateTime(Kcbnhanlichsu.gtTheDen);
                        }
                        else
                        {
                            dtpdenngay.IsNullDate = true;
                        }
                        if (Utility.sDbnull(Kcbnhanlichsu.ngayDu5Nam, "") != "")
                        {
                            dtpngaydu5nam.Value = Convert.ToDateTime(Kcbnhanlichsu.ngayDu5Nam);
                        }
                        else
                        {
                            dtpngaydu5nam.IsNullDate = true;
                        }
                        txtmathemoi.Text = Kcbnhanlichsu.maTheMoi;
                        if (Utility.sDbnull(Kcbnhanlichsu.gtTheTuMoi, "") != "")
                        {
                            dtptungaymoi.Value = Convert.ToDateTime(Kcbnhanlichsu.gtTheTuMoi);
                        }
                        else
                        {
                            dtptungaymoi.IsNullDate = true;
                        }
                        if (Utility.sDbnull(Kcbnhanlichsu.gtTheDenMoi, "") != "")
                        {
                            dtpdenngaymoi.Value = Convert.ToDateTime(Kcbnhanlichsu.gtTheDenMoi);
                        }
                        else
                        {
                            dtpdenngaymoi.IsNullDate = true;
                        }

                    }
                }
            }
            catch (Exception)
            {
                
            }
          
        }

        private void cmdChapNhan_Click(object sender, EventArgs e)
        {
            Chapnhan = true;
            this.Dispose();
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
