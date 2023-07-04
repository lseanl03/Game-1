using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 8f;
    public int currentPoint = 0;
    public bool unlocked = false;
    public bool canStartUp = true;
    public bool canMove = false;
    public bool canChangePoint = false;
    public bool isMoving = false;
    public bool startPoint=false;
    public bool shutDown =false;
    public bool movedToPoint =false;
    public bool onCollisionExit =false;
    public bool canTurnOn =false;

    public Vector2 defaultLocation;
    public enum UseCase { Auto, Manual, Remote }
    public UseCase useCase;

    public Transform[] points;
    private void Start()
    {
        SetPointStart();
    }
    private void Update()
    {
        SwitchCase();
    }
    void SwitchCase()
    {
        if (useCase == UseCase.Auto) //auto move
        {
            AutoMove();
        }
        if (useCase == UseCase.Manual) //manual move (need collision)
        {
            ManualMove();
        }
        if (useCase == UseCase.Remote)//remote move (need switch)
        {
            RemoteMove();
        }
    }
    void AutoMove()
    {
        if (startPoint)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, points[1].position, moveSpeed * Time.deltaTime); //move to point 1
        }
        else
        {
            transform.position =
                Vector3.MoveTowards(transform.position, points[0].position, moveSpeed * Time.deltaTime);  //move to point 0
        }
        if (transform.position == points[0].position)
        {
            startPoint = true;
        }
        if (transform.position == points[1].position)
        {
            startPoint = false;
        }
    }
    void ManualMove()
    {
        unlocked = true;
        Move();
        ChangePoint();
    }
    void RemoteMove()
    {
        if (unlocked)
        {
            Debug.Log("unlocked");
            StartUp();
            Move();
            ChangePoint();
            Lock();
        }
        if (!unlocked)
        {
            if(canTurnOn)
            {
                PlatformSwitch[] switches = FindObjectsOfType<PlatformSwitch>();
                foreach (PlatformSwitch switchObj in switches)
                {
                    if (switchObj.turnOn)
                    {
                        switchObj.transform.GetComponent<Interactable>().Interact();
                        Debug.Log("Interact");
                    }
                    else
                    {
                        canTurnOn = false;
                    }
                }
            }
        }
    }
    void SetPointStart()
    {
        if(useCase == UseCase.Auto || useCase==UseCase.Manual)
        {
            defaultLocation = points[0].position;
            transform.position = defaultLocation;
        }
        if(useCase == UseCase.Remote)
        {
            defaultLocation = Vector2.Lerp(points[0].position, points[1].position, 0.5f);
            transform.position = defaultLocation;
        }
    }
    void StartUp()
    {
        if (canStartUp)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);
            if (transform.position == points[currentPoint].position)
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
                Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);
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
                    movedToPoint = true;
                }
            }
            else
            {
                if (Vector2.Distance(transform.position, points[currentPoint].position) < 0.1f)
                {
                    currentPoint--;
                    canChangePoint = false;
                    movedToPoint = true;
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
            onCollisionExit = false;
            if (unlocked)
            {
                canMove = true;
                if (!isMoving)
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
            onCollisionExit = true;
            if (unlocked)
            {
                if (isMoving)
                {
                    canChangePoint = false;
                }
                else
                {
                    canMove = false;
                }
            }
            collision.transform.SetParent(null);
        }
    }
    void Lock()
    {
        if (!isMoving && onCollisionExit)
        {
            Debug.Log("set unlock = false");
            unlocked = false;
            canTurnOn = true;
            onCollisionExit = false;
        }
    }
}
