using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdinTimeSlowTemporaryBoostBuff : IBuffInterface
{
    public override float DamageMult { get => 0f; }
    public override float SpeedMult { get => 0f; }
    public override float HeatLossMult { get => 0f; }

    public override string Name { get => ""; }
    public override string Description { get => ""; } 
    
    public float removeAt = 0f;

    void Start() {
        base.OnAdded();
        removeAt = Time.time + 10f;
        GetComponent<PlayerController>().SetGhostType(3);
        GetComponent<PlayerController>().EnableGhost();
    }

    void Update() {

        if (removeAt < Time.time)
        {
            base.Remove();
            GetComponent<PlayerController>().DisableGhost();
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
