namespace ToddBoothroyd_BowlingGame
{
    /// <summary>
    /// Sometimes there is a variance in data format, for instance when working with a customer, vendor or other entity
    /// This model represents one way to handle such a variance by making an offshoot model class that you do not want to confuse with your own
    /// 
    /// This particular model represents a requestors view of the ten pin bowling game
    /// </summary>
    internal class TenPinGameCustomerDefinition
    {
        public string GameName { get; set; }
        public string Details { get; set; }
        public byte Frames { get; set; }
        public byte Throws { get; set; }
        public byte LastFrame { get; set; }
        public byte Pins { get; set; }
        public string HowToPlay { get; set; }
    }
}
