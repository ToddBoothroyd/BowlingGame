using System;
using System.Collections.Generic;
using System.Text;

namespace ToddBoothroyd_BowlingGame
{
    internal interface IBowlingGameData
    {
        BowlingGameDefinition RetrieveBowlingDefinition();
    }
}
