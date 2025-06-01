using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VietBaIT.HandyTools
{
    public static class PreloadedParameters
    {
        public static string ParentBranch_Name = "";
        public static string Branch_Name = "";
        public static string Branch_Address = "";
        public static string Branch_Phone = "";
        public static string Branch_BussinessPhone = "";
        public static string Branch_HotPhone = "";
        public static string Branch_DutyPhone = "";
        public static string Branch_FAX = "";
        public static string Branch_EMAIL = "";
        public static string Branch_TaxCode = "";

        public static string UserName = "";
        public static bool IsAdmin = false;
        public static string ServerName = "192.168.1.254";

        public static string sFolderName = Application.StartupPath + "/AppConfig/";
        public static byte[] ImageLogo = null;
        public static Color SystemColor = SystemColors.Control;

        static PreloadedParameters()
        {
            try
            {
                DataTable dt = SqlUtility.GetDataSet("exec Core_GetManagementUnit ''").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ParentBranch_Name = Utility.sDbnull(dt.Rows[0]["sParentBranchName"]);
                    Branch_Name = Utility.sDbnull(dt.Rows[0]["sName"]);
                    Branch_Address = Utility.sDbnull(dt.Rows[0]["sAddress"]);
                    Branch_Phone = Utility.sDbnull(dt.Rows[0]["sPhone"]);
                    Branch_BussinessPhone = Utility.sDbnull(dt.Rows[0]["sBussinessPhone"]);
                    Branch_HotPhone = Utility.sDbnull(dt.Rows[0]["sHotPhone"]);
                    Branch_DutyPhone = Utility.sDbnull(dt.Rows[0]["sDutyPhone"]);
                    Branch_FAX = Utility.sDbnull(dt.Rows[0]["sFAX"]);
                    Branch_EMAIL = Utility.sDbnull(dt.Rows[0]["sEMAIL"]);
                    Branch_TaxCode = Utility.sDbnull(dt.Rows[0]["sTaxCode"]);    
                }

                ServerName = Utility.ReadXMLtoString("Config.xml", "Config", "SERVERADDRESS");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
    }
}
