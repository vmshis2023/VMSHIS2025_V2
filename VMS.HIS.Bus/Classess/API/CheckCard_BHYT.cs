using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using NLog;
using SubSonic;
using VNS.HIS.DAL;
using newBus.CheckCard;
using VNS.Libs;

namespace VNS.HIS.UI.Classess.API
{
    public  class CheckCard_BHYT
    {
        private LichSuKCBClient _lichSuKcbClient;
        private readonly Logger log;

        public CheckCard_BHYT()
        {
            BasicHttpBinding basicHttpbinding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            basicHttpbinding.Name = "BasicHttpBinding_ILichSuKCB";
            basicHttpbinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            basicHttpbinding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            EndpointAddress endpointAddress = new EndpointAddress(globalVariables.BHXH_WebPath);
            //_BillingClient.Endpoint.Address = new EndpointAddress(endpointAddress, basicHttpbinding);
            _lichSuKcbClient = new LichSuKCBClient(basicHttpbinding, endpointAddress);
            log = LogManager.GetLogger("CheckCard_BHYT");
        }

        public KQNhanLichSuKCBBS CheckCard366(ApiTheBHYT objApiTheBhyt)
        {
            KQNhanLichSuKCBBS objLichSuKcb = _lichSuKcbClient.KiemTraTheBh_366(objApiTheBhyt);
            return objLichSuKcb;
        }
        public KQNhanLichSuKCBBS CheckCard595(ApiTheBHYT objApiTheBhyt)
        {
            KQNhanLichSuKCBBS objLichSuKcb = _lichSuKcbClient.KiemTraTheBh_366(objApiTheBhyt);
            return objLichSuKcb;
        }
    }
}
