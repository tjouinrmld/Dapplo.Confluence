// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


namespace Dapplo.Confluence.Query
{
    /// <summary>
    ///     An interface for a space clause
    /// </summary>
    public interface ISpaceClause
    {
        /// <summary>
        ///     This allows fluent constructs like Space.InFavouriteSpaces
        /// </summary>
        IFinalClause InFavouriteSpaces { get; }

        /// <summary>
        ///     Negates the expression
        /// </summary>
        ISpaceClause Not { get; }

        /// <summary>
        ///     This allows fluent constructs like Space.In("DEV", "PRODUCTION")
        /// </summary>
        IFinalClause In(params string[] values);

        /// <summary>
        ///     This allows fluent constructs like Space.InFavouriteSpacesAnd("DEV", "PRODUCTION")
        /// </summary>
        IFinalClause InFavouriteSpacesAnd(params string[] values);

        /// <summary>
        ///     This allows fluent constructs like Space.InRecentlyViewedSpaces(10)
        /// </summary>
        IFinalClause InRecentlyViewedSpaces(int limit);

        /// <summary>
        ///     This allows fluent constructs like Space.Is("blub")
        /// </summary>
        IFinalClause Is(string spaceId);
    }
}