using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTesting : MonoBehaviour
{
    public float bulletSpeed = 50f;
    public float attackCountDown = 1f;

    private Animator animator;

    public GameObject enemyBullet;
    public Transform firePoint;
    private EnemyControllerTesting enemyControllerTesting;
    private EnemyHealth enemyHealth;
    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyControllerTesting = GetComponent<EnemyControllerTesting>();
        enemyHealth = GetComponent<EnemyHealth>();
    }
    private void Update()
    {
        if (!enemyHealth.isDied)
        {
            if (attackCountDown >= 0)
            {
                attackCountDown -= Time.deltaTime;
            }
            AttackActive();
        }
    }
    void AttackActive()
    {
        if (enemyControllerTesting.playerFound)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        if (attackCountDown < 0 )
        {
            FindObjectOfType<AudioManager>().PlaySFX("Attack");
            attackCountDown = 1f;
            Invoke("Bullet", 0.15f);
            animator.SetTrigger("attack");
        }
    }
    void Bullet()
    {
        GameObject bullet = Instantiate(enemyBullet, firePoint.position, Quaternion.identity);
        Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();
        if (enemyControllerTesting.isFacingRight)
        {
            rb2d.velocity = new Vector2(bulletSpeed, 0f);
        }
        else
        {
            rb2d.velocity = new Vector2(-bulletSpeed, 0f);
        }
    }
}
