using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speedX;
    public float speedY;
    public float damage;
    public Vector2 power;
    private Vector3 target;
    private Rigidbody2D rb;
    private bool isShooting = false;
    private bool isHit = false;
    [SerializeField]
    private Animator animator;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        StartCoroutine(Countdown());
    }
    void Update()
    {
        SetAnimatorParameter();
    }
    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(3);
        MoveToTarget();
    }
    void MoveToTarget()
    {
        isShooting = true;
        Vector3 direction = target - transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Vector3.Angle(Vector3.up, direction));
        direction = Vector3.Normalize(direction);
        rb.linearVelocityX = speedX * direction.x;
        rb.linearVelocityY = speedY * direction.y;
    }
    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        isHit = true;
        rb.simulated = false;
        if (collider2D.TryGetComponent(out Player player))
        {
            player.GetHit(damage);
            player.GetKnockback(power);
        }
        StartCoroutine(DestroyAnimation());
    }
    private IEnumerator DestroyAnimation()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }
    private void SetAnimatorParameter()
    {
        animator.SetBool("IsShooting", isShooting);
        animator.SetBool("IsHit", isHit);
    }
}