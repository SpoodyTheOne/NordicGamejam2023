using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThorDashTrailTemporaryBoostBuff : IBuffInterface
{
    public override float DamageMult { get => 0f; }
    public override float SpeedMult { get => 0f; }
    public override float HeatLossMult { get => 0f; }

    public override string Name { get => ""; }
    public override string Description { get => ""; } 
    
    public float lastSpawn = 0f;
    public float removeAt = 0f;

    void Start() {
        base.OnAdded();
        removeAt = Time.time + 0.1f;
    }

    void Update()
    {
        
        if (lastSpawn < Time.time) {

            lastSpawn = Time.time + 0.04f;

        }

        if (removeAt < Time.time)
        {
            base.Remove();
        }

    }

    public override void OnAttack()
    {
        
    }

    public override void OnDamaged()
    {

    }

    public override void OnDeath()
    {

    }

    public override void OnDash()
    {

    }

    public override void OnEnemyDamaged(GameObject enemy)
    {

    }

}
