using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IHurter : MonoBehaviour
{
    public float Damage = 1f;
    public LayerMask canHit;

    public virtual void OnDamage(Collider2D other, bool tookDamage)
    {
        
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {

        if (((1<<other.gameObject.layer) & canHit) == 0) // Check if part of canHit layermask
            return;

        IDamagable otherDamagable = other.GetComponent<IDamagable>();
    
        bool successfulHit = false;

        if (otherDamagable)
            successfulHit = otherDamagable.TakeDamage(this.gameObject, this.Damage);

        OnDamage(other, successfulHit);
    }
}
