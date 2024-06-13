using System;
using System.Linq;
using MemoryGamePieces;

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

        public void NextPlayer()
        {
            int nextValue = ((int)m_CurrentPlayer + 1) % Enum.GetValues(typeof(eCurrentPlayer)).Length;
            m_CurrentPlayer = (eCurrentPlayer)nextValue;
        }

        public bool IsGameStillRunning()
        {
            return m_GameIsRunning;
        }

        public bool ProcessedMatch(string i_FirstValidLocation, string i_SecondValidLocation)
        {
            bool isThereAMatch = false;

            char firstCardLetter = m_gameBoard.GetLetterOfCardByStringLocation(i_FirstValidLocation);
            char secondCardLetter = m_gameBoard.GetLetterOfCardByStringLocation(i_SecondValidLocation);

            if (firstCardLetter == secondCardLetter)
            {
                isThereAMatch = true;
                addScoreToCurrentPlayer();
            }

            return isThereAMatch;
        }

        public void FlipCardsFaceDown(string i_FirstValidLocation, string i_SecondValidLocation)
        {
            m_gameBoard.FlipCardFaceDown(i_FirstValidLocation);
            m_gameBoard.FlipCardFaceDown(i_SecondValidLocation);
        }

        private void addScoreToCurrentPlayer()
        {
            m_Players[(int)m_CurrentPlayer].AddScore();
        }

        public string GetCurrentPlayerName()
        {
            return m_Players[(int)m_CurrentPlayer].GetName();
        }

        public bool EndGameIfFinished()
        {
            if (m_gameBoard.GetNumOfPairsLeft() == 0)
            {
                m_GameIsRunning = false;
            }
            return !m_GameIsRunning;
        }

        public Player GetWinner()
        {
            return m_Players.OrderByDescending(player => player.GetScore()).First();
        }

        public bool IsTheGameEndedAsTie()
        {
            return m_Players[0].GetScore() == m_Players[1].GetScore();
        }

        public bool IsLocationOutOfRange(string i_desiredLocation)
        {
            return m_gameBoard.IsValidLocation(i_desiredLocation);
        }

        public bool FlipCardToFaceUp(string i_ValidLocation)
        {
            return m_gameBoard.TryFlipCard(i_ValidLocation);
        }

        public ref Board GetBoard()
        {
            return ref m_gameBoard;
        }
    }
}
