using System.Text.RegularExpressions;

namespace TimeLib
{
    public readonly struct TimePeriod : IEquatable<TimePeriod>, IComparable, IComparable<TimePeriod>
    {
        /// <summary>
        /// Gets the total number of seconds.
        /// </summary>
        public readonly long Seconds { get; }

        #region ===== Constructors =====

        /// <summary>
        ///  Initializes a new instance of the <see cref="TimePeriod"/> class with the specified number of hours, minutes and seconds.
        /// </summary>
        /// <param name="hours">The number of hours in the time period.</param>
        /// <param name="minutes">The number of minutes in the time period.</param>
        /// <param name="seconds">The number of seconds in the time period.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="hours"/> is negative, <paramref name="minutes"/> is negative or greater than 59,
        /// or <paramref name="seconds"/> is negative or greater than 59.
        /// </exception>
        public TimePeriod(long hours, long minutes, long seconds)
        {
            if(hours < 0 || minutes < 0 || minutes > 59 || seconds < 0 || seconds > 59)
                throw new ArgumentOutOfRangeException();

            Seconds = (hours * 3600) + (minutes * 60) + seconds;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimePeriod"/> class with the specified number of hours and minutes, setting seconds to 0.
        /// </summary>
        /// <param name="hours">The number of hours in the time period.</param>
        /// <param name="minutes">The number of minutes in the time period.</param>
        public TimePeriod(long hours, long minutes): this(hours, minutes, 0) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimePeriod"/> class
        /// using the given number of seconds. Hours and minutes are set to zero.
        /// </summary>
        /// <param name="seconds">The number of seconds in the time period.</param>
        public TimePeriod(long seconds): this(0, 0, seconds) { }

        /// <summary>
        ///  Initializes a new instance of the <see cref="TimePeriod"/> class using two Time objects. 
        ///  Calculates the difference in seconds between the two Time objects and sets the Seconds property accordingly.
        /// </summary>
        /// <param name="left">The left operand <see cref="Time"/> object.</param>
        /// <param name="right">The right operand <see cref="Time"/> object.</param>
        public TimePeriod(Time left, Time right)
        {
            long leftTotalSeconds = (left.Hours * 3600) + (left.Minutes * 60) + left.Seconds;
            long rightTotalSeconds = (right.Hours * 3600) + (right.Minutes * 60) + right.Seconds;

            Seconds = leftTotalSeconds >= rightTotalSeconds ? leftTotalSeconds - rightTotalSeconds : rightTotalSeconds - leftTotalSeconds;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimePeriod"/> class with the specified time in string format "hh:mm:ss".
        /// </summary>
        /// <param name="timeText">A string that represents the time period in the format "hh:mm:ss"</param>
        /// <exception cref="ArgumentException">Thrown when the input string is null or empty.</exception>
        /// <exception cref="FormatException">Thrown when the input string cannot be parsed into a valid time period.</exception>
        public TimePeriod(string timeText)
        {
            if (String.IsNullOrEmpty(timeText))
                throw new ArgumentException("Input is null or empty");

            string regexPattern = @"^([1-9]?[0-9]+)((:[0-5]?[0-9]){0,2}|((:[0-5]?[0-9]){2}\.([0]{0,3}|[1-9][0-9]{0,2})))$";
            bool isMatch = Regex.IsMatch(timeText, regexPattern);

            if (!isMatch)
                throw new FormatException();

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

        /// <summary>
        /// Determines whether this <see cref="TimePeriod"/> instance is equal to another <see cref="TimePeriod"/> instance.
        /// </summary>
        /// <param name="other">The <see cref="TimePeriod"/> object to compare with this instance.</param>
        /// <returns><c>true</c> if the two instances have the same number of seconds; otherwise, <c>false</c>.</returns>
        public bool Equals(TimePeriod other) => Seconds == other.Seconds;

        public override int GetHashCode() => (Seconds).GetHashCode();

        public static bool operator ==(TimePeriod left, TimePeriod right) => left.Equals(right);

        public static bool operator !=(TimePeriod left, TimePeriod right) => !left.Equals(right);

        #endregion

        #region ===== Comparable =====

        /// <summary>
        /// Compares this TimePeriod object with another object, returning an integer that indicates the relationship between the two objects.
        /// </summary>
        /// <param name="obj">The object to compare to this TimePeriod object.</param>
        /// <returns>
        /// A value less than zero if this TimePeriod object is less than the <paramref name="obj"/> argument,
        /// zero if this TimePeriod object is equal to the <paramref name="obj"/> argument, and a value greater
        /// than zero if this TimePeriod object is greater than the <paramref name="obj"/> argument.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="obj"/> argument is not a TimePeriod object.
        /// </exception>
        public int CompareTo(object? obj)
        {
            if (obj is null) return +1;

            if (obj is not TimePeriod)
                throw new ArgumentException("Given argument is not type of TimePeriod");

            var other = (TimePeriod)obj;

            return this.CompareTo(other);
        }

        /// <summary>
        /// Compares the current <see cref="TimePeriod"/> object with another <see cref="TimePeriod"/> object and returns an integer that indicates their relative positions in the sort order.
        /// </summary>
        /// <param name="other">The <see cref="TimePeriod"/> object to compare with the current object.</param>
        /// <returns>A signed integer that indicates the relative position of the current object and the <paramref name="other"/> parameter in the sort order.</returns>
        public int CompareTo(TimePeriod other) => Seconds.CompareTo(other.Seconds);
     
        public static bool operator >(TimePeriod left, TimePeriod right) => left.CompareTo(right) > 0;

        public static bool operator <(TimePeriod left, TimePeriod right) => left.CompareTo(right) < 0;

        public static bool operator >=(TimePeriod left, TimePeriod right) => left.CompareTo(right) >= 0;

        public static bool operator <=(TimePeriod left, TimePeriod right) => left.CompareTo(right) <= 0;

        #endregion

        #region ===== Arithmetic operation =====

        #endregion

        #region ===== ToString =====

        /// <summary>
        /// Returns the string representation of this <see cref="TimePeriod"/> object in the format "hh:mm:ss".
        /// </summary>
        /// <returns>The string representation of this <see cref="TimePeriod"/> object.</returns>
        public override string ToString() => $"{Seconds / 3600}:{(Seconds / 60) % 60:D2}:{Seconds % 60:D2}";

        #endregion
    }
}
