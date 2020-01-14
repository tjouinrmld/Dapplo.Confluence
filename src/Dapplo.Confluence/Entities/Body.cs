// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using Newtonsoft.Json;

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     Body
    ///     See: https://docs.atlassian.com/confluence/REST/latest
    /// </summary>
    [JsonObject]
    public class Body
    {
        /// <summary>
        ///     Storage for content, used when creating
        /// </summary>
        [JsonProperty("storage", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public BodyContent Storage { get; set; }

        /// <summary>
        ///     View for Body
        /// </summary>
        [JsonProperty("view", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public BodyContent View { get; set; }
    }
}