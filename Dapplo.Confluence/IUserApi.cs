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

#endregion

namespace Dapplo.Confluence
{
	/// <summary>
	///     The interface to all user related functionality
	/// </summary>
	public interface IUserApi
	{
		/// <summary>
		///     Get currrent user information, introduced with 6.6
		///     See: https://docs.atlassian.com/confluence/REST/latest/#user-getCurrent
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>User</returns>
		Task<User> GetCurrentUserAsync(CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Get Anonymous user information, introduced with 6.6
		///     See: https://docs.atlassian.com/confluence/REST/latest/#user-getAnonymous
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>User</returns>
		Task<User> GetAnonymousUserAsync(CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Get user information, introduced with 6.6
		///     See: https://docs.atlassian.com/confluence/REST/latest/#user-getUser
		/// </summary>
		/// <param name="username">string with username</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>user information</returns>
		Task<User> GetUserAsync(string username, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Get the groups for a user
		/// </summary>
		/// <param name="username">string with username</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>List with Groups</returns>
		Task<IList<Group>> GetGroupsAsync(string username, CancellationToken cancellationToken = default(CancellationToken));
	}
}