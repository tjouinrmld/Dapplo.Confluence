// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


namespace Dapplo.Confluence.Query
{
    /// <summary>
    ///     An interface for a content id based clauses
    /// </summary>
    public interface IContentClause
    {
        /// <summary>
        ///     Negates the expression
        /// </summary>
        IContentClause Not { get; }

        /// <summary>
        ///     This allows fluent constructs like Id.In(1234, 45678)
        /// </summary>
        IFinalClause In(params long[] values);


        /// <summary>
        ///     This allows fluent constructs like Id.InRecentlyViewedContent(10, 20)
        /// </summary>
        IFinalClause InRecentlyViewedContent(int limit, int offset = 0);

        /// <summary>
        ///     This allows fluent constructs like Id.Is(12345)
        /// </summary>
        IFinalClause Is(long id);
    }
}