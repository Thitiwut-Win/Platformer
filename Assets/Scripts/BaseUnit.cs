using System.Collections;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float damage;
    public Vector2 power;
    public float weight;
    public float speedX;
    public float speedY;
    public float scale;
    public bool isFacingRight = true;
    public float attackTime;
    public float attackCooldown;
    protected bool isAttacking = false;
    public bool hasCooldown = true;
    protected bool isHit = false;
    protected bool isKnockback = false;
    protected bool isDead = false;
    [SerializeField]
    protected Animator animator;
    protected Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    protected void Flip()
    {
        if (rb.linearVelocityX > 0)
        {
            transform.localScale = new Vector3(scale, scale, scale);
            isFacingRight = true;
        }
        else if (rb.linearVelocityX < 0)
        {
            transform.localScale = new Vector3(-scale, scale, scale);
            isFacingRight = false;
        }
    }
    public void GetHit(BaseUnit other)
    {
        if (!isHit)
        {
            isHit = true;
            health -= other.damage;
            GetKnockback(other);
            StartCoroutine(HurtAnimation());
            if (health <= 0 && !isDead)
            {
                isDead = true;
                StartCoroutine(DieAnimation());
            }
        }
    }
    private void GetKnockback(BaseUnit other)
    {
        int direc;
        if (isFacingRight != other.isFacingRight)
        {
            if (other.isFacingRight) direc = 1;
            else direc = -1;
        }
        else
        {
            if (other.transform.position.x < transform.position.x && isFacingRight) direc = 1;
            else if (other.transform.position.x > transform.position.x && !isFacingRight) direc = -1;
            else direc = other.isFacingRight ? -1 : 1;

        }
        rb.linearVelocityX = power.x / weight * direc;
        rb.linearVelocityY = power.y / weight;
        isKnockback = true;
        StartCoroutine(Knockback());
    }
    private IEnumerator Knockback()
    {
        yield return new WaitForSeconds(0.1f);
        isKnockback = false;
        rb.linearVelocityX = 0;
    }
    private IEnumerator HurtAnimation()
    {
        yield return new WaitForSeconds(0.7f);
        isHit = false;
    }
    private IEnumerator DieAnimation()
    {
        yield return new WaitForSeconds(0.7f);
        rb.simulated = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    public virtual void SetAnimatorParameter()
    {
        animator.SetBool("IsAttacking", isAttacking);
        animator.SetBool("IsHit", isHit);
        animator.SetBool("IsDead", isDead);
    }
}
