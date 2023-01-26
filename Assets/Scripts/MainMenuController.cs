using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class MainMenuController : SingletonBase<MainMenuController>
    {
        [SerializeField] private SpaceShip m_DefaultSpaceShip; // переместить нужно, нет логики тут

        [SerializeField] private GameObject m_EpisodeSelection;

        [SerializeField] private GameObject m_ShipSelection; // окно выбора корабля

        private void Start()
        {
            LevelSequenceController.PlayerShip = m_DefaultSpaceShip; // На случай если он не будет установлен
        }

        public void OnButtonStartNew()
        {
            m_EpisodeSelection.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }

        public void OnSelectShip()
        { 
            m_ShipSelection.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnButtonExit() 
        {
            Application.Quit();
        }
    }
}

