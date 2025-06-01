using System;
using System.Data;

using System.Linq;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;

using System.Text;

using SubSonic;
using NLog;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using System.Transactions;
using System.Windows.Forms;
using VNS.Properties;
using System.Threading;
using Janus.Windows.GridEX;
using System.Security.Cryptography;
using System.Security;
using System.IO;

namespace VNS.Libs
{
    public class THU_VIEN_CHUNG
    {
        public static string Canhbaotamung(KcbLuotkham objLuotkham)
        {
            try
            {
                DataTable dtTamung = SPs.NoitruTimkiemlichsuNoptientamung(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan, 0, -1, (byte)(objLuotkham.TrangthaiNoitru > 0 ? 1 : 0)).GetDataSet().Tables[0];
                DataSet dsData = SPs.NoitruTongchiphi(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan).GetDataSet();
                decimal Tong_CLS = Utility.DecimaltoDbnull(dsData.Tables[0].Compute("SUM(TT_BN)", "1=1"));
                decimal Tong_Thuoc = Utility.DecimaltoDbnull(dsData.Tables[1].Compute("SUM(TT_BN)", "1=1"));
                decimal Tong_VTTH = Utility.DecimaltoDbnull(dsData.Tables[2].Compute("SUM(TT_BN)", "1=1"));
                decimal Tong_Giuong = Utility.DecimaltoDbnull(dsData.Tables[3].Compute("SUM(TT_BN)", "1=1"));
                decimal Tong_Goi = Utility.DecimaltoDbnull(dsData.Tables[4].Compute("SUM(TT_BN)", "1=1"));
                decimal Tong_Tamung = Utility.DecimaltoDbnull(dtTamung.Compute("SUM(so_tien)", "1=1"));
                decimal Tong_chiphi = Tong_CLS + Tong_Thuoc + Tong_Giuong + Tong_Goi + Tong_VTTH;
                Decimal Gioihancanhbao = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_GIOIHAN_NOPTIENTAMUNG", "0", true), 0);
                if (Tong_Tamung - Tong_chiphi > Gioihancanhbao)//OK
                {
                    return "";
                }
                string s1 = String.Format(Utility.FormatDecimal(), Gioihancanhbao);
                string s2 = String.Format(Utility.FormatDecimal(), String.Format(Utility.FormatDecimal(), Convert.ToDecimal((Tong_Tamung - Tong_chiphi).ToString())));
                string result = string.Format("Giới hạn cảnh báo <={0}. Hiện tại, Tổng tạm ứng - Tổng chi phí = {1}", s1, s2);
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }
        public static bool IsAddressAvailable(string address, int timeout)
        {

            Ping ping = new Ping();
            try
            {
                var pingReply = ping.Send(address, timeout);
                return pingReply != null && pingReply.Status == IPStatus.Success;
            }

            catch
            {
                return false;
            }

        }
        public static int Songay(DateTime from, DateTime to)
        {
            int tinh1Ngayneunhohon24H = Utility.Int32Dbnull(Laygiatrithamsohethong("NOITRU_NHOHON24H_TINH1NGAY", "1", false), 1);
            int sogiotinh = Utility.Int32Dbnull(Laygiatrithamsohethong("NOITRU_SOGIO_LAMTRONNGAY", "1", false), 1);
            double totalhour = Math.Ceiling((to - from).TotalHours);
            if (totalhour < 24)
            {
                if (tinh1Ngayneunhohon24H == 1) return 1;
                else if (totalhour >= sogiotinh) return 1;
                else return 0;
            }
            int songay = (int)totalhour / 24;
            int sogio = (int)totalhour % 24;
            int songaythem = 0;
            if (sogio >= sogiotinh) songaythem = 1;

            return songay + songaythem;
        }
        public static int Songay(DateTime from, DateTime to,bool tinh1Ngayneunhohon24H,int sogiotinhtronngay)
        {
            double totalhour = Math.Ceiling((to - from).TotalHours);
            if (totalhour < 24)
            {
                if (tinh1Ngayneunhohon24H) return 1;
                else if (totalhour >= sogiotinhtronngay) return 1;
                else return 0;
            }
            int songay = (int)totalhour / 24;
            int sogio = (int)totalhour % 24;
            int songaythem = 0;
            if (sogio >= sogiotinhtronngay) songaythem = 1;

            return songay + songaythem;
        }
        public static int SongayChuaRaVien(DateTime from, DateTime to)
        {
            int tinh1Ngayneunhohon24H = Utility.Int32Dbnull(Laygiatrithamsohethong("NOITRU_NHOHON24H_TINH1NGAY", "1", false), 1);
            int sogiotinh = Utility.Int32Dbnull(Laygiatrithamsohethong("NOITRU_SOGIO_LAMTRONNGAY", "1", false), 1);
            double totalhour = Math.Ceiling((to - from).TotalHours);
            if (totalhour < 24)
            {
                if (tinh1Ngayneunhohon24H == 1) return 1;
                else if (totalhour >= sogiotinh) return 1;
                else return 0;
            }
            int songay = (int)totalhour / 24;
            int sogio = (int)totalhour % 24;
            int songaythem = 0;
            if (sogio >= sogiotinh) songaythem = 1;
            return songay + songaythem;
        }
        public static void Logabcd(string functionName, string userName, string logInfo, action action = action.doNothing, string sourceName = "UI")
        {
            if (string.IsNullOrEmpty(functionName) | string.IsNullOrEmpty(functionName) | string.IsNullOrEmpty(functionName))
            {
                Utility.ShowMsg("Thông tin cơ bản của Log không đúng");
                return;
            }

            TLog log = new TLog();
            log.LogSource = sourceName;
            log.LogFunctionName = functionName;
            log.LogActionId = Utility.Int16Dbnull(action);
            log.LogUser = userName;
            log.LogInfo = logInfo;
            log.LogIp = Utility.GetIPAddress();
            log.LogMacAddress = globalVariables.gv_strMacAddress;
            log.LogTime = DateTime.Now;
            log.Save();
        }
        public static string Canhbaotamung(KcbLuotkham objLuotkham, DataSet dsData, DataTable dtTamung)
        {
            try
            {
                decimal Tong_CLS = Utility.DecimaltoDbnull(dsData.Tables[0].Compute("SUM(TT_BN)", "1=1"));
                decimal Tong_Thuoc = Utility.DecimaltoDbnull(dsData.Tables[1].Compute("SUM(TT_BN)", "1=1"));
                decimal Tong_VTTH = Utility.DecimaltoDbnull(dsData.Tables[2].Compute("SUM(TT_BN)", "1=1"));
                decimal Tong_Giuong = Utility.DecimaltoDbnull(dsData.Tables[3].Compute("SUM(TT_BN)", "1=1"));
                decimal Tong_Goi = Utility.DecimaltoDbnull(dsData.Tables[4].Compute("SUM(TT_BN)", "1=1"));
                decimal Tong_Tamung = Utility.DecimaltoDbnull(dtTamung.Compute("SUM(so_tien)", "1=1"));
                decimal Tong_chiphi = Tong_CLS + Tong_Thuoc + Tong_Giuong + Tong_Goi + Tong_VTTH;
                Decimal Gioihancanhbao = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_GIOIHAN_NOPTIENTAMUNG", "0", true), 0);
                if (Tong_Tamung - Tong_chiphi > Gioihancanhbao)//OK
                {
                    return "";
                }
                string sTU = String.Format(Utility.FormatDecimal(), Tong_Tamung);
                string sChiphi = String.Format(Utility.FormatDecimal(), Tong_chiphi);
                if (sTU == "") sTU = "0";
                if (sChiphi == "") sChiphi = "0";
                string s1 = String.Format(Utility.FormatDecimal(), Gioihancanhbao);
                string s2 = String.Format(Utility.FormatDecimal(), String.Format(Utility.FormatDecimal(), Convert.ToDecimal((Tong_Tamung - Tong_chiphi).ToString())));
                string s3 = String.Format(Utility.FormatDecimal(), Gioihancanhbao - (Tong_Tamung - Tong_chiphi));
                string result = "";
                if (Gioihancanhbao > 0)
                    result = string.Format("Giới hạn cảnh báo khi tổng tiền tạm ứng của người bệnh nhỏ hơn hoặc bằng {0}. Hiện tại, Tổng tạm ứng ({1}) - Tổng chi phí ({2}) = {3}. Do vậy, người bệnh cần đóng thêm: {4}", sTU, sChiphi, s1, s2, s3);
                else
                    result = string.Format("Hiện tại, Tổng tạm ứng ({0}) - Tổng chi phí ({1}) = {2}. Do vậy, người bệnh cần đóng thêm: {3}", sTU, sChiphi, s2, s3);
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }
        public static string Laygiatrithamsohethong(string paramName, bool fromDB)
        {
            try
            {
                string reval = null;
                if (fromDB)
                {
                    SqlQuery sqlQuery =
                        new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo(
                            paramName);
                    SysSystemParameter objSystemParameter = sqlQuery.ExecuteSingle<SysSystemParameter>();
                    if (objSystemParameter != null) reval = objSystemParameter.SValue;
                }
                else
                {
                    DataRow[] arrDR = globalVariables.gv_dtSysparams.Select(SysSystemParameter.SNameColumn.ColumnName + " ='" + paramName + "'");
                    if (arrDR.Length > 0) reval = Utility.sDbnull(arrDR[0][SysSystemParameter.SValueColumn.ColumnName]);
                }
                return reval;
            }
            catch
            {
                return null;
            }
        }
        public static SysSystemParameter Laythamsohethong(string ParamName)
        {
            try
            {

                SqlQuery sqlQuery =
                    new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo(
                        ParamName);
                SysSystemParameter objSystemParameter = sqlQuery.ExecuteSingle<SysSystemParameter>();

                return objSystemParameter;
            }
            catch
            {
                return null;
            }
        }
        public static QheDoituongDichvucl LayQheDoituongCLS(string madoituong, int IdChitietdichvu, string makhoathuchien, bool CLS_GIATHEO_KHOAKCB)
        {
            SqlQuery sqlQuery = new Select().From(QheDoituongDichvucl.Schema)
                                       .Where(QheDoituongDichvucl.Columns.IdChitietdichvu).IsEqualTo(
                                           IdChitietdichvu)
                                       .And(QheDoituongDichvucl.Columns.MaDoituongKcb).IsEqualTo(madoituong);
            if (CLS_GIATHEO_KHOAKCB)
                sqlQuery.And(QheDoituongDichvucl.Columns.MaKhoaThuchien).IsEqualTo(makhoathuchien);
            QheDoituongDichvucl objQheDoituongDichvucl = sqlQuery.ExecuteSingle<QheDoituongDichvucl>();
            return objQheDoituongDichvucl;
        }
        public static QheDoituongThuoc LayQheDoituongThuoc(string madoituong, int idthuoc, string makhoathuchien, bool THUOC_GIATHEO_KHOAKCB)
        {
            SqlQuery sqlQuery = new Select().From(QheDoituongThuoc.Schema)
                                       .Where(QheDoituongThuoc.Columns.IdThuoc).IsEqualTo(
                                           idthuoc)
                                       .And(QheDoituongThuoc.Columns.MaDoituongKcb).IsEqualTo(madoituong);
            if (THUOC_GIATHEO_KHOAKCB)
                sqlQuery.And(QheDoituongThuoc.Columns.MaKhoaThuchien).IsEqualTo(makhoathuchien);
            QheDoituongThuoc objQheDoituongThuoc = sqlQuery.ExecuteSingle<QheDoituongThuoc>();
            return objQheDoituongThuoc;
        }
        public static void CapnhatgiatriTieudebaocao(string Matieude, string _value)
        {
            try
            {
                if (Utility.DoTrim(Matieude) == "") return;
                DataRow[] arrDR = globalVariables.gv_dtSysTieude.Select(SysTieude.MaTieudeColumn.ColumnName + " ='" + Matieude + "'");
                SysReport _Item = new Select().From(SysReport.Schema).Where(SysReport.Columns.MaBaocao).IsEqualTo(
                            Matieude).ExecuteSingle<SysReport>();
                if (_Item != null)
                {
                    //arrDR[0][SysTieude.NoiDungColumn.ColumnName] = _value;
                    //globalVariables.gv_dtSysTieude.AcceptChanges();
                    new Update(SysReport.Schema).Set(SysReport.TieuDeColumn).EqualTo(_value).Where(SysReport.MaBaocaoColumn).IsEqualTo(Matieude).Execute();
                }
                else
                {
                    SysReport newItem = new SysReport();
                    newItem.MaBaocao = Matieude;
                    newItem.TieuDe = _value;

                    newItem.Save();
                    //DataRow newrow = globalVariables.gv_dtSysTieude.NewRow();
                    //newrow[SysTieude.MaTieudeColumn.ColumnName] = Matieude;
                    //newrow[SysTieude.NoiDungColumn.ColumnName] = _value;

                    //globalVariables.gv_dtSysTieude.Rows.Add(newrow);
                    //globalVariables.gv_dtSysTieude.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi cập nhật giá trị tiêu đề báo cáo:\n" + ex.Message);
            }
        }
        public static string LaygiatriTieudebaocao(string Matieude, string defaultval, bool fromDB)
        {
            try
            {
                string reval = defaultval;
                if (fromDB)
                {
                    SqlQuery sqlQuery =
                        new Select().From(SysReport.Schema).Where(SysReport.Columns.MaBaocao).IsEqualTo(
                            Matieude);
                    SysReport objSystemParameter = sqlQuery.ExecuteSingle<SysReport>();
                    if (objSystemParameter != null) reval = objSystemParameter.TieuDe;
                }
                else
                {
                    DataRow[] arrDR = globalVariables.gv_dtSysTieude.Select(SysTieude.MaTieudeColumn.ColumnName + " ='" + Matieude + "'");
                    if (arrDR.Length > 0) reval = Utility.sDbnull(arrDR[0][SysTieude.NoiDungColumn.ColumnName]);
                }
                return reval;
            }
            catch
            {
                return defaultval;
            }
        }
        public static string LaygiatriTieudeBaocao(string Matieude, bool fromDB)
        {
            try
            {
                string reval = "";
                if (fromDB)
                {
                    SqlQuery sqlQuery =
                        new Select().From(SysReport.Schema).Where(SysReport.Columns.MaBaocao).IsEqualTo(
                            Matieude);
                    SysReport objSystemParameter = sqlQuery.ExecuteSingle<SysReport>();
                    if (objSystemParameter != null) reval = objSystemParameter.TieuDe;
                }
                else
                {
                    DataRow[] arrDR = globalVariables.gv_dtSysTieude.Select(SysTieude.MaTieudeColumn.ColumnName + " ='" + Matieude + "'");
                    if (arrDR.Length > 0) reval = Utility.sDbnull(arrDR[0][SysTieude.NoiDungColumn.ColumnName]);
                }
                return reval;
            }
            catch
            {
                return "";
            }
        }
        public static void Capnhatgiatrithamsohethong(string paramName, string value)
        {
            try
            {
                if (Utility.DoTrim(paramName) == "") return;
                DataRow[] arrDR = globalVariables.gv_dtSysparams.Select(SysSystemParameter.SNameColumn.ColumnName + " ='" + paramName + "'");
                if (arrDR.Length > 0)
                {
                    arrDR[0][SysSystemParameter.SValueColumn.ColumnName] = value;
                    globalVariables.gv_dtSysparams.AcceptChanges();
                    new Update(SysSystemParameter.Schema).Set(SysSystemParameter.SValueColumn).EqualTo(value).Where(SysSystemParameter.SNameColumn).IsEqualTo(paramName).Execute();
                }
                else
                {
                    SysSystemParameter newItem = new SysSystemParameter();
                    newItem.FpSBranchID = globalVariables.Branch_ID;
                    newItem.SName = paramName;
                    newItem.SValue = value;
                    newItem.IMonth = 0;
                    newItem.IYear = 0;
                    newItem.IStatus = 1;
                    newItem.IsNew = true;
                    newItem.Save();
                    DataRow newrow = globalVariables.gv_dtSysparams.NewRow();
                    newrow[SysSystemParameter.FpSBranchIDColumn.ColumnName] = globalVariables.Branch_ID;
                    newrow[SysSystemParameter.SNameColumn.ColumnName] = paramName;
                    newrow[SysSystemParameter.SValueColumn.ColumnName] = value;
                    newrow[SysSystemParameter.IYearColumn.ColumnName] = 0;
                    newrow[SysSystemParameter.IMonthColumn.ColumnName] = 0;
                    newrow[SysSystemParameter.IStatusColumn.ColumnName] = 1;
                    globalVariables.gv_dtSysparams.Rows.Add(newrow);
                    globalVariables.gv_dtSysparams.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi cập nhật giá trị tham số hệ thống:\n" + ex.Message);
            }
        }
        public static void Capnhatgiatrithamsohethong(string paramName, string value, string sdesc)
        {
            try
            {
                if (Utility.DoTrim(paramName) == "") return;
                DataRow[] arrDR = globalVariables.gv_dtSysparams.Select(SysSystemParameter.SNameColumn.ColumnName + " ='" + paramName + "'");
                if (arrDR.Length > 0)
                {
                    arrDR[0][SysSystemParameter.SValueColumn.ColumnName] = value;
                    arrDR[0][SysSystemParameter.SDescColumn.ColumnName] = sdesc;
                    globalVariables.gv_dtSysparams.AcceptChanges();
                    new Update(SysSystemParameter.Schema).Set(SysSystemParameter.SValueColumn).EqualTo(value).Set(SysSystemParameter.SDescColumn).EqualTo(sdesc).Where(SysSystemParameter.SNameColumn).IsEqualTo(paramName).Execute();
                }
                else
                {
                    SysSystemParameter newItem = new SysSystemParameter();
                    newItem.FpSBranchID = globalVariables.Branch_ID;
                    newItem.SName = paramName;
                    newItem.SDesc = sdesc;
                    newItem.SValue = value;
                    newItem.IMonth = 0;
                    newItem.IYear = 0;
                    newItem.IStatus = 1;
                    newItem.IsNew = true;
                    newItem.Save();
                    DataRow newrow = globalVariables.gv_dtSysparams.NewRow();
                    newrow[SysSystemParameter.FpSBranchIDColumn.ColumnName] = globalVariables.Branch_ID;
                    newrow[SysSystemParameter.SNameColumn.ColumnName] = paramName;
                    newrow[SysSystemParameter.SDescColumn.ColumnName] = sdesc;
                    newrow[SysSystemParameter.SValueColumn.ColumnName] = value;
                    newrow[SysSystemParameter.IYearColumn.ColumnName] = 0;
                    newrow[SysSystemParameter.IMonthColumn.ColumnName] = 0;
                    newrow[SysSystemParameter.IStatusColumn.ColumnName] = 1;
                    globalVariables.gv_dtSysparams.Rows.Add(newrow);
                    globalVariables.gv_dtSysparams.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi cập nhật giá trị tham số hệ thống:\n" + ex.Message);
            }
        }
        public static void EnableBHYT(Form f)
        {
            return;//Mở phần BHYT cho nhanh
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
        public static void UpdateKeTam(long IdPhieuCT, long IdPhieu, string GUID, string PrivateGUID, long idthuockho, int id_thuoc, short Id_Kho, decimal soluong, byte Loai, string motathem)
        {

            TTamke objDTamKe = new TTamke();
            objDTamKe.GuidKey = GUID;
            objDTamKe.PrivateGuid = PrivateGUID;
            objDTamKe.IdKho = Id_Kho;
            objDTamKe.IdThuoc = id_thuoc;
            objDTamKe.IdThuockho = idthuockho;
            objDTamKe.SoLuong = soluong;
            objDTamKe.IdPhieuCtiet = IdPhieuCT;
            objDTamKe.IdPhieu = IdPhieu;
            objDTamKe.Loai = Loai;
            objDTamKe.Noitru = 0;
            objDTamKe.NgayTao = GetSysDateTime();
            objDTamKe.NguoiTao = globalVariables.UserName;
            objDTamKe.TrangThai = false;
            objDTamKe.IpMay = GetIP4Address();
            objDTamKe.MotaThem = motathem;
            try
            {
                var sp1 = SPs.TTamkeInsert(Utility.Int32Dbnull(objDTamKe.Id), Utility.Int32Dbnull(objDTamKe.IdKho),
                    Utility.Int32Dbnull(objDTamKe.IdThuockho), Utility.Int32Dbnull(objDTamKe.IdThuoc),
                    Utility.DoubletoDbnull(objDTamKe.SoLuong), Utility.Int32Dbnull(objDTamKe.IdPhieuCtiet),
                    Utility.Int32Dbnull(objDTamKe.IdPhieu), Utility.Int32Dbnull(objDTamKe.Loai),
                    Utility.sDbnull(objDTamKe.GuidKey), Utility.sDbnull(objDTamKe.PrivateGuid), objDTamKe.NgayTao, objDTamKe.NguoiTao,
                    Utility.Int32Dbnull(objDTamKe.Noitru), Utility.sDbnull(objDTamKe.MaLuotkham),
                    Utility.Int32Dbnull(objDTamKe.IdBenhnhan), Utility.Int32Dbnull(objDTamKe.TrangThai), objDTamKe.IpMay, motathem, new DateTime(2300, 1, 1));
                sp1.Execute();
            }
            catch (Exception ex)
            {
                //objDTamKe.IsNew = true;
                //objDTamKe.Save();
                Utility.ShowMsg("Lỗi trong quá trình lưu tạm kê :" + ex.Message);
            }
        }
        public static int UpdateKeTam(long IdPhieuCT, long IdPhieu, string GUID, string privateGuid, long idthuockho, int id_thuoc, short Id_Kho, decimal soluong, byte Loai, string ma_luotkham, long id_benhnhan, int noitru, DateTime ngaykeDateTime, DateTime ngay_nhaton, string motathem)
        {

            TTamke objDTamKe = new TTamke();
            objDTamKe.GuidKey = GUID;
            objDTamKe.IdKho = Id_Kho;
            objDTamKe.IdThuoc = id_thuoc;
            objDTamKe.IdThuockho = idthuockho;
            objDTamKe.SoLuong = soluong;
            objDTamKe.IdPhieuCtiet = IdPhieuCT;
            objDTamKe.IdPhieu = IdPhieu;
            objDTamKe.Loai = Loai;
            objDTamKe.NgayTao = ngaykeDateTime;
            objDTamKe.NguoiTao = globalVariables.UserName;
            objDTamKe.MaLuotkham = ma_luotkham;
            objDTamKe.IdBenhnhan = id_benhnhan;
            objDTamKe.Noitru = Utility.ByteDbnull(noitru);
            objDTamKe.TrangThai = false;
            objDTamKe.IpMay = GetIP4Address();
            objDTamKe.MotaThem = motathem;
            objDTamKe.PrivateGuid = privateGuid;
            // objDTamKe.Save();
            int banghi = 0;
            try
            {
                var sp1 = SPs.TTamkeInsert(Utility.Int32Dbnull(objDTamKe.Id), Utility.Int32Dbnull(objDTamKe.IdKho),
                    Utility.Int32Dbnull(objDTamKe.IdThuockho), Utility.Int32Dbnull(objDTamKe.IdThuoc),
                    Utility.DoubletoDbnull(objDTamKe.SoLuong), Utility.Int32Dbnull(objDTamKe.IdPhieuCtiet),
                    Utility.Int32Dbnull(objDTamKe.IdPhieu), Utility.Int32Dbnull(objDTamKe.Loai),
                    Utility.sDbnull(objDTamKe.GuidKey), Utility.sDbnull(objDTamKe.PrivateGuid), objDTamKe.NgayTao, objDTamKe.NguoiTao,
                    Utility.Int32Dbnull(objDTamKe.Noitru), Utility.sDbnull(objDTamKe.MaLuotkham),
                    Utility.Int32Dbnull(objDTamKe.IdBenhnhan), Utility.Int32Dbnull(objDTamKe.TrangThai), objDTamKe.IpMay, objDTamKe.MotaThem, new DateTime(2300, 1, 1));
                banghi = sp1.Execute();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi trong quá trình lưu tạm kê :" + ex.Message);
            }
            return banghi;
        }
        public static int UpdateKeTam(long IdPhieuCT, long IdPhieu, string GUID, string privateGuid, long idthuockho, int id_thuoc, short Id_Kho, decimal soluong, byte Loai, string ma_luotkham, long id_benhnhan, int noitru, DateTime ngaykeDateTime, string motathem)
        {

            TTamke objDTamKe = new TTamke();
            objDTamKe.GuidKey = GUID;
            objDTamKe.IdKho = Id_Kho;
            objDTamKe.IdThuoc = id_thuoc;
            objDTamKe.IdThuockho = idthuockho;
            objDTamKe.SoLuong = soluong;
            objDTamKe.IdPhieuCtiet = IdPhieuCT;
            objDTamKe.IdPhieu = IdPhieu;
            objDTamKe.Loai = Loai;
            objDTamKe.NgayTao = ngaykeDateTime;
            objDTamKe.NguoiTao = globalVariables.UserName;
            objDTamKe.MaLuotkham = ma_luotkham;
            objDTamKe.IdBenhnhan = id_benhnhan;
            objDTamKe.Noitru = Utility.ByteDbnull(noitru);
            objDTamKe.TrangThai = false;
            objDTamKe.IpMay = GetIP4Address();
            objDTamKe.MotaThem = motathem;
            objDTamKe.PrivateGuid = privateGuid;
            // objDTamKe.Save();
            int banghi = 0;
            try
            {
                var sp1 = SPs.TTamkeInsert(Utility.Int32Dbnull(objDTamKe.Id), Utility.Int32Dbnull(objDTamKe.IdKho),
                    Utility.Int32Dbnull(objDTamKe.IdThuockho), Utility.Int32Dbnull(objDTamKe.IdThuoc),
                    Utility.DoubletoDbnull(objDTamKe.SoLuong), Utility.Int32Dbnull(objDTamKe.IdPhieuCtiet),
                    Utility.Int32Dbnull(objDTamKe.IdPhieu), Utility.Int32Dbnull(objDTamKe.Loai),
                    Utility.sDbnull(objDTamKe.GuidKey), Utility.sDbnull(objDTamKe.PrivateGuid), objDTamKe.NgayTao, objDTamKe.NguoiTao,
                    Utility.Int32Dbnull(objDTamKe.Noitru), Utility.sDbnull(objDTamKe.MaLuotkham),
                    Utility.Int32Dbnull(objDTamKe.IdBenhnhan), Utility.Int32Dbnull(objDTamKe.TrangThai), objDTamKe.IpMay, objDTamKe.MotaThem, new DateTime(2300, 1, 1));
                banghi = sp1.Execute();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi trong quá trình lưu tạm kê :" + ex.Message);
            }
            return banghi;
        }
        public static int UpdateQtyKeTam(long IdPhieuCT, long IdPhieu, string GUID, string privateGuid, long idthuockho, int id_thuoc, short Id_Kho, decimal soluong, byte Loai, byte action)
        {

            TTamke objDTamKe = new TTamke();
            objDTamKe.GuidKey = GUID;
            objDTamKe.PrivateGuid = privateGuid;
            objDTamKe.IdKho = Id_Kho;
            objDTamKe.IdThuoc = id_thuoc;
            objDTamKe.IdThuockho = idthuockho;
            objDTamKe.SoLuong = soluong;
            objDTamKe.IdPhieuCtiet = IdPhieuCT;
            objDTamKe.IdPhieu = IdPhieu;
            objDTamKe.Loai = Loai;
            objDTamKe.NguoiTao = globalVariables.UserName;
            objDTamKe.TrangThai = false;
            objDTamKe.IpMay = GetIP4Address();
            // objDTamKe.Save();
            int banghi = 0;
            try
            {
                var sp1 = SPs.TTamkeUpdate(Utility.Int32Dbnull(objDTamKe.Id), Utility.Int32Dbnull(objDTamKe.IdKho),
                    Utility.Int64Dbnull(objDTamKe.IdThuockho), Utility.Int32Dbnull(objDTamKe.IdThuoc),
                    Utility.DoubletoDbnull(objDTamKe.SoLuong), Utility.Int64Dbnull(objDTamKe.IdPhieuCtiet),
                    Utility.Int64Dbnull(objDTamKe.IdPhieu), Utility.Int32Dbnull(objDTamKe.Loai),
                    Utility.sDbnull(objDTamKe.GuidKey), Utility.sDbnull(objDTamKe.PrivateGuid), action);
                banghi = sp1.Execute();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi trong quá trình lưu tạm kê :" + ex.Message);
            }
            return banghi;
        }
        public static void XoaKeTam(string GUID)
        {
            try
            {
                var sp = SPs.TTamkeDelete(GUID, -1, -1, 0, 0);
                sp.Execute();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi trong quá trình Xóa tạm kê :" + exception.Message);
            }
        }
        public static void XoaKeTam(string GUID, int loai)
        {
            try
            {
                var sp = SPs.TTamkeDelete(GUID, -1, -1, 0, loai);
                sp.Execute();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi trong quá trình Xóa tạm kê :" + exception.Message);
            }
        }
        public static void XoaKeTam(int PhieuCT, int loai)
        {
            try
            {
                var sp = SPs.TTamkeDelete(string.Empty, PhieuCT, -1, loai, 1);
                sp.Execute();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi trong quá trình Xóa tạm kê :" + exception.Message);
            }
        }
        public static string Laygiatrithamsohethong_off(string paramName, string defaultval, bool fromDb)
        {
            try
            {
                //fromDB = true;
                string reval = defaultval;
                if (fromDb)
                {
                    SqlQuery sqlQuery =
                        new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo(
                            paramName);
                    SysSystemParameter objSystemParameter = sqlQuery.ExecuteSingle<SysSystemParameter>();
                    if (objSystemParameter != null) reval = objSystemParameter.SValue;
                }
                else
                {
                    DataRow[] arrDR = globalVariables.gv_dtSysparams.Select(SysSystemParameter.SNameColumn.ColumnName + " ='" + paramName + "'");
                    if (arrDR.Length > 0) reval = Utility.sDbnull(arrDR[0][SysSystemParameter.SValueColumn.ColumnName]);
                }
                return reval;
            }
            catch
            {
                return defaultval;
            }
        }
        public static string Laygiatrithamsohethong(string paramName, string defaultval, bool fromDb)
        {
            try
            {
                //  fromDB = true;
                string reval = defaultval;
                if (fromDb)
                {
                    SqlQuery sqlQuery =
                        new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo(
                            paramName);
                    SysSystemParameter objSystemParameter = sqlQuery.ExecuteSingle<SysSystemParameter>();
                    if (objSystemParameter != null) reval = objSystemParameter.SValue;
                }
                else
                {
                    DataRow[] arrDr = globalVariables.gv_dtSysparams.Select(SysSystemParameter.SNameColumn.ColumnName + " ='" + paramName + "'");
                    if (arrDr.Length > 0) reval = Utility.sDbnull(arrDr[0][SysSystemParameter.SValueColumn.ColumnName]);
                }
                return reval;
            }
            catch
            {
                return defaultval;
            }
        }
        public static string GetGUID()
        {
            string GUID = Guid.NewGuid().ToString();
            return GUID;
        }
        public static DataTable LayDulieuDanhmucChung(List<string> lstLoai, bool fromDB)
        {
            try
            {
                DataTable m_NN = new DataTable();
                if (fromDB)
                {
                    m_NN =
                        new Select().From(DmucChung.Schema)
                            .Where(DmucChung.Columns.Loai).In(lstLoai)
                            .OrderAsc(DmucChung.Columns.SttHthi)
                            .ExecuteDataSet().Tables[0];
                }
                else
                {

                    var q = from p in globalVariables.gv_dtDmucChung.AsEnumerable()
                            where lstLoai.Contains(p.Field<string>(DmucChung.Columns.Loai))
                            select p;
                    if (q.Count() <= 0)
                        m_NN =
                        new Select().From(DmucChung.Schema)
                            .Where(DmucChung.Columns.Loai).In("-1")
                            .ExecuteDataSet().Tables[0];
                    else
                        m_NN = q.CopyToDataTable();
                }
                return m_NN;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static string Laygiatrimacdinh(DataTable dtData)
        {
            try
            {
                if (!dtData.Columns.Contains("TRANGTHAI_MACDINH") || !dtData.Columns.Contains("MA"))
                    return "";
                var q = from p in dtData.AsEnumerable()
                        where Utility.Int32Dbnull(p["TRANGTHAI_MACDINH"], 0) == 1
                        select p;
                if (q.Any())
                    return Utility.sDbnull(q.FirstOrDefault()["MA"], "");
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static DataTable LaydanhsachDoituongKcb()
        {
            try
            {
                DataTable dtData = new DataTable();

                dtData =
                        new Select().From(DmucDoituongkcb.Schema)
                            .OrderAsc(DmucDoituongkcb.Columns.SttHthi)
                            .ExecuteDataSet().Tables[0];

                return dtData;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static DataTable LaydanhsachDoituongKcb(List<string> lstDoituong)
        {
            try
            {
                DataTable dtData = new DataTable();

                dtData =
                        new Select().From(DmucDoituongkcb.Schema)
                        .Where(DmucDoituongkcb.Columns.MaDoituongKcb).In(lstDoituong)
                            .OrderAsc(DmucDoituongkcb.Columns.SttHthi)
                            .ExecuteDataSet().Tables[0];

                return dtData;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static DataTable LaydanhsachBacsi(int departmentID, int noitru)
        {
            try
            {
                DataTable dtData = new DataTable();

                dtData = SPs.DmucLaydanhsachBacsi(departmentID, noitru).GetDataSet().Tables[0];
                return dtData;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public static DataTable LaydanhsachThunganvien()
        {
            try
            {
                DataTable dtData = new DataTable();

                dtData = SPs.DmucLaydanhsachTnv().GetDataSet().Tables[0];
                return dtData;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static DataTable Laydanhsachnhanvien(string ma_loainv)
        {
            try
            {
                DataTable dtData = new DataTable();

                dtData = SPs.DmucLaydanhsachNhanvien(ma_loainv).GetDataSet().Tables[0];
                return dtData;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static DataTable LayThongTinDichVuCLS()
        {
            try
            {
                DataTable dtData = new DataTable();

                dtData =
                        new Select().From(DmucDichvucl.Schema)
                            .OrderAsc(DmucDichvucl.Columns.SttHthi)
                            .ExecuteDataSet().Tables[0];

                return dtData;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static DataTable LayThongTinDichVuCLS(string nhomchidinh)
        {
            try
            {

                return SPs.DmucLaydanhmucDichvucls(nhomchidinh).GetDataSet().Tables[0];

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public static DmucChung LaydoituongDmucChung(string Loai, string MA)
        {
            try
            {

                return new Select().From(DmucChung.Schema)
                    .Where(DmucChung.Columns.Loai).IsEqualTo(Loai)
                    .And(DmucChung.Columns.Ma).IsEqualTo(MA)
                    .ExecuteSingle<DmucChung>();

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static DataTable LayDulieuDanhmucChung(DataTable dtData, string Loaidmuc)
        {
            DataTable dtTemp = dtData.Clone();
            try
            {
                DataRow[] arrDr = dtData.Select(string.Format("{0}='{1}'", DmucChung.Columns.Loai, Loaidmuc));
                if (arrDr.Length > 0)
                    dtTemp = arrDr.CopyToDataTable();
                return dtTemp;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return dtTemp;
            }
        }
        public static DataTable LayDulieuDanhmucChung(string Loaidmuc, bool fromDB)
        {
            try
            {
                DataTable m_NN = new DataTable();
                if (fromDB)
                {
                    m_NN =
                        new Select(DmucChung.Schema.Name + ".*", "concat(ma,':',ten) as ma_ten").From(DmucChung.Schema)
                            .Where(DmucChung.Columns.Loai).IsEqualTo(Loaidmuc)
                            .OrderAsc(DmucChung.Columns.SttHthi)
                            .ExecuteDataSet().Tables[0];
                }
                else
                {

                    var q = from p in globalVariables.gv_dtDmucChung.AsEnumerable()
                            where p.Field<string>(DmucChung.Columns.Loai) == Loaidmuc
                            select p;
                    if (q.Count() <= 0)
                        m_NN =
                        new Select(DmucChung.Schema.Name + ".*", "concat(ma,':',ten) as ma_ten").From(DmucChung.Schema)
                            .Where(DmucChung.Columns.Loai).In("-1")
                            .ExecuteDataSet().Tables[0];
                    else
                        m_NN = q.CopyToDataTable();
                }
                return m_NN;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static string SinhMaBenhAn(string loaibenhan)
        {
            string MaxMaBenhAN = "";
            StoredProcedure sp = SPs.KcbSinhmaBenhanBacninh(loaibenhan, MaxMaBenhAN);
            sp.Execute();
            sp.OutputValues.ForEach(delegate(object objOutput)
            {
                MaxMaBenhAN = (String)objOutput;
            });
            return MaxMaBenhAN;
        }
        public static string SinhMaBenhAnSanKhoa(string loaibenhan)
        {
            string MaxMaBenhAN = "";
            StoredProcedure sp = SPs.KcbSinhmaBenhanSankhoa(loaibenhan, MaxMaBenhAN);
            sp.Execute();
            sp.OutputValues.ForEach(delegate(object objOutput)
            {
                MaxMaBenhAN = (String)objOutput;
            });
            return MaxMaBenhAN;
        }
        public static DataTable Laydanhsachcacphongthamkham(string ma_khoa_thien, string ma_phong_thien)
        {
            return SPs.KcbThamkhamLaydanhsachcacphongkham(ma_khoa_thien, ma_phong_thien).GetDataSet().Tables[0];
        }
        public static DataTable LaydanhsachKhoanoitruTheoBacsi(string username, byte isAdmin, byte noitru, string kieukhoaphong)
        {
            DataTable dtData = SPs.DmucLaydanhsachCackhoaKCBtheoBacsi(username, isAdmin, noitru, kieukhoaphong, globalVariables.Ma_Coso).GetDataSet().Tables[0];
            DataTable dtRevalue = dtData.Clone();
            //NOITRU_NAPKHOANOITRU_THEOKHOADANGNHAP: 1= Khi bác sĩ liên khoa đăng nhập thì tại các chức năng phần nội trú sẽ chỉ hiển thị duy nhất khoa là khoa đăng nhập
            //0= Các chức năng nội trú load tất cả các liên khoa của BS để bác sĩ tự chọn và tìm kiếm BN
            if (!Utility.Byte2Bool(isAdmin) && THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_NAPKHOANOITRU_THEOKHOADANGNHAP", "0", true) == "1")
            {
                DataRow[] arrDr = dtData.Select(DmucKhoaphong.Columns.IdKhoaphong + "=" + globalVariables.idKhoatheoMay.ToString());
                if (arrDr.Length > 0)
                    dtRevalue = arrDr.CopyToDataTable();
            }
            else
                dtRevalue = dtData.Copy();
            return dtRevalue;
        }
        public static DataTable LaydanhsachKhoanoitruTheoBacsi(string username, byte isAdmin, byte noitru)
        {
            DataTable dtData = SPs.DmucLaydanhsachCackhoaKCBtheoBacsi(username, isAdmin, noitru, "KHOA", globalVariables.Ma_Coso).GetDataSet().Tables[0];
            DataTable dtRevalue = dtData.Clone();
            //NOITRU_NAPKHOANOITRU_THEOKHOADANGNHAP: 1= Khi bác sĩ liên khoa đăng nhập thì tại các chức năng phần nội trú sẽ chỉ hiển thị duy nhất khoa là khoa đăng nhập
            //0= Các chức năng nội trú load tất cả các liên khoa của BS để bác sĩ tự chọn và tìm kiếm BN
            if (!Utility.Byte2Bool(isAdmin) && THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_NAPKHOANOITRU_THEOKHOADANGNHAP", "0", true) == "1")
            {
                DataRow[] arrDr = dtData.Select(DmucKhoaphong.Columns.IdKhoaphong + "=" + globalVariables.idKhoatheoMay.ToString());
                if (arrDr.Length > 0)
                    dtRevalue = arrDr.CopyToDataTable();
            }
            else
                dtRevalue = dtData.Copy();
            return dtRevalue;
        }
        public static DataTable LaydanhsachKhoatheoUser(string username, byte isAdmin, byte? Noitru)
        {
            DataTable dtData = SPs.DmucLaydanhsachCackhoaKCBtheoBacsi(username, isAdmin, Noitru, "KHOA", globalVariables.Ma_Coso).GetDataSet().Tables[0];
            DataTable dtRevalue = dtData.Clone();
            return dtData;
        }
        public static DataTable LaydanhsachKhoaKhidangnhap(string username, byte isAdmin)
        {
            DataTable dtData = SPs.DmucLaydanhsachCackhoaKCBtheoBacsi(username, isAdmin, (byte)2, "KHOA", globalVariables.Ma_Coso).GetDataSet().Tables[0];
            DataTable dtRevalue = dtData.Clone();
            return dtData;
        }
        public static DataTable DmucLaydanhsachCacphongkhamTheoBacsi(string UserName, short? Idkhoa, byte? IsAdmin, byte? Noitru)
        {
            return SPs.DmucLaydanhsachCacphongkhamTheoBacsi(UserName, Idkhoa, IsAdmin, Noitru).GetDataSet().Tables[0];
        }
        public static DateTime GetSysDateTime()
        {
            try
            {
                DataTable dataTable = new DataTable();
                DateTime dt = new DateTime();
                dt = DateTime.Now;
                DateTime dateTime = new SubSonic.InlineQuery().ExecuteScalar<DateTime>("select getdate()");

                return dateTime;
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public static int LayIDPhongbanTheoUser(string sUserName)
        {
            int vDepartment_Id = -1;
            try
            {
                DmucNhanvien objStaff = new DmucNhanvien();
                DataRow[] arrDr = globalVariables.gv_dtDmucNhanvien.Select("UserName='" + sUserName + "'");
                if (arrDr.Length > 0)
                {
                    vDepartment_Id = Utility.Int32Dbnull(arrDr[0]["Id_Khoa"], -1);
                    globalVariables.gv_intIDNhanvien = Utility.Int16Dbnull(arrDr[0]["Id_Nhanvien"], -1);
                    globalVariables.gv_strTenNhanvien = Utility.sDbnull(arrDr[0]["Ten_Nhanvien"]);
                    globalVariables.gv_intPhongNhanvien = Utility.Int16Dbnull(arrDr[0]["Id_Phong"]);
                }
                //SqlQuery sqlQuery = new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.Columns.UserName).IsEqualTo(sUserName);
                //DmucNhanvien objStaff = sqlQuery.ExecuteSingle<DmucNhanvien>();
                //if (objStaff != null)
                //{
                //    vDepartment_Id =Utility.Int32Dbnull( objStaff.IdKhoa,-1);
                //    globalVariables.gv_intIDNhanvien = objStaff.IdNhanvien;
                //    globalVariables.gv_strTenNhanvien = Utility.sDbnull(objStaff.TenNhanvien);
                //    globalVariables.gv_intIDNhanvien = Utility.Int16Dbnull(objStaff.IdNhanvien);
                //    globalVariables.gv_intPhongNhanvien = Utility.Int16Dbnull(objStaff.IdPhong);
                //}
            }
            catch (Exception ex)
            {
                vDepartment_Id = -1;

            }



            return vDepartment_Id;
        }
        /// <summary>
        /// Bỏ ref chỗ datatable
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="BHYT"></param>
        public static void SapxepthutuinPhieucongkhai(DataTable dataTable, bool BHYT)
        {
            Utility.AddColumToDataTable(ref dataTable, "stt_in", typeof(int));
            List<DmucChung> lst =
                new Select().From(DmucChung.Schema)
                    .Where(DmucChung.Columns.Loai)
                    .IsEqualTo(BHYT ? THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_STT_INPHOI", false) : THU_VIEN_CHUNG.Laygiatrithamsohethong("DICHVU_STT_IN", false))
                    .ExecuteAsCollection<DmucChungCollection>().ToList<DmucChung>();

            foreach (DataRow dr in dataTable.Rows)
            {
                List<DmucChung> objDmucChung = new List<DmucChung>();
                if (Utility.sDbnull(dr["id_loaithanhtoan"]) != "2")
                {
                    if (Utility.sDbnull(dr["id_loaidichvu"]) == "VTTH")
                        objDmucChung = (from p in lst
                                        where p.MotaThem == "VTTH"
                                        // where p.VietTat == "VTTH"//Bỏ 30/01/2024 do tài liệu ghi đối với VTTH thì cột mô tả thêm ghi VTTH, các loại khác Mô tả thêm= id loại thanh toán
                                        // Utility.sDbnull(dr["id_loaithanhtoan"])
                                        select p).ToList<DmucChung>();
                    else
                    {
                        objDmucChung = (from p in lst
                                        where p.VietTat == Utility.sDbnull(dr["id_loaithanhtoan"])
                                        select p).ToList<DmucChung>();
                    }

                }
                else//Tách theo nhóm in phơi BHYT(nhóm này chọn lúc tạo các dịch vụ chi tiết)
                {
                    objDmucChung = (from p in lst
                                    where p.VietTat == Utility.sDbnull(dr["id_loaithanhtoan"])
                                    && p.Ma == Utility.sDbnull(dr["nhom_inphoiBHYT"])
                                    select p).ToList<DmucChung>(); ;
                }
                if (objDmucChung != null && objDmucChung.Any())
                {
                    dr["stt_in"] = Utility.Int32Dbnull(objDmucChung.FirstOrDefault().SttHthi);
                    dr["id_loaithanhtoan"] = Utility.sDbnull(objDmucChung.FirstOrDefault().VietTat);
                    dr["ten_loaithanhtoan"] = Utility.sDbnull(objDmucChung.FirstOrDefault().Ten);

                }
                if (Utility.Int32Dbnull(dr["stt_in"], -1) <= 0) dr["stt_in"] = 100;
            }
            //Reset lại số thứ tự in để không bị ngắt quãng khi chỉ định không full
            //int max = dataTable.AsEnumerable().Select(c => c.Field<int>("stt_in")).Max();
            //for (int i = 1; i <= max; i++)
            //{
            //    if (dataTable.Select("stt_in=" + i).Length > 0) continue;
            //    var q = from p in dataTable.AsEnumerable()
            //            where Utility.Int32Dbnull(p["stt_in"]) > i
            //            select p;
            //    if (q.Any())
            //    {
            //        int min = q.Select(c => c.Field<int>("stt_in")).Min();
            //        if (min != i)
            //        {
            //            DataRow[] arrDr = dataTable.Select("stt_in=" + min);
            //            foreach (DataRow dr in arrDr)
            //                dr["stt_in"] = i;
            //        }
            //    }
            //}
            ////Reset lại số thứ tự loại(nhóm) dịch vụ CLS: XN,CDHA,TDCN để không bị ngắt quãng khi chỉ định không full
            //max = dataTable.AsEnumerable().Select(c => c.Field<int>("stt_hthi_loaidichvu")).Max();
            //for (int i = 1; i <= max; i++)
            //{
            //    if (dataTable.Select("stt_hthi_loaidichvu=" + i).Length > 0) continue;
            //    var q = from p in dataTable.AsEnumerable()
            //            where Utility.Int32Dbnull(p["stt_hthi_loaidichvu"]) > i
            //            select p;
            //    if (q.Any())
            //    {
            //        int min = q.Select(c => c.Field<int>("stt_hthi_loaidichvu")).Min();
            //        if (min != i)
            //        {
            //            DataRow[] arrDr = dataTable.Select("stt_hthi_loaidichvu=" + min);
            //            foreach (DataRow dr in arrDr)
            //                dr["stt_hthi_loaidichvu"] = i;
            //        }
            //    }
            //}
            foreach (DataRow dr in dataTable.Rows)
            {
                dr["ten_loaithanhtoan"] = dr["stt_in"] + ". " + dr["ten_loaithanhtoan"];
                if (dr["id_loaithanhtoan"].ToString() == "3" || Utility.sDbnull(dr["id_loaidichvu"]) == "VTTH")
                {
                    dr["ten_loaidichvu"] = "";
                    //dr["ten_loaidichvu"] = dr["stt_in"] + ".1 Trong danh mục BHYT ";
                }
                else
                    dr["ten_loaidichvu"] = string.Format("{0}.{1} {2}", dr["stt_in"].ToString(), dr["stt_hthi_loaidichvu"].ToString(), dr["ten_loaidichvu"].ToString());
            }
            dataTable.AcceptChanges();
        }
        public static void Sapxepthutuin(ref DataTable dataTable, bool BHYT)
        {
            Utility.AddColumToDataTable(ref dataTable, "stt_in", typeof(int));
            List<DmucChung> lst =
                new Select().From(DmucChung.Schema)
                    .Where(DmucChung.Columns.Loai)
                    .IsEqualTo(BHYT ? THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_STT_INPHOI", false) : THU_VIEN_CHUNG.Laygiatrithamsohethong("DICHVU_STT_IN", false))
                    .ExecuteAsCollection<DmucChungCollection>().ToList<DmucChung>();

            foreach (DataRow dr in dataTable.Rows)
            {
                List<DmucChung> objDmucChung = new List<DmucChung>();
                if (Utility.sDbnull(dr["id_loaithanhtoan"]) != "2")
                {
                    if (Utility.sDbnull(dr["id_loaidichvu"]) == "VTTH")
                        objDmucChung = (from p in lst
                                        where p.MotaThem == "VTTH"
                                        // Utility.sDbnull(dr["id_loaithanhtoan"])
                                        select p).ToList<DmucChung>();
                    else
                    {
                        objDmucChung = (from p in lst
                                        where p.VietTat == Utility.sDbnull(dr["id_loaithanhtoan"])
                                        select p).ToList<DmucChung>();
                    }

                }
                else//Tách theo nhóm in phơi BHYT
                {
                    objDmucChung = (from p in lst
                                    where p.VietTat == Utility.sDbnull(dr["id_loaithanhtoan"])
                                    && p.Ma == Utility.sDbnull(dr["nhom_inphoiBHYT"])
                                    select p).ToList<DmucChung>(); ;
                }
                if (objDmucChung != null && objDmucChung.Any())
                {
                    dr["stt_in"] = Utility.Int32Dbnull(objDmucChung.FirstOrDefault().SttHthi);
                    dr["id_loaithanhtoan"] = Utility.sDbnull(objDmucChung.FirstOrDefault().VietTat);
                    dr["ten_loaithanhtoan"] = Utility.sDbnull(objDmucChung.FirstOrDefault().Ten);

                }
                if (Utility.Int32Dbnull(dr["stt_in"], -1) <= 0) dr["stt_in"] = 100;
            }
            //Reset lại số thứ tự in để không bị ngắt quãng khi chỉ định không full
            //int max = dataTable.AsEnumerable().Select(c => c.Field<int>("stt_in")).Max();
            //for (int i = 1; i <= max; i++)
            //{
            //    if (dataTable.Select("stt_in=" + i).Length > 0) continue;
            //    var q = from p in dataTable.AsEnumerable()
            //            where Utility.Int32Dbnull(p["stt_in"]) > i
            //            select p;
            //    if (q.Any())
            //    {
            //        int min = q.Select(c => c.Field<int>("stt_in")).Min();
            //        if (min != i)
            //        {
            //            DataRow[] arrDr = dataTable.Select("stt_in=" + min);
            //            foreach (DataRow dr in arrDr)
            //                dr["stt_in"] = i;
            //        }
            //    }
            //}
            ////Reset lại số thứ tự loại(nhóm) dịch vụ CLS: XN,CDHA,TDCN để không bị ngắt quãng khi chỉ định không full
            //max = dataTable.AsEnumerable().Select(c => c.Field<int>("stt_hthi_loaidichvu")).Max();
            //for (int i = 1; i <= max; i++)
            //{
            //    if (dataTable.Select("stt_hthi_loaidichvu=" + i).Length > 0) continue;
            //    var q = from p in dataTable.AsEnumerable()
            //            where Utility.Int32Dbnull(p["stt_hthi_loaidichvu"]) > i
            //            select p;
            //    if (q.Any())
            //    {
            //        int min = q.Select(c => c.Field<int>("stt_hthi_loaidichvu")).Min();
            //        if (min != i)
            //        {
            //            DataRow[] arrDr = dataTable.Select("stt_hthi_loaidichvu=" + min);
            //            foreach (DataRow dr in arrDr)
            //                dr["stt_hthi_loaidichvu"] = i;
            //        }
            //    }
            //}
            foreach (DataRow dr in dataTable.Rows)
            {
                dr["ten_loaithanhtoan"] = dr["stt_in"] + ". " + dr["ten_loaithanhtoan"];
                if (dr["id_loaithanhtoan"].ToString() == "3" || Utility.sDbnull(dr["id_loaidichvu"]) == "VTTH")
                {
                    dr["ten_loaidichvu"] = "";
                    //dr["ten_loaidichvu"] = dr["stt_in"] + ".1 Trong danh mục BHYT ";
                }
                else
                    dr["ten_loaidichvu"] = string.Format("{0}.{1} {2}", dr["stt_in"].ToString(), dr["stt_hthi_loaidichvu"].ToString(), dr["ten_loaidichvu"].ToString());
            }
            dataTable.AcceptChanges();
        }
        public static void NoiTru_Sapxepthutuin(ref DataTable dataTable, bool BHYT)
        {
            Utility.AddColumToDataTable(ref dataTable, "stt_in", typeof(int));
            List<DmucChung> lst =
                new Select().From(DmucChung.Schema)
                    .Where(DmucChung.Columns.Loai)
                    .IsEqualTo(BHYT ? THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_BHYT_STT_INPHOI", false) : THU_VIEN_CHUNG.Laygiatrithamsohethong("DICHVU_STT_IN", false))
                    .ExecuteAsCollection<DmucChungCollection>().ToList<DmucChung>();

            foreach (DataRow dr in dataTable.Rows)
            {
                List<DmucChung> objDmucChung = new List<DmucChung>();
                if (Utility.sDbnull(dr["id_loaithanhtoan"]) != "2")
                {
                    if (Utility.sDbnull(dr["id_loaidichvu"]) == "VTTH")
                        objDmucChung = (from p in lst
                                        where p.MotaThem == "VTTH"
                                        // Utility.sDbnull(dr["id_loaithanhtoan"])
                                        select p).ToList<DmucChung>();
                    else
                    {
                        objDmucChung = (from p in lst
                                        where p.MotaThem == Utility.sDbnull(dr["id_loaithanhtoan"])
                                        select p).ToList<DmucChung>();
                    }

                }
                else//Tách theo nhóm in phơi BHYT
                {
                    objDmucChung = (from p in lst
                                    where p.MotaThem == Utility.sDbnull(dr["id_loaithanhtoan"])
                                    && p.Ma == Utility.sDbnull(dr["nhom_inphoiBHYT"])
                                    select p).ToList<DmucChung>(); ;
                }
                if (objDmucChung != null && objDmucChung.Any())
                {
                    dr["stt_in"] = Utility.Int32Dbnull(objDmucChung.FirstOrDefault().SttHthi);
                    dr["id_loaithanhtoan"] = Utility.Int32Dbnull(objDmucChung.FirstOrDefault().VietTat);
                    dr["ten_loaithanhtoan"] = Utility.sDbnull(objDmucChung.FirstOrDefault().Ten);

                }
            }
            int max = dataTable.AsEnumerable().Select(c => c.Field<int>("stt_in")).Max();
            for (int i = 1; i <= max; i++)
            {
                if (dataTable.Select("stt_in=" + i).Length > 0) continue;
                var q = from p in dataTable.AsEnumerable()
                        where Utility.Int32Dbnull(p["stt_in"]) > i
                        select p;
                if (q.Any())
                {
                    int min = q.Select(c => c.Field<int>("stt_in")).Min();
                    if (min != i)
                    {
                        DataRow[] arrDr = dataTable.Select("stt_in=" + min);
                        foreach (DataRow dr in arrDr)
                            dr["stt_in"] = i;
                    }
                }
            }
            foreach (DataRow dr in dataTable.Rows)
            {
                dr["ten_loaithanhtoan"] = dr["stt_in"] + ". " + dr["ten_loaithanhtoan"];
                if (dr["id_loaithanhtoan"].ToString() == "3" || Utility.sDbnull(dr["id_loaidichvu"]) == "VTTH")
                {
                    dr["ten_loaidichvu"] = "";
                    //dr["ten_loaidichvu"] = dr["stt_in"] + ".1 Trong danh mục BHYT ";
                }
                else
                    dr["ten_loaidichvu"] = string.Format("{0}.{1} {2}", dr["stt_in"].ToString(), dr["stt_hthi_loaidichvu"].ToString(), dr["ten_loaidichvu"].ToString());
            }
            dataTable.AcceptChanges();
        }

        public static int LayIdPhongbanTheoMay(string Code)
        {
            int vDepartment_Id = -1;
            try
            {
                DataRow[] arrDr = globalVariables.gv_dtDmucPhongban.Select("Ma_Khoaphong='" + Code + "'");

                //DmucKhoaphong dmucKhoaphong = new Select().From(DmucKhoaphong.Schema).Where(DmucKhoaphong.Columns.MaKhoaphong).IsEqualTo(Code).ExecuteSingle<DmucKhoaphong>();

                if (arrDr.Length > 0)//(dmucKhoaphong != null)
                {
                    return Utility.Int32Dbnull(arrDr[0]["Id_Khoaphong"], -1);// dmucKhoaphong.IdKhoaphong;
                }
                return globalVariables.IdKhoaNhanvien;
            }
            catch (Exception ex)
            {
                return globalVariables.IdKhoaNhanvien;

            }
            return vDepartment_Id;
        }
        public static void LoadThamSoHeThong()
        {
            globalVariables.FORMTITLE = THU_VIEN_CHUNG.Laygiatrithamsohethong("FORMTITLE", false);
            globalVariables.LUONGCOBAN = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_LUONGCOBAN", "83000", false), 83000);
            globalVariables.gv_strNoiDKKCBBD = THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_NOIDANGKY_KCBBD", "016", false);
            globalVariables.gv_strDiadiem = THU_VIEN_CHUNG.Laygiatrithamsohethong("DIA_DIEM", "Hà Nội", false);
            globalVariables.gv_strNoicapBHYT = THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_NOICAP_BHYT", "01", false);

            globalVariables.gv_intChophepchongiathuoc = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("CHONGIATHUOC", "0", false), 0);
            globalVariables.gv_blnApdungChedoDuyetBHYT = THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_TUDONGDUYET", "1", false) == "1";

            globalVariables.gv_GiathuoctheoGiatrongKho = THU_VIEN_CHUNG.Laygiatrithamsohethong("GIATHUOCKHO", "1", false) == "1";
            globalVariables.ChophepNhapkhoLe = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("ChophepNhapkhoLe", "0", false));
            globalVariables.gv_strTuyenBHYT = THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_TUYEN", "TW", false);
            globalVariables.TrongGio = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TRONGGIO", "0:00-23:59", false);
            globalVariables.TrongNgay = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TRONGNGAY", "2,3,4,5,6,7,CN", false);
            globalVariables.gv_intKT_TT_ChuyenCLS_DV = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KT_TT_ChuyenCLS_DV", "0", false), 0);
            globalVariables.gv_strBHYT_MAQUYENLOI_UUTIEN = THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_MAQUYENLOI_UUTIEN", "", false);
            globalVariables.gv_intKT_TT_ChuyenCLS_BHYT = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KT_TT_ChuyenCLS_BHYT", "0", false), 0);
            globalVariables.gv_strICD_BENH_AN_NGOAI_TRU = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_ICD_BENH_AN_NGOAI_TRU", "", false);

            globalVariables.gv_intSO_BENH_AN_BATDAU = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_SO_BENH_AN", "-1", false), -1);
            globalVariables.gv_strMA_BHYT_KT = THU_VIEN_CHUNG.Laygiatrithamsohethong("MA_BHYT_KT", "", false);
            globalVariables.gv_strMaQuyenLoiHuongBHYT100Phantram = THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_MAQUYENLOI_HUONG100PHANTRAM", "1,2", false);
            globalVariables.gv_intCHARACTERCASING = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_CHARACTERCASING", "0", false), 0);
            globalVariables.gv_intKIEMTRAMATHEBHYT = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_KIEMTRAMATHE", "0", false), 0);
            globalVariables.gv_intBHYT_TUDONGCHECKTRAITUYEN = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_TUDONGCHECKTRAITUYEN", "0", false), 0);
            globalVariables.gv_strBOTENDIACHINH = THU_VIEN_CHUNG.Laygiatrithamsohethong("BOTENDIACHINH", "", false);

            if (globalVariablesPrivate.objNhanvien != null)
            {
                globalVariables.gv_strTenNhanvien = globalVariablesPrivate.objNhanvien.TenNhanvien;
                globalVariables.gv_intIDNhanvien = globalVariablesPrivate.objNhanvien.IdNhanvien;
            }
            else
            {
                globalVariables.gv_strTenNhanvien = globalVariables.UserName;
                globalVariables.gv_intIDNhanvien = -1;
            }
            globalVariables.gv_dtQuyenNhanvien = new Select().From(QheNhanvienQuyensudung.Schema).Where(QheNhanvienQuyensudung.Columns.IdNhanvien).IsEqualTo(globalVariables.gv_intIDNhanvien).ExecuteDataSet().Tables[0];
            globalVariables.gv_dtQuyenNhanvien_Dmuc = new Select().From(QheNhanvienDanhmuc.Schema).Where(QheNhanvienDanhmuc.Columns.IdNhanvien).IsEqualTo(globalVariables.gv_intIDNhanvien).ExecuteDataSet().Tables[0];
        }
        /// <summary>
        /// Hàm thực hiện lấy về IpAddress của máy đang login
        /// </summary>
        /// <returns></returns>
        public static string GetIP4Address()
        {
            try
            {
                if (string.IsNullOrEmpty(globalVariables.gv_strIPAddress))
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
                    globalVariables.gv_strIPAddress = IP4Address;
                }


                return globalVariables.gv_strIPAddress;
            }
            catch
            { return "NO-IP"; }
        }
        //<summary>
        //hàm thực hiện việc lấy thông tin của địa chỉ mac cho máy tính
        //</summary>
        //<returns></returns>

        public static string GetMACAddress()
        {
            try
            {
                if (string.IsNullOrEmpty(globalVariables.gv_strMacAddress))
                {
                    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                    String sMacAddress = string.Empty;
                    foreach (NetworkInterface adapter in nics)
                    {
                        if (sMacAddress == String.Empty)// only return MAC Address from first card  
                        {
                            IPInterfaceProperties properties = adapter.GetIPProperties();
                            sMacAddress = adapter.GetPhysicalAddress().ToString();
                            globalVariables.gv_strMacAddress = sMacAddress;
                        }
                    }
                }
                //  Utility.sDbnull()
                return globalVariables.gv_strMacAddress;
            }
            catch
            { return "NO-ADDRESS"; }
        }
        public static DataTable Laydanhsachnhanvienthuockhoa(int id_khoa)
        {
            DataTable dataTable = new DataTable();
            SqlQuery sqlQuery = new Select().From(DmucNhanvien.Schema)
                .Where(DmucNhanvien.Columns.IdKhoa).IsEqualTo(id_khoa);
            dataTable = sqlQuery.ExecuteDataSet().Tables[0];
            return dataTable;

        }
        public static DataTable Laydanhsachphongthuockhoa(int id_khoa, int PhongChucnang)
        {
            DataTable dataTable = new DataTable();
            SqlQuery sqlQuery = new Select().From(VDmucKhoaphong.Schema)
                .Where(VDmucKhoaphong.Columns.MaCha).IsEqualTo(id_khoa);
            if (PhongChucnang > -1)
                sqlQuery.And(VDmucKhoaphong.Columns.PhongChucnang).IsEqualTo(PhongChucnang);
            sqlQuery.And(VDmucKhoaphong.Columns.KieuKhoaphong).IsEqualTo("PHONG");
            sqlQuery.OrderAsc(VDmucKhoaphong.Columns.SttHthi);
            dataTable = sqlQuery.ExecuteDataSet().Tables[0];
            return dataTable;

        }
        public static DataTable Laydanhsachphongthuockhoa(string ma_khoa, int PhongChucnang)
        {
            int id_khoa = -1;
            DataTable dataTable = new DataTable();
            DmucKhoaphong _item = new Select().From(DmucKhoaphong.Schema).Where(DmucKhoaphong.Columns.MaKhoaphong).IsEqualTo(ma_khoa).ExecuteSingle<DmucKhoaphong>();
            if (_item != null) id_khoa = _item.IdKhoaphong;
            SqlQuery sqlQuery = new Select().From(VDmucKhoaphong.Schema)
                .Where(VDmucKhoaphong.Columns.MaCha).IsEqualTo(id_khoa);
            if (PhongChucnang > -1)
                sqlQuery.And(VDmucKhoaphong.Columns.PhongChucnang).IsEqualTo(PhongChucnang);
            sqlQuery.And(VDmucKhoaphong.Columns.KieuKhoaphong).IsEqualTo("PHONG");
            dataTable = sqlQuery.ExecuteDataSet().Tables[0];
            return dataTable;

        }
        public static DataTable NoitruTimkiemKhoaPhongTheokhoa(int id_khoa, byte hoatdongtheotang)
        {
            return SPs.NoitruTimkiemKhoaPhongTheokhoa(id_khoa, hoatdongtheotang).GetDataSet().Tables[0];
        }
        public static DataTable NoitruTimkiembuongTheokhoa(int id_khoa)
        {
            return SPs.NoitruTimkiembuongTheokhoa(id_khoa).GetDataSet().Tables[0];
        }
        public static DataTable NoitruTimkiemgiuongTheobuong(int id_khoa, int id_buong, byte tracuu)
        {
            return SPs.NoitruTimkiemgiuongTheobuong(id_khoa, id_buong, tracuu).GetDataSet().Tables[0];
        }
        public static DataTable Laydanhmuckhoa(string NoitruNgoaitru, string kieu_khoaphong, int PhongChucnang)
        {
            try
            {

                return SPs.DmucLaydanhmuckhoa(NoitruNgoaitru, kieu_khoaphong, PhongChucnang).GetDataSet().Tables[0];
            }
            catch
            {
                return null;
            }
        }
        public static DataTable Laydanhmuckhoa(string NoitruNgoaitru, int PhongChucnang)
        {
            try
            {

                return SPs.DmucLaydanhmuckhoa(NoitruNgoaitru, "KHOA", PhongChucnang).GetDataSet().Tables[0];
            }
            catch
            {
                return null;
            }
        }
        public static DataTable Laydanhmuckhoa(string NoitruNgoaitru, int PhongChucnang, int idkhoa_loaibo)
        {
            try
            {
                SqlQuery sqlQuery = new Select().From(VDmucKhoaphong.Schema).Where(VDmucKhoaphong.Columns.KieuKhoaphong).IsEqualTo("KHOA");
                if (PhongChucnang > -1)
                    sqlQuery.And(VDmucKhoaphong.Columns.PhongChucnang).IsEqualTo(PhongChucnang);
                if (idkhoa_loaibo > -1)
                    sqlQuery.And(VDmucKhoaphong.Columns.IdKhoaphong).IsNotEqualTo(idkhoa_loaibo);
                if (NoitruNgoaitru != "ALL")
                    sqlQuery.And(VDmucKhoaphong.Columns.NoitruNgoaitru).IsEqualTo(NoitruNgoaitru);
                return sqlQuery.ExecuteDataSet().Tables[0];
            }
            catch
            {
                return null;
            }
        }
        public static DataTable Laydanhmuckhoa(int idkhoa)
        {
            try
            {
                SqlQuery sqlQuery = new Select().From(VDmucKhoaphong.Schema).Where(VDmucKhoaphong.Columns.KieuKhoaphong).IsEqualTo("KHOA");
                sqlQuery.And(VDmucKhoaphong.Columns.IdKhoaphong).IsEqualTo(idkhoa);

                return sqlQuery.ExecuteDataSet().Tables[0];
            }
            catch
            {
                return null;
            }
        }
        public static DataTable LaydanhmucPhong(int PhongChucnang, string noitru_ngoaitru,string kieu_khoa_phong)
        {
            try
            {
                SqlQuery sqlQuery 
                    = new Select().From(VDmucKhoaphong.Schema)
                    .Where(VDmucKhoaphong.Columns.KieuKhoaphong).IsEqualTo(kieu_khoa_phong)
                    .And(VDmucKhoaphong.Columns.NoitruNgoaitru).IsEqualTo(noitru_ngoaitru)
                    ;
                if (PhongChucnang > -1)
                    sqlQuery.And(VDmucKhoaphong.Columns.PhongChucnang).IsEqualTo(PhongChucnang);
                return sqlQuery.ExecuteDataSet().Tables[0];
            }
            catch
            {
                return null;
            }
        }
        public static string MaNhapKho(int LoaiPhieu)
        {
            string MaNhapKho = "";
            DataTable dataTable = SPs.ThuocTaomaphieu(LoaiPhieu).GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                MaNhapKho = Utility.sDbnull(dataTable.Rows[0][0]);
            }
            return MaNhapKho;
        }
        public static string MaXuatKhoTuyenHuyen(int LoaiPhieu)
        {
            string Maxuatkhotuyenhuyen = "";
            DataTable dataTable = SPs.ThuocTaomaphieu(LoaiPhieu).GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                Maxuatkhotuyenhuyen = Utility.sDbnull(dataTable.Rows[0][0]);
            }
            return Maxuatkhotuyenhuyen;
        }
        public static string MaTraLaiKho()
        {
            string MaNhapKho = "";
            DataTable dataTable = SPs.TaoMaphieuTraKhoLeVeKhoChan().GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                MaNhapKho = Utility.sDbnull(dataTable.Rows[0][0]);
            }
            return MaNhapKho;
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin mã phiếu xuât kho cho bệnh nhân
        /// </summary>
        /// <returns></returns>
        public static string MaPhieuXuatBN()
        {
            string MaNhapKho = "";
            DataTable dataTable = SPs.TaoMaphieuXuatthuocBN().GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                MaNhapKho = Utility.sDbnull(dataTable.Rows[0][0]);
            }
            return MaNhapKho;
        }
        public static string BottomCondition()
        {

            return string.Format("{0}, Phiếu in lúc : {1} in bởi : {2}", globalVariables.ten_phanmem,
                                 DateTime.Now,
                                 !string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien) ? globalVariables.gv_strTenNhanvien :
                                 globalVariables.UserName);

        }
        public static string TaoTenDonthuoc(string v_ma_luotkham, int v_id_benhnhan)
        {

            string v_Pres_Name = "";
            v_Pres_Name = Utility.sDbnull(SPs.TaoTenDonthuoc(v_ma_luotkham, v_id_benhnhan).ExecuteScalar(), "");
            return v_Pres_Name;
        }
        public static void CreateXML(DataTable dt)
        {
            try
            {
                if (!dt.Columns.Contains("Logo")) dt.Columns.Add(new DataColumn("Logo", typeof(byte[])));
                if (!dt.Columns.Contains("barcode")) dt.Columns.Add(new DataColumn("barcode", typeof(byte[])));
                bool _XML = Laygiatrithamsohethong("XML", "0", false) == "1";
                string _filePath = "newXML.xml";
                if (!_filePath.Contains(@"\")) _filePath = System.Windows.Forms.Application.StartupPath + @"\Xml4Reports\newXML.xml";
                if (_XML)
                {
                    DataTable newDT = dt.Copy();
                    if (newDT.DataSet != null)
                        newDT.DataSet.WriteXml(_filePath, XmlWriteMode.WriteSchema);
                    else
                    {
                        DataSet ds = new DataSet();
                        ds.Tables.Add(newDT);
                        ds.WriteXml(_filePath, XmlWriteMode.WriteSchema);
                    }
                }
                Utility.CreateMergeFields(dt);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        public static void CreateXML(DataSet ds)
        {
            try
            {
                if (!ds.Tables[0].Columns.Contains("Logo")) ds.Tables[0].Columns.Add(new DataColumn("Logo", typeof(byte[])));
                if (!ds.Tables[0].Columns.Contains("barcode")) ds.Tables[0].Columns.Add(new DataColumn("barcode", typeof(byte[])));
                bool _XML = Laygiatrithamsohethong("XML", "0", false) == "1";
                string _filePath = "newXML.xml";
                if (!_filePath.Contains(@"\")) _filePath = System.Windows.Forms.Application.StartupPath + @"\Xml4Reports\newXML.xml";
                if (_XML)
                {

                    ds.WriteXml(_filePath, XmlWriteMode.WriteSchema);
                }
                Utility.CreateMergeFields(ds);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        public static void UpdateMaVienPhi(string maphieu)
        {
            new Update(SysMavienphi.Schema)
                .Set(SysMavienphi.Columns.TrangThai).EqualTo(1)
                .Where(SysMavienphi.Columns.MaPhieu).IsEqualTo(maphieu).Execute();
        }

        public static string SinhmaVienphi(string tiento)
        {
            string mavienphi = string.Empty;
            var sp = SPs.VienPhiSinhMaVienPhi(tiento, mavienphi);
            sp.Execute();
            mavienphi = Utility.sDbnull(sp.OutputValues[0]);
            return mavienphi;
        }
        public static void CreateXML(DataTable dt, string xmlfile)
        {
            try
            {
                if (!dt.Columns.Contains("Logo")) dt.Columns.Add(new DataColumn("Logo", typeof(byte[])));
                if (!dt.Columns.Contains("barcode")) dt.Columns.Add(new DataColumn("barcode", typeof(byte[])));
                dt.TableName = Path.GetFileNameWithoutExtension(xmlfile);
                bool _XML = Laygiatrithamsohethong("XML", "0", false) == "1";
                string _filePath = xmlfile;
                if (!_filePath.ToUpper().Contains(".XML")) _filePath += ".xml";

                if (!_filePath.Contains(@"\")) _filePath = System.Windows.Forms.Application.StartupPath + @"\Xml4Reports\" + _filePath;
                Utility.CreateFolder(_filePath);
                if (_XML)
                {
                    DataTable newDT = dt.Copy();
                    if (newDT.DataSet != null)
                    {
                        newDT.Rows.Clear();
                        newDT.DataSet.WriteXml(_filePath, XmlWriteMode.WriteSchema);
                    }
                    else
                    {
                        DataSet ds = new DataSet();
                        ds.Tables.Add(newDT);
                        newDT.Rows.Clear();
                        ds.WriteXml(_filePath, XmlWriteMode.WriteSchema);
                    }
                }
                Utility.CreateMergeFields(dt);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        public static void CreateXML_NOLOGO(DataTable dt, string xmlfile)
        {
            try
            {
                bool _XML = Laygiatrithamsohethong("XML", "0", false) == "1";
                string _filePath = xmlfile;
                if (!_filePath.ToUpper().Contains(".XML")) _filePath += ".xml";

                if (!_filePath.Contains(@"\")) _filePath = System.Windows.Forms.Application.StartupPath + @"\Xml4Reports\" + _filePath;
                Utility.CreateFolder(_filePath);
                if (_XML)
                {
                    DataTable newDT = dt.Copy();
                    if (newDT.DataSet != null)
                    {
                        newDT.Rows.Clear();
                        newDT.DataSet.WriteXml(_filePath, XmlWriteMode.WriteSchema);
                    }
                    else
                    {
                        DataSet ds = new DataSet();
                        ds.Tables.Add(newDT);
                        newDT.Rows.Clear();
                        ds.WriteXml(_filePath, XmlWriteMode.WriteSchema);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        public static void CreateXML(DataSet ds, string xmlfile)
        {
            try
            {
                if (!ds.Tables[0].Columns.Contains("Logo")) ds.Tables[0].Columns.Add(new DataColumn("Logo", typeof(byte[])));
                if (!ds.Tables[0].Columns.Contains("barcode")) ds.Tables[0].Columns.Add(new DataColumn("barcode", typeof(byte[])));
                bool _XML = Laygiatrithamsohethong("XML", "0", false) == "1";
                string _filePath = xmlfile;
                if (!_filePath.ToUpper().Contains(".XML")) _filePath += ".xml";
                if (!_filePath.Contains(@"\")) _filePath = System.Windows.Forms.Application.StartupPath + @"\Xml4Reports\" + _filePath;
                Utility.CreateFolder(_filePath);
                if (_XML)
                {

                    ds.WriteXml(_filePath, XmlWriteMode.WriteSchema);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        public static void CreateXML(DataSet ds, string xmlfile, bool writeData)
        {
            try
            {
                if (!ds.Tables[0].Columns.Contains("Logo")) ds.Tables[0].Columns.Add(new DataColumn("Logo", typeof(byte[])));
                if (!ds.Tables[0].Columns.Contains("barcode")) ds.Tables[0].Columns.Add(new DataColumn("barcode", typeof(byte[])));
                bool _XML = Laygiatrithamsohethong("XML", "0", false) == "1";
                string _filePath = xmlfile;
                if (!_filePath.ToUpper().Contains(".XML")) _filePath += ".xml";
                if (!_filePath.Contains(@"\")) _filePath = System.Windows.Forms.Application.StartupPath + @"\Xml4Reports\" + _filePath;
                Utility.CreateFolder(_filePath);
                if (_XML)
                {
                    if (writeData)
                        ds.WriteXml(_filePath);
                    else
                        ds.WriteXml(_filePath, XmlWriteMode.WriteSchema);
                }
                Utility.CreateMergeFields(ds);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        public static decimal TinhPtramBhyt(KcbLuotkham ObjPatientExam)
        {
            decimal ptramBhyt = 0;
            //2 biến dưới phục vụ trái tuyến
            decimal ptramBhyt_traituyen = 0;
            decimal ptramBhyt_dauthe = 0;
            try
            {
                int BHYT_LUATTRAITUYEN_2015 = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_LUATTRAITUYEN_2015", "1", false), 1);
                int BHYT_PTRAM_TRAITUYENNOITRU = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "-1", false), -1);
                int BHYT_TRAITUYEN_GIAYBHYT_100 = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_TRAITUYEN_GIAYBHYT_100", "-1", false), -1);
                int BHYT_TRAIQUYEN_MAQUYENLOI_100 = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_TRAIQUYEN_MAQUYENLOI_100", "-1", false), -1);
                int BHYT_GIAYBHYT_PHANTRAM = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_GIAYBHYT_PHANTRAM", "-1", false), -1);

                if (string.IsNullOrEmpty(ObjPatientExam.MatheBhyt)) return ptramBhyt;
                DmucDoituongkcb objObjectType = DmucDoituongkcb.FetchByID(ObjPatientExam.IdDoituongKcb);
                if (!string.IsNullOrEmpty(ObjPatientExam.MaKcbbd))
                {
                    SqlQuery sqlQuery = new Select().From(DmucDoituongbhyt.Schema).Where(DmucDoituongbhyt.Columns.MaDoituongbhyt).IsEqualTo(Utility.sDbnull(ObjPatientExam.MaDoituongBhyt));//MaDoituongBhyt=Mã đầu thẻ
                    DmucDoituongbhyt objDoituongBhyt = sqlQuery.ExecuteSingle<DmucDoituongbhyt>();
                    QheDautheQloiBhyt objQheDautheQloiBhyt = new Select().From(QheDautheQloiBhyt.Schema)
                        .Where(QheDautheQloiBhyt.Columns.MaDoituongbhyt).IsEqualTo(ObjPatientExam.MaDoituongBhyt)
                        .And(QheDautheQloiBhyt.Columns.MaQloi).IsEqualTo(ObjPatientExam.MaQuyenloi).ExecuteSingle<QheDautheQloiBhyt>();
                    if (ObjPatientExam.DungTuyen == 1 || ObjPatientExam.MaLydovaovien == 4)//Đúng tuyến hoặc thông tuyến
                    {
                        if (objObjectType != null)
                        {
                            if (Utility.Byte2Bool(ObjPatientExam.GiayBhyt))
                            {
                                ptramBhyt = BHYT_GIAYBHYT_PHANTRAM;
                                ObjPatientExam.PtramBhyt = BHYT_GIAYBHYT_PHANTRAM;
                                ObjPatientExam.PtramBhytGoc = BHYT_GIAYBHYT_PHANTRAM;// objDoituongBHYT.PhantramBhyt;//%BHYT theo mã đầu thẻ
                            }
                            else if (globalVariables.gv_strMaQuyenLoiHuongBHYT100Phantram.Contains(ObjPatientExam.MaQuyenloi.ToString()))// objPatientExam.MaQuyenloi.ToString() == "1" || objPatientExam.MaQuyenloi.ToString() == "2")
                            {
                                ptramBhyt = 100;
                                ObjPatientExam.PtramBhyt = 100;
                                ObjPatientExam.PtramBhytGoc = 100;//%BHYT theo mã đầu thẻ
                            }
                            else
                            {
                                if (objDoituongBhyt != null)
                                {
                                    if (Utility.Byte2Bool(objDoituongBhyt.LaygiaChung))//Lấy % BHYT không phân biệt mã quyền lợi
                                    {
                                        ptramBhyt = Utility.DecimaltoDbnull(objDoituongBhyt.PhantramBhyt, 0);
                                        ObjPatientExam.PtramBhyt = ptramBhyt;
                                        ObjPatientExam.PtramBhytGoc = ptramBhyt;//%BHYT theo mã đầu thẻ
                                    }
                                    else//Lấy theo Đầu thẻ+ mã quyền lợi
                                    {
                                        if (objQheDautheQloiBhyt != null)
                                        {
                                            ptramBhyt = Utility.DecimaltoDbnull(objQheDautheQloiBhyt.PhantramBhyt, 0);
                                        }
                                        else
                                        {
                                            ptramBhyt = 0;
                                        }
                                        ObjPatientExam.PtramBhyt = ptramBhyt;
                                        ObjPatientExam.PtramBhytGoc = ptramBhyt;//%BHYT theo mã đầu thẻ
                                    }
                                }
                                else//Không tồn tại đối tượng BHYT
                                {
                                    ptramBhyt = 0;
                                    ObjPatientExam.PtramBhyt = 0;
                                    ObjPatientExam.PtramBhytGoc = 0;//%BHYT theo mã đầu thẻ
                                }
                            }
                        }
                        else//Không tìm được đối tượng Kcb
                        {
                            ptramBhyt = 0;
                        }
                    }
                    else//Trái tuyến
                    {
                        #region New
                        if (objQheDautheQloiBhyt != null)
                        {
                            ptramBhyt_dauthe = Utility.DecimaltoDbnull(objQheDautheQloiBhyt.PhantramBhyt, 0);
                        }
                        if (globalVariables.gv_strTuyenBHYT == "3" || globalVariables.gv_strTuyenBHYT == "4")
                        {
                            ptramBhyt_traituyen = 100;

                        }
                        else//Lấy theo mã quyền lợi đầu thẻ
                        {
                            ptramBhyt_traituyen = Utility.DecimaltoDbnull(objObjectType.PhantramTraituyen, 0);
                        }
                        ptramBhyt = ptramBhyt_traituyen * ptramBhyt_dauthe / 100;
                        ObjPatientExam.PtramBhyt = ptramBhyt;//vẫn tính trái tuyến ngoại trú
                        ObjPatientExam.PtramBhytGoc = ptramBhyt_dauthe;//%BHYT theo mã đầu thẻ
                        //Xem xét gán lại % BHYT gốc theo mã đầu thẻ
                        //Utility.DecimaltoDbnull(objQheDautheQloiBhyt.PhantramBhyt, 0);
                        #endregion
                        #region OLD
                        //2 điều kiện dưới có thể sẽ ko dùng nữa
                        if (Utility.Byte2Bool(ObjPatientExam.GiayBhyt))
                        {
                            ptramBhyt = 100;
                            if (BHYT_TRAITUYEN_GIAYBHYT_100 == 1) ObjPatientExam.PtramBhyt = 100;  //Ko quan tam trai tuyen
                            else
                                ObjPatientExam.PtramBhyt = 0;
                            ObjPatientExam.PtramBhytGoc = 100;//%BHYT theo mã đầu thẻ để dùng cho nội trú
                        }
                        if (globalVariables.gv_strMaQuyenLoiHuongBHYT100Phantram.Contains(ObjPatientExam.MaQuyenloi.ToString()))// objPatientExam.MaQuyenloi.ToString() == "1" || objPatientExam.MaQuyenloi.ToString() == "2")
                        {
                            ptramBhyt = 100;
                            if (BHYT_TRAIQUYEN_MAQUYENLOI_100 == 1) ObjPatientExam.PtramBhyt = 100;  //Ko quan tam trai tuyen
                            else
                                ObjPatientExam.PtramBhyt = 0;
                            ObjPatientExam.PtramBhytGoc = 100;//%BHYT theo mã đầu thẻ để dùng cho nội trú
                        }
                        if (objObjectType != null)//100%
                        {
                            if (globalVariables.gv_strTuyenBHYT == "3" || globalVariables.gv_strTuyenBHYT == "4")
                            {
                                ptramBhyt = 100;
                                ObjPatientExam.PtramBhyt = ptramBhyt;//vẫn tính trái tuyến ngoại trú
                                ObjPatientExam.PtramBhytGoc = ptramBhyt;//%BHYT theo mã đầu thẻ
                                //Xem xét gán lại % BHYT gốc theo mã đầu thẻ
                                //Utility.DecimaltoDbnull(objQheDautheQloiBhyt.PhantramBhyt, 0);
                            }
                            if (BHYT_LUATTRAITUYEN_2015 == 0 || BHYT_PTRAM_TRAITUYENNOITRU == -1)//Tính theo % trong danh mục đối tượng hoặc Chưa khai báo trên hệ thống-->Lấy theo đối tượng
                            {
                                ptramBhyt = Utility.DecimaltoDbnull(objObjectType.PhantramTraituyen, 0);
                                ObjPatientExam.PtramBhyt = ptramBhyt;//vẫn tính trái tuyến ngoại trú
                                ObjPatientExam.PtramBhytGoc = ptramBhyt;//%BHYT theo mã đầu thẻ
                            }
                            else//Tính theo đầu thẻ
                            {
                                if (objDoituongBhyt != null)//Có đối tượng
                                {
                                    if (Utility.Byte2Bool(objDoituongBhyt.LaygiaChung))
                                    {
                                        ptramBhyt = Utility.DecimaltoDbnull(objDoituongBhyt.PhantramBhyt, 0);
                                        ObjPatientExam.PtramBhyt = 0;//Ngoại trú
                                        ObjPatientExam.PtramBhytGoc = ptramBhyt;//%BHYT theo mã đầu thẻ
                                    }
                                    else//Lấy theo giá riêng-->Căn cứ đầu thẻ BHYT
                                    {
                                        if (objQheDautheQloiBhyt != null)
                                        {
                                            ptramBhyt = Utility.DecimaltoDbnull(objQheDautheQloiBhyt.PhantramBhyt, 0);
                                            ObjPatientExam.PtramBhyt = 0;
                                            ObjPatientExam.PtramBhytGoc = ptramBhyt;//%BHYT theo mã đầu thẻ
                                        }
                                        else//Tính luôn theo % trái tuyến của đối tượng BHYT
                                        {
                                            ptramBhyt = Utility.DecimaltoDbnull(objObjectType.PhantramTraituyen, 0);
                                            ObjPatientExam.PtramBhyt = 0;
                                            ObjPatientExam.PtramBhytGoc = ptramBhyt;//%BHYT theo mã đầu thẻ
                                        }

                                    }
                                }
                                else//Không tồn tại đối tượng BHYT
                                {
                                    ptramBhyt = 0;
                                    ObjPatientExam.PtramBhyt = 0;
                                    ObjPatientExam.PtramBhytGoc = 0;//%BHYT theo mã đầu thẻ
                                }
                            }

                        }
                        else
                        {
                            ptramBhyt = 0;
                            ObjPatientExam.PtramBhyt = 0;
                            ObjPatientExam.PtramBhytGoc = 0;//%BHYT theo mã đầu thẻ
                        }
                        #endregion

                    }
                }
                else//Các đối tượng khác-->Lấy % BHYT theo danh mục đối tượng
                {
                    if (objObjectType != null)
                        ptramBhyt = Utility.DecimaltoDbnull(objObjectType.PhantramDungtuyen);
                    else ptramBhyt = 0;
                }
            }
            catch (Exception exception)
            {
                ptramBhyt = 0;
                ObjPatientExam.PtramBhyt = 0;
                ObjPatientExam.PtramBhytGoc = 0;//%BHYT theo mã đầu thẻ
            }
            return ptramBhyt;
        }

        public static KcbThanhtoanChitiet[] TinhPhamTramBHYT(KcbThanhtoanChitiet[] arrPaymentDetail, KcbLuotkham objPatientExam, decimal v_BhytChitra)
        {
            string IsDungTuyen = "DT";
            DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(objPatientExam.IdDoituongKcb);
            if (objectType != null)
            {
                switch (objectType.MaDoituongKcb)
                {
                    case "BHYT":
                        if (Utility.Int32Dbnull(objPatientExam.DungTuyen, "0") == 1) IsDungTuyen = "DT";
                        else
                        {
                            IsDungTuyen = "TT";
                        }
                        break;
                    default:
                        IsDungTuyen = "KHAC";
                        break;
                }

            }
            foreach (KcbThanhtoanChitiet objChitietThanhtoan in arrPaymentDetail)
            {
                if (objChitietThanhtoan.TuTuc == 0)//Có thể tính cho BHYT
                {
                    SqlQuery sqlQuery = new Select().From(DmucBhytChitraDacbiet.Schema)
                        .Where(DmucBhytChitraDacbiet.Columns.IdDichvuChitiet).IsEqualTo(objChitietThanhtoan.IdChitietdichvu)
                        .And(DmucBhytChitraDacbiet.Columns.MaLoaithanhtoan).IsEqualTo(objChitietThanhtoan.IdLoaithanhtoan)
                        .And(DmucBhytChitraDacbiet.Columns.DungtuyenTraituyen).IsEqualTo(IsDungTuyen)
                        .And(DmucBhytChitraDacbiet.Columns.MaDoituongKcb).IsEqualTo(objPatientExam.MaDoituongKcb);
                    DmucBhytChitraDacbiet objDetailBhytChitra = sqlQuery.ExecuteSingle<DmucBhytChitraDacbiet>();
                    if (objDetailBhytChitra != null)
                    {
                        objChitietThanhtoan.MaDoituongKcb = Utility.sDbnull(objPatientExam.MaDoituongKcb);
                        objChitietThanhtoan.PtramBhyt = objDetailBhytChitra.TileGiam;
                        objChitietThanhtoan.BhytChitra = TinhBhytChitra(objDetailBhytChitra.TileGiam,
                                                      Utility.DecimaltoDbnull(
                                                          objChitietThanhtoan.DonGia, 0));
                        objChitietThanhtoan.BnhanChitra = TinhBnhanChitra(objDetailBhytChitra.TileGiam,
                                                                 Utility.DecimaltoDbnull(
                                                                     objChitietThanhtoan.DonGia, 0));
                    }
                    else
                    {
                        objChitietThanhtoan.MaDoituongKcb = Utility.sDbnull(objPatientExam.MaDoituongKcb);
                        objChitietThanhtoan.PtramBhyt = v_BhytChitra;
                        objChitietThanhtoan.BhytChitra = TinhBhytChitra(v_BhytChitra,
                                                       Utility.DecimaltoDbnull(
                                                           objChitietThanhtoan.DonGia, 0));
                        objChitietThanhtoan.BnhanChitra = TinhBnhanChitra(v_BhytChitra,
                                                                 Utility.DecimaltoDbnull(
                                                                     objChitietThanhtoan.DonGia, 0));
                    }


                }
                else
                {
                    objChitietThanhtoan.MaDoituongKcb = "DV";
                    objChitietThanhtoan.BhytChitra = 0;
                    objChitietThanhtoan.BnhanChitra = objChitietThanhtoan.DonGia;
                }

            }
            return arrPaymentDetail;
        }

        /// <summary>
        /// hàm thực hiện việc tính phần trăm của bảo hiểm y tế
        /// </summary>
        /// <param name="objPatientExam"></param>
        /// <param name="objRegExam"></param>
        public static void TinhToanKhamPtramBHYT(KcbLuotkham objPatientExam, KcbDangkyKcb objRegExam)
        {
            decimal PTramBHYT = Utility.DecimaltoDbnull(objPatientExam.PtramBhyt, 0);
            objRegExam.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;

            objRegExam.BhytChitra = Utility.DecimaltoDbnull(objRegExam.DonGia) *
                                        Utility.DecimaltoDbnull(PTramBHYT) / 100;

            objRegExam.BnhanChitra = Utility.DecimaltoDbnull(objRegExam.DonGia, 0) -
                                      Utility.DecimaltoDbnull(objRegExam.BhytChitra, 0);

        }
        public static decimal BHYT_TinhlaiPhantramTheoTongchiPhi(KcbLuotkham objLuotkham,decimal TongChiphi)
        {
            try
            {
                decimal PtramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                var sp = SPs.BhytTinhPtramBHYT(objLuotkham.MaDoituongBhyt,
                            Utility.Int32Dbnull(objLuotkham.MaQuyenloi),
                            Utility.Int32Dbnull(objLuotkham.DungTuyen), Utility.Int32Dbnull(objLuotkham.IdDoituongKcb),
                            Utility.Int32Dbnull(objLuotkham.GiayBhyt), Utility.sDbnull(objLuotkham.MadtuongSinhsong),
                            PtramBHYT, TongChiphi,
                            Utility.DecimaltoDbnull(objLuotkham.LuongCoban), objLuotkham.MaLydovaovien);
                sp.Execute();
                PtramBHYT = Utility.DecimaltoDbnull(sp.OutputValues[0], 0);
                return PtramBHYT;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
            }
           
        }
        public static void TinhPhamTramBHYT(KcbLuotkham objLuotkham, DataTable m_dtData, decimal PtramBHYT)
        {
            if (m_dtData != null)
            {
                //Tính cho các chi tiết hiện tại
                foreach (DataRow dr in m_dtData.Rows)
                {
                    if (PtramBHYT == 100)//Do tổng tiền < lương cơ bản *Ptram lương cơ bản quy định-->BHYT thanh toán 100%
                    {
                        if (Utility.Int32Dbnull(dr["tu_tuc"], 0) == 0)//Chỉ tính với các mục không phải tự túc. Còn các mục tự túc BN vẫn chi trả bình thường
                        {
                            dr["bhyt_chitra"] = Utility.DecimaltoDbnull(dr["don_gia"], 0) * (Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.TyleTt], 0) / 100);//BHYT chi trả hết
                            dr["bnhan_chitra"] = 0;//Không tính tiền
                        }
                        else//Là tự túc
                        {
                            dr["bhyt_chitra"] = 0;//BHYT chi trả 0 do tự túc
                            dr["bnhan_chitra"] = Utility.DecimaltoDbnull(dr["don_gia"], 0) * (Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.TyleTt], 0) / 100);
                        }
                    }
                    else if (Utility.Int32Dbnull(dr["tinh_chiphi"], 0) == 0)//Các mục dịch vụ không tính phí khi thanh toán cùng nội trú(phí KCB ngoại trú chưa kịp thanh toán)
                    {
                        dr["bhyt_chitra"] = 0;//BHYT chi trả 0 do tự túc
                        dr["bnhan_chitra"] = 0;//Không tính tiền
                    }
                    else if (Utility.Int32Dbnull(dr["tu_tuc"], 0) == 0)//Không phải tự túc-->Tính các khoản chi trả
                    {
                        decimal BHCT = 0m;

                        if (objLuotkham.DungTuyen == 1)
                        {
                            BHCT = Utility.DecimaltoDbnull(dr["don_gia"], 0) * (Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.TyleTt], 0) / 100) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                        }
                        else
                        {
                            if (objLuotkham.TrangthaiNoitru <= 0)
                            {
                                BHCT = Utility.DecimaltoDbnull(dr["don_gia"], 0) * (Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.TyleTt], 0) / 100) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                            }
                            else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                            {
                                BHCT = Utility.DecimaltoDbnull(dr["don_gia"], 0) * (Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.TyleTt], 0) / 100) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0) / 100);
                            }
                        }
                        dr["bhyt_chitra"] = BHCT;
                        dr["bnhan_chitra"] = Utility.DecimaltoDbnull(dr["don_gia"], 0) * (Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.TyleTt], 0) / 100) - BHCT;
                    }
                    else if (Utility.Int32Dbnull(dr["tu_tuc"], 0) == 1)
                    {
                        dr["bhyt_chitra"] = 0;//BHYT chi trả 0 do tự túc
                        dr["bnhan_chitra"] = dr["don_gia"];
                    }
                    dr["TT_BHYT"] = (Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.BhytChitra], 0)) * Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                    dr["TT_BN"] = (Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.PhuThu], 0)) * Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                    dr["TT"] = (Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.DonGia], 0) * (Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.TyleTt], 0) / 100) + Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.PhuThu], 0)) * Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                    dr["TT_PHUTHU"] = (Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.PhuThu], 0)) * Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                    dr["TT_KHONG_PHUTHU"] = Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.DonGia], 0) * (Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.TyleTt], 0) / 100) * Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                    dr["TT_BN_KHONG_PHUTHU"] = Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.BnhanChitra], 0) * Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                    if (Utility.Int32Dbnull(dr["tu_tuc"], 0) == 1)
                    {
                        dr["TT_TUTUC"] = Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.BnhanChitra], 0) * Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                        dr["TT_BN_KHONG_TUTUC"] = 0;
                    }
                    else
                    {
                        dr["TT_TUTUC"] = 0;
                        dr["TT_BN_KHONG_TUTUC"] = Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.BnhanChitra], 0) * Utility.DecimaltoDbnull(dr[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                    }

                }
            }
        }
        public static void TinhPhamTramBHYT(KcbLuotkham objLuotkham, ref List<KcbThanhtoanChitiet> lstPaymentDetail, ref List<KcbThanhtoanChitiet> lstKcbThanhtoanChitiet_truocdo, decimal PtramBHYT)
        {
            //if (THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_TINHLAI_TOANBODICHVU", "1", false) == "1")//Bỏ đi do chắc chắn phải tính lại
            //{
            if (lstKcbThanhtoanChitiet_truocdo != null)
            {
                foreach (KcbThanhtoanChitiet objChitietThanhtoan in lstKcbThanhtoanChitiet_truocdo)
                {
                    if (PtramBHYT == 100)//Do tổng tiền < lương cơ bản *Ptram lương cơ bản quy định-->BHYT thanh toán 100%
                    {
                        if (objChitietThanhtoan.TuTuc == 0)//Chỉ tính với các mục không phải tự túc. Còn các mục tự túc BN vẫn chi trả bình thường
                        {
                            objChitietThanhtoan.PtramBhyt = 100;
                            objChitietThanhtoan.BhytChitra = Utility.DecimaltoDbnull(objChitietThanhtoan.DonGia, 0) * Utility.DecimaltoDbnull(objChitietThanhtoan.TyleTt, 0) / 100;//BHYT chi trả hết
                            objChitietThanhtoan.BnhanChitra = 0;//Không tính tiền
                        }
                        else
                        {
                            objChitietThanhtoan.PtramBhyt = 0;
                            objChitietThanhtoan.BhytChitra = 0;//BHYT chi trả 0 do tự túc
                            objChitietThanhtoan.BnhanChitra = Utility.DecimaltoDbnull(objChitietThanhtoan.DonGia, 0) * Utility.DecimaltoDbnull(objChitietThanhtoan.TyleTt, 0) / 100;
                        }
                    }
                    else if (objChitietThanhtoan.TinhChiphi == 0)//Các mục dịch vụ không tính phí khi thanh toán cùng nội trú(phí KCB ngoại trú chưa kịp thanh toán)
                    {
                        objChitietThanhtoan.PtramBhyt = 0;
                        objChitietThanhtoan.BhytChitra = 0;//BHYT chi trả 0
                        objChitietThanhtoan.BnhanChitra = 0;//Không tính tiền
                    }
                    else if (objChitietThanhtoan.TuTuc == 0)////Không phải tự túc-->Tính các khoản chi trả
                    {
                        decimal BHCT = 0m;
                        decimal ptrBHYT_tempt = 0m;
                        if (objLuotkham.DungTuyen == 1)
                        {
                            ptrBHYT_tempt = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                            BHCT = Utility.DecimaltoDbnull(objChitietThanhtoan.DonGia, 0) * Utility.DecimaltoDbnull(objChitietThanhtoan.TyleTt, 0) / 100 * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                        }
                        else
                        {
                            if (objLuotkham.TrangthaiNoitru <= 0)
                            {
                                ptrBHYT_tempt = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                                BHCT = Utility.DecimaltoDbnull(objChitietThanhtoan.DonGia, 0) * Utility.DecimaltoDbnull(objChitietThanhtoan.TyleTt, 0) / 100 * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                            }
                            else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                            {
                                ptrBHYT_tempt = (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) * Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0)) / 100;
                                BHCT = Utility.DecimaltoDbnull(objChitietThanhtoan.DonGia, 0) * Utility.DecimaltoDbnull(objChitietThanhtoan.TyleTt, 0) / 100 * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0) / 100);
                            }
                        }

                        objChitietThanhtoan.PtramBhyt = ptrBHYT_tempt;
                        objChitietThanhtoan.BhytChitra = BHCT;
                        objChitietThanhtoan.BnhanChitra = objChitietThanhtoan.DonGia * Utility.DecimaltoDbnull(objChitietThanhtoan.TyleTt, 0) / 100 - BHCT;
                    }
                    else if (objChitietThanhtoan.TuTuc == 1)
                    {
                        //objChitietThanhtoan.MaDoituongKcb = "DV";
                        objChitietThanhtoan.PtramBhyt = 0;
                        objChitietThanhtoan.BhytChitra = 0;//BHYT chi trả 0 do tự túc
                        objChitietThanhtoan.BnhanChitra = objChitietThanhtoan.DonGia;
                    }
                }
            }
            if (lstPaymentDetail != null)
            {
                //Tính cho các chi tiết hiện tại
                foreach (KcbThanhtoanChitiet objChitietThanhtoan in lstPaymentDetail)
                {
                    if (PtramBHYT == 100)//Do tổng tiền < lương cơ bản *Ptram lương cơ bản quy định-->BHYT thanh toán 100%
                    {
                        if (objChitietThanhtoan.TuTuc == 0)//Chỉ tính với các mục không phải tự túc. Còn các mục tự túc BN vẫn chi trả bình thường
                        {
                            objChitietThanhtoan.PtramBhyt = 100;
                            objChitietThanhtoan.BhytChitra = objChitietThanhtoan.DonGia * Utility.DecimaltoDbnull(objChitietThanhtoan.TyleTt, 0) / 100;//BHYT chi trả hết
                            objChitietThanhtoan.BnhanChitra = 0;//Không tính tiền
                        }
                        else//Là tự túc
                        {
                            objChitietThanhtoan.PtramBhyt = 0;
                            objChitietThanhtoan.BhytChitra = 0;//BHYT chi trả 0 do tự túc
                            objChitietThanhtoan.BnhanChitra = objChitietThanhtoan.DonGia;
                        }
                    }
                    else if (objChitietThanhtoan.TinhChiphi == 0)//Các mục dịch vụ không tính phí khi thanh toán cùng nội trú(phí KCB ngoại trú chưa kịp thanh toán)
                    {
                        objChitietThanhtoan.PtramBhyt = 0;
                        objChitietThanhtoan.BhytChitra = 0;//BHYT chi trả 0 do tự túc
                        objChitietThanhtoan.BnhanChitra = 0;//Không tính tiền
                    }
                    else if (objChitietThanhtoan.TuTuc == 0)//Không phải tự túc-->Tính các khoản chi trả
                    {
                        decimal BHCT = 0m;
                        decimal ptrBHYT_tempt = 0m;
                        if (objLuotkham.DungTuyen == 1)
                        {
                            ptrBHYT_tempt = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                            BHCT = Utility.DecimaltoDbnull(objChitietThanhtoan.DonGia, 0) * Utility.DecimaltoDbnull(objChitietThanhtoan.TyleTt, 0) / 100 * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                        }
                        else
                        {
                            if (objLuotkham.TrangthaiNoitru <= 0)
                            {
                                ptrBHYT_tempt = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                                BHCT = Utility.DecimaltoDbnull(objChitietThanhtoan.DonGia, 0) * Utility.DecimaltoDbnull(objChitietThanhtoan.TyleTt, 0) / 100 * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                            }
                            else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                            {
                                ptrBHYT_tempt = (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) * Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0)) / 100;
                                BHCT = Utility.DecimaltoDbnull(objChitietThanhtoan.DonGia, 0) * Utility.DecimaltoDbnull(objChitietThanhtoan.TyleTt, 0) / 100 * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0) / 100);
                            }
                        }

                        objChitietThanhtoan.PtramBhyt = ptrBHYT_tempt;
                        objChitietThanhtoan.BhytChitra = BHCT;
                        objChitietThanhtoan.BnhanChitra = objChitietThanhtoan.DonGia * Utility.DecimaltoDbnull(objChitietThanhtoan.TyleTt, 0) / 100 - BHCT;
                    }
                    else if (objChitietThanhtoan.TuTuc == 1)
                    {
                        //objChitietThanhtoan.MaDoituongKcb = "DV";
                        objChitietThanhtoan.PtramBhyt = 0;
                        objChitietThanhtoan.BhytChitra = 0;//BHYT chi trả 0 do tự túc
                        objChitietThanhtoan.BnhanChitra = objChitietThanhtoan.DonGia;
                    }

                }
            }
            //}
        }
        /// <summary>
        /// Hàm cũ khi chưa phân tách giá cùng chi trả. backup 20/07/2024
        /// </summary>
        /// <param name="objLuotKham"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static ActionResult UpdatePtramBhyt_bak(KcbLuotkham objLuotKham, int option)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var db = new SharedDbConnectionScope())
                    {
                        bool hasPayment = false;
                        decimal ptramBhyt = Utility.DecimaltoDbnull(objLuotKham.PtramBhyt, 0m);
                        decimal ptramBhytGoc = Utility.DecimaltoDbnull(objLuotKham.PtramBhytGoc, 0m);
                        decimal bnhanchitra = 0m;
                        decimal bhytchitra = 0m;
                        decimal dongia = 0m;
                        if (option == 1 || option == -1)
                        {
                            KcbDangkyKcbCollection lstKcbDangkyKcb = new Select().From(KcbDangkyKcb.Schema)
                                .Where(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(objLuotKham.MaLuotkham)
                                .And(KcbDangkyKcb.Columns.LaPhidichvukemtheo).IsNotEqualTo(1).ExecuteAsCollection<KcbDangkyKcbCollection>();
                            foreach (KcbDangkyKcb item in lstKcbDangkyKcb)
                            {
                                if (item.TrangthaiThanhtoan == 0)
                                {
                                    dongia = item.DonGia;
                                    if (item.TuTuc == 0)
                                    {
                                        bhytchitra = THU_VIEN_CHUNG.TinhBhytChitra(ptramBhyt, dongia);
                                        bnhanchitra = dongia - bhytchitra;
                                    }
                                    else
                                    {
                                        bhytchitra = 0;
                                        bnhanchitra = dongia;
                                    }
                                    new Update(KcbDangkyKcb.Schema).Set(KcbDangkyKcb.Columns.BnhanChitra).EqualTo(bnhanchitra)
                                        .Set(KcbDangkyKcb.Columns.BhytChitra).EqualTo(bhytchitra)
                                        .Set(KcbDangkyKcb.Columns.PtramBhyt).EqualTo(ptramBhyt)
                                        .Set(KcbDangkyKcb.Columns.PtramBhytGoc).EqualTo(ptramBhytGoc)
                                        .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(item.IdKham).Execute();
                                }
                                else
                                {

                                    hasPayment = true;
                                    return ActionResult.Cancel;

                                }
                            }
                        }
                        if (option == 2 || option == -1)
                        {
                            KcbChidinhclsChitietCollection lstKcbChidinhclsChitiet = new Select().From(KcbChidinhclsChitiet.Schema)
                               .Where(KcbChidinhclsChitiet.Columns.MaLuotkham).IsEqualTo(objLuotKham.MaLuotkham)
                               .ExecuteAsCollection<KcbChidinhclsChitietCollection>();
                            foreach (KcbChidinhclsChitiet item in lstKcbChidinhclsChitiet)
                            {
                                if (item.TrangthaiThanhtoan == 0)
                                {
                                    if (item.DonGia != null) dongia = item.DonGia.Value;
                                    if (item.TuTuc == 0)
                                    {
                                        bhytchitra = THU_VIEN_CHUNG.TinhBhytChitra(ptramBhyt, dongia) * Utility.DecimaltoDbnull(item.TyleTt, 0) / 100;
                                        bnhanchitra = dongia * Utility.DecimaltoDbnull(item.TyleTt, 0) / 100 - bhytchitra;
                                    }
                                    else
                                    {
                                        bhytchitra = 0;
                                        bnhanchitra = dongia;
                                    }
                                    new Update(KcbChidinhclsChitiet.Schema).Set(KcbChidinhclsChitiet.Columns.BnhanChitra).EqualTo(bnhanchitra)
                                        .Set(KcbChidinhclsChitiet.Columns.BhytChitra).EqualTo(bhytchitra)
                                        .Set(KcbChidinhclsChitiet.Columns.PtramBhyt).EqualTo(ptramBhyt)
                                        .Set(KcbChidinhclsChitiet.Columns.PtramBhytGoc).EqualTo(ptramBhytGoc)
                                        .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(item.IdChitietchidinh).Execute();
                                }
                                else
                                {

                                    hasPayment = true;
                                    return ActionResult.Cancel;

                                }
                            }
                        }
                        if (option == 3 || option == -1)
                        {
                            KcbDonthuocChitietCollection lstKcbDonthuocChitiet = new Select().From(KcbDonthuocChitiet.Schema)
                               .Where(KcbDonthuocChitiet.Columns.MaLuotkham).IsEqualTo(objLuotKham.MaLuotkham)
                               .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                            foreach (KcbDonthuocChitiet _item in lstKcbDonthuocChitiet)
                            {
                                if (_item.TrangthaiThanhtoan == 0)
                                {
                                    dongia = _item.DonGia;
                                    if (_item.TuTuc == 0)
                                    {
                                        bhytchitra = THU_VIEN_CHUNG.TinhBhytChitra(ptramBhyt, dongia);
                                        bnhanchitra = dongia - bhytchitra;
                                    }
                                    else
                                    {
                                        bhytchitra = 0;
                                        bnhanchitra = dongia;
                                    }
                                    new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.BnhanChitra).EqualTo(bnhanchitra)
                                        .Set(KcbDonthuocChitiet.Columns.BhytChitra).EqualTo(bhytchitra)
                                        .Set(KcbDonthuocChitiet.Columns.PtramBhyt).EqualTo(ptramBhyt)
                                        .Set(KcbDonthuocChitiet.Columns.PtramBhytGoc).EqualTo(ptramBhytGoc)
                                        .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(_item.IdChitietdonthuoc).Execute();
                                }
                                else//Nếu dịch vụ đã thanh toán
                                {

                                    hasPayment = true;
                                    return ActionResult.Cancel;

                                }
                            }
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch
            {
                return ActionResult.Exception;
            }
        }
        public static void Bhyt_PhantichGiaCongkham(KcbLuotkham objluotkham, KcbDangkyKcb objCongkham)
        {
            decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", true), 0m);
            decimal don_gia = 0;
            decimal phu_thu = 0;
            decimal PtramBhyt = Utility.DecimaltoDbnull(objCongkham.PtramBhyt, 0);
            decimal tyle_tt = 100;
            decimal bhyt_gia_tyle = 0m;
            decimal bhyt_cct = 0m;
            decimal bn_cct = 0m;
            decimal bn_ttt = 0m;
            decimal bnhan_chitra = 0m;
            don_gia = Utility.DecimaltoDbnull(objCongkham.DonGia, 0);
            phu_thu = Utility.DecimaltoDbnull(objCongkham.PhuThu, 0);
            tyle_tt = !THU_VIEN_CHUNG.IsBaoHiem(objluotkham.IdLoaidoituongKcb) ? 100 : Utility.DecimaltoDbnull(objCongkham.TyleTt, 100);
            if (THU_VIEN_CHUNG.IsBaoHiem(objluotkham.IdLoaidoituongKcb))
            {//Tính lại mức hưởng
                if (objluotkham.DungTuyen == 1)
                {
                }
                else
                {
                    if (!Utility.Byte2Bool(objCongkham.Noitru))//Ngoại trú trái tuyến
                    {
                        PtramBhyt = 0;
                    }
                    else//Nội trú trái tuyến
                    {
                        PtramBhyt = (Utility.DecimaltoDbnull(objluotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                    }
                }

                if (!Utility.Byte2Bool(objCongkham.TuTuc))
                {
                    bhyt_gia_tyle = don_gia * tyle_tt / 100;
                    bhyt_cct = bhyt_gia_tyle * PtramBhyt/100;
                    bn_cct = bhyt_gia_tyle - bhyt_cct;
                    bn_ttt = don_gia - bhyt_gia_tyle;
                }
                else//Tự túc hoặc dịch vụ
                {
                    bhyt_gia_tyle = 0;
                    bhyt_cct = 0;
                    bn_cct = 0;
                    bn_ttt = 0;
                }
            }
            bnhan_chitra = don_gia - bhyt_cct;// hoặc =bn_cct+bn_ttt
            if (Utility.Byte2Bool(objCongkham.TrongGoi))
            {
                bhyt_gia_tyle = 0;
                bhyt_cct = 0;
                bn_cct = 0;
                bn_ttt = 0;
                bnhan_chitra = 0;
                PtramBhyt = 0;
            }
            //Thiết lập lại giá trị các thành phần giá
            objCongkham.PtramBhyt = PtramBhyt;
            objCongkham.BhytGiaTyle = bhyt_gia_tyle;
            objCongkham.BhytChitra = bhyt_cct;
            objCongkham.BnCct = bn_cct;
            objCongkham.BnTtt = bn_ttt;
            objCongkham.BnhanChitra = bnhan_chitra;
        }
        public static void Bhyt_PhantichGiaDichvuCLS(KcbLuotkham objluotkham, KcbChidinhclsChitiet objItem, byte noitru = 0)
        {
            decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", true), 0m);
            decimal don_gia = 0;
            decimal phu_thu = 0;
            decimal PtramBhyt = Utility.DecimaltoDbnull(objItem.PtramBhyt, 0);
            decimal tyle_tt = 100;
            decimal bhyt_gia_tyle = 0m;
            decimal bhyt_cct = 0m;
            decimal bn_cct = 0m;
            decimal bn_ttt = 0m;
            decimal bnhan_chitra = 0m;
            don_gia = Utility.DecimaltoDbnull(objItem.DonGia, 0);
            phu_thu = Utility.DecimaltoDbnull(objItem.PhuThu, 0);
            tyle_tt = !THU_VIEN_CHUNG.IsBaoHiem(objluotkham.IdLoaidoituongKcb) ? 100 : Utility.DecimaltoDbnull(objItem.TyleTt, 100);
            if (THU_VIEN_CHUNG.IsBaoHiem(objluotkham.IdLoaidoituongKcb))
            { //Tính lại mức hưởng
                if (objluotkham.DungTuyen == 1)
                {
                }
                else
                {
                    if (!Utility.Byte2Bool(noitru))//Ngoại trú trái tuyến
                    {
                        PtramBhyt = 0;
                    }
                    else//Nội trú trái tuyến
                    {
                        PtramBhyt = (Utility.DecimaltoDbnull(objluotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                    }
                }

                if (!Utility.Byte2Bool(objItem.TuTuc))
                {
                    bhyt_gia_tyle = don_gia * tyle_tt / 100;
                    bhyt_cct = bhyt_gia_tyle * PtramBhyt/100;
                    bn_cct = bhyt_gia_tyle - bhyt_cct;
                    bn_ttt = don_gia - bhyt_gia_tyle;
                }
                else//Tự túc hoặc dịch vụ
                {
                    bhyt_gia_tyle = 0;
                    bhyt_cct = 0;
                    bn_cct = 0;
                    bn_ttt = 0;
                }
            }
            bnhan_chitra = don_gia - bhyt_cct;// hoặc =bn_cct+bn_ttt
            if (Utility.Byte2Bool(objItem.TrongGoi))
            {
                bhyt_gia_tyle = 0;
                bhyt_cct = 0;
                bn_cct = 0;
                bn_ttt = 0;
                bnhan_chitra = 0;
                PtramBhyt = 0;
            }
            //Thiết lập lại giá trị các thành phần giá
            objItem.PtramBhyt = PtramBhyt;
            objItem.BhytGiaTyle = bhyt_gia_tyle;
            objItem.BhytChitra = bhyt_cct;
            objItem.BnCct = bn_cct;
            objItem.BnTtt = bn_ttt;
            objItem.BnhanChitra = bnhan_chitra;
        }
        public static void Bhyt_PhantichGiaThuocVTTH(KcbLuotkham objluotkham, KcbDonthuocChitiet objItem, byte noitru = 0)
        {
            decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", true), 0m);
            decimal don_gia = 0;
            decimal phu_thu = 0;
            decimal PtramBhyt = Utility.DecimaltoDbnull(objItem.PtramBhyt, 0);
            decimal tyle_tt = 100;
            decimal bhyt_gia_tyle = 0m;
            decimal bhyt_cct = 0m;
            decimal bn_cct = 0m;
            decimal bn_ttt = 0m;
            decimal bnhan_chitra = 0m;
            don_gia = Utility.DecimaltoDbnull(objItem.DonGia, 0);
            phu_thu = Utility.DecimaltoDbnull(objItem.PhuThu, 0);
            tyle_tt = !THU_VIEN_CHUNG.IsBaoHiem(objluotkham.IdLoaidoituongKcb) ? 100 : Utility.DecimaltoDbnull(objItem.TyleTt, 100);
            if (THU_VIEN_CHUNG.IsBaoHiem(objluotkham.IdLoaidoituongKcb))
            {//Tính lại mức hưởng
                if (objluotkham.DungTuyen == 1)
                {
                }
                else
                {
                    if (!Utility.Byte2Bool(noitru))//Ngoại trú trái tuyến
                    {
                        PtramBhyt = 0;
                    }
                    else//Nội trú trái tuyến
                    {
                        PtramBhyt = (Utility.DecimaltoDbnull(objluotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                    }
                }

                if (!Utility.Byte2Bool(objItem.TuTuc))
                {
                    bhyt_gia_tyle = don_gia * tyle_tt / 100;
                    bhyt_cct = bhyt_gia_tyle * PtramBhyt/100;
                    bn_cct = bhyt_gia_tyle - bhyt_cct;
                    bn_ttt = don_gia - bhyt_gia_tyle;
                }
                else//Tự túc hoặc dịch vụ
                {
                    bhyt_gia_tyle = 0;
                    bhyt_cct = 0;
                    bn_cct = 0;
                    bn_ttt = 0;
                }
            }
            bnhan_chitra = don_gia - bhyt_cct;// hoặc =bn_cct+bn_ttt
            if (Utility.Byte2Bool(objItem.TrongGoi))
            {
                bhyt_gia_tyle = 0;
                bhyt_cct = 0;
                bn_cct = 0;
                bn_ttt = 0;
                bnhan_chitra = 0;
                PtramBhyt = 0;
            }
            //Thiết lập lại giá trị các thành phần giá
            objItem.PtramBhyt = PtramBhyt;
            objItem.BhytGiaTyle = bhyt_gia_tyle;
            objItem.BhytChitra = bhyt_cct;
            objItem.BnCct = bn_cct;
            objItem.BnTtt = bn_ttt;
            objItem.BnhanChitra = bnhan_chitra;
        }
        public static void Bhyt_PhantichGiaBuongGiuong(KcbLuotkham objluotkham, NoitruPhanbuonggiuong objItem)
        {
            decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", true), 0m);
            decimal don_gia = 0;
            decimal phu_thu = 0;
            decimal PtramBhyt = Utility.DecimaltoDbnull(objItem.PtramBhyt, 0);
            decimal tyle_tt = 100;
            decimal bhyt_gia_tyle = 0m;
            decimal bhyt_cct = 0m;
            decimal bn_cct = 0m;
            decimal bn_ttt = 0m;
            decimal bnhan_chitra = 0m;
            don_gia = Utility.DecimaltoDbnull(objItem.DonGia, 0);
            phu_thu = Utility.DecimaltoDbnull(objItem.PhuThu, 0);
            tyle_tt = !THU_VIEN_CHUNG.IsBaoHiem(objluotkham.IdLoaidoituongKcb) ? 100 : Utility.DecimaltoDbnull(objItem.TyleTt, 100);
            if (THU_VIEN_CHUNG.IsBaoHiem(objluotkham.IdLoaidoituongKcb))
            {//Tính lại mức hưởng
                if (objluotkham.DungTuyen == 1)
                {
                }
                else
                {
                    if (!Utility.Byte2Bool(objItem.NoiTru))//Ngoại trú trái tuyến
                    {
                        PtramBhyt = 0;
                    }
                    else//Nội trú trái tuyến
                    {
                        PtramBhyt = (Utility.DecimaltoDbnull(objluotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                    }
                }

                if (!Utility.Byte2Bool(objItem.TuTuc))
                {
                    bhyt_gia_tyle = don_gia * tyle_tt / 100;
                    bhyt_cct = bhyt_gia_tyle * PtramBhyt/100;
                    bn_cct = bhyt_gia_tyle - bhyt_cct;
                    bn_ttt = don_gia - bhyt_gia_tyle;
                }
                else//Tự túc hoặc dịch vụ
                {
                    bhyt_gia_tyle = 0;
                    bhyt_cct = 0;
                    bn_cct = 0;
                    bn_ttt = 0;
                }
            }
            bnhan_chitra = don_gia - bhyt_cct;// hoặc =bn_cct+bn_ttt
            if (Utility.Byte2Bool(objItem.TrongGoi))
            {
                bhyt_gia_tyle = 0;
                bhyt_cct = 0;
                bn_cct = 0;
                bn_ttt = 0;
                bnhan_chitra = 0;
                PtramBhyt = 0;
            }

            //Thiết lập lại giá trị các thành phần giá
            objItem.PtramBhyt = PtramBhyt;
            objItem.BhytGiaTyle = bhyt_gia_tyle;
            objItem.BhytChitra = bhyt_cct;
            objItem.BnCct = bn_cct;
            objItem.BnTtt = bn_ttt;
            objItem.BnhanChitra = bnhan_chitra;
        }
        public static void Bhyt_PhantichGia(KcbLuotkham objluotkham, DataRow _row)
        {
            decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", true), 0m);
            decimal don_gia = 0;
            decimal phu_thu = 0;
            decimal PtramBhyt = Utility.DecimaltoDbnull(objluotkham.PtramBhyt, 0);
            decimal tyle_tt = 100;
            decimal bhyt_gia_tyle = 0m;
            decimal bhyt_cct = 0m;
            decimal bn_cct = 0m;
            decimal bn_ttt = 0m;
            decimal bnhan_chitra = 0m;
            don_gia = Utility.DecimaltoDbnull(_row["don_gia"], 0);
            phu_thu = Utility.DecimaltoDbnull(_row["phu_thu"], 0);
            decimal so_luong = Utility.DecimaltoDbnull(_row["so_luong"], 0);
            tyle_tt = !THU_VIEN_CHUNG.IsBaoHiem(objluotkham.IdLoaidoituongKcb) ? 100 : Utility.DecimaltoDbnull(_row["tyle_tt"], 100);
            if (THU_VIEN_CHUNG.IsBaoHiem(objluotkham.IdLoaidoituongKcb))
            {//Tính lại mức hưởng
                if (objluotkham.DungTuyen == 1)
                {
                }
                else
                {
                    if (!Utility.Byte2Bool(_row["noi_tru"]))//Ngoại trú trái tuyến
                    {
                        PtramBhyt = 0;
                    }
                    else//Nội trú trái tuyến
                    {
                        PtramBhyt = (Utility.DecimaltoDbnull(objluotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                    }
                }

                if (!Utility.Byte2Bool(_row["tu_tuc"]))
                {
                    bhyt_gia_tyle = don_gia * tyle_tt / 100;
                    bhyt_cct = bhyt_gia_tyle * PtramBhyt/100;
                    bn_cct = bhyt_gia_tyle - bhyt_cct;
                    bn_ttt = don_gia - bhyt_gia_tyle;
                }
                else//Tự túc hoặc dịch vụ
                {
                    bhyt_gia_tyle = 0;
                    bhyt_cct = 0;
                    bn_cct = 0;
                    bn_ttt = 0;
                }
            }
            bnhan_chitra = don_gia - bhyt_cct;// hoặc =bn_cct+bn_ttt
            //Thiết lập lại giá trị các thành phần giá
            if (Utility.Byte2Bool(_row["trong_goi"]))// && Utility.Int32Dbnull(_row["id_goi"])<=0)
            {
                bhyt_gia_tyle = 0;
                bhyt_cct = 0;
                bn_cct = 0;
                bn_ttt = 0;
                bnhan_chitra = 0;
                PtramBhyt = 0;
            }
            _row["tyle_tt"] = tyle_tt;
            _row["ptram_bhyt"] = PtramBhyt;
            _row["bhyt_gia_tyle"] = bhyt_gia_tyle;
            _row["bhyt_chitra"] = bhyt_cct;
            _row["bn_cct"] = bn_cct;
            _row["bn_ttt"] = bn_ttt;
            _row["bnhan_chitra"] = bnhan_chitra;
            _row["tt"] = (don_gia + phu_thu) * so_luong;
            //Tính một số đơn giá + tổng tiền
            if (_row.Table.Columns.Contains("dongia_bv")) _row["dongia_bv"] = (don_gia + phu_thu);
            if (_row.Table.Columns.Contains("dongia_cct")) _row["dongia_cct"] = bhyt_cct;
            if (_row.Table.Columns.Contains("TT")) _row["TT"] = (don_gia + phu_thu) * so_luong;
            if (_row.Table.Columns.Contains("TT_BN")) _row["TT_BN"] = (bnhan_chitra + phu_thu) * so_luong;
            if (_row.Table.Columns.Contains("TT_BHYT")) _row["TT_BHYT"] = (bhyt_gia_tyle * so_luong);
            if (_row.Table.Columns.Contains("tt_bhyt_cct")) _row["tt_bhyt_cct"] = (bhyt_cct * so_luong);

            if (_row.Table.Columns.Contains("TT_BN_CCT")) _row["TT_BN_CCT"] = (bn_cct) * so_luong;
            if (_row.Table.Columns.Contains("tt_bn_ttt")) _row["tt_bn_ttt"] = (bn_ttt * so_luong);
            if (_row.Table.Columns.Contains("TT_BN_KHONG_PHUTHU")) _row["TT_BN_KHONG_PHUTHU"] = (bnhan_chitra * so_luong);
            if (_row.Table.Columns.Contains("TT_PHUTHU")) _row["TT_PHUTHU"] = (phu_thu * so_luong);
            if (_row.Table.Columns.Contains("TT_KHONG_PHUTHU")) _row["TT_KHONG_PHUTHU"] = (don_gia * so_luong);
            if (_row.Table.Columns.Contains("TT_PHUTHU")) _row["TT_PHUTHU"] = (phu_thu * so_luong);

        }
        public static void Bhyt_PhantichGia(KcbLuotkham objluotkham, DataRow _row, decimal PtramBhyt)
        {
            decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", true), 0m);
            decimal don_gia = 0;
            decimal phu_thu = 0;
            decimal tyle_tt = 100;
            decimal bhyt_gia_tyle = 0m;
            decimal bhyt_cct = 0m;
            decimal bn_cct = 0m;
            decimal bn_ttt = 0m;
            decimal bnhan_chitra = 0m;
            don_gia = Utility.DecimaltoDbnull(_row["don_gia"], 0);
            phu_thu = Utility.DecimaltoDbnull(_row["phu_thu"], 0);
            decimal so_luong = Utility.DecimaltoDbnull(_row["so_luong"], 0);
            tyle_tt = !THU_VIEN_CHUNG.IsBaoHiem(objluotkham.IdLoaidoituongKcb) ? 100 : Utility.DecimaltoDbnull(_row["tyle_tt"], 100);
            if (THU_VIEN_CHUNG.IsBaoHiem(objluotkham.IdLoaidoituongKcb))
            {//Tính lại mức hưởng
                if (objluotkham.DungTuyen == 1)
                {
                }
                else
                {
                    if (!Utility.Byte2Bool(_row["noi_tru"]))//Ngoại trú trái tuyến
                    {
                        PtramBhyt = 0;
                    }
                    else//Nội trú trái tuyến
                    {
                        PtramBhyt = (Utility.DecimaltoDbnull(objluotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                    }
                }

                if (!Utility.Byte2Bool(_row["tu_tuc"]))
                {
                    bhyt_gia_tyle = don_gia * tyle_tt / 100;
                    bhyt_cct = bhyt_gia_tyle * PtramBhyt / 100;
                    bn_cct = bhyt_gia_tyle - bhyt_cct;
                    bn_ttt = don_gia - bhyt_gia_tyle;
                }
                else//Tự túc hoặc dịch vụ
                {
                    bhyt_gia_tyle = 0;
                    bhyt_cct = 0;
                    bn_cct = 0;
                    bn_ttt = 0;
                }
            }
            bnhan_chitra = don_gia - bhyt_cct;// hoặc =bn_cct+bn_ttt
            //Thiết lập lại giá trị các thành phần giá
            if (Utility.Byte2Bool(_row["trong_goi"]))// && Utility.Int32Dbnull(_row["id_goi"]) <= 0)
            {
                bhyt_gia_tyle = 0;
                bhyt_cct = 0;
                bn_cct = 0;
                bn_ttt = 0;
                bnhan_chitra = 0;
                PtramBhyt = 0;
            }
            _row["tyle_tt"] = tyle_tt;
            _row["ptram_bhyt"] = PtramBhyt;
            _row["bhyt_gia_tyle"] = bhyt_gia_tyle;
            _row["bhyt_chitra"] = bhyt_cct;
            _row["bn_cct"] = bn_cct;
            _row["bn_ttt"] = bn_ttt;
            _row["bnhan_chitra"] = bnhan_chitra;
            _row["tt"] = (don_gia + phu_thu) * so_luong;
            //Tính một số đơn giá + tổng tiền
            if (_row.Table.Columns.Contains("dongia_bv")) _row["dongia_bv"] = (don_gia + phu_thu);
            if (_row.Table.Columns.Contains("dongia_cct")) _row["dongia_cct"] = bhyt_cct;
            if (_row.Table.Columns.Contains("TT")) _row["TT"] = (don_gia + phu_thu) * so_luong;
            if (_row.Table.Columns.Contains("TT_BN")) _row["TT_BN"] = (bnhan_chitra + phu_thu) * so_luong;
            if (_row.Table.Columns.Contains("TT_BHYT")) _row["TT_BHYT"] = (bhyt_gia_tyle * so_luong);
            if (_row.Table.Columns.Contains("tt_bhyt_cct")) _row["tt_bhyt_cct"] = (bhyt_cct * so_luong);

            if (_row.Table.Columns.Contains("TT_BN_CCT")) _row["TT_BN_CCT"] = (bn_cct) * so_luong;
            if (_row.Table.Columns.Contains("tt_bn_ttt")) _row["tt_bn_ttt"] = (bn_ttt * so_luong);
            if (_row.Table.Columns.Contains("TT_BN_KHONG_PHUTHU")) _row["TT_BN_KHONG_PHUTHU"] = (bnhan_chitra * so_luong);
            if (_row.Table.Columns.Contains("TT_PHUTHU")) _row["TT_PHUTHU"] = (phu_thu * so_luong);
            if (_row.Table.Columns.Contains("TT_KHONG_PHUTHU")) _row["TT_KHONG_PHUTHU"] = (don_gia * so_luong);
            if (_row.Table.Columns.Contains("TT_PHUTHU")) _row["TT_PHUTHU"] = (phu_thu * so_luong);

        }
        public static void Bhyt_PhantichGia(KcbLuotkham objluotkham, GridEXRow _row)
        {
            decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", true), 0m);
            decimal don_gia = 0;
            decimal phu_thu = 0;
            decimal PtramBhyt = Utility.DecimaltoDbnull(objluotkham.PtramBhyt, 0);
            decimal tyle_tt = 100;
            decimal bhyt_gia_tyle = 0m;
            decimal bhyt_cct = 0m;
            decimal bn_cct = 0m;
            decimal bn_ttt = 0m;
            decimal bnhan_chitra = 0m;
            don_gia = Utility.DecimaltoDbnull(_row.Cells["don_gia"].Value, 0);
            phu_thu = Utility.DecimaltoDbnull(_row.Cells["phu_thu"].Value, 0);
            decimal so_luong = Utility.DecimaltoDbnull(_row.Cells["so_luong"].Value, 0);
            tyle_tt = !THU_VIEN_CHUNG.IsBaoHiem(objluotkham.IdLoaidoituongKcb) ? 100 : Utility.DecimaltoDbnull(_row.Cells["tyle_tt"].Value, 100);
            if (THU_VIEN_CHUNG.IsBaoHiem(objluotkham.IdLoaidoituongKcb))
            {//Tính lại mức hưởng
                if (objluotkham.DungTuyen == 1)
                {
                }
                else
                {
                    if (!Utility.Byte2Bool(_row.Cells["noi_tru"].Value))//Ngoại trú trái tuyến
                    {
                        PtramBhyt = 0;
                    }
                    else//Nội trú trái tuyến
                    {
                        PtramBhyt = (Utility.DecimaltoDbnull(objluotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                    }
                }

                if (!Utility.Byte2Bool(_row.Cells["tu_tuc"].Value))
                {
                    bhyt_gia_tyle = don_gia * tyle_tt / 100;
                    bhyt_cct = bhyt_gia_tyle * PtramBhyt/100;
                    bn_cct = bhyt_gia_tyle - bhyt_cct;
                    bn_ttt = don_gia - bhyt_gia_tyle;
                }
                else//Tự túc hoặc dịch vụ
                {
                    bhyt_gia_tyle = 0;
                    bhyt_cct = 0;
                    bn_cct = 0;
                    bn_ttt = 0;
                }
            }
            bnhan_chitra = don_gia - bhyt_cct;// hoặc =bn_cct+bn_ttt
            if (Utility.Byte2Bool(_row.Cells["trong_goi"].Value))
            {
                bhyt_gia_tyle = 0;
                bhyt_cct = 0;
                bn_cct = 0;
                bn_ttt = 0;
                bnhan_chitra = 0;
                PtramBhyt = 0;
            }
            //Thiết lập lại giá trị các thành phần giá
            _row.BeginEdit();
            
            _row.Cells["tyle_tt"].Value = tyle_tt;
            _row.Cells["ptram_bhyt"].Value = PtramBhyt;
            _row.Cells["bhyt_gia_tyle"].Value = bhyt_gia_tyle;
            _row.Cells["bhyt_chitra"].Value = bhyt_cct;
            _row.Cells["bn_cct"].Value = bn_cct;
            _row.Cells["bn_ttt"].Value = bn_ttt;
            _row.Cells["bnhan_chitra"].Value = bnhan_chitra;
            _row.Cells["tt"].Value = (don_gia + phu_thu) * so_luong;
            //Tính một số đơn giá + tổng tiền
            if (_row.GridEX.RootTable.Columns.Contains("dongia_bv")) _row.Cells["dongia_bv"].Value = (don_gia + phu_thu);
            if (_row.GridEX.RootTable.Columns.Contains("dongia_cct")) _row.Cells["dongia_cct"].Value = bhyt_cct;
            if (_row.GridEX.RootTable.Columns.Contains("TT")) _row.Cells["TT"].Value = (don_gia + phu_thu)* so_luong;
            if (_row.GridEX.RootTable.Columns.Contains("TT_BN")) _row.Cells["TT_BN"].Value = (bnhan_chitra + phu_thu) * so_luong;
            if (_row.GridEX.RootTable.Columns.Contains("TT_BHYT")) _row.Cells["TT_BHYT"].Value = (bhyt_gia_tyle*so_luong);
            if (_row.GridEX.RootTable.Columns.Contains("tt_bhyt_cct")) _row.Cells["tt_bhyt_cct"].Value = (bhyt_cct*so_luong);

            if (_row.GridEX.RootTable.Columns.Contains("TT_BN_CCT")) _row.Cells["TT_BN_CCT"].Value = (bn_cct) * so_luong;
            if (_row.GridEX.RootTable.Columns.Contains("tt_bn_ttt")) _row.Cells["tt_bn_ttt"].Value = (bn_ttt * so_luong);
            if (_row.GridEX.RootTable.Columns.Contains("TT_BN_KHONG_PHUTHU")) _row.Cells["TT_BN_KHONG_PHUTHU"].Value = (bnhan_chitra * so_luong);
            if (_row.GridEX.RootTable.Columns.Contains("TT_PHUTHU")) _row.Cells["TT_PHUTHU"].Value = (phu_thu * so_luong);
            if (_row.GridEX.RootTable.Columns.Contains("TT_KHONG_PHUTHU")) _row.Cells["TT_KHONG_PHUTHU"].Value = (don_gia * so_luong);
            if (_row.GridEX.RootTable.Columns.Contains("TT_PHUTHU")) _row.Cells["TT_PHUTHU"].Value = (phu_thu * so_luong);
            _row.EndEdit();
        }
        public static void Bhyt_PhantichGia(KcbLuotkham objluotkham, GridEXRow _row, decimal PtramBhyt)
        {
            decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", true), 0m);
            decimal don_gia = 0;
            decimal phu_thu = 0;
            decimal tyle_tt = 100;
            decimal bhyt_gia_tyle = 0m;
            decimal bhyt_cct = 0m;
            decimal bn_cct = 0m;
            decimal bn_ttt = 0m;
            decimal bnhan_chitra = 0m;
            don_gia = Utility.DecimaltoDbnull(_row.Cells["don_gia"].Value, 0);
            phu_thu = Utility.DecimaltoDbnull(_row.Cells["phu_thu"].Value, 0);
            decimal so_luong = Utility.DecimaltoDbnull(_row.Cells["so_luong"].Value, 0);
            tyle_tt = !THU_VIEN_CHUNG.IsBaoHiem(objluotkham.IdLoaidoituongKcb) ? 100 : Utility.DecimaltoDbnull(_row.Cells["tyle_tt"].Value, 100);
            if (THU_VIEN_CHUNG.IsBaoHiem(objluotkham.IdLoaidoituongKcb))
            {//Tính lại mức hưởng
                if (objluotkham.DungTuyen == 1)
                {
                }
                else
                {
                    if (!Utility.Byte2Bool(_row.Cells["noi_tru"].Value))//Ngoại trú trái tuyến
                    {
                        PtramBhyt = 0;
                    }
                    else//Nội trú trái tuyến
                    {
                        PtramBhyt = (Utility.DecimaltoDbnull(objluotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                    }
                }

                if (!Utility.Byte2Bool(_row.Cells["tu_tuc"].Value))
                {
                    bhyt_gia_tyle = don_gia * tyle_tt / 100;
                    bhyt_cct = bhyt_gia_tyle * PtramBhyt / 100;
                    bn_cct = bhyt_gia_tyle - bhyt_cct;
                    bn_ttt = don_gia - bhyt_gia_tyle;
                }
                else//Tự túc hoặc dịch vụ
                {
                    bhyt_gia_tyle = 0;
                    bhyt_cct = 0;
                    bn_cct = 0;
                    bn_ttt = 0;
                }
            }
            bnhan_chitra = don_gia - bhyt_cct;// hoặc =bn_cct+bn_ttt
            if (Utility.Byte2Bool(_row.Cells["trong_goi"].Value))
            {
                bhyt_gia_tyle = 0;
                bhyt_cct = 0;
                bn_cct = 0;
                bn_ttt = 0;
                bnhan_chitra = 0;
                PtramBhyt = 0;
            }
            //Thiết lập lại giá trị các thành phần giá
            _row.BeginEdit();

            _row.Cells["tyle_tt"].Value = tyle_tt;
            _row.Cells["ptram_bhyt"].Value = PtramBhyt;
            _row.Cells["bhyt_gia_tyle"].Value = bhyt_gia_tyle;
            _row.Cells["bhyt_chitra"].Value = bhyt_cct;
            _row.Cells["bn_cct"].Value = bn_cct;
            _row.Cells["bn_ttt"].Value = bn_ttt;
            _row.Cells["bnhan_chitra"].Value = bnhan_chitra;
            _row.Cells["tt"].Value = (don_gia + phu_thu) * so_luong;
            //Tính một số đơn giá + tổng tiền
            if (_row.GridEX.RootTable.Columns.Contains("dongia_bv")) _row.Cells["dongia_bv"].Value = (don_gia + phu_thu);
            if (_row.GridEX.RootTable.Columns.Contains("dongia_cct")) _row.Cells["dongia_cct"].Value = bhyt_cct;
            if (_row.GridEX.RootTable.Columns.Contains("TT")) _row.Cells["TT"].Value = (don_gia + phu_thu) * so_luong;
            if (_row.GridEX.RootTable.Columns.Contains("TT_BN")) _row.Cells["TT_BN"].Value = (bnhan_chitra + phu_thu) * so_luong;
            if (_row.GridEX.RootTable.Columns.Contains("TT_BHYT")) _row.Cells["TT_BHYT"].Value = (bhyt_gia_tyle * so_luong);
            if (_row.GridEX.RootTable.Columns.Contains("tt_bhyt_cct")) _row.Cells["tt_bhyt_cct"].Value = (bhyt_cct * so_luong);

            if (_row.GridEX.RootTable.Columns.Contains("TT_BN_CCT")) _row.Cells["TT_BN_CCT"].Value = (bn_cct) * so_luong;
            if (_row.GridEX.RootTable.Columns.Contains("tt_bn_ttt")) _row.Cells["tt_bn_ttt"].Value = (bn_ttt * so_luong);
            if (_row.GridEX.RootTable.Columns.Contains("TT_BN_KHONG_PHUTHU")) _row.Cells["TT_BN_KHONG_PHUTHU"].Value = (bnhan_chitra * so_luong);
            if (_row.GridEX.RootTable.Columns.Contains("TT_PHUTHU")) _row.Cells["TT_PHUTHU"].Value = (phu_thu * so_luong);
            if (_row.GridEX.RootTable.Columns.Contains("TT_KHONG_PHUTHU")) _row.Cells["TT_KHONG_PHUTHU"].Value = (don_gia * so_luong);
            if (_row.GridEX.RootTable.Columns.Contains("TT_PHUTHU")) _row.Cells["TT_PHUTHU"].Value = (phu_thu * so_luong);
            _row.EndEdit();
        }
        public static ActionResult UpdatePtramBhyt(KcbLuotkham objLuotkham, int option)
        {
            try
            {
                DataTable dtData = SPs.BhytLaydichvuPhantichgia(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, Utility.ByteDbnull(option == -1 ? 100 : option, 200)).GetDataSet().Tables[0];
                using (var scope = new TransactionScope())
                {
                    using (var db = new SharedDbConnectionScope())
                    {
                        decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", true), 0m);
                        bool hasPayment = false;
                        decimal don_gia = 0;
                        decimal phu_thu = 0;
                        decimal PtramBhyt = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                        decimal tyle_tt = 100;
                        decimal bhyt_gia_tyle = 0m;
                        decimal bhyt_cct = 0m;
                        decimal bn_cct = 0m;
                        decimal bn_ttt = 0m;
                        decimal bnhan_chitra = 0m;
                        decimal so_luong = 1;

                        if (option == 1 || option == -1)
                        {
                            List<DataRow> lstDr = dtData.Select("loai_thanhtoan=1").ToList<DataRow>();
                            foreach (DataRow item in lstDr)
                            {

                                don_gia = Utility.DecimaltoDbnull(item["don_gia"], 0);
                                phu_thu = Utility.DecimaltoDbnull(item["phu_thu"], 0);
                                tyle_tt = !THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) ? 100 : Utility.DecimaltoDbnull(item["tyle_tt"], 100);
                                //Tính lại mức hưởng
                                if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                                {
                                    if (objLuotkham.DungTuyen == 1)
                                    {
                                    }
                                    else
                                    {
                                        if (!Utility.Byte2Bool(item["noitru"]))//Ngoại trú trái tuyến
                                        {
                                            PtramBhyt = 0;
                                        }
                                        else//Nội trú trái tuyến
                                        {
                                            PtramBhyt = (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                                        }
                                    }

                                    if (!Utility.Byte2Bool(item["tu_tuc"]))
                                    {
                                        bhyt_gia_tyle = don_gia * tyle_tt / 100;
                                        bhyt_cct = bhyt_gia_tyle * PtramBhyt/100;
                                        bn_cct = bhyt_gia_tyle - bhyt_cct;
                                        bn_ttt = don_gia - bhyt_gia_tyle;
                                    }
                                    else//Tự túc hoặc dịch vụ
                                    {
                                        bhyt_gia_tyle = 0;
                                        bhyt_cct = 0;
                                        bn_cct = 0;
                                        bn_ttt = 0;
                                    }
                                }
                                bnhan_chitra = don_gia - bhyt_cct;// hoặc =bn_cct+bn_ttt
                                if (Utility.Byte2Bool(item["trong_goi"]))
                                {
                                    bhyt_gia_tyle = 0;
                                    bhyt_cct = 0;
                                    bn_cct = 0;
                                    bn_ttt = 0;
                                    bnhan_chitra = 0;
                                    PtramBhyt = 0;
                                }
                                new Update(KcbDangkyKcb.Schema)
                                    .Set(KcbDangkyKcb.Columns.BnhanChitra).EqualTo(bnhan_chitra)
                                    .Set(KcbDangkyKcb.Columns.BhytChitra).EqualTo(bhyt_cct)
                                    .Set(KcbDangkyKcb.Columns.BnCct).EqualTo(bn_cct)
                                    .Set(KcbDangkyKcb.Columns.BnTtt).EqualTo(bn_ttt)
                                    .Set(KcbDangkyKcb.Columns.BhytGiaTyle).EqualTo(bhyt_gia_tyle)
                                    .Set(KcbDangkyKcb.Columns.PtramBhyt).EqualTo(PtramBhyt)
                                    .Set(KcbDangkyKcb.Columns.PtramBhytGoc).EqualTo(objLuotkham.PtramBhytGoc)
                                    .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(Utility.Int64Dbnull(item["id"], -1))
                                     .And(KcbDangkyKcb.Columns.TrangthaiThanhtoan).IsEqualTo(0)
                                     .Execute();
                            }
                        }
                        if (option == 2 || option == -1)
                        {
                            List<DataRow> lstDr = dtData.Select("loai_thanhtoan=2").ToList<DataRow>();
                            foreach (DataRow item in lstDr)
                            {

                                don_gia = Utility.DecimaltoDbnull(item["don_gia"], 0);
                                phu_thu = Utility.DecimaltoDbnull(item["phu_thu"], 0);
                                tyle_tt = !THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) ? 100 : Utility.DecimaltoDbnull(item["tyle_tt"], 100);
                                if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                                {
                                    //Tính lại mức hưởng
                                    if (objLuotkham.DungTuyen == 1)
                                    {
                                    }
                                    else
                                    {
                                        if (!Utility.Byte2Bool(item["noitru"]))//Ngoại trú trái tuyến
                                        {
                                            PtramBhyt = 0;
                                        }
                                        else//Nội trú trái tuyến
                                        {
                                            PtramBhyt = (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                                        }
                                    }

                                    if (!Utility.Byte2Bool(item["tu_tuc"]))
                                    {
                                        bhyt_gia_tyle = don_gia * tyle_tt / 100;
                                        bhyt_cct = bhyt_gia_tyle * PtramBhyt/100;
                                        bn_cct = bhyt_gia_tyle - bhyt_cct;
                                        bn_ttt = don_gia - bhyt_gia_tyle;
                                    }
                                    else//Tự túc hoặc dịch vụ
                                    {
                                        bhyt_gia_tyle = 0;
                                        bhyt_cct = 0;
                                        bn_cct = 0;
                                        bn_ttt = 0;
                                    }
                                }
                                bnhan_chitra = don_gia - bhyt_cct;// hoặc =bn_cct+bn_ttt
                                if (Utility.Byte2Bool(item["trong_goi"]))
                                {
                                    bhyt_gia_tyle = 0;
                                    bhyt_cct = 0;
                                    bn_cct = 0;
                                    bn_ttt = 0;
                                    bnhan_chitra = 0;
                                    PtramBhyt = 0;
                                }
                                new Update(KcbChidinhclsChitiet.Schema)
                                    .Set(KcbChidinhclsChitiet.Columns.BnhanChitra).EqualTo(bnhan_chitra)
                                    .Set(KcbChidinhclsChitiet.Columns.BhytChitra).EqualTo(bhyt_cct)
                                    .Set(KcbChidinhclsChitiet.Columns.BnCct).EqualTo(bn_cct)
                                    .Set(KcbChidinhclsChitiet.Columns.BnTtt).EqualTo(bn_ttt)
                                    .Set(KcbChidinhclsChitiet.Columns.BhytGiaTyle).EqualTo(bhyt_gia_tyle)
                                    .Set(KcbChidinhclsChitiet.Columns.PtramBhyt).EqualTo(PtramBhyt)
                                    .Set(KcbChidinhclsChitiet.Columns.PtramBhytGoc).EqualTo(objLuotkham.PtramBhytGoc)
                                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(Utility.Int64Dbnull(item["id"], -1))
                                     .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(0)
                                     .Execute();


                            }
                        }
                        if (option == 3 || option == -1)
                        {
                            List<DataRow> lstDr = dtData.Select("loai_thanhtoan=3").ToList<DataRow>();
                            foreach (DataRow item in lstDr)
                            {

                                don_gia = Utility.DecimaltoDbnull(item["don_gia"], 0);
                                phu_thu = Utility.DecimaltoDbnull(item["phu_thu"], 0);
                                tyle_tt = !THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) ? 100 : Utility.DecimaltoDbnull(item["tyle_tt"], 100);
                                if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                                {
                                    //Tính lại mức hưởng
                                    if (objLuotkham.DungTuyen == 1)
                                    {
                                    }
                                    else
                                    {
                                        if (!Utility.Byte2Bool(item["noitru"]))//Ngoại trú trái tuyến
                                        {
                                            PtramBhyt = 0;
                                        }
                                        else//Nội trú trái tuyến
                                        {
                                            PtramBhyt = (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                                        }
                                    }

                                    if (!Utility.Byte2Bool(item["tu_tuc"]))
                                    {
                                        bhyt_gia_tyle = don_gia * tyle_tt / 100;
                                        bhyt_cct = bhyt_gia_tyle * PtramBhyt/100;
                                        bn_cct = bhyt_gia_tyle - bhyt_cct;
                                        bn_ttt = don_gia - bhyt_gia_tyle;
                                    }
                                    else//Tự túc hoặc dịch vụ
                                    {
                                        bhyt_gia_tyle = 0;
                                        bhyt_cct = 0;
                                        bn_cct = 0;
                                        bn_ttt = 0;
                                    }
                                }
                                bnhan_chitra = don_gia - bhyt_cct;// hoặc =bn_cct+bn_ttt
                                if (Utility.Byte2Bool(item["trong_goi"]))
                                {
                                    bhyt_gia_tyle = 0;
                                    bhyt_cct = 0;
                                    bn_cct = 0;
                                    bn_ttt = 0;
                                    bnhan_chitra = 0;
                                    PtramBhyt = 0;
                                }
                                new Update(KcbDonthuocChitiet.Schema)
                                    .Set(KcbDonthuocChitiet.Columns.BnhanChitra).EqualTo(bnhan_chitra)
                                    .Set(KcbDonthuocChitiet.Columns.BhytChitra).EqualTo(bhyt_cct)
                                    .Set(KcbDonthuocChitiet.Columns.BnCct).EqualTo(bn_cct)
                                    .Set(KcbDonthuocChitiet.Columns.BnTtt).EqualTo(bn_ttt)
                                    .Set(KcbDonthuocChitiet.Columns.BhytGiaTyle).EqualTo(bhyt_gia_tyle)
                                    .Set(KcbDonthuocChitiet.Columns.PtramBhyt).EqualTo(PtramBhyt)
                                    .Set(KcbDonthuocChitiet.Columns.PtramBhytGoc).EqualTo(objLuotkham.PtramBhytGoc)
                                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Utility.Int64Dbnull(item["id"], -1))
                                    .And(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(0)
                                    .Execute();
                            }

                        }
                        if (option == 4 || option == -1)
                        {
                            List<DataRow> lstDr = dtData.Select("loai_thanhtoan=4").ToList<DataRow>();
                            foreach (DataRow item in lstDr)
                            {

                                don_gia = Utility.DecimaltoDbnull(item["don_gia"], 0);
                                phu_thu = Utility.DecimaltoDbnull(item["phu_thu"], 0);
                                tyle_tt = !THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) ? 100 : Utility.DecimaltoDbnull(item["tyle_tt"], 100);

                                if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                                {//Tính lại mức hưởng
                                    if (objLuotkham.DungTuyen == 1)
                                    {
                                    }
                                    else
                                    {
                                        if (!Utility.Byte2Bool(item["noitru"]))//Ngoại trú trái tuyến
                                        {
                                            PtramBhyt = 0;
                                        }
                                        else//Nội trú trái tuyến
                                        {
                                            PtramBhyt = (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                                        }
                                    }

                                    if (!Utility.Byte2Bool(item["tu_tuc"]))
                                    {
                                        bhyt_gia_tyle = don_gia * tyle_tt / 100;
                                        bhyt_cct = bhyt_gia_tyle * PtramBhyt/100;
                                        bn_cct = bhyt_gia_tyle - bhyt_cct;
                                        bn_ttt = don_gia - bhyt_gia_tyle;
                                    }
                                    else//Tự túc hoặc dịch vụ
                                    {
                                        bhyt_gia_tyle = 0;
                                        bhyt_cct = 0;
                                        bn_cct = 0;
                                        bn_ttt = 0;
                                    }
                                }
                                bnhan_chitra = don_gia - bhyt_cct;// hoặc =bn_cct+bn_ttt
                                if (Utility.Byte2Bool(item["trong_goi"]))
                                {
                                    bhyt_gia_tyle = 0;
                                    bhyt_cct = 0;
                                    bn_cct = 0;
                                    bn_ttt = 0;
                                    bnhan_chitra = 0;
                                    PtramBhyt = 0;
                                }
                                new Update(NoitruPhanbuonggiuong.Schema)
                                    .Set(NoitruPhanbuonggiuong.Columns.BnhanChitra).EqualTo(bnhan_chitra)
                                    .Set(NoitruPhanbuonggiuong.Columns.BhytChitra).EqualTo(bhyt_cct)
                                    .Set(NoitruPhanbuonggiuong.Columns.BnCct).EqualTo(bn_cct)
                                    .Set(NoitruPhanbuonggiuong.Columns.BnTtt).EqualTo(bn_ttt)
                                    .Set(NoitruPhanbuonggiuong.Columns.BhytGiaTyle).EqualTo(bhyt_gia_tyle)
                                    .Set(NoitruPhanbuonggiuong.Columns.PtramBhyt).EqualTo(PtramBhyt)
                                    .Set(NoitruPhanbuonggiuong.Columns.PtramBhytGoc).EqualTo(objLuotkham.PtramBhytGoc)
                                    .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(Utility.Int64Dbnull(item["id"], -1))
                                     .And(NoitruPhanbuonggiuong.Columns.TrangthaiThanhtoan).IsEqualTo(0)
                                    .Execute();
                            }

                        }
                    }
                    scope.Complete();
                }
                return ActionResult.Success;
            }
            catch (Exception ex)
            {
                return ActionResult.Exception;
            }
        }
        public static ActionResult UpdatePtramBhyt(KcbLichsuDoituongKcb objThe, int option)
        {
            try
            {
                DataTable dtData = SPs.BhytLaydichvuPhantichgia(objThe.IdBenhnhan, objThe.MaLuotkham, Utility.ByteDbnull(option == -1 ? 100 : option, 200)).GetDataSet().Tables[0];
                using (var scope = new TransactionScope())
                {
                    using (var db = new SharedDbConnectionScope())
                    {
                        decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", true), 0m);
                        bool hasPayment = false;
                        decimal don_gia = 0;
                        decimal phu_thu = 0;
                        decimal PtramBhyt = Utility.DecimaltoDbnull(objThe.PtramBhyt, 0);
                        decimal tyle_tt = 100;
                        decimal bhyt_gia_tyle = 0m;
                        decimal bhyt_cct = 0m;
                        decimal bn_cct = 0m;
                        decimal bn_ttt = 0m;
                        decimal bnhan_chitra = 0m;
                        decimal so_luong = 1;

                        if (option == 1 || option == -1)
                        {
                            List<DataRow> lstDr = dtData.Select("loai_thanhtoan=1").ToList<DataRow>();
                            foreach (DataRow item in lstDr)
                            {

                                don_gia = Utility.DecimaltoDbnull(item["don_gia"], 0);
                                phu_thu = Utility.DecimaltoDbnull(item["phu_thu"], 0);
                                tyle_tt = !THU_VIEN_CHUNG.IsBaoHiem(objThe.IdLoaidoituongKcb) ? 100 : Utility.DecimaltoDbnull(item["tyle_tt"], 100);
                                //Tính lại mức hưởng
                                if (objThe.DungTuyen == 1)
                                {
                                }
                                else
                                {
                                    if (!Utility.Byte2Bool(item["noitru"]))//Ngoại trú trái tuyến
                                    {
                                        PtramBhyt = 0;
                                    }
                                    else//Nội trú trái tuyến
                                    {
                                        PtramBhyt = (Utility.DecimaltoDbnull(objThe.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                                    }
                                }
                                if (THU_VIEN_CHUNG.IsBaoHiem(objThe.IdLoaidoituongKcb))
                                {
                                    if (!Utility.Byte2Bool(item["tu_tuc"]))
                                    {
                                        bhyt_gia_tyle = don_gia * tyle_tt / 100;
                                        bhyt_cct = bhyt_gia_tyle * PtramBhyt/100;
                                        bn_cct = bhyt_gia_tyle - bhyt_cct;
                                        bn_ttt = don_gia - bhyt_gia_tyle;
                                    }
                                    else//Tự túc hoặc dịch vụ
                                    {
                                        bhyt_gia_tyle = 0;
                                        bhyt_cct = 0;
                                        bn_cct = 0;
                                        bn_ttt = 0;
                                    }
                                }
                                bnhan_chitra = don_gia - bhyt_cct;// hoặc =bn_cct+bn_ttt
                                new Update(KcbDangkyKcb.Schema)
                                    .Set(KcbDangkyKcb.Columns.BnhanChitra).EqualTo(bnhan_chitra)
                                    .Set(KcbDangkyKcb.Columns.BhytChitra).EqualTo(bhyt_cct)
                                    .Set(KcbDangkyKcb.Columns.BnCct).EqualTo(bn_cct)
                                    .Set(KcbDangkyKcb.Columns.BnTtt).EqualTo(bn_ttt)
                                    .Set(KcbDangkyKcb.Columns.BhytGiaTyle).EqualTo(bhyt_gia_tyle)
                                    .Set(KcbDangkyKcb.Columns.PtramBhyt).EqualTo(PtramBhyt)
                                    .Set(KcbDangkyKcb.Columns.PtramBhytGoc).EqualTo(objThe.PtramBhytGoc)
                                    .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(Utility.Int64Dbnull(item["id"], -1))
                                     .And(KcbDangkyKcb.Columns.TrangthaiThanhtoan).IsEqualTo(0)
                                     .Execute();
                            }
                        }
                        if (option == 2 || option == -1)
                        {
                            List<DataRow> lstDr = dtData.Select("loai_thanhtoan=2").ToList<DataRow>();
                            foreach (DataRow item in lstDr)
                            {

                                don_gia = Utility.DecimaltoDbnull(item["don_gia"], 0);
                                phu_thu = Utility.DecimaltoDbnull(item["phu_thu"], 0);
                                tyle_tt = !THU_VIEN_CHUNG.IsBaoHiem(objThe.IdLoaidoituongKcb) ? 100 : Utility.DecimaltoDbnull(item["tyle_tt"], 100);
                                //Tính lại mức hưởng
                                if (objThe.DungTuyen == 1)
                                {
                                }
                                else
                                {
                                    if (!Utility.Byte2Bool(item["noitru"]))//Ngoại trú trái tuyến
                                    {
                                        PtramBhyt = 0;
                                    }
                                    else//Nội trú trái tuyến
                                    {
                                        PtramBhyt = (Utility.DecimaltoDbnull(objThe.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                                    }
                                }
                                if (THU_VIEN_CHUNG.IsBaoHiem(objThe.IdLoaidoituongKcb))
                                {
                                    if (!Utility.Byte2Bool(item["tu_tuc"]))
                                    {
                                        bhyt_gia_tyle = don_gia * tyle_tt / 100;
                                        bhyt_cct = bhyt_gia_tyle * PtramBhyt/100;
                                        bn_cct = bhyt_gia_tyle - bhyt_cct;
                                        bn_ttt = don_gia - bhyt_gia_tyle;
                                    }
                                    else//Tự túc hoặc dịch vụ
                                    {
                                        bhyt_gia_tyle = 0;
                                        bhyt_cct = 0;
                                        bn_cct = 0;
                                        bn_ttt = 0;
                                    }
                                }
                                bnhan_chitra = don_gia - bhyt_cct;// hoặc =bn_cct+bn_ttt
                                new Update(KcbChidinhclsChitiet.Schema)
                                    .Set(KcbChidinhclsChitiet.Columns.BnhanChitra).EqualTo(bnhan_chitra)
                                    .Set(KcbChidinhclsChitiet.Columns.BhytChitra).EqualTo(bhyt_cct)
                                    .Set(KcbChidinhclsChitiet.Columns.BnCct).EqualTo(bn_cct)
                                    .Set(KcbChidinhclsChitiet.Columns.BnTtt).EqualTo(bn_ttt)
                                    .Set(KcbChidinhclsChitiet.Columns.BhytGiaTyle).EqualTo(bhyt_gia_tyle)
                                    .Set(KcbChidinhclsChitiet.Columns.PtramBhyt).EqualTo(PtramBhyt)
                                    .Set(KcbChidinhclsChitiet.Columns.PtramBhytGoc).EqualTo(objThe.PtramBhytGoc)
                                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(Utility.Int64Dbnull(item["id"], -1))
                                     .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(0)
                                     .Execute();


                            }
                        }
                        if (option == 3 || option == -1)
                        {
                            List<DataRow> lstDr = dtData.Select("loai_thanhtoan=3").ToList<DataRow>();
                            foreach (DataRow item in lstDr)
                            {

                                don_gia = Utility.DecimaltoDbnull(item["don_gia"], 0);
                                phu_thu = Utility.DecimaltoDbnull(item["phu_thu"], 0);
                                tyle_tt = !THU_VIEN_CHUNG.IsBaoHiem(objThe.IdLoaidoituongKcb) ? 100 : Utility.DecimaltoDbnull(item["tyle_tt"], 100);
                                //Tính lại mức hưởng
                                if (objThe.DungTuyen == 1)
                                {
                                }
                                else
                                {
                                    if (!Utility.Byte2Bool(item["noitru"]))//Ngoại trú trái tuyến
                                    {
                                        PtramBhyt = 0;
                                    }
                                    else//Nội trú trái tuyến
                                    {
                                        PtramBhyt = (Utility.DecimaltoDbnull(objThe.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                                    }
                                }
                                if (THU_VIEN_CHUNG.IsBaoHiem(objThe.IdLoaidoituongKcb))
                                {
                                    if (!Utility.Byte2Bool(item["tu_tuc"]))
                                    {
                                        bhyt_gia_tyle = don_gia * tyle_tt / 100;
                                        bhyt_cct = bhyt_gia_tyle * PtramBhyt/100;
                                        bn_cct = bhyt_gia_tyle - bhyt_cct;
                                        bn_ttt = don_gia - bhyt_gia_tyle;
                                    }
                                    else//Tự túc hoặc dịch vụ
                                    {
                                        bhyt_gia_tyle = 0;
                                        bhyt_cct = 0;
                                        bn_cct = 0;
                                        bn_ttt = 0;
                                    }
                                }
                                bnhan_chitra = don_gia - bhyt_cct;// hoặc =bn_cct+bn_ttt
                                new Update(KcbDonthuocChitiet.Schema)
                                    .Set(KcbDonthuocChitiet.Columns.BnhanChitra).EqualTo(bnhan_chitra)
                                    .Set(KcbDonthuocChitiet.Columns.BhytChitra).EqualTo(bhyt_cct)
                                    .Set(KcbDonthuocChitiet.Columns.BnCct).EqualTo(bn_cct)
                                    .Set(KcbDonthuocChitiet.Columns.BnTtt).EqualTo(bn_ttt)
                                    .Set(KcbDonthuocChitiet.Columns.BhytGiaTyle).EqualTo(bhyt_gia_tyle)
                                    .Set(KcbDonthuocChitiet.Columns.PtramBhyt).EqualTo(PtramBhyt)
                                    .Set(KcbDonthuocChitiet.Columns.PtramBhytGoc).EqualTo(objThe.PtramBhytGoc)
                                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Utility.Int64Dbnull(item["id"], -1))
                                    .And(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(0)
                                    .Execute();
                            }

                        }
                        if (option == 4 || option == -1)
                        {
                            List<DataRow> lstDr = dtData.Select("loai_thanhtoan=4").ToList<DataRow>();
                            foreach (DataRow item in lstDr)
                            {

                                don_gia = Utility.DecimaltoDbnull(item["don_gia"], 0);
                                phu_thu = Utility.DecimaltoDbnull(item["phu_thu"], 0);
                                tyle_tt = !THU_VIEN_CHUNG.IsBaoHiem(objThe.IdLoaidoituongKcb) ? 100 : Utility.DecimaltoDbnull(item["tyle_tt"], 100);
                                //Tính lại mức hưởng
                                if (objThe.DungTuyen == 1)
                                {
                                }
                                else
                                {
                                    if (!Utility.Byte2Bool(item["noitru"]))//Ngoại trú trái tuyến
                                    {
                                        PtramBhyt = 0;
                                    }
                                    else//Nội trú trái tuyến
                                    {
                                        PtramBhyt = (Utility.DecimaltoDbnull(objThe.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                                    }
                                }
                                if (THU_VIEN_CHUNG.IsBaoHiem(objThe.IdLoaidoituongKcb))
                                {
                                    if (!Utility.Byte2Bool(item["tu_tuc"]))
                                    {
                                        bhyt_gia_tyle = don_gia * tyle_tt / 100;
                                        bhyt_cct = bhyt_gia_tyle * PtramBhyt/100;
                                        bn_cct = bhyt_gia_tyle - bhyt_cct;
                                        bn_ttt = don_gia - bhyt_gia_tyle;
                                    }
                                    else//Tự túc hoặc dịch vụ
                                    {
                                        bhyt_gia_tyle = 0;
                                        bhyt_cct = 0;
                                        bn_cct = 0;
                                        bn_ttt = 0;
                                    }
                                }
                                bnhan_chitra = don_gia - bhyt_cct;// hoặc =bn_cct+bn_ttt
                                new Update(NoitruPhanbuonggiuong.Schema)
                                    .Set(NoitruPhanbuonggiuong.Columns.BnhanChitra).EqualTo(bnhan_chitra)
                                    .Set(NoitruPhanbuonggiuong.Columns.BhytChitra).EqualTo(bhyt_cct)
                                    .Set(NoitruPhanbuonggiuong.Columns.BnCct).EqualTo(bn_cct)
                                    .Set(NoitruPhanbuonggiuong.Columns.BnTtt).EqualTo(bn_ttt)
                                    .Set(NoitruPhanbuonggiuong.Columns.BhytGiaTyle).EqualTo(bhyt_gia_tyle)
                                    .Set(NoitruPhanbuonggiuong.Columns.PtramBhyt).EqualTo(PtramBhyt)
                                    .Set(NoitruPhanbuonggiuong.Columns.PtramBhytGoc).EqualTo(objThe.PtramBhytGoc)
                                    .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(Utility.Int64Dbnull(item["id"], -1))
                                     .And(NoitruPhanbuonggiuong.Columns.TrangthaiThanhtoan).IsEqualTo(0)
                                    .Execute();
                            }

                        }
                    }
                    scope.Complete();
                }
                return ActionResult.Success;
            }
            catch (Exception ex)
            {
                return ActionResult.Exception;
            }
        }

        public static decimal TinhBhytChitra(decimal phanTramBh, decimal originPrice)
        {
            return phanTramBh * originPrice / 100;
        }
        public static decimal TinhBhytChitra(decimal phanTramBh, decimal originPrice, int isPayment)
        {
            if (isPayment == 0)
                return phanTramBh * originPrice / 100;
            else
            {
                return 0;
            }
        }
        public static decimal TinhBnhanChitra(decimal PhanTramBH, decimal Origin_Price)
        {
            return (100 - PhanTramBH) * Origin_Price / 100;
        }
        public static decimal TinhBnhanChitra(decimal PhanTramBH, decimal Origin_Price, int IsPayment)
        {
            if (IsPayment == 0)
                return (100 - PhanTramBH) * Origin_Price / 100;
            else
            {
                return Origin_Price;
            }
        }
        public static string SinhMaChidinhCLS()
        {
            string BarCode = "";
            DataTable dataTable = new DataTable();
            dataTable =
                SPs.KcbThamkhamSinhmachidinhcls(DateTime.Now, globalVariables.MA_KHOA_THIEN,
                    globalVariables.MIN_STT, globalVariables.MAX_STT).GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                BarCode = Utility.sDbnull(dataTable.Rows[0]["BARCODE"], "");
            }
            return BarCode;
        }
        public static string SinhMaChidinhKiemNghiem()
        {
            string BarCode = "";
            DataTable dataTable = new DataTable();
            dataTable =
                SPs.KnSinhmachidinhcls(DateTime.Now, globalVariables.MA_KHOA_THIEN,
                    globalVariables.MIN_STT, globalVariables.MAX_STT).GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                BarCode = Utility.sDbnull(dataTable.Rows[0]["BARCODE"], "");
            }
            return BarCode;
        }
        public static string SinhMaChidinhCLSKSK()
        {
            string BarCode = "";
            DataTable dataTable = new DataTable();
            dataTable =
                SPs.KcbThamkhamSinhmachidinhclsKsk(DateTime.Now, globalVariables.MA_KHOA_THIEN,
                    globalVariables.MIN_STT, globalVariables.MAX_STT).GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                BarCode = Utility.sDbnull(dataTable.Rows[0]["BARCODE"], "");
            }
            return BarCode;
        }
        public static string SinhLaiMaChidinhCLS()
        {
            string BarCode = "";
            DataTable dataTable = new DataTable();
            dataTable = SPs.KcbThamkhamSinhlaimachidinhcls(DateTime.Now, globalVariables.MA_KHOA_THIEN, globalVariables.MIN_STT, globalVariables.MAX_STT).GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                BarCode = Utility.sDbnull(dataTable.Rows[0]["BARCODE"], "");
            }
            return BarCode;
        }
        /// <summary>
        /// Bỏ
        /// </summary>
        /// <param name="dtdangkyKcb"></param>
        /// <param name="objDangky"></param>
        public static void TinhlaiGiaChiphiKcb_delete(DataTable dtdangkyKcb, ref KcbDangkyKcb objDangky)
        {
            try
            {
                if (dtdangkyKcb == null || objDangky == null) return;
                int SttTt37 = dtdangkyKcb.Rows.Count + 1;
                objDangky.SttTt37 = (byte)SttTt37;
                if (!THU_VIEN_CHUNG.IsBaoHiem(objDangky.IdLoaidoituongkcb)) { objDangky.TyleTt = 100; }
                else
                {
                    if (SttTt37 == 1)
                        objDangky.TyleTt = 100;
                    else if (SttTt37 <= 4)
                        objDangky.TyleTt = 30;
                    else if (SttTt37 == 5)
                        objDangky.TyleTt = 10;
                    else
                        objDangky.TyleTt = 0;
                }
                //DataRow[] arrDr = dtdangkyKcb.Select("1=1", KcbDangkyKcb.Columns.IdKham);
                //decimal totalMoney = Utility.DecimaltoDbnull(dtdangkyKcb.Compute("SUM(TT_KPT)", "1=1"), 0);
                //if (arrDr.Length > 0)
                //{
                //    decimal firstPrice = Utility.DecimaltoDbnull(arrDr[0][KcbDangkyKcb.Columns.DonGia], 0);
                //    if (totalMoney < firstPrice * 2)
                //    //Nếu tổng tiền khám chưa >= 200% tiền khám của phòng đầu tiên thì mới tính tiền khám cho phòng này
                //    {
                //        decimal donGiaConlai = firstPrice * 2 - totalMoney;
                //        decimal donGia30 = firstPrice * 0.3m; //Giá=30% giá khám lần đầu
                //        if (donGia30 > donGiaConlai)
                //        {
                //            objDangky.TyleTt = 10;
                //            // objDangky.DonGia = donGiaConlai;
                //        }

                //        else
                //        {
                //            objDangky.TyleTt = 30;
                //            //objDangky.DonGia = donGia30;
                //        }

                //    }
                //    else
                //    {
                //        if (firstPrice > 0)
                //            objDangky.TyleTt = 0;
                //        else
                //            objDangky.TyleTt = 100;
                //        //  objDangky.DonGia = 0;
                //    }


                //}
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        public static void TinhlaiGiaChiphiKcb_bak(DataTable dtdangkyKcb, ref KcbDangkyKcb objDangky)
        {
            try
            {
                if (dtdangkyKcb == null || objDangky == null) return;
                int SttTt37 = dtdangkyKcb.Rows.Count + 1;
                objDangky.SttTt37 = (byte)SttTt37;
                //if (!THU_VIEN_CHUNG.IsBaoHiem(objDangky.IdLoaidoituongkcb)) return;
                DataRow[] arrDr = dtdangkyKcb.Select("1=1", KcbDangkyKcb.Columns.IdKham);
                decimal totalMoney = Utility.DecimaltoDbnull(dtdangkyKcb.Compute("SUM(TT_KPT)", "1=1"), 0);
                if (arrDr.Length > 0)
                {
                    decimal firstPrice = Utility.DecimaltoDbnull(arrDr[0][KcbDangkyKcb.Columns.DonGia], 0);
                    if (totalMoney < firstPrice * 2)
                    //Nếu tổng tiền khám chưa >= 200% tiền khám của phòng đầu tiên thì mới tính tiền khám cho phòng này
                    {
                        decimal donGiaConlai = firstPrice * 2 - totalMoney;
                        decimal donGia30 = firstPrice * 0.3m; //Giá=30% giá khám lần đầu
                        if (donGia30 > donGiaConlai)
                        {
                            objDangky.TyleTt = 10;
                            // objDangky.DonGia = donGiaConlai;
                        }

                        else
                        {
                            objDangky.TyleTt = 30;
                            //objDangky.DonGia = donGia30;
                        }

                    }
                    else
                    {
                        if (firstPrice > 0)
                            objDangky.TyleTt = 0;
                        else
                            objDangky.TyleTt = 100;
                        //  objDangky.DonGia = 0;
                    }


                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        public static bool IsNgoaiGio()
        {
            try
            {
                return false;
                GetTrongNgayTrongGio();
                //Kiểm tra ngày hiện tại có trong tham biến không
                if (KT_TRONGNGAY())
                {
                    // Nếu có trong ngày kiểm tra giờ hiện tại có trong giờ ko
                    if (!Utility.IsBetweenManyTimeranges(DateTime.Now, globalVariables.TrongGio))
                    {
                        //Nếu giờ hiện tại không trong giờ tham biến trả về true. Ngoài giờ khám
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
        public static void GetTrongNgayTrongGio()
        {
            globalVariables.TrongGio = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TRONGGIO", "0:00-23:59", false);
            globalVariables.TrongNgay = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TRONGNGAY", "2,3,4,5,6,7,CN", false);
        }

        /// <summary>
        /// Kiểm tra so sánh ngày hiện tại với các ngày trong biến TRONGNGAY
        /// </summary>
        /// <returns></returns>
        static bool KT_TRONGNGAY()
        {
            try
            {
                string[] TrongNgay = globalVariables.TrongNgay.Split(',');
                if (TrongNgay.Length > 0)
                {
                    //So sánh giá trị từng ngày trong mảng.
                    foreach (string s in TrongNgay)
                    {
                        switch (s)
                        {
                            //Thứ 2 : giá trị so sánh = 1;
                            case "2":
                                //Nếu so sánh ngày bằng nhau thì trả về true
                                if (_SoSanhNgay(1))
                                {
                                    return true;
                                }
                                break;
                            //Thứ 3 : giá trị so sánh = 2;
                            case "3":
                                if (_SoSanhNgay(2))
                                {
                                    return true;
                                }
                                break;
                            //Thứ 4 : giá trị so sánh = 3;
                            case "4":
                                if (_SoSanhNgay(3))
                                {
                                    return true;
                                }
                                break;
                            //Thứ 5 : giá trị so sánh = 4;
                            case "5":
                                if (_SoSanhNgay(4))
                                {
                                    return true;
                                }
                                break;
                            //Thứ 6 : giá trị so sánh = 5;
                            case "6":
                                if (_SoSanhNgay(5))
                                {
                                    return true;
                                }
                                break;
                            //Thứ 7 : giá trị so sánh = 6;
                            case "7":
                                if (_SoSanhNgay(6))
                                {
                                    return true;
                                }
                                break;
                            //Thứ CN : giá trị so sánh = 0;
                            case "CN":
                                if (_SoSanhNgay(0))
                                {
                                    return true;
                                }
                                break;
                        }
                    }
                    //Nếu hết các giá trị trong mảng ko có giá trị nào bằng ngày hiện tại thì trả về false
                    return false;
                }
                //Nếu mảng giá trị nhỏ hơn không là ko có tham biến thì trả về true
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }
        /// <summary>
        /// KIểm tra so sánh ngày trong biến truyền vào với ngày hiện tại. Nếu bằng nhau thì trả về true else false
        /// </summary>
        /// <param name="Ngay"></param>
        /// <returns></returns>
        static bool _SoSanhNgay(int Ngay)
        {
            try
            {
                return (int)GetSysDateTime().DayOfWeek == Ngay;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static DataTable Get_PHONGKHAM(string MA_DTUONG)
        {
            return SPs.DmucLaydsachPhongkham(MA_DTUONG, globalVariables.idKhoatheoMay).GetDataSet().Tables[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MA_DTUONG"></param>
        /// <param name="dongia">Nếu không muốn tìm theo đơn giá thì truyền giá trị -1</param>
        /// <returns></returns>
        public static DataTable Get_KIEUKHAM(string MA_DTUONG, decimal dongia)
        {
            return SPs.DmucLaydsachKieukham(MA_DTUONG, globalVariables.idKhoatheoMay, dongia).GetDataSet().Tables[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MA_DTUONG"></param>
        /// <param name="dongia">Nếu không muốn tìm theo đơn giá thì truyền giá trị -1</param>
        /// <returns></returns>
        public static DataTable LayDsach_Dvu_KCB(string MA_DTUONG, string madichvukcb, decimal dongia, int id_dangkygoi)
        {
            return SPs.DmucLaythongtinDvuKcb(MA_DTUONG, globalVariables.MA_KHOA_THIEN, madichvukcb, dongia, id_dangkygoi,0).GetDataSet()
                         .Tables[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MA_DTUONG"></param>
        /// <param name="dongia">Nếu không muốn tìm theo đơn giá thì truyền giá trị -1</param>
        /// <returns></returns>
        public static DataTable LayDsach_Dvu_KCB(string MA_DTUONG, string madichvukcb, decimal dongia, int id_dangkygoi,byte khamthiluc)
        {
            return SPs.DmucLaythongtinDvuKcb(MA_DTUONG, globalVariables.MA_KHOA_THIEN, madichvukcb, dongia, id_dangkygoi, khamthiluc).GetDataSet()
                         .Tables[0];
        }
        public static int LaySTTKhamTheoDoituong(Int16 ObjectTypeType)
        {
            int So_ThuTu = 0;
            DataTable dataTable = new DataTable();
            dataTable =
                SPs.LaySTTKham(DateTime.Now, Utility.Int32Dbnull(ObjectTypeType, -1), GetThamSo_ThuTu()).GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                So_ThuTu = Utility.Int32Dbnull(dataTable.Rows[0]["STT"], 0) + 1;
            }
            else
            {
                So_ThuTu = 1;
            }
            return So_ThuTu;
        }
        public static string GetThamSo_ThuTu()
        {
            string thamso = "THANG";
            SqlQuery sqlQuery =
                new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo(
                    "STT_KHAM");
            SysSystemParameter objSystemParameter = sqlQuery.ExecuteSingle<SysSystemParameter>();
            if (objSystemParameter != null) thamso = objSystemParameter.SValue;
            return thamso;
        }
        public static string GetMaPhieuThu(DateTime dateTime, int LoaiPhieu)
        {

            return Utility.sDbnull(SPs.TaoMaPhieuthu(dateTime, LoaiPhieu).ExecuteScalar<string>(), "");
        }
        public static short LaySothutuKCB(int Department_ID)
        {
            short So_kham = 0;
            DataTable dataTable = new DataTable();
            dataTable =
                SPs.KcbTiepdonLaysothutuKcb(Department_ID, DateTime.Now).GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                So_kham = (short)(Utility.Int16Dbnull(dataTable.Rows[0]["So_Kham"], 0));
            }
            else
            {
                So_kham = 1;
            }
            return So_kham;
        }
        public static string LayMaDviLamViec()
        {
            return "NO";
        }
        public static string Chuanhoadauthapphan(string s)
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
        public static bool IsBaoHiem(byte IdLoaidoituongKcb)
        {
            return IdLoaidoituongKcb == (byte)0;
        }
        public static string laytenthuoc_vattu(string KIEU_THUOC_VT)
        {
            return KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư";
        }
        public static void Phantichdongia(KcbLuotkham objluotkham, GridEXRow GEXRow, DataRow row)
        {

        }
        public static void Bhyt_TudongCapnhatSTT_TyleTT_Congkham(KcbLuotkham objLuotkham)
        {
            try
            {
               DataTable dtDataCheck = Utility.ExecuteSql(string.Format("select * from kcb_dangky_kcb where id_benhnhan={0} and ma_luotkham='{1}' order by ngay_dangky asc", objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham), CommandType.Text).Tables[0];
                byte STT = 1;
                decimal tyle_tt = 100;
                long id_kham = 0;
                KcbDangkyKcb objCK = null;
                int num = 0;
                foreach (DataRow dr in dtDataCheck.Rows)
                {
                    id_kham = Utility.Int64Dbnull(dr["id_kham"]);
                    tyle_tt = THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) ? THU_VIEN_CHUNG.Bhyt_Laytyle_tt_congkham(THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb), STT) : 100;
                    //Tìm công khám có STT hoán đổi với công khám này
                    objCK = KcbDangkyKcb.FetchByID(id_kham);
                    if (objCK != null && objCK.TrangthaiThanhtoan!=1)
                    {
                        objCK.TyleTt = tyle_tt;
                        objCK.SttTt37 = STT;
                        THU_VIEN_CHUNG.Bhyt_PhantichGiaCongkham(objLuotkham, objCK);
                        num = new Update(KcbDangkyKcb.Schema)
                        .Set(KcbDangkyKcb.Columns.SttTt37).EqualTo(objCK.SttTt37)
                    .Set(KcbDangkyKcb.Columns.BhytChitra).EqualTo(objCK.BhytChitra)
                    .Set(KcbDangkyKcb.Columns.BhytGiaTyle).EqualTo(objCK.BhytGiaTyle)
                    .Set(KcbDangkyKcb.Columns.BnCct).EqualTo(objCK.BnCct)
                    .Set(KcbDangkyKcb.Columns.BnTtt).EqualTo(objCK.BnTtt)
                    .Set(KcbDangkyKcb.Columns.BnhanChitra).EqualTo(objCK.BnhanChitra)
                    .Set(KcbDangkyKcb.Columns.TyleTt).EqualTo(objCK.TyleTt)
                    .Set(KcbDangkyKcb.Columns.PtramBhyt).EqualTo(objCK.PtramBhyt)
                    .Set(KcbDangkyKcb.Columns.PtramBhytGoc).EqualTo(objCK.PtramBhytGoc)
                   .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objCK.IdKham)
                   .Execute();
                    }
                }
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        public static decimal Bhyt_Laytyle_tt_congkham(bool isBHYT, byte? STT)
        {
            if (!isBHYT) return 100m;
            STT = Utility.ByteDbnull(STT, 1);
            if (STT == 1)
                return 100m;
            else if (STT <= 4)
                return 30m;
            else if (STT == 5)
                return 10m;
            else
                return 0m;
            return 100m;

        }
        public static bool IsBaoHiem(byte? IdLoaidoituongKcb)
        {
            return Utility.ByteDbnull(IdLoaidoituongKcb, 1) == (byte)0;
        }
        public static string LaySoBenhAn()
        {
            string SoBenhAn = "";
            DataTable dataTable = SPs.TaoBenhAn(DateTime.Now).GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0) SoBenhAn = Utility.sDbnull(dataTable.Rows[0]["SoBA"], string.Empty);

            return SoBenhAn;
        }
        public static string Laysoravien()
        {
            string Soravien = "";
            DataTable dataTable = SPs.NoitruTaosoravien().GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0) Soravien = Utility.sDbnull(dataTable.Rows[0]["Soravien"], string.Empty);

            return Soravien;
        }
        public static string LaysoVaovien()
        {
            string Sovaovien = "";
            DataTable dataTable = SPs.NoitruTaosovaovien(DateTime.Now).GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0) Sovaovien = Utility.sDbnull(dataTable.Rows[0]["SoBA"], string.Empty);

            return Sovaovien;
        }
        public static string KCB_SINH_MALANKHAM(byte loai)
        {
            string Maxma_luotkham = "";
            StoredProcedure sp = SPs.KcbSinhMalankham(globalVariables.UserName,
                Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_SOLUONGSINH_MALUOTKHAM", "200", false)),
                Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_GIOIHAN_TUDONGSINH_MALUOTKHAM", "100", false)), loai,
                Maxma_luotkham);
            sp.Execute();
            sp.OutputValues.ForEach(delegate(object objOutput)
            {
                Maxma_luotkham = (String)objOutput;
            });
            return Maxma_luotkham;
        }
        public static string KN_SINH_MADANGKY(byte loai)
        {
            string Maxma_luotkham = "";
            StoredProcedure sp = SPs.KnSinhMadangky(globalVariables.UserName,
                Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_SOLUONGSINH_MALUOTKHAM", "200", false)),
                Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_GIOIHAN_TUDONGSINH_MALUOTKHAM", "100", false)), loai,
                Maxma_luotkham);
            sp.Execute();
            sp.OutputValues.ForEach(delegate(object objOutput)
            {
                Maxma_luotkham = (String)objOutput;
            });
            return Maxma_luotkham;
        }
        public static string GetThanhToan_TraiTuyen()
        {
            string sPaymentFlow = "";
            SqlQuery sqlQuery = new Select().From(SysSystemParameter.Schema)
                .Where(SysSystemParameter.Columns.SName).IsEqualTo("TRAITUYEN");
            SysSystemParameter objSystemParameter = sqlQuery.ExecuteSingle<SysSystemParameter>();
            if (objSystemParameter != null) sPaymentFlow = objSystemParameter.SValue;
            return sPaymentFlow;
        }
        public static string TaoMathanhtoan(DateTime dateTime)
        {
            DataTable dataTable = new DataTable();
            dataTable = SPs.TaoMathanhtoan(dateTime).GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0) return dataTable.Rows[0][KcbThanhtoan.Columns.MaThanhtoan].ToString();
            else
            {
                return "";
            }
        }
        public static string TaomaDongChiTra(DateTime dateTime)
        {
            DataTable dataTable = new DataTable();
            dataTable = SPs.TaoMadongchitra(dateTime).GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                return dataTable.Rows[0][KcbPhieuDct.Columns.MaPhieuDct].ToString();
            }
            else
            {
                return "";
            }
        }
        public static string MaKieuThanhToan(int PaymentType_ID)
        {
            string MaKieu = "";
            switch (PaymentType_ID)
            {
                case 0:
                    MaKieu = "PHI_DVYC";
                    break;
                case 1:
                    MaKieu = "KHAM";
                    break;
                case 2:
                    MaKieu = "CLS";
                    break;
                case 3:
                    MaKieu = "THUOC";
                    break;
                case 4:
                    MaKieu = "GIUONG";
                    break;
                case 5:
                    MaKieu = "VT";
                    break;
                case 6:
                    MaKieu = "TAMUNG";
                    break;
                case 7:
                    MaKieu = "PHIEU_AN";
                    break;
                case 8:
                    MaKieu = "GOIDV";
                    break;
                case 9:
                    MaKieu = "CPTHEM";
                    break;
                case 10:
                    MaKieu = "SO_KHAM";
                    break;
                case 11:
                    MaKieu = "CONG_TIEM";
                    break;
            }
            return MaKieu;
        }
        public static bool KiemtraDungtuyenTraituyen(string vClinicCode)
        {
            DataTable dataTable = new DataTable();
            dataTable = SPs.DmucLaydanhsachNoikcbbd(vClinicCode).GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                SqlQuery q = new SqlQuery().From(SysSystemParameter.Schema)
                    .Where(SysSystemParameter.Columns.SName).IsEqualTo("BHYT_NOIDANGKY_KCBBD");
                if (q.GetRecordCount() > 0)
                {
                    SysSystemParameter objParameter = q.ExecuteSingle<SysSystemParameter>();
                    if (objParameter.SValue.Contains(vClinicCode)) return true;
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }


            }

            else
            {
                return true;
            }
        }
        /// <summary>
        /// Hàm thực hiện lấy danh sách bệnh viện
        /// </summary>
        /// <returns></returns>
        public static DataTable LayDanhSachBenhVien()
        {
            DataTable dtBenhVien = new Select().From(DmucBenhvien.Schema).ExecuteDataSet().Tables[0];
            return dtBenhVien;
        }
    }

}
namespace Security
{
  
    /// <summary>
    /// Generates a 16 byte Unique Identification code of a computer
    /// Example: 4876-8DB5-EE85-69D3-FE52-8CF7-395D-2EA9
    /// </summary>
    public class HardWare
    {
        private static string fingerPrint = string.Empty;
       
        public static string GetKey(string Value)
        {
            string reval = "YOURHARDKEY:" + Value;
            return GetHash(reval);
        }

        private static string GetHash(string s)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);
            return GetHexString(sec.ComputeHash(bt));
        }
        private static string GetHexString(byte[] bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Length; i++)
            {
                byte b = bt[i];
                int n, n1, n2;
                n = (int)b;
                n1 = n & 15;
                n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + (int)'A')).ToString();
                else
                    s += n2.ToString();
                if (n1 > 9)
                    s += ((char)(n1 - 10 + (int)'A')).ToString();
                else
                    s += n1.ToString();
                if ((i + 1) != bt.Length && (i + 1) % 2 == 0) s += "-";
            }
            return s;
        }
    }

}
