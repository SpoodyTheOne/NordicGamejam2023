using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : IHurter
{
    [SerializeField] private GameObject pfx;
    public override void OnDamage(Collider2D other)
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var tempPfx = Instantiate(pfx, transform.position, Quaternion.identity);
        Destroy(tempPfx, .5f);

        Destroy(gameObject);
    }
}
