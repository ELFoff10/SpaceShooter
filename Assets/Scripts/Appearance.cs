using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appearance : MonoBehaviour
{
    [SerializeField] private float m_TimeBeforeShutdown, m_TimeAfterShutdown;

    [SerializeField] private SpriteRenderer m_SpriteRenderer;

    private bool m_Running = true;

    private void Update()
    {
        var tbs = m_TimeBeforeShutdown;
        var tas = m_TimeAfterShutdown;

        m_TimeBeforeShutdown -= Time.deltaTime;

        if (m_TimeBeforeShutdown <= 0)
        {
            m_Running = false;
            m_TimeBeforeShutdown = tbs;
        }

        if (m_Running)
        {
            m_SpriteRenderer.GetComponent<SpriteRenderer>().enabled = true;
            m_SpriteRenderer.GetComponent<BoxCollider2D>().enabled = true;
            m_TimeBeforeShutdown = tbs;
        }


        if (m_Running == false)
        {
            m_SpriteRenderer.GetComponent<SpriteRenderer>().enabled = false;
            m_SpriteRenderer.GetComponent<BoxCollider2D>().enabled = false;

        }
    }
}
