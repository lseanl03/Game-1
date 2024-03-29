﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int currentPoint = 1;
    public float enemyMove;
    public float waitTime = 0f;
    public float moveSpeed = 10f;
    private float pointPos;
    private float currentPos;

    public bool isFacingRight = true;
    private bool canMove = true;
    private bool canFlip = true;
    public bool isDied = false;

    private Rigidbody2D rb2d;
    private Animator animator;

    [SerializeField] private Transform[] points;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        CheckDied();
        if (!isDied && canMove && Time.timeScale == 1)
        {
            pointPos = points[currentPoint].transform.position.x;
            currentPos = transform.position.x;
            enemyMove = Mathf.Abs(pointPos - currentPos);
            Move();
            CheckGround();
        }
    }
    void Move()
    {
        if (isFacingRight)
        {
            
            rb2d.velocity = new Vector2(moveSpeed, 0);
            animator.SetBool("isRun", true);
            if (enemyMove < 0.5f)
            {
                moveSpeed = 0;
                waitTime += Time.deltaTime;
                animator.SetBool("isRun", false);
                if (waitTime > 2f)
                {
                    Flip();
                    isFacingRight = false;
                    moveSpeed = 10f;
                    currentPoint = 0;
                    waitTime = 0f;
                }
            }
        }
        else
        {           
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            enemyMove = Mathf.Abs(pointPos - currentPos);
            animator.SetBool("isRun", true);
            if (enemyMove < 0.5f)
            {
                moveSpeed = 0;
                waitTime += Time.deltaTime;
                animator.SetBool("isRun", false);
                if (waitTime > 2f)
                {
                    Flip();
                    isFacingRight = true;
                    moveSpeed = 10f;
                    currentPoint = 1;
                    waitTime = 0f;
                }
            }
        }
    }
    void Flip()
    {
        if (canFlip)
        {
            isFacingRight= !isFacingRight;
            Vector3 newScale= transform.localScale;
            newScale.x *= -1f;
            transform.localScale=newScale;
        }
    }
    void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        Debug.DrawRay(transform.position, Vector2.down * 1f, Color.red);
        if(hit.collider != null)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
    }
    void CheckDied()
    {
        EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
        if (enemyHealth.currentHealth <= 0)
        {
            isDied = true;
        }
    }
}
