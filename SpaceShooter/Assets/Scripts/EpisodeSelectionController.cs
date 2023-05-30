using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class EpisodeSelectionController : MonoBehaviour
    {
        [SerializeField] private Episode m_Episode;
        [SerializeField] private TextMeshProUGUI m_EpisodeNickname;
        [SerializeField] private Image m_PreviewImage;

        public void OnStartEpisodeButtonClicked()
        { 
            LevelSequenceController.Instance.StartEpisode(m_Episode);
        }
    }
}

