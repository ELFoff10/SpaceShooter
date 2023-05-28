using UnityEngine;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {
        [SerializeField] private int m_NumLives; 

        [SerializeField] private SpaceShip m_Ship;
        public SpaceShip ActiveShip => m_Ship;

        [SerializeField] private CameraController m_CameraController;

        [SerializeField] private MovementController m_MovementController;

        protected override void Awake()
        {
            base.Awake();

            if (m_Ship != null)
            {
                Destroy(m_Ship.gameObject);
            }
        }

        private void Start()
        {
            Respawn();
            /*m_Ship.EventOnDeath.AddListener(OnShipDeath); */// При смерти EventOnDeath вызывается в Destructible
        }
        private void OnShipDeath()
        {
            //var explosion = Instantiate(m_ShipExplosionPrefab, m_Ship.transform.position, Quaternion.identity);

            //Destroy(explosion, 4.0f);

            m_NumLives--; 

            if (m_NumLives > 0)
            {
                Respawn();
            }
            else
            {
                LevelSequenceController.Instance.FinishCurrentLevel(false); // Если закончились жизни, то выход в меню
            }
        }

        private void Respawn()
        {
            if (LevelSequenceController.PlayerShip != null) // Установлен ли корабль в LevelSequenceController
            {
                var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);

                m_Ship = newPlayerShip.GetComponent<SpaceShip>();

                m_CameraController.SetTarget(m_Ship.transform);

                m_MovementController.SetTargetShip(m_Ship);

                m_Ship.EventOnDeath.AddListener(OnShipDeath);
            }
        }

        #region Score

        public int m_PlayerScore { get; set; }
        public int m_PlayerNumKills { get; set; }

        public void AddKill()
        {
            m_PlayerNumKills++;
        }

        public void AddScore(int num)
        {
            m_PlayerScore += num;
        }

        #endregion
    }
}

