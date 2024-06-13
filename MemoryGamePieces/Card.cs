namespace MemoryGameLogic
{
    public class Card
    {
        public char m_Letter { get; set; }
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
    }
}
