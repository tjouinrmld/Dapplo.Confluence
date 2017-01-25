//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016 Dapplo
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

using System.Runtime.Serialization;

#endregion

namespace Dapplo.Confluence.Query
{
	public enum Fields
	{
		[EnumMember(Value = "ancestor")] Ancestor,
		[EnumMember(Value = "container")] Container,
		[EnumMember(Value = "content")] Content,
		[EnumMember(Value = "created")] Created,
		[EnumMember(Value = "creator")] Creator,
		[EnumMember(Value = "contributor")] Contributor,
		[EnumMember(Value = "favourite")] Favourite,
		[EnumMember(Value = "id")] Id,
		[EnumMember(Value = "label")] Label,
		[EnumMember(Value = "lastModified")] LastModified,
		[EnumMember(Value = "macro")] Macro,
		[EnumMember(Value = "mention")] Mention,
		[EnumMember(Value = "parent")] Parent,
		[EnumMember(Value = "space")] Space,
		[EnumMember(Value = "text")] Text,
		[EnumMember(Value = "title")] Title,
		[EnumMember(Value = "type")] Type,
		[EnumMember(Value = "watcher")] Watcher
	}
}