using UnityEngine;

public class Player : BaseUnit
{
    [SerializeField]
    private LayerMask isGround;
    [SerializeField]
    private Transform groundCheck;
    private bool isGrounded = true;
    void Update()
    {
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.05f, isGround);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject) isGrounded = true;
        }
        Move();
    }
    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 v = rb.linearVelocity;
        v.x =  horizontalInput * speed;
        if (Input.GetKey(KeyCode.Space) && isGrounded) v.y = 4;
        rb.linearVelocity = v;
    }
}