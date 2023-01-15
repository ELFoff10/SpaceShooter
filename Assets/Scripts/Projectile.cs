using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        [SerializeField] private float m_Velocity;

        [SerializeField] private float m_LifeTime;

        [SerializeField] private int m_Damage;

        [SerializeField] private ImpactEffect m_ImpactEffectPrefab; // Взрыв, когда снаряд врезается

        [SerializeField] private bool isHoming = false;

        [SerializeField] private bool isAreaDamage = false;

        [SerializeField] private float m_Radius = 10000;

        private float m_Timer;

        private SpaceShip m_ParentShip;
        private Destructible m_Target;
        private Destructible m_Parent;

        private void Update()
        {
            float stepLenght = Time.deltaTime * m_Velocity; // Смещение снаряда в каждом кадре

            //if (m_TurretProperties.Mode == TurretMode.HunterRocket)
            //{
            //    transform.position = Vector2.MoveTowards(transform.position, m_Target.transform.position, stepLenght);                
            //}
            Vector2 step = GetDirection() * stepLenght; // Для рейкаста

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLenght);

            if (hit)
            {         
                Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

                if (dest != null && dest != m_Parent)
                {
                    dest.ApplyDamage(m_Damage);

                    if (isAreaDamage == true)
                    {
                        var allAffected = Physics2D.OverlapCircleAll(transform.position, m_Radius);

                        foreach (var affectObject in allAffected)
                        {                           
                            if (affectObject == affectObject.transform.root.GetComponent<Destructible>())
                            {
                                affectObject.transform.root.GetComponent<Destructible>().ApplyDamage(m_Damage);
                            }
                        }
                    }
                }

                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            m_Timer += Time.deltaTime;

            if (m_Timer > m_LifeTime)
                Destroy(gameObject);

            transform.position += new Vector3(step.x, step.y, 0);
        }

        private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            Destroy(gameObject);
        }       

        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
            m_ParentShip = parent.GetComponent<SpaceShip>();
            m_Target = FindNearestDestructableTarget(m_ParentShip);
        }

        private Destructible FindNearestDestructableTarget(SpaceShip spaceShip)
        {
            float maxDist = float.MaxValue;
            Destructible potentialTarget = null;

            // Перебираем все разрушаемые объекты сцены.

            foreach (var destructible in Destructible.AllDestructibles)
            {
                if (destructible.GetComponent<SpaceShip>() == spaceShip) continue;

                // Определем дистанцию до объекта.
                float dist = Vector2.Distance(spaceShip.transform.position, destructible.transform.position);

                // Проверем что эта дистанция короче
                if (dist < maxDist)
                {
                    // Переписываем минимальную дистанцию на ещё более короткую.
                    maxDist = dist;

                    // Запоминаем этого врага (кэш - как наиболее подходящего для атаки).
                    potentialTarget = destructible;
                }
            }

            return potentialTarget;
        }

        private Vector3 GetDirection()
        {
            if (isHoming && m_Target != null)
            {
                transform.up = (m_Target.transform.position - transform.position).normalized;
                return (m_Target.transform.position - transform.position).normalized;
            }
            else
            {
                return transform.up;
            }
        }
    }
}


