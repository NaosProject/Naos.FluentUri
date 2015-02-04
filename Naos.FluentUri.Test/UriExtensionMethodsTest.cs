// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UriExtensionMethodsTest.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri.Test
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using System.Web;

    using Xunit;

    public class UriExtensionMethodsTest
    {
        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void CallWithCookie_NetCookie_CookieIsSent()
        {
            // arrange
            var url = "http://httpbin.org/headers";
            var cookieName = "MyCookie";
            var cookie = new Cookie(cookieName, "Whatsup")
                             {
                                 Path = "/",
                                 Domain = "httpbin.org",
                                 Expires = DateTime.Now.AddDays(11)
                             };

            var expectedCookieValue = cookie.Value;

            // act
            var result = new Uri(url).Get().WithCookie(cookie).ExecuteForResult<Dictionary<string, Dictionary<string, string>>>();
            var headersDictionary = result["headers"];
            var actualCookieValue = headersDictionary["Cookie"];

            // assert
            Assert.NotNull(headersDictionary);
            Assert.NotNull(actualCookieValue);
            Assert.Equal(cookieName + "=" + expectedCookieValue, actualCookieValue);
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void CallWithCookie_WebHttpCookie_CookieIsSent()
        {
            // arrange
            var url = "http://httpbin.org/headers";
            var cookieName = "MyCookie";
            var cookie = new HttpCookie(cookieName, "Whatsup")
                             {
                                 Path = "/",
                                 Domain = "httpbin.org",
                                 Expires = DateTime.Now.AddDays(11)
                             };

            var expectedCookieValue = cookie.Value;

            // act
            var result = new Uri(url).Get().WithCookie(cookie).ExecuteForResult<Dictionary<string, Dictionary<string, string>>>();
            var headersDictionary = result["headers"];
            var actualCookieValue = headersDictionary["Cookie"];

            // assert
            Assert.NotNull(headersDictionary);
            Assert.NotNull(actualCookieValue);
            Assert.Equal(cookieName + "=" + expectedCookieValue, actualCookieValue);
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void CallWithHeaders_NameValueCollection_HeadersAreSent()
        {
            // arrange
            var url = "http://httpbin.org/headers";
            var headers = new NameValueCollection();
            var ourHeaderName = "Headername";
            var expectedValue = "headerValue";
            headers.Add(ourHeaderName, expectedValue);

            // act
            var result = new Uri(url).Get().WithHeaders(headers).ExecuteForResult<Dictionary<string, Dictionary<string, string>>>();
            var headersDictionary = result["headers"];
            var actualValue = headersDictionary[ourHeaderName];

            // assert
            Assert.NotNull(headersDictionary);
            Assert.NotNull(actualValue);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void CallWithHeaders_WebHeaderCollection_HeadersAreSent()
        {
            // arrange
            var url = "http://httpbin.org/headers";
            var headers = new WebHeaderCollection();
            var ourHeaderName = "Headername";
            var expectedValue = "headerValue";
            headers.Add(ourHeaderName, expectedValue);

            // act
            var result = new Uri(url).Get().WithHeaders(headers).ExecuteForResult<Dictionary<string, Dictionary<string, string>>>();
            var headersDictionary = result["headers"];
            var actualValue = headersDictionary[ourHeaderName];

            // assert
            Assert.NotNull(headersDictionary);
            Assert.NotNull(actualValue);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void CallWithTimeout_ShortTimeout_ExceptionThrown()
        {
            // arrange
            var timeout = TimeSpan.FromMilliseconds(4); // using this crazy short timeout to guarantee a failure. 
            var url = "http://httpbin.org/ip";

            // act
            try
            {
                new Uri(url).Get().WithTimeout(timeout).ExecuteForResult<IDictionary<string, string>>();
                Assert.True(false, "Should not have reached here.");
            }
            catch (WebException ex)
            {
                // assert
                Assert.True(ex.Message.Contains("The operation has timed out"));
            }
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void Call_BasicGet_ValidResponse()
        {
            // arrange
            var url = "http://httpbin.org/ip";
            var expectedKey = "origin";

            // act
            var result = new Uri(url).Get().ExecuteForResult<IDictionary<string, string>>();

            // assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
            Assert.Equal(expectedKey, result.Keys.Take(1).Single());
            Assert.NotNull(result[expectedKey]);
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void Call_BasicPost_ValidResponse()
        {
            // arrange
            var url = "http://httpbin.org/post";
            var body = new KeyValuePair<string, string>("BodyName", "BodyValue");

            // act
            var result = new Uri(url).Post().WithBody(body).ExecuteForResult<dynamic>();

            // assert
            Assert.NotNull(result);
            Assert.Equal(body.Key, result.json.Key.ToString());
            Assert.Equal(body.Value, result.json.Value.ToString());
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void Call_BasicPut_ValidResponse()
        {
            // arrange
            var url = "http://httpbin.org/put";
            var body = new KeyValuePair<string, string>("BodyName", "BodyValue");

            // act
            var result = new Uri(url).Put().WithBody(body).ExecuteForResult<dynamic>();

            // assert
            Assert.NotNull(result);
            Assert.Equal(body.Key, result.json.Key.ToString());
            Assert.Equal(body.Value, result.json.Value.ToString());
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void Call_BasicDelete_ValidResponse()
        {
            // arrange
            var url = "http://httpbin.org/delete";
            var body = new KeyValuePair<string, string>("BodyName", "BodyValue");

            // act
            var result = new Uri(url).Delete().WithBody(body).ExecuteForResult<dynamic>();

            // assert
            Assert.NotNull(result);
            Assert.Equal(body.Key, result.json.Key.ToString());
            Assert.Equal(body.Value, result.json.Value.ToString());
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void Call_WithBodyTwice_ThrowsException()
        {
            // arrange
            var url = "http://fakeUrl";

            // act/assert
            Assert.Throws<DuplicateCallUsingFluentGrammarException>(
                delegate { new Uri(url).Get().WithBody(null).WithBody(null); });
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void Call_WithHeadersTwice_ThrowsException()
        {
            // arrange
            var url = "http://fakeUrl";

            // act/assert
            Assert.Throws<DuplicateCallUsingFluentGrammarException>(
                delegate { new Uri(url).Get().WithHeaders(null).WithHeaders(null); });
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void Call_WithCookieTwice_ThrowsException()
        {
            // arrange
            var url = "http://fakeUrl";

            // act/assert
            Assert.Throws<DuplicateCallUsingFluentGrammarException>(
                delegate { new Uri(url).Get().WithCookie(new Cookie()).WithCookie(new Cookie()); });
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void Call_WithTimeoutTwice_ThrowsException()
        {
            // arrange
            var url = "http://fakeUrl";

            // act/assert
            Assert.Throws<DuplicateCallUsingFluentGrammarException>(
                delegate { new Uri(url).Get().WithTimeout(new TimeSpan()).WithTimeout(new TimeSpan()); });
        }
    }
}
