using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IDamagable : MonoBehaviour
{
    public float Health { get => _Health; }
    public bool Dead   { get => _Dead;  }

    private float _Health  = 1f;
    private bool  _Dead    = false;

    private float IFrameTime    = 0.0f;
    private float IFrameSeconds = 0.6f;
    private bool UseHearts      = false;

    public virtual bool TakeDamage(GameObject attacker, float Amount)
    {
        if (IFrameTime > Time.time)
            return false;

        IFrameTime = Time.time + IFrameSeconds;

        float DamageAmount = Amount;

        if (UseHearts)
            DamageAmount = Mathf.Floor(Amount);

        this._Health -= DamageAmount;
        
        BuffManager buffManager = GetComponent<BuffManager>();
        if (buffManager)
            buffManager.OnDamaged();

        if (this.Health <= 0)
            this.Die();

        return true;
    }

    public virtual void Die()
    {       
        BuffManager buffManager = GetComponent<BuffManager>();
        if (buffManager)
            buffManager.OnDeath();

        this._Dead = true; 
    }
}
