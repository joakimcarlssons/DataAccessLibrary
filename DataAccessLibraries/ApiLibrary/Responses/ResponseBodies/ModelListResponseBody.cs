using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Responses.ResponseBodies
{
    /// <summary>
    /// A generic response body returning a message and a list of desired models
    /// </summary>
    /// <typeparam name="T">The type of the model to return</typeparam>
    public class ModelListResponseBody<T>
        where T : class
    {
        /// <summary>
        /// The message to be returned
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The list of models to be returned
        /// </summary>
        public IEnumerable<T> ModelList { get; set; }
    }
}
