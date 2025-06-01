using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using NLog;
using NLog.Config;
using NLog.Targets;
namespace VMS.QMS.SpeakerWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    	public class TTS : ITTS
	{
		private string _path = string.Empty;

		private string _language = string.Empty;

		private bool _delete;

		private Logger _log;

        public TTS()
		{
			InitLogs();
			_path = ConfigurationManager.AppSettings["path"];
			_language = ((ConfigurationManager.AppSettings["language"] == "") ? "vi" : ConfigurationManager.AppSettings["language"]);
			_delete = ((ConfigurationManager.AppSettings["DeleteFile"] == "TRUE") ? true : false);
		}

		private void InitLogs()
		{
			try
			{
                var config = new LoggingConfiguration();
                var fileTarget = new FileTarget();
                config.AddTarget("file", fileTarget);
                fileTarget.FileName =
                    "${basedir}/Mylogs/${date:format=yyyy}/${date:format=MM}/${date:format=dd}/${logger}.log";
                fileTarget.Layout = "${date:format=HH\\:mm\\:ss}|${threadid}|${level}|${logger}|${message}";
                config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, fileTarget));
                LogManager.Configuration = config;
                _log = LogManager.GetCurrentClassLogger();

                //LoggingConfiguration loggingConfiguration = new LoggingConfiguration();
                //FileTarget fileTarget = new FileTarget();
                //fileTarget.FileName = "${basedir}/MyLog/${date:format=yyyy}/${date:format=MM}/${date:format=dd}/${logger}.log";
                //fileTarget.Layout = "${date:format=dd/MM/yyyy HH\\:mm\\:ss\\.fff}|${threadid}|${level}|${logger}|${message}";
                //fileTarget.ArchiveAboveSize = 5242880L;
                //fileTarget.Encoding = (Encoding)(object)Encoding.UTF8;
                //FileTarget target = fileTarget;
                //loggingConfiguration.AddTarget("file", target);
                //((ICollection<LoggingRule>)loggingConfiguration.LoggingRules).Add(new LoggingRule("*", LogLevel.Trace, target));
                //LogManager.Configuration = loggingConfiguration;
                //_log = LogManager.GetLogger("TTS");
                _log.Trace("Init successfully");
               
			}
			catch (Exception value)
			{
				_log.Trace(value);
			}
		}

		public byte[] WebRequestToBytes(string pzUrl, string pzData, string pzAuthorization, string pzMethod, string pzContentType)
		{
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(pzUrl);
				httpWebRequest.ContentType = pzContentType;
				httpWebRequest.Method = pzMethod;
				httpWebRequest.UserAgent = "VBIT";
				httpWebRequest.MediaType = "application/json";
				httpWebRequest.Headers.Add("Authorization", pzAuthorization);
				httpWebRequest.Proxy = new WebProxy();
				if (!string.IsNullOrEmpty(pzData))
				{
					using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
					{
						streamWriter.Write(pzData);
						streamWriter.Flush();
						streamWriter.Close();
					}
				}
				InitiateSslTrust();
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				using (StreamReader reader = new StreamReader(httpWebResponse.GetResponseStream()))
				{
					return StrBytes(reader);
				}
			}
			catch (Exception ex)
			{
				_log.Trace(ex.Message);
				return null;
			}
		}

		public static void InitiateSslTrust()
		{
			try
			{
				ServicePointManager.ServerCertificateValidationCallback = (object param0, X509Certificate param1, X509Chain param2, SslPolicyErrors param3) => true;
			}
			catch (Exception)
			{
			}
		}

		private byte[] StrBytes(StreamReader reader)
		{
			byte[] array = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				byte[] array2 = new byte[512];
				int num = 0;
				while ((num = reader.BaseStream.Read(array2, 0, array2.Length)) > 0)
				{
					memoryStream.Write(array2, 0, num);
				}
				return memoryStream.ToArray();
			}
		}

		public byte[] SaveAudio(string noidung, ref string filename, ref string messge)
		{
			try
			{
				_log.Trace("1." + messge);
                string text = string.Format("https://translate.googleapis.com/translate_tts?ie=UTF-8&q={0}&tl={1}&total=1&idx=0&textlen={2}&client=gtx", HttpUtility.UrlEncode(noidung), "vi", noidung.Length);
				_log.Trace("url." + text);
				filename = Guid.NewGuid().ToString();
				if (filename == null)
				{
					messge = "Không sinh Gui ID";
				}
				if (string.IsNullOrEmpty(_path))
				{
					messge = "Chưa cài đặt đường dẫn";
				}
				return WebRequestToBytes(text, "", "", "GET", "application/json");
			}
			catch (Exception ex)
			{
				_log.Trace(ex.Message);
				messge = ex.Message;
				return null;
			}
		}

		public void DeleteFile()
		{
			try
			{
				if (!_delete)
				{
					return;
				}
				string[] files = Directory.GetFiles(_path, "*.mp3");
				string[] array = files;
				foreach (string path in array)
				{
					if (File.GetCreationTime(path).Date < DateTime.Now.Date)
					{
						File.Delete(path);
					}
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
