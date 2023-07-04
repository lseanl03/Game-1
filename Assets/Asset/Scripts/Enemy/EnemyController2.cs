using Unity.VisualScripting;
using UnityEngine;

public enum MoveType{
    Idle,
    Patrol,
    Run,
    Teleport,
}
public class EnemyController2 : MonoBehaviour
{
    [SerializeField] private MoveType moveType;
    [SerializeField] private Transform[] points;
    [SerializeField] private GameObject wayPointObj;

    [SerializeField] private GameObject teleportTarget;
    private GameObject teleportTargetObj;
    private Rigidbody2D rb2d;
    private Animator animator;


    public int currentPoint = 0;
    public float speed = 10;
    public float countdown=2;

    public bool canSpawn = true;
    public bool isFacingRight = true;
    public bool isDied = false;


    private void Start()
    {
        transform.position = points[0].position;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        CheckDied();
        if (!isDied)
        {
            ChangeActive();
            ChangeAnimation();
            ChangeMoveType();
        }
    }
    void ChangeAnimation()
    {
        if (rb2d.velocity == Vector2.zero || speed == 0)
        {
            animator.SetBool("isRun", false);
        }
        else
        {
            animator.SetBool("isRun", true);
        }
    }
    void ChangeMoveType()
    {
        switch (moveType)
        {
            case MoveType.Idle:
                break;
            case MoveType.Patrol:
                break;

            case MoveType.Run:
                Move();
                break;
            case MoveType.Teleport:
                if(teleportTargetObj == null)
                {
                    SpawnTarget();
                }
                else
                {
                    TargetMove();
                    TeleportActive();
                }
                break;
        }
    }
    void Move()
    {
        if (transform.position.x < points[currentPoint].position.x)
        {
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        }
        else
        {
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
        }
    }
    void ChangeMoveType(MoveType type)
    {
        moveType = type;
    }
    void ChangeActive()
    {
        if (Vector2.Distance(gameObject.transform.position, points[currentPoint].position) > 0.1f)
        {
            ChangeMoveType(MoveType.Run);
        }
        else
        {
            if(currentPoint == 0 || currentPoint==points.Length-1)
            {
                countdown -= Time.deltaTime;

                if (countdown <= 0)
                {
                    Flip();
                    currentPoint += isFacingRight ? 1 : -1;
                    countdown= 2;
                    speed = 10;
                }
                else if(countdown<2)
                {
                    speed = 0;
                }
            }
            else
            {
                ChangeMoveType(MoveType.Teleport);
            }
        }
    }
    void SpawnTarget()
    {
        if (canSpawn)
        {
            teleportTargetObj =
            Instantiate(teleportTarget, transform.position, teleportTarget.transform.rotation);
            canSpawn = false;
        }
    }
    void TargetMove()
    {
        if (teleportTargetObj != null)
        {
            if (isFacingRight)
            {
                teleportTargetObj.transform.position =
                    Vector2.MoveTowards(teleportTargetObj.transform.position, points[currentPoint + 1].transform.position, speed * Time.deltaTime);
            }
            else
            {
                teleportTargetObj.transform.position =
                    Vector2.MoveTowards(teleportTargetObj.transform.position, points[currentPoint - 1].transform.position, speed * Time.deltaTime);
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
        else if(!isFacingRight && Vector2.Distance(teleportTargetObj.transform.position, points[currentPoint - 1].transform.position) < 0.1f)
        {
            gameObject.transform.position = teleportTargetObj.transform.position;
            currentPoint = currentPoint - 1;
            DestroyTarget();
        }
        else
        {
            rb2d.velocity = Vector2.zero;
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
        if(Vector2.Distance(gameObject.transform.position, points[currentPoint].transform.position) < 0.1f)
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
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1f;
        transform.localScale = newScale;
    }
    void CheckDied()
    {
        EnemyHealth enemyHealth=GetComponent<EnemyHealth>();
        if(enemyHealth.currentHealth<=0)
        {
            isDied = true;
            DestroyTarget();
        }
    }
}