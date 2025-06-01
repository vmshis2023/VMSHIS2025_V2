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
using QMS.UCs;
using VNS.QMS;

namespace QMS
{
    public partial class QMS_Tiepdon_BVMHN2 : Form
    {
        private QMSProperties _QMSProperties;
        int minusMargin = 50;
        public QMS_Tiepdon_BVMHN2()
        {
            InitializeComponent();
            globalVariables.m_strPropertiesFolder = Application.StartupPath + @"\Properties\";
            if (!new ConnectionSQL().ReadConfig())
            {
                Try2ExitApp();
                return;
            }
            globalVariables.m_strPropertiesFolder = Application.StartupPath;
            _QMSProperties = GetQMSProperties();
            Utility.InitSubSonic(new ConnectionSQL().KhoiTaoKetNoi(), "ORM");
            this.Load += QMS_Tiepdon_BVMHN2_Load;
           
            this.SizeChanged += QMS_Tiepdon_BVMHN2_SizeChanged;
        }

        private void QMS_Tiepdon_BVMHN2_SizeChanged(object sender, EventArgs e)
        {
            this.SuspendLayout();
            foreach (ucQMSTiepdon _qmsTiepdon in flowQMS.Controls)
            {
                _qmsTiepdon.Width = flowQMS.Width - minusMargin;
            }
            this.ResumeLayout();
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
        private void QMS_Tiepdon_BVMHN2_Load(object sender, EventArgs e)
        {
            try
            {
                SetLogo("default", ImageLayout.Zoom);
                DataTable dtQMSType = new Select().From(QmsLoai.Schema)
                    .Where(QmsLoai.Columns.Trangthai).IsEqualTo(1)
                    .OrderAsc(QmsLoai.Columns.SttHthi)
                    .ExecuteDataSet().Tables[0];
                int idx = 0;
                flowQMS.SuspendLayout();
                foreach (DataRow drQMS in dtQMSType.Rows)
                {
                    ucQMSTiepdon _qmsTiepdon = new ucQMSTiepdon(drQMS, _QMSProperties, idx==0);
                    idx++;
                    _qmsTiepdon.Init();
                    flowQMS.Controls.Add(_qmsTiepdon);
                    _qmsTiepdon.Width = flowQMS.Width- minusMargin;
                }
                flowQMS.ResumeLayout();
            }
            catch (Exception ex)
            {

               
            }
        }
        void SetLogo(string imgname, ImageLayout imglayout)
        {
            try
            {
                string sPath = Application.StartupPath;
                string filename = "";
                string s = string.Format(@"{0}\Images\{1}.jpg", sPath, imgname);
                if (File.Exists(s)) filename = s;
                else
                {
                    s = s = string.Format(@"{0}\Images\{1}.png", sPath, imgname);
                    if (File.Exists(s))
                        filename = s;
                    else
                    {
                        s = s = string.Format(@"{0}\Images\{1}.bmp", sPath, imgname);
                        if (File.Exists(s)) filename = s;
                    }
                }
                //if (!string.IsNullOrEmpty(filename)) this.BackgroundImage = Utility.LoadBitmap(filename);

                if (!string.IsNullOrEmpty(filename))
                {
                    Image img;
                    img = Image.FromFile(filename);
                    pnlTop.BackgroundImage = img;
                    pnlTop.BackgroundImageLayout = imglayout;
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                Application.DoEvents();
            }
        }
        private void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(_QMSProperties);
            frm._OnRefreshData += Frm__OnRefreshData;
            frm.ShowDialog();
          
        }

        private void Frm__OnRefreshData(object _property)
        {
            foreach (ucQMSTiepdon _qmsTiepdon in flowQMS.Controls)
            {
                _qmsTiepdon.Refresh();
            }
        }
    }
}
