using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleportTesting : EnemyControllerTesting
{
    public bool canSpawn = false;
    public bool isTeleport = true;
    [SerializeField] private bool flipped = false;


    [SerializeField] private int currentPoint = 1;
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private float targetMoveSpeed =7f;
    [SerializeField] private float distance;

    [SerializeField] private Transform wayPointObj;
    [SerializeField] private GameObject teleportTarget;
    private GameObject teleportTargetObj;

    public List<Transform> points;

    protected override void Start()
    {
        base.Start();
        GetWayPoints();
        transform.position = points[0].position;
        moveSpeed = 7f;
    }
    protected override void Update()
    {
        base.Update();
        if (!enemyHealth.isDied)
        {
            Move();
        }
    }
    void GetWayPoints()
    {
        wayPointObj = transform.parent.Find("WayPoints");
        foreach (Transform point in wayPointObj)
        {
            points.Add(point);
        }
    }
    void Move()
    {
        float enemyPos = transform.position.x;
        float pointPos = points[currentPoint].position.x;
        float distance = Mathf.Abs(pointPos - enemyPos);
        if(isTeleport)
        {
            if (!playerFound)
            {
                if (distance > 0.5f)
                {
                    rb2d.velocity = isFacingRight ? new Vector2(moveSpeed, rb2d.velocity.y) : new Vector2(-moveSpeed, rb2d.velocity.y);
                    moveSpeed = 7f;
                    animator.SetBool("isRun", true);
                }
                else
                {
                    if (currentPoint == 0 || currentPoint == points.Count - 1)
                    {
                        waitTime -= Time.deltaTime;
                        if (waitTime <= 0)
                        {
                            Flip();
                            animator.SetBool("isRun", true);
                            currentPoint += isFacingRight ? 1 : -1;
                            moveSpeed = Random.Range(5f, 7f);
                            waitTime = 2;
                        }
                        else
                        {
                            moveSpeed = 0;
                            animator.SetBool("isRun", false);
                        }
                    }
                    else
                    {
                        moveSpeed = 0;
                        animator.SetBool("isRun", false);
                        canSpawn = true;
                        SpawnTarget();
                        TargetMove();
                        TeleportActive();
                    }
                }
                if (isFacingRight)
                    flipped = false;
                else
                    flipped = true;
            }
            else
            {
                isTeleport = false;
            }
        }
        else
        {
            moveSpeed = 0;
            animator.SetBool("isRun", false);
            if (playerFound)
            {
                waitTime = 2;
            }
            else
            {
                waitTime -= Time.deltaTime;
                if (waitTime < 0)
                {
                    if(isFacingRight != !flipped)
                    {
                        Flip();
                    }
                    else if(!isFacingRight != flipped)
                    {
                        Flip();
                    }
                    animator.SetBool("isRun", true);
                    moveSpeed = Random.Range(7f, 10f);
                    waitTime = 2;
                    isTeleport = true;
                }
            }
        }

    }
    void SpawnTarget()
    {
        if (canSpawn && teleportTargetObj ==null)
        {
            teleportTargetObj =
            Instantiate(teleportTarget, transform.position, teleportTarget.transform.rotation);
            canSpawn = false;
        }
    }
    void SetParent()
    {
        teleportTargetObj.transform.parent = transform;
    }
    void TargetMove()
    {
        if (teleportTargetObj != null)
        {
            SetParent();
            if (isFacingRight)
            {
                teleportTargetObj.transform.position =
                    Vector2.MoveTowards(teleportTargetObj.transform.position, points[currentPoint + 1].transform.position, targetMoveSpeed * Time.deltaTime);
            }
            else
            {
                teleportTargetObj.transform.position =
                    Vector2.MoveTowards(teleportTargetObj.transform.position, points[currentPoint - 1].transform.position, targetMoveSpeed * Time.deltaTime);
            }
        }
    }
    void TeleportActive()
    {
        if (isFacingRight && Vector2.Distance(teleportTargetObj.transform.position, points[currentPoint + 1].transform.position) < 0.1f)
        {
            gameObject.transform.position = teleportTargetObj.transform.position;
            currentPoint = currentPoint + 1;
            DestroyTarget();
        }
        else if (!isFacingRight && Vector2.Distance(teleportTargetObj.transform.position, points[currentPoint - 1].transform.position) < 0.1f)
        {
            gameObject.transform.position = teleportTargetObj.transform.position;
            currentPoint = currentPoint - 1;
            DestroyTarget();
        }
        else
        {
            moveSpeed = 0;
        }
    }
    void DestroyTarget()
    {
        if (teleportTargetObj != null)
        {
            ChangePoint();
            Destroy(teleportTargetObj);
            canSpawn = true;
        }
    }
    void ChangePoint()
    {
        if (distance < 0.1f)
        {
            if (isFacingRight)
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
