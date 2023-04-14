using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyrStunBuff : IBuffInterface
{
    public override float DamageMult { get => 0f; }
    public override float SpeedMult { get => 0f; }
    public override float HeatLossMult { get => 0f; }

    public override string Name { get => "Bonk"; }
    public override string Description { get => "Your attacks will sometimes stun the enemy"; } 
    
    public float lastStun = 0f;

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
        if (lastStun > Time.time)
            return;

        lastStun = Time.time + 2f;
        enemy.GetComponent<EnemyBehavior>().Stun(1f);
    }

}
