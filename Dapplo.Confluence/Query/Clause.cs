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
using System.Text;
using Dapplo.HttpExtensions.Extensions;

#endregion

namespace Dapplo.Confluence.Query
{
	/// <summary>
	///     A clause which cannot be modified anymore, only ToString() makes sense
	/// </summary>
	public interface IFinalClause
	{
	}

	/// <summary>
	///     This stores the information for a CQL where clause
	/// </summary>
	public class Clause : IFinalClause
	{
		/// <summary>
		///     The field to compare
		/// </summary>
		public Fields Field { get; set; }

		/// <summary>
		///     The operator
		/// </summary>
		public Operators Operator { get; set; }

		/// <summary>
		///     Value to compare with the operator
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// Change the operator to the negative version ( equals becomes not equals becomes equals)
		/// </summary>
		public void Negate()
		{
			switch (Operator)
			{
				case Operators.Contains:
					Operator = Operators.DoesNotContain;
					break;
				case Operators.DoesNotContain:
					Operator = Operators.Contains;
					break;
				case Operators.EqualTo:
					Operator = Operators.NotEqualTo;
					break;
				case Operators.NotEqualTo:
					Operator = Operators.EqualTo;
					break;
				case Operators.In:
					Operator = Operators.NotIn;
					break;
				case Operators.NotIn:
					Operator = Operators.NotIn;
					break;
				case Operators.GreaterThan:
					Operator = Operators.LessThan;
					break;
				case Operators.GreaterThanEqualTo:
					Operator = Operators.LessThanEqualTo;
					break;
				case Operators.LessThan:
					Operator = Operators.GreaterThan;
					break;
				case Operators.LessThanEqualTo:
					Operator = Operators.GreaterThanEqualTo;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public override string ToString()
		{
			var clauseBuilder = new StringBuilder();
			clauseBuilder.Append(Field.EnumValueOf()).Append(' ');
			clauseBuilder.Append(Operator.EnumValueOf()).Append(' ');
			clauseBuilder.Append(Value);
			return clauseBuilder.ToString();
		}

		/// <summary>
		///     Add implicit casting to string
		/// </summary>
		/// <param name="clause">Clause</param>
		public static implicit operator string(Clause clause)
		{
			return clause.ToString();
		}
	}
}