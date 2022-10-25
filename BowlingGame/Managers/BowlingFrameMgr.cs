using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ToddBoothroyd_BowlingGame
{
    public class BowlingFrameMgr
    {
        private enum RollResult
        {
            [Description("None")]
            None,
            [Description("Pins Still Standing")]
            PinsStillStanding,
            [Description("Strike")]
            Strike,
            [Description("Spare")]
            Spare,
            [Description("All Toppled")]
            AllToppled
        }

        /// <summary>
        /// ContinuePresentFrame - stay on present frame to roll again and count
        /// GoToNextFrameNormalScoring - score present frame and continue to next frame with no conditions
        /// GoToNextFrameSpecialScoring - defer scoring present frame and continue to next frame with special scoring conditions
        /// LastFrameNeedSpecialScoringPhase - last frame has no special conditions, such as strike or spare
        /// ExtraFrameEndingPhase - special setting to allow extra rolls to close out game
        /// NoMoreFramesNormal - game is over no conditions
        /// NoMoreFramesSpecial - game ended with special conditions
        /// </summary>
        private enum FrameStatus
        {
            None,
            ContinuePresentFrame,
            GoToNextFrameNormalScoring,
            GoToNextFrameSpecialScoring,
            LastFrameNeedSpecialScoringPhase,
            ExtraFrameEndingPhase,
            NoMoreFramesNormal,
            NoMoreFramesSpecial
        }

        private BowlingGameDefinition _gameDefinition;
        private int _rollCount;
        private int _rollCountExtra;
        private int _frameCount = 1;
        private int _pinsStillStanding;
        
        private string _message = string.Empty;
        private int _ephemeralPinsStillStanding;
        //Frame #, Score, Ball +1, RollResult, int decrement
        private Dictionary<int, Tuple<int, int, RollResult, int>> _latentFrameResults = new Dictionary<int, Tuple<int, int, RollResult, int>>();
        private RollResult _rollResult;
        private FrameStatus _frameStatus;

        private int _minimumNumberOfPins => 0;

        public BowlingFrameMgr()
        {
          this.FrameResults = new Dictionary<int, Tuple<int, string>>();
          this._rollResult = RollResult.None;
        }
        public void LoadBowlingRules(BowlingGameDefinition gameDefinition) {
            if (gameDefinition is null)
                throw new NullReferenceException("BowlingGameDefinition is null in Bowling Frame Manager");

            this._gameDefinition = gameDefinition;
            this.SetPinsLeft(gameDefinition.PinCount);
        }
        public string RollBall(int userDefinedPinDrop = -1)
        {
            if (PreRoll_IsGameOver())
                return "Game is over. Create new game to continue playing";

            PreRoll_RollCountOperations();

            //The default value remain -1 which means no user defined value for the dropping of pins. Go random.
            int randomPinsDropped = RollHelper.GetRandomRoll(_minimumNumberOfPins, this._pinsStillStanding);
            Roll_PinsStillStanding(userDefinedPinDrop.Equals(-1) ? randomPinsDropped : userDefinedPinDrop);
            
            PostRoll_RollStatus();
            PostRoll_FrameStatus();
            PostRoll_Scoring(userDefinedPinDrop.Equals(-1) ? randomPinsDropped : userDefinedPinDrop);
            PostRoll_SetPinCount();
            PostRoll_Messaging(userDefinedPinDrop.Equals(-1) ? randomPinsDropped : userDefinedPinDrop);

            SetupNext_SetRollCount();
            SetupNext_SetFrameCount();
            SetupNext_SetRollStatus();
            SetupNext_SetFrameStatus();

            return this._message;
        }

        /// <summary>
        /// Check if game is over as a safety catch
        /// </summary>
        private bool PreRoll_IsGameOver()
        {
            return this.HasGameEnded;
        }

        private void PreRoll_RollCountOperations()
        {
            this._rollCount++;

            if (this._rollCountExtra > 0)
                this._rollCountExtra--;
        }

        private void Roll_PinsStillStanding(int pinsToppled)
        {
            this._ephemeralPinsStillStanding = this._pinsStillStanding - pinsToppled;
        }

        private void PostRoll_RollStatus()
        {
            if (this._ephemeralPinsStillStanding <= 0 && this._rollCount.Equals(1))
                this._rollResult = RollResult.Strike;
            else if (this._ephemeralPinsStillStanding <= 0 && this._rollCount.Equals(2))
                this._rollResult = RollResult.Spare;
            else if (this._ephemeralPinsStillStanding <= 0)
                this._rollResult = RollResult.AllToppled;
            else
                this._rollResult = RollResult.PinsStillStanding;
        }

        private void PostRoll_FrameStatus()
        {
            if (this._frameCount.Equals(this._gameDefinition.Frames) && this._frameStatus.Equals(FrameStatus.ExtraFrameEndingPhase) && this._rollCountExtra.Equals(0)) {
                //Frame is at the last frame and the rollcount matches the last frame number of rolls.
                //This is end of game
                this._frameStatus = FrameStatus.NoMoreFramesSpecial;
            }
            else if (this._rollResult.Equals(RollResult.Strike) || this._rollResult.Equals(RollResult.Spare))
            {
                //There is a strike in the frame so the next frame has to continue scoring for present frame.
                //If we are on last frame, then the next frame will be marked extra so it doesn't add a frame to results
                if (this._frameCount.Equals(this._gameDefinition.Frames))
                    this._frameStatus = FrameStatus.LastFrameNeedSpecialScoringPhase;
                else
                    this._frameStatus = FrameStatus.GoToNextFrameSpecialScoring;

                if (this._frameStatus.Equals(FrameStatus.LastFrameNeedSpecialScoringPhase) && this._rollCountExtra.Equals(0))
                    this._rollCountExtra = this._rollResult.Equals(RollResult.Strike) ? 2 : 1;
            }
            else if (this._frameCount < this._gameDefinition.Frames && this._rollCount.Equals(this._gameDefinition.NumberOfRolls))
            {
                //Frame is within the 'normal' range and the roll count matches the 'normal' number of rolls
                //We are done with this frame and need to go to the next but there isn't any special scoring to happen
                this._frameStatus = FrameStatus.GoToNextFrameNormalScoring;
            }
            else if (this._frameStatus.Equals(FrameStatus.ExtraFrameEndingPhase))
                return;
            else if (this._frameCount.Equals(this._gameDefinition.Frames) && this._rollCount.Equals(this._gameDefinition.NumberOfRolls))
                this._frameStatus = FrameStatus.NoMoreFramesNormal;
            else
                this._frameStatus = FrameStatus.ContinuePresentFrame;
        }

        private void PostRoll_Scoring(int pinsToppled) {

            #region Handle Latent Scoring
            Dictionary<int, Tuple<int, int, RollResult, int>> ephemeralLatentFrameResults = new Dictionary<int, Tuple<int, int, RollResult, int>>();
            foreach(KeyValuePair<int, Tuple<int, int, RollResult, int>> item in this._latentFrameResults)
            {
                //Value Score, Ball +1, Ball +2, RollResult, extra roll count
                Tuple<int, int, RollResult, int> resultItem = item.Value;
                this._latentFrameResults.Remove(item.Key); //Drop the old record
                if (resultItem.Item4.Equals(2))
                {
                    resultItem = new Tuple<int, int, RollResult, int>(resultItem.Item1, pinsToppled, resultItem.Item3, 1);
                    ephemeralLatentFrameResults.Add(item.Key, resultItem);
                }
                else if (resultItem.Item4.Equals(1))
                    AddFrameResult(item.Key, resultItem.Item1 + resultItem.Item2 + pinsToppled, resultItem.Item3.GetDescription());
            }
            foreach (var ephemeralItem in ephemeralLatentFrameResults)
                this._latentFrameResults.Add(ephemeralItem.Key, ephemeralItem.Value);
            #endregion

            if (this._frameStatus.Equals(FrameStatus.NoMoreFramesNormal))
            {
                //Game is over; Tally last score
                AddFrameResult(this._frameCount, this._gameDefinition.PinCount - this._ephemeralPinsStillStanding, string.Empty);
            }
            else if (this._frameStatus.Equals(FrameStatus.NoMoreFramesNormal))
            {
                //Game is over;
                return;
            }
            else if (this._frameStatus.Equals(FrameStatus.ContinuePresentFrame))
            {
                //Not done with present frame. Continue;
                return;
            }
            else if (this._frameStatus.Equals(FrameStatus.GoToNextFrameNormalScoring) || this._frameStatus.Equals(FrameStatus.NoMoreFramesNormal))
            {
                //The frame has ended with normal scoring. We can safely add the result.
                //Simply need to know if all pins toppled
                if (this._rollResult.Equals(RollResult.AllToppled))
                    AddFrameResult(this._frameCount, this._gameDefinition.PinCount, string.Empty);
                else
                    AddFrameResult(this._frameCount, this._gameDefinition.PinCount - this._ephemeralPinsStillStanding, string.Empty);
            }
            else if (this._frameStatus.Equals(FrameStatus.LastFrameNeedSpecialScoringPhase) || this._frameStatus.Equals(FrameStatus.GoToNextFrameSpecialScoring))
            {
                if (FrameResults.Count.Equals(this._gameDefinition.Frames) || this._latentFrameResults.ContainsKey(this._frameCount))
                    return;

                //Going into next frame and can't score current one just yet. Instead, we create a deferred score
                //Dictionary Key: Frame #
                //Value Score, Ball +1, RollResult, extra roll count
                // Tuple<int, int, RollResult, int>
                if (this._rollResult.Equals(RollResult.Strike))
                    this._latentFrameResults.Add(this._frameCount, new Tuple<int, int, RollResult, int>(this._gameDefinition.PinCount, 0, this._rollResult, 2));
                else if (this._rollResult.Equals(RollResult.Spare))
                    this._latentFrameResults.Add(this._frameCount, new Tuple<int, int, RollResult, int>(this._gameDefinition.PinCount, 0, this._rollResult, 1));
            }
        }

        private void PostRoll_SetPinCount() {
            if (this._frameStatus.Equals(FrameStatus.ContinuePresentFrame) || this._frameStatus.Equals(FrameStatus.ExtraFrameEndingPhase))
            {
                //There are pins still standing for another ball roll
                this.SetPinsLeft(this._ephemeralPinsStillStanding);
            }
            else if (this._frameStatus.Equals(FrameStatus.GoToNextFrameNormalScoring) || this._frameStatus.Equals(FrameStatus.GoToNextFrameSpecialScoring) || this._frameStatus.Equals(FrameStatus.NoMoreFramesSpecial) || this._frameStatus.Equals(FrameStatus.LastFrameNeedSpecialScoringPhase))
            {
                //This is a pin reset to start again
                this.SetPinsLeft(this._gameDefinition.PinCount);
            }
        }

        private void PostRoll_Messaging(int pinsToppled)
        {
            StringBuilder sbMessage = new StringBuilder();
            switch(this._frameStatus)
            {
                case FrameStatus.ContinuePresentFrame:
                    string vContinuePresentFrame = $"Frame {_frameCount}, throw {this._rollCount}:  {pinsToppled} pins toppled with {this._ephemeralPinsStillStanding} still standing";
                    sbMessage.AppendLine(vContinuePresentFrame);
                    break;
                case FrameStatus.GoToNextFrameNormalScoring:
                    Tuple<int, string> nextFrameNormalScoring = null;
                    string vGoToNextFrameNormalScoring = string.Empty;
                    if (FrameResults.TryGetValue(this._frameCount, out nextFrameNormalScoring)) {
                        vGoToNextFrameNormalScoring = $"Frame {_frameCount}, throw {this._rollCount}: {pinsToppled} more pins toppled. You scored {nextFrameNormalScoring.Item1}";
                    }
                    else
                    {
                        vGoToNextFrameNormalScoring = $"Frame {_frameCount}: Throw number {this._rollCount} with {pinsToppled} more pins toppled";
                    }
                    sbMessage.AppendLine(vGoToNextFrameNormalScoring);
                    break;
                case FrameStatus.GoToNextFrameSpecialScoring:
                    if (this._rollResult.Equals(RollResult.Strike))
                    {
                        string vGoToNextFrameSpecialScoringStrike = $"Frame {_frameCount}, Strike: you have two more throws before this is scored!";
                        sbMessage.AppendLine(vGoToNextFrameSpecialScoringStrike);
                    }
                    else if (this._rollResult.Equals(RollResult.Spare))
                    {
                        string vGoToNextFrameSpecialScoringSpare = $"Frame {_frameCount}, Spare: you have one more throw before this is scored!";
                        sbMessage.AppendLine(vGoToNextFrameSpecialScoringSpare);
                    }
                    else if (this._rollResult.Equals(RollResult.AllToppled))
                    {
                        sbMessage.AppendLine($"All Toppled");
                    }
                    break;
                case FrameStatus.LastFrameNeedSpecialScoringPhase:
                    if (this._rollResult.Equals(RollResult.Strike))
                    {
                        string vLastFrameNeedSpecialScoringPhaseStrike = $"Frame {_frameCount}, Strike: you have two more throws before finishing!";
                        sbMessage.AppendLine(vLastFrameNeedSpecialScoringPhaseStrike);
                    }
                    else if (this._rollResult.Equals(RollResult.Spare))
                    {
                        string vLastFrameNeedSpecialScoringPhaseSpare = $"Frame {_frameCount}, Spare: you have one more throw before finishing!";
                        sbMessage.AppendLine(vLastFrameNeedSpecialScoringPhaseSpare);
                    }
                    else if (this._rollResult.Equals(RollResult.AllToppled))
                    {
                        sbMessage.AppendLine($"All Toppled");
                    }
                    break;
                case FrameStatus.NoMoreFramesSpecial:
                    Tuple<int, string> noMoreFramesSpecialScoring = null;
                    string vNoMoreFramesSpecialScoring = string.Empty;
                    if (FrameResults.TryGetValue(this._frameCount, out noMoreFramesSpecialScoring))
                    {
                        vNoMoreFramesSpecialScoring = $"Extra roll on frame {_frameCount}, {pinsToppled} more pins toppled. You scored {noMoreFramesSpecialScoring.Item1}!";
                    }
                    else
                    {
                        vNoMoreFramesSpecialScoring = $"Extra roll on frame {_frameCount}, {pinsToppled} more pins toppled! ";
                    }
                    sbMessage.AppendLine(vNoMoreFramesSpecialScoring);
                    break;
                case FrameStatus.ExtraFrameEndingPhase:
                    string vExtraFrameEndingPhase = $"Frame {_frameCount}, {pinsToppled} more pins toppled.";
                    sbMessage.AppendLine(vExtraFrameEndingPhase);
                    break;
                case FrameStatus.NoMoreFramesNormal:
                    Tuple<int, string> noMoreFramesNormalScoring = null;
                    string vNoMoreFramesNormal = string.Empty;
                    if (FrameResults.TryGetValue(this._frameCount, out noMoreFramesNormalScoring))
                    {
                        vNoMoreFramesNormal = $"Frame {_frameCount}, {pinsToppled} more pins toppled. You scored {noMoreFramesNormalScoring.Item1}!";
                    }
                    else
                    {
                        vNoMoreFramesNormal = $"Frame {_frameCount}, throw {this._rollCount}:  {pinsToppled} more pins toppled";
                    }
                    sbMessage.AppendLine(vNoMoreFramesNormal);
                    break;
                default:
                    break;
            }

            SetMessage(sbMessage.ToString());
        }

        private void SetupNext_SetRollCount()
        {
            if (this._frameStatus.Equals(FrameStatus.NoMoreFramesSpecial) || this._frameStatus.Equals(FrameStatus.GoToNextFrameNormalScoring) || this._frameStatus.Equals(FrameStatus.GoToNextFrameSpecialScoring))
            {
                this._rollCount = 0;
            }
        }

        private void SetupNext_SetFrameCount()
        {
            if (this._frameStatus.Equals(FrameStatus.ContinuePresentFrame) || this._frameStatus.Equals(FrameStatus.NoMoreFramesSpecial) || this._frameStatus.Equals(FrameStatus.ExtraFrameEndingPhase) || this._frameStatus.Equals(FrameStatus.NoMoreFramesNormal) || this._frameStatus.Equals(FrameStatus.NoMoreFramesSpecial))
                return;
            else if (this._frameStatus.Equals(FrameStatus.GoToNextFrameNormalScoring) || this._frameStatus.Equals(FrameStatus.NoMoreFramesNormal)
                 || this._frameStatus.Equals(FrameStatus.GoToNextFrameSpecialScoring))
                this._frameCount++;
        }

        private void SetupNext_SetRollStatus()
        {
                this._rollResult = RollResult.None;
        }

        private void SetupNext_SetFrameStatus() {
            if (this._frameStatus.Equals(FrameStatus.ExtraFrameEndingPhase) || this._frameStatus.Equals(FrameStatus.LastFrameNeedSpecialScoringPhase))
                this._frameStatus = FrameStatus.ExtraFrameEndingPhase;
            else if (!this._frameStatus.Equals(FrameStatus.NoMoreFramesNormal) && !this._frameStatus.Equals(FrameStatus.NoMoreFramesSpecial))
                this._frameStatus = FrameStatus.None;
        }

        private void AddFrameResult(int frame, int score, string note)
        {
            if (FrameResults.Count < this._gameDefinition.Frames)
            {
                Tuple<int, string> frameDetail = new Tuple<int, string>(score, note);
                FrameResults.Add(frame, frameDetail);
            }
        }

        private void SetMessage(string message)
        {
            this._message = message;
        }

        private void SetPinsLeft(int pinsLeft)
        {
            this._pinsStillStanding = pinsLeft;
        }

        public Dictionary<int, Tuple<int, string>> FrameResults { get; private set; }


        public int ScoreTotal
        {
            get
            {
                int Score = 0;
                foreach (var item in FrameResults)
                {
                    Tuple<int, String> resultItem = item.Value;
                    Score += resultItem.Item1;
                }
                return Score;
            }
        }

        public bool HasGameEnded
        {
            get
            {
                return this._frameStatus.Equals(FrameStatus.NoMoreFramesNormal) || this._frameStatus.Equals(FrameStatus.NoMoreFramesSpecial);
            }
        }
    }
}
