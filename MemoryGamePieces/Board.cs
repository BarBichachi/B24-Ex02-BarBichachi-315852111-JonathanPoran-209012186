using System;
namespace MemoryGamePieces
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
            //shuffleBoard();
        }

        public int GetNumOfColumns()
        {
            return s_Width;
        }

        public int GetNumOfRows()
        {
            return s_Height;
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
                    m_Cards[column, row]= new Card(letter);

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

        public char GetLetterOfCardByStringLocation(string i_ValidLocation)
        {
            int column = (int)i_ValidLocation[0] - 'A';
            int row = (int)(i_ValidLocation[1] - '1');

            return m_Cards[column, row].m_Letter;
        }

        public char GetLetterOfCardByLocation(int i_Row, int i_Column)
        {
            return m_Cards[i_Column, i_Row].m_Letter;
        }

        public void PairRevealed()
        {
            m_NumOfPairsLeft--;
        }
        public int GetNumOfPairsLeft()
        {
            return m_NumOfPairsLeft;
        }

        public void FlipCardFaceDown(string i_ValidLocation)
        {
            int column = (int)i_ValidLocation[0] - 'A';
            int row = (int)(i_ValidLocation[1] - '1');

            m_Cards[column, row].FlipCard();
        }

        public bool IsValidLocation(string i_desiredLocation)
        {
            int column = (int)i_desiredLocation[0] - 'A';
            int row = (int)(i_desiredLocation[1] - '1');

            return (column >= 0 && column < s_Width && row >= 0 && row < s_Height);
        }

        public bool TryFlipCard(string i_ValidLocation)
        {
            bool isManagedToFlip = false;
            int column = (int)i_ValidLocation[0] - 'A';
            int row = (int)(i_ValidLocation[1] - '1');

            if (!(m_Cards[column, row].IsFaceUp()))
            {
                isManagedToFlip = true;
                m_Cards[column, row].FlipCard();
            }

            return isManagedToFlip;
        }
    }
}
