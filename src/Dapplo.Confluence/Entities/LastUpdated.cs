// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     LastUpdated information
    ///     See: https://docs.atlassian.com/confluence/REST/latest
    /// </summary>
    [JsonObject]
    public class LastUpdated
    {
        /// <summary>
        ///     User who updated
        /// </summary>
        [JsonProperty("by", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public User By { get; set; }

        /// <summary>
        ///     The values that are expandable
        /// </summary>
        [JsonProperty("_expandable", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IDictionary<string, string> Expandables { get; set; }

        /// <summary>
        ///     Friendly representation for When
        /// </summary>
        [JsonProperty("friendlyWhen", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FriendlyWhen { get; set; }

        /// <summary>
        ///     Different links for this entity, depending on the entry
        /// </summary>
        [JsonProperty("_links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Links Links { get; set; }

        /// <summary>
        ///     When the last update was
        /// </summary>
        [JsonProperty("when", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTimeOffset When { get; set; }
    }
}