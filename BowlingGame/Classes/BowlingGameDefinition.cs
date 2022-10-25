namespace ToddBoothroyd_BowlingGame
{
    public class BowlingGameDefinition: GameDefinition
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override byte Frames { get; set; }
        public override byte NumberOfRolls { get; set; }
        public override byte LastFrameNumberOfRolls { get; set; }
        public override byte PinCount { get; set; }
        public override string Rules { get; set; }
    }
}
