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
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Confluence. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.Confluence.Entities;
using Dapplo.HttpExtensions;

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
			ConfluenceBaseUri = baseUri.AppendSegments("rest", "api");

			_behaviour = new HttpBehaviour
			{
				HttpSettings = httpSettings,
				OnHttpRequestMessageCreated = httpMessage =>
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

		#region Write

		/// <summary>
		///     Add an attachment to content
		/// </summary>
		/// <typeparam name="TContent">The content to upload</typeparam>
		/// <param name="contentId">content to add the attachment to</param>
		/// <param name="content">content of type TContent tfor the attachment</param>
		/// <param name="filename">Filename of the attachment</param>
		/// <param name="comment">Comment in the attachments information</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Result with Attachment</returns>
		public async Task<Result<Attachment>> AttachAsync<TContent>(string contentId, TContent content, string filename, string comment = null,
			CancellationToken cancellationToken = default(CancellationToken))
			where TContent : class
		{
			var attachment = new AttachmentContainer<TContent>
			{
				Comment = comment,
				FileName = filename,
				Content = content
			};
			_behaviour.MakeCurrent();
			var postAttachmentUri = ConfluenceBaseUri.AppendSegments("content", contentId, "child", "attachment");
			var response = await postAttachmentUri.PostAsync<HttpResponse<Result<Attachment>, string>>(attachment, cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse);
			}
			return response.Response;
		}

		#endregion

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

		#region Read

		/// <summary>
		///     Retrieve the attachment for the supplied Attachment entity
		/// </summary>
		/// <typeparam name="TResponse">the type to return the result into. e.g. Bitmap,BitmapSource or MemoryStream</typeparam>
		/// <param name="attachment">Attachment</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Bitmap,BitmapSource or MemoryStream (etc) depending on TResponse</returns>
		public async Task<TResponse> GetAttachmentContentAsync<TResponse>(Attachment attachment, CancellationToken cancellationToken = default(CancellationToken))
			where TResponse : class
		{
			_behaviour.MakeCurrent();
			var attachmentUriBuilder = new UriBuilder(ConfluenceBaseUri)
			{
				Path = attachment.Links.Download
			};
			var response = await attachmentUriBuilder.Uri.GetAsAsync<HttpResponse<TResponse, string>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse);
			}
			return response.Response;
		}

		/// <summary>
		///     Retrieve the attachments for the content
		/// </summary>
		/// <param name="contentId">string with the content id</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Result with Attachment(s)</returns>
		public async Task<Result<Attachment>> GetAttachmentsAsync(string contentId, CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			var attachmentsUri = ConfluenceBaseUri.AppendSegments("content", contentId, "child", "attachment");
			if (ConfluenceConfig.ExpandGetAttachments != null && ConfluenceConfig.ExpandGetAttachments.Length != 0)
			{
				attachmentsUri = attachmentsUri.ExtendQuery("expand", string.Join(",", ConfluenceConfig.ExpandGetAttachments));
			}
			var response = await attachmentsUri.GetAsAsync<HttpResponse<Result<Attachment>, string>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse);
			}
			return response.Response;
		}

		/// <summary>
		///     Retrieve the picture for the supplied Picture entity
		/// </summary>
		/// <typeparam name="TResponse">the type to return the result into. e.g. Bitmap,BitmapSource or MemoryStream</typeparam>
		/// <param name="picture">Picture from User, Space, History etc</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Bitmap,BitmapSource or MemoryStream (etc) depending on TResponse</returns>
		public async Task<TResponse> GetPictureAsync<TResponse>(Picture picture, CancellationToken cancellationToken = default(CancellationToken))
			where TResponse : class
		{
			_behaviour.MakeCurrent();
			var pictureUriBuilder = new UriBuilder(ConfluenceBaseUri)
			{
				Path = picture.Path
			};
			var response = await pictureUriBuilder.Uri.GetAsAsync<HttpResponse<TResponse, string>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse);
			}
			return response.Response;
		}

		/// <summary>
		///     Get Space information see <a href="https://docs.atlassian.com/confluence/REST/latest/#d3e164">here</a>
		/// </summary>
		/// <param name="spaceKey">the space key</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Space</returns>
		public async Task<Space> GetSpaceAsync(string spaceKey, CancellationToken cancellationToken = default(CancellationToken))
		{
			var spaceUri = ConfluenceBaseUri.AppendSegments("space", spaceKey);
			if (ConfluenceConfig.ExpandGetSpace != null && ConfluenceConfig.ExpandGetSpace.Length != 0)
			{
				spaceUri = spaceUri.ExtendQuery("expand", string.Join(",", ConfluenceConfig.ExpandGetSpace));
			}

			_behaviour.MakeCurrent();
			var response = await spaceUri.GetAsAsync<HttpResponse<Space, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response;
		}

		/// <summary>
		///     Get Spaces see <a href="https://docs.atlassian.com/confluence/REST/latest/#d3e164">here</a>
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>List of Space</returns>
		public async Task<IList<Space>> GetSpacesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var spacesUri = ConfluenceBaseUri.AppendSegments("space");

			if (ConfluenceConfig.ExpandGetSpace != null && ConfluenceConfig.ExpandGetSpace.Length != 0)
			{
				spacesUri = spacesUri.ExtendQuery("expand", string.Join(",", ConfluenceConfig.ExpandGetSpace));
			}

			_behaviour.MakeCurrent();
			var response = await spacesUri.GetAsAsync<HttpResponse<Result<Space>, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response.Results;
		}

		/// <summary>
		///     Get Content information see <a href="https://docs.atlassian.com/confluence/REST/latest/#d3e164">here</a>
		/// </summary>
		/// <param name="contentId">content id</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Content</returns>
		public async Task<Content> GetContentAsync(string contentId, CancellationToken cancellationToken = default(CancellationToken))
		{
			var contentUri = ConfluenceBaseUri.AppendSegments("content", contentId);
			if (ConfluenceConfig.ExpandGetContent != null && ConfluenceConfig.ExpandGetContent.Length != 0)
			{
				contentUri = contentUri.ExtendQuery("expand", string.Join(",", ConfluenceConfig.ExpandGetContent));
			}

			_behaviour.MakeCurrent();
			var response = await contentUri.GetAsAsync<HttpResponse<Content, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response;
		}

		/// <summary>
		///     Get Content information see <a href="https://docs.atlassian.com/confluence/REST/latest/#d3e164">here</a>
		/// </summary>
		/// <param name="contentId">content id</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>List with Content</returns>
		public async Task<IList<Content>> GetChildrenAsync(string contentId, CancellationToken cancellationToken = default(CancellationToken))
		{
			var contentUri = ConfluenceBaseUri.AppendSegments("content", contentId, "child");
			if (ConfluenceConfig.ExpandGetChildren != null && ConfluenceConfig.ExpandGetChildren.Length != 0)
			{
				contentUri = contentUri.ExtendQuery("expand", string.Join(",", ConfluenceConfig.ExpandGetChildren));
			}
			_behaviour.MakeCurrent();
			var response = await contentUri.GetAsAsync<HttpResponse<Child, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response.Result.Results;
		}

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
		public async Task<Result<Content>> SearchAsync(string cql, string cqlContext = null, int limit = 20, CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();

			var searchUri = ConfluenceBaseUri.AppendSegments("content", "search").ExtendQuery("cql", cql).ExtendQuery("limit", limit);
			if (ConfluenceConfig.ExpandSearch != null && ConfluenceConfig.ExpandSearch.Length != 0)
			{
				searchUri = searchUri.ExtendQuery("expand", string.Join(",", ConfluenceConfig.ExpandSearch));
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
		///     Get content by title
		///     See: https://docs.atlassian.com/confluence/REST/latest/#d2e4539
		/// </summary>
		/// <param name="spaceKey">Space key</param>
		/// <param name="title">Title of the content</param>
		/// <param name="start">Start of the results, used for paging</param>
		/// <param name="limit">Maximum number of results returned, default is 20</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Results with content items</returns>
		public async Task<Result<Content>> GetContentByTitleAsync(string spaceKey, string title, int start = 0, int limit = 20, CancellationToken cancellationToken = default(CancellationToken))
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
				}
			});
			if (ConfluenceConfig.ExpandGetContentByTitle != null && ConfluenceConfig.ExpandGetContentByTitle.Length != 0)
			{
				searchUri = searchUri.ExtendQuery("expand", string.Join(",", ConfluenceConfig.ExpandGetContentByTitle));
			}
			var response = await searchUri.GetAsAsync<HttpResponse<Result<Content>, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response;
		}


		/// <summary>
		///     Get currrent user information, introduced with 6.6
		///     See: https://docs.atlassian.com/confluence/REST/latest/#user-getCurrent
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>User</returns>
		public async Task<User> GetCurrentUserAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var myselfUri = ConfluenceBaseUri.AppendSegments("user", "current");
			if (ConfluenceConfig.ExpandGetCurrentUser != null && ConfluenceConfig.ExpandGetCurrentUser.Length != 0)
			{
				myselfUri = myselfUri.ExtendQuery("expand", string.Join(",", ConfluenceConfig.ExpandGetCurrentUser));
			}
			_behaviour.MakeCurrent();
			var response = await myselfUri.GetAsAsync<HttpResponse<User, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response;
		}

		/// <summary>
		///     Get user information, introduced with 6.6
		///     See: https://docs.atlassian.com/confluence/REST/latest/#user-getUser
		/// </summary>
		/// <param name="username"></param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>user information</returns>
		public async Task<User> GetUserAsync(string username, CancellationToken cancellationToken = default(CancellationToken))
		{
			var userUri = ConfluenceBaseUri.AppendSegments("user").ExtendQuery("username", username);
			if (ConfluenceConfig.ExpandGetUser != null && ConfluenceConfig.ExpandGetUser.Length != 0)
			{
				userUri = userUri.ExtendQuery("expand", string.Join(",", ConfluenceConfig.ExpandGetUser));
			}
			_behaviour.MakeCurrent();
			var response = await userUri.GetAsAsync<HttpResponse<User, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response;
		}

		#endregion
	}
}