using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class GravityWell : MonoBehaviour
    {
        [SerializeField] private float m_Force, m_Radius;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.attachedRigidbody == null) return; // Есть ли у коллизии с которой столкнулись Ригидбади

            Vector2 direction = transform.position - collision.transform.position; // Направление от колизии до трансформы

            float distance = direction.magnitude; // magnitude = значение расстояния нашего направления

            if (distance < m_Radius)
            {
                Vector2 force = direction.normalized * m_Force * (distance / m_Radius); // Чем ближе, тем сильнее притяжение

                collision.attachedRigidbody.AddForce(force, ForceMode2D.Force);
            }
        }

    #if UNITY_EDITOR
        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_Radius;
        }
    #endif
    }
}
