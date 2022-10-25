using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToddBoothroyd_BowlingGame;

namespace ToddBoothroyd_BowlingGame
{
    [TestClass]
    public class RollingHelper_MSTests
    {
        [TestMethod]
        [DataRow(0, 10)]
        [DataRow(0, 1)]
        [DataRow(1, 5)]
        [DataRow(2000, 5000)]
        public void Random_InputIsMinUnderMax_IsInRange(int minValue, int maxValue)
        {
            int output = RollHelper.GetRandomRoll(minValue, maxValue);
            Assert.IsTrue(output >= minValue && output <= maxValue, $"Random number is not in valid range when minimum {minValue} is under maximum {maxValue}");
        }

        [TestMethod]
        [DataRow(10, 0)]
        [DataRow(9, 1)]
        [DataRow(5, 4)]
        [DataRow(5000, 4001)]
        public void Random_InputIsMaxUnderMin_IsNotInRange(int minValue, int maxValue)
        {
            int output = RollHelper.GetRandomRoll(minValue, maxValue);
            Assert.IsTrue(output.Equals(-1), $"Random number is not in valid range when maximum {maxValue} is under minimum {minValue}");
        }

        [TestMethod]
        [DataRow(10, 10)]
        [DataRow(9, 9)]
        [DataRow(1, 1)]
        [DataRow(99999, 99999)]
        public void Random_InputMinEqualsMax_ReturnsMax(int minValue, int maxValue)
        {
            int output = RollHelper.GetRandomRoll(minValue, maxValue);
            Assert.IsTrue(output.Equals(maxValue), $"Random number is not maximum {maxValue} when same value as minimum {minValue}");
        }

        [TestMethod]
        [DataRow(0, 0)]
        [DataRow(-1, 0)]
        [DataRow(1, 0)]
        [DataRow(80000, 0)]
        public void Random_InputMaxEqualsZero_ReturnsNegative(int minValue, int maxValue)
        {
            int output = RollHelper.GetRandomRoll(minValue, maxValue);
            Assert.IsTrue(output.Equals(-1), $"Random number is not -1 when maximum {maxValue} is zero; minimum {minValue}");
        }

        [TestMethod]
        [DataRow(-10, -1)]
        [DataRow(-12, -14)]
        [DataRow(-100, -99)]
        [DataRow(-6700, -6799)]
        public void Random_InputAreNegatives_ReturnsNegative(int minValue, int maxValue)
        {
            int output = RollHelper.GetRandomRoll(minValue, maxValue);
            Assert.IsTrue(output.Equals(-1), $"Random number is not -1 when maximum {maxValue} is zero; minimum {minValue}");
        }

        [TestMethod]
        [DataRow(0, 10)]
        [DataRow(0, 10)]
        [DataRow(0, 10)]
        [DataRow(0, 10)]
        [DataRow(0, 10)]
        public void Random_MultipleCallsSameValue_DoNotReturnSameNumber(int minValue, int maxValue)
        {
            int output1 = RollHelper.GetRandomRoll(minValue, maxValue);
            int output2 = RollHelper.GetRandomRoll(minValue, maxValue);
            int output3 = RollHelper.GetRandomRoll(minValue, maxValue);
            int output4 = RollHelper.GetRandomRoll(minValue, maxValue);
            int output5 = RollHelper.GetRandomRoll(minValue, maxValue);
            bool verifier = output1.Equals(output2) && output1.Equals(output3) && output1.Equals(output4) && output1.Equals(output5);
            Assert.IsFalse(verifier, $"Random number output is same when same values are supplied: maximum {maxValue} - minimum {minValue}");
        }

        [TestMethod]
        [DataRow(0, 10)]
        [DataRow(0, 1)]
        [DataRow(1, 5)]
        [DataRow(2000, 5000)]
        public void Random_MultipleCallsDifferentValues_DoNotReturnSameNumber(int minValue, int maxValue)
        {
            int output1 = RollHelper.GetRandomRoll(minValue, maxValue);
            int output2 = RollHelper.GetRandomRoll(minValue, maxValue);
            int output3 = RollHelper.GetRandomRoll(minValue, maxValue);
            int output4 = RollHelper.GetRandomRoll(minValue, maxValue);
            int output5 = RollHelper.GetRandomRoll(minValue, maxValue);
            bool verifier = output1.Equals(output2) && output1.Equals(output3) && output1.Equals(output4) && output1.Equals(output5);
            Assert.IsFalse(verifier, $"Random number output is same when same values are supplied: maximum {maxValue} - minimum {minValue}");
        }


        [TestMethod]
        [DataRow(20, 30)]
        public void Random_InputNormal_ReturnsIntegerDataType(int minValue, int maxValue)
        {
            Assert.IsInstanceOfType(RollHelper.GetRandomRoll(minValue, maxValue), typeof(int), "Return type is not an integer");
        }
    }
}
