//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2015-2016 Dapplo
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

using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion

namespace Dapplo.Confluence.Entities
{
	/// <summary>
	///     Space information
	///     See: https://docs.atlassian.com/confluence/REST/latest
	/// </summary>
	[DataContract]
	public class Content
	{
		/// <summary>
		/// The values that are expandable
		/// </summary>
		[DataMember(Name = "_expandable")]
		public IDictionary<string, string> Expandables { get; set; }

		/// <summary>
		/// Unique ID for the content
		/// </summary>
		[DataMember(Name = "id")]
		public string Id { get; set; }

		/// <summary>
		/// Different links for this entity, depending on the entry
		/// </summary>
		[DataMember(Name = "_links")]
		public Links Links { get; set; }

		/// <summary>
		/// The space where this content is
		/// </summary>
		[DataMember(Name = "space")]
		public Space Space { get; set; }

		/// <summary>
		/// The title of the content
		/// </summary>
		[DataMember(Name = "title")]
		public string Title { get; set; }

		/// <summary>
		/// The type for the content, e.g. page
		/// </summary>
		[DataMember(Name = "type")]
		public string Type { get; set; }

		/// <summary>
		/// Version information for the content, this is not filled unless expand=version
		/// </summary>
		[DataMember(Name = "version")]
		public Version Version { get; set; }
	}
}