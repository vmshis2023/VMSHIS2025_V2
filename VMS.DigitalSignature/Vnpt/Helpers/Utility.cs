using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VMS.ChuKySo.Api.Helpers
{
    public class Utility
    {
        private static string SymmetricAlgorithmName = "Rijndael";
        public static VietBaIT.Encrypt EncryptMethod = new VietBaIT.Encrypt(SymmetricAlgorithmName);
        public static Logger Log;
        public static LogFactory LogFactory;
        static Utility()
        {
            LogFactory = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config");
        }

        public static bool IsBase64String(string base64)
        {
            base64 = base64.Trim();
            return (base64.Length % 4 == 0) && Regex.IsMatch(base64, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }
    }
}
