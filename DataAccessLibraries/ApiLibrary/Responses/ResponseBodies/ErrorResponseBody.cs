using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Responses.ResponseBodies
{
    /// <summary>
    /// Error object holding information about an error response
    /// </summary>
    public class ErrorResponseBody
    {
        /// <summary>
        /// The error code
        /// </summary>
        public short Code { get; set; }

        /// <summary>
        /// The actual error message to be displayed
        /// </summary>
        public string Message { get; set; }
    }
}
