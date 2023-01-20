using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class AITargetControl : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            AIController aIController = collision.transform.root.GetComponent<AIController>();

            if (aIController != null)
            {

            }
        }
    }

    //private static int SolveQuadratic(float a, float b, float c, out float root1, out float root2)
    //{
    //    var discriminant = b * b - 4 * a * c;
    //    if (discriminant < 0)
    //    {
    //        root1 = Mathf.Infinity;
    //        root2 = -root1;
    //        return 0;
    //    }

    //    root1 = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
    //    root2 = (-b - Mathf.Sqrt(discriminant)) / (2 * a);

    //    return discriminant > 0 ? 2 : 1;
    //}

    //private bool MakeLead(Vector2 a, Vector2 b, Vector2 vA, float speedProjectile, out Vector3 movePosition)
    //{
    //    var aToB = b - a;
    //    var dC = aToB.magnitude;
    //    var alpha = Vector2.Angle(aToB, vA) * Mathf.Rad2Deg;
    //    var speed = vA.magnitude;
    //    var r = speed / speedProjectile;

    //    if (SolveQuadratic(1 - r * r, 2 * r * dC * Mathf.Cos(alpha), -(dC * dC), out var root1, out var root2) == 0)
    //    {
    //        movePosition = Vector3.zero;
    //        return false;
    //    }
    //    var dA = Mathf.Max(root1, root2);
    //    var t = dA / speedProjectile;
    //    var c = a + vA * t;
    //    movePosition = (c - b).normalized;

    //    return true;
    //}
}
