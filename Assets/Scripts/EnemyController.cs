using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator animator;
    public float enemyHP;
    public int value;

    public List<Vector3> waypoints;
    public float speed = 10f;
    int index;

    private void Awake()
    {
        waypoints = LevelManager.Instance.wayPointSOs[0].points;
        index = 0;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[index], speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, waypoints[index]) <= 0.01f)
        {
            index++;
            if (index >= waypoints.Count)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")) 
        {
            animator.SetTrigger("Hit");
            enemyHP -= collision.GetComponent<Bullet>().dame;

            if (enemyHP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        EnemySpawner.onEnemyDestroy.Invoke();
        GameManager.Instance.totalCoin += value;
    }
}
