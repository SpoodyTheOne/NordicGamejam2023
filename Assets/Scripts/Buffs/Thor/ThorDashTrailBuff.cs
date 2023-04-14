using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThorDashTrailBuff : IBuffInterface
{
    public override float DamageMult { get => 0f; }
    public override float SpeedMult { get => 0f; }
    public override float HeatLossMult { get => 0f; }

    public override string Name { get => "Tanngnj\u00f3str trail"; }
    public override string Description { get => "When dashing you leave behind a trail of lighting"; } 
    
    float lastPulse = 0f;

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
        if (lastPulse < Time.time) {
            gameObject.AddComponent<ThorDashTrailTemporaryBoostBuff>();
            lastPulse = Time.time + 3f;
        }
    }

    public override void OnEnemyDamaged(GameObject enemy)
    {

    }

}
