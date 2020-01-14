// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;

namespace Dapplo.Confluence.Query
{
    /// <summary>
    ///     A clause for content identifying values like ancestor, content, id and parent
    /// </summary>
    internal class ContentClause : IContentClause
    {
        private readonly Fields[] _allowedFields = { Fields.Ancestor, Fields.Content, Fields.Id, Fields.Parent };
        private readonly Clause _clause;

        private bool _negate;

        internal ContentClause(Fields contentField)
        {
            if (_allowedFields.All(field => contentField != field))
            {
                throw new InvalidOperationException($"Can't add function for the field {contentField}");
            }
            _clause = new Clause
            {
                Field = contentField
            };
        }


        /// <inheritDoc />
        public IContentClause Not
        {
            get
            {
                _negate = !_negate;
                return this;
            }
        }

        /// <inheritDoc />
        public IFinalClause Is(long value)
        {
            _clause.Operator = Operators.EqualTo;
            _clause.Value = value.ToString();
            if (_negate)
            {
                _clause.Negate();
            }
            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause InRecentlyViewedContent(int limit, int offset = 0)
        {
            _clause.Operator = Operators.In;
            var skip = offset != 0 ? $",{offset}" : "";
            _clause.Value = $"recentlyViewedContent({limit}{skip})";
            if (_negate)
            {
                _clause.Negate();
            }
            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause In(params long[] values)
        {
            _clause.Operator = Operators.In;
            _clause.Value = "(" + string.Join(", ", values) + ")";
            if (_negate)
            {
                _clause.Negate();
            }
            return _clause;
        }
    }
}