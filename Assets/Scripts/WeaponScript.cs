using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponScript : MonoBehaviour
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
    private void Update()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rot;

        /*if (Input.GetMouseButtonDown(0))
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
        }*/
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

    public void Attack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
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
}
