using System;

namespace ToddBoothroyd_BowlingGame
{
    public static class RollHelper
    {
        private static readonly Random _globalRandom = new Random();
        
        /// <summary>
        /// Returns a random number within the range of values supplied.
        /// There is a minimum number and a maximum number.
        /// Minimum number cannot be less than zero.
        /// Maximum number cannot be zero, less than zero, or less than minimum number.
        /// Invalid input results in a return value of -1.
        /// </summary>
        /// <param name="floorValue">The minimum integer value that cannot be less than zero</param>
        /// <param name="ceilingValue">the maximum integer value than cannot be less than the minimum value</param>
        /// <returns>int</returns>
        public static int GetRandomRoll(int floorValue, int ceilingValue)
        {
            if (floorValue < 0 || ceilingValue <= 0)
                return -1;
            else if (ceilingValue.Equals(floorValue))
                return ceilingValue;
            else if (ceilingValue < floorValue)
                return -1;
            
            return _globalRandom.Next(floorValue, ++ceilingValue);
        }
    }
}
