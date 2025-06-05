using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Player player))
        {
            BaseUnit enemy = transform.parent.GetComponent<BaseUnit>();
            player.GetHit(enemy);
        }
    }
}