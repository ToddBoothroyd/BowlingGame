using System;

namespace ToddBoothroyd_BowlingGame
{
    public class GameMessageEventArgs
    {
        public GameMessageEventArgs()
        {
            this.GameMessage = string.Empty;
            this.DateTimeStamp = DateTime.Now;
        }

        public GameMessageEventArgs(string gameMessage, DateTime dateTimeStamp)
        {
            this.GameMessage = gameMessage;
            this.DateTimeStamp = dateTimeStamp;
        }

        public string GameMessage { get; set; }
        
        public DateTime DateTimeStamp { get; set; }
    }
}
