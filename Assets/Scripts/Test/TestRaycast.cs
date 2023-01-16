using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRaycast : MonoBehaviour
{
    void Update()
    {
        RaycastHit hit;

        Physics.Raycast(transform.position, transform.forward, out hit, 3);

        Debug.DrawLine(transform.position, transform.position + transform.forward * 3);

        Debug.Log("Raycast" + hit.collider.gameObject.name);
    }
}
