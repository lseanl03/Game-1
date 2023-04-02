using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerEnemyBullet : MonoBehaviour
{
    private Vector2 startPos;
    private float bulletMoved;
    public float distance = 20f;
    public GameObject explosionPrefab;
    public int damage = 2;

    private void Start()
    {
        this.startPos = transform.position;
    }

    private void Update()
    {
        DestroyBullet();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.PlayerTakeDamage(damage);
        }
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.EnemyTakeDamage(damage);
        }
        explosionPrefab = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    void DestroyBullet()
    {
        bulletMoved = Vector2.Distance(this.startPos, transform.position);
        if (bulletMoved >= distance)
        {
            Destroy(gameObject);
        }
    }
}
