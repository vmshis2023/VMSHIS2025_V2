using VietBaIT.ChuKySo.Api.DigitalSignature.Viettel.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using VietBaIT.ChuKySo.Api.Helpers;
using System.Net;

namespace VietBaIT.ChuKySo.Api.DigitalSignature.Viettel
{
    public class MobileCA
    {
        private static readonly Logger logger = Utility.LogFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const String OID_NIST_SHA1 = "1.3.14.3.2.26";
        private const String OID_NIST_SHA256 = "2.16.840.1.101.3.4.2.1";
        private const String OID_RSA_RSA = "1.2.840.113549.1.1.1";
        public MobileCA()
        {

        }

        public static Dictionary<String, CertBO> getAllCertificates(String userId, String BASE_URL, String CLIENT_ID, String CLIENT_SECRET, 
            String PROFILE_ID, ref string accessToken, ref int accessTokenExpiresIn)
        {
            Dictionary<String, CertBO> certList = new Dictionary<String, CertBO>();
            try
            {
                //Step 1: login to Cloud CA
                LoginResponceBO responce = login(userId, BASE_URL, CLIENT_ID, CLIENT_SECRET, PROFILE_ID);
                if (responce == null || ((responce.access_token == null || responce.expires_in <= 0)
                        && (responce.error != null || responce.error_description != null)))
                {
                    logger.Error("ERROR: Login to Cloud CA");
                    return certList;
                }
                accessToken = responce.access_token;
                accessTokenExpiresIn = responce.expires_in;

                CredentialsListResponceBO credentialsListResponceBO = getCredentialsList(userId, accessToken, BASE_URL);
                if (credentialsListResponceBO == null)
                {
                    logger.Error("ERROR: Get Credentials list");
                    return certList;
                }
                else if (credentialsListResponceBO.error != null || credentialsListResponceBO.error_description != null)
                {
                    logger.Error("ERROR: Get Credentials list: " + "\n"
                            + "Error: " + credentialsListResponceBO.error + "\n"
                            + "Error: " + credentialsListResponceBO.error_description
                    );
                    return certList;
                }
                if (credentialsListResponceBO.credentialIDs == null || credentialsListResponceBO.credentialIDs.Length == 0)
                {
                    logger.Error("ERROR: Not found Certificate of User");
                    return certList;
                }

                foreach (String credentialID in credentialsListResponceBO.credentialIDs)
                {
                    //                credentialID = credentialsListResponceBO.credentialIDs.get(0);
                    //Step 3: Get Credential info
                    CredentialsInfoResponceBO credentialsInfoResponceBO = getCredentialsInfo(credentialID, accessToken, BASE_URL);

                    if (credentialsInfoResponceBO == null)
                    {
                        logger.Error("ERROR: Get Credentials info");
                        continue;
                    }
                    else if (credentialsInfoResponceBO.error != null || credentialsInfoResponceBO.error_description != null)
                    {
                        logger.Error("ERROR: Get Credentials info: " + "\n"
                                + "Error: " + credentialsInfoResponceBO.error + "\n"
                                + "Error: " + credentialsInfoResponceBO.error_description
                        );
                        continue;
                    }
                    //if (credentialsInfoResponceBO.cert == null || !"valid".Equals(credentialsInfoResponceBO.cert.status)
                    //        || credentialsInfoResponceBO.cert.certificates == null || credentialsInfoResponceBO.cert.certificates.Length == 0)
                    //{
                    //    logger.Info("Status of Certificate of " + credentialID + " is INVALID: " + credentialsInfoResponceBO.cert.status);
                    //    continue;
                    //}
                    //                List<String> certChain = credentialsInfoResponceBO.getCert().getCertificates();
                    //String subjectDN = credentialsInfoResponceBO.getCert().getSubjectDN();
                    certList.Add(credentialID, credentialsInfoResponceBO.cert);
                }
                return certList;
            }
            catch (Exception e)
            {
                logger.Error(e);
            }
            return certList;
        }


        public static String[] signHash(String[] hashList, int id, String dataDisplay, String credentialID, String BASE_URL, string accessToken, AppSettings appSettings)
        {
            try
            {
                //Step 4: Get SAD
                int numSignatures = hashList.Length;
                DocumentBO[] documents = new DocumentBO[hashList.Length];
                for (int i = 0; i < hashList.Length; i++)
                {
                    documents[i] = new DocumentBO(id, dataDisplay);
                }

                CredentialsAuthorizeResponceBO credentialsAuthorizeResponceBO = getSAD(credentialID, dataDisplay, accessToken, numSignatures, documents, hashList, BASE_URL);
                if (credentialsAuthorizeResponceBO == null)
                {
                    logger.Error("ERROR: Get SAD");
                    return null;
                } else if (credentialsAuthorizeResponceBO.error != null || credentialsAuthorizeResponceBO.error_description != null)
                {
                    logger.Error("ERROR: Get SAD: " + "\n"
                            + "Error: " + credentialsAuthorizeResponceBO.error + "\n"
                            + "Error: " + credentialsAuthorizeResponceBO.error_description
                    );
                    return null;
                }
                if (credentialsAuthorizeResponceBO.SAD == null)
                {
                    logger.Error("ERROR: Get SAD");
                    return null;
                }

                String SAD = credentialsAuthorizeResponceBO.SAD;

                //Step 5: register extension to allow signing without confirmation
                RegisterExtensionResponseBO registerExtensionResponseBO = registerExtension(credentialID, accessToken, SAD, BASE_URL);
                if(string.IsNullOrEmpty(registerExtensionResponseBO.expire_date) 
                    && !registerExtensionResponseBO.error_description.Contains("user ID is already registered for transaction extend"))
                {
                    logger.Error("ERROR: " + registerExtensionResponseBO.error_description);
                }

                //Step 6: Sign hash
                String hashAlgo = OID_NIST_SHA1;
                String hash = hashList[0];
                if (hash != null && hash.Length != 28)
                {
                    hashAlgo = OID_NIST_SHA256;
                }

                String signAlgo = OID_RSA_RSA;
                SignHashResponceBO signHashResponceBO = signHash(credentialID, accessToken, SAD, documents, hashList, hashAlgo, signAlgo, BASE_URL);
                
                if (signHashResponceBO == null)
                {
                    logger.Error("ERROR: Sign Hash");
                    return null;
                }
                else if (signHashResponceBO.error != null || signHashResponceBO.error_description != null)
                {
                    logger.Error("ERROR: Sign Hash: " + "\n"
                            + "Error: " + signHashResponceBO.error + "\n"
                            + "Error: " + signHashResponceBO.error_description
                    );
                    return null;
                }
                if (signHashResponceBO.signatures == null || signHashResponceBO.signatures.Length == 0)
                {
                    logger.Error("ERROR: Sign Hash");
                    return null;
                }

                var signatures = signHashResponceBO.signatures;
                return signatures;
            }
            catch (Exception e)
            {
                logger.Error(e);
            }
            return null;
        }

        public static LoginResponceBO login(String userId, String BASE_URL, String CLIENT_ID, String CLIENT_SECRET, String PROFILE_ID)
        {
            try
            {
                string url = BASE_URL + "/adss/service/ras/v1/login";
                LoginRequestBO request = new LoginRequestBO();
                request.client_id = CLIENT_ID;
                request.user_id = userId;
                request.client_secret = CLIENT_SECRET;
                request.profile_id = PROFILE_ID;
                var response = APICall.PostAsync<LoginResponceBO>(url, request).GetAwaiter().GetResult();
                return response;
            }
            catch (Exception e)
            {
                logger.Error("Error: " + e.Message);
                return null;
            }
        }
        public static RegisterExtensionResponseBO registerExtension(String userId, String accessToken, String SAD, String BASE_URL, int duration = 60)
        {
            try
            {
                string url = BASE_URL + "/vtss/service/ras/csc/v1/credentials/registerExtension";
                var request = new RegisterExtensionRequestBO();
                request.credential_id = userId;
                request.duration = duration;
                request.SAD = SAD;
                var response = APICall.PostAsync<RegisterExtensionResponseBO>(url, request, accessToken).GetAwaiter().GetResult();
                return response;
            }
            catch (Exception e)
            {
                logger.Error("Error: " + e.Message);
                return null;
            }
        }
        public static CredentialsListResponceBO getCredentialsList(String userId, String accessToken, String BASE_URL)
        {
            try
            {
                string url = BASE_URL + "/adss/service/ras/csc/v1/credentials/list";
                CredentialsListRequestBO request = new CredentialsListRequestBO();
                request.userID = userId;
                var response = APICall.PostAsync<CredentialsListResponceBO>(url, request, accessToken).GetAwaiter().GetResult();
                return response;
            }
            catch (Exception e)
            {
                logger.Error("Error: " + e.Message);
                return null;
            }
        }
        public static CredentialsInfoResponceBO getCredentialsInfo(String credentialID, String accessToken, String BASE_URL)
        {
            try
            {
                string url = BASE_URL + "/adss/service/ras/csc/v1/credentials/info";
                CredentialsInfoRequestBO request = new CredentialsInfoRequestBO();
                request.credentialID = credentialID;
                request.certificates = "chain";
                request.certInfo = true;
                request.authInfo = true;
                var response = APICall.PostAsync<CredentialsInfoResponceBO>(url, request, accessToken).GetAwaiter().GetResult();
                return response;
            }
            catch (Exception e)
            {
                logger.Error("Error: " + e.Message);
                return null;
            }
        }
        public static CredentialsAuthorizeResponceBO getSAD(String credentialID, String description, string accessToken,
            int numSignatures, DocumentBO[] documents, String[] hashs, String BASE_URL)
        {
            try
            {
                string url = BASE_URL + "/adss/service/ras/csc/v1/credentials/authorize";
                CredentialsAuthorizeRequestBO request = new CredentialsAuthorizeRequestBO();
                request.description = description;
                request.credentialID = credentialID;
                request.numSignatures = numSignatures;
                request.documents = documents;
                request.hash = hashs;

                var response = APICall.PostAsync<CredentialsAuthorizeResponceBO>(url, request, accessToken).GetAwaiter().GetResult();
                return response;
            }
            catch (Exception e)
            {
                logger.Error("Error: " + e.Message);
                return null;
            }
        }
        public static SignHashResponceBO signHash(String credentialID, String accessToken,
            String SAD, DocumentBO[] documents, String[] hashs, String hashAlgo, String signAlgo, String BASE_URL)
        {
            try
            {
                string url = BASE_URL + "/adss/service/ras/csc/v1/signatures/signHash";
                SignHashRequestBO request = new SignHashRequestBO();
                request.credentialID = credentialID;
                request.SAD = SAD;
                request.documents = documents;
                request.hash = hashs;
                request.hashAlgo = hashAlgo;
                request.signAlgo = signAlgo;

                var response = APICall.PostAsync<SignHashResponceBO>(url, request, accessToken).GetAwaiter().GetResult();
                return response;
            }
            catch (Exception e)
            {
                logger.Error("Error: " + e.Message);
                return null;
            }
        }
    }
}
