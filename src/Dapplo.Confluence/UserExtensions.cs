//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016-2018 Dapplo
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

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.Confluence.Entities;
using Dapplo.Confluence.Internals;
using Dapplo.HttpExtensions;

#endregion

namespace Dapplo.Confluence
{
    /// <summary>
    ///     Marker interface for the user domain
    /// </summary>
    public interface IUserDomain : IConfluenceDomain
    {
    }

    /// <summary>
    ///     All user related functionality
    /// </summary>
    public static class UserDomain
    {
        /// <summary>
        ///     Get Anonymous user information, introduced with 6.6
        ///     See: https://docs.atlassian.com/confluence/REST/latest/#user-getAnonymous
        /// </summary>
        /// <param name="confluenceClient">IUserDomain to bind the extension method to</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>User</returns>
        public static async Task<User> GetAnonymousUserAsync(this IUserDomain confluenceClient, CancellationToken cancellationToken = default)
        {
            var myselfUri = confluenceClient.ConfluenceApiUri.AppendSegments("user", "anonymous");
            confluenceClient.Behaviour.MakeCurrent();
            var response = await myselfUri.GetAsAsync<HttpResponse<User, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Get currrent user information, introduced with 6.6
        ///     See: https://docs.atlassian.com/confluence/REST/latest/#user-getCurrent
        /// </summary>
        /// <param name="confluenceClient">IUserDomain to bind the extension method to</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>User</returns>
        public static async Task<User> GetCurrentUserAsync(this IUserDomain confluenceClient, CancellationToken cancellationToken = default)
        {
            confluenceClient.Behaviour.MakeCurrent();
            var myselfUri = confluenceClient.ConfluenceApiUri.AppendSegments("user", "current");

            var response = await myselfUri.GetAsAsync<HttpResponse<User, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Get the groups for a user
        /// </summary>
        /// <param name="confluenceClient">IUserDomain to bind the extension method to</param>
        /// <param name="username">string with username</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>List with Groups</returns>
        public static async Task<IList<Group>> GetGroupsAsync(this IUserDomain confluenceClient, string username, CancellationToken cancellationToken = default)
        {
            var groupUri = confluenceClient.ConfluenceApiUri.AppendSegments("user", "memberof").ExtendQuery("username", username);
            confluenceClient.Behaviour.MakeCurrent();
            var response = await groupUri.GetAsAsync<HttpResponse<Result<Group>, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors()?.Results;
        }

        /// <summary>
        ///     Get user information, introduced with 6.6
        ///     See: https://docs.atlassian.com/confluence/REST/latest/#user-getUser
        /// </summary>
        /// <param name="confluenceClient">IUserDomain to bind the extension method to</param>
        /// <param name="username">string with username</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>user information</returns>
        public static async Task<User> GetUserAsync(this IUserDomain confluenceClient, string username, CancellationToken cancellationToken = default)
        {
            var userUri = confluenceClient.ConfluenceApiUri.AppendSegments("user").ExtendQuery("username", username);
            confluenceClient.Behaviour.MakeCurrent();
            var response = await userUri.GetAsAsync<HttpResponse<User, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }
    }
}