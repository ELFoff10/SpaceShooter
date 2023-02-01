using UnityEngine;

namespace SpaceShooter
{
    public class PlayerStatistics : MonoBehaviour
    {
        public int m_NumKills = 0;

        public int m_Score = 0;

        public int m_KillsBonusScore = 0;

        public int m_Time = 0;

        public int m_TimeBonusScore = 0;

        public int m_Bonus = 100;

        public int m_RecordScore = 0;

        public int m_RecordTime = 0;

        public int m_RecordKills = 0;

        public void Reset()
        {
            m_NumKills = 0;
            m_Score = 0;
            m_KillsBonusScore = 0;
            m_Time = 0;
            m_TimeBonusScore = 0;
        }
    }
}

