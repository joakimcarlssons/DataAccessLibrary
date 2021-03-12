using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DataAccess
{
    /// <summary>
    /// Make Http requests using these generic functions
    /// </summary>
    public class HttpRequests : IHttpRequests
    {
        #region Private Members

        private readonly IHttpClientFactory _clientFactory;
        private HttpClient _client;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public HttpRequests(IHttpClientFactory clientFactory)
        {
            // Injections
            _clientFactory = clientFactory;

            // Initialize client
            InitializeClient();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method used to initialize a client
        /// </summary>
        /// <remarks>Client is being initialized when a new request is being created</remarks>
        private void InitializeClient() => _client = _clientFactory.CreateClient();

        #endregion

        #region Public Methods

        /// <summary>
        /// Make a a http request and expect a JSON result
        /// </summary>
        /// <typeparam name="T">The model to map the JSON result to</typeparam>
        /// <param name="url">The url to make the http request to</param>
        /// <returns>A model of the desired type</returns>
        public async Task<T> GetJsonAsync<T>(string url)
            => (await _client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url))).Content.ReadFromJsonAsync<T>().Result;

        #endregion

    }
}
