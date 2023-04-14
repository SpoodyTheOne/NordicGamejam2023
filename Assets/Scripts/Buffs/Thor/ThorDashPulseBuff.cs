using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ThorDashPulseBuff : IBuffInterface
{
    public override float DamageMult { get => 0f; }
    public override float SpeedMult { get => 0f; }
    public override float HeatLossMult { get => 0f; }

    public override string Name { get => "Thunder Pulse"; }
    public override string Description { get => "When dashing you make a big explosion"; } 
    
    ParticleSystem dashEffect;
    float lastPulse = 0f;

    void Start() {
        base.OnAdded();
        
        if (PhotonNetwork.IsMasterClient) {
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
        if (lastPulse < Time.time) {
            
            GameObject ps = PhotonNetwork.Instantiate("ThorDashEffect",gameObject.transform.position,Quaternion.identity);
            ps.transform.parent = this.transform;
            ps.GetComponent<ParticleSystem>().Play();

            lastPulse = Time.time + 3f;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,10f);

            foreach (Collider2D collider in colliders) {
                EnemyHealth enemy = collider.gameObject.GetComponent<EnemyHealth>();
                if (enemy != null)
                    enemy.Damage(5f);
            }
        }
    }

    public override void OnEnemyDamaged(GameObject enemy)
    {

    }

}
