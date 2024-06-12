using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameLogic
{
    public class MemoryGame
    {
        private eGameStatus m_GameStatus = eGameStatus.InProgress;
        private Player[] m_Players = new Player[2];
        private eCurrentPlayer m_CurrentPlayer = eCurrentPlayer.FirstPlayer;

        // Board minimum & maximum dimensions
        private Board m_gameBoard;
        private readonly int r_gameBoardMinSize = 4;
        private readonly int r_gameBoardMaxSize = 6;

        public MemoryGame()
        {
            m_Players[0] = new Player();
            createSecondPlayerByType();
        }

        public void PlayGame()
        {
            createBoard();
            UI.PrintBoard();

            while (m_GameStatus == eGameStatus.InProgress)
            {
                bool currentPlayerIsPlaying = true;

                while (currentPlayerIsPlaying)
                {
                    ePlayerTurnResult playerTurnResult = m_Players[(int)m_CurrentPlayer].PlayTurn();

                    if (playerTurnResult == ePlayerTurnResult.FlippedPair)
                    {
                        m_gameBoard.PairRevealed();

                        if (AreThereAnyCardsToReveal())
                        {
                            m_GameStatus = eGameStatus.Completed;
                            break;
                        }
                    }
                    else if (playerTurnResult == ePlayerTurnResult.DidNotFlipPair)
                    {
                        NextPlayer();
                    }
                    else
                    {
                        m_GameStatus = eGameStatus.PlayerQuit;
                        break;
                    }
                }
            }
        }

        private void createSecondPlayerByType()
        {
            bool isValidInput = false;

            UI.DisplayMessageNewLine("Who do you want to play against?");
            UI.DisplayMessageNewLine("1. Computer");
            UI.DisplayMessageNewLine("2. Another player");

            int userChoice = UI.GetIntValues((int)ePlayerType.Computer, (int)ePlayerType.Human, "choice");

            if (userChoice == (int)ePlayerType.Computer)
            {
                m_Players[1] = new Player("Computer");
            }
            else // Human
            {
                m_Players[1] = new Player();
            }
        }

        private void createBoard()
        {
            // TODO
            //UI.DisplayMessageNewLine("Board dimensions example - 6x4");
            //UI.DisplayMessageInLine("Please enter your board dimensions ( <Width>x<Height> ): ");

            int boardWidth = UI.GetIntValues(r_gameBoardMinSize, r_gameBoardMaxSize, "board width");
            int boardHeight = UI.GetIntValues(r_gameBoardMinSize, r_gameBoardMaxSize, "board height");

            m_gameBoard.SetBoardDimensions(boardWidth, boardHeight);
        }

        private bool AreThereAnyCardsToReveal()
        {
            return m_gameBoard.GetNumOfPairsLeft() != 0;
        }

        private void NextPlayer()
        {
            int nextValue = ((int)m_CurrentPlayer + 1) % Enum.GetValues(typeof(eCurrentPlayer)).Length;
            m_CurrentPlayer = (eCurrentPlayer)nextValue;
        }

        public eGameStatus GetGameStatus()
        {
            return m_GameStatus;
        }
        public void WantAnotherGame()
        {
            m_GameStatus = eGameStatus.InProgress;
        }
    }
}
