using System.Collections;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float damage;
    public float speedX;
    public float speedY;
    public float scale;
    public bool isFacingRight = true;
    public float attackTime;
    public float attackCooldown;
    protected bool isAttacking = false;
    public bool hasCooldown = true;
    protected bool isHit = false;
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
    public void GetHit(float dmg)
    {
        if (!isHit)
        {
            isHit = true;
            health -= dmg;
            StartCoroutine(HurtAnimation());
            if (health <= 0 && !isDead)
            {
                StartCoroutine(DieAnimation());
            }
        }
    }
    private IEnumerator HurtAnimation()
    {
        yield return new WaitForSeconds(0.7f);
        isHit = false;
    }
    private IEnumerator DieAnimation()
    {
        yield return new WaitForSeconds(0.7f);
        isDead = true;
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
