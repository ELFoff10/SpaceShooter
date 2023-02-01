using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private TextMeshProUGUI m_Kills, m_KillsBonusScore, m_Score,
            m_Time, m_TimeBonusScore, m_Result, m_ButtonNextText;

        private bool m_Success; // Определяет завершили мы уровень или нет

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowResults(PlayerStatistics levelResults, bool success)
        {
            m_Kills.text = "Kills : " + levelResults.m_NumKills.ToString();

            if (levelResults.m_NumKills == 0)
            {
                m_KillsBonusScore.text = "Not a Killer... + 0";
            }

            if (levelResults.m_NumKills > 0 && levelResults.m_NumKills <= 7)
            {
                m_KillsBonusScore.text = "Killer! + " + levelResults.m_KillsBonusScore.ToString() + " points";
                levelResults.m_Score += levelResults.m_KillsBonusScore;
            }

            if (levelResults.m_NumKills > 7 && levelResults.m_NumKills <= 13)
            {
                m_KillsBonusScore.text = "Butcher! + " + levelResults.m_KillsBonusScore.ToString() + " points";
                levelResults.m_Score += levelResults.m_KillsBonusScore;
            }

            if (levelResults.m_NumKills > 13)
            {
                m_KillsBonusScore.text = "Beast! + " + levelResults.m_KillsBonusScore.ToString() + " points";
                levelResults.m_Score += levelResults.m_KillsBonusScore;
            }

            m_Time.text = "Time : " + levelResults.m_Time.ToString();

            if (levelResults.m_Time >= 0 && levelResults.m_Time <= 10)
            {
                m_TimeBonusScore.text = "Fast!!! + 100 points!!!";
                levelResults.m_Score = + 100;
            }
            if (levelResults.m_Time > 10 && levelResults.m_Time <= 30)
            {
                m_TimeBonusScore.text = "Fast!!! + 50 points!";
                levelResults.m_Score = + 50;
            }
            if (levelResults.m_Time > 30)
            {
                m_TimeBonusScore.text = "Slow!!! + 0 points";
            }

            m_Score.text = "Score : " + levelResults.m_Score.ToString();            

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

