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

using System.Linq;
using Dapplo.HttpExtensions.Extensions;

namespace Dapplo.Confluence.Query
{
    /// <inheritDoc />
    internal class TypeClause : ITypeClause
    {
        private readonly Clause _clause;
        private bool _negate;

        internal TypeClause()
        {
            _clause = new Clause
            {
                Field = Fields.Type
            };
        }

        /// <inheritDoc />
        public IFinalClause IsAttachment
        {
            get
            {
                _clause.Operator = Operators.EqualTo;
                _clause.Value = ContentTypes.Attachment.EnumValueOf();
                if (_negate)
                {
                    _clause.Negate();
                }
                return _clause;
            }
        }

        /// <inheritDoc />
        public IFinalClause IsPage
        {
            get
            {
                _clause.Operator = Operators.EqualTo;
                _clause.Value = ContentTypes.Page.EnumValueOf();
                if (_negate)
                {
                    _clause.Negate();
                }
                return _clause;
            }
        }

        /// <inheritDoc />
        public IFinalClause IsBlogPost
        {
            get
            {
                _clause.Operator = Operators.EqualTo;
                _clause.Value = ContentTypes.BlogPost.EnumValueOf();
                if (_negate)
                {
                    _clause.Negate();
                }
                return _clause;
            }
        }

        /// <inheritDoc />
        public IFinalClause IsComment
        {
            get
            {
                _clause.Operator = Operators.EqualTo;
                _clause.Value = ContentTypes.Comment.EnumValueOf();
                if (_negate)
                {
                    _clause.Negate();
                }
                return _clause;
            }
        }

        /// <inheritDoc />
        public ITypeClause Not
        {
            get
            {
                _negate = !_negate;
                return this;
            }
        }

        /// <inheritDoc />
        public IFinalClause In(params ContentTypes[] contentTypes)
        {
            _clause.Operator = Operators.In;
            _clause.Value = "(" + string.Join(", ", contentTypes.Select(type => type.EnumValueOf())) + ")";
            if (_negate)
            {
                _clause.Negate();
            }
            return _clause;
        }
    }
}