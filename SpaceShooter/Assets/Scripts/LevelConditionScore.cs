using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionScore : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private int m_ToConditionScore;

        private bool m_Reached;

        bool ILevelCondition.IsCompleted 
        { 
            get 
            {
                if (Player.Instance != null && Player.Instance.ActiveShip != null)
                {
                    if (Player.Instance.m_PlayerScore >= m_ToConditionScore)
                    {
                        m_Reached = true;
                    }
                }

                return m_Reached;
            } 
        }
    }
}

