using UnityEngine;

public class BossAggro : MonoBehaviour
{
    [SerializeField]
    Boss boss;
    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Player player))
        {
            boss.SetTarget(player);
        }
    }
}