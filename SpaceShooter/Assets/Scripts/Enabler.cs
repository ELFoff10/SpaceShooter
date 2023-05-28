using UnityEngine;

namespace SpaceShooter
{
    public class Enabler : MonoBehaviour
    {
        [SerializeField] private GameObject m_GameObject;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var ship = transform.root.GetComponent<SpaceShip>();

            if (ship != null)
            {
                m_GameObject.SetActive(true);
            }
        }
    }
}

