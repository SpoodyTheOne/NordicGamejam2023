using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FrejaDashTemporaryBoostBuff : IBuffInterface
{
    public override float DamageMult { get => 0f; }
    public override float SpeedMult { get => 1f; }
    public override float HeatLossMult { get => 0f; }

    public override string Name { get => ""; }
    public override string Description { get => ""; } 

    public float lastBoost = 0f;

    void Start() {
        base.OnAdded();
        lastBoost = Time.time + 2f;

        PlayerController PC = GetComponent<PlayerController>();

        PC.SetGhostType(2);
        PC.EnableGhost();

        //PhotonNetwork.Instantiate("OdinFrenzyRune",transform.position,Quaternion.identity);
    }

    void Update() {
        if (lastBoost < Time.time) {
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
