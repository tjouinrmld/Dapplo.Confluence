// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using Newtonsoft.Json;

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     For instance when using delete
    /// </summary>
    [JsonObject]
    public class LongRunningTask
    {
        /// <summary>
        ///     Id of the task
        /// </summary>
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Id { get; set; }

        /// <summary>
        ///     In this the link for the status is returned
        /// </summary>
        [JsonProperty("links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Links Links { get; set; }
    }
}