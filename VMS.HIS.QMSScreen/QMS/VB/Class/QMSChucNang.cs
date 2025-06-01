using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VNS.Libs;

using NLog.Config;
using NLog.Targets;
using NLog;
using VNS.Properties;

namespace VMS.QMS.Class
{
    public class QMSChucNang
    {
       public VMS.HIS.QMSScreen.QMSService.Service1 QMSSer = new VMS.HIS.QMSScreen.QMSService.Service1();
        public bool UsingWS = false;
        Logger _log;
        #region noWS
        //public bool _UpdateStatusQms(string patientCode, string maPhong, DateTime ngayTao, int trangThai)
        //{
        //    try
        //    {
        //        return QMSSer.UpdateStatusQms(patientCode, maPhong, ngayTao, Utility.Int32Dbnull(trangThai));
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Trace(ex.ToString());
        //        return false;
        //    }
        //}
        //public int _GetSoKhamQmsChucNang(string patientCode, string maPhong, DateTime ngayTao, int trangThai)
        //{
        //    try
        //    {
        //       return QMSSer.GetSoKhamQmsChucNang(patientCode, maPhong, ngayTao, trangThai);
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Trace(ex.ToString());
        //        return 0;
        //    }
        //}
        //public DataTable _GetListQmSbyMaPhong(string maPhong, string machucnang, DateTime ngayTao, int trangThai, string maKhoa)
        //{
        //    try
        //    {
        //        var dt = new DataTable();
        //        dt = QMSSer.GetListQmSbyMaPhong(maPhong, machucnang, ngayTao, trangThai, maKhoa);
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Trace(ex.ToString());
        //        return null;
        //    }
        //}

        //public void _InsertGoiLoa(string soKham, string quayGoi, string mayGoi, string maKhoa, int trangThai, string nguoiTao, DateTime ngayTao, string mayTao, string loaGoi, string noiDung)
        //{
        //    try
        //    {
        //        QMSSer.InsertGoiLoa(soKham, quayGoi, mayGoi, maKhoa, trangThai, nguoiTao, ngayTao, mayTao, loaGoi, noiDung);
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Trace(ex.ToString());

        //    }
        //}
        //public DataTable _GetNoiThucHien()
        //{
        //    return QMSSer._GetNoiThucHien();
        //}
        //public DataTable _getQMSInfor(string patientcode, string maphong)
        //{
        //    try
        //    {
        //        var dt = new DataTable();
        //        dt = QMSSer.getQMSInfor(patientcode, maphong);
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Trace(ex.ToString());
        //        return null;
        //    }
        //}
        //public bool _ChangeQMSPriority(string patientcode, string maphong, byte tthai)
        //{
        //    try
        //    {
        //        QMSSer.ChangeQMSPriority(patientcode, maphong, tthai).Execute();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Trace(ex.ToString());
        //        return false;
        //    }
        //}
        //public bool _DeleteQMS(int QMSID)
        //{
        //    try
        //    {
        //        SPs.QmsDeleteQMS(QMSID).Execute();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Trace(ex.ToString());
        //        return false;
        //    }
        //}
        //public bool _ChangeQMSStatus(string patientcode, string maphong, string machucnang, string makhoa, byte _type)
        //{
        //    try
        //    {
        //        SPs.QmsChangeQMSStatus(patientcode, maphong, machucnang, makhoa, _type).Execute();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Trace(ex.ToString());
        //        return false;
        //    }
        //}
        #endregion

        #region VMS
      
        public DataTable VmsQmsLaysoQMSGoilai(string maquay, string makhoa, string loaiqms)
        {
            try
            {
                return QMSSer.VmsQmsLaysoQMSGoilai(maquay, makhoa, loaiqms);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
      
        public DataTable VmsQmsLaydanhsachbenhnhanchokham(string MaPhongKham, string makhoa, int sluong_hthi)
        {
            try
            {
                return QMSSer.VmsQmsLaydanhsachbenhnhanchokham(MaPhongKham, makhoa, sluong_hthi);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
      
        public void ResetQMS(long id, string MaQuay, int trang_thai)
        {
            try
            {
                QMSSer.ResetQMS(id, MaQuay, trang_thai);
            }
            catch (Exception)
            {

                throw;
            }


        }
        #endregion

        public QMSChucNang()
        {
            InitLogs();
            _log = LogManager.GetLogger("QMSLog");
            _log.Trace("Start QMS log.......");
            globalVariables.m_strPropertiesFolderQMS = Application.StartupPath + @"\CauHinh_QMS";
            _qmspro = PropertyLib.GetQMSPrintProperties(Application.StartupPath + @"\CauHinh_QMS");
            QMSSer.Url = THU_VIEN_CHUNG.Laygiatrithamsohethong("QMSPK_URL","",true);// _qmspro.QMSServiceURL;
            if (Utility.DoTrim(QMSSer.Url).Length <= 0)
                Utility.ShowMsg("Bạn cần cấu hình đường dẫn Service chạy QMS phòng khám bằng tham số hệ thống QMSPK_URL.\nVui lòng kiểm tra lại");
            
        }
        void InitLogs()
        {
            try
            {
                var config = new LoggingConfiguration();
                var fileTarget = new FileTarget();
                config.AddTarget("file", fileTarget);
                fileTarget.FileName =
                    "${basedir}/Mylogs/${date:format=yyyy}/${date:format=MM}/${date:format=dd}/${logger}.log";
                fileTarget.Layout = "${date:format=HH\\:mm\\:ss}|${threadid}|${level}|${logger}|${message}";
                config.LoggingRules.Add(new LoggingRule("*", NLog.LogLevel.Trace, fileTarget));
                LogManager.Configuration = config;
            }
            catch (Exception ex)
            {
                _log.Trace(ex.ToString());
            }
        }

        public DataTable GetNoiThucHien()
        {
            try
            {
                //if (!UsingWS)
                //    return _GetNoiThucHien();
                //else

                    return QMSSer.GetNoiThucHien();
            }
            catch (Exception ex)
            {
                _log.Trace(ex.ToString());
                return null;
            }
        }
        public DataTable getQMSInfor(string patientcode, string maphong)
        {
            try
            {
                //if (!UsingWS)
                //    return _getQMSInfor(patientcode, maphong);
                //else

                    return QMSSer.getQMSInfor(patientcode, maphong);
                return null;
            }
            catch (Exception ex)
            {
                _log.Trace(ex.ToString());
                return null;
            }
        }
        public bool ChangeQMSPriority(string patientcode, string maphong, byte tthai)
        {
            bool result = false;
            string msg = "";
            try
            {
                //if (!UsingWS)
                //    result = _ChangeQMSPriority(patientcode, maphong, tthai);
                //else
                    result = QMSSer.ChangeQMSPriority(patientcode, maphong, tthai);
                _log.Trace(msg);
                return result;
            }
            catch (Exception ex)
            {
                _log.Trace(ex.ToString());
                //Utility.ShowMsg(ex.Message);
                return false;
            }         
        }
        public bool ChangeQMSStatus(string patientcode, string maphong, string machucnang, string makhoa, byte _type)
        {
            bool result = false;
            string msg = "";
            try
            {
                //if (!UsingWS)
                //    result = _ChangeQMSStatus(patientcode, maphong,machucnang,makhoa, _type);
                //else
                    result = QMSSer.ChangeQMSStatus(patientcode, maphong, machucnang, makhoa, _type);
                _log.Trace(msg);
                return result;
            }
            catch (Exception ex)
            {
                _log.Trace(ex.ToString());
                //Utility.ShowMsg(ex.Message);
                return false;
            }
        }
        public bool DeleteQms(int QMS_ID)
        {
            bool result = false;
            string msg = "";
            try
            {
                //if (!UsingWS)
                //    result = _DeleteQMS(QMS_ID);
                //else
                    result = QMSSer.DeleteQMS(QMS_ID);
                _log.Trace(msg);
                return result;
            }
            catch (Exception ex)
            {
                _log.Trace(ex.ToString());
                //Utility.ShowMsg(ex.Message);
                return false;
            }
        }
        public bool UpdateStatusQms(string patientCode, string maPhong, DateTime ngayTao, int trangThai)
        {
            bool result = false;
            string msg = "";
            try
            {
                //if (!UsingWS)
                //    result = _UpdateStatusQms(patientCode, maPhong, ngayTao, trangThai);
                //else
                    result = QMSSer.UpdateStatusQms(patientCode, maPhong, ngayTao, trangThai);
                _log.Trace(msg);
                return result;
            }
            catch (Exception ex)
            {
                _log.Trace(ex.ToString());
                //Utility.ShowMsg(ex.Message);
                return false;
            }            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idkham"></param>
        /// <param name="id"></param>
        /// <param name="trangThai"></param>
        /// <param name="QMSType">0= QMS phòng khám;1= QMS phòng chức năng</param>
        /// <returns></returns>
        public bool QmsPK_CapnhatTrangthai(long idkham, long id, int trangThai,int QMSType)
        {
            bool result = false;
            string msg = "";
            try
            {
                result = QMSSer.QMSPhongkham_CapnhatTrangthai(idkham, id, trangThai, QMSType);
                return result;
            }
            catch (Exception ex)
            {
                _log.Trace(ex.ToString());
                return false;
            }
        }

        public int GetSoKhamQmsChucNang(string patientCode, string maPhong, DateTime ngayTao, int trangThai)
        {

            try
            {
                //if (!UsingWS)
                //    return _GetSoKhamQmsChucNang(patientCode, maPhong, ngayTao, trangThai);
                //else
                    return QMSSer.GetSoKhamQmsChucNang(patientCode, maPhong, ngayTao, trangThai);
            }
            catch (Exception ex)
            {
                _log.Trace(ex.ToString());
                return 0;
            }
        }
        public DataTable QmsPK_GetData(string maPhong,  DateTime ngayTao, int trangThai, string maKhoa)
        {
            try
            {
                return QMSSer.QMSPhongkham_get(maPhong, ngayTao, trangThai, maKhoa);
            }
            catch (Exception ex)
            {
                _log.Trace(ex.ToString());
                return null;
            }
        }
        public DataTable GetListQmSbyMaPhong(string maPhong, string machucnang, DateTime ngayTao, int trangThai, string maKhoa)
        {
            try
            {
                //if (!UsingWS)
                //    return _GetListQmSbyMaPhong(maPhong, machucnang, ngayTao, trangThai, maKhoa);
                //else 

                return QMSSer.GetListQmSbyMaPhong(maPhong, machucnang,ngayTao, trangThai, maKhoa);
            }
            catch (Exception ex)
            {
                _log.Trace(ex.ToString());
                return null;
            }
        }
        public bool CheckOpened(string name)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Text == name)
                {
                    return true;
                }
            }
            return false;
        }
        public FrmShowScreen ObjSoKham;

        public void CloseScreenOnMonitor2()
        {
            if (ObjSoKham != null)
            {
                ObjSoKham.Close();
                ObjSoKham.Dispose();
                ObjSoKham = null;
            }
        }
        
        public void ShowScreenOnMonitor2()
        {
            try
            {
                Screen[] sc;
                sc = Screen.AllScreens;
                IEnumerable<Screen> query = from mh in Screen.AllScreens
                                            select mh;
                try
                {
                    if (ObjSoKham != null)
                    {
                        ObjSoKham.Close();
                        ObjSoKham.Dispose();
                        ObjSoKham = null;
                    }
                }
                catch (Exception exception)
                {
                    Utility.ShowMsg("Lỗi:" + exception.Message);
                }
                if (query.Count() >= 2)
                {
                    ObjSoKham = new FrmShowScreen(true, 1, 5, "STT,Họ và tên,Giới tính,Năm sinh@194, 701, 260, 309");
                    if (!CheckOpened(ObjSoKham.Name))
                    {
                        ObjSoKham.FormBorderStyle = FormBorderStyle.None;
                        ObjSoKham.Left = sc[1].Bounds.Width;
                        ObjSoKham.Top = sc[1].Bounds.Height;
                        ObjSoKham.StartPosition = FormStartPosition.CenterScreen;
                        ObjSoKham.Location = sc[1].Bounds.Location;
                        var p = new Point(sc[1].Bounds.Location.X, sc[1].Bounds.Location.Y);
                        ObjSoKham.Location = p;
                        ObjSoKham.WindowState = FormWindowState.Maximized;
                        ObjSoKham.Show();
                    }
                }
                else
                {
                    ObjSoKham = new FrmShowScreen(true, 1, 5, "STT,Họ và tên,Giới tính,Năm sinh@194, 701, 260, 309");
                    if (!CheckOpened(ObjSoKham.Name))
                    {
                        ObjSoKham.FormBorderStyle = FormBorderStyle.None;
                        ObjSoKham.WindowState = FormWindowState.Maximized;
                        ObjSoKham.Show();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        public QMSPrintProperties _qmspro;
        public void ShowConfig()
        {
            frm_Config frm = new frm_Config();
            frm._qmsPrintProperties = _qmspro;
            frm.ShowDialog();
        }

        public void ShowListData(bool UsingWS,int QMSType)
        {
            FrmShowListData frm = new FrmShowListData(QMSType);
            frm.UsingWS = UsingWS;
            frm.QmsPrintProperties = _qmspro;
            frm.ShowDialog();
        }
        
        public void InsertGoiLoa(string soKham, string quayGoi, string mayGoi, string maKhoa, int trangThai,byte loaiQMS, string nguoiTao, DateTime ngayTao, string mayTao, string loaGoi, string noiDung)
        {
            try
            {
                //if (!UsingWS)
                //    InsertGoiLoa(soKham, quayGoi, mayGoi, maKhoa, trangThai, nguoiTao, ngayTao, mayTao, loaGoi, noiDung);
                //else
                QMSSer.InsertGoiLoa(soKham, quayGoi, mayGoi, maKhoa, trangThai, loaiQMS,nguoiTao, ngayTao, mayTao, loaGoi, noiDung);
            }
            catch (Exception ex)
            {
                _log.Trace(ex.ToString());
                
            }
        }
    }
}