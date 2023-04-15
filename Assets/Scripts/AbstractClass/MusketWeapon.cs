
using UnityEngine;
using EZCameraShake;
using UnityEngine.InputSystem;

public class MusketWeapon : Weapon
{
    private Animator anim;
    [SerializeField] private ParticleSystem pfx;
    private bool canShoot = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0) && canShoot)
            Shoot();
    }

    private void Shoot()
    {
        canShoot = false;

        pfx.Play();
        pfx.gameObject.GetComponent<AudioSource>().Play();
         
        anim.SetTrigger("Shoot");
    }
    public void CanShoot()
    {
        canShoot = true;
    }

    public void Attack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (canShoot)
                Shoot();
        }
    }
}
