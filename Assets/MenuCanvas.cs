using TMPro;
using UnityEngine;

namespace SpaceShooter
{
    public class MenuCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_RecordScore, m_RecordKills, m_RecordTime;

        private void Update()
        {
            if (LevelSequenceController.Instance.m_UnityEvent != null)
            {
                LevelSequenceController.Instance.m_UnityEvent.AddListener(OnChange);
            }
        }

        private void OnChange()
        {
            m_RecordScore.text = "Best Score : " + LevelSequenceController.Instance.LevelStatistics.m_RecordScore.ToString();
            m_RecordKills.text = "Best number of Kills : " + LevelSequenceController.Instance.LevelStatistics.m_RecordKills.ToString();
            m_RecordTime.text = "Best Time : " + LevelSequenceController.Instance.LevelStatistics.m_RecordTime.ToString();
        }
    }
}
