using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    protected Transform  player;
    protected Animator   animator;

    public GameObject bloodSplash;
    public GameObject bloodParticle;

    public float enemySpeed;

    public int   health;
    public float hitForce;

    public bool taunting;
    public bool teleporting;
    public bool ranged;

    [Header("EnemyDrops")]
    public GameObject healthDrop;

/*  [Header("Sounds")]
    [SerializeField] private AudioClip[] enemyGrunt;
    [SerializeField] private float enemyGruntVolume;*/

    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        player   = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void takeDamage(int damage, Transform hitDirection)
    {
        health -= damage;
        transform.Translate(hitDirection.right * hitForce);
        Instantiate(bloodParticle, transform.position + new Vector3(0, 1, 0), transform.rotation);

        if(health <= 0)
        {
            GameManager.Instance.totalKills += 1;
            GameManager.Instance.totalScore += 10;
            Instantiate(bloodSplash, transform.position, Quaternion.identity);
            
            if(healthDrop!= null)
            {
                Instantiate(healthDrop, transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
        }
    }

    public void EnemyFaceDirection()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        Vector3 cross = Vector3.Cross(Vector2.up, direction);

        if (cross.z > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (cross.z < 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }
}
