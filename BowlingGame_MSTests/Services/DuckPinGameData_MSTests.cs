using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ToddBoothroyd_BowlingGame;

namespace BowlingGame_MSTests.Services
{
    [TestClass]
    public class DuckPinGameData_MSTests
    {
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void BowlingGameDataSvc_DuckPinGameData_ReturnsException()
        {
            BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.DuckPin);
        }
    }
}
