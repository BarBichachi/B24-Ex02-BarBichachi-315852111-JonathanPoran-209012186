namespace MemoryGamePieces
{
    public class Player
    {
        private readonly string r_Name;
        private int m_Score;
        private readonly ePlayerType r_PlayerType = ePlayerType.Human;

        public Player(string i_Name, ePlayerType i_PlayerType)
        {
            r_Name = i_Name;
            m_Score = 0;
            r_PlayerType = i_PlayerType;
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
            return r_PlayerType;
        }

        public string GetName()
        {
            return r_Name;
        }

        public int GetScore()
        {
            return m_Score;
        }
    }
}

