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
using Dapplo.HttpExtensions;

#endregion

namespace Dapplo.Confluence
{
	/// <summary>
	///     The is the interface to the client plugins (extension) functionality of the Confluence API
	/// </summary>
	public interface IConfluenceClientPlugins
	{
		/// <summary>
		///     Some plugins might need to access the "parent" IConfluenceClient
		/// </summary>
		IConfluenceClient Client { get; }

		/// <summary>
		///     The URI for API calls, this is used to create the specific REST calls
		/// </summary>
		Uri ConfluenceApiUri { get; }

		/// <summary>
		///     This is the base URI to access the server, used when attachements need to be downloaed
		/// </summary>
		Uri ConfluenceUri { get; }

		/// <summary>
		///     The Dapplo.HttpExtensions HttpBehaviour which takes care of authentication and other HTTP relative information.
		/// </summary>
		IHttpBehaviour HttpBehaviour { get; }

		/// <summary>
		///     Makes sure that the current Task has the correct context to make a HTTP Request to confluence
		/// </summary>
		void PromoteContext();
	}
}