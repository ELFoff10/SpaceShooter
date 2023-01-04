using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ������������ �������. �������� � ������ �� �������� LevelBoundary ���� ������� ������� �� �����.
    /// �������� �� ������ ������� ���� ����������.
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

