using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using SubSonic;
using VMS.QMS.DAL;
using QMS.UCs;
using VNS.QMS;
using VNS.Core.Classes;
using System.IO;
using System.Xml.Serialization;
using System.Configuration;
using VB = Microsoft.VisualBasic;
using VNS.Properties;
namespace QMS
{
    public partial class frm_QMSPCN_Layso : Form
    {
        private QMSProperties _QMSProperties;
        string ma_khoakcb = "";
        string ma_phong = "";
        string ma_chucnang = "";
        public frm_QMSPCN_Layso()
        {
            globalVariables.m_strPropertiesFolder = Application.StartupPath + @"\Properties\";
            if (!new ConnectionSQL().ReadConfig())
            {
                Try2ExitApp();
                return;
            }
            InitializeComponent();
            //Utility.SetVisualStyle(this);
            this.Load += frm_QMSPCN_Layso_Load;
            this.KeyDown += frm_QMSPCN_Layso_KeyDown;
            //Utility.InitSubSonic(new ConnectionSQL().KhoiTaoKetNoi(), "ORM");
            globalVariables.m_strPropertiesFolder = Application.StartupPath;
            _QMSProperties = GetQMSProperties();
            txtMachidinh.KeyDown += txtMachidinh_KeyDown;
            txtMachidinh.GotFocus += txtMachidinh_GotFocus;
        }

        void frm_QMSPCN_Layso_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtMachidinh.Focus();
                txtMachidinh.SelectAll();
            }
            else if (e.KeyCode == Keys.F5)
            {
                ReadConfig();
            }
        }

        void txtMachidinh_GotFocus(object sender, EventArgs e)
        {
            txtMachidinh.SelectAll();
        }

        void frm_QMSPCN_Layso_Load(object sender, EventArgs e)
        {
            ReadConfig(); 
        }
        void ReadConfig()
        {
            try
            {
                ma_khoakcb = Utility.sDbnull(ConfigurationManager.AppSettings["ma_khoakcb"], "KKB");
                ma_phong = Utility.sDbnull(ConfigurationManager.AppSettings["ma_phong"], "PSA");
                ma_chucnang = Utility.sDbnull(ConfigurationManager.AppSettings["ma_chucnang"], "SA,SAT");
                txtMachidinh.MaxLength = Utility.Int32Dbnull(ConfigurationManager.AppSettings["dodai_ma"], 11);
                this.Text = Utility.sDbnull(ConfigurationManager.AppSettings["Title"], "HỆ THỐNG LẤY SỐ KHOA CĐHA");

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
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
                Utility.ShowMsg("Lỗi khi đọc cấu hình Properties\n" + ex.Message);
                return new QMSProperties();
            }
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
      
        void txtMachidinh_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter )
                {
                    if (Utility.DoTrim(txtMachidinh.Text).Length != txtMachidinh.MaxLength)
                        txtMachidinh.Text = string.Format("{0}.{1}", DateTime.Now.ToString("yyMMdd"), VB.Strings.Right("0000" + Utility.DoTrim(txtMachidinh.Text), 4));
                    if (Utility.DoTrim(txtMachidinh.Text).Length == txtMachidinh.MaxLength)
                        BuildPCN();

                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
           
        }

        void BuildPCN()
        {
            try
            {
                txtma_luotkham.Clear();
                txttenbenhnhan.Clear();
                txtTuoi.Clear();
                txtGioitinh.Clear();
                txtDiaChi.Clear();
                flowPCN.Controls.Clear();
                DataSet dsData = SPs.QmsLaycacphongchucnang(ma_khoakcb, Utility.DoTrim(txtMachidinh.Text), ma_phong, ma_chucnang).GetDataSet();
                Utility.SetDataSourceForDataGridEx_Basic(grdAssignDetail, dsData.Tables[0], true, true, "", "");
                DataRow drBenhnhan = null;
                if (dsData.Tables[1].Rows.Count > 0)
                    drBenhnhan = dsData.Tables[1].Rows[0];
                if (drBenhnhan == null) return;
                txtma_luotkham.Text = Utility.sDbnull(drBenhnhan["ma_luotkham"], "UKN");
                txttenbenhnhan.Text = Utility.sDbnull(drBenhnhan["ten_benhnhan"], "UKN");
                txtTuoi.Text = Utility.sDbnull(drBenhnhan["nam_sinh"], "0000");
                txtGioitinh.Text = Utility.sDbnull(drBenhnhan["gioi_tinh"], "UKN");
                txtDiaChi.Text = Utility.sDbnull(drBenhnhan["dia_chi"], "");

                DataTable dtPCN = dsData.Tables[2];
                flowPCN.SuspendLayout();

                foreach (DataRow dr in dtPCN.Rows)
                {
                    ucQMSPCN_Item _item = new ucQMSPCN_Item();//ma_khoakcb, ma_phong, Utility.DoTrim(txtMachidinh.Text), ma_chucnang,Utility.sDbnull( dr["ten_khoaphong"],"Không xác định"), _QMSProperties, 0);
                    _item.Init(dr, drBenhnhan, ma_khoakcb, Utility.sDbnull(dr["ma_phong_stt"], ma_phong), Utility.DoTrim(txtMachidinh.Text), Convert.ToDateTime(dr["ngay_chidinh"]), Utility.sDbnull(dr["ma_dichvu"], ma_chucnang), Utility.sDbnull(dr["ten_khoaphong"], "Không xác định"), _QMSProperties, 0);
                    _item._OnCreatedQMSNumber += _item__OnCreatedQMSNumber;
                    flowPCN.Controls.Add(_item);
                }
                flowPCN.ResumeLayout();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void _item__OnCreatedQMSNumber(string QmsNum, string ma_chucnang)
        {
            try
            {
                foreach (UserControl ucs in flowPCN.Controls)
                {
                    ucQMSPCN_Item _item = ucs as ucQMSPCN_Item;
                    if (_item.ma_chucnang == ma_chucnang)
                    {
                        _item.hasQMS = true;
                        _item.cmdGetQMS.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(_QMSProperties);
            frm.ShowDialog();
        }
    }
}
