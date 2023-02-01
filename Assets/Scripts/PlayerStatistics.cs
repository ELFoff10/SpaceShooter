using UnityEngine;

namespace SpaceShooter
{
    public class PlayerStatistics : MonoBehaviour
    {
        public int m_NumKills;

        public int m_Score;

        public int m_KillsBonusScore;

        public int m_Time;

        public int m_TimeBonusScore;

        public int m_Bonus = 100;

        public int m_RecordScore;

        public int m_RecordTime;

        public int m_RecordKills;

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

