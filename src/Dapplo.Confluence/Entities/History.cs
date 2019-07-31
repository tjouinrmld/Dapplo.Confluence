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
using System.Collections.Generic;
using Newtonsoft.Json;

#endregion

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     History information
    ///     See: https://docs.atlassian.com/confluence/REST/latest
    /// </summary>
    [JsonObject]
	public class History
	{
		/// <summary>
		///     User who created it
		/// </summary>
		[JsonProperty("createdBy", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public User CreatedBy { get; set; }

		/// <summary>
		///     Created data
		/// </summary>
		[JsonProperty("createdDate", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public DateTimeOffset CreatedDate { get; set; }

		/// <summary>
		///     The values that are expandable
		/// </summary>
		[JsonProperty("_expandable", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public IDictionary<string, string> Expandables { get; set; }

		/// <summary>
		///     Last updated information
		/// </summary>
		[JsonProperty("lastUpdated", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public LastUpdated LastUpdated { get; set; }

		/// <summary>
		///     Is this history entity the latest entry?
		/// </summary>
		[JsonProperty("latest", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public bool Latest { get; set; }

		/// <summary>
		///     Different links for this entity, depending on the entry
		/// </summary>
		[JsonProperty("_links", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Links Links { get; set; }
	}
}