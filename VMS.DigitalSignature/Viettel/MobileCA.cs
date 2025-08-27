using DEMO_CLOUD_CA_DOTNET.BO;
using log4net.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;


namespace DEMO_CLOUD_CA_DOTNET
{
    public class MobileCA
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const String OID_NIST_SHA1 = "1.3.14.3.2.26";
        private const String OID_NIST_SHA256 = "2.16.840.1.101.3.4.2.1";
        private const String OID_RSA_RSA = "1.2.840.113549.1.1.1";
        public static String firstTimeSAD = null;
        private static DateTime expiredDateValidation = DateTime.Now;
        public MobileCA()
        {

        }

        public static Dictionary<String, CertBO> getAllCertificates(String userId, String BASE_URL, String CLIENT_ID, String CLIENT_SECRET, String PROFILE_ID, ref string accessToken)
        {
            try
            {
                Dictionary<String, CertBO> certList = new Dictionary<String, CertBO>();

                //Step 1: login to Cloud CA
                LoginResponseBO responce = login(userId, BASE_URL, CLIENT_ID, CLIENT_SECRET, PROFILE_ID);
                if (responce == null || ((responce.access_token == null || responce.expires_in <= 0)
                        && (responce.error != null || responce.error_description != null)))
                {
                    logger.Error("ERROR: Login to Cloud CA");
                    return null;
                }
                accessToken = responce.access_token;

                CredentialsListResponseBO credentialsListResponceBO = getCredentialsList(userId, accessToken, BASE_URL);
                if (credentialsListResponceBO == null)
                {
                    logger.Error("ERROR: Get Credentials list");
                    return null;
                }
                else if (credentialsListResponceBO.error != null || credentialsListResponceBO.error_description != null)
                {
                    logger.Error("ERROR: Get Credentials list: " + "\n"
                            + "Error: " + credentialsListResponceBO.error + "\n"
                            + "Error: " + credentialsListResponceBO.error_description
                    );
                    return null;
                }
                if (credentialsListResponceBO.credentialIDs == null || credentialsListResponceBO.credentialIDs.Length == 0)
                {
                    logger.Error("ERROR: Not found Certificate of User");
                    return null;
                }

                foreach (String credentialID in credentialsListResponceBO.credentialIDs)
                {
                    //                credentialID = credentialsListResponceBO.credentialIDs.get(0);
                    //Step 3: Get Credential info
                    CredentialsInfoResponseBO credentialsInfoResponceBO = getCredentialsInfo(credentialID, accessToken, BASE_URL);

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
            return null;
        }


        public static String[] signHash(String[] hashList, int id, String dataDisplay, String credentialID, String BASE_URL, string accessToken)
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

                CredentialsAuthorizeResponseBO credentialsAuthorizeResponceBO = getSAD(credentialID, accessToken, numSignatures, documents, hashList, BASE_URL);
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
                firstTimeSAD = SAD;

                //Step 5: Sign hash
                String hashAlgo = OID_NIST_SHA1;
                String hash = hashList[0];
                if (hash != null && hash.Length != 28)
                {
                    hashAlgo = OID_NIST_SHA256;
                }

                String signAlgo = OID_RSA_RSA;
                SignHashResponseBO signHashResponceBO = signHash(credentialID, accessToken, SAD, documents, hashList, hashAlgo, signAlgo, BASE_URL);
                
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

        public static String[] signHashWithoutValidation(String[] hashList, int id, String dataDisplay, String credentialID, int duration, String BASE_URL, string accessToken)
        {
            try
            {
                //Step 4: Register for new SAD without validation
                if (firstTimeSAD == null)
                {
                    logger.Error("ERROR: User chưa thực hiện ký có xác thực. Cần phải ký yêu cầu có xác thực trước khi thực hiện ký không xác thực.");
                    return null;
                }
                RegisterExtensionResponseBO registerExtensionResponse = new RegisterExtensionResponseBO();

                readExpireDateOnFile();

                //Compare Now() with expiredDateValidation: registerExtension would be called if the registration expired
                if (DateTime.Now >= expiredDateValidation)
                {
                    logger.Info("Tài khoản đã hết hạn đăng ký không xác thực");

                    registerExtensionResponse = registerExtension(credentialID, accessToken, firstTimeSAD, duration, BASE_URL);

                    if (registerExtensionResponse == null)
                    {
                        logger.Error("ERROR: Get SAD");
                        return null;
                    }
                    else if (registerExtensionResponse.error != null && !"60110".Equals(registerExtensionResponse.error))
                    {
                        logger.Error("ERROR: Get SAD: " + "\n"
                                + "Error: " + registerExtensionResponse.error + "\n"
                                + "Error: " + registerExtensionResponse.error_description
                        );
                        return null;
                    }
                }
                else
                {
                    logger.Info("Tài khoản đã được đăng ký để không xác thực từ trước");
                }

                if (registerExtensionResponse.expire_date != null && !"".Equals(registerExtensionResponse))
                {
                    expiredDateValidation = DateTime.Parse(registerExtensionResponse.expire_date);
                    writeExpireDateToFile(expiredDateValidation.ToString());
                }

                int numSignatures = hashList.Length;
                DocumentBO[] documents = new DocumentBO[hashList.Length];
                for (int i = 0; i < hashList.Length; i++)
                {
                    documents[i] = new DocumentBO(id, dataDisplay);
                }

                String hashAlgo = OID_NIST_SHA256;
                String hash = hashList[0];
                String signAlgo = OID_RSA_RSA;

                ExtendTransactionResponseBO newSADResponse = extendTransaction(credentialID, accessToken, firstTimeSAD, documents, hashList, hashAlgo, signAlgo, BASE_URL);
                if (newSADResponse == null)
                {
                    logger.Error("ERROR: Get SAD");
                    firstTimeSAD = null;
                    return null;
                }
                else if (newSADResponse.error != null || newSADResponse.error_description != null)
                {
                    logger.Error("ERROR: Get SAD: " + "\n"
                            + "Error: " + newSADResponse.error + "\n"
                            + "Error: " + newSADResponse.error_description
                    );
                    firstTimeSAD = null;
                    return null;
                }

                if (newSADResponse.SAD == null)
                {
                    logger.Error("ERROR: Get SAD");
                    firstTimeSAD = null;
                    return null;
                }

                String SAD = newSADResponse.SAD;
                firstTimeSAD = SAD;

                //Step 5: Sign hash

                SignHashResponseBO signHashResponceBO = signHash(credentialID, accessToken, SAD, documents, hashList, hashAlgo, signAlgo, BASE_URL);

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

        public static LoginResponseBO login(String userId, String BASE_URL, String CLIENT_ID, String CLIENT_SECRET, String PROFILE_ID)
        {
            try
            {
                string url = BASE_URL + "/vtss/service/ras/v1/login";
                LoginRequestBO request = new LoginRequestBO();
                request.client_id = CLIENT_ID;
                request.user_id = userId;
                request.client_secret = CLIENT_SECRET;
                request.profile_id = PROFILE_ID;
                var response = APICall.PostAsync<LoginResponseBO>(url, request).GetAwaiter().GetResult();
                return response;
            }
            catch (Exception e)
            {
                logger.Error("Error: " + e.Message);
                return null;
            }
        }
        public static CredentialsListResponseBO getCredentialsList(String userId, String accessToken, String BASE_URL)
        {
            try
            {
                string url = BASE_URL + "/vtss/service/ras/csc/v1/credentials/list";
                CredentialsListRequestBO request = new CredentialsListRequestBO();
                request.userID = userId;
                var response = APICall.PostAsync<CredentialsListResponseBO>(url, request, accessToken).GetAwaiter().GetResult();
                return response;
            }
            catch (Exception e)
            {
                logger.Error("Error: " + e.Message);
                return null;
            }
        }
        public static CredentialsInfoResponseBO getCredentialsInfo(String credentialID, String accessToken, String BASE_URL)
        {
            try
            {
                string url = BASE_URL + "/vtss/service/ras/csc/v1/credentials/info";
                CredentialsInfoRequestBO request = new CredentialsInfoRequestBO();
                request.credentialID = credentialID;
                request.certificates = "chain";
                request.certInfo = true;
                request.authInfo = true;
                var response = APICall.PostAsync<CredentialsInfoResponseBO>(url, request, accessToken).GetAwaiter().GetResult();
                return response;
            }
            catch (Exception e)
            {
                logger.Error("Error: " + e.Message);
                return null;
            }
        }
        public static CredentialsAuthorizeResponseBO getSAD(String credentialID, string accessToken,
            int numSignatures, DocumentBO[] documents, String[] hashs, String BASE_URL)
        {
            try
            {
                string url = BASE_URL + "/vtss/service/ras/csc/v1/credentials/authorize";
                CredentialsAuthorizeRequestBO request = new CredentialsAuthorizeRequestBO();
                request.credentialID = credentialID;
                request.numSignatures = numSignatures+10;
                request.documents = documents;
                request.hash = hashs;
                request.description = "test";

                var response = APICall.PostAsync<CredentialsAuthorizeResponseBO>(url, request, accessToken).GetAwaiter().GetResult();
                return response;
            }
            catch (Exception e)
            {
                logger.Error("Error: " + e.Message);
                return null;
            }
        }
        public static SignHashResponseBO signHash(String credentialID, String accessToken,
            String SAD, DocumentBO[] documents, String[] hashs, String hashAlgo, String signAlgo, String BASE_URL)
        {
            try
            {
                string url = BASE_URL + "/vtss/service/ras/csc/v1/signatures/signHash";
                SignHashRequestBO request = new SignHashRequestBO();
                request.credentialID = credentialID;
                request.SAD = SAD;
                request.documents = documents;
                request.hash = hashs;
                request.hashAlgo = hashAlgo;
                request.signAlgo = signAlgo;

                var response = APICall.PostAsync<SignHashResponseBO>(url, request, accessToken).GetAwaiter().GetResult();
                return response;
            }
            catch (Exception e)
            {
                logger.Error("Error: " + e.Message);
                return null;
            }
        }

        public static RegisterExtensionResponseBO registerExtension(String credentialID, String accessToken, String SAD,
            int duration, String BASE_URL)
        {
            try
            {
                string url = BASE_URL + "/vtss/service/ras/csc/v1/credentials/registerExtension";
                RegisterExtensionRequestBO request = new RegisterExtensionRequestBO();
                request.credential_id = credentialID;
                request.SAD = SAD;
                request.duration = duration;

                var response = APICall.PostAsync<RegisterExtensionResponseBO>(url, request, accessToken).GetAwaiter().GetResult();
                return response;
            }
            catch (System.Net.WebException e)
            {
                var resp = e.Response;
                var stream = resp.GetResponseStream();
                var reader = new System.IO.StreamReader(stream);
                JObject obj = JObject.Parse(reader.ReadToEnd());
                RegisterExtensionResponseBO errResp = new RegisterExtensionResponseBO();
                errResp.error = obj.GetValue("error").ToString();
                errResp.error_description = obj.GetValue("error_description").ToString();
                logger.Error("Error: " + resp);
                return errResp;
            }
        }

        public static ExtendTransactionResponseBO extendTransaction(String credentialID, String accessToken,
            String SAD, DocumentBO[] documents, String[] hashs, String hashAlgo, String signAlgo, String BASE_URL)
        {
            try
            {
                string url = BASE_URL + "/vtss/service/ras/csc/v1/credentials/extendTransaction";
                ExtendTransactionRequestBO request = new ExtendTransactionRequestBO();
                request.credentialID = credentialID;
                request.numSignatures = documents.Length;
                request.SAD = SAD;
                request.documents = documents;
                request.hash = hashs;
                request.hashAlgo = hashAlgo;
                request.signAlgo = signAlgo;

                var response = APICall.PostAsync<ExtendTransactionResponseBO>(url, request, accessToken).GetAwaiter().GetResult();
                return response;
            }
            catch (System.Net.WebException e)
            {
                var resp = e.Response;
                var stream = resp.GetResponseStream();
                var reader = new System.IO.StreamReader(stream);
                JObject obj = JObject.Parse(reader.ReadToEnd());
                ExtendTransactionResponseBO errResp = new ExtendTransactionResponseBO();
                errResp.error = obj.GetValue("error").ToString();
                errResp.error_description = obj.GetValue("error_description").ToString();
                logger.Error("Error: " + resp);
                return errResp;
            }
        }

        public static void readExpireDateOnFile()
        {
            try
            {
                // Open the text file using a stream reader.
                using (var sr = new StreamReader(@"expireDate.txt"))
                {
                    // Read the stream as a string, and write the string to the console.
                    String date = sr.ReadToEnd();
                    if (date != null && !"".Equals(date))
                    {
                        expiredDateValidation = ParseDateAuto(date).Value;// DateTime.Parse(date);
                    }
                    sr.Close();
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
        public static DateTime? ParseDateAuto(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            string[] formats = {
            "MM/dd/yyyy h:mm:ss tt",
            "dd/MM/yyyy h:mm:ss tt",
            "MM/dd/yyyy HH:mm:ss",
            "dd/MM/yyyy HH:mm:ss",
            "MM/dd/yyyy",
            "dd/MM/yyyy"
        };

            DateTime dt;
            if (DateTime.TryParseExact(
                    input.Trim(),
                    formats,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out dt))
            {
                return dt;
            }

            // fallback: thử Parse thường (để bắt thêm nhiều kiểu khác)
            if (DateTime.TryParse(input, out dt))
                return dt;

            return null; // không parse được
        }
        public static void writeExpireDateToFile(String date)
        {
            try
            {
                string tempFile = Path.GetTempFileName();
                using (var sw = new StreamWriter(tempFile))
                {
                    sw.WriteLine(date);
                    sw.Close();
                }
                File.Delete(@"expireDate.txt");
                File.Move(tempFile, @"expireDate.txt");
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
