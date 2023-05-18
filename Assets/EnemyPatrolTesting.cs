using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolTesting : EnemyControllerTesting
{
    [SerializeField] private int currentPoint = 0;
    [SerializeField] private float waitTime = 2f;
    public bool isPatrol = true;
    [SerializeField] private Transform[] points;
    protected override void Start()
    {
        base.Start();
        transform.position = points[0].position;
    }
    protected override void Update()
    {
        base.Update();
        if(!enemyHealth.isDied)
        {
            Move();
        }
    }
    void Move()
    {
        float enemyPos = transform.position.x;
        float pointPos = points[currentPoint].position.x;
        float distance = Mathf.Abs(pointPos - enemyPos);
        rb2d.velocity = isFacingRight ? new Vector2(moveSpeed, rb2d.velocity.y) : new Vector2(-moveSpeed, rb2d.velocity.y);
        animator.SetBool("isRun", true);
        if (isPatrol)
        {
            if (!playerFound)
            {

                if (distance < 0.5f)
                {
                    moveSpeed = 0f;
                    animator.SetBool("isRun", false);
                    waitTime -= Time.deltaTime;
                    if (waitTime < 0)
                    {
                        Debug.Log("distance < 0.1f");
                        currentPoint = isFacingRight ? 0 : 1;
                        moveSpeed = Random.Range(7f, 10f);
                        waitTime = Random.Range(1f, 2f);
                        Flip();
                    }
                }
                else
                {
                    if (enemyPos > pointPos)
                    {
                        if(isFacingRight)
                        {
                            Debug.Log("=");
                            Flip();
                        }
                        else
                        {
                            moveSpeed = Random.Range(7f, 10f);
                        }
                    }
                    else if(enemyPos < pointPos)
                    {
                        if (!isFacingRight)
                        {
                            Debug.Log("!");
                            Flip();
                        }
                        else
                        {
                            moveSpeed = Random.Range(7f, 10f);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                isPatrol = false;
            }
        }
        else
        {
            GameObject player = GameObject.Find("Player");
            float playerPos = player.transform.position.x;
            float distance1 = Mathf.Abs(playerPos - enemyPos);
            if(canMove)
            {
                if (distance1 >= rayDistance / 2)
                {
                    moveSpeed = 10f;
                    if (playerFound)
                    {
                        waitTime = Random.Range(1f, 2f);
                    }
                    else
                    {
                        moveSpeed = 0f;
                        animator.SetBool("isRun", false);
                        waitTime -= Time.deltaTime;
                        if (waitTime < 0)
                        {
                            animator.SetBool("isRun", true);
                            float direction1 = Vector2.Distance(transform.position, points[0].transform.position);
                            float direction2 = Vector2.Distance(transform.position, points[1].transform.position);
                            currentPoint = direction1 < direction2 ? 1 : 0;
                            moveSpeed = Random.Range(7f, 10f);
                            waitTime = Random.Range(1f, 2f);
                            isPatrol = true;
                        }
                    }
                }
                else
                {
                    if (playerFound)
                    {
                        waitTime = Random.Range(1f, 2f);
                        animator.SetBool("isRun", false);
                        moveSpeed = 0f;
                    }
                    else
                    {
                        waitTime -= Time.deltaTime;
                        if (waitTime < 0)
                        {
                            animator.SetBool("isRun", true);
                            float direction1 = Vector2.Distance(transform.position, points[0].transform.position);
                            float direction2 = Vector2.Distance(transform.position, points[1].transform.position);
                            currentPoint = direction1 < direction2 ? 1 : 0;
                            moveSpeed = Random.Range(7f, 10f);
                            waitTime = Random.Range(1f, 2f);
                            isPatrol = true;
                        }
                    }
                }
            }
            else
            {
                moveSpeed = 0;
                animator.SetBool("isRun", false);
                waitTime -= Time.deltaTime;
                if (waitTime < 0)
                {
                    //animator.SetBool("isRun", true);
                    //float direction1 = Vector2.Distance(transform.position, points[0].transform.position);
                    //float direction2 = Vector2.Distance(transform.position, points[1].transform.position);
                    //currentPoint = direction1 < direction2 ? 1 : 0;
                    //moveSpeed = Random.Range(7f, 10f);
                    waitTime = Random.Range(1f, 2f);
                    isPatrol = true;
                }
            }
        }
    }
}
