using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseUnit
{
    private int id = 0;
    private Vector2 direction;
    public float moveTime;
    public float waitTime;
    public bool willWait = true;
    private EState eState = EState.IDLE;
    public List<Vector2> moveCycle;
    private Player target;
    void Start()
    {
        StartCoroutine(ChangeDirection());
    }
    void Update()
    {
        SetAnimatorParameter();
        if (hasCooldown && target != null) Attack();
    }
    public void Attack()
    {
        isAttacking = true;
        hasCooldown = false;
        StartCoroutine(AttackAnimation());
        StartCoroutine(AttackCooldown());
    }
    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(preAttackTime);
        target?.GetHit(this);
        yield return new WaitForSeconds(attackTime);
        isAttacking = false;
    }
    void Move()
    {
        rb.linearVelocityX = direction.x * speedX;
        rb.linearVelocityY = direction.y * speedY;
        Flip();
    }
    private IEnumerator StopMoving()
    {
        yield return new WaitForSeconds(moveTime);
        direction.x = 0;
        direction.y = 0;
        eState = EState.IDLE;
        Move();
    }
    private IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(moveTime + waitTime);
        direction.x = moveCycle[id].x;
        direction.y = moveCycle[id].y;
        id++;
        eState = EState.MOVE;
        if (id >= moveCycle.Count) id = 0;
        StartCoroutine(ChangeDirection());
        Move();
        if (willWait) StartCoroutine(StopMoving());
    }
    public void SetTarget(Player player)
    {
        target = player;
    }
    public override void SetAnimatorParameter()
    {
        base.SetAnimatorParameter();
        animator.SetFloat("SpeedX", Mathf.Abs(rb.linearVelocityX));
    }
}