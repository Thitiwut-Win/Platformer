using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Camera camera;
    private Transform player;
    private float offset = -10;
    private static FollowPlayer _instance;
    public static FollowPlayer Instance
    {
        get
        {
            if (_instance == null)
            {
#pragma warning disable CS0618 // Type or member is obsolete
                _instance = FindObjectOfType<FollowPlayer>();
#pragma warning restore CS0618 // Type or member is obsolete
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject("FollowPlayer");
                    _instance = gameObject.AddComponent<FollowPlayer>();
                }
            }
            return _instance;
        }
    }
    void Awake()
    {
        camera = transform.GetComponent<Camera>();
    }
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
    public void SetCameraSize(float size)
    {
        camera.orthographicSize = size;
    }
}
