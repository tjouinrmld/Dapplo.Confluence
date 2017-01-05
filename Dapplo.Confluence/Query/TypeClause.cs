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
using Dapplo.HttpExtensions.Extensions;

#endregion

namespace Dapplo.Confluence.Query
{
	public interface ITypeClause
	{
		/// <summary>
		///     This allows fluent constructs like Type.IsAttachment
		/// </summary>
		IFinalClause IsAttachment { get; }

		/// <summary>
		///     This allows fluent constructs like Type.IsPage
		/// </summary>
		IFinalClause IsPage { get; }

		/// <summary>
		///     This allows fluent constructs like Type.IsBlogPost
		/// </summary>
		IFinalClause IsBlogPost { get; }

		/// <summary>
		///     This allows fluent constructs like Type.IsComment
		/// </summary>
		IFinalClause IsComment { get; }

		/// <summary>
		///     Negates the expression
		/// </summary>
		ITypeClause Not { get; }

		IFinalClause In(params Types[] type);
	}

	///
	public class TypeClause : ITypeClause
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

		/// <summary>
		///     check if the type of the content is an attachment
		/// </summary>
		/// <returns>IFinalClause</returns>
		public IFinalClause IsAttachment
		{
			get
			{
				_clause.Operator = Operators.EqualTo;
				_clause.Value = Types.Attachment.EnumValueOf();
				if (_negate)
				{
					_clause.Negate();
				}
				return _clause;
			}
		}

		/// <summary>
		///     check if the type of the content is a page
		/// </summary>
		/// <returns>IFinalClause</returns>
		public IFinalClause IsPage
		{
			get
			{
				_clause.Operator = Operators.EqualTo;
				_clause.Value = Types.Page.EnumValueOf();
				if (_negate)
				{
					_clause.Negate();
				}
				return _clause;
			}
		}

		/// <summary>
		///     check if the type of the content is a blogpost
		/// </summary>
		/// <returns>IFinalClause</returns>
		public IFinalClause IsBlogPost
		{
			get
			{
				_clause.Operator = Operators.EqualTo;
				_clause.Value = Types.BlogPost.EnumValueOf();
				if (_negate)
				{
					_clause.Negate();
				}
				return _clause;
			}
		}

		/// <summary>
		///     check if the type of the content is a comment
		/// </summary>
		/// <returns>IFinalClause</returns>
		public IFinalClause IsComment
		{
			get
			{
				_clause.Operator = Operators.EqualTo;
				_clause.Value = Types.Comment.EnumValueOf();
				if (_negate)
				{
					_clause.Negate();
				}
				return _clause;
			}
		}

		public ITypeClause Not
		{
			get
			{
				_negate = !_negate;
				return this;
			}
		}

		/// <summary>
		///     Test if the type of the content is one of the specified types
		/// </summary>
		/// <param name="types">Types</param>
		/// <returns>IFinalClause</returns>
		public IFinalClause In(params Types[] types)
		{
			_clause.Operator = Operators.In;
			_clause.Value = "(" + string.Join(", ", types.Select(type => type.EnumValueOf())) + ")";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}
	}
}