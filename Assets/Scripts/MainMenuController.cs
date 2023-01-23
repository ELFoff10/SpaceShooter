using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject m_EpisodeSelection;

        public void OnButtonStartNew()
        {
            m_EpisodeSelection.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }

        public void OnButtonExit() 
        {
            Application.Quit();
        }
    }
}

