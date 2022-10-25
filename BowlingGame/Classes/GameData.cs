namespace ToddBoothroyd_BowlingGame
{
    internal abstract class GameData : IBowlingGameData
    {
        public abstract BowlingGameDefinition RetrieveBowlingDefinition();
    }
}
