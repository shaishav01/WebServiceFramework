using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace com.juego.webservice
{
    public class WebRequest
    {
        Dictionary<string, string> formData;
        Dictionary<string, string> headers;
        string _endPoint;

        public WebRequest()
        {
            headers = new Dictionary<string, string>();
            formData = new Dictionary<string, string>();
        }

        public void EndPoint(string endPoint)
        {
            _endPoint = endPoint;
        }

        public void SetData(string key, string value)
        {
            formData.Add(key, value);
        }

        public void SetHeader(string key, string value)
        {
            headers.Add(key, value);
        }

        /// <summary>
        /// Post request and get response
        /// </summary>
        /// <returns></returns>
        public IEnumerator Post<T>(Action<T> OnSuccess, Action<WebResponse> OnFailed) where T : WebResponse
        {
            WWWForm wWWForm = new WWWForm();
            foreach (string key in formData.Keys)
            {
                wWWForm.AddField(key, formData[key]);
            }
            using (UnityWebRequest request = UnityWebRequest.Post(WebServiceConfig.BaseUrl + _endPoint, wWWForm))
            {
                foreach (string key in headers.Keys)
                {
                    request.SetRequestHeader(key, headers[key]);
                }
                Debug.Log("<color=red>REST Req: " + _endPoint + "</color> : " + request.url);
                yield return request.SendWebRequest();
                Debug.Log("<color=green>REST Res: " + _endPoint + "</color> : " + request.downloadHandler.text);
                ResponseHandler<T>(request, OnSuccess, OnFailed);
            }
        }

        /// <summary>
        /// Send a webrequest of type GET to the server
        /// </summary>
        /// <returns></returns>
        public IEnumerator Get<T>(Action<T> OnSuccess, Action<WebResponse> OnFailed) where T : WebResponse
        {
            string url = WebServiceConfig.BaseUrl + _endPoint;
            if (formData.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("{0}?", url);

                foreach (string key in formData.Keys)
                {
                    builder.AppendFormat("{0}={1}&", key, formData[key]);
                }
                url = builder.ToString();
                url = url.Substring(0, url.Length - 1);
            }

            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                foreach (string key in headers.Keys)
                {
                    request.SetRequestHeader(key, headers[key]);
                }
                Debug.Log("<color=red>REST Req: " + _endPoint + "</color> : " + request.url);
                yield return request.SendWebRequest();
                Debug.Log("<color=green>REST Res: " + _endPoint + "</color> : " + request.downloadHandler.text);
                ResponseHandler<T>(request, OnSuccess, OnFailed);
            }
        }

        /// <summary>
        /// Send a webrequest of type GET to the server
        /// </summary>
        /// <returns></returns>
        public IEnumerator Get<T>(Action<T> OnSuccess, Action<WebResponse> OnFailed, string url) where T : WebResponse
        {
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                foreach (string key in headers.Keys)
                {
                    request.SetRequestHeader(key, headers[key]);
                }
                Debug.Log("<color=red>REST Req: " + _endPoint + "</color> : " + request.url);
                yield return request.SendWebRequest();
                Debug.Log("<color=green>REST Res: " + _endPoint + "</color> : " + request.downloadHandler.text);
                ResponseHandler<T>(request, OnSuccess, OnFailed);
            }
        }

        /// <summary>
        /// Common web response handler
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="OnSuccess"></param>
        /// <param name="OnFailed"></param>
        void ResponseHandler<T>(UnityWebRequest request, Action<T> OnSuccess, Action<WebResponse> OnFailed) where T : WebResponse
        {
            switch (request.responseCode)
            {
                case 200:
                    ResponseReceived<T> currentResponse = JsonConvert.DeserializeObject<ResponseReceived<T>>(request.downloadHandler.text);
                    OnSuccess?.Invoke(currentResponse.responseData);
                    break;
                default:
                    WebResponse webResponse = new WebResponse();
                    webResponse.isError = true;
                    webResponse.responseCode = (int)request.responseCode;
                    webResponse.message = request.downloadHandler.text;
                    OnFailed?.Invoke(webResponse);
                    break;
            }
        }
    }
}