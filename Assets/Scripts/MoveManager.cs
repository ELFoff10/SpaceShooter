using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    [SerializeField] private Transform m_Earth;
    [SerializeField] private Transform m_Mars;
    [SerializeField] private Transform m_BlackHole;
    [SerializeField] private Transform m_BlackHoleTwo;
    [SerializeField] private float m_SpeedRotateEarth;
    [SerializeField] private float m_SpeedRotateMars;
    [SerializeField] private float m_SpeedRotateBlackHole;
    [SerializeField] private float m_SpeedRotateBlackHoleTwo;

    private void Update()
    {
        m_Earth.rotation *= Quaternion.Euler(0f, 0f, m_SpeedRotateEarth * Time.deltaTime);
        m_Mars.rotation *= Quaternion.Euler(0f, 0f, m_SpeedRotateMars * Time.deltaTime);
        m_BlackHole.rotation *= Quaternion.Euler(0f, 0f, m_SpeedRotateBlackHole * Time.deltaTime);
        m_BlackHoleTwo.rotation *= Quaternion.Euler(0f, 0f, m_SpeedRotateBlackHoleTwo * Time.deltaTime);
    }
}