using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TDCommon
{
    public static class HttpRequestHelper
    {
        /// <summary>
        /// API发送POST请求
        /// </summary>
        /// <param name="url">请求的API地址</param>
        /// <param name="parametersJson">POST过去的参数（JSON格式）字符串</param>
        /// <returns></returns>
        public static string ApiPost(string url, string parametersJson)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            // 为JSON格式添加一个Accept报头
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //需要传递的参数（参数封装成JSON）
            HttpContent content = new StringContent(parametersJson)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync().Result;
        }
        /// <summary>
        /// API发送GET请求，返回Json
        /// </summary>
        /// <param name="url"></param>
        /// <returns>如果未成功返回空</returns>
        public static string ApiGet(string url)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            // 为JSON格式添加一个Accept报头
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            return "";
        }
        /// <summary>
        ///  API发送DELETE请求，返回状态：200成功，201失败
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ApiDelete(string url)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            // 为JSON格式添加一个Accept报头
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.DeleteAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            return "";
        }
    }
}
