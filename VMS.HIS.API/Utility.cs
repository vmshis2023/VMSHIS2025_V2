using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace VMS.Invoice
{
    public class Utility
    {
        private static string SymmetricAlgorithmName = "Rijndael";
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
        /// <summary>
        /// Chuyển đổi một đối tượng sang kiểu Int16. Xử lý nếu trường hợp là null thì trả về giá trị 0
        /// </summary>
        /// <param name="obj">convert đối tượng 16</param>
        /// <returns></returns>
        public static Int16 Int16Dbnull(object obj)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt16(obj);
            }
        }

        /// <summary>
        /// Hàm thực hiện set cho đối tượng kiểu Int64 
        /// </summary>
        /// <param name="obj">Đối tượng convert thành 64  </param>
        /// <returns></returns>
        public static Int64 Int64Dbnull(object obj)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }

        /// <summary>
        /// hàm thực hiện convert đối tượng thành 64
        /// </summary>
        /// <param name="obj">Lấy đối tượng thành 64 </param>
        /// <param name="DefaultVal">Để giá trị mặc đi khi là null</param>
        /// <returns></returns>
        public static Int64 Int64Dbnull(object obj, object DefaultVal)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return Convert.ToInt64(DefaultVal);
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }

        /// <summary>
        /// Chuyển đổi một đối tượng sang kiểu Int16. Xử lý nếu trường hợp là null thì trả về giá trị DefaultVal
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="DefaultVal"></param>
        /// <returns></returns>
        public static Int16 Int16Dbnull(object obj, object DefaultVal)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return Convert.ToInt16(DefaultVal);
            }
            else
            {
                return Convert.ToInt16(obj);
            }
        }

        /// <summary>
        /// Chuyển đổi một đối tượng sang kiểu Int32. Xử lý nếu trường hợp là null thì trả về giá trị DefaultVal
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="DefaultVal"></param>
        /// <returns></returns>
        public static Int32 Int32Dbnull(object obj, object DefaultVal)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return Convert.ToInt32(DefaultVal);
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        public static bool IsNumeric(object Value)
        {
            return Microsoft.VisualBasic.Information.IsNumeric(Value);
        }

        /// <summary>
        /// Thủ tục thường dùng cho sự kiện Keypress của TextBox để ngăn không cho nhập ký tự
        /// </summary>
        /// <param name="newChar">Thường là e.KeyChar</param>
        /// <param name="CurrentString">Thường là TextObject.Text. Trong đó TextObject là tên của TextObject bạn muốn ràng buộc</param>
        /// <returns></returns>
        public static bool NumbersOnly(char newChar, string CurrentString)
        {
            if (newChar.ToString() != Microsoft.VisualBasic.Constants.vbBack)
            {
                return (IsNumeric(newChar.ToString()) || IsNumeric(newChar.ToString() + CurrentString)) ? false : true;
                //check if numeric is returned
            }
            return false; // for backspace
        }
        public static string FormatDecimal()
        {
            return "{0:#,#.###}";
        }
        public static string FormatDecimal1()
        {
            return "{0:#,#.000}";
        }
        public static string FormatDigit()
        {
            return "{0:#,#}";
        }
        ///<summary>
        ///<para>Xử lý một Object nếu là null thì trả về giá trị truyền vào</para>
        ///</summary>   
        /// <param name="obj"><para>Object truyền vào <see cref="object"/></para></param>     
        /// <param name="DefaultVal"><para>Giá trị mặc định sẽ trả về nếu obj=null <see cref="object"/></para></param>     
        ///<returns>Đối tượng chuỗi trả về từ Object. Nếu obj=null thì trả về DefaultVal</returns>
        public static string sDbnull(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return string.Empty;
            }
            else
            {
                return DoTrim(obj.ToString());
            }
            // Utility.Int32Dbnull()
        }

        public static double fDbnull(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return 0D;
            }
            else
            {
                return Convert.ToDouble(obj);
            }
        }
        public static string sDbnull(object obj, bool NotAllowedEmptyString)
        {
            if (obj == null || obj == DBNull.Value)
            {
                if (NotAllowedEmptyString)
                    return " ";
                else
                    return "";
            }
            else
            {
                return DoTrim(obj.ToString());
            }
        }
        public static string DoTrim(string value)
        {
            return value.TrimStart().TrimEnd();
        }
        public static decimal DecimaltoDbnull(object obj)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return 0m;
            }
            else
            {
                return Convert.ToDecimal(obj);
            }

        }

        /// <summary>
        /// HÀM THỰC HIỆN CONVERT DOUBLE NẾU ĐỐI TƯỢNG LÀ NULL
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double DoubletoDbnull(object obj)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return 0d;
            }
            else
            {
                return Convert.ToDouble(obj);
            }
        }

        /// <summary>
        /// HÀM THỰC HIỆN LẤY CONVERT ĐỐI TƯỢNG NẾU LÀ NULL THÌ 
        /// </summary>
        /// <param name="obj">GIÁ TRỊ TRUYỀN VÀO</param>
        /// <param name="DefaultVal">GIÁ TRỊ MẶC ĐỊNH</param>
        /// <returns></returns>
        public static double DoubletoDbnull(object obj, object DefaultVal)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return Convert.ToDouble(DefaultVal);
            }
            else
            {
                return Convert.ToDouble(obj);
            }
        }

        /// <summary>
        /// Hàm thực hiện convert đối tượng thành Decimal
        /// </summary>
        /// <param name="obj">Đối tượng cần convert</param>
        /// <param name="DefaultVal">Giá trị mặc định nếu obj là null</param>
        /// <returns></returns>
        public static decimal DecimaltoDbnull(object obj, object DefaultVal)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return Convert.ToDecimal(DefaultVal);
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }
    }

}
