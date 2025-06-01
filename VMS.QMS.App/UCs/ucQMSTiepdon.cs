using Microsoft.VisualBasic;
using SubSonic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.QMS.DAL;
using VNS.Libs;
using VNS.Libs.AppUI;
using VNS.QMS;

namespace QMS.UCs
{
    public partial class ucQMSTiepdon : UserControl
    {
        DataRow drQMS;
        private QMSProperties _QMSProperties;
        bool hasLoaded = false;
        private string loai_qms = "";
        private byte muc_uutien = 0;
        bool VisibleHeader = false;
        public bool isPriority = false;
        public ucQMSTiepdon(DataRow drQMS, QMSProperties _QMSProperties,bool VisibleHeader)
        {
            InitializeComponent();
            this.VisibleHeader = VisibleHeader;
            this.drQMS = drQMS;
            this._QMSProperties = _QMSProperties;
            cmdLaysoQMS.Click += CmdLaysoQMS_Click;
        }
        public ucQMSTiepdon()
        {
            InitializeComponent();
        }
        private void CmdLaysoQMS_Click(object sender, EventArgs e)
        {
            LaySoQMS();
        }

        public void Init()
        {
            try
            {

                Refresh();
                loai_qms = Utility.sDbnull(drQMS["loai_qms"]);
                muc_uutien = Utility.ByteDbnull(drQMS["muc_uutien"]);
                cmdLaysoQMS.Text= Utility.sDbnull(drQMS["ten_qms"]);
                LaySokham(0, lblSTT, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh), _QMSProperties.MaDoituongKCB, loai_qms, muc_uutien,Utility.ByteDbnull( isPriority ? 1 : 0));
                LaySoDanggoiQMS(lblSoDanggoi, _QMSProperties.MaQuay, _QMSProperties.MaKhoaKhamBenh, _QMSProperties.MaDoituongKCB, Utility.ByteDbnull(isPriority ? 1 : 0), loai_qms, "NO");
            }
            catch (Exception ex)
            {

            }
        }    
       public void Refresh()
        {
            if(_QMSProperties!=null)
            {
                if(VisibleHeader)
                {
                    this.Height =_QMSProperties.QMSSize.Height + _QMSProperties.QMSHeaderHeight;
                    pnlHeader.Height = _QMSProperties.QMSHeaderHeight;
                }    
               else
                {
                    this.Height = _QMSProperties.QMSSize.Height;
                    pnlHeader.Height = 0;
                }    
                cmdLaysoQMS.Font = _QMSProperties.QMSNameFont;
                cmdLaysoQMS.BackColor = System.Drawing.ColorTranslator.FromHtml(_QMSProperties.QMSBC);
                cmdLaysoQMS.ForeColor = System.Drawing.ColorTranslator.FromHtml(_QMSProperties.QMSFC);

                lblSTT.Font = _QMSProperties.QMSSTTFont;
                lblSTT.BackColor = System.Drawing.ColorTranslator.FromHtml(_QMSProperties.QMSSTTBC);
                lblSTT.ForeColor = System.Drawing.ColorTranslator.FromHtml(_QMSProperties.QMSSTTFC);

                lblSoDanggoi.Font = _QMSProperties.QMSSTTFont;
                lblSoDanggoi.BackColor = System.Drawing.ColorTranslator.FromHtml(_QMSProperties.QMSSODANGGOIBC);
                lblSoDanggoi.ForeColor = System.Drawing.ColorTranslator.FromHtml(_QMSProperties.QMSSODANGGOIFC);
                lblSTT.Width = lblSoDanggoi.Width = _QMSProperties.QMSWidth;

                lblHeaderSodanggoi.Font = lblHeaderSTT.Font = lblHeaderQMS.Font = _QMSProperties.QMSHeaderFont;
                lblHeaderSodanggoi.BackColor = lblHeaderSTT.BackColor = lblHeaderQMS.BackColor = cmdLaysoQMS.BackColor;
                lblHeaderSodanggoi.ForeColor = lblHeaderSTT.ForeColor = lblHeaderQMS.ForeColor = cmdLaysoQMS.ForeColor;
                lblHeaderSodanggoi.Width = lblSoDanggoi.Width;
                lblHeaderSTT.Width = lblSTT.Width;
                lblHeaderQMS.Width = cmdLaysoQMS.Width;
            }    
        }    
        bool Reprint = false;
        void LaySoQMS()
        {
            this.Cursor = Cursors.WaitCursor;
            UIAction._EnableControl(cmdLaysoQMS, false, "");
            bool RestoreStatus = true;
            try
            {
                if (Reprint)
                {
                    QmsTiepdon objQms = new Select().From(QmsTiepdon.Schema)
                        .Where(QmsTiepdon.Columns.SoQms).IsEqualTo(Utility.Int32Dbnull(lblSTT.Text, 0))
                        .And(QmsTiepdon.Columns.LoaiQms).IsEqualTo(loai_qms)
                        .And(QmsTiepdon.Columns.UuTien).IsEqualTo(0)
                        //.And(QmsTiepdon.Columns.MaKhoakcb).IsEqualTo(_QMSProperties.MaKhoaKhamBenh)
                        //.And(QmsTiepdon.Columns.MaDoituongKcb).IsEqualTo("ALL")
                        //.And(QmsTiepdon.Columns.MaDoituongKcb).IsEqualTo("DV")
                        .ExecuteSingle<QmsTiepdon>();
                    if (objQms == null)
                    {
                        Utility.ShowMsg("Số QMS bạn muốn in lại chưa được tạo hoặc không tồn tại. Đề nghị bạn tạo số mới");
                        RestoreStatus = false;
                        return;
                    }

                    this.InPhieuKham(_QMSProperties.TenKhoaKhamBenh, this.cmdLaysoQMS, Utility.sDbnull(objQms.SoQms), cmdLaysoQMS.Text);//  _QMSProperties.Sodichvu);
                    return;
                }
                int soluong = 1;
                //if (chkLaythemsothuong.IsChecked)
                //    soluong = (int)nmrLaythemsothuong.Value;
                for (int i = 1; i <= soluong; i++)
                {
                    this.LaySokham(1, lblSTT, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh), _QMSProperties.MaDoituongKCB, loai_qms,muc_uutien, 0);
                    if (_QMSProperties.PrintStatus)
                    {
                        this.InPhieuKham(_QMSProperties.TenKhoaKhamBenh, this.cmdLaysoQMS, Utility.sDbnull(lblSTT.Text), cmdLaysoQMS.Text);// _QMSProperties.Sodichvu);
                    }
                }
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
                }
                this.Cursor = Cursors.Default;
            }
        }
        public void LaySoDanggoiQMS(Label lbl, string MaQuay, string MaKhoa, string madoituongkcb,  byte uutien, string loaiqms, string loaiqmsbo)
        {

            try
            {
                int SoKham = 0;
                int idDichvukcb = 0;
                long idQMS = 0;
                SoKham = 0;
                idDichvukcb = 0;
                StoredProcedure sp = SPs.VmsQmsLayso(MaQuay, MaKhoa, madoituongkcb, SoKham, idDichvukcb, idQMS, uutien, loaiqms, loaiqmsbo);
                sp.Execute();
                SoKham = Utility.Int32Dbnull(sp.OutputValues[0]);
                idDichvukcb = Utility.Int32Dbnull(sp.OutputValues[1]);
                idQMS = Utility.Int64Dbnull(sp.OutputValues[2]);
                string str = Utility.FormatNumberToString(SoKham, "0000");
                lbl.Text = Utility.sDbnull(str);
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }

        }
        private void LaySokham(int status, Label lbl, string MaKhoa, string madoituongkcb, string loaiQms, byte muc_uutien, byte uutien)
        {
            try
            {
                int num = 0;
                int sttkham = 0;
                StoredProcedure procedure = SPs.VmsQmsTaoso(new int?(status), _QMSProperties.MaQuay, MaKhoa, -1, madoituongkcb, new int?(num), sttkham, _QMSProperties.PrintStatus ? true : false,uutien, loaiQms, muc_uutien, - 1, -1, -1);
                procedure.Execute();

                num = Utility.Int32Dbnull(procedure.OutputValues[0]);
                string str = Utility.sDbnull(num);
                //if (Utility.Int32Dbnull(num) < 10)
                //{
                    str = Utility.FormatNumberToString(num, "0000");
                //}
                lbl.Text = Utility.sDbnull(str);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi thực hiện lấy số thứ tự tiếp đón\n" + ex.Message);
            }
        }
        private void InPhieuKham(string tenkhoa, Button button, string sokham, string tensoQMS)
        {
            try
            {

                string val = tensoQMS;
                this.Cursor = Cursors.WaitCursor;
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
                if (loai_qms == "SOKHAM")
                {
                    crpt_sokham.SetParameterValue("TenKhoa", Utility.sDbnull(tenkhoa));
                }
                else
                {
                    crpt_sokham.SetParameterValue("TenKhoa", Utility.sDbnull(_QMSProperties.TenKhoaKhamBenhKhac));
                }

                crpt_sokham.SetParameterValue("LoaiDoiTuong", val);
                if (loai_qms == "SOKHAM")
                {
                    crpt_sokham.SetParameterValue("NoiChokham", _QMSProperties.QuaySokham);
                }
                else
                {
                    crpt_sokham.SetParameterValue("NoiChokham", _QMSProperties.QuaySokhac);
                }
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
                this.Cursor = Cursors.Default;
            }
        }
        public static string NgayIn(DateTime dt)
        {
            string str = "Ngày ";
            return ((((((str + Strings.Right("0" + dt.Day.ToString(), 2)) + "/" + Strings.Right("0" + dt.Month.ToString(), 2)) + "/" + dt.Year) + " " + Strings.Right("0" + dt.Hour.ToString(), 2)) + ":" + Strings.Right("0" + dt.Minute.ToString(), 2)) + ":" + Strings.Right("0" + dt.Second.ToString(), 2));
        }
    }
}
