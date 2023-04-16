using UnityEngine;

public class CavemanWeapon : Weapon
{
    private Animator anim;

    [HideInInspector] public bool charging, blocking;
    float timeElapsed = 0, currentScale = 2;
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0) && currentScale < 2.1f && !blocking)
            StartCharge();
        if (Input.GetMouseButtonUp(0) && charging && !blocking)
            StopCharge();

        if (Input.GetMouseButtonDown(1) && !blocking)
        {
            GetComponentInParent<Player>().speed /= 2f;
            blocking = true;
            anim.SetBool("Blocking", true);
        }
        if (Input.GetMouseButtonUp(1) && blocking)
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
        anim.SetTrigger("Charge");

        timeElapsed = 0;
        charging = true;
    }
    private void StopCharge()
    {
        GetComponentInChildren<TriggerHurt>().Damage = Mathf.Ceil(currentScale) * 2;

        charging = false;

        timeElapsed = 0;
        anim.SetTrigger("Hit");
    }
}