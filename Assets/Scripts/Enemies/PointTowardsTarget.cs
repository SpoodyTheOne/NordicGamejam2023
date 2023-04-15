using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTowardsTarget : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        transform.up = direction;
    }
}
