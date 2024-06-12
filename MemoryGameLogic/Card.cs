using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameLogic
{
    public class Card
    {
        private readonly char r_Letter;
        private bool m_IsFaceUp = false;

        public Card(char i_Letter)
        {
            this.r_Letter = i_Letter;
        }

        public void FlipCard()
        {
            m_IsFaceUp = true;
        }

        public void PrintCard()
        { 
            UI.DisplayMessageInLine(m_IsFaceUp ? r_Letter.ToString() : " ");
        }
    }
}
