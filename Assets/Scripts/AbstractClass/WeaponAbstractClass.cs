using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Player player;
    private Vector2 dir;

    public virtual void Update()
    {
        if(player.Gamepad)
        {
            dir = new Vector2(player.virtualMousePos.x - transform.position.x, player.virtualMousePos.y - transform.position.y);
        } else
        {
            dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        }

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rot;
    }
    public void Woosh()
    {
        GetComponent<AudioSource>().Play();
    }
}
