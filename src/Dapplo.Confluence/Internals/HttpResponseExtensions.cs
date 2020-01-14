// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using System.Linq;
using System.Net;
using Dapplo.Confluence.Entities;
using Dapplo.HttpExtensions;
using Dapplo.Log;

namespace Dapplo.Confluence.Internals
{
    /// <summary>
    /// Extensions for the HttpResponse object
    /// </summary>
    internal static class HttpResponseExtensions
    {
        private static readonly LogSource Log = new LogSource();

        /// <summary>
        /// Helper method to log the error
        /// </summary>
        /// <param name="httpStatusCode">HttpStatusCode</param>
        /// <param name="error">Error</param>
        private static void LogError(HttpStatusCode httpStatusCode, Error error = null)
        {
            // Log all error information
            Log.Warn().WriteLine("Http status code: {0} ({1}). Response from server: {2}", httpStatusCode.ToString(), (int)httpStatusCode, error?.Message ?? httpStatusCode.ToString());
        }

        /// <summary>
        ///     Helper method for handling errors in the response, if the response has an error an exception is thrown.
        ///     Else the real response is returned.
        /// </summary>
        /// <typeparam name="TResponse">Type for the ok content</typeparam>
        /// <param name="expectedHttpStatusCodes">optional HttpStatusCode(s) to expect</param>
        /// <param name="response">TResponse</param>
        /// <returns>TResponse</returns>
        public static TResponse HandleErrors<TResponse>(this HttpResponse<TResponse, string> response, params HttpStatusCode[] expectedHttpStatusCodes)
            where TResponse : class
        {
            if (expectedHttpStatusCodes != null && expectedHttpStatusCodes.Any(code => code == response.StatusCode))
            {
                return response.Response;
            }
            if (!response.HasError)
            {
                return response.Response;
            }

            // Log all error information
            Log.Warn().WriteLine("Http status code: {0} ({1}). Response from server: {2}", response.StatusCode.ToString(), (int)response.StatusCode, response.ErrorResponse ?? response.StatusCode.ToString());
            throw new ConfluenceException(response.StatusCode, response.ErrorResponse);
        }

        /// <summary>
        ///     Helper method for handling errors in the response, if the response has an error an exception is thrown.
        ///     Else the real response is returned.
        /// </summary>
        /// <typeparam name="TResponse">Type for the ok content</typeparam>
        /// <typeparam name="TError">Type for the error content</typeparam>
        /// <param name="expectedHttpStatusCodes">optional HttpStatusCode(s) to expect</param>
        /// <param name="response">TResponse</param>
        /// <returns>TResponse</returns>
        public static TResponse HandleErrors<TResponse, TError>(this HttpResponse<TResponse, TError> response, params HttpStatusCode[] expectedHttpStatusCodes)
            where TResponse : class where TError : Error
        {
            if (expectedHttpStatusCodes != null && expectedHttpStatusCodes.Any(code => code == response.StatusCode))
            {
                return response.Response;
            }
            if (!response.HasError)
            {
                return response.Response;
            }

            // This is currently not shown, as the HttpExtensions library already throws an exception, but might be after finding a solution for that
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                Log.Warn().WriteLine("You might need an API-Token instead of a password, use the following URL to generate one: https://id.atlassian.com/manage/api-tokens");
            }

            // Log all error information
            LogError(response.StatusCode, response.ErrorResponse);
            throw new ConfluenceException(response.StatusCode, response.ErrorResponse);
        }

        /// <summary>
        ///     Helper method for handling errors in the response, if the response doesn't have the expected status code an
        ///     exception is thrown.
        ///     Else the real response is returned.
        /// </summary>
        /// <typeparam name="TResponse">Type for the ok content</typeparam>
        /// <param name="expectedHttpStatusCodes">HttpStatusCode(s) to expect</param>
        /// <param name="response">TResponse</param>
        /// <returns>TResponse</returns>
        public static TResponse HandleErrors<TResponse>(this HttpResponse<TResponse> response, params HttpStatusCode[] expectedHttpStatusCodes)
            where TResponse : class
        {
            if (expectedHttpStatusCodes == null || expectedHttpStatusCodes.Length == 0)
            {
                expectedHttpStatusCodes = new[] { HttpStatusCode.OK };
            }
            if (expectedHttpStatusCodes.Any(code => code == response.StatusCode))
            {
                return response.Response;
            }
            LogError(response.StatusCode);
            throw new ConfluenceException(response.StatusCode);
        }

        /// <summary>
        ///     Helper method for handling errors in the response, if the response doesn't have the expected status code an
        ///     exception is thrown.
        /// </summary>
        /// <param name="expectedHttpStatusCodes">HttpStatusCode(s) to expect</param>
        /// <param name="response">TResponse</param>
        public static void HandleStatusCode(this HttpResponse response, params HttpStatusCode[] expectedHttpStatusCodes)
        {
            if (expectedHttpStatusCodes == null || expectedHttpStatusCodes.Length == 0)
            {
                expectedHttpStatusCodes = new[] { HttpStatusCode.OK };
            }
            if (expectedHttpStatusCodes.Any(code => code == response.StatusCode))
            {
                return;
            }
            LogError(response.StatusCode);
            throw new ConfluenceException(response.StatusCode);
        }

        /// <summary>
        ///     Helper method for handling errors in the response, if the response doesn't have the expected status code an
        ///     exception is thrown.
        /// </summary>
        /// <typeparam name="TError">Type for the error</typeparam>
        /// <param name="expectedHttpStatusCodes">HttpStatusCode(s) to expect</param>
        /// <param name="response">TResponse</param>
        public static void HandleStatusCode<TError>(this HttpResponseWithError<TError> response, params HttpStatusCode[] expectedHttpStatusCodes) where TError : Error
        {
            if (expectedHttpStatusCodes == null || expectedHttpStatusCodes.Length == 0)
            {
                expectedHttpStatusCodes = new[] { HttpStatusCode.OK };
            }
            if (expectedHttpStatusCodes.Any(code => code == response.StatusCode))
            {
                return;
            }
            LogError(response.StatusCode, response.ErrorResponse);
            throw new ConfluenceException(response.StatusCode, response.ErrorResponse);
        }

        /// <summary>
        ///     Helper method for handling errors in the response, if the response doesn't have the expected status code an
        ///     exception is thrown.
        /// </summary>
        /// <param name="expectedHttpStatusCodes">HttpStatusCode(s) to expect</param>
        /// <param name="response">TResponse</param>
        public static void HandleStatusCode(this HttpResponseWithError<string> response, params HttpStatusCode[] expectedHttpStatusCodes)
        {
            if (expectedHttpStatusCodes == null || expectedHttpStatusCodes.Length == 0)
            {
                expectedHttpStatusCodes = new[] { HttpStatusCode.OK };
            }
            if (expectedHttpStatusCodes.Any(code => code == response.StatusCode))
            {
                return;
            }
            Log.Warn().WriteLine("Http status code: {0}. Response from server: {1}", response.StatusCode, response.ErrorResponse);
            throw new ConfluenceException(response.StatusCode, response.ErrorResponse);
        }
    }
}
