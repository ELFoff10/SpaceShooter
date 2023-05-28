using UnityEngine;

namespace SpaceShooter
{
    public class PowerupWeapon : Powerup
    {
        [SerializeField] private TurretProperties m_Properties;

        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.AddNewWeaponToShip(m_Properties);
        }
    }
}

