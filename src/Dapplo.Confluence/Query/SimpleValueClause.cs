// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;

namespace Dapplo.Confluence.Query
{
    /// <summary>
    ///     A clause for simple fields like container, macro and label
    /// </summary>
    internal class SimpleValueClause : ISimpleValueClause
    {
        private readonly Fields[] _allowedFields = { Fields.Container, Fields.Macro, Fields.Label };
        private readonly Clause _clause;

        private bool _negate;

        internal SimpleValueClause(Fields simpleField)
        {
            if (_allowedFields.All(field => simpleField != field))
            {
                throw new InvalidOperationException($"Can't add function for the field {simpleField}");
            }
            _clause = new Clause
            {
                Field = simpleField
            };
        }


        /// <inheritDoc />
        public ISimpleValueClause Not
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