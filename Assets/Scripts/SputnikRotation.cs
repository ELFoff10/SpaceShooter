using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SpaceShooter
{
    public class SputnikRotation : MonoBehaviour
    {
        [SerializeField] private Transform m_Center;
        [SerializeField] private float m_Radius = 2f, m_AngularSpeed = 2f, m_Angle = 0f;

        float m_PosX, m_PosY;

        private void Update()
        {
            m_PosX = m_Center.position.x + Mathf.Cos(m_Angle) * m_Radius;
            m_PosY = m_Center.position.y + Mathf.Sin(m_Angle) * m_Radius;
            transform.position = new Vector2(m_PosX, m_PosY);
            m_Angle = m_Angle + m_AngularSpeed * Time.deltaTime;

            if (m_Angle > 360f)
            {
                m_Angle = 0f;
            }
        }
    }
}

