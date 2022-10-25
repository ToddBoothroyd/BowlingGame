using System;

namespace ToddBoothroyd_BowlingGame
{
    /// <summary>
    /// Populate the base rules for a duckpin bowling game.
    /// </summary>
    internal sealed class DuckPinGameDefinition : GameData
    {
        public override BowlingGameDefinition RetrieveBowlingDefinition()
        {
            //Intentionally left incomplete for later game expansion.
            //However, this data could come from a database or other data store.
            throw new NotImplementedException();
        }
    }
}
