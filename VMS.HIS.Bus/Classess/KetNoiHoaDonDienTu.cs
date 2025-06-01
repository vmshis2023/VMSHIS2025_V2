using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using VNS.HIS.DAL;
using VNS.HIS.UI.InvoiceTranfer;
using VNS.Libs;

namespace VNS.HIS.UI.Classess
{
    public class KetNoiHoaDonDienTu
    {
        private DataTable _dtBuyerInfo = new DataTable();
        private DataTable _dtInvoiceInfo = new DataTable();
        private DataTable _dtItemInfo = new DataTable();
        private DataTable _dtSellerInfo = new DataTable();
        private DataTable _dtSummarizeInfo = new DataTable();

        private InvoiceInfo CreateInvoiceInfo()
        {
            string invoiceType = null;
            string templateCode = null;
            string invoiceSeries = null;
            string invoiceIssuedDate = null;
            string currencyCode = null;
            string adjustmentType = null;
            string invoiceNote = null;
            string originalInvoiceId = null;
            string adjustmentInvoiceType = null;
            string originalInvoiceIssueDate = null;
            string additionalReferenceDesc = null;
            string additionalReferenceDate = null;
            string paymentStatus = null;
            string paymentType = null;
            string paymentTypeName = null;
            string cusGetInvoiceRight = null;
            string buyerIdType = null;
            string buyerIdNo = null;
            string uuId = null;
            if (_dtInvoiceInfo != null)
            {
                invoiceType = _dtInvoiceInfo.Rows[0]["invoiceType"].ToString();
                templateCode = _dtInvoiceInfo.Rows[0]["templateCode"].ToString();
                invoiceSeries = _dtInvoiceInfo.Rows[0]["invoiceSeries"].ToString();
                invoiceIssuedDate = _dtInvoiceInfo.Rows[0]["invoiceIssuedDate"].ToString();
                currencyCode = _dtInvoiceInfo.Rows[0]["currencyCode"].ToString();
                adjustmentType = _dtInvoiceInfo.Rows[0]["adjustmentType"].ToString();
                invoiceNote = _dtInvoiceInfo.Rows[0]["invoiceNote"].ToString();
                originalInvoiceId = _dtInvoiceInfo.Rows[0]["originalInvoiceId"].ToString();
                adjustmentInvoiceType = _dtInvoiceInfo.Rows[0]["adjustmentInvoiceType"].ToString();
                originalInvoiceIssueDate = _dtInvoiceInfo.Rows[0]["originalInvoiceIssueDate"].ToString();
                additionalReferenceDesc = _dtInvoiceInfo.Rows[0]["additionalReferenceDesc"].ToString();
                additionalReferenceDate = _dtInvoiceInfo.Rows[0]["additionalReferenceDate"].ToString();
                paymentStatus = _dtInvoiceInfo.Rows[0]["paymentStatus"].ToString() == "1" ? "true" : "false";
                paymentType = _dtInvoiceInfo.Rows[0]["paymentType"].ToString();
                paymentTypeName = _dtInvoiceInfo.Rows[0]["paymentTypeName"].ToString();
                cusGetInvoiceRight = _dtInvoiceInfo.Rows[0]["cusGetInvoiceRight"].ToString() == "1" ? "true" : "false";
                buyerIdType = _dtInvoiceInfo.Rows[0]["buyerIdType"].ToString();
                buyerIdNo = _dtInvoiceInfo.Rows[0]["buyerIdNo"].ToString();
                uuId = _dtInvoiceInfo.Rows[0]["uuId"].ToString();
            }
            //MessageBox.Show(((Int64)(Convert.ToDateTime(invoiceIssuedDate).Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds).ToString());
            var objInvoiceInfo = new InvoiceInfo();
            objInvoiceInfo.invoiceType = invoiceType;
            objInvoiceInfo.templateCode = templateCode;
            objInvoiceInfo.invoiceSeries = invoiceSeries;
            objInvoiceInfo.invoiceIssuedDate = ((Int64)(Convert.ToDateTime(DateTime.UtcNow).Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds).ToString();
            objInvoiceInfo.currencyCode = currencyCode;
            objInvoiceInfo.adjustmentType = adjustmentType;
            objInvoiceInfo.invoiceNote = invoiceNote;
            objInvoiceInfo.originalInvoiceId = originalInvoiceId;
            objInvoiceInfo.adjustmentInvoiceType = adjustmentInvoiceType;
            objInvoiceInfo.originalInvoiceIssueDate = originalInvoiceIssueDate;
            objInvoiceInfo.additionalReferenceDate = additionalReferenceDate;
            objInvoiceInfo.additionalReferenceDesc = additionalReferenceDesc;
            objInvoiceInfo.paymentStatus = paymentStatus;
            objInvoiceInfo.paymentType = paymentType;
            objInvoiceInfo.paymentTypeName = paymentTypeName;
            objInvoiceInfo.cusGetInvoiceRight = cusGetInvoiceRight;
            objInvoiceInfo.buyerIdType = buyerIdType;
            objInvoiceInfo.buyerIdNo = buyerIdNo;
            objInvoiceInfo.uuId = System.Guid.NewGuid().ToString();
            return objInvoiceInfo;
        }

        private BuyerInfo CreateBuyerInfo()
        {
            string buyerName = null;
            string buyerLegalName = null;
            string buyerTaxCode = null;
            string buyerAddressLine = null;
            string buyerPhoneNumber = null;
            string buyerEmail = null;
            string buyerFaxNumber = null;
            string buyerIdNo = null;
            string buyerIdType = null;
            string buyerBankName = null;
            string buyerBankAccount = null;
            if (_dtBuyerInfo != null)
            {
                buyerName = _dtBuyerInfo.Rows[0]["buyerName"].ToString();
                buyerLegalName = _dtBuyerInfo.Rows[0]["buyerLegalName"].ToString();
                buyerTaxCode = _dtBuyerInfo.Rows[0]["buyerTaxCode"].ToString();
                buyerAddressLine = _dtBuyerInfo.Rows[0]["buyerAddressLine"].ToString();
                buyerPhoneNumber = _dtBuyerInfo.Rows[0]["buyerPhoneNumber"].ToString();
                buyerEmail = _dtBuyerInfo.Rows[0]["buyerEmail"].ToString();
                buyerFaxNumber = _dtBuyerInfo.Rows[0]["buyerFaxNumber"].ToString();
                buyerBankName = _dtBuyerInfo.Rows[0]["buyerBankName"].ToString();
                buyerBankAccount = _dtBuyerInfo.Rows[0]["buyerBankAccount"].ToString();
                buyerIdNo = _dtInvoiceInfo.Rows[0]["buyerIdNo"].ToString();
                buyerIdType = _dtInvoiceInfo.Rows[0]["buyerIdType"].ToString();
            }
            var objBuyerInfo = new BuyerInfo();
            objBuyerInfo.buyerName = buyerName;
            objBuyerInfo.buyerLegalName = buyerLegalName;
            objBuyerInfo.buyerTaxCode = buyerTaxCode;
            objBuyerInfo.buyerAddressLine = buyerAddressLine;
            objBuyerInfo.buyerPhoneNumber = buyerPhoneNumber;
            objBuyerInfo.buyerEmail = buyerEmail;
            objBuyerInfo.buyerFaxNumber = buyerFaxNumber;
            objBuyerInfo.buyerBankName = buyerBankName;
            objBuyerInfo.buyerBankAccount = buyerBankAccount;
            objBuyerInfo.buyerIdNo = buyerIdNo;
            objBuyerInfo.buyerIdType = buyerIdType;
            return objBuyerInfo;
        }

        private SellerInfo CreateSellerInfo()
        {
            string sellerLegalName = null;
            string sellerTaxCode = null;
            string sellerAddressLine = null;
            string sellerPhoneNumber = null;
            string sellerEmail = null;
            string sellerBankName = null;
            string sellerBankAccount = null;

            var objSellerInfo = new SellerInfo();
            objSellerInfo.sellerLegalName = sellerLegalName;
            objSellerInfo.sellerTaxCode = sellerTaxCode;
            objSellerInfo.sellerAddressLine = sellerAddressLine;
            objSellerInfo.sellerPhoneNumber = sellerPhoneNumber;
            objSellerInfo.sellerEmail = sellerEmail;
            objSellerInfo.sellerBankName = sellerBankName;
            objSellerInfo.sellerBankAccount = sellerBankAccount;
            return objSellerInfo;
        }

        private List<ItemInfo> CreateItemInfo()
        {
            var _listItemInfo = new List<ItemInfo>();
            foreach (DataRow row in _dtItemInfo.AsEnumerable())
            {
                var objItemInfo = new ItemInfo();
                objItemInfo.lineNumber = row["lineNumber"].ToString();
                objItemInfo.itemCode = row["itemCode"].ToString();
                objItemInfo.itemName = row["itemName"].ToString();
                objItemInfo.unitName = row["unitName"].ToString();
                objItemInfo.unitPrice = row["unitPrice"].ToString();
                objItemInfo.quantity = row["quantity"].ToString();
                objItemInfo.itemTotalAmountWithoutTax = row["itemTotalAmountWithoutTax"].ToString();
                objItemInfo.taxPercentage = row["taxPercentage"].ToString();
                objItemInfo.taxAmount = row["taxAmount"].ToString();
                objItemInfo.discount = row["discount"].ToString();
                objItemInfo.itemDiscount = row["itemDiscount"].ToString();
                objItemInfo.adjustmentTaxAmount = row["adjustmentTaxAmount"].ToString();
                objItemInfo.isIncreaseItem = row["isIncreaseItem"].ToString();
                _listItemInfo.Add(objItemInfo);
            }
            return _listItemInfo;
        }

        private SummarizeInfo CreateSummarizeInfo()
        {
            string sumOfTotalLineAmountWithoutTax = null;
            string totalAmountWithoutTax = null;
            string totalTaxAmount = null;
            string totalAmountWithTax = null;
            string totalAmountWithTaxInWords = null;
            string discountAmount = null;
            string settlementDiscountAmount = null;
            string taxPercentage = null;
            if (_dtSummarizeInfo != null)
            {
                sumOfTotalLineAmountWithoutTax = _dtSummarizeInfo.Rows[0]["sumOfTotalLineAmountWithoutTax"].ToString();
                totalAmountWithoutTax = _dtSummarizeInfo.Rows[0]["totalAmountWithoutTax"].ToString();
                totalTaxAmount = _dtSummarizeInfo.Rows[0]["totalTaxAmount"].ToString();
                totalAmountWithTax = _dtSummarizeInfo.Rows[0]["totalAmountWithTax"].ToString();
                totalAmountWithTaxInWords =
                    Utility.DocSoThanhChu(_dtSummarizeInfo.Rows[0]["totalAmountWithTaxInWords"].ToString());
                discountAmount = _dtSummarizeInfo.Rows[0]["discountAmount"].ToString();
                settlementDiscountAmount = _dtSummarizeInfo.Rows[0]["settlementDiscountAmount"].ToString();
                taxPercentage = _dtSummarizeInfo.Rows[0]["taxPercentage"].ToString();
            }
            var objSummarizeInfo = new SummarizeInfo();
            objSummarizeInfo.sumOfTotalLineAmountWithoutTax = sumOfTotalLineAmountWithoutTax;
            objSummarizeInfo.totalAmountWithoutTax = totalAmountWithoutTax;
            objSummarizeInfo.totalTaxAmount = totalTaxAmount;
            objSummarizeInfo.totalAmountWithTax = totalAmountWithTax;
            objSummarizeInfo.totalAmountWithTaxInWords = totalAmountWithTaxInWords;
            objSummarizeInfo.discountAmount = discountAmount;
            objSummarizeInfo.settlementDiscountAmount = settlementDiscountAmount;
            objSummarizeInfo.taxPercentage = taxPercentage;
            return objSummarizeInfo;
        }
        public bool SendInvoice(string sohoadon, int ingop)
        {
            try
            {
                bool kt = false;
                if (ingop == 0)
                {
                    DataSet dsHoaDon = SPs.HoahonLaythongtinGuilencong(sohoadon, ingop).GetDataSet();
                    _dtBuyerInfo = dsHoaDon.Tables[0];
                    _dtItemInfo = dsHoaDon.Tables[1];
                    _dtSummarizeInfo = dsHoaDon.Tables[2];
                    _dtInvoiceInfo = dsHoaDon.Tables[3];
                    InvoiceInfo objInvoiceInfo = CreateInvoiceInfo();
                    BuyerInfo objBuyerInfo = CreateBuyerInfo();
                    SellerInfo objSellerInfo = CreateSellerInfo();
                    List<ItemInfo> listItemInfos = CreateItemInfo();
                    SummarizeInfo objSummarizeInfo = CreateSummarizeInfo();
                    kt = new API.InvoiceTranfer().CreateInvoiceTranfer(objInvoiceInfo, objBuyerInfo, objSellerInfo, listItemInfos,objSummarizeInfo);
                }
                else
                {
                    DataSet dsHoaDon = SPs.HoahonLaythongtinGuilencong(sohoadon, ingop).GetDataSet();
                    _dtBuyerInfo = dsHoaDon.Tables[0];
                    _dtItemInfo = dsHoaDon.Tables[1];
                    _dtSummarizeInfo = dsHoaDon.Tables[2];
                    _dtInvoiceInfo = dsHoaDon.Tables[3];
                    InvoiceInfo objInvoiceInfo = CreateInvoiceInfo();
                    BuyerInfo objBuyerInfo = CreateBuyerInfo();
                    SellerInfo objSellerInfo = CreateSellerInfo();
                    List<ItemInfo> listItemInfos = CreateItemInfo();
                    SummarizeInfo objSummarizeInfo = CreateSummarizeInfo();
                    kt = new API.InvoiceTranfer().CreateInvoiceTranfer(objInvoiceInfo, objBuyerInfo, objSellerInfo, listItemInfos, objSummarizeInfo); 
                }
                return kt;
            }
            catch (Exception)
            {
                return false;
            }
          
         
        }
    }
}