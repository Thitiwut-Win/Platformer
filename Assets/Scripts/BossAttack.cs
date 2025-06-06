using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Player player))
        {
            Boss boss = transform.parent.GetComponent<Boss>();
            bool check = false;
            if (player.isFacingRight != boss.isFacingRight
                || (boss.isFacingRight && player.transform.position.x > boss.transform.position.x)
                || (!boss.isFacingRight && player.transform.position.x < boss.transform.position.x)) check = true;
            if (!boss.IsAttacking() && check)
            {
                // boss.inRange = true;
                boss.Attack();
            }
        }
    }
    // public void OnTriggerExit2D(Collider2D collider2D)
    // {
    //     if (collider2D.TryGetComponent(out Player player))
    //     {
    //         Boss boss = transform.parent.GetComponent<Boss>();
    //         boss.inRange = false;
    //     }
    // }
}