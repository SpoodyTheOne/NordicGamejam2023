using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyrBleedBuff : IBuffInterface
{
    public override float DamageMult { get => 0f; }
    public override float SpeedMult { get => 0f; }
    public override float HeatLossMult { get => 0f; }

    public override string Name { get => "Bleed bleed!!!"; }
    public override string Description { get => "After dealing damage to an enemy, they will bleed for a short period of time"; } 
    
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
        if (enemy.GetComponents<TyrBleedEffect>().Length < 3) {
            TyrBleedEffect bleed = enemy.AddComponent<TyrBleedEffect>();
            bleed.origin = GetComponent<BuffManager>();
        }
    }

}
