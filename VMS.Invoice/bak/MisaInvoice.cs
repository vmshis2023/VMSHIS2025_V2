using Newtonsoft.Json;
using NLog;
using SubSonic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using VMS.HIS.Libs;
using VNS.Libs;
using VMS.HIS.DAL;
namespace VMS.Invoice
{
    
    public class MisaInvoice
    {
        public delegate void OnStatus(string status, bool isErr);
        public event OnStatus _OnStatus;
        private readonly Logger log;
        private string apiMisaToken = "";
        private string apiMisaThongbaophathanh = "";
        private string apiMisaPhathanh = "";
        private string apiMisaPreviewInvoice = "";
        private string apiMisaCancelInvoice = "";
        private string apiMisaSaveFileInvoice = "";
        private string apiMisaInvoiceStatus = "";
        private string apiMisaGetInvoiceByNumberCode = "";
        private int InvoiceType = 0;
        string InvoiceServicesUrl = "";
        public MisaInvoice()
        {
            try
            {
                log = LogManager.GetLogger("MISA_INVOICE");
                InvoiceServicesUrl = THU_VIEN_CHUNG.Laygiatrithamsohethong("MISA_URL", true); ;
                apiMisaToken = THU_VIEN_CHUNG.Laygiatrithamsohethong("MISA_LAYTOKEN", "", true);
                InvoiceType =Utility.Int32Dbnull( THU_VIEN_CHUNG.Laygiatrithamsohethong("MISA_LOAIHOADON", "0", true),0);
                apiMisaThongbaophathanh = THU_VIEN_CHUNG.Laygiatrithamsohethong("MISA_LAY_DANHSACHHOADON", "", true);
                apiMisaPhathanh = THU_VIEN_CHUNG.Laygiatrithamsohethong("MISA_PHATHANH_HOADON", "", true);
                apiMisaPreviewInvoice = THU_VIEN_CHUNG.Laygiatrithamsohethong("MISA_XEMTRUOC_HOADON", "", true);
                apiMisaCancelInvoice = THU_VIEN_CHUNG.Laygiatrithamsohethong("MISA_HUYHOADON", "", true);
                apiMisaSaveFileInvoice = THU_VIEN_CHUNG.Laygiatrithamsohethong("MISA_TAI_HOADON", "", true);
                apiMisaInvoiceStatus = THU_VIEN_CHUNG.Laygiatrithamsohethong("MISA_LAY_TRANGTHAIHOADON", "", true);
                apiMisaGetInvoiceByNumberCode = THU_VIEN_CHUNG.Laygiatrithamsohethong("MISA_INVOICE_GETINVOICE", "", true);
            }
            catch (Exception ex)
            {
                log.Trace(ex);
            }


        }
        
        public TemplateData Laydanhsachphathanh()
        {
            try
            {
                TemplateData objData = new TemplateData();
                string url = InvoiceServicesUrl+ Utility.sDbnull(apiMisaThongbaophathanh, "/api/MisaInvoice/lay_danhsach_mauhoadon");
                const string contentType = "application/json";
               
                    RaiseStatus(string.Format( "Bắt đầu gọi Api lấy mẫu hóa đơn {0}", url),false);
                string result = CreateRequest.WebRequest(url, "", "", "GET", contentType);
                if (!string.IsNullOrEmpty(result))
                {
                    var objResponse = JsonConvert.DeserializeObject<MisaResponse>(result);
                    if (objResponse != null && objResponse.Data != null)
                    {
                        List<TemplateData> lstList = JsonConvert.DeserializeObject<List<TemplateData>>(objResponse.Data.ToString());
                        objData = lstList[0];
                    }
                }
                else
                {
                    objData = null;
                }
                return objData;
            }
            catch (Exception ex)
            {
                log.Trace(ex.Message);
                Utility.ShowMsg(ex.Message);
                return null;
            }
        }

        public TemplateData Laydanhsachphathanh(int InvoiceType)
        {
            try
            {
                TemplateData objData = new TemplateData();
                string url = InvoiceServicesUrl+ Utility.sDbnull(apiMisaThongbaophathanh, "/api/MisaInvoice/lay_danhsach_mauhoadon");
                const string contentType = "application/json";
                var requestBody = new
                {
                    //sDataRequest = "",
                    sDataRequest = globalVariables.UserName
                };
                string jsonContent = JsonConvert.SerializeObject(requestBody);
                url = string.Format("{0}?sDataRequest={1}", url, globalVariables.UserName);

                RaiseStatus(string.Format("Bắt đầu gọi Api lấy mẫu hóa đơn {0}", url), false);
                string result = CreateRequest.WebRequest(url, "", "", "GET", contentType);
                if (!string.IsNullOrEmpty(result))
                {
                    var objResponse = JsonConvert.DeserializeObject<MisaResponse>(result);
                    if (objResponse != null && objResponse.Data != null)
                    {
                        List<TemplateData> lstList = JsonConvert.DeserializeObject<List<TemplateData>>(objResponse.Data.ToString());
                        if (lstList.Count > 0)
                        {
                            switch (InvoiceType)
                            {
                                case -1:
                                    objData = lstList[0];
                                    break;
                                default:
                                    objData = lstList.FirstOrDefault(p => p.InvoiceType == InvoiceType);
                                    break;
                            }
                        }

                    }
                }
                else
                {
                    objData = null;
                }
                return objData;
            }
            catch (Exception ex)
            {
               
                    RaiseStatus(ex.Message, true);
                log.Trace(ex.Message);
                Utility.ShowMsg(ex.Message);
                return null;
            }
        }
       
        public void PreviewInvoice(string sThamso, int kieu, ref string eMessage)
        {
            string tranSactionID = "";
            try
            {
                KcbThanhtoan objThanhtoan = null;
                log.Trace(string.Format("------------Bắt đầu tạo hóa đơn cho id_thanhtoan={0}---------------", sThamso));
                if (kieu == 1)
                {
                    objThanhtoan = KcbThanhtoan.FetchByID(sThamso);
                    if (objThanhtoan == null)
                    {
                        string.Format("Không tông tại phiếu thanh toán với id_thanhtoan={0}", sThamso);
                        log.Trace(eMessage);
                        return;
                    }
                }
               
                    RaiseStatus(string.Format("Đang kiểm tra trạng thái phát hành của hóa đơn..."),false);
                DataSet dtkiemtra = SPs.EInvoiceKiemtraHoadon(sThamso, 0, kieu).GetDataSet();
                if (dtkiemtra != null && dtkiemtra.Tables.Count > 0 && dtkiemtra.Tables[0].Rows.Count > 0)
                {
                    eMessage = string.Format("Phiếu thanh toán id_thanhtoan={0} đã được lấy hóa đơn", sThamso);
                    log.Trace(eMessage);
                    return;
                }
                else
                {
                    eMessage = string.Format("Không tồn tại phiếu thanh toán với id_thanhtoan={0}", sThamso);
                    log.Trace(eMessage);
                }
                // 
                
                    RaiseStatus(string.Format("Đang lấy dữ liệu để phát hành hóa đơn..."),false);
                DataSet ds = SPs.MisaInvoiceInfoPatient(Utility.sDbnull(sThamso), kieu, 0).GetDataSet();
                DataTable dtOrginvoicedata = ds.Tables[0];
                DataTable dtOriginalinvoicedetail = ds.Tables[1];
                DataTable dtTaxRateInfo = ds.Tables[2];
                DataTable dtOptionUserDefined = ds.Tables[3];
                DataTable dKcbThanhtoan = ds.Tables[4];
                if (dtOrginvoicedata.Rows.Count <= 0 || dtOriginalinvoicedetail.Rows.Count <= 0)
                {
                    eMessage = "Không lấy được dữ liệu tạo hóa đơn phát hành dtOrginvoicedata.Rows.Count <= 0";
                    log.Trace(eMessage);
                    return;
                }
                log.Trace("Tổng số người bệnh:  " + dtOrginvoicedata.Rows.Count);
                if (dtOriginalinvoicedetail.Rows.Count > 0 && dtOrginvoicedata.Rows.Count > 0)
                {
                    log.Trace("Bắt đầu tạo dữ liệu chi tiết cho hóa đơn từ {0} dịch vụ" + dtOriginalinvoicedetail.Rows.Count);
                    // Tạo dữ liệu chi tiết hóa đơn 
                    var lstOriginalinvoicedetail = new List<Originalinvoicedetail>();
                    foreach (DataRow row in dtOriginalinvoicedetail.AsEnumerable())
                    {
                        Originalinvoicedetail item = new Originalinvoicedetail();
                        item.ItemType = Utility.Int32Dbnull(row["ItemType"], 0);
                        item.LineNumber = Utility.Int32Dbnull(row["LineNumber"], 0);
                        item.ItemCode = Utility.sDbnull(row["ItemCode"], "");
                        item.ItemName = Utility.sDbnull(row["ItemName"], "");
                        item.UnitName = Utility.sDbnull(row["UnitName"], "");
                        item.Quantity = Utility.DecimaltoDbnull(row["Quantity"], 0);
                        item.UnitPrice = Utility.DecimaltoDbnull(row["UnitPrice"], 0);
                        // thong tin thuế xuất
                        item.AmountOC = Utility.DecimaltoDbnull(row["AmountOC"], 0);
                        item.Amount = Utility.DecimaltoDbnull(row["Amount"], 0);
                        item.AmountWithoutVATOC = Utility.DecimaltoDbnull(row["AmountWithoutVATOC"], 0);
                        // thông tin chiết khấu
                        item.DiscountRate = Utility.DecimaltoDbnull(row["DiscountRate"], 0);
                        item.DiscountAmountOC = Utility.DecimaltoDbnull(row["DiscountAmountOC"], 0);
                        item.DiscountAmount = Utility.DecimaltoDbnull(row["DiscountAmount"], 0);

                        item.VATRateName = Utility.sDbnull(row["VATRateName"], 0);
                        item.AmountAfterTax = Utility.DecimaltoDbnull(row["AmountAfterTax"], 0);
                        item.VATAmountOC = Utility.DecimaltoDbnull(row["VATAmountOC"], 0);
                        item.VATAmount = Utility.DecimaltoDbnull(row["VATAmount"], 0);
                        item.SortOrder = Utility.Int32Dbnull(row["SortOrder"], 0);

                        lstOriginalinvoicedetail.Add(item);
                    }
                    // Tạo dữ liệu chi tiết VATRateName 
                    var lsttaxrateinfo = new List<Taxrateinfo>();
                    foreach (DataRow row in dtTaxRateInfo.AsEnumerable())
                    {
                        Taxrateinfo item = new Taxrateinfo();
                        item.VATRateName = Utility.sDbnull(row["VATRateName"]);
                        item.AmountWithoutVATOC = Utility.DecimaltoDbnull(row["VATRateName"]);
                        item.VATAmountOC = Utility.DecimaltoDbnull(row["VATAmountOC"]);
                        lsttaxrateinfo.Add(item);
                    }
                    // Tạo dữ liệu chi tiết #OptionUserDefined  
                    var optionuserdefined = new Optionuserdefined();
                    foreach (DataRow row in dtOptionUserDefined.AsEnumerable())
                    {
                        optionuserdefined.MainCurrency = Utility.sDbnull(row["MainCurrency"]);
                        optionuserdefined.AmountDecimalDigits = Utility.sDbnull(row["AmountDecimalDigits"]);
                        optionuserdefined.AmountOCDecimalDigits = Utility.sDbnull(row["AmountOCDecimalDigits"]);
                        optionuserdefined.UnitPriceOCDecimalDigits = Utility.sDbnull(row["UnitPriceOCDecimalDigits"]);
                        optionuserdefined.UnitPriceDecimalDigits = Utility.sDbnull(row["UnitPriceDecimalDigits"]);
                        optionuserdefined.QuantityDecimalDigits = Utility.sDbnull(row["QuantityDecimalDigits"]);
                        optionuserdefined.CoefficientDecimalDigits = Utility.sDbnull(row["QuantityDecimalDigits"]);
                        optionuserdefined.ExchangRateDecimalDigits = Utility.sDbnull(row["ExchangRateDecimalDigits"]);
                        optionuserdefined.ClockDecimalDigits = Utility.sDbnull(row["ClockDecimalDigits"]);
                    }

                    log.Trace("Kết thúc tạo dữ liệu chi tiết hóa đơn");

                    // Thực hiện tạo data hóa đơn để gửi đi  
                    string invInvoiceSeries = "", mauHd = "", invoiceName = "";
                   
                        RaiseStatus(string.Format("Đang lấy dữ liệu mẫu hóa đơn..."),false);
                    TemplateData thongbaophathanh = Laydanhsachphathanh(InvoiceType);
                    if (thongbaophathanh != null)
                    {
                        invInvoiceSeries = thongbaophathanh.InvSeries;
                        mauHd = thongbaophathanh.IPTemplateID;
                        invoiceName = thongbaophathanh.TemplateName;
                    }
                    else
                    {
                        eMessage = "Không tồn tại thông báo phát hành hóa đơn";
                        return;
                    }

                    DataSendInvoices objdataSendInvoices = new DataSendInvoices();
                    Orginvoicedata orginvoicedata = new Orginvoicedata();
                    CultureInfo cultures = new CultureInfo("en-US");
                    if (Utility.sDbnull(invInvoiceSeries, "") == "")
                    {

                        eMessage = "Ký hiệu hóa đơn không được để trống";
                        log.Trace(eMessage);
                        return;
                    }
                    orginvoicedata.RefID = Guid.NewGuid().ToString();
                    orginvoicedata.InvSeries = invInvoiceSeries;
                    log.Trace("inv_invoiceSeries: " + orginvoicedata.InvSeries);
                    orginvoicedata.InvoiceName = invoiceName;
                    // Convert.ToDateTime(dtThontin.Rows[0]["inv_invoiceIssuedDate"], cultures).ToString("yyyy-MM-dd");
                    orginvoicedata.InvDate =
                        Convert.ToDateTime(dtOrginvoicedata.Rows[0]["InvDate"], cultures).ToString("yyyy-MM-dd");
                    orginvoicedata.CurrencyCode = Utility.sDbnull(dtOrginvoicedata.Rows[0]["CurrencyCode"]);
                    orginvoicedata.ExchangeRate = Utility.Int16Dbnull(dtOrginvoicedata.Rows[0]["ExchangeRate"]);
                    orginvoicedata.PaymentMethodName = Utility.sDbnull(dtOrginvoicedata.Rows[0]["PaymentMethodName"]);
                    try
                    {
                        orginvoicedata.BuyerFullName = Utility.sDbnull(dtOrginvoicedata.Rows[0]["BuyerFullName"]);
                        orginvoicedata.BuyerLegalName = Utility.sDbnull(dtOrginvoicedata.Rows[0]["BuyerLegalName"]);
                        orginvoicedata.BuyerTaxCode = Utility.sDbnull(dtOrginvoicedata.Rows[0]["BuyerTaxCode"]);
                    }
                    catch (Exception ex)
                    {
                        Utility.ShowMsg(ex.Message);
                        eMessage = ex.Message;
                        return;
                    }
                    orginvoicedata.BuyerAddress = Utility.sDbnull(dtOrginvoicedata.Rows[0]["BuyerAddress"]);
                    orginvoicedata.BuyerEmail = Utility.sDbnull(dtOrginvoicedata.Rows[0]["BuyerEmail"]);
                    orginvoicedata.ContactName = Utility.sDbnull(dtOrginvoicedata.Rows[0]["ContactName"]);
                    orginvoicedata.TotalAmountWithoutVATOC = Utility.DecimaltoDbnull(dtOrginvoicedata.Rows[0]["TotalAmountWithoutVATOC"], 0);
                    orginvoicedata.TotalVATAmountOC = Utility.DecimaltoDbnull(dtOrginvoicedata.Rows[0]["TotalVATAmountOC"], 0);
                    orginvoicedata.TotalAmount = Utility.DecimaltoDbnull(dtOrginvoicedata.Rows[0]["TotalAmount"], 0);
                    orginvoicedata.TotalAmountWithoutVAT = Utility.DecimaltoDbnull(dtOrginvoicedata.Rows[0]["TotalAmountWithoutVAT"], 0);
                    orginvoicedata.TotalVATAmount = Utility.DecimaltoDbnull(dtOrginvoicedata.Rows[0]["TotalVATAmount"], 0);
                    orginvoicedata.TotalSaleAmountOC = Utility.DecimaltoDbnull(dtOrginvoicedata.Rows[0]["TotalSaleAmountOC"], 0);
                    orginvoicedata.TotalSaleAmount = Utility.DecimaltoDbnull(dtOrginvoicedata.Rows[0]["TotalSaleAmount"], 0);
                    orginvoicedata.TotalAmountOC = Utility.DecimaltoDbnull(dtOrginvoicedata.Rows[0]["TotalAmountOC"], 0);
                    orginvoicedata.TotalDiscountAmount = Utility.DecimaltoDbnull(dtOrginvoicedata.Rows[0]["TotalDiscountAmount"], 0);
                    orginvoicedata.TotalDiscountAmountOC = Utility.DecimaltoDbnull(dtOrginvoicedata.Rows[0]["TotalDiscountAmountOC"], 0);
                    orginvoicedata.TotalAmountInWords = Utility.DocSoThanhChu(Convert.ToDecimal(dtOrginvoicedata.Rows[0]["TotalAmountInWords"]).ToString("####"));
                    orginvoicedata.OriginalInvoiceDetail = lstOriginalinvoicedetail;
                    orginvoicedata.IsTaxReduction43 = Utility.Int32Dbnull(dtOrginvoicedata.Rows[0]["IsTaxReduction43"], 0) == 1;

                    orginvoicedata.CustomField1 = Utility.sDbnull(dtOrginvoicedata.Rows[0]["CustomField1"], "");
                    orginvoicedata.TaxRateInfo = lsttaxrateinfo;
                    orginvoicedata.OptionUserDefined = optionuserdefined;
                    orginvoicedata.IsInvoiceCalculatingMachine = true;
                    // tạo ra object DataSendInvoices 
                    objdataSendInvoices.OrgInvoiceData = orginvoicedata;
                    objdataSendInvoices.IsSendEmail = Utility.sDbnull(dtOrginvoicedata.Rows[0]["IsSendEmail"], "0") ==
                                                      "1";
                    objdataSendInvoices.ReceiverName = Utility.sDbnull(dtOrginvoicedata.Rows[0]["ReceiverName"], "");
                    objdataSendInvoices.ReceiverEmail = Utility.sDbnull(dtOrginvoicedata.Rows[0]["ReceiverEmail"], "");
                    objdataSendInvoices.userName = globalVariables.UserName;
                    string url = InvoiceServicesUrl+ Utility.sDbnull(apiMisaPreviewInvoice, "/api/MisaInvoice/xemtruoc_hoadon");
                  
                    const string contentType = "application/json";
                    string sDataRequest = JsonConvert.SerializeObject(orginvoicedata);
                    log.Trace("sDataRequest:" + sDataRequest);

                    RaiseStatus(string.Format("Đang gửi lệnh phát hành hóa đơn. Vui lòng chờ..."),false);
                    string result = CreateRequest.WebRequest(url, sDataRequest, "", "POST", contentType);
                    if (result != null)
                    {
                        string paymentid = "";
                        var objKetquaHoadon = JsonConvert.DeserializeObject<MisaResponse>(result);
                        if (objKetquaHoadon.Success)
                        {
                            if (!string.IsNullOrEmpty(objKetquaHoadon.Data.ToString()))
                            {
                                ProcessStartInfo sInfo = new ProcessStartInfo(objKetquaHoadon.Data.ToString());
                                Process.Start(sInfo);
                            }
                            else
                            {
                               
                                eMessage = "objKetquaHoadon.Data is null ";
                               
                                    RaiseStatus(eMessage, true);
                            }
                        }
                    }

                }
                else
                {
                    eMessage = "Không có dữ liệu để gửi hóa đơn";
                   
                        RaiseStatus(eMessage, true);
                    return;
                }


            }
            catch (Exception ex)
            {
                
                    RaiseStatus(ex.Message, true);
                log.Trace(ex.Message);
                Utility.ShowMsg(ex.Message);
                return;
            }
        }
        void RaiseStatus(string msg,bool isErr)
        {
            if (_OnStatus != null)
                _OnStatus(msg, isErr);
        }
        public bool CreateInvoicesSaveSignServer(string sThamso, int kieu, ref string eMessage)
        {
            string tranSactionID = "";
            try
            {
                KcbThanhtoan objThanhtoan = null;
                log.Trace("_____Begin: " + sThamso + "_______");
                if (kieu == 1)
                {
                    objThanhtoan = KcbThanhtoan.FetchByID(sThamso);
                    if (objThanhtoan == null)
                    {
                        eMessage = "Không tồn tại lần thanh toán";
                        log.Trace(eMessage);
                        return false;
                    }
                }
                DataSet dtkiemtra = SPs.InvoiceKiemtraHoadon(sThamso, 0, kieu).GetDataSet();
                if (dtkiemtra != null && dtkiemtra.Tables.Count > 0 && dtkiemtra.Tables[0].Rows.Count > 0)
                {
                    eMessage = "Lần thanh toán này đã được lấy hóa đơn.";
                    log.Trace(eMessage);
                    return false;
                }
                else
                {
                    eMessage = "Khong kiem tra dc du lieu";
                    log.Trace(eMessage);
                }
                // 
                DataSet ds = SPs.MisaInvoiceInfoPatient(Utility.sDbnull(sThamso), kieu, 0).GetDataSet();
                DataTable dtOrginvoicedata = ds.Tables[0];
                DataTable dtOriginalinvoicedetail = ds.Tables[1];
                DataTable dtTaxRateInfo = ds.Tables[2];
                DataTable dtOptionUserDefined = ds.Tables[3];
                DataTable dKcbThanhtoan = ds.Tables[4];
                if (dtOrginvoicedata.Rows.Count <= 0 || dtOriginalinvoicedetail.Rows.Count <= 0)
                {
                    eMessage = "Không tồn tại lần thanh toán của người bệnh";
                    log.Trace(eMessage);
                    return false;
                }
                if (dtOriginalinvoicedetail.Rows.Count > 0 && dtOrginvoicedata.Rows.Count > 0)
                {
                    log.Trace("vao vong for roi");
                    log.Trace("so dich vu:  " + dtOriginalinvoicedetail.Rows.Count);
                    log.Trace("so thong tin benh nhan :  " + dtOrginvoicedata.Rows.Count);
                    // Tạo dữ liệu chi tiết hóa đơn 
                    var lstOriginalinvoicedetail = new List<Originalinvoicedetail>();
                    foreach (DataRow row in dtOriginalinvoicedetail.AsEnumerable())
                    {
                        Originalinvoicedetail item = new Originalinvoicedetail();
                        item.ItemType = Utility.Int32Dbnull(row["ItemType"], 0);
                        item.LineNumber = Utility.Int32Dbnull(row["LineNumber"], 0);
                        item.ItemCode = Utility.sDbnull(row["ItemCode"], "");
                        item.ItemName = Utility.sDbnull(row["ItemName"], "");
                        item.UnitName = Utility.sDbnull(row["UnitName"], "");
                        item.Quantity = Utility.DecimaltoDbnull(row["Quantity"], 0);
                        item.UnitPrice = Utility.DecimaltoDbnull(row["UnitPrice"], 0);
                        // thong tin thuế xuất
                        item.AmountOC = Utility.DecimaltoDbnull(row["AmountOC"], 0);
                        item.Amount = Utility.DecimaltoDbnull(row["Amount"], 0);
                        item.AmountWithoutVATOC = Utility.DecimaltoDbnull(row["AmountWithoutVATOC"], 0);
                        // thông tin chiết khấu
                        item.DiscountRate = Utility.DecimaltoDbnull(row["DiscountRate"], 0);
                        item.DiscountAmountOC = Utility.DecimaltoDbnull(row["DiscountAmountOC"], 0);
                        item.DiscountAmount = Utility.DecimaltoDbnull(row["DiscountAmount"], 0);

                        item.VATRateName = Utility.sDbnull(row["VATRateName"]);
                        item.AmountAfterTax = Utility.DecimaltoDbnull(row["AmountAfterTax"], 0);
                        item.VATAmountOC = Utility.DecimaltoDbnull(row["VATAmountOC"], 0);
                        item.VATAmount = Utility.DecimaltoDbnull(row["VATAmount"], 0);
                        item.SortOrder = Utility.Int32Dbnull(row["SortOrder"], 0);

                        lstOriginalinvoicedetail.Add(item);
                    }
                    // Tạo dữ liệu chi tiết VATRateName 
                    var lsttaxrateinfo = new List<Taxrateinfo>();
                    foreach (DataRow row in dtTaxRateInfo.AsEnumerable())
                    {
                        Taxrateinfo item = new Taxrateinfo();
                        item.VATRateName = Utility.sDbnull(row["VATRateName"]);
                        item.AmountWithoutVATOC = Utility.DecimaltoDbnull(row["AmountWithoutVATOC"]);
                        item.VATAmountOC = Utility.DecimaltoDbnull(row["VATAmountOC"]);
                        lsttaxrateinfo.Add(item);
                    }
                    // Tạo dữ liệu chi tiết #OptionUserDefined  
                    var optionuserdefined = new Optionuserdefined();
                    foreach (DataRow row in dtOptionUserDefined.AsEnumerable())
                    {
                        optionuserdefined.MainCurrency = Utility.sDbnull(row["MainCurrency"]);
                        optionuserdefined.AmountDecimalDigits = Utility.sDbnull(row["AmountDecimalDigits"]);
                        optionuserdefined.AmountOCDecimalDigits = Utility.sDbnull(row["AmountOCDecimalDigits"]);
                        optionuserdefined.UnitPriceOCDecimalDigits = Utility.sDbnull(row["UnitPriceOCDecimalDigits"]);
                        optionuserdefined.UnitPriceDecimalDigits = Utility.sDbnull(row["UnitPriceDecimalDigits"]);
                        optionuserdefined.QuantityDecimalDigits = Utility.sDbnull(row["QuantityDecimalDigits"]);
                        optionuserdefined.CoefficientDecimalDigits = Utility.sDbnull(row["QuantityDecimalDigits"]);
                        optionuserdefined.ExchangRateDecimalDigits = Utility.sDbnull(row["ExchangRateDecimalDigits"]);
                        optionuserdefined.ClockDecimalDigits = Utility.sDbnull(row["ClockDecimalDigits"]);
                    }

                    log.Trace("het viec lay thong tin chi tiet roi");

                    // Thực hiện tạo data hóa đơn để gửi đi  
                    List<DataSendInvoices> lstDataSendInvoiceses = new List<DataSendInvoices>();
                    //var lstOrginvoicedata = new List<Orginvoicedata>();
                    string invInvoiceSeries = "", mauHd = "", invoiceName = "";
                    TemplateData thongbaophathanh = Laydanhsachphathanh(InvoiceType);
                    if (thongbaophathanh != null)
                    {
                        invInvoiceSeries = thongbaophathanh.InvSeries;
                        mauHd = thongbaophathanh.OrgInvSeries;
                        invoiceName = thongbaophathanh.TemplateName;
                    }
                    else
                    {
                        eMessage = "Không tồn tại thông báo phát hành hóa đơn";
                        return false;
                    }

                    DataSendInvoices objdataSendInvoices = new DataSendInvoices();
                    Orginvoicedata orginvoicedata = new Orginvoicedata();
                    CultureInfo cultures = new CultureInfo("en-US");
                    if (Utility.sDbnull(invInvoiceSeries, "") == "")
                    {

                        eMessage = "Ký hiệu hóa đơn không được để trống";
                        log.Trace(eMessage);
                        return false;
                    }
                    orginvoicedata.RefID = Guid.NewGuid().ToString();
                    orginvoicedata.InvSeries = invInvoiceSeries;
                    log.Trace("inv_invoiceSeries: " + orginvoicedata.InvSeries);
                    orginvoicedata.InvoiceName = invoiceName;
                    // Convert.ToDateTime(dtThontin.Rows[0]["inv_invoiceIssuedDate"], cultures).ToString("yyyy-MM-dd");
                    orginvoicedata.InvDate =
                        Convert.ToDateTime(dtOrginvoicedata.Rows[0]["InvDate"], cultures).ToString("yyyy-MM-dd");
                    orginvoicedata.CurrencyCode = Utility.sDbnull(dtOrginvoicedata.Rows[0]["CurrencyCode"]);
                    orginvoicedata.ExchangeRate = Utility.Int16Dbnull(dtOrginvoicedata.Rows[0]["ExchangeRate"]);
                    orginvoicedata.PaymentMethodName = Utility.sDbnull(dtOrginvoicedata.Rows[0]["PaymentMethodName"]);
                    try
                    {
                        orginvoicedata.BuyerLegalName = Utility.sDbnull(dtOrginvoicedata.Rows[0]["BuyerLegalName"]);
                        orginvoicedata.BuyerTaxCode = Utility.sDbnull(dtOrginvoicedata.Rows[0]["BuyerTaxCode"]);
                    }
                    catch (Exception ex)
                    {
                        Utility.ShowMsg(ex.Message);
                        eMessage = ex.Message;
                        return false;
                    }
                    orginvoicedata.BuyerFullName = Utility.sDbnull(dtOrginvoicedata.Rows[0]["BuyerFullName"]);
                    orginvoicedata.BuyerAddress = Utility.sDbnull(dtOrginvoicedata.Rows[0]["BuyerAddress"]);
                    orginvoicedata.BuyerEmail = Utility.sDbnull(dtOrginvoicedata.Rows[0]["BuyerEmail"]);
                    orginvoicedata.ContactName = Utility.sDbnull(dtOrginvoicedata.Rows[0]["ContactName"]);
                    orginvoicedata.TotalAmountWithoutVATOC = Utility.DecimaltoDbnull(dtOrginvoicedata.Rows[0]["TotalAmountWithoutVATOC"], 0);
                    orginvoicedata.TotalVATAmountOC = Utility.DecimaltoDbnull(dtOrginvoicedata.Rows[0]["TotalVATAmountOC"], 0);
                    orginvoicedata.TotalAmount = Utility.DecimaltoDbnull(dtOrginvoicedata.Rows[0]["TotalAmount"], 0);
                    orginvoicedata.TotalAmountWithoutVAT = Utility.DecimaltoDbnull(dtOrginvoicedata.Rows[0]["TotalAmountWithoutVAT"], 0);
                    orginvoicedata.TotalVATAmount = Utility.DecimaltoDbnull(dtOrginvoicedata.Rows[0]["TotalVATAmount"], 0);
                    orginvoicedata.TotalSaleAmountOC = Utility.DecimaltoDbnull(dtOrginvoicedata.Rows[0]["TotalSaleAmountOC"], 0);
                    orginvoicedata.TotalSaleAmount = Utility.DecimaltoDbnull(dtOrginvoicedata.Rows[0]["TotalSaleAmount"], 0);
                    orginvoicedata.TotalAmountOC = Utility.DecimaltoDbnull(dtOrginvoicedata.Rows[0]["TotalAmountOC"], 0);
                    orginvoicedata.TotalDiscountAmount = Utility.DecimaltoDbnull(dtOrginvoicedata.Rows[0]["TotalDiscountAmount"], 0);
                    orginvoicedata.TotalDiscountAmountOC = Utility.DecimaltoDbnull(dtOrginvoicedata.Rows[0]["TotalDiscountAmountOC"], 0);
                    orginvoicedata.TotalAmountInWords = Utility.DocSoThanhChu(Convert.ToDecimal(dtOrginvoicedata.Rows[0]["TotalAmountInWords"]).ToString("####"));
                    orginvoicedata.OriginalInvoiceDetail = lstOriginalinvoicedetail;
                    orginvoicedata.IsTaxReduction43 = Utility.Int32Dbnull(dtOrginvoicedata.Rows[0]["IsTaxReduction43"], 0) == 1;
                    orginvoicedata.CustomField1 = Utility.sDbnull(dtOrginvoicedata.Rows[0]["CustomField1"], "");
                    orginvoicedata.TaxRateInfo = lsttaxrateinfo;
                    orginvoicedata.OptionUserDefined = optionuserdefined;
                    orginvoicedata.IsInvoiceCalculatingMachine = true;
                    // tạo ra object DataSendInvoices 
                    objdataSendInvoices.OrgInvoiceData = orginvoicedata;
                    objdataSendInvoices.IsSendEmail = Utility.sDbnull(dtOrginvoicedata.Rows[0]["IsSendEmail"], "0") == "1";
                    objdataSendInvoices.ReceiverName = Utility.sDbnull(dtOrginvoicedata.Rows[0]["ReceiverName"], "");
                    objdataSendInvoices.ReceiverEmail = Utility.sDbnull(dtOrginvoicedata.Rows[0]["ReceiverEmail"], "");
                    objdataSendInvoices.userName = globalVariables.UserName;
                    lstDataSendInvoiceses.Add(objdataSendInvoices);
                    string url = InvoiceServicesUrl+ Utility.sDbnull(apiMisaPhathanh, "/api/MisaInvoice/phathanh_hoadon");
                    const string contentType = "application/json";
                    string sDataRequest = JsonConvert.SerializeObject(lstDataSendInvoiceses);


                    log.Trace("sDataRequest:" + sDataRequest);

                    string result = CreateRequest.WebRequest(url, sDataRequest, "", "POST", contentType);
                    if (result != null)
                    {
                        log.Trace("result: " + result);

                        string paymentid = "";
                        var objKetquaHoadon = JsonConvert.DeserializeObject<MisaResponse>(result);
                        if (objKetquaHoadon.Success)
                        {
                            var lstMinvoices =
                                JsonConvert.DeserializeObject<List<ResponseMinvoices>>(objKetquaHoadon.Data.ToString());
                            if (lstMinvoices.Count <= 0)
                            {
                                eMessage = "Không có dữ liệu hóa đơn trả về";
                                return false;
                            }
                            ResponseMinvoices objMinvoices = lstMinvoices[0];

                            if (objMinvoices != null)
                            {
                                tranSactionID = objMinvoices.TransactionID;
                                if (kieu == 0)
                                {
                                    foreach (DataRow row in dKcbThanhtoan.AsEnumerable())
                                    {
                                        var objhoalog = new HoadonLog();
                                        objhoalog.IdThanhtoan = Utility.Int32Dbnull(row["id_thanhtoan"]);
                                        objhoalog.TongTien = Utility.DecimaltoDbnull(row["SOTIEN"]);
                                        objhoalog.IdBenhnhan = Utility.Int32Dbnull(row["id_benhnhan"]);
                                        objhoalog.MaLuotkham = Utility.sDbnull(row["ma_luotkham"]);
                                        objhoalog.MauHoadon = mauHd;
                                        objhoalog.KiHieu = Utility.sDbnull(objMinvoices.InvSeries, invInvoiceSeries);
                                        //objhoalog.CapphatId = -1;
                                        objhoalog.MaQuyen = "";
                                        objhoalog.Serie = Utility.sDbnull(objMinvoices.InvNo);
                                        objhoalog.MaNhanvien = globalVariables.UserName;
                                        objhoalog.MaLydo = string.Empty;
                                        objhoalog.NgayIn = THU_VIEN_CHUNG.GetSysDateTime();
                                        objhoalog.IpMaytao = THU_VIEN_CHUNG.GetIP4Address();
                                        objhoalog.MacMaytao = THU_VIEN_CHUNG.GetMACAddress();
                                        objhoalog.DaGui = Utility.Bool2byte(true);
                                        objhoalog.TrangThai = 0;
                                        objhoalog.QrDatacode = "";
                                        objhoalog.InvInvoiceAuthId = objMinvoices.RefID;
                                        objhoalog.InvInvoiceCodeId = objMinvoices.TransactionID;
                                        objhoalog.Sobaomat = "";
                                        objhoalog.IdRef = objMinvoices.RefID;
                                        objhoalog.Save();
                                        //StoredProcedure sp =
                                        //    SPs.VienPhiUpdateHoaDonLog(Utility.Int32Dbnull(objhoalog.HdonLogId),
                                        //        Utility.Int32Dbnull(objhoalog.CapphatId),
                                        //        Utility.Int32Dbnull(objhoalog.IdThanhtoan),
                                        //        objhoalog.TongTien, Utility.Int32Dbnull(objhoalog.IdBenhnhan),
                                        //        objhoalog.MaLuotkham,
                                        //        objhoalog.MauHdon, objhoalog.KiHieu,
                                        //        objhoalog.MaQuyen, objhoalog.Serie, objhoalog.MaNvien, objhoalog.MaLdo,
                                        //        objhoalog.NgayIn,
                                        //        objhoalog.IpAddress, objhoalog.MacAddress,
                                        //        Utility.ByteDbnull(objhoalog.TrangThai),
                                        //        objhoalog.DaGui, Utility.sDbnull(objhoalog.QrDataCode), 0,
                                        //        objhoalog.InvInvoiceAuthId, objhoalog.InvInvoiceCodeId,
                                        //        objhoalog.Sobaomat,
                                        //        objhoalog.Id, 0);
                                        //int record = sp.Execute();
                                        //if (record <= 0) return false;
                                        log.Trace("Lay hoa don thanh cong cho lan thanh toan: " +
                                                  Utility.Int32Dbnull(objhoalog.IdThanhtoan) + " voi so hoa don la: " +
                                                  objhoalog.Serie);
                                        paymentid = !string.IsNullOrEmpty(paymentid)
                                            ? paymentid + ";" + Utility.sDbnull(row["id_thanhtoan"])
                                            : Utility.sDbnull(row["id_thanhtoan"]);
                                        // lưu thông tin bảng data_ketqua 
                                    }
                                }
                                else
                                {
                                    var objhoalog = new HoadonLog();
                                    if (objThanhtoan != null)
                                    {
                                        objhoalog.IdThanhtoan = Utility.Int32Dbnull(objThanhtoan.IdThanhtoan);
                                        objhoalog.IdBenhnhan = Utility.Int32Dbnull(objThanhtoan.IdBenhnhan);
                                        objhoalog.MaLuotkham = Utility.sDbnull(objThanhtoan.MaLuotkham);
                                        paymentid = Utility.sDbnull(objThanhtoan.IdThanhtoan);
                                    }
                                    objhoalog.TongTien = Utility.DecimaltoDbnull(orginvoicedata.TotalAmount);
                                    //Utility.DecimaltoDbnull(txtSoTienCanNop.Text);
                                    objhoalog.MauHoadon = mauHd;
                                    objhoalog.KiHieu = Utility.sDbnull(objMinvoices.InvSeries, invInvoiceSeries);
                                    //objhoalog.CapphatId = -1;
                                    objhoalog.MaQuyen = "";
                                    objhoalog.Serie = Utility.sDbnull(objMinvoices.InvNo);
                                    objhoalog.MaNhanvien = globalVariables.UserName;
                                    objhoalog.NguoiTao = globalVariables.UserName;
                                    objhoalog.MaLydo = string.Empty;
                                    objhoalog.NgayIn = THU_VIEN_CHUNG.GetSysDateTime();
                                    objhoalog.IpMaytao = THU_VIEN_CHUNG.GetIP4Address();
                                    objhoalog.MacMaytao = THU_VIEN_CHUNG.GetMACAddress();
                                    objhoalog.DaGui = Utility.Bool2byte(true);
                                    objhoalog.TrangThai = 0;
                                    objhoalog.QrDatacode = "";
                                    objhoalog.InvInvoiceAuthId = objMinvoices.RefID;
                                    objhoalog.InvInvoiceCodeId = objMinvoices.TransactionID;
                                    objhoalog.Sobaomat = "";
                                    objhoalog.IdRef = objMinvoices.RefID;
                                    objhoalog.Save();
                                    //var sp = SPs.VienPhiUpdateHoaDonLog(Utility.Int32Dbnull(objhoalog.HdonLogId),
                                    //    Utility.Int32Dbnull(objhoalog.CapphatId),
                                    //    Utility.Int32Dbnull(objhoalog.IdThanhtoan),
                                    //    objhoalog.TongTien, Utility.Int32Dbnull(objhoalog.IdBenhnhan),
                                    //    objhoalog.MaLuotkham,
                                    //    objhoalog.MauHdon, objhoalog.KiHieu,
                                    //    objhoalog.MaQuyen, objhoalog.Serie, objhoalog.MaNvien, objhoalog.MaLdo,
                                    //    objhoalog.NgayIn,
                                    //    objhoalog.IpAddress, objhoalog.MacAddress,
                                    //    Utility.ByteDbnull(objhoalog.TrangThai),
                                    //    objhoalog.DaGui, Utility.sDbnull(objhoalog.QrDataCode), 0,
                                    //    objhoalog.InvInvoiceAuthId, objhoalog.InvInvoiceCodeId, objhoalog.Sobaomat,
                                    //    objhoalog.Id, 0);
                                    //int record = sp.Execute();
                                    //if (record <= 0) return false;
                                    log.Trace("Lay hoa don thanh cong cho lan thanh toan: " +
                                              Utility.Int32Dbnull(objhoalog.IdThanhtoan)
                                              + " voi so hoa don la: " + objhoalog.Serie);
                                    // lưu thông tin bảng data_ketqua 
                                }
                                //// lưu thông tin bảng data_ketqua 
                                DataKetqua objdatKetqua = new DataKetqua();
                                objdatKetqua.InvInvoiceAuthId = objMinvoices.RefID;
                                objdatKetqua.PaymentId = Utility.sDbnull(sThamso);
                                objdatKetqua.InvInvoiceType = null;
                                objdatKetqua.InvInvoiceCodeId = objMinvoices.TransactionID;
                                objdatKetqua.InvInvoiceSeries = objMinvoices.InvSeries;
                                objdatKetqua.InvInvoiceNumber = Utility.sDbnull(objMinvoices.InvNo);
                                objdatKetqua.InvInvoiceName = null;
                                objdatKetqua.InvInvoiceIssuedDate = orginvoicedata.InvDate;
                                objdatKetqua.InvSubmittedDate = null;
                                objdatKetqua.InvContractNumber = null;
                                objdatKetqua.InvContractDate = null;
                                objdatKetqua.InvCurrencyCode = orginvoicedata.CurrencyCode;
                                objdatKetqua.InvExchangeRate = "1";
                                objdatKetqua.InvInvoiceNote = null;
                                objdatKetqua.InvAdjustmentType = "1";
                                objdatKetqua.InvOriginalInvoiceId = null;
                                objdatKetqua.InvAdditionalReferenceDes = null;
                                objdatKetqua.InvAdditionalReferenceDate = null;
                                objdatKetqua.InvBuyerDisplayName = orginvoicedata.ContactName;
                                objdatKetqua.MaDt = orginvoicedata.CustomField1;
                                objdatKetqua.InvBuyerLegalName = orginvoicedata.ContactName;
                                objdatKetqua.InvBuyerTaxCode = orginvoicedata.BuyerTaxCode;
                                objdatKetqua.InvBuyerAddressLine = orginvoicedata.BuyerAddress;
                                objdatKetqua.InvBuyerEmail = orginvoicedata.BuyerAddress;
                                objdatKetqua.InvBuyerBankAccount = "";
                                objdatKetqua.InvBuyerBankName = "";
                                objdatKetqua.InvPaymentMethodName = orginvoicedata.PaymentMethodName;
                                objdatKetqua.InvSellerBankAccount = null;
                                objdatKetqua.InvSellerBankName = null;
                                objdatKetqua.InvDiscountAmount = "";
                                objdatKetqua.TrangThai = null;
                                objdatKetqua.UserNew = null;
                                objdatKetqua.DateNew = null;
                                objdatKetqua.MaDvcs = null;
                                objdatKetqua.DatabaseCode = null;
                                objdatKetqua.MaCt = null;
                                objdatKetqua.SignedDate = null;
                                objdatKetqua.SubmittedDate = null;
                                objdatKetqua.MauHd = null;
                                objdatKetqua.SoBenhAn = null;
                                objdatKetqua.Sovb = null;
                                objdatKetqua.Ngayvb = null;
                                objdatKetqua.GhiChu = null;
                                objdatKetqua.SoHdDc = null;
                                objdatKetqua.InvOriginalId = null;
                                objdatKetqua.Signature = null;
                                objdatKetqua.DieuTri = null;
                                objdatKetqua.Ma1 = null;
                                objdatKetqua.InvItemCode = null;
                                objdatKetqua.InvInvoiceName = null;
                                objdatKetqua.InvUnitCode = null;
                                objdatKetqua.InvUnitName = null;
                                objdatKetqua.InvUnitPrice = null;
                                objdatKetqua.InvQuantity = null;
                                objdatKetqua.InvTotalAmountWithoutVat =
                                    orginvoicedata.TotalAmountWithoutVAT.ToString("N");
                                objdatKetqua.InvVatPercentage = null;
                                objdatKetqua.InvVatAmount = null;
                                objdatKetqua.InvTotalAmount = orginvoicedata.TotalAmount.ToString("N");
                                objdatKetqua.NguoiKy = null;
                                objdatKetqua.Sobaomat = null;
                                objdatKetqua.TrangThaiHd = null;
                                objdatKetqua.InChuyenDoi = null;
                                objdatKetqua.NgayKy = null;
                                objdatKetqua.NguoiInCdoi = null;
                                objdatKetqua.NgayInCdoi = null;
                                objdatKetqua.InvDeliveryOrderNumber = null;
                                objdatKetqua.InvDeliveryOrderDate = null;
                                objdatKetqua.InvDeliveryBy = null;
                                objdatKetqua.InvTransportationMethod = null;
                                objdatKetqua.InvFromWarehouseName = null;
                                objdatKetqua.InvToWarehouseName = null;
                                objdatKetqua.InvSobangke = null;
                                objdatKetqua.InvNgaybangke = null;
                                objdatKetqua.KeyApi = null;
                                objdatKetqua.Id = objMinvoices.RefID;
                                objdatKetqua.IsNew = true;
                                objdatKetqua.Save();
                                eMessage =
                                    string.Format("Lấy hóa đơn thành công cho người mua: {0} với số hóa đơn là: {1}",
                                        orginvoicedata.BuyerLegalName, objMinvoices.InvNo);
                                return true;
                            }
                            else
                            {
                                eMessage = result;
                                log.Trace(eMessage);
                                return false;
                            }
                        }
                        else
                        {
                            eMessage = result;
                            log.Trace(eMessage);
                            return false;
                        }
                    }
                    else
                    {
                        eMessage = "Không có dữ liệu để gửi hóa đơn";
                        return false;
                    }

                }
                else
                {
                    eMessage = "Không có dữ liệu để gửi hóa đơn";
                    return false;
                }


            }
            catch (Exception ex)
            {
                log.Trace(ex.Message);
                Utility.ShowMsg(ex.Message);
                return false;
            }
            finally
            {
                if (!string.IsNullOrEmpty(tranSactionID))
                {
                    string eMessageDownload = "";
                    bool thongbaomofile = THU_VIEN_CHUNG.Laygiatrithamsohethong("MISA_THONGBAOMOFILE",true)=="1";
                    bool kt = SaveFileInvoice(tranSactionID, thongbaomofile, ref eMessageDownload);
                    if (kt)
                    {
                        StoredProcedure sp =
                                      SPs.EInvoiceCapnhapHoadonLog(Utility.sDbnull(tranSactionID, ""),
                                          globalVariables.gv_intIDNhanvien.ToString(), globalVariables.SysDate, 1);
                        sp.Execute();
                    }
                    log.Trace(eMessageDownload);
                }
            }
        }
        public bool CancelInvoice(string invoiceAuthId, string ghichu, DateTime ngayvb, string sohoadon, ref string eMessage)
        {
            try
            {
                var objThongtinHuyhoadon = new MisaCancelModel();
                objThongtinHuyhoadon.TransactionID = invoiceAuthId;
                objThongtinHuyhoadon.InvNo = sohoadon;
                objThongtinHuyhoadon.RefDate = ngayvb.ToString("yyyy-MM-dd");
                objThongtinHuyhoadon.CancelReason = ghichu;
                objThongtinHuyhoadon.userName = globalVariables.UserName;
                string sDataRequest = JsonConvert.SerializeObject(objThongtinHuyhoadon);
                string url = InvoiceServicesUrl+ Utility.sDbnull(apiMisaCancelInvoice, "/api/MisaInvoice/huy_hoadon");
                const string contentType = "application/json";
                log.Trace("sDataRequest: " + sDataRequest);

                string result = CreateRequest.WebRequest(url, sDataRequest, "", "POST", contentType);
                log.Trace("result: " + result);
                var mes = JsonConvert.DeserializeObject<MisaResponse>(result);
                if (mes.Success)
                {
                    eMessage = result;
                    new Update(HoadonLog.Schema)
                        .Set(HoadonLog.Columns.TrangThai).EqualTo(1)
                        .Set(HoadonLog.Columns.NgayHuy).EqualTo(globalVariables.SysDate)
                        .Set(HoadonLog.Columns.NguoiHuy).EqualTo(globalVariables.UserName)
                        .Where(HoadonLog.Columns.InvInvoiceCodeId)
                        .IsEqualTo(Utility.sDbnull(invoiceAuthId, ""))
                        .Execute();
                    return true;
                }
                else
                {
                    eMessage = string.Format("{0}-{1}", Utility.sDbnull(mes.ErrorCode), Utility.sDbnull(mes.Errors));
                    return false;
                }
            }
            catch (Exception ex)
            {
                log.Trace(ex.Message);
                return false;
            }

        }
        public bool SaveFileInvoice(string invoiceAuthId, bool isOpen, ref string eMessage)
        {
            try
            {
                downloadModel objDownloadModel = new downloadModel();
                objDownloadModel.userName = globalVariables.UserName;
                objDownloadModel.maTracuu = invoiceAuthId;
                string sDataRequest = JsonConvert.SerializeObject(objDownloadModel);

                string url = InvoiceServicesUrl+ Utility.sDbnull(apiMisaSaveFileInvoice, "/api/MisaInvoice/tai_hoadon");
                const string contentType = "application/json";
                string result = CreateRequest.WebRequest(url, sDataRequest, "", "POST", contentType);
                if (!string.IsNullOrEmpty(result))
                {
                    var objResponse = JsonConvert.DeserializeObject<MisaResponse>(result);
                    if (objResponse != null && objResponse.Success)
                    {
                        string spath = AppDomain.CurrentDomain.BaseDirectory;
                        if (!Directory.Exists(Path.Combine(spath, "pdfFile")))
                        {
                            Directory.CreateDirectory(Path.Combine(spath, "pdfFile"));
                        }
                        if (objResponse.Data != null)
                        {
                            var lstdata = JsonConvert.DeserializeObject<List<ResponseFileMinvoices>>(objResponse.Data.ToString());
                            if (lstdata.Count > 0)
                            {
                                var objdata = lstdata[0];
                                if (objdata != null)
                                {
                                    string path = string.Format(@"{0}\{1}.pdf", Path.Combine(spath, "pdfFile"), invoiceAuthId);
                                    File.WriteAllBytes(path, System.Convert.FromBase64String(objdata.Data));
                                    if (isOpen)
                                    {
                                        if (Utility.AcceptQuestion("Bạn có muốn mở file luôn không?", "Thông báo", true))
                                        {
                                            Process.Start(path);
                                        }
                                    }
                                    else
                                    {
                                        Process.Start(path);
                                    }
                                    return true;
                                }
                                else
                                {
                                    eMessage = string.Format("ResponseFileMinvoices is null: {0}", JsonConvert.SerializeObject(objResponse.Data));
                                    return false;
                                }
                            }
                            else
                            {
                                eMessage = string.Format("lstdata count <= 0: {0}", JsonConvert.SerializeObject(objResponse.Data));
                                return false;
                            }

                        }
                        else
                        {
                            eMessage = string.Format("objResponse.Data is fail: {0}", JsonConvert.SerializeObject(objResponse.Data));
                            return false;
                        }
                    }
                    else
                    {
                        eMessage = string.Format("objResponse is null Or objResponse.Success is fail: {0}",
                            objResponse == null ? "" : Utility.sDbnull(objResponse.Errors, ""));
                        return false;
                    }
                }
                else
                {
                    eMessage = "result is null";
                    return false;
                }
            }
            catch (Exception ex)
            {
                log.Trace(ex.Message);
                return false;
            }

        }
        public string GetInvoiceByNumberCode(string invoiceCode, string invoiceSeries, string invoiceNumber, ref string eMessage)
        {
            try
            {
                //string result = _invoicesConnection.GetInvoiceByNumberCode(invoiceCode, invoiceSeries, invoiceNumber, ref eMessage);
                //if (result != null)
                //{
                //    var objKetquaHoadon = JsonConvert.DeserializeObject<VietbaIT.Vacom.Invoice.Model.data_ketqua>(result);
                //    if (objKetquaHoadon != null)
                //    {
                //        return objKetquaHoadon.inv_InvoiceAuth_id;
                //        //return objKetquaHoadon.data.inv_InvoiceAuth_id;
                //    }
                //}
                return "";
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return null;
            }
        }

    }
}
