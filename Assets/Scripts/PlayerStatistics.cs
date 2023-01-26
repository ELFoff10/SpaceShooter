using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PlayerStatistics : MonoBehaviour
    {
        public int m_NumKills;

        public int m_Score;

        public int m_Time;

        public void Reset() // Ìועמה סבנאסûגאוע
        {
            m_NumKills = 0;
            m_Score = 0;
            m_Time = 0;
        }
    }
}

