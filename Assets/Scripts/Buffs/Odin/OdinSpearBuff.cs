using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdinSpearBuff : IBuffInterface
{
    public override float DamageMult { get => 0f; }
    public override float SpeedMult { get => 0f; }
    public override float HeatLossMult { get => 0f; }

    public override string Name { get => "Gungnir Strike"; }
    public override string Description { get => "When damaging an enemy with your weapon, Odin's spear Gungnir will strike that enemy for extra damage"; } 
    
    public float lastSpear = 0f;

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
        if (lastSpear > Time.time)
            return;

        lastSpear = Time.time + 1f;
    }

}
