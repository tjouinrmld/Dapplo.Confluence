//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016-2019 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.Confluence
// 
//  Dapplo.Confluence is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.Confluence is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Confluence. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     Space information
    ///     See: https://docs.atlassian.com/confluence/REST/latest
    ///     Should be called with expand=icon,description.plain,homepage
    /// </summary>
    [JsonObject]
    public class Space : BaseEntity<long>
    {
        /// <summary>
        ///     Description
        /// </summary>
        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Description Description { get; set; }

        /// <summary>
        ///     The values that are expandable
        /// </summary>
        [JsonProperty("_expandable")]
        public IDictionary<string, string> Expandables { get; set; }

        /// <summary>
        ///     Icon for the space
        /// </summary>
        [JsonProperty("icon", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Picture Icon { get; set; }

        /// <summary>
        ///     Test if this space is a personal space, this is true when the Key starts with a ~
        /// </summary>
        [JsonIgnore]
        public bool IsPersonal => true == Key?.StartsWith("~");

        /// <summary>
        ///     Key for the space
        /// </summary>
        [JsonProperty("key", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Key { get; set; }

        /// <summary>
        ///     The name of the space
        /// </summary>
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; set; }
    }
}