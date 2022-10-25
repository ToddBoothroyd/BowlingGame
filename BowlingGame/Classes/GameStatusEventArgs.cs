using System;
using System.Collections.Generic;
using System.Text;

namespace ToddBoothroyd_BowlingGame
{
    public class GameStatusEventArgs : EventArgs
    {
        public GameStatusEventArgs() {
            this.GameStatus = GameStatus.None;
            this.DateTimeStamp = DateTime.Now;
        }

        public GameStatusEventArgs(GameStatus gameStatus, DateTime dateTimeStamp)
        {
            this.GameStatus = gameStatus;
            this.DateTimeStamp = dateTimeStamp;
        }

        public GameStatus GameStatus { get; set; }
        public string Message
        {
            get { return GameStatus.GetDescription(); }
        }
        public DateTime DateTimeStamp { get; set; }
    }
}
