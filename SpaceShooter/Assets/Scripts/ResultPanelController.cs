using TMPro;
using UnityEngine;

namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private TextMeshProUGUI m_TextKills, m_TextKillsBonusScore, m_TextScore,
            m_TextTime, m_TextTimeBonusScore, /*m_TextResult*/ m_TextButtonNext;

        [SerializeField] private GameObject m_ImageWin;
        [SerializeField] private GameObject m_ImageLose;
        [SerializeField] private GameObject m_Stats;


        private bool m_Success; // Определяет завершили мы уровень или нет

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowResults(PlayerStatistics levelResults, bool success)
        {
            m_TextKills.text = "Kills : " + levelResults.m_StatNumKills.ToString();

            if (levelResults.m_StatNumKills == 0)
            {
                m_TextKillsBonusScore.text = "Not a Killer... + 0";
            }

            if (levelResults.m_StatNumKills > 0 && levelResults.m_StatNumKills <= 7)
            {
                m_TextKillsBonusScore.text = "Killer! +" + levelResults.m_StatKillsBonusScore.ToString() + " points";
                levelResults.m_StatScore += levelResults.m_StatKillsBonusScore;
            }

            if (levelResults.m_StatNumKills > 7 && levelResults.m_StatNumKills <= 13)
            {
                m_TextKillsBonusScore.text = "Butcher! +" + levelResults.m_StatKillsBonusScore.ToString() + " points";
                levelResults.m_StatScore += levelResults.m_StatKillsBonusScore;
            }

            if (levelResults.m_StatNumKills > 13)
            {
                m_TextKillsBonusScore.text = "Beast! +" + levelResults.m_StatKillsBonusScore.ToString() + " points";
                levelResults.m_StatScore += levelResults.m_StatKillsBonusScore;
            }

            m_TextTime.text = "Time : " + levelResults.m_StatTime.ToString();

            if (levelResults.m_StatTime >= 0 && levelResults.m_StatTime <= 10)
            {
                m_TextTimeBonusScore.text = "Super Fast!!! +100 points!";
                levelResults.m_StatScore += 100;
            }
            if (levelResults.m_StatTime > 10 && levelResults.m_StatTime <= 30)
            {
                m_TextTimeBonusScore.text = "Fast! +50 points!";
                levelResults.m_StatScore += 50;
            }
            if (levelResults.m_StatTime > 30)
            {
                m_TextTimeBonusScore.text = "Slow... 0 points";
            }

            levelResults.m_StatRecordScore = levelResults.m_StatScore;
            levelResults.m_StatRecordKills = levelResults.m_StatNumKills;
            levelResults.m_StatRecordTime = levelResults.m_StatTime;

            m_TextScore.text = "Score : " + levelResults.m_StatScore.ToString();

            gameObject.SetActive(true);

            m_Success = success;

            if (success == true)
            {
                m_ImageWin.gameObject.SetActive(true);
            }
            else 
            {
                m_ImageLose.gameObject.SetActive(true);
                m_Stats.gameObject.SetActive(false);
            }

            //m_Result.text = success ? "Win" : "Lose";

            m_TextButtonNext.text = success ? "Next" : "Restart";

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

