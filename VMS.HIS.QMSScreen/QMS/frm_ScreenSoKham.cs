using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VNS.Properties;
namespace VMS.QMS
{
    public partial class frm_ScreenSoKham : Form
    {
        
        public string TenBenhVien { get; set; }
        public string TenQuay { get; set; }
        long idQMS = -1;
        public frm_ScreenSoKham()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
           
            cmdConfig.Click+=new EventHandler(cmdConfig_Click);
        }
        public void SetQmsValue(long idQMS, string sokham,int isUuTien)
        {
            try
            {
                this.idQMS = idQMS;
                lblSoKham.Font = PropertyLib._QMSTiepdonProperties.FontChuSotiepdon;
                lblSoKham.Image = null;
                lblSoKham.Text = sokham;
                lblTenloaiQMS.Text = Utility.DoTrim(PropertyLib._QMSTiepdonProperties.TenLoaiQMS) == ""
                                       ? (isUuTien == 1 ? PropertyLib._QMSTiepdonProperties.TenSoUutien : PropertyLib._QMSTiepdonProperties.TenSothuong)
                                       : Utility.DoTrim(PropertyLib._QMSTiepdonProperties.TenLoaiQMS);
                if (isUuTien == 1)
                {
                    lblTenloaiQMS.ForeColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSTiepdonProperties._ForeColorTenloaiQMS_Uutien);
                    lblTenloaiQMS.BackColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSTiepdonProperties._backColorTenloaiQMS_Uutien);
                }
                else
                {
                    lblTenloaiQMS.ForeColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSTiepdonProperties._ForeColorTenloaiQMS);
                    lblTenloaiQMS.BackColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSTiepdonProperties._backColorTenloaiQMS);
                }
                if (idQMS <= 0)
                {
                    if (Utility.sDbnull(PropertyLib._QMSTiepdonProperties.duongdan_filehinhanh_khihetso, "").Length > 0)
                    {
                        if (File.Exists(PropertyLib._QMSTiepdonProperties.duongdan_filehinhanh_khihetso))
                        {
                            lblSoKham.Text = "";
                            lblSoKham.Image = Image.FromFile(PropertyLib._QMSTiepdonProperties.duongdan_filehinhanh_khihetso);
                            return;
                        }
                    }

                    if (Utility.sDbnull(PropertyLib._QMSTiepdonProperties.Noidung_hetso, "").Length > 0)
                    {
                        lblSoKham.Font = PropertyLib._QMSTiepdonProperties.FontChuHetsoQMS;
                        lblSoKham.Text = PropertyLib._QMSTiepdonProperties.Noidung_hetso;
                    }

                }
            }
            catch (Exception ex)
            {
                
            }
           
        }
        /// <summary>
        /// hàm thực hiện việc cấu hình
        /// </summary>
        private void CauHinh()
        {
            try
            {
                this.Text = string.Format("{0}-{1}", Utility.sDbnull(PropertyLib._QMSTiepdonProperties.TenBenhvien), Utility.sDbnull(PropertyLib._QMSTiepdonProperties.TenQuay, ""));
                Utility.SetMsg(lblQuaySo, Utility.sDbnull(PropertyLib._QMSTiepdonProperties.TenQuay, ""), true);
                lblTenloaiQMS.ForeColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSTiepdonProperties._ForeColorTenloaiQMS);
                lblTenloaiQMS.BackColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSTiepdonProperties._backColorTenloaiQMS);
                lblTenloaiQMS.Font = PropertyLib._QMSTiepdonProperties.FontChuTenloaiQMS;
                lblTenloaiQMS.Height = PropertyLib._QMSTiepdonProperties.ChieucaoTenloaiQMS;

                lblQuaySo.ForeColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSTiepdonProperties._ForeColorQuayso);
                lblQuaySo.BackColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSTiepdonProperties._backColorQuayso);
                lblQuaySo.Font = PropertyLib._QMSTiepdonProperties.FontChuQuayso;
                lblQuaySo.Height = PropertyLib._QMSTiepdonProperties.ChieucaoQuayso;

                lblSoKham.ForeColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSTiepdonProperties._ForeColorSotiepdon);
                lblSoKham.BackColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSTiepdonProperties._backColorSotiepdon);
                lblSoKham.Font = PropertyLib._QMSTiepdonProperties.FontChuSotiepdon;

              
                pnlLogo.Height = PropertyLib._QMSTiepdonProperties.Chieucaothongtinbenhvien;
                lblSoKham.Image = null;
                if (idQMS <= 0)
                {
                    if (Utility.sDbnull(PropertyLib._QMSTiepdonProperties.duongdan_filehinhanh_khihetso, "").Length > 0)
                    {
                        if (File.Exists(PropertyLib._QMSTiepdonProperties.duongdan_filehinhanh_khihetso))
                        {
                            lblSoKham.Text = "";
                            lblSoKham.Image = Image.FromFile(PropertyLib._QMSTiepdonProperties.duongdan_filehinhanh_khihetso);
                            return;
                        }
                    }

                    if (Utility.sDbnull(PropertyLib._QMSTiepdonProperties.Noidung_hetso, "").Length > 0)
                    {
                        lblSoKham.Font = PropertyLib._QMSTiepdonProperties.FontChuHetsoQMS;
                        lblSoKham.Text = PropertyLib._QMSTiepdonProperties.Noidung_hetso;
                    }

                }

            }
            catch (Exception)
            {
            }
        }
        private void frm_ScreenSoKham_Load(object sender, EventArgs e)
        {
            try
            {
                if (PropertyLib._QMSTiepdonProperties == null) PropertyLib._QMSTiepdonProperties = PropertyLib.GetQMSTiepdonProperties();
                CauHinh();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
          
        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties( PropertyLib._QMSTiepdonProperties);
            frm._OnRefreshData += frm__OnRefreshData;
            frm.TopMost = true;
            frm.ShowDialog();
        }

        void frm__OnRefreshData(object _property)
        {
            CauHinh();
        }

        private void PassData(string sokham)
        {
            Utility.SetMsg(lblSoKham, sokham,true);
        }

        private void lblSoKham_Click(object sender, EventArgs e)
        {

        }

        private void cmdConfig_Click_1(object sender, EventArgs e)
        {

        }
    }
}
