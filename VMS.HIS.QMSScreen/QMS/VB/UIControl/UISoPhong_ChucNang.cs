using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.EditControls;
using SubSonic;
using VNS.Libs;

using Microsoft.VisualBasic;
using VNS.Properties;

namespace VMS.QMS.UIControl
{
    public partial class UISoPhong_ChucNang : UserControl
    {
        private QMSPrintProperties _hisqmsProperties;
        public delegate void SoKham();
        public string TenHienThi { get; set; }
        public string MaQms { get; set; }
        public SoKham MySoKham;
        public int SttUutien { get; set; }
        public string NhomQms { get; set; }
        public string MakhoaQms { get; set; }
        public string SoPhong { get; set; }
        public string Machucnang { get; set; }
        public string MaDoiTuong { get; set; }
        public string PatientCode { get; set; }
        public string Maphieu { get; set; }
        public string PatientName { get; set; }
        public int NamSinh { get; set; }
        public string GioiTinh { get; set; }
        public long PatientId { get; set; }
        public int UuTien { get; set; }
        public string TenMayIn { get; set; }
        public UISoPhong_ChucNang()
        {
            InitializeComponent();
            CauHinh();
        }
        private void CauHinh()
        {
            _hisqmsProperties = PropertyLib.GetQMSPrintProperties();
            txtSokham.Font = _hisqmsProperties.FontChu;
        }
        private string GetTenKhoa(string makhoa)
        {
            string tenkhoa = "";
            switch (makhoa)
            {
                case "KKB":
                    tenkhoa = "KHOA KHÁM BỆNH";
                    break;
                case "KYC":
                    tenkhoa = "KHOA KHÁM YÊU CẦU";
                    break;
                default:
                    tenkhoa = "KHOA KHÁM BỆNH";
                    break;
            }
            return tenkhoa;
        }
        private int _sokham = 0;
        private int _thoigiancho = 0;
        public void LaySokham(int status, string maKhoa, int isUuTien)
        {
            int sophong = 0;
            StoredProcedure sp = SPs.QmsGetSoChucNang(status, Machucnang, maKhoa, _sokham, sophong, Utility.Int16Dbnull(-1),
                PatientId, PatientCode, "", NamSinh, PatientName, MaDoiTuong, UuTien, MaQms, globalVariables.Branch_ID,
                globalVariables.Branch_ID, _thoigiancho);
            sp.Execute();
            _sokham = Utility.Int32Dbnull(sp.OutputValues[0]);
            sophong = Utility.Int32Dbnull(sp.OutputValues[1]);
            _thoigiancho = Utility.Int32Dbnull(sp.OutputValues[2]);
            string sSoKham = Utility.sDbnull(_sokham);
            if (Utility.Int32Dbnull(sSoKham) < 10)
            {
                sSoKham = Utility.FormatNumberToString(_sokham, "00");
            }
            txtSokham.Text = Utility.sDbnull(sSoKham);
            if (MySoKham != null)
            {
                MySoKham();
            }
        }
        public void InPhieuKham(string tenkhoa, UIButton button, string sophong, int isUuTien, string makhoathuchien)
        {
            try
            {
                string loaidoituong = @"";
                loaidoituong = string.Format("{0}", cmdNhanSo.Text);
                // Utility.WaitNow(this);
                //EnableButton(button, false);
                cmdNhanSo.Enabled = false;
                var mDtSoKham = new DataTable();
                Utility.AddColumToDataTable(ref mDtSoKham, "So_Kham", typeof(string));
                Utility.AddColumToDataTable(ref mDtSoKham, "So_Phong", typeof(string));
                Utility.AddColumToDataTable(ref mDtSoKham, "thoigiancho", typeof(string));
                DataRow dr = mDtSoKham.NewRow();
                string sSoKham = Utility.sDbnull(_sokham);
                if (Utility.Int32Dbnull(_sokham) < 10)
                {
                    sSoKham = Utility.FormatNumberToString(_sokham, "00");
                }
                dr["So_Kham"] = Utility.sDbnull(sSoKham);
                dr["So_Phong"] = Utility.sDbnull(txtSokham.Text);
                dr["thoigiancho"] = Utility.sDbnull(_thoigiancho);
                mDtSoKham.Rows.Add(dr);
                var crpt = new ReportDocument();
                const string reportcode = "QMS_SOPHONGCHUCNANG_CHITIET";
                string path = Utility.sDbnull(AppDomain.CurrentDomain.BaseDirectory + "Report\\CRPT_SOPHONG_CHUCNANG_CHITIET.RPT");
                if (File.Exists(path))
                {
                    crpt.Load(path);
                }
                else
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", path), "Thông báo không tìm thấy File",
                        MessageBoxIcon.Warning);
                    return;
                }
                crpt.SetDataSource(mDtSoKham);
                crpt.SetParameterValue("TEN_BENH_VIEN", _hisqmsProperties.TenBenhVien);
                string printDate = NgayIn(globalVariables.SysDate);
                crpt.SetParameterValue("PrintDate", printDate);
                crpt.SetParameterValue("TenKhoa", Utility.sDbnull(tenkhoa));
                crpt.SetParameterValue("LoaiDoiTuong", loaidoituong);
                crpt.SetParameterValue("ThongTinNguoiBenh", string.Format("{0} - {1}-{2}", PatientName, GioiTinh, NamSinh));
                crpt.PrintOptions.PrinterName = TenMayIn; // Utility.sDbnull(Utility.GetDefaultPrinter());
                crpt.PrintToPrinter(1, false, 0, 0);
                button.Focus();
                crpt.Close();
                crpt.Dispose();
                Utility.CleanTemporaryFolders();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
            finally
            {
                GC.Collect();
                cmdNhanSo.Enabled = true;
            }
        }
        static string NgayIn(DateTime dt)
        {
            string str = "Ngày ";
            str += Strings.Right("0" + dt.Day, 2);
            str += "/";
            str += Strings.Right("0" + dt.Month, 2);
            str += "/";
            str += dt.Year;
            str += " ";
            str += Strings.Right("0" + dt.Hour, 2);
            str += ":";
            str += Strings.Right("0" + dt.Minute, 2);
            str += ":";
            str += Strings.Right("0" + dt.Second, 2);
            return str;
        }

        private void cmdNhanSo_Click(object sender, EventArgs e)
        {
            LaySokham(1, Utility.sDbnull(MakhoaQms), UuTien);
            if (_hisqmsProperties.IsAutoPrint)
            {
                InPhieuKham(Utility.sDbnull(GetTenKhoa(MakhoaQms)), cmdNhanSo, Utility.sDbnull(txtSokham.Text), 0,
                    Utility.sDbnull(MakhoaQms));
            }
        }

        private void UISoPhong_ChucNang_Load(object sender, EventArgs e)
        {
            int trangthai = 0;
            if (string.IsNullOrEmpty(SoPhong))
            {
                cmdNhanSo.Text = string.Format("{0}", TenHienThi);
            }
            else
            {
                cmdNhanSo.Text = string.Format("{0}-{1}", TenHienThi, SoPhong);
            }
            cmdNhanSo.Tag = MaQms;
            string sSoKham = "00";
            int sokham = 0;
            StoredProcedure sp = SPs.QmsCanlamsangGetmaxso(MaQms, Utility.ByteDbnull(UuTien), MakhoaQms, globalVariables.Branch_ID, globalVariables.Branch_ID,
                    globalVariables.SysDate, PatientCode, sokham, Utility.ByteDbnull(trangthai));
            sp.Execute();
            sokham = Utility.Int32Dbnull(sp.OutputValues[0]);
            trangthai = Utility.ByteDbnull(sp.OutputValues[1]);
            sSoKham = Utility.sDbnull(sokham);
            if (Utility.Int32Dbnull(sokham) < 10)
            {
                sSoKham = Utility.FormatNumberToString(sokham, "00");
            }
            if (trangthai == 0)
            {
                txtSokham.BackColor = Color.Beige;
            }
            else
            {
                txtSokham.BackColor = Color.LightCoral;
            }
            txtSokham.Text = sSoKham;
        }
    }
}
