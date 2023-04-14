using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdinFrenzyBuff : IBuffInterface
{
    public override float DamageMult { get => 0f; }
    public override float SpeedMult { get => 0f; }
    public override float HeatLossMult { get => 0f; }

    public override string Name { get => "To Valhalla!"; }
    public override string Description { get => "When killing an enemy, you will sometimes make a frenzied zone that grants bonusses"; } 
    
    public float lastFrenzy = 0f;

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
        if (lastFrenzy > Time.time || enemy.GetComponent<EnemyHealth>().currentHealth > 5)
            return;

        lastFrenzy = Time.time + 20f;

        Collider2D[] overlap = Physics2D.OverlapCircleAll(transform.position,5f);

        foreach (Collider2D collider in overlap) {

            BuffManager buffManager = collider.GetComponent<BuffManager>();

            if (buffManager != null)
            {
                IBuffInterface frenzy = new OdinFrenzyTemporaryBoostBuff();

                buffManager.AddBuffRPC(frenzy);

                Destroy(frenzy);
            }

        }
    }

}
