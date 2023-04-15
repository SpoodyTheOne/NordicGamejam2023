using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IHurter : MonoBehaviour
{
    public float Damage = 1f;
    public LayerMask canHit;

    public virtual void OnDamage(Collider2D other)
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1<<other.gameObject.layer) & ignoreLayers) == 0) // Check if part of canHit layermask

            return;

        IDamagable otherDamagable = other.GetComponent<IDamagable>();

        if (otherDamagable)
            otherDamagable.TakeDamage(this.gameObject, this.Damage);
    }
}
