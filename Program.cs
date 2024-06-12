using System;
using MemoryGameLogic;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B24_Ex02_BarBichachi_315852111_JonathanPoran_209012186
{
    public class Program
    {
        static void Main()
        {
            bool keepPlaying = true;

            MemoryGame memoryGame = new MemoryGame();

            while (memoryGame.GetGameStatus() != eGameStatus.InProgress)
            {
                memoryGame.PlayGame();

                if (memoryGame.GetGameStatus() == eGameStatus.Completed)
                {
                    UI.DisplayMessageNewLine("Do you want to play another game? (Y/N)"); 
                    //keepPlaying = UI.GetInput(); -- (Y/N)
                    //memoryGame.WantAnotherGame();
                }
                else
                {
                    UI.DisplayMessageNewLine("The game ended because the 'Q' key was pressed.");
                    keepPlaying = false;
                }
            }
            UI.DisplayMessageNewLine("Thank you for playing! see you next time.");

        }
    }
}
