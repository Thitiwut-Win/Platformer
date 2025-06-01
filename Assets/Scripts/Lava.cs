using UnityEngine;

public class Lava : MonoBehaviour
{
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
        if (collider2D.TryGetComponent(out BaseUnit baseUnit))
        {
            Destroy(baseUnit.gameObject);
        }
    }
}
