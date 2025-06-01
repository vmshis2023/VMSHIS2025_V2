using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic;
using System.Xml;

using System.Data;
using System.Windows.Forms;
using VNS.Libs;
using VNS.Properties;

namespace VNS.Core.Classes
{
  public class ConnectionSQL
  {
     
      public bool ReadConfig()
      {
          try
          {
              PropertyLib._ConfigProperties = PropertyLib.GetConfigProperties();
              globalVariables.ServerName = PropertyLib._ConfigProperties.DataBaseServer;
              globalVariables.sUName = PropertyLib._ConfigProperties.UID;
              globalVariables.sPwd = PropertyLib._ConfigProperties.PWD;
              globalVariables.sDbName = PropertyLib._ConfigProperties.DataBaseName;
              globalVariables.sMenuStyle = "DOCKING"; // Utility.sDbnull(dsConfigXML.Tables[0].Rows[0]["INTERFACEDISPLAY"], "MENU").ToUpper();

              globalVariables.MA_KHOA_THIEN = PropertyLib._ConfigProperties.MaKhoa;
              globalVariables.MA_PHONG_THIEN = PropertyLib._ConfigProperties.Maphong;
              globalVariables.SOMAYLE = PropertyLib._ConfigProperties.Somayle;
              globalVariables.MIN_STT=PropertyLib._ConfigProperties.Min;
              globalVariables.MAX_STT = PropertyLib._ConfigProperties.Max;
              return true;
          }
          catch(Exception ex)
          {
              Utility.ShowMsg("Lỗi khi load file Config.xml\n" + ex.Message);
              return false;
          }
      }
     public ConnectionSQL()
     {
     }
     
      public  string KhoiTaoKetNoi()
      {

          string strSQL =
              string.Format(
                  "workstation id={0};packet size=4096;data source={0};persist security info=False;initial catalog={1};uid={2};pwd={3}",
                  globalVariables.ServerName, globalVariables.sDbName, globalVariables.sUName, globalVariables.sPwd);

          return strSQL;


      }

   
     
     
    }
}
