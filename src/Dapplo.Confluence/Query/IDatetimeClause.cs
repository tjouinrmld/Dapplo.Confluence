// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


namespace Dapplo.Confluence.Query
{
    /// <summary>
    ///     An interface for a date time calculations clause
    /// </summary>
    public interface IDatetimeClause
    {
        /// <summary>
        /// Used in CQL to test if a datetime is after
        /// </summary>
		IDatetimeClauseWithoutValue After { get; }
        /// <summary>
        /// Used in CQL to test if a datetime is after or on
        /// </summary>
        IDatetimeClauseWithoutValue AfterOrOn { get; }
        /// <summary>
        /// Used in CQL to test if a datetime is before
        /// </summary>
        IDatetimeClauseWithoutValue Before { get; }
        /// <summary>
        /// Used in CQL to test if a datetime is before or on
        /// </summary>
        IDatetimeClauseWithoutValue BeforeOrOn { get; }
        /// <summary>
        /// Used in CQL to test if a datetime is on
        /// </summary>
        IDatetimeClauseWithoutValue On { get; }
    }
}