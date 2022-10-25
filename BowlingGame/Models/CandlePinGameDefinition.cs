using System;
using System.Collections.Generic;
using System.Text;

namespace ToddBoothroyd_BowlingGame
{
    /// <summary>
    /// Populate the base rules for a candlepin bowling game.
    /// </summary>
    internal sealed class CandlePinGameDefinition: GameData
    {
        public override BowlingGameDefinition RetrieveBowlingDefinition()
        {
            StringBuilder sbRules = new StringBuilder();
            sbRules.AppendLine("There are ten pins that can be bowled down by rolling a ball.  Each knocked pin counts as a point. The player has ten frames to score points with three attempts each frame to knock down all pins.");
            sbRules.AppendLine("If a player knocks down all pins in two attempts in Frame A, that is a 'Spare.'  The first roll of the next frame (Frame B) adds to tally of Frame A as well as counts for Frame B.");
            sbRules.AppendLine("If a player knocks down all pins in one attempt in Frame A, that is a 'Strike.'  The first two rolls of the next frame (Frame B) adds to tally of Frame A as well as counts for Frame B.  If a player scores a strike in Frame B, both Frame A and Frame B have a chance to increase score in Frame C.");
            sbRules.AppendLine("If a Spare or a Strike occurs in the tenth frame, the pins are setup again more time to finish allowed rolls for last frame using the normal");

            return new BowlingGameDefinition {
                Name = "Candlepin Bowling",
                Description = "Candlepin bowling is a variation of bowling that is played primarily in the Canadian Maritime provinces and the New England region of the United States. It is played with a handheld-sized ball and tall, narrow pins that resemble candles, hence the name.",
                Frames = 10,
                NumberOfRolls = 3,
                LastFrameNumberOfRolls = 3,
                Rules = sbRules.ToString(),
                PinCount = 10
            };
        }
    }
}
