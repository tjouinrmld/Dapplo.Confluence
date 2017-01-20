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

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.Confluence.Entities;
using Dapplo.Confluence.Query;

#endregion

namespace Dapplo.Confluence
{
	/// <summary>
	///     The is the interface to the content functionality of the Confluence API
	/// </summary>
	public interface IContentApi
	{
		/// <summary>
		///     Get Content information see <a href="https://docs.atlassian.com/confluence/REST/latest/#d3e164">here</a>
		/// </summary>
		/// <param name="contentId">content id</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Content</returns>
		Task<Content> GetAsync(string contentId, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Get Content History information see <a href="https://docs.atlassian.com/confluence/REST/latest/#d3e164">here</a>
		/// </summary>
		/// <param name="contentId">content id</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Content</returns>
		Task<History> GetHistoryAsync(string contentId, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Delete content (attachments are also content)
		/// </summary>
		/// <param name="contentId">ID for the content which needs to be deleted</param>
		/// <param name="isTrashed">
		///     If the content is trashable, you will need to call DeleteAsyc twice, second time with isTrashed
		///     = true
		/// </param>
		/// <param name="cancellationToken">CancellationToken</param>
		Task DeleteAsync(string contentId, bool isTrashed = false, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Create content
		/// </summary>
		/// <param name="type">Type of content, usually page</param>
		/// <param name="title">Title for the content</param>
		/// <param name="spaceKey">Key of the space to add the content to</param>
		/// <param name="body">the complete body (HTML)</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Content</returns>
		Task<Content> CreateAsync(string type, string title, string spaceKey, string body, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Update content
		/// </summary>
		/// <param name="content">Content to update</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Content</returns>
		Task<Content> UpdateAsync(Content content, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Get Content information see <a href="https://docs.atlassian.com/confluence/REST/latest/#d3e164">here</a>
		/// </summary>
		/// <param name="contentId">content id</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>List with Content</returns>
		Task<IList<Content>> GetChildrenAsync(string contentId, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Possible since 5.7
		///     Search for issues, with a CQL (e.g. from a filter) see
		///     <a href="https://docs.atlassian.com/confluence/REST/latest/#d2e4539">here</a>
		/// </summary>
		/// <param name="cql">Confluence Query Language, like SQL, for the search</param>
		/// <param name="cqlContext">
		///     the execution context for CQL functions, provides current space key and content id. If this is
		///     not provided some CQL functions will not be available.
		/// </param>
		/// <param name="limit">Maximum number of results returned, default is 20</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Result with content items</returns>
		Task<Result<Content>> SearchAsync(string cql, string cqlContext = null, int limit = 20, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Possible since 5.7
		///     Search for issues, with a CQL (e.g. from a filter) see
		///     <a href="https://docs.atlassian.com/confluence/REST/latest/#d2e4539">here</a>
		/// </summary>
		/// <param name="cqlClause">Confluence Query Language clause, created via Where</param>
		/// <param name="cqlContext">
		///     the execution context for CQL functions, provides current space key and content id. If this is
		///     not provided some CQL functions will not be available.
		/// </param>
		/// <param name="limit">Maximum number of results returned, default is 20</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Result with content items</returns>
		Task<Result<Content>> SearchAsync(IFinalClause cqlClause, string cqlContext = null, int limit = 20, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Get content by title
		///     See: https://docs.atlassian.com/confluence/REST/latest/#d2e4539
		/// </summary>
		/// <param name="spaceKey">Space key</param>
		/// <param name="title">Title of the content</param>
		/// <param name="start">Start of the results, used for paging</param>
		/// <param name="limit">Maximum number of results returned, default is 20</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Results with content items</returns>
		Task<Result<Content>> GetByTitleAsync(string spaceKey, string title, int start = 0, int limit = 20, CancellationToken cancellationToken = default(CancellationToken));
	}
}