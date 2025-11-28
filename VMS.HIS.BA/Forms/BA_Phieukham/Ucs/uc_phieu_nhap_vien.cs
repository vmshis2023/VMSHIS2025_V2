using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VMS.HIS.EMR.Forms.BA_Phieukham.Ucs
{
    public partial class uc_phieu_nhap_vien : UserControl
    {
        public uc_phieu_nhap_vien()
        {
            InitializeComponent();
        }
        public void ShowData(NoitruPhieunhapvien objPNV)
        {
            try
            {
              
                if(objPNV!=null)
                {
                    dtp_NgayNhapVien.Value = objPNV.NgayNhapvien.Value;
                    txt_chandoan_nhapvien.Text = objPNV.ChandoanVaovien;
                    txt_Quanlybenhly.Text = objPNV.QuatrinhBenhly;
                    txt_TienSuBanThan.Text = objPNV.TsuBanthan;
                    txt_TienSuGiaDinh.Text = objPNV.TsuGiadinh;
                }  
                else
                {
                    dtp_NgayNhapVien.ResetText();
                    txt_chandoan_nhapvien.Clear();
                    txt_Quanlybenhly.Clear();
                    txt_TienSuBanThan.Clear();
                    txt_TienSuGiaDinh.Clear();
                }
                
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
       
    }
}
