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
                _instance = FindObjectOfType<LevelManager>();
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject("LevelManager");
                    _instance = gameObject.AddComponent<LevelManager>();
                }
            }
            return _instance;
        }
    }
    void Awake()
    {
        spawnPosition = transform.position;
    }
    void Start()
    {
        Respawn();
    }
    public void Respawn()
    {
        Player player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        FollowPlayer.Instance.SetPlayer(player.transform);
    }
    public void SetSpawnPosition(Vector3 position)
    {
        spawnPosition = position;
    }
}