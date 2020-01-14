// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;

namespace Dapplo.Confluence.Query
{
    /// <summary>
    ///     A clause for the space field
    /// </summary>
    internal class SpaceClause : ISpaceClause
    {
        private readonly Clause _clause;

        private bool _negate;

        internal SpaceClause()
        {
            _clause = new Clause
            {
                Field = Fields.Space
            };
        }


        /// <inheritDoc />
        public ISpaceClause Not
        {
            get
            {
                _negate = !_negate;
                return this;
            }
        }

        /// <inheritDoc />
        public IFinalClause Is(string value)
        {
            _clause.Operator = Operators.EqualTo;
            _clause.Value = $"\"{value}\"";
            if (_negate)
            {
                _clause.Negate();
            }
            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause InRecentlyViewedSpaces(int limit)
        {
            _clause.Operator = Operators.In;
            _clause.Value = $"recentlyViewedSpaces({limit})";
            if (_negate)
            {
                _clause.Negate();
            }
            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause In(params string[] values)
        {
            _clause.Operator = Operators.In;
            _clause.Value = "(" + string.Join(", ", values.Select(value => $"\"{value}\"")) + ")";
            if (_negate)
            {
                _clause.Negate();
            }
            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause InFavouriteSpacesAnd(params string[] values)
        {
            _clause.Operator = Operators.In;
            _clause.Value = "(favouriteSpaces(), " + string.Join(", ", values.Select(value => $"\"{value}\"")) + ")";
            if (_negate)
            {
                _clause.Negate();
            }
            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause InFavouriteSpaces
        {
            get
            {
                _clause.Operator = Operators.In;
                _clause.Value = "(inFavouriteSpaces())";
                if (_negate)
                {
                    _clause.Negate();
                }
                return _clause;
            }
        }
    }
}