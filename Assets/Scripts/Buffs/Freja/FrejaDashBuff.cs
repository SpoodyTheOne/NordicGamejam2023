using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrejaDashBuff : IBuffInterface
{
    public override float DamageMult { get => 0f; }
    public override float SpeedMult { get => 0f; }
    public override float HeatLossMult { get => 0f; }

    public override string Name { get => "Falcon Cloak"; }
    public override string Description { get => "Gain extra speed after you dash"; } 
    
    public float lastSpeedDash = 0f;

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
        if (lastSpeedDash < Time.time) {
            lastSpeedDash = Time.time + 5f;
            gameObject.AddComponent<FrejaDashTemporaryBoostBuff>();
        }
    }

    public override void OnEnemyDamaged(GameObject enemy)
    {

    }

}
