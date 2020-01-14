// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using Newtonsoft.Json;

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     Contents of a space
    /// </summary>
    [JsonObject]
    public class SpaceContents
    {
        /// <summary>
        ///     The pages
        /// </summary>
        [JsonProperty("page", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Result<Content> Pages { get; set; }

        /// <summary>
        ///     The blogposts
        /// </summary>
        [JsonProperty("blogpost", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Result<Content> Blogposts { get; set; }

        /// <summary>
        /// The links
        /// </summary>
        [JsonProperty("_links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Links Links { get; set; }
    }
}