using UnityEngine;

namespace SpaceShooter
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        [SerializeField] private TurretProperties m_TurretProperties;

        private float m_RefireTimer;
        public bool CanFire => m_RefireTimer <= 0;

        [SerializeField] private SpaceShip m_Ship;

        private void Update()
        {
            if (m_RefireTimer > 0)
                m_RefireTimer -= Time.deltaTime;
        }

        public void Fire()
        {
            if (m_TurretProperties == null) return;

            if (m_RefireTimer > 0) return; // НЕ ПОНЯЛ! А ТАЙМЕР В МИНУС УХОДИТ И ВСЁ?

            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>();
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;

            m_RefireTimer = m_TurretProperties.RateOfFire;

            // SFX нужно добавить [Ser] AudioSoure и тут создать клип из m_TurretProperties.AidoiSource.Play
        }

        public void AssignLoadout(TurretProperties properties) // Назначили другие свойства, для бонуса
        {
            // Подменили спритблобжект(где по другому стреляет) на время, а потом опять поставили назад
            if (m_Mode != properties.Mode) return; // нельзя ставить основное оружие во вторичное?

            m_RefireTimer = 0; // Можно сразу стрелять

            m_TurretProperties = properties;
        }
    }
}