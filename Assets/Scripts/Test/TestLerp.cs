using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestLerp : MonoBehaviour
{
    public float a;
    public float b;
    public float c;

    public Vector3 aa;
    public Vector3 bb;
    public Vector3 cc;

    [Range(0f, 1f)]
    public float t;

    private void Update()
    {
        c = Mathf.Lerp(a, b, t);
        cc = Vector3.Lerp(aa, bb, t);

    }
}
