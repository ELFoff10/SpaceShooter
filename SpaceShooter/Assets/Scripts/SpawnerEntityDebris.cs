using UnityEngine;

namespace SpaceShooter
{
    public class SpawnerEntityDebris : MonoBehaviour
    {
        [SerializeField] private Destructible[] m_DebrisPrefabs;

        [SerializeField] private CircleArea m_Area;

        [SerializeField] private int m_NumDebris;

        [SerializeField] private float m_RandomSpeed;

        void Start()
        {
            for (int i = 0; i < m_NumDebris; i++)
            {
                SpawnDebris();
            }
        }

        private void SpawnDebris()
        {
            int index = Random.Range(0, m_DebrisPrefabs.Length);

            float random = Random.Range(0, 360);

            GameObject debris = Instantiate(m_DebrisPrefabs[index].gameObject);

            debris.transform.position = m_Area.GetRandomInsideZone();
            debris.transform.localRotation = Quaternion.Euler(0, 0, random);

            // Слушаем, когда уничтожается объект
            debris.GetComponent<Destructible>().EventOnDeath.AddListener(OnDebrisLifeEnd); 

            Rigidbody2D rigidbody2D = debris.GetComponent<Rigidbody2D>();

            if (rigidbody2D != null && m_RandomSpeed > 0)
            {
                rigidbody2D.velocity = Random.insideUnitCircle * m_RandomSpeed; 
            }
        }

        private void OnDebrisLifeEnd()
        {
            SpawnDebris();
        }
    }
}

