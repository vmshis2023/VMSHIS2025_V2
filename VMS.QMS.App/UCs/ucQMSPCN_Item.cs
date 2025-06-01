using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VNS.QMS;
using SubSonic;
using VMS.QMS.DAL;
using VNS.Libs.AppUI;
using Microsoft.VisualBasic;
using System.Threading;

namespace QMS.UCs
{
    public partial class ucQMSPCN_Item : UserControl
    {
        public string ma_khoakcb = "KKB";
        public string ma_phong = "SA";
        string ma_chidinh = "230101.0001";
        public string ma_chucnang ="SA";
        public string ma_KhoaKcb = "KKB";
        DataTable dtSTTKham = null;
        bool hasLoaded = false;
        string tenhienthi = "";
        byte SoUutien = 0;//0=số thường;1= số ưu tiên
        public delegate void OnCreatedQMSNumber(string QmsNum,string ma_chucnang);
        public event OnCreatedQMSNumber _OnCreatedQMSNumber;
        private QMSProperties _QMSProperties;
        long id_benhnhan = -1;
        string ma_luotkham = "";
        string ten_benhnhan = "";
        int tuoi = 0;
        int nam_sinh = 0;
        string gioitinh = "NAM";
        public bool hasQMS = false;
        DateTime ngay_chidinh=DateTime.Now;
        public ucQMSPCN_Item()
        {
            InitializeComponent();
            InitEvents();
           
        }

        public ucQMSPCN_Item(string ma_khoakcb, string ma_phong, string ma_chidinh, string ma_chucnang, string tenhienthi, QMSProperties _QMSProperties,  byte SoUutien)
        {
            InitializeComponent();
            InitEvents();
            this.tenhienthi = tenhienthi;
            this._QMSProperties = _QMSProperties;
            this.ma_KhoaKcb = ma_KhoaKcb;
            setControlProperties();
            cmdGetQMS.Text = tenhienthi;
            this.ma_khoakcb = ma_khoakcb;
            this.ma_phong = ma_phong;
            this.ma_chidinh = ma_chidinh;
            this.ma_chucnang = ma_chucnang;
            this.SoUutien = SoUutien;
        }
        public void Init(DataRow drQMS,DataRow drBenhnhan, string ma_khoakcb, string ma_phong, string ma_chidinh, DateTime ngay_chidinh, string ma_chucnang, string tenhienthi, QMSProperties _QMSProperties, byte SoUutien)
        {
            this._QMSProperties = _QMSProperties;
            this.tenhienthi = tenhienthi;
            setControlProperties();
            cmdGetQMS.Text = tenhienthi;
            this.ngay_chidinh = ngay_chidinh;
            this.ma_khoakcb = ma_khoakcb;
            this.ma_phong = ma_phong;
            this.ma_chidinh = ma_chidinh;
            this.ma_chucnang = ma_chucnang;
            this.SoUutien = SoUutien;

            id_benhnhan = Utility.Int64Dbnull(drBenhnhan["id_benhnhan"], 0);
            ma_luotkham = Utility.sDbnull(drBenhnhan["ma_luotkham"], "UKN");
            ten_benhnhan = Utility.sDbnull(drBenhnhan["ten_benhnhan"], "UKN");
            tuoi = Utility.Int32Dbnull(drBenhnhan["nam_sinh"], 0);
            gioitinh = Utility.sDbnull(drBenhnhan["gioi_tinh"], "UKN");

            if (drQMS == null)
                UIAction.SetText(lblQMSNumber, "0");
            else
            {
                int qmsNum = chkSoUutien.IsChecked ? Utility.Int32Dbnull(drQMS["STT_MAX_uutien"], 0) : Utility.Int32Dbnull(drQMS["STT_MAX"], 0);
                hasQMS = chkSoUutien.IsChecked ? Utility.Int32Dbnull(drQMS["hasQMS_uutien"], 0) > 0 : Utility.Int32Dbnull(drQMS["hasQMS"], 0) > 0;
                string str="";
                if (Utility.Int32Dbnull(qmsNum, 0) < 10)
                {
                    str = Utility.FormatNumberToString(qmsNum, "00");
                }
                UIAction.SetText(lblQMSNumber, str);
                if (hasQMS )
                    cmdGetQMS.Enabled = false;
            }
            hasLoaded = true;
        }
        public void Init(DataTable dtSTTKham)
        {
            //this.dtSTTKham = dtSTTKham;
            //LoadSTTKham();
            //hasLoaded = true;
        }
        void LoadSTTKham()
        {
            if (dtSTTKham == null)
                UIAction.SetText(lblQMSNumber, "00");
            else
            {
                DataRow[] arrDr = dtSTTKham.Select(QmsPhongchucnang.Columns.UuTien + "=" + (chkSoUutien.IsChecked ? "1" : "0"));
                if (arrDr.Length > 0)
                {
                    string str = Utility.sDbnull(arrDr[0]["STT_MAX"], "0");
                    if (Utility.Int32Dbnull(arrDr[0]["STT_MAX"], 0) < 10)
                    {
                        str = Utility.FormatNumberToString(Utility.Int32Dbnull(arrDr[0]["STT_MAX"], 0), "00");
                    }
                    UIAction.SetText(lblQMSNumber, str);
                }
                else
                    UIAction.SetText(lblQMSNumber, "00");
            }
        }
        void LoadSTTKham(string ma_phong, string ma_chucnang,byte souutien)
        {
            try
            {
                DataTable dtsoQMS = SPs.QmsLaysophongchucnang(ma_khoakcb,ma_chidinh, ma_phong, ma_chucnang, souutien).GetDataSet().Tables[0];
                if (dtsoQMS == null || dtsoQMS.Rows.Count <= 0)
                    UIAction.SetText(lblQMSNumber, "00");
                else
                {
                    string str = Utility.sDbnull(dtsoQMS.Rows[0]["so_qms_max"], "0");
                   bool hasQMS = Utility.Int32Dbnull(dtsoQMS.Rows[0]["so_qms_cuaphieu"], 0) > 0;
                   cmdGetQMS.Enabled = !hasQMS;
                    if (Utility.Int32Dbnull(dtsoQMS.Rows[0]["so_qms_max"], 0) < 10)
                    {
                        str = Utility.FormatNumberToString(Utility.Int32Dbnull(dtsoQMS.Rows[0]["so_qms_max"], 0), "00");
                    }
                    UIAction.SetText(lblQMSNumber, str);
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
           
        }
        public void setControlProperties()
        {
            this.Size = _QMSProperties.mySize;
            cmdGetQMS.Height = _QMSProperties.ButtonHeigh;
            lblQMSNumber.Height = _QMSProperties.NumberHeigh;
            cmdGetQMS.Font = _QMSProperties.NumberF;
            lblQMSNumber.Font = _QMSProperties.ButtonF;
            CauHinh();

        }
        private void CauHinh()
        {
            //lblQMSNumber.BackColor = _QMSProperties.MauB1;
            //lblQMSNumber.ForeColor = _QMSProperties.MauF1;
        }
        void InitEvents()
        {
            cmdGetQMS.Click += cmdGetQMS_Click;
            nmrMore.ValueChanged += nmrMore_ValueChanged;
            chkMore.CheckedChanged += chkMore_CheckedChanged;
            chkSoUutien._Oncheck += chkSoUutien__Oncheck;
            chkInsokhac._Oncheck += chkInsokhac__Oncheck;
            mnuReprint.Click += mnuReprint_Click;
        }

        void chkInsokhac__Oncheck()
        {
            if (hasQMS && chkInsokhac.IsChecked)
                cmdGetQMS.Enabled = true;
        }
        bool Reprint = false;
        void mnuReprint_Click(object sender, EventArgs e)
        {
            if (mnuReprint.Checked)
            {
                lblQMSNumber.ReadOnly = false;
                lblQMSNumber.BackColor = Color.White;
                Reprint = true;
                cmdGetQMS.Text = "In lại";
            }
            else
            {
                Reprint = false;
                lblQMSNumber.BackColor = cmdGetQMS.BackColor;
                lblQMSNumber.ReadOnly = true;
                cmdGetQMS.Text = tenhienthi;
            }
            
        }

        void chkSoUutien__Oncheck()
        {
            LoadSTTKham(ma_phong,ma_chucnang,Utility.Bool2byte(chkSoUutien.IsChecked));
        }
       
        void chkMore_CheckedChanged(object sender, EventArgs e)
        {
            nmrMore.Enabled = chkMore.Checked;
            if (!hasLoaded) return;
            if (chkMore.Checked)
                toolTip1.SetToolTip(lblQMSNumber, "Nếu bạn nhấn nút Lấy số. Hệ thống sẽ tự động in từ số " + (Utility.Int32Dbnull(lblQMSNumber.Text, 0) + 1).ToString() + " đến số " + (Utility.Int32Dbnull(lblQMSNumber.Text, 0) + nmrMore.Value).ToString());
            else
                toolTip1.SetToolTip(lblQMSNumber, "");
           
        }

        void nmrMore_ValueChanged(object sender, EventArgs e)
        {
            if (!hasLoaded) return;
            if (chkMore.Checked)
                toolTip1.SetToolTip(lblQMSNumber, "Nếu bạn nhấn nút Lấy số. Hệ thống sẽ tự động in từ số " + (Utility.Int32Dbnull(lblQMSNumber.Text, 0) + 1).ToString() + " đến số " + (Utility.Int32Dbnull(lblQMSNumber.Text, 0) + nmrMore.Value).ToString());
            else
                toolTip1.SetToolTip(lblQMSNumber, "");
        }

        void cmdGetQMS_Click(object sender, EventArgs e)
        {
            UIAction._EnableControl(cmdGetQMS, false, "");
            bool RestoreStatus = true;
            try
            {

                byte LaysoUutien = Utility.Bool2byte(chkSoUutien.IsChecked);
                if (Reprint)
                {
                    QmsPhongchucnang objQms = new Select().From(QmsPhongchucnang.Schema)
                        .Where(QmsPhongchucnang.Columns.SoQms).IsEqualTo(Utility.Int32Dbnull(lblQMSNumber.Text, 0))
                        .And(QmsPhongchucnang.Columns.UuTien).IsEqualTo(LaysoUutien)
                        .And(QmsPhongchucnang.Columns.MaPhong).IsEqualTo(ma_phong)
                        .And(QmsPhongchucnang.Columns.MaChucnang).IsEqualTo(ma_chucnang)
                        .And(QmsPhongchucnang.Columns.MaKhoakcb).IsEqualTo(ma_khoakcb)
                        .And(QmsPhongchucnang.Columns.IdBenhnhan).IsEqualTo(id_benhnhan)
                        .And(QmsPhongchucnang.Columns.MaLuotkham).IsEqualTo(ma_luotkham)
                        .ExecuteSingle<QmsPhongchucnang>();
                    if (objQms == null)
                    {
                        Utility.ShowMsg("Số thứ tự khám bạn muốn in lại chưa được tạo hoặc không tồn tại. Đề nghị bạn nhập số khác");
                        RestoreStatus = false;
                        lblQMSNumber.SelectAll();
                        lblQMSNumber.Focus();
                        return;
                    }
                    this.InPhieuKham(_QMSProperties.TenKhoaKhamBenh, this.cmdGetQMS, Utility.sDbnull(objQms.SoQms), LaysoUutien);
                    return;
                }
                int soluong = 1;
                if (chkInsokhac.IsChecked) chkInsokhac.IsChecked = false;
                if (chkMore.Checked)
                    soluong = (int)nmrMore.Value;
                for (int i = 1; i <= soluong; i++)
                {
                    string qmsNumber = "";
                    this.LaySokham(1,  LaysoUutien, ref qmsNumber);
                    if (_QMSProperties.PrintStatus)
                    {
                        this.InPhieuKham(_QMSProperties.TenKhoaKhamBenh, this.cmdGetQMS, qmsNumber, LaysoUutien);
                    }
                }
                
                if (_QMSProperties.SleepTime > 10)
                    Thread.Sleep(_QMSProperties.SleepTime);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                if (RestoreStatus)
                {
                    Reprint = false;
                    lblQMSNumber.BackColor = cmdGetQMS.BackColor;
                    lblQMSNumber.ReadOnly = true;
                    mnuReprint.Checked = false;
                    if (Utility.DoTrim(tenhienthi) != "") cmdGetQMS.Text = tenhienthi;
                }
               
            }
        }
        private void LaySokham(byte status, byte IsUuTien,ref string qmsNumber)
        {
            try
            {
                int num = 0;
                int sttkham = 0;
                StoredProcedure procedure = SPs.VmsQmsPCNTaoso(status, ma_khoakcb, ma_phong, ma_chucnang, ma_chidinh, sttkham, DateTime.Now, ngay_chidinh, id_benhnhan, ma_luotkham, ten_benhnhan, nam_sinh, tuoi, gioitinh, IsUuTien);
                procedure.Execute();
                //int SttKham = Utility.Int32Dbnull(procedure.OutputValues[1]);
                //UpdateSTTKham(SttKham);
                num = Utility.Int32Dbnull(procedure.OutputValues[0]);
                string str = Utility.sDbnull(num);
                if (Utility.Int32Dbnull(num) < 10)
                {
                    str = Utility.FormatNumberToString(num, "00");
                }
                qmsNumber = str;
                if (_OnCreatedQMSNumber != null)
                    _OnCreatedQMSNumber(str, ma_chucnang);
                //Bỏ 231107--tìm hiểu sau
                //str = Utility.sDbnull(SttKham);
                //if (Utility.Int32Dbnull(SttKham) < 10)
                //{
                //    str = Utility.FormatNumberToString(SttKham, "00");
                //}
                lblQMSNumber.Text = Utility.sDbnull(str);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi thực hiện lấy số QMS phòng chức năng\n" + ex.Message);
            }
        }
        void UpdateSTTKham(int SttKham)
        {
            if (dtSTTKham == null) return;
            DataRow[] arrDr = dtSTTKham.Select(QmsPhongchucnang.Columns.UuTien + "=" + (chkSoUutien.IsChecked ? "1" : SoUutien.ToString()));
            if (arrDr.Length > 0)
            {
                arrDr[0]["STT"] = SttKham;
                dtSTTKham.AcceptChanges();
            }
            else
            {
                DataRow newdr = dtSTTKham.NewRow();
                newdr["STT"] = SttKham;
                newdr["so_luong"] = Utility.Int32Dbnull(newdr["so_luong"],0) + 1;
                newdr["ma_khoakcb"] = ma_khoakcb;
                newdr["ma_phong"] = ma_phong;
                newdr["uu_tien"] = chkSoUutien.IsChecked ? 1 : SoUutien;
                dtSTTKham.Rows.Add(newdr);
            }
        }
        //  short LaySothutuKCB(int Department_ID)
        //{
        //    short So_kham = 0;
        //    DataTable dataTable = new DataTable();
        //    dataTable =
        //        SPs.KcbTiepdonLaysothutuKcb(Department_ID, globalVariables.SysDate).GetDataSet().Tables[0];
        //    if (dataTable.Rows.Count > 0)
        //    {
        //        So_kham = (short)(Utility.Int16Dbnull(dataTable.Rows[0]["So_Kham"], 0));
        //    }
        //    else
        //    {
        //        So_kham = 1;
        //    }
        //    return So_kham;
        //}
        private void InPhieuKham(string tenkhoa, Button button, string sokham, int isUuTien)
        {
            try
            {

                string val = "BỆNH NHÂN THƯỜNG";
                if (ma_KhoaKcb == "KKB")
                {
                    val = (isUuTien == 0) ? "BỆNH NHÂN THƯỜNG" : "BỆNH NHÂN ƯU TIÊN";
                }
                
                UIAction._EnableControl(button, false, "");
                DataTable dataTable = new DataTable();
                Utility.AddColumToDataTable(ref dataTable, "So_Kham", typeof(string));
                DataRow row = dataTable.NewRow();
                row["So_Kham"] = Utility.sDbnull(sokham);
                dataTable.Rows.Add(row);
                CRPT_SOKHAM crpt_sokham = new CRPT_SOKHAM();
                crpt_sokham.SetDataSource(dataTable);
                crpt_sokham.SetParameterValue("TEN_BENH_VIEN", _QMSProperties.TenBenhVien);
                string str2 = NgayIn(DateTime.Now);
                crpt_sokham.SetParameterValue("PrintDate", str2);
                crpt_sokham.SetParameterValue("TenKhoa", Utility.sDbnull(tenkhoa));
                crpt_sokham.SetParameterValue("LoaiDoiTuong", val);
                crpt_sokham.SetParameterValue("NoiChokham", tenhienthi);
                if (!string.IsNullOrEmpty(_QMSProperties.PrinterName))
                {
                    crpt_sokham.PrintOptions.PrinterName = Utility.sDbnull(_QMSProperties.PrinterName);
                }
                crpt_sokham.PrintToPrinter(1, false, 0, 0);
                button.Enabled = !hasQMS || chkInsokhac.IsChecked ;
                button.Focus();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi in số khám\n" + ex.Message);
            }
            finally
            {
               // UIAction._EnableControl(button, true, "");
            }
        }
        public static string NgayIn(DateTime dt)
        {
            string str = "Ngày ";
            return ((((((str + Strings.Right("0" + dt.Day.ToString(), 2)) + "/" + Strings.Right("0" + dt.Month.ToString(), 2)) + "/" + dt.Year) + " " + Strings.Right("0" + dt.Hour.ToString(), 2)) + ":" + Strings.Right("0" + dt.Minute.ToString(), 2)) + ":" + Strings.Right("0" + dt.Second.ToString(), 2));
        }

        private void mnuReprint_Click_1(object sender, EventArgs e)
        {

        }


    }
}
