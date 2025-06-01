using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.Libs;
using VNS.Properties;
using WPF.UCs;

namespace VNS.HIS.UI.Forms.CanLamSang
{
    public partial class frm_ViewImages : Form
    {
        KcbChidinhclsChitiet objChitiet = null;
        string Local1 = "";
        string Local2 = "";
        string Local3 = "";
        string Local4 = "";
        public FTPclient FtpClient;
        public string _FtpClientCurrentDirectory;
        private string _baseDirectory = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory,"Radio\\");
        bool m_blnForced2GetImagesFromFTP = false;
        private readonly string path = Application.StartupPath;
        public frm_ViewImages(KcbChidinhclsChitiet objChitiet)
        {
            InitializeComponent();
            this.objChitiet = objChitiet;
            this.Load += frm_ViewImages_Load;
            this.KeyDown += frm_ViewImages_KeyDown;
            imgBox1.ViewContextMenu(false);
            imgBox2.ViewContextMenu(false);
            imgBox3.ViewContextMenu(false);
            imgBox4.ViewContextMenu(false);

            imgBox1._OnViewImage += imgBox__OnViewImage;
            imgBox2._OnViewImage += imgBox__OnViewImage;
            imgBox3._OnViewImage += imgBox__OnViewImage;
            imgBox4._OnViewImage += imgBox__OnViewImage;
            CauHinh();
        }
        void imgBox__OnViewImage(ImgBox imgBox)
        {
            if (File.Exists(Utility.sDbnull(imgBox.Tag, "")))
                Utility.OpenProcess(Utility.sDbnull(imgBox.Tag, ""));
        }
        
        void frm_ViewImages_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }
            if (e.KeyCode == Keys.F5)
            {
                objChitiet = KcbChidinhclsChitiet.FetchByID(objChitiet.IdChitietchidinh);
                LoadImages();
                return;
            }
        }
        private void CauHinh()
        {
            try
            {
                FtpClient = new FTPclient(PropertyLib._HinhAnhProperties.FTPServer, PropertyLib._HinhAnhProperties.UID, PropertyLib._HinhAnhProperties.PWD);
                _FtpClientCurrentDirectory = FtpClient.CurrentDirectory;
                _baseDirectory = Utility.DoTrim(PropertyLib._HinhAnhProperties.ImageFolder);
                if (_baseDirectory.EndsWith(@"\")) _baseDirectory = _baseDirectory.Substring(0, _baseDirectory.Length - 1);
                if (!Directory.Exists(_baseDirectory))
                {
                    Directory.CreateDirectory(_baseDirectory);
                }
            }
            catch
            {
            }
        }
        void frm_ViewImages_Load(object sender, EventArgs e)
        {
            LoadImages();
        }
        void LoadImages()
        {
            try
            {
                Utility.WaitNow(this);
                ResetImages();
                CheckImages();
                LoadImage(imgBox1, Local1, objChitiet.ImgPath1);
                LoadImage(imgBox2, Local2, objChitiet.ImgPath2);
                LoadImage(imgBox3, Local3, objChitiet.ImgPath3);
                LoadImage(imgBox4, Local4, objChitiet.ImgPath4);
                pic1.Tag = imgBox1.Tag;
                pic2.Tag = imgBox2.Tag;
                pic3.Tag = imgBox3.Tag;
                pic4.Tag = imgBox4.Tag;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                Utility.DefaultNow(this);
            }
        }
       

        void LoadImage(WPF.UCs.ImgBox pImage, string mylocalImage, string imgPath)
        {
            try
            {
                if (File.Exists(mylocalImage) && !m_blnForced2GetImagesFromFTP)
                {
                    pImage.fullName = mylocalImage;
                    pImage.LoadIMg();
                    pImage.Tag = mylocalImage;
                    return;
                }
                if (!string.IsNullOrEmpty(imgPath))
                {
                    if (!PropertyLib._HinhAnhProperties.IamLocal || m_blnForced2GetImagesFromFTP)
                    {
                        FtpClient.CurrentDirectory = string.Format("{0}{1}", _FtpClientCurrentDirectory,
                            objChitiet.IdChitietchidinh.ToString());
                        if (FtpClient.FtpFileExists(FtpClient.CurrentDirectory + imgPath))
                        {
                            string sPath1 = string.Format(@"{0}\{1}\{2}", _baseDirectory,
                            objChitiet.IdChitietchidinh.ToString(), imgPath);
                            Utility.CreateFolder(sPath1);
                            FtpClient.Download(imgPath, sPath1, true);
                            pImage.fullName = sPath1;
                            pImage.LoadIMg();
                            pImage.Tag = sPath1;
                        }
                        else
                        {
                            pImage.fullName = path + @"\Path\noimage.jpg";
                            pImage.LoadIMg();
                            pImage.Tag = "";
                        }
                    }
                    else//Ảnh trên chính máy tính này
                    {
                        pImage.fullName = imgPath;
                        pImage.LoadIMg();
                        pImage.Tag = imgPath;
                    }
                }
            }
            catch
            {
            }
            finally
            {
                if (pImage._img.Source == null)
                {
                    pImage.fullName = path + @"\Path\noimage.jpg";
                    pImage.LoadIMg();
                    pImage.Tag = "";
                }
            }
        }
        void CheckImages()
        {
            try
            {
                if (Utility.Byte2Bool(objChitiet.FTPImage))
                {
                    List<string> lstImgFiles = new List<string>();

                    string PACS_SHAREDFOLDER = VNS.Libs.THU_VIEN_CHUNG.Laygiatrithamsohethong("PACS_SHAREDFOLDER", "", true);
                    string PACS_IMAGEREPLACEPATH = VNS.Libs.THU_VIEN_CHUNG.Laygiatrithamsohethong("PACS_IMAGEREPLACEPATH", "", true);
                    FtpClient.CurrentDirectory = string.Format("{0}{1}", _FtpClientCurrentDirectory,
                               objChitiet.IdChitietchidinh.ToString());

                    string _strfile = Utility.sDbnull(objChitiet.ImgPath1, "");
                    string serverFile = Utility.DoTrim(_strfile).ToUpper().Replace(PACS_IMAGEREPLACEPATH.ToUpper(), PACS_SHAREDFOLDER.ToUpper());
                    string localfile = string.Format(@"{0}\{1}\{2}", _baseDirectory, objChitiet.IdChitietchidinh.ToString(), Utility.sDbnull(objChitiet.ImgPath1, ""));// Utility.DoTrim(_strfile).ToUpper().Replace(PACS_IMAGEREPLACEPATH.ToUpper(), _baseDirectory.ToUpper());

                    if (_strfile != "")
                    {
                        //if (!File.Exists(localfile)) lstImgFiles.Add(Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath1], ""));
                        Local1 = localfile;
                    }
                    _strfile = Utility.sDbnull(objChitiet.ImgPath2, "");
                    serverFile = Utility.DoTrim(_strfile).ToUpper().Replace(PACS_IMAGEREPLACEPATH.ToUpper(), PACS_SHAREDFOLDER.ToUpper());
                    localfile = string.Format(@"{0}\{1}\{2}", _baseDirectory, objChitiet.IdChitietchidinh.ToString(), Utility.sDbnull(objChitiet.ImgPath2, ""));

                    if (_strfile != "")
                    {
                        // if (!File.Exists(localfile)) lstImgFiles.Add(Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath2], ""));
                        Local2 = localfile;
                    }
                    _strfile = Utility.sDbnull(objChitiet.ImgPath3, "");
                    serverFile = Utility.DoTrim(_strfile).ToUpper().Replace(PACS_IMAGEREPLACEPATH.ToUpper(), PACS_SHAREDFOLDER.ToUpper());
                    localfile = string.Format(@"{0}\{1}\{2}", _baseDirectory, objChitiet.IdChitietchidinh.ToString(), Utility.sDbnull(objChitiet.ImgPath3, ""));

                    if (_strfile != "")
                    {
                        //if (!File.Exists(localfile)) lstImgFiles.Add(Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath3], ""));
                        Local3 = localfile;
                    }
                    _strfile = Utility.sDbnull(objChitiet.ImgPath4, "");
                    serverFile = Utility.DoTrim(_strfile).ToUpper().Replace(PACS_IMAGEREPLACEPATH.ToUpper(), PACS_SHAREDFOLDER.ToUpper());
                    localfile = string.Format(@"{0}\{1}\{2}", _baseDirectory, objChitiet.IdChitietchidinh.ToString(), Utility.sDbnull(objChitiet.ImgPath4, ""));

                    if (_strfile != "")
                    {
                        //if (!File.Exists(localfile)) lstImgFiles.Add(Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath4], ""));
                        Local4 = localfile;
                    }
                }
                else
                {
                    Local1 =objChitiet.ImgPath1;
                    Local2 = objChitiet.ImgPath2;
                    Local3 = objChitiet.ImgPath3;
                    Local4 = objChitiet.ImgPath4;
                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void ResetImages()
        {
            imgBox1._img.Source = null;
            imgBox1.Tag = "";

            imgBox2._img.Source = null;
            imgBox2.Tag = "";

            imgBox3._img.Source = null;
            imgBox3.Tag = "";

            imgBox4._img.Source = null;
            imgBox4.Tag = "";
        }
    }
}
