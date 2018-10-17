using System;
using System.Text;

namespace Iso8601DurationHelper
{
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
            if (input[0] != 'P')
                return false;

            uint years = 0;
            uint months = 0;
            uint weeks = 0;
            uint days = 0;
            uint hours = 0;
            uint minutes = 0;
            uint seconds = 0;

            int lastComponentNumber = -1;
            int position = 1;
            int numberStart = -1;
            bool isTime = false;

            while (position < input.Length)
            {
                char c = input[position];
                if (c == 'T')
                {
                    isTime = true;
                    lastComponentNumber = 3; // day
                }
                else if (char.IsLetter(c))
                {
                    if (numberStart < 0 || numberStart >= position)
                        return false; // No number preceding letter
                    
                    string numberString = input.Substring(numberStart, position - numberStart);
                    if (!uint.TryParse(numberString, out uint value))
                        return false; // Not a valid number

                    // Check component order
                    int componentNumber = GetComponentNumber(c);
                    if (componentNumber < 0)
                        return false; // invalid character
                    if (componentNumber >= 4 && !isTime)
                        return false; // Time component before the time specified
                    if (componentNumber <= lastComponentNumber)
                        return false; // Components in wrong order

                    switch(c)
                    {
                        case 'Y':
                            years = value;
                            break;
                        case 'M' when !isTime:
                            months = value;
                            break;
                        case 'W':
                            weeks = value;
                            break;
                        case 'D':
                            days = value;
                            break;
                        case 'H':
                            hours = value;
                            break;
                        case 'M' when isTime:
                            minutes = value;
                            break;
                        case 'S':
                            seconds = value;
                            break;
                    }

                    numberStart = -1;
                    lastComponentNumber = componentNumber;
                }
                else if (char.IsDigit(c))
                {
                    if (numberStart < 0)
                        numberStart = position;
                }

                position++;
            }

            if (lastComponentNumber < 0)
                return false; // No component was specified
            if (isTime && lastComponentNumber < 4)
                return false; // We've seen the time specified, but no time component was specified

            duration = new Duration(years, months, weeks, days, hours, minutes, seconds);
            return true;

            int GetComponentNumber(char c)
            {
                switch (c)
                {
                    case 'Y': return 0;
                    case 'M' when !isTime: return 1;
                    case 'W': return 2;
                    case 'D': return 3;
                    case 'H': return 4;
                    case 'M' when isTime: return 5;
                    case 'S': return 6;
                    default: return -1;
                }
            }
        }
    }
}
