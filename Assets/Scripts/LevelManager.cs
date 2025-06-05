using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Player playerPrefab;
    private Vector3 spawnPosition;
    private static LevelManager _instance;
    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
#pragma warning disable CS0618 // Type or member is obsolete
                _instance = FindObjectOfType<LevelManager>();
#pragma warning restore CS0618 // Type or member is obsolete
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject("LevelManager");
                    _instance = gameObject.AddComponent<LevelManager>();
                }
            }
            return _instance;
        }
    }
    public List<Checkpoint> checkpoints;
    private int cp = 0;
    public int summonCount = 0;
    void Awake()
    {
        spawnPosition = transform.position;
    }
    void Start()
    {
        Respawn();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            checkpoints[cp].Activate();
            cp++;
        }
    }
    public void Respawn()
    {
        Player player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        FollowPlayer.Instance.SetPlayer(player.transform);
        FollowPlayer.Instance.SetCameraSize(5);
    }
    public void SetSpawnPosition(Vector3 position)
    {
        spawnPosition = position;
    }
}