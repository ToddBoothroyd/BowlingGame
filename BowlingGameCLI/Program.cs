using System;
using System.Collections.Generic;
using ToddBoothroyd_BowlingGame;
using static System.Console;

namespace BowlingGameCLI
{
    /// <summary>
    /// This console implements a simple ten-pin bowling game run by a game manager.
    /// This game manager can be modified to run other bowling games, such as candlepin.
    /// 
    /// 
    /// </summary>
    class Program
    {
        static GameStatus _gameStatus;

        static void Main(string[] args)
        {

            //Instantiate game and wire events
            BowlingGameMgr gameMgr = new BowlingGameMgr(BowlingGameTypes.TenPin);
            gameMgr.GameStatusNotification += new EventHandler<GameStatusEventArgs>(GetGameStatus);
            gameMgr.GameMessageNotification += new EventHandler<GameMessageEventArgs>(GetGameMessage);

            //Start Game
            gameMgr.LoadGame();

            //Game status of ere, 'before the start', allows a welcome and key press to start the game
            if(_gameStatus.Equals(GameStatus.Ere))
            {
                WriteLine($"Welcome to {gameMgr.GetGameName()}. Press any key to start playing!");
                ReadKey();
                gameMgr.PlayGame();

            }

            //Utilize a loop focusing on the 'in progress' status to roll each frame
            while (_gameStatus.Equals(GameStatus.InProgress))
            {
                WriteLine("Press any key to roll");
                ReadKey();
                gameMgr.PlayGame();
            }

            //Check on game status of 'complete' to launch a new game or perhaps exit.
            if (_gameStatus.Equals(GameStatus.Complete))
            {
                DisplayGameResults(gameMgr.GameResults);
                WriteLine($"Thank you for playing! Press any key to exit.");
                ReadKey();
                Environment.Exit(0);
            }

        }

        static void DisplayGameResults(Dictionary<int, Tuple<int, string>> gameResults)
        {
            WriteLine();
            WriteLine("Game Results");
            WriteLine("Frame # | Score | Note");
            int Score = 0;
            foreach (var item in gameResults)
            {
                Tuple<int, String> resultItem = item.Value;
                Score += resultItem.Item1;
                Console.WriteLine($"{item.Key} | {resultItem.Item1} | {resultItem.Item2} ");
            }
            WriteLine("Cumulative Score: " + Score);
        }

        /// <summary>
        /// GetGameStatus provides the status of a game with every event in the game.
        /// Utilize this status to control the display of information to player.
        /// </summary>
        /// <param name="sender">sender is the class generating the event</param>
        /// <param name="e">arguments is the information for the event</param>
        static void GetGameStatus(object sender, GameStatusEventArgs e)
        {
            //WriteLine("Game Status Event provides three properties: GameStatus enum '{0}'; Message '{1}', Date Time Stamp '{2}'.", e.GameStatus, e.Message, e.DateTimeStamp);
            _gameStatus = e.GameStatus;
        }

        /// <summary>
        /// Get Game Message event returns game play information for each event
        /// </summary>
        /// <param name="sender">sender is the class generating the event</param>
        /// <param name="e">arguments is the information for the event</param>
        static void GetGameMessage(object sender, GameMessageEventArgs e)
        {
            //WriteLine("Game Message Event provides two properties: GameMessage '{0}'; Date Time Stamp '{1}'.", e.GameMessage, e.DateTimeStamp);
            WriteLine(e.GameMessage);
        }
    }
}
