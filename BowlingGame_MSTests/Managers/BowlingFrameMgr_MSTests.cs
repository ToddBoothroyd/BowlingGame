using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToddBoothroyd_BowlingGame;

namespace BowlingGame_MSTests.Managers
{
    [TestClass]
    public class BowlingFrameMgr_MSTests
    {

        [TestMethod]
        public void BowlingFrameMgr_VariousInputs_TotalIs110()
        {
            BowlingFrameMgr mgr = new BowlingFrameMgr();
            mgr.LoadBowlingRules(BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.TenPin));
            //Frame 1
            mgr.RollBall(4);
            mgr.RollBall(3);
            //Frame 2
            mgr.RollBall(7);
            mgr.RollBall(3);
            //Frame 3
            mgr.RollBall(5);
            mgr.RollBall(2);
            //Frame 4
            mgr.RollBall(8);
            mgr.RollBall(1);
            //Frame 5
            mgr.RollBall(4);
            mgr.RollBall(6);
            //Frame 6
            mgr.RollBall(2);
            mgr.RollBall(4);
            //Frame 7
            mgr.RollBall(8);
            mgr.RollBall(0);
            //Frame 8
            mgr.RollBall(8);
            mgr.RollBall(0);
            //Frame 9
            mgr.RollBall(8);
            mgr.RollBall(2);
            //Frame 10
            mgr.RollBall(10);
            mgr.RollBall(1);
            mgr.RollBall(7);
            Assert.IsTrue(mgr.ScoreTotal == 110);
        }

        [TestMethod]
        public void BowlingFrameMgr_TenPinTwoThrowsAt2_TotalIs4()
        {
            BowlingFrameMgr mgr = new BowlingFrameMgr();
            mgr.LoadBowlingRules(BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.TenPin));
            mgr.RollBall(2);
            mgr.RollBall(2);
            Assert.IsTrue(mgr.ScoreTotal == 4);
        }

        [TestMethod]
        public void BowlingFrameMgr_TenPinFirstStrikeAndTwoThrowsAt2_TotalIs18()
        {
            BowlingFrameMgr mgr = new BowlingFrameMgr();
            mgr.LoadBowlingRules(BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.TenPin));
            mgr.RollBall(10);
            mgr.RollBall(2);
            mgr.RollBall(2);
            Assert.IsTrue(mgr.ScoreTotal == 18);
        }

        [TestMethod]
        public void BowlingFrameMgr_TenPinFirstStrikeAndTwoThrowsAt3_TotalIs22()
        {
            BowlingFrameMgr mgr = new BowlingFrameMgr();
            mgr.LoadBowlingRules(BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.TenPin));
            mgr.RollBall(10);
            mgr.RollBall(3);
            mgr.RollBall(3);
            Assert.IsTrue(mgr.ScoreTotal == 22);
        }

        [TestMethod]
        public void BowlingFrameMgr_TenPinFirstStrikeAndFourThrows_TotalIs31()
        {
            BowlingFrameMgr mgr = new BowlingFrameMgr();
            mgr.LoadBowlingRules(BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.TenPin));
            mgr.RollBall(10);
            mgr.RollBall(3);
            mgr.RollBall(3);
            mgr.RollBall(5);
            mgr.RollBall(4);
            Assert.IsTrue(mgr.ScoreTotal == 31);
        }

        [TestMethod]
        public void BowlingFrameMgr_BowlingDrunk_TotalIs0()
        {
            BowlingFrameMgr mgr = new BowlingFrameMgr();
            mgr.LoadBowlingRules(BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.TenPin));
            for(int i = 0; i<10; i++)
                mgr.RollBall(0);

            Assert.IsTrue(mgr.ScoreTotal == 0);
        }

        [TestMethod]
        public void BowlingFrameMgr_AllStrikes_TotalIs300()
        {
            BowlingFrameMgr mgr = new BowlingFrameMgr();
            mgr.LoadBowlingRules(BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.TenPin));
            for (int i = 0; i < 10; i++)
                mgr.RollBall(10);

            mgr.RollBall(10);
            mgr.RollBall(10);
       

            Assert.IsTrue(mgr.ScoreTotal == 300);
        }

        [TestMethod]
        public void BowlingFrameMgr_OneStrike_TotalIs18()
        {
            BowlingFrameMgr mgr = new BowlingFrameMgr();
            mgr.LoadBowlingRules(BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.TenPin));
            mgr.RollBall(10);
            mgr.RollBall(2);
            mgr.RollBall(2);


            Assert.IsTrue(mgr.ScoreTotal == 18);
        }


        [TestMethod]
        [DataRow(8)]
        public void BowlingFrameMgr_RollsGoNextFrameNormal_TotalIsFirstTwo(int expectedTotal)
        {
            BowlingFrameMgr mgr = new BowlingFrameMgr();
            mgr.LoadBowlingRules(BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.TenPin));
            mgr.RollBall(6);
            mgr.RollBall(2);
            mgr.RollBall(3);


            Assert.IsTrue(mgr.ScoreTotal == expectedTotal);
        }
        [TestMethod]
        public void BowlingFrameMgr_RollsLastNextFrameStruje_GameIsOver()
        {
            BowlingFrameMgr mgr = new BowlingFrameMgr();
            mgr.LoadBowlingRules(BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.TenPin));
            //Frame 1
            mgr.RollBall(4);
            mgr.RollBall(3);
            //Frame 2
            mgr.RollBall(7);
            mgr.RollBall(3);
            //Frame 3
            mgr.RollBall(5);
            mgr.RollBall(2);
            //Frame 4
            mgr.RollBall(8);
            mgr.RollBall(1);
            //Frame 5
            mgr.RollBall(4);
            mgr.RollBall(6);
            //Frame 6
            mgr.RollBall(2);
            mgr.RollBall(4);
            //Frame 7
            mgr.RollBall(8);
            mgr.RollBall(0);
            //Frame 8
            mgr.RollBall(8);
            mgr.RollBall(0);
            //Frame 9
            mgr.RollBall(8);
            mgr.RollBall(2);
            //Frame 10
            mgr.RollBall(10);
            mgr.RollBall(1);
            mgr.RollBall(7);
            Assert.IsTrue(mgr.HasGameEnded);
        }

        [TestMethod]
        public void BowlingFrameMgr_RollsLowNumbers_GameIsOver()
        {
            BowlingFrameMgr mgr = new BowlingFrameMgr();
            mgr.LoadBowlingRules(BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.TenPin));
            //Frame 1
            mgr.RollBall(4);
            mgr.RollBall(3);
            //Frame 2
            mgr.RollBall(7);
            mgr.RollBall(2);
            //Frame 3
            mgr.RollBall(5);
            mgr.RollBall(2);
            //Frame 4
            mgr.RollBall(8);
            mgr.RollBall(1);
            //Frame 5
            mgr.RollBall(4);
            mgr.RollBall(3);
            //Frame 6
            mgr.RollBall(2);
            mgr.RollBall(4);
            //Frame 7
            mgr.RollBall(8);
            mgr.RollBall(0);
            //Frame 8
            mgr.RollBall(8);
            mgr.RollBall(0);
            //Frame 9
            mgr.RollBall(8);
            mgr.RollBall(1);
            //Frame 10
            mgr.RollBall(1);
            mgr.RollBall(1);

            Assert.IsTrue(mgr.HasGameEnded);
        }

        [TestMethod]
        [DataRow(13)]
        public void BowlingFrameMgr_RollsGoNextFrameSpecial_TotalIsFirstThree(int expectedTotal)
        {
            BowlingFrameMgr mgr = new BowlingFrameMgr();
            mgr.LoadBowlingRules(BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.TenPin));
            mgr.RollBall(6);
            mgr.RollBall(4);
            mgr.RollBall(3);


            Assert.IsTrue(mgr.ScoreTotal == expectedTotal);
        }

        [TestMethod]
        [DataRow(20)]
        public void BowlingFrameMgr_RollsGoNextFrameSpecialTen_TotalIsFirstThree(int expectedTotal)
        {
            BowlingFrameMgr mgr = new BowlingFrameMgr();
            mgr.LoadBowlingRules(BowlingGameDataSvc.RetrieveBowlingGameDefinition(BowlingGameTypes.TenPin));
            mgr.RollBall(6);
            mgr.RollBall(4);
            mgr.RollBall(10);

            Assert.IsTrue(mgr.ScoreTotal == expectedTotal);
        }
    }
}
