using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class Powerup : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            SpaceShip ship = collision.transform.root.GetComponent<SpaceShip>(); // Столкнулся ли Powerup с кораблём.

            // ship != null - это значит что корабль столкнулся, 
            if (ship != null && Player.Instance.ActiveShip/*Проверка, для добавления бонуса только нашему кораблю.*/)
                
            {
                OnPickedUp(ship); //Уже ночь, туплю, узнать как называется, когда в метод мы добавляем экземпляр класса
                                  //ship именно какой корабль столкнулся и кому начислить бонусы

                Destroy(gameObject);
            }
        }

        protected abstract void OnPickedUp(SpaceShip ship);
    }
}

