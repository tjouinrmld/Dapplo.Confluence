//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2015-2016 Dapplo
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
//  You should have Config a copy of the GNU Lesser General Public License
//  along with Dapplo.Confluence. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.Confluence.Entities;

#endregion

namespace Dapplo.Confluence
{
	/// <summary>
	///     Confluence API, using Dapplo.HttpExtensions
	/// </summary>
	public class ConfluenceApi
	{
		/// <summary>
		///     Store the specific HttpBehaviour, which contains a IHttpSettings and also some additional logic for making a
		///     HttpClient which works with Confluence
		/// </summary>
		private readonly HttpBehaviour _behaviour;

		private string _password;

		private string _user;

		/// <summary>
		///     Create the ConfluenceApi object, here the HttpClient is configured
		/// </summary>
		/// <param name="baseUri">Base URL, e.g. https://yourConfluenceserver</param>
		/// <param name="httpSettings">IHttpSettings or null for default</param>
		public ConfluenceApi(Uri baseUri, IHttpSettings httpSettings = null)
		{
			if (baseUri == null)
			{
				throw new ArgumentNullException(nameof(baseUri));
			}
			ConfluenceBaseUri = baseUri.AppendSegments("rest", "api", "2");

			_behaviour = new HttpBehaviour
			{
				HttpSettings = httpSettings,
				OnHttpRequestMessageCreated = (httpMessage) =>
				{
					httpMessage?.Headers.TryAddWithoutValidation("X-Atlassian-Token", "nocheck");
					if (!string.IsNullOrEmpty(_user) && _password != null)
					{
						httpMessage?.SetBasicAuthorization(_user, _password);
					}
					return httpMessage;
				}
			};
		}

		/// <summary>
		///     The base URI for your Confluence server
		/// </summary>
		public Uri ConfluenceBaseUri { get; }

		/// <summary>
		///     Set Basic Authentication for the current client
		/// </summary>
		/// <param name="user">username</param>
		/// <param name="password">password</param>
		public void SetBasicAuthentication(string user, string password)
		{
			_user = user;
			_password = password;
		}

		#region write
		/// <summary>
		///     Attach content to the specified issue
		///     See: https://docs.atlassian.com/Confluence/REST/latest/#d2e3035
		/// </summary>
		/// <param name="contentId">Id of the content to attach to</param>
		/// <param name="content">the content can be anything what Dapplo.HttpExtensions supports</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Attachment</returns>
		public async Task<Result<Attachment>> AttachAsync(string contentId, object content, CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			var attachUri = ConfluenceBaseUri.AppendSegments("content", contentId, "child", "attachments");
			return await attachUri.PostAsync<Result<Attachment>>(content, cancellationToken).ConfigureAwait(false);
		}
		#endregion

		#region Read

		/// <summary>
		///     Retrieve the attachment for the supplied Attachment entity
		/// </summary>
		/// <typeparam name="TResponse">the type to return the result into. e.g. Bitmap,BitmapSource or MemoryStream</typeparam>
		/// <param name="attachment">Attachment</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Bitmap,BitmapSource or MemoryStream (etc) depending on TResponse</returns>
		public async Task<TResponse> PictureAsync<TResponse>(Attachment attachment, CancellationToken cancellationToken = default(CancellationToken))
			where TResponse : class
		{
			_behaviour.MakeCurrent();
			var attachmentUriBuilder = new UriBuilder(ConfluenceBaseUri);
			attachmentUriBuilder.Path = attachment.Links.Download;
			return await attachmentUriBuilder.Uri.GetAsAsync<TResponse>(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Retrieve the picture for the supplied Picture entity
		/// </summary>
		/// <typeparam name="TResponse">the type to return the result into. e.g. Bitmap,BitmapSource or MemoryStream</typeparam>
		/// <param name="picture">Picture from User, Space, History etc</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Bitmap,BitmapSource or MemoryStream (etc) depending on TResponse</returns>
		public async Task<TResponse> PictureAsync<TResponse>(Picture picture, CancellationToken cancellationToken = default(CancellationToken))
			where TResponse : class
		{
			_behaviour.MakeCurrent();
			var pictureUriBuilder = new UriBuilder(ConfluenceBaseUri);
			pictureUriBuilder.Path = picture.Path;
			return await pictureUriBuilder.Uri.GetAsAsync<TResponse>(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Get Space information
		///     See: https://docs.atlassian.com/Confluence/REST/latest/#d2e4539
		/// </summary>
		/// <param name="spaceKey">the space key</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Space</returns>
		public async Task<Space> SpaceAsync(string spaceKey, CancellationToken cancellationToken = default(CancellationToken))
		{
			var spaceUri = ConfluenceBaseUri.AppendSegments("space", spaceKey);
			_behaviour.MakeCurrent();
			return await spaceUri.GetAsAsync<Space>(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Search for issues, with a CQL (e.g. from a filter)
		///     See: https://docs.atlassian.com/Confluence/REST/latest/#d2e4539
		/// </summary>
		/// <param name="cql">Confluence Query Language, like SQL, for the search</param>
		/// <param name="cqlContext">the execution context for CQL functions, provides current space key and content id. If this is not provided some CQL functions will not be available.</param>
		/// <param name="limit">Maximum number of results returned, default is 20</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>dynamic</returns>
		public async Task<dynamic> SearchAsync(string cql, string cqlContext = null, int limit = 20, CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			var searchUri = ConfluenceBaseUri.AppendSegments("content", "search").ExtendQuery("cql", cql).ExtendQuery("cqlcontext", cqlContext).ExtendQuery("limit", limit);
			return await searchUri.GetAsAsync<dynamic>(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Get content by title
		///     See: https://docs.atlassian.com/Confluence/REST/latest/#d2e4539
		/// </summary>
		/// <param name="spaceKey">Space key</param>
		/// <param name="title">Title of the content</param>
		/// <param name="start">Start of the results, used for paging</param>
		/// <param name="limit">Maximum number of results returned, default is 20</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Results with content items</returns>
		public async Task<Result<Content>> ContentByTitleAsync(string spaceKey, string title, int start = 0, int limit = 20, CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			var searchUri = ConfluenceBaseUri.AppendSegments("content").ExtendQuery(new Dictionary<string, object>
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
					},
				});
			return await searchUri.GetAsAsync<Result<Content>>(cancellationToken).ConfigureAwait(false);
		}


		/// <summary>
		///     Get currrent user information, introduced with 6.6
		///     See: https://docs.atlassian.com/Confluence/REST/latest/#user-getCurrent
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>User</returns>
		public async Task<User> MyselfAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var myselfUri = ConfluenceBaseUri.AppendSegments("user","current");
			_behaviour.MakeCurrent();
			return await myselfUri.GetAsAsync<User>(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Get user information, introduced with 6.6
		///     See: https://docs.atlassian.com/Confluence/REST/latest/#user-getUser
		/// </summary>
		/// <param name="username"></param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>user information</returns>
		public async Task<User> UserAsync(string username, CancellationToken cancellationToken = default(CancellationToken))
		{
			var userUri = ConfluenceBaseUri.AppendSegments("user").ExtendQuery("username", username);
			_behaviour.MakeCurrent();
			return await userUri.GetAsAsync<User>(cancellationToken).ConfigureAwait(false);
		}

		#endregion
	}
}