using UnityEngine;
using EZCameraShake;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ThunderBirdPlayer : Player
{
    private bool birdForm;
    [SerializeField] private GameObject[] playerForms;

    private float flyTime = 1;

    [SerializeField] GameObject[] fx;
    private GameObject effect = null;

    private WeaponScript weaponScript;

    float timeElapsed;

    private bool tp;

    public Transform virtualCursor;

    public override void Awake()
    {
        base.Awake();

        weaponScript = GetComponentInChildren<WeaponScript>();
        BirdFormDeactivate();
    }
    public override void Update()
    {
        if (!birdForm)
        {
            //movement.x = Input.GetAxisRaw("Horizontal");
            //movement.y = Input.GetAxisRaw("Vertical");
            movement.x = movementInput.x;
            movement.y = movementInput.y;
        }

        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        /*if (Input.GetMouseButtonDown(1) && !birdForm && !tp)
        {
            effect = Instantiate(fx[0], gameObject.transform.position, Quaternion.identity);
            Destroy(effect, 1.2f);

            effect.transform.parent = playerForms[1].transform;

            timeElapsed = 0;
            birdForm = true;
        }
        if (Input.GetMouseButtonUp(1))
            BirdFormDeactivate();

        if (Input.GetKeyDown(KeyCode.E) && !birdForm && !tp)
        {
            tp = true;

            GameObject.Find("Wings").GetComponent<Animator>().SetTrigger("Wing");
            TeleportTarget();
        }
        if (Input.GetKeyUp(KeyCode.E) && tp)
        {
            tp = false;
            var pfx = Instantiate(fx[2], new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);
            Destroy(pfx, 4.1f);

            CameraShaker.Instance.ShakeOnce(10f, 10f, 0f, .67f);

            GameObject.Find("Wings").GetComponent<Animator>().SetTrigger("WingDown");
        }*/

        if (birdForm)
            BirdFormActivate();

        if (movement.magnitude > 0)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            pfx.loop = false;
            anim.SetBool("Walking", false);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        

    }

    private void BirdFormActivate()
    {
        virtualCursor.gameObject.SetActive(true);

        //lookDir = mousePos - rb.position;
        lookDir = new Vector2(virtualCursor.position.x, virtualCursor.position.y) - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        rb.rotation = angle;

        weaponScript.gameObject.SetActive(false);
        if (timeElapsed < flyTime)
        {
            float valueToLerp = Mathf.Lerp(0, 1, timeElapsed / flyTime);
            timeElapsed += Time.deltaTime;
        }

        speed = Mathf.Lerp(speed, 16f, timeElapsed);

        playerForms[1].SetActive(true);
        playerForms[0].SetActive(false);

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(mousePos.x, mousePos.y), speed * Time.deltaTime);

        if (timeElapsed > flyTime)
            BirdFormDeactivate();
    }
    public void BirdFormDeactivate()
    {
        virtualCursor.gameObject.SetActive(false);
        rb.rotation = 0;

        speed = 4;

        weaponScript.gameObject.SetActive(true);
        if (effect != null)
        {
            effect.transform.parent = null;
            effect.gameObject.GetComponent<ParticleSystem>().Stop();
        }

        timeElapsed = 0;
        birdForm = false;

        playerForms[0].SetActive(true);
        playerForms[1].SetActive(false);
    }
    public void TeleportTarget()
    {
        var target = Instantiate(fx[1], mousePos, Quaternion.identity);
    }

    public void StartPFX()
    {
        pfx.Play();
        pfx.loop = true;
    }

    public void RightClickAbility(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if(!birdForm && !tp)
            {
                effect = Instantiate(fx[0], gameObject.transform.position, Quaternion.identity);
                Destroy(effect, 1.2f);

                effect.transform.parent = playerForms[1].transform;

                timeElapsed = 0;
                birdForm = true;
            }
        }

        if (ctx.canceled)
        {
            BirdFormDeactivate();
        }
    }

    public void EAbility(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if(!birdForm && !tp)
            {
                tp = true;

                GameObject.Find("Wings").GetComponent<Animator>().SetTrigger("Wing");
                TeleportTarget();
            }
        }

        if (ctx.canceled)
        {
            if (!tp)
            {
                tp = false;
                var pfx = Instantiate(fx[2], new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);
                Destroy(pfx, 4.1f);

                CameraShaker.Instance.ShakeOnce(10f, 10f, 0f, .67f);

                GameObject.Find("Wings").GetComponent<Animator>().SetTrigger("WingDown");
            }
        }
    }
}
