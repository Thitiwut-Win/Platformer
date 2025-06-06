using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class BossAggro : MonoBehaviour
{
    [SerializeField]
    Boss boss;
    private static BossAggro _instance;
    public static BossAggro Instance
    {
        get
        {
            if (_instance == null)
            {
#pragma warning disable CS0618 // Type or member is obsolete
                _instance = FindObjectOfType<BossAggro>();
#pragma warning restore CS0618 // Type or member is obsolete
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject("BossAggro");
                    _instance = gameObject.AddComponent<BossAggro>();
                }
            }
            return _instance;
        }
    }
    [FormerlySerializedAs("onAggro")]
    [SerializeField]
    private UnityEvent m_OnAggro;
    public UnityEvent onAggro
    {
        get { return m_OnAggro; }
        set { m_OnAggro = value; }
    }
    [FormerlySerializedAs("onDisAggro")]
    [SerializeField]
    private UnityEvent m_OnDisAggro;
    public UnityEvent onDisAggro
    {
        get { return m_OnDisAggro; }
        set { m_OnDisAggro = value; }
    }
    [FormerlySerializedAs("onBossDied")]
    [SerializeField]
    private UnityEvent m_OnBossDied;
    public UnityEvent onBossDied
    {
        get { return m_OnBossDied; }
        set { m_OnBossDied = value; }
    }
    [FormerlySerializedAs("onPaused")]
    [SerializeField]
    private UnityEvent m_OnPaused;
    public UnityEvent onPaused
    {
        get { return m_OnPaused; }
        set { m_OnPaused = value; }
    }
    [FormerlySerializedAs("onUnpaused")]
    [SerializeField]
    private UnityEvent m_OnUnpaused;
    public UnityEvent onUnpaused
    {
        get { return m_OnUnpaused; }
        set { m_OnUnpaused = value; }
    }
    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Player player))
        {
            boss.SetTarget(player);
            m_OnAggro.Invoke();
        }
    }
}