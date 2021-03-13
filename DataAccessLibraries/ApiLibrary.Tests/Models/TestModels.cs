using System;
using System.Collections.Generic;
using System.Text;
using ApiLibrary.Responses;

namespace ApiLibrary.Tests.Models
{
    /// <summary>
    /// A simple user model used for testing the <see cref="ModelResponse{T}"/> class
    /// </summary>
    public class User
    {
        public string Name { get; set; }
    }
}
