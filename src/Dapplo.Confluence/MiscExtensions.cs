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
using Dapplo.Confluence.Internals;
using Dapplo.HttpExtensions;

#endregion

namespace Dapplo.Confluence
{
    /// <summary>
    ///     Marker interface for the methods which have no direct clear domain
    /// </summary>
    public interface IMiscDomain : IConfluenceDomain
    {
        
    }

    /// <summary>
    ///     All extensions which have no direct clear domain
    /// </summary>
    public static class MiscExtensions
    {
        /// <summary>
        ///     Retrieve the picture for the supplied Picture entity
        /// </summary>
        /// <param name="confluenceClient">IMiscDomain to bind the extension method to</param>
        /// <typeparam name="TResponse">the type to return the result into. e.g. Bitmap,BitmapSource or MemoryStream</typeparam>
        /// <param name="picture">Picture from User, Space, History etc</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Bitmap,BitmapSource or MemoryStream (etc) depending on TResponse</returns>
        public static async Task<TResponse> GetPictureAsync<TResponse>(this IMiscDomain confluenceClient, Picture picture,
            CancellationToken cancellationToken = default(CancellationToken))
            where TResponse : class
        {
            confluenceClient.Behaviour.MakeCurrent();

            var pictureUriBuilder = new UriBuilder(
                confluenceClient.ConfluenceUri.Scheme,
                confluenceClient.ConfluenceUri.Host,
                confluenceClient.ConfluenceUri.Port);
            var pictureUri = new Uri(pictureUriBuilder.Uri.AbsoluteUri.TrimEnd('/') + picture.Path);
            var response = await pictureUri.GetAsAsync<HttpResponse<TResponse, string>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }
    }
}