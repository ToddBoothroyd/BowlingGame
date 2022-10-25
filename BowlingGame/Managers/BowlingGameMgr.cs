using System;
using System.Collections.Generic;
using System.Text;

namespace ToddBoothroyd_BowlingGame
{
    public class BowlingGameMgr : GameMgr
    {
        private BowlingGame _bowlingGame = null;
        private BowlingGameTypes _bowlingGameType;
        private BowlingFrameMgr _bowlingFrameMgr = new BowlingFrameMgr();

        public BowlingGameMgr(BowlingGameTypes bowlingGameType)
        {
            this._bowlingGameType = bowlingGameType;
        }

        ///// <summary>
        ///// In future release, construct bowling game settings via dependencyInjection
        ///// Allowing consumer to set up game definition
        ///// </summary>
        ///// <param name="dependencyInjection"></param>
        //public BowlingGameMgr(object dependencyInjection)
        //{
            
        //}

        public override void LoadGame()
        {
            GameResults = new Dictionary<int, Tuple<int, string>>();
            this._bowlingGame = _bowlingGame = new BowlingGame();
            var definition = BowlingGameDataSvc.RetrieveBowlingGameDefinition(this._bowlingGameType);
            this._bowlingGame.GameDefinition = definition;
            this._bowlingFrameMgr.LoadBowlingRules(definition);
            OnGameStatusEvent(new GameStatusEventArgs(GameStatus.Ere, DateTime.Now));
        }

        public override void PlayGame()
        {
            OnGameStatusEvent(new GameStatusEventArgs(GameStatus.InProgress, DateTime.Now));
            OnGameMessageEvent(new GameMessageEventArgs("Rolling the ball...", DateTime.Now));
            OnGameMessageEvent(new GameMessageEventArgs(this._bowlingFrameMgr.RollBall(), DateTime.Now));
            if(this._bowlingFrameMgr.HasGameEnded) {
                OnGameStatusEvent(new GameStatusEventArgs(GameStatus.Complete, DateTime.Now));
                this.GameResults = this._bowlingFrameMgr.FrameResults;
            }
        }

        /// <summary>
        /// Returns the name of the game
        /// Null check is classic, early C#
        /// </summary>
        /// <returns></returns>
        public override string GetGameName()
        {
            if (this._bowlingGame == null)
                return string.Empty;

            return this._bowlingGame.GameDefinition.Name;
        }

        /// <summary>
        /// Returns the description of the game
        /// Null is checked with shorthand
        /// </summary>
        /// <returns></returns>
        public override string GetGameDescription()
        {
            return this._bowlingGame == null? string.Empty : this._bowlingGame.GameDefinition.Description;
        }

        /// <summary>
        /// Returns the rules of the game
        /// Null value is returned if object not instantiated
        /// </summary>
        /// <returns></returns>
        public override string GetGameRules()
        {
            return this._bowlingGame?.GameDefinition.Rules;
        }

        public Dictionary<int, Tuple<int, string>> GameResults { get; private set; }

        #region Eventing and Delegation

        public override void OnGameStatusEvent(GameStatusEventArgs e)
        {
            EventHandler<GameStatusEventArgs> handler = GameStatusNotification;
            handler?.Invoke(this, e);
        }

        public override void OnGameMessageEvent(GameMessageEventArgs e)
        {
            EventHandler<GameMessageEventArgs> handler = GameMessageNotification;
            handler?.Invoke(this, e);
        }

        public override event EventHandler<GameStatusEventArgs> GameStatusNotification;
        public override event EventHandler<GameMessageEventArgs> GameMessageNotification;
        #endregion
    }
}
