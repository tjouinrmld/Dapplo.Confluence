// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using Newtonsoft.Json;

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     User information
    ///     See: https://docs.atlassian.com/confluence/REST/latest
    /// </summary>
    [JsonObject]
    public class User
    {
        /// <summary>
        ///     The name which is displayed in the UI, usually "firstname lastname"
        /// </summary>
        [JsonProperty("displayName", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string DisplayName { get; set; }

        /// <summary>
        ///     Information on the profile picture
        /// </summary>
        [JsonProperty("profilePicture", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Picture ProfilePicture { get; set; }

        /// <summary>
        ///     Type of user
        /// </summary>
        [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Type { get; set; }

        /// <summary>
        ///     A unique key for the user
        /// </summary>
        [JsonProperty("userKey", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string UserKey { get; set; }

        /// <summary>
        ///     The username
        /// </summary>
        [JsonProperty("username", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Username { get; set; }
    }
}