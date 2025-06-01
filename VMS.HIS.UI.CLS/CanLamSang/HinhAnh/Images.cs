using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VNS.Libs;
using System.Linq;
namespace VNS.HIS.UI.Forms.HinhAnh
{
	/// <summary>
	/// Extension methods for Image
	/// </summary>
	public static class ImageExtension
	{
		/// <summary>
		/// Crops an image according to a selection rectangel
		/// </summary>
		/// <param name="image">
		/// the image to be cropped
		/// </param>
		/// <param name="selection">
		/// the selection
		/// </param>
		/// <returns>
		/// cropped image
		/// </returns>
        public static Image Crop(this Image image, Rectangle r)
		{
            Bitmap nb = new Bitmap(r.Width, r.Height);
            Graphics g = Graphics.FromImage(nb);
            g.DrawImage(image, -r.X, -r.Y);
            return nb;

            //Bitmap bmp = image as Bitmap;

            //// Check if it is a bitmap:
            //if (bmp == null)
            //    throw new ArgumentException("Kein gültiges Bild (Bitmap)");

            //// Crop the image:
            //Bitmap cropBmp = bmp.Clone(selection, bmp.PixelFormat);

            //// Release the resources:
            //image.Dispose();

			//return cropBmp;
		}
		//---------------------------------------------------------------------
		/// <summary>
		/// Fits an image to the size of a picturebox
		/// </summary>
		/// <param name="image">
		/// image to be fit
		/// </param>
		/// <param name="picBox">
		/// picturebox in that the image should fit
		/// </param>
		/// <returns>
		/// fitted image
		/// </returns>
		/// <remarks>
		/// Although the picturebox has the SizeMode-property that offers
		/// the same functionality an OutOfMemory-Exception is thrown
		/// when assigning images to a picturebox several times.
		/// 
		/// AFAIK the SizeMode is designed for assigning an image to
		/// picturebox only once.
		/// </remarks>
		public static Image Fit2PictureBox(this Image image, PictureBox picBox)
		{
			Bitmap bmp = null;
			Graphics g;

			// Scale:
			double scaleY = (double)image.Width / picBox.Width;
			double scaleX = (double)image.Height / picBox.Height;
			double scale = scaleY < scaleX ? scaleX : scaleY;

			// Create new bitmap:
			bmp = new Bitmap(
				(int)((double)image.Width / scale),
				(int)((double)image.Height / scale));

			// Set resolution of the new image:
			bmp.SetResolution(
				image.HorizontalResolution,
				image.VerticalResolution);

			// Create graphics:
			g = Graphics.FromImage(bmp);

			// Set interpolation mode:
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;

			// Draw the new image:
			g.DrawImage(
				image,
				new Rectangle(			// Ziel
					0, 0,
					bmp.Width, bmp.Height),
				new Rectangle(			// Quelle
					0, 0,
					image.Width, image.Height),
				GraphicsUnit.Pixel);

			// Release the resources of the graphics:
			g.Dispose();
			
			// Release the resources of the origin image:
			image.Dispose();

			return bmp;
		}
	}
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