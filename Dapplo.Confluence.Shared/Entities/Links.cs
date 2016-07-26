#region Dapplo 2016 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2016 Dapplo
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

#region Usings

using System;
using System.Runtime.Serialization;

#endregion

namespace Dapplo.Confluence.Entities
{
	/// <summary>
	///     Links information
	///     See: https://docs.atlassian.com/confluence/REST/latest
	/// </summary>
	[DataContract]
	public class Links
	{
		/// <summary>
		///     The base (hostname) for the server
		/// </summary>
		[DataMember(Name = "base")]
		public Uri Base { get; set; }

		/// <summary>
		///     A path to the rest API to where this belongs, content has a collection of "/rest/api/content"
		/// </summary>
		[DataMember(Name = "collection")]
		public string Collection { get; set; }

		/// <summary>
		///     TODO: What is this?
		/// </summary>
		[DataMember(Name = "context")]
		public string Context { get; set; }

		/// <summary>
		///     The link, usually for attachments, to download the content
		/// </summary>
		[DataMember(Name = "download")]
		public string Download { get; set; }

		/// <summary>
		///     A link to the entity itself (so one can find it again)
		/// </summary>
		[DataMember(Name = "self")]
		public Uri Self { get; set; }

		/// <summary>
		///     A short link to the content, relative to the hostname (and port)
		/// </summary>
		[DataMember(Name = "tinyui")]
		public string TinyUi { get; set; }

		/// <summary>
		///     A normal, but well readable, link to the content
		/// </summary>
		[DataMember(Name = "webui")]
		public string WebUi { get; set; }
	}
}