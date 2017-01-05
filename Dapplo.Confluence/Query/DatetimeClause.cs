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
	/// An interface for a date time calculations clause
	/// </summary>
	public interface IDatetimeClause
	{
	}

	/// <summary>
	/// A clause for date time calculations
	/// </summary>
	public class DatetimeClause : IDatetimeClause
	{
		private readonly Clause _clause;

		private readonly Fields[] _allowedFields = { Fields.Created, Fields.LastModified };

		internal DatetimeClause(Fields datetimeField)
		{
			if (!_allowedFields.Any(field => datetimeField == field))
			{
				throw new InvalidOperationException("Can't add function for the field {Field}");
			}
			_clause = new Clause
			{
				Field = datetimeField
			};

		}

		/// <summary>
		///     Create an increment from the timespan.
		///     increment has of (+/-)nn(y|M|w|d|h|m)
		///     If the plus/minus(+/-) sign is omitted, plus is assumed.
		///     nn: number; y: year, M: month; w: week; d: day; h: hour; m: minute.
		/// </summary>
		/// <param name="timeSpan">TimeSpan to convert</param>
		/// <returns>string</returns>
		private string TimeSpanToIncrement(TimeSpan? timeSpan)
		{
			if (timeSpan.HasValue)
			{
				// TODO: negative values and fractures
				return $"{timeSpan.Value.TotalMinutes}m";
			}
			return "";
		}

		/// <summary>
		///     Use the endOfDay function as the value to compare
		/// </summary>
		/// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
		/// <returns>this</returns>
		public IFinalClause EndOfDay(TimeSpan? timeSpan)
		{
			_clause.Value = $"endOfDay({TimeSpanToIncrement(timeSpan)})";
			return _clause;
		}

		/// <summary>
		///     Use the endOfMonth function as the value to compare
		/// </summary>
		/// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
		/// <returns>this</returns>
		public IFinalClause EndOfMonth(TimeSpan? timeSpan)
		{
			_clause.Value = $"endOfMonth({TimeSpanToIncrement(timeSpan)})";
			return _clause;
		}

		/// <summary>
		///     Use the endOfWeek function as the value to compare
		/// </summary>
		/// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
		/// <returns>this</returns>
		public IFinalClause EndOfWeek(TimeSpan? timeSpan)
		{
			_clause.Value = $"endOfWeek({TimeSpanToIncrement(timeSpan)})";
			return _clause;
		}

		/// <summary>
		///     Use the endOfYear function as the value to compare
		/// </summary>
		/// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
		/// <returns>this</returns>
		public IFinalClause EndOfYear(TimeSpan? timeSpan)
		{
			_clause.Value = $"endOfYear({TimeSpanToIncrement(timeSpan)})";
			return _clause;
		}

		/// <summary>
		///     Use the startOfDay function as the value to compare
		/// </summary>
		/// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
		/// <returns>this</returns>
		public IFinalClause StartOfDay(TimeSpan? timeSpan)
		{
			_clause.Value = $"startOfDay({TimeSpanToIncrement(timeSpan)})";
			return _clause;
		}

		/// <summary>
		///     Use the startOfMonth function as the value to compare
		/// </summary>
		/// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
		/// <returns>this</returns>
		public IFinalClause StartOfMonth(TimeSpan? timeSpan)
		{
			_clause.Value = $"startOfMonth({TimeSpanToIncrement(timeSpan)})";
			return _clause;
		}

		/// <summary>
		///     Use the startOfWeek function as the value to compare
		/// </summary>
		/// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
		/// <returns>this</returns>
		public IFinalClause StartOfWeek(TimeSpan? timeSpan)
		{
			_clause.Value = $"startOfWeek({TimeSpanToIncrement(timeSpan)})";
			return _clause;
		}

		/// <summary>
		///     Use the startOfYear function as the value to compare
		/// </summary>
		/// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
		/// <returns>this</returns>
		public IFinalClause StartOfYear(TimeSpan? timeSpan)
		{
			_clause.Value = $"startOfYear({TimeSpanToIncrement(timeSpan)})";
			return _clause;
		}
	}
}