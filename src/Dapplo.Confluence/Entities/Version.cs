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

using System;
using Newtonsoft.Json;

#endregion

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     Version information
    ///     See: https://docs.atlassian.com/confluence/REST/latest
    /// </summary>
    [JsonObject]
	public class Version
	{
		/// <summary>
		///     Who made this version
		/// </summary>
		[JsonProperty("by", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public User By { get; set; }

		/// <summary>
		///     Is it a small version change
		/// </summary>
		[JsonProperty("minorEdit", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public bool IsMinorEdit { get; set; }

		/// <summary>
		///     A message for the version
		/// </summary>
		[JsonProperty("message", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Message { get; set; }

		/// <summary>
		///     Version number
		/// </summary>
		[JsonProperty("number", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public int Number { get; set; }

		/// <summary>
		///     When was this version
		/// </summary>
		[JsonProperty("when", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public DateTimeOffset When { get; set; }
	}
}