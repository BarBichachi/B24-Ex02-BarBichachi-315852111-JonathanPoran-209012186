using System;
namespace MemoryGamePieces
{
    public class Board
    {
        private readonly Random r_Random = new Random();
        private readonly int r_Width;
        private readonly int r_Height;
        private int m_NumOfPairsLeft;
        private Card[,] m_Cards;

        public Board(int i_Width, int i_Height)
        {
            r_Width = i_Width;
            r_Height = i_Height;
            m_NumOfPairsLeft = (i_Width * i_Height) / 2;
            generatePairsOnBoard();
            shuffleBoard();
        }

        public int GetNumOfColumns()
        {
            return r_Width;
        }

        public int GetNumOfRows()
        {
            return r_Height;
        }

        public void NewPairFound()
        {
            m_NumOfPairsLeft--;
        }

        private void generatePairsOnBoard()
        {
            m_Cards = new Card[r_Width, r_Height];
            bool isSecondLetter = false;
            char letter = 'A';

            for (int row = 0; row < r_Height; row++)
            {
                for (int column = 0; column < r_Width; column++)
                {
                    m_Cards[column, row] = new Card(letter);

                    if (!isSecondLetter)
                    {
                        isSecondLetter = true;
                    }
                    else
                    {
                        letter++;
                        isSecondLetter = false;
                    }
                }
            }
        }

        private void shuffleBoard()
        {
            int numOfShuffles = r_Width * r_Height;
            
            for (int i = 0; i < numOfShuffles; i++)
            {
                swapCards(m_Cards[r_Random.Next(r_Width), r_Random.Next(r_Height)]
                        , m_Cards[r_Random.Next(r_Width), r_Random.Next(r_Height)]);
            }
        }

        private void swapCards(Card i_FirstCard, Card i_SecondCard)
        {
            char tempFirstCardLetter = i_FirstCard.GetLetter();
            i_FirstCard.SetLetter(i_SecondCard.GetLetter());
            i_SecondCard.SetLetter(tempFirstCardLetter);
        }

        public char GetLetterOfCardByStringLocation(string i_ValidLocation)
        {
            int column = (int)i_ValidLocation[0] - 'A';
            int row = (int)(i_ValidLocation[1] - '1');

            return m_Cards[column, row].ShowCard();
        }

        public char GetLetterOfCardByLocation(int i_Column, int i_Row)
        {
            return m_Cards[i_Column, i_Row].ShowCard();
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

            return (column >= 0 && column < r_Width && row >= 0 && row < r_Height);
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
