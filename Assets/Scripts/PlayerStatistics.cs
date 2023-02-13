using UnityEngine;

namespace SpaceShooter
{
    public class PlayerStatistics : MonoBehaviour
    {
        public int m_StatNumKills;

        public int m_StatScore;
        
        public int m_StatKillsBonusScore;

        public int m_StatTime;

        public int m_StatTimeBonusScore;

        public int m_StatRecordScore;

        public int m_StatRecordKills;

        public int m_StatRecordTime;

        public void Reset()
        {
            //m_StatNumKills = 0;
            m_StatScore = 0;
            //m_StatKillsBonusScore = 0;
            m_StatTime = 0;
            //m_StatTimeBonusScore = 0;
        }
    }
}

