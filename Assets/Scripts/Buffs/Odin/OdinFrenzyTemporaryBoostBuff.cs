using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdinFrenzyTemporaryBoostBuff : IBuffInterface
{
    public override float DamageMult { get => 1f; }
    public override float SpeedMult { get => 1f; }
    public override float HeatLossMult { get => -1f; }

    public override string Name { get => ""; }
    public override string Description { get => ""; } 
    
    public float TimeToRemove = 0f;

    void Start() {
        base.OnAdded();
        TimeToRemove = Time.time + 5f;

        PlayerController PC = GetComponent<PlayerController>();

        PC.SetGhostType(1);
        PC.EnableGhost();

        // PhotonNetwork.Instantiate("OdinFrenzyRune",transform.position,Quaternion.identity);
    }

    void Update() {
        if (TimeToRemove < Time.time) {
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
