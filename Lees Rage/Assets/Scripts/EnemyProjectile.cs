using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyProjectile : MonoBehaviour
{
    private Rigidbody2D _rb;

    public float lifeTime;

    private void Awake()
    {
        _rb= GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rb.velocity = transform.right * GameManager.Instance.rangedWeaponSpeed;
        Invoke("DestroyProjectile", lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().takeDamage(GameManager.Instance.bulletDamage);
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
