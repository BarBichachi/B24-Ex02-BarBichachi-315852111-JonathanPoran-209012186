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
            UI.ShowWelcomeMessage();
            Player[] playersArray = UI.SetPlayers();
            MemoryGame memoryGame = new MemoryGame(playersArray);
            UI memoryGameUI = new UI(memoryGame);

            while (wantToPlay)
            {
                memoryGameUI.RunNewGame();

                if (memoryGame.IsGameStillRunning())
                {
                    memoryGameUI.EndedBecauseOfQ();
                    wantToPlay = false;
                }
                else
                {
                    memoryGameUI.PrintGameStatistics();
                    wantToPlay = memoryGameUI.AskPlayerForAnotherGame();
                }
            }

            memoryGameUI.ProgramEnded();
        }
    }
}
