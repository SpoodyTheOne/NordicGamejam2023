using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChildDamagable : IDamagable
{
    public Player player;
    public override bool TakeDamage(GameObject attacker, float amount)
    {
        base.TakeDamage(attacker, amount);
        return player.TakeDamage(attacker, amount);
    }
}
