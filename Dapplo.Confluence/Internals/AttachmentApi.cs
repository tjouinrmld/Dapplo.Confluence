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
using System.Threading;
using System.Threading.Tasks;
using Dapplo.Confluence.Entities;
using Dapplo.HttpExtensions;

#endregion

namespace Dapplo.Confluence.Internals
{
	/// <summary>
	///     All attachment related methods
	/// </summary>
	internal class AttachmentApi : IAttachmentApi
	{
		private readonly IConfluenceClientPlugins _confluenceClientPlugins;
		private readonly IConfluenceClient _confluenceClient;

		internal AttachmentApi(IConfluenceClient confluenceClient)
		{
			_confluenceClient = confluenceClient;
			_confluenceClientPlugins = confluenceClient.Plugins;
		}

		/// <inheritdoc />
		public async Task DeleteAsync(Attachment attachment,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			_confluenceClientPlugins.PromoteContext();
			var contentUri =
				_confluenceClientPlugins.ConfluenceUri.AppendSegments("json", "removeattachmentversion.action")
					.ExtendQuery("pageId", attachment.Container.Id)
					.ExtendQuery("fileName", attachment.Title);

			await contentUri.GetAsAsync<string>(cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<TResponse> GetContentAsync<TResponse>(Attachment attachment,
			CancellationToken cancellationToken = default(CancellationToken))
			where TResponse : class
		{
			_confluenceClientPlugins.PromoteContext();

			var attachmentUri = _confluenceClient.CreateDownloadUri(attachment.Links);
			var response = await attachmentUri.GetAsAsync<HttpResponse<TResponse, string>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse);
			}
			return response.Response;
		}
	}
}