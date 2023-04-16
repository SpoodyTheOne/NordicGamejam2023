using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmothingy : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 3f);
    }
}
