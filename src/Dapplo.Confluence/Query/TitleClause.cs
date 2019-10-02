//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016-2019 Dapplo
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

using System.Linq;

namespace Dapplo.Confluence.Query
{
    /// <summary>
    ///     Interface for the CQL title clauses
    /// </summary>
    public interface ITitleClause
	{
        /// <summary>
        ///     Negates the expression
        /// </summary>
        ITitleClause Not { get; }

		/// <summary>
		///     This allows fluent constructs like Text.Contains(customernumber)
		/// </summary>
		IFinalClause Contains(string value);

        /// <summary>
		///     This allows fluent constructs like Title.In("DEV", "PRODUCTION")
		/// </summary>
		IFinalClause In(params string[] values);

        /// <summary>
        ///     This allows fluent constructs like Title.Is("DEV")
        /// </summary>
        IFinalClause Is(string value);
    }

    /// <inheritDoc />
    internal class TitleClause : ITitleClause
    {
        private readonly Clause _clause;

		private bool _negate;

		internal TitleClause(Fields titleField)
		{
			_clause = new Clause
			{
				Field = titleField
            };
		}


		/// <inheritDoc />
		public ITitleClause Not
		{
			get
			{
				_negate = !_negate;
				return this;
			}
		}

		/// <inheritDoc />
		public IFinalClause Contains(string value)
		{
			_clause.Operator = Operators.Contains;
			_clause.Value = $"\"{value}\"";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
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
        public IFinalClause In(params string[] values)
        {
            _clause.Operator = Operators.In;
            _clause.Value = "(" + string.Join(", ", values.Select(user => $"\"{user}\"")) + ")";
            if (_negate)
            {
                _clause.Negate();
            }
            return _clause;
        }
    }
}