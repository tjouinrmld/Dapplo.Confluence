// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using Dapplo.Confluence.Entities;

namespace Dapplo.Confluence.Query
{
    /// <summary>
    ///     The clause for user related fields
    /// </summary>
    public interface IUserClause
    {
        /// <summary>
        ///     This allows fluent constructs like Creator.IsCurrentUser
        /// </summary>
        /// <returns>IFinalClause</returns>
        IFinalClause IsCurrentUser { get; }

        /// <summary>
        ///     Negates the expression
        /// </summary>
        /// <returns>IFinalClause</returns>
        IUserClause Not { get; }

        /// <summary>
        ///     This allows fluent constructs like Creator.In("smith", "squarepants")
        /// </summary>
        /// <param name="users">array with usernames</param>
        /// <returns>IFinalClause</returns>
        IFinalClause In(params string[] users);

        /// <summary>
        ///     This allows fluent constructs like Creator.In("smith", "squarepants")
        /// </summary>
        /// <param name="users">array with User objects</param>
        /// <returns>IFinalClause</returns>
        IFinalClause In(params User[] users);

        /// <summary>
        ///     This allows fluent constructs like Creator.InCurrentUserAnd("smith", "squarepants")
        /// </summary>
        /// <param name="users">array with usernames</param>
        /// <returns>IFinalClause</returns>
        IFinalClause InCurrentUserAnd(params string[] users);

        /// <summary>
        ///     This allows fluent constructs like Creator.InCurrentUserAnd("smith", "squarepants")
        /// </summary>
        /// <param name="users">User array</param>
        /// <returns>IFinalClause</returns>
        IFinalClause InCurrentUserAnd(params User[] users);

        /// <summary>
        ///     This allows fluent constructs like Creator.Is("smith")
        /// </summary>
        /// <returns>IFinalClause</returns>
        IFinalClause Is(string user);

        /// <summary>
        ///     This allows fluent constructs like Creator.Is(user1)
        /// </summary>
        /// <returns>IFinalClause</returns>
        IFinalClause Is(User user);
    }
}