using System;

namespace ToddBoothroyd_BowlingGame
{
    internal class BowlingGame: Game
    {
        internal BowlingGame()
        {

        }

        internal override GameDefinition GameDefinition { get; set; }

        internal override double Score { get; set; }

        internal override string DisplayGameResults()
        {
            throw new NotImplementedException();
        }

    }
}
