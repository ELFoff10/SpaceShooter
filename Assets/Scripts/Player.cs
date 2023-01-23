using UnityEngine;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {
        [SerializeField] private int m_NumLives; // Кол-во жизней

        [SerializeField] private SpaceShip m_Ship; // На сцене
        public SpaceShip ActiveShip => m_Ship;

        [SerializeField] private GameObject m_PlayerShipPrefab, m_ShipExplosionPrefab; 

        [SerializeField] private CameraController m_CameraController;

        [SerializeField] private MovementController m_MovementController;

        private void Start()
        {
            m_Ship.EventOnDeath.AddListener(OnShipDeath); // При смерти EventOnDeath вызывается в Destructible
        }
        private void OnShipDeath()
        {
            var explosion = Instantiate(m_ShipExplosionPrefab, m_Ship.transform.position, Quaternion.identity);

            Destroy(explosion, 4.0f);

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

            m_Ship.EventOnDeath.AddListener(OnShipDeath);

            m_CameraController.SetTarget(m_Ship.transform);

            m_MovementController.SetTargetShip(m_Ship);

            m_Ship.EventOnDeath.AddListener(OnShipDeath);
        }

        #region Score

        public int Score { get; private set; }
        public int NumKills { get; private set; }

        public void AddKill()
        {
            NumKills++;
        }

        public void AddScore(int num)
        {
            Score += num;
        }

        #endregion
    }
}

