// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using Newtonsoft.Json;

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     Content for the body
    /// </summary>
    [JsonObject]
    public class BodyContent
    {
        /// <summary>
        ///     Representation
        /// </summary>
        [JsonProperty("representation", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Representation { get; set; }

        /// <summary>
        ///     Value of the view
        /// </summary>
        [JsonProperty("value", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Value { get; set; }
    }
}