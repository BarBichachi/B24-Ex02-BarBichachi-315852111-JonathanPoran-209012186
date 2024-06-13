using System;

using MemoryGameLogic;
using MemoryGamePieces;

namespace B24_Ex02_BarBichachi_315852111_JonathanPoran_209012186
{
    public class Program
    {
        static void Main()
        {
            bool wantToPlay = true;

            // 1-3
            Player[] playersArray = UI.SetPlayers();
            MemoryGame memoryGame = new MemoryGame(ref playersArray);

            while (wantToPlay)
            {
                // 4-12 (INCLUDING 14)
                UI.RunNewGame(memoryGame);

                // 13 (DID THE GAME ENDED BECAUSE IT WAS COMPLETED OR PRESSED Q
                if (memoryGame.IsGameStillRunning())
                {
                    UI.EndedBecauseOfQ();
                    wantToPlay = false;
                }
                else
                {
                    UI.PrintGameStatistics(memoryGame);
                    wantToPlay = UI.AskPlayerForAnotherGame();
                }
            }

            UI.ProgramEnded();
        }
    }
}
