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
	///     All space related methods
	/// </summary>
	public interface ISpaceApi
	{
		/// <summary>
		///     Create a space
		/// </summary>
		/// <param name="key">Key for the space</param>
		/// <param name="name">Name for the space</param>
		/// <param name="description">Description for the space</param>
		/// <param name="isPrivate">true if the space needs to be private</param>
		/// <param name="cancellationToken"></param>
		/// <returns>created Space</returns>
		Task<Space> CreateAsync(string key, string name, string description, bool isPrivate = false, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Update a space
		/// </summary>
		/// <param name="key">Key for the space</param>
		/// <param name="name">Name for the space</param>
		/// <param name="description">Description for the space</param>
		/// <param name="cancellationToken"></param>
		/// <returns>updated Space</returns>
		Task<Space> UpdateAsync(string key, string name, string description, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Delete a space
		/// </summary>
		/// <param name="key">Key for the space</param>
		/// <param name="cancellationToken"></param>
		/// <returns>Long task for deleting the space</returns>
		Task<string> DeleteAsync(string key, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Get Space information see <a href="https://docs.atlassian.com/confluence/REST/latest/#d3e164">here</a>
		/// </summary>
		/// <param name="spaceKey">the space key</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Space</returns>
		Task<Space> GetAsync(string spaceKey, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Get Spaces see <a href="https://docs.atlassian.com/confluence/REST/latest/#d3e164">here</a>
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>List of Spaces</returns>
		Task<IList<Space>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
	}
}