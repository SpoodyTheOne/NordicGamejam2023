using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ThorHitLightningBuff : IBuffInterface
{
    public override float DamageMult { get => 0f; }
    public override float SpeedMult { get => 0f; }
    public override float HeatLossMult { get => 0f; }

    public override string Name { get => "Lightning strike"; }
    public override string Description { get => "Lightning will Strike when you hit an enemy"; } 
    
    public PhotonView view;

    ParticleSystem dashEffect;
    float lastStrike = 0f;

    void Start() {
        base.OnAdded();

        //as we are on a player we a guaranteed a photon view
        view = GetComponent<PhotonView>();
        
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

    }

    public override void OnEnemyDamaged(GameObject enemy)
    {

        if (lastStrike > Time.time)
            return;

        lastStrike = Time.time + 2f;

        GameObject ps = PhotonNetwork.Instantiate("ThorHitEffect",enemy.transform.position + new Vector3(0,10,0),Quaternion.Euler(90,0,0));
        ps.GetComponent<ParticleSystem>().Play();
        enemy.GetComponent<EnemyHealth>().Damage(5f);

    }

}
