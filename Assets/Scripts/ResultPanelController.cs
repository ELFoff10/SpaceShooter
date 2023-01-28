using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private TextMeshProUGUI m_Kills, m_Score, m_Time, m_Result, m_ButtonNextText;

        private bool m_Success; // Определяет завершили мы уровень или нет

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowResults(PlayerStatistics levelResults, bool success)
        {
            m_Kills.text = "Kills : " + levelResults.m_NumKills.ToString();

            m_Score.text = "Score : " + levelResults.m_Score.ToString();

            m_Time.text = "Time : " + levelResults.m_Time.ToString();

            m_Success = success;

            m_Result.text = success ? "Win" : "Lose";

            m_ButtonNextText.text = success ? "Next" : "Restart";

            gameObject.SetActive(true);

            Time.timeScale = 0;
        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1;

            if (m_Success)
            {
                LevelSequenceController.Instance.AdvanceLevel();
            }
            else 
            {
                LevelSequenceController.Instance.RestartLevel();
            }
        }
    }
}

