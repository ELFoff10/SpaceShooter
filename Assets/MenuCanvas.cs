using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceShooter
{
    public class MenuCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_RecordScores, m_RecordKillss, m_RecordTimes;

        public void LateUpdate()
        {
            Change();
        }

        public void Change()
        {
            LevelSequenceController sequenceController = LevelSequenceController.Instance.GetComponent<LevelSequenceController>();

            if (sequenceController != null)
            {
                m_RecordScores.text = "Best Score : " + sequenceController.LevelStatistics.m_StatRecordScore.ToString();
                m_RecordKillss.text = "Best Kills : " + sequenceController.LevelStatistics.m_StatRecordKills.ToString();
                m_RecordTimes.text = "Best Time : " + sequenceController.LevelStatistics.m_StatRecordTime.ToString();
            }

        }
    }
}
