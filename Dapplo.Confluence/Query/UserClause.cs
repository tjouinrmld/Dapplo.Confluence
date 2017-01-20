#region Dapplo 2016 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Confluence
// 
// Dapplo.Confluence is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Confluence is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Confluence. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

#region Usings

using System;
using System.Linq;
using Dapplo.Confluence.Entities;

#endregion

namespace Dapplo.Confluence.Query
{
	/// <summary>
	/// The clause for user related fields
	/// </summary>
	public interface IUserClause
	{
		/// <summary>
		///     This allows fluent constructs like Creator.IsCurrentUser
		/// </summary>
		/// <returns>IFinalClause</returns>
		IFinalClause IsCurrentUser { get; }

		/// <summary>
		///     Negates the expression
		/// </summary>
		/// <returns>IFinalClause</returns>
		IUserClause Not { get; }

		/// <summary>
		///     This allows fluent constructs like Creator.Is("smith")
		/// </summary>
		/// <returns>IFinalClause</returns>
		IFinalClause Is(string user);

		/// <summary>
		///     This allows fluent constructs like Creator.Is(user1)
		/// </summary>
		/// <returns>IFinalClause</returns>
		IFinalClause Is(User user);

		/// <summary>
		///     This allows fluent constructs like Creator.In("smith", "squarepants")
		/// </summary>
		/// <param name="users">array with usernames</param>
		/// <returns>IFinalClause</returns>
		IFinalClause In(params string[] users);

		/// <summary>
		///     This allows fluent constructs like Creator.In("smith", "squarepants")
		/// </summary>
		/// <param name="users">array with User objects</param>
		/// <returns>IFinalClause</returns>
		IFinalClause In(params User[] users);

		/// <summary>
		///     This allows fluent constructs like Creator.InCurrentUserAnd("smith", "squarepants")
		/// </summary>
		/// <param name="users">array with usernames</param>
		/// <returns>IFinalClause</returns>
		IFinalClause InCurrentUserAnd(params string[] users);

		/// <summary>
		///     This allows fluent constructs like Creator.InCurrentUserAnd("smith", "squarepants")
		/// </summary>
		/// <param name="users">User array</param>
		/// <returns>IFinalClause</returns>
		IFinalClause InCurrentUserAnd(params User[] users);
	}

	/// <inheritDoc />
	internal class UserClause : IUserClause
	{
		private readonly Clause _clause;
		private readonly Fields[] _allowedFields = { Fields.Creator, Fields.Contributor, Fields.Mention, Fields.Watcher, Fields.Favourite };
		private bool _negate;

		internal UserClause(Fields userField)
		{
			if (!_allowedFields.Any(field => userField == field))
			{
				throw new InvalidOperationException("Can't add function for the field {Field}");
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