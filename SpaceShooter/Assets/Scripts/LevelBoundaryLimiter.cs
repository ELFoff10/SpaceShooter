using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Ограничитель позиции. Работает в связке со скриптом LevelBoundary если таковой имеется на сцене.
    /// Вешаем на объект который надо ограничить.
    /// </summary>
    public class LevelBoundaryLimiter : MonoBehaviour
    {
        private void Update()
        {
            if (LevelBoundary.Instance == null) return;

            var levelBoundary = LevelBoundary.Instance;

            var radius = levelBoundary.WorldRadius;

            if (transform.position.magnitude > radius)
            {
                if (levelBoundary.LimitMode == LevelBoundary.Mode.Limit)
                {
                    transform.position = transform.position.normalized * radius;
                }

                if (levelBoundary.LimitMode == LevelBoundary.Mode.Teleport)
                {
                    transform.position = -transform.position.normalized * radius;
                }
            }
        }
    }
}

