using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float damage;
    public float speedX;
    public float speedY;
    [SerializeField]
    protected float attackCooldown;
    protected bool isAttacking = false;
    protected bool hasCooldown = true;
    [SerializeField]
    protected Animator animator;
    protected Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D> ();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
