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
    ///     User information
    ///     See: https://docs.atlassian.com/confluence/REST/latest
    /// </summary>
    [JsonObject]
	public class User
	{
		/// <summary>
		///     The name which is displayed in the UI, usually "firstname lastname"
		/// </summary>
		[JsonProperty("displayName", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string DisplayName { get; set; }

		/// <summary>
		///     Information on the profile picture
		/// </summary>
		[JsonProperty("profilePicture", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Picture ProfilePicture { get; set; }

		/// <summary>
		///     Type of user
		/// </summary>
		[JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Type { get; set; }

		/// <summary>
		///     A unique key for the user
		/// </summary>
		[JsonProperty("userKey", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string UserKey { get; set; }

		/// <summary>
		///     The username
		/// </summary>
		[JsonProperty("username", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Username { get; set; }
	}
}