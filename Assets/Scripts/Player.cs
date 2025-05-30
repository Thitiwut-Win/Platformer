using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseUnit
{
    private float horizontalInput;
    private bool isGrounded = true;
    private bool isWalled = false;
    private bool isWallSliding = false;
    private bool isWallJumping = false;
    public List<Enemy> enemyList;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [Header("GroundCheck")]
    [SerializeField]
    private LayerMask isGround;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Vector2 groundCheckSize;
    [Header("WallCheck")]
    [SerializeField]
    private LayerMask isWall;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Vector2 wallCheckSize;
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        Move();
        if (Input.GetMouseButtonDown(0) && hasCooldown) Attack();
        SetAnimatorParameter();
    }
    void Move()
    {
        Vector2 v = rb.linearVelocity;
        if (!isWallJumping) v.x = horizontalInput * speedX;
        GroundCheck();
        WallCheck();
        if (isWalled && !isGrounded && horizontalInput != 0)
        {
            isWallSliding = true;
            animator.SetBool("IsWallSliding", true);
        }
        else
        {
            isWallSliding = false;
            animator.SetBool("IsWallSliding", false);
        }
        // if (!isGrounded) v.y -= 9.81f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isGrounded)
                {
                    animator.SetBool("IsJumping", true);
                    v.y = speedY;
                }
                else if (isWallSliding)
                {
                    isWallSliding = false;
                    isWallJumping = true;
                    animator.SetBool("IsWallSliding", false);
                    animator.SetBool("IsWallJumping", true);
                    v.y = speedY;
                    float jumpDirection = -transform.localScale.x / 4;
                    v.x = jumpDirection * speedX;
                }
            }
        rb.linearVelocity = v;
        Flip();
    }
    private void Attack()
    {
        isAttacking = true;
        hasCooldown = false;
        foreach (Enemy enemy in enemyList)
        {
            enemy.GetHit(damage);
        }
        StartCoroutine(AttackAnimation());
        StartCoroutine(AttackCooldown());
    }
    private IEnumerator AttackAnimation()
    {   
        yield return new WaitForSeconds(0.667f);
        isAttacking = false;
    }
    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        hasCooldown = true;
    }
    private void GroundCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(groundCheck.position, groundCheckSize, 0, isGround);
        for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				isGrounded = true;
                if (!wasGrounded)
                {
                    isWallSliding = false;
                    isWallJumping = false;
                    animator.SetBool("IsJumping", false);
                    animator.SetBool("IsWallSliding", false);
                    animator.SetBool("IsWallJumping", false);
                }
			}
		}
    }
    private void WallCheck()
    {
        isWalled = false;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(wallCheck.position, wallCheckSize, 0, isWall);
        for (int i = 0; i < colliders.Length; i++)
		{
            if (colliders[i].gameObject != gameObject)
            {
                isWalled = true;
                isWallSliding = false;
                isWallJumping = false;
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsWallSliding", false);
                animator.SetBool("IsWallJumping", false);
			}
		}
    }
    public override void SetAnimatorParameter()
    {
        base.SetAnimatorParameter();
        animator.SetFloat("SpeedX", Mathf.Abs(horizontalInput));
        animator.SetFloat("SpeedY", rb.linearVelocityY);
    }
    public bool IsAttacking()
    {
        return isAttacking;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(groundCheck.position, groundCheckSize);
        Gizmos.DrawCube(wallCheck.position, wallCheckSize);
    }
}