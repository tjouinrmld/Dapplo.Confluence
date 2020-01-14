// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapplo.HttpExtensions.Extensions;

namespace Dapplo.Confluence.Query
{
    /// <summary>
    ///     A clause which cannot be modified anymore, only ToString() makes sense
    /// </summary>
    public interface IFinalClause
    {
        /// <summary>
        ///     Specify the order by field, default field order is used, this can be called mutiple times
        /// </summary>
        /// <param name="field">Field to specify what to order by</param>
        /// <returns>IFinalClause</returns>
        IFinalClause OrderBy(Fields field);

        /// <summary>
        ///     Specify the order by, ascending, this can be called mutiple times
        /// </summary>
        /// <param name="field">Field to specify what to order by</param>
        /// <returns>IFinalClause</returns>
        IFinalClause OrderByAscending(Fields field);

        /// <summary>
        ///     Specify the order by, descending, this can be called mutiple times
        /// </summary>
        /// <param name="field">Field to specify what to order by</param>
        /// <returns>IFinalClause</returns>
        IFinalClause OrderByDescending(Fields field);
    }

    /// <summary>
    ///     This stores the information for a CQL where clause
    /// </summary>
    internal class Clause : IFinalClause
    {
        private readonly IList<Tuple<Fields, bool?>> _orderByList = new List<Tuple<Fields, bool?>>();
        private string _finalClause;

        public Clause()
        {
        }

        public Clause(string finalClause)
        {
            _finalClause = finalClause;
        }

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

        public IFinalClause OrderBy(Fields field)
        {
            if (field == Fields.Label)
            {
                throw new ArgumentException("Cannot order by something that can have multiple values, like label", nameof(field));
            }
            _orderByList.Add(new Tuple<Fields, bool?>(field, null));
            return this;
        }

        public IFinalClause OrderByDescending(Fields field)
        {
            if (field == Fields.Label)
            {
                throw new ArgumentException("Cannot order by something that can have multiple values, like label", nameof(field));
            }
            _orderByList.Add(new Tuple<Fields, bool?>(field, true));
            return this;
        }

        public IFinalClause OrderByAscending(Fields field)
        {
            if (field == Fields.Label)
            {
                throw new ArgumentException("Cannot order by something that can have multiple values, like label", nameof(field));
            }
            _orderByList.Add(new Tuple<Fields, bool?>(field, false));
            return this;
        }

        /// <summary>
        ///     Change the operator to the negative version ( equals becomes not equals becomes equals)
        /// </summary>
        public void Negate()
        {
            Operator = Operator switch
            {
                Operators.Contains => Operators.DoesNotContain,
                Operators.DoesNotContain => Operators.Contains,
                Operators.EqualTo => Operators.NotEqualTo,
                Operators.NotEqualTo => Operators.EqualTo,
                Operators.In => Operators.NotIn,
                Operators.NotIn => Operators.NotIn,
                Operators.GreaterThan => Operators.LessThan,
                Operators.GreaterThanEqualTo => Operators.LessThanEqualTo,
                Operators.LessThan => Operators.GreaterThan,
                Operators.LessThanEqualTo => Operators.GreaterThanEqualTo,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        /// <summary>
        ///     Add implicit casting to string
        /// </summary>
        /// <param name="clause">Clause</param>
        public static implicit operator string(Clause clause)
        {
            return clause.ToString();
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(_finalClause))
            {
                return _finalClause;
            }

            var clauseBuilder = new StringBuilder();
            clauseBuilder.Append(Field.EnumValueOf()).Append(' ');
            clauseBuilder.Append(Operator.EnumValueOf()).Append(' ');
            clauseBuilder.Append(Value);
            if (_orderByList.Any())
            {
                clauseBuilder.Append(" order by ");
                clauseBuilder.Append(string.Join(", ", _orderByList.Select(orderBy =>
                {
                    var order = orderBy.Item2.HasValue ? orderBy.Item2.Value ? " desc" : " asc" : "";
                    return $"{orderBy.Item1.EnumValueOf()}{order}";
                })));
            }
            _finalClause = clauseBuilder.ToString();

            return _finalClause;
        }
    }
}