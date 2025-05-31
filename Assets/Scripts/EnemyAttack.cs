using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;
    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Player player))
        {
            bool check = false;
            if (player.isFacingRight != enemy.isFacingRight
                || (enemy.isFacingRight && player.transform.position.x > enemy.transform.position.x)
                || (!enemy.isFacingRight && player.transform.position.x < enemy.transform.position.x)) check = true;
            if (!enemy.IsAttacking() && check) enemy.SetTarget(player);
        }
    }
    public void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Player player))
        {
            enemy.SetTarget(null);
        }
    }
}