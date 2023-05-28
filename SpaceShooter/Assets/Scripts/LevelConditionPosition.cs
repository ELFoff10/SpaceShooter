using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionPosition : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var spaceShip = collision.transform.root.GetComponent<SpaceShip>();

            if (spaceShip != null)
            {
                LevelSequenceController.Instance.FinishCurrentLevel(true);
            }
        }
    }
}

