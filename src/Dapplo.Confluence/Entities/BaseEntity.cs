// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dapplo.Confluence.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        protected string InternalId
        {
            get
            {
                if (Equals(default(TId), Id))
                {
                    return null;
                }
                var returnValue = Id.ToString();
                if ("0".Equals(returnValue))
                {
                    return null;
                }
                return ContentTypes.Attachment == Type ? $"att{returnValue}" : returnValue;
            }
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
        [JsonIgnore]
        public TId Id { get; set; }

        /// <summary>
        ///     Different links for this entity, depending on the entity
        /// </summary>
        [JsonProperty("_links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Links Links { get; set; }

        /// <summary>
        ///     Type of the entity
        /// </summary>
        [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
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
