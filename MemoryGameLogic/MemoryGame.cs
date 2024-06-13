using System;

namespace MemoryGameLogic
{
    public class MemoryGame
    {
        private bool m_GameIsRunning = true;
        private Player[] m_Players;
        private eCurrentPlayer m_CurrentPlayer = eCurrentPlayer.FirstPlayer;

        // Board minimum & maximum dimensions
        private Board m_gameBoard;
        private readonly int r_gameBoardMinSize = 4;
        private readonly int r_gameBoardMaxSize = 6;

        public MemoryGame(ref Player[] i_PlayersArray)
        {
            m_Players = i_PlayersArray;
        }

        public eBoardDimensionsValidation SetBoardDimensions(int i_Width, int i_Height)
        {
            eBoardDimensionsValidation result = eBoardDimensionsValidation.ValidDimensions;

            if ((i_Width * i_Height) % 2 == 1)
            {
                result = eBoardDimensionsValidation.OddCardCount;
            }
            else if (i_Width < r_gameBoardMinSize || i_Width > r_gameBoardMaxSize || 
                     i_Height < r_gameBoardMinSize || i_Height > r_gameBoardMaxSize)
            {
                result = eBoardDimensionsValidation.OutOfAllowedRange;
            }
            else
            {
                GenerateBoard(i_Width, i_Height);
            }

            return result;
        }

        public void GenerateBoard(int i_Width, int i_Height)
        {
            m_gameBoard = new Board(i_Width, i_Height);
        }

        public int GetMin()
        {
            return r_gameBoardMinSize;
        }

        public int GetMax()
        {
            return r_gameBoardMaxSize;
        }

        public void InitializeMemoryGame()
        {
            m_GameIsRunning = true;
            m_CurrentPlayer = eCurrentPlayer.FirstPlayer;

            foreach (Player player in m_Players)
            {
                player.ResetScore();
            }
        }

        private bool areThereAnyCardsToReveal()
        {
            return m_gameBoard.GetNumOfPairsLeft() != 0;
        }

        public ePlayerType GetCurrentPlayerType()
        {
            return m_Players[(int)m_CurrentPlayer].GetPlayerType();
        }

        public void NextPlayer()
        {
            int nextValue = ((int)m_CurrentPlayer + 1) % Enum.GetValues(typeof(eCurrentPlayer)).Length;
            m_CurrentPlayer = (eCurrentPlayer)nextValue;
        }

        public bool IsGameStillRunning()
        {
            return m_GameIsRunning;
        }

        public void WantAnotherGame()
        {
            m_GameIsRunning = eGameStatus.InProgress;
        }
    }
}
