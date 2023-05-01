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
        public void EqualMethod_TheSameReferenceArgument_ReturnsTrue()
        {
            Time t1 = new();
            Time t2 = t1;
            Assert.IsTrue(t1.Equals(t2));
        }

        [TestMethod, TestCategory("Equals")]
        [DataTestMethod]
        [DataRow((byte)12, (byte)30, (byte)45, 
                 (byte)12, (byte)30, (byte)45, true)]
        [DataRow((byte)11, (byte)29, (byte)59,
                 (byte)11, (byte)29, (byte)59, true)]
        [DataRow((byte)1, (byte)59, (byte)59,
                 (byte)1, (byte)59, (byte)59, true)]
        [DataRow((byte)23, (byte)59, (byte)59,
                 (byte)23, (byte)59, (byte)59, true)]
        [DataRow((byte)10, (byte)39, (byte)30,
                 (byte)10, (byte)39, (byte)31, false)]
        [DataRow((byte)12, (byte)0, (byte)0,
                 (byte)11, (byte)59, (byte)59, false)]
        public void EqualMethod_ReturnsExpectedResult(byte t1Hourse, byte t1Minutes, byte t1Seconds, byte t2Hourse, byte t2Minutes, byte t2Seconds, bool result)
        {
            Time t1 = new(t1Hourse, t1Minutes, t1Seconds);
            Time t2 = new(t2Hourse, t2Minutes, t2Seconds);
            Assert.AreEqual(result, t1.Equals(t2));
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualOperator_ComapreWithNull_ReturnsFalse()
        {
            Assert.IsFalse(new Time() == null);
        }

        [TestMethod, TestCategory("Equals")]
        public void NotEqualOperator_ComapreWithNull_ReturnsTrue()
        {
            Assert.IsTrue(new Time() != null);
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualOperator_ComapreWithTheSameReference_ReturnsTrue()
        {
            Time t1 = new();
            Time t2 = t1;
            Assert.IsTrue(t1 == t2);
        }

        [TestMethod, TestCategory("Equals")]
        public void NotEqualOperator_ComapreWithTheSameReference_ReturnsFalse()
        {
            Time t1 = new();
            Time t2 = t1;
            Assert.IsFalse(t1 != t2);
        }

        [TestMethod, TestCategory("Equals")]
        [DataTestMethod]
        [DataRow((byte)12, (byte)30, (byte)45,
                 (byte)12, (byte)30, (byte)45, true)]
        [DataRow((byte)11, (byte)29, (byte)59,
                 (byte)11, (byte)29, (byte)59, true)]
        [DataRow((byte)1, (byte)59, (byte)59,
                 (byte)1, (byte)59, (byte)59, true)]
        [DataRow((byte)23, (byte)59, (byte)59,
                 (byte)23, (byte)59, (byte)59, true)]
        [DataRow((byte)10, (byte)39, (byte)30,
                 (byte)10, (byte)39, (byte)31, false)]
        [DataRow((byte)12, (byte)0, (byte)0,
                 (byte)11, (byte)59, (byte)59, false)]
        public void EqualOperator_ReturnsExpectedResult(byte t1Hourse, byte t1Minutes, byte t1Seconds, byte t2Hourse, byte t2Minutes, byte t2Seconds, bool result)
        {
            Time t1 = new(t1Hourse, t1Minutes, t1Seconds);
            Time t2 = new(t2Hourse, t2Minutes, t2Seconds);
            Assert.AreEqual(result, t1 == t2);
        }

        [TestMethod, TestCategory("Equals")]
        [DataTestMethod]
        [DataRow((byte)12, (byte)30, (byte)45,
                 (byte)12, (byte)30, (byte)45, false)]
        [DataRow((byte)11, (byte)29, (byte)59,
                 (byte)11, (byte)29, (byte)59, false)]
        [DataRow((byte)1, (byte)59, (byte)59,
                 (byte)1, (byte)59, (byte)59, false)]
        [DataRow((byte)23, (byte)59, (byte)59,
                 (byte)23, (byte)59, (byte)59, false)]
        [DataRow((byte)10, (byte)39, (byte)30,
                 (byte)10, (byte)39, (byte)31, true)]
        [DataRow((byte)12, (byte)0, (byte)0,
                 (byte)11, (byte)59, (byte)59, true)]
        public void NotEqualOperator_ReturnsExpectedResult(byte t1Hourse, byte t1Minutes, byte t1Seconds, byte t2Hourse, byte t2Minutes, byte t2Seconds, bool result)
        {
            Time t1 = new(t1Hourse, t1Minutes, t1Seconds);
            Time t2 = new(t2Hourse, t2Minutes, t2Seconds);
            Assert.AreEqual(result, t1 != t2);
        }

        #endregion

        #region ===== Comparable =====

        [TestMethod, TestCategory("Relations")]
        public void CompareTo_NullObject_ReturnsPositiveValue()
        {
            Time time = new Time(1, 2, 3);
            int result = time.CompareTo(null);
            Assert.IsTrue(result > 0);
        }

        [TestMethod, TestCategory("Relations")]
        public void CompareTo_NonTimeObject_ThrowsArgumentException()
        {
            Time time = new Time(1, 2, 3);
            Assert.ThrowsException<ArgumentException>(() => time.CompareTo("not a time object"));
        }

        [TestMethod, TestCategory("Relations")]
        public void CompareTo_SameTime_ReturnsZero()
        {
            Time time1 = new Time(1, 2, 3);
            Time time2 = new Time(1, 2, 3);
            int result = time1.CompareTo(time2);
            Assert.AreEqual(0, result);
        }

        [TestMethod, TestCategory("Relations")]
        public void CompareTo_EarlierTime_ReturnsNegativeValue()
        {
            Time time1 = new Time(1, 2, 3);
            Time time2 = new Time(1, 3, 3);
            int result = time1.CompareTo(time2);
            Assert.IsTrue(result < 0);
        }

        [TestMethod, TestCategory("Relations")]
        public void CompareTo_LaterTime_ReturnsPositiveValue()
        {
            Time time1 = new Time(1, 2, 3);
            Time time2 = new Time(1, 1, 3);
            int result = time1.CompareTo(time2);
            Assert.IsTrue(result > 0);
        }

        [TestMethod, TestCategory("Relations")]
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
        public void RelationOperator_ReturnsExpectedResult(string time1Text, string operatorText, string time2Text, bool result)
        {
            Time t1 = new(time1Text);
            Time t2 = new(time2Text);
            
            switch(operatorText) 
            {
                case ">":
                    Assert.AreEqual(result, t1 > t2);
                    break;
                case ">=":
                    Assert.AreEqual(result, t1 >= t2);
                    break;
                case "<":
                    Assert.AreEqual(result, t1 < t2);
                    break;
                case "<=":
                    Assert.AreEqual(result, t1 <= t2);
                    break;
            }
        }

        #endregion
    }
}