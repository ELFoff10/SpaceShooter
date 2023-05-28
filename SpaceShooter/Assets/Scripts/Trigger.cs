using UnityEngine;

namespace SpaceShooter
{
    public class Trigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var aiController = collision.transform.root.GetComponent<AIController>();

            if (aiController != null)
            {
                aiController.m_FlyingPoints++;
            }
        }
    }
}
