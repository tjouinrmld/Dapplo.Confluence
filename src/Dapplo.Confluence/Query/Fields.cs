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

using System.Runtime.Serialization;

#endregion

namespace Dapplo.Confluence.Query
{
    /// <summary>
    /// Fields for the CQL clauses
    /// </summary>
	public enum Fields
	{
        /// <summary>
        /// Ancestor field
        /// </summary>
		[EnumMember(Value = "ancestor")] Ancestor,
        /// <summary>
        /// Container field
        /// </summary>
		[EnumMember(Value = "container")] Container,
        /// <summary>
        /// Content field
        /// </summary>
		[EnumMember(Value = "content")] Content,
        /// <summary>
        /// Created field
        /// </summary>
		[EnumMember(Value = "created")] Created,
        /// <summary>
        /// Creator field
        /// </summary>
		[EnumMember(Value = "creator")] Creator,
        /// <summary>
        /// Contributor field
        /// </summary>
        [EnumMember(Value = "contributor")] Contributor,
        /// <summary>
        /// Favourite field
        /// </summary>
        [EnumMember(Value = "favourite")] Favourite,
        /// <summary>
        /// Id field
        /// </summary>
        [EnumMember(Value = "id")] Id,
        /// <summary>
        /// Label field
        /// </summary>
        [EnumMember(Value = "label")] Label,
        /// <summary>
        /// LastModified field
        /// </summary>
        [EnumMember(Value = "lastModified")] LastModified,
        /// <summary>
        /// Macro field
        /// </summary>
        [EnumMember(Value = "macro")] Macro,
        /// <summary>
        /// Mention field
        /// </summary>
        [EnumMember(Value = "mention")] Mention,
        /// <summary>
        /// Parent field
        /// </summary>
        [EnumMember(Value = "parent")] Parent,
        /// <summary>
        /// Space field
        /// </summary>
        [EnumMember(Value = "space")] Space,
        /// <summary>
        /// Text field
        /// </summary>
        [EnumMember(Value = "text")] Text,
        /// <summary>
        /// Title field
        /// </summary>
        [EnumMember(Value = "title")] Title,
        /// <summary>
        /// Type field
        /// </summary>
        [EnumMember(Value = "type")] Type,
        /// <summary>
        /// Watcher field
        /// </summary>
        [EnumMember(Value = "watcher")] Watcher
	}
}