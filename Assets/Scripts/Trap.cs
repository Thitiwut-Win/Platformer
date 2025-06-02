using UnityEngine;

public class Trap : MonoBehaviour
{
    public float damage;
    public Vector2 power;
    public void OnColliderEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out BaseUnit baseUnit))
        {
            baseUnit.GetHit(this);
        }
    }
}