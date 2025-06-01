using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VNS.Libs;

namespace VMS.Invoice
{
   
    public class eInvoice
    {
        //private VacomInvoicesConnection _viettelInvoice;
        //private VacomInvoicesConnection _vacomInvoices;
        //private HiloInvoicesConnection _hiloInvoices;
        private MisaInvoice _MisaInvoices;
        //private VnptInvoicesConnection _VnptInvoices;
        private Logger _log;
        private string _donviketnoi = "VIETTEL";
        private bool _ktUrl = false;
        public eInvoice()
        {
            var config = new LoggingConfiguration();
            var fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);
            fileTarget.FileName = "${basedir}/Applog/${date:format=yyyy}/${date:format=MM}/${date:format=dd}/${logger}.log";
            fileTarget.Layout = "${date:format=HH\\:mm\\:ss}|${threadid}|${level}|${logger}|${message}";
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, fileTarget));
            LogManager.Configuration = config;
            _log = LogManager.GetCurrentClassLogger();
            _donviketnoi = THU_VIEN_CHUNG.Laygiatrithamsohethong("INVOICE_BRANCH", "VIETTEL", false);
            switch (_donviketnoi)
            {
                case "MISA":
                    _MisaInvoices = new MisaInvoice();
                    break;
                //case "VIETTEL":
                //    _viettelInvoice = new VacomInvoicesConnection();
                //    break;
                //case "HILO":
                //    _hiloInvoices = new HiloInvoicesConnection();
                //    break;
                //case "VACOM":
                //    _vacomInvoices = new VacomInvoicesConnection();
                //    break;
                //case "VNPT":
                //    _VnptInvoices = new VnptInvoicesConnection();
                //    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Hàm thực hiện chuyển dữ liệu hóa đơn điện tử
        /// </summary>
        /// <param name="sThamso"></param>
        /// <param name="adjustmentType"></param>
        /// <param name="kieu"> 1: tìm kiếm theo Payment_Id, 0: tìm tiếm theo kiểu patient_code</param>
        /// <param name="eMessage"></param>
        /// <returns></returns>
        public bool CreateInvoice(string sThamso, string adjustmentType, int kieu, ref string eMessage)
        {
            eMessage = "";
            try
            {
                switch (_donviketnoi)
                {
                    case "MISA":
                        _MisaInvoices.phathanh_hoadon(sThamso, kieu,"-1", ref eMessage);
                        break;
                    //case "HILO":
                    //    var obj = new DataPdfhoadon();
                    //    _hiloInvoices.HiloXuatHoaDon(sThamso, kieu, ref eMessage, ref obj);
                    //    break;
                    //case "VIETTEL":
                    //    _viettelInvoice.CreateInvoicesSaveSignServer(sThamso, kieu, ref eMessage);
                    //    break;
                    //case "VACOM":
                    //    _vacomInvoices.CreateInvoicesSaveSignServer(sThamso, kieu, ref eMessage);
                    //    break;
                    //case "VNPT":
                    //    _VnptInvoices.XuatHoaDon(sThamso, kieu, ref eMessage);
                    //    break;
                    default:
                        //_vacomInvoices.CreateInvoicesSaveSignServer(sThamso, kieu, ref eMessage);
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.Trace(eMessage);
                _log.Trace(ex.Message);
                return false;
            }

        }/// <summary>
         /// Gửi hóa đơn và không ký
         /// </summary>
         /// <param name="sThamso"></param>
         /// <param name="adjustmentType"></param>
         /// <param name="kieu"></param>
         /// <param name="eMessage"></param>
         /// <returns></returns>
        public bool CreateInvoiceUnSign(string sThamso, string adjustmentType, int kieu, ref string eMessage)
        {
            eMessage = "";
            try
            {
                switch (_donviketnoi)
                {
                    case "MISA":
                        _MisaInvoices.phathanh_hoadon(sThamso, kieu,"-1", ref eMessage);
                        break;
                    //case "HILO":
                    //    var obj = new DataPdfhoadon();
                    //    _hiloInvoices.HiloXuatHoaDon(sThamso, kieu, ref eMessage, ref obj);
                    //    break;
                    //case "VIETTEL":
                    //    _viettelInvoice.CreateInvoicesSaveSignServer(sThamso, kieu, ref eMessage);
                    //    break;
                    //case "VACOM":
                    //    _vacomInvoices.CreateInvoicesSaveServer(sThamso, kieu, ref eMessage);
                    //    break;
                    //case "VNPT":
                    //    _VnptInvoices.XuatHoaDon(sThamso, kieu, ref eMessage);
                    //    break;
                    default:
                        //_vacomInvoices.CreateInvoicesSaveServer(sThamso, kieu, ref eMessage);
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.Trace(eMessage);
                _log.Trace(ex.Message);
                return false;
            }

        }
        public bool CancalInvoice(string invoiceAuthId, string sovb, DateTime ngayvb, string ghichu, ref string eMessage)
        {
            eMessage = "";
            bool kt = false;
            try
            {
                switch (_donviketnoi)
                {
                    case "MISA":
                        kt = _MisaInvoices.huy_hoadon(invoiceAuthId, sovb, ngayvb, ghichu, ref eMessage);
                        break;
                    //case "VACOM":
                    //    kt = _vacomInvoices.CancelInvoice(invoiceAuthId, sovb, ngayvb, ghichu, ref eMessage);
                    //    break;
                    //case "HILO":
                    //    var objInvoiceModels = new InvoiceModels();
                    //    objInvoiceModels.fkey = invoiceAuthId;
                    //    kt = _hiloInvoices.CancelInvoice(objInvoiceModels, ref eMessage);
                    //    break;
                    //case "VNPT":
                    //    kt = _VnptInvoices.CancelInvoices(invoiceAuthId, ref eMessage);
                    //    break;
                    default:
                        //kt = _vacomInvoices.CancelInvoice(invoiceAuthId, sovb, ngayvb, ghichu, ref eMessage);
                        break;
                }
                return kt;
            }
            catch (Exception ex)
            {
                _log.Trace(eMessage);
                _log.Trace(ex.Message);
                return false;
            }

        }
        public bool SaveFileInvoice(string sPath, string invoiceAuthId, bool isOpen, ref string eMessage)
        {
            eMessage = "";
            try
            {
                switch (_donviketnoi)
                {
                    case "MISA":
                        _MisaInvoices.tai_hoadon(invoiceAuthId, isOpen, ref eMessage);
                        break;
                    //case "VIETTEL":
                    //    _viettelInvoice.SaveFileInvoice(invoiceAuthId, isOpen, ref eMessage);
                    //    break;
                    //case "VACOM":
                    //    _vacomInvoices.SaveFileInvoice(invoiceAuthId, isOpen, ref eMessage);
                    //    break;
                    //case "VNPT":
                    //    _VnptInvoices.DownloadInvoices(invoiceAuthId, isOpen, ref eMessage);
                    //    break;
                    default:
                        //_vacomInvoices.SaveFileInvoice(invoiceAuthId, isOpen, ref eMessage);
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.Trace(eMessage);
                _log.Trace(ex.Message);
                return false;
            }

        }
    }
}
