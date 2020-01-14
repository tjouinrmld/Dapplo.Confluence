// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;

namespace Dapplo.Confluence.Query
{
    /// <summary>
    ///     A clause for simple values like ancestor, Id, label, space and more
    /// </summary>
    internal class TextClause : ITextClause
    {
        private readonly Fields[] _allowedFields = { Fields.Text, Fields.Title };
        private readonly Clause _clause;

        private bool _negate;

        internal TextClause(Fields textField)
        {
            if (_allowedFields.All(field => textField != field))
            {
                throw new InvalidOperationException($"Can't add function for the field {textField}");
            }
            _clause = new Clause
            {
                Field = textField
            };
        }


        /// <inheritDoc />
        public ITextClause Not
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
    }
}