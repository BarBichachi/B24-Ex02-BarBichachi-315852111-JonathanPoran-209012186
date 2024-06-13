using System;

namespace MemoryGameLogic
{
    public class Board
    {
        private static Random random = new Random();
        private static int s_Width;
        private static int s_Height;
        private static int m_NumOfPairsLeft;
        private Card[,] m_Cards;

        public Board(int i_Width, int i_Height)
        {
            s_Width = i_Width;
            s_Height = i_Height;
            m_NumOfPairsLeft = (i_Width * i_Height) / 2;
            generatePairsOnBoard();
            shuffleBoard();
        }

        private void generatePairsOnBoard()
        {
            m_Cards = new Card[s_Width, s_Height];
            bool isSecondChar = false;
            char letter = 'A';

            for (int column = 0; column < s_Width; column++)
            {
                for (int row = 0; row < s_Height; row++)
                {
                    m_Cards[row, column].m_Letter = letter;

                    if (!isSecondChar)
                    {
                        isSecondChar = true;
                    }
                    else
                    {
                        letter++;
                        isSecondChar = false;
                    }
                }
            }
        }

        private void shuffleBoard()
        {
            int numOfShuffles = s_Width * s_Height;
            
            for (int i = 0; i < numOfShuffles; i++)
            {
                swapCards(m_Cards[random.Next(s_Width), random.Next(s_Height)]
                        , m_Cards[random.Next(s_Width), random.Next(s_Height)]);
            }
        }

        private void swapCards(Card i_FirstCard, Card i_SecondCard)
        {
            (i_FirstCard.m_Letter, i_SecondCard.m_Letter) = (i_SecondCard.m_Letter, i_FirstCard.m_Letter);
        }

        public void PairRevealed()
        {
            m_NumOfPairsLeft--;
        }
        public int GetNumOfPairsLeft()
        {
            return m_NumOfPairsLeft;
        }
    }
}
