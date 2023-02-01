using TMPro;
using UnityEngine;

namespace SpaceShooter
{
    public class MenuCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_RecordScore, m_RecordKills, m_RecordTime;

        public void Awake()
        {
            PlayerStatistics playerStatistics = new PlayerStatistics();
            m_RecordScore.text = "Best Score : " + playerStatistics.m_RecordScore.ToString();
            m_RecordKills.text = "Best number of Kills : " + playerStatistics.m_RecordKills.ToString();
            m_RecordTime.text = "Best Time : " + playerStatistics.m_RecordTime.ToString();
        }
    }
}
