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

#endregion

namespace Dapplo.Confluence.Query
{
	/// <summary>
	/// An interface for a content id based clauses
	/// </summary>
	public interface IContentClause
	{
	/// <summary>
		///     Negates the expression
		/// </summary>
		IContentClause Not { get; }

		/// <summary>
		///     This allows fluent constructs like Id.Is(12345)
		/// </summary>
		IFinalClause Is(int id);


		/// <summary>
		///     This allows fluent constructs like Id.InRecentlyViewedContent(10, 20)
		/// </summary>
		IFinalClause InRecentlyViewedContent(int limit, int offset = 0);

		/// <summary>
		///     This allows fluent constructs like Id.In(1234, 45678)
		/// </summary>
		IFinalClause In(params int[] values);
	}

	/// <summary>
	/// A clause for content identifying values like ancestor, content, id and parent
	/// </summary>
	internal class ContentClause : IContentClause
	{
		private readonly Clause _clause;

		private readonly Fields[] _allowedFields = { Fields.Ancestor, Fields.Content, Fields.Id, Fields.Parent};

		private bool _negate;

		internal ContentClause(Fields contentField)
		{
			if (!_allowedFields.Any(field => contentField == field))
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
		public IFinalClause Is(int value)
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
		public IFinalClause In(params int[] values)
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