using System;
using System.Collections.Generic;
using System.Text;

namespace ToddBoothroyd_BowlingGame
{
    public interface IGameStatusEvent
    {
        void OnGameStatusEvent(GameStatusEventArgs e);

        event EventHandler<GameStatusEventArgs> GameStatusNotification;
    }
}
