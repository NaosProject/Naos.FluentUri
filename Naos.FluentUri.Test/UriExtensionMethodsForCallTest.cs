// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UriExtensionMethodsForCallTest.cs" company="Naos">
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
    using System.Reflection;
    using System.Web;

    using Xunit;

    public class UriExtensionMethodsForCallTest
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
            var result = new Uri(url).WithCookie(cookie).Get<Dictionary<string, Dictionary<string, string>>>();
            var headersDictionary = result["headers"];
            var actualCookieValue = headersDictionary["Cookie"];

            // assert
            Assert.NotNull(headersDictionary);
            Assert.NotNull(actualCookieValue);
            Assert.Equal(cookieName + "=" + expectedCookieValue, actualCookieValue);
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void CallWithCookieTwice_NetCookie_CookieIsSent()
        {
            // arrange
            var url = "http://httpbin.org/headers";
            var cookieOne = new Cookie("MyCookie1", "Whatsup")
                             {
                                 Path = "/",
                                 Domain = "httpbin.org",
                                 Expires = DateTime.Now.AddDays(11)
                             };

            var cookieTwo = new Cookie("MyCookie2", "Whatsup")
                             {
                                 Path = "/",
                                 Domain = "httpbin.org",
                                 Expires = DateTime.Now.AddDays(11)
                             };

            // act
            var result = new Uri(url).WithCookie(cookieOne).WithCookie(cookieTwo).Get<Dictionary<string, Dictionary<string, string>>>();

            // assert
            var headersDictionary = result["headers"];
            Assert.NotNull(headersDictionary);
            var actualCookieValues = headersDictionary["Cookie"].Split(';');
            var actualCookieValueOne = actualCookieValues[0].Trim();
            var actualCookieValueTwo = actualCookieValues[1].Trim();
            Assert.NotNull(actualCookieValueOne);
            Assert.NotNull(actualCookieValueTwo);
            Assert.Equal(cookieOne.Name + "=" + cookieOne.Value, actualCookieValueOne);
            Assert.Equal(cookieTwo.Name + "=" + cookieTwo.Value, actualCookieValueTwo);
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
            var result = new Uri(url).WithCookie(cookie).Get<Dictionary<string, Dictionary<string, string>>>();
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
            var result = new Uri(url).WithHeaders(headers).Get<Dictionary<string, Dictionary<string, string>>>();
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
            var result = new Uri(url).WithHeaders(headers).Get<Dictionary<string, Dictionary<string, string>>>();
            var headersDictionary = result["headers"];
            var actualValue = headersDictionary[ourHeaderName];

            // assert
            Assert.NotNull(headersDictionary);
            Assert.NotNull(actualValue);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void CallWithHeader_ValidData_HeadersAreSent()
        {
            // arrange
            var url = "http://httpbin.org/headers";
            var ourHeaderName = "Headername";
            var expectedValue = "headerValue";

            // act
            var result = new Uri(url).WithHeader(ourHeaderName, expectedValue).Get<Dictionary<string, Dictionary<string, string>>>();
            var headersDictionary = result["headers"];
            var actualValue = headersDictionary[ourHeaderName];

            // assert
            Assert.NotNull(headersDictionary);
            Assert.NotNull(actualValue);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void CallWithHeadersTwice_KeyValuePairArray_HeadersAreSent()
        {
            // arrange
            var url = "http://httpbin.org/headers";
            var headerOne = new KeyValuePair<string, string>("Headername1", "HeaderValue1");
            var headerTwo = new KeyValuePair<string, string>("Headername2", "HeaderValue2");

            var headersOne = new List<KeyValuePair<string, string>> { headerOne };
            var headersTwo = new List<KeyValuePair<string, string>> { headerTwo };

            // act
            var result = new Uri(url).WithHeaders(headersOne.ToArray()).WithHeaders(headersTwo.ToArray()).Get<Dictionary<string, Dictionary<string, string>>>();
            var headersDictionary = result["headers"];
            var actualValueOne = headersDictionary[headerOne.Key];
            var actualValueTwo = headersDictionary[headerTwo.Key];

            // assert
            Assert.NotNull(headersDictionary);
            Assert.NotNull(actualValueOne);
            Assert.NotNull(actualValueTwo);
            Assert.Equal(headerOne.Value, actualValueOne);
            Assert.Equal(headerTwo.Value, actualValueTwo);
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void CallWithTimeout_ShortTimeout_ExceptionThrown()
        {
            // arrange
            var timeout = TimeSpan.FromTicks(1); // using this crazy short timeout to guarantee a failure. 
            var url = "http://httpbin.org/ip";

            // act
            try
            {
                new Uri(url).WithTimeout(timeout).Get<IDictionary<string, string>>();
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
            var result = new Uri(url).Get<IDictionary<string, string>>();

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
            var result = new Uri(url).WithBody(body).Post<dynamic>();

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
            var result = new Uri(url).WithBody(body).Put<dynamic>();

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
            var result = new Uri(url).WithBody(body).Delete<dynamic>();

            // assert
            Assert.NotNull(result);
            Assert.Equal(body.Key, result.json.Key.ToString());
            Assert.Equal(body.Value, result.json.Value.ToString());
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void Call_CustomVerb_ValidResponse()
        {
            // arrange
            var url = "http://httpbin.org/delete";
            var body = new KeyValuePair<string, string>("BodyName", "BodyValue");

            // act
            var result = new Uri(url).WithBody(body).CallWithVerb<dynamic>("DELETE");

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
                delegate { new Uri(url).WithBody(null).WithBody(null).Get(); });
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void Call_WithTimeoutTwice_ThrowsException()
        {
            // arrange
            var url = "http://fakeUrl";

            // act/assert
            Assert.Throws<DuplicateCallUsingFluentGrammarException>(
                delegate { new Uri(url).WithTimeout(new TimeSpan()).WithTimeout(new TimeSpan()).Get(); });
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void Call_ReturnsResponseHeaders_ResponseHeadersCaptured()
        {
            // arrange
            var url = "http://httpbin.org/headers";
            var outputHeaders = new KeyValuePair<string, string>[0];

            // act
            var result = new Uri(url).WithResponseHeaderSaveAction(_ => outputHeaders = _).Get<dynamic>();

            // assert
            Assert.NotEqual(0, outputHeaders.Length);
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void ExtensionMethodsMatchICallUriAll()
        {
            // since they can't share an interface we need to confirm that the methods are the same for consistent experience...
            var extensionType = typeof(UriExtensionMethodsForCall);
            var interfaceType = typeof(ICallOnUriAll);
            var methodsOnType = extensionType.GetMethods().ToList();
            var interfacesOnInterface = interfaceType.GetInterfaces();
            var methodsOnInterface = new List<MethodInfo>();
            foreach (var type in interfacesOnInterface)
            {
                var methods = type.GetMethods();
                methodsOnInterface.AddRange(methods);
            }

            var removeOnesToNotCompare = new Func<List<MethodInfo>, List<MethodInfo>>(
                delegate(List<MethodInfo> list)
                    {
                        var methodsToSkip = new List<string>(new[] { "ToString", "Equals", "GetHashCode", "GetType" });
                        return list.Where(methodInfo => !methodsToSkip.Contains(methodInfo.Name)).ToList();
                    });

            methodsOnInterface = removeOnesToNotCompare(methodsOnInterface);
            methodsOnType = removeOnesToNotCompare(methodsOnType);

            methodsOnInterface = methodsOnInterface.OrderBy(_ => _.Name).ToList();
            methodsOnType = methodsOnType.OrderBy(_ => _.Name).ToList();

            Assert.Equal(methodsOnInterface.Count, methodsOnType.Count);

            for (var idx = 0; idx < methodsOnInterface.Count; idx++)
            {
                Assert.Equal(methodsOnInterface[idx].Name, methodsOnType[idx].Name);
            }
        }
    }
}
