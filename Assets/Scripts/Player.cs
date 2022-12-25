using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int m_NumLives; // Кол-во жизней

        [SerializeField] private SpaceShip m_Ship; // На сцене

        [SerializeField] private GameObject m_PlayerShipPrefab; // Префаб

        [SerializeField] private CameraController m_CameraController;

        [SerializeField] private MovementController m_MovementController;

        private void Start()
        {
            m_Ship.EventOnDeath.AddListener(OnShipDeath); // При смерти EventOnDeath вызывается в Destructible
        }
        private void OnShipDeath()
        {
            m_NumLives--;

            if (m_NumLives > 0)
            {
                Respawn();
            }
        }
        private void Respawn()
        {
            var newPlayerShip = Instantiate(m_PlayerShipPrefab);

            m_Ship = newPlayerShip.GetComponent<SpaceShip>();

            m_CameraController.SetTarget(m_Ship.transform);

            m_MovementController.SetTargetShip(m_Ship);

            Debug.Log(m_NumLives);
        }
    }
}

