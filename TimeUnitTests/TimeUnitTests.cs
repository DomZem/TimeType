using TimeLib;

namespace TimeUnitTests
{
    [TestClass]
    public class TimeUnitTests
    {
        #region ===== Constructor(Hours, Minutes, Seconds) =====

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)12, (byte)30, (byte)45, 
                 (byte)12, (byte)30, (byte)45)]
        [DataRow((byte)11, (byte)29, (byte)59,
                 (byte)11, (byte)29, (byte)59)]
        [DataRow((byte)1, (byte)59, (byte)59,
                 (byte)1, (byte)59, (byte)59)]
        public void Constructor_HoursMinutesSecondsParams_SetsExpectedProperties(byte hours, byte minutes, byte seconds, byte expectedHours, byte expectedMinutes, byte expectedSeconds)
        {
            var time = new Time(hours, minutes, seconds);

            Assert.AreEqual(time.Hours, expectedHours);
            Assert.AreEqual(time.Minutes, expectedMinutes);
            Assert.AreEqual(time.Seconds, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)11, (byte)0, (byte)60)]
        [DataRow((byte)11, (byte)60, (byte)0)]
        [DataRow((byte)24, (byte)40, (byte)0)]
        public void Constructor_HoursMinutesSecondsParams_ThrowsArgumentOutOfRangeException(byte hours, byte minutes, byte seconds)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Time(hours, minutes, seconds));
        }

        #endregion

        #region ===== Constructor(Hours, Minutes) =====
        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)12, (byte)30,
                 (byte)12, (byte)30)]
        [DataRow((byte)11, (byte)29,
                 (byte)11, (byte)29)]
        [DataRow((byte)1, (byte)59,
                 (byte)1, (byte)59)]
        public void Constructor_HoursMinutesParam_SetsExpectedProperties(byte hours, byte minutes, byte expectedHours, byte expectedMinutes)
        {
            var time = new Time(hours, minutes);

            Assert.AreEqual(time.Hours, expectedHours);
            Assert.AreEqual(time.Minutes, expectedMinutes);
            Assert.AreEqual(time.Seconds, 0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)11, (byte)60)]
        [DataRow((byte)24, (byte)40)]
        public void Constructor_HoursMinutesParam_ThrowsArgumentOutOfRangeException(byte hours, byte minutes)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Time(hours, minutes));
        }

        #endregion

        #region ===== Constructor(Hours) =====

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)12,
                 (byte)12)]
        [DataRow((byte)11, 
                 (byte)11)]
        [DataRow((byte)1,
                 (byte)1)]
        public void Constructor_HoursParam_SetsExpectedProperties(byte hours, byte expectedHours)
        {
            var time = new Time(hours);

            Assert.AreEqual(time.Hours, expectedHours);
            Assert.AreEqual(time.Minutes, 0);
            Assert.AreEqual(time.Seconds, 0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)24)]
        public void Constructor_HoursParam_ThrowsArgumentOutOfRangeException(byte hours)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Time(hours));
        }

        #endregion

        #region ===== Constructor(Text) =====

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("12:30:45", (byte)12, (byte)30, (byte)45)]
        [DataRow("11:29:59", (byte)11, (byte)29, (byte)59)]
        [DataRow("1:59:59", (byte)1, (byte)59, (byte)59)]
        [DataRow("23:59:59", (byte)23, (byte)59, (byte)59)]
        public void Constructor_TextParam_SetsExpectedProperties(string text, byte expectedHours, byte expectedMinutes, byte expectedSeconds)
        {
            var time = new Time(text);

            Assert.AreEqual(time.Hours, expectedHours);
            Assert.AreEqual(time.Minutes, expectedMinutes);
            Assert.AreEqual(time.Seconds, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("")]
        [DataRow("23:50:60")]
        [DataRow("23:50:-1")]
        [DataRow("23:60:00")]
        [DataRow("23:-1:00")]
        [DataRow("24:12:00")]
        public void Constructor_TextParam_ThrowsArgumentOutOfRangeException(string text)
        {
            Assert.ThrowsException<ArgumentException>(() => new Time(text));
        }

        #endregion
    }
}