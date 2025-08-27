using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViettelFileSigner;


namespace VietBaIT.ChuKySo.Api.DigitalSignature.Viettel
{
    public class HashFilePDF
    {
        public static string HASH_ALGORITHM_SHA_1 = "SHA1";

        public static string HASH_ALGORITHM_SHA_256 = "SHA256";

        public static String GetHashTypeRectangleText(SignPdfFile pdfSig, String src, X509Certificate[] certChain, string hashAlg, 
            PdfSignerSynchronous signer, int pageNumber, string pathImage)
        {
            //DisplayConfig displayConfig = DisplayConfig.generateDisplayConfigRectangleText(0, 10, 10, 200, 80,
            //        null, DisplayConfig.SIGN_TEXT_FORMAT_4, CertUtils.GetCN(certChain[0]), "Kiểm tra", "Hà Nội", DisplayConfig.DATE_FORMAT_1);
            DisplayConfig displayConfig = DisplayConfig.generateDisplayConfigImageDefault(pageNumber, signer.OriginX, signer.OriginY, signer.CoordinateX, signer.CoordinateY, 
                pathImage);
            String base64Hash = pdfSig.createHash(src, certChain, displayConfig, hashAlg);
            //String base64Hash = pdfSig.createHash(src, certChain, null);
            return base64Hash;
        }
        public static String GetHashTypeRectangleText(SignPdfFile pdfSig, String src, X509Certificate[] certChain, string hashAlg)
        {
            DisplayConfig displayConfig = DisplayConfig.generateDisplayConfigRectangleText(0, 10, 10, 200, 80,
                    null, PdfSignerSynchronous.FORMAT_TEXT_4, CertUtils.GetCN(certChain[0]), "Kiểm tra", "Hà Nội", DisplayConfig.DATE_FORMAT_1);
            String base64Hash = pdfSig.createHash(src, certChain, displayConfig, hashAlg);
            return base64Hash;
        }

        public static String GetHashTypeRectangleText(SignPdfFile pdfSig, String src, X509Certificate[] certChain, string hashAlg, 
            float coorX, float coorY, float width, float height)
        {
            DisplayConfig displayConfig = DisplayConfig.generateDisplayConfigRectangleText(0, coorX, coorY, width, height,
                    null, DisplayConfig.SIGN_TEXT_FORMAT_4, CertUtils.GetCN(certChain[0]), "Kiểm tra", "Hà Nội", DisplayConfig.DATE_FORMAT_1);
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
    }
}
