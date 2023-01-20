using System;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null, Patrol //Points, EvadeCollision
        }

        [SerializeField] private AIBehaviour m_AIBehaviour;

        [SerializeField] private AIPointPatrol m_AIPointPatrol;

        [Range(0f, 1f)]
        [SerializeField] private float m_NavigationLinear, m_NavigationAngular; // �������� ����������� � ��������

        [SerializeField] private float m_TimeRandomMovePointPatrol, m_TimeFindNewTarget, m_TimeShootDelay;

        [SerializeField] private float m_RaycastLength; // ����� ��������

        [SerializeField] private bool m_IsControlledPatrol;

        [SerializeField] private Transform m_Target_1, m_Target_2, m_Target_3, m_Target_4;

        private bool m_ReachedTheTarget_1 = false;
        private bool m_ReachedTheTarget_2 = false;
        private bool m_ReachedTheTarget_3 = false;


        private Destructible m_SelectedTarget; // ��������� ����

        private SpaceShip m_SpaceShip;

        private Vector3 m_MoveToTargetPosition; // ����� ����������, ���� ��������� ��� �������

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
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                UpdateBehaviourPatrol();
            }
        }

        private void UpdateBehaviourPatrol()
        {
            ActionFindNewPosition();
            ActionControlShip();
            ActionFindNewAttackTarget();
            ActionFire();
            ActionEvadeCollision();
        }


        // ����������� �������������� �� ��������� ��������.
        // ����� ������� ������� ����� ��������������, � ����� ������� ������� � ���������,
        // �� ���������� ����� �������������� �� ���������, ��������, � ������ ActionFindNewMovePosition.
        private void ActionFindNewPosition()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                if (m_SelectedTarget != null)
                {
                    m_MoveToTargetPosition = m_SelectedTarget.transform.position;
                }
                else
                {
                    if (m_IsControlledPatrol == true)
                    {
                        if (m_ReachedTheTarget_1 == false)
                        {
                            m_MoveToTargetPosition = m_Target_1.transform.position;
                        }
                        
                        if (transform.position == m_Target_1.transform.position)
                        {
                            m_ReachedTheTarget_1 = true;            
                        }

                        if (m_ReachedTheTarget_1 == true)
                        {
                            m_MoveToTargetPosition = m_Target_2.transform.position;
                        }

                        if (transform.position == m_Target_2.transform.position)
                        {
                            m_ReachedTheTarget_2 = true;
                        }

                        if (m_ReachedTheTarget_2 == true)
                        {
                            m_MoveToTargetPosition = m_Target_3.transform.position;
                        }

                        if (transform.position == m_Target_3.transform.position)
                        {
                            m_ReachedTheTarget_3 = true;
                        }

                        if (m_ReachedTheTarget_3 == true)
                        {
                            m_MoveToTargetPosition = m_Target_4.transform.position;
                        }

                        if (transform.position == m_Target_4.transform.position)
                        {
                            m_AIBehaviour = AIBehaviour.Null;
                        }
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

            // ��� ��� ����� ��������, �� ��� ����� 2 ��������� int - ��� 0 � 1,
            // � ��� m_NavigationAngular ����� �� 0 �� 1
            m_SpaceShip.TorqueControl = ComputeTorqueNormalized(m_MoveToTargetPosition, m_SpaceShip.transform)
                                                                * m_NavigationAngular;
        }

        private const float MAX_ANGLE = 45.0f;

        /// <summary>
        /// ��������� ��������������� ���� �� �������AI, �� targetPosition.
        /// </summary>
        /// <param name="targetPosition"></param>
        /// <param name="ship"></param>
        /// <returns></returns>
        private static float ComputeTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            // ������ ������ �������� �� �������
            // ��������� ������� �� ������� � ��������� ����������
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            // �������� ���� ����� 2 ���������
            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            //���� �� ���� ��� ���� > 45 ��������, �� �� ��������� ������ ���� ��� ��������
            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE; // ������������ ��� ����

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
                    m_SpaceShip.ShipFire(TurretMode.Primary);

                    m_TimerFire.StartTime(m_TimeShootDelay);
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

        private void InitTimers() // ��������������
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

        public void SetPatrolBehaviour(AIPointPatrol point) // 
        {
            m_AIBehaviour = AIBehaviour.Patrol;

            m_AIPointPatrol = point;
        }

        #endregion
    }
}

