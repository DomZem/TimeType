using System.Text.RegularExpressions;

namespace TimeLib
{
    public readonly struct Time : IEquatable<Time>, IComparable, IComparable<Time>
    {
        /// <summary>
        /// Gets the number of hours.
        /// </summary>
        public readonly byte Hours { get; }

        /// <summary>
        /// Gets the number of minutes.
        /// </summary>
        public readonly byte Minutes { get; }

        /// <summary>
        /// Gets the number of seconds.
        /// </summary>
        public readonly byte Seconds { get; }

        #region ===== Constructors =====

        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> class.
        /// </summary>
        /// <param name="hours">The number of hours (0-23).</param>
        /// <param name="minutes">The number of minutes (0-59).</param>
        /// <param name="seconds">The number of seconds (0-59).</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when any of the input values are out of range.</exception>
        public Time(byte hours, byte minutes, byte seconds)
        {
            if (hours < 0 || hours > 23 || minutes < 0 || minutes > 59 || seconds < 0 || seconds > 59)
                throw new ArgumentOutOfRangeException();

            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> class with the specified hours and minutes, setting seconds to 0.
        /// </summary>
        /// <param name="hours">The number of hours (0-23).</param>
        /// <param name="minutes">The number of minutes (0-59).</param>
        public Time(byte hours, byte minutes) : this(hours, minutes, 0) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> class with the specified hours, setting minutes and seconds to 0.
        /// </summary>
        /// <param name="hours">The number of hours (0-23).</param>
        public Time(byte hours) : this(hours, 0, 0) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> class based on the provided time string in the format "HH:mm:ss".
        /// </summary>
        /// <param name="timeText">The time string in the format "HH:mm:ss".</param>
        /// <exception cref="ArgumentException">Thrown when the input string is null or empty.</exception>
        /// <exception cref="FormatException">Thrown when the input string has an invalid format.</exception>
        public Time(string timeText)
        {
            if (String.IsNullOrEmpty(timeText))
                throw new ArgumentException();

            string regexPattern = @"^(?:[01]?\d|2[0-3]):[0-5]\d:[0-5]\d$";
            bool isMatch = Regex.IsMatch(timeText, regexPattern);

            if (!isMatch)
                throw new FormatException();

            string[] timeSplit = timeText.Split(':');

            Hours = byte.Parse(timeSplit[0]);
            Minutes = byte.Parse(timeSplit[1]);
            Seconds = byte.Parse(timeSplit[2]);
        }

        // Extra
        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> class with the current system time.
        /// </summary>
        public Time() : this((byte)DateTime.Now.Hour, (byte)DateTime.Now.Minute, (byte)DateTime.Now.Second) { }

        #endregion

        #region ===== Equatable =====

        /// <summary>
        /// Determines whether the current <see cref="Time"/> object is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns><see langword="true"/> if the current object is equal to the specified object; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;

            if (obj is Time time)
                return Equals(time);

            return false;
        }

        /// <summary>
        /// Determines whether the current <see cref="Time"/> object is equal to another <see cref="Time"/> object.
        /// </summary>
        /// <param name="other">The <see cref="Time"/> object to compare.</param>
        /// <returns><see langword="true"/> if the current object has the same hours, minutes, and seconds as the specified <see cref="Time"/> object; otherwise, <see langword="false"/>.</returns>
        public bool Equals(Time other) => Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds;

        public override int GetHashCode() => (Hours, Minutes, Seconds).GetHashCode();

        public static bool operator ==(Time left, Time right) => left.Equals(right);

        public static bool operator !=(Time left, Time right) => !left.Equals(right);

        #endregion

        #region ===== Comparable =====

        /// <summary>
        /// Compares the current <see cref="Time"/> object with another object and returns an indication of their relative order.
        /// </summary>
        /// <param name="obj">The object to compare, which must be of type <see cref="Time"/>.</param>
        /// <returns>A value indicating the relative order of the objects being compared.</returns>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="obj"/> parameter is not of type <see cref="Time"/>.</exception>
        public int CompareTo(object? obj)
        {
            if (obj is null) return +1;

            if (obj is not Time)
                throw new ArgumentException();

            var other = (Time)obj;

            return this.CompareTo(other);
        }

        /// <summary>
        /// Compares the current <see cref="Time"/> object with another <see cref="Time"/> object and returns an indication of their relative order.
        /// </summary>
        /// <param name="other">The <see cref="Time"/> object to compare.</param>
        /// <returns>A value indicating the relative order of the objects being compared.</returns>
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

        #region ===== Arithmetic operation =====

        /// <summary>
        /// Adds a <see cref="TimePeriod"/> to a <see cref="Time"/> and returns a new <see cref="Time"/> object representing the result.
        /// </summary>
        /// <param name="time">The <see cref="Time"/> object to add the <paramref name="timePeriod"/> to.</param>
        /// <param name="timePeriod">The <see cref="TimePeriod"/> to add to the <paramref name="time"/>.</param>
        /// <returns>A new <see cref="Time"/> object representing the result of adding the <paramref name="timePeriod"/> to the <paramref name="time"/>.</returns>
        public static Time Plus(Time time, TimePeriod timePeriod)
        {
            long totalSeconds = time.GetTotalSeconds() + timePeriod.Seconds;

            byte hours = (byte)(totalSeconds / 3600 % 24);
            byte minutes = (byte)(totalSeconds / 60 % 60);
            byte seconds = (byte)(totalSeconds % 60);

            return new Time(hours, minutes, seconds);
        }

        /// <summary>
        /// Adds a <see cref="TimePeriod"/> to the current <see cref="Time"/> object and returns a new <see cref="Time"/> object representing the result.
        /// </summary>
        /// <param name="timePeriod">The <see cref="TimePeriod"/> to add to the current <see cref="Time"/> object.</param>
        /// <returns>A new <see cref="Time"/> object representing the result of adding the <paramref name="timePeriod"/> to the current <see cref="Time"/> object.</returns>
        public Time Plus(TimePeriod timePeriod) => Plus(this, timePeriod);

        public static Time operator +(Time time, TimePeriod timePeriod) => Plus(time, timePeriod);

        /// <summary>
        /// Subtracts a <see cref="TimePeriod"/> from a <see cref="Time"/> and returns a new <see cref="Time"/> object representing the result.
        /// </summary>
        /// <param name="time">The <see cref="Time"/> object to subtract the <paramref name="timePeriod"/> from.</param>
        /// <param name="timePeriod">The <see cref="TimePeriod"/> to subtract from the <paramref name="time"/>.</param>
        /// <returns>A new <see cref="Time"/> object representing the result of subtracting the <paramref name="timePeriod"/> from the <paramref name="time"/>.</returns>
        public static Time Minus(Time time, TimePeriod timePeriod)
        {
            long totalSeconds = time.GetTotalSeconds() - timePeriod.Seconds;
            while (totalSeconds < 0)
                totalSeconds += 24 * 3600;
            byte hours = (byte)(totalSeconds / 3600 % 24);
            byte minutes = (byte)(totalSeconds / 60 % 60);
            byte seconds = (byte)(totalSeconds % 60);
            
            return new Time(hours, minutes, seconds);
        }

        /// <summary>
        /// Subtracts a <see cref="TimePeriod"/> from the current <see cref="Time"/> object and returns a new <see cref="Time"/> object representing the result.
        /// </summary>
        /// <param name="timePeriod">The <see cref="TimePeriod"/> to subtract from the current <see cref="Time"/> object.</param>
        /// <returns>A new <see cref="Time"/> object representing the result of subtracting the <paramref name="timePeriod"/> from the current <see cref="Time"/> object.</returns>
        public Time Minus(TimePeriod timePeriod) => Minus(this, timePeriod);

        public static Time operator -(Time time, TimePeriod timePeriod) => Minus(time, timePeriod);

        #endregion

        #region ===== ToString =====

        /// <summary>
        /// Returns a string representation of the current <see cref="Time"/> object.
        /// </summary>
        /// <returns>A string representation of the current <see cref="Time"/> object in the format "HH:MM:SS".</returns>
        public override string ToString() => $"{Hours:00}:{Minutes:00}:{Seconds:00}";

        #endregion

        private int GetTotalSeconds() => (Hours * 3600) + (Minutes * 60) + Seconds;
    }
}