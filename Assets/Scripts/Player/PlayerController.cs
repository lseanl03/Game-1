using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private bool isDashing;
    public bool pressDash;
    public bool canDash = true;
    public bool alive=true;
    public bool isFacingRight = true; //hướng mặt sang phải

    private float maxJump = 2f;
    private float jumped = 0f;//đếm số lần nhảy
    [SerializeField] private float jumpForce = 40f;//lực nhảy
    private Vector2 dashingDirection;
    public float dashingTime = 0.1f;
    public float nextTimeDash = 1f;
    public float timeBetweenDash = 1f;
    [SerializeField] private float dashingVelocity = 80f;
    private float directionX = 0f;
    [SerializeField] private float moveSpeed = 15f;

    private Rigidbody2D rb2d;
    private Animator animator;
    private CapsuleCollider2D coll;
    public GameObject fire;
    public GameObject dashEffect;
    public PlayManager manager;
    [SerializeField] private LayerMask jumpableGround; //mặt đất có thể nhảy
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        if (alive)
        {
            Move();
            Jump();
            Run();
            Flip();
            Dash();
        }
        CheckDied();
    }
   void Move()
    {
        directionX = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(directionX * moveSpeed, rb2d.velocity.y);
    }
    void Dash()
    {
        pressDash = Input.GetMouseButtonDown(1);
        if (pressDash && canDash && timeBetweenDash>=nextTimeDash)
        {
            DashEffect();
            timeBetweenDash = 0;
            isDashing = true;
            canDash = false;
            dashingDirection = new Vector2(directionX, 0f);
            if (dashingDirection == Vector2.zero)
            {
                dashingDirection = new Vector2(transform.localScale.x, 0f);
            }
            StartCoroutine(StopDash()); //Bắt đầu quy trình
        }
        if (isDashing)
        {
            animator.SetBool("isRun", false);
            rb2d.velocity = dashingDirection.normalized * dashingVelocity;
            return;
        }
        if (IsGrounded())
        {
                canDash = true;
        }
        else
        {
            animator.SetBool("isRun", false);
        }
        timeBetweenDash += Time.deltaTime;
    }
    IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(nextTimeDash);
        canDash = true;
    }
    void DashEffect()
    {
        Vector3 dashPos= new Vector3(transform.position.x,transform.position.y+2,transform.position.z);
        GameObject instance= (GameObject)Instantiate(dashEffect, dashPos, Quaternion.identity);
        Destroy(instance, 1.5f);
    }
    void Jump()
    {
        while (Input.GetButtonDown("Jump") && jumped <= maxJump)
        {
            jumped++;
            animator.SetBool("isJump", true);
            if (IsGrounded())
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            else
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce - 15);
        }
        if (IsGrounded())
            jumped = 0;
        else
        {
            animator.SetBool("isRun", false);
            animator.SetBool("isJump", true);
        }
        if (Input.GetButtonUp("Jump") && rb2d.velocity.y > 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y / 2f);
        }
    }
    void Run()
    {
        animator.SetBool("isRun", false);
        if (directionX > 0.1f || directionX < -0.1f)
        {
            if (!animator.GetBool("isJump"))
                animator.SetBool("isRun", true);
        }
    }
    void Flip()
    {
        if (directionX > 0 && !isFacingRight || directionX < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 newScale = transform.localScale;
            newScale.x *= -1f;
            transform.localScale = newScale;
        }
    }
    public bool IsGrounded()
    {
        float extraHeightText = 0.05f;
        RaycastHit2D raycastHit = Physics2D.Raycast(coll.bounds.center, Vector2.down, coll.bounds.extents.y + extraHeightText,jumpableGround);
        return raycastHit.collider != null;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsGrounded())
            animator.SetBool("isJump", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
    void CheckDied()
    {
        PlayerHealth playerHealth=GetComponent<PlayerHealth>();

        if(playerHealth.currentHealth<=0)
        {
            PlayerAttack attack=GetComponent<PlayerAttack>();
            alive = false; 
            coll.sharedMaterial = null;
            attack.enabled = false;
            manager.GameOver();
            Destroy(fire.gameObject);
        }
    }
}