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
using System.Transactions;

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
        public string BuyerCode = "";
        public string BuyerLegalName = "";
        public string BuyerTaxCode = "";
        public string BuyerAddress = "";
        public string BuyerFullName = "";
        public string BuyerPhoneNumber = "";
        public string BuyerEmail = "";
        public string BuyerBankAccount = "";
        public string BuyerBankName = "";
        public string BuyerIDNumber = "";
        public bool replaceBuyer = false;
        string invInvoiceSeries = "", mauHd = "", invoiceName = "";
        public string transaction_id = "";
        public int VAT = 0;
        public bool isConfirmBeforeDoing = false;
        public BuyerInfor _buyer = null;
        public MisaInvoice()
        {
            try
            {
                log = LogManager.GetLogger("MISA_INVOICE");

                InvoiceServicesUrl = THU_VIEN_CHUNG.Laygiatrithamsohethong("MISA_URL", true); ;
                apiMisaToken = THU_VIEN_CHUNG.Laygiatrithamsohethong("MISA_LAYTOKEN", "", true);
                InvoiceType = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("MISA_LOAIHOADON", "0", true), 0);
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
        public void SetMauhoadon(string invInvoiceSeries, string mauHd, string invoiceName)
        {
            this.invInvoiceSeries = invInvoiceSeries;
            this.mauHd = mauHd;
            this.invoiceName = invoiceName;
        }
        public List<TemplateData> lay_danhsach_mauhoadon()
        {
            try
            {
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
                        return lstList;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Trace(ex.Message);
                Utility.ShowMsg(ex.Message);
                return null;
            }
        }

        public TemplateData lay_danhsach_mauhoadon(int InvoiceType)
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
        void ReplaceBuyerInfor(ref DataTable dtData)
        {

            if (replaceBuyer)
            {
                (from p in dtData.AsEnumerable() select p).ToList()
                    .ForEach(x =>
                    {
                        x["BuyerCode"] = BuyerCode;
                        x["BuyerLegalName"] = BuyerLegalName;
                        x["BuyerTaxCode"] = BuyerTaxCode;
                        x["BuyerAddress"] = BuyerAddress;
                        x["BuyerFullName"] = BuyerFullName;
                        x["BuyerPhoneNumber"] = BuyerPhoneNumber;
                        x["BuyerEmail"] = BuyerEmail;
                        x["BuyerBankAccount"] = BuyerBankAccount;
                        x["BuyerBankName"] = BuyerBankName;
                        x["BuyerIDNumber"] = BuyerIDNumber;
                        x["VAT"] = VAT;
                    }
                    );
            }
        }
        public bool  xemtruoc_hoadon(string str_IdThanhtoan, int kieu, string lstIdThanhtoanChitiet, ref string eMessage)
        {
            string tranSactionID = "";
            try
            {
                KcbThanhtoan objThanhtoan = null;
                List<string> stringList = str_IdThanhtoan.Split(',').ToList<string>();
                List<int> lstID = stringList.ConvertAll(int.Parse);
                log.Trace(string.Format("------------Bắt đầu tạo hóa đơn cho id_thanhtoan={0}---------------", str_IdThanhtoan));
                //if (kieu == 1)
                //{
                //    objThanhtoan = KcbThanhtoan.FetchByID(lstID[0]);
                //    if (objThanhtoan == null)
                //    {
                //        string.Format("Không tồn tại phiếu thanh toán với id_thanhtoan={0}", str_IdThanhtoan);
                //        log.Trace(eMessage);
                //        return;
                //    }
                //}
               
                    RaiseStatus(string.Format("Đang kiểm tra trạng thái phát hành của hóa đơn..."),false);
                DataSet dtkiemtra = SPs.EInvoiceKiemtraHoadon(lstIdThanhtoanChitiet, 0, kieu).GetDataSet();
                if (dtkiemtra != null && dtkiemtra.Tables.Count > 0 && dtkiemtra.Tables[0].Rows.Count > 0)
                {
                    eMessage = string.Format("Chi tiết thanh toán {0} đã được lấy hóa đơn", lstIdThanhtoanChitiet);
                    log.Trace(eMessage);
                    return false;
                }
                else
                {
                }
                // 
                
                    RaiseStatus(string.Format("Đang lấy dữ liệu để phát hành hóa đơn..."),false);
                DataSet ds = SPs.EInvoiceLaydulieuTaohoadon(Utility.sDbnull(str_IdThanhtoan), kieu, lstIdThanhtoanChitiet, 0, VAT).GetDataSet();
                DataTable dtOrginvoicedata = ds.Tables[0];
                DataTable dtOriginalinvoicedetail = ds.Tables[1];
                DataTable dtTaxRateInfo = ds.Tables[2];
                DataTable dtOptionUserDefined = ds.Tables[3];
                DataTable dKcbThanhtoan = ds.Tables[4];
               
                if (dtOrginvoicedata.Rows.Count <= 0 || dtOriginalinvoicedetail.Rows.Count <= 0)
                {
                    eMessage = "Không lấy được dữ liệu tạo hóa đơn phát hành từ phiếu thanh toán đang chọn dtOrginvoicedata.Rows.Count <= 0";
                    log.Trace(eMessage);
                    return false;
                }
                ReplaceBuyerInfor(ref dtOrginvoicedata);
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

                    log.Trace("Kết thúc tạo dữ liệu chi tiết hóa đơn");

                    // Thực hiện tạo data hóa đơn để gửi đi  
                    //string invInvoiceSeries = "", mauHd = "", invoiceName = "";
                    //    RaiseStatus(string.Format("Đang lấy dữ liệu mẫu hóa đơn..."),false);
                    //TemplateData thongbaophathanh = lay_danhsach_mauhoadon(InvoiceType);
                    //if (thongbaophathanh != null)
                    //{
                    //    invInvoiceSeries = thongbaophathanh.InvSeries;
                    //    mauHd = thongbaophathanh.IPTemplateID;
                    //    invoiceName = thongbaophathanh.TemplateName;
                    //}
                    //else
                    //{
                    //    eMessage = "Không tồn tại thông báo phát hành hóa đơn";
                    //    return;
                    //}

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
                        orginvoicedata.BuyerFullName = Utility.sDbnull(dtOrginvoicedata.Rows[0]["BuyerFullName"]);
                        orginvoicedata.BuyerLegalName = Utility.sDbnull(dtOrginvoicedata.Rows[0]["BuyerLegalName"]);
                        orginvoicedata.BuyerTaxCode = Utility.sDbnull(dtOrginvoicedata.Rows[0]["BuyerTaxCode"]);
                    }
                    catch (Exception ex)
                    {
                        Utility.ShowMsg(ex.Message);
                        eMessage = ex.Message;
                        return false;
                    }
                    orginvoicedata.AccountObjectIdentificationNumber = Utility.sDbnull(dtOrginvoicedata.Rows[0]["CMT"]);
                    orginvoicedata.BuyerAddress = Utility.sDbnull(dtOrginvoicedata.Rows[0]["BuyerAddress"]);
                    orginvoicedata.BuyerEmail = Utility.sDbnull(dtOrginvoicedata.Rows[0]["BuyerEmail"]);
                    orginvoicedata.ContactName = Utility.sDbnull(dtOrginvoicedata.Rows[0]["ContactName"]);
                    orginvoicedata.TotalAmountWithoutVATOC = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalAmountWithoutVATOC)", "1=1"), 0);
                    orginvoicedata.TotalVATAmountOC = Utility.DecimaltoDbnull(dtTaxRateInfo.Rows[0]["VATAmountOC"]);// Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalVATAmountOC)", "1=1"), 0);
                    orginvoicedata.TotalAmount = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalAmount)", "1=1"), 0);
                    orginvoicedata.TotalAmountWithoutVAT = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalAmountWithoutVAT)", "1=1"), 0);
                    orginvoicedata.TotalVATAmount = Utility.DecimaltoDbnull(dtTaxRateInfo.Rows[0]["VATAmountOC"]);// Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalVATAmount)", "1=1"), 0);
                    orginvoicedata.TotalSaleAmountOC = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalSaleAmountOC)", "1=1"), 0);
                    orginvoicedata.TotalSaleAmount = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalSaleAmount)", "1=1"), 0);
                    orginvoicedata.TotalAmountOC = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalAmountOC)", "1=1"), 0);
                    orginvoicedata.TotalDiscountAmount = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalDiscountAmount)", "1=1"), 0);
                    orginvoicedata.TotalDiscountAmountOC = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalDiscountAmountOC)", "1=1"), 0);
                    orginvoicedata.TotalAmountInWords = Utility.DocSoThanhChu(Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalAmountInWords)", "1=1"), 0).ToString("####"));
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

                    RaiseStatus(string.Format("Đang gửi lệnh xem trước hóa đơn. Vui lòng chờ..."),false);
                    string result = CreateRequest.WebRequest(url, sDataRequest, "", "POST", contentType);
                    if (result != null)
                    {
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
                                return false;
                            }
                        }
                    }

                }
                else
                {
                    eMessage = "Không có dữ liệu để gửi hóa đơn";
                   
                        RaiseStatus(eMessage, true);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                
                    RaiseStatus(ex.Message, true);
                log.Trace(ex.Message);
                Utility.ShowMsg(ex.Message);
                return false;
            }
        }

        public void xemtruoc_hoadon(BuyerInfor _buyer, ref string eMessage)
        {
            string tranSactionID = "";
            try
            {

                List<MisaPhatHanhHoaDon> lstDataSendInvoiceses = new List<MisaPhatHanhHoaDon>();
                
                log.Trace("Bắt đầu tạo dữ liệu chi tiết cho hóa đơn cho người mua {0}, mặt hàng {1}", _buyer.BuyerFullName, _buyer.TenHangHoa);
                // Tạo dữ liệu chi tiết hóa đơn 
                var lstOriginalinvoicedetail = new List<Originalinvoicedetail>();

                Originalinvoicedetail item = new Originalinvoicedetail();
                item.ItemType = 1;
                item.LineNumber = 1;
                item.ItemCode = "";
                item.ItemName = _buyer.TenHangHoa;
                item.UnitName = _buyer.Donvitinh;
                item.Quantity = 1;
                item.UnitPrice = _buyer.Tongtienhang;
                // thong tin thuế xuất
                item.AmountOC = _buyer.Tongtienhang;
                item.Amount = _buyer.Tongtienhang;
                item.AmountWithoutVATOC = _buyer.Tongtienhang;
                item.AmountWithoutVAT = _buyer.Tongtienhang;
                // thông tin chiết khấu
                item.DiscountRate = 0;
                item.DiscountAmountOC = 0;
                item.DiscountAmount = 0;

                item.VATRateName = _buyer.VAT;
                item.AmountAfterTax = _buyer.ThanhtienDonhang;
                item.VATAmountOC = _buyer.TongtienThue;
                item.VATAmount = _buyer.TongtienThue;
                item.SortOrder = 1;

                lstOriginalinvoicedetail.Add(item);

                // Tạo dữ liệu chi tiết VATRateName 
                var lsttaxrateinfo = new List<Taxrateinfo>();

                Taxrateinfo _Taxrateinfo = new Taxrateinfo();
                _Taxrateinfo.VATRateName = _buyer.VAT;
                _Taxrateinfo.AmountWithoutVATOC = _buyer.Tongtienhang;
                _Taxrateinfo.VATAmountOC = _buyer.TongtienThue;
                lsttaxrateinfo.Add(_Taxrateinfo);

                // Tạo dữ liệu chi tiết #OptionUserDefined  
                var optionuserdefined = new Optionuserdefined();

                optionuserdefined.MainCurrency = "VND";
                optionuserdefined.AmountDecimalDigits = "0";
                optionuserdefined.AmountOCDecimalDigits = "0";
                optionuserdefined.UnitPriceOCDecimalDigits = "0";
                optionuserdefined.UnitPriceDecimalDigits = "0";
                optionuserdefined.QuantityDecimalDigits = "0";
                optionuserdefined.CoefficientDecimalDigits = "0";
                optionuserdefined.ExchangRateDecimalDigits = "0";
                optionuserdefined.ClockDecimalDigits = "0";

                log.Trace("Kết thúc tạo dữ liệu chi tiết hóa đơn");

                MisaPhatHanhHoaDon objdataSendInvoices = new MisaPhatHanhHoaDon();
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
                orginvoicedata.InvDate = Convert.ToDateTime(DateTime.Now, cultures).ToString("yyyy-MM-dd");
                orginvoicedata.CurrencyCode = "VND";
                orginvoicedata.ExchangeRate = 1;
                orginvoicedata.PaymentMethodName = "TM/CK";
                try
                {
                    orginvoicedata.BuyerLegalName = _buyer.BuyerLegalName;
                    orginvoicedata.BuyerTaxCode = _buyer.BuyerTaxCode;
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg(ex.Message);
                    eMessage = ex.Message;
                    return;
                }
                orginvoicedata.BuyerFullName = _buyer.BuyerFullName;
                orginvoicedata.BuyerAddress = _buyer.BuyerAddress;
                orginvoicedata.BuyerEmail = _buyer.BuyerEmail;
                orginvoicedata.ContactName = _buyer.BuyerFullName;

                orginvoicedata.TotalSaleAmountOC = _buyer.Tongtienhang;
                orginvoicedata.TotalSaleAmount = _buyer.Tongtienhang;
                orginvoicedata.TotalDiscountAmount = 0;
                orginvoicedata.TotalDiscountAmountOC = 0;
                orginvoicedata.TotalAmountWithoutVATOC = _buyer.Tongtienhang;
                orginvoicedata.TotalAmountWithoutVAT = _buyer.Tongtienhang;
                orginvoicedata.TotalVATAmountOC = _buyer.TongtienThue;
                orginvoicedata.TotalVATAmount = _buyer.TongtienThue;
                orginvoicedata.TotalAmount = _buyer.ThanhtienDonhang;
                orginvoicedata.TotalAmountOC = _buyer.ThanhtienDonhang;
                orginvoicedata.TotalAmountInWords = Utility.DocSoThanhChu(_buyer.ThanhtienDonhang.ToString("####"));
                orginvoicedata.OriginalInvoiceDetail = lstOriginalinvoicedetail;
                orginvoicedata.IsTaxReduction43 = false;
                orginvoicedata.CustomField1 = "";
                orginvoicedata.TaxRateInfo = lsttaxrateinfo;
                orginvoicedata.OptionUserDefined = optionuserdefined;
                orginvoicedata.IsInvoiceCalculatingMachine = true;
                // tạo ra object DataSendInvoices 
                objdataSendInvoices.InvoiceData = new List<Orginvoicedata>() { orginvoicedata };
                objdataSendInvoices.SignType = 2;

                lstDataSendInvoiceses.Add(objdataSendInvoices);
                string url = InvoiceServicesUrl + Utility.sDbnull(apiMisaPreviewInvoice, "/api/MisaInvoice/xemtruoc_hoadon");
                const string contentType = "application/json";
                string sDataRequest = JsonConvert.SerializeObject(orginvoicedata);
                log.Trace("sDataRequest:" + sDataRequest);
                RaiseStatus(string.Format("Đang gửi lệnh xem trước hóa đơn. Vui lòng chờ..."), false);
                string result = CreateRequest.WebRequest(url, sDataRequest, "", "POST", contentType);
                if (result != null)
                {
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
            catch (Exception ex)
            {

                RaiseStatus(ex.Message, true);
                log.Trace(ex.Message);
                Utility.ShowMsg(ex.Message);
                return;
            }
        }
        void RaiseStatus(string msg, bool isErr)
        {
            log.Trace(msg);
            if (_OnStatus != null)
                _OnStatus(msg, isErr);
        }
        public bool phathanh_hoadonthaythe(string str_IdThanhtoan, int kieu, string lstIdThanhtoanChitiet, string lydohuy,ref string eMessage)
        {
            string tranSactionID = "";
            int num = 0;
            try
            {

                KcbThanhtoan objThanhtoan = null;
                List<string> stringList = str_IdThanhtoan.Split(',').ToList<string>();
                List<long> lstID = stringList.ConvertAll(long.Parse);

                List<string> stringListchitiet = lstIdThanhtoanChitiet.Split(',').ToList<string>();
                List<long> lstIDChitiet = stringListchitiet.ConvertAll(long.Parse);
                HoadonLog hoadon_thaythe = new Select().From(HoadonLog.Schema).Where(HoadonLog.Columns.IdThanhtoan).IsEqualTo(str_IdThanhtoan).And(HoadonLog.Columns.LoaiHoadon).IsEqualTo(0).ExecuteSingle<HoadonLog>();
                if(hoadon_thaythe==null)
                {
                    eMessage= string.Format("Không tìm thấy hóa đơn phát hành cho phiếu thanh toán {0}", str_IdThanhtoan);
                    RaiseStatus(eMessage, true);
                    log.Trace(eMessage);
                    return false;
                }    
                log.Trace(string.Format("------------Bắt đầu tạo hóa đơn cho id_thanhtoan={0}---------------", str_IdThanhtoan));
                if (kieu == 0)
                {
                    objThanhtoan = KcbThanhtoan.FetchByID(lstID[0]);
                    if (objThanhtoan == null)
                    {
                        string.Format("Không tồn tại phiếu thanh toán với id_thanhtoan={0}", str_IdThanhtoan);
                        log.Trace(eMessage);
                        return false;
                    }
                }
                RaiseStatus(string.Format("Đang kiểm tra trạng thái phát hành của hóa đơn..."), false);
                DataSet dtkiemtra = SPs.EInvoiceKiemtraHoadon(lstIdThanhtoanChitiet, 0, kieu).GetDataSet();
                if (dtkiemtra != null && dtkiemtra.Tables.Count > 0 && dtkiemtra.Tables[0].Rows.Count > 0)
                {
                    eMessage = string.Format("Chi tiết thanh toán {0} đã được lấy hóa đơn. Vui lòng làm mới lại dữ liệu", lstIdThanhtoanChitiet);
                    log.Trace(eMessage);
                    return false;
                }
                else
                {

                    log.Trace(eMessage);
                }

                RaiseStatus(string.Format("Đang lấy dữ liệu để phát hành hóa đơn..."), false);
                DataSet ds = SPs.EInvoiceLaydulieuTaohoadon(Utility.sDbnull(str_IdThanhtoan), kieu, lstIdThanhtoanChitiet, 0, VAT).GetDataSet();
                DataTable dtOrginvoicedata = ds.Tables[0];
                DataTable dtOriginalinvoicedetail = ds.Tables[1];
                DataTable dtTaxRateInfo = ds.Tables[2];
                DataTable dtOptionUserDefined = ds.Tables[3];
                DataTable dKcbThanhtoan = ds.Tables[4];
                if (dtOrginvoicedata.Rows.Count <= 0 || dtOriginalinvoicedetail.Rows.Count <= 0)
                {
                    eMessage = "Không lấy được dữ liệu tạo hóa đơn phát hành từ phiếu thanh toán đang chọn dtOrginvoicedata.Rows.Count <= 0";
                    log.Trace(eMessage);
                    return false;
                }
                ReplaceBuyerInfor(ref dtOrginvoicedata);
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

                    log.Trace("Kết thúc tạo dữ liệu chi tiết hóa đơn");

                    // Thực hiện tạo data hóa đơn để gửi đi  
                    List<MisaPhatHanhHoaDon> lstDataSendInvoiceses = new List<MisaPhatHanhHoaDon>();
                    //var lstOrginvoicedata = new List<Orginvoicedata>();

                    //RaiseStatus(string.Format("Đang lấy dữ liệu mẫu hóa đơn..."), false);
                    //TemplateData thongbaophathanh = lay_danhsach_mauhoadon(InvoiceType);
                    //if (thongbaophathanh != null)
                    //{
                    //    invInvoiceSeries = thongbaophathanh.InvSeries;
                    //    mauHd = thongbaophathanh.IPTemplateID;
                    //    invoiceName = thongbaophathanh.TemplateName;
                    //}
                    //else
                    //{
                    //    eMessage = "Không tồn tại thông báo phát hành hóa đơn";
                    //    return false;
                    //}

                    MisaPhatHanhHoaDon objdataSendInvoices = new MisaPhatHanhHoaDon();
                    Orginvoicedata orginvoicedata = new Orginvoicedata();
                    CultureInfo cultures = new CultureInfo("en-US");
                    if (Utility.sDbnull(invInvoiceSeries, "") == "")
                    {

                        eMessage = "Ký hiệu hóa đơn không được để trống";
                        log.Trace(eMessage);
                        return false;
                    }
                    orginvoicedata.RefID = Guid.NewGuid().ToString();
                    //Thông tin hóa đơn bị thay thế
                    //orginvoicedata.ReferenceType =1;//Tính chất hóa đơn 1: Thay thế 2: Điều chỉnh

                    //orginvoicedata.OrgInvoiceType =1; //Loại hóa đơn bị thay thế/ điều chỉnh 1: Hóa đơn NĐ 123 3: Hóa đơn NĐ 51

                    //orginvoicedata.OrgInvTemplateNo = hoadon_thaythe.KiHieu.Substring(0,1);
                    //orginvoicedata.OrgInvSeries = hoadon_thaythe.KiHieu.Substring(1);
                    //orginvoicedata.OrgInvNo = hoadon_thaythe.Serie;
                    //orginvoicedata.OrgInvDate = hoadon_thaythe.NgayHoadon.Value.ToString("yyyy-MM-dd");
                    //orginvoicedata.InvoiceNote = lydohuy;
                    //Các thông tin chung
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
                    orginvoicedata.AccountObjectIdentificationNumber = Utility.sDbnull(dtOrginvoicedata.Rows[0]["CMT"]);
                    orginvoicedata.BuyerFullName = Utility.sDbnull(dtOrginvoicedata.Rows[0]["BuyerFullName"]);
                    orginvoicedata.BuyerAddress = Utility.sDbnull(dtOrginvoicedata.Rows[0]["BuyerAddress"]);
                    orginvoicedata.BuyerEmail = Utility.sDbnull(dtOrginvoicedata.Rows[0]["BuyerEmail"]);
                    orginvoicedata.ContactName = Utility.sDbnull(dtOrginvoicedata.Rows[0]["ContactName"]);

                    orginvoicedata.TotalAmountWithoutVATOC = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalAmountWithoutVATOC)", "1=1"), 0);
                    orginvoicedata.TotalVATAmountOC = Utility.DecimaltoDbnull(dtTaxRateInfo.Rows[0]["VATAmountOC"]);// Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalVATAmountOC)", "1=1"), 0);
                    orginvoicedata.TotalAmount = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalAmount)", "1=1"), 0);
                    orginvoicedata.TotalAmountWithoutVAT = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalAmountWithoutVAT)", "1=1"), 0);
                    orginvoicedata.TotalVATAmount = Utility.DecimaltoDbnull(dtTaxRateInfo.Rows[0]["VATAmountOC"]);// Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalVATAmount)", "1=1"), 0);
                    orginvoicedata.TotalSaleAmountOC = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalSaleAmountOC)", "1=1"), 0);
                    orginvoicedata.TotalSaleAmount = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalSaleAmount)", "1=1"), 0);
                    orginvoicedata.TotalAmountOC = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalAmountOC)", "1=1"), 0);
                    orginvoicedata.TotalDiscountAmount = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalDiscountAmount)", "1=1"), 0);
                    orginvoicedata.TotalDiscountAmountOC = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalDiscountAmountOC)", "1=1"), 0);
                    orginvoicedata.TotalAmountInWords = Utility.DocSoThanhChu(Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalAmountInWords)", "1=1"), 0).ToString("####"));
                    orginvoicedata.OriginalInvoiceDetail = lstOriginalinvoicedetail;
                    orginvoicedata.IsTaxReduction43 = Utility.Int32Dbnull(dtOrginvoicedata.Rows[0]["IsTaxReduction43"], 0) == 1;
                    orginvoicedata.CustomField1 = Utility.sDbnull(dtOrginvoicedata.Rows[0]["CustomField1"], "");
                    orginvoicedata.TaxRateInfo = lsttaxrateinfo;
                    orginvoicedata.OptionUserDefined = optionuserdefined;
                    orginvoicedata.IsInvoiceCalculatingMachine = true;
                    // tạo ra object DataSendInvoices 
                    objdataSendInvoices.InvoiceData = new List<Orginvoicedata>() { orginvoicedata };
                    objdataSendInvoices.SignType = 2;

                    lstDataSendInvoiceses.Add(objdataSendInvoices);
                    string url = InvoiceServicesUrl + Utility.sDbnull(apiMisaPhathanh, "/api/MisaInvoice/phathanh_hoadon");
                    const string contentType = "application/json";
                    string sDataRequest = JsonConvert.SerializeObject(objdataSendInvoices);


                    log.Trace("sDataRequest:" + sDataRequest);
                    RaiseStatus(string.Format("Đang gửi lệnh phát hành hóa đơn. Vui lòng chờ..."), false);
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
                                RaiseStatus(eMessage, true);
                                return false;
                            }
                            ResponseMinvoices objMinvoices = lstMinvoices[0];

                            if (objMinvoices != null && Utility.sDbnull(objMinvoices.TransactionID, "") != "")
                            {
                                transaction_id = Utility.sDbnull(objMinvoices.TransactionID, "");
                                RaiseStatus(string.Format("Đã phát hành thành công. Đang lưu trữ dữ liệu phát hành..."), false);
                                using (var scope = new TransactionScope())
                                {
                                    using (var sh = new SharedDbConnectionScope())
                                    {
                                        tranSactionID = objMinvoices.TransactionID;
                                        if (kieu == 0)//Hóa đơn theo từng thanh toán
                                        {


                                            foreach (DataRow row in dKcbThanhtoan.AsEnumerable())
                                            {
                                                HoadonLog objhoalog = new HoadonLog();
                                                objhoalog.IdThanhtoan = Utility.sDbnull(row["id_thanhtoan"]);
                                                objhoalog.TongTien = Utility.DecimaltoDbnull(row["SOTIEN"]);
                                                objhoalog.IdBenhnhan = Utility.Int32Dbnull(row["id_benhnhan"]);
                                                objhoalog.MaLuotkham = Utility.sDbnull(row["ma_luotkham"]);
                                                objhoalog.MauHoadon = mauHd;
                                                objhoalog.KiHieu = Utility.sDbnull(objMinvoices.InvSeries, invInvoiceSeries);
                                                objhoalog.IdCapphat = -1;
                                                objhoalog.MaQuyen = mauHd;
                                                objhoalog.Serie = Utility.sDbnull(objMinvoices.InvNo);
                                                objhoalog.MaNhanvien = globalVariables.UserName;
                                                objhoalog.MaLydo = string.Empty;
                                                objhoalog.NgayIn = THU_VIEN_CHUNG.GetSysDateTime();
                                                objhoalog.NgayTao = DateTime.Now;
                                                objhoalog.NgayHoadon= DateTime.Now;
                                                objhoalog.NguoiTao = globalVariables.UserName;
                                                objhoalog.IpMaytao = THU_VIEN_CHUNG.GetIP4Address();
                                                objhoalog.MacMaytao = THU_VIEN_CHUNG.GetMACAddress();
                                                objhoalog.DaGui = Utility.Bool2byte(true);
                                                objhoalog.TrangThai = 0;
                                                objhoalog.TthaiHuy = false;
                                                objhoalog.QrDatacode = "";
                                                objhoalog.TransactionId = objMinvoices.TransactionID;
                                                objhoalog.InvInvoiceCodeId = objMinvoices.TransactionID;
                                                objhoalog.Sobaomat = "";
                                                objhoalog.LoaiXuathdon = 0;//0= phiếu thu;1= phiếu tạm ứng
                                                objhoalog.RefID = objMinvoices.RefID;
                                                objhoalog.InvInvoiceAuthId = objMinvoices.RefID;
                                                objhoalog.HoadonTaotay = false;
                                                objhoalog.BuyerTaxCode = orginvoicedata.BuyerTaxCode;
                                                objhoalog.BuyerFullName = orginvoicedata.BuyerFullName;
                                                objhoalog.BuyerLegalName = orginvoicedata.BuyerLegalName;
                                                objhoalog.BuyerAddress = orginvoicedata.BuyerAddress;
                                                objhoalog.BuyerEmail = orginvoicedata.BuyerEmail;
                                                objhoalog.LoaiHoadon = 0;

                                                objhoalog.Save();
                                                QheHoadondientuPhieuthanhtoan objqhe = new QheHoadondientuPhieuthanhtoan();
                                                objqhe.IdHoadon = objhoalog.IdHdonLog;
                                                objqhe.IdThanhtoan = Utility.Int64Dbnull(row["id_thanhtoan"]);
                                                objqhe.MaTracuu = objMinvoices.TransactionID;
                                                objqhe.Save();
                                                ////Không cần update nữa
                                                //num = new Update(KcbThanhtoan.Schema)
                                                //.Set(KcbThanhtoan.Columns.TthaiXuatHddt).EqualTo(1)
                                                //.Set(KcbThanhtoan.Columns.RefId).EqualTo(objhoalog.RefID)
                                                // .Set(KcbThanhtoan.Columns.TransactionId).EqualTo(objhoalog.TransactionId)
                                                //.Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objhoalog.IdThanhtoan)
                                                //.Execute();

                                                string sResult = string.Format("Xuất hóa đơn thay thế thành công cho các lần thanh toán {0} với serie {1}, mẫu hóa đơn {2}, tên mẫu {3}, transaction_id {4} và RefId {5}", objhoalog.IdThanhtoan, objhoalog.Serie, objhoalog.MauHoadon, invoiceName, objhoalog.TransactionId, objhoalog.RefID);
                                                Utility.Log("MisaInvoice", globalVariables.UserName, sResult, newaction.Upload, "Service");
                                                log.Trace(sResult);
                                            }

                                        }
                                        //else//Hóa đơn theo ma_luotkham hoặc id_benhnhan
                                        //{
                                        //    DataRow row = dKcbThanhtoan.AsEnumerable().FirstOrDefault();
                                        //    HoadonLog objhoalog = new HoadonLog();
                                        //    objhoalog.IdThanhtoan = str_IdThanhtoan;
                                        //    objhoalog.TongTien = Utility.DecimaltoDbnull(orginvoicedata.TotalAmount);
                                        //    objhoalog.IdBenhnhan = Utility.Int64Dbnull(row["id_benhnhan"]);
                                        //    objhoalog.MaLuotkham = Utility.sDbnull(row["ma_luotkham"]);
                                        //    objhoalog.MauHoadon = mauHd;
                                        //    objhoalog.KiHieu = Utility.sDbnull(objMinvoices.InvSeries, invInvoiceSeries);
                                        //    objhoalog.IdCapphat = -1;
                                        //    objhoalog.MaQuyen = mauHd;
                                        //    objhoalog.Serie = Utility.sDbnull(objMinvoices.InvNo);
                                        //    objhoalog.MaNhanvien = globalVariables.UserName;
                                        //    objhoalog.MaLydo = string.Empty;
                                        //    objhoalog.NgayIn = THU_VIEN_CHUNG.GetSysDateTime();
                                        //    objhoalog.NgayTao = DateTime.Now;
                                        //    objhoalog.NguoiTao = globalVariables.UserName;
                                        //    objhoalog.IpMaytao = THU_VIEN_CHUNG.GetIP4Address();
                                        //    objhoalog.MacMaytao = THU_VIEN_CHUNG.GetMACAddress();
                                        //    objhoalog.DaGui = Utility.Bool2byte(true);
                                        //    objhoalog.TrangThai = 0;
                                        //    objhoalog.TthaiHuy = false;
                                        //    objhoalog.QrDatacode = "";
                                        //    objhoalog.TransactionId = objMinvoices.TransactionID;
                                        //    objhoalog.InvInvoiceCodeId = objMinvoices.TransactionID;
                                        //    objhoalog.Sobaomat = "";
                                        //    objhoalog.LoaiXuathdon = 1;//0= xuất theo thanh toán;1= xuất theo lượt khám
                                        //    objhoalog.RefID = objMinvoices.RefID;
                                        //    objhoalog.InvInvoiceAuthId = objMinvoices.RefID;

                                        //    objhoalog.BuyerTaxCode = orginvoicedata.BuyerTaxCode;
                                        //    objhoalog.BuyerFullName = orginvoicedata.BuyerFullName;
                                        //    objhoalog.BuyerLegalName = orginvoicedata.BuyerLegalName;
                                        //    objhoalog.BuyerAddress = orginvoicedata.BuyerAddress;
                                        //    objhoalog.BuyerEmail = orginvoicedata.BuyerEmail;

                                        //    objhoalog.Save();

                                        //    num = new Update(KcbThanhtoan.Schema)
                                        //           .Set(KcbThanhtoan.Columns.TthaiXuatHddt).EqualTo(1)
                                        //           .Set(KcbThanhtoan.Columns.RefId).EqualTo(objhoalog.RefID)
                                        //            .Set(KcbThanhtoan.Columns.TransactionId).EqualTo(objhoalog.TransactionId)
                                        //           .Where(KcbThanhtoan.Columns.IdThanhtoan).In(lstID)
                                        //           .Execute();
                                        //    string sResult = string.Format("Xuất hóa đơn thành công cho các lần thanh toán {0} với serie {1}, mẫu hóa đơn {2}, tên mẫu {3}, transaction_id {4} và RefId {5}", str_IdThanhtoan, objhoalog.Serie, objhoalog.MauHoadon, invoiceName, objhoalog.TransactionId, objhoalog.RefID);
                                        //    Utility.Log("MisaInvoice", globalVariables.UserName, sResult, newaction.Upload, "Service");
                                        //    log.Trace(sResult);
                                        //}
                                        num = new Update(KcbThanhtoanChitiet.Schema)
                                                  .Set(KcbThanhtoanChitiet.Columns.TthaiThaythe).EqualTo(1)
                                                  .Where(KcbThanhtoanChitiet.Columns.IdChitiet).In(lstIDChitiet)
                                                  .Execute();

                                        //// lưu thông tin bảng data_ketqua 
                                        DataKetqua objdatKetqua = new DataKetqua();
                                        objdatKetqua.TransactionID = objMinvoices.TransactionID;
                                        objdatKetqua.RefID = objMinvoices.RefID;
                                        objdatKetqua.InvInvoiceAuthId = objMinvoices.RefID;
                                        objdatKetqua.PaymentId = Utility.sDbnull(str_IdThanhtoan);
                                        objdatKetqua.InvInvoiceType = null;
                                        objdatKetqua.InvInvoiceCodeId = objMinvoices.TransactionID;
                                        objdatKetqua.InvInvoiceSeries = objMinvoices.InvSeries;
                                        objdatKetqua.InvInvoiceNumber = Utility.sDbnull(objMinvoices.InvNo);
                                        objdatKetqua.InvInvoiceName = invoiceName;
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
                                        objdatKetqua.InvBuyerLegalName = orginvoicedata.BuyerLegalName;
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
                                        objdatKetqua.MauHd = mauHd;
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
                                    }
                                    scope.Complete();
                                }
                                return true;
                            }
                            else
                            {
                                eMessage = result;
                                RaiseStatus("Phát hành thành công nhưng Transaction=''-->Xem log để biết chi tiết bản tin bên HĐĐT trả về", true);
                                log.Trace(eMessage);
                                return false;
                            }
                        }
                        else
                        {
                            eMessage = result;
                            RaiseStatus(eMessage, true);
                            log.Trace(eMessage);
                            return false;
                        }
                    }
                    else
                    {
                        eMessage = "Không có dữ liệu để gửi hóa đơn";
                        RaiseStatus(eMessage, true);
                        return false;
                    }

                }
                else
                {
                    eMessage = "Không có dữ liệu để gửi hóa đơn";
                    RaiseStatus(eMessage, true);
                    return false;
                }


            }
            catch (Exception ex)
            {
                RaiseStatus(ex.Message, true);
                log.Trace(ex.Message);
                Utility.ShowMsg(ex.Message);
                return false;
            }
            finally
            {
                if (!string.IsNullOrEmpty(tranSactionID))
                {
                    string eMessageDownload = "";
                    //bool thongbaomofile = THU_VIEN_CHUNG.Laygiatrithamsohethong("MISA_THONGBAOMOFILE", true) == "1";
                    RaiseStatus(string.Format("Đang tải hóa đơn với mã: {0}", tranSactionID), false);
                    bool kt = tai_hoadon(tranSactionID, false, ref eMessageDownload);
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
        public bool phathanh_hoadon(string str_IdThanhtoan, int kieu, string lstIdThanhtoanChitiet, ref string eMessage)
        {
            string tranSactionID = "";
            int num = 0;
            try
            {
                
                KcbThanhtoan objThanhtoan = null;
                List<string> stringList = str_IdThanhtoan.Split(',').ToList<string>();
                List<long> lstID = stringList.ConvertAll(long.Parse);

                List<string> stringListchitiet = lstIdThanhtoanChitiet.Split(',').ToList<string>();
                List<long> lstIDChitiet = stringListchitiet.ConvertAll(long.Parse);

                log.Trace(string.Format("------------Bắt đầu tạo hóa đơn cho id_thanhtoan={0}---------------", str_IdThanhtoan));
                if (kieu == 0)
                {
                    objThanhtoan = KcbThanhtoan.FetchByID(lstID[0]);
                    if (objThanhtoan == null)
                    {
                        string.Format("Không tồn tại phiếu thanh toán với id_thanhtoan={0}", str_IdThanhtoan);
                        log.Trace(eMessage);
                        return false;
                    }
                }
                RaiseStatus(string.Format("Đang kiểm tra trạng thái phát hành của hóa đơn..."), false);
                DataSet dtkiemtra = SPs.EInvoiceKiemtraHoadon(lstIdThanhtoanChitiet, 0, kieu).GetDataSet();
                if (dtkiemtra != null && dtkiemtra.Tables.Count > 0 && dtkiemtra.Tables[0].Rows.Count > 0)
                {
                    eMessage = string.Format("Chi tiết thanh toán {0} đã được lấy hóa đơn. Vui lòng làm mới lại dữ liệu", lstIdThanhtoanChitiet);
                    log.Trace(eMessage);
                    return false;
                }
                else
                {
                  
                    log.Trace(eMessage);
                }
                
                RaiseStatus(string.Format("Đang lấy dữ liệu để phát hành hóa đơn..."), false);
                DataSet ds = SPs.EInvoiceLaydulieuTaohoadon(Utility.sDbnull(str_IdThanhtoan), kieu, lstIdThanhtoanChitiet, 0, VAT).GetDataSet();
                DataTable dtOrginvoicedata = ds.Tables[0];
                DataTable dtOriginalinvoicedetail = ds.Tables[1];
                DataTable dtTaxRateInfo = ds.Tables[2];
                DataTable dtOptionUserDefined = ds.Tables[3];
                DataTable dKcbThanhtoan = ds.Tables[4];
                if (dtOrginvoicedata.Rows.Count <= 0 || dtOriginalinvoicedetail.Rows.Count <= 0)
                {
                    eMessage = "Không lấy được dữ liệu tạo hóa đơn phát hành từ phiếu thanh toán đang chọn dtOrginvoicedata.Rows.Count <= 0";
                    log.Trace(eMessage);
                    return false;
                }
                ReplaceBuyerInfor(ref dtOrginvoicedata);
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

                    log.Trace("Kết thúc tạo dữ liệu chi tiết hóa đơn");

                    // Thực hiện tạo data hóa đơn để gửi đi  
                    List<MisaPhatHanhHoaDon> lstDataSendInvoiceses = new List<MisaPhatHanhHoaDon>();
                    //var lstOrginvoicedata = new List<Orginvoicedata>();
                   
                    //RaiseStatus(string.Format("Đang lấy dữ liệu mẫu hóa đơn..."), false);
                    //TemplateData thongbaophathanh = lay_danhsach_mauhoadon(InvoiceType);
                    //if (thongbaophathanh != null)
                    //{
                    //    invInvoiceSeries = thongbaophathanh.InvSeries;
                    //    mauHd = thongbaophathanh.IPTemplateID;
                    //    invoiceName = thongbaophathanh.TemplateName;
                    //}
                    //else
                    //{
                    //    eMessage = "Không tồn tại thông báo phát hành hóa đơn";
                    //    return false;
                    //}

                    MisaPhatHanhHoaDon objdataSendInvoices = new MisaPhatHanhHoaDon();
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
                    orginvoicedata.AccountObjectIdentificationNumber = Utility.sDbnull(dtOrginvoicedata.Rows[0]["CMT"]);
                    orginvoicedata.BuyerFullName = Utility.sDbnull(dtOrginvoicedata.Rows[0]["BuyerFullName"]);
                    orginvoicedata.BuyerAddress = Utility.sDbnull(dtOrginvoicedata.Rows[0]["BuyerAddress"]);
                    orginvoicedata.BuyerEmail = Utility.sDbnull(dtOrginvoicedata.Rows[0]["BuyerEmail"]);
                    orginvoicedata.ContactName = Utility.sDbnull(dtOrginvoicedata.Rows[0]["ContactName"]);
                    if(_buyer!=null)
                    {
                        List<string> lstEmail = _buyer.ReceiverEmail.Split(';').ToList<string>();
                        orginvoicedata.BuyerLegalName = _buyer.BuyerLegalName;
                        orginvoicedata.BuyerEmail =_buyer.BuyerEmail;
                        orginvoicedata.IsSendEmail = _buyer.IsSendEmail;
                        orginvoicedata.ReceiverEmail =_buyer.ReceiverEmail;
                        orginvoicedata.ReceiverName = _buyer.ReceiverName;
                    }    
                    orginvoicedata.TotalAmountWithoutVATOC = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalAmountWithoutVATOC)","1=1"), 0);
                    orginvoicedata.TotalVATAmountOC = Utility.DecimaltoDbnull(dtTaxRateInfo.Rows[0]["VATAmountOC"]);// Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalVATAmountOC)", "1=1"), 0);
                    orginvoicedata.TotalAmount = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalAmount)", "1=1"), 0);
                    orginvoicedata.TotalAmountWithoutVAT = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalAmountWithoutVAT)", "1=1"), 0);
                    orginvoicedata.TotalVATAmount = Utility.DecimaltoDbnull(dtTaxRateInfo.Rows[0]["VATAmountOC"]);// Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalVATAmount)", "1=1"), 0);
                    orginvoicedata.TotalSaleAmountOC = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalSaleAmountOC)", "1=1"), 0);
                    orginvoicedata.TotalSaleAmount = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalSaleAmount)", "1=1"), 0);
                    orginvoicedata.TotalAmountOC = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalAmountOC)", "1=1"), 0);
                    orginvoicedata.TotalDiscountAmount = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalDiscountAmount)", "1=1"), 0);
                    orginvoicedata.TotalDiscountAmountOC = Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalDiscountAmountOC)", "1=1"), 0);
                    orginvoicedata.TotalAmountInWords = Utility.DocSoThanhChu(Utility.DecimaltoDbnull(dtOrginvoicedata.Compute("sum(TotalAmountInWords)", "1=1"), 0).ToString("####"));
                    orginvoicedata.OriginalInvoiceDetail = lstOriginalinvoicedetail;
                    orginvoicedata.IsTaxReduction43 = Utility.Int32Dbnull(dtOrginvoicedata.Rows[0]["IsTaxReduction43"], 0) == 1;
                    orginvoicedata.CustomField1 = Utility.sDbnull(dtOrginvoicedata.Rows[0]["CustomField1"], "");
                    orginvoicedata.TaxRateInfo = lsttaxrateinfo;
                    orginvoicedata.OptionUserDefined = optionuserdefined;
                    orginvoicedata.IsInvoiceCalculatingMachine = true;
                    // tạo ra object DataSendInvoices 
                    objdataSendInvoices.InvoiceData =new List<Orginvoicedata>() { orginvoicedata };
                    objdataSendInvoices.SignType = 2;
                   
                    lstDataSendInvoiceses.Add(objdataSendInvoices);
                    string url = InvoiceServicesUrl+ Utility.sDbnull(apiMisaPhathanh, "/api/MisaInvoice/phathanh_hoadon");
                    const string contentType = "application/json";
                    string sDataRequest = JsonConvert.SerializeObject(objdataSendInvoices);


                    log.Trace("sDataRequest:" + sDataRequest);
                    RaiseStatus(string.Format("Đang gửi lệnh phát hành hóa đơn. Vui lòng chờ..."), false);
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
                                RaiseStatus(eMessage, true);
                                return false;
                            }
                            ResponseMinvoices objMinvoices = lstMinvoices[0];
                            
                            if (objMinvoices != null && Utility.sDbnull(objMinvoices.TransactionID,"")!="")
                            {
                                transaction_id = Utility.sDbnull(objMinvoices.TransactionID, "");
                                RaiseStatus(string.Format("Đã phát hành thành công. Đang lưu trữ dữ liệu phát hành..."), false);
                                using (var scope = new TransactionScope())
                                {
                                    using (var sh = new SharedDbConnectionScope())
                                    {
                                        tranSactionID = objMinvoices.TransactionID;
                                        if (kieu == 0)//Hóa đơn theo từng thanh toán
                                        {


                                            foreach (DataRow row in dKcbThanhtoan.AsEnumerable())
                                            {
                                                HoadonLog objhoalog = new HoadonLog();
                                                objhoalog.IdThanhtoan = Utility.sDbnull(row["id_thanhtoan"]);
                                                objhoalog.TongTien = Utility.DecimaltoDbnull(row["SOTIEN"]);
                                                objhoalog.IdBenhnhan = Utility.Int32Dbnull(row["id_benhnhan"]);
                                                objhoalog.MaLuotkham = Utility.sDbnull(row["ma_luotkham"]);
                                                objhoalog.MauHoadon = mauHd;
                                                objhoalog.KiHieu = Utility.sDbnull(objMinvoices.InvSeries, invInvoiceSeries);
                                                objhoalog.IdCapphat = -1;
                                                objhoalog.MaQuyen = mauHd;
                                                objhoalog.Serie = Utility.sDbnull(objMinvoices.InvNo);
                                                objhoalog.MaNhanvien = globalVariables.UserName;
                                                objhoalog.MaLydo = string.Empty;
                                                objhoalog.NgayIn = THU_VIEN_CHUNG.GetSysDateTime();
                                                objhoalog.NgayTao = DateTime.Now;
                                                objhoalog.NgayHoadon = DateTime.Now;
                                                objhoalog.NguoiTao = globalVariables.UserName;
                                                objhoalog.IpMaytao = THU_VIEN_CHUNG.GetIP4Address();
                                                objhoalog.MacMaytao = THU_VIEN_CHUNG.GetMACAddress();
                                                objhoalog.DaGui = Utility.Bool2byte(true);
                                                objhoalog.TrangThai = 0;
                                                objhoalog.TthaiHuy = false;
                                                objhoalog.QrDatacode = "";
                                                objhoalog.TransactionId = objMinvoices.TransactionID;
                                                objhoalog.InvInvoiceCodeId = objMinvoices.TransactionID;
                                                objhoalog.Sobaomat = "";
                                                objhoalog.LoaiXuathdon = 0;//0= phiếu thu;1= phiếu tạm ứng
                                                objhoalog.RefID = objMinvoices.RefID;
                                                objhoalog.InvInvoiceAuthId = objMinvoices.RefID;
                                                objhoalog.HoadonTaotay = false;
                                                objhoalog.BuyerTaxCode = orginvoicedata.BuyerTaxCode;
                                                objhoalog.BuyerFullName = orginvoicedata.BuyerFullName;
                                                objhoalog.BuyerLegalName = orginvoicedata.BuyerLegalName;
                                                objhoalog.BuyerAddress = orginvoicedata.BuyerAddress;
                                                objhoalog.BuyerEmail = orginvoicedata.BuyerEmail;
                                                objhoalog.LoaiHoadon = 0;

                                                objhoalog.Save();
                                                QheHoadondientuPhieuthanhtoan objqhe = new QheHoadondientuPhieuthanhtoan();
                                                objqhe.IdHoadon = objhoalog.IdHdonLog;
                                                objqhe.IdThanhtoan = Utility.Int64Dbnull(row["id_thanhtoan"]);
                                                objqhe.MaTracuu = objMinvoices.TransactionID;
                                                objqhe.Save();
                                                    num = new Update(KcbThanhtoan.Schema)
                                                    .Set(KcbThanhtoan.Columns.TthaiXuatHddt).EqualTo(1)
                                                    .Set(KcbThanhtoan.Columns.RefId).EqualTo(objhoalog.RefID)
                                                     .Set(KcbThanhtoan.Columns.TransactionId).EqualTo(objhoalog.TransactionId)
                                                     .Set(KcbThanhtoan.Columns.TthaiDangphathanh).EqualTo(0)
                                                     .Set(KcbThanhtoan.Columns.UsedBy).EqualTo("")
                                                    .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objhoalog.IdThanhtoan)
                                                    .Execute();
                                               
                                                string sResult = string.Format("Xuất hóa đơn thành công cho các lần thanh toán {0} với serie {1}, mẫu hóa đơn {2}, tên mẫu {3}, transaction_id {4} và RefId {5}", objhoalog.IdThanhtoan, objhoalog.Serie, objhoalog.MauHoadon, invoiceName, objhoalog.TransactionId, objhoalog.RefID);
                                                Utility.Log("MisaInvoice", globalVariables.UserName, sResult, newaction.Upload, "Service");
                                                log.Trace(sResult);
                                            }
                                           
                                        }
                                        else//Hóa đơn theo ma_luotkham hoặc id_benhnhan
                                        {
                                            DataRow row = dKcbThanhtoan.AsEnumerable().FirstOrDefault();
                                            HoadonLog objhoalog = new HoadonLog();
                                            objhoalog.IdThanhtoan = str_IdThanhtoan;
                                            objhoalog.TongTien = Utility.DecimaltoDbnull(orginvoicedata.TotalAmount);
                                            objhoalog.IdBenhnhan = Utility.Int64Dbnull(row["id_benhnhan"]);
                                            objhoalog.MaLuotkham = Utility.sDbnull(row["ma_luotkham"]);
                                            objhoalog.MauHoadon = mauHd;
                                            objhoalog.KiHieu = Utility.sDbnull(objMinvoices.InvSeries, invInvoiceSeries);
                                            objhoalog.IdCapphat = -1;
                                            objhoalog.MaQuyen = mauHd;
                                            objhoalog.Serie = Utility.sDbnull(objMinvoices.InvNo);
                                            objhoalog.MaNhanvien = globalVariables.UserName;
                                            objhoalog.MaLydo = string.Empty;
                                            objhoalog.NgayIn = THU_VIEN_CHUNG.GetSysDateTime();
                                            objhoalog.NgayTao = DateTime.Now;
                                            objhoalog.NguoiTao = globalVariables.UserName;
                                            objhoalog.IpMaytao = THU_VIEN_CHUNG.GetIP4Address();
                                            objhoalog.MacMaytao = THU_VIEN_CHUNG.GetMACAddress();
                                            objhoalog.DaGui = Utility.Bool2byte(true);
                                            objhoalog.TrangThai = 0;
                                            objhoalog.TthaiHuy = false;
                                            objhoalog.QrDatacode = "";
                                            objhoalog.TransactionId = objMinvoices.TransactionID;
                                            objhoalog.InvInvoiceCodeId = objMinvoices.TransactionID;
                                            objhoalog.Sobaomat = "";
                                            objhoalog.LoaiXuathdon = 1;//0= xuất theo thanh toán;1= xuất theo lượt khám
                                            objhoalog.RefID = objMinvoices.RefID;
                                            objhoalog.InvInvoiceAuthId = objMinvoices.RefID;

                                            objhoalog.BuyerTaxCode = orginvoicedata.BuyerTaxCode;
                                            objhoalog.BuyerFullName = orginvoicedata.BuyerFullName;
                                            objhoalog.BuyerLegalName = orginvoicedata.BuyerLegalName;
                                            objhoalog.BuyerAddress = orginvoicedata.BuyerAddress;
                                            objhoalog.BuyerEmail = orginvoicedata.BuyerEmail;

                                            objhoalog.Save();

                                            num = new Update(KcbThanhtoan.Schema)
                                                   .Set(KcbThanhtoan.Columns.TthaiXuatHddt).EqualTo(1)
                                                   .Set(KcbThanhtoan.Columns.RefId).EqualTo(objhoalog.RefID)
                                                    .Set(KcbThanhtoan.Columns.TransactionId).EqualTo(objhoalog.TransactionId)
                                                     .Set(KcbThanhtoan.Columns.TthaiDangphathanh).EqualTo(0)
                                                     .Set(KcbThanhtoan.Columns.UsedBy).EqualTo("")
                                                   .Where(KcbThanhtoan.Columns.IdThanhtoan).In(lstID)
                                                   .Execute();
                                            string sResult = string.Format("Xuất hóa đơn thành công cho các lần thanh toán {0} với serie {1}, mẫu hóa đơn {2}, tên mẫu {3}, transaction_id {4} và RefId {5}",str_IdThanhtoan, objhoalog.Serie, objhoalog.MauHoadon,invoiceName, objhoalog.TransactionId, objhoalog.RefID);
                                            Utility.Log("MisaInvoice", globalVariables.UserName, sResult, newaction.Upload, "Service");
                                            log.Trace(sResult);
                                        }
                                        num = new Update(KcbThanhtoanChitiet.Schema)
                                                  .Set(KcbThanhtoanChitiet.Columns.TthaiXuatHddt).EqualTo(1)
                                                  .Set(KcbThanhtoanChitiet.Columns.RefId).EqualTo(objMinvoices.RefID)
                                                   .Set(KcbThanhtoanChitiet.Columns.TransactionId).EqualTo(objMinvoices.TransactionID)
                                                  .Where(KcbThanhtoanChitiet.Columns.IdChitiet).In(lstIDChitiet)
                                                  .Execute();

                                        //// lưu thông tin bảng data_ketqua 
                                        DataKetqua objdatKetqua = new DataKetqua();
                                        objdatKetqua.TransactionID = objMinvoices.TransactionID;
                                        objdatKetqua.RefID = objMinvoices.RefID;
                                        objdatKetqua.InvInvoiceAuthId = objMinvoices.RefID;
                                        objdatKetqua.PaymentId = Utility.sDbnull(str_IdThanhtoan);
                                        objdatKetqua.InvInvoiceType = null;
                                        objdatKetqua.InvInvoiceCodeId = objMinvoices.TransactionID;
                                        objdatKetqua.InvInvoiceSeries = objMinvoices.InvSeries;
                                        objdatKetqua.InvInvoiceNumber = Utility.sDbnull(objMinvoices.InvNo);
                                        objdatKetqua.InvInvoiceName = invoiceName;
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
                                        objdatKetqua.InvBuyerLegalName = orginvoicedata.BuyerLegalName;
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
                                        objdatKetqua.MauHd = mauHd;
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
                                    }
                                    scope.Complete();
                                }
                                return true;
                            }
                            else
                            {
                                eMessage = result;
                                RaiseStatus("Phát hành thành công nhưng Transaction=''-->Xem log để biết chi tiết bản tin bên HĐĐT trả về", true);
                                log.Trace(eMessage);
                                return false;
                            }
                        }
                        else
                        {
                            eMessage = result;
                            RaiseStatus(eMessage, true);
                            log.Trace(eMessage);
                            return false;
                        }
                    }
                    else
                    {
                        eMessage = "Không có dữ liệu để gửi hóa đơn";
                        RaiseStatus(eMessage, true);
                        return false;
                    }

                }
                else
                {
                    eMessage = "Không có dữ liệu để gửi hóa đơn";
                    RaiseStatus(eMessage, true);
                    return false;
                }


            }
            catch (Exception ex)
            {
                RaiseStatus(ex.Message, true);
                log.Trace(ex.Message);
                Utility.ShowMsg(ex.Message);
                return false;
            }
            finally
            {
                if (!string.IsNullOrEmpty(tranSactionID))
                {
                    string eMessageDownload = "";
                    //bool thongbaomofile = THU_VIEN_CHUNG.Laygiatrithamsohethong("MISA_THONGBAOMOFILE", true) == "1";
                    RaiseStatus(string.Format("Đang tải hóa đơn với mã: {0}", tranSactionID), false);
                    bool kt = tai_hoadon(tranSactionID, false, ref eMessageDownload);
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
        public bool phathanh_hoadon(BuyerInfor _buyer, ref string eMessage)
        {
            string tranSactionID = "";
            int num = 0;
            try
            {
                List<MisaPhatHanhHoaDon> lstDataSendInvoiceses = new List<MisaPhatHanhHoaDon>();
                
                log.Trace("Bắt đầu tạo dữ liệu chi tiết cho hóa đơn cho người mua {0}, mặt hàng {1}", _buyer.BuyerFullName, _buyer.TenHangHoa);
                // Tạo dữ liệu chi tiết hóa đơn 
                var lstOriginalinvoicedetail = new List<Originalinvoicedetail>();

                Originalinvoicedetail item = new Originalinvoicedetail();
                item.ItemType = 1;
                item.LineNumber = 1;
                item.ItemCode = "";
                item.ItemName = _buyer.TenHangHoa;
                item.UnitName = _buyer.Donvitinh;
                item.Quantity = 1;
                item.UnitPrice = _buyer.Tongtienhang;
                
                item.AmountOC = _buyer.Tongtienhang;
                item.Amount = _buyer.Tongtienhang;
                item.AmountWithoutVATOC = _buyer.ThanhtienDonhang;
                item.AmountWithoutVAT = _buyer.ThanhtienDonhang;
                // thông tin chiết khấu
                item.DiscountRate = 0;
                item.DiscountAmountOC = 0;
                item.DiscountAmount = 0;

                item.VATRateName = "KCT";// Utility.DoTrim( _buyer.VAT.Replace("%",""));
                item.AmountAfterTax = _buyer.Tongtienhang;
                item.VATAmountOC = _buyer.TongtienThue;
                item.VATAmount = _buyer.TongtienThue;
                item.SortOrder = 1;

                lstOriginalinvoicedetail.Add(item);

                // Tạo dữ liệu chi tiết VATRateName 
                var lsttaxrateinfo = new List<Taxrateinfo>();

                Taxrateinfo _Taxrateinfo = new Taxrateinfo();
                _Taxrateinfo.VATRateName = _buyer.VAT;
                _Taxrateinfo.AmountWithoutVATOC = _buyer.Tongtienhang;
                _Taxrateinfo.VATAmountOC = _buyer.TongtienThue;
                lsttaxrateinfo.Add(_Taxrateinfo);

                // Tạo dữ liệu chi tiết #OptionUserDefined  
                var optionuserdefined = new Optionuserdefined();

                optionuserdefined.MainCurrency = "VND";
                optionuserdefined.AmountDecimalDigits = "0";
                optionuserdefined.AmountOCDecimalDigits = "0";
                optionuserdefined.UnitPriceOCDecimalDigits = "0";
                optionuserdefined.UnitPriceDecimalDigits = "0";
                optionuserdefined.QuantityDecimalDigits = "0";
                optionuserdefined.CoefficientDecimalDigits = "0";
                optionuserdefined.ExchangRateDecimalDigits = "0";
                optionuserdefined.ClockDecimalDigits = "0";

                log.Trace("Kết thúc tạo dữ liệu chi tiết hóa đơn");

                MisaPhatHanhHoaDon objdataSendInvoices = new MisaPhatHanhHoaDon();
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
                orginvoicedata.InvDate = Convert.ToDateTime(DateTime.Now, cultures).ToString("yyyy-MM-dd");
                orginvoicedata.CurrencyCode = "VND";
                orginvoicedata.ExchangeRate = 1;
                orginvoicedata.PaymentMethodName = "TM/CK";
                try
                {
                    orginvoicedata.BuyerLegalName = _buyer.BuyerLegalName;
                    orginvoicedata.BuyerTaxCode = _buyer.BuyerTaxCode;
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg(ex.Message);
                    eMessage = ex.Message;
                    return false;
                }
                orginvoicedata.BuyerFullName = _buyer.BuyerFullName;
                orginvoicedata.BuyerAddress = _buyer.BuyerAddress;
                orginvoicedata.BuyerEmail = _buyer.BuyerEmail;
                orginvoicedata.ContactName = _buyer.BuyerFullName;
                orginvoicedata.BuyerIDNumber = _buyer.BuyerIDNumber;
                orginvoicedata.TotalSaleAmountOC = _buyer.Tongtienhang;
                orginvoicedata.TotalSaleAmount = _buyer.Tongtienhang;
                orginvoicedata.TotalDiscountAmount = 0;
                orginvoicedata.TotalDiscountAmountOC = 0;
                orginvoicedata.TotalAmountWithoutVATOC = _buyer.Tongtienhang;
                orginvoicedata.TotalAmountWithoutVAT = _buyer.Tongtienhang;
                orginvoicedata.TotalVATAmountOC = _buyer.TongtienThue;
                orginvoicedata.TotalVATAmount = _buyer.TongtienThue;
                orginvoicedata.TotalAmount = _buyer.ThanhtienDonhang;
                orginvoicedata.TotalAmountOC = _buyer.ThanhtienDonhang;
                orginvoicedata.TotalAmountInWords = Utility.DocSoThanhChu(_buyer.ThanhtienDonhang.ToString("####"));
                orginvoicedata.OriginalInvoiceDetail = lstOriginalinvoicedetail;
                orginvoicedata.IsTaxReduction43 = false;
                orginvoicedata.CustomField1 = "";
                orginvoicedata.TaxRateInfo = lsttaxrateinfo;
                orginvoicedata.OptionUserDefined = optionuserdefined;
                orginvoicedata.IsInvoiceCalculatingMachine = true;
                // tạo ra object DataSendInvoices 
                objdataSendInvoices.InvoiceData = new List<Orginvoicedata>() { orginvoicedata };
                objdataSendInvoices.SignType = 2;

                lstDataSendInvoiceses.Add(objdataSendInvoices);
                string url = InvoiceServicesUrl + Utility.sDbnull(apiMisaPhathanh, "/api/MisaInvoice/phathanh_hoadon");
                const string contentType = "application/json";
                string sDataRequest = JsonConvert.SerializeObject(objdataSendInvoices);


                log.Trace("sDataRequest:" + sDataRequest);
                RaiseStatus(string.Format("Đang gửi lệnh phát hành hóa đơn. Vui lòng chờ..."), false);
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
                            RaiseStatus(eMessage, true);
                            return false;
                        }
                        ResponseMinvoices objMinvoices = lstMinvoices[0];

                        if (objMinvoices != null && Utility.sDbnull(objMinvoices.TransactionID, "") != "")
                        {
                            transaction_id = Utility.sDbnull(objMinvoices.TransactionID, "");
                            RaiseStatus(string.Format("Đã phát hành thành công. Đang lưu trữ dữ liệu phát hành..."), false);
                            using (var scope = new TransactionScope())
                            {
                                using (var sh = new SharedDbConnectionScope())
                                {
                                    tranSactionID = objMinvoices.TransactionID;


                                    HoadonLog objhoalog = new HoadonLog();
                                    objhoalog.IdThanhtoan = "-1";
                                    objhoalog.TongTien = _buyer.ThanhtienDonhang;
                                    objhoalog.IdBenhnhan = _buyer.Id_benhnhan;
                                    objhoalog.MaLuotkham = _buyer.MaLuotkham;
                                    objhoalog.MauHoadon = mauHd;
                                    objhoalog.KiHieu = Utility.sDbnull(objMinvoices.InvSeries, invInvoiceSeries);
                                    objhoalog.IdCapphat = -1;
                                    objhoalog.MaQuyen = mauHd;
                                    objhoalog.Serie = Utility.sDbnull(objMinvoices.InvNo);
                                    objhoalog.MaNhanvien = globalVariables.UserName;
                                    objhoalog.MaLydo = string.Empty;
                                    objhoalog.NgayIn = THU_VIEN_CHUNG.GetSysDateTime();
                                    objhoalog.NgayTao = DateTime.Now;
                                    objhoalog.NgayHoadon = DateTime.Now;
                                    objhoalog.NguoiTao = globalVariables.UserName;
                                    objhoalog.IpMaytao = THU_VIEN_CHUNG.GetIP4Address();
                                    objhoalog.MacMaytao = THU_VIEN_CHUNG.GetMACAddress();
                                    objhoalog.DaGui = Utility.Bool2byte(true);
                                    objhoalog.TrangThai = 0;
                                    objhoalog.TthaiHuy = false;
                                    objhoalog.QrDatacode = "";
                                    objhoalog.TransactionId = objMinvoices.TransactionID;
                                    objhoalog.InvInvoiceCodeId = objMinvoices.TransactionID;
                                    objhoalog.Sobaomat = "";
                                    objhoalog.LoaiXuathdon = 0;//0= phiếu thu;1= phiếu tạm ứng
                                    objhoalog.RefID = objMinvoices.RefID;
                                    objhoalog.InvInvoiceAuthId = objMinvoices.RefID;

                                    objhoalog.BuyerTaxCode = orginvoicedata.BuyerTaxCode;
                                    objhoalog.BuyerFullName = orginvoicedata.BuyerFullName;
                                    objhoalog.BuyerLegalName = orginvoicedata.BuyerLegalName;
                                    objhoalog.BuyerAddress = orginvoicedata.BuyerAddress;
                                    objhoalog.BuyerEmail = orginvoicedata.BuyerEmail;
                                    objhoalog.HoadonTaotay = true;

                                    objhoalog.Save();
                                   
                                    string sResult = string.Format("Xuất hóa đơn thành công cho các lần thanh toán {0} với serie {1}, mẫu hóa đơn {2}, tên mẫu {3}, transaction_id {4} và RefId {5}", objhoalog.IdThanhtoan, objhoalog.Serie, objhoalog.MauHoadon, invoiceName, objhoalog.TransactionId, objhoalog.RefID);
                                    Utility.Log("MisaInvoice", globalVariables.UserName, sResult, newaction.Upload, "Service");
                                    log.Trace(sResult);

                                    //// lưu thông tin bảng data_ketqua 
                                    DataKetqua objdatKetqua = new DataKetqua();
                                    objdatKetqua.TransactionID = objMinvoices.TransactionID;
                                    objdatKetqua.RefID = objMinvoices.RefID;
                                    objdatKetqua.InvInvoiceAuthId = objMinvoices.RefID;
                                    objdatKetqua.PaymentId = "-1";
                                    objdatKetqua.InvInvoiceType = null;
                                    objdatKetqua.InvInvoiceCodeId = objMinvoices.TransactionID;
                                    objdatKetqua.InvInvoiceSeries = objMinvoices.InvSeries;
                                    objdatKetqua.InvInvoiceNumber = Utility.sDbnull(objMinvoices.InvNo);
                                    objdatKetqua.InvInvoiceName = invoiceName;
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
                                    objdatKetqua.InvBuyerLegalName = orginvoicedata.BuyerLegalName;
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
                                    objdatKetqua.MauHd = mauHd;
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
                                }
                                scope.Complete();
                            }
                            return true;
                        }
                        else
                        {
                            eMessage = result;
                            RaiseStatus("Phát hành thành công nhưng Transaction=''-->Xem log để biết chi tiết bản tin bên HĐĐT trả về", true);
                            log.Trace(eMessage);
                            return false;
                        }
                    }
                    else
                    {
                        eMessage = result;
                        RaiseStatus(eMessage, true);
                        log.Trace(eMessage);
                        return false;
                    }
                }
                else
                {
                    eMessage = "Không có dữ liệu để gửi hóa đơn";
                    RaiseStatus(eMessage, true);
                    return false;
                }



            }
            catch (Exception ex)
            {
                RaiseStatus(ex.Message, true);
                log.Trace(ex.Message);
                Utility.ShowMsg(ex.Message);
                return false;
            }
            finally
            {
                if (!string.IsNullOrEmpty(tranSactionID))
                {
                    string eMessageDownload = "";
                    //bool thongbaomofile = THU_VIEN_CHUNG.Laygiatrithamsohethong("MISA_THONGBAOMOFILE", true) == "1";
                    RaiseStatus(string.Format("Đang tải hóa đơn với mã: {0}", tranSactionID), false);
                    bool kt = tai_hoadon(tranSactionID, false, ref eMessageDownload);
                    log.Trace(eMessageDownload);
                }
            }
        }
        public bool huy_hoadon(string transaction_id, string InvSeries, DateTime ngay_huy_hoadon, string lydo_huy, ref string eMessage)
        {
            try
            {
                var objThongtinHuyhoadon = new MisaCancelModel();
                objThongtinHuyhoadon.TransactionID = transaction_id;
                objThongtinHuyhoadon.InvSeries = InvSeries;
                objThongtinHuyhoadon.CancelReason = lydo_huy;
                string sDataRequest = JsonConvert.SerializeObject(objThongtinHuyhoadon);
                string url = InvoiceServicesUrl + Utility.sDbnull(apiMisaCancelInvoice, "/api/MisaInvoice/huy_hoadon");
                const string contentType = "application/json";
                log.Trace("sDataRequest: " + sDataRequest);

                string result = CreateRequest.WebRequest(url, sDataRequest, "", "POST", contentType);
                log.Trace("result: " + result);
                var mes = JsonConvert.DeserializeObject<MisaResponse>(result);
                if (mes.Success)
                {
                    eMessage = result;
                    new Update(HoadonLog.Schema)
                        .Set(HoadonLog.Columns.TthaiHuy).EqualTo(1)
                        .Set(HoadonLog.Columns.NgayHuy).EqualTo(globalVariables.SysDate)
                        .Set(HoadonLog.Columns.NguoiHuy).EqualTo(globalVariables.UserName)
                        .Where(HoadonLog.Columns.TransactionId).IsEqualTo(transaction_id)
                        .And(HoadonLog.Columns.TthaiHuy).IsEqualTo(0)
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
        public bool tai_hoadon(string invoiceAuthId, bool isOpen, ref string eMessage)
        {
            try
            {
                downloadModel objDownloadModel = new downloadModel();
                objDownloadModel.userName = globalVariables.UserName;
                objDownloadModel.maTracuu = invoiceAuthId;
                string sDataRequest = JsonConvert.SerializeObject(objDownloadModel);

                string url = InvoiceServicesUrl+ Utility.sDbnull(apiMisaSaveFileInvoice, "/api/MisaInvoice/tai_hoadon");
                const string contentType = "application/json";
                RaiseStatus(string.Format("Đang tải hóa đơn với mã: {0}. Vui lòng chờ trong giây lát...", invoiceAuthId), false);
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
