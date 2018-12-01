//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016-2018 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.Confluence
// 
//  Dapplo.Confluence is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.Confluence is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Confluence. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using Newtonsoft.Json;

#endregion

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     Error information
    /// </summary>
    [JsonObject]
	public class Error
	{
		/// <summary>
		///     Error message from Confluence
		/// </summary>
		[JsonProperty("message", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Message { get; set; }

		/// <summary>
		///     Confluence status code
		/// </summary>
		[JsonProperty("statusCode", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public int StatusCode { get; set; }
	}
}