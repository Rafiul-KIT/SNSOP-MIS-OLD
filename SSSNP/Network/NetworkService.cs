using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace SSSNP.Network
{
    public class NetworkService
    {
        public static T SubmitRequest<T>(object requestModel, string url, string accessToken)
        {
            try
            {
                return SubmitRequest<T>(requestModel, url, "POST", accessToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T SubmitRequest<T>(object requestModel, string url, string requestType = "POST", string accessToken = "")
        {
            try
            {
                string apiBaseURL = "";
                if (ConfigurationManager.AppSettings["build.profile.active"].ToString() == "dev")
                {
                    apiBaseURL = System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrlDev"];
                }
                else
                {
                    apiBaseURL = System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrlProd"];
                }


                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(apiBaseURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(60);


                if (!string.IsNullOrWhiteSpace(accessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }


                T responseModel;
                HttpResponseMessage response = new HttpResponseMessage();

                if (requestType == "POST")
                {
                    response = client.PostAsJsonAsync(string.Format("{0}", url), requestModel).Result;
                }
                else if (requestType == "PUT")
                {
                    response = client.PutAsJsonAsync(string.Format("{0}", url), requestModel).Result;
                }
                else if (requestType == "GET")
                {
                    response = client.GetAsync(string.Format("{0}", url)).Result;
                }
                response.EnsureSuccessStatusCode();
                responseModel = response.Content.ReadAsAsync<T>().Result;
                return responseModel;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("401")) throw new WebException();
                if (ex.Message.Contains("403")) throw new WebException();
                if (ex.Message.Contains("404")) throw new WebException();

                var expMsg1 = ex.InnerException?.Message;
                var expMsg2 = ex.InnerException?.InnerException?.Message;

                if (!string.IsNullOrEmpty(expMsg1))
                {
                    if (expMsg1.Contains("Unable to connect to the remote server")) throw new WebException();
                }

                if (!string.IsNullOrEmpty(expMsg2))
                {
                    if (expMsg2.Contains("Unable to connect to the remote server")) throw new WebException();
                    if (expMsg2.Contains("Unable to read data from the transport connection: An existing connection was forcibly closed by the remote host")) throw new WebException();

                    //The remote name could not be resolved: 'rabcdms.rab.gov.bd'
                    if (expMsg2.Contains("The remote name could not be resolved")) throw new WebException();
                    if (expMsg2.Contains("rabcdms.rab.gov.bd")) throw new WebException();
                }

                throw ex;
            }
        }
    }
}