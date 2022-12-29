using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    [SerializeField] private Transform[] m_Planets;

    [Header("Rotate speed")]
    [Space(10)]
    [SerializeField] private float[] m_Speed;

    [Header("The point around which rotate")]
    [Space(10)]
    [SerializeField] private Transform[] m_Center;
    [SerializeField] private float[] m_Radius;
    [SerializeField] private float[] m_AngularSpeed;
    [SerializeField] private float[] m_Angle;
    [SerializeField] private float[] m_PosX;
    [SerializeField] private float[] m_PosY;

    private void Update()
    {
        m_Planets[0].rotation *= Quaternion.Euler(0f, 0f, m_Speed[0] * Time.deltaTime);
        m_Planets[1].rotation *= Quaternion.Euler(0f, 0f, m_Speed[1] * Time.deltaTime);
        m_Planets[2].rotation *= Quaternion.Euler(0f, 0f, m_Speed[2] * Time.deltaTime);
        m_Planets[3].rotation *= Quaternion.Euler(0f, 0f, m_Speed[3] * Time.deltaTime);
        m_Planets[4].rotation *= Quaternion.Euler(0f, 0f, m_Speed[4] * Time.deltaTime);
        m_Planets[5].rotation *= Quaternion.Euler(0f, 0f, m_Speed[5] * Time.deltaTime);

        for (int i = 0; i < m_Center.Length; i++)
        {
            m_PosX[i] = m_Center[i].position.x + Mathf.Cos(m_Angle[i]) * m_Radius[i];
            m_PosY[i] = m_Center[i].position.y + Mathf.Sin(m_Angle[i]) * m_Radius[i];
            transform.position = new Vector2(m_PosX[i], m_PosY[i]);
            m_Angle[i] = m_Angle[i] + m_AngularSpeed[i] * Time.deltaTime;

            if (m_Angle[i] > 360f)
            {
                m_Angle[i] = 0f;
            }
        }        
    }
}