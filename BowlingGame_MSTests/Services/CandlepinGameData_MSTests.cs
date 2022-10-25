using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToddBoothroyd_BowlingGame;

namespace BowlingGame_MSTests.Services
{
    [TestClass]
    public class CandlepinGameData_MSTests
    {
        [TestMethod]
        public void BowlingGameDataSvc_CandlepinGameData_NameMatchesValue()
        {
            var candlePinGame = BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.CandlePin);
            Assert.AreEqual(candlePinGame.Name, "Candlepin Bowling");
        }

        [TestMethod]
        public void BowlingGameDataSvc_CandlepinGameData_FramesMatchesValue()
        {
            var candlePinGame = BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.CandlePin);
            Assert.AreEqual(candlePinGame.Frames, 10);
        }

        [TestMethod]
        public void BowlingGameDataSvc_CandlepinGameData_NumberOfRollsMatchesValue()
        {
            var candlePinGame = BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.CandlePin);
            Assert.AreEqual(candlePinGame.NumberOfRolls, 3);
        }

        [TestMethod]
        public void BowlingGameDataSvc_CandlepinGameData_PinCountMatchesValue()
        {
            var candlePinGame = BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.CandlePin);
            Assert.AreEqual(candlePinGame.PinCount, 10);
        }

        [TestMethod]
        public void BowlingGameDataSvc_CandlepinGameData_LastFrameNumberOfRollsMatchesValue()
        {
            var candlePinGame = BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.CandlePin);
            Assert.AreEqual(candlePinGame.LastFrameNumberOfRolls, 3);
        }

        [TestMethod]
        public void BowlingGameDataSvc_CandlepinGameData_DescriptionNotNull()
        {
            var candlePinGame = BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.CandlePin);
            Assert.IsNotNull(candlePinGame.Description);
        }

        [TestMethod]
        public void BowlingGameDataSvc_CandlepinGameData_RulesNotNull()
        {
            var candlePinGame = BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.CandlePin);
            Assert.IsNotNull(candlePinGame.Rules);
        }
    }
}
