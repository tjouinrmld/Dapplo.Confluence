// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using Newtonsoft.Json;

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     Error information
    /// </summary>
    [JsonObject]
    public class Error
    {
        /// <summary>
        ///     Error message from Confluence
        /// </summary>
        [JsonProperty("message", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        ///     Confluence status code
        /// </summary>
        [JsonProperty("statusCode", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int StatusCode { get; set; }
    }
}