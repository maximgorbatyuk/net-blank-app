﻿using System;
using Utils.Dates;

namespace Utils.Helpers
{
    public static class CompareHelpers
    {
        public static bool EqualTo(this double first, double second, double tolerance = 0.01)
        {
            return Math.Abs(first - second) < tolerance;
        }

        public static bool EqualTo(this double? first, double? second, double tolerance = 0.01)
        {
            if (first == null && second == null)
            {
                return true;
            }

            if ((first.HasValue && !second.HasValue) || (!first.HasValue && second.HasValue))
            {
                return false;
            }

            return Math.Abs(first.Value - second.Value) < tolerance;
        }

        public static bool LaterOrEqual(this DateTimeOffset first, DateTimeOffset second)
        {
            return Compare(first, second) >= 0;
        }

        public static bool EarlierOrEqual(this DateTimeOffset first, DateTimeOffset second)
        {
            return Compare(first, second) <= 0;
        }

        public static bool SameMonth(this DateTimeOffset first, DateTimeOffset second)
        {
            return first.Year == second.Year && first.Month == second.Month;
        }

        public static bool SameMonth(this Date first, Date second)
        {
            first.ThrowIfNull(nameof(first));
            second.ThrowIfNull(nameof(second));

            return first.Year == second.Year && first.Month == second.Month;
        }

        public static bool SameDay(this DateTimeOffset first, DateTimeOffset second)
        {
            return first.SameMonth(second) && first.Day == second.Day;
        }

        public static bool SameDay(this DateTimeOffset? first, DateTimeOffset? second)
        {
            return first.HasValue && second.HasValue &&
                   first.Value.SameMonth(second.Value) && first.Value.Day == second.Value.Day;
        }

        public static bool SameDay(this DateTimeOffset first, Date second)
        {
            return first.SameMonth(second.Source) && first.Day == second.Source.Day;
        }

        public static bool SameDay(this DateTimeOffset? first, Date second)
        {
            return first.HasValue && first.Value.SameMonth(second.Source) && first.Value.Day == second.Source.Day;
        }

        public static bool IsToday(this DateTimeOffset date)
        {
            var today = Date.Today;
            return date.SameMonth(today.Source) && date.Day == today.Source.Day;
        }

        public static bool Earlier(this DateTimeOffset first, DateTimeOffset second)
        {
            return Compare(first, second) < 0;
        }

        public static bool Earlier(this DateTimeOffset? first, DateTimeOffset? second)
        {
            if (first == null || second == null)
            {
                return false;
            }

            return first.Value.Earlier(second.Value);
        }

        public static bool EarlierOrEqual(this DateTimeOffset? first, DateTimeOffset? second)
        {
            if (first == null || second == null)
            {
                return false;
            }

            return first.Value.EarlierOrEqual(second.Value);
        }

        public static bool EarlierOrEqual(this DateTimeOffset? first, Date second)
        {
            if (first == null || second == null)
            {
                return false;
            }

            return first.Value.EarlierOrEqual(second.EndOfTheDay());
        }

        public static bool Later(this DateTimeOffset first, DateTimeOffset second)
        {
            return Compare(first, second) > 0;
        }

        public static bool Later(this DateTimeOffset? first, DateTimeOffset? second)
        {
            if (first == null || second == null)
            {
                return false;
            }

            return first.Value.Later(second.Value);
        }

        public static bool Later(this Date first, Date second)
        {
            return Compare(first.Source, second.Source) > 0;
        }

        public static bool Equal(this DateTimeOffset? first, DateTimeOffset? second)
        {
            if (first == null && second == null)
            {
                return true;
            }

            if (first == null || second == null)
            {
                return false;
            }

            return first.Value.Equals(second.Value);
        }

        public static bool Equal(this DateTimeOffset first, DateTimeOffset second)
        {
            return Compare(first, second) == 0;
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset.compare?view=netcore-3.1.
        /// &#60; (less than) 0 - the first is earlier than the second.
        /// = (equal to) 0 - the first is equal the second.
        /// &#62; (greater than) 0 - the first is later than the second.
        /// </summary>
        /// <param name="first">First.</param>
        /// <param name="second">Second.</param>
        /// <returns>Compare result.</returns>
        private static int Compare(DateTimeOffset first, DateTimeOffset second)
        {
            return first.CompareTo(second);
        }
    }
}