using UnityEngine;

namespace SpaceShooter
{
    public class CircularRotation : MonoBehaviour
    {
        [SerializeField] private Transform m_Center;
        [SerializeField] private float m_Radius, m_AngularSpeed, m_Angle;

        private float m_PositionX, m_PositionY;

        private void Update()
        {
            m_PositionX = m_Center.position.x + Mathf.Cos(m_Angle) * m_Radius;
            m_PositionY = m_Center.position.y + Mathf.Sin(m_Angle) * m_Radius;
            transform.position = new Vector2(m_PositionX, m_PositionY);
            m_Angle = m_Angle + m_AngularSpeed * Time.deltaTime;

            if (m_Angle > 360f)
            {
                m_Angle = 0f;
            }
        }
    }
}

