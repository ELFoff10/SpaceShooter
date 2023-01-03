using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace SpaceShooter
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int m_NumLives; // ���-�� ������

        [SerializeField] private SpaceShip m_Ship; // �� �����

        [SerializeField] private GameObject m_PlayerShipPrefab; // ������

        [SerializeField] private GameObject m_ShipExplosionPrefab;

        [SerializeField] private CameraController m_CameraController;

        [SerializeField] private MovementController m_MovementController;

        private void Start()
        {
            m_Ship.EventOnDeath.AddListener(OnShipDeath); // ��� ������ EventOnDeath ���������� � Destructible
        }
        private void OnShipDeath()
        {
            var explosion = Instantiate(m_ShipExplosionPrefab, m_Ship.transform.position, Quaternion.identity);

            Destroy(explosion, 3.0f);

            m_NumLives--;            

            if (m_NumLives > 0)
            {
                Invoke("Respawn", 3);
            }
        }
        private void Respawn()
        {
            var newPlayerShip = Instantiate(m_PlayerShipPrefab);

            m_Ship = newPlayerShip.GetComponent<SpaceShip>();

            m_CameraController.SetTarget(m_Ship.transform);

            m_MovementController.SetTargetShip(m_Ship);
        }
    }
}

