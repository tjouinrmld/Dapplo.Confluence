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

using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     Content, this can be many things e.g a page or an attachment
    ///     See: https://docs.atlassian.com/confluence/REST/latest
    /// </summary>
    [DataContract]
    public class Content : BaseEntity<long>
    {
	    /// <summary>
	    ///     The container where this content hangs, this is not filled unless expand=container
	    /// </summary>
	    [DataMember(Name = "container", EmitDefaultValue = false)]
	    public Content Container { get; set; }

	    /// <summary>
	    ///     Additional meta-data for the attachment, like the comment
	    /// </summary>
	    [DataMember(Name = "metadata", EmitDefaultValue = false)]
	    public Metadata Metadata { get; set; }

		/// <summary>
		///     Body of the content
		/// </summary>
		[DataMember(Name = "body", EmitDefaultValue = false)]
	    public Body Body { get; set; }

		/// <summary>
		///     Ancestors for the content
		/// </summary>
		[DataMember(Name = "ancestors", EmitDefaultValue = false)]
	    public IList<Content> Ancestors { get; set; }

		/// <summary>
		///     The values that are expandable
		/// </summary>
		[DataMember(Name = "_expandable", EmitDefaultValue = false)]
        public IDictionary<string, string> Expandables { get; set; }

        /// <summary>
        ///     History information for the content, this is not filled unless expand=history
        /// </summary>
        [DataMember(Name = "history", EmitDefaultValue = false)]
        public History History { get; set; }

        /// <summary>
        ///     The space where this content is
        /// </summary>
        [DataMember(Name = "space", EmitDefaultValue = false)]
        public Space Space { get; set; }

        /// <summary>
        ///     The title of the content
        /// </summary>
        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title { get; set; }

        /// <summary>
        ///     Version information for the content, this is not filled unless expand=version
        /// </summary>
        [DataMember(Name = "version", EmitDefaultValue = false)]
        public Version Version { get; set; }
    }
}