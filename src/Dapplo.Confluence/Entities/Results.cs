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

using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     A container to store pageable results
    ///     See: https://docs.atlassian.com/confluence/REST/latest
    /// </summary>
    [JsonObject]
	public class Result<TResult> : IEnumerable<TResult>
	{
		/// <summary>
		///     The result is limited by
		/// </summary>
		[JsonProperty("limit", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public int Limit { get; set; }

		/// <summary>
		///     Different links for this entity, depending on the entry
		/// </summary>
		[JsonProperty("_links", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Links Links { get; set; }

		/// <summary>
		///     The actual requested information
		/// </summary>
		[JsonProperty("results", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public IList<TResult> Results { get; set; }

		/// <summary>
		///     How many elements
		/// </summary>
		[JsonProperty("size", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public int Size { get; set; }

		/// <summary>
		///     The start of the elements, this is used for paging
		/// </summary>
		[JsonProperty("start", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public int Start { get; set; }

        /// <summary>
        /// Returns if there are more results as requested
        /// </summary>
        [JsonIgnore]
        public bool HasNext => Links?.Next != null;

        IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

	    /// <inheritdoc />
	    public IEnumerator<TResult> GetEnumerator()
		{
			return Results.GetEnumerator();
		}
	}
}