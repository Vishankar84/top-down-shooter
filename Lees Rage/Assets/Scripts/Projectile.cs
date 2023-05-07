using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;

    public  float       lifeTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.velocity = transform.right * GameManager.Instance.bulletSpeed;
        Invoke("DestroyProjectile", lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().takeDamage(GameManager.Instance.bulletDamage, this.transform);
            DestroyProjectile();
        }        
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
