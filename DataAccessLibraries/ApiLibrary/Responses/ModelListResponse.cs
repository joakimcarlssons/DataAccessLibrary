using ApiLibrary.Responses.ResponseBodies;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Responses
{
    public class ModelListResponse<T>
        where T : class
    {
        /// <summary>
        /// The response body
        /// </summary>
        public ModelListResponseBody<T> Response { get; private set; }

        /// <summary>
        /// Default constructor.
        /// Initializes a new instance of the <see cref="ModelListResponseBody{T}"/> class
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="models">The list of models</param>
        public ModelListResponse(string message, IEnumerable<T> models)
        {
            // Create response
            Response = new ModelListResponseBody<T>
            {
                Message     = message,
                Data        = models
            };
        }

    }
}
