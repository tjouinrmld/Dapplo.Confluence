//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016 Dapplo
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

using System;
using System.Net;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.Confluence.Entities;
using Dapplo.Confluence.Query;
using Dapplo.HttpExtensions;

#endregion

namespace Dapplo.Confluence
{
    /// <summary>
    ///     The is the marker interface to the content functionality of the Confluence API
    /// </summary>
    public interface IContentDomain : IConfluenceDomain
    {
    }

    /// <summary>
    ///     The is the implementation to the content functionality of the Confluence API
    /// </summary>
    public static class ContentExtensions
    {
        /// <summary>
        ///     Create content
        /// </summary>
        /// <param name="confluenceClient">IContentDomain to bind the extension method to</param>
        /// <param name="type">Type of content, usually page</param>
        /// <param name="title">Title for the content</param>
        /// <param name="spaceKey">Key of the space to add the content to</param>
        /// <param name="body">the complete body (HTML)</param>
        /// <param name="ancestorId">Optional ID for the ancestor (parent)</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Content</returns>
        public static Task<Content> CreateAsync(this IContentDomain confluenceClient, string type, string title, string spaceKey, string body, long? ancestorId = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var contentBody = new Body
            {
                Storage = new BodyContent
                {
                    Value = body,
                    Representation = "storage"
                }
            };
            return confluenceClient.CreateAsync(type, title, spaceKey, contentBody, ancestorId, cancellationToken);
        }

        /// <summary>
        ///     Create content
        /// </summary>
        /// <param name="confluenceClient">IContentDomain to bind the extension method to</param>
        /// <param name="type">Type of content, usually page</param>
        /// <param name="title">Title for the content</param>
        /// <param name="spaceKey">Key of the space to add the content to</param>
        /// <param name="body">Body</param>
        /// <param name="ancestorId">Optional ID for the ancestor (parent)</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Content</returns>
        public static Task<Content> CreateAsync(this IContentDomain confluenceClient, string type, string title, string spaceKey, Body body, long? ancestorId = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var content = new Content
            {
                Type = type,
                Title = title,
                Space = new Space
                {
                    Key = spaceKey
                },
                Body = body,
                Ancestors = !ancestorId.HasValue ? null : new List<Content>
                {
                    new Content
                    {
                        Id = ancestorId.Value
                    }
                }
            };
            return confluenceClient.CreateAsync(content, cancellationToken);
        }

        /// <summary>
        ///     Create content
        /// </summary>
        /// <param name="confluenceClient">IContentDomain to bind the extension method to</param>
        /// <param name="content">Content (e.g. Page) to create</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Content</returns>
        public static async Task<Content> CreateAsync(this IContentDomain confluenceClient, Content content, CancellationToken cancellationToken = default(CancellationToken))
        {
            var contentUri = confluenceClient.ConfluenceApiUri.AppendSegments("content");

            confluenceClient.Behaviour.MakeCurrent();
            var response = await contentUri.PostAsync<HttpResponse<Content, Error>>(content, cancellationToken).ConfigureAwait(false);

            if (response.HasError)
            {
                throw new Exception(response.ErrorResponse.Message);
            }
            return response.Response;
        }

        /// <summary>
        ///     Delete content (attachments are also content)
        /// </summary>
        /// <param name="confluenceClient">IContentDomain to bind the extension method to</param>
        /// <param name="contentId">ID for the content which needs to be deleted</param>
        /// <param name="isTrashed">
        ///     If the content is trashable, you will need to call DeleteAsyc twice, second time with isTrashed
        ///     = true
        /// </param>
        /// <param name="cancellationToken">CancellationToken</param>
        public static async Task DeleteAsync(this IContentDomain confluenceClient, long contentId, bool isTrashed = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            var contentUri = confluenceClient.ConfluenceApiUri.AppendSegments("content", contentId);

            if (isTrashed)
            {
                contentUri = contentUri.ExtendQuery("status", "trashed");
            }
            confluenceClient.Behaviour.MakeCurrent();

            var response = await contentUri.DeleteAsync<HttpResponse>(cancellationToken).ConfigureAwait(false);
            response.HandleStatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        ///     Get Content information see <a href="https://docs.atlassian.com/confluence/REST/latest/#d3e164">here</a>
        /// </summary>
        /// <param name="confluenceClient">IContentDomain to bind the extension method to</param>
        /// <param name="contentId">content id</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Content</returns>
        public static async Task<Content> GetAsync(this IContentDomain confluenceClient, long contentId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var contentUri = confluenceClient.ConfluenceApiUri.AppendSegments("content", contentId);

            if (ConfluenceClientConfig.ExpandGetContent != null && ConfluenceClientConfig.ExpandGetContent.Length != 0)
            {
                contentUri = contentUri.ExtendQuery("expand", string.Join(",", ConfluenceClientConfig.ExpandGetContent));
            }

            confluenceClient.Behaviour.MakeCurrent();

            var response = await contentUri.GetAsAsync<HttpResponse<Content, Error>>(cancellationToken).ConfigureAwait(false);

            if (response.HasError)
            {
                throw new Exception(response.ErrorResponse.Message);
            }
            return response.Response;
        }

        /// <summary>
        ///     Get content by title
        ///     See: https://docs.atlassian.com/confluence/REST/latest/#d2e4539
        /// </summary>
        /// <param name="confluenceClient">IContentDomain to bind the extension method to</param>
        /// <param name="spaceKey">Space key</param>
        /// <param name="title">Title of the content</param>
        /// <param name="start">Start of the results, used for paging</param>
        /// <param name="limit">Maximum number of results returned, default is 20</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Results with content items</returns>
        public static async Task<Result<Content>> GetByTitleAsync(this IContentDomain confluenceClient, string spaceKey, string title, int start = 0, int limit = 20, CancellationToken cancellationToken = default(CancellationToken))
        {
            confluenceClient.Behaviour.MakeCurrent();

            var searchUri = confluenceClient.ConfluenceApiUri.AppendSegments("content").ExtendQuery(new Dictionary<string, object>
            {
                {
                    "start", start
                },
                {
                    "limit", limit
                },
                {
                    "type", "page"
                },
                {
                    "spaceKey", spaceKey
                },
                {
                    "title", title
                }
            });

            if (ConfluenceClientConfig.ExpandGetContentByTitle != null && ConfluenceClientConfig.ExpandGetContentByTitle.Length != 0)
            {
                searchUri = searchUri.ExtendQuery("expand", string.Join(",", ConfluenceClientConfig.ExpandGetContentByTitle));
            }

            var response = await searchUri.GetAsAsync<HttpResponse<Result<Content>, Error>>(cancellationToken).ConfigureAwait(false);

            if (response.HasError)
            {
                throw new Exception(response.ErrorResponse.Message);
            }
            return response.Response;
        }

        /// <summary>
        ///     Get Content information see <a href="https://docs.atlassian.com/confluence/REST/latest/#d3e164">here</a>
        /// </summary>
        /// <param name="confluenceClient">IContentDomain to bind the extension method to</param>
        /// <param name="contentId">content id</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>List with Content</returns>
        public static async Task<IList<Content>> GetChildrenAsync(this IContentDomain confluenceClient, long contentId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var contentUri = confluenceClient.ConfluenceApiUri.AppendSegments("content", contentId, "child");

            if (ConfluenceClientConfig.ExpandGetChildren != null && ConfluenceClientConfig.ExpandGetChildren.Length != 0)
            {
                contentUri = contentUri.ExtendQuery("expand", string.Join(",", ConfluenceClientConfig.ExpandGetChildren));
            }
            confluenceClient.Behaviour.MakeCurrent();

            var response = await contentUri.GetAsAsync<HttpResponse<Children, Error>>(cancellationToken).ConfigureAwait(false);

            if (response.HasError)
            {
                throw new Exception(response.ErrorResponse.Message);
            }
            return response.Response.Result.Results;
        }

        /// <summary>
        ///     Get Content History information see <a href="https://docs.atlassian.com/confluence/REST/latest/#d3e164">here</a>
        /// </summary>
        /// <param name="confluenceClient">IContentDomain to bind the extension method to</param>
        /// <param name="contentId">content id</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Content</returns>
        public static async Task<History> GetHistoryAsync(this IContentDomain confluenceClient, long contentId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var historyUri = confluenceClient.ConfluenceApiUri.AppendSegments("content", contentId, "history");

            confluenceClient.Behaviour.MakeCurrent();

            var response = await historyUri.GetAsAsync<HttpResponse<History, Error>>(cancellationToken).ConfigureAwait(false);

            if (response.HasError)
            {
                throw new Exception(response.ErrorResponse.Message);
            }
            return response.Response;
        }

        /// <summary>
        ///     Possible since 5.7
        ///     Search for issues, with a CQL (e.g. from a filter) see
        ///     <a href="https://docs.atlassian.com/confluence/REST/latest/#d2e4539">here</a>
        /// </summary>
        /// <param name="confluenceClient">IContentDomain to bind the extension method to</param>
        /// <param name="cqlClause">Confluence Query Language clause, created via Where</param>
        /// <param name="cqlContext">
        ///     the execution context for CQL functions, provides current space key and content id. If this is
        ///     not provided some CQL functions will not be available.
        /// </param>
        /// <param name="limit">Maximum number of results returned, default is 20</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Result with content items</returns>
        public static Task<Result<Content>> SearchAsync(this IContentDomain confluenceClient, IFinalClause cqlClause, string cqlContext = null, int limit = 20, CancellationToken cancellationToken = default(CancellationToken))
        {
            return confluenceClient.SearchAsync(cqlClause.ToString(), cqlContext, limit, cancellationToken);
        }

        /// <summary>
        ///     Possible since 5.7
        ///     Search for issues, with a CQL (e.g. from a filter) see
        ///     <a href="https://docs.atlassian.com/confluence/REST/latest/#d2e4539">here</a>
        /// </summary>
        /// <param name="confluenceClient">IContentDomain to bind the extension method to</param>
        /// <param name="cql">Confluence Query Language, like SQL, for the search</param>
        /// <param name="cqlContext">
        ///     the execution context for CQL functions, provides current space key and content id. If this is
        ///     not provided some CQL functions will not be available.
        /// </param>
        /// <param name="limit">Maximum number of results returned, default is 20</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Result with content items</returns>
        public static async Task<Result<Content>> SearchAsync(this IContentDomain confluenceClient, string cql, string cqlContext = null, int limit = 20, CancellationToken cancellationToken = default(CancellationToken))
        {
            confluenceClient.Behaviour.MakeCurrent();

            var searchUri = confluenceClient.ConfluenceApiUri.AppendSegments("content", "search").ExtendQuery("cql", cql).ExtendQuery("limit", limit);

            if (ConfluenceClientConfig.ExpandSearch != null && ConfluenceClientConfig.ExpandSearch.Length != 0)
            {
                searchUri = searchUri.ExtendQuery("expand", string.Join(",", ConfluenceClientConfig.ExpandSearch));
            }
            if (cqlContext != null)
            {
                searchUri = searchUri.ExtendQuery("cqlcontext", cqlContext);
            }

            var response = await searchUri.GetAsAsync<HttpResponse<Result<Content>, Error>>(cancellationToken).ConfigureAwait(false);

            if (response.HasError)
            {
                throw new Exception(response.ErrorResponse.Message);
            }
            return response.Response;
        }

        /// <summary>
        ///     Update content
        /// </summary>
        /// <param name="confluenceClient">IContentDomain to bind the extension method to</param>
        /// <param name="content">Content to update</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Content</returns>
        public static async Task<Content> UpdateAsync(this IContentDomain confluenceClient, Content content, CancellationToken cancellationToken = default(CancellationToken))
        {
            var contentUri = confluenceClient.ConfluenceApiUri.AppendSegments("content", content.Id);

            confluenceClient.Behaviour.MakeCurrent();
            var response = await contentUri.PutAsync<HttpResponse<Content, Error>>(content, cancellationToken).ConfigureAwait(false);

            if (response.HasError)
            {
                throw new Exception(response.ErrorResponse.Message);
            }
            return response.Response;
        }
    }
}