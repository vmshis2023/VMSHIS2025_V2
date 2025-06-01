using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VMS.Invoice;

namespace VMS.HIS.API
{
	public class CreateRequest
	{

		public static string WebRequest(string pzUrl, string pzData, string pzAuthorization, string pzMethod, string pzContentType)
		{
			string result = string.Empty;
			try
			{
				
				Utility.Log.Trace(pzUrl);
				HttpWebRequest httpWebRequest = (HttpWebRequest)System.Net.WebRequest.Create(pzUrl);
				httpWebRequest.ContentType = pzContentType;
				httpWebRequest.Method = pzMethod;
				if (!string.IsNullOrEmpty(pzAuthorization))
				{
					httpWebRequest.Headers.Add("Authorization", pzAuthorization);
				}
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
				HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					result = streamReader.ReadToEnd();
				}
				Utility.Log.Trace(result);
				return result;
			}
			catch (Exception ex)
			{
				Utility.Log.Trace("WebRequest " + ex.Message);
				return ex.Message.ToString();
			}
		}

		public static string WebRequest(string pzUrl, string pzData, string pzAuthorization, Dictionary<string, string> headers, string pzMethod, string pzContentType)
		{
			try
			{
				Utility.Log.Trace(pzUrl);
				HttpWebRequest httpWebRequest = (HttpWebRequest)System.Net.WebRequest.Create(pzUrl);
				httpWebRequest.ContentType = pzContentType;
				httpWebRequest.Method = pzMethod;
				if (!string.IsNullOrEmpty(pzAuthorization))
				{
					httpWebRequest.Headers.Add("Authorization", pzAuthorization);
				}
				if (headers.Count > 0)
				{
					foreach (KeyValuePair<string, string> header in headers)
					{
						httpWebRequest.Headers.Add(header.Key, header.Value);
					}
				}
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
				HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				string result = string.Empty;
				using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					result = streamReader.ReadToEnd();
				}
				Utility.Log.Trace(result);
				return result;
			}
			catch (Exception ex)
			{
				Utility.Log.Trace("WebRequest " + ex.Message);
				return null;
			}
		}

		public static byte[] WebRequestToBytes(string pzUrl, string pzData, string pzAuthorization, string pzMethod, string pzContentType)
		{
			try
			{
				HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(pzUrl);
				request.ContentType = pzContentType;
				request.Method = pzMethod;
				request.UserAgent = "VBIT";
				request.MediaType = "application/json";
				request.Headers.Add("Authorization", pzAuthorization);
				request.Proxy = new WebProxy();
				if (!string.IsNullOrEmpty(pzData))
				{
					using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
					{
						writer.Write(pzData);
						writer.Flush();
						writer.Close();
					}
				}
				InitiateSslTrust();
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				using (StreamReader reader = new StreamReader(response.GetResponseStream()))
				{
					return StrBytes(reader);
				}
			}
			catch (Exception exception)
			{
				Utility.Log.Trace(exception.Message);
				return null;
			}
		}

		private static byte[] StrBytes(StreamReader reader)
		{
			byte[] bytes = null;
			using (MemoryStream memstream = new MemoryStream())
			{
				byte[] buffer = new byte[512];
				int bytesRead = 0;
				while ((bytesRead = reader.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
				{
					memstream.Write(buffer, 0, bytesRead);
				}
				return memstream.ToArray();
			}
		}

		public static void InitiateSslTrust()
		{
			try
			{
				ServicePointManager.ServerCertificateValidationCallback  +=(sender, cert, chain, sslPolicyErrors) => true;
			}
			catch (Exception)
			{
			}
		}
	}
}
