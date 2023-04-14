using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrejaHalfHealthBuff : IBuffInterface
{
    public override float DamageMult { get => 0f; }
    public override float SpeedMult { get => 0f; }
    public override float HeatLossMult { get => 0f; }

    public override string Name { get => "Broken Heart"; }
    public override string Description { get => "Deal extra damage to enemies below half health"; } 
    
    public float added = 0f;

    void Start() {
        base.OnAdded();
    }

    void Update() {

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
        EnemyHealth hp = enemy.GetComponent<EnemyHealth>();

        if (hp.currentHealth / hp.maxHealth < 0.5f)
            hp.Damage(1.5f);
    }

}
