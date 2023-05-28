using UnityEngine;

namespace SpaceShooter
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        [SerializeField] private TurretProperties m_TurretProperties;

        [SerializeField] private SpaceShip m_Ship;

        private float m_RefireTimer; // ������� �������� � ����� ������ TurretFire() �� m_TurretProperties.RateOfFire

        public bool CanFire => m_RefireTimer <= 0;

        private void Update()
        {
            if (m_RefireTimer > 0)
                m_RefireTimer -= Time.deltaTime;
        }

        public void TurretFire()
        {
            if (m_TurretProperties == null) return; // ���� ��� �����, �� ����� ������ ��������� �� null
 
            if (m_RefireTimer > 0) return; // ���������� ����� ������� �������� 
            // ���������� 0, ������� ��� ������ � �������, � ����� ����� �������� RateOfFire = 0.2, ��� � Update �� 0

            if (m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false) // ���� ����� �� DrawEnergy �� ��������, �� �� return
                return;

            if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false)
                return;

            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab);
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;

            projectile.SetParentShooter(m_Ship);

            m_RefireTimer = m_TurretProperties.RateOfFire;

            // SFX ����� �������� [Ser] AudioSoure � ��� ������� ���� �� m_TurretProperties.AidoiSource.Play
        }

        // 
        public void WeaponReplacement(TurretProperties properties) // ��������� ������ ��������, ��� ������
        {
            // ��������� �������������(��� �� ������� ��������) �� �����, � ����� ����� ��������� �����
            if (m_Mode != properties.Mode) return; // �� ��������� ��� ���, ���� �� �����, �� �� ������, ����� return

            m_RefireTimer = 0; // ����� ����� ��������

            m_TurretProperties = properties;
        }
    }
}