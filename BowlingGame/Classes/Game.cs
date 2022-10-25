namespace ToddBoothroyd_BowlingGame
{
    internal abstract class Game
    {
        internal abstract GameDefinition GameDefinition { get; set; }
        internal abstract double Score { get; set; }
        internal abstract string DisplayGameResults();
    }
}
