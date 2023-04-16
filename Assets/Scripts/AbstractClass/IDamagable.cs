using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IDamagable : MonoBehaviour
{
    public float Health { get => _Health; }
    public bool Dead   { get => _Dead;  }

    [SerializeField]
    protected float _Health  = 1f;
    [SerializeField]
    protected bool  _Dead    = false;

    protected float IFrameTime    = 0.0f;
    [SerializeField]
    protected float IFrameSeconds = 0.08f;
    protected bool UseHearts      = false;

    public virtual bool TakeDamage(GameObject attacker, float Amount)
    {
        // Check if we have iframes
        if (IFrameTime > Time.time)
            return false; // Return false to indicate a non-successful hit

        // Update IFrameTime so we have iframes for this long
        IFrameTime = Time.time + IFrameSeconds;

        // Remove decimal numbers in case we want to use a hearts system instead
        float DamageAmount = Amount;
        if (UseHearts)
            DamageAmount = Mathf.Floor(Amount);

        // Do the damage
        this._Health -= DamageAmount;
        
        // Check if the gameobject has a BuffManager
        BuffManager buffManager = GetComponent<BuffManager>();
        if (buffManager)
            buffManager.OnDamaged(); // Trigger buff manager OnDamaged() event
        
        // Die if health is 0
        if (this._Health <= 0)
            this.Die();
    
        // Return true to indicate a successful hit
        return true;
    }

    public virtual void Die()
    {       
        if (_Dead) // Prevent being called twice
            return;

        BuffManager buffManager = GetComponent<BuffManager>();
        if (buffManager)
            buffManager.OnDeath(); // Trigger OnDeath() event if the gameobject has a BuffManager

        this._Dead = true; 
    }
}
