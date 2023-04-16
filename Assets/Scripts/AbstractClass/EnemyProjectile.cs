using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : IHurter
{
    public LayerMask DontFUckingHit;
    public LayerMask DestroyOnHit;

    [SerializeField] private GameObject pfx;
    public override void OnDamage(Collider2D other, bool success)
    {
        if (((1<<other.gameObject.layer) & DontFUckingHit) != 0) // Check if part of canHit layermask
            return;
        
        Debug.Log("HIT!" + other.gameObject.name);

        if (success) {
            Destroy(gameObject);
            var tempPfx = Instantiate(pfx, transform.position, Quaternion.identity);
            Destroy(tempPfx, .5f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (((1<<other.gameObject.layer) & DontFUckingHit) != 0) // Check if part of canHit layermask
            return;

        
        if (((1<<other.gameObject.layer) & DestroyOnHit) != 0) // Check if part of canHit layermask
        {
            Destroy(gameObject);
            var tempPfx = Instantiate(pfx, transform.position, Quaternion.identity);
            Destroy(tempPfx, .5f);
        }

    }
}
