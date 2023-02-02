using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ScoreStats : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_ScoreText;

        public int m_ScoreStatsLastScore;

        private void Update()
        {
            UpdateScore();
        }

        private void UpdateScore()
        {
            if (Player.Instance != null)
            {
                int currentScore = Player.Instance.m_PlayerScore;

                if (m_ScoreStatsLastScore != currentScore) // Т.к метод в апдэйте, то при условии if мы только когда надо отрисовывем очки
                {
                    m_ScoreStatsLastScore = currentScore;

                    m_ScoreText.text = "Score : " + m_ScoreStatsLastScore.ToString();
                }
            }
        }
    }
} 
