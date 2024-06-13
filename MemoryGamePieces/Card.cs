namespace MemoryGamePieces
{
    public class Card
    {
        private char m_Letter;
        private bool m_IsFaceUp = false;

        public Card(char i_Letter)
        {
            this.m_Letter = i_Letter;
        }

        public void FlipCard()
        {
            m_IsFaceUp = !m_IsFaceUp;
        }

        public bool IsFaceUp()
        {
            return m_IsFaceUp;
        }

        public char ShowCard()
        {
            char letter = ' ';

            if (m_IsFaceUp)
            {
                letter = m_Letter;
            }

            return letter;
        }

        internal char GetLetter()
        {
            return m_Letter;
        }

        internal void SetLetter(char i_Letter)
        {
            m_Letter = i_Letter;
        }
    }
}
