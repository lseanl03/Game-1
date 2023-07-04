using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public bool isDashing;
    public bool pressDash;
    public bool canDash = true;
    public bool alive = true;
    public bool isFacingRight = true;

    private float maxJump = 2f;
    public float pressJump = 0f;
    private Vector2 dashingDirection;
    public float dashingTime = 0.1f;
    public float nextTimeDash = 1f;
    public float timeBetweenDash = 1f;
    [SerializeField] private float jumpForce = 40f;
    [SerializeField] private float vacuumJumpForce = 35f;
    [SerializeField] private float dashingVelocity = 80f;
    private float directionX = 0f;
    [SerializeField] private float moveSpeed = 15f;
    private float originalMoveSpeed;
    private float originalDashingVelocity;
    private float originalJumpForce;
    private float originalVacuumJumpForce;

    private Rigidbody2D rb2d;
    private Animator animator;
    private CapsuleCollider2D capColl2d;
    public GameObject fire;
    public GameObject dashEffect;
    public PauseMenuManager pauseMenu;
    public PlayerInteract playerInteract;
    [SerializeField] private LayerMask jumpableGround;
    private void Start()
    {
        originalMoveSpeed = moveSpeed;
        originalDashingVelocity = dashingVelocity;
        originalJumpForce = jumpForce;
        originalVacuumJumpForce = vacuumJumpForce;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capColl2d = GetComponent<CapsuleCollider2D>();
        playerInteract = GetComponent<PlayerInteract>();
    }
    private void Update()
    {
        if (alive && Time.timeScale == 1)
        {
            if (!playerInteract.interacting)
            {
                Move();
                Jump();
                Run();
                Flip();
                Dash();
            }
            else
            {
                rb2d.velocity = new Vector2 (0f, rb2d.velocity.y);
                animator.SetBool("isRun", false);
            }
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
        if (pressDash && canDash && timeBetweenDash >= nextTimeDash)
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
            StartCoroutine(StopDash());
        }
        if (isDashing)
        {
            FindObjectOfType<AudioManager>().PlaySFX("Dash");
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
        Vector3 dashPos = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        GameObject instance = Instantiate(dashEffect, dashPos, Quaternion.identity);
        Destroy(instance, 1.5f);
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            pressJump++;
            animator.SetBool("isJump", true);
            if (pressJump <= maxJump)
            {
                if (IsGrounded())
                    rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                else
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, vacuumJumpForce);
                    pressJump += 1;
                }
            }
            else
            {
                pressJump = maxJump;
            }
        }
        else
        {
            if (IsGrounded())
                pressJump = 0;
            else
            {
                animator.SetBool("isJump", true);
                animator.SetBool("isRun", false);
            }
        }
        if (Input.GetButtonUp("Jump") && rb2d.velocity.y > 0)
        {
            Vector2 currentVelocity = rb2d.velocity;
            rb2d.velocity = new Vector2(currentVelocity.x, Mathf.Min(currentVelocity.y, -10));
        }
    }
    void Run()
    {
        if (directionX > 0.1f || directionX < -0.1f)
        {
            if (!animator.GetBool("isJump"))
                animator.SetBool("isRun", true);
            if (IsGrounded())
            {
                animator.SetBool("isJump", false);
            }

        }
        else
        {
            animator.SetBool("isRun", false);
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
        float extraHeightText = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(capColl2d.bounds.center, Vector2.down, capColl2d.bounds.extents.y + extraHeightText, jumpableGround);
        float rayDistance = capColl2d.bounds.extents.y + extraHeightText;
        Debug.DrawRay(capColl2d.bounds.center, Vector2.down * rayDistance, Color.red);
        return hit.collider != null;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsGrounded())
            animator.SetBool("isJump", false);
    }
    void CheckDied()
    {
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();

        if (playerHealth.currentHealth <= 0)
        {
            PlayerAttack attack = GetComponent<PlayerAttack>();
            alive = false;
            capColl2d.sharedMaterial = null;
            attack.enabled = false;
            pauseMenu.GameOver();
            Destroy(fire.gameObject);
            playerInteract.interactButton.SetActive(false);
        }
    }
    void PlayerActionLock()
    {
        if (playerInteract.interacting)
        {
            moveSpeed = 0f;
            dashingVelocity = 0f;
            jumpForce = 0f;
            vacuumJumpForce = 0f;
            animator.SetBool("idle", true);
        }
        else
        {
            moveSpeed = originalMoveSpeed;
            dashingVelocity = originalDashingVelocity;
            jumpForce = originalJumpForce;
            vacuumJumpForce = originalVacuumJumpForce;
        }
    }
}