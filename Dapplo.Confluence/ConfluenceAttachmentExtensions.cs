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
using System.Threading;
using System.Threading.Tasks;
using Dapplo.Confluence.Entities;
using Dapplo.Confluence.Internals;
using Dapplo.HttpExtensions;

#endregion

namespace Dapplo.Confluence
{
	/// <summary>
	///     All attachment related extensions
	/// </summary>
	public static class ConfluenceAttachmentExtensions
	{
		/// <summary>
		///     Add an attachment to the specified content
		/// </summary>
		/// <typeparam name="TContent">The content to upload</typeparam>
		/// <param name="confluenceClient">Instance of a confluence client</param>
		/// <param name="contentId">content to add the attachment to</param>
		/// <param name="content">content of type TContent tfor the attachment</param>
		/// <param name="filename">Filename of the attachment</param>
		/// <param name="comment">Comment in the attachments information</param>
		/// <param name="contentType">Content-Type for the content, or null</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Result with Attachment</returns>
		public static async Task<Result<Attachment>> AttachAsync<TContent>(this IConfluenceClient confluenceClient, string contentId, TContent content, string filename,
			string comment = null, string contentType = null,
			CancellationToken cancellationToken = default(CancellationToken))
			where TContent : class
		{
			var attachment = new AttachmentContainer<TContent>
			{
				Comment = comment,
				FileName = filename,
				Content = content,
				ContentType = contentType
			};
			var confluenceClientPlugins = confluenceClient.Plugins;
			confluenceClientPlugins.PromoteContext();

			var postAttachmentUri = confluenceClientPlugins.ConfluenceApiUri.AppendSegments("content", contentId, "child", "attachment");
			var response = await postAttachmentUri.PostAsync<HttpResponse<Result<Attachment>, string>>(attachment, cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse);
			}
			return response.Response;
		}

		/// <summary>
		///     Retrieve the attachments for the specified content
		/// </summary>
		/// <param name="confluenceClient">Instance of a confluence client</param>
		/// <param name="contentId">string with the content id</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Result with Attachment(s)</returns>
		public static async Task<Result<Attachment>> GetAttachmentsAsync(this IConfluenceClient confluenceClient, string contentId,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var confluenceClientPlugins = confluenceClient.Plugins;
			confluenceClientPlugins.PromoteContext();
			var attachmentsUri = confluenceClientPlugins.ConfluenceApiUri.AppendSegments("content", contentId, "child", "attachment");
			if ((ConfluenceClientConfig.ExpandGetAttachments != null) && (ConfluenceClientConfig.ExpandGetAttachments.Length != 0))
			{
				attachmentsUri = attachmentsUri.ExtendQuery("expand", string.Join(",", ConfluenceClientConfig.ExpandGetAttachments));
			}
			var response = await attachmentsUri.GetAsAsync<HttpResponse<Result<Attachment>, string>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse);
			}
			return response.Response;
		}

		/// <summary>
		///     Delete attachment
		///     Can't work yet, see <a href="https://jira.atlassian.com/browse/CONF-36015">CONF-36015</a>
		/// </summary>
		/// <param name="confluenceClient">Instance of a confluence client</param>
		/// <param name="attachment">Attachment which needs to be deleted</param>
		/// <param name="cancellationToken">cancellationToken</param>
		public static async Task DeleteAttachmentAsync(this IConfluenceClient confluenceClient, Attachment attachment,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var confluenceClientPlugins = confluenceClient.Plugins;
			confluenceClientPlugins.PromoteContext();
			var contentUri =
				confluenceClientPlugins.ConfluenceUri.AppendSegments("json", "removeattachmentversion.action")
					.ExtendQuery("pageId", attachment.Container.Id)
					.ExtendQuery("fileName", attachment.Title);

			await contentUri.GetAsAsync<string>(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Retrieve the attachment for the supplied Attachment entity
		/// </summary>
		/// <typeparam name="TResponse">the type to return the result into. e.g. Bitmap,BitmapSource or MemoryStream</typeparam>
		/// <param name="confluenceClient">Instance of a confluence client</param>
		/// <param name="attachment">Attachment</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Bitmap,BitmapSource or MemoryStream (etc) depending on TResponse</returns>
		public static async Task<TResponse> GetAttachmentContentAsync<TResponse>(this IConfluenceClient confluenceClient, Attachment attachment,
			CancellationToken cancellationToken = default(CancellationToken))
			where TResponse : class
		{
			var confluenceClientPlugins = confluenceClient.Plugins;
			confluenceClientPlugins.PromoteContext();

			var attachmentUriBuilder = new UriBuilder(confluenceClientPlugins.ConfluenceUri)
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
	}
}