using System.Text.RegularExpressions;

namespace TimeLib
{
    public readonly struct Time : IEquatable<Time>, IComparable, IComparable<Time>
    {
        /// <summary>
        /// Gets the number of hours in the time.
        /// </summary>
        public readonly byte Hours { get; }

        /// <summary>
        /// Gets the number of minutes in the time.
        /// </summary>
        public readonly byte Minutes { get; }

        /// <summary>
        /// Gets the number of seconds in the time.
        /// </summary>
        public readonly byte Seconds { get; }

        #region ===== Constructors =====

        /// <summary>
        /// Creates a new Time object with the specified hours, minutes, and seconds. 
        /// </summary>
        /// <param name="hours">The number of hours in the time. Must be between 0 and 23.</param>
        /// <param name="minutes">The number of minutes in the time. Must be between 0 and 59.</param>
        /// <param name="seconds">The number of seconds in the time. Must be between 0 and 59.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if any of the parameters are out of range.</exception>
        public Time(byte hours, byte minutes, byte seconds)
        {
            if (hours < 0 || hours > 23 || minutes < 0 || minutes > 59 || seconds < 0 || seconds > 59)
                throw new ArgumentOutOfRangeException();

            Hours = hours;
            Minutes = minutes;  
            Seconds = seconds;  
        }

        /// <summary>
        /// Creates a new Time object with the specified hours and minutes. The seconds are set to 0.
        /// </summary>
        /// <param name="hours">The number of hours in the time. Must be between 0 and 23.</param>
        /// <param name="minutes">The number of hours in the time. Must be between 0 and 23.</param>
        public Time(byte hours, byte minutes): this(hours, minutes, 0) { }

        /// <summary>
        /// Creates a new Time object with the specified number of hours. The minutes and seconds are set to 0.
        /// </summary>
        /// <param name="hours">The number of hours in the time. Must be between 0 and 23.</param>
        public Time(byte hours): this(hours, 0, 0) { }

        /// <summary>
        /// Creates a new Time object from a string in the format "hh:mm:ss".
        /// </summary>
        /// <param name="timeText">The time string to parse. Must be in the format "hh:mm:ss".</param>
        /// <exception cref="ArgumentException">Thrown when the time string is null, empty or not in the correct format.</exception>
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

        // Extra
        /// <summary>
        /// Creates a new Time object initialized to the current system time.
        /// </summary>
        public Time(): this((byte)DateTime.Now.Hour, (byte)DateTime.Now.Minute, (byte)DateTime.Now.Second) { }

        #endregion

        #region ===== Equatable =====
     
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;

            if (obj is Time time)
                return Equals(time);
            
            return false;
        }

        /// <summary>
        /// Determines whether this <see cref="Time"/> instance is equal to another <see cref="Time"/> instance.
        /// </summary>
        /// <param name="other">The <see cref="Time"/> to compare with this instance.</param>
        /// <returns><c>true</c> if the two instances have the same number of hours, minutes and seconds; otherwise, <c>false</c>.</returns>
        public bool Equals(Time other) => Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds;

        public override int GetHashCode() => (Hours, Minutes, Seconds).GetHashCode();

        public static bool operator ==(Time left, Time right) => left.Equals(right);

        public static bool operator !=(Time left, Time right) => !left.Equals(right);

        #endregion

        #region ===== Comparable =====

        /// <summary>
        /// Compares this Time object with another object, returning an integer that indicates the relationship between the two objects.
        /// </summary>
        /// <param name="obj">The object to compare to this Time object.</param>
        /// <returns>
        /// A value less than zero if this Time object is less than the <paramref name="obj"/> argument,
        /// zero if this Time object is equal to the <paramref name="obj"/> argument, and a value greater
        /// than zero if this Time object is greater than the <paramref name="obj"/> argument.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="obj"/> argument is not a Time object.
        /// </exception>
        public int CompareTo(object? obj)
        {
            if (obj is null) return +1;

            if (obj is not Time)
                throw new ArgumentException();

            var other = (Time)obj;

            return this.CompareTo(other);
        }

        /// <summary>
        /// Compares the current <see cref="Time"/> object with another <see cref="Time"/> object and returns an integer that indicates their relative positions in the sort order.
        /// </summary>
        /// <param name="other">The <see cref="Time"/> object to compare with the current object.</param>
        /// <returns>A signed integer that indicates the relative position of the current object and the <paramref name="other"/> parameter in the sort order.</returns>
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

        /// <summary>
        /// Returns the string representation of this Time object in the format "hh:mm:ss".
        /// </summary>
        /// <returns>The string representation of this Time object.</returns>
        public override string ToString() => $"{Hours:00}:{Minutes:00}:{Seconds:00}";
    }
}