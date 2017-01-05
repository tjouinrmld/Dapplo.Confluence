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

using System.Linq;

#endregion

namespace Dapplo.Confluence.Query
{
	/// <summary>
	/// An interface for a space clause
	/// </summary>
	public interface ISpaceClause
	{
	/// <summary>
		///     Negates the expression
		/// </summary>
		ISpaceClause Not { get; }

		/// <summary>
		///     This allows fluent constructs like Space.Is("blub")
		/// </summary>
		IFinalClause Is(string spaceId);

		/// <summary>
		///     This allows fluent constructs like Space.InRecentlyViewedSpaces(10)
		/// </summary>
		IFinalClause InRecentlyViewedSpaces(int limit);

		/// <summary>
		///     This allows fluent constructs like Space.In("DEV", "PRODUCTION")
		/// </summary>
		IFinalClause In(params string[] values);

		/// <summary>
		///     This allows fluent constructs like Space.InFavouriteSpacesAnd("DEV", "PRODUCTION")
		/// </summary>
		IFinalClause InFavouriteSpacesAnd(params string[] values);

		/// <summary>
		///     This allows fluent constructs like Space.InFavouriteSpaces
		/// </summary>
		IFinalClause InFavouriteSpaces { get; }
	}

	/// <summary>
	/// A clause for space
	/// </summary>
	public class SpaceClause : ISpaceClause
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