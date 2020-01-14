// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     History information
    ///     See: https://docs.atlassian.com/confluence/REST/latest
    /// </summary>
    [JsonObject]
    public class History
    {
        /// <summary>
        ///     User who created it
        /// </summary>
        [JsonProperty("createdBy", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public User CreatedBy { get; set; }

        /// <summary>
        ///     Created data
        /// </summary>
        [JsonProperty("createdDate", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTimeOffset CreatedDate { get; set; }

        /// <summary>
        ///     The values that are expandable
        /// </summary>
        [JsonProperty("_expandable", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IDictionary<string, string> Expandables { get; set; }

        /// <summary>
        ///     Last updated information
        /// </summary>
        [JsonProperty("lastUpdated", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public LastUpdated LastUpdated { get; set; }

        /// <summary>
        ///     Is this history entity the latest entry?
        /// </summary>
        [JsonProperty("latest", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Latest { get; set; }

        /// <summary>
        ///     Different links for this entity, depending on the entry
        /// </summary>
        [JsonProperty("_links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Links Links { get; set; }
    }
}