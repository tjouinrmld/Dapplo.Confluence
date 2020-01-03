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
using Dapplo.Confluence.Entities;

namespace Dapplo.Confluence.Query
{
    /// <inheritDoc />
    internal class UserClause : IUserClause
    {
        private readonly Fields[] _allowedFields = { Fields.Creator, Fields.Contributor, Fields.Mention, Fields.Watcher, Fields.Favourite };
        private readonly Clause _clause;
        private bool _negate;

        internal UserClause(Fields userField)
        {
            if (_allowedFields.All(field => userField != field))
            {
                throw new InvalidOperationException($"Can't add function for the field {userField}");
            }
            _clause = new Clause
            {
                Field = userField
            };
        }


        /// <inheritDoc />
        public IFinalClause IsCurrentUser
        {
            get
            {
                _clause.Value = "currentUser()";
                if (_negate)
                {
                    _clause.Negate();
                }
                return _clause;
            }
        }

        /// <inheritDoc />
        public IUserClause Not
        {
            get
            {
                _negate = !_negate;
                return this;
            }
        }

        /// <inheritDoc />
        public IFinalClause Is(string user)
        {
            _clause.Operator = Operators.EqualTo;
            _clause.Value = $"\"{user}\"";
            if (_negate)
            {
                _clause.Negate();
            }
            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause Is(User user)
        {
            return Is(user.Username);
        }

        /// <inheritDoc />
        public IFinalClause In(params string[] users)
        {
            _clause.Operator = Operators.In;
            _clause.Value = "(" + string.Join(", ", users.Select(user => $"\"{user}\"")) + ")";
            if (_negate)
            {
                _clause.Negate();
            }
            return _clause;
        }


        /// <inheritDoc />
        public IFinalClause In(params User[] users)
        {
            return In(users.Select(user => user.Username).ToArray());
        }

        /// <inheritDoc />
        public IFinalClause InCurrentUserAnd(params string[] users)
        {
            _clause.Operator = Operators.In;
            _clause.Value = "(currentUser(), " + string.Join(", ", users.Select(user => $"\"{user}\"")) + ")";
            if (_negate)
            {
                _clause.Negate();
            }
            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause InCurrentUserAnd(params User[] users)
        {
            return InCurrentUserAnd(users.Select(user => user.Username).ToArray());
        }
    }
}