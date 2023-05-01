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
        public void Constructor_HoursMinutesSecondsArguments_SetsExpectedProperties(byte hours, byte minutes, byte seconds, byte expectedHours, byte expectedMinutes, byte expectedSeconds)
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
        public void Constructor_HoursMinutesSecondsArguments_ThrowsArgumentOutOfRangeException(byte hours, byte minutes, byte seconds)
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
        public void Constructor_HoursMinutesArguments_SetsExpectedProperties(byte hours, byte minutes, byte expectedHours, byte expectedMinutes)
        {
            var time = new Time(hours, minutes);

            Assert.AreEqual(time.Hours, expectedHours);
            Assert.AreEqual(time.Minutes, expectedMinutes);
            Assert.AreEqual(time.Seconds, 0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)11, (byte)60)]
        [DataRow((byte)24, (byte)40)]
        public void Constructor_HoursMinutesArguments_ThrowsArgumentOutOfRangeException(byte hours, byte minutes)
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
        public void Constructor_HoursArgument_SetsExpectedProperties(byte hours, byte expectedHours)
        {
            var time = new Time(hours);

            Assert.AreEqual(time.Hours, expectedHours);
            Assert.AreEqual(time.Minutes, 0);
            Assert.AreEqual(time.Seconds, 0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)24)]
        public void Constructor_HoursArgument_ThrowsArgumentOutOfRangeException(byte hours)
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
        public void Constructor_TextArgument_SetsExpectedProperties(string text, byte expectedHours, byte expectedMinutes, byte expectedSeconds)
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
        public void Constructor_TextArgument_ThrowsArgumentOutOfRangeException(string text)
        {
            Assert.ThrowsException<ArgumentException>(() => new Time(text));
        }

        #endregion

        #region ===== Equatable =====

        [TestMethod, TestCategory("Equals")]
        public void EqualMethod_NullArgument_ReturnsFalse()
        {
            Assert.IsFalse((new Time()).Equals(null));
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualMethod_OtherTypeArgument_ReturnsFalse()
        {
            Time t = new();
            var anonymousTypeVariable = new { x = 0, y = 1 };
            Assert.IsFalse(t.Equals(anonymousTypeVariable));
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualMethod_SameReferenceObjectArgument_ReturnsTrue()
        {
            Time t1 = new();
            Time t2 = t1;
            Assert.IsTrue(t1.Equals(t2));
        }

        [TestMethod, TestCategory("Equals")]
        [DataTestMethod]
        [DataRow("12:34:56", "12:34:56", true)]
        [DataRow("00:00:00", "00:00:00", true)]
        [DataRow("00:00:01", "00:00:00", false)]
        [DataRow("23:59:59", "00:00:00", false)]
        [DataRow("15:45:00", "23:59:59", false)]
        [DataRow("03:45:21", "03:45:22", false)]
        public void EqualMethod_ReturnsExpectedResult(string time1Text, string time2Text, bool expectedResult)
        {
            Time t1 = new(time1Text);
            Time t2 = new(time2Text);
            Assert.AreEqual(expectedResult, t1.Equals(t2));
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualOperator_ComapreWithNull_ReturnsFalse()
        {
            Assert.IsFalse(new Time() == null);
        }

        [TestMethod, TestCategory("Equals")]
        public void NotEqualOperator_CompareWithNull_ReturnsTrue()
        {
            Assert.IsTrue(new Time() != null);
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualOperator_CompareWithSameReference_ReturnsTrue()
        {
            Time t1 = new();
            Time t2 = t1;
            Assert.IsTrue(t1 == t2);
        }

        [TestMethod, TestCategory("Equals")]
        public void NotEqualOperator_CompareWithSameReference_ReturnsFalse()
        {
            Time t1 = new();
            Time t2 = t1;
            Assert.IsFalse(t1 != t2);
        }

        [TestMethod, TestCategory("Equals")]
        [DataTestMethod]
        [DataRow("12:30:00", "==", "12:30:00", true)]
        [DataRow("12:30:00", "==", "12:31:00", false)]
        [DataRow("12:30:00", "!=", "12:30:00", false)]
        [DataRow("12:30:00", "!=", "12:31:00", true)]
        [DataRow("00:00:00", "==", "23:59:59", false)]
        [DataRow("00:00:00", "!=", "23:59:59", true)]
        [DataRow("12:00:00", "==", "15:00:00", false)]
        [DataRow("12:00:00", "!=", "15:00:00", true)]
        [DataRow("23:59:59", "==", "00:00:00", false)]
        [DataRow("23:59:59", "!=", "00:00:00", true)]
        public void EqualOperator_ReturnsExpectedResult(string time1Text, string operatorText, string time2Text, bool expectedResult)
        {
            Time time1 = new(time1Text);
            Time time2 = new(time2Text);

            switch (operatorText)
            {
                case "==":
                    Assert.AreEqual(expectedResult, time1 == time2);
                    break;
                case "!=":
                    Assert.AreEqual(expectedResult, time1 != time2);
                    break;
            }
        }

        #endregion

        #region ===== Comparable =====

        [TestMethod, TestCategory("Compares")]
        public void CompareToMethod_Null_ReturnsPositiveValue()
        {
            Time time = new Time(1, 2, 3);
            int result = time.CompareTo(null);
            Assert.IsTrue(result > 0);
        }

        [TestMethod, TestCategory("Compares")]
        public void CompareToMethod_NonTimeObject_ThrowsArgumentException()
        {
            Time time = new Time(1, 2, 3);
            Assert.ThrowsException<ArgumentException>(() => time.CompareTo("not a time object"));
        }

        [TestMethod, TestCategory("Compares")]
        public void CompareToMethod_SameTime_ReturnsZero()
        {
            Time time1 = new Time(1, 2, 3);
            Time time2 = new Time(1, 2, 3);
            int result = time1.CompareTo(time2);
            Assert.AreEqual(0, result);
        }

        [TestMethod, TestCategory("Compares")]
        public void CompareToMethod_EarlierTime_ReturnsNegativeValue()
        {
            Time time1 = new Time(1, 2, 3);
            Time time2 = new Time(1, 3, 3);
            int result = time1.CompareTo(time2);
            Assert.IsTrue(result < 0);
        }

        [TestMethod, TestCategory("Compares")]
        public void CompareToMethod_LaterTime_ReturnsPositiveValue()
        {
            Time time1 = new Time(1, 2, 3);
            Time time2 = new Time(1, 1, 3);
            int result = time1.CompareTo(time2);
            Assert.IsTrue(result > 0);
        }

        [TestMethod, TestCategory("Compares")]
        [DataTestMethod]
        [DataRow("1:00:00", ">",  "2:00:00", false)]
        [DataRow("1:00:00", ">",  "1:01:00", false)]
        [DataRow("1:00:00", ">",  "1:00:01", false)]
        [DataRow("1:00:00", ">",  "1:00:00", false)]
        [DataRow("1:00:00", ">=", "2:00:00", false)]
        [DataRow("1:00:00", ">=", "1:01:00", false)]
        [DataRow("1:00:00", ">=", "1:00:01", false)]
        [DataRow("1:00:00", ">=", "1:00:00", true)]
        [DataRow("1:00:00", "<",  "2:00:00", true)]
        [DataRow("1:00:00", "<", "1:01:00", true)]
        [DataRow("1:00:00", "<", "1:00:01", true)]
        [DataRow("1:00:00", "<", "1:00:00", false)]
        [DataRow("1:00:00", "<=", "2:00:00", true)]
        [DataRow("1:00:00", "<=", "1:01:00", true)]
        [DataRow("1:00:00", "<=", "1:00:01", true)]
        [DataRow("1:00:00", "<=", "1:00:00", true)]
        public void CompareOperator_ReturnsExpectedResult(string time1Text, string operatorText, string time2Text, bool expectedResult)
        {
            Time t1 = new(time1Text);
            Time t2 = new(time2Text);
            
            switch(operatorText) 
            {
                case ">":
                    Assert.AreEqual(expectedResult, t1 > t2);
                    break;
                case ">=":
                    Assert.AreEqual(expectedResult, t1 >= t2);
                    break;
                case "<":
                    Assert.AreEqual(expectedResult, t1 < t2);
                    break;
                case "<=":
                    Assert.AreEqual(expectedResult, t1 <= t2);
                    break;
            }
        }

        #endregion
    }
}