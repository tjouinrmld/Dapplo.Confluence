// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using System;
using Newtonsoft.Json;

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     Version information
    ///     See: https://docs.atlassian.com/confluence/REST/latest
    /// </summary>
    [JsonObject]
    public class Version
    {
        /// <summary>
        ///     Who made this version
        /// </summary>
        [JsonProperty("by", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public User By { get; set; }

        /// <summary>
        ///     Is it a small version change
        /// </summary>
        [JsonProperty("minorEdit", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsMinorEdit { get; set; }

        /// <summary>
        ///     A message for the version
        /// </summary>
        [JsonProperty("message", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        ///     Version number
        /// </summary>
        [JsonProperty("number", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Number { get; set; }

        /// <summary>
        ///     When was this version
        /// </summary>
        [JsonProperty("when", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTimeOffset When { get; set; }
    }
}