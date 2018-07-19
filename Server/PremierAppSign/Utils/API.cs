using Ignition.Common.Logger;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace PremierAppSign.Utils
{
    public static class API
    {
        public static T Get<T>(string url, ILogger logger, string name, object content = null, string apiKey = null, string authToken = null)
        {
            return Call<T>(url, logger, name, HttpMethod.Get, content, apiKey, authToken);
        }

        public static T Post<T>(string url, ILogger logger, string name, object content = null, string apiKey = null, string authToken = null)
        {
            return Call<T>(url, logger, name, HttpMethod.Post, content, apiKey, authToken);
        }

        private static T Call<T>(string url, ILogger logger, string name, HttpMethod httpMethod, object content = null, string apiKey = null, string authToken = null)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    logger.Log(LogLevel.DIAGNOSTIC, MetricType.INFO, $"Start {httpMethod.ToString()} {url}", name);
                    client.DefaultRequestHeaders.Accept.Clear();
                    HttpRequestMessage configRequest = new HttpRequestMessage(httpMethod, url);
                    if (!string.IsNullOrWhiteSpace(authToken))
                    {
                        configRequest.Headers.Add("Authorization", $"Bearer {authToken}");
                    }
                    if (!string.IsNullOrWhiteSpace(apiKey))
                    {
                        configRequest.Headers.Add("x-api-key", apiKey);
                    }
                    if (content != null)
                    {
                        configRequest.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
                    }
                    var response = client.SendAsync(configRequest).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        logger.Log(LogLevel.DIAGNOSTIC, MetricType.INFO, $"Succeed {httpMethod.ToString()} {url}, {(int)response.StatusCode} {response.StatusCode}", name);
                        if (response.StatusCode == HttpStatusCode.NoContent)
                        {
                            return default(T);
                        }
                        return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        throw new Exception($"{response.Content.ReadAsStringAsync().Result}");
                    }
                }
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.DIAGNOSTIC, MetricType.DEBUG, $"Error {httpMethod.ToString()} {url}, {e.Message}", name);
                throw e;
            }
        }
    }
}
