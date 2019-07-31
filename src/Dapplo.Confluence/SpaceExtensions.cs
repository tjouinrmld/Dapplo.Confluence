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

#region using

using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.Confluence.Entities;
using Dapplo.HttpExtensions;
using Dapplo.Confluence.Internals;
using Enumerable = System.Linq.Enumerable;

#endregion

namespace Dapplo.Confluence
{
    /// <summary>
    ///     Marker interface for the space related methods
    /// </summary>
    public interface ISpaceDomain : IConfluenceDomain
    {
    }

    /// <summary>
    ///     All space related extension methods
    /// </summary>
    public static class SpaceExtensions
    {
        /// <summary>
        ///     Create a space
        /// </summary>
        /// <param name="confluenceClient">ISpaceDomain to bind the extension method to</param>
        /// <param name="key">Key for the space</param>
        /// <param name="name">Name for the space</param>
        /// <param name="description">Description for the space</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>created Space</returns>
        public static async Task<Space> CreateAsync(this ISpaceDomain confluenceClient, string key, string name, string description, CancellationToken cancellationToken = default)
        {
            confluenceClient.Behaviour.MakeCurrent();
            var space = new Space
            {
                Key = key,
                Name = name,
                Description = new Description
                {
                    Plain = new Plain
                    {
                        Value = description
                    }
                }
            };
            var spaceUri = confluenceClient.ConfluenceApiUri.AppendSegments("space");
            var response = await spaceUri.PostAsync<HttpResponse<Space, Error>>(space, cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Create a private space
        /// </summary>
        /// <param name="confluenceClient">ISpaceDomain to bind the extension method to</param>
        /// <param name="key">Key for the space</param>
        /// <param name="name">Name for the space</param>
        /// <param name="description">Description for the space</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>created Space</returns>
        public static async Task<Space> CreatePrivateAsync(this ISpaceDomain confluenceClient, string key, string name, string description, CancellationToken cancellationToken = default)
        {
            confluenceClient.Behaviour.MakeCurrent();
            var space = new Space
            {
                Key = key,
                Name = name,
                Description = new Description
                {
                    Plain = new Plain
                    {
                        Value = description
                    }
                }
            };
            var spaceUri = confluenceClient.ConfluenceApiUri.AppendSegments("space", "_private");
            var response = await spaceUri.PostAsync<HttpResponse<Space, Error>>(space, cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Delete a space
        /// </summary>
        /// <param name="confluenceClient">ISpaceDomain to bind the extension method to</param>
        /// <param name="key">Key for the space</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Long running task, which takes care of deleting the space</returns>
        public static async Task<LongRunningTask> DeleteAsync(this ISpaceDomain confluenceClient, string key, CancellationToken cancellationToken = default)
        {
            confluenceClient.Behaviour.MakeCurrent();
            var spaceUri = confluenceClient.ConfluenceApiUri.AppendSegments("space", key);
            var response = await spaceUri.DeleteAsync<HttpResponse<LongRunningTask>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors(HttpStatusCode.Accepted);
        }

        /// <summary>
        ///     Get Spaces with all the defaults, see <a href="https://docs.atlassian.com/confluence/REST/latest/#d3e164">here</a>
        /// </summary>
        /// <param name="confluenceClient">ISpaceDomain to bind the extension method to</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>List of Spaces</returns>
        public static Task<IList<Space>> GetAllAsync(this ISpaceDomain confluenceClient, CancellationToken cancellationToken = default)
        {
            return confluenceClient.GetAllWithParametersAsync(cancellationToken: cancellationToken);
        }

        /// <summary>
        ///     Get Spaces with specific parameters, see <a href="https://docs.atlassian.com/confluence/REST/latest/#d3e164">here</a>
        /// </summary>
        /// <param name="confluenceClient">ISpaceDomain to bind the extension method to</param>
        /// <param name="spaceKeys">IEnumerable of string with space keys</param>
        /// <param name="type">string filter the list of spaces returned by type (global, personal)</param>
        /// <param name="status">string filter the list of spaces returned by status (current, archived)</param>
        /// <param name="label">string filter the list of spaces returned by label</param>
        /// <param name="favourite">bool filter the list of spaces returned by favourites</param>
        /// <param name="start">the start point of the collection to return</param>
        /// <param name="limit">The maximum number of spaces to return per page. System default is 25, override this with a value. Note, this may be restricted by fixed system limits.</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>List of Spaces</returns>
        public static async Task<IList<Space>> GetAllWithParametersAsync(this ISpaceDomain confluenceClient, IEnumerable<string> spaceKeys = null, string type = null, string status = null, string label = null, bool? favourite = null, int? start = null, int? limit = null, CancellationToken cancellationToken = default)
        {
            confluenceClient.Behaviour.MakeCurrent();
            var spacesUri = confluenceClient.ConfluenceApiUri.AppendSegments("space");

            foreach (var spaceKey in spaceKeys ?? Enumerable.Empty<string>())
            {
                spacesUri = spacesUri.ExtendQuery("spaceKey", spaceKey);
            }

            if (!string.IsNullOrEmpty(type))
            {
                spacesUri = spacesUri.ExtendQuery("type", type);
            }
            if (!string.IsNullOrEmpty(status))
            {
                spacesUri = spacesUri.ExtendQuery("status", status);
            }
            if (!string.IsNullOrEmpty(label))
            {
                spacesUri = spacesUri.ExtendQuery("label", label);
            }
            if (favourite.HasValue)
            {
                spacesUri = spacesUri.ExtendQuery("favourite", favourite);
            }
            var expand = string.Join(",", ConfluenceClientConfig.ExpandGetSpace ?? Enumerable.Empty<string>());
            if (!string.IsNullOrEmpty(expand))
            {
                spacesUri = spacesUri.ExtendQuery("expand", expand);
            }
            if (start.HasValue)
            {
                spacesUri = spacesUri.ExtendQuery("start", start.Value);
            }
            if (limit.HasValue)
            {
                spacesUri = spacesUri.ExtendQuery("limit", limit.Value);
            }

            var response = await spacesUri.GetAsAsync<HttpResponse<Result<Space>, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors()?.Results;
        }

        /// <summary>
        ///     Get Space information see <a href="https://docs.atlassian.com/confluence/REST/latest/#d3e164">here</a>
        /// </summary>
        /// <param name="confluenceClient">ISpaceDomain to bind the extension method to</param>
        /// <param name="spaceKey">the space key</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Space</returns>
        public static async Task<Space> GetAsync(this ISpaceDomain confluenceClient, string spaceKey, CancellationToken cancellationToken = default)
        {
            confluenceClient.Behaviour.MakeCurrent();
            var spaceUri = confluenceClient.ConfluenceApiUri.AppendSegments("space", spaceKey);

            var expand = string.Join(",", ConfluenceClientConfig.ExpandGetSpace ?? Enumerable.Empty<string>());
            if (!string.IsNullOrEmpty(expand))
            {
                spaceUri = spaceUri.ExtendQuery("expand", expand);
            }

            var response = await spaceUri.GetAsAsync<HttpResponse<Space, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Get Contents in a space, see <a href="https://developer.atlassian.com/cloud/confluence/rest/?_ga=2.79668370.74649793.1513115044-949371064.1513115044#api-space-spaceKey-content-get">here</a>
        /// </summary>
        /// <param name="confluenceClient">IContentDomain to bind the extension method to</param>
        /// <param name="space">space key to get the content for</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>List with Content</returns>
        public static async Task<SpaceContents> GetContentsAsync(this ISpaceDomain confluenceClient, string space, CancellationToken cancellationToken = default)
        {
            var contentUri = confluenceClient.ConfluenceApiUri.AppendSegments("space", space, "content");

            var expand = string.Join(",", ConfluenceClientConfig.ExpandSpaceGetContents ?? Enumerable.Empty<string>());
            if (!string.IsNullOrEmpty(expand))
            {
                contentUri = contentUri.ExtendQuery("expand", expand);
            }

            confluenceClient.Behaviour.MakeCurrent();

            var response = await contentUri.GetAsAsync<HttpResponse<SpaceContents, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Update a space
        /// </summary>
        /// <param name="confluenceClient">ISpaceDomain to bind the extension method to</param>
        /// <param name="space">Space to update</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>created Space</returns>
        public static async Task<Space> UpdateAsync(this ISpaceDomain confluenceClient, Space space, CancellationToken cancellationToken = default)
        {
            confluenceClient.Behaviour.MakeCurrent();
            var spaceUri = confluenceClient.ConfluenceApiUri.AppendSegments("space", space.Key);
            var response = await spaceUri.PutAsync<HttpResponse<Space, string>>(space, cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }
    }
}