using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    private float stoppingDistanceMin = 2.5f;
    private float stoppingDistanceMax = 3.5f;

    public float   stoppingDistance;
    public float waitForAttack; 

    private bool  readyToAttack;
    private float timeBtwAttack = 0;

    public override void Start()
    {
        base.Start();
        stoppingDistance = Random.Range(stoppingDistanceMin, stoppingDistanceMax);
    }

    private void Update()
    {
        EnemyFaceDirection();
        EnemyMovement();
        
        if(!GameManager.Instance.gameOver) {
            Attack();
        }
    }

    private void EnemyMovement()
    {
        if (Vector2.Distance(player.position, transform.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime);
            animator.SetBool("isWalking", true);
            readyToAttack = false;
        }
        else
        {
            animator.SetBool("isWalking", false);
            readyToAttack = true;
        }
    }

    private void Attack()
    {
        if(readyToAttack)
        {
            if (timeBtwAttack <= 0)
            {
                animator.SetTrigger("Attack");
                timeBtwAttack = waitForAttack;
            }
            else
            {
                if (taunting)
                {
                    animator.SetTrigger("Taunting");
                }
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }
}
