using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using C1.C1Excel;
using Microsoft.VisualBasic;
using System.IO;
using System.Reflection;
using System.Collections;
using Microsoft.Win32;
using SubSonic;
using System.Data.SqlClient;
using System.Drawing;
using System.Net.Mail;
using System.Text.RegularExpressions;
//using Mabry.Windows.Forms.Barcode;
using System.Net;
using Janus.Windows.GridEX;
using System.Drawing.Drawing2D;
using Microsoft.VisualBasic;
using VMS.HIS.DAL;
using VB6 = Microsoft.VisualBasic;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Threading;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX.EditControls;
using VNS.Properties;
using Janus.Windows.UI.StatusBar;
using System.ServiceProcess;
using System.ComponentModel;
using Leadtools;
using Leadtools.Barcode;
using Janus.Windows.CalendarCombo;
using Janus.Windows.EditControls;
using Janus.Windows.UI.Tab;
using System.Net.Sockets;
using System.Globalization;

using System.Diagnostics;

using QRCoder;
using NLog;

namespace VNS.Libs
{
    public class Tralaithuoctaiquay
    {
        public long id_chitiet_thanhtoan { get; set; }
        public long id_donthuoc { get; set; }
        public long id_chitiet_donthuoc { get; set; }
        public int sl_tralai { get; set; }
        public int id_thuoc { get; set; }
        public byte id_loaithanhtoan { get; set; }
        public decimal don_gia { get; set; }
    }
    public class HamDungChung
    {


        #region Excel Style


        public static XLStyle styleAllBorder(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.AlignHorz = XLAlignHorzEnum.Right;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;

            return style;
        }

        public static XLStyle styleStringAllBorder(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Format = "";
            style.Font = new Font("Times New Roman", 11, FontStyle.Regular);
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.WordWrap = true;
            style.Locked = true;

            return style;
        }
        public static XLStyle styleNumber_UnStyle(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Regular);
            style.Format = "### ### ### ### ###";
            style.AlignHorz = XLAlignHorzEnum.Center;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;
            return style;
        }
        public static XLStyle styleNumber(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Regular);
            style.Format = "#,##0.00";
            style.AlignHorz = XLAlignHorzEnum.Right;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;
            return style;
        }
        public static XLStyle styleNumber_Bold(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Bold);
            style.Format = "### ### ### ### ###";
            style.AlignHorz = XLAlignHorzEnum.Right;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;
            return style;
        }
        public static XLStyle styleNumber_Right(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Regular);
            style.Format = "### ### ### ### ###";
            style.AlignHorz = XLAlignHorzEnum.Right;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;
            return style;
        }
        public static XLStyle styleStringCenter_Rotate(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Bold);
            style.Format = "### ### ### ### ###";
            style.AlignHorz = XLAlignHorzEnum.Center;
            style.Rotation = 90;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;
            return style;
        }
        public static XLStyle styleStringCenter_Bold(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Bold);
            style.Format = "### ### ### ### ###";
            style.AlignHorz = XLAlignHorzEnum.Center;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;
            return style;
        }
        public static XLStyle styleStringCenter(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Regular);
            style.Format = "### ### ### ### ###";
            style.AlignHorz = XLAlignHorzEnum.Center;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;
            return style;
        }
        public static XLStyle styleStringLeft(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Regular);
            style.Format = "### ### ### ### ###";
            style.AlignHorz = XLAlignHorzEnum.Left;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.WordWrap = true;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;
            return style;
        }
        public static XLStyle styleStringRight(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Regular);
            style.Format = "### ### ### ### ###";
            style.AlignHorz = XLAlignHorzEnum.Right;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.WordWrap = true;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;
            return style;
        }
        public static XLStyle styleStringLeft_Bold(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Bold);
            style.Format = "### ### ### ### ###";
            style.AlignHorz = XLAlignHorzEnum.Left;

            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;
            return style;
        }
        public static XLStyle styleDecimalAllBorder(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Regular);
            style.Format = "### ### ### ### ##0.00";
            style.AlignHorz = XLAlignHorzEnum.Right;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;

            return style;
        }

        public static XLStyle StyleTiTle(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 12, FontStyle.Bold);


            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Double;
            style.WordWrap = true;


            return style;
        }
        public static XLStyle StyleTiTleCenter(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 12, FontStyle.Italic);

            style.AlignHorz = XLAlignHorzEnum.Center;
            style.AlignVert = XLAlignVertEnum.Center;

            style.WordWrap = true;


            return style;
        }
        public static XLStyle StyleTiTleKy(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 12, FontStyle.Bold);


            style.AlignVert = XLAlignVertEnum.Center;

            style.WordWrap = false;


            return style;
        }
        public static XLStyle styleLevel_1(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 14, FontStyle.Bold);


            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.WordWrap = true;


            return style;
        }
        public static XLStyle styleLevel_2(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Bold);


            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.WordWrap = true;
            return style;
        }
        public static XLStyle styleLevel_3(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Bold | FontStyle.Italic);

            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.WordWrap = true;
            return style;
        }
        public static XLStyle styleLevel_4(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Italic);


            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.WordWrap = true;
            return style;
        }

        public static XLStyle styleDecimalBoldAllBorder(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Bold);
            style.Format = "### ### ### ### ### ### ##0.00";
            style.AlignHorz = XLAlignHorzEnum.Right;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Hair;
            style.BorderTop = XLLineStyleEnum.Hair;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;

            return style;
        }

        // trường hợp số thập phân,  lấy 2 số đằng sau
        public static XLStyle styleNumberDecimalBoldAllBorder_Null(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);
            style.Format = "### ### ### ### ### ### ##0.00";
            style.Font = new Font("Times New Roman", 11, FontStyle.Bold);

            style.AlignHorz = XLAlignHorzEnum.Right;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;
            return style;
        }
        public static XLStyle styleDecimalBoldAllBorder_Null(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);
            style.Format = "#,##0.00";
            style.Font = new Font("Times New Roman", 11, FontStyle.Bold);
            style.AlignHorz = XLAlignHorzEnum.Right;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;


            return style;
        }
        public static XLStyle styleStringCenter_UnBorder(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Regular);
            style.Format = "### ### ### ### ###";
            style.AlignHorz = XLAlignHorzEnum.Center;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.None;
            style.BorderTop = XLLineStyleEnum.None;
            style.BorderRight = XLLineStyleEnum.None;
            style.BorderLeft = XLLineStyleEnum.None;
            style.Locked = false;
            return style;
        }

        public static XLStyle styleDecimalBoldAllBorder_Money(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);
            style.Format = "#,##0.00";
            style.Font = new Font("Times New Roman", 11, FontStyle.Regular);
            style.AlignHorz = XLAlignHorzEnum.Right;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;
            return style;
        }
        public static XLStyle styleDecimalBoldAllBorder_Null_TimKiem(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);


            style.AlignHorz = XLAlignHorzEnum.Right;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;


            return style;
        }
        // Trường hợp số 0 lấy mình số 0 ra
        public static XLStyle styleNumberDecimalBoldAllBorder_So0(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Bold);
            style.Format = "### ### ### ### ### ### ##0.00";
            style.AlignHorz = XLAlignHorzEnum.Right;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;

            return style;
        }
        public static XLStyle styleDecimalBoldAllBorder_So0(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Bold);
            style.Format = "#,##0.00";
            style.AlignHorz = XLAlignHorzEnum.Right;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;

            return style;
        }

        public static XLStyle To_o(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Bold);
            style.Format = "#,##0.00";
            style.AlignHorz = XLAlignHorzEnum.Center;
            style.AlignVert = XLAlignVertEnum.Center;
            style.WordWrap = true;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;

            return style;
        }
        public static XLStyle styleStringItalicAllBorder(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Bold | FontStyle.Italic);
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Hair;
            style.BorderTop = XLLineStyleEnum.Hair;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.WordWrap = true;

            return style;
        }

        public static XLStyle styleDecimalItalicAllBorder(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Bold | FontStyle.Italic);
            style.Format = "### ### ### ### ### ### ##0.00";
            style.AlignHorz = XLAlignHorzEnum.Right;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Hair;
            style.BorderTop = XLLineStyleEnum.Hair;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;

            return style;
        }



        public static XLStyle styleRightMediumBorder(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.AlignHorz = XLAlignHorzEnum.Right;
            style.BorderBottom = XLLineStyleEnum.Hair;
            style.BorderTop = XLLineStyleEnum.Hair;
            style.BorderRight = XLLineStyleEnum.Medium;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;

            return style;
        }

        public static XLStyle styleTopMediumBorder(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.AlignHorz = XLAlignHorzEnum.Center;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Medium;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.WordWrap = true;
            style.Locked = false;

            return style;
        }

        public static XLStyle styleAllThinBorder(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.AlignHorz = XLAlignHorzEnum.Center;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Thin;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.WordWrap = true;
            style.Locked = false;

            return style;
        }

        public static XLStyle styleTopThinBorder(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.AlignHorz = XLAlignHorzEnum.Right;
            style.BorderBottom = XLLineStyleEnum.Hair;
            style.BorderTop = XLLineStyleEnum.Thin;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.WordWrap = true;
            style.Locked = false;

            return style;
        }

        public static XLStyle styleStringCenterAllBorder(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Regular);
            style.AlignHorz = XLAlignHorzEnum.Center;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Hair;
            style.BorderTop = XLLineStyleEnum.Hair;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.WordWrap = true;
            style.Locked = false;

            return style;
        }

        public static XLStyle styleStringBoldCenterAllBorder(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Bold);
            style.AlignHorz = XLAlignHorzEnum.Center;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Hair;
            style.BorderTop = XLLineStyleEnum.Hair;
            style.BorderRight = XLLineStyleEnum.Thin;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.WordWrap = true;
            style.Locked = false;

            return style;
        }

        public static XLStyle styleDecimalRightMediumBorder(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Regular);
            style.Format = "### ### ### ### ###.0";
            style.AlignHorz = XLAlignHorzEnum.Right;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Hair;
            style.BorderTop = XLLineStyleEnum.Hair;
            style.BorderRight = XLLineStyleEnum.Medium;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;

            return style;
        }

        public static XLStyle styleDecimalBoldRightMediumBorder(C1XLBook book)
        {
            XLStyle style = new XLStyle(book);

            style.Font = new Font("Times New Roman", 11, FontStyle.Bold);
            style.Format = "### ### ### ### ###.0";
            style.AlignHorz = XLAlignHorzEnum.Right;
            style.AlignVert = XLAlignVertEnum.Center;
            style.BorderBottom = XLLineStyleEnum.Hair;
            style.BorderTop = XLLineStyleEnum.Hair;
            style.BorderRight = XLLineStyleEnum.Medium;
            style.BorderLeft = XLLineStyleEnum.Thin;
            style.Locked = false;

            return style;
        }
        #endregion


    }
    public static class FormExtensions
    {
        public static bool? SetScreenToFirstNonPrimary(this Form self)
        {
            try
            {
               
                // Retrieves the collection of available screens (monitors)
                var aScreens = Screen.AllScreens;
                // If count is not greater than 1 then exit
                if (aScreens.Length <= 1)
                    return false; // screen kept original

                // Saves current screen reference
                var screenOld = Screen.FromControl(self);
                foreach (var screen in aScreens)
                {
                    // Skips primary and current screen
                    if (screen.Primary || screen.Equals(screenOld))
                        continue;

                    var boundsScreen = screen.Bounds;

                    var oldState = self.WindowState;
                    // If form is currently maximized ...
                    if (oldState == FormWindowState.Maximized)
                    {
                        self.WindowState = FormWindowState.Normal;
                        self.StartPosition = FormStartPosition.Manual;
                        self.Location = boundsScreen.Location;
                        self.WindowState = FormWindowState.Maximized;
                    }
                    else
                    {
                        self.StartPosition = FormStartPosition.Manual;
                        // Center into new screen
                        var sizeGaps = boundsScreen.Size - self.Size;
                        var x = boundsScreen.Left + (sizeGaps.Width / 2);
                        var y = boundsScreen.Top + (sizeGaps.Height / 2);
                        self.Location = new Point(x, y);
                    }

                    return true; // screen has been changed
                }

                return false; // screen kept original
            }
            catch (Exception ex)
            {
                // process exception ex
                // ...

                return null; // error occurred
            }
        }
    }
    /// <summary>
    /// Biến xác định xem Form được gọi từ nguồn nào
    /// </summary>
    public enum CallAction
    {
        FromMenu = 0,
        FromParentFormList = 1,
        FromAnotherForm = 2
    } ;
    public enum TrangThaiCLS
    {
        KhongChuyenCLS = 0,
        ChuyenCLS = 1,
        DaThucHien = 2

    } ;
    public enum Dotuoi
    {
        TreSosinh = 0,
        TreEm = 1,
        Nguoilon = 2

    } ;
    public enum LoaiTamKe
    {
        KEDONTHUOC = 1,
        XUATKHO = 10,
    }
    public enum LoaiPhieu
    {
        PhieuNhapKho = 1,
        PhieuXuatKho = 2,
        PhieuXuatKhoBenhNhan = 3,
        PhieuNhapTraLaiKhoLeVeKhoChan = 4,//Kho chẵn nhận thuốc trả về từ kho lẻ +
        PhieuXuatKhoLeTraKhoChan = 9,//Kho lẻ trả thuốc về kho chẵn -
        PhieuNhapTraKhoLe = 5,//Kho nội trú Nhận thuốc từ tủ trực trả về +
        PhieuXuatKhoa = 6,
        PhieuHuy = 7,
        PhieuTraNcc = 8,
        PhieuThanhly=10,
        Phieutrathuocthua=11,
        PhieuNhapKhoTututruc = 12,
        PhieuXuatKhoBenhNhanTuTutruc = 13,
        PhieuXuatkhoTuyenXaHuyen=14,
        PhieuXuatKhoaPhongchucnang = 15,
        PhieuXuatKhoaPhongchucnangTraKho = 16,
        PhieuTralaithuocTaiQuay = 17,
        PhieuXuatTraKhoLe = 18,//Tủ trực trả thuốc về kho nội trú 
        PhieuXuatHaophi = 19,//Tủ trực trả thuốc về kho nội trú -
         PhieuXuatNgoai = 20//Xuất đặc biệt- xuất ngoài, ví dụ điều chuyển thuốc tới 1 kho ở chi nhánh khác hoặc BV khác,...
    } ;
    public enum TonghopStatus
    {
        TongHopKhoaDieuTri = 1,
        TongHopXetDuyet = 2
    } ;
    public enum LoaiBenhNhan
    {
        NgoaiTru = 0,
        NoiTru = 1,
        ChoRaVien = 2,
        RaVien = 3
    } ;
    public enum LoaiThuocVT
    {
        Thuoc = 0,
        VatTuYte = 1

    } ;
    public enum ConditionPhongGiuong
    {
        TatCa = -1,
        Bang = 0,
        Khac = 1
    } ;
    public enum ConditionPhongKhoa
    {
        TatCa = -1,
        Bang = 0,
        Khac = 1
    } ;

    public enum LoaiChanDoan
    {
        GhiNho = 0,
        MauChanDoan = 1
    } ;
    public enum TrangthaiNoitru
    {
        NgoaiTru = 0,
        NoiTru = 1,
        Dangdieutri=2,
        Lapphieuravien=3,
        Xacnhandulieunoitru=4,
        Taichinhduyet = 5,
        Thanhtoanravien = 6
    } ;
    public enum KieuThanhToan
    {
        NgoaiTru = 0,
        NoiTru = 1,
        TamUng = 2,
        GoiDvu = 3,


    } ;
    public enum KieuLoaiThanhToan
    {
        ChiPhiThem = 0,
        Kham = 1,
        CLS = 2,
        Thuoc = 3,
        Giuong = 4,
        VTTH=5,
        TamUng = 6,
        TienAn = 7,
        GoiDvu = 8,
        TraLaiTien = 9



    } ;
    public enum CallNOITRU
    {
        RAVIEN = 0,
        CHORAVIEN = 1,
        KETHUC = 2
    } ;

    public enum CallObject_BN
    {
        BN_NGOAITRU = 0,
        BN_NOITRU = 1
    } ;

    public enum CallBenhAn
    {
        TIEPDON = 1,
        PHANBUONG = 2
    } ;

    public enum CallNhapVien
    {

        NHAPVIEN = 1,
        BENHAN = 2
    } ;
    public enum CallInPhieuXN
    {
        Assign_ID = 1,
        Payment_ID,
        Treat_ID = 2,
        Exam_ID = 3
    } ;
    //public enum actionTrangThai {};
    public enum CallPaymentType
    {
        KhamBenh = 1,
        DichVu = 2,
        Thuoc = 2,
        GiuongBenh = 4,
        VatTuThue = 5
    } ;

    public enum CallActionTemp
    {
        KeDonThuoc = 1,
        MauDonthuoc = 2
    } ;

    public enum CallActionMauDichVu_Thuoc
    {
        MAUTHUOC = 1,
        MAUDICHVU = 2
    } ;

    public enum CallActionPayment
    {
        NoiTru = 1,
        NgoaiTru = 0
    } ;

    public enum LoaiBenhAn
    {
        BA_Thuong = 1,
        BA_TuyenGiap = 2,
        BA_DaiThaoDuong = 3,
        BA_TangHuyetAp = 4,
        BA_Basedow = 5,
        BA_ViemGanB = 6,
        BA_Cop = 7,
        BA_TaiMuiHong = 8,
        BA_RangHamMat = 9,

    };
    public enum CallActionExamInfo
    {
        FromMeetingPatient = 0,
        FromAccouting = 1,
        FromCreateNewPatient = 2
    } ;

    public enum CallActionInDonThuoc
    {
        TreatID = 0,
        Exam_ID = 1,
        Pres_ID = 2
    } ;

    public enum actionHeathCare
    {
        FromHeath = 0,
        FromTreat = 1
    } ;
    public enum actionPhieuChamSoc
    {
        ChamSoc = 0,
        TheoDoi = 1
    } ;
    public enum CallActionAssign
    {
        FromPatientCode = 0,
        FromTreat = 1
    } ;

    public enum CallActionThanhToan
    {
        ThanhToan = 0,
        THop = 1
    } ;

    public enum CallActionKieuKeDon
    {
        TheoDoiTuong = 0,
        TuTuc = 1
    } ;

    /// <summary>
    /// Kiểu kết quả trả về khi thực hiện các lệnh DML với CSDL
    /// </summary>
    public enum ActionResult
    {
        ActAsInput = 1,
        DataHasUsedinAnotherTable = 2,
        Error = 3,
        Exceed = 4,
        Exception = 5,
        ExistedRecord = 6,
        Success = 7,
        NotEnoughDrugInStock = 8,
        UNKNOW = 9,
        PresIsNotPayment = 10,
        IsNotUserName = 11,
        PresIsConfirmed = 12,
        AssignIsConfirmed = 14,
        Cancel = 15,
        DataChanged = 16,
        DataUsed=17,
        notExists,
        NodataFound=19
    } ;

    /// <summary>
    /// Kiểu dữ liệu thao tác
    /// </summary>
    public enum action
    {
        Insert = 0,
        Update = 1,
        Delete = 2,
        Select = 3,
        FirstOrFinished = 4,
        Normal = 5,
        Error = -1,
        doNothing = 6,
        ConfirmData = 7,
        Add = 8,
        Duplicate=9,
        Cancel=10,
        View=11

    } ;
    public enum newaction
    {
        Insert = 0,
        Update = 1,
        Delete = 2,
        Save = 3,
        ConfirmData = 4,
        Select = 5,
        Search = 6,
        Capture = 7,
        Print = 8,
        Browse = 9,
        FTP = 10,
        View = 11,
        Export = 12,
        Import = 13,
        Found = 14,
        NotFound = 15,
        Refresh = 16,
        Reset = 17,
        Error = 18,
        Empty = 19,
        Restore = 20,
        Cut = 21,
        CancelData=22,
        CheckLicense=23,
        Add=24,
        Duplicate = 25,
        FirstOrFinished = 26,
        Normal = 27,
        DoNothing = 28,
        Payment =29,
        Check=30,
        Move=31,
        Transfer=32,
        Start=33,
        Begin=34,
        Download=35,
        Upload = 36
    };

    public enum actionExamPres
    {
        PresExam = 1,
        PresNoExam = 2
    } ;

    public enum ParamsPaymentType
    {
        RegExam = 1,
        Pres = 4
    } ;

    /// <summary>
    /// Drug status in stock
    /// </summary>
    public enum DRUG_STATUS
    {
        Available = 0,
        OutOfStock = 1,
        Expired = 2
    }

    public enum enObjectType
    {
        Thuoc = 0,
        Surgery = 1,
        DichvuCLS = 2,
        DetailService = 3,
        Material = 4,
        Department = 5,
        RoomBed = 6,
        GoiDichVu = 7
    }

    /// <summary>=5
    /// Kiểu tiến trình đang làm việc. Được dùng khi việc Search dữ liệu để Report hoặc hiện thị hoặc làm việc nào đó mất nhiều time
    /// Form Progress sẽ hiển thị lên và thông báo tiến trình HIS đang thực hiện là gì để User biết đỡ nhàm chán
    /// </summary>
    public enum ProcessStatus
    {
        PreparingData = 0,
        SetValueforEntity = 1,
        RequestingData = 2,
        ProcessDataBeforeUsing = 3,
        PushDataIntoDataGridview = 4,
        PushDataIntoReport = 5,
        WritetoFile = 6
    } ;

    public enum CodeGeneratorStatus
    {
        OpenConnection = 0,
        LoadTables = 1,
        LoadColumns = 2,
        LoadContraints = 3,
        WriteController = 4,
        WriteInfor = 5
    } ;
    public enum KyQuiHoanQui
    {
        KyQui = 0,
        HoanQui = 1,
        HuyKyQui = 2,
        HuyHoanKyQui = 3
    } ;

    //----------------------------------------------------------------------------
    //-----------------------------NEW CLASS--------------------------------------
    //----------------------------------------------------------------------------
    ///<summary>
    ///<para>Lớp Utility chứa các hàm dùng chung dùng cho việc lập trình</para>
    ///</summary>
    public class Utility
    {
      
        public static void OpenHelpFile(string helpfile)
        {
            try
            {
                 System.Diagnostics.Process.Start(helpfile);
            }
            catch (Exception ex)
            {
                
            }
        }
        public static decimal getSUM(DataTable dt, string fieldName)
        {
            try
            {
                return DecimaltoDbnull(dt.Compute(String.Format("sum({0})", fieldName), "1=1"), 0m);
            }
            catch (Exception ex)
            {

                return 0m;
            }
        }
        public static string GetFirstValueFromFile(string fileName)
        {
            try
            {
                if (!File.Exists(fileName)) return "";
                using (StreamReader _Reader = new StreamReader(fileName))
                {
                    object obj = _Reader.ReadLine();
                    if (obj == null) return "";
                    return obj.ToString().Trim();
                }
                return "";
            }
            catch
            {
                return "";
            }
        }
        
        public static string translateYYYYMMDD2DDMMYYYY(string YYYYMMDD)
        {
            try
            {
                return YYYYMMDD.Substring(6, 2) + "/" + YYYYMMDD.Substring(4, 2) + "/" + YYYYMMDD.Substring(0, 4);
            }
            catch
            {
                return YYYYMMDD;
            }
        }
        public static string getAPPName(VNS.Libs.AppType.AppEnum.AppName _AppName)
        {
            switch (_AppName)
            {
                case VNS.Libs.AppType.AppEnum.AppName.XFILM:
                    return "XFILM WORKSTATION";
                case VNS.Libs.AppType.AppEnum.AppName.XVIEW:
                    return "XVIEW WORKSTATION";
                case VNS.Libs.AppType.AppEnum.AppName.DICOMVIEWER:
                    return "DICOMVIEWER WORKSTATION";
                case VNS.Libs.AppType.AppEnum.AppName.RISLINK:
                    return "RISLINK WORKSTATION";
                //case VNS.Libs.AppType.AppEnum.AppName.WORKLIST:
                //    return "DICOM WORKLIST";
                //case VNS.Libs.AppType.AppEnum.AppName.HL7:
                //    return "HL7";
                case VNS.Libs.AppType.AppEnum.AppName.QLPK:
                    return "QLPK";
                default:
                    return "XFILM WORKSTATION";

            }
            return "XFILM WORKSTATION";
        }
       public static void setEnterEvent(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl.GetType().Equals(typeof(EditBox)))
                {
                    EditBox box = ctrl as EditBox;
                    if (box.Multiline)
                    {
                        box.Enter += _Enter;
                        box.LostFocus += box_LostFocus;
                        //box.KeyDown += box_KeyDown;
                    }
                }
                else if (ctrl.GetType().Equals(typeof(TextBox)))
                {
                    TextBox box = ctrl as TextBox;
                    if (box.Multiline)
                    {
                        box.Enter += _Enter;
                        box.LostFocus += box_LostFocus;
                        //box.KeyDown+=box_KeyDown;
                    }
                }
                else
                    setEnterEvent(ctrl);
            }
        }

       static void box_LostFocus(object sender, EventArgs e)
       {
           if (sender.GetType().Equals(typeof(EditBox)))
           {
               EditBox box = sender as EditBox;
               box.Text = Utility.DoTrim(box.Text);
           }
           if (sender.GetType().Equals(typeof(TextBox)))
           {
               TextBox box = sender as TextBox;
               box.Text = Utility.DoTrim(box.Text);
           }
       }

       static void box_KeyDown(object sender, KeyEventArgs e)
       {
           //if (e.KeyCode == Keys.Enter) e.Handled = false;
       }
       static void _Enter(object sender, EventArgs e)
       {
           if (sender.GetType().Equals(typeof(EditBox)))
           {
               EditBox box = sender as EditBox;
               int position = box.Text.Length;
               box.Select(position, 0);
           }
           if (sender.GetType().Equals(typeof(TextBox)))
           {
               TextBox box = sender as TextBox;
               int position = box.Text.Length;
               box.Select(position, 0);
           }
       }
       public static void SaveValue2Lines(List<string> values, string file)
       {
           try
           {


               using (StreamWriter _Writer = new StreamWriter(file, false))
               {
                   string value = "";
                   foreach (string s in values)
                   {
                       _Writer.WriteLine(s);
                   }
                   _Writer.Flush();
                   _Writer.Close();
               }
           }
           catch
           {
           }
       }
       public static void SetVisualStyle(Control _parent, bool checkShowHide)
       {
           try
           {
               foreach (Control ctr in _parent.Controls)
               {
                   if (ctr is GridEX)
                   {
                       ((GridEX)ctr).VisualStyle = (Janus.Windows.GridEX.VisualStyle)PropertyLib._AppProperties.NormalVisualStyle;
                       //((GridEX)ctr).SelectedFormatStyle.BackColor = System.Drawing.SystemColors.Highlight;
                       ((GridEX)ctr).SelectedFormatStyle.BackColor = Color.SteelBlue;
                       ((GridEX)ctr).AlternatingColors = false;
                        ((GridEX)ctr).FilterRowFormatStyle.BackColor = Color.FromArgb(192, 255, 192);
                        ((GridEX)ctr).BackColor = Color.WhiteSmoke;
                       ((GridEX)ctr).CellSelectionMode = CellSelectionMode.EntireRow;
                       ((GridEX)ctr).FocusCellFormatStyle.BackColor = Color.Snow;
                       ((GridEX)ctr).FocusCellFormatStyle.ForeColor = Color.Black;
                       ((GridEX)ctr).FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
                       ((GridEX)ctr).KeyDown += Utility_KeyDown;
                       ShowHideColumns((GridEX)ctr);
                       if (checkShowHide) ShowHideMoneyColumns((GridEX)ctr);
                       AutoAddContextMenu((GridEX)ctr);
                       //((GridEX)ctr).FocusCellFormatStyle.ForeColor = Color.White;
                   }
                   else if (ctr is CalendarCombo)
                       ((CalendarCombo)ctr).VisualStyle = (Janus.Windows.CalendarCombo.VisualStyle)PropertyLib._AppProperties.NormalVisualStyle;
                   else if (ctr is UIButton)
                       ((UIButton)ctr).VisualStyle = (Janus.Windows.UI.VisualStyle)PropertyLib._AppProperties.Sta_Cbo_CmdVisualStyle;
                   else if (ctr is UIComboBox)
                       ((UIComboBox)ctr).VisualStyle = (Janus.Windows.UI.VisualStyle)PropertyLib._AppProperties.Sta_Cbo_CmdVisualStyle;
                   else if (ctr is UIStatusBar)
                       ((UIStatusBar)ctr).VisualStyle = (Janus.Windows.UI.VisualStyle)PropertyLib._AppProperties.Sta_Cbo_CmdVisualStyle;
                   else if (ctr is UITab)
                       ((UITab)ctr).VisualStyle = TabVisualStyle.VS2005;// (TabVisualStyle)PropertyLib._AppProperties.TabVisualStyle;
                    else if (ctr is MultiColumnCombo)
                       ((MultiColumnCombo)ctr).VisualStyle = (Janus.Windows.GridEX.VisualStyle)PropertyLib._AppProperties.NormalVisualStyle;
                   else if (ctr is EditBox)
                       ((EditBox)ctr).VisualStyle = (Janus.Windows.GridEX.VisualStyle)PropertyLib._AppProperties.NormalVisualStyle;
                   else if (ctr is MaskedEditBox)
                       ((MaskedEditBox)ctr).VisualStyle = (Janus.Windows.GridEX.VisualStyle)PropertyLib._AppProperties.NormalVisualStyle;
                   else if (ctr is UIGroupBox)
                       ((UIGroupBox)ctr).VisualStyle = (Janus.Windows.UI.Dock.PanelVisualStyle)PropertyLib._AppProperties.NormalVisualStyle;
                   else if (ctr is CalendarCombo)
                       ((CalendarCombo)ctr).VisualStyle = (Janus.Windows.CalendarCombo.VisualStyle)PropertyLib._AppProperties.NormalVisualStyle;
                   else if (ctr is UIRadioButton)
                       ((UIRadioButton)ctr).VisualStyle = (Janus.Windows.UI.VisualStyle)PropertyLib._AppProperties.Sta_Cbo_CmdVisualStyle;
                   if (ctr.Controls.Count > 0)
                       SetVisualStyle(ctr);
               }
           }
           catch (Exception ex)
           {
           }
       }
       public static void SetVisualStyle(Control _parent)
        {
            try
            {
                foreach (Control ctr in _parent.Controls)
                {
                    if (ctr is GridEX)
                    {
                        ((GridEX)ctr).VisualStyle = (Janus.Windows.GridEX.VisualStyle)PropertyLib._AppProperties.NormalVisualStyle;
                        //((GridEX)ctr).SelectedFormatStyle.BackColor = System.Drawing.SystemColors.Highlight;
                        ((GridEX)ctr).FilterRowFormatStyle.BackColor = Color.FromArgb(192, 255, 192);
                        ((GridEX)ctr).SelectedFormatStyle.BackColor = Color.SteelBlue;
                        ((GridEX)ctr).AlternatingColors = false;
                        ((GridEX)ctr).BackColor = Color.WhiteSmoke;
                        ((GridEX)ctr).CellSelectionMode = CellSelectionMode.EntireRow;
                        ((GridEX)ctr).FocusCellFormatStyle.BackColor = Color.Snow;
                        ((GridEX)ctr).FocusCellFormatStyle.ForeColor = Color.Black;
                        ((GridEX)ctr).FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
                        ((GridEX)ctr).KeyDown += Utility_KeyDown;
                        ShowHideColumns((GridEX)ctr);
                        ShowHideMoneyColumns((GridEX)ctr);
                        AutoAddContextMenu((GridEX)ctr);
                        //((GridEX)ctr).FocusCellFormatStyle.ForeColor = Color.White;
                    }
                    else if (ctr is CalendarCombo)
                        ((CalendarCombo)ctr).VisualStyle = (Janus.Windows.CalendarCombo.VisualStyle)PropertyLib._AppProperties.NormalVisualStyle;
                    else if (ctr is UIButton)
                        ((UIButton)ctr).VisualStyle = (Janus.Windows.UI.VisualStyle)PropertyLib._AppProperties.Sta_Cbo_CmdVisualStyle;
                    else if (ctr is UIComboBox)
                        ((UIComboBox)ctr).VisualStyle = (Janus.Windows.UI.VisualStyle)PropertyLib._AppProperties.Sta_Cbo_CmdVisualStyle;
                    else if (ctr is UIStatusBar)
                        ((UIStatusBar)ctr).VisualStyle = (Janus.Windows.UI.VisualStyle)PropertyLib._AppProperties.Sta_Cbo_CmdVisualStyle;
                    else if (ctr is UITab)
                        ((UITab)ctr).VisualStyle = TabVisualStyle.Normal;// (TabVisualStyle)PropertyLib._AppProperties.TabVisualStyle;
                    else if (ctr is MultiColumnCombo)
                        ((MultiColumnCombo)ctr).VisualStyle = (Janus.Windows.GridEX.VisualStyle)PropertyLib._AppProperties.NormalVisualStyle;
                    else if (ctr is EditBox)
                        ((EditBox)ctr).VisualStyle = (Janus.Windows.GridEX.VisualStyle)PropertyLib._AppProperties.NormalVisualStyle;
                    else if (ctr is MaskedEditBox)
                        ((MaskedEditBox)ctr).VisualStyle = (Janus.Windows.GridEX.VisualStyle)PropertyLib._AppProperties.NormalVisualStyle;
                    else if (ctr is UIGroupBox)
                        ((UIGroupBox)ctr).VisualStyle = (Janus.Windows.UI.Dock.PanelVisualStyle)PropertyLib._AppProperties.NormalVisualStyle;
                    else if (ctr is CalendarCombo)
                        ((CalendarCombo)ctr).VisualStyle = (Janus.Windows.CalendarCombo.VisualStyle)PropertyLib._AppProperties.NormalVisualStyle;
                    else if (ctr is UIRadioButton)
                        ((UIRadioButton)ctr).VisualStyle = (Janus.Windows.UI.VisualStyle)PropertyLib._AppProperties.Sta_Cbo_CmdVisualStyle;
                    if (ctr.Controls.Count > 0)
                        SetVisualStyle(ctr);
                }
            }
            catch (Exception ex)
            {
            }
        }
        public static void SetVisualStyleBasic(Control _parent)
        {
            try
            {
                foreach (Control ctr in _parent.Controls)
                {
                    if (ctr is GridEX)
                    {
                        ((GridEX)ctr).VisualStyle = (Janus.Windows.GridEX.VisualStyle)PropertyLib._AppProperties.NormalVisualStyle;
                        //((GridEX)ctr).SelectedFormatStyle.BackColor = System.Drawing.SystemColors.Highlight;
                        ((GridEX)ctr).SelectedFormatStyle.BackColor = Color.SteelBlue;
                        ((GridEX)ctr).AlternatingColors = false;
                        ((GridEX)ctr).BackColor = Color.WhiteSmoke;
                        ((GridEX)ctr).CellSelectionMode = CellSelectionMode.EntireRow;
                        ((GridEX)ctr).FocusCellFormatStyle.BackColor = Color.Snow;
                        ((GridEX)ctr).FocusCellFormatStyle.ForeColor = Color.Black;
                        ((GridEX)ctr).FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
                        ((GridEX)ctr).KeyDown += Utility_KeyDown;
                       
                    }
                    else if (ctr is CalendarCombo)
                        ((CalendarCombo)ctr).VisualStyle = (Janus.Windows.CalendarCombo.VisualStyle)PropertyLib._AppProperties.NormalVisualStyle;
                    else if (ctr is UIButton)
                        ((UIButton)ctr).VisualStyle = (Janus.Windows.UI.VisualStyle)PropertyLib._AppProperties.Sta_Cbo_CmdVisualStyle;
                    else if (ctr is UIComboBox)
                        ((UIComboBox)ctr).VisualStyle = (Janus.Windows.UI.VisualStyle)PropertyLib._AppProperties.Sta_Cbo_CmdVisualStyle;
                    else if (ctr is UIStatusBar)
                        ((UIStatusBar)ctr).VisualStyle = (Janus.Windows.UI.VisualStyle)PropertyLib._AppProperties.Sta_Cbo_CmdVisualStyle;
                    else if (ctr is UITab)
                        ((UITab)ctr).VisualStyle = TabVisualStyle.VS2005;// (TabVisualStyle)PropertyLib._AppProperties.TabVisualStyle;
                    else if (ctr is MultiColumnCombo)
                        ((MultiColumnCombo)ctr).VisualStyle = (Janus.Windows.GridEX.VisualStyle)PropertyLib._AppProperties.NormalVisualStyle;
                    else if (ctr is EditBox)
                        ((EditBox)ctr).VisualStyle = (Janus.Windows.GridEX.VisualStyle)PropertyLib._AppProperties.NormalVisualStyle;
                    else if (ctr is MaskedEditBox)
                        ((MaskedEditBox)ctr).VisualStyle = (Janus.Windows.GridEX.VisualStyle)PropertyLib._AppProperties.NormalVisualStyle;
                    else if (ctr is UIGroupBox)
                        ((UIGroupBox)ctr).VisualStyle = (Janus.Windows.UI.Dock.PanelVisualStyle)PropertyLib._AppProperties.NormalVisualStyle;
                    else if (ctr is CalendarCombo)
                        ((CalendarCombo)ctr).VisualStyle = (Janus.Windows.CalendarCombo.VisualStyle)PropertyLib._AppProperties.NormalVisualStyle;
                    else if (ctr is UIRadioButton)
                        ((UIRadioButton)ctr).VisualStyle = (Janus.Windows.UI.VisualStyle)PropertyLib._AppProperties.Sta_Cbo_CmdVisualStyle;
                    if (ctr.Controls.Count > 0)
                        SetVisualStyle(ctr);
                }
            }
            catch (Exception ex)
            {
            }
        }
        static void AutoAddContextMenu(GridEX grd)
       {
           try
           {
               if (grd.Name == "grdNhanvien")
               {
                   string _name = grd.Name;
               }
               ContextMenuStrip ctx = grd.ContextMenuStrip;
                bool isExist = false;
                if (ctx != null)
                {
                    //Utility.ShowMsg("Update");
                    foreach (ToolStripItem _item in ctx.Items)
                    {
                        if (_item.Tag != null && _item.Tag.ToString() == "EXCEL")
                            isExist = true;
                        if (_item.Tag != null && _item.Tag.ToString() == "SHOWHIDE")
                            isExist = true;
                    }
                }
                if (!isExist)
                {
                   // Utility.ShowMsg("Add");
                    ToolStripMenuItem newItem = null;
                    if (ctx == null)
                    {
                        ctx = new ContextMenuStrip();
                    }
                    else
                    {
                        ctx.Items.Add(new ToolStripSeparator());
                    }
                    //Add các item
                    newItem = new ToolStripMenuItem("Xuất dữ liệu trên lưới ra file Excel");
                    newItem.Tag = "EXCEL";
                    newItem.Click += newItem_Click;
                    ctx.Items.Add(newItem);

                    newItem = new ToolStripMenuItem("Cấu hình ẩn hiện các cột theo người dùng");
                    newItem.Tag = "SHOWHIDE";
                    newItem.Click += newItem_Click;
                    ctx.Items.Add(newItem);
                    newItem = new ToolStripMenuItem("AutoFit Column");
                    newItem.Tag = "AFC";
                    newItem.Click += newItem_Click;
                    ctx.Items.Add(newItem);
                }
               grd.ContextMenuStrip = ctx;
           }
           catch (Exception ex)
           {
                Utility.CatchException(ex);
            }
           

       }

       static void newItem_Click(object sender, EventArgs e)
       {
           ToolStripMenuItem selectedItem = sender as ToolStripMenuItem;
           string tabVal = selectedItem.Tag.ToString();
           GridEX grd = ((ContextMenuStrip)((ToolStripItem)sender).Owner).SourceControl as GridEX;
            if (tabVal == "EXCEL")
            {

                Utility.ExportExcel(grd);
            }
            else if (tabVal == "SHOWHIDE")
            {
                frm_GridConfig _GridConfig = new frm_GridConfig(grd);
                _GridConfig._OnAccept += _GridConfig__OnAccept;
                _GridConfig.ShowDialog();
            }
            else if (tabVal == "AFC")
            {
                grd.AutoSizeColumns();
            }
        }
        public static void ShowHideColumns(GridEX grd)
        {
            try
            {
                Form _f = grd.FindForm();
                if (_f != null)
                {
                    //Tìm cấu hình của admin
                    DataTable dtColumnData = new Select().From(SysGridUserConfig.Schema)
                      .Where(SysGridUserConfig.Columns.GridName).IsEqualTo(grd.Name)
                      .And(SysGridUserConfig.Columns.ApplyAll).IsEqualTo(1)
                       .And(SysGridUserConfig.Columns.FormName).IsEqualTo(grd.FindForm().Name)
                       .OrderAsc(SysGridUserConfig.Columns.ColIndex)
                      .ExecuteDataSet().Tables[0];
                    DataTable dtExtraCols = new Select().From(SysGridUserConfig.Schema)
                     .Where(SysGridUserConfig.Columns.GridName).IsEqualTo(grd.Name)
                      .And(SysGridUserConfig.Columns.FormName).IsEqualTo(grd.FindForm().Name)
                      .And(SysGridUserConfig.Columns.Source).IsEqualTo(0)
                      .And(SysGridUserConfig.Columns.ApplyAll).IsEqualTo(1)
                      .OrderAsc(SysGridUserConfig.Columns.ColIndex)
                     .ExecuteDataSet().Tables[0];
                    if (dtColumnData.Rows.Count <= 0)//Lấy theo cấu hình của user
                    {
                        dtColumnData = new Select().From(SysGridUserConfig.Schema)
                         .Where(SysGridUserConfig.Columns.Username).IsEqualTo(globalVariables.UserName)
                         .And(SysGridUserConfig.Columns.GridName).IsEqualTo(grd.Name)
                          .And(SysGridUserConfig.Columns.FormName).IsEqualTo(grd.FindForm().Name)
                          .OrderAsc(SysGridUserConfig.Columns.ColIndex)
                         .ExecuteDataSet().Tables[0];

                        dtExtraCols = new Select().From(SysGridUserConfig.Schema)
                        .Where(SysGridUserConfig.Columns.GridName).IsEqualTo(grd.Name)
                        .And(SysGridUserConfig.Columns.Username).IsEqualTo(globalVariables.UserName)
                         .And(SysGridUserConfig.Columns.FormName).IsEqualTo(grd.FindForm().Name)
                         .And(SysGridUserConfig.Columns.Source).IsEqualTo(0)
                         .OrderAsc(SysGridUserConfig.Columns.ColIndex)
                        .ExecuteDataSet().Tables[0];

                    }
                    
                    //Code tự động filter


                    if (dtColumnData.Rows.Count > 0)
                    {
                        foreach (GridEXColumn col in grd.RootTable.Columns)
                        {
                            DataRow[] arrdr = dtColumnData.Select("col_key='" + col.Key + "' and isVisible=1");
                            if (arrdr.Length > 0)
                            {
                                col.Visible = Convert.ToBoolean(arrdr[0]["isVisible"]);
                                col.Width = Utility.Int32Dbnull(arrdr[0]["width"]);
                                col.Caption = Utility.sDbnull(arrdr[0]["col_header"]);
                                if (Utility.Int32Dbnull(arrdr[0]["col_index"]) <= grd.RootTable.Columns.Count - 1)
                                    col.Position = Utility.Int32Dbnull(arrdr[0]["col_index"]);
                                else
                                    col.Position = grd.RootTable.Columns.Count - 1;
                            }
                        }
                    }
                    List<string> lstDone = new List<string>();
                    if (dtExtraCols.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtExtraCols.Rows)
                        {
                            if (!lstDone.Contains(Utility.sDbnull(dr["col_key"])))
                            {
                                GridEXColumn _newCol = new GridEXColumn();
                                _newCol.Caption = Utility.sDbnull(dr["col_header"]);
                                _newCol.Width = Utility.Int32Dbnull(dr["width"]);
                                _newCol.Key = Utility.sDbnull(dr["col_key"]);
                                _newCol.DataMember = Utility.sDbnull(dr["col_key"]);
                                grd.RootTable.Columns.Add(_newCol);
                                if (Utility.Int32Dbnull(dr["col_index"]) <= grd.RootTable.Columns.Count - 1)
                                    _newCol.Position = Utility.Int32Dbnull(dr["col_index"]);
                                else
                                    _newCol.Position = grd.RootTable.Columns.Count - 1;
                                lstDone.Add(Utility.sDbnull(dr["col_key"]));
                            }
                        }
                    }
                    foreach (GridEXColumn col in grd.RootTable.Columns)
                    {
                        col.FilterEditType = FilterEditType.TextBox;
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
        }
        static void _GridConfig__OnAccept(DataTable dtColData, GridEX grd)
        {
            try
            {
                foreach (GridEXColumn col in grd.RootTable.Columns)
                {
                    DataRow[] arrdr = dtColData.Select("col_key='" + col.Key + "'");
                    if (arrdr.Length > 0)
                    {
                        col.Visible = Convert.ToBoolean(arrdr[0]["isVisible"]);
                        col.Width = Utility.Int32Dbnull(arrdr[0]["width"]);
                        col.Caption = Utility.sDbnull(arrdr[0]["col_header"]);
                        col.Position = Utility.Int32Dbnull(arrdr[0]["col_index"]);
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }
       static void ShowHideMoneyColumns(GridEX grd)
       {
           List<string> lstKey = new List<string>();
           lstKey = Laygiatrithamsohethong("MONEY_COLUMNS", "XYZ", false).ToUpper().Split(',').ToList<string>();
           foreach (GridEXColumn col in grd.RootTable.Columns)
           {
               if (QuyenKhongxemgia("KHONGDUOC_XEM_GIA") && (lstKey.Contains(col.Key.ToUpper()) || lstKey.Contains(col.DataMember.ToUpper())))
               {
                   col.Visible = false;
               }
             
           }
       }
       static void Utility_KeyDown(object sender, KeyEventArgs e)
       {
           if (e.Control && e.KeyCode == Keys.E)
               CollapseGrid((GridEX)sender);
           if (e.Control && e.KeyCode == Keys.C)
               SaochepThongtin((GridEX)sender, true);
           if (e.Alt && e.KeyCode == Keys.C)
               SaochepThongtin((GridEX)sender, false);

       }
       static void SaochepThongtin(GridEX grd, bool copyCurrentCellOrRow)
       {
           try
           {
               if (!Utility.isValidGrid(grd)) return;
               string _value = "";
               if (copyCurrentCellOrRow)
               {
                   _value = Utility.sDbnull(grd.GetValue(grd.CurrentColumn), "");
               }
               else
               {
                   foreach (GridEXColumn col in grd.RootTable.Columns)
                       if (col.Visible)
                           _value += Utility.sDbnull(grd.GetValue(col), "") + ";";
               }
                if (_value == "") _value = "NONE";
               System.Windows.Forms.Clipboard.SetText(string.Format("{0}", _value));
           }
           catch (Exception ex)
           {

               Utility.CatchException(ex);
           }
       }
        private static readonly string[] VietnameseSigns =
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };
        public static string BoDau(string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)

                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }

            return str;
        }
        public static void FreeMemory(CrystalDecisions.CrystalReports.Engine.ReportDocument RptDoc)
        {
            try
            {
                if (RptDoc != null)
                {
                    RptDoc.Close();
                    RptDoc.Dispose();
                    RptDoc = null;
                    GC.Collect();
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                CleanTemporaryFolders();
            }
        }
        public static NetworkCredential CreateCredentials(string UID,string PWD)
        {
            NetworkCredential credentials = new NetworkCredential(UID, PWD);
            return credentials;
        }
        public static long GetSequence()
        {
            try
            {
                SysSequence _newitem = new SysSequence();
                _newitem.IsNew = true;
                _newitem.Save();
                return _newitem.SeqId;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public static string getTrinhky(string reportName,DateTime NgayinphoiBHYT)
        {
            DataTable dtbNguoiKy = new DataTable();
            string NoidungTK = "";
            try
            {
                SqlQuery sqlQuery = new Select().From(SysTrinhky.Schema).Where(SysTrinhky.Columns.ReportName).IsEqualTo(
                    reportName);
                dtbNguoiKy = sqlQuery.ExecuteDataSet().Tables[0];

                if (dtbNguoiKy.Rows.Count > 0)
                {
                    NoidungTK= sDbnull(dtbNguoiKy.Rows[0][SysTrinhky.Columns.Trinhky], "");
                }
                else
                {
                    NoidungTK= "";
                }
                NoidungTK = NoidungTK.Replace("&DIADIEM", GetRtfUnicodeEscapedString(Laygiatrithamsohethong("DIA_DIEM", "Địa điểm", false)));
                NoidungTK = NoidungTK.Replace("&NHANVIEN", globalVariables.gv_strTenNhanvien);
                NoidungTK = NoidungTK.Replace("&NGAYIN", GetRtfUnicodeEscapedString(FormatDateTimeWithLocation(DateTime.Now, globalVariables.gv_strDiadiem)));
                NoidungTK = NoidungTK.Replace("&NGUOIIN", GetRtfUnicodeEscapedString(globalVariables.gv_strTenNhanvien));
                NoidungTK = NoidungTK.Replace("&NGAY", Strings.Right("0" + NgayinphoiBHYT.Day.ToString(), 2)).Replace("&THANG", Strings.Right("0" + NgayinphoiBHYT.Month.ToString(), 2)).Replace("&NAM", NgayinphoiBHYT.Year.ToString());
                return NoidungTK;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static Dotuoi Laydotuoi(int Tuoi)
        {
            string treSosinh = Laygiatrithamsohethong("DOTUOI_TRESOSINH","0-1",false);
            string treEm = Laygiatrithamsohethong("DOTUOI_TREEM", "2-17", false);
            string Nguoilon = Laygiatrithamsohethong("DOTUOI_NGUOILON", "18-500", false);
            int min =Utility.Int32Dbnull( treSosinh.Split('-')[0]);
            int max = Utility.Int32Dbnull(treSosinh.Split('-')[1]);
            if (Tuoi >= min && Tuoi <= max) return Dotuoi.TreSosinh;
            min = Utility.Int32Dbnull(treEm.Split('-')[0]);
            max = Utility.Int32Dbnull(treEm.Split('-')[1]);
            if (Tuoi >= min && Tuoi <= max) return Dotuoi.TreEm;
            return Dotuoi.Nguoilon;
        }
        public static string LaythongtinTrangthaiCLS(byte trang_thai)
        {
            string status = "Không xác định";
            switch (trang_thai)
            {
                case 0:
                    status = "Mới chỉ định";
                    break;
                case 1:
                    status = "Đã chuyển CLS";
                    break;
                case 2:
                    status = "Đang thực hiện";
                    break;
                case 3:
                    status = "Đã có kết quả CLS";
                    break;
                case 4:
                    status = "Đã xác nhận kết quả";
                    break;
                default:
                    status = "Không xác định";
                    break;
            }
            return status;
        }
        /// <summary>
        ///  CASE WHEN @LoaiPhieu=1 THEN N'PNK'--loại phiếu nhập kho thuốc
                             //WHEN @LoaiPhieu=2 THEN N'PXK'--loại phiếu xuất kho thuốc
                             //WHEN @LoaiPhieu=3 THEN N'PXKBN'--loại phiếu xuất kho bệnh nhân
                             //WHEN @LoaiPhieu=4 THEN N'PNTKLC'--loại phiếu trả lại cho kho lẻ về kho chẵn
                             //WHEN @LoaiPhieu=5 THEN N'PNTKL'--loại phiếu trả lại kho lẻ
                             //WHEN @LoaiPhieu=6 THEN N'PXKHOA'--loại phiếu xuất khoa						   
                             //WHEN @LoaiPhieu=7 THEN N'PHUY'--Phiếu hủy thuốc
                             //WHEN @LoaiPhieu=8 THEN N'PTNCC'--Phiếu trả nhà cung cấp
                             //WHEN @LoaiPhieu=9 THEN N'PXKTL'--Phiếu xuất kho lẻ trả kho chẵn
                             //WHEN @LoaiPhieu=10 THEN N'PTL'--Phiếu thanh lý
                             // WHEN @LoaiPhieu=15 THEN N'PCN'--Phiếu xuất hao phí
        /// </summary>
        /// <param name="loaiphieu"></param>
        /// <param name="khoxuat"></param>
        /// <param name="khonhap"></param>
        /// <param name="khoalinh"></param>
        /// <param name="ma_luotkham"></param>
        /// <returns></returns>
        public static string Laythongtinbiendongthuoc(byte loaiphieu,string khoxuat,string khonhap,string khoalinh,string ma_luotkham,string ten_nhacungcap)
        {
            string status = "Không xác định";
            switch (loaiphieu)
            {
                case 1:
                    status = string.Format("Nhập thuốc từ nhà cung cấp: {0}", ten_nhacungcap);
                    break;
                case 2:
                    status = string.Format("Xuất thuốc từ kho: {0} về kho {1}",khoxuat, khonhap);
                    break;
                case 3:
                    status = string.Format("Xuất thuốc bệnh nhân: {0}", ma_luotkham);
                    break;
                case 4:
                    status = string.Format("Trả thuốc về kho: {0}", khonhap);
                    break;
                case 5:
                    status = string.Format("Trả lại về kho: {0}", khonhap);
                    break;
                case 6:
                    status = string.Format("xuất khoa: {0} - {1}",khoalinh, khonhap );
                    break;
                case 7:
                    status = string.Format("Hủy thuốc");
                    break;
                case 8:
                    status = string.Format("Trả nhà cung cấp");
                    break;
                case 9:
                    status = string.Format("Trả thuốc về kho: {0}", khonhap);
                    break;
                case 10:
                    status = string.Format("Thanh lý thuốc");
                    break;
                case 11:
                    status = string.Format("Trả thuốc thừa");
                    break;
                case 12:
                    status = string.Format("Trả thuốc về kho: {0}", khonhap);
                    break;
                case 13:
                    status = string.Format("Xuất thuốccho kho: {0}", khonhap);
                    break;
                case 14:
                    status = string.Format("Xuất thuốc/vt xã huyện");
                    break;
                case 15:
                    status = string.Format("Xuất thuốc cho khoa: {0}", khoalinh); 
                    break;
                case 19:
                    status = string.Format("Xuất hao phí nội trú từ kho: {0}", khoxuat);
                    break;
                default:
                    status = "Không xác định";
                    break;
            }
            return status;
        }

        public static string Laythongtintrangthainguoibenh(KcbLuotkham objluotkham)
        {
            try
            {
               // DataTable dtBG = SPs.NoitruTimkiemlichsuBuonggiuong(objluotkham.MaLuotkham, objluotkham.IdBenhnhan, "-1", -1).GetDataSet().Tables[0];
                string status = "Không xác định";
                if (objluotkham != null)
                {
                    switch (objluotkham.TrangthaiNoitru)
                    {
                        case 0:
                            status = "Người bệnh đang ngoại trú";
                            break;
                        case 1:
                            status = string.Format("{0} {1}", "Người bệnh đã làm thủ tục nhập viện điều trị", (objluotkham.HuongDieutri == "4" ? "ngoại trú" : "nội trú")); ;
                            break;
                        case 2:
                            status = "Người bệnh đã lập phiếu điều trị (y lệnh)";
                            break;
                        case 3:
                            status = "Người bệnh đã làm phiếu ra viện";
                            break;
                        case 4:
                            status = "Người bệnh đã được tổng hợp dữ liệu chờ tài chính xác nhận thanh toán (hoặc chờ thu ngân thanh toán ra viện)";
                            break;
                        case 5:
                            status = "Người bệnh đã được tài chính xác nhận và chuyển thanh toán";
                            break;
                        case 6:
                            status = string.Format("{0} {1}", "Người bệnh đã thanh toán và hoàn tất", objluotkham.HuongDieutri == "4" ? "điều trị ngoại trú" : "việc ra viện");
                            break;
                        default:
                            status = "Không xác định";
                            break;
                    }
                }
                return status;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return "Không xác định";
            }
            
        }
        public static bool CoquyenSuperAdmin(string maquyen)
        {
            return globalVariables.isSuperAdmin || (globalVariablesPrivate.objNhanvien != null && globalVariables.gv_dtQuyenNhanvien.Select(QheNhanvienQuyensudung.Columns.Ma + "='" + maquyen + "'").Length > 0);
        }
        public static bool Coquyen(string maquyen)
        {
            return globalVariables.isSuperAdmin ||  globalVariables.IsAdmin || (globalVariablesPrivate.objNhanvien != null && globalVariables.gv_dtQuyenNhanvien.Select(QheNhanvienQuyensudung.Columns.Ma + "='" + maquyen + "'").Length > 0);
        }
        public static bool QuyenKhongxemgia(string maquyen)
        {
            return globalVariablesPrivate.objNhanvien != null && globalVariables.gv_dtQuyenNhanvien.Select(QheNhanvienQuyensudung.Columns.Ma + "='" + maquyen + "'").Length > 0;
        }
        public static void thongbaokhongcoquyen(string maquyen,string noidung)
        {
            Utility.ShowMsg(string.Format("Bạn không có quyền {0}({1}). Vui lòng liên hệ quản trị hệ thống để được phân quyền", noidung, maquyen));
        }
        public static bool CoquyenThemnhanhDmucChung(string LOAI)
        {

            return globalVariables.IsAdmin || (globalVariablesPrivate.objNhanvien != null && globalVariables.gv_dtQuyenNhanvienCapnhatnhanhDmucChung!=null && globalVariables.gv_dtQuyenNhanvienCapnhatnhanhDmucChung.Columns.Count>0 && globalVariables.gv_dtQuyenNhanvienCapnhatnhanhDmucChung.Select(QheNhanvienDmucchung.Columns.Loai + "='" + LOAI + "'").Length > 0);
        }
        public static bool CoquyenTruycapDanhmuc(string id_danhmuc,string loai)
        {
            return globalVariables.IsAdmin ||
                   (globalVariablesPrivate.objNhanvien != null &&
                    globalVariables.gv_dtQuyenNhanvien_Dmuc.Select(QheNhanvienDanhmuc.Columns.IdDichvu + "='" +
                                                                   id_danhmuc + "' AND " +
                                                                   QheNhanvienDanhmuc.Columns.Loai + "='" + loai + "'")
                        .Length > 0);
        }
        public static void LoadProperties()
        {
            PropertyLib._HinhAnhProperties = PropertyLib.GetHinhAnhProperties();
            PropertyLib._PhieuxuatBNProperty = PropertyLib.GetPhieuxuatBNProperties();
            PropertyLib._TrathuocthuaProperties = PropertyLib.GetTrathuocthuaProperties();
            PropertyLib._FTPProperties = PropertyLib.GetFTPProperties();
            PropertyLib._BenhAnProperties = PropertyLib.GetBenhAnProperties();
            PropertyLib._DuocNoitruProperties = PropertyLib.GetDuocNoitruProperties();
            PropertyLib._HisDuocProperties = PropertyLib.GetHisDuocProperties();
            PropertyLib._HISQMSProperties = PropertyLib.GetHISQMSProperties();
            PropertyLib._NoitruProperties = PropertyLib.GetNoitruProperties();

            PropertyLib._QMSPrintProperties = PropertyLib.GetQMSPrintProperties();
            PropertyLib._KCBProperties = PropertyLib.GetKCBProperties();
            PropertyLib._ThamKhamProperties = PropertyLib.GetThamKhamProperties();
            PropertyLib._ThanhtoanProperties = PropertyLib.GetThanhtoanProperties();
            PropertyLib._MayInProperties = PropertyLib.GetMayInProperties();
            PropertyLib._HISCLSProperties = PropertyLib.GetHISCLSProperties();

            PropertyLib._QheGiaCLSProperties = PropertyLib.GetQheGiaCLSProperties();
            PropertyLib._ThuocProperties = PropertyLib.GetThuocProperties();
            PropertyLib._QheGiaThuocProperties = PropertyLib.GetQheGiaThuocProperties();

            PropertyLib._NhapkhoProperties = PropertyLib.GetNhapkhoProperties();
            PropertyLib._ChuyenkhoProperties = PropertyLib.GetChuyenkhoProperties();
            PropertyLib._QuaythuocProperties = PropertyLib.GetQuaythuocProperties();
            PropertyLib._DynamicInputProperties = PropertyLib.GetDynamicInputProperties();

        }
        public static void SaveValue2File(string fileName, List<string> values, bool append)
        {
            try
            {
                using (StreamWriter _Writer = new StreamWriter(fileName, append))
                {
                    string value = "";
                    foreach (string s in values)
                    {
                        _Writer.WriteLine(s);
                    }
                    _Writer.Flush();
                    _Writer.Close();
                }
            }
            catch
            {
            }
        }
        public static void SaveValue2File(string fileName, List<string> values)
        {
            try
            {
                using (StreamWriter _Writer = new StreamWriter(fileName))
                {
                    string value = "";
                    foreach (string s in values)
                    {
                        value += s + ",";
                    }
                    if (value.Length > 0) value = value.Substring(0, value.Length - 1);
                    _Writer.WriteLine(value);
                    _Writer.Flush();
                    _Writer.Close();
                }
            }
            catch
            {
            }
        }
        public static void SaveValue2Lines(string fileName, List<string> values)
        {
            try
            {
                using (StreamWriter _Writer = new StreamWriter(fileName))
                {
                    string value = "";
                    foreach (string s in values)
                    {
                        _Writer.WriteLine(s);
                    }
                    _Writer.Flush();
                    _Writer.Close();
                }
            }
            catch
            {
            }
        }
        public static List<string> GetAllLinesFromFile(string fileName)
        {
            List<string> lstReval = new List<string>();
            try
            {
                if (!File.Exists(fileName)) return lstReval;
                using (StreamReader _Reader = new StreamReader(fileName))
                {
                    while (_Reader.Peek() > -1)
                    {
                        string obj = _Reader.ReadLine();
                        if (Utility.sDbnull(obj.Trim(), "") != "")
                            lstReval.Add(obj);
                    }

                }
                return lstReval;
            }
            catch
            {
                return lstReval;
            }
        }
        public static void SaveValue2File(string fileName, string value)
        {
            try
            {
                using (StreamWriter _Writer = new StreamWriter(fileName))
                {
                    _Writer.Write(value);
                    _Writer.Flush();
                    _Writer.Close();
                }
            }
            catch
            {
            }
        }
       
        public static string Try2DelFile(string file)
        {
            try
            {
                if (File.Exists(file)) File.Delete(file);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static void AutoCompeleteAddress(DataTable dtData)
        {
            try
            {
                if (globalVariables.dtAutocompleteAddress == null || globalVariables.dtAutocompleteAddress.Columns.Count <= 0)
                {
                    globalVariables.dtAutocompleteAddress = new DataTable();
                    globalVariables.dtAutocompleteAddress.Columns.AddRange(new[]
                                          {
                                               new DataColumn("MaTinhTP", typeof (string)),
                                                new DataColumn("MaQH", typeof (string)),
                                                 new DataColumn("MaXaPhuong", typeof (string)),
                                              new DataColumn("ShortCutXP", typeof (string)),
                                              new DataColumn("ShortCutQH", typeof (string)),
                                              new DataColumn("ShortCutTP", typeof (string)),
                                              new DataColumn("Value", typeof (string)),
                                              new DataColumn("ComparedValue", typeof (string))
                                          });
                }
                if (globalVariables.dtAutocompleteAddress == null || globalVariables.dtAutocompleteAddress.Rows.Count>0) return;//Đã được nạp lần login đầu tiên
                if (!globalVariables.dtAutocompleteAddress.Columns.Contains("ShortCut")) globalVariables.dtAutocompleteAddress.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in dtData.Select("loai_diachinh=0"))
                {
                    //DataRow drShortcut = globalVariables.dtAutocompleteAddress.NewRow();
                    string _Value = "";
                    string _ComparedValue = "";
                    string realName = "";
                    
                    DataRow[] arrQuanHuyen =
                        dtData.Select("ma_cha='" + Utility.sDbnull(dr[DmucDiachinh.Columns.MaDiachinh], "") + "'");
                    foreach (DataRow drQH in arrQuanHuyen)
                    {
                       
                        DataRow[] arrXaPhuong =
                            dtData.Select("ma_cha='" + Utility.sDbnull(drQH[DmucDiachinh.Columns.MaDiachinh], "") + "'");
                        foreach (DataRow drXP in arrXaPhuong)
                        {
                           DataRow   drShortcut = globalVariables.dtAutocompleteAddress.NewRow();
                            drShortcut["MaXaPhuong"] = drXP["ma_diachinh"];
                            drShortcut["MaTinhTP"] = dr["ma_diachinh"];
                            drShortcut["MaQH"] = drQH["ma_diachinh"];
                            
                            realName = "";
                            _Value = drXP[DmucDiachinh.Columns.TenDiachinh] + ", ";
                            _ComparedValue = drXP[DmucDiachinh.Columns.TenDiachinh] + ", ";
                            _ComparedValue += Utility.Bodau(Utility.sDbnull(drXP[DmucDiachinh.Columns.TenDiachinh], "")) + ", ";
                            drShortcut["ShortCutXP"] = drXP["mota_them"].ToString().Trim();

                            #region addShortcut

                            _Value += drQH[DmucDiachinh.Columns.TenDiachinh] + ", ";
                            _ComparedValue = drQH[DmucDiachinh.Columns.TenDiachinh] + ", ";
                            _ComparedValue += Utility.Bodau(Utility.sDbnull(drQH[DmucDiachinh.Columns.TenDiachinh], "")) + ", ";

                            drShortcut["ShortCutQH"] = drQH["mota_them"].ToString().Trim();

                            _Value += dr[DmucDiachinh.Columns.TenDiachinh].ToString();
                            _ComparedValue = dr[DmucDiachinh.Columns.TenDiachinh] + ", ";
                            _ComparedValue += Utility.Bodau(Utility.sDbnull(dr[DmucDiachinh.Columns.TenDiachinh], "")) + ", ";
                            drShortcut["ShortCutTP"] = dr["mota_them"].ToString().Trim();
                            //Ghép chuỗi

                            drShortcut["ComparedValue"] = _ComparedValue.Replace("Không Xác định,","");
                            drShortcut["Value"] = _Value.Replace("Không Xác định,", "");
                            globalVariables.dtAutocompleteAddress.Rows.Add(drShortcut);

                            #endregion
                        }

                        if (arrXaPhuong.Length <= 0)//For phía trên ko xảy ra
                        {
                            #region addShortcut

                            DataRow drShortcut = globalVariables.dtAutocompleteAddress.NewRow();
                            drShortcut["MaXaPhuong"] = "";
                            drShortcut["MaTinhTP"] = dr["ma_diachinh"];
                            drShortcut["MaQH"] = drQH["ma_diachinh"];
                            realName = "";
                            drShortcut["ShortCutXP"] = "kx";
                            _Value = drQH[DmucDiachinh.Columns.TenDiachinh] + ", ";
                            _ComparedValue = drQH[DmucDiachinh.Columns.TenDiachinh] + ", ";
                            _ComparedValue += Utility.Bodau(Utility.sDbnull(drQH[DmucDiachinh.Columns.TenDiachinh], "")) + ", ";
                            drShortcut["ShortCutQH"] = drQH["mota_them"].ToString().Trim();

                            _Value += dr[DmucDiachinh.Columns.TenDiachinh].ToString();
                            _ComparedValue = dr[DmucDiachinh.Columns.TenDiachinh] + ", ";
                            _ComparedValue += Utility.Bodau(Utility.sDbnull(dr[DmucDiachinh.Columns.TenDiachinh], "")) + ", ";
                            drShortcut["ShortCutTP"] = dr["mota_them"].ToString().Trim();

                            drShortcut["ComparedValue"] = _ComparedValue.Replace("Không Xác định,", "");
                            drShortcut["Value"] = _Value.Replace("Không Xác định,", "");
                            globalVariables.dtAutocompleteAddress.Rows.Add(drShortcut);

                            #endregion
                        }
                    }
                    if (arrQuanHuyen.Length <= 0)//Không có quận huyện
                    {
                        #region addShortcut
                        DataRow drShortcut = globalVariables.dtAutocompleteAddress.NewRow();
                        drShortcut["MaXaPhuong"] = "";
                        drShortcut["MaTinhTP"] = dr["ma_diachinh"];
                        drShortcut["MaQH"] = "";
                        
                        realName = "";
                        drShortcut["ShortCutXP"] = "kx";
                        drShortcut["ShortCutQH"] = "kx";
                        drShortcut["ShortCutTP"] = dr["mota_them"].ToString().Trim();
                        _Value = dr[DmucDiachinh.Columns.TenDiachinh].ToString();
                        _ComparedValue = dr[DmucDiachinh.Columns.TenDiachinh] + ", ";

                        drShortcut["ComparedValue"] = _ComparedValue.Replace("Không Xác định,", "");
                        drShortcut["Value"] = _Value.Replace("Không Xác định,", "");
                        globalVariables.dtAutocompleteAddress.Rows.Add(drShortcut);

                        #endregion
                    }
                }
            }
            catch
            {
            }
            finally
            {
                var source = new List<string>();
                var query = from p in globalVariables.dtAutocompleteAddress.AsEnumerable()
                            select
                                p.Field<string>("ShortCutTP").ToString() + "#" +
                                p.Field<string>("ShortCutQH").ToString() + "#" +
                                p.Field<string>("ShortCutXP").ToString() + "@" + p.Field<string>("Value").ToString() +
                                "@" + p.Field<string>("Value").ToString();
                source = query.ToList();
                globalVariables.LstAutocompleteAddressSource = source;
            }
        }
       
        public static string GetStrValuefromDataRow(DataRow dr, string fieldName, string defaultVal)
        {
            try
            {
                if (dr == null || dr.Table == null || dr.Table.Columns.Count <= 0) return defaultVal;
                return Utility.sDbnull(dr[fieldName], defaultVal);
            }
            catch
            {
                return "EX." + defaultVal;
            }
        }
        
        public static string GetStrValuefromDataRow(DataRow dr, string fieldName)
        {
            string defaultVal = "NULL";
            try
            {
                if (dr == null || dr.Table == null || dr.Table.Columns.Count <= 0) return defaultVal;
                return Utility.sDbnull(dr[fieldName], defaultVal);
            }
            catch
            {
                return "EX." + defaultVal;
            }
        }

        public static void OpenProcess(string appName)
        {
            try
            {
                KillProcess(appName);
                System.Diagnostics.Process.Start(appName);
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public static void KillProcess(string appName)
        {
            try
            {
                appName = Path.GetFileNameWithoutExtension(appName);
                System.Diagnostics.Process[] arrProcess = System.Diagnostics.Process.GetProcessesByName(appName);
                if (arrProcess.Length > 0) arrProcess[0].Kill();
            }
            catch
            {
            }
        }

        public static bool ExistsProcess(string processName)
        {
            try
            {
                System.Diagnostics.Process[] arrProcess = System.Diagnostics.Process.GetProcessesByName(processName);
                return arrProcess.Length > 0;
            }
            catch
            {
                return false;
            }
        }
        public static DataSet ExecuteSql(string sql,CommandType cmdtype)
        {
            try
            {
                QueryCommand cmd = new QueryCommand(sql);
                cmd.CommandType = cmdtype;
                return DataService.GetDataSet(cmd);
            }
            catch (Exception ex)
            {
                return new DataSet();
            }
        }
        public static int ExecuteNonQuery(string sql, CommandType cmdtype)
        {
            try
            {
                QueryCommand cmd = new QueryCommand(sql);
                cmd.CommandType = cmdtype;
                return DataService.ExecuteQuery(cmd);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public static void MergeFieldsCheckBox2Doc(Aspose.Words.DocumentBuilder builder, Dictionary<string, string> dicMF , List<string> lstcheckboxfields, DataRow drData)
        {
            try
            {
                if (dicMF != null && dicMF.Count > 0)
                {
                    foreach (string field in dicMF.Keys)
                    {
                        if (builder.MoveToMergeField(field))
                        {
                            if (dicMF[field] == "1")
                            {
                                builder.Font.Name = "Wingdings 2";
                                builder.Write(char.ConvertFromUtf32(82));
                                builder.Font.ClearFormatting();
                            }
                            else
                            {
                                builder.Font.Name = "Wingdings 2";
                                builder.Write(char.ConvertFromUtf32(163));
                                builder.Font.ClearFormatting();
                            }
                        }
                    }
                }
                if (lstcheckboxfields != null && lstcheckboxfields.Count > 0)
                {
                    foreach (string field in lstcheckboxfields)
                    {
                        if (builder.MoveToMergeField(field))
                        {
                            if (Byte2Bool(drData[field]))
                            {
                                builder.Font.Name = "Wingdings 2";
                                builder.Write(char.ConvertFromUtf32(82));
                                builder.Font.ClearFormatting();
                                // builder.InsertCheckBox(field, true, 10);
                            }
                            else
                            {
                                builder.Font.Name = "Wingdings 2";
                                builder.Write(char.ConvertFromUtf32(163));
                                builder.Font.ClearFormatting();
                                ///builder.InsertCheckBox(field, false, 10);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        public static byte[]  GetQRCode(string sInput)
        {
            try
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(sInput, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);
                byte[] QRCode = fromimagetobyte(qrCodeImage);
                return QRCode;
            }
            catch (Exception ex)
            {
                return null;
                Utility.CatchException(ex);
            }
          
        }
        static byte[] fromimagetobyte(Bitmap img)
        {
            try
            {
                using (var stream = new MemoryStream())
                {
                    img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    return stream.ToArray();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static void CatchException(Exception ex)
        {
            //if (globalVariables.IsAdmin)
            //{
                Utility.ShowMsg(ex.ToString());
            //}
            //else
            //{
            //    if (Utility.AcceptQuestion("Có lỗi trong quá trình thực hiện. Bạn có muốn xem nội dung lỗi không(Chú ý: Nội dung có thể chứa tiếng Anh. Trong trường hợp không hiểu vui lòng liên hệ bộ phận IT để được trợ giúp)", "Thông báo lỗi ngoại lệ", true))
            //        Utility.ShowMsg(ex.ToString());
            //}
        }
        public static void CatchException(string AssName,string FormName, Exception ex)
        {
            if (globalVariables.IsAdmin)
            {
                Utility.ShowMsg(ex.ToString());
            }
            else
            {
                if (Utility.AcceptQuestion("Có lỗi trong quá trình thực hiện. Bạn có muốn xem nội dung lỗi không(Chú ý: Nội dung có thể chứa tiếng Anh. Trong trường hợp không hiểu vui lòng liên hệ bộ phận IT để được trợ giúp)", "Thông báo lỗi ngoại lệ", true))
                    Utility.ShowMsg(ex.ToString());
            }
        }
        public static void CatchException(string warning, Exception ex)
        {
            if (globalVariables.IsAdmin)
            {
                Utility.ShowMsg(warning + "\n" + ex.ToString());
            }
            else
            {
                if (Utility.DoTrim(warning) != "")
                    Utility.ShowMsg(warning);
            }
        }
        public static string getTenloaiDiachinh(int _type)
        {
            return _type == 0 ? "Tỉnh/Thành phố" : (_type == 1 ? "Quận/huyện" : "Xã/Phường");
        }

        /// <summary>
        /// hàm thực hêện việc tính tổng từ Table với trường đưa vào
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public static string SumOfTotal(DataTable dataTable, string FieldName)
        {
            // return Utility.sDbnull(dataTable.Compute(string.Format("SUM({0})",FieldName), "1=1"));
            var TinhTong = (from x in dataTable.AsEnumerable()
                            // let y = x.Field<>()
                            select x.Field<decimal>(FieldName)).Sum();
            return Utility.sDbnull(TinhTong);

        }
        public static void EnableBHYT(Form f)
        {
            try
            {
                if (Laygiatrithamsohethong("BHYT", "0", true) == "1") return;
                foreach (Control ctr in f.Controls)
                    if (ctr.Tag != null && ctr.Tag.ToString() == "BHYT")
                        ctr.Enabled = false;
                    else
                        EnableBHYT(ctr);
            }
            catch (Exception)
            {

            }
        }
        public static void EnableBHYT(Control _parent)
        {
            try
            {
                foreach (Control ctr in _parent.Controls)
                    if (ctr.Tag != null && ctr.Tag.ToString() == "BHYT")
                        ctr.Enabled = false;
                    else
                        EnableBHYT(ctr);
            }
            catch (Exception)
            {

            }
        }

        public static string DoTrim(string value)
        {
            return value.TrimStart().TrimEnd();
        }
        public static object getCellValuefromGridEXRow(GridEXRow _row, string FieldName)
        {
            try
            {
               return _row.Cells[FieldName].Value;
            }
            catch
            {
                return null;
            }

        }
        public static object getValueOfGridCell(GridEX grd, string FieldName)
        {
            try
            {
                if (!isValidGrid(grd)) return null;
                else
                {

                    DataRow dr = ((DataRowView)grd.CurrentRow.DataRow).Row;
                    return sDbnull(dr[FieldName], "");
                }
                return null;
            }
            catch
            {
                return null;
            }

        }
        public static DataRow getCurrentDataRow(GridEX grd)
        {
            try
            {
                if (!isValidGrid(grd)) return null;
                else
                {

                    DataRow dr = ((DataRowView)grd.CurrentRow.DataRow).Row;
                    return dr;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public static DataRow getCurrentDataRow(GridEXRow _row)
        {
            try
            {
                DataRow dr = ((DataRowView)_row.DataRow).Row;
                return dr;
            }
            catch
            {
                return null;
            }
        }
        public static bool isValidDatatable(DataTable dtData, bool checkColumns)
        {
            try
            {
                if (dtData != null && (!checkColumns || dtData.Columns.Count > 0)) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public static bool isValidGrid(GridEX grd)
        {
            try
            {
                //230517 Tạm Rem bỏ mục grd.SelectedItems != null 
               // if (grd != null && grd.RowCount > 0 && grd.SelectedItems != null && grd.CurrentRow != null && grd.CurrentRow.RowType == RowType.Record) return true;
                if (grd != null && grd.RowCount > 0  && grd.CurrentRow != null && grd.CurrentRow.RowType == RowType.Record) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public static bool isValidCheckedGrid(GridEX grd)
        {
            try
            {
                if (grd != null && grd.RowCount > 0 && grd.GetCheckedRows().Length>0) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// hàm thực hiện việc 
        /// </summary>
        public static void CleanTemporaryFolders()
        {
            try
            {
                String tempFolder = Environment.ExpandEnvironmentVariables("%TEMP%");
                String tempFolder1 = Path.GetTempPath();
                // String recent = Environment.ExpandEnvironmentVariables("%USERPROFILE%") + "\\Recent";
                // String prefetch = Environment.ExpandEnvironmentVariables("%SYSTEMROOT%") + "\\Prefetch";
                EmptyFolderContents(tempFolder);
                EmptyFolderContents(tempFolder1);
                //  EmptyFolderContents(recent);
                //  EmptyFolderContents(prefetch);
            }
            catch (Exception)
            {

            }

        }
        /// <summary>
        /// hàm thực hiện rỗng thư mục của dữ liệu trong Folder
        /// 
        /// </summary>
        /// <param name="folderName"></param>
        private static void EmptyFolderContents(string folderName)
        {
            foreach (var folder in Directory.GetDirectories(folderName))
            {
                try
                {
                    Directory.Delete(folder, true);
                }
                catch (Exception excep)
                {
                    System.Diagnostics.Debug.WriteLine(excep);
                }
            }
            foreach (var file in Directory.GetFiles(folderName))
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception excep)
                {
                    System.Diagnostics.Debug.WriteLine(excep);
                }
            }
        }

        public static void CreateFolder(string fileName)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(fileName))) Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            }
            catch
            {
            }
        }
        public static bool IsBetweenManyTimeranges(DateTime t, string timerange)
        {
            string[] arrTimeRange = timerange.Split(';');
            bool _isValid = true;
            foreach (string s in arrTimeRange)
            {
                _isValid = IsBetweenTimerange(t, s);
                if (_isValid) return true;
            }
            return false;
        }
        public static bool IsBetweenTimerange(DateTime t, string timerange)
        {
            DateTime start = Convert.ToDateTime(timerange.Split('-')[0]);
            DateTime end = Convert.ToDateTime(timerange.Split('-')[1]);
            var time_of_day = t.TimeOfDay;
            var start_time_of_day = start.TimeOfDay;
            var end_time_of_day = end.TimeOfDay;
            if (start_time_of_day <= end_time_of_day)
                return start_time_of_day <= time_of_day && time_of_day <= end_time_of_day;
            return start_time_of_day <= time_of_day || time_of_day <= end_time_of_day;
        }
        public static bool TimeOfDayIsBetween(DateTime t, DateTime start, DateTime end)
        {
            var time_of_day = t.TimeOfDay;
            var start_time_of_day = start.TimeOfDay;
            var end_time_of_day = end.TimeOfDay;

            if (start_time_of_day <= end_time_of_day)
                return start_time_of_day <= time_of_day && time_of_day <= end_time_of_day;

            return start_time_of_day <= time_of_day || time_of_day <= end_time_of_day;

        }
        public static string TenLoaiPhieu(LoaiPhieu _loaiphieu)
        {
            if (_loaiphieu == LoaiPhieu.PhieuXuatKhoLeTraKhoChan) return "PXKTL";
            else if (_loaiphieu == LoaiPhieu.PhieuNhapKho) return "PNK";
            else if (_loaiphieu == LoaiPhieu.PhieuXuatKhoa) return "PXKHOA";
            else if (_loaiphieu == LoaiPhieu.PhieuXuatKho) return "PXK";
            else if (_loaiphieu == LoaiPhieu.PhieuXuatKhoBenhNhan) return "PXKBN";
            else if (_loaiphieu == LoaiPhieu.PhieuNhapTraLaiKhoLeVeKhoChan) return "PNT";
            else if (_loaiphieu == LoaiPhieu.PhieuNhapTraKhoLe) return "PNTKL";
            else if (_loaiphieu == LoaiPhieu.PhieuXuatTraKhoLe) return "PXTKL";
            else if (_loaiphieu == LoaiPhieu.PhieuHuy) return "PHUY";
            else if (_loaiphieu == LoaiPhieu.PhieuXuatkhoTuyenXaHuyen) return "PXKTH";
            else if (_loaiphieu == LoaiPhieu.PhieuThanhly) return "PTL";
            else if (_loaiphieu == LoaiPhieu.PhieuTraNcc) return "PTNCC";
            else if (_loaiphieu == LoaiPhieu.Phieutrathuocthua) return "PTraTThua";
            else if (_loaiphieu == LoaiPhieu.PhieuXuatKhoaPhongchucnang) return "PXKHOAPHONGCHUCNANG";
            else if (_loaiphieu == LoaiPhieu.PhieuTralaithuocTaiQuay) return "PTTTQ";
            else if (_loaiphieu == LoaiPhieu.PhieuXuatHaophi) return "PXHP";
            else if (_loaiphieu == LoaiPhieu.PhieuXuatNgoai) return "PX_DACBIET";
            return "PNK";
        }
        /// <summary>
        /// hàm thực hiêện iệc ẩn hiện cột trong janus
        /// </summary>
        /// <param name="gridEx"></param>
        /// <param name="colName"></param>
        /// <param name="b_ColVisiable"></param>
        public static void VisiableGridEx(Janus.Windows.GridEX.GridEX gridEx, string colName, bool b_ColVisiable)
        {
            if (gridEx.RootTable.Columns.Contains(colName))
            {
                gridEx.RootTable.Columns[colName].Visible = b_ColVisiable;
            }
        }
        /// <summary>
        /// hàm thực hiện việc cell trong form bị cho phép sửa hoặc không sửa thông tin 
        /// 
        /// </summary>
        /// <param name="gridEx"></param>
        /// <param name="colName"></param>
        /// <param name="b_ColVisiable"></param>
        public static void EnableCellGridEx(Janus.Windows.GridEX.GridEX gridEx, string colName, bool b_ColVisiable)
        {
            if (gridEx.RootTable.Columns.Contains(colName))
            {
                gridEx.RootTable.Columns[colName].Selectable = b_ColVisiable;
            }
        }
        public static bool isPrintPreview(string printerName, bool _PrintPreview)
        {
            if (_PrintPreview) return true;
            if (Utility.DoTrim(printerName) == "") return true;
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                string _name = PrinterSettings.InstalledPrinters[i];
                if (Utility.DoTrim(_name).ToUpper() == Utility.DoTrim(printerName).ToUpper())
                    return false;
            }
            return true;
        }
        private delegate void SetControlPropertyDelegate(Control control, string propertyName, object propertyValue);
        public static void SetControlProperty(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyDelegate(SetControlProperty), new object[] {
				control,
				propertyName,
				propertyValue
			});
            }
            else
            {
                control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
            }
        }
        /// <summary>
        /// hàm thực hiện việc thêm cột thông tin vào datatable 
        /// </summary>
        /// <param name="dataTable">Nguồn vào Datatable</param>
        /// <param name="drv">Dòng cần insert</param>
        /// <param name="filedName">Giá trị tên cột</param>
        /// <param name="value">giá trị cần truyền</param>
        public static void AddDataColInDataTable(ref DataTable dataTable, DataRow drv, string filedName, object value)
        {
            if (dataTable.Columns.Contains(filedName))
            {
                drv[filedName] = value;
            }

        }

        /// Delegate to invoke a specific method on the control thread-safely.
        /// </summary>
        /// <param name="control">Control on which to invoke the method</param>
        /// <param name="methodName">Method to be invoked</param>
        /// <param name="paramValues">Method parameters</param>
        /// <returns>Value returned by the invoked method</returns>
        private delegate object MethodInvoker(Control control, string methodName, object[] paramValues);

        /// <summary>
        /// Delegate to get a property value on the control thread-safely.
        /// </summary>
        /// <param name="control">Control on which to GET the property value</param>
        /// <param name="propertyName">Property name</param>
        /// <return>Property value</return>
        private delegate object PropertyGetInvoker(Control control, string propertyName);

        /// <summary>
        /// Delegate to set a property value on the control thread-safely.
        /// </summary>
        /// <param name="control">Control on which to SET the property value</param>
        /// <param name="propertyName">Property name</param>
        /// <param name="value">New property value</param>
        private delegate void PropertySetInvoker(Control control, string propertyName, object value);

        /// <summary>
        /// Invoke a specific method on the control thread-safely.
        /// </summary>
        /// <param name="control">Control on which to invoke the method</param>
        /// <param name="methodName">Method to be invoked</param>
        /// <param name="paramValues">Method parameters</param>
        /// <return>Value returned by the invoked method</return>
        public static object InvokeMethod(Control control, string methodName, params object[] paramValues)
        {
            if (control != null && !string.IsNullOrEmpty(methodName))
            {
                if (control.InvokeRequired)
                {
                    return control.Invoke(new MethodInvoker(InvokeMethod), control, methodName, paramValues);
                }
                else
                {
                    MethodInfo methodInfo = null;

                    if (paramValues != null && paramValues.Length > 0)
                    {
                        Type[] types = new Type[paramValues.Length - 1];
                        for (int i = 0; i <= paramValues.Length - 1; i++)
                        {
                            if (paramValues[i] != null)
                            {
                                types[i] = paramValues[i].GetType();
                            }
                        }
                        methodInfo = control.GetType().GetMethod(methodName, types);
                    }
                    else
                    {
                        methodInfo = control.GetType().GetMethod(methodName);
                    }

                    if (methodInfo != null)
                    {
                        return methodInfo.Invoke(control, paramValues);
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Get a PropertyInfo object associated with a specific property on the control.
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="propertyName">Property name</param>
        /// <return>A PropertyInfo object associated with 
        /// 'propertyName' on specified 'control'</return>
        private static PropertyInfo GetProperty(Control control, string propertyName)
        {
            if (control != null && !string.IsNullOrEmpty(propertyName))
            {
                PropertyInfo propertyInfo = control.GetType().GetProperty(propertyName);
                if (propertyInfo == null)
                {
                    throw new Exception(control.GetType().ToString() + " does not contain '" + propertyName + "' property.");
                }

                return propertyInfo;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Set a property value on the control thread-safely.
        /// </summary>
        /// <param name="control">Control on which to SET the property value</param>
        /// <param name="propertyName">Property name</param>
        /// <param name="value">New property value</param>
        public static void SetPropertyValue(Control control, string propertyName, object value)
        {
            if (control != null && !string.IsNullOrEmpty(propertyName))
            {
                if (control.InvokeRequired)
                {
                    control.Invoke(new PropertySetInvoker(SetPropertyValue), control, propertyName, value);
                }
                else
                {
                    PropertyInfo propertyInfo = GetProperty(control, propertyName);
                    if (propertyInfo != null)
                    {
                        if (propertyInfo.CanWrite)
                        {
                            propertyInfo.SetValue(control, value, null);
                        }
                        else
                        {
                            throw new Exception(control.GetType().ToString() + "." + propertyName + " is read-only property.");
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Get a property value on the control thread-safely.
        /// </summary>
        /// <param name="control">Control on which to GET the property value</param>
        /// <param name="propertyName">Property name</param>
        /// <return>Property value</return>
        public static object GetPropertyValue(Control control, string propertyName)
        {
            if (control != null && !string.IsNullOrEmpty(propertyName))
            {
                if (control.InvokeRequired)
                {
                    return control.Invoke(new PropertyGetInvoker(GetPropertyValue), control, propertyName);
                }
                else
                {
                    PropertyInfo propertyInfo = GetProperty(control, propertyName);
                    if (propertyInfo != null)
                    {
                        if (propertyInfo.CanRead)
                        {
                            return propertyInfo.GetValue(control, null);
                        }
                        else
                        {
                            throw new Exception(control.GetType().ToString() + "." + propertyName + " is write-only property.");
                        }
                    }
                    return null;
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public static void GonewRowJanus(Janus.Windows.GridEX.GridEX grdList, string ColumnName, string sValue)
        {
            var _row = (from row in grdList.GetDataRows()
                       let loz2 = row.Cells[ColumnName].Value
                       where (loz2 != null) && (loz2.ToString().Equals(sValue))
                       select row).FirstOrDefault();
            if (_row != null)
                grdList.MoveTo(_row);
        }
        /// <summary>
        /// Bind dữ liệu vào control từ data source
        /// </summary>
        /// <param name="objectToBind">Control cần bind DL</param>
        /// <param name="propertyName">Property cần bind DL</param>
        /// <param name="sourceData">Data Source</param>
        /// <param name="memberName">Data Member</param>
        /// <returns></returns>
        public static bool TryToSetBindData(object objectToBind, string propertyName, object sourceData, string memberName)
        {
            try
            {
                try
                {
                    //((Control)objectToBind).DataBindings.Remove(new Binding(propertyName, sourceData, memberName, true,
                    // DataSourceUpdateMode.OnPropertyChanged));
                    ((Control)objectToBind).DataBindings.Clear();
                }
                catch (Exception ex)
                {
                }
                ((Control)objectToBind).DataBindings.Add(new Binding(propertyName, sourceData, memberName, true,
                DataSourceUpdateMode.OnPropertyChanged));
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        static List<string> lstUnicode = new List<string>() {  "â"
                        , "ă"
                        , "ấ"
                        , "ầ"
                        , "ậ"
                        , "ẫ"
                        , "ẩ"
                        , "ắ"
                        , "ằ"
                        , "ẵ"
                        , "ẳ"
                        , "ặ"
                        , "á"
                        , "à"
                        , "ả"
                        , "ã"
                        , "ạ"
                        , "Â"
                        , "Ă"
                        , "Ấ"
                        , "Ầ"
                        , "Ậ"
                        , "Ẫ"
                        , "Ẩ"
                        , "Ắ"
                        , "Ằ"
                        , "Ẵ"
                        , "Ẳ"
                        , "Ặ"
                        , "Á"
                        , "À"
                        , "Ả"
                        , "Ã"
                        , "Ạ"
                        , "ó"
                        , "ò"
                        , "ỏ"
                        , "õ"
                        , "ọ"
                        , "ô"
                        , "ố"
                        , "ồ"
                        , "ổ"
                        , "ỗ"
                        , "ộ"
                        , "ơ"
                        , "ớ"
                        , "ờ"
                        , "ợ"
                        , "ở"
                        , "ỡ"
                        , "Ó"
                        , "Ò"
                        , "Ỏ"
                        , "Õ"
                        , "Ọ"
                        , "Ô"
                        , "Ố"
                        , "Ồ"
                        , "Ổ"
                        , "Ỗ"
                        , "Ộ"
                        , "Ơ"
                        , "Ớ"
                        , "Ờ"
                        , "Ợ"
                        , "Ở"
                        , "Ỡ"
                        , "ư"
                        , "ứ"
                        , "ừ"
                        , "ự"
                        , "ử"
                        , "ữ"
                        , "ù"
                        , "ú"
                        , "ủ"
                        , "ũ"
                        , "ụ"
                        , "Ư"
                        , "Ứ"
                        , "Ừ"
                        , "Ự"
                        , "Ử"
                        , "Ữ"
                        , "Ù"
                        , "Ú"
                        , "Ủ"
                        , "Ũ"
                        , "Ụ"
                        , "ê"
                        , "ế"
                        , "ề"
                        , "ệ"
                        , "ể"
                        , "ễ"
                        , "è"
                        , "é"
                        , "ẻ"
                        , "ẽ"
                        , "ẹ"
                        , "Ê"
                        , "Ế"
                        , "Ề"
                        , "Ệ"
                        , "Ể"
                        , "Ễ"
                        , "È"
                        , "É"
                        , "Ẻ"
                        , "Ẽ"
                        , "Ẹ"
                        , "í"
                        , "ì"
                        , "ị"
                        , "ỉ"
                        , "ĩ"
                        , "Í"
                        , "Ì"
                        , "Ỉ"
                        , "Ĩ"
                        , "Ị"
                        , "ý"
                        , "ỳ"
                        , "ỵ"
                        , "ỷ"
                        , "ỹ"
                        , "Ý"
                        , "Ỳ"
                        , "Ỵ"
                        , "Ỷ"
                        , "Ỹ"
                        , "đ"
                        , "Đ" };
        public static bool CheckUnicode(string s)
        {
            var lstValues = s.ToLower().ToCharArray().Select(c => c.ToString()).ToList();
            var q = from p in lstUnicode
                    join p1 in lstValues on p equals p1
                    select p;
            return q.Any();

            int i = 0;
            string CH = null;
            //###################################################
            if (!string.IsNullOrEmpty(s.Trim()))
            {
                for (i = 1; i <= s.Length; i++)
                {
                    CH = VB6.Strings.Mid(s, i, 1);
                    switch (CH)
                    {
                        case "â":
                        case "ă":
                        case "ấ":
                        case "ầ":
                        case "ậ":
                        case "ẫ":
                        case "ẩ":
                        case "ắ":
                        case "ằ":
                        case "ẵ":
                        case "ẳ":
                        case "ặ":
                        case "á":
                        case "à":
                        case "ả":
                        case "ã":
                        case "ạ":
                        case "Â":
                        case "Ă":
                        case "Ấ":
                        case "Ầ":
                        case "Ậ":
                        case "Ẫ":
                        case "Ẩ":
                        case "Ắ":
                        case "Ằ":
                        case "Ẵ":
                        case "Ẳ":
                        case "Ặ":
                        case "Á":
                        case "À":
                        case "Ả":
                        case "Ã":
                        case "Ạ":
                        case "ó":
                        case "ò":
                        case "ỏ":
                        case "õ":
                        case "ọ":
                        case "ô":
                        case "ố":
                        case "ồ":
                        case "ổ":
                        case "ỗ":
                        case "ộ":
                        case "ơ":
                        case "ớ":
                        case "ờ":
                        case "ợ":
                        case "ở":
                        case "ỡ":
                        case "Ó":
                        case "Ò":
                        case "Ỏ":
                        case "Õ":
                        case "Ọ":
                        case "Ô":
                        case "Ố":
                        case "Ồ":
                        case "Ổ":
                        case "Ỗ":
                        case "Ộ":
                        case "Ơ":
                        case "Ớ":
                        case "Ờ":
                        case "Ợ":
                        case "Ở":
                        case "Ỡ":
                        case "ư":
                        case "ứ":
                        case "ừ":
                        case "ự":
                        case "ử":
                        case "ữ":
                        case "ù":
                        case "ú":
                        case "ủ":
                        case "ũ":
                        case "ụ":
                        case "Ư":
                        case "Ứ":
                        case "Ừ":
                        case "Ự":
                        case "Ử":
                        case "Ữ":
                        case "Ù":
                        case "Ú":
                        case "Ủ":
                        case "Ũ":
                        case "Ụ":
                        case "ê":
                        case "ế":
                        case "ề":
                        case "ệ":
                        case "ể":
                        case "ễ":
                        case "è":
                        case "é":
                        case "ẻ":
                        case "ẽ":
                        case "ẹ":
                        case "Ê":
                        case "Ế":
                        case "Ề":
                        case "Ệ":
                        case "Ể":
                        case "Ễ":
                        case "È":
                        case "É":
                        case "Ẻ":
                        case "Ẽ":
                        case "Ẹ":
                        case "í":
                        case "ì":
                        case "ị":
                        case "ỉ":
                        case "ĩ":
                        case "Í":
                        case "Ì":
                        case "Ỉ":
                        case "Ĩ":
                        case "Ị":
                        case "ý":
                        case "ỳ":
                        case "ỵ":
                        case "ỷ":
                        case "ỹ":
                        case "Ý":
                        case "Ỳ":
                        case "Ỵ":
                        case "Ỷ":
                        case "Ỹ":
                        case "đ":
                        case "Đ":
                            return true;
                    }
                }
            }
            return false;
        }
        public static string Bodau(string s)
        {

            int i = 0;
            string CH = null;
            if (!string.IsNullOrEmpty(s.Trim()))
            {
                for (i = 1; i <= s.Length; i++)
                {

                    CH = VB6.Strings.Mid(s, i, 1);
                    switch (CH)
                    {
                        case "â":
                        case "ă":
                        case "ấ":
                        case "ầ":
                        case "ậ":
                        case "ẫ":
                        case "ẩ":
                        case "ắ":
                        case "ằ":
                        case "ẵ":
                        case "ẳ":
                        case "ặ":
                        case "á":
                        case "à":
                        case "ả":
                        case "ã":
                        case "ạ":
                            s = s.Replace(CH, "a");
                            break;
                        case "Â":
                        case "Ă":
                        case "Ấ":
                        case "Ầ":
                        case "Ậ":
                        case "Ẫ":
                        case "Ẩ":
                        case "Ắ":
                        case "Ằ":
                        case "Ẵ":
                        case "Ẳ":
                        case "Ặ":
                        case "Á":
                        case "À":
                        case "Ả":
                        case "Ã":
                        case "Ạ":
                            s = s.Replace(CH, "A");
                            break;
                        case "ó":
                        case "ò":
                        case "ỏ":
                        case "õ":
                        case "ọ":
                        case "ô":
                        case "ố":
                        case "ồ":
                        case "ổ":
                        case "ỗ":
                        case "ộ":
                        case "ơ":
                        case "ớ":
                        case "ờ":
                        case "ợ":
                        case "ở":
                        case "ỡ":
                            s = s.Replace(CH, "o");
                            break;
                        case "Ó":
                        case "Ò":
                        case "Ỏ":
                        case "Õ":
                        case "Ọ":
                        case "Ô":
                        case "Ố":
                        case "Ồ":
                        case "Ổ":
                        case "Ỗ":
                        case "Ộ":
                        case "Ơ":
                        case "Ớ":
                        case "Ờ":
                        case "Ợ":
                        case "Ở":
                        case "Ỡ":
                            s = s.Replace(CH, "O");
                            break;
                        case "ư":
                        case "ứ":
                        case "ừ":
                        case "ự":
                        case "ử":
                        case "ữ":
                        case "ù":
                        case "ú":
                        case "ủ":
                        case "ũ":
                        case "ụ":
                            s = s.Replace(CH, "u");
                            break;
                        case "Ư":
                        case "Ứ":
                        case "Ừ":
                        case "Ự":
                        case "Ử":
                        case "Ữ":
                        case "Ù":
                        case "Ú":
                        case "Ủ":
                        case "Ũ":
                        case "Ụ":
                            s = s.Replace(CH, "U");
                            break;
                        case "ê":
                        case "ế":
                        case "ề":
                        case "ệ":
                        case "ể":
                        case "ễ":
                        case "è":
                        case "é":
                        case "ẻ":
                        case "ẽ":
                        case "ẹ":
                            s = s.Replace(CH, "e");
                            break;
                        case "Ê":
                        case "Ế":
                        case "Ề":
                        case "Ệ":
                        case "Ể":
                        case "Ễ":
                        case "È":
                        case "É":
                        case "Ẻ":
                        case "Ẽ":
                        case "Ẹ":
                            s = s.Replace(CH, "E");
                            break;
                        case "í":
                        case "ì":
                        case "ị":
                        case "ỉ":
                        case "ĩ":
                            s = s.Replace(CH, "i");
                            break;
                        case "Í":
                        case "Ì":
                        case "Ỉ":
                        case "Ĩ":
                        case "Ị":
                            s = s.Replace(CH, "I");
                            break;
                        case "ý":
                        case "ỳ":
                        case "ỵ":
                        case "ỷ":
                        case "ỹ":
                            s = s.Replace(CH, "y");
                            break;
                        case "Ý":
                        case "Ỳ":
                        case "Ỵ":
                        case "Ỷ":
                        case "Ỹ":
                            s = s.Replace(CH, "Y");
                            break;
                        case "đ":
                            s = s.Replace(CH, "d");
                            break;
                        case "Đ":
                            s = s.Replace(CH, "D");
                            break;
                    }
                }
            }
            return s;
        }
        public Image GetThumbnail(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = (size.Width / (float)sourceWidth);
            nPercentH = (size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            var destWidth = (int)(sourceWidth * nPercent);
            var destHeight = (int)(sourceHeight * nPercent);

            var b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return b;
        }
        public static void grdExVisiableColName(Janus.Windows.GridEX.GridEX grdList, string colName, bool b_Visiable)
        {
            if (grdList.RootTable.Columns.Contains(colName))
                grdList.RootTable.Columns[colName].Visible = b_Visiable;
        }
        /// <summary>
        /// hàm thực hiện thực hiện set 
        /// </summary>
        /// <param name="grdList"></param>
        /// <param name="gridExColumn"></param>
        /// <param name="sortOrder"></param>
        public static void SetGridEXSortKey(GridEX grdList, Janus.Windows.GridEX.GridEXColumn gridExColumn, Janus.Windows.GridEX.SortOrder sortOrder)
        {
            try
            {

                //Janus.Windows.GridEX.GridEXColumn gridExColumn = grdList.RootTable.Columns[DPhieuNhapTra.Columns.IdPhieuNhapTra];
                Janus.Windows.GridEX.GridEXSortKey gridExSortKey = new GridEXSortKey(gridExColumn, sortOrder);
                grdList.RootTable.SortKeys.Add(gridExSortKey);

            }
            catch (Exception ex)
            {
                return;
            }
        }
        /// <summary>
        /// hàm thực hiện việc set thông tin của trên lưới khi tìm kiếm
        /// </summary>
        /// <param name="grdList"></param>
        /// <param name="gridExColumnName"></param>
        /// <param name="sortOrder"></param>
        public static void SetGridEXSortKey(GridEX grdList, string gridExColumnName, Janus.Windows.GridEX.SortOrder sortOrder)
        {
            try
            {
                if (grdList.RootTable.Columns.Contains(gridExColumnName))
                {
                    Janus.Windows.GridEX.GridEXColumn gridExColumn = grdList.RootTable.Columns[gridExColumnName];
                    Janus.Windows.GridEX.GridEXSortKey gridExSortKey = new GridEXSortKey(gridExColumn, sortOrder);
                    grdList.RootTable.SortKeys.Add(gridExSortKey);
                }


            }
            catch (Exception ex)
            {
                return;
            }
        }

        #region "PHAN CODE CỦA ANH CƯỜNG"

        public static object GetValueDatatableColụmn(DataTable dt, String SearchColumn, String ValueColumn,
                                                     Object sValue)
        {
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr[SearchColumn].ToString() == sValue.ToString())
                    {
                        return dr[ValueColumn];
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static object GetValueOfDataRowFields(DataRow dr, String fieldName, object defaultVal)
        {
            try
            {
                if (!dr.Table.Columns.Contains(fieldName)) return defaultVal;
                return dr[fieldName];
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// hàm thực hiện việc convert thông tin của phần convert việt  nam
        /// </summary>
        /// <param name="DayEn"></param>
        /// <returns></returns>
        public static string ConvertDayVietnamese(string DayEn)
        {
            string DayVN = "";
            switch (DayEn)
            {
                case "Monday":
                    DayVN = "Thứ 2";
                    break;
                case "Tuesday":
                    DayVN = "Thứ 3";
                    break;
                case "Wednesday":
                    DayVN = "Thứ 4";
                    break;
                case "Thursday":
                    DayVN = "Thứ 5";
                    break;
                case "Friday":
                    DayVN = "Thứ 6";
                    break;
                case "Saturday":
                    DayVN = "Thứ 7";
                    break;
                case "Sunday":
                    DayVN = "Chủ nhật";
                    break;
                //case "Wednesday": DayVN = "Thứ 4"; break;
            }
            return DayVN;
        }

        public static string GetPatientSex(int PatientSex)
        {
            string sPatientSex = "Nam";
            switch (PatientSex)
            {
                case 0:
                    sPatientSex = "Nam";
                    break;
                case 1:
                    sPatientSex = "Nữ";
                    break;
                case 2:
                    sPatientSex = "Khác";
                    break;
            }
            return sPatientSex;
        }

        public static string UpperCaseFirstLetter(string str)
        {
            try
            {
                string result = str;

                if (!string.IsNullOrEmpty(str))
                {
                    string[] words = str.Split(' ');
                    for (int index = 0; index < words.Length; index++)
                    {
                        var s = words[index].ToLower();
                        if (s.Trim().Length > 0)
                        {
                            words[index] = s[0].ToString().ToUpper() + s.Substring(1);
                        }
                    }
                    result = string.Join(" ", words);
                }
                return result;
            }
            catch
            {
                return str;
            }
        }

        public static bool IsCheckConnected(string strservers, string DbName, string users, string password)
        {
            string strsql = "";
            // Common.cTripleDES MaHoa = new cTripleDES();
            bool flag = false;
            //string servers = MaHoa.Decrypt(strservers);
            //string DataName = MaHoa.Decrypt(DbName);
            //string User_ID = MaHoa.Decrypt(users);
            //string Password = MaHoa.Decrypt(password);               
            strsql += "packet size=4096;workstation id=" + strservers;
            strsql += ";data source=" + strservers;
            strsql += " ;persist security info=False;initial catalog=" + DbName;
            strsql += ";uid=" + users;
            strsql += ";pwd=" + password;
            SqlConnection cnn = new SqlConnection(strsql);
            ///Mở CSDL
            try
            {
                // Me.Cursor = Cursors.WaitCursor
                // this.Cursor = Cursors.WaitCursor;
                cnn.Open();
                //  MessageBox.Show("Kết nối thành công. Chúc mừng bạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cnn.Close();
                cnn.Dispose();
                // cmdSave.Focus();
                //this.Cursor = Cursors.Default;
                flag = true;
            }

            catch
            {
                flag = false;
            }
            return flag;
        }

        //public static string RemoveSpecialCharacters(string str)
        //{
        //    return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "" ,RegexOptions.Compiled);
        //}
        public static string RemoveSpecialCharacters(string str)
        {
            string SpecialCharacter = "~`!@#$%^&)([]{}|+-;'\"?><\\";
            string reval = "";
            for (int i = 0; i <= str.Length - 1; i++)
            {
                string s = str.Substring(i, 1);
                if (SpecialCharacter.Contains(s)) s = "";
                reval += s;
            }
            return reval;
        }

        /// <summary>
        /// Khởi tạo Subsonic
        /// </summary>
        /// <param name="globalVariables.SqlConnstr">Connection String kết nối tới CSDL</param>
        /// <param name="ProviderName">Tên Provider mặc định là ORM</param>
        public static void InitSubSonic(string SqlConnstr, string ProviderName)
        {
            DataService.Providers = new DataProviderCollection();
            var myProvider = new CustomSqlProvider(SqlConnstr);
            if (SubSonic.DataService.Providers[ProviderName] == null)
            {
                DataService.Providers.Add(myProvider);
                DataService.Provider = myProvider;
            }
            else
            {
                DataService.Provider.DefaultConnectionString = SqlConnstr;
            }
        }

        /// <summary>
        /// Thêm mới một Datarow có giá trị cột DisplayValueName=String.Empty và cột SelectedValueName=-1
        /// Được dùng chủ yếu trong mục đích bind dữ liệu từ một danh mục vào Combobox với phần tử hiển thị đầu tiên có giá trị=FirstValueToDisplay
        /// Bảng phải thỏa mãn có các cột sau
        /// Cột 1 tên:SelectedValueName
        /// Cột 2 tên:DisplayValueName
        /// Cột 3 tên:IntOrder
        /// Các cột còn lại phải allownull=true
        /// </summary>
        /// <param name="dt">Bảng chưa dữ liệu danh mục khi chưa thêm gì cả</param>
        /// <param name="SelectedValueName">Tên cột chứa Giá trị cần lấy(Thường là cột ID)</param>
        /// <param name="DisplayValueName">Tên cột chứa giá trị hiển thị(Thường là cột Name)</param>
        public static void AddColumnAlltoDataTable(ref DataTable dt, string SelectedValueName, string DisplayValueName,
                                                   string FirstValueToDisplay)
        {
            if (dt == null) return;
            DataRow dr = dt.NewRow();
            dr[SelectedValueName] = -1;
            dr[DisplayValueName] = FirstValueToDisplay;
            //L_Stocks
            //List Objects
            if (dt.Columns.Contains(DmucDoituongkcb.Columns.SttHthi)) dr[DmucDoituongkcb.Columns.SttHthi] = 0;
            //SysRoles
            if (dt.Columns.Contains(SysRole.Columns.IParentRole)) dr[SysRole.Columns.IParentRole] = 1;
            if (dt.Columns.Contains(SysRole.Columns.FpSBranchID)) dr[SysRole.Columns.FpSBranchID] = globalVariables.Branch_ID;
            //LStaffs
            if (dt.Columns.Contains(DmucNhanvien.Columns.MaLoainhanvien)) dr[DmucNhanvien.Columns.MaLoainhanvien] = 1;
            if (dt.Columns.Contains(DmucKhoaphong.Columns.IdKhoaphong)) dr[DmucKhoaphong.Columns.IdKhoaphong] = -1;

            if (dt.Columns.Contains(DmucChung.Columns.TrangThai)) dr[DmucChung.Columns.TrangThai] = 1;
            //LDepartments


            if (dt.Columns.Contains(DmucKhoaphong.Columns.PhongChucnang)) dr[DmucKhoaphong.Columns.PhongChucnang] = 1;
            if (dt.Columns.Contains(DmucKhoaphong.Columns.KieuKhoaphong)) dr[DmucKhoaphong.Columns.KieuKhoaphong] = 0;
            if (dt.Columns.Contains(DmucKhoaphong.Columns.MaCha)) dr[DmucKhoaphong.Columns.MaCha] = 0;

            if (dt.Columns.Contains(TDmucKho.Columns.MaKho)) dr[TDmucKho.Columns.MaKho] = "";


            if (dt.Columns.Contains(DmucDoituongkcb.Columns.SttHthi)) dr[DmucDoituongkcb.Columns.SttHthi] = 0;
            if (dt.Columns.Contains(DmucDoituongkcb.Columns.PhantramDungtuyen)) dr[DmucDoituongkcb.Columns.PhantramDungtuyen] = 0;
            if (dt.Columns.Contains(DmucDoituongkcb.Columns.IdLoaidoituongKcb)) dr[DmucDoituongkcb.Columns.IdLoaidoituongKcb] = 0;
            //Loại khám 
            dt.Rows.Add(dr);
            dt.AcceptChanges();
        }

        public static void AddColumnAlltoUserDataTable(ref DataTable dt, string SelectedValueName,
                                                       string FirstValueToDisplay)
        {
            if (dt == null) return;
            DataRow dr = dt.NewRow();
            dr[SelectedValueName] = FirstValueToDisplay;
            if (dt.Columns.Contains(SysRole.Columns.FpSBranchID)) dr[SysRole.Columns.FpSBranchID] = "";
            dt.Rows.Add(dr);
            dt.AcceptChanges();
        }

        /// <summary>
        /// Lấy về số các phần tử được Check trong các Datatable có cột Chọn(CheckColumnName)
        /// </summary>
        /// <param name="dt">Data chứa dữ liệu</param>
        /// <param name="CheckColumnName">Tên cột GridCheckboxColumn</param>
        /// <returns></returns>
        public static int GetItemsChecked(DataTable dt, string CheckColumnName)
        {
            int reval = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[CheckColumnName] != DBNull.Value && dr[CheckColumnName] != null &&
                    Convert.ToInt16(dr[CheckColumnName]) == 1) reval += 1;
            }
            return reval;
        }

        /// <summary>
        /// HAM THỰC HIỆN HIÊN THỊ LABLEL
        /// </summary>
        /// <param name="lblMsg"></param>
        /// <param name="Message"></param>
        /// <param name="isErr"></param>
        public static void SetMsg(Label lblMsg, string Message, bool isErr)
        {
            if (isErr)
            {
                lblMsg.ForeColor = Color.Red;
            }
            else
            {
                lblMsg.ForeColor = Color.DarkBlue;
            }
            lblMsg.Text = Message;
            Application.DoEvents();
        }
        public static void GetChandoanNoitru(KcbLuotkham objKcbLuotkham,DateTime ngay_dieutri, ref string ma_icd, ref string ten_icd, ref string chandoan_phu)
        {
            try
            {
                DataTable dtChandoan= SPs.NoitruLaythongtinchandoan(objKcbLuotkham.MaLuotkham,objKcbLuotkham.IdBenhnhan,-1,100,ngay_dieutri).GetDataSet().Tables[0];
                DataTable dtTemp = dtChandoan.Clone();
                DataRow[] arrDr = dtChandoan.Select("noitru=1 and kieu_chandoan=2");
                if (arrDr.Length > 0) dtTemp = arrDr.CopyToDataTable();
                List<string> lstIcd = (from p in dtTemp.AsEnumerable()
                                           select Utility.sDbnull(p["mabenh_chinh"], "")).ToList<string>();
                ma_icd = string.Join(",", lstIcd.ToArray<string>());
                chandoan_phu = string.Join(",", (from p in dtTemp.AsEnumerable()
                                           select Utility.sDbnull(p["chan_doan"], "")).ToArray<string>());
                ten_icd= string.Join(",", (from p in globalVariables.gv_dtDmucBenh.AsEnumerable()
                                           where lstIcd.Contains(Utility.sDbnull(p["ma_benh"]))
                                           select Utility.sDbnull(p["ten_benh"], "")).ToArray<string>());
            }
            catch (Exception  ex)
            {

                Utility.CatchException(ex);
            }
        }
        public static void GetChandoanNoitru(long id_benhnhan, string ma_luotkham, DateTime ngay_dieutri, ref string ma_icd, ref string ten_icd, ref string chandoan_phu)
        {
            try
            {
                DataTable dtChandoan = SPs.NoitruLaythongtinchandoan(ma_luotkham, id_benhnhan, -1, 100, ngay_dieutri).GetDataSet().Tables[0];
                DataTable dtTemp = dtChandoan.Clone();
                DataRow[] arrDr = dtChandoan.Select("noitru=1 and kieu_chandoan=2");
                if (arrDr.Length > 0) dtTemp = arrDr.CopyToDataTable();
                List<string> lstIcd = (from p in dtTemp.AsEnumerable()
                                       select Utility.sDbnull(p["mabenh_chinh"], "")).ToList<string>();
                ma_icd = string.Join(",", lstIcd.ToArray<string>());
                chandoan_phu = string.Join(",", (from p in dtTemp.AsEnumerable()
                                                 select Utility.sDbnull(p["chan_doan"], "")).ToArray<string>());
                ten_icd = string.Join(",", (from p in globalVariables.gv_dtDmucBenh.AsEnumerable()
                                            where lstIcd.Contains(Utility.sDbnull(p["ma_benh"]))
                                            select Utility.sDbnull(p["ten_benh"], "")).ToArray<string>());
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        public static KcbLuotkham getKcbLuotkhamFromGrid(GridEX grdlist)
        {
            try
            {
               return new Select().From(KcbLuotkham.Schema)
                    .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.GetValueFromGridColumn(grdlist, KcbLuotkham.Columns.IdBenhnhan))
                    .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.GetValueFromGridColumn(grdlist, KcbLuotkham.Columns.MaLuotkham)).ExecuteSingle<KcbLuotkham>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Refresh lại lượt khám
        /// </summary>
        /// <param name="objKcbLuotkham"></param>
        /// <returns></returns>
        public static KcbLuotkham getKcbLuotkham(KcbLuotkham objKcbLuotkham)
        {
            try
            {
                return new Select().From(KcbLuotkham.Schema)
                     .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objKcbLuotkham.IdBenhnhan)
                     .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objKcbLuotkham.MaLuotkham).ExecuteSingle<KcbLuotkham>();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return null;
            }
        }
        public static KcbLuotkham getKcbLuotkham(GridEXRow row)
        {
            try
            {
                return new Select().From(KcbLuotkham.Schema)
                     .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int64Dbnull(row.Cells[KcbLuotkham.Columns.IdBenhnhan].Value))
                     .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(row.Cells[KcbLuotkham.Columns.MaLuotkham].Value)).ExecuteSingle<KcbLuotkham>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static VKcbLuotkham getKcbBenhnhan(long idbenhnhan, string maluotkham)
        {
            try
            {
                var q = new Select().From(VKcbLuotkham.Schema)
                            .Where(VKcbLuotkham.Columns.IdBenhnhan).IsEqualTo(idbenhnhan)
                            .And(VKcbLuotkham.Columns.MaLuotkham).IsEqualTo(maluotkham)
                            .ExecuteAsCollection<VKcbLuotkhamCollection>();
                if (q.Any())
                    return q.FirstOrDefault();
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static KcbDanhsachBenhnhan getKcbDanhsachBenhnhan(KcbLuotkham objLuotkham)
        {
            try
            {
                return KcbDanhsachBenhnhan.FetchByID(objLuotkham.IdBenhnhan);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static KcbLuotkham getKcbLuotkham(long idbenhnhan,string maluotkham)
        {
            try
            {
                return new Select().From(KcbLuotkham.Schema)
                     .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(idbenhnhan)
                     .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(maluotkham).ExecuteSingle<KcbLuotkham>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static KcbLuotkham getKcbLuotkham(object idbenhnhan, object maluotkham)
        {
            try
            {
                return new Select().From(KcbLuotkham.Schema)
                     .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(idbenhnhan)
                     .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(maluotkham).ExecuteSingle<KcbLuotkham>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static NoitruPhieudieutri getNoitruPhieudieutri(long idbenhnhan, string maluotkham)
        {
            try
            {
                return new Select().From(NoitruPhieudieutri.Schema)
                     .Where(NoitruPhieudieutri.Columns.IdBenhnhan).IsEqualTo(idbenhnhan)
                     .And(NoitruPhieudieutri.Columns.MaLuotkham).IsEqualTo(maluotkham).ExecuteSingle<NoitruPhieudieutri>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static KcbLuotkham getKcbLuotkham(string maluotkham)
        {
            try
            {
                return new Select().From(KcbLuotkham.Schema)
                     .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(maluotkham).ExecuteSingle<KcbLuotkham>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static void SetColor(Label lblMsg,Color _color)
        {
           
                lblMsg.ForeColor = _color;
           
            Application.DoEvents();
        }
        public static void SetMsg(ToolStripStatusLabel lblMsg, string Message, bool isErr)
        {
            if (isErr)
            {
                lblMsg.ForeColor = Color.Red;
            }
            else
            {
                lblMsg.ForeColor = Color.DarkBlue;
            }
            lblMsg.Text = Message;
            Application.DoEvents();
        }
        public static void SetMsg(UIStatusBarPanel lblMsg, string Message, bool isErr)
        {
            if (isErr)
            {
                lblMsg.FormatStyle.ForeColor = Color.Red;
            }
            else
            {
                lblMsg.FormatStyle.ForeColor = Color.DarkBlue;
            }
            lblMsg.Text = Message;
            Application.DoEvents();
        }
        public static void SetMsg(UIStatusBarPanel lblMsg, string Message)
        {
            lblMsg.Text = Message;
            Application.DoEvents();
        }
        public static byte Bool2byte(RadioButton obj)
        {
            return obj.Checked ? Convert.ToByte(1) : Convert.ToByte(0);
        }
        public static byte Bool2byte(CheckBox obj)
        {
            return obj.Checked ? Convert.ToByte(1) : Convert.ToByte(0);
        }
        public static byte Bool2byte(bool obj)
        {
            return obj?Convert.ToByte(1):Convert.ToByte(0);
        }
        public static byte Bool2byte(bool? obj)
        {
            if (obj == null || !obj.HasValue) Convert.ToByte(0);
            return obj.Value ? Convert.ToByte(1) : Convert.ToByte(0);
        }
        public static bool Byte2Bool(byte obj)
        {
            return obj != null &&  obj == 1;
        }
        public static bool Obj2Bool(object obj)
        {
            return obj != null && Convert.ToBoolean( obj);
        }
        public static bool Byte2Bool(byte? obj)
        {
            return obj != null && obj.Value == 1;
        }
        public static bool Bool2Bool(bool? obj)
        {
            return obj != null && obj.Value ;
        }
        public static bool Byte2Bool(object obj)
        {
            return obj != null && Utility.Int32Dbnull( obj,0) == 1;
        }
       
        public static byte ByteDbnull(object obj)
        {
            if (!(((obj != null) && (obj != DBNull.Value)) && IsNumeric(obj)))
            {
                return 0;
            }
            return Convert.ToByte(obj);
        }
        public static byte GetVitriHinhAnhByName(string fileName)
        {
          return  Utility.ByteDbnull(fileName.Contains("_OS_") || fileName.Contains("_TRAI_") ? 1 : 0);
        }
        /// <summary>
        /// hàm thưucj hiện kiểm tra xem thông tin nếu bytenull
        /// thì trả về giá trị mặc định nếu không 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="DefaultVal"></param>
        /// <returns></returns>
        public static byte ByteDbnull(object obj, object DefaultVal)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return Convert.ToByte(DefaultVal);
            }
            else
            {
                return Convert.ToByte(obj);
            }
        }
        public static byte isTrue(byte _value, byte truevalue, byte falsevalue)
        {
            if (Utility.ByteDbnull(_value, 0) == (byte)1) return truevalue;
            return falsevalue;
        }
        public static byte isTrue(byte? _value, byte truevalue, byte falsevalue)
        {
            if (Utility.ByteDbnull(_value, 0) == (byte)1) return truevalue;
            return falsevalue;
        }
        public static bool isTrue(byte _value)
        {
            return Utility.ByteDbnull(_value, 0) == (byte)1;
        }
        public static bool isTrue(byte? _value)
        {
            return Utility.ByteDbnull(_value, 0) == (byte)1;
        }
        public static void TryToFilter(DataView dv, string RowFilter)
        {
            try
            {
                dv.RowFilter = RowFilter;
            }
            catch
            {
                dv.RowFilter = "1=1";
            }
        }

        /// <summary>
        /// Lấy về số các phần tử được Check trong các Datatable có cột Chọn(CheckColumnName)
        /// </summary>
        /// <param name="dt">Data chứa dữ liệu</param>
        /// <param name="CheckColumnName">Tên cột GridCheckboxColumn</param>
        /// <returns></returns>
        public static int GetItemsChecked(DataView dtv, string CheckColumnName)
        {
            int reval = 0;
            foreach (DataRowView drv in dtv)
            {
                if (drv[CheckColumnName] != DBNull.Value && drv[CheckColumnName] != null &&
                    Convert.ToInt16(drv[CheckColumnName]) == 1) reval += 1;
            }
            return reval;
        }

        /// <summary>
        /// Hàm dùng để binding giá trị nhập của một Textbox chỉ được phép là chữ số
        /// Hàm này được gọi trong sự kiện Keypress
        /// </summary>
        /// <param name="pstrChar">e.Keychar</param>
        /// <param name="oTextBox">TextBox cần binding</param>
        /// <returns></returns>
        public static bool NumbersOnly(Char pstrChar, TextBox oTextBox)
        {
            if (pstrChar.ToString() != Constants.vbBack)
            {
                return IsNumeric(pstrChar) || IsNumeric(pstrChar.ToString() + oTextBox.Text) ? false : true;
            }
            return false;
        }

        /// <summary>
        /// Trả về DataRow trong bảng dt thỏa mãn điều kiện lọc filterExpression
        /// </summary>
        /// <param name="dt">Bảng chứa dữ liệu cần tìm</param>
        /// <param name="filterExpression">Điều kiện lọc</param>
        /// <returns></returns>
        public static DataRow FetchOnebyCondition(DataTable dt, string filterExpression)
        {
            try
            {
                DataRow[] dr = dt.Select(filterExpression);
                if (dr.GetLength(0) > 0) return dr[0];
                else return null;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Lấy về các nhân viên dựa vào loại nhân viên
        /// </summary>
        /// <returns></returns>
        private static string GetStaffRowFilter(string StaffType)
        {
            string Reval = "";
            string[] Args = StaffType.Split(',');
            for (int i = 0; i <= Args.Length - 1; i++)
            {
                if (!Utility.IsNumeric(Args[i])) return "1=1";
                Reval = Reval + " StaffType_ID=" + Args[i] + " OR ";
            }
            Reval = Reval.Substring(0, Reval.LastIndexOf(" OR "));
            return Reval;
        }

        /// <summary>
        /// Lấy về các nhân viên là bảo vệ dựa vào biến hệ thống có tên Guardian
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyGuardians()
        {
            return GetStaffRowFilter(globalVariables.Guardian);
        }

        /// <summary>
        /// Lấy về các nhân viên là Thư ký dựa vào biến hệ thống có tên Clerk
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyClerks()
        {
            return GetStaffRowFilter(globalVariables.Clerk);
        }

        /// <summary>
        /// Lấy về các nhân viên là Trợ lý dựa vào biến hệ thống có tên Assistant
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyAssistants()
        {
            return GetStaffRowFilter(globalVariables.Assistant);
        }

        /// <summary>
        /// Lấy về các nhân viên là nhân viên tiếp tân dựa vào biến hệ thống có tên Receptionist
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyReceptionists()
        {
            return GetStaffRowFilter(globalVariables.Receptionist);
        }
        /// <summary>
        /// Lấy về ký hiệu bệnh án
        /// </summary>
        /// <returns></returns>
        public static string GetLoaiBenhAn(LoaiBenhAn _loaibenhan)
        {
            if (_loaibenhan == LoaiBenhAn.BA_Thuong) return "THU";
            if (_loaibenhan == LoaiBenhAn.BA_DaiThaoDuong) return "DTD";
            if (_loaibenhan == LoaiBenhAn.BA_TangHuyetAp) return "THA";
            if (_loaibenhan == LoaiBenhAn.BA_Basedow) return "BAS";
            if (_loaibenhan == LoaiBenhAn.BA_Cop) return "COP";
            if (_loaibenhan == LoaiBenhAn.BA_ViemGanB) return "VGB";
            if (_loaibenhan == LoaiBenhAn.BA_TaiMuiHong) return "TMH";
            if (_loaibenhan == LoaiBenhAn.BA_RangHamMat) return "RHM";


            return "";
        }
        /// <summary>
        /// Lấy về các nhân viên là bác sĩ chẩn đoán dựa vào biến hệ thống có tên DiagnosticDoctor
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyDiagnosticDoctors()
        {
            return GetStaffRowFilter(globalVariables.DiagnosticDoctor);
        }

        /// <summary>
        /// Lấy về các nhân viên là bác sĩ chỉ định dựa vào biến hệ thống có tên AssignDoctor
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyAssignDoctors()
        {
            return GetStaffRowFilter(globalVariables.AssignDoctor);
        }

        /// <summary>
        /// Lấy về các nhân viên là kế toán dựa vào biến hệ thống có tên Accountant
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyAccountants()
        {
            return GetStaffRowFilter(globalVariables.Accountant);
        }

        /// <summary>
        /// Lấy về các nhân viên là Dược sĩ dựa vào biến hệ thống có tên Druggist
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyDruggists()
        {
            return GetStaffRowFilter(globalVariables.Druggist);
        }

        /// <summary>
        /// Lấy về các nhân viên là bác sĩ dựa vào biến hệ thống có tên Doctors
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyDoctors()
        {
            return GetStaffRowFilter(globalVariables.Doctors);
        }

        /// <summary>
        /// Lấy về các nhân viên là nhân viên kho dựa vào biến hệ thống có tên StoreKeepers
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyStoreKeepers()
        {
            return GetStaffRowFilter(globalVariables.StoreKeepers);
        }

        /// <summary>
        /// Trả về mảng các DataRow trong bảng dt thỏa mãn điều kiện lọc filterExpression
        /// </summary>
        /// <param name="dt">Bảng chứa dữ liệu cần tìm</param>
        /// <param name="filterExpression">Điều kiện lọc</param>
        /// <returns></returns>
        public static DataRow[] FetchAllsbyCondition(DataTable dt, string filterExpression)
        {
            try
            {
                DataRow[] dr = dt.Select(filterExpression);
                if (dr.GetLength(0) > 0) return dr;
                else return null;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Ánh xạ từ Subsonic Object vào DataRow
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dr"></param>
        public static void FromObjectToDatarow(object obj, ref DataRow dr)
        {
            try
            {
                //Nếu dữ liệu không hợp lệ thì không làm gì cả
                if (dr == null || obj == null) return;
                //Nếu dữ liệu hợp lệ thì tiến hành set giá trị cho ObjectInfor từ giá trị các cột chứa trong Entity
                foreach (DataColumn Col in dr.Table.Columns)
                {
                    foreach (
                        System.Reflection.PropertyInfo pro in
                            obj.GetType().GetProperties(System.Reflection.BindingFlags.Instance |
                                                        System.Reflection.BindingFlags.NonPublic |
                                                        System.Reflection.BindingFlags.Public))
                    {
                        if (pro.Name.ToUpper() == Col.ColumnName.ToUpper() ||
                            pro.Name.Replace("_", "").ToUpper() == Col.ColumnName.Replace("_", "").ToUpper())
                        {
                            try
                            {
                                dr[Col.ColumnName] = pro.GetValue(obj, null) == null
                                                         ? DBNull.Value
                                                         : pro.GetValue(obj, null);
                            }
                            catch
                            {
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
                return;
            }
        }

        ///<summary>
        ///<para>Tự động Mapping các giá trị từ Entity sang ObjectInfor</para>
        ///</summary>   
        /// <param name="ObjectInfor"><para>Đối tượng cần Map giá trị</para></param>     
        /// <param name="Entity"><para>Entity chứa giá trị của Object</para></param>     
        ///<returns>Gắn các giá trị từ Entity vào ObjectInfor</returns>
        /// <summary>
        public static void MapValueFromEntityIntoObjectInfor(object ObjectInfo, DataTable Entity)
        {
            try
            {
                //Nếu dữ liệu không hợp lệ thì không làm gì cả
                if (Entity == null || ObjectInfo == null || Entity.Rows.Count <= 0) return;
                //Nếu dữ liệu hợp lệ thì tiến hành set giá trị cho ObjectInfor từ giá trị các cột chứa trong Entity
                foreach (DataColumn Col in Entity.Columns)
                {
                    foreach (
                        System.Reflection.FieldInfo field in
                            ObjectInfo.GetType().GetFields(System.Reflection.BindingFlags.Instance |
                                                           System.Reflection.BindingFlags.NonPublic |
                                                           System.Reflection.BindingFlags.Public))
                    {
                        if (field.Name.ToUpper() == Col.ColumnName.ToUpper())
                        {
                            if (Entity.Rows[0][Col.ColumnName] == DBNull.Value || Entity.Rows[0][Col.ColumnName] == null)
                                field.SetValue(ObjectInfo, null);
                            else
                                field.SetValue(ObjectInfo, Entity.Rows[0][Col.ColumnName]);
                            //thoát khỏi vòng lặp sau khi tìm thấy
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
                return;
            }
        }

        /// <summary>
        /// Ánh xạ giá trị từ DataRowView thành giá trị của ObjectInfo
        /// </summary>
        /// <param name="ObjectInfo">Object Info</param>
        /// <param name="Entity">Entity chứa dữ liệu</param>
        public static void MapValueFromEntityIntoObjectInfor(object ObjectInfo, DataRowView DRV)
        {
            try
            {
                //Nếu dữ liệu không hợp lệ thì không làm gì cả
                if (DRV == null || ObjectInfo == null) return;
                //Nếu dữ liệu hợp lệ thì tiến hành set giá trị cho ObjectInfor từ giá trị các cột chứa trong Entity
                foreach (DataColumn Col in DRV.DataView.Table.Columns)
                {
                    foreach (
                        System.Reflection.FieldInfo field in
                            ObjectInfo.GetType().GetFields(System.Reflection.BindingFlags.Instance |
                                                           System.Reflection.BindingFlags.NonPublic |
                                                           System.Reflection.BindingFlags.Public))
                    {
                        if (field.Name.ToUpper() == Col.ColumnName.ToUpper())
                        {
                            if (DRV[Col.ColumnName] == DBNull.Value || DRV[Col.ColumnName] == null)
                                field.SetValue(ObjectInfo, null);
                            else
                                field.SetValue(ObjectInfo, DRV[Col.ColumnName]);
                            //thoát khỏi vòng lặp sau khi tìm thấy
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
                return;
            }
        }

        /// <summary>
        /// Ánh xạ giá trị từ Entity thành giá trị của ObjectInfo
        /// </summary>
        /// <param name="ObjectInfo">Object Info</param>
        /// <param name="Entity">Entity chứa dữ liệu</param>
        public static void MapValueFromEntityIntoObjectInfor(object ObjectInfo, DataRow Entity)
        {
            try
            {
                //Nếu dữ liệu không hợp lệ thì không làm gì cả
                if (Entity == null || ObjectInfo == null) return;
                //Nếu dữ liệu hợp lệ thì tiến hành set giá trị cho ObjectInfor từ giá trị các cột chứa trong Entity
                foreach (DataColumn Col in Entity.Table.Columns)
                {
                    foreach (
                        System.Reflection.FieldInfo field in
                            ObjectInfo.GetType().GetFields(System.Reflection.BindingFlags.Instance |
                                                           System.Reflection.BindingFlags.NonPublic |
                                                           System.Reflection.BindingFlags.Public))
                    {
                        if (field.Name.ToUpper() == Col.ColumnName.ToUpper())
                        {
                            if (Entity[Col.ColumnName] == DBNull.Value || Entity[Col.ColumnName] == null)
                                field.SetValue(ObjectInfo, null);
                            else
                                field.SetValue(ObjectInfo, Entity[Col.ColumnName]);
                            //thoát khỏi vòng lặp sau khi tìm thấy
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
                return;
            }
        }
        public static void FromDatarow2Object(DataRow Entity, object ObjectInfo)
        {
            try
            {
                //Nếu dữ liệu không hợp lệ thì không làm gì cả
                if (Entity == null || ObjectInfo == null) return;
                //Nếu dữ liệu hợp lệ thì tiến hành set giá trị cho ObjectInfor từ giá trị các cột chứa trong Entity
                PropertyInfo[] arrProps = ObjectInfo.GetType().GetProperties();
                foreach (DataColumn Col in Entity.Table.Columns)
                {
                    foreach (System.Reflection.PropertyInfo props in arrProps)
                    {
                        if (props.Name.ToUpper() == Col.ColumnName.ToUpper())
                        {
                            if (Entity[Col.ColumnName] == DBNull.Value || Entity[Col.ColumnName] == null)
                                props.SetValue(ObjectInfo, null,null);
                            else
                                props.SetValue(ObjectInfo, Entity[Col.ColumnName],null);
                            //thoát khỏi vòng lặp sau khi tìm thấy
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
                return;
            }
        }
        ///<summary>
        ///<para>Lấy về IPAddress của máy tính thực hiện gọi hàm này. Khi lập trình thì IP này được xác định ngay từ các máy Client(Browse)</para>
        ///</summary>   
        ///<returns>Địa chỉ máy tính. Ví dụ: 10.1.6.60</returns>
        public static string GetIPAddress()
        {
            try
            {
                return System.Net.Dns.Resolve(System.Net.Dns.GetHostName()).AddressList[0].ToString();
            }
            catch (Exception ex)
            {
                return "127.0.0.1";
            }
        }
        public static void GetIP4Address()
        {
            try
            {
                string IP4Address = String.Empty;

                foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
                {
                    if (IPA.AddressFamily == AddressFamily.InterNetwork)
                    {
                        IP4Address = IPA.ToString();
                        break;
                    }
                }

                globalVariables.gv_strMacAddress = IP4Address;

            }
            catch (Exception)
            {
                globalVariables.gv_strMacAddress = "";
            }

        }
        /// <summary>
        /// Lấy về tên máy tính người dùng đang chạy hệ thống
        /// </summary>
        /// <returns></returns>
        public static string GetComputerName()
        {
            return System.Environment.MachineName;
        }

        /// <summary>
        /// Lấy về tên tài khoản đăng nhập vào windows
        /// </summary>
        /// <returns></returns>
        public static string GetAccountName()
        {
            return System.Environment.UserName;
        }

        ///<summary>
        ///<para>Lưu vết người dùng</para>
        ///</summary>   
        /// <param name="trace"><para>Đối tượng vết <see cref="HISTrace"/></para></param>     
        ///<returns>
        /// <para>ResultType.SaveTraceSuccess nếu SaveTrace thành công</para>
        /// <para>ResultType.CannotSaveTrace or ResultType.Exception nếu SaveTrace không thành công</para>
        ///</returns>
        public static ActionResult SaveTrace(TraceInfor trace, System.Data.SqlClient.SqlTransaction Trans)
        {
            if (trace == null)
            {
                //Utility.ShowMsg("Bạn phải khởi tạo đối tượng trace=new TraceInfor() trước khi sử dụng Method này");
                //Nếu TraceInfor=null có nghĩa là ko cần theo dõi vết đối với nghiệp vụ đang xét
                return ActionResult.Success;
            }
            return new TraceController(trace).Save(Trans);
        }

        ///<summary>
        ///<para>Lấy về đối tượng vết từ gói tin gửi từ Client lên Application Server: Header</para>
        ///</summary>   
        /// <param name="Header"><para>Bảng chứa thông tin vết. Thường là Header.Tables["Header"] <see cref="DataTable"/></para></param>     
        ///<returns>HISTrace nếu tồn tại Header. Ngược lại trả về null.</returns>
        public static TraceInfor GetTraceObjectFromHeader(DataTable Header)
        {
            TraceInfor trace = new TraceInfor();
            try
            {
                if (Header.Equals(null))
                {
                    //Utility.ShowMsg("Header bạn đưa vào chưa được khởi tạo nên chưa có thông tin về Trace. Đề nghị bạn xem lại...");
                    return null;
                }
                //Read from Header Into Trace
                trace.pID = Int32Dbnull(Header.Rows[0]["ID"], 0);
                trace.pBranchID = CorrectStringValue(sDbnull(Header.Rows[0]["BranchID"]));
                trace.pUserName = CorrectStringValue(sDbnull(Header.Rows[0]["UserName"]));
                trace.pCreatedDate = CorrectStringValue(sDbnull(Header.Rows[0]["CreatedDate"]));
                trace.pIPAddress = CorrectStringValue(sDbnull(Header.Rows[0]["IPAddress"]));
                trace.ComputerName = CorrectStringValue(sDbnull(Header.Rows[0]["ComputerName"]));
                trace.AccountName = CorrectStringValue(sDbnull(Header.Rows[0]["AccountName"]));
                trace.DLLName = CorrectStringValue(sDbnull(Header.Rows[0]["DLLName"]));
                trace.pSubSystemName = CorrectStringValue(sDbnull(Header.Rows[0]["SubSystemName"]));
                trace.pFunctionID = Int16Dbnull(Header.Rows[0]["FunctionID"]);
                trace.FunctionName = CorrectStringValue(sDbnull(Header.Rows[0]["FunctionName"]));
                trace.pTableName = CorrectStringValue(sDbnull(Header.Rows[0]["TableName"]));
                trace.pDesc = CorrectStringValue(sDbnull(Header.Rows[0]["Desc"]));
                trace.pLO = Int16Dbnull(Header.Rows[0]["LOT"], 0);
                return trace;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lấy đối tượng vết từ Header:\n" + ex.Message);
                return null;
            }
        }

        ///<summary>
        ///<para>Lấy về đối tượng vết từ gói tin gửi từ Client lên Application Server: Header</para>
        ///</summary>   
        /// <param name="Header"><para>Dataset chứa Header chứa thông tin vết. Thường là Header <see cref="DataTable"/></para></param>     
        ///<returns>HISTrace nếu tồn tại Header. Ngược lại trả về null.</returns>
        public static TraceInfor GetTraceObjectFromHeader(DataSet Header)
        {
            TraceInfor trace = new TraceInfor();
            try
            {
                if (Header.Equals(null) || Header.Tables.Count <= 0 || Header.Tables["Header"].Rows.Count <= 0)
                {
                    //Utility.ShowMsg("Header bạn đưa vào chưa được khởi tạo nên chưa có thông tin về Trace. Đề nghị bạn xem lại...");
                    return null;
                }
                //Read from Header Into Trace
                trace.pID = Int32Dbnull(Header.Tables["Header"].Rows[0]["ID"], 0);
                trace.pBranchID = CorrectStringValue(sDbnull(Header.Tables["Header"].Rows[0]["BranchID"]));
                trace.pUserName = CorrectStringValue(sDbnull(Header.Tables["Header"].Rows[0]["UserName"]));
                trace.pCreatedDate = CorrectStringValue(sDbnull(Header.Tables["Header"].Rows[0]["CreatedDate"]));
                trace.pIPAddress = CorrectStringValue(sDbnull(Header.Tables["Header"].Rows[0]["IPAddress"]));
                trace.ComputerName = CorrectStringValue(sDbnull(Header.Tables["Header"].Rows[0]["ComputerName"]));
                trace.AccountName = CorrectStringValue(sDbnull(Header.Tables["Header"].Rows[0]["AccountName"]));
                trace.DLLName = CorrectStringValue(sDbnull(Header.Tables["Header"].Rows[0]["DLLName"]));
                trace.pSubSystemName = CorrectStringValue(sDbnull(Header.Tables["Header"].Rows[0]["SubSystemName"]));
                trace.pFunctionID = Int16Dbnull(Header.Tables["Header"].Rows[0]["FunctionID"]);
                trace.FunctionName = CorrectStringValue(sDbnull(Header.Tables["Header"].Rows[0]["FunctionName"]));
                trace.pTableName = CorrectStringValue(sDbnull(Header.Tables["Header"].Rows[0]["TableName"]));
                trace.pDesc = CorrectStringValue(sDbnull(Header.Tables["Header"].Rows[0]["Desc"]));
                trace.pLO = Int16Dbnull(Header.Tables["Header"].Rows[0]["LOT"], 0);
                return trace;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lấy đối tượng vết từ Header:\n" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Các trạng thái được dùng khi tìm kiếm hoặc thao tác với CSDL mất nhiều time
        /// </summary>
        public enum LoadingStatus
        {
            PreparingSearchCondition = 0,
            RequestingData = 1,
            PustDataIntoDataGrid = 2,
            ProcessingDataBeforeUsing = 3,
            PustDataIntoCrystalReport = 4,
            WritetoFile = 5
        }

        /// <summary>
        /// Chuyển giá trị(thường là của một đối tượng Datetime) có giá trị mặc định là MinValue thành null
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static object DateTimeToNUll(DateTime dt)
        {
            if (dt.ToShortDateString() == DateTime.MinValue.ToShortDateString()) return null;
            else return dt;
        }

        /// <summary>
        /// Kiểm tra đối tượng có phải là kiểu chữ số hay không
        /// </summary>
        /// <param name="Value">đối tượng cần kiểm tra</param>
        /// <returns></returns>
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

        ///<summary>
        ///<para>Thiết lập DataSource cho DataGridView</para>
        ///</summary>   
        /// <param name="DataGridView"><para>GridView hiển thị thông tin <see cref="DataGridView"/></para></param>     
        /// <param name="DataSource"><para>Source của GridView <see cref="DataTable"/></para></param> 
        /// <param name="AutogeneratedColumns"><para>Có tự động sinh cột hay không. Mặc định nên đặt giá trị này=false <see cref="bool"/></para></param>     
        /// <param name="DefaultView"><para>Source là Dataview của DataTable hay không. Nên đặt=true <see cref="bool"/></para></param>     
        /// <param name="RowFilterExpression"><para> Điều kiện lọc dữ liệu từ DefaultView.Nếu ko muốn thì đặt giá trị=Empty<see cref="bool"/></para></param>     
        /// <param name="DefaultViewSort"><para>Thứ tự sắp xếp trên DefaultView.Nếu ko muốn thì đặt giá trị=Empty<see cref="bool"/></para></param>     
        ///<returns></returns>
        public static void SetDataSourceForDataGridView(DataGridView varDataGridView, DataTable DataSource,
                                                        bool AutogeneratedColumns, bool UseDefaultViewAsDataSource,
                                                        string RowFilterExpression, string DefaultViewSort)
        {
            try
            {
                if (DataSource == null || DataSource.Columns.Count <= 0) return;
                varDataGridView.DataSource = null;
                varDataGridView.AutoGenerateColumns = false;
                if (UseDefaultViewAsDataSource)
                {
                    if (DefaultViewSort.Trim() != "" && DefaultViewSort.Trim() != string.Empty)
                        DataSource.DefaultView.Sort = DefaultViewSort;
                    if (RowFilterExpression.Trim() != "" && RowFilterExpression.Trim() != string.Empty)
                        DataSource.DefaultView.RowFilter = RowFilterExpression;
                    varDataGridView.DataSource = DataSource.DefaultView;
                }
                else
                {
                    varDataGridView.DataSource = DataSource;
                }
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message);
            }
        }

        /// <summary>
        /// Tự động Select dòng đầu tiên của DataGridView
        /// </summary>
        /// <param name="grd">DataGridView</param>
        public static void SetCurrentCellOfDataGridView(DataGridView grd)
        {
            if (grd.RowCount <= 0) return;
            string FirstVisibleColName = GetFirstColumnVisibleOfDataGridView(grd);
            if (FirstVisibleColName != "")
            {
                grd.CurrentCell = grd[FirstVisibleColName, 0];
            }
        }

        /// <summary>
        /// Trả về tên cột đầu tiên mà có tính chất hiển thị của cột là Visible
        /// </summary>
        /// <param name="grd">DataGridView</param>
        /// <returns></returns>
        public static string GetFirstColumnVisibleOfDataGridView(DataGridView grd)
        {
            foreach (DataGridViewColumn col in grd.Columns)
            {
                if (col.Visible) return col.Name;
            }
            return "";
        }

        /// <summary>
        /// Chưa dùng
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="EqualValue"></param>
        /// <param name="WantedValue"></param>
        public static void SetValue(ref object obj, string EqualValue, string WantedValue)
        {
            if (obj == null || obj == DBNull.Value)
            {
                obj = EqualValue;
            }
            if (obj.ToString() == EqualValue)
            {
                obj = WantedValue;
            }
        }

        /// <summary>
        /// Chưa dùng
        /// </summary>
        /// <param name="Compareobj"></param>
        /// <param name="EqualValue"></param>
        /// <param name="ObjectValueChanged"></param>
        /// <param name="ChangedValue"></param>
        public static void SetValue(object Compareobj, string EqualValue, ref object ObjectValueChanged,
                                    string ChangedValue)
        {
            if (Compareobj == null)
            {
                Compareobj = EqualValue;
            }
            if (Compareobj.ToString() == EqualValue)
            {
                ObjectValueChanged = ChangedValue;
            }
        }

        /// <summary>
        /// Copy giá trị từ bảng nguồn chứa dữ liệu SourceTable sang bảng đích DesTable. Điều kiện thực hiện thành công là 2 bảng này phải có cấu trúc giống nhau
        /// </summary>
        /// <param name="SourceTable">Bảng nguồn chứa dữ liệu</param>
        /// <param name="DesTable">Bảng đích nơi dữ liệu được chép sang</param>
        /// <param name="ClearAllRowsBeforeCopy">Có xóa dữ liệu bảng đích trước khi copy các bản ghi mới hay không?</param>
        public static void CopyData(DataTable SourceTable, DataTable DesTable, bool ClearAllRowsBeforeCopy)
        {
            try
            {
                if (SourceTable == null || DesTable == null)
                {
                    Utility.ShowMsg(
                        "Bạn chưa khởi tạo bảng nguồn và bảng đích! Một trong 2 bảng này đang có giá trị null.");
                    return;
                }
                if (ClearAllRowsBeforeCopy) DesTable.Rows.Clear();
                foreach (DataRow dr in SourceTable.Rows)
                {
                    DesTable.ImportRow(dr);
                }
                DesTable.AcceptChanges();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Copy giá trị từ Datarow nguồn chứa dữ liệu SourceRow sang Datarow đích DesTable. 
        /// </summary>
        /// <param name="SourceRow">Datarow nguồn chứa dữ liệu</param>
        /// <param name="DesRow">Datarow đích nơi dữ liệu được chép sang</param>
        public static void CopyData(DataRow SourceRow, ref DataRow DesRow)
        {
            try
            {
                if (SourceRow == null || DesRow == null)
                {
                    Utility.ShowMsg(
                        "Bạn chưa khởi tạo Datarow nguồn và Datarow đích! Một trong 2 Datarow này đang có giá trị null.");
                }
                foreach (DataColumn col in SourceRow.Table.Columns)
                {
                    if (DesRow.Table.Columns.Contains(col.ColumnName) &&
                        DesRow.Table.Columns[col.ColumnName].GetType().Equals(
                            SourceRow.Table.Columns[col.ColumnName].GetType()))
                        DesRow[col.ColumnName] = SourceRow[col.ColumnName];
                }
                DesRow.AcceptChanges();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Copy giá trị từ Datarow nguồn chứa dữ liệu SourceRow sang Datarow đích DesTable. 
        /// </summary>
        /// <param name="SourceRow">Datarow nguồn chứa dữ liệu</param>
        /// <param name="DesRow">Datarow đích nơi dữ liệu được chép sang</param>
        public static DataRow CopyData(DataRow SourceRow, DataTable DesTable)
        {
            DataRow DesRow = DesTable.NewRow();
            try
            {
                if (SourceRow == null || DesRow == null)
                {
                    Utility.ShowMsg(
                        "Bạn chưa khởi tạo Datarow nguồn và Datarow đích! Một trong 2 Datarow này đang có giá trị null.");
                    return null;
                }
                foreach (DataColumn col in SourceRow.Table.Columns)
                {
                    DesRow[col.ColumnName] = SourceRow[col.ColumnName];
                }
                return DesRow;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SourceTable"></param>
        /// <param name="fieldName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static DataRow GetDataRow(DataTable SourceTable, string[] fieldName, object[] Value)
        {
            try
            {
                foreach (DataRow dr in SourceTable.Rows)
                {
                    bool found = true;
                    for (int i = 0; i <= fieldName.Length - 1; i++)
                    {
                        found = found && dr[fieldName[i]].ToString() == Value[i].ToString();
                    }
                    if (found)
                        return dr;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SourceTable"></param>
        /// <param name="fieldName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static DataRow GetDataRow(DataTable SourceTable, string fieldName, object Value)
        {
            try
            {
                foreach (DataRow dr in SourceTable.Rows)
                {
                    if (dr[fieldName].ToString() == Value.ToString())
                        return dr;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Trả về một đối tượng Exception với Message truyền vào
        /// </summary>
        /// <param name="ExMsg"></param>
        /// <returns></returns>
        public static Exception NewException(string ExMsg)
        {
            return new Exception(ExMsg);
        }

        /// <summary>
        /// Lấy giá trị thực sự từ một giá trị cho trước. Thường dùng khi lấy giá trị từ TextBox
        /// Nếu cho phép trả về null (RevalCanbeNull==true) thì kiểm tra nếu chuỗi rỗng sẽ trả về giá trị null
        /// Nếu không cho phép trả về null thì sẽ
        /// CorrectStringValue: Nhằm thay thế dấu ' bằng dấu ''
        /// NotEmptyStrValue: Thay chuỗi rỗng bằng chuỗi cách " ". Tránh lỗi nếu CSDL là OracleDB
        /// </summary>
        /// <param name="OriginVal">Giá trị ban đầu</param>
        /// <param name="RevalCanbeNull">Có thể trả về null nếu giá trị là chuỗi rỗng hay không?</param>
        /// <returns></returns>
        public static string GetValue(string OriginVal, bool RevalCanbeNull)
        {
            if (RevalCanbeNull && String.IsNullOrEmpty(OriginVal.Trim()))
                return null;
            else
                return NotEmptyStrValue(CorrectStringValue(OriginVal), " ");
        }

        /// <summary>
        /// Chuyển đổi giá trị từ DBNull.Value thành null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object FromDBNullToNull(object value)
        {
            if (value == DBNull.Value) return null;
            else return value;
        }

        /// <summary>
        /// Chuyển đổi giá trị từ null thành DBNull.Value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object FromNullToDBNull(object value)
        {
            if (value == null) return DBNull.Value;
            else return value;
        }

        /// <summary>
        /// Chuyển giá trị chuỗi rỗng thành giá trị null
        /// </summary>
        /// <param name="OriginVal"></param>
        /// <returns></returns>
        public static string ChangeEmptyStringToNull(string OriginVal)
        {
            if (String.IsNullOrEmpty(OriginVal.Trim()))
                return null;
            else
                return OriginVal;
        }
        public static byte[] fromimagepath2byte(string filename)
        {
            try
            {
                using (System.Drawing.Image image = System.Drawing.Image.FromFile(filename))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();
                        // Convert byte[] to Base64 String
                        return imageBytes;

                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static string fromimagepath2base64(string filename)
        {
            try
            {
                using (System.Drawing.Image image = System.Drawing.Image.FromFile(filename))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();
                        // Convert byte[] to Base64 String
                        return Convert.ToBase64String(imageBytes);

                    }
                }
            }
            catch (Exception)
            {
                return "";
            }

        }
        public static System.Drawing.Image frombase642Img(string base64String)
        {
            try
            {
                byte[] fileBytes = Convert.FromBase64String(base64String);
                using (MemoryStream ms = new MemoryStream(fileBytes))
                {
                    System.Drawing.Image streamImage = System.Drawing.Image.FromStream(ms);
                    return streamImage;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
        public static byte[] frombase642byte(string base64String)
        {
            try
            {
                if (base64String.TrimStart().TrimEnd() == string.Empty) return null;
                byte[] fileBytes = Convert.FromBase64String(base64String);
                return fileBytes;
            }
            catch (Exception)
            {
                return null;
            }

        }
        /// <summary>
        /// Hàm kiểm tra và chuyển chuỗi từ Empty sang giá trị ReplaceVal. Hàm này dùng để tránh trường hợp truyền chuỗi rỗng lên Server vì Oracle ko cho phép
        /// </summary>
        /// <param name="OriginVal">Chuỗi ban đầu</param>
        /// <param name="ReplaceVal">Chuỗi thay thế</param>
        /// <returns></returns>
        public static string NotEmptyStrValue(string OriginVal, string ReplaceVal)
        {
            if (String.IsNullOrEmpty(OriginVal.Trim()))
                return ReplaceVal;
            else
                return OriginVal;
        }

        ///<summary>
        ///<para>Thay dấu ' thành dấu '' để tránh lỗi khi thực hiện DML trên DataBase</para>
        ///</summary>   
        /// <param name="obj"><para>chuỗi có chứa dấu ' <see cref="string"/></para></param>     
        ///<returns>Chuỗi đã được thay thế dấu ' bằng ''</returns>
        public static string CorrectStringValue(string obj)
        {
            return obj.Replace("'", "''");
        }

        /// <summary>
        /// Hiển thị hộp thoại OKCancel để hỏi người dùng khi cần câu trả lời là có hay không?
        /// </summary>
        /// <param name="msg">Câu hỏi</param>
        /// <param name="title">Tiêu đề</param>
        /// <returns></returns>
        public static bool AcceptQuestion(string msg, string title, bool YesNoOrOKCancel)
        {
            if (
                System.Windows.Forms.MessageBox.Show(msg, title,
                                                     YesNoOrOKCancel == true
                                                         ? System.Windows.Forms.MessageBoxButtons.YesNo
                                                         : System.Windows.Forms.MessageBoxButtons.OKCancel,
                                                     System.Windows.Forms.MessageBoxIcon.Question) ==
                (YesNoOrOKCancel == true ? System.Windows.Forms.DialogResult.Yes : System.Windows.Forms.DialogResult.OK))
                return true;
            else
                return false;
        }
       public static DataTable fromGridRow2DataTable(GridEX grd, bool getCheckedData)
        {
            DataTable dtData = new DataTable();
            //Set structure
            foreach (GridEXColumn col in grd.RootTable.Columns)
            {
                if (col.Visible)
                {
                    dtData.Columns.Add(new DataColumn(col.Key, typeof(string)));
                }
            }
            //Set value
            if (getCheckedData)
            {
                foreach (GridEXRow row in grd.GetCheckedRows())
                {
                    DataRow dr = dtData.NewRow();
                    foreach (GridEXColumn col in grd.RootTable.Columns)
                    {
                        if (col.Visible)
                        {
                            dr[col.Key] = Utility.sDbnull(row.Cells[col].Value, "UKN");
                        }
                    }
                    dtData.Rows.Add(dr);
                }
            }
            else
            {
                DataRow dr = dtData.NewRow();
                foreach (GridEXColumn col in grd.RootTable.Columns)
                {
                    if (col.Visible)
                    {
                        dr[col.Key] = Utility.sDbnull(grd.GetValue(col), "UKN");
                    }
                }
                dtData.Rows.Add(dr);
            }
            return dtData;
        }
        ///<summary>
        ///<para>Hiển thị hộp thoại thông báo</para>
        ///</summary>   
        /// <param name="Message"><para>Nội dung thông báo</para></param>     
        ///<returns>Hiển thị MessageBox có title="Thông báo"</returns>
        public static void ShowMsg(string Message)
        {
            System.Windows.Forms.MessageBox.Show(Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void ExportExcel(GridEX grdResult)
        {
            try
            {
                Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter();
                if (grdResult.RowCount <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu để xuất ra file Excel!");
                    return;
                }

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                    {
                        gridEXExporter1.GridEX = grdResult;
                        gridEXExporter1.Export(s);

                    }
                    Utility.ShowMsg("Xuất Excel thành công. Nhấn OK để mở file");
                    System.Diagnostics.Process.Start(saveFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        public static GridEXRow findthelastChild(GridEXRow parent)
        {
            try
            {
                if (parent == null ) return null;
                if (parent.GridEX != null && parent.GridEX.GetDataRows().Count() <= 0) return null;
                if (parent.RowType == RowType.Record) return parent;
                foreach (GridEXRow item in parent.GetChildRows())
                {
                    if (item.RowType == RowType.Record) return item;
                    else
                        return findthelastChild(item);
                }
                return null;
            }
            catch (Exception ex)
            {
                CatchException(ex);
                return null;
            }
           
        }
        ///<summary>
        ///<para>Hiển thị hộp thoại thông báo</para>
        ///</summary>   
        /// <param name="Message"><para>Nội dung thông báo</para></param>     
        /// <param name="Title"><para>Title của MessageBox</para></param>     
        ///<returns>Hiển thị MessageBox có title= <see cref="Title"/></returns>
        public static void ShowMsg(string Message, string Title)
        {
            System.Windows.Forms.MessageBox.Show(Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void Log(string FunctionName, string UserName, string LogInfo, newaction action = newaction.Search, string SourceName = "UI")
        {
            try
            {
                if (string.IsNullOrEmpty(FunctionName) || string.IsNullOrEmpty(SourceName))
                {
                    Utility.ShowMsg("Thông tin cơ bản của Log không đúng");
                    return;
                }

                StoredProcedure sp = SPs.LogInsert(SourceName, FunctionName, Utility.Int16Dbnull(action), UserName, LogInfo, Utility.GetIPAddress(), globalVariables.gv_strMacAddress, DateTime.Now, "", "");
                sp.Execute();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            
        }
        public static void Log(string FunctionName, string UserName, string LogInfo, newaction action, string SourceName ,string giatri_moi,string giatri_cu)
        {
            try
            {
                if (string.IsNullOrEmpty(FunctionName) || string.IsNullOrEmpty(SourceName))
                {
                    Utility.ShowMsg("Thông tin cơ bản của Log không đúng");
                    return;
                }
                StoredProcedure sp = SPs.LogInsert(SourceName, FunctionName, Utility.Int16Dbnull(action), UserName, LogInfo, Utility.GetIPAddress(), globalVariables.gv_strMacAddress, DateTime.Now, giatri_cu, giatri_moi);
                sp.Execute();
                //TLog log = new TLog();
                //log.LogSource = SourceName;
                //log.LogFunctionName = FunctionName;
                //log.LogActionId = Utility.Int16Dbnull(action);
                //log.LogUser = UserName;
                //log.LogInfo = LogInfo;
                //log.LogIp = Utility.GetIPAddress();
                //log.LogMacAddress = globalVariables.gv_strMacAddress;
                //log.LogTime = DateTime.Now;
                //log.Save();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }

        }
        public static bool IsValidCreatedUser(string created_user,string thuoc_quyenxoaphieulinhthuocnoitru)
        {
            if ( globalVariables.UserName== created_user || Utility.Coquyen(thuoc_quyenxoaphieulinhthuocnoitru)) return true;
            return false;
        }
        public static void CheckClientLic(string key)
        {
            SysClient client = new Select().From(SysClient.Schema).Where(SysClient.Columns.LicKey).IsEqualTo(key).ExecuteSingle<SysClient>();
            if (client == null)
            {
                client = new SysClient();
                client.LicKey = key;
                client.IsNew = true;
                client.Save();
            }
            else
            {
            }
        }

        /// <summary>
        /// hàm thực hiện message alert thông báo ra
        /// </summary>
        /// <param name="Message">nội dung message</param>
        /// <param name="Title">nội dung tiêu đề</param>
        /// <param name="Icon">Icon thực hiện</param>
        public static void ShowMsg(string Message, string Title, MessageBoxIcon Icon)
        {
            System.Windows.Forms.MessageBox.Show(Message, Title, MessageBoxButtons.OK, Icon);
        }
        public static string getTenNhanvien()
        {
            if (globalVariablesPrivate.objNhanvien != null) return globalVariablesPrivate.objNhanvien.TenNhanvien;
            return globalVariables.UserName;
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="NotAllowedEmptyString"></param>
        /// <returns></returns>
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

        /// <summary>
        /// hàm thực hiện trạng thái thanh toán
        /// </summary>
        /// <param name="PaymentStatus"></param>
        /// <returns></returns>
        public static string GetPaymentStatus(int PaymentStatus)
        {
            string paymentStatus = "Chưa thanh toán";
            switch (PaymentStatus)
            {
                case 0:
                    paymentStatus = "Chưa thanh toán";
                    break;
                case 1:
                    paymentStatus = "Đã thanh toán";
                    break;
            }
            return paymentStatus;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maluotkham">Mã lượt khám</param>
        /// <param name="tenchucnang">Tên chức năng đang đặt code kiểm tra</param>
        /// <param name="machucnang">TD=Tiếp đón;TK=Thăm khám;TT=Thanh toán</param>
        public static bool CheckLockObject(string maluotkham, string tenchucnang, string machucnang)
        {
            try
            {
                return true;
                SysLockObject objSysLockObject = new Select().From(SysLockObject.Schema)
                           .Where(SysLockObject.Columns.MaLuotkham).IsEqualTo(maluotkham).ExecuteSingle<SysLockObject>();
                if (objSysLockObject == null)
                {
                    SPs.KcbLockObject(maluotkham, globalVariablesPrivate.objNhanvien != null ? globalVariablesPrivate.objNhanvien.TenNhanvien : globalVariables.UserName, globalVariables.UserName, machucnang, tenchucnang, globalVariablesPrivate.objNhanvien == null ? "" : "").Execute();
                    return true;
                }
                else
                {
                    if (objSysLockObject.LockedBy != globalVariables.UserName)
                    {
                        Utility.ShowMsg(string.Format("Bệnh nhân bạn muốn khám đang được sử dụng bởi {0}({1}) tại chức năng {2} từ lúc {3}. Đề nghị liên hệ {4} để được giải quyết", objSysLockObject.LockedFullname, objSysLockObject.LockedBy, objSysLockObject.FunctionName, objSysLockObject.LockedTime.ToString("dd/MM/yyyy HH:mm:ss"), objSysLockObject.SDesc));
                        return false;
                    }
                    else if (objSysLockObject.FunctionCode != "TK")//Được khóa bởi chính người này-->Do được cấp sử dụng 2 chức năng có sử dụng cơ chế khóa đối tượng
                    {
                        Utility.ShowMsg(string.Format("Bệnh nhân bạn muốn khám đang được sử dụng bởi chính bạn tại chức năng {0} từ lúc {1}. Đề nghị bạn xem lại", objSysLockObject.FunctionName, objSysLockObject.LockedTime.ToString("dd/MM/yyyy HH:mm:ss")));
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return true;
            }
        }
        public static void FreeLockObject(string maluotkham)
        {
            try
            {
                return;
                SPs.KcbUnLockObject(maluotkham, globalVariables.UserName).Execute();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        public static DataTable ConvertGridEXToDataTable(GridEX grd, bool addOnlyVisibleColumn)
        {
            DataTable dt = new DataTable();
            foreach (GridEXColumn col in grd.RootTable.Columns)
            {
                if (addOnlyVisibleColumn && !col.Visible) continue;
                var dataColumn = new DataColumn();
                // Cột ẩn thì để readonly
                dataColumn.ReadOnly = !addOnlyVisibleColumn && !col.Visible;
                dataColumn.ColumnName = col.Caption;
                dt.Columns.Add(dataColumn);
            }

            foreach (GridEXRow row in grd.GetDataRows())
            {
                DataRow dRow = dt.NewRow();
                int i = 0;
                foreach (GridEXCell cell in row.Cells)
                {
                    if (addOnlyVisibleColumn && !cell.Column.Visible) continue;
                    dRow[i] = cell.Value;
                    i++;
                }
                dt.Rows.Add(dRow);
            }
            return dt;
        }
        ///<summary>
        ///<para>Xử lý một Object nếu là null thì trả về giá trị truyền vào</para>
        ///</summary>   
        /// <param name="obj"><para>Object truyền vào <see cref="object"/></para></param>     
        /// <param name="DefaultVal"><para>Giá trị mặc định sẽ trả về nếu obj=null <see cref="object"/></para></param>     
        ///<returns>Đối tượng chuỗi trả về từ Object. Nếu obj=null thì trả về DefaultVal</returns>
        public static string sDbnull(object obj, object DefaultVal)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return DefaultVal.ToString();
            }
            else
            {
                return DoTrim(obj.ToString());
            }
        }

        /// <summary>
        /// Hàm chuyển đổi một giá trị chuỗi nếu rỗng thành một giá trị mặc định
        /// </summary>
        /// <param name="obj">Giá trị chuỗi</param>
        /// <param name="DefaultVal">Giá trị mặc định trả về nếu obj=rỗng</param>
        /// <returns></returns>
        public static string EmptytoDefaultValue(string obj, string DefaultVal)
        {
            if (string.IsNullOrEmpty(obj.Trim()))
            {
                return DefaultVal;
            }
            else
            {
                return GetValue(obj, false);
            }
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
        public static void CapnhatDongia_bo(DataTable dtData, string dongia_columnName, DmucDoituongkcb objDoituongKCB)
        {
            try
            {
                foreach (DataRow dr in dtData.Rows)
                    dr[dongia_columnName] = Utility.DecimaltoDbnull(dr[dongia_columnName]) * (1 + Utility.DecimaltoDbnull(objDoituongKCB.MotaThem, 0) / 100);
            }
            catch (Exception ex)
            {
                ShowMsg(ex.ToString());
            }
        }
        public static void FocusAndSelectAll(TextBox txt)
        {
            txt.Focus();
            txt.SelectAll();
        }

        public static string GetYYYYMMDDHHMMSS(DateTime dtp)
        {
            return dtp.Year.ToString() + dtp.Month.ToString().PadLeft(2, '0') + dtp.Day.ToString().PadLeft(2, '0') +
                   dtp.Hour.ToString().PadLeft(2, '0') + dtp.Minute.ToString().PadLeft(2, '0') +
                   dtp.Second.ToString().PadLeft(2, '0');
        }
        public static string GetHHMMSS(DateTime dtp)
        {
            return dtp.Hour.ToString().PadLeft(2, '0') + dtp.Minute.ToString().PadLeft(2, '0') +
                   dtp.Second.ToString().PadLeft(2, '0');
        }
        /// <summary>
        /// hàm thực hiện việc lấy định dạng là yymmddhhmmss
        /// </summary>
        /// <param name="dtp"></param>
        /// <returns></returns>
        public static string GetYYMMDDHHMMSS(DateTime dtp)
        {
            //return dtp.Year.ToString().PadLeft(2, '0') + dtp.Month.ToString().PadLeft(2, '0') + dtp.Day.ToString().PadLeft(2, '0') + dtp.Hour.ToString().PadLeft(2, '0') + dtp.Minute.ToString().PadLeft(2, '0') + dtp.Second.ToString().PadLeft(2, '0');
            var result = dtp.ToString("yyMMddHHmmss");
            return result;
        }

        /// <summary>
        /// hàm thực hiện việc lấy định dạng ngày tháng,giờ, phút giấy theo định dang của truyền vào
        /// </summary>
        /// <param name="dtp"></param>
        /// <param name="sFormatDateTime"></param>
        /// <returns></returns>
        public static string GetFormatDateTime(DateTime dtp, string sFormatDateTime)
        {
            var result = "";
            try
            {
                result = dtp.ToString(sFormatDateTime);
            }
            catch (Exception)
            {
                result = dtp.ToString();
            }
            //return dtp.Year.ToString().PadLeft(2, '0') + dtp.Month.ToString().PadLeft(2, '0') + dtp.Day.ToString().PadLeft(2, '0') + dtp.Hour.ToString().PadLeft(2, '0') + dtp.Minute.ToString().PadLeft(2, '0') + dtp.Second.ToString().PadLeft(2, '0');

            return result;
        }

        ///<summary>
        ///<para>Tạo Header có gắn DML type và HISTrace</para>
        ///</summary>   
        /// <param name="Header"><para>Header chứa gói tin</para></param>     
        /// <param name="action"><para>Loại nghiệp vụ cần thao tác <see cref="action"/></para></param>     
        /// <param name="trace"><para>Đối tượng lưu vết <see cref="HISTrace"/></para></param>     
        ///<returns></returns>
        public static void CreateHeader(ref DataTable Header, action action, TraceInfor trace)
        {
            try
            {
                if ((Header == null))
                {
                    Utility.ShowMsg(
                        "Header chưa được khởi tạo. Đề nghị LTV khai báo Header=new Header() trước khi sử dụng thủ tục này!");
                    return;
                }
                if (trace == null)
                {
                    //Utility.ShowMsg("Bạn chưa khởi tạo đối tượng trace. Đề nghị bạn khởi tạo trace trước khi sử dụng hàm này!");
                    return;
                }

                //Read from Header Into Trace
                DataRow dr = Header.NewRow();
                dr["action"] = action;
                dr["ID"] = trace.pID;
                dr["BranchID"] = CorrectStringValue(sDbnull(trace.pBranchID));
                dr["UserName"] = CorrectStringValue(sDbnull(trace.pUserName));
                dr["CreatedDate"] = CorrectStringValue(sDbnull(trace.pCreatedDate));
                dr["IPAddress"] = CorrectStringValue(sDbnull(trace.pIPAddress));
                dr["ComputerName"] = CorrectStringValue(sDbnull(trace.ComputerName));
                dr["AccountName"] = CorrectStringValue(sDbnull(trace.AccountName));
                dr["DLLName"] = CorrectStringValue(sDbnull(trace.DLLName));
                dr["SubSystemName"] = CorrectStringValue(sDbnull(trace.pSubSystemName));
                dr["FunctionID"] = CorrectStringValue(sDbnull(trace.pFunctionID));
                dr["FunctionName"] = CorrectStringValue(sDbnull(trace.FunctionName));
                dr["TableName"] = CorrectStringValue(sDbnull(trace.pTableName));
                dr["Desc"] = CorrectStringValue(sDbnull(trace.pDesc));
                dr["LOT"] = Int32Dbnull(trace.pLO, 0);
                Header.Rows.Add(dr);
                Header.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi tạo Header từ Trace:\n" + ex.Message);
                return;
            }
        }

        ///<summary>
        ///<para>Tạo Header có gắn DML type và HISTrace</para>
        ///</summary>   
        /// <param name="Header"><para>Header chứa gói tin</para></param>     
        /// <param name="action"><para>Loại nghiệp vụ cần thao tác <see cref="action"/></para></param>     
        ///<returns></returns>
        public static void CreateHeader(ref DataTable Header, action action, string BranchID, string UserName,
                                        string CreatedDate, string IPAddress, string SubSystemName, string FunctionID)
        {
            if ((Header == null))
            {
                Utility.ShowMsg(
                    "Header chưa được khởi tạo. Đề nghị LTV khai báo Header=new Header() trước khi sử dụng thủ tục này!");
                return;
            }
            Header.Rows.Clear();
            DataRow newRow = Header.NewRow();
            newRow["action"] = action;
            newRow["BranchID"] = BranchID;
            newRow["UserName"] = UserName;
            newRow["CreatedDate"] = CreatedDate;
            newRow["IPAddress"] = IPAddress;
            newRow["SubSystemName"] = SubSystemName;
            newRow["FunctionID"] = FunctionID;
            Header.Rows.Add(newRow);
            Header.AcceptChanges();
        }

        ///<summary>
        ///<para>Tạo Header có gắn DML type và HISTrace</para>
        ///</summary>   
        /// <param name="Header"><para>Header chứa gói tin</para></param>     
        /// <param name="action"><para>Loại nghiệp vụ cần thao tác <see cref="action"/></para></param>     
        ///<returns></returns>
        public static void CreateHeader(ref DataTable Header, action action, string BranchID, string UserName,
                                        string CreatedDate, string IPAddress, string SubSystemName, string FunctionID,
                                        string TableName, string Desc, Int16 LO)
        {
            if ((Header == null))
            {
                Utility.ShowMsg(
                    "Header chưa được khởi tạo. Đề nghị LTV khai báo Header=new Header() trước khi sử dụng thủ tục này!");
                return;
            }
            Header.Rows.Clear();
            DataRow newRow = Header.NewRow();
            newRow["action"] = action;
            newRow["BranchID"] = BranchID;
            newRow["UserName"] = UserName;
            newRow["CreatedDate"] = CreatedDate;
            newRow["IPAddress"] = IPAddress;
            newRow["SubSystemName"] = SubSystemName;
            newRow["FunctionID"] = FunctionID;
            newRow["TableName"] = TableName;
            newRow["Desc"] = Desc;
            newRow["LO"] = LO;
            Header.Rows.Add(newRow);
            Header.AcceptChanges();
        }
        public static void CreateMergeFields(DataTable dt)
        {
            try
            {
                string fields = "";
                string values = "";
                foreach (DataColumn col in dt.Columns)
                {
                    fields += col.ColumnName + ",";
                    values += col.ColumnName + "_Value,";
                }
                if (fields.Length > 0) fields = fields.Substring(0, fields.Length - 1);
                if (values.Length > 0) values = values.Substring(0, values.Length - 1);
                string fileName = string.Format(@"{0}\{1}\{2}.txt", Application.StartupPath, "MergeFields", dt.TableName);
                using (StreamWriter _Writer = new StreamWriter(fileName))
                {
                    _Writer.WriteLine(fields);
                    _Writer.WriteLine(values);
                    _Writer.Flush();
                    _Writer.Close();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        public static void CreateMergeFields(DataSet ds)
        {
            try
            {
                foreach (DataTable dtData in ds.Tables)
                {
                    string fields = "";
                    string values = "";
                    foreach (DataColumn col in dtData.Columns)
                    {
                        fields += col.ColumnName + ",";
                        values += col.ColumnName + "_Value,";
                    }
                    if (fields.Length > 0) fields = fields.Substring(0, fields.Length - 1);
                    if (values.Length > 0) values = values.Substring(0, values.Length - 1);
                    string fileName = string.Format(@"{0}\{1}\{2}.txt", Application.StartupPath, "MergeFields", dtData.TableName);
                    using (StreamWriter _Writer = new StreamWriter(fileName))
                    {
                        _Writer.WriteLine(fields);
                        _Writer.WriteLine(values);
                        _Writer.Flush();
                        _Writer.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        ///<summary>
        ///<para>Gắn Header vào Entity để truyền lên BusinessLogic xử lý</para>
        ///</summary>   
        /// <param name="Entity"><para>Entity chứa thông tin nghiệp vụ</para></param>     
        /// <param name="Header"><para>Header chứa gói tin lưu vết và loại nghiệp vụ cần thao tác</para></param>     
        ///<returns></returns>
        public static void MergeHeader(ref DataSet Entity, DataTable Header)
        {
            try
            {
                if (Entity.Equals(null))
                {
                    Utility.ShowMsg("Bạn phải khởi tạo Entity trước khi thực hiện hàm này");
                    return;
                }
                if (Header.Equals(null))
                {
                    Utility.ShowMsg("Bạn phải khởi tạo Header trước khi thực hiện hàm này");
                    return;
                }
                if (Entity.Tables.Contains(Header.TableName))
                {
                    Entity.Tables.Remove(Header.TableName);
                    Entity.AcceptChanges();
                    Entity.Merge(Header);
                    Entity.AcceptChanges();
                }
                else
                {
                    Entity.Merge(Header);
                    Entity.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                ShowMsg("Lỗi khi gắn Header vào Entity:\n" + ex.Message);
            }
        }

        /// <summary>
        ///  Reset Entity về trạng thái ban đầu chưa chứa dữ liệu
        /// </summary>
        /// <param name="Entity">Entity</param>
        /// <param name="EntityName">Tên của Entity. Thường đặt trùng với tên bảng CSDL thao tác</param>
        public static void ResetEntity(ref DataTable Entity)
        {
            if (Entity != null)
            {
                Entity.Rows.Clear();
            }
        }

        /// <summary>
        ///  Reset Entity về trạng thái ban đầu ko chứa bảng nào
        /// </summary>
        /// <param name="Entity">Entity</param>
        /// <param name="EntityName">Tên của Entity. Thường đặt trùng với tên bảng CSDL thao tác</param>
        public static void ResetEntity(ref DataSet Entity)
        {
            if (Entity != null)
            {
                Entity.Tables.Clear();
            }
        }

        /// <summary>
        /// Hàm định dạng tiền trong HIS
        /// </summary>
        /// <returns></returns>
        public static string FormatCurrecy()
        {
            return "{0:#,##0.###}";// "{0:#,#.##}";// "{0:0,0.0}";
        }
        public static string FormatCurrecy_zero()
        {
            return  "{0:#,##0.###}";//"{0:0,0.0}";
        }

        /// <summary>
        /// Hàm thực hiện khi định dạng o textbox
        /// </summary>
        /// <param name="txtBox"></param>
        public static void FormatCurrencyHIS(TextBox txtBox)
        {
            if ((txtBox.Text != null) && (txtBox.Text.Trim() != ""))
                if (Utility.DecimaltoDbnull(txtBox.Text, 0) > 0)
                    txtBox.Text = String.Format(FormatCurrecy(), Convert.ToDouble(txtBox.Text));
                else
                    txtBox.Text = String.Format(FormatCurrecy_zero(), Convert.ToDouble(txtBox.Text));
            if (txtBox.Text != null) txtBox.Select(txtBox.Text.Length, 0);
        }
        public static string FormatCurrencyHIS(decimal value)
        {
            return String.Format(FormatCurrecy(), Convert.ToDouble(value));

        }
        public static string FormatCurrencyHIS(string value)
        {
            if ((value != null) && (value.Trim() != ""))
                if (Utility.DecimaltoDbnull(value, 0) > 0)
                    value = String.Format(FormatCurrecy(), Convert.ToDouble(value));
                else
                    value = String.Format(FormatCurrecy_zero(), Convert.ToDouble(value));
            return value;
        }
        public static void FormatCurrencyHISofWindow(TextBox txtBox)
        {
            if ((txtBox.Text != null) && (txtBox.Text.Trim() != ""))
                txtBox.Text = String.Format(FormatCurrecy(), Convert.ToDouble(txtBox.Text));
            if (txtBox.Text != null) txtBox.Select(txtBox.Text.Length, 0);
        }
        /// <summary>
        /// Hàm thực hiện khi định dạng o textbox của janus
        /// </summary>
        /// <param name="txtBox"></param>
        public static void FormatCurrencyHIS(Janus.Windows.GridEX.EditControls.EditBox txtBox)
        {
            if (!Utility.IsNumeric(txtBox.Text)) return;
            if ((txtBox.Text != null) && txtBox.Text.Trim() != "")
                if (Utility.DecimaltoDbnull(txtBox.Text, 0) > 0)
                    txtBox.Text = String.Format(FormatCurrecy(), Convert.ToDouble(txtBox.Text));
                else
                    txtBox.Text = String.Format(FormatCurrecy_zero(), Convert.ToDouble(txtBox.Text));
            if (txtBox.Text != null) txtBox.Select(txtBox.Text.Length, 0);
        }

        /// <summary>
        /// Dinh dang tien label cho his
        /// </summary>
        /// <param name="txtBox"></param>
        public static void FormatCurrencyHIS(Label lbl)
        {
            if ((lbl.Text != null) && (lbl.Text.Trim() != ""))
                lbl.Text = String.Format(FormatCurrecy(), Convert.ToDouble(lbl.Text));
        }

        /// <summary>
        /// HÀM THỰC HIỆN ĐỊNH DẠNG TIỀN CỦA ToolStripStatusLabel
        /// </summary>
        /// <param name="lbl">THAM SỐ TRUYỀN VÀO</param>
        public static void FormatCurrencyHIS(System.Windows.Forms.ToolStripStatusLabel lbl)
        {
            if ((lbl.Text != null) && (lbl.Text.Trim() != ""))
                lbl.Text = String.Format(FormatCurrecy(), Convert.ToDouble(lbl.Text));
        }

        public static void FormatCurrencyHIS(System.Windows.Forms.ToolStripLabel lbl)
        {
            if ((lbl.Text != null) && (lbl.Text.Trim() != ""))
                lbl.Text = String.Format(FormatCurrecy(), Convert.ToDouble(lbl.Text));
        }
        public static void FormatCurrencyHIS(Janus.Windows.GridEX.EditControls.MaskedEditBox txtBox)
        {
            if ((txtBox.Text != null) && (txtBox.Text.Trim() != ""))
                if (Utility.DecimaltoDbnull(txtBox.Text, 0) > 0)
                    txtBox.Text = String.Format(FormatCurrecy(), Convert.ToDouble(txtBox.Text));
                else
                    txtBox.Text = String.Format(FormatCurrecy_zero(), Convert.ToDouble(txtBox.Text));
            if (txtBox.Text != null) txtBox.Select(txtBox.Text.Length, 0);
        }
        ///<summary>
        ///<para>Lấy về loại nghiệp vụ từ Header</para>
        ///</summary>   
        /// <param name="DT"><para>Bảng Header trong gói tin truyền lên BusinessLogic xử lý <see cref="Header"/></para></param>     
        ///<returns>action <see cref="action"/></returns>
        public static action Getaction(DataTable DT)
        {
            try
            {
                if (DT.Rows.Count > 0)
                {
                    if ((DT.Rows[0]["action"].ToString() == "-1") || (DT.Rows[0]["action"] == null))
                    {
                        return action.Error;
                    }
                    else
                    {
                        switch (DT.Rows[0]["action"].ToString())
                        {
                            case "0":
                                return action.Insert;
                            case "1":
                                return action.Update;
                            case "2":
                                return action.Delete;
                            case "3":
                                return action.Select;
                            default:
                                return action.Error;
                        }
                    }
                }
                else
                {
                    return action.Error;
                }
            }
            catch (Exception ex)
            {
                return action.Error;
            }
        }

        //Chỉnh lại tên tham số trên SQL phải có chữ @
        public static string CorrectParameterNameOfSQLServer(string ParameterName)
        {
            if (ParameterName.Substring(0, 1) != "@")
            {
                return ParameterName = "@" + ParameterName;
            }
            else
            {
                return ParameterName;
            }
        }

        //Chỉnh lại tên tham số loại bỏ chữ @
        public static string CorrectParameterNameOfOracleServer(string ParameterName)
        {
            if (ParameterName.Substring(0, 1) == "@")
            {
                return ParameterName.Substring(1);
            }
            else
            {
                return ParameterName;
            }
        }

        ///<summary>
        ///<para>Khởi tạo MSDTC để có thể sử dụng Transaction Scope bảo đảm toàn vẹn dữ liệu trong một nghiệp vụ</para>
        ///</summary>       
        public static void StartMSDTC()
        {
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
            info.FileName = "sc.exe";
            info.Arguments = "start msdtc";
            info.CreateNoWindow = true;
            info.UseShellExecute = false;
            try
            {
                ExecuteProcess(info);
            }
            catch (Exception ex)
            {
               CatchException("Could not start MSDTC\n",ex);
            }
        }

        /// <summary>
        /// Thực thi tiến trình khởi tạo MSDTC
        /// </summary>
        /// <param name="startInfo"></param>
        private static void ExecuteProcess(System.Diagnostics.ProcessStartInfo startInfo)
        {
            using (System.Diagnostics.Process process = new System.Diagnostics.Process())
            {
                process.StartInfo = startInfo;
                process.Start();
                const int waitMilliSec = 60000;
                bool complete = process.WaitForExit(waitMilliSec);
                if (!complete)
                {
                    Utility.ShowMsg(
                        "Quá trình khởi tạo MSDTC không thành công. Các giao dịch của bạn không được theo dõi bởi Transaction và không được đảm bảo tính toàn vẹn dữ liệu");
                }
            }
        }

        /// <summary>
        /// Disable TextBox và đưa màu của TextBox=BackGroundColor
        /// </summary>
        /// <param name="t">Đối tượng TextBox</param>
        /// <param name="BackGroundColor">Màu của TextBox t lúc Disable</param>
        public static void DisabledTextBox(TextBox t, System.Drawing.Color BackGroundColor)
        {
            t.Enabled = false;
            t.BackColor = BackGroundColor;
        }

        /// <summary>
        /// Enabled TextBox và đưa màu của TextBox=BackGroundColor
        /// </summary>
        /// <param name="t">Đối tượng TextBox</param>
        /// <param name="BackGroundColor">Màu của TextBox t lúc Enabled</param>
        public static void EnabledTextBox(TextBox t, System.Drawing.Color BackGroundColor)
        {
            t.Enabled = true;
            t.BackColor = BackGroundColor;
        }

        /// <summary>
        /// Disabled TextBox và đưa màu của TextBox=System.Drawing.Color.WhiteSmoke
        /// </summary>
        /// <param name="t">Đối tượng TextBox</param>
        public static void DisabledTextBox(TextBox t)
        {
            t.Enabled = false;
            t.BackColor = System.Drawing.Color.WhiteSmoke;
        }

        /// <summary>
        /// hàm thực hiện disable MaskedEditBox của janus
        /// </summary>
        /// <param name="t"></param>
        public static void DisabledTextBox(Janus.Windows.GridEX.EditControls.MaskedEditBox t)
        {
            t.Enabled = false;
            t.BackColor = System.Drawing.Color.WhiteSmoke;
        }
        public static void DisabledTextBox(Janus.Windows.GridEX.EditControls.EditBox t)
        {
            t.Enabled = false;
            t.BackColor = System.Drawing.Color.WhiteSmoke;
        }

        /// <summary>
        /// Enabled TextBox và đưa màu của TextBox=System.Drawing.Color.White
        /// </summary>
        /// <param name="t">Đối tượng TextBox</param>
        public static void EnabledTextBox(TextBox t)
        {
            t.Enabled = true;
            t.BackColor = System.Drawing.Color.White;
        }
        public static void EnabledControl(Control t)
        {
            t.Enabled = true;
        }
        /// <summary>
        /// hàm thực hiện enable MaskedEditBox
        /// </summary>
        /// <param name="t">đối tượng truyền vào</param>
        public static void EnabledTextBox(Janus.Windows.GridEX.EditControls.MaskedEditBox t)
        {
            t.Enabled = true;
            t.BackColor = System.Drawing.Color.White;
        }
        public static void EnabledTextBox(Janus.Windows.GridEX.EditControls.EditBox t)
        {
            t.Enabled = true;
            t.BackColor = System.Drawing.Color.White;
        }
        /// <summary>
        /// Disabled TextBox và đưa màu của TextBox=System.Drawing.Color.WhiteSmoke
        /// </summary>
        /// <param name="t">Đối tượng TextBox</param>
        public static void DisabledComboBox(ComboBox t)
        {
            t.Enabled = false;
            t.BackColor = System.Drawing.Color.WhiteSmoke;
        }

        /// <summary>
        /// Enabled TextBox và đưa màu của TextBox=System.Drawing.Color.White
        /// </summary>
        /// <param name="t">Đối tượng TextBox</param>
        public static void EnabledComboBox(ComboBox t)
        {
            t.Enabled = true;
            t.BackColor = System.Drawing.Color.White;
        }
        public static void EnabledComboBox(Janus.Windows.EditControls.UIComboBox t)
        {
            t.Enabled = true;
            t.BackColor = System.Drawing.Color.White;
        }

        public static string GetDiscountType(int discountType_ID)
        {
            string disCountType_Name = "Khấu trừ %";

            if (discountType_ID == 1) disCountType_Name = "Khấu trừ số lượng";
            if (discountType_ID == 2) disCountType_Name = "Khấu trừ tiền";


            return disCountType_Name;
        }
         public static void RemoveRowfromDataTable(string Filter,DataTable dtData)
        {
            try
            {
                //Xóa khỏi DataTable để phản ánh lên lưới
                DataRow drDeleteRow = Utility.FetchOnebyCondition(dtData, Filter);
                if (drDeleteRow != null)
                {
                    dtData.Rows.Remove(drDeleteRow);
                    dtData.AcceptChanges();
                }
            }
            catch
            {
            }
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
        public static  string ConvertHexStrToUnicode(string hexString)
        {
            int length = hexString.Length;
            byte[] bytes = new byte[length / 2];

            for (int i = 0; i < length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return Encoding.UTF8.GetString(bytes);
        }
        public static object null2DBNull(object obj, object reval)
        {
            if (obj == null)
            {
                return reval;
            }
            else
            {
                return obj;
            }
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin máy in xét nghiệm
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultPrinter()
        {
            try
            {
                PrinterSettings settings = new PrinterSettings();
                foreach (string printer in PrinterSettings.InstalledPrinters)
                {
                    settings.PrinterName = printer;
                    if (settings.IsDefaultPrinter)
                        return printer;
                }
                return string.Empty;
            }
            catch(Exception ex)
            {
                Utility.CatchException("Bạn cần kiểm tra services PrintSpooler.ex và đặt service ở chế độ tự động(Automatic)\nNội dung lỗi:\n",ex);
                return string.Empty;
            }
        }
        public static void autoStartWServices(List<string> lstWServiceNames)
        {
            try
            {
                ServiceController[] arrService;

                arrService = ServiceController.GetServices();
                foreach (ServiceController ServiceCtrl in arrService)
                {
                    if (lstWServiceNames.Contains(ServiceCtrl.ServiceName) && (ServiceCtrl.Status == ServiceControllerStatus.Stopped || ServiceCtrl.Status == ServiceControllerStatus.Paused))
                        try
                        {
                            ServiceCtrl.Start();
                            return;
                        }
                        catch
                        {
                            return;
                        }
                }
            }
            catch
            {
            }
        }
       public static void CanhbaoBMI(DataTable dtSource,string BMI,Label lbl)
        {
            try
            {
                if (DoTrim( BMI )== "")
                {
                    lbl.Text = "";
                    return;
                }
                foreach (DataRow dr in dtSource.Rows)
                {
                    List<string> lst_rangeData = Utility.sDbnull(dr[DmucChung.Columns.MotaThem], "0-100").Split('-').ToList<string>();
                    if (Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(BMI)) >= Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(lst_rangeData[0])) && Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(BMI)) <= Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(lst_rangeData[1])))
                    {
                        if (Utility.sDbnull(dr[DmucChung.Columns.VietTat], "") == "BT")
                            Utility.SetMsg(lbl, Utility.sDbnull(dr[DmucChung.Columns.Ten]), false);
                        else
                            Utility.SetMsg(lbl, Utility.sDbnull(dr[DmucChung.Columns.Ten]), true);
                    }
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
       public static string CanhbaoBMI_getData(DataTable dtSource, string BMI,ref bool isNormal)
       {
           try
           {
               if (DoTrim(BMI) == "")
               {
                   isNormal = true;
                   return "";
               }
               foreach (DataRow dr in dtSource.Rows)
               {
                   List<string> lst_rangeData = Utility.sDbnull(dr[DmucChung.Columns.MotaThem], "0-100").Split('-').ToList<string>();
                   if (Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(BMI)) >= Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(lst_rangeData[0])) && Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(BMI)) <= Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(lst_rangeData[1])))
                   {
                       if (Utility.sDbnull(dr[DmucChung.Columns.VietTat], "") == "BT")
                       {
                           isNormal = true;
                           return Utility.sDbnull(dr[DmucChung.Columns.Ten]);
                       }
                       else
                       {
                           isNormal = false;
                          return Utility.sDbnull(dr[DmucChung.Columns.Ten]);
                       }
                   }
               }
               return "";
           }
           catch (Exception ex)
           {
               return ex.Message;
           }
       }
       public static void CanhbaoSPO2(DataTable dtSource, string SPO2, Label lbl)
       {
           try
           {
               if (DoTrim(SPO2) == "")
               {
                   lbl.Text = "";
                   return;
               }
               foreach (DataRow dr in dtSource.Rows)
               {
                   List<string> lst_rangeData = Utility.sDbnull(dr[DmucChung.Columns.MotaThem], "0-100").Split('-').ToList<string>();
                   if (Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(SPO2)) >= Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(lst_rangeData[0])) && Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(SPO2)) <= Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(lst_rangeData[1])))
                   {
                       if (Utility.sDbnull(dr[DmucChung.Columns.VietTat], "") == "BT")
                           Utility.SetMsg(lbl, Utility.sDbnull(dr[DmucChung.Columns.Ten]), false);
                       else
                           Utility.SetMsg(lbl, Utility.sDbnull(dr[DmucChung.Columns.Ten]), true);
                   }
               }
           }
           catch (Exception ex)
           {

               Utility.CatchException(ex);
           }
       }
       public static string CanhbaoSPO2_getData(DataTable dtSource, string SPO2, ref bool isNormal)
       {
           try
           {
               if (DoTrim(SPO2) == "")
               {
                   isNormal = true;
                   return "";
               }
               foreach (DataRow dr in dtSource.Rows)
               {
                   List<string> lst_rangeData = Utility.sDbnull(dr[DmucChung.Columns.MotaThem], "0-100").Split('-').ToList<string>();
                   if (Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(SPO2)) >= Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(lst_rangeData[0])) && Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(SPO2)) <= Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(lst_rangeData[1])))
                   {
                       if (Utility.sDbnull(dr[DmucChung.Columns.VietTat], "") == "BT")
                       {
                           isNormal = true;
                           return Utility.sDbnull(dr[DmucChung.Columns.Ten]);
                       }
                       else
                       {
                           isNormal = false;
                           return Utility.sDbnull(dr[DmucChung.Columns.Ten]);
                       }
                   }
               }
               return "";
           }
           catch (Exception ex)
           {
               return ex.Message;
           }
       }
        /// <summary>
        /// Chuyển đổi một đối tượng sang kiểu Int32. Xử lý nếu trường hợp là null thì trả về giá trị 0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Int32 Int32Dbnull(object obj)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// Hàm thực hiện lấy đối convert đối tượng thành kiểu decimal
        /// </summary>
        /// <param name="obj">Đối tượng cần convert</param>
        /// <returns></returns>
        public static decimal DecimaltoDbnull(object obj)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return 0m;
            }
            else
            {
                return Convert.ToDecimal( obj);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_time"></param>
        /// <returns></returns>
        public static int GetMinute(DateTime _time)
        {
            return _time.Hour * 60 + _time.Minute;
        }

        /// <summary>
        /// Lấy về chuỗi mô tả ngày tháng hiện tại. ví dụ: Ngày 01 tháng 01 năm 2010
        /// </summary>
        /// <returns></returns>
        public static string sGetCurrentDay()
        {
            return "Ngày " + Strings.Right("0" + System.DateTime.Now.Day, 2) + " tháng " +
                   Strings.Right("0" + System.DateTime.Now.Month, 2) + " năm " + System.DateTime.Now.Year;
        }

        /// <summary>
        /// Thiết lập giá trị cho một cột trên DataGridView
        /// </summary>
        /// <param name="grdList"></param>
        /// <param name="ColumnName"></param>
        /// <param name="RowIndex"></param>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static void SetValueForGridColumn(DataGridView grdList, string ColumnName, int RowIndex, object objValue)
        {
            grdList[ColumnName, RowIndex].Value = objValue;
        }

        /// <summary>
        /// Lấy về giá trị của một cột trên DataGridView
        /// </summary>
        /// <param name="grdList"></param>
        /// <param name="ColumnName"></param>
        /// <param name="RowIndex"></param>
        /// <returns></returns>
        public static string GetValueFromGridColumn(DataGridView grdList, string ColumnName, int RowIndex)
        {
            return Utility.sDbnull(grdList[ColumnName, RowIndex].Value);
        }

        /// <summary>
        /// hàm thực iên lấy giá trị của hàm thiện tại của janus
        /// </summary>
        /// <param name="grdList"></param>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public static string GetValueFromGridColumn(Janus.Windows.GridEX.GridEX grdList, string ColumnName)
        {
            try
            {
                if (grdList.CurrentRow != null && grdList.RootTable.Columns.Contains(ColumnName))
                    return Utility.sDbnull(grdList.CurrentRow.Cells[ColumnName].Value);
                else
                    return "-1";
            }
            catch (Exception)
            {

                return "null";
            }
            
        }
        public static string GetValueFromGridColumn(Janus.Windows.GridEX.GridEX grdList, string ColumnName, string defaultVal)
        {
            try
            {
                if (grdList.CurrentRow!=null && grdList.RootTable.Columns.Contains(ColumnName))
                    return Utility.sDbnull(grdList.CurrentRow.Cells[ColumnName].Value, defaultVal);
                else
                    return "-1";
            }
            catch (Exception)
            {

                return defaultVal;
            }

        }
        public static string GetValueFromGridColumn(Janus.Windows.GridEX.GridEXRow row, string ColumnName,string defaultVal)
        {
            try
            {
                if (row != null && row.GridEX.RootTable.Columns.Contains(ColumnName))
                    return Utility.sDbnull(row.Cells[ColumnName].Value, defaultVal);
                else
                    return "-1";
            }
            catch (Exception)
            {

                return defaultVal;
            }

        }
        public static string GetValueFromGridColumn(Janus.Windows.GridEX.GridEXRow row, string ColumnName)
        {
            try
            {
                if (row!=null && row.GridEX.RootTable.Columns.Contains(ColumnName))
                    return Utility.sDbnull(row.Cells[ColumnName].Value);
                else
                    return "-1";
            }
            catch (Exception)
            {

                return "null";
            }

        }
        public static  List<string> GetVisibleColumns(Janus.Windows.GridEX.GridEX grdList)
        {
            try
            {
                List<string> LstColumns = new List<string>();
                foreach (GridEXColumn col in grdList.RootTable.Columns)
                {
                    if (col.Visible) LstColumns.Add(col.DataMember);
                }
                return LstColumns;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public static byte getUserConfigValue(string config_name, byte defaultVal)
        {
             DataRow[] arrdr=globalVariables.gv_dtSysUserConfig.Select(string.Format("{0}='{1}' and {2}='{3}'",SysUserConfig.Columns.Username,globalVariables.UserName,SysUserConfig.Columns.ConfigName,config_name));
             if (arrdr.Length > 0)
             {
                 return Utility.ByteDbnull(arrdr[0][SysUserConfig.Columns.ConfigValue], 0);
             }
             return defaultVal;
        }
        public static void SaveUserConfig(string config_name, byte config_value)
        {
            try
            {
                DataRow[] arrdr=globalVariables.gv_dtSysUserConfig.Select(string.Format("{0}='{1}' and {2}='{3}'",SysUserConfig.Columns.Username,globalVariables.UserName,SysUserConfig.Columns.ConfigName,config_name));
                if (arrdr.Length > 0)
                {
                    SysUserConfig uc = SysUserConfig.FetchByID(Utility.Int64Dbnull(arrdr[0][SysUserConfig.Columns.Id]));
                    uc.ConfigValue = config_value;
                    uc.MarkOld();
                    uc.Save();
                    arrdr[0][SysUserConfig.Columns.ConfigValue] = config_value;
                    
                }
                else
                {
                    SysUserConfig uc = new SysUserConfig();
                    uc.ConfigName = config_name;
                    uc.Username = globalVariables.UserName;
                    uc.ConfigValue = config_value;
                    uc.IsNew = true;
                    uc.Save();
                    DataRow newitem = globalVariables.gv_dtSysUserConfig.NewRow();
                    newitem[SysUserConfig.Columns.Id] = uc.Id;
                    newitem[SysUserConfig.Columns.ConfigName] = config_name;
                    newitem[SysUserConfig.Columns.ConfigValue] = config_value;
                    newitem[SysUserConfig.Columns.Username] = globalVariables.UserName;
                    globalVariables.gv_dtSysUserConfig.Rows.Add(newitem);
                }
                globalVariables.gv_dtSysUserConfig.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        public static void ShowColumns(Janus.Windows.GridEX.GridEX grdList, List<string> LstColumns)
        {
            try
            {
                if (LstColumns == null || LstColumns.Count<=0) return;
                grdList.SuspendLayout();
                foreach (GridEXColumn col in grdList.RootTable.Columns)
                {
                    col.Visible = false;
                    if (LstColumns.Contains(col.DataMember))
                        col.Visible = true;
                }
                grdList.ResumeLayout();
            }
            catch (Exception)
            {

            }

        }

        /// <summary>
        /// Lấy về giá trị của một cột trên DataGridView
        /// </summary>
        /// <param name="grdList"></param>
        /// <param name="ColumnName"></param>
        /// <param name="RowIndex"></param>
        /// <returns></returns>
        public static object GetObjectValueFromGridColumn(DataGridView grdList, string ColumnName, int RowIndex)
        {
            return grdList[ColumnName, RowIndex].Value;
        }
        public static int GetSelectedIndex(MultiColumnCombo cbo, string _Value)
        {
            if (cbo.DataSource==null || ((DataTable)cbo.DataSource).Rows.Count <= 0) return -1;
            for (int i = 0; i <= cbo.DropDownList.RecordCount - 1; i++)
            {
                cbo.SelectedIndex = i;
                if (cbo.Value != null && cbo.Value.ToString() == _Value) return i;
            }
            return -1;
        }
        public static short GetWeekOfMonth(DateTime date)
        {
            DateTime beginningOfMonth = new DateTime(date.Year, date.Month, 1);

            while (date.Date.AddDays(1).DayOfWeek != CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                date = date.AddDays(1);

            return (short)(Math.Truncate((double)date.Subtract(beginningOfMonth).TotalDays / 7f) + 1);
        } 
        /// <summary>
        /// Trả về giá trị SelectedIndex của một ComboBox nếu SelectedValue=giá trị truyền vào
        /// </summary>
        /// <param name="cbo"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static int GetSelectedIndex(ComboBox cbo, string Value)
        {
            if (cbo.Items.Count <= 0) return -1;
            for (int i = 0; i <= cbo.Items.Count - 1; i++)
            {
                cbo.SelectedIndex = i;
                if (cbo.SelectedValue != null && cbo.SelectedValue.ToString() == Value) return i;
            }
            return -1;
        }
        public static int GetSelectedIndexbyStringVal(ComboBox cbo, string Value)
        {
            if (cbo.Items.Count <= 0) return -1;
            for (int i = 0; i <= cbo.Items.Count - 1; i++)
            {
                cbo.SelectedIndex = i;
                if (cbo.Text == Value) return i;
            }
            return -1;
        }
        public static int GetSelectedIndex(ComboBox cbo, string Value, int defaultval)
        {
            if (cbo.Items.Count <= 0) return -1;
            for (int i = 0; i <= cbo.Items.Count - 1; i++)
            {
                cbo.SelectedIndex = i;
                if ((cbo.SelectedValue != null && cbo.SelectedValue.ToString() == Value) || cbo.Text == Value) return i;
            }
            return defaultval;
        }

        public static int GetSelectedIndex(Janus.Windows.EditControls.UIComboBox cbo, string Value)
        {
            if (cbo.Items.Count <= 0) return -1;
            for (int i = 0; i <= cbo.Items.Count - 1; i++)
            {
                cbo.SelectedIndex = i;
                if (cbo.SelectedValue.ToString() == Value) return i;
            }
            return -1;
        }



        public static int GetSelectedIndex(ComboBox cbo, string Value, bool SourceAsDataView)
        {
            if (!SourceAsDataView) return GetSelectedIndex(cbo, Value);

            if (cbo.Items.Count <= 0) return -1;
            for (int i = 0; i <= cbo.Items.Count - 1; i++)
            {
                cbo.SelectedIndex = i;
                if (cbo.SelectedValue.ToString() == Value) return i;
            }
            return -1;
        }

        /// <summary>
        /// Trả về giá trị SelectedIndex của một ComboBox nếu Text =giá trị truyền vào.
        /// Trường hợp này Item.Text có dạng: ID+Seperator+Name. Ví dụ: 1-Việt Nam
        /// </summary>
        /// <param name="cbo">ComboBox</param>
        /// <param name="Value">Giá trị cần so sánh</param>
        /// <param name="Seperator">Dấu ngăn cách phần ID và Name</param>
        /// <returns></returns>
        public static int GetSelectedIndex(ComboBox cbo, string Value, char Seperator, int idx)
        {
            if (Value == "-1") return -1;
            if (cbo.Items.Count <= 0) return -1;
            for (int i = 0; i <= cbo.Items.Count - 1; i++)
            {
                if (cbo.Items[i].ToString().Split(Seperator)[idx].ToUpper() == Value.ToUpper()) return i;
            }
            return -1;
        }

        public static void CreateMessage(Label obj, string Text, bool Visible)
        {
            obj.Text = Text;
            obj.Visible = Visible;
        }

        /// <summary>
        /// Tìm và nhảy đến cột có tên ColumnName trên DataGridView nếu tồn tại cột có giá trị Value 
        /// </summary>
        /// <param name="grdList">Đối tượng lưới dữ liệu DataGridView</param>
        /// <param name="ColumnName">Tên cột chứa giá trị</param>
        /// <param name="Value">Giá trị cần tìm</param>
        public static void GotoNewRow(DataGridView grdList, string ColumnName, string Value)
        {
            for (int i = 0; i <= grdList.RowCount - 1; i++)
            {
                if (Utility.sDbnull(grdList[ColumnName, i].Value).Trim() == Value.Trim())
                {
                    foreach (DataGridViewColumn grdCol in grdList.Columns)
                    {
                        if (grdCol.Visible)
                        {
                            grdList.CurrentCell = grdList[grdCol.Name, i];
                            break;
                        }
                    }

                    break; // TODO: might not be correct. Was : Exit For
                }
            }
        }
        
        /// <summary>
        /// hàm thực hiện việc nhây thông tin của tới row cần trỏ
        /// </summary>
        /// <param name="grdList"></param>
        /// <param name="ColumnName"></param>
        /// <param name="Value"></param>
        public static void GotoNewRowJanus(GridEX grdList, string ColumnName, string value)
        {
            try
            {
                //var source = ((DataView) grdList.DataSource).Table;
                //DataRow loz = (from row in source.AsEnumerable()
                //               where row[ColumnName].ToString()==value
                //               select row).FirstOrDefault();
                //grdList.MoveTo(grdList.GetRow(loz)); 

                GridEXRow newRow = (from row in grdList.GetDataRows()
                                    where sDbnull(row.Cells[ColumnName].Value, "-1") == value
                                    select row).FirstOrDefault();
                if (newRow != null)
                    grdList.MoveTo(newRow);
                //foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdList.GetDataRows())
                //{
                //    if (gridExRow.RowType!=RowType.Record) return;
                //    if(gridExRow.Cells[ColumnName].Value.Equals(value))
                //    {
                //        grdList.MoveTo(gridExRow);
                //        break;
                //    }
                //}
            }
            catch (Exception ex)
            {
                return;
            }
        }

        /// <summary>
        /// So sánh giá trị của 2 ngày
        /// </summary>
        /// <param name="pv_sDate1"></param>
        /// <param name="pv_sDate2"></param>
        /// <returns>True nếu pv_sDate2>pv_sDate1. Ngược lại bằng false</returns>
        public static bool CompareDate(string pv_sDate1, string pv_sDate2)
        {
            if (System.DateTime.Parse(pv_sDate2, new System.Globalization.CultureInfo("vi-VN")) >=
                System.DateTime.Parse(pv_sDate1, new System.Globalization.CultureInfo("vi-VN")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// hàm thực hiện thông tin của so sánh 2 ngày hiện tại, nếu ngày 1 lớn hơn ngày thứ 2 trả lại là true, ngược lại false
        /// </summary>
        /// <param name="pv_sDate1"></param>
        /// <param name="pv_sDate2"></param>
        /// <returns></returns>
        public static bool CompareToDate(string pv_sDate1, string pv_sDate2)
        {
            return (DateTime.Compare(Convert.ToDateTime(pv_sDate1), Convert.ToDateTime(pv_sDate2)) > 0 ? true : false);
        }
        public static bool isValidNumber(int fromNum, int toNum)
        {
            return toNum >= fromNum;
        }
        public static bool isValidDate(DateTime fromDate, DateTime toDate)
        {
            return Int32Dbnull(Utility.GetYYMMDD(toDate)) >= Int32Dbnull(Utility.GetYYMMDD(fromDate));
        }
        public static bool CompareEqualToDate(string pv_sDate1, string pv_sDate2)
        {
            return (DateTime.Compare(Convert.ToDateTime(pv_sDate1), Convert.ToDateTime(pv_sDate2)) >= 0 ? true : false);
        }

        public static bool CompareDateUsingDateTimePicker(string pv_sDate1, string pv_sDate2)
        {
            DateTimePicker d1 = new DateTimePicker();
            DateTimePicker d2 = new DateTimePicker();

            try
            {
                d1.Text = pv_sDate1;
                d2.Text = pv_sDate2;
                if (d1.Value > d2.Value)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Trả về chuỗi date có dạng 01/01/2009 AM(hoặc PM)
        /// </summary>
        /// <param name="_Date"></param>
        /// <returns></returns>
        public static string ToShortDateString(System.DateTime _Date)
        {
            string AMPM = "";
            if (_Date.ToString().Contains("AM"))
            {
                AMPM = "AM";
            }
            else
            {
                AMPM = "PM";
            }
            return Strings.Right("0" + _Date.Day.ToString(), 2) + "/" + Strings.Right("0" + _Date.Month.ToString(), 2) +
                   "/" + _Date.Year.ToString() + " " + _Date.ToLongTimeString();
            //_Date.ToShortDateString & " " & _Date.ToLongTimeString '
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pv_sDate"></param>
        /// <returns></returns>
        public static System.DateTime ConvertDate(string pv_sDate)
        {
            return DateTime.ParseExact(pv_sDate, "dd/MM/yyyy", new System.Globalization.CultureInfo("vi-VN", false));
        }

        /// <summary>
        /// Đưa con trỏ của Form về trạng thái chờ đợi
        /// </summary>
        /// <param name="_me"></param>
        public static void WaitNow(System.Windows.Forms.Form _me)
        {
            _me.Cursor = Cursors.WaitCursor;
        }

        /// <summary>
        /// Đưa con trỏ của Form về trạng thái mặc định để có thể làm việc bình thường
        /// </summary>
        /// <param name="_me"></param>
        public static void DefaultNow(System.Windows.Forms.Form _me)
        {
            _me.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Lấy về chuỗi năm tháng ngày có dạng YYYYMMDD. Ví dụ ngày 02/01/2010 sẽ trả về 20100102
        /// </summary>
        /// <param name="tDate"></param>
        /// <returns></returns>
        public static string GetYYYYMMDD(System.DateTime tDate)
        {
            return tDate.Year.ToString() + Strings.Right("0" + tDate.Month.ToString(), 2) +
                   Strings.Right("0" + tDate.Day.ToString(), 2);
        }
        public static string GetYYYYMM(System.DateTime tDate)
        {
            return tDate.Year.ToString() + Strings.Right("0" + tDate.Month.ToString(), 2);
        }
        public static string GetYYYYMMDD(System.DateTime? tDate,DateTime defaultVal)
        {
            if(tDate.Equals(null) || tDate.Equals(DBNull.Value))
                return defaultVal.Year.ToString() + Strings.Right("0" + defaultVal.Month.ToString(), 2) +
                   Strings.Right("0" + defaultVal.Day.ToString(), 2);
            else
            return tDate.Value.Year.ToString() + Strings.Right("0" + tDate.Value.Month.ToString(), 2) +
                   Strings.Right("0" + tDate.Value.Day.ToString(), 2);
        }
        public static string GetYYYYMMDD(string sDate)
        {
            string[] arrValues = sDate.Split('/');
            return arrValues[2] + Strings.Right("0" + arrValues[1], 2) +
                   Strings.Right("0" + arrValues[2], 2);
        }
        public static DateTime FromYYYYMMDD2Datetime(string YYYYMMDD)
        {
            return new DateTime(Utility.Int32Dbnull(YYYYMMDD.Substring(0, 4), 1900), Utility.Int32Dbnull(YYYYMMDD.Substring(4, 2), 1), Utility.Int32Dbnull(YYYYMMDD.Substring(6, 2), 1));
        }
        public static bool isNullValue(object obj)
        {
            return obj.Equals(DBNull.Value);
        }
        public static string FormatNumberToString(int iNumber, string Format)
        {
            return Strings.Right(Format + iNumber.ToString(), Format.Length);
        }
        public static void focusCell(GridEX grdList,string ColName)
        {
            try
            {
                grdList.Focus();
                grdList.MoveFirst();
                Janus.Windows.GridEX.GridEXColumn gridExColumn = grdList.RootTable.Columns[ColName];
                grdList.Col = gridExColumn.Position;
            }
            catch
            { }
        }
        public static void focusCellofCurrentRow(GridEX grdList, string ColName)
        {
            try
            {
                grdList.Focus();
                Janus.Windows.GridEX.GridEXColumn gridExColumn = grdList.RootTable.Columns[ColName];
                grdList.Col = gridExColumn.Position;
            }
            catch
            { }
        }
        public static void Showhelps(string modulename, string sformName)
        {
            try
            {
                SysFunction _objSysFunction = new Select().From(SysFunction.Schema)
                    .Where(SysFunction.Columns.SDLLname).IsEqualTo(modulename)
                    .And(SysFunction.Columns.SFormName).IsEqualTo(sformName).ExecuteSingle<SysFunction>();
                if (_objSysFunction != null)
                {
                    OpenHelpFile(string.Format(@"{0}\{1}\{2}",Application.StartupPath,"Help", _objSysFunction.SDesc));
                }
            }
            catch (Exception ex)
            {
                
                
            }
        }
        public static string AutoFillMaBA(string _value)
        {
            if (Utility.DoTrim(_value).Length == 0) return _value;
            if (!Utility.IsNumeric(_value))
            {
                Utility.ShowMsg("Mã Bệnh án phải là chữ số");
                return _value;
            }
            if (Utility.DoTrim(_value).Length <= 6)
                return Utility.GetYY(DateTime.Now) +
                                   Utility.FormatNumberToString(Utility.Int32Dbnull(_value, 0), "000000");
            return _value;
        }
        public static string AutoFullPatientCode(string _value)
        {
            if (Utility.DoTrim(_value).Length == 0) return _value;
            if (!Utility.IsNumeric(_value))
            {
                Utility.ShowMsg("Mã lượt khám phải là chữ số");
                return _value;
            }
            if (Utility.DoTrim(_value).Length <= 6)
                return Utility.GetYY(DateTime.Now) +
                                   Utility.FormatNumberToString(Utility.Int32Dbnull(_value, 0), "000000");
            return _value;
        }
        /// <summary>
        /// Lấy về chuỗi năm tháng ngày có dạng YYMMDD. Ví dụ ngày 02/01/2010 sẽ trả về 100102
        /// </summary>
        /// <param name="tDate"></param>
        /// <returns></returns>
        public static string GetYYMMDD(System.DateTime tDate)
        {
            return Strings.Right(tDate.Year.ToString(), 2) + Strings.Right("0" + tDate.Month.ToString(), 2) +
                   Strings.Right("0" + tDate.Day.ToString(), 2);
        }
        public static string GetYY(System.DateTime tDate)
        {
            return Strings.Right(tDate.Year.ToString(), 2);
        }
        /// <summary>
        /// Lấy về chuỗi giờ phút có dạng HHMM: ví dụ 9h30 ' thì sẽ trả về 0930
        /// </summary>
        /// <param name="tDate">Giờ hiện tại</param>
        /// <returns></returns>
        public static string GetHHMM(System.DateTime tDate)
        {
            return Strings.Right("0" + tDate.Hour.ToString(), 2) + ":" + Strings.Right("0" + tDate.Minute.ToString(), 2);
        }
        public static string CapitalizeWords(string value)
        {
            if (globalVariables.CHARACTERCASING == 1) return value;
            if (value == null)
                return "";
            if (value.Length == 0)
                return value;

            var result = new StringBuilder(value);
            result[0] = char.ToUpper(result[0]);
            for (int i = 1; i < result.Length; ++i)
            {
                if (char.IsWhiteSpace(result[i - 1]))
                    result[i] = char.ToUpper(result[i]);
                else
                    result[i] = char.ToLower(result[i]);
            }
            return result.ToString();
        }
        public static string CapitalizeFirstLetters(string value)
        {
            if (value == null)
                return "";
            if (value.Length == 0)
                return value;

            var result = new StringBuilder(value);
            result[0] = char.ToUpper(result[0]);
            for (int i = 1; i < result.Length; ++i)
            {
                if (char.IsWhiteSpace(result[i - 1]))
                    result[i] = char.ToUpper(result[i]);
                else
                    result[i] = char.ToLower(result[i]);
            }
            return result.ToString();
        }
        public static byte[] bytGetImage(string pv_sImgPath)
        {
            try
            {
                FileStream fs = null;
                if (File.Exists(pv_sImgPath))
                {
                    fs = new FileStream(pv_sImgPath, FileMode.Open);
                }
                else
                {
                    return null;
                }
                BinaryReader rd = new BinaryReader(fs);
                byte[] arrData = rd.ReadBytes((int)fs.Length);
                fs.Flush();
                fs.Close();
                return arrData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Tìm đối tượng DataGridView có tên pv_sName trong Control objMain
        /// </summary>
        /// <param name="objMain">Controls cần tìm DataGridView có tên pv_sName</param>
        /// <param name="pv_sName">Tên DataGridView cần tìm</param>
        private static void objGetCurrentDataGridView(Control objMain, string pv_sName)
        {
            foreach (Control ctr in objMain.Controls)
            {
                if (ctr.Name == pv_sName)
                {
                    globalVariables.CurrDtGridView = (DataGridView)ctr;
                }
                else
                {
                    objGetCurrentDataGridView(ctr, pv_sName);
                }
            }
        }

        /// <summary>
        /// HÀM THỰC HIỆN LOAD PROCESSBAR
        /// </summary>
        /// <param name="prgb">ĐỐI TƯỢNG TRUYỀN VÀO</param>
        /// <param name="Max">SỐ LƯỢNG MAX BẢN GHI</param>
        /// <param name="visi">THAM SỐ ẨN HIỆN</param>
        public static void ResetProgressBar(ProgressBar prgb, int Max, bool visi)
        {
            prgb.Step = 1;
            prgb.Visible = visi;
            prgb.Maximum = Max;
            prgb.Minimum = 0;
            prgb.Value = 0;
        }

        public static void ResetProgressBarJanus(Janus.Windows.EditControls.UIProgressBar prgb, int Max, bool visi)
        {
            // prgb.Step = 1;
            prgb.Visible = visi;
            prgb.Maximum = Max;
            prgb.Minimum = 0;
            prgb.Value = 0;
        }

        /// <summary>
        /// hàm thực hiện việc thêm số thứ tự với tên columne truyền vào
        /// </summary>
        /// <param name="dataTable">datatable muốn chèn thêm</param>
        /// <param name="ColumnName">tên trường muốn chèn vào</param>
        public static void UpdateOrderInDataTable(ref DataTable dataTable, string ColumnName)
        {
            int idx = 1;
            if (!dataTable.Columns.Contains(ColumnName)) dataTable.Columns.Add(ColumnName, typeof(int));
            foreach (DataRow drv in dataTable.Rows)
            {
                drv[ColumnName] = idx;
                idx++;
            }
            dataTable.AcceptChanges();
        }

        /// <summary>
        /// hàm thực hiện việc cho số tự tăng 
        /// mặc định column name là STT
        /// </summary>
        /// <param name="dataTable">datatbel muốn thêm vào</param>
        public static void UpdateOrderInDataTable(ref DataTable dataTable)
        {
            int idx = 1;
            if (!dataTable.Columns.Contains("STT")) dataTable.Columns.Add("STT", typeof(int));
            foreach (DataRow drv in dataTable.Rows)
            {
                drv["STT"] = idx;
                idx++;
            }
            dataTable.AcceptChanges();
        }

        public static void ResetProgressBarHavePercent(ProgressBar prgb, int Max, bool visi)
        {
            prgb.Step = 1;
            prgb.Visible = visi;
            prgb.Maximum = Max;
            prgb.Minimum = 0;
            int percent = (int)(((double)prgb.Value / (double)prgb.Maximum) * 100);
            prgb.CreateGraphics().DrawString("Đang cập nhập :" + percent.ToString() + "%",
                                             new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black,
                                             new PointF(prgb.Width / 2 - 10, prgb.Height / 2 - 7));
            prgb.Value = 0;
        }

        /// <summary>
        /// HÀM THỰC HIỆN LOAD THÔNG TIN CỦA PROCESSBAR CỦA PHẦN TOOLSTRIPpROCESSBAR
        /// </summary>
        /// <param name="prgb">ĐỐI TƯỢNG TRUYỀN VÀO</param>
        /// <param name="Max">SỐ LƯỢNG MAX BẢN GHI</param>
        /// <param name="visi">THAM SỐ ẨN HIỆN</param>
        public static void ResetToolTipProgressBar(System.Windows.Forms.ToolStripProgressBar prgb, int Max, bool visi)
        {
            prgb.Step = 1;
            prgb.Visible = visi;
            prgb.Maximum = Max;
            prgb.Minimum = 0;
            prgb.Value = 0;
        }

        /// <summary>
        /// Chuyển đổi Sex từ M sang Nam và còn lại sang Nữ
        /// </summary>
        /// <param name="Sex">có giá trị M hoặc F hoặc bất kỳ giá trị nào khác</param>
        /// <returns></returns>
        public static string TranslateSex(string Sex)
        {
            if (Sex.ToUpper() == "M")
            {
                return "Nam";
            }
            else
            {
                return "Nữ";
            }
        }

        /// <summary>
        /// HÀM THỰC HIỆN LOAD THÔNG TIN GIỚI TÍNH
        /// </summary>
        /// <param name="PatientSex"></param>
        /// <returns></returns>
        public static string TranslateSex(int PatientSex)
        {
            if (PatientSex.ToString().ToUpper() == "0")
            {
                return "Nam";
            }
            else
            {
                return "Nữ";
            }
        }

        /// <summary>
        /// Lấy về giá trị ID từ một Text của ComboBox. Nếu chọn Text là "Tất cả" trả về giá trị -1. Nếu không trả về giá trị cbo.Text.Split('-')[0]
        /// Chú ý: Chỉ nên dùng với các ComboBox có chứa các Items có Text là "Tất cả" hoặc có dạng Chuỗi 1+"-"+Chuỗi 2
        /// </summary>
        /// <param name="cbo"></param>
        /// <returns></returns>
        public static int intGetCoboVal(ComboBox cbo)
        {
            if (cbo.Items.Count <= 0) return -1;
            if (cbo.Text.Trim().ToUpper() == "Tất cả".ToUpper())
            {
                return -1;
            }
            else
            {
                return Utility.Int32Dbnull(cbo.Text.Split('-')[0]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="FieldName"></param>
        /// <param name="FieldValue"></param>
        public static void SetField(Type t, string FieldName, object FieldValue)
        {
            try
            {
                t.InvokeMember(FieldName.Trim(), BindingFlags.Default | BindingFlags.SetField, null, t,
                               new object[] { FieldValue });
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Biến giá trị từ một mảng thành một chuỗi cách nhau bởi dấu ,
        /// </summary>
        /// <param name="ArrDelegateUser"></param>
        /// <returns></returns>
        public static string FromArrayListToString(ArrayList ArrDelegateUser)
        {
            string DelegateUser = "";
            for (int i = 0; i <= ArrDelegateUser.Count - 1; i++)
            {
                DelegateUser += ArrDelegateUser[i].ToString() + ",";
            }
            if (DelegateUser != "") DelegateUser = DelegateUser.Substring(0, DelegateUser.Length - 1);
            return DelegateUser;
        }

        /// <summary>
        /// ShowMessage dựa vào MessageID từ file XML
        /// </summary>
        /// <param name="MsgID">Mã Message</param>
        /// <returns></returns>
        public static void ShowMsgBox(string MsgCode)
        {
            if (!System.IO.File.Exists(Application.StartupPath + @"\HISLinkMessage.XML"))
            {
                ShowMsg(
                    "Chưa tồn tại file HISLinkMessage.XML để hiển thị các thông báo trong hệ thống. Đề nghị liên hệ với Admin để có file này!");
                return;
            }
            DataSet dsMsg = new DataSet();
            dsMsg.ReadXml(Application.StartupPath + @"\HISLinkMessage.XML");
            if (dsMsg == null || dsMsg.Tables.Count <= 0 || dsMsg.Tables[0].Rows.Count <= 0)
            {
                ShowMsg("Nội dung Message chưa có gì. Đề nghị liên hệ với Admin để có file HISLinkMessage.XML");
                return;
            }
            DataRow[] dr = dsMsg.Tables[0].Select("Msg_Code='" + MsgCode + "'");
            if (dr.GetLength(0) <= 0)
            {
                ShowMsg("Không tồn tại Message có mã: " + MsgCode.ToString() + "\n.Đề nghị bạn xem lại");
                return;
            }
            string VnMsg_Content = sDbnull(dr[0]["VnMsg_Content"], "Không có nội dung Message. Liên hệ với Admin");
            string EnMsg_Content = sDbnull(dr[0]["EnMsg_Content"],
                                           "There is no Message contents. Plz contact with your Admin");
            string MsgType = sDbnull(dr[0]["Msg_Type"], "Infor");
            string VnMsg_Title = sDbnull(dr[0]["VnMsg_Title"], "");
            string EnMsg_Title = sDbnull(dr[0]["EnMsg_Title"], "");
            switch (globalVariables.DisplayLanguage)
            {
                case "VN":
                    System.Windows.Forms.MessageBox.Show(VnMsg_Content, GetTitle(MsgType, VnMsg_Title),
                                                         GetMsgButton(MsgType), GetMsgIcon(MsgType));
                    break;
                case "EN":
                    System.Windows.Forms.MessageBox.Show(EnMsg_Content, GetTitle(MsgType, EnMsg_Title),
                                                         GetMsgButton(MsgType), GetMsgIcon(MsgType));
                    break;
                default:
                    ShowMsg(
                        "Không xác định được loại ngôn ngữ hiển thị trên giao diện của bạn. Đề nghị liên hệ với đơn vị cung cấp phần mềm để được giải đáp");
                    break;
            }
        }

        /// <summary>
        /// Hiển thị Message dưới dạng hỏi là YesNo hoặc OKCancel dựa vào MessageID từ file XML
        /// </summary>
        /// <param name="MsgID">Mã Message</param>
        /// <returns>True nếu chọn Yes hoặc OK. False nếu chọn No hoặc Cancel</returns>
        public static bool AcceptQuestion(string MsgCode)
        {
            if (!System.IO.File.Exists(Application.StartupPath + @"\HISLinkMessage.XML"))
            {
                ShowMsg(
                    "Chưa tồn tại file HISLinkMessage.XML để hiển thị các thông báo trong hệ thống. Đề nghị liên hệ với Admin để có file này!");
                return false;
            }
            DataSet dsMsg = new DataSet();
            dsMsg.ReadXml(Application.StartupPath + @"\HISLinkMessage.XML");
            if (dsMsg == null || dsMsg.Tables.Count <= 0 || dsMsg.Tables[0].Rows.Count <= 0)
            {
                ShowMsg("Nội dung Message chưa có gì. Đề nghị liên hệ với Admin để có file Message.XML");
                return false;
            }
            DataRow[] dr = dsMsg.Tables[0].Select("Msg_Code='" + MsgCode + "'");
            if (dr.GetLength(0) <= 0)
            {
                ShowMsg("Không tồn tại Message có mã: " + MsgCode.ToString() + "\n.Đề nghị bạn xem lại");
                return false;
            }
            string VnMsg_Content = sDbnull(dr[0]["VnMsg_Content"], "Không có nội dung Message. Liên hệ với Admin");
            string EnMsg_Content = sDbnull(dr[0]["EnMsg_Content"],
                                           "There is no Message contents. Plz contact with Admin");
            string MsgType = sDbnull(dr[0]["Msg_Type"], "Infor");
            string VnMsg_Title = sDbnull(dr[0]["VnMsg_Title"], "");
            string EnMsg_Title = sDbnull(dr[0]["EnMsg_Title"], "");
            DialogResult result;
            switch (globalVariables.DisplayLanguage)
            {
                case "VN":
                    result = System.Windows.Forms.MessageBox.Show(VnMsg_Content, GetTitle(MsgType, VnMsg_Title),
                                                                  GetMsgButton(MsgType), GetMsgIcon(MsgType));
                    return (result == DialogResult.OK || result == DialogResult.Yes) ? true : false;
                    break;
                case "EN":
                    result = System.Windows.Forms.MessageBox.Show(EnMsg_Content, GetTitle(MsgType, EnMsg_Title),
                                                                  GetMsgButton(MsgType), GetMsgIcon(MsgType));
                    return (result == DialogResult.OK || result == DialogResult.Yes) ? true : false;
                    break;
                default:
                    ShowMsg(
                        "Không xác định được loại ngôn ngữ hiển thị trên giao diện của bạn. ");
                    return false;
            }
        }

        private static string GetTitle(string MsgType, string Title)
        {
            if (Title.Trim() != "") return Title;
            switch (globalVariables.DisplayLanguage)
            {
                case "VN":
                    switch (MsgType.ToUpper())
                    {
                        case "INFOR":
                            return "Thông báo";
                        case "WARN":
                            return "Cảnh báo";
                        case "YESN":
                            return "Xác nhận";
                        case "OKC":
                            return "Xác nhận";
                        default:
                            return "Thông báo";
                    }
                    break;
                case "EN":
                    switch (MsgType.ToUpper())
                    {
                        case "INFOR":
                            return "Information";
                        case "WARN":
                            return "Warning";
                        case "YESN":
                            return "Confirm";
                        case "OKC":
                            return "Confirm";
                        default:
                            return "Information";
                    }
                default:
                    return "";
            }
        }

        private static MessageBoxIcon GetMsgIcon(string MsgType)
        {
            switch (MsgType.ToUpper())
            {
                case "ERR":
                    return MessageBoxIcon.Error;
                case "INFOR":
                    return MessageBoxIcon.Information;
                case "WARN":
                    return MessageBoxIcon.Warning;
                case "YESN":
                    return MessageBoxIcon.Question;
                case "OKC":
                    return MessageBoxIcon.Question;
                default:
                    return MessageBoxIcon.Information;
            }
        }

        private static MessageBoxButtons GetMsgButton(string MsgType)
        {
            switch (MsgType.ToUpper())
            {
                case "ERR":
                    return MessageBoxButtons.OK;
                case "INFOR":
                    return MessageBoxButtons.OK;
                case "WARN":
                    return MessageBoxButtons.OK;
                case "YESN":
                    return MessageBoxButtons.YesNo;
                case "OKC":
                    return MessageBoxButtons.OKCancel;
                default:
                    return MessageBoxButtons.OK;
            }
        }

        private static DialogResult GetMsgDialogResult(string MsgType)
        {
            switch (MsgType.ToUpper())
            {
                case "YESN":
                    return DialogResult.Yes;
                case "OKC":
                    return DialogResult.OK;
                default:
                    return DialogResult.OK;
            }
        }

        /// <summary>
        /// Hàm trả về điều kiện sử dụng dữ liệu theo DelegateUser có dạng AND (CreatedBy='CUONGDV' OR CreatedBy='HUNGND')
        /// </summary>
        /// <param name="sDelegateUser">Chuỗi các MainUser cách nhau bởi dấu ,: CuongDV,HungND</param>
        /// <returns></returns>
        public static string sGetConditionFromDelegate(string sDelegateUser)
        {
            string Reval = "";
            string[] arrDelegateUser = sDelegateUser.Split(',');
            for (int i = 0; i <= arrDelegateUser.Length - 1; i++)
            {
                Reval += " upper(CreatedBy)=N'" + arrDelegateUser[i].ToString().ToUpper() + "' OR ";
            }
            if (Reval != "")
            {
                //Chuỗi đang có dạng CreatedBy='CUONGDV' OR CreatedBy='HUNGND' OR 
                Reval = Reval.Substring(0, Reval.LastIndexOf("OR "));
                //Chuỗi Đã có dạng CreatedBy='CUONGDV' OR CreatedBy='HUNGND'
                Reval = " AND ( " + Reval + " ) ";
            }
            return Reval;
        }

        /// <summary>
        /// Hàm trả về điều kiện sử dụng dữ liệu theo DelegateUser có dạng AND (CreatedBy='CUONGDV' OR CreatedBy='HUNGND')
        /// </summary>
        /// <param name="ArrDelegateUser">Mảng chứa các DelegateUser</param>
        /// <returns></returns>
        public static string sGetConditionFromDelegate(ArrayList ArrDelegateUser)
        {
            string Reval = "";

            for (int i = 0; i <= ArrDelegateUser.Count - 1; i++)
            {
                Reval += " upper(CreatedBy)=N'" + ArrDelegateUser[i].ToString().ToUpper() + "' OR ";
            }
            if (Reval != "")
            {
                //Chuỗi đang có dạng CreatedBy='CUONGDV' OR CreatedBy='HUNGND' OR 
                Reval = Reval.Substring(0, Reval.LastIndexOf("OR "));
                //Chuỗi Đã có dạng CreatedBy='CUONGDV' OR CreatedBy='HUNGND'
                Reval = " AND ( " + Reval + " ) ";
            }
            return Reval;
        }

        public static ArrayList FromStringToArrayList(string DelegateUser)
        {
            ArrayList arrDelegateUser = new ArrayList();
            string[] _arrDelegateUser = DelegateUser.Split(',');
            for (int i = 0; i <= _arrDelegateUser.Length - 1; i++)
            {
                arrDelegateUser.Add(_arrDelegateUser[i].ToString());
            }
            return arrDelegateUser;
        }

        public static string sGetDelegateUser(DataTable dtDelegate)
        {
            string DelegateUser = "";
            foreach (DataRow dr in dtDelegate.Rows)
            {
                DelegateUser += dr["MainUser"].ToString() + ",";
            }
            if (DelegateUser != "") DelegateUser = DelegateUser.Substring(0, DelegateUser.Length - 1);
            return DelegateUser;
        }

        public static string sGetName(string pv_sTableName, string FieldToTake, string pv_sFieldName, int pv_ID)
        {
            try
            {
                SqlDataAdapter fv_da =
                    new SqlDataAdapter(
                        "SELECT " + FieldToTake + " FROM " + pv_sTableName + " WHERE " + pv_sFieldName + "=" + pv_ID,
                        globalVariables.SqlConn);
                DataTable fv_dt = new DataTable();
                fv_da.Fill(fv_dt);
                if (fv_dt.Rows.Count > 0)
                {
                    return Utility.sDbnull(fv_dt.Rows[0][FieldToTake]);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// biến thông tin 
        /// </summary>
        /// <param name="HosStatus"></param>
        /// <returns></returns>
        public static string GetHosStatus(byte HosStatus)
        {
            string strHos_Status = "Ngoại trú";
            if (Convert.ToInt32(HosStatus) == 2) strHos_Status = "Chờ ra viện";
            if (Convert.ToInt32(HosStatus) == 3) strHos_Status = "Đã xuất viện";
            if (Convert.ToInt32(HosStatus) == 0) strHos_Status = "Ngoại trú";
            if (Convert.ToInt32(HosStatus) == 1) strHos_Status = "Nội trú";
            return strHos_Status;
        }

        /// <summary>
        /// Lấy về giá trị SysDate từ DatabaseServer. Chủ yếu được dùng cho mục đích đồng bộ hóa thời gian giữa Client và Server
        /// </summary>
        /// <returns></returns>
        public static System.DateTime getSysDate()
        {
            try
            {
                SqlDataAdapter DA = new SqlDataAdapter("SELECT GETDATE()", globalVariables.SqlConn);
                DataTable DT = new DataTable();
                DA.Fill(DT);
                return Convert.ToDateTime(DT.Rows[0][0]);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lấy thời gian của hệ thống");
                return DateTime.Now;
            }
        }

        /// <summary>
        /// hàm thực hiện đặt hienr thị cho phần ToolStripStatusLabel
        /// </summary>
        /// <param name="obj">Đối tượng truyền vào</param>
        /// <param name="Text">dữ kiệu hiển thị</param>
        /// <param name="Visible">ẩn hay hiện thị của control</param>
        public static void CreateMessage(System.Windows.Forms.ToolStripStatusLabel obj, string Text, bool Visible)
        {
            obj.Text = Text;
            obj.Visible = Visible;
        }

        public static string sGetFieldValue(string pv_sTableName, string pv_sFieldName, string pv_sCondition)
        {
            try
            {
                SqlDataAdapter fv_da =
                    new SqlDataAdapter(
                        "SELECT " + pv_sFieldName + " FROM " + pv_sTableName + " WHERE " + pv_sCondition,
                        globalVariables.SqlConn);
                DataTable fv_dt = new DataTable();
                fv_da.Fill(fv_dt);
                if (fv_dt.Rows.Count > 0)
                {
                    return Utility.sDbnull(fv_dt.Rows[0][0]);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static DataRow drGetDatarow(string pv_sTableName, string pv_sFieldName, string pv_sCondition)
        {
            try
            {
                SqlDataAdapter fv_da =
                    new SqlDataAdapter(
                        "SELECT " + pv_sFieldName + " FROM " + pv_sTableName + " WHERE " + pv_sCondition,
                        globalVariables.SqlConn);
                DataTable fv_dt = new DataTable();
                fv_da.Fill(fv_dt);
                if (fv_dt.Rows.Count > 0)
                {
                    return fv_dt.Rows[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void FillComboBox(DataSet pv_ds, string sDisplaymember, string sValueMember, ComboBox pv_oCboBox)
        {
            try
            {
                pv_oCboBox.DataSource = null;
                {
                    pv_oCboBox.DataSource = pv_ds.Tables[0].DefaultView;
                    pv_oCboBox.DisplayMember = sDisplaymember;
                    pv_oCboBox.ValueMember = sValueMember;
                }
                if (pv_oCboBox.Items.Count > 0)
                {
                    pv_oCboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Tạo các Items trong ComboBox từ nguồn dữ liệu DataSet với DisplayMember = "sNAME" và ValueMember = "ID"
        /// Chú ý: Chỉ thành công nếu bảng trong Dataset chứa 2 trường có tên sNAME và ID
        /// </summary>
        /// <param name="pv_ds"></param>
        /// <param name="pv_oCboBox"></param>
        public static void FillComboBox(DataSet pv_ds, ComboBox pv_oCboBox)
        {
            try
            {
                pv_oCboBox.DataSource = null;
                {
                    pv_oCboBox.DataSource = pv_ds.Tables[0].DefaultView;
                    pv_oCboBox.DisplayMember = "SNAME";
                    pv_oCboBox.ValueMember = "ID";
                }
                if (pv_oCboBox.Items.Count > 0)
                {
                    pv_oCboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Tạo các Items trong ComboBox từ nguồn dữ liệu DataSet với Item.Text=giá trị cột ID+ "-"+ giá trị cột sName
        /// Chú ý: Chỉ thành công nếu bảng trong Dataset chứa 2 trường có tên sNAME và ID
        /// </summary>
        /// <param name="pv_ds"></param>
        /// <param name="pv_oCboBox"></param>
        public static void FillComboBoxNoUsingValueMember(DataSet pv_ds, ComboBox pv_oCboBox)
        {
            try
            {
                pv_oCboBox.Items.Clear();

                foreach (DataRow dr in pv_ds.Tables[0].Rows)
                {
                    pv_oCboBox.Items.Add(dr["ID"] + "-" + dr["sName"]);
                }
                if (pv_oCboBox.Items.Count > 0)
                {
                    pv_oCboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Tạo các Items trong ComboBox từ nguồn dữ liệu DataSet với Item.Text=giá trị cột ID+ "-"+ giá trị cột sName
        /// Ngoài ra còn có Item đầu tiên có text là FirstItem. Thường dùng cho ComboBox có chứa giá trị "Tất cả"
        /// Chú ý: Chỉ thành công nếu bảng trong Dataset chứa 2 trường có tên sNAME và ID
        /// </summary>
        /// <param name="pv_ds"></param>
        /// <param name="pv_oCboBox"></param>
        /// <param name="FirstItem"></param>
        public static void FillComboBoxNoUsingValueMember(DataSet pv_ds, ComboBox pv_oCboBox, string FirstItem)
        {
            try
            {
                pv_oCboBox.Items.Clear();

                pv_oCboBox.Items.Add(FirstItem);
                foreach (DataRow dr in pv_ds.Tables[0].Rows)
                {
                    pv_oCboBox.Items.Add(dr["ID"] + "-" + dr["sName"]);
                }
                if (pv_oCboBox.Items.Count > 0)
                {
                    pv_oCboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Tạo các Items trong ComboBox từ nguồn dữ liệu DataSet với DisplayMember = sDisplaymember và ValueMember = sValueMember. Có sử dụng sắp sếp dữ liệu trước khi Fill vào ComboBox
        /// </summary>
        /// <param name="pv_ds"></param>
        /// <param name="pv_oCboBox"></param>
        /// <param name="sDisplaymember"></param>
        /// <param name="sValueMember"></param>
        /// <param name="OrderExpression">Mệnh đề Order. Ví dụ: ID Desc(Sắp sếp tăng dần theo cột ID)</param>
        public static void FillComboBoxHavingOrderData(DataSet pv_ds, ComboBox pv_oCboBox, string sDisplaymember,
                                                       string sValueMember, string OrderExpression)
        {
            try
            {
                pv_oCboBox.Items.Clear();
                pv_oCboBox.DataSource = null;
                pv_ds.Tables[0].DefaultView.Sort = OrderExpression;
                {
                    pv_oCboBox.DataSource = pv_ds.Tables[0].DefaultView;
                    pv_oCboBox.DisplayMember = sDisplaymember;
                    pv_oCboBox.ValueMember = sValueMember;
                }
                if (pv_oCboBox.Items.Count > 0)
                {
                    pv_oCboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Tạo các Items trong ComboBox từ nguồn dữ liệu (thông qua truy vấn bảng có tên pv_sTableName) với DisplayMember = sDisplaymember và ValueMember = sValueMember. 
        /// </summary>
        /// <param name="pv_sTableName"></param>
        /// <param name="sDisplaymember"></param>
        /// <param name="sValueMember"></param>
        /// <param name="pv_oCboBox"></param>
        public static void FillComboBox(string pv_sTableName, string sDisplaymember, string sValueMember,
                                        ComboBox pv_oCboBox)
        {
            SqlDataAdapter sv_Da = new SqlDataAdapter("SELECT * FROM " + pv_sTableName, globalVariables.SqlConn);
            DataTable sv_Dt = new DataTable();
            try
            {
                sv_Da.Fill(sv_Dt);
                pv_oCboBox.Items.Clear();
                pv_oCboBox.DataSource = null;
                {
                    pv_oCboBox.DataSource = sv_Dt.DefaultView;
                    pv_oCboBox.DisplayMember = sDisplaymember;
                    pv_oCboBox.ValueMember = sValueMember;
                }
                if (pv_oCboBox.Items.Count > 0)
                {
                    pv_oCboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// /// Tạo các Items trong ComboBox từ nguồn dữ liệu (thông qua truy vấn bảng có tên pv_sTableName) với DisplayMember = sDisplaymember và ValueMember = sValueMember. 
        /// ComboBox có chứa phần tử đầu tiên mang giá trị FirstItem
        /// </summary>
        /// <param name="pv_sTableName"></param>
        /// <param name="sDisplaymember"></param>
        /// <param name="sValueMember"></param>
        /// <param name="pv_oCboBox"></param>
        /// <param name="FirstItem"></param>
        public static void FillComboBoxNoUsingValueMember(string pv_sTableName, string sDisplaymember,
                                                          string sValueMember, ComboBox pv_oCboBox, string FirstItem)
        {
            SqlDataAdapter sv_Da = new SqlDataAdapter("SELECT * FROM " + pv_sTableName, globalVariables.SqlConn);
            DataTable sv_Dt = new DataTable();
            try
            {
                sv_Da.Fill(sv_Dt);
                pv_oCboBox.Items.Clear();
                pv_oCboBox.Items.Add(FirstItem);
                foreach (DataRow dr in sv_Dt.Rows)
                {
                    pv_oCboBox.Items.Add(dr[sValueMember] + "-" + dr[sDisplaymember]);
                }
                if (pv_oCboBox.Items.Count > 0)
                {
                    pv_oCboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pv_sTableName"></param>
        /// <param name="sDisplaymember"></param>
        /// <param name="sValueMember"></param>
        /// <param name="pv_oCboBox"></param>
        public static void FillComboBoxNoUsingValueMember(string pv_sTableName, string sDisplaymember,
                                                          string sValueMember, ComboBox pv_oCboBox)
        {
            SqlDataAdapter sv_Da = new SqlDataAdapter("SELECT * FROM " + pv_sTableName, globalVariables.SqlConn);
            DataTable sv_Dt = new DataTable();
            try
            {
                sv_Da.Fill(sv_Dt);
                pv_oCboBox.Items.Clear();
                foreach (DataRow dr in sv_Dt.Rows)
                {
                    pv_oCboBox.Items.Add(dr[sValueMember] + "-" + dr[sDisplaymember]);
                }
                if (pv_oCboBox.Items.Count > 0)
                {
                    pv_oCboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Fill giá trị của một bảng vào Combobox
        /// </summary>
        /// <param name="pv_sTableName"></param>
        /// <param name="pv_oCboBox"></param>
        public static void FillComboBox(string pv_sTableName, ComboBox pv_oCboBox)
        {
            SqlDataAdapter sv_Da = new SqlDataAdapter("SELECT * FROM " + pv_sTableName + " ORDER BY intSTT ASC",
                                                      globalVariables.SqlConn);
            DataTable sv_Dt = new DataTable();
            try
            {
                sv_Da.Fill(sv_Dt);
                pv_oCboBox.Items.Clear();
                pv_oCboBox.DataSource = null;
                {
                    pv_oCboBox.DataSource = sv_Dt.DefaultView;
                    pv_oCboBox.DisplayMember = "SNAME";
                    pv_oCboBox.ValueMember = "ID";
                }
                if (pv_oCboBox.Items.Count > 0)
                {
                    pv_oCboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Lấy về giá trị kế tiếp sau giá trị lớn nhất của trường pv_sFieldName trong bảng pv_sTableName. 
        /// Thường được dùng trong các bảng có cột ID làm khóa ,kiểu dữ liệu là Numeric và không có tính chất tự tăng
        /// </summary>
        /// <param name="pv_sFieldName"></param>
        /// <param name="pv_sTableName"></param>
        /// <returns>Nếu không có bản ghi nào thì trả về giá trị 1.</returns>
        public static int getNextMaxID(string pv_sFieldName, string pv_sTableName)
        {
            DataTable fv_dt = new DataTable();
            SqlDataAdapter fv_da = new SqlDataAdapter("SELECT MAX(" + pv_sFieldName + ") FROM " + pv_sTableName,
                                                      globalVariables.SqlConn);
            try
            {
                fv_da.Fill(fv_dt);
                if (fv_dt.Rows.Count > 0)
                {
                    return (Information.IsDBNull(fv_dt.Rows[0][0]) ? 1 : Utility.Int32Dbnull(fv_dt.Rows[0][0])) + 1;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return 1;
            }
        }

        public static int getNextMaxID(string pv_sFieldName, string pv_sTableName, Int16 Val)
        {
            DataTable fv_dt = new DataTable();
            SqlDataAdapter fv_da =
                new SqlDataAdapter(
                    "SELECT MAX(" + pv_sFieldName + ") FROM " + pv_sTableName + " where Parent_ID=" + Val,
                    globalVariables.SqlConn);
            try
            {
                fv_da.Fill(fv_dt);
                if (fv_dt.Rows.Count > 0)
                {
                    return (Information.IsDBNull(fv_dt.Rows[0][0]) ? 1 : Utility.Int32Dbnull(fv_dt.Rows[0][0])) + 1;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return 1;
            }
        }

        /// <summary>
        /// Lấy về giá trị lớn nhất của một cột có tên pv_sFieldName trong bảng có tên pv_sTableName
        /// Thường được dùng trong các bảng có cột ID làm khóa ,kiểu dữ liệu là Numeric và không có tính chất tự tăng
        /// </summary>
        /// <param name="pv_sFieldName"></param>
        /// <param name="pv_sTableName"></param>
        /// <returns></returns>
        public static int getCurrentMaxID(string pv_sFieldName, string pv_sTableName)
        {
            DataTable fv_dt = new DataTable();
            SqlDataAdapter fv_da = new SqlDataAdapter("SELECT MAX(" + pv_sFieldName + ") FROM " + pv_sTableName,
                                                      globalVariables.SqlConn);
            try
            {
                fv_da.Fill(fv_dt);
                if (fv_dt.Rows.Count > 0)
                {
                    return (Information.IsDBNull(fv_dt.Rows[0][0]) ? 1 : Utility.Int32Dbnull(fv_dt.Rows[0][0]));
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return 1;
            }
        }

        /// <summary>
        /// Lấy về tỉ giá của một loại tiền so với đồng Vnd. Chưa dùng
        /// </summary>
        /// <param name="pv_MoneyID"></param>
        /// <param name="pv_sMoneyName"></param>
        /// <returns></returns>
        public static double dblgetExchangeRate(int pv_MoneyID, string pv_sMoneyName)
        {
            SqlDataAdapter fv_DA = null;
            if (pv_MoneyID != -1)
            {
                fv_DA = new SqlDataAdapter("SELECT fRate FROM TBL_MONEYUNIT WHERE ID=" + pv_MoneyID,
                                           globalVariables.SqlConn);
            }
            else
            {
                fv_DA = new SqlDataAdapter("SELECT fRate FROM TBL_MONEYUNIT WHERE sName=N'" + pv_sMoneyName + "'",
                                           globalVariables.SqlConn);
            }

            DataSet fv_DS = new DataSet();
            try
            {
                fv_DA.Fill(fv_DS, "TBL_MONEYUNIT");
                if (fv_DS.Tables[0].Rows.Count > 0)
                {
                    return (double)fv_DS.Tables[0].Rows[0][0];
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// Kiểm tra sự tồn tại của một bản ghi trong bảng 
        /// </summary>
        /// <param name="pv_sTableName">Tên bảng</param>
        /// <param name="pv_sFieldName">Tên trường </param>
        /// <param name="pv_objValue">Giá trị của trường</param>
        /// <param name="pv_bIsDigit">Giá trị kiểu chữ số hay chữ cái</param>
        /// <returns></returns>
        public static bool bIsExisted(string pv_sTableName, string pv_sFieldName, object pv_objValue, bool pv_bIsDigit)
        {
            try
            {
                SqlDataAdapter fv_DA =
                    new SqlDataAdapter(
                        "SELECT * FROM " + pv_sTableName + " WHERE " + pv_sFieldName +
                        sGetCondition(pv_objValue, pv_bIsDigit), globalVariables.SqlConn);
                DataSet fv_DS = new DataSet();
                fv_DA.Fill(fv_DS, pv_sTableName);
                if (fv_DS.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra sự tồn tại của bản ghi trong bảng CSDL
        /// </summary>
        /// <param name="pv_sTableName">Tên bảng</param>
        /// <param name="sCondition">Điều kiện tìm kiếm</param>
        /// <returns></returns>
        public static bool bIsExisted(string pv_sTableName, string sCondition)
        {
            try
            {
                SqlDataAdapter fv_DA = new SqlDataAdapter("SELECT 1 FROM " + pv_sTableName + " WHERE " + sCondition,
                                                          globalVariables.SqlConn);
                DataSet fv_DS = new DataSet();
                fv_DA.Fill(fv_DS, pv_sTableName);
                if (fv_DS.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return false;
            }
        }

        public static string sGetCondition(object pv_objValue, bool pv_bIsDigit)
        {
            if (pv_bIsDigit)
            {
                return " =" + Utility.Int32Dbnull(pv_objValue);
            }
            else
            {
                return " =N'" + Utility.sDbnull(pv_objValue) + "'";
            }
        }

        /// <summary>
        /// hàm thực hiện Import thông tin của một mảng datarow vào trong một datatable
        /// </summary>
        /// <param name="dataTable">datatable</param>
        /// <param name="arrDr">mảng datarow</param>
        /// <returns></returns>
        public static DataTable ImportDataRowToDatatable(DataTable dt, DataRow[] arrDr)
        {
            DataTable dataTable = new DataTable();
            dataTable = dt.Clone();
            foreach (DataRow dr in arrDr)
            {
                dataTable.ImportRow(dr);
            }
            dataTable.AcceptChanges();
            return dataTable;
        }

        /// <summary>
        /// Ham thuc hien lay thong tin bien doi gia tri cua mot cot trong datatable vao chuoi string
        /// </summary>
        /// <param name="dt">datatable nguon truyen vao</param>
        /// <param name="colFilter">Gia tri can loc ,vd :"CHON=1"</param>
        /// <param name="colID">Gia tri can lay de luu vao string</param>
        /// <returns></returns>
        public static string GetCheckedID(DataTable dt, string colFilter, string colID)
        {
            string str = "-1";
            foreach (DataRow dr in dt.Select(colFilter))
            {
                str += "," + dr[colID].ToString();
            }
            return str;
        }

        /// <summary>
        /// hàm thực hiện thêm giá trị vào một cọt nào đó
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="FiledName"></param>
        /// <param name="FiledValue"></param>
        public static void InsertValueToDatatable(ref DataTable dataTable, string FiledName, string FiledValue)
        {
            if (!dataTable.Columns.Contains("CHON")) dataTable.Columns.Add("CHON", typeof(int));
            foreach (DataRow dr in dataTable.Rows)
            {
                dr[FiledName] = FiledValue;
            }
            dataTable.AcceptChanges();
        }

        /// <summary>
        /// Tìm kiếm ký tự đầu để làm số phiếu khám bệnh kế tiếp
        /// </summary>
        /// <param name="yymmdd"></param>
        /// <returns></returns>
        public static string findFirstCharacter(string yymmdd)
        {
            SqlDataAdapter da =
                new SqlDataAdapter(
                    "select max(Left(RegNo+'0',1)) from TBL_REGSERVICE where RIGHT('000'+REGNO,3)<='999' AND RIGHT(LEFT(REGNO+'0000000',7),6)='" +
                    yymmdd + "'", globalVariables.SqlConn);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (Information.IsDBNull(dt.Rows[0][0]))
                    {
                        return "A";
                    }
                    if (Strings.InStr("ABCDEFGHIJKNMLOPQSRTVWZX", Utility.sDbnull(dt.Rows[0][0]), CompareMethod.Text) >
                        0)
                    {
                        return CharacterFound(Utility.sDbnull(dt.Rows[0][0]), Utility.sDbnull(dt.Rows[0][0]) + yymmdd);
                    }
                    else
                    {
                        return "A";
                    }
                }
                else
                {
                    return "A";
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return "A";
            }
        }

        private static string CharacterFound(string C, string CYYMMDD)
        {
            SqlDataAdapter da =
                new SqlDataAdapter(
                    "select 1 from TBL_REGSERVICE where RIGHT('000'+REGNO,3)='999' AND LEFT(REGNO+'0000000',7)='" +
                    CYYMMDD + "'", globalVariables.SqlConn);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string[] arrCharacter = "A,B,C,D,E,F,G,H,I,J,K,N,M,L,O,P,Q,S,R,T,V,W,X,Y,Z".Split(',');
                    int Index = 0;
                    for (int i = 0; i <= arrCharacter.GetLength(0) - 1; i++)
                    {
                        if (arrCharacter[i] == C)
                        {
                            Index = i;
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                    if (Index < arrCharacter.GetLength(0) - 1)
                    {
                        return arrCharacter[Index + 1];
                    }
                    else
                    {
                        Utility.ShowMsg("Hệ thống sinh mã đã đạt tới giới hạn trong ngày Z_YYMMDD_999" +
                                        Constants.vbCrLf +
                                        "Bạn cần phải xem lại hệ thống trước khi tiếp tục sử dụng chương trình.");
                        return "-A";
                    }
                }
                else
                {
                    return C;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return C;
            }
        }

        /// <summary>
        /// Lấy giá trị của một trường pv_sFieldName trong  một bảng pv_sTableName thỏa mãn điều kiện tìm kiếm pv_sCondition
        /// </summary>
        /// <param name="pv_sTableName">Tên bảng</param>
        /// <param name="pv_sFieldName">Tên trường cần lấy giá trị</param>
        /// <param name="pv_sCondition">Tên điều kiện tìm kiếm</param>
        /// <returns></returns>
        public static object GetFieldValue(string pv_sTableName, string pv_sFieldName, string pv_sCondition)
        {
            string fv_sSql = "";
            fv_sSql = "SELECT " + pv_sFieldName + " FROM " + pv_sTableName + " WHERE " + pv_sCondition;
            SqlDataAdapter DA = new SqlDataAdapter(fv_sSql, globalVariables.SqlConn);
            DataTable DT = new DataTable();
            try
            {
                DA.Fill(DT);
                if (DT.Rows.Count > 0)
                {
                    return DT.Rows[0][0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Tạo nguồn dữ liệu cho AutocompleteTextBox
        /// </summary>
        /// <param name="datasource">Nguồn chứa dữ liệu</param>
        /// <param name="FieldName">Tên column trong bảng chứa dữ liệu</param>
        /// <returns></returns>
        public static AutoCompleteStringCollection GetAutoCompleteStringCollection(DataTable datasource,
                                                                                   string FieldName)
        {
            try
            {
                AutoCompleteStringCollection AutoComp = new AutoCompleteStringCollection();

                foreach (DataRow dr in datasource.Rows)
                {
                    string Values = "";

                    Values += dr[FieldName].ToString();

                    AutoComp.Add(Values);
                }
                return AutoComp;
            }
            catch
            {
                return new AutoCompleteStringCollection();
            }
        }

        /// <summary>
        /// Tạo nguồn dữ liệu cho AutocompleteTextBox
        /// </summary>
        /// <param name="datasource">Nguồn chứa dữ liệu</param>
        /// <param name="FieldName">Tên column trong bảng chứa dữ liệu</param>
        /// <returns></returns>
        public static AutoCompleteStringCollection GetAutoCompleteStringCollection(DataView datasource, string FieldName)
        {
            try
            {
                AutoCompleteStringCollection AutoComp = new AutoCompleteStringCollection();

                foreach (DataRowView drv in datasource)
                {
                    string Values = "";

                    Values += drv[FieldName].ToString();

                    AutoComp.Add(Values);
                }
                return AutoComp;
            }
            catch
            {
                return new AutoCompleteStringCollection();
            }
        }

        /// <summary>
        /// Tạo danh sách dữ liệu dùng trong các AutoCompleteTextBox. Chú ý: Số phần tử của arrField=Seperator.
        /// </summary>
        /// <param name="datasource">Nguồn chứa dữ liệu</param>
        /// <param name="arrField">Danh sách các field trong datasource muốn kết hợp tạo thành giá trị trong AutocompleteTextBox</param>
        /// <param name="Seperator">Phân cách giữa các field</param>
        /// <returns></returns>
        public static AutoCompleteStringCollection GetAutoCompleteStringCollection(DataTable datasource,
                                                                                   ArrayList arrField,
                                                                                   ArrayList Seperator)
        {
            try
            {
                AutoCompleteStringCollection AutoComp = new AutoCompleteStringCollection();
                if (arrField.Count != Seperator.Count) return AutoComp;
                foreach (DataRow dr in datasource.Rows)
                {
                    string Values = "";
                    for (int i = 0; i <= arrField.Count - 1; i++)
                    {
                        Values += dr[arrField[i].ToString()].ToString() + Seperator[i].ToString();
                    }
                    Values = Values.Substring(0, Values.LastIndexOf(Seperator[Seperator.Count - 1].ToString()));
                    AutoComp.Add(Values);
                }
                return AutoComp;
            }
            catch
            {
                return new AutoCompleteStringCollection();
            }
        }

        /// <summary>
        /// Tạo danh sách dữ liệu dùng trong các AutoCompleteTextBox. Chú ý: Số phần tử của arrField=Seperator.
        /// </summary>
        /// <param name="datasource">Nguồn chứa dữ liệu</param>
        /// <param name="arrField">Danh sách các field trong datasource muốn kết hợp tạo thành giá trị trong AutocompleteTextBox</param>
        /// <param name="Seperator">Phân cách giữa các field</param>
        /// <returns></returns>
        public static AutoCompleteStringCollection GetAutoCompleteStringCollection(DataView datasource,
                                                                                   ArrayList arrField,
                                                                                   ArrayList Seperator)
        {
            try
            {
                AutoCompleteStringCollection AutoComp = new AutoCompleteStringCollection();
                if (arrField.Count != Seperator.Count) return AutoComp;
                foreach (DataRowView drv in datasource)
                {
                    string Values = "";
                    for (int i = 0; i <= arrField.Count - 1; i++)
                    {
                        Values += drv[arrField[i].ToString()].ToString() + Seperator[i].ToString();
                    }
                    Values = Values.Substring(0, Values.LastIndexOf(Seperator[Seperator.Count - 1].ToString()));
                    AutoComp.Add(Values);
                }
                return AutoComp;
            }
            catch
            {
                return new AutoCompleteStringCollection();
            }
        }

        public static DataRow[] ArrGetFieldValue(string pv_sTableName, string pv_sFieldName, string pv_sCondition)
        {
            string fv_sSql = "";
            fv_sSql = "SELECT " + pv_sFieldName + " FROM " + pv_sTableName + " WHERE " + pv_sCondition;
            SqlDataAdapter DA = new SqlDataAdapter(fv_sSql, globalVariables.SqlConn);
            DataTable DT = new DataTable();
            try
            {
                DA.Fill(DT);
                if (DT.Rows.Count > 0)
                {
                    return DT.Select("1=1");
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return null;
            }
        }

        public static void LoadParamsValues(Assembly pv_objAss)
        {
            DataRow CurRow = default(DataRow);
            Type t = null;
            try
            {
                foreach (Type t_loopVariable in pv_objAss.GetTypes())
                {
                    t = t_loopVariable;
                    FieldInfo[] fi = t.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.IgnoreCase);
                    FieldInfo f = default(FieldInfo);
                    for (int i = 0; i <= fi.Length - 1; i++)
                    {
                        f = fi[i];
                        switch (f.Name)
                        {
                            case "Branch_Name":
                                SetField(t, "Branch_Name", globalVariables.Branch_Name);
                                break;
                            case "gv_StaffID":
                                SetField(t, "gv_StaffID", globalVariables.gv_intIDNhanvien);
                                break;
                            case "Branch_Address":
                                SetField(t, "Branch_Address", globalVariables.Branch_Address);
                                break;
                           
                            case "gv_sStaffName":
                                SetField(t, "gv_sStaffName", globalVariables.gv_strTenNhanvien);
                                break;
                            case "gv_intServiceType_ID":
                                SetField(t, "gv_intServiceType_ID", globalVariables.gv_intServiceType_ID);
                                break;
                            case "Branch_Phone":
                                SetField(t, "Branch_Phone", globalVariables.Branch_Phone);
                                break;
                            case "Branch_Email":
                                SetField(t, "Branch_Email", globalVariables.Branch_Email);
                                break;
                            case "Branch_Website":
                                SetField(t, "Branch_Website", globalVariables.Branch_Website);
                                break;
                            case "globalVariables.SqlConn":
                                SetField(t, "globalVariables.SqlConn", globalVariables.SqlConn);
                                break;
                            case "Branch_ID":
                                SetField(t, "Branch_ID", globalVariables.Branch_ID);
                                break;
                            case "gv_intCurrMonth":
                                SetField(t, "gv_intCurrMonth", globalVariables.gv_intCurrMonth);
                                break;
                            case "gv_intCurrYear":
                                SetField(t, "gv_intCurrYear", globalVariables.gv_intCurrYear);
                                break;
                            case "UserName":
                                SetField(t, "UserName", globalVariables.UserName);
                                break;
                            case "ParentBranch_Name":
                                SetField(t, "ParentBranch_Name", globalVariables.ParentBranch_Name);
                                break;
                            case "DisplayLanguage":
                                SetField(t, "DisplayLanguage", globalVariables.DisplayLanguage);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.ToString(), MsgBoxStyle.Exclamation, "DVC");
            }
        }

        #endregion

        #region "PHAN THU VIEN DUNG CHUNG CUA TÚ"

        public static bool HasNull(DataTable table)
        {
            foreach (DataColumn column in table.Columns)
            {
                if (table.Rows.OfType<DataRow>().Any(r => r.IsNull(column)))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// ham thuc hien viec convert row to Column
        /// </summary>
        /// <param name="inputTable"></param>
        /// <returns></returns>
        public DataTable ConvertTableRowToColumn(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();
            // Add columns by looping rows

            // Header row's first column is same as in inputTable
            outputTable.Columns.Add(inputTable.Columns[0].ColumnName.ToString());

            // Header row's second column onwards, 'inputTable's first column taken
            foreach (DataRow inRow in inputTable.Rows)
            {
                string newColName = inRow[0].ToString();
                outputTable.Columns.Add(newColName);
            }

            // Add rows by looping columns        
            for (int rCount = 1; rCount <= inputTable.Columns.Count - 1; rCount++)
            {
                DataRow newRow = outputTable.NewRow();

                // First column is inputTable's Header row's second column
                newRow[0] = inputTable.Columns[rCount].ColumnName.ToString();
                for (int cCount = 0; cCount <= inputTable.Rows.Count - 1; cCount++)
                {
                    string colValue = inputTable.Rows[cCount][rCount].ToString();
                    newRow[cCount + 1] = colValue;
                }
                outputTable.Rows.Add(newRow);
            }

            return outputTable;
        }

        public static void Try2CreateFolder(string FolderName)
        {
            try
            {
                if (!Directory.Exists(FolderName))
                    Directory.CreateDirectory(FolderName);
            }
            catch
            {
            }
        }
        //public static void Try2CreateFolder(string file)
        //{
        //    try
        //    {
        //        if (!Directory.Exists(Path.GetDirectoryName(file)))
        //            Directory.CreateDirectory(Path.GetDirectoryName(file));
        //    }
        //    catch
        //    {
        //    }
        //}
        public static string chuanhoaDecimal(string s)
        {
            try
            {
                if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ".")
                    return s.Replace(",", ".");
                else
                    return s.Replace(".", ",");
            }
            catch (Exception)
            {
                return s;

            }
        }
        public static string chuanhoachuoi(string chuoi) //chuoi: chuỗi cần chuẩn hoá
        {
            string s = chuoi;
            string a = Microsoft.VisualBasic.Strings.StrConv(s, Microsoft.VisualBasic.VbStrConv.ProperCase, 0);
            a = a.Trim();
            while (Microsoft.VisualBasic.Strings.InStr(a, "  ", 0) > 0)
            {
                a = a.Replace("  ", " ");
            }
            return a;
        }

        /// <summary>
        /// hàm thực hiện việc địnhn ạng hệ thống theo định dạng đưa vào
        /// </summary>
        /// <param name="sFormatDate">định dạng theo dúng định dạng theo qui định</param>
        public static void FormatSystemDateTime(string sFormatDate)
        {
            //RegistryKey reg;
            Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\Control Panel\\International", "sShortDate",
                                              sFormatDate);
            Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\Control Panel\\International", "sLongDate",
                                              sFormatDate);
        }

        /// <summary>
        /// hàm thực hiện việc update thông tin khi dữ liệu truyền vào nếu là null
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="sFieldName"></param>
        /// <param name="sValue"></param>
        public static void UpdateDataInColWhenValueIsNull(ref DataTable dataTable, string sFieldName, string sValue)
        {
            if (dataTable.Columns.Contains(sFieldName))
            {
                foreach (DataRow drv in dataTable.Rows)
                {
                    if (Utility.Int32Dbnull(drv[sFieldName], -1) < 0)
                    {
                        drv[sFieldName] = sValue;
                    }
                }
            }
            dataTable.AcceptChanges();
        }

        public static void AddDataInColumnToDatatable(ref DataTable dataTable, string FieldName, string sValues)
        {
            foreach (DataRow dr in dataTable.Rows)
            {
                dr[FieldName] = sValues;
            }
            dataTable.AcceptChanges();
        }

        public static void AddColumToDataTable(ref DataTable dataTable, string FieldName, Type type)
        {
            if (!dataTable.Columns.Contains(FieldName))
                dataTable.Columns.Add(FieldName, type);
            dataTable.AcceptChanges();
        }
        public static void AddColums2DataTable(ref DataTable dataTable, List<string> lstFieldNames, List<Type> lstTypes)
        {
            int idx = 0;
            foreach (string FieldName in lstFieldNames)
            {
                if (!dataTable.Columns.Contains(FieldName))
                    dataTable.Columns.Add(FieldName, lstTypes[idx]);
                idx++;
            }
            dataTable.AcceptChanges();
        }
        public static void AddColums2DataTable(ref DataTable dataTable, List<string> lstFieldNames,Type FieldType)
        {
            foreach (string FieldName in lstFieldNames)
            {
                if (dataTable!=null && !dataTable.Columns.Contains(FieldName))
                    dataTable.Columns.Add(FieldName, FieldType);
            }
            dataTable.AcceptChanges();
        }
        public static void InsertValueIdentity(ref DataTable dt, string colFieldName)
        {
            if (!dt.Columns.Contains(colFieldName)) dt.Columns.Add(colFieldName, typeof(int));
            int idx = 1;
            foreach (DataRow dr in dt.Rows)
            {
                dr[colFieldName] = idx;
                idx++;
            }
            dt.AcceptChanges();
        }

        /// <summary>
        /// hàm thực hiện việc load thông itn vào bộ nhớ
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Bitmap LoadBitmap(string path)
        {
            //Open file in read only mode
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            //Get a binary reader for the file stream
            using (BinaryReader reader = new BinaryReader(stream))
            {
                //copy the content of the file into a memory stream
                var memoryStream = new MemoryStream(reader.ReadBytes((int)stream.Length));
                //make a new Bitmap object the owner of the MemoryStream
                stream.Dispose();
                reader.Close();
                return new Bitmap(memoryStream);
            }
        }

        public static void CreateFileName(string sFathName, string sValue)
        {
            if (!System.IO.Directory.Exists(globalVariables.sFolderName))
            {
                System.IO.Directory.CreateDirectory(globalVariables.sFolderName);
            }

            if (System.IO.File.Exists(sFathName))
            {
                System.IO.File.WriteAllText(sFathName, sValue);
            }
            else
            {
                System.IO.File.WriteAllText(sFathName, sValue);
            }
        }

        /// <summary>
        /// ham thuc hien viec set resize cuar form cuar hamf
        /// </summary>
        /// <param name="vForm"></param>
        public static void SetResize(Form vForm)
        {
            if (!System.IO.Directory.Exists(globalVariables.sFolderName))
            {
                System.IO.Directory.CreateDirectory(globalVariables.sFolderName);
            }
            string sFathName = globalVariables.sFolderName + vForm.Name + ".txt";
            string[] arrList = { vForm.Width.ToString(), vForm.Height.ToString() };
            if (System.IO.File.Exists(sFathName))
            {
                System.IO.File.WriteAllLines(sFathName, arrList);
            }
            else
            {
                System.IO.File.WriteAllLines(sFathName, arrList);
            }
        }

        /// <summary>
        /// hàm thực hiện việc cho phépđoọc thông tin ủa dđương dẫn
        /// </summary>
        /// <param name="sFathName"></param>
        /// <returns></returns>
        public static string[] ReadFileName(string sFathName)
        {
            string[] arrList = { "100", "100" };
            if (System.IO.File.Exists(sFathName))
            {
                arrList = System.IO.File.ReadAllLines(sFathName);
            }
            return arrList;
        }

        /// <summary>
        /// hàm thực hiện âấy thoongtin của form hiện tại
        /// </summary>
        /// <param name="vForm"></param>
        public static void GetResizeForm(Form vForm)
        {
            string sFathName = globalVariables.sFolderName + vForm.Name + ".txt";
            if (!System.IO.File.Exists(sFathName))
            {
                SetResize(vForm);
            }
            Size OldSize = vForm.Size;
            try
            {
                
                string[] arrList = ReadFileName(sFathName);
                if (arrList.GetLength(0) > 0)
                {
                    System.Drawing.Point point = new Point(Utility.Int32Dbnull(arrList[0]),
                                                           Utility.Int32Dbnull(arrList[1]));
                    Size newSize = new Size(point);
                    vForm.Size = newSize;
                }
            }
            catch (Exception)
            {
                vForm.Size = OldSize;
                //throw;
            }
        }

        /// <summary>
        /// hàm thực hiện trả về giá trị mặc định nếu giá trì bằng null
        /// </summary>
        /// <param name="dataTable">Datatale cần truyền</param>
        /// <param name="sColName">Tên cột giá trị</param>
        /// <param name="sValue">Giá trị truyền vào</param>
        public static void UpdateColumnToDefultValueIsnull(ref DataTable dataTable, string sColName, string sValue)
        {
            foreach (DataRow drv in dataTable.Rows)
            {
                if (String.IsNullOrEmpty(drv[sColName].ToString())) drv[sColName] = sValue;
            }
            dataTable.AcceptChanges();
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

        public static void LoadImageLogo()
        {

            if (globalVariables.SysLogo != null) return;
            byte[] byteArray = null;
            string PathName = Application.StartupPath + @"\logo\logo.jpg";
            if (File.Exists(PathName))
            {

                byteArray = Utility.ConvertImageToByteArray(Image.FromFile(PathName),
                                                 ImageFormat.Jpeg);
            }
            else
            {
                PathName = Application.StartupPath + @"\logo\logo.bmp";
                if (File.Exists(PathName))
                {
                    byteArray = Utility.ConvertImageToByteArray(Image.FromFile(PathName),
                                             ImageFormat.Bmp);
                }
                else
                {
                    PathName = Application.StartupPath + @"\logo\logo.png";
                    byteArray = Utility.ConvertImageToByteArray(Image.FromFile(PathName),
                                           ImageFormat.Bmp);
                }
            }
            globalVariables.SysLogo = byteArray;

        }
        public static void SetParameterValue(ReportDocument crpt, string pName, object pVal)
        {
            try
            {
                foreach (CrystalDecisions.Shared.ParameterField p in crpt.ParameterFields)
                    if (Utility.DoTrim( p.Name.ToUpper()) == Utility.DoTrim(pName.ToUpper()))
                    {
                        crpt.SetParameterValue(pName, pVal);
                        return;

                    }
            }
            catch (Exception ex)
            {
                Utility.CatchException(string.Format("ex:{0}- name:{1} -val {2}", ex.ToString(),pName,pVal),ex);
            }
        }
        public static void SetParameterValueNoCheckExists(ReportDocument crpt, string pName, object pVal)
        {
            try
            {
                crpt.SetParameterValue(pName, pVal);
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        public static void GetChanDoan(string icdChinh, string idcPhu,string chandoan, ref string icdName, ref string icdCode)
        {
            try
            {
                List<string> lstIcd = icdChinh.Split(',').ToList();
                //DmucBenhCollection list =
                //    new DmucBenhController().FetchByQuery(
                //        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstIcd));
                DataRow[] arrBenh = globalVariables.gv_dtDmucBenh.Select(string.Format("{0} in ('{1}')", DmucBenh.MaBenhColumn.ColumnName, string.Join("','",icdChinh.Split(',').ToArray<string>())));
                foreach (DataRow item in arrBenh)
                {
                    icdName += item[DmucBenh.TenBenhColumn.ColumnName] + "; ";
                    icdCode += item[DmucBenh.MaBenhColumn.ColumnName] + "; ";
                }
               arrBenh = globalVariables.gv_dtDmucBenh.Select(string.Format("{0} in ('{1}')", DmucBenh.MaBenhColumn.ColumnName, string.Join("','",idcPhu.Split(',').ToArray<string>())));
                foreach (DataRow item in arrBenh)
                {
                    icdName += item[DmucBenh.TenBenhColumn.ColumnName] + "; ";
                    icdCode += item[DmucBenh.MaBenhColumn.ColumnName] + "; ";
                }
                if (Utility.sDbnull(chandoan).Length > 0)
                {
                    DmucChung _dmucchung=new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("CHANDOAN")
                        .And(DmucChung.Columns.Ma).IsEqualTo(chandoan).ExecuteSingle<DmucChung>();
                    if(_dmucchung!=null)
                        icdName += _dmucchung.Ten;
                }
                if (icdName.Trim() != "") icdName = icdName.Substring(0, icdName.Length - 1);
                if (icdCode.Trim() != "") icdCode = icdCode.Substring(0, icdCode.Length - 1);
               
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin) Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        /// <summary>
        /// ham thuc hien load thong tin logo vao du lieu nguon
        /// </summary>
        /// <param name="dataTable"></param>
        public static void UpdateLogotoDatatable(ref DataTable dataTable)
        {
            LoadImageLogo();
            if (!dataTable.Columns.Contains("LOGO")) dataTable.Columns.Add("LOGO", typeof(byte[]));
            foreach (DataRow dr in dataTable.Rows)
            {
                dr["LOGO"] = globalVariables.SysLogo;
            }
            dataTable.AcceptChanges();
        }
        public static void CreateBarcodeData(ref DataTable dt, string _value)
        {
            try
            {
                //return;
                string ErrMsg="";
                int resolution =Utility.Int32Dbnull( Laygiatrithamsohethong("BARCODE_RESOLUTION","300",false),300);
                int Width =Utility.Int32Dbnull( Laygiatrithamsohethong("BARCODE_WIDTH","600",false),600);
                int Height =Utility.Int32Dbnull( Laygiatrithamsohethong("BARCODE_HEIGHT","200",false),200);
                bool BARCODE_MEMORY = Laygiatrithamsohethong("BARCODE_MEMORY", "0", false) == "1";
                if (!dt.Columns.Contains("BarCode")) dt.Columns.Add("BarCode", typeof(byte[]));
                byte[] bytBarcode = BarcodeLibs.BarcodeCreator.CreateBarcode(BarcodeSymbology.Code128,  _value, resolution, Width, Height, true,BARCODE_MEMORY, ref ErrMsg);
                if (bytBarcode == null)
                    CreateBarcodeDataMarby(ref dt, _value);
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["BarCode"] = bytBarcode;
                    }
                }
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        public static void CreateBarcodeData(ref DataTable dt, string _value, ref byte[] bytBarcode)
        {
            try
            {
                //return;
                string ErrMsg = "";
                int resolution = Utility.Int32Dbnull(Laygiatrithamsohethong("BARCODE_RESOLUTION", "300", false), 300);
                int Width = Utility.Int32Dbnull(Laygiatrithamsohethong("BARCODE_WIDTH", "600", false), 600);
                int Height = Utility.Int32Dbnull(Laygiatrithamsohethong("BARCODE_HEIGHT", "200", false), 200);
                bool BARCODE_MEMORY = Laygiatrithamsohethong("BARCODE_MEMORY", "0", false) == "1";
                if (!dt.Columns.Contains("BarCode")) dt.Columns.Add("BarCode", typeof(byte[]));
                bytBarcode = BarcodeLibs.BarcodeCreator.CreateBarcode(BarcodeSymbology.Code128, _value, resolution, Width, Height, true, BARCODE_MEMORY, ref ErrMsg);
                if (bytBarcode == null)
                    CreateBarcodeDataMarby(ref dt, _value);
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["BarCode"] = bytBarcode;
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        public static void CreateBarcodeDataMarby(ref DataTable dt, string _value)
        {
            //try
            //{
            //    if (!dt.Columns.Contains("BarCode")) dt.Columns.Add("BarCode", typeof(byte[]));
            //    Mabry.Windows.Forms.Barcode.Barcode barcode = new Mabry.Windows.Forms.Barcode.Barcode();
            //    barcode.BackColor = System.Drawing.Color.White;
            //    barcode.BarColor = System.Drawing.Color.Black;
            //    barcode.BarRatio = 2F;
            //    barcode.Name = "barcode";
            //    barcode.Size = new System.Drawing.Size(140, 80);
            //    barcode.Symbology = Mabry.Windows.Forms.Barcode.Barcode.BarcodeSymbologies.Code128;
            //    barcode.Data = _value;
            //    barcode.Font = new Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Pixel, 0); 
            //    byte[] bytBarcode = Utility.GenerateBarCode(barcode);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        dr["BarCode"] = bytBarcode;
            //    }
            //}
            //catch
            //{
            //}
        }
        public static string ConvertDataTableToXML(DataTable dtData)
        {
            DataSet dsData = new DataSet();
            StringBuilder sbSQL;
            StringWriter swSQL;
            string XMLformat;
            try
            {
                sbSQL = new StringBuilder();
                swSQL = new StringWriter(sbSQL);
                dsData.Merge(dtData, true, MissingSchemaAction.AddWithKey);
                dsData.Tables[0].TableName = "SampleDataTable";
                foreach (DataColumn col in dsData.Tables[0].Columns)
                {
                    col.ColumnMapping = MappingType.Attribute;
                }
                dsData.WriteXml(swSQL, XmlWriteMode.WriteSchema);
                XMLformat = sbSQL.ToString();
                return XMLformat;
            }
            catch (Exception sysException)
            {
                throw sysException;
            }
        }

        /// <summary>
        /// Hàm tính tổng của một datatable
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="DataField"></param>
        /// <returns></returns>
        public static string TotaltoDataTable(DataTable dt, string DataField)
        {
            decimal Total = 0;
            //decimal Total = 0;
            if (dt.Rows.Count > 0)
            {
                Total = Utility.DecimaltoDbnull(dt.Compute("Sum(" + DataField + ")", "1=1"), 0);
            }
            return Total.ToString();
        }

        public static string ConvertDate(DateTime dt)
        {
            return dt.ToShortDateString();
        }

        /// <summary>
        /// Hàm convert Image vào mảng chỉ cần truyền vào là Image
        /// </summary>
        /// <param name="imageIn"></param>
        /// <returns></returns>
        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        /// <summary>
        /// Hàm convert từ Byte sang Image
        /// </summary>
        /// <param name="byteArrayIn"></param>
        /// <returns></returns>
        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        /// <summary>
        /// Hàm tự sinh Barcode trong Crystral report chỉ cần đầu vào là tên mã barcode
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        //public static byte[] GenerateBarCode(Barcode barcode)
        //{
        //    MemoryStream ms = new MemoryStream();

        //    barcode.Size = new Size(140, 60);
        //    barcode.Font = new Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point, 0);
        //    barcode.Image().Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

        //    byte[] data = new byte[ms.Length];
        //    ms.Position = 0;
        //    ms.Read(data, 0, (int)ms.Length);
        //    return data;
        //}

        /// <summary>
        /// Hàm thực hiện chỉ cho phép nhập só vào ô textbox
        /// </summary>
        /// <param name="e"></param>
        public static void OnlyDigit(KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
            else if (Char.IsControl(e.KeyChar))
            {
                int ma = (int)e.KeyChar;
                if ((ma == 26) || (ma == 24) || (ma == 3) || (ma == 22) || (ma == 1))
                    e.Handled = true;
            }
        }
        public static void EnableButton(Janus.Windows.EditControls.UIButton cmdControl, bool b_ColVisiable)
        {
            cmdControl.Enabled = b_ColVisiable;
        }
        public static void EnableButton(Button cmdControl, bool b_ColVisiable)
        {
            cmdControl.Enabled = b_ColVisiable;
        }
        /// <summary>
        /// hàm thực hiện việc cho phép ẩn hiện button của toolstrip
        /// </summary>
        /// <param name="cmdControl"></param>
        /// <param name="b_ColVisiable"></param>
        public static void EnableButton(ToolStripButton cmdControl, bool b_ColVisiable)
        {
            cmdControl.Enabled = b_ColVisiable;
        }
        /// <summary>
        /// hàm thực hiện việc hiển tị control của nút button
        /// </summary>
        /// <param name="cmdControl"></param>
        /// <param name="b_ColVisiable"></param>
        public static void VisiableButton(Janus.Windows.EditControls.UIButton cmdControl, bool b_ColVisiable)
        {
            cmdControl.Visible = b_ColVisiable;
        }
        public static void VisiableToolStripButton(System.Windows.Forms.ToolStripButton control, bool b_ColVisiable)
        {
            control.Visible = b_ColVisiable;
        }
        public static void VisiableToolStripSeparator(System.Windows.Forms.ToolStripSeparator control, bool b_ColVisiable)
        {
            control.Visible = b_ColVisiable;
        }
        public static void OnlyDigit(object sender, KeyPressEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
                e.Handled = true;
        }

        public static void OnlyDigitNative(KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0') || (e.KeyChar > '9')) e.Handled = true;
        }
        public static string DocSoThanhChu(string number)
        {
            string strReturn = "";
            string s = number;
            while (s.Length > 0 && s.Substring(0, 1) == "0")
            {
                s = s.Substring(1);
            }
            string[] so = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] hang = new string[] { "", "nghìn", "triệu", "tỷ" };
            int i, j, donvi, chuc, tram;

            bool booAm = false;
            decimal decS = 0;

            try
            {
                decS = Convert.ToDecimal(s.ToString());
            }
            catch { }

            if (decS < 0)
            {
                decS = -decS;
                //s = decS.ToString();
                booAm = true;
            }
            i = s.Length;
            if (i == 0)
                strReturn = so[0] + strReturn;
            else
            {
                j = 0;
                while (i > 0)
                {
                    donvi = Convert.ToInt32(s.Substring(i - 1, 1));
                    i--;
                    if (i > 0)
                        chuc = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        chuc = -1;
                    i--;
                    if (i > 0)
                        tram = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        tram = -1;
                    i--;
                    if ((donvi > 0) || (chuc > 0) || (tram > 0) || (j == 3))
                        strReturn = hang[j] + strReturn;
                    j++;
                    if (j > 3) j = 1;   //Tránh lỗi, nếu dưới 13 số thì không có vấn đề.
                    //Hàm này chỉ dùng để đọc đến 9 số nên không phải bận tâm
                    if ((donvi == 1) && (chuc > 1))
                        strReturn = "mốt " + strReturn;
                    else
                    {
                        if ((donvi == 5) && (chuc > 0))
                            strReturn = "lăm " + strReturn;
                        else if (donvi > 0)
                            strReturn = so[donvi] + " " + strReturn;
                    }
                    if (chuc < 0) break;//Hết số
                    else
                    {
                        if ((chuc == 0) && (donvi > 0)) strReturn = "linh " + strReturn;
                        if (chuc == 1) strReturn = "mười " + strReturn;
                        if (chuc > 1) strReturn = so[chuc] + " mươi " + strReturn;
                    }
                    if (tram < 0) break;//Hết số
                    else
                    {
                        if ((tram > 0) || (chuc > 0) || (donvi > 0)) strReturn = so[tram] + " trăm " + strReturn;
                    }
                    strReturn = " " + strReturn;
                }
            }
            if (booAm) strReturn = "Âm " + strReturn;
            string result = strReturn.Trim().Substring(0, 1).ToUpper() + strReturn.Trim().Substring(1) + " đồng chẵn";
            return result;
        }
        public static string FormatDateTime_giophut_ngay_thang_nam(DateTime dt, string tugio)
        {
           
            string str = string.Format("{0} {1}, {2}", tugio, FormatDateTime_Giophut(dt), FormatDateTime(dt));

            return str;
        }
        public static string FormatDateTime_gio_ngay_thang_nam(DateTime dt, string tugio)
        {
            
            string str = string.Format("{0} {1}, {2}", tugio, FormatDateTime_Gio(dt), FormatDateTime(dt));

            return str;
        }

        public static string FormatDateTime_giophut_ngay_thang_nam(DateTime? dt,string tugio)
        {
            if (!dt.HasValue) return "";
            string str = string.Format("{0} {1}, {2}", tugio, FormatDateTime_Giophut(dt.Value), FormatDateTime(dt.Value));
            
            return str;
        }
        public static string FormatDateTime_gio_ngay_thang_nam(DateTime? dt, string tugio)
        {
            if (!dt.HasValue) return "";
            string str = string.Format("{0} {1}, {2}", tugio, FormatDateTime_Gio(dt.Value), FormatDateTime(dt.Value));

            return str;
        }
        
        public static string FormatDateTime_Giophut(DateTime dt)
        {
            string str = "";
            str += Strings.Right("0" + dt.Hour.ToString(), 2);
            str += " giờ ";
            str += Strings.Right("0" + dt.Minute.ToString(), 2);
            str += " phút ";
            return str;
           
        }
        public static string FormatDateTime_Gio(DateTime dt)
        {
            string str = "";
            str += Strings.Right("0" + dt.Hour.ToString(), 2);
            str += " giờ ";
            return str;

        }
        public static string FormatDateTime(DateTime dt)
        {
            string str = "Ngày ";
            str += Strings.Right("0" + dt.Day.ToString(), 2);
            str += " Tháng ";
            str += Strings.Right("0" + dt.Month.ToString(), 2);
            str += " Năm ";
            str += dt.Year;
            return str;
        }
        public static string FormatDateTime(string ddMMyyyy,string defaultVal)
        {
            if (ddMMyyyy == "") return defaultVal ;
            string str = defaultVal;
            List<string> lstDMY = ddMMyyyy.Split('/').ToList<string>();
            if (lstDMY.Count == 3)
            {
                str = "Ngày ";
                str += Strings.Right("0" + lstDMY[0], 2);
                str += " Tháng ";
                str += Strings.Right("0" + lstDMY[1], 2);
                str += " Năm ";
                str += lstDMY[2];
            }
            return str;
        }
        public static string FormatDateTimeWithLocation(DateTime dt, string location)
        {
            string str = location + ", Ngày ";
            str += Strings.Right("0" + dt.Day.ToString(), 2);
            str += " Tháng ";
            str += Strings.Right("0" + dt.Month.ToString(), 2);
            str += " Năm ";
            str += dt.Year;
            return str;
        }
        public static string FormatDateTimeWithLocation(string dt, string location)
        {
            List<string> lstDMY = dt.Split('/').ToList<string>();
            return string.Format("{0}, Ngày {1} tháng {2} năm {3}", location, lstDMY[0], lstDMY[1], lstDMY[2]);
        }
        /// <summary>
        /// hàm thực hiện việc format datetime với thành phố
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetCityCode1()
        {
            string CiTyCode = "";
            SqlQuery sqlQuery = new Select().From(DmucDiachinh.Schema)
                .Where(DmucDiachinh.Columns.MaDiachinh).IsEqualTo(globalVariables.gv_strNoicapBHYT);
            DmucDiachinh objSurvey = sqlQuery.ExecuteSingle<DmucDiachinh>();
            if (objSurvey != null) CiTyCode = objSurvey.TenDiachinh;
            return CiTyCode;
        }
        public static string FormatDateTimeWithThanhPho(DateTime dt)
        {
            string str = Laygiatrithamsohethong("DIA_DIEM", "Hà Nội", false);
            str += ", Ngày ";
            str += Strings.Right("0" + dt.Day.ToString(), 2);
            str += " Tháng ";
            str += Strings.Right("0" + dt.Month.ToString(), 2);
            str += " Năm ";
            str += dt.Year;
            return str;
        }
        public static void CollapseGrid(GridEX grd)
        {
            grd.GroupMode = grd.GroupMode == GroupMode.Expanded ? GroupMode.Collapsed : GroupMode.Expanded;
            grd.Refetch();
        }
        
        /// <summary>
        /// hàm thực hiện viêc set thông tin cho phần grid của janus
        /// </summary>
        /// <param name="varDataGridView">Control janus cần gán</param>
        /// <param name="DataSource">Datasource muốn gán</param>
        /// <param name="FilterColumn">Cho phép lọc thông tin </param>
        /// <param name="UseDefaultViewAsDataSource">Cho phép defaultview</param>
        /// <param name="RowFilterExpression">Filter theo điều kiện</param>
        /// <param name="DefaultViewSort">Lọc thông tin </param>
        public static void SetDataSourceForDataGridEx(Janus.Windows.GridEX.GridEX varDataGridView, DataTable DataSource,
                                                      bool FilterColumn, bool UseDefaultViewAsDataSource,
                                                      string RowFilterExpression, string DefaultViewSort)
        {
            try
            {
                //varDataGridView.SaveSettings = true;
                //varDataGridView.Font=new Font(FontFamily.);
                if (DataSource == null || DataSource.Columns.Count <= 0)
                {
                    varDataGridView.DataSource = null;
                    return;
                }
                varDataGridView.DataSource = null;
                if (FilterColumn)
                {
                    varDataGridView.FilterMode = FilterMode.Automatic;
                    varDataGridView.FilterRowButtonStyle =
                        Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
                    varDataGridView.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
                    varDataGridView.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;

                }
                else
                {
                    varDataGridView.FilterMode = FilterMode.None;
                }
                if (globalVariables.IsAdmin)
                {
                    if (DataSource.Columns.Contains("IP_MAY_TAO"))
                    {
                        if (!varDataGridView.RootTable.Columns.Contains("IP_MAY_TAO"))
                            varDataGridView.RootTable.Columns.Add("IP_MAY_TAO", ColumnType.Text);
                    }
                    if (DataSource.Columns.Contains("IP_MAC_TAO"))
                    {
                        if (!varDataGridView.RootTable.Columns.Contains("IP_MAC_TAO"))
                            varDataGridView.RootTable.Columns.Add("IP_MAC_TAO", ColumnType.Text);
                    }
                    if (DataSource.Columns.Contains("IP_MAC_SUA"))
                    {
                        if (!varDataGridView.RootTable.Columns.Contains("IP_MAC_SUA"))
                            varDataGridView.RootTable.Columns.Add("IP_MAC_SUA", ColumnType.Text);
                    }
                    if (DataSource.Columns.Contains("IP_MAY_SUA"))
                    {
                        if (!varDataGridView.RootTable.Columns.Contains("IP_MAY_SUA"))
                            varDataGridView.RootTable.Columns.Add("IP_MAY_SUA", ColumnType.Text);
                    }
                }
                if (UseDefaultViewAsDataSource)
                {
                    
                    if (RowFilterExpression.Trim() != "" && RowFilterExpression.Trim() != string.Empty)
                    {
                        DataSource.DefaultView.RowFilter = RowFilterExpression;
                        // DataSource.AcceptChanges();
                    }
                    if (DefaultViewSort.Trim() != "" && DefaultViewSort.Trim() != string.Empty)
                    {
                        DataSource.DefaultView.Sort = DefaultViewSort;
                    }
                    varDataGridView.DataSource = DataSource.DefaultView;
                }
                else
                {
                    varDataGridView.DataSource = DataSource;
                }
                varDataGridView.HideSelection = HideSelection.Highlight;
                varDataGridView.BackColor = Color.Silver;
                varDataGridView.GroupRowFormatStyle.ForeColor = Color.Black;
                varDataGridView.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;

                //varDataGridView.VisualStyle = VisualStyle.Standard;
                varDataGridView.MoveFirst();
                // varDataGridView.AutoSizeColumns();

            }
            catch (Exception ex)
            {
                CatchException(ex);
            }
        }
        public static void UpdateGroup(GridEX _grd, DataTable dtData, string groupColumn, string groupName)
        {
            try
            {
                Int16 b;
                var counts = (from p in dtData.AsEnumerable()
                              select Utility.sDbnull(p[groupColumn], "-1")).Distinct();
                if (counts.Count() >= 2)
                {
                    if (_grd.RootTable.Groups.Count <= 0)
                    {
                        GridEXColumn gridExColumn = _grd.RootTable.Columns[groupColumn];
                        var gridExGroup = new GridEXGroup(gridExColumn);
                        gridExGroup.GroupPrefix = groupName;
                        _grd.RootTable.Groups.Add(gridExGroup);
                    }
                }
                else
                {
                    GridEXColumn gridExColumn = _grd.RootTable.Columns[groupColumn];
                    var gridExGroup = new GridEXGroup(gridExColumn);
                    _grd.RootTable.Groups.Clear();
                }
                _grd.UpdateData();
                _grd.Refresh();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        public static void SetDataSourceForDataGridEx_Basic(Janus.Windows.GridEX.GridEX varDataGridView, DataTable DataSource,
                                                     bool FilterColumn, bool UseDefaultViewAsDataSource,
                                                     string RowFilterExpression, string DefaultViewSort)
        {
            try
            {
                //varDataGridView.SaveSettings = true;
                //varDataGridView.Font=new Font(FontFamily.);
                if (DataSource == null || DataSource.Columns.Count <= 0) return;
                varDataGridView.DataSource = null;
                if (FilterColumn)
                {
                    varDataGridView.FilterMode = FilterMode.Automatic;
                    varDataGridView.FilterRowButtonStyle =
                        Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
                    varDataGridView.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
                    varDataGridView.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;


                }
                else
                {
                    varDataGridView.FilterMode = FilterMode.None;
                }

                if (UseDefaultViewAsDataSource)
                {
                    if (DefaultViewSort.Trim() != "" && DefaultViewSort.Trim() != string.Empty)
                    {
                        DataSource.DefaultView.Sort = DefaultViewSort;
                    }
                    if (RowFilterExpression.Trim() != "" && RowFilterExpression.Trim() != string.Empty)
                    {
                        DataSource.DefaultView.RowFilter = RowFilterExpression;
                    }
                    varDataGridView.DataSource = DataSource.DefaultView;
                }
                else
                {
                    varDataGridView.DataSource = DataSource;
                }
                varDataGridView.HideSelection = HideSelection.Highlight;
                if (varDataGridView.GetDataRows().Length > 0)
                    varDataGridView.MoveFirst();

            }
            catch (Exception ex)
            {
                CatchException(ex);
            }
        }
        public static void SetDataSourceForDataGridEx_BasicNofilter(Janus.Windows.GridEX.GridEX varDataGridView, DataTable DataSource,
                                                    bool FilterColumn, bool UseDefaultViewAsDataSource,
                                                    string RowFilterExpression, string DefaultViewSort)
        {
            try
            {
                if (DataSource == null || DataSource.Columns.Count <= 0) return;
                varDataGridView.DataSource = null;

                if (UseDefaultViewAsDataSource)
                {
                    if (DefaultViewSort.Trim() != "" && DefaultViewSort.Trim() != string.Empty)
                    {
                        DataSource.DefaultView.Sort = DefaultViewSort;
                    }
                    if (RowFilterExpression.Trim() != "" && RowFilterExpression.Trim() != string.Empty)
                    {
                        DataSource.DefaultView.RowFilter = RowFilterExpression;
                    }
                    varDataGridView.DataSource = DataSource.DefaultView;
                }
                else
                {
                    varDataGridView.DataSource = DataSource;
                }
                varDataGridView.HideSelection = HideSelection.Highlight;
                if (varDataGridView.GetDataRows().Length > 0)
                    varDataGridView.MoveFirst();

            }
            catch (Exception ex)
            {
            }
        }
        public static string GetPathExcel(string mabaocao)
        {
            try
            {
                string path = "";
                string filereport = mabaocao;
                //  ReportDocument crpt = new ReportDocument();
                SysReport _object = new Select().From(SysReport.Schema).Where(SysReport.Columns.MaBaocao).IsEqualTo(mabaocao).ExecuteSingle<SysReport>();
                if (_object == null)
                {
                    Utility.ShowMsg("Không tồn tại báo cáo có mã:" + mabaocao + "\nKiểm tra lại chức năng khai báo");
                    return null;
                }
                filereport = Utility.DoTrim(_object.FileChuan);
                //if (Utility.DoTrim(_object.FileRieng) != "")
                //    filereport = Utility.DoTrim(_object.FileRieng);
                string reportFolder = Laygiatrithamsohethong("ExcelFolder", "0", true);
                reportFolder = reportFolder == "0" ? Application.StartupPath + @"\Excel\" : reportFolder;
                filereport = filereport.ToUpper().Replace("XLSX", "").Replace(".RPT", "");
                string fullPath = reportFolder + filereport.ToUpper().Replace(".XLS", "") + ".XLS";


                if (File.Exists(fullPath))
                {
                    return fullPath;
                    //  crpt.Load(fullPath);
                }
                else
                {
                    Utility.ShowMsg("Không tồn tại file báo cáo tại đường dẫn sau\n" + fullPath, "thông báo");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi nạp báo cáo " + mabaocao + "-->\n" + ex.Message);
                //ErrMsg = ex.Message;
                return null;
            }
        }
        public static ReportDocument GetReport(string mabaocao,ref string tieude,ref string fileName,ref SysReport _object)
        {
            try
            {
                globalVariables.logReportCode.Trace(mabaocao);
                tieude = "";
              
                string filereport = mabaocao;
                ReportDocument crpt = new ReportDocument();
                 _object = new Select().From(SysReport.Schema).Where(SysReport.Columns.MaBaocao).IsEqualTo(mabaocao).ExecuteSingle<SysReport>();
                if (_object == null)
                {
                    Utility.ShowMsg("Không tồn tại báo cáo có mã:" + mabaocao + "\nKiểm tra lại chức năng khai báo");
                    return null;
                }
              
                filereport = Utility.DoTrim(_object.FileChuan);
                if (Utility.DoTrim(_object.FileRieng) != "")
                    filereport = Utility.DoTrim(_object.FileRieng);
                fileName = filereport.ToUpper().Replace(".RPT", "") + ".RPT";
                tieude = _object.TieuDe;
                string REPORT_FOLDER = Laygiatrithamsohethong("REPORT_FOLDER", "0", true);
                 REPORT_FOLDER = REPORT_FOLDER == "0" ? Application.StartupPath + @"\reports\" : REPORT_FOLDER;
                 string fullPath = REPORT_FOLDER + filereport.ToUpper().Replace(".RPT", "") + ".RPT";
                if (Laygiatrithamsohethong("REPORT_LOADFROMDLL", "0", false) == "1")
                {
                    Assembly assembly = Assembly.LoadFile(Application.StartupPath + @"\reports.dll");
                    Type[] type = assembly.GetTypes();
                    foreach (Type objType in type)
                    {
                        if (objType.Name.ToUpper() == filereport.ToUpper().Replace(".RPT", ""))
                        {
                            crpt = assembly.CreateInstance(objType.FullName, true) as ReportDocument;
                            break;
                        }
                    }
                }
                else
                {
                    if (File.Exists(fullPath))
                    {
                        crpt.Load(fullPath);
                    }
                    else
                    {
                        Utility.ShowMsg("Không tồn tại file báo cáo tại đường dẫn sau\n" + fullPath, "thông báo");
                        return null;
                    }
                }
                
                return crpt;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi nạp báo cáo " + mabaocao + "-->\n" + ex.Message);
                //ErrMsg = ex.Message;
                return null;
            }
        }
        public static ReportDocument GetReport(string mabaocao, string khogiay,ref string tieude, ref string fileName, ref SysReport _object)
        {
            try
            {
                if (khogiay == "") return GetReport(mabaocao, ref tieude, ref fileName);
                tieude = "";
                
                string filereport = mabaocao;
                ReportDocument crpt = new ReportDocument();
                 _object = new Select().From(SysReport.Schema).Where(SysReport.Columns.MaBaocao).IsEqualTo(mabaocao).ExecuteSingle<SysReport>();
                if (_object == null)
                {
                    Utility.ShowMsg("Không tồn tại báo cáo có mã:" + mabaocao + "\nKiểm tra lại chức năng khai báo");
                    return null;
                }
               
                filereport = Utility.DoTrim(_object.FileChuan);
                if (Utility.DoTrim(_object.FileRieng) != "")
                    filereport = Utility.DoTrim(_object.FileRieng);
                fileName = filereport.ToUpper().Replace(".RPT", "") + ".RPT";
                tieude = _object.TieuDe;
                string REPORT_FOLDER = Laygiatrithamsohethong("REPORT_FOLDER", "0", true);
                REPORT_FOLDER = REPORT_FOLDER == "0" ? Application.StartupPath + @"\reports\" : REPORT_FOLDER;
                string fullPath = REPORT_FOLDER + filereport.ToUpper().Replace(".RPT", "") + ".RPT";
                string fullPath_org = fullPath;
                if (khogiay == "A4")
                {
                    fullPath = fullPath.Replace("A5", "A4");
                    fileName = fileName.Replace("A5", "A4");
                }
                else  if (khogiay == "A5")
                {
                    fullPath = fullPath.Replace("A4", "A5");
                    fileName = fileName.Replace("A4", "A5");
                }
                if (Laygiatrithamsohethong("REPORT_LOADFROMDLL", "0", false) == "1")
                {
                    Assembly assembly = Assembly.LoadFile(Application.StartupPath + @"\reports.dll");
                    Type[] type = assembly.GetTypes();
                    foreach (Type objType in type)
                    {
                        if (objType.Name.ToUpper() == filereport.ToUpper().Replace(".RPT", ""))
                        {
                            crpt = assembly.CreateInstance(objType.FullName, true) as ReportDocument;
                            break;
                        }
                    }
                }
                else
                {
                    if (File.Exists(fullPath))
                    {
                        crpt.Load(fullPath);
                    }
                    else if (File.Exists(fullPath_org))
                    {
                        fileName = Path.GetFileName(fullPath_org);
                        crpt.Load(fullPath_org);
                    }
                    else
                    {
                        Utility.ShowMsg("Không tồn tại file báo cáo tại đường dẫn sau\n" + fullPath, "thông báo");
                        return null;
                    }
                }

                return crpt;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi nạp báo cáo " + mabaocao + "-->\n" + ex.Message);
                //ErrMsg = ex.Message;
                return null;
            }
        }
        public static ReportDocument GetReport(string mabaocao, ref string tieude, ref string fileName)
        {
            try
            {
                globalVariables.logReportCode.Trace(mabaocao);
                tieude = "";

                string filereport = mabaocao;
                ReportDocument crpt = new ReportDocument();
                SysReport _object = new Select().From(SysReport.Schema).Where(SysReport.Columns.MaBaocao).IsEqualTo(mabaocao).ExecuteSingle<SysReport>();
                if (_object == null)
                {
                    Utility.ShowMsg("Không tồn tại báo cáo có mã:" + mabaocao + "\nKiểm tra lại chức năng khai báo");
                    return null;
                }

                filereport = Utility.DoTrim(_object.FileChuan);
                if (Utility.DoTrim(_object.FileRieng) != "")
                    filereport = Utility.DoTrim(_object.FileRieng);
                fileName = filereport.ToUpper().Replace(".RPT", "") + ".RPT";
                tieude = _object.TieuDe;
                string REPORT_FOLDER = Laygiatrithamsohethong("REPORT_FOLDER", "0", true);
                REPORT_FOLDER = REPORT_FOLDER == "0" ? Application.StartupPath + @"\reports\" : REPORT_FOLDER;
                string fullPath = REPORT_FOLDER + filereport.ToUpper().Replace(".RPT", "") + ".RPT";
                if (Laygiatrithamsohethong("REPORT_LOADFROMDLL", "0", false) == "1")
                {
                    Assembly assembly = Assembly.LoadFile(Application.StartupPath + @"\reports.dll");
                    Type[] type = assembly.GetTypes();
                    foreach (Type objType in type)
                    {
                        if (objType.Name.ToUpper() == filereport.ToUpper().Replace(".RPT", ""))
                        {
                            crpt = assembly.CreateInstance(objType.FullName, true) as ReportDocument;
                            break;
                        }
                    }
                }
                else
                {
                    if (File.Exists(fullPath))
                    {
                        crpt.Load(fullPath);
                    }
                    else
                    {
                        Utility.ShowMsg("Không tồn tại file báo cáo tại đường dẫn sau\n" + fullPath, "thông báo");
                        return null;
                    }
                }

                return crpt;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi nạp báo cáo " + mabaocao + "-->\n" + ex.Message);
                //ErrMsg = ex.Message;
                return null;
            }
        }
        public static ReportDocument GetReport(string mabaocao, string khogiay, ref string tieude, ref string fileName)
        {
            try
            {
                if (khogiay == "") return GetReport(mabaocao, ref tieude, ref fileName);
                tieude = "";

                string filereport = mabaocao;
                ReportDocument crpt = new ReportDocument();
                SysReport _object = new Select().From(SysReport.Schema).Where(SysReport.Columns.MaBaocao).IsEqualTo(mabaocao).ExecuteSingle<SysReport>();
                if (_object == null)
                {
                    Utility.ShowMsg("Không tồn tại báo cáo có mã:" + mabaocao + "\nKiểm tra lại chức năng khai báo");
                    return null;
                }

                filereport = Utility.DoTrim(_object.FileChuan);
                if (Utility.DoTrim(_object.FileRieng) != "")
                    filereport = Utility.DoTrim(_object.FileRieng);
                fileName = filereport.ToUpper().Replace(".RPT", "") + ".RPT";
                tieude = _object.TieuDe;
                string REPORT_FOLDER = Laygiatrithamsohethong("REPORT_FOLDER", "0", true);
                REPORT_FOLDER = REPORT_FOLDER == "0" ? Application.StartupPath + @"\reports\" : REPORT_FOLDER;
                string fullPath = REPORT_FOLDER + filereport.ToUpper().Replace(".RPT", "") + ".RPT";
                string fullPath_org = fullPath;
                if (khogiay == "A4")
                {
                    fullPath = fullPath.Replace("A5", "A4");
                    fileName = fileName.Replace("A5", "A4");
                }
                else if (khogiay == "A5")
                {
                    fullPath = fullPath.Replace("A4", "A5");
                    fileName = fileName.Replace("A4", "A5");
                }
                if (Laygiatrithamsohethong("REPORT_LOADFROMDLL", "0", false) == "1")
                {
                    Assembly assembly = Assembly.LoadFile(Application.StartupPath + @"\reports.dll");
                    Type[] type = assembly.GetTypes();
                    foreach (Type objType in type)
                    {
                        if (objType.Name.ToUpper() == filereport.ToUpper().Replace(".RPT", ""))
                        {
                            crpt = assembly.CreateInstance(objType.FullName, true) as ReportDocument;
                            break;
                        }
                    }
                }
                else
                {
                    if (File.Exists(fullPath))
                    {
                        crpt.Load(fullPath);
                    }
                    else if (File.Exists(fullPath_org))
                    {
                        fileName = Path.GetFileName(fullPath_org);
                        crpt.Load(fullPath_org);
                    }
                    else
                    {
                        Utility.ShowMsg("Không tồn tại file báo cáo tại đường dẫn sau\n" + fullPath, "thông báo");
                        return null;
                    }
                }

                return crpt;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi nạp báo cáo " + mabaocao + "-->\n" + ex.Message);
                //ErrMsg = ex.Message;
                return null;
            }
        }
        public static string GetRtfUnicodeEscapedString(string s)
        {
            try
            {
                var sb = new StringBuilder();
                foreach (var c in s)
                {
                    if (c == '\\' || c == '{' || c == '}')
                        sb.Append(@"\" + c);
                    else if (c <= 0x7f)
                        sb.Append(c);
                    else
                        sb.Append("\\u" + Convert.ToUInt32(c) + "?");
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                Utility.CatchException("GetRtfUnicodeEscapedString.Exception", ex);
                return s;

            }

        }
        public static string MoveFile(string source, string dest)
        {
            try
            {
                CreateFolder(dest);
                File.Copy(source, dest,true);
                File.Delete(source);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }
        public static string FixedFolder( string FolderPath)
        {
            string reval = FolderPath;
            if (!reval.EndsWith(@"\"))
                reval += @"\";
            return reval;
        }
        public static Control getActiveControl(ContainerControl ctrl)
        {
            var control = ctrl.ActiveControl;

            while (control is ContainerControl && !control.GetType().Equals(typeof( Janus.Windows.CalendarCombo.CalendarCombo)))
            {
                ContainerControl container = control as ContainerControl;
                control = container.ActiveControl;
            }

            return control;

            //Control tempctrl;
            //if (ctrl is ContainerControl) {
            //ContainerControl conterner = ctrl as ContainerControl;
            //while (temctrl is ContainerControl)
            //{
            //    temctrl = conterner.ActiveControl;
            //}

            //return temctrl;
            //}
            //else
            //    return ctrl;
        }
        public static string Laygiatrithamsohethong(string ParamName, string defaultval, bool fromDB)
        {
            try
            {
                //globalVariables.logThamsohethong.Trace(ParamName);
                fromDB = true;
                string reval = defaultval;
                if (fromDB)
                {
                    SqlQuery sqlQuery =
                        new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo(
                            ParamName);
                    SysSystemParameter objSystemParameter = sqlQuery.ExecuteSingle<SysSystemParameter>();
                    if (objSystemParameter != null) reval = objSystemParameter.SValue;
                }
                else
                {
                    DataRow[] arrDR = globalVariables.gv_dtSysparams.Select(SysSystemParameter.SNameColumn.ColumnName + " ='" + ParamName + "'");
                    if (arrDR.Length > 0) reval = Utility.sDbnull(arrDR[0][SysSystemParameter.SValueColumn.ColumnName]);
                }
                return reval;
            }
            catch
            {
                return defaultval;
            }
        }
        /// <summary>
        /// hàm thực hiện việc cho phép gắn datasource gridex
        /// </summary>
        /// <param name="varDataGridView">tên janus cần gắn</param>
        /// <param name="DataSource">Datasiource bind dữ liệu vào</param>
        /// <param name="FilterColumn">cho phép filter cột thông tin </param>
        /// <param name="UseDefaultViewAsDataSource"></param>
        /// <param name="RowFilterExpression"></param>
        /// <param name="DefaultViewSort">Cho phép sắp xếp theo datasource</param>
        /// <param name="AutoColumnRezie">Tự động giãn theo dữ liệu có trong cột</param>
        public static void SetDataSourceForDataGridEx(Janus.Windows.GridEX.GridEX varDataGridView, DataTable DataSource,
                                                     bool FilterColumn, bool UseDefaultViewAsDataSource,
                                                     string RowFilterExpression, string DefaultViewSort, bool AutoColumnRezie)
        {
            try
            {
                //varDataGridView.SaveSettings = true;
                //varDataGridView.Font=new Font(FontFamily.);
                if (DataSource == null || DataSource.Columns.Count <= 0) return;
                varDataGridView.DataSource = null;
                if (FilterColumn)
                {
                    varDataGridView.FilterMode = FilterMode.Automatic;
                    varDataGridView.FilterRowButtonStyle =
                        Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
                    varDataGridView.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
                    varDataGridView.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;

                }
                else
                {
                    varDataGridView.FilterMode = FilterMode.None;
                }
                if (globalVariables.IsAdmin)
                {
                    if (DataSource.Columns.Contains("IP_MAY_TAO"))
                    {
                        if (!varDataGridView.RootTable.Columns.Contains("IP_MAY_TAO"))
                            varDataGridView.RootTable.Columns.Add("IP_MAY_TAO", ColumnType.Text);
                    }
                    if (DataSource.Columns.Contains("IP_MAC_TAO"))
                    {
                        if (!varDataGridView.RootTable.Columns.Contains("IP_MAC_TAO"))
                            varDataGridView.RootTable.Columns.Add("IP_MAC_TAO", ColumnType.Text);
                    }
                    if (DataSource.Columns.Contains("IP_MAC_SUA"))
                    {
                        if (!varDataGridView.RootTable.Columns.Contains("IP_MAC_SUA"))
                            varDataGridView.RootTable.Columns.Add("IP_MAC_SUA", ColumnType.Text);
                    }
                    if (DataSource.Columns.Contains("IP_MAY_SUA"))
                    {
                        if (!varDataGridView.RootTable.Columns.Contains("IP_MAY_SUA"))
                            varDataGridView.RootTable.Columns.Add("IP_MAY_SUA", ColumnType.Text);
                    }
                }
                if (UseDefaultViewAsDataSource)
                {
                    if (DefaultViewSort.Trim() != "" && DefaultViewSort.Trim() != string.Empty)
                    {
                        DataSource.DefaultView.Sort = DefaultViewSort;
                    }
                    if (RowFilterExpression.Trim() != "" && RowFilterExpression.Trim() != string.Empty)
                    {
                        DataSource.DefaultView.RowFilter = RowFilterExpression;
                        // DataSource.AcceptChanges();
                    }
                    varDataGridView.DataSource = DataSource.DefaultView;
                }
                else
                {
                    varDataGridView.DataSource = DataSource;
                }

                varDataGridView.HideSelection = HideSelection.Highlight;
                varDataGridView.BackColor = Color.Silver;
                varDataGridView.GroupRowFormatStyle.ForeColor = Color.Black;
                varDataGridView.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
                //varDataGridView.VisualStyle = VisualStyle.Standard;
                varDataGridView.MoveFirst();
                if (AutoColumnRezie) varDataGridView.AutoSizeColumns();

            }
            catch (Exception ex)
            {
                // ShowMsg(ex.Message);
            }
        }

        public static void SetDataSourceForDataGridEx_NochangeBackColor(Janus.Windows.GridEX.GridEX varDataGridView, DataTable DataSource,
                                                   bool FilterColumn, bool UseDefaultViewAsDataSource,
                                                   string RowFilterExpression, string DefaultViewSort, bool AutoColumnRezie)
        {
            try
            {
                if (DataSource == null || DataSource.Columns.Count <= 0) return;
                varDataGridView.DataSource = null;
                if (FilterColumn)
                {
                    varDataGridView.FilterMode = FilterMode.Automatic;
                    varDataGridView.FilterRowButtonStyle =
                        Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
                    varDataGridView.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
                    varDataGridView.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;

                   
                }
                else
                {
                    varDataGridView.FilterMode = FilterMode.None;
                }
                if (globalVariables.IsAdmin)
                {
                    if (DataSource.Columns.Contains("IP_MAY_TAO"))
                    {
                        if (!varDataGridView.RootTable.Columns.Contains("IP_MAY_TAO"))
                            varDataGridView.RootTable.Columns.Add("IP_MAY_TAO", ColumnType.Text);
                    }
                    if (DataSource.Columns.Contains("IP_MAC_TAO"))
                    {
                        if (!varDataGridView.RootTable.Columns.Contains("IP_MAC_TAO"))
                            varDataGridView.RootTable.Columns.Add("IP_MAC_TAO", ColumnType.Text);
                    }
                    if (DataSource.Columns.Contains("IP_MAC_SUA"))
                    {
                        if (!varDataGridView.RootTable.Columns.Contains("IP_MAC_SUA"))
                            varDataGridView.RootTable.Columns.Add("IP_MAC_SUA", ColumnType.Text);
                    }
                    if (DataSource.Columns.Contains("IP_MAY_SUA"))
                    {
                        if (!varDataGridView.RootTable.Columns.Contains("IP_MAY_SUA"))
                            varDataGridView.RootTable.Columns.Add("IP_MAY_SUA", ColumnType.Text);
                    }
                }
                if (UseDefaultViewAsDataSource)
                {
                    if (DefaultViewSort.Trim() != "" && DefaultViewSort.Trim() != string.Empty)
                    {
                        DataSource.DefaultView.Sort = DefaultViewSort;
                    }
                    if (RowFilterExpression.Trim() != "" && RowFilterExpression.Trim() != string.Empty)
                    {
                        DataSource.DefaultView.RowFilter = RowFilterExpression;
                    }
                    varDataGridView.DataSource = DataSource.DefaultView;
                }
                else
                {
                    varDataGridView.DataSource = DataSource;
                }
                varDataGridView.HideSelection = HideSelection.Highlight;
                if (varDataGridView.GetDataRows().Length > 0)
                    varDataGridView.MoveFirst();
                if (AutoColumnRezie) varDataGridView.AutoSizeColumns();

            }
            catch (Exception ex)
            {
                // ShowMsg(ex.Message);
            }
        }

        /// <summary>
        /// hàm thực hiện việc định dạng format của giờ
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string FormatHour(DateTime dt)
        {
            string str = Strings.Right("0" + dt.Hour.ToString(), 2) + " Giờ ";
            str += Strings.Right("0" + dt.Minute.ToString(), 2);
            str += " Phút ";

            return str;
        }

        /// <summary>
        /// Định dạng ngày tháng của việt nam trong Crystral report 
        /// </summary>
        /// <param name="Todate"></param>
        /// <param name="FromDate"></param>
        /// <returns></returns>
        public static string FromToDateTime(string FromDate, string Todate)
        {
            string str = "Từ ngày ";
            str += FromDate;
            str += " đến ngày ";
            str += Todate;
            return str;
        }

        public static string GetYear(DateTime dt)
        {
            return dt.Year.ToString();
        }

        public static DataTable ReadXMLtoDatatable(string xmlPath, string DataTableName)
        {
            DataSet dsStore = new DataSet();
            dsStore.ReadXml(xmlPath);
            return dsStore.Tables[DataTableName];
        }

        public static byte[] ToByteArray(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }

        public static decimal ConvertObjectToDecimal(object obj)
        {
            decimal Total = 0;
            try
            {
                Total = Convert.ToDecimal(obj);
            }
            catch (Exception ex)
            {
            }
            return Total;
        }

        public static bool CompareToDate(DateTime dt1, DateTime dt2)
        {
            return (DateTime.Compare(dt1, dt2) > 0 ? true : false);
        }

        public static bool IsLocalIpAddress(string host)
        {
            try
            {
                // get host IP addresses
                IPAddress[] hostIPs = Dns.GetHostAddresses(host);
                // get local IP addresses
                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

                // test if any host IP equals to any local IP or to localhost
                foreach (IPAddress hostIP in hostIPs)
                {
                    // is localhost
                    if (IPAddress.IsLoopback(hostIP)) return true;
                    // is local address
                    foreach (IPAddress localIP in localIPs)
                    {
                        if (hostIP.Equals(localIP)) return true;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// Hàm chuyển đổi từ ảnh sang mảng
        /// </summary>
        /// <param name="imageToConvert"></param>
        /// <param name="formatOfImage"></param>
        /// <returns></returns>
        public static byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert, ImageFormat formatOfImage)
        {
            if (imageToConvert == null || imageToConvert.Equals(DBNull.Value)) return null;
            byte[] Ret;
            try
            {
                using (var ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    Ret = ms.ToArray();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Ret;
        }

        public static List<int> FindIndex(object source, List<int> values)
        {
            var result = new List<int>();
            var table = source as DataTable;
            if (table != null)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    foreach (var value in values)
                    {
                        if (table.Rows[i][0].ToString().Equals(value.ToString()))
                            result.Add(i);
                    }
                }
            }
            return result;
        }

        public static List<int> FindIndex(object source, DataTable values)
        {
            var result = new List<int>();
            var table = source as DataTable;
            if (table != null)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    foreach (DataRow value in values.Rows)
                    {
                        if (table.Rows[i][0].ToString().Equals(value.ToString()))
                            result.Add(i);
                    }
                }
            }
            return result;
        }

        public static void XMLtoDataTable(string Address, string DataBaseName, string User_ID, string Password,
                                          string MutiServiceType, string ConnectLab, string strBranchID)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            //create element root
            XmlElement rootNode = xmlDoc.CreateElement("NewDataSet");
            xmlDoc.InsertBefore(xmlDeclaration, xmlDoc.DocumentElement);
            xmlDoc.AppendChild(rootNode);
            // Create a new <Category> element and add it to the root node
            XmlElement parentNode = xmlDoc.CreateElement("Config");
            xmlDoc.DocumentElement.PrependChild(parentNode);
            XmlElement SERVERADDRESS = xmlDoc.CreateElement("SERVERADDRESS");
            XmlElement DATABASE_ID = xmlDoc.CreateElement("DATABASE_ID");
            XmlElement USERNAME = xmlDoc.CreateElement("USERNAME");
            XmlElement PASSWORD = xmlDoc.CreateElement("PASSWORD");
            XmlElement MUTISERVICETYPE = xmlDoc.CreateElement("MUTISERVICETYPE");
            XmlElement CONNECTLAB = xmlDoc.CreateElement("CONNECTLAB");
            XmlElement BranchID = xmlDoc.CreateElement("BranchID");
            //XmlElement CONNNECTED = xmlDoc.CreateElement("CONNNECTED");
            parentNode.AppendChild(SERVERADDRESS);
            parentNode.AppendChild(DATABASE_ID);
            parentNode.AppendChild(USERNAME);
            parentNode.AppendChild(PASSWORD);
            parentNode.AppendChild(MUTISERVICETYPE);
            parentNode.AppendChild(CONNECTLAB);
            parentNode.AppendChild(BranchID);
            //parentNode.AppendChild(CONNNECTED);

            // save the value of the fields into the nodes
            XmlText xmlServer = xmlDoc.CreateTextNode(Address);
            XmlText xmlDataBase = xmlDoc.CreateTextNode(DataBaseName);
            XmlText xmlUser = xmlDoc.CreateTextNode(User_ID);
            XmlText xmlPassword = xmlDoc.CreateTextNode(Password);
            XmlText xmlMutiServiceType = xmlDoc.CreateTextNode(MutiServiceType);
            XmlText xmlConectLab = xmlDoc.CreateTextNode(ConnectLab);
            XmlText xmlBranchID = xmlDoc.CreateTextNode(strBranchID);
            //XmlText xmlCONNNECTED = xmlDoc.CreateTextNode(strBranchID);


            ////
            SERVERADDRESS.AppendChild(xmlServer);
            DATABASE_ID.AppendChild(xmlDataBase);
            USERNAME.AppendChild(xmlUser);
            PASSWORD.AppendChild(xmlPassword);
            MUTISERVICETYPE.AppendChild(xmlMutiServiceType);
            CONNECTLAB.AppendChild(xmlConectLab);
            BranchID.AppendChild(xmlBranchID);
            xmlDoc.Save("LABLinkConfig.XML");
        }

        public static bool IsValidEmailAddress(string email)
        {
            try
            {
                MailAddress ma = new MailAddress(email);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string Chuanhoa(string str)
        {
            string st = str.Trim().ToLower();
            string st1 = null;
            string FieldName = "";
            if (!string.IsNullOrEmpty(str))
            {
                while (st.Trim().Length != 0)
                {
                    st += " ";
                    int i = st.IndexOf(" ");
                    string d = st.Substring(0, i);
                    d = char.ToUpper(d[0]) + d.Substring(1);
                    st = st.Substring(i + 1).Trim();
                    st1 += d.Trim() + " ";
                }
                FieldName = st1.TrimEnd();
            }
            return FieldName;
        }

        public static string UpperCharacter(string str)
        {
            string st = str.Trim().ToUpper();


            return st;
        }

        public static bool valiEmail(string str)
        {
            Regex rgxemail = new Regex(@"^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-z0-9]{2,4}$");
            if (!rgxemail.IsMatch(str))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Returns a null if the entered string was null, otherwise it trims the entered string.
        /// </summary>
        public static string Trim(string value)
        {
            return value == null ? null : value.Trim();
        }

        /// <summary>
        /// Trims the string then returns if the trimmed string is null or empty.
        /// </summary>
        public static bool IsTrimmedNullOrEmpty(string value)
        {
            return String.IsNullOrEmpty(Trim(value));
        }

        /// <summary>
        /// Kiểm tra chuỗi nhập vào có empty không.
        /// </summary>
        /// <param name="s">Input String to check</param>
        /// <returns >boolean</returns>
        public static bool IsEmpty(string s)
        {
            bool b = false;
            try
            {
                b = string.IsNullOrEmpty(s);
                return b;
            }
            catch (Exception)
            {
                return b;
            }
        }

        /// <summary>
        /// ham thuc hien xoa thong tin cua datatable cua mot truong, voi dieu kien truyen vao
        /// </summary>
        /// <param name="dt_Datatable">datatable truyen vao</param>
        /// <param name="FieldName">truong so sanh</param>
        /// <param name="Condition">gia tri truyen vao</param>
        public static void DeleteFetchDataRow(ref DataTable dt_Datatable, string FieldName, string Condition)
        {
            foreach (DataRow dr in dt_Datatable.Rows)
            {
                if (dr[FieldName].ToString().Equals(Condition))
                {
                    dr.Delete();
                    break;
                }
            }
            dt_Datatable.AcceptChanges();
        }

        public static bool IsDigit(object obj)
        {
            bool b = false;
            try
            {
                string s = chuanhoaDecimal(obj.ToString());
                b = Convert.ToDecimal(s) != null;
                //chay thu lai cai SVN xem OK khong ????
                return b;
            }
            catch (Exception)
            {
                return b;
            }
        }

        public static bool ValiNumber(string str)
        {
            Regex rgxstring = new Regex(@"^\d*[0-9]?$");
            if (!rgxstring.IsMatch(str))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// hàm cho phép thực hiện công việc 
        /// </summary>
        /// <param name="q">câu lệnh sql của subsonic</param>
        /// <returns></returns>
        public static bool AllowDelete(SqlQuery q)
        {
            int recordcount = q.GetRecordCount();
            if (recordcount > 0) return false;
            else return true;
        }

        public static DataGridViewTextBoxColumn createTextColumn(string psName, string psHeaderText,
                                                                 string psDataPropertyName, bool pbVisible,
                                                                 bool pbReadOnly)
        {
            return createTextColumn(psName, psHeaderText, psDataPropertyName, 0, pbVisible, pbReadOnly);
        }

        /// <summary>
        /// Tạo ra Text Column co Data Grid View
        /// </summary>
        /// <param name="psName">Tên cột</param>
        /// <param name="psHeaderText">Tiêu đề cột</param>
        /// <param name="psDataPropertyName">Thuộc tính Dữ liệu</param>
        /// <param name="piColumnWidth">Độ rộng của cột</param>
        /// <param name="pbVisible">True:Hiện, False:Ẩn</param>
        /// <param name="pbReadOnly">True:ReadOnly</param>
        /// <returns></returns>
        public static DataGridViewTextBoxColumn createTextColumn(string psName, string psHeaderText,
                                                                 string psDataPropertyName, int piColumnWidth,
                                                                 bool pbVisible,
                                                                 bool pbReadOnly)
        {
            DataGridViewTextBoxColumn pmColumn;
            using (pmColumn = new DataGridViewTextBoxColumn
            {
                Name = psName,
                HeaderText = psHeaderText,
                DataPropertyName = psDataPropertyName,
                Visible = pbVisible,
                ReadOnly = pbReadOnly
            })
                if (piColumnWidth != 0)
                {
                    pmColumn.Width = piColumnWidth;
                }

            return pmColumn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="psName"></param>
        /// <param name="psHeaderText"></param>
        /// <param name="psDataPropertyName"></param>
        /// <param name="pbVisible"></param>
        /// <param name="pbReadOnly"></param>
        /// <param name="hidden"></param>
        /// <returns></returns>
        public static DataGridViewTextBoxColumn createTextColumn(string psName, string psHeaderText,
                                                                 string psDataPropertyName, bool pbVisible,
                                                                 bool pbReadOnly, string hidden)
        {
            return createTextColumn(psName, psHeaderText, psDataPropertyName, 0, pbVisible, pbReadOnly, hidden);
        }

        public static DataGridViewTextBoxColumn createTextColumn(string psName, string psHeaderText,
                                                                 string psDataPropertyName, int piColumnWidth,
                                                                 bool pbVisible,
                                                                 bool pbReadOnly, string hidden)
        {
            DataGridViewTextBoxColumn pmColumn;
            using (pmColumn = new DataGridViewTextBoxColumn
            {
                Name = psName,
                HeaderText = psHeaderText,
                ToolTipText = hidden,
                DataPropertyName = psDataPropertyName,
                Visible = pbVisible,
                ReadOnly = pbReadOnly
            })
                if (piColumnWidth != 0)
                {
                    pmColumn.Width = piColumnWidth;
                }

            return pmColumn;
        }

        public static DataGridViewCheckBoxColumn createCheckBoxColumn(string psName, string psHeaderText,
                                                                      string psDataPropertyName, bool pbVisible,
                                                                      bool pbReadOnly)
        {
            return createCheckBoxColumn(psName, psHeaderText, psDataPropertyName, 0, pbVisible, pbReadOnly);
        }

        public static DataGridViewCheckBoxColumn createCheckBoxColumn(string psName, string psHeaderText,
                                                                      string psDataPropertyName, int piColumnWidth,
                                                                      bool pbVisible,
                                                                      bool pbReadOnly)
        {
            DataGridViewCheckBoxColumn pmColumn;
            using (pmColumn = new DataGridViewCheckBoxColumn
            {
                Name = psName,
                HeaderText = psHeaderText,
                DataPropertyName = psDataPropertyName,
                Visible = pbVisible,
                ReadOnly = pbReadOnly
            })
                if (piColumnWidth != 0)
                {
                    pmColumn.Width = piColumnWidth;
                }

            return pmColumn;
        }

        public static string ReplaceStr(string mystring)
        {
            string temp = mystring.Replace(Environment.NewLine, string.Empty);
            return temp;
        }

        public static DataGridViewCheckBoxColumn createCheckBoxColumn(string psName, string psHeaderText,
                                                                      string psDataPropertyName, bool pbVisible,
                                                                      bool pbReadOnly, string hidden)
        {
            return createCheckBoxColumn(psName, psHeaderText, psDataPropertyName, 0, pbVisible, pbReadOnly, hidden);
        }

        public static string GenerateInt(SqlQuery q)
        {
            // SqlQuery q = new Select(Aggregate.Max(LDepartment.Columns.IntOrder)).From(LDepartment.Schema.TableName);
            int record = q.ExecuteScalar<int>();
            return (record + 1).ToString();
        }

        public static DataGridViewCheckBoxColumn createCheckBoxColumn(string psName, string psHeaderText,
                                                                      string psDataPropertyName, int piColumnWidth,
                                                                      bool pbVisible,
                                                                      bool pbReadOnly, string hidden)
        {
            DataGridViewCheckBoxColumn pmColumn;
            using (pmColumn = new DataGridViewCheckBoxColumn
            {
                Name = psName,
                HeaderText = psHeaderText,
                ToolTipText = hidden,
                DataPropertyName = psDataPropertyName,
                Visible = pbVisible,
                ReadOnly = pbReadOnly
            })
                if (piColumnWidth != 0)
                {
                    pmColumn.Width = piColumnWidth;
                }

            return pmColumn;
        }

        /// <summary>
        /// Thu hien lay du lieu tu 1 file XML 
        /// </summary>
        /// <param name="FileName">Ten file xml</param>
        /// <param name="FieldName">Truong file cua xml</param>
        /// <param name="ChildName">TRuong can lay cua file xml</param>
        /// <returns></returns>
        public static string ReadXMLtoString(string FileName, string FieldName, string ChildName)
        {
            string strxml = null;

            try
            {
                // XmlTextReader reader = new XmlTextReader();
                if (File.Exists(FileName))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(FileName);
                    XmlNodeList bookList = doc.GetElementsByTagName(FieldName);
                    foreach (XmlNode node in bookList)
                    {
                        XmlElement bookElement = (XmlElement)node;
                        strxml = bookElement.GetElementsByTagName(ChildName)[0].InnerText;
                        strxml = strxml;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return strxml;
        }

        public static int Config()
        {
            try
            {
                return Convert.ToInt32(Utility.ReadXMLtoString("LABLinkConfig.XML", "Config", "MUTISERVICETYPE"));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Ham thuc hien lấy dữ liệu tình trạng tham số có cho phép kết nối 
        /// lablink hay không? Nếu là 1 là connect và 0 là disconnect
        /// </summary>
        /// <returns></returns>
        public static int SysParamenterStatus()
        {
            try
            {
                return Convert.ToInt32(Utility.ReadXMLtoString("LABLinkConfig.XML", "Config", "CONNECTLAB"));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Hàm thực hiện so sách giá trị truyền vào với giá trị Value của Combobox, và trả lại Index của Combobox
        /// </summary>
        /// <param name="cbo">Giá trị Combobox</param>
        /// <param name="Value">Giá trị Của combobox</param>
        /// <param name="ValueToCompare">Giá trị Của giá trị so sách</param>
        public static void CompareToCombox(ComboBox FillCombox, string Value, int ValueToCompare)
        {
            try
            {
                if (FillCombox.Items.Count > 0)
                {
                    //MessageBox.Show("Ok");
                    for (int i = 0; i < FillCombox.Items.Count; i++)
                    {
                        DataRowView dr = (DataRowView)FillCombox.Items[i];
                        if (Convert.ToInt16(dr[Value].ToString()) == ValueToCompare)
                        {
                            FillCombox.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FillCombox.SelectedIndex = -1;
            }
        }

        public static decimal TotalToDatatable(DataTable dataTable, string FieldName, string Filter)
        {
            decimal total = 0;
            try
            {
                total = Utility.DecimaltoDbnull(dataTable.Compute("SUM(" + FieldName + ")", Filter), 0);
            }
            catch (Exception ex)
            {
            }
            return total;
        }

        /// <summary>
        /// hamg lay thong tin vao combox
        /// </summary>
        /// <param name="FillCombox"></param>
        /// <param name="Value"></param>
        /// <param name="ValueToCompare"></param>
        public static void CompareToCombox(ComboBox FillCombox, string Value, string ValueToCompare)
        {
            try
            {
                if (FillCombox.Items.Count > 0)
                {
                    //MessageBox.Show("Ok");
                    for (int i = 0; i < FillCombox.Items.Count; i++)
                    {
                        DataRowView dr = (DataRowView)FillCombox.Items[i];
                        if (dr[Value].ToString().Equals(ValueToCompare))
                        {
                            FillCombox.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FillCombox.SelectedIndex = -1;
            }
        }

        public static bool ValiFloat(string str)
        {
            //Regex rgxstring = new Regex(@"^\d*[0-9](|,\d*[0-9])?$");
            Regex rgxString = new Regex(@"^\d*[0-9](|,\d*[0-9]|.\d*[0-9])?$");
            if (!rgxString.IsMatch(str))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// hàm thực hiện đưa vào thực hiện báo hiển thị lỗi dùng control errorProvider
        /// </summary>
        /// <param name="errorProvider">Đối tượng errorProvider</param>
        /// <param name="objValidate">Đối tượng hiển thị lỗi</param>
        /// <param name="messageError">Message muốn hiển thị</param>
        public static void SetMsgError(System.Windows.Forms.ErrorProvider errorProvider,
                                           System.Windows.Forms.Control objValidate, string messageError)
        {
            errorProvider.SetError(objValidate, messageError);
        }

        public static void ResetMessageError(System.Windows.Forms.ErrorProvider errorProvider)
        {
            errorProvider.Clear();
        }

        /// <summary>
        /// hàm thực hiện relace string,break line
        /// </summary>
        /// <param name="yourString"></param>
        /// <returns></returns>
        public static string ReplaceString(string yourString)
        {
            return yourString.Replace("\r\n", "\n").Replace("\r", "\n");
        }

        /// <summary>
        /// hàm thực hiện bỏ dấu thông tin của hàm
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string UnSignedCharacter(string s)
        {
            int i = 0;
            string CH = null;
            //###################################################
            if (!string.IsNullOrEmpty(s.Trim()))
            {
                for (i = 1; i <= s.Length; i++)
                {
                    CH = Strings.Mid(s, i, 1);
                    switch (CH)
                    {
                        case "â":
                        case "ă":
                        case "ấ":
                        case "ầ":
                        case "ậ":
                        case "ẫ":
                        case "ẩ":
                        case "ắ":
                        case "ằ":
                        case "ẵ":
                        case "ẳ":
                        case "ặ":
                        case "á":
                        case "à":
                        case "ả":
                        case "ã":
                        case "ạ":
                            s = s.Replace(CH, "a");
                            break;
                        case "Â":
                        case "Ă":
                        case "Ấ":
                        case "Ầ":
                        case "Ậ":
                        case "Ẫ":
                        case "Ẩ":
                        case "Ắ":
                        case "Ằ":
                        case "Ẵ":
                        case "Ẳ":
                        case "Ặ":
                        case "Á":
                        case "À":
                        case "Ả":
                        case "Ã":
                        case "Ạ":
                            s = s.Replace(CH, "A");
                            break;
                        case "ó":
                        case "ò":
                        case "ỏ":
                        case "õ":
                        case "ọ":
                        case "ô":
                        case "ố":
                        case "ồ":
                        case "ổ":
                        case "ỗ":
                        case "ộ":
                        case "ơ":
                        case "ớ":
                        case "ờ":
                        case "ợ":
                        case "ở":
                        case "ỡ":
                            s = s.Replace(CH, "o");
                            break;
                        case "Ó":
                        case "Ò":
                        case "Ỏ":
                        case "Õ":
                        case "Ọ":
                        case "Ô":
                        case "Ố":
                        case "Ồ":
                        case "Ổ":
                        case "Ỗ":
                        case "Ộ":
                        case "Ơ":
                        case "Ớ":
                        case "Ờ":
                        case "Ợ":
                        case "Ở":
                        case "Ỡ":
                            s = s.Replace(CH, "O");
                            break;
                        case "ư":
                        case "ứ":
                        case "ừ":
                        case "ự":
                        case "ử":
                        case "ữ":
                        case "ù":
                        case "ú":
                        case "ủ":
                        case "ũ":
                        case "ụ":
                            s = s.Replace(CH, "u");
                            break;
                        case "Ư":
                        case "Ứ":
                        case "Ừ":
                        case "Ự":
                        case "Ử":
                        case "Ữ":
                        case "Ù":
                        case "Ú":
                        case "Ủ":
                        case "Ũ":
                        case "Ụ":
                            s = s.Replace(CH, "U");
                            break;
                        case "ê":
                        case "ế":
                        case "ề":
                        case "ệ":
                        case "ể":
                        case "ễ":
                        case "è":
                        case "é":
                        case "ẻ":
                        case "ẽ":
                        case "ẹ":
                            s = s.Replace(CH, "e");
                            break;
                        case "Ê":
                        case "Ế":
                        case "Ề":
                        case "Ệ":
                        case "Ể":
                        case "Ễ":
                        case "È":
                        case "É":
                        case "Ẻ":
                        case "Ẽ":
                        case "Ẹ":
                            s = s.Replace(CH, "E");
                            break;
                        case "í":
                        case "ì":
                        case "ị":
                        case "ỉ":
                        case "ĩ":
                            s = s.Replace(CH, "i");
                            break;
                        case "Í":
                        case "Ì":
                        case "Ỉ":
                        case "Ĩ":
                        case "Ị":
                            s = s.Replace(CH, "I");
                            break;
                        case "ý":
                        case "ỳ":
                        case "ỵ":
                        case "ỷ":
                        case "ỹ":
                            s = s.Replace(CH, "y");
                            break;
                        case "Ý":
                        case "Ỳ":
                        case "Ỵ":
                        case "Ỷ":
                        case "Ỹ":
                            s = s.Replace(CH, "Y");
                            break;
                        case "đ":
                            s = s.Replace(CH, "d");
                            break;
                        case "Đ":
                            s = s.Replace(CH, "D");
                            break;
                    }
                }
            }
            return s;
        }

        public static byte GetPaymentType(string VoteName)
        {
            byte v_PaymentTypeId = 0;
            switch (VoteName)
            {
                case "REG":
                    v_PaymentTypeId = 1;
                    break;
                case "ASSIGN":
                    v_PaymentTypeId = 2;
                    break;
                case "DRUG":
                    v_PaymentTypeId = 3;
                    break;
                case "VT":
                    v_PaymentTypeId = 5;
                    break;
                case "BED":
                    v_PaymentTypeId = 4;
                    break;
            }
            return v_PaymentTypeId;
        }

        public static DataTable LoadExcelDataIntoTable(string fileName, string SheetName)
        {
            DataTable dtData = new DataTable();
            try
            {
                SheetName = SheetName.Replace("[", "").Replace("]", "").Replace("(", "").Replace(")", "").Replace("$",
                                                                                                                  "");
                String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";" +
                                           "Extended Properties=Excel 8.0;";
                OleDbConnection objConn = new OleDbConnection(sConnectionString);
                objConn.Open();
                //Đọc dữ liệu

                OleDbCommand objCmd = new OleDbCommand("SELECT * FROM [" + SheetName + "$]", objConn);
                OleDbDataAdapter objAdapter = new OleDbDataAdapter();
                objAdapter.SelectCommand = objCmd;
                objAdapter.Fill(dtData);
                dtData.TableName = SheetName;
                return dtData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion
    }
    public class NetworkConnection : IDisposable
    {
        string _networkName;

        public NetworkConnection(string networkName,
            NetworkCredential credentials)
        {
            _networkName = networkName;

            var netResource = new NetResource()
            {
                Scope = ResourceScope.GlobalNetwork,
                ResourceType = ResourceType.Disk,
                DisplayType = ResourceDisplaytype.Share,
                RemoteName = networkName
            };

            var userName = string.IsNullOrEmpty(credentials.Domain)
                ? credentials.UserName
                : string.Format(@"{0}\{1}", credentials.Domain, credentials.UserName);

            var result = WNetAddConnection2(
                netResource,
                credentials.Password,
                userName,
                0);

            if (result != 0)
            {
                throw new Win32Exception(result, "Error connecting to remote share");
            }
        }

        ~NetworkConnection()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            WNetCancelConnection2(_networkName, 0, true);
        }

        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(NetResource netResource,
            string password, string username, int flags);

        [DllImport("mpr.dll")]
        private static extern int WNetCancelConnection2(string name, int flags,
            bool force);
    }

    [StructLayout(LayoutKind.Sequential)]
    public class NetResource
    {
        public ResourceScope Scope;
        public ResourceType ResourceType;
        public ResourceDisplaytype DisplayType;
        public int Usage;
        public string LocalName;
        public string RemoteName;
        public string Comment;
        public string Provider;
    }

    public enum ResourceScope : int
    {
        Connected = 1,
        GlobalNetwork,
        Remembered,
        Recent,
        Context
    };

    public enum ResourceType : int
    {
        Any = 0,
        Disk = 1,
        Print = 2,
        Reserved = 8,
    }

    public enum ResourceDisplaytype : int
    {
        Generic = 0x0,
        Domain = 0x01,
        Server = 0x02,
        Share = 0x03,
        File = 0x04,
        Group = 0x05,
        Network = 0x06,
        Root = 0x07,
        Shareadmin = 0x08,
        Directory = 0x09,
        Tree = 0x0a,
        Ndscontainer = 0x0b
    }
    public class TraceInfor
    {
        public int ID = -1;
        public string BranchID = "Unknown";
        public string UserName = "Unknown";
        public string CreatedDate = System.DateTime.Now.ToShortDateString();
        public string IPAddress = "127.0.0.1";
        public string SubSystemName = "Unknown";
        public int FunctionID = -1;
        public string TableName = "Unknown";
        public string Desc = "Unknown";
        public Int16 LOT = 0;
        public string FunctionName = "";
        public string ComputerName = "";
        public string DLLName = "";
        public string AccountName = "";

        public TraceInfor()
        {
        }

        public TraceInfor(string BranchID, string UserName, string Date, string IPAddress, string DLLName,
                          string SubSystemName, int FunctionID, string FunctionName, string ComputerName,
                          string AccountName)
        {
            this.BranchID = BranchID;
            this.UserName = UserName;
            this.CreatedDate = Date;
            this.IPAddress = IPAddress;
            this.SubSystemName = SubSystemName;
            this.FunctionID = FunctionID;
            this.DLLName = DLLName;
            this.FunctionID = FunctionID;
            this.FunctionName = FunctionName;
            this.ComputerName = ComputerName;
            this.AccountName = AccountName;
        }

        ///<summary>
        ///<para>Phương thức khởi tạo có tham số</para>
        ///</summary>   
        /// <param name="BranchID"><para>Mã đơn vị quản lý</para></param>     
        /// <param name="UserName"><para>User đang thao tác với chức năng. Là User đăng nhập</para></param>     
        /// <param name="Date"><para>Ngày thao tác: DateTime.Now.ToShortDateString()<see cref="DateTime.Now"/></para></param>     
        /// <param name="IPAddress"><para>Địa chỉ của máy tính chạy Client</para></param>     
        /// <param name="SubSystemName"><para>Tên phân hệ(Tên DLL trong winform hoặc tên bằng chữ trên webform).System.Reflection.Assembly.GetExecutingAssembly().FullName;</para></param>     
        /// <param name="FunctionID"><para>Tên hàm(Tên form trong winform hoặc tên bằng chữ của chức năng trong webform)</para></param>     
        /// <param name="Table_Name"><para>Tên bảng bị tác động thay đổi dữ liệu</para></param>     
        /// <param name="Desc"><para>Mô tả thao tác chức năng. Ví dụ:"Thêm mới nước có mã: " + objInfor.pMA_NUOC</para></param>     
        /// <param name="LO"><para></para>Xác định việc cập nhật thông tin vào CSDL theo một lô hay không. </param>     
        public TraceInfor(string BranchID, string UserName, string Date, string IPAddress, string DLLName,
                          string SubSystemName, int FunctionID, string FunctionName, string ComputerName,
                          string AccountName, string Table_Name, string Desc, Int16 LOT)
        {
            this.BranchID = BranchID;
            this.UserName = UserName;
            this.CreatedDate = Date;
            this.IPAddress = IPAddress;
            this.SubSystemName = SubSystemName;
            this.DLLName = DLLName;
            this.FunctionID = FunctionID;
            this.FunctionName = FunctionName;
            this.ComputerName = ComputerName;
            this.AccountName = AccountName;
            this.TableName = Table_Name;
            this.Desc = Desc;
            this.LOT = LOT;
        }

        ///<summary>
        ///<para>set hoặc get giá trị ID của Trace</para>
        ///</summary>  
        public int pID
        {
            set { ID = value; }
            get { return ID; }
        }

        ///<summary>
        ///<para>set hoặc get giá trị cho mã đơn vị quản lý</para>
        ///</summary>  
        public string pBranchID
        {
            set { BranchID = value; }
            get { return BranchID; }
        }

        ///<summary>
        ///<para>set hoặc get giá trị cho Người thao tác(Username)</para>
        ///</summary>  
        public string pUserName
        {
            set { UserName = value; }
            get { return UserName; }
        }

        ///<summary>
        ///<para>set hoặc get giá trị cho ngày thao tác</para>
        ///</summary>  
        public string pCreatedDate
        {
            set { CreatedDate = value; }
            get { return CreatedDate; }
        }

        ///<summary>
        ///<para>set hoặc get giá trị cho địa chỉ máy tính chạy Client</para>
        ///</summary>  
        public string pIPAddress
        {
            set { IPAddress = value; }
            get { return IPAddress; }
        }

        ///<summary>
        ///<para>set hoặc get giá trị cho tên phân hệ</para>
        ///</summary>  
        public string pSubSystemName
        {
            set { SubSystemName = value; }
            get { return SubSystemName; }
        }

        ///<summary>
        ///<para>set hoặc get giá trị cho tên chức năng</para>
        ///</summary>  
        public int pFunctionID
        {
            set { FunctionID = value; }
            get { return FunctionID; }
        }

        ///<summary>
        ///<para>set hoặc get giá trị cho tên bảng bị tác động thay đổi dữ liệu bởi nghiệp vụ.</para>
        ///</summary>  
        public string pTableName
        {
            set { TableName = value; }
            get { return TableName; }
        }

        ///<summary>
        ///<para>set hoặc get giá trị cho mô tả thao tác</para>
        ///</summary>  
        public string pDesc
        {
            set { Desc = value; }
            get { return Desc; }
        }

        ///<summary>
        ///<para>set hoặc get giá trị cho việc cập nhật theo Lô</para>
        ///</summary>  
        public Int16 pLO
        {
            set { LOT = value; }
            get { return LOT; }
        }
    }

    //----------------------------------------------------------------------------
    //-----------------------------NEW CLASS--------------------------------------
    //----------------------------------------------------------------------------
    ///<summary>
    ///<para>Lớp Trace dùng để lưu vết thao tác của người dùng</para>
    ///</summary> 
    public class TraceController
    {
        public TraceInfor Infor;

        ///<summary>
        ///<para>Phương thức khởi tạo không có tham số. Các thuộc tính lấy giá trị default</para>
        ///</summary>   
        public TraceController()
        {
        }

        public TraceController(TraceInfor Infor)
        {
            this.Infor = Infor;
        }


        ///<summary>
        ///<para>Lưu lại giá trị vết vào CSDL</para>
        ///</summary>   
        ///<returns>true nếu thành công và false nếu thất bại.</returns>
        public ActionResult Save(System.Data.SqlClient.SqlTransaction Trans)
        {
            string SQLStatement =
                "INSERT INTO Sys_TRACE(BranchID,UserName,CreatedDate,IPAddress,ComputerName,AccountName,DLLName,SubSystemName,FunctionID,FunctionName,TableName,[Desc],LOT)";
            SQLStatement += " VALUES('" + Infor.BranchID + "','" + Infor.UserName + "',CONVERT(datetime,'" +
                            Infor.CreatedDate + "',103),'";
            SQLStatement += Infor.IPAddress + "',N'" + Infor.ComputerName + "',N'" + Infor.AccountName + "',N'" +
                            Infor.DLLName + "',N'" + Infor.SubSystemName + "'," + Infor.FunctionID + ",N'" +
                            Infor.FunctionName + "',N'" + Infor.TableName + "',N'" + Infor.Desc + "'," + Infor.LOT + ")";

            try
            {
                return ActionResult.Success;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lưu vết:\n" + ex.Message);
                return ActionResult.Exception;
            }
        }

        ///<summary>
        ///<para>Đưa giá trị của vết vào Header trước khi gửi lên Business xử lý</para>
        ///</summary>   
        /// <param name="Header"><para>Bảng lưu vết</para></param>     
        /// <param name="trace"><para>Đối tượng vết <see cref="HISTrace"/></para></param>     
        public void MergeTraceIntoHeader(ref DataTable Header, TraceInfor trace)
        {
            try
            {
                DataRow dr;
                bool bExist = false;
                if (Header.Equals(null))
                {
                    Utility.ShowMsg(
                        "Không thể gắn Trace vào Header vì đối tượng Header bạn truyền vào chưa được khởi tạo. Đề nghị bạn xem lại...");
                    return;
                }
                if (Header.Rows.Count > 0)
                {
                    dr = Header.Rows[0];
                    bExist = true;
                }
                else
                {
                    bExist = false;
                    dr = Header.NewRow();
                }
                //Read from Header Into Trace
                if (trace == null) return;
                dr["ID"] = Utility.Int16Dbnull(trace.pID, 0);

                dr["BranchID"] = Utility.CorrectStringValue(Utility.sDbnull(trace.pBranchID));
                dr["UserName"] = Utility.CorrectStringValue(Utility.sDbnull(trace.pUserName));
                dr["CreatedDate"] =
                    Utility.CorrectStringValue(Utility.sDbnull(trace.pCreatedDate, DateTime.Now.ToShortDateString()));
                dr["IPAddress"] = Utility.CorrectStringValue(Utility.sDbnull(trace.pIPAddress));
                dr["ComputerName"] = Utility.CorrectStringValue(Utility.sDbnull(trace.ComputerName));
                dr["AccountName"] = Utility.CorrectStringValue(Utility.sDbnull(trace.AccountName));
                dr["DLLName"] = Utility.CorrectStringValue(Utility.sDbnull(trace.DLLName));
                dr["SubSystemName"] = Utility.CorrectStringValue(Utility.sDbnull(trace.pSubSystemName));
                dr["FunctionID"] = Utility.CorrectStringValue(Utility.sDbnull(trace.pFunctionID));
                dr["FunctionName"] = Utility.CorrectStringValue(Utility.sDbnull(trace.FunctionName));
                dr["TableName"] = Utility.CorrectStringValue(Utility.sDbnull(trace.pTableName));
                dr["Desc"] = Utility.CorrectStringValue(Utility.sDbnull(trace.pDesc));
                dr["LOT"] = Utility.Int16Dbnull(trace.pLO, 0);
                if (!bExist)
                {
                    Header.Rows.Add(dr);
                }
                Header.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi tạo gán thông tin Trace vào Header:\n" + ex.Message);
                return;
            }
        }

        ///<summary>
        ///<para>Tạo thông tin vết từ Header</para>
        ///</summary>   
        /// <param name="Header"><para>Bảng lưu vết trong Header</para></param>     
        public void GetTraceInfoFromHeader(DataTable Header)
        {
            try
            {
                if (Header.Equals(null))
                {
                    return;
                }

                //Read from Header Into Trace
                Infor.ID = Utility.Int32Dbnull(Header.Rows[0]["ID"], 0);
                Infor.BranchID = Utility.CorrectStringValue(Utility.sDbnull(Header.Rows[0]["BranchID"]));
                Infor.UserName = Utility.CorrectStringValue(Utility.sDbnull(Header.Rows[0]["UserName"]));
                Infor.CreatedDate = Utility.CorrectStringValue(Utility.sDbnull(Header.Rows[0]["CreatedDate"]));
                Infor.IPAddress = Utility.CorrectStringValue(Utility.sDbnull(Header.Rows[0]["IPAddress"]));
                Infor.ComputerName = Utility.CorrectStringValue(Utility.sDbnull(Header.Rows[0]["ComputerName"]));
                Infor.AccountName = Utility.CorrectStringValue(Utility.sDbnull(Header.Rows[0]["AccountName"]));
                Infor.DLLName = Utility.CorrectStringValue(Utility.sDbnull(Header.Rows[0]["DLLName"]));
                Infor.SubSystemName = Utility.CorrectStringValue(Utility.sDbnull(Header.Rows[0]["SubSystemName"]));
                Infor.FunctionID = Utility.Int16Dbnull(Header.Rows[0]["FunctionID"]);
                Infor.FunctionName = Utility.CorrectStringValue(Utility.sDbnull(Header.Rows[0]["FunctionName"]));
                Infor.TableName = Utility.CorrectStringValue(Utility.sDbnull(Header.Rows[0]["TableName"]));
                Infor.Desc = Utility.CorrectStringValue(Utility.sDbnull(Header.Rows[0]["Desc"]));
                Infor.LOT = Utility.Int16Dbnull(Header.Rows[0]["LOT"], 0);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lấy thông tin đối tượng lưu vết từ Header\n" + ex.Message);
                return;
            }
        }
    }

    internal class CustomSqlProvider : SqlDataProvider
    {
        public CustomSqlProvider(string connectionString)
        {
            this.DefaultConnectionString = connectionString;
        }

        public override string Name
        {
            get { return "ORM"; }
        }
    }

    public class MoneyByLetter
    {
        public string SothanhChu(string text)
        {
            string s = "";
            if (text.Contains(","))
            {
                string s1 = text.Split(',')[0];
                string s2 = text.Split(',')[1];
                s = String.Format("{0} phẩy {1}", sMoneyToLetter(s1).Replace("đồng", ""), sMoneyToLetter(s2).Replace("đồng", ""));
            }
            else if (text.Contains("."))
            {
                string s1 = text.Split('.')[0];
                string s2 = text.Split('.')[1];
                s = String.Format("{0} phẩy {1}", sMoneyToLetter(s1).Replace("đồng", ""),sMoneyToLetter(s2).Replace("đồng", ""));
            }
            else
                s = new MoneyByLetter().sMoneyToLetter(text).Replace("đồng", "");
            return s;
        }

        public string sMoneyToLetter(string strNumber)
        {
            if (!Utility.IsNumeric(strNumber)) return "Số tiền phải là chữ số. Đề nghị bạn kiểm tra lại";
            strNumber = Convert.ToInt64(Convert.ToDouble(strNumber)).ToString();
            string functionReturnValue = null;
            string chu = "";
            int lng;
            string CD;
            int dd;
            int i;
            string TP;
            bool bolCheck;
            bool BolSoAm = false;
            if (Strings.Left(strNumber, 1) == "-")
            {
                // số âm
                BolSoAm = true;
                strNumber = strNumber.Substring(1);
            }
            bolCheck = false;
            CD = "";
            TP = "";
            if (strNumber == "")
            {
                functionReturnValue = "";
                return ""; // TODO: might not be correct. Was : Exit Function
            }
            //If Trim(Str$(strNumber)) <> "" Then
            if (Strings.Trim(strNumber) != "")
            {
                for (i = 0; i <= Strings.Len(strNumber); i++)
                {
                    if (Strings.Right(Strings.Left(strNumber, i), 1) == ".")
                    {
                        bolCheck = true;
                        CD = Strings.Left(strNumber, i - 1);
                        TP = Strings.Right(strNumber, Strings.Len(strNumber) - i);
                        TP = TP + "0";
                        TP = Strings.Left(TP, 2);
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
                if (bolCheck == false)
                {
                    //                CD = Trim(Str$(strNumber))
                    CD = Strings.Trim(strNumber);
                }
                lng = Strings.Len(CD);
                switch (lng)
                {
                    case 0:
                        chu = " ";
                        break;
                    case 1:
                        chu = DonVi(CD);
                        break;
                    case 2:
                        chu = Chuc(CD);
                        break;
                    case 3:
                        chu = Tram(CD);
                        break;
                    case 4:
                    case 5:
                    case 6:
                        chu = Nghin(CD);
                        break;
                    case 7:
                    case 8:
                    case 9:
                        chu = Trieu(CD);
                        break;
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                        chu = Ti(CD);
                        break;
                }

                dd = Strings.Len(Strings.Trim(chu));
                functionReturnValue = Strings.UCase(Strings.Mid(Strings.Trim(chu), 1, 1)) +
                                      Strings.Mid(Strings.Trim(chu), 2, dd - 1) + " đồng";
                //MoneyByLetter = Replace(MoneyByLetter, " không trăm  ngh×n", "")
                functionReturnValue = functionReturnValue.Replace(" không trăm  nghìn", "");
                functionReturnValue = functionReturnValue.Replace(" không trăm  triệu", "");
                functionReturnValue = functionReturnValue.Replace("  ", " ");
                if (TP != "")
                {
                    i = Convert.ToInt32(TP);
                    lng = Strings.Len(TP);
                    if (lng > 0)
                    {
                        switch (lng)
                        {
                            case 1:
                                chu = DonVi(TP);
                                break;
                            case 2:
                                chu = ChucHao(TP);
                                break;
                        }
                        dd = Strings.Len(Strings.Trim(chu));
                        functionReturnValue = functionReturnValue + " " +
                                              Strings.LCase(Strings.Mid(Strings.Trim(chu), 1, 1)) +
                                              Strings.Mid(Strings.Trim(chu), 2, dd - 1) + " xu";
                    }
                }
            }
            if (BolSoAm) functionReturnValue = "Âm " + functionReturnValue;
            return functionReturnValue.Replace("không xu", "");
        }

        public string MoneyByLetterShort(string strNumber, int muc)
        {
            string functionReturnValue = null;
            string chu = "";
            int lng;
            string CD;
            int dd;
            int i;
            string TP;
            bool bolCheck;
            bool BolSoAm = false;
            if (Strings.Left(strNumber, 1) == "-")
            {
                // số âm
                BolSoAm = true;
                strNumber = strNumber.Substring(1);
            }
            bolCheck = false;
            CD = "";
            TP = "";
            if (strNumber == "")
            {
                functionReturnValue = "";
                return "";
            }
            //If Trim(Str$(strNumber)) <> "" Then
            if (Strings.Trim(strNumber) != "")
            {
                for (i = 0; i <= Strings.Len(strNumber); i++)
                {
                    if (Strings.Right(Strings.Left(strNumber, i), 1) == ".")
                    {
                        bolCheck = true;
                        CD = Strings.Left(strNumber, i - 1);
                        TP = Strings.Right(strNumber, Strings.Len(strNumber) - i);
                        TP = TP + "0";
                        TP = Strings.Left(TP, 2);
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
                if (bolCheck == false)
                {
                    //                CD = Trim(Str$(strNumber))
                    CD = Strings.Trim(strNumber);
                }
                lng = Strings.Len(CD);
                switch (lng)
                {
                    case 0:
                        chu = " ";
                        break;
                    case 1:
                        chu = DonVi(CD);
                        break;
                    case 2:
                        chu = ChucShort(CD);
                        break;
                    case 3:
                        chu = TramShort(CD, muc);
                        break;
                    case 4:
                    case 5:
                    case 6:
                        chu = NghinShort(CD, muc);
                        break;
                    case 7:
                    case 8:
                    case 9:
                        chu = TrieuShort(CD, muc);
                        break;
                    case 10:
                    case 11:
                    case 12:
                        chu = TiShort(CD, muc);
                        break;
                }

                dd = Strings.Len(Strings.Trim(chu));
                functionReturnValue = Strings.UCase(Strings.Mid(Strings.Trim(chu), 1, 1)) +
                                      Strings.Mid(Strings.Trim(chu), 2, dd - 1) + " đồng";
                //MoneyByLetter = Replace(MoneyByLetter, " không trăm  ngh×n", "")
                functionReturnValue = functionReturnValue.Replace("  ", " ");
                functionReturnValue = functionReturnValue.Replace(" không trăm nghìn", "");
                functionReturnValue = functionReturnValue.Replace(" không trăm triệu", "");
                //MoneyByLetterShort = Replace(MoneyByLetterShort, " mươi", "")
                //If muc = 2 Then
                //    MoneyByLetterShort = Replace(MoneyByLetterShort, " trăm", "")
                //End If
                if (TP != "")
                {
                    i = Convert.ToInt32(TP);
                    lng = Strings.Len(TP);
                    if (lng > 0)
                    {
                        switch (lng)
                        {
                            case 1:
                                chu = DonVi(TP);
                                break;
                            case 2:
                                chu = ChucHaoShort(TP);
                                break;
                        }
                        dd = Strings.Len(Strings.Trim(chu));
                        functionReturnValue = functionReturnValue + " " +
                                              Strings.LCase(Strings.Mid(Strings.Trim(chu), 1, 1)) +
                                              Strings.Mid(Strings.Trim(chu), 2, dd - 1) + " xu";
                    }
                }
            }
            return functionReturnValue;
            //If BolSoAm Then MoneyByLetter = "Âm " & MoneyByLetter
        }

        private string ChucHao(string d)
        {
            string tmp = "";
            if (Strings.Len(d) != 2)
            {
                tmp = "Error";
            }
            else
            {
                switch (Strings.Mid(d, 1, 1))
                {
                    case "0":
                        tmp = " " + DonVi(Strings.Mid(d, 2, 1));
                        break;
                    case "1":
                        if (Strings.Mid(d, 2, 1) == "0")
                        {
                            tmp = "mười ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "5")
                        {
                            tmp = "lăm ";
                        }
                        else
                        {
                            tmp = "mười " + DonVi(Strings.Mid(d, 2, 1));
                        }

                        break;
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        if (Strings.Mid(d, 2, 1) == "0")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "1")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi mốt ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "4")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi tư ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "5")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi lăm ";
                        }
                        else
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi " + DonVi(Strings.Mid(d, 2, 1));
                        }

                        break;
                }
            }
            return tmp;
        }

        private string ChucHaoShort(string d)
        {
            string tmp = "";
            if (Strings.Len(d) != 2)
            {
                tmp = "Error";
            }
            else
            {
                switch (Strings.Mid(d, 1, 1))
                {
                    case "0":
                        tmp = " " + DonVi(Strings.Mid(d, 2, 1));
                        break;
                    case "1":
                        if (Strings.Mid(d, 2, 1) == "0")
                        {
                            tmp = "mười ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "5")
                        {
                            tmp = "lăm ";
                        }
                        else
                        {
                            tmp = "mười " + DonVi(Strings.Mid(d, 2, 1));
                        }

                        break;
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        if (Strings.Mid(d, 2, 1) == "0")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "1")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mốt ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "4")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "tư ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "5")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "lăm ";
                        }
                        else
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + DonVi(Strings.Mid(d, 2, 1));
                        }

                        break;
                }
            }
            return tmp;
        }

        private string DonVi(string d)
        {
            string functionReturnValue = null;
            int tmp;
            tmp = Strings.Len(d);
            if (tmp == 0)
            {
                functionReturnValue = " ";
            }
            else
            {
                switch (Strings.Mid(d, 1, 1))
                {
                    case "0":
                        functionReturnValue = "không ";
                        break;
                    case "1":
                        functionReturnValue = "một ";
                        break;
                    case "2":
                        functionReturnValue = "hai ";
                        break;
                    case "3":
                        functionReturnValue = "ba ";
                        break;
                    case "4":
                        functionReturnValue = "bốn ";
                        break;
                    case "5":
                        functionReturnValue = "năm ";
                        break;
                    case "6":
                        functionReturnValue = "sáu ";
                        break;
                    case "7":
                        functionReturnValue = "bảy ";
                        break;
                    case "8":
                        functionReturnValue = "tám ";
                        break;
                    case "9":
                        functionReturnValue = "chín ";
                        break;
                }
            }
            return functionReturnValue;
        }

        private string Chuc(string d)
        {
            string tmp = "";
            if (Strings.Len(d) != 2)
            {
                tmp = "Error";
            }
            else
            {
                switch (Strings.Mid(d, 1, 1))
                {
                    case "1":
                        if (Strings.Mid(d, 2, 1) == "0")
                        {
                            tmp = "mười ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "5")
                        {
                            tmp = "mười lăm ";
                        }
                        else
                        {
                            tmp = "mười " + DonVi(Strings.Mid(d, 2, 1));
                        }

                        break;
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        if (Strings.Mid(d, 2, 1) == "0")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "1")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi mốt ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "4")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi tư ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "5")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi lăm ";
                        }
                        else
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi " + DonVi(Strings.Mid(d, 2, 1));
                        }

                        break;
                }
            }
            return tmp;
        }

        private string ChucShort(string d)
        {
            string tmp = "";
            if (Strings.Len(d) != 2)
            {
                tmp = "Error";
            }
            else
            {
                switch (Strings.Mid(d, 1, 1))
                {
                    case "1":
                        if (Strings.Mid(d, 2, 1) == "0")
                        {
                            tmp = "mười ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "5")
                        {
                            tmp = "mười lăm ";
                        }
                        else
                        {
                            tmp = "mười " + DonVi(Strings.Mid(d, 2, 1));
                        }

                        break;
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        if (Strings.Mid(d, 2, 1) == "0")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "1")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mốt ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "4")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "tư ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "5")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "lăm ";
                        }
                        else
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + DonVi(Strings.Mid(d, 2, 1));
                        }

                        break;
                }
            }
            return tmp;
        }

        private string Tram(string d)
        {
            string functionReturnValue = null;
            string tmp = "";
            string d1;
            string d2;
            string d3;
            string Temp = "";

            d1 = Strings.Mid(d, 1, 1);
            d2 = Strings.Mid(d, 2, 1);
            d3 = Strings.Mid(d, 3, 1);
            if (Strings.Len(d) != 3)
            {
                Temp = "Error";
            }
            else
            {
                switch (d2)
                {
                    case "0":
                        if (d3 == "0")
                        {
                            tmp = DonVi(d1) + "trăm  ";
                        }
                        else if (d3 == "1")
                        {
                            tmp = DonVi(d1) + "trăm linh một ";
                        }
                        else if (d3 == "4")
                        {
                            tmp = DonVi(d1) + "trăm linh tư ";
                        }
                        else if (d3 == "5")
                        {
                            tmp = DonVi(d1) + "trăm linh năm ";
                        }
                        else
                        {
                            tmp = DonVi(d1) + "trăm linh " + DonVi(d3);
                        }

                        break;
                    case "1":
                        if (d3 == "0")
                        {
                            tmp = DonVi(d1) + "trăm mười ";
                        }
                        else
                        {
                            tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3);
                        }

                        break;
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        // tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3)
                        tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3);
                        break;
                }
                functionReturnValue = tmp;
            }
            return functionReturnValue;
        }

        private string TramShort(string d, int muc)
        {
            string functionReturnValue = null;
            string tmp = "";
            string d1;
            string d2;
            string d3;
            string Temp = "";
            if (muc == 2)
            {
                d1 = Strings.Mid(d, 1, 1);
                d2 = Strings.Mid(d, 2, 1);
                d3 = Strings.Mid(d, 3, 1);
                if (Strings.Len(d) != 3)
                {
                    Temp = "Error";
                }
                else
                {
                    switch (d2)
                    {
                        case "0":
                            if (d3 == "0")
                            {
                                tmp = DonVi(d1) + "trăm  ";
                            }
                            else if (d3 == "1")
                            {
                                tmp = DonVi(d1) + "linh một ";
                            }
                            else if (d3 == "4")
                            {
                                tmp = DonVi(d1) + "linh tư ";
                            }
                            else if (d3 == "5")
                            {
                                tmp = DonVi(d1) + "linh năm ";
                            }
                            else
                            {
                                tmp = DonVi(d1) + "linh " + DonVi(d3);
                            }

                            break;
                        case "1":
                            if (d3 == "0")
                            {
                                tmp = DonVi(d1) + "trăm mười ";
                            }
                            else
                            {
                                tmp = DonVi(d1) + ChucShort(d2 + d3);
                            }

                            break;
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            // tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3)
                            tmp = DonVi(d1) + ChucShort(d2 + d3);
                            break;
                    }
                    functionReturnValue = tmp;
                }
            }
            else
            {
                d1 = Strings.Mid(d, 1, 1);
                d2 = Strings.Mid(d, 2, 1);
                d3 = Strings.Mid(d, 3, 1);
                if (Strings.Len(d) != 3)
                {
                    Temp = "Error";
                }
                else
                {
                    switch (d2)
                    {
                        case "0":
                            if (d3 == "0")
                            {
                                tmp = DonVi(d1) + "trăm  ";
                            }
                            else if (d3 == "1")
                            {
                                tmp = DonVi(d1) + "trăm linh một ";
                            }
                            else if (d3 == "4")
                            {
                                tmp = DonVi(d1) + "trăm linh tư ";
                            }
                            else if (d3 == "5")
                            {
                                tmp = DonVi(d1) + "trăm linh năm ";
                            }
                            else
                            {
                                tmp = DonVi(d1) + "trăm linh " + DonVi(d3);
                            }

                            break;
                        case "1":
                            if (d3 == "0")
                            {
                                tmp = DonVi(d1) + "trăm mười ";
                            }
                            else
                            {
                                tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3);
                            }

                            break;
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            // tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3)
                            tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3);
                            break;
                    }
                    functionReturnValue = tmp;
                }
            }
            return functionReturnValue;
        }

        private string Nghin(string d)
        {
            string tmp = "";
            string s1 = "";
            string s2 = "";
            string tmp1 = "";
            int dai;
            int ln;
            dai = Strings.Len(d);
            ln = dai - 3;
            s1 = Strings.Mid(d, 1, ln);
            s2 = Strings.Mid(d, ln + 1, 3);
            if (s2 == "000")
            {
                tmp1 = "nghìn ";
            }
            else
            {
                tmp1 = "nghìn " + Tram(s2);
            }
            switch (Strings.Len(s1))
            {
                case 1:
                    tmp = DonVi(s1) + tmp1;
                    break;
                case 2:
                    tmp = Chuc(s1) + tmp1;
                    break;
                case 3:
                    tmp = Tram(s1) + tmp1;
                    break;
            }
            return tmp;
        }

        private string NghinShort(string d, int muc)
        {
            string tmp = "";
            string s1;
            string s2;
            string tmp1;
            int dai;
            int ln;
            dai = Strings.Len(d);
            ln = dai - 3;
            s1 = Strings.Mid(d, 1, ln);
            s2 = Strings.Mid(d, ln + 1, 3);
            if (s2 == "000")
            {
                tmp1 = "nghìn ";
            }
            else
            {
                tmp1 = "nghìn " + TramShort(s2, muc);
            }
            switch (Strings.Len(s1))
            {
                case 1:
                    tmp = DonVi(s1) + tmp1;
                    break;
                case 2:
                    tmp = ChucShort(s1) + tmp1;
                    break;
                case 3:
                    tmp = TramShort(s1, muc) + tmp1;
                    break;
            }
            return tmp;
        }

        private string Trieu(string d)
        {
            string tmp = "";
            string s1 = "";
            string s2 = "";
            string tmp1 = "";
            int dai;
            int ln;
            dai = Strings.Len(d);
            ln = dai - 6;
            s1 = Strings.Mid(d, 1, ln);
            s2 = Strings.Mid(d, ln + 1, 6);
            if (s2 == "000000")
            {
                tmp1 = "triệu ";
            }
            else
            {
                tmp1 = "triệu " + Nghin(s2);
            }
            switch (Strings.Len(s1))
            {
                case 1:
                    tmp = DonVi(s1) + tmp1;
                    break;
                case 2:
                    tmp = Chuc(s1) + tmp1;
                    break;
                case 3:
                    tmp = Tram(s1) + tmp1;
                    break;
            }
            return tmp;
        }

        private string TrieuShort(string d, int muc)
        {
            string tmp = "";
            string s1 = "";
            string s2 = "";
            string tmp1 = "";
            int dai;
            int ln;
            dai = Strings.Len(d);
            ln = dai - 6;
            s1 = Strings.Mid(d, 1, ln);
            s2 = Strings.Mid(d, ln + 1, 6);
            if (s2 == "000000")
            {
                tmp1 = "triệu ";
            }
            else
            {
                tmp1 = "triệu " + NghinShort(s2, muc);
            }
            switch (Strings.Len(s1))
            {
                case 1:
                    tmp = DonVi(s1) + tmp1;
                    break;
                case 2:
                    tmp = ChucShort(s1) + tmp1;
                    break;
                case 3:
                    tmp = TramShort(s1, muc) + tmp1;
                    break;
            }
            return tmp;
        }

        private string Ti(string d)
        {
            string tmp = "";
            string s1 = "";
            string s2 = "";
            string tmp1 = "";
            int dai;
            int ln;
            dai = Strings.Len(d);
            ln = dai - 9;
            s1 = Strings.Mid(d, 1, ln);
            s2 = Strings.Mid(d, ln + 1, 9);

            if (s2 == "000000000")
            {
                tmp1 = "tỷ ";
            }
            else
            {
                tmp1 = "tỷ " + Trieu(s2);
            }

            switch (Strings.Len(s1))
            {
                case 1:
                    tmp = DonVi(s1) + tmp1;
                    break;
                case 2:
                    tmp = Chuc(s1) + tmp1;
                    break;
                case 3:
                    tmp = Tram(s1) + tmp1;
                    break;
                case 4:
                    tmp = Nghin(s1) + tmp1;
                    break;
                case 5:
                    tmp = "(Số tiền quá lớn. Bạn cần xem lại đơn hàng)";
                    break;
            }
            return tmp;
        }

        private string TiShort(string d, int muc)
        {
            string tmp = "";
            string s1 = "";
            string s2 = "";
            string tmp1 = "";
            int dai;
            int ln;
            dai = Strings.Len(d);
            ln = dai - 9;
            s1 = Strings.Mid(d, 1, ln);
            s2 = Strings.Mid(d, ln + 1, 9);

            if (s2 == "000000000")
            {
                tmp1 = "tỷ ";
            }
            else
            {
                tmp1 = "tỷ " + TrieuShort(s2, muc);
            }

            switch (Strings.Len(s1))
            {
                case 1:
                    tmp = DonVi(s1) + tmp1;
                    break;
                case 2:
                    tmp = ChucShort(s1) + tmp1;
                    break;
                case 3:
                    tmp = TramShort(s1, muc) + tmp1;
                    break;
                case 4:
                    tmp = TrieuShort(s1, muc) + tmp1;
                    break;
            }
            return tmp;
        }
    }

    public class cls_SignInfor
    {
        #region "Khai bao bien"
        public string mv_ReportCode = "";
        public string mv_TEN_BIEUBC = "";
        public string mv_TEN_DOITUONG = "";
        public int mv_CHIEU_RONG = 0;
        public int mv_CHIEU_DAI = 0;
        public int mv_TOADO_DOC = 0;
        public int mv_TOADO_NGANG = 0;
        public string mv_FONT_CHU = "Arial";
        public int mv_CO_CHU = 12;
        public string mv_KIEU_CHU = "Regular";
        public string mv_NOI_DUNG = "";
        public string mv_MA_DVIQLY = "";
        public System.DateTime mv_NGAY_CAPNHAT = new System.DateTime();
        private bool exists = false;

        #endregion

        #region "Properties"

        public bool _TonTai
        {
            get { return exists; }
        }

        #endregion

        #region "Cac ham khoi tao"

        public cls_SignInfor()
        {
            exists = false;
        }

        //ham nay se thuc hien lay thong tin tren server ve
        public cls_SignInfor(string reportcode, string sv_sBieuMau, string sv_sDonVi)
        {
            exists = true;
            mv_ReportCode = reportcode;
            mv_TEN_BIEUBC = sv_sBieuMau;
            DataTable dtbNguoiKy = new DataTable();
            try
            {
                SysTrinhky objTK = new Select().From(SysTrinhky.Schema).Where(SysTrinhky.Columns.ReportCode).IsEqualTo(mv_ReportCode).ExecuteSingle<SysTrinhky>();
                if (objTK != null)
                {
                   
                }
                else//Đang bị lấy theo tên report
                {
                    exists = true;
                    objTK = new Select().From(SysTrinhky.Schema).Where(SysTrinhky.Columns.ReportName).IsEqualTo(mv_TEN_BIEUBC).ExecuteSingle<SysTrinhky>();
                    if (objTK != null)//Có trình ký-->kiểm tra nếu mã báo cáo chưa có thì update
                    {
                        if (Utility.sDbnull(objTK.ReportCode, "").Length <= 0)//Cập nhật mã report cho các report cũ lấy trình kí theo tên report thay vì mã report
                        {
                            new Update(SysTrinhky.Schema)
                                 .Set(SysTrinhky.Columns.ReportCode).EqualTo(mv_ReportCode)
                                 .Set(SysTrinhky.Columns.NgaySua).EqualTo(DateTime.Now)
                                 .Set(SysTrinhky.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                 .Where(SysTrinhky.Columns.Id).IsEqualTo(objTK.Id)
                                 .Execute();
                            Utility.Log("cls_SignInfor", globalVariables.UserName, string.Format("Trình ký cũ lấy theo tên báo cáo ReportName-->Cập nhật lại ReportCode từ Empty={0} từ Id cũ={1} ", mv_ReportCode, objTK.Id), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                        }
                        else
                            if (Utility.sDbnull(objTK.ReportCode, "") != mv_ReportCode)//Thêm mới
                            {
                                SysTrinhky objTrinhky = new SysTrinhky();
                                objTrinhky.ReportCode = mv_ReportCode;
                                objTrinhky.Trinhky = mv_NOI_DUNG;
                                objTrinhky.ReportName = mv_TEN_BIEUBC;
                                objTrinhky.NgayTao = DateTime.Now;
                                objTrinhky.NguoiTao = globalVariables.UserName;
                                objTrinhky.IsNew = true;
                                objTrinhky.Save();
                                Utility.Log("cls_SignInfor", globalVariables.UserName, string.Format("Trình ký cũ lấy theo tên báo cáo ReportName-->có mã ReportCode ={0} khác so với mã mới truyền vào {1}-->Thêm trình ký mới cho mã mới {2} tạo ra Id mới {3} ", objTK.ReportCode, mv_ReportCode, mv_ReportCode, objTrinhky.Id), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                            }
                    }
                    else//Chưa có trình kí-->Thêm mới
                    {
                        SysTrinhky objTrinhky = new SysTrinhky();
                        objTrinhky.ReportCode = mv_ReportCode;
                        objTrinhky.Trinhky = mv_NOI_DUNG;
                        objTrinhky.ReportName = mv_TEN_BIEUBC;
                        objTrinhky.NgayTao = DateTime.Now;
                        objTrinhky.NguoiTao = globalVariables.UserName;
                        objTrinhky.IsNew = true;
                        objTrinhky.Save();
                        Utility.Log("cls_SignInfor", globalVariables.UserName, string.Format("Thêm mới trình ký cho ReportCode ={0} ", mv_ReportCode), newaction.Insert, this.GetType().Assembly.ManifestModule.Name);
                        //new Update(SysTrinhky.Schema)
                        //    .Set(SysTrinhky.Columns.ReportCode).EqualTo(mv_ReportCode)
                        //    .Set(SysTrinhky.Columns.NgaySua).EqualTo(DateTime.Now)
                        //    .Set(SysTrinhky.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                        //    .Where(SysTrinhky.Columns.Id).IsEqualTo(objTK.Id)
                        //    .Execute();


                    }
                }
                SqlQuery sqlQuery = new Select().From(SysTrinhky.Schema).Where(SysTrinhky.Columns.ReportCode).IsEqualTo(reportcode);//.And(SysTrinhky.Columns.ObjectName).IsEqualTo(globalVariables.UserName);
                dtbNguoiKy = sqlQuery.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                exists = false;
            }
            if (exists == false) return;


            try
            {
                if (dtbNguoiKy.Rows.Count > 0)
                {
                    //gan cac thong tin co duoc vao bien
                    mv_TEN_BIEUBC = (string)dtbNguoiKy.Rows[0]["ReportName"];
                    mv_ReportCode = Utility.sDbnull( dtbNguoiKy.Rows[0]["Report_Code"],"");
                    try
                    {
                        mv_TEN_DOITUONG = (string)dtbNguoiKy.Rows[0]["ObjectName"];
                    }
                    catch (Exception ex)
                    {
                        mv_TEN_DOITUONG = " ";
                    }

                   
                    try
                    {
                        mv_NOI_DUNG = (string)dtbNguoiKy.Rows[0][SysTrinhky.Columns.Trinhky];
                    }
                    catch (Exception ex)
                    {
                        mv_NOI_DUNG = " ";
                    }
                }
                else
                {
                    exists = false;
                }
            }
            catch (Exception ex)
            {
                exists = false;
            }
        }

        //ham nay se thuc hien lay thong tin tu file rpt
        public cls_SignInfor(CrystalDecisions.Shared.ParameterField rpt, string sv_sDonVi, string report_code, string baoCao,
                             string pv_sContent)
        {
            exists = false;
            try
            {
                mv_ReportCode = report_code;
                //gan cac thong tin co duoc vao bien
                mv_TEN_BIEUBC = baoCao;
                //rpt.ToString
                mv_TEN_DOITUONG = "txtTrinhky";
               
                mv_NOI_DUNG = pv_sContent.Replace("\"", "");
                mv_MA_DVIQLY = sv_sDonVi;
                mv_NGAY_CAPNHAT = new System.DateTime();
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region "Cac thu tuc"

       

        //thu tuc de ghi cau hinh cua file RPT len Database
        public void updateRPTtoDB(CrystalDecisions.CrystalReports.Engine.TextObject rpt, string madonvi, string filenane)
        {
            SysTrinhky objTK = new Select().From(SysTrinhky.Schema).Where(SysTrinhky.Columns.ReportCode).IsEqualTo(mv_ReportCode).ExecuteSingle<SysTrinhky>();
            if (objTK != null)
            {
                objTK.Trinhky = mv_NOI_DUNG;
                objTK.NgaySua = DateTime.Now;
                objTK.NguoiSua = globalVariables.UserName;
                objTK.IsNew = false;
                objTK.MarkOld();
                objTK.Save();
                Utility.Log("cls_SignInfor", globalVariables.UserName, string.Format("Cập nhật trình ký cho Id={0} từ ReportCode ={1} ", objTK.Id, mv_ReportCode), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
            }
            else//Đang bị lấy theo tên report
            {
                objTK = new Select().From(SysTrinhky.Schema).Where(SysTrinhky.Columns.ReportName).IsEqualTo(mv_TEN_BIEUBC).ExecuteSingle<SysTrinhky>();
                if (objTK != null)//Có trình ký-->kiểm tra nếu mã báo cáo chưa có thì update
                {
                    if (Utility.sDbnull(objTK.ReportCode, "").Length <= 0)//Cập nhật mã report cho các report cũ lấy trình kí theo tên report thay vì mã report
                    {
                        new Update(SysTrinhky.Schema)
                             .Set(SysTrinhky.Columns.ReportCode).EqualTo(mv_ReportCode)
                             .Set(SysTrinhky.Columns.NgaySua).EqualTo(DateTime.Now)
                             .Set(SysTrinhky.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                             .Where(SysTrinhky.Columns.Id).IsEqualTo(objTK.Id)
                             .Execute();
                        Utility.Log("cls_SignInfor", globalVariables.UserName, string.Format("Trình ký cũ lấy theo tên báo cáo ReportName-->Cập nhật lại ReportCode từ Empty={0} từ Id cũ={1} ",mv_ReportCode, objTK.Id ), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                    }
                    else
                        if (Utility.sDbnull(objTK.ReportCode, "") != mv_ReportCode)//Thêm mới
                        {
                            SysTrinhky objTrinhky = new SysTrinhky();
                            objTrinhky.ReportCode = mv_ReportCode;
                            objTrinhky.Trinhky = mv_NOI_DUNG;
                            objTrinhky.ReportName = mv_TEN_BIEUBC;
                            objTrinhky.NgayTao = DateTime.Now;
                            objTrinhky.NguoiTao = globalVariables.UserName;
                            objTrinhky.IsNew = true;
                            objTrinhky.Save();
                            Utility.Log("cls_SignInfor", globalVariables.UserName, string.Format("Trình ký cũ lấy theo tên báo cáo ReportName-->có mã ReportCode ={0} khác so với mã mới truyền vào {1}-->Thêm trình ký mới cho mã mới {2} tạo ra Id mới {3} ", objTK.ReportCode, mv_ReportCode, mv_ReportCode, objTrinhky.Id), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                        }
                } 
                //if (objTK != null)//Chưa có trình ký-->Thêm mới
                //{
                //    objTK.ReportCode = mv_ReportCode;
                //    objTK.Trinhky = mv_NOI_DUNG;
                //    objTK.NgaySua = DateTime.Now;
                //    objTK.NguoiSua = globalVariables.UserName;
                //    objTK.IsNew = false;
                //    objTK.MarkOld();
                //    objTK.Save();
                //}
                else//Chưa có trình kí-->Thêm mới
                {
                    SysTrinhky objTrinhky = new SysTrinhky();
                    objTrinhky.ReportCode = mv_ReportCode;
                    objTrinhky.Trinhky = mv_NOI_DUNG;
                    objTrinhky.ReportName = mv_TEN_BIEUBC;
                    objTrinhky.ObjectName = globalVariables.UserName;
                    objTrinhky.NgayTao = DateTime.Now;
                    objTrinhky.NguoiTao = globalVariables.UserName;
                    objTrinhky.IsNew = true;
                    objTrinhky.Save();
                    Utility.Log("cls_SignInfor", globalVariables.UserName, string.Format("Thêm mới trình ký cho ReportCode ={0} ", mv_ReportCode), newaction.Insert, this.GetType().Assembly.ManifestModule.Name);
                    
                }
            }

            //SqlQuery sqlQuery = new Select().From(SysTrinhky.Schema)
            //    .Where(SysTrinhky.Columns.ReportName).IsEqualTo(mv_TEN_BIEUBC)
            //    .Or(SysTrinhky.Columns.ReportCode).IsEqualTo(mv_ReportCode);
            //    //.And(SysTrinhky.Columns.ObjectName).IsEqualTo(globalVariables.UserName);

            //if (sqlQuery.GetRecordCount() > 0)
            //{
            //    SysTrinhky objSysUpdateTrinhky = CreateTrinhKy();
             
            //    new Update(SysTrinhky.Schema)
            //        .Set(SysTrinhky.Columns.Trinhky).EqualTo(objSysUpdateTrinhky.Trinhky)
            //         .Set(SysTrinhky.Columns.ReportCode).EqualTo(objSysUpdateTrinhky.ReportCode)
            //        .Where(SysTrinhky.Columns.ReportName).IsEqualTo(objSysUpdateTrinhky.ReportName).Execute();
            //}
            //else
            //{
            //    SysTrinhky objNewSysTrinhky = CreateTrinhKy();
            //    objNewSysTrinhky.IsNew = true;
            //    objNewSysTrinhky.Save();
            //}
           
        }

       
        //thu tuc de ghi cau hinh cua file RPT len Database
        public void updateRPTtoDB()
        {
            try
            {
                SysTrinhky objTK = new Select().From(SysTrinhky.Schema).Where(SysTrinhky.Columns.ReportCode).IsEqualTo(mv_ReportCode).ExecuteSingle<SysTrinhky>();
                if (objTK != null)
                {
                    objTK.Trinhky = mv_NOI_DUNG;
                    objTK.NgaySua = DateTime.Now;
                    objTK.NguoiSua = globalVariables.UserName;
                    objTK.IsNew = false;
                    objTK.MarkOld();
                    objTK.Save();
                }
                else//Đang bị lấy theo tên report
                {
                    objTK = new Select().From(SysTrinhky.Schema).Where(SysTrinhky.Columns.ReportName).IsEqualTo(mv_TEN_BIEUBC).ExecuteSingle<SysTrinhky>();
                    if (objTK != null)//Có trình ký-->kiểm tra nếu mã báo cáo chưa có thì update
                    {
                        if (Utility.sDbnull(objTK.ReportCode, "").Length <= 0)//Cập nhật mã report cho các report cũ lấy trình kí theo tên report thay vì mã report
                            new Update(SysTrinhky.Schema)
                                 .Set(SysTrinhky.Columns.ReportCode).EqualTo(mv_ReportCode)
                                 .Set(SysTrinhky.Columns.NgaySua).EqualTo(DateTime.Now)
                                 .Set(SysTrinhky.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                 .Where(SysTrinhky.Columns.Id).IsEqualTo(objTK.Id)
                                 .Execute();
                        else
                            if (Utility.sDbnull(objTK.ReportCode, "") != mv_ReportCode)//Thêm mới
                            {
                                SysTrinhky objTrinhky = new SysTrinhky();
                                objTrinhky.ReportCode = mv_ReportCode;
                                objTrinhky.Trinhky = mv_NOI_DUNG;
                                objTrinhky.ReportName = mv_TEN_BIEUBC;
                                objTrinhky.NgayTao = DateTime.Now;
                                objTrinhky.NguoiTao = globalVariables.UserName;
                                objTrinhky.IsNew = true;
                                objTrinhky.Save();
                            }
                    }
                    else//Chưa có trình ký-->Thêm mới
                    {

                        SysTrinhky objTrinhky = new SysTrinhky();
                        objTrinhky.ReportCode = mv_ReportCode;
                        objTrinhky.Trinhky = mv_NOI_DUNG;
                        objTrinhky.ReportName = mv_TEN_BIEUBC;
                        objTrinhky.ObjectName = globalVariables.UserName;
                        objTrinhky.NgayTao = DateTime.Now;
                        objTrinhky.NguoiTao = globalVariables.UserName;
                        objTrinhky.IsNew = true;
                        objTrinhky.Save();
                    }
                }
                Utility.Log("frmPrintPreview", globalVariables.UserName, string.Format("Cập nhật thông tin trình ký report code={0}, nội dung={1}", objTK.ReportCode, objTK.Trinhky), objTK.NgaySua.HasValue? newaction.Update : newaction.Insert, this.GetType().Assembly.ManifestModule.Name);
            }
            catch (Exception ex)
            {
                
               
            }
           
        }

        private SysTrinhky CreateTrinhKy()
        {
            SysTrinhky objTrinhky = new SysTrinhky();
            objTrinhky.ReportCode = mv_ReportCode;
            objTrinhky.Trinhky = mv_NOI_DUNG;
            objTrinhky.ReportName = mv_TEN_BIEUBC;
            objTrinhky.ObjectName = globalVariables.UserName;
            objTrinhky.NgayTao = DateTime.Now;
            objTrinhky.NguoiTao = globalVariables.UserName;
            return objTrinhky;
        }

        #endregion
    }

    public class objHisLink
    {
        private static short ServiceType_ID = -1;
        private static short Service_ID = -1;
        private static short ObjectType_ID = -1;
        private static bool b_Sended = false;
        private static long l_AssignInfo = 0;

        /// <summary>
        /// Ham thuc hien laybein cua ServiceType_ID
        /// </summary>
        public static short GetServiceType_ID
        {
            get { return ServiceType_ID; }
            set { ServiceType_ID = value; }
        }

        /// <summary>
        /// Ham thuc hien lai ID cua xet dich vu
        /// </summary>
        public static long GetAssignInfo_ID
        {
            get { return l_AssignInfo; }
            set { l_AssignInfo = value; }
        }

        public static bool GetStatusSended
        {
            get { return b_Sended; }
            set { b_Sended = value; }
        }

        public static short GetService_ID
        {
            get { return Service_ID; }
            set { Service_ID = value; }
        }

        public static short GetObjectType_ID
        {
            get { return ObjectType_ID; }
            set { ObjectType_ID = value; }
        }

        public static class DataBinding
        {
            /// <summary>
            /// Help to bind the data
            /// </summary>
            /// <param name="control"></param>
            /// <param name="data"></param>
            /// <param name="dataValueField"></param>
            /// <param name="dataTextField"></param>
            public static void BindData(Control control, object data, string dataValueField, string dataTextField)
            {
                if (typeof(ListControl).IsInstanceOfType(control))
                {
                    var listControl = control as ListControl;

                    if (listControl != null)
                    {
                        listControl.DataSource = data;
                        listControl.ValueMember = dataValueField;
                        listControl.DisplayMember = dataTextField;
                    }
                }
                if (typeof(IDataBoundable).IsInstanceOfType(control))
                {
                    var dataBoundControl = control as IDataBoundable;
                    if (dataBoundControl != null)
                    {
                        dataBoundControl.DataSource = data;
                        dataBoundControl.DataBind();
                    }
                }
            }

            public static void BindDataCombox(Control control, object data, string dataValueField, string dataTextField)
            {
                if (typeof(ListControl).IsInstanceOfType(control))
                {
                    ComboBox combox = control as ComboBox;
                    DataTable dt = new DataTable();
                    dt = (DataTable)data;
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = "--- Chọn ---";
                    dr[dataValueField] = "-1";
                    dt.Rows.InsertAt(dr, 0);
                    if (combox != null)
                    {
                        combox.DataSource = dt;
                        combox.ValueMember = dataValueField;
                        combox.DisplayMember = dataTextField;
                    }
                }
                if (typeof(IDataBoundable).IsInstanceOfType(control))
                {
                    var dataBoundControl = control as IDataBoundable;
                    if (dataBoundControl != null)
                    {
                        dataBoundControl.DataSource = data;
                        dataBoundControl.DataBind();
                    }
                }
            }

            /// <summary>
            /// Hàm thực hiện convert từ datetime thành string
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            public static string ConvertDate(DateTime str)
            {
                return str.ToString("dd/MM/yyyy");
            }

            public static bool IsCheckConnected(string strservers, string DbName, string users, string password)
            {
                string strsql = "";
                // Common.cTripleDES MaHoa = new cTripleDES();
                bool flag = false;
                //string servers = MaHoa.Decrypt(strservers);
                //string DataName = MaHoa.Decrypt(DbName);
                //string User_ID = MaHoa.Decrypt(users);
                //string Password = MaHoa.Decrypt(password);               
                strsql += "packet size=4096;workstation id=" + strservers;
                strsql += ";data source=" + strservers;
                strsql += " ;persist security info=False;initial catalog=" + DbName;
                strsql += ";uid=" + users;
                strsql += ";pwd=" + password;
                SqlConnection cnn = new SqlConnection(strsql);
                ///Mở CSDL
                try
                {
                    // Me.Cursor = Cursors.WaitCursor
                    // this.Cursor = Cursors.WaitCursor;
                    cnn.Open();
                    //  MessageBox.Show("Kết nối thành công. Chúc mừng bạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cnn.Close();
                    cnn.Dispose();
                    // cmdSave.Focus();
                    //this.Cursor = Cursors.Default;
                    flag = true;
                }

                catch
                {
                    flag = false;
                }
                return flag;
            }

            public static void BindDataCombox(Control control, object data, string dataValueField, string dataTextField,
                                              string SelectItem)
            {
                if (typeof(ListControl).IsInstanceOfType(control))
                {
                    ComboBox combox = control as ComboBox;
                    DataTable dt = new DataTable();
                    dt = (DataTable)data;
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = SelectItem;
                    dr[dataValueField] = "-1";
                    dt.Rows.InsertAt(dr, 0);
                    if (combox != null)
                    {
                        combox.DataSource = dt;
                        combox.ValueMember = dataValueField;
                        combox.DisplayMember = dataTextField;
                    }
                }
                if (typeof(IDataBoundable).IsInstanceOfType(control))
                {
                    var dataBoundControl = control as IDataBoundable;
                    if (dataBoundControl != null)
                    {
                        dataBoundControl.DataSource = data;
                        dataBoundControl.DataBind();
                    }
                }
            }

            public static void BindDataComboxDefaultView(Control control, object data, bool TrueDefaultView,
                                                         string dataValueField, string dataTextField, string SelectItem)
            {
                if (TrueDefaultView)
                {
                    if (typeof(ListControl).IsInstanceOfType(control))
                    {
                        ComboBox combox = control as ComboBox;
                        DataTable dt = new DataTable();
                        dt = (DataTable)data;
                        DataRow dr = dt.NewRow();
                        dr[dataTextField] = SelectItem;
                        dr[dataValueField] = "-1";
                        dt.Rows.InsertAt(dr, 0);

                        if (combox != null)
                        {
                            combox.DataSource = dt.DefaultView;
                            combox.ValueMember = dataValueField;
                            combox.DisplayMember = dataTextField;
                        }
                    }
                }
                else
                {
                    if (typeof(ListControl).IsInstanceOfType(control))
                    {
                        ComboBox combox = control as ComboBox;
                        DataTable dt = new DataTable();
                        dt = (DataTable)data;
                        DataRow dr = dt.NewRow();
                        dr[dataTextField] = "--- Chọn ---";
                        dr[dataValueField] = "0";
                        dt.Rows.InsertAt(dr, 0);
                        if (combox != null)
                        {
                            combox.DataSource = dt;
                            combox.ValueMember = dataValueField;
                            combox.DisplayMember = dataTextField;
                        }
                    }
                }
            }


            /// <summary>
            /// Bind when no need data value field and data field
            /// </summary>
            /// <param name="control"></param>
            /// <param name="data"></param>
            public static void BindData(Control control, object data)
            {
                BindData(control, data, null, null);
            }

            public static int GetValueFromCheckListBoxControl(object rowView, string displayValue)
            {
                var result = 0;
                if ((DataRowView)rowView != null)
                {
                    result = Convert.ToInt16(((DataRowView)rowView).Row[displayValue].ToString());
                }
                return result;
            }
        }

        public class MoneyByLetter
        {
            public string sMoneyToLetter(string strNumber)
            {
                if (!Utility.IsNumeric(strNumber)) return "Số tiền phải là chữ số. Đề nghị bạn kiểm tra lại";
                strNumber = Convert.ToInt64(Convert.ToDouble(strNumber)).ToString();
                string functionReturnValue = null;
                string chu = "";
                int lng;
                string CD;
                int dd;
                int i;
                string TP;
                bool bolCheck;
                bool BolSoAm = false;
                if (Strings.Left(strNumber, 1) == "-")
                {
                    // số âm
                    BolSoAm = true;
                    strNumber = strNumber.Substring(1);
                }
                bolCheck = false;
                CD = "";
                TP = "";
                if (strNumber == "")
                {
                    functionReturnValue = "";
                    return ""; // TODO: might not be correct. Was : Exit Function
                }
                //If Trim(Str$(strNumber)) <> "" Then
                if (Strings.Trim(strNumber) != "")
                {
                    for (i = 0; i <= Strings.Len(strNumber); i++)
                    {
                        if (Strings.Right(Strings.Left(strNumber, i), 1) == ".")
                        {
                            bolCheck = true;
                            CD = Strings.Left(strNumber, i - 1);
                            TP = Strings.Right(strNumber, Strings.Len(strNumber) - i);
                            TP = TP + "0";
                            TP = Strings.Left(TP, 2);
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                    if (bolCheck == false)
                    {
                        //                CD = Trim(Str$(strNumber))
                        CD = Strings.Trim(strNumber);
                    }
                    lng = Strings.Len(CD);
                    switch (lng)
                    {
                        case 0:
                            chu = " ";
                            break;
                        case 1:
                            chu = DonVi(CD);
                            break;
                        case 2:
                            chu = Chuc(CD);
                            break;
                        case 3:
                            chu = Tram(CD);
                            break;
                        case 4:
                        case 5:
                        case 6:
                            chu = Nghin(CD);
                            break;
                        case 7:
                        case 8:
                        case 9:
                            chu = Trieu(CD);
                            break;
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                            chu = Ti(CD);
                            break;
                    }

                    dd = Strings.Len(Strings.Trim(chu));
                    functionReturnValue = Strings.UCase(Strings.Mid(Strings.Trim(chu), 1, 1)) +
                                          Strings.Mid(Strings.Trim(chu), 2, dd - 1) + " đồng";
                    //MoneyByLetter = Replace(MoneyByLetter, " không trăm  ngh×n", "")
                    functionReturnValue = functionReturnValue.Replace(" không trăm  nghìn", "");
                    functionReturnValue = functionReturnValue.Replace(" không trăm  triệu", "");
                    functionReturnValue = functionReturnValue.Replace("  ", " ");
                    if (TP != "")
                    {
                        i = Convert.ToInt32(TP);
                        lng = Strings.Len(TP);
                        if (lng > 0)
                        {
                            switch (lng)
                            {
                                case 1:
                                    chu = DonVi(TP);
                                    break;
                                case 2:
                                    chu = ChucHao(TP);
                                    break;
                            }
                            dd = Strings.Len(Strings.Trim(chu));
                            functionReturnValue = functionReturnValue + " " +
                                                  Strings.LCase(Strings.Mid(Strings.Trim(chu), 1, 1)) +
                                                  Strings.Mid(Strings.Trim(chu), 2, dd - 1) + " xu";
                        }
                    }
                }
                if (BolSoAm) functionReturnValue = "Âm " + functionReturnValue;
                return functionReturnValue.Replace("không xu", "");
            }

            public string MoneyByLetterShort(string strNumber, int muc)
            {
                string functionReturnValue = null;
                string chu = "";
                int lng;
                string CD;
                int dd;
                int i;
                string TP;
                bool bolCheck;
                bool BolSoAm = false;
                if (Strings.Left(strNumber, 1) == "-")
                {
                    // số âm
                    BolSoAm = true;
                    strNumber = strNumber.Substring(1);
                }
                bolCheck = false;
                CD = "";
                TP = "";
                if (strNumber == "")
                {
                    functionReturnValue = "";
                    return ""; // TODO: might not be correct. Was : Exit Function
                }
                //If Trim(Str$(strNumber)) <> "" Then
                if (Strings.Trim(strNumber) != "")
                {
                    for (i = 0; i <= Strings.Len(strNumber); i++)
                    {
                        if (Strings.Right(Strings.Left(strNumber, i), 1) == ".")
                        {
                            bolCheck = true;
                            CD = Strings.Left(strNumber, i - 1);
                            TP = Strings.Right(strNumber, Strings.Len(strNumber) - i);
                            TP = TP + "0";
                            TP = Strings.Left(TP, 2);
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                    if (bolCheck == false)
                    {
                        //                CD = Trim(Str$(strNumber))
                        CD = Strings.Trim(strNumber);
                    }
                    lng = Strings.Len(CD);
                    switch (lng)
                    {
                        case 0:
                            chu = " ";
                            break;
                        case 1:
                            chu = DonVi(CD);
                            break;
                        case 2:
                            chu = ChucShort(CD);
                            break;
                        case 3:
                            chu = TramShort(CD, muc);
                            break;
                        case 4:
                        case 5:
                        case 6:
                            chu = NghinShort(CD, muc);
                            break;
                        case 7:
                        case 8:
                        case 9:
                            chu = TrieuShort(CD, muc);
                            break;
                        case 10:
                        case 11:
                        case 12:
                            chu = TiShort(CD, muc);
                            break;
                    }

                    dd = Strings.Len(Strings.Trim(chu));
                    functionReturnValue = Strings.UCase(Strings.Mid(Strings.Trim(chu), 1, 1)) +
                                          Strings.Mid(Strings.Trim(chu), 2, dd - 1) + " đồng";
                    //MoneyByLetter = Replace(MoneyByLetter, " không trăm  ngh×n", "")
                    functionReturnValue = functionReturnValue.Replace("  ", " ");
                    functionReturnValue = functionReturnValue.Replace(" không trăm nghìn", "");
                    functionReturnValue = functionReturnValue.Replace(" không trăm triệu", "");
                    //MoneyByLetterShort = Replace(MoneyByLetterShort, " mươi", "")
                    //If muc = 2 Then
                    //    MoneyByLetterShort = Replace(MoneyByLetterShort, " trăm", "")
                    //End If
                    if (TP != "")
                    {
                        i = Convert.ToInt32(TP);
                        lng = Strings.Len(TP);
                        if (lng > 0)
                        {
                            switch (lng)
                            {
                                case 1:
                                    chu = DonVi(TP);
                                    break;
                                case 2:
                                    chu = ChucHaoShort(TP);
                                    break;
                            }
                            dd = Strings.Len(Strings.Trim(chu));
                            functionReturnValue = functionReturnValue + " " +
                                                  Strings.LCase(Strings.Mid(Strings.Trim(chu), 1, 1)) +
                                                  Strings.Mid(Strings.Trim(chu), 2, dd - 1) + " xu";
                        }
                    }
                }
                return functionReturnValue;
                //If BolSoAm Then MoneyByLetter = "Âm " & MoneyByLetter
            }

            private string ChucHao(string d)
            {
                string tmp = "";
                if (Strings.Len(d) != 2)
                {
                    tmp = "Error";
                }
                else
                {
                    switch (Strings.Mid(d, 1, 1))
                    {
                        case "0":
                            tmp = " " + DonVi(Strings.Mid(d, 2, 1));
                            break;
                        case "1":
                            if (Strings.Mid(d, 2, 1) == "0")
                            {
                                tmp = "mười ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "5")
                            {
                                tmp = "lăm ";
                            }
                            else
                            {
                                tmp = "mười " + DonVi(Strings.Mid(d, 2, 1));
                            }

                            break;
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            if (Strings.Mid(d, 2, 1) == "0")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "1")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi mốt ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "4")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi tư ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "5")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi lăm ";
                            }
                            else
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi " + DonVi(Strings.Mid(d, 2, 1));
                            }

                            break;
                    }
                }
                return tmp;
            }

            private string ChucHaoShort(string d)
            {
                string tmp = "";
                if (Strings.Len(d) != 2)
                {
                    tmp = "Error";
                }
                else
                {
                    switch (Strings.Mid(d, 1, 1))
                    {
                        case "0":
                            tmp = " " + DonVi(Strings.Mid(d, 2, 1));
                            break;
                        case "1":
                            if (Strings.Mid(d, 2, 1) == "0")
                            {
                                tmp = "mười ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "5")
                            {
                                tmp = "lăm ";
                            }
                            else
                            {
                                tmp = "mười " + DonVi(Strings.Mid(d, 2, 1));
                            }

                            break;
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            if (Strings.Mid(d, 2, 1) == "0")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "1")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mốt ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "4")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "tư ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "5")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "lăm ";
                            }
                            else
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + DonVi(Strings.Mid(d, 2, 1));
                            }

                            break;
                    }
                }
                return tmp;
            }

            private string DonVi(string d)
            {
                string functionReturnValue = null;
                int tmp;
                tmp = Strings.Len(d);
                if (tmp == 0)
                {
                    functionReturnValue = " ";
                }
                else
                {
                    switch (Strings.Mid(d, 1, 1))
                    {
                        case "0":
                            functionReturnValue = "không ";
                            break;
                        case "1":
                            functionReturnValue = "một ";
                            break;
                        case "2":
                            functionReturnValue = "hai ";
                            break;
                        case "3":
                            functionReturnValue = "ba ";
                            break;
                        case "4":
                            functionReturnValue = "bốn ";
                            break;
                        case "5":
                            functionReturnValue = "năm ";
                            break;
                        case "6":
                            functionReturnValue = "sáu ";
                            break;
                        case "7":
                            functionReturnValue = "bảy ";
                            break;
                        case "8":
                            functionReturnValue = "tám ";
                            break;
                        case "9":
                            functionReturnValue = "chín ";
                            break;
                    }
                }
                return functionReturnValue;
            }

            private string Chuc(string d)
            {
                string tmp = "";
                if (Strings.Len(d) != 2)
                {
                    tmp = "Error";
                }
                else
                {
                    switch (Strings.Mid(d, 1, 1))
                    {
                        case "1":
                            if (Strings.Mid(d, 2, 1) == "0")
                            {
                                tmp = "mười ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "5")
                            {
                                tmp = "mười lăm ";
                            }
                            else
                            {
                                tmp = "mười " + DonVi(Strings.Mid(d, 2, 1));
                            }

                            break;
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            if (Strings.Mid(d, 2, 1) == "0")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "1")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi mốt ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "4")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi tư ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "5")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi lăm ";
                            }
                            else
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi " + DonVi(Strings.Mid(d, 2, 1));
                            }

                            break;
                    }
                }
                return tmp;
            }

            private string ChucShort(string d)
            {
                string tmp = "";
                if (Strings.Len(d) != 2)
                {
                    tmp = "Error";
                }
                else
                {
                    switch (Strings.Mid(d, 1, 1))
                    {
                        case "1":
                            if (Strings.Mid(d, 2, 1) == "0")
                            {
                                tmp = "mười ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "5")
                            {
                                tmp = "mười lăm ";
                            }
                            else
                            {
                                tmp = "mười " + DonVi(Strings.Mid(d, 2, 1));
                            }

                            break;
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            if (Strings.Mid(d, 2, 1) == "0")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "1")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mốt ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "4")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "tư ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "5")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "lăm ";
                            }
                            else
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + DonVi(Strings.Mid(d, 2, 1));
                            }

                            break;
                    }
                }
                return tmp;
            }

            private string Tram(string d)
            {
                string functionReturnValue = null;
                string tmp = "";
                string d1;
                string d2;
                string d3;
                string Temp = "";

                d1 = Strings.Mid(d, 1, 1);
                d2 = Strings.Mid(d, 2, 1);
                d3 = Strings.Mid(d, 3, 1);
                if (Strings.Len(d) != 3)
                {
                    Temp = "Error";
                }
                else
                {
                    switch (d2)
                    {
                        case "0":
                            if (d3 == "0")
                            {
                                tmp = DonVi(d1) + "trăm  ";
                            }
                            else if (d3 == "1")
                            {
                                tmp = DonVi(d1) + "trăm linh một ";
                            }
                            else if (d3 == "4")
                            {
                                tmp = DonVi(d1) + "trăm linh tư ";
                            }
                            else if (d3 == "5")
                            {
                                tmp = DonVi(d1) + "trăm linh năm ";
                            }
                            else
                            {
                                tmp = DonVi(d1) + "trăm linh " + DonVi(d3);
                            }

                            break;
                        case "1":
                            if (d3 == "0")
                            {
                                tmp = DonVi(d1) + "trăm mười ";
                            }
                            else
                            {
                                tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3);
                            }

                            break;
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            // tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3)
                            tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3);
                            break;
                    }
                    functionReturnValue = tmp;
                }
                return functionReturnValue;
            }

            private string TramShort(string d, int muc)
            {
                string functionReturnValue = null;
                string tmp = "";
                string d1;
                string d2;
                string d3;
                string Temp = "";
                if (muc == 2)
                {
                    d1 = Strings.Mid(d, 1, 1);
                    d2 = Strings.Mid(d, 2, 1);
                    d3 = Strings.Mid(d, 3, 1);
                    if (Strings.Len(d) != 3)
                    {
                        Temp = "Error";
                    }
                    else
                    {
                        switch (d2)
                        {
                            case "0":
                                if (d3 == "0")
                                {
                                    tmp = DonVi(d1) + "trăm  ";
                                }
                                else if (d3 == "1")
                                {
                                    tmp = DonVi(d1) + "linh một ";
                                }
                                else if (d3 == "4")
                                {
                                    tmp = DonVi(d1) + "linh tư ";
                                }
                                else if (d3 == "5")
                                {
                                    tmp = DonVi(d1) + "linh năm ";
                                }
                                else
                                {
                                    tmp = DonVi(d1) + "linh " + DonVi(d3);
                                }

                                break;
                            case "1":
                                if (d3 == "0")
                                {
                                    tmp = DonVi(d1) + "trăm mười ";
                                }
                                else
                                {
                                    tmp = DonVi(d1) + ChucShort(d2 + d3);
                                }

                                break;
                            case "2":
                            case "3":
                            case "4":
                            case "5":
                            case "6":
                            case "7":
                            case "8":
                            case "9":
                                // tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3)
                                tmp = DonVi(d1) + ChucShort(d2 + d3);
                                break;
                        }
                        functionReturnValue = tmp;
                    }
                }
                else
                {
                    d1 = Strings.Mid(d, 1, 1);
                    d2 = Strings.Mid(d, 2, 1);
                    d3 = Strings.Mid(d, 3, 1);
                    if (Strings.Len(d) != 3)
                    {
                        Temp = "Error";
                    }
                    else
                    {
                        switch (d2)
                        {
                            case "0":
                                if (d3 == "0")
                                {
                                    tmp = DonVi(d1) + "trăm  ";
                                }
                                else if (d3 == "1")
                                {
                                    tmp = DonVi(d1) + "trăm linh một ";
                                }
                                else if (d3 == "4")
                                {
                                    tmp = DonVi(d1) + "trăm linh tư ";
                                }
                                else if (d3 == "5")
                                {
                                    tmp = DonVi(d1) + "trăm linh năm ";
                                }
                                else
                                {
                                    tmp = DonVi(d1) + "trăm linh " + DonVi(d3);
                                }

                                break;
                            case "1":
                                if (d3 == "0")
                                {
                                    tmp = DonVi(d1) + "trăm mười ";
                                }
                                else
                                {
                                    tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3);
                                }

                                break;
                            case "2":
                            case "3":
                            case "4":
                            case "5":
                            case "6":
                            case "7":
                            case "8":
                            case "9":
                                // tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3)
                                tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3);
                                break;
                        }
                        functionReturnValue = tmp;
                    }
                }
                return functionReturnValue;
            }

            private string Nghin(string d)
            {
                string tmp = "";
                string s1 = "";
                string s2 = "";
                string tmp1 = "";
                int dai;
                int ln;
                dai = Strings.Len(d);
                ln = dai - 3;
                s1 = Strings.Mid(d, 1, ln);
                s2 = Strings.Mid(d, ln + 1, 3);
                if (s2 == "000")
                {
                    tmp1 = "nghìn ";
                }
                else
                {
                    tmp1 = "nghìn " + Tram(s2);
                }
                switch (Strings.Len(s1))
                {
                    case 1:
                        tmp = DonVi(s1) + tmp1;
                        break;
                    case 2:
                        tmp = Chuc(s1) + tmp1;
                        break;
                    case 3:
                        tmp = Tram(s1) + tmp1;
                        break;
                }
                return tmp;
            }

            private string NghinShort(string d, int muc)
            {
                string tmp = "";
                string s1;
                string s2;
                string tmp1;
                int dai;
                int ln;
                dai = Strings.Len(d);
                ln = dai - 3;
                s1 = Strings.Mid(d, 1, ln);
                s2 = Strings.Mid(d, ln + 1, 3);
                if (s2 == "000")
                {
                    tmp1 = "nghìn ";
                }
                else
                {
                    tmp1 = "nghìn " + TramShort(s2, muc);
                }
                switch (Strings.Len(s1))
                {
                    case 1:
                        tmp = DonVi(s1) + tmp1;
                        break;
                    case 2:
                        tmp = ChucShort(s1) + tmp1;
                        break;
                    case 3:
                        tmp = TramShort(s1, muc) + tmp1;
                        break;
                }
                return tmp;
            }

            private string Trieu(string d)
            {
                string tmp = "";
                string s1 = "";
                string s2 = "";
                string tmp1 = "";
                int dai;
                int ln;
                dai = Strings.Len(d);
                ln = dai - 6;
                s1 = Strings.Mid(d, 1, ln);
                s2 = Strings.Mid(d, ln + 1, 6);
                if (s2 == "000000")
                {
                    tmp1 = "triệu ";
                }
                else
                {
                    tmp1 = "triệu " + Nghin(s2);
                }
                switch (Strings.Len(s1))
                {
                    case 1:
                        tmp = DonVi(s1) + tmp1;
                        break;
                    case 2:
                        tmp = Chuc(s1) + tmp1;
                        break;
                    case 3:
                        tmp = Tram(s1) + tmp1;
                        break;
                }
                return tmp;
            }

            private string TrieuShort(string d, int muc)
            {
                string tmp = "";
                string s1 = "";
                string s2 = "";
                string tmp1 = "";
                int dai;
                int ln;
                dai = Strings.Len(d);
                ln = dai - 6;
                s1 = Strings.Mid(d, 1, ln);
                s2 = Strings.Mid(d, ln + 1, 6);
                if (s2 == "000000")
                {
                    tmp1 = "triệu ";
                }
                else
                {
                    tmp1 = "triệu " + NghinShort(s2, muc);
                }
                switch (Strings.Len(s1))
                {
                    case 1:
                        tmp = DonVi(s1) + tmp1;
                        break;
                    case 2:
                        tmp = ChucShort(s1) + tmp1;
                        break;
                    case 3:
                        tmp = TramShort(s1, muc) + tmp1;
                        break;
                }
                return tmp;
            }

            private string Ti(string d)
            {
                string tmp = "";
                string s1 = "";
                string s2 = "";
                string tmp1 = "";
                int dai;
                int ln;
                dai = Strings.Len(d);
                ln = dai - 9;
                s1 = Strings.Mid(d, 1, ln);
                s2 = Strings.Mid(d, ln + 1, 9);

                if (s2 == "000000000")
                {
                    tmp1 = "tỷ ";
                }
                else
                {
                    tmp1 = "tỷ " + Trieu(s2);
                }

                switch (Strings.Len(s1))
                {
                    case 1:
                        tmp = DonVi(s1) + tmp1;
                        break;
                    case 2:
                        tmp = Chuc(s1) + tmp1;
                        break;
                    case 3:
                        tmp = Tram(s1) + tmp1;
                        break;
                    case 4:
                        tmp = Nghin(s1) + tmp1;
                        break;
                    case 5:
                        tmp = "(Số tiền quá lớn. Bạn cần xem lại đơn hàng)";
                        break;
                }
                return tmp;
            }

            private string TiShort(string d, int muc)
            {
                string tmp = "";
                string s1 = "";
                string s2 = "";
                string tmp1 = "";
                int dai;
                int ln;
                dai = Strings.Len(d);
                ln = dai - 9;
                s1 = Strings.Mid(d, 1, ln);
                s2 = Strings.Mid(d, ln + 1, 9);

                if (s2 == "000000000")
                {
                    tmp1 = "tỷ ";
                }
                else
                {
                    tmp1 = "tỷ " + TrieuShort(s2, muc);
                }

                switch (Strings.Len(s1))
                {
                    case 1:
                        tmp = DonVi(s1) + tmp1;
                        break;
                    case 2:
                        tmp = ChucShort(s1) + tmp1;
                        break;
                    case 3:
                        tmp = TramShort(s1, muc) + tmp1;
                        break;
                    case 4:
                        tmp = TrieuShort(s1, muc) + tmp1;
                        break;
                }
                return tmp;
            }
        }

        public interface IDataBoundable
        {
            object DataSource { get; set; }
            void DataBind();
        }
    }

    public interface IDataBoundable
    {
        object DataSource { get; set; }
        void DataBind();
    }

    public static class DataBinding
    {
        /// <summary>
        /// Help to bind the data
        /// </summary>
        /// <param name="control"></param>
        /// <param name="data"></param>
        /// <param name="dataValueField"></param>
        /// <param name="dataTextField"></param>
        public static void BindData(Control control, object data, string dataValueField, string dataTextField)
        {
            if (typeof(ListControl).IsInstanceOfType(control))
            {
                var listControl = control as ListControl;

                if (listControl != null)
                {
                    listControl.DataSource = data;
                    listControl.ValueMember = dataValueField;
                    listControl.DisplayMember = dataTextField;
                }
            }
            if (typeof(IDataBoundable).IsInstanceOfType(control))
            {
                var dataBoundControl = control as IDataBoundable;
                if (dataBoundControl != null)
                {
                    dataBoundControl.DataSource = data;
                    dataBoundControl.DataBind();
                }
            }
        }

        public static void BindData(Janus.Windows.EditControls.UIComboBox objCombobox, object data,
                                    string dataValueField, string dataTextField)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)data;
                objCombobox.DataSource = dt;
                objCombobox.ValueMember = dataValueField;
                objCombobox.DisplayMember = dataTextField;
                if (dt.Rows.Count > 0)
                    objCombobox.SelectedIndex = 0;
            }
            catch
            {
            }
        }

        public static void BindDataNotSelectIndex(Janus.Windows.EditControls.UIComboBox objCombobox, object data,
                                                  string dataValueField, string dataTextField)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)data;


            objCombobox.DataSource = dt;
            objCombobox.ValueMember = dataValueField;
            objCombobox.DisplayMember = dataTextField;
        }

        public static void BindDataCombox(ComboBox combox, object data, string dataValueField, string dataTextField)
        {
            if (data == null)
            {
                combox.DataSource = null;
                return;
            }



            DataTable dt = new DataTable();
            dt = (DataTable)data;
            DataRow dr = dt.NewRow();
            dr[dataTextField] = "--- Chọn ---";
            dr[dataValueField] = "-1";
            dt.Rows.InsertAt(dr, 0);
            if (combox != null)
            {
                combox.DataSource = dt;
                combox.ValueMember = dataValueField;
                combox.DisplayMember = dataTextField;
            }


        }

        public static void BindDataCombox(ComboBox combox, object data, string dataValueField, string dataTextField,
                                          string SelectItem, bool AddDefaultIfNoData)
        {

            if (data == null)
            {
                combox.DataSource = null;
                return;
            }
            DataTable dt = new DataTable();
            dt = (DataTable)data;
            if (AddDefaultIfNoData)
            {
                DataRow dr = dt.NewRow();
                dr[dataTextField] = SelectItem;
                dr[dataValueField] = -1;
                dt.Rows.InsertAt(dr, 0);
            }
            else if (dt.Rows.Count > 1)
            {
                DataRow dr = dt.NewRow();
                dr[dataTextField] = SelectItem;
                dr[dataValueField] = -1;
                dt.Rows.InsertAt(dr, 0);
            }

            if (combox != null)
            {
                combox.DataSource = dt;
                combox.ValueMember = dataValueField;
                combox.DisplayMember = dataTextField;
            }


        }
        public static void BindDataCombox(ComboBox combox, object data, string dataValueField, string dataTextField,
                                          string SelectItem, bool AddDefaultIfNoData, bool NoAddIfMorethan2Items)
        {

            if (data == null)
            {
                combox.DataSource = null;
                return;
            }
            DataTable dt = new DataTable();
            dt = (DataTable)data;
            if (AddDefaultIfNoData)
            {
                DataRow dr = dt.NewRow();
                dr[dataTextField] = SelectItem;
                dr[dataValueField] = -1;
                dt.Rows.InsertAt(dr, 0);
            }
            else if (dt.Rows.Count > 1)
            {
                if (!NoAddIfMorethan2Items)
                {
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = SelectItem;
                    dr[dataValueField] = -1;
                    dt.Rows.InsertAt(dr, 0);
                }
            }

            if (combox != null)
            {
                combox.DataSource = dt;
                combox.ValueMember = dataValueField;
                combox.DisplayMember = dataTextField;
            }


        }
        public static void BindDataCombobox(Janus.Windows.EditControls.UIComboBox objCombobox, object data,
                                         string dataValueField, string dataTextField)
        {
            try
            {
                if (data == null) return;
                DataTable dt = new DataTable();
                dt = (DataTable)data;
                if (dt.Columns.Count <= 0 || !dt.Columns.Contains(dataTextField) || !dt.Columns.Contains(dataValueField)) return;
                objCombobox.DataSource = dt;
                objCombobox.ValueMember = dataValueField;
                objCombobox.DisplayMember = dataTextField;
                if (objCombobox.Items.Count > 0) objCombobox.SelectedIndex = 0;
            }
            catch
            {
            }
        }
        public static void BindDataCombobox(Janus.Windows.EditControls.UIComboBox objCombobox, DataTable data,
                                      string dataValueField, string dataTextField, string defaultItem, string defaultValue, bool AddDefaultIfNoData)
        {
            try
            {
                if (data == null) return;
                DataTable dt = new DataTable();
                dt = data.Copy();
                if (dt.Columns.Count <= 0 || !dt.Columns.Contains(dataTextField) || !dt.Columns.Contains(dataValueField)) return;
                if (AddDefaultIfNoData && data.Rows.Count <= 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = defaultItem;
                    dr[dataValueField] = defaultValue;
                    dt.Rows.InsertAt(dr, 0);
                }
                else if (dt.Rows.Count > 1)
                {
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = defaultItem;
                    dr[dataValueField] = defaultValue;
                    dt.Rows.InsertAt(dr, 0);
                }
                objCombobox.DataSource = dt;
                objCombobox.ValueMember = dataValueField;
                objCombobox.DisplayMember = dataTextField;
                if (objCombobox.Items.Count > 0) objCombobox.SelectedIndex = 0;
            }
            catch
            {
            }
        }
       
        public static void BindDataCombobox_Goi(Janus.Windows.EditControls.UIComboBox objCombobox, DataTable data,
                                      string dataValueField, string dataTextField, string defaultItem,  bool AddDefaultIfNoData, bool AlwayAddDefaultData)
        {
            try
            {
                if (data == null) return;
                DataTable dt = new DataTable();
                dt = data.Copy();
                if (dt.Columns.Count <= 0 || !dt.Columns.Contains(dataTextField) || !dt.Columns.Contains(dataValueField)) return;
                if (AddDefaultIfNoData || data.Rows.Count <= 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = defaultItem;
                    dr[dataValueField] = -1;
                    dt.Rows.InsertAt(dr, 0);
                }
                else if (AlwayAddDefaultData || dt.Rows.Count > 1)
                {
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = defaultItem;
                    dr[dataValueField] = -1;
                    dt.Rows.InsertAt(dr, 0);
                }
                objCombobox.DataSource = dt;
                objCombobox.ValueMember = dataValueField;
                objCombobox.DisplayMember = dataTextField;
                if (objCombobox.Items.Count > 0) objCombobox.SelectedIndex = 0;
            }
            catch
            {
            }
        }
        public static void BindDataCombobox(Janus.Windows.EditControls.UIComboBox objCombobox, object data,
                                        string dataValueField, string dataTextField, string defaultItem, bool AddDefaultIfNoData, bool NoAddIfMorethan2Items)
        {
            try
            {
                if (data == null) return;
                DataTable dt = new DataTable();
                dt = (DataTable)data;
                if (dt.Columns.Count <= 0 || !dt.Columns.Contains(dataTextField) || !dt.Columns.Contains(dataValueField)) return;
                if (AddDefaultIfNoData)
                {
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = defaultItem;
                    dr[dataValueField] = -1;
                    dt.Rows.InsertAt(dr, 0);
                }
                else if (dt.Rows.Count > 1)
                {
                    if (!NoAddIfMorethan2Items)
                    {
                        DataRow dr = dt.NewRow();
                        dr[dataTextField] = defaultItem;
                        dr[dataValueField] = -1;
                        dt.Rows.InsertAt(dr, 0);
                    }
                }
                objCombobox.DataSource = dt;
                objCombobox.ValueMember = dataValueField;
                objCombobox.DisplayMember = dataTextField;
                if (objCombobox.Items.Count > 0) objCombobox.SelectedIndex = 0;
            }
            catch
            {
            }
        }
        public static void BindDataCombobox(Janus.Windows.EditControls.UIComboBox objCombobox, object data,
                                         string dataValueField, string dataTextField, string defaultItem, bool AddDefaultIfNoData)
        {
            try
            {
                if (data == null ) return;
                DataTable dt = new DataTable();
                dt = (DataTable)data;
                if (dt.Columns.Count<=0 || !dt.Columns.Contains(dataTextField) || !dt.Columns.Contains(dataValueField)) return;
                if (AddDefaultIfNoData)
                {
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = defaultItem;
                    dr[dataValueField] = -1;
                    dt.Rows.InsertAt(dr, 0);
                }
                else if (dt.Rows.Count > 1)
                {
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = defaultItem;
                    dr[dataValueField] = -1;
                    dt.Rows.InsertAt(dr, 0);
                }
                objCombobox.DataSource = dt;
                objCombobox.ValueMember = dataValueField;
                objCombobox.DisplayMember = dataTextField;
                if (objCombobox.Items.Count > 0) objCombobox.SelectedIndex = 0;
            }
            catch
            {
            }
        }
        public static void BindDataCombobox(Janus.Windows.EditControls.UIComboBox objCombobox, DataTable data,
                                        string dataValueField, string dataTextField, string defaultItem, bool AddDefaultIfNoData)
        {
            try
            {
                if (data == null) return;
                DataTable dt = new DataTable();
                dt = data.Copy();
                if (dt.Columns.Count <= 0 || !dt.Columns.Contains(dataTextField) || !dt.Columns.Contains(dataValueField)) return;
                if (AddDefaultIfNoData && data.Rows.Count<=0)
                {
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = defaultItem;
                    dr[dataValueField] = -1;
                    dt.Rows.InsertAt(dr, 0);
                }
                else if (dt.Rows.Count > 1)
                {
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = defaultItem;
                    dr[dataValueField] = -1;
                    dt.Rows.InsertAt(dr, 0);
                }
                objCombobox.DataSource = dt;
                objCombobox.ValueMember = dataValueField;
                objCombobox.DisplayMember = dataTextField;
                if (objCombobox.Items.Count > 0) objCombobox.SelectedIndex = 0;
            }
            catch
            {
            }
        }
       
        public static void BindDataCombobox(ComboBox objCombobox, object data,
                                         string dataValueField, string dataTextField, string defaultItem, bool AddDefaultIfNoData)
        {
            try
            {
                if (data == null) return;
                DataTable dt = new DataTable();
                dt = (DataTable)data;
                if (dt.Columns.Count <= 0 || !dt.Columns.Contains(dataTextField) || !dt.Columns.Contains(dataValueField)) return;
                if (AddDefaultIfNoData && dt.Rows.Count <= 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = defaultItem;
                    dr[dataValueField] = -1;
                    dt.Rows.InsertAt(dr, 0);
                }
                else if (dt.Rows.Count > 1)
                {
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = defaultItem;
                    dr[dataValueField] = -1;
                    dt.Rows.InsertAt(dr, 0);
                }
                objCombobox.DataSource = dt;
                objCombobox.ValueMember = dataValueField;
                objCombobox.DisplayMember = dataTextField;
                if (objCombobox.Items.Count > 0) objCombobox.SelectedIndex = 0;
            }
            catch
            {
            }
        }
        public static void BindDataCombobox(ComboBox objCombobox, object data,
                                        string dataValueField, string dataTextField, string defaultItem, string defaultValue, bool AddDefaultIfNoData)
        {
            try
            {
                if (data == null) return;
                DataTable dt = new DataTable();
                dt = (DataTable)data;
                if (dt.Columns.Count <= 0 || !dt.Columns.Contains(dataTextField) || !dt.Columns.Contains(dataValueField)) return;
                if (AddDefaultIfNoData && dt.Rows.Count <= 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = defaultItem;
                    dr[dataValueField] = defaultValue;
                    dt.Rows.InsertAt(dr, 0);
                }
                else if (dt.Rows.Count > 1)
                {
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = defaultItem;
                    dr[dataValueField] = defaultValue;
                    dt.Rows.InsertAt(dr, 0);
                }
                objCombobox.DataSource = dt;
                objCombobox.ValueMember = dataValueField;
                objCombobox.DisplayMember = dataTextField;
                if (objCombobox.Items.Count > 0) objCombobox.SelectedIndex = 0;
            }
            catch
            {
            }
        }
        public static void BindDataCombobox_Basic(Janus.Windows.EditControls.UIComboBox objCombobox, object data,
                                          string dataValueField, string dataTextField)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)data;

            objCombobox.DataSource = dt;
            objCombobox.ValueMember = dataValueField;
            objCombobox.DisplayMember = dataTextField;
            if (objCombobox.Items.Count > 0) objCombobox.SelectedIndex = 0;
            else objCombobox.SelectedIndex = -1;
        }
        public static void BindDataCombobox(ComboBox objCombobox, object data,
                                         string dataValueField, string dataTextField)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)data;

            objCombobox.DataSource = dt;
            objCombobox.ValueMember = dataValueField;
            objCombobox.DisplayMember = dataTextField;
            if (objCombobox.Items.Count > 0) objCombobox.SelectedIndex = 0;
            else objCombobox.SelectedIndex = -1;
        }

        public static void BindDataCombox_CheckItem(Janus.Windows.EditControls.UIComboBox objCombobox, object data,
                                         string dataValueField, string dataTextField, string SelectItem)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)data;
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr[dataTextField] = SelectItem;
                dr[dataValueField] = -1;
                dt.Rows.InsertAt(dr, 0);
            }
            objCombobox.DataSource = data;
            objCombobox.ValueMember = dataValueField;
            objCombobox.DisplayMember = dataTextField;
            if (objCombobox.Items.Count > 0) objCombobox.SelectedIndex = 0;
        }
        public static void BindDataComboxDefaultView(Control control, DataTable data, bool TrueDefaultView,
                                                     string dataValueField, string dataTextField, string SelectItem)
        {
            if (TrueDefaultView)
            {
                if (typeof(ListControl).IsInstanceOfType(control))
                {
                    ComboBox combox = control as ComboBox;
                    DataTable dt = new DataTable();
                    // dt = (DataTable)data;
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = SelectItem;
                    dr[dataValueField] = "-1";
                    dt.Rows.InsertAt(dr, 0);

                    if (combox != null)
                    {
                        combox.DataSource = data.DefaultView;
                        combox.ValueMember = dataValueField;
                        combox.DisplayMember = dataTextField;
                    }
                }
            }
            else
            {
                if (typeof(ListControl).IsInstanceOfType(control))
                {
                    ComboBox combox = control as ComboBox;
                    DataTable dt = new DataTable();
                    dt = (DataTable)data;
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = "--- Chọn ---";
                    dr[dataValueField] = "0";
                    dt.Rows.InsertAt(dr, 0);
                    if (combox != null)
                    {
                        combox.DataSource = dt.DefaultView;
                        combox.ValueMember = dataValueField;
                        combox.DisplayMember = dataTextField;
                    }
                }
            }
        }


        /// <summary>
        /// Bind when no need data value field and data field
        /// </summary>
        /// <param name="control"></param>
        /// <param name="data"></param>
        public static void BindData(Control control, object data)
        {
            BindData(control, data, null, null);
        }

        public static int GetValueFromCheckListBoxControl(object rowView, string displayValue)
        {
            var result = 0;
            if ((DataRowView)rowView != null)
            {
                result = Convert.ToInt16(((DataRowView)rowView).Row[displayValue].ToString());
            }
            return result;
        }

        public static int GetValueFromCheckListBoxControl(object rowView, string displayValue, int index)
        {
            var result = 0;
            if ((DataRowView)rowView != null)
            {
                result = Convert.ToInt16(((DataRowView)rowView).Row[displayValue].ToString());
            }
            return result;
        }
    }

    /// <summary>
    /// An useful class to read/write/delete/count registry keys
    /// </summary>
    public class ModifyRegistry
    {
        private bool showError = false;

        /// <summary>
        /// A property to show or hide error messages 
        /// (default = false)
        /// </summary>
        public bool ShowError
        {
            get { return showError; }
            set { showError = value; }
        }

        private string subKey = "SOFTWARE\\" + Application.ProductName.ToUpper();

        /// <summary>
        /// A property to set the SubKey value
        /// (default = "SOFTWARE\\" + Application.ProductName.ToUpper())
        /// </summary>
        public string SubKey
        {
            get { return subKey; }
            set { subKey = value; }
        }

        private RegistryKey baseRegistryKey = Registry.LocalMachine;

        /// <summary>
        /// A property to set the BaseRegistryKey value.
        /// (default = Registry.LocalMachine)
        /// </summary>
        public RegistryKey BaseRegistryKey
        {
            get { return baseRegistryKey; }
            set { baseRegistryKey = value; }
        }

        /* **************************************************************************
         * **************************************************************************/

        /// <summary>
        /// To read a registry key.
        /// input: KeyName (string)
        /// output: value (string) 
        /// </summary>
        public string Read(string KeyName)
        {
            // Opening the registry key
            RegistryKey rk = baseRegistryKey;
            // Open a subKey as read-only
            RegistryKey sk1 = rk.OpenSubKey(subKey);
            // If the RegistrySubKey doesn't exist -> (null)
            if (sk1 == null)
            {
                return null;
            }
            else
            {
                try
                {
                    // If the RegistryKey exists I get its value
                    // or null is returned.
                    return (string)sk1.GetValue(KeyName.ToUpper());
                }
                catch (Exception e)
                {
                    // AAAAAAAAAAARGH, an error!
                    ShowErrorMessage(e, "Reading registry " + KeyName.ToUpper());
                    return null;
                }
            }
        }

        /* **************************************************************************
         * **************************************************************************/

        /// <summary>
        /// To write into a registry key.
        /// input: KeyName (string) , Value (object)
        /// output: true or false 
        /// </summary>
        public bool Write(string KeyName, object Value)
        {
            try
            {
                // Setting
                RegistryKey rk = baseRegistryKey;
                // I have to use CreateSubKey 
                // (create or open it if already exits), 
                // 'cause OpenSubKey open a subKey as read-only
                RegistryKey sk1 = rk.CreateSubKey(subKey);
                // Save the value
                sk1.SetValue(KeyName.ToUpper(), Value);

                return true;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                ShowErrorMessage(e, "Writing registry " + KeyName.ToUpper());
                return false;
            }
        }

        /* **************************************************************************
         * **************************************************************************/

        /// <summary>
        /// To delete a registry key.
        /// input: KeyName (string)
        /// output: true or false 
        /// </summary>
        public bool DeleteKey(string KeyName)
        {
            try
            {
                // Setting
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk1 = rk.CreateSubKey(subKey);
                // If the RegistrySubKey doesn't exists -> (true)
                if (sk1 == null)
                    return true;
                else
                    sk1.DeleteValue(KeyName);

                return true;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                ShowErrorMessage(e, "Deleting SubKey " + subKey);
                return false;
            }
        }

        /* **************************************************************************
         * **************************************************************************/

        /// <summary>
        /// To delete a sub key and any child.
        /// input: void
        /// output: true or false 
        /// </summary>
        public bool DeleteSubKeyTree()
        {
            try
            {
                // Setting
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk1 = rk.OpenSubKey(subKey);
                // If the RegistryKey exists, I delete it
                if (sk1 != null)
                    rk.DeleteSubKeyTree(subKey);

                return true;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                ShowErrorMessage(e, "Deleting SubKey " + subKey);
                return false;
            }
        }

        /* **************************************************************************
         * **************************************************************************/

        /// <summary>
        /// Retrive the count of subkeys at the current key.
        /// input: void
        /// output: number of subkeys
        /// </summary>
        public int SubKeyCount()
        {
            try
            {
                // Setting
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk1 = rk.OpenSubKey(subKey);
                // If the RegistryKey exists...
                if (sk1 != null)
                    return sk1.SubKeyCount;
                else
                    return 0;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                ShowErrorMessage(e, "Retriving subkeys of " + subKey);
                return 0;
            }
        }

        /* **************************************************************************
         * **************************************************************************/

        /// <summary>
        /// Retrive the count of values in the key.
        /// input: void
        /// output: number of keys
        /// </summary>
        public int ValueCount()
        {
            try
            {
                // Setting
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk1 = rk.OpenSubKey(subKey);
                // If the RegistryKey exists...
                if (sk1 != null)
                    return sk1.ValueCount;
                else
                    return 0;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                ShowErrorMessage(e, "Retriving keys of " + subKey);
                return 0;
            }
        }

        /* **************************************************************************
         * **************************************************************************/

        private void ShowErrorMessage(Exception e, string Title)
        {
            if (showError == true)
                MessageBox.Show(e.Message,
                                Title
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// hàm thực hiện lọc thông tin của phần bác sỹ
    /// </summary>
    public class FilterAssignStatff
    {
        /// <summary>
        /// lọc thông tin của bác sỹ
        /// </summary>
        /// <returns></returns>
        public static string FilterAssignDoctor()
        {
            return "Staff_ID=-1 or Staff_Type_Type_ID='DOCTOR'";
        }

        /// <summary>
        /// lọc thông tin của y tá
        /// </summary>
        /// <returns></returns>
        public static string FilterAssignNurse()
        {
            return "Staff_ID =-1 or Staff_Type_Type_ID='NURSE'";
        }
    }

    /// <summary>
    /// Summary description for ExcelToXML.
    /// </summary>
    public class UnicodeConverter
    {
        private List<char> tcvnchars = new List<char>()
                                           {
                                               'µ',
                                               '¸',
                                               '¶',
                                               '·',
                                               '¹',
                                               'µ',
                                               '¸',
                                               '¶',
                                               '·',
                                               '¹',
                                               '¨',
                                               '»',
                                               '¾',
                                               '¼',
                                               '½',
                                               'Æ',
                                               '¡',
                                               '»',
                                               '¾',
                                               '¼',
                                               '½',
                                               'Æ',
                                               '©',
                                               'Ç',
                                               'Ê',
                                               'È',
                                               'É',
                                               'Ë',
                                               '¢',
                                               'Ç',
                                               'Ê',
                                               'È',
                                               'É',
                                               'Ë',
                                               '®',
                                               'Ì',
                                               'Ð',
                                               'Î',
                                               'Ï',
                                               'Ñ',
                                               '§',
                                               'Ì',
                                               'Ð',
                                               'Î',
                                               'Ï',
                                               'Ñ',
                                               'ª',
                                               'Ò',
                                               'Õ',
                                               'Ó',
                                               'Ô',
                                               'Ö',
                                               '£',
                                               'Ò',
                                               'Õ',
                                               'Ó',
                                               'Ö',
                                               '×',
                                               'Ý',
                                               'Ø',
                                               'Ü',
                                               'Þ',
                                               '×',
                                               'Ý',
                                               'Ø',
                                               'Ü',
                                               'Þ',
                                               'ß',
                                               'ã',
                                               'á',
                                               'â',
                                               'ä',
                                               'ß',
                                               'ã',
                                               'á',
                                               'â',
                                               'ä',
                                               '«',
                                               'å',
                                               'è',
                                               'æ',
                                               'ç',
                                               'é',
                                               '¤',
                                               'å',
                                               'è',
                                               'æ',
                                               'ç',
                                               'é',
                                               '¬',
                                               'ê',
                                               'í',
                                               'ë',
                                               'ì',
                                               'î',
                                               '¥',
                                               'ê',
                                               'í',
                                               'ë',
                                               'ì',
                                               'î',
                                               'ï',
                                               'ó',
                                               'ñ',
                                               'ò',
                                               'ô',
                                               'ï',
                                               'ó',
                                               'ñ',
                                               'ò',
                                               'ô',
                                               '­',
                                               'õ',
                                               'ø',
                                               'ö',
                                               '÷',
                                               'ù',
                                               '¦',
                                               'õ',
                                               'ø',
                                               'ö',
                                               '÷',
                                               'ù',
                                               'ú',
                                               'ý',
                                               'û',
                                               'ü',
                                               'þ',
                                               'ú',
                                               'ý',
                                               'û',
                                               'ü',
                                               'þ'
                                           };

        private List<char> unichars = new List<char>()
                                          {
                                              'à',
                                              'á',
                                              'ả',
                                              'ã',
                                              'ạ',
                                              'À',
                                              'Á',
                                              'Ả',
                                              'Ã',
                                              'Ạ',
                                              'ă',
                                              'ằ',
                                              'ắ',
                                              'ẳ',
                                              'ẵ',
                                              'ặ',
                                              'Ă',
                                              'Ằ',
                                              'Ắ',
                                              'Ẳ',
                                              'Ẵ',
                                              'Ặ',
                                              'â',
                                              'ầ',
                                              'ấ',
                                              'ẩ',
                                              'ẫ',
                                              'ậ',
                                              'Â',
                                              'Ầ',
                                              'Ấ',
                                              'Ẩ',
                                              'Ẫ',
                                              'Ậ',
                                              'đ',
                                              'è',
                                              'é',
                                              'ẻ',
                                              'ẽ',
                                              'ẹ',
                                              'Đ',
                                              'È',
                                              'É',
                                              'Ẻ',
                                              'Ẽ',
                                              'Ẹ',
                                              'ê',
                                              'ề',
                                              'ế',
                                              'ể',
                                              'ễ',
                                              'ệ',
                                              'Ê',
                                              'Ề',
                                              'Ế',
                                              'Ể',
                                              'Ệ',
                                              'ì',
                                              'í',
                                              'ỉ',
                                              'ĩ',
                                              'ị',
                                              'Ì',
                                              'Í',
                                              'Ỉ',
                                              'Ĩ',
                                              'Ị',
                                              'ò',
                                              'ó',
                                              'ỏ',
                                              'õ',
                                              'ọ',
                                              'Ò',
                                              'Ó',
                                              'Ỏ',
                                              'Õ',
                                              'Ọ',
                                              'ô',
                                              'ồ',
                                              'ố',
                                              'ổ',
                                              'ỗ',
                                              'ộ',
                                              'Ô',
                                              'Ồ',
                                              'Ố',
                                              'Ổ',
                                              'Ỗ',
                                              'Ộ',
                                              'ơ',
                                              'ờ',
                                              'ớ',
                                              'ở',
                                              'ỡ',
                                              'ợ',
                                              'Ơ',
                                              'Ờ',
                                              'Ớ',
                                              'Ở',
                                              'Ỡ',
                                              'Ợ',
                                              'ù',
                                              'ú',
                                              'ủ',
                                              'ũ',
                                              'ụ',
                                              'Ù',
                                              'Ú',
                                              'Ủ',
                                              'Ũ',
                                              'Ụ',
                                              'ư',
                                              'ừ',
                                              'ứ',
                                              'ử',
                                              'ữ',
                                              'ự',
                                              'Ư',
                                              'Ừ',
                                              'Ứ',
                                              'Ử',
                                              'Ữ',
                                              'Ự',
                                              'ỳ',
                                              'ý',
                                              'ỷ',
                                              'ỹ',
                                              'ỵ',
                                              'Ỳ',
                                              'Ý',
                                              'Ỷ',
                                              'Ỹ',
                                              'Ỵ'
                                          };

        private char[] convertTable;
        private char[] convertTableUNICODE2TCVN3;

        public UnicodeConverter()
        {
        }

        public string TCVN3ToUnicode(string value)
        {
            try
            {
                char[] chars = Utility.UpperCaseFirstLetter(value).ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                    if (tcvnchars.Contains(chars[i]))
                        chars[i] = unichars[tcvnchars.IndexOf(chars[i])];
                return new string(chars);
            }
            catch
            {
                return value;
            }
        }

        public string Unicode2TCVN3(string value)
        {
            try
            {
                char[] chars = Utility.UpperCaseFirstLetter(value).ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                    if (unichars.Contains(chars[i]))
                        chars[i] = tcvnchars[unichars.IndexOf(chars[i])];
                return new string(chars);
            }
            catch
            {
                return value;
            }
        }

        /// <summary>
        /// Chuyển đổi dữ liệu trong các cột có kiểu dữ liệu String từ Unicode sang TCVN3 hoặc ngược lại
        /// </summary>
        /// <param name="dtData">Datatable chứa dữ liệu</param>
        /// <param name="IsUnicode2TCVN3">IsUnicode2TCVN3=True=Chuyển từ Unicode sang TCVN3;IsUnicode2TCVN3=True=Chuyển từ TCVN3 sang Unicode</param>
        public void ConvertData(ref DataTable dtData, bool IsUnicode2TCVN3)
        {
            try
            {
                foreach (DataColumn col in dtData.Columns)
                {
                    if (col.DataType.Equals(typeof(string)))
                    {
                        foreach (DataRow dr in dtData.Rows)
                        {
                            dr[col.ColumnName] = IsUnicode2TCVN3
                                                     ? Unicode2TCVN3(dr[col.ColumnName].ToString())
                                                     : TCVN3ToUnicode(dr[col.ColumnName].ToString());
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public void ConvertData(ref DataTable dtData, List<string> lstColumnWillbeConverted, bool IsUnicode2TCVN3)
        {
            try
            {
                foreach (DataColumn col in dtData.Columns)
                {
                    if (lstColumnWillbeConverted.Contains(col.ColumnName) && col.DataType.Equals(typeof(string)))
                    {
                        foreach (DataRow dr in dtData.Rows)
                        {
                            dr[col.ColumnName] = IsUnicode2TCVN3
                                                     ? Unicode2TCVN3(dr[col.ColumnName].ToString())
                                                     : TCVN3ToUnicode(dr[col.ColumnName].ToString());
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }

    public class ExportExcel
    {
        public static void exportToExcel(DataTable dtData, string fileName, string SheetName)
        {
            try
            {
                System.IO.StreamWriter excelDoc;

                excelDoc = new System.IO.StreamWriter(fileName);
                const string startExcelXML = "<xml version>\r\n<Workbook " +
                                             "xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n" +
                                             " xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n " +
                                             "xmlns:x=\"urn:schemas-    microsoft-com:office:" +
                                             "excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:" +
                                             "office:spreadsheet\">\r\n <Styles>\r\n " +
                                             "<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n " +
                                             "<Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>" +
                                             "\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>" +
                                             "\r\n <Protection/>\r\n </Style>\r\n " +
                                             "<Style ss:ID=\"BoldColumn\">\r\n <Font " +
                                             "x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n " +
                                             "<Style     ss:ID=\"StringLiteral\">\r\n <NumberFormat" +
                                             " ss:Format=\"@\"/>\r\n </Style>\r\n <Style " +
                                             "ss:ID=\"Decimal\">\r\n <NumberFormat " +
                                             "ss:Format=\"0.0000\"/>\r\n </Style>\r\n " +
                                             "<Style ss:ID=\"Integer\">\r\n <NumberFormat " +
                                             "ss:Format=\"0\"/>\r\n </Style>\r\n <Style " +
                                             "ss:ID=\"DateLiteral\">\r\n <NumberFormat " +
                                             "ss:Format=\"mm/dd/yyyy;@\"/>\r\n </Style>\r\n " +
                                             "</Styles>\r\n ";
                const string endExcelXML = "</Workbook>";

                int rowCount = 0;
                int sheetCount = 1;
                excelDoc.Write(startExcelXML);
                excelDoc.Write("<Worksheet ss:Name=\"Sheet" + SheetName + "\">");
                excelDoc.Write("<Table>");
                excelDoc.Write("<Row>");
                for (int x = 0; x < dtData.Columns.Count; x++)
                {
                    excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                    excelDoc.Write(dtData.Columns[x].ColumnName);
                    excelDoc.Write("</Data></Cell>");
                }
                excelDoc.Write("</Row>");
                foreach (DataRow x in dtData.Rows)
                {
                    rowCount++;
                    //if the number of rows is > 64000 create a new page to continue output

                    if (rowCount == 64000)
                    {
                        rowCount = 0;
                        sheetCount++;
                        excelDoc.Write("</Table>");
                        excelDoc.Write(" </Worksheet>");
                        excelDoc.Write("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
                        excelDoc.Write("<Table>");
                    }
                    excelDoc.Write("<Row>"); //ID=" + rowCount + "

                    for (int y = 0; y < dtData.Columns.Count; y++)
                    {
                        System.Type rowType;
                        rowType = x[y].GetType();
                        switch (rowType.ToString())
                        {
                            case "System.String":
                                string XMLstring = x[y].ToString();
                                XMLstring = XMLstring.Trim();
                                XMLstring = XMLstring.Replace("&", "&");
                                XMLstring = XMLstring.Replace(">", ">");
                                XMLstring = XMLstring.Replace("<", "<");
                                excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                               "<Data ss:Type=\"String\">");
                                excelDoc.Write(XMLstring);
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.DateTime":
                                //Excel has a specific Date Format of YYYY-MM-DD followed by 

                                //the letter 'T' then hh:mm:sss.lll Example 2005-01-31T24:01:21.000

                                //The Following Code puts the date stored in XMLDate

                                //to the format above

                                DateTime XMLDate = (DateTime)x[y];
                                string XMLDatetoString = ""; //Excel Converted Date

                                XMLDatetoString = XMLDate.Year.ToString() +
                                                  "-" +
                                                  (XMLDate.Month < 10
                                                       ? "0" +
                                                         XMLDate.Month.ToString()
                                                       : XMLDate.Month.ToString()) +
                                                  "-" +
                                                  (XMLDate.Day < 10
                                                       ? "0" +
                                                         XMLDate.Day.ToString()
                                                       : XMLDate.Day.ToString()) +
                                                  "T" +
                                                  (XMLDate.Hour < 10
                                                       ? "0" +
                                                         XMLDate.Hour.ToString()
                                                       : XMLDate.Hour.ToString()) +
                                                  ":" +
                                                  (XMLDate.Minute < 10
                                                       ? "0" +
                                                         XMLDate.Minute.ToString()
                                                       : XMLDate.Minute.ToString()) +
                                                  ":" +
                                                  (XMLDate.Second < 10
                                                       ? "0" +
                                                         XMLDate.Second.ToString()
                                                       : XMLDate.Second.ToString()) +
                                                  ".000";
                                excelDoc.Write("<Cell ss:StyleID=\"DateLiteral\">" +
                                               "<Data ss:Type=\"DateTime\">");
                                excelDoc.Write(XMLDatetoString);
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.Boolean":
                                excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                               "<Data ss:Type=\"String\">");
                                excelDoc.Write(x[y].ToString());
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                excelDoc.Write("<Cell ss:StyleID=\"Integer\">" +
                                               "<Data ss:Type=\"Number\">");
                                excelDoc.Write(x[y].ToString());
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.Decimal":
                            case "System.Double":
                                string _value = x[y].ToString();
                                if (_value.Contains(".") || _value.Contains(","))
                                {
                                }
                                else
                                {
                                }
                                excelDoc.Write("<Cell ss:StyleID=\"Decimal\">" +
                                               "<Data ss:Type=\"Number\">");
                                excelDoc.Write(_value);
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.DBNull":
                                excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                               "<Data ss:Type=\"String\">");
                                excelDoc.Write("");
                                excelDoc.Write("</Data></Cell>");
                                break;
                            default:
                                throw (new Exception(rowType.ToString() + " not handled."));
                        }
                    }
                    excelDoc.Write("</Row>");
                }
                excelDoc.Write("</Table>");
                excelDoc.Write(" </Worksheet>");
                excelDoc.Write(endExcelXML);
                excelDoc.Close();
            }
            catch
            {
            }
        }
    }

    namespace AppUI
    {
        public class UIAction
        {
            delegate void _Scroll2Control(FlowLayoutPanel _parent, Control _child);
            public static void Scroll2Control(FlowLayoutPanel _parent, Control _child)
            {
                try
                {
                    if (_parent.InvokeRequired)
                        _parent.Invoke(new _Scroll2Control(Scroll2Control), new object[] { _parent, _child });
                    else
                        _parent.ScrollControlIntoView(_child);

                }
                catch
                {
                }
            }

            delegate void _AddControl(Control _parent, Control _child);
            public static void AddControl(Control _parent, Control _child)
            {
                try
                {
                    if (_parent.InvokeRequired)
                        _parent.Invoke(new _AddControl(AddControl), new object[] { _parent, _child });
                    else
                        _parent.Controls.Add(_child);

                }
                catch
                {
                }
            }

            delegate void _RemoveControl(Control _parent, Control _child);
            public static void RemoveControl(Control _parent, Control _child)
            {
                try
                {
                    if (_parent.InvokeRequired)
                        _parent.Invoke(new _RemoveControl(RemoveControl), new object[] { _parent, _child });
                    else
                        _parent.Controls.Remove(_child);

                }
                catch
                {
                }
            }

            delegate void Bring2Front(Control ctr, bool _bring2Front);
            public static void _Bring2Front(Control ctr, bool _bring2Front)
            {
                try
                {
                    if (ctr.InvokeRequired)
                    {
                        ctr.Invoke(new Bring2Front(_Bring2Front), new object[] { ctr, _bring2Front });
                    }
                    else
                    {
                        if (_bring2Front) ctr.BringToFront();
                        else ctr.SendToBack();
                    }
                }
                catch
                {
                }
                finally
                {
                    Application.DoEvents();
                }
            }
            delegate void FocusControl(Control ctr);
            public static void FocusMe(Control ctr)
            {
                try
                {
                    if (ctr.InvokeRequired)
                    {
                        ctr.Invoke(new FocusControl(FocusMe), new object[] { ctr });
                    }
                    else
                    {
                        ctr.Focus();
                        ctr.Select();
                        Application.DoEvents();
                    }
                }
                catch
                {
                }
                finally
                {
                    Application.DoEvents();
                }
            }
            delegate void FocusControlEditbox(EditBox ctr);
            public static void FocusEditbox(EditBox ctr)
            {
                try
                {
                    if (ctr.InvokeRequired)
                    {
                        ctr.Invoke(new FocusControlEditbox(FocusEditbox), new object[] { ctr });
                    }
                    else
                    {
                        ctr.Focus();
                        ctr.SelectAll();
                        Application.DoEvents();
                    }
                }
                catch
                {
                }
                finally
                {
                    Application.DoEvents();
                }
            }
            delegate void FocusControlTextbox(TextBox ctr);
            public static void FocusTextBox(TextBox ctr)
            {
                try
                {
                    if (ctr.InvokeRequired)
                    {
                        ctr.Invoke(new FocusControlTextbox(FocusTextBox), new object[] { ctr });
                    }
                    else
                    {
                        ctr.Focus();
                        ctr.SelectAll();
                        Application.DoEvents();
                    }
                }
                catch
                {
                }
                finally
                {
                    Application.DoEvents();
                }
            }
            delegate void VisibleControl(Control ctr, bool _visible);
            public static void _Visible(Control ctr, bool _visible)
            {
                try
                {
                    if (ctr.InvokeRequired)
                    {
                        ctr.Invoke(new VisibleControl(_Visible), new object[] { ctr, _visible });
                    }
                    else
                    {
                        ctr.Visible = _visible;
                        Application.DoEvents();
                    }
                }
                catch
                {
                }
                finally
                {
                    Application.DoEvents();
                }
            }
            delegate void SetPrgValue(ProgressBar Prg, int _Value);
            public static void SetValue4Prg(ProgressBar Prg, int _Value)
            {
                try
                {

                    if (Prg.InvokeRequired)
                    {
                        Prg.Invoke(new SetPrgValue(SetValue4Prg), new object[] { Prg, _Value });
                    }
                    else
                    {
                        Prg.Value += _Value;
                        Prg.Refresh();
                    }
                }
                catch
                {
                }
                finally
                {
                    Application.DoEvents();
                }
            }
            delegate void EnableControl(Control ctrl, bool isEnabled, string text);
            public static void _EnableControl(Control ctrl, bool isEnabled, string text)
            {
                try
                {
                    if (ctrl.InvokeRequired)
                    {
                        ctrl.Invoke(new EnableControl(_EnableControl), new object[] { ctrl, isEnabled, text });
                    }
                    else
                    {
                        ctrl.Enabled = isEnabled;
                        if (text.Trim() != "") ctrl.Text = text;
                        Application.DoEvents();
                    }
                }
                catch
                {
                }
                finally
                {
                    Application.DoEvents();
                }
            }

            delegate void _SetText(Control ctr, string content);
            public static void SetText(Control ctr, string content)
            {
                try
                {
                    if (ctr.InvokeRequired)
                    {
                        ctr.Invoke(new _SetText(SetText), new object[] { ctr, content });
                    }
                    else
                    {
                        ctr.Text = content;
                        ctr.Refresh();

                    }
                }
                catch
                {
                }
                finally
                {
                    Application.DoEvents();
                }
            }
            delegate void _SetBackGroundImg(Control ctr, Bitmap bmp);
            public static void SetBackGroundImg(Control ctr, Bitmap bmp)
            {
                try
                {
                    if (ctr.InvokeRequired)
                    {
                        ctr.Invoke(new _SetBackGroundImg(SetBackGroundImg), new object[] { ctr, bmp });
                    }
                    else
                    {
                        ctr.BackgroundImage = bmp;
                        ctr.Invalidate();

                    }
                }
                catch
                {
                }
                finally
                {
                    Application.DoEvents();
                }
            }
            delegate void _SetTextStatus(Control ctr, string content, Color errorColor);
            public static void SetTextStatus(Control ctr, string content, Color errorColor)
            {
                try
                {
                    if (ctr.InvokeRequired)
                    {
                        ctr.Invoke(new _SetTextStatus(SetTextStatus), new object[] { ctr, content, errorColor });
                    }
                    else
                    {
                        ctr.Text = content;
                        if (errorColor == Color.White) ctr.ForeColor = Color.Black;
                        else ctr.ForeColor = errorColor;
                        ctr.Refresh();

                    }
                }
                catch
                {
                }
                finally
                {
                    Application.DoEvents();
                }
            }
            delegate void _SetTextStatus1(Control ctr, string content, bool error);
            public static void SetTextStatus(Control ctr, string content, bool error)
            {
                try
                {
                    if (ctr.InvokeRequired)
                    {
                        ctr.Invoke(new _SetTextStatus1(SetTextStatus), new object[] { ctr, content, error });
                    }
                    else
                    {
                        ctr.Text = content;
                        if (!error) ctr.ForeColor = Color.DarkBlue;
                        else ctr.ForeColor = Color.Red;
                        ctr.Refresh();

                    }
                }
                catch
                {
                }
                finally
                {
                    Application.DoEvents();
                }
            }
            delegate void _HightLighLable(Label ctr, bool AllowHightLight, System.Drawing.Font _NormalFont, System.Drawing.Font _SelectedFont, Color _NormalforeColor, Color _SelectedforeColor);
            public static void HightLighLable(Label ctr, bool AllowHightLight, System.Drawing.Font _NormalFont, System.Drawing.Font _SelectedFont, Color _NormalforeColor, Color _SelectedforeColor)
            {
                try
                {

                    if (ctr.InvokeRequired)
                    {
                        ctr.Invoke(new _HightLighLable(HightLighLable), new object[] { ctr, AllowHightLight, _NormalFont, _SelectedFont, _NormalforeColor, _SelectedforeColor });
                    }
                    else
                    {
                        if (AllowHightLight)
                        {
                            ctr.ForeColor = _SelectedforeColor;
                            ctr.Font = new Font(_SelectedFont.FontFamily, _SelectedFont.Size, _SelectedFont.Style);
                        }
                        else
                        {
                            ctr.ForeColor = _NormalforeColor;
                            ctr.Font = new Font(_NormalFont.FontFamily, _NormalFont.Size, _NormalFont.Style);
                        }
                        ctr.Refresh();

                    }
                }
                catch
                {
                }
                finally
                {
                    Application.DoEvents();
                }
            }
        }
    }
    namespace AppLogger
    {
        public class LogAction
        {

            delegate void AddLog(ListBox lstAct, string content);
            public static void AddLog2List(ListBox lstAct, string content)
            {
                try
                {
                    if (lstAct.InvokeRequired)
                    {
                        lstAct.Invoke(new AddLog(AddLog2List), new object[] { lstAct, content });
                    }
                    else
                    {
                        lstAct.BeginUpdate();
                        lstAct.Items.Add(content);
                        lstAct.EndUpdate();
                    }
                }
                catch
                {
                }
            }
            #region LogErrors
            public static void LogActions(string _content)
            {
                try
                {

                    string ActionLog = Application.StartupPath + @"\Logs\AppLog_" + Utility.GetYYMMDD(DateTime.Now) + ".txt";
                    string ParentFolder = Path.GetDirectoryName(ActionLog);
                    if (!Directory.Exists(ParentFolder)) Directory.CreateDirectory(ParentFolder);
                    bool IsAppend = File.Exists(ActionLog);
                    using (StreamWriter writer = new StreamWriter(ActionLog, IsAppend))
                    {
                        writer.WriteLine(_content);
                        writer.Flush();
                        writer.Close();
                    }
                }
                catch
                {
                }

            }
            public static string getApplicationStartupPath()
            {
                string _StartupPath = AppDomain.CurrentDomain.BaseDirectory.Trim();
                if (_StartupPath.EndsWith(@"\")) _StartupPath = _StartupPath.Substring(0, _StartupPath.Length - 1);
                return _StartupPath;
            }
            public static string getApplicationStartupPathEndedWithDirectorySeparatorChar()
            {
                string _StartupPath = AppDomain.CurrentDomain.BaseDirectory.Trim();
                if (_StartupPath.EndsWith(@"\")) _StartupPath = _StartupPath.Substring(0, _StartupPath.Length - 1);
                return _StartupPath + @"\";
            }
            public static void LogSCPService(string _content)
            {
                try
                {
                    string ActionLog = getApplicationStartupPathEndedWithDirectorySeparatorChar() + @"ServicesLogs\AppLog_" + Utility.GetYYMMDD(DateTime.Now) + ".txt";
                    string ParentFolder = Path.GetDirectoryName(ActionLog);
                    if (!Directory.Exists(ParentFolder)) Directory.CreateDirectory(ParentFolder);
                    bool IsAppend = File.Exists(ActionLog);
                    using (StreamWriter writer = new StreamWriter(ActionLog, IsAppend))
                    {
                        writer.WriteLine(_content);
                        writer.Flush();
                        writer.Close();
                    }
                }
                catch
                {
                }

            }
            public static void LogWindowService(string _content)
            {
                try
                {

                    string ActionLog = getApplicationStartupPathEndedWithDirectorySeparatorChar() + @"ServicesLogs\AppLog_" + Utility.GetYYMMDD(DateTime.Now) + ".txt";
                    string ParentFolder = Path.GetDirectoryName(ActionLog);
                    if (!Directory.Exists(ParentFolder)) Directory.CreateDirectory(ParentFolder);
                    bool IsAppend = File.Exists(ActionLog);
                    using (StreamWriter writer = new StreamWriter(ActionLog, IsAppend))
                    {
                        writer.WriteLine(_content);
                        writer.Flush();
                        writer.Close();
                    }
                }
                catch
                {
                }

            }
            public static void LogActions(string ActionLogFile, string _content)
            {
                try
                {
                    string ParentFolder = Path.GetDirectoryName(ActionLogFile);
                    if (!Directory.Exists(ParentFolder)) Directory.CreateDirectory(ParentFolder);
                    bool IsAppend = File.Exists(ActionLogFile);
                    using (StreamWriter writer = new StreamWriter(ActionLogFile, IsAppend))
                    {
                        writer.WriteLine(_content);
                        writer.Flush();
                        writer.Close();
                    }
                }
                catch
                {
                }

            }
            #endregion
        }
    }
    namespace AppType
    {
        public class AppEnum
        {
            public enum Sta_Cbo_Cmd_VisualStyle
            {
                Default = 0,
                OfficeXP = 1,
                Standard = 2,
                VS2005 = 3,
                Office2003 = 4,
                Office2007 = 5,
            }

            public enum Normal_VisualStyle
            {
                Default = 0,
                Standard = 1,
                Office2003 = 2,
                VS2005 = 3,
                Office2007 = 4,
            }
            public enum Tab_VisualStyle
            {
                Flat = 0,
                Office2003 = 1,
                VS2005 = 2,
                Normal = 3,
                Light3D = 4,
                Office2007 = 5,
            }
            public enum InsertReturn
            {
                Exists = 0, Error = 1, Success = 2
            }
            public enum WORKLISTSEARCHTYPE { ModalityWorklistQuery = 0, PatientRootQueryStudy = 1 }
            public enum HardKeyType { SOFTKEY = 0, USBDONGLE = 1, MIXEDMODE = 2, SOFTKEY_EXP = 3 }
            public enum AppName { XFILM = 0, XVIEW = 1, RISLINK = 2, DICOMVIEWER = 3,QLPK=4 }
            public enum HIDReaderType { Internal = 0, FromFile = 1 }
            public enum DataBaseType { SQLSERVER = 0, SQLCE = 1, OLEDB = 2, ORACLE = 3 }
            public enum Navigation { None = 0, Next = 1, Back = 2 };
            public enum TabMode { WorkList = 0, Acq = 1, StudyList = 2, System = 3, Viewer = 4, Print = 5, Option = 6, List = 7, Other = 8, Suspending = 9 };
            public enum DoubleMode { WorkList = 0, StudyList = 2 };
            public enum AppMode { Demo = 0, License = 1, Developer = 2 };
            public enum ViewState { Ready = 0, Capture = 1 };
            public enum FPDMode { SingleMode = 0, DualMode = 1, Other = 2 };
            public enum LoginMode { None = -1, Login = 0, Logout = 1 };
            public enum DepartmentType { ngoaitru = 0, noitru = 1, tatca = 2 };
            public enum ClickMode { Click = 0, DoubleClick = 1 };
            public enum ToolMode { None = 0, WindowLevel = 1, Zoom = 2, Magnify = 3, Crop = 4, Annotation = 5, Select = 6 };
            public enum AnnType { None = 0, Arrow = 1, Square = 2, Ellipse = 3, Pen = 4, Angle = 5, Ruler = 6 };
        }
    }

    namespace RegistryManager
    {
        /// <summary>
        /// An useful class to read/write/delete/count registry keys
        /// </summary>
        public class RegManager
        {
            private bool showError = false;

            /// <summary>
            /// A property to show or hide error messages 
            /// (default = false)
            /// </summary>
            public bool ShowError
            {
                get { return showError; }
                set { showError = value; }
            }

            private string subKey = "SOFTWARE\\" + Application.ProductName.ToUpper();

            /// <summary>
            /// A property to set the SubKey value
            /// (default = "SOFTWARE\\" + Application.ProductName.ToUpper())
            /// </summary>
            public string SubKey
            {
                get { return subKey; }
                set { subKey = value; }
            }

            private RegistryKey baseRegistryKey = Registry.LocalMachine;

            /// <summary>
            /// A property to set the BaseRegistryKey value.
            /// (default = Registry.LocalMachine)
            /// </summary>
            public RegistryKey BaseRegistryKey
            {
                get { return baseRegistryKey; }
                set { baseRegistryKey = value; }
            }

            /* **************************************************************************
             * **************************************************************************/

            /// <summary>
            /// To read a registry key.
            /// input: KeyName (string)
            /// output: value (string) 
            /// </summary>
            public string Read(string KeyName)
            {
                // Opening the registry key
                RegistryKey rk = baseRegistryKey;
                // Open a subKey as read-only
                RegistryKey sk1 = rk.OpenSubKey(subKey);
                // If the RegistrySubKey doesn't exist -> (null)
                if (sk1 == null)
                {
                    return "";
                }
                else
                {
                    try
                    {
                        // If the RegistryKey exists I get its value
                        // or null is returned.
                        return (string)sk1.GetValue(KeyName.ToUpper());
                    }
                    catch (Exception e)
                    {
                        // AAAAAAAAAAARGH, an error!
                        ShowErrorMessage(e, "Reading registry " + KeyName.ToUpper());
                        return "";
                    }
                }
            }

            /* **************************************************************************
             * **************************************************************************/

            /// <summary>
            /// To write into a registry key.
            /// input: KeyName (string) , Value (object)
            /// output: true or false 
            /// </summary>
            public bool Write(string KeyName, object Value)
            {
                try
                {
                    // Setting
                    RegistryKey rk = baseRegistryKey;
                    // I have to use CreateSubKey 
                    // (create or open it if already exits), 
                    // 'cause OpenSubKey open a subKey as read-only
                    RegistryKey sk1 = rk.CreateSubKey(subKey);
                    // Save the value
                    sk1.SetValue(KeyName.ToUpper(), Value);

                    return true;
                }
                catch (Exception e)
                {
                    // AAAAAAAAAAARGH, an error!
                    ShowErrorMessage(e, "Writing registry " + KeyName.ToUpper());
                    return false;
                }
            }

            /* **************************************************************************
             * **************************************************************************/

            /// <summary>
            /// To delete a registry key.
            /// input: KeyName (string)
            /// output: true or false 
            /// </summary>
            public bool DeleteKey(string KeyName)
            {
                try
                {
                    // Setting
                    RegistryKey rk = baseRegistryKey;
                    RegistryKey sk1 = rk.CreateSubKey(subKey);
                    // If the RegistrySubKey doesn't exists -> (true)
                    if (sk1 == null)
                        return true;
                    else
                        sk1.DeleteValue(KeyName);

                    return true;
                }
                catch (Exception e)
                {
                    // AAAAAAAAAAARGH, an error!
                    ShowErrorMessage(e, "Deleting SubKey " + subKey);
                    return false;
                }
            }

            /* **************************************************************************
             * **************************************************************************/

            /// <summary>
            /// To delete a sub key and any child.
            /// input: void
            /// output: true or false 
            /// </summary>
            public bool DeleteSubKeyTree()
            {
                try
                {
                    // Setting
                    RegistryKey rk = baseRegistryKey;
                    RegistryKey sk1 = rk.OpenSubKey(subKey);
                    // If the RegistryKey exists, I delete it
                    if (sk1 != null)
                        rk.DeleteSubKeyTree(subKey);

                    return true;
                }
                catch (Exception e)
                {
                    // AAAAAAAAAAARGH, an error!
                    ShowErrorMessage(e, "Deleting SubKey " + subKey);
                    return false;
                }
            }

            /* **************************************************************************
             * **************************************************************************/

            /// <summary>
            /// Retrive the count of subkeys at the current key.
            /// input: void
            /// output: number of subkeys
            /// </summary>
            public int SubKeyCount()
            {
                try
                {
                    // Setting
                    RegistryKey rk = baseRegistryKey;
                    RegistryKey sk1 = rk.OpenSubKey(subKey);
                    // If the RegistryKey exists...
                    if (sk1 != null)
                        return sk1.SubKeyCount;
                    else
                        return 0;
                }
                catch (Exception e)
                {
                    // AAAAAAAAAAARGH, an error!
                    ShowErrorMessage(e, "Retriving subkeys of " + subKey);
                    return 0;
                }
            }

            /* **************************************************************************
             * **************************************************************************/

            /// <summary>
            /// Retrive the count of values in the key.
            /// input: void
            /// output: number of keys
            /// </summary>
            public int ValueCount()
            {
                try
                {
                    // Setting
                    RegistryKey rk = baseRegistryKey;
                    RegistryKey sk1 = rk.OpenSubKey(subKey);
                    // If the RegistryKey exists...
                    if (sk1 != null)
                        return sk1.ValueCount;
                    else
                        return 0;
                }
                catch (Exception e)
                {
                    // AAAAAAAAAAARGH, an error!
                    ShowErrorMessage(e, "Retriving keys of " + subKey);
                    return 0;
                }
            }

            /* **************************************************************************
             * **************************************************************************/

            private void ShowErrorMessage(Exception e, string Title)
            {
                if (showError == true)
                    MessageBox.Show(e.Message,
                                    Title
                                    , MessageBoxButtons.OK
                                    , MessageBoxIcon.Error);
            }
        }
    }
    #region "FTP client class"
    /// <summary>
    /// A wrapper class for .NET 2.0 FTP protocol
    /// </summary>
    /// <remarks>
    /// This class does not hold open an FTP connection but
    /// instead is stateless: for each FTP request it
    /// connects, performs the request and disconnects.
    /// 
    /// v1.0 - original version
    /// 
    /// v1.1 - added support for EnableSSL, UsePassive and Proxy connections
    /// 
    /// v1.2 - added support for downloading correct date/time from FTP server for
    ///        each file
    ///        Added FtpDirectoryExists function as FtpFileExists does not work as directory
    ///        exists check.
    ///        Amended all URI encoding to ensure special characters are encoded 
    /// </remarks>
    public class FTPclient
    {

        #region "CONSTRUCTORS"
        /// <summary>
        /// Blank constructor
        /// </summary>
        /// <remarks>Hostname, username and password must be set manually</remarks>
        public FTPclient()
        {
        }

        /// <summary>
        /// Constructor just taking the hostname
        /// </summary>
        /// <param name="Hostname">in either ftp://ftp.host.com or ftp.host.com form</param>
        /// <remarks></remarks>
        public FTPclient(string Hostname)
        {
            _hostname = Hostname;
        }

        /// <summary>
        /// Constructor taking hostname, username and password
        /// </summary>
        /// <param name="Hostname">in either ftp://ftp.host.com or ftp.host.com form</param>
        /// <param name="Username">Leave blank to use 'anonymous' but set password to your email</param>
        /// <param name="Password"></param>
        /// <remarks></remarks>
        public FTPclient(string Hostname, string Username, string Password)
        {
            _hostname = Hostname;
            _username = Username;
            _password = Password;
        }

        /// <summary>
        /// Constructor taking hostname, username, password and KeepAlive property
        /// </summary>
        /// <param name="Hostname">in either ftp://ftp.host.com or ftp.host.com form</param>
        /// <param name="Username">Leave blank to use 'anonymous' but set password to your email</param>
        /// <param name="Password">Password</param>
        /// <param name="KeepAlive">Set True to keep connection alive after each request</param>
        /// <remarks></remarks>
        public FTPclient(string Hostname, string Username, string Password, bool KeepAlive)
        {
            _hostname = Hostname;
            _username = Username;
            _password = Password;
            _keepAlive = KeepAlive;
        }

        #endregion

        #region "Directory functions"
        /// <summary>
        /// Return a simple directory listing
        /// </summary>
        /// <param name="directory">Directory to list, e.g. /pub</param>
        /// <returns>A list of filenames and directories as a List(of String)</returns>
        /// <remarks>For a detailed directory listing, use ListDirectoryDetail</remarks>
        public List<string> ListDirectory(string directory)
        {
            //return a simple list of filenames in directory
            String URI = GetDirectory(directory);
            System.Net.FtpWebRequest ftp = GetRequest(URI);
            //Set request to do simple list
            ftp.Method = System.Net.WebRequestMethods.Ftp.ListDirectory;

            string str = GetStringResponse(ftp);
            //replace CRLF to CR, remove last instance
            str = str.Replace("\r\n", "\r").TrimEnd('\r');
            //split the string into a list
            List<string> result = new List<string>();
            result.AddRange(str.Split('\r'));
            return result;
        }

        /// <summary>
        /// List current directory
        /// </summary>
        /// <returns></returns>
        public FTPdirectory ListDirectoryDetail()
        {
            return ListDirectoryDetail("", false);
        }

        /// <summary>
        /// List specified directory (do not obtain datetime stamps)
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public FTPdirectory ListDirectoryDetail(string directory)
        {
            return ListDirectoryDetail(directory, false);
        }

        /// <summary>
        /// Return a detailed directory listing, and also download datetime stamps if specified
        /// </summary>
        /// <param name="directory">Directory to list, e.g. /pub/etc</param>
        /// <param name="doDateTimeStamp">Boolean: set to True to download the datetime stamp for files</param>
        /// <returns>An FTPDirectory object</returns>
        public FTPdirectory ListDirectoryDetail(string directory, bool doDateTimeStamp)
        {
            String URI = GetDirectory(directory);
            System.Net.FtpWebRequest ftp = GetRequest(URI);
            // Set request to do simple list
            ftp.Method = System.Net.WebRequestMethods.Ftp.ListDirectoryDetails;

            string str = GetStringResponse(ftp);
            // replace CRLF to CR, remove last instance
            str = str.Replace("\r\n", "\r").TrimEnd('\r');
            // split the string into a list
            FTPdirectory dir = new FTPdirectory(str, _lastDirectory);

            // download timestamps if requested
            if (doDateTimeStamp)
            {
                foreach (FTPfileInfo fi in dir)
                {
                    fi.FileDateTime = this.GetDateTimestamp(fi);
                }
            }

            return dir;
        }

        #endregion

        #region "Upload: File transfer TO ftp server"

        /// <summary>
        /// Copy a local file to the FTP server from local filename string
        /// </summary>
        /// <param name="localFilename">Full path of the local file</param>
        /// <param name="targetFilename">Target filename, if required</param>
        /// <returns></returns>
        /// <remarks>If the target filename is blank, the source filename is used
        /// (assumes current directory). Otherwise use a filename to specify a name
        /// or a full path and filename if required.</remarks>
        public bool Upload(string localFilename, string targetFilename)
        {
            //1. check source
            if (!File.Exists(localFilename))
            {
                throw (new ApplicationException("File " + localFilename + " not found"));
            }
            //copy to FI
            FileInfo fi = new FileInfo(localFilename);
            return Upload(fi, targetFilename);
        }

        /// <summary>
        /// Upload a local file to the FTP server
        /// </summary>
        /// <param name="fi">Source file</param>
        /// <param name="targetFilename">Target filename (optional)</param>
        /// <returns>
        /// 1.2 [HR] simplified checks on target
        /// </returns>
        public bool Upload(FileInfo fi, string targetFilename)
        {
            //copy the file specified to target file: target file can be full path or just filename (uses current dir)

            //1. check target
            string target;
            if (String.IsNullOrEmpty(targetFilename))
            {
                //Blank target: use source filename & current dir
                target = this.CurrentDirectory + fi.Name;
            }
            else
            {
                //otherwise use original
                target = targetFilename;
            }
            using (FileStream fs = fi.OpenRead())
            {
                try
                {
                    return Upload(fs, target);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    //ensure file closed
                    fs.Close();
                }
            }
            return false;
        }

        /// <summary>
        /// Upload a local source strean to the FTP server
        /// </summary>
        /// <param name="sourceStream">Source Stream</param>
        /// <param name="targetFilename">Target filename</param>
        /// <returns>
        /// 1.2 [HR] added CreateURI
        /// </returns>
        public bool Upload(Stream sourceStream, string targetFilename)
        {
            // validate the target file
            if (string.IsNullOrEmpty(targetFilename))
            {
                throw new ApplicationException("Target filename must be specified");
            };

            //perform copy
            string URI = CreateURI(targetFilename);
            System.Net.FtpWebRequest ftp = GetRequest(URI);

            //Set request to upload a file in binary
            ftp.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
            ftp.UseBinary = true;

            //Notify FTP of the expected size
            ftp.ContentLength = sourceStream.Length;

            //create byte array to store: ensure at least 1 byte!
            const int BufferSize = 2048;
            byte[] content = new byte[BufferSize - 1 + 1];
            int dataRead;

            //open file for reading
            using (sourceStream)
            {
                try
                {
                    sourceStream.Position = 0;
                    //open request to send
                    using (Stream rs = ftp.GetRequestStream())
                    {
                        do
                        {
                            dataRead = sourceStream.Read(content, 0, BufferSize);
                            rs.Write(content, 0, dataRead);
                        } while (!(dataRead < BufferSize));
                        rs.Close();
                    }
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    //ensure file closed
                    sourceStream.Close();
                    ftp = null;
                }
            }
            return false;
        }


        #endregion

        #region "Download: File transfer FROM ftp server"
        /// <summary>
        /// Copy a file from FTP server to local
        /// </summary>
        /// <param name="sourceFilename">Target filename, if required</param>
        /// <param name="localFilename">Full path of the local file</param>
        /// <returns></returns>
        /// <remarks>
        /// Target can be blank (use same filename), or just a filename
        /// (assumes current directory) or a full path and filename
        /// 1.2 [HR] added CreateURI
        /// </remarks>
        public bool Download(string sourceFilename, string localFilename, bool PermitOverwrite)
        {
            //2. determine target file
            FileInfo fi = new FileInfo(localFilename);
            return this.Download(sourceFilename, fi, PermitOverwrite);
        }

        /// <summary>
        /// Version taking an FtpFileInfo
        /// </summary>
        /// <param name="file"></param>
        /// <param name="localFilename"></param>
        /// <param name="PermitOverwrite"></param>
        /// <returns></returns>
        public bool Download(FTPfileInfo file, string localFilename, bool PermitOverwrite)
        {
            return this.Download(file.FullName, localFilename, PermitOverwrite);
        }

        /// <summary>
        /// Another version taking FtpFileInfo and FileInfo
        /// </summary>
        /// <param name="file"></param>
        /// <param name="localFI"></param>
        /// <param name="PermitOverwrite"></param>
        /// <returns></returns>
        public bool Download(FTPfileInfo file, FileInfo localFI, bool PermitOverwrite)
        {
            return this.Download(file.FullName, localFI, PermitOverwrite);
        }

        /// <summary>
        /// Version taking string/FileInfo
        /// </summary>
        /// <param name="sourceFilename"></param>
        /// <param name="targetFI"></param>
        /// <param name="PermitOverwrite"></param>
        /// <returns></returns>
        public bool Download(string sourceFilename, FileInfo targetFI, bool PermitOverwrite)
        {
            //1. check target
            if (targetFI.Exists && !(PermitOverwrite))
            {
                throw (new ApplicationException("Target file already exists"));
            }

            //2. check source
            if (String.IsNullOrEmpty(sourceFilename))
            {
                throw (new ApplicationException("File not specified"));
            }

            //3. perform copy
            string URI = CreateURI(sourceFilename);
            System.Net.FtpWebRequest ftp = GetRequest(URI);

            //Set request to download a file in binary mode
            ftp.Method = System.Net.WebRequestMethods.Ftp.DownloadFile;
            ftp.UseBinary = true;

            //open request and get response stream
            using (FtpWebResponse response = (FtpWebResponse)ftp.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    //loop to read & write to file
                    using (FileStream fs = targetFI.OpenWrite())
                    {
                        try
                        {
                            byte[] buffer = new byte[2048];
                            int read = 0;
                            do
                            {
                                read = responseStream.Read(buffer, 0, buffer.Length);
                                fs.Write(buffer, 0, read);
                            } while (!(read == 0));
                            responseStream.Close();
                            fs.Flush();
                            fs.Close();
                        }
                        catch (Exception)
                        {
                            //catch error and delete file only partially downloaded
                            fs.Close();
                            //delete target file as it's incomplete
                            targetFI.Delete();
                            throw;
                        }
                    }

                    responseStream.Close();
                }

                response.Close();
            }


            return true;
        }
        #endregion

        #region "Other functions: Delete rename etc."
        /// <summary>
        /// Delete remote file
        /// </summary>
        /// <param name="filename">filename or full path</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool FtpDelete(string filename)
        {
            //Determine if file or full path
            string URI = CreateURI(filename);
            System.Net.FtpWebRequest ftp = GetRequest(URI);
            //Set request to delete
            ftp.Method = System.Net.WebRequestMethods.Ftp.DeleteFile;
            try
            {
                //get response but ignore it
                string str = GetStringResponse(ftp);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Determine if file exists on remote FTP site
        /// </summary>
        /// <param name="filename">Filename (for current dir) or full path</param>
        /// <returns></returns>
        /// <remarks>Note this only works for files</remarks>
        public bool FtpFileExists(string filename)
        {
            //Try to obtain filesize: if we get error msg containing "550"
            //the file does not exist
            try
            {
                long size = GetFileSize(filename);
                return true;
            }
            catch (System.Net.WebException webex)
            {
                if (webex.Message.Contains("550")) return false;
                // other errors not handled
                throw;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Determine if a directory exists on remote ftp server
        /// </summary>
        /// <param name="remoteDir">Directory path, e.g. /pub/test</param>
        /// <returns>True if directory exists, otherwise false</returns>
        /// <remarks></remarks>
        public bool FtpDirectoryExists(string remoteDir)
        {
            try
            {
                // attempt directory listing
                List<string> files = this.ListDirectory(remoteDir);
                return true;
            }
            catch (System.Net.WebException webex)
            {
                // error should contain 550 if not found
                if (webex.Message.Contains("550")) return false;
                // other error - not handled
                throw;
            }
            catch
            {
                // all other errors
                throw;
            }

        }


        /// <summary>
        /// Determine size of remote file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        /// <remarks>Throws an exception if file does not exist</remarks>
        public long GetFileSize(string filename)
        {
            string URI = CreateURI(filename);

            System.Net.FtpWebRequest ftp = GetRequest(URI);
            //Try to get info on file/dir?
            ftp.Method = System.Net.WebRequestMethods.Ftp.GetFileSize;
            string tmp = this.GetStringResponse(ftp);
            return GetSize(ftp);
        }

        /// <summary>
        /// Rename a remote file
        /// </summary>
        /// <param name="sourceFilename">Either partial or full filename</param>
        /// <param name="newName">Partial or full name to rename to</param>
        /// <returns></returns>
        public bool FtpRename(string sourceFilename, string newName)
        {
            //Does file exist?
            string source = GetFullPath(sourceFilename);
            if (!FtpFileExists(source))
            {
                throw (new FileNotFoundException("File " + source + " not found"));
            }

            //build target name, ensure it does not exist
            string target = GetFullPath(newName);
            if (target == source)
            {
                throw (new ApplicationException("Source and target are the same"));
            }
            else if (FtpFileExists(target))
            {
                throw (new ApplicationException("Target file " + target + " already exists"));
            }

            //perform rename
            string URI = CreateURI(source);
            System.Net.FtpWebRequest ftp = GetRequest(URI);
            //Set request to delete
            ftp.Method = System.Net.WebRequestMethods.Ftp.Rename;
            ftp.RenameTo = target;
            try
            {
                //get response but ignore it
                string str = GetStringResponse(ftp);
            }
            catch (Exception)
            {
                // do not handle error
                throw;
            }
            return true;
        }

        public bool FtpCreateDirectory(string dirpath)
        {
            //perform create
            string URI = CreateURI(dirpath);
            System.Net.FtpWebRequest ftp = GetRequest(URI);
            //Set request to MkDir
            ftp.Method = System.Net.WebRequestMethods.Ftp.MakeDirectory;
            try
            {
                //get response but ignore it
                string str = GetStringResponse(ftp);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool FtpDeleteDirectory(string dirpath)
        {
            //perform remove
            string URI = CreateURI(dirpath);
            System.Net.FtpWebRequest ftp = GetRequest(URI);
            //Set request to RmDir
            ftp.Method = System.Net.WebRequestMethods.Ftp.RemoveDirectory;
            try
            {
                //get response but ignore it
                string str = GetStringResponse(ftp);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Obtain datetimestamp for remote file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public DateTime GetDateTimestamp(FTPfileInfo file)
        {
            DateTime result = this.GetDateTimestamp(file.Filename);
            file.FileDateTime = result;
            return result;
        }

        /// <summary>
        /// Obtain datetimestamp for remote file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public DateTime GetDateTimestamp(string filename)
        {
            string URI = CreateURI(filename);
            FtpWebRequest ftp = GetRequest(URI);
            ftp.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            return this.GetLastModified(ftp);
        }



        #endregion

        #region "private supporting fns"

        /// <summary>
        /// Ensure the data payload for URI is properly encoded
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private string CreateURI(string filename)
        {
            string path;
            if (filename.Contains("/"))
            {
                path = AdjustDir(filename);
            }
            else
            {
                path = this.CurrentDirectory + filename;
            }
            // escape the path
            string escapedPath = GetEscapedPath(path);
            return this.Hostname + escapedPath;
        }

        //Get the basic FtpWebRequest object with the
        //common settings and security
        private FtpWebRequest GetRequest(string URI)
        {
            //create request
            FtpWebRequest result = (FtpWebRequest)FtpWebRequest.Create(URI);
            //Set the login details
            result.Credentials = GetCredentials();
            // support for EnableSSL
            result.EnableSsl = EnableSSL;
            //keep alive? (stateless mode)
            result.KeepAlive = KeepAlive;
            // support for passive connections 
            result.UsePassive = UsePassive;
            // support for proxy settings
            result.Proxy = Proxy;

            return result;
        }

        /// <summary>
        /// Ensure chars in path are correctly escaped e.g. #
        /// </summary>
        /// <param name="path">path to escape</param>
        /// <returns></returns>
        private string GetEscapedPath(string path)
        {
            string[] parts;
            parts = path.Split('/');
            string result;
            result = "";
            foreach (string part in parts)
            {
                if (!string.IsNullOrEmpty(part))
                    result += @"/" + Uri.EscapeDataString(part);
            }
            return result;
        }


        /// <summary>
        /// Get the credentials from username/password
        /// </summary>
        /// <remarks>
        /// Amended to store credentials on first use, for re-use
        /// when using KeepAlive=true
        /// </remarks>
        private System.Net.ICredentials GetCredentials()
        {
            if (_credentials == null)
                _credentials = new System.Net.NetworkCredential(Username, Password);
            return _credentials;
        }

        /// <summary>
        /// stored credentials
        /// </summary>
        private System.Net.NetworkCredential _credentials = null;

        /// <summary>
        /// returns a full path using CurrentDirectory for a relative file reference
        /// </summary>
        private string GetFullPath(string file)
        {
            if (file.Contains("/"))
            {
                return AdjustDir(file);
            }
            else
            {
                return this.CurrentDirectory + file;
            }
        }

        /// <summary>
        /// Amend an FTP path so that it always starts with /
        /// </summary>
        /// <param name="path">Path to adjust</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string AdjustDir(string path)
        {
            return ((path.StartsWith("/")) ? "" : "/").ToString() + path;
        }

        private string GetDirectory(string directory)
        {
            string URI;
            if (directory == "")
            {
                //build from current
                URI = Hostname + this.CurrentDirectory;
                _lastDirectory = this.CurrentDirectory;
            }
            else
            {
                if (!directory.StartsWith("/"))
                {
                    throw (new ApplicationException("Directory should start with /"));
                }
                URI = this.Hostname + directory;
                _lastDirectory = directory;
            }
            return URI;
        }

        //stores last retrieved/set directory
        private string _lastDirectory = "";

        /// <summary>
        /// Obtains a response stream as a string
        /// </summary>
        /// <param name="ftp">current FTP request</param>
        /// <returns>String containing response</returns>
        /// <remarks>
        /// FTP servers typically return strings with CR and
        /// not CRLF. Use respons.Replace(vbCR, vbCRLF) to convert
        /// to an MSDOS string
        /// 1.1: modified to ensure accepts UTF8 encoding
        /// </remarks>
        private string GetStringResponse(FtpWebRequest ftp)
        {
            //Get the result, streaming to a string
            string result = "";
            using (FtpWebResponse response = (FtpWebResponse)ftp.GetResponse())
            {
                long size = response.ContentLength;
                using (Stream datastream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(datastream, System.Text.Encoding.UTF8))
                    {
                        result = sr.ReadToEnd();
                        sr.Close();
                    }

                    datastream.Close();
                }

                response.Close();
            }

            return result;
        }

        /// <summary>
        /// Gets the size of an FTP request
        /// </summary>
        /// <param name="ftp"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private long GetSize(FtpWebRequest ftp)
        {
            long size;
            using (FtpWebResponse response = (FtpWebResponse)ftp.GetResponse())
            {
                size = response.ContentLength;
                response.Close();
            }

            return size;
        }

        /// <summary>
        /// Internal function to get the modified datetime stamp via FTP
        /// </summary>
        /// <param name="ftp">connection to use</param>
        /// <returns>
        /// DateTime of file, or throws exception
        /// </returns>
        private DateTime GetLastModified(FtpWebRequest ftp)
        {
            DateTime lastmodified;
            using (FtpWebResponse response = (FtpWebResponse)ftp.GetResponse())
            {
                lastmodified = response.LastModified;
                response.Close();
            }
            return lastmodified;

        }
        #endregion

        #region Properties

        /// <summary>
        /// Hostname
        /// </summary>
        /// <value></value>
        /// <remarks>Hostname can be in either the full URL format
        /// ftp://ftp.myhost.com or just ftp.myhost.com
        /// </remarks>
        public string Hostname
        {
            get
            {
                if (_hostname.StartsWith("ftp://"))
                {
                    return _hostname;
                }
                else
                {
                    return "ftp://" + _hostname;
                }
            }
            set { _hostname = value; }
        }
        private string _hostname;

        /// <summary>
        /// Username property
        /// </summary>
        /// <value></value>
        /// <remarks>Can be left blank, in which case 'anonymous' is returned</remarks>
        public string Username
        {
            get { return (_username == "" ? "anonymous" : _username); }
            set { _username = value; }
        }
        private string _username;

        /// <summary>
        /// Password for username
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        private string _password;

        /// <summary>
        /// The CurrentDirectory value
        /// </summary>
        /// <remarks>Defaults to the root '/'</remarks>
        public string CurrentDirectory
        {
            get
            {
                //return directory, ensure it ends with /
                return _currentDirectory + ((_currentDirectory.EndsWith("/")) ? "" : "/").ToString();
            }
            set
            {
                if (!value.StartsWith("/"))
                {
                    throw (new ApplicationException("Directory should start with /"));
                }
                _currentDirectory = value;
            }
        }
        private string _currentDirectory = "/";

        /// <summary>
        /// Support for EnableSSL flag on FtpWebRequest class
        /// </summary>
        public bool EnableSSL
        {
            get { return _enableSSL; }
            set { _enableSSL = value; }
        }
        private bool _enableSSL = false;

        /// <summary>
        /// KeepAlive property for permanent connections
        /// </summary>
        /// <remarks>
        /// KeepAlive is set False by default (no permanent connection)
        /// </remarks>
        public bool KeepAlive
        {
            get { return _keepAlive; }
            set { _keepAlive = value; }
        }
        private bool _keepAlive = false;

        /// <summary>
        /// Support for Passive mode
        /// </summary>
        public bool UsePassive
        {
            get { return _usePassive; }
            set { _usePassive = value; }
        }
        private bool _usePassive;

        /// <summary>
        /// Support for Proxy settings
        /// </summary>
        public IWebProxy Proxy
        {
            get { return _proxy; }
            set { _proxy = value; }
        }
        private IWebProxy _proxy = null;

        #endregion

    }
    #endregion

    #region "FTP Directory class"
    /// <summary>
    /// Stores a list of files and directories from an FTP result
    /// </summary>
    /// <remarks></remarks>
    public class FTPdirectory : List<FTPfileInfo>
    {


        public FTPdirectory()
        {
            //creates a blank directory listing
        }

        /// <summary>
        /// Constructor: create list from a (detailed) directory string
        /// </summary>
        /// <param name="dir">directory listing string</param>
        /// <param name="path"></param>
        /// <remarks></remarks>
        public FTPdirectory(string dir, string path)
        {
            foreach (string line in dir.Replace("\n", "").Split(System.Convert.ToChar('\r')))
            {
                //parse
                if (line != "")
                {
                    this.Add(new FTPfileInfo(line, path));
                }
            }
        }

        /// <summary>
        /// Filter out only files from directory listing
        /// </summary>
        /// <param name="ext">optional file extension filter</param>
        /// <returns>FTPdirectory listing</returns>
        public FTPdirectory GetFiles(string ext)
        {
            return this.GetFileOrDir(FTPfileInfo.DirectoryEntryTypes.File, ext);
        }

        /// <summary>
        /// Returns a list of only subdirectories
        /// </summary>
        /// <returns>FTPDirectory list</returns>
        /// <remarks></remarks>
        public FTPdirectory GetDirectories()
        {
            return this.GetFileOrDir(FTPfileInfo.DirectoryEntryTypes.Directory, "");
        }

        //internal: share use function for GetDirectories/Files
        private FTPdirectory GetFileOrDir(FTPfileInfo.DirectoryEntryTypes type, string ext)
        {
            FTPdirectory result = new FTPdirectory();
            foreach (FTPfileInfo fi in this)
            {
                if (fi.FileType == type)
                {
                    if (ext == "")
                    {
                        result.Add(fi);
                    }
                    else if (ext == fi.Extension)
                    {
                        result.Add(fi);
                    }
                }
            }
            return result;

        }

        public bool FileExists(string filename)
        {
            foreach (FTPfileInfo ftpfile in this)
            {
                if (ftpfile.Filename == filename)
                {
                    return true;
                }
            }
            return false;
        }

        private const char slash = '/';

        public static string GetParentDirectory(string dir)
        {
            string tmp = dir.TrimEnd(slash);
            int i = tmp.LastIndexOf(slash);
            if (i > 0)
            {
                return tmp.Substring(0, i - 1);
            }
            else
            {
                throw (new ApplicationException("No parent for root"));
            }
        }
    }
    #endregion
    #region "FTP file info class"
    /// <summary>
    /// Represents a file or directory entry from an FTP listing
    /// </summary>
    /// <remarks>
    /// This class is used to parse the results from a detailed
    /// directory list from FTP. It supports most formats of
    /// 
    /// v1.1 fixed bug in Fullname/path
    /// </remarks>
    public class FTPfileInfo
    {

        //Stores extended info about FTP file

        #region "Properties"
        public string FullName
        {
            get
            {
                return Path + Filename;
            }
        }
        public string Filename
        {
            get
            {
                return _filename;
            }
        }
        /// <summary>
        /// Path of file (always ending in /)
        /// </summary>
        /// <remarks>
        /// 1.1: Modifed to ensure always ends in / - with thanks to jfransella for pointing this out
        /// </remarks>
        public string Path
        {
            get
            {
                return _path + (_path.EndsWith("/") ? "" : "/");
            }
        }
        public DirectoryEntryTypes FileType
        {
            get
            {
                return _fileType;
            }
        }
        public long Size
        {
            get
            {
                return _size;
            }
        }
        public DateTime FileDateTime
        {
            get
            {
                return _fileDateTime;
            }
            internal set
            {
                _fileDateTime = value;
            }

        }
        public string Permission
        {
            get
            {
                return _permission;
            }
        }
        public string Extension
        {
            get
            {
                int i = this.Filename.LastIndexOf(".");
                if (i >= 0 && i < (this.Filename.Length - 1))
                {
                    return this.Filename.Substring(i + 1);
                }
                else
                {
                    return "";
                }
            }
        }
        public string NameOnly
        {
            get
            {
                int i = this.Filename.LastIndexOf(".");
                if (i > 0)
                {
                    return this.Filename.Substring(0, i);
                }
                else
                {
                    return this.Filename;
                }
            }
        }
        private string _filename;
        private string _path;
        private DirectoryEntryTypes _fileType;
        private long _size;
        private DateTime _fileDateTime;
        private string _permission;

        #endregion

        /// <summary>
        /// Identifies entry as either File or Directory
        /// </summary>
        public enum DirectoryEntryTypes
        {
            File,
            Directory
        }

        /// <summary>
        /// Constructor taking a directory listing line and path
        /// </summary>
        /// <param name="line">The line returned from the detailed directory list</param>
        /// <param name="path">Path of the directory</param>
        /// <remarks></remarks>
        public FTPfileInfo(string line, string path)
        {
            //parse line
            Match m = GetMatchingRegex(line);
            if (m == null)
            {
                //failed
                throw (new ApplicationException("Unable to parse line: " + line));
            }
            else
            {
                _filename = m.Groups["name"].Value;
                _path = path;

                Int64.TryParse(m.Groups["size"].Value, out _size);
                //_size = System.Convert.ToInt32(m.Groups["size"].Value);

                _permission = m.Groups["permission"].Value;
                string _dir = m.Groups["dir"].Value;
                if (_dir != "" && _dir != "-")
                {
                    _fileType = DirectoryEntryTypes.Directory;
                }
                else
                {
                    _fileType = DirectoryEntryTypes.File;
                }

                try
                {
                    _fileDateTime = DateTime.Parse(m.Groups["timestamp"].Value);
                }
                catch (Exception)
                {
                    _fileDateTime = Convert.ToDateTime(null);
                }

            }
        }

        private Match GetMatchingRegex(string line)
        {
            Regex rx;
            Match m;
            for (int i = 0; i <= _ParseFormats.Length - 1; i++)
            {
                rx = new Regex(_ParseFormats[i]);
                m = rx.Match(line);
                if (m.Success)
                {
                    return m;
                }
            }
            return null;
        }

        #region "Regular expressions for parsing LIST results"
        /// <summary>
        /// List of REGEX formats for different FTP server listing formats
        /// </summary>
        /// <remarks>
        /// The first three are various UNIX/LINUX formats, fourth is for MS FTP
        /// in detailed mode and the last for MS FTP in 'DOS' mode.
        /// I wish VB.NET had support for Const arrays like C# but there you go
        /// </remarks>
        private static string[] _ParseFormats = new string[] { 
            "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\w+\\s+\\w+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{4})\\s+(?<name>.+)", 
            "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\d+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{4})\\s+(?<name>.+)", 
            "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\d+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{1,2}:\\d{2})\\s+(?<name>.+)", 
            "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\w+\\s+\\w+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{1,2}:\\d{2})\\s+(?<name>.+)", 
            "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})(\\s+)(?<size>(\\d+))(\\s+)(?<ctbit>(\\w+\\s\\w+))(\\s+)(?<size2>(\\d+))\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{2}:\\d{2})\\s+(?<name>.+)", 
            "(?<timestamp>\\d{2}\\-\\d{2}\\-\\d{2}\\s+\\d{2}:\\d{2}[Aa|Pp][mM])\\s+(?<dir>\\<\\w+\\>){0,1}(?<size>\\d+){0,1}\\s+(?<name>.+)" };
        #endregion
    }
    #endregion
}