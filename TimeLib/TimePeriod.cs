using System.Text.RegularExpressions;

namespace TimeLib
{
    public readonly struct TimePeriod : IEquatable<TimePeriod>, IComparable, IComparable<TimePeriod>
    {
        /// <summary>
        /// Gets the total number of seconds represented by the current <see cref="TimePeriod"/> object.
        /// </summary>
        public readonly long Seconds { get; }

        #region ===== Constructors =====

        /// <summary>
        /// Initializes a new instance of the <see cref="TimePeriod"/> class with the specified hours, minutes, and seconds.
        /// </summary>
        /// <param name="hours">The number of hours in the time period.</param>
        /// <param name="minutes">The number of minutes in the time period (between 0 and 59).</param>
        /// <param name="seconds">The number of seconds in the time period (between 0 and 59).</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when any of the parameters is negative or when the minutes or seconds exceed the valid range.
        /// </exception>
        public TimePeriod(long hours, long minutes, long seconds)
        {
            if(hours < 0 || minutes < 0 || minutes > 59 || seconds < 0 || seconds > 59)
                throw new ArgumentOutOfRangeException();

            Seconds = (hours * 3600) + (minutes * 60) + seconds;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimePeriod"/> class with the specified hours and minutes.
        /// The seconds are set to 0.
        /// </summary>
        /// <param name="hours">The number of hours in the time period.</param>
        /// <param name="minutes">The number of minutes in the time period (between 0 and 59).</param>
        public TimePeriod(long hours, long minutes): this(hours, minutes, 0) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimePeriod"/> class with the specified total seconds.
        /// </summary>
        /// <param name="seconds">The total number of seconds in the time period.</param>
        public TimePeriod(long seconds): this(0, 0, seconds) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimePeriod"/> class representing the time duration between two <see cref="Time"/> objects.
        /// </summary>
        /// <param name="left">The left boundary <see cref="Time"/> object.</param>
        /// <param name="right">The right boundary <see cref="Time"/> object.</param>
        /// <exception cref="ArgumentException">Thrown when the total seconds of the left time are greater than the right time.</exception>
        public TimePeriod(Time left, Time right)
        {
            long leftTotalSeconds = (left.Hours * 3600) + (left.Minutes * 60) + left.Seconds;
            long rightTotalSeconds = (right.Hours * 3600) + (right.Minutes * 60) + right.Seconds;

            if(leftTotalSeconds > rightTotalSeconds) 
                throw new ArgumentException("The total seconds of the left time are greater than the right time");

            Seconds = rightTotalSeconds - leftTotalSeconds;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimePeriod"/> class based on the provided string representation of a time period.
        /// </summary>
        /// <param name="timeText">The string representation of a time period.</param>
        /// <exception cref="ArgumentException">Thrown when the input string is null or empty.</exception>
        /// <exception cref="FormatException">Thrown when the input string has an invalid format.</exception>
        public TimePeriod(string timeText)
        {
            if (String.IsNullOrEmpty(timeText))
                throw new ArgumentException();

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
        /// Determines whether the current <see cref="TimePeriod"/> object is equal to the specified object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;

            if (obj is TimePeriod period)
                return Equals(period);
            
            return false;
        }

        /// <summary>
        /// Determines whether the current <see cref="TimePeriod"/> object is equal to the specified <see cref="TimePeriod"/> object.
        /// </summary>
        /// <param name="other">The <see cref="TimePeriod"/> object to compare with the current object.</param>
        /// <returns><c>true</c> if the number of seconds in <see cref="TimePeriod"/> object is equal to the current; otherwise, <c>false</c>.</returns
        public bool Equals(TimePeriod other) => Seconds == other.Seconds;

        public override int GetHashCode() => (Seconds).GetHashCode();

        public static bool operator ==(TimePeriod left, TimePeriod right) => left.Equals(right);

        public static bool operator !=(TimePeriod left, TimePeriod right) => !left.Equals(right);

        #endregion

        #region ===== Comparable =====

        /// <summary>
        /// Compares the current <see cref="TimePeriod"/> object with the specified object and returns an integer that indicates whether the current object is before, after, or at the same position as the specified object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// A signed integer that indicates the relative position of the current object and the specified object.
        /// -1 if the current object is before the specified object.
        /// 0 if the current object is at the same position as the specified object.
        /// +1 if the current object is after the specified object.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the specified object is not of type <see cref="TimePeriod"/>.</exception>
        public int CompareTo(object? obj)
        {
            if (obj is null) return +1;

            if (obj is not TimePeriod)
                throw new ArgumentException("Given argument is not type of TimePeriod");

            var other = (TimePeriod)obj;

            return this.CompareTo(other);
        }

        /// <summary>
        /// Compares the current <see cref="TimePeriod"/> object with another <see cref="TimePeriod"/> object and returns an integer that indicates whether the current object is before, after, or at the same position as the other object.
        /// </summary>
        /// <param name="other">The <see cref="TimePeriod"/> object to compare with the current object.</param>
        /// <returns>
        /// A signed integer that indicates the relative position of the current object and the other object.
        /// -1 if the current object is before the other object.
        /// 0 if the current object is at the same position as the other object.
        /// +1 if the current object is after the other object.
        /// </returns>
        public int CompareTo(TimePeriod other) => Seconds.CompareTo(other.Seconds);
     
        public static bool operator >(TimePeriod left, TimePeriod right) => left.CompareTo(right) > 0;

        public static bool operator <(TimePeriod left, TimePeriod right) => left.CompareTo(right) < 0;

        public static bool operator >=(TimePeriod left, TimePeriod right) => left.CompareTo(right) >= 0;

        public static bool operator <=(TimePeriod left, TimePeriod right) => left.CompareTo(right) <= 0;

        #endregion

        #region ===== Arithmetic operation =====

        /// <summary>
        /// Adds two <see cref="TimePeriod"/> objects and returns a new <see cref="TimePeriod"/> representing the sum.
        /// </summary>
        /// <param name="left">The first <see cref="TimePeriod"/> object to add.</param>
        /// <param name="right">The second <see cref="TimePeriod"/> object to add.</param>
        /// <returns>A new <see cref="TimePeriod"/> object representing the sum of the two input objects.</returns>
        public static TimePeriod Plus(TimePeriod left, TimePeriod right)
        {
            long totalSeconds = left.Seconds + right.Seconds;
            long hours = totalSeconds / 3600;
            long minutes = (totalSeconds % 3600) / 60;
            long seconds = totalSeconds % 60;

            return new TimePeriod(hours, minutes, seconds); 
        }

        /// <summary>
        /// Adds the specified <see cref="TimePeriod"/> object to the current <see cref="TimePeriod"/> object and returns a new <see cref="TimePeriod"/> representing the sum.
        /// </summary>
        /// <param name="timePeriod">The <see cref="TimePeriod"/> object to add.</param>
        /// <returns>A new <see cref="TimePeriod"/> object representing the sum of the current object and the specified object.</returns>
        public TimePeriod Plus(TimePeriod timePeriod) => Plus(this, timePeriod);

        public static TimePeriod operator +(TimePeriod left, TimePeriod right) => Plus(left, right);

        /// <summary>
        /// Subtracts the specified <see cref="TimePeriod"/> object from another <see cref="TimePeriod"/> object and returns a new <see cref="TimePeriod"/> representing the difference.
        /// </summary>
        /// <param name="left">The <see cref="TimePeriod"/> object to subtract from.</param>
        /// <param name="right">The <see cref="TimePeriod"/> object to subtract.</param>
        /// <returns>A new <see cref="TimePeriod"/> object representing the difference between the two input objects.</returns>
        /// <exception cref="ArgumentException">Thrown when the resulting difference is negative.</exception>
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
        /// Subtracts the specified <see cref="TimePeriod"/> object from the current <see cref="TimePeriod"/> object and returns a new <see cref="TimePeriod"/> representing the difference.
        /// </summary>
        /// <param name="timePeriod">The <see cref="TimePeriod"/> object to subtract.</param>
        /// <returns>A new <see cref="TimePeriod"/> object representing the difference between the current object and the specified object.</returns>
        /// <exception cref="ArgumentException">Thrown when the resulting difference is negative.</exception>
        public TimePeriod Minus(TimePeriod timePeriod) => Minus(this, timePeriod);

        public static TimePeriod operator -(TimePeriod left, TimePeriod right) => Minus(left, right);

        #endregion

        #region ===== ToString =====

        /// <summary>
        /// Returns a string that represents the current <see cref="TimePeriod"/> object.
        /// The string is formatted as "hours:minutes:seconds".
        /// </summary>
        /// <returns>A string representation of the current <see cref="TimePeriod"/> object.</returns>
        public override string ToString() => $"{Seconds / 3600}:{(Seconds / 60) % 60:D2}:{Seconds % 60:D2}";

        #endregion
    }
}
