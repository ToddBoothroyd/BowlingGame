using System;

namespace ToddBoothroyd_BowlingGame
{
    public abstract class GameMgr : IGameLoad, IGamePlay, IGameStatusEvent, IGameMessageEvent
    {
        public abstract void LoadGame();
        public abstract void PlayGame();

        public abstract string GetGameName();
        public abstract string GetGameDescription();
        public abstract string GetGameRules();

        public abstract void OnGameStatusEvent(GameStatusEventArgs e);

        public abstract void OnGameMessageEvent(GameMessageEventArgs e);

        public abstract event EventHandler<GameStatusEventArgs> GameStatusNotification;
        public abstract event EventHandler<GameMessageEventArgs> GameMessageNotification;
    }
}
