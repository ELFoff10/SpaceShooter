using TMPro;
using UnityEngine;

namespace SpaceShooter
{
    public class MenuCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_RecordScores, m_RecordKillss, m_RecordTimes;

        LevelSequenceController m_LevelSequenceController;

        private void Start()
        {
            //m_LevelSequenceController = // сдесь надо как-то найти LevelSequenceController 
        }

        public void Update()
        {
            OnChange(m_LevelSequenceController.LevelStatistics);
        }

        private void OnChange(PlayerStatistics playerStatistics)
        {
            m_RecordScores.text = "Best Score : " + playerStatistics.m_Score.ToString();
            m_RecordKillss.text = "Best number of Kills : " + playerStatistics.m_RecordKills.ToString();
            m_RecordTimes.text = "Best Time : " + playerStatistics.m_RecordTime.ToString();
        }
    }
}
