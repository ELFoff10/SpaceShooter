using UnityEngine;

namespace SpaceShooter
{
    public class MoveManager : MonoBehaviour
    {
        [SerializeField] private Transform[] m_Planets;

        [Header("Rotate speed")]
        [Space(10)]
        [SerializeField] private float[] m_Speed;

        [SerializeField] private Transform[] m_Sputniks;

        [Header("The point around which rotate")]
        [Space(10)]
        [SerializeField] private Transform[] m_Center;
        [SerializeField] private float[] m_Radius, m_AngularSpeed, m_Angle;

        private float m_PositionX, m_PositionY;

        private void Update()
        {
            m_Planets[0].rotation *= Quaternion.Euler(0f, 0f, m_Speed[0] * Time.deltaTime);
            m_Planets[1].rotation *= Quaternion.Euler(0f, 0f, m_Speed[1] * Time.deltaTime);

            if (m_Center != null)
            {
                for (int i = 0; i < m_Center.Length; i++)
                {
                    m_PositionX = m_Center[i].transform.position.x + Mathf.Cos(m_Angle[i]) * m_Radius[i];
                    m_PositionY = m_Center[i].transform.position.y + Mathf.Sin(m_Angle[i]) * m_Radius[i];
                    m_Sputniks[i].transform.position = new Vector2(m_PositionX, m_PositionY);
                    m_Angle[i] = m_Angle[i] + Time.deltaTime * m_AngularSpeed[i];

                    if (m_Angle[i] > 360f)
                    {
                        m_Angle[i] = 0f;
                    }
                }
            }            
        }
    }
}
