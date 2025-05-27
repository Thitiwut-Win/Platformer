using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float damage;
    public float speed;
    public float jumpSpeed;
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
