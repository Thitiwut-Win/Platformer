using System.Collections;
using UnityEngine;

public class Boss : BaseUnit
{
    protected bool isAggro = false;
    // public bool inRange = false;
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
        StartCoroutine(AttackAnimation());
        // StartCoroutine(AttackCooldown());
    }
    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(attackCooldown);
        // if(inRange) target?.GetHit(this);
        // yield return new WaitForSeconds(attackTime);
        isAttacking = false;
    }
    public override IEnumerator DieAnimation()
    {
        yield return new WaitForSeconds(0.7f);
        rb.simulated = false;
        BossAggro.Instance.onBossDied.Invoke();
        Time.timeScale = 0;
    }
    public void SetTarget(Player player)
    {
        target = player;
    }
}