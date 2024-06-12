using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameLogic
{
    public class Player
    {
        private string m_Name;
        private int m_Score;
        private bool m_IsPlaying;
        private bool m_IsPerson = false;
        private ePlayerType m_PlayerType = ePlayerType.Human;

        public Player()
        {
            UI.DisplayMessageNewLine("Please enter your player name: ");
            m_Name = UI.GetInput();
            m_IsPerson = true;
        }

        public Player(string i_Name)
        {
            m_Name = i_Name;
            m_PlayerType = ePlayerType.Computer;
        }

        public ePlayerTurnResult PlayTurn()
        {
            // Get player first choice & validate (CHECK IF HE PRESSED Q)
            // Print the board again after the flip (flip & clear screen)

            // Get player second choice & validate (CHECK IF HE PRESSED Q)
            // Print the board again after the flip (flip & clear screen)

            // Check if there's a match

            // if (match)
            //   cards need to have isflipped to be true 
            //   player receives a point
            //   return true

            // else
            //   wait 2 seconds
            //   return card to beginning
            //   return false because turn ended

            return ePlayerTurnResult.DidNotFlipPair;
        }

        public string GetPlayerChoice()
        {
            bool isValidChoice = false;
            while (!isValidChoice)
            {
                string playerChoice = UI.GetInput();

                if (playerChoice.Length == 2)
                {

                }
            }
        }
    }
}
