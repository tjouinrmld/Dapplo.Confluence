// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;

namespace Dapplo.Confluence.Query
{
    /// <summary>
    ///     A clause for date time calculations
    /// </summary>
    internal class DatetimeClause : IDatetimeClause, IDatetimeClauseWithoutValue
    {
        private readonly Fields[] _allowedFields = { Fields.Created, Fields.LastModified };
        private readonly Clause _clause;

        internal DatetimeClause(Fields datetimeField)
        {
            if (_allowedFields.All(field => datetimeField != field))
            {
                throw new InvalidOperationException($"Can't add function for the field {datetimeField}");
            }
            _clause = new Clause
            {
                Field = datetimeField
            };
        }

        /// <inheritDoc />
        public IDatetimeClauseWithoutValue On
        {
            get
            {
                _clause.Operator = Operators.EqualTo;
                return this;
            }
        }

        /// <inheritDoc />
        public IDatetimeClauseWithoutValue Before
        {
            get
            {
                _clause.Operator = Operators.LessThan;
                return this;
            }
        }

        /// <inheritDoc />
        public IDatetimeClauseWithoutValue BeforeOrOn
        {
            get
            {
                _clause.Operator = Operators.LessThanEqualTo;
                return this;
            }
        }

        /// <inheritDoc />
        public IDatetimeClauseWithoutValue After
        {
            get
            {
                _clause.Operator = Operators.GreaterThan;
                return this;
            }
        }

        /// <inheritDoc />
        public IDatetimeClauseWithoutValue AfterOrOn
        {
            get
            {
                _clause.Operator = Operators.GreaterThanEqualTo;
                return this;
            }
        }

        /// <inheritDoc />
        public IFinalClause DateTime(DateTime dateTime)
        {
            if (dateTime.Minute == 0 && dateTime.Hour == 0)
            {
                _clause.Value = $"\"{dateTime:yyyy-MM-dd}\"";
            }
            else
            {
                _clause.Value = $"\"{dateTime:yyyy-MM-dd HH:mm}\"";
            }
            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause EndOfDay(TimeSpan? timeSpan = null)
        {
            _clause.Value = $"endOfDay({TimeSpanToIncrement(timeSpan)})";
            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause EndOfMonth(TimeSpan? timeSpan = null)
        {
            _clause.Value = $"endOfMonth({TimeSpanToIncrement(timeSpan)})";
            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause EndOfWeek(TimeSpan? timeSpan = null)
        {
            _clause.Value = $"endOfWeek({TimeSpanToIncrement(timeSpan)})";
            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause EndOfYear(TimeSpan? timeSpan = null)
        {
            _clause.Value = $"endOfYear({TimeSpanToIncrement(timeSpan)})";
            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause StartOfDay(TimeSpan? timeSpan = null)
        {
            _clause.Value = $"startOfDay({TimeSpanToIncrement(timeSpan)})";
            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause StartOfMonth(TimeSpan? timeSpan = null)
        {
            _clause.Value = $"startOfMonth({TimeSpanToIncrement(timeSpan)})";
            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause StartOfWeek(TimeSpan? timeSpan = null)
        {
            _clause.Value = $"startOfWeek({TimeSpanToIncrement(timeSpan)})";
            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause StartOfYear(TimeSpan? timeSpan = null)
        {
            _clause.Value = $"startOfYear({TimeSpanToIncrement(timeSpan)})";
            return _clause;
        }

        /// <summary>
        ///     Create an increment from the timespan.
        ///     increment has of (+/-)nn(y|M|w|d|h|m)
        ///     If the plus/minus(+/-) sign is omitted, plus is assumed.
        ///     nn: number; y: year, M: month; w: week; d: day; h: hour; m: minute.
        /// </summary>
        /// <param name="timeSpan">TimeSpan to convert</param>
        /// <returns>string</returns>
        private static string TimeSpanToIncrement(TimeSpan? timeSpan = null)
        {
            if (!timeSpan.HasValue)
            {
                return "";
            }
            var increment = timeSpan.Value;
            var days = increment.TotalDays;
            if ((days > double.Epsilon || days < double.Epsilon) && days % 1 < double.Epsilon)
            {
                return $"\"{days}d\"";
            }
            var hours = increment.TotalHours;
            if ((hours > double.Epsilon || hours < double.Epsilon) && hours % 1 < double.Epsilon)
            {
                return $"\"{hours}h\"";
            }
            return $"\"{(int)timeSpan.Value.TotalMinutes}m\"";
        }
    }
}