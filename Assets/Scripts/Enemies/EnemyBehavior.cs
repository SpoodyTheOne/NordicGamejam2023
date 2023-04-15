using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyBehavior : MonoBehaviour
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

    private GameObject[] players;
    private GameObject currentPlayerToTarget;
    private float lastDistance;

    private void Start()
    {
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

        animator.SetFloat("MoveX", aIPathScript.velocity.x);
        animator.SetFloat("MoveY", aIPathScript.velocity.y);

        if (aIPathScript.canMove == true)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        FindPlayerToTarget();
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
    }
}
