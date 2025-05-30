using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;
    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Player player))
        {
            int direc = enemy.isFacingRight ? 1 : -1;
            player.GetHit(enemy);
        }
    }
}