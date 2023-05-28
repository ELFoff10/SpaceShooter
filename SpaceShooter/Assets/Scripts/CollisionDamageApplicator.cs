using UnityEngine;

namespace SpaceShooter
{
    public class CollisionDamageApplicator : MonoBehaviour
    {
        public static string IgnoreTag = "WorldBoundary";
        public static string PointTag = "Point";

        [SerializeField] private float m_VelocityDamageModifier;
        [SerializeField] private float m_DamageConstant;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == IgnoreTag) return;

            //var aiController = transform.root.GetComponent<AIController>();

            //if (aiController != null)
            //{
            //    if (collision.transform.tag == PointTag)
            //    {
            //        aiController.Increase();
            //        Debug.Log(collision.transform.name);
            //    }
            //}

            var destructible = transform.root.GetComponent<Destructible>(); // root главный родительский объект

            if (destructible != null)
            {
                // учитываем урон от скорости. collision.relativeVelocity.magnitude = наша скорость
                destructible.ApplyDamage((int)m_DamageConstant +
                                         (int)(m_VelocityDamageModifier * collision.relativeVelocity.magnitude));
            }
        }
    }
}
