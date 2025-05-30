using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Enemy enemy))
        {
            player.enemyList.Add(enemy);
        }
    }
    public void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Enemy enemy))
        {
            player.enemyList.Remove(enemy);
        }
    }
}
