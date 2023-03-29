using UnityEngine;

namespace com.juego.webservice.sample
{
    public class WebRequestManager : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            WebServiceConfig.Init("https://imdb8.p.rapidapi.com/");

            new WebRequestSample().Send(this, (res) =>
            {
                Debug.Log("Success->" + res.message);

            }, (error) =>
            {
                Debug.Log("Error->" + error.message);
            });
        }
    }
}