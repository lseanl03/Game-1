using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//public enum MoveType1
//{
//    Idle,
//    Patrol,
//    Run,
//    Teleport,
//}
public class EnemyControllerTesting : MonoBehaviour
{
    public float moveSpeed = 10;
    public float rayDistance = 20f;
    public bool isFacingRight = true;
    public bool playerFound = false;
    public bool canMove = true;
    public bool isGround = true;
    public bool canCheckGround = false;

    public LayerMask playerLayer;
    public LayerMask groundLayer;

    protected Rigidbody2D rb2d;
    protected Animator animator;
    protected EnemyHealth enemyHealth;

    protected virtual void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }
    protected virtual void Update()
    {
        if (!enemyHealth.isDied)
        {
            CheckPlayer();
            CheckGround();
        }
    }
    protected virtual void CheckPlayer()
    {
        Vector3 newVector = new Vector3(0f, 2f);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + newVector, Vector2.right, rayDistance, playerLayer);
        Debug.DrawRay(transform.position + newVector, Vector2.right * rayDistance, Color.red);

        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + newVector, Vector2.left, rayDistance, playerLayer);
        Debug.DrawRay(transform.position + newVector, Vector2.left * rayDistance, Color.red);
        if (hitLeft.collider != null)
        {
            if (isFacingRight)
                Flip();
        }
        if (hitRight.collider != null)
        {
            if (!isFacingRight)
                Flip();
        }
        if ((hitLeft.collider != null) || (hitRight.collider != null))
        {
            playerFound = true;
        }
        if ((hitLeft.collider == null) && (hitRight.collider == null))
        {
            playerFound = false;
        }
    }
    protected virtual void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1f;
        transform.localScale = newScale;
    }
    protected virtual void CheckGround()
    {
        Vector3 newVector;
        if (isFacingRight)
        {
            newVector = new Vector3(2f, 0f);
        }
        else
        {
            newVector = new Vector3(-2f, 0f);
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position + newVector, Vector2.down, 1f, groundLayer);
        Debug.DrawRay(transform.position + newVector, Vector2.down * 1f, Color.red);
        if (hit.collider == null)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
    }
}
