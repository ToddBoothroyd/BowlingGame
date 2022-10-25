namespace ToddBoothroyd_BowlingGame
{
    public abstract class GameDefinition
    {
        public abstract string Description { get; set; }
        public abstract byte Frames { get; set; }
        public abstract byte LastFrameNumberOfRolls { get; set; }
        public abstract string Name { get; set; }
        public abstract byte NumberOfRolls { get; set; }
        public abstract byte PinCount { get; set; }
        public abstract string Rules { get; set; }
    }
}
