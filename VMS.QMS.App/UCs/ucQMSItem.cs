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
    public partial class ucQMSItem : UserControl
    {
        public int id_kieukham = -1;
        public int id_phongkham = -1;
        string ma_doituong_kcb = "";
        public int id_KhoaKcb = -1;
        public string ma_KhoaKcb = "KKB";
        int id_dichvukcb=-1;
        DataTable dtSTTKham = null;
        bool hasLoaded = false;
        string tenhienthi = "";
        int SoUutien = 0;//0=số thường;1= số ưu tiên
        public delegate void OnCreatedQMSNumber(string MaxNumber);
        public event OnCreatedQMSNumber _OnCreatedQMSNumber;
        private QMSProperties _QMSProperties;
        public ucQMSItem()
        {
            InitializeComponent();
            InitEvents();
           
        }
        
        public ucQMSItem(int id_KhoaKcb,string ma_KhoaKcb, int id_kieukham, int id_phongkham, int id_dichvukcb,string tenhienthi, QMSProperties _QMSProperties, string ma_doituong_kcb, int SoUutien)
        {
            InitializeComponent();
            InitEvents();
            this.tenhienthi = tenhienthi;
            this._QMSProperties = _QMSProperties;
            this.id_dichvukcb = id_dichvukcb;
            this.ma_KhoaKcb = ma_KhoaKcb;
            setControlProperties();
            cmdGetQMS.Text = tenhienthi;
            this.id_KhoaKcb = id_KhoaKcb;
            this.id_kieukham = id_kieukham;
            this.id_phongkham = id_phongkham;
            this.ma_doituong_kcb = ma_doituong_kcb;
            this.SoUutien = SoUutien;
        }
        public void Init(DataRow dr,int id_KhoaKcb, int id_kieukham, int id_phongkham, string tenhienthi, QMSProperties _QMSProperties, string ma_doituong_kcb, int SoUutien)
        {
            this._QMSProperties = _QMSProperties;
            this.tenhienthi = tenhienthi;
            setControlProperties();
            cmdGetQMS.Text = tenhienthi;
            this.id_KhoaKcb = id_KhoaKcb;
            this.id_kieukham = id_kieukham;
            this.id_phongkham = id_phongkham;
            this.ma_doituong_kcb = ma_doituong_kcb;
            this.SoUutien = SoUutien;

            if (dr == null)
                UIAction.SetText(lblQMSNumber, "0");
            else
            {
                string str = Utility.sDbnull(dr["STT_KHAM"], "0");
                if (Utility.Int32Dbnull(dr["STT_KHAM"], 0) < 10)
                {
                    str = Utility.FormatNumberToString(Utility.Int32Dbnull(dr["STT_KHAM"], 0), "00");
                }
                UIAction.SetText(lblQMSNumber, str);
            }
            hasLoaded = true;
        }
        public void Init(DataTable dtSTTKham)
        {
            this.dtSTTKham = dtSTTKham;
            LoadSTTKham();
            hasLoaded = true;
        }
        void LoadSTTKham()
        {
            if (dtSTTKham == null)
                UIAction.SetText(lblQMSNumber, "00");
            else
            {
                DataRow[] arrDr = dtSTTKham.Select(QmsTiepdon.Columns.LoaiQms + "=" + (chkSoUutien.IsChecked ? "1" : "0"));
                if (arrDr.Length > 0)
                {
                    string str = Utility.sDbnull(arrDr[0]["STT_KHAM"], "0");
                    if (Utility.Int32Dbnull(arrDr[0]["STT_KHAM"], 0) < 10)
                    {
                        str = Utility.FormatNumberToString(Utility.Int32Dbnull(arrDr[0]["STT_KHAM"], 0), "00");
                    }
                    UIAction.SetText(lblQMSNumber, str);
                }
                else
                    UIAction.SetText(lblQMSNumber, "00");
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
            lblQMSNumber.BackColor = _QMSProperties.MauB1;
            lblQMSNumber.ForeColor = _QMSProperties.MauF1;
        }
        void InitEvents()
        {
            cmdGetQMS.Click += cmdGetQMS_Click;
            nmrMore.ValueChanged += nmrMore_ValueChanged;
            chkMore.CheckedChanged += chkMore_CheckedChanged;
            chkSoUutien._Oncheck += chkSoUutien__Oncheck;
            mnuReprint.Click += mnuReprint_Click;
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
            LoadSTTKham();
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
                
                int LaysoUutien = chkSoUutien.IsChecked ? 1 : SoUutien;
                if (Reprint)
                {
                    QmsTiepdon objQms = new Select().From(QmsTiepdon.Schema)
                        .Where(QmsTiepdon.Columns.SttKham).IsEqualTo(Utility.Int32Dbnull(lblQMSNumber.Text,0))
                        .And(QmsTiepdon.Columns.LoaiQms).IsEqualTo(LaysoUutien)
                        .And(QmsTiepdon.Columns.IdDichvukcb).IsEqualTo(id_dichvukcb)
                        .ExecuteSingle<QmsTiepdon>();
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
                if (chkMore.Checked)
                    soluong = (int)nmrMore.Value;
                for (int i = 1; i <= soluong; i++)
                {
                    string qmsNumber = "";
                    this.LaySokham(1, lblQMSNumber, Utility.sDbnull(ma_KhoaKcb), ma_doituong_kcb, LaysoUutien, ref qmsNumber);
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
                UIAction._EnableControl(cmdGetQMS, true, "");
            }
        }
        private void LaySokham(int status, TextBox lblQMSNumber, string MaKhoa, string madoituongkcb, int IsUuTien,ref string qmsNumber)
        {
            try
            {
                int num = 0;
                int sttkham = 0;
                StoredProcedure procedure = SPs.VmsQmsTaoso(new int?(status), _QMSProperties.MaQuay, MaKhoa, id_KhoaKcb, madoituongkcb, new int?(num), sttkham, _QMSProperties.PrintStatus ? true : false,0,"DV",0,this.id_kieukham, this.id_phongkham, id_dichvukcb);
                procedure.Execute();
                int SttKham = Utility.Int32Dbnull(procedure.OutputValues[1]);
                UpdateSTTKham(SttKham);
                num = Utility.Int32Dbnull(procedure.OutputValues[0]);
                string str = Utility.sDbnull(num);
                if (Utility.Int32Dbnull(num) < 10)
                {
                    str = Utility.FormatNumberToString(num, "00");
                }
                qmsNumber = str;
                if (_OnCreatedQMSNumber != null)
                    _OnCreatedQMSNumber(str);
                str = Utility.sDbnull(SttKham);
                if (Utility.Int32Dbnull(SttKham) < 10)
                {
                    str = Utility.FormatNumberToString(SttKham, "00");
                }
                lblQMSNumber.Text = Utility.sDbnull(str);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi thực hiện lấy số thứ tự tiếp đón\n" + ex.Message);
            }
        }
        void UpdateSTTKham(int SttKham)
        {
            if (dtSTTKham == null) return;
            DataRow[] arrDr = dtSTTKham.Select(QmsTiepdon.Columns.LoaiQms + "=" + (chkSoUutien.IsChecked ? "1" : SoUutien.ToString()));
            if (arrDr.Length > 0)
            {
                arrDr[0]["STT_KHAM"] = SttKham;
                dtSTTKham.AcceptChanges();
            }
            else
            {
                DataRow newdr = dtSTTKham.NewRow();
                newdr["STT_KHAM"] = SttKham;
                newdr["so_luong"] = Utility.Int32Dbnull(newdr["so_luong"],0) + 1;
                newdr["id_khoakcb"] = id_KhoaKcb;
                newdr["id_phongkham"] = id_phongkham;
                newdr["loai_qms"] = chkSoUutien.IsChecked ? 1 : SoUutien;
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
                if (!string.IsNullOrEmpty(_QMSProperties.PrinterName))
                {
                    crpt_sokham.PrintOptions.PrinterName = Utility.sDbnull(_QMSProperties.PrinterName);
                }
                crpt_sokham.PrintToPrinter(1, false, 0, 0);
                UIAction._EnableControl(button, true, "");
                button.Focus();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi in số khám\n" + ex.Message);
            }
            finally
            {
                UIAction._EnableControl(button, true, "");
            }
        }
        public static string NgayIn(DateTime dt)
        {
            string str = "Ngày ";
            return ((((((str + Strings.Right("0" + dt.Day.ToString(), 2)) + "/" + Strings.Right("0" + dt.Month.ToString(), 2)) + "/" + dt.Year) + " " + Strings.Right("0" + dt.Hour.ToString(), 2)) + ":" + Strings.Right("0" + dt.Minute.ToString(), 2)) + ":" + Strings.Right("0" + dt.Second.ToString(), 2));
        }


    }
}
