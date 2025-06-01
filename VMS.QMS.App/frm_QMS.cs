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
using System.Xml.Serialization;
using QMS;
using QMS.UCs;
using VMS.HIS.DAL;

namespace VNS.QMS
{
    public partial class frm_QMS : Form
    {
        private QMSProperties _QMSProperties;
        bool hasLoaded = false;
        public frm_QMS()
        {
            globalVariables.m_strPropertiesFolder = Application.StartupPath + @"\Properties\";
            if (!new ConnectionSQL().ReadConfig())
            {
                Try2ExitApp();
                return;
            }
            InitializeComponent();
            tabkhoaKcb.TabPages.Clear();
            globalVariables.m_strPropertiesFolder = Application.StartupPath;
            _QMSProperties = GetQMSProperties();
            this.KeyDown += new KeyEventHandler(frm_QMS_KeyDown);
            Utility.InitSubSonic(new ConnectionSQL().KhoiTaoKetNoi(), "ORM");
            this.Load+=new EventHandler(frm_QMS_Load);
            CauHinh();
            cmdConfig.Click+=new EventHandler(cmdConfig_Click);
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

        void InitControls()
        {
            try
            {
               tabkhoaKcb.TabPages.Clear();
               DataTable dtKhoaKcb= Laydanhmuckhoa("NGOAI",0);
               foreach (DataRow dr in dtKhoaKcb.Rows)
               {
                   DataTable dtData=SPs.QmsLayDanhsachbuongkham(Utility.Int32Dbnull(dr[DmucKhoaphong.Columns.IdKhoaphong])).GetDataSet().Tables[0];
                   DataTable dtSttKham = SPs.QmsLayMaxSTTKham(Utility.Int32Dbnull(dr[DmucKhoaphong.Columns.IdKhoaphong]), -1).GetDataSet().Tables[0];
                   ucQMS _ucQMS = new ucQMS();
                   _ucQMS.Init(dtData,dtSttKham, Utility.Int32Dbnull(dr[DmucKhoaphong.Columns.IdKhoaphong]), _QMSProperties, "ALL");
                   TabPage _newPage = new TabPage(Utility.sDbnull(dr[DmucKhoaphong.Columns.TenKhoaphong]));
                  
                   tabkhoaKcb.TabPages.Add(_newPage);
                   _ucQMS.Parent = _newPage;

                   Application.DoEvents();
                   _newPage.Controls.Add(_ucQMS);
                   _ucQMS.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
                   _ucQMS.Dock = DockStyle.Fill;
                   _ucQMS.Visible = true;
                   Application.DoEvents();
               }
            }
            catch (Exception ex)
            {
                
            }
        }
        public static DataTable Laydanhmuckhoa(string NoitruNgoaitru, int PhongChucnang)
        {
            try
            {
                SqlQuery sqlQuery = new Select().From(VDmucKhoaphong.Schema).Where(VDmucKhoaphong.Columns.KieuKhoaphong).IsEqualTo("KHOA");
                if (PhongChucnang > -1)
                    sqlQuery.And(VDmucKhoaphong.Columns.PhongChucnang).IsEqualTo(PhongChucnang);
                if (NoitruNgoaitru != "ALL")
                    sqlQuery.And(VDmucKhoaphong.Columns.NoitruNgoaitru).IsEqualTo(NoitruNgoaitru);
                return sqlQuery.ExecuteDataSet().Tables[0];
            }
            catch
            {
                return null;
            }
        }
        void frm_QMS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                InitControls();
            }
            //if (e.KeyCode == Keys.F1)
            //    cmdGetSoKham_Click(cmdGetSoKham, new EventArgs());
            //if (e.KeyCode == Keys.F2)
            //    cmdKhamYC_Click(cmdKhamYC, new EventArgs());
            //if (e.KeyCode == Keys.F3)
            //    cmdKhamUuTien_Click(cmdKhamUuTien, new EventArgs());
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
            UIAction.SetText(this.lblMessage, _QMSProperties.TenBenhVien);
            UpdateQMSItems();
        }

      

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(_QMSProperties);
            frm.ShowDialog();
            CauHinh();
            InitControls();

        }

        void UpdateQMSItems()
        {
            foreach (TabPage _page in tabkhoaKcb.TabPages)
            {
                foreach(Control ucs in _page.Controls)
                    if (ucs.GetType().Equals(typeof(ucQMS)))
                    {
                        foreach (Control _item in ucs.Controls)
                            if (_item.GetType().Equals(typeof(ucQMSItem)))
                                ((ucQMSItem)_item).setControlProperties();
                    }
            }
        }

        private void frm_QMS_Load(object sender, EventArgs e)
        {
            InitControls();
           hasLoaded = true;
           
           
        }
       

    }
}
