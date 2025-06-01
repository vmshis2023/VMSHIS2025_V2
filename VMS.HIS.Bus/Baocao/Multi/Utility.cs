using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace VietBaIT.HandyTools
{
    public class GridColumnInfo
    {
        public string SqlName { get; set; }
        public string DisplayName { get; set; }
        public int Width { get; set; }

        public GridColumnInfo(string ColumnSqlName, string ColumnDisplayName)
        {
            SqlName = ColumnSqlName;
            DisplayName = ColumnDisplayName;
            Width = 100;
        }

        public GridColumnInfo(string ColumnSqlName, string ColumnDisplayName, int ColumnWidth)
        {
            SqlName = ColumnSqlName;
            DisplayName = ColumnDisplayName;
            Width = ColumnWidth;
        }
    }

    public class Utility
    {
        public static string ConfigPath = Application.StartupPath + "\\AppConfig\\";
        public static string DateTimeLongFormat = "dd-MM-yyyy HH:mm:ss";

        static Utility()
        {
            if (!System.IO.Directory.Exists(ConfigPath))
                System.IO.Directory.CreateDirectory(ConfigPath);
        }

        public static Int16 Int16Dbnull(object obj)
        {
            try
            {
                return Convert.ToInt16(obj);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static Int16 Int16Dbnull(object obj, Int16 DefaultVal)
        {
            try
            {
                if (obj == null) return DefaultVal;
                return Convert.ToInt16(obj);
            }
            catch (Exception)
            {
                return DefaultVal;
            }
        }

        public static Int32 Int32Dbnull(object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static Int32 Int32Dbnull(object obj, Int32 DefaultVal)
        {
            try
            {
                if (obj == null) return DefaultVal;
                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                return DefaultVal;
            }
        }

        public static Int64 Int64Dbnull(object obj)
        {
            try
            {
                return Convert.ToInt64(obj);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static Int64 Int64Dbnull(object obj, Int64 DefaultVal)
        {
            try
            {
                if (obj == null) return DefaultVal;
                return Convert.ToInt64(obj);
            }
            catch (Exception)
            {
                return DefaultVal;
            }
        }

        public static decimal DecimaltoDbnull(object obj)
        {
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static decimal DecimaltoDbnull(object obj, decimal DefaultVal)
        {
            try
            {
                if (obj == null) return DefaultVal;
                return Convert.ToDecimal(obj);
            }
            catch (Exception)
            {
                return DefaultVal;
            }
        }

        public static double DoubletoDbnull(object obj)
        {
            try
            {
                return Convert.ToDouble(obj);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static double DoubletoDbnull(object obj, double DefaultVal)
        {
            try
            {
                if (obj == null) return DefaultVal;
                return Convert.ToDouble(obj);
            }
            catch (Exception)
            {
                return DefaultVal;
            }
        }

        public static byte ByteDbnull(object obj)
        {
            try
            {
                return Convert.ToByte(obj);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static byte ByteDbnull(object obj, byte DefaultVal)
        {
            try
            {
                if (obj == null) return DefaultVal;
                return Convert.ToByte(obj);
            }
            catch (Exception)
            {
                return DefaultVal;
            }
        }

        public static string sDbnull(object obj)
        {
            try
            {
                return Convert.ToString(obj);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string sDbnull(object obj, string DefaultVal)
        {
            try
            {
                if (obj == null) return DefaultVal;
                return Convert.ToString(obj);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string ReadXMLtoString(string FileName, string FieldName, string ChildName)
        {
            string strxml = null;

            try
            {
                if (File.Exists(FileName))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(FileName);
                    XmlNodeList bookList = doc.GetElementsByTagName(FieldName);
                    foreach (XmlNode node in bookList)
                    {
                        XmlElement bookElement = (XmlElement)node;
                        strxml = bookElement.GetElementsByTagName(ChildName)[0].InnerText;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return strxml;
        }

        public static void ShowMsg(string Message)
        {
            MessageBox.Show(Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowStopMsg(string Message)
        {
            MessageBox.Show(Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        public static void ShowErrorMsg(string Message)
        {
            MessageBox.Show(Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowErrorMsg(Exception ex)
        {
            if (MessageBox.Show(ex.Message + "\r\n \r\n Xem thông tin lỗi ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Error,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                MessageBox.Show(ex.StackTrace,"Thông báo",MessageBoxButtons.OK,MessageBoxIcon.None);
        }

        public static bool AcceptQuestion(string Message, bool YesNoOrOKCancel, bool FirstButtonSelected)
        {
            var dialogResult = MessageBox.Show(Message, "Thông báo", YesNoOrOKCancel ? MessageBoxButtons.YesNo : MessageBoxButtons.OKCancel, MessageBoxIcon.Information, FirstButtonSelected ? MessageBoxDefaultButton.Button1 : MessageBoxDefaultButton.Button2);
            if (dialogResult == DialogResult.OK | dialogResult == DialogResult.Yes) return true;
            else return false;
        }

        public static void CallDynamicMethodFromDll(string FileName, string TypeName, string MethodName, object[] arrObjects)
        {
            var DLL = Assembly.LoadFile(FileName);
            MethodInfo addMethod = DLL.GetType(TypeName).GetMethod(MethodName);
            object result = addMethod.Invoke(null, arrObjects == null ? null : (arrObjects.Any() ? arrObjects : null));
        }

        public static void CallDynamicMethodFromDllIgnoreError(string FileName, string TypeName, string MethodName, object[] arrObjects)
        {
            try
            {
                var DLL = Assembly.LoadFile(FileName);
                MethodInfo addMethod = DLL.GetType(TypeName).GetMethod(MethodName);
                object result = addMethod.Invoke(null, arrObjects == null ? null : (arrObjects.Any() ? arrObjects : null));
            }
            catch (Exception ex)
            {
                
            }
        }

        public static void UpdateLogotoDatatable(ref DataTable dataTable, byte[] imageLogo)
        {
            if (!dataTable.Columns.Contains("LOGO")) dataTable.Columns.Add("LOGO", typeof(byte[]));
            foreach (DataRow dr in dataTable.Rows)
            {
                dr["LOGO"] = imageLogo;
            }
            dataTable.AcceptChanges();
        }

        public static DateTime GetSystemDatetime()
        {
            return DateTime.ParseExact(Utility.sDbnull(SqlUtility.ExecuteScalar("select convert(varchar(50),getdate(),121)")), "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
        }

        public static DataRow GetDataRow(DataTable table, string colSearch, object compareValue)
        {
            try
            {
                return (from dr in table.AsEnumerable() where sDbnull(dr.Field<object>(colSearch)) == sDbnull(compareValue) select dr).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static object GetValueFromDatarow(DataTable table, string colSearch, object compareValue, string colResult)
        {
            try
            {
                return (from dr in table.AsEnumerable() where sDbnull(dr.Field<object>(colSearch)) == sDbnull(compareValue) select dr.Field<object>(colResult)).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static object GetAssemblyFieldInfo(string fileName, string assmeblyPath)
        {
            string fieldName = assmeblyPath.Split('.').Last();
            assmeblyPath = assmeblyPath.Substring(0, assmeblyPath.Length - fieldName.Length - 1);
            Assembly assembly = Assembly.LoadFrom(fileName);
            Type t = assembly.GetType(assmeblyPath);

            //string s = "";
            //foreach (FieldInfo f in t.GetFields())
            //{
            //    s += f.Name + "\r\n";
            //}
            //Utility.ShowMsg(s);
            FieldInfo field = t.GetFields().SingleOrDefault(p => p.Name == fieldName);
            if (field != null) return field.GetValue(null);
            PropertyInfo propertyInfo = t.GetProperties().SingleOrDefault(p => p.Name == fieldName);
            return propertyInfo.GetValue(t, null);

        }
    }
}
