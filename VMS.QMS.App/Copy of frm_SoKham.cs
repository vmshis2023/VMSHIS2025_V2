using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Properties;
using VNS.Libs;
using System.IO;
using System.Diagnostics;
using SubSonic;
using Microsoft.VisualBasic;
using VNS.Core.Classes;
using VNS.Libs.AppUI;
using CrystalDecisions.CrystalReports.Engine;
using System.Xml.Serialization;
using QMS;
using VMS.QMS.DAL;

namespace VNS.QMS
{
    public partial class frm_SoKham : Form
    {
        private QMSProperties _QMSProperties;
        bool hasLoaded = false;
        private string loaisoqms = "";
        public frm_SoKham()
        {
            globalVariables.m_strPropertiesFolder = Application.StartupPath + @"\Properties\";
            if (!new ConnectionSQL().ReadConfig())
            {
                Try2ExitApp();
                return;
            }
            InitializeComponent();
            globalVariables.ServerName = PropertyLib._ConfigProperties.DataBaseServer;
            globalVariables.sUName = PropertyLib._ConfigProperties.UID;
            globalVariables.sPwd = PropertyLib._ConfigProperties.PWD;
            globalVariables.sDbName = PropertyLib._ConfigProperties.DataBaseName;

            globalVariables.m_strPropertiesFolder = Application.StartupPath;
            _QMSProperties = GetQMSProperties();
            this.KeyDown += new KeyEventHandler(frm_SoKham_KeyDown);
            Utility.InitSubSonic(new ConnectionSQL().KhoiTaoKetNoi(), "ORM");
            this.Load+=new EventHandler(frm_SoKham_Load);
            CauHinh();
            this.cmdGetSoKham.Click += new EventHandler(this.cmdGetSoKham_Click);
            cmdgetBHYT.Click += new EventHandler(cmdgetBHYT_Click);
            this.cmdKhamUuTien.Click += new EventHandler(this.cmdKhamUuTien_Click);
            this.cmdKhamYC.Click += new EventHandler(this.cmdKhamYC_Click);

            chkLaythemsoYC.CheckedChanged += new EventHandler(chkLaythemsoYC_CheckedChanged);
            chkLaythemsothuong.CheckedChanged += new EventHandler(chkLaythemsothuong_CheckedChanged);
            chkLaythemsouutien.CheckedChanged += new EventHandler(chkLaythemsouutien_CheckedChanged);

            nmrLaythemsothuong.ValueChanged += new EventHandler(nmrLaythemsothuong_ValueChanged);
            nmrLaythemsoUutien.ValueChanged += new EventHandler(nmrLaythemsoUutien_ValueChanged);
            nmrLaythemsoYC.ValueChanged += new EventHandler(nmrLaythemsoYC_ValueChanged);

            cmdConfig.Click+=new EventHandler(cmdConfig_Click);
            mnuReprint.Click += mnuReprint_Click;


            chkLaythemsokhac.CheckedChanged += chkLaythemsokhac_CheckedChanged;
            nmrLaythemsokhac.ValueChanged += nmrLaythemsokhac_ValueChanged;
            cmdSotiemchung.Click += cmdSotiemchung_Click;
            cmdSotiemchung_uutien.Click+=cmdSotiemchung_uutien_Click;
        }

        void cmdSotiemchung_Click(object sender, EventArgs e)
        {
            Utility.WaitNow(this);
            UIAction._EnableControl(cmdSotiemchung, false, "");
            bool RestoreStatus = true;
            try
            {
                loaisoqms = "SOTIEMCHUNG";
                if (Reprint)
                {
                    QmsTiepdon objQms = new Select().From(QmsTiepdon.Schema)
                        .Where(QmsTiepdon.Columns.SoQms).IsEqualTo(Utility.Int32Dbnull(txtsokhac.Text, 0))
                        .And(QmsTiepdon.Columns.LoaiQms).IsEqualTo(2)
                        .And(QmsTiepdon.Columns.UuTien).IsEqualTo(0)
                        .And(QmsTiepdon.Columns.MaKhoakcb).IsEqualTo(_QMSProperties.MaKhoaKhamBenh)
                        .And(QmsTiepdon.Columns.MaDoituongKcb).IsEqualTo("ALL")
                        .ExecuteSingle<QmsTiepdon>();
                    if (objQms == null)
                    {
                        Utility.ShowMsg("Số QMS bạn muốn in lại chưa được tạo hoặc không tồn tại. Đề nghị bạn nhập số khác");
                        RestoreStatus = false;
                        txtsokhac.SelectAll();
                        txtsokhac.Focus();
                        return;
                    }
                    this.InPhieuKham(_QMSProperties.TenKhoaKhamBenh, this.cmdSotiemchung, Utility.sDbnull(objQms.SoQms), _QMSProperties.Sokhac);
                    return;
                }
                int soluong = 1;
                if (chkLaythemsokhac.Checked)
                    soluong = (int)nmrLaythemsokhac.Value;
                for (int i = 1; i <= soluong; i++)
                {
                    this.LaySokham(1, txtsokhac, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh), _QMSProperties.madoituongkhac, 2,0);
                    if (_QMSProperties.PrintStatus)
                    {
                        this.InPhieuKham(_QMSProperties.TenKhoaKhamBenh, this.cmdSotiemchung, Utility.sDbnull(txtsokhac.Text), _QMSProperties.Sokhac);
                    }
                }
                Utility.DefaultNow(this);
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
                    txtsokhac.BackColor = cmdSotiemchung.BackColor;
                    txtsokhac.ReadOnly = true;
                   // cmdSotiemchung.Text = cmdSotiemchung.Tag.ToString();
                    mnuReprint.Checked = false;
                }
                UIAction._EnableControl(cmdSotiemchung, true, "");
            }
        }

        void nmrLaythemsokhac_ValueChanged(object sender, EventArgs e)
        {
            if (!hasLoaded) return;
            lblHelpsokhac.Text = "Nếu bạn nhấn nút " + cmdSotiemchung.Text + ". Hệ thống sẽ tự động in từ số " +
                                 (Utility.Int32Dbnull(txtsokhac.Text, 0) + 1).ToString() + " đến số " +
                                 (Utility.Int32Dbnull(txtsokhac.Text, 0) + nmrLaythemsokhac.Value).ToString();
        }

        void chkLaythemsokhac_CheckedChanged(object sender, EventArgs e)
        {
            nmrLaythemsokhac.Enabled = chkLaythemsokhac.Checked;
            lblHelpsokhac.Visible = chkLaythemsokhac.Checked;
            if (chkLaythemsokhac.Checked)
            {
                lblHelpsokhac.Text = "Nếu bạn nhấn nút " + cmdSotiemchung.Text + ". Hệ thống sẽ tự động in từ số " +
                                     (Utility.Int32Dbnull(txtsokhac.Text, 0) + 1).ToString() + " đến số " +
                                     (Utility.Int32Dbnull(txtsokhac.Text, 0) + nmrLaythemsokhac.Value).ToString();
                nmrLaythemsokhac.Focus();
            }
            else
            {
                cmdSotiemchung.Focus();
            }
        }
        bool Reprint = false;
        void mnuReprint_Click(object sender, EventArgs e)
        {
            string ctrlName = ((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl.Name;
            if (ctrlName == txtSoKham.Name)
            {
                if (mnuReprint.Checked)
                {
                    txtSoKham.ReadOnly = false;
                    txtSoKham.BackColor = Color.White;
                    Reprint = true;
                    cmdGetSoKham.Text = "In lại";
                }
                else
                {
                    Reprint = false;
                    txtSoKham.BackColor = cmdGetSoKham.BackColor;
                    txtSoKham.ReadOnly = true;
                    cmdGetSoKham.Text = cmdGetSoKham.Tag.ToString();
                }
            }
            else if (ctrlName == txtSoKhamBHYT.Name)
            {
                if (mnuReprint.Checked)
                {
                    txtSoKhamBHYT.ReadOnly = false;
                    txtSoKhamBHYT.BackColor = Color.White;
                    Reprint = true;
                    cmdgetBHYT.Text = "In lại";
                }
                else
                {
                    Reprint = false;
                    txtSoKhamBHYT.BackColor = cmdgetBHYT.BackColor;
                    txtSoKhamBHYT.ReadOnly = true;
                    cmdgetBHYT.Text = cmdgetBHYT.Tag.ToString();
                }
            }
            else if (ctrlName == txtSoKhamUuTien.Name)
            {
                if (mnuReprint.Checked)
                {
                    txtSoKhamUuTien.ReadOnly = false;
                    txtSoKhamUuTien.BackColor = Color.White;
                    Reprint = true;
                    cmdKhamUuTien.Text = "In lại";
                }
                else
                {
                    Reprint = false;
                    txtSoKhamUuTien.BackColor = cmdKhamUuTien.BackColor;
                    txtSoKhamUuTien.ReadOnly = true;
                    cmdKhamUuTien.Text = cmdKhamUuTien.Tag.ToString();
                }
            }
            else if (ctrlName == txtSoKhamYeuCau.Name)
            {
                if (mnuReprint.Checked)
                {
                    txtSoKhamYeuCau.ReadOnly = false;
                    txtSoKhamYeuCau.BackColor = Color.White;
                    Reprint = true;
                    cmdKhamYC.Text = "In lại";
                }
                else
                {
                    Reprint = false;
                    txtSoKhamYeuCau.BackColor = cmdKhamYC.BackColor;
                    txtSoKhamYeuCau.ReadOnly = true;
                    cmdKhamYC.Text = cmdKhamYC.Tag.ToString();
                }
            }
            else if (ctrlName == txtsokhac.Name)
            {
                if (mnuReprint.Checked)
                {
                    txtsokhac.ReadOnly = false;
                    txtsokhac.BackColor = Color.White;
                    Reprint = true;
                    cmdSotiemchung.Text = "In lại";
                }
                else
                {
                    Reprint = false;
                    txtsokhac.BackColor = cmdSotiemchung.BackColor;
                    txtsokhac.ReadOnly = true;
                    cmdSotiemchung.Text = cmdSotiemchung.Tag.ToString();
                }
            }
            else if (ctrlName == txtsokhacUutien.Name)
            {
                if (mnuReprint.Checked)
                {
                    txtsokhacUutien.ReadOnly = false;
                    txtsokhacUutien.BackColor = Color.White;
                    Reprint = true;
                    cmdSotiemchung_uutien.Text = "In lại";
                }
                else
                {
                    Reprint = false;
                    txtsokhacUutien.BackColor = cmdSotiemchung_uutien.BackColor;
                    txtsokhacUutien.ReadOnly = true;
                    cmdSotiemchung_uutien.Text = cmdSotiemchung_uutien.Tag.ToString();
                }
            }
        }

     

        void nmrLaythemsoYC_ValueChanged(object sender, EventArgs e)
        {
            if (!hasLoaded) return;
            lblHelpsoYC.Text = "Nếu bạn nhấn nút Lấy số yêu cầu. Hệ thống sẽ tự động in từ số " +
                               (Utility.Int32Dbnull(txtSoKhamYeuCau.Text, 0) + 1).ToString() + " đến số " +
                               (Utility.Int32Dbnull(txtSoKhamYeuCau.Text, 0) + nmrLaythemsoYC.Value).ToString();
        }

        void nmrLaythemsoUutien_ValueChanged(object sender, EventArgs e)
        {
            if (!hasLoaded) return;
            lblHelpUutien.Text = "Nếu bạn nhấn nút Lấy số ưu tiên. Hệ thống sẽ tự động in từ số " +
                                 (Utility.Int32Dbnull(txtSoKhamUuTien.Text, 0) + 1).ToString() + " đến số " +
                                 (Utility.Int32Dbnull(txtSoKhamUuTien.Text, 0) + nmrLaythemsoUutien.Value).ToString();
        }

        void nmrLaythemsothuong_ValueChanged(object sender, EventArgs e)
        {
            if (!hasLoaded) return;
            lblHelpsothuong.Text = "Nếu bạn nhấn nút Lấy số thường. Hệ thống sẽ tự động in từ số " +
                                   (Utility.Int32Dbnull(txtSoKham.Text, 0) + 1).ToString() + " đến số " +
                                   (Utility.Int32Dbnull(txtSoKham.Text, 0) + nmrLaythemsothuong.Value).ToString();
        }
        public static QMSProperties GetQMSProperties()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new QMSProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (QMSProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi đọc cấu hình Properties\n"+ex.Message);
                return new QMSProperties();
            }
        }
        void linkLabel1_Click(object sender, EventArgs e)
        {
           
        }

        void chkLaythemsouutien_CheckedChanged(object sender, EventArgs e)
        {
            nmrLaythemsoUutien.Enabled = chkLaythemsouutien.Checked;
            lblHelpUutien.Visible = chkLaythemsouutien.Checked;
            if (chkLaythemsouutien.Checked)
            {
                lblHelpUutien.Text = "Nếu bạn nhấn nút Lấy số ưu tiên. Hệ thống sẽ tự động in từ số " +
                                     (Utility.Int32Dbnull(txtSoKhamUuTien.Text, 0) + 1).ToString() + " đến số " +
                                     (Utility.Int32Dbnull(txtSoKhamUuTien.Text, 0) + nmrLaythemsoUutien.Value).ToString();
                nmrLaythemsoUutien.Focus();
            }
            else
            {
                cmdKhamUuTien.Focus();
            }
        }

        void chkLaythemsothuong_CheckedChanged(object sender, EventArgs e)
        {
            nmrLaythemsothuong.Enabled = chkLaythemsothuong.Checked;
            lblHelpsothuong.Visible = chkLaythemsothuong.Checked;
            if (chkLaythemsothuong.Checked)
            {
                lblHelpsothuong.Text = "Nếu bạn nhấn nút Lấy số thường. Hệ thống sẽ tự động in từ số " +
                                       (Utility.Int32Dbnull(txtSoKham.Text, 0) + 1).ToString() + " đến số " +
                                       (Utility.Int32Dbnull(txtSoKham.Text, 0) + nmrLaythemsothuong.Value).ToString();
                nmrLaythemsothuong.Focus();
            }
            else
            {
                cmdGetSoKham.Focus();
            }
        }

        void chkLaythemsoYC_CheckedChanged(object sender, EventArgs e)
        {
            nmrLaythemsoYC.Enabled = chkLaythemsoYC.Checked;
            lblHelpsoYC.Visible = chkLaythemsoYC.Checked;
            if (chkLaythemsoYC.Checked)
            {
                lblHelpsoYC.Text = "Nếu bạn nhấn nút Lấy số yêu cầu. Hệ thống sẽ tự động in từ số " +
                                   (Utility.Int32Dbnull(txtSoKhamYeuCau.Text, 0) + 1).ToString() + " đến số " +
                                   (Utility.Int32Dbnull(txtSoKhamYeuCau.Text, 0) + nmrLaythemsoYC.Value).ToString();
                nmrLaythemsoYC.Focus();
            }
            else
            {
                cmdKhamYC.Focus();
            }
        }

        void frm_SoKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                this.LaySokham(0, txtSoKham, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh), _QMSProperties.madoituongdichvu,0, 0);
                this.LaySokham(0, txtSoKhamBHYT, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh), _QMSProperties.madoituongbhyt,0, 0);
                this.LaySokham(0, txtSoKhamUuTien, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh), "ALL",0, 1);
                this.LaySokham(0, txtSoKhamYeuCau, Utility.sDbnull(_QMSProperties.MaKhoaYeuCau), "ALL", 0,0);
                this.LaySokham(0, txtsokhac, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh), _QMSProperties.madoituongkhac, 2, 0);
                this.LaySokham(0, txtsokhacUutien, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh), _QMSProperties.madoituongkhac, 2, 1);
            }
            if (e.KeyCode == Keys.F1 && _QMSProperties.EnableFKey)
                cmdGetSoKham_Click(cmdGetSoKham, new EventArgs());
            if (e.KeyCode == Keys.F6 && _QMSProperties.EnableFKey)
                cmdKhamYC_Click(cmdKhamYC, new EventArgs());
            if (e.KeyCode == Keys.F2 && _QMSProperties.EnableFKey)
                cmdKhamUuTien_Click(cmdKhamUuTien, new EventArgs());
            if (e.KeyCode == Keys.F3 && _QMSProperties.EnableFKey)
                cmdSotiemchung_Click(cmdSotiemchung, new EventArgs());
            if (e.KeyCode == Keys.F4 && _QMSProperties.EnableFKey)
                cmdKhamUuTien_Click(cmdKhamUuTien, new EventArgs());
            if (e.KeyCode == Keys.F5 && _QMSProperties.EnableFKey)
                cmdgetBHYT_Click(cmdgetBHYT, new EventArgs());
        }
        void Try2ExitApp()
        {
            try
            {
                this.Close();
                this.Dispose();
                Application.Exit();
            }
            catch
            {
            }
        }
      

        private void CauHinh()
        {
            try
            {
                UIAction.SetText(this.lblMessage, _QMSProperties.TenBenhVien);
                //txtSoKham.BackColor = _QMSProperties.MauB1;
                //txtSoKham.ForeColor = _QMSProperties.MauF1;

                //txtSoKhamYeuCau.BackColor = _QMSProperties.MauB2;
                //txtSoKhamYeuCau.ForeColor = _QMSProperties.MauF2;

                //txtSoKhamUuTien.BackColor = _QMSProperties.MauB3;
                //txtSoKhamUuTien.ForeColor = _QMSProperties.MauF3;

                pnlBHYT.Visible = _QMSProperties.hienthiLaysoBHYT;
                pnlSoDVthuong.Visible = _QMSProperties.hienthiLaysoDV;
                pnlSokhac.Visible = _QMSProperties.hienthiLaysokhac;
                pnlUutien.Visible = _QMSProperties.hienthiLaysoUutien;
                pnlSokhacUutien.Visible = _QMSProperties.hienthiLaysokhacUutien;

                cmdGetSoKham.Text = _QMSProperties.tensodichvu;
                cmdKhamUuTien.Text = _QMSProperties.tensouutien;
                cmdSotiemchung.Text = _QMSProperties.tensokhac;
                cmdSotiemchung_uutien.Text = _QMSProperties.tensokhacUutien;
                cmdgetBHYT.Text = _QMSProperties.tensobhyt;
            }
            catch (Exception ex)
            {
                
                
            }
           
        }

        

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(_QMSProperties);
            frm.ShowDialog();
            CauHinh();
        }
        void cmdgetBHYT_Click(object sender, EventArgs e)
        {
            Utility.WaitNow(this);
            UIAction._EnableControl(cmdgetBHYT, false, "");
            bool RestoreStatus = true;
            try
            {
                if (Reprint)
                {
                    QmsTiepdon objQms = new Select().From(QmsTiepdon.Schema)
                        .Where(QmsTiepdon.Columns.SoQms).IsEqualTo(Utility.Int32Dbnull(txtSoKhamBHYT.Text, 0))
                        .And(QmsTiepdon.Columns.LoaiQms).IsEqualTo(0)
                        .And(QmsTiepdon.Columns.UuTien).IsEqualTo(0)
                        .And(QmsTiepdon.Columns.MaKhoakcb).IsEqualTo(_QMSProperties.MaKhoaKhamBenh)
                        .And(QmsTiepdon.Columns.MaDoituongKcb).IsEqualTo("ALL")
                        //.And(QmsTiepdon.Columns.MaDoituongKcb).IsEqualTo("BHYT")
                        .ExecuteSingle<QmsTiepdon>();
                    if (objQms == null)
                    {
                        Utility.ShowMsg("Số QMS bạn muốn in lại chưa được tạo hoặc không tồn tại. Đề nghị bạn nhập số khác");
                        RestoreStatus = false;
                        txtSoKhamBHYT.SelectAll();
                        txtSoKhamBHYT.Focus();
                        return;
                    }
                    this.InPhieuKham(_QMSProperties.TenKhoaKhamBenh, this.cmdgetBHYT, Utility.sDbnull(objQms.SoQms), _QMSProperties.Sobhyt);
                    return;
                }

                int soluong = 1;
                if (chkLaythemsoBhyt.Checked)
                    soluong = (int)nmrLaythemsoBHYT.Value;
                for (int i = 1; i <= soluong; i++)
                {
                    this.LaySokham(1, txtSoKhamBHYT, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh), _QMSProperties.madoituongbhyt, 0,0);
                    if (_QMSProperties.PrintStatus)
                    {
                        this.InPhieuKham(_QMSProperties.TenKhoaKhamBenh, this.cmdgetBHYT, Utility.sDbnull(txtSoKhamBHYT.Text), _QMSProperties.Sobhyt);
                    }
                }
                Utility.DefaultNow(this);
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
                    txtSoKhamBHYT.BackColor = cmdgetBHYT.BackColor;
                    txtSoKhamBHYT.ReadOnly = true;
                    cmdgetBHYT.Text = cmdgetBHYT.Tag.ToString();
                    mnuReprint.Checked = false;
                }
                UIAction._EnableControl(cmdgetBHYT, true, "");
            }
        }
        private void cmdGetSoKham_Click(object sender, EventArgs e)
        {
            Utility.WaitNow(this);
            UIAction._EnableControl(cmdGetSoKham, false, "");
            bool RestoreStatus = true;
            try
            {
                loaisoqms = "SOKHAM";
                if (Reprint)
                {
                    QmsTiepdon objQms = new Select().From(QmsTiepdon.Schema)
                        .Where(QmsTiepdon.Columns.SoQms).IsEqualTo(Utility.Int32Dbnull(txtSoKham.Text, 0))
                        .And(QmsTiepdon.Columns.LoaiQms).IsEqualTo(0)
                        .And(QmsTiepdon.Columns.UuTien).IsEqualTo(0)
                        .And(QmsTiepdon.Columns.MaKhoakcb).IsEqualTo(_QMSProperties.MaKhoaKhamBenh)
                        .And(QmsTiepdon.Columns.MaDoituongKcb).IsEqualTo("ALL")
                        //.And(QmsTiepdon.Columns.MaDoituongKcb).IsEqualTo("DV")
                        .ExecuteSingle<QmsTiepdon>();
                    if (objQms == null)
                    {
                        Utility.ShowMsg("Số QMS bạn muốn in lại chưa được tạo hoặc không tồn tại. Đề nghị bạn nhập số khác");
                        RestoreStatus = false;
                        txtSoKham.SelectAll();
                        txtSoKham.Focus();
                        return;
                    }
                    
                    this.InPhieuKham(_QMSProperties.TenKhoaKhamBenh, this.cmdGetSoKham, Utility.sDbnull(objQms.SoQms), _QMSProperties.Sodichvu);
                    return;
                }
                int soluong = 1;
                if (chkLaythemsothuong.Checked)
                    soluong = (int)nmrLaythemsothuong.Value;
                for (int i = 1; i <= soluong; i++)
                {
                    this.LaySokham(1, txtSoKham, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh), _QMSProperties.madoituongdichvu,0, 0);
                    if (_QMSProperties.PrintStatus)
                    {
                        this.InPhieuKham(_QMSProperties.TenKhoaKhamBenh, this.cmdGetSoKham, Utility.sDbnull(txtSoKham.Text), _QMSProperties.Sodichvu);
                    }
                }
                Utility.DefaultNow(this);
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
                    txtSoKham.BackColor = cmdGetSoKham.BackColor;
                    txtSoKham.ReadOnly = true;
                   // cmdGetSoKham.Text = cmdGetSoKham.Tag.ToString();
                    mnuReprint.Checked = false;
                }
                UIAction._EnableControl(cmdGetSoKham, true, "");
            }
        }
        private void cmdKhamYC_Click(object sender, EventArgs e)
        {
            Utility.WaitNow(this);
            UIAction._EnableControl(cmdKhamYC, false, "");
            bool RestoreStatus = true;
            try
            {
                if (Reprint)
                {
                    QmsTiepdon objQms = new Select().From(QmsTiepdon.Schema)
                        .Where(QmsTiepdon.Columns.SoQms).IsEqualTo(Utility.Int32Dbnull(txtSoKhamYeuCau.Text, 0))
                        .And(QmsTiepdon.Columns.LoaiQms).IsEqualTo(0)
                        .And(QmsTiepdon.Columns.MaKhoakcb).IsEqualTo(_QMSProperties.MaKhoaKhamBenh)
                        .And(QmsTiepdon.Columns.MaDoituongKcb).IsEqualTo("ALL")
                        .ExecuteSingle<QmsTiepdon>();
                    if (objQms == null)
                    {
                        Utility.ShowMsg("Số QMS bạn muốn in lại chưa được tạo hoặc không tồn tại. Đề nghị bạn nhập số khác");
                        RestoreStatus = false;
                        txtSoKhamYeuCau.SelectAll();
                        txtSoKhamYeuCau.Focus();
                        return;
                    }
                    this.InPhieuKham(_QMSProperties.TenKhoaKhamBenh, this.cmdKhamYC, Utility.sDbnull(objQms.SoQms), _QMSProperties.SoYeucau);
                    return;
                }
                int soluong = 1;
                if (chkLaythemsoYC.Checked)
                    soluong = (int)nmrLaythemsoYC.Value;
                for (int i = 1; i <= soluong; i++)
                {
                    this.LaySokham(1, txtSoKhamYeuCau, Utility.sDbnull(_QMSProperties.MaKhoaYeuCau), "ALL", 0,0);
                    if (_QMSProperties.PrintStatus)
                    {
                        this.InPhieuKham(_QMSProperties.TenKhoaYeuCau, this.cmdKhamYC, Utility.sDbnull(this.txtSoKhamYeuCau.Text), _QMSProperties.SoYeucau);
                    }
                }
                Utility.DefaultNow(this);
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
                    txtSoKhamYeuCau.BackColor = cmdKhamYC.BackColor;
                    txtSoKhamYeuCau.ReadOnly = true;
                  //  cmdKhamYC.Text = cmdKhamYC.Tag.ToString();
                    mnuReprint.Checked = false;
                }
                UIAction._EnableControl(cmdKhamYC, true, "");
            }
        }
        private void cmdKhamUuTien_Click(object sender, EventArgs e)
        {
            Utility.WaitNow(this);
            UIAction._EnableControl(cmdKhamUuTien, false, "");
            bool RestoreStatus = true;
            try
            {
                loaisoqms = "SOKHAM";
                if (Reprint)
                {
                    QmsTiepdon objQms = new Select().From(QmsTiepdon.Schema)
                        .Where(QmsTiepdon.Columns.SoQms).IsEqualTo(Utility.Int32Dbnull(txtSoKhamUuTien.Text, 0))
                        .And(QmsTiepdon.Columns.LoaiQms).IsEqualTo(0)
                        .And(QmsTiepdon.Columns.MaKhoakcb).IsEqualTo(_QMSProperties.MaKhoaKhamBenh)
                        .And(QmsTiepdon.Columns.MaDoituongKcb).IsEqualTo("ALL")
                        .ExecuteSingle<QmsTiepdon>();
                    if (objQms == null)
                    {
                        Utility.ShowMsg("Số QMS bạn muốn in lại chưa được tạo hoặc không tồn tại. Đề nghị bạn nhập số khác");
                        RestoreStatus = false;
                        txtSoKhamUuTien.SelectAll();
                        txtSoKhamUuTien.Focus();
                        return;
                    }
                    this.InPhieuKham(_QMSProperties.TenKhoaKhamBenh, this.cmdKhamUuTien, Utility.sDbnull(objQms.SoQms), _QMSProperties.Souutien);
                    return;
                }
                int soluong = 1;
                if (chkLaythemsouutien.Checked)
                    soluong = (int)nmrLaythemsoUutien.Value;
                for (int i = 1; i <= soluong; i++)
                {
                    this.LaySokham(1, txtSoKhamUuTien, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh), _QMSProperties.madoituonguutien, 0,1);
                    if (_QMSProperties.PrintStatus)
                    {
                        this.InPhieuKham(_QMSProperties.TenKhoaKhamBenh, this.cmdKhamUuTien, Utility.sDbnull(txtSoKhamUuTien.Text), _QMSProperties.Souutien);
                    }
                }
                Utility.DefaultNow(this);
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
                    txtSoKhamUuTien.BackColor = cmdKhamUuTien.BackColor;
                    txtSoKhamUuTien.ReadOnly = true;
                  //  cmdKhamUuTien.Text = cmdKhamUuTien.Tag.ToString();
                    mnuReprint.Checked = false;
                }
                UIAction._EnableControl(cmdKhamUuTien, true, "");
            }
        }
        private void cmdSotiemchung_uutien_Click(object sender, EventArgs e)
        {
            Utility.WaitNow(this);
            UIAction._EnableControl(cmdSotiemchung_uutien, false, "");
            bool RestoreStatus = true;
            try
            {
                loaisoqms = "SOTIEMCHUNG";
                if (Reprint)
                {
                    QmsTiepdon objQms = new Select().From(QmsTiepdon.Schema)
                        .Where(QmsTiepdon.Columns.SoQms).IsEqualTo(Utility.Int32Dbnull(txtsokhacUutien.Text, 0))
                        .And(QmsTiepdon.Columns.LoaiQms).IsEqualTo(2)
                        .And(QmsTiepdon.Columns.UuTien).IsEqualTo(1)
                        .And(QmsTiepdon.Columns.MaKhoakcb).IsEqualTo(_QMSProperties.MaKhoaKhamBenh)
                        .And(QmsTiepdon.Columns.MaDoituongKcb).IsEqualTo("ALL")
                        .ExecuteSingle<QmsTiepdon>();
                    if (objQms == null)
                    {
                        Utility.ShowMsg("Số QMS bạn muốn in lại chưa được tạo hoặc không tồn tại. Đề nghị bạn nhập số khác");
                        RestoreStatus = false;
                        txtsokhacUutien.SelectAll();
                        txtsokhacUutien.Focus();
                        return;
                    }
                    this.InPhieuKham(_QMSProperties.TenKhoaKhamBenh, this.cmdSotiemchung_uutien, Utility.sDbnull(objQms.SoQms), _QMSProperties.SokhacUutien);
                    return;
                }
                int soluong = 1;
                if (chkLaythemsokhacUutien.Checked)
                    soluong = (int)nmrLaythemsokhacUutien.Value;
                for (int i = 1; i <= soluong; i++)
                {
                    this.LaySokham(1, txtsokhacUutien, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh), _QMSProperties.madoituongkhac, 2,1);
                    if (_QMSProperties.PrintStatus)
                    {
                        this.InPhieuKham(_QMSProperties.TenKhoaKhamBenh, this.cmdSotiemchung_uutien, Utility.sDbnull(txtsokhacUutien.Text), _QMSProperties.SokhacUutien);
                    }
                }
                Utility.DefaultNow(this);
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
                    txtsokhacUutien.BackColor = cmdSotiemchung_uutien.BackColor;
                    txtsokhacUutien.ReadOnly = true;
                   // cmdSotiemchung_uutien.Text = cmdSotiemchung_uutien.Tag.ToString();
                    mnuReprint.Checked = false;
                }
                UIAction._EnableControl(cmdSotiemchung_uutien, true, "");
            }
        }
        private void EmptyFolderContents(string folderName)
        {
            try
            {
                Exception exception;
                foreach (string str in Directory.GetDirectories(folderName))
                {
                    try
                    {
                        Directory.Delete(str, true);
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        Debug.WriteLine(exception);
                    }
                }
                foreach (string str2 in Directory.GetFiles(folderName))
                {
                    try
                    {
                        File.Delete(str2);
                    }
                    catch (Exception exception2)
                    {
                        exception = exception2;
                        Debug.WriteLine(exception);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void frm_SoKham_Load(object sender, EventArgs e)
        {
            this.LaySokham(0, txtSoKham, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh), _QMSProperties.madoituongdichvu, 0, 0);
           this.LaySokham(0, txtSoKhamUuTien, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh), "ALL",0,1);
           this.LaySokham(0, txtSoKhamYeuCau, Utility.sDbnull(_QMSProperties.MaKhoaYeuCau),"ALL", 0,0);
           this.LaySokham(0, txtSoKhamBHYT, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh), _QMSProperties.madoituongbhyt, 0, 0);
           this.LaySokham(0, txtsokhac, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh), _QMSProperties.madoituongkhac, 2,0);
           this.LaySokham(0, txtsokhacUutien, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh), _QMSProperties.madoituongkhac, 2, 1);
           hasLoaded = true;
        }
        private void InPhieuKham(string tenkhoa, Button button, string sokham, string tensoQMS)
        {
            try
            {

                string val = tensoQMS;
                Utility.WaitNow(this);
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
                if (loaisoqms == "SOKHAM")
                {
                    crpt_sokham.SetParameterValue("TenKhoa", Utility.sDbnull(tenkhoa));
                }
                else
                {
                    crpt_sokham.SetParameterValue("TenKhoa", Utility.sDbnull(_QMSProperties.TenKhoaKhamBenhKhac));
                }
               
                crpt_sokham.SetParameterValue("LoaiDoiTuong", val);
                if(loaisoqms=="SOKHAM")
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
                Utility.DefaultNow(this);
            }
        }

       

        private void LaySokham(int status,TextBox txt, string MaKhoa,string madoituongkcb, int loaiQms,byte uutien)
        {
            try
            {
                int num = 0;
                int sttkham = 0;
                StoredProcedure procedure = SPs.VmsQmsTaoso(new int?(status), _QMSProperties.MaQuay, MaKhoa, -1, madoituongkcb, new int?(num), sttkham, _QMSProperties.PrintStatus ? true : false,uutien, loaiQms, -1, -1, -1);
                procedure.Execute();
               
                num = Utility.Int32Dbnull(procedure.OutputValues[0]);
                string str = Utility.sDbnull(num);
                if (Utility.Int32Dbnull(num) < 10)
                {
                    str = Utility.FormatNumberToString(num, "00");
                }
                txt.Text = Utility.sDbnull(str);
            }
            catch(Exception ex)
            {
                Utility.ShowMsg("Lỗi khi thực hiện lấy số thứ tự tiếp đón\n"+ex.Message);
            }
        }

        public static string NgayIn(DateTime dt)
        {
            string str = "Ngày ";
            return ((((((str + Strings.Right("0" + dt.Day.ToString(), 2)) + "/" + Strings.Right("0" + dt.Month.ToString(), 2)) + "/" + dt.Year) + " " + Strings.Right("0" + dt.Hour.ToString(), 2)) + ":" + Strings.Right("0" + dt.Minute.ToString(), 2)) + ":" + Strings.Right("0" + dt.Second.ToString(), 2));
        }

        private void cmdGetSoKham_Click_1(object sender, EventArgs e)
        {

        }

    }
}
