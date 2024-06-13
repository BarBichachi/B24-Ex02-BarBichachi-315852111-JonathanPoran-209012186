namespace MemoryGamePieces
{
    public class Player
    {
        private string m_Name;
        private int m_Score;
        private ePlayerType m_PlayerType = ePlayerType.Human;

        public Player(string i_Name, ePlayerType i_PlayerType)
        {
            m_Name = i_Name;
            m_Score = 0;
            m_PlayerType = i_PlayerType;
        }

        public void ChangePlayer(string i_Name, ePlayerType i_PlayerType)
        {
            m_Name = i_Name;
            m_Score = 0;
            m_PlayerType = i_PlayerType;
        }

        public void ResetScore()
        {
            m_Score = 0;
        }

        public void AddScore()
        {
            this.m_Score++;
        }

        public ePlayerType GetPlayerType()
        {
            return m_PlayerType;
        }

        public string GetName()
        {
            return m_Name;
        }

        public int GetScore()
        {
            return m_Score;
        }
    }
}

