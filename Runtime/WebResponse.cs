using Newtonsoft.Json;

namespace com.juego.webservice
{
    /// <summary>
    /// Contains the details of response received from webrequest.
    /// </summary>
    public class WebResponse
    {
        public int responseCode;
        public bool isError;
        public string message;
    }

    public class ResponseReceived<T> where T : WebResponse
    {
        [JsonProperty("responseCode")] public int responseCode;
        [JsonProperty("responseMessage")] public string responseMessage;
        [JsonProperty("responseData")] public T responseData;
    }
}