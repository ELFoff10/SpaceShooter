using UnityEngine;

namespace SpaceShooter
{
    public class Appearance : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_SpriteRenderer;

        [SerializeField] private float m_Time;

        private float m_Timer;

        private void Update()
        {
            m_Timer += Time.deltaTime / m_Time;

            if ((int)m_Timer % 2 == 0)
            {
                m_SpriteRenderer.GetComponent<SpriteRenderer>().enabled = false;
                m_SpriteRenderer.GetComponent<BoxCollider2D>().enabled = false;

            }
            if ((int)m_Timer % 2 == 1)
            {
                m_SpriteRenderer.GetComponent<SpriteRenderer>().enabled = true;
                m_SpriteRenderer.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
