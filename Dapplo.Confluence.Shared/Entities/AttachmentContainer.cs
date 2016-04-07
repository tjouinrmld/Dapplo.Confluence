//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2015-2016 Dapplo
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

using Dapplo.HttpExtensions.Support;

#endregion

namespace Dapplo.Confluence.Entities
{
	/// <summary>
	///     The attachment needs to be uploaded as a multi-part request
	/// </summary>
	[HttpRequest(MultiPart = true)]
	public class AttachmentContainer<T>
	{
		[HttpPart(HttpParts.RequestContent, Order = 1)]
		public string Comment { get; set; }

		[HttpPart(HttpParts.RequestContentType, Order = 1)]
		public string CommentContentType { get; } = "text/plain";

		// Comment information
		[HttpPart(HttpParts.RequestMultipartName, Order = 1)]
		public string CommentName { get; } = "comment";

		[HttpPart(HttpParts.RequestContent, Order = 0)]
		public T Content { get; set; }

		[HttpPart(HttpParts.RequestMultipartName, Order = 0)]
		public string ContentName { get; } = "file";

		[HttpPart(HttpParts.RequestContentType, Order = 0)]
		public string ContentType { get; set; } = "text/plain";

		[HttpPart(HttpParts.RequestMultipartFilename, Order = 0)]
		public string FileName { get; set; }
	}
}