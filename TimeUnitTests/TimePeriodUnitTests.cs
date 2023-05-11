using TimeLib;

namespace TimeUnitTests
{
    [TestClass]
    public class TimePeriodUnitTests
    {
        #region ===== Constructor(Hours, Minutes, Seconds) =====

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0, 0, 0, 0)]
        [DataRow(1, 0, 0, 3600)]
        [DataRow(0, 30, 0, 1800)]
        [DataRow(0, 0, 30, 30)]
        [DataRow(12, 30, 45, 45_045)]
        public void Constructor_HoursMinutesSecondsArguments_SetsExpectedProperties(long hours, long minutes, long seconds, long expectedTotalSeconds)
        {
            TimePeriod timePeriod = new TimePeriod(hours, minutes, seconds);

            Assert.AreEqual(timePeriod.Seconds, expectedTotalSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11, 0, 60)]
        [DataRow(11, 60, 0)]
        [DataRow(-11, 58, 58)]
        [DataRow(12, -11, 0)]
        [DataRow(12, 12, -11)]
        public void Constructor_IncorrectHoursMinutesSecondsArguments_ThrowsArgumentOutOfRangeException(long hours, long minutes, long seconds)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new TimePeriod(hours, minutes, seconds));
        }

        #endregion

        #region ===== Constructor(Hours, Minutes) =====

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0, 0, 0)]
        [DataRow(1, 0, 3600)]
        [DataRow(0, 30, 1800)]
        [DataRow(12, 30, 45_000)]
        public void Constructor_HoursMinutesArguments_SetsExpectedProperties(long hours, long minutes, long expectedTotalSeconds)
        {
            TimePeriod timePeriod = new TimePeriod(hours, minutes);

            Assert.AreEqual(timePeriod.Seconds, expectedTotalSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11, -20)]
        [DataRow(11, 60)]
        public void Constructor_IncorrectHoursMinutesArguments_ThrowsArgumentOutOfRangeException(long hours, long minutes)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new TimePeriod(hours, minutes));
        }

        #endregion

        #region ===== Constructor(Seconds) =====

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(30, 30)]
        [DataRow(59, 59)]
        public void Constructor_SecondsArgument_SetsExpectedProperties(long seconds, long expectedTotalSeconds)
        {
            TimePeriod timePeriod = new TimePeriod(seconds);

            Assert.AreEqual(timePeriod.Seconds, expectedTotalSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-59)]
        [DataRow(60)]
        public void Constructor_IncorrectSecondsArgument_ThrowsArgumentOutOfRangeException(long seconds)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new TimePeriod(seconds));
        }

        #endregion

        #region ===== Constructor(Time1, Time2) =====

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("12:30:45", "11:30:45")]
        [DataRow("11:30:29", "11:30:28")]
        public void Constructor_TimesArguments_ThrowsArgumentException(string timeText1, string timeText2)
        {
            Time time1 = new Time(timeText1);
            Time time2 = new Time(timeText2);

            Assert.ThrowsException<ArgumentException>(() => new TimePeriod(time1, time2));
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("11:30:45", "12:30:45", "1:00:00", true)]
        [DataRow("11:30:45", "12:30:45", "0:59:59", false)]
        [DataRow("7:30:00", "21:30:00", "14:00:00", true)]
        [DataRow("10:30:15", "10:30:15", "0:00:00", true)]
        [DataRow("10:30:15", "10:30:15", "0:00:01", false)]
        [DataRow("00:00:00", "00:00:00", "0:00:00", true)]
        public void Constructor_TimesArguments_SetsExpectedResult(string timeText1, string timeText2, string timePeriodText, bool expectedResult)
        {
            Time time1 = new Time(timeText1);
            Time time2 = new Time(timeText2);

            TimePeriod timePeriod = new TimePeriod(time1, time2);

            Assert.AreEqual(expectedResult, timePeriod.ToString() == timePeriodText);
        }

        #endregion

        #region ===== Constructor(Text) =====

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("1:00:00", 3600)]
        [DataRow("0:30:00", 1800)]
        [DataRow("0:00:30", 30)]
        [DataRow("12:30:45", 45_045)]
        public void Constructor_TextArgument_SetsExpectedProperties(string text, long expectedSeconds)
        {
            TimePeriod timePeriod = new TimePeriod(text);

            Assert.AreEqual(timePeriod.Seconds, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("23:50:60")]
        [DataRow("23:50:-1")]
        [DataRow("23:60:00")]
        [DataRow("23:-1:00")]
        [DataRow("-1:12:15")]
        public void Constructor_IncorrectTextArgument_ThrowsFormatException(string text)
        {
            Assert.ThrowsException<FormatException>(() => new Time(text));
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("")]
        [DataRow(null)]
        public void Constructor_IncorrectTextArgument_ThrowsArgumentException(string text)
        {
            Assert.ThrowsException<ArgumentException>(() => new Time(text));
        }

        #endregion

        #region ===== Equatable =====

        [TestMethod, TestCategory("Equals")]
        public void EqualMethod_NullArgument_ReturnsFalse()
        {
            Assert.IsFalse((new TimePeriod(23, 10, 10)).Equals(null));
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualMethod_OtherTypeArgument_ReturnsFalse()
        {
            TimePeriod timePeriod = new TimePeriod(23, 10, 10);
            var anonymousTypeVariable = new { x = 0, y = 1 };
            Assert.IsFalse(timePeriod.Equals(anonymousTypeVariable));
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualMethod_SameReferenceObjectArgument_ReturnsTrue()
        {
            TimePeriod timePeriod1 = new TimePeriod(23, 10, 10);
            TimePeriod timePeriod2 = timePeriod1;
            Assert.IsTrue(timePeriod1.Equals(timePeriod2));
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow("12:34:56", "12:34:56", true)]
        [DataRow("127:59:48", "127:59:48", true)]
        [DataRow("7:40:34", "7:40:35", false)]
        public void EqualMethod_ReturnsExpectedResult(string timePeriod1Text, string timePeriod2Text, bool expectedResult)
        {
            TimePeriod timePeriod1 = new(timePeriod1Text);
            TimePeriod timePeriod2 = new(timePeriod2Text);
            Assert.AreEqual(expectedResult, timePeriod1.Equals(timePeriod2));
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualOperator_ComapreWithNull_ReturnsFalse()
        {
            Assert.IsFalse(new TimePeriod(23, 10, 10) == null);
        }

        [TestMethod, TestCategory("Equals")]
        public void NotEqualOperator_CompareWithNull_ReturnsTrue()
        {
            Assert.IsTrue(new TimePeriod(23, 10, 10) != null);
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualOperator_CompareWithSameReference_ReturnsTrue()
        {
            TimePeriod timePeriod1 = new TimePeriod(23, 10, 10);
            TimePeriod timePeriod2 = timePeriod1;
            Assert.IsTrue(timePeriod1 == timePeriod2);
        }

        [TestMethod, TestCategory("Equals")]
        public void NotEqualOperator_CompareWithSameReference_ReturnsFalse()
        {
            TimePeriod timePeriod1 = new TimePeriod(23, 10, 10);
            TimePeriod timePeriod2 = timePeriod1;


            Assert.IsFalse(timePeriod1 != timePeriod2);
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow("12:30:00", "==", "12:30:00", true)]
        [DataRow("12:30:00", "==", "12:31:00", false)]
        [DataRow("12:30:00", "!=", "12:30:00", false)]
        [DataRow("12:30:00", "!=", "12:31:00", true)]
        [DataRow("24:00:00", "==", "23:59:59", false)]
        [DataRow("24:00:00", "!=", "23:59:59", true)]
        [DataRow("127:23:54", "==", "127:23:54", true)]
        [DataRow("12:00:00", "!=", "15:00:00", true)]
        [DataRow("192:59:59", "==", "193:00:00", false)]
        [DataRow("192:59:59", "!=", "193:00:00", true)]
        public void EqualOperator_ReturnsExpectedResult(string timePeriod1Text, string operatorText, string timePeriod2Text, bool expectedResult)
        {
            TimePeriod timePeriod1 = new TimePeriod(timePeriod1Text);
            TimePeriod timePeriod2 = new TimePeriod(timePeriod2Text);

            switch (operatorText)
            {
                case "==":
                    Assert.AreEqual(expectedResult, timePeriod1 == timePeriod2);
                    break;
                case "!=":
                    Assert.AreEqual(expectedResult, timePeriod1 != timePeriod2);
                    break;
            }
        }

        #endregion

        #region ===== Comparable =====

        [TestMethod, TestCategory("Compares")]
        public void CompareToMethod_NullArgument_ReturnsPositiveValue()
        {
            TimePeriod timePeriod = new TimePeriod(12, 23, 30);
            int result = timePeriod.CompareTo(null);
            Assert.IsTrue(result > 0);
        }

        [TestMethod, TestCategory("Compares")]
        public void CompareToMethod_NonTimePeriodObjectArgument_ThrowsArgumentException()
        {
            TimePeriod timePeriod = new TimePeriod(12, 23, 30);
            Assert.ThrowsException<ArgumentException>(() => timePeriod.CompareTo("not a timePeriod object"));
        }

        [TestMethod, TestCategory("Compares")]
        public void CompareToMethod_SameTimePeriodArgument_ReturnsZero()
        {
            TimePeriod timePeriod1 = new TimePeriod(12, 23, 30);
            TimePeriod timePeriod2 = new TimePeriod(12, 23, 30);
            int result = timePeriod1.CompareTo(timePeriod2);
            Assert.AreEqual(0, result);
        }

        [TestMethod, TestCategory("Compares")]
        public void CompareToMethod_EarlierTimePeriodArgument_ReturnsNegativeValue()
        {
            TimePeriod timePeriod1 = new TimePeriod(12, 23, 30);
            TimePeriod timePeriod2 = new TimePeriod(12, 30, 30);
            int result = timePeriod1.CompareTo(timePeriod2);
            Assert.IsTrue(result < 0);
        }

        [TestMethod, TestCategory("Compares")]
        public void CompareToMethod_LaterTimePeriodArgument_ReturnsPositiveValue()
        {
            TimePeriod timePeriod1 = new TimePeriod(12, 30, 30);
            TimePeriod timePeriod2 = new TimePeriod(12, 23, 30);
            int result = timePeriod1.CompareTo(timePeriod2);
            Assert.IsTrue(result > 0);
        }

        [DataTestMethod, TestCategory("Compares")]
        [DataRow("1:00:00", ">", "2:00:00", false)]
        [DataRow("1:00:00", ">", "1:01:00", false)]
        [DataRow("1:00:00", ">", "1:00:01", false)]
        [DataRow("1:00:00", ">", "1:00:00", false)]
        [DataRow("1:00:00", ">=", "2:00:00", false)]
        [DataRow("1:00:00", ">=", "1:01:00", false)]
        [DataRow("1:00:00", ">=", "1:00:01", false)]
        [DataRow("1:00:00", ">=", "1:00:00", true)]
        [DataRow("1:00:00", "<", "2:00:00", true)]
        [DataRow("1:00:00", "<", "1:01:00", true)]
        [DataRow("1:00:00", "<", "1:00:01", true)]
        [DataRow("1:00:00", "<", "1:00:00", false)]
        [DataRow("1:00:00", "<=", "2:00:00", true)]
        [DataRow("1:00:00", "<=", "1:01:00", true)]
        [DataRow("1:00:00", "<=", "1:00:01", true)]
        [DataRow("1:00:00", "<=", "1:00:00", true)]
        [DataRow("127:00:01", ">", "127:00:00", true)]
        public void CompareOperator_ReturnsExpectedResult(string timePeriod1Text, string operatorText, string timePeriod2Text, bool expectedResult)
        {
            TimePeriod timePeriod1 = new TimePeriod(timePeriod1Text);
            TimePeriod timePeriod2 = new TimePeriod(timePeriod2Text);

            switch (operatorText)
            {
                case ">":
                    Assert.AreEqual(expectedResult, timePeriod1 > timePeriod2);
                    break;
                case ">=":
                    Assert.AreEqual(expectedResult, timePeriod1 >= timePeriod2);
                    break;
                case "<":
                    Assert.AreEqual(expectedResult, timePeriod1 < timePeriod2);
                    break;
                case "<=":
                    Assert.AreEqual(expectedResult, timePeriod1 <= timePeriod2);
                    break;
            }
        }

        #endregion

        #region ===== Arithmetic operation =====

        [DataTestMethod, TestCategory("Arithmetics")]
        [DataRow("12:30:30", "1:25:25", "13:55:55", true)]
        [DataRow("0:00:00", "0:00:00", "0:00:00", true)]
        [DataRow("1:00:00", "0:00:00", "1:00:00", true)]
        [DataRow("0:30:00", "0:30:00", "1:00:00", true)]
        [DataRow("0:30:00", "0:29:59", "1:00:00", false)]
        [DataRow("1:30:30", "2:30:30", "4:01:00", true)]
        [DataRow("12:30:30", "1:25:25", "13:55:55", true)]
        public void PlusStaticMethod_ReturnsExpectedResult(string timePeriod1Text, string timePeriod2Text, string newTimePeriodResult, bool expectedResult)
        {
            TimePeriod timePeriod1 = new TimePeriod(timePeriod1Text);
            TimePeriod timePeriod2 = new TimePeriod(timePeriod2Text);

            Assert.AreEqual(expectedResult, TimePeriod.Plus(timePeriod1, timePeriod2).ToString() == newTimePeriodResult);
        }

        [DataTestMethod, TestCategory("Arithmetics")]
        [DataRow("0:00:00", "0:00:00", "0:00:00", true)]
        [DataRow("1:00:00", "0:00:00", "1:00:00", true)]
        [DataRow("0:30:00", "0:30:00", "0:00:00", true)]
        [DataRow("1:30:30", "0:30:30", "1:00:00", true)]
        [DataRow("12:30:30", "1:25:25", "11:05:05", true)]
        public void MinusStaticMethod_ReturnsExpectedResult(string timePeriod1Text, string timePeriod2Text, string newTimePeriodResult, bool expectedResult)
        {
            TimePeriod timePeriod1 = new TimePeriod(timePeriod1Text);
            TimePeriod timePeriod2 = new TimePeriod(timePeriod2Text);

            Assert.AreEqual(expectedResult, TimePeriod.Minus(timePeriod1, timePeriod2).ToString() == newTimePeriodResult);
        }

        [DataTestMethod, TestCategory("Arithmetics")]
        [DataRow("2:30:25", "23:30:30")]
        public void MinusStaticMethod_SecondGreaterThanFirstArguments_ThrowsArgumentException(string timePeriod1Text, string timePeriod2Text)
        {
            TimePeriod timePeriod1 = new TimePeriod(timePeriod1Text);
            TimePeriod timePeriod2 = new TimePeriod(timePeriod2Text);

            Assert.ThrowsException<ArgumentException>(() => TimePeriod.Minus(timePeriod1, timePeriod2));
        }

        [DataTestMethod, TestCategory("Arithmetics")]
        [DataRow("12:30:30", "+", "2:00:00", "14:30:30", true)]
        [DataRow("0:00:00", "+", "0:00:00", "0:00:00", true)]
        [DataRow("1:00:00", "+", "0:00:00", "1:00:00", true)]
        [DataRow("0:30:00", "+", "0:30:00", "1:00:00", true)]
        [DataRow("0:30:00", "+", "0:29:59", "1:00:00", false)]
        [DataRow("1:30:30", "+", "2:30:30", "4:01:00", true)]
        [DataRow("12:30:30", "+", "1:25:25", "13:55:55", true)]
        [DataRow("0:00:00", "-", "0:00:00", "0:00:00", true)]
        [DataRow("1:00:00", "-", "0:00:00", "1:00:00", true)]
        [DataRow("0:30:00", "-", "0:30:00", "0:00:00", true)]
        [DataRow("1:30:30", "-", "0:30:30", "1:00:00", true)]
        [DataRow("12:30:30", "-", "1:25:25", "11:05:05", true)]
        public void ArithmeticOperator_ReturnsExpectedResult(string timePeriod1Text, string operatorText, string timePeriod2Text, string newTimePeriodResult, bool expectedResult)
        {
            TimePeriod timePeriod1 = new TimePeriod(timePeriod1Text);
            TimePeriod timePeriod2 = new TimePeriod(timePeriod2Text);

            switch (operatorText)
            {
                case "+":
                    Assert.AreEqual(expectedResult, (timePeriod1 + timePeriod2).ToString() == newTimePeriodResult);
                    break;
                case "-":
                    Assert.AreEqual(expectedResult, (timePeriod1 - timePeriod2).ToString() == newTimePeriodResult);
                    break;
            }
        }

        #endregion

        #region ===== ToString =====

        [DataTestMethod, TestCategory("ToString")]
        [DataRow(12, 30, 30, "12:30:30")]
        [DataRow(0, 0, 0, "0:00:00")]
        [DataRow(78, 59, 59, "78:59:59")]
        [DataRow(0, 30, 29, "0:30:29")]
        public void ToString_ReturnsExpectedResult(long hours, long minutes, long seconds, string expectedString)
        {
            TimePeriod timePeriod = new TimePeriod(hours, minutes, seconds);
            string actualString = timePeriod.ToString();
            Assert.AreEqual(expectedString, actualString);
        }

        #endregion

    }
}