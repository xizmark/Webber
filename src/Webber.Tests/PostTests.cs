﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Xizmark.Webber.Tests
{
    [TestClass]
    public class PostTests
    {
        [TestMethod]
        public void Can_Deserialize_Successful_Post_Response()
        {
            var url = "http://jsonplaceholder.typicode.com/posts";

            var request = new
            {
                title = "Webber TestTitle " + DateTime.Now.Ticks,
                body = "Webber TestBody " + DateTime.Now.Ticks,
                userId = 404
            };

            var data = JsonConvert.SerializeObject(request);

            var webberResponse = Xizmark.Webber.Webber.Post<SamplePost>(url, data);

            Console.WriteLine(webberResponse.RawResult);

            Assert.IsNotNull(webberResponse);
            Assert.IsTrue(webberResponse.Success);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(webberResponse.RawResult));
            Assert.IsNotNull(webberResponse.Result);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(webberResponse.Result.Body));
            Assert.IsTrue(!string.IsNullOrWhiteSpace(webberResponse.Result.Title));
            Assert.IsTrue(webberResponse.Result.UserId > 0);
            Assert.IsTrue(webberResponse.Result.Id > 0);
        }

        public class SamplePost
        {
            public string Body;
            public int Id;
            public string Title;
            public int UserId;
        }
    }
}