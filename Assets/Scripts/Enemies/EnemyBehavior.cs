using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyBehavior : IDamagable
{
    public AIPath aIPathScript;
    public float desiredDistanceToPlayer;
    protected float currentDistanceToPlayer;
    public float timeTillMoveAgain;
    private float lastMove;
    public bool intervalMovement;
    public bool animated;
    public Animator animator;
    public AIDestinationSetter destinationSetter;
    public PointTowardsTarget ptt;

    private GameObject[] players;
    private GameObject currentPlayerToTarget;
    [SerializeField] private ParticleSystem pfx;
    [SerializeField] private GameObject deadLilguy;
    private float lastDistance;

    public EnemySpawner spawner;

    private void Start()
    {
        animator.SetBool("isMoving", true);
        players = GameObject.FindGameObjectsWithTag("Player");
        FindPlayerToTarget();
    }

    protected void Update()
    {
        currentDistanceToPlayer = Vector3.Distance(currentPlayerToTarget.transform.position, transform.position);
        
        if(!intervalMovement && currentDistanceToPlayer <= desiredDistanceToPlayer)
        {
            aIPathScript.canMove = false;
            lastMove = Time.time;
        }

        if (!intervalMovement && Time.time - lastMove > timeTillMoveAgain)
        {
            if (currentDistanceToPlayer >= desiredDistanceToPlayer)
            {
                aIPathScript.canMove = true;
            }
        }

        if (intervalMovement == true)
        {
            if (Time.time - lastMove > timeTillMoveAgain)
            {
                if (aIPathScript.canMove == false)
                {
                    aIPathScript.canMove = true;
                }
                else
                {
                    aIPathScript.canMove = false;
                }

                lastMove = Time.time;
            }
        }

        //if (animator != null)
        //{
        //    animator.SetFloat("MoveX", aIPathScript.velocity.x);
        //    animator.SetFloat("MoveY", aIPathScript.velocity.y);

        //    if (aIPathScript.canMove == true)
        //        animator.SetBool("isMoving", true);
        //    else
        //        animator.SetBool("isMoving", false);
        //}

        FindPlayerToTarget();
    }

    public void Stun(float stunTime)
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        pfx.Play();
        pfx.loop = true;

        GetComponentInChildren<PointTowardsTarget>().enabled = false;
        GetComponentInChildren<InstantiateOverTime>().enabled = false;
        animator.SetBool("isMoving", false);

        Invoke("UnStun", stunTime);
    }
    private void UnStun()
    {
        pfx.loop = false;

        GetComponentInChildren<PointTowardsTarget>().enabled = true;
        GetComponentInChildren<InstantiateOverTime>().enabled = true;
        animator.SetBool("isMoving", true);

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void FindPlayerToTarget()
    {
        for (int i = 0; i < players.Length; i++)
        {
            float currentDistance = Vector3.Distance(players[i].transform.position, this.transform.position);
            if (currentDistance > lastDistance)
            {
                currentPlayerToTarget = players[i];
            }
            lastDistance = currentDistance;
        }

        destinationSetter.target = currentPlayerToTarget.transform;
        ptt.target = currentPlayerToTarget.transform;
    }

    public override void Die()
    {
        base.Die();
        
        spawner.OnEnemyDied(gameObject);

        var obj = Instantiate(deadLilguy, transform.position, Quaternion.identity);
        Destroy(obj, 6f);

        Destroy(gameObject);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3);

        foreach(var col in colliders)
        {
            Vector2 dir = col.transform.position - transform.position;
            if (col.GetComponent<Rigidbody2D>())
                col.GetComponent<Rigidbody2D>().AddForce(dir * 10f);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        TakeDamage(GameObject.Find("Harry Musketeer"), 10f);
    }
    public override bool TakeDamage(GameObject attacker, float Amount)
    {
        // Check if we have iframes
        if (IFrameTime > Time.time)
            return false; // Return false to indicate a non-successful hit

        animator.SetTrigger("Damage");
        GetComponentInChildren<AudioSource>().Play();

        // Update IFrameTime so we have iframes for this long
        IFrameTime = Time.time + IFrameSeconds;

        // Remove decimal numbers in case we want to use a hearts system instead
        float DamageAmount = Amount;
        if (UseHearts)
            DamageAmount = Mathf.Floor(Amount);

        // Do the damage
        this._Health -= DamageAmount;

        // Check if the gameobject has a BuffManager
        BuffManager buffManager = GetComponent<BuffManager>();
        if (buffManager)
            buffManager.OnDamaged(); // Trigger buff manager OnDamaged() event

        // Die if health is 0
        if (this._Health <= 0)
            this.Die();

        // Return true to indicate a successful hit
        return true;
    }
}
