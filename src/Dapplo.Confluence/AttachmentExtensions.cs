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

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.Confluence.Entities;
using Dapplo.Confluence.Internals;
using Dapplo.Confluence.Query;
using Dapplo.HttpExtensions;
using Enumerable = System.Linq.Enumerable;

namespace Dapplo.Confluence
{
    /// <summary>
    ///     Marker interface for the attachment domain
    /// </summary>
    public interface IAttachmentDomain : IConfluenceDomain
    {
    }

    /// <summary>
    ///     All attachment related extension methods
    /// </summary>
    public static class AttachmentDomain
    {
        /// <summary>
        /// Obsolete: this AttachAsync is a wrapper for the new signature, which only excepts a long for the ID.
        /// </summary>
        /// <typeparam name="TContent">Type of the content</typeparam>
        /// <param name="confluenceClient">IAttachmentDomain</param>
        /// <param name="contentId">string</param>
        /// <param name="content">TContent</param>
        /// <param name="filename">string</param>
        /// <param name="comment">string</param>
        /// <param name="contentType">string</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Result with Content</returns>
        [Obsolete("The contentId should be of type long")]
        public static Task<Result<Content>> AttachAsync<TContent>(this IAttachmentDomain confluenceClient, string contentId, TContent content, string filename,
            string comment = null, string contentType = null, CancellationToken cancellationToken = default)
            where TContent : class
        {
            return confluenceClient.AttachAsync(long.Parse(contentId), content, filename, comment, contentType, cancellationToken);
        }

        /// <summary>
        ///     Add an attachment to the specified content
        /// </summary>
        /// <typeparam name="TContent">The content to upload</typeparam>
        /// <param name="confluenceClient">IAttachmentDomain to bind the extension method to</param>
        /// <param name="contentId">content to add the attachment to</param>
        /// <param name="content">content of type TContent for the attachment</param>
        /// <param name="filename">Filename of the attachment</param>
        /// <param name="comment">Comment in the attachments information</param>
        /// <param name="contentType">Content-Type for the content, or null</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Result with Attachment</returns>
        public static async Task<Result<Content>> AttachAsync<TContent>(this IAttachmentDomain confluenceClient, long contentId, TContent content, string filename, string comment = null, string contentType = null, CancellationToken cancellationToken = default)
            where TContent : class
        {
            var attachment = new AttachmentContainer<TContent>
            {
                Comment = comment,
                FileName = filename,
                Content = content,
                ContentType = contentType
            };
            confluenceClient.Behaviour.MakeCurrent();

            var postAttachmentUri = confluenceClient.ConfluenceApiUri.AppendSegments("content", contentId, "child", "attachment");
            var response = await postAttachmentUri.PostAsync<HttpResponse<Result<Content>, Error>>(attachment, cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Delete attachment
        ///     Can't work yet, see <a href="https://jira.atlassian.com/browse/CONF-36015">CONF-36015</a>
        /// </summary>
        /// <param name="confluenceClient">IAttachmentDomain to bind the extension method to</param>
        /// <param name="attachment">Attachment which needs to be deleted</param>
        /// <param name="cancellationToken">cancellationToken</param>
        public static async Task DeleteAsync(this IAttachmentDomain confluenceClient, Content attachment,
            CancellationToken cancellationToken = default)
        {
	        if (attachment.Type != ContentTypes.Attachment)
	        {
		        throw new ArgumentException("Not an attachment", nameof(attachment));
	        }
            confluenceClient.Behaviour.MakeCurrent();

            var contentUri = confluenceClient.ConfluenceUri
                .AppendSegments("json", "removeattachmentversion.action")
                .ExtendQuery("pageId", attachment.Container.Id)
                .ExtendQuery("fileName", attachment.Title);

            await contentUri.GetAsAsync<string>(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        ///     Delete content (attachments are also content)
        /// </summary>
        /// <param name="confluenceClient">IAttachmentDomain to bind the extension method to</param>
        /// <param name="attachtmentId">ID for the content which needs to be deleted</param>
        /// <param name="isTrashed">If the content is trashable, you will need to call DeleteAsyc twice, second time with isTrashed = true</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public static async Task DeleteAsync(this IAttachmentDomain confluenceClient, long attachtmentId, bool isTrashed = false, CancellationToken cancellationToken = default)
        {
            var contentUri = confluenceClient.ConfluenceApiUri.AppendSegments("content", $"att{attachtmentId}");

            if (isTrashed)
            {
                contentUri = contentUri.ExtendQuery("status", "trashed");
            }
            confluenceClient.Behaviour.MakeCurrent();

            var response = await contentUri.DeleteAsync<HttpResponse>(cancellationToken).ConfigureAwait(false);
            response.HandleStatusCode(isTrashed ? HttpStatusCode.OK : HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Obsolete: this GetAttachmentsAsync is a wrapper for the new signature, which only excepts a long for the ID.
        /// </summary>
        /// <param name="confluenceClient">IAttachmentDomain</param>
        /// <param name="contentId">string</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Result with Content</returns>
        [Obsolete("The contentId should be of type long")]
        public static Task<Result<Content>> GetAttachmentsAsync(this IAttachmentDomain confluenceClient, string contentId, CancellationToken cancellationToken = default)
        {
            return confluenceClient.GetAttachmentsAsync(long.Parse(contentId), cancellationToken);
        }

        /// <summary>
        ///     Retrieve the attachments for the specified content
        /// </summary>
        /// <param name="confluenceClient">IAttachmentDomain to bind the extension method to</param>
        /// <param name="contentId">string with the content id</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Result with Attachment(s)</returns>
        public static async Task<Result<Content>> GetAttachmentsAsync(this IAttachmentDomain confluenceClient, long contentId,
            CancellationToken cancellationToken = default)
        {
            confluenceClient.Behaviour.MakeCurrent();

            var attachmentsUri = confluenceClient.ConfluenceApiUri.AppendSegments("content", contentId, "child", "attachment");

            var expand = string.Join(",", ConfluenceClientConfig.ExpandGetAttachments ?? Enumerable.Empty<string>());
            if (!string.IsNullOrEmpty(expand))
            {
                attachmentsUri = attachmentsUri.ExtendQuery("expand", expand);
            }

            var response = await attachmentsUri.GetAsAsync<HttpResponse<Result<Content>, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Retrieve the attachment for the supplied Attachment entity
        /// </summary>
        /// <typeparam name="TResponse">the type to return the result into. e.g. Bitmap,BitmapSource or MemoryStream</typeparam>
        /// <param name="confluenceClient">IAttachmentDomain to bind the extension method to</param>
        /// <param name="attachment">Attachment</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Bitmap,BitmapSource or MemoryStream (etc) depending on TResponse</returns>
        public static async Task<TResponse> GetContentAsync<TResponse>(this IAttachmentDomain confluenceClient, Content attachment,
            CancellationToken cancellationToken = default)
            where TResponse : class
        {
	        if (attachment.Type != ContentTypes.Attachment)
	        {
		        throw new ArgumentException("Not an attachment", nameof(attachment));
	        }
			confluenceClient.Behaviour.MakeCurrent();

            var attachmentUri = confluenceClient.CreateDownloadUri(attachment.Links);
            var response = await attachmentUri.GetAsAsync<HttpResponse<TResponse, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Update the attachment information
        /// </summary>
        /// <param name="confluenceClient">IAttachmentDomain to bind the extension method to</param>
        /// <param name="attachment">Attachment</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Attachment</returns>
        public static async Task<Content> UpdateAsync(this IAttachmentDomain confluenceClient, Content attachment, CancellationToken cancellationToken = default)
        {
            confluenceClient.Behaviour.MakeCurrent();

            var attachmentsUri = confluenceClient.ConfluenceApiUri.AppendSegments("content", attachment.Container.Id, "child", "attachment", attachment.Id);

            var expand = string.Join(",", ConfluenceClientConfig.ExpandGetAttachments ?? Enumerable.Empty<string>());
            if (!string.IsNullOrEmpty(expand))
            {
                attachmentsUri = attachmentsUri.ExtendQuery("expand", expand);
            }

            var response = await attachmentsUri.GetAsAsync<HttpResponse<Content, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }
    }
}