// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using System.Net;
using System.Net.Http;
using Dapplo.Confluence.Entities;

namespace Dapplo.Confluence
{
    /// <summary>
    /// This wraps the HttpRequestException with Confluence specific informationen
    /// </summary>
    public class ConfluenceException : HttpRequestException
    {
        /// <summary>
        /// Constructor with a HttpStatus code and an error response
        /// </summary>
        /// <param name="httpStatusCode">HttpStatusCode</param>
        /// <param name="response">string with the error response message</param>
        public ConfluenceException(HttpStatusCode httpStatusCode, string response) : base($"{httpStatusCode}({(int)httpStatusCode}) : {response}")
        {
        }

        /// <summary>
        ///  Constructor with a HttpStatus code and an Error object
        /// </summary>
        /// <param name="httpStatusCode">HttpStatusCode</param>
        /// <param name="error">Error</param>
        public ConfluenceException(HttpStatusCode httpStatusCode, Error error = null) : base(error?.Message ?? $"{httpStatusCode}({(int)httpStatusCode})")
        {
        }
    }

}
