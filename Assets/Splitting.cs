using UnityEngine;

namespace SpaceShooter
{
    public class Splitting : MonoBehaviour
    {
        private void Start()
        {
            SplittingOnSmallAsteroids();
        }
        private void Update()
        {
            SplittingOnSmallAsteroids();
        }
        private void SplittingOnSmallAsteroids()
        {
            Instantiate(gameObject);
        }
    }
}

