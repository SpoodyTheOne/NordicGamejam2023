using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : IHurter
{
    public override void OnDamage(Collider2D other)
    {
        Destroy(gameObject);
    }
}
