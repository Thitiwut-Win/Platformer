using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isActivated = false;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Player player))
        {
            Activate();
        }
    }
    public void Activate()
    {
        isActivated = true;
        spriteRenderer.color = Color.white;
        LevelManager.Instance.SetSpawnPosition(transform.position);
    }
}