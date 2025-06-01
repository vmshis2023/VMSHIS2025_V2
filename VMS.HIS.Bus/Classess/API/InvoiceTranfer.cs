using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using NLog;
using VNS.HIS.UI.InvoiceTranfer;
using VNS.Libs;

namespace VNS.HIS.UI.Classess.API
{
    public  class InvoiceTranfer
    {
        public InvoiceTranferClient ObjInvoiceInfoClient;
         private readonly Logger log;

        public InvoiceTranfer()
        {
            BasicHttpBinding basicHttpbinding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            basicHttpbinding.Name = "basicHttpsBinding";
            basicHttpbinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            basicHttpbinding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            EndpointAddress endpointAddress = new EndpointAddress(globalVariables.Invoice_WebPath);
            //_BillingClient.Endpoint.Address = new EndpointAddress(endpointAddress, basicHttpbinding);
            ObjInvoiceInfoClient = new InvoiceTranferClient(basicHttpbinding, endpointAddress);
            log = LogManager.GetLogger("InvoiceTranfer");
        }

        public bool CreateInvoiceTranfer(InvoiceInfo objInvoiceInfo, BuyerInfo objBuyer, SellerInfo objSeller, List<ItemInfo> lstItem, SummarizeInfo objSummary)
        {
            bool result = ObjInvoiceInfoClient.CreateInvoice(objInvoiceInfo, objBuyer, objSeller, lstItem.ToArray(), objSummary);
            if (result)
            {
                log.Trace("Chuyen thanh cong len cong hoa don dien tu so hoa don: " + objInvoiceInfo.invoiceSeries);
            }
            else
            {
                log.Trace("Chuyen khong thanh cong len cong hoa don dien tu so hoa don: " + objInvoiceInfo.invoiceSeries);
            }
            return result;
        }
    }
}
