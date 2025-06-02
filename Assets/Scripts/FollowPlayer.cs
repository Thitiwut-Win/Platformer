using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private float offset = -10;
    private static FollowPlayer _instance;
    public static FollowPlayer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<FollowPlayer>();
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject("FollowPlayer");
                    _instance = gameObject.AddComponent<FollowPlayer>();
                }
            }
            return _instance;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + new Vector3(0, 0, offset);
    }
    void SetOffset(float off)
    {
        offset = off;
    }
    public void SetPlayer(Transform transform)
    {
        player = transform;
    }
}
