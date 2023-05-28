using System;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null, Patrol, Points
        }

        [SerializeField] private AIBehaviour m_AIBehaviour;
        [SerializeField] private AIPointPatrol m_AIPointPatrol;
        [SerializeField] public GameObject[] m_PointsToFly;

        [Range(0f, 1f)]
        [SerializeField] private float m_NavigationLinear, m_NavigationAngular;

        [SerializeField] private float m_TimeRandomMovePointPatrol, m_TimeFindNewTarget, m_TimeShootDelay;
        [SerializeField] private float m_RaycastLength;

        public int m_FlyingPoints = 1;
        private float m_Coefficient = 8;

        private Destructible m_SelectedTarget;
        private SpaceShip m_SpaceShip;
        private Vector3 m_MoveToTargetPosition;
        private Timer m_TimerRandomizeDirection, m_TimerFire, m_TimerFindNewTarget;

        private void Start()
        {
            m_SpaceShip = GetComponent<SpaceShip>();

            InitTimers();
        }

        private void Update()
        {
            UpdateTimers();

            UpdateAI();
        }

        private void UpdateAI()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol || m_AIBehaviour == AIBehaviour.Points)
            {
                UpdateBehaviourPatrol();
            }
        }

        private void UpdateBehaviourPatrol()
        {
            if (m_AIBehaviour == AIBehaviour.Points)
            {
                ActionFindNewPosition();
                ActionControlShip();
            }
            else
            {
                ActionFindNewPosition();
                ActionControlShip();
                ActionFindNewAttackTarget();
                ActionFire();
                ActionEvadeCollision();
            }
        }

        private void ActionFindNewPosition()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                if (m_SelectedTarget != null)
                {
                    m_MoveToTargetPosition = m_SelectedTarget.transform.position
                    + (m_SelectedTarget.transform.up * (Player.Instance.ActiveShip.ThrustControl * m_Coefficient));
                }

                else
                {
                    if (m_AIPointPatrol != null)
                    {
                        bool isInsidePatrolZone = (m_AIPointPatrol.transform.position - transform.position).sqrMagnitude
                                                   < m_AIPointPatrol.PatrolRadius * m_AIPointPatrol.PatrolRadius;
                        if (isInsidePatrolZone == true)
                        {
                            if (m_TimerRandomizeDirection.IsFinished == true)
                            {
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere
                                                   * m_AIPointPatrol.PatrolRadius + m_AIPointPatrol.transform.position;

                                m_MoveToTargetPosition = newPoint;

                                m_TimerRandomizeDirection.StartTime(m_TimeRandomMovePointPatrol);
                            }
                        }
                        else
                        {
                            m_MoveToTargetPosition = m_AIPointPatrol.transform.position;
                        }
                    }
                }
            }

            if (m_AIBehaviour == AIBehaviour.Points)
            {
                if (m_FlyingPoints == m_PointsToFly.Length)
                {
                    m_FlyingPoints = 0;
                }

                m_MoveToTargetPosition = m_PointsToFly[m_FlyingPoints].transform.position;
            }
        }

        private void ActionEvadeCollision()
        {
            if (Physics2D.Raycast(transform.position, transform.up, m_RaycastLength) == true)
            {
                m_MoveToTargetPosition = transform.position + transform.right * 100.0f;
            }
        }

        private void ActionControlShip()
        {
            m_SpaceShip.ThrustControl = m_NavigationLinear;

            // Тут нам нужно вращение, но оно имеет 2 параметра int - это 0 и 1,
            // а наш m_NavigationAngular флоат от 0 до 1
            m_SpaceShip.TorqueControl = ComputeTorqueNormalized(m_MoveToTargetPosition, m_SpaceShip.transform)
                                                                * m_NavigationAngular;
        }

        private const float MAX_ANGLE = 45.0f;

        /// <summary>
        /// Вычисляем нормализованный угол от корабляAI, до targetPosition.
        /// </summary>
        /// <param name="targetPosition"></param>
        /// <param name="ship"></param>
        /// <returns></returns>
        private static float ComputeTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            // Считай делаем дочерним от корабля
            // Переводим позицию из мировой в локальные координаты
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            // Получаем угол между 2 векторами
            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            //Если до цели наш угол > 45 градусов, то мы применяем полную силу для вращения
            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE; // Ограничивает наш угол

            return -angle;
        }

        private void ActionFindNewAttackTarget()
        {
            if (m_TimerFindNewTarget.IsFinished == true)
            {
                m_SelectedTarget = FindNearestDestructibleTarget();

                m_TimerFindNewTarget.StartTime(m_TimeShootDelay);
            }
        }

        private void ActionFire()
        {
            if (m_SelectedTarget != null)
            {
                if (m_TimerFire.IsFinished == true)
                {
                    if (gameObject.transform.tag == "Secondary")
                    {
                        m_SpaceShip.ShipFire(TurretMode.Secondary);

                        m_TimerFire.StartTime(m_TimeShootDelay);
                    }
                    else
                    {
                        m_SpaceShip.ShipFire(TurretMode.Primary);

                        m_TimerFire.StartTime(m_TimeShootDelay);
                    }
                }
            }
        }
        private Destructible FindNearestDestructibleTarget()
        {
            float maxDist = float.MaxValue;

            Destructible potencialTarget = null;

            foreach (var v in Destructible.AllDestructibles)
            {
                if (v.GetComponent<SpaceShip>() == m_SpaceShip)
                    continue;

                if (v.TeamId == Destructible.TeamIdNeutral)
                    continue;

                if (v.TeamId == m_SpaceShip.TeamId)
                    continue;

                float dist = Vector2.Distance(m_SpaceShip.transform.position, v.transform.position);

                if (dist < maxDist)
                {
                    maxDist = dist;
                    potencialTarget = v;
                }
            }

            return potencialTarget;
        }

        #region Timers

        private void InitTimers()
        {
            m_TimerRandomizeDirection = new Timer(m_TimeRandomMovePointPatrol);

            m_TimerFire = new Timer(m_TimeShootDelay);

            m_TimerFindNewTarget = new Timer(m_TimeFindNewTarget);
        }
        private void UpdateTimers()
        {
            m_TimerRandomizeDirection.RemoveTime(Time.deltaTime);

            m_TimerFire.RemoveTime(Time.deltaTime);

            m_TimerFindNewTarget.RemoveTime(Time.deltaTime);
        }

        /// <summary>
        /// Метод, который настраивает точку патрулирования.
        /// </summary>
        /// <param name="point"></param>
        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBehaviour = AIBehaviour.Patrol;

            m_AIPointPatrol = point;
        }

        #endregion
    }
}

