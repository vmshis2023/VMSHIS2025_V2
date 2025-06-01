using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.HIS.UI.Forms.HinhAnh;
using VNS.Libs;

namespace VMS.HIS.Danhmuc
{
    public class Util
    {
        public static void ReleaseControlMemory(Control pnl, string RML)
        {
            try
            {
                int count = pnl.Controls.Count - 1;
                int i = 0;
                while (i <= count)
                {
                    foreach (Control uc in pnl.Controls)
                    {
                        uc.Tag = null;
                        pnl.Controls.Remove(uc);
                        uc.Dispose();

                    }
                    i++;
                }

            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
            finally
            {
                GC.Collect();
            }


        }
        public static void ReleaseMemory(FlowLayoutPanel pnl, string RML)
        {
            try
            {
                RML = "3";
                if (RML == "3")
                {
                    int count = pnl.Controls.Count - 1;
                    int i = 0;
                    while (i <= count)
                    {
                        foreach (UC_Image uc in pnl.Controls)
                        {
                            uc.Realse();
                            pnl.Controls.Remove(uc);
                            uc.Dispose();

                        }
                        i++;
                    }
                }
                else if (RML == "2")
                    foreach (UC_Image uc in pnl.Controls)
                    {
                        uc.Realse();
                        pnl.Controls.Remove(uc);
                    }
                else if (RML == "1")
                {
                    foreach (UC_Image uc in pnl.Controls)
                    {
                        uc.Realse1();
                        pnl.Controls.Remove(uc);
                    }
                }
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
            finally
            {
                GC.Collect();
            }


        }
        public static void DeleteAllImages(FlowLayoutPanel pnl)
        {
            try
            {
                int count = pnl.Controls.Count - 1;
                int i = 0;
                while (i <= count)
                {
                    foreach (UC_Image uc in pnl.Controls)
                    {
                        uc.Realse();
                        uc.DeleteMe(false);
                    }
                    i++;
                }
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        public static string getDir()
        {
            return VNS.Properties.PropertyLib._HinhAnhProperties.WatchFolder;
        }
        public static string getMaKieuDichVu()
        {
            return VNS.Properties.PropertyLib._HinhAnhProperties.MaDvu;
        }
        public static string getAutoRefresh()
        {
            return VNS.Properties.PropertyLib._HinhAnhProperties.Autorefresh ? "1" : "0";
        }
        public static string getTime2Refresh()
        {
            return VNS.Properties.PropertyLib._HinhAnhProperties.Time2Refresh.ToString();
        }
        public static List<string> GetFrameInfor()
        {
            return VNS.Properties.PropertyLib._HinhAnhProperties.FrameSizeList.Split(',').ToList<string>();
        }
        public static List<string> GetKhoaPhong()
        {
            if (Utility.DoTrim(VNS.Properties.PropertyLib._HinhAnhProperties.Khoaphong).Length <= 0) return new List<string>();
            return VNS.Properties.PropertyLib._HinhAnhProperties.Khoaphong.Split(',').ToList<string>();
        }
        public static string getExtraWidth()
        {
            return VNS.Properties.PropertyLib._HinhAnhProperties.ExtraWidth.ToString();
        }
        public static string getVideoType()
        {
            return VNS.Properties.PropertyLib._HinhAnhProperties.VideoType;
        }
        public static string getImageFormat()
        {
            return VNS.Properties.PropertyLib._HinhAnhProperties.ImageFormat;
        }

        public static string getMaCoSo()
        {
            return VNS.Properties.PropertyLib._HinhAnhProperties.MaCoso;
        }
        public static string getNhomBS()
        {
            return VNS.Properties.PropertyLib._HinhAnhProperties.NhomBS;
        }
        public static string getMaBenhvien()
        {
            return VNS.Properties.PropertyLib._HinhAnhProperties.MaBV;
        }


    }
}
