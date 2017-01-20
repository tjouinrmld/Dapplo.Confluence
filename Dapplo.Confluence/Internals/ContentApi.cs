#region Dapplo 2016 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2016 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Confluence
// 
// Dapplo.Confluence is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Confluence is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Confluence. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

#region Usings

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.Confluence.Entities;
using Dapplo.Confluence.Query;
using Dapplo.HttpExtensions;

#endregion

namespace Dapplo.Confluence.Internals
{
	/// <summary>
	///     The is the implementation to the content functionality of the Confluence API
	/// </summary>
	internal class ContentApi : IContentApi
	{
		private readonly IConfluenceClientPlugins _confluenceClientPlugins;

		internal ContentApi(IConfluenceClient confluenceClient)
		{
			_confluenceClientPlugins = confluenceClient.Plugins;
		}

		/// <inheritdoc />
		public async Task<Content> GetAsync(string contentId, CancellationToken cancellationToken = default(CancellationToken))
		{
			var contentUri = _confluenceClientPlugins.ConfluenceApiUri.AppendSegments("content", contentId);
			if ((ConfluenceClientConfig.ExpandGetContent != null) && (ConfluenceClientConfig.ExpandGetContent.Length != 0))
			{
				contentUri = contentUri.ExtendQuery("expand", string.Join(",", ConfluenceClientConfig.ExpandGetContent));
			}

			_confluenceClientPlugins.PromoteContext();
			var response = await contentUri.GetAsAsync<HttpResponse<Content, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response;
		}

		/// <inheritdoc />
		public async Task<History> GetHistoryAsync(string contentId, CancellationToken cancellationToken = default(CancellationToken))
		{
			var historyUri = _confluenceClientPlugins.ConfluenceApiUri.AppendSegments("content", contentId, "history");

			_confluenceClientPlugins.PromoteContext();

			var response = await historyUri.GetAsAsync<HttpResponse<History, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response;
		}

		/// <inheritdoc />
		public async Task DeleteAsync(string contentId, bool isTrashed = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			var contentUri = _confluenceClientPlugins.ConfluenceApiUri.AppendSegments("content", contentId);
			if (isTrashed)
			{
				contentUri = contentUri.ExtendQuery("status", "trashed");
			}
			_confluenceClientPlugins.PromoteContext();

			var response = await contentUri.DeleteAsync<HttpResponse<Content, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
		}

		/// <inheritdoc />
		public async Task<Content> CreateAsync(string type, string title, string spaceKey, string body, CancellationToken cancellationToken = default(CancellationToken))
		{
			var contentUri = _confluenceClientPlugins.ConfluenceApiUri.AppendSegments("content");

			var newPage = new Content
			{
				Type = type,
				Title = title,
				Space = new Space
				{
					Key = spaceKey
				},
				Body = new Body
				{
					Storage = new BodyContent
					{
						Value = body,
						Representation = "storage"
					}
				}
			};
			_confluenceClientPlugins.PromoteContext();
			var response = await contentUri.PostAsync<HttpResponse<Content, Error>>(newPage, cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response;
		}

		/// <inheritdoc />
		public async Task<Content> UpdateAsync(Content content, CancellationToken cancellationToken = default(CancellationToken))
		{
			var contentUri = _confluenceClientPlugins.ConfluenceApiUri.AppendSegments("content", content.Id);

			_confluenceClientPlugins.PromoteContext();
			var response = await contentUri.PostAsync<HttpResponse<Content, Error>>(content, cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response;
		}

		/// <inheritdoc />
		public async Task<IList<Content>> GetChildrenAsync(string contentId, CancellationToken cancellationToken = default(CancellationToken))
		{
			var contentUri = _confluenceClientPlugins.ConfluenceApiUri.AppendSegments("content", contentId, "child");
			if ((ConfluenceClientConfig.ExpandGetChildren != null) && (ConfluenceClientConfig.ExpandGetChildren.Length != 0))
			{
				contentUri = contentUri.ExtendQuery("expand", string.Join(",", ConfluenceClientConfig.ExpandGetChildren));
			}
			_confluenceClientPlugins.PromoteContext();
			var response = await contentUri.GetAsAsync<HttpResponse<Child, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response.Result.Results;
		}

		/// <inheritdoc />
		public async Task<Result<Content>> SearchAsync(IFinalClause clause, string cqlContext = null, int limit = 20, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await SearchAsync(clause.ToString(), cqlContext, limit, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<Result<Content>> SearchAsync(string cql, string cqlContext = null, int limit = 20, CancellationToken cancellationToken = default(CancellationToken))
		{
			_confluenceClientPlugins.PromoteContext();

			var searchUri = _confluenceClientPlugins.ConfluenceApiUri.AppendSegments("content", "search").ExtendQuery("cql", cql).ExtendQuery("limit", limit);
			if ((ConfluenceClientConfig.ExpandSearch != null) && (ConfluenceClientConfig.ExpandSearch.Length != 0))
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

		/// <inheritdoc />
		public async Task<Result<Content>> GetByTitleAsync(string spaceKey, string title, int start = 0, int limit = 20, CancellationToken cancellationToken = default(CancellationToken))
		{
			_confluenceClientPlugins.PromoteContext();
			var searchUri = _confluenceClientPlugins.ConfluenceApiUri.AppendSegments("content").ExtendQuery(new Dictionary<string, object>
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
			if ((ConfluenceClientConfig.ExpandGetContentByTitle != null) && (ConfluenceClientConfig.ExpandGetContentByTitle.Length != 0))
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
	}
}