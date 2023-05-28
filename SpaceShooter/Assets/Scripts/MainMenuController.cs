using UnityEngine;

namespace SpaceShooter
{
    public class MainMenuController : SingletonBase<MainMenuController>
    {
        [SerializeField] private SpaceShip m_DefaultSpaceShip; 

        [SerializeField] private GameObject m_EpisodeSelectPanel;
        [SerializeField] private GameObject m_ShipSelectionPanel;
        [SerializeField] private GameObject m_GeneralGameStatistics;

        private void Start()
        {
            LevelSequenceController.PlayerShip = m_DefaultSpaceShip; 
        }

        public void OnButtonStartNew()
        {
            m_EpisodeSelectPanel.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }

        public void OnSelectShip()
        { 
            m_ShipSelectionPanel.SetActive(true);

            gameObject.SetActive(false);
        }

        public void OnSelectStatistics()
        {
            m_GeneralGameStatistics.SetActive(true);

            gameObject.SetActive(false);
        }

        public void OnButtonExit() 
        {
            Application.Quit();
        }
    }
}

