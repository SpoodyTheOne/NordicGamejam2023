using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdinTimeSlowBuff : IBuffInterface
{
    public override float DamageMult { get => 0f; }
    public override float SpeedMult { get => 0f; }
    public override float HeatLossMult { get => 0f; }

    public override string Name { get => "Slow down"; }
    public override string Description { get => "You can slow down enemies in an area"; } 
    
    public float lastSlow = 0f;

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
        if (enemy.GetComponent<EnemyHealth>().currentHealth < 3 && lastSlow < Time.time) {
            
            lastSlow = Time.time + 60f;

            GetComponent<PlayerController>().SetEnemySpeedMult(0.3f);
            gameObject.AddComponent<OdinTimeSlowTemporaryBoostBuff>();
            StartCoroutine("Resetspeed");

        }
    }

    IEnumerator Resetspeed() {
        yield return new WaitForSeconds(10f);

        GetComponent<PlayerController>().SetEnemySpeedMult(1f);

        yield return null;
    }

}
