using System.Collections;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    public Transform barrelPrefab;
    public float spawnInterval;
    void Start()
    {
        StartCoroutine(SpawnBarrel());
    }
    private IEnumerator SpawnBarrel()
    {
        yield return new WaitForSeconds(spawnInterval);
        Transform barrel = Instantiate(barrelPrefab, transform.position + new Vector3(3, 0, 0), Quaternion.identity);
        Rigidbody2D rb = barrel.GetComponent<Rigidbody2D>();
        rb.linearVelocityX = 5;
        StartCoroutine(SpawnBarrel());
    }
}