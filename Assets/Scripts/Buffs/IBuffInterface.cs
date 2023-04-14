using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IBuffInterface : MonoBehaviour
{
    public virtual float SpeedMult  { get => 0f; }
    public virtual float DamageMult { get => 0f; }
    public virtual float HealthMult { get => 0f; }
    public virtual float ArmorMult  { get => 0f; }

    public abstract string Name { get; }
    public abstract string Description { get; }

    public virtual void OnAdded()
    {
        GetComponent<BuffManager>().BuffAdded(this);
    }

    public virtual void Remove() 
    {
        GetComponent<BuffManager>().BuffRemoved(this);
        Destroy(this);
    }

    public virtual void OnAttack()
    {

    }

    public virtual void OnDamaged()
    {

    }


    public virtual void OnDeath()
    {

    }

    public virtual void OnDash()
    {

    }

    public virtual void OnEnemyDamaged(GameObject enemy) 
    {

    }

    public virtual void OnEnemyKilled(GameObject enemy)
    {

    }

}
