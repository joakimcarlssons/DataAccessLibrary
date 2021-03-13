using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Responses
{
    /// <summary>
    /// Creates a dynamic response of your choice
    /// </summary>
    public class DynamicResponse
    {
        /// <summary>
        /// The response body
        /// </summary>
        public dynamic ResponseBody { get; private set; }

        /// <summary>
        /// Default constructor. Setting the dynamic response.
        /// </summary>
        /// <param name="responseBody"></param>
        public DynamicResponse(dynamic data) => ResponseBody = data;
    }
}
