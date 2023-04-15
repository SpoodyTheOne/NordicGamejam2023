
using UnityEngine;
using EZCameraShake;

public class MusketWeapon : Weapon
{
    private Animator anim;
    [SerializeField] private ParticleSystem[] pfx;
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
        if (Input.GetMouseButtonDown(1) && canShoot)
            MassiveShot();
    }

    private void Shoot()
    {
        canShoot = false;

        pfx[0].Play();
        pfx[0].gameObject.GetComponent<AudioSource>().Play();
         
        anim.SetTrigger("Shoot");
    }
    private void MassiveShot()
    {
        canShoot = false;

        pfx[1].Play();
        pfx[1].gameObject.GetComponent<AudioSource>().Play();

        CameraShaker.Instance.ShakeOnce(10f, 10f, 0f, .67f);

        anim.SetTrigger("BigShoot");
    }
    public void CanShoot()
    {
        canShoot = true;
    }
}
