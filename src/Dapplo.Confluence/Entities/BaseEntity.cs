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

using System;
using System.Runtime.Serialization;
using Dapplo.Confluence.Query;

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     Base entity, defines things that are in every entity.
    ///     Content, Space, Attachments are all entities.
    /// </summary>
    public class BaseEntity<TId>
    {
		/// <summary>
		/// Used internally to convert the string with the id to a typed value
		/// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        protected string InternalId {
	        get => ContentTypes.Attachment == Type ? $"att{Id}" : Id.ToString();
	        set
	        {
		        if (value == null)
		        {
			        Id = default;
			        return;
		        }
				Id = (TId)Convert.ChangeType(value.Replace("att", ""), typeof(TId));
			}
        }

	    /// <summary>
	    ///     Unique ID for this entity
	    /// </summary>
		public TId Id { get; set; }

		/// <summary>
		///     Different links for this entity, depending on the entity
		/// </summary>
		[DataMember(Name = "_links", EmitDefaultValue = false)]
        public Links Links { get; set; }

        /// <summary>
        ///     Type of the entity
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public ContentTypes Type { get; set; }

        /// <summary>
        ///     Implicit cast of BaseEntity to the ID type (e.g. Content -> long for contentId)
        /// </summary>
        /// <param name="entity">BaseEntity of type TId</param>
        /// <returns>TId</returns>
        public static implicit operator TId(BaseEntity<TId> entity)
        {
            return entity.Id;
        }
    }
}
