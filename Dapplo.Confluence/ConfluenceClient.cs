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
using Dapplo.Confluence.Entities;
using Dapplo.Confluence.Internals;
using Dapplo.HttpExtensions;

#endregion

namespace Dapplo.Confluence
{
	/// <summary>
	///     A Confluence client build by using Dapplo.HttpExtensions
	/// </summary>
	public class ConfluenceClient : IConfluenceClient, IConfluenceClientPlugins
	{
		/// <summary>
		///     Store the specific HttpBehaviour, which contains a IHttpSettings and also some additional logic for making a
		///     HttpClient which works with Confluence
		/// </summary>
		private readonly IHttpBehaviour _behaviour;

		/// <summary>
		///     Password for the basic authentication
		/// </summary>
		private string _password;

		/// <summary>
		///     User for the basic authentication
		/// </summary>
		private string _user;

		/// <summary>
		///     Create the ConfluenceApi object, here the HttpClient is configured
		/// </summary>
		/// <param name="confluenceUri">Base URL, e.g. https://yourConfluenceserver</param>
		/// <param name="httpSettings">IHttpSettings or null for default</param>
		private ConfluenceClient(Uri confluenceUri, IHttpSettings httpSettings = null)
		{
			if (confluenceUri == null)
			{
				throw new ArgumentNullException(nameof(confluenceUri));
			}
			ConfluenceUri = confluenceUri;
			ConfluenceApiUri = confluenceUri.AppendSegments("rest", "api");

			_behaviour = ConfigureBehaviour(new HttpBehaviour(), httpSettings);

			Content = new ContentApi(this);
			User = new UserApi(this);
			Space = new SpaceApi(this);
			Attachment = new AttachmentApi(this);
		}

		/// <summary>
		///     Plugins dock to this property by implementing an extension method to IConfluenceClientPlugins
		/// </summary>
		public IConfluenceClientPlugins Plugins => this;

		/// <summary>
		///     Plugins can use this interface to get back to the main
		/// </summary>
		public IConfluenceClient Client => this;

		/// <summary>
		///     Set Basic Authentication for the current client
		/// </summary>
		/// <param name="user">username</param>
		/// <param name="password">password</param>
		public void SetBasicAuthentication(string user, string password)
		{
			_user = user;
			_password = password;
		}

		/// <summary>
		///     The IHttpBehaviour for this Confluence instance
		/// </summary>
		public IHttpBehaviour HttpBehaviour => _behaviour;

		/// <summary>
		///     This makes sure that the HttpBehavior is promoted for the following Http call.
		/// </summary>
		public void PromoteContext()
		{
			_behaviour.MakeCurrent();
		}

		/// <summary>
		///     The base URI for your Confluence server api calls
		/// </summary>
		public Uri ConfluenceApiUri { get; }

		/// <summary>
		///     The base URI for your Confluence server downloads
		/// </summary>
		public Uri ConfluenceUri { get; }

		/// <summary>
		///     Factory method to create a ConfluenceClient
		/// </summary>
		/// <param name="confluenceUri">Uri to your confluence server</param>
		/// <param name="httpSettings">IHttpSettings used if you need specific settings</param>
		/// <returns>IConfluenceClient</returns>
		public static IConfluenceClient Create(Uri confluenceUri, IHttpSettings httpSettings = null)
		{
			return new ConfluenceClient(confluenceUri, httpSettings);
		}

		/// <summary>
		///     Helper method to configure the IChangeableHttpBehaviour
		/// </summary>
		/// <param name="behaviour">IChangeableHttpBehaviour</param>
		/// <param name="httpSettings">IHttpSettings</param>
		/// <returns>the behaviour, but configured as IHttpBehaviour </returns>
		private IHttpBehaviour ConfigureBehaviour(IChangeableHttpBehaviour behaviour, IHttpSettings httpSettings = null)
		{
			behaviour.HttpSettings = httpSettings ?? HttpExtensionsGlobals.HttpSettings;
			behaviour.OnHttpRequestMessageCreated = httpMessage =>
			{
				httpMessage?.Headers.TryAddWithoutValidation("X-Atlassian-Token", "no-check");
				if (!string.IsNullOrEmpty(_user) && (_password != null))
				{
					httpMessage?.SetBasicAuthorization(_user, _password);
				}
				return httpMessage;
			};
			return behaviour;
		}

		/// <inheritdoc />
		public IAttachmentApi Attachment { get; }

		/// <inheritdoc />
		public IContentApi Content { get; }

		/// <inheritdoc />
		public IUserApi User { get; }

		/// <inheritdoc />
		public ISpaceApi Space { get; }

		/// <inheritdoc />
		public Uri WebUiUri(Links links)
		{
			if (links != null && links.Base == null)
			{
				links.Base = ConfluenceUri;
			}
			return links?.Base.AppendSegments(links.WebUi);
		}

		/// <inheritdoc />
		public Uri TinyUiUri(Links links)
		{
			if (links != null && links.Base == null)
			{
				links.Base = ConfluenceUri;
			}
			return links?.Base.AppendSegments(links.TinyUi);
		}

		/// <inheritdoc />
		public Uri DownloadUri(Links links)
		{
			if (links.Base == null)
			{
				links.Base = ConfluenceUri;
			}
			return links?.Base.AppendSegments(links.Download);
		}
	}
}