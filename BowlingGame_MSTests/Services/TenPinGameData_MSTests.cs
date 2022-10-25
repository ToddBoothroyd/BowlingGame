using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToddBoothroyd_BowlingGame;

namespace BowlingGame_MSTests.Services
{
    [TestClass]
    public class TenPinGameData_MSTests
    {
        [TestMethod]
        public void BowlingGameDataSvc_TenPinGameData_NameMatchesValue()
        {
            var tenpinGame = BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.TenPin);
            Assert.AreEqual(tenpinGame.Name, "Ten-Pin Bowling");
        }

        [TestMethod]
        public void BowlingGameDataSvc_TenpinGameData_FramesMatchesValue()
        {
            var tenpinGame = BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.TenPin);
            Assert.AreEqual(tenpinGame.Frames, 10);
        }

        [TestMethod]
        public void BowlingGameDataSvc_TenpinGameData_NumberOfRollsMatchesValue()
        {
            var tenpinGame = BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.TenPin);
            Assert.AreEqual(tenpinGame.NumberOfRolls, 2);
        }

        [TestMethod]
        public void BowlingGameDataSvc_TenpinGameData_PinCountMatchesValue()
        {
            var tenpinGame = BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.TenPin);
            Assert.AreEqual(tenpinGame.PinCount, 10);
        }

        [TestMethod]
        public void BowlingGameDataSvc_TenpinGameData_LastFrameNumberOfRollsMatchesValue()
        {
            var tenpinGame = BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.TenPin);
            Assert.AreEqual(tenpinGame.LastFrameNumberOfRolls, 3);
        }

        [TestMethod]
        public void BowlingGameDataSvc_TenpinGameData_DescriptionNotNull()
        {
            var tenpinGame = BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.TenPin);
            Assert.IsNotNull(tenpinGame.Description);
        }

        [TestMethod]
        public void BowlingGameDataSvc_TenpinGameData_RulesNotNull()
        {
            var tenpinGame = BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.TenPin);
            Assert.IsNotNull(tenpinGame.Rules);
        }
    }
}
