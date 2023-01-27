using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ScoreStats : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_Text;

        private int m_LastScore;

        private void Update()
        {
            UpdateScore();
        }

        private void UpdateScore()
        {
            if (Player.Instance != null)
            {
                int currentScore = Player.Instance.Score;

                if (m_LastScore != currentScore) // Т.к метод в апдэйте, то при условии if мы только когда надо отрисовывем очки
                {
                    m_LastScore = currentScore;

                    m_Text.text = "Score : " + m_LastScore.ToString();
                }
            }
        }
    }
} 
