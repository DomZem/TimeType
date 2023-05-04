using TimeLib;

namespace TimeUnitTests
{
    [TestClass]
    public class TimePeriodUnitTests
    {
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

    }
}