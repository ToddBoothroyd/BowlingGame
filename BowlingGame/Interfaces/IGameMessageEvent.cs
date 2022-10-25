using System;

namespace ToddBoothroyd_BowlingGame
{
    public interface IGameMessageEvent
    {
        void OnGameMessageEvent(GameMessageEventArgs e);

        event EventHandler<GameMessageEventArgs> GameMessageNotification;
    }
}
