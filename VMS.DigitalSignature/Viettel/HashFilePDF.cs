using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViettelFileSigner;

namespace DEMO_CLOUD_CA_DOTNET
{
    public class HashFilePDF
    {
        public static string HASH_ALGORITHM_SHA_1 = "SHA1";

        public static string HASH_ALGORITHM_SHA_256 = "SHA256";
        public static String GetHashTypeRectangleText(SignPdfFile pdfSig, String src, X509Certificate[] certChain, string hashAlg)
        {
            DisplayConfig displayConfig = DisplayConfig.generateDisplayConfigRectangleText(0, 10, 10, 200, 80,
                    null, DisplayConfig.SIGN_TEXT_FORMAT_4, CertUtils.GetCN(certChain[0]), "Kiểm tra", "Hà Nội", DisplayConfig.DATE_FORMAT_1);
            displayConfig.IsDisplaySignature = true;
            String base64Hash = pdfSig.createHash(src, certChain, displayConfig, hashAlg);
            //String base64Hash = pdfSig.createHash(src, certChain, null);
            return base64Hash;
        }

        public static String GetHashTypeRectangleText2_ExistedSignatureField(SignPdfFile pdfSig, String src, X509Certificate[] certChain, String displayText, String fieldName, string hashAlg)
        {
            //DisplayConfig displayConfig = DisplayConfig.generateDisplayConfigRectangleText(1, 10, 10, 200, 80,
            //        DisplayConfig.SIGN_TEXT_FORMAT_4, "Dương Ngọc Khánh", "Kiểm tra", "Hà Nội", DisplayConfig.DATE_FORMAT_1);
            DisplayConfig displayConfig = DisplayConfig.generateDisplayConfigRectangleText_ExistedSignatureField(1, 10, 10, 200, 80,
                    displayText, null, CertUtils.GetCN(certChain[0]), "Kiểm tra", "Hà Nội", DisplayConfig.DATE_FORMAT_1);
            String base64Hash = pdfSig.createHashExistedSignatureField(src, certChain, displayConfig, fieldName, hashAlg);
            return base64Hash;
        }
        public static String GetHashTypeRectangleTextWithCheck(SignPdfFile pdfSig, String src, X509Certificate[] certChain, string hashAlg)
        {
            DisplayConfig displayConfig = DisplayConfig.generateDisplayConfigRectangleTextWithCheck(0, 10, 10, 200, 80,
                    null, DisplayConfig.SIGN_TEXT_FORMAT_1, CertUtils.GetCN(certChain[0]), "Kiểm tra", "Hà Nội", DisplayConfig.DATE_FORMAT_1);
            displayConfig.IsDisplaySignature = false;
            String base64Hash = pdfSig.createHash(src, certChain, displayConfig, hashAlg);
            //String base64Hash = pdfSig.createHash(src, certChain, null);
            return base64Hash;
        }
        public static String GetHashTypeImgText(SignPdfFile pdfSig, String src, X509Certificate[] certChain, string hashAlg, string imgUri)
        {
            DisplayConfig displayConfig = DisplayConfig.generateDisplayConfigImageText(0, 10, 10, 200, 80,
                null, DisplayConfig.SIGN_TEXT_FORMAT_4, CertUtils.GetCN(certChain[0]), "Kiểm tra", "Hà Nội", "{0:dd/MM/yyyy HH:mm:ss}", imgUri);
            displayConfig.SizeFont = 11;
            String base64Hash = pdfSig.createHash(src, certChain, displayConfig, hashAlg);
            //String base64Hash = pdfSig.createHash(src, certChain, null);
            return base64Hash;
        }
        public static String GetHashTypeImgText(float X,float Y,float W,float H, SignPdfFile pdfSig, String src, X509Certificate[] certChain, string hashAlg, string imgUri,int SizeFont, int SizeFontImage)
        {
            DisplayConfig displayConfig = null;
            if (string.IsNullOrEmpty(imgUri))
            {
                displayConfig = DisplayConfig.generateDisplayConfigRectangleText(0, X, Y, W, H,
               null, DisplayConfig.SIGN_TEXT_FORMAT_4, CertUtils.GetCN(certChain[0]), "Kí chỉ định", "Hà Nội", "{0:dd/MM/yyyy HH:mm:ss}");
            }
            else
            {
                displayConfig = DisplayConfig.generateDisplayConfigImageText(0, X, Y, W, H,
                null, DisplayConfig.SIGN_TEXT_FORMAT_4, CertUtils.GetCN(certChain[0]), "Kí chỉ định", "Hà Nội", "{0:dd/MM/yyyy HH:mm:ss}", imgUri);
            }
           if(imgUri=="") displayConfig.SizeFont = SizeFont;
           else displayConfig.SizeFont = SizeFontImage;
            String base64Hash = pdfSig.createHash(src, certChain, displayConfig, hashAlg);
            //String base64Hash = pdfSig.createHash(src, certChain, null);
            return base64Hash;
        }
    }
}
