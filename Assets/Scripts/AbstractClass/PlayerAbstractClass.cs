using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public abstract class Player : IDamagable
{
    #region Essentials
    [HideInInspector] public SpriteRenderer playerSprite;
    [HideInInspector] public Rigidbody2D rb;
    #endregion
    #region ColorVar
    [Header("Color")]
    public Color normalColor;
    [Space(10)]
    #endregion
    #region Vector2Var
    [HideInInspector] public Vector2 movement;
    [HideInInspector] public Vector2 movementInput;
    [HideInInspector] public Vector2 mousePos;
    [HideInInspector] public Vector2 lookDir;
    #endregion
    #region FloatVar
    [Header("PlayerVariables")]
    public float speed = 0;
    public float damage = 0;
    [Space(10)]
    private float keptSpeed;
    #endregion
    #region RosemaryMeter
    public float maxSpice = 100;
    public float currentSpice = 100;

    public float specialCost, commonCost;
    public bool continouosConsumption;

    public float specialCooldown, commonCooldown;
    public Image rosemary;
    #endregion

    public Image healthBar;
    public float maxHealth;

    [HideInInspector] public Animator anim;
    public ParticleSystem pfx;

    public Transform virtualCursor;
    public Vector2 virtualMousePos;
    public PlayerInput playerInput;
    public bool Gamepad;

    public virtual void Awake()
    {
        #region SetComponents

        anim = GetComponent<Animator>();

        playerSprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // IDamagable
        UseHearts = true;

        #endregion
        maxHealth = Health;
    }

    public virtual void Update()
    {

        #region Inputs
        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");
        movement.x = movementInput.x;
        movement.y = movementInput.y;

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float tempSpice = currentSpice / 100f;

        float tempHealth = (1 / maxHealth) * Health;

        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, tempHealth, 10f * Time.deltaTime);

        rosemary.fillAmount = tempSpice;
        rosemary.GetComponentInChildren<TextMeshProUGUI>().text = currentSpice + " / " + maxSpice;

        #endregion
        #region Animation
        if (movement.magnitude > 0)
            anim.SetBool("Walking", true);
        else
        {
            pfx.loop = false;
            anim.SetBool("Walking", false);
        }
        #endregion
    }

    public virtual void FixedUpdate()
    {
        #region Movement
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        #endregion
        #region Rotation
        //lookDir = mousePos - rb.position;
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        //rb.rotation = angle;
        #endregion
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
        Debug.Log("2");
    }

    public override bool TakeDamage(GameObject attacker, float amount)
    {


        bool d = base.TakeDamage(attacker, amount);

        return d;
    }
}