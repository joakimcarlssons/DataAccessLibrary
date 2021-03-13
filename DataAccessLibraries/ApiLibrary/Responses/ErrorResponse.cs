using ApiLibrary.Responses.ResponseBodies;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Responses
{
    /// <summary>
    /// The response to use when an error occurs
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// The response body
        /// </summary>
        public ErrorResponseBody Response { get; private set; }

        /// <summary>
        /// Default constructor.
        /// Initializes a new instance of the <see cref="ErrorResponseBody"/> class
        /// </summary>
        /// <param name="code">The error code</param>
        /// <param name="message">The message</param>
        public ErrorResponse(short code, string message)
        {
            // Create the body
            Response = new ErrorResponseBody
            {
                Code    = code,
                Message = message
            };
        }
    }
}
