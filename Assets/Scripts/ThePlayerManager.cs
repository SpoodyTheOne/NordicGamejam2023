using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThePlayerManager : MonoBehaviour
{
    public Player player;
    public GameObject weapon;

    public int characterClass; // 1 = musket, 2 = thunder, 3 = cave

    public void Start()
    {
        weapon = player.GetComponentInChildren<WeaponScript>().gameObject;
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        player.Move(ctx);
    }

    public void Attack(InputAction.CallbackContext ctx)
    {
        weapon.GetComponent<WeaponScript>().Attack(ctx);
    }

    public void RightClickAbility(InputAction.CallbackContext ctx)
    {
        //player.RightClickAbility(ctx);
    }

    public void EAbility(InputAction.CallbackContext ctx)
    {
        //player.EAbility(ctx);
    }
}
