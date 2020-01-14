// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


namespace Dapplo.Confluence.Query
{
    /// <summary>
    /// Interface for the CQL type clauses
    /// </summary>
    public interface ITypeClause
    {
        /// <summary>
        ///     This allows fluent constructs like Type.IsAttachment
        /// </summary>
        IFinalClause IsAttachment { get; }

        /// <summary>
        ///     This allows fluent constructs like Type.IsBlogPost
        /// </summary>
        IFinalClause IsBlogPost { get; }

        /// <summary>
        ///     This allows fluent constructs like Type.IsComment
        /// </summary>
        IFinalClause IsComment { get; }

        /// <summary>
        ///     This allows fluent constructs like Type.IsPage
        /// </summary>
        IFinalClause IsPage { get; }

        /// <summary>
        ///     Negates the expression
        /// </summary>
        ITypeClause Not { get; }

        /// <summary>
        ///     Test if the type is one of the specified types
        /// </summary>
        /// <param name="contentTypes">array of types</param>
        /// <returns>IFinalClause</returns>
        IFinalClause In(params ContentTypes[] contentTypes);
    }
}