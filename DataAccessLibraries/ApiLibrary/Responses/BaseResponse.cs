using ApiLibrary.Responses.ResponseBodies;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Responses
{
    /// <summary>
    /// The response to return when a model should be returned
    /// </summary>
    /// <typeparam name="T">The type of the model to return</typeparam>
    public class BaseResponse<T>
        where T : class
    {
        /// <summary>
        /// The response body
        /// </summary>
        public ModelResponseBody<T> Response { get; private set; }

        /// <summary>
        /// Default constructor.
        /// Initializes a new instance of the <see cref="BaseResponse{T}{T}"/> class
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="data">The model</param>
        public BaseResponse(string message, T model)
        {
            // Create response
            Response = new ModelResponseBody<T>
            {
                Message = message,
                Data = model
            };
        }
    }
}
