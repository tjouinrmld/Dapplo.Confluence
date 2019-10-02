//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016-2019 Dapplo
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

using Newtonsoft.Json;

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     Space information
    ///     See: https://docs.atlassian.com/confluence/REST/latest
    /// </summary>
    [JsonObject]
	public class Picture
	{
		/// <summary>
		///     Height of the picture
		/// </summary>
		[JsonProperty("height", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public int Height { get; set; }

		/// <summary>
		///     Is this picture the default
		/// </summary>
		[JsonProperty("isDefault", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public bool IsDefault { get; set; }

		/// <summary>
		///     The path for the picture relative to the hostname (and port) of the server, this is outside the Rest API path
		/// </summary>
		[JsonProperty("path", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Path { get; set; }

		/// <summary>
		///     Width of the picture
		/// </summary>
		[JsonProperty("width", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public int Width { get; set; }
	}
}