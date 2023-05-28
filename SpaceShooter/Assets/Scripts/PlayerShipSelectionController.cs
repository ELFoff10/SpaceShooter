using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class PlayerShipSelectionController : MonoBehaviour
    {
        [SerializeField] private SpaceShip m_Prefab;

        [SerializeField] private TextMeshProUGUI m_Shipname;
        [SerializeField] private TextMeshProUGUI m_Hitpoints;
        [SerializeField] private TextMeshProUGUI m_Speed;
        [SerializeField] private TextMeshProUGUI m_Agility;

        [SerializeField] private Transform m_Target;
        
        [SerializeField] private Image m_Preview;

        private void Start()
        {
            if (m_Prefab != null)
            {
                m_Shipname.text = m_Prefab.NickName;
                m_Hitpoints.text = "Hit points: " + m_Prefab.HitPoints.ToString();
                m_Speed.text = "Speed: " + m_Prefab.Thrust.ToString();
                m_Agility.text = "Agility: " + m_Prefab.Mobility.ToString();
                m_Preview.sprite = m_Prefab.PreviewImage;
            }
        }

        public void OnSelectShip()
        {
            LevelSequenceController.PlayerShip = m_Prefab;

            //Instantiate(m_Prefab.PreviewImage, m_Target);


            //StartCoroutine(Coroutine());
        }

        //IEnumerator Coroutine()
        //{
        //    yield return new WaitForSeconds(3.0f);
        //    MainMenuController.Instance.gameObject.SetActive(true);
        //}
    }
}