// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using Newtonsoft.Json;

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     Plain information, used in the description.
    ///     TODO: Find a better name
    ///     See: https://docs.atlassian.com/confluence/REST/latest
    /// </summary>
    [JsonObject]
    public class Plain
    {
        /// <summary>
        ///     Type of representation
        /// </summary>
        [JsonProperty("representation", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Representation { get; set; } = "plain";

        /// <summary>
        ///     Value of the plain description
        /// </summary>
        [JsonProperty("value", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Value { get; set; }
    }
}