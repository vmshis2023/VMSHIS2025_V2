using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using System.IO;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;
using System.Drawing;

namespace VietBaIT.ChuKySo.Api.DigitalSignature.Viettel
{
    public class PdfSignerSynchronous
    {
        public PdfSignerSynchronous()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private string inputPDF = "";
        private string outputPDF = "";
        private X509Certificate[] certChain;

        private bool isMultiSignatures = true;
        public const string FORMAT_TEXT_1 = "Ky Boi: {0} \r\nNgay Ky: {1:dd/MM/yyyy HH:mm:ss}";
        public const string FORMAT_TEXT_2 = "Ky Boi: {0} \r\nNgay Ky: {1:dd/MM/yyyy HH:mm:ss} \r\nLy Do: {2}";
        public const string FORMAT_TEXT_3 = "Ky Boi: {0} \r\nNgay Ky: {1:dd/MM/yyyy HH:mm:ss} \r\nLy Do: {2} \r\nDia Diem: {3}";
        public const string FORMAT_TEXT_4 = "Ký bởi: {0} \r\nNgày ký: {1:dd/MM/yyyy HH:mm:ss}";
        public const string FORMAT_TEXT_5 = "Digital signed by: {0} \r\nDate: {1:dd/MM/yyyy HH:mm:ss} \r\nReason: {2}";
        public const string FORMAT_TEXT_6 = "Digital signed by: {0} \r\nDate: {1:dd/MM/yyyy HH:mm:ss} \r\nReason: {2} \r\nLocation: {3}";
        private string sigTextFormat = "Ky Boi: {0} \r\nNgay Ky: {1:dd/MM/yyyy HH:mm:ss}";
        private string sigReason = "";
        private string sigContact = "";
        private string sigLocation = "";
        private int sigPage = 1;
        private bool visible = false;
        private float originX = 10;
        private float originY = 10;
        private float coordinateX = 400;
        private float coordinateY = 50;

        private ITSAClient tsaClient = null;
        private bool useTSA = false;
        CryptoStandard sigtype = CryptoStandard.CMS;

        private PdfReader reader;
        private PdfStamper stamper;
        private ICollection<byte[]> crlBytes;
        private PdfSignatureAppearance sap;
        private PdfPKCS7 sgn;
        private byte[] hash;
        private DateTime cal;
        private byte[] ocsp;
        private int estimatedSize;

        public bool IsMultiSignatures
        {
            get { return isMultiSignatures; }
            set { isMultiSignatures = value; }
        }

        public string SigTextFormat
        {
            get { return sigTextFormat; }
            set { sigTextFormat = value; }
        }

        public string SigReason
        {
            get { return sigReason; }
            set { sigReason = value; }
        }

        public string SigContact
        {
            get { return sigContact; }
            set { sigContact = value; }
        }

        public string SigLocation
        {
            get { return sigLocation; }
            set { sigLocation = value; }
        }

        public int SigPage
        {
            get { return sigPage; }
            set { sigPage = value; }
        }

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public float OriginX
        {
            get { return originX; }
            set { originX = value; }
        }

        public float OriginY
        {
            get { return originY; }
            set { originY = value; }
        }

        public float CoordinateX
        {
            get { return coordinateX; }
            set { coordinateX = value; }
        }

        public float CoordinateY
        {
            get { return coordinateY; }
            set { coordinateY = value; }
        }

        public ITSAClient TsaClient
        {
            get { return tsaClient; }
            set { tsaClient = value; }
        }

        public bool UseTSA
        {
            get { return useTSA; }
            set { useTSA = value; }
        }

        public byte[] CreateHash(string input, string output, X509Certificate[] chain, DateTime signDate)
        {
            try
            {
                this.certChain = chain;
                this.inputPDF = input;
                this.outputPDF = output;
                string signName = UtilSigner.getSignName();

                reader = new PdfReader(this.inputPDF);
                //Activate MultiSignatures
                stamper = PdfStamper.CreateSignature(reader, new FileStream(this.outputPDF, FileMode.Create, FileAccess.Write), '\0', null, isMultiSignatures);

                PdfSignatureAppearance appearance = stamper.SignatureAppearance;

                //appearance.SetCrypto(this.myCert.Akp, this.myCert.Chain, null, PdfSignatureAppearance.WINCER_SIGNED);
                appearance.Reason = sigReason;
                appearance.Contact = sigContact;
                appearance.Location = sigLocation;
                appearance.SignDate = signDate;

                int numberPage = reader.NumberOfPages;
                if (sigPage > numberPage)
                {
                    //Console.WriteLine("Error sigPage");
                    return null;
                }

                // is show signature?
                if (visible)
                {
                    appearance.SetVisibleSignature(new iTextSharp.text.Rectangle(originX, originY, coordinateX, coordinateY), sigPage, signName);
                    var signedOn = appearance.SignDate;
                    //appearance.Layer2Text = String.Format(sigTextFormat, sigContact, signedOn);
                    //string fontpath = @"C:/windows/fonts/times.ttf";
                    //BaseFont bf;
                    //bf = BaseFont.CreateFont(fontpath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    //appearance.Layer2Font = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
                    ////appearance.Layer2Font.Size = 12;

                    //iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(@"D:\backup\Project\Viettel CA\Ho Tro\Dot Net\Demo Mobile CA ASP DotNet\Images\chuky.png");
                    //appearance.SignatureGraphic = image;
                    //////appearance.ImageScale = (72f / (float)300 * 100f);
                    //appearance.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.GRAPHIC_AND_DESCRIPTION;


                    PdfTemplate n2 = appearance.GetLayer(2);
                    float x2 = n2.BoundingBox.Left;
                    float y2 = n2.BoundingBox.Bottom;
                    float w2 = n2.BoundingBox.Width;
                    float h2 = n2.BoundingBox.Height;

                    ColumnText ct = new ColumnText(n2);
                    ct.SetSimpleColumn(x2, y2, w2, h2);

                    string SIGN_TEXT_FORMAT_2 = "Người Ký: {0} \r\nNgày Ký: {1}";
                    string fontpath = @"C:/windows/fonts/times.ttf";
                    BaseFont bf;
                    bf = BaseFont.CreateFont(fontpath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    String displayText = String.Format(SIGN_TEXT_FORMAT_2, sigContact, signedOn);
                    iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph(displayText, new iTextSharp.text.Font(bf, 10));//(displayText, new Font(bf, 10));
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    ct.AddElement(paragraph);
                    ct.Go();
                }
                else
                {
                    appearance.SetVisibleSignature(new iTextSharp.text.Rectangle(0, 0, 0, 0), 1, signName);
                }

                //SignDetached(appearance, pks, this.myCert.Chain, null, null, null, 0, CryptoStandard.CMS);

                byte[] sh = GetHashFile(appearance, certChain, null, null, null, 0, CryptoStandard.CMS);
                return sh;
            }
            catch (Exception e)
            {
                //Console.WriteLine("Error create hash: " + e.Message);
                return null;
            }
        }



        public byte[] GetHashFile(PdfSignatureAppearance sapInput, ICollection<X509Certificate> chain, ICollection<ICrlClient> crlList, IOcspClient ocspClient,
                ITSAClient tsaClient, int estimatedSizeInput, CryptoStandard sigtype)
        {
            try
            {
                this.sap = sapInput;
                this.estimatedSize = estimatedSizeInput;
                List<X509Certificate> certa = new List<X509Certificate>(chain);

                this.crlBytes = null;
                int i = 0;
                while (crlBytes == null && i < certa.Count)
                    crlBytes = MakeSignature.ProcessCrl(certa[i++], crlList);
                if (estimatedSize == 0)
                {
                    estimatedSize = 8192;
                    if (crlBytes != null)
                    {
                        foreach (byte[] element in crlBytes)
                        {
                            estimatedSize += element.Length + 10;
                        }
                    }
                    if (ocspClient != null)
                        estimatedSize += 4192;
                    if (tsaClient != null)
                        estimatedSize += 4192;
                }
                sap.Certificate = certa[0];
                if (sigtype == CryptoStandard.CADES)
                    sap.AddDeveloperExtension(PdfDeveloperExtension.ESIC_1_7_EXTENSIONLEVEL2);
                PdfSignature dic = new PdfSignature(PdfName.ADOBE_PPKLITE, sigtype == CryptoStandard.CADES ? PdfName.ETSI_CADES_DETACHED : PdfName.ADBE_PKCS7_DETACHED);
                dic.Reason = sap.Reason;
                dic.Location = sap.Location;
                dic.SignatureCreator = sap.SignatureCreator;
                dic.Contact = sap.Contact;
                dic.Date = new PdfDate(sap.SignDate); // time-stamp will over-rule this
                sap.CryptoDictionary = dic;

                Dictionary<PdfName, int> exc = new Dictionary<PdfName, int>();
                exc[PdfName.CONTENTS] = estimatedSize * 2 + 2;
                sap.PreClose(exc);

                //String hashAlgorithm = externalSignature.GetHashAlgorithm();
                String hashAlgorithm = "SHA1";

                sgn = new PdfPKCS7(null, chain, hashAlgorithm, false);
                IDigest messageDigest = DigestUtilities.GetDigest(hashAlgorithm);
                // Get data from file PDF
                Stream data = sap.GetRangeStream();
                // Create hash
                hash = DigestAlgorithms.Digest(data, hashAlgorithm);
                cal = DateTime.Now;
                ocsp = null;
                if (chain.Count >= 2 && ocspClient != null)
                {
                    ocsp = ocspClient.GetEncoded(certa[0], certa[1], null);
                }
                byte[] sh = sgn.getAuthenticatedAttributeBytes(hash, cal, ocsp, crlBytes, sigtype);
                return sh;
            }
            catch (Exception e)
            {
                //Console.WriteLine("Error create hash: " + e.Message);
                return null;
            }
        }

        public bool InsertSignature(byte[] extSignature)
        {
            try
            {
                sgn.SetExternalDigest(extSignature, null, "RSA");
                byte[] encodedSig;

                if (useTSA) // use Timestamp
                {
                    encodedSig = sgn.GetEncodedPKCS7(hash, DateTime.Now, tsaClient, ocsp, crlBytes, sigtype);
                }
                else        // don't use Timestamp
                {
                    encodedSig = sgn.GetEncodedPKCS7(hash, DateTime.Now, null, ocsp, crlBytes, sigtype);
                }


                if (estimatedSize < encodedSig.Length)
                    throw new IOException("Not enough space");

                byte[] paddedSig = new byte[estimatedSize];
                System.Array.Copy(encodedSig, 0, paddedSig, 0, encodedSig.Length);

                PdfDictionary dic2 = new PdfDictionary();
                dic2.Put(PdfName.CONTENTS, new PdfString(paddedSig).SetHexWriting(true));
                sap.Close(dic2);
                stamper.Close();
                return true;
            }
            catch (Exception e)
            {
                //Console.WriteLine("Error insert signature: " + e.Message);
                return false;
            }
        }
    }
}
