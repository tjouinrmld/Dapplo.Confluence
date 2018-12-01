#region Dapplo 2018 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017-2018 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Confluence
// 
// Dapplo.Confluence is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Confluence is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Confluence. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

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
        public ConfluenceException(HttpStatusCode httpStatusCode, Error error = null) : base(error?.Message ?? $"{httpStatusCode}({(int) httpStatusCode})")
        {
        }
    }

}
