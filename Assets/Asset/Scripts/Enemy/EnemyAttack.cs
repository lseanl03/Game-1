using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool canShoot = true;
    public bool targetDetection = false;
    public GameObject enemyBullet;
    public float bulletSpeed=50f;
    public float fireCooldown=1f;
    private Animator animator;
    private EnemyController enemyController;
    public Transform firePoint;
    public PlayerHealth playerHealth;
    private Vector2 raycastDirection= Vector2.right;
    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
    }
    private void Update()
    {
        if (canShoot && Time.timeScale != 0f)
        {
            fireCooldown -= Time.deltaTime;
            CheckPlayer();
        }
        if (playerHealth.currentHealth <= 0)
        {
            canShoot = false;
        }
    }
    void Shoot()
    {        
        if (fireCooldown < 0 && targetDetection)
        {
            FindObjectOfType<AudioManager>().PlaySFX("Attack");
            Invoke("Bullet", 0.15f);
            animator.SetTrigger("attack");
            fireCooldown = 1;
        }
    }
    void Bullet()
    {
        var bullet= Instantiate(enemyBullet, firePoint.position, Quaternion.identity);
        Rigidbody2D rb2d=bullet.GetComponent<Rigidbody2D>();
        if(enemyController.isFacingRight)
        {
            rb2d.velocity = new Vector2(bulletSpeed, 0f);
        }
        else
        {            
            rb2d.velocity = new Vector2(-bulletSpeed, 0f);
        }
    }
    void CheckPlayer()
    {
        if (raycastDirection == Vector2.right)
        {
            if (!enemyController.isFacingRight)
            {
                raycastDirection = Vector2.left;
            }
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position + new Vector3(0.6f, 2, 0), Vector2.right, 50f);
            Debug.DrawRay(transform.position + new Vector3(0.6f, 2, 0), Vector2.right * 50f, Color.red);
            if (hit2D.collider != null && hit2D.collider.CompareTag("Player"))
            {
                targetDetection=true;
                Shoot();
            }
            else
            {
                targetDetection = false;
            }
        }
        else
        {
            if(enemyController.isFacingRight)
            {
                raycastDirection = Vector2.right;
            }
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position + new Vector3(-0.6f, 2, 0), Vector2.left, 50f);
            Debug.DrawRay(transform.position + new Vector3(-0.6f, 2, 0), Vector2.left * 50f, Color.red);
            if (hit2D.collider != null && hit2D.collider.CompareTag("Player"))
            {
                targetDetection = true;
                Shoot();
            }
            else
            {
                targetDetection = false;
            }
        }
    }
}
