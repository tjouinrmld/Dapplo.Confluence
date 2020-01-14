// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using System.Runtime.Serialization;

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