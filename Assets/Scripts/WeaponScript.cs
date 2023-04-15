using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : Weapon
{
    private Animator anim;

    private bool dontStop, started = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        started = false;
        anim.SetTrigger("OnEnable");
    }
    public override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0))
        {
            if (!started)
            {
                started = true;
                anim.SetTrigger("StartAnimation");
            }
            else
            {
                dontStop = true;
            }
        }
    }

    public void StopAnimation()
    {
        if (!dontStop)
        {
            anim.SetTrigger("StopAnimation");
            started = false;
        }

        dontStop = false;
    }
}
