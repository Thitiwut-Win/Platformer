using UnityEngine;

public class Player : BaseUnit
{
    private float horizontalInput;
    [SerializeField]
    private Animator animator;
    private bool isFacingRight = true;
    private bool isJumping = false;
    private bool isGrounded = true;
    private bool isWallSliding = false;
    private bool isWallSticking = false;
    private bool isWallJumping = false;
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
        SetAnimatorParameter();
    }
    void Move()
    {
        Vector2 v = rb.linearVelocity;
        if (!isWallJumping) v.x = horizontalInput * speed;
        if (GroundCheck())
        {
            isGrounded = true;
            isJumping = false;
            isWallJumping = false;
        }
        else isGrounded = false;
        if (WallCheck() && !isGrounded && v.x != 0)
        {
            isWallSliding = true;
            isWallJumping = false;
        }
        else isWallSliding = false;
        if (Input.GetKey(KeyCode.Space))
        {
            print(isGrounded);
            if (isGrounded)
            {
                isJumping = true;
                v.y = jumpSpeed;
            }
            else if (isWallSliding)
            {
                isWallJumping = true;
                v.y = jumpSpeed;
                float jumpDirection = -transform.localScale.x / 4;
                v.x = jumpDirection * speed;
            }
        }
        Flip(v);
        rb.linearVelocity = v;
    }
    private void Flip(Vector3 v)
    {
        if(v.x > 0) transform.localScale = new Vector3(4, 4, 4);
        else if(v.x < 0) transform.localScale = new Vector3(-4, 4, 4);
    }
    private bool GroundCheck()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, isGround);
    }
    private bool WallCheck()
    {
        return Physics2D.OverlapBox(wallCheck.position, wallCheckSize, 0, isWall);
    }
    private void SetAnimatorParameter()
    {
        animator.SetFloat("SpeedX", Mathf.Abs(horizontalInput));
        animator.SetFloat("SpeedY", rb.linearVelocityY);
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsWallSticking", isWallSticking);
        animator.SetBool("IsWallJumping", isWallJumping);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(groundCheck.position, groundCheckSize);
        Gizmos.DrawCube(wallCheck.position, wallCheckSize);
    }
}