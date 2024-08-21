using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform target;

    public float dame;
    public float speed;

    private void Awake()
    {
        Destroy(gameObject, 2f);
    }

    private void Update()
    {
        if (!target)
        {
            return;
        }

        Vector2 dir = (target.position - transform.position).normalized;
        rb.velocity = dir * speed;
        
    }
    public void SetDamage(float newDame)
    {
        dame = newDame;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
