using ApiLibrary.Responses;
using ApiLibrary.Responses.ResponseBodies;
using ApiLibrary.Tests.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Tests
{
    [TestFixture]
    public class ResponseTests
    {
        /// <summary>
        /// Testing the <see cref="DynamicResponse"/>
        /// </summary>
        [Test]
        public void DynamicResponse_WhenCreated_CreateBody()
        {
            // Create a new response
            var response = new DynamicResponse("Test");

            // Check the result
            Assert.That(response.ResponseBody, Is.EqualTo("Test"));
        }

        /// <summary>
        /// Testing the <see cref="ErrorResponse"/>
        /// </summary>
        [Test]
        public void ErrorResponse_WhenCreated_CreateBody()
        {
            // Create a new response
            var response = new ErrorResponse(404, "Not Found");

            // Check the result
            Assert.That(response.Response.Code, Is.EqualTo(404));
            Assert.That(response.Response.Message, Is.EqualTo("Not Found"));
        }

        /// <summary>
        /// Testing the <see cref="ModelResponse{T}"/>
        /// </summary>
        [Test]
        public void ModelResponse_WhenCreated_CreateBody()
        {
            // Create a test model
            var model = new User { Name = "Test" };

            // Create the new response
            var response = new ModelResponse<User>("User created", model);

            // Check the result
            Assert.That(response.Response.Message, Is.EqualTo("User created"));
            Assert.That(response.Response.Model, Is.EqualTo(model));
        }

        /// <summary>
        /// Testing the <see cref="ModelListResponse{T}"/>
        /// </summary>
        [Test]
        public void ModelListResponse_WhenCreated_CreateBody()
        {
            // Create a list of test models
            var modelList = new List<User>
            {
                new User
                {
                    Name = "Test"
                },
                new User
                {
                    Name = "Test2"
                }
            };

            // Create a new response
            var response = new ModelListResponse<User>("Users created", modelList);

            // Check the result
            Assert.That(response.Response.Message, Is.EqualTo("Users created"));
            Assert.That(response.Response.ModelList, Is.EqualTo(modelList));
        }
    }
}
