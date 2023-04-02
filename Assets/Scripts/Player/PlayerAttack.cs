using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public BulletSpawn bulletSpawn;
    public float attackCountdown=1f;
    private void Start()
    {
        animator=GetComponent<Animator>();
    }
    private void Update()
    {
        attackCountdown += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && attackCountdown>=1f)
        {
            Attack();
            attackCountdown=0;
        }
        else
        {
            return;
        }
        if (attackCountdown >= 5f)
        {
            attackCountdown = 1f;
        }
    }
    void Attack()
    {

        animator.SetTrigger("attack");
        Invoke("Shoot", 0.15f);
    }
    void Shoot()
    {
        bulletSpawn.Bullet();
    }

}
