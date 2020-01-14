// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using Newtonsoft.Json;

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     Info on a label
    ///     See: https://docs.atlassian.com/confluence/REST/latest
    /// </summary>
    [JsonObject]
    public class Label
    {
        /// <summary>
        ///     Prefix of the label
        /// </summary>
        [JsonProperty("prefix", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Prefix { get; set; } = "global";

        /// <summary>
        ///     Name of the label
        /// </summary>
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        ///     Id of the label
        /// </summary>
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long Id { get; set; }
    }
}