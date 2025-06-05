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
        BaseUnit baseUnit = null;
        if (collider2D.TryGetComponent(out Enemy enemy)) baseUnit = enemy;
        if (collider2D.TryGetComponent(out Boss boss)) baseUnit = boss;
        if (baseUnit != null) player.enemyList.Add(baseUnit);
    }
    public void OnTriggerExit2D(Collider2D collider2D)
    {
        BaseUnit baseUnit = null;
        if (collider2D.TryGetComponent(out Enemy enemy)) baseUnit = enemy;
        if (collider2D.TryGetComponent(out Boss boss)) baseUnit = boss;
        if (baseUnit != null) player.enemyList.Remove(baseUnit);
    }
}
