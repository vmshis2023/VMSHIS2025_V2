using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using NLog;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using System.Transactions;
using VMS.HIS.BHYT;

namespace VMS.HIS.Bus.QRPay
{

    public class HDBankConnection
    {

        private readonly Logger log;
        private string kieuView = "";
        private string serialNumber = "";

        public HDBankConnection()
        {


            log = LogManager.GetLogger("HDBankConnection");
            if (Utility.sDbnull(globalVariables.gv_strBankAPILink, "") == "")
            {
                globalVariables.gv_strBankAPILink = THU_VIEN_CHUNG.Laygiatrithamsohethong("QR_BANK_API", "CRC16", false);
                log.Trace("Load URL HDBank = " + globalVariables.gv_strBankAPILink);

            }
            kieuView = THU_VIEN_CHUNG.Laygiatrithamsohethong("QR_TYPE", "CRC16", false);

            serialNumber = THU_VIEN_CHUNG.Laygiatrithamsohethong("QR_SERIALNUM", "ABC...XYZ", false);

        }


        public string CreateQRCode(data_qrcode input)
        {
            string stringQRcode = "";
            try
            {

                GenQRInputDto objGenQrInputDto = new GenQRInputDto();
                objGenQrInputDto.Amount = input.transactionAmount;
                objGenQrInputDto.BillNumber = input.invoiceId;
                objGenQrInputDto.ClientId = globalVariables.gv_strMacAddress;
                objGenQrInputDto.Description = JsonConvert.SerializeObject(input.additionalData);
                objGenQrInputDto.SerialNumber = serialNumber;
                string url = globalVariables.gv_strBankAPILink;
                //string apiLink = url + "/api/HDBank/GenQRCode";
                string apiLink = url + "/api/services/app/HDBankQR/GenQR";
                const string contentType = "application/json";
                const string meThod = "POST";
                string requestdata = JsonConvert.SerializeObject(objGenQrInputDto);
                string result = CreateRequest.WebRequest(apiLink, requestdata, "", meThod, contentType);
                if (!string.IsNullOrEmpty(result))
                {
                    log.Trace("result QR: " + result);
                    ResponseHDBank response = JsonConvert.DeserializeObject<ResponseHDBank>(result);
                    if (response != null && response.isSuccess)
                    {
                        switch (kieuView)
                        {
                            case "CRC16":
                                stringQRcode = response.data.qrData;
                                break;
                            case "BASE64":
                                stringQRcode = response.data.qrDataBase64;
                                break;
                            case "API":
                                stringQRcode = response.data.qrDataBase64;
                                break;
                        }
                    }
                    else
                    {
                        stringQRcode = "";
                    }
                }
                else
                {
                    stringQRcode = "";
                }
                return stringQRcode;
            }
            catch (Exception ex)
            {
                log.Trace(ex.Message);
                return null;
            }
        }
        public ActionResult LuuThongTinQRcode(QrPhieuThanhtoan objDatum, List<QrPhieuThanhtoanChitiet> _lstDataDetails,
         ref string eMessage)
        {
            try
            {
                var option = new TransactionOptions();
                option.IsolationLevel = System.Transactions.IsolationLevel.Snapshot;
                option.Timeout = TimeSpan.FromMinutes(5);
                using (var scope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        if (objDatum != null)
                        {
                            objDatum.IsNew = true;
                            objDatum.Save();
                            string idQrdata = Utility.sDbnull(objDatum.IdQrCode);
                            foreach (QrPhieuThanhtoanChitiet objDataDetail in _lstDataDetails)
                            {
                                objDataDetail.IdQrcode = idQrdata;
                                objDataDetail.IsNew = true;
                                objDataDetail.Save();
                            }
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                eMessage = ex.Message;
                return ActionResult.Error;
            }
        }
    }
    public class GenQRInputDto
    {
        public string BillNumber { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string ClientId { get; set; }
        public string SerialNumber { get; set; }
    }
    public class GenQROutputDto
    {
        public string qrData { get; set; }
        public string qrDataBase64 { get; set; }

    }
    public class SingleResponse
    {
        public string Message { get; set; }

        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public object Data { get; set; }
    }
    public class ResponseHDBank
    {
        public object message { get; set; }
        public bool isSuccess { get; set; }
        public object errorMessage { get; set; }
        public DataQR data { get; set; }
    }

    public class DataQR
    {
        public string qrData { get; set; }
        public string qrDataBase64 { get; set; }
    }
    public class data_qrcode
    {
        public string merchantId { get; set; }
        public string invoiceId { get; set; }
        public string type { get; set; }
        public decimal transactionAmount { get; set; }
        public Additionaldata additionalData { get; set; }
        public long transactionTime { get; set; }

    }
    public class Additionaldata
    {
        public string serviceCode { get; set; }
        public string patientCode { get; set; }
        public string patientName { get; set; }
        public string description { get; set; }
    }

    public class data_response
    {
        public string merchantId { get; set; }
        public string invoiceId { get; set; }
        public string transactionId { get; set; }
        public string status { get; set; }
        public decimal? transactionAmount { get; set; }
        public decimal? paidAmount { get; set; }
        public string transactionDescription { get; set; }
        public string paidDescription { get; set; }
        public decimal? paidTime { get; set; }
        public object additionaldata { get; set; }
    }
    public class WebSocketHubDto
    {
        public string Method { get; set; }
        public object Data { get; set; }
        public string InvocationId { get; set; }
    }


}
