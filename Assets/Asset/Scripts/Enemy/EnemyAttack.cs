using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public bool targetDetection = false;
    public float bulletSpeed = 50f;
    public float fireCooldown = 1f;

    private Vector2 raycastDirection= Vector2.right;

    private Animator animator;
    public GameObject enemyBullet;
    public Transform firePoint;
    public EnemyController2 enemyController;
    private EnemyHealth enemyHealth;
    public PlayerHealth playerHealth;
    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyController = GetComponent<EnemyController2>();
        
        playerHealth=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    private void Update()
    {
        if (!playerHealth.isDead && !enemyHealth.isDied && Time.timeScale == 1)
        {   
            fireCooldown -= Time.deltaTime;
            CheckPlayer();
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
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position + new Vector3(0.6f, 2, 0), Vector2.right, 20f);
            Debug.DrawRay(transform.position + new Vector3(0.6f, 2, 0), Vector2.right * 20f, Color.red);
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
