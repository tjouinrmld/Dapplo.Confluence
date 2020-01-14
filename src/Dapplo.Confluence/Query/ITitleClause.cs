// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


namespace Dapplo.Confluence.Query
{
    /// <summary>
    ///     Interface for the CQL title clauses
    /// </summary>
    public interface ITitleClause
    {
        /// <summary>
        ///     Negates the expression
        /// </summary>
        ITitleClause Not { get; }

        /// <summary>
        ///     This allows fluent constructs like Text.Contains(customernumber)
        /// </summary>
        IFinalClause Contains(string value);

        /// <summary>
		///     This allows fluent constructs like Title.In("DEV", "PRODUCTION")
		/// </summary>
		IFinalClause In(params string[] values);

        /// <summary>
        ///     This allows fluent constructs like Title.Is("DEV")
        /// </summary>
        IFinalClause Is(string value);
    }
}