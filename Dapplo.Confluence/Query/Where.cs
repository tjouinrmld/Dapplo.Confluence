//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016 Dapplo
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

#region using

using System.Linq;

#endregion

namespace Dapplo.Confluence.Query
{
	/// <summary>
	///     Factory method for CQL where clauses
	/// </summary>
	public static class Where
	{
		/// <summary>
		///     Create a clause for the created field
		/// </summary>
		public static IDatetimeClause Created => new DatetimeClause(Fields.Created);

		/// <summary>
		///     Create a clause for the lastmodified field
		/// </summary>
		public static IDatetimeClause LastModified => new DatetimeClause(Fields.LastModified);


		/// <summary>
		///     Create a clause for the Space field
		/// </summary>
		public static ISpaceClause Space => new SpaceClause();

		/// <summary>
		///     Create a clause for the type field
		/// </summary>
		public static ITypeClause Type => new TypeClause();

		#region User based clauses

		/// <summary>
		///     Create a clause for the creator
		/// </summary>
		public static IUserClause Creator => new UserClause(Fields.Creator);

		/// <summary>
		///     Create a clause for the contributor
		/// </summary>
		public static IUserClause Contributor => new UserClause(Fields.Contributor);

		/// <summary>
		///     Create a clause for the mention
		/// </summary>
		public static IUserClause Mention => new UserClause(Fields.Mention);

		/// <summary>
		///     Create a clause for the watcher
		/// </summary>
		public static IUserClause Watcher => new UserClause(Fields.Watcher);

		/// <summary>
		///     Create a clause for the favourite
		/// </summary>
		public static IUserClause Favourite => new UserClause(Fields.Favourite);

		#endregion

		#region BooleanLogic

		public static IFinalClause And(params IFinalClause[] clauses)
		{
			return new Clause("(" + string.Join(" and ", clauses.ToList()) + ")");
		}

		public static IFinalClause Or(params IFinalClause[] clauses)
		{
			return new Clause("(" + string.Join(" or ", clauses.ToList()) + ")");
		}

		#endregion

		#region text

		/// <summary>
		///     Create a clause for the Text field
		/// </summary>
		public static ITextClause Text => new TextClause(Fields.Text);

		/// <summary>
		///     Create a clause for the Title field
		/// </summary>
		public static ITextClause Title => new TextClause(Fields.Title);

		#endregion

		#region content id based

		/// <summary>
		///     Create a clause for the Id field
		/// </summary>
		public static IContentClause Id => new ContentClause(Fields.Id);

		/// <summary>
		///     Create a clause for the Ancestor field
		/// </summary>
		public static IContentClause Ancestor => new ContentClause(Fields.Ancestor);

		/// <summary>
		///     Create a clause for the Content field
		/// </summary>
		public static IContentClause Content => new ContentClause(Fields.Content);

		/// <summary>
		///     Create a clause for the Parent field
		/// </summary>
		public static IContentClause Parent => new ContentClause(Fields.Parent);

		#endregion

		#region simple values

		/// <summary>
		///     Create a clause for the Label field
		/// </summary>
		public static ISimpleValueClause Label => new SimpleValueClause(Fields.Label);


		/// <summary>
		///     Create a clause for the Container field
		/// </summary>
		public static ISimpleValueClause Container => new SimpleValueClause(Fields.Container);

		/// <summary>
		///     Create a clause for the Macro field
		/// </summary>
		public static ISimpleValueClause Macro => new SimpleValueClause(Fields.Macro);

		#endregion
	}
}