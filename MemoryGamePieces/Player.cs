namespace MemoryGameLogic
{
    public class Player
    {
        private string m_Name;
        private int m_Score;
        private ePlayerType m_PlayerType = ePlayerType.Human;

        public Player(string i_Name, ePlayerType i_PlayerType)
        {
            m_Name = i_Name;
            m_Score = 0;
            m_PlayerType = i_PlayerType;
        }

        public void ResetScore()
        {
            m_Score = 0;
        }

        public ePlayerType GetPlayerType()
        {
            return m_PlayerType;
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
