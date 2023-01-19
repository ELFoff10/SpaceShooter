using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null, Patrol
        }

        [SerializeField] private AIBehaviour m_AIBehaviour;

        [SerializeField] private AIPointPatrol m_PointPatrol;

        [Range(0f, 1f)]
        [SerializeField] private float m_NavigationLinear, m_NavigationAngular; // �������� ����������� � ��������

        [SerializeField] private float m_TimeRandomMovePoint, m_TimeFindNewTarget, m_TimeShootDelay;

        [SerializeField] private float m_RaycastLength; // ����� ��������

        private SpaceShip m_SpaceShip;

        private Vector3 m_MoveTargetPosition; // ����� ����������, ���� ��������� ��� �������

        private Destructible m_SelectedTarget; // ��������� ����

        private Timer m_RandomizeDirectionTimer, m_FiteTimer, m_FindNewTargetTimer;

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
                    m_MoveTargetPosition = m_SelectedTarget.transform.position;
                }
                else
                {
                    if (m_PointPatrol != null)
                    {
                        bool isInsidePatrolZone = (m_PointPatrol.transform.position - transform.position).sqrMagnitude
                                                   < m_PointPatrol.Radius * m_PointPatrol.Radius;
                        if (isInsidePatrolZone == true)
                        {
                            if (m_RandomizeDirectionTimer.IsFinished == true)
                            {
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere 
                                                   * m_PointPatrol.Radius + m_PointPatrol.transform.position;

                                m_MoveTargetPosition = newPoint;

                                m_RandomizeDirectionTimer.StartTime(m_TimeRandomMovePoint);
                            }
                        }
                        else
                        {
                            m_MoveTargetPosition = m_PointPatrol.transform.position;
                        }
                    }
                }
            }
        }

        private void ActionEvadeCollision()
        {
            if (Physics2D.Raycast(transform.position, transform.up, m_RaycastLength) == true)
            {
                m_MoveTargetPosition = transform.position + transform.right * 100.0f;
            }
        }

        private void ActionControlShip()
        {
            m_SpaceShip.ThrustControl = m_NavigationLinear;

            // ��� ��� ����� ��������, �� ��� ����� 2 ��������� int - ��� 0 � 1,
            // � ��� m_NavigationAngular ����� �� 0 �� 1
            m_SpaceShip.TorqueControl = ComputeTorqueNormalized(m_MoveTargetPosition, m_SpaceShip.transform) 
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
            if (m_FindNewTargetTimer.IsFinished == true)
            {
                m_SelectedTarget = FindNearestDestructibleTarget();

                m_FindNewTargetTimer.StartTime(m_TimeShootDelay);
            }
        }

        private void ActionFire()
        {
            if (m_SelectedTarget != null)
            {
                if (m_FiteTimer.IsFinished == true)
                {
                    m_SpaceShip.ShipFire(TurretMode.Primary);

                    m_FiteTimer.StartTime(m_TimeShootDelay);
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
            m_RandomizeDirectionTimer = new Timer(m_TimeRandomMovePoint);

            m_FiteTimer = new Timer(m_TimeShootDelay);

            m_FindNewTargetTimer = new Timer(m_TimeFindNewTarget);
        }
        private void UpdateTimers()
        {
            m_RandomizeDirectionTimer.RemoveTime(Time.deltaTime);

            m_FiteTimer.RemoveTime(Time.deltaTime);

            m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
        }

        public void SetPatrolBehaviour(AIPointPatrol point) // 
        {
            m_AIBehaviour = AIBehaviour.Patrol;

            m_PointPatrol = point;
        }

        #endregion
    }
}

