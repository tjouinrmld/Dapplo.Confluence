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

using System;
using System.Runtime.Serialization;

#endregion

namespace Dapplo.Confluence.Entities
{
	/// <summary>
	///     Version information
	///     See: https://docs.atlassian.com/confluence/REST/latest
	/// </summary>
	[DataContract]
	public class Version
	{
		/// <summary>
		/// Who made this version
		/// </summary>
		[DataMember(Name = "by")]
		public User By { get; set; }

		/// <summary>
		/// Is it a small version change
		/// </summary>
		[DataMember(Name = "minorEdit")]
		public bool IsMinorEdit { get; set; }

		/// <summary>
		/// A message for the version
		/// </summary>
		[DataMember(Name = "message")]
		public string Message { get; set; }

		/// <summary>
		/// Version number
		/// </summary>
		[DataMember(Name = "number")]
		public int Number { get; set; }

		/// <summary>
		/// When was this version
		/// </summary>
		[DataMember(Name = "when")]
		public DateTimeOffset When { get; set; }
	}
}