using Unity.VisualScripting;
using UnityEngine;

public enum MoveType{
    Run,
    Teleport 
}
public class EnemyController2 : MonoBehaviour
{
    public MoveType moveType;

    private Rigidbody2D rb2d;
    public Transform[] points;

    public GameObject teleportTargetObj;
    public GameObject teleportTargetSelect;

    public int currentPoint = 0;
    public float speed = 5f;

    public bool canSpawn = true;
    public bool isFacingRight = true;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        moveType = MoveType.Run;
    }
    private void Update()
    {
        ChangeActive();
        Flip();
        switch (moveType)
        {
            case MoveType.Run:
                Move();
                break;
            case MoveType.Teleport:
                SpawnTarget();
                TargetMove();
                TeleportActive();
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
            Debug.Log("MoveType: " + moveType.ToString());
        }
        else
        {
            if (transform.position.x == points[points.Length - 1].position.x)
            {
                isFacingRight = false;
                currentPoint--;
                ChangeMoveType(MoveType.Run);
            }
            ChangeMoveType(MoveType.Teleport);
            Debug.Log("MoveType: " + moveType.ToString());
        }
    }
    void SpawnTarget()
    {
        if (canSpawn)
        {
            teleportTargetObj =
            Instantiate(teleportTargetSelect, transform.position, teleportTargetSelect.transform.rotation);
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
        if (Vector2.Distance(teleportTargetObj.transform.position, points[currentPoint + 1].transform.position) < 0.1f)
        {
            gameObject.transform.position = teleportTargetObj.transform.position;
            currentPoint = currentPoint + 1;
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
        //if (Vector2.Distance(gameObject.transform.position, points[points.Length-1].transform.position) < 0.1f)
        //{
        //    isFacingRight = false;
        //    currentPoint--;
        //}
        //else if (Vector2.Distance(gameObject.transform.position, points[0].transform.position) < 0.1f)
        //{
        //    isFacingRight = true;
        //    currentPoint++;
        //}
    }
}