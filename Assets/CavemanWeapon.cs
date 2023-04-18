using UnityEngine;
using UnityEngine.InputSystem;

public class CavemanWeapon : Weapon
{
    private Animator anim;

    [HideInInspector] public bool charging, blocking;
    float timeElapsed = 0, currentScale = 2;

    private bool hitDone = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0) && currentScale < 2.1f && !blocking && hitDone)
            StartCharge();
        if (Input.GetMouseButtonUp(0) && charging && !blocking)
            StopCharge();

        if (Input.GetMouseButtonDown(1) && !blocking && !charging && GetComponentInParent<Player>().currentSpice > GetComponentInParent<Player>().commonCost)
        {
            GetComponentInParent<Player>().speed /= 2f;
            blocking = true;
            anim.SetBool("Blocking", true);
        }
        else if (Input.GetMouseButtonUp(1) && blocking && !charging)
        {
            GetComponentInParent<Player>().speed *= 2f;
            blocking = false;
            anim.SetBool("Blocking", false);
        }

        if (charging)
            if (timeElapsed < 7f)
            {
                currentScale = Mathf.Lerp(currentScale, 10f, timeElapsed / 2500f);
                timeElapsed += Time.deltaTime;
            }

        if (!charging)
        {
            currentScale = Mathf.Lerp(currentScale, 2f, timeElapsed / 40f);
            timeElapsed += Time.deltaTime;
        }

        GameObject.Find("baton").transform.localScale = new Vector3(currentScale, currentScale, 1);

    }
    private void StartCharge()
    {
        hitDone = false;
        anim.ResetTrigger("Hit");

        anim.SetTrigger("Charge");

        timeElapsed = 0;
        charging = true;
    }
    private void StopCharge()
    {
        GetComponentInChildren<TriggerHurt>().Damage = Mathf.Ceil(currentScale) * 3;

        charging = false;

        timeElapsed = 0;
        anim.SetTrigger("Hit");
    }

    public void HitDone()
    {
        hitDone = true;
    }

    public void Attack(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            StartCharge();
        }

        if(ctx.canceled)
        {
            StopCharge();
        }
    }

    public void RightClickAbility(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && currentScale < 2.1f && !blocking && hitDone)
        {
            GetComponentInParent<Player>().speed /= 2f;
            blocking = true;
            anim.SetBool("Blocking", true);
        }

        if (ctx.canceled && blocking && !charging)
        {
            GetComponentInParent<Player>().speed *= 2f;
            blocking = false;
            anim.SetBool("Blocking", false);
        }
    }
}
