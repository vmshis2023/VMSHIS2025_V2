using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Org.BouncyCastle.Pkcs;
using System.IO;
using Org.BouncyCastle.Crypto;
using iTextSharp.text.pdf.security;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System.Security;

namespace DEMO_CLOUD_CA_DOTNET
{
    public class UtilSigner
    {
        public UtilSigner()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static byte[] SignHashUseP12(string fileP12, string password, byte[] sh)
        {
            try
            {
                string alias = null;
                Pkcs12Store pk12;

                //First we'll read the certificate file
                pk12 = new Pkcs12Store(new FileStream(fileP12, FileMode.Open, FileAccess.Read), password.ToCharArray());
                //then Iterate throught certificate entries to find the private key entry
                System.Collections.IEnumerator i = pk12.Aliases.GetEnumerator();
                while (i.MoveNext())
                {
                    alias = ((string)i.Current);
                    if (pk12.IsKeyEntry(alias))
                        break;
                }

                AsymmetricKeyParameter akp = pk12.GetKey(alias).Key;

                PrivateKeySignature pks = new PrivateKeySignature(akp, DigestAlgorithms.SHA1);

                byte[] extSignature = pks.Sign(sh);

                return extSignature;
            }
            catch (Exception e)
            {
                //Console.WriteLine("Error: " + e.Message);
                return null;
            }
        }

        public static string GetCNFromDN(string dn)
        {
            try
            {
                string[] info = dn.Split(',');
                string cn = "";
                for (int i = 0; i < info.Length; i++)
                {
                    if (info[i].IndexOf("CN=") != -1)
                    {
                        cn = info[i].Split('=')[1];
                    }
                }
                if (cn != "")
                {
                    return ConvertTVKhongDau(cn);
                }
                return cn;
            }
            catch (Exception e)
            {
                //Console.WriteLine("Error create hash: " + e.Message);
                return "";
            }
        }

        public static string ConvertTVKhongDau(string str)
        {
            string[] a = { "à", "á", "ạ", "ả", "ã", "â", "ầ", "ấ", "ậ", "ẩ", "ẫ", "ă", "ằ", "ắ", "ặ", "ẳ", "ẵ" };
            string[] aUpper = { "À", "Á", "Ạ", "Ả", "Ã", "Â", "Ầ", "Ấ", "Ậ", "Ẩ", "Ẫ", "Ă", "Ằ", "Ắ", "Ặ", "Ẳ", "Ẵ" };
            string[] e = { "è", "é", "ẹ", "ẻ", "ẽ", "ê", "ề", "ế", "ệ", "ể", "ễ" };
            string[] eUpper = { "È", "É", "Ẹ", "Ẻ", "Ẽ", "Ê", "Ề", "Ế", "Ệ", "Ể", "Ễ" };
            string[] i = { "ì", "í", "ị", "ỉ", "ĩ" };
            string[] iUpper = { "Ì", "Í", "Ị", "Ỉ", "Ĩ" };
            string[] o = { "ò", "ó", "ọ", "ỏ", "õ", "ô", "ồ", "ố", "ộ", "ổ", "ỗ", "ơ", "ờ", "ớ", "ợ", "ở", "ỡ" };
            string[] oUpper = { "Ò", "Ó", "Ọ", "Ỏ", "Õ", "Ô", "Ồ", "Ố", "Ộ", "Ổ", "Ỗ", "Ơ", "Ờ", "Ớ", "Ợ", "Ở", "Ỡ" };
            string[] u = { "ù", "ú", "ụ", "ủ", "ũ", "ư", "ừ", "ứ", "ự", "ử", "ữ" };
            string[] uUpper = { "Ù", "Ú", "Ụ", "Ủ", "Ũ", "Ư", "Ừ", "Ứ", "Ự", "Ử", "Ữ" };
            string[] y = { "ỳ", "ý", "ỵ", "ỷ", "ỹ" };
            string[] yUpper = { "Ỳ", "Ý", "Ỵ", "Ỷ", "Ỹ" };
            str = str.Replace("đ", "d");
            str = str.Replace("Đ", "D");
            foreach (string a1 in a)
            {
                str = str.Replace(a1, "a");
            }
            foreach (string a1 in aUpper)
            {
                str = str.Replace(a1, "A");
            }
            foreach (string e1 in e)
            {
                str = str.Replace(e1, "e");
            }
            foreach (string e1 in eUpper)
            {
                str = str.Replace(e1, "E");
            }
            foreach (string i1 in i)
            {
                str = str.Replace(i1, "i");
            }
            foreach (string i1 in iUpper)
            {
                str = str.Replace(i1, "I");
            }
            foreach (string o1 in o)
            {
                str = str.Replace(o1, "o");
            }
            foreach (string o1 in oUpper)
            {
                str = str.Replace(o1, "O");
            }
            foreach (string u1 in u)
            {
                str = str.Replace(u1, "u");
            }
            foreach (string u1 in uUpper)
            {
                str = str.Replace(u1, "U");
            }
            foreach (string y1 in y)
            {
                str = str.Replace(y1, "y");
            }
            foreach (string y1 in yUpper)
            {
                str = str.Replace(y1, "Y");
            }
            return str;
        }

        public static string GenerateTempFile()
        {
            string tempFile;
            try
            {
                tempFile = System.IO.Path.GetTempFileName();
                return tempFile;
            }
            catch (System.IO.IOException e)
            {
                //Console.WriteLine("Error create temp file: " + e.Message);
                return "";
            }
        }

        public static string getSignName()
        {
            string signName = "" + GetCurrentMilli();
            return signName;
        }

        public static double GetCurrentMilli()
        {
            try
            {
                DateTime Jan1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return (long)((DateTime.UtcNow - Jan1970).TotalMilliseconds);
            }
            catch (Exception e)
            {
                //Console.WriteLine("Error get current milli: " + e.Message);
                return 0;
            }
        }

        public static X509Certificate[] GetChainCertFromString(string certString)
        {
            try
            {
                System.Security.Cryptography.X509Certificates.X509Certificate cert = new System.Security.Cryptography.X509Certificates.X509Certificate();
                byte[] rawData = GetBytes(certString);
                cert.Import(rawData);
                X509Certificate certBouncyCastle = DotNetUtilities.FromX509Certificate(cert);

                X509Certificate[] chain = new X509Certificate[1];
                chain[0] = (X509Certificate)certBouncyCastle;
                return chain;
            }
            catch (Exception e)
            {
                //Console.WriteLine("Error get Chain cert: " + e.Message);
                return null;
            }
        }

        public static X509Certificate[] GetChainCertFromFileCert(string fileCert)
        {
            try
            {
                System.Security.Cryptography.X509Certificates.X509Certificate cert = new System.Security.Cryptography.X509Certificates.X509Certificate();

                cert.Import(fileCert);
                X509Certificate certBouncyCastle = DotNetUtilities.FromX509Certificate(cert);

                X509Certificate[] chain = new X509Certificate[1];
                chain[0] = (X509Certificate)certBouncyCastle;
                return chain;

            }
            catch (Exception e)
            {
                //Console.WriteLine("Error get Chain cert: " + e.Message);
                return null;
            }
        }

        public static X509Certificate[] GetChainCertFromP12(string fileP12, string password)
        {
            try
            {
                using (SecureString securePwd = new SecureString())
                {
                    foreach (char c in password)
                    {
                        securePwd.AppendChar(c);
                    }
                    System.Security.Cryptography.X509Certificates.X509Certificate2 cert = new System.Security.Cryptography.X509Certificates.X509Certificate2(fileP12, securePwd);

                    X509Certificate certBouncyCastle = DotNetUtilities.FromX509Certificate(cert);

                    X509Certificate[] chain = new X509Certificate[1];
                    chain[0] = (X509Certificate)certBouncyCastle;
                    return chain;
                }

            }
            catch (Exception e)
            {
                //Console.WriteLine("Error get Chain cert: " + e.Message);
                return null;
            }
        }

        public static byte[] GetBytes(string str)
        {
            try
            {
                byte[] bytes = new byte[str.Length * sizeof(char)];
                System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
                return bytes;
            }
            catch (Exception e)
            {
                //Console.WriteLine("Error get Byte from string: " + e.Message);
                return null;
            }
        }

        public static string GetString(byte[] bytes)
        {
            try
            {
                char[] chars = new char[bytes.Length / sizeof(char)];
                System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
                return new string(chars);
            }
            catch (Exception e)
            {
                //Console.WriteLine("Error get String from byte: " + e.Message);
                return null;
            }
        }
    }
}
