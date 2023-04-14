using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThorHitLightningBuff : IBuffInterface
{
    public override float DamageMult { get => 0f; }
    public override float SpeedMult { get => 0f; }
    public override float HeatLossMult { get => 0f; }

    public override string Name { get => "Lightning strike"; }
    public override string Description { get => "Lightning will Strike when you hit an enemy"; } 

    ParticleSystem dashEffect;
    float lastStrike = 0f;

    void Start() {
        base.OnAdded();
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
        if (lastStrike > Time.time)
            return;

        lastStrike = Time.time + 2f;
    }

}
