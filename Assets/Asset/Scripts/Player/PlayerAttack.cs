using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float timeBetweenAttack=0f;
    public float attackTime=1f;
    public float bulletSpeed = 80f;

    public Animator animator;
    public GameObject bullet;
    public Transform firePoint;
    public PlayerController playerController;
    public PlayerInteract playerInteract;
    private void Start()
    {
        animator=GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        playerInteract=GetComponent<PlayerInteract>();
    }
    private void Update()
    {
        timeBetweenAttack += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && timeBetweenAttack >= attackTime 
            && Time.timeScale == 1 && !playerInteract.interacting)
        {
            Attack();
            timeBetweenAttack = 0f;
        }
        if (timeBetweenAttack >= 5f)
        {
            timeBetweenAttack = 1f;
        }
    }
    void Attack()
    {
        FindObjectOfType<AudioManager>().PlaySFX("Attack");
        animator.SetTrigger("attack");
        Invoke("Shoot", 0.15f);
    }
    void Shoot()
    {
        GameObject bulletPrefab = Instantiate(bullet, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bulletPrefab.GetComponent<Rigidbody2D>();
        if (playerController.isFacingRight)
        {
            bulletRb.velocity = new Vector2(bulletSpeed, 0f);
        }
        else
        {
            bulletRb.velocity = new Vector2(-bulletSpeed, 0f);
        }

    }


}
