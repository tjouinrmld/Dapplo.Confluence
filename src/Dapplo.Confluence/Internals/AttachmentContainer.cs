// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using Dapplo.HttpExtensions.Support;

namespace Dapplo.Confluence.Internals
{
    /// <summary>
    ///     The attachment needs to be uploaded as a multi-part request
    /// </summary>
    [HttpRequest(MultiPart = true)]
    internal class AttachmentContainer<T>
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