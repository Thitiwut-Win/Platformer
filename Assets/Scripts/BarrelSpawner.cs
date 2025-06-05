using System.Collections;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    public Transform barrelPrefab;
    public float spawnInterval;
    public float offset;
    public Vector2 direction;
    public float initialSpeed;
    void Start()
    {
        StartCoroutine(OffsetCountdown());
    }
    private IEnumerator SpawnBarrel()
    {
        yield return new WaitForSeconds(spawnInterval);
        Transform barrel = Instantiate(barrelPrefab, transform.position + new Vector3(2 * direction.x, 2 * direction.y, 0), Quaternion.identity);
        Rigidbody2D rb = barrel.GetComponent<Rigidbody2D>();
        rb.linearVelocityX = initialSpeed;
        StartCoroutine(SpawnBarrel());
    }
    private IEnumerator OffsetCountdown()
    {
        yield return new WaitForSeconds(offset);
        StartCoroutine(SpawnBarrel());
    }
}