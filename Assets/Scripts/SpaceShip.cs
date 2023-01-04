using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        [SerializeField] private ParticleSystem m_ParticleSystemforward;
        [SerializeField] private ParticleSystem m_ParticleSystemforward_1;
        [SerializeField] private ParticleSystem m_ParticleSystemBack;
        [SerializeField] private ParticleSystem m_ParticleSystemBack_1;
        //[SerializeField] private ParticleSystem m_ParticleSystemLeft;
        //[SerializeField] private ParticleSystem m_ParticleSystemLeft_1;
        //[SerializeField] private ParticleSystem m_ParticleSystemRight;
        //[SerializeField] private ParticleSystem m_ParticleSystemRight_1;

        /// <summary>
        /// Масса для автоматической установки у ригида.
        /// </summary>
        [Header("Space ship")]
        [SerializeField] private float m_Mass;

        /// <summary>
        /// Толкающая вперед сила.
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// Вращающая сила.
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// Максимальная линейная скорость(Ограничитель скорости).
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;

        /// <summary>
        /// Максимальная вращательная скорость(Ограничитель вращения). В градусах/сек.
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;

        /// <summary>
        /// Сохраненная ссылка на ригид.
        /// </summary>
        private Rigidbody2D m_Rigidbody2D;

        #region Public API

        /// <summary>
        /// Управление линейной тягой. от -1.0 до +1.0
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Управление вращательной тягой. от -1.0 до +1.0
        /// </summary>
        public float TorqueControl { get; set; }

        #endregion

        #region Unity Event
        protected override void Start()
        {
            base.Start();

            m_Rigidbody2D = GetComponent<Rigidbody2D>();

            m_Rigidbody2D.mass = m_Mass;

            m_Rigidbody2D.inertia = 1;
        }
        private void FixedUpdate()
        {
            UpdateRigidBody();

            if (ThrustControl == 1)
            {
                m_ParticleSystemforward.Play();
                m_ParticleSystemforward_1.Play();
            }

            if (ThrustControl == -1)
            {
                m_ParticleSystemBack.Play();
                m_ParticleSystemBack_1.Play();
            }

            //if (TorqueControl == 1)
            //{
            //    m_ParticleSystemLeft.Play();
            //    m_ParticleSystemLeft_1.Play();
            //}

            //if (TorqueControl == -1)
            //{
            //    m_ParticleSystemRight.Play();
            //    m_ParticleSystemRight_1.Play();

            //}
        }

        #endregion

        /// <summary>
        /// Метод добавляения сил кораблю для движения.
        /// </summary>
        private void UpdateRigidBody()
        {
            m_Rigidbody2D.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigidbody2D.AddForce(-m_Rigidbody2D.velocity * (m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigidbody2D.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigidbody2D.AddTorque(-m_Rigidbody2D.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }
    }
}

