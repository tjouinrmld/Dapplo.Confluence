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
using Dapplo.HttpExtensions;

#endregion

namespace Dapplo.Confluence.Internals
{
	/// <summary>
	///     All space related extension methods
	/// </summary>
	internal class SpaceApi : ISpaceApi
	{
		private readonly IConfluenceClientPlugins _confluenceClientPlugins;

		internal SpaceApi(IConfluenceClient confluenceClient)
		{
			_confluenceClientPlugins = confluenceClient.Plugins;
		}

		/// <inheritdoc />
		public async Task<Space> CreateAsync(string key, string name, string description, bool isPrivate = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			_confluenceClientPlugins.PromoteContext();
			var space = new Space
			{
				Key = key,
				Name = name,
				Description = new Description
				{
					Plain = new Plain
					{
						Value = description
					}
				}
			};
			var spaceUri = _confluenceClientPlugins.ConfluenceApiUri.AppendSegments("space");
			// Create private space?
			if (isPrivate)
			{
				spaceUri = spaceUri.AppendSegments("_private");
			}
			var response = await spaceUri.PostAsync<HttpResponse<Space, string>>(space, cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse);
			}
			return response.Response;
		}

		/// <inheritdoc />
		public async Task<Space> UpdateAsync(string key, string name, string description, CancellationToken cancellationToken = default(CancellationToken))
		{
			var space = new Space
			{
				Key = key,
				Name = name,
				Description = new Description
				{
					Plain = new Plain
					{
						Value = description
					}
				}
			};
			_confluenceClientPlugins.PromoteContext();
			var spaceUri = _confluenceClientPlugins.ConfluenceApiUri.AppendSegments("space");
			var response = await spaceUri.PutAsync<HttpResponse<Space, string>>(space, cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse);
			}
			return response.Response;
		}

		/// <inheritdoc />
		public async Task<string> DeleteAsync(string key, CancellationToken cancellationToken = default(CancellationToken))
		{
			_confluenceClientPlugins.PromoteContext();
			var spaceUri = _confluenceClientPlugins.ConfluenceApiUri.AppendSegments("space");
			return await spaceUri.DeleteAsync<string>(cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<Space> GetAsync(string spaceKey, CancellationToken cancellationToken = default(CancellationToken))
		{
			_confluenceClientPlugins.PromoteContext();
			var spaceUri = _confluenceClientPlugins.ConfluenceApiUri.AppendSegments("space", spaceKey);
			if ((ConfluenceClientConfig.ExpandGetSpace != null) && (ConfluenceClientConfig.ExpandGetSpace.Length != 0))
			{
				spaceUri = spaceUri.ExtendQuery("expand", string.Join(",", ConfluenceClientConfig.ExpandGetSpace));
			}

			var response = await spaceUri.GetAsAsync<HttpResponse<Space, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response;
		}

		/// <inheritdoc />
		public async Task<IList<Space>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			_confluenceClientPlugins.PromoteContext();
			var spacesUri = _confluenceClientPlugins.ConfluenceApiUri.AppendSegments("space");

			if ((ConfluenceClientConfig.ExpandGetSpace != null) && (ConfluenceClientConfig.ExpandGetSpace.Length != 0))
			{
				spacesUri = spacesUri.ExtendQuery("expand", string.Join(",", ConfluenceClientConfig.ExpandGetSpace));
			}

			var response = await spacesUri.GetAsAsync<HttpResponse<Result<Space>, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response.Results;
		}
	}
}