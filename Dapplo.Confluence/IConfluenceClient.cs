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

using System;
using Dapplo.Confluence.Entities;

namespace Dapplo.Confluence
{
	/// <summary>
	///     The is the interface to the base client functionality of the Confluence API
	/// </summary>
	public interface IConfluenceClient
	{
		/// <summary>
		///     Extensions of the client dock to this property, so typing "confluenceClient.Plugins." should show your extension.
		/// </summary>
		IConfluenceClientPlugins Plugins { get; }

		/// <summary>
		///     Enables basic authentication for every request following this call
		/// </summary>
		/// <param name="user">string with the confluence user</param>
		/// <param name="password">string with the password for the confluence user</param>
		void SetBasicAuthentication(string user, string password);

		/// <summary>
		/// Retrieve the WebUi as Uri from the supplied links object
		/// </summary>
		/// <param name="links">Links</param>
		/// <returns>Uri</returns>
		Uri WebUiUri(Links links);

		/// <summary>
		/// Retrieve the TinyUi as Uri from the supplied links object
		/// </summary>
		/// <param name="links">Links</param>
		/// <returns>Uri</returns>
		Uri TinyUiUri(Links links);

		/// <summary>
		/// Retrieve the Download as Uri from the supplied links object
		/// </summary>
		/// <param name="links">Links</param>
		/// <returns>Uri</returns>
		Uri DownloadUri(Links links);

		/// <summary>
		/// The attachment domain
		/// </summary>
		IAttachmentApi Attachment { get; }

		/// <summary>
		/// The content domain
		/// </summary>
		IContentApi Content { get; }

		/// <summary>
		/// The user domain
		/// </summary>
		IUserApi User { get; }

		/// <summary>
		/// The space domain
		/// </summary>
		ISpaceApi Space { get; }
	}
}