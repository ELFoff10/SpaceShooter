using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// ������������ ������ �� �����. �� ��� ����� ����� ��� ������.
    /// </summary>
    public class Destructible : Entity
    {
        [SerializeField] private ImpactEffect m_ExplosionPrefab;

        [SerializeField] private bool m_EffectAvailable;

        #region Properties
        /// <summary>
        /// ������ ����������� �����������.
        /// </summary>
        [SerializeField] protected bool m_Indestructible;
        public bool Indestructible => m_Indestructible;

        /// <summary>
        /// ��������� ���-�� ������.
        /// </summary>
        [SerializeField] private int m_HitPoints;
        public int HitPoints => m_HitPoints;

        /// <summary>
        /// ������� ���-�� ������.
        /// </summary>
        private int m_CurrentHitPoints;
        public int CurrentHitPoints => m_CurrentHitPoints;

        private static List<Destructible> m_AllDestructibles;
        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;
        #endregion

        #region Unity Events
        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints;
        }

        protected virtual void OnEnable()
        {
            if (m_AllDestructibles == null)
            {
                m_AllDestructibles = new List<Destructible>();
            }

            m_AllDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructibles.Remove(this);
        }
        #endregion

        #region Public API
        /// <summary>
        /// ���������� ����� � �������.
        /// </summary>
        /// <param name="damage">���� ��������� �������</param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return;

            m_CurrentHitPoints -= damage;

            if (m_CurrentHitPoints <= 0)
            {
                OnDeath();
            }
        }
        #endregion

        /// <summary>
        /// ���������������� ������� ����������� �������, ����� ����� ������� ���� ����.
        /// </summary>
        protected virtual void OnDeath()
        {
            Destroy(gameObject);

            m_EventOnDeath?.Invoke();

            if (m_EffectAvailable == true)
            {
                Instantiate(m_ExplosionPrefab, transform.position, Quaternion.identity);
            }            
        }

        public const int TeamIdNeutral = 0;

        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;

        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        #region Score

        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;

        #endregion
    }
}
