using System.Text.RegularExpressions;

namespace TimeLib
{
    public readonly struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        public readonly long Seconds { get; }

        #region ===== Constructors =====
        
        public TimePeriod(long hours, long minutes, long seconds)
        {
            if(hours < 0 || minutes < 0 || minutes > 59 || seconds < 0 || seconds > 59)
                throw new ArgumentOutOfRangeException();

            Seconds = (hours * 3600) + (minutes * 60) + seconds;
        }

        public TimePeriod(long hours, long minutes): this(hours, minutes, 0) { }
        
        public TimePeriod(long seconds): this(0, 0, seconds) { }

        public TimePeriod(Time left, Time right)
        {
            long leftTotalSeconds = (left.Hours * 3600) + (left.Minutes * 60) + left.Seconds;
            long rightTotalSeconds = (right.Hours * 3600) + (right.Minutes * 60) + right.Seconds;

            Seconds = leftTotalSeconds >= rightTotalSeconds ? leftTotalSeconds - rightTotalSeconds : rightTotalSeconds - leftTotalSeconds;
        }

        public TimePeriod(string timeText)
        {
            if (String.IsNullOrEmpty(timeText))
                throw new ArgumentException();

            string regexPattern = @"^(?:[01]\d|2[0-3]):[0-5]\d:[0-5]\d$";
            Regex regex = new Regex(regexPattern);
            bool isMatch = regex.IsMatch(timeText);

            if (!isMatch)
                throw new ArgumentException();

            string[] timeSplit = timeText.Split(':');
            
            long hours = long.Parse(timeSplit[0]);
            long minutes = long.Parse(timeSplit[1]);
            long seconds = long.Parse(timeSplit[2]);

            Seconds = (hours * 3600) + (minutes * 60) + seconds;
        }

        #endregion

        #region ===== Equatable =====

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;

            if (obj is TimePeriod period)
                return Equals(period);
            
            return false;
        }

        public bool Equals(TimePeriod other) => Seconds == other.Seconds;

        public override int GetHashCode() => (Seconds).GetHashCode();

        public static bool operator ==(TimePeriod left, TimePeriod right) => left.Equals(right);

        public static bool operator !=(TimePeriod left, TimePeriod right) => !left.Equals(right);

        #endregion

        #region ===== Comparable =====

        public int CompareTo(TimePeriod other) => Seconds.CompareTo(other.Seconds);
     
        public static bool operator >(TimePeriod left, TimePeriod right) => left.CompareTo(right) > 0;

        public static bool operator <(TimePeriod left, TimePeriod right) => left.CompareTo(right) < 0;

        public static bool operator >=(TimePeriod left, TimePeriod right) => left.CompareTo(right) >= 0;

        public static bool operator <=(TimePeriod left, TimePeriod right) => left.CompareTo(right) <= 0;

        #endregion

        public override string ToString() => $"{Seconds / 3600}:{(Seconds / 60) % 60:D2}:{Seconds % 60:D2}";

    }
}
