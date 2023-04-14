using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FrejaArrowBuff : IBuffInterface
{
    public override float DamageMult { get => 0f; }
    public override float SpeedMult { get => 0f; }
    public override float HeatLossMult { get => 0f; }

    public override string Name { get => "Heartseeker"; }
    public override string Description { get => "Fire arrows at enemies when you damage them"; } 
    
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

        Debug.Log("MAking le arro");
        GameObject arrow = PhotonNetwork.Instantiate("FrejaArrow",transform.position + new Vector3(0,3,0),Quaternion.identity);
        arrow.GetComponent<FrejaArrow>().target = enemy.transform;
    }

}
