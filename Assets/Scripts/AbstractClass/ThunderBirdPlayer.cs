using UnityEngine;
using UnityEngine.UI;

public class ThunderBirdPlayer : Player
{
    private bool birdForm;
    [SerializeField] private GameObject[] playerForms;

    private float flyTime = 1;
    [SerializeField] private Image timeForFly;

    [SerializeField] private ParticleSystem pfx;
    private Animator anim;

    [SerializeField] GameObject[] fx;
    private GameObject effect = null;

    private WeaponScript weaponScript;

    float timeElapsed;

    private bool tp;

    public override void Awake()
    {
        base.Awake();

        anim = GetComponent<Animator>();

        weaponScript = GetComponentInChildren<WeaponScript>();
        BirdFormDeactivate();
    }
    public override void Update()
    {
        if (!birdForm)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        speed = Mathf.Lerp(speed, 4f, timeElapsed);

        if (speed > 4.0001)
        {
            timeElapsed += Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(mousePos.x, mousePos.y), speed * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(1) && !birdForm && !tp)
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
            GameObject.Find("Wings").GetComponent<Animator>().SetTrigger("WingDown");
        }

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
        lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        rb.rotation = angle;

        weaponScript.gameObject.SetActive(false);
        if (timeElapsed < flyTime)
        {
            float valueToLerp = Mathf.Lerp(0, 1, timeElapsed / flyTime);
            timeElapsed += Time.deltaTime;
            timeForFly.fillAmount = timeElapsed;
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
        rb.rotation = 0;

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
}
