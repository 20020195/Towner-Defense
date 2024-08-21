using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FarmerController : MonoBehaviour
{
    public int level;
    public int cost;
    public float dame;
    public float rateOfFire;

    public GameObject bullet;
    public Transform bulletSpawnPos;

    [SerializeField]
    private float radius;
    [SerializeField]
    private LayerMask enemyLayer;

    private GameObject enemy;
    private float nextFireTime;

    private void Update()
    {
        if (enemy == null)
        {
            Collider2D[] results = new Collider2D[1];
            int numColliders = Physics2D.OverlapCircleNonAlloc(transform.position, radius, results, enemyLayer);
            if (numColliders > 0)
            {
                enemy = results[0].gameObject;
            }
        } else
        {
            if (Vector2.Distance(transform.position, enemy.transform.position) > radius)
            {
                enemy = null;
                return;
            }

            if (Time.time >= nextFireTime)
            {
                ShootAtEnemy();
                nextFireTime = Time.time + rateOfFire;
            }
        }
    }

    private void ShootAtEnemy()
    {
        GameObject spawnedBullet = Instantiate(bullet, bulletSpawnPos.position, Quaternion.identity);

        Vector2 direction = (enemy.transform.position - bulletSpawnPos.position).normalized;
        spawnedBullet.transform.up = direction;

        spawnedBullet.GetComponent<Bullet>().SetDamage(dame);

        spawnedBullet.GetComponent<Bullet>().target = enemy.transform;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
