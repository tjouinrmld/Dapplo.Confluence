// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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