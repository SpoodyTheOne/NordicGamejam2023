
using UnityEngine;
using EZCameraShake;
using UnityEngine.InputSystem;

public class MusketWeapon : Weapon
{
    private Animator anim;
    [SerializeField] private ParticleSystem pfx;
    private bool canShoot = true;

    [SerializeField] GameObject[] weapons;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0) && canShoot)
            Shoot();

        if (Input.GetMouseButtonDown(1))
        {
            GetComponentInParent<Player>().speed /= 5;
            anim.SetTrigger("EquipCanon");
            weapons[1].SetActive(true);
            weapons[0].SetActive(false);
        }
        if (Input.GetMouseButtonUp(1))
        {
            GetComponentInParent<Player>().speed *= 5;
            anim.SetTrigger("EquipMusket");
            weapons[1].SetActive(false);
            weapons[0].SetActive(true);
        }
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
