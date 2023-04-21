using System.Text.RegularExpressions;

namespace TimeLib
{
    public readonly struct Time : IEquatable<Time>, IComparable<Time>
    {
        public readonly byte Hours { get; }

        public readonly byte Minutes { get; }

        public readonly byte Seconds { get; }

        #region ===== Constructors =====

        public Time(byte hours, byte minutes, byte seconds)
        {
            if (hours < 0 || hours > 23 || minutes < 0 || minutes > 59 || seconds < 0 || seconds > 59)
                throw new ArgumentOutOfRangeException();

            Hours = hours;
            Minutes = minutes;  
            Seconds = seconds;  
        }

        public Time(byte hours, byte minutes): this(hours, minutes, 0) { }
    
        public Time(byte hours): this(hours, 0, 0) { }
        
        public Time(string timeText)
        {
            if (String.IsNullOrEmpty(timeText))
                throw new ArgumentException();

            string regexPattern = @"^(?:[01]?\d|2[0-3]):[0-5]\d:[0-5]\d$";
            bool isMatch = Regex.IsMatch(timeText, regexPattern);

            if (!isMatch)
                throw new ArgumentException();

            string[] timeSplit = timeText.Split(':');

            Hours = byte.Parse(timeSplit[0]);
            Minutes = byte.Parse(timeSplit[1]);
            Seconds = byte.Parse(timeSplit[2]);
        }

        public Time(): this((byte)DateTime.Now.Hour, (byte)DateTime.Now.Minute, (byte)DateTime.Now.Second) { }

        #endregion

        #region ===== Equatable =====

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;

            if (obj is Time)
            {
                return Equals((Time)obj);
            }

            return false;
        }

        public bool Equals(Time other) => Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds;

        public override int GetHashCode() => (Hours, Minutes, Seconds).GetHashCode();

        public static bool operator ==(Time left, Time right) => left.Equals(right);

        public static bool operator !=(Time left, Time right) => !left.Equals(right);

        #endregion

        #region ===== Comparable =====

        public int CompareTo(Time other)
        {
            if (Hours != other.Hours)
                return Hours.CompareTo(other.Hours);
            
            if (!Minutes.Equals(other.Minutes))
                return Minutes.CompareTo(other.Minutes);

            return Seconds.CompareTo(other.Seconds);
        }

        public static bool operator >(Time left, Time right) => left.CompareTo(right) > 0;

        public static bool operator <(Time left, Time right) => left.CompareTo(right) < 0;

        public static bool operator >=(Time left, Time right) => left.CompareTo(right) >= 0;

        public static bool operator <=(Time left, Time right) => left.CompareTo(right) <= 0;

        #endregion

        public override string ToString() => $"{Hours:00}:{Minutes:00}:{Seconds:00}";
    }
}