// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UriExtensionMethodsForBuildingTest.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri.Test
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    using Newtonsoft.Json;

    using Xunit;

    /// <summary>
    /// Tests for the URL building methods.
    /// </summary>
    public class UriExtensionMethodsForBuildingTest
    {
        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void AppendPathSegment_ValidUrlNoTrailingSlashAppendedWithSegment_ValidlyAppendedResult()
        {
            // arrange
            var baseUrl = "http://myurl";
            var uri = new Uri(baseUrl);
            var pathSegmentToAdd = "myController";
            var expectedAbsoluteUri = baseUrl + "/" + pathSegmentToAdd;

            // act
            var actualUri = uri.AppendPathSegment(pathSegmentToAdd);

            // assert
            var actualAbsoluteUri = actualUri.AbsoluteUri;
            Assert.Equal(expectedAbsoluteUri, actualAbsoluteUri);
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void AppendPathSegment_ValidUrlNoTrailingSlashAppendedWithSegmentTwice_ValidlyAppendedResult()
        {
            // arrange
            var baseUrl = "http://myurl";
            var uri = new Uri(baseUrl);
            var pathSegmentToAddOne = "myController";
            var pathSegmentToAddTwo = "myAction";
            var expectedAbsoluteUri = baseUrl + "/" + pathSegmentToAddOne + "/" + pathSegmentToAddTwo;

            // act
            var actualUri = uri.AppendPathSegment(pathSegmentToAddOne).AppendPathSegment(pathSegmentToAddTwo);

            // assert
            var actualAbsoluteUri = actualUri.AbsoluteUri;
            Assert.Equal(expectedAbsoluteUri, actualAbsoluteUri);
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void AppendPathSegment_ValidUrlTrailingSlashAppendedWithSegment_ValidlyAppendedResult()
        {
            // arrange
            var baseUrl = "http://myurl/";
            var uri = new Uri(baseUrl);
            var pathSegmentToAdd = "myController";
            var expectedAbsoluteUri = baseUrl + pathSegmentToAdd;

            // act
            var actualUri = uri.AppendPathSegment(pathSegmentToAdd);

            // assert
            var actualAbsoluteUri = actualUri.AbsoluteUri;
            Assert.Equal(expectedAbsoluteUri, actualAbsoluteUri);
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void AppendQueryStringParam_ValidUrlAppendedWithQueryStringParamStringTwice_ValidlyAppendedResult()
        {
            // arrange
            var baseUrl = "http://myurl/";
            var uri = new Uri(baseUrl);

            var paramNameOne = "paramNameOne";
            var paramValueOne = "paramValueOne";

            var paramNameTwo = "paramNameTwo";
            var paramValueTwo = "paramValueTwo";

            var expectedAbsoluteUri = baseUrl + "?" + paramNameOne + "=" + paramValueOne + "&" + paramNameTwo + "=" + paramValueTwo;

            // act
            var actualUri = uri.AppendQueryStringParam(paramNameOne, paramValueOne)
                .AppendQueryStringParam(paramNameTwo, paramValueTwo);

            // assert
            var actualAbsoluteUri = actualUri.AbsoluteUri;
            Assert.Equal(expectedAbsoluteUri, actualAbsoluteUri);
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void AppendQueryStringParam_ValidUrlAppendedWithQueryStringParam_ValidlyAppendedResult()
        {
            // arrange
            var baseUrl = "http://myurl/";
            var uri = new Uri(baseUrl);

            var paramName = "paramName";
            var paramValue = "paramValue";
            var expectedAbsoluteUri = baseUrl + "?" + paramName + "=" + paramValue;

            // act
            var actualUri = uri.AppendQueryStringParam(paramName, paramValue);

            // assert
            var actualAbsoluteUri = actualUri.AbsoluteUri;
            Assert.Equal(expectedAbsoluteUri, actualAbsoluteUri);
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void AppendQueryStringParams_ValidUrlAppendedWithQueryStringParamsList_ValidlyAppendedResult()
        {
            // arrange
            var baseUrl = "http://myurl/";
            var uri = new Uri(baseUrl);

            var paramNameOne = "paramNameOne";
            var paramValueOne = "paramValueOne";

            var paramNameTwo = "paramNameTwo";
            var paramValueTwo = "paramValueTwo";

            var expectedAbsoluteUri = baseUrl + "?" + paramNameOne + "=" + paramValueOne + "&" + paramNameTwo + "=" + paramValueTwo;
            var listOfParams = new[]
                                   {
                                       new KeyValuePair<string, string>(paramNameOne, paramValueOne),
                                       new KeyValuePair<string, string>(paramNameTwo, paramValueTwo),
                                   };

            // act
            var actualUri = uri.AppendQueryStringParams(listOfParams);

            // assert
            var actualAbsoluteUri = actualUri.AbsoluteUri;
            Assert.Equal(expectedAbsoluteUri, actualAbsoluteUri);
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void AppendQueryStringParams_ValidUrlAppendedWithQueryStringParamsDictionary_ValidlyAppendedResult()
        {
            // arrange
            var baseUrl = "http://myurl/";
            var uri = new Uri(baseUrl);

            var paramNameOne = "paramNameOne";
            var paramValueOne = "paramValueOne";

            var paramNameTwo = "paramNameTwo";
            var paramValueTwo = "paramValueTwo";

            var expectedAbsoluteUri = baseUrl + "?" + paramNameOne + "=" + paramValueOne + "&" + paramNameTwo + "=" + paramValueTwo;
            var dictionaryOfParams = new Dictionary<string, string>()
                                         {
                                             { paramNameOne, paramValueOne },
                                             { paramNameTwo, paramValueTwo },
                                         };

            // act
            var actualUri = uri.AppendQueryStringParams(dictionaryOfParams);

            // assert
            var actualAbsoluteUri = actualUri.AbsoluteUri;
            Assert.Equal(expectedAbsoluteUri, actualAbsoluteUri);
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public static void AppendPathSegmentThenAppendQueryStringParamTheAppendPathSegment_ValidUrlOperations_ValidlyAppendedResult()
        {
            // arrange
            var baseUrl = "http://myurl";
            var uri = new Uri(baseUrl);

            var pathSegment = "myController";

            var paramNameOne = "paramNameOne";
            var paramValueOne = "paramValueOne";

            var paramNameTwo = "paramNameTwo";
            var paramValueTwo = "paramValueTwo";

            var expectedAbsoluteUri = baseUrl + "/" + pathSegment + "?" + paramNameOne + "=" + paramValueOne + "&" + paramNameTwo + "=" + paramValueTwo;

            // act
            var actualUri =
                uri.AppendQueryStringParam(paramNameOne, paramValueOne)
                    .AppendPathSegment(pathSegment)
                    .AppendQueryStringParam(paramNameTwo, paramValueTwo);

            // assert
            var actualAbsoluteUri = actualUri.AbsoluteUri;
            Assert.Equal(expectedAbsoluteUri, actualAbsoluteUri);
        }
    }
}
