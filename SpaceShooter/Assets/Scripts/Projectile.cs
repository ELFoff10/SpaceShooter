using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        [SerializeField] private float m_Velocity, m_LifeTime, m_Radius;

        [SerializeField] private int m_Damage;
        [SerializeField] private int m_AreaDamage;

        [SerializeField] private ImpactEffect m_Effect; // Создаем на пару сек объект в конце жизни снярада

        [SerializeField] private bool isHoming, isAreaDamage = false;

        private float m_Timer;

        private bool m_IsPlayer;

        private SpaceShip m_ParentShip;
        private Destructible m_Target;
        private Destructible m_Parent;

        private void Update()
        {
            float stepLenght = Time.deltaTime * m_Velocity; // Смещение снаряда в каждом кадре

            Vector2 step = GetDirection() * stepLenght;

            //Raycast выпускает луч впереди снаряда и если впереди что то есть и он Destructible, то мы наносим ему урон.
            //В Raycast мы указываем дистакицию луча и т.к stepLenght = 3.3, если Velocity 10, то луч короткий 
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLenght);

            // Не забыть выкл в св-ах проекта, вкладка Physics2d иначе не заработает
            // disable queries hit trigger + start in collider

            if (hit == true)
            {
                var dest = hit.collider.transform.root.GetComponent<Destructible>();

                if (dest != null && dest != m_Parent)
                {
                    dest.ApplyDamage(m_Damage);

                    if (m_Parent == Player.Instance.ActiveShip)
                    {
                        Player.Instance.AddScore(dest.ScoreValue);

                        if (dest.CurrentHitPoints <= 0)
                        {
                            Player.Instance.AddKill();
                        }
                    }

                    if (m_IsPlayer && dest.CurrentHitPoints <= 0)
                    {
                        Player.Instance.AddScore(dest.ScoreValue);
                    }

                    if (isAreaDamage == true)
                    {
                        var allAffected = Physics2D.OverlapCircleAll(transform.position, m_Radius); // Радиус после столкновения

                        foreach (var affectObject in allAffected)
                        {
                            // Все Destructible в радиусе, после столкновения снаряда с объектом
                            Destructible destructible = affectObject.transform.root.GetComponent<Destructible>();

                            if (destructible != null && destructible != m_Parent)
                            {
                                destructible.ApplyDamage(m_Damage);

                                if (m_Parent == Player.Instance.ActiveShip)
                                {
                                    Player.Instance.AddScore(dest.ScoreValue);

                                    if (destructible.CurrentHitPoints <= 0)
                                    {
                                        Player.Instance.AddKill();
                                    }
                                }
                            }
                        }
                    }

                    OnProjectileLifeEnd(hit.collider, hit.point);
                }
            }

            m_Timer += Time.deltaTime;

            if (m_Timer > m_LifeTime)
                Destroy(gameObject);

            transform.position += new Vector3(step.x, step.y, 0);
        }

        private void OnProjectileLifeEnd(Collider2D collider2D, Vector2 vector2)
        {
            Instantiate(m_Effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        public void SetParentShooter(Destructible destructible)
        {
            m_Parent = destructible;
            m_ParentShip = destructible.GetComponent<SpaceShip>();
            m_Target = FindNearestDestructableTarget(m_ParentShip);
            m_IsPlayer = m_Parent == Player.Instance.ActiveShip;
        }

        private Destructible FindNearestDestructableTarget(SpaceShip spaceShip) // Находит ближайший объект с Destructible
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

        private Vector3 GetDirection() // Направление снаряда, нормализованный вектор
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


