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

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3);

        foreach(var col in colliders)
        {
            Vector2 dir = col.transform.position - transform.position;
            col.GetComponent<Rigidbody2D>().AddForce(dir * 10f);
        }

        Destroy(gameObject);
    }

    private void OnParticleCollision(GameObject other)
    {
        TakeDamage(GameObject.Find("Harry Musketeer"), 10f);
    }
}
