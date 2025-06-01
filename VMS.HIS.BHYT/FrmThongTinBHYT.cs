using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using VNS.Libs;


namespace VMS.HIS.BHYT
{
    public partial class FrmThongTinBHYT : Form
    {
        public KQLichSuKCB Kcbnhanlichsu = new KQLichSuKCB();
        public bool Chapnhan = false;
        public DataTable dtLichSuKCB = new DataTable();
        public FrmThongTinBHYT()
        {
            InitializeComponent();
            lblMessega.Text = "";
            grdLichSuKCB.DataSource = null;
            grdkiemtrathe.DataSource = null;
        }
      

        private void FrmThongTinCheckThe_Load(object sender, EventArgs e)
        {
            if (Kcbnhanlichsu != null)
            {
                string canhbao = "";
                if (Kcbnhanlichsu.maKetQua == "000")
                {
                    lblMessega.ForeColor = Color.Blue;
                }
                else
                {
                    lblMessega.ForeColor = Color.Red;
                }
                if (Kcbnhanlichsu.maDKBD != null)
                {
                    ma_the = string.Format("Thông báo: Mã kết quả {0}-{1}", Kcbnhanlichsu.maThe, Kcbnhanlichsu.maDKBD);
                    if ((globalVariables.gv_strNoicapBHYT + globalVariables.gv_strNoiDKKCBBD) != Kcbnhanlichsu.maDKBD)
                    {
                        lblcanhbao.ForeColor = Color.Red;
                        canhbao = string.Format("Thẻ không đăng ký ban đầu ở {0}", globalVariables.Branch_Name);
                    }
                    else
                    {
                        lblcanhbao.ForeColor = Color.Blue;
                        canhbao = "";
                    }
                }
                lblMessega.Text = string.Format("{0} - {1}", Kcbnhanlichsu.maKetQua, Kcbnhanlichsu.ghiChu);
                ghichu = string.Format("{0} - {1}", Kcbnhanlichsu.maKetQua, Kcbnhanlichsu.ghiChu);
                lblcanhbao.Text = canhbao;
                if (Kcbnhanlichsu.dsLichSuKCB2018 != null)
                {
                    string a = "1";
                    dtLichSuKCB = ConvertTableToList.dtLichSuKCB(Kcbnhanlichsu.dsLichSuKCB2018);
                    grdLichSuKCB.DataSource = dtLichSuKCB;
                }
                if (Kcbnhanlichsu.dsLichSuKT2018 != null)
                {
                    DataTable dtLicSuKt2018 = ConvertTableToList.ToDataTable(Kcbnhanlichsu.dsLichSuKT2018);
                    if (!dtLicSuKt2018.Columns.Contains("TenCSKCB"))
                    {
                        dtLicSuKt2018.Columns.Add("TenCSKCB");
                    }
                    var result = (from row1 in dtLicSuKt2018.AsEnumerable()
                                  select new
                                  {
                                      maLoi = row1.Field<string>("maLoi"),
                                      userKT = row1.Field<string>("userKT"),
                                      thongBao = row1.Field<string>("thongBao"),
                                      thoiGianKT = DateTime.ParseExact(row1.Field<string>("thoiGianKT"), "yyyyMMddHHmm", CultureInfo.InvariantCulture)
                                  }).ToList();
                    if (result.Any())
                    {
                        grdkiemtrathe.DataSource = result;
                        _dtLicSuKt2018_utm = ConvertTableToList.ToDataTable(result);
                    }
                   
                }
                
            }
        }
    
        private void cmdChapNhan_Click(object sender, EventArgs e)
        {
            Chapnhan = true;
            this.Dispose();
        }
        private void FrmThongTinBHYT_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private DataTable _dtLicSuKt2018_utm = null;
        private string ghichu = "";
        private string ma_the = "";

        private void cmdLichSuKiemTra_Click(object sender, EventArgs e)
        {
            try
            {
                //Inphieu_KhamBenh.InPhieuLichSuKiemTraTheBhyt(_dtLicSuKt2018_utm,dtLichSuKCB, "LỊCH SỬ KIỂM TRA THẺ BHYT", "A4", ghichu, ma_the);
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
        }

        private void cmdLichsuKcb_Click(object sender, EventArgs e)
        {
            try
            {
               // Inphieu_KhamBenh.InPhieuLichSuKcb(dtLichSuKCB, "LỊCH SỬ KIỂM TRA THẺ BHYT", "A4", ghichu, ma_the);
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
