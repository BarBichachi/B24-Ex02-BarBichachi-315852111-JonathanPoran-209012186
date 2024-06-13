using System;
using System.Linq;
using MemoryGamePieces;

namespace MemoryGameLogic
{
    public class MemoryGame
    {
        private bool m_GameIsRunning = true;
        private readonly Player[] r_Players;
        private eCurrentPlayer m_CurrentPlayer = eCurrentPlayer.FirstPlayer;
        private Board m_gameBoard;
        private readonly int r_gameBoardMinSize = 4;
        private readonly int r_gameBoardMaxSize = 6;

        public MemoryGame(Player[] i_PlayersArray)
        {
            r_Players = i_PlayersArray;
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

        public int GetMinBoardDimension()
        {
            return r_gameBoardMinSize;
        }

        public int GetMaxBoardDimension()
        {
            return r_gameBoardMaxSize;
        }

        public void InitializeMemoryGame()
        {
            m_GameIsRunning = true;
            m_CurrentPlayer = eCurrentPlayer.FirstPlayer;

            foreach (Player player in r_Players)
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
                m_gameBoard.NewPairFound();
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
            r_Players[(int)m_CurrentPlayer].AddScore();
        }

        public string GetCurrentPlayerName()
        {
            return r_Players[(int)m_CurrentPlayer].GetName();
        }

        public ePlayerType GetCurrentPlayerType()
        {
            return r_Players[(int)m_CurrentPlayer].GetPlayerType();
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
            return r_Players.OrderByDescending(player => player.GetScore()).First();
        }

        public Player GetLoser()
        {
            return r_Players.OrderByDescending(player => player.GetScore()).Last();
        }

        public bool IsTheGameEndedAsTie()
        {
            return r_Players[0].GetScore() == r_Players[1].GetScore();
        }

        public bool IsLocationOutOfRange(string i_desiredLocation)
        {
            return m_gameBoard.IsValidLocation(i_desiredLocation);
        }

        public bool FlipCardToFaceUp(string i_ValidLocation)
        {
            return m_gameBoard.TryFlipCard(i_ValidLocation);
        }

        public Board GetBoard()
        {
            return m_gameBoard;
        }

        public string ShuffleRandomBoardLocation()
        {
            Random random = new Random();

            int maxColumn = m_gameBoard.GetNumOfColumns();
            int maxRow = m_gameBoard.GetNumOfRows();

            char randomColumn = (char)('A' + random.Next(maxColumn));
            char randomRow = (char)('1' + random.Next(maxRow));
            
            return string.Concat(randomColumn, randomRow);
        }
    }
}
