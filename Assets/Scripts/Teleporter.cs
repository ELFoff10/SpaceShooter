using UnityEngine;

namespace SpaceShooter
{
    //[RequireComponent(typeof(AudioSource))]
    public class Teleporter : MonoBehaviour
    {
        [SerializeField] private Teleporter target;
        //[HideInInspector] private new AudioSource audio;

        [HideInInspector] public bool IsReceive;

        //private void Start()
        //{
        //    audio = GetComponent<AudioSource>();
        //}

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsReceive == true) return;

            SpaceShip spaceShip = collision.transform.root.GetComponent<SpaceShip>();

            if (spaceShip != null)
            {
                target.IsReceive = true;

                spaceShip.transform.position = target.transform.position;

                //audio.Play();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            SpaceShip spaceShip = collision.transform.root.GetComponent<SpaceShip>();

            if (spaceShip != null)
            {
                IsReceive = false;
            }
        }
    }
}

