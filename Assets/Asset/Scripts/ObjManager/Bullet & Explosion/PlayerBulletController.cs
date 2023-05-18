using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    private Vector2 startPos;
    private float bulletMoved;
    public float distance = 50f;
    public int damage = 2;
    public GameObject explosionPrefab;
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
        if (collision.CompareTag("Enemy") && collision.GetComponent<EnemyHealth>())
        {
            collision.GetComponent<EnemyHealth>().EnemyTakeDamage(damage);
        }
        explosionPrefab = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(0f);
    }
    void DestroyBullet()
    {
        bulletMoved = Vector2.Distance(this.startPos, transform.position);
        if (bulletMoved >= distance)
        {
            Destroy(0f);
        }
    }
    void Destroy(float time)
    {
        Destroy(gameObject,time);
    }
}
