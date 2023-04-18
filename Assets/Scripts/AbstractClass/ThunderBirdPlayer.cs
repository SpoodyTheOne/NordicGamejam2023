using UnityEngine;
using EZCameraShake;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

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

    private bool coolDown1 = false, coolDown2 = false;

    public Image img;
    public Image img2;

    public override void Awake()
    {
        base.Awake();

        weaponScript = GetComponentInChildren<WeaponScript>();
        BirdFormDeactivate();
    }
    public override void Update()
    {
        base.Update();

        if (coolDown1)
        {
            img.fillAmount -= 1 / commonCooldown * Time.deltaTime;

            if (img.fillAmount <= 0)
            {
                img.fillAmount = 0;
                coolDown1 = false;
            }
        }

        if (coolDown2)
        {
            img2.fillAmount -= 1 / specialCooldown * Time.deltaTime;

            if (img2.fillAmount <= 0)
            {
                img2.fillAmount = 0;
                coolDown2 = false;
            }
        }

        float tempSpice = currentSpice / 100f;

        float tempHealth = (1 / maxHealth) * Health;

        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, tempHealth, 10f * Time.deltaTime);


        rosemary.fillAmount = tempSpice;
        rosemary.GetComponentInChildren<TextMeshProUGUI>().text = currentSpice + " / " + maxSpice;

        rosemary.fillAmount = Mathf.Lerp(rosemary.fillAmount, currentSpice / 100f, 100f * Time.deltaTime);

        virtualMousePos = Camera.main.ScreenToWorldPoint(new Vector2(virtualCursor.position.x, virtualCursor.position.y));

        if (!birdForm)
        {
            //movement.x = Input.GetAxisRaw("Horizontal");
            //movement.y = Input.GetAxisRaw("Vertical");
            movement.x = movementInput.x;
            movement.y = movementInput.y;
        }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
            anim.SetBool("Walking", true);
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

    public override bool TakeDamage(GameObject attacker, float amount)
    {
        return base.TakeDamage(attacker, amount);
    }

    private void BirdFormActivate()
    {
        if (Gamepad)
            lookDir = virtualMousePos - rb.position;
        else
            lookDir = mousePos - rb.position;

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

        if(Gamepad)
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(virtualMousePos.x, virtualMousePos.y), speed * Time.deltaTime);
        else
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(mousePos.x, mousePos.y), speed * Time.deltaTime);

        if (timeElapsed > flyTime)
            BirdFormDeactivate();
    }
    public void BirdFormDeactivate()
    {
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
        if (coolDown2)
            return;

        coolDown2 = true;
        img2.fillAmount = 1;

        if (currentSpice < specialCost)
            return;

        tp = true;

        GameObject.Find("Wings").GetComponent<Animator>().SetTrigger("Wing");

        currentSpice -= specialCost;
        var target = Instantiate(fx[1], mousePos, Quaternion.identity);
    }

    public void StartPFX()
    {
        pfx.Play();
        pfx.loop = true;
    }

    public void RightClickAbility(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
            BirdFormDeactivate();

        if (coolDown1)
            return;

        if (currentSpice < commonCost)
            return;

        if (ctx.performed)
            if(!birdForm && !tp)
            {
                coolDown1 = true;
                img.fillAmount = 1;

                currentSpice -= commonCost;

                effect = Instantiate(fx[0], gameObject.transform.position, Quaternion.identity);
                Destroy(effect, 1.2f);

                effect.transform.parent = playerForms[1].transform;

                timeElapsed = 0;
                birdForm = true;
            }
    }

    public void EAbility(InputAction.CallbackContext ctx)
    {
        Debug.Log("H");

        if (ctx.canceled)
            Debug.Log("Hel");

        if (ctx.canceled && tp)
        {
            Debug.Log("Hello");
            if (Gamepad)
            {
                if (coolDown2)
                    return;

                coolDown2 = true;
                img2.fillAmount = 1;

                if (currentSpice < specialCost)
                    return;

                tp = true;

                GameObject.Find("Wings").GetComponent<Animator>().SetTrigger("Wing");

                currentSpice -= specialCost;

                gameObject.transform.position = virtualMousePos;
                gameObject.GetComponent<ThunderBirdPlayer>().BirdFormDeactivate();
                var pfx = Instantiate(fx[2], new Vector3(virtualMousePos.x, virtualMousePos.y, 0), Quaternion.identity);
                Destroy(pfx, 4.1f);
            }
            else
            {
                var pfx = Instantiate(fx[2], new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);
                Destroy(pfx, 4.1f);
            }

            tp = false;

            CameraShaker.Instance.ShakeOnce(10f, 10f, 0f, .67f);

            GameObject.Find("Wings").GetComponent<Animator>().SetTrigger("WingDown");
        }

        if (ctx.performed)
        {
            if (!birdForm && !tp && !coolDown2 && currentSpice > specialCost)
            {
                tp = true;
                Debug.Log("He");
                if (!Gamepad)
                    TeleportTarget();
            }
        }
    }
}
