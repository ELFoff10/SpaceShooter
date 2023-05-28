using UnityEngine;

namespace SpaceShooter
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        [SerializeField] private TurretProperties m_TurretProperties;

        [SerializeField] private SpaceShip m_Ship;

        private float m_RefireTimer; // Задаётся значение в конце метода TurretFire() из m_TurretProperties.RateOfFire

        public bool CanFire => m_RefireTimer <= 0;

        private void Update()
        {
            if (m_RefireTimer > 0)
                m_RefireTimer -= Time.deltaTime;
        }

        public void TurretFire()
        {
            if (m_TurretProperties == null) return; // Если это класс, то нужно всегда проверять на null
 
            if (m_RefireTimer > 0) return; // Промежуток между спавном снарядов 
            // Изначально 0, поэтому идём дальше и спавним, а потом когда значение RateOfFire = 0.2, ждём в Update до 0

            if (m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false) // Если метод не DrawEnergy не сработал, то мы return
                return;

            if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false)
                return;

            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab);
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;

            projectile.SetParentShooter(m_Ship);

            m_RefireTimer = m_TurretProperties.RateOfFire;

            // SFX нужно добавить [Ser] AudioSoure и тут создать клип из m_TurretProperties.AidoiSource.Play
        }

        // 
        public void WeaponReplacement(TurretProperties properties) // Назначили другие свойства, для бонуса
        {
            // Подменили спритблобжект(где по другому стреляет) на время, а потом опять поставили назад
            if (m_Mode != properties.Mode) return; // Мы проверяем наш Мод, если он равен, то мы меняем, иначе return

            m_RefireTimer = 0; // Можно сразу стрелять

            m_TurretProperties = properties;
        }
    }
}