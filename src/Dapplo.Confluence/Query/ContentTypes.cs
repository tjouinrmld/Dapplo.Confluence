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

using System.Runtime.Serialization;

namespace Dapplo.Confluence.Query
{
    /// <summary>
    /// Possible types of content, used in CQL
    /// </summary>
	public enum ContentTypes
	{
		/// <summary>
		/// The content is global content
		/// </summary>
		[EnumMember(Value = "global")] Global,
        /// <summary>
		/// The content is a page
		/// </summary>
		[EnumMember(Value = "page")] Page,
        /// <summary>
        /// The content is a blogpost
        /// </summary>
		[EnumMember(Value = "blogpost")] BlogPost,
        /// <summary>
        /// The content is a comment
        /// </summary>
		[EnumMember(Value = "comment")] Comment,
        /// <summary>
        /// The content is an attachment
        /// </summary>
		[EnumMember(Value = "attachment")] Attachment,
	    /// <summary>
	    /// The content is a personal page
	    /// </summary>
	    [EnumMember(Value = "personal")] Personal
    }
}