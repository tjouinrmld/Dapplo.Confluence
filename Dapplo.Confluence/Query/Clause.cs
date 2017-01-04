using System;
using System.Collections.Generic;
using System.Linq;

namespace Dapplo.Confluence.Query
{
	public class Clause
	{
		/// <summary>
		/// The field to compare
		/// </summary>
		public Fields Field { get; set; }

		/// <summary>
		/// The operator
		/// </summary>
		public Operators Operator { get; set; }

		/// <summary>
		/// Value to compare with the operator
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// Use the currentUser function as the value to compare 
		/// </summary>
		/// <returns>this</returns>
		public Clause CurrentUser()
		{
			ValidateField(new[] { Fields.Creator,  Fields.Contributor,  Fields.Mention,  Fields.Watcher,  Fields.Favourite});
			Value = "currentUser()";
			return this;
		}

		/// <summary>
		/// Validate if the field is allowed for a certain operation/function
		/// </summary>
		/// <param name="fields">IEnumerable with the allowed fields</param>
		private void ValidateField(IEnumerable<Fields> fields)
		{
			if (!fields.Contains(Field))
			{
				throw new InvalidOperationException("Can't add function for the field {Field}");
			}
		}

		/// <summary>
		/// Use the endOfDay function as the value to compare 
		/// </summary>
		/// <param name="increment">inc is an optional increment of (+/-)nn(y|M|w|d|h|m)
		/// If the plus/minus(+/-) sign is omitted, plus is assumed.
		/// nn: number; y: year, M: month; w: week; d: day; h: hour; m: minute.</param>
		/// <returns>this</returns>
		public Clause EndOfDay(string increment = "")
		{
			ValidateField(new []{ Fields.Created, Fields.LastModified});
			Value = $"endOfDay({increment})";
			return this;
		}

		/// <summary>
		/// Use the endOfMonth function as the value to compare 
		/// </summary>
		/// <param name="increment">inc is an optional increment of (+/-)nn(y|M|w|d|h|m)
		/// If the plus/minus(+/-) sign is omitted, plus is assumed.
		/// nn: number; y: year, M: month; w: week; d: day; h: hour; m: minute.</param>
		/// <returns>this</returns>
		public Clause EndOfMonth(string increment = "")
		{
			ValidateField(new[] { Fields.Created, Fields.LastModified });
			Value = $"endOfMonth({increment})";
			return this;
		}

		/// <summary>
		/// Use the endOfWeek function as the value to compare 
		/// </summary>
		/// <param name="increment">inc is an optional increment of (+/-)nn(y|M|w|d|h|m)
		/// If the plus/minus(+/-) sign is omitted, plus is assumed.
		/// nn: number; y: year, M: month; w: week; d: day; h: hour; m: minute.</param>
		/// <returns>this</returns>
		public Clause EndOfWeek(string increment = "")
		{
			ValidateField(new[] { Fields.Created, Fields.LastModified });
			Value = $"endOfWeek({increment})";
			return this;
		}

		/// <summary>
		/// Use the endOfYear function as the value to compare 
		/// </summary>
		/// <param name="increment">inc is an optional increment of (+/-)nn(y|M|w|d|h|m)
		/// If the plus/minus(+/-) sign is omitted, plus is assumed.
		/// nn: number; y: year, M: month; w: week; d: day; h: hour; m: minute.</param>
		/// <returns>this</returns>
		public Clause EndOfYear(string increment = "")
		{
			ValidateField(new[] { Fields.Created, Fields.LastModified });
			Value = $"endOfYear({increment})";
			return this;
		}

		/// <summary>
		/// Use the startOfDay function as the value to compare 
		/// </summary>
		/// <param name="increment">inc is an optional increment of (+/-)nn(y|M|w|d|h|m)
		/// If the plus/minus(+/-) sign is omitted, plus is assumed.
		/// nn: number; y: year, M: month; w: week; d: day; h: hour; m: minute.</param>
		/// <returns>this</returns>
		public Clause StartOfDay(string increment = "")
		{
			ValidateField(new[] { Fields.Created, Fields.LastModified });
			Value = $"startOfDay({increment})";
			return this;
		}

		/// <summary>
		/// Use the startOfMonth function as the value to compare 
		/// </summary>
		/// <param name="increment">inc is an optional increment of (+/-)nn(y|M|w|d|h|m)
		/// If the plus/minus(+/-) sign is omitted, plus is assumed.
		/// nn: number; y: year, M: month; w: week; d: day; h: hour; m: minute.</param>
		/// <returns>this</returns>
		public Clause StartOfMonth(string increment = "")
		{
			ValidateField(new[] { Fields.Created, Fields.LastModified });
			Value = $"startOfMonth({increment})";
			return this;
		}

		/// <summary>
		/// Use the startOfWeek function as the value to compare 
		/// </summary>
		/// <param name="increment">inc is an optional increment of (+/-)nn(y|M|w|d|h|m)
		/// If the plus/minus(+/-) sign is omitted, plus is assumed.
		/// nn: number; y: year, M: month; w: week; d: day; h: hour; m: minute.</param>
		/// <returns>this</returns>
		public Clause StartOfWeek(string increment = "")
		{
			ValidateField(new[] { Fields.Created, Fields.LastModified });
			Value = $"startOfWeek({increment})";
			return this;
		}

		/// <summary>
		/// Use the startOfYear function as the value to compare 
		/// </summary>
		/// <param name="increment">inc is an optional increment of (+/-)nn(y|M|w|d|h|m)
		/// If the plus/minus(+/-) sign is omitted, plus is assumed.
		/// nn: number; y: year, M: month; w: week; d: day; h: hour; m: minute.</param>
		/// <returns>this</returns>
		public Clause StartOfYear(string increment = "")
		{
			ValidateField(new[] { Fields.Created, Fields.LastModified });
			Value = $"startOfYear({increment})";
			return this;
		}

		/// <summary>
		/// Use the favouriteSpaces function as the value to compare 
		/// </summary>
		/// <returns>this</returns>
		public Clause FavouriteSpaces()
		{
			Value = "favouriteSpaces()";
			return this;
		}

		/// <summary>
		/// Use the recentlyViewedContent function as the value to compare 
		/// </summary>
		/// <returns>this</returns>
		public Clause RecentlyViewedContent()
		{
			Value = "recentlyViewedContent()";
			return this;
		}

		/// <summary>
		/// Use the recentlyViewedSpaces function as the value to compare 
		/// </summary>
		/// <returns>this</returns>
		public Clause RecentlyViewedSpaces()
		{
			Value = "recentlyViewedSpaces()";
			return this;
		}
	}
}
