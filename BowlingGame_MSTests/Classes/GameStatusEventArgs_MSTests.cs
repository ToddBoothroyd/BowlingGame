using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ToddBoothroyd_BowlingGame;

namespace BowlingGame_MSTests.Classes
{
    [TestClass]
    public class GameStatusEventArgs_MSTests
    {
        [TestMethod]
        public void GameStatusEventArgs_DefaultConstructor_EnumStatusIsNone()
        {
            GameStatusEventArgs gameStatusEventArgs = new GameStatusEventArgs();
            Assert.IsTrue(gameStatusEventArgs.GameStatus.Equals(GameStatus.None), $"GameStatus Enum {gameStatusEventArgs.GameStatus.GetDescription()} is not the desired default");
        }

        [TestMethod]
        public void GameStatusEventArgs_DefaultConstructor_EnumStatusDescriptionIsNone()
        {
            GameStatusEventArgs gameStatusEventArgs = new GameStatusEventArgs();
            Assert.IsTrue(gameStatusEventArgs.GameStatus.GetDescription().Equals(GameStatus.None.GetDescription()), $"GameStatus Enum {gameStatusEventArgs.GameStatus.GetDescription()} is not the desired default");
        }

        [TestMethod]
        public void GameStatusEventArgs_DefaultConstructor_MessageIsNone()
        {
            GameStatusEventArgs gameStatusEventArgs = new GameStatusEventArgs();
            Assert.IsTrue(gameStatusEventArgs.Message.Equals("None"), $"GameStatus Enum {gameStatusEventArgs.GameStatus.GetDescription()} is not the desired default");
        }

        [TestMethod]
        [DataRow(GameStatus.Ere)]
        [DataRow(GameStatus.InProgress)]
        [DataRow(GameStatus.Complete)]
        public void GameStatusEventArgs_CustomConstructor_EnumStatusIsValid(GameStatus gameStatus)
        {
            GameStatusEventArgs gameStatusEventArgs = new GameStatusEventArgs(gameStatus, DateTime.Now);
            Assert.IsTrue(gameStatusEventArgs.GameStatus.Equals(gameStatus), $"GameStatus Enum {gameStatusEventArgs.GameStatus.GetDescription()} is not the desired setting");
        }

        [TestMethod]
        [DataRow(GameStatus.Ere)]
        [DataRow(GameStatus.InProgress)]
        [DataRow(GameStatus.Complete)]
        public void GameStatusEventArgs_CustomConstructor_EnumStatusDescriptionIsValid(GameStatus gameStatus)
        {
            GameStatusEventArgs gameStatusEventArgs = new GameStatusEventArgs(gameStatus, DateTime.Now);
            Assert.IsTrue(gameStatusEventArgs.GameStatus.GetDescription().Equals(gameStatus.GetDescription()), $"GameStatus Enum {gameStatusEventArgs.GameStatus.GetDescription()} is not the desired setting");
        }

        [TestMethod]
        [DataRow(GameStatus.Ere)]
        [DataRow(GameStatus.InProgress)]
        [DataRow(GameStatus.Complete)]
        public void GameStatusEventArgs_CustomConstructor_MessageIsValid(GameStatus gameStatus)
        {
            GameStatusEventArgs gameStatusEventArgs = new GameStatusEventArgs(gameStatus, DateTime.Now);
            Assert.IsTrue(gameStatusEventArgs.Message.Equals(gameStatus.GetDescription()), $"GameStatus Enum {gameStatusEventArgs.GameStatus.GetDescription()} is not the desired setting");
        }
    }
}
