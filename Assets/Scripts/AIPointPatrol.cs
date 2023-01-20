using UnityEngine;

namespace SpaceShooter
{
    public class AIPointPatrol : MonoBehaviour
    {
        [SerializeField] private float m_PatrolRadius;
        public float PatrolRadius => m_PatrolRadius;

        private static readonly Color GizmoColor = new Color(1, 0, 0, 0.3f);

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = GizmoColor;
            Gizmos.DrawSphere(transform.position, m_PatrolRadius);
        }
#endif
    }
}

