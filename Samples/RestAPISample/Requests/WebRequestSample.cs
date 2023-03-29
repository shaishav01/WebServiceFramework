using System;
using UnityEngine;

namespace com.juego.webservice.sample
{
    /// <summary>
    /// A sample request to get the data from server
    /// </summary>
    public class WebRequestSample : WebRequest
    {
        internal void Send(MonoBehaviour behaviour, Action<SampleResponse> OnSuccess, Action<WebResponse> OnFailed)
        {
            //Set the endpoint
            EndPoint("auto-complete");
            //Set body parameters
            SetData("q", "game of thr");
            //Set headers
            SetHeader("X-RapidAPI-Key", "0a421f45d8mshfae0f9501e8f888p1a3445jsne61999eb0936");
            SetHeader("X-RapidAPI-Host", "imdb8.p.rapidapi.com");
            //Send a request
            behaviour.StartCoroutine(Post<SampleResponse>(OnSuccess, OnFailed));
        }
    }
}