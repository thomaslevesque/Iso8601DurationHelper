using System;
using System.ComponentModel;
using System.Text;

namespace Iso8601DurationHelper
{
    [TypeConverter(typeof(DurationConverter))]
    public struct Duration : IEquatable<Duration>
    {
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

        public uint Years { get; }
        public uint Months { get; }
        public uint Weeks { get; }
        public uint Days { get; }
        public uint Hours { get; }
        public uint Minutes { get; }
        public uint Seconds { get; }

        public static Duration Zero { get; } = new Duration(0, 0, 0, 0, 0, 0, 0);

        public override bool Equals(object obj) => obj is Duration other && Equals(other);

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

        public bool Equals(Duration other) =>
            Years == other.Years &&
            Months == other.Months &&
            Weeks == other.Weeks &&
            Days == other.Days &&
            Hours == other.Hours &&
            Minutes == other.Minutes &&
            Seconds == other.Seconds;

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

        public static Duration operator +(Duration duration1, Duration duration2)
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

        public static Duration Parse(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (TryParse(input, out var duration))
                return duration;

            throw new FormatException("Invalid duration format");
        }

        public static bool TryParse(string input, out Duration duration)
        {
            duration = default;
            if (input == null)
                throw new ArgumentNullException(nameof(input));
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

        public static Duration FromYears(uint years) => new Duration(years, 0, 0, 0, 0, 0, 0);
        public static Duration FromMonths(uint months) => new Duration(0, months, 0, 0, 0, 0, 0);
        public static Duration FromWeeks(uint weeks) => new Duration(0, 0, weeks, 0, 0, 0, 0);
        public static Duration FromDays(uint days) => new Duration(0, 0, 0, days, 0, 0, 0);
        public static Duration FromHours(uint hours) => new Duration(0, 0, 0, 0, hours, 0, 0);
        public static Duration FromMinutes(uint minutes) => new Duration(0, 0, 0, 0, 0, minutes, 0);
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
