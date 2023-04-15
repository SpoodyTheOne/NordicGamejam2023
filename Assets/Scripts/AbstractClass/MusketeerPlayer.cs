using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusketeerPlayer : Player
{
    [SerializeField] private GameObject fx;
    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.E))
            Target();
    }
    private void Target()
    {
        var target = Instantiate(fx, mousePos, Quaternion.identity);
    }
    public void StartPFX()
    {
        pfx.Play();
        pfx.loop = true;
    }
}
