using System.Text.RegularExpressions;

namespace TimeLib
{
    public readonly struct TimePeriod : IEquatable<TimePeriod>, IComparable, IComparable<TimePeriod>
    {
        /// <summary>
        /// Gets the total number of seconds in this TimePeriod object.
        /// </summary>
        public readonly long Seconds { get; }

        #region ===== Constructors =====

        /// <summary>
        /// Initializes a new instance of the TimePeriod class using the specified number of hours, minutes, and seconds.
        /// </summary>
        /// <param name="hours">The number of hours.</param>
        /// <param name="minutes">The number of minutes.</param>
        /// <param name="seconds">The number of seconds.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when any of the arguments is negative or minutes/seconds are greater than 59.</exception>
        public TimePeriod(long hours, long minutes, long seconds)
        {
            if(hours < 0 || minutes < 0 || minutes > 59 || seconds < 0 || seconds > 59)
                throw new ArgumentOutOfRangeException();

            Seconds = (hours * 3600) + (minutes * 60) + seconds;
        }

        /// <summary>
        /// Initializes a new instance of the TimePeriod class using the specified number of hours and minutes, and sets the number of seconds to zero.
        /// </summary>
        /// <param name="hours">The number of hours.</param>
        /// <param name="minutes">The number of minutes.</param>
        public TimePeriod(long hours, long minutes): this(hours, minutes, 0) { }

        /// <summary>
        /// Initializes a new instance of the TimePeriod class using the specified number of seconds and sets the number of hours and minutes to zero.
        /// </summary>
        /// <param name="seconds">The number of seconds.</param>
        public TimePeriod(long seconds): this(0, 0, seconds) { }

        public TimePeriod(Time left, Time right)
        {
            long leftTotalSeconds = (left.Hours * 3600) + (left.Minutes * 60) + left.Seconds;
            long rightTotalSeconds = (right.Hours * 3600) + (right.Minutes * 60) + right.Seconds;

            if(leftTotalSeconds > rightTotalSeconds) 
                throw new ArgumentException("The total seconds of the left time are greater than the right time");

            Seconds = rightTotalSeconds - leftTotalSeconds;
        }

        /// <summary>
        /// Initializes a new instance of the TimePeriod class using the specified string representation of time.
        /// </summary>
        /// <param name="timeText">The string representation of time.</param>
        /// <exception cref="ArgumentException">Thrown when the input is null or empty.</exception>
        /// <exception cref="FormatException">Thrown when the input is not in a valid time format.</exception>
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

        /// <summary>
        /// Determines whether the specified object is equal to the current TimePeriod object.
        /// </summary>
        /// <param name="obj">The object to compare with the current TimePeriod object.</param>
        /// <returns>true if the specified object is equal to the current TimePeriod object; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;

            if (obj is TimePeriod period)
                return Equals(period);
            
            return false;
        }

        /// <summary>
        /// Determines whether the specified TimePeriod object is equal to the current TimePeriod object.
        /// </summary>
        /// <param name="other">The TimePeriod object to compare with the current TimePeriod object.</param>
        /// <returns>true if the specified TimePeriod object is equal to the current TimePeriod object; otherwise, false.</returns>
        public bool Equals(TimePeriod other) => Seconds == other.Seconds;

        public override int GetHashCode() => (Seconds).GetHashCode();

        public static bool operator ==(TimePeriod left, TimePeriod right) => left.Equals(right);

        public static bool operator !=(TimePeriod left, TimePeriod right) => !left.Equals(right);

        #endregion

        #region ===== Comparable =====

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance
        /// precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        /// <exception cref="ArgumentException">Thrown when the given argument is not of type TimePeriod.</exception>
        public int CompareTo(object? obj)
        {
            if (obj is null) return +1;

            if (obj is not TimePeriod)
                throw new ArgumentException("Given argument is not type of TimePeriod");

            var other = (TimePeriod)obj;

            return this.CompareTo(other);
        }

        /// <summary>
        /// Compares the current instance with another TimePeriod object and returns an integer that indicates whether the current instance
        /// precedes, follows, or occurs in the same position in the sort order as the other TimePeriod object.
        /// </summary>
        /// <param name="other">The TimePeriod object to compare with the current instance.</param>
        /// <returns>A value that indicates the relative order of the TimePeriod objects being compared.</returns>
        public int CompareTo(TimePeriod other) => Seconds.CompareTo(other.Seconds);
     
        public static bool operator >(TimePeriod left, TimePeriod right) => left.CompareTo(right) > 0;

        public static bool operator <(TimePeriod left, TimePeriod right) => left.CompareTo(right) < 0;

        public static bool operator >=(TimePeriod left, TimePeriod right) => left.CompareTo(right) >= 0;

        public static bool operator <=(TimePeriod left, TimePeriod right) => left.CompareTo(right) <= 0;

        #endregion

        #region ===== Arithmetic operation =====

        /// <summary>
        /// Adds two time periods together and returns the sum as a new TimePeriod object.
        /// </summary>
        /// <param name="left">The first TimePeriod to add.</param>
        /// <param name="right">The second TimePeriod to add.</param>
        /// <returns>A new TimePeriod representing the sum of the two input TimePeriods.</returns>
        public static TimePeriod Plus(TimePeriod left, TimePeriod right)
        {
            long totalSeconds = left.Seconds + right.Seconds;
            long hours = totalSeconds / 3600;
            long minutes = (totalSeconds % 3600) / 60;
            long seconds = totalSeconds % 60;

            return new TimePeriod(hours, minutes, seconds); 
        }

        /// <summary>
        /// Adds a TimePeriod to the current instance and returns the sum as a new TimePeriod object.
        /// </summary>
        /// <param name="timePeriod">The TimePeriod to add to the current instance.</param>
        /// <returns>A new TimePeriod representing the sum of the current instance and the input TimePeriod.</returns>
        public TimePeriod Plus(TimePeriod timePeriod) => Plus(this, timePeriod);

        public static TimePeriod operator +(TimePeriod left, TimePeriod right) => Plus(left, right);

        /// <summary>
        /// Subtracts one TimePeriod from another and returns the result as a new TimePeriod object.
        /// </summary>
        /// <param name="left">The TimePeriod to subtract from.</param>
        /// <param name="right">The TimePeriod to subtract.</param>
        /// <returns>A new TimePeriod representing the result of the subtraction.</returns>
        /// <exception cref="ArgumentException">Thrown when the result is negative.</exception>
        public static TimePeriod Minus(TimePeriod left, TimePeriod right)
        {
            long totalSeconds = left.Seconds - right.Seconds;

            if (totalSeconds < 0)
                throw new ArgumentException();

            long hours = totalSeconds / 3600;
            long minutes = (totalSeconds % 3600) / 60;
            long seconds = totalSeconds % 60;

            return new TimePeriod(hours, minutes, seconds);
        }

        /// <summary>
        /// Subtracts a TimePeriod from the current instance and returns the result as a new TimePeriod object.
        /// </summary>
        /// <param name="timePeriod">The TimePeriod to subtract from the current instance.</param>
        /// <returns>A new TimePeriod representing the result of the subtraction.</returns>
        /// <exception cref="ArgumentException">Thrown when the result is negative.</exception>
        public TimePeriod Minus(TimePeriod timePeriod) => Minus(this, timePeriod);

        public static TimePeriod operator -(TimePeriod left, TimePeriod right) => Minus(left, right);

        #endregion

        #region ===== ToString =====

        /// <summary>
        /// Returns a string representation of the current TimePeriod object.
        /// </summary>
        /// <returns>A string representation of the current TimePeriod object in the format 'hours:minutes:seconds'.</returns>
        public override string ToString() => $"{Seconds / 3600}:{(Seconds / 60) % 60:D2}:{Seconds % 60:D2}";

        #endregion
    }
}
