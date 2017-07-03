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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.Confluence.Entities;
using Dapplo.Confluence.Internals;
using Dapplo.HttpExtensions;

#endregion

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
        public static async Task<Result<Attachment>> AttachAsync<TContent>(this IAttachmentDomain confluenceClient, string contentId, TContent content, string filename, string comment = null, string contentType = null, CancellationToken cancellationToken = default(CancellationToken))
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
            var response = await postAttachmentUri.PostAsync<HttpResponse<Result<Attachment>, string>>(attachment, cancellationToken).ConfigureAwait(false);
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
        /// <param name="confluenceClient">IAttachmentDomain to bind the extension method to</param>
        /// <param name="attachment">Attachment which needs to be deleted</param>
        /// <param name="cancellationToken">cancellationToken</param>
        public static async Task DeleteAsync(this IAttachmentDomain confluenceClient, Attachment attachment,
            CancellationToken cancellationToken = default(CancellationToken))
        {
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
        /// <param name="isTrashed">
        ///     If the content is trashable, you will need to call DeleteAsyc twice, second time with isTrashed
        ///     = true
        /// </param>
        /// <param name="cancellationToken">CancellationToken</param>
        public static async Task DeleteAsync(this IAttachmentDomain confluenceClient, string attachtmentId, bool isTrashed = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            var contentUri = confluenceClient.ConfluenceApiUri.AppendSegments("content", attachtmentId);
            if (isTrashed)
            {
                contentUri = contentUri.ExtendQuery("status", "trashed");
            }
            confluenceClient.Behaviour.MakeCurrent();

            var response = await contentUri.DeleteAsync<HttpResponse<Content, Error>>(cancellationToken).ConfigureAwait(false);
            if (response.HasError)
            {
                throw new Exception(response.ErrorResponse.Message);
            }
        }


        /// <summary>
        ///     Retrieve the attachments for the specified content
        /// </summary>
        /// <param name="confluenceClient">IAttachmentDomain to bind the extension method to</param>
        /// <param name="contentId">string with the content id</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Result with Attachment(s)</returns>
        public static async Task<Result<Attachment>> GetAttachmentsAsync(this IAttachmentDomain confluenceClient, string contentId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            confluenceClient.Behaviour.MakeCurrent();
            var attachmentsUri = confluenceClient.ConfluenceApiUri.AppendSegments("content", contentId, "child", "attachment");
            if (ConfluenceClientConfig.ExpandGetAttachments != null && ConfluenceClientConfig.ExpandGetAttachments.Length != 0)
            {
                attachmentsUri = attachmentsUri.ExtendQuery("expand", string.Join(",", ConfluenceClientConfig.ExpandGetAttachments));
            }
            var response = await attachmentsUri.GetAsAsync<HttpResponse<Result<Attachment>, Error>>(cancellationToken).ConfigureAwait(false);
            if (response.HasError)
            {
                throw new Exception(response.ErrorResponse.Message);
            }
            return response.Response;
        }

        /// <summary>
        ///     Retrieve the attachment for the supplied Attachment entity
        /// </summary>
        /// <typeparam name="TResponse">the type to return the result into. e.g. Bitmap,BitmapSource or MemoryStream</typeparam>
        /// <param name="confluenceClient">IAttachmentDomain to bind the extension method to</param>
        /// <param name="attachment">Attachment</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Bitmap,BitmapSource or MemoryStream (etc) depending on TResponse</returns>
        public static async Task<TResponse> GetContentAsync<TResponse>(this IAttachmentDomain confluenceClient, Attachment attachment,
            CancellationToken cancellationToken = default(CancellationToken))
            where TResponse : class
        {
            confluenceClient.Behaviour.MakeCurrent();

            var attachmentUri = confluenceClient.CreateDownloadUri(attachment.Links);
            var response = await attachmentUri.GetAsAsync<HttpResponse<TResponse, string>>(cancellationToken).ConfigureAwait(false);
            if (response.HasError)
            {
                throw new Exception(response.ErrorResponse);
            }
            return response.Response;
        }

        /// <summary>
        ///     Update the attachment information
        /// </summary>
        /// <param name="confluenceClient">IAttachmentDomain to bind the extension method to</param>
        /// <param name="attachment">Attachment</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Attachment</returns>
        public static async Task<Attachment> UpdateAsync(this IAttachmentDomain confluenceClient, Attachment attachment, CancellationToken cancellationToken = default(CancellationToken))
        {
            confluenceClient.Behaviour.MakeCurrent();
            var id = Regex.Replace(attachment.Id, "[a-z]*", "");
            var attachmentsUri = confluenceClient.ConfluenceApiUri.AppendSegments("content", attachment.Container.Id, "child", "attachment", id);
            if (ConfluenceClientConfig.ExpandGetAttachments != null && ConfluenceClientConfig.ExpandGetAttachments.Length != 0)
            {
                attachmentsUri = attachmentsUri.ExtendQuery("expand", string.Join(",", ConfluenceClientConfig.ExpandGetAttachments));
            }
            var response = await attachmentsUri.GetAsAsync<HttpResponse<Attachment, Error>>(cancellationToken).ConfigureAwait(false);
            if (response.HasError)
            {
                throw new Exception(response.ErrorResponse.Message);
            }
            return response.Response;
        }
    }
}