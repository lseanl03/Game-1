using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    public GameObject[] movingPlatform;
    public Transform[] points;
    public int currentPoint;
    public float moveSpeed;
    public bool canMove=false;
    public bool startMove=false;
    private void Start()
    {
        currentPoint = 0;
        moveSpeed = 8f;
    }
    private void Update()
    {
        if (canMove)
        {
            CheckPoint();
            ChangePoint();
        }

    }
    void CheckPoint()
    {
        if (points[currentPoint] != null)
        {
            Move();
        }
    }
    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);
    }
    void ChangePoint()
    {
        if (Vector2.Distance(transform.position, points[currentPoint].position) < 0.1f)
        {
            moveSpeed = 0f;
            if (startMove) {
                moveSpeed = 8f;
                if (currentPoint == 0)
                {
                    currentPoint++;
                }
                else
                {
                    currentPoint--;
                }
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            startMove = true;
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            startMove = false;
            collision.transform.SetParent(null);
        }
    }
}
