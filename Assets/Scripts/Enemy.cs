using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseUnit
{
    private int id = 0;
    private bool hasMove = false;
    [SerializeField]
    Vector2 direction;
    [SerializeField]
    List<Pair<int, int>> MoveCycle;
    void Start()
    {
        StartCoroutine(ChangeDirection());
    }
    void Update()
    {
        if (!hasMove) Move();
    }
    void Move()
    {
        rb.linearVelocityX = direction.x * speedX;
        rb.linearVelocityY = direction.y * speedY;
        StartCoroutine(StopMoving());
    }
    private IEnumerator StopMoving()
    {
        hasMove = true;
        yield return new WaitForSeconds(3);
        hasMove = false;
    }
    private IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(6);
        direction.x *= MoveCycle[id].first;
        direction.y *= MoveCycle[id].second;
        id++;
        if (id >= MoveCycle.Count) id = 0;
        StartCoroutine(ChangeDirection());
    }
    private void SetAnimatorParameter()
    {
        animator.SetFloat("SpeedX", Mathf.Abs(rb.linearVelocityX));
        animator.SetBool("IsAttacking", isAttacking);
    }
}

class Pair<T, U>
{
    public Pair()
    {}

    public Pair(T first, U second)
    {
        this.first = first;
        this.second = second;
    }

    public T first { get; set; }
    public U second { get; set; }
}