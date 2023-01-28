using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public interface ILevelCondition
    {
        bool IsCompleted { get; }
    }

    public class LevelController : SingletonBase<LevelController>
    {
        [SerializeField] private int m_ReferenceTime;
        public int ReferenceTime => m_ReferenceTime;

        [SerializeField] private UnityEvent m_EventLevelCompleted;

        private PlayerStatistics m_PlayerStatistics;

        private ILevelCondition[] m_Conditions;

        private bool m_IsLevelCompleted;

        private float m_LevelTime;
        public float LevelTime => m_LevelTime;

        private void Start()
        {
            m_Conditions = GetComponentsInChildren<ILevelCondition>();
        }

        private void Update()
        {
            if (!m_IsLevelCompleted)
            {
                m_LevelTime += Time.deltaTime;

                ChecckLevelConditions();
            }
        }

        private void ChecckLevelConditions()
        {
            if (m_Conditions == null || m_Conditions.Length == 0)
                return;

            int numCompleted = 0;

            foreach (var v in m_Conditions)
            {
                if (v.IsCompleted)
                {
                    numCompleted++;
                }
            }

            if (numCompleted == m_Conditions.Length)
            {
                m_IsLevelCompleted = true;

                m_EventLevelCompleted?.Invoke();

                LevelSequenceController.Instance?.FinishCurrentLevel(true);
            }
        }

        public void AddBonusScore()
        {
            if (m_LevelTime >= 60)
            {
                m_PlayerStatistics.m_BonusScore += 50;
            }

            if (m_LevelTime >= 45 || m_LevelTime < 60)
            {
                m_PlayerStatistics.m_BonusScore += 100;
            }

            if (m_LevelTime >= 30 || m_LevelTime < 45)
            {
                m_PlayerStatistics.m_BonusScore += 150;
            }

            if (m_LevelTime >= 15 || m_LevelTime < 30)
            {
                m_PlayerStatistics.m_BonusScore += 200;
            }

            if (m_LevelTime >= 1 || m_LevelTime < 15)
            {
                m_PlayerStatistics.m_BonusScore += 250;
            }
        }
    }
}

