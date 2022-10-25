namespace ToddBoothroyd_BowlingGame
{
    public static class BowlingGameDataSvc
    {
        public static BowlingGameDefinition RetrieveBowlingGameDefinition(BowlingGameTypes bowlingGameType)
        {
            switch (bowlingGameType)
            {
                case BowlingGameTypes.CandlePin:
                    var candlepinGame = new CandlePinGameDefinition();
                    return candlepinGame.RetrieveBowlingDefinition();
                case BowlingGameTypes.TenPin:
                    var tenpinGame = new TenPinGameDefinition();
                    return tenpinGame.RetrieveBowlingDefinition();
                case BowlingGameTypes.DuckPin:
                    var duckpinGame = new DuckPinGameDefinition();
                    return duckpinGame.RetrieveBowlingDefinition();
                case BowlingGameTypes.None:
                default:
                    return null;
            }
        }
    }
}
