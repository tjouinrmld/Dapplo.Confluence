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

#region using

using System;
using Newtonsoft.Json;

#endregion

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     Links information
    ///     See: https://docs.atlassian.com/confluence/REST/latest
    /// </summary>
    [JsonObject]
	public class Links
	{
		/// <summary>
		///     The base (hostname) for the server
		/// </summary>
		[JsonProperty("base", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Uri Base { get; set; }

		/// <summary>
		///     A path to the rest API to where this belongs, content has a collection of "/rest/api/content"
		/// </summary>
		[JsonProperty("collection", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Collection { get; set; }

		/// <summary>
		///     TODO: What is this?
		/// </summary>
		[JsonProperty("context", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Context { get; set; }

		/// <summary>
		///     The link, usually for attachments, to download the content
		/// </summary>
		[JsonProperty("download", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Download { get; set; }

		/// <summary>
		///     A link to the entity itself (so one can find it again)
		/// </summary>
		[JsonProperty("self", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Uri Self { get; set; }

		/// <summary>
		///     Status of a task
		/// </summary>
		[JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Status { get; set; }

		/// <summary>
		///     A short link to the content, relative to the hostname (and port)
		/// </summary>
		[JsonProperty("tinyui", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string TinyUi { get; set; }

		/// <summary>
		///     A normal, but well readable, link to the content
		/// </summary>
		[JsonProperty("webui", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string WebUi { get; set; }

		/// <summary>
		///     A link to the next result when using paging
		/// </summary>
		[JsonProperty("next", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Uri Next { get; set; }
	}
}