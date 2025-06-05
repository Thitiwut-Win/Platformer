using System.Collections;
using UnityEngine;

public class Boss : BaseUnit
{
    protected bool isAggro = false;
    protected Player target;
    public virtual void Update()
    {
        SetAnimatorParameter();
        isAggro = target != null;
        if (!isAggro)
        {
            rb.linearVelocityX = 0;
            rb.linearVelocityY = 0;
        }
    }
    protected void MoveToTarget(float spX, float spY)
    {
        Vector3 direc = Vector3.Normalize(target.transform.position - transform.position);
        rb.linearVelocityX = direc.x * spX;
        rb.linearVelocityY = direc.y * spY;
        Flip();
    }
    public virtual void Attack()
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
    public void SetTarget(Player player)
    {
        target = player;
    }
}