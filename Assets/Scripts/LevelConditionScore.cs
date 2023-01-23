using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionScore : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private int m_Score;

        private bool m_Reached;

        bool ILevelCondition.IsCompleted 
        { 
            get 
            {
                if (Player.Instance != null && Player.Instance.ActiveShip != null)
                {
                    if (Player.Instance.Score >= m_Score)
                    {
                        m_Reached = true;
                        Debug.Log("Reached!");
                    }
                }
                return m_Reached;
            } 
        }
    }
}

