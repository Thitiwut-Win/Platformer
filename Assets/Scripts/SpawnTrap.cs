using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SpawnTrap : MonoBehaviour
{
    private bool isActivated = false;
    public List<SpawnInfo> spawns = new List<SpawnInfo>();
    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (!isActivated && collider2D.TryGetComponent(out Player player))
        {
            isActivated = true;
            foreach (SpawnInfo spawn in spawns)
            {
                Enemy enemy = Instantiate(spawn.enemy, transform.position + spawn.spawnpoint, Quaternion.identity);
                enemy.moveCycle = spawn.moveCycle;
                enemy.waitTime = 0;
                enemy.willWait = false;
            }
        }
    }
}

[System.Serializable]
public class SpawnInfo
{
    public Enemy enemy;
    public Vector3 spawnpoint;
    public List<Vector2> moveCycle;
}