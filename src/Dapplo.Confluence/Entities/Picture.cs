// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using Newtonsoft.Json;

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     Space information
    ///     See: https://docs.atlassian.com/confluence/REST/latest
    /// </summary>
    [JsonObject]
    public class Picture
    {
        /// <summary>
        ///     Height of the picture
        /// </summary>
        [JsonProperty("height", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Height { get; set; }

        /// <summary>
        ///     Is this picture the default
        /// </summary>
        [JsonProperty("isDefault", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsDefault { get; set; }

        /// <summary>
        ///     The path for the picture relative to the hostname (and port) of the server, this is outside the Rest API path
        /// </summary>
        [JsonProperty("path", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Path { get; set; }

        /// <summary>
        ///     Width of the picture
        /// </summary>
        [JsonProperty("width", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Width { get; set; }
    }
}