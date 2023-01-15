using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerupStats : Powerup
    {
        public enum EffectType
        {
            AddAmmo, AddEnergy, AddSpeed, AddIndestructible
        }

        [SerializeField] private EffectType m_EffectType;

        [SerializeField] private float m_Value;

        protected override void OnPickedUp(SpaceShip ship)
        {
            if (m_EffectType == EffectType.AddEnergy)
            {
                ship.AddEnergy((int) m_Value);
            }

            if (m_EffectType == EffectType.AddAmmo)
            {
                ship.AddAmmo((int) m_Value);
            }

            if (m_EffectType == EffectType.AddSpeed)
            {
                ship.AddSpeed((int)m_Value);
            }

            if (m_EffectType == EffectType.AddIndestructible)
            {
                ship.AddIndestructible();
            }
        }
    }
}

