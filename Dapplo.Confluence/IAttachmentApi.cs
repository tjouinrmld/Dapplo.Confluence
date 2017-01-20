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

using System.Threading;
using System.Threading.Tasks;
using Dapplo.Confluence.Entities;

#endregion

namespace Dapplo.Confluence
{
	/// <summary>
	///     All attachment related methods
	/// </summary>
	public interface IAttachmentApi
	{
		/// <summary>
		///     Delete attachment
		///     Can't work yet, see <a href="https://jira.atlassian.com/browse/CONF-36015">CONF-36015</a>
		/// </summary>
		/// <param name="attachment">Attachment which needs to be deleted</param>
		/// <param name="cancellationToken">cancellationToken</param>
		Task DeleteAsync(Attachment attachment, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Retrieve the attachment for the supplied Attachment entity
		/// </summary>
		/// <typeparam name="TResponse">the type to return the result into. e.g. Bitmap,BitmapSource or MemoryStream</typeparam>
		/// <param name="attachment">Attachment</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Bitmap,BitmapSource or MemoryStream (etc) depending on TResponse</returns>
		Task<TResponse> GetContentAsync<TResponse>(Attachment attachment, CancellationToken cancellationToken = default(CancellationToken))
			where TResponse : class;

		/// <summary>
		///     Update the attachment information
		/// </summary>
		/// <param name="attachment">Attachment</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Attachment</returns>
		Task<Attachment> UpdateAsync(Attachment attachment, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Add an attachment to the specified content
		/// </summary>
		/// <typeparam name="TContent">The content to upload</typeparam>
		/// <param name="contentId">content to add the attachment to</param>
		/// <param name="content">content of type TContent for the attachment</param>
		/// <param name="filename">Filename of the attachment</param>
		/// <param name="comment">Comment in the attachments information</param>
		/// <param name="contentType">Content-Type for the content, or null</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Result with Attachment</returns>
		Task<Result<Attachment>> AttachAsync<TContent>(string contentId, TContent content, string filename, string comment = null, string contentType = null, CancellationToken cancellationToken = default(CancellationToken))
			where TContent : class;

		/// <summary>
		///     Retrieve the attachments for the specified content
		/// </summary>
		/// <param name="contentId">string with the content id</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Result with Attachment(s)</returns>
		Task<Result<Attachment>> GetAttachmentsAsync(string contentId, CancellationToken cancellationToken = default(CancellationToken));

	}
}