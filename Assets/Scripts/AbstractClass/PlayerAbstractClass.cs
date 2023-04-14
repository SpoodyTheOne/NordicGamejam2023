using UnityEngine;

public abstract class Player : MonoBehaviour
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
    Vector2 movement;
    Vector2 mousePos;
    Vector2 lookDir;
    #endregion
    #region FloatVar
    [Header("PlayerVariables")]
    public float speed = 0;
    public float health = 0;
    public float damage = 0;
    [Space(10)]
    private float keptSpeed;
    #endregion
    #region BoolVar
    private bool
        m1,
        m2;
    #endregion
    
    public virtual void Awake()
    {
        #region SetComponents

        playerSprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        #endregion
    }

    public virtual void Update()
    {
        #region Inputs
        m1 = Input.GetMouseButtonDown(0);
        m2 = Input.GetMouseButtonDown(1);

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        #endregion
    }

    public virtual void FixedUpdate()
    {
        #region Movement
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        #endregion
        #region Rotation
        lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        rb.rotation = angle;
        #endregion
    }
}