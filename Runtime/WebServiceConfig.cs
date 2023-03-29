namespace com.juego.webservice
{
    /// <summary>
	/// Set the baseic Configuration of WebServiceFramework
	/// </summary>
    public class WebServiceConfig
    {
        private static string _baseUrl;
        private static bool _isDebug;

        /// <summary>
        /// Initialise the framework
        /// </summary>
        /// <param name="baseUrl"></param>
        public static void Init(string baseUrl, bool isDebug = true)
        {
            _baseUrl = baseUrl;
            _isDebug = isDebug;
        }

        /// <summary>
        /// If enable, show logs.
        /// </summary>
        public static bool IsDebugEnable => _isDebug;

        /// <summary>
		/// Returns the base url  
		/// </summary>
        public static string BaseUrl { get { return _baseUrl; } }
    }
}
