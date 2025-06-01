using System;
using System.Windows.Forms;

using Leadtools;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace VMS.HIS.Cls.DicomGateWay
{
    public static class Support
    {
        public static bool KernelExpired
        {
            get
            {
                if (RasterSupport.KernelExpired)
                {
                    MessageBox.Show(
                       null,
                       "This library has expired.  Contact LEAD Technologies, Inc. at (704) 332-5532 to order a new version.",
                       "LEADTOOLS for .NET Evalutation Notice",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Stop);
                    return true;
                }
                else
                    return false;
            }
        }
        static Hashtable lstKeys = new Hashtable();
        static string ReadlineFromFile(string file)
        {
            try
            {
                using (StreamReader _reader = new StreamReader(file))
                {
                    object _obj = _reader.ReadLine();
                    if (_obj == null) return "";
                    return _obj.ToString();
                }
            }
            catch
            {
                return "";
            }
        }
        private static void getLicense(string clientKey)
        {
            try
            {
                lstKeys.Clear();
                string licfile = "";
                string licfilePath = Application.StartupPath + @"\licPath.txt";
                string defaultLicFileName = Application.StartupPath + @"\ltlrtlic.rtm";
                if (File.Exists(licfilePath))
                {
                    licfile = ReadlineFromFile(licfilePath);
                }
                else
                    licfile = defaultLicFileName;
                if (licfile.Trim() == "") licfile = defaultLicFileName;
                if (File.Exists(licfile))
                {
                    using (StreamReader reader = new StreamReader(licfile))
                    {
                        while (reader.Peek() > 0)
                        {
                            string sval = reader.ReadLine();
                            if (sval != null)
                            {
                                string[] arrValues = sval.Split(',');
                                if (arrValues.Length == 2)
                                {
                                    xvect.Encrypt _ect = new xvect.Encrypt();
                                    _ect.UpdateAlgName(_ect.AlgName);
                                    _ect.sPwd = clientKey;

                                    string sVal1 = _ect.GiaiMa(arrValues[1]);
                                    _ect.sPwd = _ect.Fam_PWD;
                                    string[] _ArrSupportTypeName = arrValues[0].Split('.');
                                    string _SupportType = "";
                                    try
                                    {
                                        if (_ArrSupportTypeName.Length < 2)
                                            _SupportType = _ArrSupportTypeName[0].Trim();
                                        else
                                            _SupportType = _ArrSupportTypeName[1].Trim();
                                    }
                                    catch
                                    {
                                        _SupportType = "dvcnnqdqm";
                                    }

                                    if (!lstKeys.Contains(_SupportType.Trim())) lstKeys.Add(_SupportType.Trim(), _ect.GiaiMa(sVal1));
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                if (lstKeys.Count <= 0)
                    ShowLeadtoolsErrMsg();
            }
        }
        static void ShowLeadtoolsErrMsg()
        {
            MessageBox.Show(
                      null,
                      "Contact LEAD Technologies, Inc. at (704) 332-5532 to order a new version or a licsence file.",
                      "LEADTOOLS for .NET Evalutation Notice",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Stop);
        }
        private static string getLicense(RasterSupportType _SupportType)
        {
            try
            {
                if (lstKeys.Count <= 0) return "";
                if (!lstKeys.Contains(_SupportType.ToString().Trim())) return "";
                return lstKeys[_SupportType.ToString()].ToString().Trim();
            }
            catch
            {
                return "";
            }
        }
        public static void UnlockNO(bool check, string clientKey, string p)
        {
            RasterSupport.Unlock(RasterSupportType.Abc, "R3naWU3mHs");
            RasterSupport.Unlock(RasterSupportType.AbicRead, "dpKdJvh2P7");
            RasterSupport.Unlock(RasterSupportType.AbicSave, "Nb39Cvv6Q2");
            RasterSupport.Unlock(RasterSupportType.Barcodes1D, "UbXxzPTCVP");
            RasterSupport.Unlock(RasterSupportType.Barcodes1DSilver, "jvMTAubUkH");
            RasterSupport.Unlock(RasterSupportType.BarcodesDataMatrixRead, "E4Fy2TzBCc");
            RasterSupport.Unlock(RasterSupportType.BarcodesDataMatrixWrite, "Np2utTGDD3");
            RasterSupport.Unlock(RasterSupportType.BarcodesPdfRead, "n4WidQyJxd");
            RasterSupport.Unlock(RasterSupportType.BarcodesPdfWrite, "WpKd6QFMyB");
            RasterSupport.Unlock(RasterSupportType.BarcodesQRRead, "ypC4PUHpip");
            RasterSupport.Unlock(RasterSupportType.BarcodesQRWrite, "BbXXGuVSjk");
            RasterSupport.Unlock(RasterSupportType.Bitonal, "KbGGrSUz3N");
            RasterSupport.Unlock(RasterSupportType.Ccow, "QvMN82YPTR");
            RasterSupport.Unlock(RasterSupportType.Cmw, "rhiWfrmt5X");
            RasterSupport.Unlock(RasterSupportType.Dicom, "y47S3rZv6U");
            RasterSupport.Unlock(RasterSupportType.Document, "HbQR9NSXQ3");
            RasterSupport.Unlock(RasterSupportType.DocumentWriters, "BhaNezSEBB");
            RasterSupport.Unlock(RasterSupportType.DocumentWritersPdf, "3b39Q3YMdX");
            RasterSupport.Unlock(RasterSupportType.ExtGray, "bpTmxSfx8R");
            RasterSupport.Unlock(RasterSupportType.Forms, "GpC33ZK78k");
            RasterSupport.Unlock(RasterSupportType.IcrPlus, "9vdKEtBhFy");
            RasterSupport.Unlock(RasterSupportType.IcrProfessional, "3p2UAxjTy5");
            RasterSupport.Unlock(RasterSupportType.J2k, "Hvu2PRAr3z");
            RasterSupport.Unlock(RasterSupportType.Jbig2, "43WiSV4YNB");
            RasterSupport.Unlock(RasterSupportType.Jpip, "YbGG7wWiVJ");
            RasterSupport.Unlock(RasterSupportType.Pro, "");
            RasterSupport.Unlock(RasterSupportType.LeadOmr, "J3vh828GC8");
            RasterSupport.Unlock(RasterSupportType.MediaWriter, "TpjDw2kJD2");
            RasterSupport.Unlock(RasterSupportType.Medical, "ZhyFRnk3sY");
            RasterSupport.Unlock(RasterSupportType.Medical3d, "DvuzH3ePeu");
            RasterSupport.Unlock(RasterSupportType.MedicalNet, "b4nBinY7tv");
            RasterSupport.Unlock(RasterSupportType.MedicalServer, "QbXwuZxA3h");
            RasterSupport.Unlock(RasterSupportType.Mobile, "");
            RasterSupport.Unlock(RasterSupportType.Nitf, "G37rmw5dTr");
            RasterSupport.Unlock(RasterSupportType.OcrAdvantage, "vhyejyrZ4T");
            RasterSupport.Unlock(RasterSupportType.OcrAdvantagePdfLeadOutput, "83nacy746p");
            RasterSupport.Unlock(RasterSupportType.OcrArabic, "RpTMEwJfUN");
            RasterSupport.Unlock(RasterSupportType.OcrArabicPdfLeadOutput, "mhiVa3Trfr");
            RasterSupport.Unlock(RasterSupportType.OcrPlus, "rvdKxn8Zr4");
            RasterSupport.Unlock(RasterSupportType.OcrPlusPdfOutput, "4hS7bsn9bF");
            RasterSupport.Unlock(RasterSupportType.OcrPlusPdfLeadOutput, "Bv4CWXckvf");
            RasterSupport.Unlock(RasterSupportType.OcrProfessional, "jhr6pXRnwc");
            RasterSupport.Unlock(RasterSupportType.OcrProfessionalAsian, "WbQQMTuFE4");
            RasterSupport.Unlock(RasterSupportType.OcrProfessionalPdfOutput, "T3eYHx6Rx3");
            RasterSupport.Unlock(RasterSupportType.OcrProfessionalPdfLeadOutput, "NvdjSYDX2v");
            RasterSupport.Unlock(RasterSupportType.PdfAdvanced, "8hivUWQbSU");
            RasterSupport.Unlock(RasterSupportType.PdfRead, "Wvuz2WC3rX");
            RasterSupport.Unlock(RasterSupportType.PdfSave, "tv4CJsa5aJ");
            RasterSupport.Unlock(RasterSupportType.PrintDriver, "YvMsmzECAE");
            RasterSupport.Unlock(RasterSupportType.PrintDriverServer, "v37Ry49tHN");
            RasterSupport.Unlock(RasterSupportType.Vector, "KpC5bPeAUs");

            //RasterSupport.Unlock(RasterSupportType.Abc, "");
            //RasterSupport.Unlock(RasterSupportType.AbicRead, "");
            //RasterSupport.Unlock(RasterSupportType.AbicSave, "");
            //RasterSupport.Unlock(RasterSupportType.Barcodes1D, "");
            //RasterSupport.Unlock(RasterSupportType.Barcodes1DSilver, "");
            //RasterSupport.Unlock(RasterSupportType.BarcodesDataMatrixRead, "");
            //RasterSupport.Unlock(RasterSupportType.BarcodesDataMatrixWrite, "");
            //RasterSupport.Unlock(RasterSupportType.BarcodesPdfRead, "");
            //RasterSupport.Unlock(RasterSupportType.BarcodesPdfWrite, "");
            //RasterSupport.Unlock(RasterSupportType.BarcodesQRRead, "");
            //RasterSupport.Unlock(RasterSupportType.BarcodesQRWrite, "");
            //RasterSupport.Unlock(RasterSupportType.Bitonal, "");
            //RasterSupport.Unlock(RasterSupportType.Ccow, "");
            //RasterSupport.Unlock(RasterSupportType.Cmw, "");
            //RasterSupport.Unlock(RasterSupportType.Dicom, "");
            //RasterSupport.Unlock(RasterSupportType.Document, "");
            //RasterSupport.Unlock(RasterSupportType.DocumentWriters, "");
            //RasterSupport.Unlock(RasterSupportType.DocumentWritersPdf, "");
            //RasterSupport.Unlock(RasterSupportType.ExtGray, "");
            //RasterSupport.Unlock(RasterSupportType.Forms, "");
            //RasterSupport.Unlock(RasterSupportType.IcrPlus, "");
            //RasterSupport.Unlock(RasterSupportType.IcrProfessional, "");
            //RasterSupport.Unlock(RasterSupportType.J2k, "");
            //RasterSupport.Unlock(RasterSupportType.Jbig2, "");
            //RasterSupport.Unlock(RasterSupportType.Jpip, "");
            //RasterSupport.Unlock(RasterSupportType.Pro, "");
            //RasterSupport.Unlock(RasterSupportType.LeadOmr, "");
            //RasterSupport.Unlock(RasterSupportType.MediaWriter, "");
            //RasterSupport.Unlock(RasterSupportType.Medical, "ZhyFRnk3sY");
            //RasterSupport.Unlock(RasterSupportType.Medical3d, "");
            //RasterSupport.Unlock(RasterSupportType.MedicalNet, "b4nBinY7tv");
            //RasterSupport.Unlock(RasterSupportType.MedicalServer, "");
            //RasterSupport.Unlock(RasterSupportType.Mobile, "");
            //RasterSupport.Unlock(RasterSupportType.Nitf, "");
            //RasterSupport.Unlock(RasterSupportType.OcrAdvantage, "");
            //RasterSupport.Unlock(RasterSupportType.OcrAdvantagePdfLeadOutput, "");
            //RasterSupport.Unlock(RasterSupportType.OcrArabic, "");
            //RasterSupport.Unlock(RasterSupportType.OcrArabicPdfLeadOutput, "");
            //RasterSupport.Unlock(RasterSupportType.OcrPlus, "");
            //RasterSupport.Unlock(RasterSupportType.OcrPlusPdfOutput, "");
            //RasterSupport.Unlock(RasterSupportType.OcrPlusPdfLeadOutput, "");
            //RasterSupport.Unlock(RasterSupportType.OcrProfessional, "");
            //RasterSupport.Unlock(RasterSupportType.OcrProfessionalPdfOutput, "");
            //RasterSupport.Unlock(RasterSupportType.OcrProfessionalPdfLeadOutput, "");
            //RasterSupport.Unlock(RasterSupportType.PdfAdvanced, "");
            //RasterSupport.Unlock(RasterSupportType.PdfRead, "");
            //RasterSupport.Unlock(RasterSupportType.PdfSave, "");
            //RasterSupport.Unlock(RasterSupportType.PrintDriver, "");
            //RasterSupport.Unlock(RasterSupportType.PrintDriverServer, "");
            //RasterSupport.Unlock(RasterSupportType.Vector, "");
            if (check)
            {
                Array a = Enum.GetValues(typeof(RasterSupportType));
                foreach (RasterSupportType i in a)
                {
                    if (i != RasterSupportType.Vector && i != RasterSupportType.MedicalNet)
                    {
                        if (RasterSupport.IsLocked(i))
                        {
                        }
                    }
                }
            }

        }
        public static void Unlock(bool check, string clientKey)
        {
            getLicense(clientKey);
            RasterSupport.Unlock(RasterSupportType.Abc, getLicense(RasterSupportType.Abc));
            RasterSupport.Unlock(RasterSupportType.AbicRead, getLicense(RasterSupportType.AbicRead));
            RasterSupport.Unlock(RasterSupportType.AbicSave, getLicense(RasterSupportType.AbicSave));
            RasterSupport.Unlock(RasterSupportType.Barcodes1D, getLicense(RasterSupportType.Barcodes1D));
            RasterSupport.Unlock(RasterSupportType.Barcodes1DSilver, getLicense(RasterSupportType.Barcodes1DSilver));
            RasterSupport.Unlock(RasterSupportType.BarcodesDataMatrixRead, getLicense(RasterSupportType.BarcodesDataMatrixRead));
            RasterSupport.Unlock(RasterSupportType.BarcodesDataMatrixWrite, getLicense(RasterSupportType.BarcodesDataMatrixWrite));
            RasterSupport.Unlock(RasterSupportType.BarcodesPdfRead, getLicense(RasterSupportType.BarcodesPdfRead));
            RasterSupport.Unlock(RasterSupportType.BarcodesPdfWrite, getLicense(RasterSupportType.BarcodesPdfWrite));
            RasterSupport.Unlock(RasterSupportType.BarcodesQRRead, getLicense(RasterSupportType.BarcodesQRRead));
            RasterSupport.Unlock(RasterSupportType.BarcodesQRWrite, getLicense(RasterSupportType.BarcodesQRWrite));
            RasterSupport.Unlock(RasterSupportType.Bitonal, getLicense(RasterSupportType.Bitonal));
            RasterSupport.Unlock(RasterSupportType.Ccow, getLicense(RasterSupportType.Ccow));
            RasterSupport.Unlock(RasterSupportType.Cmw, getLicense(RasterSupportType.Cmw));
            RasterSupport.Unlock(RasterSupportType.Dicom, getLicense(RasterSupportType.Dicom));
            RasterSupport.Unlock(RasterSupportType.Document, getLicense(RasterSupportType.Document));
            RasterSupport.Unlock(RasterSupportType.DocumentWriters, getLicense(RasterSupportType.DocumentWriters));
            RasterSupport.Unlock(RasterSupportType.DocumentWritersPdf, getLicense(RasterSupportType.DocumentWritersPdf));
            RasterSupport.Unlock(RasterSupportType.ExtGray, getLicense(RasterSupportType.ExtGray));
            RasterSupport.Unlock(RasterSupportType.Forms, getLicense(RasterSupportType.Forms));
            RasterSupport.Unlock(RasterSupportType.IcrPlus, getLicense(RasterSupportType.IcrPlus));
            RasterSupport.Unlock(RasterSupportType.IcrProfessional, getLicense(RasterSupportType.IcrProfessional));
            RasterSupport.Unlock(RasterSupportType.J2k, getLicense(RasterSupportType.J2k));
            RasterSupport.Unlock(RasterSupportType.Jbig2, getLicense(RasterSupportType.Jbig2));
            RasterSupport.Unlock(RasterSupportType.Jpip, getLicense(RasterSupportType.Jpip));
            RasterSupport.Unlock(RasterSupportType.Pro, getLicense(RasterSupportType.Pro));
            RasterSupport.Unlock(RasterSupportType.LeadOmr, getLicense(RasterSupportType.LeadOmr));
            RasterSupport.Unlock(RasterSupportType.MediaWriter, getLicense(RasterSupportType.MediaWriter));
            RasterSupport.Unlock(RasterSupportType.Medical, getLicense(RasterSupportType.Medical));
            RasterSupport.Unlock(RasterSupportType.Medical3d, getLicense(RasterSupportType.Medical3d));
            RasterSupport.Unlock(RasterSupportType.MedicalNet, getLicense(RasterSupportType.MedicalNet));
            RasterSupport.Unlock(RasterSupportType.MedicalServer, getLicense(RasterSupportType.MedicalServer));
            RasterSupport.Unlock(RasterSupportType.Mobile, getLicense(RasterSupportType.Mobile));
            RasterSupport.Unlock(RasterSupportType.Nitf, getLicense(RasterSupportType.Nitf));
            RasterSupport.Unlock(RasterSupportType.OcrAdvantage, getLicense(RasterSupportType.OcrAdvantage));
            RasterSupport.Unlock(RasterSupportType.OcrAdvantagePdfLeadOutput, getLicense(RasterSupportType.OcrAdvantagePdfLeadOutput));
            RasterSupport.Unlock(RasterSupportType.OcrArabic, getLicense(RasterSupportType.OcrArabic));
            RasterSupport.Unlock(RasterSupportType.OcrArabicPdfLeadOutput, getLicense(RasterSupportType.OcrArabicPdfLeadOutput));
            RasterSupport.Unlock(RasterSupportType.OcrPlus, getLicense(RasterSupportType.OcrPlus));
            RasterSupport.Unlock(RasterSupportType.OcrPlusPdfOutput, getLicense(RasterSupportType.OcrPlusPdfOutput));
            RasterSupport.Unlock(RasterSupportType.OcrPlusPdfLeadOutput, getLicense(RasterSupportType.OcrPlusPdfLeadOutput));
            RasterSupport.Unlock(RasterSupportType.OcrProfessional, getLicense(RasterSupportType.OcrProfessional));
            RasterSupport.Unlock(RasterSupportType.OcrProfessionalAsian, getLicense(RasterSupportType.OcrProfessionalAsian));
            RasterSupport.Unlock(RasterSupportType.OcrProfessionalPdfOutput, getLicense(RasterSupportType.OcrProfessionalPdfOutput));
            RasterSupport.Unlock(RasterSupportType.OcrProfessionalPdfLeadOutput, getLicense(RasterSupportType.OcrProfessionalPdfLeadOutput));
            RasterSupport.Unlock(RasterSupportType.PdfAdvanced, getLicense(RasterSupportType.PdfAdvanced));
            RasterSupport.Unlock(RasterSupportType.PdfRead, getLicense(RasterSupportType.PdfRead));
            RasterSupport.Unlock(RasterSupportType.PdfSave, getLicense(RasterSupportType.PdfSave));
            RasterSupport.Unlock(RasterSupportType.PrintDriver, getLicense(RasterSupportType.PrintDriver));
            RasterSupport.Unlock(RasterSupportType.PrintDriverServer, getLicense(RasterSupportType.PrintDriverServer));
            RasterSupport.Unlock(RasterSupportType.Vector, getLicense(RasterSupportType.Vector));



            if (check)
            {
                Array a = Enum.GetValues(typeof(RasterSupportType));
                foreach (RasterSupportType i in a)
                {
                    if (i != RasterSupportType.Vector && i != RasterSupportType.MedicalNet)
                    {
                        if (RasterSupport.IsLocked(i))
                        {
                        }
                    }
                }
            }

        }

    }
}
