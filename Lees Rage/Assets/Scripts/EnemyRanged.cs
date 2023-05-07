using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{
    public GameObject _rangedWeapon;

    private int stoppingDistanceMin = 15;
    private int stoppingDistanceMax = 20;

    public int   stoppingDistance;
    public float rotationSpeed;
    public float waitForThrow;
    public LayerMask layerMask;

    private bool  inRange            = false;
    private bool  inDirection        = false;
    private float timeBtwThrow       = 0;
    private Transform throwPoint;

    public override void Start()
    {
        base.Start();
        stoppingDistance = Random.Range(stoppingDistanceMin, stoppingDistanceMax);
        throwPoint = transform.GetChild(1);
    }

    private void Update()
    {
        EnemyFaceDirection();
        CheckForDirection();
        EnemyMovement();
        
        if(!GameManager.Instance.gameOver) {
            ThrowWeapon();
        }
    }

    private void EnemyMovement()
    {
        if (Vector2.Distance(player.position, transform.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime);
            animator.SetBool("isWalking", true);
            inRange = false;
        }
        else
        {
            animator.SetBool("isWalking", false);
            inRange = true;
        }
    }

    private void CheckForDirection()
    {
        RaycastHit2D hit = Physics2D.Raycast(throwPoint.position, throwPoint.right, 20, layerMask);
        Debug.DrawRay(throwPoint.position, throwPoint.right * 20, inDirection ? Color.yellow : Color.green);

        Vector2 displacement = player.position - transform.position;

        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        throwPoint.rotation = Quaternion.Slerp(throwPoint.rotation, rotation, rotationSpeed * Time.deltaTime);

        if (hit)
        {
            inDirection = true;
        }
        else
        {
            inDirection = false;
        }
    }

    private void ThrowWeapon()
    {
        if(inDirection && inRange)
        {
            if(timeBtwThrow <= 0)
            {
                animator.SetTrigger("Attack");
                Instantiate(_rangedWeapon, throwPoint.position, throwPoint.rotation);
                timeBtwThrow = waitForThrow;
            }
            else
            {
                timeBtwThrow -= Time.deltaTime;
            }
        }
        else if(!inDirection && inRange && taunting)
        {
            animator.SetTrigger("Taunting");
        }
    }
}
