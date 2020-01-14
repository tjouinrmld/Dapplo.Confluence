// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     Child information
    ///     See: https://docs.atlassian.com/confluence/REST/latest
    /// </summary>
    [JsonObject]
    public class Children
    {
        /// <summary>
        ///     The values that are expandable
        /// </summary>
        [JsonProperty("_expandable")]
        public IDictionary<string, string> Expandables { get; set; }

        /// <summary>
        ///     Different links for this entity, depending on the entry
        /// </summary>
        [JsonProperty("_links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Links Links { get; set; }

        /// <summary>
        ///     Results with pages
        /// </summary>
        [JsonProperty("page", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Result<Content> Result { get; set; }
    }
}