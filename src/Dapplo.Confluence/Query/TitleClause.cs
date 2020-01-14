// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using System.Linq;

namespace Dapplo.Confluence.Query
{
    /// <inheritDoc />
    internal class TitleClause : ITitleClause
    {
        private readonly Clause _clause;

        private bool _negate;

        internal TitleClause(Fields titleField)
        {
            _clause = new Clause
            {
                Field = titleField
            };
        }


        /// <inheritDoc />
        public ITitleClause Not
        {
            get
            {
                _negate = !_negate;
                return this;
            }
        }

        /// <inheritDoc />
        public IFinalClause Contains(string value)
        {
            _clause.Operator = Operators.Contains;
            _clause.Value = $"\"{value}\"";
            if (_negate)
            {
                _clause.Negate();
            }
            return _clause;
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
        public IFinalClause In(params string[] values)
        {
            _clause.Operator = Operators.In;
            _clause.Value = "(" + string.Join(", ", values.Select(user => $"\"{user}\"")) + ")";
            if (_negate)
            {
                _clause.Negate();
            }
            return _clause;
        }
    }
}