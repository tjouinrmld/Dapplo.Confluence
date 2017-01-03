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
	///     All user related functionality
	/// </summary>
	internal class UserApi : IUserApi
	{
		private readonly IConfluenceClientPlugins _confluenceClientPlugins;

		internal UserApi(IConfluenceClient confluenceClient)
		{
			_confluenceClientPlugins = confluenceClient.Plugins;
		}

		/// <inheritdoc />
		public async Task<User> GetCurrentUserAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			_confluenceClientPlugins.PromoteContext();
			var myselfUri = _confluenceClientPlugins.ConfluenceApiUri.AppendSegments("user", "current");

			var response = await myselfUri.GetAsAsync<HttpResponse<User, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response;
		}

		/// <inheritdoc />
		public async Task<User> GetAnonymousUserAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var myselfUri = _confluenceClientPlugins.ConfluenceApiUri.AppendSegments("user", "anonymous");
			_confluenceClientPlugins.PromoteContext();
			var response = await myselfUri.GetAsAsync<HttpResponse<User, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response;
		}

		/// <inheritdoc />
		public async Task<User> GetUserAsync(string username, CancellationToken cancellationToken = default(CancellationToken))
		{
			var userUri = _confluenceClientPlugins.ConfluenceApiUri.AppendSegments("user").ExtendQuery("username", username);
			_confluenceClientPlugins.PromoteContext();
			var response = await userUri.GetAsAsync<HttpResponse<User, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response;
		}

		/// <inheritdoc />
		public async Task<IList<Group>> GetGroupsAsync(string username, CancellationToken cancellationToken = default(CancellationToken))
		{
			var groupUri = _confluenceClientPlugins.ConfluenceApiUri.AppendSegments("user", "memberof").ExtendQuery("username", username);
			_confluenceClientPlugins.PromoteContext();
			var response = await groupUri.GetAsAsync<HttpResponse<Result<Group>, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response.Results;
		}
	}
}