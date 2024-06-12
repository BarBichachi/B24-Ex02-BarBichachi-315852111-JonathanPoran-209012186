using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameLogic
{
    public class Board
    {
        private static int s_Width;
        private static int s_Height;
        private static int m_NumOfPairsLeft;

        public void SetBoardDimensions(int i_Width, int i_Height)
        {
            s_Width = i_Width;
            s_Height = i_Height;
            m_NumOfPairsLeft = (i_Width * i_Height) / 2;
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
