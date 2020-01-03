//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016-2020 Dapplo
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
    ///     Child information
    ///     See: https://docs.atlassian.com/confluence/REST/latest
    /// </summary>
    [JsonObject]
    public class Children
    {
        /// <summary>
        ///     The values that are expandable
        /// </summary>
        [JsonProperty("_expandable")]
        public IDictionary<string, string> Expandables { get; set; }

        /// <summary>
        ///     Different links for this entity, depending on the entry
        /// </summary>
        [JsonProperty("_links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Links Links { get; set; }

        /// <summary>
        ///     Results with pages
        /// </summary>
        [JsonProperty("page", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Result<Content> Result { get; set; }
    }
}