using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiMovingPlatform : MonoBehaviour
{
    public int currentPoint;
    public float moveSpeed;

    public bool unlocked = false;
    public bool canStartUp = true;
    public bool canMove = false;
    public bool canChangePoint = false;
    public bool isMoving = false;

    public Vector2 defaultLocation;

    public Transform[] points;
    private void Start()
    {
        currentPoint = 0;
        moveSpeed = 8f;
        SetDefaultPoint();
    }
    private void Update()
    {
        if (unlocked)
        {
            StartUp();
            Move();
            ChangePoint();
        }
    }
    void SetDefaultPoint()
    {
        defaultLocation = points[0].transform.position;
        transform.position = defaultLocation;
    }
    void StartUp()
    {
        if (canStartUp)
        {
            transform.position =
                Vector2.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, points[currentPoint].position) < 0.1f)
            {
                canStartUp = false;
            }
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
        if (canChangePoint)
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
            canMove = true;
            if (!isMoving)
            {
                canChangePoint = true;
            }
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canMove = false;
            if (isMoving)
            {
                canChangePoint = false;
            }
            collision.transform.SetParent(null);
        }
    }
}
