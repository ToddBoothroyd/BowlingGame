using System.ComponentModel;

namespace ToddBoothroyd_BowlingGame
{
    public enum BowlingGameTypes
    {
        [Description("None")]
        None = 0,
        [Description("Ten-Pin")]
        TenPin = 1,
        [Description("Candlepin")]
        CandlePin = 2,
        [Description("Duckpin")]
        DuckPin = 3
    }

    public enum GameStatus
    {
        [Description("None")]
        None = 0,
        [Description("Not Started")]
        Ere = 1,
        [Description("In Progress")]
        InProgress = 2,
        [Description("Complete")]
        Complete = 3
    }

    /// <summary>
    /// Use for localization and internationalization
    /// Note: future work
    /// </summary>
    public enum GameLanguage
    {
        [Description("None")]
        None,
        [Description("English")]
        English,
        [Description("German")]
        German
    }
}
