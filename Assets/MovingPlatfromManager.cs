using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingPlatfromManager : MonoBehaviour
{
    public int currentPoint;
    public float moveSpeed;

    public bool unlocked=false;
    public bool canMove = false;
    public bool canChangePoint = false;
    public bool isMoving=false;

    public Transform[] points;

    private void Start()
    {
        currentPoint = 0;
        moveSpeed = 8f;
        transform.position = points[currentPoint].position;
    }
    private void Update()
    {
        if (unlocked)
        {
            Move();
            ChangePoint();
        }
    }
    void Move()
    {
        if (points[currentPoint] != null && canMove)
        {
            if (Vector2.Distance(transform.position, points[currentPoint].position) >= 0.1f)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
            transform.position = 
                Vector2.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);
        }   
    }
    void ChangePoint()
    {
        if(canChangePoint)
        {
            if (currentPoint == 0)
            {
                if (Vector2.Distance(transform.position, points[currentPoint].position) < 0.1f)
                {
                    currentPoint++;
                    canChangePoint = false;
                }
            }
            else
            {
                if (Vector2.Distance(transform.position, points[currentPoint].position) < 0.1f)
                {
                    currentPoint--;
                    canChangePoint = false;
                }
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (unlocked)
            {
                canMove = true;
                if(!isMoving)
                {
                    canChangePoint = true;
                }                
            }
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (unlocked)
            {
                canMove = false;
                if (isMoving)
                {
                    canChangePoint = false;
                }
            }
            collision.transform.SetParent(null);
        }
    }
}
