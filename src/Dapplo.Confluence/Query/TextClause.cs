//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016-2020 Dapplo
//
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
//
//  This file is part of Dapplo.Confluence
//
//  Dapplo.Confluence is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Dapplo.Confluence is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Confluence. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

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