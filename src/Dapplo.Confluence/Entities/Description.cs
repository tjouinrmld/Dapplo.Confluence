// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     Description information
    ///     See: https://docs.atlassian.com/confluence/REST/latest
    /// </summary>
    [JsonObject]
    public class Description
    {
        /// <summary>
        ///     Plain text
        /// </summary>
        [JsonProperty("plain", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Plain Plain { get; set; }
    }
}