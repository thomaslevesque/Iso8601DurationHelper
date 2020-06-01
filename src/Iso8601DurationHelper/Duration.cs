using System;
using System.ComponentModel;
using System.Text;

namespace Iso8601DurationHelper
{
    /// <summary>
    /// Represents an ISO8601 duration expressed in number of years, months, weeks, days, hours, minutes and seconds.
    /// </summary>
    [TypeConverter(typeof(DurationConverter))]
    public struct Duration : IEquatable<Duration>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Duration"/>.
        /// </summary>
        /// <param name="years">Number of years.</param>
        /// <param name="months">Number of months.</param>
        /// <param name="weeks">Number of weeks.</param>
        /// <param name="days">Number of days.</param>
        /// <param name="hours">Number of hours.</param>
        /// <param name="minutes">Number of minutes.</param>
        /// <param name="seconds">Number of seconds.</param>
        public Duration(uint years, uint months, uint weeks, uint days, uint hours, uint minutes, uint seconds)
        {
            Years = years;
            Months = months;
            Weeks = weeks;
            Days = days;
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        /// <summary>
        /// The number of years in this <see cref="Duration"/>.
        /// </summary>
        public uint Years { get; }

        /// <summary>
        /// The number of months in this <see cref="Duration"/>.
        /// </summary>
        public uint Months { get; }

        /// <summary>
        /// The number of weeks in this <see cref="Duration"/>.
        /// </summary>
        public uint Weeks { get; }

        /// <summary>
        /// The number of days in this <see cref="Duration"/>.
        /// </summary>
        public uint Days { get; }

        /// <summary>
        /// The number of hours in this <see cref="Duration"/>.
        /// </summary>
        public uint Hours { get; }

        /// <summary>
        /// The number of minutes in this <see cref="Duration"/>.
        /// </summary>
        public uint Minutes { get; }

        /// <summary>
        /// The number of seconds in this <see cref="Duration"/>.
        /// </summary>
        public uint Seconds { get; }

        /// <summary>
        /// Returns the zero <see cref="Duration"/>.
        /// </summary>
        public static Duration Zero { get; } = new Duration(0, 0, 0, 0, 0, 0, 0);

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="value">An object to compare with this instance.</param>
        /// <returns><c>true</c> if <c>value</c> is a <see cref="Duration"/> object that represents the same duration as this instance, <c>false</c>.</returns>
        public override bool Equals(object value) => value is Duration other && Equals(other);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int) 2166136261;
                hash = (hash * 16777619) ^ Years.GetHashCode();
                hash = (hash * 16777619) ^ Months.GetHashCode();
                hash = (hash * 16777619) ^ Weeks.GetHashCode();
                hash = (hash * 16777619) ^ Days.GetHashCode();
                hash = (hash * 16777619) ^ Hours.GetHashCode();
                hash = (hash * 16777619) ^ Minutes.GetHashCode();
                hash = (hash * 16777619) ^ Seconds.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns><c>true</c> if <c>other</c> represents the same duration as this instance; otherwise, <c>false</c>.</returns>
        public bool Equals(Duration other) =>
            Years == other.Years &&
            Months == other.Months &&
            Weeks == other.Weeks &&
            Days == other.Days &&
            Hours == other.Hours &&
            Minutes == other.Minutes &&
            Seconds == other.Seconds;

        /// <summary>
        /// Converts this <see cref="Duration"/> to its ISO8601 representation.
        /// </summary>
        /// <returns>The ISO8601 representation of this <see cref="Duration"/>.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder("P");

            // Date components
            AppendComponent(Years, "Y");
            AppendComponent(Months, "M");
            AppendComponent(Weeks, "W");
            AppendComponent(Days, "D");

            // Time separator
            if (Hours != 0 || Minutes != 0 || Seconds != 0)
                sb.Append("T");

            // Time components
            AppendComponent(Hours, "H");
            AppendComponent(Minutes, "M");
            AppendComponent(Seconds, "S");

            // Empty duration
            if (sb.Length == 1)
                sb.Append("0D");

            return sb.ToString();

            void AppendComponent(uint number, string symbol)
            {
                if (number != 0)
                    sb.Append(number).Append(symbol);
            }
        }

        /// <summary>
        /// Adds a <see cref="Duration"/> to a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> to add the duration to.</param>
        /// <param name="duration">The <see cref="Duration"/> to add.</param>
        /// <returns>A new <see cref="DateTime"/> with is the result of the addition.</returns>
        public static DateTime operator+(DateTime dateTime, Duration duration)
        {
            if (duration.Years != 0)
                dateTime = dateTime.AddYears((int)duration.Years);
            if (duration.Months != 0)
                dateTime = dateTime.AddMonths((int)duration.Months);
            if (duration.Weeks != 0)
                dateTime = dateTime.AddDays(7 * (int)duration.Weeks);
            if (duration.Days != 0)
                dateTime = dateTime.AddDays((int)duration.Days);
            if (duration.Hours != 0)
                dateTime = dateTime.AddHours((int)duration.Hours);
            if (duration.Minutes != 0)
                dateTime = dateTime.AddMinutes((int)duration.Minutes);
            if (duration.Seconds != 0)
                dateTime = dateTime.AddSeconds((int)duration.Seconds);
            return dateTime;
        }

        /// <summary>
        /// Subtracts a <see cref="Duration"/> from a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> to subtract the duration from.</param>
        /// <param name="duration">The <see cref="Duration"/> to subtract.</param>
        /// <returns>A new <see cref="DateTime"/> with is the result of the subtraction.</returns>
        public static DateTime operator-(DateTime dateTime, Duration duration)
        {
            if (duration.Years != 0)
                dateTime = dateTime.AddYears(-(int)duration.Years);
            if (duration.Months != 0)
                dateTime = dateTime.AddMonths(-(int)duration.Months);
            if (duration.Weeks != 0)
                dateTime = dateTime.AddDays(-7 * (int)duration.Weeks);
            if (duration.Days != 0)
                dateTime = dateTime.AddDays(-(int)duration.Days);
            if (duration.Hours != 0)
                dateTime = dateTime.AddHours(-(int)duration.Hours);
            if (duration.Minutes != 0)
                dateTime = dateTime.AddMinutes(-(int)duration.Minutes);
            if (duration.Seconds != 0)
                dateTime = dateTime.AddSeconds(-(int)duration.Seconds);
            return dateTime;
        }

        /// <summary>
        /// Adds a <see cref="Duration"/> to a <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTimeOffset"/> to add the duration to.</param>
        /// <param name="duration">The <see cref="Duration"/> to add.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> with is the result of the addition.</returns>
        public static DateTimeOffset operator+(DateTimeOffset dateTime, Duration duration)
        {
            if (duration.Years != 0)
                dateTime = dateTime.AddYears((int)duration.Years);
            if (duration.Months != 0)
                dateTime = dateTime.AddMonths((int)duration.Months);
            if (duration.Weeks != 0)
                dateTime = dateTime.AddDays(7 * (int)duration.Weeks);
            if (duration.Days != 0)
                dateTime = dateTime.AddDays((int)duration.Days);
            if (duration.Hours != 0)
                dateTime = dateTime.AddHours((int)duration.Hours);
            if (duration.Minutes != 0)
                dateTime = dateTime.AddMinutes((int)duration.Minutes);
            if (duration.Seconds != 0)
                dateTime = dateTime.AddSeconds((int)duration.Seconds);
            return dateTime;
        }

        /// <summary>
        /// Subtracts a <see cref="Duration"/> from a <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTimeOffset"/> to subtract the duration from.</param>
        /// <param name="duration">The <see cref="Duration"/> to subtract.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> with is the result of the subtraction.</returns>
        public static DateTimeOffset operator-(DateTimeOffset dateTime, Duration duration)
        {
            if (duration.Years != 0)
                dateTime = dateTime.AddYears(-(int)duration.Years);
            if (duration.Months != 0)
                dateTime = dateTime.AddMonths(-(int)duration.Months);
            if (duration.Weeks != 0)
                dateTime = dateTime.AddDays(-7 * (int)duration.Weeks);
            if (duration.Days != 0)
                dateTime = dateTime.AddDays(-(int)duration.Days);
            if (duration.Hours != 0)
                dateTime = dateTime.AddHours(-(int)duration.Hours);
            if (duration.Minutes != 0)
                dateTime = dateTime.AddMinutes(-(int)duration.Minutes);
            if (duration.Seconds != 0)
                dateTime = dateTime.AddSeconds(-(int)duration.Seconds);
            return dateTime;
        }

        /// <summary>
        /// Adds two instances of <see cref="Duration"/>.
        /// </summary>
        /// <param name="duration1">The first duration.</param>
        /// <param name="duration2">The second duration.</param>
        /// <returns>The sum of the two durations.</returns>
        public static Duration operator+(Duration duration1, Duration duration2)
        {
            return new Duration(
                duration1.Years + duration2.Years,
                duration1.Months + duration2.Months,
                duration1.Weeks + duration2.Weeks,
                duration1.Days + duration2.Days,
                duration1.Hours + duration2.Hours,
                duration1.Minutes + duration2.Minutes,
                duration1.Seconds + duration2.Seconds
            );
        }

        /// <summary>
        /// Returns a value indicating whether two <see cref="Duration"/> instances are equal.
        /// </summary>
        /// <param name="duration1">The first duration.</param>
        /// <param name="duration2">The second duration.</param>
        /// <returns><c>true</c> if the arguments represents the same duration; otherwise, <c>false</c>.</returns>
        public static bool operator==(Duration duration1, Duration duration2)
        {
            return duration1.Equals(duration2);
        }

        /// <summary>
        /// Returns a value indicating whether two <see cref="Duration"/> instances are different.
        /// </summary>
        /// <param name="duration1">The first duration.</param>
        /// <param name="duration2">The second duration.</param>
        /// <returns><c>true</c> if the arguments represent different durations; otherwise, <c>false</c>.</returns>
        public static bool operator!=(Duration duration1, Duration duration2)
        {
            return !(duration1 == duration2);
        }

        /// <summary>
        /// Converts the ISO8601 representation of a duration to a <see cref="Duration"/> instance.
        /// </summary>
        /// <param name="input">The ISO8601 representation of a duration.</param>
        /// <returns>A <see cref="Duration"/> instance that corresponds to the ISO8601 duration.</returns>
        /// <exception cref="ArgumentNullException"><c>input</c> is null.</exception>
        /// <exception cref="FormatException"><c>input</c> has an invalid format.</exception>
        public static Duration Parse(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (TryParse(input, out var duration))
                return duration;

            throw new FormatException("Invalid duration format");
        }

        /// <summary>
        /// Attempts to convert the ISO8601 representation of a duration to a <see cref="Duration"/> instance.
        /// </summary>
        /// <param name="input">The ISO8601 representation of a duration.</param>
        /// <param name="duration">When this method returns, contains an instance of <see cref="Duration"/> equivalent to <c>input</c>, or <see cref="Zero"/> if the conversion failed. This parameter is passed uninitialized.</param>
        /// <returns><c>true</c> if <c>input</c> was converted successfully; otherwise, <c>false</c>.</returns>
        public static bool TryParse(string input, out Duration duration)
        {
            duration = default;
            if (input == null)
                return false;
            if (input.Length < 3)
                return false;
            if (input[0] != DurationChars.Prefix)
                return false;

            uint years = 0;
            uint months = 0;
            uint weeks = 0;
            uint days = 0;
            uint hours = 0;
            uint minutes = 0;
            uint seconds = 0;

            var lastComponent = DurationComponent.None;
            int position = 1;
            int numberStart = -1;
            bool isTimeSpecified = false;

            while (position < input.Length)
            {
                char c = input[position];
                if (c == DurationChars.Time)
                {
                    isTimeSpecified = true;
                    lastComponent = DurationComponent.Time;
                }
                else if (char.IsLetter(c))
                {
                    if (numberStart < 0 || numberStart >= position)
                        return false; // No number preceding letter

                    string numberString = input.Substring(numberStart, position - numberStart);
                    if (!uint.TryParse(numberString, out uint value))
                        return false; // Not a valid number

                    // Check component order
                    var component = GetComponent(c, isTimeSpecified);
                    if (component == DurationComponent.None)
                        return false; // invalid character
                    if (component > DurationComponent.Time && !isTimeSpecified)
                        return false; // Time component before the time specified
                    if (component <= lastComponent)
                        return false; // Components in wrong order

                    switch(component)
                    {
                        case DurationComponent.Years:
                            years = value;
                            break;
                        case DurationComponent.Months:
                            months = value;
                            break;
                        case DurationComponent.Weeks:
                            weeks = value;
                            break;
                        case DurationComponent.Days:
                            days = value;
                            break;
                        case DurationComponent.Hours:
                            hours = value;
                            break;
                        case DurationComponent.Minutes:
                            minutes = value;
                            break;
                        case DurationComponent.Seconds:
                            seconds = value;
                            break;
                    }

                    numberStart = -1;
                    lastComponent = component;
                }
                else if (char.IsDigit(c))
                {
                    if (numberStart < 0)
                        numberStart = position;
                }
                else
                {
                    // Invalid character
                    return false;
                }

                position++;
            }

            if (lastComponent == DurationComponent.None)
                return false; // No component was specified
            if (isTimeSpecified && lastComponent <= DurationComponent.Time)
                return false; // We've seen the time specifier, but no time component was specified

            duration = new Duration(years, months, weeks, days, hours, minutes, seconds);
            return true;
        }

        /// <summary>
        /// Creates an instance of <see cref="Duration"/> with the specified number of years.
        /// </summary>
        /// <param name="years">The number of years.</param>
        /// <returns>An instance of <see cref="Duration"/> with the specified number of years.</returns>
        public static Duration FromYears(uint years) => new Duration(years, 0, 0, 0, 0, 0, 0);

        /// <summary>
        /// Creates an instance of <see cref="Duration"/> with the specified number of months.
        /// </summary>
        /// <param name="months">The number of months.</param>
        /// <returns>An instance of <see cref="Duration"/> with the specified number of months.</returns>
        public static Duration FromMonths(uint months) => new Duration(0, months, 0, 0, 0, 0, 0);

        /// <summary>
        /// Creates an instance of <see cref="Duration"/> with the specified number of weeks.
        /// </summary>
        /// <param name="weeks">The number of weeks.</param>
        /// <returns>An instance of <see cref="Duration"/> with the specified number of weeks.</returns>
        public static Duration FromWeeks(uint weeks) => new Duration(0, 0, weeks, 0, 0, 0, 0);

        /// <summary>
        /// Creates an instance of <see cref="Duration"/> with the specified number of days.
        /// </summary>
        /// <param name="days">The number of days.</param>
        /// <returns>An instance of <see cref="Duration"/> with the specified number of days.</returns>
        public static Duration FromDays(uint days) => new Duration(0, 0, 0, days, 0, 0, 0);

        /// <summary>
        /// Creates an instance of <see cref="Duration"/> with the specified number of hours.
        /// </summary>
        /// <param name="hours">The number of hours.</param>
        /// <returns>An instance of <see cref="Duration"/> with the specified number of hours.</returns>
        public static Duration FromHours(uint hours) => new Duration(0, 0, 0, 0, hours, 0, 0);

        /// <summary>
        /// Creates an instance of <see cref="Duration"/> with the specified number of minutes.
        /// </summary>
        /// <param name="minutes">The number of minutes.</param>
        /// <returns>An instance of <see cref="Duration"/> with the specified number of minutes.</returns>
        public static Duration FromMinutes(uint minutes) => new Duration(0, 0, 0, 0, 0, minutes, 0);

        /// <summary>
        /// Creates an instance of <see cref="Duration"/> with the specified number of seconds.
        /// </summary>
        /// <param name="seconds">The number of seconds.</param>
        /// <returns>An instance of <see cref="Duration"/> with the specified number of seconds.</returns>
        public static Duration FromSeconds(uint seconds) => new Duration(0, 0, 0, 0, 0, 0, seconds);


        private static DurationComponent GetComponent(char c, bool isTimeSpecified)
        {
            switch (c)
            {
                case DurationChars.Year:
                    return DurationComponent.Years;
                case DurationChars.Months when !isTimeSpecified:
                    return DurationComponent.Months;
                case DurationChars.Weeks:
                    return DurationComponent.Weeks;
                case DurationChars.Days:
                    return DurationComponent.Days;
                case DurationChars.Time when !isTimeSpecified:
                    return DurationComponent.Time;
                case DurationChars.Hours:
                    return DurationComponent.Hours;
                case DurationChars.Minutes when isTimeSpecified:
                    return DurationComponent.Minutes;
                case DurationChars.Seconds:
                    return DurationComponent.Seconds;
                default: return DurationComponent.None;
            }
        }

        private static class DurationChars
        {
            public const char Prefix = 'P';
            public const char Time = 'T';

            public const char Year = 'Y';
            public const char Months = 'M';
            public const char Weeks = 'W';
            public const char Days = 'D';
            public const char Hours = 'H';
            public const char Minutes = 'M';
            public const char Seconds = 'S';
        }

        private enum DurationComponent
        {
            None = 0,
            Years = None + 1,
            Months = Years + 1,
            Weeks = Months + 1,
            Days = Weeks + 1,
            Time = Days + 1,
            Hours = Time + 1,
            Minutes = Hours + 1,
            Seconds = Minutes + 1
        }
    }
}
