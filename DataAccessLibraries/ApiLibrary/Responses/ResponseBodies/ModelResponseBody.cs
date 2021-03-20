using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Responses.ResponseBodies
{
    /// <summary>
    /// A generic response body returning a message and a desired model
    /// </summary>
    /// <typeparam name="T">The type of the model to create a response for</typeparam>
    public class ModelResponseBody<T>
    {
        /// <summary>
        /// The message to return
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The model the return
        /// </summary>
        public T Data { get; set; }
    }
}
